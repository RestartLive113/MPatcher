using System;
using HarmonyLib;
using MPatchrMain;
using McnCraft;
using UnityEngine;

internal static class pTbXl0BQalSWSABflrYhjNmk_A7RB7wKIBoc_0024LjdnXpzOQoqdduhV2bTbESRxybyAA
{
	[HarmonyPatch(typeof(KEFHJCGICLE))]
	[HarmonyPatch("HNAHBIMJDCB")]
	internal static class AjldIet_ZgmqPhFe2_zL1VF_IzreX0P_ZeYtsbxVHW25oGvB5MHBm6VM2xEH6UZouusAh4iwfCMNWLrRubl63lkI5U_cLotTe61529dVwpp_0024enHG0lhGLCR_0024JIfHOzV4jA
	{
		[HarmonyPrefix]
		internal static bool smethod_0(string CBNCLLHJONG, float NDHDALEEEOP)
		{
			if (!MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.audioCutoffFix)
			{
				return true;
			}
			AudioSource[] array = Class17.Ft_0024nB0s4dAgugQeebb6PgzB4yHCRQNxXyCsH1X_0024ugky6<AudioSource[]>(smethod_1(typeof(KEFHJCGICLE).TypeHandle), global::_003CModule_003E.smethod_27<string>(2754603156u));
			for (int i = 0; i < array.Length; i++)
			{
				if (smethod_2((UnityEngine.Object)array[i], (UnityEngine.Object)null))
				{
					return false;
				}
			}
			return true;
		}

		internal static Type smethod_1(RuntimeTypeHandle runtimeTypeHandle_0)
		{
			return Type.GetTypeFromHandle(runtimeTypeHandle_0);
		}

		internal static bool smethod_2(UnityEngine.Object object_0, UnityEngine.Object object_1)
		{
			return object_0 == object_1;
		}
	}

	[HarmonyPatch(typeof(MachineController))]
	[HarmonyPatch("Awake")]
	internal static class _ksMqmG_0024wFoYcgR7FFZgQraIKCA58Zjx0aPJS7A87WYMf_YJQatyE__OuCCeaka_HN475HlADCiG3Iz_0024gQUJSblZTieLn7y2Ck5b1i9EHeNndh_00244L56uMXtY5UFYRbIC6w
	{
		internal static Material SFOosMzb_XbDTwovJ3J28wg;

		[HarmonyPostfix]
		internal static void FeUAVwFbW6wGJJdNimZY9yI(MachineController __instance)
		{
			if (smethod_0((UnityEngine.Object)SFOosMzb_XbDTwovJ3J28wg, (UnityEngine.Object)null))
			{
				SFOosMzb_XbDTwovJ3J28wg = UnityEngine.Object.Instantiate(__instance.BDAHJMCJKLD);
			}
		}

		internal static bool smethod_0(UnityEngine.Object object_0, UnityEngine.Object object_1)
		{
			return object_0 == object_1;
		}
	}
}
