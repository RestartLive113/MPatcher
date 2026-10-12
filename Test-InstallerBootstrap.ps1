[CmdletBinding()]
param(
    [string]$Version = '0.3.8',
    [ValidateSet('Stable','Alpha')][string]$UpdateChannel = 'Stable',
    [string]$PackageDirectory = '',
    [string]$PayloadPath = ''
)

$ErrorActionPreference = 'Stop'
$root = $PSScriptRoot
if ([string]::IsNullOrWhiteSpace($PackageDirectory)) {
    $PackageDirectory = Join-Path $root ('dist\MPatcher-v' + $Version + $(if($UpdateChannel -eq 'Alpha'){'-Alpha'}else{''}))
}
$bootstrap = Join-Path $packageDirectory 'MPatcherInstaller.exe'
$packageInstaller = Join-Path $packageDirectory 'MPatcherPackage.exe'
$payload = if ([string]::IsNullOrWhiteSpace($PayloadPath)) { Join-Path $root 'build\__Internal.MPatcherFork.dll' } else { $PayloadPath }
$projectConfig = Get-Content -Raw -LiteralPath (Join-Path $root '..\..\project.config.json') | ConvertFrom-Json
$supportedGameExe = Join-Path ([string]$projectConfig.testGameRoot) 'McnCraft.exe'
$testParent = Join-Path $root 'installer-tests'
$testRoot = Join-Path $testParent ('bootstrap-' + $Version + '-' + [DateTime]::UtcNow.ToString('yyyyMMdd-HHmmss-fff'))

foreach ($required in @($bootstrap, $packageInstaller, $payload, $supportedGameExe)) {
    if (-not (Test-Path -LiteralPath $required)) { throw "Required file is missing: $required" }
}
$resolvedParent = [IO.Path]::GetFullPath($testParent)
$resolvedRoot = [IO.Path]::GetFullPath($testRoot)
if (-not $resolvedRoot.StartsWith($resolvedParent + [IO.Path]::DirectorySeparatorChar, [StringComparison]::OrdinalIgnoreCase)) {
    throw "Refusing test path outside installer-tests: $resolvedRoot"
}
New-Item -ItemType Directory -Path $resolvedRoot -Force | Out-Null

function Assert-Equal([object]$Actual, [object]$Expected, [string]$Message) {
    if ($Actual -ne $Expected) { throw "$Message. Actual=$Actual Expected=$Expected" }
}

function New-Fixture([string]$Name) {
    $fixture = Join-Path $resolvedRoot $Name
    New-Item -ItemType Directory -Path (Join-Path $fixture 'McnCraft_Data\Mono') -Force | Out-Null
    New-Item -ItemType Directory -Path (Join-Path $fixture 'UserData') -Force | Out-Null
    Copy-Item -LiteralPath $supportedGameExe -Destination (Join-Path $fixture 'McnCraft.exe')
    [IO.File]::WriteAllBytes((Join-Path $fixture 'McnCraft_Data\Mono\mono.dll'), [byte[]](77, 90))
    [IO.File]::WriteAllText((Join-Path $fixture 'UserData\sentinel.txt'), 'preserve-me', [Text.UTF8Encoding]::new($false))
    return $fixture
}

function Get-FreePort {
    $probe = [Net.Sockets.TcpListener]::new([Net.IPAddress]::Loopback, 0)
    $probe.Start()
    try { return ([Net.IPEndPoint]$probe.LocalEndpoint).Port }
    finally { $probe.Stop() }
}

