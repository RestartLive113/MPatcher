using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using ExitGames.Client.Photon;
using HarmonyLib;
using MPatchrMain;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

internal class aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw
{
	internal enum JVDvlJAU_UpBfFlkSS4dz_00248kv5abfDJ2__002485ml_QUV8f_QRNWaHa5H5V3A4r06LroP9HTbhPsjhXYZvZn3LwqA4ocLFaSz_CXXamUCpUMYfS
	{
		FLIP,
		MIRROR,
		TURNL,
		TURNR
	}

	internal enum vHzdJCHle0_WDdePHYCc8AhqzhWCxC0OPs26PKN15EZeVBCdnWO9jLHew1ubRXEy4NK_Omoj8gX2DsiXOUSGiTIWO9KIjLEIFV8_0024N5jD2IB99u864TUjCYn7tH6mXPiRjA
	{
		None = -1,
		Form,
		Sculpt,
		Paint,
		Setup,
		Group,
		numEditMode
	}

	[CompilerGenerated]
	private sealed class cQRmTGfalxbXpc502C_8jfAHU_0024tvTKODxuwVWhdlaC8kN3_WuxqmFuOptQs_0024tTBVYWaeV9hatXrIHMDZLnF58Ry2EQAfXDtizpLllL4kzmK5ZHUWUMhcQ3OmMQNyJ6_iZg : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int SjlBM8inVA_YE4YVlr_0024gluY;

		private object yT7HpVIzmqW54W307WgJtr4;

		public int[] yiEgK5NJ3m5NS75DZj_0024iako;

		public OPLNFKECCLE mSC9Nl_0024CzW4EDuXyt6DfwUs;

		private bool g6te3OMoAOO7rq3qCEUSfjY;

		private List<BlockData> kk7aVaoykyULev_0024LRgpXWaU;

		private MemoryStream _0CNiGGQ3H4Gq5_y1N7SfIw;

		private int lm0UG8l6ssSgcJOawkIKtcs;

		private List<BlockData>.Enumerator XkVZHbGI4Rc3FWssDUSfxWI;

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
		public cQRmTGfalxbXpc502C_8jfAHU_0024tvTKODxuwVWhdlaC8kN3_WuxqmFuOptQs_0024tTBVYWaeV9hatXrIHMDZLnF58Ry2EQAfXDtizpLllL4kzmK5ZHUWUMhcQ3OmMQNyJ6_iZg(int _003C_003E1__state)
		{
			SjlBM8inVA_YE4YVlr_0024gluY = _003C_003E1__state;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			int num = SjlBM8inVA_YE4YVlr_0024gluY;
			if (num == -3 || num == 2)
			{
				try
				{
				}
				finally
				{
					ITybmnn_CCVC5Wu_0024wHlWVVQ();
				}
			}
			kk7aVaoykyULev_0024LRgpXWaU = null;
			_0CNiGGQ3H4Gq5_y1N7SfIw = null;
			XkVZHbGI4Rc3FWssDUSfxWI = default(List<BlockData>.Enumerator);
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
					g6te3OMoAOO7rq3qCEUSfjY = true;
					if (yiEgK5NJ3m5NS75DZj_0024iako == null)
					{
						yT7HpVIzmqW54W307WgJtr4 = smethod_0(1f);
						SjlBM8inVA_YE4YVlr_0024gluY = 1;
						return true;
					}
					kk7aVaoykyULev_0024LRgpXWaU = new List<BlockData>();
					foreach (BlockData blockDatum in Build.GFJLEEJELOL.blockData)
					{
						if (yiEgK5NJ3m5NS75DZj_0024iako.Contains(blockDatum.y))
						{
							kk7aVaoykyULev_0024LRgpXWaU.Add(blockDatum);
						}
					}
					g6te3OMoAOO7rq3qCEUSfjY = false;
					goto IL_00d2;
				case 1:
					SjlBM8inVA_YE4YVlr_0024gluY = -1;
					kk7aVaoykyULev_0024LRgpXWaU = Build.GFJLEEJELOL.blockData;
					goto IL_00d2;
				case 2:
					{
						SjlBM8inVA_YE4YVlr_0024gluY = -3;
						break;
					}
					IL_00d2:
					_0CNiGGQ3H4Gq5_y1N7SfIw = smethod_1();
					lm0UG8l6ssSgcJOawkIKtcs = 0;
					XkVZHbGI4Rc3FWssDUSfxWI = kk7aVaoykyULev_0024LRgpXWaU.GetEnumerator();
					SjlBM8inVA_YE4YVlr_0024gluY = -3;
					break;
				}
				while (XkVZHbGI4Rc3FWssDUSfxWI.MoveNext())
				{
					XkVZHbGI4Rc3FWssDUSfxWI.Current.EB00nJF8bfT7ocPR4HRIIqc(_0CNiGGQ3H4Gq5_y1N7SfIw);
					lm0UG8l6ssSgcJOawkIKtcs++;
					if (smethod_2((Stream)_0CNiGGQ3H4Gq5_y1N7SfIw) > 500L)
					{
						smethod_3((Stream)_0CNiGGQ3H4Gq5_y1N7SfIw, 0L, SeekOrigin.Begin);
						byte[] byte_ = H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.lD8ilO2D57aRcy0P8Iq13zE(_0CNiGGQ3H4Gq5_y1N7SfIw);
						smethod_4(_mLDDjXswSCGaR35tynAvAg.bkEYtCXOGBC5a3sGZc1baFBEZsix9sHaNV7UHBoeB8aKxFK0bpuW1JKVsL3eaRtFkQ_0.nGT7VUZTN5zsOGBEDOWNDC4, global::_003CModule_003E.smethod_28<string>(2909665688u), mSC9Nl_0024CzW4EDuXyt6DfwUs, new object[4]
						{
							lm0UG8l6ssSgcJOawkIKtcs,
							Y0682_EmDASRYkXiQKRSXHE.Hnc3QYGDRf1W8TRMMLwwZ48(byte_, int_0),
							g6te3OMoAOO7rq3qCEUSfjY,
							kk7aVaoykyULev_0024LRgpXWaU.Count
						});
						if (g6te3OMoAOO7rq3qCEUSfjY)
						{
							g6te3OMoAOO7rq3qCEUSfjY = false;
						}
						smethod_5((Stream)_0CNiGGQ3H4Gq5_y1N7SfIw);
						lm0UG8l6ssSgcJOawkIKtcs = 0;
						_0CNiGGQ3H4Gq5_y1N7SfIw = smethod_1();
						yT7HpVIzmqW54W307WgJtr4 = smethod_0(0.01f);
						SjlBM8inVA_YE4YVlr_0024gluY = 2;
						return true;
					}
				}
				ITybmnn_CCVC5Wu_0024wHlWVVQ();
				XkVZHbGI4Rc3FWssDUSfxWI = default(List<BlockData>.Enumerator);
				if (smethod_2((Stream)_0CNiGGQ3H4Gq5_y1N7SfIw) > 0L)
				{
					smethod_3((Stream)_0CNiGGQ3H4Gq5_y1N7SfIw, 0L, SeekOrigin.Begin);
					byte[] byte_2 = H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.lD8ilO2D57aRcy0P8Iq13zE(_0CNiGGQ3H4Gq5_y1N7SfIw);
					smethod_4(_mLDDjXswSCGaR35tynAvAg.bkEYtCXOGBC5a3sGZc1baFBEZsix9sHaNV7UHBoeB8aKxFK0bpuW1JKVsL3eaRtFkQ_0.nGT7VUZTN5zsOGBEDOWNDC4, global::_003CModule_003E.smethod_25<string>(4259612073u), mSC9Nl_0024CzW4EDuXyt6DfwUs, new object[4]
					{
						lm0UG8l6ssSgcJOawkIKtcs,
						Y0682_EmDASRYkXiQKRSXHE.Hnc3QYGDRf1W8TRMMLwwZ48(byte_2, int_0),
						g6te3OMoAOO7rq3qCEUSfjY,
						kk7aVaoykyULev_0024LRgpXWaU.Count
					});
					smethod_5((Stream)_0CNiGGQ3H4Gq5_y1N7SfIw);
				}
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
			((IDisposable)XkVZHbGI4Rc3FWssDUSfxWI/*cast due to .constrained prefix*/).Dispose();
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

		internal static MemoryStream smethod_1()
		{
			return new MemoryStream();
		}

		internal static long smethod_2(Stream stream_0)
		{
			return stream_0.Position;
		}

		internal static long smethod_3(Stream stream_0, long long_0, SeekOrigin seekOrigin_0)
		{
			return stream_0.Seek(long_0, seekOrigin_0);
		}

		internal static void smethod_4(PhotonView photonView_0, string string_0, OPLNFKECCLE oplnfkeccle_0, object[] object_0)
		{
			photonView_0.RPC(string_0, oplnfkeccle_0, object_0);
		}

		internal static void smethod_5(Stream stream_0)
		{
			stream_0.Close();
		}

