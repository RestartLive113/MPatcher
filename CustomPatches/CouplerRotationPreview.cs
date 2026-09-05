using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
	internal static class CouplerRotationPreview
	{
		private static OffsetRenderer activeRenderer;
		private static BlockData activeBlock;
		private static int activeGroup = -1;
		private static int activeFrame = -1;
		private static int renderedAxes;
		private static BlockData lastBlock;
		private static int lastGroup = -1;
		private static Vector3 lastAngles;
		private static int lastOrder = -1;
		private static int lastLogTick;
		private static string lastFailure;

		internal static void Register(Harmony patcher)
		{
			MethodInfo preview = AccessTools.Method(typeof(Build), "Update", Type.EmptyTypes);
			MethodInfo render = AccessTools.Method(typeof(OffsetRenderer), "OnRenderObject", Type.EmptyTypes);
			if (preview == null || render == null)
				throw new MissingMethodException("Coupler assembled preview axes");
			HarmonyMethod postfix = new HarmonyMethod(AccessTools.Method(typeof(CouplerRotationPreview), "PreviewPostfix"));
			HarmonyMethod transpiler = new HarmonyMethod(AccessTools.Method(typeof(CouplerRotationPreview), "RenderTranspiler"));
			postfix.priority = transpiler.priority = Priority.Last;
			if (!StartupHarmonyBatch.Registered)
				patcher.Patch(preview, null, postfix, null, null);
			patcher.Patch(render, null, null, transpiler, null);
		}

		internal static bool ShouldAlign(BlockData block, bool assembled, bool visible, int group, int groupCount)
		{
			return CouplerRotation.IsRegistered && block != null && block.type == BlockData.AAHMDBHDCDK.Coupler
				&& assembled && visible && group >= 0 && group < groupCount;
		}

		internal static Quaternion RotationThroughAxis(Vector3 angles, int order, int axis)
		{
			if (axis < 0 || axis > 2)
				throw new ArgumentOutOfRangeException("axis");
			Vector3 prefix = Vector3.zero;
			for (int row = 0; row < 3; row++)
			{
				int current = CouplerRotationOrder.Axis(order, row);
				prefix[current] = angles[current];
				if (current == axis)
					break;
			}
			return CouplerRotationMath.FromEuler(prefix, order);
		}

		internal static bool ShouldDraw(OffsetRenderer renderer, int frame, int axis)
		{
			return CouplerRotation.IsRegistered && activeBlock != null && !ReferenceEquals(renderer, null)
				&& ReferenceEquals(renderer, activeRenderer) && frame == activeFrame && axis >= 0 && axis < 3;
		}

		internal static void PreviewPostfix(BlockData ___LBBOFMGMMFF, BlockController ___MLJHGDBPMEA,
			HIPBCCKFFAG ___FFJDGJFPLAD, GameObject ___LENNGHKPCAN)
		{
			ClearContext();
			try
			{
				int group = ___MLJHGDBPMEA != null ? ___MLJHGDBPMEA.DCNIOOFAOMB : -1;
				int groupCount = ___FFJDGJFPLAD != null && ___FFJDGJFPLAD.CLNMBHMCPGB != null ? ___FFJDGJFPLAD.CLNMBHMCPGB.Count : 0;
				if (!ShouldAlign(___LBBOFMGMMFF, ___FFJDGJFPLAD != null && ___FFJDGJFPLAD.HCMMJPFOIHD,
					___LENNGHKPCAN != null && ___LENNGHKPCAN.activeSelf, group, groupCount))
				{
					lastBlock = null;
					return;
				}
				if (___FFJDGJFPLAD.CLNMBHMCPGB[group] == null)
					throw new InvalidOperationException("Missing Coupler preview body " + group);
				activeRenderer = ___LENNGHKPCAN.GetComponent<OffsetRenderer>();
				if (activeRenderer == null)
					throw new InvalidOperationException("Missing Coupler OffsetRenderer");
				activeBlock = ___LBBOFMGMMFF;
				activeGroup = group;
				activeFrame = Time.frameCount;
			}
			catch (Exception error)
			{
				ReportFailure(error);
			}
		}

		private static void ClearContext()
		{
			activeRenderer = null;
			activeBlock = null;
			activeGroup = activeFrame = -1;
			renderedAxes = 0;
		}

		private static Matrix4x4 AxisFrame(Matrix4x4 original, OffsetRenderer renderer, int axis)
		{
			if (!ShouldDraw(renderer, Time.frameCount, axis))
				return original;
			try
			{
				Vector3 angles = SetupPrecisionData.Angles(activeBlock);
				int order = CouplerRotationOrder.Read(activeBlock);
				Transform frame = renderer.transform;
				Matrix4x4 result = StageFrame(frame.position, frame.localRotation, frame.localScale, RotationThroughAxis(angles, order, axis));
				renderedAxes |= 1 << axis;
				lastFailure = null;
				if (renderedAxes == 7)
					LogRendered(angles, order);
				return result;
			}
			catch (Exception error)
			{
				ReportFailure(error);
				return original;
			}
		}

		private static Matrix4x4 StageFrame(Vector3 position, Quaternion parent, Vector3 scale, Quaternion stage)
		{
			return Matrix4x4.TRS(position, parent * stage, scale);
		}

		private static void LogRendered(Vector3 angles, int order)
		{
			int now = Environment.TickCount;
			bool selectionChanged = !ReferenceEquals(lastBlock, activeBlock) || lastGroup != activeGroup;
			if (!selectionChanged && (unchecked(now - lastLogTick) < 500 || (lastAngles == angles && lastOrder == order)))
				return;
			lastBlock = activeBlock;
			lastGroup = activeGroup;
			lastAngles = angles;
			lastOrder = order;
			lastLogTick = now;
			CouplerRotation.Log("PREVIEW_AXES_RENDER group=" + activeGroup + " block=" + activeBlock.x + "," + activeBlock.y + "," + activeBlock.z
				+ " order=" + CouplerRotationOrder.Name(order) + " angles=" + angles.ToString("F0")
				+ " drawMask=XYZ frame=" + activeFrame + " source=ordered-stages");
		}

		private static void ReportFailure(Exception error)
		{
			ClearContext();
			string failure = error.GetType().Name + ":" + error.Message;
			if (failure != lastFailure)
			{
				lastFailure = failure;
				CouplerRotation.Log("PREVIEW_AXES_FAILED " + failure);
			}
		}

		private static IEnumerable<CodeInstruction> RenderTranspiler(IEnumerable<CodeInstruction> instructions)
		{
			List<CodeInstruction> codes = new List<CodeInstruction>();
			foreach (CodeInstruction instruction in instructions)
				codes.Add(new CodeInstruction(instruction));
			MethodInfo push = AccessTools.Method(typeof(GL), "PushMatrix", Type.EmptyTypes);
			MethodInfo multiply = AccessTools.Method(typeof(GL), "MultMatrix", new Type[] { typeof(Matrix4x4) });
			MethodInfo trs = AccessTools.Method(typeof(Matrix4x4), "TRS", new Type[] { typeof(Vector3), typeof(Quaternion), typeof(Vector3) });
			MethodInfo draw = AccessTools.Method(typeof(OffsetRenderer), "LLOEOFBFLNP", new Type[] { typeof(Color) });
			MethodInfo helper = AccessTools.Method(typeof(CouplerRotationPreview), "AxisFrame");
			string[] colors = { "get_green", "get_red", "get_blue" };
			int[] axes = { 1, 0, 2 };
			List<int> loads = new List<int>();
			int baseLocal = -1;
			int matrixCalls = 0;
			int drawCalls = 0;
			for (int index = 0; index < codes.Count; index++)
			{
				if (baseLocal < 0 && Calls(codes[index], trs) && index + 1 < codes.Count)
					baseLocal = Local(codes[index + 1], true);
				if (Calls(codes[index], push))
				{
					if (baseLocal < 0 || index + 1 >= codes.Count || Local(codes[index + 1], false) != baseLocal)
						throw new InvalidOperationException("Coupler axis base matrix changed");
					loads.Add(index + 1);
				}
				if (Calls(codes[index], multiply))
					matrixCalls++;
				if (Calls(codes[index], draw))
				{
					MethodInfo color = index == 0 ? null : codes[index - 1].operand as MethodInfo;
					if (drawCalls >= 3 || loads.Count != drawCalls + 1 || matrixCalls != drawCalls + 1
						|| color == null || color.DeclaringType != typeof(Color) || color.Name != colors[drawCalls])
						throw new InvalidOperationException("Coupler axis color/order changed");
					drawCalls++;
				}
			}
			if (loads.Count != 3 || matrixCalls != 3 || drawCalls != 3)
				throw new InvalidOperationException("Coupler axis render count changed");
			for (int index = loads.Count - 1; index >= 0; index--)
				codes.InsertRange(loads[index] + 1, new CodeInstruction[] {
					new CodeInstruction(OpCodes.Ldarg_0), new CodeInstruction(OpCodes.Ldc_I4, axes[index]), new CodeInstruction(OpCodes.Call, helper) });
			return codes;
		}

		private static bool Calls(CodeInstruction instruction, MethodInfo method)
		{
			return (instruction.opcode == OpCodes.Call || instruction.opcode == OpCodes.Callvirt) && Equals(instruction.operand, method);
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

		internal static void VerifyRuntimeMath()
		{
			Vector3[] samples = { Vector3.zero, new Vector3(90, 0, 0), new Vector3(30, -45, 60), new Vector3(-180, 180, -180),
				new Vector3(180, -180, 180), new Vector3(-60, -150, 0) };
			Vector3[] vertices = { Vector3.zero, Vector3.up, new Vector3(0.1f, 1f, 0f), new Vector3(0f, 1f, 0.1f) };
			Quaternion[] parents = { Quaternion.identity, Quaternion.Euler(23f, -41f, 17f) };
			Vector3[] scales = { Vector3.one, new Vector3(0.4f, 0.7f, 1.1f) };
			Vector3 position = new Vector3(1.25f, -2.5f, 3.75f);
			float maxError = 0f;
			int cases = 0;
			for (int order = 0; order < CouplerRotationOrder.Count; order++)
			foreach (Vector3 angles in samples)
			foreach (Quaternion parent in parents)
			foreach (Vector3 scale in scales)
			{
				Quaternion expected = Quaternion.identity;
				for (int row = 0; row < 3; row++)
				{
					int axis = CouplerRotationOrder.Axis(order, row);
					Vector3 step = Vector3.zero;
					step[axis] = -angles[axis];
					expected *= Quaternion.Euler(step);
					Quaternion nativeAxis = axis == 0 ? Quaternion.Euler(0f, 0f, -90f)
						: axis == 2 ? Quaternion.Euler(90f, 0f, 0f) : Quaternion.identity;
					Matrix4x4 actual = StageFrame(position, parent, scale, RotationThroughAxis(angles, order, axis));
					if (axis != 1)
						actual *= Matrix4x4.TRS(Vector3.zero, nativeAxis, scale);
					foreach (Vector3 vertex in vertices)
					{
						Vector3 oriented = axis == 1 ? vertex : nativeAxis * Vector3.Scale(scale, vertex);
						Vector3 target = position + parent * (expected * Vector3.Scale(scale, oriented));
						maxError = Math.Max(maxError, (actual.MultiplyPoint3x4(vertex) - target).magnitude);
					}
					cases++;
				}
			}
			if (float.IsNaN(maxError) || maxError > 0.00002f)
				throw new InvalidOperationException("Coupler Unity axis stage check failed: error=" + maxError);
			CouplerRotation.Log("UNITY_PREVIEW_AXES_CHECK orders=6 cases=" + cases + " maxVertexError="
				+ maxError.ToString("G6", System.Globalization.CultureInfo.InvariantCulture) + " result=PASS source=ordered-stages");
		}
	}
}
