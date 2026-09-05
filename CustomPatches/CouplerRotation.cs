using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using MPatchrMain;
using UnityEngine;
using UnityEngine.UI;

namespace MPatcherFork.CustomPatches
{
	internal static class CouplerRotation
	{
		private const string PatchId = "local.moddev.machinecraft.coupler-rotation.v10";
		private struct RotationEdit
		{
			internal bool Entered;
			internal bool RememberFree;
			internal BlockData Before;
			internal int Parameter;
			internal int PreviousValue;
			internal CouplerRotationProfiles.Rotation Vanilla;
		}
		private static readonly object RegistrationLock = new object();
		private static Harmony harmony;
		private static volatile bool registered;
		private static BlockData lastLoggedBlock;
		private static int lastLogTick;
		private static string lastPreviewFailure;
		private static BlockData lastOffsetBlock;
		private static int lastOffsetLogTick;
		private static int nativeRotationEdits;
		private static bool restoringReadOnlySlider;
		private static BlockData lastReadOnlyBlock;
		private static int lastReadOnlyLogTick;

		internal static void TryRegister()
		{
			lock (RegistrationLock)
				RegisterOnce();
		}

		private static void RegisterOnce()
		{
			if (registered)
				return;

			Stopwatch startup = Stopwatch.StartNew();
			long checkpoint = 0;
			try
			{
				Log("STARTUP_TIMING_BEGIN version=10");
				MethodInfo slider = AccessTools.Method(typeof(Build), "BKACIJDGAPP", new Type[] { typeof(string), typeof(GameObject) });
				MethodInfo preview = AccessTools.Method(typeof(Build), "JOHIPODALCN", Type.EmptyTypes);
				MethodInfo offset = AccessTools.Method(typeof(BlockData), "GetCouplerOffset");
				MethodInfo rotate = AccessTools.Method(typeof(BlockData), "RotCouplerParam", new Type[] { typeof(Vector3) });
				MethodInfo mirror = AccessTools.Method(typeof(BlockData), "FlipParam", Type.EmptyTypes);
				MethodInfo conversion = AccessTools.Method(typeof(BDLEJBBJJOI), "GKCKPLGPDFK", new Type[] { typeof(Vector3) });
				MethodInfo copyAction = AccessTools.Method(typeof(BlockData), "CopyAction", new Type[] { typeof(BlockData) });
				MethodInfo matchAction = AccessTools.Method(typeof(BlockData), "_CheckMatchAction", new Type[] { typeof(BlockData) });
				if (slider == null || preview == null || offset == null || rotate == null || mirror == null || conversion == null
					|| copyAction == null || matchAction == null)
					throw new MissingMethodException("Coupler slider/preview/offset/rotation methods");

				harmony = new Harmony(PatchId);
				HarmonyMethod transpiler = new HarmonyMethod(AccessTools.Method(typeof(CouplerRotation), "SliderTranspiler"));
				transpiler.priority = Priority.Last;
				harmony.Patch(slider, new HarmonyMethod(AccessTools.Method(typeof(CouplerRotation), "SliderPrefix")), null, transpiler,
					new HarmonyMethod(AccessTools.Method(typeof(CouplerRotation), "FinishSliderEdit")));
				LogStartupTiming(startup, "Build.BKACIJDGAPP", ref checkpoint);
				bool sharedBuildHooks = StartupHarmonyBatch.TryRegister();
				LogStartupTiming(startup, sharedBuildHooks ? "shared-build-hooks" : "shared-build-hooks.fallback", ref checkpoint);
				if (!sharedBuildHooks)
					harmony.Patch(preview, null, new HarmonyMethod(AccessTools.Method(typeof(CouplerRotation), "PreviewPostfix")), null, null);
				LogStartupTiming(startup, sharedBuildHooks ? "Build.JOHIPODALCN.shared-reuse" : "Build.JOHIPODALCN", ref checkpoint);
				harmony.Patch(offset, null, new HarmonyMethod(AccessTools.Method(typeof(CouplerRotation), "OffsetPostfix")), null, null);
				LogStartupTiming(startup, "BlockData.GetCouplerOffset", ref checkpoint);
				harmony.Patch(rotate, new HarmonyMethod(AccessTools.Method(typeof(CouplerRotation), "RotateParametersPrefix")),
					new HarmonyMethod(AccessTools.Method(typeof(CouplerRotation), "RotateParametersPostfix")), null, null);
				LogStartupTiming(startup, "BlockData.RotCouplerParam", ref checkpoint);
				harmony.Patch(mirror, null, new HarmonyMethod(AccessTools.Method(typeof(CouplerRotation), "MirrorParametersPostfix")), null, null);
				LogStartupTiming(startup, "BlockData.FlipParam", ref checkpoint);
				harmony.Patch(conversion, new HarmonyMethod(AccessTools.Method(typeof(CouplerRotation), "RotationVectorPrefix")), null, null, null);
				LogStartupTiming(startup, "BDLEJBBJJOI.GKCKPLGPDFK", ref checkpoint);
				harmony.Patch(copyAction, null, new HarmonyMethod(AccessTools.Method(typeof(CouplerRotation), "CopyActionPostfix")), null, null);
				LogStartupTiming(startup, "BlockData.CopyAction", ref checkpoint);
				harmony.Patch(matchAction, null, new HarmonyMethod(AccessTools.Method(typeof(CouplerRotation), "MatchActionPostfix")), null, null);
				LogStartupTiming(startup, "BlockData._CheckMatchAction", ref checkpoint);
				CouplerRotationNetwork.Register(harmony);
				LogStartupTiming(startup, "network-hooks", ref checkpoint);
				CouplerRotationUi.Register(harmony);
				LogStartupTiming(startup, "ui-hooks", ref checkpoint);
				CouplerRotationCopy.Register(harmony);
				LogStartupTiming(startup, "copy-hooks", ref checkpoint);
				CouplerRotationPreview.Register(harmony);
				LogStartupTiming(startup, "preview-hooks", ref checkpoint);
				registered = true;
				VerifyRuntimeMath();
				LogStartupTiming(startup, "math-check", ref checkpoint);
				CouplerRotationPreview.VerifyRuntimeMath();
				LogStartupTiming(startup, "preview-check", ref checkpoint);
				Log("REGISTERED version=10 phase=SteamAPI.Init target=Build.BKACIJDGAPP resetWrites=6 preview=Build.JOHIPODALCN enabled="
					+ Enabled + " orders=6 default=YXZ physics=always network=XYZ-v1/v2 range=-180..180 disabled=profile-switch copy=block/settings previewAxes=ordered-stages");
			}
			catch (Exception error)
			{
				registered = false;
				Log("REGISTER_FAILED type=" + error.GetType().Name + " message=" + error.Message);
				try { CouplerRotationNetwork.UnregisterShared(); }
				catch (Exception rollbackError) { Log("SHARED_ROLLBACK_FAILED type=" + rollbackError.GetType().Name + " message=" + rollbackError.Message); }
				try { if (harmony != null) harmony.UnpatchAll(PatchId); }
				catch (Exception rollbackError) { Log("ROLLBACK_FAILED type=" + rollbackError.GetType().Name + " message=" + rollbackError.Message); }
				harmony = null;
			}
		}

