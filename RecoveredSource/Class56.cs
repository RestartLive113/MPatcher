using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using HarmonyLib;
using LitJson;
using MPatchrMain;
using McnCraft;
using UnityEngine;

internal class Class56 : MonoBehaviour
{
	[HarmonyPatch(typeof(TextureLoader))]
	[HarmonyPatch(new Type[] { })]
	[HarmonyPatch("HIOLLCEJNOC")]
	internal class hV1kc4UyLxNGd1I_0024rfx_88TR6luLZfynSMe65w_SXQWJ4AHYsMXMrLdqWlDuhjVDWmWSih4fr11raL9U1Kb3Kgg
	{
		[HarmonyPostfix]
		internal static void FeUAVwFbW6wGJJdNimZY9yI(TextureLoader __instance)
		{
			smethod_0((MonoBehaviour)MPatchr.xcBvxcM_0024ckBeZyvdSoAkJoM, Class56.smethod_0(__instance));
		}

		internal static Coroutine smethod_0(MonoBehaviour monoBehaviour_0, IEnumerator ienumerator_0)
		{
			return monoBehaviour_0.StartCoroutine(ienumerator_0);
		}
	}

	[HarmonyPatch(typeof(MachineController))]
	[HarmonyPatch("Awake")]
	internal class VyvC14dv321xosTSmlEtK6RZ_0024PTiI3GAqIWsbj6f9SOL4s24n6a3rXZHyPq8ySUMojDrPQQFucqhqlmnLI5NmOP9plJQ2i1_CoMmWg50OmhX7_0024iPAsah4ggOMgwL6V_iPw
	{
		[HarmonyPostfix]
		internal static void FeUAVwFbW6wGJJdNimZY9yI(MachineController __instance)
		{
			if (MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.stampNormalMap)
			{
				smethod_0((Component)__instance).AddComponent<Class56>();
			}
		}

		internal static GameObject smethod_0(Component component_0)
		{
			return component_0.gameObject;
		}
	}

	[CompilerGenerated]
	private sealed class XxAJ0vo10qaLovS4w9cCmkqj4kGa4GNTvCHjie_0024_CuCW28Gg_zImMKRjzk4rP9PBXmXDTo7udFa1G3RhmnAuXeU : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int SjlBM8inVA_YE4YVlr_0024gluY;

		private object yT7HpVIzmqW54W307WgJtr4;

		public TextureLoader wQ6mrkDog7tAEXGul0Y8Sv0;

		private string rq6mGt_fR8cqAX6sxxATGIM;

		private int gR8ZDu0MSJBpNpUVIsNsGFg;

		private WWW Ciu_0024YtWQUtSHTEMGz_0024d7hKU;

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
		public XxAJ0vo10qaLovS4w9cCmkqj4kGa4GNTvCHjie_0024_CuCW28Gg_zImMKRjzk4rP9PBXmXDTo7udFa1G3RhmnAuXeU(int _003C_003E1__state)
		{
			SjlBM8inVA_YE4YVlr_0024gluY = _003C_003E1__state;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			rq6mGt_fR8cqAX6sxxATGIM = null;
			Ciu_0024YtWQUtSHTEMGz_0024d7hKU = null;
			SjlBM8inVA_YE4YVlr_0024gluY = -2;
		}

