using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using McnCraft;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
	// Native Initialize computes bounds before restoring temporary block types,
	// serializing structure, scaling bodies and destroying construction objects.
	// Its y > 2000 filter separates the machine at y=4000 from missile templates
	// placed at world zero. Direct replacement needs that same separation by
	// hierarchy, while retaining the native bounds calculation and its timing.
	internal static class LegacyMachineNetworkAnchor
	{
		private static MachineController current;
		private static HashSet<Transform> templateRoots;
		private static int includedBlocks;
		private static int excludedBlocks;
		private static readonly Func<Transform, Transform> Parent = value => value.parent;

		internal static void Begin(MachineController machine)
		{
			Clear();
			current = machine;
		}

		internal static void Clear()
		{
			current = null;
			templateRoots = null;
			includedBlocks = 0;
			excludedBlocks = 0;
		}

		internal static bool TryCapture(MachineController machine, out int blocks, out Vector3 anchor)
		{
			blocks = includedBlocks;
			anchor = Vector3.zero;
			if (!machine || !object.ReferenceEquals(current, machine) || blocks == 0)
				return false;
			MachineSerializer serializer = machine.GetComponent<MachineSerializer>();
			if (!serializer) return false;
			anchor = serializer.JJMDFCDDJBA;
			bool finite = Finite(anchor.x) && Finite(anchor.y) && Finite(anchor.z);
			LegacyTransientReconnect.Log("MACHINE_CHANGE_ANCHOR source=native-construction-bounds"
				+ " included=" + includedBlocks + " excludedMissileBlocks=" + excludedBlocks
				+ " missileRoots=" + (templateRoots == null ? 0 : templateRoots.Count)
				+ " finite=" + finite);
			return finite;
		}

		private static bool Finite(float value) { return !float.IsNaN(value) && !float.IsInfinity(value); }

		private static bool IncludeBlock(float worldY, BlockController block, MachineController machine)
		{
			if (!object.ReferenceEquals(current, machine)) return worldY > 2000f;
			if (templateRoots == null)
			{
				templateRoots = new HashSet<Transform>();
				if (machine.EPGELCMKKOC == null || machine.EPGELCMKKOC.CDOJFIKGAMA == null)
					throw new InvalidOperationException("Native missile groups are unavailable during anchor construction.");
				foreach (int group in machine.EPGELCMKKOC.CDOJFIKGAMA)
				{
					if (group <= 0 || group >= machine.KBLANAFAJFP.Count || !machine.KBLANAFAJFP[group])
						throw new InvalidOperationException("Invalid native missile group during anchor construction: " + group);
					templateRoots.Add(machine.KBLANAFAJFP[group].transform);
				}
			}
			if (IsWithinTemplate(block.transform, templateRoots, Parent))
			{
				excludedBlocks++;
				return false;
			}
			Vector3 position = block.transform.position;
			if (!Finite(position.x) || !Finite(position.y) || !Finite(position.z))
				throw new InvalidOperationException("Non-finite native block position during anchor construction.");
			includedBlocks++;
			return true;
		}

		// Includes nested limbs and generated descendants, regardless of their
		// BlockController group numbers or the machine's world position.
		internal static bool IsWithinTemplate<T>(T node, HashSet<T> roots, Func<T, T> parent) where T : class
		{
			if (roots.Count == 0) return false;
			for (T item = node; !object.ReferenceEquals(item, null); item = parent(item))
				if (roots.Contains(item)) return true;
			return false;
		}

		private static bool LogLowConstructionHeight(float worldY, MachineController machine)
		{
			// Direct spawning below 3646 is expected; do not emit one native DP.C
			// warning for every block. Other initialization paths retain the check.
			return !object.ReferenceEquals(current, machine) && worldY < 3646f;
		}

		internal static IEnumerable<CodeInstruction> InitializeTranspiler(IEnumerable<CodeInstruction> instructions)
		{
			List<CodeInstruction> codes = new List<CodeInstruction>();
			foreach (CodeInstruction code in instructions) codes.Add(new CodeInstruction(code));
			int filter = -1, diagnostic = -1;
			for (int i = 6; i + 6 < codes.Count; i++)
			{
				if (codes[i].opcode != OpCodes.Ldc_R4) continue;
				if (Equals(codes[i].operand, 2000f))
				{
					if (filter >= 0 || !IsHeightRead(codes, i) || codes[i + 1].opcode != OpCodes.Ble_Un
						|| (codes[i - 6].opcode != OpCodes.Ldloc && codes[i - 6].opcode != OpCodes.Ldloc_S)
						|| !Calls(codes[i + 6], typeof(Bounds), "Encapsulate"))
						throw new InvalidOperationException("Unexpected native network-anchor height filter.");
					filter = i;
				}
				if (Equals(codes[i].operand, 3646f))
				{
					if (diagnostic >= 0 || !IsHeightRead(codes, i) || codes[i + 1].opcode != OpCodes.Bge_Un)
						throw new InvalidOperationException("Unexpected native construction-height diagnostic.");
					diagnostic = i;
				}
			}
			if (filter < 0 || diagnostic <= filter)
				throw new InvalidOperationException("Native network-anchor bounds pattern was not found.");
			List<CodeInstruction> result = new List<CodeInstruction>();
			for (int i = 0; i < codes.Count; i++)
			{
				CodeInstruction code = codes[i];
				if (i == filter)
				{
					code.opcode = codes[i - 6].opcode;
					code.operand = codes[i - 6].operand;
					result.Add(code);
					result.Add(new CodeInstruction(OpCodes.Ldarg_0));
					result.Add(new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(LegacyMachineNetworkAnchor), "IncludeBlock")));
					codes[i + 1].opcode = OpCodes.Brfalse;
				}
				else if (i == diagnostic)
				{
					code.opcode = OpCodes.Ldarg_0;
					code.operand = null;
					result.Add(code);
					result.Add(new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(LegacyMachineNetworkAnchor), "LogLowConstructionHeight")));
					codes[i + 1].opcode = OpCodes.Brfalse;
				}
				else result.Add(code);
			}
			return result;
		}

		private static bool IsHeightRead(List<CodeInstruction> codes, int i)
		{
			return Calls(codes[i - 5], typeof(Component), "get_transform")
				&& Calls(codes[i - 4], typeof(Transform), "get_position")
				&& codes[i - 1].opcode == OpCodes.Ldfld
				&& Equals(codes[i - 1].operand, AccessTools.Field(typeof(Vector3), "y"));
		}

		private static bool Calls(CodeInstruction code, Type type, string name)
		{
			MethodInfo method = code.operand as MethodInfo;
			return (code.opcode == OpCodes.Call || code.opcode == OpCodes.Callvirt)
				&& method != null && method.DeclaringType == type && method.Name == name;
		}
	}
}
