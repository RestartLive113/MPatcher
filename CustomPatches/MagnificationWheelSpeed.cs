using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using McnCraft;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
	// Opt-in research: compare the native constant-world-speed target (1/s RPM)
	// with constant RPM and size-dependent tread speed, requested by the user.
	internal static class MagnificationWheelSpeed
	{
		internal static float AngularLimit(float native, float scale) { return native / MagnificationPhysicsNormalization.ScaleFactor(scale); }
		private static float Limit(float value, WheelController wheel)
		{
			return MagnificationCraftProbe.Model >= 11 && MagnificationCraftProbe.Model < 17 ? AngularLimit(value, MagnificationPhysicsNormalization.MachineScale(wheel.FPEBEEJGFPI)) : value;
		}
		internal static void Register(Harmony harmony)
		{
			foreach (string name in new[] { "Start", "MakeTire" })
				harmony.Patch(AccessTools.DeclaredMethod(typeof(WheelController), name), null, null,
					new HarmonyMethod(AccessTools.Method(typeof(MagnificationWheelSpeed), "Transpiler")), null);
			harmony.Patch(AccessTools.DeclaredMethod(typeof(WheelController), "Start"), null, null,
				new HarmonyMethod(AccessTools.Method(typeof(MagnificationWheelSpeed), "TargetTranspiler")), null);
			MagnificationDownscale.Log("WHEEL_SPEED_HOOKS scope=research model11-16=world-speed model17=constant-rpm+native-angular-cap");
		}
		internal static float TargetDivisor(float scale, bool boss, int model) { return model >= 17 && !boss ? 1f : scale; }
		private static float TargetScale(MachineController machine) { return TargetDivisor(machine.DJCDFLDHPHK, machine.CHDEBOIIMCI, MagnificationCraftProbe.Model); }
		internal static IEnumerable<CodeInstruction> TargetTranspiler(IEnumerable<CodeInstruction> instructions)
		{
			List<CodeInstruction> codes = MagnificationDownscale.Copy(instructions);
			int found = 0;
			for (int i = 0; i + 3 < codes.Count; i++)
			{
				if (!MagnificationDownscale.Field(codes[i], OpCodes.Ldfld, typeof(MachineController), "DJCDFLDHPHK")
					|| codes[i + 1].opcode != OpCodes.Mul || codes[i + 2].opcode != OpCodes.Div
					|| !MagnificationDownscale.Field(codes[i + 3], OpCodes.Stfld, typeof(WheelController), "EPJKGGHAOAI")) continue;
				codes[i].opcode = OpCodes.Call; codes[i].operand = AccessTools.Method(typeof(MagnificationWheelSpeed), "TargetScale"); found++;
			}
			MagnificationDownscale.Require(found == 1, "one wheel target RPM scale divisor", found);
			return codes;
		}
		internal static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
		{
			List<CodeInstruction> codes = MagnificationDownscale.Copy(instructions);
			int found = 0;
			for (int i = codes.Count - 1; i > 0; i--)
			{
				MethodInfo call = codes[i].operand as MethodInfo;
				if (call == null || call.DeclaringType != typeof(Rigidbody) || call.Name != "set_maxAngularVelocity") continue;
				if (!MagnificationDownscale.Float(codes[i - 1], 777f)) throw new InvalidOperationException("wheel angular cap layout changed");
				CodeInstruction argument = new CodeInstruction(OpCodes.Ldarg_0);
				argument.labels.AddRange(codes[i].labels); codes[i].labels.Clear();
				argument.blocks.AddRange(codes[i].blocks); codes[i].blocks.Clear();
				codes.InsertRange(i, new[] { argument, new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(MagnificationWheelSpeed), "Limit")) });
				found++;
			}
			MagnificationDownscale.Require(found == 1, "one native wheel angular cap", found);
			return codes;
		}
	}
}
