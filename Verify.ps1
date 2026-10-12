param(
    [string]$AssemblyPath,
    [ValidatePattern('^\d+\.\d+\.\d+(?:\.\d+)?$')]
    [string]$ExpectedProductVersion = '0.3.7',
    [switch]$ExpectPlayerPresenceExcluded,
    [switch]$ExpectPrecompiledPatches
)

$ErrorActionPreference = 'Stop'
Set-StrictMode -Version Latest

$root = $PSScriptRoot
if ([string]::IsNullOrWhiteSpace($AssemblyPath)) {
    $AssemblyPath = Join-Path $root 'build\MPatchrMain.dll'
}
$assembly = (Resolve-Path -LiteralPath $AssemblyPath).Path
$logDirectory = Join-Path $root 'logs'
New-Item -ItemType Directory -Path $logDirectory -Force | Out-Null

$inspector = Join-Path $root 'tools\bin\Inspect-MPatcherAssembly.exe'
$inspection = @(& $inspector $assembly 2>&1)
$exitCode = $LASTEXITCODE
$inspectionPath = Join-Path $logDirectory 'assembly-inspection.log'
$inspection | Set-Content -LiteralPath $inspectionPath -Encoding UTF8
if ($exitCode -ne 0) {
    $inspection
    exit $exitCode
}

