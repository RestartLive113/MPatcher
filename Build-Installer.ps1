[CmdletBinding()]
param(
    [ValidatePattern('^\d+\.\d+\.\d+(?:\.\d+)?$')]
    [string]$Version = '0.3.8',
	[ValidateSet('Stable', 'Alpha')]
	[string]$UpdateChannel = 'Stable',
    [string]$ReferenceDirectory,
    [string]$OriginalAssemblyPath,
    [string]$BaseLoaderPath,
    [string]$PreparedAssemblyPath,
    [string]$PreparedLoaderPath,
    [ValidatePattern('^[A-Fa-f0-9]{64}$')][string]$ExpectedPreparedLoaderSha256,
    [string]$ScaleModuleDirectory,
    [switch]$PrecompiledPatches,
    [ValidatePattern('^[A-Za-z0-9-]*$')][string]$ArtifactSuffix = '',
    [ValidatePattern('^[A-Za-z0-9_.-]+/[A-Za-z0-9_.-]+$')]
    [string]$GitHubRepository = 'RestartLive113/MPatcher',
    [switch]$ExcludePlayerPresence
)

$ErrorActionPreference = 'Stop'
$root = $PSScriptRoot
$loaderBuilder = Join-Path $root 'Build-TestLoader.ps1'
$payload = Join-Path $root 'build\__Internal.MPatcherFork.dll'
$installerSource = Join-Path $root 'Installer'
$installerBuild = Join-Path $root 'build\installer'
$watchdogBuild = Join-Path $root 'build\watchdog'
$watchdogSource = Join-Path $installerSource 'CrashWatchdog.cs'
$watchdog = Join-Path $watchdogBuild 'MPatcherCrashWatchdog.exe'
$networkAssistSource = Join-Path $root 'NetworkAssist'
$networkAssistVendor = Join-Path $root 'third-party\zapret-legacy-v1'
$networkAssistBuild = Join-Path $root 'build\network-assist'
$networkAssistHelper = Join-Path $networkAssistBuild 'MPatcherNetworkAssist.exe'
$networkAssistChecks = Join-Path $networkAssistBuild 'LegacyZapretChecks.exe'
$networkAssistPayloadSource = Join-Path $networkAssistBuild 'AssistPayload.Generated.cs'
$networkAssistPackageSource = Join-Path $installerBuild 'AssistPackage.Generated.cs'
$packageManifest = Join-Path $installerBuild 'Installer.Package.manifest'
if ($ArtifactSuffix) {
    $installerBuild = Join-Path $root ('build\installer-' + $ArtifactSuffix)
    $payload = Join-Path $installerBuild '__Internal.MPatcherFork.dll'
    $watchdogBuild = Join-Path $installerBuild 'watchdog'
    $watchdog = Join-Path $watchdogBuild 'MPatcherCrashWatchdog.exe'
    $networkAssistBuild = Join-Path $installerBuild 'network-assist'
    $networkAssistHelper = Join-Path $networkAssistBuild 'MPatcherNetworkAssist.exe'
    $networkAssistChecks = Join-Path $networkAssistBuild 'LegacyZapretChecks.exe'
    $networkAssistPayloadSource = Join-Path $networkAssistBuild 'AssistPayload.Generated.cs'
    $networkAssistPackageSource = Join-Path $installerBuild 'AssistPackage.Generated.cs'
    $packageManifest = Join-Path $installerBuild 'Installer.Package.manifest'
}
if ($PrecompiledPatches -and (-not $PreparedAssemblyPath -or (-not $BaseLoaderPath -and -not $PreparedLoaderPath))) {
    throw 'Precompiled package requires matching prepared payload and loader'
}
if ($PreparedLoaderPath -and -not $ExpectedPreparedLoaderSha256) { throw 'Exact prepared loader requires an expected SHA-256.' }
if ($ExpectedPreparedLoaderSha256 -and -not $PreparedLoaderPath) { throw 'Expected prepared loader SHA-256 requires its path.' }
$distRoot = Join-Path $root 'dist'
$versionComponentCount = $Version.Split('.').Length
if ($UpdateChannel -eq 'Stable' -and $versionComponentCount -ne 3) {
    throw 'Stable packages must use a three-part version such as 0.3.8.'
}
if ($UpdateChannel -eq 'Alpha' -and $versionComponentCount -ne 4) {
    throw 'Alpha packages must use a four-part version such as 0.3.9.1.'
}
$packageName = 'MPatcher-v' + $Version + $(if ($UpdateChannel -eq 'Alpha') { '-Alpha' } else { '' })
if ($ArtifactSuffix) { $packageName += '-' + $ArtifactSuffix }
$packageDirectory = Join-Path $distRoot $packageName
if ($ArtifactSuffix -and (Test-Path -LiteralPath $packageDirectory)) { throw 'Named artifact already exists; preserving it' }
$bootstrapExe = Join-Path $packageDirectory 'MPatcherInstaller.exe'
$packageInstallerExe = Join-Path $packageDirectory 'MPatcherPackage.exe'
$zipPath = Join-Path $distRoot ($packageName + '.zip')
$csc = Join-Path $env:WINDIR 'Microsoft.NET\Framework\v4.0.30319\csc.exe'
$csc64 = Join-Path $env:WINDIR 'Microsoft.NET\Framework64\v4.0.30319\csc.exe'

