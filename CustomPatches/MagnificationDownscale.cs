using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using McnCraft;
using UnityEngine;
using UnityEngine.UI;

namespace MPatcherFork.CustomPatches
{
	internal static class MagnificationDownscale
	{
		private const string PatchId = "local.mpatcher.magnification-downscale.v1-persistence";
		internal const string ReleaseProfile = "0.3.8-v1-persistence";
		internal const int MinimumPercent = 1;
		internal const int DefaultPercent = 100;
		internal const int MaximumPercent = 300;
		private static bool registered;
		private static Harmony harmony;
		private static GameObject lastSlider;
		private static int lastSliderPercent;
		private const float PersistenceDelaySeconds = 0.75f;
		private static BuildData pendingBuild;
		private static int pendingPercent;
		private static float pendingSaveAt;
		private static string pendingMachineName;
		private static string pendingFolderName;
		private static bool persistenceBusy;
		private static MagnificationPersistenceRunner persistenceRunner;
		private static string restoredPath;
		private static long restoredWriteTicks;
		private static float nextRestoreProbeAt;
		private static string lastRestoreFailure;

		internal static void TryRegister()
		{
			if (registered) return;
			try
			{
				Type iterator = typeof(MachineSerializer).GetNestedType("<MakeStructureNB>c__Iterator0", BindingFlags.NonPublic);
				if (iterator == null) throw new MissingMemberException("MakeStructureNB iterator");
				harmony = new Harmony(PatchId);
				Patch(typeof(BuildData), "CorrectMagnification", Type.EmptyTypes, null, null, "CorrectTranspiler");
				Patch(typeof(MachineController), "Initialize", new[] { typeof(string), typeof(BuildData), typeof(AssignData), typeof(Vector3) },
					null, "InitializePostfix", "InitializeTranspiler");
				Patch(iterator, "MoveNext", Type.EmptyTypes, null, null, "ReaderTranspiler");
				MagnificationDownscalePistons.Register(harmony);
				Patch(typeof(SceneMan), "SetSLD", new[] { typeof(string), typeof(int), typeof(bool) }, "SetSliderPrefix", null, null);
				Patch(typeof(SceneMan), "OnSlide", new[] { typeof(GameObject) }, null, "SliderChangedPostfix", null);
				Patch(typeof(Menu), "Update", Type.EmptyTypes, null, "MenuUpdatePostfix", null);
				registered = true;
				Log("REGISTERED profile=0.3.8-v1-persistence percent=1..300 legacyZero=100 modes=native-offline-or-meeting boss=native wire=native-header peers=require-patch persistence=debounced+cold-restore physics=v1-baseline physics-rework=deferred-0.3.9");
			}
			catch (Exception error)
			{
				registered = false;
				Log("REGISTER_FAILED " + error);
				try { if (harmony != null) harmony.UnpatchAll(PatchId); }
				catch (Exception rollbackError) { Log("ROLLBACK_FAILED " + rollbackError); }
				harmony = null;
			}
		}

		private static void Patch(Type type, string name, Type[] args, string prefix, string postfix, string transpiler)
		{
			MethodInfo target = AccessTools.Method(type, name, args);
			if (target == null) throw new MissingMethodException(type.FullName, name);
			harmony.Patch(target, Hook(prefix), Hook(postfix), Hook(transpiler), null);
			Log("HOOK target=" + type.FullName + "." + name);
		}

		private static HarmonyMethod Hook(string name)
		{
			return name == null ? null : new HarmonyMethod(AccessTools.Method(typeof(MagnificationDownscale), name));
		}

		// Missing/zero and negative legacy values used to become 100, not the new
		// minimum. This also makes opening an old build in the editor lossless.
		internal static int NormalizePercent(int value)
		{
			return value <= 0 ? DefaultPercent : Math.Min(value, MaximumPercent);
		}

		private static int CorrectPercent(int value, int nativeMinimum, int nativeMaximum)
		{
			return NormalizePercent(value);
		}

