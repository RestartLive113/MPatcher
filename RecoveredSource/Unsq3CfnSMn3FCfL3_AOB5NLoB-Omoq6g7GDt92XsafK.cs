using UnityEngine;

internal static class Unsq3CfnSMn3FCfL3_AOB5NLoB_0024Omoq6g7GDt92XsafK
{
	internal static Vector3 s5kQPgZ5bcXpM_0024NqtOcz05ygRaXORIoZiK4US0SDGfSJ(this Transform transform_0, Vector3 vector3_0)
	{
		return Matrix4x4.TRS(smethod_0(transform_0), smethod_1(transform_0), Vector3.one).MultiplyPoint3x4(vector3_0);
	}

	internal static Vector3 exhY6_0024AhIhNhSJdwumimcf0iHRTFwCXVc9Jdygwp0YSE(this Transform transform_0, Vector3 vector3_0)
	{
		return Matrix4x4.TRS(smethod_0(transform_0), smethod_1(transform_0), Vector3.one).inverse.MultiplyPoint3x4(vector3_0);
	}

	internal static Vector3 smethod_0(Transform transform_0)
	{
		return transform_0.position;
	}

	internal static Quaternion smethod_1(Transform transform_0)
	{
		return transform_0.rotation;
	}
}
