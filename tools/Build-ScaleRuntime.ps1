[CmdletBinding()]
param(
    [Parameter(Mandatory = $true)][string]$CoreAssemblyPath,
    [Parameter(Mandatory = $true)][string]$OutputDirectory,
    [Parameter(Mandatory = $true)][ValidatePattern('^\d+\.\d+\.\d+\.\d+$')][string]$ProductVersion,
    [string]$SourceDirectory,
    [string]$ReferenceDirectory
)

$ErrorActionPreference = 'Stop'
Set-StrictMode -Version Latest
$repo = Split-Path -Parent $PSScriptRoot
$project = [IO.Path]::GetFullPath((Join-Path $repo '..\..'))
$buildRoot = [IO.Path]::GetFullPath((Join-Path $repo 'build'))
$output = [IO.Path]::GetFullPath($OutputDirectory)
if (-not $output.StartsWith($buildRoot + '\scale-', [StringComparison]::OrdinalIgnoreCase)) {
    throw 'Choose an unused repository/build/scale-* output directory.'
}
if (Test-Path -LiteralPath $output) { throw 'Output exists; choose a new revision to preserve evidence.' }
$core = (Resolve-Path -LiteralPath $CoreAssemblyPath).Path
$coreBefore = (Get-FileHash -LiteralPath $core -Algorithm SHA256).Hash
$defaultSourcesRoot = [IO.Path]::GetFullPath((Join-Path $repo 'ScaleRuntime'))
if (-not $SourceDirectory) { $SourceDirectory = $defaultSourcesRoot }
if (-not (Test-Path -LiteralPath $SourceDirectory -PathType Container)) { throw 'ScaleRuntime source directory is missing.' }
$sourcesRoot = (Resolve-Path -LiteralPath $SourceDirectory).Path
$analysisRoot = [IO.Path]::GetFullPath((Join-Path $project 'analysis'))
if (-not $sourcesRoot.Equals($defaultSourcesRoot, [StringComparison]::OrdinalIgnoreCase) -and
    -not $sourcesRoot.StartsWith($analysisRoot + '\', [StringComparison]::OrdinalIgnoreCase)) {
    throw 'SourceDirectory must be ScaleRuntime or an immutable project/analysis source snapshot.'
}
if (-not (Test-Path -LiteralPath $sourcesRoot -PathType Container)) { throw 'ScaleRuntime source directory is missing.' }
$sources = @(Get-ChildItem -LiteralPath $sourcesRoot -Filter '*.cs' -File -Recurse | Sort-Object FullName)
if ($sources.Count -eq 0) { throw 'ScaleRuntime has no C# source files.' }
if (-not $ReferenceDirectory) {
    $ReferenceDirectory = Join-Path $project 'reference\mpatcher-installer-v1.6\mpatchrmain-recovery\references-patched-game'
}
$referencesRoot = (Resolve-Path -LiteralPath $ReferenceDirectory).Path
$references = @(Get-ChildItem -LiteralPath $referencesRoot -Filter '*.dll' -File | Sort-Object Name)
if (@($references | Where-Object Name -eq 'MPatchrMain.dll').Count -ne 0) {
    throw 'Reference directory must not provide a second MPatchrMain.dll.'
}
$key = (Resolve-Path -LiteralPath (Join-Path $repo 'signing\MPatchrMain.public.snk')).Path
Add-Type -Path (Join-Path $PSScriptRoot 'bin\dnlib.dll')
$main = [dnlib.DotNet.ModuleDefMD]::Load($core)
try {
    if ($main.Assembly.Name.String -ne 'MPatchrMain') { throw 'Core assembly identity must be MPatchrMain.' }
    $publicKey = [BitConverter]::ToString($main.Assembly.PublicKey.Data).Replace('-', '').ToLowerInvariant()
    if (-not $main.IsStrongNameSigned -or $publicKey.Length -eq 0) { throw 'Core public-sign metadata is missing.' }
    $friend = 'MPatcher.ScaleRuntime, PublicKey=' + $publicKey
    $friends = @($main.Assembly.CustomAttributes | Where-Object {
        $_.TypeFullName -eq 'System.Runtime.CompilerServices.InternalsVisibleToAttribute'
    } | ForEach-Object { [string]$_.ConstructorArguments[0].Value })
    if (@($friends | Where-Object { $_.Equals($friend, [StringComparison]::OrdinalIgnoreCase) }).Count -ne 1) {
        throw 'Core must grant exactly one public-key-qualified MPatcher.ScaleRuntime friend access.'
    }
    if ($null -eq $main.Find('MPatcherFork.CustomPatches.IScaleRuntime', $false)) {
        throw 'Core ScaleRuntime interface is missing.'
    }
    if (@($main.GetAssemblyRefs() | Where-Object { $_.Name.String -eq 'MPatcher.ScaleRuntime' }).Count -ne 0) {
        throw 'Stable core must not contain a compile-time ScaleRuntime assembly reference.'
    }
    $coreMvid = $main.Mvid.ToString()
    $coreIdentity = $main.Assembly.FullName
} finally { $main.Dispose() }

$sdkLines = @(& dotnet --list-sdks)
if ($LASTEXITCODE -ne 0 -or $sdkLines.Count -eq 0) { throw 'Unable to locate a .NET SDK.' }
$sdkLine = $sdkLines | Select-Object -Last 1
if ($sdkLine -notmatch '^([^ ]+) \[(.+)\]$') { throw "Unrecognized SDK line: $sdkLine" }
$sdkVersion = $Matches[1]
$compiler = Join-Path (Join-Path $Matches[2] $sdkVersion) 'Roslyn\bincore\csc.dll'
if (-not (Test-Path -LiteralPath $compiler)) { throw "Missing compiler: $compiler" }
New-Item -ItemType Directory -Path (Join-Path $output 'generated') -Force | Out-Null
$sourceCopies = @(
    foreach ($source in $sources) {
        $relative = $source.FullName.Substring($sourcesRoot.Length).TrimStart([char[]]'\/')
        $copy = Join-Path (Join-Path $output 'sources') $relative
        New-Item -ItemType Directory -Path (Split-Path -Parent $copy) -Force | Out-Null
        $sourceHash = (Get-FileHash -LiteralPath $source.FullName).Hash
        Copy-Item -LiteralPath $source.FullName -Destination $copy
        if ((Get-FileHash -LiteralPath $copy).Hash -ne $sourceHash) { throw "Source changed during snapshot: $($source.FullName)" }
        [ordered]@{ Path = $source.FullName; RelativePath = $relative; SnapshotPath = $copy; Sha256 = $sourceHash }
    }
)
$versionSource = Join-Path $output 'generated\ScaleRuntimeVersion.cs'
$versionText = @"
using System;
using System.Reflection;
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("$ProductVersion")]
[assembly: AssemblyInformationalVersion("$ProductVersion")]
[assembly: MPatcherFork.ScaleRuntime.ScaleRuntimeApiVersionAttribute(1)]
namespace MPatcherFork.ScaleRuntime
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
    internal sealed class ScaleRuntimeApiVersionAttribute : Attribute
    {
        internal ScaleRuntimeApiVersionAttribute(int version) { Version = version; }
        internal int Version { get; private set; }
    }
}
"@
[IO.File]::WriteAllText($versionSource, $versionText, [Text.UTF8Encoding]::new($false))
$payload = Join-Path $output 'MPatcher.ScaleRuntime.dll'
$response = Join-Path $output 'csc.rsp'
$compilerLog = Join-Path $output 'compiler.log'
$arguments = [Collections.Generic.List[string]]::new()
foreach ($argument in @('/nologo', '/noconfig', '/nostdlib+', '/target:library', '/unsafe+',
    '/platform:anycpu', '/optimize+', '/deterministic+', '/langversion:12', '/utf8output', '/warn:0',
    ('/out:"{0}"' -f $payload), ('/keyfile:"{0}"' -f $key), '/publicsign+')) {
    $arguments.Add($argument)
}
foreach ($reference in $references) { $arguments.Add('/reference:"{0}"' -f $reference.FullName) }
$arguments.Add('/reference:"{0}"' -f $core)
foreach ($source in $sourceCopies) { $arguments.Add('"{0}"' -f $source.SnapshotPath) }
$arguments.Add('"{0}"' -f $versionSource)
$arguments | Set-Content -LiteralPath $response -Encoding UTF8
$started = [DateTime]::UtcNow
$compilerOutput = @(& dotnet $compiler ('@' + $response) 2>&1)
$compileExit = $LASTEXITCODE
$compilerOutput | Set-Content -LiteralPath $compilerLog -Encoding UTF8
if ($compileExit -ne 0) { throw "ScaleRuntime compilation failed; see $compilerLog" }

