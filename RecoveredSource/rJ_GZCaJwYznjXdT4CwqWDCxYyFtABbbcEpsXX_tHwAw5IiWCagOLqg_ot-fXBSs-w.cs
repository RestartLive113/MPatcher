using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using HarmonyLib;
using MPatchrMain;
using UnityEngine;

internal class rJ_GZCaJwYznjXdT4CwqWDCxYyFtABbbcEpsXX_tHwAw5IiWCagOLqg_ot_0024fXBSs_0024w
{
	[HarmonyPatch("FLONKPAGMJK")]
	[HarmonyPatch(typeof(KEFHJCGICLE))]
	internal class er0HYGqNAojXGQnki2Q6p2xHA8QyMHMSXWEk_e87lm2Zjib7sDsApYu6olYKz6duakOmPurjVj1wHKvlTnr4I0tyUWJN94Gmktwnc6ecA5f3
	{
		[HarmonyPostfix]
		internal static void FeUAVwFbW6wGJJdNimZY9yI(string CBNCLLHJONG, string NFGDLEJPCEA, float NDHDALEEEOP, Dictionary<string, object> ___EPFADGGEEDC)
		{
			if (!MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.audioVariations || !___EPFADGGEEDC.ContainsKey(CBNCLLHJONG))
			{
				return;
			}
			if (_63xbQbNha8ln3skMjZCb8M.ContainsKey(CBNCLLHJONG))
			{
				_63xbQbNha8ln3skMjZCb8M[CBNCLLHJONG].Clear();
			}
			if (smethod_0(NFGDLEJPCEA, global::_003CModule_003E.smethod_25<string>(1946152840u)))
			{
				return;
			}
			string path = smethod_1(JKGKJLLFMLE.LAOHLAOMCPN, global::_003CModule_003E.smethod_26<string>(2758617653u));
			int num = 1;
			while (File.Exists(Path.Combine(path, NFGDLEJPCEA + global::_003CModule_003E.smethod_29<string>(2779159449u) + num + global::_003CModule_003E.smethod_29<string>(3947732558u))))
			{
				string text = NFGDLEJPCEA + global::_003CModule_003E.smethod_25<string>(3634801910u) + num + global::_003CModule_003E.smethod_29<string>(3947732558u);
				if (!_63xbQbNha8ln3skMjZCb8M.ContainsKey(CBNCLLHJONG))
				{
					_63xbQbNha8ln3skMjZCb8M.Add(CBNCLLHJONG, new List<string>());
				}
				if (!_63xbQbNha8ln3skMjZCb8M[CBNCLLHJONG].Contains(text))
				{
					MPatchr.xcBvxcM_0024ckBeZyvdSoAkJoM.StartCoroutine(qQ100vLrveIFhf6kyxv6esRlPW3fZgd7JdGdQ2aL4_WY(text, Path.Combine(path, text)));
					_63xbQbNha8ln3skMjZCb8M[CBNCLLHJONG].Add(text);
					num++;
				}
			}
		}

		internal static bool smethod_0(string string_0, string string_1)
		{
			return string_0 == string_1;
		}

		internal static string smethod_1(string string_0, string string_1)
		{
			return Path.Combine(string_0, string_1);
		}
	}

