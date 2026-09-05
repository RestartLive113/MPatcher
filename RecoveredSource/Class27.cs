using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using HarmonyLib;
using MPatchrMain;
using UnityEngine;

[HarmonyPatch(typeof(HIPBCCKFFAG))]
[HarmonyPatch("ACMGPBMMKNI")]
internal class Class27
{
	[CompilerGenerated]
	private sealed class _0024iZGQyDxLT4VIvWohGlnNVsELgW4XVz2ciE8bE7kdx1kUfj95VGLpEo7v0y_0024ce8Ebyd4Ks5D9VgsNugDhYKZjXMhVr_0024Ob9_7Melhr0HCLCuQb9E9gjnMK6yb4EYzjO7zw2IU9G80ZuaCPojstssHjf4 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int SjlBM8inVA_YE4YVlr_0024gluY;

		private object yT7HpVIzmqW54W307WgJtr4;

		public HIPBCCKFFAG mcK1TWle2rrKoYzxrO_0024wMYQ;

		public bool EBuL2cFtbUzvmThrQfXEmXE;

		public BuildData qwuGs2uZZkvjLMEHDhpc6og;

		public List<GameObject> NR6o8kJWEmRvjePi6tHtEEs;

		public List<int> yPgQFjS68o1MK_0024lmACub20Q;

		public List<BlockData> moAdYEkdD59OffBEobdsjlI;

		public bool U3JVnvAEQIewfxdg8MwdqjQ;

		public int sPQ8ilfwcuyklUjev4VNxhg;

		public int XC_0024rME0yvTFikERP2IK_NXY;

		public bool[] UpwhidkFZpPWx1q9lZBsxs4;

		public Vector3[] tvsdpI6J0TPvcT1FZFRrwpg;

		public bool kZvRrjq_M7YRbr9Tlnfb6Qc;

		private int int_0;

		private bool BngnOj_YgHMaT_YS6EPuSeJTAL5ZsoySAc1vkqPGKarD;

		private BuildData buildData_0;

		private List<BoxGenController> mKHIvDy1myjFaTS4C3kXZgw;

		private List<ShaftController> g_0024Jj_0024iHnWr1ONlkWbubne1U;

		private List<StampController> d0TVCQIZnggODSr2LoxmE4E;

		private List<int> a0vG1ZEwlUuzN0EY91Oc1YM;

		private List<BlockData.AAHMDBHDCDK> X5mZu6Ls6t1TbdradQjNDD8;

		private int _W03hk82PzzvcAWApzsyTb4;

		private int C1u6Ug52EtV72jzh_eBpTHw;

		private long KIV6vcWkGTQ8sa0ybVz4D38;

		private Vector3[] oiMSR7WWwJrfAUbHlqwYlNQ;

		private int[] xwQBfMRmYCdNxUSSGbbFluM;

		private Vector3[] oaFl6ZJye29BN6L1KhyoOd4;

		private int C39_Am6yjzOvsiGTol_0024_Ehg;

		private List<BoxGenController>.Enumerator RGVPoiWeW8KMKhn0x4JAiXo;

		private BoxGenController V_0024pyTsPmyoCq5Sxa_0024NJuhTOmkVxgiGa8b4F9V0riZ7P_0024;

		private bool BWotflsiCTLs8dYMhU3GMvA;

		private List<StampController>.Enumerator R8rmyRfRO5mf_0024ervV4noja0;

		private StampController wSxCxKqODPiH6W5WRt33iOWOo_UBm889GBSjd3tXirA9;

		private BlockData _ffyYK7CKkwmW9WUTzEG8j1U9C5hX1GhMn_4_0024_SlCkxt;

		private int dICA33w7eYv32IMf1sO_rug;

		private List<ShaftController>.Enumerator RCUuoCxD1NMsfhzYTMs6CVw;

		private ShaftController MCJ_0024oPXJwyr6YZR4lhofp46XGTnypARqZAbc99zPxaF7;

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
		public _0024iZGQyDxLT4VIvWohGlnNVsELgW4XVz2ciE8bE7kdx1kUfj95VGLpEo7v0y_0024ce8Ebyd4Ks5D9VgsNugDhYKZjXMhVr_0024Ob9_7Melhr0HCLCuQb9E9gjnMK6yb4EYzjO7zw2IU9G80ZuaCPojstssHjf4(int _003C_003E1__state)
		{
			SjlBM8inVA_YE4YVlr_0024gluY = _003C_003E1__state;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			switch (SjlBM8inVA_YE4YVlr_0024gluY)
			{
			case -3:
			case 4:
				try
				{
				}
				finally
				{
					ITybmnn_CCVC5Wu_0024wHlWVVQ();
				}
				break;
			case -4:
			case 5:
			case 6:
				try
				{
				}
				finally
				{
					IkXk87oS8XNDSZg4w7A9Tz0();
				}
				break;
			case -5:
			case 7:
				try
				{
				}
				finally
				{
					I25Yy4Wt5WvJ2FFuthm6M50();
				}
				break;
			}
			buildData_0 = null;
			mKHIvDy1myjFaTS4C3kXZgw = null;
			g_0024Jj_0024iHnWr1ONlkWbubne1U = null;
			d0TVCQIZnggODSr2LoxmE4E = null;
			a0vG1ZEwlUuzN0EY91Oc1YM = null;
			X5mZu6Ls6t1TbdradQjNDD8 = null;
			oiMSR7WWwJrfAUbHlqwYlNQ = null;
			xwQBfMRmYCdNxUSSGbbFluM = null;
			oaFl6ZJye29BN6L1KhyoOd4 = null;
			RGVPoiWeW8KMKhn0x4JAiXo = default(List<BoxGenController>.Enumerator);
			V_0024pyTsPmyoCq5Sxa_0024NJuhTOmkVxgiGa8b4F9V0riZ7P_0024 = null;
			R8rmyRfRO5mf_0024ervV4noja0 = default(List<StampController>.Enumerator);
			wSxCxKqODPiH6W5WRt33iOWOo_UBm889GBSjd3tXirA9 = null;
			_ffyYK7CKkwmW9WUTzEG8j1U9C5hX1GhMn_4_0024_SlCkxt = null;
			RCUuoCxD1NMsfhzYTMs6CVw = default(List<ShaftController>.Enumerator);
			MCJ_0024oPXJwyr6YZR4lhofp46XGTnypARqZAbc99zPxaF7 = null;
			SjlBM8inVA_YE4YVlr_0024gluY = -2;
		}

