[CmdletBinding()]
param(
    [string]$Version = '0.3.8',
    [ValidateSet('Stable','Alpha')][string]$UpdateChannel = 'Stable',
    [string]$PackageDirectory = '',
    [string]$PayloadPath = '',
    [string]$WatchdogPath = ''
)

$ErrorActionPreference = 'Stop'
$root = $PSScriptRoot
if ([string]::IsNullOrWhiteSpace($PackageDirectory)) {
    $PackageDirectory = Join-Path $root ('dist\MPatcher-v' + $Version + $(if($UpdateChannel -eq 'Alpha'){'-Alpha'}else{''}))
}
$bootstrap = Join-Path $packageDirectory 'MPatcherInstaller.exe'
$installer = Join-Path $packageDirectory 'MPatcherPackage.exe'
$updateManifest = Join-Path $packageDirectory 'MPatcherUpdate.ini'
$payload = if ([string]::IsNullOrWhiteSpace($PayloadPath)) { Join-Path $root 'build\__Internal.MPatcherFork.dll' } else { $PayloadPath }
$watchdog = if ([string]::IsNullOrWhiteSpace($WatchdogPath)) { Join-Path $root 'build\watchdog\MPatcherCrashWatchdog.exe' } else { $WatchdogPath }
$officialLoader = Join-Path $root '..\..\reference\mpatcher-installer-v1.6\ghidra-input\PatchLoader-28c3-installed.dll'
$projectConfig = Get-Content -Raw -LiteralPath (Join-Path $root '..\..\project.config.json') | ConvertFrom-Json
$supportedGameExe = Join-Path ([string]$projectConfig.testGameRoot) 'McnCraft.exe'
$testParent = Join-Path $root 'installer-tests'
$testRoot = Join-Path $testParent ('run-' + [DateTime]::UtcNow.ToString('yyyyMMdd-HHmmss-fff'))

foreach ($required in @($bootstrap, $installer, $updateManifest, $payload, $watchdog, $officialLoader, $supportedGameExe)) {
    if (-not (Test-Path -LiteralPath $required)) {
        throw "Required file is missing: $required"
    }
}

$resolvedParent = [IO.Path]::GetFullPath($testParent)
$resolvedRoot = [IO.Path]::GetFullPath($testRoot)
if (-not $resolvedRoot.StartsWith($resolvedParent + [IO.Path]::DirectorySeparatorChar, [StringComparison]::OrdinalIgnoreCase)) {
    throw "Refusing test path outside installer-tests: $resolvedRoot"
}
New-Item -ItemType Directory -Path $resolvedRoot -Force | Out-Null

function New-Fixture([string]$Name) {
    $fixture = Join-Path $resolvedRoot $Name
    New-Item -ItemType Directory -Path (Join-Path $fixture 'McnCraft_Data\Mono') -Force | Out-Null
    New-Item -ItemType Directory -Path (Join-Path $fixture 'UserData') -Force | Out-Null
    Copy-Item -LiteralPath $supportedGameExe -Destination (Join-Path $fixture 'McnCraft.exe')
    [IO.File]::WriteAllBytes((Join-Path $fixture 'McnCraft_Data\Mono\mono.dll'), [byte[]](77, 90))
    [IO.File]::WriteAllText((Join-Path $fixture 'UserData\sentinel.txt'), 'preserve-me', [Text.UTF8Encoding]::new($false))
    return $fixture
}

function Invoke-Installer([string]$Action, [string]$GameRoot, [int]$ExpectedExitCode = 0, [string]$Language = '', [string]$ResultPath = '') {
	$languageArguments = if ([string]::IsNullOrWhiteSpace($Language)) { '' } else { '--lang ' + $Language + ' ' }
	$resultArguments = if ([string]::IsNullOrWhiteSpace($ResultPath)) { '' } else { ' --result-file "' + $ResultPath.Replace('"', '\"') + '"' }
	$argumentLine = $languageArguments + '--' + $Action + ' "' + $GameRoot.Replace('"', '\"') + '"' + $resultArguments
    $process = Start-Process -FilePath $installer -ArgumentList $argumentLine -Wait -PassThru -WindowStyle Hidden
    if ($process.ExitCode -ne $ExpectedExitCode) {
        $log = Join-Path $GameRoot 'logs\MPatcherInstaller.log'
        $tail = if (Test-Path -LiteralPath $log) { (Get-Content -LiteralPath $log -Tail 20) -join [Environment]::NewLine } else { '<no log>' }
        throw "Installer --$Action returned $($process.ExitCode), expected $ExpectedExitCode.`n$tail"
    }
}