	[HarmonyPatch("BNPCKAALHCL")]
	[HarmonyPatch(typeof(KEFHJCGICLE))]
	internal class Class26
	{
		[HarmonyPostfix]
		internal static void FeUAVwFbW6wGJJdNimZY9yI(string CBNCLLHJONG, string NFGDLEJPCEA, float NDHDALEEEOP, Dictionary<string, object> ___NBNEOJOCDAH)
		{
			if (!MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.audioVariations || !___NBNEOJOCDAH.ContainsKey(CBNCLLHJONG))
			{
				return;
			}
			if (_63xbQbNha8ln3skMjZCb8M.ContainsKey(CBNCLLHJONG))
			{
				_63xbQbNha8ln3skMjZCb8M[CBNCLLHJONG].Clear();
			}
			if (smethod_0(NFGDLEJPCEA, global::_003CModule_003E.smethod_29<string>(2077237075u)))
			{
				return;
			}
			string path = smethod_1(JKGKJLLFMLE.LAOHLAOMCPN, global::_003CModule_003E.smethod_26<string>(2758617653u));
			int num = 1;
			while (File.Exists(Path.Combine(path, NFGDLEJPCEA + global::_003CModule_003E.smethod_28<string>(3883984841u) + num + global::_003CModule_003E.smethod_26<string>(4243734658u))))
			{
				string text = NFGDLEJPCEA + global::_003CModule_003E.smethod_25<string>(3634801910u) + num + global::_003CModule_003E.smethod_26<string>(4243734658u);
				if (!_63xbQbNha8ln3skMjZCb8M.ContainsKey(CBNCLLHJONG))
				{
					_63xbQbNha8ln3skMjZCb8M.Add(CBNCLLHJONG, new List<string>());
				}
				if (!_63xbQbNha8ln3skMjZCb8M[CBNCLLHJONG].Contains(text))
				{
					MPatchr.xcBvxcM_0024ckBeZyvdSoAkJoM.StartCoroutine(qQ100vLrveIFhf6kyxv6esRlPW3fZgd7JdGdQ2aL4_WY(text, Path.Combine(path, text)));
					_63xbQbNha8ln3skMjZCb8M[CBNCLLHJONG].Add(text);
					num++;
				}
			}
		}

		internal static bool smethod_0(string string_0, string string_1)
		{
			return string_0 == string_1;
		}

		internal static string smethod_1(string string_0, string string_1)
		{
			return Path.Combine(string_0, string_1);
		}
	}

	[HarmonyPatch("BKODCLJILAO")]
	[HarmonyPatch(typeof(KEFHJCGICLE))]
	internal class W7R47_jkRnWN3ptk8JRVHEGN8kdlXdVfmp5Gt_o5MqqwUV78QOWW9GNTO6BIVxQyZ4dS6g7yCnosZw9Gy6y5uHw
	{
		[HarmonyPrefix]
		internal static void smethod_0(bool EELHGJDCAHF, string CBNCLLHJONG, Dictionary<string, object> ___NBNEOJOCDAH, Dictionary<string, object> ___EPFADGGEEDC, ref AudioClip __state)
		{
			if (!MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.audioVariations)
			{
				return;
			}
			__state = null;
			if (!_63xbQbNha8ln3skMjZCb8M.ContainsKey(CBNCLLHJONG) || _63xbQbNha8ln3skMjZCb8M[CBNCLLHJONG].Count == 0)
			{
				return;
			}
			int num = smethod_1(0, _63xbQbNha8ln3skMjZCb8M[CBNCLLHJONG].Count + 1) - 1;
			if (num == -1)
			{
				return;
			}
			string key = _63xbQbNha8ln3skMjZCb8M[CBNCLLHJONG][num];
			if (KwSGNekH_GrR_00245D8lX7qnf8.ContainsKey(key))
			{
				if (EELHGJDCAHF)
				{
					__state = Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<AudioClip>(global::_003CModule_003E.smethod_25<string>(2920218019u), ___NBNEOJOCDAH[CBNCLLHJONG]);
					Class17.DMoImhPC2jNUt2tgmSezeT_IK_0024XK5Df9PmexG_CaKb1R(global::_003CModule_003E.smethod_26<string>(2919001372u), ___NBNEOJOCDAH[CBNCLLHJONG], KwSGNekH_GrR_00245D8lX7qnf8[key]);
				}
				else
				{
					__state = Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<AudioClip>(global::_003CModule_003E.smethod_26<string>(2919001372u), ___EPFADGGEEDC[CBNCLLHJONG]);
					Class17.DMoImhPC2jNUt2tgmSezeT_IK_0024XK5Df9PmexG_CaKb1R(global::_003CModule_003E.smethod_25<string>(2920218019u), ___EPFADGGEEDC[CBNCLLHJONG], KwSGNekH_GrR_00245D8lX7qnf8[key]);
				}
			}
		}