$resolvedDistRoot = [IO.Path]::GetFullPath($distRoot)
$resolvedPackageDirectory = [IO.Path]::GetFullPath($packageDirectory)
$resolvedZipPath = [IO.Path]::GetFullPath($zipPath)
if (-not $resolvedPackageDirectory.StartsWith($resolvedDistRoot + [IO.Path]::DirectorySeparatorChar, [StringComparison]::OrdinalIgnoreCase)) {
    throw "Refusing package path outside dist: $resolvedPackageDirectory"
}
if (-not [string]::Equals([IO.Path]::GetDirectoryName($resolvedZipPath), $resolvedDistRoot, [StringComparison]::OrdinalIgnoreCase)) {
    throw "Refusing archive path outside dist: $resolvedZipPath"
}

if (-not (Test-Path -LiteralPath $csc)) {
    throw "C# compiler not found: $csc"
}
if (-not (Test-Path -LiteralPath $csc64)) {
    throw "64-bit C# compiler not found: $csc64"
}

$networkAssistManifest = Join-Path $networkAssistVendor 'files.sha256.json'
foreach ($required in @(
    $networkAssistManifest,
    (Join-Path $networkAssistSource 'AssistPolicy.cs'),
    (Join-Path $networkAssistSource 'AssistService.cs'),
    (Join-Path $installerSource 'NetworkAssistSetup.cs'),
    (Join-Path $root 'tools\LegacyZapretChecks.cs')
)) {
    if (-not (Test-Path -LiteralPath $required -PathType Leaf)) {
        throw "Network Assist release input is missing: $required"
    }
}

New-Item -ItemType Directory -Path $networkAssistBuild -Force | Out-Null
$networkAssistPayloadFiles = @(Get-Content -LiteralPath $networkAssistManifest -Raw | ConvertFrom-Json)
foreach ($file in $networkAssistPayloadFiles) {
    $path = Join-Path $networkAssistVendor $file.Name
    if (-not (Test-Path -LiteralPath $path -PathType Leaf)) {
        throw "Pinned Network Assist file is missing: $path"
    }
    $actualHash = (Get-FileHash -LiteralPath $path -Algorithm SHA256).Hash
    if (-not [string]::Equals($actualHash, $file.Hash, [StringComparison]::OrdinalIgnoreCase)) {
        throw "Pinned Network Assist file changed: $($file.Name)"
    }
}
$networkAssistPayloadCode = 'namespace MPatcher.NetworkAssist { internal static class AssistPayload { internal static readonly string[] Files = new string[] { ' + (($networkAssistPayloadFiles | ForEach-Object { '"' + $_.Name + '|' + $_.Hash + '"' }) -join ',') + ' }; } }'
[IO.File]::WriteAllText($networkAssistPayloadSource, $networkAssistPayloadCode, [Text.UTF8Encoding]::new($false))

$networkAssistArguments = @(
    '/nologo',
    '/target:winexe',
    '/platform:x64',
    '/optimize+',
    '/debug-',
    '/warn:4',
    '/codepage:65001',
    ('/out:' + $networkAssistHelper),
    '/reference:System.dll',
    '/reference:System.Core.dll',
    '/reference:System.ServiceProcess.dll',
    (Join-Path $networkAssistSource 'AssistPolicy.cs'),
    (Join-Path $networkAssistSource 'AssistService.cs'),
    $networkAssistPayloadSource
)
& $csc64 @networkAssistArguments
if ($LASTEXITCODE -ne 0) {
    throw "Network Assist service compiler failed with exit code $LASTEXITCODE"
}

