using System.Collections.Generic;
using System.Globalization;
using McnCraft;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
	// Emitted script call sites only: native controller setters already convert
	// their own copies. Read/write pairs keep script-side caches in craft units.
	internal static class MagnificationScriptJointUnits
	{
		private static readonly HashSet<int> motorLogged = new HashSet<int>(), springLogged = new HashSet<int>();
		internal static int MotorReads, MotorWrites, SpringReads, SpringWrites;
		internal static void Clear() { MotorReads = MotorWrites = SpringReads = SpringWrites = 0; motorLogged.Clear(); springLogged.Clear(); }
		private static float Scale(HingeJoint joint)
		{
			if (MagnificationCraftProbe.Model < 19) return 1f;
			BodyController owner = joint.GetComponentInParent<BodyController>();
			return owner == null ? 1f : MagnificationPhysicsMode.PhysicsScale(owner.CIPOPAGDJDE);
		}
		private static float Factor(HingeJoint joint) { float s = Scale(joint); return MagnificationScalePolicy.Enabled ? MagnificationScalePolicy.Torque(s) : s * s; }
        private static bool IsVanilla(HingeJoint joint)
        {
            BodyController owner = joint.GetComponentInParent<BodyController>();
            MachineController machine = owner == null ? null : owner.CIPOPAGDJDE;
            if (machine == null) machine = joint.GetComponentInParent<MachineController>();
            return MagnificationPhysicsMode.IsVanilla(machine);
        }
		internal static JointMotor MotorToWorld(JointMotor value, float factor) { value.force *= factor; return value; }
		internal static JointMotor MotorToCraft(JointMotor value, float factor) { value.force /= factor; return value; }
		internal static JointSpring SpringToWorld(JointSpring value, float factor) { value.spring *= factor; value.damper *= factor; return value; }
		internal static JointSpring SpringToCraft(JointSpring value, float factor) { value.spring /= factor; value.damper /= factor; return value; }
        internal static JointMotor ReadMotor(HingeJoint joint)
        {
            if (IsVanilla(joint)) return joint.motor;
            MotorReads++; return MotorToCraft(joint.motor, Factor(joint));
        }
		internal static void WriteMotor(HingeJoint joint, JointMotor value)
		{
            if (IsVanilla(joint)) { joint.motor = value; return; }
			MotorWrites++;
			float factor = Factor(joint);
			joint.motor = MagnificationRotorIsolationProbe.FilterMotor(joint, MotorToWorld(value, factor));
			if (factor != 1f && motorLogged.Add(joint.GetInstanceID()))
				MagnificationDownscale.Log("CRAFT_SCRIPT_MOTOR factor=" + F(factor) + " craftForce=" + F(value.force) + " worldForce=" + F(joint.motor.force) + " targetDegPerSec=" + F(value.targetVelocity));
		}
        internal static JointSpring ReadSpring(HingeJoint joint)
        {
            if (IsVanilla(joint)) return joint.spring;
            SpringReads++; return MagnificationScalePolicy.Enabled ? MagnificationScalePolicy.AngularSpring(joint.spring, Scale(joint), false) : SpringToCraft(joint.spring, Factor(joint));
        }
		internal static void WriteSpring(HingeJoint joint, JointSpring value)
		{
            if (IsVanilla(joint)) { joint.spring = value; return; }
			SpringWrites++;
			float factor = Factor(joint);
			joint.spring = MagnificationRotorIsolationProbe.FilterSpring(joint, MagnificationScalePolicy.Enabled ? MagnificationScalePolicy.AngularSpring(value, Scale(joint), true) : SpringToWorld(value, factor), true);
			if (factor != 1f && springLogged.Add(joint.GetInstanceID()))
				MagnificationDownscale.Log("CRAFT_SCRIPT_SPRING factor=" + F(factor) + " craftSpring=" + F(value.spring) + " worldSpring=" + F(joint.spring.spring) + " craftDamper=" + F(value.damper) + " worldDamper=" + F(joint.spring.damper));
		}
		private static string F(float value) { return value.ToString("R", CultureInfo.InvariantCulture); }
	}
}