function Start-TestServer([string]$ManifestTemplate, [string]$PackagePath, [string]$Name, [int]$RequestCount = 4) {
    $port = Get-FreePort
    $ready = Join-Path $resolvedRoot ($Name + '.ready')
    $manifestText = $ManifestTemplate.Replace('__PORT__', [string]$port)
    $manifestBase64 = [Convert]::ToBase64String([Text.Encoding]::UTF8.GetBytes($ManifestText))
    $job = Start-Job -ScriptBlock {
        param($Port, $ManifestBase64, $PackagePath, $ReadyPath, $RequestCount)
        $listener = [Net.Sockets.TcpListener]::new([Net.IPAddress]::Loopback, [int]$Port)
        try {
            $manifestBytes = [Convert]::FromBase64String($ManifestBase64)
            $listener.Start()
            [IO.File]::WriteAllText($ReadyPath, 'ready', [Text.UTF8Encoding]::new($false))
            for ($served = 0; $served -lt [int]$RequestCount; $served++) {
                $client = $listener.AcceptTcpClient()
                try {
                    $stream = $client.GetStream()
                    $reader = [IO.StreamReader]::new($stream, [Text.Encoding]::ASCII, $false, 1024, $true)
                    $requestLine = $reader.ReadLine()
                    while (-not [string]::IsNullOrEmpty($reader.ReadLine())) { }
                    $requestPath = if ([string]::IsNullOrWhiteSpace($requestLine)) { '/' } else { ($requestLine -split ' ')[1] }
                    if ($requestPath -eq '/MPatcherUpdate.ini') {
                        $body = $manifestBytes
                        $status = '200 OK'
                        $contentType = 'text/plain; charset=utf-8'
                    } elseif ($requestPath -eq '/MPatcherPackage.exe') {
                        $body = [IO.File]::ReadAllBytes($PackagePath)
                        $status = '200 OK'
                        $contentType = 'application/octet-stream'
                    } else {
                        $body = [Text.Encoding]::UTF8.GetBytes('not found')
                        $status = '404 Not Found'
                        $contentType = 'text/plain'
                    }
                    $header = "HTTP/1.1 $status`r`nContent-Type: $contentType`r`nContent-Length: $($body.Length)`r`nConnection: close`r`n`r`n"
                    $headerBytes = [Text.Encoding]::ASCII.GetBytes($header)
                    $stream.Write($headerBytes, 0, $headerBytes.Length)
                    $stream.Write($body, 0, $body.Length)
                    $stream.Flush()
                } finally {
                    $client.Dispose()
                }
            }
        } finally {
            $listener.Stop()
        }
    } -ArgumentList $port, $manifestBase64, $PackagePath, $ready, $RequestCount

    $deadline = [DateTime]::UtcNow.AddSeconds(10)
    while (-not (Test-Path -LiteralPath $ready) -and [DateTime]::UtcNow -lt $deadline) {
        Start-Sleep -Milliseconds 100
    }
    if (-not (Test-Path -LiteralPath $ready)) {
        $details = Receive-Job -Job $job -Keep -ErrorAction SilentlyContinue | Out-String
        Stop-Job -Job $job -ErrorAction SilentlyContinue
        Remove-Job -Job $job -Force -ErrorAction SilentlyContinue
        throw "Test HTTP server did not start. $details"
    }
    return [pscustomobject]@{ Job = $job; Port = $port; ManifestUrl = "http://127.0.0.1:$port/MPatcherUpdate.ini" }
}

function Stop-TestServer($Server) {
    if ($null -eq $Server) { return }
    $null = Wait-Job -Job $Server.Job -Timeout 10 -ErrorAction SilentlyContinue
    if ($Server.Job.State -notin @('Completed', 'Failed', 'Stopped')) {
        Stop-Job -Job $Server.Job -ErrorAction SilentlyContinue
    }
    Remove-Job -Job $Server.Job -Force -ErrorAction SilentlyContinue
}

function Invoke-Bootstrap([string]$Action, [string]$GameRoot, [string]$ManifestUrl, [string]$TempRoot, [int]$ExpectedExitCode) {
    $arguments = '--bootstrap-action ' + $Action + ' --path "' + $GameRoot.Replace('"', '\"') +
        '" --bootstrap-manifest "' + $ManifestUrl + '" --bootstrap-temp-root "' + $TempRoot.Replace('"', '\"') + '" --lang en'
    $process = Start-Process -FilePath $bootstrap -ArgumentList $arguments -Wait -PassThru -WindowStyle Hidden
    Assert-Equal $process.ExitCode $ExpectedExitCode "Bootstrap $Action exit code mismatch"
}

$bootstrapAssembly = [Reflection.Assembly]::LoadFile((Resolve-Path -LiteralPath $bootstrap).Path)
Assert-Equal @($bootstrapAssembly.GetManifestResourceNames()).Count 0 'Universal installer unexpectedly embeds a payload'
$packageHash = (Get-FileHash -LiteralPath $packageInstaller -Algorithm SHA256).Hash
$packageLength = (Get-Item -LiteralPath $packageInstaller).Length
$payloadHash = (Get-FileHash -LiteralPath $payload -Algorithm SHA256).Hash

