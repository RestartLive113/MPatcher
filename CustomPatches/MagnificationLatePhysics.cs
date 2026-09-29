using System.Globalization;
using UnityEngine;
namespace MPatcherFork.CustomPatches
{
    // Wheel.Start changes tire geometry after MakeTire has constructed its joint.
    // Refresh the cached physical anchor after final geometry, never per tick.
    internal static class MagnificationLatePhysics
    {
        internal static void FinalizeBody(Rigidbody body, float scale, string phase)
        {
            if (!MagnificationScalePolicy.SameTime || MagnificationScalePolicy.Bypass(scale) || body == null) return;
            foreach (Joint joint in body.GetComponents<Joint>())
            {
                // This repair is for the wheel's unswapped late-scaled joint.
                // A swapped joint uses a different native frame update path.
                ConfigurableJoint configurable = joint as ConfigurableJoint;
                if (configurable != null && configurable.swapBodies)
                {
                    MagnificationDownscale.Log("LATE_JOINT_REFRESH_SKIPPED phase=" + phase + " body=" + body.name + " reason=swapped-body-frames");
                    continue;
                }
                MagnificationJointPoseNative.Snapshot before, after; string failure;
                bool readBefore = MagnificationJointPoseNative.TryRead(joint, out before, out failure);
                Vector3 anchor = joint.anchor;
                bool automatic = joint.autoConfigureConnectedAnchor;
                // The native setter applies the final Transform scale and honors
                // the existing connected-anchor mode. Do not scale this value again.
                joint.anchor = anchor;
                bool readAfter = MagnificationJointPoseNative.TryRead(joint, out after, out failure);
                MagnificationDownscale.Log("LATE_JOINT_FINALIZED phase=" + phase + " body=" + body.name
                    + " scale=" + F(scale) + " type=" + joint.GetType().Name + " autoConnected=" + automatic
                    + " autoConnectedAfter=" + joint.autoConfigureConnectedAnchor + " anchor=" + anchor.ToString("R") + " localScale=" + body.transform.lossyScale.ToString("R")
                    + " beforeRead=" + readBefore + " ownPointDeltaBefore=" + (readBefore ? F(before.OwnPointDelta) : "unavailable")
                    + " afterRead=" + readAfter + " ownPointDeltaAfter=" + (readAfter ? F(after.OwnPointDelta) : "unavailable")
                    + " nativeGapAfter=" + (readAfter ? F(after.NativeGap) : failure)
                    + " ownLeverBefore=" + (readBefore ? before.OwnLeverFromCom.ToString("R") : "unavailable")
                    + " ownLeverAfter=" + (readAfter ? after.OwnLeverFromCom.ToString("R") : "unavailable"));
            }
            MagnificationConstraintUnits.ApplyHierarchy(body.gameObject, scale, phase);
        }
        private static string F(float value) { return value.ToString("R", CultureInfo.InvariantCulture); }
    }
}