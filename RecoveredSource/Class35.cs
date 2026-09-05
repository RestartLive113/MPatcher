using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using HarmonyLib;
using MPatchrMain;
using UnityEngine;
using UnityEngine.UI;
using VRGIN.Core;
using VRGIN.Visuals;

[HarmonyPatch(typeof(Option))]
[HarmonyPatch(new Type[] { })]
[HarmonyPatch("Start")]
internal class Class35 : Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA
{
	[HarmonyPatch(typeof(Option))]
	[HarmonyPatch("OnSelect")]
	internal class __0024lja4xRbfBw6CfK30uhCTtos5zGXRk5CFdnwZBy8xztX3Y5UitBns7IuP1vkcm1_vY_0024tW4z4tkFBjkipasoRTWv3so_0024tMi_0024_0024OFn9kN_0024SVUa
	{
		internal static bool smethod_0(string DPGKEOAGONA, GameObject NGLBLAGMBLN)
		{
			string string_ = smethod_1(NGLBLAGMBLN.GetComponent<ListController>());
			mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(smethod_2(global::_003CModule_003E.smethod_27<string>(2738730063u), DPGKEOAGONA, global::_003CModule_003E.smethod_29<string>(929710696u), string_), bool_0: true);
			return true;
		}

		internal static string smethod_1(ListController listController_0)
		{
			return listController_0.GetSelectedItem();
		}

		internal static string smethod_2(string string_0, string string_1, string string_2, string string_3)
		{
			return string_0 + string_1 + string_2 + string_3;
		}

		internal static bool smethod_3(string string_0, string string_1)
		{
			return string_0 == string_1;
		}

		internal static void smethod_4(InputField inputField_0, string string_0)
		{
			inputField_0.text = string_0;
		}

		internal static string smethod_5(string string_0)
		{
			return string_0.ToUpper();
		}
	}

	[Serializable]
	[CompilerGenerated]
	private sealed class Class37
	{
		public static readonly Class37 _003C_003E9 = new Class37();

		public static Action _003C_003E9__28_0;

		public static Action _003C_003E9__28_1;

		public static Action _003C_003E9__28_3;

		public static Action _003C_003E9__28_4;

		public static Action _003C_003E9__28_6;

		public static Action _003C_003E9__28_7;

		public static Action<lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw> _003C_003E9__29_0;

		public static Action<lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw> _003C_003E9__29_1;

		public static Action<lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw> _003C_003E9__29_2;

		public static Action<bool> _003C_003E9__29_3;

		public static Action _003C_003E9__29_20;

		public static Action<bool> _003C_003E9__29_6;

		public static Action<bool> _003C_003E9__29_8;

		public static Action<bool> _003C_003E9__29_9;

		public static Action<bool> _003C_003E9__29_10;

		public static Action<bool> _003C_003E9__29_11;

		public static Action<bool> _003C_003E9__29_12;

		public static Action<bool> _003C_003E9__29_13;

		public static Action<bool> _003C_003E9__29_14;

		public static Action<bool> _003C_003E9__29_15;

		public static Action<AmMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F> _003C_003E9__29_16;

		internal void YDJxyH1XIM4IoCwQ4v_0024L_GedZIm4YKEG0bxDlWzaXmLP()
		{
			MPatchr.ShowDebugMsg(global::_003CModule_003E.smethod_29<string>(2654644942u));
		}

		internal void YbmpusSiC3KhVwg5kGQiXu9cnWBbCoE8HnmTKIAoJdWj()
		{
			MPatchr.ShowDebugMsg(global::_003CModule_003E.smethod_26<string>(2893919704u));
		}

		internal void Y_0024RnTdLlSmKt9f4EYKudXdn449ogYpfA63kBX9bziwDA()
		{
			MPatchr.ShowDebugMsg(global::_003CModule_003E.smethod_27<string>(4036986621u));
		}

		internal void ZDUOsVW_0024J25BSK2sauKvjIv4t5AsHLyZ_0024hYstLbmznZ5()
		{
			MPatchr.ShowDebugMsg(global::_003CModule_003E.smethod_28<string>(3062667248u));
		}

		internal void Zk6rAtw2jTk_0024_2luWmXQfxycmywZrqEZY2mikRASxeqN()
		{
			MPatchr.ShowDebugMsg(global::_003CModule_003E.smethod_25<string>(1178465625u));
		}

		internal void Z91pEI98irpUTnxa7m8QYO3AVIn_00243RhZ1wMz4aXYURDG()
		{
			MPatchr.ShowDebugMsg(global::_003CModule_003E.smethod_26<string>(2429350413u));
		}

		internal void method_0(lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw button)
		{
			smethod_0(global::_003CModule_003E.smethod_28<string>(1347532871u));
		}

		internal void method_1(lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw button)
		{
			smethod_0(global::_003CModule_003E.smethod_25<string>(4203850144u));
		}

		internal void method_2(lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw button)
		{
			smethod_0(global::_003CModule_003E.smethod_25<string>(2365417764u));
		}

		internal void method_3(bool toggled)
		{
			MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.jNJFoLQ_wY8hPL4TF_0024pIwMo(toggled ? 50 : (-1));
		}

		internal void tT6AZAe8kszbTswXebbBNF0ogyF7K1fidIQHK3KmsJBY()
		{
			if (smethod_1(global::_003CModule_003E.smethod_25<string>(2417726564u)))
			{
				smethod_2(global::_003CModule_003E.smethod_27<string>(3896380086u));
			}
			string text = global::_003CModule_003E.smethod_29<string>(2004422723u);
			File.Move(text, global::_003CModule_003E.smethod_25<string>(865445550u) + smethod_4(smethod_3()) + global::_003CModule_003E.smethod_25<string>(3495179356u));
			File.Move(global::_003CModule_003E.smethod_29<string>(3356702250u), text);
			MPatchr.xcBvxcM_0024ckBeZyvdSoAkJoM.bL_IIv_OFtIrJGjLZXq12K8(2, bool_1: true);
			Application.Quit();
		}

		internal void method_4(bool toggled)
		{
			MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.showUpdateNotif = toggled;
			MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.UUiRNMwxRbfk_Fs4cDErRoM();
		}

		internal void method_5(bool tgld)
		{
			MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.vr_headset = 0;
			MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.UUiRNMwxRbfk_Fs4cDErRoM();
		}

		internal void method_6(bool tgld)
		{
			MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.vr_headset = 1;
			MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.UUiRNMwxRbfk_Fs4cDErRoM();
		}

		internal void ssV_0024KU5IW6hIzau0oYK8INcyaFxNTdyNIFFqOYS9_pnI(bool tgld)
		{
			MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.vr_headset = 2;
			MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.UUiRNMwxRbfk_Fs4cDErRoM();
		}

		internal void swH2EPKixiM2qM250t2ggMkQTN1fUNo1Te_0024YQlDhJ83A(bool tgld)
		{
			MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.vr_headset = -1;
			MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.UUiRNMwxRbfk_Fs4cDErRoM();
		}

		internal void tHjBTplUDlnPxSoO6X3RtEf7twvTpFve19_0024HLirTHdSm(bool toggled)
		{
			MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.vr_curvedScreen = toggled;
			if (smethod_5((UnityEngine.Object)KEjZwYcSzxf2DUM4tsmq7LTU5Rz8bWNX1Ud16EuZVShS.d_RafUgoVPViHGf69VfQ7eM(), (UnityEngine.Object)null))
			{
				smethod_8(smethod_7(smethod_6(KEjZwYcSzxf2DUM4tsmq7LTU5Rz8bWNX1Ud16EuZVShS.d_RafUgoVPViHGf69VfQ7eM())), MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.vr_curvedScreen ? GUIMonitor.CurvinessState.Curved : GUIMonitor.CurvinessState.Flat);
			}
		}

		internal void tXFk5FoXLoAOyEKBJRqmcmQZryP6IZk4D_0024kdXcq7yIaP(bool toggled)
		{
			MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.vr_camOffset = toggled;
			MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.UUiRNMwxRbfk_Fs4cDErRoM();
		}

		internal void thn_002413dEmPRVL7kJTXWev2y2we2rHZEvRiihLompgVCl(bool toggled)
		{
			MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.vr_lockMouse = toggled;
			MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.UUiRNMwxRbfk_Fs4cDErRoM();
		}

		internal void tyWG5s2foXJhYVHBYnRhPHTSe4q19p0YPzdT6I3W50Dm(bool toggled)
		{
			MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.vr_gameRendDist = toggled;
			MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.UUiRNMwxRbfk_Fs4cDErRoM();
		}

