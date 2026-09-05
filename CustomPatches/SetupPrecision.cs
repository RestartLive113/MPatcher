using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using MPatchrMain;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
    internal enum SetupPrecisionEditorMode
    {
        VanillaSlider = 0,
        DigitSpinner = 1,
        SliderDigitSpinner = 2
    }

    internal static class SetupPrecision
    {
        private const string PatchId = "local.moddev.machinecraft.setup-precision.v1";
        internal static bool IsRegistered { get; private set; }

        internal static void TryRegister()
        {
            if (IsRegistered) return;
            Stopwatch startup = Stopwatch.StartNew();
            long checkpoint = 0;
            Harmony patcher = new Harmony(PatchId);
            try
            {
                Log("STARTUP_TIMING_BEGIN version=34");
                FloatReads(patcher, typeof(BoxGenController), "MakeBox");
                FloatReads(patcher, typeof(BoxGenController), "AdjustBox");
                FloatReads(patcher, typeof(CapGenController), "MakeCapsule");
                FloatReads(patcher, typeof(BlockData), "GetCouplerOffset");
                FloatReads(patcher, typeof(PartsController), "Start");
                FloatReads(patcher, typeof(JointController), "AssignAxisID");
                FloatReads(patcher, typeof(PistonController), "MakeFakeArm");
                FloatReads(patcher, typeof(PistonController), "DivideMesh");
                if (!StartupHarmonyBatch.Registered)
                    FloatReads(patcher, typeof(Build), "JOHIPODALCN");
                else
                    Log("STARTUP_TIMING float=Build.JOHIPODALCN shared-reuse stepMs=0");
                LogStartupTiming(startup, "float-reads", ref checkpoint);
                Patch(patcher, typeof(BlockData), "GetFixOffset", "FixOffsetPrefix", null);
                Patch(patcher, typeof(BlockData), "CopyAction", null, "CopyPostfix");
                Patch(patcher, typeof(BlockData), "_CheckMatchAction", null, "MatchPostfix");
                Patch(patcher, typeof(BlockData), "FlipParam", "CapturePrefix", "MirrorPostfix");
                Patch(patcher, typeof(BlockData), "InvertJointParam", "CapturePrefix", "InvertPostfix");
                Patch(patcher, typeof(BlockData), "RotCouplerParam", "CapturePrefix", "RotateOffsetPostfix");
                Patch(patcher, typeof(HIPBCCKFFAG), "CHAJBDNKDNJ", null, "PreviewPostfix");
                LogStartupTiming(startup, "core-hooks", ref checkpoint);
                SetupPrecisionShapes.Register(patcher);
                LogStartupTiming(startup, "shape-hooks", ref checkpoint);
                SetupPrecisionUi.Register(patcher);
                LogStartupTiming(startup, "ui-hooks", ref checkpoint);
                SetupPrecisionNetwork.Register(patcher);
                LogStartupTiming(startup, "network-hooks", ref checkpoint);
                IsRegistered = true;
                LogStartupTiming(startup, "complete", ref checkpoint);
                Log("REGISTERED version=35 enabled=" + Enabled + " toggle=Settings-P2/default-on gear=adjacent/generated-sprite settingsPage=reusable editor=" + EditorModeName
                    + " editorModes=vanilla-handle-input,digit-spinner,slider+digit-spinner selection=exclusive-toggle-group decimals=3 range=-500..500 scope=SETUP parts=Box,Capsule,Coupler,Rotator,Hinge,Piston sizes=Box/Capsule storage=props-v2 network=SETUP-v2 motion=owner-relay keys=A/D,S/Shift=center,W=signed-negate/unsigned-mirror sizeHotkeys=logical-0..250 safeMin=0.001 floatBounds=normalized wheel=setting alt=x10 ctrl=.100 ctrl+alt=.001 ctrlScope=A/D+wheel/all-editors carry=enabled preview=only-when-open buildUpdate=postfix+mapped-mode-key-guard arrows=native-sprite/14x8/gap3/current-native-row paste=world-no-panel-reopen cells=24x20/154,154,154/white-base/absolute-tint/no-reactivation hover=245,245,245/native-fade focus=pointer-only/root-raycast+bounds specials=ST,OP/FR,EE width=+16 offsetY=-7 hybrid=slider100/sameY/fraction-inset8/native-side-arrows24/no-direct-input vanillaInput=double-click/handle-contained/rendered-font-minus-one+preferred-width/no-wrap/fixed-3/comma/fraction-selected-post-activation/selection-replace/S-clears-fraction sign=17/white-outline");
            }
            catch (Exception error)
            {
                Log("REGISTER_FAILED " + error);
                try { patcher.UnpatchAll(PatchId); }
                catch (Exception rollbackError) { Log("ROLLBACK_FAILED " + rollbackError); }
                CouplerRotationNetwork.RestoreCouplerOnlyAfterSetupFailure();
            }
        }

        private static void LogStartupTiming(Stopwatch timer, string phase, ref long checkpoint)
        {
            long elapsed = timer.ElapsedMilliseconds;
            Log("STARTUP_TIMING phase=" + phase + " stepMs=" + (elapsed - checkpoint) + " totalMs=" + elapsed);
            checkpoint = elapsed;
        }

        internal static bool Enabled
        {
            get
            {
                settingsIngame settings = MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68;
                return IsRegistered && settings != null && settings.setupPrecision;
            }
        }

        internal static SetupPrecisionEditorMode EditorMode
        {
            get
            {
                settingsIngame settings = MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68;
                int value = settings == null ? 0 : settings.setupPrecisionEditorMode;
                return value >= 0 && value <= 2 ? (SetupPrecisionEditorMode)value : SetupPrecisionEditorMode.VanillaSlider;
            }
        }

        internal static string EditorModeName
        {
            get
            {
                switch (EditorMode)
                {
                    case SetupPrecisionEditorMode.DigitSpinner: return "digit-spinner";
                    case SetupPrecisionEditorMode.SliderDigitSpinner: return "slider+digit-spinner";
                    default: return "vanilla-slider";
                }
            }
        }

        internal static void SetEnabled(bool enabled)
        {
            settingsIngame settings = MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68;
            if (settings == null) return;
            settings.setupPrecision = enabled;
            settings.UUiRNMwxRbfk_Fs4cDErRoM();
            SetupPrecisionUi.OnSettingChanged();
            Log("SETTING enabled=" + enabled + " registered=" + IsRegistered
                + " editor=" + EditorModeName + " network=compatible-always");
        }

        internal static void SetEditorMode(int mode)
        {
            settingsIngame settings = MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68;
            if (settings == null) return;
            if (mode < 0 || mode > 2) mode = 0;
            if (settings.setupPrecisionEditorMode == mode) return;
            settings.setupPrecisionEditorMode = mode;
            settings.UUiRNMwxRbfk_Fs4cDErRoM();
            SetupPrecisionUi.OnSettingChanged();
            SetupPrecisionSettingsUi.Sync();
            Log("EDITOR_MODE value=" + mode + " name=" + EditorModeName + " network=unchanged");
        }

        internal static void Patch(Harmony patcher, Type owner, string method, string prefix, string postfix)
        {
            MethodInfo target = AccessTools.Method(owner, method);
            if (target == null) throw new MissingMethodException(owner.Name, method);
            patcher.Patch(target, prefix == null ? null : new HarmonyMethod(typeof(SetupPrecision), prefix),
                postfix == null ? null : new HarmonyMethod(typeof(SetupPrecision), postfix), null, null);
        }

        private static void FloatReads(Harmony patcher, Type owner, string name)
        {
            MethodInfo method = AccessTools.Method(owner, name);
            if (method == null) throw new MissingMethodException(owner.Name, name);
            HarmonyMethod transpiler = new HarmonyMethod(typeof(SetupPrecision), "FloatReadTranspiler");
            transpiler.priority = Priority.Last;
            patcher.Patch(method, null, null, transpiler, null);
        }

        // Replace only integer-array reads that immediately become floats. Layout,
        // indices, sizes and native discrete consumers keep their original types.
        internal static IEnumerable<CodeInstruction> FloatReadTranspiler(IEnumerable<CodeInstruction> instructions, MethodBase __originalMethod)
        {
            return RewriteFloatReads(instructions, __originalMethod.DeclaringType.Name + "." + __originalMethod.Name);
        }

        private static IEnumerable<CodeInstruction> RewriteFloatReads(IEnumerable<CodeInstruction> instructions, string target)
        {
            List<CodeInstruction> codes = new List<CodeInstruction>();
            foreach (CodeInstruction code in instructions) codes.Add(new CodeInstruction(code));
            int count = 0;
            for (int i = 0; i + 3 < codes.Count; i++)
            {
                FieldInfo field = codes[i].operand as FieldInfo;
                if (codes[i].opcode != OpCodes.Ldfld || field == null || field.DeclaringType != typeof(BlockData)
                    || (field.Name != "actionParam" && field.Name != "actionID") || !IndexLoad(codes[i + 1])
                    || codes[i + 2].opcode != OpCodes.Ldelem_I4) continue;
                int conversion = i + 3;
                if (codes[conversion].opcode == OpCodes.Neg) conversion++;
                bool offsetAction = conversion + 2 < codes.Count && codes[conversion].opcode == OpCodes.Ldc_I4
                    && Equals(codes[conversion].operand, 10000) && codes[conversion + 1].opcode == OpCodes.Add
                    && codes[conversion + 2].opcode == OpCodes.Conv_R4;
                if (offsetAction) conversion += 2;
                if (conversion >= codes.Count || codes[conversion].opcode != OpCodes.Conv_R4) continue;
                if (offsetAction) { codes[i + 3].opcode = OpCodes.Ldc_R4; codes[i + 3].operand = 10000f; }
                codes[i].opcode = OpCodes.Nop;
                codes[i].operand = null;
                codes[i + 2].opcode = OpCodes.Call;
                codes[i + 2].operand = AccessTools.Method(typeof(SetupPrecisionData), field.Name == "actionParam" ? "Read" : "ReadActionAngle");
                count++;
            }
            if (count == 0) throw new InvalidOperationException("No float reads in " + target);
            Log("FLOAT_READS target=" + target + " count=" + count);
            return codes;
        }

        private static bool IndexLoad(CodeInstruction code)
        {
            return code.opcode.Name.StartsWith("ldc.i4", StringComparison.Ordinal)
                || code.opcode.Name.StartsWith("ldloc", StringComparison.Ordinal) && !code.opcode.Name.StartsWith("ldloca", StringComparison.Ordinal);
        }

        private static bool FixOffsetPrefix(BlockData __instance, ref Vector3 aang, ref Vector3 pos, bool isFlip, ref bool __result)
        {
            BlockData block = __instance;
            if (!SetupPrecisionData.IsMechanism(block) || !SetupPrecisionData.HasAny(block)) return true;
            __result = false;
            if (block.gid != 7 && (block.gid >= 0 || aang != Vector3.zero)) return false;
            bool alternate = false;
            float value = 0;
            for (int i = 0; i < 8; i++)
                if (block.actionID[i] == 60)
                {
                    value = SetupPrecisionData.Read(block, i);
                    if (i < 7 && block.actionID[i + 1] == 60 && SetupPrecisionData.Read(block, i + 1) == value)
                        alternate = block.rgbI == 16777216;
                    break;
                }
            __result = true;
            if (block.type == BlockData.AAHMDBHDCDK.PistonL)
            {
                if (value >= 0 && value <= 1000) pos = block.GetAxisY() * value * ((isFlip ^ alternate) ? 0.01f : -0.01f);
                return false;
            }
            bool flip = isFlip;
            switch (block.type)
            {
                case BlockData.AAHMDBHDCDK.JointBA:
                    if (value >= -90 && value <= 90) { aang = block.GetAxisZ(); flip = block.index != 16 ? isFlip : !isFlip; }
                    break;
                case BlockData.AAHMDBHDCDK.JointPA:
                    if (value >= -90 && value <= 90) { aang = block.GetAxisX(); flip = (block.index == 2 || block.index == 3) ^ !isFlip; }
                    break;
                default:
                    if (value >= -180 && value <= 180) aang = block.GetAxisY();
                    break;
            }
            aang *= flip ? value : -value;
            Matrix4x4 matrix = BDLEJBBJJOI.IGMEGICHKOE(aang * (float)Math.PI / 180f);
            pos = block.GetPos() - matrix.MultiplyPoint(block.GetPos());
            return false;
        }

        private static void CopyPostfix(BlockData __instance, BlockData src) { SetupPrecisionData.Copy(__instance, src); }
        private static void MatchPostfix(BlockData __instance, BlockData other, ref bool __result)
        {
            if (__result) __result = SetupPrecisionData.Matches(__instance, other);
        }

        private sealed class SavedParameters
        {
            internal readonly float[] Values = new float[9];
            internal readonly int[] Actions = new int[8];
        }

        private static void CapturePrefix(BlockData __instance, out SavedParameters __state)
        {
            __state = null;
            if (!SetupPrecisionData.HasAny(__instance)) return;
            __state = new SavedParameters();
            Array.Copy(__instance.actionID, __state.Actions, 8);
            for (int slot = 0; slot < 9; slot++)
                if (SetupPrecisionData.Supports(__instance, slot)) __state.Values[slot] = SetupPrecisionData.Read(__instance, slot);
        }

        private static void MirrorPostfix(BlockData __instance, SavedParameters __state)
        {
            if (__state == null) return;
            if (__instance.type == BlockData.AAHMDBHDCDK.Coupler)
                foreach (int slot in new int[] { 0, 4, 5 }) SetupPrecisionData.Set(__instance, slot, -__state.Values[slot]);
            else if (__instance.type == BlockData.AAHMDBHDCDK.BoxGen || __instance.type == BlockData.AAHMDBHDCDK.CapGen)
                foreach (int slot in new int[] { 3, 7, 8 }) SetupPrecisionData.Set(__instance, slot, -__state.Values[slot]);
        }

        private static void InvertPostfix(BlockData __instance, SavedParameters __state)
        {
            if (__state == null) return;
            int lower = Array.IndexOf(__state.Actions, 65), upper = Array.IndexOf(__state.Actions, 66);
            for (int i = 0; i < 8; i++)
            {
                if (!SetupPrecisionData.Supports(__instance, i)) continue;
                float value = __state.Values[i];
                if (__state.Actions[i] < 62)
                {
                    int sentinel = __instance.CheckMask(896L) ? 101 : __instance.type == BlockData.AAHMDBHDCDK.JointTA ? 181 : 91;
                    if (Math.Abs(value) != sentinel) value = -value;
                }
                else if (lower >= 0 && upper >= 0 && (i == lower || i == upper)) value = __state.Values[i == lower ? upper : lower];
                SetupPrecisionData.Set(__instance, i, value);
            }
        }

        private static void RotateOffsetPostfix(BlockData __instance, Vector3 axis, SavedParameters __state)
        {
            if (__state == null || __instance.type != BlockData.AAHMDBHDCDK.Coupler) return;
            Vector3 offset = CouplerRotationMath.FromRotationVector(axis * 90f)
                * new Vector3(__state.Values[0], __state.Values[1], __state.Values[2]);
            for (int slot = 0; slot < 3; slot++) SetupPrecisionData.Set(__instance, slot, offset[slot]);
        }

        private static void PreviewPostfix(HIPBCCKFFAG __instance, BlockController HLEKLIGJLDL)
        {
            if (HLEKLIGJLDL == null) return;
            SetupPrecisionData.Prune(HLEKLIGJLDL.JNKEKNOAPHO);
            PersistPreview(__instance, HLEKLIGJLDL.JNKEKNOAPHO);
        }

        internal static void PersistPreview(HIPBCCKFFAG assembly, BlockData source)
        {
            if (assembly == null || !assembly.HCMMJPFOIHD || Build.GFJLEEJELOL == null) return;
            List<BlockData> blocks = (List<BlockData>)AccessTools.Field(typeof(HIPBCCKFFAG), "KLOGIIBKDEM").GetValue(assembly);
            List<int> indices = (List<int>)AccessTools.Field(typeof(HIPBCCKFFAG), "HLAFDKCFFGD").GetValue(assembly);
            int index = blocks.IndexOf(source);
            if (index >= 0 && index < indices.Count && indices[index] >= 0 && indices[index] < Build.GFJLEEJELOL.blockData.Count)
                SetupPrecisionData.Copy(Build.GFJLEEJELOL.blockData[indices[index]], source);
        }

        internal static void Log(string message)
        {
            try { global::mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng("[SETUP-PRECISION] " + message); }
            catch (Exception) { }
        }
    }
}
