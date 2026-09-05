using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using MPatchrMain;
using MPatchrMain.patching;
using McnCraft;
using Translation;
using UnityEngine;
using UnityEngine.SceneManagement;

internal static class FMpPDgPqT_0024MlkjDbhXAGLgVbz45OJagoxMsHXXVw14C6
{
	[Serializable]
	[CompilerGenerated]
	private sealed class gnRUQtSP6HaPYqmIMjTfoY0poSQRS1p47dkkLwM4aLxb9RbGbB_002467M2egoodqJph9g
	{
		public static readonly gnRUQtSP6HaPYqmIMjTfoY0poSQRS1p47dkkLwM4aLxb9RbGbB_002467M2egoodqJph9g _003C_003E9 = new gnRUQtSP6HaPYqmIMjTfoY0poSQRS1p47dkkLwM4aLxb9RbGbB_002467M2egoodqJph9g();

		public static Action _003C_003E9__5_0;

		public static Action _003C_003E9__9_0;

		public static Action<Game, string> _003C_003E9__10_0;

		internal void O89qPkCbHvD6o7OBEyr8hJ5PQ9H7YhfPv0joeAPcjahV()
		{
			try
			{
				if (smethod_0().name == global::_003CModule_003E.smethod_27<string>(3514760917u))
				{
					if (!aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.OwUd7bq6TgB1A4bFX_0024KLOmY)
					{
						Camera.main.renderingPath = RenderingPath.DeferredLighting;
					}
					else
					{
						Camera.main.renderingPath = RenderingPath.Forward;
					}
				}
			}
			catch (Exception)
			{
			}
		}

		internal void r4qwl7DlqhhR_00249xfzD8ApE6VXD_CoBDdo7dvh4rNvljf()
		{
			if (!MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.resizableWindow)
			{
				MPatchr.Pz7Y2DcAhZzcv7Lk7wXiIUCbShOnchoinPsXIA3FwiDS(global::_003CModule_003E.smethod_26<string>(1770398195u));
			}
			if (RF3hUfOyLY1kfDwv1hf0PRkQC5xzGOKEdcXE2nfCjXWkSQMIXJh4VtWAWsodkC_00242og.jitvyACtH8_0024OuAF27TVcILM == smethod_1() && RF3hUfOyLY1kfDwv1hf0PRkQC5xzGOKEdcXE2nfCjXWkSQMIXJh4VtWAWsodkC_00242og.rzD0aRqGqbffGtPSpW9MBqY == smethod_2())
			{
				return;
			}
			if (smethod_1() < 640 || smethod_2() < 480)
			{
				if (HelpDefs.isJ)
				{
					MPatchr.ShowDebugMsg(global::_003CModule_003E.smethod_26<string>(2618021356u));
				}
				else
				{
					MPatchr.ShowDebugMsg(global::_003CModule_003E.smethod_27<string>(848874501u));
				}
			}
			RF3hUfOyLY1kfDwv1hf0PRkQC5xzGOKEdcXE2nfCjXWkSQMIXJh4VtWAWsodkC_00242og.jitvyACtH8_0024OuAF27TVcILM = smethod_1();
			RF3hUfOyLY1kfDwv1hf0PRkQC5xzGOKEdcXE2nfCjXWkSQMIXJh4VtWAWsodkC_00242og.rzD0aRqGqbffGtPSpW9MBqY = smethod_2();
			JKGKJLLFMLE.IGOBPLOLHEP.screenWidth = RF3hUfOyLY1kfDwv1hf0PRkQC5xzGOKEdcXE2nfCjXWkSQMIXJh4VtWAWsodkC_00242og.jitvyACtH8_0024OuAF27TVcILM;
			JKGKJLLFMLE.IGOBPLOLHEP.screenHeight = RF3hUfOyLY1kfDwv1hf0PRkQC5xzGOKEdcXE2nfCjXWkSQMIXJh4VtWAWsodkC_00242og.rzD0aRqGqbffGtPSpW9MBqY;
			zAOrzM_2ysNo3jthAClNSQ3POH9nkmbIAQjQeFsGY2hYwlsUfYcNDVio3ZwNGLR_00245A.smethod_0();
			MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.resizable_w = smethod_1();
			MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.resizable_h = smethod_2();
			MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.UUiRNMwxRbfk_Fs4cDErRoM();
			CameraController component = smethod_4((Component)smethod_3()).GetComponent<CameraController>();
			if (smethod_5((UnityEngine.Object)component, (UnityEngine.Object)null))
			{
				component.D7dqMa4OGuhdUnXHjJZM6vrBYDmyw_0024HWYteNzGVsTh5k(global::_003CModule_003E.smethod_27<string>(3924670478u), new Vector3(smethod_1() / 2, smethod_2() / 2, 0f));
			}
		}

		internal void ZIfcarTZfyoB_00243u_9rBW065EFmRKxlwj2InWxWhS1Tyw(Game g, string cmd)
		{
			wGLWBKZMCHG1PG26hWKZuoGdzLEts0xKvyJlBoYeGhF0rumkaUIvqdRFzYTgXqjOS0iNS0jUZLLw7F_RM_0024TJz7s wGLWBKZMCHG1PG26hWKZuoGdzLEts0xKvyJlBoYeGhF0rumkaUIvqdRFzYTgXqjOS0iNS0jUZLLw7F_RM_0024TJz7s = new wGLWBKZMCHG1PG26hWKZuoGdzLEts0xKvyJlBoYeGhF0rumkaUIvqdRFzYTgXqjOS0iNS0jUZLLw7F_RM_0024TJz7s
			{
				Z5gvZl0Zayye87QIBE7TQaw = g
			};
			if (MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.translation == settingsIngame.translationMode.OFF)
			{
				return;
			}
			if (smethod_6(cmd, global::_003CModule_003E.smethod_29<string>(3022260179u)))
			{
				string text = smethod_9(smethod_8(smethod_7(cmd, new char[1] { ' ' })[0], global::_003CModule_003E.smethod_25<string>(1356149000u), ""));
				if (!smethod_10(text) && smethod_12(cmd, smethod_11(global::_003CModule_003E.smethod_29<string>(3022260179u), text, global::_003CModule_003E.smethod_26<string>(1847872584u))) && (!smethod_13(text, global::_003CModule_003E.smethod_27<string>(1081527876u)) || !smethod_13(text, global::_003CModule_003E.smethod_27<string>(309084854u)) || !smethod_13(text, global::_003CModule_003E.smethod_29<string>(1994478693u)) || !smethod_13(text, global::_003CModule_003E.smethod_28<string>(575524507u)) || !smethod_13(text, global::_003CModule_003E.smethod_25<string>(2056882174u))))
				{
					string text2 = smethod_16(cmd, smethod_15(smethod_14(global::_003CModule_003E.smethod_26<string>(1453671789u), text)) + 1);
					if (smethod_17(text, global::_003CModule_003E.smethod_28<string>(939764111u)))
					{
						text = global::_003CModule_003E.smethod_27<string>(2426315109u);
					}
					Translator.Run(text2, text, MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.translationEngine, wGLWBKZMCHG1PG26hWKZuoGdzLEts0xKvyJlBoYeGhF0rumkaUIvqdRFzYTgXqjOS0iNS0jUZLLw7F_RM_0024TJz7s.paYmadyjFzDm1HMlEEBtwyk1SgYiPYgrx5wAO_0g86vn);
				}
				else
				{
					H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.dPyDIxn1eBQrUn5pGr6HiSQ(global::_003CModule_003E.smethod_25<string>(391648684u));
					H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.dPyDIxn1eBQrUn5pGr6HiSQ(global::_003CModule_003E.smethod_27<string>(881429065u));
					H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.dPyDIxn1eBQrUn5pGr6HiSQ(global::_003CModule_003E.smethod_29<string>(3033362879u));
				}
			}
			else
			{
				UUi9CES6jT0ZzKFkxbFyR2jEgvhxrm6WMabMV4KBwk4K_0024MAe7FKbv3UblWLVLXi1aw.u5ER09FBgDoEuNjNt6mdw_k = !UUi9CES6jT0ZzKFkxbFyR2jEgvhxrm6WMabMV4KBwk4K_0024MAe7FKbv3UblWLVLXi1aw.u5ER09FBgDoEuNjNt6mdw_k;
				if (!UUi9CES6jT0ZzKFkxbFyR2jEgvhxrm6WMabMV4KBwk4K_0024MAe7FKbv3UblWLVLXi1aw.u5ER09FBgDoEuNjNt6mdw_k)
				{
					H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.dPyDIxn1eBQrUn5pGr6HiSQ(global::_003CModule_003E.smethod_28<string>(4081404799u));
				}
				else
				{
					H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.dPyDIxn1eBQrUn5pGr6HiSQ(global::_003CModule_003E.smethod_26<string>(2743011958u));
				}
			}
		}

		internal void eCW1CjoVVMqnJieCJTe6NQM()
		{
			z49_0024smggAJwSCmco_EK3QdA();
		}

		internal void eb1BTYSGUYhqkoUPUCyderU()
		{
			x7AguKcJt_p_00243G_0024FOdNNNBSiqEFMJQVHrxflheIFW9i2();
		}

		internal void eiXpj064ih0swqNwcQ5mbDc()
		{
			smethod_1();
		}

		internal void e_JgsrM_0024hpth6BubHV_AWLg()
		{
			galspacGafd_uWwApPBMht8rH_I5fyt3OnEX0dkDYlrh();
		}

		internal void fM0n9yWFbCorsa_68IGKSm8()
		{
			VN0mf6zO5NE6n17Sdyaf5t_wayU7GC4eDhiYm118_0024nBN();
		}

		internal void fRwYwnwEaZxgRkucbTsLF_k()
		{
			JKGKJLLFMLE.KAOJMNJNLLM = false;
		}

		internal void fnZX2DTqxDPxcAN3lKm9zn4()
		{
			smethod_18((MonoBehaviour)MPatchr.xcBvxcM_0024ckBeZyvdSoAkJoM, OQrGD0dFQowqrabnCwI0QL6cs4cXqPFzIaqBSKdYwXCe());
		}

		internal void f2VQhWmu_0024vFiIMeD8rDzo4o()
		{
			b_00242GpwOCqbMPpEsHdSXFl92QQWj2bJWFPRLJ9ViPaRNz();
		}