$probeDirectory = Join-Path $resolvedRoot 'game-close-probe-build'
New-Item -ItemType Directory -Path $probeDirectory -Force | Out-Null
$probeSource = Join-Path $probeDirectory 'GameCloseProbe.cs'
$probeExe = Join-Path $probeDirectory 'McnCraft.exe'
$probeCode = @'
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

internal static class GameCloseProbe
{
    private static int Main()
    {
        File.WriteAllText(Path.Combine(Environment.CurrentDirectory, "bootstrap-close-ready.txt"), Process.GetCurrentProcess().Id.ToString());
        Thread.Sleep(30000);
        return 0;
    }
}
'@
[IO.File]::WriteAllText($probeSource, $probeCode, [Text.UTF8Encoding]::new($false))
$csc = Join-Path $env:WINDIR 'Microsoft.NET\Framework\v4.0.30319\csc.exe'
& $csc /nologo /target:winexe /optimize+ ('/out:' + $probeExe) $probeSource
if ($LASTEXITCODE -ne 0) { throw "Game close probe compiler failed with exit code $LASTEXITCODE" }

$successFixture = New-Fixture 'online-success'
$successFixtureExe = Join-Path $successFixture 'McnCraft.exe'
Copy-Item -LiteralPath $probeExe -Destination $successFixtureExe -Force
$successTemp = Join-Path $resolvedRoot 'online-success-temp'
$successServer = $null
$successGameProcess = $null
$successRestoreJob = $null
try {
    $successManifestTemplate = @(
        'Format=1'
        ('Version=' + $Version)
        'InstallerUrl=http://127.0.0.1:__PORT__/MPatcherPackage.exe'
        ('InstallerSha256=' + $packageHash)
        ('InstallerLength=' + $packageLength)
    ) -join "`n"
    $successServer = Start-TestServer $successManifestTemplate $packageInstaller 'online-success'
    $successManifestUrl = $successServer.ManifestUrl

	$successGameProcess = Start-Process -FilePath $successFixtureExe -WorkingDirectory $successFixture -PassThru -WindowStyle Hidden
	$readyDeadline = [DateTime]::UtcNow.AddSeconds(10)
	while (-not (Test-Path -LiteralPath (Join-Path $successFixture 'bootstrap-close-ready.txt')) -and [DateTime]::UtcNow -lt $readyDeadline) {
		Start-Sleep -Milliseconds 100
	}
	if (-not (Test-Path -LiteralPath (Join-Path $successFixture 'bootstrap-close-ready.txt'))) {
		throw 'Timed out waiting for the bootstrap game-close probe'
	}
	$successRestoreMarker = Join-Path $successFixture 'supported-exe-restored.txt'
	$successRestoreJob = Start-Job -ScriptBlock {
		param($ProcessId, $Source, $Destination, $Marker)
		Wait-Process -Id $ProcessId -ErrorAction SilentlyContinue
		Copy-Item -LiteralPath $Source -Destination $Destination -Force
		[IO.File]::WriteAllText($Marker, 'restored', [Text.UTF8Encoding]::new($false))
	} -ArgumentList $successGameProcess.Id, $supportedGameExe, $successFixtureExe, $successRestoreMarker
    Invoke-Bootstrap 'install' $successFixture $successManifestUrl $successTemp 0
	$null = Wait-Job -Job $successRestoreJob -Timeout 10
	if ($successRestoreJob.State -ne 'Completed') {
		throw 'Timed out restoring the supported McnCraft.exe after the close probe'
	}
	Receive-Job -Job $successRestoreJob -ErrorAction Stop | Out-Null
	Assert-Equal (Test-Path -LiteralPath $successRestoreMarker) $true 'Supported McnCraft.exe restore marker missing'
	Assert-Equal (Get-FileHash -LiteralPath $successFixtureExe -Algorithm SHA256).Hash (Get-FileHash -LiteralPath $supportedGameExe -Algorithm SHA256).Hash 'Supported McnCraft.exe was not restored after the close probe'
	$successGameProcess.Refresh()
	Assert-Equal $successGameProcess.HasExited $true 'Universal installer did not close the selected running game'
    Assert-Equal (Get-FileHash -LiteralPath (Join-Path $successFixture 'McnCraft_Data\Mono\__Internal.dll') -Algorithm SHA256).Hash $payloadHash 'Bootstrap installed payload mismatch'
    Assert-Equal (Get-Content -LiteralPath (Join-Path $successFixture 'UserData\sentinel.txt') -Raw) 'preserve-me' 'Bootstrap install changed UserData'
    Assert-Equal @(Get-ChildItem -LiteralPath $successTemp -Force -ErrorAction SilentlyContinue).Count 0 'Bootstrap retained downloaded package after install'
    $successLog = Get-Content -LiteralPath (Join-Path $successFixture 'logs\MPatcherInstaller.log') -Raw
    foreach ($marker in @('BOOTSTRAP_CHECK_OK', 'BOOTSTRAP_DOWNLOAD_OK', 'GAME_CLOSE_FORCE_BEGIN',
		'GAME_CLOSE_OK', 'forced=True', 'BOOTSTRAP_PACKAGE_RESULT success=True',
		'installed successfully', 'BOOTSTRAP_PACKAGE_OK action=install')) {
        Assert-Equal $successLog.Contains($marker) $true "Bootstrap success log marker missing: $marker"
    }

    Invoke-Bootstrap 'uninstall' $successFixture $successManifestUrl $successTemp 0
    Assert-Equal (Test-Path -LiteralPath (Join-Path $successFixture 'McnCraft_Data\Mono\__Internal.dll')) $false 'Bootstrap uninstall retained loader'
    Assert-Equal (Get-Content -LiteralPath (Join-Path $successFixture 'UserData\sentinel.txt') -Raw) 'preserve-me' 'Bootstrap uninstall changed UserData'
    Assert-Equal @(Get-ChildItem -LiteralPath $successTemp -Force -ErrorAction SilentlyContinue).Count 0 'Bootstrap retained downloaded package after uninstall'
} finally {
	if ($null -ne $successGameProcess) {
		if (-not $successGameProcess.HasExited) { Stop-Process -Id $successGameProcess.Id -Force }
		$successGameProcess.Dispose()
	}
	if ($null -ne $successRestoreJob) {
		if ($successRestoreJob.State -notin @('Completed', 'Failed', 'Stopped')) { Stop-Job -Job $successRestoreJob -ErrorAction SilentlyContinue }
		Remove-Job -Job $successRestoreJob -Force -ErrorAction SilentlyContinue
	}
    Stop-TestServer $successServer
}

