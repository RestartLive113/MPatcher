using System;
using System.IO;
using HarmonyLib;
using McnCraft;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
	// Referenced only by a generated source override in Build-MagnificationResearch.
	// The ordinary 0.3.8 bootstrap never calls this class.
	internal static class MagnificationResearchBootstrap
	{
		private static bool publicAlpha;
		internal static bool Manual { get { return publicAlpha || Array.IndexOf(Environment.GetCommandLineArgs(), "--mpatcher-magnification-manual") >= 0; } }
		internal static float ManualMassUnit { get { return publicAlpha || (Manual && Array.IndexOf(Environment.GetCommandLineArgs(), "--mpatcher-magnification-manual-body-units") >= 0) ? 1000000f : 1f; } }
		private static bool attempted;
		private static int inertiaWarnings;
		private static void ObserveNativeLog(string message, string stack, LogType type)
		{
			if (message == null || message.IndexOf("Inertia tensor must be larger", StringComparison.Ordinal) < 0) return;
			inertiaWarnings++;
			if (inertiaWarnings <= 4) MagnificationDownscale.Log("NATIVE_INERTIA_WARNING index=" + inertiaWarnings
				+ " unityStack=" + (stack ?? string.Empty).Replace('\r', ' ').Replace('\n', '|')
				+ " managedStack=" + new System.Diagnostics.StackTrace(1, false).ToString().Replace('\r', ' ').Replace('\n', '|'));
		}
		// Called only by the generated public Alpha entry point. No probe flags,
		// fixed test path, synthetic input, world replacement or automatic exit.
		internal static void TryStartPublicAlpha()
		{
			if (attempted) return;
			attempted = true;
			publicAlpha = true;
			Application.logMessageReceived += ObserveNativeLog;
			RegisterPhysics();
		}
		internal static void TryStart()
		{
			if (attempted) return;
			attempted = true;
			string root = Path.GetFullPath(Path.Combine(Application.dataPath, "..")).TrimEnd('\\', '/');
			string expected = Path.Combine(Environment.GetEnvironmentVariable("USERPROFILE") ?? "", "Downloads\\MachineCraftMPatcherTest");
			if (!MagnificationInuProbe.IsTestRoot(root)
				|| (!Manual && Array.IndexOf(Environment.GetCommandLineArgs(), "--mpatcher-magnification-craft-probe") < 0))
			{
				MagnificationDownscale.Log("RESEARCH_REFUSED requires=isolated-test-root+manual-or-craft-probe-flag");
				return;
			}
			if (Array.IndexOf(Environment.GetCommandLineArgs(), "--mpatcher-inertia-native-probe") >= 0)
			{
				new GameObject("MPatcher_InertiaProbe").AddComponent<MagnificationInertiaProbe>();
				return;
			}
			Application.logMessageReceived += ObserveNativeLog;
			if (Array.IndexOf(Environment.GetCommandLineArgs(), "--mpatcher-contact-similarity-probe") >= 0)
            {
                new GameObject("MPatcher_ContactSimilarityProbe").AddComponent<MagnificationContactSimilarityProbe>();
                return;
            }
            if (Array.IndexOf(Environment.GetCommandLineArgs(), "--mpatcher-joint-pose-probe") >= 0)
			{
				new GameObject("MPatcher_JointPoseProbe").AddComponent<MagnificationJointPoseProbe>();
				return;
			}
			if (Array.IndexOf(Environment.GetCommandLineArgs(), "--mpatcher-angular-similarity-probe") >= 0)
			{
				new GameObject("MPatcher_AngularSimilarityProbe").AddComponent<MagnificationAngularSimilarityProbe>();
				return;
			}
			if (Array.IndexOf(Environment.GetCommandLineArgs(), "--mpatcher-gravity-similarity-probe") >= 0)
			{
				new GameObject("MPatcher_GravityNativeProbe").AddComponent<MagnificationGravityNativeProbe>();
				return;
			}
			if (Array.IndexOf(Environment.GetCommandLineArgs(), "--mpatcher-small-mass-probe") >= 0)
			{
				new GameObject("MPatcher_MassNativeProbe").AddComponent<MagnificationMassNativeProbe>();
				return;
			}
			if (Array.IndexOf(Environment.GetCommandLineArgs(), "--mpatcher-coupled-drive-probe") >= 0 || Array.IndexOf(Environment.GetCommandLineArgs(), "--mpatcher-coupled-similarity-probe") >= 0)
			{
				new GameObject("MPatcher_CoupledDriveProbeRunner").AddComponent<MagnificationCoupledDriveProbe>();
				return;
			}
			if (Array.IndexOf(Environment.GetCommandLineArgs(), "--mpatcher-joint-drive-probe") >= 0)
			{
				new GameObject("MPatcher_JointDriveProbeRunner").AddComponent<MagnificationJointDriveProbe>();
				return;
			}
			if (Array.IndexOf(Environment.GetCommandLineArgs(), "--mpatcher-mass-force-probe") >= 0)
			{
				new GameObject("MPatcher_MassForceProbeRunner").AddComponent<MagnificationForceProbe>();
				return;
			}
			if (Array.IndexOf(Environment.GetCommandLineArgs(), "--mpatcher-craft-native-control") >= 0)
			{
				MagnificationDownscale.Log("RESEARCH_NATIVE_CONTROL physicsHooks=disabled nativeMPatcher=retained inputAndObservers=probe");
				MagnificationScaleProbe.TryStart();
				return;
			}
			RegisterPhysics();
		}
		private static void RegisterPhysics()
		{
			Harmony harmony = new Harmony("local.mpatcher.magnification-research.039");
			try
			{
				MagnificationPhysicsMode.Register(harmony);
				MagnificationVisualFixes.Register(harmony);
				MagnificationJointAnchors.Register(harmony);
				MagnificationPistonDrive.Register(harmony);
				harmony.Patch(AccessTools.Method(typeof(MachineController), "Initialize"), null,
					new HarmonyMethod(AccessTools.Method(typeof(MagnificationResearchBootstrap), "Initialize")), null, null);
				harmony.Patch(AccessTools.Method(typeof(MachineController), "Respawn"), null,
					new HarmonyMethod(AccessTools.Method(typeof(MagnificationResearchBootstrap), "Respawn")), null, null);
				MagnificationDownscale.Log("RESEARCH_REGISTERED baseline=v25 release038=unchanged recoveredRecoil=emitter-mass acceptance=pending");
                if (Manual)
                {
                    MagnificationCraftModelProbe.Register(harmony);
                    MagnificationSafetyObserver.Register(harmony);
                    MagnificationPhysicsModeUi.Start();
                    MagnificationDownscale.Log((publicAlpha ? "ALPHA_MAGNIFICATION_READY revision=R68c baseline=R66 safetyUnits=true model=22 massUnit=" : "RESEARCH_MANUAL_READY revision=R68c baseline=R66 safetyUnits=true model=22 massUnit=") + ManualMassUnit.ToString("R", System.Globalization.CultureInfo.InvariantCulture) + " percent100=bypass input=native automaticRuns=false automaticExit=false nativeLimitExperiment=false jointUnitExperiment=false sledVisualUnits=true acceptance=pending");
                    return;
                }
				MagnificationScaleProbe.TryStart();
			}
			catch (Exception error)
			{
				harmony.UnpatchAll(harmony.Id);
				MagnificationDownscale.Log("RESEARCH_REGISTER_FAILED " + error);
			}
		}
		private static void Initialize(MachineController __instance)
		{
			MagnificationJointAnchors.FinalizeMachine(__instance);
			if (MagnificationCraftProbe.Model != 18 && MagnificationCraftProbe.Model != 19 && MagnificationCraftProbe.Model != 20 && MagnificationCraftProbe.Model != 22) MagnificationJointMassBalance.Track(__instance);
			if (MagnificationScalePolicy.Enabled) MagnificationCraftModelProbe.ApplyInertia(__instance);
			MagnificationRuntimeDiagnostics.Attach(__instance);
		}
		private static void Respawn(MachineController __instance)
		{
			if (MagnificationCraftProbe.Model != 18 && MagnificationCraftProbe.Model != 19 && MagnificationCraftProbe.Model != 20 && MagnificationCraftProbe.Model != 22) MagnificationJointMassBalance.Track(__instance);
			if (MagnificationScalePolicy.Enabled) MagnificationCraftModelProbe.ApplyInertia(__instance);
			MagnificationRuntimeDiagnostics.ObserveRespawn(__instance);
		}
	}
}


