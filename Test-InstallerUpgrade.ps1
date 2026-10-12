[CmdletBinding()]
# The archived 0.3.4 EXE is omitted from the local default because Windows
# Defender quarantined that specific binary. Pass it explicitly on a host where
# a trusted copy is available; release checks also cover an equivalent manifest.
param(
    [string]$Version = '0.3.8',
    [string[]]$PreviousVersions = @('0.3.0', '0.3.1', '0.3.2', '0.3.3', '0.3.5', '0.3.6'),
    [ValidateSet('Stable','Alpha')][string]$UpdateChannel = 'Stable',
    [string]$PackageDirectory = '',
    [string]$PayloadPath = ''
)

$ErrorActionPreference = 'Stop'
$root = $PSScriptRoot
if ([string]::IsNullOrWhiteSpace($PackageDirectory)) {
    $PackageDirectory = Join-Path $root ('dist\MPatcher-v' + $Version + $(if($UpdateChannel -eq 'Alpha'){'-Alpha'}else{''}))
}
$newInstaller = Join-Path $PackageDirectory 'MPatcherPackage.exe'
$newPayload = if ([string]::IsNullOrWhiteSpace($PayloadPath)) { Join-Path $root 'build\__Internal.MPatcherFork.dll' } else { $PayloadPath }
$officialLoader = Join-Path $root '..\..\reference\mpatcher-installer-v1.6\ghidra-input\PatchLoader-28c3-installed.dll'
$projectConfig = Get-Content -Raw -LiteralPath (Join-Path $root '..\..\project.config.json') | ConvertFrom-Json
$supportedGameExe = Join-Path ([string]$projectConfig.testGameRoot) 'McnCraft.exe'
$testParent = Join-Path $root 'installer-tests'
$testRoot = Join-Path $testParent ('upgrade-' + $Version + '-' + [DateTime]::UtcNow.ToString('yyyyMMdd-HHmmss-fff'))

foreach ($required in @($newInstaller, $newPayload, $officialLoader, $supportedGameExe)) {
    if (-not (Test-Path -LiteralPath $required)) { throw "Required file is missing: $required" }
}
$resolvedParent = [IO.Path]::GetFullPath($testParent)
$resolvedRoot = [IO.Path]::GetFullPath($testRoot)
if (-not $resolvedRoot.StartsWith($resolvedParent + [IO.Path]::DirectorySeparatorChar, [StringComparison]::OrdinalIgnoreCase)) {
    throw "Refusing test path outside installer-tests: $resolvedRoot"
}
New-Item -ItemType Directory -Path $resolvedRoot -Force | Out-Null

function Invoke-Installer([string]$Path, [string]$Action, [string]$GameRoot) {
    $arguments = '--' + $Action + ' "' + $GameRoot.Replace('"', '\"') + '"'
    $process = Start-Process -FilePath $Path -ArgumentList $arguments -Wait -PassThru -WindowStyle Hidden
    if ($process.ExitCode -ne 0) { throw "$Path --$Action returned $($process.ExitCode)" }
}

$payloadHash = (Get-FileHash -LiteralPath $newPayload -Algorithm SHA256).Hash
$officialHash = (Get-FileHash -LiteralPath $officialLoader -Algorithm SHA256).Hash
$results = @()
foreach ($previous in $PreviousVersions) {
    $oldInstaller = Join-Path $root ('dist\MPatcher-v' + $previous + '\MPatcherInstaller.exe')
    if (-not (Test-Path -LiteralPath $oldInstaller)) { throw "Previous installer is missing: $oldInstaller" }
    foreach ($scenario in @('clean', 'existing')) {
        $fixture = Join-Path $resolvedRoot ($previous + '-' + $scenario)
        New-Item -ItemType Directory -Path (Join-Path $fixture 'McnCraft_Data\Mono') -Force | Out-Null
        New-Item -ItemType Directory -Path (Join-Path $fixture 'UserData') -Force | Out-Null
        Copy-Item -LiteralPath $supportedGameExe -Destination (Join-Path $fixture 'McnCraft.exe')
        [IO.File]::WriteAllBytes((Join-Path $fixture 'McnCraft_Data\Mono\mono.dll'), [byte[]](77, 90))
        [IO.File]::WriteAllText((Join-Path $fixture 'UserData\sentinel.txt'), 'preserve-me', [Text.UTF8Encoding]::new($false))
        $installedLoader = Join-Path $fixture 'McnCraft_Data\Mono\__Internal.dll'
        if ($scenario -eq 'existing') { Copy-Item -LiteralPath $officialLoader -Destination $installedLoader }

        Invoke-Installer $oldInstaller 'install' $fixture
        Invoke-Installer $newInstaller 'install' $fixture
        if ((Get-FileHash -LiteralPath $installedLoader -Algorithm SHA256).Hash -ne $payloadHash) {
            throw "Upgrade payload mismatch: $previous/$scenario"
        }
        Invoke-Installer $newInstaller 'uninstall' $fixture
        if ($scenario -eq 'clean' -and (Test-Path -LiteralPath $installedLoader)) {
            throw "Clean uninstall retained a loader: $previous"
        }
        $loaderRestored = (Test-Path -LiteralPath $installedLoader) -and
            ((Get-FileHash -LiteralPath $installedLoader -Algorithm SHA256).Hash -eq $officialHash)
        if ($scenario -eq 'existing' -and -not $loaderRestored) {
            throw "Existing loader was not restored: $previous"
        }
        if ((Get-Content -LiteralPath (Join-Path $fixture 'UserData\sentinel.txt') -Raw) -ne 'preserve-me') {
            throw "UserData changed: $previous/$scenario"
        }
        $results += [pscustomobject]@{ from = $previous; scenario = $scenario; result = 'passed' }
    }
}

[ordered]@{
    tested_utc = [DateTime]::UtcNow.ToString('o')
    target = $Version
    fixture_root = $resolvedRoot
    count = $results.Count
    cases = $results
    game_runtime_tested = $false
} | ConvertTo-Json -Depth 5