$module = [dnlib.DotNet.ModuleDefMD]::Load($payload)
try {
    if ($module.Assembly.Name.String -ne 'MPatcher.ScaleRuntime' -or $module.Assembly.Version.ToString() -ne '1.0.0.0') {
        throw 'ScaleRuntime assembly name/version mismatch.'
    }
    $modulePublicKey = [BitConverter]::ToString($module.Assembly.PublicKey.Data).Replace('-', '').ToLowerInvariant()
    if (-not $module.IsStrongNameSigned -or $modulePublicKey -ne $publicKey) { throw 'ScaleRuntime public-sign identity differs from core.' }
    $entry = $module.Find('MPatcherFork.ScaleRuntime.ScaleRuntimeModule', $false)
    if ($null -eq $entry -or -not $entry.IsPublic -or $entry.IsAbstract) { throw 'Public concrete ScaleRuntimeModule entry is missing.' }
    if (@($entry.Interfaces | Where-Object {
        $_.Interface.FullName -eq 'MPatcherFork.CustomPatches.IScaleRuntime'
    }).Count -ne 1) { throw 'ScaleRuntimeModule must implement the stable core IScaleRuntime interface.' }
    if (@($entry.Methods | Where-Object {
        $_.IsInstanceConstructor -and $_.IsPublic -and $_.MethodSig.Params.Count -eq 0
    }).Count -ne 1) { throw 'ScaleRuntimeModule public parameterless constructor is missing.' }
    $api = @($module.Assembly.CustomAttributes | Where-Object {
        $_.TypeFullName -eq 'MPatcherFork.ScaleRuntime.ScaleRuntimeApiVersionAttribute'
    })
    if ($api.Count -ne 1 -or [int]$api[0].ConstructorArguments[0].Value -ne 1) { throw 'Host API version attribute mismatch.' }
    foreach ($type in $module.GetTypes()) {
        if ($type.Namespace.String -eq 'MPatcherFork.CustomPatches') { throw "Module defines a stable core namespace type: $($type.FullName)" }
        foreach ($method in $type.Methods) {
            if (-not $method.HasBody) { continue }
            foreach ($instruction in $method.Body.Instructions) {
                $callee = $instruction.Operand -as [dnlib.DotNet.IMethod]
                if ($null -ne $callee -and $callee.DeclaringType.FullName -eq 'HarmonyLib.Harmony' -and
                    $callee.Name.String -in @('Patch', 'PatchAll', 'UnpatchAll', 'CreateAndPatchAll')) {
                    throw "Mutable module registers/changes Harmony targets: $($method.FullName) -> $($callee.FullName)"
                }
            }
        }
    }
    $mainRefs = @($module.GetAssemblyRefs() | Where-Object { $_.Name.String -eq 'MPatchrMain' })
    if ($mainRefs.Count -ne 1 -or $mainRefs[0].FullName -ne $coreIdentity) { throw 'ScaleRuntime references a different core identity.' }
    $moduleMvid = $module.Mvid.ToString()
} finally { $module.Dispose() }
$coreAfter = (Get-FileHash -LiteralPath $core -Algorithm SHA256).Hash
if ($coreAfter -ne $coreBefore) { throw 'Core bytes changed during module compilation.' }
$payloadHash = (Get-FileHash -LiteralPath $payload -Algorithm SHA256).Hash
$manifest = Join-Path $output 'MPatcher.ScaleRuntime.manifest.ini'
$manifestText = "HostApiVersion=1`r`nModuleAssemblyName=MPatcher.ScaleRuntime`r`nProductVersion=$ProductVersion`r`nPayloadSha256=$payloadHash`r`n"
[IO.File]::WriteAllText($manifest, $manifestText, [Text.UTF8Encoding]::new($false))
$result = [ordered]@{
    CreatedUtc = $started.ToString('o'); ProductVersion = $ProductVersion; HostApiVersion = 1
    ModuleAssemblyName = 'MPatcher.ScaleRuntime'; AssemblyVersion = '1.0.0.0'; PublicSigned = $true
    Sdk = $sdkVersion; CoreReference = $core; CoreIdentity = $coreIdentity; CoreMvid = $coreMvid
    CoreRefSha256 = $coreBefore; CoreUnchanged = $true; Module = $payload; ModuleMvid = $moduleMvid
    PayloadSha256 = $payloadHash; Manifest = $manifest; ManifestSha256 = (Get-FileHash -LiteralPath $manifest).Hash
    SourceDirectory = $sourcesRoot; Sources = $sourceCopies
    SourceCount = $sources.Count; References = $references.Count + 1; ExitCode = $compileExit
    StaticVerified = $true; RuntimeTested = $false; Deployed = $false; FullCaptureRequired = $false
}
$result | ConvertTo-Json -Depth 6 | Tee-Object -FilePath (Join-Path $output 'scale-runtime-build.json')