		internal static IEnumerable<CodeInstruction> CorrectTranspiler(IEnumerable<CodeInstruction> instructions)
		{
			List<CodeInstruction> codes = Copy(instructions);
			MethodInfo clamp = AccessTools.Method(typeof(Mathf), "Clamp", new[] { typeof(int), typeof(int), typeof(int) });
			int found = 0;
			for (int i = 3; i + 1 < codes.Count; i++)
			{
				if (codes[i].opcode != OpCodes.Call || !Equals(codes[i].operand, clamp)) continue;
				if (!Field(codes[i - 3], OpCodes.Ldfld, typeof(BuildData), "magnification")
					|| !Integer(codes[i - 2], 100) || !Integer(codes[i - 1], 300)
					|| !Field(codes[i + 1], OpCodes.Stfld, typeof(BuildData), "magnification"))
					throw new InvalidOperationException("Magnification native clamp layout changed");
				codes[i].operand = AccessTools.Method(typeof(MagnificationDownscale), "CorrectPercent");
				found++;
			}
			Require(found == 1, "one BuildData magnification clamp", found);
			return codes;
		}

		internal static IEnumerable<CodeInstruction> InitializeTranspiler(IEnumerable<CodeInstruction> instructions)
		{
			List<CodeInstruction> codes = Copy(instructions);
			int found = 0;
			for (int i = 1; i + 9 < codes.Count; i++)
			{
				if (!Field(codes[i - 1], OpCodes.Ldfld, typeof(BuildData), "magnification") || !Integer(codes[i], 100)) continue;
				if (codes[i + 1].opcode != OpCodes.Ble || codes[i + 2].opcode != OpCodes.Ldarg_0
					|| codes[i + 3].opcode != OpCodes.Ldarg_0
					|| !Field(codes[i + 4], OpCodes.Ldfld, typeof(MachineController), "HHGILAIOCLG")
					|| !Field(codes[i + 5], OpCodes.Ldfld, typeof(BuildData), "magnification")
					|| codes[i + 6].opcode != OpCodes.Conv_R4 || !Float(codes[i + 7], 0.01f)
					|| codes[i + 8].opcode != OpCodes.Mul
					|| !Field(codes[i + 9], OpCodes.Stfld, typeof(MachineController), "DJCDFLDHPHK"))
					throw new InvalidOperationException("Magnification native Initialize layout changed");
				// Retain the native Offline/Meeting and Boss tests, multiplication,
				// bounds/structure ordering, joint setup and body scaling.
				codes[i].opcode = OpCodes.Ldc_I4_0;
				codes[i].operand = null;
				found++;
			}
			Require(found == 1, "one owner scale gate", found);
			return codes;
		}

		internal static IEnumerable<CodeInstruction> ReaderTranspiler(IEnumerable<CodeInstruction> instructions)
		{
			List<CodeInstruction> codes = Copy(instructions);
			int validation = 0, header = 0, scale = 0;
			for (int i = 1; i + 1 < codes.Count; i++)
			{
				if (Integer(codes[i], 10100))
				{
					if (!Field(codes[i - 1], OpCodes.Ldfld, typeof(NMLMDCCDFPN), "CDCGCGDAOEO"))
						throw new InvalidOperationException("Magnification parent marker layout changed");
					if (codes[i + 1].opcode == OpCodes.Bge) validation++;
					else if (codes[i + 1].opcode == OpCodes.Blt) header++;
					else throw new InvalidOperationException("Magnification parent marker branch changed");
					codes[i].opcode = OpCodes.Ldc_I4;
					codes[i].operand = 10000 + MinimumPercent;
				}
				if (!Integer(codes[i], 100) || i + 8 >= codes.Count
					|| !Field(codes[i + 8], OpCodes.Stfld, typeof(MachineController), "DJCDFLDHPHK")) continue;
				if (!Local(codes[i - 1]) || codes[i + 1].opcode != OpCodes.Ble
					|| codes[i + 2].opcode != OpCodes.Ldarg_0 || codes[i + 3].opcode != OpCodes.Ldfld
					|| !Equals(codes[i - 1].operand, codes[i + 4].operand) || codes[i - 1].opcode != codes[i + 4].opcode
					|| codes[i + 5].opcode != OpCodes.Conv_R4 || !Float(codes[i + 6], 0.01f) || codes[i + 7].opcode != OpCodes.Mul)
					throw new InvalidOperationException("Magnification remote scale layout changed");
				codes[i].opcode = OpCodes.Ldc_I4_0;
				codes[i].operand = null;
				codes[i + 8].opcode = OpCodes.Call;
				codes[i + 8].operand = AccessTools.Method(typeof(MagnificationDownscale), "SetRemoteScale");
				scale++;
			}
			Require(validation == 1 && header == 1 && scale == 1, "remote validation/header/scale gates", validation + header + scale);
			return codes;
		}

