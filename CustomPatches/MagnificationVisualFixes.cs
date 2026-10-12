using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using McnCraft;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
    internal static class MagnificationVisualFixes
    {
        private static readonly FieldInfo SmokeScale = AccessTools.Field(typeof(SmokeController), "MEAKADBPCJA");
        private static readonly FieldInfo ArenaMachine = AccessTools.Field(typeof(Arena), "FICMBCLEFDL");
        private static readonly HashSet<int> LoggedLaunchers = new HashSet<int>();

        internal static void Register(Harmony harmony)
        {
            if (SmokeScale == null || ArenaMachine == null) throw new MissingFieldException("Magnification visual fields");
            harmony.Patch(AccessTools.DeclaredMethod(typeof(SmokeController), "Update"), null, null,
                new HarmonyMethod(AccessTools.Method(typeof(MagnificationVisualFixes), "SmokeTranspiler")), null);
            harmony.Patch(AccessTools.DeclaredMethod(typeof(CannonController), "CheckBodyInclusion", new[] { typeof(Bounds) }),
                new HarmonyMethod(AccessTools.Method(typeof(MagnificationVisualFixes), "CannonPrefix")), null, null, null);
            harmony.Patch(AccessTools.DeclaredMethod(typeof(LauncherController), "Update"), null, null,
                new HarmonyMethod(AccessTools.Method(typeof(MagnificationVisualFixes), "LauncherTranspiler")), null);
            harmony.Patch(AccessTools.DeclaredMethod(typeof(Arena), "OBBFPAMAMDE"), null, null,
                new HarmonyMethod(AccessTools.Method(typeof(MagnificationVisualFixes), "ScopeTranspiler")), null);
            MagnificationDownscale.Log("VISUAL_FIXES_REGISTERED smoke=size+offset scope=position+near-clip cannon=body-inclusion launcher=scaled-capsule-clearance");
        }

        private static float SmokePadding(SmokeController smoke) { return 2f * SmokeVisualScale(smoke); }
        private static float SmokeOffset(SmokeController smoke) { return 0.2f * SmokeVisualScale(smoke); }
        private static float SmokeVisualScale(SmokeController smoke)
        {
            if (smoke == null) return 1f;
            return MagnificationPhysicsNormalization.ScaleFactor((float)SmokeScale.GetValue(smoke));
        }

        private static IEnumerable<CodeInstruction> SmokeTranspiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> codes = MagnificationDownscale.Copy(instructions);
            int size = 0, offset = 0;
            for (int i = codes.Count - 3; i >= 0; i--)
            {
                if (MagnificationDownscale.Float(codes[i], 2f) && codes[i + 1].opcode == OpCodes.Add
                    && IsCall(codes[i + 2], typeof(ParticleSystem), "set_startSize"))
                {
                    ReplaceConstant(codes, i, "SmokePadding"); size++;
                }
                else if (MagnificationDownscale.Float(codes[i], 0.2f) && codes[i + 1].opcode == OpCodes.Add
                    && IsCall(codes[i + 2], typeof(Vector3), "op_Multiply"))
                {
                    ReplaceConstant(codes, i, "SmokeOffset"); offset++;
                }
            }
            MagnificationDownscale.Require(size == 1 && offset == 1, "smoke size and emission offset", size + offset);
            return codes;
        }

        private static void ReplaceConstant(List<CodeInstruction> codes, int index, string helper)
        {
            CodeInstruction original = codes[index];
            CodeInstruction argument = new CodeInstruction(OpCodes.Ldarg_0);
            argument.labels.AddRange(original.labels);
            argument.blocks.AddRange(original.blocks);
            codes[index] = argument;
            codes.Insert(index + 1, new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(MagnificationVisualFixes), helper)));
        }

        private static void CannonPrefix(CannonController __instance, ref Bounds __0)
        {
            if (__instance == null || __instance.FPEBEEJGFPI == null) return;
			if (!MagnificationPhysicsMode.Applies(__instance.FPEBEEJGFPI)) return;
            float scale = MagnificationPhysicsNormalization.MachineScale(__instance.FPEBEEJGFPI);
            if (scale == 1f) return;
            Vector3 origin = __instance.transform.position;
            __0 = new Bounds(origin + (__0.center - origin) / scale, __0.size / scale);
        }

        private static IEnumerable<CodeInstruction> LauncherTranspiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> codes = MagnificationDownscale.Copy(instructions);
            int found = 0;
            for (int i = codes.Count - 1; i >= 2; i--)
            {
                if (!IsCall(codes[i], typeof(Physics), "CheckCapsule")
                    || codes[i - 1].opcode != OpCodes.Ldc_I4 || Convert.ToInt32(codes[i - 1].operand) != 1064961
                    || !MagnificationDownscale.Field(codes[i - 2], OpCodes.Ldfld, typeof(MissileController), "CLLDAFNNBLE")) continue;
                CodeInstruction owner = new CodeInstruction(OpCodes.Ldarg_0);
                owner.labels.AddRange(codes[i].labels); codes[i].labels.Clear();
                owner.blocks.AddRange(codes[i].blocks); codes[i].blocks.Clear();
                codes.Insert(i, owner);
                codes[i + 1].opcode = OpCodes.Call;
                codes[i + 1].operand = AccessTools.Method(typeof(MagnificationVisualFixes), "LauncherClearance");
                found++;
            }
            MagnificationDownscale.Require(found == 1, "one launcher ground/wall capsule check", found);
            return codes;
        }

        private static Vector3 ScaledEndpoint(Vector3 point, Vector3 origin, float axialScale)
        {
            return origin + (point - origin) * axialScale;
        }

        private static float ScaledRadius(float radius, Vector3 lossyScale)
        {
            return radius * Math.Max(Math.Abs(lossyScale.x), Math.Abs(lossyScale.z));
        }

        private static bool LauncherClearance(Vector3 first, Vector3 second, float radius, int layerMask,
            LauncherController launcher)
        {
            if (launcher == null || launcher.FPEBEEJGFPI == null
				|| !MagnificationPhysicsMode.Applies(launcher.FPEBEEJGFPI)
                || MagnificationPhysicsNormalization.MachineScale(launcher.FPEBEEJGFPI) == 1f
                || launcher.HFCKFMJMHGA == null)
                return Physics.CheckCapsule(first, second, radius, layerMask);
            Transform rocket = launcher.HFCKFMJMHGA.transform;
            Vector3 lossy = rocket.lossyScale;
            float axial = Math.Abs(lossy.y);
            float worldRadius = ScaledRadius(radius, lossy);
            if (!(axial > 0f) || float.IsNaN(axial) || float.IsInfinity(axial)
                || !(worldRadius > 0f) || float.IsNaN(worldRadius) || float.IsInfinity(worldRadius))
                return Physics.CheckCapsule(first, second, radius, layerMask);
            Vector3 origin = rocket.position;
            bool blocked = Physics.CheckCapsule(ScaledEndpoint(first, origin, axial),
                ScaledEndpoint(second, origin, axial), worldRadius, layerMask);
            if (LoggedLaunchers.Count < 32 && LoggedLaunchers.Add(launcher.GetInstanceID()))
            {
                bool nativeBlocked = Physics.CheckCapsule(first, second, radius, layerMask);
                MagnificationDownscale.Log("LAUNCHER_CLEARANCE machine=" + launcher.FPEBEEJGFPI.GetInstanceID()
                    + " visualScale=" + MagnificationPhysicsNormalization.MachineScale(launcher.FPEBEEJGFPI)
                    + " rocketScale=" + lossy + " nativeRadius=" + radius + " worldRadius=" + worldRadius
                    + " nativeBlocked=" + nativeBlocked + " blocked=" + blocked);
            }
            return blocked;
        }

        private static float ScopeOffset(float value, Arena arena)
        {
            MachineController machine = ArenaMachine.GetValue(arena) as MachineController;
            return value * MagnificationPhysicsNormalization.MachineScale(machine);
        }

        private static float ScopeNearClip(float value, Arena arena)
        {
            MachineController machine = ArenaMachine.GetValue(arena) as MachineController;
            return machine != null && machine.NJJHFDDAFPO > 0
                ? value * MagnificationPhysicsNormalization.MachineScale(machine) : value;
        }

        private static IEnumerable<CodeInstruction> ScopeTranspiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> codes = MagnificationDownscale.Copy(instructions);
            int position = 0, near = 0, average = 0;
            for (int i = codes.Count - 1; i >= 0; i--)
            {
                if (codes[i].opcode == OpCodes.Sub && i >= 8 && MagnificationDownscale.Float(codes[i - 2], 0.003f)
                    && MagnificationDownscale.Float(codes[i - 8], 0.34f))
                {
                    InsertScopeConversion(codes, i + 1, "ScopeOffset"); position++;
                }
                else if (IsCall(codes[i], typeof(RideCameraController), "SetNearClip"))
                {
                    InsertScopeConversion(codes, i, "ScopeNearClip"); near++;
                }
                else if (MagnificationDownscale.Float(codes[i], 0.2f) && i + 2 < codes.Count
                    && IsCall(codes[i + 1], typeof(Vector3), "op_Multiply")
                    && IsCall(codes[i + 2], typeof(Vector3), "op_Addition"))
                {
                    ReplaceScopeConstant(codes, i); average++;
                }
            }
            MagnificationDownscale.Require(position == 1 && near == 1 && average == 1,
                "scope focus/near clip/average", position + near + average);
            return codes;
        }

        private static void ReplaceScopeConstant(List<CodeInstruction> codes, int index)
        {
            CodeInstruction original = codes[index];
            CodeInstruction value = new CodeInstruction(OpCodes.Ldc_R4, 0.2f);
            value.labels.AddRange(original.labels);
            value.blocks.AddRange(original.blocks);
            codes[index] = value;
            InsertScopeConversion(codes, index + 1, "ScopeOffset");
        }

        private static void InsertScopeConversion(List<CodeInstruction> codes, int index, string helper)
        {
            CodeInstruction argument = new CodeInstruction(OpCodes.Ldarg_0);
            if (index < codes.Count)
            {
                argument.labels.AddRange(codes[index].labels); codes[index].labels.Clear();
                argument.blocks.AddRange(codes[index].blocks); codes[index].blocks.Clear();
            }
            codes.InsertRange(index, new[] { argument, new CodeInstruction(OpCodes.Call,
                AccessTools.Method(typeof(MagnificationVisualFixes), helper)) });
        }

        private static bool IsCall(CodeInstruction code, Type type, string name)
        {
            MethodInfo method = code.operand as MethodInfo;
            return (code.opcode == OpCodes.Call || code.opcode == OpCodes.Callvirt)
                && method != null && method.DeclaringType == type && method.Name == name;
        }
    }
}