		internal static void DisableAfterNetworkFailure(Exception error)
		{
			registered = false;
			Log("NETWORK_DISABLED_AFTER_ROLLBACK_FAILURE type=" + error.GetType().Name + " message=" + error.Message);
			try { CouplerRotationNetwork.UnregisterShared(); }
			catch (Exception rollbackError) { Log("NETWORK_SHARED_DISABLE_FAILED type=" + rollbackError.GetType().Name + " message=" + rollbackError.Message); }
			try { if (harmony != null) harmony.UnpatchAll(PatchId); }
			catch (Exception rollbackError) { Log("NETWORK_DISABLE_FAILED type=" + rollbackError.GetType().Name + " message=" + rollbackError.Message); }
			harmony = null;
		}

		private static void LogStartupTiming(Stopwatch timer, string phase, ref long checkpoint)
		{
			long elapsed = timer.ElapsedMilliseconds;
			Log("STARTUP_TIMING phase=" + phase + " stepMs=" + (elapsed - checkpoint) + " totalMs=" + elapsed);
			checkpoint = elapsed;
		}

		internal static void SetEnabled(bool enabled)
		{
			settingsIngame settings = MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68;
			if (settings == null)
				return;

			settings.freeCouplerRot = enabled;
			settings.UUiRNMwxRbfk_Fs4cDErRoM();
			lastLoggedBlock = null;
			lastReadOnlyBlock = null;
			CouplerRotationUi.OnSettingChanged();
			Log("SETTING enabled=" + enabled + " registered=" + registered + " profiles=preserved restoreFree=local-editor");
		}

