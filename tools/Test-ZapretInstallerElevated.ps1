[CmdletBinding()]
param([Parameter(Mandatory=$true)][string]$ReportFile,[Parameter(Mandatory=$true)][string]$PackageReport,[Parameter(Mandatory=$true)][string]$TestGame,[string]$CloneGame,[switch]$FixtureOnly)
$ErrorActionPreference='Stop'
Set-StrictMode -Version Latest
$result=[ordered]@{State='STARTED';Checks=@();Error=$null;InstalledGames=@()}
function Save-State { $result | ConvertTo-Json -Depth 8 | Set-Content -LiteralPath $ReportFile -Encoding UTF8 }
function Check([bool]$ok,[string]$message) { if(-not $ok) {throw $message}; $result.Checks+= $message; Save-State }
function Hash-File([string]$file) { $sha=[Security.Cryptography.SHA256]::Create(); $stream=[IO.File]::OpenRead($file); try { [BitConverter]::ToString($sha.ComputeHash($stream)).Replace('-','') } finally {$stream.Dispose();$sha.Dispose()} }
function UserData([string]$game) { $list=@{}; Get-ChildItem -LiteralPath (Join-Path $game 'UserData') -File -Recurse | ForEach-Object {$list[$_.FullName.Substring($game.Length)]=Hash-File $_.FullName}; $list }
function SameData($a,$b) { if($a.Count -ne $b.Count){return $false}; foreach($k in $a.Keys){if($a[$k] -ne $b[$k]){return $false}}; return $true }
function Run-Package([string]$action,[string]$game,[int]$expected=0) {
    $p=Start-Process -FilePath $package.ResolvedInstaller -ArgumentList ('--'+$action+' "'+$game+'"') -WindowStyle Hidden -PassThru -Wait
    Check ($p.ExitCode -eq $expected) ($action+' '+$game+' exit='+$p.ExitCode)
}
try {
    Save-State
    $package=Get-Content -LiteralPath $PackageReport -Raw | ConvertFrom-Json
    $installerProperty=if($package.PSObject.Properties['Installer']){'Installer'}else{'package_installer'}
    $installerHashProperty=if($package.PSObject.Properties['InstallerSha256']){'InstallerSha256'}else{'package_installer_sha256'}
    $loaderHashProperty=if($package.PSObject.Properties['LoaderSha256']){'LoaderSha256'}else{'payload_sha256'}
    $package | Add-Member -NotePropertyName ResolvedInstaller -NotePropertyValue $package.$installerProperty
    $package | Add-Member -NotePropertyName ResolvedInstallerSha256 -NotePropertyValue $package.$installerHashProperty
    $package | Add-Member -NotePropertyName ResolvedLoaderSha256 -NotePropertyValue $package.$loaderHashProperty
    Check ((Hash-File $package.ResolvedInstaller) -eq $package.ResolvedInstallerSha256) 'Installer hash matches package report'
    Check (@(Get-Process -Name McnCraft,winws,winws2 -ErrorAction SilentlyContinue).Count -eq 0) 'No running games or external Zapret'
    Check ($null -eq (Get-Service -Name MPatcherLegacyZapret -ErrorAction SilentlyContinue)) 'No previous helper service'
    $repo=Split-Path $PSScriptRoot -Parent
    $fixtures=Join-Path $repo ('installer-tests\zapret-'+(Get-Date -Format 'yyyyMMdd-HHmmss'))
    if(-not [IO.Path]::GetFullPath($fixtures).StartsWith([IO.Path]::GetFullPath((Join-Path $repo 'installer-tests'))+'\',[StringComparison]::OrdinalIgnoreCase)){throw 'Fixture root outside workspace'}
    $a=Join-Path $fixtures 'clean'; $b=Join-Path $fixtures 'existing'
    foreach($game in @($a,$b)) {
        New-Item -ItemType Directory -Path (Join-Path $game 'McnCraft_Data\Mono'),(Join-Path $game 'UserData') -Force | Out-Null
        Copy-Item -LiteralPath (Join-Path $TestGame 'McnCraft.exe') -Destination (Join-Path $game 'McnCraft.exe')
        [IO.File]::WriteAllBytes((Join-Path $game 'McnCraft_Data\Mono\mono.dll'),[byte[]](77,90))
        [IO.File]::WriteAllText((Join-Path $game 'UserData\sentinel.txt'),'preserve machine data')
    }
    $prior=Join-Path $TestGame 'McnCraft_Data\Mono\__Internal.dll'
    $priorHash=Hash-File $prior
    Copy-Item -LiteralPath $prior -Destination (Join-Path $b 'McnCraft_Data\Mono\__Internal.dll')
    Run-Package install $a
    Run-Package install $b
    Run-Package install $b
    Check ((Get-Service MPatcherLegacyZapret).Status -eq 'Running') 'Idle service running'
    Check (@(Get-Process winws -ErrorAction SilentlyContinue).Count -eq 0) 'Disabled default starts no packet filter'
    $key=Get-Item 'HKLM:\SOFTWARE\MPatcher\LegacyZapret\Games'
    Check ($key.GetValueNames().Count -eq 2) 'Two registrations; reinstall is idempotent'
    foreach($game in @($a,$b)) { Check ((Hash-File (Join-Path $game 'McnCraft_Data\Mono\__Internal.dll')) -eq $package.ResolvedLoaderSha256) 'Installed candidate loader matches' }
    Run-Package uninstall $a
    Check ($null -ne (Get-Service MPatcherLegacyZapret -ErrorAction SilentlyContinue)) 'First uninstall preserves second registration service'
    Check (-not(Test-Path -LiteralPath (Join-Path $a 'McnCraft_Data\Mono\__Internal.dll'))) 'Clean fixture restored'
    Run-Package uninstall $b
    Check ((Hash-File (Join-Path $b 'McnCraft_Data\Mono\__Internal.dll')) -eq $priorHash) 'Existing loader restored exactly'
    Check ($null -eq (Get-Service MPatcherLegacyZapret -ErrorAction SilentlyContinue)) 'Last uninstall removes service'
    foreach($game in @($a,$b)) {Check (([IO.File]::ReadAllText((Join-Path $game 'UserData\sentinel.txt'))) -eq 'preserve machine data') 'Fixture UserData preserved'}
    if(-not $FixtureOnly) {
        if([string]::IsNullOrWhiteSpace($CloneGame)){throw 'CloneGame is required unless FixtureOnly is set'}
        foreach($game in @($TestGame,$CloneGame)) {
            $before=UserData $game
            Run-Package install $game
            Check (SameData $before (UserData $game)) ('UserData hashes preserved: '+$game)
            Check ((Hash-File (Join-Path $game 'McnCraft_Data\Mono\__Internal.dll')) -eq $package.ResolvedLoaderSha256) ('Deployed loader verified: '+$game)
            $result.InstalledGames+=$game
        }
    }
    $result.State='PASS'
} catch { $result.State='FAIL';$result.Error=$_.Exception.ToString() }
finally { Save-State }