		private bool MoveNext()
		{
			try
			{
				BlockData jNKEKNOAPHO;
				GameObject gameObject;
				Transform transform;
				BlockController component;
				BlockController component2;
				BlockData jNKEKNOAPHO2;
				BlockData blockData;
				GameObject gameObject2;
				switch (SjlBM8inVA_YE4YVlr_0024gluY)
				{
				default:
					return false;
				case 0:
				{
					SjlBM8inVA_YE4YVlr_0024gluY = -1;
					int_0 = 0;
					mcK1TWle2rrKoYzxrO_0024wMYQ.BICFNKNMDMO = (EBuL2cFtbUzvmThrQfXEmXE ? 1 : 2);
					mcK1TWle2rrKoYzxrO_0024wMYQ.HCMMJPFOIHD = true;
					mcK1TWle2rrKoYzxrO_0024wMYQ.DHADJPNKHIJ = Vector3.up * smethod_0(-999f, -99f);
					BngnOj_YgHMaT_YS6EPuSeJTAL5ZsoySAc1vkqPGKarD = PAEHEMJNPND.DNMNDAACAIH;
					buildData_0 = ((!BngnOj_YgHMaT_YS6EPuSeJTAL5ZsoySAc1vkqPGKarD) ? smethod_1(JKGKJLLFMLE.HHGILAIOCLG) : smethod_1(Build.GFJLEEJELOL));
					if (qwuGs2uZZkvjLMEHDhpc6og != null)
					{
						buildData_0 = qwuGs2uZZkvjLMEHDhpc6og;
					}
					for (int num = NR6o8kJWEmRvjePi6tHtEEs.Count - 1; num >= 0; num--)
					{
						smethod_2((UnityEngine.Object)NR6o8kJWEmRvjePi6tHtEEs[num]);
					}
					NR6o8kJWEmRvjePi6tHtEEs.Clear();
					for (int num2 = mcK1TWle2rrKoYzxrO_0024wMYQ.CLNMBHMCPGB.Count - 1; num2 >= 0; num2--)
					{
						smethod_2((UnityEngine.Object)mcK1TWle2rrKoYzxrO_0024wMYQ.CLNMBHMCPGB[num2]);
					}
					mcK1TWle2rrKoYzxrO_0024wMYQ.CLNMBHMCPGB.Clear();
					smethod_3(mcK1TWle2rrKoYzxrO_0024wMYQ.EPGELCMKKOC, bool_0: false);
					mcK1TWle2rrKoYzxrO_0024wMYQ.EPGELCMKKOC.CJBGADKMKIC = true;
					BuildData.LEGHEEKCJAF surfaceType = buildData_0.surfaceType;
					smethod_4((surfaceType != BuildData.LEGHEEKCJAF.Auto) ? (surfaceType == BuildData.LEGHEEKCJAF.Flat) : JKGKJLLFMLE.IGOBPLOLHEP.isFlatSurface);
					smethod_5(buildData_0.antiSSAO);
					PAEHEMJNPND.DNMNDAACAIH = false;
					yPgQFjS68o1MK_0024lmACub20Q.Clear();
					moAdYEkdD59OffBEobdsjlI.Clear();
					mKHIvDy1myjFaTS4C3kXZgw = new List<BoxGenController>();
					g_0024Jj_0024iHnWr1ONlkWbubne1U = new List<ShaftController>();
					d0TVCQIZnggODSr2LoxmE4E = new List<StampController>();
					a0vG1ZEwlUuzN0EY91Oc1YM = new List<int>();
					X5mZu6Ls6t1TbdradQjNDD8 = new List<BlockData.AAHMDBHDCDK>();
					_W03hk82PzzvcAWApzsyTb4 = buildData_0.blockData.Count;
					C39_Am6yjzOvsiGTol_0024_Ehg = 0;
					goto IL_05b7;
				}
				case 1:
					SjlBM8inVA_YE4YVlr_0024gluY = -1;
					goto IL_05e1;
				case 2:
					SjlBM8inVA_YE4YVlr_0024gluY = -1;
					goto IL_06af;
				case 3:
					SjlBM8inVA_YE4YVlr_0024gluY = -1;
					goto IL_0760;
				case 4:
					SjlBM8inVA_YE4YVlr_0024gluY = -3;
					goto IL_0846;
				case 5:
					SjlBM8inVA_YE4YVlr_0024gluY = -4;
					goto IL_097d;
				case 6:
					SjlBM8inVA_YE4YVlr_0024gluY = -4;
					goto IL_0a79;
				case 7:
					SjlBM8inVA_YE4YVlr_0024gluY = -5;
					goto IL_0b49;
				case 8:
					SjlBM8inVA_YE4YVlr_0024gluY = -1;
					goto IL_0c38;
				case 9:
					SjlBM8inVA_YE4YVlr_0024gluY = -1;
					goto IL_0f5d;
				case 10:
					SjlBM8inVA_YE4YVlr_0024gluY = -1;
					goto IL_11be;
				case 11:
					{
						SjlBM8inVA_YE4YVlr_0024gluY = -1;
						goto IL_1292;
					}
					IL_0685:
					if (C39_Am6yjzOvsiGTol_0024_Ehg < C1u6Ug52EtV72jzh_eBpTHw)
					{
						if (int_0 <= 100)
						{
							int_0++;
							goto IL_06af;
						}
						int_0 = 0;
						yT7HpVIzmqW54W307WgJtr4 = null;
						SjlBM8inVA_YE4YVlr_0024gluY = 2;
						return true;
					}
					C39_Am6yjzOvsiGTol_0024_Ehg = a0vG1ZEwlUuzN0EY91Oc1YM.Count - 1;
					goto IL_0738;
					IL_092f:
					if (dICA33w7eYv32IMf1sO_rug >= 8)
					{
						goto IL_09b5;
					}
					if (int_0 <= 100)
					{
						int_0++;
						goto IL_0a79;
					}
					int_0 = 0;
					yT7HpVIzmqW54W307WgJtr4 = null;
					SjlBM8inVA_YE4YVlr_0024gluY = 6;
					return true;
					IL_0760:
					jNKEKNOAPHO = NR6o8kJWEmRvjePi6tHtEEs[a0vG1ZEwlUuzN0EY91Oc1YM[C39_Am6yjzOvsiGTol_0024_Ehg]].GetComponent<BlockController>().JNKEKNOAPHO;
					jNKEKNOAPHO.type = X5mZu6Ls6t1TbdradQjNDD8[C39_Am6yjzOvsiGTol_0024_Ehg];
					jNKEKNOAPHO.gid = 0;
					C39_Am6yjzOvsiGTol_0024_Ehg--;
					goto IL_0738;
					IL_06af:
					gameObject = smethod_21();
					gameObject.name = global::_003CModule_003E.smethod_28<string>(2301957451u) + C39_Am6yjzOvsiGTol_0024_Ehg;
					mcK1TWle2rrKoYzxrO_0024wMYQ.CLNMBHMCPGB.Add(gameObject);
					C39_Am6yjzOvsiGTol_0024_Ehg++;
					goto IL_0685;
					IL_1292:
					transform = mcK1TWle2rrKoYzxrO_0024wMYQ.CLNMBHMCPGB[C39_Am6yjzOvsiGTol_0024_Ehg].transform;
					if (!(transform.lossyScale.x < 0.99f))
					{
						transform.position += oaFl6ZJye29BN6L1KhyoOd4[C39_Am6yjzOvsiGTol_0024_Ehg];
					}
					else
					{
						transform.localScale = Vector3.one;
						UpwhidkFZpPWx1q9lZBsxs4[C39_Am6yjzOvsiGTol_0024_Ehg] = false;
					}
					C39_Am6yjzOvsiGTol_0024_Ehg--;
					goto IL_124c;
					IL_0c38:
					component = NR6o8kJWEmRvjePi6tHtEEs[C39_Am6yjzOvsiGTol_0024_Ehg].GetComponent<BlockController>();
					if (!component.JNKEKNOAPHO.CheckMask(KIV6vcWkGTQ8sa0ybVz4D38))
					{
						component.HideBlock();
					}
					C39_Am6yjzOvsiGTol_0024_Ehg--;
					goto IL_0c13;
					IL_0aad:
					RCUuoCxD1NMsfhzYTMs6CVw = g_0024Jj_0024iHnWr1ONlkWbubne1U.GetEnumerator();
					SjlBM8inVA_YE4YVlr_0024gluY = -5;
					goto IL_0b0c;
					IL_11be:
					if (mcK1TWle2rrKoYzxrO_0024wMYQ.EPGELCMKKOC.PKBPJPCJAID[C39_Am6yjzOvsiGTol_0024_Ehg] < 0)
					{
						if (U3JVnvAEQIewfxdg8MwdqjQ)
						{
							mcK1TWle2rrKoYzxrO_0024wMYQ.CLNMBHMCPGB[C39_Am6yjzOvsiGTol_0024_Ehg].SetActive(value: false);
						}
					}
					else
					{
						Transform transform2 = mcK1TWle2rrKoYzxrO_0024wMYQ.CLNMBHMCPGB[C39_Am6yjzOvsiGTol_0024_Ehg].transform;
						transform2.parent = mcK1TWle2rrKoYzxrO_0024wMYQ.CLNMBHMCPGB[mcK1TWle2rrKoYzxrO_0024wMYQ.EPGELCMKKOC.PKBPJPCJAID[C39_Am6yjzOvsiGTol_0024_Ehg]].transform;
						transform2.localRotation = BDLEJBBJJOI.INECOALCJIE(BDLEJBBJJOI.GKCKPLGPDFK(mcK1TWle2rrKoYzxrO_0024wMYQ.EPGELCMKKOC.MJBDKMNEKML[C39_Am6yjzOvsiGTol_0024_Ehg]));
						transform2.localPosition = mcK1TWle2rrKoYzxrO_0024wMYQ.EPGELCMKKOC.NNNBCKKNONF[C39_Am6yjzOvsiGTol_0024_Ehg];
						if (UpwhidkFZpPWx1q9lZBsxs4[C39_Am6yjzOvsiGTol_0024_Ehg])
						{
							transform2.localScale *= 0.998f;
							if (xwQBfMRmYCdNxUSSGbbFluM[C39_Am6yjzOvsiGTol_0024_Ehg] == 0)
							{
								DP.CD(global::_003CModule_003E.smethod_28<string>(2028777748u) + C39_Am6yjzOvsiGTol_0024_Ehg + global::_003CModule_003E.smethod_26<string>(3043991974u));
								xwQBfMRmYCdNxUSSGbbFluM[C39_Am6yjzOvsiGTol_0024_Ehg] = 1;
							}
							tvsdpI6J0TPvcT1FZFRrwpg[C39_Am6yjzOvsiGTol_0024_Ehg] = oiMSR7WWwJrfAUbHlqwYlNQ[C39_Am6yjzOvsiGTol_0024_Ehg] / xwQBfMRmYCdNxUSSGbbFluM[C39_Am6yjzOvsiGTol_0024_Ehg];
							oaFl6ZJye29BN6L1KhyoOd4[C39_Am6yjzOvsiGTol_0024_Ehg] = (tvsdpI6J0TPvcT1FZFRrwpg[C39_Am6yjzOvsiGTol_0024_Ehg] - transform2.position) * 0.002f;
						}
					}
					C39_Am6yjzOvsiGTol_0024_Ehg--;
					goto IL_100f;
					IL_124c:
					if (C39_Am6yjzOvsiGTol_0024_Ehg >= 0)
					{
						if (int_0 <= 100)
						{
							int_0++;
							goto IL_1292;
						}
						int_0 = 0;
						yT7HpVIzmqW54W307WgJtr4 = null;
						SjlBM8inVA_YE4YVlr_0024gluY = 11;
						return true;
					}
					kZvRrjq_M7YRbr9Tlnfb6Qc = false;
					mcK1TWle2rrKoYzxrO_0024wMYQ.BKMCJDEEDJD();
					if (mcK1TWle2rrKoYzxrO_0024wMYQ.CLNMBHMCPGB.Count > 0)
					{
						FJLJNEKHKKH.DMADFEPLJNP(mcK1TWle2rrKoYzxrO_0024wMYQ.CLNMBHMCPGB[0].transform, 9);
					}
					PAEHEMJNPND.CAMDMHABENK(BHCKMFDEBBH: false);
					return false;
					IL_0f5d:
					component2 = NR6o8kJWEmRvjePi6tHtEEs[C39_Am6yjzOvsiGTol_0024_Ehg].GetComponent<BlockController>();
					jNKEKNOAPHO2 = component2.JNKEKNOAPHO;
					if (component2.MKHLOPIFNDI || component2.DCNIOOFAOMB < 0 || jNKEKNOAPHO2.CheckMask(12884901888L))
					{
						NR6o8kJWEmRvjePi6tHtEEs[C39_Am6yjzOvsiGTol_0024_Ehg].SetActive(value: false);
					}
					else
					{
						if (jNKEKNOAPHO2.CheckMask(543277952L) && component2.DCNIOOFAOMB > 0 && component2.ILFEIIFBHMP >= 0 && (jNKEKNOAPHO2.type == BlockData.AAHMDBHDCDK.Coupler || jNKEKNOAPHO2.gid == 7))
						{
							oiMSR7WWwJrfAUbHlqwYlNQ[component2.DCNIOOFAOMB] += jNKEKNOAPHO2.GetPos();
							xwQBfMRmYCdNxUSSGbbFluM[component2.DCNIOOFAOMB]++;
						}
						if (jNKEKNOAPHO2.CheckMask(17666539541L) && (jNKEKNOAPHO2.rgbI & 0x1000000) != 0)
						{
							NR6o8kJWEmRvjePi6tHtEEs[C39_Am6yjzOvsiGTol_0024_Ehg].SetActive(value: false);
						}
						else
						{
							if (component2.DCNIOOFAOMB > 0 && jNKEKNOAPHO2.gid == 7 && jNKEKNOAPHO2.type == BlockData.AAHMDBHDCDK.Chassis && jNKEKNOAPHO2.press >> 6 == 0)
							{
								UpwhidkFZpPWx1q9lZBsxs4[component2.DCNIOOFAOMB] = true;
							}
							if ((jNKEKNOAPHO2.rgbI == 16777216 || jNKEKNOAPHO2.type == BlockData.AAHMDBHDCDK.Coupler) && !jNKEKNOAPHO2.CheckMask(-1073741808L))
							{
								NR6o8kJWEmRvjePi6tHtEEs[C39_Am6yjzOvsiGTol_0024_Ehg].SetActive(value: false);
								if (jNKEKNOAPHO2.type != BlockData.AAHMDBHDCDK.Coupler || component2.ILFEIIFBHMP < 0)
								{
									goto IL_0f21;
								}
								mcK1TWle2rrKoYzxrO_0024wMYQ.PDDNDFNLDDG[component2.DCNIOOFAOMB] = component2;
							}
							if (jNKEKNOAPHO2.gid == 7 && jNKEKNOAPHO2.CheckMask(6407040L))
							{
								JointController component3 = component2.GetComponent<JointController>();
								component3.DivideMesh(mcK1TWle2rrKoYzxrO_0024wMYQ.CLNMBHMCPGB[component2.ILFEIIFBHMP], MCKBICGHHOK: true);
								if (jNKEKNOAPHO2.type == BlockData.AAHMDBHDCDK.PistonL)
								{
									(component3 as PistonController).MakeFakeArm();
								}
							}
							NR6o8kJWEmRvjePi6tHtEEs[C39_Am6yjzOvsiGTol_0024_Ehg].transform.parent = mcK1TWle2rrKoYzxrO_0024wMYQ.CLNMBHMCPGB[component2.DCNIOOFAOMB].transform;
							component2.LJOAMOJGJIL = false;
						}
					}
					goto IL_0f21;
					IL_0c13:
					if (C39_Am6yjzOvsiGTol_0024_Ehg >= 0)
					{
						if (int_0 <= 100)
						{
							int_0++;
							goto IL_0c38;
						}
						int_0 = 0;
						yT7HpVIzmqW54W307WgJtr4 = null;
						SjlBM8inVA_YE4YVlr_0024gluY = 8;
						return true;
					}
					mcK1TWle2rrKoYzxrO_0024wMYQ.PDDNDFNLDDG = new BlockController[C1u6Ug52EtV72jzh_eBpTHw];
					UpwhidkFZpPWx1q9lZBsxs4 = new bool[C1u6Ug52EtV72jzh_eBpTHw];
					tvsdpI6J0TPvcT1FZFRrwpg = new Vector3[C1u6Ug52EtV72jzh_eBpTHw];
					oiMSR7WWwJrfAUbHlqwYlNQ = new Vector3[C1u6Ug52EtV72jzh_eBpTHw];
					xwQBfMRmYCdNxUSSGbbFluM = new int[C1u6Ug52EtV72jzh_eBpTHw];
					C39_Am6yjzOvsiGTol_0024_Ehg = 0;
					goto IL_0f33;
					IL_0809:
					if (RGVPoiWeW8KMKhn0x4JAiXo.MoveNext())
					{
						V_0024pyTsPmyoCq5Sxa_0024NJuhTOmkVxgiGa8b4F9V0riZ7P_0024 = RGVPoiWeW8KMKhn0x4JAiXo.Current;
						if (int_0 <= 100)
						{
							int_0++;
							goto IL_0846;
						}
						int_0 = 0;
						yT7HpVIzmqW54W307WgJtr4 = null;
						SjlBM8inVA_YE4YVlr_0024gluY = 4;
						return true;
					}
					ITybmnn_CCVC5Wu_0024wHlWVVQ();
					RGVPoiWeW8KMKhn0x4JAiXo = default(List<BoxGenController>.Enumerator);
					if ((bool)mcK1TWle2rrKoYzxrO_0024wMYQ.LMNBBEDPNCH)
					{
						BWotflsiCTLs8dYMhU3GMvA = true;
						R8rmyRfRO5mf_0024ervV4noja0 = d0TVCQIZnggODSr2LoxmE4E.GetEnumerator();
						SjlBM8inVA_YE4YVlr_0024gluY = -4;
						goto IL_093d;
					}
					goto IL_0aad;
					IL_0a79:
					if (_ffyYK7CKkwmW9WUTzEG8j1U9C5hX1GhMn_4_0024_SlCkxt.actionID[dICA33w7eYv32IMf1sO_rug] != C39_Am6yjzOvsiGTol_0024_Ehg)
					{
						dICA33w7eYv32IMf1sO_rug++;
						goto IL_092f;
					}
					wSxCxKqODPiH6W5WRt33iOWOo_UBm889GBSjd3tXirA9.Bake(mcK1TWle2rrKoYzxrO_0024wMYQ.EPGELCMKKOC, _ffyYK7CKkwmW9WUTzEG8j1U9C5hX1GhMn_4_0024_SlCkxt.actionParam[dICA33w7eYv32IMf1sO_rug], sPQ8ilfwcuyklUjev4VNxhg, XC_0024rME0yvTFikERP2IK_NXY, mcK1TWle2rrKoYzxrO_0024wMYQ.AJKPKECMDIJ);
					if (BWotflsiCTLs8dYMhU3GMvA && (bool)wSxCxKqODPiH6W5WRt33iOWOo_UBm889GBSjd3tXirA9.HDKLPEHKJNA)
					{
						wSxCxKqODPiH6W5WRt33iOWOo_UBm889GBSjd3tXirA9.HDKLPEHKJNA.GetComponent<MeshRenderer>().sharedMaterial.mainTexture = mcK1TWle2rrKoYzxrO_0024wMYQ.LMNBBEDPNCH;
						BWotflsiCTLs8dYMhU3GMvA = false;
					}
					goto IL_09b5;
					IL_0f21:
					C39_Am6yjzOvsiGTol_0024_Ehg++;
					goto IL_0f33;
					IL_05b7:
					if (C39_Am6yjzOvsiGTol_0024_Ehg < _W03hk82PzzvcAWApzsyTb4)
					{
						if (int_0 <= 100)
						{
							int_0++;
							goto IL_05e1;
						}
						int_0 = 0;
						yT7HpVIzmqW54W307WgJtr4 = null;
						SjlBM8inVA_YE4YVlr_0024gluY = 1;
						return true;
					}
					_W03hk82PzzvcAWApzsyTb4 = NR6o8kJWEmRvjePi6tHtEEs.Count;
					PAEHEMJNPND.DNMNDAACAIH = BngnOj_YgHMaT_YS6EPuSeJTAL5ZsoySAc1vkqPGKarD;
					smethod_19(mcK1TWle2rrKoYzxrO_0024wMYQ.EPGELCMKKOC, (BlockController)null);
					C1u6Ug52EtV72jzh_eBpTHw = smethod_20(mcK1TWle2rrKoYzxrO_0024wMYQ.EPGELCMKKOC);
					C39_Am6yjzOvsiGTol_0024_Ehg = 0;
					goto IL_0685;
					IL_0846:
					V_0024pyTsPmyoCq5Sxa_0024NJuhTOmkVxgiGa8b4F9V0riZ7P_0024.GetComponent<MeshRenderer>().enabled = false;
					V_0024pyTsPmyoCq5Sxa_0024NJuhTOmkVxgiGa8b4F9V0riZ7P_0024.MakeBox();
					V_0024pyTsPmyoCq5Sxa_0024NJuhTOmkVxgiGa8b4F9V0riZ7P_0024.MakeCollider();
					V_0024pyTsPmyoCq5Sxa_0024NJuhTOmkVxgiGa8b4F9V0riZ7P_0024.NGLBLAGMBLN.SetActive(value: true);
					UnityEngine.Object.Destroy(V_0024pyTsPmyoCq5Sxa_0024NJuhTOmkVxgiGa8b4F9V0riZ7P_0024.gameObject.GetComponent<Collider>());
					V_0024pyTsPmyoCq5Sxa_0024NJuhTOmkVxgiGa8b4F9V0riZ7P_0024 = null;
					goto IL_0809;
					IL_09b5:
					_ffyYK7CKkwmW9WUTzEG8j1U9C5hX1GhMn_4_0024_SlCkxt = null;
					wSxCxKqODPiH6W5WRt33iOWOo_UBm889GBSjd3tXirA9 = null;
					goto IL_093d;
					IL_05e1:
					blockData = buildData_0.blockData[C39_Am6yjzOvsiGTol_0024_Ehg];
					gameObject2 = smethod_6(blockData, U3JVnvAEQIewfxdg8MwdqjQ);
					if (!smethod_7((UnityEngine.Object)gameObject2, (UnityEngine.Object)null))
					{
						if (blockData.type != BlockData.AAHMDBHDCDK.Chassis)
						{
							if (!smethod_14(blockData, 6407040L))
							{
								if (blockData.type == BlockData.AAHMDBHDCDK.BoxGen)
								{
									mKHIvDy1myjFaTS4C3kXZgw.Add(gameObject2.GetComponent<BoxGenController>());
									yPgQFjS68o1MK_0024lmACub20Q.Add(C39_Am6yjzOvsiGTol_0024_Ehg);
									moAdYEkdD59OffBEobdsjlI.Add(blockData);
								}
								else if (blockData.type == BlockData.AAHMDBHDCDK.CapGen)
								{
									smethod_15(gameObject2.GetComponent<CapGenController>(), (GameObject)null);
									smethod_16((Renderer)gameObject2.GetComponent<MeshRenderer>(), bool_0: false);
									smethod_2((UnityEngine.Object)gameObject2.GetComponent<Collider>());
									yPgQFjS68o1MK_0024lmACub20Q.Add(C39_Am6yjzOvsiGTol_0024_Ehg);
									moAdYEkdD59OffBEobdsjlI.Add(blockData);
								}
								else if (blockData.type != BlockData.AAHMDBHDCDK.Coupler)
								{
									if (blockData.type != BlockData.AAHMDBHDCDK.Wheel)
									{
										if (blockData.type != BlockData.AAHMDBHDCDK.Shaft)
										{
											if (smethod_14(blockData, 12884901888L))
											{
												d0TVCQIZnggODSr2LoxmE4E.Add(gameObject2.GetComponent<StampController>());
											}
										}
										else
										{
											g_0024Jj_0024iHnWr1ONlkWbubne1U.Add(gameObject2.GetComponent<ShaftController>());
										}
									}
									else
									{
										smethod_17(gameObject2.GetComponent<WheelController>(), (GameObject)null);
									}
								}
								else
								{
									yPgQFjS68o1MK_0024lmACub20Q.Add(C39_Am6yjzOvsiGTol_0024_Ehg);
									moAdYEkdD59OffBEobdsjlI.Add(blockData);
								}
							}
							else if (blockData.gid != 7)
							{
								if (EBuL2cFtbUzvmThrQfXEmXE)
								{
									if (smethod_14(blockData, 4195200L))
									{
										switch (blockData.type)
										{
										case BlockData.AAHMDBHDCDK.PistonS:
											blockData.type = BlockData.AAHMDBHDCDK.PistonL;
											break;
										case BlockData.AAHMDBHDCDK.JointTS:
											blockData.type = BlockData.AAHMDBHDCDK.JointTA;
											break;
										case BlockData.AAHMDBHDCDK.JointPS:
											blockData.type = BlockData.AAHMDBHDCDK.JointPA;
											break;
										case BlockData.AAHMDBHDCDK.JointBS:
											blockData.type = BlockData.AAHMDBHDCDK.JointBA;
											break;
										}
										int num3 = 0;
										for (int i = 0; i < 8; i++)
										{
											if (blockData.actionID[i] == 70)
											{
												num3 = blockData.actionParam[i];
											}
										}
										blockData.actionID[0] = 60;
										blockData.actionParam[0] = num3;
										blockData.actionID[1] = -1;
										blockData.actionParam[1] = 0;
									}
									else if (blockData.type == BlockData.AAHMDBHDCDK.PistonL && blockData.gid != 7)
									{
										bool flag = false;
										for (int j = 0; j < 8; j++)
										{
											if (flag)
											{
												blockData.actionID[j] = -1;
												blockData.actionParam[j] = 0;
											}
											else if (blockData.actionID[j] == 60)
											{
												flag = true;
											}
										}
									}
									blockData.gid = 7;
									if (smethod_14(blockData, 98304L))
									{
										for (int k = 0; k < 8; k++)
										{
											if (blockData.actionID[k] == 60 && blockData.actionParam[k] == -91)
											{
												blockData.gid = -1;
												break;
											}
										}
									}
								}
								else
								{
									a0vG1ZEwlUuzN0EY91Oc1YM.Add(C39_Am6yjzOvsiGTol_0024_Ehg);
									X5mZu6Ls6t1TbdradQjNDD8.Add(blockData.type);
									blockData.type = BlockData.AAHMDBHDCDK.Cannon1;
									blockData.gid = 7;
								}
							}
							else
							{
								yPgQFjS68o1MK_0024lmACub20Q.Add(C39_Am6yjzOvsiGTol_0024_Ehg);
								moAdYEkdD59OffBEobdsjlI.Add(blockData);
							}
						}
						else
						{
							if (smethod_9(smethod_8(gameObject2)) == 1)
							{
								smethod_2((UnityEngine.Object)smethod_11((Component)smethod_10(smethod_8(gameObject2), 0)));
							}
							if (blockData.press >> 6 != 0)
							{
								Collider component4 = gameObject2.GetComponent<Collider>();
								if (smethod_12((UnityEngine.Object)component4))
								{
									smethod_13(component4, bool_0: false);
								}
							}
						}
						NR6o8kJWEmRvjePi6tHtEEs.Add(gameObject2);
						smethod_18(mcK1TWle2rrKoYzxrO_0024wMYQ.EPGELCMKKOC, gameObject2.GetComponent<BlockController>());
					}
					C39_Am6yjzOvsiGTol_0024_Ehg++;
					goto IL_05b7;
					IL_093d:
					if (R8rmyRfRO5mf_0024ervV4noja0.MoveNext())
					{
						wSxCxKqODPiH6W5WRt33iOWOo_UBm889GBSjd3tXirA9 = R8rmyRfRO5mf_0024ervV4noja0.Current;
						if (int_0 <= 100)
						{
							int_0++;
							goto IL_097d;
						}
						int_0 = 0;
						yT7HpVIzmqW54W307WgJtr4 = null;
						SjlBM8inVA_YE4YVlr_0024gluY = 5;
						return true;
					}
					IkXk87oS8XNDSZg4w7A9Tz0();
					R8rmyRfRO5mf_0024ervV4noja0 = default(List<StampController>.Enumerator);
					goto IL_0aad;
					IL_0738:
					if (C39_Am6yjzOvsiGTol_0024_Ehg >= 0)
					{
						if (int_0 <= 100)
						{
							int_0++;
							goto IL_0760;
						}
						int_0 = 0;
						yT7HpVIzmqW54W307WgJtr4 = null;
						SjlBM8inVA_YE4YVlr_0024gluY = 3;
						return true;
					}
					mcK1TWle2rrKoYzxrO_0024wMYQ.EPGELCMKKOC.DONCHDIDFEE();
					RGVPoiWeW8KMKhn0x4JAiXo = mKHIvDy1myjFaTS4C3kXZgw.GetEnumerator();
					SjlBM8inVA_YE4YVlr_0024gluY = -3;
					goto IL_0809;
					IL_0b0c:
					if (RCUuoCxD1NMsfhzYTMs6CVw.MoveNext())
					{
						MCJ_0024oPXJwyr6YZR4lhofp46XGTnypARqZAbc99zPxaF7 = RCUuoCxD1NMsfhzYTMs6CVw.Current;
						if (int_0 <= 100)
						{
							int_0++;
							goto IL_0b49;
						}
						int_0 = 0;
						yT7HpVIzmqW54W307WgJtr4 = null;
						SjlBM8inVA_YE4YVlr_0024gluY = 7;
						return true;
					}
					I25Yy4Wt5WvJ2FFuthm6M50();
					RCUuoCxD1NMsfhzYTMs6CVw = default(List<ShaftController>.Enumerator);
					KIV6vcWkGTQ8sa0ybVz4D38 = long.MaxValue;
					if (mcK1TWle2rrKoYzxrO_0024wMYQ.KHMJJPMFENJ != 1)
					{
						if (mcK1TWle2rrKoYzxrO_0024wMYQ.KHMJJPMFENJ == 2)
						{
							KIV6vcWkGTQ8sa0ybVz4D38 = JKGKJLLFMLE.IGOBPLOLHEP.showMask2;
						}
					}
					else
					{
						KIV6vcWkGTQ8sa0ybVz4D38 = JKGKJLLFMLE.IGOBPLOLHEP.showMask1;
					}
					C39_Am6yjzOvsiGTol_0024_Ehg = NR6o8kJWEmRvjePi6tHtEEs.Count - 1;
					goto IL_0c13;
					IL_097d:
					_ffyYK7CKkwmW9WUTzEG8j1U9C5hX1GhMn_4_0024_SlCkxt = wSxCxKqODPiH6W5WRt33iOWOo_UBm889GBSjd3tXirA9.JNKEKNOAPHO;
					C39_Am6yjzOvsiGTol_0024_Ehg = ((_ffyYK7CKkwmW9WUTzEG8j1U9C5hX1GhMn_4_0024_SlCkxt.type != BlockData.AAHMDBHDCDK.StampF) ? 72 : 60);
					dICA33w7eYv32IMf1sO_rug = 0;
					goto IL_092f;
					IL_0b49:
					MCJ_0024oPXJwyr6YZR4lhofp46XGTnypARqZAbc99zPxaF7.MakeWing();
					MCJ_0024oPXJwyr6YZR4lhofp46XGTnypARqZAbc99zPxaF7 = null;
					goto IL_0b0c;
					IL_0f33:
					if (C39_Am6yjzOvsiGTol_0024_Ehg < _W03hk82PzzvcAWApzsyTb4)
					{
						if (int_0 <= 100)
						{
							int_0++;
							goto IL_0f5d;
						}
						int_0 = 0;
						yT7HpVIzmqW54W307WgJtr4 = null;
						SjlBM8inVA_YE4YVlr_0024gluY = 9;
						return true;
					}
					oaFl6ZJye29BN6L1KhyoOd4 = new Vector3[C1u6Ug52EtV72jzh_eBpTHw];
					C39_Am6yjzOvsiGTol_0024_Ehg = C1u6Ug52EtV72jzh_eBpTHw - 1;
					goto IL_100f;
					IL_100f:
					if (C39_Am6yjzOvsiGTol_0024_Ehg > 0)
					{
						if (int_0 <= 100)
						{
							int_0++;
							goto IL_11be;
						}
						int_0 = 0;
						yT7HpVIzmqW54W307WgJtr4 = null;
						SjlBM8inVA_YE4YVlr_0024gluY = 10;
						return true;
					}
					C39_Am6yjzOvsiGTol_0024_Ehg = C1u6Ug52EtV72jzh_eBpTHw - 1;
					goto IL_124c;
				}
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
			((IDisposable)RGVPoiWeW8KMKhn0x4JAiXo/*cast due to .constrained prefix*/).Dispose();
		}

		private void IkXk87oS8XNDSZg4w7A9Tz0()
		{
			SjlBM8inVA_YE4YVlr_0024gluY = -1;
			((IDisposable)R8rmyRfRO5mf_0024ervV4noja0/*cast due to .constrained prefix*/).Dispose();
		}

		private void I25Yy4Wt5WvJ2FFuthm6M50()
		{
			SjlBM8inVA_YE4YVlr_0024gluY = -1;
			((IDisposable)RCUuoCxD1NMsfhzYTMs6CVw/*cast due to .constrained prefix*/).Dispose();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw smethod_22();
		}

		internal static float smethod_0(float float_0, float float_1)
		{
			return UnityEngine.Random.Range(float_0, float_1);
		}

		internal static BuildData smethod_1(BuildData buildData_1)
		{
			return buildData_1.Clone();
		}

		internal static void smethod_2(UnityEngine.Object object_0)
		{
			UnityEngine.Object.Destroy(object_0);
		}

		internal static void smethod_3(HDBLLPODNLN hdbllpodnln_0, bool bool_0)
		{
			hdbllpodnln_0.AFJJGAHKLKD(bool_0);
		}

		internal static void smethod_4(bool bool_0)
		{
			PAEHEMJNPND.CAMDMHABENK(bool_0);
		}

		internal static void smethod_5(int[] int_1)
		{
			PAEHEMJNPND.PMNNCDCOCCA(int_1);
		}

		internal static GameObject smethod_6(BlockData blockData_0, bool bool_0)
		{
			return PAEHEMJNPND.PKLHNJNFKFH(blockData_0, bool_0);
		}

		internal static bool smethod_7(UnityEngine.Object object_0, UnityEngine.Object object_1)
		{
			return object_0 == object_1;
		}

		internal static Transform smethod_8(GameObject gameObject_0)
		{
			return gameObject_0.transform;
		}

		internal static int smethod_9(Transform transform_0)
		{
			return transform_0.childCount;
		}

		internal static Transform smethod_10(Transform transform_0, int int_1)
		{
			return transform_0.GetChild(int_1);
		}

		internal static GameObject smethod_11(Component component_0)
		{
			return component_0.gameObject;
		}

		internal static bool smethod_12(UnityEngine.Object object_0)
		{
			return object_0;
		}

		internal static void smethod_13(Collider collider_0, bool bool_0)
		{
			collider_0.enabled = bool_0;
		}

		internal static bool smethod_14(BlockData blockData_0, long long_0)
		{
			return blockData_0.CheckMask(long_0);
		}

		internal static void smethod_15(CapGenController capGenController_0, GameObject gameObject_0)
		{
			capGenController_0.MakeCapsule(gameObject_0);
		}

		internal static void smethod_16(Renderer renderer_0, bool bool_0)
		{
			renderer_0.enabled = bool_0;
		}

		internal static void smethod_17(WheelController wheelController_0, GameObject gameObject_0)
		{
			wheelController_0.MakeTire(gameObject_0);
		}

		internal static void smethod_18(HDBLLPODNLN hdbllpodnln_0, BlockController blockController_0)
		{
			hdbllpodnln_0.HDLEKABOEFL(blockController_0);
		}

		internal static void smethod_19(HDBLLPODNLN hdbllpodnln_0, BlockController blockController_0)
		{
			hdbllpodnln_0.ANBKLJFHMOB(blockController_0);
		}

		internal static int smethod_20(HDBLLPODNLN hdbllpodnln_0)
		{
			return hdbllpodnln_0.JKAJGAGDMAJ();
		}

		internal static GameObject smethod_21()
		{
			return new GameObject();
		}

		internal static NotSupportedException smethod_22()
		{
			return new NotSupportedException();
		}
	}

