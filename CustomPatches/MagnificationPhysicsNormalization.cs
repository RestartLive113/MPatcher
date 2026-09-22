using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using McnCraft;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
	// MachineCraft keeps gameplay mass constant when Magnification changes. Only
	// world-space lengths are converted here. Rigidbody inertia, joint drives and
	// projection tolerances stay native: reducing those values below the old
	// PhysX solver's practical tolerances made 5..10% machines wobble or lock up.
	internal static class MagnificationPhysicsNormalization
	{
		private static readonly MethodInfo ScaleWheelLengthMethod = AccessTools.Method(typeof(MagnificationPhysicsNormalization), "ScaleWheelLength");
		private static readonly MethodInfo UnscaleWheelLengthMethod = AccessTools.Method(typeof(MagnificationPhysicsNormalization), "UnscaleWheelLength");
		private static readonly MethodInfo ScaleMachineLengthMethod = AccessTools.Method(typeof(MagnificationPhysicsNormalization), "ScaleMachineLength");
		private static readonly MethodInfo ScaleBodyLengthMethod = AccessTools.Method(typeof(MagnificationPhysicsNormalization), "ScaleBodyLength");
		private static readonly MethodInfo UnscaleBodyLengthMethod = AccessTools.Method(typeof(MagnificationPhysicsNormalization), "UnscaleBodyLength");
		private static readonly MethodInfo ScaleShaftLengthMethod = AccessTools.Method(typeof(MagnificationPhysicsNormalization), "ScaleShaftLength");
		private static readonly MethodInfo ScaleShaftVectorMethod = AccessTools.Method(typeof(MagnificationPhysicsNormalization), "ScaleShaftVector");
		private static readonly MethodInfo VectorMultiplyMethod = AccessTools.Method(typeof(Vector3), "op_Multiply", new[] { typeof(Vector3), typeof(float) });
		private static readonly MethodInfo VectorAddMethod = AccessTools.Method(typeof(Vector3), "op_Addition", new[] { typeof(Vector3), typeof(Vector3) });
		private static readonly MethodInfo FloatMinMethod = AccessTools.Method(typeof(Mathf), "Min", new[] { typeof(float), typeof(float) });
		private static readonly MethodInfo RaycastMethod = AccessTools.Method(typeof(Physics), "RaycastNonAlloc",
			new[] { typeof(Vector3), typeof(Vector3), typeof(RaycastHit[]), typeof(float), typeof(int) });

		internal static void Register(Harmony harmony)
		{
			Patch(harmony, typeof(WheelController), "FixedUpdate", Type.EmptyTypes, "WheelFixedUpdateTranspiler", null);
			Patch(harmony, typeof(MachineController), "FixedUpdate", Type.EmptyTypes, "MachineFixedUpdateTranspiler", null);
			Patch(harmony, typeof(BodyController), "FixedUpdate", Type.EmptyTypes, "BodyFixedUpdateTranspiler", null);
			Patch(harmony, typeof(ShaftController), "FixedUpdate", Type.EmptyTypes, "ShaftFixedUpdateTranspiler", null);
			MagnificationDownscale.Log("PHYSICS_HOOKS model=native-solver-stability mass=native inertia=native jointAngular=native projection=native wheelContact=normalized waterDepth=normalized proximity=scale");
		}

		private static void Patch(Harmony harmony, Type type, string name, Type[] args, string transpiler, string postfix)
		{
			MethodInfo target = AccessTools.Method(type, name, args);
			if (target == null) throw new MissingMethodException(type.FullName, name);
			HarmonyMethod transpilerHook = transpiler == null ? null : new HarmonyMethod(AccessTools.Method(typeof(MagnificationPhysicsNormalization), transpiler));
			HarmonyMethod postfixHook = postfix == null ? null : new HarmonyMethod(AccessTools.Method(typeof(MagnificationPhysicsNormalization), postfix));
			harmony.Patch(target, null, postfixHook, transpilerHook, null);
			MagnificationDownscale.Log("HOOK target=" + type.FullName + "." + name + " physics=normalized");
		}

		internal static float ScaleFactor(float scale)
		{
			return !float.IsNaN(scale) && !float.IsInfinity(scale)
				&& scale >= MagnificationDownscale.MinimumPercent * 0.01f
				&& scale <= MagnificationDownscale.MaximumPercent * 0.01f ? scale : 1f;
		}

		internal static float MachineScale(MachineController machine)
		{
			return ReferenceEquals(machine, null) ? 1f : ScaleForMachine(machine.DJCDFLDHPHK, machine.CHDEBOIIMCI);
		}
		internal static float ScaleForMachine(float scale, bool boss) { return boss ? 1f : ScaleFactor(scale); }

		internal static float ScaleLength(float value, float scale) { return value * ScaleFactor(scale); }
		internal static float UnscaleLength(float value, float scale) { return value / ScaleFactor(scale); }
		internal static float ScaleAngularCoefficient(float value, float scale) { return value; }
		internal static Vector3 ScaleInertia(Vector3 value, float scale) { return value; }

		internal static void NormalizeMachine(MachineController machine)
		{
			float scale = MachineScale(machine);
			if (scale == 1f) return;
			MagnificationDownscale.Log("PHYSICS_MACHINE scale=" + scale.ToString("R", CultureInfo.InvariantCulture)
				+ " mass=native inertia=native joints=native");
		}

		internal static IEnumerable<CodeInstruction> WheelFixedUpdateTranspiler(IEnumerable<CodeInstruction> instructions)
		{
			List<CodeInstruction> codes = MagnificationDownscale.Copy(instructions);
			List<KeyValuePair<int, MethodInfo>> conversions = new List<KeyValuePair<int, MethodInfo>>();
			int radii = 0, buoyancyCap = 0, raycastClearance = 0, contactGap = 0;
			for (int i = 1; i + 2 < codes.Count; i++)
			{
				if (codes[i - 1].opcode == OpCodes.Ldarg_0
					&& MagnificationDownscale.Field(codes[i], OpCodes.Ldfld, typeof(WheelController), "JHADHFJDPFD")
					&& MagnificationDownscale.Float(codes[i + 1], 0.5f) && codes[i + 2].opcode == OpCodes.Mul)
				{
					conversions.Add(new KeyValuePair<int, MethodInfo>(i + 3, ScaleWheelLengthMethod));
					radii++;
				}
				if (MagnificationDownscale.Field(codes[i], OpCodes.Ldfld, typeof(WheelController), "JHADHFJDPFD")
					&& codes[i + 1].opcode == OpCodes.Call && Equals(codes[i + 1].operand, FloatMinMethod))
				{
					conversions.Add(new KeyValuePair<int, MethodInfo>(i + 1, ScaleWheelLengthMethod));
					buoyancyCap++;
				}
				if (MagnificationDownscale.Float(codes[i], 1f) && codes[i + 1].opcode == OpCodes.Add
					&& i + 3 < codes.Count && codes[i + 3].opcode == OpCodes.Call && Equals(codes[i + 3].operand, RaycastMethod))
				{
					conversions.Add(new KeyValuePair<int, MethodInfo>(i + 1, ScaleWheelLengthMethod));
					raycastClearance++;
				}
				MethodInfo distance = i >= 2 ? codes[i - 2].operand as MethodInfo : null;
				if (codes[i].opcode == OpCodes.Sub && IsStoreLocal(codes[i + 1])
					&& distance != null && distance.DeclaringType == typeof(RaycastHit) && distance.Name == "get_distance")
				{
					conversions.Add(new KeyValuePair<int, MethodInfo>(i + 1, UnscaleWheelLengthMethod));
					contactGap++;
				}
			}
			MagnificationDownscale.Require(radii == 2 && buoyancyCap == 1 && raycastClearance == 1 && contactGap == 1,
				"wheel radii/buoyancy cap/raycast clearance/contact gap", radii + buoyancyCap + raycastClearance + contactGap);
			InsertConversions(codes, conversions);
			return codes;
		}

		private static float ScaleWheelLength(float value, WheelController wheel)
		{
			return value * MachineScale(wheel.FPEBEEJGFPI);
		}
		private static float UnscaleWheelLength(float value, WheelController wheel)
		{
			return value / MachineScale(wheel.FPEBEEJGFPI);
		}

		internal static IEnumerable<CodeInstruction> MachineFixedUpdateTranspiler(IEnumerable<CodeInstruction> instructions)
		{
			List<CodeInstruction> codes = MagnificationDownscale.Copy(instructions);
			List<KeyValuePair<int, MethodInfo>> conversions = new List<KeyValuePair<int, MethodInfo>>();
			int proximity = 0, forceArms = 0;
			for (int i = 1; i + 3 < codes.Count; i++)
			{
				if ((MagnificationDownscale.Float(codes[i], 5f) || MagnificationDownscale.Float(codes[i], 15f))
					&& ((MagnificationDownscale.Field(codes[i - 1], OpCodes.Ldfld, typeof(MachineController), "HNHGELLPCGG")
						&& codes[i + 1].opcode == OpCodes.Add)
						|| (Call(codes[i - 1], typeof(MachineController), "POGEEJEANGD")
							&& (codes[i + 1].opcode == OpCodes.Bge || codes[i + 1].opcode == OpCodes.Bge_Un))))
				{
					conversions.Add(new KeyValuePair<int, MethodInfo>(i + 1, ScaleMachineLengthMethod));
					proximity++;
				}
				if ((MagnificationDownscale.Float(codes[i], 50f) || MagnificationDownscale.Float(codes[i], -50f)
						|| MagnificationDownscale.Float(codes[i], 100f))
					&& IsCall(codes[i + 1], VectorMultiplyMethod) && IsCall(codes[i + 2], VectorAddMethod))
				{
					conversions.Add(new KeyValuePair<int, MethodInfo>(i + 1, ScaleMachineLengthMethod));
					forceArms++;
				}
			}
			MagnificationDownscale.Require(proximity == 4 && forceArms == 4,
				"machine proximity/force-arm lengths", proximity + forceArms);
			InsertConversions(codes, conversions);
			return codes;
		}

		private static float ScaleMachineLength(float value, MachineController machine) { return value * MachineScale(machine); }
		internal static IEnumerable<CodeInstruction> BodyFixedUpdateTranspiler(IEnumerable<CodeInstruction> instructions)
		{
			List<CodeInstruction> codes = MagnificationDownscale.Copy(instructions);
			List<KeyValuePair<int, MethodInfo>> conversions = new List<KeyValuePair<int, MethodInfo>>();
			int waterThresholds = 0, waterDepth = 0;
			for (int i = 0; i < codes.Count; i++)
			{
				if ((MagnificationDownscale.Float(codes[i], 0.5f)
						&& i + 1 < codes.Count && codes[i + 1].opcode == OpCodes.Bge_Un)
					|| (MagnificationDownscale.Float(codes[i], -0.5f)
						&& i + 1 < codes.Count && codes[i + 1].opcode == OpCodes.Ble_Un))
				{
					conversions.Add(new KeyValuePair<int, MethodInfo>(i + 1, ScaleBodyLengthMethod));
					waterThresholds++;
				}
				if (MagnificationDownscale.Float(codes[i], 0.5f) && i + 3 < codes.Count && codes[i + 3].opcode == OpCodes.Sub)
				{
					conversions.Add(new KeyValuePair<int, MethodInfo>(i + 1, ScaleBodyLengthMethod));
					conversions.Add(new KeyValuePair<int, MethodInfo>(i + 4, UnscaleBodyLengthMethod));
					waterThresholds++;
					waterDepth++;
				}
			}
			MagnificationDownscale.Require(waterThresholds == 3 && waterDepth == 1,
				"body water thresholds/depth", waterThresholds + waterDepth);
			InsertConversions(codes, conversions);
			return codes;
		}

		private static float ScaleBodyLength(float value, BodyController body) { return value * MachineScale(body.CIPOPAGDJDE); }
		private static float UnscaleBodyLength(float value, BodyController body) { return value / MachineScale(body.CIPOPAGDJDE); }

		internal static IEnumerable<CodeInstruction> ShaftFixedUpdateTranspiler(IEnumerable<CodeInstruction> instructions)
		{
			List<CodeInstruction> codes = MagnificationDownscale.Copy(instructions);
			List<KeyValuePair<int, MethodInfo>> conversions = new List<KeyValuePair<int, MethodInfo>>();
			int arm = 0, velocityPoint = 0;
			for (int i = 0; i + 1 < codes.Count; i++)
			{
				if (i >= 4 && codes[i].opcode == OpCodes.Add && IsCall(codes[i + 1], VectorMultiplyMethod)
					&& MagnificationDownscale.Field(codes[i - 4], OpCodes.Ldfld, typeof(ShaftController), "DAHHHCBADLJ")
					&& MagnificationDownscale.Float(codes[i - 3], 0.15f) && codes[i - 2].opcode == OpCodes.Mul
					&& MagnificationDownscale.Float(codes[i - 1], 2f))
				{
					conversions.Add(new KeyValuePair<int, MethodInfo>(i + 1, ScaleShaftLengthMethod));
					arm++;
				}
				if (Call(codes[i], typeof(Transform), "get_up") && codes[i + 1].opcode == OpCodes.Call
					&& codes[i + 1].operand is MethodInfo && ((MethodInfo)codes[i + 1].operand).DeclaringType == typeof(Vector3)
					&& ((MethodInfo)codes[i + 1].operand).Name == "op_Subtraction")
				{
					conversions.Add(new KeyValuePair<int, MethodInfo>(i + 1, ScaleShaftVectorMethod));
					velocityPoint++;
				}
			}
			MagnificationDownscale.Require(arm == 1 && velocityPoint == 1, "Shaft force arm and velocity point", arm + velocityPoint);
			InsertConversions(codes, conversions);
			return codes;
		}

		private static float ScaleShaftLength(float value, ShaftController shaft) { return value * MachineScale(shaft.FPEBEEJGFPI); }
		private static Vector3 ScaleShaftVector(Vector3 value, ShaftController shaft) { return value * MachineScale(shaft.FPEBEEJGFPI); }

		private static void InsertConversion(List<CodeInstruction> codes, int index, MethodInfo method)
		{
			CodeInstruction argument = new CodeInstruction(OpCodes.Ldarg_0);
			if (index < codes.Count)
			{
				argument.labels.AddRange(codes[index].labels);
				codes[index].labels.Clear();
			}
			codes.InsertRange(index, new[] { argument, new CodeInstruction(OpCodes.Call, method) });
		}

		private static void InsertConversions(List<CodeInstruction> codes, List<KeyValuePair<int, MethodInfo>> conversions)
		{
			conversions.Sort((left, right) => right.Key.CompareTo(left.Key));
			for (int i = 0; i < conversions.Count; i++) InsertConversion(codes, conversions[i].Key, conversions[i].Value);
		}

		private static bool IsCall(CodeInstruction code, MethodInfo method)
		{
			return (code.opcode == OpCodes.Call || code.opcode == OpCodes.Callvirt) && Equals(code.operand, method);
		}

		private static bool Call(CodeInstruction code, Type type, string name)
		{
			MethodInfo method = code.operand as MethodInfo;
			return (code.opcode == OpCodes.Call || code.opcode == OpCodes.Callvirt)
				&& method != null && method.DeclaringType == type && method.Name == name;
		}

		private static bool IsStoreLocal(CodeInstruction code)
		{
			return code.opcode == OpCodes.Stloc || code.opcode == OpCodes.Stloc_S || code.opcode == OpCodes.Stloc_0
				|| code.opcode == OpCodes.Stloc_1 || code.opcode == OpCodes.Stloc_2 || code.opcode == OpCodes.Stloc_3;
		}

	}
}