$networkAssistCheckArguments = @(
    '/nologo',
    '/target:exe',
    '/platform:x64',
    '/optimize+',
    '/debug-',
    '/warn:4',
    ('/out:' + $networkAssistChecks),
    (Join-Path $networkAssistSource 'AssistPolicy.cs'),
    (Join-Path $root 'tools\LegacyZapretChecks.cs')
)
& $csc64 @networkAssistCheckArguments
if ($LASTEXITCODE -ne 0) {
    throw "Network Assist checks compiler failed with exit code $LASTEXITCODE"
}
Copy-Item -LiteralPath (Join-Path $networkAssistVendor 'WinDivert.dll') -Destination $networkAssistBuild -Force
& $networkAssistChecks
if ($LASTEXITCODE -ne 0) {
    throw "Network Assist policy checks failed with exit code $LASTEXITCODE"
}

New-Item -ItemType Directory -Path $watchdogBuild -Force | Out-Null
$watchdogArguments = @(
    '/nologo',
    '/target:winexe',
    '/platform:anycpu',
    '/optimize+',
    '/debug-',
    '/warn:4',
    '/codepage:65001',
    ('/out:' + $watchdog),
    '/reference:System.dll',
    '/reference:System.Core.dll',
    '/reference:System.Xml.dll',
    $watchdogSource
)
& $csc @watchdogArguments
if ($LASTEXITCODE -ne 0) {
    throw "Crash watchdog compiler failed with exit code $LASTEXITCODE"
}
$watchdogItem = Get-Item -LiteralPath $watchdog
$watchdogHash = (Get-FileHash -LiteralPath $watchdog -Algorithm SHA256).Hash

$loaderParameters = @{ OutputPath = $payload }
$loaderParameters.ProductVersion = $Version
if (-not [string]::IsNullOrWhiteSpace($PreparedAssemblyPath)) {
    $PreparedAssemblyPath = (Resolve-Path -LiteralPath $PreparedAssemblyPath).Path
    & (Join-Path $root 'Verify.ps1') -AssemblyPath $PreparedAssemblyPath -ExpectedProductVersion $Version -ExpectPlayerPresenceExcluded:$ExcludePlayerPresence -ExpectPrecompiledPatches:$PrecompiledPatches
    if ($LASTEXITCODE -ne 0) { throw 'Prepared assembly verification failed' }
    $loaderParameters.MPatchrMainPath = $PreparedAssemblyPath
    $loaderParameters.SkipMPatcherBuild = $true
}
if (-not [string]::IsNullOrWhiteSpace($ReferenceDirectory)) {
    $loaderParameters.ReferenceDirectory = $ReferenceDirectory
}
if (-not [string]::IsNullOrWhiteSpace($OriginalAssemblyPath)) {
    $loaderParameters.OriginalAssemblyPath = $OriginalAssemblyPath
}
if (-not [string]::IsNullOrWhiteSpace($BaseLoaderPath)) {
    $loaderParameters.BaseLoaderPath = $BaseLoaderPath
}
if ($ExcludePlayerPresence) {
    $loaderParameters.ExcludePlayerPresence = $true
}
if ($PreparedLoaderPath) {
    $PreparedLoaderPath = (Resolve-Path -LiteralPath $PreparedLoaderPath).Path
    if ((Get-FileHash -LiteralPath $PreparedLoaderPath).Hash -ne $ExpectedPreparedLoaderSha256) { throw 'Prepared loader differs from the accepted build.' }
    New-Item -ItemType Directory -Path (Split-Path -Parent $payload) -Force | Out-Null
    Copy-Item -LiteralPath $PreparedLoaderPath -Destination $payload -Force
    if ((Get-FileHash -LiteralPath $payload).Hash -ne $ExpectedPreparedLoaderSha256) { throw 'Exact loader copy verification failed.' }
} else {
    & $loaderBuilder @loaderParameters
    if ($LASTEXITCODE -ne 0) {
        throw "Build-TestLoader.ps1 failed with exit code $LASTEXITCODE"
    }
}

