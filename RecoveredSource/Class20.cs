using HarmonyLib;
using McnCraft;
using UnityEngine;

[HarmonyPatch("OnTriggerStay")]
[HarmonyPatch(typeof(BodyController))]
internal class Class20
{
	[HarmonyPrefix]
	internal static void smethod_0(Collider LLLCAFCOBHB, BodyController __instance)
	{
		smethod_1((object)global::_003CModule_003E.smethod_25<string>(1723701017u));
		MachineController cIPOPAGDJDE = __instance.CIPOPAGDJDE;
		EntityController.OCFNGMHHLLM gEOICBCHPNG = cIPOPAGDJDE.GEOICBCHPNG;
		int num = 1;
		if (gEOICBCHPNG == EntityController.OCFNGMHHLLM.NetClone)
		{
			smethod_4(cIPOPAGDJDE.GetComponent<PhotonView>(), global::_003CModule_003E.smethod_25<string>(318582909u), smethod_3(cIPOPAGDJDE.GetComponent<PhotonView>()), new object[3]
			{
				num,
				float.MaxValue,
				0
			});
		}
		else
		{
			smethod_2(cIPOPAGDJDE.i0duGIJ6M7BXC7c2cbeyvXM1hCrcoNKNJzlqDrhwjoxM<BodyController[]>(global::_003CModule_003E.smethod_28<string>(1042418993u))[num - 1], float.MaxValue, bool_0: false);
		}
	}

	internal static void smethod_1(object object_0)
	{
		Debug.Log(object_0);
	}

	internal static void smethod_2(BodyController bodyController_0, float float_0, bool bool_0)
	{
		bodyController_0.ApplyDamage(float_0, bool_0);
	}

	internal static OPLNFKECCLE smethod_3(PhotonView photonView_0)
	{
		return photonView_0.MFNPHJFOIGC;
	}

	internal static void smethod_4(PhotonView photonView_0, string string_0, OPLNFKECCLE oplnfkeccle_0, object[] object_0)
	{
		photonView_0.RPC(string_0, oplnfkeccle_0, object_0);
	}
}
