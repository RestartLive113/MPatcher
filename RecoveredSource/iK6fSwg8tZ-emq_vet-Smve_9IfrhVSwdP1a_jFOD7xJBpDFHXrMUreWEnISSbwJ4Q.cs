using System;
using System.IO;
using ExitGames.Client.Photon;
using HarmonyLib;
using MPatchrMain;
using UnityEngine;
using UnityEngine.SceneManagement;

[HarmonyPatch("CJLFFPJICPC")]
[HarmonyPatch(typeof(SceneMan))]
[HarmonyPatch(new Type[]
{
	typeof(string),
	typeof(bool)
})]
internal class iK6fSwg8tZ_0024emq_vet_0024Smve_9IfrhVSwdP1a_jFOD7xJBpDFHXrMUreWEnISSbwJ4Q
{
	[HarmonyPrefix]
	internal static void smethod_0(string CBNCLLHJONG, bool MIBADJBFJDB)
	{
		XxAJ0vo10qaLovS4w9cCmko_0024VY60fHaHGA9858Yt4TL0r8NsjpNSMzO89lYqP89_00246EUCpyR3IgNa_0024kpjiKwbu6E.string_0 = "";
		XxAJ0vo10qaLovS4w9cCmko_0024VY60fHaHGA9858Yt4TL0r8NsjpNSMzO89lYqP89_00246EUCpyR3IgNa_0024kpjiKwbu6E.ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ_0 = null;
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_25<string>(1140404804u) + CBNCLLHJONG + global::_003CModule_003E.smethod_28<string>(2167813015u) + MIBADJBFJDB);
		if (MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4 != null)
		{
			MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.UUiRNMwxRbfk_Fs4cDErRoM();
		}
		if (Y_KnsAySLoCDb4SHb_0024ozdg9Jhandbv5CjRS2vvjE93HwxrEnN8gRKN_3gH8ZJ8dFtwLfFaqEhK1SoCTTPpPBgcQ.BmX9fkX90Trh4MiCFQ9HUq6Bf23SL0OB3yAPfwsaL7EbEGh22P6F6ygh7saJ5JgFCpe49M01gAXl8wnxSj3NQcZaFmNbdCsuuk37PKwTmIWo.KAhaFpfVvyK7iQzKoWEe86c != null)
		{
			JKGKJLLFMLE.MIIGKEBFKKD = Y_KnsAySLoCDb4SHb_0024ozdg9Jhandbv5CjRS2vvjE93HwxrEnN8gRKN_3gH8ZJ8dFtwLfFaqEhK1SoCTTPpPBgcQ.BmX9fkX90Trh4MiCFQ9HUq6Bf23SL0OB3yAPfwsaL7EbEGh22P6F6ygh7saJ5JgFCpe49M01gAXl8wnxSj3NQcZaFmNbdCsuuk37PKwTmIWo.KAhaFpfVvyK7iQzKoWEe86c.Clone();
			Y_KnsAySLoCDb4SHb_0024ozdg9Jhandbv5CjRS2vvjE93HwxrEnN8gRKN_3gH8ZJ8dFtwLfFaqEhK1SoCTTPpPBgcQ.BmX9fkX90Trh4MiCFQ9HUq6Bf23SL0OB3yAPfwsaL7EbEGh22P6F6ygh7saJ5JgFCpe49M01gAXl8wnxSj3NQcZaFmNbdCsuuk37PKwTmIWo.KAhaFpfVvyK7iQzKoWEe86c = null;
			mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_25<string>(1709254407u));
		}
		if (MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.vrARG && File.Exists(global::_003CModule_003E.smethod_29<string>(3660153561u)) && File.Exists(global::_003CModule_003E.smethod_28<string>(270855250u)))
		{
			string[] commandLineArgs = Environment.GetCommandLineArgs();
			for (int i = 0; i < commandLineArgs.Length; i++)
			{
				if (commandLineArgs[i] == global::_003CModule_003E.smethod_25<string>(1106828468u) && MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.vr_mode != -1)
				{
					_Xy1VslaHlYtfsUmuCgSy8DOmtBN1a9chf9fX3d_0024zmHL.jV3t994texBBSfqt24MwAxMtQHR8Q3AmTXBC9nCSTQ7D();
					break;
				}
			}
		}
		if (SceneManager.GetActiveScene().name == global::_003CModule_003E.smethod_26<string>(1204526061u) && Class35.listController_0 != null)
		{
			MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.N4UcFQkZXBLT3Ewo5_rO7w4((settingsIngame.translationMode)Class35.listController_0.VM8U7XPbYLat9FUUBIFTocY(Class35.uGQUy_0024Mw_q46atKOrYCeWos));
			MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.KcD_0024nCK3Ay8M_lhV1rrgPkap2XWotLEXldWxUskU_0024hzx((settingsIngame.translationEngines)Class35.WM8LkAhdj7QtGX92nWcmHq4.VM8U7XPbYLat9FUUBIFTocY(Class35.string_0));
			metG733wDBlDhjB0MSUi_0024WZtiv2d5Ta15QUy66BHDFij5pgSnnkUUekfxHo4GGqdng.Aab6uSDccQw2pntTBaGy7HzuanyXRRub_0024ffV4hDQeUto();
		}
		if (CBNCLLHJONG != global::_003CModule_003E.smethod_26<string>(1973698662u) && CBNCLLHJONG != global::_003CModule_003E.smethod_29<string>(2432523475u))
		{
			Y_KnsAySLoCDb4SHb_0024ozdg9Jhandbv5CjRS2vvjE93HwxrEnN8gRKN_3gH8ZJ8dFtwLfFaqEhK1SoCTTPpPBgcQ.y5kmSOqVaOjhMvxRjeJAZmo = null;
			Y_KnsAySLoCDb4SHb_0024ozdg9Jhandbv5CjRS2vvjE93HwxrEnN8gRKN_3gH8ZJ8dFtwLfFaqEhK1SoCTTPpPBgcQ.nu9wqOavhI9USqcxKp2D2JdN6BvnIdeDULV1uF0wgOAy = null;
		}
		if (CBNCLLHJONG != global::_003CModule_003E.smethod_28<string>(102257120u))
		{
			Y_KnsAySLoCDb4SHb_0024ozdg9Jhandbv5CjRS2vvjE93HwxrEnN8gRKN_3gH8ZJ8dFtwLfFaqEhK1SoCTTPpPBgcQ.OB3U2zm9e2wliliNbaRFPAY = false;
		}
		if (CBNCLLHJONG != global::_003CModule_003E.smethod_25<string>(1691553299u) && CBNCLLHJONG != global::_003CModule_003E.smethod_25<string>(3051704328u) && CBNCLLHJONG != global::_003CModule_003E.smethod_26<string>(1165509650u))
		{
			JONBPAFNPBD.APFEPHDDNFM.Protocol = ConnectionProtocol.Udp;
		}
		if (p15TFflpW2KKy78hPxhNbiPljDEt9MtSH5pTlOs7Y_0024LnvAl3Cjkk3Gzzu32qTCrlBA.vVn7afGBU8BLmNjGePpOYo89VaBf_0024RaeOHzWZmSqv8cA != null && CBNCLLHJONG != global::_003CModule_003E.smethod_28<string>(2987648538u))
		{
			UnityEngine.Object.Destroy(p15TFflpW2KKy78hPxhNbiPljDEt9MtSH5pTlOs7Y_0024LnvAl3Cjkk3Gzzu32qTCrlBA.vVn7afGBU8BLmNjGePpOYo89VaBf_0024RaeOHzWZmSqv8cA);
		}
		foreach (HostScript xIX1nY_0024eHA9QSBbIs6EBuz in boq2Pu1wVUKBmYhCHYqNyeIJToBLrl9v4fX1Wj2K7fW80xAZGrjtRfrLlnuFUPPFo7r3VDm3ADUhB_r2BgwLSN0.xIX1nY_0024eHA9QSBbIs6EBuzs)
		{
			try
			{
				xIX1nY_0024eHA9QSBbIs6EBuz.onDestroy();
			}
			catch (Exception ex)
			{
				Arena.OEDCBNHNGMJ.AddScriptLog(ex.Message, global::_003CModule_003E.smethod_25<string>(2982091682u));
				Arena.OEDCBNHNGMJ.AddScriptLog(ex.StackTrace, global::_003CModule_003E.smethod_28<string>(10900805u));
			}
		}
		boq2Pu1wVUKBmYhCHYqNyeIJToBLrl9v4fX1Wj2K7fW80xAZGrjtRfrLlnuFUPPFo7r3VDm3ADUhB_r2BgwLSN0.xIX1nY_0024eHA9QSBbIs6EBuzs.Clear();
	}
}
