using System;
using System.IO;
using System.Runtime.CompilerServices;
using Converter.MeshFormat.Reader;
using Converter.MeshFormat.Writer;
using HarmonyLib;
using MPatchrMain;
using Parabox.STL;
using UnityEngine;
using UnityEngine.UI;

[HarmonyPatch(typeof(Build))]
[HarmonyPatch("IEIKIMMMHPF")]
internal class cueQi_wQP26TRIVnZP9Z8aKHYYL_00245mvYiphrS2INIGjFBzL0M0_0024ZrwQ0Yl0WnnCZVw
{
	[CompilerGenerated]
	private sealed class _00247ZR6o2lkhBastv3E8uM6Kr_00247Hz4U4IBlxIY7z361c7LycNuiOBpOz0KakG1PyHkhIAQ5S_0024duNArs8iOrHBXUrOUSfSDDUvGs4Nwj8KGLerI02BUwgMRddsbGc8dRA2NSw
	{
		public GameObject tTCBVE8wPVPHfGDkecHRn4M;

		public bool cyETWNLQnBnCbM1UajPK3PM;

		public ListController E2ZU4x9MsEz_0024X55Wlk4dasI;

		public string[] kIp26e25DhIAn1xjVh_0024AgyE;

		public Predicate<string> predicate_0;

		internal void method_0(lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw button)
		{
			smethod_1(tTCBVE8wPVPHfGDkecHRn4M, !smethod_0(tTCBVE8wPVPHfGDkecHRn4M));
		}

		internal void method_1(float value)
		{
			UveP8b00PUlmVfaMhyr2jHw = (int)value;
			UUiRNMwxRbfk_Fs4cDErRoM();
			G_tAsMqijFlE62CHaw1EnOo(cyETWNLQnBnCbM1UajPK3PM);
		}

		internal void method_2(float value)
		{
			bYEBUC3V9AL8F1BfUn9TeDQ = (int)value;
			UUiRNMwxRbfk_Fs4cDErRoM();
			G_tAsMqijFlE62CHaw1EnOo(cyETWNLQnBnCbM1UajPK3PM);
		}

		internal void method_3(float value)
		{
			iM3JEr6PTIzQeQfXXIM9lCI = (int)value;
			UUiRNMwxRbfk_Fs4cDErRoM();
			G_tAsMqijFlE62CHaw1EnOo(cyETWNLQnBnCbM1UajPK3PM);
		}

		internal void method_4(string value)
		{
			if (int.TryParse(value, out var result))
			{
				UveP8b00PUlmVfaMhyr2jHw = result;
				UUiRNMwxRbfk_Fs4cDErRoM();
				G_tAsMqijFlE62CHaw1EnOo(cyETWNLQnBnCbM1UajPK3PM);
			}
		}

		internal void method_5(string value)
		{
			if (int.TryParse(value, out var result))
			{
				bYEBUC3V9AL8F1BfUn9TeDQ = result;
				UUiRNMwxRbfk_Fs4cDErRoM();
				G_tAsMqijFlE62CHaw1EnOo(cyETWNLQnBnCbM1UajPK3PM);
			}
		}

		internal void method_6(string value)
		{
			if (int.TryParse(value, out var result))
			{
				iM3JEr6PTIzQeQfXXIM9lCI = result;
				UUiRNMwxRbfk_Fs4cDErRoM();
				G_tAsMqijFlE62CHaw1EnOo(cyETWNLQnBnCbM1UajPK3PM);
			}
		}

		internal void jIRyM_0024i36X8Eez5_ofM3lUpaHGtQYI5vAolJjXJUxQhY(float value)
		{
			lptWFZXC_5XxXOiE9y1G9fM = (int)value;
			UUiRNMwxRbfk_Fs4cDErRoM();
			G_tAsMqijFlE62CHaw1EnOo(cyETWNLQnBnCbM1UajPK3PM);
		}

		internal void ja1IEuo0ywe3JGjquBwhkAQfN74ZsJBM2VBOgKv85Tk9(float value)
		{
			se1xdwxG_0024GsA0diB9M3Fzm8 = (int)value;
			UUiRNMwxRbfk_Fs4cDErRoM();
			G_tAsMqijFlE62CHaw1EnOo(cyETWNLQnBnCbM1UajPK3PM);
		}

		internal void jiUum_0024LDKcMKj7KPNiOWDR8f65sd2wa20DTBBemIEGKZ(float value)
		{
			zIVmtI3upt3qSoNlsRdRixM = (int)value;
			UUiRNMwxRbfk_Fs4cDErRoM();
			G_tAsMqijFlE62CHaw1EnOo(cyETWNLQnBnCbM1UajPK3PM);
		}

		internal void j4CMDgl8wZOxF9lj3GLdzYH7_RyC7FaMhE9nzokvFNiC(float value)
		{
			Z3zFd_bV0CuH_om_iJXGNWk = (int)value;
			UUiRNMwxRbfk_Fs4cDErRoM();
			G_tAsMqijFlE62CHaw1EnOo(cyETWNLQnBnCbM1UajPK3PM);
		}

		internal void kNKrEEs8121K5JyKgNKqN0cgTm7MqBcyYI0nUjiIHWRh(float value)
		{
			mJRRGc1MCKKCdMAZPVtY74Q = (int)value;
			UUiRNMwxRbfk_Fs4cDErRoM();
			G_tAsMqijFlE62CHaw1EnOo(cyETWNLQnBnCbM1UajPK3PM);
		}

		internal void kdepwr9K7_0024LtZY1kFd7QQgZ6JA_00243aOs8RwYGyt7llpPC(float value)
		{
			FB_RI_0024pdgWj_C0uU8kVb1xY = (int)value;
			UUiRNMwxRbfk_Fs4cDErRoM();
			G_tAsMqijFlE62CHaw1EnOo(cyETWNLQnBnCbM1UajPK3PM);
		}

		internal void ktVXuosreseTDV5Gr3D6IJDjuHAL_0024t6QWHKDXrAxIewH(bool active)
		{
			C12PB1vA_qYK5Jnetlc9RBSzPXTYhVyiUfwxG1qa0jFH = active;
			UUiRNMwxRbfk_Fs4cDErRoM();
			G_tAsMqijFlE62CHaw1EnOo(cyETWNLQnBnCbM1UajPK3PM);
		}

		internal void k65faOnMecZa9fywe7czRwZ_00241RlA8rDirA8p1iqw6aZr(bool active)
		{
			rDw0tG4RmL5JYcee9fgEboMfKhKeL_Y6jHSvaFofn4de = active;
			UUiRNMwxRbfk_Fs4cDErRoM();
			G_tAsMqijFlE62CHaw1EnOo(cyETWNLQnBnCbM1UajPK3PM);
		}

		internal void lFav0LyCTIZhaZ9r1dy6V1PKaTbldEmQ8_0024411dZj4qUh(bool active)
		{
			TRcJx0tr_0bbA4nnDIJPMbD2J3eaaDQBU9fLuH_0024Tvbq8 = active;
			UUiRNMwxRbfk_Fs4cDErRoM();
			G_tAsMqijFlE62CHaw1EnOo(cyETWNLQnBnCbM1UajPK3PM);
		}

