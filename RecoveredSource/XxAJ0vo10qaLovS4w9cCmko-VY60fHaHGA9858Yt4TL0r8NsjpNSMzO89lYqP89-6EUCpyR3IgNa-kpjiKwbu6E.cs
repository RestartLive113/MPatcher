using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HarmonyLib;
using MPatchrMain;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[HarmonyPatch(typeof(SceneMan))]
[HarmonyPatch("NNMOPNJABNE")]
[HarmonyPatch(new Type[]
{
	typeof(string),
	typeof(string),
	typeof(string)
})]
internal class XxAJ0vo10qaLovS4w9cCmko_0024VY60fHaHGA9858Yt4TL0r8NsjpNSMzO89lYqP89_00246EUCpyR3IgNa_0024kpjiKwbu6E
{
	[HarmonyPatch(typeof(Menu))]
	[HarmonyPatch("BDKIMPEDKCJ")]
	internal class VTCBWB3Ryo2bP94njcplaeAsG5unvSI0dsLCPlzBTGoC_0024Rtr72ooetvguyDPDT5DvLlqkFCGrcAcuPu5lr_LZU2dEhnq6yjA6UBg8E9sOp0I
	{
		[HarmonyPrefix]
		internal static bool smethod_0(string DPGKEOAGONA, GameObject NGLBLAGMBLN, Menu __instance)
		{
			if (smethod_1(DPGKEOAGONA, global::_003CModule_003E.smethod_25<string>(1390756692u)))
			{
				Text component = smethod_3(smethod_2(NGLBLAGMBLN), 0).GetComponent<Text>();
				if (!(smethod_4((Graphic)component) == Color.yellow) && !(smethod_4((Graphic)component) == Color.magenta))
				{
					smethod_7((SceneMan)__instance, global::_003CModule_003E.smethod_28<string>(1343975903u), bool_0: false);
					JKGKJLLFMLE.IGOBPLOLHEP.machineName = smethod_5(component);
					smethod_8(bool_0: false);
					smethod_9((SceneMan)__instance, global::_003CModule_003E.smethod_27<string>(1153812668u), smethod_5(component));
					__instance.OPJGFMKMJJP = true;
					Class17.DQGZpGELKqeKmXweFYGEcaXR73CqGywHQI6u2TATGL80(global::_003CModule_003E.smethod_29<string>(3727860964u), __instance);
					Class17.DQGZpGELKqeKmXweFYGEcaXR73CqGywHQI6u2TATGL80(global::_003CModule_003E.smethod_28<string>(2257094432u), __instance, false);
				}
				else
				{
					JKGKJLLFMLE.CFGKIAPCDLB = ((!smethod_1(smethod_5(component), global::_003CModule_003E.smethod_25<string>(1597333287u))) ? smethod_5(component) : string.Empty);
					Class17.DQGZpGELKqeKmXweFYGEcaXR73CqGywHQI6u2TATGL80(global::_003CModule_003E.smethod_27<string>(2698698712u), __instance, new Type[3]
					{
						smethod_6(typeof(string).TypeHandle),
						smethod_6(typeof(string).TypeHandle),
						smethod_6(typeof(string).TypeHandle)
					}, JKGKJLLFMLE.CFGKIAPCDLB, JKGKJLLFMLE.IGOBPLOLHEP.machineName, null);
				}
				return false;
			}
			return true;
		}

		internal static bool smethod_1(string string_0, string string_1)
		{
			return string_0 == string_1;
		}

		internal static Transform smethod_2(GameObject gameObject_0)
		{
			return gameObject_0.transform;
		}

		internal static Transform smethod_3(Transform transform_0, int int_0)
		{
			return transform_0.GetChild(int_0);
		}

		internal static Color smethod_4(Graphic graphic_0)
		{
			return graphic_0.color;
		}

		internal static string smethod_5(Text text_0)
		{
			return text_0.text;
		}

