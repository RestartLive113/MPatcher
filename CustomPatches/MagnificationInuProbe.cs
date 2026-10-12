using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using HarmonyLib;
using McnCraft;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
    // Explicit offline fixture only. Observes native protection without suppressing it.
    internal static class MagnificationInuProbe
    {
        internal static readonly bool Requested = Array.IndexOf(Environment.GetCommandLineArgs(), "--mpatcher-inu-safety-probe") >= 0;
        internal static readonly bool Comparison = Requested && Array.IndexOf(Environment.GetCommandLineArgs(), "--mpatcher-inu-safety-comparison") >= 0;
        internal static bool Baseline { get { return Comparison && MagnificationCraftProbe.RunIndex >= 0 && MagnificationCraftProbe.RunIndex < 2; } }
        private static readonly Dictionary<string, int> warnings = new Dictionary<string, int>();
        private static readonly FieldInfo Current = AccessTools.Field(typeof(MachineController), "NIJMNHOBBEF");
        private static readonly FieldInfo Previous = AccessTools.Field(typeof(MachineController), "BIFHEBBFPAD");
        private static readonly FieldInfo WarpScore = AccessTools.Field(typeof(MachineController), "GOOJJFCGEOH");
        private static readonly FieldInfo Counter = AccessTools.Field(typeof(JointController), "HEJAMHDHKJG");
        private static readonly FieldInfo Power = AccessTools.Field(typeof(JointController), "OCGBGKBDJPL");
        private static int observedRun = -1;
        private static long energyCalls;
        private static double energySuppressed;
        private static readonly Dictionary<int, float> energyMinimum = new Dictionary<int, float>();
        internal static bool IsTestRoot(string root)
        {
            root = Path.GetFullPath(root).TrimEnd('\\', '/');
            string profile = Environment.GetEnvironmentVariable("USERPROFILE") ?? "";
            return string.Equals(root, Path.Combine(profile, "Downloads\\MachineCraftMPatcherTest"), StringComparison.OrdinalIgnoreCase)
                || (Requested && string.Equals(root, Path.Combine(profile, "Documents\\MachineCraftMP\\analysis\\magnification-inu-300-20260930\\runtime"), StringComparison.OrdinalIgnoreCase));
        }
        internal static void Configure(string craft, bool nativeControl, ref int[] scales, ref int[] models, ref float massUnit)
        {
            if (!Requested) return;
            if (craft != "Inu-Sangue" || nativeControl) throw new InvalidOperationException("inu-probe-requires-Inu-Sangue-model22");
            scales = Comparison ? new[] { 100, 300, 100, 300 } : new[] { 100, 300 }; models = Comparison ? new[] { 22, 22, 22, 22 } : new[] { 22, 22 }; massUnit = 1000000f;
            Log("INU_CONFIG order=100,300 alpha=R66 non100MassUnit=1000000 input=idle-W-coast-S-coast-ShiftOnly fixture=native-ground saves=blocked comparison=" + Comparison);
        }
        internal static void Register(Harmony harmony)
        {
            if (!Requested) return;
            foreach (FieldInfo field in new[] { Current, Previous, WarpScore, Counter, Power })
                if (field == null) throw new MissingFieldException("inu-safety-observer-field");
            harmony.Patch(AccessTools.Method(typeof(DP), "D", new[] { typeof(object), typeof(int) }), new HarmonyMethod(AccessTools.Method(typeof(MagnificationInuProbe), "ObserveObject")), null, null, null);
            harmony.Patch(AccessTools.Method(typeof(DP), "CD", new[] { typeof(string) }), new HarmonyMethod(AccessTools.Method(typeof(MagnificationInuProbe), "ObserveText")), null, null, null);
            harmony.Patch(AccessTools.DeclaredMethod(typeof(MachineController), "UseEnergy"), new HarmonyMethod(AccessTools.Method(typeof(MagnificationInuProbe), "UseEnergy")), null, null, null);
            Log("INU_OBSERVER_REGISTERED nativeWarnings=retained snapshots=read-only energyConsumption=zero scope=explicit-offline-fixture-only");
        }
        private static bool UseEnergy(float __0)
        {
            if (!Requested || MagnificationCraftProbe.RunIndex < 0) return true;
            energyCalls++; energySuppressed += Math.Max(0f, __0); return false;
        }
        internal static void EnergySettings()
        {
            if (!Requested) return;
            JKGKJLLFMLE.IGOBPLOLHEP.practiceChargeRate = 999;
            JKGKJLLFMLE.MHJBJGEFECP = 9.99f;
            Log("INU_ENERGY_CONFIG run=" + MagnificationCraftProbe.RunIndex + " consumption=zero chargePercent=999 scope=fixture-in-memory nativeSaves=blocked");
        }
        private static void ObserveObject(object __0) { ObserveText(__0 == null ? "" : __0.ToString()); }
        private static void ObserveText(string __0)
        {
            if (!Requested || __0 == null || !__0.StartsWith("WRN:")) return;
            string kind = __0.Contains("is dislocated") ? "dislocated" : __0.Contains("Irregular acceleration") ? "acceleration" : __0.Contains("High-speed vibration") ? "vibration" : __0.Contains("warp") ? "warp" : "other";
            int count; string key = MagnificationCraftProbe.RunIndex + ":" + kind;
            warnings.TryGetValue(key, out count); warnings[key] = ++count;
            if (count <= 8) Log("INU_NATIVE_WARNING run=" + MagnificationCraftProbe.RunIndex + " kind=" + kind + " index=" + count + " message=" + __0);
        }
        internal static void Sample(MachineController machine, int frame)
        {
            if (!Requested) return;
            int run = MagnificationCraftProbe.RunIndex;
            float minimum; if (!energyMinimum.TryGetValue(run, out minimum)) minimum = 1f;
            float energy = machine.GetEnergyRate(); energyMinimum[run] = Mathf.Min(minimum, energy);
            if (frame != 1 && frame % 100 != 0) return;
            if (observedRun != run) { observedRun = run; Log("INU_RUN run=" + run + " scale=" + F(machine.DJCDFLDHPHK) + " safetyFix=" + !Baseline); }
            float scale = machine.DJCDFLDHPHK, maxGap = 0f, maxAngular = 0f, maxVelocity = 0f;
            int joints = 0, rawOver = 0, relativeOver = 0, recovering = 0, colliders = 0;
            string worst = "none"; HashSet<int> seen = new HashSet<int>();
            List<GameObject> roots = new List<GameObject> { machine.gameObject }; roots.AddRange(machine.KBLANAFAJFP);
            foreach (GameObject root in roots) if (root != null)
            {
                foreach (Collider c in root.GetComponentsInChildren<Collider>(true)) if (seen.Add(c.GetInstanceID())) colliders++;
                foreach (Rigidbody body in root.GetComponentsInChildren<Rigidbody>(true)) if (seen.Add(body.GetInstanceID()))
                { maxAngular = Mathf.Max(maxAngular, body.angularVelocity.magnitude); maxVelocity = Mathf.Max(maxVelocity, body.velocity.magnitude); }
                foreach (JointController controller in root.GetComponentsInChildren<JointController>(true))
                {
                    if (!seen.Add(controller.GetInstanceID()) || controller.ALGHGLCBLDJ == null || controller.ALGHGLCBLDJ.connectedBody == null) continue;
                    HingeJoint joint = controller.ALGHGLCBLDJ;
                    float gap = (joint.transform.TransformPoint(joint.anchor) - joint.connectedBody.transform.TransformPoint(joint.connectedAnchor)).magnitude;
                    joints++; if (gap * gap > .2f) rawOver++; if (gap * gap / (scale * scale) > .2f) relativeOver++;
                    if ((int)Counter.GetValue(controller) > 0) recovering++;
                    if (gap > maxGap) { maxGap = gap; worst = controller.JNKEKNOAPHO.GetPosString(true) + ":power=" + Power.GetValue(controller); }
                }
            }
            Log("INU_SAMPLE run=" + run + " frame=" + frame + " scale=" + F(scale) + " joints=" + joints + " colliders=" + colliders
                + " gapWorld=" + F(maxGap) + " gapCraft=" + F(maxGap / scale) + " rawOver=" + rawOver + " relativeOver=" + relativeOver + " recovering=" + recovering
                + " worst=" + worst + " velocityMax=" + F(maxVelocity) + " velocityCraft=" + F(maxVelocity / scale) + " angularMax=" + F(maxAngular)
                + " energy=" + F(energy) + " energyMin=" + F(energyMinimum[run]) + " displacement=" + Current.GetValue(machine) + " previousDisplacement=" + Previous.GetValue(machine) + " warpScore=" + WarpScore.GetValue(machine));
        }
        internal static void Flush()
        {
            if (!Requested) return;
            Log("INU_ENERGY_SUMMARY calls=" + energyCalls + " nativeConsumptionSuppressed=" + energySuppressed.ToString("R", CultureInfo.InvariantCulture));
            foreach (KeyValuePair<int, float> entry in energyMinimum) Log("INU_ENERGY_MIN run=" + entry.Key + " rate=" + F(entry.Value));
            foreach (KeyValuePair<string, int> entry in warnings) Log("INU_WARNINGS runKind=" + entry.Key + " count=" + entry.Value);
        }
        private static string F(float value) { return value.ToString("R", CultureInfo.InvariantCulture); }
        private static void Log(string text) { MagnificationDownscale.Log(text); }
    }
}
