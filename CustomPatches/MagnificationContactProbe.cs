using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
	// Native ContactSensor turns collisions off for two fixed frames when a fast
	// body is separating from a surface. Measure this path before changing policy.
	internal static class MagnificationContactProbe
	{
		internal static int DisableRequests, Suppressed;
		internal static void Reset() { DisableRequests = Suppressed = 0; }
		internal static bool ShouldSuppress(bool enabled, float scale, int model)
		{
			return !enabled && scale >= .01f && scale < 1f && model >= 13 && model < 17;
		}
		private static void SetCollision(Rigidbody body, bool enabled, ContactSensor sensor)
		{
			if (!enabled) DisableRequests++;
			if (sensor.DLMKKFCHFNC != null && !MagnificationPhysicsMode.Applies(sensor.DLMKKFCHFNC.CIPOPAGDJDE))
			{
				body.detectCollisions = enabled;
				return;
			}
			float scale = sensor.DLMKKFCHFNC == null ? 1f : MagnificationPhysicsNormalization.MachineScale(sensor.DLMKKFCHFNC.CIPOPAGDJDE);
			if (ShouldSuppress(enabled, scale, MagnificationCraftProbe.Model)) { Suppressed++; return; }
			body.detectCollisions = enabled;
		}
		internal static void Register(Harmony harmony)
		{
			harmony.Patch(AccessTools.DeclaredMethod(typeof(ContactSensor), "OnCollisionStay"), null, null,
				new HarmonyMethod(AccessTools.Method(typeof(MagnificationContactProbe), "Transpiler")), null);
			MagnificationDownscale.Log("CONTACT_PROBE_HOOK nativeDisable=counted suppression=model13+-downscale-only");
		}
		internal static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
		{
			int found = 0;
			foreach (CodeInstruction source in instructions)
			{
				CodeInstruction code = new CodeInstruction(source);
				MethodInfo method = code.operand as MethodInfo;
				if (method != null && method.DeclaringType == typeof(Rigidbody) && method.Name == "set_detectCollisions")
				{
					CodeInstruction argument = new CodeInstruction(OpCodes.Ldarg_0);
					argument.labels.AddRange(code.labels); code.labels.Clear(); argument.blocks.AddRange(code.blocks); code.blocks.Clear();
					yield return argument; code.opcode = OpCodes.Call;
					code.operand = AccessTools.Method(typeof(MagnificationContactProbe), "SetCollision"); found++;
				}
				yield return code;
			}
			MagnificationDownscale.Require(found == 1, "one native temporary collision toggle", found);
		}
	}
}
