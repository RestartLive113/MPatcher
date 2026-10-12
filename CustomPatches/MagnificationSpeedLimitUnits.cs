using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using McnCraft;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
	// Keep the configured cap and its native decay in source units. Only the
	// velocity comparison and the physical clamp receive world speed limits.
	internal static class MagnificationSpeedLimitUnits
	{
		internal static float ToWorldLimit(float value, float scale, bool enabled)
		{
			float s = MagnificationScalePolicy.Valid(scale);
			return !enabled || s == 1f || value <= 0f ? value : value * s;
		}
		private static float MachineLimit(float value, MachineController owner)
		{
			if (!MagnificationScalePolicy.SameTime) return value;
			return ToWorldLimit(value, MagnificationPhysicsMode.PhysicsScale(owner), true);
		}
		private static float BodyLimit(float value, BodyController owner)
		{
			if (!MagnificationScalePolicy.SameTime) return value;
			return ToWorldLimit(value, MagnificationPhysicsMode.PhysicsScale(owner.CIPOPAGDJDE), true);
		}
		internal static void Register(Harmony harmony)
		{
			foreach (int token in new[] { 0x060041C8, 0x06002681 })
			{
				MethodInfo target = (MethodInfo)typeof(MachineController).Module.ResolveMethod(token);
				harmony.Patch(target, null, null, new HarmonyMethod(AccessTools.Method(typeof(MagnificationSpeedLimitUnits), "Transpiler")), null);
				MagnificationDownscale.Log("SPEED_LIMIT_UNITS_HOOK target=" + target.DeclaringType.Name + "." + target.Name
					+ " active=model22 sourceCap=unchanged sourceDecay=unchanged physicalCap=source*scale baseline100=direct");
			}
		}
		private static bool LimitField(CodeInstruction code)
		{
			return MagnificationDownscale.Field(code, OpCodes.Ldfld, typeof(MachineController), "BHACJNECPIM");
		}
		private static bool Clamp(CodeInstruction code)
		{
			MethodInfo method = code.operand as MethodInfo;
			return code.opcode == OpCodes.Call && method != null && method.DeclaringType == typeof(Vector3)
				&& method.Name == "ClampMagnitude" && method.GetParameters().Length == 2;
		}
		internal static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, MethodBase __originalMethod)
		{
			List<CodeInstruction> codes = MagnificationDownscale.Copy(instructions);
			if (__originalMethod.DeclaringType == typeof(MachineController) && __originalMethod.Name == "FixedUpdate"
				&& __originalMethod.MetadataToken == 0x060041C8)
			{
				int reads = 0, pairs = 0, pair = -1;
				for (int i = 0; i < codes.Count; i++)
				{
					if (!LimitField(codes[i])) continue;
					reads++;
					if (i + 4 < codes.Count && codes[i + 1].opcode == OpCodes.Ldarg_0 && LimitField(codes[i + 2])
						&& codes[i + 3].opcode == OpCodes.Mul && codes[i + 4].opcode == OpCodes.Bgt_Un) { pairs++; pair = i; }
				}
				if (reads != 3 || pairs != 1) throw new InvalidOperationException("speed-limit-machine-layout reads=" + reads + " pairs=" + pairs);
				// The other read at IL0AD0 belongs to the source-unit cap decay.
				Insert(codes, pair + 3, "MachineLimit");
				Insert(codes, pair + 1, "MachineLimit");
			}
			else if (__originalMethod.DeclaringType == typeof(BodyController) && __originalMethod.Name == "_ApplyGravityAndFieldAccel"
				&& __originalMethod.MetadataToken == 0x06002681)
			{
				int clamps = 0, at = -1;
				for (int i = 0; i < codes.Count; i++) if (Clamp(codes[i])) { clamps++; at = i; }
				if (clamps != 1 || at < 1 || codes[at - 1].opcode != OpCodes.Ldarg_3)
					throw new InvalidOperationException("speed-limit-body-layout clamps=" + clamps);
				// The earlier argument read tests the unchanged disabled sentinel.
				Insert(codes, at, "BodyLimit");
			}
			else throw new InvalidOperationException("unaudited-speed-limit-target " + __originalMethod);
			return codes;
		}
		private static void Insert(List<CodeInstruction> codes, int at, string helper)
		{
			CodeInstruction owner = new CodeInstruction(OpCodes.Ldarg_0);
			owner.labels.AddRange(codes[at].labels); codes[at].labels.Clear();
			owner.blocks.AddRange(codes[at].blocks); codes[at].blocks.Clear();
			codes.InsertRange(at, new[] { owner, new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(MagnificationSpeedLimitUnits), helper)) });
		}
	}
}
