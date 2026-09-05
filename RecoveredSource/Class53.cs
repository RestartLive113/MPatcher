using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using HarmonyLib;
using MPatchrMain;
using UnityEngine;
using UnityEngine.SceneManagement;

[HarmonyPatch(typeof(Arena))]
[HarmonyPatch("LoadWorld")]
internal class Class53
{
	[CompilerGenerated]
	private sealed class MBe5qZVM8bV6BRQFLUrD78oCAQuN4uSWlBiBtiId9puBOyOCv1NkMQymq_0024G2u_0024krwo7vw_wfQ_0024R4lC6qlEYco_5Q2OPLFuzjtsjpltO11amqvWFHI3fxPYZKK6h5Xr9tE04eKFe8j8YupYBYs5OUKWU
	{
		public WWW CxC16FoGnjbIzP7FXZpk2gA;

		public Texture2D p0juANsEAhC8ssiPbRj1_3g;

		internal void _O86Pj4hxAo_HFKk_0024jMS2kbPwNj2Cy3J3jNiv0PUUZzK()
		{
			if (CxC16FoGnjbIzP7FXZpk2gA == null)
			{
				MPatchr.Oo1ruUb5AAVwJnBX0QWSyZhuNBfTSYAlNOATacVohQ5n(global::_003CModule_003E.smethod_27<string>(3029184864u));
			}
			GUI.DrawTexture(new Rect(0f, 10f, 100f * smethod_0(CxC16FoGnjbIzP7FXZpk2gA), 10f), p0juANsEAhC8ssiPbRj1_3g);
			if (CxC16FoGnjbIzP7FXZpk2gA.isDone || !string.IsNullOrEmpty(CxC16FoGnjbIzP7FXZpk2gA.error))
			{
				MPatchr.Oo1ruUb5AAVwJnBX0QWSyZhuNBfTSYAlNOATacVohQ5n(global::_003CModule_003E.smethod_29<string>(1183711725u));
			}
		}

		internal static float smethod_0(WWW www_0)
		{
			return www_0.progress;
		}
	}

	[CompilerGenerated]
	private sealed class i91uqN5FNcTjv7U2TjCY9xACHfMIt3KrxzGpJo5Yag8iC5zgs1DWAzJB92exZ1zaH_WZQg3b68PjuCnWfFQBgSVgTqme8RbW_0024LWBoevuBgzYO8pxKlm64CJiPeuif5XtKL1ynWNyUdWZO4LYP0B4n6k : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int SjlBM8inVA_YE4YVlr_0024gluY;

		private object yT7HpVIzmqW54W307WgJtr4;

		public Arena wQ6mrkDog7tAEXGul0Y8Sv0;

		public string N5mTrSSgP9hoOTIcN5PyQZA;

		public int UcH7l_0cqDbQ1dCZqMJPVmg;

		private AssetBundle zV_zhUsPWJwdeJhkAI1dXP7u2LiEyfBlR0VZtsTaswfp;

		private string ens1k1ZsoqrTYDIiHWcnkdmscuL75qXZoqQdB8fyr_0024UO;

		private int ZOZEq3VBMymFEtMC6amMWWU;

		private WWW C3ciLsR4RaXDCFvJoPf3DpE;

		private string zm0X0RUMuZAZl_nbgeR61nk;

		private string[] HTNtrkIdVpTw8ovwkOgRJLpzTWxeR_UOtCoKHTD7_0024uft;

		private string[] YGH6LpmmEtNyUILba0H0_zU;

		private string xIzVE24H7X7qIrGTfn2VpN4;

		private int DNHn830AuSCbHGQe4wptI5I;

		private float C1u6Ug52EtV72jzh_eBpTHw;

