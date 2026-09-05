using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using McnCraft;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
	internal static class CouplerRotationNetwork
	{
		private const string SharedPatchId = "local.moddev.machinecraft.structure-network.shared.v2";
		private static Harmony sharedHarmony;
		private static Harmony couplerHarmony;
		private static bool sharedSetupPrecision;
		private static int lastWriteLogTick;
		private static int lastReadLogTick;

		internal static void Register(Harmony harmony)
		{
			Stopwatch startup = Stopwatch.StartNew();
			MethodInfo writer;
			MethodInfo reader;
			ResolveStructureMethods(out writer, out reader);
			couplerHarmony = harmony;
			Harmony batch = new Harmony(SharedPatchId);
			try
			{
				PatchProcessor writerProcessor = new PatchProcessor(batch, writer);
				writerProcessor.AddTranspiler(Transpiler(typeof(CouplerRotationNetwork), "SharedWriterTranspiler"));
				writerProcessor.Patch();
				long writerMs = startup.ElapsedMilliseconds;
				CouplerRotation.Log("STARTUP_TIMING network=SyncStructure shared=Coupler+Setup stepMs=" + writerMs + " totalMs=" + writerMs);

				PatchProcessor readerProcessor = new PatchProcessor(batch, reader);
				readerProcessor.AddTranspiler(Transpiler(typeof(CouplerRotationNetwork), "SharedReaderTranspiler"));
				readerProcessor.Patch();
				long totalMs = startup.ElapsedMilliseconds;
				sharedHarmony = batch;
				sharedSetupPrecision = true;
				CouplerRotation.Log("STARTUP_TIMING network=MakeStructureNB.MoveNext shared=Coupler+Setup stepMs="
					+ (totalMs - writerMs) + " totalMs=" + totalMs);
			}
			catch
			{
				sharedHarmony = batch;
				sharedSetupPrecision = false;
				batch.UnpatchAll(SharedPatchId);
				sharedHarmony = null;
				throw;
			}
		}

		internal static bool SharedSetupPrecisionRegistered
		{
			get { return sharedSetupPrecision; }
		}

		internal static void UnregisterShared()
		{
			Harmony owner = sharedHarmony;
			sharedSetupPrecision = false;
			couplerHarmony = null;
			if (owner != null)
				owner.UnpatchAll(SharedPatchId);
			sharedHarmony = null;
		}

		internal static void RestoreCouplerOnlyAfterSetupFailure()
		{
			if (!sharedSetupPrecision)
				return;

			Harmony original = couplerHarmony;
			Harmony owner = sharedHarmony;
			sharedSetupPrecision = false;
			try
			{
				if (owner != null)
					owner.UnpatchAll(SharedPatchId);
				sharedHarmony = null;
				if (original == null || !CouplerRotation.IsRegistered)
					return;
				Stopwatch restore = Stopwatch.StartNew();
				MethodInfo writer;
				MethodInfo reader;
				ResolveStructureMethods(out writer, out reader);
				original.Patch(writer, null, null, Transpiler(typeof(CouplerRotationNetwork), "WriterTranspiler"), null);
				original.Patch(reader, null, null, Transpiler(typeof(CouplerRotationNetwork), "ReaderTranspiler"), null);
				CouplerRotation.Log("NETWORK_SHARED_ROLLBACK restored=Coupler-only elapsedMs=" + restore.ElapsedMilliseconds);
			}
			catch (Exception error)
			{
				sharedHarmony = owner;
				CouplerRotation.DisableAfterNetworkFailure(error);
			}
		}

		private static void ResolveStructureMethods(out MethodInfo writer, out MethodInfo reader)
		{
			writer = AccessTools.Method(typeof(MachineSerializer), "SyncStructure", new Type[] { typeof(bool[]) });
			Type iterator = typeof(MachineSerializer).GetNestedType("<MakeStructureNB>c__Iterator0", BindingFlags.NonPublic);
			reader = iterator == null ? null : AccessTools.Method(iterator, "MoveNext");
			if (writer == null || reader == null)
				throw new MissingMethodException("Coupler structure writer/reader");
		}

		private static HarmonyMethod Transpiler(Type owner, string name)
		{
			MethodInfo method = AccessTools.Method(owner, name);
			if (method == null)
				throw new MissingMethodException(owner.Name, name);
			HarmonyMethod result = new HarmonyMethod(method);
			result.priority = Priority.Last;
			return result;
		}

		internal static IEnumerable<CodeInstruction> SharedWriterTranspiler(IEnumerable<CodeInstruction> instructions)
		{
			return SetupPrecisionNetwork.WriterTranspiler(WriterTranspiler(instructions));
		}

		internal static IEnumerable<CodeInstruction> SharedReaderTranspiler(IEnumerable<CodeInstruction> instructions)
		{
			return SetupPrecisionNetwork.ReaderTranspiler(ReaderTranspiler(instructions));
		}

		internal static IEnumerable<CodeInstruction> WriterTranspiler(IEnumerable<CodeInstruction> instructions)
		{
			List<CodeInstruction> codes = Copy(instructions);
			int comparison = FindComparison(codes, AccessTools.Field(typeof(BodyController), "ANAHNNNBKFC"), OpCodes.Bne_Un);
			int start = comparison + 3;
			int end = FindLabel(codes, codes[comparison + 2].operand);
			FieldInfo rotations = AccessTools.Field(typeof(HDBLLPODNLN), "MJBDKMNEKML");
			FieldInfo positions = AccessTools.Field(typeof(HDBLLPODNLN), "NFOEKNHCNBM");
			MethodInfo add = AccessTools.Method(typeof(List<int>), "Add");
			if (end <= start || codes[end - 1].opcode != OpCodes.Br || !(codes[end - 1].operand is Label)
				|| codes[start + 1].opcode != OpCodes.Ldfld || !Equals(codes[start + 1].operand, rotations)
				|| !IsLocalLoad(codes[start]) || !IsLocalLoad(codes[start + 2]))
				throw new InvalidOperationException("Coupler structure writer entry changed");
			List<CodeInstruction> body = codes.GetRange(start, end - start);
			List<CodeInstruction> writes = body.FindAll(code => code.opcode == OpCodes.Callvirt && Equals(code.operand, add));
			int rotationReads = body.FindAll(code => code.opcode == OpCodes.Ldfld && Equals(code.operand, rotations)).Count;
			int positionReads = body.FindAll(code => code.opcode == OpCodes.Ldfld && Equals(code.operand, positions)).Count;
			if (writes.Count != 2 || rotationReads != 5 || positionReads != 3)
				throw new InvalidOperationException("Coupler structure writer shape changed: writes=" + writes.Count
					+ " rotations=" + rotationReads + " positions=" + positionReads);
			CodeInstruction payload = codes[codes.IndexOf(writes[0]) - 2];
			CodeInstruction secondPayload = codes[codes.IndexOf(writes[1]) - 2];
			if (!IsLocalLoad(payload) || payload.opcode != secondPayload.opcode || !Equals(payload.operand, secondPayload.operand))
				throw new InvalidOperationException("Coupler structure writer payload changed");
			InsertGuard(codes, start, new CodeInstruction[]
			{
				new CodeInstruction(payload.opcode, payload.operand),
				new CodeInstruction(codes[start].opcode, codes[start].operand),
				new CodeInstruction(codes[start + 2].opcode, codes[start + 2].operand),
				new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(CouplerRotationNetwork), "TryWriteCoupler")),
				new CodeInstruction(OpCodes.Brtrue, codes[end - 1].operand)
			});
			return codes;
		}

		internal static IEnumerable<CodeInstruction> ReaderTranspiler(IEnumerable<CodeInstruction> instructions)
		{
			List<CodeInstruction> codes = Copy(instructions);
			int comparison = FindComparison(codes, AccessTools.Field(typeof(NMLMDCCDFPN), "KMCBOCKBEAJ"), OpCodes.Beq);
			int start = FindLabel(codes, codes[comparison + 2].operand);
			if (start < 1 || codes[start - 1].opcode != OpCodes.Br)
				throw new InvalidOperationException("Coupler structure reader entry changed");
			int end = FindLabel(codes, codes[start - 1].operand);
			if (end <= start || codes[start].opcode != OpCodes.Ldarg_0 || codes[start + 1].opcode != OpCodes.Ldfld
				|| codes[comparison - 1].opcode != OpCodes.Ldfld)
				throw new InvalidOperationException("Coupler structure reader fields changed");
			FieldInfo group = codes[comparison - 1].operand as FieldInfo;
			FieldInfo header = codes[start + 1].operand as FieldInfo;
			List<CodeInstruction> body = codes.GetRange(start, end - start);
			List<CodeInstruction> arrays = body.FindAll(code => code.opcode == OpCodes.Ldfld
				&& code.operand is FieldInfo && ((FieldInfo)code.operand).FieldType == typeof(int[]));
			List<CodeInstruction> cursors = body.FindAll(code => code.opcode == OpCodes.Stfld
				&& code.operand is FieldInfo && ((FieldInfo)code.operand).FieldType == typeof(int));
			MethodInfo rotate = AccessTools.Method(typeof(NMLMDCCDFPN), "MDAAHPEENMJ");
			MethodInfo offset = AccessTools.Method(typeof(NMLMDCCDFPN), "AGGDIGBFEBC");
			if (group == null || group.FieldType != typeof(NMLMDCCDFPN) || header == null || header.FieldType != typeof(uint)
				|| arrays.Count != 1 || cursors.Count != 1
				|| body.FindAll(code => code.opcode == OpCodes.Callvirt && Equals(code.operand, rotate)).Count != 3
				|| body.FindAll(code => code.opcode == OpCodes.Callvirt && Equals(code.operand, offset)).Count != 1)
				throw new InvalidOperationException("Coupler structure reader shape changed");
			InsertGuard(codes, start, new CodeInstruction[]
			{
				new CodeInstruction(OpCodes.Ldarg_0), new CodeInstruction(OpCodes.Ldfld, group),
				new CodeInstruction(OpCodes.Ldarg_0), new CodeInstruction(OpCodes.Ldfld, header),
				new CodeInstruction(OpCodes.Ldarg_0), new CodeInstruction(OpCodes.Ldfld, arrays[0].operand),
				new CodeInstruction(OpCodes.Ldarg_0), new CodeInstruction(OpCodes.Ldflda, cursors[0].operand),
				new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(CouplerRotationNetwork), "TryReadCoupler")),
				new CodeInstruction(OpCodes.Brtrue, codes[start - 1].operand)
			});
			return codes;
		}

		private static bool TryWriteCoupler(List<int> words, HDBLLPODNLN graph, int group)
		{
			if (!CouplerRotation.IsRegistered || !CouplerRotationMath.IsMixed(graph.MJBDKMNEKML[group]))
				return false;
			int order;
			Vector3 angles = CouplerRotationCodec.WriteRecord(words, graph.MJBDKMNEKML[group], graph.NFOEKNHCNBM[group], out order);
			LogTransfer("NETWORK_WRITE", angles, order, ref lastWriteLogTick);
			return true;
		}

		private static bool TryReadCoupler(NMLMDCCDFPN group, uint header, int[] words, ref int cursor)
		{
			if (!CouplerRotation.IsRegistered || !CouplerRotationCodec.IsExtended(header))
				return false;
			Vector3 angles;
			Vector3 position;
			int order;
			bool valid = CouplerRotationCodec.ReadRecord(header, words, ref cursor, out angles, out position, out order);
			Vector3 nativeAngles = order == CouplerRotationOrder.Default ? angles
				: CouplerRotationMath.ToBoxEuler(CouplerRotationMath.FromEuler(angles, order));
			group.MDAAHPEENMJ(-nativeAngles.x, -nativeAngles.y, -nativeAngles.z);
			group.AGGDIGBFEBC(position);
			LogTransfer(valid ? "NETWORK_READ" : "NETWORK_READ_REJECTED", angles, order, ref lastReadLogTick);
			return true;
		}

		private static void LogTransfer(string eventName, Vector3 angles, int order, ref int lastTick)
		{
			int now = Environment.TickCount;
			if (lastTick != 0 && unchecked(now - lastTick) < 1000)
				return;
			lastTick = now;
			CouplerRotation.Log(eventName + " protocol=" + (order == CouplerRotationOrder.Default ? "XYZ-v1" : "XYZ-v2")
				+ " euler=" + angles.ToString("F0") + " order=" + CouplerRotationOrder.Name(order) + " checkbox=not-required");
		}

		private static List<CodeInstruction> Copy(IEnumerable<CodeInstruction> instructions)
		{
			List<CodeInstruction> codes = new List<CodeInstruction>();
			foreach (CodeInstruction instruction in instructions)
				codes.Add(new CodeInstruction(instruction));
			return codes;
		}

		private static int FindComparison(List<CodeInstruction> codes, FieldInfo field, OpCode branch)
		{
			int result = -1;
			for (int position = 0; position < codes.Count - 2; position++)
			{
				if (codes[position].opcode != OpCodes.Ldfld || !Equals(codes[position].operand, field)
					|| codes[position + 1].opcode != OpCodes.Ldc_I4_S || Convert.ToInt32(codes[position + 1].operand) != 15
					|| codes[position + 2].opcode != branch)
					continue;
				if (result >= 0)
					throw new InvalidOperationException("Ambiguous Coupler structure branch");
				result = position;
			}
			if (result < 0)
				throw new InvalidOperationException("Missing Coupler structure branch");
			return result;
		}

		private static int FindLabel(List<CodeInstruction> codes, object label)
		{
			int position = label is Label ? codes.FindIndex(code => code.labels.Contains((Label)label)) : -1;
			if (position < 0)
				throw new InvalidOperationException("Missing Coupler structure branch target");
			return position;
		}

		private static bool IsLocalLoad(CodeInstruction code)
		{
			return code.opcode == OpCodes.Ldloc || code.opcode == OpCodes.Ldloc_S || code.opcode == OpCodes.Ldloc_0
				|| code.opcode == OpCodes.Ldloc_1 || code.opcode == OpCodes.Ldloc_2 || code.opcode == OpCodes.Ldloc_3;
		}

		private static void InsertGuard(List<CodeInstruction> codes, int start, CodeInstruction[] guard)
		{
			guard[0].labels.AddRange(codes[start].labels);
			guard[0].blocks.AddRange(codes[start].blocks);
			codes[start].labels.Clear();
			codes[start].blocks.Clear();
			codes.InsertRange(start, guard);
		}
	}
}
