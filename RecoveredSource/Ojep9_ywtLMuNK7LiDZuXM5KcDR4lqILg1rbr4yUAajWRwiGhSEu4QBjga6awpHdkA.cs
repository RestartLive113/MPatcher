using System;
using System.Runtime.CompilerServices;
using HarmonyLib;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

[HarmonyPatch("JOJJFMHJAHM")]
[HarmonyPatch(typeof(SliderController))]
internal class Ojep9_ywtLMuNK7LiDZuXM5KcDR4lqILg1rbr4yUAajWRwiGhSEu4QBjga6awpHdkA
{
	[Serializable]
	[CompilerGenerated]
	private sealed class iK6fSwg8tZ_0024emq_vet_0024SmvchXXrHzGuVxhULMEttpB_0024O0ab_w95XoT08n12ZcvfKJ1iE6oG_0024sCqXEWK2AvEHAn0
	{
		public static readonly iK6fSwg8tZ_0024emq_vet_0024SmvchXXrHzGuVxhULMEttpB_0024O0ab_w95XoT08n12ZcvfKJ1iE6oG_0024sCqXEWK2AvEHAn0 _003C_003E9 = new iK6fSwg8tZ_0024emq_vet_0024SmvchXXrHzGuVxhULMEttpB_0024O0ab_w95XoT08n12ZcvfKJ1iE6oG_0024sCqXEWK2AvEHAn0();

		public static UnityAction<string> _003C_003E9__4_0;

		internal void o507Q68kp7hPMErG4QenZRw(string b)
		{
			Class17.DMoImhPC2jNUt2tgmSezeT_IK_0024XK5Df9PmexG_CaKb1R(global::_003CModule_003E.smethod_27<string>(2000907672u), SceneMan.JFAOKFIDAGK, ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ_0.pZEKY5TzLd4S3z2lXESoRnw);
			HOCGCCAIPFF.NDIOFGDJAJO = false;
			smethod_1((UnityEngine.Object)smethod_0((Component)ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ_0));
			smethod_3(smethod_2(), (GameObject)null);
		}

		internal static GameObject smethod_0(Component component_0)
		{
			return component_0.gameObject;
		}

		internal static void smethod_1(UnityEngine.Object object_0)
		{
			UnityEngine.Object.Destroy(object_0);
		}

		internal static EventSystem smethod_2()
		{
			return EventSystem.current;
		}

		internal static void smethod_3(EventSystem eventSystem_0, GameObject gameObject_0)
		{
			eventSystem_0.SetSelectedGameObject(gameObject_0);
		}
	}

	[CompilerGenerated]
	private sealed class Ox2iDlZ3c70_00246RoQSfv0JsVowQjZ_krd273OczjWUNfGOOZl3l8XZqlYBAf7HDbXcTnv32_00244Q0pCTHz_00248eV_0024ztBlsayv507f7P_AhrFyAAyn
	{
		public SliderController wQ6mrkDog7tAEXGul0Y8Sv0;

		internal void IwpAdGgWEUdf_0024eEm_00249pHggM(string val)
		{
			if (smethod_0((UnityEngine.Object)wQ6mrkDog7tAEXGul0Y8Sv0, (UnityEngine.Object)null))
			{
				smethod_2((UnityEngine.Object)smethod_1((Component)ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ_0));
				HOCGCCAIPFF.NDIOFGDJAJO = false;
				smethod_4(smethod_3(), (GameObject)null);
			}
			else if (!smethod_5(val))
			{
				int int_ = int.Parse(val);
				smethod_6(wQ6mrkDog7tAEXGul0Y8Sv0, int_);
			}
		}

		internal void JOszlzxE6wywOEn1PlZz5vE()
		{
			if (smethod_7((UnityEngine.Object)(SceneMan.JFAOKFIDAGK as Build), (UnityEngine.Object)null) && (smethod_0((UnityEngine.Object)wQ6mrkDog7tAEXGul0Y8Sv0, (UnityEngine.Object)null) || Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<FreeCameraController>(global::_003CModule_003E.smethod_29<string>(119146129u), (Build)SceneMan.JFAOKFIDAGK).FMGOKAGJMJH))
			{
				smethod_2((UnityEngine.Object)smethod_1((Component)ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ_0));
				HOCGCCAIPFF.NDIOFGDJAJO = false;
				smethod_4(smethod_3(), (GameObject)null);
			}
		}

		internal static bool smethod_0(UnityEngine.Object object_0, UnityEngine.Object object_1)
		{
			return object_0 == object_1;
		}

		internal static GameObject smethod_1(Component component_0)
		{
			return component_0.gameObject;
		}

		internal static void smethod_2(UnityEngine.Object object_0)
		{
			UnityEngine.Object.Destroy(object_0);
		}

		internal static EventSystem smethod_3()
		{
			return EventSystem.current;
		}

		internal static void smethod_4(EventSystem eventSystem_0, GameObject gameObject_0)
		{
			eventSystem_0.SetSelectedGameObject(gameObject_0);
		}