		internal static Type smethod_6(RuntimeTypeHandle runtimeTypeHandle_0)
		{
			return Type.GetTypeFromHandle(runtimeTypeHandle_0);
		}

		internal static void smethod_7(SceneMan sceneMan_0, string string_0, bool bool_0)
		{
			sceneMan_0.ValidatePNL(string_0, bool_0);
		}

		internal static bool smethod_8(bool bool_0)
		{
			return JKGKJLLFMLE.MIONNHPELLN(bool_0);
		}

		internal static void smethod_9(SceneMan sceneMan_0, string string_0, string string_1)
		{
			sceneMan_0.SetInputText(string_0, string_1);
		}
	}

	[Serializable]
	[CompilerGenerated]
	private sealed class D680s8YNf6dxnA_0024HNI2OIv79j2yiv0FOOgkNX7zkQmAyws0P36gDI6M4qocfLuvrLY4HfJBuF_9_y4Dt1IzC9PETVgobk9MuwfB23gQpj7rbaWjb49Gg5wdwKWoCvzY8Rg
	{
		public static readonly D680s8YNf6dxnA_0024HNI2OIv79j2yiv0FOOgkNX7zkQmAyws0P36gDI6M4qocfLuvrLY4HfJBuF_9_y4Dt1IzC9PETVgobk9MuwfB23gQpj7rbaWjb49Gg5wdwKWoCvzY8Rg _003C_003E9 = new D680s8YNf6dxnA_0024HNI2OIv79j2yiv0FOOgkNX7zkQmAyws0P36gDI6M4qocfLuvrLY4HfJBuF_9_y4Dt1IzC9PETVgobk9MuwfB23gQpj7rbaWjb49Gg5wdwKWoCvzY8Rg();

		public static UnityAction<string> _003C_003E9__3_1;

		public static Action _003C_003E9__3_0;

		internal void mvQ1WfNNc78oJSjtQdL2uFE()
		{
			string name = smethod_0().name;
			if (!MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.QOL || name != global::_003CModule_003E.smethod_28<string>(3169768340u) || SceneMan.JFAOKFIDAGK.GetPNL(global::_003CModule_003E.smethod_25<string>(1638648606u)) == null || !SceneMan.JFAOKFIDAGK.GetPNL(global::_003CModule_003E.smethod_27<string>(1986762476u)).activeInHierarchy || (!Input.GetKeyDown(KeyCode.LeftShift) && !Input.GetKeyDown(KeyCode.RightShift)) || !(name == global::_003CModule_003E.smethod_26<string>(1306246642u)))
			{
				return;
			}
			if (ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ_0 == null)
			{
				ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ_0 = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.bqYYMQnP2SDqYH85wmN_0024evI(global::_003CModule_003E.smethod_26<string>(183002176u), JV8JAQLOIUh6NTPHUb4_gac(0), global::_003CModule_003E.smethod_29<string>(179361776u), SceneMan.JFAOKFIDAGK.GetPNL(global::_003CModule_003E.smethod_28<string>(1950943105u)).transform, string_0);
				ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ_0.DmPZGWxJ26_0024f_0024QOvQiqpmW8(new Vector2(-150f, 0f));
				ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ_0.BSdnl9DYm6Rd4cVhJ555c_A.onValueChanged.AddListener(delegate
				{
					string_0 = ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ_0.pZEKY5TzLd4S3z2lXESoRnw;
					Class17.DQGZpGELKqeKmXweFYGEcaXR73CqGywHQI6u2TATGL80(global::_003CModule_003E.smethod_28<string>(1161856101u), SceneMan.JFAOKFIDAGK, new Type[3]
					{
						smethod_1(typeof(string).TypeHandle),
						smethod_1(typeof(string).TypeHandle),
						smethod_1(typeof(string).TypeHandle)
					}, JKGKJLLFMLE.CFGKIAPCDLB, JKGKJLLFMLE.IGOBPLOLHEP.machineName, null);
					smethod_2((Selectable)ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ_0.BSdnl9DYm6Rd4cVhJ555c_A);
				});
			}
			else
			{
				ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ_0.gameObject.SetActive(value: true);
			}
			ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ_0.transform.SetAsFirstSibling();
			Class17.DQGZpGELKqeKmXweFYGEcaXR73CqGywHQI6u2TATGL80(global::_003CModule_003E.smethod_28<string>(1161856101u), SceneMan.JFAOKFIDAGK, new Type[3]
			{
				typeof(string),
				typeof(string),
				typeof(string)
			}, JKGKJLLFMLE.CFGKIAPCDLB, JKGKJLLFMLE.IGOBPLOLHEP.machineName, null);
		}