	[CompilerGenerated]
	private sealed class Aj9rkvkmT0_0024GQciqtMEiLoWD5a_0024gKbx4KymieNRH_0024O0RHbyp2pFsaZFdtjgqV3_0024nbr6iYsaYKTiLAj2fMo3A0wDxDqA5Lb6dGhkXOcsWSsw1jB4qw3H977wjw_0024ZauG1Li9w8ETLpQtyep8f_0024wttCu2o : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int SjlBM8inVA_YE4YVlr_0024gluY;

		private object yT7HpVIzmqW54W307WgJtr4;

		public HDBLLPODNLN n96ilJFY3oZo7_0024vgH1NCChk;

		private Dictionary<int, CouplerController> zMonjAsNtKI07jpCZ1IhOyF1DvuSjMmc3s0LTOX20RMA;

		private int int_0;

		private List<BlockController> gSzvvc1hw7KDANGtzedjXXw;

		private int _0024LN7pf1eAoX0cI3qjp27Wzs;

		private bool SWqU_Q9z_2q3GFCdAn2er7M;

		private int WqeupfJ4Rcnn_002487y2EDHCh8;

		private List<BlockController>.Enumerator YGH6LpmmEtNyUILba0H0_zU;

		private BlockController _0024V3vQpPHCxrElIm2DNCGf3VCOZ3GO6WK0Yazd5ChiAk6;

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
		public Aj9rkvkmT0_0024GQciqtMEiLoWD5a_0024gKbx4KymieNRH_0024O0RHbyp2pFsaZFdtjgqV3_0024nbr6iYsaYKTiLAj2fMo3A0wDxDqA5Lb6dGhkXOcsWSsw1jB4qw3H977wjw_0024ZauG1Li9w8ETLpQtyep8f_0024wttCu2o(int _003C_003E1__state)
		{
			SjlBM8inVA_YE4YVlr_0024gluY = _003C_003E1__state;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			switch (SjlBM8inVA_YE4YVlr_0024gluY)
			{
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
			case -4:
			case 5:
				try
				{
				}
				finally
				{
					IkXk87oS8XNDSZg4w7A9Tz0();
				}
				break;
			}
			zMonjAsNtKI07jpCZ1IhOyF1DvuSjMmc3s0LTOX20RMA = null;
			gSzvvc1hw7KDANGtzedjXXw = null;
			YGH6LpmmEtNyUILba0H0_zU = default(List<BlockController>.Enumerator);
			_0024V3vQpPHCxrElIm2DNCGf3VCOZ3GO6WK0Yazd5ChiAk6 = null;
			SjlBM8inVA_YE4YVlr_0024gluY = -2;
		}

