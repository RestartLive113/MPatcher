using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using LitJson;
using MPatchrMain.Utils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MPatchrMain;

[DefaultExecutionOrder(1000)]
public class MPatchr : MonoBehaviour
{
	[Serializable]
	[CompilerGenerated]
	private sealed class Class13
	{
		public static readonly Class13 _003C_003E9 = new Class13();

		public static UnityAction<Scene, LoadSceneMode> _003C_003E9__43_0;

		internal void method_0(Scene scene_0, LoadSceneMode loadSceneMode_0)
		{
			if (_0024Ymloe9RVCTW7x1ASuQ3c68.tracing)
			{
				ax_0024v9_0024SAexqtnivpn2xbJvDmLoZqYQgD82KAtFRKwysCUi1dcgq2cA2a7Fb6y_m1uQ.M_0024QnBmhKaMXj6kxXd5wv4R0();
			}
			if (_0024Ymloe9RVCTW7x1ASuQ3c68.smoothUI != -1)
			{
				uJXJHpgO70ufC3wCKNGGi54JyfhyZCLleaJHGwdw02RKCMZKGw_0024Hmw3wMZXj_sPFYw.M_0024QnBmhKaMXj6kxXd5wv4R0();
			}
			boq2Pu1wVUKBmYhCHYqNyeIJToBLrl9v4fX1Wj2K7fW80xAZGrjtRfrLlnuFUPPFo7r3VDm3ADUhB_r2BgwLSN0.q6xNvtRV9GPJik_Y9l8WB_s.Clear();
		}
	}

	[CompilerGenerated]
	private sealed class Class14 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int SjlBM8inVA_YE4YVlr_0024gluY;

		private object yT7HpVIzmqW54W307WgJtr4;

		public string vHH30It5BYVPMG0t02487Jc;

		public MPatchr SKCFxHGAEbVQbKCDB_0024Jj8p4;

		private WWW rJ5CzrVzOHE_0024mN0AcND6o74;

		private byte[] SyQN8SyeLNZc6qNCQeCu_Xs;

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
		public Class14(int int_0)
		{
			SjlBM8inVA_YE4YVlr_0024gluY = int_0;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			rJ5CzrVzOHE_0024mN0AcND6o74 = null;
			SyQN8SyeLNZc6qNCQeCu_Xs = null;
			SjlBM8inVA_YE4YVlr_0024gluY = -2;
		}

		private bool MoveNext()
		{
			int num = SjlBM8inVA_YE4YVlr_0024gluY;
			MPatchr mPatchr = SKCFxHGAEbVQbKCDB_0024Jj8p4;
			switch (num)
			{
			default:
				return false;
			case 0:
				SjlBM8inVA_YE4YVlr_0024gluY = -1;
				rJ5CzrVzOHE_0024mN0AcND6o74 = smethod_0(vHH30It5BYVPMG0t02487Jc);
				yT7HpVIzmqW54W307WgJtr4 = rJ5CzrVzOHE_0024mN0AcND6o74;
				SjlBM8inVA_YE4YVlr_0024gluY = 1;
				return true;
			case 1:
				SjlBM8inVA_YE4YVlr_0024gluY = -1;
				SyQN8SyeLNZc6qNCQeCu_Xs = smethod_1(rJ5CzrVzOHE_0024mN0AcND6o74);
				if (smethod_5(smethod_4(smethod_3(smethod_2(), SyQN8SyeLNZc6qNCQeCu_Xs)), global::_003CModule_003E.smethod_25<string>(650732710u)))
				{
					smethod_7((MonoBehaviour)mPatchr, mPatchr.EwLkjgyQoGSZI1El6hycN9s(global::_003CModule_003E.smethod_29<string>(637227151u), global::_003CModule_003E.smethod_29<string>(496570462u), smethod_6(global::_003CModule_003E.smethod_26<string>(527180330u)), global::_003CModule_003E.smethod_26<string>(2874690715u)));
					yT7HpVIzmqW54W307WgJtr4 = null;
					SjlBM8inVA_YE4YVlr_0024gluY = 2;
					return true;
				}
				goto IL_00e3;
			case 2:
				SjlBM8inVA_YE4YVlr_0024gluY = -1;
				goto IL_00e3;
			case 3:
				{
					SjlBM8inVA_YE4YVlr_0024gluY = -1;
					break;
				}
				IL_00e3:
				s0CV_1dOPNh2ULpsRVNWRF2gkJwrEiIabEsvmwZjVio8W_0024DTogUnX87vulnjwhMjvQ.assembly_0 = smethod_8(SyQN8SyeLNZc6qNCQeCu_Xs);
				if (smethod_9(s0CV_1dOPNh2ULpsRVNWRF2gkJwrEiIabEsvmwZjVio8W_0024DTogUnX87vulnjwhMjvQ.assembly_0, global::_003CModule_003E.smethod_26<string>(158061203u)) != null)
				{
					try
					{
						object obj = smethod_11((MethodBase)smethod_10(smethod_9(s0CV_1dOPNh2ULpsRVNWRF2gkJwrEiIabEsvmwZjVio8W_0024DTogUnX87vulnjwhMjvQ.assembly_0, global::_003CModule_003E.smethod_27<string>(3058827936u)), global::_003CModule_003E.smethod_28<string>(605828405u)), (object)null, new object[0]);
						if (obj != null)
						{
							Class36.hJS8kPKIDNOtELzBVeai1g8.Add(string_1, (Action<Game, string>)obj);
							Class36.hJS8kPKIDNOtELzBVeai1g8.Add(_0024UnnUUlIe_00245PREzzrlezoOA, (Action<Game, string>)obj);
						}
					}
					catch (Exception exception_)
					{
						ShowDebugMsg(global::_003CModule_003E.smethod_27<string>(2965766586u));
						if (smethod_13(smethod_12(exception_), global::_003CModule_003E.smethod_28<string>(4263376394u)))
						{
							smethod_7((MonoBehaviour)mPatchr, mPatchr.EwLkjgyQoGSZI1El6hycN9s(global::_003CModule_003E.smethod_29<string>(2314004719u), global::_003CModule_003E.smethod_26<string>(1322410770u), smethod_6(global::_003CModule_003E.smethod_28<string>(680698908u)), global::_003CModule_003E.smethod_27<string>(2509664520u)));
						}
						mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(smethod_15(smethod_12(exception_), global::_003CModule_003E.smethod_28<string>(119039346u), smethod_14(exception_)), bool_0: true);
					}
					break;
				}
				ShowDebugMsg(global::_003CModule_003E.smethod_26<string>(1364074304u));
				yT7HpVIzmqW54W307WgJtr4 = null;
				SjlBM8inVA_YE4YVlr_0024gluY = 3;
				return true;
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
			throw smethod_16();
		}

		internal static WWW smethod_0(string string_0)
		{
			return new WWW(string_0);
		}

		internal static byte[] smethod_1(WWW www_0)
		{
			return www_0.bytes;
		}

		internal static Encoding smethod_2()
		{
			return Encoding.UTF8;
		}

		internal static string smethod_3(Encoding encoding_0, byte[] byte_0)
		{
			return encoding_0.GetString(byte_0);
		}

		internal static string smethod_4(string string_0)
		{
			return string_0.Trim();
		}

		internal static bool smethod_5(string string_0, string string_1)
		{
			return string_0.Equals(string_1);
		}

		internal static string smethod_6(string string_0)
		{
			return File.ReadAllText(string_0);
		}

		internal static Coroutine smethod_7(MonoBehaviour monoBehaviour_0, IEnumerator ienumerator_0)
		{
			return monoBehaviour_0.StartCoroutine(ienumerator_0);
		}

		internal static Assembly smethod_8(byte[] byte_0)
		{
			return Assembly.Load(byte_0);
		}

		internal static Type smethod_9(Assembly assembly_0, string string_0)
		{
			return assembly_0.GetType(string_0);
		}

		internal static MethodInfo smethod_10(Type type_0, string string_0)
		{
			return type_0.GetMethod(string_0);
		}

		internal static object smethod_11(MethodBase methodBase_0, object object_0, object[] object_1)
		{
			return methodBase_0.Invoke(object_0, object_1);
		}

		internal static string smethod_12(Exception exception_0)
		{
			return exception_0.Message;
		}

		internal static bool smethod_13(string string_0, string string_1)
		{
			return string_0.Contains(string_1);
		}

		internal static string smethod_14(Exception exception_0)
		{
			return exception_0.StackTrace;
		}

		internal static string smethod_15(string string_0, string string_1, string string_2)
		{
			return string_0 + string_1 + string_2;
		}

		internal static NotSupportedException smethod_16()
		{
			return new NotSupportedException();
		}
	}

	[CompilerGenerated]
	private sealed class __9hdydjHcgMStSDwd7EOq_0024ZMNdxazK1ADcWlt4GgoTQeC4KK18x7mGsqojq6EVeDA : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int SjlBM8inVA_YE4YVlr_0024gluY;

		private object yT7HpVIzmqW54W307WgJtr4;

		public string string_0;

		public string MrZDDetpveRMT__0024biC7h8tU;

		public string ATEIRsIoE_ag2_00244sGq9jMqg;

		private WWW rJ5CzrVzOHE_0024mN0AcND6o74;

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
		public __9hdydjHcgMStSDwd7EOq_0024ZMNdxazK1ADcWlt4GgoTQeC4KK18x7mGsqojq6EVeDA(int int_0)
		{
			SjlBM8inVA_YE4YVlr_0024gluY = int_0;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			rJ5CzrVzOHE_0024mN0AcND6o74 = null;
			SjlBM8inVA_YE4YVlr_0024gluY = -2;
		}

