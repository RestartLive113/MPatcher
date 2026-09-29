using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using McnCraft;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
    // Explicit isolated probes only. No registration from the release/manual path.
    internal static class MagnificationAuxiliaryProbe
    {
        private static readonly bool EventComparison = Array.IndexOf(Environment.GetCommandLineArgs(), "--mpatcher-craft-event-comparison") >= 0;
        internal static readonly bool MatchedGround = Array.IndexOf(Environment.GetCommandLineArgs(), "--mpatcher-craft-matched-ground") >= 0;
        internal static bool LongRest { get { return NativeGround || MatchedGround; } }
        internal static readonly bool NativeGround = Array.IndexOf(Environment.GetCommandLineArgs(), "--mpatcher-craft-native-ground") >= 0;
        internal static readonly bool ToleranceComparison = Array.IndexOf(Environment.GetCommandLineArgs(), "--mpatcher-craft-small-tolerances") >= 0;
        internal static readonly bool StraightComparison = Array.IndexOf(Environment.GetCommandLineArgs(), "--mpatcher-craft-straight-comparison") >= 0;
        internal static readonly bool BoundaryComparison = Array.IndexOf(Environment.GetCommandLineArgs(), "--mpatcher-craft-boundary-comparison") >= 0;
        internal static readonly bool SledGeometry = Array.IndexOf(Environment.GetCommandLineArgs(), "--mpatcher-sled-geometry") >= 0;
        private static readonly FieldInfo Hook = typeof(SledController).GetField("POMMKHACNGA", BindingFlags.Instance | BindingFlags.NonPublic);
        private static string F(float v) { return v.ToString("R", CultureInfo.InvariantCulture); }
        private static string V(Vector3 v) { return F(v.x) + "," + F(v.y) + "," + F(v.z); }
        internal static void Configure(string craft, bool nativeControl, ref int[] scales, ref int[] models)
        {
            if (NativeGround && (!StraightComparison || MatchedGround)) throw new InvalidOperationException("native-ground-requires-exclusive-straight-protocol");
            if (MatchedGround && !StraightComparison && !ToleranceComparison && !MagnificationJointUnitProbe.Requested) throw new InvalidOperationException("matched-ground-requires-straight-or-tolerance");
            if (MagnificationJointUnitProbe.Requested)
            {
                if (craft != "Car" || nativeControl || !MatchedGround || StraightComparison || ToleranceComparison || BoundaryComparison || EventComparison || SledGeometry || NativeGround) throw new InvalidOperationException("joint-unit-exclusive-Car-matched-ground");
                scales = new[] { 100, 5, 5, 5 }; models = new[] { 22, 22, 22, 22 };
                MagnificationDownscale.Log("JOINT_UNIT_CONFIG order=100,5,D6unit5,bodyUnit5 drive=W restingSeconds=20 realMassUnit=1-except-positive-control"); return;
            }
            if (ToleranceComparison)
            {
                if (craft != "Car" || nativeControl || StraightComparison || BoundaryComparison || EventComparison || SledGeometry) throw new InvalidOperationException("tolerance-probe-exclusive-Car-mode");
                scales = new[] { 5, 5, 5, 5 }; models = new[] { 22, 22, 22, 22 };
                MagnificationDownscale.Log("TOLERANCE_PROBE_REGISTERED order=base,d6Contact,floorContact,both commonMassUnit=1e6 drive=W-from-rest");
                return;
            }
            if (StraightComparison)
            {
                if (craft != "Car" || BoundaryComparison || EventComparison || SledGeometry) throw new InvalidOperationException("straight-probe-exclusive-Car-mode");
                scales = nativeControl ? new[] { 100, 100 } : new[] { 100, 100, 5, 5 };
                models = nativeControl ? new[] { 0, 0 } : new[] { 22, 22, 22, 22 };
                MagnificationDownscale.Log("STRAIGHT_REST_CONFIG nativeControl=" + nativeControl + " order=100W,100S,5W,5S non100CommonMassUnit=1e6 nativeControlRuns=first-two restingFrames=" + (LongRest ? 2000 : 1000) + " driveFrames=500 eachDirection=fresh-scene");
                return;
            }
            if (BoundaryComparison)
            {
                if (craft != "Car" || nativeControl || EventComparison || SledGeometry) throw new InvalidOperationException("boundary-probe-exclusive-Car-mode");
                scales = new[] { 100, 5, 5, 5 }; models = new[] { 22, 22, 22, 22 };
                MagnificationDownscale.Log("BOUNDARY_PROBE_REGISTERED order=ordinary100,ordinary5,noGround5,commonMassUnit1e6_5 scope=isolated-test-only physicalLaws=unchanged");
                return;
            }
            if (NativeGround && !StraightComparison) throw new InvalidOperationException("native-ground-requires-straight-protocol");
            if (!EventComparison && !SledGeometry) return;
            if (EventComparison && SledGeometry || nativeControl) throw new InvalidOperationException("auxiliary-probe-exclusive-mode");
            if (EventComparison && craft != "Car" || SledGeometry && craft.Trim() != "Ayra\u00b3") throw new InvalidOperationException("auxiliary-probe-craft-mismatch");
            scales = EventComparison ? new[] { 100, 5, 5 } : new[] { 100, 5, 200 };
            models = new[] { 22, 22, 22 };
            MagnificationDownscale.Log("AUXILIARY_PROBE_REGISTERED eventComparison=" + EventComparison + " sledGeometry=" + SledGeometry + " scope=isolated-test-only");
        }
        internal static float MassUnit(int run, float requested) { return MagnificationJointUnitProbe.Requested ? (run == 3 ? 1000000f : 1f) : ToleranceComparison ? 1000000f : StraightComparison ? (run >= 2 ? 1000000f : 1f) : BoundaryComparison ? (run == 3 ? 1000000f : 1f) : requested; }
        internal static void Ground(BoxCollider ground, float scale)
        {
            if (ToleranceComparison)
            {
                float before = ground.contactOffset;
                if (MagnificationCraftProbe.RunIndex >= 2) ground.contactOffset *= scale;
                MagnificationDownscale.Log("TOLERANCE_GROUND run=" + MagnificationCraftProbe.RunIndex + " before=" + F(before) + " after=" + F(ground.contactOffset));
            }
            if (!BoundaryComparison) return;
            bool disable = MagnificationCraftProbe.RunIndex == 2;
            if (disable) ground.enabled = false;
            MagnificationDownscale.Log("BOUNDARY_PROBE_READY run=" + MagnificationCraftProbe.RunIndex + " groundEnabled=" + ground.enabled + " commonMassUnit=" + F(MagnificationCraftProbe.MassUnit) + " massInertiaSpringsForces=common-factor");
        }
        internal static void Ready(MachineController machine)
        {
            MagnificationJointUnitProbe.Ready(machine);
            if (!EventComparison) return;
            List<FixedJoint> helpers = new List<FixedJoint>();
            foreach (FixedJoint joint in machine.GetComponentsInChildren<FixedJoint>(true))
                if (joint.name == "EventTrigger" && joint.connectedBody == machine.NFMPBACKJOJ) helpers.Add(joint);
            if (helpers.Count != 1) throw new InvalidOperationException("event-probe-expected-one-helper");
            FixedJoint target = helpers[0]; Rigidbody body = target.GetComponent<Rigidbody>();
            Collider[] colliders = body.GetComponents<Collider>();
            if (body.useGravity || body.isKinematic || colliders.Length != 1 || !colliders[0].isTrigger) throw new InvalidOperationException("event-probe-unexpected-helper-profile");
            bool remove = MagnificationCraftProbe.RunIndex == 2;
            MagnificationDownscale.Log("EVENT_CONSTRAINT_PROBE run=" + MagnificationCraftProbe.RunIndex + " removed=" + remove + " helperMass=" + F(body.mass) + " helperInertia=" + V(body.inertiaTensor)
                + " mainMass=" + F(machine.NFMPBACKJOJ.mass) + " scope=one-EventTrigger-FixedJoint controllersAndPhysicalParts=unchanged");
            if (remove)
            {
                if (Math.Abs(machine.DJCDFLDHPHK - .05f) > .000001f) throw new InvalidOperationException("event-probe-forbidden-scale");
                UnityEngine.Object.DestroyImmediate(target);
                if (body.GetComponent<FixedJoint>() != null) throw new InvalidOperationException("event-probe-removal-failed");
            }
        }
        internal static void Sample(MachineController machine, int frame)
        {
            if (!SledGeometry || (frame != 1 && frame != 5 && frame != 100 && frame != 1000)) return;
            HashSet<int> seen = new HashSet<int>(); int count = 0;
            List<GameObject> roots = new List<GameObject> { machine.gameObject }; roots.AddRange(machine.KBLANAFAJFP);
            float s = machine.DJCDFLDHPHK;
            foreach (GameObject root in roots) if (root != null)
                foreach (SledController sled in root.GetComponentsInChildren<SledController>(true))
                {
                    if (!seen.Add(sled.GetInstanceID())) continue;
                    Rigidbody body = MagnificationMassBoundary.SledBody(sled);
                    if (body == null) throw new InvalidOperationException("sled-probe-missing-roller");
                    SphereCollider sphere = body.GetComponent<SphereCollider>();
                    Vector3 lossy = sphere.transform.lossyScale;
                    float expected = sphere.radius * Mathf.Max(Mathf.Abs(lossy.x), Mathf.Abs(lossy.y), Mathf.Abs(lossy.z));
                    Bounds bounds = sphere.bounds;
                    RaycastHit hit;
                    bool ray = sphere.Raycast(new Ray(bounds.center + Vector3.right * expected * 3f, Vector3.left), out hit, expected * 6f);
                    float actual = ray ? Vector3.Distance(hit.point, bounds.center) : -1f;
                    GameObject hook = Hook.GetValue(sled) as GameObject;
                    MagnificationDownscale.Log("SLED_GEOMETRY frame=" + frame + " scale=" + F(s) + " part=" + sled.JNKEKNOAPHO.GetPosString(true)
                        + " radiusLocal=" + F(sphere.radius) + " lossy=" + V(lossy) + " boundsRadiusWorld=" + F(bounds.extents.x)
                        + " boundsRadiusCraft=" + F(bounds.extents.x / s) + " rayHit=" + ray + " rayRadiusWorld=" + F(actual) + " rayRadiusCraft=" + F(actual / s)
                        + " expectedWorld=" + F(expected) + " mountOffsetCraft=" + V((bounds.center - sled.transform.TransformPoint(Vector3.down * .1f)) / s)
                        + " hookActive=" + (hook != null && hook.activeInHierarchy) + " hookLossy=" + (hook == null ? "none" : V(hook.transform.lossyScale))
                        + " hookColliders=" + (hook == null ? 0 : hook.GetComponentsInChildren<Collider>(true).Length) + " mutation=none");
                    count++;
                }
            if (count != 16) throw new InvalidOperationException("Ayra-sled-count-expected16-actual" + count);
            MagnificationDownscale.Log("SLED_GEOMETRY_END frame=" + frame + " scale=" + F(s) + " sleds=" + count);
        }
    }
}