		internal void uCQr6RbyBJcno98Uk0wgAZU2rM2YaPR9pYafipEauG0H(AmMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F me)
		{
			if (smethod_10(smethod_9(dtWh6TqzU6x_Ry1bMWDXo0E), me.hpiqzm2jQTswCo32f7jvrQ4<string>(global::_003CModule_003E.smethod_28<string>(2227976190u))))
			{
				me.clf0br4v0HxmaJWIcm_0024_0024GUg(global::_003CModule_003E.smethod_28<string>(2227976190u), smethod_9(dtWh6TqzU6x_Ry1bMWDXo0E));
				MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.updateChannel = (settingsIngame.updateChannels)dtWh6TqzU6x_Ry1bMWDXo0E.EeNYggqZWtiar0rSt8jVD1eSfb4J4GSFkxD2kxLGahLU();
				if (MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.updateChannel == settingsIngame.updateChannels.stable)
				{
					smethod_11(ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ_0.BSdnl9DYm6Rd4cVhJ555c_A, "");
				}
				else
				{
					smethod_11(ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ_0.BSdnl9DYm6Rd4cVhJ555c_A, smethod_12(MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.updateChannel.ToString()));
				}
			}
		}

		internal static void smethod_0(string string_0)
		{
			Application.OpenURL(string_0);
		}

		internal static bool smethod_1(string string_0)
		{
			return File.Exists(string_0);
		}

		internal static void smethod_2(string string_0)
		{
			File.Delete(string_0);
		}

		internal static Process smethod_3()
		{
			return Process.GetCurrentProcess();
		}

		internal static int smethod_4(Process process_0)
		{
			return process_0.Id;
		}

		internal static bool smethod_5(UnityEngine.Object object_0, UnityEngine.Object object_1)
		{
			return object_0 != object_1;
		}

		internal static IVRManagerContext smethod_6(VRManager vrmanager_0)
		{
			return vrmanager_0.Context;
		}

		internal static VRSettings smethod_7(IVRManagerContext ivrmanagerContext_0)
		{
			return ivrmanagerContext_0.Settings;
		}

		internal static void smethod_8(VRSettings vrsettings_0, GUIMonitor.CurvinessState curvinessState_0)
		{
			vrsettings_0.Projection = curvinessState_0;
		}

		internal static string smethod_9(ListController listController_0)
		{
			return listController_0.GetSelectedItem();
		}

		internal static bool smethod_10(string string_0, string string_1)
		{
			return string_0 != string_1;
		}

		internal static void smethod_11(InputField inputField_0, string string_0)
		{
			inputField_0.text = string_0;
		}

		internal static string smethod_12(string string_0)
		{
			return string_0.ToUpper();
		}
	}

	[CompilerGenerated]
	private sealed class Class38
	{
		public Action<bool> g5ivPA06YWgY4EpXr0G2x10;

		public bool a_6oXGVfXUqln5sS28zkBKg;

		internal void soDuGG6f_MyH4Fqnf_vMhHlnVc1DIoxP6NtWPV73RGoA(bool toggled)
		{
			g5ivPA06YWgY4EpXr0G2x10(toggled);
			if (a_6oXGVfXUqln5sS28zkBKg)
			{
				a8zL_jLlMzZ_3qoyYFbFj64(toggled);
			}
		}
	}

	[CompilerGenerated]
	private sealed class Class39
	{
		public Ox2iDlZ3c70_00246RoQSfv0JsUA9Zr8fowUBRLdiyCjXAIPYXycTbHLX39SoZBma4Hq2A rZWNRvB73KHFd2kQTExe3U0;

		internal void AawUxQY9353b2A5OGE4l0yUxT4S4XBWuEI_0024QUzpfxB1W(float prog)
		{
			rZWNRvB73KHFd2kQTExe3U0.method_1(prog * 100f);
		}
	}

	[CompilerGenerated]
	private sealed class Hn0l0GNFrsPTtJLnYPdC5kstSoR0QDTE9ryF4MZWxnfvyPILHvg2xgflP78f6237KLozQUyL_0024rkMHW57LTXcv7a9PYzi9PlA1Haf6t_0024yjosu
	{
		public Ox2iDlZ3c70_00246RoQSfv0JsUA9Zr8fowUBRLdiyCjXAIPYXycTbHLX39SoZBma4Hq2A zi7W21I_0024y4peEBI96i4vh28;

		public lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw s91jRzLMVbxNDONSilDMbWQ;

		public GameObject EtKYDMu4poqOB22SlzMLERHGSjWcebefrsf6_0024uAAk0da;

		public GameObject HY_w_AyUHHp9hrJckZ7e3B0;

		public GameObject rYYTC4OVAoi1HNKYSdRxdwE;

		public GameObject F6axQ9D1ECsGlTewUCPLil8;

		public GameObject gameObject_0;

		public Action<float> action_0;

		internal void BzbFh_0024tmotEAiJcyQy15gbc(lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw button)
		{
			IK5FoqU27QNYKBuS9GCG4jKsQbgT_pRUAmN209UXpaiq8GRIlOaVW6_QHuvhHLx718BRBxGV9203TIg3Ubyefe3_0024TiklknV8YoBfkquf0ePS iK5FoqU27QNYKBuS9GCG4jKsQbgT_pRUAmN209UXpaiq8GRIlOaVW6_QHuvhHLx718BRBxGV9203TIg3Ubyefe3_0024TiklknV8YoBfkquf0ePS = new IK5FoqU27QNYKBuS9GCG4jKsQbgT_pRUAmN209UXpaiq8GRIlOaVW6_QHuvhHLx718BRBxGV9203TIg3Ubyefe3_0024TiklknV8YoBfkquf0ePS
			{
				g0mQ_0024tSzvWAIzvC2JeS9zbk = this
			};
			if (!smethod_0(KeyCode.LeftShift) && !smethod_0(KeyCode.RightShift))
			{
				s91jRzLMVbxNDONSilDMbWQ.FLSdXom6uNTfN55f5nxTsH8 = false;
				smethod_2(smethod_1((Component)ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ_0), bool_0: false);
				iK5FoqU27QNYKBuS9GCG4jKsQbgT_pRUAmN209UXpaiq8GRIlOaVW6_QHuvhHLx718BRBxGV9203TIg3Ubyefe3_0024TiklknV8YoBfkquf0ePS.S_0024cB_L_0024t0_xRxsSjJS4RjlQ = ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ_0.pZEKY5TzLd4S3z2lXESoRnw;
				smethod_2(smethod_1((Component)zi7W21I_0024y4peEBI96i4vh28), bool_0: true);
				if (!smethod_3(global::_003CModule_003E.smethod_25<string>(4000329416u)))
				{
					smethod_4(global::_003CModule_003E.smethod_29<string>(2020429971u));
				}
				MPatchr.NNbVj5nqStzgkt0zSfIM_qs(smethod_5(global::_003CModule_003E.smethod_25<string>(692246660u), iK5FoqU27QNYKBuS9GCG4jKsQbgT_pRUAmN209UXpaiq8GRIlOaVW6_QHuvhHLx718BRBxGV9203TIg3Ubyefe3_0024TiklknV8YoBfkquf0ePS.S_0024cB_L_0024t0_xRxsSjJS4RjlQ, global::_003CModule_003E.smethod_26<string>(3112548823u)), global::_003CModule_003E.smethod_29<string>(3356702250u), bool_1: false, Class37._003C_003E9.tT6AZAe8kszbTswXebbBNF0ogyF7K1fidIQHK3KmsJBY, iK5FoqU27QNYKBuS9GCG4jKsQbgT_pRUAmN209UXpaiq8GRIlOaVW6_QHuvhHLx718BRBxGV9203TIg3Ubyefe3_0024TiklknV8YoBfkquf0ePS.QLsSHfAPERxCiN0BRIDH7eo, delegate(float progress)
				{
					zi7W21I_0024y4peEBI96i4vh28.method_1(progress * 100f);
					zi7W21I_0024y4peEBI96i4vh28.Uyxr04ltGE_xJflB_0024UyNu8w((int)(progress * 100f) + global::_003CModule_003E.smethod_28<string>(2000104127u));
				});
			}
			else
			{
				smethod_2(smethod_1((Component)ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ_0), bool_0: true);
				smethod_2(smethod_1((Component)zi7W21I_0024y4peEBI96i4vh28), bool_0: false);
			}
		}

		internal void QW6hQqto6zDbXZnK1L8mRtE(float progress)
		{
			zi7W21I_0024y4peEBI96i4vh28.method_1(progress * 100f);
			zi7W21I_0024y4peEBI96i4vh28.Uyxr04ltGE_xJflB_0024UyNu8w((int)(progress * 100f) + global::_003CModule_003E.smethod_28<string>(2000104127u));
		}

		internal void CE_0024fb8Vs_0024NbV5dIygtcVK9M(lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw button)
		{
			smethod_2(EtKYDMu4poqOB22SlzMLERHGSjWcebefrsf6_0024uAAk0da, bool_0: false);
			smethod_2(HY_w_AyUHHp9hrJckZ7e3B0, bool_0: true);
		}

		internal void Cu5yHL2zRFxfY8gCMaYkaoU(lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw button)
		{
			smethod_2(EtKYDMu4poqOB22SlzMLERHGSjWcebefrsf6_0024uAAk0da, bool_0: false);
			smethod_2(rYYTC4OVAoi1HNKYSdRxdwE, bool_0: true);
		}

