[CmdletBinding()]
param(
 [Parameter(Mandatory=$true)][string]$CaptureDirectory,
 [Parameter(Mandatory=$true)][string]$PreparedAssemblyPath,
 [Parameter(Mandatory=$true)][string]$OutputDirectory,
 [string]$ReferenceDirectory,
 [ValidatePattern('^\d+\.\d+\.\d+(?:\.\d+)?$')][string]$ProductVersion='0.3.9.2'
)
$ErrorActionPreference='Stop'
Set-StrictMode -Version Latest
$repo=Split-Path -Parent $PSScriptRoot
$project=[IO.Path]::GetFullPath((Join-Path $repo '..\..'))
if (-not $ReferenceDirectory) { $ReferenceDirectory=Join-Path $project 'reference\mpatcher-installer-v1.6\mpatchrmain-recovery\references-patched-game' }
$capture=(Resolve-Path -LiteralPath $CaptureDirectory).Path
$inputPayload=(Resolve-Path -LiteralPath $PreparedAssemblyPath).Path
$output=[IO.Path]::GetFullPath($OutputDirectory)
$buildRoot=[IO.Path]::GetFullPath((Join-Path $repo 'build'))
if (-not $output.StartsWith($buildRoot+'\',[StringComparison]::OrdinalIgnoreCase)) { throw 'Output must be an unused directory under repository/build' }
if (Test-Path -LiteralPath $output) { throw 'Output exists; choose a new revision to preserve evidence' }
$completion=Get-Content -LiteralPath (Join-Path $capture 'complete.txt') -Raw
if ($completion -notmatch '(?m)^failures=0\s*$') { throw 'Capture is incomplete or contains errors' }
New-Item -ItemType Directory -Path $output | Out-Null
$csc=Join-Path $env:WINDIR 'Microsoft.NET\Framework64\v4.0.30319\csc.exe'
foreach ($name in @('BuildPrecompiledPatches','VerifyPrecompiledIdentity')) {
 & $csc /nologo /target:exe /optimize+ ('/out:'+ (Join-Path $output ($name+'.exe'))) ('/reference:'+ (Join-Path $PSScriptRoot 'bin\dnlib.dll')) (Join-Path $PSScriptRoot ($name+'.cs')) *> (Join-Path $output ($name+'-compile.log'))
 if ($LASTEXITCODE -ne 0) { throw "Failed compiling $name" }
}
Copy-Item -LiteralPath (Join-Path $PSScriptRoot 'bin\dnlib.dll') -Destination (Join-Path $output 'dnlib.dll')
& (Join-Path $output 'BuildPrecompiledPatches.exe') $capture $inputPayload $ReferenceDirectory $output *> (Join-Path $output 'weave.log')
if ($LASTEXITCODE -ne 0) { throw 'Offline IL preparation failed; see weave.log' }
& (Join-Path $output 'VerifyPrecompiledIdentity.exe') $inputPayload (Join-Path $ReferenceDirectory 'Assembly-CSharp.dll') $output *> (Join-Path $output 'identity.log')
if ($LASTEXITCODE -ne 0) { throw 'Original IL/metadata identity check failed' }
& (Join-Path $repo 'Verify.ps1') -AssemblyPath (Join-Path $output 'MPatchrMain.dll') -ExpectedProductVersion $ProductVersion -ExpectPlayerPresenceExcluded -ExpectPrecompiledPatches *> (Join-Path $output 'verify.log')
if ($LASTEXITCODE -ne 0) { throw 'Prepared assembly verification failed' }
Copy-Item -LiteralPath (Join-Path $repo 'logs\verification-summary.json') -Destination (Join-Path $output 'verification-summary.json')
$loader=Join-Path $output '__Internal.dll'
& (Join-Path $repo 'Build-TestLoader.ps1') -MPatchrMainPath (Join-Path $output 'MPatchrMain.dll') -OutputPath $loader -SkipMPatcherBuild -ProductVersion $ProductVersion -ExcludePlayerPresence *> (Join-Path $output 'loader.log')
if ($LASTEXITCODE -ne 0) { throw 'Loader packing failed' }
# Existing native loader validates the original game fingerprint before applying
# MBSDIF43. A full extra-copy record carries the prepared image entirely in memory.
$game=[IO.File]::ReadAllBytes((Join-Path $output 'Assembly-CSharp.dll'))
$stream=[IO.MemoryStream]::new(); $writer=[IO.BinaryWriter]::new($stream)
try {
 $writer.Write([Text.Encoding]::ASCII.GetBytes('MBSDIF43'))
 $writer.Write([int64]24); $writer.Write([int64]0); $writer.Write([int64]$game.Length)
 $writer.Write([int64]0); $writer.Write([int64]$game.Length); $writer.Write([int64]0); $writer.Write($game)
 $patch=$stream.ToArray()
} finally { $writer.Dispose(); $stream.Dispose() }
$stream=[IO.MemoryStream]::new(); $writer=[IO.BinaryWriter]::new($stream)
try {
 $writer.Write([byte]2); $writer.Write([uint32]($patch.Length+9)); $writer.Write([uint32]$patch.Length); $writer.Write($patch)
 $container=$stream.ToArray()
} finally { $writer.Dispose(); $stream.Dispose() }
[IO.File]::WriteAllBytes((Join-Path $output 'game.resource.bin'),$container)
$handle=[MPatcherFork.NativeResources]::BeginUpdateResource($loader,$false)
if ($handle -eq [IntPtr]::Zero) { throw 'BeginUpdateResource failed' }
$committed=$false
try {
 if (-not [MPatcherFork.NativeResources]::UpdateResource($handle,'BINARY',[IntPtr]105,[uint16]2048,$container,[uint32]$container.Length)) { throw 'Game resource update failed' }
 if (-not [MPatcherFork.NativeResources]::EndUpdateResource($handle,$false)) { throw 'Game resource commit failed' }
 $committed=$true
} finally { if (-not $committed) { [void][MPatcherFork.NativeResources]::EndUpdateResource($handle,$true) } }
# Read back both resources from the completed PE without executing the loader.
$module=[MPatcherFork.NativeResources]::LoadLibraryEx($loader,[IntPtr]::Zero,[uint32]2)
if ($module -eq [IntPtr]::Zero) { throw 'Loader resource inspection failed' }
try {
 foreach ($id in @(103,105)) {
  $expected=if ($id -eq 103) { [IO.File]::ReadAllBytes((Join-Path $output 'MPatchrMain.dll')) } else { $container }
  $resource=[MPatcherFork.NativeResources]::FindResourceEx($module,'BINARY',[IntPtr]$id,[uint16]2048)
  if ($resource -eq [IntPtr]::Zero) { throw "Missing resource $id" }
  $size=[MPatcherFork.NativeResources]::SizeofResource($module,$resource)
  if ($size -ne $expected.Length) { throw "Resource size mismatch $id" }
  $data=[MPatcherFork.NativeResources]::LockResource([MPatcherFork.NativeResources]::LoadResource($module,$resource))
  $actual=[byte[]]::new($size); [Runtime.InteropServices.Marshal]::Copy($data,$actual,0,$size)
  $sha=[Security.Cryptography.SHA256]::Create()
  try { if ([Convert]::ToBase64String($sha.ComputeHash($actual)) -ne [Convert]::ToBase64String($sha.ComputeHash($expected))) { throw "Resource hash mismatch $id" } } finally { $sha.Dispose() }
 }
} finally { [void][MPatcherFork.NativeResources]::FreeLibrary($module) }
$result=[ordered]@{
 CreatedUtc=[DateTime]::UtcNow.ToString('o'); ProductVersion=$ProductVersion; Capture=$capture
 InputPayloadSha256=(Get-FileHash -LiteralPath $inputPayload).Hash
 CapturePlansSha256=(Get-FileHash -LiteralPath (Join-Path $capture 'plans.tsv')).Hash
 Loader=$loader; LoaderSha256=(Get-FileHash -LiteralPath $loader).Hash
 PayloadSha256=(Get-FileHash -LiteralPath (Join-Path $output 'MPatchrMain.dll')).Hash
 GameSha256=(Get-FileHash -LiteralPath (Join-Path $output 'Assembly-CSharp.dll')).Hash
 Plans=@(Get-Content -LiteralPath (Join-Path $output 'plans.tsv')).Count
 StaticVerified=$true; NativeResourcesVerified=$true; Deployed=$false; RuntimeTested=$false; Published=$false
}
$result | ConvertTo-Json | Tee-Object -FilePath (Join-Path $output 'precompiled-build.json')