		private static void SetRemoteScale(MachineController machine, float value)
		{
			machine.DJCDFLDHPHK = value;
			Log("REMOTE_HEADER scale=" + value.ToString("R", CultureInfo.InvariantCulture));
		}

		private static void SetSliderPrefix(SceneMan __instance, string __0, ref int __1)
		{
			if (!(__instance is Build) || __0 != "Mag") return;
			GameObject widget = __instance.GetSLD("Mag");
			if (!widget) return;
			Slider slider = widget.GetComponent<Slider>();
			if (!slider) return;
			// Native SLD_Mag is an integer 100..300 slider, with no multiplier or
			// endpoint sentinel. Keep its ordinary 1% / modifier stepping.
			if (slider.minValue != MinimumPercent)
			{
				slider.minValue = MinimumPercent;
				Log("UI_RANGE min=" + slider.minValue + " max=" + slider.maxValue + " wholeNumbers=" + slider.wholeNumbers);
			}
			__1 = NormalizePercent(__1);
		}

		private static void SliderChangedPostfix(SceneMan __instance, GameObject __0)
		{
			if (!(__instance is Build) || !__0 || __0.name != "SLD_Mag") return;
			Slider slider = __0.GetComponent<Slider>();
			if (!slider) return;
			int percent = (int)slider.value;
			if (ReferenceEquals(lastSlider, __0) && lastSliderPercent == percent) return;
			lastSlider = __0;
			lastSliderPercent = percent;
			Log("UI_VALUE percent=" + percent);
			SchedulePersistence(percent);
		}

		private static void SchedulePersistence(int percent)
		{
			BuildData build = Build.GFJLEEJELOL;
			if (build == null) return;
			pendingBuild = build;
			pendingPercent = percent;
			pendingSaveAt = Time.realtimeSinceStartup + PersistenceDelaySeconds;
			pendingMachineName = JKGKJLLFMLE.IGOBPLOLHEP == null ? string.Empty : JKGKJLLFMLE.IGOBPLOLHEP.machineName ?? string.Empty;
			pendingFolderName = JKGKJLLFMLE.IGOBPLOLHEP == null ? string.Empty : JKGKJLLFMLE.IGOBPLOLHEP.folderName ?? string.Empty;
			EnsurePersistenceRunner();
		}

		private static void EnsurePersistenceRunner()
		{
			if (persistenceRunner) return;
			GameObject host = new GameObject("MPatcher_MagnificationPersistence");
			host.hideFlags = HideFlags.HideAndDontSave;
			UnityEngine.Object.DontDestroyOnLoad(host);
			persistenceRunner = host.AddComponent<MagnificationPersistenceRunner>();
			Log("PERSISTENCE_RUNNER_READY lifecycle=Menu.Update");
		}

		private static void MenuUpdatePostfix()
		{
			EnsurePersistenceRunner();
		}

		internal static void TickPersistence(bool force)
		{
			if (pendingBuild == null || persistenceBusy || (!force && Time.realtimeSinceStartup < pendingSaveAt)) return;
			persistenceBusy = true;
			try
			{
				string machineName = JKGKJLLFMLE.IGOBPLOLHEP == null ? string.Empty : JKGKJLLFMLE.IGOBPLOLHEP.machineName ?? string.Empty;
				string folderName = JKGKJLLFMLE.IGOBPLOLHEP == null ? string.Empty : JKGKJLLFMLE.IGOBPLOLHEP.folderName ?? string.Empty;
				if (!ReferenceEquals(pendingBuild, Build.GFJLEEJELOL)
					|| !string.Equals(machineName, pendingMachineName, StringComparison.Ordinal)
					|| !string.Equals(folderName, pendingFolderName, StringComparison.Ordinal))
				{
					Log("PERSIST_SKIPPED reason=selection-changed percent=" + pendingPercent);
					pendingBuild = null;
					return;
				}

				JKGKJLLFMLE.HHGILAIOCLG = pendingBuild;
				string compressedPath;
				string storage = MachineCompression.TryPersistCurrentCompressed(pendingBuild, out compressedPath)
					? "mzbd" : "mcbd";
				if (storage == "mcbd") JKGKJLLFMLE.BOMAFGLNGMI();
				Log("PERSISTED percent=" + pendingBuild.magnification + " storage=" + storage
					+ (compressedPath == null ? string.Empty : " path=" + compressedPath));
				pendingBuild = null;
			}
			catch (Exception error)
			{
				pendingSaveAt = Time.realtimeSinceStartup + 2f;
				Log("PERSIST_FAILED percent=" + pendingPercent + " type=" + error.GetType().Name + " message=" + error.Message);
			}
			finally { persistenceBusy = false; }
		}