		internal void Q8IRIYh1xdDNYZLo3rk05vw(bool toggled)
		{
			LogSettingsUi("toggle callback entered: " + toggled);
			try
			{
				if (!toggled)
				{
					MPatcherFork.CustomPatches.PatchSettingsPage.Close();
				}
				smethod_7(smethod_6(global::_003CModule_003E.smethod_26<string>(3103631283u)));
				smethod_8(SceneMan.JFAOKFIDAGK, global::_003CModule_003E.smethod_25<string>(2885014434u), bool_0: false);
				smethod_8(SceneMan.JFAOKFIDAGK, global::_003CModule_003E.smethod_29<string>(786601733u), bool_0: false);
				smethod_8(SceneMan.JFAOKFIDAGK, global::_003CModule_003E.smethod_26<string>(1948199256u), bool_0: false);
				smethod_8(SceneMan.JFAOKFIDAGK, global::_003CModule_003E.smethod_26<string>(1198255644u), bool_0: false);
				smethod_2(smethod_10(smethod_9(SceneMan.JFAOKFIDAGK, global::_003CModule_003E.smethod_25<string>(2621247292u))), bool_0: false);
				smethod_2(smethod_10(smethod_9(SceneMan.JFAOKFIDAGK, global::_003CModule_003E.smethod_29<string>(4146355021u))), bool_0: false);
				smethod_2(EtKYDMu4poqOB22SlzMLERHGSjWcebefrsf6_0024uAAk0da, toggled);
				smethod_2(F6axQ9D1ECsGlTewUCPLil8, toggled);
				smethod_2(rYYTC4OVAoi1HNKYSdRxdwE, bool_0: false);
				smethod_2(HY_w_AyUHHp9hrJckZ7e3B0, bool_0: false);
				smethod_2(gameObject_0, toggled);
				LogSettingsUi("toggle callback completed: " + toggled);
			}
			catch (Exception ex)
			{
				LogSettingsUi("toggle callback failed: " + ex);
				UnityEngine.Debug.LogException(ex);
				throw;
			}
		}

		internal void RJiw4N9LGUVBzWXJaevLL_Y()
		{
			LogSettingsUi("close callback entered");
			MPatcherFork.CustomPatches.PatchSettingsPage.Close();
			smethod_2(EtKYDMu4poqOB22SlzMLERHGSjWcebefrsf6_0024uAAk0da, bool_0: false);
			smethod_2(F6axQ9D1ECsGlTewUCPLil8, bool_0: false);
			smethod_2(rYYTC4OVAoi1HNKYSdRxdwE, bool_0: false);
			smethod_2(gameObject_0, bool_0: false);
			smethod_2(HY_w_AyUHHp9hrJckZ7e3B0, bool_0: false);
		}

		internal static bool smethod_0(KeyCode keyCode_0)
		{
			return Input.GetKey(keyCode_0);
		}

		internal static GameObject smethod_1(Component component_0)
		{
			return component_0.gameObject;
		}

		internal static void smethod_2(GameObject gameObject_1, bool bool_0)
		{
			gameObject_1.SetActive(bool_0);
		}

		internal static bool smethod_3(string string_0)
		{
			return Directory.Exists(string_0);
		}

		internal static DirectoryInfo smethod_4(string string_0)
		{
			return Directory.CreateDirectory(string_0);
		}

		internal static string smethod_5(string string_0, string string_1, string string_2)
		{
			return string_0 + string_1 + string_2;
		}

		internal static GameObject smethod_6(string string_0)
		{
			return GameObject.Find(string_0);
		}

		internal static Transform smethod_7(GameObject gameObject_1)
		{
			return gameObject_1.transform;
		}

		internal static void smethod_8(SceneMan sceneMan_0, string string_0, bool bool_0)
		{
			sceneMan_0.ValidatePNL(string_0, bool_0);
		}

		internal static GameObject smethod_9(SceneMan sceneMan_0, string string_0)
		{
			return sceneMan_0.GetGRP(string_0);
		}

		internal static GameObject smethod_10(GameObject gameObject_1)
		{
			return gameObject_1.gameObject;
		}
	}

	[CompilerGenerated]
	private sealed class H2eRtg3j8LQbRFA5W4lA82a14657bmCSdMuY9EVVSHvtJYr8OhIdXfJVxfCG4cOMphTbZa6DsKCLU0NrUKx5g9PynCpA8ksxLH7V7dDbSeo0
	{
		public int tws1nQARWc_py1S24is5aQY;

		internal void RR3TwAwFZhij3o7cJTyFWGw(bool toggled)
		{
			int_0 = tws1nQARWc_py1S24is5aQY;
			URHB7oHimfLtN31uHHTfkH6leCSDr7GxS9wOPypJM2F_();
		}
	}

	[CompilerGenerated]
	private sealed class IK5FoqU27QNYKBuS9GCG4jKsQbgT_pRUAmN209UXpaiq8GRIlOaVW6_QHuvhHLx718BRBxGV9203TIg3Ubyefe3_0024TiklknV8YoBfkquf0ePS
	{
		public string S_0024cB_L_0024t0_xRxsSjJS4RjlQ;

		public Hn0l0GNFrsPTtJLnYPdC5kstSoR0QDTE9ryF4MZWxnfvyPILHvg2xgflP78f6237KLozQUyL_0024rkMHW57LTXcv7a9PYzi9PlA1Haf6t_0024yjosu g0mQ_0024tSzvWAIzvC2JeS9zbk;

		internal void QLsSHfAPERxCiN0BRIDH7eo()
		{
			if (smethod_0(global::_003CModule_003E.smethod_28<string>(2379495680u)))
			{
				smethod_1(global::_003CModule_003E.smethod_26<string>(1989862790u));
			}
			g0mQ_0024tSzvWAIzvC2JeS9zbk.zi7W21I_0024y4peEBI96i4vh28.Uyxr04ltGE_xJflB_0024UyNu8w(global::_003CModule_003E.smethod_27<string>(3567077529u));
			g0mQ_0024tSzvWAIzvC2JeS9zbk.s91jRzLMVbxNDONSilDMbWQ.FLSdXom6uNTfN55f5nxTsH8 = true;
			MPatchr.ShowDebugMsg(smethod_3(global::_003CModule_003E.smethod_25<string>(650931341u), smethod_2(S_0024cB_L_0024t0_xRxsSjJS4RjlQ) ? "" : global::_003CModule_003E.smethod_29<string>(457199144u)));
		}

		internal static bool smethod_0(string string_0)
		{
			return File.Exists(string_0);
		}

		internal static void smethod_1(string string_0)
		{
			File.Delete(string_0);
		}

		internal static bool smethod_2(string string_0)
		{
			return string.IsNullOrEmpty(string_0);
		}

		internal static string smethod_3(string string_0, string string_1)
		{
			return string_0 + string_1;
		}
	}

	internal static ListController listController_0;

	internal static ListController WM8LkAhdj7QtGX92nWcmHq4;

	internal static ListController dtWh6TqzU6x_Ry1bMWDXo0E;

	internal static ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ_0;

	internal static readonly string[] uGQUy_0024Mw_q46atKOrYCeWos = new string[7]
	{
		global::_003CModule_003E.smethod_27<string>(2506076688u),
		global::_003CModule_003E.smethod_26<string>(1646243069u),
		global::_003CModule_003E.smethod_25<string>(3933772637u),
		global::_003CModule_003E.smethod_25<string>(1435922402u),
		global::_003CModule_003E.smethod_26<string>(3612511231u),
		global::_003CModule_003E.smethod_27<string>(1533534855u),
		global::_003CModule_003E.smethod_26<string>(844324474u)
	};

	internal static readonly string[] string_0 = new string[2]
	{
		global::_003CModule_003E.smethod_26<string>(2489825198u),
		global::_003CModule_003E.smethod_29<string>(3390010350u)
	};

	private static readonly string[] pbzR7S54NBhTZR8oT99MTeSRARcwBRQpjaybDXm_0024t6qA = new string[3]
	{
		global::_003CModule_003E.smethod_29<string>(3249353661u),
		global::_003CModule_003E.smethod_27<string>(2090719360u),
		global::_003CModule_003E.smethod_26<string>(3880468210u)
	};

	private static readonly string[] tgTJhpFjjWkKov3zqTwQ0KquLxdaZcnuZ_0024D9KVqvLQPo = new string[3]
	{
		global::_003CModule_003E.smethod_29<string>(4245053184u),
		global::_003CModule_003E.smethod_26<string>(125732947u),
		global::_003CModule_003E.smethod_29<string>(274583929u)
	};

	internal static int oXDDyITkEuj8nDCyvcjtZDQ;

	internal static int om7X2R_aOZ08nmTyKqLRdgs;

	internal static int int_0 = 0;

	internal static List<Transform> list_0 = new List<Transform>();

	internal static List<Control0> JqKDtyiFnJcdoNFYMikviGo = new List<Control0>();

	internal static readonly int AAQQTSoA9S5pIq8vyt_0Jn0 = -125;

	internal static readonly int AfZDs_0024IG7ovd1i5_0024f_00244KVwk = 245;

	internal static readonly int int_1 = 250;

	internal static readonly int int_2 = -35;

	internal static readonly int yE0WRQCDuxkVK37TLNJxGY4 = 2;

	internal static readonly Vector2 vector2_0 = new Vector2(530f, 555f);