if ($PrecompiledPatches) {
    if (-not ('MPatcherFork.NativeResources' -as [type])) {
        Add-Type -TypeDefinition @'
using System;
using System.Runtime.InteropServices;
namespace MPatcherFork { public static class NativeResources {
[DllImport("kernel32.dll",EntryPoint="LoadLibraryExW",CharSet=CharSet.Unicode)] public static extern IntPtr LoadLibraryEx(string path,IntPtr file,uint flags);
[DllImport("kernel32.dll",EntryPoint="FindResourceExW",CharSet=CharSet.Unicode)] public static extern IntPtr FindResourceEx(IntPtr module,string type,IntPtr name,ushort language);
[DllImport("kernel32.dll")] public static extern uint SizeofResource(IntPtr module,IntPtr resource);
[DllImport("kernel32.dll")] public static extern IntPtr LoadResource(IntPtr module,IntPtr resource);
[DllImport("kernel32.dll")] public static extern IntPtr LockResource(IntPtr resource);
[DllImport("kernel32.dll")] public static extern bool FreeLibrary(IntPtr module);
} }
'@
    }
    $expectedGameResource = Join-Path (Split-Path -Parent $PreparedAssemblyPath) 'game.resource.bin'
    $expected = [IO.File]::ReadAllBytes($expectedGameResource)
    $nativeModule = [MPatcherFork.NativeResources]::LoadLibraryEx($payload,[IntPtr]::Zero,[uint32]2)
    if ($nativeModule -eq [IntPtr]::Zero) { throw 'Cannot inspect precompiled loader' }
    try {
        $resource = [MPatcherFork.NativeResources]::FindResourceEx($nativeModule,'BINARY',[IntPtr]105,[uint16]2048)
        if ($resource -eq [IntPtr]::Zero) { throw 'Missing precompiled game resource' }
        $size = [MPatcherFork.NativeResources]::SizeofResource($nativeModule,$resource)
        if ($size -ne $expected.Length) { throw 'Prepared game/loader size mismatch' }
        $pointer = [MPatcherFork.NativeResources]::LockResource([MPatcherFork.NativeResources]::LoadResource($nativeModule,$resource))
        $bytes = [byte[]]::new($size)
        [Runtime.InteropServices.Marshal]::Copy($pointer,$bytes,0,$size)
        $sha = [Security.Cryptography.SHA256]::Create()
        try {
            if ([Convert]::ToBase64String($sha.ComputeHash($bytes)) -ne [Convert]::ToBase64String($sha.ComputeHash($expected))) { throw 'Prepared game/loader hash mismatch' }
        } finally { $sha.Dispose() }
    } finally { [void][MPatcherFork.NativeResources]::FreeLibrary($nativeModule) }
    if ($PreparedLoaderPath) {
        $expectedCore = [IO.File]::ReadAllBytes($PreparedAssemblyPath)
        $nativeModule = [MPatcherFork.NativeResources]::LoadLibraryEx($payload,[IntPtr]::Zero,[uint32]2)
        if ($nativeModule -eq [IntPtr]::Zero) { throw 'Cannot inspect prepared loader core resource.' }
        try {
            $resource = [MPatcherFork.NativeResources]::FindResourceEx($nativeModule,'BINARY',[IntPtr]103,[uint16]2048)
            if ($resource -eq [IntPtr]::Zero) { throw 'Missing prepared core resource.' }
            $size = [MPatcherFork.NativeResources]::SizeofResource($nativeModule,$resource)
            if ($size -ne $expectedCore.Length) { throw 'Prepared core/loader size mismatch.' }
            $pointer = [MPatcherFork.NativeResources]::LockResource([MPatcherFork.NativeResources]::LoadResource($nativeModule,$resource))
            $bytes = [byte[]]::new($size)
            [Runtime.InteropServices.Marshal]::Copy($pointer,$bytes,0,$size)
            $sha = [Security.Cryptography.SHA256]::Create()
            try {
                if ([Convert]::ToBase64String($sha.ComputeHash($bytes)) -ne [Convert]::ToBase64String($sha.ComputeHash($expectedCore))) { throw 'Prepared core/loader hash mismatch.' }
            } finally { $sha.Dispose() }
        } finally { [void][MPatcherFork.NativeResources]::FreeLibrary($nativeModule) }
    }
}
$payloadItem = Get-Item -LiteralPath $payload
$payloadHash = (Get-FileHash -LiteralPath $payload -Algorithm SHA256).Hash

