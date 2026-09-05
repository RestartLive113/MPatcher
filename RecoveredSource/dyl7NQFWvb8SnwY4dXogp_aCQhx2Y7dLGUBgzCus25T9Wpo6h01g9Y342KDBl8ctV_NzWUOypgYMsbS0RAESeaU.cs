using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using HarmonyLib;
using MPatchrMain;
using McnCraft;
using UnityEngine;
using UnityEngine.UI;

internal static class dyl7NQFWvb8SnwY4dXogp_aCQhx2Y7dLGUBgzCus25T9Wpo6h01g9Y342KDBl8ctV_NzWUOypgYMsbS0RAESeaU
{
	[HarmonyPatch(typeof(Meeting))]
	[HarmonyPatch("BDKIMPEDKCJ")]
	internal static class Class50
	{
		internal static string WCKsvBPB6cSYds0fexVu_00247Y;

		[HarmonyPrefix]
		internal static bool smethod_0(string DPGKEOAGONA, GameObject NGLBLAGMBLN, Meeting __instance)
		{
			mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(smethod_1(global::_003CModule_003E.smethod_28<string>(190352881u), DPGKEOAGONA), bool_0: true);
			if (!smethod_2(DPGKEOAGONA, global::_003CModule_003E.smethod_28<string>(1343975903u)))
			{
				mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(smethod_1(global::_003CModule_003E.smethod_28<string>(4212140474u), WCKsvBPB6cSYds0fexVu_00247Y), bool_0: true);
				if (smethod_3(WCKsvBPB6cSYds0fexVu_00247Y, global::_003CModule_003E.smethod_29<string>(38705087u)))
				{
					if (JKGKJLLFMLE.EGFHGHKLNAO != JKGKJLLFMLE.LENPCAMMAEP.Meeting)
					{
						return false;
					}
					mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_27<string>(4113366500u));
					if (__instance.FICMBCLEFDL.KDODJPCDEHO)
					{
						Class17.DQGZpGELKqeKmXweFYGEcaXR73CqGywHQI6u2TATGL80(global::_003CModule_003E.smethod_26<string>(2456661466u), (Arena)SceneMan.JFAOKFIDAGK);
						MPatchr.ShowDebugMsg(smethod_1(global::_003CModule_003E.smethod_26<string>(1736258292u), xh28Je5vGZzmlyaYTv4leqahX_0024d76HU68fsrFhxkr_aiwJvGWxEZr2ULd9ujX_W1gg.UzhVmO15k8UHVkcjd2D2eFw(xh28Je5vGZzmlyaYTv4leqahX_0024d76HU68fsrFhxkr_aiwJvGWxEZr2ULd9ujX_W1gg.eEsL_CqOa2k0Oy16TqJNFVJPuzSIPe8Trw5760Yzq7e5ghLIwg7ZyOeyUc9A2E9z_0024mUEQCN09awhbakTSEXMPkahOFo0RcQ91HbGZkSM_Bhl.meeting_changeMachineDeniedAttack)));
						return false;
					}
					Text component = smethod_5(smethod_4(NGLBLAGMBLN), 0).GetComponent<Text>();
					if (!(smethod_6((Graphic)component) == Color.yellow))
					{
						smethod_9((SceneMan)__instance, global::_003CModule_003E.smethod_29<string>(1347799951u), bool_0: false);
						if (!(smethod_10() - float_0 >= 10f))
						{
							Class17.DQGZpGELKqeKmXweFYGEcaXR73CqGywHQI6u2TATGL80(global::_003CModule_003E.smethod_29<string>(2866889846u), (Arena)SceneMan.JFAOKFIDAGK);
							MPatchr.ShowDebugMsg(smethod_1(global::_003CModule_003E.smethod_25<string>(3601821467u), smethod_11(xh28Je5vGZzmlyaYTv4leqahX_0024d76HU68fsrFhxkr_aiwJvGWxEZr2ULd9ujX_W1gg.UzhVmO15k8UHVkcjd2D2eFw(xh28Je5vGZzmlyaYTv4leqahX_0024d76HU68fsrFhxkr_aiwJvGWxEZr2ULd9ujX_W1gg.eEsL_CqOa2k0Oy16TqJNFVJPuzSIPe8Trw5760Yzq7e5ghLIwg7ZyOeyUc9A2E9z_0024mUEQCN09awhbakTSEXMPkahOFo0RcQ91HbGZkSM_Bhl.meeting_changeMachineTimeout), (object)10f, (object)(int)(10f - (smethod_10() - float_0)))));
							return false;
						}
						MPatcherFork.CustomPatches.LegacyMachineChangeIngame.BeginSelectionTransaction(__instance);
						JKGKJLLFMLE.IGOBPLOLHEP.machineName = smethod_7(component);
						smethod_12(bool_0: false);
						bool isReady = JKGKJLLFMLE.HHGILAIOCLG.isReady;
						smethod_13(JKGKJLLFMLE.HHGILAIOCLG, smethod_3(JKGKJLLFMLE.IGOBPLOLHEP.folderName, global::_003CModule_003E.smethod_27<string>(4150523406u)), bool_1: true);
						smethod_14(JKGKJLLFMLE.HHGILAIOCLG);
						if (!isReady)
						{
							MPatcherFork.CustomPatches.LegacyMachineChangeIngame.RollbackSelectionTransaction("load-failed");
							Class17.DQGZpGELKqeKmXweFYGEcaXR73CqGywHQI6u2TATGL80(global::_003CModule_003E.smethod_28<string>(374695788u), (Arena)SceneMan.JFAOKFIDAGK);
							MPatchr.ShowDebugMsg(smethod_1(global::_003CModule_003E.smethod_27<string>(3855128093u), xh28Je5vGZzmlyaYTv4leqahX_0024d76HU68fsrFhxkr_aiwJvGWxEZr2ULd9ujX_W1gg.UzhVmO15k8UHVkcjd2D2eFw(xh28Je5vGZzmlyaYTv4leqahX_0024d76HU68fsrFhxkr_aiwJvGWxEZr2ULd9ujX_W1gg.eEsL_CqOa2k0Oy16TqJNFVJPuzSIPe8Trw5760Yzq7e5ghLIwg7ZyOeyUc9A2E9z_0024mUEQCN09awhbakTSEXMPkahOFo0RcQ91HbGZkSM_Bhl.meeting_changeMachineFailLoad)));
						}
						else if ((HNJDDKJLHMM.FHLGOMHPDLN == HNJDDKJLHMM.HKGAACMIPIH.Legacy)
							? MPatcherFork.CustomPatches.LegacyMachineChangeIngame.ValidateCurrentSelection()
							: dyl7NQFWvb8SnwY4dXogp_aCQhx2Y7dLGUBgzCus25T9Wpo6h01g9Y342KDBl8ctV_NzWUOypgYMsbS0RAESeaU.smethod_0())
						{
							mDZmboOC2mLx2MmBzu_00244bNw(__instance);
							MPatcherFork.CustomPatches.LegacyMachineChangeIngame.CommitSelectionTransaction();
							float_0 = smethod_10();
							zAOrzM_2ysNo3jthAClNSQ3POH9nkmbIAQjQeFsGY2hYwlsUfYcNDVio3ZwNGLR_00245A.smethod_0();
						}
						else
						{
							MPatcherFork.CustomPatches.LegacyMachineChangeIngame.RollbackSelectionTransaction("regulation-denied");
							Class17.DQGZpGELKqeKmXweFYGEcaXR73CqGywHQI6u2TATGL80(global::_003CModule_003E.smethod_29<string>(2866889846u), (Arena)SceneMan.JFAOKFIDAGK);
							MPatchr.ShowDebugMsg(smethod_1(global::_003CModule_003E.smethod_25<string>(3601821467u), xh28Je5vGZzmlyaYTv4leqahX_0024d76HU68fsrFhxkr_aiwJvGWxEZr2ULd9ujX_W1gg.UzhVmO15k8UHVkcjd2D2eFw(xh28Je5vGZzmlyaYTv4leqahX_0024d76HU68fsrFhxkr_aiwJvGWxEZr2ULd9ujX_W1gg.eEsL_CqOa2k0Oy16TqJNFVJPuzSIPe8Trw5760Yzq7e5ghLIwg7ZyOeyUc9A2E9z_0024mUEQCN09awhbakTSEXMPkahOFo0RcQ91HbGZkSM_Bhl.meeting_changeMachineNoMatchRegulation)));
						}
					}
					else
					{
						JKGKJLLFMLE.CFGKIAPCDLB = ((!smethod_3(smethod_7(component), global::_003CModule_003E.smethod_28<string>(660952542u))) ? smethod_7(component) : string.Empty);
						Class17.DQGZpGELKqeKmXweFYGEcaXR73CqGywHQI6u2TATGL80(global::_003CModule_003E.smethod_29<string>(3868517653u), (SceneMan)__instance, new Type[3]
						{
							smethod_8(typeof(string).TypeHandle),
							smethod_8(typeof(string).TypeHandle),
							smethod_8(typeof(string).TypeHandle)
						}, new object[3]
						{
							JKGKJLLFMLE.CFGKIAPCDLB,
							JKGKJLLFMLE.IGOBPLOLHEP.machineName,
							null
						});
					}
					return false;
				}
				return true;
			}
			WCKsvBPB6cSYds0fexVu_00247Y = DPGKEOAGONA;
			return true;
		}