	internal static string[] SAf0_OYikVNNjzcbx_HtZZP6NQa_0024naD53sU2J3O0XKHk
	{
		get
		{
			if (!HelpDefs.isJ)
			{
				return pbzR7S54NBhTZR8oT99MTeSRARcwBRQpjaybDXm_0024t6qA;
			}
			return tgTJhpFjjWkKov3zqTwQ0KquLxdaZcnuZ_0024D9KVqvLQPo;
		}
	}

	internal static void a8zL_jLlMzZ_3qoyYFbFj64(bool toggled, bool showMsg = true)
	{
		if (toggled)
		{
			metG733wDBlDhjB0MSUi_0024WZtiv2d5Ta15QUy66BHDFij5pgSnnkUUekfxHo4GGqdng.Aab6uSDccQw2pntTBaGy7HzuanyXRRub_0024ffV4hDQeUto();
		}
		else if (showMsg)
		{
			if (!HelpDefs.isJ)
			{
				MPatchr.ShowDebugMsg(global::_003CModule_003E.smethod_29<string>(264707366u));
			}
			else
			{
				MPatchr.ShowDebugMsg(global::_003CModule_003E.smethod_25<string>(2962961956u));
			}
		}
	}

	internal static void N3ZzJUXduuCvN68K8J5pzSI()
	{
		int_0++;
		oXDDyITkEuj8nDCyvcjtZDQ = AAQQTSoA9S5pIq8vyt_0Jn0;
		om7X2R_aOZ08nmTyKqLRdgs = AfZDs_0024IG7ovd1i5_0024f_00244KVwk;
	}

	internal static void URHB7oHimfLtN31uHHTfkH6leCSDr7GxS9wOPypJM2F_()
	{
		for (int i = 0; i < list_0.Count; i++)
		{
			smethod_44(smethod_43((Component)list_0[i]), i == int_0);
			JqKDtyiFnJcdoNFYMikviGo[i].hLxnG9Hq33zU_YUsu_00240_zak = i == int_0;
		}
	}

	internal static void smethod_39(string name, string text, Transform parent, string patchName, bool reInit = true, bool increment = true, bool interactable = true)
	{
		smethod_41(name, text, parent, delegate(bool toggled)
		{
			LogSettingsUi("patch toggle: " + patchName + "=" + toggled);
			smethod_42(patchName, toggled);
			FMpPDgPqT_0024MlkjDbhXAGLgVbz45OJagoxMsHXXVw14C6.smethod_0(patchName, toggled);
		}, FMpPDgPqT_0024MlkjDbhXAGLgVbz45OJagoxMsHXXVw14C6.BBgGuwZ6cMArb_002436eVwTw8A(patchName), reInit: true, increment, interactable);
	}

	[Obsolete]
	internal static void smethod_40(string name, Vector3 pos, string text, Transform parent, Action<bool> onToggle, Vector2 size, bool initiallyToggled, bool reInit = true, bool interactable = true)
	{
		Control0 control = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.uEsWMK_pFkCY_0024M5zt8zLsQk(name, pos, text, parent, resetGroup: true, delegate(bool toggled)
		{
			onToggle(toggled);
			if (reInit)
			{
				a8zL_jLlMzZ_3qoyYFbFj64(toggled);
			}
		});
		smethod_46((RectTransform)smethod_45((Component)control), size);
		control.hLxnG9Hq33zU_YUsu_00240_zak = initiallyToggled;
		control.FLSdXom6uNTfN55f5nxTsH8 = interactable;
		Text component = smethod_43((Component)control).smethod_0(global::_003CModule_003E.smethod_28<string>(436489240u)).GetComponent<Text>();
		smethod_47(component, 10);
		smethod_49(component, smethod_48(component));
		smethod_50(component, bool_0: true);
	}

	[Obsolete]
	internal static void smethod_41(string name, string text, Transform parent, Action<bool> onToggle, bool initiallyToggled, bool reInit = true, bool increment = true, bool interactable = true)
	{
		while (list_0.Count < int_0 + 1)
		{
			GameObject gameObject = new GameObject(global::_003CModule_003E.smethod_28<string>(223769126u) + list_0.Count);
			RectTransform rectTransform = gameObject.AddComponent<RectTransform>();
			rectTransform.localPosition = new Vector3(0f, 0f);
			rectTransform.sizeDelta = vector2_0;
			gameObject.SetActive(value: true);
			list_0.Add(gameObject.transform);
			gameObject.transform.SetParent(parent, worldPositionStays: false);
		}
		smethod_40(name, new Vector3(oXDDyITkEuj8nDCyvcjtZDQ, om7X2R_aOZ08nmTyKqLRdgs), text, list_0[int_0], onToggle, new Vector2(240f, 30f), initiallyToggled, reInit, interactable);
		if (increment)
		{
			oXDDyITkEuj8nDCyvcjtZDQ += int_1;
			if (oXDDyITkEuj8nDCyvcjtZDQ >= AAQQTSoA9S5pIq8vyt_0Jn0 + int_1 * yE0WRQCDuxkVK37TLNJxGY4)
			{
				oXDDyITkEuj8nDCyvcjtZDQ = AAQQTSoA9S5pIq8vyt_0Jn0;
				om7X2R_aOZ08nmTyKqLRdgs += int_2;
			}
		}
	}

