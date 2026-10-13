param([string]$AssemblyPath, [string]$ReferenceDirectory, [string]$ReportDirectory, [string]$FrozenV2Path, [string]$MachineFixturePath, [string]$DnlibPath)
$ErrorActionPreference = 'Stop'
Set-StrictMode -Version Latest
$root = Split-Path -Parent $PSScriptRoot
if ([string]::IsNullOrWhiteSpace($AssemblyPath)) { $AssemblyPath = Join-Path $root 'build\MPatchrMain.dll' }
if ([string]::IsNullOrWhiteSpace($ReferenceDirectory)) {
    $ReferenceDirectory = Join-Path $root '..\..\reference\mpatcher-installer-v1.6\mpatchrmain-recovery\references-patched-game'
}
if ([string]::IsNullOrWhiteSpace($ReportDirectory)) {
    $ReportDirectory = Join-Path $root ('analysis\setup-precision-network\run-' + [DateTime]::UtcNow.ToString('yyyyMMdd-HHmmss-fff'))
}
$reference = (Resolve-Path -LiteralPath $ReferenceDirectory).Path
$assembly = (Resolve-Path -LiteralPath $AssemblyPath).Path
if ([string]::IsNullOrWhiteSpace($DnlibPath)) { $DnlibPath = Join-Path $PSScriptRoot 'bin\dnlib.dll' }
$dnlib = (Resolve-Path -LiteralPath $DnlibPath).Path
New-Item -ItemType Directory -Path $ReportDirectory -Force | Out-Null
$report = (Resolve-Path -LiteralPath $ReportDirectory).Path
$previousFixture = $env:MPATCHER_FROZEN_SETUP_V2
$previousMachineFixture = $env:MPATCHER_SETUP_MACHINE_FIXTURE
try {
    if (![string]::IsNullOrWhiteSpace($FrozenV2Path)) { $env:MPATCHER_FROZEN_SETUP_V2 = (Resolve-Path -LiteralPath $FrozenV2Path).Path }
    if (![string]::IsNullOrWhiteSpace($MachineFixturePath)) { $env:MPATCHER_SETUP_MACHINE_FIXTURE = (Resolve-Path -LiteralPath $MachineFixturePath).Path }
    dotnet run --project (Join-Path $PSScriptRoot 'SetupPrecisionExpandedNetworkChecks.csproj') --configuration Release --artifacts-path (Join-Path $report 'artifacts') -p:NuGetAudit=false "-p:ReferenceDirectory=$reference" "-p:DnlibPath=$dnlib" -- $reference $assembly 2>&1 |
        Tee-Object -FilePath (Join-Path $report 'checks.log')
    $result = $LASTEXITCODE
} finally {
    $env:MPATCHER_FROZEN_SETUP_V2 = $previousFixture
    $env:MPATCHER_SETUP_MACHINE_FIXTURE = $previousMachineFixture
}
[pscustomobject]@{
    checked_utc = [DateTime]::UtcNow.ToString('o')
    assembly = $assembly
    sha256 = (Get-FileHash -LiteralPath $assembly -Algorithm SHA256).Hash
    passed = $result -eq 0
    deployed = $false
    game_runtime_tested = $false
} | ConvertTo-Json | Set-Content -LiteralPath (Join-Path $report 'summary.json') -Encoding UTF8
Write-Output ('Test log: ' + $report)
if ($result -ne 0) { exit $result }
