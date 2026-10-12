param(
    [string]$ReferenceDirectory,
    [string]$OriginalAssemblyPath,
    [string]$OutputPath,
    [ValidatePattern('^\d+\.\d+\.\d+(?:\.\d+)?$')]
    [string]$ProductVersion = '0.3.8',
    [switch]$ExcludePlayerPresence
)

$ErrorActionPreference = 'Stop'
Set-StrictMode -Version Latest

$root = $PSScriptRoot
if ([string]::IsNullOrWhiteSpace($ReferenceDirectory)) {
    $ReferenceDirectory = Join-Path $root '..\..\reference\mpatcher-installer-v1.6\mpatchrmain-recovery\references-patched-game'
}
if ([string]::IsNullOrWhiteSpace($OriginalAssemblyPath)) {
    $OriginalAssemblyPath = Join-Path $root '..\..\reference\mpatcher-installer-v1.6\mpatchrmain-recovery\de4dot-cex-default-keep-types\MPatchrMain.deobfuscated.dll'
}
if ([string]::IsNullOrWhiteSpace($OutputPath)) {
    $OutputPath = Join-Path $root 'build\MPatchrMain.dll'
}

$buildDirectory = Split-Path -Parent ([IO.Path]::GetFullPath($OutputPath))
$logDirectory = Join-Path $root 'logs'
New-Item -ItemType Directory -Path $buildDirectory -Force | Out-Null
New-Item -ItemType Directory -Path $logDirectory -Force | Out-Null

$builder = Join-Path $root 'tools\Build-DecompiledMPatcher.ps1'
$key = Join-Path $root 'signing\MPatchrMain.public.snk'
$roslynDirectory = Join-Path $buildDirectory 'roslyn'
New-Item -ItemType Directory -Path $roslynDirectory -Force | Out-Null
$roslynOutput = Join-Path $roslynDirectory ([IO.Path]::GetFileName($OutputPath))
$generatedDirectory = Join-Path $buildDirectory 'generated'
New-Item -ItemType Directory -Path $generatedDirectory -Force | Out-Null
$releaseVersionSource = Join-Path $generatedDirectory 'MPatcherReleaseVersion.g.cs'
$releaseVersionCode = "using System.Reflection;`r`n[assembly: AssemblyInformationalVersion(`"$ProductVersion`")]`r`n"
[IO.File]::WriteAllText($releaseVersionSource, $releaseVersionCode, [Text.UTF8Encoding]::new($false))
$compilerSummaryLines = @(& $builder `
    -ProjectDirectory $root `
    -ReferenceDirectory $ReferenceDirectory `
    -OutputPath $roslynOutput `
    -ResponsePath (Join-Path $buildDirectory 'csc.rsp') `
    -LogPath (Join-Path $logDirectory 'compiler.log') `
    -EmbeddedResourceDirectory (Join-Path $root 'resources') `
    -PublicKeyFile $key `
    -AdditionalSourceFiles @($releaseVersionSource) `
    -ExcludePlayerPresence:$ExcludePlayerPresence)
$compilerExitCode = $LASTEXITCODE
if ($compilerExitCode -ne 0) {
    $compilerSummaryLines
    exit $compilerExitCode
}
$compilerSummary = (($compilerSummaryLines -join "`n") | ConvertFrom-Json)

# C# cannot emit the '$', '<' and '>' characters used by the obfuscated CLR
# identifiers, and the decompiler omitted five WidgetController MethodImpl rows.
# Restore both pieces of original metadata after compilation.
$restorer = Join-Path $root 'tools\bin\Restore-MPatcherMetadataNames.exe'
$originalAssembly = (Resolve-Path -LiteralPath $OriginalAssemblyPath).Path
$finalOutput = [IO.Path]::GetFullPath($OutputPath)
$restorerLines = @(& $restorer $roslynOutput $finalOutput $originalAssembly 2>&1)
$restorerExitCode = $LASTEXITCODE
$restorerLog = Join-Path $logDirectory 'metadata-name-restoration.log'
$restorerLines | Set-Content -LiteralPath $restorerLog -Encoding UTF8
if ($restorerExitCode -ne 0) {
    $restorerLines
    exit $restorerExitCode
}

$renameLine = $restorerLines | Where-Object { $_ -match '^RENAMED_COUNT=' } | Select-Object -Last 1
if ($renameLine -notmatch '^RENAMED_COUNT=(\d+)$') {
    throw 'Metadata restorer did not report RENAMED_COUNT.'
}
$renameCount = [int]$Matches[1]
$overrideLine = $restorerLines | Where-Object { $_ -match '^RESTORED_OVERRIDE_COUNT=' } | Select-Object -Last 1
if ($overrideLine -notmatch '^RESTORED_OVERRIDE_COUNT=(\d+)$') {
    throw 'Metadata restorer did not report RESTORED_OVERRIDE_COUNT.'
}
$restoredOverrideCount = [int]$Matches[1]

$finalPath = (Resolve-Path -LiteralPath $OutputPath).Path
$result = [pscustomobject]@{
    created_utc = [DateTime]::UtcNow.ToString('o')
    output = $finalPath
    output_length = (Get-Item -LiteralPath $finalPath).Length
    output_sha256 = (Get-FileHash -Algorithm SHA256 -LiteralPath $finalPath).Hash
    compiler = $compilerSummary
    metadata_names_restored = $renameCount
    widget_controller_overrides_restored = $restoredOverrideCount
    embedded_resource_preserved = $true
    original_public_key_preserved = $true
    authentic_strong_name_signature = $false
    product_version = $ProductVersion
    player_presence_included = -not $ExcludePlayerPresence
    deployed = $false
    runtime_tested = $false
}
$json = $result | ConvertTo-Json -Depth 7
$json | Set-Content -LiteralPath (Join-Path $logDirectory 'build-summary.json') -Encoding UTF8
$json