		internal void lSDx2pDIXFEWF6RczoQt5_gEjg8hB_OwDeR2bWInNTPv(AmMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F me)
		{
			if (!me.AqTKGFxfR1r6eAzrvm4_0024bck(global::_003CModule_003E.smethod_29<string>(750638958u)))
			{
				me.clf0br4v0HxmaJWIcm_0024_0024GUg(global::_003CModule_003E.smethod_25<string>(1381191829u), smethod_2(E2ZU4x9MsEz_0024X55Wlk4dasI));
			}
			else if (smethod_3(me.hpiqzm2jQTswCo32f7jvrQ4<string>(global::_003CModule_003E.smethod_26<string>(3715485026u)), smethod_2(E2ZU4x9MsEz_0024X55Wlk4dasI)))
			{
				me.clf0br4v0HxmaJWIcm_0024_0024GUg(global::_003CModule_003E.smethod_26<string>(3715485026u), smethod_2(E2ZU4x9MsEz_0024X55Wlk4dasI));
				jUXEKVD_hVvLU2_00247ZKrlGNYnl_JXlZjgpo_gKExPiGEA = (mcpd.buildWireframeMode)Array.FindIndex(kIp26e25DhIAn1xjVh_0024AgyE, (string item) => smethod_4(item, smethod_2(E2ZU4x9MsEz_0024X55Wlk4dasI)));
				UUiRNMwxRbfk_Fs4cDErRoM();
				G_tAsMqijFlE62CHaw1EnOo(cyETWNLQnBnCbM1UajPK3PM);
			}
		}

		internal bool method_7(string item)
		{
			return smethod_4(item, smethod_2(E2ZU4x9MsEz_0024X55Wlk4dasI));
		}

		internal void method_8(string text)
		{
			String_0 = text;
			UUiRNMwxRbfk_Fs4cDErRoM();
			if (smethod_5((UnityEngine.Object)_eatlpymFHP2FW6WtcEk26w, (UnityEngine.Object)null))
			{
				smethod_6((UnityEngine.Object)_eatlpymFHP2FW6WtcEk26w);
			}
			WsRTClbFOqctTzQQJXwCPNU(text, cyETWNLQnBnCbM1UajPK3PM);
		}

		internal static bool smethod_0(GameObject gameObject_0)
		{
			return gameObject_0.activeSelf;
		}

		internal static void smethod_1(GameObject gameObject_0, bool bool_0)
		{
			gameObject_0.SetActive(bool_0);
		}

		internal static string smethod_2(ListController listController_0)
		{
			return listController_0.GetSelectedItem();
		}

		internal static bool smethod_3(string string_0, string string_1)
		{
			return string_0 != string_1;
		}

		internal static bool smethod_4(string string_0, string string_1)
		{
			return string_0 == string_1;
		}

		internal static bool smethod_5(UnityEngine.Object object_0, UnityEngine.Object object_1)
		{
			return object_0 != object_1;
		}

		internal static void smethod_6(UnityEngine.Object object_0)
		{
			UnityEngine.Object.Destroy(object_0);
		}
	}

	[CompilerGenerated]
	private sealed class _N0FgnlP8K15hFaWm38CRHuMKwOx1RWvYklF5mKmV1rAqWhAzf0c_3p4kbzR1EXr4tY02xmG_DmiqCeNYVh_0024PADU0PDPjeZLZgbRqax_IVRAL9hxIhCo59BiLk1P7jWGjw
	{
		public Toggle vhcAS28uyig7KSF7oB37W8k;

		public GameObject r7Y7eQYKUdX3VaJWXExl9PQ;

		public Control0 IoFzxjU9W_HmbUsigktsi3c;

		public _00247ZR6o2lkhBastv3E8uM6Kr_00247Hz4U4IBlxIY7z361c7LycNuiOBpOz0KakG1PyHkhIAQ5S_0024duNArs8iOrHBXUrOUSfSDDUvGs4Nwj8KGLerI02BUwgMRddsbGc8dRA2NSw g0mQ_0024tSzvWAIzvC2JeS9zbk;

		internal void method_0(bool toggled)
		{
			if (toggled && smethod_0(vhcAS28uyig7KSF7oB37W8k))
			{
				smethod_1(vhcAS28uyig7KSF7oB37W8k, bool_0: false);
				smethod_2(r7Y7eQYKUdX3VaJWXExl9PQ, bool_0: false);
			}
			smethod_2(g0mQ_0024tSzvWAIzvC2JeS9zbk.tTCBVE8wPVPHfGDkecHRn4M, toggled);
		}

		internal void method_1(bool toggled)
		{
			if (toggled && IoFzxjU9W_HmbUsigktsi3c.hLxnG9Hq33zU_YUsu_00240_zak)
			{
				IoFzxjU9W_HmbUsigktsi3c.hLxnG9Hq33zU_YUsu_00240_zak = false;
				smethod_2(g0mQ_0024tSzvWAIzvC2JeS9zbk.tTCBVE8wPVPHfGDkecHRn4M, bool_0: false);
			}
		}

		internal static bool smethod_0(Toggle toggle_0)
		{
			return toggle_0.isOn;
		}

		internal static void smethod_1(Toggle toggle_0, bool bool_0)
		{
			toggle_0.isOn = bool_0;
		}

		internal static void smethod_2(GameObject gameObject_0, bool bool_0)
		{
			gameObject_0.SetActive(bool_0);
		}
	}

	private static AssetBundle ReBrVNsm5U1QCVoO3SuLwMC_CDU_0024C9sRhpqoT91OPqmR;

	internal static GameObject _eatlpymFHP2FW6WtcEk26w;

	private static bool XWhHEj7R54U3DnnKtAx38lk;

	public static string String_0
	{
		get
		{
			if (XWhHEj7R54U3DnnKtAx38lk)
			{
				return MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.objPlan_name;
			}
			return MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.objPlan_name;
		}
		set
		{
			if (XWhHEj7R54U3DnnKtAx38lk)
			{
				MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.objPlan_name = value;
			}
			else
			{
				MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.objPlan_name = value;
			}
		}
	}

	public static int UveP8b00PUlmVfaMhyr2jHw
	{
		get
		{
			if (XWhHEj7R54U3DnnKtAx38lk)
			{
				return MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.objPlan_xoff;
			}
			return MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.objPlan_xoff;
		}
		set
		{
			if (XWhHEj7R54U3DnnKtAx38lk)
			{
				MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.objPlan_xoff = value;
			}
			else
			{
				MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.objPlan_xoff = value;
			}
		}
	}

	public static int bYEBUC3V9AL8F1BfUn9TeDQ
	{
		get
		{
			if (XWhHEj7R54U3DnnKtAx38lk)
			{
				return MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.objPlan_yoff;
			}
			return MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.objPlan_yoff;
		}
		set
		{
			if (XWhHEj7R54U3DnnKtAx38lk)
			{
				MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.objPlan_yoff = value;
			}
			else
			{
				MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.objPlan_yoff = value;
			}
		}
	}

	public static int iM3JEr6PTIzQeQfXXIM9lCI
	{
		get
		{
			if (XWhHEj7R54U3DnnKtAx38lk)
			{
				return MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.objPlan_zoff;
			}
			return MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.objPlan_zoff;
		}
		set
		{
			if (XWhHEj7R54U3DnnKtAx38lk)
			{
				MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.objPlan_zoff = value;
			}
			else
			{
				MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.objPlan_zoff = value;
			}
		}
	}