		private GameObject O__pnflKG3w_qwrdoysnEptsBFKK4ZDNTVLqcQExEgnA;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return yT7HpVIzmqW54W307WgJtr4;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return yT7HpVIzmqW54W307WgJtr4;
			}
		}

		[DebuggerHidden]
		public i91uqN5FNcTjv7U2TjCY9xACHfMIt3KrxzGpJo5Yag8iC5zgs1DWAzJB92exZ1zaH_WZQg3b68PjuCnWfFQBgSVgTqme8RbW_0024LWBoevuBgzYO8pxKlm64CJiPeuif5XtKL1ynWNyUdWZO4LYP0B4n6k(int _003C_003E1__state)
		{
			SjlBM8inVA_YE4YVlr_0024gluY = _003C_003E1__state;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			switch (SjlBM8inVA_YE4YVlr_0024gluY)
			{
			case -3:
			case 2:
				try
				{
				}
				finally
				{
					ITybmnn_CCVC5Wu_0024wHlWVVQ();
				}
				break;
			case -4:
			case 4:
			case 5:
			case 6:
			case 7:
			case 8:
			case 9:
			case 10:
				try
				{
				}
				finally
				{
					IkXk87oS8XNDSZg4w7A9Tz0();
				}
				break;
			}
			zV_zhUsPWJwdeJhkAI1dXP7u2LiEyfBlR0VZtsTaswfp = null;
			ens1k1ZsoqrTYDIiHWcnkdmscuL75qXZoqQdB8fyr_0024UO = null;
			C3ciLsR4RaXDCFvJoPf3DpE = null;
			zm0X0RUMuZAZl_nbgeR61nk = null;
			HTNtrkIdVpTw8ovwkOgRJLpzTWxeR_UOtCoKHTD7_0024uft = null;
			YGH6LpmmEtNyUILba0H0_zU = null;
			xIzVE24H7X7qIrGTfn2VpN4 = null;
			O__pnflKG3w_qwrdoysnEptsBFKK4ZDNTVLqcQExEgnA = null;
			SjlBM8inVA_YE4YVlr_0024gluY = -2;
		}

		private bool MoveNext()
		{
			try
			{
				switch (SjlBM8inVA_YE4YVlr_0024gluY)
				{
				default:
					return false;
				case 0:
					SjlBM8inVA_YE4YVlr_0024gluY = -1;
					Class17.DMoImhPC2jNUt2tgmSezeT_IK_0024XK5Df9PmexG_CaKb1R(global::_003CModule_003E.smethod_25<string>(3302254847u), (object)SceneMan.JFAOKFIDAGK, 180);
					Class17.DQGZpGELKqeKmXweFYGEcaXR73CqGywHQI6u2TATGL80(global::_003CModule_003E.smethod_25<string>(4266755163u), wQ6mrkDog7tAEXGul0Y8Sv0);
					smethod_0(-1f);
					ZOZEq3VBMymFEtMC6amMWWU = ((!smethod_1()) ? 10 : 30);
					goto IL_00e2;
				case 1:
					SjlBM8inVA_YE4YVlr_0024gluY = -1;
					ZOZEq3VBMymFEtMC6amMWWU--;
					goto IL_00e2;
				case 2:
					SjlBM8inVA_YE4YVlr_0024gluY = -3;
					if (C3ciLsR4RaXDCFvJoPf3DpE.error == null)
					{
						zV_zhUsPWJwdeJhkAI1dXP7u2LiEyfBlR0VZtsTaswfp = C3ciLsR4RaXDCFvJoPf3DpE.assetBundle;
						if (!zV_zhUsPWJwdeJhkAI1dXP7u2LiEyfBlR0VZtsTaswfp)
						{
							DP.CD(global::_003CModule_003E.smethod_29<string>(1832707807u));
						}
						else
						{
							DP.CD(global::_003CModule_003E.smethod_26<string>(2634462527u));
							File.SetLastAccessTime(ens1k1ZsoqrTYDIiHWcnkdmscuL75qXZoqQdB8fyr_0024UO, DateTime.Now);
						}
					}
					else
					{
						DP.CD(global::_003CModule_003E.smethod_26<string>(1992927651u) + C3ciLsR4RaXDCFvJoPf3DpE.error);
					}
					ITybmnn_CCVC5Wu_0024wHlWVVQ();
					C3ciLsR4RaXDCFvJoPf3DpE = null;
					goto IL_02d8;
				case 3:
					SjlBM8inVA_YE4YVlr_0024gluY = -1;
					if (zV_zhUsPWJwdeJhkAI1dXP7u2LiEyfBlR0VZtsTaswfp == null && N5mTrSSgP9hoOTIcN5PyQZA != null)
					{
						C3ciLsR4RaXDCFvJoPf3DpE = new WWW(N5mTrSSgP9hoOTIcN5PyQZA);
						SjlBM8inVA_YE4YVlr_0024gluY = -4;
						DP.C(global::_003CModule_003E.smethod_25<string>(1694211904u));
						wQ6mrkDog7tAEXGul0Y8Sv0.StartCoroutine(uBJwKirAjdzudbMO0yHed_0024F_0024uQO__0024Q8xjwdE90hm4wMb(C3ciLsR4RaXDCFvJoPf3DpE));
						yT7HpVIzmqW54W307WgJtr4 = C3ciLsR4RaXDCFvJoPf3DpE;
						SjlBM8inVA_YE4YVlr_0024gluY = 4;
						return true;
					}
					goto IL_0528;
				case 4:
					SjlBM8inVA_YE4YVlr_0024gluY = -4;
					DP.C(global::_003CModule_003E.smethod_26<string>(3282267820u));
					yT7HpVIzmqW54W307WgJtr4 = null;
					SjlBM8inVA_YE4YVlr_0024gluY = 5;
					return true;
				case 5:
					SjlBM8inVA_YE4YVlr_0024gluY = -4;
					if (!string.IsNullOrEmpty(C3ciLsR4RaXDCFvJoPf3DpE.error))
					{
						DP.CD(global::_003CModule_003E.smethod_26<string>(2236638438u) + C3ciLsR4RaXDCFvJoPf3DpE.error);
						yT7HpVIzmqW54W307WgJtr4 = null;
						SjlBM8inVA_YE4YVlr_0024gluY = 10;
						return true;
					}
					DP.C(global::_003CModule_003E.smethod_26<string>(514081063u));
					yT7HpVIzmqW54W307WgJtr4 = null;
					SjlBM8inVA_YE4YVlr_0024gluY = 6;
					return true;
				case 6:
					SjlBM8inVA_YE4YVlr_0024gluY = -4;
					zV_zhUsPWJwdeJhkAI1dXP7u2LiEyfBlR0VZtsTaswfp = C3ciLsR4RaXDCFvJoPf3DpE.assetBundle;
					if ((bool)zV_zhUsPWJwdeJhkAI1dXP7u2LiEyfBlR0VZtsTaswfp)
					{
						if (ens1k1ZsoqrTYDIiHWcnkdmscuL75qXZoqQdB8fyr_0024UO == null)
						{
							DP.CD(global::_003CModule_003E.smethod_27<string>(2829086053u));
							yT7HpVIzmqW54W307WgJtr4 = null;
							SjlBM8inVA_YE4YVlr_0024gluY = 8;
							return true;
						}
						if (UcH7l_0cqDbQ1dCZqMJPVmg == 0)
						{
							UcH7l_0cqDbQ1dCZqMJPVmg = FJLJNEKHKKH.AGBLBIJBMBH(C3ciLsR4RaXDCFvJoPf3DpE.responseHeaders);
							ens1k1ZsoqrTYDIiHWcnkdmscuL75qXZoqQdB8fyr_0024UO = FJLJNEKHKKH.OKBHDGLODJD(N5mTrSSgP9hoOTIcN5PyQZA, UcH7l_0cqDbQ1dCZqMJPVmg, HOHAPGCKEFE: false);
						}
						File.WriteAllBytes(ens1k1ZsoqrTYDIiHWcnkdmscuL75qXZoqQdB8fyr_0024UO, C3ciLsR4RaXDCFvJoPf3DpE.bytes);
						DP.CD(global::_003CModule_003E.smethod_28<string>(3331845362u));
						yT7HpVIzmqW54W307WgJtr4 = null;
						SjlBM8inVA_YE4YVlr_0024gluY = 7;
						return true;
					}
					DP.CD(global::_003CModule_003E.smethod_26<string>(3240604286u));
					yT7HpVIzmqW54W307WgJtr4 = null;
					SjlBM8inVA_YE4YVlr_0024gluY = 9;
					return true;
				case 7:
					SjlBM8inVA_YE4YVlr_0024gluY = -4;
					goto IL_051b;
				case 8:
					SjlBM8inVA_YE4YVlr_0024gluY = -4;
					goto IL_051b;
				case 9:
					SjlBM8inVA_YE4YVlr_0024gluY = -4;
					goto IL_051b;
				case 10:
					SjlBM8inVA_YE4YVlr_0024gluY = -4;
					goto IL_051b;
				case 11:
					SjlBM8inVA_YE4YVlr_0024gluY = -1;
					if ((bool)zV_zhUsPWJwdeJhkAI1dXP7u2LiEyfBlR0VZtsTaswfp)
					{
						DP.C(global::_003CModule_003E.smethod_27<string>(218596826u));
						yT7HpVIzmqW54W307WgJtr4 = null;
						SjlBM8inVA_YE4YVlr_0024gluY = 12;
						return true;
					}
					break;
				case 12:
					SjlBM8inVA_YE4YVlr_0024gluY = -1;
					zm0X0RUMuZAZl_nbgeR61nk = string.Empty;
					HTNtrkIdVpTw8ovwkOgRJLpzTWxeR_UOtCoKHTD7_0024uft = zV_zhUsPWJwdeJhkAI1dXP7u2LiEyfBlR0VZtsTaswfp.GetAllScenePaths();
					DP.C(global::_003CModule_003E.smethod_29<string>(2492806589u) + HTNtrkIdVpTw8ovwkOgRJLpzTWxeR_UOtCoKHTD7_0024uft.Length);
					yT7HpVIzmqW54W307WgJtr4 = null;
					SjlBM8inVA_YE4YVlr_0024gluY = 13;
					return true;
				case 13:
					SjlBM8inVA_YE4YVlr_0024gluY = -1;
					YGH6LpmmEtNyUILba0H0_zU = HTNtrkIdVpTw8ovwkOgRJLpzTWxeR_UOtCoKHTD7_0024uft;
					ZOZEq3VBMymFEtMC6amMWWU = 0;
					goto IL_064e;
				case 14:
					SjlBM8inVA_YE4YVlr_0024gluY = -1;
					if (xIzVE24H7X7qIrGTfn2VpN4.EndsWith(global::_003CModule_003E.smethod_27<string>(404719526u)))
					{
						DP.C(global::_003CModule_003E.smethod_26<string>(2403292574u));
						yT7HpVIzmqW54W307WgJtr4 = null;
						SjlBM8inVA_YE4YVlr_0024gluY = 15;
						return true;
					}
					xIzVE24H7X7qIrGTfn2VpN4 = null;
					ZOZEq3VBMymFEtMC6amMWWU++;
					goto IL_064e;
				case 15:
					SjlBM8inVA_YE4YVlr_0024gluY = -1;
					DNHn830AuSCbHGQe4wptI5I = xIzVE24H7X7qIrGTfn2VpN4.LastIndexOf(global::_003CModule_003E.smethod_26<string>(174643069u));
					DP.C(global::_003CModule_003E.smethod_26<string>(4209177017u) + (DNHn830AuSCbHGQe4wptI5I + 1) + global::_003CModule_003E.smethod_27<string>(3737871438u) + (xIzVE24H7X7qIrGTfn2VpN4.Length - DNHn830AuSCbHGQe4wptI5I - 7));
					yT7HpVIzmqW54W307WgJtr4 = null;
					SjlBM8inVA_YE4YVlr_0024gluY = 16;
					return true;
				case 16:
					SjlBM8inVA_YE4YVlr_0024gluY = -1;
					zm0X0RUMuZAZl_nbgeR61nk = xIzVE24H7X7qIrGTfn2VpN4.Substring(DNHn830AuSCbHGQe4wptI5I + 1, xIzVE24H7X7qIrGTfn2VpN4.Length - DNHn830AuSCbHGQe4wptI5I - 7);
					DP.C(global::_003CModule_003E.smethod_28<string>(2163218598u) + zm0X0RUMuZAZl_nbgeR61nk);
					yT7HpVIzmqW54W307WgJtr4 = null;
					SjlBM8inVA_YE4YVlr_0024gluY = 17;
					return true;
				case 17:
					SjlBM8inVA_YE4YVlr_0024gluY = -1;
					try
					{
						SceneManager.LoadScene(zm0X0RUMuZAZl_nbgeR61nk, LoadSceneMode.Additive);
					}
					catch
					{
						DP.D(global::_003CModule_003E.smethod_26<string>(1440990260u));
					}
					DP.C(global::_003CModule_003E.smethod_28<string>(3438057212u));
					yT7HpVIzmqW54W307WgJtr4 = null;
					SjlBM8inVA_YE4YVlr_0024gluY = 18;
					return true;
				case 18:
				{
					SjlBM8inVA_YE4YVlr_0024gluY = -1;
					GameObject gameObject4 = GameObject.Find(global::_003CModule_003E.smethod_27<string>(544311551u));
					C1u6Ug52EtV72jzh_eBpTHw = ((!gameObject4) ? 0f : gameObject4.transform.position.y);
					DP.C(global::_003CModule_003E.smethod_28<string>(2800637905u) + C1u6Ug52EtV72jzh_eBpTHw);
					yT7HpVIzmqW54W307WgJtr4 = null;
					SjlBM8inVA_YE4YVlr_0024gluY = 19;
					return true;
				}
				case 19:
				{
					SjlBM8inVA_YE4YVlr_0024gluY = -1;
					if (C1u6Ug52EtV72jzh_eBpTHw < 0.242f)
					{
						DP.D(global::_003CModule_003E.smethod_27<string>(2019486125u) + C1u6Ug52EtV72jzh_eBpTHw + global::_003CModule_003E.smethod_25<string>(3019357979u));
						if (ens1k1ZsoqrTYDIiHWcnkdmscuL75qXZoqQdB8fyr_0024UO != null)
						{
							File.Delete(ens1k1ZsoqrTYDIiHWcnkdmscuL75qXZoqQdB8fyr_0024UO);
						}
						goto IL_09a8;
					}
					GameObject gameObject2 = GameObject.Find(global::_003CModule_003E.smethod_26<string>(3003163916u));
					if ((bool)gameObject2)
					{
						UnityEngine.Object.Destroy(gameObject2);
					}
					GameObject gameObject3 = GameObject.Find(global::_003CModule_003E.smethod_25<string>(824130231u));
					if ((bool)gameObject3)
					{
						UnityEngine.Object.Destroy(gameObject3);
					}
					yT7HpVIzmqW54W307WgJtr4 = null;
					SjlBM8inVA_YE4YVlr_0024gluY = 20;
					return true;
				}
				case 20:
					SjlBM8inVA_YE4YVlr_0024gluY = -1;
					O__pnflKG3w_qwrdoysnEptsBFKK4ZDNTVLqcQExEgnA = GameObject.Find(global::_003CModule_003E.smethod_28<string>(873228035u));
					DP.C(global::_003CModule_003E.smethod_26<string>(3044827450u) + O__pnflKG3w_qwrdoysnEptsBFKK4ZDNTVLqcQExEgnA);
					yT7HpVIzmqW54W307WgJtr4 = null;
					SjlBM8inVA_YE4YVlr_0024gluY = 21;
					return true;
				case 21:
					SjlBM8inVA_YE4YVlr_0024gluY = -1;
					Class17.DQGZpGELKqeKmXweFYGEcaXR73CqGywHQI6u2TATGL80(global::_003CModule_003E.smethod_25<string>(2997172386u), wQ6mrkDog7tAEXGul0Y8Sv0, O__pnflKG3w_qwrdoysnEptsBFKK4ZDNTVLqcQExEgnA);
					DP.D(global::_003CModule_003E.smethod_28<string>(53688926u));
					O__pnflKG3w_qwrdoysnEptsBFKK4ZDNTVLqcQExEgnA = null;
					goto IL_09a8;
				case 22:
					{
						SjlBM8inVA_YE4YVlr_0024gluY = -1;
						goto IL_09c5;
					}
					IL_00e2:
					if (ZOZEq3VBMymFEtMC6amMWWU >= 0)
					{
						DP.C(global::_003CModule_003E.smethod_27<string>(3322345025u) + ZOZEq3VBMymFEtMC6amMWWU);
						yT7HpVIzmqW54W307WgJtr4 = null;
						SjlBM8inVA_YE4YVlr_0024gluY = 1;
						return true;
					}
					zV_zhUsPWJwdeJhkAI1dXP7u2LiEyfBlR0VZtsTaswfp = null;
					ens1k1ZsoqrTYDIiHWcnkdmscuL75qXZoqQdB8fyr_0024UO = null;
					if (N5mTrSSgP9hoOTIcN5PyQZA.Contains(global::_003CModule_003E.smethod_28<string>(2001882611u)))
					{
						if (N5mTrSSgP9hoOTIcN5PyQZA == global::_003CModule_003E.smethod_27<string>(1005015959u))
						{
							GameObject gameObject = Class17.DQGZpGELKqeKmXweFYGEcaXR73CqGywHQI6u2TATGL80<Arena, GameObject>(global::_003CModule_003E.smethod_29<string>(621084969u), wQ6mrkDog7tAEXGul0Y8Sv0, new object[1] { string.Empty });
							Class17.DQGZpGELKqeKmXweFYGEcaXR73CqGywHQI6u2TATGL80(global::_003CModule_003E.smethod_28<string>(99144773u), wQ6mrkDog7tAEXGul0Y8Sv0, gameObject);
							N5mTrSSgP9hoOTIcN5PyQZA = null;
						}
						else
						{
							ens1k1ZsoqrTYDIiHWcnkdmscuL75qXZoqQdB8fyr_0024UO = FJLJNEKHKKH.OKBHDGLODJD(N5mTrSSgP9hoOTIcN5PyQZA, UcH7l_0cqDbQ1dCZqMJPVmg, HOHAPGCKEFE: false);
							if (File.Exists(ens1k1ZsoqrTYDIiHWcnkdmscuL75qXZoqQdB8fyr_0024UO))
							{
								C3ciLsR4RaXDCFvJoPf3DpE = new WWW(global::_003CModule_003E.smethod_28<string>(4168611318u) + ens1k1ZsoqrTYDIiHWcnkdmscuL75qXZoqQdB8fyr_0024UO);
								SjlBM8inVA_YE4YVlr_0024gluY = -3;
								yT7HpVIzmqW54W307WgJtr4 = C3ciLsR4RaXDCFvJoPf3DpE;
								SjlBM8inVA_YE4YVlr_0024gluY = 2;
								return true;
							}
						}
					}
					else
					{
						N5mTrSSgP9hoOTIcN5PyQZA = global::_003CModule_003E.smethod_28<string>(4168611318u) + JKGKJLLFMLE.LAOHLAOMCPN + global::_003CModule_003E.smethod_28<string>(2390794247u) + N5mTrSSgP9hoOTIcN5PyQZA;
					}
					goto IL_02d8;
					IL_09a8:
					yT7HpVIzmqW54W307WgJtr4 = null;
					SjlBM8inVA_YE4YVlr_0024gluY = 22;
					return true;
					IL_051b:
					IkXk87oS8XNDSZg4w7A9Tz0();
					C3ciLsR4RaXDCFvJoPf3DpE = null;
					goto IL_0528;
					IL_0528:
					yT7HpVIzmqW54W307WgJtr4 = null;
					SjlBM8inVA_YE4YVlr_0024gluY = 11;
					return true;
					IL_064e:
					if (ZOZEq3VBMymFEtMC6amMWWU < YGH6LpmmEtNyUILba0H0_zU.Length)
					{
						xIzVE24H7X7qIrGTfn2VpN4 = YGH6LpmmEtNyUILba0H0_zU[ZOZEq3VBMymFEtMC6amMWWU];
						DP.C(global::_003CModule_003E.smethod_25<string>(243492623u) + xIzVE24H7X7qIrGTfn2VpN4);
						yT7HpVIzmqW54W307WgJtr4 = null;
						SjlBM8inVA_YE4YVlr_0024gluY = 14;
						return true;
					}
					goto IL_09c5;
					IL_09c5:
					YGH6LpmmEtNyUILba0H0_zU = null;
					if (!string.IsNullOrEmpty(zm0X0RUMuZAZl_nbgeR61nk))
					{
						SceneManager.UnloadSceneAsync(zm0X0RUMuZAZl_nbgeR61nk);
					}
					else
					{
						DP.D(global::_003CModule_003E.smethod_28<string>(4258485563u));
					}
					zV_zhUsPWJwdeJhkAI1dXP7u2LiEyfBlR0VZtsTaswfp.Unload(unloadAllLoadedObjects: false);
					if (Application.isEditor)
					{
						DP.C(global::_003CModule_003E.smethod_29<string>(2870365856u));
					}
					zm0X0RUMuZAZl_nbgeR61nk = null;
					HTNtrkIdVpTw8ovwkOgRJLpzTWxeR_UOtCoKHTD7_0024uft = null;
					break;
					IL_02d8:
					yT7HpVIzmqW54W307WgJtr4 = null;
					SjlBM8inVA_YE4YVlr_0024gluY = 3;
					return true;
				}
				wQ6mrkDog7tAEXGul0Y8Sv0.BOIEJCIBHKI.SwitchCheckCover(BHCKMFDEBBH: true);
				return false;
			}
			catch
			{
				//try-fault
				((IDisposable)this).Dispose();
				throw;
			}
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		private void ITybmnn_CCVC5Wu_0024wHlWVVQ()
		{
			SjlBM8inVA_YE4YVlr_0024gluY = -1;
			if (C3ciLsR4RaXDCFvJoPf3DpE != null)
			{
				smethod_2((IDisposable)C3ciLsR4RaXDCFvJoPf3DpE);
			}
		}

		private void IkXk87oS8XNDSZg4w7A9Tz0()
		{
			SjlBM8inVA_YE4YVlr_0024gluY = -1;
			if (C3ciLsR4RaXDCFvJoPf3DpE != null)
			{
				smethod_2((IDisposable)C3ciLsR4RaXDCFvJoPf3DpE);
			}
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw smethod_3();
		}

		internal static void smethod_0(float float_0)
		{
			AudioListener.volume = float_0;
		}

		internal static bool smethod_1()
		{
			return Application.isEditor;
		}

		internal static void smethod_2(IDisposable idisposable_0)
		{
			idisposable_0.Dispose();
		}

		internal static NotSupportedException smethod_3()
		{
			return new NotSupportedException();
		}
	}

	[CompilerGenerated]
	private sealed class GjmreSFF8OQ2FpIzdjNdgvMPx2962QjzdDmnUI_0024eyduJfGk0Vokt6BYh88N3EqC93NOB1x40QF3VdE2DUbmEYqrEUfW8LDmNdPv_ooQtmS0UBPiEhvXG4wL16pYFOKVrjAklIbLH3QKG99FDk10MfLTm7dLVj1kucrGVktduGN_0024p : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int SjlBM8inVA_YE4YVlr_0024gluY;

		private object yT7HpVIzmqW54W307WgJtr4;

		public WWW CxC16FoGnjbIzP7FXZpk2gA;

		private MBe5qZVM8bV6BRQFLUrD78oCAQuN4uSWlBiBtiId9puBOyOCv1NkMQymq_0024G2u_0024krwo7vw_wfQ_0024R4lC6qlEYco_5Q2OPLFuzjtsjpltO11amqvWFHI3fxPYZKK6h5Xr9tE04eKFe8j8YupYBYs5OUKWU HwNyne6Wz9F6iW9xGPj6Zl8;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return yT7HpVIzmqW54W307WgJtr4;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return yT7HpVIzmqW54W307WgJtr4;
			}
		}

		[DebuggerHidden]
		public GjmreSFF8OQ2FpIzdjNdgvMPx2962QjzdDmnUI_0024eyduJfGk0Vokt6BYh88N3EqC93NOB1x40QF3VdE2DUbmEYqrEUfW8LDmNdPv_ooQtmS0UBPiEhvXG4wL16pYFOKVrjAklIbLH3QKG99FDk10MfLTm7dLVj1kucrGVktduGN_0024p(int _003C_003E1__state)
		{
			SjlBM8inVA_YE4YVlr_0024gluY = _003C_003E1__state;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			HwNyne6Wz9F6iW9xGPj6Zl8 = null;
			SjlBM8inVA_YE4YVlr_0024gluY = -2;
		}

		private bool MoveNext()
		{
			int num;
			switch (SjlBM8inVA_YE4YVlr_0024gluY)
			{
			case 1:
				SjlBM8inVA_YE4YVlr_0024gluY = -1;
				num = (int)(smethod_3(HwNyne6Wz9F6iW9xGPj6Zl8.CxC16FoGnjbIzP7FXZpk2gA) * 100f);
				if (num % 5 == 0)
				{
					MPatchr.ShowDebugMsg(global::_003CModule_003E.smethod_25<string>(917158457u) + num + global::_003CModule_003E.smethod_28<string>(2000104127u));
				}
				break;
			default:
				return false;
			case 0:
				SjlBM8inVA_YE4YVlr_0024gluY = -1;
				HwNyne6Wz9F6iW9xGPj6Zl8 = new MBe5qZVM8bV6BRQFLUrD78oCAQuN4uSWlBiBtiId9puBOyOCv1NkMQymq_0024G2u_0024krwo7vw_wfQ_0024R4lC6qlEYco_5Q2OPLFuzjtsjpltO11amqvWFHI3fxPYZKK6h5Xr9tE04eKFe8j8YupYBYs5OUKWU();
				HwNyne6Wz9F6iW9xGPj6Zl8.CxC16FoGnjbIzP7FXZpk2gA = CxC16FoGnjbIzP7FXZpk2gA;
				HwNyne6Wz9F6iW9xGPj6Zl8.p0juANsEAhC8ssiPbRj1_3g = smethod_0(1, 1);
				smethod_1(HwNyne6Wz9F6iW9xGPj6Zl8.p0juANsEAhC8ssiPbRj1_3g, 0, 0, Color.green);
				smethod_2(HwNyne6Wz9F6iW9xGPj6Zl8.p0juANsEAhC8ssiPbRj1_3g);
				if (HwNyne6Wz9F6iW9xGPj6Zl8.CxC16FoGnjbIzP7FXZpk2gA == null)
				{
					return false;
				}
				MPatchr.smethod_0(global::_003CModule_003E.smethod_27<string>(3029184864u), delegate
				{
					if (HwNyne6Wz9F6iW9xGPj6Zl8.CxC16FoGnjbIzP7FXZpk2gA == null)
					{
						MPatchr.Oo1ruUb5AAVwJnBX0QWSyZhuNBfTSYAlNOATacVohQ5n(global::_003CModule_003E.smethod_27<string>(3029184864u));
					}
					GUI.DrawTexture(new Rect(0f, 10f, 100f * MBe5qZVM8bV6BRQFLUrD78oCAQuN4uSWlBiBtiId9puBOyOCv1NkMQymq_0024G2u_0024krwo7vw_wfQ_0024R4lC6qlEYco_5Q2OPLFuzjtsjpltO11amqvWFHI3fxPYZKK6h5Xr9tE04eKFe8j8YupYBYs5OUKWU.smethod_0(HwNyne6Wz9F6iW9xGPj6Zl8.CxC16FoGnjbIzP7FXZpk2gA), 10f), HwNyne6Wz9F6iW9xGPj6Zl8.p0juANsEAhC8ssiPbRj1_3g);
					if (HwNyne6Wz9F6iW9xGPj6Zl8.CxC16FoGnjbIzP7FXZpk2gA.isDone || !string.IsNullOrEmpty(HwNyne6Wz9F6iW9xGPj6Zl8.CxC16FoGnjbIzP7FXZpk2gA.error))
					{
						MPatchr.Oo1ruUb5AAVwJnBX0QWSyZhuNBfTSYAlNOATacVohQ5n(global::_003CModule_003E.smethod_29<string>(1183711725u));
					}
				});
				num = (int)smethod_3(HwNyne6Wz9F6iW9xGPj6Zl8.CxC16FoGnjbIzP7FXZpk2gA);
				break;
			}
			if (num < 95 && !HwNyne6Wz9F6iW9xGPj6Zl8.CxC16FoGnjbIzP7FXZpk2gA.isDone && string.IsNullOrEmpty(HwNyne6Wz9F6iW9xGPj6Zl8.CxC16FoGnjbIzP7FXZpk2gA.error))
			{
				yT7HpVIzmqW54W307WgJtr4 = smethod_4();
				SjlBM8inVA_YE4YVlr_0024gluY = 1;
				return true;
			}
			if (!string.IsNullOrEmpty(HwNyne6Wz9F6iW9xGPj6Zl8.CxC16FoGnjbIzP7FXZpk2gA.error))
			{
				MPatchr.ShowDebugMsg(global::_003CModule_003E.smethod_29<string>(814802884u));
			}
			else
			{
				MPatchr.ShowDebugMsg(global::_003CModule_003E.smethod_26<string>(2295301576u));
			}
			MPatchr.Oo1ruUb5AAVwJnBX0QWSyZhuNBfTSYAlNOATacVohQ5n(global::_003CModule_003E.smethod_27<string>(3029184864u));
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw smethod_5();
		}

		internal static Texture2D smethod_0(int int_0, int int_1)
		{
			return new Texture2D(int_0, int_1);
		}

		internal static void smethod_1(Texture2D texture2D_0, int int_0, int int_1, Color color_0)
		{
			texture2D_0.SetPixel(int_0, int_1, color_0);
		}

		internal static void smethod_2(Texture2D texture2D_0)
		{
			texture2D_0.Apply();
		}

		internal static float smethod_3(WWW www_0)
		{
			return www_0.progress;
		}

		internal static WaitForEndOfFrame smethod_4()
		{
			return new WaitForEndOfFrame();
		}

		internal static NotSupportedException smethod_5()
		{
			return new NotSupportedException();
		}
	}

	[HarmonyPrefix]
	internal static bool smethod_0(string NKKILLKNDOA, int PLOODOBLDCI, Arena __instance)
	{
		if (JKGKJLLFMLE.JMOEMCPIEJL != JKGKJLLFMLE.CDIAGJLJCJC.MOD)
		{
			return false;
		}
		if (smethod_1(NKKILLKNDOA))
		{
			return false;
		}
		smethod_2(__instance.BOIEJCIBHKI, bool_0: false);
		if (smethod_3((UnityEngine.Object)__instance.FICMBCLEFDL) && !__instance.FICMBCLEFDL.EFCBCPOCOBB)
		{
			Class17.DMoImhPC2jNUt2tgmSezeT_IK_0024XK5Df9PmexG_CaKb1R(global::_003CModule_003E.smethod_28<string>(1480195237u), __instance, 999);
			smethod_4(__instance, bool_0: true, bool_1: false);
		}
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_26<string>(2355358623u) + NKKILLKNDOA + global::_003CModule_003E.smethod_28<string>(2654009246u) + PLOODOBLDCI, bool_0: true);
		__instance.StartCoroutine(KJTLccZFRAhLHwM4rw3DXKU(NKKILLKNDOA, PLOODOBLDCI, __instance));
		return false;
	}

	internal static IEnumerator uBJwKirAjdzudbMO0yHed_0024F_0024uQO__0024Q8xjwdE90hm4wMb(WWW www)
	{
		Texture2D p0juANsEAhC8ssiPbRj1_3g = GjmreSFF8OQ2FpIzdjNdgvMPx2962QjzdDmnUI_0024eyduJfGk0Vokt6BYh88N3EqC93NOB1x40QF3VdE2DUbmEYqrEUfW8LDmNdPv_ooQtmS0UBPiEhvXG4wL16pYFOKVrjAklIbLH3QKG99FDk10MfLTm7dLVj1kucrGVktduGN_0024p.smethod_0(1, 1);
		GjmreSFF8OQ2FpIzdjNdgvMPx2962QjzdDmnUI_0024eyduJfGk0Vokt6BYh88N3EqC93NOB1x40QF3VdE2DUbmEYqrEUfW8LDmNdPv_ooQtmS0UBPiEhvXG4wL16pYFOKVrjAklIbLH3QKG99FDk10MfLTm7dLVj1kucrGVktduGN_0024p.smethod_1(p0juANsEAhC8ssiPbRj1_3g, 0, 0, Color.green);
		GjmreSFF8OQ2FpIzdjNdgvMPx2962QjzdDmnUI_0024eyduJfGk0Vokt6BYh88N3EqC93NOB1x40QF3VdE2DUbmEYqrEUfW8LDmNdPv_ooQtmS0UBPiEhvXG4wL16pYFOKVrjAklIbLH3QKG99FDk10MfLTm7dLVj1kucrGVktduGN_0024p.smethod_2(p0juANsEAhC8ssiPbRj1_3g);
		if (www == null)
		{
			yield break;
		}
		MPatchr.smethod_0(global::_003CModule_003E.smethod_27<string>(3029184864u), delegate
		{
			if (www == null)
			{
				MPatchr.Oo1ruUb5AAVwJnBX0QWSyZhuNBfTSYAlNOATacVohQ5n(global::_003CModule_003E.smethod_27<string>(3029184864u));
			}
			GUI.DrawTexture(new Rect(0f, 10f, 100f * MBe5qZVM8bV6BRQFLUrD78oCAQuN4uSWlBiBtiId9puBOyOCv1NkMQymq_0024G2u_0024krwo7vw_wfQ_0024R4lC6qlEYco_5Q2OPLFuzjtsjpltO11amqvWFHI3fxPYZKK6h5Xr9tE04eKFe8j8YupYBYs5OUKWU.smethod_0(www), 10f), p0juANsEAhC8ssiPbRj1_3g);
			if (www.isDone || !string.IsNullOrEmpty(www.error))
			{
				MPatchr.Oo1ruUb5AAVwJnBX0QWSyZhuNBfTSYAlNOATacVohQ5n(global::_003CModule_003E.smethod_29<string>(1183711725u));
			}
		});
		int num = (int)GjmreSFF8OQ2FpIzdjNdgvMPx2962QjzdDmnUI_0024eyduJfGk0Vokt6BYh88N3EqC93NOB1x40QF3VdE2DUbmEYqrEUfW8LDmNdPv_ooQtmS0UBPiEhvXG4wL16pYFOKVrjAklIbLH3QKG99FDk10MfLTm7dLVj1kucrGVktduGN_0024p.smethod_3(www);
		while (num < 95 && !www.isDone && string.IsNullOrEmpty(www.error))
		{
			yield return GjmreSFF8OQ2FpIzdjNdgvMPx2962QjzdDmnUI_0024eyduJfGk0Vokt6BYh88N3EqC93NOB1x40QF3VdE2DUbmEYqrEUfW8LDmNdPv_ooQtmS0UBPiEhvXG4wL16pYFOKVrjAklIbLH3QKG99FDk10MfLTm7dLVj1kucrGVktduGN_0024p.smethod_4();
			num = (int)(GjmreSFF8OQ2FpIzdjNdgvMPx2962QjzdDmnUI_0024eyduJfGk0Vokt6BYh88N3EqC93NOB1x40QF3VdE2DUbmEYqrEUfW8LDmNdPv_ooQtmS0UBPiEhvXG4wL16pYFOKVrjAklIbLH3QKG99FDk10MfLTm7dLVj1kucrGVktduGN_0024p.smethod_3(www) * 100f);
			if (num % 5 == 0)
			{
				MPatchr.ShowDebugMsg(global::_003CModule_003E.smethod_25<string>(917158457u) + num + global::_003CModule_003E.smethod_28<string>(2000104127u));
			}
		}
		if (!string.IsNullOrEmpty(www.error))
		{
			MPatchr.ShowDebugMsg(global::_003CModule_003E.smethod_29<string>(814802884u));
		}
		else
		{
			MPatchr.ShowDebugMsg(global::_003CModule_003E.smethod_26<string>(2295301576u));
		}
		MPatchr.Oo1ruUb5AAVwJnBX0QWSyZhuNBfTSYAlNOATacVohQ5n(global::_003CModule_003E.smethod_27<string>(3029184864u));
	}

	internal static IEnumerator KJTLccZFRAhLHwM4rw3DXKU(string mapURL, int urlHash, Arena __instance)
	{
		Class17.DMoImhPC2jNUt2tgmSezeT_IK_0024XK5Df9PmexG_CaKb1R(global::_003CModule_003E.smethod_25<string>(3302254847u), (object)SceneMan.JFAOKFIDAGK, 180);
		Class17.DQGZpGELKqeKmXweFYGEcaXR73CqGywHQI6u2TATGL80(global::_003CModule_003E.smethod_25<string>(4266755163u), __instance);
		i91uqN5FNcTjv7U2TjCY9xACHfMIt3KrxzGpJo5Yag8iC5zgs1DWAzJB92exZ1zaH_WZQg3b68PjuCnWfFQBgSVgTqme8RbW_0024LWBoevuBgzYO8pxKlm64CJiPeuif5XtKL1ynWNyUdWZO4LYP0B4n6k.smethod_0(-1f);
		for (int num = ((!i91uqN5FNcTjv7U2TjCY9xACHfMIt3KrxzGpJo5Yag8iC5zgs1DWAzJB92exZ1zaH_WZQg3b68PjuCnWfFQBgSVgTqme8RbW_0024LWBoevuBgzYO8pxKlm64CJiPeuif5XtKL1ynWNyUdWZO4LYP0B4n6k.smethod_1()) ? 10 : 30); num >= 0; num--)
		{
			DP.C(global::_003CModule_003E.smethod_27<string>(3322345025u) + num);
			yield return null;
		}
		AssetBundle assetBundle = null;
		string text = null;
		if (mapURL.Contains(global::_003CModule_003E.smethod_28<string>(2001882611u)))
		{
			if (mapURL == global::_003CModule_003E.smethod_27<string>(1005015959u))
			{
				GameObject gameObject = Class17.DQGZpGELKqeKmXweFYGEcaXR73CqGywHQI6u2TATGL80<Arena, GameObject>(global::_003CModule_003E.smethod_29<string>(621084969u), __instance, new object[1] { string.Empty });
				Class17.DQGZpGELKqeKmXweFYGEcaXR73CqGywHQI6u2TATGL80(global::_003CModule_003E.smethod_28<string>(99144773u), __instance, gameObject);
				mapURL = null;
			}
			else
			{
				text = FJLJNEKHKKH.OKBHDGLODJD(mapURL, urlHash, HOHAPGCKEFE: false);
				if (File.Exists(text))
				{
					WWW wWW = new WWW(global::_003CModule_003E.smethod_28<string>(4168611318u) + text);
					try
					{
						yield return wWW;
						if (wWW.error == null)
						{
							assetBundle = wWW.assetBundle;
							if (!assetBundle)
							{
								DP.CD(global::_003CModule_003E.smethod_29<string>(1832707807u));
							}
							else
							{
								DP.CD(global::_003CModule_003E.smethod_26<string>(2634462527u));
								File.SetLastAccessTime(text, DateTime.Now);
							}
						}
						else
						{
							DP.CD(global::_003CModule_003E.smethod_26<string>(1992927651u) + wWW.error);
						}
					}
					finally
					{
						if (wWW != null)
						{
							i91uqN5FNcTjv7U2TjCY9xACHfMIt3KrxzGpJo5Yag8iC5zgs1DWAzJB92exZ1zaH_WZQg3b68PjuCnWfFQBgSVgTqme8RbW_0024LWBoevuBgzYO8pxKlm64CJiPeuif5XtKL1ynWNyUdWZO4LYP0B4n6k.smethod_2((IDisposable)wWW);
						}
					}
				}
			}
		}
		else
		{
			mapURL = global::_003CModule_003E.smethod_28<string>(4168611318u) + JKGKJLLFMLE.LAOHLAOMCPN + global::_003CModule_003E.smethod_28<string>(2390794247u) + mapURL;
		}
		yield return null;
		if (assetBundle == null && mapURL != null)
		{
			WWW wWW = new WWW(mapURL);
			try
			{
				DP.C(global::_003CModule_003E.smethod_25<string>(1694211904u));
				__instance.StartCoroutine(uBJwKirAjdzudbMO0yHed_0024F_0024uQO__0024Q8xjwdE90hm4wMb(wWW));
				yield return wWW;
				DP.C(global::_003CModule_003E.smethod_26<string>(3282267820u));
				yield return null;
				if (!string.IsNullOrEmpty(wWW.error))
				{
					DP.CD(global::_003CModule_003E.smethod_26<string>(2236638438u) + wWW.error);
					yield return null;
				}
				else
				{
					DP.C(global::_003CModule_003E.smethod_26<string>(514081063u));
					yield return null;
					assetBundle = wWW.assetBundle;
					if ((bool)assetBundle)
					{
						if (text == null)
						{
							DP.CD(global::_003CModule_003E.smethod_27<string>(2829086053u));
							yield return null;
						}
						else
						{
							if (urlHash == 0)
							{
								urlHash = FJLJNEKHKKH.AGBLBIJBMBH(wWW.responseHeaders);
								text = FJLJNEKHKKH.OKBHDGLODJD(mapURL, urlHash, HOHAPGCKEFE: false);
							}
							File.WriteAllBytes(text, wWW.bytes);
							DP.CD(global::_003CModule_003E.smethod_28<string>(3331845362u));
							yield return null;
						}
					}
					else
					{
						DP.CD(global::_003CModule_003E.smethod_26<string>(3240604286u));
						yield return null;
					}
				}
			}
			finally
			{
				if (wWW != null)
				{
					i91uqN5FNcTjv7U2TjCY9xACHfMIt3KrxzGpJo5Yag8iC5zgs1DWAzJB92exZ1zaH_WZQg3b68PjuCnWfFQBgSVgTqme8RbW_0024LWBoevuBgzYO8pxKlm64CJiPeuif5XtKL1ynWNyUdWZO4LYP0B4n6k.smethod_2((IDisposable)wWW);
				}
			}
		}
		yield return null;
		if ((bool)assetBundle)
		{
			DP.C(global::_003CModule_003E.smethod_27<string>(218596826u));
			yield return null;
			string text2 = string.Empty;
			string[] allScenePaths = assetBundle.GetAllScenePaths();
			DP.C(global::_003CModule_003E.smethod_29<string>(2492806589u) + allScenePaths.Length);
			yield return null;
			string[] array = allScenePaths;
			foreach (string text3 in array)
			{
				DP.C(global::_003CModule_003E.smethod_25<string>(243492623u) + text3);
				yield return null;
				if (!text3.EndsWith(global::_003CModule_003E.smethod_27<string>(404719526u)))
				{
					continue;
				}
				DP.C(global::_003CModule_003E.smethod_26<string>(2403292574u));
				yield return null;
				int num2 = text3.LastIndexOf(global::_003CModule_003E.smethod_26<string>(174643069u));
				DP.C(global::_003CModule_003E.smethod_26<string>(4209177017u) + (num2 + 1) + global::_003CModule_003E.smethod_27<string>(3737871438u) + (text3.Length - num2 - 7));
				yield return null;
				text2 = text3.Substring(num2 + 1, text3.Length - num2 - 7);
				DP.C(global::_003CModule_003E.smethod_28<string>(2163218598u) + text2);
				yield return null;
				try
				{
					SceneManager.LoadScene(text2, LoadSceneMode.Additive);
				}
				catch
				{
					DP.D(global::_003CModule_003E.smethod_26<string>(1440990260u));
				}
				DP.C(global::_003CModule_003E.smethod_28<string>(3438057212u));
				yield return null;
				GameObject gameObject2 = GameObject.Find(global::_003CModule_003E.smethod_27<string>(544311551u));
				float num3 = ((!gameObject2) ? 0f : gameObject2.transform.position.y);
				DP.C(global::_003CModule_003E.smethod_28<string>(2800637905u) + num3);
				yield return null;
				if (num3 < 0.242f)
				{
					DP.D(global::_003CModule_003E.smethod_27<string>(2019486125u) + num3 + global::_003CModule_003E.smethod_25<string>(3019357979u));
					if (text != null)
					{
						File.Delete(text);
					}
				}
				else
				{
					GameObject gameObject3 = GameObject.Find(global::_003CModule_003E.smethod_26<string>(3003163916u));
					if ((bool)gameObject3)
					{
						UnityEngine.Object.Destroy(gameObject3);
					}
					GameObject gameObject4 = GameObject.Find(global::_003CModule_003E.smethod_25<string>(824130231u));
					if ((bool)gameObject4)
					{
						UnityEngine.Object.Destroy(gameObject4);
					}
					yield return null;
					GameObject gameObject5 = GameObject.Find(global::_003CModule_003E.smethod_28<string>(873228035u));
					DP.C(global::_003CModule_003E.smethod_26<string>(3044827450u) + gameObject5);
					yield return null;
					Class17.DQGZpGELKqeKmXweFYGEcaXR73CqGywHQI6u2TATGL80(global::_003CModule_003E.smethod_25<string>(2997172386u), __instance, gameObject5);
					DP.D(global::_003CModule_003E.smethod_28<string>(53688926u));
				}
				yield return null;
				break;
			}
			if (!string.IsNullOrEmpty(text2))
			{
				SceneManager.UnloadSceneAsync(text2);
			}
			else
			{
				DP.D(global::_003CModule_003E.smethod_28<string>(4258485563u));
			}
			assetBundle.Unload(unloadAllLoadedObjects: false);
			if (Application.isEditor)
			{
				DP.C(global::_003CModule_003E.smethod_29<string>(2870365856u));
			}
		}
		__instance.BOIEJCIBHKI.SwitchCheckCover(BHCKMFDEBBH: true);
	}

	internal static bool smethod_1(string string_0)
	{
		return string.IsNullOrEmpty(string_0);
	}

	internal static void smethod_2(RideCameraController rideCameraController_0, bool bool_0)
	{
		rideCameraController_0.SwitchCheckCover(bool_0);
	}

	internal static bool smethod_3(UnityEngine.Object object_0)
	{
		return object_0;
	}

	internal static void smethod_4(Arena arena_0, bool bool_0, bool bool_1)
	{
		arena_0.LockSelf(bool_0, bool_1);
	}
}
