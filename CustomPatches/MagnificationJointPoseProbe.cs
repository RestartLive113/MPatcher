using System;
using System.Collections.Generic;
using System.Globalization;
using McnCraft;
using UnityEngine;
namespace MPatcherFork.CustomPatches
{
    // Isolated calibration plus bounded read-only craft snapshots.
    internal sealed class MagnificationJointPoseProbe : MonoBehaviour
    {
        private readonly List<Joint> samples = new List<Joint>();
        private int tick;
        private void Start()
        {
            try
            {
                foreach (float s in new[] { 1f, .05f, 2f })
                {
                    Rigidbody a = Make("Own_" + F(s), Vector3.zero), b = Make("Other_" + F(s), Vector3.right * 2f);
                    a.centerOfMass = new Vector3(.2f, .3f, .4f); b.centerOfMass = new Vector3(-.1f, .2f, -.3f);
                    HingeJoint joint = a.gameObject.AddComponent<HingeJoint>();
                    joint.connectedBody = b; joint.autoConfigureConnectedAnchor = false;
                    joint.axis = Vector3.forward; joint.anchor = new Vector3(1f, .5f, .25f);
                    joint.connectedAnchor = new Vector3(-1f, .5f, .25f);
                    Read(joint, "before-scale-" + F(s));
                    a.transform.localScale = b.transform.localScale = Vector3.one * s;
                    b.transform.position = Vector3.right * (2f * s);
                    a.centerOfMass *= s; b.centerOfMass *= s;
                    // Same public body-local finalization as the current craft patch.
                    MagnificationJointAnchors.SetSamePivot(joint, new Vector3(1f, .5f, .25f));
                    Read(joint, "after-scale-" + F(s)); samples.Add(joint);
                }
                // Wheel.Start: parent already scaled; create tire world geometry,
                // parent with worldPositionStays, add body/joint, then scale child.
                foreach (float s in new[] { 1f, .05f, 2f })
                    foreach (bool explicitOffset in new[] { false, true })
                        foreach (bool refresh in new[] { false, true })
                        {
                            string tag = (explicitOffset ? "offset" : "default") + (refresh ? "-refresh-" : "-untouched-") + F(s);
                            Rigidbody b = Make("AutoParent_" + tag, Vector3.zero);
                            b.transform.localScale = Vector3.one * s;
                            b.centerOfMass = new Vector3(-.1f, .2f, -.3f) * s;
                            GameObject tire = new GameObject("MPatcher_Pose_AutoTire_" + tag);
                            tire.transform.position = Vector3.right * (2f * s);
                            tire.transform.localScale = new Vector3(2.5f, .45f, 2.5f);
                            tire.transform.SetParent(b.transform, true);
                            BoxCollider collider = tire.AddComponent<BoxCollider>(); collider.center = new Vector3(0f, .5f, 0f);
                            Rigidbody a = tire.AddComponent<Rigidbody>();
                            a.useGravity = a.detectCollisions = false; a.mass = 1f;
                            a.inertiaTensor = Vector3.one; a.inertiaTensorRotation = Quaternion.identity; a.sleepThreshold = 0f;
                            ConfigurableJoint joint = tire.AddComponent<ConfigurableJoint>();
                            joint.connectedBody = b;
                            if (explicitOffset) joint.anchor = new Vector3(0f, .5f, 0f);
                            Read(joint, "auto-before-scale-" + tag);
                            tire.transform.localScale *= s;
                            Read(joint, "auto-after-scale-" + tag);
                            if (refresh)
                            {
                                joint.anchor = joint.anchor;
                                Read(joint, "auto-after-refresh-" + tag);
                            }
                            samples.Add(joint);
                        }
                MagnificationDownscale.Log("JOINT_POSE_PROBE_START calibration=100/5/200 manualMove=Transform.position autoFixture=Wheel.Start-final-child-scale autoControls=default/offset,untouched/anchor-refresh gravity=false contacts=false physicsHooks=disabled");
            }
            catch (Exception error) { MagnificationDownscale.Log("JOINT_POSE_PROBE_FAILED " + error); Application.Quit(); }
        }
        private static Rigidbody Make(string name, Vector3 position)
        {
            GameObject obj = new GameObject("MPatcher_Pose_" + name); obj.transform.position = position;
            Rigidbody body = obj.AddComponent<Rigidbody>(); body.useGravity = body.detectCollisions = false;
            body.mass = 1f; body.inertiaTensor = Vector3.one; body.inertiaTensorRotation = Quaternion.identity;
            body.sleepThreshold = 0f; return body;
        }
        private void FixedUpdate()
        {
            tick++;
            if (tick == 1 || tick == 2 || tick == 5 || tick == 10)
                foreach (Joint joint in samples) Read(joint, "fixed-" + tick);
            if (tick == 10) { MagnificationDownscale.Log("JOINT_POSE_PROBE_COMPLETE completed=True pairs=" + samples.Count + " acceptance=not-assigned"); Application.Quit(); }
        }
        private static void Read(Joint joint, string phase)
        {
            MagnificationJointPoseNative.Snapshot value; string failure;
            bool success = MagnificationJointPoseNative.TryRead(joint, out value, out failure);
            MagnificationDownscale.Log("JOINT_NATIVE_POSE phase=" + phase + " body=" + joint.name + " type=" + joint.GetType().Name
                + " scale=" + F(joint.transform.lossyScale.x) + " ownScale=" + joint.transform.lossyScale.ToString("R")
                + " otherScale=" + (joint.connectedBody == null ? "world" : joint.connectedBody.transform.lossyScale.ToString("R"))
                + " anchorLocal=" + joint.anchor.ToString("R") + " connectedAnchorLocal=" + joint.connectedAnchor.ToString("R")
                + " autoConnected=" + joint.autoConfigureConnectedAnchor
                + " swapBodies=" + (joint is ConfigurableJoint ? ((ConfigurableJoint)joint).swapBodies.ToString() : "na")
                + " success=" + success + " " + (success ? value.ToString() : failure));
        }
        internal static void SampleMachine(MachineController machine, string phase)
        {
            List<GameObject> roots = new List<GameObject> { machine.gameObject }; roots.AddRange(machine.KBLANAFAJFP);
            HashSet<int> seen = new HashSet<int>(); int count = 0, rejected = 0, details = 0;
            float pointDelta = 0f, frameDelta = 0f, actorDelta = 0f, comDelta = 0f; Joint worst = null;
            foreach (GameObject root in roots) if (root != null) foreach (Joint joint in root.GetComponentsInChildren<Joint>())
            {
                if (joint.connectedBody == null || !seen.Add(joint.GetInstanceID())) continue;
                MagnificationJointPoseNative.Snapshot value; string failure;
                if (!MagnificationJointPoseNative.TryRead(joint, out value, out failure))
                {
                    rejected++; if (rejected <= 3) MagnificationDownscale.Log("JOINT_NATIVE_POSE_REJECT phase=" + phase + " type=" + joint.GetType().Name + " reason=" + failure);
                    continue;
                }
                count++; float delta = Mathf.Max(value.OwnPointDelta, value.OtherPointDelta);
                if (worst == null || delta > pointDelta) { pointDelta = delta; worst = joint; }
                frameDelta = Mathf.Max(frameDelta, Mathf.Max(value.OwnFrameCacheDelta, value.OtherFrameCacheDelta));
                actorDelta = Mathf.Max(actorDelta, Mathf.Max(value.OwnActorPositionDelta, value.OtherActorPositionDelta));
                comDelta = Mathf.Max(comDelta, Mathf.Max(value.OwnComDelta, value.OtherComDelta));
                if (details < 4) { details++; Read(joint, phase); }
            }
            MagnificationDownscale.Log("JOINT_NATIVE_POSE_AGGREGATE phase=" + phase + " scale=" + F(machine.DJCDFLDHPHK)
                + " count=" + count + " rejected=" + rejected + " pointDeltaMax=" + F(pointDelta)
                + " frameCacheDeltaMax=" + F(frameDelta) + " actorPositionDeltaMax=" + F(actorDelta) + " comDeltaMax=" + F(comDelta)
                + " pistonGap=not-a-zero-error-invariant behavior=read-only");
            if (worst != null) Read(worst, phase + "-worst");
        }
        private static string F(float value) { return value.ToString("R", CultureInfo.InvariantCulture); }
    }
}