$packageFailureFixture = New-Fixture 'package-failure-result'
New-Item -ItemType Directory -Path (Join-Path $packageFailureFixture 'McnCraft_Data\Mono\__Internal.dll') -Force | Out-Null
$packageFailureTemp = Join-Path $resolvedRoot 'package-failure-temp'
$packageFailureServer = $null
try {
	$packageFailureManifest = @(
		'Format=1'
		('Version=' + $Version)
		'InstallerUrl=http://127.0.0.1:__PORT__/MPatcherPackage.exe'
		('InstallerSha256=' + $packageHash)
		('InstallerLength=' + $packageLength)
	) -join "`n"
	$packageFailureServer = Start-TestServer $packageFailureManifest $packageInstaller 'package-failure' 2
	Invoke-Bootstrap 'install' $packageFailureFixture $packageFailureServer.ManifestUrl $packageFailureTemp 1
	Assert-Equal (Test-Path -LiteralPath (Join-Path $packageFailureFixture 'McnCraft_Data\MPatcherFork.install.ini')) $false 'Failed package created an install manifest'
	Assert-Equal (Get-Content -LiteralPath (Join-Path $packageFailureFixture 'UserData\sentinel.txt') -Raw) 'preserve-me' 'Failed package changed UserData'
	Assert-Equal @(Get-ChildItem -LiteralPath $packageFailureTemp -Force -ErrorAction SilentlyContinue).Count 0 'Failed package retained a download'
	$packageFailureLog = Get-Content -LiteralPath (Join-Path $packageFailureFixture 'logs\MPatcherInstaller.log') -Raw
	foreach ($marker in @('BOOTSTRAP_PACKAGE_RESULT success=False', 'MPatcher could not be installed', 'BOOTSTRAP_PACKAGE_FAILED action=install')) {
		Assert-Equal $packageFailureLog.Contains($marker) $true "Package failure result marker missing: $marker"
	}
} finally {
	Stop-TestServer $packageFailureServer
}

