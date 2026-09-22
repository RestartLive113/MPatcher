using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
	// Piston drive coefficients are stored in craft units. Convert a copy at the
	// native setter, leaving the controller cache intact for later parameter changes.
	internal static class MagnificationPistonDrive
	{
		internal static void Register(Harmony harmony)
		{
			foreach (string name in new[] { "SetJoint", "SetSpring", "SetDamper", "SetSpringRate", "MIONKDMBNGG" })
			{
				MethodInfo target = AccessTools.Method(typeof(PistonController), name);
				if (target == null || target.DeclaringType != typeof(PistonController)) throw new MissingMethodException("PistonController", name);
				harmony.Patch(target, null, null, new HarmonyMethod(AccessTools.Method(typeof(MagnificationPistonDrive), "Transpiler")), null);
				MagnificationDownscale.Log("HOOK target=PistonController." + name + " drive=spring-inverse-scale+damper-inverse-sqrt-scale maxForce=native scope=downscale");
			}
		}
		internal static JointDrive NormalizeDrive(JointDrive value, float scale)
		{
			if (!(scale >= 0.01f && scale < 1f)) return value;
			value.positionSpring /= scale;
			value.positionDamper /= (float)Math.Sqrt(scale);
			return value;
		}
		private static JointDrive ToWorld(JointDrive value, PistonController piston)
		{
			return NormalizeDrive(value, MagnificationPhysicsNormalization.MachineScale(piston.FPEBEEJGFPI));
		}
		internal static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
		{
			List<CodeInstruction> codes = MagnificationDownscale.Copy(instructions);
			int found = 0;
			HashSet<string> axes = new HashSet<string>();
			for (int i = codes.Count - 1; i >= 0; i--)
			{
				MethodInfo method = codes[i].operand as MethodInfo;
				if ((codes[i].opcode != OpCodes.Call && codes[i].opcode != OpCodes.Callvirt) || method == null
					|| method.DeclaringType != typeof(ConfigurableJoint)
					|| (method.Name != "set_xDrive" && method.Name != "set_yDrive" && method.Name != "set_zDrive")) continue;
				if (i == 0 || !MagnificationDownscale.Field(codes[i - 1], OpCodes.Ldfld, typeof(PistonController), "EDEDBPALHEL"))
					throw new InvalidOperationException("Piston drive source is no longer the unscaled controller cache");
				CodeInstruction argument = new CodeInstruction(OpCodes.Ldarg_0);
				argument.labels.AddRange(codes[i].labels); codes[i].labels.Clear();
				argument.blocks.AddRange(codes[i].blocks); codes[i].blocks.Clear();
				codes.InsertRange(i, new[] { argument, new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(MagnificationPistonDrive), "ToWorld")) });
				found++; axes.Add(method.Name);
			}
			MagnificationDownscale.Require(found == 3 && axes.Count == 3, "one cached piston drive write per axis", found);
			return codes;
		}
	}
}