$text = $inspection -join "`n"
$checks = [ordered]@{
    assembly_identity = $text -match 'ASSEMBLY=MPatchrMain, Version=1\.0\.0\.0, Culture=neutral, PublicKeyToken=32b2b1d9003f7b8c'
    clr_and_pe_flags = $text -match 'RUNTIME=v2\.0\.50727\|KIND=Dll\|MACHINE=I386\|COR20=ILOnly, StrongNameSigned'
    reference_count = $text -match $(if ($ExpectPrecompiledPatches) { 'RESOURCES=2\|ASSEMBLY_REFS=11' } else { 'RESOURCES=1\|ASSEMBLY_REFS=11' })
    loader_entrypoint = $text -match 'ENTRY_TYPE=MPatchrMain\.MPatchr\|LOAD_COUNT=1' -and $text -match 'LOAD\|.*STATIC=True.*SIGNATURE=System\.Void \(\).*HAS_BODY=True'
    resource_exact = $text -match 'RESOURCE=.*\|LENGTH=2296671\|SHA256=FC69CD160704AEC36D1B57DF78A4180457EF5C2616FBD122F39A7A932125B9E0'
    no_unsafe_dependency = $text -notmatch '^REFERENCE=System\.Runtime\.CompilerServices\.Unsafe,'
}
$expectedWidgetOverrides = @(
    'Control0|slAohyJbgvnQR$kdjOrtJBQ|MGBADOFGMFP',
    'lcJ$o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC$9HYbtFHskS9uvU9Grlzn58YAeCbpw|slAohyJbgvnQR$kdjOrtJBQ|MGBADOFGMFP',
    'lcJ$o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC$9HYbtFHskS9uvU9Grlzn58YAeCbpw|EKmj2wjaAOQ9HFvKkrJJ_4U|JOJJFMHJAHM',
    'lcJ$o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC$9HYbtFHskS9uvU9Grlzn58YAeCbpw|muLYqcKag6qbwQ4qIBhj4mY|CJLBGKELNHP',
    'lcJ$o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC$9HYbtFHskS9uvU9Grlzn58YAeCbpw|OvP2yXRDJ_0wb2Bkvo533Fw|AGEIMACGIEL'
)
Add-Type -Path (Join-Path $root 'tools\bin\dnlib.dll')
$module = [dnlib.DotNet.ModuleDefMD]::Load($assembly)
$crashDiagnosticsNoEarlyGameObject = $false
$crashDiagnosticsCleanExitHook = $false
$actualProductVersion = $null
$updaterTypesPresent = $false
$updaterStartHook = $false
$settingsUiSchedulerHook = $false
$updaterUiHooks = $false
$disabledUpdaterLogAbsent = $false
$startupGateContract = $false
$updaterChannelContract = $false
$timedUpdateNoticeContract = $false
$startupReadyHook = $false
$mainMenuVersionTypePresent = $false
$mainMenuVersionRegisterHook = $false
$mainMenuVersionContract = $false
$machineChangeUnrestrictedZeroCooldown = $false
$machineChangeDeferredCleanup = $false
$machineChangeDirectSpawn = $false
$machineChangeSinglePositioning = $false
$machineChangeTransactionalRegistration = $false
$machineChangeRollbackRecovery = $false
$machineChangeReboundRetire = $false
$legacyConnectionStabilityTypes = $false
$legacyConnectionRetryContract = $false
$legacyFacilitatorRegisterHook = $false
$legacyFacilitatorNativeContract = $false
$legacyHostStabilityTypes = $false
$legacyHostRegisterHook = $false
$legacyHostDirectoryContract = $false
$legacyHostFacilitatorRegisterHook = $false
$legacyHostFacilitatorTypes = $false
$legacyHostFacilitatorNativeContract = $false
$legacyMasterLifecycleHook = $false
$legacyMasterLifecycleContract = $false
$legacyMasterQueryHook = $false
$legacyMasterQueryContract = $false
$legacySocketSelfTestHook = $false
$legacySocketSelfTestContract = $false
$legacyCatalogueRecoveryHook = $false
$legacyCatalogueRecoveryContract = $false
$legacyIndivFixBundleContract = $false
$autoReconnectSettingsContract = $false
$legacyMachineRecoveryTypes = $false
$legacyMachineRecoveryRegisterHook = $false
$legacyMachineRecoveryContract = $false
try {
    $allTypes = @($module.GetTypes())
    $versionAttributes = @($module.Assembly.CustomAttributes | Where-Object { $_.AttributeType.FullName -eq 'System.Reflection.AssemblyInformationalVersionAttribute' })
    if ($versionAttributes.Count -eq 1 -and $versionAttributes[0].ConstructorArguments.Count -eq 1) {
        $actualProductVersion = [string]$versionAttributes[0].ConstructorArguments[0].Value
    }

    $actualWidgetOverrides = @(
        foreach ($type in $allTypes) {
            foreach ($method in $type.Methods) {
                foreach ($override in $method.Overrides) {
                    if ($override.MethodDeclaration.DeclaringType.FullName -eq 'WidgetController') {
                        '{0}|{1}|{2}' -f $type.FullName, $method.Name, $override.MethodDeclaration.Name
                    }
                }
            }
        }
    )

    $crashDiagnosticsType = $allTypes | Where-Object FullName -eq 'MPatcherFork.CustomPatches.CrashDiagnostics'
    $tryRegister = $crashDiagnosticsType.Methods | Where-Object Name -eq 'TryRegister'
    if ($null -ne $tryRegister -and $tryRegister.HasBody) {
        $earlyUnityObjectCalls = @(
            $tryRegister.Body.Instructions |
                ForEach-Object { [string]$_.Operand } |
                Where-Object { $_ -match 'UnityEngine\.GameObject|UnityEngine\.Object::DontDestroyOnLoad' }
        )
        $crashDiagnosticsNoEarlyGameObject = $earlyUnityObjectCalls.Count -eq 0
    }

    $mpatchrType = $allTypes | Where-Object FullName -eq 'MPatchrMain.MPatchr'
    $mainMenuVersionType = $allTypes | Where-Object FullName -eq 'MPatcherFork.CustomPatches.MainMenuVersionLabel'
    $mainMenuVersionTypePresent = $null -ne $mainMenuVersionType
    $load = $mpatchrType.Methods | Where-Object Name -eq 'Load'
	$completeLoad = $mpatchrType.Methods | Where-Object Name -eq 'CompleteLoad'
    if ($null -ne $load -and $load.HasBody) {
        $loadOperands = @($load.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$bootstrapInstructions = @($load.Body.Instructions)
		if ($null -ne $completeLoad -and $completeLoad.HasBody) {
			$bootstrapInstructions += @($completeLoad.Body.Instructions)
		}
		$bootstrapOperands = @($bootstrapInstructions | ForEach-Object { [string]$_.Operand })
        $mainMenuVersionRegisterHook = @($bootstrapOperands | Where-Object { $_ -match 'MPatcherFork\.CustomPatches\.MainMenuVersionLabel::TryRegister\(' }).Count -eq 1
		$legacyIndivFixBundleType = $allTypes | Where-Object FullName -eq 'MPatcherFork.CustomPatches.LegacyIndivFixBundle'
		$legacyIndivFixBundleRegister = $legacyIndivFixBundleType.Methods | Where-Object Name -eq 'TryRegister'
		$legacyIndivFixBundleActivate = $legacyIndivFixBundleType.Methods | Where-Object Name -eq 'Activate'
		$legacyIndivFixBundleOperands = @($legacyIndivFixBundleActivate.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyIndivFixBundleBootstrap = @($bootstrapOperands | Where-Object { $_ -match 'MPatcherFork\.CustomPatches\.LegacyIndivFixBundle::TryRegister\(' }).Count -eq 1
		$legacyFacilitatorRegisterHook = $legacyIndivFixBundleBootstrap -and @($legacyIndivFixBundleOperands | Where-Object { $_ -match 'LegacyFacilitatorDisconnectFix::TryRegister\(' }).Count -eq 1
		$legacyHostFacilitatorRegisterHook = $legacyIndivFixBundleBootstrap -and @($legacyIndivFixBundleOperands | Where-Object { $_ -match 'LegacyHostFacilitatorReconnectFix::TryRegister\(' }).Count -eq 1
		$legacyHostRegisterHook = $legacyIndivFixBundleBootstrap -and @($legacyIndivFixBundleOperands | Where-Object { $_ -match 'LegacyHostStability::TryRegister\(' }).Count -eq 1
		$legacyMasterLifecycleHook = $legacyIndivFixBundleBootstrap -and @($legacyIndivFixBundleOperands | Where-Object { $_ -match 'LegacyMasterLifecycleFix::TryRegister\(' }).Count -eq 1
		$legacyMasterQueryHook = $legacyIndivFixBundleBootstrap -and @($legacyIndivFixBundleOperands | Where-Object { $_ -match 'LegacyMasterQueryFix::TryRegister\(' }).Count -eq 1
		$legacySocketSelfTestHook = $legacyIndivFixBundleBootstrap -and @($legacyIndivFixBundleOperands | Where-Object { $_ -match 'LegacySocketSelfTestFix::TryRegister\(' }).Count -eq 1
		$legacyCatalogueRecoveryHook = $legacyIndivFixBundleBootstrap -and @($legacyIndivFixBundleOperands | Where-Object { $_ -match 'LegacyCatalogueRecovery::TryRegister\(' }).Count -eq 1
		$legacyIndivFixBundleContract = $legacyIndivFixBundleBootstrap -and
			@($bootstrapOperands | Where-Object { $_ -match 'Legacy(ConnectionDiagnostics|MtuProbeFix|FacilitatorDisconnectFix|HostFacilitatorReconnectFix|MasterLifecycleFix|MasterQueryFix|SocketSelfTestFix|CatalogueRecovery|HostStability|HostAutoPort|HostMigration)::TryRegister\(' }).Count -eq 0 -and
			@($legacyIndivFixBundleOperands | Where-Object { $_ -match 'LegacyConnectionDiagnostics::TryRegister\(' }).Count -ge 1 -and
			@($legacyIndivFixBundleOperands | Where-Object { $_ -match 'LegacyMtuProbeFix::TryRegister\(' }).Count -ge 1 -and
			@($legacyIndivFixBundleOperands | Where-Object { $_ -match 'LegacyHostAutoPort::TryRegister\(' }).Count -ge 1 -and
			@($legacyIndivFixBundleOperands | Where-Object { $_ -match 'LegacyHostMigration::TryRegister\(' }).Count -ge 1
		$legacyMachineRecoveryRegisterHook =
			@($bootstrapOperands | Where-Object { $_ -match 'MPatcherFork\.CustomPatches\.LegacyTransientReconnect::TryRegister\(' }).Count -eq 1 -and
			@($bootstrapOperands | Where-Object { $_ -match 'MPatcherFork\.CustomPatches\.LegacyMachineRecovery::TryRegister\(' }).Count -eq 0
    }
    if ($mainMenuVersionTypePresent) {
        $mainMenuVersionStrings = @(
            foreach ($method in $mainMenuVersionType.Methods) {
                if ($method.HasBody) {
                    $method.Body.Instructions |
                        Where-Object { $_.OpCode.Code -eq 'Ldstr' } |
                        ForEach-Object { [string]$_.Operand }
                }
            }
        )
        $mainMenuVersionContract = 'Menu.Start' -in $mainMenuVersionStrings -and
            'Version' -in $mainMenuVersionStrings -and
            'MP Ver.' -in $mainMenuVersionStrings -and
            'MPatcherVersion' -in $mainMenuVersionStrings
    }

	$legacyDiagnosticsType = $allTypes | Where-Object FullName -eq 'MPatcherFork.CustomPatches.LegacyConnectionDiagnostics'
	$legacyRetryType = $allTypes | Where-Object FullName -eq 'MPatcherFork.CustomPatches.LegacyConnectionRetryFix'
	$legacyFacilitatorType = $allTypes | Where-Object FullName -eq 'MPatcherFork.CustomPatches.LegacyFacilitatorDisconnectFix'
	$legacyConnectionStabilityTypes = $null -ne $legacyDiagnosticsType -and $null -ne $legacyRetryType -and $null -ne $legacyFacilitatorType
	if ($legacyConnectionStabilityTypes) {
		$diagnosticsRegister = $legacyDiagnosticsType.Methods | Where-Object Name -eq 'TryRegister'
		$diagnosticsFailed = $legacyDiagnosticsType.Methods | Where-Object Name -eq 'FailedPrefix'
		$diagnosticsReset = $legacyDiagnosticsType.Methods | Where-Object Name -eq 'ResetPrefix'
		$diagnosticsUpdate = $legacyDiagnosticsType.Methods | Where-Object Name -eq 'UpdatePrefix'
		$diagnosticsOperands = @($diagnosticsRegister.Body.Instructions + $diagnosticsFailed.Body.Instructions + $diagnosticsReset.Body.Instructions + $diagnosticsUpdate.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$retryStrings = @(
			foreach ($method in $legacyRetryType.Methods) {
				if ($method.HasBody) {
					$method.Body.Instructions | Where-Object { $_.OpCode.Code -eq 'Ldstr' } | ForEach-Object { [string]$_.Operand }
				}
			}
		)
		$retryMethods = @($legacyRetryType.Methods.Name)
		$legacyConnectionRetryContract = @('Begin', 'HandleFailure', 'Update', 'BeforeReset', 'Connected') | Where-Object { $_ -notin $retryMethods } | Measure-Object | Select-Object -ExpandProperty Count
		$legacyConnectionRetryContract = $legacyConnectionRetryContract -eq 0 -and
			@($diagnosticsOperands | Where-Object { $_ -match 'LegacyConnectionRetryFix::Initialize\(' }).Count -eq 1 -and
			@($diagnosticsOperands | Where-Object { $_ -match 'LegacyConnectionRetryFix::HandleFailure\(' }).Count -eq 1 -and
			@($diagnosticsOperands | Where-Object { $_ -match 'LegacyConnectionRetryFix::BeforeReset\(' }).Count -eq 1 -and
			@($diagnosticsOperands | Where-Object { $_ -match 'LegacyConnectionRetryFix::Update\(' }).Count -eq 1 -and
			$retryStrings -contains 'MPATCHER_LEGACY_AUTO_RETRY_DISABLE' -and
			$retryStrings -contains 'REGISTERED version=1 scope=Legacy-GUID-only physicalAttempts=3 perAttemptTimeout=10 retryDelay=0.75 directHostData=unchanged admissionErrors=unchanged'

		$facilitatorRegister = $legacyFacilitatorType.Methods | Where-Object Name -eq 'TryRegister'
		$facilitatorStrings = @($facilitatorRegister.Body.Instructions | Where-Object { $_.OpCode.Code -eq 'Ldstr' } | ForEach-Object { [string]$_.Operand })
		$facilitatorOperands = @($facilitatorRegister.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyFacilitatorNativeContract = $facilitatorStrings -contains 'MPATCHER_LEGACY_FACILITATOR_FIX_DISABLE' -and
			$facilitatorStrings -contains '4AF49BD45AD8274B0269552B89EA0068C3397208869C478580C9C2CF283105EE' -and
			$facilitatorStrings -contains 'APPLIED version=1 rva=0x471254 events=DisconnectionNotification,ConnectionLost address=nat-facilitator role=client action=ignore-auxiliary-loss gameServerDisconnect=unchanged hostReconnect=unchanged scope=current-process-only exeFile=unchanged' -and
			@($facilitatorOperands | Where-Object { $_ -match 'VirtualProtect\(' }).Count -ge 2 -and
			@($facilitatorOperands | Where-Object { $_ -match 'FlushInstructionCache\(' }).Count -ge 2
	}

	$legacyHostType = $allTypes | Where-Object FullName -eq 'MPatcherFork.CustomPatches.LegacyHostStability'
	$masterFix = $allTypes | Where-Object FullName -eq 'MPatcherFork.CustomPatches.LegacyMasterLifecycleFix'
	$masterLayout = $allTypes | Where-Object FullName -eq 'MPatcherFork.CustomPatches.LegacyMasterLifecycleLayout'
	if ($null -ne $masterFix -and $null -ne $masterLayout) {
		$masterInstructions = @($masterFix.Methods | Where-Object HasBody | ForEach-Object { $_.Body.Instructions })
		$masterStrings = @($masterInstructions | Where-Object { $_.OpCode.Code -eq 'Ldstr' } | ForEach-Object { [string]$_.Operand })
		$masterOperands = @($masterInstructions | ForEach-Object { [string]$_.Operand })
		$legacyMasterLifecycleContract =
			$masterStrings -contains 'MPATCHER_LEGACY_MASTER_LIFECYCLE_FIX_DISABLE' -and
			$masterStrings -contains '4AF49BD45AD8274B0269552B89EA0068C3397208869C478580C9C2CF283105EE' -and
			$masterStrings -contains 'APPLIED version=1 profile=Unity-5.6.2f1-x86 cancelRva=0x4641FF cleared=registration+update queryPending=preserved transport=unchanged scope=current-process-only exeFile=unchanged' -and
			@($masterOperands | Where-Object { $_ -match 'LegacyMasterLifecycleLayout::BuildCancelStub\(' }).Count -eq 1 -and
			@($masterOperands | Where-Object { $_ -match 'UnityEngine\.Network::(Disconnect|InitializeServer|DestroyPlayerObjects)\(' }).Count -eq 0 -and
			@($masterOperands | Where-Object { $_ -match 'System\.IO\.File::(Write|Delete|Move|Copy)' }).Count -eq 0 -and
			@($masterLayout.Methods | Where-Object Name -eq 'BuildCancelStub').Count -eq 1
	}
	$legacyHostDriverType = $allTypes | Where-Object FullName -eq 'MPatcherFork.CustomPatches.LegacyHostStabilityDriver'
	$queryFix = $allTypes | Where-Object FullName -eq 'MPatcherFork.CustomPatches.LegacyMasterQueryFix'
	$queryLayout = $allTypes | Where-Object FullName -eq 'MPatcherFork.CustomPatches.LegacyMasterQueryLayout'
	$catalogueRecovery = $allTypes | Where-Object FullName -eq 'MPatcherFork.CustomPatches.LegacyCatalogueRecovery'
	$socketFix = $allTypes | Where-Object FullName -eq 'MPatcherFork.CustomPatches.LegacySocketSelfTestFix'
	$socketLayout = $allTypes | Where-Object FullName -eq 'MPatcherFork.CustomPatches.LegacySocketSelfTestLayout'
	if ($null -ne $socketFix -and $null -ne $socketLayout) {
		$socketInstructions = @($socketFix.Methods | Where-Object HasBody | ForEach-Object { $_.Body.Instructions })
		$socketStrings = @($socketInstructions | Where-Object { $_.OpCode.Code -eq 'Ldstr' } | ForEach-Object { [string]$_.Operand })
		$socketOperands = @($socketInstructions | ForEach-Object { [string]$_.Operand })
		$legacySocketSelfTestContract =
			$socketStrings -contains 'MPATCHER_LEGACY_SOCKET_SELF_TEST_FIX_DISABLE' -and
			$socketStrings -contains '4AF49BD45AD8274B0269552B89EA0068C3397208869C478580C9C2CF283105EE' -and
			@($socketStrings | Where-Object { $_ -like 'APPLIED version=2*scope=Initialize+Shutdown-self-send-only*' }).Count -eq 1 -and
			@($socketOperands | Where-Object { $_ -match 'LegacySocketSelfTestLayout::BuildStub\(' }).Count -eq 1 -and
			@($socketOperands | Where-Object { $_ -match 'UnityEngine\.Network::(Disconnect|InitializeServer|DestroyPlayerObjects)\(' }).Count -eq 0 -and
			@($socketOperands | Where-Object { $_ -match 'System\.IO\.File::(Write|Delete|Move|Copy)' }).Count -eq 0 -and
			@($bootstrapOperands | Where-Object { $_ -match 'LegacyCatalogueQueryPeer::Restart\(' }).Count -eq 0
	}
	if ($null -ne $queryFix -and $null -ne $queryLayout) {
		$queryInstructions = @($queryFix.Methods | Where-Object HasBody | ForEach-Object { $_.Body.Instructions })
		$queryStrings = @($queryInstructions | Where-Object { $_.OpCode.Code -eq 'Ldstr' } | ForEach-Object { [string]$_.Operand })
		$queryOperands = @($queryInstructions | ForEach-Object { [string]$_.Operand })
		$legacyMasterQueryContract =
			$queryStrings -contains 'MPATCHER_LEGACY_MASTER_QUERY_FIX_DISABLE' -and
			$queryStrings -contains '4AF49BD45AD8274B0269552B89EA0068C3397208869C478580C9C2CF283105EE' -and
			@($queryOperands | Where-Object { $_ -match 'LegacyMasterQueryLayout::BuildInitializeStub\(' }).Count -eq 1 -and
			@($queryOperands | Where-Object { $_ -match 'LegacyMasterQueryLayout::BuildFailureStub\(' }).Count -eq 1 -and
			@($queryOperands | Where-Object { $_ -match 'UnityEngine\.Network::(Disconnect|InitializeServer|DestroyPlayerObjects)\(' }).Count -eq 0 -and
			@($queryOperands | Where-Object { $_ -match 'System\.IO\.File::(Write|Delete|Move|Copy)' }).Count -eq 0
	}
	if ($null -ne $catalogueRecovery) {
		$catalogueInstructions = @($catalogueRecovery.Methods | Where-Object HasBody | ForEach-Object { $_.Body.Instructions })
		$catalogueStrings = @($catalogueInstructions | Where-Object { $_.OpCode.Code -eq 'Ldstr' } | ForEach-Object { [string]$_.Operand })
		$catalogueOperands = @($catalogueInstructions | ForEach-Object { [string]$_.Operand })
		$legacyCatalogueRecoveryContract =
			$catalogueStrings -contains 'OnFailedToConnectToMasterServer' -and
			$catalogueStrings -contains 'OCFLJMCKNHB' -and
			$catalogueStrings -contains 'JPDLDKABINE' -and
			$catalogueStrings -contains 'MPATCHER_LEGACY_CATALOGUE_REOPEN_DISABLE' -and
			@($catalogueOperands | Where-Object { $_ -match 'LegacyCatalogueRecoveryPolicy::CancelPhotonSearch\(' }).Count -eq 1 -and
			@($catalogueOperands | Where-Object { $_ -match 'LegacyCatalogueRecoveryPolicy::KeepBrowser\(' }).Count -eq 1 -and
			@($catalogueOperands | Where-Object { $_ -match 'LegacyCatalogueSocketPolicy::ShouldRestart\(' }).Count -eq 1 -and
			@($catalogueOperands | Where-Object { $_ -match 'LegacyCatalogueSocketPolicy::Reopened\(' }).Count -eq 1 -and
			@($catalogueOperands | Where-Object { $_ -match 'LegacyCatalogueQueryPeer::Restart\(' }).Count -eq 1 -and
			@($catalogueOperands | Where-Object { $_ -match 'UnityEngine\.Network::(Disconnect|InitializeServer)\(' }).Count -eq 0
	}
	$legacyHostPolicyType = $allTypes | Where-Object FullName -eq 'MPatcherFork.CustomPatches.LegacyHostDirectoryPolicy'
	$legacyHostFacilitatorType = $allTypes | Where-Object FullName -eq 'MPatcherFork.CustomPatches.LegacyHostFacilitatorReconnectFix'
	$legacyHostFacilitatorPolicyType = $allTypes | Where-Object FullName -eq 'MPatcherFork.CustomPatches.LegacyHostFacilitatorRetryPolicy'
	$legacyHostNativeLayoutType = $allTypes | Where-Object FullName -eq 'MPatcherFork.CustomPatches.LegacyHostNativePatchLayout'
	$legacyHostStabilityTypes = $null -ne $legacyHostType -and $null -ne $legacyHostDriverType -and $null -ne $legacyHostPolicyType
	$legacyHostFacilitatorTypes = $null -ne $legacyHostFacilitatorType -and $null -ne $legacyHostFacilitatorPolicyType -and $null -ne $legacyHostNativeLayoutType
	if ($legacyHostStabilityTypes) {
		$legacyHostTryRegister = $legacyHostType.Methods | Where-Object Name -eq 'TryRegister'
		$legacyHostCapture = $legacyHostType.Methods | Where-Object Name -eq 'RegisterOriginalAndCapture'
		$legacyHostDriverMethods = @($legacyHostDriverType.Methods | Where-Object HasBody)
		$legacyHostTryStrings = @($legacyHostTryRegister.Body.Instructions | Where-Object { $_.OpCode.Code -eq 'Ldstr' } | ForEach-Object { [string]$_.Operand })
		$legacyHostTryOperands = @($legacyHostTryRegister.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyHostCaptureOperands = @($legacyHostCapture.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyHostDriverOperands = @($legacyHostDriverMethods.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyHostPolicyStrings = @($legacyHostPolicyType.Methods | Where-Object HasBody | ForEach-Object { $_.Body.Instructions } | Where-Object { $_.OpCode.Code -eq 'Ldstr' } | ForEach-Object { [string]$_.Operand })
		$legacyHostForbiddenCalls = @($legacyHostDriverOperands | Where-Object {
			$_ -match 'UnityEngine\.Network::InitializeServer\(' -or
			$_ -match 'UnityEngine\.Network::Disconnect\(' -or
			$_ -match 'UnityEngine\.Network::DestroyPlayerObjects\('
		})
		$legacyHostDirectoryContract =
			$legacyHostTryStrings -contains 'REGISTERED version=3 scope=Legacy-host catalogueCheck=15 replyTimeout=12 retry=2,5,10,30 registrationAck=optional-with-bounded-list-verification endpointRefresh=network-change+60s liveSession=metadata facilitatorReconnect=separate-native-fix serverReinitialize=never' -and
			$legacyHostPolicyStrings -contains 'registration acknowledgement absent; checking catalogue independently' -and
			$legacyHostPolicyStrings -contains 'master operation failed; independent catalogue verification still pending' -and
			$legacyHostTryStrings -contains 'EOHFEIHAMHD' -and
			@($legacyHostTryOperands | Where-Object { $_ -eq 'Configure' }).Count -eq 1 -and
			@($legacyHostCaptureOperands | Where-Object { $_ -match 'LegacyHostStabilityDriver::Capture\(' }).Count -eq 1 -and
			@($legacyHostCaptureOperands | Where-Object { $_ -match 'UnityEngine\.MasterServer::RegisterHost\(' }).Count -eq 1 -and
			@($legacyHostDriverOperands | Where-Object { $_ -match 'UnityEngine\.MasterServer::RequestHostList\(' }).Count -eq 1 -and
			@($legacyHostDriverOperands | Where-Object { $_ -match 'UnityEngine\.MasterServer::PollHostList\(' }).Count -eq 1 -and
			@($legacyHostDriverOperands | Where-Object { $_ -match 'UnityEngine\.MasterServer::RegisterHost\(' }).Count -eq 1 -and
			$legacyHostForbiddenCalls.Count -eq 0

		if ($legacyHostFacilitatorTypes) {
			$legacyHostFacilitatorMethods = @($legacyHostFacilitatorType.Methods | Where-Object HasBody)
			$legacyHostFacilitatorStrings = @($legacyHostFacilitatorMethods.Body.Instructions | Where-Object { $_.OpCode.Code -eq 'Ldstr' } | ForEach-Object { [string]$_.Operand })
			$legacyHostFacilitatorOperands = @($legacyHostFacilitatorMethods.Body.Instructions | ForEach-Object { [string]$_.Operand })
			$legacyHostFacilitatorMethodNames = @($legacyHostFacilitatorType.Methods.Name)
			$facilitatorV1 = $legacyHostFacilitatorStrings -contains 'APPLIED version=1 profile=Unity-5.6.2f1-x86 lossRva=0x47125A connectedRva=0x470CF4 nativeFirstAttempt=unchanged managedRetry=8,15,30,max30 sessionReinitialize=never scope=current-process-only exeFile=unchanged sha256='
			$facilitatorV2 = $legacyHostFacilitatorStrings -contains 'APPLIED version=2 profile=Unity-5.6.2f1-x86 lossRva=0x47125A connectedRva=0x470CF4 independentHealth=RakPeer-IsConnected healthInterval=5 healthMissingSamples=2 nativeFirstAttempt=unchanged managedRetry=8,15,30,max30 sessionReinitialize=never scope=current-process-only exeFile=unchanged sha256='
			$facilitatorDriverContract = $false
			if ($facilitatorV1) {
				$facilitatorDriverContract =
					@($legacyHostDriverOperands | Where-Object { $_ -match 'LegacyHostFacilitatorReconnectFix::TryGetPending\(' }).Count -eq 1 -and
					@($legacyHostDriverOperands | Where-Object { $_ -match 'LegacyHostFacilitatorReconnectFix::TryReconnect\(' }).Count -eq 1 -and
					@($legacyHostDriverOperands | Where-Object { $_ -match 'LegacyHostFacilitatorReconnectFix::ClearPending\(' }).Count -eq 1
			}
			if ($facilitatorV2) {
				$healthType = $allTypes | Where-Object FullName -eq 'MPatcherFork.CustomPatches.LegacyHostFacilitatorHealthPolicy'
				$recoveryType = $allTypes | Where-Object FullName -eq 'MPatcherFork.CustomPatches.LegacyHostFacilitatorRecovery'
				$autoPortDriver = $allTypes | Where-Object FullName -eq 'MPatcherFork.CustomPatches.LegacyHostAutoPortDriver'
				if ($null -ne $healthType -and $null -ne $recoveryType -and $null -ne $autoPortDriver) {
					$recoveryOperands = @($recoveryType.Methods | Where-Object HasBody | ForEach-Object { $_.Body.Instructions } | ForEach-Object { [string]$_.Operand })
					$autoPortOperands = @($autoPortDriver.Methods | Where-Object HasBody | ForEach-Object { $_.Body.Instructions } | ForEach-Object { [string]$_.Operand })
					$missingMethods = @(@('ReadHealth', 'MarkMissing', 'ForgetHost', 'TryGetHostManager') | Where-Object { $_ -notin $legacyHostFacilitatorMethodNames })
					$forbiddenRecovery = @($recoveryOperands | Where-Object { $_ -match 'UnityEngine\.Network::(InitializeServer|Disconnect)\(' })
					$facilitatorDriverContract = $missingMethods.Count -eq 0 -and $forbiddenRecovery.Count -eq 0 -and
						@($legacyHostFacilitatorOperands | Where-Object { $_ -match 'LegacyHostNativePatchLayout::BuildConnectivityStub\(' }).Count -eq 1 -and
						@($legacyHostDriverOperands | Where-Object { $_ -match 'LegacyHostFacilitatorRecovery::Update\(' }).Count -eq 1 -and
						@($autoPortOperands | Where-Object { $_ -match 'LegacyHostFacilitatorRecovery::Update\(' }).Count -eq 1 -and
						@($recoveryOperands | Where-Object { $_ -match 'LegacyHostFacilitatorReconnectFix::ReadHealth\(' }).Count -ge 1 -and
						@($recoveryOperands | Where-Object { $_ -match 'LegacyHostFacilitatorReconnectFix::MarkMissing\(' }).Count -eq 1 -and
						@($recoveryOperands | Where-Object { $_ -match 'LegacyHostFacilitatorReconnectFix::TryReconnect\(' }).Count -eq 1 -and
						@($recoveryOperands | Where-Object { $_ -match 'LegacyHostFacilitatorReconnectFix::ClearPending\(' }).Count -eq 1
				}
			}
			$legacyHostFacilitatorNativeContract =
				@('TryGetPending', 'TryReconnect', 'ClearPending', 'WritePatch', 'TryRestore') | Where-Object { $_ -notin $legacyHostFacilitatorMethodNames } | Measure-Object | Select-Object -ExpandProperty Count
			$legacyHostFacilitatorNativeContract = $legacyHostFacilitatorNativeContract -eq 0 -and
				$legacyHostFacilitatorStrings -contains 'MPATCHER_LEGACY_HOST_FACILITATOR_RETRY_DISABLE' -and
				$legacyHostFacilitatorStrings -contains '4AF49BD45AD8274B0269552B89EA0068C3397208869C478580C9C2CF283105EE' -and
				($facilitatorV1 -or $facilitatorV2) -and $facilitatorDriverContract -and
				@($legacyHostFacilitatorOperands | Where-Object { $_ -match 'VirtualAlloc\(' }).Count -eq 1 -and
				@($legacyHostFacilitatorOperands | Where-Object { $_ -match 'VirtualFree\(' }).Count -ge 1 -and
				@($legacyHostFacilitatorOperands | Where-Object { $_ -match 'VirtualProtect\(' }).Count -ge 4 -and
				@($legacyHostFacilitatorOperands | Where-Object { $_ -match 'FlushInstructionCache\(' }).Count -ge 3 -and
				@($legacyHostFacilitatorOperands | Where-Object { $_ -match 'LegacyHostNativePatchLayout::BuildLossStub\(' }).Count -eq 1 -and
				@($legacyHostFacilitatorOperands | Where-Object { $_ -match 'LegacyHostNativePatchLayout::BuildConnectedStub\(' }).Count -eq 1 -and
				@($legacyHostFacilitatorOperands | Where-Object { $_ -match 'LegacyHostNativePatchLayout::BuildReconnectStub\(' }).Count -eq 1
		}
	}

	$legacyRecoveryType = $allTypes | Where-Object FullName -eq 'MPatcherFork.CustomPatches.LegacyTransientReconnect'
	$legacyRecoveryAgentType = $allTypes | Where-Object FullName -eq 'MPatcherFork.CustomPatches.LegacyTransientReconnectController'
	$legacyRecoveryConsentUiType = $allTypes | Where-Object FullName -eq 'MPatcherFork.CustomPatches.LegacyCrashResumeConsentUi'
	$legacyRecoveryConsentInputType = $allTypes | Where-Object FullName -eq 'MPatcherFork.CustomPatches.LegacyCrashResumeConsentInput'
	$legacyRecoveryTargetType = $allTypes | Where-Object FullName -eq 'MPatcherFork.CustomPatches.LegacyReconnectTarget'
	$legacyCrashMarkerType = $allTypes | Where-Object FullName -eq 'MPatcherFork.CustomPatches.LegacyCrashResumeMarker'
	$legacyCrashOwnerCheckpointType = $allTypes | Where-Object FullName -eq 'MPatcherFork.CustomPatches.LegacyCrashOwnerCheckpoint'
	$legacyGhostTransportType = $allTypes | Where-Object FullName -eq 'MPatcherFork.CustomPatches.LegacyGhostPoseTransportCodec'
	$legacyGhostSnapshotCodecType = $allTypes | Where-Object FullName -eq 'MPatcherFork.CustomPatches.LegacyGhostPoseSnapshotCodec'
	$legacyGhostVisualStateType = $allTypes | Where-Object FullName -eq 'MPatcherFork.CustomPatches.LegacyGhostPoseVisualState'
	$legacyGhostTransformStateType = $allTypes | Where-Object FullName -eq 'MPatcherFork.CustomPatches.LegacyGhostPoseTransformState'
	$legacyGhostAuxiliaryStateType = $allTypes | Where-Object FullName -eq 'MPatcherFork.CustomPatches.LegacyGhostPoseAuxiliaryState'
	$legacyMachineRecoveryTypes = $null -ne $legacyRecoveryType -and $null -ne $legacyRecoveryAgentType -and
		$null -ne $legacyRecoveryConsentUiType -and $null -ne $legacyRecoveryConsentInputType -and
		$null -ne $legacyRecoveryTargetType -and $null -ne $legacyCrashMarkerType -and
		$null -ne $legacyCrashOwnerCheckpointType -and
		$null -ne $legacyGhostTransportType -and
		$null -ne $legacyGhostSnapshotCodecType -and $null -ne $legacyGhostVisualStateType -and
		$null -ne $legacyGhostTransformStateType -and $null -ne $legacyGhostAuxiliaryStateType
	if ($legacyMachineRecoveryTypes) {
		$legacyRecoveryRegister = $legacyRecoveryType.Methods | Where-Object Name -eq 'TryRegister'
		$legacyRecoveryDisconnect = $legacyRecoveryType.Methods | Where-Object Name -eq 'PlayerDisconnectedPrefix'
		$legacyRecoveryConnect = $legacyRecoveryType.Methods | Where-Object Name -eq 'PlayerConnectedPrefix'
		$legacyRecoveryClientDisconnect = $legacyRecoveryType.Methods | Where-Object Name -eq 'GameDisconnectedPrefix'
		$legacyRecoveryGameUpdateTranspiler = $legacyRecoveryType.Methods | Where-Object Name -eq 'GameUpdateTranspiler'
		$legacyRecoverySafeAveragePing = $legacyRecoveryType.Methods | Where-Object Name -eq 'SafeAveragePing'
		$legacyRecoveryRpcPrefix = $legacyRecoveryType.Methods | Where-Object Name -eq 'NetworkViewRpcPrefix'
		$legacyRecoveryControllerCatchUp = $legacyRecoveryType.Methods | Where-Object Name -eq 'MachineControllerCatchUpPrefix'
		$legacyRecoveryControllerNamePostfix = $legacyRecoveryType.Methods | Where-Object Name -eq 'MachineControllerNamePostfix'
		$legacyRecoveryMachineSerialize = $legacyRecoveryType.Methods | Where-Object Name -eq 'MachineSerializePrefix'
		$legacyRecoveryGuidelinePrefix = $legacyRecoveryType.Methods | Where-Object Name -eq 'ReconnectGuidelinePrefix'
		$legacyRecoveryResumeDecision = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'ShouldResumeDisconnect'
		$legacyRecoveryCrashMarkerRetryDecision = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'ShouldPreserveCrashMarkerForRetry'
		$legacyRecoveryObserveNativeDisconnect = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'ObserveNativeDisconnect'
		$legacyRecoverySceneDestroy = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'NotifySceneDestroy'
		$legacyRecoveryMarkerPreservedLog = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'LogCrashMarkerPreserved'
		$legacyRecoveryRetain = $legacyRecoveryType.Methods | Where-Object Name -eq 'TryRetainPlayer'
		$legacyRecoveryFreeze = $legacyRecoveryType.Methods | Where-Object Name -eq 'FreezePlayerObjects'
		$legacyRecoveryTargetConnect = $legacyRecoveryTargetType.Methods | Where-Object Name -eq 'Connect'
		$legacyRecoveryTestDrop = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'ExecuteTestDrop'
		$legacyRecoveryClaim = $legacyRecoveryType.Methods | Where-Object Name -eq 'HandleClaim'
		$legacyRecoveryHello = $legacyRecoveryType.Methods | Where-Object Name -eq 'HandleHello'
		$legacyRecoveryCrashHello = $legacyRecoveryType.Methods | Where-Object Name -eq 'HandleCrashHello'
		$legacyRecoveryCrashDecision = $legacyRecoveryType.Methods | Where-Object Name -eq 'HandleCrashDecision'
		$legacyRecoveryBeginCrashRestore = $legacyRecoveryType.Methods | Where-Object Name -eq 'BeginCrashRestore'
		$legacyRecoveryCrashMachineComparison = $legacyRecoveryType.Methods | Where-Object Name -eq 'TryCompareCrashMachineConstruction'
		$legacyRecoveryCrashFingerprintExtract = $legacyRecoveryType.Methods | Where-Object Name -eq 'TryExtractOwnerConstructionFingerprint'
		$legacyRecoveryCrashIdentityExtract = $legacyRecoveryType.Methods | Where-Object Name -eq 'TryExtractOwnerConstructionIdentity'
		$legacyRecoveryCrashOwnerApplied = $legacyRecoveryType.Methods | Where-Object Name -eq 'HandleCrashOwnerApplied'
		$legacyRecoveryCrashServerApplied = $legacyRecoveryType.Methods | Where-Object Name -eq 'HandleCrashServerApplied'
		$legacyRecoveryCrashComplete = $legacyRecoveryType.Methods | Where-Object Name -eq 'TryCompleteCrashRestore'
		$legacyRecoveryDestroyTracked = $legacyRecoveryType.Methods | Where-Object Name -eq 'DestroyTrackedRetainedViews'
		$legacyRecoveryRetire = $legacyRecoveryType.Methods | Where-Object Name -eq 'HandleRetire'
		$legacyRecoveryRemoveViewBuffer = $legacyRecoveryType.Methods | Where-Object Name -eq 'RemoveViewBuffer'
		$legacyRecoverySendActiveRebinds = $legacyRecoveryType.Methods | Where-Object Name -eq 'SendActiveRebinds'
		$legacyRecoverySendGhostSnapshots = $legacyRecoveryType.Methods | Where-Object Name -eq 'SendRetainedGhostSnapshots'
		$legacyRecoveryCaptureGhost = $legacyRecoveryType.Methods | Where-Object Name -eq 'CaptureGhostSnapshot'
		$legacyRecoveryCaptureVisuals = $legacyRecoveryType.Methods | Where-Object Name -eq 'CaptureGhostVisualStates'
		$legacyRecoveryCollectBodies = $legacyRecoveryType.Methods | Where-Object Name -eq 'TryCollectMachineBodies'
		$legacyRecoveryFreezeBody = $legacyRecoveryType.Methods | Where-Object Name -eq 'FreezeBody'
		$legacyRecoveryReleaseFrozen = $legacyRecoveryType.Methods | Where-Object Name -eq 'ReleaseFrozen'
		$legacyRecoveryClientPrepare = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'PrepareClientRebind'
		$legacyRecoveryClientAuthorize = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'AuthorizeClientRebind'
		$legacyRecoveryClientRetire = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'TryRequestReboundViewRetire'
		$legacyRecoveryRetireRpc = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'MPatcherResumeRetireV2'
		$legacyRecoveryRetiredRpc = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'MPatcherResumeRetiredV2'
		$legacyRecoveryRpcDecision = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'ShouldSuppressOutgoingRpc'
		$legacyRecoverySerializeDecision = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'ShouldSuppressNativeMachineSerialize'
		$legacyRecoveryCatchUpDecision = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'ShouldSuppressNativeMachineCatchUp'
		$legacyRecoveryGuidelineDecision = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'ShouldSuppressReconnectGuideline'
		$legacyRecoveryApplicationQuit = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'OnApplicationQuit'
		$legacyRecoveryClientGate = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'GateClientMachineTraffic'
		$legacyRecoveryClientRelease = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'ReleaseClientMachineTraffic'
		$legacyRecoveryBroadcast = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'BroadcastRebind'
		$legacyRecoverySendRebind = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'SendRebind'
		$legacyRecoveryBroadcastRetire = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'BroadcastRetire'
		$legacyRecoveryUpdate = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'Update'
		$legacyRecoverySendHello = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'SendHello'
		$legacyRecoveryStartCrashFallbackTimer = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'StartCrashProtocolFallbackTimer'
		$legacyRecoveryStartCrashConsentTimer = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'StartCrashConsentCompatibilityTimer'
		$legacyRecoveryAbort = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'AbortRecovery'
		$legacyRecoveryNativeExit = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'InvokeNativeDisconnect'
		$legacyRecoveryGhostRpc = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'MPatcherResumeGhostPoseV2'
		$legacyRecoveryCrashHelloRpc = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'MPatcherCrashResumeHelloV3'
		$legacyRecoveryCrashHelloV4Rpc = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'MPatcherCrashResumeHelloV4'
		$legacyRecoveryCrashHelloV5Rpc = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'MPatcherCrashResumeHelloV5'
		$legacyRecoveryCrashHelloV6Rpc = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'MPatcherCrashResumeHelloV6'
		$legacyRecoveryCrashDecisionV4Rpc = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'MPatcherCrashResumeDecisionV4'
		$legacyRecoveryCrashDecisionV5Rpc = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'MPatcherCrashResumeDecisionV5'
		$legacyRecoveryCrashDecisionV6Rpc = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'MPatcherCrashResumeDecisionV6'
		$legacyRecoveryCrashPoseRpc = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'MPatcherCrashRestorePoseV3'
		$legacyRecoveryCrashAppliedRpc = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'MPatcherCrashRestoreAppliedV3'
		$legacyRecoveryCrashStatusRpc = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'MPatcherCrashResumeStatusV3'
		$legacyRecoveryShowCrashConsent = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'ShowCrashRestoreConsent'
		$legacyRecoverySubmitCrashConsent = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'SubmitCrashRestoreConsent'
		$legacyRecoverySendCrashConsent = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'SendCrashRestoreConsentDecision'
		$legacyRecoveryCloseCrashConsent = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'CloseCrashRestoreConsent'
		$legacyRecoveryConsentInputUpdate = $legacyRecoveryConsentInputType.Methods | Where-Object Name -eq 'Update'
		$legacyRecoveryConsentInputConfigure = $legacyRecoveryConsentInputType.Methods | Where-Object Name -eq 'Configure'
		$legacyRecoveryConsentInputLateUpdate = $legacyRecoveryConsentInputType.Methods | Where-Object Name -eq 'LateUpdate'
		$legacyRecoveryConsentInputHold = $legacyRecoveryConsentInputType.Methods | Where-Object Name -eq 'HoldInteractiveState'
		$legacyRecoveryConsentCameraHold = $legacyRecoveryConsentInputType.Methods | Where-Object Name -eq 'HoldCameraController'
		$legacyRecoveryConsentInputRestore = $legacyRecoveryConsentInputType.Methods | Where-Object Name -eq 'RestoreInteractiveState'
		$legacyRecoveryConsentCameraRestore = $legacyRecoveryConsentInputType.Methods | Where-Object Name -eq 'RestoreCameraController'
		$legacyRecoveryConsentInputDisable = $legacyRecoveryConsentInputType.Methods | Where-Object Name -eq 'OnDisable'
		$legacyRecoveryConsentInputDestroy = $legacyRecoveryConsentInputType.Methods | Where-Object Name -eq 'OnDestroy'
		$legacyRecoveryConsentCameraRestoreState = $legacyRecoveryConsentInputType.NestedTypes | Where-Object Name -like '*RestoreCameraAfterPointerFrame*'
		$legacyRecoveryConsentCameraRestoreMoveNext = $legacyRecoveryConsentCameraRestoreState.Methods | Where-Object Name -eq 'MoveNext'
		$legacyRecoveryCrashIdentity = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'GetCrashCheckpointIdentity'
		$legacyRecoveryFreshOwnerIdentity = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'TryGetCrashFreshOwnerIdentity'
		$legacyRecoveryReceiveGhost = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'ReceiveGhostPose'
		$legacyRecoveryReceiveGhostCore = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'ReceiveGhostPoseCore'
		$legacyRecoveryApplyGhosts = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'ApplyPendingGhostPoses'
		$legacyRecoveryApplyGhost = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'TryApplyGhostPose'
		$legacyRecoveryScheduleRemotePresentation = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'ScheduleRemoteCrashPresentationRefresh'
		$legacyRecoveryApplyPendingRemotePresentation = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'ApplyPendingRemoteCrashPresentationRefreshes'
		$legacyRecoveryCompleteRemotePresentation = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'CompleteRemoteCrashPresentation'
		$legacyRecoveryObserveNativePlayerName = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'ObserveNativePlayerName'
		$legacyRecoveryEnsureRemoteHostEarlyRegistration = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'EnsureRemoteCrashHostEarlyRegistration'
		$legacyRecoveryNormalizeRemoteHostRegistration = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'NormalizeRemoteCrashHostRegistration'
		$legacyRecoveryReapplyRemoteVisualSnapshot = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'TryReapplyRemoteCrashVisualSnapshot'
		$legacyRecoveryReconcileRemotePresentation = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'ReconcileRemoteCrashPresentation'
		$legacyRecoveryCaptureOwnerCheckpoint = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'CaptureOwnerCheckpoint'
		$legacyRecoveryLoadOwnerCheckpoint = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'TryLoadOwnerCheckpoint'
		$legacyRecoveryApplyOwnerCheckpoint = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'ApplyOwnerCheckpointWhenReady'
		$legacyRecoveryValidateOwnerCheckpoint = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'ValidateOwnerCheckpoint'
		$legacyRecoveryApplyOwnerJoints = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'ApplyOwnerCheckpointBrokenJoints'
		$legacyRecoveryOwnerDetached = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'IsOwnerCheckpointDetachedBody'
		$legacyRecoveryApplyOwnerBody = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'ApplyOwnerCheckpointBody'
		$legacyRecoveryLateUpdate = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'LateUpdate'
		$legacyRecoveryEnforceGhosts = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'EnforceClientGhostPoses'
		$legacyRecoveryApplyGhostBody = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'ApplyClientGhostBody'
		$legacyRecoveryApplyGhostVisual = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'ApplyClientGhostVisual'
		$legacyRecoveryApplyGhostTransform = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'ApplyClientGhostTransform'
		$legacyRecoveryReleaseGhost = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'ReleaseClientGhost'
		$legacyRecoveryApplyPeerRebind = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'ApplyOrQueuePeerRebind'
		$legacyRecoverySendGhost = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'SendGhostPose'
		$legacyRecoveryBroadcastCrashPose = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'BroadcastCrashRestorePose'
		$legacyRecoverySendCrashStatus = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'SendCrashStatus'
		$legacyRecoverySendCrashAck = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'SendCrashOwnerApplied'
		$legacyRecoveryMarkerArm = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'ArmActiveSession'
		$legacyRecoveryMarkerClear = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'ClearActiveSession'
		$legacyCrashMarkerEncode = $legacyCrashMarkerType.Methods | Where-Object Name -eq 'Encode'
		$legacyCrashMarkerDecode = $legacyCrashMarkerType.Methods | Where-Object Name -eq 'TryDecode'
		$legacyOwnerCheckpointSave = $legacyCrashOwnerCheckpointType.Methods | Where-Object Name -eq 'Save'
		$legacyOwnerCheckpointLoad = $legacyCrashOwnerCheckpointType.Methods | Where-Object Name -eq 'TryLoad'
		$legacyOwnerCheckpointClear = $legacyCrashOwnerCheckpointType.Methods | Where-Object Name -eq 'Clear'
		$legacyOwnerCheckpointEncode = $legacyCrashOwnerCheckpointType.Methods | Where-Object Name -eq 'TryEncodeEnvelope'
		$legacyOwnerCheckpointDecode = $legacyCrashOwnerCheckpointType.Methods | Where-Object Name -eq 'TryDecodeEnvelope'
		$legacyGhostTransportSplit = $legacyGhostTransportType.Methods | Where-Object Name -eq 'Split'
		$legacyGhostTransportJoin = $legacyGhostTransportType.Methods | Where-Object Name -eq 'TryJoin'
		$legacyGhostSnapshotEncode = $legacyGhostSnapshotCodecType.Methods | Where-Object Name -eq 'TryEncode'
		$legacyGhostSnapshotDecode = $legacyGhostSnapshotCodecType.Methods | Where-Object Name -eq 'TryDecode'
		$legacyRecoveryAgentMethods = @($legacyRecoveryAgentType.Methods | Where-Object HasBody)
		$legacyRecoveryAllMethods = @($legacyRecoveryType.Methods + $legacyRecoveryAgentMethods +
			$legacyRecoveryTargetType.Methods + $legacyCrashMarkerType.Methods +
			$legacyCrashOwnerCheckpointType.Methods + $legacyRecoveryConsentUiType.Methods +
			$legacyRecoveryConsentInputType.Methods |
			Where-Object HasBody)
		$legacyRecoveryRegisterStrings = @($legacyRecoveryRegister.Body.Instructions | Where-Object { $_.OpCode.Code -eq 'Ldstr' } | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryRegisterOperands = @($legacyRecoveryRegister.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryDisconnectOperands = @($legacyRecoveryDisconnect.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryConnectOperands = @($legacyRecoveryConnect.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryClientDisconnectOperands = @($legacyRecoveryClientDisconnect.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryGameUpdateTranspilerStrings = @($legacyRecoveryGameUpdateTranspiler.Body.Instructions | Where-Object { $_.OpCode.Code -eq 'Ldstr' } | ForEach-Object { [string]$_.Operand })
		$legacyRecoverySafeAveragePingOperands = @($legacyRecoverySafeAveragePing.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryRpcPrefixOperands = @($legacyRecoveryRpcPrefix.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryControllerCatchUpOperands = @($legacyRecoveryControllerCatchUp.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryControllerNamePostfixOperands = @($legacyRecoveryControllerNamePostfix.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryMachineSerializeOperands = @($legacyRecoveryMachineSerialize.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryGuidelinePrefixOperands = @($legacyRecoveryGuidelinePrefix.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryResumeDecisionOperands = @($legacyRecoveryResumeDecision.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryRetainOperands = @($legacyRecoveryRetain.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryFreezeOperands = @($legacyRecoveryFreeze.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryFreezeBodyOperands = @($legacyRecoveryFreezeBody.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryCaptureGhostOperands = @($legacyRecoveryCaptureGhost.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryCaptureGhostStrings = @($legacyRecoveryCaptureGhost.Body.Instructions | Where-Object { $_.OpCode.Code -eq 'Ldstr' } | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryCaptureVisualsOperands = @($legacyRecoveryCaptureVisuals.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryCollectBodiesOperands = @($legacyRecoveryCollectBodies.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryTargetConnectOperands = @($legacyRecoveryTargetConnect.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryTestDropOperands = @($legacyRecoveryTestDrop.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryClaimOperands = @($legacyRecoveryClaim.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryHelloStrings = @($legacyRecoveryHello.Body.Instructions | Where-Object { $_.OpCode.Code -eq 'Ldstr' } | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryHelloOperands = @($legacyRecoveryHello.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryCrashHelloOperands = @($legacyRecoveryCrashHello.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryCrashDecisionOperands = @($legacyRecoveryCrashDecision.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryCrashDecisionStrings = @($legacyRecoveryCrashDecision.Body.Instructions | Where-Object { $_.OpCode.Code -eq 'Ldstr' } | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryBeginCrashRestoreOperands = @($legacyRecoveryBeginCrashRestore.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryCrashMachineComparisonOperands = @($legacyRecoveryCrashMachineComparison.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryCrashIdentityOperands = @($legacyRecoveryCrashIdentity.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryFreshOwnerIdentityOperands = @($legacyRecoveryFreshOwnerIdentity.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryCrashCompleteOperands = @($legacyRecoveryCrashComplete.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryCrashCompleteStrings = @($legacyRecoveryCrashComplete.Body.Instructions | Where-Object { $_.OpCode.Code -eq 'Ldstr' } | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryDestroyTrackedOperands = @($legacyRecoveryDestroyTracked.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryRetireOperands = @($legacyRecoveryRetire.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryRemoveViewBufferOperands = @($legacyRecoveryRemoveViewBuffer.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoverySendActiveRebindsOperands = @($legacyRecoverySendActiveRebinds.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoverySendGhostSnapshotsOperands = @($legacyRecoverySendGhostSnapshots.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryReleaseFrozenOperands = @($legacyRecoveryReleaseFrozen.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryClientPrepareOperands = @($legacyRecoveryClientPrepare.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryClientAuthorizeOperands = @($legacyRecoveryClientAuthorize.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryClientRetireOperands = @($legacyRecoveryClientRetire.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryRetireRpcOperands = @($legacyRecoveryRetireRpc.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryRetiredRpcOperands = @($legacyRecoveryRetiredRpc.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryRpcDecisionOperands = @($legacyRecoveryRpcDecision.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoverySerializeDecisionOperands = @($legacyRecoverySerializeDecision.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryCatchUpDecisionStrings = @($legacyRecoveryCatchUpDecision.Body.Instructions | Where-Object { $_.OpCode.Code -eq 'Ldstr' } | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryGuidelineDecisionStrings = @($legacyRecoveryGuidelineDecision.Body.Instructions | Where-Object { $_.OpCode.Code -eq 'Ldstr' } | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryObserveNativeDisconnectOperands = @($legacyRecoveryObserveNativeDisconnect.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoverySceneDestroyOperands = @($legacyRecoverySceneDestroy.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryMarkerPreservedStrings = @($legacyRecoveryMarkerPreservedLog.Body.Instructions | Where-Object { $_.OpCode.Code -eq 'Ldstr' } | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryApplicationQuitOperands = @($legacyRecoveryApplicationQuit.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryClientGateOperands = @($legacyRecoveryClientGate.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryClientReleaseOperands = @($legacyRecoveryClientRelease.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryBroadcastOperands = @($legacyRecoveryBroadcast.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoverySendRebindOperands = @($legacyRecoverySendRebind.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryBroadcastRetireOperands = @($legacyRecoveryBroadcastRetire.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryUpdateOperands = @($legacyRecoveryUpdate.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoverySendHelloOperands = @($legacyRecoverySendHello.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryStartCrashFallbackTimerStrings = @($legacyRecoveryStartCrashFallbackTimer.Body.Instructions | Where-Object { $_.OpCode.Code -eq 'Ldstr' } | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryStartCrashConsentTimerStrings = @($legacyRecoveryStartCrashConsentTimer.Body.Instructions | Where-Object { $_.OpCode.Code -eq 'Ldstr' } | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryAbortOperands = @($legacyRecoveryAbort.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryNativeExitOperands = @($legacyRecoveryNativeExit.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryGhostRpcOperands = @($legacyRecoveryGhostRpc.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryCrashHelloRpcOperands = @($legacyRecoveryCrashHelloRpc.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryCrashHelloV4RpcOperands = @($legacyRecoveryCrashHelloV4Rpc.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryCrashHelloV5RpcOperands = @($legacyRecoveryCrashHelloV5Rpc.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryCrashHelloV6RpcOperands = @($legacyRecoveryCrashHelloV6Rpc.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryCrashDecisionV4RpcOperands = @($legacyRecoveryCrashDecisionV4Rpc.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryCrashDecisionV5RpcOperands = @($legacyRecoveryCrashDecisionV5Rpc.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryCrashDecisionV6RpcOperands = @($legacyRecoveryCrashDecisionV6Rpc.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryCrashPoseRpcOperands = @($legacyRecoveryCrashPoseRpc.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryCrashAppliedRpcOperands = @($legacyRecoveryCrashAppliedRpc.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryCrashStatusRpcOperands = @($legacyRecoveryCrashStatusRpc.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoverySubmitCrashConsentOperands = @($legacyRecoverySubmitCrashConsent.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoverySendCrashConsentOperands = @($legacyRecoverySendCrashConsent.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryConsentCopy = $legacyRecoveryConsentUiType.Methods | Where-Object Name -eq 'ApplyCopy'
		$legacyRecoveryConsentPromptStyle = $legacyRecoveryConsentUiType.Methods | Where-Object Name -eq 'ApplyPromptStyle'
		$legacyRecoveryConsentUiClose = $legacyRecoveryConsentUiType.Methods | Where-Object Name -eq 'Close'
		$legacyRecoveryConsentCopyCalls = @($legacyRecoveryConsentCopy.Body.Instructions |
			Where-Object { $_.OpCode.Code -in @('Call', 'Callvirt') } |
			ForEach-Object { [string]$_.Operand })
		$legacyRecoveryConsentPromptStyleCalls = @($legacyRecoveryConsentPromptStyle.Body.Instructions |
			Where-Object { $_.OpCode.Code -in @('Call', 'Callvirt') } |
			ForEach-Object { [string]$_.Operand })
		$legacyRecoveryConsentConfigureOperands = @($legacyRecoveryConsentInputConfigure.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryConsentLateUpdateOperands = @($legacyRecoveryConsentInputLateUpdate.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryConsentHoldOperands = @($legacyRecoveryConsentInputHold.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryConsentCameraHoldOperands = @($legacyRecoveryConsentCameraHold.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryConsentRestoreOperands = @($legacyRecoveryConsentInputRestore.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryConsentCameraRestoreOperands = @($legacyRecoveryConsentCameraRestore.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryConsentCameraRestoreMoveNextOperands = @($legacyRecoveryConsentCameraRestoreMoveNext.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryConsentCloseOperands = @($legacyRecoveryConsentUiClose.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryConsentDisableOperands = @($legacyRecoveryConsentInputDisable.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryConsentDestroyOperands = @($legacyRecoveryConsentInputDestroy.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryConsentUiStrings = @($legacyRecoveryConsentUiType.Methods | Where-Object HasBody | ForEach-Object { $_.Body.Instructions } | Where-Object { $_.OpCode.Code -eq 'Ldstr' } | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryConsentUiOperands = @($legacyRecoveryConsentUiType.Methods | Where-Object HasBody | ForEach-Object { $_.Body.Instructions } | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryConsentInputOperands = @($legacyRecoveryConsentInputUpdate.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryReceiveGhostOperands = @($legacyRecoveryReceiveGhost.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryReceiveGhostCoreOperands = @($legacyRecoveryReceiveGhostCore.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryApplyGhostsOperands = @($legacyRecoveryApplyGhosts.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryApplyGhostOperands = @($legacyRecoveryApplyGhost.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryApplyGhostStrings = @($legacyRecoveryApplyGhost.Body.Instructions | Where-Object { $_.OpCode.Code -eq 'Ldstr' } | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryScheduleRemotePresentationOperands = @($legacyRecoveryScheduleRemotePresentation.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryApplyPendingRemotePresentationOperands = @($legacyRecoveryApplyPendingRemotePresentation.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryCompleteRemotePresentationOperands = @($legacyRecoveryCompleteRemotePresentation.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryCompleteRemotePresentationStrings = @($legacyRecoveryCompleteRemotePresentation.Body.Instructions | Where-Object { $_.OpCode.Code -eq 'Ldstr' } | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryObserveNativePlayerNameOperands = @($legacyRecoveryObserveNativePlayerName.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryObserveNativePlayerNameStrings = @($legacyRecoveryObserveNativePlayerName.Body.Instructions | Where-Object { $_.OpCode.Code -eq 'Ldstr' } | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryEnsureRemoteHostEarlyRegistrationOperands = @($legacyRecoveryEnsureRemoteHostEarlyRegistration.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryEnsureRemoteHostEarlyRegistrationStrings = @($legacyRecoveryEnsureRemoteHostEarlyRegistration.Body.Instructions | Where-Object { $_.OpCode.Code -eq 'Ldstr' } | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryNormalizeRemoteHostRegistrationOperands = @($legacyRecoveryNormalizeRemoteHostRegistration.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryNormalizeRemoteHostRegistrationStrings = @($legacyRecoveryNormalizeRemoteHostRegistration.Body.Instructions | Where-Object { $_.OpCode.Code -eq 'Ldstr' } | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryResetMotion = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'ResetOwnerMotionHistory'
		$legacyRecoveryResetMotionOperands = @($legacyRecoveryResetMotion.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryReconcileRemotePresentationOperands = @($legacyRecoveryReconcileRemotePresentation.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryReconcileRemotePresentationStrings = @($legacyRecoveryReconcileRemotePresentation.Body.Instructions | Where-Object { $_.OpCode.Code -eq 'Ldstr' } | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryCaptureOwnerCheckpointOperands = @($legacyRecoveryCaptureOwnerCheckpoint.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryLoadOwnerCheckpointOperands = @($legacyRecoveryLoadOwnerCheckpoint.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryValidateOwnerCheckpointOperands = @($legacyRecoveryValidateOwnerCheckpoint.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryApplyOwnerCheckpointOperands = @($legacyRecoveryApplyOwnerCheckpoint.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryApplyOwnerCheckpointStrings = @($legacyRecoveryApplyOwnerCheckpoint.Body.Instructions | Where-Object { $_.OpCode.Code -eq 'Ldstr' } | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryApplyOwnerJointsOperands = @($legacyRecoveryApplyOwnerJoints.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryApplyOwnerBodyOperands = @($legacyRecoveryApplyOwnerBody.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryLateUpdateOperands = @($legacyRecoveryLateUpdate.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryEnforceGhostsOperands = @($legacyRecoveryEnforceGhosts.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryApplyGhostBodyOperands = @($legacyRecoveryApplyGhostBody.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryApplyGhostVisualOperands = @($legacyRecoveryApplyGhostVisual.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryApplyGhostTransformOperands = @($legacyRecoveryApplyGhostTransform.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryReleaseGhostOperands = @($legacyRecoveryReleaseGhost.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryApplyPeerRebindOperands = @($legacyRecoveryApplyPeerRebind.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoverySendGhostOperands = @($legacyRecoverySendGhost.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryBroadcastCrashPoseOperands = @($legacyRecoveryBroadcastCrashPose.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryMarkerArmOperands = @($legacyRecoveryMarkerArm.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryMarkerClearOperands = @($legacyRecoveryMarkerClear.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyCrashMarkerDecodeStrings = @($legacyCrashMarkerDecode.Body.Instructions | Where-Object { $_.OpCode.Code -eq 'Ldstr' } | ForEach-Object { [string]$_.Operand })
		$legacyOwnerCheckpointDecodeStrings = @($legacyOwnerCheckpointDecode.Body.Instructions | Where-Object { $_.OpCode.Code -eq 'Ldstr' } | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryAgentOperands = @($legacyRecoveryAgentMethods.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryStrings = @($legacyRecoveryAllMethods.Body.Instructions | Where-Object { $_.OpCode.Code -eq 'Ldstr' } | ForEach-Object { [string]$_.Operand })
		$legacyRecoveryRpcMethods = @($legacyRecoveryAgentType.Methods | Where-Object {
			@($_.CustomAttributes | Where-Object { $_.AttributeType.FullName -eq 'UnityEngine.RPC' }).Count -eq 1
		})
		$legacyRecoveryForbidden = @($legacyRecoveryAllMethods.Body.Instructions | ForEach-Object { [string]$_.Operand } | Where-Object {
			$_ -match 'UnityEngine\.Network::InitializeServer\(' -or
			$_ -match 'SceneMan::CJLFFPJICPC\(' -or
			$_ -match '::ShowDebugMsg\(' -or
			$_ -match '::DP\.D\('
		})
		$legacyRecoveryUnsafeSuspend = @($legacyRecoveryAgentMethods | Where-Object Name -ne 'ApplyOwnerCheckpointBody' |
			ForEach-Object { $_.Body.Instructions } | ForEach-Object { [string]$_.Operand } |
			Where-Object { $_ -match 'McnCraft\.BodyController::Suspend\(' })
		$legacyMachineRecoveryContract =
			@($legacyRecoveryRegisterStrings | Where-Object {
				$_ -like 'REGISTERED version=48 machineChangeAnchor=native-bounds-without-missile-templates*' -and
				$_ -like '*playerList=event-driven-reference-identity-all-peers tagRetire=explicit-before-machine-destroy crashConsentInput=unity-button+raw-mouse+keyboard+auto-cursor+modal-game-input+camera-controller-suspended*' -and
				$_ -like '*crashConsentUi=native-dialog-white-normal-en-ja*' -and
				$_ -like '*previousConnection=cleared-after-crash-restore*' -and
				$_ -like '*crashMachineIdentity=owner-checkpoint+fresh-owner-preflight+fresh-owner-decision+owner-apply crashConsent=same-construction-owner-choice-before-pose*' -and
				$_ -like '*crashHelloTimeout=v6-to-v3-5s+v3-to-v2-30s*'
			}).Count -eq 1 -and
			@($legacyRecoveryType.Methods.Name | Where-Object { $_ -in @('GameUpdateTranspiler', 'SafeAveragePing', 'NetworkViewRpcPrefix', 'ReconnectGuidelinePrefix', 'MachineControllerCatchUpPrefix', 'MachineControllerNamePostfix', 'GameDisconnectedPrefix', 'PlayerConnectedPrefix', 'PlayerDisconnectedPrefix', 'MachineCatchUpPrefix', 'MachineSerializePrefix', 'PrepareReconnectPlayer', 'TryRetainPlayer', 'FreezePlayerObjects', 'FindMachineForView', 'TryCollectMachineBodies', 'AddMachineBody', 'FreezeBody', 'CaptureGhostSnapshot', 'CaptureGhostVisualStates', 'CaptureGhostTransform', 'DescribeFirstVisual', 'NextGhostSnapshotId', 'HandleHello', 'HandleCrashHello', 'HandleCrashDecision', 'BeginCrashRestore', 'TryCompareCrashMachineConstruction', 'TryExtractOwnerConstructionFingerprint', 'TryExtractOwnerConstructionIdentity', 'IsConstructionFingerprint', 'HandleCrashOwnerApplied', 'HandleCrashServerApplied', 'TryCompleteCrashRestore', 'DestroyTrackedRetainedViews', 'RegisterCrashFallback', 'ReplaceRetainedWithCrashFallback', 'PromoteRetainedToCrashFallback', 'ResetCrashRestore', 'HandleClaim', 'CompleteServerRebind', 'HandleGoodbye', 'HandleTestDrop', 'TryRetireReboundView', 'HandleRetire', 'RemoveViewBuffer', 'SendActiveRebinds', 'SendRetainedGhostSnapshots', 'FindViewById') }).Count -eq 49 -and
			@($legacyRecoveryAgentType.Methods.Name | Where-Object { $_ -in @('BeginRecovery', 'TryReconnect', 'OnConnectedToServer', 'OnFailedToConnect', 'NotifyVoluntaryExit', 'OnApplicationQuit', 'LogClientInventory', 'ScheduleTestDrop', 'ExecuteTestDrop', 'CheckTestDropRequest', 'ShouldResumeDisconnect', 'ShouldPreserveCrashMarkerForRetry', 'ShouldSuppressNativeMachineCatchUp', 'ShouldSuppressNativeMachineSerialize', 'ShouldSuppressOutgoingRpc', 'ShouldSuppressReconnectGuideline', 'ObserveEmptyPingGuard', 'CaptureClientClaims', 'GateClientMachineTraffic', 'PrepareClientRebind', 'ClientClaimsPrepared', 'AuthorizeClientRebind', 'SendClaims', 'ReleaseClientMachineTraffic', 'ReceiveGhostPose', 'ReceiveGhostPoseCore', 'ApplyPendingGhostPoses', 'ScheduleRemoteCrashPresentationRefresh', 'ApplyPendingRemoteCrashPresentationRefreshes', 'CompleteRemoteCrashPresentation', 'ObserveNativePlayerName', 'EnsureRemoteCrashHostEarlyRegistration', 'CountRemoteMachineOccurrences', 'NormalizeRemoteCrashHostRegistration', 'ResetOwnerMotionHistory', 'ReconcileRemoteCrashPresentation', 'TryApplyGhostPose', 'CaptureOwnerCheckpoint', 'GetCrashCheckpointIdentity', 'TryGetCrashFreshOwnerIdentity', 'TryLoadOwnerCheckpoint', 'BuildOwnerCheckpointFingerprint', 'TryApplyOwnerCheckpointPose', 'ApplyOwnerCheckpointBrokenJoints', 'IsOwnerCheckpointDetachedBody', 'ApplyOwnerCheckpointBody', 'ShouldApplySnapshotDamage', 'LateUpdate', 'EnforceClientGhostPoses', 'ApplyClientGhostBody', 'ApplyClientGhostVisual', 'ApplyClientGhostTransform', 'FreezeClientBody', 'ReleaseClientGhost', 'ReleaseFrozenClientBodies', 'ReleaseAllClientGhosts', 'CleanupClientGhosts', 'TryGetCrashFreshView', 'TryGetLocalCrashMachineReadiness', 'CrashViewUsable', 'GateCrashResumeTraffic', 'ReleaseCrashResumeTraffic', 'SendHello', 'StartCrashConsentCompatibilityTimer', 'StartCrashProtocolFallbackTimer', 'ShowCrashRestoreConsent', 'SubmitCrashRestoreConsent', 'SendCrashRestoreConsentDecision', 'CloseCrashRestoreConsent', 'SendGhostPose', 'BroadcastCrashRestorePose', 'SendCrashStatus', 'SendCrashOwnerApplied', 'ArmActiveSession', 'ClearActiveSession', 'LogCrashMarkerPreserved', 'BroadcastRebind', 'ObserveNativeDisconnect', 'AbortRecovery', 'InvokeNativeDisconnect', 'TryRequestReboundViewRetire', 'SendRebind', 'BroadcastRetire') }).Count -eq 83 -and
			@($legacyCrashOwnerCheckpointType.Methods.Name | Where-Object { $_ -in @('Save', 'TryLoad', 'Clear', 'TryEncodeEnvelope', 'TryDecodeEnvelope') }).Count -eq 5 -and
			@($legacyRecoveryAgentType.Fields | Where-Object Name -eq 'NativeStructureReadyField').Count -eq 1 -and
			@($legacyRecoveryAgentType.Fields | Where-Object Name -eq 'nativeCrashPlacementObserved').Count -eq 0 -and
			@($legacyRecoveryRpcMethods | Where-Object { $_.Name -notlike 'MPatcherMigration*' }).Count -eq 19 -and
			-not ($legacyRecoveryRegisterStrings -contains 'LocateSelf') -and
			-not ($legacyRecoveryRegisterStrings -contains 'PlayLocateSelfPostfix') -and
			$legacyRecoveryRegisterStrings -contains 'RPC_SyncStructure' -and
			$legacyRecoveryRegisterStrings -contains 'RPC_SyncCollider' -and
			$legacyRecoveryRegisterStrings -contains 'RPC_SyncStampTexture' -and
			$legacyRecoveryRegisterStrings -contains 'RPC_SyncPlayerName' -and
			$legacyRecoveryRegisterStrings -contains 'MachineControllerNamePostfix' -and
			$legacyRecoveryRegisterStrings -contains 'GameUpdateTranspiler' -and
			$legacyRecoveryGameUpdateTranspilerStrings -contains 'SafeAveragePing' -and
			$legacyRecoveryGameUpdateTranspilerStrings -contains 'Game.Update average-ping pattern count=' -and
			@($legacyRecoverySafeAveragePingOperands | Where-Object { $_ -match 'UnityEngine\.Network::get_connections\(' }).Count -eq 1 -and
			@($legacyRecoverySafeAveragePingOperands | Where-Object { $_ -match 'UnityEngine\.Network::GetAveragePing\(' }).Count -eq 1 -and
			@($legacyRecoverySafeAveragePingOperands | Where-Object { $_ -match 'LegacyTransientReconnectController::ObserveEmptyPingGuard\(' }).Count -eq 1 -and
			@($legacyRecoveryRpcPrefixOperands | Where-Object { $_ -match 'LegacyTransientReconnectController::ShouldSuppressOutgoingRpc\(' }).Count -eq 1 -and
			@($legacyRecoveryGuidelinePrefixOperands | Where-Object { $_ -match 'LegacyTransientReconnectController::ShouldSuppressReconnectGuideline\(' }).Count -eq 1 -and
			$legacyRecoveryGuidelineDecisionStrings -contains 'CLIENT_RECONNECT_GUIDELINE_SUPPRESSED rpc=' -and
			@($legacyRecoveryObserveNativeDisconnectOperands | Where-Object { $_ -match 'LegacyTransientReconnectController::ShouldPreserveCrashMarkerForRetry\(' }).Count -eq 1 -and
			@($legacyRecoveryObserveNativeDisconnectOperands | Where-Object { $_ -match 'LegacyTransientReconnectController::LogCrashMarkerPreserved\(' }).Count -eq 1 -and
			@($legacyRecoverySceneDestroyOperands | Where-Object { $_ -match 'LegacyTransientReconnectController::ShouldPreserveCrashMarkerForRetry\(' }).Count -eq 1 -and
			@($legacyRecoverySceneDestroyOperands | Where-Object { $_ -match 'LegacyTransientReconnectController::LogCrashMarkerPreserved\(' }).Count -eq 1 -and
			$legacyRecoveryMarkerPreservedStrings -contains 'CLIENT_CRASH_MARKER_PRESERVED source=' -and
			@($legacyRecoveryControllerCatchUpOperands | Where-Object { $_ -match 'LegacyTransientReconnectController::ShouldSuppressNativeMachineCatchUp\(' }).Count -ge 1 -and
			@($legacyRecoveryMachineSerializeOperands | Where-Object { $_ -match 'LegacyTransientReconnectController::ShouldSuppressNativeMachineSerialize\(' }).Count -eq 1 -and
			@($legacyRecoverySerializeDecisionOperands | Where-Object { $_ -match 'LegacyTransientReconnectController::ShouldSuppressNativeMachineCatchUp\(' }).Count -eq 1 -and
			$legacyRecoveryCatchUpDecisionStrings -contains 'CLIENT_GHOST_NATIVE_PACKET_SUPPRESSED source=' -and
			$legacyRecoveryRegisterStrings -contains 'OnSerializeNetworkView' -and
			$legacyRecoveryResumeDecisionOperands -contains '20' -and
			@($legacyRecoveryClientDisconnectOperands | Where-Object { $_ -match 'LegacyTransientReconnectController::BeginRecovery\(' }).Count -eq 1 -and
			@($legacyRecoveryDisconnectOperands | Where-Object { $_ -match 'LegacyTransientReconnect::TryRetainPlayer\(' }).Count -eq 1 -and
			@($legacyRecoveryConnectOperands | Where-Object { $_ -match 'LegacyTransientReconnect::PrepareReconnectPlayer\(' }).Count -eq 1 -and
			@($legacyRecoveryRetainOperands | Where-Object { $_ -match 'LegacyTransientReconnect::FreezePlayerObjects\(' }).Count -eq 1 -and
			@($legacyRecoveryFreezeOperands | Where-Object { $_ -match 'UnityEngine\.Object::FindObjectsOfType\(' }).Count -eq 1 -and
			@($legacyRecoveryFreezeOperands | Where-Object { $_ -match 'ResumeRecord::BufferedViewIds' }).Count -ge 3 -and
			@($legacyRecoveryFreezeOperands | Where-Object { $_ -match 'LegacyTransientReconnect::CaptureGhostSnapshot\(' }).Count -eq 1 -and
			@($legacyRecoveryFreezeBodyOperands | Where-Object { $_ -match 'UnityEngine\.Rigidbody::set_isKinematic\(' }).Count -eq 1 -and
			@($legacyRecoveryCaptureGhostOperands | Where-Object { $_ -match 'LegacyGhostPoseSnapshotCodec::TryEncode\(' }).Count -eq 1 -and
			@($legacyRecoveryCaptureGhostOperands | Where-Object { $_ -match 'LegacyTransientReconnect::CaptureGhostVisualStates\(' }).Count -eq 1 -and
			@($legacyRecoveryCaptureGhostOperands | Where-Object { $_ -match 'LegacyMachineRecoveryFingerprint::TryCompute\(' }).Count -eq 0 -and
			$legacyRecoveryCaptureGhostStrings -contains ';bodies=' -and
			$legacyRecoveryCaptureGhostStrings -contains ';visuals=' -and
			@($legacyRecoveryCaptureVisualsOperands | Where-Object { $_ -match 'McnCraft\.MachineController::CFECMHAACID' }).Count -ge 1 -and
			@($legacyRecoveryCaptureVisualsOperands | Where-Object { $_ -match 'UnityEngine\.Transform::get_localScale\(' }).Count -ge 1 -and
			@($legacyRecoveryCollectBodiesOperands | Where-Object { $_ -match 'UnityEngine\.Object::FindObjectsOfType\(' }).Count -eq 1 -and
			@($legacyRecoveryCaptureGhostOperands | Where-Object { $_ -match 'LegacyGhostPoseTransportCodec::Split\(' }).Count -eq 1 -and
			@($legacyRecoveryTargetConnectOperands | Where-Object { $_ -match 'UnityEngine\.Network::Connect\(System.String\)' }).Count -eq 1 -and
			@($legacyRecoveryTargetConnectOperands | Where-Object { $_ -match 'UnityEngine\.Network::Connect\(System.String\[\],System.Int32\)' }).Count -eq 1 -and
			@($legacyRecoveryTestDropOperands | Where-Object { $_ -match 'UnityEngine\.Network::CloseConnection\(UnityEngine.NetworkPlayer,System.Boolean\)' }).Count -eq 1 -and
			$legacyRecoveryHelloStrings -contains 'session-unavailable' -and
			$legacyRecoveryHelloStrings -contains 'registered-after-retained-discard' -and
			@($legacyRecoveryHelloOperands | Where-Object { $_ -match 'LegacyTransientReconnect::DestroyRetained\(' }).Count -eq 1 -and
			@($legacyRecoveryClaimOperands | Where-Object { $_ -match 'UnityEngine\.NetworkView::set_viewID\(' }).Count -eq 1 -and
			@($legacyRecoveryClaimOperands | Where-Object { $_ -match 'UnityEngine\.NetworkView::Find\(' }).Count -eq 0 -and
			@($legacyRecoveryHelloOperands | Where-Object { $_ -match 'LegacyTransientReconnect::SendActiveRebinds\(' }).Count -eq 2 -and
			@($legacyRecoveryHelloOperands | Where-Object { $_ -match 'LegacyTransientReconnect::SendRetainedGhostSnapshots\(' }).Count -eq 2 -and
			@($legacyRecoverySendActiveRebindsOperands | Where-Object { $_ -match 'LegacyTransientReconnectController::SendRebind\(' }).Count -eq 1 -and
			@($legacyRecoverySendActiveRebindsOperands | Where-Object { $_ -match 'ResumeRecord::BufferedViewIds' }).Count -ge 1 -and
			@($legacyRecoverySendGhostSnapshotsOperands | Where-Object { $_ -match 'LegacyTransientReconnectController::SendRebind\(' }).Count -eq 1 -and
			@($legacyRecoverySendGhostSnapshotsOperands | Where-Object { $_ -match 'GhostViewSnapshot::BufferedViewId' }).Count -ge 1 -and
			@($legacyRecoverySendGhostSnapshotsOperands | Where-Object { $_ -match 'LegacyTransientReconnectController::SendGhostPose\(' }).Count -eq 1 -and
			@($legacyRecoveryClientRetireOperands | Where-Object { $_ -match 'UnityEngine\.NetworkView::RPC\(' }).Count -eq 1 -and
			@($legacyRecoveryRetireRpcOperands | Where-Object { $_ -match 'LegacyTransientReconnect::HandleRetire\(' }).Count -eq 1 -and
			@($legacyRecoveryRetireOperands | Where-Object { $_ -match 'UnityEngine\.Network::Destroy\(UnityEngine.GameObject\)' }).Count -eq 1 -and
			@($legacyRecoveryRetireOperands | Where-Object { $_ -match 'LegacyTransientReconnect::RemoveViewBuffer\(' }).Count -eq 1 -and
			@($legacyRecoveryRetireOperands | Where-Object { $_ -match 'LegacyTransientReconnectController::BroadcastRetire\(' }).Count -eq 2 -and
			@($legacyRecoveryRetireOperands | Where-Object { $_ -match 'LegacyRetirementIds::Collect' }).Count -eq 1 -and
			@($legacyRecoveryRetireOperands | Where-Object { $_ -match 'ResumeRecord::BufferedViewIds' }).Count -ge 2 -and
			@($legacyRecoveryReceiveGhostCoreOperands | Where-Object { $_ -match 'LegacyRestoreDeliveryLedger.*::Observe\(' }).Count -eq 1 -and
			@($legacyRecoveryRetireOperands | Where-Object { $_ -match 'UnityEngine\.Object::Destroy\(' }).Count -eq 1 -and
			@($legacyRecoveryRemoveViewBufferOperands | Where-Object { $_ -match 'UnityEngine\.Network::RemoveRPCs\(UnityEngine.NetworkViewID\)' }).Count -eq 1 -and
			@($legacyRecoveryReleaseFrozenOperands | Where-Object { $_ -match 'System.Collections.Generic.List.*::Clear\(' }).Count -eq 1 -and
			@($legacyRecoveryRetiredRpcOperands | Where-Object { $_ -match 'LegacyTransientReconnectController::RetireClientView\(' }).Count -eq 3 -and
			@($legacyRecoveryRetiredRpcOperands | Where-Object { $_ -match 'LegacyTransientReconnectController::IsServerMessage\(' }).Count -eq 1 -and
			@($legacyRecoveryControllerNamePostfixOperands | Where-Object { $_ -match 'LegacyPlayerList::Normalize\(' }).Count -eq 1 -and
			$legacyRecoveryRegisterStrings -contains 'MachineDestroyedPrefix' -and
			@($legacyRecoverySendRebindOperands | Where-Object { $_ -match 'UnityEngine\.NetworkView::RPC\(' }).Count -eq 1 -and
			@($legacyRecoveryBroadcastRetireOperands | Where-Object { $_ -match 'UnityEngine\.NetworkView::RPC\(' }).Count -eq 1 -and
			@($legacyRecoveryClientPrepareOperands | Where-Object { $_ -match 'UnityEngine\.Network::AllocateViewID\(' }).Count -eq 1 -and
			@($legacyRecoveryClientPrepareOperands | Where-Object { $_ -match 'UnityEngine\.NetworkView::set_viewID\(' }).Count -eq 1 -and
			@($legacyRecoveryClientAuthorizeOperands | Where-Object { $_ -match 'UnityEngine\.Time::get_realtimeSinceStartup\(' }).Count -eq 1 -and
			@($legacyRecoveryRpcDecisionOperands | Where-Object { $_ -match 'UnityEngine\.Network::get_isClient\(' }).Count -eq 1 -and
			@($legacyRecoveryApplicationQuitOperands | Where-Object { $_ -match 'LegacyTransientReconnectController::NotifyVoluntaryExit\(' }).Count -eq 1 -and
			@($legacyRecoveryApplicationQuitOperands | Where-Object { $_ -match 'UnityEngine\.Network::Disconnect\(System.Int32\)' }).Count -eq 1 -and
			@($legacyRecoveryUpdateOperands | Where-Object { $_ -match 'LegacyTransientReconnectController::PrepareClientRebind\(' }).Count -eq 1 -and
			@($legacyRecoveryLateUpdateOperands | Where-Object { $_ -match 'LegacyTransientReconnectController::ApplyPendingGhostPoses\(' }).Count -eq 1 -and
			@($legacyRecoveryLateUpdateOperands | Where-Object { $_ -match 'LegacyTransientReconnectController::CaptureOwnerCheckpoint\(' }).Count -eq 1 -and
			@($legacyRecoveryCaptureOwnerCheckpointOperands | Where-Object { $_ -match 'LegacyMachineRecoveryFingerprint::TryCompute\(' }).Count -eq 1 -and
			@($legacyRecoveryCaptureOwnerCheckpointOperands | Where-Object { $_ -match 'LegacyCrashOwnerCheckpoint::Save\(' }).Count -eq 1 -and
			@($legacyRecoveryCaptureOwnerCheckpointOperands | Where-Object { $_ -match 'LegacyTransientReconnectController::IsOwnerCheckpointDetachedBody\(' }).Count -eq 1 -and
			@($legacyRecoveryLoadOwnerCheckpointOperands | Where-Object { $_ -match 'LegacyCrashOwnerCheckpoint::TryLoad\(' }).Count -eq 1 -and
			@($legacyRecoveryLoadOwnerCheckpointOperands | Where-Object { $_ -match 'LegacyMachineRecoveryFingerprint::TryCompute\(' }).Count -eq 0 -and
			@($legacyRecoveryCrashIdentityOperands | Where-Object { $_ -match 'LegacyCrashOwnerCheckpoint::TryLoad\(' }).Count -eq 1 -and
			@($legacyRecoveryCrashIdentityOperands | Where-Object { $_ -match 'LegacyTransientReconnect::TryExtractOwnerConstructionFingerprint\(' }).Count -eq 1 -and
			@($legacyRecoveryValidateOwnerCheckpointOperands | Where-Object { $_ -match 'LegacyMachineRecoveryFingerprint::TryCompute\(' }).Count -eq 1 -and
			@($legacyRecoveryApplyOwnerCheckpointOperands | Where-Object { $_ -match 'LegacyTransientReconnectController::ValidateOwnerCheckpoint\(' }).Count -eq 1 -and
			@($legacyRecoverySendHelloOperands | Where-Object { $_ -match 'LegacyTransientReconnectController::StartCrashProtocolFallbackTimer\(' }).Count -eq 1 -and
			@($legacyRecoverySendHelloOperands | Where-Object { $_ -match 'LegacyTransientReconnectController::StartCrashConsentCompatibilityTimer\(' }).Count -eq 1 -and
			$legacyRecoveryStartCrashFallbackTimerStrings -contains 'CLIENT_CRASH_PROTOCOL_TIMER_STARTED source=hello-sent timeoutSeconds=30' -and
			$legacyRecoveryStartCrashConsentTimerStrings -contains 'CLIENT_CRASH_PROTOCOL_TIMER_STARTED source=v6-hello-sent timeoutSeconds=5 fallback=v3' -and
			@($legacyRecoveryGhostRpcOperands | Where-Object { $_ -match 'LegacyTransientReconnectController::ReceiveGhostPose\(' }).Count -eq 1 -and
			@($legacyRecoveryReceiveGhostOperands | Where-Object { $_ -match 'LegacyTransientReconnectController::ReceiveGhostPoseCore\(' }).Count -eq 1 -and
			@($legacyRecoveryReceiveGhostCoreOperands | Where-Object { $_ -match 'LegacyGhostPoseTransportCodec::TryJoin\(' }).Count -eq 1 -and
			@($legacyRecoveryReceiveGhostCoreOperands | Where-Object { $_ -match 'LegacyGhostPoseSnapshotCodec::TryDecode\(' }).Count -eq 1 -and
			@($legacyRecoveryApplyGhostsOperands | Where-Object { $_ -match 'LegacyTransientReconnectController::TryApplyGhostPose\(' }).Count -eq 1 -and
			@($legacyRecoveryApplyGhostOperands | Where-Object { $_ -match 'LegacyTransientReconnect::TryCollectMachineBodies\(' }).Count -eq 1 -and
			@($legacyRecoveryApplyGhostOperands | Where-Object { $_ -match 'System.Reflection.FieldInfo::GetValue\(' }).Count -eq 1 -and
			@($legacyRecoveryApplyGhostOperands | Where-Object { $_ -match 'LegacyMachineRecoveryFingerprint::TryCompute\(' }).Count -eq 0 -and
			$legacyRecoveryApplyGhostStrings -contains 'ghost-view-identity-mismatch' -and
			$legacyRecoveryApplyGhostStrings -notcontains 'owner-native-locate-pending' -and
			$legacyRecoveryApplyGhostStrings -notcontains 'local-articulation-index-not-ready-' -and
			@($legacyRecoveryApplyGhostOperands | Where-Object { $_ -match 'LegacyTransientReconnectController::ShouldWaitForCrashOwnerNativePlacement\(' }).Count -eq 0 -and
			@($legacyRecoveryApplyGhostOperands | Where-Object { $_ -match 'LegacyTransientReconnectController::TryApplyOwnerCheckpointPose\(' }).Count -eq 1 -and
			@($legacyRecoveryApplyGhostOperands | Where-Object { $_ -match 'McnCraft\.MachineController::UpdateHealthEnergy\(' }).Count -eq 1 -and
			@($legacyRecoveryApplyOwnerCheckpointOperands | Where-Object { $_ -match 'LegacyTransientReconnectController::TryLoadOwnerCheckpoint\(' }).Count -eq 1 -and
			@($legacyRecoveryApplyOwnerCheckpointOperands | Where-Object { $_ -match 'LegacyTransientReconnectController::ApplyOwnerCheckpointBody\(' }).Count -eq 1 -and
			@($legacyRecoveryApplyOwnerCheckpointOperands | Where-Object { $_ -match 'LegacyTransientReconnectController::ApplyOwnerCheckpointBrokenJoints\(' }).Count -eq 1 -and
			@($legacyRecoveryApplyOwnerCheckpointOperands | Where-Object { $_ -match 'LegacyTransientReconnectController::IsOwnerCheckpointDetachedBody\(' }).Count -eq 1 -and
			@($legacyRecoveryApplyOwnerCheckpointOperands | Where-Object { $_ -match 'McnCraft\.MachineController::UpdateHealthEnergy\(' }).Count -eq 1 -and
			@($legacyRecoveryApplyOwnerCheckpointOperands | Where-Object { $_ -match 'McnCraft\.MachineController::SelectBiggestBody\(' }).Count -eq 1 -and
			@($legacyRecoveryApplyOwnerCheckpointOperands | Where-Object { $_ -match 'LegacyTransientReconnectController::SendCrashOwnerApplied\(' }).Count -eq 1 -and
			@($legacyRecoveryApplyOwnerCheckpointStrings | Where-Object { $_ -match 'applyPath=owner-checkpoint damage=owner-checkpoint source=local-physical-bodies' }).Count -eq 1 -and
			@($legacyRecoveryApplyOwnerJointsOperands | Where-Object { $_ -match 'JointController::BreakJoint\(' }).Count -eq 1 -and
			@($legacyRecoveryApplyOwnerJointsOperands | Where-Object { $_ -match 'LegacyTransientReconnectController::IsOwnerCheckpointDetachedBody\(' }).Count -eq 1 -and
			@($legacyRecoveryApplyOwnerBodyOperands | Where-Object { $_ -match 'McnCraft\.BodyController::Suspend\(' }).Count -eq 1 -and
			@($legacyRecoveryApplyOwnerBodyOperands | Where-Object { $_ -match 'McnCraft\.BodyController::IFCNJLPLNGF' }).Count -eq 0 -and
			@($legacyRecoveryLateUpdateOperands | Where-Object { $_ -match 'LegacyTransientReconnectController::EnforceClientGhostPoses\(' }).Count -eq 1 -and
			@($legacyRecoveryEnforceGhostsOperands | Where-Object { $_ -match 'LegacyTransientReconnectController::ApplyClientGhostBody\(' }).Count -eq 1 -and
			@($legacyRecoveryEnforceGhostsOperands | Where-Object { $_ -match 'LegacyTransientReconnectController::ApplyClientGhostVisual\(' }).Count -eq 1 -and
			@($legacyRecoveryApplyGhostOperands | Where-Object { $_ -match 'LegacyTransientReconnectController::ApplyClientGhostBody\(' }).Count -eq 1 -and
			@($legacyRecoveryApplyGhostOperands | Where-Object { $_ -match 'LegacyTransientReconnectController::ApplyClientGhostVisual\(' }).Count -eq 1 -and
			@($legacyRecoveryApplyGhostBodyOperands | Where-Object { $_ -match 'McnCraft\.BodyController::SetVisibility\(' }).Count -eq 2 -and
			@($legacyRecoveryApplyGhostVisualOperands | Where-Object { $_ -match 'LegacyTransientReconnectController::ApplyClientGhostTransform\(' }).Count -eq 2 -and
			@($legacyRecoveryApplyGhostVisualOperands | Where-Object { $_ -match 'UnityEngine\.Transform::set_localScale\(' }).Count -eq 1 -and
			@($legacyRecoveryApplyGhostTransformOperands | Where-Object { $_ -match 'UnityEngine\.Transform::set_position\(' }).Count -eq 1 -and
			@($legacyRecoveryApplyGhostTransformOperands | Where-Object { $_ -match 'UnityEngine\.Transform::set_rotation\(' }).Count -eq 1 -and
			@($legacyRecoveryApplyGhostTransformOperands | Where-Object { $_ -match 'UnityEngine\.Transform::set_localScale\(' }).Count -eq 1 -and
			@($legacyRecoveryApplyPeerRebindOperands | Where-Object { $_ -match 'LegacyTransientReconnectController::ReleaseClientGhost\(' }).Count -eq 1 -and
			@($legacyRecoverySendGhostOperands | Where-Object { $_ -match 'UnityEngine\.NetworkView::RPC\(' }).Count -eq 1 -and
			@($legacyRecoveryCrashHelloRpcOperands | Where-Object { $_ -match 'LegacyTransientReconnect::HandleCrashHello\(' }).Count -eq 1 -and
			@($legacyRecoveryCrashHelloV4RpcOperands | Where-Object { $_ -match 'LegacyTransientReconnect::HandleCrashHello\(' }).Count -eq 1 -and
			@($legacyRecoveryCrashHelloV5RpcOperands | Where-Object { $_ -match 'LegacyTransientReconnect::HandleCrashHello\(' }).Count -eq 1 -and
			@($legacyRecoveryCrashHelloV6RpcOperands | Where-Object { $_ -match 'LegacyTransientReconnect::HandleCrashHello\(' }).Count -eq 1 -and
			@($legacyRecoveryCrashDecisionV4RpcOperands | Where-Object { $_ -match 'LegacyTransientReconnect::HandleCrashDecision\(' }).Count -eq 1 -and
			@($legacyRecoveryCrashDecisionV5RpcOperands | Where-Object { $_ -match 'LegacyTransientReconnect::HandleCrashDecision\(' }).Count -eq 1 -and
			@($legacyRecoveryCrashDecisionV6RpcOperands | Where-Object { $_ -match 'LegacyTransientReconnect::HandleCrashDecision\(' }).Count -eq 1 -and
			@($legacyRecoveryCrashAppliedRpcOperands | Where-Object { $_ -match 'LegacyTransientReconnect::HandleCrashOwnerApplied\(' }).Count -eq 1 -and
			@($legacyRecoveryCrashPoseRpcOperands | Where-Object { $_ -match 'LegacyTransientReconnectController::ReceiveGhostPoseCore\(' }).Count -eq 1 -and
			@($legacyRecoveryCrashHelloOperands | Where-Object { $_ -match 'LegacyGhostPoseTransportCodec::TryJoin\(' }).Count -eq 0 -and
			@($legacyRecoveryCrashHelloOperands | Where-Object { $_ -match 'LegacyTransientReconnect::TryExtractOwnerConstructionIdentity\(' }).Count -eq 2 -and
			@($legacyRecoveryCrashHelloOperands | Where-Object { $_ -match 'LegacyTransientReconnect::TryCompareCrashMachineConstruction\(' }).Count -eq 1 -and
			@($legacyRecoveryCrashHelloOperands | Where-Object { $_ -match 'LegacyTransientReconnect::BeginCrashRestore\(' }).Count -eq 1 -and
			@($legacyRecoveryCrashDecisionOperands | Where-Object { $_ -match 'LegacyTransientReconnect::TryCompareCrashMachineConstruction\(' }).Count -eq 0 -and
			@($legacyRecoveryCrashDecisionOperands | Where-Object { $_ -match 'LegacyTransientReconnect::TryExtractOwnerConstructionIdentity\(' }).Count -eq 2 -and
			@($legacyRecoveryCrashDecisionOperands | Where-Object { $_ -match 'LegacyTransientReconnect::BeginCrashRestore\(' }).Count -eq 1 -and
			$legacyRecoveryCrashDecisionStrings -contains 'restore-declined-by-owner' -and
			@($legacyRecoveryCrashMachineComparisonOperands | Where-Object { $_ -match 'LegacyMachineRecoveryFingerprint::TryCompute\(' }).Count -eq 3 -and
			@($legacyRecoveryCrashMachineComparisonOperands | Where-Object { $_ -match 'LegacyTransientReconnect::TryExtractOwnerConstructionFingerprint\(' }).Count -eq 1 -and
			@($legacyRecoveryFreshOwnerIdentityOperands | Where-Object { $_ -match 'LegacyMachineRecoveryFingerprint::TryCompute\(' }).Count -eq 1 -and
			@($legacyRecoverySendHelloOperands | Where-Object { $_ -match 'LegacyTransientReconnectController::TryGetCrashFreshOwnerIdentity\(' }).Count -eq 1 -and
			@($legacyRecoverySendHelloOperands | Where-Object { $_ -eq 'MPatcherCrashResumeHelloV6' }).Count -eq 1 -and
			@($legacyRecoverySendCrashConsentOperands | Where-Object { $_ -match 'LegacyTransientReconnectController::TryGetCrashFreshOwnerIdentity\(' }).Count -eq 1 -and
			@($legacyRecoverySendCrashConsentOperands | Where-Object { $_ -eq 'MPatcherCrashResumeDecisionV6' }).Count -eq 1 -and
			@($legacyRecoveryBeginCrashRestoreOperands | Where-Object { $_ -match 'LegacyGhostPoseTransportCodec::TryJoin\(' }).Count -eq 1 -and
			@($legacyRecoveryBeginCrashRestoreOperands | Where-Object { $_ -match 'LegacyGhostPoseSnapshotCodec::TryDecode\(' }).Count -eq 1 -and
			@($legacyRecoveryBeginCrashRestoreOperands | Where-Object { $_ -match 'LegacyGhostPoseSnapshotCodec::TryEncode\(' }).Count -eq 1 -and
			@($legacyRecoveryBeginCrashRestoreOperands | Where-Object { $_ -match 'LegacyGhostPoseTransportCodec::Split\(' }).Count -eq 1 -and
			@($legacyRecoveryBeginCrashRestoreOperands | Where-Object { $_ -match 'LegacyTransientReconnectController::BroadcastCrashRestorePose\(' }).Count -eq 1 -and
			@($legacyRecoveryCrashStatusRpcOperands | Where-Object { $_ -match 'LegacyTransientReconnectController::ShowCrashRestoreConsent\(' }).Count -eq 1 -and
			@($legacyRecoveryCrashStatusRpcOperands | Where-Object { $_ -match 'LegacyTransientReconnectController::SendCrashRestoreConsentDecision\(' }).Count -eq 1 -and
			@($legacyRecoverySubmitCrashConsentOperands | Where-Object { $_ -match 'LegacyTransientReconnectController::CloseCrashRestoreConsent\(' }).Count -ge 1 -and
			@($legacyRecoverySubmitCrashConsentOperands | Where-Object { $_ -match 'LegacyTransientReconnectController::SendCrashRestoreConsentDecision\(' }).Count -eq 1 -and
			$legacyRecoverySendCrashConsentOperands -contains 'MPatcherCrashResumeDecisionV6' -and
			$legacyRecoveryConsentUiStrings -contains 'Restore your machine to its position and pose from before the crash?' -and
			$legacyRecoveryConsentUiStrings -contains 'クラッシュ前の位置と姿勢にマシンを復元しますか？' -and
			$legacyRecoveryConsentUiStrings -contains 'native-overwrite-clone' -and
			$legacyRecoveryConsentUiStrings -contains 'Overwrite' -and
			$legacyRecoveryConsentUiStrings -notcontains '<color=#54C7F3><size=22>' -and
			$legacyRecoveryConsentUiStrings -notcontains 'Хотите вернуться на то же место в ту же позу что была до краша?' -and
			@($legacyRecoveryConsentCopyCalls | Where-Object { $_ -match 'UnityEngine\.UI\.Text::set_text\(' }).Count -eq 3 -and
			@($legacyRecoveryConsentCopyCalls | Where-Object { $_ -match 'UnityEngine\.UI\.(Graphic::set_color|Text::set_fontSize|Text::set_supportRichText)\(' }).Count -eq 0 -and
			@($legacyRecoveryConsentCopyCalls | Where-Object { $_ -match 'LegacyCrashResumeConsentUi::ApplyPromptStyle\(' }).Count -eq 1 -and
			@($legacyRecoveryConsentPromptStyleCalls | Where-Object { $_ -match 'UnityEngine\.UI\.Text::set_fontStyle\(' }).Count -eq 1 -and
			@($legacyRecoveryConsentPromptStyleCalls | Where-Object { $_ -match 'UnityEngine\.UI\.Graphic::set_color\(' }).Count -eq 1 -and
			@($legacyRecoveryConsentPromptStyleCalls | Where-Object { $_ -match 'UnityEngine\.UI\.Text::set_supportRichText\(' }).Count -eq 1 -and
			@($legacyRecoveryConsentPromptStyleCalls | Where-Object { $_ -match '::GetComponents<UnityEngine\.UI\.Shadow>\(' }).Count -eq 1 -and
			@($legacyRecoveryConsentPromptStyleCalls | Where-Object { $_ -match 'UnityEngine\.Behaviour::set_enabled\(' }).Count -eq 1 -and
			$legacyRecoveryConsentUiStrings -contains 'CLIENT_CRASH_CONSENT_INPUT source=' -and
			@($legacyRecoveryConsentUiOperands | Where-Object { $_ -match 'LegacyCrashResumeConsentInput::Configure\(' }).Count -eq 1 -and
			@($legacyRecoveryConsentInputOperands | Where-Object { $_ -match 'UnityEngine\.Input::GetMouseButtonDown\(' }).Count -eq 1 -and
			@($legacyRecoveryConsentInputOperands | Where-Object { $_ -match 'UnityEngine\.Input::GetKeyDown\(' }).Count -eq 5 -and
			@($legacyRecoveryConsentInputOperands | Where-Object { $_ -match 'UnityEngine\.RectTransformUtility::RectangleContainsScreenPoint\(' }).Count -eq 2 -and
			@($legacyRecoveryConsentConfigureOperands | Where-Object { $_ -match 'HOCGCCAIPFF::OHCMPEJKDHJ\(' }).Count -eq 1 -and
			@($legacyRecoveryConsentConfigureOperands | Where-Object { $_ -match 'LegacyCrashResumeConsentInput::HoldInteractiveState\(' }).Count -eq 1 -and
			@($legacyRecoveryConsentInputOperands | Where-Object { $_ -match 'LegacyCrashResumeConsentInput::HoldInteractiveState\(' }).Count -eq 1 -and
			@($legacyRecoveryConsentLateUpdateOperands | Where-Object { $_ -match 'LegacyCrashResumeConsentInput::HoldInteractiveState\(' }).Count -eq 1 -and
			@($legacyRecoveryConsentHoldOperands | Where-Object { $_ -match 'HOCGCCAIPFF::NDIOFGDJAJO' }).Count -eq 1 -and
			@($legacyRecoveryConsentHoldOperands | Where-Object { $_ -match 'UnityEngine\.Cursor::set_(lockState|visible)\(' }).Count -eq 2 -and
			@($legacyRecoveryConsentHoldOperands | Where-Object { $_ -match 'LegacyCrashResumeConsentInput::HoldCameraController\(' }).Count -eq 1 -and
			@($legacyRecoveryConsentCameraHoldOperands | Where-Object { $_ -match 'RideCameraController::set_enabled\(|UnityEngine\.Behaviour::set_enabled\(' }).Count -eq 1 -and
			$legacyRecoveryConsentCameraHoldOperands -contains 'CLIENT_CRASH_CONSENT_CAMERA_SUSPENDED previousEnabled=' -and
			@($legacyRecoveryConsentRestoreOperands | Where-Object { $_ -match 'HOCGCCAIPFF::NDIOFGDJAJO' }).Count -eq 1 -and
			@($legacyRecoveryConsentRestoreOperands | Where-Object { $_ -match 'UnityEngine\.Cursor::set_(lockState|visible)\(' }).Count -eq 2 -and
			@($legacyRecoveryConsentRestoreOperands | Where-Object { $_ -match 'LegacyCrashResumeConsentInput::RestoreCameraController\(' }).Count -eq 1 -and
			@($legacyRecoveryConsentCameraRestoreOperands | Where-Object { $_ -match 'UnityEngine\.MonoBehaviour::StartCoroutine\(' }).Count -eq 1 -and
			@($legacyRecoveryConsentCameraRestoreOperands | Where-Object { $_ -match 'UnityEngine\.Behaviour::set_enabled\(' }).Count -eq 2 -and
			@($legacyRecoveryConsentCameraRestoreMoveNextOperands | Where-Object { $_ -match 'UnityEngine\.Behaviour::set_enabled\(' }).Count -eq 1 -and
			$legacyRecoveryConsentCameraRestoreMoveNextOperands -contains 'CLIENT_CRASH_CONSENT_CAMERA_RESTORED deferred=true' -and
			@($legacyRecoveryConsentCloseOperands | Where-Object { $_ -match 'LegacyCrashResumeConsentInput::RestoreInteractiveState\(' }).Count -eq 1 -and
			@($legacyRecoveryConsentDisableOperands | Where-Object { $_ -match 'LegacyCrashResumeConsentInput::RestoreInteractiveState\(' }).Count -eq 1 -and
			@($legacyRecoveryConsentDestroyOperands | Where-Object { $_ -match 'LegacyCrashResumeConsentInput::RestoreInteractiveState\(' }).Count -eq 1 -and
			@($legacyRecoveryCrashCompleteOperands | Where-Object { $_ -match 'LegacyTransientReconnect::DestroyRetained\(' }).Count -eq 1 -and
			@($legacyRecoveryCrashCompleteOperands | Where-Object { $_ -match 'LegacyTransientReconnectController::CompleteRemoteCrashPresentation\(' }).Count -eq 1 -and
			@($legacyRecoveryCrashCompleteOperands | Where-Object { $_ -match 'LegacyTransientReconnectController::SendCrashStatus\(' }).Count -eq 1 -and
			@($legacyRecoveryCrashCompleteOperands | Where-Object { $_ -match 'ResumeRecord::PreviousPlayer' }).Count -ge 1 -and
			$legacyRecoveryCrashCompleteStrings -contains ' oldGhostDestroyed=true previousPlayerCleared=true machineGroup=1 sending=true' -and
			@($legacyRecoveryDestroyTrackedOperands | Where-Object { $_ -match 'UnityEngine\.Network::Destroy\(UnityEngine.GameObject\)' }).Count -eq 1 -and
			@($legacyRecoveryDestroyTrackedOperands | Where-Object { $_ -match 'UnityEngine\.Object::Destroy\(' }).Count -eq 1 -and
			@($legacyRecoveryDestroyTrackedOperands | Where-Object { $_ -match 'LegacyPlayerList::Remove\(' }).Count -eq 1 -and
			@($legacyRecoveryDestroyTrackedOperands | Where-Object { $_ -match 'LegacyTransientReconnect::RemoveViewBuffer\(' }).Count -eq 1 -and
			@($legacyRecoveryDestroyTrackedOperands | Where-Object { $_ -match 'LegacyRetirementIds::Collect' }).Count -eq 1 -and
			@($legacyRecoveryBroadcastCrashPoseOperands | Where-Object { $_ -match 'UnityEngine\.NetworkView::RPC\(' }).Count -eq 1 -and
			@($legacyRecoveryApplyGhostOperands | Where-Object { $_ -match 'LegacyTransientReconnect::HandleCrashServerApplied\(' }).Count -eq 1 -and
			@($legacyRecoveryApplyGhostOperands | Where-Object { $_ -match 'LegacyTransientReconnectController::SendCrashOwnerApplied\(' }).Count -eq 0 -and
			@($legacyRecoveryApplyGhostOperands | Where-Object { $_ -match 'McnCraft\.MachineController::SelectBiggestBody\(' }).Count -eq 1 -and
			@($legacyRecoveryApplyGhostOperands | Where-Object { $_ -match 'LegacyTransientReconnectController::ScheduleRemoteCrashPresentationRefresh\(' }).Count -eq 1 -and
			@($legacyRecoveryUpdateOperands | Where-Object { $_ -match 'LegacyTransientReconnectController::ApplyPendingRemoteCrashPresentationRefreshes\(' }).Count -eq 1 -and
			@($legacyRecoveryControllerNamePostfixOperands | Where-Object { $_ -match 'LegacyTransientReconnectController::ObserveNativePlayerName\(' }).Count -eq 1 -and
			@($legacyRecoveryCompleteRemotePresentationOperands | Where-Object { $_ -match 'PendingRemotePresentationRefresh::EarlyRegistrationPending' }).Count -ge 2 -and
			@($legacyRecoveryCompleteRemotePresentationOperands | Where-Object { $_ -match 'PendingRemotePresentationRefresh::AwaitNativeRegistration' }).Count -ge 2 -and
			$legacyRecoveryCompleteRemotePresentationStrings -contains 'REMOTE_CRASH_HOST_EARLY_REGISTRATION_QUEUED view=' -and
			@($legacyRecoveryObserveNativePlayerNameOperands | Where-Object { $_ -match 'PendingRemotePresentationRefresh::NativeNameObserved' }).Count -ge 1 -and
			$legacyRecoveryObserveNativePlayerNameStrings -contains 'REMOTE_CRASH_NATIVE_PLAYER_NAME view=' -and
			@($legacyRecoveryApplyPendingRemotePresentationOperands | Where-Object { $_ -match 'LegacyTransientReconnectController::EnsureRemoteCrashHostEarlyRegistration\(' }).Count -eq 1 -and
			@($legacyRecoveryApplyPendingRemotePresentationOperands | Where-Object { $_ -match 'PendingRemotePresentationRefresh::NativeNameObserved' }).Count -ge 1 -and
			@($legacyRecoveryApplyPendingRemotePresentationOperands | Where-Object { $_ -match 'LegacyTransientReconnectController::NormalizeRemoteCrashHostRegistration\(' }).Count -eq 1 -and
			@($legacyRecoveryApplyPendingRemotePresentationOperands | Where-Object { $_ -match 'LegacyTransientReconnectController::TryReapplyRemoteCrashVisualSnapshot\(' }).Count -eq 0 -and
			@($legacyRecoveryScheduleRemotePresentationOperands | Where-Object { $_ -match 'LegacyTransientReconnectController::ReconcileRemoteCrashPresentation\(' }).Count -eq 1 -and
			@($legacyRecoveryApplyPendingRemotePresentationOperands | Where-Object { $_ -match 'LegacyTransientReconnectController::ReconcileRemoteCrashPresentation\(' }).Count -eq 1 -and
			@($legacyRecoveryEnsureRemoteHostEarlyRegistrationOperands | Where-Object { $_ -match 'Arena::PBBCHKBJAEA' }).Count -ge 3 -and
			@($legacyRecoveryEnsureRemoteHostEarlyRegistrationOperands | Where-Object { $_ -match 'System.Collections.Generic.List.*::Add\(' }).Count -eq 1 -and
			@($legacyRecoveryEnsureRemoteHostEarlyRegistrationOperands | Where-Object { $_ -match 'Game::IGEAEEAMAPM' }).Count -eq 1 -and
			$legacyRecoveryEnsureRemoteHostEarlyRegistrationStrings -contains 'REMOTE_CRASH_HOST_EARLY_REGISTRATION view=' -and
			@($legacyRecoveryNormalizeRemoteHostRegistrationOperands | Where-Object { $_ -match 'Arena::PBBCHKBJAEA' }).Count -ge 4 -and
			@($legacyRecoveryNormalizeRemoteHostRegistrationOperands | Where-Object { $_ -match 'System.Collections.Generic.List.*::RemoveAt\(' }).Count -eq 1 -and
			@($legacyRecoveryNormalizeRemoteHostRegistrationOperands | Where-Object { $_ -match 'System.Collections.Generic.List.*::Add\(' }).Count -eq 0 -and
			@($legacyRecoveryNormalizeRemoteHostRegistrationOperands | Where-Object { $_ -match 'Game::IGEAEEAMAPM' }).Count -eq 1 -and
			$legacyRecoveryNormalizeRemoteHostRegistrationStrings -contains 'REMOTE_CRASH_HOST_NATIVE_REGISTRATION view=' -and
			$null -eq $legacyRecoveryReapplyRemoteVisualSnapshot -and
			@($legacyRecoveryResetMotionOperands | Where-Object { $_ -match 'FieldInfo::SetValue\(' }).Count -eq 3 -and
			@($legacyRecoveryReconcileRemotePresentationOperands | Where-Object { $_ -match 'McnCraft\.MachineController::UpdateBound\(' }).Count -eq 2 -and
			@($legacyRecoveryReconcileRemotePresentationOperands | Where-Object { $_ -match 'NMLMDCCDFPN::MHPPLEHPJHH\(' }).Count -eq 1 -and
			@($legacyRecoveryReconcileRemotePresentationOperands | Where-Object { $_ -match 'TagController::SetTarget\(' }).Count -eq 1 -and
			@($legacyRecoveryReconcileRemotePresentationOperands | Where-Object { $_ -match 'McnCraft\.MachineController::PGLHDPCAGEM' }).Count -ge 1 -and
			$legacyRecoveryReconcileRemotePresentationStrings -contains 'REMOTE_CRASH_PRESENTATION_REFRESH view=' -and
			@($legacyRecoveryMarkerArmOperands | Where-Object { $_ -match 'LegacyCrashResumeMarker::Arm\(' }).Count -eq 1 -and
			@($legacyRecoveryMarkerClearOperands | Where-Object { $_ -match 'LegacyCrashResumeMarker::Clear\(' }).Count -eq 1 -and
			@($legacyRecoveryMarkerClearOperands | Where-Object { $_ -match 'LegacyCrashOwnerCheckpoint::Clear\(' }).Count -eq 1 -and
			$legacyCrashMarkerDecodeStrings -contains 'marker-target-base64' -and
			$legacyOwnerCheckpointDecodeStrings -contains 'checkpoint-envelope-checksum' -and
			@($legacyGhostTransportType.Methods.Name | Where-Object { $_ -in @('Split', 'TryJoin') }).Count -eq 2 -and
			@($legacyGhostSnapshotCodecType.Methods.Name | Where-Object { $_ -in @('TryEncode', 'TryDecode') }).Count -eq 2 -and
			@($legacyRecoveryClientGateOperands | Where-Object { $_ -match 'UnityEngine\.Network::SetReceivingEnabled\(' }).Count -eq 1 -and
			@($legacyRecoveryClientReleaseOperands | Where-Object { $_ -match 'UnityEngine\.Network::SetReceivingEnabled\(' }).Count -eq 1 -and
			@($legacyRecoveryBroadcastOperands | Where-Object { $_ -match 'UnityEngine\.NetworkView::RPC\(' }).Count -eq 1 -and
			@($legacyRecoveryUpdateOperands | Where-Object { $_ -match 'LegacyTransientReconnectController::AbortRecovery\(' }).Count -eq 1 -and
			@($legacyRecoveryAbortOperands | Where-Object { $_ -match 'UnityEngine\.Network::Disconnect\(' }).Count -eq 1 -and
			@($legacyRecoveryNativeExitOperands | Where-Object { $_ -match 'HarmonyLib\.AccessTools::Method\(' }).Count -eq 1 -and
			@($legacyRecoveryNativeExitOperands | Where-Object { $_ -match 'System\.Reflection\.MethodBase::Invoke\(' }).Count -eq 1 -and
			$legacyRecoveryStrings -contains 'RPC_SysMsg' -and
			@($legacyRecoveryAgentOperands | Where-Object { $_ -match 'HarmonyLib\.AccessTools::Method\(' }).Count -ge 1 -and
			$legacyRecoveryStrings -contains 'legacy-recovery-token.txt' -and
			$legacyRecoveryStrings -contains 'legacy-active-session-v1.txt' -and
			$legacyRecoveryStrings -contains 'legacy-owner-checkpoint-v1.bin' -and
			$legacyRecoveryStrings -contains 'legacy-transient-test-drop.request' -and
			$legacyRecoveryStrings -contains '[MPatcher] Connection lost. Local control remains active; reconnecting in 10 seconds...' -and
			$legacyRecoveryStrings -contains '[MPatcher] The original server session is no longer available.' -and
			$legacyRecoveryForbidden.Count -eq 0 -and
			$legacyRecoveryUnsafeSuspend.Count -eq 0
	}

    $machineChangeType = $allTypes | Where-Object FullName -eq 'MPatcherFork.CustomPatches.LegacyMachineChangeIngame'
    $machineChangeState = $allTypes | Where-Object FullName -like 'MPatcherFork.CustomPatches.LegacyMachineChangeIngame/<DeferredLegacyReplacement>*'
    $machineChangeFinalizeState = $allTypes | Where-Object FullName -like 'MPatcherFork.CustomPatches.LegacyMachineChangeIngame/<FinalizeAcceptedReplacementCore>*'
    $recoveredMachineChangeType = $allTypes | Where-Object FullName -eq 'dyl7NQFWvb8SnwY4dXogp_aCQhx2Y7dLGUBgzCus25T9Wpo6h01g9Y342KDBl8ctV_NzWUOypgYMsbS0RAESeaU'
    $recoveredMachineChangePrefixType = $allTypes | Where-Object FullName -eq 'dyl7NQFWvb8SnwY4dXogp_aCQhx2Y7dLGUBgzCus25T9Wpo6h01g9Y342KDBl8ctV_NzWUOypgYMsbS0RAESeaU/Class50'
    if ($null -ne $machineChangeType -and $null -ne $machineChangeState -and $null -ne $machineChangeFinalizeState -and
        $null -ne $recoveredMachineChangeType -and $null -ne $recoveredMachineChangePrefixType) {
        $validateSelection = $machineChangeType.Methods | Where-Object Name -eq 'ValidateCurrentSelection'
        $machineChangeRegister = $machineChangeType.Methods | Where-Object Name -eq 'TryRegister'
        $meetingActionPrefix = $machineChangeType.Methods | Where-Object Name -eq 'MeetingActionPrefix'
        $meetingActionPostfix = $machineChangeType.Methods | Where-Object Name -eq 'MeetingActionPostfix'
        $applyZeroCooldown = $machineChangeType.Methods | Where-Object Name -eq 'ApplyZeroCooldown'
		$networkDestroyPrefix = $machineChangeType.Methods | Where-Object Name -eq 'NetworkDestroyPrefix'
        $networkInstantiatePrefix = $machineChangeType.Methods | Where-Object Name -eq 'NetworkInstantiatePrefix'
        $initializeMachinePrefix = $machineChangeType.Methods | Where-Object Name -eq 'InitializeMachinePrefix'
		$initializeMachinePostfix = $machineChangeType.Methods | Where-Object Name -eq 'InitializeMachinePostfix'
		$nativeAnchorType = $module.Types | Where-Object FullName -eq 'MPatcherFork.CustomPatches.LegacyMachineNetworkAnchor'
		$captureNetworkAnchor = $nativeAnchorType.Methods | Where-Object Name -eq 'TryCapture'
        $finalizeReplacement = $machineChangeType.Methods | Where-Object Name -eq 'FinalizeAcceptedReplacement'
        $showDebugMessagePrefix = $machineChangeType.Methods | Where-Object Name -eq 'ShowDebugMessagePrefix'
        $tryCompleteReplacement = $machineChangeType.Methods | Where-Object Name -eq 'TryCompleteLegacyReplacement'
        $completeSelection = $machineChangeType.Methods | Where-Object Name -eq 'CompleteDeferredSelection'
        $commitSelection = $machineChangeType.Methods | Where-Object Name -eq 'CommitSelectionTransaction'
        $rollbackSelection = $machineChangeType.Methods | Where-Object Name -eq 'RollbackSelectionTransaction'
        $cleanupHelpers = $machineChangeType.Methods | Where-Object Name -eq 'CleanupSpringControlHelpers'
        $deferredMoveNext = $machineChangeState.Methods | Where-Object Name -eq 'MoveNext'
        $finalizeMoveNext = $machineChangeFinalizeState.Methods | Where-Object Name -eq 'MoveNext'
        $recoveredPrefix = $recoveredMachineChangePrefixType.Methods | Where-Object Name -eq 'smethod_0'
        $replacementEntry = $recoveredMachineChangeType.Methods | Where-Object Name -like 'mDZmboOC2mLx2MmBzu*4bNw'

        $validateOperands = @($validateSelection.Body.Instructions | ForEach-Object { [string]$_.Operand })
        $validateStrings = @($validateSelection.Body.Instructions | Where-Object { $_.OpCode.Code -eq 'Ldstr' } | ForEach-Object { [string]$_.Operand })
        $registerOperands = @($machineChangeRegister.Body.Instructions | ForEach-Object { [string]$_.Operand })
        $registerStrings = @($machineChangeRegister.Body.Instructions | Where-Object { $_.OpCode.Code -eq 'Ldstr' } | ForEach-Object { [string]$_.Operand })
        $meetingPrefixOperands = @($meetingActionPrefix.Body.Instructions | ForEach-Object { [string]$_.Operand })
        $meetingPrefixStrings = @($meetingActionPrefix.Body.Instructions | Where-Object { $_.OpCode.Code -eq 'Ldstr' } | ForEach-Object { [string]$_.Operand })
        $meetingPostfixOperands = @($meetingActionPostfix.Body.Instructions | ForEach-Object { [string]$_.Operand })
        $zeroCooldownOperands = @($applyZeroCooldown.Body.Instructions | ForEach-Object { [string]$_.Operand })
        $zeroCooldownConstants = @($applyZeroCooldown.Body.Instructions | Where-Object { $_.OpCode.Code -eq 'Ldc_R4' } | ForEach-Object { [single]$_.Operand })
		$networkDestroyOperands = @($networkDestroyPrefix.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$networkDestroyStrings = @($networkDestroyPrefix.Body.Instructions | Where-Object { $_.OpCode.Code -eq 'Ldstr' } | ForEach-Object { [string]$_.Operand })
        $networkInstantiateOperands = @($networkInstantiatePrefix.Body.Instructions | ForEach-Object { [string]$_.Operand })
        $initializeMachineOperands = @($initializeMachinePrefix.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$initializeMachinePostfixOperands = @($initializeMachinePostfix.Body.Instructions | ForEach-Object { [string]$_.Operand })
		$captureNetworkAnchorOperands = @($captureNetworkAnchor.Body.Instructions | ForEach-Object { [string]$_.Operand })
        $finalizeWrapperOperands = @($finalizeReplacement.Body.Instructions | ForEach-Object { [string]$_.Operand })
        $finalizeOperands = @($finalizeMoveNext.Body.Instructions | ForEach-Object { [string]$_.Operand })
        $finalizeStrings = @($finalizeMoveNext.Body.Instructions | Where-Object { $_.OpCode.Code -eq 'Ldstr' } | ForEach-Object { [string]$_.Operand })
        $showDebugStrings = @($showDebugMessagePrefix.Body.Instructions | Where-Object { $_.OpCode.Code -eq 'Ldstr' } | ForEach-Object { [string]$_.Operand })
        $tryCompleteOperands = @($tryCompleteReplacement.Body.Instructions | ForEach-Object { [string]$_.Operand })
        $tryCompleteStrings = @($tryCompleteReplacement.Body.Instructions | Where-Object { $_.OpCode.Code -eq 'Ldstr' } | ForEach-Object { [string]$_.Operand })
        $completeSelectionOperands = @($completeSelection.Body.Instructions | ForEach-Object { [string]$_.Operand })
        $commitStrings = @($commitSelection.Body.Instructions | Where-Object { $_.OpCode.Code -eq 'Ldstr' } | ForEach-Object { [string]$_.Operand })
        $rollbackStrings = @($rollbackSelection.Body.Instructions | Where-Object { $_.OpCode.Code -eq 'Ldstr' } | ForEach-Object { [string]$_.Operand })
        $prefixOperands = @($recoveredPrefix.Body.Instructions | ForEach-Object { [string]$_.Operand })
        $replacementOperands = @($replacementEntry.Body.Instructions | ForEach-Object { [string]$_.Operand })
        $deferredOperands = @($deferredMoveNext.Body.Instructions | ForEach-Object { [string]$_.Operand })
        $helperStrings = @($cleanupHelpers.Body.Instructions | Where-Object { $_.OpCode.Code -eq 'Ldstr' } | ForEach-Object { [string]$_.Operand })
        $cooldownConstants = @($recoveredPrefix.Body.Instructions | Where-Object { $_.OpCode.Code -eq 'Ldc_R4' -and [single]$_.Operand -eq 10.0 })
        $obsoleteValidator = @($machineChangeType.Methods | Where-Object Name -eq 'ValidateLegacyMachine')

        $machineChangeUnrestrictedZeroCooldown = $validateStrings -contains 'VALIDATE result=DEFERRED mode=unrestricted cooldownSeconds=0 machine=' -and
            $registerStrings -contains 'REGISTERED entry=Meeting.BDKIMPEDKCJ restrictions=none cooldownSeconds=0 replacement=validation-dispatch/deferred-cleanup spawnPosition=target/ready-warp networkAnchor=native-bounds-excluding-missile-template-hierarchy destroy=Unity-Network/rebound-server-retire scriptHelpers=SpringControl' -and
            @($validateOperands | Where-Object { $_ -match 'FindObjectsOfType|::smethod_4\(BlockData|RegulationData|HDBLLPODNLN' }).Count -eq 0 -and
            $obsoleteValidator.Count -eq 0 -and
            $meetingPrefixStrings -contains 'LIMIT bypassed=attack mode=Legacy restrictions=none cooldownSeconds=0' -and
            @($meetingPrefixOperands | Where-Object { $_ -match 'LegacyMachineChangeIngame::ApplyZeroCooldown\(' }).Count -ge 1 -and
            @($zeroCooldownOperands | Where-Object { $_ -match 'System\.Reflection\.FieldInfo::SetValue\(' }).Count -ge 1 -and
            @($zeroCooldownConstants | Where-Object { $_ -le -2000000000.0 }).Count -ge 1 -and
            @($meetingPrefixOperands | Where-Object { $_ -match 'KDODJPCDEHO' }).Count -ge 1 -and
            @($meetingPostfixOperands | Where-Object { $_ -match 'LegacyMachineChangeIngame::RestoreAttackBypass\(' }).Count -ge 1 -and
            @($prefixOperands | Where-Object { $_ -match 'LegacyMachineChangeIngame::ValidateCurrentSelection\(' }).Count -ge 1 -and
            $cooldownConstants.Count -ge 2

        $machineChangeDeferredCleanup = @($validateOperands | Where-Object { $_ -match 'LegacyMachineChangeIngame::BeginDeferredReplacement\(' }).Count -ge 1 -and
            $registerStrings -notcontains 'mDZmboOC2mLx2MmBzu$4bNw' -and
            $registerStrings -notcontains 'ReplacementPrefix' -and
            [string]$replacementEntry.Name -eq 'mDZmboOC2mLx2MmBzu$4bNw' -and
            @($replacementOperands | Where-Object { $_ -match 'LegacyMachineChangeIngame::BeginDeferredReplacement\(' }).Count -eq 0 -and
            @($tryCompleteOperands | Where-Object { $_ -match 'mDZmboOC2mLx2MmBzu.*4bNw\(' }).Count -ge 1 -and
            $tryCompleteStrings -contains 'MPatcherMachineChangeAnchorRoot' -and
            @($deferredOperands | Where-Object { $_ -match 'WaitForEndOfFrame::.ctor\(' }).Count -ge 1 -and
            @($deferredOperands | Where-Object { $_ -match 'Resources::UnloadUnusedAssets\(' }).Count -eq 1 -and
            @($deferredOperands | Where-Object { $_ -match 'CleanupSpringControlHelpers\(' }).Count -ge 2 -and
            @($deferredOperands | Where-Object { $_ -match 'CompleteDeferredSelection\(' }).Count -ge 1 -and
            $helperStrings -contains 'LibSawUtil_reg.SpringControl' -and
            $helperStrings -contains 'joint' -and
            $helperStrings -contains 'isEnabled'

        $machineChangeDirectSpawn = $registerStrings -contains 'smethod_22' -and
            $registerStrings -contains 'smethod_26' -and
            $registerStrings -contains 'NetworkInstantiatePrefix' -and
            $registerStrings -contains 'InitializeMachinePrefix' -and
			$registerStrings -contains 'InitializeMachinePostfix' -and
            @($networkInstantiateOperands | Where-Object { $_ -match 'LegacyMachineChangeIngame::directSpawnPosition' }).Count -ge 1 -and
            @($networkInstantiateOperands | Where-Object { $_ -match 'LegacyMachineChangeIngame::directSpawnRotation' }).Count -ge 1 -and
            @($initializeMachineOperands | Where-Object { $_ -match 'LegacyMachineChangeIngame::directSpawnPosition' }).Count -ge 1 -and
			@($initializeMachinePostfix.Body.Instructions | Where-Object { $_.OpCode.Code -eq 'Throw' }).Count -eq 1 -and
			$registerStrings -contains 'BeforeSyncStructure' -and
			$registerStrings -contains 'InitializeTranspiler' -and
			@($initializeMachineOperands | Where-Object { $_ -match 'LegacyMachineNetworkAnchor::Begin\(' }).Count -eq 1 -and
			@($captureNetworkAnchorOperands | Where-Object { $_ -match 'MachineSerializer::JJMDFCDDJBA' }).Count -eq 1 -and
			@($captureNetworkAnchor.Body.Instructions | Where-Object { $_.OpCode.Code -eq 'Stfld' }).Count -eq 0 -and
            @($tryCompleteOperands | Where-Object { $_ -match 'LegacyMachineChangeIngame::directSpawnPositionValid' }).Count -ge 2

        $machineChangeSinglePositioning = @($finalizeWrapperOperands | Where-Object { $_ -match 'LegacyMachineChangeIngame::networkSpawnPositionApplied' }).Count -ge 2 -and
            @($finalizeWrapperOperands | Where-Object { $_ -match 'LegacyMachineChangeIngame::initializeSpawnPositionApplied' }).Count -ge 2 -and
			@($finalizeWrapperOperands | Where-Object { $_ -match 'LegacyMachineChangeIngame::networkAnchorRepaired' }).Count -ge 2 -and
            @($finalizeWrapperOperands | Where-Object { $_ -match 'LegacyMachineChangeIngame::FinalizeAcceptedReplacementCore\(' }).Count -eq 1 -and
			$finalizeStrings -contains 'DIRECT_WARP' -and
            $finalizeStrings -contains 'FALLBACK_WARP' -and
			@($finalizeOperands | Where-Object { $_ -match 'LegacySpawnFinalization::Run\(' }).Count -eq 1 -and
			@($deferredOperands | Where-Object { $_ -match 'AwaitSpawnFinalization\(' }).Count -eq 2 -and
			@($deferredOperands | Where-Object { $_ -match 'LegacySpawnFinalization::Succeeded' }).Count -eq 2

        $machineChangeTransactionalRegistration = @($registerOperands | Where-Object { $_ -match 'HarmonyLib\.Harmony::UnpatchAll\(' }).Count -ge 1 -and
            $registerStrings -contains 'REGISTER_ROLLBACK_FAILED type=' -and
            $showDebugStrings -contains 'MESSAGE suppressed reason=deferred-dispatch text=' -and
            $rollbackStrings -contains 'TRANSACTION rollback-suppressed reason=deferred-dispatch'

        $machineChangeRollbackRecovery = $commitStrings -contains 'TRANSACTION commit-deferred reason=replacement-in-progress' -and
            @($completeSelectionOperands | Where-Object { $_ -match 'CommitSelectionTransaction\(' }).Count -ge 1 -and
            @($deferredOperands | Where-Object { $_ -match 'RollbackSelectionTransaction\(' }).Count -ge 2 -and
            @($deferredOperands | Where-Object { $_ -match 'RestorePreviousCooldown\(' }).Count -ge 2 -and
            @($deferredOperands | Where-Object { $_ -match 'TryCompleteLegacyReplacement\(' }).Count -eq 2 -and
            @($deferredOperands | Where-Object { $_ -match 'REPLACE_ROLLBACK result=' }).Count -eq 1

		$machineChangeReboundRetire =
			@($networkDestroyOperands | Where-Object { $_ -match 'LegacyTransientReconnect::TryRetireReboundView\(' }).Count -eq 1 -and
			$networkDestroyStrings -contains 'DESTROY transport=Legacy mode=rebound-server-retire root='
    }
    $presenceTypes = @($allTypes | Where-Object FullName -like 'MPatcherFork.CustomPatches.PlayerPresence*')
    $requiredUpdaterTypes = @(
        'MPatcherFork.CustomPatches.MPatcherUpdateManifest',
        'MPatcherFork.CustomPatches.MPatcherUpdater',
		'MPatcherFork.CustomPatches.MPatcherUpdaterUi',
		'MPatcherFork.CustomPatches.MPatcherUpdateChannelWatcher',
		'MPatcherFork.CustomPatches.MPatcherStartupGate'
    )
    $updaterTypesPresent = @($requiredUpdaterTypes | Where-Object { $_ -notin $allTypes.FullName }).Count -eq 0
    $start = $mpatchrType.Methods | Where-Object Name -eq 'Start'
    if ($null -ne $start -and $start.HasBody) {
        $startOperands = @($start.Body.Instructions | ForEach-Object { [string]$_.Operand })
        $updaterStartHook = @($startOperands | Where-Object { $_ -match 'MPatcherFork\.CustomPatches\.MPatcherUpdater::TryStart\(' }).Count -eq 1
        $settingsUiSchedulerHook = @($startOperands | Where-Object { $_ -match 'Class35::EnsureSettingsUiScheduled\(' }).Count -eq 1
		$startupReadyHook = @($start.Body.Instructions | Where-Object {
			$_.OpCode.Code -eq 'Call' -and [string]$_.Operand -match 'MPatcherFork\.CustomPatches\.MPatcherStartupGate::MarkRuntimeReady\('
		}).Count -eq 1
        $disabledUpdaterLogAbsent = @($start.Body.Instructions | Where-Object { $_.OpCode.Code -eq 'Ldstr' -and [string]$_.Operand -match '\[MPatcher\.Update\].*disabled' }).Count -eq 0
    }
	$startupGateType = $allTypes | Where-Object FullName -eq 'MPatcherFork.CustomPatches.MPatcherStartupGate'
	$startupGateContract = $null -ne $startupGateType -and $null -ne $completeLoad -and $completeLoad.IsStatic -and
		@($load.Body.Instructions | Where-Object {
			$_.OpCode.Code -eq 'Call' -and [string]$_.Operand -match 'MPatcherFork\.CustomPatches\.MPatcherStartupGate::Begin\('
		}).Count -eq 1 -and
		$startupReadyHook
    $settingsUi = $allTypes | Where-Object FullName -eq 'Class35'
    $settingsOperands = @(
        foreach ($method in $settingsUi.Methods) {
            if ($method.HasBody) {
                $method.Body.Instructions | ForEach-Object { [string]$_.Operand }
            }
        }
    )
    $updaterUiHooks = @($settingsOperands | Where-Object { $_ -match 'MPatcherFork\.CustomPatches\.MPatcherUpdaterUi::Create\(' }).Count -eq 1 -and
        @($settingsOperands | Where-Object { $_ -match 'MPatcherFork\.CustomPatches\.MPatcherUpdaterUi::BindLauncher\(' }).Count -eq 1
	$updaterType = $allTypes | Where-Object FullName -eq 'MPatcherFork.CustomPatches.MPatcherUpdater'
	$updaterUiType = $allTypes | Where-Object FullName -eq 'MPatcherFork.CustomPatches.MPatcherUpdaterUi'
	$updaterStrings = @(
		foreach ($type in @($updaterType, $updaterUiType)) {
			if ($null -ne $type) {
				foreach ($method in $type.Methods) {
					if ($method.HasBody) {
						$method.Body.Instructions | Where-Object { $_.OpCode.Code -eq 'Ldstr' } | ForEach-Object { [string]$_.Operand }
					}
				}
			}
		}
	)
	$updaterChannelContract = 'Stable' -in $updaterStrings -and 'Alpha' -in $updaterStrings -and
		'https://github.com/RestartLive113/MPatcher/releases/download/alpha/MPatcherUpdate.ini' -in $updaterStrings -and
		$null -ne ($updaterType.Methods | Where-Object Name -eq 'ChangeChannel')
	$timedUpdateNoticeContract = 'MPatcherUpdateNotice' -in $updaterStrings -and
		'NOTICE_CREATED background=none placement=version-stack offsetX=-135 offsetY=11 visibleSeconds=20 fadeSeconds=3 raycast=False' -in $updaterStrings -and
		$null -ne ($allTypes | Where-Object FullName -eq 'MPatcherFork.CustomPatches.MPatcherUpdateNoticeLifetime')
	$autoReconnectType = $allTypes | Where-Object FullName -eq 'MPatcherFork.CustomPatches.AutoReconnect'
	$autoReconnectUiType = $allTypes | Where-Object FullName -eq 'MPatcherFork.CustomPatches.AutoReconnectSettingsUi'
	$settingsType = $allTypes | Where-Object FullName -eq 'settingsIngame'
	$autoReconnectCreateRow = $autoReconnectUiType.Methods | Where-Object Name -eq 'CreateRow'
	$autoReconnectCreateStrings = @($autoReconnectCreateRow.Body.Instructions |
		Where-Object { $_.OpCode.Code -eq 'Ldstr' } | ForEach-Object { [string]$_.Operand })
	$autoReconnectShowConsent = $legacyRecoveryAgentType.Methods | Where-Object Name -eq 'ShowCrashRestoreConsent'
	$autoReconnectShowConsentOperands = @($autoReconnectShowConsent.Body.Instructions | ForEach-Object { [string]$_.Operand })
	$autoReconnectGameStart = $legacyRecoveryType.Methods | Where-Object Name -eq 'GameStartPostfix'
	$autoReconnectDisconnect = $legacyRecoveryType.Methods | Where-Object Name -eq 'GameDisconnectedPrefix'
	$autoReconnectGateOperands = @($autoReconnectGameStart.Body.Instructions + $autoReconnectDisconnect.Body.Instructions |
		ForEach-Object { [string]$_.Operand })
	$autoReconnectSettingsContract = $null -ne $autoReconnectType -and $null -ne $autoReconnectUiType -and
		@($settingsType.Fields | Where-Object { $_.Name -in @('autoReconnect', 'autoReconnectCrashRestore') }).Count -eq 2 -and
		@($settingsOperands | Where-Object { $_ -match 'AutoReconnectSettingsUi::CreateRow\(' }).Count -eq 1 -and
		@($settingsOperands | Where-Object { $_ -match 'LegacyIndivFixBundle::SettingChanged\(' }).Count -eq 1 -and
		@($settingsOperands | Where-Object { $_ -match 'LegacyHostAutoPort::SetEnabled\(|LegacyHostMigration::SetEnabled\(' }).Count -eq 0 -and
		'Toggle_AutoReconect' -in $autoReconnectCreateStrings -and 'AutoReconect' -in $autoReconnectCreateStrings -and
		'Auto restore after crash' -in $autoReconnectCreateStrings -and 'Restore manually' -in $autoReconnectCreateStrings -and
		@($autoReconnectShowConsentOperands | Where-Object { $_ -match 'AutoReconnect::get_AutomaticCrashRestore\(' }).Count -ge 1 -and
		@($autoReconnectShowConsentOperands | Where-Object { $_ -match 'SubmitCrashRestoreConsent\(' }).Count -eq 1 -and
		@($autoReconnectGateOperands | Where-Object { $_ -match 'AutoReconnect::get_Enabled\(' }).Count -ge 2 -and
		@($autoReconnectGateOperands | Where-Object { $_ -match 'LegacyIndivFixBundle::get_Enabled\(' }).Count -ge 1
    if ($ExpectPlayerPresenceExcluded) {
        $checks.player_presence_types_excluded = $presenceTypes.Count -eq 0
        $presenceControls = @(
            foreach ($method in $settingsUi.Methods) {
                if ($method.HasBody) {
                    $method.Body.Instructions | Where-Object { $_.OpCode.Code -eq 'Ldstr' -and $_.Operand -in @('Toggle_PlayerPresence', 'Player Status') }
                }
            }
        )
        $checks.player_presence_settings_ui_excluded = $presenceControls.Count -eq 0
		$exclusionLog = @($bootstrapInstructions | Where-Object { $_.OpCode.Code -eq 'Ldstr' -and $_.Operand -eq '[PLAYER-PRESENCE] EXCLUDED_FROM_BUILD' })
        $checks.player_presence_exclusion_logged = $exclusionLog.Count -eq 1
        $settings = $allTypes | Where-Object FullName -eq 'settingsIngame'
        $checks.player_presence_saved_settings_preserved = @($settings.Fields | Where-Object { $_.Name -in @('playerPresence', 'playerPresenceMode') }).Count -eq 2
    }
    if ($ExpectPrecompiledPatches) {
        $gamePath = Join-Path (Split-Path -Parent $assembly) 'Assembly-CSharp.dll'
        $game = [dnlib.DotNet.ModuleDefMD]::Load($gamePath)
        try {
            $resources = @($module.Resources | Where-Object Name -eq 'MPatcher.Prepatch.Plans.tsv')
            $checks.precompiled_manifest_resource = $resources.Count -eq 1 -and $resources[0].IsPrivate
            if (-not $checks.precompiled_manifest_resource) { throw 'Precompiled manifest missing or duplicated' }
            $keys = [Collections.Generic.HashSet[string]]::new()
            $valid = $true
            $lines = [Text.Encoding]::UTF8.GetString($resources[0].GetResourceData()).Split("`n")
            foreach ($line in $lines) {
                if ([string]::IsNullOrWhiteSpace($line)) { continue }
                $fields = $line.Split("`t")
                if ($fields.Count -ne 4 -or $fields[0] -notmatch '^[A-F0-9]{64}$' -or -not $keys.Add($fields[0])) { $valid=$false; break }
                $owner = [Text.Encoding]::UTF8.GetString([Convert]::FromBase64String($fields[1]))
                $type = [Text.Encoding]::UTF8.GetString([Convert]::FromBase64String($fields[2])).TrimStart('~').Replace('+','/')
                $name = [Text.Encoding]::UTF8.GetString([Convert]::FromBase64String($fields[3]))
                $targetModule = if ($owner -eq 'MPatchrMain') { $module } elseif ($owner -eq 'Assembly-CSharp') { $game } else { $null }
                if ($null -eq $targetModule) { $valid=$false; break }
                $targetType = $targetModule.Find($type,$false)
                $methods = @(if ($null -ne $targetType) { $targetType.Methods | Where-Object Name -eq $name })
                if ($methods.Count -ne 1 -or -not $methods[0].HasBody -or -not $methods[0].IsStatic -or $name -ne ('__MPatcher_Precompiled_'+$fields[0])) { $valid=$false; break }
            }
            $checks.precompiled_manifest_targets = $valid -and $keys.Count -gt 0
            $checks.precompiled_no_pointer_tables = $module.MetaData.TablesStream.MethodPtrTable.Rows -eq 0 -and $game.MetaData.TablesStream.MethodPtrTable.Rows -eq 0 -and $module.MetaData.TablesStream.FieldPtrTable.Rows -eq 0 -and $game.MetaData.TablesStream.FieldPtrTable.Rows -eq 0
            $checks.precompiled_bootstrap = @($bootstrapInstructions | Where-Object { [string]$_.Operand -eq 'System.Void MPatcherFork.CustomPatches.PrepatchCapture::Begin()' }).Count -eq 1
            $runtime = $module.Find('MPatcherFork.CustomPatches.PrepatchRuntime',$false)
            $checks.precompiled_runtime = $null -ne $runtime -and @($runtime.Methods | Where-Object Name -eq 'Resolve').Count -eq 1
        }
        finally { $game.Dispose() }
    }
    $onApplicationQuit = $mpatchrType.Methods | Where-Object Name -eq 'OnApplicationQuit'
    if ($null -ne $onApplicationQuit -and $onApplicationQuit.HasBody) {
        $cleanExitCalls = @(
            $onApplicationQuit.Body.Instructions |
                ForEach-Object { [string]$_.Operand } |
                Where-Object { $_ -eq 'System.Void MPatcherFork.CustomPatches.CrashDiagnostics::MarkCleanExit()' }
        )
        $crashDiagnosticsCleanExitHook = $cleanExitCalls.Count -eq 1
    }
}
finally {
    $module.Dispose()
}
$checks.widget_controller_overrides = $actualWidgetOverrides.Count -eq $expectedWidgetOverrides.Count -and @($expectedWidgetOverrides | Where-Object { $_ -notin $actualWidgetOverrides }).Count -eq 0
$checks.crash_diagnostics_no_early_game_object = $crashDiagnosticsNoEarlyGameObject
$checks.crash_diagnostics_clean_exit_hook = $crashDiagnosticsCleanExitHook
$checks.product_version = $actualProductVersion -eq $ExpectedProductVersion
$checks.updater_types = $updaterTypesPresent
$checks.updater_start_hook = $updaterStartHook
$checks.settings_ui_scheduler_hook = $settingsUiSchedulerHook
$checks.updater_ui_hooks = $updaterUiHooks
$checks.disabled_updater_log_absent = $disabledUpdaterLogAbsent
$checks.startup_gate_contract = $startupGateContract
$checks.updater_channel_contract = $updaterChannelContract
$checks.timed_update_notice_contract = $timedUpdateNoticeContract
$checks.auto_reconnect_settings_contract = $autoReconnectSettingsContract
$checks.indivfix_server_bundle_contract = $legacyIndivFixBundleContract
$checks.main_menu_version_type = $mainMenuVersionTypePresent
$checks.main_menu_version_register_hook = $mainMenuVersionRegisterHook
$checks.main_menu_version_contract = $mainMenuVersionContract
$checks.machine_change_unrestricted_zero_cooldown = $machineChangeUnrestrictedZeroCooldown
$checks.machine_change_deferred_cleanup = $machineChangeDeferredCleanup
$checks.machine_change_direct_spawn = $machineChangeDirectSpawn
$checks.machine_change_single_positioning = $machineChangeSinglePositioning
$checks.machine_change_transactional_registration = $machineChangeTransactionalRegistration
$checks.machine_change_rollback_recovery = $machineChangeRollbackRecovery
$checks.machine_change_rebound_retire = $machineChangeReboundRetire
$checks.legacy_connection_stability_types = $legacyConnectionStabilityTypes
$checks.legacy_connection_retry_contract = $legacyConnectionRetryContract
$checks.legacy_facilitator_register_hook = $legacyFacilitatorRegisterHook
$checks.legacy_facilitator_native_contract = $legacyFacilitatorNativeContract
$checks.legacy_host_stability_types = $legacyHostStabilityTypes
$checks.legacy_host_register_hook = $legacyHostRegisterHook
$checks.legacy_host_directory_contract = $legacyHostDirectoryContract
$checks.legacy_host_facilitator_register_hook = $legacyHostFacilitatorRegisterHook
$checks.legacy_host_facilitator_types = $legacyHostFacilitatorTypes
$checks.legacy_host_facilitator_native_contract = $legacyHostFacilitatorNativeContract
$checks.legacy_master_lifecycle_register_hook = $legacyMasterLifecycleHook
$checks.legacy_master_lifecycle_contract = $legacyMasterLifecycleContract
$checks.legacy_master_query_register_hook = $legacyMasterQueryHook
$checks.legacy_master_query_contract = $legacyMasterQueryContract
$checks.legacy_socket_self_test_register_hook = $legacySocketSelfTestHook
$checks.legacy_socket_self_test_contract = $legacySocketSelfTestContract
$checks.legacy_catalogue_recovery_register_hook = $legacyCatalogueRecoveryHook
$checks.legacy_catalogue_recovery_contract = $legacyCatalogueRecoveryContract
$checks.legacy_transient_reconnect_types = $legacyMachineRecoveryTypes
$checks.legacy_transient_reconnect_register_hook = $legacyMachineRecoveryRegisterHook
$checks.legacy_transient_reconnect_contract = $legacyMachineRecoveryContract
$passed = -not ($checks.Values -contains $false)
$result = [pscustomobject]@{
    checked_utc = [DateTime]::UtcNow.ToString('o')
    assembly = $assembly
    sha256 = (Get-FileHash -Algorithm SHA256 -LiteralPath $assembly).Hash
    expected_product_version = $ExpectedProductVersion
    product_version = $actualProductVersion
    checks = $checks
    player_presence_included = $presenceTypes.Count -gt 0
    passed = $passed
    strong_name_note = 'Public-signed with the recovered public key; the original private signature cannot be reproduced.'
    deployed = $false
    runtime_tested = $false
}
$json = $result | ConvertTo-Json -Depth 6
$json | Set-Content -LiteralPath (Join-Path $logDirectory 'verification-summary.json') -Encoding UTF8
$json
if (-not $passed) {
    exit 1
}