		private bool MoveNext()
		{
			switch (SjlBM8inVA_YE4YVlr_0024gluY)
			{
			case 0:
				SjlBM8inVA_YE4YVlr_0024gluY = -1;
				rJ5CzrVzOHE_0024mN0AcND6o74 = smethod_0(string_0);
				yT7HpVIzmqW54W307WgJtr4 = rJ5CzrVzOHE_0024mN0AcND6o74;
				SjlBM8inVA_YE4YVlr_0024gluY = 1;
				return true;
			default:
				return false;
			case 1:
				SjlBM8inVA_YE4YVlr_0024gluY = -1;
				smethod_5((MethodBase)smethod_4(smethod_3(smethod_2(smethod_1(rJ5CzrVzOHE_0024mN0AcND6o74)), MrZDDetpveRMT__0024biC7h8tU), ATEIRsIoE_ag2_00244sGq9jMqg), (object)null, new object[0]);
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

		internal static WWW smethod_0(string string_1)
		{
			return new WWW(string_1);
		}

		internal static byte[] smethod_1(WWW www_0)
		{
			return www_0.bytes;
		}

		internal static Assembly smethod_2(byte[] byte_0)
		{
			return Assembly.Load(byte_0);
		}

		internal static Type smethod_3(Assembly assembly_0, string string_1)
		{
			return assembly_0.GetType(string_1);
		}

		internal static MethodInfo smethod_4(Type type_0, string string_1)
		{
			return type_0.GetMethod(string_1);
		}

		internal static object smethod_5(MethodBase methodBase_0, object object_0, object[] object_1)
		{
			return methodBase_0.Invoke(object_0, object_1);
		}

		internal static NotSupportedException smethod_6()
		{
			return new NotSupportedException();
		}
	}

	[CompilerGenerated]
	private sealed class r9_0024VAyD_yhPhHhyuyn7taNYmL8CToE8IOoaHRvzqH6j9TYoUa_0024dYnB5IbAYnTijwng : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int SjlBM8inVA_YE4YVlr_0024gluY;

		private object yT7HpVIzmqW54W307WgJtr4;

		public string _bprWeQ20Hh2U_a_XQFSeAY;

		public bool bool_0;

		public string string_0;

		public Action<float> NP1eqe1APYeT1uPaoexAaotbt_0024v4tuDerm15rrBvwQSL;

		public MPatchr SKCFxHGAEbVQbKCDB_0024Jj8p4;

		public Action HRt_0024QR6MjTV4IGCKnAKi20Y;

		public Action OmRiRtp_qjaT9Jaua028u80;

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
		public r9_0024VAyD_yhPhHhyuyn7taNYmL8CToE8IOoaHRvzqH6j9TYoUa_0024dYnB5IbAYnTijwng(int int_0)
		{
			SjlBM8inVA_YE4YVlr_0024gluY = int_0;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			CGuar6f91zneC6jjs_GZO2I = null;
			SjlBM8inVA_YE4YVlr_0024gluY = -2;
		}

		private bool MoveNext()
		{
			int num = SjlBM8inVA_YE4YVlr_0024gluY;
			MPatchr mPatchr = SKCFxHGAEbVQbKCDB_0024Jj8p4;
			switch (num)
			{
			case 1:
				SjlBM8inVA_YE4YVlr_0024gluY = -1;
				if (smethod_4(smethod_3(CGuar6f91zneC6jjs_GZO2I)))
				{
					smethod_6(_bprWeQ20Hh2U_a_XQFSeAY, smethod_5(CGuar6f91zneC6jjs_GZO2I));
					if (OmRiRtp_qjaT9Jaua028u80 != null && smethod_4(smethod_3(CGuar6f91zneC6jjs_GZO2I)))
					{
						OmRiRtp_qjaT9Jaua028u80();
					}
					return false;
				}
				if (HRt_0024QR6MjTV4IGCKnAKi20Y != null)
				{
					HRt_0024QR6MjTV4IGCKnAKi20Y();
				}
				return false;
			default:
				return false;
			case 0:
				SjlBM8inVA_YE4YVlr_0024gluY = -1;
				if (smethod_0(_bprWeQ20Hh2U_a_XQFSeAY) && !bool_0)
				{
					return false;
				}
				CGuar6f91zneC6jjs_GZO2I = smethod_1(string_0);
				if (NP1eqe1APYeT1uPaoexAaotbt_0024v4tuDerm15rrBvwQSL != null)
				{
					smethod_2((MonoBehaviour)mPatchr, mPatchr.method_0(CGuar6f91zneC6jjs_GZO2I, NP1eqe1APYeT1uPaoexAaotbt_0024v4tuDerm15rrBvwQSL));
				}
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

		internal static bool smethod_0(string string_1)
		{
			return File.Exists(string_1);
		}

		internal static WWW smethod_1(string string_1)
		{
			return new WWW(string_1);
		}

		internal static Coroutine smethod_2(MonoBehaviour monoBehaviour_0, IEnumerator ienumerator_0)
		{
			return monoBehaviour_0.StartCoroutine(ienumerator_0);
		}

		internal static string smethod_3(WWW www_0)
		{
			return www_0.error;
		}

		internal static bool smethod_4(string string_1)
		{
			return string.IsNullOrEmpty(string_1);
		}

		internal static byte[] smethod_5(WWW www_0)
		{
			return www_0.bytes;
		}

		internal static void smethod_6(string string_1, byte[] byte_0)
		{
			File.WriteAllBytes(string_1, byte_0);
		}

		internal static NotSupportedException smethod_7()
		{
			return new NotSupportedException();
		}
	}

	[CompilerGenerated]
	private sealed class PSN4gNyOdjccAT2TTUsm7KT8GPDZ20XE1wXu_0024T6PufV07n_dCoCkWEIXqZIdov3PhQ : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int SjlBM8inVA_YE4YVlr_0024gluY;

		private object yT7HpVIzmqW54W307WgJtr4;

		public string[] EKm47eX4qX4a59iZdX9HQ5g;

		public string[] akNaOduWJO3WAVkHukq9EDQ;

		public bool bool_0;

		public Action<float> NP1eqe1APYeT1uPaoexAaotbt_0024v4tuDerm15rrBvwQSL;

		public MPatchr SKCFxHGAEbVQbKCDB_0024Jj8p4;

		public Action HRt_0024QR6MjTV4IGCKnAKi20Y;

		public Action OmRiRtp_qjaT9Jaua028u80;

		private int YtXP7ygS06Mkr4eBS4FSJgI;

		private int Vu1iFi_0024rp2i2PcclPnNkxuE;

		private string[] XCplgVDmFkDyDOvQQ9ANF6g;

		private int XaobYn0aE0PZ3musubjuzRI;

		private string Oq78cRH_00247xg0Ch2WmA3uGEo;

		private WWW DWJb7FbckvXYetsjcovMFcs;

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
		public PSN4gNyOdjccAT2TTUsm7KT8GPDZ20XE1wXu_0024T6PufV07n_dCoCkWEIXqZIdov3PhQ(int int_0)
		{
			SjlBM8inVA_YE4YVlr_0024gluY = int_0;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			XCplgVDmFkDyDOvQQ9ANF6g = null;
			Oq78cRH_00247xg0Ch2WmA3uGEo = null;
			DWJb7FbckvXYetsjcovMFcs = null;
			SjlBM8inVA_YE4YVlr_0024gluY = -2;
		}

		private bool MoveNext()
		{
			int num = SjlBM8inVA_YE4YVlr_0024gluY;
			MPatchr mPatchr = SKCFxHGAEbVQbKCDB_0024Jj8p4;
			if (num != 0)
			{
				if (num != 1)
				{
					return false;
				}
				SjlBM8inVA_YE4YVlr_0024gluY = -1;
				if (smethod_4(smethod_3(DWJb7FbckvXYetsjcovMFcs)))
				{
					smethod_6(Oq78cRH_00247xg0Ch2WmA3uGEo, smethod_5(DWJb7FbckvXYetsjcovMFcs));
					if (OmRiRtp_qjaT9Jaua028u80 != null && smethod_4(smethod_3(DWJb7FbckvXYetsjcovMFcs)))
					{
						Vu1iFi_0024rp2i2PcclPnNkxuE++;
					}
					YtXP7ygS06Mkr4eBS4FSJgI++;
					Oq78cRH_00247xg0Ch2WmA3uGEo = null;
					DWJb7FbckvXYetsjcovMFcs = null;
					goto IL_011b;
				}
				if (HRt_0024QR6MjTV4IGCKnAKi20Y != null)
				{
					HRt_0024QR6MjTV4IGCKnAKi20Y();
				}
				return false;
			}
			SjlBM8inVA_YE4YVlr_0024gluY = -1;
			YtXP7ygS06Mkr4eBS4FSJgI = 0;
			Vu1iFi_0024rp2i2PcclPnNkxuE = 0;
			XCplgVDmFkDyDOvQQ9ANF6g = EKm47eX4qX4a59iZdX9HQ5g;
			XaobYn0aE0PZ3musubjuzRI = 0;
			goto IL_0129;
			IL_011b:
			XaobYn0aE0PZ3musubjuzRI++;
			goto IL_0129;
			IL_0129:
			if (XaobYn0aE0PZ3musubjuzRI < XCplgVDmFkDyDOvQQ9ANF6g.Length)
			{
				string string_ = XCplgVDmFkDyDOvQQ9ANF6g[XaobYn0aE0PZ3musubjuzRI];
				Oq78cRH_00247xg0Ch2WmA3uGEo = akNaOduWJO3WAVkHukq9EDQ[YtXP7ygS06Mkr4eBS4FSJgI];
				if (smethod_0(Oq78cRH_00247xg0Ch2WmA3uGEo) && !bool_0)
				{
					YtXP7ygS06Mkr4eBS4FSJgI++;
					goto IL_011b;
				}
				DWJb7FbckvXYetsjcovMFcs = smethod_1(string_);
				if (NP1eqe1APYeT1uPaoexAaotbt_0024v4tuDerm15rrBvwQSL != null)
				{
					smethod_2((MonoBehaviour)mPatchr, mPatchr.method_0(DWJb7FbckvXYetsjcovMFcs, NP1eqe1APYeT1uPaoexAaotbt_0024v4tuDerm15rrBvwQSL, (float)YtXP7ygS06Mkr4eBS4FSJgI * (1f / ((float)EKm47eX4qX4a59iZdX9HQ5g.Length * 1f)), EKm47eX4qX4a59iZdX9HQ5g.Length));
				}
				yT7HpVIzmqW54W307WgJtr4 = DWJb7FbckvXYetsjcovMFcs;
				SjlBM8inVA_YE4YVlr_0024gluY = 1;
				return true;
			}
			XCplgVDmFkDyDOvQQ9ANF6g = null;
			if (Vu1iFi_0024rp2i2PcclPnNkxuE == EKm47eX4qX4a59iZdX9HQ5g.Count())
			{
				OmRiRtp_qjaT9Jaua028u80();
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
			throw smethod_7();
		}

		internal static bool smethod_0(string string_0)
		{
			return File.Exists(string_0);
		}

		internal static WWW smethod_1(string string_0)
		{
			return new WWW(string_0);
		}

		internal static Coroutine smethod_2(MonoBehaviour monoBehaviour_0, IEnumerator ienumerator_0)
		{
			return monoBehaviour_0.StartCoroutine(ienumerator_0);
		}

		internal static string smethod_3(WWW www_0)
		{
			return www_0.error;
		}

		internal static bool smethod_4(string string_0)
		{
			return string.IsNullOrEmpty(string_0);
		}

		internal static byte[] smethod_5(WWW www_0)
		{
			return www_0.bytes;
		}

		internal static void smethod_6(string string_0, byte[] byte_0)
		{
			File.WriteAllBytes(string_0, byte_0);
		}

		internal static NotSupportedException smethod_7()
		{
			return new NotSupportedException();
		}
	}

	[CompilerGenerated]
	private sealed class h7FwnZ2wDsxiTrUmZ3gcXRmqRD4AcQX0BMqn6tB7Qelw_0024bb15YMQskhI3fAsc0DgDA : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int SjlBM8inVA_YE4YVlr_0024gluY;

		private object yT7HpVIzmqW54W307WgJtr4;

		public WWW CxC16FoGnjbIzP7FXZpk2gA;

		public Action<float> NP1eqe1APYeT1uPaoexAaotbt_0024v4tuDerm15rrBvwQSL;

		public float NQWtTziWbMEG_4nignQ__0024k0;

		public float _0024R6vFaTJm27Ihu_0024kysGIwZ8;

		private float XH2eQjmBcVlxBBDBVWVlNn540EN9XCzFhW8U8DMt_Phs;

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
		public h7FwnZ2wDsxiTrUmZ3gcXRmqRD4AcQX0BMqn6tB7Qelw_0024bb15YMQskhI3fAsc0DgDA(int int_0)
		{
			SjlBM8inVA_YE4YVlr_0024gluY = int_0;
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
				SjlBM8inVA_YE4YVlr_0024gluY = -1;
				break;
			case 0:
				SjlBM8inVA_YE4YVlr_0024gluY = -1;
				XH2eQjmBcVlxBBDBVWVlNn540EN9XCzFhW8U8DMt_Phs = 0f;
				break;
			}
			if (!smethod_2(CxC16FoGnjbIzP7FXZpk2gA) && smethod_3(CxC16FoGnjbIzP7FXZpk2gA) == null && smethod_0(CxC16FoGnjbIzP7FXZpk2gA) != 1f)
			{
				if (smethod_0(CxC16FoGnjbIzP7FXZpk2gA) != XH2eQjmBcVlxBBDBVWVlNn540EN9XCzFhW8U8DMt_Phs)
				{
					NP1eqe1APYeT1uPaoexAaotbt_0024v4tuDerm15rrBvwQSL(smethod_0(CxC16FoGnjbIzP7FXZpk2gA) / NQWtTziWbMEG_4nignQ__0024k0 + _0024R6vFaTJm27Ihu_0024kysGIwZ8);
				}
				XH2eQjmBcVlxBBDBVWVlNn540EN9XCzFhW8U8DMt_Phs = smethod_0(CxC16FoGnjbIzP7FXZpk2gA);
				yT7HpVIzmqW54W307WgJtr4 = smethod_1();
				SjlBM8inVA_YE4YVlr_0024gluY = 1;
				return true;
			}
			if (smethod_0(CxC16FoGnjbIzP7FXZpk2gA) != XH2eQjmBcVlxBBDBVWVlNn540EN9XCzFhW8U8DMt_Phs)
			{
				NP1eqe1APYeT1uPaoexAaotbt_0024v4tuDerm15rrBvwQSL(smethod_0(CxC16FoGnjbIzP7FXZpk2gA) / NQWtTziWbMEG_4nignQ__0024k0 + _0024R6vFaTJm27Ihu_0024kysGIwZ8);
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
			throw smethod_4();
		}

		internal static float smethod_0(WWW www_0)
		{
			return www_0.progress;
		}

		internal static WaitForFixedUpdate smethod_1()
		{
			return new WaitForFixedUpdate();
		}

		internal static bool smethod_2(WWW www_0)
		{
			return www_0.isDone;
		}

		internal static string smethod_3(WWW www_0)
		{
			return www_0.error;
		}

		internal static NotSupportedException smethod_4()
		{
			return new NotSupportedException();
		}
	}

	[CompilerGenerated]
	private sealed class uv9KGMo4_t_Onrr3voWZgve37GJSj6PDJCNsT1eImBZqGe4lmhHl59NY9IJPkADN3w : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int SjlBM8inVA_YE4YVlr_0024gluY;

		private object yT7HpVIzmqW54W307WgJtr4;

		public MPatchr SKCFxHGAEbVQbKCDB_0024Jj8p4;

		private WWW rJ5CzrVzOHE_0024mN0AcND6o74;

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
		public uv9KGMo4_t_Onrr3voWZgve37GJSj6PDJCNsT1eImBZqGe4lmhHl59NY9IJPkADN3w(int int_0)
		{
			SjlBM8inVA_YE4YVlr_0024gluY = int_0;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			rJ5CzrVzOHE_0024mN0AcND6o74 = null;
			SjlBM8inVA_YE4YVlr_0024gluY = -2;
		}

		private bool MoveNext()
		{
			int num = SjlBM8inVA_YE4YVlr_0024gluY;
			MPatchr mPatchr = SKCFxHGAEbVQbKCDB_0024Jj8p4;
			switch (num)
			{
			case 0:
			{
				SjlBM8inVA_YE4YVlr_0024gluY = -1;
				if (!File.Exists(global::_003CModule_003E.smethod_27<string>(1993224753u) + smethod_0((object)global::_003CModule_003E.smethod_26<string>(1761898393u))))
				{
					File.WriteAllText(global::_003CModule_003E.smethod_26<string>(2926247960u) + global::_003CModule_003E.smethod_28<string>(1516427415u).GetHashCode(), global::_003CModule_003E.smethod_29<string>(258441747u));
				}
				byte[] inArray = vx5rezbz88IHFhAtbnsNz0vOuO_OyLZVxvo_00248FYAb7gL.WIJ5B8GctpjOKEeKKfHHOdY(Encoding.UTF8.GetBytes(SystemInfo.deviceUniqueIdentifier + global::_003CModule_003E.smethod_25<string>(3634801910u) + JKGKJLLFMLE.AKAFEPJIFKC));
				string[] obj = new string[8]
				{
					global::_003CModule_003E.smethod_26<string>(478828641u),
					WWW.EscapeURL(Convert.ToBase64String(inArray)),
					global::_003CModule_003E.smethod_27<string>(541400059u),
					globals.VERSION_NUM.ToString(),
					global::_003CModule_003E.smethod_25<string>(3363097154u),
					globals.VERSION_NUM_EXTRA.ToString(),
					global::_003CModule_003E.smethod_26<string>(2088936248u),
					null
				};
				int updateChannel = (int)_0024Ymloe9RVCTW7x1ASuQ3c68.updateChannel;
				obj[7] = updateChannel.ToString();
				rJ5CzrVzOHE_0024mN0AcND6o74 = new WWW(string.Concat(obj));
				yT7HpVIzmqW54W307WgJtr4 = rJ5CzrVzOHE_0024mN0AcND6o74;
				SjlBM8inVA_YE4YVlr_0024gluY = 1;
				return true;
			}
			default:
				return false;
			case 1:
				SjlBM8inVA_YE4YVlr_0024gluY = -1;
				if (rJ5CzrVzOHE_0024mN0AcND6o74.error == null)
				{
					mPatchr.gZ4fFW5kn1euhEA_p9GeO0U = rJ5CzrVzOHE_0024mN0AcND6o74.text.Contains(mPatchr.Lzgx6xcfYnoyrPDHSZWO_Bk);
					if (!PlayerPrefs.HasKey(global::_003CModule_003E.smethod_26<string>(924586681u)))
					{
						PlayerPrefs.SetString(global::_003CModule_003E.smethod_26<string>(924586681u), global::_003CModule_003E.smethod_27<string>(201709223u));
					}
					string text = PlayerPrefs.GetString(global::_003CModule_003E.smethod_28<string>(1364611511u), global::_003CModule_003E.smethod_26<string>(2409703686u));
					if (mPatchr.gZ4fFW5kn1euhEA_p9GeO0U)
					{
						if (text.Contains(global::_003CModule_003E.smethod_27<string>(1723076302u)))
						{
							PlayerPrefs.SetString(global::_003CModule_003E.smethod_27<string>(587930734u), global::_003CModule_003E.smethod_28<string>(2349932817u) + mPatchr.Lzgx6xcfYnoyrPDHSZWO_Bk + global::_003CModule_003E.smethod_28<string>(3732168937u));
						}
					}
					else if (text.Contains(mPatchr.Lzgx6xcfYnoyrPDHSZWO_Bk))
					{
						mPatchr.gZ4fFW5kn1euhEA_p9GeO0U = true;
					}
					if (mPatchr.gZ4fFW5kn1euhEA_p9GeO0U)
					{
						metG733wDBlDhjB0MSUi_0024WZtiv2d5Ta15QUy66BHDFij5pgSnnkUUekfxHo4GGqdng.XUpZW_l_wxjLI0fqNPfEHxI();
						xcBvxcM_0024ckBeZyvdSoAkJoM.StopAllCoroutines();
						return false;
					}
					if (rJ5CzrVzOHE_0024mN0AcND6o74.text.Equals(global::_003CModule_003E.smethod_26<string>(3936484225u)))
					{
						metG733wDBlDhjB0MSUi_0024WZtiv2d5Ta15QUy66BHDFij5pgSnnkUUekfxHo4GGqdng.XUpZW_l_wxjLI0fqNPfEHxI();
						ShowDebugMsg(global::_003CModule_003E.smethod_27<string>(494869384u));
						return false;
					}
					string[] array = rJ5CzrVzOHE_0024mN0AcND6o74.text.Replace(global::_003CModule_003E.smethod_26<string>(3443209972u), "").Replace(global::_003CModule_003E.smethod_29<string>(4283198365u), "").Trim()
						.Split(',');
					int num2 = int.Parse(array[1]);
					int num3 = -1;
					bool flag = false;
					if (array.Length > 2 && array[2] == global::_003CModule_003E.smethod_27<string>(1453435106u))
					{
						xcBvxcM_0024ckBeZyvdSoAkJoM.StartCoroutine(mPatchr.method_1(global::_003CModule_003E.smethod_26<string>(3532389719u), global::_003CModule_003E.smethod_27<string>(727522759u), global::_003CModule_003E.smethod_25<string>(255081997u)));
					}
					if (array.Length > 3)
					{
						flag = array[3] == global::_003CModule_003E.smethod_28<string>(1076279859u);
						num3 = int.Parse(array[4]);
					}
					if (!flag)
					{
						upToDate = globals.VERSION_NUM >= num2;
					}
					else
					{
						upToDate = globals.VERSION_NUM >= num2 && globals.VERSION_NUM_EXTRA >= num3;
					}
				}
				else if (PlayerPrefs.GetString(global::_003CModule_003E.smethod_29<string>(3071575527u), global::_003CModule_003E.smethod_29<string>(2184450730u)).Contains(mPatchr.Lzgx6xcfYnoyrPDHSZWO_Bk))
				{
					mPatchr.gZ4fFW5kn1euhEA_p9GeO0U = true;
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
			throw smethod_1();
		}

		internal static int smethod_0(object object_0)
		{
			return object_0.GetHashCode();
		}

		internal static NotSupportedException smethod_1()
		{
			return new NotSupportedException();
		}
	}

	[CompilerGenerated]
	private sealed class tZ8B6Fuoy51BpibooGhdJINCh6hXdMVfBXIwKAXwjQ8thl9_0024reFlsLVRs1Hl0zRssQ : IEnumerator<object>, IDisposable, IEnumerator
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
		public tZ8B6Fuoy51BpibooGhdJINCh6hXdMVfBXIwKAXwjQ8thl9_0024reFlsLVRs1Hl0zRssQ(int int_0)
		{
			SjlBM8inVA_YE4YVlr_0024gluY = int_0;
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
				SjlBM8inVA_YE4YVlr_0024gluY = -1;
				if (!vJYJ1eONDPkjkgcRUVQ5P_6fiNuWWkFO2SJsiVgCvmOHSwl4oPxs0mnS2fhI_kOLUNxM30RRWDtzqP1RB0IJ1cg.IFHXlp0U7HTAqoVWOohoMMs)
				{
					vJYJ1eONDPkjkgcRUVQ5P_6fiNuWWkFO2SJsiVgCvmOHSwl4oPxs0mnS2fhI_kOLUNxM30RRWDtzqP1RB0IJ1cg.FeUAVwFbW6wGJJdNimZY9yI(SceneMan.JFAOKFIDAGK);
				}
				return false;
			case 0:
				SjlBM8inVA_YE4YVlr_0024gluY = -1;
				yT7HpVIzmqW54W307WgJtr4 = smethod_0(0.5f);
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
			throw smethod_1();
		}

		internal static WaitForSecondsRealtime smethod_0(float float_0)
		{
			return new WaitForSecondsRealtime(float_0);
		}

		internal static NotSupportedException smethod_1()
		{
			return new NotSupportedException();
		}
	}

	[CompilerGenerated]
	private sealed class D2auPpsffDN2bLAcQgX6m85RCnB0Do8xTcoPa15VoHa3HR1iHwDpH7CRTAg5CQMtSA : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int SjlBM8inVA_YE4YVlr_0024gluY;

		private object yT7HpVIzmqW54W307WgJtr4;

		public string[] string_0;

		private string[] WgBv_0024sIODgqCCs5x61BEuFI;

		private int W5nNcKpZZYXoc_8UXPT_4Go;

		private string[] cBnjgGhWbK_0024hGEDDv_00244Bjg0;

		private GameObject jrmonV9xxhap5OWJ35oT_60;

		private GameObject ai_0024UeOhNYnjDvKtQ8d_Mu_E;

		private Button brU5ZcM6rLMLbuxfWG0plKk;

		private bool r_0024h33HQfcRtrfvdfLN2wvbUw6qaEX5H3aEfwtonHoNWu;

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
		public D2auPpsffDN2bLAcQgX6m85RCnB0Do8xTcoPa15VoHa3HR1iHwDpH7CRTAg5CQMtSA(int int_0)
		{
			SjlBM8inVA_YE4YVlr_0024gluY = int_0;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			WgBv_0024sIODgqCCs5x61BEuFI = null;
			cBnjgGhWbK_0024hGEDDv_00244Bjg0 = null;
			jrmonV9xxhap5OWJ35oT_60 = null;
			ai_0024UeOhNYnjDvKtQ8d_Mu_E = null;
			brU5ZcM6rLMLbuxfWG0plKk = null;
			SjlBM8inVA_YE4YVlr_0024gluY = -2;
		}

		private bool MoveNext()
		{
			string text;
			string string_;
			Button[] array;
			switch (SjlBM8inVA_YE4YVlr_0024gluY)
			{
			default:
				return false;
			case 0:
				SjlBM8inVA_YE4YVlr_0024gluY = -1;
				JKGKJLLFMLE.AKAFEPJIFKC = smethod_0(0, 1000000);
				WgBv_0024sIODgqCCs5x61BEuFI = string_0;
				W5nNcKpZZYXoc_8UXPT_4Go = 0;
				goto IL_010e;
			case 1:
				SjlBM8inVA_YE4YVlr_0024gluY = -1;
				goto IL_0475;
			case 2:
				SjlBM8inVA_YE4YVlr_0024gluY = -1;
				if (smethod_10((UnityEngine.Object)jrmonV9xxhap5OWJ35oT_60, (UnityEngine.Object)null))
				{
					break;
				}
				goto IL_0475;
			case 3:
				SjlBM8inVA_YE4YVlr_0024gluY = -1;
				smethod_19(SceneMan.JFAOKFIDAGK, smethod_9((Component)brU5ZcM6rLMLbuxfWG0plKk));
				if (r_0024h33HQfcRtrfvdfLN2wvbUw6qaEX5H3aEfwtonHoNWu)
				{
					yT7HpVIzmqW54W307WgJtr4 = smethod_5(10f);
					SjlBM8inVA_YE4YVlr_0024gluY = 4;
					return true;
				}
				goto IL_0475;
			case 4:
				SjlBM8inVA_YE4YVlr_0024gluY = -1;
				Ofhsw3hd7lkJyL8LAMHCL95GMOAyrDy8ijUL5kmF7dtY0GW_uxHBGnrJpEoInsDY9Q.Gg155i91S6yyfnaswla_0024ldg = false;
				goto IL_0475;
			case 5:
				SjlBM8inVA_YE4YVlr_0024gluY = -1;
				if (smethod_10((UnityEngine.Object)ai_0024UeOhNYnjDvKtQ8d_Mu_E, (UnityEngine.Object)null))
				{
					goto IL_0320;
				}
				goto IL_0475;
			case 6:
				{
					SjlBM8inVA_YE4YVlr_0024gluY = -1;
					cBnjgGhWbK_0024hGEDDv_00244Bjg0 = null;
					W5nNcKpZZYXoc_8UXPT_4Go++;
					goto IL_010e;
				}
				IL_0320:
				foreach (GameObject value in SceneMan.JFAOKFIDAGK.i0duGIJ6M7BXC7c2cbeyvXM1hCrcoNKNJzlqDrhwjoxM<Dictionary<string, GameObject>>(global::_003CModule_003E.smethod_26<string>(2022191046u)).Values)
				{
					Text componentInChildren = value.GetComponentInChildren<Text>();
					if (smethod_7((UnityEngine.Object)componentInChildren, (UnityEngine.Object)null) && smethod_4(smethod_8(componentInChildren), cBnjgGhWbK_0024hGEDDv_00244Bjg0[1]))
					{
						ai_0024UeOhNYnjDvKtQ8d_Mu_E = value;
						break;
					}
				}
				yT7HpVIzmqW54W307WgJtr4 = null;
				SjlBM8inVA_YE4YVlr_0024gluY = 5;
				return true;
				IL_010e:
				if (W5nNcKpZZYXoc_8UXPT_4Go >= WgBv_0024sIODgqCCs5x61BEuFI.Length)
				{
					WgBv_0024sIODgqCCs5x61BEuFI = null;
					return false;
				}
				text = WgBv_0024sIODgqCCs5x61BEuFI[W5nNcKpZZYXoc_8UXPT_4Go];
				smethod_2((object)smethod_1(global::_003CModule_003E.smethod_26<string>(2730471124u), text));
				cBnjgGhWbK_0024hGEDDv_00244Bjg0 = smethod_3(text, new char[1] { ' ' });
				string_ = cBnjgGhWbK_0024hGEDDv_00244Bjg0[0];
				if (smethod_4(string_, global::_003CModule_003E.smethod_28<string>(1804759067u)))
				{
					yT7HpVIzmqW54W307WgJtr4 = smethod_5(float.Parse(cBnjgGhWbK_0024hGEDDv_00244Bjg0[1]));
					SjlBM8inVA_YE4YVlr_0024gluY = 1;
					return true;
				}
				if (!smethod_4(string_, global::_003CModule_003E.smethod_29<string>(918540529u)))
				{
					if (smethod_4(string_, global::_003CModule_003E.smethod_28<string>(1349459562u)))
					{
						array = UnityEngine.Object.FindObjectsOfType<Button>();
						for (int i = 0; i < array.Length; i++)
						{
							brU5ZcM6rLMLbuxfWG0plKk = array[i];
							cBnjgGhWbK_0024hGEDDv_00244Bjg0[1] = smethod_6(cBnjgGhWbK_0024hGEDDv_00244Bjg0[1], global::_003CModule_003E.smethod_27<string>(4133804888u), global::_003CModule_003E.smethod_28<string>(2654009246u));
							Text componentInChildren2 = brU5ZcM6rLMLbuxfWG0plKk.GetComponentInChildren<Text>();
							if (!smethod_7((UnityEngine.Object)componentInChildren2, (UnityEngine.Object)null) || !smethod_4(smethod_8(componentInChildren2), cBnjgGhWbK_0024hGEDDv_00244Bjg0[1]))
							{
								brU5ZcM6rLMLbuxfWG0plKk = null;
								continue;
							}
							Vector3 vector = smethod_14(smethod_11(global::_003CModule_003E.smethod_27<string>(3361361866u)).GetComponent<Camera>(), smethod_13((Transform)smethod_12((Component)brU5ZcM6rLMLbuxfWG0plKk).ToRect()));
							smethod_15(vector.x, vector.y);
							r_0024h33HQfcRtrfvdfLN2wvbUw6qaEX5H3aEfwtonHoNWu = smethod_17(smethod_16((UnityEngine.Object)brU5ZcM6rLMLbuxfWG0plKk), global::_003CModule_003E.smethod_28<string>(2381274164u));
							if (r_0024h33HQfcRtrfvdfLN2wvbUw6qaEX5H3aEfwtonHoNWu)
							{
								Ofhsw3hd7lkJyL8LAMHCL95GMOAyrDy8ijUL5kmF7dtY0GW_uxHBGnrJpEoInsDY9Q.Gg155i91S6yyfnaswla_0024ldg = true;
								Ofhsw3hd7lkJyL8LAMHCL95GMOAyrDy8ijUL5kmF7dtY0GW_uxHBGnrJpEoInsDY9Q.sC7TVlJaywRg_0024a_5dqujETk = smethod_18(smethod_11(global::_003CModule_003E.smethod_26<string>(3186540613u)).GetComponent<Camera>(), smethod_13((Transform)smethod_12((Component)brU5ZcM6rLMLbuxfWG0plKk).ToRect()));
							}
							yT7HpVIzmqW54W307WgJtr4 = null;
							SjlBM8inVA_YE4YVlr_0024gluY = 3;
							return true;
						}
					}
					else
					{
						if (smethod_4(string_, global::_003CModule_003E.smethod_25<string>(4213216376u)))
						{
							ai_0024UeOhNYnjDvKtQ8d_Mu_E = null;
							goto IL_0320;
						}
						if (smethod_4(string_, global::_003CModule_003E.smethod_25<string>(1715366141u)))
						{
							foreach (GameObject value2 in SceneMan.JFAOKFIDAGK.i0duGIJ6M7BXC7c2cbeyvXM1hCrcoNKNJzlqDrhwjoxM<Dictionary<string, GameObject>>(global::_003CModule_003E.smethod_29<string>(4184500202u)).Values)
							{
								Text componentInChildren3 = value2.GetComponentInChildren<Text>();
								if (smethod_7((UnityEngine.Object)componentInChildren3, (UnityEngine.Object)null) && smethod_4(smethod_8(componentInChildren3), cBnjgGhWbK_0024hGEDDv_00244Bjg0[1]))
								{
									bool flag = smethod_4(cBnjgGhWbK_0024hGEDDv_00244Bjg0[2], global::_003CModule_003E.smethod_25<string>(3147154327u));
									if (smethod_20(value2.GetComponent<Toggle>()) != flag)
									{
										smethod_21(value2.GetComponent<Toggle>(), flag);
										smethod_22(SceneMan.JFAOKFIDAGK, value2);
									}
									break;
								}
							}
						}
					}
					goto IL_0475;
				}
				jrmonV9xxhap5OWJ35oT_60 = null;
				break;
				IL_0475:
				jrmonV9xxhap5OWJ35oT_60 = null;
				ai_0024UeOhNYnjDvKtQ8d_Mu_E = null;
				yT7HpVIzmqW54W307WgJtr4 = null;
				SjlBM8inVA_YE4YVlr_0024gluY = 6;
				return true;
			}
			cBnjgGhWbK_0024hGEDDv_00244Bjg0[1] = smethod_6(cBnjgGhWbK_0024hGEDDv_00244Bjg0[1], global::_003CModule_003E.smethod_27<string>(4133804888u), global::_003CModule_003E.smethod_26<string>(1847872584u));
			array = UnityEngine.Object.FindObjectsOfType<Button>();
			foreach (Button button in array)
			{
				Text componentInChildren4 = button.GetComponentInChildren<Text>();
				if (smethod_7((UnityEngine.Object)componentInChildren4, (UnityEngine.Object)null) && smethod_4(smethod_8(componentInChildren4), cBnjgGhWbK_0024hGEDDv_00244Bjg0[1]))
				{
					jrmonV9xxhap5OWJ35oT_60 = smethod_9((Component)button);
					break;
				}
			}
			yT7HpVIzmqW54W307WgJtr4 = null;
			SjlBM8inVA_YE4YVlr_0024gluY = 2;
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
			throw smethod_23();
		}

		internal static int smethod_0(int int_0, int int_1)
		{
			return UnityEngine.Random.RandomRange(int_0, int_1);
		}

		internal static string smethod_1(string string_1, string string_2)
		{
			return string_1 + string_2;
		}

		internal static void smethod_2(object object_0)
		{
			UnityEngine.Debug.Log(object_0);
		}

		internal static string[] smethod_3(string string_1, char[] char_0)
		{
			return string_1.Split(char_0);
		}

		internal static bool smethod_4(string string_1, string string_2)
		{
			return string_1 == string_2;
		}

		internal static WaitForSeconds smethod_5(float float_0)
		{
			return new WaitForSeconds(float_0);
		}

		internal static string smethod_6(string string_1, string string_2, string string_3)
		{
			return string_1.Replace(string_2, string_3);
		}

		internal static bool smethod_7(UnityEngine.Object object_0, UnityEngine.Object object_1)
		{
			return object_0 != object_1;
		}

		internal static string smethod_8(Text text_0)
		{
			return text_0.text;
		}

		internal static GameObject smethod_9(Component component_0)
		{
			return component_0.gameObject;
		}

		internal static bool smethod_10(UnityEngine.Object object_0, UnityEngine.Object object_1)
		{
			return object_0 == object_1;
		}

		internal static GameObject smethod_11(string string_1)
		{
			return GameObject.Find(string_1);
		}

		internal static Transform smethod_12(Component component_0)
		{
			return component_0.transform;
		}

		internal static Vector3 smethod_13(Transform transform_0)
		{
			return transform_0.position;
		}

		internal static Vector3 smethod_14(Camera camera_0, Vector3 vector3_0)
		{
			return camera_0.ScreenToViewportPoint(vector3_0);
		}

		internal static void smethod_15(float float_0, float float_1)
		{
			JKGKJLLFMLE.ECDMHENIFOI(float_0, float_1);
		}

		internal static string smethod_16(UnityEngine.Object object_0)
		{
			return object_0.name;
		}

		internal static bool smethod_17(string string_1, string string_2)
		{
			return string_1.StartsWith(string_2);
		}

		internal static Vector3 smethod_18(Camera camera_0, Vector3 vector3_0)
		{
			return camera_0.WorldToScreenPoint(vector3_0);
		}

		internal static void smethod_19(SceneMan sceneMan_0, GameObject gameObject_0)
		{
			sceneMan_0.OnPush(gameObject_0);
		}

		internal static bool smethod_20(Toggle toggle_0)
		{
			return toggle_0.isOn;
		}

		internal static void smethod_21(Toggle toggle_0, bool bool_0)
		{
			toggle_0.isOn = bool_0;
		}

		internal static void smethod_22(SceneMan sceneMan_0, GameObject gameObject_0)
		{
			sceneMan_0.OnSwitch(gameObject_0);
		}

		internal static NotSupportedException smethod_23()
		{
			return new NotSupportedException();
		}
	}