		internal void m9veTC64eFICbyA_BU3wJgY(string value)
		{
			string_0 = ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ_0.pZEKY5TzLd4S3z2lXESoRnw;
			Class17.DQGZpGELKqeKmXweFYGEcaXR73CqGywHQI6u2TATGL80(global::_003CModule_003E.smethod_28<string>(1161856101u), SceneMan.JFAOKFIDAGK, new Type[3]
			{
				smethod_1(typeof(string).TypeHandle),
				smethod_1(typeof(string).TypeHandle),
				smethod_1(typeof(string).TypeHandle)
			}, JKGKJLLFMLE.CFGKIAPCDLB, JKGKJLLFMLE.IGOBPLOLHEP.machineName, null);
			smethod_2((Selectable)ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ_0.BSdnl9DYm6Rd4cVhJ555c_A);
		}

		internal static Scene smethod_0()
		{
			return SceneManager.GetActiveScene();
		}

		internal static Type smethod_1(RuntimeTypeHandle runtimeTypeHandle_0)
		{
			return Type.GetTypeFromHandle(runtimeTypeHandle_0);
		}

		internal static void smethod_2(Selectable selectable_0)
		{
			selectable_0.Select();
		}
	}

	internal static string string_0 = "";

	internal static ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ_0 = null;

	private static Vector2 JV8JAQLOIUh6NTPHUb4_gac(int index)
	{
		return new Vector2(index % 4 * 315 - 470, -(index / 4) * 58 - 29);
	}