		internal static bool IsRotationReadOnly(BlockData block)
		{
			if (!registered || block == null || block.type != BlockData.AAHMDBHDCDK.Coupler)
				return false;
			return Enabled ? CouplerRotationProfiles.IsVanilla(block)
				: CouplerRotationProfiles.HasFreeValues(block) && !CouplerRotationProfiles.IsVanilla(block);
		}

		private static bool BeginRotationEdit(BlockData block, int parameter, out bool entered)
		{
			entered = false;
			if (!registered || block == null || block.type != BlockData.AAHMDBHDCDK.Coupler || parameter < 3 || parameter > 5)
				return true;
			if (nativeRotationEdits == 0 && IsRotationReadOnly(block))
				return false;
			if (Enabled)
				return true;
			nativeRotationEdits++;
			entered = true;
			return true;
		}

		private static bool SliderPrefix(string DPGKEOAGONA, GameObject NGLBLAGMBLN, BlockData ___LBBOFMGMMFF,
			int ___LIOOKHCGPIO, bool ___OIAOGDEPDCO, out RotationEdit __state)
		{
			__state = default(RotationEdit);
			if (DPGKEOAGONA != "ParamC" || NGLBLAGMBLN == null)
				return true;
			int parameter = SceneMan.GetWidgetID(NGLBLAGMBLN);
			if (___OIAOGDEPDCO && ___LIOOKHCGPIO > 0)
				return true;
			if (BeginRotationEdit(___LBBOFMGMMFF, parameter, out __state.Entered))
			{
				if (Enabled && ___LBBOFMGMMFF != null && ___LBBOFMGMMFF.type == BlockData.AAHMDBHDCDK.Coupler
					&& parameter >= 3 && parameter <= 5 && ___LIOOKHCGPIO == 0 && !___OIAOGDEPDCO
					&& !CouplerRotationProfiles.HasArchive(___LBBOFMGMMFF))
				{
					__state.RememberFree = true;
					__state.Before = ___LBBOFMGMMFF;
					__state.Parameter = parameter;
					__state.PreviousValue = ___LBBOFMGMMFF.actionParam[parameter];
					__state.Vanilla = CouplerRotationProfiles.InitialVanilla(___LBBOFMGMMFF);
				}
				return true;
			}
			if (!restoringReadOnlySlider)
			{
				try
				{
					restoringReadOnlySlider = true;
					Slider slider = NGLBLAGMBLN.GetComponent<Slider>();
					if (slider != null)
						slider.value = ___LBBOFMGMMFF.actionParam[parameter];
				}
				finally
				{
					restoringReadOnlySlider = false;
				}
				int now = Environment.TickCount;
				if (!ReferenceEquals(lastReadOnlyBlock, ___LBBOFMGMMFF) || unchecked(now - lastReadOnlyLogTick) >= 500)
				{
					lastReadOnlyBlock = ___LBBOFMGMMFF;
					lastReadOnlyLogTick = now;
					Log("READ_ONLY order=" + CouplerRotationOrder.Name(CouplerRotationOrder.Read(___LBBOFMGMMFF))
						+ " xyz=" + ___LBBOFMGMMFF.actionParam[3] + "," + ___LBBOFMGMMFF.actionParam[4] + "," + ___LBBOFMGMMFF.actionParam[5]
						+ " rejectedAxis=" + (char)('X' + parameter - 3));
				}
			}
			return false;
		}