$scaleModule = $null
$scaleModuleManifest = $null
$scaleModuleHash = ''
$scaleModuleManifestHash = ''
if ($ScaleModuleDirectory) {
    $ScaleModuleDirectory = (Resolve-Path -LiteralPath $ScaleModuleDirectory).Path
    $scaleModule = Get-Item -LiteralPath (Join-Path $ScaleModuleDirectory 'MPatcher.ScaleRuntime.dll')
    $scaleModuleManifest = Get-Item -LiteralPath (Join-Path $ScaleModuleDirectory 'MPatcher.ScaleRuntime.manifest.ini')
    $scaleModuleHash = (Get-FileHash -LiteralPath $scaleModule.FullName).Hash
    $scaleModuleManifestHash = (Get-FileHash -LiteralPath $scaleModuleManifest.FullName).Hash
    $moduleMetadata = @{}
    foreach ($line in [IO.File]::ReadAllLines($scaleModuleManifest.FullName)) {
        if ($line -notmatch '^([^=]+)=(.*)$' -or $moduleMetadata.ContainsKey($Matches[1])) { throw 'Invalid Scale module sidecar.' }
        $moduleMetadata.Add($Matches[1],$Matches[2])
    }
    if ($moduleMetadata.HostApiVersion -ne '1' -or $moduleMetadata.ModuleAssemblyName -ne 'MPatcher.ScaleRuntime' -or $moduleMetadata.PayloadSha256 -ne $scaleModuleHash -or $moduleMetadata.ProductVersion -ne $Version) { throw 'Scale module sidecar/API/version/hash mismatch.' }
    $moduleIdentity = [Reflection.AssemblyName]::GetAssemblyName($scaleModule.FullName)
    if ($moduleIdentity.Name -ne 'MPatcher.ScaleRuntime') { throw 'Wrong Scale module assembly.' }
    if ($PreparedAssemblyPath) {
        $coreIdentity = [Reflection.AssemblyName]::GetAssemblyName($PreparedAssemblyPath)
        if ([Convert]::ToBase64String($moduleIdentity.GetPublicKey()) -ne [Convert]::ToBase64String($coreIdentity.GetPublicKey())) { throw 'Scale module public key differs from its core.' }
    }
}

New-Item -ItemType Directory -Path $installerBuild -Force | Out-Null
if (Test-Path -LiteralPath $resolvedPackageDirectory) {
    Remove-Item -LiteralPath $resolvedPackageDirectory -Recurse -Force
}
New-Item -ItemType Directory -Path $resolvedPackageDirectory -Force | Out-Null

$payloadTemplate = Get-Content -LiteralPath (Join-Path $installerSource 'PayloadInfo.cs') -Raw
$payloadSource = $payloadTemplate.Replace('__GENERATED_VERSION__', $Version).Replace('__GENERATED_SHA256__', $payloadHash).Replace('__GENERATED_WATCHDOG_SHA256__', $watchdogHash).Replace('internal const long Length = 0;', 'internal const long Length = ' + $payloadItem.Length + 'L;').Replace('internal const long WatchdogLength = 0;', 'internal const long WatchdogLength = ' + $watchdogItem.Length + 'L;')
$payloadSource = $payloadSource.Replace('__GENERATED_SCALE_MODULE_SHA256__',$scaleModuleHash).Replace('__GENERATED_SCALE_MODULE_MANIFEST_SHA256__',$scaleModuleManifestHash)
if ($scaleModule) {
    $payloadSource = $payloadSource.Replace('internal const bool ScaleModuleIncluded = false;','internal const bool ScaleModuleIncluded = true;').Replace('internal const long ScaleModuleLength = 0;','internal const long ScaleModuleLength = ' + $scaleModule.Length + 'L;').Replace('internal const long ScaleModuleManifestLength = 0;','internal const long ScaleModuleManifestLength = ' + $scaleModuleManifest.Length + 'L;')
}
$generatedPayloadSource = Join-Path $installerBuild 'PayloadInfo.Generated.cs'
[IO.File]::WriteAllText($generatedPayloadSource, $payloadSource, [Text.UTF8Encoding]::new($false))

