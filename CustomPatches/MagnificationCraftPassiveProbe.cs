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
    // Isolated diagnostic only. Freeze native control after Start; retain the
    // constructed joints, contacts, body parameters and current dynamic state.
    internal static class MagnificationCraftPassiveProbe
    {
        internal static readonly bool Comparison = Array.IndexOf(Environment.GetCommandLineArgs(), "--mpatcher-craft-passive-comparison") >= 0;
        internal static readonly bool Requested = Comparison || Array.IndexOf(Environment.GetCommandLineArgs(), "--mpatcher-craft-passive") >= 0;
        internal static bool Active { get { return Requested && (!Comparison || MagnificationCraftProbe.RunIndex >= 2); } }
        private static readonly HashSet<int> isolated = new HashSet<int>();
        private static readonly HashSet<MethodBase> patched = new HashSet<MethodBase>();
        private static readonly HashSet<string> callbacks = new HashSet<string>(new[]
        {
            "Update", "FixedUpdate", "LateUpdate", "OnEnable", "OnDisable",
            "OnCollisionEnter", "OnCollisionStay", "OnCollisionExit",
            "OnTriggerEnter", "OnTriggerStay", "OnTriggerExit", "OnJointBreak"
        });
        private static MachineController attached;

        private static void RequireTestProcess()
        {
            string root = Path.GetFullPath(Path.Combine(Application.dataPath, ".."));
            string expected = Path.GetFullPath(Path.Combine(Environment.GetEnvironmentVariable("USERPROFILE") ?? "", "Downloads\\MachineCraftMPatcherTest"));
            if (!string.Equals(root.TrimEnd('\\', '/'), expected.TrimEnd('\\', '/'), StringComparison.OrdinalIgnoreCase)
                || Array.IndexOf(Environment.GetCommandLineArgs(), "--mpatcher-magnification-craft-probe") < 0)
                throw new InvalidOperationException("passive-requires-isolated-test-root-and-craft-probe");
        }

        internal static void Configure(string craft, bool nativeControl, float massUnit, ref int[] scales, ref int[] models)
        {
            if (!Requested) return;
            RequireTestProcess();
            if (craft != "Car" || nativeControl || massUnit != 1f) throw new InvalidOperationException("passive-requires-Car-model22-unit1");
            if (Comparison)
            {
                if (Array.IndexOf(Environment.GetCommandLineArgs(), "--mpatcher-craft-similarity") < 0
                    || Array.IndexOf(Environment.GetCommandLineArgs(), "--mpatcher-craft-rotor-passive") >= 0)
                    throw new InvalidOperationException("passive-comparison-requires-similarity-without-rotor-isolation");
                scales = new[] { 100, 5, 100, 5 };
                models = new[] { 22, 22, 22, 22 };
                MagnificationDownscale.Log("CRAFT_PASSIVE_COMPARISON_CONFIG runs=4 model=22 order=ordinary100,ordinary5,passive100,passive5 activeFromRun=2 ordinaryInput=standard-probe passiveInput=none scope=diagnostic-only");
                return;
            }
            if (models.Length == 1)
            {
                if (models[0] != 22 || scales.Length != 1 || (scales[0] != 100 && scales[0] != 5))
                    throw new InvalidOperationException("passive-single-run-requires-model22-percent100-or5");
            }
            else
            {
                if (Array.IndexOf(Environment.GetCommandLineArgs(), "--mpatcher-craft-similarity") < 0)
                    throw new InvalidOperationException("passive-matrix-requires-craft-similarity");
                scales = new[] { 100, 5 };
                models = new[] { 22, 22 };
            }
            MagnificationDownscale.Log("CRAFT_PASSIVE_CONFIG runs=" + scales.Length
                + " model=22 input=none gravity=all-canonical-dynamic-bodies-times-scale scope=diagnostic-only acceptance=not-vanilla-driving");
        }

        internal static void Reset()
        {
            if (!Requested) return;
            // Old scene is about to be unloaded. Keep method patches installed,
            // but their prefix no longer blocks any old or unrelated instance.
            isolated.Clear(); attached = null;
        }

        private static bool AllowCallback(MonoBehaviour __instance)
        {
            return __instance == null || !isolated.Contains(__instance.GetInstanceID());
        }

        private static void PatchCallbacks(Type type, Harmony harmony)
        {
            Assembly game = typeof(MachineController).Assembly;
            for (Type current = type; current != null && current.Assembly == game; current = current.BaseType)
                foreach (MethodInfo method in current.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly))
                {
                    if (!callbacks.Contains(method.Name) || method.IsAbstract || method.IsGenericMethod || method.ReturnType != typeof(void)
                        || method.GetMethodBody() == null || !patched.Add(method)) continue;
                    // Disabled MonoBehaviours can still receive physics callbacks.
                    // Also block OnDisable cleanup so isolation itself cannot
                    // execute a controller's joint/body teardown path.
                    harmony.Patch(method, new HarmonyMethod(AccessTools.Method(typeof(MagnificationCraftPassiveProbe), "AllowCallback")), null, null, null);
                    MagnificationDownscale.Log("CRAFT_PASSIVE_CALLBACK_GUARD method=" + current.FullName + "." + method.Name + " scope=listed-craft-instances");
                }
        }

        internal static void Attach(MachineController machine, Harmony harmony)
        {
            if (!Active) return;
            RequireTestProcess();
            if (machine == null || harmony == null || MagnificationCraftProbe.Model != 22) throw new InvalidOperationException("passive-invalid-ready-state");
            if (ReferenceEquals(attached, machine)) return;
            float scale = MagnificationScalePolicy.Valid(machine.DJCDFLDHPHK);
            List<GameObject> roots = new List<GameObject> { machine.gameObject };
            if (machine.KBLANAFAJFP != null) roots.AddRange(machine.KBLANAFAJFP);
            Dictionary<int, Rigidbody> bodies = new Dictionary<int, Rigidbody>();
            Dictionary<int, MonoBehaviour> controllers = new Dictionary<int, MonoBehaviour>();
            Assembly game = typeof(MachineController).Assembly;
            AddBody(bodies, machine.NFMPBACKJOJ);
            foreach (GameObject root in roots) if (root != null)
            {
                AddBody(bodies, root.GetComponent<Rigidbody>());
                foreach (BodyController owner in root.GetComponentsInChildren<BodyController>(true)) AddBody(bodies, owner.NFMPBACKJOJ);
                foreach (WheelController wheel in root.GetComponentsInChildren<WheelController>(true)) AddBody(bodies, wheel.EKHOCFCEGCB);
                foreach (SledController sled in root.GetComponentsInChildren<SledController>(true)) AddBody(bodies, MagnificationMassBoundary.SledBody(sled));
                foreach (FixedJoint joint in root.GetComponentsInChildren<FixedJoint>(true))
                    if (joint.connectedBody != null) AddBody(bodies, joint.GetComponent<Rigidbody>());
                foreach (MonoBehaviour component in root.GetComponentsInChildren<MonoBehaviour>(true))
                    if (component != null && component.GetType().Assembly == game) controllers[component.GetInstanceID()] = component;
            }
            if (bodies.Count == 0 || controllers.Count == 0) throw new InvalidOperationException("passive-empty-craft-graph");
            // Patch every relevant method before changing enabled: OnDisable is
            // synchronous. Membership is set for all instances before any stop.
            foreach (MonoBehaviour component in controllers.Values) PatchCallbacks(component.GetType(), harmony);
            foreach (int id in controllers.Keys) isolated.Add(id);
            SortedDictionary<string, int> totalTypes = new SortedDictionary<string, int>(StringComparer.Ordinal);
            SortedDictionary<string, int> changedTypes = new SortedDictionary<string, int>(StringComparer.Ordinal);
            foreach (MonoBehaviour component in controllers.Values)
            {
                string name = component.GetType().FullName;
                Count(totalTypes, name);
                component.StopAllCoroutines();
                component.CancelInvoke();
                if (component.enabled) { component.enabled = false; Count(changedTypes, name); }
            }
            foreach (KeyValuePair<string, int> pair in totalTypes)
            {
                int changed; changedTypes.TryGetValue(pair.Key, out changed);
                MagnificationDownscale.Log("CRAFT_PASSIVE_DISABLED type=" + pair.Key + " instances=" + pair.Value + " enabledToDisabled=" + changed + " stopCoroutines=true cancelInvokes=true");
            }
            int dynamicBodies = 0, converted = 0, alreadyGravity = 0, kinematic = 0, inactive = 0;
            foreach (Rigidbody body in bodies.Values)
            {
                if (!body.gameObject.activeInHierarchy) { inactive++; continue; }
                if (body.isKinematic) { kinematic++; continue; }
                bool before = body.useGravity;
                if (before) alreadyGravity++; else { body.useGravity = true; converted++; }
                MagnificationGravitySimilarity.Track(body, scale);
                dynamicBodies++;
                MagnificationDownscale.Log("CRAFT_PASSIVE_GRAVITY body=" + body.name + " id=" + body.GetInstanceID()
                    + " useGravityBefore=" + before + " useGravityAfter=" + body.useGravity + " scale=" + F(scale)
                    + " effectiveAcceleration=" + (Physics.gravity * scale).ToString("R")
                    + " sleeping=" + body.IsSleeping() + " velocity=" + body.velocity.ToString("R") + " angularVelocity=" + body.angularVelocity.ToString("R"));
            }
            attached = machine;
            MagnificationDownscale.Log("CRAFT_PASSIVE_READY scale=" + F(scale) + " frame=" + Time.frameCount + " fixedTime=" + F(Time.fixedTime)
                + " controllers=" + controllers.Count + " canonicalBodies=" + bodies.Count + " dynamicBodies=" + dynamicBodies
                + " convertedUseGravity=" + converted + " alreadyUseGravity=" + alreadyGravity + " kinematic=" + kinematic + " inactive=" + inactive
                + " gravity=Unity-plus-existing-similarity-no-wake input=none jointWrites=0 poseWrites=0 velocityWrites=0 massInertiaWrites=0 scope=diagnostic-only");
        }

        private static void AddBody(Dictionary<int, Rigidbody> bodies, Rigidbody body)
        {
            if (body != null) bodies[body.GetInstanceID()] = body;
        }
        private static void Count(SortedDictionary<string, int> counts, string key)
        {
            int count; counts.TryGetValue(key, out count); counts[key] = count + 1;
        }
        private static string F(float value) { return value.ToString("R", CultureInfo.InvariantCulture); }
    }
}