		[HarmonyPostfix]
		internal static void FeUAVwFbW6wGJJdNimZY9yI(bool EELHGJDCAHF, string CBNCLLHJONG, Dictionary<string, object> ___NBNEOJOCDAH, Dictionary<string, object> ___EPFADGGEEDC, ref AudioClip __state)
		{
			if (MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.audioVariations && smethod_2((UnityEngine.Object)__state, (UnityEngine.Object)null))
			{
				if (EELHGJDCAHF)
				{
					Class17.DMoImhPC2jNUt2tgmSezeT_IK_0024XK5Df9PmexG_CaKb1R(global::_003CModule_003E.smethod_26<string>(2919001372u), ___NBNEOJOCDAH[CBNCLLHJONG], __state);
				}
				else
				{
					Class17.DMoImhPC2jNUt2tgmSezeT_IK_0024XK5Df9PmexG_CaKb1R(global::_003CModule_003E.smethod_26<string>(2919001372u), ___EPFADGGEEDC[CBNCLLHJONG], __state);
				}
			}
		}

		internal static int smethod_1(int int_0, int int_1)
		{
			return UnityEngine.Random.Range(int_0, int_1);
		}

		internal static bool smethod_2(UnityEngine.Object object_0, UnityEngine.Object object_1)
		{
			return object_0 != object_1;
		}
	}

	[CompilerGenerated]
	private sealed class Kv9fOab2ojY_gQvZCr5kGSWDme0M_FSDsulXhnCeH2pjCt_WJ0TqU97WfZ_AIqiqnW1FsZcabHv04rwd5gc3LANA7wbbmmbxaPSuAfSgT0od : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int SjlBM8inVA_YE4YVlr_0024gluY;

		private object yT7HpVIzmqW54W307WgJtr4;

		public string EoPN1__b0zkQ3NlcZfbz4qs;

		public string YB08lIstfP1LI2QBCk6BbIw;

		private WWW CGuar6f91zneC6jjs_GZO2I;

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
		public Kv9fOab2ojY_gQvZCr5kGSWDme0M_FSDsulXhnCeH2pjCt_WJ0TqU97WfZ_AIqiqnW1FsZcabHv04rwd5gc3LANA7wbbmmbxaPSuAfSgT0od(int _003C_003E1__state)
		{
			SjlBM8inVA_YE4YVlr_0024gluY = _003C_003E1__state;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			CGuar6f91zneC6jjs_GZO2I = null;
			SjlBM8inVA_YE4YVlr_0024gluY = -2;
		}

		private bool MoveNext()
		{
			switch (SjlBM8inVA_YE4YVlr_0024gluY)
			{
			case 1:
			{
				SjlBM8inVA_YE4YVlr_0024gluY = -1;
				AudioClip value = smethod_2(CGuar6f91zneC6jjs_GZO2I);
				if (!smethod_4(smethod_3(CGuar6f91zneC6jjs_GZO2I)))
				{
					smethod_6((object)smethod_5(global::_003CModule_003E.smethod_25<string>(949902068u), EoPN1__b0zkQ3NlcZfbz4qs, global::_003CModule_003E.smethod_25<string>(1753625593u), smethod_3(CGuar6f91zneC6jjs_GZO2I)), 1);
					return false;
				}
				try
				{
					KwSGNekH_GrR_00245D8lX7qnf8.Add(EoPN1__b0zkQ3NlcZfbz4qs, value);
				}
				catch (ArgumentException)
				{
				}
				return false;
			}
			default:
				return false;
			case 0:
				SjlBM8inVA_YE4YVlr_0024gluY = -1;
				if (KwSGNekH_GrR_00245D8lX7qnf8.ContainsKey(EoPN1__b0zkQ3NlcZfbz4qs))
				{
					return false;
				}
				CGuar6f91zneC6jjs_GZO2I = smethod_1(smethod_0(global::_003CModule_003E.smethod_28<string>(4168611318u), YB08lIstfP1LI2QBCk6BbIw));
				yT7HpVIzmqW54W307WgJtr4 = CGuar6f91zneC6jjs_GZO2I;
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
			throw smethod_7();
		}

		internal static string smethod_0(string string_0, string string_1)
		{
			return string_0 + string_1;
		}

		internal static WWW smethod_1(string string_0)
		{
			return new WWW(string_0);
		}

		internal static AudioClip smethod_2(WWW www_0)
		{
			return www_0.GetAudioClip();
		}

		internal static string smethod_3(WWW www_0)
		{
			return www_0.error;
		}

		internal static bool smethod_4(string string_0)
		{
			return string.IsNullOrEmpty(string_0);
		}

		internal static string smethod_5(string string_0, string string_1, string string_2, string string_3)
		{
			return string_0 + string_1 + string_2 + string_3;
		}

		internal static void smethod_6(object object_0, int int_0)
		{
			DP.D(object_0, int_0);
		}

		internal static NotSupportedException smethod_7()
		{
			return new NotSupportedException();
		}
	}