$parsedVersion = [Version]$Version
$assemblyRevision = [Math]::Max(0, $parsedVersion.Revision)
$assemblyVersion = '{0}.{1}.{2}.{3}' -f $parsedVersion.Major, $parsedVersion.Minor, [Math]::Max(0, $parsedVersion.Build), $assemblyRevision
$assemblyTemplate = Get-Content -LiteralPath (Join-Path $installerSource 'AssemblyInfo.cs') -Raw
$assemblySource = $assemblyTemplate.Replace('__GENERATED_ASSEMBLY_VERSION__', $assemblyVersion).Replace('__GENERATED_VERSION__', $Version)
$generatedAssemblySource = Join-Path $installerBuild 'AssemblyInfo.Generated.cs'
[IO.File]::WriteAllText($generatedAssemblySource, $assemblySource, [Text.UTF8Encoding]::new($false))

$networkAssistRuntimeFiles = @(
    $networkAssistPayloadFiles | ForEach-Object { Get-Item -LiteralPath (Join-Path $networkAssistVendor $_.Name) }
)
$networkAssistRuntimeFiles += Get-Item -LiteralPath $networkAssistHelper
$networkAssistRuntimeFiles += @(
    Get-ChildItem -LiteralPath $networkAssistVendor -File |
        Where-Object { $_.Name -match 'LICENSE|NOTICE|source|src\.tar|upstream|provenance' }
)
$networkAssistRuntimeFiles = @($networkAssistRuntimeFiles | Sort-Object FullName -Unique)
$networkAssistEntries = @($networkAssistRuntimeFiles | ForEach-Object {
    '"' + $_.Name + '|' + (Get-FileHash -LiteralPath $_.FullName -Algorithm SHA256).Hash + '"'
})
$networkAssistHelperHash = (Get-FileHash -LiteralPath $networkAssistHelper -Algorithm SHA256).Hash
$networkAssistPackageCode = 'namespace MachineCraftMPatcherInstaller { internal static class AssistPackage { internal const string Id="v1-' + $networkAssistHelperHash.Substring(0, 16) + '"; internal static readonly string[] Files = new string[] { ' + ($networkAssistEntries -join ',') + ' }; } }'
[IO.File]::WriteAllText($networkAssistPackageSource, $networkAssistPackageCode, [Text.UTF8Encoding]::new($false))

$packageManifestText = (Get-Content -LiteralPath (Join-Path $installerSource 'Installer.manifest') -Raw).Replace('level="asInvoker"', 'level="requireAdministrator"')
if (-not $packageManifestText.Contains('level="requireAdministrator"')) {
    throw 'Failed to create the elevated package installer manifest.'
}
[IO.File]::WriteAllText($packageManifest, $packageManifestText, [Text.UTF8Encoding]::new($false))

$compilerArguments = @(
    '/nologo',
    '/target:winexe',
    '/platform:x64',
    '/optimize+',
    '/debug-',
    '/warn:4',
    '/codepage:65001',
	'/define:MPATCHER_ZAPRET_PACKAGE',
	('/out:' + $packageInstallerExe),
    ('/win32manifest:' + $packageManifest),
    ('/resource:' + $payload + ',MachineCraftMPatcherInstaller.Payload.__Internal.dll'),
    ('/resource:' + $watchdog + ',MachineCraftMPatcherInstaller.Payload.MPatcherCrashWatchdog.exe'),
    '/reference:System.dll',
    '/reference:System.Core.dll',
    '/reference:System.Drawing.dll',
    '/reference:System.ServiceProcess.dll',
    '/reference:System.Windows.Forms.dll',
    (Join-Path $installerSource 'Program.cs'),
    (Join-Path $installerSource 'MainForm.cs'),
    (Join-Path $installerSource 'Localization.cs'),
    (Join-Path $installerSource 'InstallerEngine.cs'),
    (Join-Path $installerSource 'AutoUpdateRunner.cs'),
    (Join-Path $installerSource 'GameProcessController.cs'),
    (Join-Path $installerSource 'InstallerResultProtocol.cs'),
    (Join-Path $installerSource 'NetworkAssistSetup.cs'),
    (Join-Path $networkAssistSource 'AssistPolicy.cs'),
    $generatedPayloadSource,
    $generatedAssemblySource,
    $networkAssistPackageSource
)

