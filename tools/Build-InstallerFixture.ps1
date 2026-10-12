[CmdletBinding()]
param(
    [Parameter(Mandatory=$true)][string]$PackageDirectory,
    [Parameter(Mandatory=$true)][string]$InstallerBuildDirectory,
    [Parameter(Mandatory=$true)][string]$FixtureDirectory,
    [Parameter(Mandatory=$true)][string]$PayloadPath,
    [Parameter(Mandatory=$true)][string]$WatchdogPath,
    [string]$ScaleModuleDirectory
)
# Standard CLI and bootstrap fixture of the final sources/resources. Intentionally
# asInvoker and without MPATCHER_ZAPRET_PACKAGE: this does not alter global services.
# Production UAC/resource/ZIP verification remains a separate release gate.
$ErrorActionPreference='Stop'
Set-StrictMode -Version Latest
$repo=Split-Path $PSScriptRoot -Parent
$source=Join-Path $repo 'Installer'
$PackageDirectory=(Resolve-Path -LiteralPath $PackageDirectory).Path
$InstallerBuildDirectory=(Resolve-Path -LiteralPath $InstallerBuildDirectory).Path
$FixtureDirectory=[IO.Path]::GetFullPath($FixtureDirectory)
$allowed=[IO.Path]::GetFullPath((Join-Path $repo 'installer-tests'))+[IO.Path]::DirectorySeparatorChar
if(-not $FixtureDirectory.StartsWith($allowed,[StringComparison]::OrdinalIgnoreCase)){throw 'Installer fixture output must stay under installer-tests.'}
if(Test-Path -LiteralPath $FixtureDirectory){throw 'Preserving existing installer fixture output.'}
New-Item -ItemType Directory -Path $FixtureDirectory -Force | Out-Null
$csc=Join-Path $env:WINDIR 'Microsoft.NET\Framework\v4.0.30319\csc.exe'
$csc64=Join-Path $env:WINDIR 'Microsoft.NET\Framework64\v4.0.30319\csc.exe'
$productionPackage=Join-Path $PackageDirectory 'MPatcherPackage.exe'
$productionAssembly=[Reflection.Assembly]::LoadFile($productionPackage)
$resources=@{ 'MachineCraftMPatcherInstaller.Payload.__Internal.dll'=(Resolve-Path -LiteralPath $PayloadPath).Path; 'MachineCraftMPatcherInstaller.Payload.MPatcherCrashWatchdog.exe'=(Resolve-Path -LiteralPath $WatchdogPath).Path }
if($ScaleModuleDirectory){
    $resources['MachineCraftMPatcherInstaller.Payload.MPatcher.ScaleRuntime.dll']=(Resolve-Path -LiteralPath (Join-Path $ScaleModuleDirectory 'MPatcher.ScaleRuntime.dll')).Path
    $resources['MachineCraftMPatcherInstaller.Payload.MPatcher.ScaleRuntime.manifest.ini']=(Resolve-Path -LiteralPath (Join-Path $ScaleModuleDirectory 'MPatcher.ScaleRuntime.manifest.ini')).Path
}
$resourceHashes=[ordered]@{}
foreach($name in $resources.Keys){
    $stream=$productionAssembly.GetManifestResourceStream($name)
    if($null -eq $stream){throw ('Production package resource missing: '+$name)}
    $sha=[Security.Cryptography.SHA256]::Create()
    try{$resourceHash=[BitConverter]::ToString($sha.ComputeHash($stream)).Replace('-','')}finally{$sha.Dispose();$stream.Dispose()}
    if($resourceHash -ne (Get-FileHash -LiteralPath $resources[$name]).Hash){throw ('Fixture input differs from production resource: '+$name)}
    $resourceHashes[$name]=$resourceHash
}
$productionHasModule=$productionAssembly.GetManifestResourceNames() -contains 'MachineCraftMPatcherInstaller.Payload.MPatcher.ScaleRuntime.dll'
if($productionHasModule -ne [bool]$ScaleModuleDirectory){throw 'Fixture module inclusion differs from production package.'}
$fixturePackage=Join-Path $FixtureDirectory 'MPatcherPackage.exe'
$fixtureBootstrap=Join-Path $FixtureDirectory 'MPatcherInstaller.exe'
$manifest=Join-Path $source 'Installer.manifest'
if(-not ([IO.File]::ReadAllText($manifest)).Contains('level="asInvoker"')){throw 'Fixture manifest must use asInvoker.'}
$arguments=@('/nologo','/target:winexe','/platform:x64','/optimize+','/debug-','/warn:4','/codepage:65001',('/out:'+$fixturePackage),('/win32manifest:'+$manifest),'/reference:System.dll','/reference:System.Core.dll','/reference:System.Drawing.dll','/reference:System.Windows.Forms.dll')
foreach($name in @('Program.cs','MainForm.cs','Localization.cs','InstallerEngine.cs','AutoUpdateRunner.cs','GameProcessController.cs','InstallerResultProtocol.cs')){$arguments+=(Join-Path $source $name)}
foreach($name in @('PayloadInfo.Generated.cs','AssemblyInfo.Generated.cs')){$arguments+=(Join-Path $InstallerBuildDirectory $name)}
foreach($name in $resources.Keys){$arguments+=('/resource:'+$resources[$name]+','+$name)}
& $csc64 @arguments 2>&1 | Out-File -LiteralPath (Join-Path $FixtureDirectory 'package-compile.log') -Encoding utf8
if($LASTEXITCODE -ne 0){throw ('Fixture package compilation failed: '+[IO.File]::ReadAllText((Join-Path $FixtureDirectory 'package-compile.log')))}
$arguments=@('/nologo','/target:winexe','/platform:anycpu','/optimize+','/debug-','/warn:4','/codepage:65001',('/out:'+$fixtureBootstrap),('/win32manifest:'+$manifest),'/reference:System.dll','/reference:System.Core.dll','/reference:System.Drawing.dll','/reference:System.Windows.Forms.dll')
foreach($name in @('BootstrapProgram.cs','BootstrapForm.cs','BootstrapClient.cs','BootstrapManifest.cs','BootstrapGameLocator.cs','Localization.cs','GameProcessController.cs','InstallerResultProtocol.cs')){$arguments+=(Join-Path $source $name)}
foreach($name in @('BootstrapInfo.Generated.cs','BootstrapAssemblyInfo.Generated.cs')){$arguments+=(Join-Path $InstallerBuildDirectory $name)}
& $csc @arguments 2>&1 | Out-File -LiteralPath (Join-Path $FixtureDirectory 'bootstrap-compile.log') -Encoding utf8
if($LASTEXITCODE -ne 0){throw ('Fixture bootstrap compilation failed: '+[IO.File]::ReadAllText((Join-Path $FixtureDirectory 'bootstrap-compile.log')))}
$packageHash=(Get-FileHash -LiteralPath $fixturePackage).Hash
$manifestText=[IO.File]::ReadAllText((Join-Path $PackageDirectory 'MPatcherUpdate.ini'))
$manifestText=[regex]::Replace($manifestText,'(?m)^InstallerSha256=.*$','InstallerSha256='+$packageHash)
$manifestText=[regex]::Replace($manifestText,'(?m)^InstallerLength=.*$','InstallerLength='+(Get-Item -LiteralPath $fixturePackage).Length)
[IO.File]::WriteAllText((Join-Path $FixtureDirectory 'MPatcherUpdate.ini'),$manifestText,(New-Object Text.UTF8Encoding($false)))
$fixtureAssembly=[Reflection.Assembly]::LoadFile($fixturePackage)
foreach($name in $resources.Keys){
    $stream=$fixtureAssembly.GetManifestResourceStream($name)
    $sha=[Security.Cryptography.SHA256]::Create()
    try{$actual=[BitConverter]::ToString($sha.ComputeHash($stream)).Replace('-','')}finally{$sha.Dispose();$stream.Dispose()}
    if($actual -ne $resourceHashes[$name]){throw ('Fixture resource changed: '+$name)}
}
$report=[ordered]@{FixtureDirectory=$FixtureDirectory;ProductionPackage=$productionPackage;ProductionPackageSha256=(Get-FileHash -LiteralPath $productionPackage).Hash;FixturePackageSha256=$packageHash;FixtureBootstrapSha256=(Get-FileHash -LiteralPath $fixtureBootstrap).Hash;Resources=$resourceHashes;SourcesMatchProduction=$true;RequireAdministrator=$false;NetworkAssistExcluded=$true;NoGameOrServiceExecuted=$true;ProductionUacZipVerificationRequired=$true}
$report | ConvertTo-Json -Depth 6 | Set-Content -LiteralPath (Join-Path $FixtureDirectory 'fixture-build.json') -Encoding UTF8
$report | ConvertTo-Json -Depth 6
