using System;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
    // Harmony captures only these core methods. Mutable UI behavior lives in
    // IScaleRuntime, so a module-only rebuild does not invalidate captured IL.
    internal static class SculptScaleUi
    {
        private const string PatchId = "local.moddev.machinecraft.sculpt-scale-ui.v1";
        private static readonly MethodInfo RawKeyDown = AccessTools.Method(typeof(HOCGCCAIPFF),
            "FGCCNKAIKAI", new Type[] { typeof(KeyCode) });
        private static bool registered;

        internal static void TryRegister()
        {
            if (registered) return;
            Harmony patcher = new Harmony(PatchId);
            try
            {
                MethodInfo sculpt = AccessTools.Method(typeof(Build), "IPFFLBAMPNG", Type.EmptyTypes);
                MethodInfo open = AccessTools.Method(typeof(Build), "FBBJKNANFAG");
                MethodInfo mask = AccessTools.Method(typeof(Build), "KFMHIFEBOLK", new Type[] { typeof(int), typeof(bool) });
                if (RawKeyDown == null || sculpt == null || open == null || mask == null)
                    throw new MissingMemberException("Stable Scale UI hook targets");
                patcher.Patch(sculpt, null, null, new HarmonyMethod(typeof(SculptScaleUi), "SculptKeyTranspiler"));
                patcher.Patch(open, new HarmonyMethod(typeof(SculptScaleUi), "NativeTabPrefix"));
                patcher.Patch(mask, new HarmonyMethod(typeof(SculptScaleUi), "NativeMaskPrefix"));
                registered = true;
                Log("MASK_REGISTERED target=Build.KFMHIFEBOLK(int,bool) behavior=external-module");
                Log("REGISTERED entry=2 hooks=stable-core behavior=external-module");
            }
            catch (Exception error)
            {
                try { patcher.UnpatchAll(PatchId); } catch (Exception) { }
                Log("REGISTER_FAILED " + error);
            }
        }

        private static void NativeTabPrefix(Build __instance)
        {
            IScaleRuntime module = ScaleRuntimeHost.Runtime;
            if (module != null) module.BeforeNativeTab(__instance);
        }

        private static void NativeMaskPrefix(Build __instance)
        {
            IScaleRuntime module = ScaleRuntimeHost.Runtime;
            if (module != null) module.BeforeNativeMask(__instance);
        }

        private static IEnumerable<CodeInstruction> SculptKeyTranspiler(IEnumerable<CodeInstruction> instructions)
        {
            MethodInfo replacement = AccessTools.Method(typeof(SculptScaleUi), "SculptSubmodeKeyDown");
            List<CodeInstruction> result = new List<CodeInstruction>();
            int replaced = 0;
            foreach (CodeInstruction source in instructions)
            {
                CodeInstruction instruction = new CodeInstruction(source);
                if (Equals(instruction.operand, RawKeyDown))
                {
                    instruction.operand = replacement;
                    replaced++;
                }
                result.Add(instruction);
            }
            if (replaced != 2) throw new InvalidOperationException("Native Sculpt key reads changed: " + replaced);
            Log("SCULPT_KEY_GUARD replaced=" + replaced + " callback=stable-core");
            return result;
        }

        private static bool SculptSubmodeKeyDown(KeyCode key)
        {
            // If the optional module could not load, retain native Sculpt input.
            return (ScaleRuntimeHost.Runtime == null || key != KeyCode.Alpha2) && HOCGCCAIPFF.FGCCNKAIKAI(key);
        }

        internal static bool FilterMappedKeyDown(SystemData.EHLMFKOOHLI action, bool pressed)
        {
            IScaleRuntime module = ScaleRuntimeHost.Runtime;
            return module == null ? pressed : module.FilterMappedKeyDown(action, pressed);
        }

        internal static bool IsScaleActive(Build owner)
        {
            IScaleRuntime module = ScaleRuntimeHost.Runtime;
            return module != null && module.IsScaleActive(owner);
        }

        internal static bool NumericInputFocused(Build owner)
        {
            IScaleRuntime module = ScaleRuntimeHost.Runtime;
            return module != null && module.NumericInputFocused(owner);
        }

        internal static bool OwnsCamera(CameraController camera)
        {
            IScaleRuntime module = ScaleRuntimeHost.Runtime;
            return module != null && module.OwnsCamera(camera);
        }

        internal static bool CameraKeyboardAllowed
        {
            get { IScaleRuntime module = ScaleRuntimeHost.Runtime; return module != null && module.CameraKeyboardAllowed; }
        }

        internal static bool CameraLookHeld
        {
            get { IScaleRuntime module = ScaleRuntimeHost.Runtime; return module != null && module.CameraLookHeld; }
        }

        private static void Log(string message) { ScaleRuntimeHost.Log("[SCULPT-SCALE-UI] " + message); }
    }
}