$rejectedFixture = New-Fixture 'rejected-download'
$rejectedTemp = Join-Path $resolvedRoot 'rejected-download-temp'
$rejectedServer = $null
try {
    $rejectedManifestTemplate = @(
        'Format=1'
        ('Version=' + $Version)
        'InstallerUrl=http://127.0.0.1:__PORT__/MPatcherPackage.exe'
        'InstallerSha256=0000000000000000000000000000000000000000000000000000000000000000'
        ('InstallerLength=' + $packageLength)
    ) -join "`n"
    $rejectedServer = Start-TestServer $rejectedManifestTemplate $packageInstaller 'rejected-download' 2
    Invoke-Bootstrap 'install' $rejectedFixture $rejectedServer.ManifestUrl $rejectedTemp 1
    Assert-Equal (Test-Path -LiteralPath (Join-Path $rejectedFixture 'McnCraft_Data\Mono\__Internal.dll')) $false 'Rejected bootstrap changed loader'
    Assert-Equal (Test-Path -LiteralPath (Join-Path $rejectedFixture 'McnCraft_Data\MPatcherFork.install.ini')) $false 'Rejected bootstrap created install manifest'
    Assert-Equal (Get-Content -LiteralPath (Join-Path $rejectedFixture 'UserData\sentinel.txt') -Raw) 'preserve-me' 'Rejected bootstrap changed UserData'
    Assert-Equal @(Get-ChildItem -LiteralPath $rejectedTemp -Force -ErrorAction SilentlyContinue).Count 0 'Rejected bootstrap retained a download'
    $rejectedLog = Get-Content -LiteralPath (Join-Path $rejectedFixture 'logs\MPatcherInstaller.log') -Raw
    Assert-Equal $rejectedLog.Contains('SHA-256 does not match manifest') $true 'Rejected hash explanation marker missing'
} finally {
    Stop-TestServer $rejectedServer
}

$offlineFixture = New-Fixture 'network-unavailable'
$offlineTemp = Join-Path $resolvedRoot 'network-unavailable-temp'
$offlinePort = Get-FreePort
Invoke-Bootstrap 'install' $offlineFixture "http://127.0.0.1:$offlinePort/MPatcherUpdate.ini" $offlineTemp 3
Assert-Equal (Test-Path -LiteralPath (Join-Path $offlineFixture 'McnCraft_Data\Mono\__Internal.dll')) $false 'Offline bootstrap changed loader'
Assert-Equal (Test-Path -LiteralPath (Join-Path $offlineFixture 'McnCraft_Data\MPatcherFork.install.ini')) $false 'Offline bootstrap created install manifest'
Assert-Equal (Get-Content -LiteralPath (Join-Path $offlineFixture 'UserData\sentinel.txt') -Raw) 'preserve-me' 'Offline bootstrap changed UserData'
Assert-Equal @(Get-ChildItem -LiteralPath $offlineTemp -Force -ErrorAction SilentlyContinue).Count 0 'Offline bootstrap retained a download'
$offlineLog = Get-Content -LiteralPath (Join-Path $offlineFixture 'logs\MPatcherInstaller.log') -Raw
Assert-Equal $offlineLog.Contains('BOOTSTRAP_CHECK_NETWORK_FAILED') $true 'Offline explanation marker missing'
Assert-Equal $offlineLog.Contains('BOOTSTRAP_FAILED type=BootstrapNetworkException') $true 'Offline failure marker missing'

[ordered]@{
    tested_utc = [DateTime]::UtcNow.ToString('o')
    version = $Version
    universal_installer = $bootstrap
    universal_installer_sha256 = (Get-FileHash -LiteralPath $bootstrap -Algorithm SHA256).Hash
    package_installer_sha256 = $packageHash
    fixture_root = $resolvedRoot
    checks = [ordered]@{
        no_embedded_payload = 'passed'
        online_install = 'passed'
        online_uninstall = 'passed'
		running_game_auto_close = 'passed'
		clear_package_result = 'passed'
		clear_package_failure_result = 'passed'
        verified_download_cleanup = 'passed'
        corrupt_download_rejected = 'passed'
        network_unavailable_no_changes = 'passed'
    }
    game_runtime_tested = $false
} | ConvertTo-Json -Depth 4
