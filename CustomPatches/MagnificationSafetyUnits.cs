using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using McnCraft;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
    // Same-time model: world displacement and its per-tick change scale with L.
    // Native stop/recovery actions remain intact; compare equivalent craft units.
    internal static class MagnificationSafetyUnits
    {
        private static readonly HashSet<int> logged = new HashSet<int>();
        internal static float Threshold(float value, float scale, bool squared, bool enabled)
        {
            float s = MagnificationScalePolicy.Valid(scale);
            return !enabled || s == 1f ? value : squared ? value * (s * s) : value * s;
        }
        internal static float GapSquared(float value, float scale, bool enabled)
        {
            float s = MagnificationScalePolicy.Valid(scale);
            return !enabled || s == 1f ? value : value / (s * s);
        }
        private static float Scale(MachineController machine)
        {
            if (!MagnificationScalePolicy.SameTime || MagnificationInuProbe.Baseline) return 1f;
            float scale = MagnificationPhysicsMode.PhysicsScale(machine);
            if (scale != 1f && logged.Add(machine.GetInstanceID()))
                MagnificationDownscale.Log("SAFETY_UNITS_ACTIVE machine=" + machine.GetInstanceID() + " scale=" + scale.ToString("R", System.Globalization.CultureInfo.InvariantCulture)
                    + " accelerationLimit=" + (3f * scale).ToString("R", System.Globalization.CultureInfo.InvariantCulture)
                    + " gapSquaredUnits=craft nativeStops=retained worldPositions=unchanged");
            return scale;
        }
        private static float MachineLength(float value, MachineController owner) { return Threshold(value, Scale(owner), false, true); }
        private static float MachineSquared(float value, MachineController owner) { return Threshold(value, Scale(owner), true, true); }
        private static float JointGap(float value, JointController owner) { return GapSquared(value, Scale(owner.FPEBEEJGFPI), true); }
        internal static void Register(Harmony harmony)
        {
            foreach (Type type in new[] { typeof(MachineController), typeof(JointController) })
            {
                MethodInfo target = AccessTools.DeclaredMethod(type, "FixedUpdate");
                harmony.Patch(target, null, null, new HarmonyMethod(AccessTools.Method(typeof(MagnificationSafetyUnits), "Transpiler")), null);
                MagnificationDownscale.Log("SAFETY_UNITS_HOOK target=" + type.Name + ".FixedUpdate active=model22 nativeProtection=retained baseline100=direct");
            }
        }
        private static bool Field(CodeInstruction c, string name) { return MagnificationDownscale.Field(c, OpCodes.Ldfld, typeof(MachineController), name); }
        private static bool Call(CodeInstruction c, Type type, string name)
        { MethodInfo m = c.operand as MethodInfo; return m != null && m.DeclaringType == type && m.Name == name && (c.opcode == OpCodes.Call || c.opcode == OpCodes.Callvirt); }
        internal static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, MethodBase __originalMethod)
        {
            bool machine = __originalMethod.DeclaringType == typeof(MachineController);
            if (__originalMethod.Name != "FixedUpdate" || __originalMethod.MetadataToken != (machine ? 0x060041C8 : 0x06003DD3)
                || (!machine && __originalMethod.DeclaringType != typeof(JointController))) throw new InvalidOperationException("unaudited-safety-target " + __originalMethod);
            List<CodeInstruction> codes = MagnificationDownscale.Copy(instructions);
            int acceleration = 0, teleport = 0, warp = 0, gap = 0;
            for (int i = codes.Count - 5; i >= 5; i--)
            {
                string helper = null; int insert = i + 1;
                if (machine)
                {
                    if (MagnificationDownscale.Float(codes[i], 3f) && codes[i - 1].opcode == OpCodes.Sub
                        && Field(codes[i - 2], "BIFHEBBFPAD") && Field(codes[i - 4], "NIJMNHOBBEF")
                        && codes[i + 1].opcode == OpCodes.Ble_Un && Field(codes[i + 3], "PNIEOHABEEM"))
                    { helper = "MachineLength"; acceleration++; }
                    else if (MagnificationDownscale.Float(codes[i], 10000f) && Call(codes[i - 1], typeof(Vector3), "get_sqrMagnitude") && codes[i + 1].opcode == OpCodes.Ble_Un)
                    { helper = "MachineSquared"; teleport++; }
                    else if ((MagnificationDownscale.Float(codes[i], 5f) || MagnificationDownscale.Float(codes[i], 1000f))
                        && Field(codes[i - 1], "GOOJJFCGEOH") && codes[i + 1].opcode == OpCodes.Ble_Un)
                    { helper = "MachineLength"; warp++; }
                }
                else if (Call(codes[i], typeof(Vector3), "get_sqrMagnitude") && codes[i + 1].opcode == OpCodes.Stloc_S
                    && codes[i + 2].opcode == OpCodes.Ldloc_S && Equals(codes[i + 1].operand, codes[i + 2].operand)
                    && MagnificationDownscale.Float(codes[i + 3], .2f) && codes[i + 4].opcode == OpCodes.Ble_Un)
                { helper = "JointGap"; gap++; }
                if (helper == null) continue;
                CodeInstruction owner = new CodeInstruction(OpCodes.Ldarg_0);
                owner.labels.AddRange(codes[insert].labels); codes[insert].labels.Clear();
                owner.blocks.AddRange(codes[insert].blocks); codes[insert].blocks.Clear();
                codes.InsertRange(insert, new[] { owner, new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(MagnificationSafetyUnits), helper)) });
            }
            MagnificationDownscale.Require(machine ? acceleration == 1 && teleport == 1 && warp == 2 && gap == 0 : gap == 1 && acceleration == 0 && teleport == 0 && warp == 0,
                "safety semantic sites " + __originalMethod.DeclaringType.Name, acceleration + teleport + warp + gap);
            return codes;
        }
    }
}
