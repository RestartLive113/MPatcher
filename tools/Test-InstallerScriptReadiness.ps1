[CmdletBinding()]
param(
    [Parameter(Mandatory=$true)][string]$InstallerPath,
    [Parameter(Mandatory=$true)][string]$PreviousInstallerPath,
    [Parameter(Mandatory=$true)][string]$PayloadPath,
    [Parameter(Mandatory=$true)][string]$ExternalLoaderPath,
    [Parameter(Mandatory=$true)][string]$EvidenceDirectory
)

$ErrorActionPreference = 'Stop'
Set-StrictMode -Version Latest
$repo = Split-Path $PSScriptRoot -Parent
$fixtureRoot = Join-Path $repo ('installer-tests\script-readiness-' + [DateTime]::UtcNow.ToString('yyyyMMdd-HHmmss-fff'))
New-Item -ItemType Directory -Path $fixtureRoot,$EvidenceDirectory -Force | Out-Null
$payloadHash = (Get-FileHash -LiteralPath $PayloadPath).Hash
$externalHash = (Get-FileHash -LiteralPath $ExternalLoaderPath).Hash
$results = [ordered]@{}

function Check([bool]$Condition, [string]$Message) {
    if (-not $Condition) { throw $Message }
}
function New-Fixture([string]$Name) {
    $path = Join-Path $fixtureRoot $Name
    New-Item -ItemType Directory -Path (Join-Path $path 'McnCraft_Data\Mono'),(Join-Path $path 'UserData') -Force | Out-Null
    [IO.File]::WriteAllBytes((Join-Path $path 'McnCraft.exe'),[byte[]](77,90))
    [IO.File]::WriteAllBytes((Join-Path $path 'McnCraft_Data\Mono\mono.dll'),[byte[]](77,90))
    [IO.File]::WriteAllText((Join-Path $path 'UserData\script.cs'),'preserve user script')
    return $path
}
function Invoke-Install([string]$Installer,[string]$Action,[string]$Fixture,[int]$Expected=0) {
    $process = Start-Process -FilePath $Installer -ArgumentList @('--lang','en',('--'+$Action),('"'+$Fixture+'"')) -Wait -PassThru -WindowStyle Hidden
    if ($process.ExitCode -ne $Expected) {
        $tail = (Get-Content -LiteralPath (Join-Path $Fixture 'logs\MPatcherInstaller.log') -Tail 15) -join "`n"
        throw "Installer returned $($process.ExitCode), expected $Expected : $tail"
    }
    Check ((Get-Content -LiteralPath (Join-Path $Fixture 'UserData\script.cs') -Raw) -eq 'preserve user script') 'UserData changed'
}
function Read-Manifest([string]$Fixture) {
    $result = @{}
    foreach($line in Get-Content -LiteralPath (Join-Path $Fixture 'McnCraft_Data\MPatcherFork.install.ini')) {
        $split = $line.IndexOf('=')
        if($split -gt 0) { $result[$line.Substring(0,$split)]=$line.Substring($split+1) }
    }
    return $result
}
function Write-Loader([string]$Fixture,[byte]$Value) {
    $path=Join-Path $Fixture 'McnCraft_Data\Mono\__Internal.dll'
    [IO.File]::WriteAllBytes($path,[byte[]](77,90,$Value,7,8,9))
    return (Get-FileHash -LiteralPath $path).Hash
}
function Add-Record([string]$Fixture,[string]$Name,[string]$Hash) {
    Add-Content -LiteralPath (Join-Path $Fixture 'logs\MPatcherInstaller.log') -Value ('2026-09-13 00:00:00.000 BACKUP_CREATED sha256='+$Hash+' file='+$Name)
}
function Seed-History([string]$Fixture) {
    $originalHash=Write-Loader $Fixture 11
    Invoke-Install $PreviousInstallerPath 'install' $Fixture
    $first=Read-Manifest $Fixture
    Copy-Item -LiteralPath $ExternalLoaderPath -Destination (Join-Path $Fixture 'McnCraft_Data\Mono\__Internal.dll') -Force
    Invoke-Install $PreviousInstallerPath 'install' $Fixture
    $current=Read-Manifest $Fixture
    Check ($first.BackupFileName -ne $current.BackupFileName) 'History fixture did not create two backups'
    return @{FirstName=$first.BackupFileName;FirstHash=$originalHash;CurrentName=$current.BackupFileName;CurrentHash=$externalHash}
}