	internal static void smethod_42(string id, bool enabled)
	{
		if (!smethod_51(id, global::_003CModule_003E.smethod_29<string>(1462707763u)))
		{
			if (smethod_51(id, global::_003CModule_003E.smethod_27<string>(2946849963u)))
			{
				return;
			}
			else if (!smethod_51(id, global::_003CModule_003E.smethod_25<string>(3741444021u)))
			{
				if (!smethod_51(id, global::_003CModule_003E.smethod_27<string>(2220937616u)))
				{
					if (!smethod_51(id, global::_003CModule_003E.smethod_27<string>(243299386u)))
					{
						if (smethod_51(id, global::_003CModule_003E.smethod_29<string>(2459633423u)))
						{
							MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.tracing = enabled;
							if (!smethod_57(global::_003CModule_003E.smethod_27<string>(94671762u)))
							{
								smethod_58(global::_003CModule_003E.smethod_28<string>(1029786563u));
							}
							if (enabled && !MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.tracingSupported)
							{
								if (smethod_52(global::_003CModule_003E.smethod_25<string>(3102783141u)))
								{
									smethod_53(global::_003CModule_003E.smethod_25<string>(3102783141u), global::_003CModule_003E.smethod_27<string>(3495844294u));
									return;
								}
								Control0 component = smethod_54(global::_003CModule_003E.smethod_26<string>(1475130163u)).GetComponent<Control0>();
								Ox2iDlZ3c70_00246RoQSfv0JsUA9Zr8fowUBRLdiyCjXAIPYXycTbHLX39SoZBma4Hq2A rZWNRvB73KHFd2kQTExe3U0 = null;
								if (smethod_55((UnityEngine.Object)component.GetComponent<Ox2iDlZ3c70_00246RoQSfv0JsUA9Zr8fowUBRLdiyCjXAIPYXycTbHLX39SoZBma4Hq2A>(), (UnityEngine.Object)null))
								{
									rZWNRvB73KHFd2kQTExe3U0 = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.ZM_0024v0fYSxVbN4TDn9D55Ev7u_0024qEN6_e_xmFvC96KpSiq(component);
								}
								else
								{
									rZWNRvB73KHFd2kQTExe3U0 = component.GetComponent<Ox2iDlZ3c70_00246RoQSfv0JsUA9Zr8fowUBRLdiyCjXAIPYXycTbHLX39SoZBma4Hq2A>();
								}
								rZWNRvB73KHFd2kQTExe3U0.method_1(0f);
								component.FLSdXom6uNTfN55f5nxTsH8 = false;
								MPatchr.NNbVj5nqStzgkt0zSfIM_qs(global::_003CModule_003E.smethod_27<string>(2808441533u), global::_003CModule_003E.smethod_26<string>(1524875761u), bool_1: true, delegate
								{
									MPatchr.ShowDebugMsg(global::_003CModule_003E.smethod_25<string>(1178465625u));
								}, delegate
								{
									MPatchr.ShowDebugMsg(global::_003CModule_003E.smethod_26<string>(2429350413u));
								}, delegate(float prog)
								{
									rZWNRvB73KHFd2kQTExe3U0.method_1(prog * 100f);
								});
							}
						}
					}
					else
					{
						MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.vrARG = enabled;
						if (enabled && !MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.vrSupported)
						{
							Control0 component2 = smethod_54(global::_003CModule_003E.smethod_25<string>(1553557994u)).GetComponent<Control0>();
							Ox2iDlZ3c70_00246RoQSfv0JsUA9Zr8fowUBRLdiyCjXAIPYXycTbHLX39SoZBma4Hq2A rZWNRvB73KHFd2kQTExe3U1 = null;
							if (smethod_55((UnityEngine.Object)component2.GetComponent<Ox2iDlZ3c70_00246RoQSfv0JsUA9Zr8fowUBRLdiyCjXAIPYXycTbHLX39SoZBma4Hq2A>(), (UnityEngine.Object)null))
							{
								rZWNRvB73KHFd2kQTExe3U1 = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.ZM_0024v0fYSxVbN4TDn9D55Ev7u_0024qEN6_e_xmFvC96KpSiq(component2);
							}
							else
							{
								rZWNRvB73KHFd2kQTExe3U1 = component2.GetComponent<Ox2iDlZ3c70_00246RoQSfv0JsUA9Zr8fowUBRLdiyCjXAIPYXycTbHLX39SoZBma4Hq2A>();
							}
							rZWNRvB73KHFd2kQTExe3U1.method_1(0f);
							component2.FLSdXom6uNTfN55f5nxTsH8 = false;
							MPatchr.D_piYD85y42L6Wf1rNLe2jo(new string[3]
							{
								global::_003CModule_003E.smethod_26<string>(3399734791u),
								global::_003CModule_003E.smethod_26<string>(394107664u),
								global::_003CModule_003E.smethod_29<string>(1823033645u)
							}, new string[3]
							{
								global::_003CModule_003E.smethod_27<string>(792293717u),
								global::_003CModule_003E.smethod_29<string>(3660153561u),
								global::_003CModule_003E.smethod_29<string>(2773028764u)
							}, bool_1: true, delegate
							{
								MPatchr.ShowDebugMsg(global::_003CModule_003E.smethod_27<string>(4036986621u));
							}, delegate
							{
								MPatchr.ShowDebugMsg(global::_003CModule_003E.smethod_28<string>(3062667248u));
							}, delegate(float prog)
							{
								rZWNRvB73KHFd2kQTExe3U1.method_1(prog * 100f);
							});
						}
						else if (!enabled && MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.vrSupported)
						{
							smethod_56(global::_003CModule_003E.smethod_29<string>(1280092548u));
							smethod_56(global::_003CModule_003E.smethod_29<string>(3660153561u));
							smethod_56(global::_003CModule_003E.smethod_26<string>(2047690452u));
							try
							{
								smethod_56(global::_003CModule_003E.smethod_27<string>(1857896900u));
							}
							catch (UnauthorizedAccessException)
							{
								if (smethod_52(global::_003CModule_003E.smethod_25<string>(1874515683u)))
								{
									smethod_56(global::_003CModule_003E.smethod_26<string>(3043574236u));
								}
								smethod_53(global::_003CModule_003E.smethod_29<string>(3660153561u), global::_003CModule_003E.smethod_26<string>(3043574236u));
							}
							try
							{
								smethod_56(global::_003CModule_003E.smethod_25<string>(2981893051u));
							}
							catch (UnauthorizedAccessException)
							{
								if (smethod_52(global::_003CModule_003E.smethod_26<string>(2158328573u)))
								{
									smethod_56(global::_003CModule_003E.smethod_29<string>(3034656483u));
								}
								smethod_53(global::_003CModule_003E.smethod_28<string>(270855250u), global::_003CModule_003E.smethod_26<string>(2158328573u));
							}
						}
					}
				}
				else
				{
					MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.discordRPC = enabled;
					if (enabled && !MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.discordSupported)
					{
						if (smethod_52(global::_003CModule_003E.smethod_25<string>(2122407597u)))
						{
							smethod_53(global::_003CModule_003E.smethod_25<string>(2122407597u), global::_003CModule_003E.smethod_25<string>(3682626225u));
							return;
						}
						Control0 component3 = smethod_54(global::_003CModule_003E.smethod_28<string>(3881317115u)).GetComponent<Control0>();
						Ox2iDlZ3c70_00246RoQSfv0JsUA9Zr8fowUBRLdiyCjXAIPYXycTbHLX39SoZBma4Hq2A rZWNRvB73KHFd2kQTExe3U2 = null;
						if (smethod_55((UnityEngine.Object)component3.GetComponent<Ox2iDlZ3c70_00246RoQSfv0JsUA9Zr8fowUBRLdiyCjXAIPYXycTbHLX39SoZBma4Hq2A>(), (UnityEngine.Object)null))
						{
							rZWNRvB73KHFd2kQTExe3U2 = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.ZM_0024v0fYSxVbN4TDn9D55Ev7u_0024qEN6_e_xmFvC96KpSiq(component3);
						}
						else
						{
							rZWNRvB73KHFd2kQTExe3U2 = component3.GetComponent<Ox2iDlZ3c70_00246RoQSfv0JsUA9Zr8fowUBRLdiyCjXAIPYXycTbHLX39SoZBma4Hq2A>();
						}
						rZWNRvB73KHFd2kQTExe3U2.method_1(0f);
						component3.FLSdXom6uNTfN55f5nxTsH8 = false;
						MPatchr.NNbVj5nqStzgkt0zSfIM_qs(global::_003CModule_003E.smethod_26<string>(2716536381u), global::_003CModule_003E.smethod_25<string>(3682626225u), bool_1: true, delegate
						{
							MPatchr.ShowDebugMsg(global::_003CModule_003E.smethod_29<string>(2654644942u));
						}, delegate
						{
							MPatchr.ShowDebugMsg(global::_003CModule_003E.smethod_26<string>(2893919704u));
						}, delegate(float prog)
						{
							rZWNRvB73KHFd2kQTExe3U2.method_1(prog * 100f);
						});
					}
					else if (!enabled && MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.discordSupported)
					{
						try
						{
							smethod_56(global::_003CModule_003E.smethod_26<string>(4013958614u));
						}
						catch (UnauthorizedAccessException)
						{
							if (smethod_52(global::_003CModule_003E.smethod_26<string>(1635513882u)))
							{
								smethod_56(global::_003CModule_003E.smethod_26<string>(1635513882u));
							}
							smethod_53(global::_003CModule_003E.smethod_26<string>(4013958614u), global::_003CModule_003E.smethod_29<string>(2515214390u));
						}
					}
				}
			}
			else
			{
				MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.indivFix = enabled;
				a8zL_jLlMzZ_3qoyYFbFj64(enabled);
			}
		}
		else
		{
			MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.hardKick = enabled;
			a8zL_jLlMzZ_3qoyYFbFj64(enabled);
		}
		MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.UUiRNMwxRbfk_Fs4cDErRoM();
	}

	private static void LogSettingsUi(object message)
	{
		string text = "[MPatcherFork.Settings] " + message;
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(text);
	}

	private static bool settingsUiBuildPending;

	private static GameObject settingsUiRoot;

	private static bool IsSettingsUiTemplateReady()
	{
		try
		{
			return Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.smethod_0(Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.objectType.list) != null;
		}
		catch (Exception)
		{
			return false;
		}
	}

	private static IEnumerator BuildSettingsUiAfterSceneReady()
	{
		LogSettingsUi("deferred settings UI initialization scheduled");
		while (smethod_59() < 1f || !IsSettingsUiTemplateReady())
		{
			yield return null;
		}
		yield return null;
		settingsUiBuildPending = false;
		if (settingsUiRoot != null)
		{
			LogSettingsUi("deferred initialization skipped because UI was already created");
			yield break;
		}
		LogSettingsUi("scene is ready; resuming settings UI initialization");
		FeUAVwFbW6wGJJdNimZY9yI(null);
	}

