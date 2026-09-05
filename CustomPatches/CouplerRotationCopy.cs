using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;

namespace MPatcherFork.CustomPatches
{
	internal static class CouplerRotationCopy
	{
		private sealed class CopyLoop
		{
			internal int Start;
			internal int End;
			internal CodeInstruction[] Target;
		}

		internal static void Register(Harmony patcher)
		{
			MethodInfo placement = AccessTools.Method(typeof(Build), "HJFFIDBJIEL", Type.EmptyTypes);
			MethodInfo settings = AccessTools.Method(typeof(Build), "JOHIPODALCN", Type.EmptyTypes);
			if (placement == null || settings == null)
				throw new MissingMethodException("Coupler block/settings copy hooks");
			HarmonyMethod placementPatch = new HarmonyMethod(AccessTools.Method(typeof(CouplerRotationCopy), "PlacementTranspiler"));
			HarmonyMethod settingsPatch = new HarmonyMethod(AccessTools.Method(typeof(CouplerRotationCopy), "SettingsTranspiler"));
			placementPatch.priority = settingsPatch.priority = Priority.Last;
			patcher.Patch(placement, null, null, placementPatch, null);
			if (!StartupHarmonyBatch.Registered)
				patcher.Patch(settings, null, null, settingsPatch, null);
		}

		internal static bool CanCopy(BlockData destination, BlockData source)
		{
			return CouplerRotation.IsRegistered && destination != null && source != null
				&& destination.type == BlockData.AAHMDBHDCDK.Coupler && source.type == destination.type
				&& destination.actionParam != null && destination.actionParam.Length >= 8
				&& source.actionParam != null && source.actionParam.Length >= 8
				&& destination.actionID != null && destination.actionID.Length >= 8
				&& source.actionID != null && source.actionID.Length >= 8;
		}

		internal static bool Apply(BlockData destination, BlockData source, string route)
		{
			if (!CanCopy(destination, source))
				return false;
			CouplerRotationOrder.Set(destination, CouplerRotationOrder.Read(source));
			CouplerRotationProfiles.Copy(destination, source);
			SetupPrecisionData.Copy(destination, source);
			bool restoredFree = CouplerRotation.Enabled && CouplerRotationProfiles.Switch(destination, false);
			CouplerRotation.Log("COPY_APPLIED route=" + route + " block=" + destination.x + "," + destination.y + "," + destination.z
				+ " order=" + CouplerRotationOrder.Name(CouplerRotationOrder.Read(destination))
				+ " xyz=" + destination.actionParam[3] + "," + destination.actionParam[4] + "," + destination.actionParam[5]
				+ " profile=" + (CouplerRotationProfiles.IsVanilla(destination) ? "Vanilla" : "Free")
				+ " history=" + CouplerRotationProfiles.HasArchive(destination) + " restoredFree=" + restoredFree);
			return true;
		}

		internal static bool ValidSettings(BlockData destination, BlockData source)
		{
			CouplerRotationProfiles.Rotation selected;
			CouplerRotationProfiles.Rotation retained;
			return CanCopy(destination, source)
				&& CouplerRotationProfiles.TryGetProfile(source, !CouplerRotation.Enabled, out selected)
				&& CouplerRotationProfiles.TryGetProfile(destination, CouplerRotation.Enabled, out retained);
		}

		internal static bool TryGetBufferRotation(BlockData source, bool settingsCopy, bool enabled,
			out CouplerRotationProfiles.Rotation rotation, out bool free)
		{
			free = enabled || (!settingsCopy && CouplerRotationProfiles.HasFreeValues(source) && !CouplerRotationProfiles.IsVanilla(source));
			return CouplerRotationProfiles.TryGetProfile(source, !free, out rotation);
		}

		internal static bool MatchesSettings(BlockData destination, BlockData source)
		{
			if (!ValidSettings(destination, source))
				return false;
			bool vanilla = !CouplerRotation.Enabled;
			if (vanilla ? CouplerRotationProfiles.HasFreeValues(destination) && !CouplerRotationProfiles.IsVanilla(destination)
				: !CouplerRotationProfiles.HasArchive(destination) || CouplerRotationProfiles.IsVanilla(destination))
				return false;
			CouplerRotationProfiles.Rotation selected;
			CouplerRotationProfiles.TryGetProfile(source, vanilla, out selected);
			CouplerRotationProfiles.Rotation current = CouplerRotationProfiles.Current(destination);
			if (!current.Same(selected) || !SetupPrecisionData.Matches(destination, source, true))
				return false;
			for (int index = 0; index < 8; index++)
				if (destination.actionID[index] != source.actionID[index]
					|| ((index < 3 || index > 5) && destination.actionParam[index] != source.actionParam[index]))
					return false;
			return true;
		}

