using System;
using System.IO;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
    // The stress suite exercises the exact native adapter shipped by the candidate.
    internal static class MagnificationNativeJointProbe
    {
        internal static bool IsIsolatedProcess()
        {
            string root = Path.GetFullPath(Path.Combine(Application.dataPath, ".."));
            string expected = Path.Combine(Environment.GetEnvironmentVariable("USERPROFILE") ?? string.Empty, "Downloads\\MachineCraftMPatcherTest");
            return string.Equals(root.TrimEnd('\\', '/'), expected, StringComparison.OrdinalIgnoreCase)
                && Array.IndexOf(Environment.GetCommandLineArgs(), "--mpatcher-magnification-stress-probe") >= 0;
        }
        private static bool Allowed(Joint joint)
        {
            return IsIsolatedProcess() && joint != null && joint.name.StartsWith("StressProbe/", StringComparison.Ordinal);
        }
        internal static bool Balance(Joint joint, float ratio, bool inertia)
        {
            if (!Allowed(joint)) { MagnificationDownscale.Log("NATIVE_JOINT_PROBE_REFUSED"); return false; }
            return MagnificationJointMassNative.Balance(joint, ratio, inertia);
        }
        internal static void Inspect(Joint joint)
        {
            if (Allowed(joint)) MagnificationJointMassNative.Inspect(joint);
        }
        internal static void Release() { MagnificationJointMassNative.Release(); }
    }
}