		internal static bool smethod_5(string string_0)
		{
			return string.IsNullOrEmpty(string_0);
		}

		internal static void smethod_6(SliderController sliderController_0, int int_0)
		{
			sliderController_0.Set(int_0);
		}

		internal static bool smethod_7(UnityEngine.Object object_0, UnityEngine.Object object_1)
		{
			return object_0 != object_1;
		}
	}

	private static readonly float float_0 = 0.5f;

	private static float float_1 = 0f;

	private static int Z3wcERBZopqgiEV5jwdyXBc = 1;

	private static ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ_0;

	[HarmonyPrefix]
	internal static void smethod_0(SliderController __instance)
	{
		if (smethod_1() - float_1 > float_0)
		{
			Z3wcERBZopqgiEV5jwdyXBc = 1;
		}
		else
		{
			Z3wcERBZopqgiEV5jwdyXBc++;
		}
		float_1 = smethod_1();
		if (Z3wcERBZopqgiEV5jwdyXBc != 2)
		{
			return;
		}
		Z3wcERBZopqgiEV5jwdyXBc = 0;
		if (smethod_2((UnityEngine.Object)ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ_0, (UnityEngine.Object)null))
		{
			smethod_4((UnityEngine.Object)smethod_3((Component)ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ_0));
		}
		ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ_0 = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.bqYYMQnP2SDqYH85wmN_0024evI(global::_003CModule_003E.smethod_29<string>(890069510u), Vector3.zero, "", "", smethod_6(smethod_5(global::_003CModule_003E.smethod_25<string>(806627754u))));
		smethod_9(smethod_7((Component)ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ_0), smethod_8(smethod_7((Component)__instance)));
		smethod_10(ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ_0.BSdnl9DYm6Rd4cVhJ555c_A, InputField.CharacterValidation.Integer);
		HOCGCCAIPFF.NDIOFGDJAJO = true;
		ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ_0.TJgoS_qAksEywwB0VyKhSGw();
		smethod_12(smethod_11(), smethod_3((Component)ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ_0));
		smethod_13(ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ_0.BSdnl9DYm6Rd4cVhJ555c_A).AddListener(delegate
		{
			Class17.DMoImhPC2jNUt2tgmSezeT_IK_0024XK5Df9PmexG_CaKb1R(global::_003CModule_003E.smethod_27<string>(2000907672u), SceneMan.JFAOKFIDAGK, ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ_0.pZEKY5TzLd4S3z2lXESoRnw);
			HOCGCCAIPFF.NDIOFGDJAJO = false;
			iK6fSwg8tZ_0024emq_vet_0024SmvchXXrHzGuVxhULMEttpB_0024O0ab_w95XoT08n12ZcvfKJ1iE6oG_0024sCqXEWK2AvEHAn0.smethod_1((UnityEngine.Object)iK6fSwg8tZ_0024emq_vet_0024SmvchXXrHzGuVxhULMEttpB_0024O0ab_w95XoT08n12ZcvfKJ1iE6oG_0024sCqXEWK2AvEHAn0.smethod_0((Component)ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ_0));
			iK6fSwg8tZ_0024emq_vet_0024SmvchXXrHzGuVxhULMEttpB_0024O0ab_w95XoT08n12ZcvfKJ1iE6oG_0024sCqXEWK2AvEHAn0.smethod_3(iK6fSwg8tZ_0024emq_vet_0024SmvchXXrHzGuVxhULMEttpB_0024O0ab_w95XoT08n12ZcvfKJ1iE6oG_0024sCqXEWK2AvEHAn0.smethod_2(), (GameObject)null);
		});
		ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ_0.JNMaMdWdD3fzh8iVBUwSGz4 = delegate(string val)
		{
			if (Ox2iDlZ3c70_00246RoQSfv0JsVowQjZ_krd273OczjWUNfGOOZl3l8XZqlYBAf7HDbXcTnv32_00244Q0pCTHz_00248eV_0024ztBlsayv507f7P_AhrFyAAyn.smethod_0((UnityEngine.Object)__instance, (UnityEngine.Object)null))
			{
				Ox2iDlZ3c70_00246RoQSfv0JsVowQjZ_krd273OczjWUNfGOOZl3l8XZqlYBAf7HDbXcTnv32_00244Q0pCTHz_00248eV_0024ztBlsayv507f7P_AhrFyAAyn.smethod_2((UnityEngine.Object)Ox2iDlZ3c70_00246RoQSfv0JsVowQjZ_krd273OczjWUNfGOOZl3l8XZqlYBAf7HDbXcTnv32_00244Q0pCTHz_00248eV_0024ztBlsayv507f7P_AhrFyAAyn.smethod_1((Component)ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ_0));
				HOCGCCAIPFF.NDIOFGDJAJO = false;
				Ox2iDlZ3c70_00246RoQSfv0JsVowQjZ_krd273OczjWUNfGOOZl3l8XZqlYBAf7HDbXcTnv32_00244Q0pCTHz_00248eV_0024ztBlsayv507f7P_AhrFyAAyn.smethod_4(Ox2iDlZ3c70_00246RoQSfv0JsVowQjZ_krd273OczjWUNfGOOZl3l8XZqlYBAf7HDbXcTnv32_00244Q0pCTHz_00248eV_0024ztBlsayv507f7P_AhrFyAAyn.smethod_3(), (GameObject)null);
			}
			else if (!Ox2iDlZ3c70_00246RoQSfv0JsVowQjZ_krd273OczjWUNfGOOZl3l8XZqlYBAf7HDbXcTnv32_00244Q0pCTHz_00248eV_0024ztBlsayv507f7P_AhrFyAAyn.smethod_5(val))
			{
				int int_ = int.Parse(val);
				Ox2iDlZ3c70_00246RoQSfv0JsVowQjZ_krd273OczjWUNfGOOZl3l8XZqlYBAf7HDbXcTnv32_00244Q0pCTHz_00248eV_0024ztBlsayv507f7P_AhrFyAAyn.smethod_6(__instance, int_);
			}
		};
		ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ_0.WKq1TUYmKJJXRZQEHbwVXPg = delegate
		{
			if (Ox2iDlZ3c70_00246RoQSfv0JsVowQjZ_krd273OczjWUNfGOOZl3l8XZqlYBAf7HDbXcTnv32_00244Q0pCTHz_00248eV_0024ztBlsayv507f7P_AhrFyAAyn.smethod_7((UnityEngine.Object)(SceneMan.JFAOKFIDAGK as Build), (UnityEngine.Object)null) && (Ox2iDlZ3c70_00246RoQSfv0JsVowQjZ_krd273OczjWUNfGOOZl3l8XZqlYBAf7HDbXcTnv32_00244Q0pCTHz_00248eV_0024ztBlsayv507f7P_AhrFyAAyn.smethod_0((UnityEngine.Object)__instance, (UnityEngine.Object)null) || Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<FreeCameraController>(global::_003CModule_003E.smethod_29<string>(119146129u), (Build)SceneMan.JFAOKFIDAGK).FMGOKAGJMJH))
			{
				Ox2iDlZ3c70_00246RoQSfv0JsVowQjZ_krd273OczjWUNfGOOZl3l8XZqlYBAf7HDbXcTnv32_00244Q0pCTHz_00248eV_0024ztBlsayv507f7P_AhrFyAAyn.smethod_2((UnityEngine.Object)Ox2iDlZ3c70_00246RoQSfv0JsVowQjZ_krd273OczjWUNfGOOZl3l8XZqlYBAf7HDbXcTnv32_00244Q0pCTHz_00248eV_0024ztBlsayv507f7P_AhrFyAAyn.smethod_1((Component)ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ_0));
				HOCGCCAIPFF.NDIOFGDJAJO = false;
				Ox2iDlZ3c70_00246RoQSfv0JsVowQjZ_krd273OczjWUNfGOOZl3l8XZqlYBAf7HDbXcTnv32_00244Q0pCTHz_00248eV_0024ztBlsayv507f7P_AhrFyAAyn.smethod_4(Ox2iDlZ3c70_00246RoQSfv0JsVowQjZ_krd273OczjWUNfGOOZl3l8XZqlYBAf7HDbXcTnv32_00244Q0pCTHz_00248eV_0024ztBlsayv507f7P_AhrFyAAyn.smethod_3(), (GameObject)null);
			}
		};
	}