		internal static bool CopySettings(BlockData destination, BlockData source)
		{
			if (!CanCopy(destination, source) || !CouplerRotationProfiles.CopySettings(destination, source, !CouplerRotation.Enabled))
				return false;
			for (int index = 0; index < 8; index++)
			{
				destination.actionID[index] = source.actionID[index];
				if (index < 3 || index > 5)
					destination.actionParam[index] = source.actionParam[index];
			}
			CouplerRotation.Log("COPY_APPLIED route=settings mode=" + (CouplerRotation.Enabled ? "Free" : "Vanilla")
				+ " block=" + destination.x + "," + destination.y + "," + destination.z
				+ " order=" + CouplerRotationOrder.Name(CouplerRotationOrder.Read(destination))
				+ " xyz=" + destination.actionParam[3] + "," + destination.actionParam[4] + "," + destination.actionParam[5]
				+ " otherProfile=preserved");
			SetupPrecisionData.Copy(destination, source, true);
			return true;
		}

		private static bool SettingsMatch(BlockData destination, BlockData source)
		{
			return CanCopy(destination, source) ? MatchesSettings(destination, source) : destination.CheckMatchAction(source);
		}

		private static void PlacementCopied(BlockData destination, BlockData source)
		{
			SetupPrecisionData.Copy(destination, source);
			Apply(destination, source, "block");
		}

