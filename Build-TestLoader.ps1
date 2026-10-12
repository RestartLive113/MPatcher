param(
    [string]$BaseLoaderPath,
    [string]$MPatchrMainPath,
    [string]$ReferenceDirectory,
    [string]$OriginalAssemblyPath,
    [string]$OutputPath,
    [ValidatePattern('^\d+\.\d+\.\d+(?:\.\d+)?$')]
    [string]$ProductVersion = '0.3.8',
    [switch]$SkipMPatcherBuild,
    [switch]$ExcludePlayerPresence
)

$ErrorActionPreference = 'Stop'
Set-StrictMode -Version Latest

$root = $PSScriptRoot
if ([string]::IsNullOrWhiteSpace($BaseLoaderPath)) {
    $BaseLoaderPath = Join-Path $root '..\..\reference\mpatcher-installer-v1.6\downloaded\PatchLoader.unpacked.dll'
}
if ([string]::IsNullOrWhiteSpace($MPatchrMainPath)) {
    $MPatchrMainPath = Join-Path $root 'build\MPatchrMain.dll'
}
if ([string]::IsNullOrWhiteSpace($OutputPath)) {
    $OutputPath = Join-Path $root 'build\__Internal.MPatcherFork.dll'
}

$buildRoot = [IO.Path]::GetFullPath((Join-Path $root 'build'))
$resolvedOutput = [IO.Path]::GetFullPath($OutputPath)
$allowedPrefix = $buildRoot.TrimEnd([IO.Path]::DirectorySeparatorChar, [IO.Path]::AltDirectorySeparatorChar) + [IO.Path]::DirectorySeparatorChar
if (-not $resolvedOutput.StartsWith($allowedPrefix, [StringComparison]::OrdinalIgnoreCase)) {
    throw "Safety check failed: test loader output must stay under $buildRoot"
}

$logDirectory = Join-Path $root 'logs'
$logPath = Join-Path $logDirectory 'test-loader-build.log'
$summaryPath = Join-Path $logDirectory 'test-loader-summary.json'
New-Item -ItemType Directory -Path $buildRoot -Force | Out-Null
New-Item -ItemType Directory -Path $logDirectory -Force | Out-Null
Set-Content -LiteralPath $logPath -Value '' -Encoding UTF8

function Write-BuildLog([string]$Message) {
    $line = '{0} [TEST-LOADER] {1}' -f [DateTime]::UtcNow.ToString('o'), $Message
    Write-Host $line
    Add-Content -LiteralPath $logPath -Value $line -Encoding UTF8
}

if (-not $SkipMPatcherBuild) {
    Write-BuildLog 'Building canonical MPatchrMain.dll before repacking.'
    $buildParameters = @{ OutputPath = $MPatchrMainPath; ProductVersion = $ProductVersion }
    if (-not [string]::IsNullOrWhiteSpace($ReferenceDirectory)) {
        $buildParameters.ReferenceDirectory = $ReferenceDirectory
    }
    if (-not [string]::IsNullOrWhiteSpace($OriginalAssemblyPath)) {
        $buildParameters.OriginalAssemblyPath = $OriginalAssemblyPath
    }
    if ($ExcludePlayerPresence) {
        $buildParameters.ExcludePlayerPresence = $true
    }
    $buildOutput = @(& (Join-Path $root 'Build.ps1') @buildParameters 2>&1)
    $buildExitCode = $LASTEXITCODE
    $buildOutput | ForEach-Object { Write-BuildLog "BUILD $_" }
    if ($buildExitCode -ne 0) { throw "MPatcher compilation failed ($buildExitCode); refusing to repack a previous DLL." }
}

$resolvedBaseLoader = (Resolve-Path -LiteralPath $BaseLoaderPath).Path
$resolvedMPatchrMain = (Resolve-Path -LiteralPath $MPatchrMainPath).Path
if ($resolvedBaseLoader.Equals($resolvedOutput, [StringComparison]::OrdinalIgnoreCase)) {
    throw 'Safety check failed: base loader and output path are identical.'
}