	[HarmonyPostfix]
	[HarmonyPriority(200)]
	internal static void FeUAVwFbW6wGJJdNimZY9yI(Option __instance)
	{
		if (settingsUiRoot != null)
		{
			LogSettingsUi("settings UI already exists");
			return;
		}
		float timeSinceLevelLoad = smethod_59();
		LogSettingsUi("Option.Start postfix entered; timeSinceLevelLoad=" + timeSinceLevelLoad);
		if (timeSinceLevelLoad < 1f || !IsSettingsUiTemplateReady())
		{
			if (!settingsUiBuildPending)
			{
				settingsUiBuildPending = true;
				MPatchr runner = MPatchr.xcBvxcM_0024ckBeZyvdSoAkJoM;
				if (runner == null)
				{
					settingsUiBuildPending = false;
					LogSettingsUi("cannot schedule deferred initialization because MPatchr runner is missing");
					return;
				}
				runner.StartCoroutine(BuildSettingsUiAfterSceneReady());
			}
			return;
		}
		Hn0l0GNFrsPTtJLnYPdC5kstSoR0QDTE9ryF4MZWxnfvyPILHvg2xgflP78f6237KLozQUyL_0024rkMHW57LTXcv7a9PYzi9PlA1Haf6t_0024yjosu settingsUi = new Hn0l0GNFrsPTtJLnYPdC5kstSoR0QDTE9ryF4MZWxnfvyPILHvg2xgflP78f6237KLozQUyL_0024rkMHW57LTXcv7a9PYzi9PlA1Haf6t_0024yjosu();
		try
		{
		GameObject gameObject = settingsUi.EtKYDMu4poqOB22SlzMLERHGSjWcebefrsf6_0024uAAk0da = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.qSsYOsEQb7x452a9Y45dVEk(new Vector2(-80f, 30f), vector2_0);
		settingsUi.EtKYDMu4poqOB22SlzMLERHGSjWcebefrsf6_0024uAAk0da.SetActive(value: false);
		GameObject F6axQ9D1ECsGlTewUCPLil8 = settingsUi.F6axQ9D1ECsGlTewUCPLil8 = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.qSsYOsEQb7x452a9Y45dVEk(new Vector2(320f, 207f), new Vector2(250f, 200f));
		settingsUi.F6axQ9D1ECsGlTewUCPLil8.SetActive(value: false);
		lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw2 = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.P3tLroX6fcPfQha_0024JdbpYXg(global::_003CModule_003E.smethod_27<string>(1835899700u), new Vector3(0f, 50f), global::_003CModule_003E.smethod_25<string>(1693379179u), null, F6axQ9D1ECsGlTewUCPLil8.transform);
		lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw2.DmPZGWxJ26_0024f_0024QOvQiqpmW8(new Vector2(60f, 0f));
		lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw3 = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.P3tLroX6fcPfQha_0024JdbpYXg(global::_003CModule_003E.smethod_25<string>(3094845527u), new Vector3(0f, 0f), global::_003CModule_003E.smethod_25<string>(596995292u), null, F6axQ9D1ECsGlTewUCPLil8.transform);
		lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw3.DmPZGWxJ26_0024f_0024QOvQiqpmW8(new Vector2(60f, 0f));
		lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw obj = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.P3tLroX6fcPfQha_0024JdbpYXg(global::_003CModule_003E.smethod_29<string>(3510913913u), new Vector3(0f, -50f), global::_003CModule_003E.smethod_29<string>(2623789116u), null, F6axQ9D1ECsGlTewUCPLil8.transform);
		obj.DmPZGWxJ26_0024f_0024QOvQiqpmW8(new Vector2(60f, 0f));
		lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw2.t2iJT_tBPyB6QRMBLAdXYUs(delegate
		{
			Class37.smethod_0(global::_003CModule_003E.smethod_28<string>(1347532871u));
		});
		lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw3.t2iJT_tBPyB6QRMBLAdXYUs(delegate
		{
			Class37.smethod_0(global::_003CModule_003E.smethod_25<string>(4203850144u));
		});
		obj.t2iJT_tBPyB6QRMBLAdXYUs(delegate
		{
			Class37.smethod_0(global::_003CModule_003E.smethod_25<string>(2365417764u));
		});
		oXDDyITkEuj8nDCyvcjtZDQ = AAQQTSoA9S5pIq8vyt_0Jn0;
		om7X2R_aOZ08nmTyKqLRdgs = AfZDs_0024IG7ovd1i5_0024f_00244KVwk;
		int_0 = 0;
		list_0.Clear();
		smethod_39(global::_003CModule_003E.smethod_25<string>(3622379811u), global::_003CModule_003E.smethod_29<string>(2201819049u), gameObject.transform, global::_003CModule_003E.smethod_26<string>(1220690189u));
		smethod_39(global::_003CModule_003E.smethod_29<string>(286912766u), global::_003CModule_003E.smethod_26<string>(3297178734u), gameObject.transform, global::_003CModule_003E.smethod_25<string>(3741444021u));
		smethod_39(global::_003CModule_003E.smethod_28<string>(1999511299u), global::_003CModule_003E.smethod_29<string>(2526317090u), gameObject.transform, global::_003CModule_003E.smethod_25<string>(1243593786u));
		smethod_39(global::_003CModule_003E.smethod_28<string>(815732586u), global::_003CModule_003E.smethod_28<string>(3292466002u), gameObject.transform, global::_003CModule_003E.smethod_26<string>(216724341u));
		smethod_39(global::_003CModule_003E.smethod_29<string>(4373251u), global::_003CModule_003E.smethod_29<string>(3985877739u), gameObject.transform, global::_003CModule_003E.smethod_28<string>(2776262287u));
		smethod_39(global::_003CModule_003E.smethod_28<string>(4200990114u), global::_003CModule_003E.smethod_25<string>(2137885563u), gameObject.transform, global::_003CModule_003E.smethod_29<string>(3434353683u));
		smethod_39(global::_003CModule_003E.smethod_25<string>(3677943109u), global::_003CModule_003E.smethod_25<string>(2540243903u), gameObject.transform, global::_003CModule_003E.smethod_29<string>(1962464197u));
		smethod_39(global::_003CModule_003E.smethod_25<string>(4073593822u), global::_003CModule_003E.smethod_28<string>(4248372652u), gameObject.transform, global::_003CModule_003E.smethod_28<string>(1789755325u));
		smethod_39(global::_003CModule_003E.smethod_26<string>(529827453u), global::_003CModule_003E.smethod_25<string>(1462592480u), gameObject.transform, global::_003CModule_003E.smethod_29<string>(2914911590u));
		smethod_39(global::_003CModule_003E.smethod_28<string>(2728879749u), global::_003CModule_003E.smethod_26<string>(1626596342u), gameObject.transform, global::_003CModule_003E.smethod_25<string>(1948811445u));
		smethod_39(global::_003CModule_003E.smethod_28<string>(2364640145u), global::_003CModule_003E.smethod_25<string>(2954627080u), gameObject.transform, global::_003CModule_003E.smethod_25<string>(2427092796u));
		smethod_39(global::_003CModule_003E.smethod_25<string>(2498928584u), global::_003CModule_003E.smethod_26<string>(1674530293u), gameObject.transform, global::_003CModule_003E.smethod_28<string>(4278824757u));
		smethod_39(global::_003CModule_003E.smethod_28<string>(72990671u), global::_003CModule_003E.smethod_28<string>(894456471u), gameObject.transform, global::_003CModule_003E.smethod_26<string>(4240669797u));
		smethod_39(global::_003CModule_003E.smethod_25<string>(4032278503u), global::_003CModule_003E.smethod_29<string>(3498585076u), gameObject.transform, global::_003CModule_003E.smethod_29<string>(156065173u), reInit: true, increment: true, File.Exists(global::_003CModule_003E.smethod_28<string>(2850243548u)));
		smethod_39(global::_003CModule_003E.smethod_27<string>(2957407327u), global::_003CModule_003E.smethod_26<string>(1193379136u), gameObject.transform, global::_003CModule_003E.smethod_27<string>(67057710u));
		smethod_39(global::_003CModule_003E.smethod_29<string>(4072314532u), global::_003CModule_003E.smethod_26<string>(4115679195u), gameObject.transform, global::_003CModule_003E.smethod_27<string>(574292793u));
		smethod_39(global::_003CModule_003E.smethod_26<string>(215330432u), global::_003CModule_003E.smethod_26<string>(1347492438u), gameObject.transform, global::_003CModule_003E.smethod_26<string>(4157342729u));
		smethod_39(global::_003CModule_003E.smethod_29<string>(2903876357u), global::_003CModule_003E.smethod_25<string>(1783550169u), gameObject.transform, global::_003CModule_003E.smethod_28<string>(3186105945u));
		smethod_39(global::_003CModule_003E.smethod_29<string>(242501966u), global::_003CModule_003E.smethod_26<string>(2120288333u), gameObject.transform, global::_003CModule_003E.smethod_25<string>(1060631402u));
		smethod_39(global::_003CModule_003E.smethod_25<string>(3018525254u), global::_003CModule_003E.smethod_25<string>(2754758112u), gameObject.transform, global::_003CModule_003E.smethod_26<string>(2132829167u));
		smethod_39(global::_003CModule_003E.smethod_25<string>(1221408193u), global::_003CModule_003E.smethod_29<string>(2016684093u), gameObject.transform, global::_003CModule_003E.smethod_29<string>(1270215985u));
		smethod_39(global::_003CModule_003E.smethod_27<string>(1845273469u), global::_003CModule_003E.smethod_28<string>(2427174632u), gameObject.transform, global::_003CModule_003E.smethod_26<string>(4199006263u));
		smethod_39(global::_003CModule_003E.smethod_28<string>(2379792094u), global::_003CModule_003E.smethod_26<string>(3919902359u), gameObject.transform, global::_003CModule_003E.smethod_28<string>(3641405450u));
		smethod_39(global::_003CModule_003E.smethod_29<string>(1887197571u), global::_003CModule_003E.smethod_25<string>(2171263268u), gameObject.transform, global::_003CModule_003E.smethod_26<string>(4192735846u));
		smethod_39(global::_003CModule_003E.smethod_26<string>(2264925662u), global::_003CModule_003E.smethod_25<string>(2031442083u), gameObject.transform, global::_003CModule_003E.smethod_29<string>(2309100171u));
		smethod_39(global::_003CModule_003E.smethod_28<string>(2152216445u), global::_003CModule_003E.smethod_25<string>(3177078903u), gameObject.transform, global::_003CModule_003E.smethod_25<string>(2608229300u));
		smethod_39(global::_003CModule_003E.smethod_28<string>(133746674u), global::_003CModule_003E.smethod_29<string>(491665914u), gameObject.transform, global::_003CModule_003E.smethod_28<string>(2533534689u));
		smethod_39(global::_003CModule_003E.smethod_25<string>(4040216117u), global::_003CModule_003E.smethod_26<string>(2383645847u), gameObject.transform, global::_003CModule_003E.smethod_25<string>(2201783737u));
		smethod_39(global::_003CModule_003E.smethod_26<string>(54946713u), global::_003CModule_003E.smethod_26<string>(847530030u), gameObject.transform, global::_003CModule_003E.smethod_27<string>(29900804u));
		N3ZzJUXduuCvN68K8J5pzSI();
		LogSettingsUi("adding Compression toggle on P2; initial=" + MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.compression);
		smethod_41("Toggle_Compression", "Compression", gameObject.transform, delegate(bool toggled)
		{
			MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.compression = toggled;
			MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.UUiRNMwxRbfk_Fs4cDErRoM();
			LogSettingsUi("Compression=" + toggled);
		}, MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.compression);
		smethod_39(global::_003CModule_003E.smethod_26<string>(2906460538u), global::_003CModule_003E.smethod_27<string>(1891127804u), gameObject.transform, global::_003CModule_003E.smethod_27<string>(4208456870u));
		smethod_39(global::_003CModule_003E.smethod_27<string>(579571475u), global::_003CModule_003E.smethod_26<string>(2761823209u), gameObject.transform, global::_003CModule_003E.smethod_28<string>(2654898488u));
		smethod_39(global::_003CModule_003E.smethod_25<string>(396729062u), global::_003CModule_003E.smethod_28<string>(4293976706u), gameObject.transform, global::_003CModule_003E.smethod_27<string>(2370410665u));
		smethod_39(global::_003CModule_003E.smethod_25<string>(528612633u), global::_003CModule_003E.smethod_28<string>(1364907925u), gameObject.transform, global::_003CModule_003E.smethod_26<string>(1074658951u));
		smethod_39(global::_003CModule_003E.smethod_25<string>(264845491u), global::_003CModule_003E.smethod_26<string>(3842845708u), gameObject.transform, global::_003CModule_003E.smethod_29<string>(3769954424u));
		smethod_39(global::_003CModule_003E.smethod_29<string>(632390070u), global::_003CModule_003E.smethod_25<string>(291515569u), gameObject.transform, global::_003CModule_003E.smethod_25<string>(3621982549u));
		smethod_39(global::_003CModule_003E.smethod_25<string>(3158346439u), global::_003CModule_003E.smethod_26<string>(2855461726u), gameObject.transform, global::_003CModule_003E.smethod_25<string>(1883683284u));
		smethod_41(global::_003CModule_003E.smethod_25<string>(2457613265u), global::_003CModule_003E.smethod_27<string>(1677729222u), gameObject.transform, delegate(bool toggled)
		{
			MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.jNJFoLQ_wY8hPL4TF_0024pIwMo(toggled ? 50 : (-1));
		}, MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.smoothUI != -1);
		smethod_39(global::_003CModule_003E.smethod_27<string>(132843178u), global::_003CModule_003E.smethod_29<string>(2924788153u), gameObject.transform, global::_003CModule_003E.smethod_25<string>(2584416458u));
		smethod_39(global::_003CModule_003E.smethod_25<string>(3685880723u), global::_003CModule_003E.smethod_25<string>(177134475u), gameObject.transform, global::_003CModule_003E.smethod_27<string>(3598955753u));
		smethod_39(global::_003CModule_003E.smethod_29<string>(1422042841u), global::_003CModule_003E.smethod_27<string>(3259264917u), gameObject.transform, global::_003CModule_003E.smethod_25<string>(1578600823u));
		smethod_41("Toggle_FreeCouplerRot", "Free Coupler Rot", gameObject.transform,
			MPatcherFork.CustomPatches.CouplerRotation.SetEnabled,
			MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.freeCouplerRot, reInit: false);
		LogSettingsUi("adding Setup Precision toggle on P2; initial=" + MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.setupPrecision);
		MPatcherFork.CustomPatches.SetupPrecisionSettingsUi.CreateRow(list_0[int_0], gameObject.transform,
			new Vector3(oXDDyITkEuj8nDCyvcjtZDQ, om7X2R_aOZ08nmTyKqLRdgs));
		int_0 = 0;
		JqKDtyiFnJcdoNFYMikviGo.Clear();
		for (int num = 0; num < list_0.Count; num++)
		{
			int tws1nQARWc_py1S24is5aQY = num;
			Control0 control = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.uEsWMK_pFkCY_0024M5zt8zLsQk(global::_003CModule_003E.smethod_25<string>(3512681833u) + num, new Vector3(-230 + 55 * num, -300f), global::_003CModule_003E.smethod_29<string>(2914979057u) + (num + 1), gameObject.transform, resetGroup: true, delegate
			{
				LogSettingsUi("page selected: P" + (tws1nQARWc_py1S24is5aQY + 1));
				int_0 = tws1nQARWc_py1S24is5aQY;
				URHB7oHimfLtN31uHHTfkH6leCSDr7GxS9wOPypJM2F_();
			});
			control.UzVS61irgJn5Pnqwx0lThng(new Vector2(50f, 40f));
			JqKDtyiFnJcdoNFYMikviGo.Add(control);
		}
		URHB7oHimfLtN31uHHTfkH6leCSDr7GxS9wOPypJM2F_();
		LogSettingsUi("legacy updater controls omitted: button, channel and notifications");
		GameObject gameObject_0 = settingsUi.gameObject_0 = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.qSsYOsEQb7x452a9Y45dVEk(new Vector2(400f, 55f), new Vector2(420f, 100f));
		gameObject_0.SetActive(value: false);
		listController_0 = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.nN2N4qjnQLwFOaONUPeRAdg(global::_003CModule_003E.smethod_27<string>(3878816143u), global::_003CModule_003E.smethod_26<string>(2002403624u), new Vector3(-100f, -10f), uGQUy_0024Mw_q46atKOrYCeWos, gameObject_0.transform);
		listController_0.SetSelectedItem(uGQUy_0024Mw_q46atKOrYCeWos[(int)MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.translation]);
		WM8LkAhdj7QtGX92nWcmHq4 = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.nN2N4qjnQLwFOaONUPeRAdg(global::_003CModule_003E.smethod_27<string>(3878816143u), global::_003CModule_003E.smethod_29<string>(3984719069u), new Vector3(100f, -10f), string_0, gameObject_0.transform);
		WM8LkAhdj7QtGX92nWcmHq4.SetSelectedItem(string_0[(int)MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.translationEngine]);
		GameObject HY_w_AyUHHp9hrJckZ7e3B0 = settingsUi.HY_w_AyUHHp9hrJckZ7e3B0 = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.qSsYOsEQb7x452a9Y45dVEk(new Vector2(-90f, 0f), new Vector2(500f, 450f));
		HY_w_AyUHHp9hrJckZ7e3B0.SetActive(value: false);
		lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw obj2 = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.P3tLroX6fcPfQha_0024JdbpYXg(global::_003CModule_003E.smethod_28<string>(1256621177u), new Vector2(400f, -50f), (!HelpDefs.isJ) ? global::_003CModule_003E.smethod_27<string>(588945244u) : global::_003CModule_003E.smethod_27<string>(3292495821u), null, gameObject.transform);
		obj2.DmPZGWxJ26_0024f_0024QOvQiqpmW8(new Vector2(50f, 0f));
		obj2.t2iJT_tBPyB6QRMBLAdXYUs(settingsUi.CE_0024fb8Vs_0024NbV5dIygtcVK9M);
		Text component = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.YU_pwpP3pKH76IHZTea_SXk(global::_003CModule_003E.smethod_27<string>(3678717332u), new Vector2(0f, 190f), xh28Je5vGZzmlyaYTv4leqahX_0024d76HU68fsrFhxkr_aiwJvGWxEZr2ULd9ujX_W1gg.UzhVmO15k8UHVkcjd2D2eFw(xh28Je5vGZzmlyaYTv4leqahX_0024d76HU68fsrFhxkr_aiwJvGWxEZr2ULd9ujX_W1gg.eEsL_CqOa2k0Oy16TqJNFVJPuzSIPe8Trw5760Yzq7e5ghLIwg7ZyOeyUc9A2E9z_0024mUEQCN09awhbakTSEXMPkahOFo0RcQ91HbGZkSM_Bhl.credits), HY_w_AyUHHp9hrJckZ7e3B0.transform, rmOutline: false, 25, FontStyle.Normal, TextAnchor.UpperCenter).GetComponent<Text>();
		component.horizontalOverflow = HorizontalWrapMode.Overflow;
		component.verticalOverflow = VerticalWrapMode.Overflow;
		GameObject rYYTC4OVAoi1HNKYSdRxdwE = settingsUi.rYYTC4OVAoi1HNKYSdRxdwE = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.qSsYOsEQb7x452a9Y45dVEk(new Vector2(-90f, 0f), new Vector2(500f, 450f));
		rYYTC4OVAoi1HNKYSdRxdwE.SetActive(value: false);
		lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw obj3 = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.P3tLroX6fcPfQha_0024JdbpYXg(global::_003CModule_003E.smethod_26<string>(3445857095u), new Vector2(400f, -100f), global::_003CModule_003E.smethod_28<string>(2258280088u), null, gameObject.transform);
		obj3.DmPZGWxJ26_0024f_0024QOvQiqpmW8(new Vector2(50f, 0f));
		obj3.t2iJT_tBPyB6QRMBLAdXYUs(settingsUi.Cu5yHL2zRFxfY8gCMaYkaoU);
		obj3.FLSdXom6uNTfN55f5nxTsH8 = File.Exists(global::_003CModule_003E.smethod_26<string>(562573447u));
		ToggleGroup toggleGroup = GameObject.Find(global::_003CModule_003E.smethod_27<string>(862174272u)).AddComponent<ToggleGroup>();
		Vector3 vector = new Vector3(-130f, 170f);
		Control0 control3 = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.uEsWMK_pFkCY_0024M5zt8zLsQk(global::_003CModule_003E.smethod_29<string>(1507186030u), vector, global::_003CModule_003E.smethod_27<string>(1314857591u), rYYTC4OVAoi1HNKYSdRxdwE.transform, resetGroup: true, delegate
		{
			MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.vr_headset = 0;
			MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.UUiRNMwxRbfk_Fs4cDErRoM();
		}, null, toggleGroup);
		Control0 control4 = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.uEsWMK_pFkCY_0024M5zt8zLsQk(global::_003CModule_003E.smethod_25<string>(1461362493u), vector - new Vector3(0f, 50f), global::_003CModule_003E.smethod_26<string>(760997406u), rYYTC4OVAoi1HNKYSdRxdwE.transform, resetGroup: true, delegate
		{
			MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.vr_headset = 1;
			MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.UUiRNMwxRbfk_Fs4cDErRoM();
		}, null, toggleGroup);
		Control0 control5 = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.uEsWMK_pFkCY_0024M5zt8zLsQk(global::_003CModule_003E.smethod_26<string>(2566881849u), vector - new Vector3(0f, 100f), global::_003CModule_003E.smethod_27<string>(2952804985u), rYYTC4OVAoi1HNKYSdRxdwE.transform, resetGroup: true, delegate
		{
			MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.vr_headset = 2;
			MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.UUiRNMwxRbfk_Fs4cDErRoM();
		}, null, toggleGroup);
		Control0 control6 = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.uEsWMK_pFkCY_0024M5zt8zLsQk(global::_003CModule_003E.smethod_26<string>(1283812097u), vector - new Vector3(0f, 150f), global::_003CModule_003E.smethod_25<string>(760629319u), rYYTC4OVAoi1HNKYSdRxdwE.transform, resetGroup: true, delegate
		{
			MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.vr_headset = -1;
			MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.UUiRNMwxRbfk_Fs4cDErRoM();
		}, null, toggleGroup);
		switch (MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.vr_headset)
		{
		default:
			control6.hLxnG9Hq33zU_YUsu_00240_zak = true;
			break;
		case 0:
			control3.hLxnG9Hq33zU_YUsu_00240_zak = true;
			break;
		case 1:
			control4.hLxnG9Hq33zU_YUsu_00240_zak = true;
			break;
		case 2:
			control5.hLxnG9Hq33zU_YUsu_00240_zak = true;
			break;
		}
		Control0 control7 = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.uEsWMK_pFkCY_0024M5zt8zLsQk(global::_003CModule_003E.smethod_28<string>(1651164679u), new Vector3(100f, 150f), global::_003CModule_003E.smethod_29<string>(1518288730u), rYYTC4OVAoi1HNKYSdRxdwE.transform, resetGroup: true, delegate(bool toggled)
		{
			MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.vr_curvedScreen = toggled;
			if (Class37.smethod_5((UnityEngine.Object)KEjZwYcSzxf2DUM4tsmq7LTU5Rz8bWNX1Ud16EuZVShS.d_RafUgoVPViHGf69VfQ7eM(), (UnityEngine.Object)null))
			{
				Class37.smethod_8(Class37.smethod_7(Class37.smethod_6(KEjZwYcSzxf2DUM4tsmq7LTU5Rz8bWNX1Ud16EuZVShS.d_RafUgoVPViHGf69VfQ7eM())), MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.vr_curvedScreen ? GUIMonitor.CurvinessState.Curved : GUIMonitor.CurvinessState.Flat);
			}
		});
		control7.hLxnG9Hq33zU_YUsu_00240_zak = MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.vr_curvedScreen;
		((RectTransform)control7.transform).sizeDelta += new Vector2(80f, 0f);
		Control0 control8 = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.uEsWMK_pFkCY_0024M5zt8zLsQk(global::_003CModule_003E.smethod_29<string>(2264756838u), new Vector3(100f, 100f), global::_003CModule_003E.smethod_25<string>(1551930745u), rYYTC4OVAoi1HNKYSdRxdwE.transform, resetGroup: true, delegate(bool toggled)
		{
			MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.vr_camOffset = toggled;
			MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.UUiRNMwxRbfk_Fs4cDErRoM();
		});
		control8.hLxnG9Hq33zU_YUsu_00240_zak = MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.vr_camOffset;
		((RectTransform)control8.transform).sizeDelta += new Vector2(80f, 0f);
		Control0 control9 = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.uEsWMK_pFkCY_0024M5zt8zLsQk(global::_003CModule_003E.smethod_28<string>(3108123095u), new Vector3(100f, 50f), global::_003CModule_003E.smethod_25<string>(2648314632u), rYYTC4OVAoi1HNKYSdRxdwE.transform, resetGroup: true, delegate(bool toggled)
		{
			MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.vr_lockMouse = toggled;
			MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.UUiRNMwxRbfk_Fs4cDErRoM();
		});
		control9.hLxnG9Hq33zU_YUsu_00240_zak = MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.vr_lockMouse;
		((RectTransform)control9.transform).sizeDelta += new Vector2(80f, 0f);
		Control0 control10 = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.uEsWMK_pFkCY_0024M5zt8zLsQk(global::_003CModule_003E.smethod_28<string>(998593423u), new Vector3(100f, 0f), global::_003CModule_003E.smethod_25<string>(1947581458u), rYYTC4OVAoi1HNKYSdRxdwE.transform, resetGroup: true, delegate(bool toggled)
		{
			MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.vr_gameRendDist = toggled;
			MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.UUiRNMwxRbfk_Fs4cDErRoM();
		});
		control10.hLxnG9Hq33zU_YUsu_00240_zak = MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.vr_gameRendDist;
		((RectTransform)control10.transform).sizeDelta += new Vector2(80f, 0f);
		Control0 settingsLauncher = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.uEsWMK_pFkCY_0024M5zt8zLsQk(global::_003CModule_003E.smethod_25<string>(2441738037u), new Vector2(140f, -300f), global::_003CModule_003E.smethod_27<string>(142216947u), null, resetGroup: false, null);
		settingsLauncher.hLxnG9Hq33zU_YUsu_00240_zak = false;
		settingsLauncher.method_0(settingsUi.RJiw4N9LGUVBzWXJaevLL_Y);
		settingsLauncher.Tz4h_68oANQj5xAU0vtoknA.onValueChanged.AddListener(delegate(bool toggled)
		{
			LogSettingsUi("launcher onValueChanged: " + toggled);
			settingsUi.Q8IRIYh1xdDNYZLo3rk05vw(toggled);
		});
		settingsUiRoot = gameObject;
		LogSettingsUi("settings UI ready");
		}
		catch (Exception ex)
		{
			settingsUiRoot = null;
			LogSettingsUi("settings UI initialization failed: " + ex);
			UnityEngine.Debug.LogException(ex);
		}
	}