		internal static string smethod_1(string string_0, string string_1)
		{
			return string_0 + string_1;
		}

		internal static bool smethod_2(string string_0, string string_1)
		{
			return string_0 != string_1;
		}

		internal static bool smethod_3(string string_0, string string_1)
		{
			return string_0 == string_1;
		}

		internal static Transform smethod_4(GameObject gameObject_0)
		{
			return gameObject_0.transform;
		}

		internal static Transform smethod_5(Transform transform_0, int int_0)
		{
			return transform_0.GetChild(int_0);
		}

		internal static Color smethod_6(Graphic graphic_0)
		{
			return graphic_0.color;
		}

		internal static string smethod_7(Text text_0)
		{
			return text_0.text;
		}

		internal static Type smethod_8(RuntimeTypeHandle runtimeTypeHandle_0)
		{
			return Type.GetTypeFromHandle(runtimeTypeHandle_0);
		}

		internal static void smethod_9(SceneMan sceneMan_0, string string_0, bool bool_0)
		{
			sceneMan_0.ValidatePNL(string_0, bool_0);
		}

		internal static float smethod_10()
		{
			return Time.realtimeSinceStartup;
		}

		internal static string smethod_11(string string_0, object object_0, object object_1)
		{
			return string.Format(string_0, object_0, object_1);
		}

		internal static bool smethod_12(bool bool_0)
		{
			return JKGKJLLFMLE.MIONNHPELLN(bool_0);
		}

