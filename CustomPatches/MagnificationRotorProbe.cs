using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using HarmonyLib;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
	// Called only by the explicitly enabled craft experiment. Does not alter drives.
	internal static class MagnificationRotorProbe
	{
		private sealed class Torque { internal Vector3 Value; internal ForceMode Mode; internal float At; }
		private static readonly Dictionary<int, Torque> torques = new Dictionary<int, Torque>();
		private static readonly FieldInfo angle = AccessTools.Field(typeof(JointController), "OPKPNLOMBDI");
		private static readonly FieldInfo command = AccessTools.Field(typeof(JointController), "KAEEPGIHJCA");
		private static readonly FieldInfo power = AccessTools.Field(typeof(JointController), "DCLNENGNEPK");
		internal static void Clear() { torques.Clear(); }
		internal static void RecordTorque(JointController owner, Vector3 value, ForceMode mode)
		{
			Torque record;
			if (!torques.TryGetValue(owner.GetInstanceID(), out record)) torques[owner.GetInstanceID()] = record = new Torque();
			record.Value = value; record.Mode = mode; record.At = Time.fixedTime;
		}
		internal static void Snapshot(JointController owner, int frame)
		{
			HingeJoint joint = owner.ALGHGLCBLDJ;
			if (joint == null || joint.connectedBody == null) return;
			Rigidbody own = joint.GetComponent<Rigidbody>(), other = joint.connectedBody;
			Vector3 axis = joint.transform.TransformDirection(joint.axis).normalized;
			JointSpring spring = joint.spring;
			JointMotor drive = joint.motor;
			float actual = (float)angle.GetValue(owner), rate = Vector3.Dot(own.angularVelocity - other.angularVelocity, axis);
			Torque motor; bool recent = torques.TryGetValue(owner.GetInstanceID(), out motor) && Time.fixedTime - motor.At <= Time.fixedDeltaTime * 1.5f;
			MagnificationDownscale.Log("CRAFT_PROBE_ROTOR model=" + MagnificationCraftProbe.Model + " frame=" + frame
				+ " part=" + owner.JNKEKNOAPHO.GetPosString(true) + " type=" + owner.JNKEKNOAPHO.type
				+ " wheel=" + (owner.OMBBGKNHGEF != null) + " command=" + F((float)command.GetValue(owner)) + " power=" + F((float)power.GetValue(owner))
				+ " springOn=" + joint.useSpring + " spring=" + F(spring.spring) + " damper=" + F(spring.damper)
				+ " hingeMotorOn=" + joint.useMotor + " hingeMotorForce=" + F(drive.force)
				+ " hingeMotorTarget=" + F(drive.targetVelocity) + " hingeVelocity=" + F(joint.velocity) + " hingeFreeSpin=" + drive.freeSpin
				+ " target=" + F(spring.targetPosition) + " actual=" + F(actual) + " error=" + F(Mathf.DeltaAngle(actual, spring.targetPosition))
				+ " rate=" + F(rate) + " ownAxisI=" + F(AxisInertia(own, axis)) + " otherAxisI=" + F(AxisInertia(other, axis))
				+ " reportedTorque=" + F(Vector3.Dot(joint.currentTorque, axis))
				+ " motorMode=" + (recent ? motor.Mode.ToString() : "none") + " motorDelta=" + F(recent ? Vector3.Dot(motor.Value, axis) : 0f));
		}
		private static float AxisInertia(Rigidbody body, Vector3 worldAxis)
		{
			Vector3 axis = Quaternion.Inverse(body.rotation * body.inertiaTensorRotation) * worldAxis, inertia = body.inertiaTensor;
			// Tensor about COM only; lever-arm and full constraint response are not inferred.
			return 1f / (axis.x * axis.x / inertia.x + axis.y * axis.y / inertia.y + axis.z * axis.z / inertia.z);
		}
		private static string F(float value) { return value.ToString("R", CultureInfo.InvariantCulture); }
	}
}