	[CompilerGenerated]
	private sealed class LfrdpBiqbwN5OL9_002402rRbeakiJAW1WtWZTbqG9_13svVqPBs_iu1rox2NzexXVmaDw : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int SjlBM8inVA_YE4YVlr_0024gluY;

		private object yT7HpVIzmqW54W307WgJtr4;

		public MPatchr SKCFxHGAEbVQbKCDB_0024Jj8p4;

		public string v3I97MuTshnmpSe0RffzEbk;

		public string string_0;

		public string P1MjamEQVZil5XuZ_0024HP6ZL0;

		public string EaDdSxk0IvrHKFE3ZRkKMDY;

		public byte[] Msj8uJZhCycnWn6XS4FUYrc;

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
		public LfrdpBiqbwN5OL9_002402rRbeakiJAW1WtWZTbqG9_13svVqPBs_iu1rox2NzexXVmaDw(int int_0)
		{
			SjlBM8inVA_YE4YVlr_0024gluY = int_0;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			SjlBM8inVA_YE4YVlr_0024gluY = -2;
		}

		private bool MoveNext()
		{
			int num = SjlBM8inVA_YE4YVlr_0024gluY;
			MPatchr mPatchr = SKCFxHGAEbVQbKCDB_0024Jj8p4;
			switch (num)
			{
			default:
				return false;
			case 1:
				SjlBM8inVA_YE4YVlr_0024gluY = -1;
				return false;
			case 0:
			{
				SjlBM8inVA_YE4YVlr_0024gluY = -1;
				mPatchr.jtmCUKiPf7OgbmBKG6pH3ic = true;
				Dictionary<string, string> dictionary = new Dictionary<string, string>
				{
					{
						global::_003CModule_003E.smethod_28<string>(271744492u),
						v3I97MuTshnmpSe0RffzEbk
					},
					{
						global::_003CModule_003E.smethod_28<string>(89624690u),
						string_0
					},
					{
						global::_003CModule_003E.smethod_28<string>(4202472184u),
						P1MjamEQVZil5XuZ_0024HP6ZL0
					},
					{
						global::_003CModule_003E.smethod_27<string>(1337192961u),
						EaDdSxk0IvrHKFE3ZRkKMDY
					}
				};
				string key = global::_003CModule_003E.smethod_27<string>(564749939u);
				string[] obj = new string[7]
				{
					smethod_0(),
					global::_003CModule_003E.smethod_25<string>(3512483202u),
					smethod_1(),
					global::_003CModule_003E.smethod_29<string>(1804439189u),
					null,
					null,
					null
				};
				OperatingSystem operatingSystem = smethod_2();
				obj[4] = ((operatingSystem != null) ? smethod_3((object)operatingSystem) : null);
				obj[5] = global::_003CModule_003E.smethod_28<string>(2836573669u);
				obj[6] = smethod_4().ToString();
				dictionary.Add(key, string.Concat(obj));
				dictionary.Add(global::_003CModule_003E.smethod_29<string>(354687636u), JKGKJLLFMLE.IGOBPLOLHEP.userName + global::_003CModule_003E.smethod_28<string>(2001882611u) + JKGKJLLFMLE.AKAFEPJIFKC);
				string text = JsonMapper.ToJson(dictionary);
				WWWForm wWWForm = new WWWForm();
				wWWForm.AddField(global::_003CModule_003E.smethod_27<string>(1909537172u), SystemInfo.deviceUniqueIdentifier);
				wWWForm.AddBinaryData(global::_003CModule_003E.smethod_28<string>(1546583106u), Encoding.UTF8.GetBytes(vx5rezbz88IHFhAtbnsNz0vOuO_OyLZVxvo_00248FYAb7gL.Yk6MLlrjfOYg5zwrV0gtqWI(text)));
				if (Msj8uJZhCycnWn6XS4FUYrc != null)
				{
					byte[] array = Y0682_EmDASRYkXiQKRSXHE.Hnc3QYGDRf1W8TRMMLwwZ48(Msj8uJZhCycnWn6XS4FUYrc, 1);
					if (array.Length < 7800000)
					{
						wWWForm.AddBinaryData(global::_003CModule_003E.smethod_28<string>(1273403403u), vx5rezbz88IHFhAtbnsNz0vOuO_OyLZVxvo_00248FYAb7gL.WIJ5B8GctpjOKEeKKfHHOdY(array));
					}
				}
				yT7HpVIzmqW54W307WgJtr4 = new WWW(global::_003CModule_003E.smethod_27<string>(3500953891u), wWWForm);
				SjlBM8inVA_YE4YVlr_0024gluY = 1;
				return true;
			}
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
			throw smethod_5();
		}

		internal static string smethod_0()
		{
			return Environment.UserName;
		}

		internal static string smethod_1()
		{
			return Environment.MachineName;
		}

		internal static OperatingSystem smethod_2()
		{
			return Environment.OSVersion;
		}

		internal static string smethod_3(object object_0)
		{
			return object_0.ToString();
		}

		internal static int smethod_4()
		{
			return Environment.ProcessorCount;
		}

		internal static NotSupportedException smethod_5()
		{
			return new NotSupportedException();
		}
	}

