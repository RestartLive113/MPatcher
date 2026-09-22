using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using McnCraft;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
	// Experimental models for the opt-in native Car benchmark ONLY. No bootstrap
	// registration: these are comparisons, not a released normalization policy.
	internal static class MagnificationCraftModelProbe
	{
		private static MethodInfo legacyWater;
		internal static void Register(Harmony harmony)
		{
			MagnificationInertiaBoundary.Register(harmony);
			foreach (Type type in typeof(UDIRjB3VSmwHpVmUxcnHH66kk6ThWzbxmySWu7OngspoKs_47sbCTTGVAxk9y7DBfQ).GetNestedTypes(BindingFlags.Public | BindingFlags.NonPublic))
			{
				MethodInfo found = type.GetMethod("C8DNZpzKjPK7v0b5djavXno", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
				if (found != null) legacyWater = found;
			}
			if (legacyWater == null) throw new MissingMethodException("recovered body buoyancy helper");
			// This benchmark uses dry ground. Recovered helpers contain module
			// generic calls unsupported by this Harmony emitter; retain their behavior.
			foreach (Type type in new[] { typeof(ShaftController), typeof(BodyController), typeof(MachineController) })
				harmony.Patch(AccessTools.DeclaredMethod(type, "FixedUpdate"), null, null,
					new HarmonyMethod(AccessTools.Method(typeof(MagnificationCraftModelProbe), "OtherUnits")), null);
			harmony.Patch(AccessTools.Method(typeof(WheelController), "FixedUpdate"), null, null,
				new HarmonyMethod(AccessTools.Method(typeof(MagnificationCraftModelProbe), "Wheel")), null);
			foreach (string name in new[] { "SetJoint", "FixedUpdate" })
				harmony.Patch(AccessTools.DeclaredMethod(typeof(JointController), name), null, null,
					new HarmonyMethod(AccessTools.Method(typeof(MagnificationCraftModelProbe), "Angular")), null);
		}
		private static float Scale(MachineController machine) { return MagnificationCraftProbe.Model >= 2 ? MagnificationPhysicsNormalization.MachineScale(machine) : 1f; }
		private static float OtherScale(MachineController machine) { return MagnificationCraftProbe.Model >= 7 ? Scale(machine) : 1f; }
		private static float ScaleMachineLength(float value, MachineController machine)
		{
			float s = OtherScale(machine);
			// These 50/100 arms are artificial recovery/stabilizer force couples,
			// not attachment geometry. Match the I*s*s boundary to retain angular gain.
			// Proximity padding is a world-space safety margin at native world
			// speeds; changing it would delay activation around other machines.
			return Mathf.Abs(value) < 50f ? value : value * s * (MagnificationCraftProbe.Model != 9 ? s : 1f);
		}
		private static float ScaleBodyLength(float value, BodyController body) { return value * OtherScale(body.CIPOPAGDJDE); }
		private static float UnscaleBodyLength(float value, BodyController body) { return value / OtherScale(body.CIPOPAGDJDE); }
		private static float ScaleShaftLength(float value, ShaftController shaft) { return value * OtherScale(shaft.FPEBEEJGFPI); }
		private static Vector3 ScaleShaftVector(Vector3 value, ShaftController shaft) { return value * OtherScale(shaft.FPEBEEJGFPI); }
		private static IEnumerable<CodeInstruction> OtherUnits(IEnumerable<CodeInstruction> instructions, MethodBase __originalMethod)
		{
			List<CodeInstruction> original = MagnificationDownscale.Copy(instructions);
			if (__originalMethod.DeclaringType == typeof(BodyController))
				foreach (CodeInstruction code in original)
					if (Equals(code.operand, legacyWater))
					{
						MagnificationDownscale.Log("CRAFT_BODY_WATER_ROUTE helper=recovered-MPatcher nativeBlock=replaced conversion=not-tested-dry-fixture");
						return original;
					}
			return OtherUnitsConverted(original, __originalMethod);
		}
		private static IEnumerable<CodeInstruction> OtherUnitsConverted(IEnumerable<CodeInstruction> instructions, MethodBase __originalMethod)
		{
			IEnumerable<CodeInstruction> converted = __originalMethod.DeclaringType == typeof(ShaftController)
				? MagnificationPhysicsNormalization.ShaftFixedUpdateTranspiler(instructions)
				: __originalMethod.DeclaringType == typeof(BodyController) ? MagnificationPhysicsNormalization.BodyFixedUpdateTranspiler(instructions)
				: MagnificationPhysicsNormalization.MachineFixedUpdateTranspiler(instructions);
			foreach (CodeInstruction code in converted)
			{
				MethodInfo call = code.operand as MethodInfo;
				if (call != null && call.DeclaringType == typeof(MagnificationPhysicsNormalization))
					code.operand = AccessTools.Method(typeof(MagnificationCraftModelProbe), call.Name);
				yield return code;
			}
		}
		private static IEnumerable<CodeInstruction> LegacyWater(IEnumerable<CodeInstruction> instructions)
		{
			List<CodeInstruction> codes = MagnificationDownscale.Copy(instructions);
			int halves = 0, depth = 0;
			for (int i = codes.Count - 1; i >= 0; i--)
			{
				string conversion = null;
				if (MagnificationDownscale.Float(codes[i], 0.5f)) { conversion = "ScaleBodyLength"; halves++; }
				else if (codes[i].opcode == OpCodes.Sub && i + 2 < codes.Count && MagnificationDownscale.Float(codes[i + 1], 1f)
					&& codes[i + 2].operand is MethodInfo && ((MethodInfo)codes[i + 2].operand).DeclaringType == typeof(Mathf) && ((MethodInfo)codes[i + 2].operand).Name == "Min")
				{ conversion = "UnscaleBodyLength"; depth++; }
				if (conversion != null) codes.InsertRange(i + 1, new[] { new CodeInstruction(OpCodes.Ldarg_0), new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(MagnificationCraftModelProbe), conversion)) });
			}
			MagnificationDownscale.Require(halves == 3 && depth == 1, "recovered body water half/depth", halves + depth);
			return codes;
		}
		private static float Length(float value, WheelController wheel) { return value * Scale(wheel.FPEBEEJGFPI); }
		private static float CraftLength(float value, WheelController wheel) { return value / Scale(wheel.FPEBEEJGFPI); }
		private static Vector3 ContactVelocity(Vector3 normal, Vector3 angular, WheelController wheel) { return Vector3.Cross(normal, angular) * Scale(wheel.FPEBEEJGFPI); }
		private static IEnumerable<CodeInstruction> Wheel(IEnumerable<CodeInstruction> instructions)
		{
			List<CodeInstruction> codes = new List<CodeInstruction>(MagnificationPhysicsNormalization.WheelFixedUpdateTranspiler(instructions));
			int cross = 0;
			foreach (CodeInstruction code in codes)
			{
				MethodInfo call = code.operand as MethodInfo;
				if (call != null && call.DeclaringType == typeof(MagnificationPhysicsNormalization))
				{
					if (call.Name == "ScaleWheelLength") code.operand = AccessTools.Method(typeof(MagnificationCraftModelProbe), "Length");
					if (call.Name == "UnscaleWheelLength") code.operand = AccessTools.Method(typeof(MagnificationCraftModelProbe), "CraftLength");
				}
				if (call != null && call.DeclaringType == typeof(Vector3) && call.Name == "Cross")
				{
					CodeInstruction arg = new CodeInstruction(OpCodes.Ldarg_0);
					arg.labels.AddRange(code.labels); code.labels.Clear(); arg.blocks.AddRange(code.blocks); code.blocks.Clear();
					yield return arg; code.operand = AccessTools.Method(typeof(MagnificationCraftModelProbe), "ContactVelocity"); cross++;
				}
				yield return code;
			}
			if (cross != 1) throw new InvalidOperationException("wheel contact cross count " + cross);
		}
		private static void SetSpring(HingeJoint joint, JointSpring value, JointController owner)
		{
			float scale = Scale(owner.FPEBEEJGFPI);
			if (MagnificationCraftProbe.Model >= 3)
			{
				value.spring *= scale;
				value.damper *= MagnificationCraftProbe.Model == 3 || MagnificationCraftProbe.Model == 9 ? scale : scale * Mathf.Sqrt(scale);
			}
			joint.spring = value;
		}
		private static void WheelTorque(Rigidbody body, Vector3 value, ForceMode mode, JointController owner)
		{
			if (MagnificationCraftProbe.Model >= 3 && owner.OMBBGKNHGEF != null) value /= Scale(owner.FPEBEEJGFPI);
			body.AddTorque(value, mode);
		}
		private static IEnumerable<CodeInstruction> Angular(IEnumerable<CodeInstruction> instructions)
		{
			foreach (CodeInstruction source in instructions)
			{
				CodeInstruction code = new CodeInstruction(source);
				MethodInfo method = code.operand as MethodInfo;
				string replacement = method != null && method.DeclaringType == typeof(HingeJoint) && method.Name == "set_spring" ? "SetSpring"
					: method != null && method.DeclaringType == typeof(Rigidbody) && method.Name == "AddTorque" && method.GetParameters().Length == 2 ? "WheelTorque" : null;
				if (replacement != null)
				{
					CodeInstruction arg = new CodeInstruction(OpCodes.Ldarg_0); arg.labels.AddRange(code.labels); code.labels.Clear(); arg.blocks.AddRange(code.blocks); code.blocks.Clear();
					yield return arg; code.opcode = OpCodes.Call; code.operand = AccessTools.Method(typeof(MagnificationCraftModelProbe), replacement);
				}
				yield return code;
			}
		}
		internal static void ApplyInertia(MachineController machine)
		{
			if (MagnificationCraftProbe.Model < 3) return;
			float scale = Scale(machine);
			float factor = MagnificationCraftProbe.Model == 3 || MagnificationCraftProbe.Model == 9 ? scale : scale * scale;
			HashSet<int> seen = new HashSet<int>();
			List<GameObject> roots = new List<GameObject> { machine.gameObject };
			roots.AddRange(machine.KBLANAFAJFP);
			if (MagnificationCraftProbe.Model == 10)
			{
				MagnificationJointMassBalance balance = machine.GetComponent<MagnificationJointMassBalance>();
				if (balance != null) balance.enabled = false;
				HashSet<int> restored = new HashSet<int>();
				foreach (GameObject root in roots) if (root != null) foreach (Joint joint in root.GetComponents<Joint>())
				{
					if (joint.connectedBody == null || (!(joint is HingeJoint) && !(joint is ConfigurableJoint)) || !restored.Add(joint.GetInstanceID())) continue;
					if (!MagnificationJointMassNative.Balance(joint, float.MaxValue, true, true)) throw new InvalidOperationException("probe-restore-native-joint-mass");
				}
				MagnificationDownscale.Log("CRAFT_MODEL_CONDITIONING ratio=native restored=" + restored.Count);
			}
			foreach (GameObject root in roots) if (root != null) foreach (Rigidbody body in root.GetComponentsInChildren<Rigidbody>())
			{
				if (!seen.Add(body.GetInstanceID())) continue;
				MagnificationInertiaBoundary.Track(body, factor);
				MagnificationInertiaBoundary.CheckRoundtrip(body);
				if (MagnificationCraftProbe.Model == 6 || MagnificationCraftProbe.Model == 8) { body.solverIterations = Math.Max(body.solverIterations, 50); body.solverVelocityIterations = Math.Max(body.solverVelocityIterations, 20); }
			}
			if (MagnificationCraftProbe.Model == 5 || MagnificationCraftProbe.Model == 6 || MagnificationCraftProbe.Model == 8)
			{
				HashSet<int> changed = new HashSet<int>();
				foreach (GameObject root in roots) if (root != null)
				{
					foreach (Collider collider in root.GetComponentsInChildren<Collider>())
						if (changed.Add(collider.GetInstanceID())) collider.contactOffset = Math.Max(0.00001f, collider.contactOffset * scale);
					foreach (ConfigurableJoint joint in root.GetComponentsInChildren<ConfigurableJoint>())
						if (changed.Add(joint.GetInstanceID())) joint.projectionDistance *= scale;
				}
			}
			MagnificationDownscale.Log("CRAFT_MODEL_INERTIA model=" + MagnificationCraftProbe.Model + " bodies=" + seen.Count + " factor=" + factor + " exposed=" + MagnificationInertiaBoundary.Read(machine.NFMPBACKJOJ).ToString("R") + " physical=" + MagnificationInertiaBoundary.Physical(machine.NFMPBACKJOJ).ToString("R") + " repeatedWrites=pass");
		}
	}
}
