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
			MagnificationMassBoundary.Register(harmony);
			MagnificationForceUnits.Register(harmony);
			MagnificationWheelSpeed.Register(harmony);
			MagnificationLengthUnits.Register(harmony);
			MagnificationVelocityUnits.Register(harmony);
			MagnificationSpeedLimitUnits.Register(harmony);
            MagnificationSafetyUnits.Register(harmony);
			MagnificationEnvironmentUnits.Register(harmony);
			MagnificationContactProbe.Register(harmony);
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
			foreach (string name in new[] { "SetJoint", "FixedUpdate", "Reset" })
				harmony.Patch(AccessTools.DeclaredMethod(typeof(JointController), name), null, null,
					new HarmonyMethod(AccessTools.Method(typeof(MagnificationCraftModelProbe), "Angular")), null);
		}
		private static float Scale(MachineController machine) { return MagnificationCraftProbe.Model >= 2 ? MagnificationPhysicsMode.PhysicsScale(machine) : 1f; }
		private static float GeometryScale(MachineController machine) { return MagnificationPhysicsNormalization.MachineScale(machine); }
		private static float OtherScale(MachineController machine) { return MagnificationCraftProbe.Model >= 7 ? GeometryScale(machine) : 1f; }
		private static float ScaleMachineLength(float value, MachineController machine)
		{
			if (!MagnificationPhysicsMode.Applies(machine)) return value;
			float s = OtherScale(machine);
			// These 50/100 arms are artificial recovery/stabilizer force couples,
			// not attachment geometry. Match the I*s*s boundary to retain angular gain.
			// Same-time motion scales world speed, including proximity padding.
			// Older diagnostic models retain their original world-space margins.
			return Mathf.Abs(value) < 50f && !MagnificationScalePolicy.SameTime ? value : value * s * (MagnificationCraftProbe.Model != 9 && !MagnificationScalePolicy.Enabled ? s : 1f);
		}
		private static float ScaleBodyLength(float value, BodyController body) { return !MagnificationPhysicsMode.Applies(body.CIPOPAGDJDE) ? value : value * OtherScale(body.CIPOPAGDJDE); }
		private static float UnscaleBodyLength(float value, BodyController body) { return !MagnificationPhysicsMode.Applies(body.CIPOPAGDJDE) ? value : value / OtherScale(body.CIPOPAGDJDE); }
		private static float ScaleShaftLength(float value, ShaftController shaft) { return !MagnificationPhysicsMode.Applies(shaft.FPEBEEJGFPI) ? value : value * OtherScale(shaft.FPEBEEJGFPI); }
		private static Vector3 ScaleShaftVector(Vector3 value, ShaftController shaft) { return !MagnificationPhysicsMode.Applies(shaft.FPEBEEJGFPI) ? value : value * OtherScale(shaft.FPEBEEJGFPI); }
		private static IEnumerable<CodeInstruction> OtherUnits(IEnumerable<CodeInstruction> instructions, MethodBase __originalMethod)
		{
			List<CodeInstruction> original = MagnificationDownscale.Copy(instructions);
			if (__originalMethod.DeclaringType == typeof(BodyController))
				foreach (CodeInstruction code in original)
					if (Equals(code.operand, legacyWater))
					{
						MagnificationDownscale.Log("CRAFT_BODY_WATER_ROUTE helper=recovered-MPatcher nativeBlock=replaced conversion=research-generated-source wetRuntime=pending");
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
		private static float Length(float value, WheelController wheel) { return !MagnificationPhysicsMode.Applies(wheel.FPEBEEJGFPI) ? value : value * GeometryScale(wheel.FPEBEEJGFPI); }
		private static float CraftLength(float value, WheelController wheel) { return !MagnificationPhysicsMode.Applies(wheel.FPEBEEJGFPI) ? value : value / GeometryScale(wheel.FPEBEEJGFPI); }
		private static Vector3 ContactVelocity(Vector3 normal, Vector3 angular, WheelController wheel)
		{
			if (!MagnificationPhysicsMode.Applies(wheel.FPEBEEJGFPI)) return Vector3.Cross(normal, angular);
			return Vector3.Cross(normal, angular) * (MagnificationScalePolicy.SameTime ? 1f : Scale(wheel.FPEBEEJGFPI));
		}
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
			if (!MagnificationPhysicsMode.Applies(owner.FPEBEEJGFPI)) { joint.spring = value; return; }
			float scale = Scale(owner.FPEBEEJGFPI);
			joint.spring = MagnificationRotorIsolationProbe.FilterSpring(joint, RotorDrive(value, scale, MagnificationCraftProbe.Model), false);
		}
		internal static JointSpring RotorDrive(JointSpring value, float scale, int model)
		{
			if (model == 20 || model == 21 || model == 22) return MagnificationScalePolicy.AngularSpring(value, scale, true);
			if (model < 3) return value;
			// 14 preserves the native angular natural frequency and damping ratio
			// with I*s*s. 15 isolates spring strength (20x model 13 at 5%).
			// Both are controlled comparisons, not a release policy.
			value.spring *= model == 14 || model >= 17 ? scale * scale : model == 15 ? 1f : scale;
			value.damper *= model == 14 || model >= 17 ? scale * scale : model == 3 || model == 9 ? scale : scale * (float)Math.Sqrt(scale);
			return value;
		}
		private static void WheelTorque(Rigidbody body, Vector3 value, ForceMode mode, JointController owner)
		{
			if (!MagnificationPhysicsMode.Applies(owner.FPEBEEJGFPI)) { body.AddTorque(value, mode); return; }
			if (MagnificationCraftProbe.Model >= 3 && MagnificationCraftProbe.Model < 16 && owner.OMBBGKNHGEF != null) value /= Scale(owner.FPEBEEJGFPI);
			if (MagnificationRotorIsolationProbe.SuppressTorque(owner, value, mode)) { MagnificationRotorProbe.RecordTorque(owner, Vector3.zero, mode); return; }
			MagnificationRotorProbe.RecordTorque(owner, value, mode);
			body.AddTorque(value, mode);
		}
		private static IEnumerable<CodeInstruction> Angular(IEnumerable<CodeInstruction> instructions)
		{
			int springs = 0;
			foreach (CodeInstruction source in instructions)
			{
				CodeInstruction code = new CodeInstruction(source);
				MethodInfo method = code.operand as MethodInfo;
				string replacement = method != null && method.DeclaringType == typeof(HingeJoint) && method.Name == "set_spring" ? "SetSpring"
					: method != null && method.DeclaringType == typeof(Rigidbody) && method.Name == "AddTorque" && method.GetParameters().Length == 2 ? "WheelTorque" : null;
				if (replacement != null)
				{
					if (replacement == "SetSpring") springs++;
					CodeInstruction arg = new CodeInstruction(OpCodes.Ldarg_0); arg.labels.AddRange(code.labels); code.labels.Clear(); arg.blocks.AddRange(code.blocks); code.blocks.Clear();
					yield return arg; code.opcode = OpCodes.Call; code.operand = AccessTools.Method(typeof(MagnificationCraftModelProbe), replacement);
				}
				yield return code;
			}
			if (springs == 0) throw new InvalidOperationException("missing native angular spring setter");
		}
		internal static void ApplyInertia(MachineController machine)
		{
			if (!MagnificationPhysicsMode.Applies(machine)) return;
			if (MagnificationCraftProbe.Model < 3) return;
			float scale = Scale(machine);
			if (MagnificationScalePolicy.Bypass(scale)) { MagnificationDownscale.Log("SIMILARITY_BASELINE scale=1 extraMassWrites=0 extraInertiaWrites=0 conditioning=false"); return; }
			MagnificationConstraintUnits.Apply(machine);
			float factor = MagnificationScalePolicy.Enabled ? MagnificationScalePolicy.Inertia(scale) : MagnificationCraftProbe.Model == 3 || MagnificationCraftProbe.Model == 9 ? scale : scale * scale;
			HashSet<int> seen = new HashSet<int>();
			List<GameObject> roots = new List<GameObject> { machine.gameObject };
			roots.AddRange(machine.KBLANAFAJFP);
			// MakeBlur clones a tire, then schedules its Rigidbody/Joint for Destroy.
			// They remain enumerable until end of frame and already carry physical
			// values. Only structural bodies and the wheel's live tire are owners.
			HashSet<int> physical = new HashSet<int>();
			HashSet<int> sledBodies = new HashSet<int>();
			foreach (GameObject root in roots) if (root != null)
			{
				foreach (BodyController owner in root.GetComponentsInChildren<BodyController>())
					if (owner.NFMPBACKJOJ != null) physical.Add(owner.NFMPBACKJOJ.GetInstanceID());
				foreach (WheelController wheel in root.GetComponentsInChildren<WheelController>())
					if (wheel.EKHOCFCEGCB != null) physical.Add(wheel.EKHOCFCEGCB.GetInstanceID());
				// The native event helper has a real FixedJoint and contributes
				// inertia. It is not one of MakeBlur's disposable visual copies.
				foreach (FixedJoint joint in root.GetComponentsInChildren<FixedJoint>())
					if (joint.connectedBody != null && joint.GetComponent<Rigidbody>() != null)
						physical.Add(joint.GetComponent<Rigidbody>().GetInstanceID());
				foreach (SledController sled in root.GetComponentsInChildren<SledController>())
				{
					Rigidbody roller = MagnificationMassBoundary.SledBody(sled);
					if (roller != null) { physical.Add(roller.GetInstanceID()); sledBodies.Add(roller.GetInstanceID()); }
				}
			}
			if (MagnificationCraftProbe.Model == 18 || MagnificationCraftProbe.Model == 19 || MagnificationCraftProbe.Model == 20 || MagnificationCraftProbe.Model == 22)
			{
				if (machine.GetComponent<MagnificationJointMassBalance>() != null) throw new InvalidOperationException("native-joint-model-has-mass-conditioning");
				MagnificationDownscale.Log("CRAFT_MODEL_CONDITIONING ratio=native installed=false nativeMassBridge=unused");
			}
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
				if (MagnificationScalePolicy.Enabled && sledBodies.Contains(body.GetInstanceID())) continue;
				if (MagnificationScalePolicy.Enabled && !physical.Contains(body.GetInstanceID()))
				{
					MagnificationDownscale.Log("MASS_UNITS_AUXILIARY body=" + body.name + " kinematic=" + body.isKinematic + " reason=not-structural-body-or-live-tire");
					continue;
				}
				if (MagnificationScalePolicy.Enabled) MagnificationMassBoundary.TrackPhysics(body, scale);
				else MagnificationInertiaBoundary.Track(body, factor);
				if (!MagnificationScalePolicy.SameTime) MagnificationInertiaBoundary.CheckRoundtrip(body);
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
			MagnificationDownscale.Log("CRAFT_MODEL_INERTIA model=" + MagnificationCraftProbe.Model + " bodies=" + seen.Count + " factor=" + factor + " exposed=" + MagnificationInertiaBoundary.Read(machine.NFMPBACKJOJ).ToString("R") + " physical=" + MagnificationInertiaBoundary.Physical(machine.NFMPBACKJOJ).ToString("R") + (MagnificationScalePolicy.SameTime ? " repeatedWrites=disabled-in-live-probe" : " repeatedWrites=pass"));
		}
	}
}

