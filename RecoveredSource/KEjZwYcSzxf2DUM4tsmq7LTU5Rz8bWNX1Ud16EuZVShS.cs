using UnityEngine;
using VRGIN.Controls;
using VRGIN.Core;

internal class KEjZwYcSzxf2DUM4tsmq7LTU5Rz8bWNX1Ud16EuZVShS
{
	private static bool tFkSEGrsJkNSVaONOlQWq4c;

	private static IVRManagerContext AepoN33BA0wryjo0QDB7aBs;

	private static VRManager PcWvNjORxeL00founWCiJnk;

	internal static Controller dgGc88yQZbStwVQArlFrjJo;

	internal static Controller H8ElYMyOv_Upfw6rpoy4w2g;

	private static IVRManagerContext EfMg5m6u4cEO_0024aJE0N_In_0024g()
	{
		if (AepoN33BA0wryjo0QDB7aBs == null)
		{
			AepoN33BA0wryjo0QDB7aBs = new v8fwj6Sh74zipqAfgLp_00241v03QrY57rxEDKsDLlfUsTxb();
		}
		return AepoN33BA0wryjo0QDB7aBs;
	}

	internal static VRManager d_RafUgoVPViHGf69VfQ7eM()
	{
		return PcWvNjORxeL00founWCiJnk;
	}

	internal static void INITVR()
	{
		PcWvNjORxeL00founWCiJnk = VRManager.Create<Nz27rvAh_TUCPsctJNzLiCjjTfZt7XFBnnDiA7F_0024B8yr6ll5sodkRaq_0024s3PhpIOSQA>(EfMg5m6u4cEO_0024aJE0N_In_0024g());
		PcWvNjORxeL00founWCiJnk.SetMode<v8fwj6Sh74zipqAfgLp_00241v0npFIO5aYaLDeNaTjpNQTpqdMWHS3ztRRUN84mMCWdfg>();
		tFkSEGrsJkNSVaONOlQWq4c = true;
	}

	internal static void PRS()
	{
		if (tFkSEGrsJkNSVaONOlQWq4c)
		{
			smethod_3(smethod_2(smethod_1(smethod_0())), (Transform)null);
			smethod_5((Object)smethod_4((Component)smethod_2(smethod_1(smethod_0()))));
		}
	}

	internal static void SS(float s)
	{
		if (tFkSEGrsJkNSVaONOlQWq4c)
		{
			smethod_7(smethod_6(), s);
		}
	}

	internal static VRCamera smethod_0()
	{
		return VR.Camera;
	}

	internal static SteamVR_Camera smethod_1(VRCamera vrcamera_0)
	{
		return vrcamera_0.SteamCam;
	}

	internal static Transform smethod_2(SteamVR_Camera steamVR_Camera_0)
	{
		return steamVR_Camera_0.origin;
	}

	internal static void smethod_3(Transform transform_0, Transform transform_1)
	{
		transform_0.SetParent(transform_1);
	}

	internal static GameObject smethod_4(Component component_0)
	{
		return component_0.gameObject;
	}

	internal static void smethod_5(Object object_0)
	{
		Object.DontDestroyOnLoad(object_0);
	}

	internal static VRSettings smethod_6()
	{
		return VR.Settings;
	}

	internal static void smethod_7(VRSettings vrsettings_0, float float_0)
	{
		vrsettings_0.IPDScale = float_0;
	}
}
