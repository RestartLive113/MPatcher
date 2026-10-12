using System;
using System.Collections.Generic;
using McnCraft;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
    // Guarded Car experiment: common row units for D6 constraints, real bodies unchanged.
    // This deliberately does not cover Hinge/Fixed constraints or become a release policy.
    internal static class MagnificationJointUnitProbe
    {
        internal static readonly bool Requested = Array.IndexOf(Environment.GetCommandLineArgs(), "--mpatcher-craft-joint-units") >= 0;
        internal static readonly bool Resume = Array.IndexOf(Environment.GetCommandLineArgs(), "--mpatcher-craft-joint-units-resume") >= 0;
        private const float Unit = 1000000f;
        private static MachineController attached;
        private static readonly List<ConfigurableJoint> joints = new List<ConfigurableJoint>();
        internal static JointDrive Convert(JointDrive drive, PistonController owner)
        {
            return Requested && MagnificationCraftProbe.RunIndex == 2 && attached != null && owner.FPEBEEJGFPI == attached ? Multiply(drive) : drive;
        }
        private static JointDrive Multiply(JointDrive value)
        {
            value.positionSpring *= Unit; value.positionDamper *= Unit;
            if (value.maximumForce < float.MaxValue / Unit) value.maximumForce *= Unit;
            return value;
        }
        internal static void Ready(MachineController machine)
        {
            if (!Requested) return;
            attached = null; joints.Clear();
            if (MagnificationCraftProbe.RunIndex != 2) return;
            if (MagnificationCraftProbe.Model != 22 || MagnificationCraftProbe.MassUnit != 1f || Math.Abs(machine.DJCDFLDHPHK - .05f) > 0.000001f)
                throw new InvalidOperationException("joint-unit-probe-scale-profile");
            List<GameObject> roots = new List<GameObject> { machine.gameObject }; roots.AddRange(machine.KBLANAFAJFP);
            HashSet<int> seen = new HashSet<int>(), bodies = new HashSet<int>();
            List<GameObject> scan = new List<GameObject>(roots);
            Action<Rigidbody> add = delegate(Rigidbody body) { if (body == null) return; bodies.Add(body.GetInstanceID()); if (!scan.Contains(body.gameObject)) scan.Add(body.gameObject); };
            add(machine.NFMPBACKJOJ);
            // Match PhysicalGraph's canonical body inventory, excluding inactive prefab auxiliaries.
            foreach (GameObject root in roots) if (root != null)
            {
                add(root.GetComponent<Rigidbody>());
                foreach (BodyController body in root.GetComponentsInChildren<BodyController>(true)) add(body.NFMPBACKJOJ);
                foreach (WheelController wheel in root.GetComponentsInChildren<WheelController>(true)) add(wheel.EKHOCFCEGCB);
                foreach (FixedJoint helper in root.GetComponentsInChildren<FixedJoint>(true)) if (helper.connectedBody != null) add(helper.GetComponent<Rigidbody>());
            }
            foreach (GameObject root in scan) if (root != null)
                foreach (ConfigurableJoint joint in root.GetComponentsInChildren<ConfigurableJoint>(true))
                    if (joint.GetComponent<Rigidbody>() != null && bodies.Contains(joint.GetComponent<Rigidbody>().GetInstanceID()) && seen.Add(joint.GetInstanceID()))
                    {
                        if (joint.connectedBody == null || !bodies.Contains(joint.connectedBody.GetInstanceID()) || !bodies.Contains(joint.GetComponent<Rigidbody>().GetInstanceID()))
                            throw new InvalidOperationException("joint-unit-external-connection");
                        if (!float.IsPositiveInfinity(joint.breakForce) || !float.IsPositiveInfinity(joint.breakTorque)) throw new InvalidOperationException("joint-unit-breakable-not-supported");
                        foreach (JointDrive drive in new[] { joint.angularXDrive, joint.angularYZDrive, joint.slerpDrive })
                            if (drive.positionSpring != 0f || drive.positionDamper != 0f) throw new InvalidOperationException("joint-unit-angular-drive-not-supported");
                        joints.Add(joint);
                    }
            if (joints.Count != 18) throw new InvalidOperationException("joint-unit-requires-18-Car-D6 actual=" + joints.Count + " bodies=" + bodies.Count);
            foreach (ConfigurableJoint joint in joints) MagnificationJointMassNative.UniformD6Unit(joint, Unit, false, true);
            foreach (ConfigurableJoint joint in joints)
            {
                MagnificationJointMassNative.UniformD6Unit(joint, Unit, true, true);
                // Only the four force-based piston drives are active. Other D6 rows are hard constraints.
                if (joint.xDrive.positionSpring != 0f || joint.xDrive.positionDamper != 0f) joint.xDrive = Multiply(joint.xDrive);
                if (joint.yDrive.positionSpring != 0f || joint.yDrive.positionDamper != 0f) joint.yDrive = Multiply(joint.yDrive);
                if (joint.zDrive.positionSpring != 0f || joint.zDrive.positionDamper != 0f) joint.zDrive = Multiply(joint.zDrive);
                MagnificationJointMassNative.UniformD6Unit(joint, Unit, false, false);
            }
            attached = machine;
            MagnificationDownscale.Log("JOINT_UNIT_READY D6=18 unit=1000000 invMassInertiaScale=0.000001 drives=times-unit realBodyMassInertia=unchanged externalForces=unchanged hingeFixed=unchanged scope=isolated-test-only");
        }
        internal static void Sample(int frame)
        {
            if (!Requested || attached == null || MagnificationCraftProbe.RunIndex != 2 || (frame != 1 && frame != 100 && frame != 1000 && frame != 2000 && frame != 2500)) return;
            foreach (ConfigurableJoint joint in joints) MagnificationJointMassNative.UniformD6Unit(joint, Unit, false, false);
            MagnificationDownscale.Log("JOINT_UNIT_READBACK frame=" + frame + " D6=" + joints.Count + " nativeScale=match mutation=none");
        }
    }
}
