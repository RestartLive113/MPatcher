using UnityEngine;
using Valve.VR;

internal static class h2lGWq7sdOQyg331JpPXlr0gm_0024aR7ZGDluTAO3U_002407Foi2urXCbaPrmt7Ys4eB3o7g
{
	private const float float_0 = 0.3f;

	public static bool LvOk3Rn3olmFHkvFGvfmBfw(this SteamVR_Controller.Device device_0, EVRButtonId evrbuttonId_0)
	{
		if (smethod_0(device_0, 4294967296uL))
		{
			Vector2 vector = smethod_1(device_0, EVRButtonId.k_EButton_Axis0);
			if (vector.y > 0.7f)
			{
				return evrbuttonId_0 == EVRButtonId.k_EButton_DPad_Up;
			}
			if (!(vector.y >= 0.3f))
			{
				return evrbuttonId_0 == EVRButtonId.k_EButton_DPad_Down;
			}
			if (vector.x > 0.7f)
			{
				return evrbuttonId_0 == EVRButtonId.k_EButton_DPad_Right;
			}
			if (vector.x < 0.3f)
			{
				return evrbuttonId_0 == EVRButtonId.k_EButton_DPad_Left;
			}
		}
		return false;
	}

	internal static bool smethod_0(SteamVR_Controller.Device device_0, ulong ulong_0)
	{
		return device_0.GetPress(ulong_0);
	}

	internal static Vector2 smethod_1(SteamVR_Controller.Device device_0, EVRButtonId evrbuttonId_0)
	{
		return device_0.GetAxis(evrbuttonId_0);
	}
}
