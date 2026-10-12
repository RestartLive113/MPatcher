using System;
using System.Collections;
using System.IO;
using System.Reflection;
using HarmonyLib;
using McnCraft;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
	// Cecil is already bundled privately in this game's Harmony. Rewrite the
	// emitted image before loading, avoiding Mono's unsupported dynamic-method IL
	// reader and struct-return detours. Script source and source locations survive.
	internal static class MagnificationScriptImage
	{
		private const BindingFlags Flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
		private static object Get(object value, string name) { return value.GetType().GetProperty(name, Flags).GetValue(value, null); }
		private static void Set(object value, string name, object next) { value.GetType().GetProperty(name, Flags).SetValue(value, next, null); }
		private static string FeedbackReplacement(object operand)
		{
			object declaring = Get(operand, "DeclaringType"), scope = Get(declaring, "Scope");
			// A same-named user type or another overload is not this native API.
			if ((string)Get(declaring, "FullName") != "AutoPilot" || scope == null
				|| scope.GetType().Name != "AssemblyNameReference"
				|| (string)Get(scope, "Name") != typeof(AutoPilot).Assembly.GetName().Name
				|| !(bool)Get(operand, "HasThis") || ((IList)Get(operand, "Parameters")).Count != 0
				|| (bool)Get(operand, "HasGenericParameters")) return null;
			string name = (string)Get(operand, "Name"), result = (string)Get(Get(operand, "ReturnType"), "FullName");
			return name == "GetSpeed" && result == "System.Single" ? "ReadScriptSpeed"
				: name == "GetVelocity" && result == "UnityEngine.Vector3" ? "ReadScriptVelocity" : null;
		}
		internal static byte[] Rewrite(string path, bool probeInput)
		{
			return RewriteCore(path, probeInput, false);
		}
		internal static byte[] RewriteInputOnly(string path) { return RewriteCore(path, true, true); }
		private static byte[] RewriteCore(string path, bool probeInput, bool inputOnly)
		{
			Assembly cecil = typeof(Harmony).Assembly;
			Type moduleType = cecil.GetType("Mono.Cecil.ModuleDefinition", true);
			object module = moduleType.GetMethod("ReadModule", Flags, null, new[] { typeof(string) }, null).Invoke(null, new object[] { path });
			int reads = 0, writes = 0, inputs = 0, jointCalls = 0, inputDelegates = 0, feedbackSpeed = 0, feedbackVelocity = 0;
			try
			{
				// Mono may unify a byte[] load with the existing AssemblyBuilder by
				// assembly identity. Give the rewritten image its own identity.
				object identity = Get(Get(module, "Assembly"), "Name");
				Set(identity, "Name", (string)Get(identity, "Name") + "_MPatcher");
				MethodInfo import = moduleType.GetMethod("ImportReference", Flags, null, new[] { typeof(MethodBase) }, null);
				object callOpcode = cecil.GetType("Mono.Cecil.Cil.OpCodes", true).GetField("Call", Flags).GetValue(null);
				foreach (object type in (IEnumerable)moduleType.GetMethod("GetTypes", Flags, null, Type.EmptyTypes, null).Invoke(module, null))
					foreach (object method in (IEnumerable)Get(type, "Methods"))
					{
						if (!(bool)Get(method, "HasBody")) continue;
						foreach (object instruction in (IEnumerable)Get(Get(method, "Body"), "Instructions"))
						{
							string opcode = Get(Get(instruction, "OpCode"), "Code").ToString();
							bool delegateTarget = opcode == "Ldftn";
							if (opcode != "Call" && opcode != "Callvirt" && !delegateTarget) continue;
							object operand = Get(instruction, "Operand");
							if (operand == null || operand.GetType().GetProperty("DeclaringType", Flags) == null || operand.GetType().GetProperty("Parameters", Flags) == null) continue;
							string owner = (string)Get(Get(operand, "DeclaringType"), "FullName"), name = (string)Get(operand, "Name"), replacement = null;
							if (inputOnly && owner != "UnityEngine.Input") continue;
							// Static Input delegates have exactly the same signature as
							// the probe bridge. Keep ldftn; replacing it with call would
							// corrupt delegate construction. Instance physics delegates
							// need a separate open/closed delegate policy and stay native.
							if (delegateTarget && (!probeInput || owner != "UnityEngine.Input" || (bool)Get(operand, "HasThis"))) continue;
							if (owner == "UnityEngine.Rigidbody")
							{
								if (name == "get_inertiaTensor") { replacement = "ReadInertia"; reads++; }
								if (name == "set_inertiaTensor") { replacement = "WriteInertia"; writes++; }
								if (name == "get_mass") replacement = "ReadMass";
								if (name == "set_mass") replacement = "WriteMass";
							}
							else if (owner == "UnityEngine.HingeJoint")
							{
								replacement = name == "get_motor" ? "ReadHingeMotor" : name == "set_motor" ? "WriteHingeMotor"
									: name == "get_spring" ? "ReadHingeSpring" : name == "set_spring" ? "WriteHingeSpring" : null;
								if (replacement != null) jointCalls++;
							}
							else if (owner == "AutoPilot")
							{
								replacement = FeedbackReplacement(operand);
								if (replacement == "ReadScriptSpeed") feedbackSpeed++;
								else if (replacement == "ReadScriptVelocity") feedbackVelocity++;
							}
							else if (probeInput && owner == "UnityEngine.Input")
							{
								IList parameters = (IList)Get(operand, "Parameters");
								if (parameters.Count != 1) continue;
								string parameter = (string)Get(Get(parameters[0], "ParameterType"), "FullName");
								if (parameter == "UnityEngine.KeyCode") replacement = name == "GetKey" ? "ProbeKey" : name == "GetKeyDown" || name == "GetKeyUp" ? "ProbeKeyEdge" : null;
								if (parameter == "System.String" && (name == "GetAxis" || name == "GetAxisRaw")) replacement = "ProbeAxis";
								if (replacement != null) inputs++;
							}
							if (replacement == null) continue;
							MethodInfo bridge = typeof(MagnificationManagedBridge).GetMethod(replacement, Flags);
							if (delegateTarget) inputDelegates++;
							else Set(instruction, "OpCode", callOpcode);
							Set(instruction, "Operand", import.Invoke(module, new object[] { bridge }));
						}
					}
				using (MemoryStream output = new MemoryStream())
				{
					moduleType.GetMethod("Write", Flags, null, new[] { typeof(Stream) }, null).Invoke(module, new object[] { output });
						MagnificationDownscale.Log("CRAFT_SCRIPT_IMAGE_REWRITTEN inertiaReads=" + reads + " inertiaWrites=" + writes + " inputCalls=" + inputs + " inputDelegates=" + inputDelegates + " hingeCalls=" + jointCalls
							+ " feedbackSpeedCalls=" + feedbackSpeed + " feedbackVelocityCalls=" + feedbackVelocity + " feedbackActive=model22 scriptHud=source-units sourceFiles=unchanged inputOnly=" + inputOnly);
					return output.ToArray();
				}
			}
			finally { ((IDisposable)module).Dispose(); }
		}
	}

	// Must be public because emitted script assemblies call these methods.
	public static class MagnificationManagedBridge
	{
		internal static int InertiaReads, InertiaWrites, KeyReads;
		internal static int FeedbackSpeedReads, FeedbackVelocityReads;
		private static bool speedLogged, velocityLogged;
		private static readonly FieldInfo autoPilotMachine = AccessTools.Field(typeof(AutoPilot), "machineScr");
		internal static void ResetCounters()
		{
			InertiaReads = InertiaWrites = KeyReads = FeedbackSpeedReads = FeedbackVelocityReads = 0;
			speedLogged = velocityLogged = false;
			MagnificationScriptJointUnits.Clear();
		}
		private static float FeedbackScale(AutoPilot owner)
		{
			if (autoPilotMachine == null) throw new MissingFieldException("AutoPilot", "machineScr");
			return MagnificationPhysicsMode.PhysicsScale((MachineController)autoPilotMachine.GetValue(owner));
		}
        private static bool IsVanilla(AutoPilot owner)
        {
            if (autoPilotMachine == null) throw new MissingFieldException("AutoPilot", "machineScr");
            return MagnificationPhysicsMode.IsVanilla((MachineController)autoPilotMachine.GetValue(owner));
        }
		// Calls only from rewritten scripts. Native AI, world positions/raycasts
		// and the game's speed display retain world units. A script that uses its
		// control speed for a HUD (such as JZZ30) also displays source-craft speed.
		public static float ReadScriptSpeed(AutoPilot owner)
		{
			float world = owner.GetSpeed();
            if (IsVanilla(owner)) return world;
			FeedbackSpeedReads++;
			if (!MagnificationScalePolicy.SameTime) return world;
			float scale = FeedbackScale(owner);
			if (scale == 1f) return world;
			if (!speedLogged)
			{
				speedLogged = true;
				MagnificationDownscale.Log("SCRIPT_FEEDBACK_UNITS api=AutoPilot.GetSpeed scale=" + scale + " returned=source-speed scriptHud=source-units");
			}
			return MagnificationVelocityUnits.ToCraftSpeed(world, scale, true);
		}
		public static Vector3 ReadScriptVelocity(AutoPilot owner)
		{
			Vector3 world = owner.GetVelocity();
            if (IsVanilla(owner)) return world;
			FeedbackVelocityReads++;
			if (!MagnificationScalePolicy.SameTime) return world;
			float scale = FeedbackScale(owner);
			if (scale == 1f) return world;
			if (!velocityLogged)
			{
				velocityLogged = true;
				MagnificationDownscale.Log("SCRIPT_FEEDBACK_UNITS api=AutoPilot.GetVelocity scale=" + scale + " returned=source-velocity positions=world");
			}
			return MagnificationVelocityUnits.ToCraftVelocity(world, scale, true);
		}
		public static Vector3 ReadInertia(Rigidbody body) { InertiaReads++; return MagnificationInertiaBoundary.Read(body); }
		public static float ReadMass(Rigidbody body) { MagnificationMassBoundary.ScriptReads++; return MagnificationMassBoundary.Read(body); }
		public static void WriteMass(Rigidbody body, float value) { MagnificationMassBoundary.ScriptWrites++; MagnificationMassBoundary.Write(body, value); }
		public static void WriteInertia(Rigidbody body, Vector3 value) { InertiaWrites++; MagnificationInertiaBoundary.Write(body, value); }
		public static JointMotor ReadHingeMotor(HingeJoint joint) { return MagnificationScriptJointUnits.ReadMotor(joint); }
		public static void WriteHingeMotor(HingeJoint joint, JointMotor value) { MagnificationScriptJointUnits.WriteMotor(joint, value); }
		public static JointSpring ReadHingeSpring(HingeJoint joint) { return MagnificationScriptJointUnits.ReadSpring(joint); }
		public static void WriteHingeSpring(HingeJoint joint, JointSpring value) { MagnificationScriptJointUnits.WriteSpring(joint, value); }
		public static bool ProbeKey(KeyCode key) { KeyReads++; return MagnificationCraftProbe.ScriptReadKey(key); }
		public static bool ProbeKeyEdge(KeyCode key) { return MagnificationCraftProbe.ScriptReadKeyEdge(key); }
		public static float ProbeAxis(string name) { return MagnificationCraftProbe.ScriptReadAxis(name); }
	}
}
