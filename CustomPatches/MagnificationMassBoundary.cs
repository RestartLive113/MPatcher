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
	// Managed call sites retain craft mass units for native caches and scripts.
	// Unity receives the physical mass. Never multiply the stored source again.
	internal static class MagnificationMassBoundary
	{
        private sealed class Entry { internal Rigidbody Body; internal BodyController Owner; internal float Factor, Scale; }
		private static readonly Dictionary<int, Entry> entries = new Dictionary<int, Entry>();
		private static readonly HashSet<int> logged = new HashSet<int>();
		private static readonly FieldInfo sledBody = AccessTools.Field(typeof(SledController), "KBCHBCMHFEH");
		internal static int ScriptReads, ScriptWrites;
		internal static readonly int[] Tokens = { 0x060020ED, 0x06002132, 0x06002170, 0x0600260F, 0x06002618, 0x0600265D, 0x0600266E,
			0x06004119, 0x06004142, 0x0600416D, 0x0600419E, 0x060041C8, 0x060041DB, 0x06004513, 0x0600451E, 0x0600559D, 0x06005D80 };
		internal static void Register(Harmony harmony)
		{
			foreach (int token in Tokens)
			{
				MethodInfo target = (MethodInfo)typeof(MachineController).Module.ResolveMethod(token);
				if (target.ReturnType.IsValueType && !target.ReturnType.IsPrimitive && target.ReturnType != typeof(void)) throw new NotSupportedException("mass-callsite-struct-return " + target);
				harmony.Patch(target, null, null, new HarmonyMethod(AccessTools.Method(typeof(MagnificationMassBoundary), "Transpiler")), null);
				MagnificationDownscale.Log("MASS_UNITS_HOOK target=" + target.DeclaringType.Name + "." + target.Name + " token=" + token.ToString("X8"));
			}
			// GetBiggestBodyPos returns a struct (unsafe old Harmony ABI). Its
			// mass-weighted world position legitimately uses physical masses.
			harmony.Patch(AccessTools.DeclaredMethod(typeof(WheelController), "MakeTire"), null,
				new HarmonyMethod(AccessTools.Method(typeof(MagnificationMassBoundary), "AfterMakeTire")), null, null);
			harmony.Patch(AccessTools.DeclaredMethod(typeof(WheelController), "Start"), null,
				new HarmonyMethod(AccessTools.Method(typeof(MagnificationMassBoundary), "AfterWheelStart")), null, null);
			harmony.Patch(AccessTools.DeclaredMethod(typeof(SledController), "Start"), null,
				new HarmonyMethod(AccessTools.Method(typeof(MagnificationMassBoundary), "AfterSledStart")), null, null);
			MagnificationDownscale.Log("SLED_VISUAL_SCALE_HOOK target=SledController.Start scope=model22-non100 physics=unchanged");
			MagnificationDownscale.Log("SLED_JOINT_UNITS_HOOK target=SledController.Start scope=model22-non100 source=instantiated-prefab anchors=unchanged");
			MagnificationDownscale.Log("MASS_UNITS_REGISTERED methods=" + Tokens.Length + " positionQuery=physical-mass scalePolicy=density scripts=emitted-image");
		}
		internal static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, MethodBase __originalMethod)
		{
			int count = 0;
			List<CodeInstruction> input = MagnificationDownscale.Copy(instructions);
			int legacy = 0;
			for (int i = 0; i < input.Count; i++)
			{
				CodeInstruction code = new CodeInstruction(input[i]);
				if (__originalMethod.MetadataToken == 0x06004513 && MagnificationDownscale.Field(code, OpCodes.Ldfld, typeof(MachineController), "DJCDFLDHPHK")
					&& i + 2 < input.Count && input[i + 1].opcode == OpCodes.Mul && input[i + 2].operand is MethodInfo && ((MethodInfo)input[i + 2].operand).Name == "set_mass")
				{
					code.opcode = OpCodes.Call; code.operand = AccessTools.Method(typeof(MagnificationMassBoundary), "LegacyMassScale"); legacy++;
				}
				MethodInfo call = code.operand as MethodInfo;
				if (call != null && call.DeclaringType == typeof(Rigidbody) && (call.Name == "get_mass" || call.Name == "set_mass"))
				{
					code.opcode = OpCodes.Call;
					code.operand = AccessTools.Method(typeof(MagnificationMassBoundary), call.Name == "get_mass" ? "Read" : "Write");
					count++;
				}
				yield return code;
			}
			if (count == 0) throw new InvalidOperationException("missing-mass-callsite " + __originalMethod);
			if (__originalMethod.MetadataToken == 0x06004513 && legacy != 1) throw new InvalidOperationException("remote-legacy-mass-scale-count " + legacy);
		}
        private static float LegacyMassScale(MachineController machine)
        {
            if (MagnificationPhysicsMode.IsVanilla(machine)) return machine.DJCDFLDHPHK;
            return MagnificationScalePolicy.Enabled ? 1f : machine.DJCDFLDHPHK;
        }
        internal static MachineController Owner(Rigidbody body)
        {
            if (body == null) return null;
            Entry cached;
            // Tracked ON bodies already have their native controller. Read its
            // live owner field: SyncAttach can transfer that controller to a
            // different machine, so caching only a MachineController is stale.
            if (entries.TryGetValue(body.GetInstanceID(), out cached) && ReferenceEquals(cached.Body, body)
                && cached.Owner != null && cached.Owner.CIPOPAGDJDE != null) return cached.Owner.CIPOPAGDJDE;
            BodyController owner = body.GetComponentInParent<BodyController>();
            MachineController machine = owner == null ? null : owner.CIPOPAGDJDE;
            return machine == null ? body.GetComponentInParent<MachineController>() : machine;
        }
        internal static bool IsVanilla(Rigidbody body) { return MagnificationPhysicsMode.IsVanilla(Owner(body)); }
		internal static float Scale(Rigidbody body)
		{
			Entry entry;
			return entries.TryGetValue(body.GetInstanceID(), out entry) && ReferenceEquals(body, entry.Body) ? entry.Scale : 1f;
		}
		internal static float ResolveScale(Rigidbody body)
		{
			Entry entry;
			if (entries.TryGetValue(body.GetInstanceID(), out entry) && ReferenceEquals(body, entry.Body)) return entry.Scale;
			BodyController owner = body.GetComponentInParent<BodyController>();
			MachineController machine = owner == null ? null : owner.CIPOPAGDJDE;
			if (machine == null) machine = body.GetComponentInParent<MachineController>();
			return MagnificationPhysicsMode.PhysicsScale(machine);
		}
		internal static bool Bypass(Rigidbody body)
		{
            if (IsVanilla(body)) return true;
			// Older diagnostic models retain their existing lookup/write behavior.
			return MagnificationScalePolicy.SameTime && MagnificationScalePolicy.Bypass(ResolveScale(body));
		}
		internal static bool IsTracked(Rigidbody body)
		{
			Entry entry;
			return entries.TryGetValue(body.GetInstanceID(), out entry) && ReferenceEquals(body, entry.Body);
		}
		internal static float Factor(Rigidbody body)
		{
			Entry entry;
			return entries.TryGetValue(body.GetInstanceID(), out entry) && ReferenceEquals(body, entry.Body) ? entry.Factor : 1f;
		}
		internal static float Read(Rigidbody body)
		{
			if (Bypass(body)) return body.mass;
			return body.mass / Factor(body);
		}
		internal static void Write(Rigidbody body, float value)
		{
			if (Bypass(body)) { body.mass = value; return; }
			Entry current;
			if (MagnificationScalePolicy.Enabled && (!entries.TryGetValue(body.GetInstanceID(), out current) || !ReferenceEquals(current.Body, body)))
			{
				BodyController owner = body.GetComponentInParent<BodyController>();
				MachineController machine = owner == null ? body.GetComponentInParent<MachineController>() : owner.CIPOPAGDJDE;
				if (machine != null)
				{
					float scale = MagnificationPhysicsMode.PhysicsScale(machine);
                    entries[body.GetInstanceID()] = new Entry { Body = body, Owner = owner, Scale = scale, Factor = MagnificationScalePolicy.Mass(scale) };
				}
			}
			float physical = value * Factor(body);
			if (MagnificationScalePolicy.SameTime && physical > 0f && physical < 1e-7f) MagnificationJointMassNative.WriteSmallMass(body, physical);
			else body.mass = physical;
		}
		internal static void Track(Rigidbody body, float scale)
		{
            if (IsVanilla(body)) return;
			if (!MagnificationScalePolicy.Enabled || MagnificationScalePolicy.Bypass(scale)) return;
			float source = Read(body), factor = MagnificationScalePolicy.Mass(scale);
			if (!(source > 0f) || float.IsInfinity(source)) throw new InvalidOperationException("non-positive-finite-mass " + body.name);
            entries[body.GetInstanceID()] = new Entry { Body = body, Owner = body.GetComponentInParent<BodyController>(), Factor = factor, Scale = scale };
			Write(body, source);
			float requested = source * factor;
			if (Math.Abs(body.mass - requested) > requested * .0001f) throw new InvalidOperationException("Unity-mass-clamp " + body.name + " requested=" + requested + " actual=" + body.mass);
			MagnificationConstraintUnits.ApplyBody(body, scale);
			MagnificationGravitySimilarity.Track(body, scale);
			if (logged.Add(body.GetInstanceID())) MagnificationDownscale.Log("MASS_UNITS_BODY body=" + body.name + " source=" + F(source) + " factor=" + F(factor) + " physical=" + F(body.mass)
				+ " sleepThreshold=" + F(body.sleepThreshold) + " maxDepenetrationVelocity=" + F(body.maxDepenetrationVelocity));
		}
		private static void AfterMakeTire(WheelController __instance)
		{
            if (MagnificationPhysicsMode.IsVanilla(__instance.FPEBEEJGFPI)) return;
			if (!MagnificationScalePolicy.Enabled || __instance.EKHOCFCEGCB == null) return;
			float scale = MagnificationPhysicsMode.PhysicsScale(__instance.FPEBEEJGFPI);
			if (MagnificationScalePolicy.Bypass(scale)) return;
			TrackPhysics(__instance.EKHOCFCEGCB, scale);
		}
		private static void AfterWheelStart(WheelController __instance)
		{
            if (MagnificationPhysicsMode.IsVanilla(__instance.FPEBEEJGFPI)) return;
			if (!MagnificationScalePolicy.Enabled || __instance.EKHOCFCEGCB == null) return;
			Rigidbody body = __instance.EKHOCFCEGCB;
			if (MagnificationScalePolicy.Bypass(MagnificationPhysicsMode.PhysicsScale(__instance.FPEBEEJGFPI))) return;
			Vector3 source = MagnificationInertiaBoundary.LastRequested(body);
			float factor = MagnificationScalePolicy.Inertia(MagnificationPhysicsMode.PhysicsScale(__instance.FPEBEEJGFPI));
			MagnificationDownscale.Log("TIRE_INERTIA_LIFECYCLE body=" + body.name + " requested=" + (source * factor).ToString("R") + " afterGeometry=" + body.inertiaTensor.ToString("R"));
			MagnificationInertiaBoundary.TrackSource(body, factor, source);
            MagnificationLatePhysics.FinalizeBody(body, MagnificationPhysicsMode.PhysicsScale(__instance.FPEBEEJGFPI), "Wheel.Start");
		}
		internal static Rigidbody SledBody(SledController sled) { return (Rigidbody)sledBody.GetValue(sled); }
		private static void AfterSledStart(SledController __instance)
		{
			Rigidbody body = SledBody(__instance);
			if (!MagnificationScalePolicy.Enabled || body == null) return;
			float visualScale = MagnificationPhysicsNormalization.MachineScale(__instance.FPEBEEJGFPI);
			MagnificationSledVisualUnits.Apply(__instance, visualScale);
            if (MagnificationPhysicsMode.IsVanilla(__instance.FPEBEEJGFPI)) return;
			float s = MagnificationPhysicsMode.PhysicsScale(__instance.FPEBEEJGFPI);
			if (MagnificationScalePolicy.Bypass(s)) return;
			Track(body, s);
            MagnificationSledJointUnits.Apply(body, s);
            MagnificationConstraintUnits.ApplyHierarchy(body.gameObject, s, "Sled.Start");
			// Native FixedUpdate writes its command-dependent tensor before the
			// physics step. Do not reinterpret the prefab's automatic tensor.
			MagnificationDownscale.Log("SLED_UNITS_CREATED mass=" + body.mass + " inertia=next-native-FixedUpdate speedLimit=" + body.maxDepenetrationVelocity);
		}
		internal static void TrackPhysics(Rigidbody body, float scale)
		{
            if (IsVanilla(body)) return;
			if (MagnificationScalePolicy.Bypass(scale)) return;
			Vector3 source = MagnificationInertiaBoundary.LastRequested(body);
			Track(body, scale);
			MagnificationInertiaBoundary.TrackSource(body, MagnificationScalePolicy.Inertia(scale), source);
		}
		internal static void Clear() { entries.Clear(); logged.Clear(); MagnificationSledJointUnits.Clear(); ScriptReads = ScriptWrites = 0; }
		private static string F(float value) { return value.ToString("R", CultureInfo.InvariantCulture); }
	}
}