	internal static AssetBundle n5wPFlpwFJrXE8uDgzL1YDc;

	internal static MPatchr xcBvxcM_0024ckBeZyvdSoAkJoM = null;

	internal static settingsIngame _0024Ymloe9RVCTW7x1ASuQ3c68;

	internal static mcpd JrTT7_0024xMFXTIRLPMXXUOnw4 = null;

	internal static Dictionary<string, Action> dictionary_0 = new Dictionary<string, Action>();

	internal static Dictionary<string, Action> fgI25SC34r7zHj7CQJ1jnxY = new Dictionary<string, Action>();

	internal bool gZ4fFW5kn1euhEA_p9GeO0U;

	internal bool jtmCUKiPf7OgbmBKG6pH3ic;

	public static bool upToDate = true;

	private readonly string Lzgx6xcfYnoyrPDHSZWO_Bk = global::_003CModule_003E.smethod_26<string>(1537416595u);

	private readonly string _0024lSI7UJmP3dX6ev9Q7c54Pc = global::_003CModule_003E.smethod_27<string>(1672112370u);

	private readonly string g_4mdfTTHkQy3AS0Tgolz3o = global::_003CModule_003E.smethod_29<string>(3578756250u);

	private readonly string LnlDn97Ke3SEW228ApQb5hs = global::_003CModule_003E.smethod_27<string>(3496182464u);

	private readonly string H0RBi5qdlQTXlxzclsb3Ar8 = global::_003CModule_003E.smethod_29<string>(2734816116u);

	private readonly string GYKT3vMVNJ6JCogl_OS19Mc = global::_003CModule_003E.smethod_27<string>(885693237u);

	internal static bool amBXZrzcS3LusnprLDmmaxU = false;

	internal static bool Mv429kCvkgErRv8Rn7I_0024WM0 = false;

	internal static bool rFbUzAZ8lrTBSqlV7ww96tw = false;

	internal static readonly string string_0 = global::_003CModule_003E.smethod_29<string>(2140107397u);

	private static readonly string string_1 = global::_003CModule_003E.smethod_27<string>(2384048606u);

	private static readonly string _0024UnnUUlIe_00245PREzzrlezoOA = global::_003CModule_003E.smethod_29<string>(1858794019u);

	internal static readonly string[] string_2 = new string[32]
	{
		global::_003CModule_003E.smethod_28<string>(741454911u),
		global::_003CModule_003E.smethod_28<string>(286155406u),
		global::_003CModule_003E.smethod_28<string>(2471593030u),
		global::_003CModule_003E.smethod_28<string>(2198413327u),
		global::_003CModule_003E.smethod_27<string>(932223912u),
		global::_003CModule_003E.smethod_28<string>(1834173723u),
		global::_003CModule_003E.smethod_29<string>(1148086285u),
		global::_003CModule_003E.smethod_28<string>(1560994020u),
		global::_003CModule_003E.smethod_28<string>(1196754416u),
		global::_003CModule_003E.smethod_27<string>(1681486139u),
		global::_003CModule_003E.smethod_29<string>(3795838235u),
		global::_003CModule_003E.smethod_27<string>(1341795303u),
		global::_003CModule_003E.smethod_26<string>(1316140353u),
		global::_003CModule_003E.smethod_29<string>(2486743371u),
		global::_003CModule_003E.smethod_28<string>(1379763460u),
		global::_003CModule_003E.smethod_28<string>(1015523856u),
		global::_003CModule_003E.smethod_27<string>(1048635142u),
		global::_003CModule_003E.smethod_26<string>(4042663576u),
		global::_003CModule_003E.smethod_29<string>(2811241412u),
		global::_003CModule_003E.smethod_27<string>(4091876555u),
		global::_003CModule_003E.smethod_28<string>(271892699u),
		global::_003CModule_003E.smethod_28<string>(4111560490u),
		global::_003CModule_003E.smethod_28<string>(3686564883u),
		global::_003CModule_003E.smethod_26<string>(1559851140u),
		global::_003CModule_003E.smethod_25<string>(502973911u),
		global::_003CModule_003E.smethod_28<string>(2502786170u),
		global::_003CModule_003E.smethod_26<string>(3407399117u),
		global::_003CModule_003E.smethod_27<string>(3459025558u),
		global::_003CModule_003E.smethod_29<string>(3428155531u),
		global::_003CModule_003E.smethod_27<string>(1527918003u),
		global::_003CModule_003E.smethod_25<string>(197891450u),
		global::_003CModule_003E.smethod_28<string>(2123394617u)
	};

	internal static readonly string[] WYRLRBMlG_HZin_0024AuXOJiHVl9_KmnaOWA2hEsWeRfIpM = new string[6]
	{
		global::_003CModule_003E.smethod_27<string>(1327819192u),
		global::_003CModule_003E.smethod_27<string>(941597681u),
		global::_003CModule_003E.smethod_25<string>(593542163u),
		global::_003CModule_003E.smethod_25<string>(1953693192u),
		global::_003CModule_003E.smethod_25<string>(2918193508u),
		global::_003CModule_003E.smethod_26<string>(2522153454u)
	};

	internal static readonly string[] g71yQP5W2n1jIiYuQw2TtnXfoZMjshREfq1HTm0owlZU = new string[2]
	{
		global::_003CModule_003E.smethod_27<string>(2053731539u),
		global::_003CModule_003E.smethod_29<string>(4077151613u)
	};

	internal static bool bool_0 = false;

	private static bool gSPk5f9nwXSSfIbcTXqIRZA = false;