	internal static GameObject smethod_43(Component component_0)
	{
		return component_0.gameObject;
	}

	internal static void smethod_44(GameObject gameObject_0, bool bool_0)
	{
		gameObject_0.SetActive(bool_0);
	}

	internal static Transform smethod_45(Component component_0)
	{
		return component_0.transform;
	}

	internal static void smethod_46(RectTransform rectTransform_0, Vector2 vector2_1)
	{
		rectTransform_0.sizeDelta = vector2_1;
	}

	internal static void smethod_47(Text text_0, int int_3)
	{
		text_0.resizeTextMinSize = int_3;
	}

	internal static int smethod_48(Text text_0)
	{
		return text_0.fontSize;
	}

	internal static void smethod_49(Text text_0, int int_3)
	{
		text_0.resizeTextMaxSize = int_3;
	}

	internal static void smethod_50(Text text_0, bool bool_0)
	{
		text_0.resizeTextForBestFit = bool_0;
	}

	internal static bool smethod_51(string string_1, string string_2)
	{
		return string_1 == string_2;
	}

	internal static bool smethod_52(string string_1)
	{
		return File.Exists(string_1);
	}

	internal static void smethod_53(string string_1, string string_2)
	{
		File.Move(string_1, string_2);
	}

	internal static GameObject smethod_54(string string_1)
	{
		return GameObject.Find(string_1);
	}

	internal static bool smethod_55(UnityEngine.Object object_0, UnityEngine.Object object_1)
	{
		return object_0 == object_1;
	}

	internal static void smethod_56(string string_1)
	{
		File.Delete(string_1);
	}

	internal static bool smethod_57(string string_1)
	{
		return Directory.Exists(string_1);
	}

	internal static DirectoryInfo smethod_58(string string_1)
	{
		return Directory.CreateDirectory(string_1);
	}

	internal static float smethod_59()
	{
		return Time.time;
	}
}