function Read-InstallerResult([string]$Path) {
    $values = @{}
    foreach ($line in Get-Content -LiteralPath $Path) {
        $separator = $line.IndexOf('=')
        if ($separator -le 0) { throw "Invalid installer result line: $line" }
        $values[$line.Substring(0, $separator)] = $line.Substring($separator + 1)
    }
    return [pscustomobject]@{
        Success = [bool]::Parse($values.Success)
        Message = [Text.Encoding]::UTF8.GetString([Convert]::FromBase64String($values.MessageBase64))
        Details = [Text.Encoding]::UTF8.GetString([Convert]::FromBase64String($values.DetailsBase64))
        LogPath = [Text.Encoding]::UTF8.GetString([Convert]::FromBase64String($values.LogPathBase64))
        ClosedGameProcesses = [int]$values.ClosedGameProcesses
        ForcedGameProcesses = [int]$values.ForcedGameProcesses
    }
}

function Assert-Equal([object]$Actual, [object]$Expected, [string]$Message) {
    if ($Actual -ne $Expected) {
        throw "$Message. Actual=$Actual Expected=$Expected"
    }
}

function Assert-Common([string]$Fixture) {
    Assert-Equal (Get-Content -LiteralPath (Join-Path $Fixture 'UserData\sentinel.txt') -Raw) 'preserve-me' 'UserData sentinel changed'
}

function Assert-Watchdog([string]$Fixture, [bool]$Expected) {
    $path = Join-Path $Fixture 'McnCraft_Data\MPatcherFork\MPatcherCrashWatchdog.exe'
    Assert-Equal (Test-Path -LiteralPath $path) $Expected 'Crash watchdog state mismatch'
    if ($Expected) {
        Assert-Equal (Get-FileHash -LiteralPath $path -Algorithm SHA256).Hash $watchdogHash 'Crash watchdog payload mismatch'
    }
}

function Wait-ForPath([string]$Path, [int]$TimeoutMilliseconds = 10000) {
    $deadline = [DateTime]::UtcNow.AddMilliseconds($TimeoutMilliseconds)
    while ([DateTime]::UtcNow -lt $deadline) {
        if (Test-Path -LiteralPath $Path) { return }
        Start-Sleep -Milliseconds 100
    }
    throw "Timed out waiting for path: $Path"
}

function Start-SupportedGameRestore([Diagnostics.Process]$Process, [string]$Fixture) {
    $destination = Join-Path $Fixture 'McnCraft.exe'
    return Start-Job -ScriptBlock {
        param($ProcessId, $Source, $Destination)
        Wait-Process -Id $ProcessId -ErrorAction SilentlyContinue
        Copy-Item -LiteralPath $Source -Destination $Destination -Force
    } -ArgumentList $Process.Id, $supportedGameExe, $destination
}

function Complete-SupportedGameRestore($Job, [string]$Fixture) {
    $null = Wait-Job -Job $Job -Timeout 10
    if ($Job.State -ne 'Completed') { throw 'Timed out restoring the supported McnCraft.exe after the close probe' }
    Receive-Job -Job $Job -ErrorAction Stop | Out-Null
    Assert-Equal (Get-FileHash -LiteralPath (Join-Path $Fixture 'McnCraft.exe') -Algorithm SHA256).Hash `
        (Get-FileHash -LiteralPath $supportedGameExe -Algorithm SHA256).Hash `
        'Supported McnCraft.exe was not restored after the close probe'
}

function Remove-TestJob($Job) {
    if ($null -eq $Job) { return }
    if ($Job.State -notin @('Completed', 'Failed', 'Stopped')) { Stop-Job -Job $Job -ErrorAction SilentlyContinue }
    Remove-Job -Job $Job -Force -ErrorAction SilentlyContinue
}