	public static int lptWFZXC_5XxXOiE9y1G9fM
	{
		get
		{
			if (XWhHEj7R54U3DnnKtAx38lk)
			{
				return MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.objPlan_xrot;
			}
			return MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.objPlan_xrot;
		}
		set
		{
			if (XWhHEj7R54U3DnnKtAx38lk)
			{
				MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.objPlan_xrot = value;
			}
			else
			{
				MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.objPlan_xrot = value;
			}
		}
	}

	public static int se1xdwxG_0024GsA0diB9M3Fzm8
	{
		get
		{
			if (XWhHEj7R54U3DnnKtAx38lk)
			{
				return MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.objPlan_yrot;
			}
			return MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.objPlan_yrot;
		}
		set
		{
			if (XWhHEj7R54U3DnnKtAx38lk)
			{
				MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.objPlan_yrot = value;
			}
			else
			{
				MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.objPlan_yrot = value;
			}
		}
	}

	public static int zIVmtI3upt3qSoNlsRdRixM
	{
		get
		{
			if (XWhHEj7R54U3DnnKtAx38lk)
			{
				return MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.objPlan_zrot;
			}
			return MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.objPlan_zrot;
		}
		set
		{
			if (XWhHEj7R54U3DnnKtAx38lk)
			{
				MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.objPlan_zrot = value;
			}
			else
			{
				MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.objPlan_zrot = value;
			}
		}
	}

	public static int Z3zFd_bV0CuH_om_iJXGNWk
	{
		get
		{
			if (XWhHEj7R54U3DnnKtAx38lk)
			{
				return MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.objPlan_scale;
			}
			return MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.objPlan_scale;
		}
		set
		{
			if (XWhHEj7R54U3DnnKtAx38lk)
			{
				MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.objPlan_scale = value;
			}
			else
			{
				MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.objPlan_scale = value;
			}
		}
	}

	public static int mJRRGc1MCKKCdMAZPVtY74Q
	{
		get
		{
			if (XWhHEj7R54U3DnnKtAx38lk)
			{
				return MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.objPlan_opacity;
			}
			return MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.objPlan_opacity;
		}
		set
		{
			if (XWhHEj7R54U3DnnKtAx38lk)
			{
				MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.objPlan_opacity = value;
			}
			else
			{
				MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.objPlan_opacity = value;
			}
		}
	}

	public static int FB_RI_0024pdgWj_C0uU8kVb1xY
	{
		get
		{
			if (XWhHEj7R54U3DnnKtAx38lk)
			{
				return MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.objPlan_color;
			}
			return MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.objPlan_color;
		}
		set
		{
			if (XWhHEj7R54U3DnnKtAx38lk)
			{
				MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.objPlan_color = value;
			}
			else
			{
				MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.objPlan_color = value;
			}
		}
	}

	public static bool C12PB1vA_qYK5Jnetlc9RBSzPXTYhVyiUfwxG1qa0jFH
	{
		get
		{
			if (XWhHEj7R54U3DnnKtAx38lk)
			{
				return MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.objplan_xMirrored;
			}
			return MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.objplan_xMirrored;
		}
		set
		{
			if (XWhHEj7R54U3DnnKtAx38lk)
			{
				MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.objplan_xMirrored = value;
			}
			else
			{
				MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.objplan_xMirrored = value;
			}
		}
	}

	public static bool rDw0tG4RmL5JYcee9fgEboMfKhKeL_Y6jHSvaFofn4de
	{
		get
		{
			if (XWhHEj7R54U3DnnKtAx38lk)
			{
				return MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.objplan_yMirrored;
			}
			return MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.objplan_yMirrored;
		}
		set
		{
			if (XWhHEj7R54U3DnnKtAx38lk)
			{
				MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.objplan_yMirrored = value;
			}
			else
			{
				MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.objplan_yMirrored = value;
			}
		}
	}

	public static bool TRcJx0tr_0bbA4nnDIJPMbD2J3eaaDQBU9fLuH_0024Tvbq8
	{
		get
		{
			if (XWhHEj7R54U3DnnKtAx38lk)
			{
				return MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.objplan_zMirrored;
			}
			return MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.objplan_zMirrored;
		}
		set
		{
			if (XWhHEj7R54U3DnnKtAx38lk)
			{
				MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.objplan_zMirrored = value;
			}
			else
			{
				MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.objplan_zMirrored = value;
			}
		}
	}

	public static mcpd.buildWireframeMode jUXEKVD_hVvLU2_00247ZKrlGNYnl_JXlZjgpo_gKExPiGEA
	{
		get
		{
			if (XWhHEj7R54U3DnnKtAx38lk)
			{
				return MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.objPlan_wireframe;
			}
			return MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.objPlan_wireframe;
		}
		set
		{
			if (XWhHEj7R54U3DnnKtAx38lk)
			{
				MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.objPlan_wireframe = value;
			}
			else
			{
				MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.objPlan_wireframe = value;
			}
		}
	}

	private static void UUiRNMwxRbfk_Fs4cDErRoM()
	{
		if (XWhHEj7R54U3DnnKtAx38lk)
		{
			MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.UUiRNMwxRbfk_Fs4cDErRoM();
		}
		else
		{
			MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.UUiRNMwxRbfk_Fs4cDErRoM();
		}
	}

	[HarmonyPostfix]
	internal static void FeUAVwFbW6wGJJdNimZY9yI(bool BHCKMFDEBBH)
	{
		if (MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.OBJPlan && SceneMan.APNKDLDMACA >= 10)
		{
			G_tAsMqijFlE62CHaw1EnOo(isBuild: true);
		}
	}

