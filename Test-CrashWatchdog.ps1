[CmdletBinding()]
param([string]$WatchdogPath = '')

$ErrorActionPreference = 'Stop'
$root = $PSScriptRoot
$watchdog = if ([string]::IsNullOrWhiteSpace($WatchdogPath)) { Join-Path $root 'build\watchdog\MPatcherCrashWatchdog.exe' } else { $WatchdogPath }
$testParent = Join-Path $root 'installer-tests'
$testRoot = Join-Path $testParent ('watchdog-' + [DateTime]::UtcNow.ToString('yyyyMMdd-HHmmss-fff'))
$dummyExecutable = Join-Path $env:WINDIR 'System32\WindowsPowerShell\v1.0\powershell.exe'

foreach ($required in @($watchdog, $dummyExecutable)) {
    if (-not (Test-Path -LiteralPath $required)) {
        throw "Required file is missing: $required"
    }
}

$resolvedParent = [IO.Path]::GetFullPath($testParent)
$resolvedRoot = [IO.Path]::GetFullPath($testRoot)
if (-not $resolvedRoot.StartsWith($resolvedParent + [IO.Path]::DirectorySeparatorChar, [StringComparison]::OrdinalIgnoreCase)) {
    throw "Refusing test path outside installer-tests: $resolvedRoot"
}

function New-WatchdogFixture([string]$Name) {
    $fixture = Join-Path $resolvedRoot $Name
    $logs = Join-Path $fixture 'logs'
    New-Item -ItemType Directory -Path (Join-Path $fixture 'McnCraft_Data') -Force | Out-Null
    New-Item -ItemType Directory -Path $logs -Force | Out-Null
    [IO.File]::WriteAllText((Join-Path $fixture 'McnCraft_Data\output_log.txt'), 'unity-output-tail', [Text.UTF8Encoding]::new($false))
    [IO.File]::WriteAllText((Join-Path $logs 'patch.log'), 'patch-tail', [Text.UTF8Encoding]::new($false))
    [IO.File]::WriteAllText((Join-Path $logs 'unity.log'), 'unity-callback-tail', [Text.UTF8Encoding]::new($false))
    return $fixture
}

function Start-Dummy([int]$ExitCode) {
    return Start-Process -FilePath $dummyExecutable -ArgumentList @('-NoProfile', '-Command', "Start-Sleep -Seconds 1; exit $ExitCode") -WindowStyle Hidden -PassThru
}

function Start-Watchdog([Diagnostics.Process]$Dummy, [string]$Fixture, [string]$Session, [string]$StatePath) {
    $logs = Join-Path $Fixture 'logs'
    $arguments = '--pid {0} --root "{1}" --logs "{2}" --session "{3}" --state "{4}" --patch-log "{5}" --unity-log "{6}"' -f @(
        $Dummy.Id,
        $Fixture,
        $logs,
        $Session,
        $StatePath,
        (Join-Path $logs 'patch.log'),
        (Join-Path $logs 'unity.log')
    )
    return Start-Process -FilePath $watchdog -ArgumentList $arguments -WindowStyle Hidden -PassThru
}

$abnormal = New-WatchdogFixture 'abnormal'
$abnormalState = Join-Path $abnormal 'logs\.state'
[IO.File]::WriteAllText($abnormalState, 'state=active', [Text.UTF8Encoding]::new($false))
$abnormalDummy = Start-Dummy 5
$abnormalWatcher = Start-Watchdog $abnormalDummy $abnormal 'abnormal' $abnormalState
$abnormalWatcher.WaitForExit()
if ($abnormalWatcher.ExitCode -ne 0) { throw "Abnormal watcher exited with $($abnormalWatcher.ExitCode)" }
$abnormalReport = Join-Path $abnormal ('logs\Crash_abnormal_pid' + $abnormalDummy.Id + '_exit.txt')
if (-not (Test-Path -LiteralPath $abnormalReport)) { throw "Abnormal crash report missing: $abnormalReport" }
$abnormalText = Get-Content -LiteralPath $abnormalReport -Raw
foreach ($expected in @('exitCode=5 (0x00000005)', 'classification=abnormal process exit', 'patch-tail', 'unity-callback-tail', 'unity-output-tail')) {
    if (-not $abnormalText.Contains($expected)) { throw "Crash report omitted: $expected" }
}

$clean = New-WatchdogFixture 'clean'
$cleanState = Join-Path $clean 'logs\.state'
[IO.File]::WriteAllText($cleanState, 'state=active', [Text.UTF8Encoding]::new($false))
$cleanDummy = Start-Dummy 0
$cleanWatcher = Start-Watchdog $cleanDummy $clean 'clean' $cleanState
[IO.File]::WriteAllText($cleanState, 'state=clean', [Text.UTF8Encoding]::new($false))
$cleanWatcher.WaitForExit()
if ($cleanWatcher.ExitCode -ne 0) { throw "Clean watcher exited with $($cleanWatcher.ExitCode)" }
if (Get-ChildItem -LiteralPath (Join-Path $clean 'logs') -File -Filter 'Crash_*_exit.txt') {
    throw 'Clean shutdown produced a crash report'
}
$cleanArchive = Join-Path $clean ('logs\UnityOutput_clean_pid' + $cleanDummy.Id + '.log')
if (-not (Test-Path -LiteralPath $cleanArchive)) { throw 'Clean shutdown did not archive output_log.txt' }

[ordered]@{
    tested_utc = [DateTime]::UtcNow.ToString('o')
    watchdog = $watchdog
    watchdog_sha256 = (Get-FileHash -LiteralPath $watchdog -Algorithm SHA256).Hash
    fixture_root = $resolvedRoot
    abnormal_exit_report = $abnormalReport
    clean_output_archive = $cleanArchive
    results = [ordered]@{
        abnormal_exit = 'passed'
        clean_exit = 'passed'
    }
    game_runtime_tested = $false
} | ConvertTo-Json -Depth 4
