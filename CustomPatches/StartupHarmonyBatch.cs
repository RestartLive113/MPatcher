using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;
using HarmonyLib;
using MPatchrMain;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
	internal static class StartupHarmonyBatch
	{
		private const string PatchId = "local.moddev.machinecraft.build-heavy.shared.v2";
		private static readonly object RegistrationLock = new object();
		private static readonly Type StickyPreviewPatch = typeof(global::w5chUZMTNzi3hsv_ZuyAmjsrIqV_JS4UmvQj9ChlfHgM0YkamwAOecvqmdcCMPuZpA.ZaPtz1XbmxIr9Gg_l8jFEtOyYpePV_0024eSA6CbHt_OILZrRco40bZ2fFF2aqOpAhkwnNqWjn64ZpiZnq_apiQHarimF4RwG7xNBIjbY2m3PPb0AeYLJaYnEg_WojuhwF3jgg);
		private static Harmony sharedHarmony;
		private static MPatchrMain.patching.Patch stickyPatch;
		private static bool attempted;
		private static volatile bool registered;
		private static int settingsDispatchLogged;
		private static int updateDispatchLogged;

		internal static bool Registered
		{
			get { return registered; }
		}

		internal static bool TryRegister()
		{
			lock (RegistrationLock)
			{
				if (attempted)
					return registered;
				attempted = true;
				RegisterOnce();
				return registered;
			}
		}

		private static void RegisterOnce()
		{
			Stopwatch startup = Stopwatch.StartNew();
			Harmony batch = new Harmony(PatchId);
			bool legacyMarked = false;
			try
			{
				MethodInfo settingsPreview = AccessTools.Method(typeof(Build), "JOHIPODALCN", Type.EmptyTypes);
				MethodInfo buildUpdate = AccessTools.Method(typeof(Build), "Update", Type.EmptyTypes);
				if (settingsPreview == null || buildUpdate == null)
					throw new MissingMethodException("Build.JOHIPODALCN/Update");

				PatchProcessor previewProcessor = new PatchProcessor(batch, settingsPreview);
				previewProcessor.AddPostfix(Method(typeof(StartupHarmonyBatch), "BuildSettingsPostfix"));
				previewProcessor.AddTranspiler(Method(typeof(StartupHarmonyBatch), "BuildSettingsTranspiler", Priority.Last));
				previewProcessor.Patch();
				long previewMs = startup.ElapsedMilliseconds;
				Log("STARTUP_TIMING target=Build.JOHIPODALCN shared=Coupler+Setup+MCNAnnotations stepMs="
					+ previewMs + " totalMs=" + previewMs);

				PatchProcessor updateProcessor = new PatchProcessor(batch, buildUpdate);
				updateProcessor.AddPostfix(Method(typeof(StartupHarmonyBatch), "BuildUpdatePostfix", Priority.Last));
				updateProcessor.AddTranspiler(Method(typeof(SetupPrecisionUi), "BuildUpdateTranspiler"));
				updateProcessor.Patch();
				long totalMs = startup.ElapsedMilliseconds;
				Log("STARTUP_TIMING target=Build.Update shared=CouplerUI+CouplerPreview+Setup stepMs="
					+ (totalMs - previewMs) + " totalMs=" + totalMs);

				legacyMarked = MarkLegacyPatchHandled();
				sharedHarmony = batch;
				registered = true;
				Log("REGISTERED targets=Build.JOHIPODALCN,Build.Update regenerations=2 composition=single-slot-safe optionalMCNAnnotations=guarded");
			}
			catch (Exception error)
			{
				registered = false;
				try { batch.UnpatchAll(PatchId); }
				catch (Exception rollbackError) { Log("ROLLBACK_FAILED " + rollbackError); }
				if (legacyMarked)
					global::OhrvFPoRtmqzpOPJ_gdyp43sPzNUY8iJz48Anha3tAYq.j9O0UPvu4fmbYepM3CYIC_0024s.Remove(StickyPreviewPatch);
				sharedHarmony = null;
				Log("REGISTER_FAILED " + error);
			}
		}

		private static HarmonyMethod Method(Type owner, string name)
		{
			return Method(owner, name, Priority.Normal);
		}

		private static HarmonyMethod Method(Type owner, string name, int priority)
		{
			MethodInfo method = AccessTools.Method(owner, name);
			if (method == null)
				throw new MissingMethodException(owner.Name, name);
			HarmonyMethod result = new HarmonyMethod(method);
			result.priority = priority;
			return result;
		}

		private static IEnumerable<CodeInstruction> BuildSettingsTranspiler(IEnumerable<CodeInstruction> instructions,
			ILGenerator generator, MethodBase __originalMethod)
		{
			IEnumerable<CodeInstruction> copied = CouplerRotationCopy.SettingsTranspiler(instructions, generator);
			return SetupPrecision.FloatReadTranspiler(copied, __originalMethod);
		}

		private static void BuildSettingsPostfix(Build __instance, BlockData ___DFAAJHFLMJL,
			BlockData ___LBBOFMGMMFF, GameObject ___FBGCMCPNDAH, GameObject ___DNFHFEBGNPM,
			ref bool ___OIAOGDEPDCO, FreeCameraController ___BOIEJCIBHKI, ref float ___JBBCIHLBDNL)
		{
			if (Interlocked.Exchange(ref settingsDispatchLogged, 1) == 0)
				Log("RUNTIME_DISPATCH target=Build.JOHIPODALCN callbacks=CouplerPreview+MCNAnnotations");
			CouplerRotation.PreviewPostfix(___LBBOFMGMMFF, ___FBGCMCPNDAH, ___DNFHFEBGNPM);
			StickyNotesPreviewPostfix(__instance, ___DFAAJHFLMJL, ___LBBOFMGMMFF,
				ref ___OIAOGDEPDCO, ___BOIEJCIBHKI, ref ___JBBCIHLBDNL);
		}

		private static void BuildUpdatePostfix(Build __instance, BlockData ___LBBOFMGMMFF,
			BlockController ___MLJHGDBPMEA, HIPBCCKFFAG ___FFJDGJFPLAD, GameObject ___LENNGHKPCAN)
		{
			if (Interlocked.Exchange(ref updateDispatchLogged, 1) == 0)
				Log("RUNTIME_DISPATCH target=Build.Update callbacks=SetupUI+CouplerUI+CouplerPreview");
			SetupPrecisionUi.UpdatePostfix(__instance);
			CouplerRotationUi.UpdatePostfix(__instance);
			CouplerRotationPreview.PreviewPostfix(___LBBOFMGMMFF, ___MLJHGDBPMEA, ___FFJDGJFPLAD, ___LENNGHKPCAN);
		}

		private static bool MarkLegacyPatchHandled()
		{
			if (global::OhrvFPoRtmqzpOPJ_gdyp43sPzNUY8iJz48Anha3tAYq.j9O0UPvu4fmbYepM3CYIC_0024s.Contains(StickyPreviewPatch))
				return false;
			global::OhrvFPoRtmqzpOPJ_gdyp43sPzNUY8iJz48Anha3tAYq.j9O0UPvu4fmbYepM3CYIC_0024s.Add(StickyPreviewPatch);
			return true;
		}

		private static bool LegacyStickyPatchEnabled()
		{
			settingsIngame settings = MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68;
			if (settings == null || !settings.stickyNotes)
				return false;

			MPatchrMain.patching.Patch current = stickyPatch;
			if (current == null)
			{
				foreach (MPatchrMain.patching.Patch candidate in global::FMpPDgPqT_0024MlkjDbhXAGLgVbz45OJagoxMsHXXVw14C6.ckaPtFGmKTrL9dtatRozMxw)
				{
					if (candidate == null || candidate.patchClasses == null)
						continue;
					foreach (Type patchClass in candidate.patchClasses)
						if (patchClass == StickyPreviewPatch)
						{
							current = candidate;
							stickyPatch = candidate;
							break;
						}
					if (current != null)
						break;
				}
			}
			return current != null && current.patched;
		}

		private static void StickyNotesPreviewPostfix(Build __instance, BlockData ___DFAAJHFLMJL,
			BlockData ___LBBOFMGMMFF, ref bool ___OIAOGDEPDCO, FreeCameraController ___BOIEJCIBHKI,
			ref float ___JBBCIHLBDNL)
		{
			if (!LegacyStickyPatchEnabled())
				return;
			global::w5chUZMTNzi3hsv_ZuyAmjsrIqV_JS4UmvQj9ChlfHgM0YkamwAOecvqmdcCMPuZpA.ZaPtz1XbmxIr9Gg_l8jFEtOyYpePV_0024eSA6CbHt_OILZrRco40bZ2fFF2aqOpAhkwnNqWjn64ZpiZnq_apiQHarimF4RwG7xNBIjbY2m3PPb0AeYLJaYnEg_WojuhwF3jgg.FeUAVwFbW6wGJJdNimZY9yI(
				__instance, ___DFAAJHFLMJL, ___LBBOFMGMMFF, ref ___OIAOGDEPDCO, ___BOIEJCIBHKI, ref ___JBBCIHLBDNL);
		}

		private static void Log(string message)
		{
			try { global::mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng("[STARTUP-BATCH] " + message); }
			catch (Exception) { }
		}
	}
}