		private bool MoveNext()
		{
			try
			{
				BlockController blockController2;
				switch (SjlBM8inVA_YE4YVlr_0024gluY)
				{
				default:
					return false;
				case 0:
				{
					SjlBM8inVA_YE4YVlr_0024gluY = -1;
					MPatchr.ShowDebugMsg(global::_003CModule_003E.smethod_25<string>(3109292137u));
					zMonjAsNtKI07jpCZ1IhOyF1DvuSjMmc3s0LTOX20RMA = Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<Dictionary<int, CouplerController>>(global::_003CModule_003E.smethod_26<string>(596572655u), n96ilJFY3oZo7_0024vgH1NCChk);
					List<int> list = Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<List<int>>(global::_003CModule_003E.smethod_25<string>(2012908250u), n96ilJFY3oZo7_0024vgH1NCChk);
					List<BlockController> list2 = Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<List<BlockController>>(global::_003CModule_003E.smethod_25<string>(2241670438u), n96ilJFY3oZo7_0024vgH1NCChk);
					BlockController blockController = Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<BlockController>(global::_003CModule_003E.smethod_25<string>(3206170754u), n96ilJFY3oZo7_0024vgH1NCChk);
					Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<int>(global::_003CModule_003E.smethod_28<string>(1938607089u), n96ilJFY3oZo7_0024vgH1NCChk);
					int_0 = 0;
					foreach (BlockController item in n96ilJFY3oZo7_0024vgH1NCChk.GEGOAHPBGKB)
					{
						item.DCNIOOFAOMB = -1;
						item.ILFEIIFBHMP = -1;
						item.LJOAMOJGJIL = false;
					}
					zMonjAsNtKI07jpCZ1IhOyF1DvuSjMmc3s0LTOX20RMA.Clear();
					list.Clear();
					n96ilJFY3oZo7_0024vgH1NCChk.CDOJFIKGAMA = new List<int>();
					n96ilJFY3oZo7_0024vgH1NCChk.CKILPAHPPOO = new List<BlockData>();
					n96ilJFY3oZo7_0024vgH1NCChk.FKLBCLHKNML = new Dictionary<BlockData, BlockData>();
					list2 = new List<BlockController>();
					List<BlockController> list3 = new List<BlockController>();
					gSzvvc1hw7KDANGtzedjXXw = new List<BlockController>();
					tjLsYT_0024xlAPMPH7Oek0spTI(n96ilJFY3oZo7_0024vgH1NCChk, blockController, null, 0, list3, gSzvvc1hw7KDANGtzedjXXw);
					bool flag = SceneMan.JFAOKFIDAGK is Build;
					_0024LN7pf1eAoX0cI3qjp27Wzs = (smethod_0((UnityEngine.Object)blockController) ? 1 : 0);
					for (int i = 0; i < 9999; i++)
					{
						if (i > 9990)
						{
							DP.D(global::_003CModule_003E.smethod_28<string>(1574367485u) + i);
						}
						if (list3.Count == 0)
						{
							break;
						}
						if (list3[0].DCNIOOFAOMB >= 0)
						{
							if (!flag)
							{
								n96ilJFY3oZo7_0024vgH1NCChk.GEGOAHPBGKB.Remove(list3[0]);
							}
							else
							{
								list3[0].LJOAMOJGJIL = true;
							}
							if (HNJDDKJLHMM.FHLGOMHPDLN == HNJDDKJLHMM.HKGAACMIPIH.Offline)
							{
								string text = global::_003CModule_003E.smethod_28<string>(3850865010u);
								if (HelpDefs.isJ)
								{
									text += global::_003CModule_003E.smethod_26<string>(571908725u);
									text += list3[0].JNKEKNOAPHO.type;
									text += global::_003CModule_003E.smethod_26<string>(4142014077u);
								}
								else
								{
									text += global::_003CModule_003E.smethod_26<string>(7430500u);
									text += list3[0].JNKEKNOAPHO.type;
									text += global::_003CModule_003E.smethod_28<string>(466496724u);
								}
								Vector3 pos = list3[0].JNKEKNOAPHO.GetPos();
								string text2 = text;
								text = text2 + global::_003CModule_003E.smethod_29<string>(358433514u) + Mathf.RoundToInt(pos.x) + global::_003CModule_003E.smethod_25<string>(1625195151u) + Mathf.RoundToInt(pos.y) + global::_003CModule_003E.smethod_26<string>(2331670864u) + Mathf.RoundToInt(pos.z) + global::_003CModule_003E.smethod_27<string>(1583484277u);
								DP.CD(text);
							}
						}
						else
						{
							if (!list2.Contains(list3[0]))
							{
								tjLsYT_0024xlAPMPH7Oek0spTI(n96ilJFY3oZo7_0024vgH1NCChk, list3[0], null, _0024LN7pf1eAoX0cI3qjp27Wzs++, list3, gSzvvc1hw7KDANGtzedjXXw);
							}
							else
							{
								tjLsYT_0024xlAPMPH7Oek0spTI(n96ilJFY3oZo7_0024vgH1NCChk, list3[0], null, _0024LN7pf1eAoX0cI3qjp27Wzs++, list3, null);
							}
							if (list3.Count == 0)
							{
								break;
							}
						}
						list3.RemoveAt(0);
					}
					SWqU_Q9z_2q3GFCdAn2er7M = SceneMan.JFAOKFIDAGK is Arena;
					n96ilJFY3oZo7_0024vgH1NCChk.MJBDKMNEKML = new Vector3[_0024LN7pf1eAoX0cI3qjp27Wzs];
					n96ilJFY3oZo7_0024vgH1NCChk.NNNBCKKNONF = new Vector3[_0024LN7pf1eAoX0cI3qjp27Wzs];
					n96ilJFY3oZo7_0024vgH1NCChk.NFOEKNHCNBM = new Vector3[_0024LN7pf1eAoX0cI3qjp27Wzs];
					n96ilJFY3oZo7_0024vgH1NCChk.PKBPJPCJAID = new int[_0024LN7pf1eAoX0cI3qjp27Wzs];
					WqeupfJ4Rcnn_002487y2EDHCh8 = 0;
					goto IL_047b;
				}
				case 1:
					SjlBM8inVA_YE4YVlr_0024gluY = -1;
					goto IL_04a5;
				case 2:
					SjlBM8inVA_YE4YVlr_0024gluY = -1;
					goto IL_053e;
				case 3:
					SjlBM8inVA_YE4YVlr_0024gluY = -3;
					goto IL_0b16;
				case 4:
					SjlBM8inVA_YE4YVlr_0024gluY = -1;
					goto IL_0c0a;
				case 5:
					SjlBM8inVA_YE4YVlr_0024gluY = -4;
					goto IL_0ce9;
				case 6:
					SjlBM8inVA_YE4YVlr_0024gluY = -1;
					Ik_q35lTrP8u6tO9BA3U_0024wU = true;
					yT7HpVIzmqW54W307WgJtr4 = null;
					SjlBM8inVA_YE4YVlr_0024gluY = 7;
					return true;
				case 7:
					{
						SjlBM8inVA_YE4YVlr_0024gluY = -1;
						return false;
					}
					IL_0bba:
					if (WqeupfJ4Rcnn_002487y2EDHCh8 < _0024LN7pf1eAoX0cI3qjp27Wzs)
					{
						if (int_0 <= 100)
						{
							int_0++;
							goto IL_0c0a;
						}
						int_0 = 0;
						yT7HpVIzmqW54W307WgJtr4 = null;
						SjlBM8inVA_YE4YVlr_0024gluY = 4;
						return true;
					}
					YGH6LpmmEtNyUILba0H0_zU = n96ilJFY3oZo7_0024vgH1NCChk.GEGOAHPBGKB.GetEnumerator();
					SjlBM8inVA_YE4YVlr_0024gluY = -4;
					goto IL_0caf;
					IL_053e:
					n96ilJFY3oZo7_0024vgH1NCChk.CJJJLCAHIFM[WqeupfJ4Rcnn_002487y2EDHCh8] = 999999;
					WqeupfJ4Rcnn_002487y2EDHCh8++;
					goto IL_0511;
					IL_047b:
					if (WqeupfJ4Rcnn_002487y2EDHCh8 < _0024LN7pf1eAoX0cI3qjp27Wzs)
					{
						if (int_0 <= 100)
						{
							int_0++;
							goto IL_04a5;
						}
						int_0 = 0;
						yT7HpVIzmqW54W307WgJtr4 = null;
						SjlBM8inVA_YE4YVlr_0024gluY = 1;
						return true;
					}
					n96ilJFY3oZo7_0024vgH1NCChk.CJJJLCAHIFM = new int[_0024LN7pf1eAoX0cI3qjp27Wzs];
					WqeupfJ4Rcnn_002487y2EDHCh8 = 1;
					goto IL_0511;
					IL_0c0a:
					if (n96ilJFY3oZo7_0024vgH1NCChk.PKBPJPCJAID[WqeupfJ4Rcnn_002487y2EDHCh8] <= -11111)
					{
						n96ilJFY3oZo7_0024vgH1NCChk.PKBPJPCJAID[WqeupfJ4Rcnn_002487y2EDHCh8] += 22222;
					}
					if (n96ilJFY3oZo7_0024vgH1NCChk.PKBPJPCJAID[WqeupfJ4Rcnn_002487y2EDHCh8] == -1)
					{
						n96ilJFY3oZo7_0024vgH1NCChk.GPONFKAJAHI.Add(WqeupfJ4Rcnn_002487y2EDHCh8);
					}
					WqeupfJ4Rcnn_002487y2EDHCh8++;
					goto IL_0bba;
					IL_0ce9:
					blockController2 = n96ilJFY3oZo7_0024vgH1NCChk.ODENHACMMKO(_0024V3vQpPHCxrElIm2DNCGf3VCOZ3GO6WK0Yazd5ChiAk6);
					if ((bool)blockController2)
					{
						_0024V3vQpPHCxrElIm2DNCGf3VCOZ3GO6WK0Yazd5ChiAk6.DCNIOOFAOMB = ((blockController2.ILFEIIFBHMP < 0 || _0024V3vQpPHCxrElIm2DNCGf3VCOZ3GO6WK0Yazd5ChiAk6.JNKEKNOAPHO.gid < 10) ? blockController2.DCNIOOFAOMB : blockController2.ILFEIIFBHMP);
					}
					_0024V3vQpPHCxrElIm2DNCGf3VCOZ3GO6WK0Yazd5ChiAk6 = null;
					goto IL_0caf;
					IL_04a5:
					n96ilJFY3oZo7_0024vgH1NCChk.PKBPJPCJAID[WqeupfJ4Rcnn_002487y2EDHCh8] = -1;
					WqeupfJ4Rcnn_002487y2EDHCh8++;
					goto IL_047b;
					IL_0caf:
					if (YGH6LpmmEtNyUILba0H0_zU.MoveNext())
					{
						_0024V3vQpPHCxrElIm2DNCGf3VCOZ3GO6WK0Yazd5ChiAk6 = YGH6LpmmEtNyUILba0H0_zU.Current;
						if (int_0 <= 100)
						{
							int_0++;
							goto IL_0ce9;
						}
						int_0 = 0;
						yT7HpVIzmqW54W307WgJtr4 = null;
						SjlBM8inVA_YE4YVlr_0024gluY = 5;
						return true;
					}
					IkXk87oS8XNDSZg4w7A9Tz0();
					YGH6LpmmEtNyUILba0H0_zU = default(List<BlockController>.Enumerator);
					IR1BlfUg0M3aZH8RXPUkABs = _0024LN7pf1eAoX0cI3qjp27Wzs;
					yT7HpVIzmqW54W307WgJtr4 = null;
					SjlBM8inVA_YE4YVlr_0024gluY = 6;
					return true;
					IL_0adc:
					if (YGH6LpmmEtNyUILba0H0_zU.MoveNext())
					{
						_0024V3vQpPHCxrElIm2DNCGf3VCOZ3GO6WK0Yazd5ChiAk6 = YGH6LpmmEtNyUILba0H0_zU.Current;
						if (int_0 <= 100)
						{
							int_0++;
							goto IL_0b16;
						}
						int_0 = 0;
						yT7HpVIzmqW54W307WgJtr4 = null;
						SjlBM8inVA_YE4YVlr_0024gluY = 3;
						return true;
					}
					ITybmnn_CCVC5Wu_0024wHlWVVQ();
					YGH6LpmmEtNyUILba0H0_zU = default(List<BlockController>.Enumerator);
					n96ilJFY3oZo7_0024vgH1NCChk.GPONFKAJAHI = new List<int>();
					WqeupfJ4Rcnn_002487y2EDHCh8 = 0;
					goto IL_0bba;
					IL_0511:
					if (WqeupfJ4Rcnn_002487y2EDHCh8 < _0024LN7pf1eAoX0cI3qjp27Wzs)
					{
						if (int_0 <= 100)
						{
							int_0++;
							goto IL_053e;
						}
						int_0 = 0;
						yT7HpVIzmqW54W307WgJtr4 = null;
						SjlBM8inVA_YE4YVlr_0024gluY = 2;
						return true;
					}
					n96ilJFY3oZo7_0024vgH1NCChk.AJKCGCCJHDJ = new BlockController[_0024LN7pf1eAoX0cI3qjp27Wzs];
					n96ilJFY3oZo7_0024vgH1NCChk.OPBKFDLBBNO = new BlockController[_0024LN7pf1eAoX0cI3qjp27Wzs];
					YGH6LpmmEtNyUILba0H0_zU = n96ilJFY3oZo7_0024vgH1NCChk.GEGOAHPBGKB.GetEnumerator();
					SjlBM8inVA_YE4YVlr_0024gluY = -3;
					goto IL_0adc;
					IL_0b16:
					if (_0024V3vQpPHCxrElIm2DNCGf3VCOZ3GO6WK0Yazd5ChiAk6.DCNIOOFAOMB >= 0)
					{
						BlockData jNKEKNOAPHO = _0024V3vQpPHCxrElIm2DNCGf3VCOZ3GO6WK0Yazd5ChiAk6.JNKEKNOAPHO;
						if (!gSzvvc1hw7KDANGtzedjXXw.Contains(_0024V3vQpPHCxrElIm2DNCGf3VCOZ3GO6WK0Yazd5ChiAk6))
						{
							if (!jNKEKNOAPHO.CheckMask(6407040L))
							{
								if (jNKEKNOAPHO.type == BlockData.AAHMDBHDCDK.Coupler && _0024V3vQpPHCxrElIm2DNCGf3VCOZ3GO6WK0Yazd5ChiAk6.ILFEIIFBHMP >= 0 && !_0024V3vQpPHCxrElIm2DNCGf3VCOZ3GO6WK0Yazd5ChiAk6.LJOAMOJGJIL)
								{
									if (n96ilJFY3oZo7_0024vgH1NCChk.PKBPJPCJAID[_0024V3vQpPHCxrElIm2DNCGf3VCOZ3GO6WK0Yazd5ChiAk6.DCNIOOFAOMB] == -1)
									{
										n96ilJFY3oZo7_0024vgH1NCChk.PKBPJPCJAID[_0024V3vQpPHCxrElIm2DNCGf3VCOZ3GO6WK0Yazd5ChiAk6.DCNIOOFAOMB] = _0024V3vQpPHCxrElIm2DNCGf3VCOZ3GO6WK0Yazd5ChiAk6.ILFEIIFBHMP;
										if (jNKEKNOAPHO.actionParam[7] == 0 && zMonjAsNtKI07jpCZ1IhOyF1DvuSjMmc3s0LTOX20RMA.ContainsKey(jNKEKNOAPHO.rgbI))
										{
											BlockData jNKEKNOAPHO2 = zMonjAsNtKI07jpCZ1IhOyF1DvuSjMmc3s0LTOX20RMA[jNKEKNOAPHO.rgbI].JNKEKNOAPHO;
											jNKEKNOAPHO.GetCouplerOffset(ref n96ilJFY3oZo7_0024vgH1NCChk.MJBDKMNEKML[_0024V3vQpPHCxrElIm2DNCGf3VCOZ3GO6WK0Yazd5ChiAk6.DCNIOOFAOMB], ref n96ilJFY3oZo7_0024vgH1NCChk.NNNBCKKNONF[_0024V3vQpPHCxrElIm2DNCGf3VCOZ3GO6WK0Yazd5ChiAk6.DCNIOOFAOMB], ref n96ilJFY3oZo7_0024vgH1NCChk.NFOEKNHCNBM[_0024V3vQpPHCxrElIm2DNCGf3VCOZ3GO6WK0Yazd5ChiAk6.DCNIOOFAOMB], jNKEKNOAPHO2);
										}
										else
										{
											jNKEKNOAPHO.GetCouplerOffset(ref n96ilJFY3oZo7_0024vgH1NCChk.MJBDKMNEKML[_0024V3vQpPHCxrElIm2DNCGf3VCOZ3GO6WK0Yazd5ChiAk6.DCNIOOFAOMB], ref n96ilJFY3oZo7_0024vgH1NCChk.NNNBCKKNONF[_0024V3vQpPHCxrElIm2DNCGf3VCOZ3GO6WK0Yazd5ChiAk6.DCNIOOFAOMB], ref n96ilJFY3oZo7_0024vgH1NCChk.NFOEKNHCNBM[_0024V3vQpPHCxrElIm2DNCGf3VCOZ3GO6WK0Yazd5ChiAk6.DCNIOOFAOMB], null);
										}
									}
									n96ilJFY3oZo7_0024vgH1NCChk.CJJJLCAHIFM[_0024V3vQpPHCxrElIm2DNCGf3VCOZ3GO6WK0Yazd5ChiAk6.DCNIOOFAOMB] = _0024V3vQpPHCxrElIm2DNCGf3VCOZ3GO6WK0Yazd5ChiAk6.ILFEIIFBHMP;
									n96ilJFY3oZo7_0024vgH1NCChk.AJKCGCCJHDJ[_0024V3vQpPHCxrElIm2DNCGf3VCOZ3GO6WK0Yazd5ChiAk6.DCNIOOFAOMB] = _0024V3vQpPHCxrElIm2DNCGf3VCOZ3GO6WK0Yazd5ChiAk6;
								}
							}
							else
							{
								if (jNKEKNOAPHO.CheckMask(2211840L) && n96ilJFY3oZo7_0024vgH1NCChk.PKBPJPCJAID[_0024V3vQpPHCxrElIm2DNCGf3VCOZ3GO6WK0Yazd5ChiAk6.DCNIOOFAOMB] < 0 && !_0024V3vQpPHCxrElIm2DNCGf3VCOZ3GO6WK0Yazd5ChiAk6.LJOAMOJGJIL && jNKEKNOAPHO.GetFixOffset(ref n96ilJFY3oZo7_0024vgH1NCChk.MJBDKMNEKML[_0024V3vQpPHCxrElIm2DNCGf3VCOZ3GO6WK0Yazd5ChiAk6.DCNIOOFAOMB], ref n96ilJFY3oZo7_0024vgH1NCChk.NNNBCKKNONF[_0024V3vQpPHCxrElIm2DNCGf3VCOZ3GO6WK0Yazd5ChiAk6.DCNIOOFAOMB], (_0024V3vQpPHCxrElIm2DNCGf3VCOZ3GO6WK0Yazd5ChiAk6 as JointController).EHICOGEOIIB))
								{
									n96ilJFY3oZo7_0024vgH1NCChk.PKBPJPCJAID[_0024V3vQpPHCxrElIm2DNCGf3VCOZ3GO6WK0Yazd5ChiAk6.DCNIOOFAOMB] = ((jNKEKNOAPHO.gid < 0) ? (_0024V3vQpPHCxrElIm2DNCGf3VCOZ3GO6WK0Yazd5ChiAk6.ILFEIIFBHMP - 22222) : _0024V3vQpPHCxrElIm2DNCGf3VCOZ3GO6WK0Yazd5ChiAk6.ILFEIIFBHMP);
								}
								if (n96ilJFY3oZo7_0024vgH1NCChk.CJJJLCAHIFM[_0024V3vQpPHCxrElIm2DNCGf3VCOZ3GO6WK0Yazd5ChiAk6.DCNIOOFAOMB] <= _0024V3vQpPHCxrElIm2DNCGf3VCOZ3GO6WK0Yazd5ChiAk6.ILFEIIFBHMP)
								{
									if ((n96ilJFY3oZo7_0024vgH1NCChk.CJJJLCAHIFM[_0024V3vQpPHCxrElIm2DNCGf3VCOZ3GO6WK0Yazd5ChiAk6.DCNIOOFAOMB] == _0024V3vQpPHCxrElIm2DNCGf3VCOZ3GO6WK0Yazd5ChiAk6.ILFEIIFBHMP) & SWqU_Q9z_2q3GFCdAn2er7M)
									{
										if (!n96ilJFY3oZo7_0024vgH1NCChk.OPBKFDLBBNO[_0024V3vQpPHCxrElIm2DNCGf3VCOZ3GO6WK0Yazd5ChiAk6.DCNIOOFAOMB])
										{
											n96ilJFY3oZo7_0024vgH1NCChk.OPBKFDLBBNO[_0024V3vQpPHCxrElIm2DNCGf3VCOZ3GO6WK0Yazd5ChiAk6.DCNIOOFAOMB] = _0024V3vQpPHCxrElIm2DNCGf3VCOZ3GO6WK0Yazd5ChiAk6;
										}
										else
										{
											int num = jNKEKNOAPHO.x + jNKEKNOAPHO.y + jNKEKNOAPHO.z;
											BlockData jNKEKNOAPHO3 = n96ilJFY3oZo7_0024vgH1NCChk.AJKCGCCJHDJ[_0024V3vQpPHCxrElIm2DNCGf3VCOZ3GO6WK0Yazd5ChiAk6.DCNIOOFAOMB].JNKEKNOAPHO;
											int num2 = jNKEKNOAPHO3.x + jNKEKNOAPHO3.y + jNKEKNOAPHO3.z;
											BlockData jNKEKNOAPHO4 = n96ilJFY3oZo7_0024vgH1NCChk.OPBKFDLBBNO[_0024V3vQpPHCxrElIm2DNCGf3VCOZ3GO6WK0Yazd5ChiAk6.DCNIOOFAOMB].JNKEKNOAPHO;
											int num3 = jNKEKNOAPHO4.x + jNKEKNOAPHO4.y + jNKEKNOAPHO4.z;
											if ((num < num2 && num2 < num3) || (num > num2 && num2 > num3))
											{
												n96ilJFY3oZo7_0024vgH1NCChk.AJKCGCCJHDJ[_0024V3vQpPHCxrElIm2DNCGf3VCOZ3GO6WK0Yazd5ChiAk6.DCNIOOFAOMB] = _0024V3vQpPHCxrElIm2DNCGf3VCOZ3GO6WK0Yazd5ChiAk6;
											}
											else if ((num < num3 && num3 < num2) || (num > num3 && num3 > num2))
											{
												n96ilJFY3oZo7_0024vgH1NCChk.OPBKFDLBBNO[_0024V3vQpPHCxrElIm2DNCGf3VCOZ3GO6WK0Yazd5ChiAk6.DCNIOOFAOMB] = _0024V3vQpPHCxrElIm2DNCGf3VCOZ3GO6WK0Yazd5ChiAk6;
											}
										}
									}
								}
								else
								{
									n96ilJFY3oZo7_0024vgH1NCChk.CJJJLCAHIFM[_0024V3vQpPHCxrElIm2DNCGf3VCOZ3GO6WK0Yazd5ChiAk6.DCNIOOFAOMB] = _0024V3vQpPHCxrElIm2DNCGf3VCOZ3GO6WK0Yazd5ChiAk6.ILFEIIFBHMP;
									n96ilJFY3oZo7_0024vgH1NCChk.AJKCGCCJHDJ[_0024V3vQpPHCxrElIm2DNCGf3VCOZ3GO6WK0Yazd5ChiAk6.DCNIOOFAOMB] = _0024V3vQpPHCxrElIm2DNCGf3VCOZ3GO6WK0Yazd5ChiAk6;
								}
							}
						}
						else
						{
							n96ilJFY3oZo7_0024vgH1NCChk.CJJJLCAHIFM[_0024V3vQpPHCxrElIm2DNCGf3VCOZ3GO6WK0Yazd5ChiAk6.DCNIOOFAOMB] = _0024V3vQpPHCxrElIm2DNCGf3VCOZ3GO6WK0Yazd5ChiAk6.ILFEIIFBHMP;
							n96ilJFY3oZo7_0024vgH1NCChk.AJKCGCCJHDJ[_0024V3vQpPHCxrElIm2DNCGf3VCOZ3GO6WK0Yazd5ChiAk6.DCNIOOFAOMB] = _0024V3vQpPHCxrElIm2DNCGf3VCOZ3GO6WK0Yazd5ChiAk6;
							n96ilJFY3oZo7_0024vgH1NCChk.CDOJFIKGAMA.Add(_0024V3vQpPHCxrElIm2DNCGf3VCOZ3GO6WK0Yazd5ChiAk6.DCNIOOFAOMB);
							n96ilJFY3oZo7_0024vgH1NCChk.CKILPAHPPOO.Add(jNKEKNOAPHO);
						}
					}
					_0024V3vQpPHCxrElIm2DNCGf3VCOZ3GO6WK0Yazd5ChiAk6 = null;
					goto IL_0adc;
				}
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
			((IDisposable)YGH6LpmmEtNyUILba0H0_zU/*cast due to .constrained prefix*/).Dispose();
		}

		private void IkXk87oS8XNDSZg4w7A9Tz0()
		{
			SjlBM8inVA_YE4YVlr_0024gluY = -1;
			((IDisposable)YGH6LpmmEtNyUILba0H0_zU/*cast due to .constrained prefix*/).Dispose();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw smethod_1();
		}

		internal static bool smethod_0(UnityEngine.Object object_0)
		{
			return object_0;
		}

		internal static NotSupportedException smethod_1()
		{
			return new NotSupportedException();
		}
	}