$payloadHash = (Get-FileHash -LiteralPath $payload -Algorithm SHA256).Hash
$watchdogHash = (Get-FileHash -LiteralPath $watchdog -Algorithm SHA256).Hash
$officialHash = (Get-FileHash -LiteralPath $officialLoader -Algorithm SHA256).Hash
$installerHash = (Get-FileHash -LiteralPath $installer -Algorithm SHA256).Hash
$installerLength = (Get-Item -LiteralPath $installer).Length
$bootstrapHash = (Get-FileHash -LiteralPath $bootstrap -Algorithm SHA256).Hash
$releaseTag = if($UpdateChannel -eq 'Alpha'){'alpha'}else{'v'+$Version}
$updateManifestText = Get-Content -LiteralPath $updateManifest -Raw
Assert-Equal ($updateManifestText -match ('(?m)^Version=' + [regex]::Escape($Version) + '\r?$')) $true 'Update manifest version mismatch'
Assert-Equal ($updateManifestText -match ('(?m)^InstallerSha256=' + $installerHash + '\r?$')) $true 'Update manifest installer hash mismatch'
Assert-Equal ($updateManifestText -match ('(?m)^InstallerLength=' + $installerLength + '\r?$')) $true 'Update manifest installer length mismatch'
Assert-Equal ($updateManifestText -match ('(?m)^InstallerUrl=https://github\.com/RestartLive113/MPatcher/releases/download/' + [regex]::Escape($releaseTag) + '/MPatcherPackage\.exe\r?$')) $true 'Update manifest installer URL mismatch'
$results = [ordered]@{}

$clean = New-Fixture 'clean'
[IO.File]::WriteAllText((Join-Path $clean 'McnCraft_Data\MPatcherFork.log'), 'legacy-log', [Text.UTF8Encoding]::new($false))
$cleanInstallResultPath = Join-Path $resolvedRoot 'clean-install-result.ini'
Invoke-Installer 'install' $clean 0 'ru' $cleanInstallResultPath
$cleanInstallResult = Read-InstallerResult $cleanInstallResultPath
Assert-Equal $cleanInstallResult.Success $true 'Clean install result did not report success'
Assert-Equal $cleanInstallResult.Message.Contains('успешно установлен') $true 'Clean install result lacks a clear Russian success message'
Assert-Equal $cleanInstallResult.Message.Contains($clean) $true 'Clean install result lacks the game path'
Assert-Equal (Get-FileHash -LiteralPath (Join-Path $clean 'McnCraft_Data\Mono\__Internal.dll') -Algorithm SHA256).Hash $payloadHash 'Clean install payload mismatch'
Assert-Equal (Test-Path -LiteralPath (Join-Path $clean 'McnCraft_Data\MPatcherFork.install.ini')) $true 'Clean install manifest missing'
Assert-Watchdog $clean $true
Assert-Equal @(Get-ChildItem -LiteralPath (Join-Path $clean 'logs') -File -Filter 'Legacy_*_MPatcherFork.log').Count 1 'Legacy patch log was not migrated'
Assert-Equal (Test-Path -LiteralPath (Join-Path $clean 'logs\MPatcherInstaller.log')) $true 'Installer log missing from logs folder'
Assert-Common $clean
$cleanUninstallResultPath = Join-Path $resolvedRoot 'clean-uninstall-result.ini'
Invoke-Installer 'uninstall' $clean 0 'ru' $cleanUninstallResultPath
$cleanUninstallResult = Read-InstallerResult $cleanUninstallResultPath
Assert-Equal $cleanUninstallResult.Success $true 'Clean uninstall result did not report success'
Assert-Equal $cleanUninstallResult.Message.Contains('успешно удалён') $true 'Clean uninstall result lacks a clear Russian success message'
Assert-Equal (Test-Path -LiteralPath (Join-Path $clean 'McnCraft_Data\Mono\__Internal.dll')) $false 'Clean uninstall left loader'
Assert-Watchdog $clean $false
Assert-Common $clean
$results.clean = 'passed'
$results.clear_result_messages = 'passed'

$invalidResultPath = Join-Path $resolvedRoot 'invalid-install-result.ini'
$invalidRoot = Join-Path $resolvedRoot 'invalid-game-root'
New-Item -ItemType Directory -Path $invalidRoot -Force | Out-Null
Invoke-Installer 'install' $invalidRoot 1 'ru' $invalidResultPath
$invalidResult = Read-InstallerResult $invalidResultPath
Assert-Equal $invalidResult.Success $false 'Invalid install result unexpectedly reported success'
Assert-Equal $invalidResult.Message.Contains('Не удалось установить MPatcher') $true 'Invalid install result lacks a clear Russian failure message'
Assert-Equal ([string]::IsNullOrWhiteSpace($invalidResult.Details)) $false 'Invalid install result lacks failure details'
$results.clear_failure_message = 'passed'