		private static Exception FinishSliderEdit(Exception __exception, RotationEdit __state, BlockData ___LBBOFMGMMFF, HIPBCCKFFAG ___FFJDGJFPLAD)
		{
			SliderFinalizer(__exception, __state.Entered);
			if (__exception == null && __state.RememberFree && ___LBBOFMGMMFF != null
				&& ___LBBOFMGMMFF.type == __state.Before.type && ___LBBOFMGMMFF.x == __state.Before.x
				&& ___LBBOFMGMMFF.y == __state.Before.y && ___LBBOFMGMMFF.z == __state.Before.z
				&& ___LBBOFMGMMFF.actionParam[__state.Parameter] != __state.PreviousValue)
			{
				try
				{
					if (CouplerRotationProfiles.RememberFreeEdit(___LBBOFMGMMFF, __state.Vanilla))
						CouplerRotationUi.PersistPreviewProperties(___FFJDGJFPLAD, ___LBBOFMGMMFF);
				}
				catch (Exception error)
				{
					Log("PROFILE_CAPTURE_FAILED type=" + error.GetType().Name + " message=" + error.Message);
				}
			}
			return __exception;
		}

		private static Exception SliderFinalizer(Exception __exception, bool __state)
		{
			if (__state)
				nativeRotationEdits--;
			return __exception;
		}