	private const int _0024WZs9P7HU_0024S_0024TJlEmJ3m2oo = 100;

	private static int IR1BlfUg0M3aZH8RXPUkABs;

	private static bool Ik_q35lTrP8u6tO9BA3U_0024wU;

	[HarmonyPrefix]
	internal static bool smethod_0(HIPBCCKFFAG __instance, bool LBOKOPEGKGE, bool LGBGNLDPMNN, ref List<GameObject> ___FHLICBAMEMC, ref List<int> ___HLAFDKCFFGD, ref List<BlockData> ___KLOGIIBKDEM, ref int ___KFAKAPMEKNG, ref int ___GAHICIOBMLK, ref bool[] ___CJCCLCFEBPA, ref Vector3[] ___EBKAHAMIBDD, ref bool ___JPIMCIGHPFH)
	{
		smethod_1((MonoBehaviour)MPatchr.xcBvxcM_0024ckBeZyvdSoAkJoM, FEeoV8jjl7nfTYJapv0_0024jiM(__instance, LBOKOPEGKGE, LGBGNLDPMNN, ___FHLICBAMEMC, ___HLAFDKCFFGD, ___KLOGIIBKDEM, ___KFAKAPMEKNG, ___GAHICIOBMLK, ___CJCCLCFEBPA, ___EBKAHAMIBDD, ___JPIMCIGHPFH));
		return false;
	}

