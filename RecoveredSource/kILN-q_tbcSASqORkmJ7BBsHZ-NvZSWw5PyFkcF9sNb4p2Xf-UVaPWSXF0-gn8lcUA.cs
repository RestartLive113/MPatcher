using System.Collections.Generic;
using ExitGames.Client.Photon;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;

[HarmonyPatch("ODJCBKPHNHF")]
[HarmonyPatch(typeof(Configure))]
internal class kILN_0024q_tbcSASqORkmJ7BBsHZ_0024NvZSWw5PyFkcF9sNb4p2Xf_0024UVaPWSXF0_0024gn8lcUA
{
	internal static ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ LC0iMCkMK03PiX6mz5DQcnM;

	[HarmonyPrefix]
	internal static bool smethod_0(Configure __instance)
	{
		if (smethod_1((Object)LC0iMCkMK03PiX6mz5DQcnM, (Object)null) && !smethod_2(LC0iMCkMK03PiX6mz5DQcnM.pZEKY5TzLd4S3z2lXESoRnw))
		{
			HMKGOOGACGN hMKGOOGACGN = smethod_3();
			smethod_4(hMKGOOGACGN, bool_0: true);
			smethod_5(hMKGOOGACGN, bool_0: true);
			hMKGOOGACGN.CAEKPAOEKMK = (byte)smethod_7(smethod_6((SceneMan)__instance, global::_003CModule_003E.smethod_25<string>(3105243115u)).GetComponent<Slider>());
			hMKGOOGACGN.PPBILCPCOMH = Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<Configure, Hashtable>(global::_003CModule_003E.smethod_28<string>(2512602667u), __instance);
			hMKGOOGACGN.KJKPKGDIOLA = Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<Configure, List<string>>(global::_003CModule_003E.smethod_29<string>(1370005351u), __instance).ToArray();
			if (HNJDDKJLHMM.OLHPKFAKNOG)
			{
				hMKGOOGACGN.CAEKPAOEKMK++;
			}
			smethod_9(HNJDDKJLHMM.JCOLMIBIGOP, hMKGOOGACGN, smethod_8(LC0iMCkMK03PiX6mz5DQcnM.pZEKY5TzLd4S3z2lXESoRnw, LPBOIGNBIGO.Default));
			smethod_11(smethod_10(), 1);
			Class17.DMoImhPC2jNUt2tgmSezeT_IK_0024XK5Df9PmexG_CaKb1R(global::_003CModule_003E.smethod_26<string>(4288463053u), __instance, 7777);
			return false;
		}
		return true;
	}

	internal static bool smethod_1(Object object_0, Object object_1)
	{
		return object_0 != object_1;
	}

	internal static bool smethod_2(string string_0)
	{
		return string.IsNullOrEmpty(string_0);
	}

	internal static HMKGOOGACGN smethod_3()
	{
		return new HMKGOOGACGN();
	}

	internal static void smethod_4(HMKGOOGACGN hmkgoogacgn_0, bool bool_0)
	{
		hmkgoogacgn_0.CALCLODIOAF = bool_0;
	}

	internal static void smethod_5(HMKGOOGACGN hmkgoogacgn_0, bool bool_0)
	{
		hmkgoogacgn_0.KEMDJOIEPJM = bool_0;
	}

	internal static GameObject smethod_6(SceneMan sceneMan_0, string string_0)
	{
		return sceneMan_0.GetSLD(string_0);
	}

	internal static float smethod_7(Slider slider_0)
	{
		return slider_0.value;
	}

	internal static PEKCNAMEPIA smethod_8(string string_0, LPBOIGNBIGO lpboignbigo_0)
	{
		return new PEKCNAMEPIA(string_0, lpboignbigo_0);
	}

	internal static bool smethod_9(string string_0, HMKGOOGACGN hmkgoogacgn_0, PEKCNAMEPIA pekcnamepia_0)
	{
		return JONBPAFNPBD.ODJCBKPHNHF(string_0, hmkgoogacgn_0, pekcnamepia_0);
	}

	internal static OPLNFKECCLE smethod_10()
	{
		return JONBPAFNPBD.DBLGHCEAEHC;
	}

	internal static void smethod_11(OPLNFKECCLE oplnfkeccle_0, int int_0)
	{
		oplnfkeccle_0.ICMHBALEPBM(int_0);
	}
}