		private static IEnumerable<CodeInstruction> PlacementTranspiler(IEnumerable<CodeInstruction> instructions)
		{
			List<CodeInstruction> codes = Clone(instructions);
			CopyLoop loop = FindLoop(codes, "CCKPNDEICKJ", false);
			List<CodeInstruction> added = new List<CodeInstruction>(loop.Target);
			added.Add(new CodeInstruction(OpCodes.Ldarg_0));
			added.Add(new CodeInstruction(OpCodes.Ldfld, AccessTools.Field(typeof(Build), "CCKPNDEICKJ")));
			added.Add(new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(CouplerRotationCopy), "PlacementCopied")));
			Insert(codes, loop.End, added);
			return codes;
		}

		internal static IEnumerable<CodeInstruction> SettingsTranspiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
		{
			List<CodeInstruction> codes = Clone(instructions);
			CopyLoop loop = FindLoop(codes, "DFAAJHFLMJL", true);
			MethodInfo match = AccessTools.Method(typeof(BlockData), "CheckMatchAction", new Type[] { typeof(BlockData) });
			List<CodeInstruction> comparisons = codes.FindAll(code => code.opcode == OpCodes.Callvirt && Equals(code.operand, match));
			if (comparisons.Count != 1)
				throw new InvalidOperationException("Coupler settings clipboard comparison changed");
			comparisons[0].opcode = OpCodes.Call;
			comparisons[0].operand = AccessTools.Method(typeof(CouplerRotationCopy), "SettingsMatch");
			Label finished = generator.DefineLabel();
			codes[loop.End].labels.Add(finished);
			Insert(codes, loop.Start, new List<CodeInstruction>
			{
				new CodeInstruction(OpCodes.Ldarg_0),
				new CodeInstruction(OpCodes.Ldarg_0),
				new CodeInstruction(OpCodes.Ldfld, AccessTools.Field(typeof(Build), "DFAAJHFLMJL")),
				new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(SetupPrecisionUi), "TryCopySettings")),
				new CodeInstruction(OpCodes.Brtrue, finished)
			});
			return codes;
		}

		private static CopyLoop FindLoop(List<CodeInstruction> codes, string source, bool selected)
		{
			int anchor = -1;
			for (int index = 1; index + 4 < codes.Count; index++)
			{
				if (LoadsField(codes[index], typeof(Build), source) && LoadsField(codes[index + 1], typeof(BlockData), "actionParam")
					&& Local(codes[index + 2], false) >= 0 && codes[index + 3].opcode == OpCodes.Ldelem_I4
					&& codes[index + 4].opcode == OpCodes.Stelem_I4)
				{
					if (anchor >= 0)
						throw new InvalidOperationException("Multiple Coupler copy loops: " + source);
					anchor = index;
				}
			}
			int targetLength = selected ? 2 : 1;
			int parameterStart = anchor - targetLength - 3;
			int bodyStart = parameterStart - targetLength - 8;
			int start = bodyStart - 3;
			int end = anchor + 12;
			if (anchor < 0 || start < 0 || end >= codes.Count)
				throw new InvalidOperationException("Missing Coupler copy loop: " + source);
			CodeInstruction[] target = new CodeInstruction[targetLength];
			for (int index = 0; index < target.Length; index++)
				target[index] = new CodeInstruction(codes[parameterStart + index].opcode, codes[parameterStart + index].operand);
			int counter = Local(codes[anchor + 2], false);
			if ((selected ? target[0].opcode != OpCodes.Ldarg_0 || !LoadsField(target[1], typeof(Build), "LBBOFMGMMFF")
				: Local(target[0], false) < 0)
				|| !CopyArray(codes, bodyStart, target, source, "actionID", counter)
				|| !CopyArray(codes, parameterStart, target, source, "actionParam", counter)
				|| codes[start].opcode != OpCodes.Ldc_I4_0 || Local(codes[start + 1], true) != counter
				|| !BranchesTo(codes[start + 2], OpCodes.Br, OpCodes.Br_S, codes[anchor + 9])
				|| Local(codes[anchor + 5], false) != counter || codes[anchor + 6].opcode != OpCodes.Ldc_I4_1
				|| codes[anchor + 7].opcode != OpCodes.Add || Local(codes[anchor + 8], true) != counter
				|| Local(codes[anchor + 9], false) != counter || codes[anchor + 10].opcode != OpCodes.Ldc_I4_8
				|| !BranchesTo(codes[anchor + 11], OpCodes.Blt, OpCodes.Blt_S, codes[bodyStart]))
				throw new InvalidOperationException("Coupler copy loop changed: " + source);
			return new CopyLoop { Start = start, End = end, Target = target };
		}

		private static bool CopyArray(List<CodeInstruction> codes, int start, CodeInstruction[] target, string source, string array, int counter)
		{
			for (int index = 0; index < target.Length; index++)
				if (codes[start + index].opcode != target[index].opcode || !Equals(codes[start + index].operand, target[index].operand))
					return false;
			int field = start + target.Length;
			return LoadsField(codes[field], typeof(BlockData), array) && Local(codes[field + 1], false) == counter
				&& codes[field + 2].opcode == OpCodes.Ldarg_0 && LoadsField(codes[field + 3], typeof(Build), source)
				&& LoadsField(codes[field + 4], typeof(BlockData), array) && Local(codes[field + 5], false) == counter
				&& codes[field + 6].opcode == OpCodes.Ldelem_I4 && codes[field + 7].opcode == OpCodes.Stelem_I4;
		}

		private static bool LoadsField(CodeInstruction code, Type owner, string name)
		{
			FieldInfo field = code.operand as FieldInfo;
			return code.opcode == OpCodes.Ldfld && field != null && field.DeclaringType == owner && field.Name == name;
		}

		private static bool BranchesTo(CodeInstruction code, OpCode longForm, OpCode shortForm, CodeInstruction target)
		{
			return (code.opcode == longForm || code.opcode == shortForm) && code.operand is Label && target.labels.Contains((Label)code.operand);
		}

		private static int Local(CodeInstruction code, bool store)
		{
			if (code.opcode == (store ? OpCodes.Stloc_0 : OpCodes.Ldloc_0)) return 0;
			if (code.opcode == (store ? OpCodes.Stloc_1 : OpCodes.Ldloc_1)) return 1;
			if (code.opcode == (store ? OpCodes.Stloc_2 : OpCodes.Ldloc_2)) return 2;
			if (code.opcode == (store ? OpCodes.Stloc_3 : OpCodes.Ldloc_3)) return 3;
			if (code.opcode != (store ? OpCodes.Stloc : OpCodes.Ldloc) && code.opcode != (store ? OpCodes.Stloc_S : OpCodes.Ldloc_S))
				return -1;
			LocalBuilder local = code.operand as LocalBuilder;
			return local == null ? Convert.ToInt32(code.operand) : local.LocalIndex;
		}

		private static List<CodeInstruction> Clone(IEnumerable<CodeInstruction> instructions)
		{
			List<CodeInstruction> codes = new List<CodeInstruction>();
			foreach (CodeInstruction code in instructions)
				codes.Add(new CodeInstruction(code));
			return codes;
		}

		private static void Insert(List<CodeInstruction> codes, int index, List<CodeInstruction> added)
		{
			added[0].labels.AddRange(codes[index].labels);
			added[0].blocks.AddRange(codes[index].blocks);
			codes[index].labels.Clear();
			codes[index].blocks.Clear();
			codes.InsertRange(index, added);
		}
	}
}