foreach ($file in $networkAssistRuntimeFiles) {
    $compilerArguments += '/resource:' + $file.FullName + ',MachineCraftMPatcherInstaller.NetworkAssist.' + $file.Name
}
if ($scaleModule) {
    $compilerArguments += '/resource:' + $scaleModule.FullName + ',MachineCraftMPatcherInstaller.Payload.MPatcher.ScaleRuntime.dll'
    $compilerArguments += '/resource:' + $scaleModuleManifest.FullName + ',MachineCraftMPatcherInstaller.Payload.MPatcher.ScaleRuntime.manifest.ini'
}

& $csc64 @compilerArguments
if ($LASTEXITCODE -ne 0) {
    throw "Package installer compiler failed with exit code $LASTEXITCODE"
}

$packageInstallerItem = Get-Item -LiteralPath $packageInstallerExe
$packageInstallerHash = (Get-FileHash -LiteralPath $packageInstallerExe -Algorithm SHA256).Hash
$updateManifest = Join-Path $packageDirectory 'MPatcherUpdate.ini'
$releaseTag = if ($UpdateChannel -eq 'Alpha') { 'alpha' } else { 'v' + $Version }
$installerUrl = 'https://github.com/' + $GitHubRepository + '/releases/download/' + $releaseTag + '/MPatcherPackage.exe'
$updateManifestLines = @(
    'Format=1',
    ('Version=' + $Version),
    ('InstallerUrl=' + $installerUrl),
    ('InstallerSha256=' + $packageInstallerHash),
    ('InstallerLength=' + $packageInstallerItem.Length)
)
[IO.File]::WriteAllLines($updateManifest, $updateManifestLines, [Text.UTF8Encoding]::new($false))

$manifestUrl = if ($UpdateChannel -eq 'Alpha') {
    'https://github.com/' + $GitHubRepository + '/releases/download/alpha/MPatcherUpdate.ini'
} else {
    'https://github.com/' + $GitHubRepository + '/releases/latest/download/MPatcherUpdate.ini'
}
$repositoryReleasePath = '/' + $GitHubRepository + '/releases/download/'
$bootstrapInfoTemplate = Get-Content -LiteralPath (Join-Path $installerSource 'BootstrapInfo.cs') -Raw
$bootstrapInfoSource = $bootstrapInfoTemplate.Replace('__GENERATED_MANIFEST_URL__', $manifestUrl).Replace('__GENERATED_REPOSITORY_RELEASE_PATH__', $repositoryReleasePath)
$generatedBootstrapInfoSource = Join-Path $installerBuild 'BootstrapInfo.Generated.cs'
[IO.File]::WriteAllText($generatedBootstrapInfoSource, $bootstrapInfoSource, [Text.UTF8Encoding]::new($false))

$bootstrapAssemblyTemplate = Get-Content -LiteralPath (Join-Path $installerSource 'BootstrapAssemblyInfo.cs') -Raw
$bootstrapAssemblySource = $bootstrapAssemblyTemplate.Replace('__GENERATED_ASSEMBLY_VERSION__', $assemblyVersion).Replace('__GENERATED_VERSION__', $Version)
$generatedBootstrapAssemblySource = Join-Path $installerBuild 'BootstrapAssemblyInfo.Generated.cs'
[IO.File]::WriteAllText($generatedBootstrapAssemblySource, $bootstrapAssemblySource, [Text.UTF8Encoding]::new($false))

$bootstrapCompilerArguments = @(
    '/nologo',
    '/target:winexe',
    '/platform:anycpu',
    '/optimize+',
    '/debug-',
    '/warn:4',
    '/codepage:65001',
    ('/out:' + $bootstrapExe),
    # The bootstrapper must already be elevated before it starts the downloaded
    # requireAdministrator package. This gives the user one UAC prompt at
    # bootstrap startup and avoids ERROR_ELEVATION_REQUIRED in the GUI worker.
    ('/win32manifest:' + $packageManifest),
    '/reference:System.dll',
    '/reference:System.Core.dll',
    '/reference:System.Drawing.dll',
    '/reference:System.Windows.Forms.dll',
    (Join-Path $installerSource 'BootstrapProgram.cs'),
    (Join-Path $installerSource 'BootstrapForm.cs'),
    (Join-Path $installerSource 'BootstrapClient.cs'),
    (Join-Path $installerSource 'BootstrapManifest.cs'),
    (Join-Path $installerSource 'BootstrapGameLocator.cs'),
    (Join-Path $installerSource 'Localization.cs'),
    (Join-Path $installerSource 'GameProcessController.cs'),
    (Join-Path $installerSource 'InstallerResultProtocol.cs'),
    $generatedBootstrapInfoSource,
    $generatedBootstrapAssemblySource
)