		internal void gA0wgmdtmCQ63V1oDPP8oxs()
		{
			ED0L_0024k_0024edhSAYyVWskVWfnbeLRq4AQYAoBLMbHaSLzGs();
		}

		internal static Scene smethod_0()
		{
			return SceneManager.GetActiveScene();
		}

		internal static int smethod_1()
		{
			return Screen.width;
		}

		internal static int smethod_2()
		{
			return Screen.height;
		}

		internal static Camera smethod_3()
		{
			return Camera.main;
		}

		internal static GameObject smethod_4(Component component_0)
		{
			return component_0.gameObject;
		}

		internal static bool smethod_5(UnityEngine.Object object_0, UnityEngine.Object object_1)
		{
			return object_0 != object_1;
		}

		internal static bool smethod_6(string string_0, string string_1)
		{
			return string_0.StartsWith(string_1);
		}

		internal static string[] smethod_7(string string_0, char[] char_0)
		{
			return string_0.Split(char_0);
		}

		internal static string smethod_8(string string_0, string string_1, string string_2)
		{
			return string_0.Replace(string_1, string_2);
		}

		internal static string smethod_9(string string_0)
		{
			return string_0.ToLower();
		}

		internal static bool smethod_10(string string_0)
		{
			return string.IsNullOrEmpty(string_0);
		}

		internal static string smethod_11(string string_0, string string_1, string string_2)
		{
			return string_0 + string_1 + string_2;
		}

		internal static bool smethod_12(string string_0, string string_1)
		{
			return string_0.Contains(string_1);
		}

		internal static bool smethod_13(string string_0, string string_1)
		{
			return string_0 != string_1;
		}

		internal static string smethod_14(string string_0, string string_1)
		{
			return string_0 + string_1;
		}

		internal static int smethod_15(string string_0)
		{
			return string_0.Length;
		}

		internal static string smethod_16(string string_0, int int_0)
		{
			return string_0.Substring(int_0);
		}

		internal static bool smethod_17(string string_0, string string_1)
		{
			return string_0 == string_1;
		}

		internal static Coroutine smethod_18(MonoBehaviour monoBehaviour_0, IEnumerator ienumerator_0)
		{
			return monoBehaviour_0.StartCoroutine(ienumerator_0);
		}
	}

	[CompilerGenerated]
	private sealed class wGLWBKZMCHG1PG26hWKZuoGdzLEts0xKvyJlBoYeGhF0rumkaUIvqdRFzYTgXqjOS0iNS0jUZLLw7F_RM_0024TJz7s
	{
		public Game Z5gvZl0Zayye87QIBE7TQaw;

		internal void paYmadyjFzDm1HMlEEBtwyk1SgYiPYgrx5wAO_0g86vn(string r2, string detected)
		{
			if (smethod_0((UnityEngine.Object)Z5gvZl0Zayye87QIBE7TQaw.FICMBCLEFDL, (UnityEngine.Object)null))
			{
				smethod_2(Z5gvZl0Zayye87QIBE7TQaw.GetComponent<PhotonView>(), global::_003CModule_003E.smethod_25<string>(3897140434u), BFDCHLBGJHF.Others, new object[2]
				{
					smethod_1(global::_003CModule_003E.smethod_28<string>(3626105294u), r2),
					-1
				});
				H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.dPyDIxn1eBQrUn5pGr6HiSQ(smethod_3(global::_003CModule_003E.smethod_25<string>(712606373u), r2, global::_003CModule_003E.smethod_27<string>(2905597970u)));
			}
			else
			{
				smethod_4(Z5gvZl0Zayye87QIBE7TQaw.FICMBCLEFDL, Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<Game, string>(global::_003CModule_003E.smethod_29<string>(3076547542u), Z5gvZl0Zayye87QIBE7TQaw), r2);
			}
		}

		internal static bool smethod_0(UnityEngine.Object object_0, UnityEngine.Object object_1)
		{
			return object_0 == object_1;
		}

		internal static string smethod_1(string string_0, string string_1)
		{
			return string_0 + string_1;
		}

		internal static void smethod_2(PhotonView photonView_0, string string_0, BFDCHLBGJHF bfdchlbgjhf_0, object[] object_0)
		{
			photonView_0.RPC(string_0, bfdchlbgjhf_0, object_0);
		}

		internal static string smethod_3(string string_0, string string_1, string string_2)
		{
			return string_0 + string_1 + string_2;
		}

		internal static void smethod_4(MachineController machineController_0, string string_0, string string_1)
		{
			machineController_0.SendChat(string_0, string_1);
		}
	}

	[CompilerGenerated]
	private sealed class ji7mhhpPdKoRYAmkjomxEky1SNTib1s_ZI_7d77cWwYjpQFQgEHlWkk31ISWaAWHu9ounzTWMQkpaMSMK3em87k : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int SjlBM8inVA_YE4YVlr_0024gluY;

		private object yT7HpVIzmqW54W307WgJtr4;

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
		public ji7mhhpPdKoRYAmkjomxEky1SNTib1s_ZI_7d77cWwYjpQFQgEHlWkk31ISWaAWHu9ounzTWMQkpaMSMK3em87k(int _003C_003E1__state)
		{
			SjlBM8inVA_YE4YVlr_0024gluY = _003C_003E1__state;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			SjlBM8inVA_YE4YVlr_0024gluY = -2;
		}

		private bool MoveNext()
		{
			switch (SjlBM8inVA_YE4YVlr_0024gluY)
			{
			case 0:
				SjlBM8inVA_YE4YVlr_0024gluY = -1;
				yT7HpVIzmqW54W307WgJtr4 = null;
				SjlBM8inVA_YE4YVlr_0024gluY = 1;
				return true;
			case 1:
			{
				SjlBM8inVA_YE4YVlr_0024gluY = -1;
				AudioConfiguration audioConfiguration_ = smethod_0();
				audioConfiguration_.numVirtualVoices = 256;
				smethod_1(audioConfiguration_);
				return false;
			}
			default:
				return false;
			}
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw smethod_2();
		}

		internal static AudioConfiguration smethod_0()
		{
			return AudioSettings.GetConfiguration();
		}

		internal static bool smethod_1(AudioConfiguration audioConfiguration_0)
		{
			return AudioSettings.Reset(audioConfiguration_0);
		}