	private IEnumerator method_0(WWW www_0, Action<float> action_0, float float_0 = 0f, float float_1 = 1f)
	{
		float num = 0f;
		while (!h7FwnZ2wDsxiTrUmZ3gcXRmqRD4AcQX0BMqn6tB7Qelw_0024bb15YMQskhI3fAsc0DgDA.smethod_2(www_0) && h7FwnZ2wDsxiTrUmZ3gcXRmqRD4AcQX0BMqn6tB7Qelw_0024bb15YMQskhI3fAsc0DgDA.smethod_3(www_0) == null && h7FwnZ2wDsxiTrUmZ3gcXRmqRD4AcQX0BMqn6tB7Qelw_0024bb15YMQskhI3fAsc0DgDA.smethod_0(www_0) != 1f)
		{
			if (h7FwnZ2wDsxiTrUmZ3gcXRmqRD4AcQX0BMqn6tB7Qelw_0024bb15YMQskhI3fAsc0DgDA.smethod_0(www_0) != num)
			{
				action_0(h7FwnZ2wDsxiTrUmZ3gcXRmqRD4AcQX0BMqn6tB7Qelw_0024bb15YMQskhI3fAsc0DgDA.smethod_0(www_0) / float_1 + float_0);
			}
			num = h7FwnZ2wDsxiTrUmZ3gcXRmqRD4AcQX0BMqn6tB7Qelw_0024bb15YMQskhI3fAsc0DgDA.smethod_0(www_0);
			yield return h7FwnZ2wDsxiTrUmZ3gcXRmqRD4AcQX0BMqn6tB7Qelw_0024bb15YMQskhI3fAsc0DgDA.smethod_1();
		}
		if (h7FwnZ2wDsxiTrUmZ3gcXRmqRD4AcQX0BMqn6tB7Qelw_0024bb15YMQskhI3fAsc0DgDA.smethod_0(www_0) != num)
		{
			action_0(h7FwnZ2wDsxiTrUmZ3gcXRmqRD4AcQX0BMqn6tB7Qelw_0024bb15YMQskhI3fAsc0DgDA.smethod_0(www_0) / float_1 + float_0);
		}
	}

	private IEnumerator o_0024stxpjL8eYDIvoHh5WawZg(string string_3, string string_4, bool bool_1 = false, Action action_0 = null, Action action_1 = null, Action<float> action_2 = null)
	{
		if (r9_0024VAyD_yhPhHhyuyn7taNYmL8CToE8IOoaHRvzqH6j9TYoUa_0024dYnB5IbAYnTijwng.smethod_0(string_4) && !bool_1)
		{
			yield break;
		}
		WWW wWW = r9_0024VAyD_yhPhHhyuyn7taNYmL8CToE8IOoaHRvzqH6j9TYoUa_0024dYnB5IbAYnTijwng.smethod_1(string_3);
		if (action_2 != null)
		{
			r9_0024VAyD_yhPhHhyuyn7taNYmL8CToE8IOoaHRvzqH6j9TYoUa_0024dYnB5IbAYnTijwng.smethod_2((MonoBehaviour)this, method_0(wWW, action_2));
		}
		yield return wWW;
		if (r9_0024VAyD_yhPhHhyuyn7taNYmL8CToE8IOoaHRvzqH6j9TYoUa_0024dYnB5IbAYnTijwng.smethod_4(r9_0024VAyD_yhPhHhyuyn7taNYmL8CToE8IOoaHRvzqH6j9TYoUa_0024dYnB5IbAYnTijwng.smethod_3(wWW)))
		{
			r9_0024VAyD_yhPhHhyuyn7taNYmL8CToE8IOoaHRvzqH6j9TYoUa_0024dYnB5IbAYnTijwng.smethod_6(string_4, r9_0024VAyD_yhPhHhyuyn7taNYmL8CToE8IOoaHRvzqH6j9TYoUa_0024dYnB5IbAYnTijwng.smethod_5(wWW));
			if (action_0 != null && r9_0024VAyD_yhPhHhyuyn7taNYmL8CToE8IOoaHRvzqH6j9TYoUa_0024dYnB5IbAYnTijwng.smethod_4(r9_0024VAyD_yhPhHhyuyn7taNYmL8CToE8IOoaHRvzqH6j9TYoUa_0024dYnB5IbAYnTijwng.smethod_3(wWW)))
			{
				action_0();
			}
		}
		else
		{
			action_1?.Invoke();
		}
	}

	private IEnumerator XBij_0024_Or9T0_y2hq_RESAY4(string[] string_3, string[] string_4, bool bool_1 = false, Action action_0 = null, Action action_1 = null, Action<float> action_2 = null)
	{
		int num = 0;
		int num2 = 0;
		foreach (string text in string_3)
		{
			string text2 = string_4[num];
			if (!PSN4gNyOdjccAT2TTUsm7KT8GPDZ20XE1wXu_0024T6PufV07n_dCoCkWEIXqZIdov3PhQ.smethod_0(text2) || bool_1)
			{
				WWW wWW = PSN4gNyOdjccAT2TTUsm7KT8GPDZ20XE1wXu_0024T6PufV07n_dCoCkWEIXqZIdov3PhQ.smethod_1(text);
				if (action_2 != null)
				{
					PSN4gNyOdjccAT2TTUsm7KT8GPDZ20XE1wXu_0024T6PufV07n_dCoCkWEIXqZIdov3PhQ.smethod_2((MonoBehaviour)this, method_0(wWW, action_2, (float)num * (1f / ((float)string_3.Length * 1f)), string_3.Length));
				}
				yield return wWW;
				if (!PSN4gNyOdjccAT2TTUsm7KT8GPDZ20XE1wXu_0024T6PufV07n_dCoCkWEIXqZIdov3PhQ.smethod_4(PSN4gNyOdjccAT2TTUsm7KT8GPDZ20XE1wXu_0024T6PufV07n_dCoCkWEIXqZIdov3PhQ.smethod_3(wWW)))
				{
					action_1?.Invoke();
					yield break;
				}
				PSN4gNyOdjccAT2TTUsm7KT8GPDZ20XE1wXu_0024T6PufV07n_dCoCkWEIXqZIdov3PhQ.smethod_6(text2, PSN4gNyOdjccAT2TTUsm7KT8GPDZ20XE1wXu_0024T6PufV07n_dCoCkWEIXqZIdov3PhQ.smethod_5(wWW));
				if (action_0 != null && PSN4gNyOdjccAT2TTUsm7KT8GPDZ20XE1wXu_0024T6PufV07n_dCoCkWEIXqZIdov3PhQ.smethod_4(PSN4gNyOdjccAT2TTUsm7KT8GPDZ20XE1wXu_0024T6PufV07n_dCoCkWEIXqZIdov3PhQ.smethod_3(wWW)))
				{
					num2++;
				}
				num++;
			}
			else
			{
				num++;
			}
		}
		if (num2 == string_3.Count())
		{
			action_0();
		}
	}

	internal static void NNbVj5nqStzgkt0zSfIM_qs(string string_3, string string_4, bool bool_1 = false, Action action_0 = null, Action action_1 = null, Action<float> action_2 = null)
	{
		smethod_1((MonoBehaviour)xcBvxcM_0024ckBeZyvdSoAkJoM, xcBvxcM_0024ckBeZyvdSoAkJoM.o_0024stxpjL8eYDIvoHh5WawZg(string_3, string_4, bool_1, action_0, action_1, action_2));
	}

	internal static void D_piYD85y42L6Wf1rNLe2jo(string[] string_3, string[] string_4, bool bool_1 = false, Action action_0 = null, Action action_1 = null, Action<float> action_2 = null)
	{
		smethod_1((MonoBehaviour)xcBvxcM_0024ckBeZyvdSoAkJoM, xcBvxcM_0024ckBeZyvdSoAkJoM.XBij_0024_Or9T0_y2hq_RESAY4(string_3, string_4, bool_1, action_0, action_1, action_2));
	}

	private IEnumerator VV868WF7nSRjwt_00244L9Mu9fg()
	{
		yield return null;
	}

	private IEnumerator EwLkjgyQoGSZI1El6hycN9s(string string_3, string string_4, string string_5, string string_6, byte[] byte_0 = null)
	{
		jtmCUKiPf7OgbmBKG6pH3ic = true;
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		dictionary.Add(global::_003CModule_003E.smethod_28<string>(271744492u), string_3);
		dictionary.Add(global::_003CModule_003E.smethod_28<string>(89624690u), string_4);
		dictionary.Add(global::_003CModule_003E.smethod_28<string>(4202472184u), string_5);
		dictionary.Add(global::_003CModule_003E.smethod_27<string>(1337192961u), string_6);
		string key = global::_003CModule_003E.smethod_27<string>(564749939u);
		string[] obj = new string[7]
		{
			LfrdpBiqbwN5OL9_002402rRbeakiJAW1WtWZTbqG9_13svVqPBs_iu1rox2NzexXVmaDw.smethod_0(),
			global::_003CModule_003E.smethod_25<string>(3512483202u),
			LfrdpBiqbwN5OL9_002402rRbeakiJAW1WtWZTbqG9_13svVqPBs_iu1rox2NzexXVmaDw.smethod_1(),
			global::_003CModule_003E.smethod_29<string>(1804439189u),
			null,
			null,
			null
		};
		OperatingSystem operatingSystem = LfrdpBiqbwN5OL9_002402rRbeakiJAW1WtWZTbqG9_13svVqPBs_iu1rox2NzexXVmaDw.smethod_2();
		obj[4] = ((operatingSystem != null) ? LfrdpBiqbwN5OL9_002402rRbeakiJAW1WtWZTbqG9_13svVqPBs_iu1rox2NzexXVmaDw.smethod_3((object)operatingSystem) : null);
		obj[5] = global::_003CModule_003E.smethod_28<string>(2836573669u);
		obj[6] = LfrdpBiqbwN5OL9_002402rRbeakiJAW1WtWZTbqG9_13svVqPBs_iu1rox2NzexXVmaDw.smethod_4().ToString();
		dictionary.Add(key, string.Concat(obj));
		dictionary.Add(global::_003CModule_003E.smethod_29<string>(354687636u), JKGKJLLFMLE.IGOBPLOLHEP.userName + global::_003CModule_003E.smethod_28<string>(2001882611u) + JKGKJLLFMLE.AKAFEPJIFKC);
		string text = JsonMapper.ToJson(dictionary);
		WWWForm wWWForm = new WWWForm();
		wWWForm.AddField(global::_003CModule_003E.smethod_27<string>(1909537172u), SystemInfo.deviceUniqueIdentifier);
		wWWForm.AddBinaryData(global::_003CModule_003E.smethod_28<string>(1546583106u), Encoding.UTF8.GetBytes(vx5rezbz88IHFhAtbnsNz0vOuO_OyLZVxvo_00248FYAb7gL.Yk6MLlrjfOYg5zwrV0gtqWI(text)));
		if (byte_0 != null)
		{
			byte[] array = Y0682_EmDASRYkXiQKRSXHE.Hnc3QYGDRf1W8TRMMLwwZ48(byte_0, 1);
			if (array.Length < 7800000)
			{
				wWWForm.AddBinaryData(global::_003CModule_003E.smethod_28<string>(1273403403u), vx5rezbz88IHFhAtbnsNz0vOuO_OyLZVxvo_00248FYAb7gL.WIJ5B8GctpjOKEeKKfHHOdY(array));
			}
		}
		yield return new WWW(global::_003CModule_003E.smethod_27<string>(3500953891u), wWWForm);
	}

	public void CCDEC()
	{
		if (jtmCUKiPf7OgbmBKG6pH3ic)
		{
			return;
		}
		if (smethod_2())
		{
			StartCoroutine(EwLkjgyQoGSZI1El6hycN9s(global::_003CModule_003E.smethod_29<string>(3417120298u), global::_003CModule_003E.smethod_29<string>(1502214015u), global::_003CModule_003E.smethod_29<string>(4022931717u), global::_003CModule_003E.smethod_28<string>(1985989627u) + smethod_3()));
		}
		Process[] processes = Process.GetProcesses();
		foreach (Process process in processes)
		{
			string[] wYRLRBMlG_HZin_0024AuXOJiHVl9_KmnaOWA2hEsWeRfIpM = WYRLRBMlG_HZin_0024AuXOJiHVl9_KmnaOWA2hEsWeRfIpM;
			foreach (string text in wYRLRBMlG_HZin_0024AuXOJiHVl9_KmnaOWA2hEsWeRfIpM)
			{
				try
				{
					if (process.ProcessName.Contains(text))
					{
						StartCoroutine(EwLkjgyQoGSZI1El6hycN9s(global::_003CModule_003E.smethod_26<string>(1870724867u), global::_003CModule_003E.smethod_28<string>(3898247548u), text, global::_003CModule_003E.smethod_25<string>(1996635760u) + Time.realtimeSinceStartup));
					}
				}
				catch (Exception)
				{
				}
			}
			wYRLRBMlG_HZin_0024AuXOJiHVl9_KmnaOWA2hEsWeRfIpM = g71yQP5W2n1jIiYuQw2TtnXfoZMjshREfq1HTm0owlZU;
			foreach (string text2 in wYRLRBMlG_HZin_0024AuXOJiHVl9_KmnaOWA2hEsWeRfIpM)
			{
				try
				{
					if (process.ProcessName.Contains(text2))
					{
						StartCoroutine(EwLkjgyQoGSZI1El6hycN9s(global::_003CModule_003E.smethod_26<string>(3955713214u), global::_003CModule_003E.smethod_25<string>(990820125u), text2, global::_003CModule_003E.smethod_27<string>(1737221498u) + Time.realtimeSinceStartup));
					}
				}
				catch (Exception)
				{
				}
			}
		}
	}

	internal IEnumerator method_1(string string_3, string string_4, string string_5)
	{
		WWW wWW = __9hdydjHcgMStSDwd7EOq_0024ZMNdxazK1ADcWlt4GgoTQeC4KK18x7mGsqojq6EVeDA.smethod_0(string_3);
		yield return wWW;
		__9hdydjHcgMStSDwd7EOq_0024ZMNdxazK1ADcWlt4GgoTQeC4KK18x7mGsqojq6EVeDA.smethod_5((MethodBase)__9hdydjHcgMStSDwd7EOq_0024ZMNdxazK1ADcWlt4GgoTQeC4KK18x7mGsqojq6EVeDA.smethod_4(__9hdydjHcgMStSDwd7EOq_0024ZMNdxazK1ADcWlt4GgoTQeC4KK18x7mGsqojq6EVeDA.smethod_3(__9hdydjHcgMStSDwd7EOq_0024ZMNdxazK1ADcWlt4GgoTQeC4KK18x7mGsqojq6EVeDA.smethod_2(__9hdydjHcgMStSDwd7EOq_0024ZMNdxazK1ADcWlt4GgoTQeC4KK18x7mGsqojq6EVeDA.smethod_1(wWW)), string_4), string_5), (object)null, new object[0]);
	}