$existing = New-Fixture 'existing-mpatcher'
Copy-Item -LiteralPath $officialLoader -Destination (Join-Path $existing 'McnCraft_Data\Mono\__Internal.dll')
Invoke-Installer 'install' $existing 0 'en'
Assert-Equal (Get-FileHash -LiteralPath (Join-Path $existing 'McnCraft_Data\Mono\__Internal.dll') -Algorithm SHA256).Hash $payloadHash 'Existing MPatcher update payload mismatch'
Assert-Watchdog $existing $true
$backup = Get-ChildItem -LiteralPath (Join-Path $existing 'McnCraft_Data\MPatcherForkBackup') -File -Filter '*.dll'
Assert-Equal $backup.Count 1 'Existing MPatcher backup count mismatch'
Assert-Equal (Get-FileHash -LiteralPath $backup[0].FullName -Algorithm SHA256).Hash $officialHash 'Existing MPatcher backup mismatch'
Assert-Common $existing
Invoke-Installer 'uninstall' $existing 0 'en'
Assert-Equal (Get-FileHash -LiteralPath (Join-Path $existing 'McnCraft_Data\Mono\__Internal.dll') -Algorithm SHA256).Hash $officialHash 'Existing MPatcher was not restored'
Assert-Watchdog $existing $false
Assert-Common $existing
$results.existing_mpatcher = 'passed'

$repeat = New-Fixture 'repeat-update'
Invoke-Installer 'install' $repeat
Invoke-Installer 'install' $repeat
Assert-Equal (Get-FileHash -LiteralPath (Join-Path $repeat 'McnCraft_Data\Mono\__Internal.dll') -Algorithm SHA256).Hash $payloadHash 'Repeat update payload mismatch'
Assert-Watchdog $repeat $true
Assert-Equal @(Get-ChildItem -LiteralPath (Join-Path $repeat 'McnCraft_Data\MPatcherForkBackup') -File -ErrorAction SilentlyContinue).Count 0 'Repeat clean update created an original backup'
Invoke-Installer 'uninstall' $repeat
Assert-Equal (Test-Path -LiteralPath (Join-Path $repeat 'McnCraft_Data\Mono\__Internal.dll')) $false 'Repeat update uninstall left loader'
Assert-Watchdog $repeat $false
Assert-Common $repeat
$results.repeat_update = 'passed'