	[HarmonyPrefix]
	internal static bool smethod_0(string KNJKMHJHHNF, string HCFHKNOLMGA, string IMCFODFJAIJ, SceneMan __instance)
	{
		if (!MPatchr.NVLDd8Md_CiOlwr_00245znsTdSBQWRcTl3QD_NMvxUXuYfT(global::_003CModule_003E.smethod_29<string>(4138728331u)) && MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.QOL && smethod_1().name == global::_003CModule_003E.smethod_29<string>(52462462u))
		{
			MPatchr.IqEoTLbjuIvkBlM_0024FuGaiKp4jfGtyoFGXu7ctG9PkRuX(global::_003CModule_003E.smethod_25<string>(3303882096u), delegate
			{
				string name = D680s8YNf6dxnA_0024HNI2OIv79j2yiv0FOOgkNX7zkQmAyws0P36gDI6M4qocfLuvrLY4HfJBuF_9_y4Dt1IzC9PETVgobk9MuwfB23gQpj7rbaWjb49Gg5wdwKWoCvzY8Rg.smethod_0().name;
				if (MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.QOL && !(name != global::_003CModule_003E.smethod_28<string>(3169768340u)) && !(SceneMan.JFAOKFIDAGK.GetPNL(global::_003CModule_003E.smethod_25<string>(1638648606u)) == null) && SceneMan.JFAOKFIDAGK.GetPNL(global::_003CModule_003E.smethod_27<string>(1986762476u)).activeInHierarchy && (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) && name == global::_003CModule_003E.smethod_26<string>(1306246642u))
				{
					if (ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ_0 == null)
					{
						ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ_0 = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.bqYYMQnP2SDqYH85wmN_0024evI(global::_003CModule_003E.smethod_26<string>(183002176u), JV8JAQLOIUh6NTPHUb4_gac(0), global::_003CModule_003E.smethod_29<string>(179361776u), SceneMan.JFAOKFIDAGK.GetPNL(global::_003CModule_003E.smethod_28<string>(1950943105u)).transform, string_0);
						ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ_0.DmPZGWxJ26_0024f_0024QOvQiqpmW8(new Vector2(-150f, 0f));
						ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ_0.BSdnl9DYm6Rd4cVhJ555c_A.onValueChanged.AddListener(delegate
						{
							string_0 = ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ_0.pZEKY5TzLd4S3z2lXESoRnw;
							Class17.DQGZpGELKqeKmXweFYGEcaXR73CqGywHQI6u2TATGL80(global::_003CModule_003E.smethod_28<string>(1161856101u), SceneMan.JFAOKFIDAGK, new Type[3]
							{
								D680s8YNf6dxnA_0024HNI2OIv79j2yiv0FOOgkNX7zkQmAyws0P36gDI6M4qocfLuvrLY4HfJBuF_9_y4Dt1IzC9PETVgobk9MuwfB23gQpj7rbaWjb49Gg5wdwKWoCvzY8Rg.smethod_1(typeof(string).TypeHandle),
								D680s8YNf6dxnA_0024HNI2OIv79j2yiv0FOOgkNX7zkQmAyws0P36gDI6M4qocfLuvrLY4HfJBuF_9_y4Dt1IzC9PETVgobk9MuwfB23gQpj7rbaWjb49Gg5wdwKWoCvzY8Rg.smethod_1(typeof(string).TypeHandle),
								D680s8YNf6dxnA_0024HNI2OIv79j2yiv0FOOgkNX7zkQmAyws0P36gDI6M4qocfLuvrLY4HfJBuF_9_y4Dt1IzC9PETVgobk9MuwfB23gQpj7rbaWjb49Gg5wdwKWoCvzY8Rg.smethod_1(typeof(string).TypeHandle)
							}, JKGKJLLFMLE.CFGKIAPCDLB, JKGKJLLFMLE.IGOBPLOLHEP.machineName, null);
							D680s8YNf6dxnA_0024HNI2OIv79j2yiv0FOOgkNX7zkQmAyws0P36gDI6M4qocfLuvrLY4HfJBuF_9_y4Dt1IzC9PETVgobk9MuwfB23gQpj7rbaWjb49Gg5wdwKWoCvzY8Rg.smethod_2((Selectable)ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ_0.BSdnl9DYm6Rd4cVhJ555c_A);
						});
					}
					else
					{
						ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ_0.gameObject.SetActive(value: true);
					}
					ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ_0.transform.SetAsFirstSibling();
					Class17.DQGZpGELKqeKmXweFYGEcaXR73CqGywHQI6u2TATGL80(global::_003CModule_003E.smethod_28<string>(1161856101u), SceneMan.JFAOKFIDAGK, new Type[3]
					{
						typeof(string),
						typeof(string),
						typeof(string)
					}, JKGKJLLFMLE.CFGKIAPCDLB, JKGKJLLFMLE.IGOBPLOLHEP.machineName, null);
				}
			});
		}
		GameObject pNL = __instance.GetPNL(global::_003CModule_003E.smethod_28<string>(1950943105u));
		for (int num = pNL.transform.childCount - 1; num >= 0; num--)
		{
			if (ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ_0 != null && pNL.transform.GetChild(num).gameObject.name != ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ_0.name)
			{
				pNL.transform.GetChild(num).gameObject.SetActive(value: false);
			}
		}
		if (ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ_0 != null)
		{
			if ((bool)__instance.GetBTN(global::_003CModule_003E.smethod_26<string>(1382744860u)))
			{
				UnityEngine.Object.Destroy(__instance.GetBTN(global::_003CModule_003E.smethod_29<string>(1477353940u)));
				Dictionary<string, GameObject> dictionary = Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<Dictionary<string, GameObject>>(global::_003CModule_003E.smethod_29<string>(1550370693u), __instance);
				dictionary.Remove(global::_003CModule_003E.smethod_27<string>(195246946u));
				Class17.DMoImhPC2jNUt2tgmSezeT_IK_0024XK5Df9PmexG_CaKb1R(global::_003CModule_003E.smethod_25<string>(4121219506u), __instance, dictionary);
			}
			ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ_0.BSdnl9DYm6Rd4cVhJ555c_A.Select();
			Class17.DMoImhPC2jNUt2tgmSezeT_IK_0024XK5Df9PmexG_CaKb1R(global::_003CModule_003E.smethod_27<string>(2945328198u), __instance, 1);
		}
		else
		{
			Class17.DMoImhPC2jNUt2tgmSezeT_IK_0024XK5Df9PmexG_CaKb1R(global::_003CModule_003E.smethod_26<string>(2142999921u), __instance, 0);
		}
		if (IMCFODFJAIJ != null)
		{
			Text text = Class17.DQGZpGELKqeKmXweFYGEcaXR73CqGywHQI6u2TATGL80<SceneMan, Text>(global::_003CModule_003E.smethod_27<string>(1400442154u), __instance, new object[2]
			{
				Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<int>(global::_003CModule_003E.smethod_29<string>(2083165359u), __instance),
				pNL
			});
			Class17.DMoImhPC2jNUt2tgmSezeT_IK_0024XK5Df9PmexG_CaKb1R(global::_003CModule_003E.smethod_28<string>(3954260927u), __instance, Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<int>(global::_003CModule_003E.smethod_26<string>(2142999921u), __instance) + 1);
			text.text = IMCFODFJAIJ;
			text.color = Color.cyan;
		}
		if (KNJKMHJHHNF == string.Empty)
		{
			string[] array = JKGKJLLFMLE.EAPGPJLCFIJ();
			for (int num2 = 0; num2 < array.Length; num2++)
			{
				int startIndex = array[num2].LastIndexOf(global::_003CModule_003E.smethod_27<string>(2681980194u)) + 1;
				string text2 = array[num2].Substring(startIndex);
				bool flag = true;
				if (MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.QOL && string_0 != null)
				{
					flag = text2.ToLower().Contains(string_0.ToLower());
				}
				if (flag && (text2[0] != '_' || !(text2 != global::_003CModule_003E.smethod_25<string>(3831416380u)) || !(text2 != global::_003CModule_003E.smethod_28<string>(2952602016u)) || !(text2 != global::_003CModule_003E.smethod_26<string>(3990547898u))))
				{
					Text text3 = Class17.DQGZpGELKqeKmXweFYGEcaXR73CqGywHQI6u2TATGL80<SceneMan, Text>(global::_003CModule_003E.smethod_27<string>(1400442154u), __instance, new object[2]
					{
						Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<int>(global::_003CModule_003E.smethod_28<string>(3954260927u), __instance),
						pNL
					});
					Class17.DMoImhPC2jNUt2tgmSezeT_IK_0024XK5Df9PmexG_CaKb1R(global::_003CModule_003E.smethod_26<string>(2142999921u), __instance, Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<int>(global::_003CModule_003E.smethod_25<string>(1465449716u), __instance) + 1);
					text3.text = text2;
					if (!(text2 != global::_003CModule_003E.smethod_28<string>(934132245u)))
					{
						text3.color = Color.magenta;
					}
					else
					{
						text3.color = Color.yellow;
					}
					text3.fontStyle = FontStyle.Bold;
				}
			}
		}
		else
		{
			Text text4 = Class17.DQGZpGELKqeKmXweFYGEcaXR73CqGywHQI6u2TATGL80<SceneMan, Text>(global::_003CModule_003E.smethod_28<string>(3590021323u), __instance, new object[2]
			{
				Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<int>(global::_003CModule_003E.smethod_29<string>(2083165359u), __instance),
				pNL
			});
			Class17.DMoImhPC2jNUt2tgmSezeT_IK_0024XK5Df9PmexG_CaKb1R(global::_003CModule_003E.smethod_26<string>(2142999921u), __instance, Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<int>(global::_003CModule_003E.smethod_27<string>(2945328198u), __instance) + 1);
			text4.text = global::_003CModule_003E.smethod_25<string>(1597333287u);
			text4.color = Color.yellow;
		}
		bool flag2 = !JKGKJLLFMLE.IGOBPLOLHEP.isExpert && (KNJKMHJHHNF == global::_003CModule_003E.smethod_29<string>(3576101575u) || KNJKMHJHHNF == global::_003CModule_003E.smethod_28<string>(2952602016u));
		string[] array2 = JKGKJLLFMLE.PALEKLLJNAN(KNJKMHJHHNF);
		for (int num3 = 0; num3 < array2.Length; num3++)
		{
			if (!array2[num3].EndsWith(global::_003CModule_003E.smethod_26<string>(2986582050u)) && (!array2[num3].EndsWith(global::_003CModule_003E.smethod_26<string>(1822232483u)) || !MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.compression))
			{
				continue;
			}
			int num4 = array2[num3].LastIndexOf(global::_003CModule_003E.smethod_28<string>(2001882611u)) + 1;
			string text5 = array2[num3].Substring(num4, array2[num3].Length - num4 - 5);
			bool flag3 = true;
			if (string_0 != null && MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.QOL)
			{
				flag3 = text5.ToLower().Contains(string_0.ToLower());
			}
			if (!flag3 || (flag2 && OFHDENEELDC.APAJLEAEOBL(text5)) || (JKGKJLLFMLE.NCGIGDIPKPI != 2 && OFHDENEELDC.GNGCCILIBDM(text5)))
			{
				continue;
			}
			Text text6 = Class17.DQGZpGELKqeKmXweFYGEcaXR73CqGywHQI6u2TATGL80<SceneMan, Text>(global::_003CModule_003E.smethod_28<string>(3590021323u), __instance, new object[2]
			{
				Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<int>(global::_003CModule_003E.smethod_27<string>(2945328198u), __instance),
				pNL
			});
			Class17.DMoImhPC2jNUt2tgmSezeT_IK_0024XK5Df9PmexG_CaKb1R(global::_003CModule_003E.smethod_29<string>(2083165359u), __instance, Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<int>(global::_003CModule_003E.smethod_27<string>(2945328198u), __instance) + 1);
			text6.text = text5;
			if (!(text5 == HCFHKNOLMGA))
			{
				if (text5 == global::_003CModule_003E.smethod_28<string>(1617155606u))
				{
					text6.color = Color.green;
				}
				else
				{
					text6.color = Color.white;
				}
			}
			else
			{
				text6.color = Color.red;
			}
			if (!array2[num3].EndsWith(global::_003CModule_003E.smethod_27<string>(967689968u)))
			{
				text6.fontStyle = FontStyle.Bold;
			}
			else
			{
				text6.fontStyle = FontStyle.Italic;
			}
		}
		float a = (Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<int>(global::_003CModule_003E.smethod_27<string>(2945328198u), __instance) - 1) / 4 * 58 + 58;
		RectTransform component = pNL.GetComponent<RectTransform>();
		component.sizeDelta = new Vector2(0f, Mathf.Max(a, 720f));
		component.anchoredPosition = Vector2.zero;
		return false;
	}

	internal static Scene smethod_1()
	{
		return SceneManager.GetActiveScene();
	}
}
