using System;
using System.Collections;
using System.IO;
using System.Reflection;
using HarmonyLib;
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
		internal static byte[] Rewrite(string path, bool probeInput)
		{
			Assembly cecil = typeof(Harmony).Assembly;
			Type moduleType = cecil.GetType("Mono.Cecil.ModuleDefinition", true);
			object module = moduleType.GetMethod("ReadModule", Flags, null, new[] { typeof(string) }, null).Invoke(null, new object[] { path });
			int reads = 0, writes = 0, inputs = 0;
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
							if (opcode != "Call" && opcode != "Callvirt") continue;
							object operand = Get(instruction, "Operand");
							if (operand == null || operand.GetType().GetProperty("DeclaringType", Flags) == null || operand.GetType().GetProperty("Parameters", Flags) == null) continue;
							string owner = (string)Get(Get(operand, "DeclaringType"), "FullName"), name = (string)Get(operand, "Name"), replacement = null;
							if (owner == "UnityEngine.Rigidbody")
							{
								if (name == "get_inertiaTensor") { replacement = "ReadInertia"; reads++; }
								if (name == "set_inertiaTensor") { replacement = "WriteInertia"; writes++; }
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
							Set(instruction, "OpCode", callOpcode);
							Set(instruction, "Operand", import.Invoke(module, new object[] { bridge }));
						}
					}
				using (MemoryStream output = new MemoryStream())
				{
					moduleType.GetMethod("Write", Flags, null, new[] { typeof(Stream) }, null).Invoke(module, new object[] { output });
					MagnificationDownscale.Log("CRAFT_SCRIPT_IMAGE_REWRITTEN inertiaReads=" + reads + " inertiaWrites=" + writes + " inputCalls=" + inputs + " sourceFiles=unchanged");
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
		internal static void ResetCounters() { InertiaReads = InertiaWrites = KeyReads = 0; }
		public static Vector3 ReadInertia(Rigidbody body) { InertiaReads++; return MagnificationInertiaBoundary.Read(body); }
		public static void WriteInertia(Rigidbody body, Vector3 value) { InertiaWrites++; MagnificationInertiaBoundary.Write(body, value); }
		public static bool ProbeKey(KeyCode key) { KeyReads++; return MagnificationCraftProbe.ScriptReadKey(key); }
		public static bool ProbeKeyEdge(KeyCode key) { return MagnificationCraftProbe.ScriptReadKeyEdge(key); }
		public static float ProbeAxis(string name) { return MagnificationCraftProbe.ScriptReadAxis(name); }
	}
}