$supportRecovery = New-Fixture 'support-folder-self-heal'
Copy-Item -LiteralPath $officialLoader -Destination (Join-Path $supportRecovery 'McnCraft_Data\Mono\__Internal.dll')
Invoke-Installer 'install' $supportRecovery 0 'en'
$supportBackupDirectory = Join-Path $supportRecovery 'McnCraft_Data\MPatcherForkBackup'
$supportBackupBefore = @(Get-ChildItem -LiteralPath $supportBackupDirectory -File -Filter '*.dll')
Assert-Equal $supportBackupBefore.Count 1 'Support recovery fixture backup count mismatch before corruption'
$supportBackupName = $supportBackupBefore[0].Name
$supportBackupHash = (Get-FileHash -LiteralPath $supportBackupBefore[0].FullName -Algorithm SHA256).Hash
$supportDirectory = Join-Path $supportRecovery 'McnCraft_Data\MPatcherFork'
$supportWatchdog = Join-Path $supportDirectory 'MPatcherCrashWatchdog.exe'
Remove-Item -LiteralPath $supportWatchdog -Force
New-Item -ItemType Directory -Path $supportWatchdog | Out-Null
[IO.File]::WriteAllText((Join-Path $supportWatchdog 'corrupt-file.txt'), 'stale-support-data', [Text.UTF8Encoding]::new($false))
[IO.File]::WriteAllText((Join-Path $supportDirectory 'unexpected-stale-file.bin'), 'stale-support-data', [Text.UTF8Encoding]::new($false))
$supportRecoveryResultPath = Join-Path $resolvedRoot 'support-recovery-result.ini'
Invoke-Installer 'install' $supportRecovery 0 'ru' $supportRecoveryResultPath
$supportRecoveryResult = Read-InstallerResult $supportRecoveryResultPath
Assert-Equal $supportRecoveryResult.Success $true 'Support folder recovery did not report success'
Assert-Watchdog $supportRecovery $true
Assert-Equal (Test-Path -LiteralPath (Join-Path $supportDirectory 'unexpected-stale-file.bin')) $false 'Support folder recovery retained stale data'
Assert-Equal (Test-Path -LiteralPath (Join-Path $supportRecovery 'McnCraft_Data\MPatcherFork.install-previous')) $false 'Support folder recovery left a directory snapshot'
Assert-Equal (Test-Path -LiteralPath (Join-Path $supportRecovery 'McnCraft_Data\MPatcherFork.install-previous.file')) $false 'Support folder recovery left a file snapshot'
$supportBackupAfter = @(Get-ChildItem -LiteralPath $supportBackupDirectory -File -Filter '*.dll')
Assert-Equal $supportBackupAfter.Count 1 'Support folder recovery changed backup count'
Assert-Equal $supportBackupAfter[0].Name $supportBackupName 'Support folder recovery changed backup name'
Assert-Equal (Get-FileHash -LiteralPath $supportBackupAfter[0].FullName -Algorithm SHA256).Hash $supportBackupHash 'Support folder recovery changed backup contents'
$supportRecoveryLog = Get-Content -LiteralPath (Join-Path $supportRecovery 'logs\MPatcherInstaller.log') -Raw
foreach ($marker in @('SUPPORT_RESET_BEGIN', 'SUPPORT_RESET_READY previous=directory', 'SUPPORT_RESET_COMMIT')) {
    Assert-Equal $supportRecoveryLog.Contains($marker) $true "Support recovery log marker missing: $marker"
}
Assert-Common $supportRecovery
$supportRollbackSentinel = Join-Path $supportDirectory 'rollback-sentinel.txt'
[IO.File]::WriteAllText($supportRollbackSentinel, 'restore-this-support-folder', [Text.UTF8Encoding]::new($false))
$blockedManifestRollback = Join-Path $supportRecovery 'McnCraft_Data\MPatcherFork.install.ini.rollback'
New-Item -ItemType Directory -Path $blockedManifestRollback | Out-Null
$supportRollbackResultPath = Join-Path $resolvedRoot 'support-rollback-result.ini'
Invoke-Installer 'install' $supportRecovery 1 'en' $supportRollbackResultPath
$supportRollbackResult = Read-InstallerResult $supportRollbackResultPath
Assert-Equal $supportRollbackResult.Success $false 'Forced support rollback unexpectedly reported success'
Assert-Equal ([string]::IsNullOrWhiteSpace($supportRollbackResult.Details)) $false 'Forced support rollback lacks failure details'
Assert-Equal (Get-Content -LiteralPath $supportRollbackSentinel -Raw) 'restore-this-support-folder' 'Failed install did not restore the prior support folder'
Assert-Watchdog $supportRecovery $true
$supportBackupAfterRollback = @(Get-ChildItem -LiteralPath $supportBackupDirectory -File -Filter '*.dll')
Assert-Equal $supportBackupAfterRollback.Count 1 'Failed support reset changed backup count'
Assert-Equal $supportBackupAfterRollback[0].Name $supportBackupName 'Failed support reset changed backup name'
Assert-Equal (Get-FileHash -LiteralPath $supportBackupAfterRollback[0].FullName -Algorithm SHA256).Hash $supportBackupHash 'Failed support reset changed backup contents'
$supportRollbackLog = Get-Content -LiteralPath (Join-Path $supportRecovery 'logs\MPatcherInstaller.log') -Raw
Assert-Equal $supportRollbackLog.Contains('SUPPORT_RESET_ROLLBACK') $true 'Support reset rollback log marker missing'
Remove-Item -LiteralPath $blockedManifestRollback
Invoke-Installer 'install' $supportRecovery 0 'en'
Assert-Equal (Test-Path -LiteralPath $supportRollbackSentinel) $false 'Successful retry retained obsolete support data'
Assert-Watchdog $supportRecovery $true
Assert-Common $supportRecovery
Invoke-Installer 'uninstall' $supportRecovery 0 'en'
Assert-Equal (Get-FileHash -LiteralPath (Join-Path $supportRecovery 'McnCraft_Data\Mono\__Internal.dll') -Algorithm SHA256).Hash $officialHash 'Support recovery uninstall did not restore the original loader'
Assert-Common $supportRecovery
$results.support_folder_self_heal = 'passed'
$results.support_folder_transactional_rollback = 'passed'

