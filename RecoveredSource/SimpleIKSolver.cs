using System;
using UnityEngine;

public class SimpleIKSolver : MonoBehaviour
{
	[Serializable]
	public class JointEntity
	{
		public Transform Joint;

		public AngleRestriction AngleRestrictionRange;

		internal Quaternion _initialRotation;

		public JointEntity()
		{
		}

		public JointEntity(Transform transform_0)
		{
			Joint = transform_0;
			AngleRestrictionRange = new AngleRestriction();
		}
	}

	[Serializable]
	public class AngleRestriction
	{
		public bool xAxis;

		public float xMin = -180f;

		public float xMax = 180f;

		public bool yAxis;

		public float yMin = -180f;

		public float yMax = 180f;

		public bool zAxis;

		public float zMin = 180f;

		public float zMax = 180f;
	}

	private const float CMlWRJ3WnGi6LyAUg9ehT7c = 0.125f;

	private const int f_uK3neta0gFaBF3UzIbldE = 20;

	public bool IsActive = true;

	public Transform Target;

	public JointEntity[] JointEntities;

	public bool IsDamping;

	public float DampingMax = 0.5f;

	private void sp_GCK595YHY1vrEPNGiSrQ()
	{
		if (smethod_0((UnityEngine.Object)Target, (UnityEngine.Object)null))
		{
			Target = smethod_1((Component)this);
		}
		JointEntity[] jointEntities = JointEntities;
		foreach (JointEntity obj in jointEntities)
		{
			obj._initialRotation = smethod_2(obj.Joint);
		}
	}

	private void method_0()
	{
		if (IsActive)
		{
			i5uNwW5RolDIWHdol8WQGOY();
		}
	}

	private void i5uNwW5RolDIWHdol8WQGOY()
	{
		Transform joint = JointEntities[JointEntities.Length - 1].Joint;
		Vector3 zero = Vector3.zero;
		Vector3 zero2 = Vector3.zero;
		Vector3 zero3 = Vector3.zero;
		int num = JointEntities.Length - 1;
		int num2 = 0;
		Vector3 vector2;
		do
		{
			if (num < 0)
			{
				num = JointEntities.Length - 1;
			}
			Vector3 vector = smethod_3(JointEntities[num].Joint);
			vector2 = smethod_3(joint);
			zero2 = vector2 - vector;
			zero = smethod_3(Target) - vector;
			zero2.Normalize();
			zero.Normalize();
			float num3 = Vector3.Dot(zero2, zero);
			if (num3 < 0.99999f)
			{
				zero3 = Vector3.Cross(zero2, zero);
				zero3.Normalize();
				float num4 = Mathf.Acos(num3);
				if (IsDamping && num4 > DampingMax)
				{
					num4 = DampingMax;
				}
				num4 *= 57.29578f;
				JointEntities[num].Joint.rotation = Quaternion.AngleAxis(num4, zero3) * JointEntities[num].Joint.rotation;
				csXG5q_0024HaddKfAXiFVz5Ho9LJ7PrA44CzCcT18XpiKfg(JointEntities[num]);
			}
			num--;
		}
		while (num2++ < 20 && (vector2 - Target.position).sqrMagnitude > 0.125f);
	}

	private void csXG5q_0024HaddKfAXiFVz5Ho9LJ7PrA44CzCcT18XpiKfg(JointEntity jointEntity_0)
	{
		Vector3 eulerAngles = smethod_2(jointEntity_0.Joint).eulerAngles;
		if (jointEntity_0.AngleRestrictionRange.xAxis)
		{
			if (eulerAngles.x > 180f)
			{
				eulerAngles.x -= 360f;
			}
			eulerAngles.x = Mathf.Clamp(eulerAngles.x, jointEntity_0.AngleRestrictionRange.xMin, jointEntity_0.AngleRestrictionRange.xMax);
		}
		if (jointEntity_0.AngleRestrictionRange.yAxis)
		{
			if (eulerAngles.y > 180f)
			{
				eulerAngles.y -= 360f;
			}
			eulerAngles.y = Mathf.Clamp(eulerAngles.y, jointEntity_0.AngleRestrictionRange.yMin, jointEntity_0.AngleRestrictionRange.yMax);
		}
		if (jointEntity_0.AngleRestrictionRange.zAxis)
		{
			if (eulerAngles.z > 180f)
			{
				eulerAngles.z -= 360f;
			}
			eulerAngles.z = Mathf.Clamp(eulerAngles.z, jointEntity_0.AngleRestrictionRange.zMin, jointEntity_0.AngleRestrictionRange.zMax);
		}
		jointEntity_0.Joint.localEulerAngles = eulerAngles;
	}

	public void ResetJoints()
	{
		JointEntity[] jointEntities = JointEntities;
		foreach (JointEntity jointEntity in jointEntities)
		{
			smethod_4(jointEntity.Joint, jointEntity._initialRotation);
		}
	}

	internal static bool smethod_0(UnityEngine.Object object_0, UnityEngine.Object object_1)
	{
		return object_0 == object_1;
	}

	internal static Transform smethod_1(Component component_0)
	{
		return component_0.transform;
	}

	internal static Quaternion smethod_2(Transform transform_0)
	{
		return transform_0.localRotation;
	}

	internal static Vector3 smethod_3(Transform transform_0)
	{
		return transform_0.position;
	}

	internal static void smethod_4(Transform transform_0, Quaternion quaternion_0)
	{
		transform_0.localRotation = quaternion_0;
	}
}