		internal static void smethod_13(BuildData buildData_0, bool bool_0, bool bool_1)
		{
			buildData_0.CorrectSpeedLimit(bool_0, bool_1);
		}

		internal static void smethod_14(BuildData buildData_0)
		{
			buildData_0.CorrectMagnification();
		}
	}

	[CompilerGenerated]
	private sealed class Class51
	{
		public Meeting wQ6mrkDog7tAEXGul0Y8Sv0;

		internal void jIRyM_0024i36X8Eez5_ofM3lUpaHGtQYI5vAolJjXJUxQhY(lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw button)
		{
			if (wQ6mrkDog7tAEXGul0Y8Sv0.FICMBCLEFDL.KDODJPCDEHO)
			{
				Class17.DQGZpGELKqeKmXweFYGEcaXR73CqGywHQI6u2TATGL80(global::_003CModule_003E.smethod_28<string>(374695788u), (Arena)SceneMan.JFAOKFIDAGK);
				MPatchr.ShowDebugMsg(smethod_0(global::_003CModule_003E.smethod_25<string>(3601821467u), xh28Je5vGZzmlyaYTv4leqahX_0024d76HU68fsrFhxkr_aiwJvGWxEZr2ULd9ujX_W1gg.UzhVmO15k8UHVkcjd2D2eFw(xh28Je5vGZzmlyaYTv4leqahX_0024d76HU68fsrFhxkr_aiwJvGWxEZr2ULd9ujX_W1gg.eEsL_CqOa2k0Oy16TqJNFVJPuzSIPe8Trw5760Yzq7e5ghLIwg7ZyOeyUc9A2E9z_0024mUEQCN09awhbakTSEXMPkahOFo0RcQ91HbGZkSM_Bhl.meeting_changeMachineDeniedAttack)));
				return;
			}
			Class50.WCKsvBPB6cSYds0fexVu_00247Y = global::_003CModule_003E.smethod_26<string>(3313619905u);
			smethod_1((SceneMan)wQ6mrkDog7tAEXGul0Y8Sv0, global::_003CModule_003E.smethod_26<string>(1305688209u), bool_0: true);
			Class17.DQGZpGELKqeKmXweFYGEcaXR73CqGywHQI6u2TATGL80(global::_003CModule_003E.smethod_25<string>(4020490498u), (SceneMan)wQ6mrkDog7tAEXGul0Y8Sv0, new Type[3]
			{
				smethod_2(typeof(string).TypeHandle),
				smethod_2(typeof(string).TypeHandle),
				smethod_2(typeof(string).TypeHandle)
			}, new object[3]
			{
				JKGKJLLFMLE.CFGKIAPCDLB,
				JKGKJLLFMLE.IGOBPLOLHEP.machineName,
				null
			});
		}

		internal static string smethod_0(string string_0, string string_1)
		{
			return string_0 + string_1;
		}

		internal static void smethod_1(SceneMan sceneMan_0, string string_0, bool bool_0)
		{
			sceneMan_0.ValidatePNL(string_0, bool_0);
		}

		internal static Type smethod_2(RuntimeTypeHandle runtimeTypeHandle_0)
		{
			return Type.GetTypeFromHandle(runtimeTypeHandle_0);
		}
	}

	[CompilerGenerated]
	private sealed class Class52 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int SjlBM8inVA_YE4YVlr_0024gluY;

		private object yT7HpVIzmqW54W307WgJtr4;

		public Meeting wQ6mrkDog7tAEXGul0Y8Sv0;

		public Vector3 sC7TVlJaywRg_0024a_5dqujETk;

		public Quaternion w9K4d9QT8cXOAZKHQEp0fRg;

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
		public Class52(int _003C_003E1__state)
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
			default:
				return false;
			case 1:
			{
				SjlBM8inVA_YE4YVlr_0024gluY = -1;
				bool flag = true;
				foreach (GameObject item in wQ6mrkDog7tAEXGul0Y8Sv0.FICMBCLEFDL.KBLANAFAJFP)
				{
					SledController[] componentsInChildren = item.GetComponentsInChildren<SledController>();
					foreach (SledController object_ in componentsInChildren)
					{
						if (smethod_1((UnityEngine.Object)Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<ContactSensor>(global::_003CModule_003E.smethod_25<string>(3853601973u), object_), (UnityEngine.Object)null))
						{
							flag = false;
						}
					}
				}
				if (flag)
				{
					smethod_2(wQ6mrkDog7tAEXGul0Y8Sv0.FICMBCLEFDL, sC7TVlJaywRg_0024a_5dqujETk, w9K4d9QT8cXOAZKHQEp0fRg, bool_0: true);
					return false;
				}
				break;
			}
			case 0:
				SjlBM8inVA_YE4YVlr_0024gluY = -1;
				break;
			}
			yT7HpVIzmqW54W307WgJtr4 = smethod_0(1f);
			SjlBM8inVA_YE4YVlr_0024gluY = 1;
			return true;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw smethod_3();
		}

		internal static WaitForSeconds smethod_0(float float_0)
		{
			return new WaitForSeconds(float_0);
		}

		internal static bool smethod_1(UnityEngine.Object object_0, UnityEngine.Object object_1)
		{
			return object_0 == object_1;
		}

		internal static void smethod_2(MachineController machineController_0, Vector3 vector3_0, Quaternion quaternion_0, bool bool_0)
		{
			machineController_0.Warp(vector3_0, quaternion_0, bool_0);
		}

		internal static NotSupportedException smethod_3()
		{
			return new NotSupportedException();
		}
	}

	private static float float_0 = -2.1474836E+09f;

	private const float vhf2C7cgPKiVMXM1QgChE1o = 10f;

	internal static void KKCHLiHZDjufqY4nZJceFC4(Meeting __instance)
	{
		if (JKGKJLLFMLE.EGFHGHKLNAO != JKGKJLLFMLE.LENPCAMMAEP.Meeting)
		{
			return;
		}
		Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.P3tLroX6fcPfQha_0024JdbpYXg(global::_003CModule_003E.smethod_26<string>(3313619905u), new Vector3(155f, 230f), global::_003CModule_003E.smethod_25<string>(1522640263u), delegate
		{
			if (__instance.FICMBCLEFDL.KDODJPCDEHO)
			{
				Class17.DQGZpGELKqeKmXweFYGEcaXR73CqGywHQI6u2TATGL80(global::_003CModule_003E.smethod_28<string>(374695788u), (Arena)SceneMan.JFAOKFIDAGK);
				MPatchr.ShowDebugMsg(Class51.smethod_0(global::_003CModule_003E.smethod_25<string>(3601821467u), xh28Je5vGZzmlyaYTv4leqahX_0024d76HU68fsrFhxkr_aiwJvGWxEZr2ULd9ujX_W1gg.UzhVmO15k8UHVkcjd2D2eFw(xh28Je5vGZzmlyaYTv4leqahX_0024d76HU68fsrFhxkr_aiwJvGWxEZr2ULd9ujX_W1gg.eEsL_CqOa2k0Oy16TqJNFVJPuzSIPe8Trw5760Yzq7e5ghLIwg7ZyOeyUc9A2E9z_0024mUEQCN09awhbakTSEXMPkahOFo0RcQ91HbGZkSM_Bhl.meeting_changeMachineDeniedAttack)));
			}
			else
			{
				Class50.WCKsvBPB6cSYds0fexVu_00247Y = global::_003CModule_003E.smethod_26<string>(3313619905u);
				Class51.smethod_1((SceneMan)__instance, global::_003CModule_003E.smethod_26<string>(1305688209u), bool_0: true);
				Class17.DQGZpGELKqeKmXweFYGEcaXR73CqGywHQI6u2TATGL80(global::_003CModule_003E.smethod_25<string>(4020490498u), (SceneMan)__instance, new Type[3]
				{
					Class51.smethod_2(typeof(string).TypeHandle),
					Class51.smethod_2(typeof(string).TypeHandle),
					Class51.smethod_2(typeof(string).TypeHandle)
				}, new object[3]
				{
					JKGKJLLFMLE.CFGKIAPCDLB,
					JKGKJLLFMLE.IGOBPLOLHEP.machineName,
					null
				});
			}
		}, GameObject.Find(global::_003CModule_003E.smethod_27<string>(862174272u)).smethod_0(global::_003CModule_003E.smethod_27<string>(565088109u)).transform);
	}

	internal static bool smethod_0()
	{
		List<GameObject> list = new List<GameObject>();
		bool flag = true;
		try
		{
			BuildData hHGILAIOCLG = JKGKJLLFMLE.HHGILAIOCLG;
			HDBLLPODNLN hDBLLPODNLN = smethod_1();
			HIPBCCKFFAG hIPBCCKFFAG = smethod_2();
			smethod_3(hDBLLPODNLN, bool_0: false);
			foreach (BlockData blockDatum in hHGILAIOCLG.blockData)
			{
				GameObject gameObject = smethod_4(blockDatum, bool_0: false);
				if (smethod_5((UnityEngine.Object)gameObject))
				{
					list.Add(gameObject);
					smethod_6(gameObject, bool_0: false);
					smethod_7(hDBLLPODNLN, gameObject.GetComponent<BlockController>());
				}
			}
			smethod_8(hDBLLPODNLN, (BlockController)null);
			smethod_9(hDBLLPODNLN);
			smethod_10(hDBLLPODNLN);
			if (hHGILAIOCLG.size == 0)
			{
				try
				{
					smethod_11(hIPBCCKFFAG, bool_0: true, bool_1: false);
				}
				catch (Exception)
				{
				}
				hHGILAIOCLG.size = hIPBCCKFFAG.CBEGHPGKNNI;
				hHGILAIOCLG.spawnAltOffset = Mathf.RoundToInt(0f - hIPBCCKFFAG.MFGJHOHNCDB.min.y) + 1;
				hIPBCCKFFAG.MEDPEFNEGIG(BHCKMFDEBBH: false);
				JKGKJLLFMLE.BOMAFGLNGMI();
			}
			if (JKGKJLLFMLE.IGOBPLOLHEP.photonRegion == 4)
			{
				return false;
			}
			if (hDBLLPODNLN.KADEOCMCJLA() > 65)
			{
				DP.D(global::_003CModule_003E.smethod_25<string>(2487140579u) + hDBLLPODNLN.KADEOCMCJLA() + global::_003CModule_003E.smethod_26<string>(3230292837u));
				flag = false;
			}
			JKGKJLLFMLE.LENPCAMMAEP lENPCAMMAEP = (JKGKJLLFMLE.LENPCAMMAEP)int.Parse(xL4cQz4BzhEm_1ut8KutgRI(global::_003CModule_003E.smethod_26<string>(1905559551u)));
			bool flag2 = !JKGKJLLFMLE.IGOBPLOLHEP.machineName.StartsWith(global::_003CModule_003E.smethod_25<string>(2882791292u));
			if (lENPCAMMAEP != JKGKJLLFMLE.LENPCAMMAEP.BossHunt && lENPCAMMAEP != JKGKJLLFMLE.LENPCAMMAEP.Meeting)
			{
				flag2 = true;
			}
			if (flag2)
			{
				if (int.TryParse(xL4cQz4BzhEm_1ut8KutgRI(global::_003CModule_003E.smethod_29<string>(3630388938u)), out var result))
				{
					flag &= hDBLLPODNLN.OHNPKPMDHGK() <= result;
				}
				if (int.TryParse(xL4cQz4BzhEm_1ut8KutgRI(global::_003CModule_003E.smethod_27<string>(3517672409u)), out result))
				{
					flag &= JKGKJLLFMLE.HHGILAIOCLG.size <= result;
				}
				if (int.TryParse(xL4cQz4BzhEm_1ut8KutgRI(global::_003CModule_003E.smethod_26<string>(3432340090u)), out result))
				{
					flag &= hDBLLPODNLN.PCFKNOAKFHD >= (float)result;
				}
				if (int.TryParse(xL4cQz4BzhEm_1ut8KutgRI(global::_003CModule_003E.smethod_27<string>(1972786365u)), out result))
				{
					flag &= hDBLLPODNLN.PCFKNOAKFHD <= (float)result;
				}
				flag = (flag = (flag = (flag = (flag = (flag = (flag = (flag = (flag = (flag = (flag = (flag = (flag &= nA1tBImRNmCXvYQ17Rt4LBY(BlockData.AAHMDBHDCDK.JointTS, global::_003CModule_003E.smethod_25<string>(1876975657u), hDBLLPODNLN)) & nA1tBImRNmCXvYQ17Rt4LBY(BlockData.AAHMDBHDCDK.Thruster, global::_003CModule_003E.smethod_29<string>(3349075560u), hDBLLPODNLN)) & nA1tBImRNmCXvYQ17Rt4LBY(BlockData.AAHMDBHDCDK.AGDevice, global::_003CModule_003E.smethod_29<string>(1574825966u), hDBLLPODNLN)) & nA1tBImRNmCXvYQ17Rt4LBY(BlockData.AAHMDBHDCDK.Wheel, global::_003CModule_003E.smethod_27<string>(3177981573u), hDBLLPODNLN)) & nA1tBImRNmCXvYQ17Rt4LBY(BlockData.AAHMDBHDCDK.Shaft, global::_003CModule_003E.smethod_26<string>(4073874966u), hDBLLPODNLN)) & nA1tBImRNmCXvYQ17Rt4LBY(BlockData.AAHMDBHDCDK.Mover, global::_003CModule_003E.smethod_26<string>(1911829968u), hDBLLPODNLN)) & nA1tBImRNmCXvYQ17Rt4LBY(BlockData.AAHMDBHDCDK.Cannon1, BlockData.AAHMDBHDCDK.Cannon2, global::_003CModule_003E.smethod_27<string>(1386466043u), hDBLLPODNLN)) & nA1tBImRNmCXvYQ17Rt4LBY(BlockData.AAHMDBHDCDK.Sword, global::_003CModule_003E.smethod_29<string>(3176336908u), hDBLLPODNLN)) & nA1tBImRNmCXvYQ17Rt4LBY(BlockData.AAHMDBHDCDK.Discharger, global::_003CModule_003E.smethod_28<string>(3832897128u), hDBLLPODNLN)) & nA1tBImRNmCXvYQ17Rt4LBY(BlockData.AAHMDBHDCDK.Launcher, global::_003CModule_003E.smethod_27<string>(3364104273u), hDBLLPODNLN)) & nA1tBImRNmCXvYQ17Rt4LBY(BlockData.AAHMDBHDCDK.Beamer, global::_003CModule_003E.smethod_29<string>(2148555422u), hDBLLPODNLN)) & nA1tBImRNmCXvYQ17Rt4LBY(BlockData.AAHMDBHDCDK.Shield, global::_003CModule_003E.smethod_25<string>(377003443u), hDBLLPODNLN)) & nA1tBImRNmCXvYQ17Rt4LBY(BlockData.AAHMDBHDCDK.Tracker, global::_003CModule_003E.smethod_29<string>(2895023530u), hDBLLPODNLN);
				if (int.TryParse(xL4cQz4BzhEm_1ut8KutgRI(global::_003CModule_003E.smethod_28<string>(1541247654u)), out result))
				{
					flag &= hDBLLPODNLN.GPKANGPHHMC <= result;
				}
				if (int.TryParse(xL4cQz4BzhEm_1ut8KutgRI(global::_003CModule_003E.smethod_25<string>(3534271533u)), out result))
				{
					flag &= hDBLLPODNLN.CAACNMEKOOC <= result;
				}
			}
			flag &= JKGKJLLFMLE.IGOBPLOLHEP.isExpert || int.Parse(xL4cQz4BzhEm_1ut8KutgRI(global::_003CModule_003E.smethod_25<string>(1869038043u))) != 5;
		}
		catch (Exception ex2)
		{
			mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_25<string>(203804553u) + ex2.Message + global::_003CModule_003E.smethod_26<string>(3443209972u) + ex2.StackTrace);
		}
		foreach (GameObject item in list)
		{
			UnityEngine.Object.Destroy(item);
		}
		return flag;
	}

	private static string xL4cQz4BzhEm_1ut8KutgRI(string key)
	{
		object value = null;
		if (fYYx8vN1tah7ZgfygXMH9eJU0YzjaUsqtv9tADffziMcu1k1It8P_0024ZciTdZuxN09bA.MCJUb7mzEcz9seOWPbquoos.bDL22TQxAdGVLPWswOwgdOWuG2AEuTx3OXpQHzZV6eVL.TryGetValue(key, out value))
		{
			return smethod_12(value);
		}
		return string.Empty;
	}

	private static bool nA1tBImRNmCXvYQ17Rt4LBY(BlockData.AAHMDBHDCDK GGJJFDKGDMB, string IIECBLOPIII, HDBLLPODNLN EPGELCMKKOC)
	{
		if (!smethod_13(xL4cQz4BzhEm_1ut8KutgRI(IIECBLOPIII), string.Empty))
		{
			if (int.TryParse(xL4cQz4BzhEm_1ut8KutgRI(IIECBLOPIII), out var result))
			{
				return EPGELCMKKOC.BGIDJHJBICM[(int)GGJJFDKGDMB] <= result;
			}
			return false;
		}
		return true;
	}

	private static bool nA1tBImRNmCXvYQ17Rt4LBY(BlockData.AAHMDBHDCDK FJDONLDCCKE, BlockData.AAHMDBHDCDK HELLCJLPFAJ, string IIECBLOPIII, HDBLLPODNLN EPGELCMKKOC)
	{
		if (!smethod_13(xL4cQz4BzhEm_1ut8KutgRI(IIECBLOPIII), string.Empty))
		{
			if (!int.TryParse(xL4cQz4BzhEm_1ut8KutgRI(IIECBLOPIII), out var result))
			{
				return false;
			}
			return EPGELCMKKOC.BGIDJHJBICM[(int)FJDONLDCCKE] + EPGELCMKKOC.BGIDJHJBICM[(int)HELLCJLPFAJ] <= result;
		}
		return true;
	}

	internal static void mDZmboOC2mLx2MmBzu_00244bNw(Meeting __instance)
	{
		Vector3 pos = smethod_15(smethod_14(__instance.JPIAFJHAPHM));
		Quaternion rot = smethod_16(smethod_14(__instance.JPIAFJHAPHM));
		smethod_19(smethod_18((Component)smethod_17(smethod_14(__instance.JPIAFJHAPHM))));
		smethod_20((UnityEngine.Object)smethod_18((Component)smethod_17(smethod_14(__instance.JPIAFJHAPHM))));
		smethod_21();
		if (HNJDDKJLHMM.FHLGOMHPDLN == HNJDDKJLHMM.HKGAACMIPIH.Legacy)
		{
			__instance.JPIAFJHAPHM = (GameObject)smethod_22((UnityEngine.Object)__instance.MJBCDCIENAO, Vector3.zero, Quaternion.identity, 1);
		}
		else
		{
			__instance.JPIAFJHAPHM = smethod_23(global::_003CModule_003E.smethod_29<string>(3219521571u), Vector3.zero, Quaternion.identity, 0);
		}
		if (smethod_24((UnityEngine.Object)__instance.JPIAFJHAPHM, (UnityEngine.Object)null))
		{
			smethod_25((Arena)__instance);
		}
		__instance.FICMBCLEFDL = __instance.JPIAFJHAPHM.GetComponent<MachineController>();
		smethod_26(__instance.FICMBCLEFDL, global::_003CModule_003E.smethod_29<string>(2690202915u), JKGKJLLFMLE.HHGILAIOCLG, JKGKJLLFMLE.MIIGKEBFKKD, Vector3.up * 4000f);
		smethod_27((UnityEngine.Object)__instance.FICMBCLEFDL, global::_003CModule_003E.smethod_25<string>(2264688756u));
		Class17.DQGZpGELKqeKmXweFYGEcaXR73CqGywHQI6u2TATGL80(global::_003CModule_003E.smethod_28<string>(2360786763u), (Arena)__instance, new object[0]);
		__instance.FICMBCLEFDL.KDODJPCDEHO = false;
		smethod_28(__instance.FICMBCLEFDL);
		smethod_29((MonoBehaviour)MPatchr.xcBvxcM_0024ckBeZyvdSoAkJoM, Class17.DQGZpGELKqeKmXweFYGEcaXR73CqGywHQI6u2TATGL80<Game, IEnumerator>(global::_003CModule_003E.smethod_29<string>(1163958599u), (Game)SceneMan.JFAOKFIDAGK, new object[0]));
		Class17.DQGZpGELKqeKmXweFYGEcaXR73CqGywHQI6u2TATGL80(global::_003CModule_003E.smethod_29<string>(1910426707u), SceneMan.JFAOKFIDAGK, global::_003CModule_003E.smethod_29<string>(2656894815u), false);
		smethod_31(smethod_30(SceneMan.JFAOKFIDAGK, global::_003CModule_003E.smethod_29<string>(2656894815u)).GetComponent<Toggle>(), bool_0: false);
		Class17.DMoImhPC2jNUt2tgmSezeT_IK_0024XK5Df9PmexG_CaKb1R(global::_003CModule_003E.smethod_25<string>(3624839785u), (object)__instance, gparam_0: false);
		((Arena)SceneMan.JFAOKFIDAGK).BOIEJCIBHKI.FMGOKAGJMJH = true;
		Class17.DQGZpGELKqeKmXweFYGEcaXR73CqGywHQI6u2TATGL80(global::_003CModule_003E.smethod_29<string>(2866889846u), (Arena)SceneMan.JFAOKFIDAGK);
		((Arena)SceneMan.JFAOKFIDAGK).BOIEJCIBHKI.ONGNOMCJBGE = false;
		smethod_29((MonoBehaviour)__instance, MPatcherFork.CustomPatches.LegacyMachineChangeIngame.FinalizeAcceptedReplacement(__instance.FICMBCLEFDL, pos, rot));
		WD8ZhiSkVbF6OX9KtGVjskMcpDKnQtgHa3jD9xYxskDb(UnityEngine.Object.FindObjectOfType<RideCameraController>());
	}

	private static void WD8ZhiSkVbF6OX9KtGVjskMcpDKnQtgHa3jD9xYxskDb(RideCameraController cam)
	{
		smethod_32();
		cam.FEBMBJPBPCI = smethod_33(global::_003CModule_003E.smethod_28<string>(3544565476u));
		if (smethod_5((UnityEngine.Object)cam.FEBMBJPBPCI))
		{
			Texture2D gJIFEBLBNKA = JKGKJLLFMLE.GJIFEBLBNKA;
			smethod_6(cam.FEBMBJPBPCI, smethod_34((UnityEngine.Object)gJIFEBLBNKA, (UnityEngine.Object)null));
			if (smethod_5((UnityEngine.Object)gJIFEBLBNKA))
			{
				float num = Mathf.Max(1280f / (float)smethod_35(), 720f / (float)smethod_36());
				cam.FEBMBJPBPCI.GetComponent<RectTransform>().sizeDelta = new Vector2(smethod_37((Texture)gJIFEBLBNKA), smethod_38((Texture)gJIFEBLBNKA)) * num;
				cam.FEBMBJPBPCI.GetComponent<Image>().sprite = Sprite.Create(gJIFEBLBNKA, new Rect(0f, 0f, gJIFEBLBNKA.width, gJIFEBLBNKA.height), Vector2.zero);
			}
			else
			{
				cam.FEBMBJPBPCI = null;
			}
		}
		cam.BAGPLJNELME = cam.transform.Find(global::_003CModule_003E.smethod_26<string>(705816867u)).gameObject;
		if (!HNJDDKJLHMM.NLNLBIHKELD())
		{
			cam.BAGPLJNELME.SetActive(value: true);
			Texture2D iNGKCFBOPMC = JKGKJLLFMLE.INGKCFBOPMC;
			cam.BAGPLJNELME.SetActive(iNGKCFBOPMC != null);
			if (!iNGKCFBOPMC)
			{
				cam.BAGPLJNELME = null;
			}
			else
			{
				cam.BAGPLJNELME.GetComponent<MeshRenderer>().material.SetTexture(global::_003CModule_003E.smethod_27<string>(3387454153u), iNGKCFBOPMC);
			}
		}
		Texture2D aBLLFAFJLFM = JKGKJLLFMLE.ABLLFAFJLFM;
		if ((bool)aBLLFAFJLFM)
		{
			int width = aBLLFAFJLFM.width;
			int height = aBLLFAFJLFM.height;
			if (width < height)
			{
				cam.HBGINIPKAJM = GameObject.Find(global::_003CModule_003E.smethod_29<string>(2515011989u));
				if ((bool)cam.HBGINIPKAJM)
				{
					cam.HBGINIPKAJM.SetActive(value: true);
					cam.HBGINIPKAJM.GetComponent<MeshRenderer>().material.SetTexture(global::_003CModule_003E.smethod_27<string>(3387454153u), aBLLFAFJLFM);
				}
				GameObject gameObject = GameObject.Find(global::_003CModule_003E.smethod_29<string>(4289261583u));
				if ((bool)gameObject)
				{
					gameObject.SetActive(value: false);
				}
				return;
			}
			cam.EFJKGEILMGO = GameObject.Find(global::_003CModule_003E.smethod_28<string>(736712287u));
			if ((bool)cam.EFJKGEILMGO)
			{
				cam.EFJKGEILMGO.SetActive(value: true);
				float num2 = Mathf.Max(1280f / (float)Screen.width, 720f / (float)Screen.height);
				cam.EFJKGEILMGO.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height) * num2;
				cam.EFJKGEILMGO.GetComponent<Image>().sprite = Sprite.Create(aBLLFAFJLFM, new Rect(0f, 0f, width, height), Vector2.zero);
			}
			GameObject gameObject2 = GameObject.Find(global::_003CModule_003E.smethod_29<string>(2515011989u));
			if ((bool)gameObject2)
			{
				gameObject2.SetActive(value: false);
			}
		}
		else
		{
			GameObject gameObject3 = GameObject.Find(global::_003CModule_003E.smethod_28<string>(554592485u));
			if ((bool)gameObject3)
			{
				gameObject3.SetActive(value: false);
			}
			gameObject3 = GameObject.Find(global::_003CModule_003E.smethod_26<string>(359967761u));
			if ((bool)gameObject3)
			{
				gameObject3.SetActive(value: false);
			}
		}
	}

	internal static IEnumerator jtGxiEkY_0024y2_0024s5dspSD1ay8(Meeting __instance, Vector3 pos, Quaternion rot)
	{
		bool flag;
		do
		{
			yield return Class52.smethod_0(1f);
			flag = true;
			foreach (GameObject item in __instance.FICMBCLEFDL.KBLANAFAJFP)
			{
				SledController[] componentsInChildren = item.GetComponentsInChildren<SledController>();
				foreach (SledController object_ in componentsInChildren)
				{
					if (Class52.smethod_1((UnityEngine.Object)Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<ContactSensor>(global::_003CModule_003E.smethod_25<string>(3853601973u), object_), (UnityEngine.Object)null))
					{
						flag = false;
					}
				}
			}
		}
		while (!flag);
		Class52.smethod_2(__instance.FICMBCLEFDL, pos, rot, bool_0: true);
	}

	internal static HDBLLPODNLN smethod_1()
	{
		return new HDBLLPODNLN();
	}

	internal static HIPBCCKFFAG smethod_2()
	{
		return new HIPBCCKFFAG();
	}

	internal static void smethod_3(HDBLLPODNLN hdbllpodnln_0, bool bool_0)
	{
		hdbllpodnln_0.AFJJGAHKLKD(bool_0);
	}

	internal static GameObject smethod_4(BlockData blockData_0, bool bool_0)
	{
		return PAEHEMJNPND.PKLHNJNFKFH(blockData_0, bool_0);
	}

	internal static bool smethod_5(UnityEngine.Object object_0)
	{
		return object_0;
	}

	internal static void smethod_6(GameObject gameObject_0, bool bool_0)
	{
		gameObject_0.SetActive(bool_0);
	}

	internal static void smethod_7(HDBLLPODNLN hdbllpodnln_0, BlockController blockController_0)
	{
		hdbllpodnln_0.HDLEKABOEFL(blockController_0);
	}

	internal static void smethod_8(HDBLLPODNLN hdbllpodnln_0, BlockController blockController_0)
	{
		hdbllpodnln_0.ANBKLJFHMOB(blockController_0);
	}

	internal static int smethod_9(HDBLLPODNLN hdbllpodnln_0)
	{
		return hdbllpodnln_0.JKAJGAGDMAJ();
	}

	internal static void smethod_10(HDBLLPODNLN hdbllpodnln_0)
	{
		hdbllpodnln_0.ACBKKKLCJCH();
	}

	internal static void smethod_11(HIPBCCKFFAG hipbcckffag_0, bool bool_0, bool bool_1)
	{
		hipbcckffag_0.ACMGPBMMKNI(bool_0, bool_1);
	}

	internal static string smethod_12(object object_0)
	{
		return object_0.ToString();
	}

	internal static bool smethod_13(string string_0, string string_1)
	{
		return string_0 == string_1;
	}

	internal static Transform smethod_14(GameObject gameObject_0)
	{
		return gameObject_0.transform;
	}

	internal static Vector3 smethod_15(Transform transform_0)
	{
		return transform_0.position;
	}

	internal static Quaternion smethod_16(Transform transform_0)
	{
		return transform_0.rotation;
	}

	internal static Transform smethod_17(Transform transform_0)
	{
		return transform_0.parent;
	}

	internal static GameObject smethod_18(Component component_0)
	{
		return component_0.gameObject;
	}

	internal static void smethod_19(GameObject gameObject_0)
	{
		JONBPAFNPBD.JFIKNOAGBFB(gameObject_0);
	}

	internal static void smethod_20(UnityEngine.Object object_0)
	{
		UnityEngine.Object.Destroy(object_0);
	}

	internal static void smethod_21()
	{
		AutoPilot.ClearDebugInfo();
	}

	internal static UnityEngine.Object smethod_22(UnityEngine.Object object_0, Vector3 vector3_0, Quaternion quaternion_0, int int_0)
	{
		return Network.Instantiate(object_0, vector3_0, quaternion_0, int_0);
	}

	internal static GameObject smethod_23(string string_0, Vector3 vector3_0, Quaternion quaternion_0, int int_0)
	{
		return JONBPAFNPBD.PDHPKOEKACB(string_0, vector3_0, quaternion_0, int_0);
	}

	internal static bool smethod_24(UnityEngine.Object object_0, UnityEngine.Object object_1)
	{
		return object_0 == object_1;
	}

	internal static void smethod_25(Arena arena_0)
	{
		arena_0.Exit();
	}

	internal static void smethod_26(MachineController machineController_0, string string_0, BuildData buildData_0, AssignData assignData_0, Vector3 vector3_0)
	{
		machineController_0.Initialize(string_0, buildData_0, assignData_0, vector3_0);
	}

	internal static void smethod_27(UnityEngine.Object object_0, string string_0)
	{
		object_0.name = string_0;
	}

	internal static void smethod_28(MachineController machineController_0)
	{
		machineController_0.SyncTotalWeight();
	}

	internal static Coroutine smethod_29(MonoBehaviour monoBehaviour_0, IEnumerator ienumerator_0)
	{
		return monoBehaviour_0.StartCoroutine(ienumerator_0);
	}

	internal static GameObject smethod_30(SceneMan sceneMan_0, string string_0)
	{
		return sceneMan_0.GetTGL(string_0);
	}

	internal static void smethod_31(Toggle toggle_0, bool bool_0)
	{
		toggle_0.isOn = bool_0;
	}

	internal static void smethod_32()
	{
		JKGKJLLFMLE.ILHGLFHNIDL();
	}

	internal static GameObject smethod_33(string string_0)
	{
		return GameObject.Find(string_0);
	}

	internal static bool smethod_34(UnityEngine.Object object_0, UnityEngine.Object object_1)
	{
		return object_0 != object_1;
	}

	internal static int smethod_35()
	{
		return Screen.width;
	}

	internal static int smethod_36()
	{
		return Screen.height;
	}

	internal static int smethod_37(Texture texture_0)
	{
		return texture_0.width;
	}

	internal static int smethod_38(Texture texture_0)
	{
		return texture_0.height;
	}
}