$externalAfterManaged = New-Fixture 'external-after-managed'
Invoke-Installer 'install' $externalAfterManaged 0 'ja'
$externalAfterManagedLoader = Join-Path $externalAfterManaged 'McnCraft_Data\Mono\__Internal.dll'
Copy-Item -LiteralPath $officialLoader -Destination $externalAfterManagedLoader -Force
Invoke-Installer 'install' $externalAfterManaged
Assert-Equal (Get-FileHash -LiteralPath $externalAfterManagedLoader -Algorithm SHA256).Hash $payloadHash 'External-after-managed update payload mismatch'
Assert-Watchdog $externalAfterManaged $true
Invoke-Installer 'uninstall' $externalAfterManaged
Assert-Equal (Get-FileHash -LiteralPath $externalAfterManagedLoader -Algorithm SHA256).Hash $officialHash 'External loader installed after this MPatcher was not restored'
Assert-Watchdog $externalAfterManaged $false
Assert-Common $externalAfterManaged
$results.external_after_managed = 'passed'

$unknown = New-Fixture 'unknown-loader'
$unknownLoader = Join-Path $unknown 'McnCraft_Data\Mono\__Internal.dll'
[IO.File]::WriteAllBytes($unknownLoader, [byte[]](1, 3, 3, 7, 9, 11, 17, 23))
$unknownHash = (Get-FileHash -LiteralPath $unknownLoader -Algorithm SHA256).Hash
Invoke-Installer 'install' $unknown
Assert-Equal (Get-FileHash -LiteralPath $unknownLoader -Algorithm SHA256).Hash $payloadHash 'Unknown loader update payload mismatch'
Assert-Watchdog $unknown $true
Invoke-Installer 'uninstall' $unknown
Assert-Equal (Get-FileHash -LiteralPath $unknownLoader -Algorithm SHA256).Hash $unknownHash 'Unknown loader was not restored'
Assert-Watchdog $unknown $false
Assert-Common $unknown
$results.unknown_loader = 'passed'

$probeDirectory = Join-Path $resolvedRoot 'restart-probe-build'
New-Item -ItemType Directory -Path $probeDirectory -Force | Out-Null
$probeSource = Join-Path $probeDirectory 'RestartProbe.cs'
$probeExe = Join-Path $probeDirectory 'McnCraft.exe'
$probeCode = @'
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

internal static class RestartProbe
{
	[STAThread]
    private static int Main(string[] args)
    {
        if (args.Length > 0 && string.Equals(args[0], "--hold", StringComparison.Ordinal))
        {
            File.WriteAllText(Path.Combine(Environment.CurrentDirectory, "hold-ready.txt"), Process.GetCurrentProcess().Id.ToString());
            Thread.Sleep(3000);
            return 0;
        }
		if (args.Length > 0 && string.Equals(args[0], "--hold-long", StringComparison.Ordinal))
		{
			File.WriteAllText(Path.Combine(Environment.CurrentDirectory, "force-ready.txt"), Process.GetCurrentProcess().Id.ToString());
			Thread.Sleep(30000);
			return 0;
		}
		if (args.Length > 0 && string.Equals(args[0], "--hold-window", StringComparison.Ordinal))
		{
			Application.EnableVisualStyles();
			Form form = new Form();
			form.Text = "MachineCraft installer close test";
			form.ShowInTaskbar = true;
			form.FormBorderStyle = FormBorderStyle.FixedDialog;
			form.Width = 100;
			form.Height = 80;
			form.WindowState = FormWindowState.Minimized;
			form.Shown += delegate
			{
				File.WriteAllText(Path.Combine(Environment.CurrentDirectory, "window-ready.txt"), Process.GetCurrentProcess().Id.ToString());
			};
			Application.Run(form);
			return 0;
		}
        File.AppendAllText(Path.Combine(Environment.CurrentDirectory, "restart-probe.txt"), DateTime.UtcNow.ToString("o") + Environment.NewLine);
        return 0;
    }
}
'@
[IO.File]::WriteAllText($probeSource, $probeCode, [Text.UTF8Encoding]::new($false))
$csc = Join-Path $env:WINDIR 'Microsoft.NET\Framework\v4.0.30319\csc.exe'
& $csc /nologo /target:winexe /optimize+ /reference:System.Windows.Forms.dll ('/out:' + $probeExe) $probeSource
if ($LASTEXITCODE -ne 0) { throw "Restart probe compiler failed with exit code $LASTEXITCODE" }

