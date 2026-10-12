using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using McnCraft;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
	// Corrections at audited world/craft boundaries, isolated from inertia policy.
	// Not registered by the 0.3.8 bootstrap; enabled only in research model 12+.
	internal static class MagnificationLengthUnits
	{
		internal static float WorldLength(float value, float scale) { return value * MagnificationPhysicsNormalization.ScaleFactor(scale); }
		internal static float LinearSpring(float value, float scale) { return value / MagnificationPhysicsNormalization.ScaleFactor(scale); }
		internal static float LinearDamper(float value, float scale) { return value / (float)Math.Sqrt(MagnificationPhysicsNormalization.ScaleFactor(scale)); }
		private static float Scale(MachineController machine) { return MagnificationCraftProbe.Model >= 12 ? MagnificationPhysicsMode.PhysicsScale(machine) : 1f; }
		private static float GeometryScale(MachineController machine) { return MagnificationCraftProbe.Model >= 12 ? MagnificationPhysicsNormalization.MachineScale(machine) : 1f; }
		private static float PartLength(float value, PartsController owner) { return !MagnificationPhysicsMode.Applies(owner.FPEBEEJGFPI) ? value : WorldLength(value, GeometryScale(owner.FPEBEEJGFPI)); }
		private static float WheelSpring(float value, WheelController owner) { return !MagnificationPhysicsMode.Applies(owner.FPEBEEJGFPI) ? value : LinearSpring(value, Scale(owner.FPEBEEJGFPI)); }
		private static float CannonLength(float value, BodyController owner) { return !MagnificationPhysicsMode.Applies(owner.CIPOPAGDJDE) ? value : WorldLength(value, GeometryScale(owner.CIPOPAGDJDE)); }
		private static float ContactSpring(float value, ContactSensor owner) { if (!MagnificationPhysicsMode.Applies(owner.DLMKKFCHFNC.CIPOPAGDJDE)) return value; float s = Scale(owner.DLMKKFCHFNC.CIPOPAGDJDE); return MagnificationScalePolicy.Enabled ? value * MagnificationScalePolicy.LinearSpring(s) : LinearSpring(value, s); }
		private static float ContactDamper(float value, ContactSensor owner) { if (!MagnificationPhysicsMode.Applies(owner.DLMKKFCHFNC.CIPOPAGDJDE)) return value; float s = Scale(owner.DLMKKFCHFNC.CIPOPAGDJDE); return MagnificationScalePolicy.Enabled ? value * MagnificationScalePolicy.LinearDamper(s) : LinearDamper(value, s); }
		private static float ContactBreakForce(float value, ContactSensor owner) { if (!MagnificationPhysicsMode.Applies(owner.DLMKKFCHFNC.CIPOPAGDJDE)) return value; return MagnificationScalePolicy.Enabled ? value * MagnificationScalePolicy.Force(Scale(owner.DLMKKFCHFNC.CIPOPAGDJDE)) : value; }
		internal static void Register(Harmony harmony)
		{
			foreach (MethodInfo method in new[] { AccessTools.DeclaredMethod(typeof(PistonController), "FixedUpdate"),
				AccessTools.DeclaredMethod(typeof(WheelController), "FixedUpdate"), AccessTools.DeclaredMethod(typeof(BodyController), "CalcCannonMoment"),
				AccessTools.DeclaredMethod(typeof(ContactSensor), "Adsorb"), AccessTools.DeclaredMethod(typeof(ContactSensor), "Adjust") })
			{
				harmony.Patch(method, null, null, new HarmonyMethod(AccessTools.Method(typeof(MagnificationLengthUnits), "Transpiler")), null);
				MagnificationDownscale.Log("LENGTH_UNITS_HOOK target=" + method.DeclaringType.Name + "." + method.Name + " scope=research-model12+");
			}
		}
		internal static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, MethodBase __originalMethod)
		{
			List<CodeInstruction> codes = MagnificationDownscale.Copy(instructions);
			Type owner = __originalMethod.DeclaringType;
			int found = 0;
			for (int i = codes.Count - 2; i >= 0; i--)
			{
				string helper = null;
				int insert = i + 1;
				if (owner == typeof(PistonController) && MagnificationDownscale.Field(codes[i], OpCodes.Ldfld, owner, "PDBPFCDDFAE"))
				{
					if (codes[i + 1].opcode != OpCodes.Bge_Un) throw new InvalidOperationException("piston safety threshold branch changed");
					helper = "PartLength";
				}
				else if (owner == typeof(WheelController) && MagnificationDownscale.Float(codes[i], 30f)
					&& Call(codes[i + 1], typeof(Vector3), "op_Multiply")) helper = "WheelSpring";
				else if (owner == typeof(BodyController) && MagnificationDownscale.Field(codes[i], OpCodes.Ldfld, typeof(CannonController), "AIBNJHMPMJN"))
				{
					if (codes[i + 1].opcode != OpCodes.Conv_R4) throw new InvalidOperationException("cannon length conversion changed");
					insert = i + 2; helper = "CannonLength";
				}
				else if (owner == typeof(ContactSensor))
				{
					if (Call(codes[i], typeof(SpringJoint), "set_spring")) helper = "ContactSpring";
					if (Call(codes[i], typeof(SpringJoint), "set_damper")) helper = "ContactDamper";
					if (Call(codes[i], typeof(Joint), "set_breakForce")) helper = "ContactBreakForce";
					insert = i;
				}
				if (helper == null) continue;
				CodeInstruction argument = new CodeInstruction(OpCodes.Ldarg_0);
				argument.labels.AddRange(codes[insert].labels); codes[insert].labels.Clear();
				argument.blocks.AddRange(codes[insert].blocks); codes[insert].blocks.Clear();
				codes.InsertRange(insert, new[] { argument, new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(MagnificationLengthUnits), helper)) });
				found++;
			}
			int expected = __originalMethod.Name == "Adsorb" ? 3 : owner == typeof(BodyController) ? 2 : 1;
			MagnificationDownscale.Require(found == expected, "world/craft boundaries " + owner.Name + "." + __originalMethod.Name, found);
			return codes;
		}
		private static bool Call(CodeInstruction code, Type type, string name)
		{
			MethodInfo call = code.operand as MethodInfo;
			return call != null && call.DeclaringType == type && call.Name == name;
		}
	}
}