		internal static NotSupportedException smethod_2()
		{
			return new NotSupportedException();
		}
	}

	public static Patch[] ckaPtFGmKTrL9dtatRozMxw = new Patch[39]
	{
		new Patch(global::_003CModule_003E.smethod_28<string>(2304328763u), global::_003CModule_003E.smethod_25<string>(3385282747u), new Type[1] { smethod_18(typeof(Unqldt6n_0024hz_1hUSDmZpkf68UcmWgqOc0n_0024ERarCi9fcUgSnxTsl0DYO631WcA1ZOg).TypeHandle) }),
		new Patch(global::_003CModule_003E.smethod_26<string>(1701841346u), global::_003CModule_003E.smethod_27<string>(1849199471u), new Type[2]
		{
			smethod_18(typeof(ED5WPQxYa_WFEJ8sZpYciyF71F7y6F6L9h2_xaanIs8prucXCT5rAhf6sMNx9KK4eg).TypeHandle),
			smethod_18(typeof(_0024ICGGEmuSHiS4yKkMRFcT8e35reXck3MpVcnhLf251m9XytrQcHGzwATCnZLORd7rQ).TypeHandle)
		}),
		new Patch(global::_003CModule_003E.smethod_25<string>(3954132350u), global::_003CModule_003E.smethod_29<string>(1183846659u), new Type[1] { smethod_18(typeof(MLPOLjLeebDuY_VDPBvb03j_0024WB7OWYG07MV3isC1kCg_0024).TypeHandle) }),
		new Patch(global::_003CModule_003E.smethod_28<string>(4051100901u), global::_003CModule_003E.smethod_28<string>(3777921198u), new Type[0], delegate
		{
			z49_0024smggAJwSCmco_EK3QdA();
		}),
		new Patch(global::_003CModule_003E.smethod_25<string>(1375477357u), global::_003CModule_003E.smethod_29<string>(3423250983u), new Type[0], delegate
		{
			x7AguKcJt_p_00243G_0024FOdNNNBSiqEFMJQVHrxflheIFW9i2();
		}),
		new Patch(global::_003CModule_003E.smethod_29<string>(761876592u), global::_003CModule_003E.smethod_28<string>(1213092021u), new Type[1] { smethod_18(typeof(Class55).TypeHandle) }),
		new Patch(global::_003CModule_003E.smethod_26<string>(4276062914u), global::_003CModule_003E.smethod_27<string>(3757126231u), new Type[2]
		{
			smethod_18(typeof(kILN_0024q_tbcSASqORkmJ7BBsHZ_0024NvZSWw5PyFkcF9sNb4p2Xf_0024UVaPWSXF0_0024gn8lcUA).TypeHandle),
			smethod_18(typeof(v1JBKckAa1RFmn2CeELS4d1FhLzhlYwRV2bd7TgD_0024MEJnWym5unAzsCQpkwgvPK2FbBLfBqBfJdE_8ZO15q40ZU).TypeHandle)
		}),
		new Patch(global::_003CModule_003E.smethod_29<string>(3607092335u), global::_003CModule_003E.smethod_28<string>(2396870734u), new Type[1] { smethod_18(typeof(m0HM_uVPHuetIhO98ec0YYXakAbCJhU7jw0q6FI9lBE6NNfOlW0PtzeNiryZBW4hGBKIOl92ebLgbYsZGLkYn2s).TypeHandle) }),
		new Patch(global::_003CModule_003E.smethod_25<string>(2476345729u), global::_003CModule_003E.smethod_25<string>(2171263268u), new Type[1] { smethod_18(typeof(dyl7NQFWvb8SnwY4dXogp_aCQhx2Y7dLGUBgzCus25T9Wpo6h01g9Y342KDBl8ctV_NzWUOypgYMsbS0RAESeaU.Class50).TypeHandle) }),
		new Patch(global::_003CModule_003E.smethod_27<string>(1146637004u), global::_003CModule_003E.smethod_29<string>(2016684093u), new Type[4]
		{
			smethod_18(typeof(Y_KnsAySLoCDb4SHb_0024ozdg9Jhandbv5CjRS2vvjE93HwxrEnN8gRKN_3gH8ZJ8dFtwLfFaqEhK1SoCTTPpPBgcQ.BmX9fkX90Trh4MiCFQ9HUq6Bf23SL0OB3yAPfwsaL7EbEGh22P6F6ygh7saJ5JgFCpe49M01gAXl8wnxSj3NQcZaFmNbdCsuuk37PKwTmIWo).TypeHandle),
			smethod_18(typeof(Y_KnsAySLoCDb4SHb_0024ozdg9Jhandbv5CjRS2vvjE93HwxrEnN8gRKN_3gH8ZJ8dFtwLfFaqEhK1SoCTTPpPBgcQ.NITp1tLbTWES2Ob6kMhtDtPwvH602qGymXMfH_0024CEMgKuAv7OVF9f0mzKSxZ1NgPwsZSvWzyjeshABfM0CJHrny5nf_yHLSKeM0DEKstOpERjwFRcTI6of8GIKwWM2oUMhjtrHr4gOeVSd4wCkZ9eTa8).TypeHandle),
			smethod_18(typeof(Y_KnsAySLoCDb4SHb_0024ozdg9Jhandbv5CjRS2vvjE93HwxrEnN8gRKN_3gH8ZJ8dFtwLfFaqEhK1SoCTTPpPBgcQ.Class29).TypeHandle),
			smethod_18(typeof(Y_KnsAySLoCDb4SHb_0024ozdg9Jhandbv5CjRS2vvjE93HwxrEnN8gRKN_3gH8ZJ8dFtwLfFaqEhK1SoCTTPpPBgcQ.qC9jPXa9x5w7_4FIGyr6StdFsakjWRwnySHoa8RO9gH8mjXUEqbnJu1k2WohVbC5d91lAw9KvgAn_lMpenOZne6U97LRMpXA6wf0wZFduAqT2uAcHAskpVU_0024HsV5EKFVSSjE4vSCy2SF2ZCNt7MTsms).TypeHandle)
		}, delegate
		{
			smethod_1();
		}),
		new Patch(global::_003CModule_003E.smethod_26<string>(4199006263u), global::_003CModule_003E.smethod_25<string>(2732175257u), new Type[13]
		{
			smethod_18(typeof(kILN_0024q_tbcSASqORkmJ7BBsDUjxuSBqI3VGW6VcIauaSSt6_Qk8HVbbHG_0024WDerWUiLyHmclIfPXtnf3BNWVqydk.drGKDPlVNm06T6fgsX2kJFCo2xHXzX8YWj4uxnNHFyFJF_0024C_0024RPEXrWmetvDxlbSqKAe_0024lmq1NIv4vOXFKCeSERWoRyqm12q_7mlFZN0DcrW3).TypeHandle),
			smethod_18(typeof(kILN_0024q_tbcSASqORkmJ7BBsDUjxuSBqI3VGW6VcIauaSSt6_Qk8HVbbHG_0024WDerWUiLyHmclIfPXtnf3BNWVqydk.Class41).TypeHandle),
			smethod_18(typeof(kILN_0024q_tbcSASqORkmJ7BBsDUjxuSBqI3VGW6VcIauaSSt6_Qk8HVbbHG_0024WDerWUiLyHmclIfPXtnf3BNWVqydk.Class40).TypeHandle),
			smethod_18(typeof(kILN_0024q_tbcSASqORkmJ7BBsDUjxuSBqI3VGW6VcIauaSSt6_Qk8HVbbHG_0024WDerWUiLyHmclIfPXtnf3BNWVqydk.D_wOoOtOlIgcgktWR5FkIGrx3gk_0024RFfquiBdHerTG8cv_cUKX27mxGzjKcGhN7jKZndlHnPU26qcsDqaoAfuFj2b4_BxxVsM58X2QVDtb9cbxoms7NCgVKZFSYxuOX93nexDX3gy7pfpNl1KxdB5Yoc).TypeHandle),
			smethod_18(typeof(kILN_0024q_tbcSASqORkmJ7BBsDUjxuSBqI3VGW6VcIauaSSt6_Qk8HVbbHG_0024WDerWUiLyHmclIfPXtnf3BNWVqydk.k1jjXoMSLucngJadkqOae4tqtDtA_Ar5NPUDWU_JEmoZU_0024pnOX7WSTZMWE6fXgq0UrfPf_vgGAV45LYNMO1ZqHoX5t_0024vlyoMqNjsGeJnej1_DqX81cfpx02j6izeMrnBkJtRG7IAOXJcu6xEml6MYr4).TypeHandle),
			smethod_18(typeof(kILN_0024q_tbcSASqORkmJ7BBsDUjxuSBqI3VGW6VcIauaSSt6_Qk8HVbbHG_0024WDerWUiLyHmclIfPXtnf3BNWVqydk.MWNHoohRGa4s8_0024E5_0024BQnWn3ijbG1uSnbYyYLKXAqwwUZ4nlVt6oBIbwDsjoESQ_0024p8myzj3I4Vw6vNTzvtcXVek26EDzirEZqgURXKFJQbD0db_HRwPgR_duZiLLUXXvAlrbP0uPDp1ixId7zVcoCW8Q).TypeHandle),
			smethod_18(typeof(kILN_0024q_tbcSASqORkmJ7BBsDUjxuSBqI3VGW6VcIauaSSt6_Qk8HVbbHG_0024WDerWUiLyHmclIfPXtnf3BNWVqydk.l5JX6nBieZEhKSPiShCrEwh7hxzumDvDl6w7GPMTOtkkisjdrIASl6hb7ebIhj19cc_0024niXHIRlOhQdTq5pWUdqjtJBx72FsGO5KZPhrLGTV3vN_0024yiYkRfVVKVfuC2tp8I4mOQrCQDhMKpj5QPo9hk18).TypeHandle),
			smethod_18(typeof(UDIRjB3VSmwHpVmUxcnHH66kk6ThWzbxmySWu7OngspoKs_47sbCTTGVAxk9y7DBfQ.BapBggtVQ_zz0yHfrbAIedzYaFFBakH3QJO7dF00lWjCOcFjk_zIcZA_Kz6biNDdwkkAXNbVOkNevpVGmoKELcmR6abGlXLIqBcxN4_0024ZtbuT).TypeHandle),
			smethod_18(typeof(UDIRjB3VSmwHpVmUxcnHH66kk6ThWzbxmySWu7OngspoKs_47sbCTTGVAxk9y7DBfQ.ax_0024v9_0024SAexqtnivpn2xbJvBNQXgcREocz5Od3npk66lRA5IniL27VLMerN_Vm1J8hkDOs3YQv5i0zmQLIWIgFpE).TypeHandle),
			smethod_18(typeof(UDIRjB3VSmwHpVmUxcnHH66kk6ThWzbxmySWu7OngspoKs_47sbCTTGVAxk9y7DBfQ.AXdZOpi5bHv3uj8JPpkS5wIcK3ayxkAHNgE4_QMsyJPTUT_nJsJBN6sW_SsiQrOHtZTXdF7Scfi6AufhowjTYFc).TypeHandle),
			smethod_18(typeof(UDIRjB3VSmwHpVmUxcnHH66kk6ThWzbxmySWu7OngspoKs_47sbCTTGVAxk9y7DBfQ.DMzITGEX1p2AB0rI_0024ovDSLwwvTLgzIan63Pk608ygcwFJlX_0024WMVZm7f_xImiWySFnaP5WMg6Whrcdrlw48W9ZaA).TypeHandle),
			smethod_18(typeof(UDIRjB3VSmwHpVmUxcnHH66kk6ThWzbxmySWu7OngspoKs_47sbCTTGVAxk9y7DBfQ.D_0024dw1eRPSF4yp_0024UxZQjRCroUoH1XOK81Sbvq7nAuOLIvqdVTFuN0_CQ1eoBpHnAWuq78kcigmAxkLL_0024mzPOOsQGDl1vJkX854bUCHitdZUda).TypeHandle),
			smethod_18(typeof(UDIRjB3VSmwHpVmUxcnHH66kk6ThWzbxmySWu7OngspoKs_47sbCTTGVAxk9y7DBfQ.BOj4l6TYT0cMEcyp0kn5iLxmWghs2F_JIYskR3PIcMxl4xbAs397F0zWr4dk0O_0024ULOVIwZz4usFBzgOdwfmatzvaIFXepmg0__0024tREydAHVhF).TypeHandle)
		}),
		new Patch(global::_003CModule_003E.smethod_27<string>(2537954912u), global::_003CModule_003E.smethod_28<string>(4248372652u), new Type[2]
		{
			smethod_18(typeof(u8Ar2V0yBXu1MN706lBpr0cloYcEfFWi2mOKspfamqUe_cd8Q8IzHpYyhevoqoPeRwzeHCG7cneLxvJIVa5JjOk).TypeHandle),
			smethod_18(typeof(U1GmT9ZMi6XvQgaRUcEHGTIQHfzPhx1iuaxf6E3VI8FO1W6Cvda7X81QvAFK4kp0BblGT5OfBUQXMt7rhMCay3U).TypeHandle)
		}),
		new Patch(global::_003CModule_003E.smethod_27<string>(2444893562u), global::_003CModule_003E.smethod_25<string>(2031442083u), new Type[2]
		{
			smethod_18(typeof(Class58).TypeHandle),
			smethod_18(typeof(iK6fSwg8tZ_0024emq_vet_0024SmvfeOX0MyVTRqRjd5zqmuBGHyyucmpSbIMqTFoCGV5KVaeJjT_0024pbHDyMSME9Vz9XDIw).TypeHandle)
		}),
		new Patch(global::_003CModule_003E.smethod_29<string>(2914911590u), global::_003CModule_003E.smethod_25<string>(1462592480u), new Type[6]
		{
			smethod_18(typeof(Q8GK9vSf_0024VEM692w50G2d5SmUcj4PtKNzDGwgaTN75YKao1YzfuGvFueAht8oPEnuw.jLOxhHK9EVxooJKItvFQG0G7FOaLUlGVn5zFuPYyq6DUUj6BJXH6ATMSwuF8GTr7LEkMDBpu8a79O840FQjdSVUEs1_Y_0024DGHqnB9MKf_0024xZMpdlbx8AgC4_uzvhqLmIuNQA).TypeHandle),
			smethod_18(typeof(Q8GK9vSf_0024VEM692w50G2d5SmUcj4PtKNzDGwgaTN75YKao1YzfuGvFueAht8oPEnuw.up6FXLYVyjEksJXtTa1iFHsb2iCdhPfIw_oEhmBFk00sM_0024tJG7SP0Jry0qdwDeo_0024o6cqCsygIsldUfqTzgjsAyvX0hDuy5QUXpXFAI9DWyC4YxWBX2hZKqoKpFJXIShpSQ).TypeHandle),
			smethod_18(typeof(Q8GK9vSf_0024VEM692w50G2d5SmUcj4PtKNzDGwgaTN75YKao1YzfuGvFueAht8oPEnuw.Class42).TypeHandle),
			smethod_18(typeof(Q8GK9vSf_0024VEM692w50G2d5SmUcj4PtKNzDGwgaTN75YKao1YzfuGvFueAht8oPEnuw.k2WIaTyroTQRMvU79wRrXLqyfoUcMlf_SqqbTddesrILWNpgMk5KTj37bDhltkgSQINCosnE02O3824W1B1qS0efo8Kd7BTcVGwDSCuVrW7xzJqJfVdK9QcRY2bAb9gWyw).TypeHandle),
			smethod_18(typeof(Q8GK9vSf_0024VEM692w50G2d5SmUcj4PtKNzDGwgaTN75YKao1YzfuGvFueAht8oPEnuw.bxVbt4NSfHWpKne7t5VufJftUp_Hf7zbDHCXkxZ2fyBrGLCgUqOIvTNgQUrNiW3c0xKbKfoAGpp2LDNO0TyOm8nJVvnTM_e3GTu11ER9X51si1hgDB_00249y0iFPxKiV9XiHQ).TypeHandle),
			smethod_18(typeof(Q8GK9vSf_0024VEM692w50G2d5SmUcj4PtKNzDGwgaTN75YKao1YzfuGvFueAht8oPEnuw.kpIgfIkCVRvmymFgFktallLmRWh9OUSe52g6A_afk_0024eAo_zNqRenTPmmNY6RGascdPlshLxrK9EhxwOtFixRP5_Dl_0024ugUGX7bcfREsaS06sQ6EapISkWSnQJOvUOfE_bTw).TypeHandle)
		}),
		new Patch(global::_003CModule_003E.smethod_26<string>(2149411033u), global::_003CModule_003E.smethod_28<string>(3140501891u), new Type[5]
		{
			smethod_18(typeof(j11cGBDYP8UYQEHVTkChdIOmgQumIJyS_qg07j_tssO5oe12WhiMFv9DLxz68nMrQg.exQgSGVqTUVXODHL2vUf2jUBoIsVYD9gr0MdsEjIk1Xjtbvd8WHkDq8fHFO8oB78KpcP8DmDYSyIZczZnFIILsAwawUiawVhlr6b_2lhPBmYzBb0QYhfmdSRPc5JBC_mKA).TypeHandle),
			smethod_18(typeof(j11cGBDYP8UYQEHVTkChdIOmgQumIJyS_qg07j_tssO5oe12WhiMFv9DLxz68nMrQg.Class43).TypeHandle),
			smethod_18(typeof(j11cGBDYP8UYQEHVTkChdIOmgQumIJyS_qg07j_tssO5oe12WhiMFv9DLxz68nMrQg.XBS7OSIVgwiqp2YsikrDtq4BQmpUvr84rfCKNKcGbAc9UfXB8jDM3L0Wpz7fZcn_EGBZDh_0024Z94v9pz54Cd0SD2U).TypeHandle),
			smethod_18(typeof(j11cGBDYP8UYQEHVTkChdIOmgQumIJyS_qg07j_tssO5oe12WhiMFv9DLxz68nMrQg.UDIRjB3VSmwHpVmUxcnHH65yPcxd5H6_00246yqnbce0mrXAqs8qxP9wsqVz0g8In_0024HEOhehd6xMECMcOiGbfmO6MRA).TypeHandle),
			smethod_18(typeof(j11cGBDYP8UYQEHVTkChdIOmgQumIJyS_qg07j_tssO5oe12WhiMFv9DLxz68nMrQg.Class45).TypeHandle)
		}),
		new Patch(global::_003CModule_003E.smethod_28<string>(2776262287u), global::_003CModule_003E.smethod_26<string>(2511842005u), new Type[9]
		{
			smethod_18(typeof(fyuG4GW3IraSRLT0CLbDKQ09fF3B9ergVMo51j9Rl0_0024r5ffufWNoL4IzyNljW0l7_0024w.s5IyDQwGN4oJ7sCRj3JwQiURoQQgzMLO1h_7Nv1d6aknGhYzXJ4dMoJVg0y_UVRY0zkSuVKBAHwEJ8ZTggZ4fSRUoXyDbDMjsxMHAjzyd2XYkwSDCOZ5RKDY9WP9PBZl9Q).TypeHandle),
			smethod_18(typeof(fyuG4GW3IraSRLT0CLbDKQ09fF3B9ergVMo51j9Rl0_0024r5ffufWNoL4IzyNljW0l7_0024w.Class31).TypeHandle),
			smethod_18(typeof(fyuG4GW3IraSRLT0CLbDKQ09fF3B9ergVMo51j9Rl0_0024r5ffufWNoL4IzyNljW0l7_0024w.Class32).TypeHandle),
			smethod_18(typeof(fyuG4GW3IraSRLT0CLbDKQ09fF3B9ergVMo51j9Rl0_0024r5ffufWNoL4IzyNljW0l7_0024w.xW0dccGv_zVG8iMOKUmaxtTqNmFR3cN5v9KW3QD_lpQ3M22P1lFTdVMQg4FHkTOzASgItAABjWhwPVNZ9t_dxRVLtKVh8Y1OLdfL7bvb1mypYk0mO_0024Ry8xs4vNCvmFAnfA).TypeHandle),
			smethod_18(typeof(fyuG4GW3IraSRLT0CLbDKQ09fF3B9ergVMo51j9Rl0_0024r5ffufWNoL4IzyNljW0l7_0024w.Iy0yDHSokn7A1acUSob1vPaa5JfPGtXOyI2wMz0l1hdznKe08xppB3tj0JMHUhj3dWO8i5ptFcclTjSfPVgCCWdFuwwdtbfnYkiPRGFYUYP0uYVAyrHEiSH_0024hooW8m1wtg).TypeHandle),
			smethod_18(typeof(fyuG4GW3IraSRLT0CLbDKQ09fF3B9ergVMo51j9Rl0_0024r5ffufWNoL4IzyNljW0l7_0024w.xKBuYdyDbBHJjstz9dcxj1vO43xpZUHAF3yv1i95Db01bNK4TLZ2Mnx5CrTh_xmAsGU4e1s5Eep_00247KSnrQAWGLd9r9h9T_0024oODPdVEAB8p85B).TypeHandle),
			smethod_18(typeof(fyuG4GW3IraSRLT0CLbDKQ09fF3B9ergVMo51j9Rl0_0024r5ffufWNoL4IzyNljW0l7_0024w.xW0dccGv_zVG8iMOKUmaxtTqNmFR3cN5v9KW3QD_lpQ3M22P1lFTdVMQg4FHkTOzASgItAABjWhwPVNZ9t_dxRXJoy0LYEPnl0zAGSKshXjY).TypeHandle),
			smethod_18(typeof(fyuG4GW3IraSRLT0CLbDKQ09fF3B9ergVMo51j9Rl0_0024r5ffufWNoL4IzyNljW0l7_0024w.F3z9FVn3k2nDHv5n4doF0Uxn9J_3B2jIciT1Z0tAhMSMpvI4_0024JonvQDmiL_0024TvKx1yMQgKSjetA7g6G2CXU2FPWn4z1JW1w_0024eMHEQeYVj8ytU_aFpIcurBv8gZtpWHsavOA).TypeHandle),
			smethod_18(typeof(fyuG4GW3IraSRLT0CLbDKQ09fF3B9ergVMo51j9Rl0_0024r5ffufWNoL4IzyNljW0l7_0024w.DzasVnZU3q62SUdBxyHmufs3d1qiuDogSOVepunmMl95gKwar2bJ_78Z7f8wnLUWI2xlPk1tjGqw2JOuju77lpZ6AuoR_0024n2nQJUA_00246bURj0x).TypeHandle)
		}, delegate
		{
			galspacGafd_uWwApPBMht8rH_I5fyt3OnEX0dkDYlrh();
		}),
		new Patch(global::_003CModule_003E.smethod_27<string>(574292793u), global::_003CModule_003E.smethod_25<string>(1379961842u), new Type[2]
		{
			smethod_18(typeof(Class56.VyvC14dv321xosTSmlEtK6RZ_0024PTiI3GAqIWsbj6f9SOL4s24n6a3rXZHyPq8ySUMojDrPQQFucqhqlmnLI5NmOP9plJQ2i1_CoMmWg50OmhX7_0024iPAsah4ggOMgwL6V_iPw).TypeHandle),
			smethod_18(typeof(Class56.hV1kc4UyLxNGd1I_0024rfx_88TR6luLZfynSMe65w_SXQWJ4AHYsMXMrLdqWlDuhjVDWmWSih4fr11raL9U1Kb3Kgg).TypeHandle)
		}, delegate
		{
			VN0mf6zO5NE6n17Sdyaf5t_wayU7GC4eDhiYm118_0024nBN();
		}),
		new Patch(global::_003CModule_003E.smethod_25<string>(1990126764u), global::_003CModule_003E.smethod_27<string>(1039599543u), new Type[1] { smethod_18(typeof(cueQi_wQP26TRIVnZP9Z8aKHYYL_00245mvYiphrS2INIGjFBzL0M0_0024ZrwQ0Yl0WnnCZVw).TypeHandle) }),
		new Patch(global::_003CModule_003E.smethod_25<string>(1651666598u), global::_003CModule_003E.smethod_28<string>(2730806440u), new Type[3]
		{
			smethod_18(typeof(rJ_GZCaJwYznjXdT4CwqWDCxYyFtABbbcEpsXX_tHwAw5IiWCagOLqg_ot_0024fXBSs_0024w.er0HYGqNAojXGQnki2Q6p2xHA8QyMHMSXWEk_e87lm2Zjib7sDsApYu6olYKz6duakOmPurjVj1wHKvlTnr4I0tyUWJN94Gmktwnc6ecA5f3).TypeHandle),
			smethod_18(typeof(rJ_GZCaJwYznjXdT4CwqWDCxYyFtABbbcEpsXX_tHwAw5IiWCagOLqg_ot_0024fXBSs_0024w.Class26).TypeHandle),
			smethod_18(typeof(rJ_GZCaJwYznjXdT4CwqWDCxYyFtABbbcEpsXX_tHwAw5IiWCagOLqg_ot_0024fXBSs_0024w.W7R47_jkRnWN3ptk8JRVHEGN8kdlXdVfmp5Gt_o5MqqwUV78QOWW9GNTO6BIVxQyZ4dS6g7yCnosZw9Gy6y5uHw).TypeHandle)
		}, delegate
		{
			JKGKJLLFMLE.KAOJMNJNLLM = false;
		}),
		new Patch(global::_003CModule_003E.smethod_25<string>(2352399772u), global::_003CModule_003E.smethod_28<string>(439156966u), new Type[0], delegate
		{
			gnRUQtSP6HaPYqmIMjTfoY0poSQRS1p47dkkLwM4aLxb9RbGbB_002467M2egoodqJph9g.smethod_18((MonoBehaviour)MPatchr.xcBvxcM_0024ckBeZyvdSoAkJoM, OQrGD0dFQowqrabnCwI0QL6cs4cXqPFzIaqBSKdYwXCe());
		}),
		new Patch(global::_003CModule_003E.smethod_25<string>(2179200882u), global::_003CModule_003E.smethod_28<string>(4096704955u), new Type[5]
		{
			smethod_18(typeof(Ojep9_ywtLMuNK7LiDZuXM5KcDR4lqILg1rbr4yUAajWRwiGhSEu4QBjga6awpHdkA).TypeHandle),
			smethod_18(typeof(Class54).TypeHandle),
			smethod_18(typeof(Class53).TypeHandle),
			smethod_18(typeof(Class21).TypeHandle),
			smethod_18(typeof(Class49).TypeHandle)
		}),
		new Patch(global::_003CModule_003E.smethod_25<string>(1082816995u), global::_003CModule_003E.smethod_27<string>(160119060u), new Type[1] { smethod_18(typeof(bOo8x9nQoPUnPLha_0024sUCFCqhjTVaFjLEDRZK9mDqIg9yrCGxqCOAt3DLrt_mrOyUYOUF4ILJGSQEyiaIRTSrMu8).TypeHandle) }),
		new Patch(global::_003CModule_003E.smethod_29<string>(1941417467u), global::_003CModule_003E.smethod_26<string>(1591203225u), new Type[1] { smethod_18(typeof(BmX9fkX90Trh4MiCFQ9HUq4fs5QR_0024AcKSvsuQGsj1D98eu2yqZ_MR31lJY10Sbt5mA).TypeHandle) }),
		new Patch(global::_003CModule_003E.smethod_26<string>(2957600045u), global::_003CModule_003E.smethod_27<string>(1658474429u), new Type[1] { smethod_18(typeof(WjdgUJZnYG6vw94Y3x8Nf0K0lQC2qufqVZyuZsp_B3WP2Wpkr37R0UEpZiI_81GH3w.Class57).TypeHandle) }),
		new Patch(global::_003CModule_003E.smethod_29<string>(3153040305u), global::_003CModule_003E.smethod_28<string>(2078235184u), new Type[1] { smethod_18(typeof(psdRdI7hc081t9dJJ5z_0024OdB8xbQGLZVsl590oxhjP7f4VlD_n3_0024h4tuXN_0024gEeO_qnA).TypeHandle) }),
		new Patch(global::_003CModule_003E.smethod_26<string>(1757857361u), global::_003CModule_003E.smethod_25<string>(1470530094u), new Type[2]
		{
			smethod_18(typeof(fM8akW_NCNObVpnHZDsn9ZpvaBT_00242EZI_0024IX7xApy8Ppu5lo_bKdEPM8rXjLtGIhLxvyaFhnCfADn6F2MZVB9G2w).TypeHandle),
			smethod_18(typeof(miRGDz0BfTDQ_sn8vjwTjb9JXVzP6HdOAt_dpDR6Ehhw4F9dMzqzIQstsbJ5nmVjQw97Btz_eoznmLy53zIbgYU).TypeHandle)
		}),
		new Patch(global::_003CModule_003E.smethod_29<string>(1325729485u), global::_003CModule_003E.smethod_27<string>(3342952498u), new Type[1] { smethod_18(typeof(RF3hUfOyLY1kfDwv1hf0PRkQC5xzGOKEdcXE2nfCjXWkSQMIXJh4VtWAWsodkC_00242og).TypeHandle) }, delegate
		{
			b_00242GpwOCqbMPpEsHdSXFl92QQWj2bJWFPRLJ9ViPaRNz();
		}),
		new Patch(global::_003CModule_003E.smethod_26<string>(2091165633u), global::_003CModule_003E.smethod_29<string>(37613884u), new Type[2]
		{
			smethod_18(typeof(UUi9CES6jT0ZzKFkxbFyR2jEgvhxrm6WMabMV4KBwk4K_0024MAe7FKbv3UblWLVLXi1aw).TypeHandle),
			smethod_18(typeof(yzJYu9roTp6ENqtLoiCZ_0024IlmR4S21MYknwWab8ZmGOpbf99_0024V1fkXD6vIvlbY2i_00244wLBDd9OSyxlNysLO3mMuTc).TypeHandle)
		}, delegate
		{
			ED0L_0024k_0024edhSAYyVWskVWfnbeLRq4AQYAoBLMbHaSLzGs();
		}),
		new Patch(global::_003CModule_003E.smethod_25<string>(3094448265u), global::_003CModule_003E.smethod_29<string>(643425303u), new Type[1] { smethod_18(typeof(yzJYu9roTp6ENqtLoiCZ_0024IkzieVUW5qCWgOWO91IB0T1qP3hebt7hsycbIzVsKXt6A).TypeHandle) }),
		new Patch(global::_003CModule_003E.smethod_25<string>(3358215407u), global::_003CModule_003E.smethod_28<string>(4293976706u), new Type[2]
		{
			smethod_18(typeof(kR5a7hOtkF_0024_CEj9hhobe_0024WbGh1FQ9BN5bMDF7i1YIxY5T81x6j25eGuymw3K0yjVg.NBSSa3UOzg3vQZOrw3GdTwPMFeKbwKScd9b33CH9eASoo1tw68uCuqqdIJ6x4Ri_8EIiPPb0LVXhyZ77_00242sv_00245_ZDeuunPSeCu3PxpypyG1BlfZpifBLNouWJN4H7GDB2w).TypeHandle),
			smethod_18(typeof(kR5a7hOtkF_0024_CEj9hhobe_0024WbGh1FQ9BN5bMDF7i1YIxY5T81x6j25eGuymw3K0yjVg.KAFhrDJzdEfCrDGx4HtPO1h2EXkQ9HDiptu6SuATQ5js01GHtlUb97r_y0bVoPQ1TlcWtWHZCwAn9b3KJUd10n3fvEpzPaMWiGtetIP8sPr1eadAL1XObam4YBWeN2AO_g).TypeHandle)
		}),
		new Patch(global::_003CModule_003E.smethod_27<string>(3575605873u), global::_003CModule_003E.smethod_27<string>(2030719829u), new Type[5]
		{
			smethod_18(typeof(boq2Pu1wVUKBmYhCHYqNyeIJToBLrl9v4fX1Wj2K7fW80xAZGrjtRfrLlnuFUPPFo7r3VDm3ADUhB_r2BgwLSN0.Class46).TypeHandle),
			smethod_18(typeof(boq2Pu1wVUKBmYhCHYqNyeIJToBLrl9v4fX1Wj2K7fW80xAZGrjtRfrLlnuFUPPFo7r3VDm3ADUhB_r2BgwLSN0.YNvvIbIK3oKl15gmDYIsL3pVuqwZIJiSFZ5QsMxhKAmbwZ3pdHuixT4Gm15OW6CldXiVDLXKu05vXvw_NdzpnnTob23cmSjWsKTVD4JBsNtXLnWO5Gi6QPia77_0024e9qOW1w).TypeHandle),
			smethod_18(typeof(boq2Pu1wVUKBmYhCHYqNyeIJToBLrl9v4fX1Wj2K7fW80xAZGrjtRfrLlnuFUPPFo7r3VDm3ADUhB_r2BgwLSN0.E0nx12SufeXO3_0024RORc_9HBJFGtbii5CdRbIG18PODSX4P2uoRyQ5d_0024tq0D3MrztITnUYdEXyL02gwAFReKj8j8tTD5CdvZtEfaW_0024L1kW8vCSP0NYMgh4bwxwPOekMGOlBCKQPDtX_35j51HDrimQs4U).TypeHandle),
			smethod_18(typeof(boq2Pu1wVUKBmYhCHYqNyeIJToBLrl9v4fX1Wj2K7fW80xAZGrjtRfrLlnuFUPPFo7r3VDm3ADUhB_r2BgwLSN0.fWvgZH1Uh15yXdrDTaG_0024OCUO8Sz9M82PV6wpYB6vYRkKIjbnwhlITCvKZXWJyuvN28h8bQY_tAhQnjUxLOqtGUPxKeX4NhzJIzLkwkBMQh1xJJx6B6_JZtzdl06S87si5w).TypeHandle),
			smethod_18(typeof(boq2Pu1wVUKBmYhCHYqNyeIJToBLrl9v4fX1Wj2K7fW80xAZGrjtRfrLlnuFUPPFo7r3VDm3ADUhB_r2BgwLSN0._0024sJ3urUFa23oy83oXCnfFmDVwTEIW_08ibLGJPG_0024GJkLL3Oj0DBI6OZZOkWgAUQeRSVwlhmpK37zEPeMyVmcGxZQhZ36pMiXfojyBtEj4MR_).TypeHandle)
		}),
		new Patch(global::_003CModule_003E.smethod_26<string>(1074658951u), global::_003CModule_003E.smethod_26<string>(2720159675u), new Type[4]
		{
			smethod_18(typeof(p15TFflpW2KKy78hPxhNbiPljDEt9MtSH5pTlOs7Y_0024LnvAl3Cjkk3Gzzu32qTCrlBA.TwmqlqBLWYBXcErN_ZNGWmgM8LpwpZOQQMFY4Fl_OimudZrpOpZDbnRkhz3D2Xf1HpM9guPECfD_QPxCI3nO_P_jtL6ErlOPB1BrsZKXsO0nbLdyJzo2Fwmz9_0024_g1NeqZQ).TypeHandle),
			smethod_18(typeof(p15TFflpW2KKy78hPxhNbiPljDEt9MtSH5pTlOs7Y_0024LnvAl3Cjkk3Gzzu32qTCrlBA.E0sz_00246BEV0Xn5QLzeZaAyotYeonhEkrEXu1BivzXDxm3UQrMTcPL_Wkpghp_vPQurQaYhPLhbgKl6S2G1y_00240H_0024c9AUbT3cbQtx50VWX_2DefyqROcpK_0024dgDnVkj9CesScg).TypeHandle),
			smethod_18(typeof(p15TFflpW2KKy78hPxhNbiPljDEt9MtSH5pTlOs7Y_0024LnvAl3Cjkk3Gzzu32qTCrlBA.LeFDAbekdb1DVzsC9Sq2VK4486SIi5z3e5avCWG8PcqAHGtNzs1OTSIuQemA0JEOu6zX7L358Mt7rDWuL_fWmlN7duiNUTAwRxnPLgUHxkCJ9tl4z_ETCd2yM9zebzkk_Q).TypeHandle),
			smethod_18(typeof(Class58).TypeHandle)
		}),
		new Patch(global::_003CModule_003E.smethod_26<string>(2880543394u), global::_003CModule_003E.smethod_25<string>(291515569u), new Type[1] { smethod_18(typeof(cueQi_wQP26TRIVnZP9Z8aIXZH4JvDO0r_vy_0024_0024NeyIlt6nScWImBH6O_ma4h8XogcA).TypeHandle) }),
		new Patch(global::_003CModule_003E.smethod_25<string>(1883683284u), global::_003CModule_003E.smethod_26<string>(2855461726u), new Type[6]
		{
			smethod_18(typeof(ax_0024v9_0024SAexqtnivpn2xbJvDmLoZqYQgD82KAtFRKwysCUi1dcgq2cA2a7Fb6y_m1uQ.KyntqBibyWTkeiAbBoA_0024nppi_0024tp_AR94Y6Mg2IVP_tbQuQ5lzyX57WLntLQSaLYe8JoU3oI2vKYVMubKtyi1o2DI3kPUxMOzG1NXq6fO8V3LqRgo6t48u4jjBfE1X_0024vK_Q).TypeHandle),
			smethod_18(typeof(ax_0024v9_0024SAexqtnivpn2xbJvDmLoZqYQgD82KAtFRKwysCUi1dcgq2cA2a7Fb6y_m1uQ.kK8bkyuB8Zu60PtPS9BeRK7qd6HKZbGSkRF01RokQsRwpuj_JoA7VKWEtK894NznFK5CAN4VWz59xIIBt_4rTrpwsRiOH1djfhz3XvqKVhEbO4iPcd1Sm_0024aJ9tPZiDInFA).TypeHandle),
			smethod_18(typeof(ax_0024v9_0024SAexqtnivpn2xbJvDmLoZqYQgD82KAtFRKwysCUi1dcgq2cA2a7Fb6y_m1uQ.XNC_0024X9uoQycz6Br_Ur2QkNHcz5CqwMF0XMyzsaU_0024WkaZDMGcC05iebaedaxYNLpe_ywCHO6wg1lfmYJkyal0Dc4UaIJ_0024ym5WduY6j6nSMWTkKlDh_0024dTQJm0WIfaazZ2oOQ).TypeHandle),
			smethod_18(typeof(ax_0024v9_0024SAexqtnivpn2xbJvDmLoZqYQgD82KAtFRKwysCUi1dcgq2cA2a7Fb6y_m1uQ.lWL6zB_A30b36LWXtRFH7_0gCBdAXCjkoZy7_pc30v4AlubSKEJa1wha5bOaW40MX23gfRaEMdh7du_0024C6aMP2RUmKN5HUJZNvyi5SJzYpmIYtQ4J_taaa_0024L29QL_tJWCyg).TypeHandle),
			smethod_18(typeof(ax_0024v9_0024SAexqtnivpn2xbJvDmLoZqYQgD82KAtFRKwysCUi1dcgq2cA2a7Fb6y_m1uQ.pYTpos8Hp6bKZk1KuMzbKYS27TBMwtq7mBP67nEZpPfc1DDlNNJ4yd7DsJl0sKq4TjMH_rEn8Ne_0024omyYmAxWzp1jgntOhf2z_SxajsUnDZKRXj1pBBvTYuhEtk_n9Dv7iw).TypeHandle),
			smethod_18(typeof(ax_0024v9_0024SAexqtnivpn2xbJvDmLoZqYQgD82KAtFRKwysCUi1dcgq2cA2a7Fb6y_m1uQ.o4N6sDPgGylHiQdonYk6x1TKIrgjlvBb9NabAXKd54Qll0kbYI2fG9q4tTOswRLwiidwo8cmVKI_crhliohxV8qjwNebsbArnYwo4UdX3zDsbvGE2Bb8rHnJl3mK5WkVig).TypeHandle)
		}),
		new Patch(global::_003CModule_003E.smethod_28<string>(1486123517u), global::_003CModule_003E.smethod_29<string>(2924788153u), new Type[5]
		{
			smethod_18(typeof(w5chUZMTNzi3hsv_ZuyAmjsrIqV_JS4UmvQj9ChlfHgM0YkamwAOecvqmdcCMPuZpA.Class24).TypeHandle),
			smethod_18(typeof(w5chUZMTNzi3hsv_ZuyAmjsrIqV_JS4UmvQj9ChlfHgM0YkamwAOecvqmdcCMPuZpA.ZaPtz1XbmxIr9Gg_l8jFEtOyYpePV_0024eSA6CbHt_OILZrRco40bZ2fFF2aqOpAhkwnNqWjn64ZpiZnq_apiQHarimF4RwG7xNBIjbY2m3PPb0AeYLJaYnEg_WojuhwF3jgg).TypeHandle),
			smethod_18(typeof(w5chUZMTNzi3hsv_ZuyAmjsrIqV_JS4UmvQj9ChlfHgM0YkamwAOecvqmdcCMPuZpA.EW64Igh84u6I4kLPhvZCaryAfzR0C0zYc33fzYPkADdSy_Kf_aA_0024_qS0s8EuTjNxfiEYl_0024ovaQmGuVl3B6DgCuT1A1n_SCcPAJoBTabakzp5nGONUcuNI0hrHl1RUJhRPg).TypeHandle),
			smethod_18(typeof(w5chUZMTNzi3hsv_ZuyAmjsrIqV_JS4UmvQj9ChlfHgM0YkamwAOecvqmdcCMPuZpA.Class23).TypeHandle),
			smethod_18(typeof(w5chUZMTNzi3hsv_ZuyAmjsrIqV_JS4UmvQj9ChlfHgM0YkamwAOecvqmdcCMPuZpA.fWZd_0024jPwaa4EJAeJ8CwEK2PgKc7Bg9QmtUjU4O7MoAFn3czxtnIhzXkiVu_00248wgBdqqStYr_E7kutH00veSQ73qqzlaox4QtPi_0024dHHqpcvbhf8uxcpoIghQwXIc0yBDtprQ).TypeHandle)
		}),
		new Patch(global::_003CModule_003E.smethod_25<string>(3243834313u), global::_003CModule_003E.smethod_29<string>(2643474775u), new Type[0]),
		new Patch(global::_003CModule_003E.smethod_25<string>(3507601455u), global::_003CModule_003E.smethod_25<string>(177134475u), new Type[1] { smethod_18(typeof(q11OkRCNnJSc7B9oAm2iiDy_dxuAP_XQuqvU6kvp3YxcC3E9HoBBDUaHX4F1nUnZzg).TypeHandle) }),
		new Patch(global::_003CModule_003E.smethod_28<string>(560372558u), global::_003CModule_003E.smethod_27<string>(3259264917u), new Type[1] { smethod_18(typeof(Hn0l0GNFrsPTtJLnYPdC5ktemJTCB1tOl3SdH45rF9OBAhL2qyryTi5LXdRLqzpTIg).TypeHandle) }),
		new Patch(global::_003CModule_003E.smethod_26<string>(687146311u), global::_003CModule_003E.smethod_28<string>(2199450776u), new Type[1] { smethod_18(typeof(Class22).TypeHandle) })
	};

	public static void TF_pZa3icvqb3FX9wdJrMOQ()
	{
		Patch[] array = ckaPtFGmKTrL9dtatRozMxw;
		foreach (Patch patch in array)
		{
			if (!smethod_2(patch.internalName, global::_003CModule_003E.smethod_25<string>(3343967428u)))
			{
				smethod_0(patch.internalName, BBgGuwZ6cMArb_002436eVwTw8A(patch.internalName), noSave: true);
			}
			else if (MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.translation != settingsIngame.translationMode.OFF)
			{
				patch.enable(metG733wDBlDhjB0MSUi_0024WZtiv2d5Ta15QUy66BHDFij5pgSnnkUUekfxHo4GGqdng.dh18OlujRtC2fZ7NxG9SAeI);
			}
		}
	}

	public static bool BBgGuwZ6cMArb_002436eVwTw8A(string patchName)
	{
		if (smethod_2(patchName, global::_003CModule_003E.smethod_25<string>(3343967428u)))
		{
			return false;
		}
		try
		{
			return Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<bool>(patchName, MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68);
		}
		catch (Exception exception_)
		{
			mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(smethod_4(global::_003CModule_003E.smethod_27<string>(3408061626u), patchName, global::_003CModule_003E.smethod_29<string>(2307874034u), smethod_3(exception_)));
			return false;
		}
	}

	public static void smethod_0(string patchName, bool status, bool noSave = false)
	{
		Patch[] array = ckaPtFGmKTrL9dtatRozMxw;
		foreach (Patch patch in array)
		{
			if (!smethod_2(patch.internalName, patchName))
			{
				continue;
			}
			try
			{
				Class17.DMoImhPC2jNUt2tgmSezeT_IK_0024XK5Df9PmexG_CaKb1R(patchName, (object)MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68, status);
				if (!noSave)
				{
					MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.UUiRNMwxRbfk_Fs4cDErRoM();
				}
			}
			catch (Exception)
			{
				break;
			}
			if (status)
			{
				patch.enable(metG733wDBlDhjB0MSUi_0024WZtiv2d5Ta15QUy66BHDFij5pgSnnkUUekfxHo4GGqdng.dh18OlujRtC2fZ7NxG9SAeI);
			}
		}
	}

	private static void z49_0024smggAJwSCmco_EK3QdA()
	{
		try
		{
			if (smethod_6(smethod_5()) != DepthTextureMode.DepthNormals && !smethod_7().name.Equals(global::_003CModule_003E.smethod_25<string>(1691553299u)))
			{
				Camera.main.depthTextureMode = DepthTextureMode.DepthNormals;
			}
		}
		catch (Exception)
		{
		}
	}

	private static void x7AguKcJt_p_00243G_0024FOdNNNBSiqEFMJQVHrxflheIFW9i2()
	{
		MPatchr.IqEoTLbjuIvkBlM_0024FuGaiKp4jfGtyoFGXu7ctG9PkRuX(global::_003CModule_003E.smethod_25<string>(1109884335u), delegate
		{
			try
			{
				if (gnRUQtSP6HaPYqmIMjTfoY0poSQRS1p47dkkLwM4aLxb9RbGbB_002467M2egoodqJph9g.smethod_0().name == global::_003CModule_003E.smethod_27<string>(3514760917u))
				{
					if (!aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.OwUd7bq6TgB1A4bFX_0024KLOmY)
					{
						Camera.main.renderingPath = RenderingPath.DeferredLighting;
					}
					else
					{
						Camera.main.renderingPath = RenderingPath.Forward;
					}
				}
			}
			catch (Exception)
			{
			}
		});
	}

	public static void smethod_1()
	{
		try
		{
			Y_KnsAySLoCDb4SHb_0024ozdg9Jhandbv5CjRS2vvjE93HwxrEnN8gRKN_3gH8ZJ8dFtwLfFaqEhK1SoCTTPpPBgcQ.aIaZtYI7wNQDacAAHL34p0Nl7kMZhMIwBjo5rC4fBnZ8de3uDeiL_0024F1XWjt1eSdJKxXnftNZci6jmYbeKvtCJtklYiGVafxup2rfGZJZKqmo.KnAOJbw49k2cqOyUYNexTSY();
		}
		catch (Exception exception_)
		{
			mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(smethod_9(new string[5]
			{
				global::_003CModule_003E.smethod_26<string>(3332571851u),
				smethod_3(exception_),
				global::_003CModule_003E.smethod_25<string>(1308721947u),
				smethod_8(exception_),
				global::_003CModule_003E.smethod_26<string>(2328606003u)
			}));
		}
	}

	public static void galspacGafd_uWwApPBMht8rH_I5fyt3OnEX0dkDYlrh()
	{
		string text = smethod_10(global::_003CModule_003E.smethod_29<string>(2020429971u), global::_003CModule_003E.smethod_27<string>(2775210629u));
		if (!smethod_11(global::_003CModule_003E.smethod_26<string>(1329098925u)))
		{
			smethod_12(global::_003CModule_003E.smethod_28<string>(1029786563u));
		}
		if (!smethod_13(text))
		{
			mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_27<string>(597473588u));
			MPatchr.NNbVj5nqStzgkt0zSfIM_qs(global::_003CModule_003E.smethod_25<string>(970063150u), text);
		}
	}

	public static void VN0mf6zO5NE6n17Sdyaf5t_wayU7GC4eDhiYm118_0024nBN()
	{
		string text = smethod_10(global::_003CModule_003E.smethod_25<string>(4000329416u), global::_003CModule_003E.smethod_29<string>(3662605835u));
		if (!smethod_11(global::_003CModule_003E.smethod_29<string>(2020429971u)))
		{
			smethod_12(global::_003CModule_003E.smethod_27<string>(94671762u));
		}
		if (!smethod_13(text))
		{
			mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_26<string>(368608258u));
			MPatchr.NNbVj5nqStzgkt0zSfIM_qs(global::_003CModule_003E.smethod_29<string>(4268417254u), text);
		}
	}

	public static void b_00242GpwOCqbMPpEsHdSXFl92QQWj2bJWFPRLJ9ViPaRNz()
	{
		smethod_14((MonoBehaviour)MPatchr.xcBvxcM_0024ckBeZyvdSoAkJoM, RF3hUfOyLY1kfDwv1hf0PRkQC5xzGOKEdcXE2nfCjXWkSQMIXJh4VtWAWsodkC_00242og.Q7PcOPlQiL1v1u8rCD5zc6Y());
		if (RF3hUfOyLY1kfDwv1hf0PRkQC5xzGOKEdcXE2nfCjXWkSQMIXJh4VtWAWsodkC_00242og.jitvyACtH8_0024OuAF27TVcILM != smethod_15() || RF3hUfOyLY1kfDwv1hf0PRkQC5xzGOKEdcXE2nfCjXWkSQMIXJh4VtWAWsodkC_00242og.rzD0aRqGqbffGtPSpW9MBqY != smethod_16())
		{
			RF3hUfOyLY1kfDwv1hf0PRkQC5xzGOKEdcXE2nfCjXWkSQMIXJh4VtWAWsodkC_00242og.jitvyACtH8_0024OuAF27TVcILM = smethod_15();
			RF3hUfOyLY1kfDwv1hf0PRkQC5xzGOKEdcXE2nfCjXWkSQMIXJh4VtWAWsodkC_00242og.rzD0aRqGqbffGtPSpW9MBqY = smethod_16();
			JKGKJLLFMLE.IGOBPLOLHEP.screenWidth = RF3hUfOyLY1kfDwv1hf0PRkQC5xzGOKEdcXE2nfCjXWkSQMIXJh4VtWAWsodkC_00242og.jitvyACtH8_0024OuAF27TVcILM;
			JKGKJLLFMLE.IGOBPLOLHEP.screenHeight = RF3hUfOyLY1kfDwv1hf0PRkQC5xzGOKEdcXE2nfCjXWkSQMIXJh4VtWAWsodkC_00242og.rzD0aRqGqbffGtPSpW9MBqY;
		}
		MPatchr.IqEoTLbjuIvkBlM_0024FuGaiKp4jfGtyoFGXu7ctG9PkRuX(global::_003CModule_003E.smethod_25<string>(1060631402u), delegate
		{
			if (!MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.resizableWindow)
			{
				MPatchr.Pz7Y2DcAhZzcv7Lk7wXiIUCbShOnchoinPsXIA3FwiDS(global::_003CModule_003E.smethod_26<string>(1770398195u));
			}
			if (RF3hUfOyLY1kfDwv1hf0PRkQC5xzGOKEdcXE2nfCjXWkSQMIXJh4VtWAWsodkC_00242og.jitvyACtH8_0024OuAF27TVcILM != gnRUQtSP6HaPYqmIMjTfoY0poSQRS1p47dkkLwM4aLxb9RbGbB_002467M2egoodqJph9g.smethod_1() || RF3hUfOyLY1kfDwv1hf0PRkQC5xzGOKEdcXE2nfCjXWkSQMIXJh4VtWAWsodkC_00242og.rzD0aRqGqbffGtPSpW9MBqY != gnRUQtSP6HaPYqmIMjTfoY0poSQRS1p47dkkLwM4aLxb9RbGbB_002467M2egoodqJph9g.smethod_2())
			{
				if (gnRUQtSP6HaPYqmIMjTfoY0poSQRS1p47dkkLwM4aLxb9RbGbB_002467M2egoodqJph9g.smethod_1() < 640 || gnRUQtSP6HaPYqmIMjTfoY0poSQRS1p47dkkLwM4aLxb9RbGbB_002467M2egoodqJph9g.smethod_2() < 480)
				{
					if (HelpDefs.isJ)
					{
						MPatchr.ShowDebugMsg(global::_003CModule_003E.smethod_26<string>(2618021356u));
					}
					else
					{
						MPatchr.ShowDebugMsg(global::_003CModule_003E.smethod_27<string>(848874501u));
					}
				}
				RF3hUfOyLY1kfDwv1hf0PRkQC5xzGOKEdcXE2nfCjXWkSQMIXJh4VtWAWsodkC_00242og.jitvyACtH8_0024OuAF27TVcILM = gnRUQtSP6HaPYqmIMjTfoY0poSQRS1p47dkkLwM4aLxb9RbGbB_002467M2egoodqJph9g.smethod_1();
				RF3hUfOyLY1kfDwv1hf0PRkQC5xzGOKEdcXE2nfCjXWkSQMIXJh4VtWAWsodkC_00242og.rzD0aRqGqbffGtPSpW9MBqY = gnRUQtSP6HaPYqmIMjTfoY0poSQRS1p47dkkLwM4aLxb9RbGbB_002467M2egoodqJph9g.smethod_2();
				JKGKJLLFMLE.IGOBPLOLHEP.screenWidth = RF3hUfOyLY1kfDwv1hf0PRkQC5xzGOKEdcXE2nfCjXWkSQMIXJh4VtWAWsodkC_00242og.jitvyACtH8_0024OuAF27TVcILM;
				JKGKJLLFMLE.IGOBPLOLHEP.screenHeight = RF3hUfOyLY1kfDwv1hf0PRkQC5xzGOKEdcXE2nfCjXWkSQMIXJh4VtWAWsodkC_00242og.rzD0aRqGqbffGtPSpW9MBqY;
				zAOrzM_2ysNo3jthAClNSQ3POH9nkmbIAQjQeFsGY2hYwlsUfYcNDVio3ZwNGLR_00245A.smethod_0();
				MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.resizable_w = gnRUQtSP6HaPYqmIMjTfoY0poSQRS1p47dkkLwM4aLxb9RbGbB_002467M2egoodqJph9g.smethod_1();
				MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.resizable_h = gnRUQtSP6HaPYqmIMjTfoY0poSQRS1p47dkkLwM4aLxb9RbGbB_002467M2egoodqJph9g.smethod_2();
				MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.UUiRNMwxRbfk_Fs4cDErRoM();
				CameraController component = gnRUQtSP6HaPYqmIMjTfoY0poSQRS1p47dkkLwM4aLxb9RbGbB_002467M2egoodqJph9g.smethod_4((Component)gnRUQtSP6HaPYqmIMjTfoY0poSQRS1p47dkkLwM4aLxb9RbGbB_002467M2egoodqJph9g.smethod_3()).GetComponent<CameraController>();
				if (gnRUQtSP6HaPYqmIMjTfoY0poSQRS1p47dkkLwM4aLxb9RbGbB_002467M2egoodqJph9g.smethod_5((UnityEngine.Object)component, (UnityEngine.Object)null))
				{
					component.D7dqMa4OGuhdUnXHjJZM6vrBYDmyw_0024HWYteNzGVsTh5k(global::_003CModule_003E.smethod_27<string>(3924670478u), new Vector3(gnRUQtSP6HaPYqmIMjTfoY0poSQRS1p47dkkLwM4aLxb9RbGbB_002467M2egoodqJph9g.smethod_1() / 2, gnRUQtSP6HaPYqmIMjTfoY0poSQRS1p47dkkLwM4aLxb9RbGbB_002467M2egoodqJph9g.smethod_2() / 2, 0f));
				}
			}
		});
	}

	public static void ED0L_0024k_0024edhSAYyVWskVWfnbeLRq4AQYAoBLMbHaSLzGs()
	{
		Class36.hJS8kPKIDNOtELzBVeai1g8.Add(global::_003CModule_003E.smethod_27<string>(3394085515u), delegate(Game g, string cmd)
		{
			wGLWBKZMCHG1PG26hWKZuoGdzLEts0xKvyJlBoYeGhF0rumkaUIvqdRFzYTgXqjOS0iNS0jUZLLw7F_RM_0024TJz7s wGLWBKZMCHG1PG26hWKZuoGdzLEts0xKvyJlBoYeGhF0rumkaUIvqdRFzYTgXqjOS0iNS0jUZLLw7F_RM_0024TJz7s = new wGLWBKZMCHG1PG26hWKZuoGdzLEts0xKvyJlBoYeGhF0rumkaUIvqdRFzYTgXqjOS0iNS0jUZLLw7F_RM_0024TJz7s();
			wGLWBKZMCHG1PG26hWKZuoGdzLEts0xKvyJlBoYeGhF0rumkaUIvqdRFzYTgXqjOS0iNS0jUZLLw7F_RM_0024TJz7s.Z5gvZl0Zayye87QIBE7TQaw = g;
			if (MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.translation != settingsIngame.translationMode.OFF)
			{
				if (gnRUQtSP6HaPYqmIMjTfoY0poSQRS1p47dkkLwM4aLxb9RbGbB_002467M2egoodqJph9g.smethod_6(cmd, global::_003CModule_003E.smethod_29<string>(3022260179u)))
				{
					string text = gnRUQtSP6HaPYqmIMjTfoY0poSQRS1p47dkkLwM4aLxb9RbGbB_002467M2egoodqJph9g.smethod_9(gnRUQtSP6HaPYqmIMjTfoY0poSQRS1p47dkkLwM4aLxb9RbGbB_002467M2egoodqJph9g.smethod_8(gnRUQtSP6HaPYqmIMjTfoY0poSQRS1p47dkkLwM4aLxb9RbGbB_002467M2egoodqJph9g.smethod_7(cmd, new char[1] { ' ' })[0], global::_003CModule_003E.smethod_25<string>(1356149000u), ""));
					if (!gnRUQtSP6HaPYqmIMjTfoY0poSQRS1p47dkkLwM4aLxb9RbGbB_002467M2egoodqJph9g.smethod_10(text) && gnRUQtSP6HaPYqmIMjTfoY0poSQRS1p47dkkLwM4aLxb9RbGbB_002467M2egoodqJph9g.smethod_12(cmd, gnRUQtSP6HaPYqmIMjTfoY0poSQRS1p47dkkLwM4aLxb9RbGbB_002467M2egoodqJph9g.smethod_11(global::_003CModule_003E.smethod_29<string>(3022260179u), text, global::_003CModule_003E.smethod_26<string>(1847872584u))) && (!gnRUQtSP6HaPYqmIMjTfoY0poSQRS1p47dkkLwM4aLxb9RbGbB_002467M2egoodqJph9g.smethod_13(text, global::_003CModule_003E.smethod_27<string>(1081527876u)) || !gnRUQtSP6HaPYqmIMjTfoY0poSQRS1p47dkkLwM4aLxb9RbGbB_002467M2egoodqJph9g.smethod_13(text, global::_003CModule_003E.smethod_27<string>(309084854u)) || !gnRUQtSP6HaPYqmIMjTfoY0poSQRS1p47dkkLwM4aLxb9RbGbB_002467M2egoodqJph9g.smethod_13(text, global::_003CModule_003E.smethod_29<string>(1994478693u)) || !gnRUQtSP6HaPYqmIMjTfoY0poSQRS1p47dkkLwM4aLxb9RbGbB_002467M2egoodqJph9g.smethod_13(text, global::_003CModule_003E.smethod_28<string>(575524507u)) || !gnRUQtSP6HaPYqmIMjTfoY0poSQRS1p47dkkLwM4aLxb9RbGbB_002467M2egoodqJph9g.smethod_13(text, global::_003CModule_003E.smethod_25<string>(2056882174u))))
					{
						string text2 = gnRUQtSP6HaPYqmIMjTfoY0poSQRS1p47dkkLwM4aLxb9RbGbB_002467M2egoodqJph9g.smethod_16(cmd, gnRUQtSP6HaPYqmIMjTfoY0poSQRS1p47dkkLwM4aLxb9RbGbB_002467M2egoodqJph9g.smethod_15(gnRUQtSP6HaPYqmIMjTfoY0poSQRS1p47dkkLwM4aLxb9RbGbB_002467M2egoodqJph9g.smethod_14(global::_003CModule_003E.smethod_26<string>(1453671789u), text)) + 1);
						if (gnRUQtSP6HaPYqmIMjTfoY0poSQRS1p47dkkLwM4aLxb9RbGbB_002467M2egoodqJph9g.smethod_17(text, global::_003CModule_003E.smethod_28<string>(939764111u)))
						{
							text = global::_003CModule_003E.smethod_27<string>(2426315109u);
						}
						Translator.Run(text2, text, MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.translationEngine, wGLWBKZMCHG1PG26hWKZuoGdzLEts0xKvyJlBoYeGhF0rumkaUIvqdRFzYTgXqjOS0iNS0jUZLLw7F_RM_0024TJz7s.paYmadyjFzDm1HMlEEBtwyk1SgYiPYgrx5wAO_0g86vn);
					}
					else
					{
						H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.dPyDIxn1eBQrUn5pGr6HiSQ(global::_003CModule_003E.smethod_25<string>(391648684u));
						H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.dPyDIxn1eBQrUn5pGr6HiSQ(global::_003CModule_003E.smethod_27<string>(881429065u));
						H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.dPyDIxn1eBQrUn5pGr6HiSQ(global::_003CModule_003E.smethod_29<string>(3033362879u));
					}
				}
				else
				{
					UUi9CES6jT0ZzKFkxbFyR2jEgvhxrm6WMabMV4KBwk4K_0024MAe7FKbv3UblWLVLXi1aw.u5ER09FBgDoEuNjNt6mdw_k = !UUi9CES6jT0ZzKFkxbFyR2jEgvhxrm6WMabMV4KBwk4K_0024MAe7FKbv3UblWLVLXi1aw.u5ER09FBgDoEuNjNt6mdw_k;
					if (!UUi9CES6jT0ZzKFkxbFyR2jEgvhxrm6WMabMV4KBwk4K_0024MAe7FKbv3UblWLVLXi1aw.u5ER09FBgDoEuNjNt6mdw_k)
					{
						H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.dPyDIxn1eBQrUn5pGr6HiSQ(global::_003CModule_003E.smethod_28<string>(4081404799u));
					}
					else
					{
						H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.dPyDIxn1eBQrUn5pGr6HiSQ(global::_003CModule_003E.smethod_26<string>(2743011958u));
					}
				}
			}
		});
	}

	internal static IEnumerator OQrGD0dFQowqrabnCwI0QL6cs4cXqPFzIaqBSKdYwXCe()
	{
		yield return null;
		AudioConfiguration audioConfiguration_ = ji7mhhpPdKoRYAmkjomxEky1SNTib1s_ZI_7d77cWwYjpQFQgEHlWkk31ISWaAWHu9ounzTWMQkpaMSMK3em87k.smethod_0();
		audioConfiguration_.numVirtualVoices = 256;
		ji7mhhpPdKoRYAmkjomxEky1SNTib1s_ZI_7d77cWwYjpQFQgEHlWkk31ISWaAWHu9ounzTWMQkpaMSMK3em87k.smethod_1(audioConfiguration_);
	}

	internal static void EbqWYtEBV6CFHD2vyTeKoGu_0024qzVX5dThyNNdYZyFiZZb(bool enabled)
	{
		if (enabled)
		{
			MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.resizable_w = smethod_15();
			MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.resizable_h = smethod_16();
			MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.resizable_full = smethod_17();
		}
	}

	internal static bool smethod_2(string string_0, string string_1)
	{
		return string_0 == string_1;
	}

	internal static string smethod_3(Exception exception_0)
	{
		return exception_0.Message;
	}

	internal static string smethod_4(string string_0, string string_1, string string_2, string string_3)
	{
		return string_0 + string_1 + string_2 + string_3;
	}

	internal static Camera smethod_5()
	{
		return Camera.main;
	}

	internal static DepthTextureMode smethod_6(Camera camera_0)
	{
		return camera_0.depthTextureMode;
	}

	internal static Scene smethod_7()
	{
		return SceneManager.GetActiveScene();
	}

	internal static string smethod_8(Exception exception_0)
	{
		return exception_0.StackTrace;
	}

	internal static string smethod_9(string[] string_0)
	{
		return string.Concat(string_0);
	}

	internal static string smethod_10(string string_0, string string_1)
	{
		return Path.Combine(string_0, string_1);
	}

	internal static bool smethod_11(string string_0)
	{
		return Directory.Exists(string_0);
	}

	internal static DirectoryInfo smethod_12(string string_0)
	{
		return Directory.CreateDirectory(string_0);
	}

	internal static bool smethod_13(string string_0)
	{
		return File.Exists(string_0);
	}

	internal static Coroutine smethod_14(MonoBehaviour monoBehaviour_0, IEnumerator ienumerator_0)
	{
		return monoBehaviour_0.StartCoroutine(ienumerator_0);
	}

	internal static int smethod_15()
	{
		return Screen.width;
	}

	internal static int smethod_16()
	{
		return Screen.height;
	}

	internal static bool smethod_17()
	{
		return Screen.fullScreen;
	}

	internal static Type smethod_18(RuntimeTypeHandle runtimeTypeHandle_0)
	{
		return Type.GetTypeFromHandle(runtimeTypeHandle_0);
	}
}
