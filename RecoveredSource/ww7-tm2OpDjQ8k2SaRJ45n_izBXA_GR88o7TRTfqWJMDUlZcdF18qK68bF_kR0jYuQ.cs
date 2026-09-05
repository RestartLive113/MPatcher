using MPatchrMain;
using UnityEngine;

internal class ww7_0024tm2OpDjQ8k2SaRJ45n_izBXA_GR88o7TRTfqWJMDUlZcdF18qK68bF_kR0jYuQ : MonoBehaviour
{
	public void method_0()
	{
		if (smethod_0(KeyCode.LeftShift))
		{
			smethod_5(smethod_1((Component)this), smethod_4(smethod_2(), smethod_3()));
			MPatchr.ShowDebugMsg(smethod_6(smethod_1((Component)this)));
		}
	}

	internal static bool smethod_0(KeyCode keyCode_0)
	{
		return Input.GetKey(keyCode_0);
	}

	internal static Transform smethod_1(Component component_0)
	{
		return component_0.transform;
	}

	internal static Camera smethod_2()
	{
		return Camera.current;
	}

	internal static Vector3 smethod_3()
	{
		return Input.mousePosition;
	}

	internal static Vector3 smethod_4(Camera camera_0, Vector3 vector3_0)
	{
		return camera_0.ScreenToWorldPoint(vector3_0);
	}

	internal static void smethod_5(Transform transform_0, Vector3 vector3_0)
	{
		transform_0.position = vector3_0;
	}

	internal static Vector3 smethod_6(Transform transform_0)
	{
		return transform_0.localPosition;
	}
}
