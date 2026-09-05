using UnityEngine;

internal class GgrMl0eHjkTKzAqKC9Taf8i2a4kabVFQXHtJQoLFRBUAsOCvMpzQHKVdH70BhjSZvQ : pQCv4GWEmO7suQD936qIBn22jIu63BZDtto8pweiUAZY
{
	private GameObject[] pWD_0024JR_0024_do_00247D5Sw6GplVOI;

	internal GgrMl0eHjkTKzAqKC9Taf8i2a4kabVFQXHtJQoLFRBUAsOCvMpzQHKVdH70BhjSZvQ(GameObject[] objs)
	{
		pWD_0024JR_0024_do_00247D5Sw6GplVOI = objs;
	}

	public void bv4xsECkipN_002441Wa7mcfeqY()
	{
		GameObject[] array = pWD_0024JR_0024_do_00247D5Sw6GplVOI;
		foreach (GameObject obj in array)
		{
			smethod_0(obj, bool_0: false);
			PointController component = obj.GetComponent<PointController>();
			if (smethod_1((Object)component, (Object)null))
			{
				smethod_0(Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<GameObject>(global::_003CModule_003E.smethod_28<string>(3392897779u), component), bool_0: false);
			}
		}
	}

	public void M_UYuO7m_5zc0NRNG1OJLAE()
	{
		GameObject[] array = pWD_0024JR_0024_do_00247D5Sw6GplVOI;
		foreach (GameObject obj in array)
		{
			smethod_0(obj, bool_0: true);
			PointController component = obj.GetComponent<PointController>();
			if (smethod_1((Object)component, (Object)null))
			{
				smethod_0(Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<GameObject>(global::_003CModule_003E.smethod_27<string>(3350128162u), component), bool_0: true);
			}
		}
	}

	internal static void smethod_0(GameObject gameObject_0, bool bool_0)
	{
		gameObject_0.SetActive(bool_0);
	}

	internal static bool smethod_1(Object object_0, Object object_1)
	{
		return object_0 != object_1;
	}
}