		internal static void TickColdRestore()
		{
			if (Time.realtimeSinceStartup < nextRestoreProbeAt) return;
			nextRestoreProbeAt = Time.realtimeSinceStartup + 0.25f;
			try
			{
				string path;
				long writeTicks;
				if (!MachineCompression.TryGetSelectedBuildFile(out path, out writeTicks)) return;
				if (string.Equals(path, restoredPath, StringComparison.OrdinalIgnoreCase) && writeTicks == restoredWriteTicks) return;

				BuildData diskBuild;
				if (!MachineCompression.TryReadBuild(path, out diskBuild) || diskBuild == null) return;
				int percent = diskBuild.magnification;
				if (percent < MinimumPercent || percent >= DefaultPercent)
				{
					restoredPath = path;
					restoredWriteTicks = writeTicks;
					lastRestoreFailure = null;
					return;
				}

				bool applied = false;
				if (JKGKJLLFMLE.HHGILAIOCLG != null)
				{
					JKGKJLLFMLE.HHGILAIOCLG.magnification = percent;
					applied = true;
				}
				if (Build.GFJLEEJELOL != null)
				{
					Build.GFJLEEJELOL.magnification = percent;
					applied = true;
				}
				if (!applied) return;

				restoredPath = path;
				restoredWriteTicks = writeTicks;
				lastRestoreFailure = null;
				Log("PERSISTENCE_RESTORED percent=" + percent + " path=" + path);
			}
			catch (Exception error)
			{
				string failure = error.GetType().Name + ":" + error.Message;
				if (!string.Equals(failure, lastRestoreFailure, StringComparison.Ordinal))
					Log("PERSISTENCE_RESTORE_FAILED type=" + error.GetType().Name + " message=" + error.Message);
				lastRestoreFailure = failure;
				nextRestoreProbeAt = Time.realtimeSinceStartup + 2f;
			}
		}

		private static void InitializePostfix(MachineController __instance)
		{
			Log("OWNER_INITIALIZED requested=" + __instance.HHGILAIOCLG.magnification
				+ " scale=" + __instance.DJCDFLDHPHK.ToString("R", CultureInfo.InvariantCulture)
				+ " boss=" + __instance.CHDEBOIIMCI + " transport=" + HNJDDKJLHMM.FHLGOMHPDLN
				+ " mode=" + JKGKJLLFMLE.EGFHGHKLNAO + " bodies=" + __instance.KBLANAFAJFP.Count);
		}

		internal static List<CodeInstruction> Copy(IEnumerable<CodeInstruction> source)
		{
			List<CodeInstruction> copy = new List<CodeInstruction>();
			foreach (CodeInstruction code in source) copy.Add(new CodeInstruction(code));
			return copy;
		}

		internal static bool Float(CodeInstruction code, float value) { return code.opcode == OpCodes.Ldc_R4 && Equals(code.operand, value); }
		private static bool Integer(CodeInstruction code, int value)
		{
			return (code.opcode == OpCodes.Ldc_I4 || code.opcode == OpCodes.Ldc_I4_S) && Convert.ToInt32(code.operand) == value;
		}
		internal static bool Field(CodeInstruction code, OpCode opcode, Type type, string name)
		{
			return code.opcode == opcode && Equals(code.operand, AccessTools.Field(type, name));
		}
		private static bool Local(CodeInstruction code)
		{
			return code.opcode == OpCodes.Ldloc || code.opcode == OpCodes.Ldloc_S || code.opcode == OpCodes.Ldloc_0
				|| code.opcode == OpCodes.Ldloc_1 || code.opcode == OpCodes.Ldloc_2 || code.opcode == OpCodes.Ldloc_3;
		}
		internal static void Require(bool valid, string shape, int actual)
		{
			if (!valid) throw new InvalidOperationException("Expected " + shape + "; matches=" + actual);
		}
		internal static void Log(string message)
		{
			try { global::mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng("[MAGNIFICATION] " + message); }
			catch (Exception) { }
		}
	}

	internal sealed class MagnificationPersistenceRunner : MonoBehaviour
	{
		private void Update()
		{
			MagnificationDownscale.TickColdRestore();
			MagnificationDownscale.TickPersistence(false);
		}
		private void OnApplicationQuit() { MagnificationDownscale.TickPersistence(true); }
	}
}
