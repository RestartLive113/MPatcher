[CmdletBinding()]
param(
    [Parameter(Mandatory=$true)][string]$PayloadPath,
    [Parameter(Mandatory=$true)][string]$WatchdogPath,
    [Parameter(Mandatory=$true)][string]$ScaleModuleDirectory,
    [Parameter(Mandatory=$true)][string]$EvidenceDirectory
)
$ErrorActionPreference='Stop'
Set-StrictMode -Version Latest
$repo=Split-Path $PSScriptRoot -Parent
$fixtures=Join-Path $repo ('installer-tests\scale-module-'+[DateTime]::UtcNow.ToString('yyyyMMdd-HHmmss-fff'))
New-Item -ItemType Directory -Path $fixtures,$EvidenceDirectory -Force | Out-Null
$dllSource=Join-Path $ScaleModuleDirectory 'MPatcher.ScaleRuntime.dll'
$sidecarSource=Join-Path $ScaleModuleDirectory 'MPatcher.ScaleRuntime.manifest.ini'
$hashes=@{Loader=(Get-FileHash -LiteralPath $PayloadPath).Hash;Watchdog=(Get-FileHash -LiteralPath $WatchdogPath).Hash;Dll=(Get-FileHash -LiteralPath $dllSource).Hash;Sidecar=(Get-FileHash -LiteralPath $sidecarSource).Hash}
$compiler=Join-Path $env:WINDIR 'Microsoft.NET\Framework64\v4.0.30319\csc.exe'
$results=[ordered]@{}
function Check([bool]$condition,[string]$message) { if(-not $condition){throw $message} }
function New-Case([string]$name) {
    $path=Join-Path $fixtures $name
    New-Item -ItemType Directory -Path (Join-Path $path 'McnCraft_Data\Mono'),(Join-Path $path 'UserData') -Force | Out-Null
    [IO.File]::WriteAllBytes((Join-Path $path 'McnCraft.exe'),[byte[]](77,90))
    [IO.File]::WriteAllBytes((Join-Path $path 'McnCraft_Data\Mono\mono.dll'),[byte[]](77,90))
    [IO.File]::WriteAllText((Join-Path $path 'UserData\sentinel'),'preserve-userdata')
    return $path
}
function Paths([string]$root) {
    return @{Loader=(Join-Path $root 'McnCraft_Data\Mono\__Internal.dll');Watchdog=(Join-Path $root 'McnCraft_Data\MPatcherFork\MPatcherCrashWatchdog.exe');Manifest=(Join-Path $root 'McnCraft_Data\MPatcherFork.install.ini');Dll=(Join-Path $root 'McnCraft_Data\MPatcher\Modules\MPatcher.ScaleRuntime.dll');Sidecar=(Join-Path $root 'McnCraft_Data\MPatcher\Modules\MPatcher.ScaleRuntime.manifest.ini')}
}
function Compile-Package([string]$name,[bool]$includeModule,[bool]$badResource=$false) {
    $directory=Join-Path $fixtures ('compile-'+$name)
    New-Item -ItemType Directory -Path $directory -Force | Out-Null
    $source=[IO.File]::ReadAllText((Join-Path $repo 'Installer\PayloadInfo.cs')).Replace('__GENERATED_VERSION__','0.3.9.12').Replace('__GENERATED_SHA256__',$hashes.Loader).Replace('__GENERATED_WATCHDOG_SHA256__',$hashes.Watchdog).Replace('internal const long Length = 0;','internal const long Length = '+(Get-Item -LiteralPath $PayloadPath).Length+'L;').Replace('internal const long WatchdogLength = 0;','internal const long WatchdogLength = '+(Get-Item -LiteralPath $WatchdogPath).Length+'L;')
    if($includeModule){
        $source=$source.Replace('internal const bool ScaleModuleIncluded = false;','internal const bool ScaleModuleIncluded = true;').Replace('__GENERATED_SCALE_MODULE_SHA256__',$hashes.Dll).Replace('__GENERATED_SCALE_MODULE_MANIFEST_SHA256__',$hashes.Sidecar).Replace('internal const long ScaleModuleLength = 0;','internal const long ScaleModuleLength = '+(Get-Item -LiteralPath $dllSource).Length+'L;').Replace('internal const long ScaleModuleManifestLength = 0;','internal const long ScaleModuleManifestLength = '+(Get-Item -LiteralPath $sidecarSource).Length+'L;')
    }else{$source=$source.Replace('__GENERATED_SCALE_MODULE_SHA256__','').Replace('__GENERATED_SCALE_MODULE_MANIFEST_SHA256__','')}
    $generated=Join-Path $directory 'PayloadInfo.cs'
    [IO.File]::WriteAllText($generated,$source,(New-Object Text.UTF8Encoding($false)))
    $exe=Join-Path $directory 'FixturePackage.exe'
    $arguments=@('/nologo','/target:exe','/platform:x64','/optimize+','/codepage:65001',('/out:'+$exe),'/reference:System.dll','/reference:System.Core.dll','/reference:System.Drawing.dll','/reference:System.Windows.Forms.dll',('/resource:'+$PayloadPath+',MachineCraftMPatcherInstaller.Payload.__Internal.dll'),('/resource:'+$WatchdogPath+',MachineCraftMPatcherInstaller.Payload.MPatcherCrashWatchdog.exe'),(Join-Path $repo 'Installer\InstallerEngine.cs'),(Join-Path $repo 'Installer\GameProcessController.cs'),(Join-Path $repo 'Installer\Localization.cs'),(Join-Path $PSScriptRoot 'InstallerScaleModuleChecks.cs'),$generated)
    if($includeModule){
        $resourceDll=$dllSource
        if($badResource){$resourceDll=Join-Path $directory 'corrupt-module.dll';[IO.File]::WriteAllBytes($resourceDll,[IO.File]::ReadAllBytes($dllSource)+[byte[]](1))}
        $arguments+=('/resource:'+$resourceDll+',MachineCraftMPatcherInstaller.Payload.MPatcher.ScaleRuntime.dll'),('/resource:'+$sidecarSource+',MachineCraftMPatcherInstaller.Payload.MPatcher.ScaleRuntime.manifest.ini')
    }
    & $compiler @arguments 2>&1 | Out-File -LiteralPath (Join-Path $directory 'compile.log') -Encoding utf8
    if($LASTEXITCODE -ne 0){throw ('Module fixture compile failed: '+[IO.File]::ReadAllText((Join-Path $directory 'compile.log')))}
    return $exe
}
function Invoke-Case([string]$package,[string]$action,[string]$root,[int]$expected=0) {
    $output=@(& $package $action $root 2>&1)
    $exit=$LASTEXITCODE
    $output | Out-File -LiteralPath (Join-Path $EvidenceDirectory ((Split-Path $root -Leaf)+'-'+$action+'-'+[DateTime]::UtcNow.Ticks+'.log')) -Encoding utf8
    if($exit -ne $expected){throw "Module fixture $action exited $exit, expected $expected : $($output -join ' ')"}
    Check ([IO.File]::ReadAllText((Join-Path $root 'UserData\sentinel')) -eq 'preserve-userdata') 'UserData changed'
    return ($output -join "`n")
}
function Assert-Pair([string]$root,[bool]$exists) {
    $paths=Paths $root
    foreach($key in @('Dll','Sidecar')){
        Check ((Test-Path -LiteralPath $paths[$key]) -eq $exists) ('Module presence '+$key)
        if($exists){Check ((Get-FileHash -LiteralPath $paths[$key]).Hash -eq $hashes[$key]) ('Module hash '+$key)}
    }
}
function Inventory([string]$root) {
    $paths=Paths $root
    $lines=foreach($key in @('Loader','Manifest','Watchdog','Dll','Sidecar')){ if(Test-Path -LiteralPath $paths[$key] -PathType Leaf){$key+'|'+(Get-FileHash -LiteralPath $paths[$key]).Hash}else{$key+'|absent'} }
    return ($lines -join "`n")
}
function Seed-Old([string]$root) {
    $paths=Paths $root
    New-Item -ItemType Directory -Path (Split-Path $paths.Dll -Parent),(Split-Path $paths.Watchdog -Parent) -Force | Out-Null
    [IO.File]::WriteAllBytes($paths.Loader,[byte[]](77,90,31))
    [IO.File]::WriteAllBytes($paths.Dll,[byte[]](77,90,32))
    [IO.File]::WriteAllBytes($paths.Watchdog,[byte[]](77,90,33))
    $oldDllHash=(Get-FileHash -LiteralPath $paths.Dll).Hash
    [IO.File]::WriteAllLines($paths.Sidecar,@('HostApiVersion=1','ModuleAssemblyName=MPatcher.ScaleRuntime','ProductVersion=0.3.9.11',('PayloadSha256='+$oldDllHash)))
    [IO.File]::WriteAllLines($paths.Manifest,@('Format=2','ProductVersion=0.3.9.11',('InstalledPayloadSha256='+(Get-FileHash -LiteralPath $paths.Loader).Hash),('InstalledWatchdogSha256='+(Get-FileHash -LiteralPath $paths.Watchdog).Hash),'OriginalExisted=False',('InstalledScaleModuleSha256='+$oldDllHash),('InstalledScaleModuleManifestSha256='+(Get-FileHash -LiteralPath $paths.Sidecar).Hash),'InstalledScaleModuleApiVersion=1','InstalledScaleModuleName=MPatcher.ScaleRuntime'))
}
$alpha=Compile-Package 'alpha' $true
$stable=Compile-Package 'no-module' $false
$broken=Compile-Package 'bad-resource' $true $true
$clean=New-Case 'clean'
$null=Invoke-Case $alpha 'install' $clean
Assert-Pair $clean $true
Check ((Invoke-Case $alpha 'probe' $clean).Trim() -eq 'True') 'Probe misses installed module'
$initial=Inventory $clean
$null=Invoke-Case $alpha 'install' $clean
Check ((Get-FileHash -LiteralPath (Paths $clean).Dll).Hash -eq $hashes.Dll) 'Repeat install mismatch'
$results.CleanRepeatProbe=$true
$paths=Paths $clean
[IO.File]::Delete($paths.Sidecar)
Check ((Invoke-Case $alpha 'probe' $clean).Trim() -eq 'False') 'Probe ignored missing sidecar'
$null=Invoke-Case $alpha 'install' $clean
Assert-Pair $clean $true
[IO.File]::Delete($paths.Dll)
$null=Invoke-Case $alpha 'install' $clean
Assert-Pair $clean $true
$results.MissingPairRepair=$true
$legacy=New-Case 'legacy-manual-split'
Seed-Old $legacy
$legacyPaths=Paths $legacy
$legacyText=[IO.File]::ReadAllText($legacyPaths.Manifest)
$legacyDllHash=(Get-FileHash -LiteralPath $legacyPaths.Dll).Hash
$legacyText=[regex]::Replace($legacyText,'(?m)^InstalledScaleModule.*\r?\n?','')+"`nScaleRuntimeApiVersion=1`nScaleRuntimePayloadSha256=$legacyDllHash`n"
[IO.File]::WriteAllText($legacyPaths.Manifest,$legacyText)
$null=Invoke-Case $alpha 'install' $legacy
Assert-Pair $legacy $true
Check ([IO.File]::ReadAllText($legacyPaths.Manifest).Contains('InstalledScaleModuleManifestSha256='+$hashes.Sidecar)) 'Legacy ownership not migrated'
$results.LegacyManualSplitUpgrade=$true
$unknown=New-Case 'unknown-canonical'
$unknownPaths=Paths $unknown
New-Item -ItemType Directory -Path (Split-Path $unknownPaths.Dll -Parent) -Force | Out-Null
[IO.File]::WriteAllBytes($unknownPaths.Dll,[byte[]](77,90,99))
$before=Inventory $unknown
$null=Invoke-Case $alpha 'install' $unknown 1
Check ((Inventory $unknown) -eq $before) 'Unknown conflict changed owned files'
$results.UnknownCanonicalFailClosed=$true
foreach($key in @('Dll','Sidecar')) {
    $tampered=New-Case ('tampered-'+$key)
    $null=Invoke-Case $alpha 'install' $tampered
    $target=(Paths $tampered)[$key]
    [IO.File]::WriteAllBytes($target,[IO.File]::ReadAllBytes($target)+[byte[]](66))
    $before=Inventory $tampered
    $null=Invoke-Case $alpha 'install' $tampered 1
    $null=Invoke-Case $alpha 'uninstall' $tampered 1
    Check ((Inventory $tampered) -eq $before) 'Tamper rejection changed files'
    $results['Tampered'+$key+'InstallUninstallFailClosed']=$true
}
$resource=New-Case 'corrupt-resource'
Seed-Old $resource
$before=Inventory $resource
$null=Invoke-Case $broken 'install' $resource 1
Check ((Inventory $resource) -eq $before) 'Corrupt resource changed installation'
Check (-not (Test-Path -LiteralPath (Join-Path $resource 'MPatcherModuleTransaction'))) 'Corrupt resource left transaction'
$results.CorruptResourcePreflight=$true
$downgrade=New-Case 'alpha-to-stable'
$null=Invoke-Case $alpha 'install' $downgrade
$other=Join-Path (Split-Path (Paths $downgrade).Dll -Parent) 'Unknown.dll'
[IO.File]::WriteAllText($other,'unknown module keep')
$null=Invoke-Case $stable 'install' $downgrade
Assert-Pair $downgrade $false
Check ([IO.File]::ReadAllText($other) -eq 'unknown module keep') 'Stable transition removed unrelated module'
$null=Invoke-Case $alpha 'install' $downgrade
$null=Invoke-Case $alpha 'uninstall' $downgrade
Assert-Pair $downgrade $false
Check ([IO.File]::ReadAllText($other) -eq 'unknown module keep') 'Uninstall removed unrelated module'
$results.StableTransitionUnknownPreservedUninstall=$true
foreach($command in @('rollback','interrupt')) {
    $case=New-Case $command
    Seed-Old $case
    $before=Inventory $case
    $null=Invoke-Case $alpha $command $case
    if($command -eq 'interrupt') {
        Check (Test-Path -LiteralPath (Join-Path $case 'MPatcherModuleTransaction\journal.ini')) 'Interrupted journal missing'
        $null=Invoke-Case $alpha 'recover' $case
    }
    Check ((Inventory $case) -eq $before) ($command+' failed full pair/loader/manifest/watchdog restore')
    Check (-not (Test-Path -LiteralPath (Join-Path $case 'MPatcherModuleTransaction'))) ($command+' left transaction')
    $results[$command+'RestoresCoherentState']=$true
}
$blocked=New-Case 'recovery-changed-target'
Seed-Old $blocked
$null=Invoke-Case $alpha 'interrupt' $blocked
$blockedDll=(Paths $blocked).Dll
[IO.File]::WriteAllBytes($blockedDll,[IO.File]::ReadAllBytes($blockedDll)+[byte[]](77))
$before=Inventory $blocked
$null=Invoke-Case $alpha 'recover' $blocked 1
Check ((Inventory $blocked) -eq $before) 'Recovery overwrote changed target'
$results.RecoveryChangedTargetFailClosed=$true
$recovery=New-Case 'automatic-recovery'
Seed-Old $recovery
$null=Invoke-Case $alpha 'interrupt' $recovery
$null=Invoke-Case $alpha 'install' $recovery
Assert-Pair $recovery $true
Check (-not (Test-Path -LiteralPath (Join-Path $recovery 'MPatcherModuleTransaction'))) 'Automatic recovery did not clean journal'
$results.AutomaticRecovery=$true
$rollbackFixture=New-Case 'engine-rollback'
$null=Invoke-Case $alpha 'install' $rollbackFixture
$before=Inventory $rollbackFixture
New-Item -ItemType Directory -Path ((Paths $rollbackFixture).Manifest+'.rollback') -Force | Out-Null
$null=Invoke-Case $alpha 'install' $rollbackFixture 1
Check ((Inventory $rollbackFixture) -eq $before) 'Engine exception failed rollback'
$results.EngineExceptionRollback=$true
function Corrupt-WatchdogDirectory([string]$root) {
    $watchdog=(Paths $root).Watchdog
    [IO.File]::Delete($watchdog)
    New-Item -ItemType Directory -Path (Join-Path $watchdog 'child'),(Join-Path $watchdog 'empty-child') -Force | Out-Null
    [IO.File]::WriteAllText((Join-Path $watchdog 'child\stale-file.txt'),'preserve-watchdog-directory-on-rollback')
}
$supportHeal=New-Case 'support-watchdog-directory-self-heal'
$null=Invoke-Case $alpha 'install' $supportHeal
Corrupt-WatchdogDirectory $supportHeal
$null=Invoke-Case $alpha 'install' $supportHeal
Check ((Get-FileHash -LiteralPath (Paths $supportHeal).Watchdog).Hash -eq $hashes.Watchdog) 'Watchdog directory self-heal failed'
Assert-Pair $supportHeal $true
$results.WatchdogDirectorySelfHeal=$true
foreach($command in @('rollback-engine','interrupt-engine')) {
    $case=New-Case ('watchdog-directory-'+$command)
    $null=Invoke-Case $alpha 'install' $case
    Corrupt-WatchdogDirectory $case
    $before=Inventory $case
    $null=Invoke-Case $alpha $command $case $(if($command -eq 'rollback-engine'){1}else{0})
    if($command -eq 'interrupt-engine'){$null=Invoke-Case $alpha 'recover' $case}
    Check ((Inventory $case) -eq $before) ('Watchdog directory '+$command+' changed installation')
    $watchdog=(Paths $case).Watchdog
    Check ([IO.File]::ReadAllText((Join-Path $watchdog 'child\stale-file.txt')) -eq 'preserve-watchdog-directory-on-rollback') ('Watchdog directory content not restored: '+$command)
    Check (Test-Path -LiteralPath (Join-Path $watchdog 'empty-child') -PathType Container) ('Watchdog empty directory not restored: '+$command)
    Check (-not (Test-Path -LiteralPath (Join-Path $case 'MPatcherModuleTransaction'))) 'Watchdog directory recovery left transaction'
    $results['WatchdogDirectory'+$command+'Restore']=$true
}
$report=[ordered]@{Passed=$true;Checks=$results.Count;Cases=$results;FixtureRoot=$fixtures;Sources=@('Installer/InstallerEngine.cs','Installer/PayloadInfo.cs');NoGameExecuted=$true;NetworkAssistNotCompiled=$true;PayloadHash=$hashes.Loader;ModuleHash=$hashes.Dll;SidecarHash=$hashes.Sidecar}
$report | ConvertTo-Json -Depth 6 | Set-Content -LiteralPath (Join-Path $EvidenceDirectory 'module-installer-fixtures.json') -Encoding UTF8
$report | ConvertTo-Json -Depth 6