		private bool MoveNext()
		{
			switch (SjlBM8inVA_YE4YVlr_0024gluY)
			{
			default:
				return false;
			case 0:
				SjlBM8inVA_YE4YVlr_0024gluY = -1;
				rq6mGt_fR8cqAX6sxxATGIM = nImd3eE4fzkfSnSPsEWM5L88g7B6hCV1Os3TO5zmspa2();
				if (!smethod_0(rq6mGt_fR8cqAX6sxxATGIM))
				{
					mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_25<string>(1708459883u));
					break;
				}
				gR8ZDu0MSJBpNpUVIsNsGFg = (rq6mGt_fR8cqAX6sxxATGIM + smethod_1(rq6mGt_fR8cqAX6sxxATGIM).ToString()).GetHashCode();
				if (MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4 == null || gR8ZDu0MSJBpNpUVIsNsGFg != MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.nmapHash)
				{
					yT7HpVIzmqW54W307WgJtr4 = null;
					SjlBM8inVA_YE4YVlr_0024gluY = 1;
					return true;
				}
				goto IL_0390;
			case 1:
			{
				SjlBM8inVA_YE4YVlr_0024gluY = -1;
				byte[] array = Class17.DQGZpGELKqeKmXweFYGEcaXR73CqGywHQI6u2TATGL80<TextureLoader, byte[]>(global::_003CModule_003E.smethod_25<string>(2269371872u), wQ6mrkDog7tAEXGul0Y8Sv0, new object[1] { rq6mGt_fR8cqAX6sxxATGIM });
				if (MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.moreStampSize)
				{
					float num = (float)new FileInfo(rq6mGt_fR8cqAX6sxxATGIM).Length / 1024f / 1024f;
					if (num > psdRdI7hc081t9dJJ5z_0024OdB8xbQGLZVsl590oxhjP7f4VlD_n3_0024h4tuXN_0024gEeO_qnA.cNpCOVrnTHBotobXpn4SoqVUaM6jzYImF684aCRdTXWu)
					{
						DP.D(global::_003CModule_003E.smethod_27<string>(120933134u) + num + global::_003CModule_003E.smethod_29<string>(3748975161u) + psdRdI7hc081t9dJJ5z_0024OdB8xbQGLZVsl590oxhjP7f4VlD_n3_0024h4tuXN_0024gEeO_qnA.cNpCOVrnTHBotobXpn4SoqVUaM6jzYImF684aCRdTXWu + global::_003CModule_003E.smethod_29<string>(2132683108u));
						return false;
					}
				}
				Texture2D texture2D = new Texture2D(0, 0, TextureFormat.ARGB32, mipmap: true);
				if (texture2D.LoadImage(array))
				{
					int width = texture2D.width;
					int height = texture2D.height;
					if (!MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.moreStampSize)
					{
						if (width > tZ6_KB17w5s4zB9mZxKkznQ && !JKGKJLLFMLE.KIEMANLPECC)
						{
							DP.D(global::_003CModule_003E.smethod_29<string>(1302163014u) + width + global::_003CModule_003E.smethod_29<string>(3748975161u) + tZ6_KB17w5s4zB9mZxKkznQ + global::_003CModule_003E.smethod_28<string>(2850539962u));
							return false;
						}
						if (width > tZ6_KB17w5s4zB9mZxKkznQ && !JKGKJLLFMLE.KIEMANLPECC)
						{
							DP.D(global::_003CModule_003E.smethod_28<string>(3757730211u) + height + global::_003CModule_003E.smethod_28<string>(2184595241u) + tZ6_KB17w5s4zB9mZxKkznQ + global::_003CModule_003E.smethod_28<string>(2850539962u));
							return false;
						}
					}
					if (array != null)
					{
						WWWForm wWWForm = new WWWForm();
						wWWForm.AddField(global::_003CModule_003E.smethod_29<string>(64791299u), Convert.ToBase64String(array));
						wWWForm.AddField(global::_003CModule_003E.smethod_26<string>(2545282780u), global::_003CModule_003E.smethod_27<string>(2145102039u));
						Ciu_0024YtWQUtSHTEMGz_0024d7hKU = new WWW(global::_003CModule_003E.smethod_28<string>(737601529u), wWWForm.data, new Dictionary<string, string> { 
						{
							global::_003CModule_003E.smethod_25<string>(3117863845u),
							global::_003CModule_003E.smethod_25<string>(2812781384u)
						} });
						yT7HpVIzmqW54W307WgJtr4 = Ciu_0024YtWQUtSHTEMGz_0024d7hKU;
						SjlBM8inVA_YE4YVlr_0024gluY = 2;
						return true;
					}
					goto IL_0390;
				}
				DP.D(global::_003CModule_003E.smethod_28<string>(1344568731u));
				return false;
			}
			case 2:
				{
					SjlBM8inVA_YE4YVlr_0024gluY = -1;
					JsonData jsonData = JsonMapper.ToObject(Ciu_0024YtWQUtSHTEMGz_0024d7hKU.text);
					if ((bool)jsonData[global::_003CModule_003E.smethod_28<string>(1648200539u)])
					{
						if (MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4 == null)
						{
							MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4 = new mcpd();
						}
						MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.nmapURL = jsonData[global::_003CModule_003E.smethod_27<string>(4215801619u)][global::_003CModule_003E.smethod_26<string>(2824386684u)].ToString();
						MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.nmapHash = gR8ZDu0MSJBpNpUVIsNsGFg;
						MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.UUiRNMwxRbfk_Fs4cDErRoM();
					}
					Ciu_0024YtWQUtSHTEMGz_0024d7hKU = null;
					goto IL_0390;
				}
				IL_0390:
				if (HNJDDKJLHMM.FHLGOMHPDLN == HNJDDKJLHMM.HKGAACMIPIH.Offline)
				{
					break;
				}
				if (HNJDDKJLHMM.FHLGOMHPDLN != HNJDDKJLHMM.HKGAACMIPIH.Legacy)
				{
					if (HNJDDKJLHMM.FHLGOMHPDLN == HNJDDKJLHMM.HKGAACMIPIH.Photon)
					{
						wQ6mrkDog7tAEXGul0Y8Sv0.GetComponent<PhotonView>().RPC(global::_003CModule_003E.smethod_25<string>(3645398129u), BFDCHLBGJHF.OthersBuffered, MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.nmapURL, MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.smooth, MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.metal);
					}
				}
				else
				{
					wQ6mrkDog7tAEXGul0Y8Sv0.GetComponent<NetworkView>().RPC(global::_003CModule_003E.smethod_29<string>(3909250042u), RPCMode.OthersBuffered, MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.nmapURL, MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.smooth, MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.metal);
				}
				break;
			}
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
			throw smethod_2();
		}

		internal static bool smethod_0(string string_0)
		{
			return File.Exists(string_0);
		}

		internal static DateTime smethod_1(string string_0)
		{
			return File.GetLastWriteTime(string_0);
		}

		internal static NotSupportedException smethod_2()
		{
			return new NotSupportedException();
		}
	}

	[CompilerGenerated]
	private sealed class _0024sJ3urUFa23oy83oXCnfFmAcfbTpt3GgP5uVnmsCHGD2a_vH2ALfj_KHFQXCy1KetWbk8_0024XmaNu3LLBho2lQLnQ : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int SjlBM8inVA_YE4YVlr_0024gluY;

		private object yT7HpVIzmqW54W307WgJtr4;

		public float mVOc3cKGD7MgBGiVuOAA1B0;

		public Class56 SKCFxHGAEbVQbKCDB_0024Jj8p4;

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
		public _0024sJ3urUFa23oy83oXCnfFmAcfbTpt3GgP5uVnmsCHGD2a_vH2ALfj_KHFQXCy1KetWbk8_0024XmaNu3LLBho2lQLnQ(int _003C_003E1__state)
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
			int num = SjlBM8inVA_YE4YVlr_0024gluY;
			Class56 @class = SKCFxHGAEbVQbKCDB_0024Jj8p4;
			switch (num)
			{
			default:
				return false;
			case 1:
			{
				SjlBM8inVA_YE4YVlr_0024gluY = -1;
				if (smethod_2(smethod_1((Component)@class), global::_003CModule_003E.smethod_29<string>(2690202915u)))
				{
					string string_ = nImd3eE4fzkfSnSPsEWM5L88g7B6hCV1Os3TO5zmspa2();
					if (!smethod_3(string_))
					{
						return false;
					}
					Texture2D texture2D_ = smethod_4(1, 1);
					smethod_6(texture2D_, smethod_5(string_));
					@class.GetComponent<Class56>().texture2D_0 = texture2D_;
					@class.GetComponent<Class56>().float_0 = (float)MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.smooth / 100f;
					@class.GetComponent<Class56>().Z17lHAwIZq_0024CHjlLc4rbKik = (float)MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.metal / 100f;
					@class.GetComponent<Class56>().a1LjQ2IIOmFaJUM544IaryU = (float)MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.xOffset / 100f;
					@class.GetComponent<Class56>().RCHkKphZpq9eNrQBt_H6wwU = (float)MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.yOffset / 100f;
				}
				Transform transform = smethod_8(smethod_7((Component)@class));
				if (smethod_2(smethod_1((Component)@class), global::_003CModule_003E.smethod_28<string>(3788034109u)))
				{
					transform = smethod_7((Component)@class);
				}
				MeshRenderer[] componentsInChildren = transform.GetComponentsInChildren<MeshRenderer>();
				foreach (MeshRenderer meshRenderer in componentsInChildren)
				{
					if (smethod_2(smethod_11((UnityEngine.Object)smethod_10(smethod_9((Renderer)meshRenderer))), global::_003CModule_003E.smethod_25<string>(306993535u)) || smethod_2(smethod_11((UnityEngine.Object)smethod_10(smethod_9((Renderer)meshRenderer))), global::_003CModule_003E.smethod_29<string>(793823621u)))
					{
						Class55.MB4V5ahgKNonxUqDz1C8X92J2fRrbY6gHbOaoQnjXfJw(meshRenderer, 1f, 1f, 1f, triplanar: true);
					}
				}
				return false;
			}
			case 0:
				SjlBM8inVA_YE4YVlr_0024gluY = -1;
				yT7HpVIzmqW54W307WgJtr4 = smethod_0(mVOc3cKGD7MgBGiVuOAA1B0);
				SjlBM8inVA_YE4YVlr_0024gluY = 1;
				return true;
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
			throw smethod_12();
		}

		internal static WaitForSecondsRealtime smethod_0(float float_0)
		{
			return new WaitForSecondsRealtime(float_0);
		}

		internal static string smethod_1(Component component_0)
		{
			return component_0.tag;
		}

		internal static bool smethod_2(string string_0, string string_1)
		{
			return string_0 == string_1;
		}

		internal static bool smethod_3(string string_0)
		{
			return File.Exists(string_0);
		}

		internal static Texture2D smethod_4(int int_0, int int_1)
		{
			return new Texture2D(int_0, int_1);
		}

		internal static byte[] smethod_5(string string_0)
		{
			return File.ReadAllBytes(string_0);
		}

		internal static bool smethod_6(Texture2D texture2D_0, byte[] byte_0)
		{
			return texture2D_0.LoadImage(byte_0);
		}

		internal static Transform smethod_7(Component component_0)
		{
			return component_0.transform;
		}

		internal static Transform smethod_8(Transform transform_0)
		{
			return transform_0.parent;
		}

		internal static Material smethod_9(Renderer renderer_0)
		{
			return renderer_0.material;
		}

		internal static Shader smethod_10(Material material_0)
		{
			return material_0.shader;
		}

		internal static string smethod_11(UnityEngine.Object object_0)
		{
			return object_0.name;
		}

		internal static NotSupportedException smethod_12()
		{
			return new NotSupportedException();
		}
	}

	[CompilerGenerated]
	private sealed class q11OkRCNnJSc7B9oAm2iiDyqnwFGtf2UVVpiMePyq6WT2dWokzREmRATkdBQmH_0024c9qVPLmn5AJdPYA85Xdd3P_0024tGwEqB_UhOiMk7wwkdRUNj : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int SjlBM8inVA_YE4YVlr_0024gluY;

		private object yT7HpVIzmqW54W307WgJtr4;

		public Class56 SKCFxHGAEbVQbKCDB_0024Jj8p4;

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
		public q11OkRCNnJSc7B9oAm2iiDyqnwFGtf2UVVpiMePyq6WT2dWokzREmRATkdBQmH_0024c9qVPLmn5AJdPYA85Xdd3P_0024tGwEqB_UhOiMk7wwkdRUNj(int _003C_003E1__state)
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
			int num = SjlBM8inVA_YE4YVlr_0024gluY;
			Class56 @class = SKCFxHGAEbVQbKCDB_0024Jj8p4;
			switch (num)
			{
			case 0:
				SjlBM8inVA_YE4YVlr_0024gluY = -1;
				yT7HpVIzmqW54W307WgJtr4 = smethod_0(5f);
				SjlBM8inVA_YE4YVlr_0024gluY = 1;
				return true;
			default:
				return false;
			case 1:
				SjlBM8inVA_YE4YVlr_0024gluY = -1;
				if (!@class.bool_0)
				{
					@class.bool_0 = true;
					List<MeshRenderer> list = new List<MeshRenderer>();
					MeshRenderer[] componentsInChildren = smethod_1((Component)@class).GetComponentsInChildren<MeshRenderer>();
					foreach (MeshRenderer meshRenderer in componentsInChildren)
					{
						if (smethod_5(smethod_4((UnityEngine.Object)smethod_3(smethod_2((Renderer)meshRenderer))), global::_003CModule_003E.smethod_26<string>(3311808258u)))
						{
							list.Add(meshRenderer);
						}
					}
					foreach (MeshRenderer item in list)
					{
						Class55.MB4V5ahgKNonxUqDz1C8X92J2fRrbY6gHbOaoQnjXfJw(item, 1.001f, 1.1f, 1.001f);
					}
					return false;
				}
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
			throw smethod_6();
		}

		internal static WaitForSecondsRealtime smethod_0(float float_0)
		{
			return new WaitForSecondsRealtime(float_0);
		}

		internal static Transform smethod_1(Component component_0)
		{
			return component_0.transform;
		}

		internal static Material smethod_2(Renderer renderer_0)
		{
			return renderer_0.material;
		}

		internal static Shader smethod_3(Material material_0)
		{
			return material_0.shader;
		}

		internal static string smethod_4(UnityEngine.Object object_0)
		{
			return object_0.name;
		}

		internal static bool smethod_5(string string_0, string string_1)
		{
			return string_0 == string_1;
		}

		internal static NotSupportedException smethod_6()
		{
			return new NotSupportedException();
		}
	}

	private static readonly int tZ6_KB17w5s4zB9mZxKkznQ = 1024;

	internal string P7o06Th40d7eSKrQ9TsbFqg = "";

	internal float Z17lHAwIZq_0024CHjlLc4rbKik;

	internal float float_0;

	internal float a1LjQ2IIOmFaJUM544IaryU;

	internal float RCHkKphZpq9eNrQBt_H6wwU;

	internal Texture2D texture2D_0;

	internal bool bool_0;

	internal static IEnumerator smethod_0(TextureLoader __instance)
	{
		string text = nImd3eE4fzkfSnSPsEWM5L88g7B6hCV1Os3TO5zmspa2();
		if (!XxAJ0vo10qaLovS4w9cCmkqj4kGa4GNTvCHjie_0024_CuCW28Gg_zImMKRjzk4rP9PBXmXDTo7udFa1G3RhmnAuXeU.smethod_0(text))
		{
			mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_25<string>(1708459883u));
			yield break;
		}
		int hashCode = (text + XxAJ0vo10qaLovS4w9cCmkqj4kGa4GNTvCHjie_0024_CuCW28Gg_zImMKRjzk4rP9PBXmXDTo7udFa1G3RhmnAuXeU.smethod_1(text).ToString()).GetHashCode();
		if (MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4 == null || hashCode != MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.nmapHash)
		{
			yield return null;
			byte[] array = Class17.DQGZpGELKqeKmXweFYGEcaXR73CqGywHQI6u2TATGL80<TextureLoader, byte[]>(global::_003CModule_003E.smethod_25<string>(2269371872u), __instance, new object[1] { text });
			if (MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.moreStampSize)
			{
				float num = (float)new FileInfo(text).Length / 1024f / 1024f;
				if (num > psdRdI7hc081t9dJJ5z_0024OdB8xbQGLZVsl590oxhjP7f4VlD_n3_0024h4tuXN_0024gEeO_qnA.cNpCOVrnTHBotobXpn4SoqVUaM6jzYImF684aCRdTXWu)
				{
					DP.D(global::_003CModule_003E.smethod_27<string>(120933134u) + num + global::_003CModule_003E.smethod_29<string>(3748975161u) + psdRdI7hc081t9dJJ5z_0024OdB8xbQGLZVsl590oxhjP7f4VlD_n3_0024h4tuXN_0024gEeO_qnA.cNpCOVrnTHBotobXpn4SoqVUaM6jzYImF684aCRdTXWu + global::_003CModule_003E.smethod_29<string>(2132683108u));
					yield break;
				}
			}
			Texture2D texture2D = new Texture2D(0, 0, TextureFormat.ARGB32, mipmap: true);
			if (!texture2D.LoadImage(array))
			{
				DP.D(global::_003CModule_003E.smethod_28<string>(1344568731u));
				yield break;
			}
			int width = texture2D.width;
			int height = texture2D.height;
			if (!MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.moreStampSize)
			{
				if (width > tZ6_KB17w5s4zB9mZxKkznQ && !JKGKJLLFMLE.KIEMANLPECC)
				{
					DP.D(global::_003CModule_003E.smethod_29<string>(1302163014u) + width + global::_003CModule_003E.smethod_29<string>(3748975161u) + tZ6_KB17w5s4zB9mZxKkznQ + global::_003CModule_003E.smethod_28<string>(2850539962u));
					yield break;
				}
				if (width > tZ6_KB17w5s4zB9mZxKkznQ && !JKGKJLLFMLE.KIEMANLPECC)
				{
					DP.D(global::_003CModule_003E.smethod_28<string>(3757730211u) + height + global::_003CModule_003E.smethod_28<string>(2184595241u) + tZ6_KB17w5s4zB9mZxKkznQ + global::_003CModule_003E.smethod_28<string>(2850539962u));
					yield break;
				}
			}
			if (array != null)
			{
				WWWForm wWWForm = new WWWForm();
				wWWForm.AddField(global::_003CModule_003E.smethod_29<string>(64791299u), Convert.ToBase64String(array));
				wWWForm.AddField(global::_003CModule_003E.smethod_26<string>(2545282780u), global::_003CModule_003E.smethod_27<string>(2145102039u));
				WWW wWW = new WWW(global::_003CModule_003E.smethod_28<string>(737601529u), wWWForm.data, new Dictionary<string, string> { 
				{
					global::_003CModule_003E.smethod_25<string>(3117863845u),
					global::_003CModule_003E.smethod_25<string>(2812781384u)
				} });
				yield return wWW;
				JsonData jsonData = JsonMapper.ToObject(wWW.text);
				if ((bool)jsonData[global::_003CModule_003E.smethod_28<string>(1648200539u)])
				{
					if (MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4 == null)
					{
						MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4 = new mcpd();
					}
					MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.nmapURL = jsonData[global::_003CModule_003E.smethod_27<string>(4215801619u)][global::_003CModule_003E.smethod_26<string>(2824386684u)].ToString();
					MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.nmapHash = hashCode;
					MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.UUiRNMwxRbfk_Fs4cDErRoM();
				}
			}
		}
		if (HNJDDKJLHMM.FHLGOMHPDLN == HNJDDKJLHMM.HKGAACMIPIH.Offline)
		{
			yield break;
		}
		if (HNJDDKJLHMM.FHLGOMHPDLN != HNJDDKJLHMM.HKGAACMIPIH.Legacy)
		{
			if (HNJDDKJLHMM.FHLGOMHPDLN == HNJDDKJLHMM.HKGAACMIPIH.Photon)
			{
				__instance.GetComponent<PhotonView>().RPC(global::_003CModule_003E.smethod_25<string>(3645398129u), BFDCHLBGJHF.OthersBuffered, MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.nmapURL, MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.smooth, MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.metal);
			}
		}
		else
		{
			__instance.GetComponent<NetworkView>().RPC(global::_003CModule_003E.smethod_29<string>(3909250042u), RPCMode.OthersBuffered, MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.nmapURL, MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.smooth, MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.metal);
		}
	}

	internal static Texture2D _zfkCFRb0XH3m9ne9dWrGsc(string filePath)
	{
		Texture2D texture2D_ = smethod_1(1, 1);
		smethod_3(texture2D_, smethod_2(filePath));
		return H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.b6Ztrosp6JyR7j55isJA2gcLLe_NYUiYgy8g5Yxncty2(texture2D_);
	}

	internal static string nImd3eE4fzkfSnSPsEWM5L88g7B6hCV1Os3TO5zmspa2()
	{
		int num = smethod_4(JKGKJLLFMLE.HHGILAIOCLG.texName, '#');
		return smethod_7(new string[6]
		{
			JKGKJLLFMLE.LAOHLAOMCPN,
			global::_003CModule_003E.smethod_25<string>(4198372504u),
			smethod_5(JKGKJLLFMLE.HHGILAIOCLG.texName, 0, num),
			global::_003CModule_003E.smethod_25<string>(1700522269u),
			smethod_5(JKGKJLLFMLE.HHGILAIOCLG.texName, num, smethod_6(JKGKJLLFMLE.HHGILAIOCLG.texName) - num),
			global::_003CModule_003E.smethod_25<string>(3497639330u)
		});
	}

	private IEnumerator method_0(float time)
	{
		yield return _0024sJ3urUFa23oy83oXCnfFmAcfbTpt3GgP5uVnmsCHGD2a_vH2ALfj_KHFQXCy1KetWbk8_0024XmaNu3LLBho2lQLnQ.smethod_0(time);
		if (_0024sJ3urUFa23oy83oXCnfFmAcfbTpt3GgP5uVnmsCHGD2a_vH2ALfj_KHFQXCy1KetWbk8_0024XmaNu3LLBho2lQLnQ.smethod_2(_0024sJ3urUFa23oy83oXCnfFmAcfbTpt3GgP5uVnmsCHGD2a_vH2ALfj_KHFQXCy1KetWbk8_0024XmaNu3LLBho2lQLnQ.smethod_1((Component)this), global::_003CModule_003E.smethod_29<string>(2690202915u)))
		{
			string string_ = nImd3eE4fzkfSnSPsEWM5L88g7B6hCV1Os3TO5zmspa2();
			if (!_0024sJ3urUFa23oy83oXCnfFmAcfbTpt3GgP5uVnmsCHGD2a_vH2ALfj_KHFQXCy1KetWbk8_0024XmaNu3LLBho2lQLnQ.smethod_3(string_))
			{
				yield break;
			}
			Texture2D texture2D = _0024sJ3urUFa23oy83oXCnfFmAcfbTpt3GgP5uVnmsCHGD2a_vH2ALfj_KHFQXCy1KetWbk8_0024XmaNu3LLBho2lQLnQ.smethod_4(1, 1);
			_0024sJ3urUFa23oy83oXCnfFmAcfbTpt3GgP5uVnmsCHGD2a_vH2ALfj_KHFQXCy1KetWbk8_0024XmaNu3LLBho2lQLnQ.smethod_6(texture2D, _0024sJ3urUFa23oy83oXCnfFmAcfbTpt3GgP5uVnmsCHGD2a_vH2ALfj_KHFQXCy1KetWbk8_0024XmaNu3LLBho2lQLnQ.smethod_5(string_));
			GetComponent<Class56>().texture2D_0 = texture2D;
			GetComponent<Class56>().float_0 = (float)MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.smooth / 100f;
			GetComponent<Class56>().Z17lHAwIZq_0024CHjlLc4rbKik = (float)MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.metal / 100f;
			GetComponent<Class56>().a1LjQ2IIOmFaJUM544IaryU = (float)MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.xOffset / 100f;
			GetComponent<Class56>().RCHkKphZpq9eNrQBt_H6wwU = (float)MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.yOffset / 100f;
		}
		Transform transform = _0024sJ3urUFa23oy83oXCnfFmAcfbTpt3GgP5uVnmsCHGD2a_vH2ALfj_KHFQXCy1KetWbk8_0024XmaNu3LLBho2lQLnQ.smethod_8(_0024sJ3urUFa23oy83oXCnfFmAcfbTpt3GgP5uVnmsCHGD2a_vH2ALfj_KHFQXCy1KetWbk8_0024XmaNu3LLBho2lQLnQ.smethod_7((Component)this));
		if (_0024sJ3urUFa23oy83oXCnfFmAcfbTpt3GgP5uVnmsCHGD2a_vH2ALfj_KHFQXCy1KetWbk8_0024XmaNu3LLBho2lQLnQ.smethod_2(_0024sJ3urUFa23oy83oXCnfFmAcfbTpt3GgP5uVnmsCHGD2a_vH2ALfj_KHFQXCy1KetWbk8_0024XmaNu3LLBho2lQLnQ.smethod_1((Component)this), global::_003CModule_003E.smethod_28<string>(3788034109u)))
		{
			transform = _0024sJ3urUFa23oy83oXCnfFmAcfbTpt3GgP5uVnmsCHGD2a_vH2ALfj_KHFQXCy1KetWbk8_0024XmaNu3LLBho2lQLnQ.smethod_7((Component)this);
		}
		MeshRenderer[] componentsInChildren = transform.GetComponentsInChildren<MeshRenderer>();
		foreach (MeshRenderer meshRenderer in componentsInChildren)
		{
			if (_0024sJ3urUFa23oy83oXCnfFmAcfbTpt3GgP5uVnmsCHGD2a_vH2ALfj_KHFQXCy1KetWbk8_0024XmaNu3LLBho2lQLnQ.smethod_2(_0024sJ3urUFa23oy83oXCnfFmAcfbTpt3GgP5uVnmsCHGD2a_vH2ALfj_KHFQXCy1KetWbk8_0024XmaNu3LLBho2lQLnQ.smethod_11((UnityEngine.Object)_0024sJ3urUFa23oy83oXCnfFmAcfbTpt3GgP5uVnmsCHGD2a_vH2ALfj_KHFQXCy1KetWbk8_0024XmaNu3LLBho2lQLnQ.smethod_10(_0024sJ3urUFa23oy83oXCnfFmAcfbTpt3GgP5uVnmsCHGD2a_vH2ALfj_KHFQXCy1KetWbk8_0024XmaNu3LLBho2lQLnQ.smethod_9((Renderer)meshRenderer))), global::_003CModule_003E.smethod_25<string>(306993535u)) || _0024sJ3urUFa23oy83oXCnfFmAcfbTpt3GgP5uVnmsCHGD2a_vH2ALfj_KHFQXCy1KetWbk8_0024XmaNu3LLBho2lQLnQ.smethod_2(_0024sJ3urUFa23oy83oXCnfFmAcfbTpt3GgP5uVnmsCHGD2a_vH2ALfj_KHFQXCy1KetWbk8_0024XmaNu3LLBho2lQLnQ.smethod_11((UnityEngine.Object)_0024sJ3urUFa23oy83oXCnfFmAcfbTpt3GgP5uVnmsCHGD2a_vH2ALfj_KHFQXCy1KetWbk8_0024XmaNu3LLBho2lQLnQ.smethod_10(_0024sJ3urUFa23oy83oXCnfFmAcfbTpt3GgP5uVnmsCHGD2a_vH2ALfj_KHFQXCy1KetWbk8_0024XmaNu3LLBho2lQLnQ.smethod_9((Renderer)meshRenderer))), global::_003CModule_003E.smethod_29<string>(793823621u)))
			{
				Class55.MB4V5ahgKNonxUqDz1C8X92J2fRrbY6gHbOaoQnjXfJw(meshRenderer, 1f, 1f, 1f, triplanar: true);
			}
		}
	}

	public void Start()
	{
		if (smethod_9(smethod_8((Component)this), global::_003CModule_003E.smethod_26<string>(1443219645u)))
		{
			smethod_10((MonoBehaviour)this, method_0(5f));
		}
	}

	internal IEnumerator om2UmgTTrbeFy2MuHdZBRthrRuDGpTpqcdrXBLqh6Utm()
	{
		yield return q11OkRCNnJSc7B9oAm2iiDyqnwFGtf2UVVpiMePyq6WT2dWokzREmRATkdBQmH_0024c9qVPLmn5AJdPYA85Xdd3P_0024tGwEqB_UhOiMk7wwkdRUNj.smethod_0(5f);
		if (bool_0)
		{
			yield break;
		}
		bool_0 = true;
		List<MeshRenderer> list = new List<MeshRenderer>();
		MeshRenderer[] componentsInChildren = q11OkRCNnJSc7B9oAm2iiDyqnwFGtf2UVVpiMePyq6WT2dWokzREmRATkdBQmH_0024c9qVPLmn5AJdPYA85Xdd3P_0024tGwEqB_UhOiMk7wwkdRUNj.smethod_1((Component)this).GetComponentsInChildren<MeshRenderer>();
		foreach (MeshRenderer meshRenderer in componentsInChildren)
		{
			if (q11OkRCNnJSc7B9oAm2iiDyqnwFGtf2UVVpiMePyq6WT2dWokzREmRATkdBQmH_0024c9qVPLmn5AJdPYA85Xdd3P_0024tGwEqB_UhOiMk7wwkdRUNj.smethod_5(q11OkRCNnJSc7B9oAm2iiDyqnwFGtf2UVVpiMePyq6WT2dWokzREmRATkdBQmH_0024c9qVPLmn5AJdPYA85Xdd3P_0024tGwEqB_UhOiMk7wwkdRUNj.smethod_4((UnityEngine.Object)q11OkRCNnJSc7B9oAm2iiDyqnwFGtf2UVVpiMePyq6WT2dWokzREmRATkdBQmH_0024c9qVPLmn5AJdPYA85Xdd3P_0024tGwEqB_UhOiMk7wwkdRUNj.smethod_3(q11OkRCNnJSc7B9oAm2iiDyqnwFGtf2UVVpiMePyq6WT2dWokzREmRATkdBQmH_0024c9qVPLmn5AJdPYA85Xdd3P_0024tGwEqB_UhOiMk7wwkdRUNj.smethod_2((Renderer)meshRenderer))), global::_003CModule_003E.smethod_26<string>(3311808258u)))
			{
				list.Add(meshRenderer);
			}
		}
		foreach (MeshRenderer item in list)
		{
			Class55.MB4V5ahgKNonxUqDz1C8X92J2fRrbY6gHbOaoQnjXfJw(item, 1.001f, 1.1f, 1.001f);
		}
	}

	internal static Texture2D smethod_1(int int_0, int int_1)
	{
		return new Texture2D(int_0, int_1);
	}

	internal static byte[] smethod_2(string string_0)
	{
		return File.ReadAllBytes(string_0);
	}

	internal static bool smethod_3(Texture2D texture2D_1, byte[] byte_0)
	{
		return texture2D_1.LoadImage(byte_0);
	}

	internal static int smethod_4(string string_0, char char_0)
	{
		return string_0.LastIndexOf(char_0);
	}

	internal static string smethod_5(string string_0, int int_0, int int_1)
	{
		return string_0.Substring(int_0, int_1);
	}

	internal static int smethod_6(string string_0)
	{
		return string_0.Length;
	}

	internal static string smethod_7(string[] string_0)
	{
		return string.Concat(string_0);
	}

	internal static string smethod_8(Component component_0)
	{
		return component_0.tag;
	}

	internal static bool smethod_9(string string_0, string string_1)
	{
		return string_0 == string_1;
	}

	internal static Coroutine smethod_10(MonoBehaviour monoBehaviour_0, IEnumerator ienumerator_0)
	{
		return monoBehaviour_0.StartCoroutine(ienumerator_0);
	}
}