if (-not ('MPatcherFork.NativeResources' -as [type])) {
    Add-Type -TypeDefinition @'
using System;
using System.Runtime.InteropServices;

namespace MPatcherFork
{
    public static class NativeResources
    {
        public const uint LOAD_LIBRARY_AS_DATAFILE = 0x00000002;

        [DllImport("kernel32.dll", EntryPoint = "BeginUpdateResourceW", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr BeginUpdateResource(string fileName, [MarshalAs(UnmanagedType.Bool)] bool deleteExistingResources);

        [DllImport("kernel32.dll", EntryPoint = "UpdateResourceW", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UpdateResource(IntPtr update, string type, IntPtr name, ushort language, byte[] data, uint size);

        [DllImport("kernel32.dll", EntryPoint = "EndUpdateResourceW", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EndUpdateResource(IntPtr update, [MarshalAs(UnmanagedType.Bool)] bool discard);

        [DllImport("kernel32.dll", EntryPoint = "LoadLibraryExW", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr LoadLibraryEx(string fileName, IntPtr file, uint flags);

        [DllImport("kernel32.dll", EntryPoint = "FindResourceExW", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr FindResourceEx(IntPtr module, string type, IntPtr name, ushort language);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern uint SizeofResource(IntPtr module, IntPtr resourceInfo);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr LoadResource(IntPtr module, IntPtr resourceInfo);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr LockResource(IntPtr resourceData);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool FreeLibrary(IntPtr module);
    }
}
'@
}

$resourceType = 'BINARY'
$resourceId = [IntPtr]103
$resourceLanguage = [uint16]2048
$payload = [IO.File]::ReadAllBytes($resolvedMPatchrMain)

if (Test-Path -LiteralPath $resolvedOutput) {
    Remove-Item -LiteralPath $resolvedOutput -Force
}
Copy-Item -LiteralPath $resolvedBaseLoader -Destination $resolvedOutput
Write-BuildLog "Copied isolated base loader: $resolvedBaseLoader"

$update = [MPatcherFork.NativeResources]::BeginUpdateResource($resolvedOutput, $false)
if ($update -eq [IntPtr]::Zero) {
    throw "BeginUpdateResource failed with Win32 error $([Runtime.InteropServices.Marshal]::GetLastWin32Error())."
}

$ended = $false
try {
    if (-not [MPatcherFork.NativeResources]::UpdateResource(
        $update,
        $resourceType,
        $resourceId,
        $resourceLanguage,
        $payload,
        [uint32]$payload.Length
    )) {
        throw "UpdateResource failed with Win32 error $([Runtime.InteropServices.Marshal]::GetLastWin32Error())."
    }
    if (-not [MPatcherFork.NativeResources]::EndUpdateResource($update, $false)) {
        $ended = $true
        throw "EndUpdateResource failed with Win32 error $([Runtime.InteropServices.Marshal]::GetLastWin32Error())."
    }
    $ended = $true
}
finally {
    if (-not $ended) {
        [void][MPatcherFork.NativeResources]::EndUpdateResource($update, $true)
    }
}

# Verify the resource from the finished PE without executing the DLL.
$module = [MPatcherFork.NativeResources]::LoadLibraryEx(
    $resolvedOutput,
    [IntPtr]::Zero,
    [MPatcherFork.NativeResources]::LOAD_LIBRARY_AS_DATAFILE
)
if ($module -eq [IntPtr]::Zero) {
    throw "LoadLibraryEx(AS_DATAFILE) failed with Win32 error $([Runtime.InteropServices.Marshal]::GetLastWin32Error())."
}
try {
    $resourceInfo = [MPatcherFork.NativeResources]::FindResourceEx($module, $resourceType, $resourceId, $resourceLanguage)
    if ($resourceInfo -eq [IntPtr]::Zero) {
        throw "Resource BINARY/103/2048 was not found; Win32 error $([Runtime.InteropServices.Marshal]::GetLastWin32Error())."
    }
    $resourceSize = [MPatcherFork.NativeResources]::SizeofResource($module, $resourceInfo)
    $resourceHandle = [MPatcherFork.NativeResources]::LoadResource($module, $resourceInfo)
    $resourcePointer = [MPatcherFork.NativeResources]::LockResource($resourceHandle)
    if ($resourceSize -eq 0 -or $resourceHandle -eq [IntPtr]::Zero -or $resourcePointer -eq [IntPtr]::Zero) {
        throw "Reading resource BINARY/103/2048 failed with Win32 error $([Runtime.InteropServices.Marshal]::GetLastWin32Error())."
    }
    $resourceBytes = [byte[]]::new([int]$resourceSize)
    [Runtime.InteropServices.Marshal]::Copy($resourcePointer, $resourceBytes, 0, [int]$resourceSize)
}
finally {
    [void][MPatcherFork.NativeResources]::FreeLibrary($module)
}

$sha = [Security.Cryptography.SHA256]::Create()
try {
    $payloadHash = [BitConverter]::ToString($sha.ComputeHash($payload)).Replace('-', '')
    $resourceHash = [BitConverter]::ToString($sha.ComputeHash($resourceBytes)).Replace('-', '')
}
finally {
    $sha.Dispose()
}
if ($payloadHash -ne $resourceHash -or $payload.Length -ne $resourceBytes.Length) {
    throw "Embedded resource mismatch: input=$payloadHash/$($payload.Length), resource=$resourceHash/$($resourceBytes.Length)."
}

$summary = [ordered]@{
    created_utc = [DateTime]::UtcNow.ToString('o')
    base_loader = $resolvedBaseLoader
    base_loader_sha256 = (Get-FileHash -Algorithm SHA256 -LiteralPath $resolvedBaseLoader).Hash
    mpatchrmain = $resolvedMPatchrMain
    mpatchrmain_sha256 = $payloadHash
    output = $resolvedOutput
    output_length = (Get-Item -LiteralPath $resolvedOutput).Length
    output_sha256 = (Get-FileHash -Algorithm SHA256 -LiteralPath $resolvedOutput).Hash
    embedded_resource = 'BINARY/103/2048'
    embedded_length = $resourceBytes.Length
    embedded_sha256 = $resourceHash
    embedded_resource_verified = $true
    product_version = $ProductVersion
    player_presence_included = -not $ExcludePlayerPresence
    deployed = $false
    runtime_tested = $false
}
$summaryJson = $summary | ConvertTo-Json -Depth 4
$summaryJson | Set-Content -LiteralPath $summaryPath -Encoding UTF8
Write-BuildLog "Verified BINARY/103/2048 length=$($resourceBytes.Length) sha256=$resourceHash"
Write-BuildLog "Created isolated test loader: $resolvedOutput"
$summaryJson
