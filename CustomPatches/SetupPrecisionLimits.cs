using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;

namespace MPatcherFork.CustomPatches
{
    // Extend only the native Box/Capsule dimensions and shape/Coupler offsets.
    // Mechanism, rotation, and FORM grid limits keep their original values.
    internal static class SetupPrecisionLimits
    {
        internal static void Register(Harmony patcher)
        {
            MethodInfo target = AccessTools.Method(typeof(BlockData), "AdjustParam");
            if (target == null) throw new MissingMethodException("BlockData", "AdjustParam");
            patcher.Patch(target, new HarmonyMethod(typeof(SetupPrecisionLimits), "CaptureExtendedPrefix"),
                new HarmonyMethod(typeof(SetupPrecisionLimits), "LogExtendedPostfix"),
                new HarmonyMethod(typeof(SetupPrecisionLimits), "AdjustParamTranspiler"), null);
            SetupPrecision.Log("LIMITS_REGISTERED target=BlockData.AdjustParam shapeSize=1000 boxOffset=+-10000 capsuleOffset=+-1000 couplerOffset=+-10000 precision=0.001");
        }

        private static IEnumerable<CodeInstruction> AdjustParamTranspiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> result = new List<CodeInstruction>();
            int sizes = 0, negativeOffsets = 0, positiveOffsets = 0;
            foreach (CodeInstruction instruction in instructions)
            {
                CodeInstruction code = new CodeInstruction(instruction);
                if (code.opcode == OpCodes.Ldc_I4 && code.operand is int)
                {
                    int value = (int)code.operand;
                    if (value == 250) { code.operand = SetupPrecisionData.ShapeSizeMaximum; sizes++; }
                    else if (value == -500)
                    {
                        negativeOffsets++;
                        if (negativeOffsets == 1) AddShapeLimitCall(result, code, "ShapeOffsetMinimum");
                        else code.operand = -SetupPrecisionData.CouplerOffsetMaximum;
                    }
                    else if (value == 500)
                    {
                        positiveOffsets++;
                        if (positiveOffsets == 1) AddShapeLimitCall(result, code, "ShapeOffsetMaximum");
                        else code.operand = SetupPrecisionData.CouplerOffsetMaximum;
                    }
                }
                result.Add(code);
            }
            if (sizes != 5 || negativeOffsets != 2 || positiveOffsets != 2)
                throw new InvalidOperationException("BlockData.AdjustParam shape limit IL changed: "
                    + sizes + "/" + negativeOffsets + "/" + positiveOffsets);
            SetupPrecision.Log("NATIVE_LIMITS sizeClamps=" + sizes + " shapeOffsetPairs=1 bound=per-Box-or-Capsule couplerOffsetPairs=1");
            return result;
        }

        private static void AddShapeLimitCall(List<CodeInstruction> result, CodeInstruction code, string method)
        {
            CodeInstruction instance = new CodeInstruction(OpCodes.Ldarg_0);
            instance.labels.AddRange(code.labels);
            instance.blocks.AddRange(code.blocks);
            code.labels.Clear();
            code.blocks.Clear();
            result.Add(instance);
            code.opcode = OpCodes.Call;
            code.operand = AccessTools.Method(typeof(SetupPrecisionLimits), method);
        }

        private static int ShapeOffsetMinimum(BlockData block) { return -SetupPrecisionData.ShapeOffsetLimit(block); }
        private static int ShapeOffsetMaximum(BlockData block) { return SetupPrecisionData.ShapeOffsetLimit(block); }

        private static void CaptureExtendedPrefix(BlockData __instance, out bool __state)
        {
            __state = false;
            if (__instance == null || __instance.actionParam == null || __instance.actionParam.Length < 6) return;
            int[] values = __instance.actionParam;
            if (__instance.type == BlockData.AAHMDBHDCDK.BoxGen || __instance.type == BlockData.AAHMDBHDCDK.CapGen)
            {
                int dimensions = __instance.type == BlockData.AAHMDBHDCDK.BoxGen ? 3 : 2;
                for (int slot = 0; slot < dimensions; slot++) if (values[slot] > 250) __state = true;
                for (int slot = 3; slot < 6; slot++) if (Math.Abs((long)values[slot]) > 500) __state = true;
            }
            else if (__instance.type == BlockData.AAHMDBHDCDK.Coupler)
                for (int slot = 0; slot < 3; slot++) if (Math.Abs((long)values[slot]) > 500) __state = true;
        }

        private static void LogExtendedPostfix(BlockData __instance, bool __state)
        {
            if (__state)
                SetupPrecision.Log("EXTENDED_PARAM type=" + __instance.type + " block=" + __instance.GetPosString());
        }
    }
}
