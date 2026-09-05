using System;
using System.Runtime.CompilerServices;
using HarmonyLib;
using LanguageDetection;
using MPatchrMain;
using McnCraft;
using Translation;
using UnityEngine;

[HarmonyPatch("RPC_Chat")]
[HarmonyPatch(new Type[]
{
	typeof(string),
	typeof(string)
})]
[HarmonyPatch(typeof(MachineController))]
internal class UUi9CES6jT0ZzKFkxbFyR2jEgvhxrm6WMabMV4KBwk4K_0024MAe7FKbv3UblWLVLXi1aw
{
	[CompilerGenerated]
	private sealed class Rw1gRBZINYqqUycQDAVzVUs1tvRtJB7PxqbzqWqLgxHlSEyx8cHDq__0024jNomtsWBa_00247QqoBLee9bLdMs0_0024OVlT27m6sTWlRyUqtPVl76CFIGL
	{
		public string BxbrAfN1vHu_2B7LAFTdTgI;

		public string rk8YbdZoKLPvEMFD0jhc_jM;

		public string X8fsvmyhDx5FlIBig0C80rk;

		public int int_0;

		internal void Igw1p5BTtR8vumMgD8M16qo(string translationResult, string detectedLang)
		{
			mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(smethod_0(global::_003CModule_003E.smethod_27<string>(1847339536u), detectedLang, global::_003CModule_003E.smethod_27<string>(828267028u), MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.translation.ToString()), bool_0: true);
			detectedLang = smethod_1(detectedLang);
			if (!smethod_2(detectedLang, MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.translation.ToString()) && detectedLang != null && (!smethod_3(detectedLang, global::_003CModule_003E.smethod_26<string>(1243401777u)) || !smethod_3(detectedLang, global::_003CModule_003E.smethod_26<string>(4213635787u)) || !smethod_3(detectedLang, global::_003CModule_003E.smethod_29<string>(1593352955u)) || !smethod_3(detectedLang, global::_003CModule_003E.smethod_29<string>(4114070657u)) || !smethod_3(detectedLang, global::_003CModule_003E.smethod_25<string>(3151837443u))) && translationResult != null && !smethod_2(BxbrAfN1vHu_2B7LAFTdTgI, translationResult))
			{
				if (smethod_2(translationResult, global::_003CModule_003E.smethod_29<string>(2339821063u)))
				{
					MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.KcD_0024nCK3Ay8M_lhV1rrgPkap2XWotLEXldWxUskU_0024hzx(settingsIngame.translationEngines.microsoft);
					return;
				}
				smethod_6(Arena.OEDCBNHNGMJ as Game, smethod_5(new string[5]
				{
					rk8YbdZoKLPvEMFD0jhc_jM,
					smethod_4(global::_003CModule_003E.smethod_25<string>(1214303304u), string_0, global::_003CModule_003E.smethod_27<string>(1430423396u)),
					X8fsvmyhDx5FlIBig0C80rk,
					global::_003CModule_003E.smethod_25<string>(3011420365u),
					translationResult
				}), int_0);
			}
		}

		internal static string smethod_0(string string_0, string string_1, string string_2, string string_3)
		{
			return string_0 + string_1 + string_2 + string_3;
		}

		internal static string smethod_1(string string_0)
		{
			return string_0.ToUpper();
		}

		internal static bool smethod_2(string string_0, string string_1)
		{
			return string_0 == string_1;
		}

		internal static bool smethod_3(string string_0, string string_1)
		{
			return string_0 != string_1;
		}

		internal static string smethod_4(string string_0, string string_1, string string_2)
		{
			return string_0 + string_1 + string_2;
		}

		internal static string smethod_5(string[] string_0)
		{
			return string.Concat(string_0);
		}

		internal static void smethod_6(Game game_0, string string_0, int int_1)
		{
			game_0.RPC_Chat(string_0, int_1);
		}
	}

	internal static bool u5ER09FBgDoEuNjNt6mdw_k = true;

	internal static LanguageDetector PskB4r1albRmRPcipS0XQ5A;

	internal static readonly string string_0 = global::_003CModule_003E.smethod_26<string>(1807880002u);