	private static Dictionary<string, List<string>> _63xbQbNha8ln3skMjZCb8M = new Dictionary<string, List<string>>();

	private static Dictionary<string, AudioClip> KwSGNekH_GrR_00245D8lX7qnf8 = new Dictionary<string, AudioClip>();

	private static IEnumerator qQ100vLrveIFhf6kyxv6esRlPW3fZgd7JdGdQ2aL4_WY(string sname, string spath)
	{
		if (KwSGNekH_GrR_00245D8lX7qnf8.ContainsKey(sname))
		{
			yield break;
		}
		WWW wWW = Kv9fOab2ojY_gQvZCr5kGSWDme0M_FSDsulXhnCeH2pjCt_WJ0TqU97WfZ_AIqiqnW1FsZcabHv04rwd5gc3LANA7wbbmmbxaPSuAfSgT0od.smethod_1(Kv9fOab2ojY_gQvZCr5kGSWDme0M_FSDsulXhnCeH2pjCt_WJ0TqU97WfZ_AIqiqnW1FsZcabHv04rwd5gc3LANA7wbbmmbxaPSuAfSgT0od.smethod_0(global::_003CModule_003E.smethod_28<string>(4168611318u), spath));
		yield return wWW;
		AudioClip value = Kv9fOab2ojY_gQvZCr5kGSWDme0M_FSDsulXhnCeH2pjCt_WJ0TqU97WfZ_AIqiqnW1FsZcabHv04rwd5gc3LANA7wbbmmbxaPSuAfSgT0od.smethod_2(wWW);
		if (!Kv9fOab2ojY_gQvZCr5kGSWDme0M_FSDsulXhnCeH2pjCt_WJ0TqU97WfZ_AIqiqnW1FsZcabHv04rwd5gc3LANA7wbbmmbxaPSuAfSgT0od.smethod_4(Kv9fOab2ojY_gQvZCr5kGSWDme0M_FSDsulXhnCeH2pjCt_WJ0TqU97WfZ_AIqiqnW1FsZcabHv04rwd5gc3LANA7wbbmmbxaPSuAfSgT0od.smethod_3(wWW)))
		{
			Kv9fOab2ojY_gQvZCr5kGSWDme0M_FSDsulXhnCeH2pjCt_WJ0TqU97WfZ_AIqiqnW1FsZcabHv04rwd5gc3LANA7wbbmmbxaPSuAfSgT0od.smethod_6((object)Kv9fOab2ojY_gQvZCr5kGSWDme0M_FSDsulXhnCeH2pjCt_WJ0TqU97WfZ_AIqiqnW1FsZcabHv04rwd5gc3LANA7wbbmmbxaPSuAfSgT0od.smethod_5(global::_003CModule_003E.smethod_25<string>(949902068u), sname, global::_003CModule_003E.smethod_25<string>(1753625593u), Kv9fOab2ojY_gQvZCr5kGSWDme0M_FSDsulXhnCeH2pjCt_WJ0TqU97WfZ_AIqiqnW1FsZcabHv04rwd5gc3LANA7wbbmmbxaPSuAfSgT0od.smethod_3(wWW)), 1);
			yield break;
		}
		try
		{
			KwSGNekH_GrR_00245D8lX7qnf8.Add(sname, value);
		}
		catch (ArgumentException)
		{
		}
	}
}