& $csc @bootstrapCompilerArguments
if ($LASTEXITCODE -ne 0) {
    throw "Universal installer compiler failed with exit code $LASTEXITCODE"
}

$bootstrapItem = Get-Item -LiteralPath $bootstrapExe
$bootstrapHash = (Get-FileHash -LiteralPath $bootstrapExe -Algorithm SHA256).Hash

$readmeName = if($UpdateChannel -eq 'Alpha'){'DISTRIBUTION-README-ALPHA.txt'}else{'DISTRIBUTION-README.txt'}
Copy-Item -LiteralPath (Join-Path $installerSource $readmeName) -Destination (Join-Path $packageDirectory 'README.txt')
Copy-Item -LiteralPath (Join-Path $installerSource 'THIRD-PARTY-NOTICES.txt') -Destination (Join-Path $packageDirectory 'THIRD-PARTY-NOTICES.txt')

$checksumLines = Get-ChildItem -LiteralPath $packageDirectory -File | Sort-Object Name | ForEach-Object {
    '{0}  {1}' -f (Get-FileHash -LiteralPath $_.FullName -Algorithm SHA256).Hash, $_.Name
}
[IO.File]::WriteAllLines((Join-Path $packageDirectory 'SHA256SUMS.txt'), $checksumLines, [Text.UTF8Encoding]::new($false))

if (Test-Path -LiteralPath $resolvedZipPath) {
    Remove-Item -LiteralPath $resolvedZipPath -Force
}
Compress-Archive -Path (Join-Path $resolvedPackageDirectory '*') -DestinationPath $resolvedZipPath -CompressionLevel Optimal

$result = [ordered]@{
    created_utc = [DateTime]::UtcNow.ToString('o')
    version = $Version
	update_channel = $UpdateChannel
    payload = $payload
    payload_length = $payloadItem.Length
    payload_sha256 = $payloadHash
    watchdog = $watchdog
    watchdog_length = $watchdogItem.Length
    watchdog_sha256 = $watchdogHash
    network_assist_included = $true
    network_assist_default_enabled = $false
    network_assist_helper = $networkAssistHelper
    network_assist_helper_sha256 = $networkAssistHelperHash
    network_assist_runtime_files = @($networkAssistRuntimeFiles.Name)
    installer = $bootstrapExe
    installer_length = $bootstrapItem.Length
    installer_sha256 = $bootstrapHash
    package_installer = $packageInstallerExe
    package_installer_length = $packageInstallerItem.Length
    package_installer_sha256 = $packageInstallerHash
    update_manifest = $updateManifest
    update_manifest_sha256 = (Get-FileHash -LiteralPath $updateManifest -Algorithm SHA256).Hash
    update_manifest_url = $manifestUrl
    package = $zipPath
    package_length = (Get-Item -LiteralPath $zipPath).Length
    package_sha256 = (Get-FileHash -LiteralPath $zipPath -Algorithm SHA256).Hash
    player_presence_included = -not $ExcludePlayerPresence
    precompiled_patches = [bool]$PrecompiledPatches
    exact_prepared_loader = [bool]$PreparedLoaderPath
    scale_module_included = [bool]$scaleModule
    scale_module_sha256 = $scaleModuleHash
    scale_module_manifest_sha256 = $scaleModuleManifestHash
    scale_module_host_api_version = $(if ($scaleModule) { 1 } else { $null })
    artifact_suffix = $ArtifactSuffix
    deployed = $false
    runtime_tested = $false
}
$resultJson = $result | ConvertTo-Json -Depth 4
$releaseReport = Join-Path $installerBuild ('release-' + $packageName + '.json')
[IO.File]::WriteAllText($releaseReport, $resultJson, [Text.UTF8Encoding]::new($false))
$resultJson