		private static IEnumerable<CodeInstruction> SliderTranspiler(IEnumerable<CodeInstruction> instructions)
		{
			List<CodeInstruction> codes = new List<CodeInstruction>();
			foreach (CodeInstruction instruction in instructions)
				codes.Add(new CodeInstruction(instruction));

			List<CodeInstruction> anchors = codes.FindAll(instruction => instruction.opcode == OpCodes.Ldstr
				&& Equals(instruction.operand, "ParamC3"));
			if (anchors.Count != 1)
				throw new InvalidOperationException("Expected one Coupler reset block");

			int start = codes.IndexOf(anchors[0]) - 1;
			if (start < 1 || codes[start].opcode != OpCodes.Ldarg_0
				|| (codes[start - 1].opcode != OpCodes.Brtrue && codes[start - 1].opcode != OpCodes.Brtrue_S)
				|| !(codes[start - 1].operand is Label))
			{
				throw new InvalidOperationException("Coupler reset entry changed");
			}

			Label exitLabel = (Label)codes[start - 1].operand;
			int end = codes.FindIndex(start + 1, instruction => instruction.labels.Contains(exitLabel));
			if (end <= start)
				throw new InvalidOperationException("Coupler reset exit missing");

			MethodInfo setValue = AccessTools.PropertySetter(typeof(Slider), "value");
			int resetWrites = 0;
			int otherSliders = 0;
			for (int position = start; position < end; position++)
			{
				CodeInstruction instruction = codes[position];
				if (instruction.opcode == OpCodes.Ldstr
					&& (Equals(instruction.operand, "ParamC4") || Equals(instruction.operand, "ParamC5")))
				{
					otherSliders++;
				}
				if (instruction.opcode == OpCodes.Callvirt && Equals(instruction.operand, setValue)
					&& position > start && codes[position - 1].opcode == OpCodes.Ldc_R4
					&& Equals(codes[position - 1].operand, 0f))
				{
					resetWrites++;
				}
			}
			if (resetWrites != 6 || otherSliders != 2)
				throw new InvalidOperationException("Coupler reset shape changed: writes=" + resetWrites + " sliders=" + otherSliders);

			FieldInfo selectedBlock = AccessTools.Field(typeof(Build), "LBBOFMGMMFF");
			if (selectedBlock == null || selectedBlock.FieldType != typeof(BlockData))
				throw new MissingFieldException("Build.LBBOFMGMMFF");

			CodeInstruction entry = new CodeInstruction(OpCodes.Ldarg_0);
			entry.labels.AddRange(codes[start].labels);
			entry.blocks.AddRange(codes[start].blocks);
			codes[start].labels.Clear();
			codes[start].blocks.Clear();
			codes.InsertRange(start, new CodeInstruction[]
			{
				entry,
				new CodeInstruction(OpCodes.Ldfld, selectedBlock),
				new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(CouplerRotation), "PreserveOtherAxes")),
				new CodeInstruction(OpCodes.Brtrue, exitLabel)
			});
			return codes;
		}

		private static bool PreserveOtherAxes(BlockData blockData)
		{
			if (!Enabled || blockData == null || blockData.type != BlockData.AAHMDBHDCDK.Coupler || CouplerRotationProfiles.IsVanilla(blockData))
				return false;

			int activeAxes = 0;
			for (int parameter = 3; parameter <= 5; parameter++)
				if (blockData.actionParam[parameter] != 0)
					activeAxes++;

			int now = Environment.TickCount;
			if (activeAxes > 1 && (lastLoggedBlock != blockData || unchecked(now - lastLogTick) >= 500))
			{
				lastLoggedBlock = blockData;
				lastLogTick = now;
				Log("APPLIED block=" + blockData.x + "," + blockData.y + "," + blockData.z
					+ " rotation=" + blockData.actionParam[3] + "," + blockData.actionParam[4] + "," + blockData.actionParam[5]
					+ " order=" + CouplerRotationOrder.Name(CouplerRotationOrder.Read(blockData)));
			}
			return true;
		}

		private static void OffsetPostfix(BlockData __instance, ref Vector3 aang, ref Vector3 pos)
		{
			if (!registered || __instance.type != BlockData.AAHMDBHDCDK.Coupler)
				return;
			Vector3 angles = SetupPrecisionData.Angles(__instance);
			int activeAxes = (angles.x != 0 ? 1 : 0) + (angles.y != 0 ? 1 : 0) + (angles.z != 0 ? 1 : 0);
			if (activeAxes < 2)
				return;

			int order = CouplerRotationOrder.Read(__instance);
			Quaternion rotation = CouplerRotationMath.FromEuler(angles, order);
			Vector3 rotationVector = CouplerRotationMath.ToRotationVector(rotation);
			Vector3 blockPosition = __instance.GetPos();
			Matrix4x4 originalRotation = BDLEJBBJJOI.IGMEGICHKOE(aang * (float)Math.PI / 180f);
			pos += originalRotation.MultiplyPoint(blockPosition) - rotation * blockPosition;
			aang = rotationVector;

			int now = Environment.TickCount;
			if (!ReferenceEquals(lastOffsetBlock, __instance) || unchecked(now - lastOffsetLogTick) >= 500)
			{
				lastOffsetBlock = __instance;
				lastOffsetLogTick = now;
				Log("OFFSET version=10 editorEnabled=" + Enabled + " euler=" + angles.ToString("F1")
					+ " order=" + CouplerRotationOrder.Name(order)
					+ " nativeVector=" + rotationVector.ToString("F4"));
			}
		}

		private static bool RotationVectorPrefix(Vector3 __0, ref Vector3 __result)
		{
			if (!registered || !CouplerRotationMath.IsMixed(__0))
				return true;
			__result = CouplerRotationMath.ToBoxEuler(CouplerRotationMath.FromRotationVector(__0));
			return false;
		}

		private static void VerifyRuntimeMath()
		{
			float[] values = { -180f, -135f, -120f, -91f, -90f, -80f, -45f, 0f, 5f, 30f, 45f, 90f, 180f };
			Vector3[] basis = { Vector3.right, Vector3.up, Vector3.forward };
			float maxError = 0f;
			int cases = 0;
			for (int order = 0; order < CouplerRotationOrder.Count; order++)
			foreach (float angleX in values)
			foreach (float angleY in values)
			foreach (float angleZ in values)
			{
				Vector3 angles = new Vector3(angleX, angleY, angleZ);
				Quaternion expected = Quaternion.identity;
				for (int row = 0; row < 3; row++)
				{
					int axis = CouplerRotationOrder.Axis(order, row);
					Vector3 step = Vector3.zero;
					step[axis] = -angles[axis];
					expected = expected * Quaternion.Euler(step);
				}
				Quaternion actual = CouplerRotationMath.FromEuler(angles, order);
				Vector3 rotationVector = CouplerRotationMath.ToRotationVector(actual);
				Quaternion rendered = BDLEJBBJJOI.INECOALCJIE(BDLEJBBJJOI.GKCKPLGPDFK(rotationVector));
				foreach (Vector3 axis in basis)
				{
					maxError = Math.Max(maxError, (expected * axis - actual * axis).magnitude);
					maxError = Math.Max(maxError, (expected * axis - rendered * axis).magnitude);
				}
				cases++;
			}
			if (float.IsNaN(maxError) || maxError > 0.00002f)
				throw new InvalidOperationException("Coupler Unity rotation check failed: error=" + maxError);
			Log("UNITY_MATH_CHECK version=10 orders=6 cases=" + cases + " maxDirectionError="
				+ maxError.ToString("G6", System.Globalization.CultureInfo.InvariantCulture) + " result=PASS");
		}

		private static void RotateParametersPrefix(BlockData __instance, out Quaternion __state)
		{
			__state = default(Quaternion);
			if (registered && __instance.type == BlockData.AAHMDBHDCDK.Coupler
				&& (!CouplerRotationProfiles.Current(__instance).Vanilla || SetupPrecisionData.HasAny(__instance)))
			{
				__state = CouplerRotationMath.FromEuler(SetupPrecisionData.Angles(__instance), CouplerRotationOrder.Read(__instance));
			}
		}

		private static void RotateParametersPostfix(BlockData __instance, Vector3 axis, Quaternion __state)
		{
			if (registered)
				CouplerRotationProfiles.RotateSaved(__instance, axis);
			if (__state.x == 0f && __state.y == 0f && __state.z == 0f && __state.w == 0f)
				return;
			Quaternion basis = CouplerRotationMath.FromRotationVector(axis * 90f);
			Quaternion inverseBasis = new Quaternion(-basis.x, -basis.y, -basis.z, basis.w);
			int order = CouplerRotationOrder.Read(__instance);
			Vector3 angles = CouplerRotationMath.ToEuler(basis * __state * inverseBasis, order);
			int[] parameters = __instance.actionParam;
			SetupPrecisionData.SetAngles(__instance, angles);
			Log("ROTATE_PARAMETERS version=10 order=" + CouplerRotationOrder.Name(order)
				+ " euler=" + parameters[3] + "," + parameters[4] + "," + parameters[5]);
		}

		private static void MirrorParametersPostfix(BlockData __instance)
		{
			if (registered)
				CouplerRotationProfiles.MirrorSaved(__instance);
		}

		private static void CopyActionPostfix(BlockData __instance, BlockData src)
		{
			if (registered && __instance.type == BlockData.AAHMDBHDCDK.Coupler && src != null && src.type == __instance.type)
			{
				if (CouplerRotationOrder.Set(__instance, CouplerRotationOrder.Read(src)))
					Log("ORDER_COPIED order=" + CouplerRotationOrder.Name(CouplerRotationOrder.Read(src)));
				if (CouplerRotationProfiles.Copy(__instance, src))
					Log("PROFILES_COPIED active=" + (CouplerRotationProfiles.IsVanilla(src) ? "Vanilla" : "Free"));
			}
		}

		private static void MatchActionPostfix(BlockData __instance, BlockData other, ref bool __result)
		{
			if (registered && __result && __instance.type == BlockData.AAHMDBHDCDK.Coupler && other != null && other.type == __instance.type)
				__result = CouplerRotationOrder.Read(__instance) == CouplerRotationOrder.Read(other) && CouplerRotationProfiles.Matches(__instance, other);
		}

		internal static void PreviewPostfix(BlockData ___LBBOFMGMMFF, GameObject ___FBGCMCPNDAH, GameObject ___DNFHFEBGNPM)
		{
			try
			{
				if (!registered || ___LBBOFMGMMFF == null || ___LBBOFMGMMFF.type != BlockData.AAHMDBHDCDK.Coupler
					|| ___FBGCMCPNDAH == null || ___DNFHFEBGNPM == null || !___DNFHFEBGNPM.activeSelf)
				{
					return;
				}

				Vector3 rotation = SetupPrecisionData.Angles(___LBBOFMGMMFF);
				___DNFHFEBGNPM.transform.rotation = CouplerRotationMath.FromEuler(rotation, CouplerRotationOrder.Read(___LBBOFMGMMFF));
			}
			catch (Exception error)
			{
				string failure = error.GetType().Name + ":" + error.Message;
				if (failure != lastPreviewFailure)
				{
					lastPreviewFailure = failure;
					Log("PREVIEW_FAILED " + failure);
				}
			}
		}

		internal static bool Enabled
		{
			get { return registered && MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68 != null && MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.freeCouplerRot; }
		}

		internal static bool IsRegistered
		{
			get { return registered; }
		}

		internal static void Log(string message)
		{
			try { global::mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng("[COUPLER-ROTATION] " + message); }
			catch (Exception) { }
		}
	}
}