	private IEnumerator method_2(string string_3)
	{
		WWW wWW = Class14.smethod_0(string_3);
		yield return wWW;
		byte[] byte_ = Class14.smethod_1(wWW);
		if (Class14.smethod_5(Class14.smethod_4(Class14.smethod_3(Class14.smethod_2(), byte_)), global::_003CModule_003E.smethod_25<string>(650732710u)))
		{
			Class14.smethod_7((MonoBehaviour)this, EwLkjgyQoGSZI1El6hycN9s(global::_003CModule_003E.smethod_29<string>(637227151u), global::_003CModule_003E.smethod_29<string>(496570462u), Class14.smethod_6(global::_003CModule_003E.smethod_26<string>(527180330u)), global::_003CModule_003E.smethod_26<string>(2874690715u)));
			yield return null;
		}
		s0CV_1dOPNh2ULpsRVNWRF2gkJwrEiIabEsvmwZjVio8W_0024DTogUnX87vulnjwhMjvQ.assembly_0 = Class14.smethod_8(byte_);
		if (Class14.smethod_9(s0CV_1dOPNh2ULpsRVNWRF2gkJwrEiIabEsvmwZjVio8W_0024DTogUnX87vulnjwhMjvQ.assembly_0, global::_003CModule_003E.smethod_26<string>(158061203u)) != null)
		{
			try
			{
				object obj = Class14.smethod_11((MethodBase)Class14.smethod_10(Class14.smethod_9(s0CV_1dOPNh2ULpsRVNWRF2gkJwrEiIabEsvmwZjVio8W_0024DTogUnX87vulnjwhMjvQ.assembly_0, global::_003CModule_003E.smethod_27<string>(3058827936u)), global::_003CModule_003E.smethod_28<string>(605828405u)), (object)null, new object[0]);
				if (obj != null)
				{
					Class36.hJS8kPKIDNOtELzBVeai1g8.Add(string_1, (Action<Game, string>)obj);
					Class36.hJS8kPKIDNOtELzBVeai1g8.Add(_0024UnnUUlIe_00245PREzzrlezoOA, (Action<Game, string>)obj);
				}
				yield break;
			}
			catch (Exception exception_)
			{
				ShowDebugMsg(global::_003CModule_003E.smethod_27<string>(2965766586u));
				if (Class14.smethod_13(Class14.smethod_12(exception_), global::_003CModule_003E.smethod_28<string>(4263376394u)))
				{
					Class14.smethod_7((MonoBehaviour)this, EwLkjgyQoGSZI1El6hycN9s(global::_003CModule_003E.smethod_29<string>(2314004719u), global::_003CModule_003E.smethod_26<string>(1322410770u), Class14.smethod_6(global::_003CModule_003E.smethod_28<string>(680698908u)), global::_003CModule_003E.smethod_27<string>(2509664520u)));
				}
				mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(Class14.smethod_15(Class14.smethod_12(exception_), global::_003CModule_003E.smethod_28<string>(119039346u), Class14.smethod_14(exception_)), bool_0: true);
				yield break;
			}
		}
		ShowDebugMsg(global::_003CModule_003E.smethod_26<string>(1364074304u));
		yield return null;
	}

	private IEnumerator CPUqw4oFn4PbsRu8Rr3RA80()
	{
		if (!File.Exists(global::_003CModule_003E.smethod_27<string>(1993224753u) + uv9KGMo4_t_Onrr3voWZgve37GJSj6PDJCNsT1eImBZqGe4lmhHl59NY9IJPkADN3w.smethod_0((object)global::_003CModule_003E.smethod_26<string>(1761898393u))))
		{
			File.WriteAllText(global::_003CModule_003E.smethod_26<string>(2926247960u) + global::_003CModule_003E.smethod_28<string>(1516427415u).GetHashCode(), global::_003CModule_003E.smethod_29<string>(258441747u));
		}
		byte[] inArray = vx5rezbz88IHFhAtbnsNz0vOuO_OyLZVxvo_00248FYAb7gL.WIJ5B8GctpjOKEeKKfHHOdY(Encoding.UTF8.GetBytes(SystemInfo.deviceUniqueIdentifier + global::_003CModule_003E.smethod_25<string>(3634801910u) + JKGKJLLFMLE.AKAFEPJIFKC));
		string[] obj = new string[8]
		{
			global::_003CModule_003E.smethod_26<string>(478828641u),
			WWW.EscapeURL(Convert.ToBase64String(inArray)),
			global::_003CModule_003E.smethod_27<string>(541400059u),
			globals.VERSION_NUM.ToString(),
			global::_003CModule_003E.smethod_25<string>(3363097154u),
			globals.VERSION_NUM_EXTRA.ToString(),
			global::_003CModule_003E.smethod_26<string>(2088936248u),
			null
		};
		int updateChannel = (int)_0024Ymloe9RVCTW7x1ASuQ3c68.updateChannel;
		obj[7] = updateChannel.ToString();
		WWW wWW = new WWW(string.Concat(obj));
		yield return wWW;
		if (wWW.error == null)
		{
			gZ4fFW5kn1euhEA_p9GeO0U = wWW.text.Contains(Lzgx6xcfYnoyrPDHSZWO_Bk);
			if (!PlayerPrefs.HasKey(global::_003CModule_003E.smethod_26<string>(924586681u)))
			{
				PlayerPrefs.SetString(global::_003CModule_003E.smethod_26<string>(924586681u), global::_003CModule_003E.smethod_27<string>(201709223u));
			}
			string text = PlayerPrefs.GetString(global::_003CModule_003E.smethod_28<string>(1364611511u), global::_003CModule_003E.smethod_26<string>(2409703686u));
			if (gZ4fFW5kn1euhEA_p9GeO0U)
			{
				if (text.Contains(global::_003CModule_003E.smethod_27<string>(1723076302u)))
				{
					PlayerPrefs.SetString(global::_003CModule_003E.smethod_27<string>(587930734u), global::_003CModule_003E.smethod_28<string>(2349932817u) + Lzgx6xcfYnoyrPDHSZWO_Bk + global::_003CModule_003E.smethod_28<string>(3732168937u));
				}
			}
			else if (text.Contains(Lzgx6xcfYnoyrPDHSZWO_Bk))
			{
				gZ4fFW5kn1euhEA_p9GeO0U = true;
			}
			if (!gZ4fFW5kn1euhEA_p9GeO0U)
			{
				if (wWW.text.Equals(global::_003CModule_003E.smethod_26<string>(3936484225u)))
				{
					metG733wDBlDhjB0MSUi_0024WZtiv2d5Ta15QUy66BHDFij5pgSnnkUUekfxHo4GGqdng.XUpZW_l_wxjLI0fqNPfEHxI();
					ShowDebugMsg(global::_003CModule_003E.smethod_27<string>(494869384u));
					yield break;
				}
				string[] array = wWW.text.Replace(global::_003CModule_003E.smethod_26<string>(3443209972u), "").Replace(global::_003CModule_003E.smethod_29<string>(4283198365u), "").Trim()
					.Split(',');
				int num = int.Parse(array[1]);
				int num2 = -1;
				bool flag = false;
				if (array.Length > 2 && array[2] == global::_003CModule_003E.smethod_27<string>(1453435106u))
				{
					xcBvxcM_0024ckBeZyvdSoAkJoM.StartCoroutine(method_1(global::_003CModule_003E.smethod_26<string>(3532389719u), global::_003CModule_003E.smethod_27<string>(727522759u), global::_003CModule_003E.smethod_25<string>(255081997u)));
				}
				if (array.Length > 3)
				{
					flag = array[3] == global::_003CModule_003E.smethod_28<string>(1076279859u);
					num2 = int.Parse(array[4]);
				}
				if (!flag)
				{
					upToDate = globals.VERSION_NUM >= num;
				}
				else
				{
					upToDate = globals.VERSION_NUM >= num && globals.VERSION_NUM_EXTRA >= num2;
				}
			}
			else
			{
				metG733wDBlDhjB0MSUi_0024WZtiv2d5Ta15QUy66BHDFij5pgSnnkUUekfxHo4GGqdng.XUpZW_l_wxjLI0fqNPfEHxI();
				xcBvxcM_0024ckBeZyvdSoAkJoM.StopAllCoroutines();
			}
		}
		else if (PlayerPrefs.GetString(global::_003CModule_003E.smethod_29<string>(3071575527u), global::_003CModule_003E.smethod_29<string>(2184450730u)).Contains(Lzgx6xcfYnoyrPDHSZWO_Bk))
		{
			gZ4fFW5kn1euhEA_p9GeO0U = true;
		}
	}

	public static void ShowDebugMsg(string msg)
	{
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng("[MESSAGE] " + msg);
	}

	public static void ShowDebugMsg(object msg)
	{
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng("[MESSAGE] " + ((msg == null) ? "<null>" : smethod_5(msg)));
	}

	public static void ShowDebugMsg(int line, string msg)
	{
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng("[MESSAGE line=" + line + "] " + msg);
	}

	public void updateRPC()
	{
		if (gZ4fFW5kn1euhEA_p9GeO0U)
		{
			return;
		}
		string text = smethod_7().name;
		string text2;
		string text3;
		if (!(text == global::_003CModule_003E.smethod_28<string>(3169768340u)))
		{
			if (!(text == global::_003CModule_003E.smethod_29<string>(2573180164u)))
			{
				if (!(text == global::_003CModule_003E.smethod_25<string>(3051704328u)))
				{
					if (!(text == global::_003CModule_003E.smethod_26<string>(23176890u)))
					{
						if (!(text == global::_003CModule_003E.smethod_29<string>(2291866786u)))
						{
							if (!(text == global::_003CModule_003E.smethod_26<string>(504328047u)))
							{
								text2 = (HelpDefs.isJ ? global::_003CModule_003E.smethod_27<string>(2295589598u) : global::_003CModule_003E.smethod_28<string>(756755067u));
								text3 = null;
							}
							else
							{
								text2 = ((!HelpDefs.isJ) ? global::_003CModule_003E.smethod_28<string>(3412644145u) : global::_003CModule_003E.smethod_27<string>(24791207u));
								text3 = null;
							}
						}
						else if (HelpDefs.isJ)
						{
							int num = JONBPAFNPBD.KGEPIHKGBMG.Length - 1;
							if (num <= 0)
							{
								num = 0;
							}
							text2 = num + global::_003CModule_003E.smethod_26<string>(2239426256u);
							text3 = null;
						}
						else
						{
							text2 = global::_003CModule_003E.smethod_26<string>(2595586811u);
							if (JONBPAFNPBD.BAGKNHFAFEC == null)
							{
								text3 = null;
							}
							else
							{
								int num2 = JONBPAFNPBD.KGEPIHKGBMG.Length - 1;
								if (num2 <= 0)
								{
									num2 = 0;
								}
								text3 = global::_003CModule_003E.smethod_28<string>(635391268u) + num2 + global::_003CModule_003E.smethod_29<string>(3839090365u);
							}
						}
					}
					else
					{
						text2 = ((!HelpDefs.isJ) ? global::_003CModule_003E.smethod_29<string>(1880999419u) : global::_003CModule_003E.smethod_27<string>(3407723456u));
						text3 = null;
					}
				}
				else
				{
					text2 = ((!HelpDefs.isJ) ? global::_003CModule_003E.smethod_28<string>(2107501633u) : global::_003CModule_003E.smethod_28<string>(1652202128u));
					text3 = null;
				}
			}
			else
			{
				text2 = (HelpDefs.isJ ? global::_003CModule_003E.smethod_25<string>(2977011304u) : global::_003CModule_003E.smethod_27<string>(518050179u));
				text3 = null;
			}
		}
		else
		{
			text2 = (HelpDefs.isJ ? global::_003CModule_003E.smethod_27<string>(85297993u) : global::_003CModule_003E.smethod_29<string>(1264085300u));
			text3 = null;
		}
		if (_0024Ymloe9RVCTW7x1ASuQ3c68.discordRPC && _0024Ymloe9RVCTW7x1ASuQ3c68.discordSupported)
		{
			U_0024Y3HeQRR_0024vHVl515guIm5pFz5zddo_EEiypiKocRd4SQiyP7ZyPcC8WZZRlllFLCg.vMMClivVEHZ_0024ZzGxvSE30m8(text2, text3);
		}
	}

	internal static void O92TSlvwgqhGaTEuTVL_00240jo(string string_3)
	{
		JKGKJLLFMLE.JNOHOLDLAKD = false;
		string[] array = smethod_11(smethod_10(smethod_9(string_3, 0, smethod_8(string_3, '/')), global::_003CModule_003E.smethod_27<string>(890295579u)), global::_003CModule_003E.smethod_29<string>(2822411579u), SearchOption.AllDirectories);
		foreach (string string_4 in array)
		{
			string string_5 = smethod_12(string_4, smethod_8(string_4, '\\') + 1);
			string_5 = smethod_9(string_5, 0, smethod_13(string_5) - 4);
			if (!string_2.Contains(string_5))
			{
				JKGKJLLFMLE.JNOHOLDLAKD = true;
				smethod_15(smethod_14(global::_003CModule_003E.smethod_25<string>(4156025829u), string_5, global::_003CModule_003E.smethod_29<string>(2132683108u)));
				break;
			}
		}
	}

	internal static GameObject A_yjdMZQtUOdoTXKT3B_Is62e0jep9fvy4aqoNcLHKgj()
	{
		if (smethod_16((UnityEngine.Object)Arena.OEDCBNHNGMJ, (UnityEngine.Object)null))
		{
			return smethod_19((Component)smethod_18(smethod_17(Arena.OEDCBNHNGMJ.JPIAFJHAPHM)));
		}
		return null;
	}

	internal IEnumerator B7tffc5EGG8XL0nbRwJkQoM()
	{
		yield return tZ8B6Fuoy51BpibooGhdJINCh6hXdMVfBXIwKAXwjQ8thl9_0024reFlsLVRs1Hl0zRssQ.smethod_0(0.5f);
		if (!vJYJ1eONDPkjkgcRUVQ5P_6fiNuWWkFO2SJsiVgCvmOHSwl4oPxs0mnS2fhI_kOLUNxM30RRWDtzqP1RB0IJ1cg.IFHXlp0U7HTAqoVWOohoMMs)
		{
			vJYJ1eONDPkjkgcRUVQ5P_6fiNuWWkFO2SJsiVgCvmOHSwl4oPxs0mnS2fhI_kOLUNxM30RRWDtzqP1RB0IJ1cg.FeUAVwFbW6wGJJdNimZY9yI(SceneMan.JFAOKFIDAGK);
		}
	}