	internal static float smethod_1()
	{
		return Time.unscaledTime;
	}

	internal static bool smethod_2(UnityEngine.Object object_0, UnityEngine.Object object_1)
	{
		return object_0 != object_1;
	}

	internal static GameObject smethod_3(Component component_0)
	{
		return component_0.gameObject;
	}

	internal static void smethod_4(UnityEngine.Object object_0)
	{
		UnityEngine.Object.Destroy(object_0);
	}

	internal static GameObject smethod_5(string string_0)
	{
		return GameObject.Find(string_0);
	}

	internal static Transform smethod_6(GameObject gameObject_0)
	{
		return gameObject_0.transform;
	}

	internal static Transform smethod_7(Component component_0)
	{
		return component_0.transform;
	}

	internal static Vector3 smethod_8(Transform transform_0)
	{
		return transform_0.position;
	}

	internal static void smethod_9(Transform transform_0, Vector3 vector3_0)
	{
		transform_0.position = vector3_0;
	}

	internal static void smethod_10(InputField inputField_0, InputField.CharacterValidation characterValidation_0)
	{
		inputField_0.characterValidation = characterValidation_0;
	}

	internal static EventSystem smethod_11()
	{
		return EventSystem.current;
	}

	internal static void smethod_12(EventSystem eventSystem_0, GameObject gameObject_0)
	{
		eventSystem_0.SetSelectedGameObject(gameObject_0);
	}

	internal static InputField.SubmitEvent smethod_13(InputField inputField_0)
	{
		return inputField_0.onEndEdit;
	}
}
