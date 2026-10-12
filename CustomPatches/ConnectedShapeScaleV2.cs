using System;
using System.Reflection;
using HarmonyLib;

namespace MPatcherFork.CustomPatches
{
    // Stable Build.Start hook. No solver/UI implementation is embedded here.
    internal static class ConnectedShapeScaleV2
    {
        private const string PatchId = "local.moddev.machinecraft.connected-shape-scale.v2";
        private static bool registered;
        internal static void TryRegister()
        {
            if (registered) return;
            Harmony patcher = new Harmony(PatchId);
            try
            {
                MethodInfo start = AccessTools.Method(typeof(Build), "Start", Type.EmptyTypes);
                if (start == null) throw new MissingMethodException("Build.Start");
                patcher.Patch(start, null, new HarmonyMethod(typeof(ConnectedShapeScaleV2), "BuildStartPostfix"));
                registered = true;
                ScaleRuntimeHost.Log("[CONNECTED-SHAPE-SCALE] REGISTERED target=Build.Start hooks=stable-core behavior=external-module");
                ScaleRuntimeHost.TryLoad();
            }
            catch (Exception error)
            {
                ScaleRuntimeHost.Log("[CONNECTED-SHAPE-SCALE] REGISTER_FAILED " + error);
                try { patcher.UnpatchAll(PatchId); } catch (Exception) { }
            }
        }

        private static void BuildStartPostfix(Build __instance)
        {
            IScaleRuntime module = ScaleRuntimeHost.Runtime;
            if (module != null) module.Attach(__instance);
        }
    }
}
