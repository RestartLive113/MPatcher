using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using HarmonyLib;
using McnCraft;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
	// Research observer only. Every prefix is void; no argument, return value,
	// Rigidbody, controller state, or native protection is changed.
	internal static class MagnificationSafetyObserver
	{
		private sealed class State
		{
			internal MachineController Machine;
			internal long Ticks, Stops, StopsOne, StopsTen, StopsOther, BodyStops, ClearingTicks;
			internal int Details, TickDetails, AggregateDetails, BeforeStop;
			internal float BeforeVelocity;
		}
		private sealed class BodySample { internal BodyController Body; internal Vector3 Previous; }
		private static readonly Dictionary<int, State> machines = new Dictionary<int, State>();
		private static readonly Dictionary<int, BodySample> bodies = new Dictionary<int, BodySample>();
		private static BodyController executingBody;
		private static readonly FieldInfo stop = AccessTools.Field(typeof(MachineController), "EPDHAAIIKCF");
		private static readonly FieldInfo score = AccessTools.Field(typeof(BodyController), "BIMHBCEKGFN");
		private static readonly FieldInfo previous = AccessTools.Field(typeof(BodyController), "KCAHKBMGNDI");
		private static readonly FieldInfo exclude = AccessTools.Field(typeof(BodyController), "JPCNPLMHCGB");
		private static readonly FieldInfo hardStop = AccessTools.Field(typeof(MachineController), "PNIEOHABEEM");
		private static readonly FieldInfo pistonImpulse = AccessTools.Field(typeof(MachineController), "FELFCAMFKAK");
		internal static void Register(Harmony harmony)
		{
			foreach (FieldInfo field in new[] { stop, score, previous, exclude, hardStop, pistonImpulse })
				if (field == null) throw new MissingFieldException("safety-observer-native-field");
			Patch(harmony, typeof(MachineController), "StopMovement", "BeforeStop", null);
			Patch(harmony, typeof(MachineController), "FixedUpdate", "BeforeMachine", "AfterMachine");
			Patch(harmony, typeof(BodyController), "FixedUpdate", "BeforeBody", "AfterBody");
			Log("SAFETY_OBSERVER_REGISTERED behavior=read-only stopDetailLimit=12 tickDetailLimit=12 aggregatePeriod=500 baseline100=unchanged");
		}
		private static void Patch(Harmony harmony, Type type, string name, string prefix, string postfix)
		{
			MethodInfo target = AccessTools.DeclaredMethod(type, name);
			int expected = name == "StopMovement" ? 0x06004154 : type == typeof(MachineController) ? 0x060041C8 : 0x06002613;
			if (target == null || target.MetadataToken != expected) throw new MissingMethodException("safety-observer-target " + type.FullName + "." + name);
			harmony.Patch(target, new HarmonyMethod(AccessTools.Method(typeof(MagnificationSafetyObserver), prefix)),
				postfix == null ? null : new HarmonyMethod(AccessTools.Method(typeof(MagnificationSafetyObserver), postfix)), null, null);
		}
		private static State Get(MachineController machine)
		{
			int id = machine.GetInstanceID(); State state;
			if (!machines.TryGetValue(id, out state) || !ReferenceEquals(state.Machine, machine))
				machines[id] = state = new State { Machine = machine };
			return state;
		}
		private static int StopTicks(MachineController machine) { return (int)stop.GetValue(machine); }
		private static void BeforeBody(BodyController __instance, out BodyController __state)
		{
			__state = executingBody;
			executingBody = __instance;
			int id = __instance.GetInstanceID(); BodySample sample;
			if (!bodies.TryGetValue(id, out sample) || !ReferenceEquals(sample.Body, __instance))
				bodies[id] = sample = new BodySample { Body = __instance };
			// Native code overwrites KCAHKBMGNDI before calling StopMovement.
			sample.Previous = (Vector3)previous.GetValue(__instance);
		}
		private static void AfterBody(BodyController __state) { executingBody = __state; }
		private static void BeforeMachine(MachineController __instance)
		{
			State state = Get(__instance); state.Ticks++;
			state.BeforeStop = StopTicks(__instance);
			if (state.BeforeStop > 0) state.ClearingTicks++;
			if (state.TickDetails < 12) state.BeforeVelocity = VelocityMax(__instance);
		}
		private static void AfterMachine(MachineController __instance)
		{
			State state = Get(__instance); int after = StopTicks(__instance);
			if (state.TickDetails < 12 && (state.BeforeStop > 0 || after > 0 || state.Ticks == 1))
			{
				state.TickDetails++;
				Log("SAFETY_TICK machine=" + __instance.GetInstanceID() + " scale=" + F(__instance.DJCDFLDHPHK)
					+ " tick=" + state.Ticks + " stopBefore=" + state.BeforeStop + " stopAfter=" + after
					+ " hardStop=" + hardStop.GetValue(__instance) + " velocityMaxBefore=" + F(state.BeforeVelocity)
					+ " velocityMaxAfter=" + F(VelocityMax(__instance)) + " bodies=" + Snapshot(__instance));
			}
			if (state.Ticks % 500 == 0 && state.AggregateDetails < 24) { state.AggregateDetails++; Aggregate(state, "periodic"); }
		}
		private static void BeforeStop(MachineController __instance, int __0)
		{
			State state = Get(__instance); state.Stops++;
			if (__0 == 1) state.StopsOne++; else if (__0 == 10) state.StopsTen++; else state.StopsOther++;
			bool bodyCaller = executingBody != null && (ReferenceEquals(executingBody, __instance) || ReferenceEquals(executingBody.CIPOPAGDJDE, __instance));
			if (bodyCaller) state.BodyStops++;
			if (state.Details >= 12) return;
			state.Details++;
			Log("SAFETY_STOP machine=" + __instance.GetInstanceID() + " scale=" + F(__instance.DJCDFLDHPHK)
				+ " tick=" + state.Ticks + " requestedTicks=" + __0 + " counterBefore=" + StopTicks(__instance)
				+ " bodyContext=" + bodyCaller + " body=" + (bodyCaller ? Describe(executingBody) : "none")
				+ " pistonDot=" + F(Vector3.Dot((Vector3)pistonImpulse.GetValue(__instance), __instance.NFMPBACKJOJ.velocity))
				+ " sourceSpeed=" + F(__instance.KDCAPPINCMF / MagnificationPhysicsNormalization.MachineScale(__instance))
				+ " stack=" + CallerStack());
		}
		private static string Describe(BodyController body)
		{
			Rigidbody rb = body.NFMPBACKJOJ;
			if (rb == null) return body.GetInstanceID() + ":no-rigidbody";
			BodySample sample; bool sampled = bodies.TryGetValue(body.GetInstanceID(), out sample) && ReferenceEquals(sample.Body, body);
			Vector3 prior = sampled ? sample.Previous : (Vector3)previous.GetValue(body);
			Vector3 current = rb.angularVelocity;
			return body.GetInstanceID() + ":" + body.name + ":score=" + F((float)score.GetValue(body))
				+ ":excluded=" + exclude.GetValue(body) + ":velocity=" + rb.velocity.ToString("R")
				+ ":angular=" + current.ToString("R") + ":prior=" + prior.ToString("R")
				+ ":dot=" + F(Vector3.Dot(current, prior)) + ":priorSampled=" + sampled;
		}
		private static string Snapshot(MachineController machine)
		{
			BodyController worst = null; float maximum = float.NegativeInfinity;
			int count = 0, sleeping = 0, kinematic = 0;
			foreach (BodyController body in machine.ILBAAENKMBL) if (body != null && body.NFMPBACKJOJ != null)
			{
				count++; if (body.NFMPBACKJOJ.IsSleeping()) sleeping++; if (body.NFMPBACKJOJ.isKinematic) kinematic++;
				float value = (float)score.GetValue(body); if (value > maximum) { maximum = value; worst = body; }
			}
			return count + ",sleeping=" + sleeping + ",kinematic=" + kinematic + ",worst=" + (worst == null ? "none" : Describe(worst));
		}
		private static float VelocityMax(MachineController machine)
		{
			float maximum = 0f;
			foreach (BodyController body in machine.ILBAAENKMBL) if (body != null && body.NFMPBACKJOJ != null) maximum = Mathf.Max(maximum, body.NFMPBACKJOJ.velocity.magnitude);
			return maximum;
		}
		private static string CallerStack()
		{
			StackFrame[] frames = new StackTrace(1, false).GetFrames();
			if (frames == null) return "none";
			List<string> names = new List<string>();
			foreach (StackFrame frame in frames)
			{
				MethodBase method = frame.GetMethod(); if (method == null || method.DeclaringType == typeof(MagnificationSafetyObserver)) continue;
				names.Add((method.DeclaringType == null ? "" : method.DeclaringType.Name + ".") + method.Name);
				if (names.Count == 5) break;
			}
			return string.Join("|", names.ToArray());
		}
		private static void Aggregate(State state, string reason)
		{
			Log("SAFETY_AGGREGATE machine=" + (state.Machine == null ? "destroyed" : state.Machine.GetInstanceID().ToString())
				+ " reason=" + reason + " ticks=" + state.Ticks + " stopCalls=" + state.Stops + " oneTick=" + state.StopsOne
				+ " tenTicks=" + state.StopsTen + " otherTicks=" + state.StopsOther + " bodyContextCalls=" + state.BodyStops
				+ " clearingTicks=" + state.ClearingTicks);
		}
		internal static void Flush() { foreach (State state in machines.Values) Aggregate(state, "flush"); }
		private static string F(float value) { return value.ToString("R", CultureInfo.InvariantCulture); }
		private static void Log(string message) { MagnificationDownscale.Log(message); }
	}
}
