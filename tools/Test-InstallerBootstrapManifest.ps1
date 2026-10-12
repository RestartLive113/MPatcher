param()

$ErrorActionPreference = 'Stop'
Set-StrictMode -Version Latest

$root = Split-Path -Parent $PSScriptRoot
$logDirectory = Join-Path $root ('logs\installer-bootstrap-manifest-' + [DateTime]::UtcNow.ToString('yyyyMMdd-HHmmss-fff'))
New-Item -ItemType Directory -Path $logDirectory -Force | Out-Null
$project = Join-Path $PSScriptRoot 'InstallerBootstrapChecks\InstallerBootstrapChecks.csproj'
$artifacts = Join-Path $root 'build\dotnet-tests\installer-bootstrap-manifest'

dotnet run --project $project --configuration Release --artifacts-path $artifacts -p:NuGetAudit=false 2>&1 |
    Tee-Object -FilePath (Join-Path $logDirectory 'checks.log')
$exitCode = $LASTEXITCODE
[pscustomobject]@{
    checked_utc = [DateTime]::UtcNow.ToString('o')
    passed = $exitCode -eq 0
    runtime_tested = $false
} | ConvertTo-Json | Set-Content -LiteralPath (Join-Path $logDirectory 'summary.json') -Encoding UTF8
Write-Output ('Test log: ' + $logDirectory)
if ($exitCode -ne 0) { exit $exitCode }