	[HarmonyPrefix]
	internal static void smethod_0(MachineController __instance, string DDMLCAJGAID, string KNNKJJMKAAI, int ___LCKDHPKIPEI, string ___GOMAGBONMGB)
	{
		if (MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.translation == settingsIngame.translationMode.OFF || MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.translation == settingsIngame.translationMode.OTR || !u5ER09FBgDoEuNjNt6mdw_k || Game.IALNHPEKDON.Contains(___LCKDHPKIPEI))
		{
			return;
		}
		MachineController fICMBCLEFDL = (Arena.OEDCBNHNGMJ as Game).FICMBCLEFDL;
		if (smethod_1((UnityEngine.Object)fICMBCLEFDL, (UnityEngine.Object)null) && fICMBCLEFDL.LCKDHPKIPEI == __instance.LCKDHPKIPEI)
		{
			return;
		}
		smethod_2(KNNKJJMKAAI, global::_003CModule_003E.smethod_29<string>(4275841543u));
		if (PskB4r1albRmRPcipS0XQ5A == null)
		{
			PskB4r1albRmRPcipS0XQ5A = new LanguageDetector();
			PskB4r1albRmRPcipS0XQ5A.AddAllLanguages();
		}
		if (smethod_3(PskB4r1albRmRPcipS0XQ5A.Detect(KNNKJJMKAAI), MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.translation.ToString()))
		{
			return;
		}
		Translator.Run(KNNKJJMKAAI, MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.translation.ToString(), MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.translationEngine, delegate(string translationResult, string detectedLang)
		{
			mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(Rw1gRBZINYqqUycQDAVzVUs1tvRtJB7PxqbzqWqLgxHlSEyx8cHDq__0024jNomtsWBa_00247QqoBLee9bLdMs0_0024OVlT27m6sTWlRyUqtPVl76CFIGL.smethod_0(global::_003CModule_003E.smethod_27<string>(1847339536u), detectedLang, global::_003CModule_003E.smethod_27<string>(828267028u), MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.translation.ToString()), bool_0: true);
			detectedLang = Rw1gRBZINYqqUycQDAVzVUs1tvRtJB7PxqbzqWqLgxHlSEyx8cHDq__0024jNomtsWBa_00247QqoBLee9bLdMs0_0024OVlT27m6sTWlRyUqtPVl76CFIGL.smethod_1(detectedLang);
			if (!Rw1gRBZINYqqUycQDAVzVUs1tvRtJB7PxqbzqWqLgxHlSEyx8cHDq__0024jNomtsWBa_00247QqoBLee9bLdMs0_0024OVlT27m6sTWlRyUqtPVl76CFIGL.smethod_2(detectedLang, MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.translation.ToString()) && detectedLang != null && (!Rw1gRBZINYqqUycQDAVzVUs1tvRtJB7PxqbzqWqLgxHlSEyx8cHDq__0024jNomtsWBa_00247QqoBLee9bLdMs0_0024OVlT27m6sTWlRyUqtPVl76CFIGL.smethod_3(detectedLang, global::_003CModule_003E.smethod_26<string>(1243401777u)) || !Rw1gRBZINYqqUycQDAVzVUs1tvRtJB7PxqbzqWqLgxHlSEyx8cHDq__0024jNomtsWBa_00247QqoBLee9bLdMs0_0024OVlT27m6sTWlRyUqtPVl76CFIGL.smethod_3(detectedLang, global::_003CModule_003E.smethod_26<string>(4213635787u)) || !Rw1gRBZINYqqUycQDAVzVUs1tvRtJB7PxqbzqWqLgxHlSEyx8cHDq__0024jNomtsWBa_00247QqoBLee9bLdMs0_0024OVlT27m6sTWlRyUqtPVl76CFIGL.smethod_3(detectedLang, global::_003CModule_003E.smethod_29<string>(1593352955u)) || !Rw1gRBZINYqqUycQDAVzVUs1tvRtJB7PxqbzqWqLgxHlSEyx8cHDq__0024jNomtsWBa_00247QqoBLee9bLdMs0_0024OVlT27m6sTWlRyUqtPVl76CFIGL.smethod_3(detectedLang, global::_003CModule_003E.smethod_29<string>(4114070657u)) || !Rw1gRBZINYqqUycQDAVzVUs1tvRtJB7PxqbzqWqLgxHlSEyx8cHDq__0024jNomtsWBa_00247QqoBLee9bLdMs0_0024OVlT27m6sTWlRyUqtPVl76CFIGL.smethod_3(detectedLang, global::_003CModule_003E.smethod_25<string>(3151837443u))) && translationResult != null && !Rw1gRBZINYqqUycQDAVzVUs1tvRtJB7PxqbzqWqLgxHlSEyx8cHDq__0024jNomtsWBa_00247QqoBLee9bLdMs0_0024OVlT27m6sTWlRyUqtPVl76CFIGL.smethod_2(KNNKJJMKAAI, translationResult))
			{
				if (Rw1gRBZINYqqUycQDAVzVUs1tvRtJB7PxqbzqWqLgxHlSEyx8cHDq__0024jNomtsWBa_00247QqoBLee9bLdMs0_0024OVlT27m6sTWlRyUqtPVl76CFIGL.smethod_2(translationResult, global::_003CModule_003E.smethod_29<string>(2339821063u)))
				{
					MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.KcD_0024nCK3Ay8M_lhV1rrgPkap2XWotLEXldWxUskU_0024hzx(settingsIngame.translationEngines.microsoft);
				}
				else
				{
					Rw1gRBZINYqqUycQDAVzVUs1tvRtJB7PxqbzqWqLgxHlSEyx8cHDq__0024jNomtsWBa_00247QqoBLee9bLdMs0_0024OVlT27m6sTWlRyUqtPVl76CFIGL.smethod_6(Arena.OEDCBNHNGMJ as Game, Rw1gRBZINYqqUycQDAVzVUs1tvRtJB7PxqbzqWqLgxHlSEyx8cHDq__0024jNomtsWBa_00247QqoBLee9bLdMs0_0024OVlT27m6sTWlRyUqtPVl76CFIGL.smethod_5(new string[5]
					{
						DDMLCAJGAID,
						Rw1gRBZINYqqUycQDAVzVUs1tvRtJB7PxqbzqWqLgxHlSEyx8cHDq__0024jNomtsWBa_00247QqoBLee9bLdMs0_0024OVlT27m6sTWlRyUqtPVl76CFIGL.smethod_4(global::_003CModule_003E.smethod_25<string>(1214303304u), string_0, global::_003CModule_003E.smethod_27<string>(1430423396u)),
						___GOMAGBONMGB,
						global::_003CModule_003E.smethod_25<string>(3011420365u),
						translationResult
					}), ___LCKDHPKIPEI);
				}
			}
		});
	}

	internal static bool smethod_1(UnityEngine.Object object_0, UnityEngine.Object object_1)
	{
		return object_0 != object_1;
	}

	internal static int smethod_2(string string_1, string string_2)
	{
		return string_1.IndexOf(string_2);
	}

	internal static bool smethod_3(string string_1, string string_2)
	{
		return string_1 == string_2;
	}
}
