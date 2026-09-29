using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using McnCraft;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
	// Only the audited controller decisions read craft speed. The shared machine
	// speed and Rigidbody velocity remain world values for display and integration.
	internal static class MagnificationVelocityUnits
	{
		internal static float ToCraftSpeed(float value, float scale, bool enabled)
		{
			float s = MagnificationScalePolicy.Valid(scale);
			return !enabled || s == 1f ? value : value / s;
		}
		internal static Vector3 ToCraftVelocity(Vector3 value, float scale, bool enabled)
		{
			float s = MagnificationScalePolicy.Valid(scale);
			return !enabled || s == 1f ? value : value / s;
		}
		private static float MachineSpeed(float value, MachineController owner)
		{
			if (!MagnificationScalePolicy.SameTime) return value;
			return ToCraftSpeed(value, MagnificationPhysicsNormalization.MachineScale(owner), true);
		}
		private static float PistonSpeed(float value, PistonController owner)
		{
			if (!MagnificationScalePolicy.SameTime) return value;
			return ToCraftSpeed(value, MagnificationPhysicsNormalization.MachineScale(owner.FPEBEEJGFPI), true);
		}
		private static Vector3 ContactVelocity(Vector3 value, ContactSensor owner)
		{
			if (!MagnificationScalePolicy.SameTime || owner.DLMKKFCHFNC == null) return value;
			return ToCraftVelocity(value, MagnificationPhysicsNormalization.MachineScale(owner.DLMKKFCHFNC.CIPOPAGDJDE), true);
		}
		internal static void Register(Harmony harmony)
		{
			foreach (Type type in new[] { typeof(MachineController), typeof(PistonController), typeof(ContactSensor) })
			{
				string name = type == typeof(ContactSensor) ? "OnCollisionStay" : "FixedUpdate";
				MethodInfo target = AccessTools.DeclaredMethod(type, name);
				if (target == null) throw new MissingMethodException(type.FullName, name);
				harmony.Patch(target, null, null, new HarmonyMethod(AccessTools.Method(typeof(MagnificationVelocityUnits), "Transpiler")), null);
				MagnificationDownscale.Log("VELOCITY_UNITS_HOOK target=" + type.Name + "." + name
					+ " active=model22 units=source-speed sharedWorldSpeed=unchanged baseline100=direct");
			}
		}
		private static bool Site(CodeInstruction code, bool contact)
		{
			if (!contact) return MagnificationDownscale.Field(code, OpCodes.Ldfld, typeof(MachineController), "KDCAPPINCMF");
			MethodInfo method = code.operand as MethodInfo;
			return (code.opcode == OpCodes.Call || code.opcode == OpCodes.Callvirt) && method != null
				&& method.DeclaringType == typeof(Rigidbody) && method.Name == "get_velocity";
		}
		internal static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, MethodBase __originalMethod)
		{
			bool contact = __originalMethod.DeclaringType == typeof(ContactSensor);
			bool machine = __originalMethod.DeclaringType == typeof(MachineController);
			int token = contact ? 0x06002DA6 : machine ? 0x060041C8 : 0x06004CFC;
			if ((!contact && !machine && __originalMethod.DeclaringType != typeof(PistonController))
				|| __originalMethod.MetadataToken != token
				|| __originalMethod.Name != (contact ? "OnCollisionStay" : "FixedUpdate"))
				throw new InvalidOperationException("unaudited-velocity-target " + __originalMethod);
			string helper = contact ? "ContactVelocity" : machine ? "MachineSpeed" : "PistonSpeed";
			int expected = machine || contact ? 2 : 1, found = 0;
			List<CodeInstruction> codes = MagnificationDownscale.Copy(instructions);
			for (int i = codes.Count - 2; i >= 0; i--)
			{
				if (!Site(codes[i], contact)) continue;
				// Branches that originally enter the consumer must perform the same
				// conversion as fallthrough, with its existing value on the stack.
				CodeInstruction owner = new CodeInstruction(OpCodes.Ldarg_0);
				owner.labels.AddRange(codes[i + 1].labels); codes[i + 1].labels.Clear();
				owner.blocks.AddRange(codes[i + 1].blocks); codes[i + 1].blocks.Clear();
				codes.InsertRange(i + 1, new[] { owner,
					new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(MagnificationVelocityUnits), helper)) });
				found++;
			}
			MagnificationDownscale.Require(found == expected, "same-time semantic speed reads " + __originalMethod.DeclaringType.Name, found);
			return codes;
		}
	}
}