		internal static NotSupportedException smethod_6()
		{
			return new NotSupportedException();
		}
	}

	[CompilerGenerated]
	private sealed class L2EFyjYaXZOKMMw0KwyZO35Iv71zpoGED4WWXsO9Kgff0nuCHlvMjDRxtyej9TCtqoSD3Hd7YJ4UdirS0mlQiw2YryuG4YYQo7CoXIL4uLDy : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int SjlBM8inVA_YE4YVlr_0024gluY;

		private object yT7HpVIzmqW54W307WgJtr4;

		private List<BlockData> LqQPt0bkP30DCEiEHqeM4gI;

		private List<BlockData> Ysk7nlhO5lRriS1F0kmKAgU;

		private List<BlockData> n3ryScrBAZmcAPH8Z_0024I4f_0024Q;

		private List<BlockData>.Enumerator XaobYn0aE0PZ3musubjuzRI;

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
		public L2EFyjYaXZOKMMw0KwyZO35Iv71zpoGED4WWXsO9Kgff0nuCHlvMjDRxtyej9TCtqoSD3Hd7YJ4UdirS0mlQiw2YryuG4YYQo7CoXIL4uLDy(int _003C_003E1__state)
		{
			SjlBM8inVA_YE4YVlr_0024gluY = _003C_003E1__state;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			switch (SjlBM8inVA_YE4YVlr_0024gluY)
			{
			case -4:
			case 4:
				try
				{
				}
				finally
				{
					IkXk87oS8XNDSZg4w7A9Tz0();
				}
				break;
			case -3:
			case 3:
				try
				{
				}
				finally
				{
					ITybmnn_CCVC5Wu_0024wHlWVVQ();
				}
				break;
			}
			LqQPt0bkP30DCEiEHqeM4gI = null;
			Ysk7nlhO5lRriS1F0kmKAgU = null;
			n3ryScrBAZmcAPH8Z_0024I4f_0024Q = null;
			XaobYn0aE0PZ3musubjuzRI = default(List<BlockData>.Enumerator);
			SjlBM8inVA_YE4YVlr_0024gluY = -2;
		}

		private bool MoveNext()
		{
			try
			{
				int num;
				switch (SjlBM8inVA_YE4YVlr_0024gluY)
				{
				default:
					return false;
				case 0:
					SjlBM8inVA_YE4YVlr_0024gluY = -1;
					if (e6LNe0Mxpa_w7jYjEVFEsoY)
					{
						AzzZaKp3Ff0jxSmcOyfPmWY = true;
						return false;
					}
					e6LNe0Mxpa_w7jYjEVFEsoY = true;
					LqQPt0bkP30DCEiEHqeM4gI = new List<BlockData>();
					foreach (BlockData blockDatum in Build.GFJLEEJELOL.blockData)
					{
						LqQPt0bkP30DCEiEHqeM4gI.Add(smethod_0(blockDatum));
					}
					yT7HpVIzmqW54W307WgJtr4 = smethod_1();
					SjlBM8inVA_YE4YVlr_0024gluY = 1;
					return true;
				case 1:
					SjlBM8inVA_YE4YVlr_0024gluY = -1;
					yT7HpVIzmqW54W307WgJtr4 = smethod_1();
					SjlBM8inVA_YE4YVlr_0024gluY = 2;
					return true;
				case 2:
					SjlBM8inVA_YE4YVlr_0024gluY = -1;
					Ysk7nlhO5lRriS1F0kmKAgU = new List<BlockData>();
					n3ryScrBAZmcAPH8Z_0024I4f_0024Q = new List<BlockData>();
					num = 0;
					XaobYn0aE0PZ3musubjuzRI = LqQPt0bkP30DCEiEHqeM4gI.GetEnumerator();
					SjlBM8inVA_YE4YVlr_0024gluY = -3;
					goto IL_0156;
				case 3:
					SjlBM8inVA_YE4YVlr_0024gluY = -3;
					num = 0;
					goto IL_0156;
				case 4:
					{
						SjlBM8inVA_YE4YVlr_0024gluY = -4;
						num = 0;
						break;
					}
					IL_0156:
					while (XaobYn0aE0PZ3musubjuzRI.MoveNext())
					{
						BlockData current = XaobYn0aE0PZ3musubjuzRI.Current;
						if (!Build.GFJLEEJELOL.blockData._1q8sDnsMlmqfRgkKEi_0024uDs(current))
						{
							Ysk7nlhO5lRriS1F0kmKAgU.Add(current);
						}
						num++;
						if (num > int_1)
						{
							yT7HpVIzmqW54W307WgJtr4 = null;
							SjlBM8inVA_YE4YVlr_0024gluY = 3;
							return true;
						}
					}
					ITybmnn_CCVC5Wu_0024wHlWVVQ();
					XaobYn0aE0PZ3musubjuzRI = default(List<BlockData>.Enumerator);
					num = 0;
					XaobYn0aE0PZ3musubjuzRI = Build.GFJLEEJELOL.blockData.GetEnumerator();
					SjlBM8inVA_YE4YVlr_0024gluY = -4;
					break;
				}
				do
				{
					if (XaobYn0aE0PZ3musubjuzRI.MoveNext())
					{
						BlockData current3 = XaobYn0aE0PZ3musubjuzRI.Current;
						if (!LqQPt0bkP30DCEiEHqeM4gI._1q8sDnsMlmqfRgkKEi_0024uDs(current3))
						{
							n3ryScrBAZmcAPH8Z_0024I4f_0024Q.Add(current3);
						}
						num++;
						continue;
					}
					IkXk87oS8XNDSZg4w7A9Tz0();
					XaobYn0aE0PZ3musubjuzRI = default(List<BlockData>.Enumerator);
					if (n3ryScrBAZmcAPH8Z_0024I4f_0024Q.Count == 0 && Ysk7nlhO5lRriS1F0kmKAgU.Count == 0)
					{
						e6LNe0Mxpa_w7jYjEVFEsoY = false;
						if (AzzZaKp3Ff0jxSmcOyfPmWY)
						{
							AzzZaKp3Ff0jxSmcOyfPmWY = false;
							smethod_2((MonoBehaviour)_mLDDjXswSCGaR35tynAvAg, mBUGiiYzkcmUy7PdhBgqAxs());
						}
						return false;
					}
					MemoryStream stream_ = smethod_3();
					foreach (BlockData item in n3ryScrBAZmcAPH8Z_0024I4f_0024Q)
					{
						item.EB00nJF8bfT7ocPR4HRIIqc(stream_);
					}
					smethod_4((Stream)stream_, 0L, SeekOrigin.Begin);
					byte[] byte_ = H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.lD8ilO2D57aRcy0P8Iq13zE(stream_);
					List<Vector3> list = new List<Vector3>();
					foreach (BlockData item2 in Ysk7nlhO5lRriS1F0kmKAgU)
					{
						list.Add(new Vector3(item2.x, item2.y, item2.z));
					}
					_mLDDjXswSCGaR35tynAvAg.bkEYtCXOGBC5a3sGZc1baFBEZsix9sHaNV7UHBoeB8aKxFK0bpuW1JKVsL3eaRtFkQ_0.nGT7VUZTN5zsOGBEDOWNDC4.RPC(global::_003CModule_003E.smethod_25<string>(3535066057u), BFDCHLBGJHF.Others, list.ToArray(), Y0682_EmDASRYkXiQKRSXHE.Hnc3QYGDRf1W8TRMMLwwZ48(byte_, int_0), n3ryScrBAZmcAPH8Z_0024I4f_0024Q.Count);
					Ysk7nlhO5lRriS1F0kmKAgU = null;
					n3ryScrBAZmcAPH8Z_0024I4f_0024Q = null;
					e6LNe0Mxpa_w7jYjEVFEsoY = false;
					if (AzzZaKp3Ff0jxSmcOyfPmWY)
					{
						AzzZaKp3Ff0jxSmcOyfPmWY = false;
						_mLDDjXswSCGaR35tynAvAg.StartCoroutine(mBUGiiYzkcmUy7PdhBgqAxs());
					}
					return false;
				}
				while (num <= int_1);
				yT7HpVIzmqW54W307WgJtr4 = null;
				SjlBM8inVA_YE4YVlr_0024gluY = 4;
				return true;
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
			((IDisposable)XaobYn0aE0PZ3musubjuzRI/*cast due to .constrained prefix*/).Dispose();
		}

		private void IkXk87oS8XNDSZg4w7A9Tz0()
		{
			SjlBM8inVA_YE4YVlr_0024gluY = -1;
			((IDisposable)XaobYn0aE0PZ3musubjuzRI/*cast due to .constrained prefix*/).Dispose();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw smethod_5();
		}

		internal static BlockData smethod_0(BlockData blockData_0)
		{
			return blockData_0.Clone();
		}

		internal static WaitForEndOfFrame smethod_1()
		{
			return new WaitForEndOfFrame();
		}

		internal static Coroutine smethod_2(MonoBehaviour monoBehaviour_0, IEnumerator ienumerator_0)
		{
			return monoBehaviour_0.StartCoroutine(ienumerator_0);
		}

		internal static MemoryStream smethod_3()
		{
			return new MemoryStream();
		}

		internal static long smethod_4(Stream stream_0, long long_0, SeekOrigin seekOrigin_0)
		{
			return stream_0.Seek(long_0, seekOrigin_0);
		}

		internal static NotSupportedException smethod_5()
		{
			return new NotSupportedException();
		}
	}

	internal static Class30 _mLDDjXswSCGaR35tynAvAg = null;

	internal static lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw GjjztfvuViq0_Q_00249_BUsoYY;

	internal static lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw QEcOzq1h48SV8MxfAnba82U;

	internal static lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw sc3Wu5ekd0q_0024RAkWvkAECNI;

	internal static ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ iOXrQ3ou11UqRX_0024NnkXQnmI;

	internal static ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ KtAkTPvF6e8D9Ss2KoAUKAQ;

	internal static ListController Nsgp7ukveOtfQVdR4hYjDAI;

	private static Button G4uVRc2EKln_zYS9d6i0O5k;

	internal static readonly int int_0 = 1;

	private static bool e6LNe0Mxpa_w7jYjEVFEsoY = false;

	private static bool AzzZaKp3Ff0jxSmcOyfPmWY = false;

	internal static readonly int int_1 = 500;

	private static FieldInfo rhVWql2XvURvFsi4MgjvYz9VkYgUs_0024U8adN8jsgWo6pF;

	private static MethodInfo EfOgC9TIu4k2rsEGuDYqHPvvqvUuQ3hGBIoQtSPWT6oV;

	internal static AssetBundle eMeHtB0nEBTQAy_Ed4AHx1M = null;

	internal static Button zmh9Gea8K2d3y7BCuea6ouU
	{
		get
		{
			if (smethod_5((UnityEngine.Object)Nsgp7ukveOtfQVdR4hYjDAI, (UnityEngine.Object)null))
			{
				return null;
			}
			if (smethod_5((UnityEngine.Object)G4uVRc2EKln_zYS9d6i0O5k, (UnityEngine.Object)null))
			{
				G4uVRc2EKln_zYS9d6i0O5k = Nsgp7ukveOtfQVdR4hYjDAI.GetComponent<Button>();
			}
			return G4uVRc2EKln_zYS9d6i0O5k;
		}
	}

	internal static Build sv6WiQ0SPlxR07h3vryVBDY => (Build)SceneMan.JFAOKFIDAGK;

	internal static BlockData BlockData_0
	{
		get
		{
			return Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<Build, BlockData>(global::_003CModule_003E.smethod_28<string>(1149223671u), sv6WiQ0SPlxR07h3vryVBDY);
		}
		set
		{
			Class17.DMoImhPC2jNUt2tgmSezeT_IK_0024XK5Df9PmexG_CaKb1R(global::_003CModule_003E.smethod_28<string>(1149223671u), sv6WiQ0SPlxR07h3vryVBDY, value);
		}
	}

	internal static BlockController j3uodwqE1g7Iyn1DHaxjECjkiwusGFwp2CvfcTVBjLJHRYmIjiZ05_28w3DlSI5zLg => Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<Build, BlockController>(global::_003CModule_003E.smethod_28<string>(3698900899u), sv6WiQ0SPlxR07h3vryVBDY);

	internal static GameObject PE5sYlzltGGVQg4HL2k4HC8IHLb_pbX477PyFTi4cBWYR4CijFhQ7jSAn1NoFkOd_0024A => Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<Build, GameObject>(global::_003CModule_003E.smethod_29<string>(2322790079u), sv6WiQ0SPlxR07h3vryVBDY);

	internal static bool OwUd7bq6TgB1A4bFX_0024KLOmY => Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<Build, HIPBCCKFFAG>(global::_003CModule_003E.smethod_25<string>(1242165168u), sv6WiQ0SPlxR07h3vryVBDY).HCMMJPFOIHD;

	internal static vHzdJCHle0_WDdePHYCc8AhqzhWCxC0OPs26PKN15EZeVBCdnWO9jLHew1ubRXEy4NK_Omoj8gX2DsiXOUSGiTIWO9KIjLEIFV8_0024N5jD2IB99u864TUjCYn7tH6mXPiRjA VHzdJCHle0_WDdePHYCc8AhqzhWCxC0OPs26PKN15EZeVBCdnWO9jLHew1ubRXEy4NK_Omoj8gX2DsiXOUSGiTIWO9KIjLEIFV8_0024N5jD2IB99u864TUjCYn7tH6mXPiRjA_0 => (vHzdJCHle0_WDdePHYCc8AhqzhWCxC0OPs26PKN15EZeVBCdnWO9jLHew1ubRXEy4NK_Omoj8gX2DsiXOUSGiTIWO9KIjLEIFV8_0024N5jD2IB99u864TUjCYn7tH6mXPiRjA)Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<Build, int>(global::_003CModule_003E.smethod_26<string>(1271689001u), sv6WiQ0SPlxR07h3vryVBDY);

	private static BlockData sXd9j_0024Y9K39IIQAsJsSdEDM
	{
		get
		{
			c_0024r_0024ac_0024K3A6JmEGgPtvP4htM8Y7M_0024B3aNiM0oPQIJMKP();
			return (BlockData)smethod_6(rhVWql2XvURvFsi4MgjvYz9VkYgUs_0024U8adN8jsgWo6pF, (object)sv6WiQ0SPlxR07h3vryVBDY);
		}
		set
		{
			c_0024r_0024ac_0024K3A6JmEGgPtvP4htM8Y7M_0024B3aNiM0oPQIJMKP();
			smethod_7(rhVWql2XvURvFsi4MgjvYz9VkYgUs_0024U8adN8jsgWo6pF, (object)sv6WiQ0SPlxR07h3vryVBDY, (object)value);
			smethod_8((MethodBase)EfOgC9TIu4k2rsEGuDYqHPvvqvUuQ3hGBIoQtSPWT6oV, (object)sv6WiQ0SPlxR07h3vryVBDY, new object[2] { value.type, false });
		}
	}

	internal static Transform Sl3qb7_zclIQRMX7sWCu5EE => smethod_9(Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<Build, GameObject>(global::_003CModule_003E.smethod_28<string>(860892019u), sv6WiQ0SPlxR07h3vryVBDY));

	private static void c_0024r_0024ac_0024K3A6JmEGgPtvP4htM8Y7M_0024B3aNiM0oPQIJMKP()
	{
		if (rhVWql2XvURvFsi4MgjvYz9VkYgUs_0024U8adN8jsgWo6pF == null)
		{
			rhVWql2XvURvFsi4MgjvYz9VkYgUs_0024U8adN8jsgWo6pF = smethod_11(smethod_10(typeof(Build).TypeHandle), global::_003CModule_003E.smethod_28<string>(4138455627u));
		}
		if (EfOgC9TIu4k2rsEGuDYqHPvvqvUuQ3hGBIoQtSPWT6oV == null)
		{
			EfOgC9TIu4k2rsEGuDYqHPvvqvUuQ3hGBIoQtSPWT6oV = smethod_12(smethod_10(typeof(Build).TypeHandle), global::_003CModule_003E.smethod_26<string>(1671324737u), (Type[])null, (Type[])null);
		}
	}

	internal static void a1j0jjN_0024MJMaHvEbQb12VkSxN0YF1la8dglN_0024kcX56bR()
	{
		Class17.DQGZpGELKqeKmXweFYGEcaXR73CqGywHQI6u2TATGL80(global::_003CModule_003E.smethod_28<string>(496652415u), sv6WiQ0SPlxR07h3vryVBDY);
	}

	internal static void smethod_0(string folder, string mname)
	{
		JKGKJLLFMLE.IGOBPLOLHEP.machineName = mname;
		JKGKJLLFMLE.IGOBPLOLHEP.folderName = folder;
		JKGKJLLFMLE.CFGKIAPCDLB = folder;
	}

	internal static void GqSH_0024GjFZ_wAxTK4jePCv5hrQ4zJEGQBZ87jE_jTtN8M(bool firstArg = false)
	{
		if (!OwUd7bq6TgB1A4bFX_0024KLOmY)
		{
			Class17.DQGZpGELKqeKmXweFYGEcaXR73CqGywHQI6u2TATGL80(global::_003CModule_003E.smethod_25<string>(805199136u), sv6WiQ0SPlxR07h3vryVBDY, firstArg, true);
		}
	}

	internal static void vO_KQIaPS1OoKibvTAx3G6g(string roomname, bool loading = false)
	{
		Class17.DMoImhPC2jNUt2tgmSezeT_IK_0024XK5Df9PmexG_CaKb1R(global::_003CModule_003E.smethod_27<string>(1300543310u), SceneMan.JFAOKFIDAGK, roomname);
		Class17.DMoImhPC2jNUt2tgmSezeT_IK_0024XK5Df9PmexG_CaKb1R(global::_003CModule_003E.smethod_26<string>(909258029u), SceneMan.JFAOKFIDAGK, 0.02f);
		Class17.DMoImhPC2jNUt2tgmSezeT_IK_0024XK5Df9PmexG_CaKb1R(global::_003CModule_003E.smethod_27<string>(1979924982u), SceneMan.JFAOKFIDAGK, loading);
		smethod_13(1);
		smethod_14(CursorLockMode.Locked);
		GameObject gameObject = smethod_15(global::_003CModule_003E.smethod_26<string>(4200259477u));
		if (smethod_16((UnityEngine.Object)gameObject))
		{
			smethod_17((Behaviour)gameObject.GetComponent<EventSystem>(), bool_0: false);
		}
		KEFHJCGICLE.MHDNBCGHFPA = false;
		SceneMan.APNKDLDMACA = -1;
		if (smethod_16((UnityEngine.Object)Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<SceneMan, GameObject>(global::_003CModule_003E.smethod_27<string>(3185120190u), SceneMan.JFAOKFIDAGK)))
		{
			smethod_18((UnityEngine.Object)Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<SceneMan, GameObject>(global::_003CModule_003E.smethod_25<string>(1332733420u), SceneMan.JFAOKFIDAGK));
		}
		smethod_20(smethod_19(), ProcessPriorityClass.Normal);
	}

	internal static void HAUjeOv9NpGdCjgGsLAf_0024_0024w(BlockData bd, bool addtobd = true, bool refresh = true)
	{
		if (bd.type == (BlockData.AAHMDBHDCDK)(-1))
		{
			SdmR5MvApM0UiXaOngPQlXs(bd.x, bd.y, bd.z, refresh);
			return;
		}
		if (refresh)
		{
			GqSH_0024GjFZ_wAxTK4jePCv5hrQ4zJEGQBZ87jE_jTtN8M();
		}
		if (smethod_21((UnityEngine.Object)FIaa_0024NpNdufR51oyzfpkj3A(bd.x, bd.y, bd.z), (UnityEngine.Object)null))
		{
			SdmR5MvApM0UiXaOngPQlXs(bd.x, bd.y, bd.z);
		}
		Class17.DQGZpGELKqeKmXweFYGEcaXR73CqGywHQI6u2TATGL80<Build, GameObject>(global::_003CModule_003E.smethod_25<string>(1061028664u), sv6WiQ0SPlxR07h3vryVBDY, new object[2]
		{
			smethod_22(bd, bool_0: false),
			false
		});
		Build.GFJLEEJELOL.blockData.Add(bd);
	}

	internal static GameObject FIaa_0024NpNdufR51oyzfpkj3A(int x, int y, int z)
	{
		GameObject result = null;
		foreach (GameObject item in Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<Build, List<GameObject>>(global::_003CModule_003E.smethod_29<string>(278329807u), sv6WiQ0SPlxR07h3vryVBDY))
		{
			BlockController component = item.GetComponent<BlockController>();
			if (!smethod_21((UnityEngine.Object)component, (UnityEngine.Object)null) || component.JNKEKNOAPHO.x != x || component.JNKEKNOAPHO.y != y || component.JNKEKNOAPHO.z != z)
			{
				continue;
			}
			result = item;
			break;
		}
		return result;
	}

	internal static BlockData pgjGYOaPlQ0OeYr8hfVzECU(int x, int y, int z)
	{
		foreach (BlockData blockDatum in Build.GFJLEEJELOL.blockData)
		{
			if (blockDatum.x != x || blockDatum.y != y || blockDatum.z != z)
			{
				continue;
			}
			return blockDatum;
		}
		return null;
	}

	private static bool SdmR5MvApM0UiXaOngPQlXs(int x, int y, int z, bool refresh = true)
	{
		if (refresh)
		{
			GqSH_0024GjFZ_wAxTK4jePCv5hrQ4zJEGQBZ87jE_jTtN8M(firstArg: true);
		}
		GameObject gameObject = FIaa_0024NpNdufR51oyzfpkj3A(x, y, z);
		if (smethod_5((UnityEngine.Object)gameObject, (UnityEngine.Object)null))
		{
			return false;
		}
		if (BlockData_0 != null && BlockData_0.x == x && BlockData_0.y == y && BlockData_0.z == z && (smethod_23((SceneMan)sv6WiQ0SPlxR07h3vryVBDY, global::_003CModule_003E.smethod_26<string>(2294466100u)) || smethod_23((SceneMan)sv6WiQ0SPlxR07h3vryVBDY, global::_003CModule_003E.smethod_26<string>(1130116533u)) || smethod_23((SceneMan)sv6WiQ0SPlxR07h3vryVBDY, global::_003CModule_003E.smethod_25<string>(4286679413u)) || smethod_23((SceneMan)sv6WiQ0SPlxR07h3vryVBDY, global::_003CModule_003E.smethod_26<string>(1450883971u)) || smethod_23((SceneMan)sv6WiQ0SPlxR07h3vryVBDY, global::_003CModule_003E.smethod_28<string>(1073463926u)) || smethod_23((SceneMan)sv6WiQ0SPlxR07h3vryVBDY, global::_003CModule_003E.smethod_27<string>(1058516166u)) || smethod_23((SceneMan)sv6WiQ0SPlxR07h3vryVBDY, global::_003CModule_003E.smethod_25<string>(2448247033u))))
		{
			MPatchr.ShowDebugMsg(global::_003CModule_003E.smethod_29<string>(1024797915u));
			smethod_24(global::_003CModule_003E.smethod_29<string>(4151327036u), 1f);
			smethod_25((SceneMan)sv6WiQ0SPlxR07h3vryVBDY, global::_003CModule_003E.smethod_26<string>(2294466100u), bool_0: false);
			smethod_25((SceneMan)sv6WiQ0SPlxR07h3vryVBDY, global::_003CModule_003E.smethod_29<string>(3481351691u), bool_0: false);
			smethod_25((SceneMan)sv6WiQ0SPlxR07h3vryVBDY, global::_003CModule_003E.smethod_28<string>(3000873796u), bool_0: false);
			smethod_25((SceneMan)sv6WiQ0SPlxR07h3vryVBDY, global::_003CModule_003E.smethod_28<string>(2727694093u), bool_0: false);
			smethod_25((SceneMan)sv6WiQ0SPlxR07h3vryVBDY, global::_003CModule_003E.smethod_29<string>(1566445408u), bool_0: false);
			smethod_25((SceneMan)sv6WiQ0SPlxR07h3vryVBDY, global::_003CModule_003E.smethod_27<string>(1058516166u), bool_0: false);
			smethod_25((SceneMan)sv6WiQ0SPlxR07h3vryVBDY, global::_003CModule_003E.smethod_27<string>(672294655u), bool_0: false);
		}
		Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<Build, List<GameObject>>(global::_003CModule_003E.smethod_29<string>(278329807u), sv6WiQ0SPlxR07h3vryVBDY).Remove(gameObject);
		Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<Build, List<Transform>>(global::_003CModule_003E.smethod_26<string>(3202564046u), sv6WiQ0SPlxR07h3vryVBDY).Remove(smethod_9(gameObject));
		BlockController component = gameObject.GetComponent<BlockController>();
		smethod_26(Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<Build, HDBLLPODNLN>(global::_003CModule_003E.smethod_29<string>(3123545550u), sv6WiQ0SPlxR07h3vryVBDY), component);
		Build.GFJLEEJELOL.blockData.Remove(component.JNKEKNOAPHO);
		Class17.DQGZpGELKqeKmXweFYGEcaXR73CqGywHQI6u2TATGL80(global::_003CModule_003E.smethod_25<string>(1456679377u), sv6WiQ0SPlxR07h3vryVBDY, 9, component);
		smethod_18((UnityEngine.Object)gameObject);
		return true;
	}

	internal static void z3n2bV4jTuYqWHBSISaM9vo(int chunk = -1)
	{
		BlockData[] array = Build.GFJLEEJELOL.blockData.ToArray();
		foreach (BlockData blockData in array)
		{
			if (chunk == -1 || blockData.y == chunk)
			{
				SdmR5MvApM0UiXaOngPQlXs(blockData.x, blockData.y, blockData.z, refresh: false);
			}
		}
		GqSH_0024GjFZ_wAxTK4jePCv5hrQ4zJEGQBZ87jE_jTtN8M();
	}

	internal static void smethod_1(int a, int b, int c, int x, int y, int z)
	{
		if (x == -1)
		{
			Class17.DMoImhPC2jNUt2tgmSezeT_IK_0024XK5Df9PmexG_CaKb1R<Build, GameObject>(global::_003CModule_003E.smethod_25<string>(4135666116u), sv6WiQ0SPlxR07h3vryVBDY, null);
		}
		else
		{
			Class17.DMoImhPC2jNUt2tgmSezeT_IK_0024XK5Df9PmexG_CaKb1R(global::_003CModule_003E.smethod_25<string>(4135666116u), sv6WiQ0SPlxR07h3vryVBDY, FIaa_0024NpNdufR51oyzfpkj3A(x, y, z));
			Class17.DMoImhPC2jNUt2tgmSezeT_IK_0024XK5Df9PmexG_CaKb1R(global::_003CModule_003E.smethod_29<string>(829853863u), sv6WiQ0SPlxR07h3vryVBDY, pgjGYOaPlQ0OeYr8hfVzECU(x, y, z));
		}
		Class17.DQGZpGELKqeKmXweFYGEcaXR73CqGywHQI6u2TATGL80(global::_003CModule_003E.smethod_25<string>(1984213661u), sv6WiQ0SPlxR07h3vryVBDY, a, b, c);
	}

	internal static void smethod_2(JVDvlJAU_UpBfFlkSS4dz_00248kv5abfDJ2__002485ml_QUV8f_QRNWaHa5H5V3A4r06LroP9HTbhPsjhXYZvZn3LwqA4ocLFaSz_CXXamUCpUMYfS action, bool ud = false, bool fb = false, bool isEven = false)
	{
		switch (action)
		{
		case JVDvlJAU_UpBfFlkSS4dz_00248kv5abfDJ2__002485ml_QUV8f_QRNWaHa5H5V3A4r06LroP9HTbhPsjhXYZvZn3LwqA4ocLFaSz_CXXamUCpUMYfS.FLIP:
			smethod_28(smethod_27((SceneMan)sv6WiQ0SPlxR07h3vryVBDY, global::_003CModule_003E.smethod_27<string>(760753663u)).GetComponent<Toggle>(), ud);
			smethod_28(smethod_27((SceneMan)sv6WiQ0SPlxR07h3vryVBDY, global::_003CModule_003E.smethod_27<string>(4283277937u)).GetComponent<Toggle>(), fb);
			Build.GFJLEEJELOL.isEven = isEven;
			Class17.DQGZpGELKqeKmXweFYGEcaXR73CqGywHQI6u2TATGL80(global::_003CModule_003E.smethod_28<string>(2621334036u), sv6WiQ0SPlxR07h3vryVBDY);
			break;
		case JVDvlJAU_UpBfFlkSS4dz_00248kv5abfDJ2__002485ml_QUV8f_QRNWaHa5H5V3A4r06LroP9HTbhPsjhXYZvZn3LwqA4ocLFaSz_CXXamUCpUMYfS.MIRROR:
			Class17.DQGZpGELKqeKmXweFYGEcaXR73CqGywHQI6u2TATGL80(global::_003CModule_003E.smethod_28<string>(1513463275u), sv6WiQ0SPlxR07h3vryVBDY, false);
			break;
		case JVDvlJAU_UpBfFlkSS4dz_00248kv5abfDJ2__002485ml_QUV8f_QRNWaHa5H5V3A4r06LroP9HTbhPsjhXYZvZn3LwqA4ocLFaSz_CXXamUCpUMYfS.TURNL:
		{
			Class17.DQGZpGELKqeKmXweFYGEcaXR73CqGywHQI6u2TATGL80(global::_003CModule_003E.smethod_28<string>(132412811u), sv6WiQ0SPlxR07h3vryVBDY, false, false);
			for (int num2 = Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<Build, List<GameObject>>(global::_003CModule_003E.smethod_28<string>(344836511u), sv6WiQ0SPlxR07h3vryVBDY).Count - 1; num2 >= 0; num2--)
			{
				smethod_29(Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<Build, List<GameObject>>(global::_003CModule_003E.smethod_27<string>(621161638u), sv6WiQ0SPlxR07h3vryVBDY)[num2].GetComponent<BlockController>().JNKEKNOAPHO);
			}
			Class17.DQGZpGELKqeKmXweFYGEcaXR73CqGywHQI6u2TATGL80(global::_003CModule_003E.smethod_28<string>(2257094432u), sv6WiQ0SPlxR07h3vryVBDY);
			Class17.DQGZpGELKqeKmXweFYGEcaXR73CqGywHQI6u2TATGL80(global::_003CModule_003E.smethod_27<string>(4190216587u), sv6WiQ0SPlxR07h3vryVBDY);
			break;
		}
		case JVDvlJAU_UpBfFlkSS4dz_00248kv5abfDJ2__002485ml_QUV8f_QRNWaHa5H5V3A4r06LroP9HTbhPsjhXYZvZn3LwqA4ocLFaSz_CXXamUCpUMYfS.TURNR:
		{
			Class17.DQGZpGELKqeKmXweFYGEcaXR73CqGywHQI6u2TATGL80(global::_003CModule_003E.smethod_29<string>(1760163323u), sv6WiQ0SPlxR07h3vryVBDY, false, false);
			for (int num = Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<Build, List<GameObject>>(global::_003CModule_003E.smethod_25<string>(2025528980u), sv6WiQ0SPlxR07h3vryVBDY).Count - 1; num >= 0; num--)
			{
				smethod_30(Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<Build, List<GameObject>>(global::_003CModule_003E.smethod_26<string>(4004482641u), sv6WiQ0SPlxR07h3vryVBDY)[num].GetComponent<BlockController>().JNKEKNOAPHO);
			}
			Class17.DQGZpGELKqeKmXweFYGEcaXR73CqGywHQI6u2TATGL80(global::_003CModule_003E.smethod_26<string>(469770406u), sv6WiQ0SPlxR07h3vryVBDY);
			Class17.DQGZpGELKqeKmXweFYGEcaXR73CqGywHQI6u2TATGL80(global::_003CModule_003E.smethod_29<string>(3307386902u), sv6WiQ0SPlxR07h3vryVBDY);
			break;
		}
		}
	}

	internal static void ZTLLJrKNkB7rW1DQ_0024MTI83E(JVDvlJAU_UpBfFlkSS4dz_00248kv5abfDJ2__002485ml_QUV8f_QRNWaHa5H5V3A4r06LroP9HTbhPsjhXYZvZn3LwqA4ocLFaSz_CXXamUCpUMYfS a, bool ud = false, bool fb = false, bool isEven = false)
	{
		smethod_31(_mLDDjXswSCGaR35tynAvAg.bkEYtCXOGBC5a3sGZc1baFBEZsix9sHaNV7UHBoeB8aKxFK0bpuW1JKVsL3eaRtFkQ_0.nGT7VUZTN5zsOGBEDOWNDC4, global::_003CModule_003E.smethod_29<string>(4053855010u), BFDCHLBGJHF.Others, new object[4] { a, ud, fb, isEven });
	}

	internal static void ZTLLJrKNkB7rW1DQ_0024MTI83E(int a, int b, int c)
	{
		int num = -1;
		int num2 = -1;
		int num3 = -1;
		if (BlockData_0 != null)
		{
			num = BlockData_0.x;
			num2 = BlockData_0.y;
			num3 = BlockData_0.z;
		}
		smethod_31(_mLDDjXswSCGaR35tynAvAg.bkEYtCXOGBC5a3sGZc1baFBEZsix9sHaNV7UHBoeB8aKxFK0bpuW1JKVsL3eaRtFkQ_0.nGT7VUZTN5zsOGBEDOWNDC4, global::_003CModule_003E.smethod_25<string>(2511747945u), BFDCHLBGJHF.Others, new object[6] { a, b, c, num, num2, num3 });
	}

	internal static IEnumerator FGAOESvRy6bCNgeQ0J8Oo0E(OPLNFKECCLE player, int[] chunks = null)
	{
		bool flag = true;
		List<BlockData> list;
		if (chunks == null)
		{
			yield return cQRmTGfalxbXpc502C_8jfAHU_0024tvTKODxuwVWhdlaC8kN3_WuxqmFuOptQs_0024tTBVYWaeV9hatXrIHMDZLnF58Ry2EQAfXDtizpLllL4kzmK5ZHUWUMhcQ3OmMQNyJ6_iZg.smethod_0(1f);
			list = Build.GFJLEEJELOL.blockData;
		}
		else
		{
			list = new List<BlockData>();
			foreach (BlockData blockDatum in Build.GFJLEEJELOL.blockData)
			{
				if (chunks.Contains(blockDatum.y))
				{
					list.Add(blockDatum);
				}
			}
			flag = false;
		}
		MemoryStream stream_ = cQRmTGfalxbXpc502C_8jfAHU_0024tvTKODxuwVWhdlaC8kN3_WuxqmFuOptQs_0024tTBVYWaeV9hatXrIHMDZLnF58Ry2EQAfXDtizpLllL4kzmK5ZHUWUMhcQ3OmMQNyJ6_iZg.smethod_1();
		int num = 0;
		foreach (BlockData item in list)
		{
			item.EB00nJF8bfT7ocPR4HRIIqc(stream_);
			num++;
			if (cQRmTGfalxbXpc502C_8jfAHU_0024tvTKODxuwVWhdlaC8kN3_WuxqmFuOptQs_0024tTBVYWaeV9hatXrIHMDZLnF58Ry2EQAfXDtizpLllL4kzmK5ZHUWUMhcQ3OmMQNyJ6_iZg.smethod_2((Stream)stream_) > 500L)
			{
				cQRmTGfalxbXpc502C_8jfAHU_0024tvTKODxuwVWhdlaC8kN3_WuxqmFuOptQs_0024tTBVYWaeV9hatXrIHMDZLnF58Ry2EQAfXDtizpLllL4kzmK5ZHUWUMhcQ3OmMQNyJ6_iZg.smethod_3((Stream)stream_, 0L, SeekOrigin.Begin);
				byte[] byte_ = H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.lD8ilO2D57aRcy0P8Iq13zE(stream_);
				cQRmTGfalxbXpc502C_8jfAHU_0024tvTKODxuwVWhdlaC8kN3_WuxqmFuOptQs_0024tTBVYWaeV9hatXrIHMDZLnF58Ry2EQAfXDtizpLllL4kzmK5ZHUWUMhcQ3OmMQNyJ6_iZg.smethod_4(_mLDDjXswSCGaR35tynAvAg.bkEYtCXOGBC5a3sGZc1baFBEZsix9sHaNV7UHBoeB8aKxFK0bpuW1JKVsL3eaRtFkQ_0.nGT7VUZTN5zsOGBEDOWNDC4, global::_003CModule_003E.smethod_28<string>(2909665688u), player, new object[4]
				{
					num,
					Y0682_EmDASRYkXiQKRSXHE.Hnc3QYGDRf1W8TRMMLwwZ48(byte_, int_0),
					flag,
					list.Count
				});
				if (flag)
				{
					flag = false;
				}
				cQRmTGfalxbXpc502C_8jfAHU_0024tvTKODxuwVWhdlaC8kN3_WuxqmFuOptQs_0024tTBVYWaeV9hatXrIHMDZLnF58Ry2EQAfXDtizpLllL4kzmK5ZHUWUMhcQ3OmMQNyJ6_iZg.smethod_5((Stream)stream_);
				num = 0;
				stream_ = cQRmTGfalxbXpc502C_8jfAHU_0024tvTKODxuwVWhdlaC8kN3_WuxqmFuOptQs_0024tTBVYWaeV9hatXrIHMDZLnF58Ry2EQAfXDtizpLllL4kzmK5ZHUWUMhcQ3OmMQNyJ6_iZg.smethod_1();
				yield return cQRmTGfalxbXpc502C_8jfAHU_0024tvTKODxuwVWhdlaC8kN3_WuxqmFuOptQs_0024tTBVYWaeV9hatXrIHMDZLnF58Ry2EQAfXDtizpLllL4kzmK5ZHUWUMhcQ3OmMQNyJ6_iZg.smethod_0(0.01f);
			}
		}
		if (cQRmTGfalxbXpc502C_8jfAHU_0024tvTKODxuwVWhdlaC8kN3_WuxqmFuOptQs_0024tTBVYWaeV9hatXrIHMDZLnF58Ry2EQAfXDtizpLllL4kzmK5ZHUWUMhcQ3OmMQNyJ6_iZg.smethod_2((Stream)stream_) > 0L)
		{
			cQRmTGfalxbXpc502C_8jfAHU_0024tvTKODxuwVWhdlaC8kN3_WuxqmFuOptQs_0024tTBVYWaeV9hatXrIHMDZLnF58Ry2EQAfXDtizpLllL4kzmK5ZHUWUMhcQ3OmMQNyJ6_iZg.smethod_3((Stream)stream_, 0L, SeekOrigin.Begin);
			byte[] byte_2 = H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.lD8ilO2D57aRcy0P8Iq13zE(stream_);
			cQRmTGfalxbXpc502C_8jfAHU_0024tvTKODxuwVWhdlaC8kN3_WuxqmFuOptQs_0024tTBVYWaeV9hatXrIHMDZLnF58Ry2EQAfXDtizpLllL4kzmK5ZHUWUMhcQ3OmMQNyJ6_iZg.smethod_4(_mLDDjXswSCGaR35tynAvAg.bkEYtCXOGBC5a3sGZc1baFBEZsix9sHaNV7UHBoeB8aKxFK0bpuW1JKVsL3eaRtFkQ_0.nGT7VUZTN5zsOGBEDOWNDC4, global::_003CModule_003E.smethod_25<string>(4259612073u), player, new object[4]
			{
				num,
				Y0682_EmDASRYkXiQKRSXHE.Hnc3QYGDRf1W8TRMMLwwZ48(byte_2, int_0),
				flag,
				list.Count
			});
			cQRmTGfalxbXpc502C_8jfAHU_0024tvTKODxuwVWhdlaC8kN3_WuxqmFuOptQs_0024tTBVYWaeV9hatXrIHMDZLnF58Ry2EQAfXDtizpLllL4kzmK5ZHUWUMhcQ3OmMQNyJ6_iZg.smethod_5((Stream)stream_);
		}
	}

	internal static void V_NGBHwDrkTOfihwsi3eOOBhNumbeCNe_jj52dboz3u2()
	{
		if (smethod_32().name == global::_003CModule_003E.smethod_27<string>(3514760917u) && fYYx8vN1tah7ZgfygXMH9eJU0YzjaUsqtv9tADffziMcu1k1It8P_0024ZciTdZuxN09bA.PafCCA77BFEJYaAdgSJv7Ys && _mLDDjXswSCGaR35tynAvAg != null)
		{
			_mLDDjXswSCGaR35tynAvAg.StartCoroutine(mBUGiiYzkcmUy7PdhBgqAxs());
		}
	}

	internal static IEnumerator mBUGiiYzkcmUy7PdhBgqAxs()
	{
		if (e6LNe0Mxpa_w7jYjEVFEsoY)
		{
			AzzZaKp3Ff0jxSmcOyfPmWY = true;
			yield break;
		}
		e6LNe0Mxpa_w7jYjEVFEsoY = true;
		List<BlockData> list = new List<BlockData>();
		foreach (BlockData blockDatum in Build.GFJLEEJELOL.blockData)
		{
			list.Add(L2EFyjYaXZOKMMw0KwyZO35Iv71zpoGED4WWXsO9Kgff0nuCHlvMjDRxtyej9TCtqoSD3Hd7YJ4UdirS0mlQiw2YryuG4YYQo7CoXIL4uLDy.smethod_0(blockDatum));
		}
		yield return L2EFyjYaXZOKMMw0KwyZO35Iv71zpoGED4WWXsO9Kgff0nuCHlvMjDRxtyej9TCtqoSD3Hd7YJ4UdirS0mlQiw2YryuG4YYQo7CoXIL4uLDy.smethod_1();
		yield return L2EFyjYaXZOKMMw0KwyZO35Iv71zpoGED4WWXsO9Kgff0nuCHlvMjDRxtyej9TCtqoSD3Hd7YJ4UdirS0mlQiw2YryuG4YYQo7CoXIL4uLDy.smethod_1();
		List<BlockData> list2 = new List<BlockData>();
		List<BlockData> list3 = new List<BlockData>();
		int num = 0;
		foreach (BlockData item in list)
		{
			if (!Build.GFJLEEJELOL.blockData._1q8sDnsMlmqfRgkKEi_0024uDs(item))
			{
				list2.Add(item);
			}
			num++;
			if (num > int_1)
			{
				yield return null;
				num = 0;
			}
		}
		num = 0;
		foreach (BlockData blockDatum2 in Build.GFJLEEJELOL.blockData)
		{
			if (!list._1q8sDnsMlmqfRgkKEi_0024uDs(blockDatum2))
			{
				list3.Add(blockDatum2);
			}
			num++;
			if (num > int_1)
			{
				yield return null;
				num = 0;
			}
		}
		if (list3.Count == 0 && list2.Count == 0)
		{
			e6LNe0Mxpa_w7jYjEVFEsoY = false;
			if (AzzZaKp3Ff0jxSmcOyfPmWY)
			{
				AzzZaKp3Ff0jxSmcOyfPmWY = false;
				L2EFyjYaXZOKMMw0KwyZO35Iv71zpoGED4WWXsO9Kgff0nuCHlvMjDRxtyej9TCtqoSD3Hd7YJ4UdirS0mlQiw2YryuG4YYQo7CoXIL4uLDy.smethod_2((MonoBehaviour)_mLDDjXswSCGaR35tynAvAg, mBUGiiYzkcmUy7PdhBgqAxs());
			}
			yield break;
		}
		MemoryStream stream_ = L2EFyjYaXZOKMMw0KwyZO35Iv71zpoGED4WWXsO9Kgff0nuCHlvMjDRxtyej9TCtqoSD3Hd7YJ4UdirS0mlQiw2YryuG4YYQo7CoXIL4uLDy.smethod_3();
		foreach (BlockData item2 in list3)
		{
			item2.EB00nJF8bfT7ocPR4HRIIqc(stream_);
		}
		L2EFyjYaXZOKMMw0KwyZO35Iv71zpoGED4WWXsO9Kgff0nuCHlvMjDRxtyej9TCtqoSD3Hd7YJ4UdirS0mlQiw2YryuG4YYQo7CoXIL4uLDy.smethod_4((Stream)stream_, 0L, SeekOrigin.Begin);
		byte[] byte_ = H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.lD8ilO2D57aRcy0P8Iq13zE(stream_);
		List<Vector3> list4 = new List<Vector3>();
		foreach (BlockData item3 in list2)
		{
			list4.Add(new Vector3(item3.x, item3.y, item3.z));
		}
		_mLDDjXswSCGaR35tynAvAg.bkEYtCXOGBC5a3sGZc1baFBEZsix9sHaNV7UHBoeB8aKxFK0bpuW1JKVsL3eaRtFkQ_0.nGT7VUZTN5zsOGBEDOWNDC4.RPC(global::_003CModule_003E.smethod_25<string>(3535066057u), BFDCHLBGJHF.Others, list4.ToArray(), Y0682_EmDASRYkXiQKRSXHE.Hnc3QYGDRf1W8TRMMLwwZ48(byte_, int_0), list3.Count);
		e6LNe0Mxpa_w7jYjEVFEsoY = false;
		if (AzzZaKp3Ff0jxSmcOyfPmWY)
		{
			AzzZaKp3Ff0jxSmcOyfPmWY = false;
			_mLDDjXswSCGaR35tynAvAg.StartCoroutine(mBUGiiYzkcmUy7PdhBgqAxs());
		}
	}

	internal static void K_0024vpUUzAbQGz74d7UWL6CDA(BlockData bd)
	{
		if (smethod_21((UnityEngine.Object)_mLDDjXswSCGaR35tynAvAg, (UnityEngine.Object)null) && fYYx8vN1tah7ZgfygXMH9eJU0YzjaUsqtv9tADffziMcu1k1It8P_0024ZciTdZuxN09bA.PafCCA77BFEJYaAdgSJv7Ys)
		{
			c_0024r_0024ac_0024K3A6JmEGgPtvP4htM8Y7M_0024B3aNiM0oPQIJMKP();
			Vector3 vector = smethod_33(smethod_9(Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<Build, GameObject>(global::_003CModule_003E.smethod_29<string>(267227107u), sv6WiQ0SPlxR07h3vryVBDY)));
			if (bd.type == BlockData.AAHMDBHDCDK.BoxGen && bd.actionID[0] != 0 && vector == Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<Build, Vector3>(global::_003CModule_003E.smethod_25<string>(450863742u), sv6WiQ0SPlxR07h3vryVBDY))
			{
				smethod_34(bd, bd.actionID[0] & 0xFF);
			}
			smethod_31(_mLDDjXswSCGaR35tynAvAg.bkEYtCXOGBC5a3sGZc1baFBEZsix9sHaNV7UHBoeB8aKxFK0bpuW1JKVsL3eaRtFkQ_0.nGT7VUZTN5zsOGBEDOWNDC4, global::_003CModule_003E.smethod_28<string>(3805112749u), BFDCHLBGJHF.Others, new object[1] { Y0682_EmDASRYkXiQKRSXHE.Hnc3QYGDRf1W8TRMMLwwZ48(bd.EB00nJF8bfT7ocPR4HRIIqc(), int_0) });
		}
	}

	internal static void smethod_3(BlockData bd)
	{
		if (smethod_21((UnityEngine.Object)_mLDDjXswSCGaR35tynAvAg, (UnityEngine.Object)null) && fYYx8vN1tah7ZgfygXMH9eJU0YzjaUsqtv9tADffziMcu1k1It8P_0024ZciTdZuxN09bA.PafCCA77BFEJYaAdgSJv7Ys)
		{
			_mLDDjXswSCGaR35tynAvAg.bkEYtCXOGBC5a3sGZc1baFBEZsix9sHaNV7UHBoeB8aKxFK0bpuW1JKVsL3eaRtFkQ_0.nGT7VUZTN5zsOGBEDOWNDC4.RPC(global::_003CModule_003E.smethod_25<string>(1415364058u), BFDCHLBGJHF.Others, new Vector3[1]
			{
				new Vector3(bd.x, bd.y, bd.z)
			});
		}
	}

	internal static void smethod_4(BlockData[] bds)
	{
		if (!smethod_5((UnityEngine.Object)_mLDDjXswSCGaR35tynAvAg, (UnityEngine.Object)null) && fYYx8vN1tah7ZgfygXMH9eJU0YzjaUsqtv9tADffziMcu1k1It8P_0024ZciTdZuxN09bA.PafCCA77BFEJYaAdgSJv7Ys)
		{
			List<Vector3> list = new List<Vector3>();
			foreach (BlockData blockData in bds)
			{
				list.Add(new Vector3(blockData.x, blockData.y, blockData.z));
			}
			_mLDDjXswSCGaR35tynAvAg.bkEYtCXOGBC5a3sGZc1baFBEZsix9sHaNV7UHBoeB8aKxFK0bpuW1JKVsL3eaRtFkQ_0.nGT7VUZTN5zsOGBEDOWNDC4.RPC(global::_003CModule_003E.smethod_26<string>(3962819107u), BFDCHLBGJHF.Others, list.ToArray());
		}
	}

	internal static int[] RA6UFk5RROWymTy4UxPCTnKoscj9apr2_00245y1UMlZ58OG(long[] hashesA, long[] hashesB)
	{
		List<int> list = new List<int>();
		for (int i = 0; i < 99; i++)
		{
			if (hashesA[i] != hashesB[i])
			{
				list.Add(i);
			}
		}
		return list.ToArray();
	}

	internal static void XQmWSqHGRQJe0OTPktLpGG0(lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw btn)
	{
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(smethod_35(global::_003CModule_003E.smethod_29<string>(1531911171u), JONBPAFNPBD.APFEPHDDNFM.Protocol.ToString()));
		JONBPAFNPBD.APFEPHDDNFM.Protocol = ConnectionProtocol.Tcp;
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(smethod_35(global::_003CModule_003E.smethod_26<string>(3616970001u), JONBPAFNPBD.APFEPHDDNFM.Protocol.ToString()));
		string string_ = smethod_36(global::_003CModule_003E.smethod_27<string>(94671762u), global::_003CModule_003E.smethod_27<string>(2775210629u));
		if (smethod_37(string_) && smethod_5((UnityEngine.Object)eMeHtB0nEBTQAy_Ed4AHx1M, (UnityEngine.Object)null))
		{
			eMeHtB0nEBTQAy_Ed4AHx1M = smethod_38(string_);
		}
		if (smethod_5((UnityEngine.Object)_mLDDjXswSCGaR35tynAvAg, (UnityEngine.Object)null))
		{
			_mLDDjXswSCGaR35tynAvAg = smethod_39(global::_003CModule_003E.smethod_28<string>(633019956u)).AddComponent<Class30>();
		}
		if (fYYx8vN1tah7ZgfygXMH9eJU0YzjaUsqtv9tADffziMcu1k1It8P_0024ZciTdZuxN09bA.PafCCA77BFEJYaAdgSJv7Ys || _mLDDjXswSCGaR35tynAvAg.hn0l0GNFrsPTtJLnYPdC5kufoQdroygW0c_0024coK_0024bqiHUyUKuNGxxEWa6iWJgwKxAk5py8zqyEY7YQFC_01oXiVU_0 != Class30.Hn0l0GNFrsPTtJLnYPdC5kufoQdroygW0c_0024coK_0024bqiHUyUKuNGxxEWa6iWJgwKxAk5py8zqyEY7YQFC_01oXiVU.disconnected)
		{
			if (fYYx8vN1tah7ZgfygXMH9eJU0YzjaUsqtv9tADffziMcu1k1It8P_0024ZciTdZuxN09bA.PafCCA77BFEJYaAdgSJv7Ys)
			{
				switch (_mLDDjXswSCGaR35tynAvAg.hn0l0GNFrsPTtJLnYPdC5kufoQdroygW0c_0024coK_0024bqiHUyUKuNGxxEWa6iWJgwKxAk5py8zqyEY7YQFC_01oXiVU_0)
				{
				case Class30.Hn0l0GNFrsPTtJLnYPdC5kufoQdroygW0c_0024coK_0024bqiHUyUKuNGxxEWa6iWJgwKxAk5py8zqyEY7YQFC_01oXiVU.hostingRoom:
				case Class30.Hn0l0GNFrsPTtJLnYPdC5kufoQdroygW0c_0024coK_0024bqiHUyUKuNGxxEWa6iWJgwKxAk5py8zqyEY7YQFC_01oXiVU.connectedToRoom:
					Class17.DQGZpGELKqeKmXweFYGEcaXR73CqGywHQI6u2TATGL80(global::_003CModule_003E.smethod_27<string>(3441123445u), sv6WiQ0SPlxR07h3vryVBDY);
					break;
				case Class30.Hn0l0GNFrsPTtJLnYPdC5kufoQdroygW0c_0024coK_0024bqiHUyUKuNGxxEWa6iWJgwKxAk5py8zqyEY7YQFC_01oXiVU.viewingRooms:
					_mLDDjXswSCGaR35tynAvAg.AA0Uc_IZxCzkuW6s0ItCfaA();
					break;
				}
			}
		}
		else
		{
			_mLDDjXswSCGaR35tynAvAg.method_0();
			btn.FLSdXom6uNTfN55f5nxTsH8 = false;
		}
	}

	internal static void b6vtu1ZHdIgbkU4vG5_COJ4(lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw btn)
	{
		if (smethod_21((UnityEngine.Object)_mLDDjXswSCGaR35tynAvAg, (UnityEngine.Object)null) && fYYx8vN1tah7ZgfygXMH9eJU0YzjaUsqtv9tADffziMcu1k1It8P_0024ZciTdZuxN09bA.PafCCA77BFEJYaAdgSJv7Ys && !smethod_40(iOXrQ3ou11UqRX_0024NnkXQnmI.pZEKY5TzLd4S3z2lXESoRnw, global::_003CModule_003E.smethod_27<string>(1370423865u)) && !smethod_40(iOXrQ3ou11UqRX_0024NnkXQnmI.pZEKY5TzLd4S3z2lXESoRnw, global::_003CModule_003E.smethod_26<string>(3415898919u)) && !smethod_40(iOXrQ3ou11UqRX_0024NnkXQnmI.pZEKY5TzLd4S3z2lXESoRnw, global::_003CModule_003E.smethod_29<string>(214030947u)))
		{
			btn.FLSdXom6uNTfN55f5nxTsH8 = !_mLDDjXswSCGaR35tynAvAg.uctMOjOKl0cnSRlnCCJ08CI(5, iOXrQ3ou11UqRX_0024NnkXQnmI.pZEKY5TzLd4S3z2lXESoRnw, smethod_41(KtAkTPvF6e8D9Ss2KoAUKAQ.pZEKY5TzLd4S3z2lXESoRnw, string.Empty), KtAkTPvF6e8D9Ss2KoAUKAQ.pZEKY5TzLd4S3z2lXESoRnw);
		}
	}

	internal static void shmFoyxCMRKaGuRRXQ2TnMI(lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw btn)
	{
		if (fYYx8vN1tah7ZgfygXMH9eJU0YzjaUsqtv9tADffziMcu1k1It8P_0024ZciTdZuxN09bA.PafCCA77BFEJYaAdgSJv7Ys && !smethod_42())
		{
			_mLDDjXswSCGaR35tynAvAg.pLE8WTgZE0t0Om1V6BWOGpQ = 0;
			smethod_43(_mLDDjXswSCGaR35tynAvAg.bkEYtCXOGBC5a3sGZc1baFBEZsix9sHaNV7UHBoeB8aKxFK0bpuW1JKVsL3eaRtFkQ_0.nGT7VUZTN5zsOGBEDOWNDC4, global::_003CModule_003E.smethod_29<string>(3630658806u), fYYx8vN1tah7ZgfygXMH9eJU0YzjaUsqtv9tADffziMcu1k1It8P_0024ZciTdZuxN09bA.cRsccKpNy9maPWfSh0XAIqg, new object[0]);
		}
	}

	internal static BlockData OWtbIufJWOmFnUwp2L_jUuo(bool flag = false, int gid = 0, int index = 255, int press = 0, int rgbI = 12632256, BlockData.AAHMDBHDCDK type = BlockData.AAHMDBHDCDK.Chassis, int x = 0, int y = 0, int z = 0)
	{
		BlockData blockData = smethod_44();
		blockData.flag = flag;
		blockData.gid = gid;
		blockData.index = index;
		blockData.press = press;
		blockData.rgbI = rgbI;
		blockData.type = type;
		blockData.x = x;
		blockData.y = y;
		blockData.z = z;
		return blockData;
	}

	internal static bool smethod_5(UnityEngine.Object object_0, UnityEngine.Object object_1)
	{
		return object_0 == object_1;
	}

	internal static object smethod_6(FieldInfo fieldInfo_0, object object_0)
	{
		return fieldInfo_0.GetValue(object_0);
	}

	internal static void smethod_7(FieldInfo fieldInfo_0, object object_0, object object_1)
	{
		fieldInfo_0.SetValue(object_0, object_1);
	}

	internal static object smethod_8(MethodBase methodBase_0, object object_0, object[] object_1)
	{
		return methodBase_0.Invoke(object_0, object_1);
	}

	internal static Transform smethod_9(GameObject gameObject_0)
	{
		return gameObject_0.transform;
	}

	internal static Type smethod_10(RuntimeTypeHandle runtimeTypeHandle_0)
	{
		return Type.GetTypeFromHandle(runtimeTypeHandle_0);
	}

	internal static FieldInfo smethod_11(Type type_0, string string_0)
	{
		return AccessTools.Field(type_0, string_0);
	}

	internal static MethodInfo smethod_12(Type type_0, string string_0, Type[] type_1, Type[] type_2)
	{
		return AccessTools.Method(type_0, string_0, type_1, type_2);
	}

	internal static void smethod_13(int int_2)
	{
		QualitySettings.vSyncCount = int_2;
	}

	internal static void smethod_14(CursorLockMode cursorLockMode_0)
	{
		Cursor.lockState = cursorLockMode_0;
	}

	internal static GameObject smethod_15(string string_0)
	{
		return GameObject.Find(string_0);
	}

	internal static bool smethod_16(UnityEngine.Object object_0)
	{
		return object_0;
	}

	internal static void smethod_17(Behaviour behaviour_0, bool bool_0)
	{
		behaviour_0.enabled = bool_0;
	}

	internal static void smethod_18(UnityEngine.Object object_0)
	{
		UnityEngine.Object.Destroy(object_0);
	}

	internal static Process smethod_19()
	{
		return Process.GetCurrentProcess();
	}

	internal static void smethod_20(Process process_0, ProcessPriorityClass processPriorityClass_0)
	{
		process_0.PriorityClass = processPriorityClass_0;
	}

	internal static bool smethod_21(UnityEngine.Object object_0, UnityEngine.Object object_1)
	{
		return object_0 != object_1;
	}

	internal static GameObject smethod_22(BlockData blockData_0, bool bool_0)
	{
		return PAEHEMJNPND.PKLHNJNFKFH(blockData_0, bool_0);
	}

	internal static bool smethod_23(SceneMan sceneMan_0, string string_0)
	{
		return sceneMan_0.CheckPNL(string_0);
	}

	internal static void smethod_24(string string_0, float float_0)
	{
		KEFHJCGICLE.HNAHBIMJDCB(string_0, float_0);
	}

	internal static void smethod_25(SceneMan sceneMan_0, string string_0, bool bool_0)
	{
		sceneMan_0.ValidatePNL(string_0, bool_0);
	}

	internal static void smethod_26(HDBLLPODNLN hdbllpodnln_0, BlockController blockController_0)
	{
		hdbllpodnln_0.PJDKNPIDAIA(blockController_0);
	}

	internal static GameObject smethod_27(SceneMan sceneMan_0, string string_0)
	{
		return sceneMan_0.GetTGL(string_0);
	}

	internal static void smethod_28(Toggle toggle_0, bool bool_0)
	{
		toggle_0.isOn = bool_0;
	}

	internal static void smethod_29(BlockData blockData_0)
	{
		blockData_0.RotPY();
	}

	internal static void smethod_30(BlockData blockData_0)
	{
		blockData_0.RotNY();
	}

	internal static void smethod_31(PhotonView photonView_0, string string_0, BFDCHLBGJHF bfdchlbgjhf_0, object[] object_0)
	{
		photonView_0.RPC(string_0, bfdchlbgjhf_0, object_0);
	}

	internal static Scene smethod_32()
	{
		return SceneManager.GetActiveScene();
	}

	internal static Vector3 smethod_33(Transform transform_0)
	{
		return transform_0.up;
	}

	internal static int smethod_34(BlockData blockData_0, int int_2)
	{
		return blockData_0.GetChassisIndex(int_2);
	}

	internal static string smethod_35(string string_0, string string_1)
	{
		return string_0 + string_1;
	}

	internal static string smethod_36(string string_0, string string_1)
	{
		return Path.Combine(string_0, string_1);
	}

	internal static bool smethod_37(string string_0)
	{
		return File.Exists(string_0);
	}

	internal static AssetBundle smethod_38(string string_0)
	{
		return AssetBundle.LoadFromFile(string_0);
	}

	internal static GameObject smethod_39(string string_0)
	{
		return new GameObject(string_0);
	}

	internal static bool smethod_40(string string_0, string string_1)
	{
		return string_0.Contains(string_1);
	}

	internal static bool smethod_41(string string_0, string string_1)
	{
		return string_0 != string_1;
	}

	internal static bool smethod_42()
	{
		return JONBPAFNPBD.JNLBBLEEPBJ;
	}

	internal static void smethod_43(PhotonView photonView_0, string string_0, OPLNFKECCLE oplnfkeccle_0, object[] object_0)
	{
		photonView_0.RPC(string_0, oplnfkeccle_0, object_0);
	}

	internal static BlockData smethod_44()
	{
		return new BlockData();
	}
}