	internal static IEnumerator FEeoV8jjl7nfTYJapv0_0024jiM(HIPBCCKFFAG fg, bool LBOKOPEGKGE, bool LGBGNLDPMNN, List<GameObject> FHLICBAMEMC, List<int> HLAFDKCFFGD, List<BlockData> KLOGIIBKDEM, int KFAKAPMEKNG, int GAHICIOBMLK, bool[] CJCCLCFEBPA, Vector3[] EBKAHAMIBDD, bool JPIMCIGHPFH, BuildData customBuildData = null)
	{
		int num = 0;
		fg.BICFNKNMDMO = (LBOKOPEGKGE ? 1 : 2);
		fg.HCMMJPFOIHD = true;
		fg.DHADJPNKHIJ = Vector3.up * _0024iZGQyDxLT4VIvWohGlnNVsELgW4XVz2ciE8bE7kdx1kUfj95VGLpEo7v0y_0024ce8Ebyd4Ks5D9VgsNugDhYKZjXMhVr_0024Ob9_7Melhr0HCLCuQb9E9gjnMK6yb4EYzjO7zw2IU9G80ZuaCPojstssHjf4.smethod_0(-999f, -99f);
		bool dNMNDAACAIH = PAEHEMJNPND.DNMNDAACAIH;
		BuildData buildData = ((!dNMNDAACAIH) ? _0024iZGQyDxLT4VIvWohGlnNVsELgW4XVz2ciE8bE7kdx1kUfj95VGLpEo7v0y_0024ce8Ebyd4Ks5D9VgsNugDhYKZjXMhVr_0024Ob9_7Melhr0HCLCuQb9E9gjnMK6yb4EYzjO7zw2IU9G80ZuaCPojstssHjf4.smethod_1(JKGKJLLFMLE.HHGILAIOCLG) : _0024iZGQyDxLT4VIvWohGlnNVsELgW4XVz2ciE8bE7kdx1kUfj95VGLpEo7v0y_0024ce8Ebyd4Ks5D9VgsNugDhYKZjXMhVr_0024Ob9_7Melhr0HCLCuQb9E9gjnMK6yb4EYzjO7zw2IU9G80ZuaCPojstssHjf4.smethod_1(Build.GFJLEEJELOL));
		if (customBuildData != null)
		{
			buildData = customBuildData;
		}
		for (int num2 = FHLICBAMEMC.Count - 1; num2 >= 0; num2--)
		{
			_0024iZGQyDxLT4VIvWohGlnNVsELgW4XVz2ciE8bE7kdx1kUfj95VGLpEo7v0y_0024ce8Ebyd4Ks5D9VgsNugDhYKZjXMhVr_0024Ob9_7Melhr0HCLCuQb9E9gjnMK6yb4EYzjO7zw2IU9G80ZuaCPojstssHjf4.smethod_2((UnityEngine.Object)FHLICBAMEMC[num2]);
		}
		FHLICBAMEMC.Clear();
		for (int num3 = fg.CLNMBHMCPGB.Count - 1; num3 >= 0; num3--)
		{
			_0024iZGQyDxLT4VIvWohGlnNVsELgW4XVz2ciE8bE7kdx1kUfj95VGLpEo7v0y_0024ce8Ebyd4Ks5D9VgsNugDhYKZjXMhVr_0024Ob9_7Melhr0HCLCuQb9E9gjnMK6yb4EYzjO7zw2IU9G80ZuaCPojstssHjf4.smethod_2((UnityEngine.Object)fg.CLNMBHMCPGB[num3]);
		}
		fg.CLNMBHMCPGB.Clear();
		_0024iZGQyDxLT4VIvWohGlnNVsELgW4XVz2ciE8bE7kdx1kUfj95VGLpEo7v0y_0024ce8Ebyd4Ks5D9VgsNugDhYKZjXMhVr_0024Ob9_7Melhr0HCLCuQb9E9gjnMK6yb4EYzjO7zw2IU9G80ZuaCPojstssHjf4.smethod_3(fg.EPGELCMKKOC, bool_0: false);
		fg.EPGELCMKKOC.CJBGADKMKIC = true;
		BuildData.LEGHEEKCJAF surfaceType = buildData.surfaceType;
		_0024iZGQyDxLT4VIvWohGlnNVsELgW4XVz2ciE8bE7kdx1kUfj95VGLpEo7v0y_0024ce8Ebyd4Ks5D9VgsNugDhYKZjXMhVr_0024Ob9_7Melhr0HCLCuQb9E9gjnMK6yb4EYzjO7zw2IU9G80ZuaCPojstssHjf4.smethod_4((surfaceType != BuildData.LEGHEEKCJAF.Auto) ? (surfaceType == BuildData.LEGHEEKCJAF.Flat) : JKGKJLLFMLE.IGOBPLOLHEP.isFlatSurface);
		_0024iZGQyDxLT4VIvWohGlnNVsELgW4XVz2ciE8bE7kdx1kUfj95VGLpEo7v0y_0024ce8Ebyd4Ks5D9VgsNugDhYKZjXMhVr_0024Ob9_7Melhr0HCLCuQb9E9gjnMK6yb4EYzjO7zw2IU9G80ZuaCPojstssHjf4.smethod_5(buildData.antiSSAO);
		PAEHEMJNPND.DNMNDAACAIH = false;
		HLAFDKCFFGD.Clear();
		KLOGIIBKDEM.Clear();
		List<BoxGenController> list = new List<BoxGenController>();
		List<ShaftController> list2 = new List<ShaftController>();
		List<StampController> list3 = new List<StampController>();
		List<int> list4 = new List<int>();
		List<BlockData.AAHMDBHDCDK> list5 = new List<BlockData.AAHMDBHDCDK>();
		int count = buildData.blockData.Count;
		for (int i = 0; i < count; i++)
		{
			if (num <= 100)
			{
				num++;
			}
			else
			{
				num = 0;
				yield return null;
			}
			BlockData blockData = buildData.blockData[i];
			GameObject gameObject = _0024iZGQyDxLT4VIvWohGlnNVsELgW4XVz2ciE8bE7kdx1kUfj95VGLpEo7v0y_0024ce8Ebyd4Ks5D9VgsNugDhYKZjXMhVr_0024Ob9_7Melhr0HCLCuQb9E9gjnMK6yb4EYzjO7zw2IU9G80ZuaCPojstssHjf4.smethod_6(blockData, LGBGNLDPMNN);
			if (_0024iZGQyDxLT4VIvWohGlnNVsELgW4XVz2ciE8bE7kdx1kUfj95VGLpEo7v0y_0024ce8Ebyd4Ks5D9VgsNugDhYKZjXMhVr_0024Ob9_7Melhr0HCLCuQb9E9gjnMK6yb4EYzjO7zw2IU9G80ZuaCPojstssHjf4.smethod_7((UnityEngine.Object)gameObject, (UnityEngine.Object)null))
			{
				continue;
			}
			if (blockData.type != BlockData.AAHMDBHDCDK.Chassis)
			{
				if (!_0024iZGQyDxLT4VIvWohGlnNVsELgW4XVz2ciE8bE7kdx1kUfj95VGLpEo7v0y_0024ce8Ebyd4Ks5D9VgsNugDhYKZjXMhVr_0024Ob9_7Melhr0HCLCuQb9E9gjnMK6yb4EYzjO7zw2IU9G80ZuaCPojstssHjf4.smethod_14(blockData, 6407040L))
				{
					if (blockData.type == BlockData.AAHMDBHDCDK.BoxGen)
					{
						list.Add(gameObject.GetComponent<BoxGenController>());
						HLAFDKCFFGD.Add(i);
						KLOGIIBKDEM.Add(blockData);
					}
					else if (blockData.type == BlockData.AAHMDBHDCDK.CapGen)
					{
						_0024iZGQyDxLT4VIvWohGlnNVsELgW4XVz2ciE8bE7kdx1kUfj95VGLpEo7v0y_0024ce8Ebyd4Ks5D9VgsNugDhYKZjXMhVr_0024Ob9_7Melhr0HCLCuQb9E9gjnMK6yb4EYzjO7zw2IU9G80ZuaCPojstssHjf4.smethod_15(gameObject.GetComponent<CapGenController>(), (GameObject)null);
						_0024iZGQyDxLT4VIvWohGlnNVsELgW4XVz2ciE8bE7kdx1kUfj95VGLpEo7v0y_0024ce8Ebyd4Ks5D9VgsNugDhYKZjXMhVr_0024Ob9_7Melhr0HCLCuQb9E9gjnMK6yb4EYzjO7zw2IU9G80ZuaCPojstssHjf4.smethod_16((Renderer)gameObject.GetComponent<MeshRenderer>(), bool_0: false);
						_0024iZGQyDxLT4VIvWohGlnNVsELgW4XVz2ciE8bE7kdx1kUfj95VGLpEo7v0y_0024ce8Ebyd4Ks5D9VgsNugDhYKZjXMhVr_0024Ob9_7Melhr0HCLCuQb9E9gjnMK6yb4EYzjO7zw2IU9G80ZuaCPojstssHjf4.smethod_2((UnityEngine.Object)gameObject.GetComponent<Collider>());
						HLAFDKCFFGD.Add(i);
						KLOGIIBKDEM.Add(blockData);
					}
					else if (blockData.type != BlockData.AAHMDBHDCDK.Coupler)
					{
						if (blockData.type != BlockData.AAHMDBHDCDK.Wheel)
						{
							if (blockData.type != BlockData.AAHMDBHDCDK.Shaft)
							{
								if (_0024iZGQyDxLT4VIvWohGlnNVsELgW4XVz2ciE8bE7kdx1kUfj95VGLpEo7v0y_0024ce8Ebyd4Ks5D9VgsNugDhYKZjXMhVr_0024Ob9_7Melhr0HCLCuQb9E9gjnMK6yb4EYzjO7zw2IU9G80ZuaCPojstssHjf4.smethod_14(blockData, 12884901888L))
								{
									list3.Add(gameObject.GetComponent<StampController>());
								}
							}
							else
							{
								list2.Add(gameObject.GetComponent<ShaftController>());
							}
						}
						else
						{
							_0024iZGQyDxLT4VIvWohGlnNVsELgW4XVz2ciE8bE7kdx1kUfj95VGLpEo7v0y_0024ce8Ebyd4Ks5D9VgsNugDhYKZjXMhVr_0024Ob9_7Melhr0HCLCuQb9E9gjnMK6yb4EYzjO7zw2IU9G80ZuaCPojstssHjf4.smethod_17(gameObject.GetComponent<WheelController>(), (GameObject)null);
						}
					}
					else
					{
						HLAFDKCFFGD.Add(i);
						KLOGIIBKDEM.Add(blockData);
					}
				}
				else if (blockData.gid != 7)
				{
					if (LBOKOPEGKGE)
					{
						if (_0024iZGQyDxLT4VIvWohGlnNVsELgW4XVz2ciE8bE7kdx1kUfj95VGLpEo7v0y_0024ce8Ebyd4Ks5D9VgsNugDhYKZjXMhVr_0024Ob9_7Melhr0HCLCuQb9E9gjnMK6yb4EYzjO7zw2IU9G80ZuaCPojstssHjf4.smethod_14(blockData, 4195200L))
						{
							switch (blockData.type)
							{
							case BlockData.AAHMDBHDCDK.PistonS:
								blockData.type = BlockData.AAHMDBHDCDK.PistonL;
								break;
							case BlockData.AAHMDBHDCDK.JointTS:
								blockData.type = BlockData.AAHMDBHDCDK.JointTA;
								break;
							case BlockData.AAHMDBHDCDK.JointPS:
								blockData.type = BlockData.AAHMDBHDCDK.JointPA;
								break;
							case BlockData.AAHMDBHDCDK.JointBS:
								blockData.type = BlockData.AAHMDBHDCDK.JointBA;
								break;
							}
							int num4 = 0;
							for (int j = 0; j < 8; j++)
							{
								if (blockData.actionID[j] == 70)
								{
									num4 = blockData.actionParam[j];
								}
							}
							blockData.actionID[0] = 60;
							blockData.actionParam[0] = num4;
							blockData.actionID[1] = -1;
							blockData.actionParam[1] = 0;
						}
						else if (blockData.type == BlockData.AAHMDBHDCDK.PistonL && blockData.gid != 7)
						{
							bool flag = false;
							for (int k = 0; k < 8; k++)
							{
								if (flag)
								{
									blockData.actionID[k] = -1;
									blockData.actionParam[k] = 0;
								}
								else if (blockData.actionID[k] == 60)
								{
									flag = true;
								}
							}
						}
						blockData.gid = 7;
						if (_0024iZGQyDxLT4VIvWohGlnNVsELgW4XVz2ciE8bE7kdx1kUfj95VGLpEo7v0y_0024ce8Ebyd4Ks5D9VgsNugDhYKZjXMhVr_0024Ob9_7Melhr0HCLCuQb9E9gjnMK6yb4EYzjO7zw2IU9G80ZuaCPojstssHjf4.smethod_14(blockData, 98304L))
						{
							for (int l = 0; l < 8; l++)
							{
								if (blockData.actionID[l] == 60 && blockData.actionParam[l] == -91)
								{
									blockData.gid = -1;
									break;
								}
							}
						}
					}
					else
					{
						list4.Add(i);
						list5.Add(blockData.type);
						blockData.type = BlockData.AAHMDBHDCDK.Cannon1;
						blockData.gid = 7;
					}
				}
				else
				{
					HLAFDKCFFGD.Add(i);
					KLOGIIBKDEM.Add(blockData);
				}
			}
			else
			{
				if (_0024iZGQyDxLT4VIvWohGlnNVsELgW4XVz2ciE8bE7kdx1kUfj95VGLpEo7v0y_0024ce8Ebyd4Ks5D9VgsNugDhYKZjXMhVr_0024Ob9_7Melhr0HCLCuQb9E9gjnMK6yb4EYzjO7zw2IU9G80ZuaCPojstssHjf4.smethod_9(_0024iZGQyDxLT4VIvWohGlnNVsELgW4XVz2ciE8bE7kdx1kUfj95VGLpEo7v0y_0024ce8Ebyd4Ks5D9VgsNugDhYKZjXMhVr_0024Ob9_7Melhr0HCLCuQb9E9gjnMK6yb4EYzjO7zw2IU9G80ZuaCPojstssHjf4.smethod_8(gameObject)) == 1)
				{
					_0024iZGQyDxLT4VIvWohGlnNVsELgW4XVz2ciE8bE7kdx1kUfj95VGLpEo7v0y_0024ce8Ebyd4Ks5D9VgsNugDhYKZjXMhVr_0024Ob9_7Melhr0HCLCuQb9E9gjnMK6yb4EYzjO7zw2IU9G80ZuaCPojstssHjf4.smethod_2((UnityEngine.Object)_0024iZGQyDxLT4VIvWohGlnNVsELgW4XVz2ciE8bE7kdx1kUfj95VGLpEo7v0y_0024ce8Ebyd4Ks5D9VgsNugDhYKZjXMhVr_0024Ob9_7Melhr0HCLCuQb9E9gjnMK6yb4EYzjO7zw2IU9G80ZuaCPojstssHjf4.smethod_11((Component)_0024iZGQyDxLT4VIvWohGlnNVsELgW4XVz2ciE8bE7kdx1kUfj95VGLpEo7v0y_0024ce8Ebyd4Ks5D9VgsNugDhYKZjXMhVr_0024Ob9_7Melhr0HCLCuQb9E9gjnMK6yb4EYzjO7zw2IU9G80ZuaCPojstssHjf4.smethod_10(_0024iZGQyDxLT4VIvWohGlnNVsELgW4XVz2ciE8bE7kdx1kUfj95VGLpEo7v0y_0024ce8Ebyd4Ks5D9VgsNugDhYKZjXMhVr_0024Ob9_7Melhr0HCLCuQb9E9gjnMK6yb4EYzjO7zw2IU9G80ZuaCPojstssHjf4.smethod_8(gameObject), 0)));
				}
				if (blockData.press >> 6 != 0)
				{
					Collider component = gameObject.GetComponent<Collider>();
					if (_0024iZGQyDxLT4VIvWohGlnNVsELgW4XVz2ciE8bE7kdx1kUfj95VGLpEo7v0y_0024ce8Ebyd4Ks5D9VgsNugDhYKZjXMhVr_0024Ob9_7Melhr0HCLCuQb9E9gjnMK6yb4EYzjO7zw2IU9G80ZuaCPojstssHjf4.smethod_12((UnityEngine.Object)component))
					{
						_0024iZGQyDxLT4VIvWohGlnNVsELgW4XVz2ciE8bE7kdx1kUfj95VGLpEo7v0y_0024ce8Ebyd4Ks5D9VgsNugDhYKZjXMhVr_0024Ob9_7Melhr0HCLCuQb9E9gjnMK6yb4EYzjO7zw2IU9G80ZuaCPojstssHjf4.smethod_13(component, bool_0: false);
					}
				}
			}
			FHLICBAMEMC.Add(gameObject);
			_0024iZGQyDxLT4VIvWohGlnNVsELgW4XVz2ciE8bE7kdx1kUfj95VGLpEo7v0y_0024ce8Ebyd4Ks5D9VgsNugDhYKZjXMhVr_0024Ob9_7Melhr0HCLCuQb9E9gjnMK6yb4EYzjO7zw2IU9G80ZuaCPojstssHjf4.smethod_18(fg.EPGELCMKKOC, gameObject.GetComponent<BlockController>());
		}
		count = FHLICBAMEMC.Count;
		PAEHEMJNPND.DNMNDAACAIH = dNMNDAACAIH;
		_0024iZGQyDxLT4VIvWohGlnNVsELgW4XVz2ciE8bE7kdx1kUfj95VGLpEo7v0y_0024ce8Ebyd4Ks5D9VgsNugDhYKZjXMhVr_0024Ob9_7Melhr0HCLCuQb9E9gjnMK6yb4EYzjO7zw2IU9G80ZuaCPojstssHjf4.smethod_19(fg.EPGELCMKKOC, (BlockController)null);
		int num5 = _0024iZGQyDxLT4VIvWohGlnNVsELgW4XVz2ciE8bE7kdx1kUfj95VGLpEo7v0y_0024ce8Ebyd4Ks5D9VgsNugDhYKZjXMhVr_0024Ob9_7Melhr0HCLCuQb9E9gjnMK6yb4EYzjO7zw2IU9G80ZuaCPojstssHjf4.smethod_20(fg.EPGELCMKKOC);
		for (int i = 0; i < num5; i++)
		{
			if (num <= 100)
			{
				num++;
			}
			else
			{
				num = 0;
				yield return null;
			}
			GameObject gameObject2 = _0024iZGQyDxLT4VIvWohGlnNVsELgW4XVz2ciE8bE7kdx1kUfj95VGLpEo7v0y_0024ce8Ebyd4Ks5D9VgsNugDhYKZjXMhVr_0024Ob9_7Melhr0HCLCuQb9E9gjnMK6yb4EYzjO7zw2IU9G80ZuaCPojstssHjf4.smethod_21();
			gameObject2.name = global::_003CModule_003E.smethod_28<string>(2301957451u) + i;
			fg.CLNMBHMCPGB.Add(gameObject2);
		}
		for (int i = list4.Count - 1; i >= 0; i--)
		{
			if (num <= 100)
			{
				num++;
			}
			else
			{
				num = 0;
				yield return null;
			}
			BlockData jNKEKNOAPHO = FHLICBAMEMC[list4[i]].GetComponent<BlockController>().JNKEKNOAPHO;
			jNKEKNOAPHO.type = list5[i];
			jNKEKNOAPHO.gid = 0;
		}
		fg.EPGELCMKKOC.DONCHDIDFEE();
		foreach (BoxGenController item in list)
		{
			if (num <= 100)
			{
				num++;
			}
			else
			{
				num = 0;
				yield return null;
			}
			item.GetComponent<MeshRenderer>().enabled = false;
			item.MakeBox();
			item.MakeCollider();
			item.NGLBLAGMBLN.SetActive(value: true);
			UnityEngine.Object.Destroy(item.gameObject.GetComponent<Collider>());
		}
		if ((bool)fg.LMNBBEDPNCH)
		{
			bool flag2 = true;
			foreach (StampController item2 in list3)
			{
				if (num <= 100)
				{
					num++;
				}
				else
				{
					num = 0;
					yield return null;
				}
				BlockData jNKEKNOAPHO2 = item2.JNKEKNOAPHO;
				int i = ((jNKEKNOAPHO2.type != BlockData.AAHMDBHDCDK.StampF) ? 72 : 60);
				for (int m = 0; m < 8; m++)
				{
					if (num <= 100)
					{
						num++;
					}
					else
					{
						num = 0;
						yield return null;
					}
					if (jNKEKNOAPHO2.actionID[m] == i)
					{
						item2.Bake(fg.EPGELCMKKOC, jNKEKNOAPHO2.actionParam[m], KFAKAPMEKNG, GAHICIOBMLK, fg.AJKPKECMDIJ);
						if (flag2 && (bool)item2.HDKLPEHKJNA)
						{
							item2.HDKLPEHKJNA.GetComponent<MeshRenderer>().sharedMaterial.mainTexture = fg.LMNBBEDPNCH;
							flag2 = false;
						}
						break;
					}
				}
			}
		}
		foreach (ShaftController item3 in list2)
		{
			if (num <= 100)
			{
				num++;
			}
			else
			{
				num = 0;
				yield return null;
			}
			item3.MakeWing();
		}
		long mask = long.MaxValue;
		if (fg.KHMJJPMFENJ != 1)
		{
			if (fg.KHMJJPMFENJ == 2)
			{
				mask = JKGKJLLFMLE.IGOBPLOLHEP.showMask2;
			}
		}
		else
		{
			mask = JKGKJLLFMLE.IGOBPLOLHEP.showMask1;
		}
		for (int i = FHLICBAMEMC.Count - 1; i >= 0; i--)
		{
			if (num <= 100)
			{
				num++;
			}
			else
			{
				num = 0;
				yield return null;
			}
			BlockController component2 = FHLICBAMEMC[i].GetComponent<BlockController>();
			if (!component2.JNKEKNOAPHO.CheckMask(mask))
			{
				component2.HideBlock();
			}
		}
		fg.PDDNDFNLDDG = new BlockController[num5];
		CJCCLCFEBPA = new bool[num5];
		EBKAHAMIBDD = new Vector3[num5];
		Vector3[] array = new Vector3[num5];
		int[] array2 = new int[num5];
		for (int i = 0; i < count; i++)
		{
			if (num <= 100)
			{
				num++;
			}
			else
			{
				num = 0;
				yield return null;
			}
			BlockController component3 = FHLICBAMEMC[i].GetComponent<BlockController>();
			BlockData jNKEKNOAPHO3 = component3.JNKEKNOAPHO;
			if (component3.MKHLOPIFNDI || component3.DCNIOOFAOMB < 0 || jNKEKNOAPHO3.CheckMask(12884901888L))
			{
				FHLICBAMEMC[i].SetActive(value: false);
				continue;
			}
			if (jNKEKNOAPHO3.CheckMask(543277952L) && component3.DCNIOOFAOMB > 0 && component3.ILFEIIFBHMP >= 0 && (jNKEKNOAPHO3.type == BlockData.AAHMDBHDCDK.Coupler || jNKEKNOAPHO3.gid == 7))
			{
				array[component3.DCNIOOFAOMB] += jNKEKNOAPHO3.GetPos();
				array2[component3.DCNIOOFAOMB]++;
			}
			if (jNKEKNOAPHO3.CheckMask(17666539541L) && (jNKEKNOAPHO3.rgbI & 0x1000000) != 0)
			{
				FHLICBAMEMC[i].SetActive(value: false);
				continue;
			}
			if (component3.DCNIOOFAOMB > 0 && jNKEKNOAPHO3.gid == 7 && jNKEKNOAPHO3.type == BlockData.AAHMDBHDCDK.Chassis && jNKEKNOAPHO3.press >> 6 == 0)
			{
				CJCCLCFEBPA[component3.DCNIOOFAOMB] = true;
			}
			if ((jNKEKNOAPHO3.rgbI == 16777216 || jNKEKNOAPHO3.type == BlockData.AAHMDBHDCDK.Coupler) && !jNKEKNOAPHO3.CheckMask(-1073741808L))
			{
				FHLICBAMEMC[i].SetActive(value: false);
				if (jNKEKNOAPHO3.type != BlockData.AAHMDBHDCDK.Coupler || component3.ILFEIIFBHMP < 0)
				{
					continue;
				}
				fg.PDDNDFNLDDG[component3.DCNIOOFAOMB] = component3;
			}
			if (jNKEKNOAPHO3.gid == 7 && jNKEKNOAPHO3.CheckMask(6407040L))
			{
				JointController component4 = component3.GetComponent<JointController>();
				component4.DivideMesh(fg.CLNMBHMCPGB[component3.ILFEIIFBHMP], MCKBICGHHOK: true);
				if (jNKEKNOAPHO3.type == BlockData.AAHMDBHDCDK.PistonL)
				{
					(component4 as PistonController).MakeFakeArm();
				}
			}
			FHLICBAMEMC[i].transform.parent = fg.CLNMBHMCPGB[component3.DCNIOOFAOMB].transform;
			component3.LJOAMOJGJIL = false;
		}
		Vector3[] array3 = new Vector3[num5];
		for (int i = num5 - 1; i > 0; i--)
		{
			if (num <= 100)
			{
				num++;
			}
			else
			{
				num = 0;
				yield return null;
			}
			if (fg.EPGELCMKKOC.PKBPJPCJAID[i] < 0)
			{
				if (LGBGNLDPMNN)
				{
					fg.CLNMBHMCPGB[i].SetActive(value: false);
				}
			}
			else
			{
				Transform transform = fg.CLNMBHMCPGB[i].transform;
				transform.parent = fg.CLNMBHMCPGB[fg.EPGELCMKKOC.PKBPJPCJAID[i]].transform;
				transform.localRotation = BDLEJBBJJOI.INECOALCJIE(BDLEJBBJJOI.GKCKPLGPDFK(fg.EPGELCMKKOC.MJBDKMNEKML[i]));
				transform.localPosition = fg.EPGELCMKKOC.NNNBCKKNONF[i];
				if (CJCCLCFEBPA[i])
				{
					transform.localScale *= 0.998f;
					if (array2[i] == 0)
					{
						DP.CD(global::_003CModule_003E.smethod_28<string>(2028777748u) + i + global::_003CModule_003E.smethod_26<string>(3043991974u));
						array2[i] = 1;
					}
					EBKAHAMIBDD[i] = array[i] / array2[i];
					array3[i] = (EBKAHAMIBDD[i] - transform.position) * 0.002f;
				}
			}
		}
		for (int i = num5 - 1; i >= 0; i--)
		{
			if (num <= 100)
			{
				num++;
			}
			else
			{
				num = 0;
				yield return null;
			}
			Transform transform2 = fg.CLNMBHMCPGB[i].transform;
			if (!(transform2.lossyScale.x < 0.99f))
			{
				transform2.position += array3[i];
			}
			else
			{
				transform2.localScale = Vector3.one;
				CJCCLCFEBPA[i] = false;
			}
		}
		JPIMCIGHPFH = false;
		fg.BKMCJDEEDJD();
		if (fg.CLNMBHMCPGB.Count > 0)
		{
			FJLJNEKHKKH.DMADFEPLJNP(fg.CLNMBHMCPGB[0].transform, 9);
		}
		PAEHEMJNPND.CAMDMHABENK(BHCKMFDEBBH: false);
	}

