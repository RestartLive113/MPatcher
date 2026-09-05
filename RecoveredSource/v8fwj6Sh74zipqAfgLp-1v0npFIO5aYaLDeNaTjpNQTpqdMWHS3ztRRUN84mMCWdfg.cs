using MPatchrMain;
using UnityEngine;
using VRGIN.Core;
using VRGIN.Modes;
using Valve.VR;

internal class v8fwj6Sh74zipqAfgLp_00241v0npFIO5aYaLDeNaTjpNQTpqdMWHS3ztRRUN84mMCWdfg : SeatedMode
{
	private static Camera odfJBQTDcC_0024hszgpkWVB9Vc;

	protected override void OnLevel(int level)
	{
		smethod_1(smethod_0(), 3f);
		smethod_4(smethod_2(), smethod_3(), bool_0: false, bool_1: false);
		smethod_5(smethod_2(), smethod_3());
		smethod_6(smethod_2());
		if (smethod_11((Object)smethod_9(smethod_8(smethod_7(smethod_2()))), (Object)smethod_10((Component)smethod_3())))
		{
			smethod_12(smethod_8(smethod_7(smethod_2())), smethod_10((Component)smethod_3()));
			smethod_13(smethod_8(smethod_7(smethod_2())), Quaternion.identity);
		}
	}

	protected override void OnUpdate()
	{
		if (smethod_14((Object)odfJBQTDcC_0024hszgpkWVB9Vc, (Object)null))
		{
			odfJBQTDcC_0024hszgpkWVB9Vc = smethod_7(smethod_2()).GetComponent<Camera>();
		}
		if (smethod_11((Object)odfJBQTDcC_0024hszgpkWVB9Vc, (Object)null) && MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.vr_gameRendDist)
		{
			smethod_16(odfJBQTDcC_0024hszgpkWVB9Vc, smethod_15(smethod_3()) - 500f);
		}
		base.OnUpdate();
		if (smethod_17(SystemData.EHLMFKOOHLI.Modifier) && smethod_18(KeyCode.C))
		{
			smethod_20(smethod_19());
		}
		smethod_10((Component)Monitor).localPosition = new Vector3(0f, 0f, 0.3f);
		VR.Camera.SteamCam.origin.rotation = Camera.main.transform.rotation;
		if (VR.Camera.SteamCam.origin.parent != null)
		{
			if (MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.vr_camOffset)
			{
				VR.Camera.SteamCam.origin.localPosition = new Vector3(0f, 0f, 1f);
			}
			else
			{
				VR.Camera.SteamCam.origin.localPosition = Vector3.zero;
			}
		}
		else
		{
			VR.Camera.SteamCam.origin.position = Camera.main.transform.position;
		}
	}

	protected override void OnStart()
	{
		base.OnStart();
	}

	internal static VRSettings smethod_0()
	{
		return VR.Settings;
	}

	internal static void smethod_1(VRSettings vrsettings_0, float float_0)
	{
		vrsettings_0.IPDScale = float_0;
	}

	internal static VRCamera smethod_2()
	{
		return VR.Camera;
	}

	internal static Camera smethod_3()
	{
		return Camera.main;
	}

	internal static void smethod_4(VRCamera vrcamera_0, Camera camera_0, bool bool_0, bool bool_1)
	{
		vrcamera_0.Copy(camera_0, bool_0, bool_1);
	}

	internal static void smethod_5(VRCamera vrcamera_0, Camera camera_0)
	{
		vrcamera_0.CopyFX(camera_0);
	}

	internal static void smethod_6(VRCamera vrcamera_0)
	{
		vrcamera_0.Refresh();
	}

	internal static SteamVR_Camera smethod_7(VRCamera vrcamera_0)
	{
		return vrcamera_0.SteamCam;
	}

	internal static Transform smethod_8(SteamVR_Camera steamVR_Camera_0)
	{
		return steamVR_Camera_0.origin;
	}

	internal static Transform smethod_9(Transform transform_0)
	{
		return transform_0.parent;
	}

	internal static Transform smethod_10(Component component_0)
	{
		return component_0.transform;
	}

	internal static bool smethod_11(Object object_0, Object object_1)
	{
		return object_0 != object_1;
	}

	internal static void smethod_12(Transform transform_0, Transform transform_1)
	{
		transform_0.parent = transform_1;
	}

	internal static void smethod_13(Transform transform_0, Quaternion quaternion_0)
	{
		transform_0.localRotation = quaternion_0;
	}

	internal static bool smethod_14(Object object_0, Object object_1)
	{
		return object_0 == object_1;
	}

	internal static float smethod_15(Camera camera_0)
	{
		return camera_0.farClipPlane;
	}

	internal static void smethod_16(Camera camera_0, float float_0)
	{
		camera_0.farClipPlane = float_0;
	}

	internal static bool smethod_17(SystemData.EHLMFKOOHLI ehlmfkoohli_0)
	{
		return HOCGCCAIPFF.AFLJECMLJDL(ehlmfkoohli_0);
	}

	internal static bool smethod_18(KeyCode keyCode_0)
	{
		return Input.GetKeyDown(keyCode_0);
	}

	internal static CVRSystem smethod_19()
	{
		return OpenVR.System;
	}

	internal static void smethod_20(CVRSystem cvrsystem_0)
	{
		cvrsystem_0.ResetSeatedZeroPose();
	}
}