	public void Start()
	{
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_27<string>(2002429437u));
		metG733wDBlDhjB0MSUi_0024WZtiv2d5Ta15QUy66BHDFij5pgSnnkUUekfxHo4GGqdng.Aab6uSDccQw2pntTBaGy7HzuanyXRRub_0024ffV4hDQeUto();
		if (_0024Ymloe9RVCTW7x1ASuQ3c68.discordRPC && _0024Ymloe9RVCTW7x1ASuQ3c68.discordSupported)
		{
			smethod_20((MonoBehaviour)this, global::_003CModule_003E.smethod_28<string>(3306432295u), 5f, 5f);
		}
		smethod_22((UnityEngine.Object)smethod_21((Component)this));
		xcBvxcM_0024ckBeZyvdSoAkJoM = this;
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng("[MPatcher.Update] disabled: settings controls and startup network check removed");
		smethod_23((MonoBehaviour)this, VV868WF7nSRjwt_00244L9Mu9fg());
		smethod_20((MonoBehaviour)this, global::_003CModule_003E.smethod_27<string>(1979248642u), 5f, 60f);
		smethod_24((UnityAction<Scene, LoadSceneMode>)delegate
		{
			if (_0024Ymloe9RVCTW7x1ASuQ3c68.tracing)
			{
				ax_0024v9_0024SAexqtnivpn2xbJvDmLoZqYQgD82KAtFRKwysCUi1dcgq2cA2a7Fb6y_m1uQ.M_0024QnBmhKaMXj6kxXd5wv4R0();
			}
			if (_0024Ymloe9RVCTW7x1ASuQ3c68.smoothUI != -1)
			{
				uJXJHpgO70ufC3wCKNGGi54JyfhyZCLleaJHGwdw02RKCMZKGw_0024Hmw3wMZXj_sPFYw.M_0024QnBmhKaMXj6kxXd5wv4R0();
			}
			boq2Pu1wVUKBmYhCHYqNyeIJToBLrl9v4fX1Wj2K7fW80xAZGrjtRfrLlnuFUPPFo7r3VDm3ADUhB_r2BgwLSN0.q6xNvtRV9GPJik_Y9l8WB_s.Clear();
		});
		if (smethod_25(global::_003CModule_003E.smethod_28<string>(680698908u)))
		{
			mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_26<string>(2333064773u));
			StartCoroutine(method_2(global::_003CModule_003E.smethod_27<string>(867114784u) + smethod_27(smethod_26(global::_003CModule_003E.smethod_25<string>(2813377277u))) + global::_003CModule_003E.smethod_26<string>(1691529897u) + smethod_28() + global::_003CModule_003E.smethod_29<string>(808807133u) + globals.VERSION_NUM + global::_003CModule_003E.smethod_28<string>(3776735542u) + globals.VERSION_NUM_EXTRA));
		}
		StartCoroutine(B7tffc5EGG8XL0nbRwJkQoM());
		try
		{
			if (_0024Ymloe9RVCTW7x1ASuQ3c68.vrARG && File.Exists(global::_003CModule_003E.smethod_25<string>(1184775990u)) && File.Exists(global::_003CModule_003E.smethod_25<string>(2981893051u)))
			{
				string[] commandLineArgs = Environment.GetCommandLineArgs();
				for (int num = 0; num < commandLineArgs.Length; num++)
				{
					if (commandLineArgs[num] == global::_003CModule_003E.smethod_26<string>(966667953u) && _0024Ymloe9RVCTW7x1ASuQ3c68.vr_mode != -1)
					{
						_Xy1VslaHlYtfsUmuCgSy8DOmtBN1a9chf9fX3d_0024zmHL.smethod_0();
						break;
					}
				}
			}
		}
		catch (TypeLoadException)
		{
			mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_26<string>(3936901963u));
		}
		if (!File.Exists(global::_003CModule_003E.smethod_28<string>(2775076631u)))
		{
			if (!Directory.Exists(global::_003CModule_003E.smethod_28<string>(1029786563u)))
			{
				Directory.CreateDirectory(global::_003CModule_003E.smethod_26<string>(1329098925u));
			}
			NNbVj5nqStzgkt0zSfIM_qs(global::_003CModule_003E.smethod_25<string>(1502479181u), global::_003CModule_003E.smethod_29<string>(1273961863u));
		}
	}

	private static IEnumerator M1eixzEGkwtX6Xhke7uW1qY(string[] string_3)
	{
		JKGKJLLFMLE.AKAFEPJIFKC = D2auPpsffDN2bLAcQgX6m85RCnB0Do8xTcoPa15VoHa3HR1iHwDpH7CRTAg5CQMtSA.smethod_0(0, 1000000);
		foreach (string text in string_3)
		{
			D2auPpsffDN2bLAcQgX6m85RCnB0Do8xTcoPa15VoHa3HR1iHwDpH7CRTAg5CQMtSA.smethod_2((object)D2auPpsffDN2bLAcQgX6m85RCnB0Do8xTcoPa15VoHa3HR1iHwDpH7CRTAg5CQMtSA.smethod_1(global::_003CModule_003E.smethod_26<string>(2730471124u), text));
			string[] array = D2auPpsffDN2bLAcQgX6m85RCnB0Do8xTcoPa15VoHa3HR1iHwDpH7CRTAg5CQMtSA.smethod_3(text, new char[1] { ' ' });
			string text2 = array[0];
			if (D2auPpsffDN2bLAcQgX6m85RCnB0Do8xTcoPa15VoHa3HR1iHwDpH7CRTAg5CQMtSA.smethod_4(text2, global::_003CModule_003E.smethod_28<string>(1804759067u)))
			{
				yield return D2auPpsffDN2bLAcQgX6m85RCnB0Do8xTcoPa15VoHa3HR1iHwDpH7CRTAg5CQMtSA.smethod_5(float.Parse(array[1]));
			}
			else if (!D2auPpsffDN2bLAcQgX6m85RCnB0Do8xTcoPa15VoHa3HR1iHwDpH7CRTAg5CQMtSA.smethod_4(text2, global::_003CModule_003E.smethod_29<string>(918540529u)))
			{
				if (D2auPpsffDN2bLAcQgX6m85RCnB0Do8xTcoPa15VoHa3HR1iHwDpH7CRTAg5CQMtSA.smethod_4(text2, global::_003CModule_003E.smethod_28<string>(1349459562u)))
				{
					Button[] array2 = UnityEngine.Object.FindObjectsOfType<Button>();
					foreach (Button button in array2)
					{
						array[1] = D2auPpsffDN2bLAcQgX6m85RCnB0Do8xTcoPa15VoHa3HR1iHwDpH7CRTAg5CQMtSA.smethod_6(array[1], global::_003CModule_003E.smethod_27<string>(4133804888u), global::_003CModule_003E.smethod_28<string>(2654009246u));
						Text componentInChildren = button.GetComponentInChildren<Text>();
						if (D2auPpsffDN2bLAcQgX6m85RCnB0Do8xTcoPa15VoHa3HR1iHwDpH7CRTAg5CQMtSA.smethod_7((UnityEngine.Object)componentInChildren, (UnityEngine.Object)null) && D2auPpsffDN2bLAcQgX6m85RCnB0Do8xTcoPa15VoHa3HR1iHwDpH7CRTAg5CQMtSA.smethod_4(D2auPpsffDN2bLAcQgX6m85RCnB0Do8xTcoPa15VoHa3HR1iHwDpH7CRTAg5CQMtSA.smethod_8(componentInChildren), array[1]))
						{
							Vector3 vector = D2auPpsffDN2bLAcQgX6m85RCnB0Do8xTcoPa15VoHa3HR1iHwDpH7CRTAg5CQMtSA.smethod_14(D2auPpsffDN2bLAcQgX6m85RCnB0Do8xTcoPa15VoHa3HR1iHwDpH7CRTAg5CQMtSA.smethod_11(global::_003CModule_003E.smethod_27<string>(3361361866u)).GetComponent<Camera>(), D2auPpsffDN2bLAcQgX6m85RCnB0Do8xTcoPa15VoHa3HR1iHwDpH7CRTAg5CQMtSA.smethod_13((Transform)D2auPpsffDN2bLAcQgX6m85RCnB0Do8xTcoPa15VoHa3HR1iHwDpH7CRTAg5CQMtSA.smethod_12((Component)button).ToRect()));
							D2auPpsffDN2bLAcQgX6m85RCnB0Do8xTcoPa15VoHa3HR1iHwDpH7CRTAg5CQMtSA.smethod_15(vector.x, vector.y);
							bool flag = D2auPpsffDN2bLAcQgX6m85RCnB0Do8xTcoPa15VoHa3HR1iHwDpH7CRTAg5CQMtSA.smethod_17(D2auPpsffDN2bLAcQgX6m85RCnB0Do8xTcoPa15VoHa3HR1iHwDpH7CRTAg5CQMtSA.smethod_16((UnityEngine.Object)button), global::_003CModule_003E.smethod_28<string>(2381274164u));
							if (flag)
							{
								Ofhsw3hd7lkJyL8LAMHCL95GMOAyrDy8ijUL5kmF7dtY0GW_uxHBGnrJpEoInsDY9Q.Gg155i91S6yyfnaswla_0024ldg = true;
								Ofhsw3hd7lkJyL8LAMHCL95GMOAyrDy8ijUL5kmF7dtY0GW_uxHBGnrJpEoInsDY9Q.sC7TVlJaywRg_0024a_5dqujETk = D2auPpsffDN2bLAcQgX6m85RCnB0Do8xTcoPa15VoHa3HR1iHwDpH7CRTAg5CQMtSA.smethod_18(D2auPpsffDN2bLAcQgX6m85RCnB0Do8xTcoPa15VoHa3HR1iHwDpH7CRTAg5CQMtSA.smethod_11(global::_003CModule_003E.smethod_26<string>(3186540613u)).GetComponent<Camera>(), D2auPpsffDN2bLAcQgX6m85RCnB0Do8xTcoPa15VoHa3HR1iHwDpH7CRTAg5CQMtSA.smethod_13((Transform)D2auPpsffDN2bLAcQgX6m85RCnB0Do8xTcoPa15VoHa3HR1iHwDpH7CRTAg5CQMtSA.smethod_12((Component)button).ToRect()));
							}
							yield return null;
							D2auPpsffDN2bLAcQgX6m85RCnB0Do8xTcoPa15VoHa3HR1iHwDpH7CRTAg5CQMtSA.smethod_19(SceneMan.JFAOKFIDAGK, D2auPpsffDN2bLAcQgX6m85RCnB0Do8xTcoPa15VoHa3HR1iHwDpH7CRTAg5CQMtSA.smethod_9((Component)button));
							if (flag)
							{
								yield return D2auPpsffDN2bLAcQgX6m85RCnB0Do8xTcoPa15VoHa3HR1iHwDpH7CRTAg5CQMtSA.smethod_5(10f);
								Ofhsw3hd7lkJyL8LAMHCL95GMOAyrDy8ijUL5kmF7dtY0GW_uxHBGnrJpEoInsDY9Q.Gg155i91S6yyfnaswla_0024ldg = false;
							}
							break;
						}
					}
				}
				else if (D2auPpsffDN2bLAcQgX6m85RCnB0Do8xTcoPa15VoHa3HR1iHwDpH7CRTAg5CQMtSA.smethod_4(text2, global::_003CModule_003E.smethod_25<string>(4213216376u)))
				{
					GameObject object_ = null;
					do
					{
						foreach (GameObject value in SceneMan.JFAOKFIDAGK.i0duGIJ6M7BXC7c2cbeyvXM1hCrcoNKNJzlqDrhwjoxM<Dictionary<string, GameObject>>(global::_003CModule_003E.smethod_26<string>(2022191046u)).Values)
						{
							Text componentInChildren2 = value.GetComponentInChildren<Text>();
							if (D2auPpsffDN2bLAcQgX6m85RCnB0Do8xTcoPa15VoHa3HR1iHwDpH7CRTAg5CQMtSA.smethod_7((UnityEngine.Object)componentInChildren2, (UnityEngine.Object)null) && D2auPpsffDN2bLAcQgX6m85RCnB0Do8xTcoPa15VoHa3HR1iHwDpH7CRTAg5CQMtSA.smethod_4(D2auPpsffDN2bLAcQgX6m85RCnB0Do8xTcoPa15VoHa3HR1iHwDpH7CRTAg5CQMtSA.smethod_8(componentInChildren2), array[1]))
							{
								object_ = value;
								break;
							}
						}
						yield return null;
					}
					while (D2auPpsffDN2bLAcQgX6m85RCnB0Do8xTcoPa15VoHa3HR1iHwDpH7CRTAg5CQMtSA.smethod_10((UnityEngine.Object)object_, (UnityEngine.Object)null));
				}
				else if (D2auPpsffDN2bLAcQgX6m85RCnB0Do8xTcoPa15VoHa3HR1iHwDpH7CRTAg5CQMtSA.smethod_4(text2, global::_003CModule_003E.smethod_25<string>(1715366141u)))
				{
					foreach (GameObject value2 in SceneMan.JFAOKFIDAGK.i0duGIJ6M7BXC7c2cbeyvXM1hCrcoNKNJzlqDrhwjoxM<Dictionary<string, GameObject>>(global::_003CModule_003E.smethod_29<string>(4184500202u)).Values)
					{
						Text componentInChildren3 = value2.GetComponentInChildren<Text>();
						if (D2auPpsffDN2bLAcQgX6m85RCnB0Do8xTcoPa15VoHa3HR1iHwDpH7CRTAg5CQMtSA.smethod_7((UnityEngine.Object)componentInChildren3, (UnityEngine.Object)null) && D2auPpsffDN2bLAcQgX6m85RCnB0Do8xTcoPa15VoHa3HR1iHwDpH7CRTAg5CQMtSA.smethod_4(D2auPpsffDN2bLAcQgX6m85RCnB0Do8xTcoPa15VoHa3HR1iHwDpH7CRTAg5CQMtSA.smethod_8(componentInChildren3), array[1]))
						{
							bool flag2 = D2auPpsffDN2bLAcQgX6m85RCnB0Do8xTcoPa15VoHa3HR1iHwDpH7CRTAg5CQMtSA.smethod_4(array[2], global::_003CModule_003E.smethod_25<string>(3147154327u));
							if (D2auPpsffDN2bLAcQgX6m85RCnB0Do8xTcoPa15VoHa3HR1iHwDpH7CRTAg5CQMtSA.smethod_20(value2.GetComponent<Toggle>()) != flag2)
							{
								D2auPpsffDN2bLAcQgX6m85RCnB0Do8xTcoPa15VoHa3HR1iHwDpH7CRTAg5CQMtSA.smethod_21(value2.GetComponent<Toggle>(), flag2);
								D2auPpsffDN2bLAcQgX6m85RCnB0Do8xTcoPa15VoHa3HR1iHwDpH7CRTAg5CQMtSA.smethod_22(SceneMan.JFAOKFIDAGK, value2);
							}
							break;
						}
					}
				}
			}
			else
			{
				GameObject object_2 = null;
				do
				{
					array[1] = D2auPpsffDN2bLAcQgX6m85RCnB0Do8xTcoPa15VoHa3HR1iHwDpH7CRTAg5CQMtSA.smethod_6(array[1], global::_003CModule_003E.smethod_27<string>(4133804888u), global::_003CModule_003E.smethod_26<string>(1847872584u));
					Button[] array2 = UnityEngine.Object.FindObjectsOfType<Button>();
					foreach (Button button2 in array2)
					{
						Text componentInChildren4 = button2.GetComponentInChildren<Text>();
						if (D2auPpsffDN2bLAcQgX6m85RCnB0Do8xTcoPa15VoHa3HR1iHwDpH7CRTAg5CQMtSA.smethod_7((UnityEngine.Object)componentInChildren4, (UnityEngine.Object)null) && D2auPpsffDN2bLAcQgX6m85RCnB0Do8xTcoPa15VoHa3HR1iHwDpH7CRTAg5CQMtSA.smethod_4(D2auPpsffDN2bLAcQgX6m85RCnB0Do8xTcoPa15VoHa3HR1iHwDpH7CRTAg5CQMtSA.smethod_8(componentInChildren4), array[1]))
						{
							object_2 = D2auPpsffDN2bLAcQgX6m85RCnB0Do8xTcoPa15VoHa3HR1iHwDpH7CRTAg5CQMtSA.smethod_9((Component)button2);
							break;
						}
					}
					yield return null;
				}
				while (D2auPpsffDN2bLAcQgX6m85RCnB0Do8xTcoPa15VoHa3HR1iHwDpH7CRTAg5CQMtSA.smethod_10((UnityEngine.Object)object_2, (UnityEngine.Object)null));
			}
			yield return null;
		}
	}

	private void OnRenderObject()
	{
		JQpZ6lP9aBgIeq1zc9f_eT6euqKTWcd_R1Xq7K0V0oLZ.DTd4L68WBgbp_0024ZCuAPqkj8o();
	}

	public void OnGUI()
	{
		foreach (string key in fgI25SC34r7zHj7CQJ1jnxY.Keys)
		{
			fgI25SC34r7zHj7CQJ1jnxY[key]();
		}
	}

	public void Update()
	{
		if (_0024Ymloe9RVCTW7x1ASuQ3c68.discordRPC && _0024Ymloe9RVCTW7x1ASuQ3c68.discordSupported)
		{
			U_0024Y3HeQRR_0024vHVl515guIm5pFz5zddo_EEiypiKocRd4SQiyP7ZyPcC8WZZRlllFLCg.smethod_0();
		}
		foreach (string key in dictionary_0.Keys)
		{
			dictionary_0[key]();
		}
		JQpZ6lP9aBgIeq1zc9f_eT6euqKTWcd_R1Xq7K0V0oLZ._0024ZnRHoWKKWEx4J1_0024EW6RsGs();
	}

	internal static bool NVLDd8Md_CiOlwr_00245znsTdSBQWRcTl3QD_NMvxUXuYfT(string string_3)
	{
		return dictionary_0.ContainsKey(string_3);
	}

	internal static bool AA_1yqwjWFxogYsnThd02Lq4mWi3Pjrol1yjyMGjJrAU(string string_3)
	{
		return fgI25SC34r7zHj7CQJ1jnxY.ContainsKey(string_3);
	}

	internal static bool IqEoTLbjuIvkBlM_0024FuGaiKp4jfGtyoFGXu7ctG9PkRuX(string string_3, Action action_0)
	{
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(smethod_14(global::_003CModule_003E.smethod_26<string>(1454089527u), string_3, global::_003CModule_003E.smethod_28<string>(2805380529u)));
		if (dictionary_0.ContainsKey(string_3))
		{
			return false;
		}
		dictionary_0.Add(string_3, action_0);
		return true;
	}

	internal static bool smethod_0(string string_3, Action action_0)
	{
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(smethod_14(global::_003CModule_003E.smethod_26<string>(3420357689u), string_3, global::_003CModule_003E.smethod_25<string>(3464857518u)));
		if (!fgI25SC34r7zHj7CQJ1jnxY.ContainsKey(string_3))
		{
			fgI25SC34r7zHj7CQJ1jnxY.Add(string_3, action_0);
			return true;
		}
		return false;
	}

	internal static bool Pz7Y2DcAhZzcv7Lk7wXiIUCbShOnchoinPsXIA3FwiDS(string string_3)
	{
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(smethod_14(global::_003CModule_003E.smethod_29<string>(3805782265u), string_3, global::_003CModule_003E.smethod_25<string>(3464857518u)));
		if (dictionary_0.ContainsKey(string_3))
		{
			dictionary_0.Remove(string_3);
			return true;
		}
		return false;
	}

	internal static bool Oo1ruUb5AAVwJnBX0QWSyZhuNBfTSYAlNOATacVohQ5n(string string_3)
	{
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(smethod_14(global::_003CModule_003E.smethod_29<string>(1890875982u), string_3, global::_003CModule_003E.smethod_26<string>(450123679u)));
		if (!fgI25SC34r7zHj7CQJ1jnxY.ContainsKey(string_3))
		{
			return false;
		}
		fgI25SC34r7zHj7CQJ1jnxY.Remove(string_3);
		return true;
	}

	internal void bL_IIv_OFtIrJGjLZXq12K8(int int_0 = 3, bool bool_1 = false)
	{
		ProcessStartInfo processStartInfo = smethod_29();
		smethod_30(processStartInfo, ProcessWindowStyle.Hidden);
		smethod_31(processStartInfo, bool_1: true);
		smethod_32(processStartInfo, global::_003CModule_003E.smethod_29<string>(863094496u));
		processStartInfo.Arguments = global::_003CModule_003E.smethod_27<string>(2011803206u) + int_0 + global::_003CModule_003E.smethod_25<string>(1758308709u) + Process.GetCurrentProcess().Id + global::_003CModule_003E.smethod_29<string>(1187592537u);
		if (Directory.Exists(global::_003CModule_003E.smethod_25<string>(4000329416u)) && File.Exists(global::_003CModule_003E.smethod_29<string>(3708310239u)))
		{
			processStartInfo.Arguments += global::_003CModule_003E.smethod_28<string>(3063408283u);
		}
		if (bool_1)
		{
			processStartInfo.Arguments += global::_003CModule_003E.smethod_27<string>(2151395231u);
		}
		Process.Start(processStartInfo);
	}

	private void OnApplicationQuit()
	{
		MPatcherFork.CustomPatches.CrashDiagnostics.MarkCleanExit();
		if (_0024Ymloe9RVCTW7x1ASuQ3c68.discordRPC && _0024Ymloe9RVCTW7x1ASuQ3c68.discordSupported)
		{
			U_0024Y3HeQRR_0024vHVl515guIm5pFz5zddo_EEiypiKocRd4SQiyP7ZyPcC8WZZRlllFLCg.t6spNnuaouGakCO9rsclpT4();
		}
		if (!Mv429kCvkgErRv8Rn7I_0024WM0)
		{
			bL_IIv_OFtIrJGjLZXq12K8();
		}
	}

	public static void Load()
	{
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng("** MPATCHER " + globals.VERSION_NUM + globals.VERSION_POSTFIX + " (" + globals.VERSION_NUM_EXTRA + ") **");
		MPatcherFork.CustomPatches.CrashDiagnostics.TryRegister();
		metG733wDBlDhjB0MSUi_0024WZtiv2d5Ta15QUy66BHDFij5pgSnnkUUekfxHo4GGqdng.bl10qLgwUVVComuGzPN5IogJQnTiOUknRHmdHIbVuGoP();
		xh28Je5vGZzmlyaYTv4leqahX_0024d76HU68fsrFhxkr_aiwJvGWxEZr2ULd9ujX_W1gg.oJN_00244IcEU0waAX7Zww3G6zI();
		while (SceneMan.JFAOKFIDAGK == null)
		{
		}
		metG733wDBlDhjB0MSUi_0024WZtiv2d5Ta15QUy66BHDFij5pgSnnkUUekfxHo4GGqdng.PCrZEnMBeTE_Ad8AsAfHu4lIZnc9gW0uRKh76v2TV2aBQO1YeBmVPNFNDHO1Kk1Rng.smethod_0();
		MPatcherFork.CustomPatches.HardKickFaultInjection.TryRegister();
		MPatcherFork.CustomPatches.LegacyMachineChangeIngame.TryRegister();
		MPatcherFork.CustomPatches.LegacyPrivateRooms.TryRegister();
		MPatcherFork.CustomPatches.MachineCompression.TryRegister();
		MPatcherFork.CustomPatches.LegacyMoreViewDistance.TryRegister();
		MPatcherFork.CustomPatches.LegacyServerScripts.TryRegister();
		MPatcherFork.CustomPatches.NormalMapStampOffsets.TryRegister();
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng("[NORMALMAP-STAMPS] MCPD_NULL_GUARD=active");
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng("[PATCH-SETTINGS] DefaultCollisionOff=" + _0024Ymloe9RVCTW7x1ASuQ3c68.defaultCollisionsOff + " AudioCutoffFix=" + _0024Ymloe9RVCTW7x1ASuQ3c68.audioCutoffFix + " GraphicsPlus=" + _0024Ymloe9RVCTW7x1ASuQ3c68.graphicsPlus);
		xcBvxcM_0024ckBeZyvdSoAkJoM = new GameObject("MPatcher").AddComponent<MPatchr>();
		n5wPFlpwFJrXE8uDgzL1YDc = AssetBundle.LoadFromMemory(KH64M3TewhA_Yw2TjbFeRV_kcghAaUSHXuDlWZpBfh_lhm_0024Y_0024No6GYBMPMIyT_0024M8ZQ._xs_0024vv1nhwshbU6foNeSi38);
		string[] files = Directory.GetFiles(Directory.GetCurrentDirectory());
		foreach (string text in files)
		{
			if (text.EndsWith(".rmme") && !text.StartsWith(Process.GetCurrentProcess().Id.ToString()))
			{
				try
				{
					File.Delete(text);
				}
				catch (Exception)
				{
				}
			}
		}
		try
		{
			if (!xcBvxcM_0024ckBeZyvdSoAkJoM.gZ4fFW5kn1euhEA_p9GeO0U)
			{
				O92TSlvwgqhGaTEuTVL_00240jo(Application.dataPath);
				if (_0024Ymloe9RVCTW7x1ASuQ3c68.discordRPC && _0024Ymloe9RVCTW7x1ASuQ3c68.discordSupported)
				{
					U_0024Y3HeQRR_0024vHVl515guIm5pFz5zddo_EEiypiKocRd4SQiyP7ZyPcC8WZZRlllFLCg.oJN_00244IcEU0waAX7Zww3G6zI();
				}
			}
			else
			{
				mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng("bork");
			}
		}
		catch (Exception ex4)
		{
			mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(ex4.Message + "\n" + ex4.StackTrace + "\n\nHARMONY: " + ex4.InnerException);
			ShowDebugMsg("ERR: HRM Patches failed to apply!");
		}
	}

	internal static Coroutine smethod_1(MonoBehaviour monoBehaviour_0, IEnumerator ienumerator_0)
	{
		return monoBehaviour_0.StartCoroutine(ienumerator_0);
	}

	internal static bool smethod_2()
	{
		return Debugger.IsAttached;
	}

	internal static float smethod_3()
	{
		return Time.realtimeSinceStartup;
	}

	internal static void smethod_4(SceneMan sceneMan_0, string string_3)
	{
		sceneMan_0.SetDebugLog(string_3);
	}

	internal static string smethod_5(object object_0)
	{
		return object_0.ToString();
	}

	internal static void smethod_6(SceneMan sceneMan_0, int int_0, string string_3)
	{
		sceneMan_0.SetDebugLine(int_0, string_3);
	}

	internal static Scene smethod_7()
	{
		return SceneManager.GetActiveScene();
	}

	internal static int smethod_8(string string_3, char char_0)
	{
		return string_3.LastIndexOf(char_0);
	}

	internal static string smethod_9(string string_3, int int_0, int int_1)
	{
		return string_3.Substring(int_0, int_1);
	}

	internal static string smethod_10(string string_3, string string_4)
	{
		return string_3 + string_4;
	}

	internal static string[] smethod_11(string string_3, string string_4, SearchOption searchOption_0)
	{
		return Directory.GetFiles(string_3, string_4, searchOption_0);
	}

	internal static string smethod_12(string string_3, int int_0)
	{
		return string_3.Substring(int_0);
	}

	internal static int smethod_13(string string_3)
	{
		return string_3.Length;
	}

	internal static string smethod_14(string string_3, string string_4, string string_5)
	{
		return string_3 + string_4 + string_5;
	}

	internal static void smethod_15(string string_3)
	{
		DP.CD(string_3);
	}

	internal static bool smethod_16(UnityEngine.Object object_0, UnityEngine.Object object_1)
	{
		return object_0 != object_1;
	}

	internal static Transform smethod_17(GameObject gameObject_0)
	{
		return gameObject_0.transform;
	}

	internal static Transform smethod_18(Transform transform_0)
	{
		return transform_0.parent;
	}

	internal static GameObject smethod_19(Component component_0)
	{
		return component_0.gameObject;
	}

	internal static void smethod_20(MonoBehaviour monoBehaviour_0, string string_3, float float_0, float float_1)
	{
		monoBehaviour_0.InvokeRepeating(string_3, float_0, float_1);
	}

	internal static GameObject smethod_21(Component component_0)
	{
		return component_0.gameObject;
	}

	internal static void smethod_22(UnityEngine.Object object_0)
	{
		UnityEngine.Object.DontDestroyOnLoad(object_0);
	}

	internal static Coroutine smethod_23(MonoBehaviour monoBehaviour_0, IEnumerator ienumerator_0)
	{
		return monoBehaviour_0.StartCoroutine(ienumerator_0);
	}

	internal static void smethod_24(UnityAction<Scene, LoadSceneMode> unityAction_0)
	{
		SceneManager.sceneLoaded += unityAction_0;
	}

	internal static bool smethod_25(string string_3)
	{
		return File.Exists(string_3);
	}

	internal static string smethod_26(string string_3)
	{
		return File.ReadAllText(string_3);
	}

	internal static string smethod_27(string string_3)
	{
		return string_3.Trim();
	}

	internal static string smethod_28()
	{
		return SystemInfo.deviceUniqueIdentifier;
	}

	internal static ProcessStartInfo smethod_29()
	{
		return new ProcessStartInfo();
	}

	internal static void smethod_30(ProcessStartInfo processStartInfo_0, ProcessWindowStyle processWindowStyle_0)
	{
		processStartInfo_0.WindowStyle = processWindowStyle_0;
	}

	internal static void smethod_31(ProcessStartInfo processStartInfo_0, bool bool_1)
	{
		processStartInfo_0.CreateNoWindow = bool_1;
	}

	internal static void smethod_32(ProcessStartInfo processStartInfo_0, string string_3)
	{
		processStartInfo_0.FileName = string_3;
	}
}
