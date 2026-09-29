using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
    // Sled.Start instantiates Roller with serialized joint coefficients; there is
    // no managed drive setter for the existing piston boundary to intercept.
    internal static class MagnificationSledJointUnits
    {
        private sealed class Source
        {
            internal ConfigurableJoint Joint;
            internal SoftJointLimit LinearLimit;
            internal SoftJointLimitSpring LinearSpring, AngularXSpring, AngularYZSpring;
            internal JointDrive X, Y, Z, AngularX, AngularYZ, Slerp;
            internal Vector3 TargetPosition, TargetVelocity;
            internal float BreakForce, BreakTorque;
        }
        private static readonly Dictionary<int, Source> sources = new Dictionary<int, Source>();
        // Exact serialized Roller maximumForce is one ULP below float.MaxValue.
        // Preserve this unbounded prefab setting instead of overflowing on upscale.
        private const float PrefabUnlimitedForce = 3.4028232635611926e38f;

        internal static void Apply(Rigidbody body, float scale)
        {
            if (!MagnificationScalePolicy.SameTime || MagnificationScalePolicy.Bypass(scale) || body == null) return;
            float s = MagnificationScalePolicy.Valid(scale);
            foreach (ConfigurableJoint joint in body.GetComponents<ConfigurableJoint>())
            {
                int id = joint.GetInstanceID(); Source source;
                bool captured = !sources.TryGetValue(id, out source) || !ReferenceEquals(source.Joint, joint);
                if (captured)
                {
                    sources[id] = source = new Source
                    {
                        Joint = joint, LinearLimit = joint.linearLimit,
                        LinearSpring = joint.linearLimitSpring, AngularXSpring = joint.angularXLimitSpring,
                        AngularYZSpring = joint.angularYZLimitSpring,
                        X = joint.xDrive, Y = joint.yDrive, Z = joint.zDrive,
                        AngularX = joint.angularXDrive, AngularYZ = joint.angularYZDrive, Slerp = joint.slerpDrive,
                        TargetPosition = joint.targetPosition, TargetVelocity = joint.targetVelocity,
                        BreakForce = joint.breakForce, BreakTorque = joint.breakTorque
                    };
                }
                float linearK = MagnificationScalePolicy.LinearSpring(s), linearC = MagnificationScalePolicy.LinearDamper(s);
                float angularK = MagnificationScalePolicy.Torque(s), angularC = MagnificationScalePolicy.AngularDamper(s);
                float force = MagnificationScalePolicy.Force(s), torque = MagnificationScalePolicy.Torque(s);
                joint.linearLimit = Limit(source.LinearLimit, s);
                joint.linearLimitSpring = Spring(source.LinearSpring, linearK, linearC);
                joint.angularXLimitSpring = Spring(source.AngularXSpring, angularK, angularC);
                joint.angularYZLimitSpring = Spring(source.AngularYZSpring, angularK, angularC);
                joint.xDrive = Drive(source.X, linearK, linearC, force);
                joint.yDrive = Drive(source.Y, linearK, linearC, force);
                joint.zDrive = Drive(source.Z, linearK, linearC, force);
                joint.angularXDrive = Drive(source.AngularX, angularK, angularC, torque);
                joint.angularYZDrive = Drive(source.AngularYZ, angularK, angularC, torque);
                joint.slerpDrive = Drive(source.Slerp, angularK, angularC, torque);
                joint.targetPosition = source.TargetPosition * s;
                joint.targetVelocity = source.TargetVelocity * s;
                joint.breakForce = ForceLimit(source.BreakForce, force);
                joint.breakTorque = ForceLimit(source.BreakTorque, torque);
                // Projection distance remains owned by ConstraintUnits' source cache.
                // Anchors/axes are body-local and unchanged. Angular limits, angular
                // targets, modes, swapBodies and collision/preprocessing are unchanged.
                MagnificationDownscale.Log("SLED_JOINT_UNITS body=" + body.name + " joint=" + id + " scale=" + F(s)
                    + " sourceCaptured=" + captured + " repeatedApply=source-cache"
                    + " linearLimitSource=" + F(source.LinearLimit.limit) + " linearLimit=" + F(joint.linearLimit.limit)
                    + " contactDistanceSource=" + F(source.LinearLimit.contactDistance) + " contactDistance=" + F(joint.linearLimit.contactDistance)
                    + " linearSpringSource=" + F(source.LinearSpring.spring) + "," + F(source.LinearSpring.damper)
                    + " linearSpring=" + F(joint.linearLimitSpring.spring) + "," + F(joint.linearLimitSpring.damper)
                    + " angularSlerpSource=" + F(source.Slerp.positionSpring) + "," + F(source.Slerp.positionDamper)
                    + " angularSlerp=" + F(joint.slerpDrive.positionSpring) + "," + F(joint.slerpDrive.positionDamper)
                    + " linearMotion=" + joint.xMotion + "," + joint.yMotion + "," + joint.zMotion
                    + " angularMotion=" + joint.angularXMotion + "," + joint.angularYMotion + "," + joint.angularZMotion
                    + " swapBodies=" + joint.swapBodies + " autoConnected=" + joint.autoConfigureConnectedAnchor
                    + " anchorsWritten=False unboundedForceCaps=preserved");
            }
        }
        private static SoftJointLimit Limit(SoftJointLimit value, float scale)
        { value.limit *= scale; value.contactDistance *= scale; return value; }
        private static SoftJointLimitSpring Spring(SoftJointLimitSpring value, float spring, float damper)
        { value.spring *= spring; value.damper *= damper; return value; }
        private static JointDrive Drive(JointDrive value, float spring, float damper, float force)
        {
            value.positionSpring *= spring; value.positionDamper *= damper;
            value.maximumForce = ForceLimit(value.maximumForce, force); return value;
        }
        private static float ForceLimit(float value, float factor)
        { return value >= PrefabUnlimitedForce ? value : value * factor; }
        internal static void Clear() { sources.Clear(); }
        private static string F(float value) { return value.ToString("R", CultureInfo.InvariantCulture); }
    }
}
