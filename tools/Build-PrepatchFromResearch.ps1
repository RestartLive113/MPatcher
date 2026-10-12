param(
 [Parameter(Mandatory=$true)][ValidatePattern('^[a-z0-9-]+$')][string]$Revision,
 [Parameter(Mandatory=$true)][ValidatePattern('^\d+\.\d+\.\d+\.\d+$')][string]$ProductVersion,
 [Parameter(Mandatory=$true)][string]$ResearchBuildDirectory
)
$ErrorActionPreference='Stop'
$r=Split-Path -Parent $PSScriptRoot
$p=[IO.Path]::GetFullPath((Join-Path $r '..\..'))
$b=Join-Path $r ('build\prepatch-'+$Revision)
if(Test-Path $b){throw 'New revision required'}
New-Item -ItemType Directory -Path (Join-Path $b 'generated'),(Join-Path $b 'roslyn') | Out-Null
$research=(Resolve-Path -LiteralPath $ResearchBuildDirectory).Path
$buildRoot=[IO.Path]::GetFullPath((Join-Path $r 'build'))
if (-not $research.StartsWith($buildRoot+'\',[StringComparison]::OrdinalIgnoreCase)) { throw 'Research build must be under repository/build' }
$researchManifest=Get-Content -LiteralPath (Join-Path $research 'research.json') -Raw | ConvertFrom-Json
if ($researchManifest.ProductVersion -ne $ProductVersion -or -not $researchManifest.PublicAlpha) { throw 'Research build profile or product version mismatch' }
$alpha=Join-Path $research 'generated'
foreach ($name in @('MagnificationDownscale.cs','ResearchBodyWater.cs','ResearchRecoil.cs')) {
 if (-not (Test-Path -LiteralPath (Join-Path $alpha $name))) { throw "Missing research override: $name" }
}
$versionSource=Join-Path $b 'generated\Version.cs'
[IO.File]::WriteAllText($versionSource,('using System.Reflection; [assembly: AssemblyInformationalVersion("'+$ProductVersion+'")]'),[Text.UTF8Encoding]::new($false))
$src=Join-Path $r 'RecoveredSource\MPatchrMain\MPatchr.cs'
$txt=[IO.File]::ReadAllText($src)
$needle='MPatcherFork.CustomPatches.CrashDiagnostics.TryRegister();'
if([regex]::Matches($txt,[regex]::Escape($needle)).Count -ne 1){throw 'Load entry changed'}
$txt=$txt.Replace($needle,'MPatcherFork.CustomPatches.PrepatchCapture.Begin();'+"`r`n`t`t"+$needle)
$entry=Join-Path $b 'generated\MPatchr.cs'
[IO.File]::WriteAllText($entry,$txt,[Text.UTF8Encoding]::new($false))
$refs=Join-Path $p 'reference\mpatcher-installer-v1.6\mpatchrmain-recovery\references-patched-game'
$original=Join-Path $p 'reference\mpatcher-installer-v1.6\mpatchrmain-recovery\de4dot-cex-default-keep-types\MPatchrMain.deobfuscated.dll'
$overrides=@{}
$overrides[$src]=$entry
# Recovered Class48 declares both Start and Update; metadata attribute ordering
# changed its resolved target in cap11. Preserve the installed Option.Update guard.
# Only the generated annotation changes: recovered source and prefix body stay intact.
$optionSource=Join-Path $r 'RecoveredSource\Class48.cs'
$optionText=[IO.File]::ReadAllText($optionSource)
$optionHeader='\[HarmonyPriority\(800\)\]\s*\[HarmonyPatch\("Update"\)\]\s*\[HarmonyPatch\(typeof\(Option\)\)\]\s*\[HarmonyPatch\("Start"\)\]'
if([regex]::Matches($optionText,$optionHeader).Count -ne 1){throw 'Recovered Option guard annotations changed'}
$optionText=[regex]::Replace($optionText,$optionHeader,'[HarmonyPriority(800)]' + "`r`n" + '[HarmonyPatch(typeof(Option), "Update")]')
$optionGenerated=Join-Path $b 'generated\Class48.cs'
[IO.File]::WriteAllText($optionGenerated,$optionText,[Text.UTF8Encoding]::new($false))
$overrides[$optionSource]=$optionGenerated
$magSource=[IO.File]::ReadAllText((Join-Path $alpha 'MagnificationDownscale.cs'))
$magSource=[regex]::Replace($magSource,'(private static void MenuUpdatePostfix\(\)\s*\{)','$1' + "`r`n            PrepatchCapture.Poll();")
$magGenerated=Join-Path $b 'generated\MagnificationDownscale.cs'
[IO.File]::WriteAllText($magGenerated,$magSource,[Text.UTF8Encoding]::new($false))
$overrides[(Join-Path $r 'CustomPatches\MagnificationDownscale.cs')]=$magGenerated
$overrides[(Join-Path $r 'RecoveredSource\UDIRjB3VSmwHpVmUxcnHH66kk6ThWzbxmySWu7OngspoKs_47sbCTTGVAxk9y7DBfQ.cs')]=Join-Path $alpha 'ResearchBodyWater.cs'
$overrides[(Join-Path $r 'RecoveredSource\yzJYu9roTp6ENqtLoiCZ-IkzieVUW5qCWgOWO91IB0T1qP3hebt7hsycbIzVsKXt6A.cs')]=Join-Path $alpha 'ResearchRecoil.cs'
& "$PSScriptRoot\Build-DecompiledMPatcher.ps1" -ProjectDirectory $r -ReferenceDirectory $refs -OutputPath "$b\roslyn\MPatchrMain.dll" -ResponsePath "$b\csc.rsp" -LogPath "$b\compiler.log" -EmbeddedResourceDirectory "$r\resources" -PublicKeyFile "$r\signing\MPatchrMain.public.snk" -AdditionalSourceFiles @($versionSource) -SourceOverrides $overrides -ExcludePlayerPresence
if($LASTEXITCODE -ne 0){throw 'Compile failed'}
& "$PSScriptRoot\bin\Restore-MPatcherMetadataNames.exe" "$b\roslyn\MPatchrMain.dll" "$b\MPatchrMain.dll" $original > "$b\metadata.log"
if($LASTEXITCODE -ne 0){throw 'Metadata failed'}
& "$r\Build-TestLoader.ps1" -MPatchrMainPath "$b\MPatchrMain.dll" -OutputPath "$b\__Internal.dll" -SkipMPatcherBuild -ProductVersion $ProductVersion -ExcludePlayerPresence
if($LASTEXITCODE -ne 0){throw 'Loader failed'}
