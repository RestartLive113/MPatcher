param(
    [Parameter(Mandatory = $true)]
    [string]$ProjectDirectory,

    [Parameter(Mandatory = $true)]
    [string]$ReferenceDirectory,

    [Parameter(Mandatory = $true)]
    [string]$OutputPath,

    [Parameter(Mandatory = $true)]
    [string]$ResponsePath,

    [Parameter(Mandatory = $true)]
    [string]$LogPath,

    [string]$EmbeddedResourceDirectory,

	[string]$PublicKeyFile,

	[string[]]$AdditionalSourceFiles,

	[hashtable]$SourceOverrides,

	[switch]$ExcludePlayerPresence
)

$ErrorActionPreference = 'Stop'
Set-StrictMode -Version Latest

$project = (Resolve-Path -LiteralPath $ProjectDirectory).Path
$references = (Resolve-Path -LiteralPath $ReferenceDirectory).Path
$output = [IO.Path]::GetFullPath($OutputPath)
$response = [IO.Path]::GetFullPath($ResponsePath)
$log = [IO.Path]::GetFullPath($LogPath)
foreach ($directory in @((Split-Path -Parent $output), (Split-Path -Parent $response), (Split-Path -Parent $log))) {
    New-Item -ItemType Directory -Path $directory -Force | Out-Null
}

$sdkLines = @(& dotnet --list-sdks)
if ($LASTEXITCODE -ne 0 -or $sdkLines.Count -eq 0) {
    throw 'Unable to locate a .NET SDK.'
}
$sdkLine = $sdkLines | Select-Object -Last 1
if ($sdkLine -notmatch '^([^ ]+) \[(.+)\]$') {
    throw "Unrecognized SDK line: $sdkLine"
}
$sdkVersion = $Matches[1]
$sdkRoot = $Matches[2]
$compiler = Join-Path (Join-Path $sdkRoot $sdkVersion) 'Roslyn\bincore\csc.dll'
if (-not (Test-Path -LiteralPath $compiler)) {
    throw "Missing compiler: $compiler"
}

$referenceFiles = @(Get-ChildItem -LiteralPath $references -Filter '*.dll' -File | Sort-Object Name)
$sourceFiles = @(
    foreach ($sourceDirectoryName in @('RecoveredSource', 'CustomPatches')) {
        $sourceDirectory = Join-Path $project $sourceDirectoryName
        if (Test-Path -LiteralPath $sourceDirectory) {
            Get-ChildItem -LiteralPath $sourceDirectory -Filter '*.cs' -File -Recurse
        }
    }
) | Sort-Object FullName
if ($null -ne $SourceOverrides) {
    foreach ($entry in $SourceOverrides.GetEnumerator()) {
        $original = (Resolve-Path -LiteralPath $entry.Key).Path
        $replacement = (Resolve-Path -LiteralPath $entry.Value).Path
        if (@($sourceFiles | Where-Object FullName -eq $original).Count -ne 1 -or
            -not $replacement.EndsWith('.cs', [StringComparison]::OrdinalIgnoreCase)) {
            throw "Invalid source override: $original -> $replacement"
        }
        $sourceFiles = @($sourceFiles | ForEach-Object {
            if ($_.FullName -eq $original) { Get-Item -LiteralPath $replacement } else { $_ }
        })
    }
}
if ($null -ne $AdditionalSourceFiles) {
    foreach ($additionalSource in $AdditionalSourceFiles) {
        if ([string]::IsNullOrWhiteSpace($additionalSource)) { continue }
        $resolvedAdditionalSource = (Resolve-Path -LiteralPath $additionalSource).Path
        if (-not $resolvedAdditionalSource.EndsWith('.cs', [StringComparison]::OrdinalIgnoreCase)) {
            throw "Additional source is not a C# file: $resolvedAdditionalSource"
        }
        $sourceFiles += Get-Item -LiteralPath $resolvedAdditionalSource
    }
    $sourceFiles = @($sourceFiles | Sort-Object FullName -Unique)
}
if ($ExcludePlayerPresence) {
    $presenceSourcePattern = Join-Path $project 'CustomPatches\PlayerPresence*.cs'
    $sourceFiles = @($sourceFiles | Where-Object { $_.FullName -notlike $presenceSourcePattern })
}
$resourceFiles = @()
if (-not [string]::IsNullOrWhiteSpace($EmbeddedResourceDirectory)) {
    $resourceDirectory = (Resolve-Path -LiteralPath $EmbeddedResourceDirectory).Path
    $resourceFiles = @(Get-ChildItem -LiteralPath $resourceDirectory -Filter '*.resources' -File | Sort-Object Name)
    if ($resourceFiles.Count -eq 0) {
        throw "No compiled .resources files under $resourceDirectory"
    }
}
$publicKey = $null
if (-not [string]::IsNullOrWhiteSpace($PublicKeyFile)) {
    $publicKey = (Resolve-Path -LiteralPath $PublicKeyFile).Path
}
if ($sourceFiles.Count -eq 0) {
    throw "No C# source files under $project"
}

$arguments = [Collections.Generic.List[string]]::new()
foreach ($option in @('/nologo', '/noconfig', '/nostdlib+', '/target:library', '/unsafe+', '/platform:anycpu', '/optimize+', '/deterministic+', '/langversion:12', '/utf8output', '/warn:0', ('/out:"{0}"' -f $output))) {
    $arguments.Add($option)
}
if ($ExcludePlayerPresence) {
    $arguments.Add('/define:MPATCHER_EXCLUDE_PLAYER_PRESENCE')
}
foreach ($file in $referenceFiles) {
    $arguments.Add('/reference:"{0}"' -f $file.FullName)
}
foreach ($file in $resourceFiles) {
    $arguments.Add(('/resource:"{0}","{1}",public' -f $file.FullName, $file.Name))
}
if ($null -ne $publicKey) {
    # The recovered source does not include the private signing key. Public
    # signing preserves the original public-key identity and metadata flag for
    # static/control builds while making that limitation explicit.
    $arguments.Add('/keyfile:"{0}"' -f $publicKey)
    $arguments.Add('/publicsign+')
}
foreach ($file in $sourceFiles) {
    $arguments.Add('"{0}"' -f $file.FullName)
}
$arguments | Set-Content -LiteralPath $response -Encoding UTF8

$started = [DateTime]::UtcNow
$compilerOutput = @(& dotnet $compiler ('@' + $response) 2>&1)
$exitCode = $LASTEXITCODE
$compilerOutput | Set-Content -LiteralPath $log -Encoding UTF8
$errorLines = @($compilerOutput | Where-Object { $_ -match ': error CS\d+:' })
$errorCodes = [ordered]@{}
foreach ($group in @($errorLines | ForEach-Object { if ($_ -match ': error (CS\d+):') { $Matches[1] } } | Group-Object | Sort-Object Name)) {
    $errorCodes[$group.Name] = $group.Count
}

$summary = [pscustomobject]@{
    created_utc = $started.ToString('o')
    sdk = $sdkVersion
    project = $project
    references = $referenceFiles.Count
    sources = $sourceFiles.Count
    additional_sources = @($AdditionalSourceFiles).Count
    player_presence_included = -not $ExcludePlayerPresence
    embedded_resources = $resourceFiles.Count
    public_signed = ($null -ne $publicKey)
    public_key_file = $publicKey
    output = $output
    exit_code = $exitCode
    error_count = $errorLines.Count
    errors_by_code = $errorCodes
    deployed = $false
    runtime_tested = $false
}
$summary | ConvertTo-Json -Depth 5
if ($exitCode -ne 0) {
    exit $exitCode
}