	internal static void KKCHLiHZDjufqY4nZJceFC4(bool isBuildmode)
	{
		XWhHEj7R54U3DnnKtAx38lk = isBuildmode;
		if (smethod_0((UnityEngine.Object)ReBrVNsm5U1QCVoO3SuLwMC_CDU_0024C9sRhpqoT91OPqmR, (UnityEngine.Object)null))
		{
			ReBrVNsm5U1QCVoO3SuLwMC_CDU_0024C9sRhpqoT91OPqmR = smethod_1(KH64M3TewhA_Yw2TjbFeRV_kcghAaUSHXuDlWZpBfh_lhm_0024Y_0024No6GYBMPMIyT_0024M8ZQ.zRLfH6irXhnE9uS_0024EJithbo);
		}
		string[] array = new string[5]
		{
			global::_003CModule_003E.smethod_28<string>(3468509317u),
			global::_003CModule_003E.smethod_27<string>(437274090u),
			global::_003CModule_003E.smethod_28<string>(3013209812u),
			global::_003CModule_003E.smethod_26<string>(1120222822u),
			global::_003CModule_003E.smethod_29<string>(361976991u)
		};
		GameObject tTCBVE8wPVPHfGDkecHRn4M;
		if (!isBuildmode)
		{
			Toggle vhcAS28uyig7KSF7oB37W8k = GameObject.Find(global::_003CModule_003E.smethod_26<string>(3103631283u)).smethod_0(global::_003CModule_003E.smethod_25<string>(2888467563u)).smethod_0(global::_003CModule_003E.smethod_26<string>(1649307930u))
				.GetComponent<Toggle>();
			GameObject r7Y7eQYKUdX3VaJWXExl9PQ = GameObject.Find(global::_003CModule_003E.smethod_27<string>(862174272u)).smethod_0(global::_003CModule_003E.smethod_29<string>(2575699905u));
			tTCBVE8wPVPHfGDkecHRn4M = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.qSsYOsEQb7x452a9Y45dVEk(new Vector3(0f, -300f), new Vector2(250f, 600f), r7Y7eQYKUdX3VaJWXExl9PQ.transform.parent);
			Control0 IoFzxjU9W_HmbUsigktsi3c = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.uEsWMK_pFkCY_0024M5zt8zLsQk(global::_003CModule_003E.smethod_28<string>(2047783079u), new Vector3(65f, 340f), global::_003CModule_003E.smethod_26<string>(4157342729u), GameObject.Find(global::_003CModule_003E.smethod_26<string>(3103631283u)).smethod_0(global::_003CModule_003E.smethod_25<string>(2888467563u)).transform, resetGroup: true, delegate(bool toggled)
			{
				if (toggled && _N0FgnlP8K15hFaWm38CRHuMKwOx1RWvYklF5mKmV1rAqWhAzf0c_3p4kbzR1EXr4tY02xmG_DmiqCeNYVh_0024PADU0PDPjeZLZgbRqax_IVRAL9hxIhCo59BiLk1P7jWGjw.smethod_0(vhcAS28uyig7KSF7oB37W8k))
				{
					_N0FgnlP8K15hFaWm38CRHuMKwOx1RWvYklF5mKmV1rAqWhAzf0c_3p4kbzR1EXr4tY02xmG_DmiqCeNYVh_0024PADU0PDPjeZLZgbRqax_IVRAL9hxIhCo59BiLk1P7jWGjw.smethod_1(vhcAS28uyig7KSF7oB37W8k, bool_0: false);
					_N0FgnlP8K15hFaWm38CRHuMKwOx1RWvYklF5mKmV1rAqWhAzf0c_3p4kbzR1EXr4tY02xmG_DmiqCeNYVh_0024PADU0PDPjeZLZgbRqax_IVRAL9hxIhCo59BiLk1P7jWGjw.smethod_2(r7Y7eQYKUdX3VaJWXExl9PQ, bool_0: false);
				}
				_N0FgnlP8K15hFaWm38CRHuMKwOx1RWvYklF5mKmV1rAqWhAzf0c_3p4kbzR1EXr4tY02xmG_DmiqCeNYVh_0024PADU0PDPjeZLZgbRqax_IVRAL9hxIhCo59BiLk1P7jWGjw.smethod_2(tTCBVE8wPVPHfGDkecHRn4M, toggled);
			});
			vhcAS28uyig7KSF7oB37W8k.onValueChanged.AddListener(delegate(bool toggled)
			{
				if (toggled && IoFzxjU9W_HmbUsigktsi3c.hLxnG9Hq33zU_YUsu_00240_zak)
				{
					IoFzxjU9W_HmbUsigktsi3c.hLxnG9Hq33zU_YUsu_00240_zak = false;
					_N0FgnlP8K15hFaWm38CRHuMKwOx1RWvYklF5mKmV1rAqWhAzf0c_3p4kbzR1EXr4tY02xmG_DmiqCeNYVh_0024PADU0PDPjeZLZgbRqax_IVRAL9hxIhCo59BiLk1P7jWGjw.smethod_2(tTCBVE8wPVPHfGDkecHRn4M, bool_0: false);
				}
			});
			IoFzxjU9W_HmbUsigktsi3c.transform.parent.GetComponent<RectTransform>().sizeDelta += new Vector2(0f, 40f);
			for (int num = 0; num < IoFzxjU9W_HmbUsigktsi3c.transform.parent.childCount; num++)
			{
				IoFzxjU9W_HmbUsigktsi3c.transform.parent.GetChild(num).GetComponent<RectTransform>().localPosition -= new Vector3(0f, 30f);
			}
		}
		else
		{
			if (MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4 == null)
			{
				MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4 = new mcpd();
			}
			Transform transform = smethod_3(smethod_2(global::_003CModule_003E.smethod_29<string>(618767629u)).smethod_0(global::_003CModule_003E.smethod_28<string>(539440536u)));
			transform.localPosition = smethod_4(transform) + new Vector3(0f, 15f);
			GameObject.Find(global::_003CModule_003E.smethod_26<string>(3103631283u)).smethod_0(global::_003CModule_003E.smethod_26<string>(2967770799u)).transform.localPosition += new Vector3(0f, 15f);
			GameObject.Find(global::_003CModule_003E.smethod_25<string>(806627754u)).smethod_0(global::_003CModule_003E.smethod_27<string>(1163186437u)).transform.localPosition += new Vector3(0f, 30f);
			GameObject.Find(global::_003CModule_003E.smethod_27<string>(862174272u)).smethod_0(global::_003CModule_003E.smethod_26<string>(966109520u)).transform.localPosition += new Vector3(0f, -7f);
			GameObject.Find(global::_003CModule_003E.smethod_25<string>(806627754u)).smethod_0(global::_003CModule_003E.smethod_29<string>(3456424149u)).transform.localPosition += new Vector3(0f, -7f);
			GameObject.Find(global::_003CModule_003E.smethod_25<string>(806627754u)).smethod_0(global::_003CModule_003E.smethod_28<string>(2861393908u)).transform.localPosition += new Vector3(0f, -7f);
			lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw2 = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.P3tLroX6fcPfQha_0024JdbpYXg(global::_003CModule_003E.smethod_29<string>(1541517866u), new Vector3(0f, -180f), global::_003CModule_003E.smethod_29<string>(3845221050u), null, GameObject.Find(global::_003CModule_003E.smethod_25<string>(806627754u)).smethod_0(global::_003CModule_003E.smethod_25<string>(1743464837u)).transform.parent);
			tTCBVE8wPVPHfGDkecHRn4M = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.qSsYOsEQb7x452a9Y45dVEk(new Vector3(245f, -10f), new Vector2(250f, 600f), lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw2.transform.parent);
			lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw2.t2iJT_tBPyB6QRMBLAdXYUs(delegate
			{
				_00247ZR6o2lkhBastv3E8uM6Kr_00247Hz4U4IBlxIY7z361c7LycNuiOBpOz0KakG1PyHkhIAQ5S_0024duNArs8iOrHBXUrOUSfSDDUvGs4Nwj8KGLerI02BUwgMRddsbGc8dRA2NSw.smethod_1(tTCBVE8wPVPHfGDkecHRn4M, !_00247ZR6o2lkhBastv3E8uM6Kr_00247Hz4U4IBlxIY7z361c7LycNuiOBpOz0KakG1PyHkhIAQ5S_0024duNArs8iOrHBXUrOUSfSDDUvGs4Nwj8KGLerI02BUwgMRddsbGc8dRA2NSw.smethod_0(tTCBVE8wPVPHfGDkecHRn4M));
			});
		}
		int num2 = 0;
		int num3 = (isBuildmode ? 240 : 550);
		ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ obj = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.bqYYMQnP2SDqYH85wmN_0024evI(global::_003CModule_003E.smethod_25<string>(4199999753u), new Vector3(0f, num3), global::_003CModule_003E.smethod_29<string>(2893797393u), global::_003CModule_003E.smethod_27<string>(530335440u), tTCBVE8wPVPHfGDkecHRn4M.transform);
		num3 -= 60;
		obj.pZEKY5TzLd4S3z2lXESoRnw = String_0;
		if (isBuildmode)
		{
			Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.tjX92nnz9Ioc_0024izMOOLq4fI(global::_003CModule_003E.smethod_26<string>(4215447434u), global::_003CModule_003E.smethod_29<string>(2019271301u), new Vector3(num2, num3), -500, 500, UveP8b00PUlmVfaMhyr2jHw, delegate(float value)
			{
				UveP8b00PUlmVfaMhyr2jHw = (int)value;
				UUiRNMwxRbfk_Fs4cDErRoM();
				G_tAsMqijFlE62CHaw1EnOo(isBuildmode);
			}, tTCBVE8wPVPHfGDkecHRn4M.transform);
			num3 -= 50;
			Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.tjX92nnz9Ioc_0024izMOOLq4fI(global::_003CModule_003E.smethod_28<string>(1935642949u), global::_003CModule_003E.smethod_29<string>(991489815u), new Vector3(num2, num3), -500, 500, bYEBUC3V9AL8F1BfUn9TeDQ, delegate(float value)
			{
				bYEBUC3V9AL8F1BfUn9TeDQ = (int)value;
				UUiRNMwxRbfk_Fs4cDErRoM();
				G_tAsMqijFlE62CHaw1EnOo(isBuildmode);
			}, tTCBVE8wPVPHfGDkecHRn4M.transform);
			num3 -= 50;
			Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.tjX92nnz9Ioc_0024izMOOLq4fI(global::_003CModule_003E.smethod_26<string>(241247576u), global::_003CModule_003E.smethod_27<string>(4238982414u), new Vector3(num2, num3), -500, 500, iM3JEr6PTIzQeQfXXIM9lCI, delegate(float value)
			{
				iM3JEr6PTIzQeQfXXIM9lCI = (int)value;
				UUiRNMwxRbfk_Fs4cDErRoM();
				G_tAsMqijFlE62CHaw1EnOo(isBuildmode);
			}, tTCBVE8wPVPHfGDkecHRn4M.transform);
		}
		else
		{
			ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ obj2 = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.bqYYMQnP2SDqYH85wmN_0024evI(global::_003CModule_003E.smethod_29<string>(978891110u), new Vector3(num2, num3 + 10), global::_003CModule_003E.smethod_25<string>(2619818674u), tTCBVE8wPVPHfGDkecHRn4M.transform, UveP8b00PUlmVfaMhyr2jHw.ToString());
			Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.YU_pwpP3pKH76IHZTea_SXk(global::_003CModule_003E.smethod_29<string>(3218295434u), new Vector3(-100f, num3 + 10), global::_003CModule_003E.smethod_29<string>(1786047134u), tTCBVE8wPVPHfGDkecHRn4M.transform, rmOutline: false, -1, FontStyle.Normal, TextAnchor.MiddleCenter, default(Color), resizeRect: true);
			num3 -= 50;
			ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ2 = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.bqYYMQnP2SDqYH85wmN_0024evI(global::_003CModule_003E.smethod_29<string>(838234421u), new Vector3(num2, num3 + 10), global::_003CModule_003E.smethod_25<string>(2356051532u), tTCBVE8wPVPHfGDkecHRn4M.transform, bYEBUC3V9AL8F1BfUn9TeDQ.ToString());
			Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.YU_pwpP3pKH76IHZTea_SXk(global::_003CModule_003E.smethod_25<string>(4026800863u), new Vector3(-100f, num3 + 10), global::_003CModule_003E.smethod_26<string>(2594192902u), tTCBVE8wPVPHfGDkecHRn4M.transform, rmOutline: false, -1, FontStyle.Normal, TextAnchor.MiddleCenter, default(Color), resizeRect: true);
			num3 -= 50;
			ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ3 = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.bqYYMQnP2SDqYH85wmN_0024evI(global::_003CModule_003E.smethod_27<string>(1875122673u), new Vector3(num2, num3 + 10), global::_003CModule_003E.smethod_29<string>(4105420231u), tTCBVE8wPVPHfGDkecHRn4M.transform, iM3JEr6PTIzQeQfXXIM9lCI.ToString());
			Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.YU_pwpP3pKH76IHZTea_SXk(global::_003CModule_003E.smethod_27<string>(1782061323u), new Vector3(-100f, num3 + 10), global::_003CModule_003E.smethod_29<string>(3278983350u), tTCBVE8wPVPHfGDkecHRn4M.transform, rmOutline: false, -1, FontStyle.Normal, TextAnchor.MiddleCenter, default(Color), resizeRect: true);
			obj2.BSdnl9DYm6Rd4cVhJ555c_A.characterValidation = InputField.CharacterValidation.Integer;
			ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ2.BSdnl9DYm6Rd4cVhJ555c_A.characterValidation = InputField.CharacterValidation.Integer;
			ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ3.BSdnl9DYm6Rd4cVhJ555c_A.characterValidation = InputField.CharacterValidation.Integer;
			obj2.BSdnl9DYm6Rd4cVhJ555c_A.onValueChanged.AddListener(delegate(string value)
			{
				if (int.TryParse(value, out var result))
				{
					UveP8b00PUlmVfaMhyr2jHw = result;
					UUiRNMwxRbfk_Fs4cDErRoM();
					G_tAsMqijFlE62CHaw1EnOo(isBuildmode);
				}
			});
			ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ2.BSdnl9DYm6Rd4cVhJ555c_A.onValueChanged.AddListener(delegate(string value)
			{
				if (int.TryParse(value, out var result))
				{
					bYEBUC3V9AL8F1BfUn9TeDQ = result;
					UUiRNMwxRbfk_Fs4cDErRoM();
					G_tAsMqijFlE62CHaw1EnOo(isBuildmode);
				}
			});
			ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ3.BSdnl9DYm6Rd4cVhJ555c_A.onValueChanged.AddListener(delegate(string value)
			{
				if (int.TryParse(value, out var result))
				{
					iM3JEr6PTIzQeQfXXIM9lCI = result;
					UUiRNMwxRbfk_Fs4cDErRoM();
					G_tAsMqijFlE62CHaw1EnOo(isBuildmode);
				}
			});
		}
		num3 -= 50;
		Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.tjX92nnz9Ioc_0024izMOOLq4fI(global::_003CModule_003E.smethod_26<string>(603678548u), global::_003CModule_003E.smethod_25<string>(2930416976u), new Vector3(num2, num3), -90, 90, lptWFZXC_5XxXOiE9y1G9fM, delegate(float value)
		{
			lptWFZXC_5XxXOiE9y1G9fM = (int)value;
			UUiRNMwxRbfk_Fs4cDErRoM();
			G_tAsMqijFlE62CHaw1EnOo(isBuildmode);
		}, tTCBVE8wPVPHfGDkecHRn4M.transform);
		num3 -= 50;
		Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.tjX92nnz9Ioc_0024izMOOLq4fI(global::_003CModule_003E.smethod_25<string>(432566741u), global::_003CModule_003E.smethod_27<string>(1056148976u), new Vector3(num2, num3), -90, 90, se1xdwxG_0024GsA0diB9M3Fzm8, delegate(float value)
		{
			se1xdwxG_0024GsA0diB9M3Fzm8 = (int)value;
			UUiRNMwxRbfk_Fs4cDErRoM();
			G_tAsMqijFlE62CHaw1EnOo(isBuildmode);
		}, tTCBVE8wPVPHfGDkecHRn4M.transform);
		num3 -= 50;
		Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.tjX92nnz9Ioc_0024izMOOLq4fI(global::_003CModule_003E.smethod_27<string>(1195741001u), global::_003CModule_003E.smethod_29<string>(1877118607u), new Vector3(num2, num3), -180, 180, zIVmtI3upt3qSoNlsRdRixM, delegate(float value)
		{
			zIVmtI3upt3qSoNlsRdRixM = (int)value;
			UUiRNMwxRbfk_Fs4cDErRoM();
			G_tAsMqijFlE62CHaw1EnOo(isBuildmode);
		}, tTCBVE8wPVPHfGDkecHRn4M.transform);
		num3 -= 50;
		Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.tjX92nnz9Ioc_0024izMOOLq4fI(global::_003CModule_003E.smethod_25<string>(3688340697u), global::_003CModule_003E.smethod_28<string>(634946647u), new Vector3(num2, num3), 5, 2000, Z3zFd_bV0CuH_om_iJXGNWk, delegate(float value)
		{
			Z3zFd_bV0CuH_om_iJXGNWk = (int)value;
			UUiRNMwxRbfk_Fs4cDErRoM();
			G_tAsMqijFlE62CHaw1EnOo(isBuildmode);
		}, tTCBVE8wPVPHfGDkecHRn4M.transform);
		num3 -= 50;
		Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.tjX92nnz9Ioc_0024izMOOLq4fI(global::_003CModule_003E.smethod_28<string>(3256085617u), global::_003CModule_003E.smethod_29<string>(1595805229u), new Vector3(num2, num3), 0, 100, mJRRGc1MCKKCdMAZPVtY74Q, delegate(float value)
		{
			mJRRGc1MCKKCdMAZPVtY74Q = (int)value;
			UUiRNMwxRbfk_Fs4cDErRoM();
			G_tAsMqijFlE62CHaw1EnOo(isBuildmode);
		}, tTCBVE8wPVPHfGDkecHRn4M.transform);
		num3 -= 50;
		Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.tjX92nnz9Ioc_0024izMOOLq4fI(global::_003CModule_003E.smethod_29<string>(708680432u), global::_003CModule_003E.smethod_27<string>(2649932885u), new Vector3(num2, num3), 0, 100, FB_RI_0024pdgWj_C0uU8kVb1xY, delegate(float value)
		{
			FB_RI_0024pdgWj_C0uU8kVb1xY = (int)value;
			UUiRNMwxRbfk_Fs4cDErRoM();
			G_tAsMqijFlE62CHaw1EnOo(isBuildmode);
		}, tTCBVE8wPVPHfGDkecHRn4M.transform);
		num3 -= 50;
		Control0 control = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.uEsWMK_pFkCY_0024M5zt8zLsQk(global::_003CModule_003E.smethod_26<string>(1251483841u), new Vector3(-80f, num3), global::_003CModule_003E.smethod_26<string>(2736600846u), tTCBVE8wPVPHfGDkecHRn4M.transform, resetGroup: true, delegate(bool active)
		{
			C12PB1vA_qYK5Jnetlc9RBSzPXTYhVyiUfwxG1qa0jFH = active;
			UUiRNMwxRbfk_Fs4cDErRoM();
			G_tAsMqijFlE62CHaw1EnOo(isBuildmode);
		});
		control.UzVS61irgJn5Pnqwx0lThng(new Vector2(70f, 35f));
		control.hLxnG9Hq33zU_YUsu_00240_zak = C12PB1vA_qYK5Jnetlc9RBSzPXTYhVyiUfwxG1qa0jFH;
		Control0 control2 = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.uEsWMK_pFkCY_0024M5zt8zLsQk(global::_003CModule_003E.smethod_27<string>(3126848556u), new Vector3(0f, num3), global::_003CModule_003E.smethod_28<string>(144897034u), tTCBVE8wPVPHfGDkecHRn4M.transform, resetGroup: true, delegate(bool active)
		{
			rDw0tG4RmL5JYcee9fgEboMfKhKeL_Y6jHSvaFofn4de = active;
			UUiRNMwxRbfk_Fs4cDErRoM();
			G_tAsMqijFlE62CHaw1EnOo(isBuildmode);
		});
		control2.UzVS61irgJn5Pnqwx0lThng(new Vector2(70f, 35f));
		control2.hLxnG9Hq33zU_YUsu_00240_zak = rDw0tG4RmL5JYcee9fgEboMfKhKeL_Y6jHSvaFofn4de;
		Control0 control3 = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.uEsWMK_pFkCY_0024M5zt8zLsQk(global::_003CModule_003E.smethod_26<string>(2172122621u), new Vector3(80f, num3), global::_003CModule_003E.smethod_29<string>(1920303270u), tTCBVE8wPVPHfGDkecHRn4M.transform, resetGroup: true, delegate(bool active)
		{
			TRcJx0tr_0bbA4nnDIJPMbD2J3eaaDQBU9fLuH_0024Tvbq8 = active;
			UUiRNMwxRbfk_Fs4cDErRoM();
			G_tAsMqijFlE62CHaw1EnOo(isBuildmode);
		});
		control3.UzVS61irgJn5Pnqwx0lThng(new Vector2(70f, 35f));
		control3.hLxnG9Hq33zU_YUsu_00240_zak = TRcJx0tr_0bbA4nnDIJPMbD2J3eaaDQBU9fLuH_0024Tvbq8;
		ListController listController;
		if (isBuildmode)
		{
			listController = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.nN2N4qjnQLwFOaONUPeRAdg(global::_003CModule_003E.smethod_29<string>(1779646581u), new Vector3(240f, 0f), array, tTCBVE8wPVPHfGDkecHRn4M.transform);
		}
		else
		{
			listController = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.nN2N4qjnQLwFOaONUPeRAdg(global::_003CModule_003E.smethod_25<string>(3647025378u), global::_003CModule_003E.smethod_25<string>(4215874981u), new Vector3(240f, 300f), array, tTCBVE8wPVPHfGDkecHRn4M.transform);
		}
		listController.SetSelectedItem(array[(int)jUXEKVD_hVvLU2_00247ZKrlGNYnl_JXlZjgpo_gKExPiGEA]);
		listController.gameObject.AddComponent<AmMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F>().U_0024eIoX6e3N9Ag_us_EcGHBI(delegate(AmMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F me)
		{
			if (!me.AqTKGFxfR1r6eAzrvm4_0024bck(global::_003CModule_003E.smethod_29<string>(750638958u)))
			{
				me.clf0br4v0HxmaJWIcm_0024_0024GUg(global::_003CModule_003E.smethod_25<string>(1381191829u), _00247ZR6o2lkhBastv3E8uM6Kr_00247Hz4U4IBlxIY7z361c7LycNuiOBpOz0KakG1PyHkhIAQ5S_0024duNArs8iOrHBXUrOUSfSDDUvGs4Nwj8KGLerI02BUwgMRddsbGc8dRA2NSw.smethod_2(listController));
			}
			else if (_00247ZR6o2lkhBastv3E8uM6Kr_00247Hz4U4IBlxIY7z361c7LycNuiOBpOz0KakG1PyHkhIAQ5S_0024duNArs8iOrHBXUrOUSfSDDUvGs4Nwj8KGLerI02BUwgMRddsbGc8dRA2NSw.smethod_3(me.hpiqzm2jQTswCo32f7jvrQ4<string>(global::_003CModule_003E.smethod_26<string>(3715485026u)), _00247ZR6o2lkhBastv3E8uM6Kr_00247Hz4U4IBlxIY7z361c7LycNuiOBpOz0KakG1PyHkhIAQ5S_0024duNArs8iOrHBXUrOUSfSDDUvGs4Nwj8KGLerI02BUwgMRddsbGc8dRA2NSw.smethod_2(listController)))
			{
				me.clf0br4v0HxmaJWIcm_0024_0024GUg(global::_003CModule_003E.smethod_26<string>(3715485026u), _00247ZR6o2lkhBastv3E8uM6Kr_00247Hz4U4IBlxIY7z361c7LycNuiOBpOz0KakG1PyHkhIAQ5S_0024duNArs8iOrHBXUrOUSfSDDUvGs4Nwj8KGLerI02BUwgMRddsbGc8dRA2NSw.smethod_2(listController));
				jUXEKVD_hVvLU2_00247ZKrlGNYnl_JXlZjgpo_gKExPiGEA = (mcpd.buildWireframeMode)Array.FindIndex(array, (string item) => _00247ZR6o2lkhBastv3E8uM6Kr_00247Hz4U4IBlxIY7z361c7LycNuiOBpOz0KakG1PyHkhIAQ5S_0024duNArs8iOrHBXUrOUSfSDDUvGs4Nwj8KGLerI02BUwgMRddsbGc8dRA2NSw.smethod_4(item, _00247ZR6o2lkhBastv3E8uM6Kr_00247Hz4U4IBlxIY7z361c7LycNuiOBpOz0KakG1PyHkhIAQ5S_0024duNArs8iOrHBXUrOUSfSDDUvGs4Nwj8KGLerI02BUwgMRddsbGc8dRA2NSw.smethod_2(listController)));
				UUiRNMwxRbfk_Fs4cDErRoM();
				G_tAsMqijFlE62CHaw1EnOo(isBuildmode);
			}
		});
		if (isBuildmode)
		{
			Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.YU_pwpP3pKH76IHZTea_SXk(global::_003CModule_003E.smethod_25<string>(3910792520u), new Vector3(0f, 35f), global::_003CModule_003E.smethod_29<string>(3413239486u), listController.transform, rmOutline: false, -1, FontStyle.Normal, TextAnchor.LowerCenter);
		}
		obj.JNMaMdWdD3fzh8iVBUwSGz4 = delegate(string text)
		{
			String_0 = text;
			UUiRNMwxRbfk_Fs4cDErRoM();
			if (_00247ZR6o2lkhBastv3E8uM6Kr_00247Hz4U4IBlxIY7z361c7LycNuiOBpOz0KakG1PyHkhIAQ5S_0024duNArs8iOrHBXUrOUSfSDDUvGs4Nwj8KGLerI02BUwgMRddsbGc8dRA2NSw.smethod_5((UnityEngine.Object)_eatlpymFHP2FW6WtcEk26w, (UnityEngine.Object)null))
			{
				_00247ZR6o2lkhBastv3E8uM6Kr_00247Hz4U4IBlxIY7z361c7LycNuiOBpOz0KakG1PyHkhIAQ5S_0024duNArs8iOrHBXUrOUSfSDDUvGs4Nwj8KGLerI02BUwgMRddsbGc8dRA2NSw.smethod_6((UnityEngine.Object)_eatlpymFHP2FW6WtcEk26w);
			}
			WsRTClbFOqctTzQQJXwCPNU(text, isBuildmode);
		};
		WsRTClbFOqctTzQQJXwCPNU(String_0, isBuildmode);
		tTCBVE8wPVPHfGDkecHRn4M.SetActive(value: false);
	}