$graceful = New-Fixture 'running-game-graceful-close'
Copy-Item -LiteralPath $probeExe -Destination (Join-Path $graceful 'McnCraft.exe') -Force
$gracefulProcess = Start-Process -FilePath (Join-Path $graceful 'McnCraft.exe') -ArgumentList '--hold-window' -WorkingDirectory $graceful -PassThru -WindowStyle Hidden
$gracefulRestoreJob = $null
try {
    Wait-ForPath (Join-Path $graceful 'window-ready.txt')
    $gracefulRestoreJob = Start-SupportedGameRestore $gracefulProcess $graceful
    $gracefulResultPath = Join-Path $resolvedRoot 'graceful-close-result.ini'
    Invoke-Installer 'install' $graceful 0 'en' $gracefulResultPath
    Complete-SupportedGameRestore $gracefulRestoreJob $graceful
    $gracefulProcess.Refresh()
    Assert-Equal $gracefulProcess.HasExited $true 'Installer did not close the selected running game gracefully'
    $gracefulResult = Read-InstallerResult $gracefulResultPath
    Assert-Equal $gracefulResult.ClosedGameProcesses 1 'Graceful close count mismatch'
    Assert-Equal $gracefulResult.ForcedGameProcesses 0 'Graceful close unexpectedly used force'
	Assert-Equal $gracefulResult.Message.Contains('closed automatically') $true 'Graceful close success message is unclear'
    $gracefulLog = Get-Content -LiteralPath (Join-Path $graceful 'logs\MPatcherInstaller.log') -Raw
    foreach ($marker in @('GAME_CLOSE_REQUESTED', 'GAME_CLOSE_OK', 'forced=False')) {
        Assert-Equal $gracefulLog.Contains($marker) $true "Graceful close log marker missing: $marker"
    }
    Assert-Common $graceful
} finally {
    if (-not $gracefulProcess.HasExited) { Stop-Process -Id $gracefulProcess.Id -Force }
    $gracefulProcess.Dispose()
    Remove-TestJob $gracefulRestoreJob
}
$results.running_game_graceful_close = 'passed'

$forced = New-Fixture 'running-game-forced-close'
Copy-Item -LiteralPath $probeExe -Destination (Join-Path $forced 'McnCraft.exe') -Force
$forcedProcess = Start-Process -FilePath (Join-Path $forced 'McnCraft.exe') -ArgumentList '--hold-long' -WorkingDirectory $forced -PassThru -WindowStyle Hidden
$forcedRestoreJob = $null
try {
    Wait-ForPath (Join-Path $forced 'force-ready.txt')
    $forcedRestoreJob = Start-SupportedGameRestore $forcedProcess $forced
    $forcedResultPath = Join-Path $resolvedRoot 'forced-close-result.ini'
    Invoke-Installer 'install' $forced 0 'ja' $forcedResultPath
    Complete-SupportedGameRestore $forcedRestoreJob $forced
    $forcedProcess.Refresh()
    Assert-Equal $forcedProcess.HasExited $true 'Installer did not force-close the selected running game'
    $forcedResult = Read-InstallerResult $forcedResultPath
    Assert-Equal $forcedResult.ClosedGameProcesses 1 'Forced close count mismatch'
    Assert-Equal $forcedResult.ForcedGameProcesses 1 'Forced close was not reported'
	Assert-Equal $forcedResult.Message.Contains('強制終了') $true 'Forced close Japanese success message is unclear'
    $forcedLog = Get-Content -LiteralPath (Join-Path $forced 'logs\MPatcherInstaller.log') -Raw
    foreach ($marker in @('GAME_CLOSE_FORCE_BEGIN', 'GAME_CLOSE_OK', 'forced=True')) {
        Assert-Equal $forcedLog.Contains($marker) $true "Forced close log marker missing: $marker"
    }
    Assert-Common $forced
} finally {
    if (-not $forcedProcess.HasExited) { Stop-Process -Id $forcedProcess.Id -Force }
    $forcedProcess.Dispose()
    Remove-TestJob $forcedRestoreJob
}
$results.running_game_forced_close = 'passed'