$clean=New-Fixture 'work-readiness'
Invoke-Install $InstallerPath 'install' $clean
Check (Test-Path -LiteralPath (Join-Path $clean 'Work') -PathType Container) 'Missing script compiler Work directory'
$workFile=Join-Path $clean 'Work\keep.csw'
[IO.File]::WriteAllText($workFile,'keep compiler scratch data')
Invoke-Install $InstallerPath 'install' $clean
Invoke-Install $InstallerPath 'uninstall' $clean
Check ((Get-Content -LiteralPath $workFile -Raw) -eq 'keep compiler scratch data') 'Existing Work contents changed'
$results.work_created_and_preserved='passed'

$manifestOnly=New-Fixture 'prior-manifest-without-log'
$originalHash=Write-Loader $manifestOnly 12
Invoke-Install $PreviousInstallerPath 'install' $manifestOnly
$original=Read-Manifest $manifestOnly
Move-Item -LiteralPath (Join-Path $manifestOnly 'logs\MPatcherInstaller.log') -Destination (Join-Path $manifestOnly 'previous-installer.log')
Copy-Item -LiteralPath $ExternalLoaderPath -Destination (Join-Path $manifestOnly 'McnCraft_Data\Mono\__Internal.dll') -Force
Invoke-Install $InstallerPath 'install' $manifestOnly
$current=Read-Manifest $manifestOnly
$archived=Join-Path $manifestOnly ('MPatcherBackupHistory\'+$original.BackupFileName)
Check ((Get-FileHash -LiteralPath $archived).Hash -eq $originalHash) 'Previous-manifest backup was not preserved outside data'
Check ($current.OriginalSha256 -eq $externalHash) 'Wrong current rollback loader'
Check (@(Get-ChildItem -LiteralPath (Join-Path $manifestOnly 'McnCraft_Data\MPatcherForkBackup') -Filter '*.dll').Count -eq 1) 'Superseded DLL is still scanned'
Invoke-Install $InstallerPath 'uninstall' $manifestOnly
Check ((Get-FileHash -LiteralPath (Join-Path $manifestOnly 'McnCraft_Data\Mono\__Internal.dll')).Hash -eq $externalHash) 'External loader was not restored'
$results.superseded_manifest_without_log='passed'

$historyFixture=New-Fixture 'recorded-history-and-unknown-files'
$history=Seed-History $historyFixture
$backupDirectory=Join-Path $historyFixture 'McnCraft_Data\MPatcherForkBackup'
$archiveDirectory=Join-Path $historyFixture 'MPatcherBackupHistory'
New-Item -ItemType Directory -Path $archiveDirectory | Out-Null
$collision=Join-Path $archiveDirectory $history.FirstName
[IO.File]::WriteAllText($collision,'preexisting history must survive')
$unknownNames=@('unknown.dll','__Internal.unrecorded.dll','__Internal.modified.dll','__Internal.conflicting.dll')
$unknownHashes=@{}
foreach($name in $unknownNames) {
    $file=Join-Path $backupDirectory $name
    [IO.File]::WriteAllText($file,'retain '+$name)
    $unknownHashes[$name]=(Get-FileHash -LiteralPath $file).Hash
}
Add-Record $historyFixture '__Internal.modified.dll' ('0'*64)
Add-Record $historyFixture '__Internal.conflicting.dll' $unknownHashes['__Internal.conflicting.dll']
Add-Record $historyFixture '__Internal.conflicting.dll' ('0'*64)
$outside=Join-Path $historyFixture 'outside.dll'
[IO.File]::WriteAllText($outside,'outside backup directory')
Add-Record $historyFixture '__Internal.bad/../../outside.dll' (Get-FileHash -LiteralPath $outside).Hash
Invoke-Install $InstallerPath 'install' $historyFixture
Check (-not (Test-Path -LiteralPath (Join-Path $backupDirectory $history.FirstName))) 'Logged historical DLL remains in data'
Check ((Get-Content -LiteralPath $collision -Raw) -eq 'preexisting history must survive') 'Archive collision overwritten'
$matchingArchives=@(Get-ChildItem -LiteralPath $archiveDirectory -File | Where-Object { (Get-FileHash -LiteralPath $_.FullName).Hash -eq $history.FirstHash })
Check ($matchingArchives.Count -eq 1) 'Historical bytes not archived exactly once'
Check ((Get-FileHash -LiteralPath (Join-Path $backupDirectory $history.CurrentName)).Hash -eq $history.CurrentHash) 'Current rollback backup changed'
$results.recorded_history_and_collision='passed'
foreach($name in $unknownNames) { Check ((Get-FileHash -LiteralPath (Join-Path $backupDirectory $name)).Hash -eq $unknownHashes[$name]) ('Unverified file moved or changed: '+$name) }
Check ((Get-Content -LiteralPath $outside -Raw) -eq 'outside backup directory') 'Traversal record changed another file'
$results.unknown_modified_conflicting_and_traversal_retained='passed'

$rollback=New-Fixture 'history-rollback'
$history=Seed-History $rollback
$loader=Join-Path $rollback 'McnCraft_Data\Mono\__Internal.dll'
$manifest=Join-Path $rollback 'McnCraft_Data\MPatcherFork.install.ini'
$watchdog=Join-Path $rollback 'McnCraft_Data\MPatcherFork\MPatcherCrashWatchdog.exe'
$beforeLoader=(Get-FileHash -LiteralPath $loader).Hash
$beforeManifest=(Get-FileHash -LiteralPath $manifest).Hash
$beforeWatchdog=(Get-FileHash -LiteralPath $watchdog).Hash
# Failure occurs after the archive move, inside the existing install transaction.
[IO.File]::WriteAllText((Join-Path $rollback 'Work'),'blocked Work path')
Invoke-Install $InstallerPath 'install' $rollback 1
Check ((Get-FileHash -LiteralPath $loader).Hash -eq $beforeLoader) 'Loader rollback failed'
Check ((Get-FileHash -LiteralPath $manifest).Hash -eq $beforeManifest) 'Manifest rollback failed'
Check ((Get-FileHash -LiteralPath $watchdog).Hash -eq $beforeWatchdog) 'Support rollback failed'
Check ((Get-FileHash -LiteralPath (Join-Path $rollback ('McnCraft_Data\MPatcherForkBackup\'+$history.FirstName))).Hash -eq $history.FirstHash) 'Archived backup did not roll back'
Check ((Get-Content -LiteralPath (Join-Path $rollback 'Work') -Raw) -eq 'blocked Work path') 'Conflicting Work file changed'
$rollbackLog=Get-Content -LiteralPath (Join-Path $rollback 'logs\MPatcherInstaller.log') -Raw
Check ($rollbackLog.Contains('BACKUP_HISTORY_ARCHIVED') -and $rollbackLog.Contains('BACKUP_HISTORY_ROLLBACK')) 'Archive rollback path was not exercised'
$results.backup_archive_transaction_rollback='passed'
Move-Item -LiteralPath (Join-Path $rollback 'Work') -Destination (Join-Path $rollback 'Work-conflict.before')
Invoke-Install $InstallerPath 'install' $rollback
Check ((Get-FileHash -LiteralPath $loader).Hash -eq $payloadHash) 'Retry payload mismatch'
Check (Test-Path -LiteralPath (Join-Path $rollback 'Work') -PathType Container) 'Retry did not create Work'
Invoke-Install $InstallerPath 'uninstall' $rollback
Check ((Get-FileHash -LiteralPath $loader).Hash -eq $externalHash) 'Retry uninstall did not restore original external loader'
$results.retry_after_rollback='passed'

$report=[ordered]@{CreatedUtc=[DateTime]::UtcNow.ToString('o');Installer=$InstallerPath;FixtureRoot=$fixtureRoot;Count=$results.Count;Cases=$results;GameRuntimeTested=$false;GameCopiesChanged=$false}
$report | ConvertTo-Json -Depth 4 | Set-Content -LiteralPath (Join-Path $EvidenceDirectory 'script-readiness-fixtures.json') -Encoding UTF8
$report | ConvertTo-Json -Depth 4
