using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;

namespace MPatcherFork.CustomPatches
{
    internal static class SetupPrecisionShapes
    {
        internal static void Register(Harmony patcher)
        {
            HarmonyMethod threshold = new HarmonyMethod(typeof(SetupPrecisionShapes), "ThresholdTranspiler");
            threshold.priority = Priority.Last;
            foreach (string name in new[] { "RotPY", "RotNY", "RotPZ", "RotNZ" })
                patcher.Patch(AccessTools.Method(typeof(BlockData), name), new HarmonyMethod(typeof(SetupPrecisionShapes), "CapturePrefix"),
                    new HarmonyMethod(typeof(SetupPrecisionShapes), "RotatePostfix"), null, null);
            foreach (Type type in new[] { typeof(BoxGenController), typeof(CapGenController) })
            {
                patcher.Patch(AccessTools.Method(type, "SetLowMesh"), null, null, threshold, null);
                if (type == typeof(BoxGenController))
                    foreach (string name in new[] { "MakeBox", "MakeCollider" })
                        patcher.Patch(AccessTools.Method(type, name), null, null, threshold, null);
            }
        }

        private static void CapturePrefix(BlockData __instance, out float[] __state)
        {
            __state = null;
            if (!SetupPrecisionData.IsSize(__instance, 0) || !SetupPrecisionData.HasAny(__instance)) return;
            __state = new float[9];
            for (int slot = 0; slot < 9; slot++)
                if (SetupPrecisionData.Supports(__instance, slot)) __state[slot] = SetupPrecisionData.Read(__instance, slot);
        }

        private static void RotatePostfix(BlockData __instance, float[] __state, MethodBase __originalMethod)
        {
            if (__state != null) ApplyRotation(__instance, __state, __originalMethod.Name);
        }

        // Float counterpart of the native shape-only parameter permutation. The
        // original method still rotates FORM coordinates, shape IDs and orientation.
        private static void ApplyRotation(BlockData block, float[] values, string method)
        {
            bool y = method == "RotPY" || method == "RotNY";
            bool positive = method == "RotPY" || method == "RotPZ";
            int index = block.index;
            if (block.type == BlockData.AAHMDBHDCDK.BoxGen && (y || index == 1 || index == 3)) Swap(values, 0, 2, 1, 1);
            if (y)
            {
                int sign = (index == 2 ? 1 : -1) * (positive ? 1 : -1);
                Swap(values, 3, 5, sign, -sign);
                int angleSign = ((index & 1) == 1 ? -1 : 1) * (positive ? 1 : -1);
                if ((angleSign == (positive ? -1 : 1)) ^ (values[6] == 0)) angleSign = -angleSign;
                if (index == 2) angleSign = -angleSign;
                Swap(values, 6, 7, angleSign, angleSign);
            }
            else if (index == 2 || index == (positive ? 48 : 16))
                foreach (int slot in new[] { 3, 5, 6, 7 }) values[slot] = -values[slot];
            else if (index == 1 || index == 3)
            {
                int sign = (index == 1 ? 1 : -1) * (positive ? 1 : -1);
                Swap(values, 3, 5, sign, -sign);
                Swap(values, 6, 7, sign, -sign);
            }
            for (int slot = 0; slot < 9; slot++)
                if (SetupPrecisionData.Supports(block, slot)) SetupPrecisionData.Set(block, slot, values[slot]);
        }

        private static void Swap(float[] values, int first, int second, int firstSign, int secondSign)
        {
            float saved = values[first]; values[first] = values[second] * firstSign; values[second] = saved * secondSign;
        }

        private static float ThresholdValue(int native, BlockData block, bool area)
        {
            bool precise = false;
            for (int slot = 0; slot < 3; slot++)
                if (SetupPrecisionData.IsSize(block, slot) && SetupPrecisionData.Read(block, slot) != block.actionParam[slot]) precise = true;
            if (!precise) return native;
            float x = SetupPrecisionData.Read(block, 0), y = SetupPrecisionData.Read(block, 1);
            if (block.type == BlockData.AAHMDBHDCDK.CapGen) return x * (y + (block.actionParam[2] == 0 ? x : 0));
            float z = SetupPrecisionData.Read(block, 2);
            return area ? (x <= y ? y * Math.Max(x, z) : x * Math.Max(y, z))
                : (x <= y ? Math.Max(x, z) : Math.Max(y, z));
        }

        private static IEnumerable<CodeInstruction> ThresholdTranspiler(IEnumerable<CodeInstruction> instructions, MethodBase __originalMethod)
        {
            return RewriteThresholds(instructions, __originalMethod.DeclaringType.Name + "." + __originalMethod.Name);
        }

        private static IEnumerable<CodeInstruction> RewriteThresholds(IEnumerable<CodeInstruction> instructions, string target)
        {
            List<CodeInstruction> codes = new List<CodeInstruction>();
            foreach (CodeInstruction code in instructions) codes.Add(new CodeInstruction(code));
            bool area = target.EndsWith(".SetLowMesh", StringComparison.Ordinal);
            int count = 0;
            for (int i = 0; i + 1 < codes.Count; i++)
            {
                CodeInstruction constant = codes[i];
                if (constant.opcode != OpCodes.Ldc_I4 && constant.opcode != OpCodes.Ldc_I4_S) continue;
                int limit = Convert.ToInt32(constant.operand);
                if ((area ? limit != 100 : limit != 10 && limit != 50) || codes[i + 1].opcode.FlowControl != FlowControl.Cond_Branch) continue;
                CodeInstruction start = new CodeInstruction(OpCodes.Ldarg_0);
                start.labels.AddRange(constant.labels); constant.labels.Clear();
                start.blocks.AddRange(constant.blocks); constant.blocks.Clear();
                codes.InsertRange(i, new[] { start,
                    new CodeInstruction(OpCodes.Ldfld, AccessTools.Field(typeof(BlockController), "JNKEKNOAPHO")),
                    new CodeInstruction(area ? OpCodes.Ldc_I4_1 : OpCodes.Ldc_I4_0),
                    new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(SetupPrecisionShapes), "ThresholdValue")) });
                constant.opcode = OpCodes.Ldc_R4; constant.operand = (float)limit;
                count++; i += 4;
            }
            if (count != (target == "BoxGenController.MakeBox" ? 2 : 1)) throw new InvalidOperationException("SETUP shape thresholds changed: " + target);
            SetupPrecision.Log("SIZE_THRESHOLDS target=" + target + " count=" + count);
            return codes;
        }
    }
}