$otherCopy = New-Fixture 'other-copy-remains-running'
Copy-Item -LiteralPath $probeExe -Destination (Join-Path $otherCopy 'McnCraft.exe') -Force
$selectedCopy = New-Fixture 'selected-copy-install'
$otherProcess = Start-Process -FilePath (Join-Path $otherCopy 'McnCraft.exe') -ArgumentList '--hold-long' -WorkingDirectory $otherCopy -PassThru -WindowStyle Hidden
try {
    Wait-ForPath (Join-Path $otherCopy 'force-ready.txt')
    Invoke-Installer 'install' $selectedCopy 0 'en'
    $otherProcess.Refresh()
    Assert-Equal $otherProcess.HasExited $false 'Installer closed a different MachineCraft copy'
    Assert-Common $selectedCopy
} finally {
    if (-not $otherProcess.HasExited) { Stop-Process -Id $otherProcess.Id -Force }
    $otherProcess.Dispose()
}
$results.other_game_copy_preserved = 'passed'

$automatic = New-Fixture 'automatic-update'
Copy-Item -LiteralPath $probeExe -Destination (Join-Path $automatic 'McnCraft.exe') -Force
$holdProcess = Start-Process -FilePath (Join-Path $automatic 'McnCraft.exe') -ArgumentList '--hold' -WorkingDirectory $automatic -PassThru -WindowStyle Hidden
$automaticRestoreJob = $null
try {
    Wait-ForPath (Join-Path $automatic 'hold-ready.txt')
    $automaticRestoreJob = Start-SupportedGameRestore $holdProcess $automatic
    $automaticArguments = '--auto-update "' + $automatic.Replace('"', '\"') + '" --wait-pid ' + $holdProcess.Id + ' --lang en'
    $automaticStopwatch = [Diagnostics.Stopwatch]::StartNew()
    $automaticInstaller = Start-Process -FilePath $installer -ArgumentList $automaticArguments -Wait -PassThru -WindowStyle Hidden
    $automaticStopwatch.Stop()
    Assert-Equal $automaticInstaller.ExitCode 0 'Automatic update exit code mismatch'
    Complete-SupportedGameRestore $automaticRestoreJob $automatic
    if ($automaticStopwatch.ElapsedMilliseconds -lt 1000) { throw 'Automatic update did not wait for the running game process' }
} finally {
    if (-not $holdProcess.HasExited) { Stop-Process -Id $holdProcess.Id -Force }
    $holdProcess.Dispose()
    Remove-TestJob $automaticRestoreJob
}
Assert-Equal (Get-FileHash -LiteralPath (Join-Path $automatic 'McnCraft_Data\Mono\__Internal.dll') -Algorithm SHA256).Hash $payloadHash 'Automatic update payload mismatch'
Assert-Watchdog $automatic $true
$automaticResult = Get-Content -LiteralPath (Join-Path $automatic 'McnCraft_Data\MPatcherFork\MPatcherUpdate.result.ini') -Raw
Assert-Equal ($automaticResult -match '(?m)^Success=True\r?$') $true 'Automatic update result was not successful'
Assert-Equal ($automaticResult -match ('(?m)^Version=' + [regex]::Escape($Version) + '\r?$')) $true 'Automatic update result version mismatch'
$automaticLog = Get-Content -LiteralPath (Join-Path $automatic 'logs\MPatcherInstaller.log') -Raw
foreach ($marker in @('AUTO_UPDATE_WAIT_OK', 'AUTO_UPDATE_OK')) {
    Assert-Equal ($automaticLog.Contains($marker)) $true "Automatic update log marker missing: $marker"
}
Assert-Common $automatic
$results.automatic_update = 'passed'

[ordered]@{
    tested_utc = [DateTime]::UtcNow.ToString('o')
    installer = $installer
    installer_sha256 = $installerHash
    universal_installer = $bootstrap
    universal_installer_sha256 = $bootstrapHash
    update_manifest_sha256 = (Get-FileHash -LiteralPath $updateManifest -Algorithm SHA256).Hash
    payload_sha256 = $payloadHash
	watchdog_sha256 = $watchdogHash
    fixture_root = $resolvedRoot
	locales_cli = @('ru', 'en', 'ja')
    results = $results
    game_runtime_tested = $false
} | ConvertTo-Json -Depth 4