	internal static void G_tAsMqijFlE62CHaw1EnOo(bool isBuild)
	{
		if (smethod_0((UnityEngine.Object)_eatlpymFHP2FW6WtcEk26w, (UnityEngine.Object)null))
		{
			return;
		}
		smethod_6(_eatlpymFHP2FW6WtcEk26w, !isBuild || smethod_5(smethod_2(global::_003CModule_003E.smethod_29<string>(3131926108u)).smethod_0(global::_003CModule_003E.smethod_27<string>(130137818u))) != 0);
		smethod_3(_eatlpymFHP2FW6WtcEk26w).position = new Vector3((float)UveP8b00PUlmVfaMhyr2jHw / 10f, (float)bYEBUC3V9AL8F1BfUn9TeDQ / 10f + 50f, (float)iM3JEr6PTIzQeQfXXIM9lCI / 10f);
		_eatlpymFHP2FW6WtcEk26w.transform.rotation = Quaternion.Euler(lptWFZXC_5XxXOiE9y1G9fM, se1xdwxG_0024GsA0diB9M3Fzm8, zIVmtI3upt3qSoNlsRdRixM);
		_eatlpymFHP2FW6WtcEk26w.transform.localScale = new Vector3((float)Z3zFd_bV0CuH_om_iJXGNWk / (C12PB1vA_qYK5Jnetlc9RBSzPXTYhVyiUfwxG1qa0jFH ? (-100f) : 100f), (float)Z3zFd_bV0CuH_om_iJXGNWk / (rDw0tG4RmL5JYcee9fgEboMfKhKeL_Y6jHSvaFofn4de ? (-100f) : 100f), (float)Z3zFd_bV0CuH_om_iJXGNWk / (TRcJx0tr_0bbA4nnDIJPMbD2J3eaaDQBU9fLuH_0024Tvbq8 ? (-100f) : 100f));
		Color color_ = Color.HSVToRGB((float)FB_RI_0024pdgWj_C0uU8kVb1xY / 100f, 1f, 1f);
		color_.a = (float)mJRRGc1MCKKCdMAZPVtY74Q / 100f;
		MeshRenderer[] componentsInChildren = _eatlpymFHP2FW6WtcEk26w.GetComponentsInChildren<MeshRenderer>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			Material material = componentsInChildren[i].material;
			material.mainTexture = H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.ZCzmS7RxXq2MC4IAXH5LMuk(color_);
			if (jUXEKVD_hVvLU2_00247ZKrlGNYnl_JXlZjgpo_gKExPiGEA != mcpd.buildWireframeMode.OFF)
			{
				if (jUXEKVD_hVvLU2_00247ZKrlGNYnl_JXlZjgpo_gKExPiGEA == mcpd.buildWireframeMode.SOLID)
				{
					material.shader = ReBrVNsm5U1QCVoO3SuLwMC_CDU_0024C9sRhpqoT91OPqmR.LoadAsset<Shader>(global::_003CModule_003E.smethod_26<string>(366238178u));
				}
				else if (jUXEKVD_hVvLU2_00247ZKrlGNYnl_JXlZjgpo_gKExPiGEA == mcpd.buildWireframeMode.TRANSPARENT)
				{
					material.shader = ReBrVNsm5U1QCVoO3SuLwMC_CDU_0024C9sRhpqoT91OPqmR.LoadAsset<Shader>(global::_003CModule_003E.smethod_27<string>(702482029u));
				}
				else if (jUXEKVD_hVvLU2_00247ZKrlGNYnl_JXlZjgpo_gKExPiGEA != mcpd.buildWireframeMode.SOLIDTRANSPARENT)
				{
					if (jUXEKVD_hVvLU2_00247ZKrlGNYnl_JXlZjgpo_gKExPiGEA == mcpd.buildWireframeMode.QUADS)
					{
						material.shader = ReBrVNsm5U1QCVoO3SuLwMC_CDU_0024C9sRhpqoT91OPqmR.LoadAsset<Shader>(global::_003CModule_003E.smethod_29<string>(2115247322u));
					}
				}
				else
				{
					material.shader = ReBrVNsm5U1QCVoO3SuLwMC_CDU_0024C9sRhpqoT91OPqmR.LoadAsset<Shader>(global::_003CModule_003E.smethod_28<string>(3999716774u));
				}
			}
			else
			{
				material.shader = Shader.Find(global::_003CModule_003E.smethod_28<string>(726895790u));
			}
			material.SetColor(global::_003CModule_003E.smethod_27<string>(3978376817u), new Color(color_.r, color_.g, color_.b, 1f));
			material.SetColor(global::_003CModule_003E.smethod_26<string>(2358564179u), new Color(0f, 0f, 0f, color_.a));
			material.SetInt(global::_003CModule_003E.smethod_25<string>(3538323u), (int)((float)mJRRGc1MCKKCdMAZPVtY74Q / 100f * 700f) + 100);
			material.SetFloat(global::_003CModule_003E.smethod_28<string>(2527606409u), (float)mJRRGc1MCKKCdMAZPVtY74Q / 100f * 0.5f);
			material.SetColor(global::_003CModule_003E.smethod_27<string>(2340429423u), new Color(color_.r, color_.g, color_.b, 1f));
			material.SetColor(global::_003CModule_003E.smethod_27<string>(795543379u), new Color(color_.r, color_.g, color_.b, 1f));
		}
	}

	internal static void WsRTClbFOqctTzQQJXwCPNU(string text, bool isBuildmode)
	{
		try
		{
			if (smethod_7((UnityEngine.Object)_eatlpymFHP2FW6WtcEk26w, (UnityEngine.Object)null))
			{
				smethod_8((UnityEngine.Object)_eatlpymFHP2FW6WtcEk26w);
			}
			string text2 = smethod_9(JKGKJLLFMLE.LAOHLAOMCPN, global::_003CModule_003E.smethod_26<string>(1453531094u), text, global::_003CModule_003E.smethod_26<string>(3917532279u));
			if (!smethod_10(text2))
			{
				return;
			}
			_eatlpymFHP2FW6WtcEk26w = smethod_11(global::_003CModule_003E.smethod_27<string>(1544805606u));
			mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(smethod_12(global::_003CModule_003E.smethod_29<string>(3931455442u), text2));
			ObjFormatReader objFormatReader = new ObjFormatReader();
			StlFormatWriter stlFormatWriter = new StlFormatWriter();
			MemoryStream memoryStream = smethod_13();
			stlFormatWriter.WriteToStreamNodispose(objFormatReader.ReadFromStream(smethod_14(text2, FileMode.Open)), memoryStream);
			smethod_15((Stream)memoryStream, 0L, SeekOrigin.Begin);
			Mesh[] array = STL_ImportScript.Import(memoryStream);
			if (array == null)
			{
				MPatchr.ShowDebugMsg(global::_003CModule_003E.smethod_26<string>(1588833145u));
				return;
			}
			mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_29<string>(988767673u) + array.Length + global::_003CModule_003E.smethod_29<string>(2622360578u));
			int num = 1;
			Mesh[] array2 = array;
			foreach (Mesh mesh in array2)
			{
				GameObject gameObject = new GameObject(global::_003CModule_003E.smethod_25<string>(3087740638u) + num);
				mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_25<string>(589890403u) + num);
				gameObject.transform.parent = _eatlpymFHP2FW6WtcEk26w.transform;
				MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
				gameObject.AddComponent<MeshFilter>().mesh = mesh;
				gameObject.layer = 15;
				meshRenderer.material = new Material(Shader.Find(global::_003CModule_003E.smethod_28<string>(726895790u)))
				{
					mainTexture = H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.ZCzmS7RxXq2MC4IAXH5LMuk(new Color(1f, 0f, 0f, 0.5f))
				};
				num++;
			}
			_eatlpymFHP2FW6WtcEk26w.transform.position = Vector3.zero;
			_eatlpymFHP2FW6WtcEk26w.layer = 15;
			if (isBuildmode)
			{
				GameObject.Find(global::_003CModule_003E.smethod_28<string>(3787293074u)).smethod_0(global::_003CModule_003E.smethod_25<string>(1981791888u)).layer = 31;
				GameObject.Find(global::_003CModule_003E.smethod_25<string>(4042676091u)).smethod_0(global::_003CModule_003E.smethod_29<string>(2200390511u)).layer = 31;
				GameObject.Find(global::_003CModule_003E.smethod_27<string>(2061245373u)).smethod_0(global::_003CModule_003E.smethod_28<string>(1540951240u)).layer = 31;
			}
			G_tAsMqijFlE62CHaw1EnOo(isBuildmode);
		}
		catch (Exception ex)
		{
			MPatchr.ShowDebugMsg(global::_003CModule_003E.smethod_25<string>(2650774606u));
			mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_25<string>(812342226u) + ex.Message + global::_003CModule_003E.smethod_28<string>(119039346u) + ex.StackTrace);
		}
	}

	internal static bool smethod_0(UnityEngine.Object object_0, UnityEngine.Object object_1)
	{
		return object_0 == object_1;
	}

	internal static AssetBundle smethod_1(byte[] byte_0)
	{
		return AssetBundle.LoadFromMemory(byte_0);
	}

	internal static GameObject smethod_2(string string_0)
	{
		return GameObject.Find(string_0);
	}

	internal static Transform smethod_3(GameObject gameObject_0)
	{
		return gameObject_0.transform;
	}

	internal static Vector3 smethod_4(Transform transform_0)
	{
		return transform_0.localPosition;
	}

	internal static int smethod_5(GameObject gameObject_0)
	{
		return gameObject_0.layer;
	}

	internal static void smethod_6(GameObject gameObject_0, bool bool_0)
	{
		gameObject_0.SetActive(bool_0);
	}

	internal static bool smethod_7(UnityEngine.Object object_0, UnityEngine.Object object_1)
	{
		return object_0 != object_1;
	}

	internal static void smethod_8(UnityEngine.Object object_0)
	{
		UnityEngine.Object.Destroy(object_0);
	}

	internal static string smethod_9(string string_0, string string_1, string string_2, string string_3)
	{
		return string_0 + string_1 + string_2 + string_3;
	}

	internal static bool smethod_10(string string_0)
	{
		return File.Exists(string_0);
	}

	internal static GameObject smethod_11(string string_0)
	{
		return new GameObject(string_0);
	}

	internal static string smethod_12(string string_0, string string_1)
	{
		return string_0 + string_1;
	}

	internal static MemoryStream smethod_13()
	{
		return new MemoryStream();
	}

	internal static FileStream smethod_14(string string_0, FileMode fileMode_0)
	{
		return File.Open(string_0, fileMode_0);
	}

	internal static long smethod_15(Stream stream_0, long long_0, SeekOrigin seekOrigin_0)
	{
		return stream_0.Seek(long_0, seekOrigin_0);
	}
}
