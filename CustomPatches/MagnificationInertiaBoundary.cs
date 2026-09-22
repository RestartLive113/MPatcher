using System;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using McnCraft;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
	// Opt-in experiment. Convert at managed CALL SITES, never detour Unity's
	// struct-return getter (unsupported by the shipped Harmony/Mono ABI).
	internal static class MagnificationInertiaBoundary
	{
		private sealed class Entry { internal Rigidbody Body; internal float Factor; }
		private static readonly Dictionary<int, Entry> entries = new Dictionary<int, Entry>();
		private static readonly HashSet<MethodBase> patched = new HashSet<MethodBase>();
		// Canonical closure from hash-matched Assembly-CSharp, not obfuscator decoys.
		private static readonly HashSet<int> gameCallsites = new HashSet<int> { 0x0600260F, 0x06002613, 0x06002641, 0x0600266C, 0x0600268F,
			0x06004119, 0x06004123, 0x06004142, 0x060041C8, 0x060041DB, 0x060041F1, 0x06004513, 0x0600451E, 0x0600559D, 0x06005655, 0x06005D80, 0x06005D94 };
		private static readonly Dictionary<short, OpCode> opcodes = MakeOpcodes();
		private static Harmony harmony;
		private static Dictionary<short, OpCode> MakeOpcodes()
		{
			var result = new Dictionary<short, OpCode>();
			foreach (FieldInfo field in typeof(OpCodes).GetFields(BindingFlags.Public | BindingFlags.Static))
				if (field.FieldType == typeof(OpCode)) { OpCode code = (OpCode)field.GetValue(null); result[code.Value] = code; }
			return result;
		}
		internal static void Register(Harmony value)
		{
			harmony = value;
			foreach (Type type in new[] { typeof(BodyController), typeof(MachineController), typeof(MachineSerializer), typeof(WheelController), typeof(ShaftController), typeof(SledController) }) PatchType(type);
			if (patched.Count != gameCallsites.Count) throw new InvalidOperationException("inertia-native-callsite-count " + patched.Count);
			Type iterator = AccessTools.Inner(typeof(AutoPilot), "<StartMCS>c__Iterator0");
			MethodInfo factory = iterator == null ? null : AccessTools.Method(iterator, "FLHFABLHLNJ", new[] { typeof(Type) });
			if (factory == null) throw new MissingMethodException("AutoPilot script factory");
			harmony.Patch(factory, new HarmonyMethod(AccessTools.Method(typeof(MagnificationInertiaBoundary), "BeforeScriptFactory")), null, null, null);
			MethodInfo compiler = AccessTools.Method(typeof(CSharpCompiler.AMDKKOGEGJL), "CompileAssemblyFromFileBatch", new[] { typeof(CompilerParameters), typeof(string[]) });
			harmony.Patch(compiler, new HarmonyMethod(AccessTools.Method(typeof(MagnificationInertiaBoundary), "BeforeCompile")), new HarmonyMethod(AccessTools.Method(typeof(MagnificationInertiaBoundary), "AfterCompile")), null, null);
			MagnificationDownscale.Log("CRAFT_INERTIA_CALLSITES_READY methods=" + patched.Count + " scriptFactory=before-AddComponent");
		}
		private sealed class Compilation { internal string Path, OriginalPath; internal bool InMemory; }
		private static void BeforeCompile(CompilerParameters __0, out Compilation __state)
		{
			string directory = Path.GetFullPath(Path.Combine(Application.dataPath, "../Work/MPatcherCraftProbe"));
			Directory.CreateDirectory(directory);
			__state = new Compilation { Path = Path.Combine(directory, "ProbeScript_" + Guid.NewGuid().ToString("N") + ".dll"), OriginalPath = __0.OutputAssembly, InMemory = __0.GenerateInMemory };
			__0.OutputAssembly = __state.Path;
			__0.GenerateInMemory = false;
		}
		private static void AfterCompile(CompilerParameters __0, CompilerResults __result, Compilation __state)
		{
			__0.OutputAssembly = __state.OriginalPath; __0.GenerateInMemory = __state.InMemory;
			if (__result == null || __result.Errors.HasErrors || !File.Exists(__state.Path)) return;
			// Mono 2.x crashes in GetMethodBody on live AssemblyBuilder methods.
			// Reload the same emitted image before any script instance is created.
			byte[] image = MagnificationScriptImage.Rewrite(__state.Path, true);
			File.WriteAllBytes(__state.Path + ".rewritten", image);
			__result.CompiledAssembly = Assembly.Load(image);
			MagnificationDownscale.Log("CRAFT_SCRIPT_IMAGE_RELOADED file=" + Path.GetFileName(__state.Path) + " sourceFiles=unchanged");
		}
		private static void BeforeScriptFactory(Type __0)
		{
			if (__0.Assembly is AssemblyBuilder) throw new NotSupportedException("live-script-AssemblyBuilder-cannot-read-IL");
			MagnificationDownscale.Log("CRAFT_SCRIPT_FACTORY type=" + __0.FullName + " image=rewritten-before-load");
		}
		private static void PatchType(Type type)
		{
			foreach (MethodInfo method in type.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly))
			{
				if (type.Assembly == typeof(MachineController).Assembly && !gameCallsites.Contains(method.MetadataToken)) continue;
				MethodImplAttributes implementation = method.GetMethodImplementationFlags();
				if (patched.Contains(method) || method.IsGenericMethod || method.IsAbstract
					|| (method.Attributes & MethodAttributes.PinvokeImpl) != 0
					|| (implementation & MethodImplAttributes.CodeTypeMask) != MethodImplAttributes.IL
					|| (implementation & (MethodImplAttributes.InternalCall | MethodImplAttributes.Unmanaged)) != 0) continue;
				if (type.Assembly != typeof(MachineController).Assembly) MagnificationDownscale.Log("CRAFT_SCRIPT_SCAN method=" + type.FullName + "." + method.Name);
				if (method.GetMethodBody() == null || !UsesInertia(method)) continue;
				if (method.ReturnType.IsValueType && !method.ReturnType.IsPrimitive && !method.ReturnType.IsEnum && method.ReturnType != typeof(void))
					throw new NotSupportedException("inertia-callsite-struct-return " + method);
				harmony.Patch(method, null, null, new HarmonyMethod(AccessTools.Method(typeof(MagnificationInertiaBoundary), "Transpiler")), null);
				patched.Add(method);
				MagnificationDownscale.Log("CRAFT_INERTIA_CALLSITE method=" + type.FullName + "." + method.Name);
			}
			foreach (Type child in type.GetNestedTypes(BindingFlags.Public | BindingFlags.NonPublic)) PatchType(child);
		}
		private static bool UsesInertia(MethodInfo method)
		{
			byte[] bytes = method.GetMethodBody().GetILAsByteArray();
			for (int index = 0; index < bytes.Length;)
			{
				short key = bytes[index++];
				if (key == 0xfe) key = (short)(0xfe00 | bytes[index++]);
				OpCode op = opcodes[key];
				if (op.OperandType == OperandType.InlineMethod)
				{
					MethodBase call = method.Module.ResolveMethod(BitConverter.ToInt32(bytes, index));
					if (call.DeclaringType == typeof(Rigidbody) && (call.Name == "get_inertiaTensor" || call.Name == "set_inertiaTensor")) return true;
					if (method.DeclaringType.Assembly != typeof(MachineController).Assembly && InputReplacement(call) != null) return true;
				}
				switch (op.OperandType)
				{
					case OperandType.InlineNone: break;
					case OperandType.ShortInlineBrTarget: case OperandType.ShortInlineI: case OperandType.ShortInlineVar: index++; break;
					case OperandType.InlineVar: index += 2; break;
					case OperandType.InlineI8: case OperandType.InlineR: index += 8; break;
					case OperandType.InlineSwitch: index += 4 + BitConverter.ToInt32(bytes, index) * 4; break;
					default: index += 4; break;
				}
			}
			return false;
		}
		private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
		{
			foreach (CodeInstruction source in instructions)
			{
				CodeInstruction code = new CodeInstruction(source);
				MethodInfo call = code.operand as MethodInfo;
				if (call != null && call.DeclaringType == typeof(Rigidbody) && (call.Name == "get_inertiaTensor" || call.Name == "set_inertiaTensor"))
				{
					code.opcode = OpCodes.Call;
					code.operand = AccessTools.Method(typeof(MagnificationInertiaBoundary), call.Name == "get_inertiaTensor" ? "Read" : "Write");
				}
				else if (call != null && InputReplacement(call) != null)
				{
					code.opcode = OpCodes.Call;
					code.operand = AccessTools.Method(typeof(MagnificationCraftProbe), InputReplacement(call));
				}
				yield return code;
			}
		}
		private static string InputReplacement(MethodBase call)
		{
			if (call.DeclaringType != typeof(Input) || call.GetParameters().Length != 1) return null;
			if ((call.Name == "GetAxis" || call.Name == "GetAxisRaw") && call.GetParameters()[0].ParameterType == typeof(string)) return "ScriptReadAxis";
			if (call.GetParameters()[0].ParameterType != typeof(KeyCode)) return null;
			return call.Name == "GetKey" ? "ScriptReadKey" : call.Name == "GetKeyDown" || call.Name == "GetKeyUp" ? "ScriptReadKeyEdge" : null;
		}
		private static float Factor(Rigidbody body)
		{
			Entry entry;
			return entries.TryGetValue(body.GetInstanceID(), out entry) && ReferenceEquals(entry.Body, body) ? entry.Factor : 1f;
		}
		internal static Vector3 Read(Rigidbody body) { return body.inertiaTensor / Factor(body); }
		internal static void Write(Rigidbody body, Vector3 value) { body.inertiaTensor = value * Factor(body); }
		internal static Vector3 Physical(Rigidbody body) { return body.inertiaTensor; }
		internal static void Track(Rigidbody body, float factor)
		{
			Vector3 source = Read(body);
			if (!(source.x > 0f && source.y > 0f && source.z > 0f) || float.IsInfinity(source.sqrMagnitude))
			{
				MagnificationDownscale.Log("CRAFT_INERTIA_NOT_TRACKED body=" + body.name + " source=" + source.ToString("R") + " reason=no-positive-finite-tensor");
				return;
			}
			entries[body.GetInstanceID()] = new Entry { Body = body, Factor = factor };
			Write(body, source);
			CheckRoundtrip(body);
		}
		internal static void Clear() { entries.Clear(); }
		internal static void CheckRoundtrip(Rigidbody body)
		{
			Vector3 source = Read(body);
			// Helper bodies without colliders can report an automatic zero tensor.
			// Merely testing such a body must not write that invalid value back.
			if (!(source.x > 0f && source.y > 0f && source.z > 0f) || float.IsInfinity(source.sqrMagnitude)) return;
			for (int i = 0; i < 10; i++) Write(body, Read(body));
			if ((source - Read(body)).sqrMagnitude > 1e-7f || (Physical(body) - source * Factor(body)).sqrMagnitude > 1e-10f)
				throw new InvalidOperationException("inertia-boundary-repeat " + body.name);
		}
	}
}
