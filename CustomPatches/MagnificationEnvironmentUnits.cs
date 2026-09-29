using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using McnCraft;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
	internal static class MagnificationEnvironmentUnits
	{
		internal static float Scale(Component owner) { return MagnificationScalePolicy.Enabled ? MagnificationPhysicsNormalization.MachineScale(MagnificationForceUnits.Machine(owner)) : 1f; }
		private static float CraftLength(float value, Component owner) { return value / Scale(owner); }
		private static Vector3 CraftVelocity(Vector3 value, Component owner) { return value / Scale(owner); }
		internal static void Register(Harmony harmony)
		{
			foreach (Type type in new[] { typeof(WheelController), typeof(ShaftController), typeof(AGDeviceController), typeof(SledController) })
			{
				harmony.Patch(AccessTools.DeclaredMethod(type, "FixedUpdate"), null, null,
					new HarmonyMethod(AccessTools.Method(typeof(MagnificationEnvironmentUnits), "Transpiler")), null);
				MagnificationDownscale.Log("ENVIRONMENT_UNITS_HOOK target=" + type.Name + ".FixedUpdate units=craft-depth-and-speed force=emitter-mass");
			}
		}
		internal static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, MethodBase __originalMethod)
		{
			List<CodeInstruction> codes = MagnificationDownscale.Copy(instructions);
			int sites = 0;
			bool wheel = __originalMethod.DeclaringType == typeof(WheelController);
			bool agd = __originalMethod.DeclaringType == typeof(AGDeviceController);
			bool sled = __originalMethod.DeclaringType == typeof(SledController);
			for (int i = codes.Count - 2; i >= 1; i--)
			{
				bool convert = false;
				string helper = "CraftLength";
				if (agd)
				{
					// Water-only AGD: Clamp(.5 - worldY / s, 0, 1).
					// The sea plane remains world y=0; the immersion width scales.
					convert = MagnificationDownscale.Field(codes[i], OpCodes.Ldfld, typeof(Vector3), "y");
				}
				else if (sled)
				{
					// Thresholds and adhesion spring feedback use source speed.
					// Do not change the machine's shared/displayed world speed.
					convert = MagnificationDownscale.Field(codes[i], OpCodes.Ldfld, typeof(MachineController), "KDCAPPINCMF");
					if (Call(codes[i], typeof(Rigidbody), "get_velocity")) { convert = true; helper = "CraftVelocity"; }
				}
				else if (wheel)
				{
					// V6 is shaped submerged depth. Geometry computations need world
					// depth; only the buoyancy/drag inputs use source-craft depth.
					LocalVariableInfo local = codes[i].operand as LocalVariableInfo;
					LocalBuilder builder = codes[i].operand as LocalBuilder;
					int index = local != null ? local.LocalIndex : builder != null ? builder.LocalIndex : codes[i].operand is byte ? (byte)codes[i].operand : -1;
					convert = (codes[i].opcode == OpCodes.Ldloc_S || codes[i].opcode == OpCodes.Ldloc) && index == 6
						&& (Call(codes[i - 1], typeof(Vector3), "get_up") || Call(codes[i + 1], typeof(Mathf), "Sqrt"));
				}
				else
				{
					// Two finite differences: world delta * 100 -> craft velocity.
					convert = MagnificationDownscale.Float(codes[i], 100f) && Call(codes[i + 1], typeof(Vector3), "op_Multiply");
					// Two water tests read the force point's world y relative to y=0.
					if (MagnificationDownscale.Field(codes[i], OpCodes.Ldfld, typeof(Vector3), "y")) convert = true;
				}
				if (!convert) continue;
				codes.InsertRange(i + 1, new[] { new CodeInstruction(OpCodes.Ldarg_0), new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(MagnificationEnvironmentUnits), helper)) });
				sites++;
			}
			MagnificationDownscale.Require(sites == (agd ? 1 : wheel || sled ? 3 : 4), "environment units " + __originalMethod.DeclaringType.Name, sites);
			return codes;
		}
		private static bool Call(CodeInstruction code, Type type, string name) { MethodInfo method = code.operand as MethodInfo; return method != null && method.DeclaringType == type && method.Name == name; }
	}
}