	private static IEnumerator se37pnRAhSVnzxajE7UOBOk(HDBLLPODNLN selfObj)
	{
		MPatchr.ShowDebugMsg(global::_003CModule_003E.smethod_25<string>(3109292137u));
		Dictionary<int, CouplerController> dictionary = Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<Dictionary<int, CouplerController>>(global::_003CModule_003E.smethod_26<string>(596572655u), selfObj);
		List<int> list = Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<List<int>>(global::_003CModule_003E.smethod_25<string>(2012908250u), selfObj);
		Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<List<BlockController>>(global::_003CModule_003E.smethod_25<string>(2241670438u), selfObj);
		BlockController blockController = Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<BlockController>(global::_003CModule_003E.smethod_25<string>(3206170754u), selfObj);
		Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<int>(global::_003CModule_003E.smethod_28<string>(1938607089u), selfObj);
		int num = 0;
		foreach (BlockController item in selfObj.GEGOAHPBGKB)
		{
			item.DCNIOOFAOMB = -1;
			item.ILFEIIFBHMP = -1;
			item.LJOAMOJGJIL = false;
		}
		dictionary.Clear();
		list.Clear();
		selfObj.CDOJFIKGAMA = new List<int>();
		selfObj.CKILPAHPPOO = new List<BlockData>();
		selfObj.FKLBCLHKNML = new Dictionary<BlockData, BlockData>();
		List<BlockController> list2 = new List<BlockController>();
		List<BlockController> list3 = new List<BlockController>();
		List<BlockController> list4 = new List<BlockController>();
		tjLsYT_0024xlAPMPH7Oek0spTI(selfObj, blockController, null, 0, list3, list4);
		bool flag = SceneMan.JFAOKFIDAGK is Build;
		int num2 = (Aj9rkvkmT0_0024GQciqtMEiLoWD5a_0024gKbx4KymieNRH_0024O0RHbyp2pFsaZFdtjgqV3_0024nbr6iYsaYKTiLAj2fMo3A0wDxDqA5Lb6dGhkXOcsWSsw1jB4qw3H977wjw_0024ZauG1Li9w8ETLpQtyep8f_0024wttCu2o.smethod_0((UnityEngine.Object)blockController) ? 1 : 0);
		for (int i = 0; i < 9999; i++)
		{
			if (i > 9990)
			{
				DP.D(global::_003CModule_003E.smethod_28<string>(1574367485u) + i);
			}
			if (list3.Count == 0)
			{
				break;
			}
			if (list3[0].DCNIOOFAOMB >= 0)
			{
				if (!flag)
				{
					selfObj.GEGOAHPBGKB.Remove(list3[0]);
				}
				else
				{
					list3[0].LJOAMOJGJIL = true;
				}
				if (HNJDDKJLHMM.FHLGOMHPDLN == HNJDDKJLHMM.HKGAACMIPIH.Offline)
				{
					string text = global::_003CModule_003E.smethod_28<string>(3850865010u);
					if (HelpDefs.isJ)
					{
						text += global::_003CModule_003E.smethod_26<string>(571908725u);
						text += list3[0].JNKEKNOAPHO.type;
						text += global::_003CModule_003E.smethod_26<string>(4142014077u);
					}
					else
					{
						text += global::_003CModule_003E.smethod_26<string>(7430500u);
						text += list3[0].JNKEKNOAPHO.type;
						text += global::_003CModule_003E.smethod_28<string>(466496724u);
					}
					Vector3 pos = list3[0].JNKEKNOAPHO.GetPos();
					string text2 = text;
					text = text2 + global::_003CModule_003E.smethod_29<string>(358433514u) + Mathf.RoundToInt(pos.x) + global::_003CModule_003E.smethod_25<string>(1625195151u) + Mathf.RoundToInt(pos.y) + global::_003CModule_003E.smethod_26<string>(2331670864u) + Mathf.RoundToInt(pos.z) + global::_003CModule_003E.smethod_27<string>(1583484277u);
					DP.CD(text);
				}
			}
			else
			{
				if (!list2.Contains(list3[0]))
				{
					tjLsYT_0024xlAPMPH7Oek0spTI(selfObj, list3[0], null, num2++, list3, list4);
				}
				else
				{
					tjLsYT_0024xlAPMPH7Oek0spTI(selfObj, list3[0], null, num2++, list3, null);
				}
				if (list3.Count == 0)
				{
					break;
				}
			}
			list3.RemoveAt(0);
		}
		bool flag2 = SceneMan.JFAOKFIDAGK is Arena;
		selfObj.MJBDKMNEKML = new Vector3[num2];
		selfObj.NNNBCKKNONF = new Vector3[num2];
		selfObj.NFOEKNHCNBM = new Vector3[num2];
		selfObj.PKBPJPCJAID = new int[num2];
		for (int j = 0; j < num2; j++)
		{
			if (num <= 100)
			{
				num++;
			}
			else
			{
				num = 0;
				yield return null;
			}
			selfObj.PKBPJPCJAID[j] = -1;
		}
		selfObj.CJJJLCAHIFM = new int[num2];
		for (int j = 1; j < num2; j++)
		{
			if (num <= 100)
			{
				num++;
			}
			else
			{
				num = 0;
				yield return null;
			}
			selfObj.CJJJLCAHIFM[j] = 999999;
		}
		selfObj.AJKCGCCJHDJ = new BlockController[num2];
		selfObj.OPBKFDLBBNO = new BlockController[num2];
		foreach (BlockController item2 in selfObj.GEGOAHPBGKB)
		{
			if (num <= 100)
			{
				num++;
			}
			else
			{
				num = 0;
				yield return null;
			}
			if (item2.DCNIOOFAOMB < 0)
			{
				continue;
			}
			BlockData jNKEKNOAPHO = item2.JNKEKNOAPHO;
			if (!list4.Contains(item2))
			{
				if (!jNKEKNOAPHO.CheckMask(6407040L))
				{
					if (jNKEKNOAPHO.type != BlockData.AAHMDBHDCDK.Coupler || item2.ILFEIIFBHMP < 0 || item2.LJOAMOJGJIL)
					{
						continue;
					}
					if (selfObj.PKBPJPCJAID[item2.DCNIOOFAOMB] == -1)
					{
						selfObj.PKBPJPCJAID[item2.DCNIOOFAOMB] = item2.ILFEIIFBHMP;
						if (jNKEKNOAPHO.actionParam[7] == 0 && dictionary.ContainsKey(jNKEKNOAPHO.rgbI))
						{
							BlockData jNKEKNOAPHO2 = dictionary[jNKEKNOAPHO.rgbI].JNKEKNOAPHO;
							jNKEKNOAPHO.GetCouplerOffset(ref selfObj.MJBDKMNEKML[item2.DCNIOOFAOMB], ref selfObj.NNNBCKKNONF[item2.DCNIOOFAOMB], ref selfObj.NFOEKNHCNBM[item2.DCNIOOFAOMB], jNKEKNOAPHO2);
						}
						else
						{
							jNKEKNOAPHO.GetCouplerOffset(ref selfObj.MJBDKMNEKML[item2.DCNIOOFAOMB], ref selfObj.NNNBCKKNONF[item2.DCNIOOFAOMB], ref selfObj.NFOEKNHCNBM[item2.DCNIOOFAOMB], null);
						}
					}
					selfObj.CJJJLCAHIFM[item2.DCNIOOFAOMB] = item2.ILFEIIFBHMP;
					selfObj.AJKCGCCJHDJ[item2.DCNIOOFAOMB] = item2;
					continue;
				}
				if (jNKEKNOAPHO.CheckMask(2211840L) && selfObj.PKBPJPCJAID[item2.DCNIOOFAOMB] < 0 && !item2.LJOAMOJGJIL && jNKEKNOAPHO.GetFixOffset(ref selfObj.MJBDKMNEKML[item2.DCNIOOFAOMB], ref selfObj.NNNBCKKNONF[item2.DCNIOOFAOMB], (item2 as JointController).EHICOGEOIIB))
				{
					selfObj.PKBPJPCJAID[item2.DCNIOOFAOMB] = ((jNKEKNOAPHO.gid < 0) ? (item2.ILFEIIFBHMP - 22222) : item2.ILFEIIFBHMP);
				}
				if (selfObj.CJJJLCAHIFM[item2.DCNIOOFAOMB] <= item2.ILFEIIFBHMP)
				{
					if (!(selfObj.CJJJLCAHIFM[item2.DCNIOOFAOMB] == item2.ILFEIIFBHMP && flag2))
					{
						continue;
					}
					if (!selfObj.OPBKFDLBBNO[item2.DCNIOOFAOMB])
					{
						selfObj.OPBKFDLBBNO[item2.DCNIOOFAOMB] = item2;
						continue;
					}
					int num3 = jNKEKNOAPHO.x + jNKEKNOAPHO.y + jNKEKNOAPHO.z;
					BlockData jNKEKNOAPHO3 = selfObj.AJKCGCCJHDJ[item2.DCNIOOFAOMB].JNKEKNOAPHO;
					int num4 = jNKEKNOAPHO3.x + jNKEKNOAPHO3.y + jNKEKNOAPHO3.z;
					BlockData jNKEKNOAPHO4 = selfObj.OPBKFDLBBNO[item2.DCNIOOFAOMB].JNKEKNOAPHO;
					int num5 = jNKEKNOAPHO4.x + jNKEKNOAPHO4.y + jNKEKNOAPHO4.z;
					if ((num3 < num4 && num4 < num5) || (num3 > num4 && num4 > num5))
					{
						selfObj.AJKCGCCJHDJ[item2.DCNIOOFAOMB] = item2;
					}
					else if ((num3 < num5 && num5 < num4) || (num3 > num5 && num5 > num4))
					{
						selfObj.OPBKFDLBBNO[item2.DCNIOOFAOMB] = item2;
					}
				}
				else
				{
					selfObj.CJJJLCAHIFM[item2.DCNIOOFAOMB] = item2.ILFEIIFBHMP;
					selfObj.AJKCGCCJHDJ[item2.DCNIOOFAOMB] = item2;
				}
			}
			else
			{
				selfObj.CJJJLCAHIFM[item2.DCNIOOFAOMB] = item2.ILFEIIFBHMP;
				selfObj.AJKCGCCJHDJ[item2.DCNIOOFAOMB] = item2;
				selfObj.CDOJFIKGAMA.Add(item2.DCNIOOFAOMB);
				selfObj.CKILPAHPPOO.Add(jNKEKNOAPHO);
			}
		}
		selfObj.GPONFKAJAHI = new List<int>();
		for (int j = 0; j < num2; j++)
		{
			if (num <= 100)
			{
				num++;
			}
			else
			{
				num = 0;
				yield return null;
			}
			if (selfObj.PKBPJPCJAID[j] <= -11111)
			{
				selfObj.PKBPJPCJAID[j] += 22222;
			}
			if (selfObj.PKBPJPCJAID[j] == -1)
			{
				selfObj.GPONFKAJAHI.Add(j);
			}
		}
		foreach (BlockController item3 in selfObj.GEGOAHPBGKB)
		{
			if (num <= 100)
			{
				num++;
			}
			else
			{
				num = 0;
				yield return null;
			}
			BlockController blockController2 = selfObj.ODENHACMMKO(item3);
			if ((bool)blockController2)
			{
				item3.DCNIOOFAOMB = ((blockController2.ILFEIIFBHMP < 0 || item3.JNKEKNOAPHO.gid < 10) ? blockController2.DCNIOOFAOMB : blockController2.ILFEIIFBHMP);
			}
		}
		IR1BlfUg0M3aZH8RXPUkABs = num2;
		yield return null;
		Ik_q35lTrP8u6tO9BA3U_0024wU = true;
		yield return null;
	}

	private static void tjLsYT_0024xlAPMPH7Oek0spTI(HDBLLPODNLN mee, BlockController HLEKLIGJLDL, BlockController ELHAIKHDLLE, int NGGOGGKHBJN, List<BlockController> IFPDDICHBIH, List<BlockController> IPMCNEKMLCO)
	{
		Class17.DQGZpGELKqeKmXweFYGEcaXR73CqGywHQI6u2TATGL80(global::_003CModule_003E.smethod_26<string>(2562840817u), mee, HLEKLIGJLDL, ELHAIKHDLLE, NGGOGGKHBJN, IFPDDICHBIH, IPMCNEKMLCO);
	}

	internal static Coroutine smethod_1(MonoBehaviour monoBehaviour_0, IEnumerator ienumerator_0)
	{
		return monoBehaviour_0.StartCoroutine(ienumerator_0);
	}
}
