using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using HarmonyLib;
using UnityEngine;

internal static class nfIqruisjwbrqbYtCJuoSA7DosA4L0FkAbTogZt6U8q_00244Mw9hpDlrdyv1kVeKjhH6g
{
	[HarmonyPatch(typeof(BattleRuler))]
	[HarmonyPatch("_CountDeathB")]
	internal static class NBX2jo2jVIZ3KhLRhJq8sL47qkk4EJcCl2NKQGG9xt9zPMZO_00248wr4sZBQ1erGAr5SrIxDO5nSxXPeW_UNzLOo3h0b_Bw_0024ri1jPGyrbZa3bsAvDgpHzgaVLY3ovQWWnI_SA
	{
		[HarmonyPrefix]
		internal static bool smethod_0(int GECCDMGLAOB, ref int ___AIIICLKJEAN, ref int ___FIMOAPNBKFA)
		{
			if (fYYx8vN1tah7ZgfygXMH9eJU0YzjaUsqtv9tADffziMcu1k1It8P_0024ZciTdZuxN09bA.f0cTFWbzuyWZNgKY9qJLSeA && JKGKJLLFMLE.EGFHGHKLNAO == JKGKJLLFMLE.LENPCAMMAEP.TeamBattle)
			{
				if (JKGKJLLFMLE.JNOGNOMLMEA != null && JKGKJLLFMLE.JNOGNOMLMEA.details.GetValueSafe(global::_003CModule_003E.smethod_27<string>(4278506510u)) != 0)
				{
					mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_25<string>(2178169526u), bool_0: true);
					return false;
				}
				return true;
			}
			return true;
		}
	}

	[HarmonyPatch("GatherRendererTs")]
	[HarmonyPatch(typeof(MachineAdjuster))]
	internal static class sAbtaY_8bdoZkoMlX2z4sxvQXPXe0REh_DhQdNvKt03iOL5ZrTm3cpDDft0IaNTFvtLNufIoKGHjMcgvH_0024COZD6qiE0CnAXbaadJEf3jvsgFVbd2QIbXayT_pid8F1ovOQ
	{
		[CompilerGenerated]
		private sealed class xQLlXwvQfU73AR7DWoyqeJuLCRqnj13zuRYIyyC9v9m7QPwjDg3nx_oYRgkfGe9iVrRDE2QgYEAczSfgELOYx6gl6V5AFWlN__0024I9HqkAoqL2 : IEnumerator<object>, IDisposable, IEnumerator
		{
			private int SjlBM8inVA_YE4YVlr_0024gluY;

			private object yT7HpVIzmqW54W307WgJtr4;

			public MachineAdjuster wQ6mrkDog7tAEXGul0Y8Sv0;

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
			public xQLlXwvQfU73AR7DWoyqeJuLCRqnj13zuRYIyyC9v9m7QPwjDg3nx_oYRgkfGe9iVrRDE2QgYEAczSfgELOYx6gl6V5AFWlN__0024I9HqkAoqL2(int _003C_003E1__state)
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
					SjlBM8inVA_YE4YVlr_0024gluY = -1;
					break;
				case 0:
					SjlBM8inVA_YE4YVlr_0024gluY = -1;
					break;
				}
				if (!smethod_2(smethod_1((Component)wQ6mrkDog7tAEXGul0Y8Sv0), global::_003CModule_003E.smethod_28<string>(420003428u)))
				{
					if (fYYx8vN1tah7ZgfygXMH9eJU0YzjaUsqtv9tADffziMcu1k1It8P_0024ZciTdZuxN09bA.f0cTFWbzuyWZNgKY9qJLSeA && JKGKJLLFMLE.EGFHGHKLNAO == JKGKJLLFMLE.LENPCAMMAEP.TeamBattle)
					{
						if (JKGKJLLFMLE.JNOGNOMLMEA != null && JKGKJLLFMLE.JNOGNOMLMEA.details.GetValueSafe(global::_003CModule_003E.smethod_28<string>(3166952407u)) != 0)
						{
							int key = ((HNJDDKJLHMM.FHLGOMHPDLN != HNJDDKJLHMM.HKGAACMIPIH.Legacy) ? new BkEYtCXOGBC5a3sGZc1baFBEZsix9sHaNV7UHBoeB8aKxFK0bpuW1JKVsL3eaRtFkQ(wQ6mrkDog7tAEXGul0Y8Sv0.GetComponent<PhotonView>()).UqrvyFYAsLATo10rDjF7eQA.Int32_0 : smethod_3(wQ6mrkDog7tAEXGul0Y8Sv0.GetComponent<NetworkView>()).guid.GetHashCode());
							if ((SceneMan.JFAOKFIDAGK as Battle).GetComponent<HPes7ND2CaDqC2mrirINwsPhZuBmTlh4TBklV_qvzB2kvtC8Z4sg95BwhxNlis22kdlEfEjqJcj0WGLZTVA1vbzD9zMTC5jxO5NPy746g1Yv_9_0024TRCgeve_aDX1Lc3sDaA>() != null && (SceneMan.JFAOKFIDAGK as Battle).GetComponent<HPes7ND2CaDqC2mrirINwsPhZuBmTlh4TBklV_qvzB2kvtC8Z4sg95BwhxNlis22kdlEfEjqJcj0WGLZTVA1vbzD9zMTC5jxO5NPy746g1Yv_9_0024TRCgeve_aDX1Lc3sDaA>().dictionary_0.ContainsKey(key))
							{
								return false;
							}
							float num = 0f;
							foreach (GameObject item in wQ6mrkDog7tAEXGul0Y8Sv0.CIPOPAGDJDE.FHLICBAMEMC)
							{
								if (!(item == null))
								{
									BlockData jNKEKNOAPHO = item.GetComponent<BlockController>().JNKEKNOAPHO;
									mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_29<string>(1188886141u) + jNKEKNOAPHO.type, bool_0: true);
									switch (jNKEKNOAPHO.type)
									{
									case BlockData.AAHMDBHDCDK.Thruster:
										num += (float)JKGKJLLFMLE.JNOGNOMLMEA.details.GetValueSafe(global::_003CModule_003E.smethod_27<string>(3985346349u));
										break;
									case BlockData.AAHMDBHDCDK.AGDevice:
										num += (float)JKGKJLLFMLE.JNOGNOMLMEA.details.GetValueSafe(global::_003CModule_003E.smethod_25<string>(4073792453u));
										break;
									case BlockData.AAHMDBHDCDK.Sword:
										num += (float)JKGKJLLFMLE.JNOGNOMLMEA.details.GetValueSafe(global::_003CModule_003E.smethod_25<string>(3463627531u));
										break;
									case BlockData.AAHMDBHDCDK.Wheel:
										num += (float)JKGKJLLFMLE.JNOGNOMLMEA.details.GetValueSafe(global::_003CModule_003E.smethod_29<string>(875490800u));
										break;
									case BlockData.AAHMDBHDCDK.Shaft:
										num += (float)JKGKJLLFMLE.JNOGNOMLMEA.details.GetValueSafe(global::_003CModule_003E.smethod_29<string>(3255551813u));
										break;
									case BlockData.AAHMDBHDCDK.JointTS:
									case BlockData.AAHMDBHDCDK.JointPS:
									case BlockData.AAHMDBHDCDK.JointBS:
									case BlockData.AAHMDBHDCDK.JointTA:
									case BlockData.AAHMDBHDCDK.JointPA:
									case BlockData.AAHMDBHDCDK.JointBA:
										num += (float)JKGKJLLFMLE.JNOGNOMLMEA.details.GetValueSafe(global::_003CModule_003E.smethod_27<string>(849043586u));
										break;
									case BlockData.AAHMDBHDCDK.Discharger:
										num += (float)JKGKJLLFMLE.JNOGNOMLMEA.details.GetValueSafe(global::_003CModule_003E.smethod_29<string>(1665143571u));
										break;
									case BlockData.AAHMDBHDCDK.Tracker:
										num += (float)JKGKJLLFMLE.JNOGNOMLMEA.details.GetValueSafe(global::_003CModule_003E.smethod_26<string>(638236189u));
										break;
									case BlockData.AAHMDBHDCDK.Mover:
										num += (float)JKGKJLLFMLE.JNOGNOMLMEA.details.GetValueSafe(global::_003CModule_003E.smethod_26<string>(3685526850u));
										break;
									case BlockData.AAHMDBHDCDK.Cannon1:
									case BlockData.AAHMDBHDCDK.Cannon2:
										num += (float)JKGKJLLFMLE.JNOGNOMLMEA.details.GetValueSafe(global::_003CModule_003E.smethod_27<string>(1374857122u));
										break;
									case BlockData.AAHMDBHDCDK.Beamer:
										num += (float)JKGKJLLFMLE.JNOGNOMLMEA.details.GetValueSafe(global::_003CModule_003E.smethod_28<string>(1178934741u));
										break;
									case BlockData.AAHMDBHDCDK.Shield:
										num += (float)JKGKJLLFMLE.JNOGNOMLMEA.details.GetValueSafe(global::_003CModule_003E.smethod_28<string>(723635236u));
										break;
									default:
										num += (float)JKGKJLLFMLE.JNOGNOMLMEA.details.GetValueSafe(global::_003CModule_003E.smethod_26<string>(959003627u));
										break;
									case BlockData.AAHMDBHDCDK.Launcher:
										num += (float)JKGKJLLFMLE.JNOGNOMLMEA.details.GetValueSafe(global::_003CModule_003E.smethod_27<string>(1807609308u));
										break;
									}
								}
							}
							num /= 10f;
							mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_25<string>(1526689285u) + num, bool_0: true);
							if (HNJDDKJLHMM.FHLGOMHPDLN == HNJDDKJLHMM.HKGAACMIPIH.Legacy)
							{
								(Arena.OEDCBNHNGMJ as Game).GetComponent<NetworkView>().RPC(global::_003CModule_003E.smethod_29<string>(1582385189u), wQ6mrkDog7tAEXGul0Y8Sv0.GetComponent<NetworkView>().owner, global::_003CModule_003E.smethod_26<string>(2764888070u) + num + global::_003CModule_003E.smethod_29<string>(2422714379u), -1);
							}
							else
							{
								(Arena.OEDCBNHNGMJ as Game).GetComponent<PhotonView>().RPC(global::_003CModule_003E.smethod_29<string>(1582385189u), new BkEYtCXOGBC5a3sGZc1baFBEZsix9sHaNV7UHBoeB8aKxFK0bpuW1JKVsL3eaRtFkQ(wQ6mrkDog7tAEXGul0Y8Sv0.GetComponent<PhotonView>()).K52jeLH_0024D_rsZoa7xVSPVPk, global::_003CModule_003E.smethod_26<string>(2764888070u) + num + global::_003CModule_003E.smethod_26<string>(1642202037u), -1);
							}
							Battle battle = SceneMan.JFAOKFIDAGK as Battle;
							if (battle.GetComponent<HPes7ND2CaDqC2mrirINwsPhZuBmTlh4TBklV_qvzB2kvtC8Z4sg95BwhxNlis22kdlEfEjqJcj0WGLZTVA1vbzD9zMTC5jxO5NPy746g1Yv_9_0024TRCgeve_aDX1Lc3sDaA>() == null)
							{
								battle.gameObject.AddComponent<HPes7ND2CaDqC2mrirINwsPhZuBmTlh4TBklV_qvzB2kvtC8Z4sg95BwhxNlis22kdlEfEjqJcj0WGLZTVA1vbzD9zMTC5jxO5NPy746g1Yv_9_0024TRCgeve_aDX1Lc3sDaA>();
								mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_28<string>(480759431u));
							}
							battle.GetComponent<HPes7ND2CaDqC2mrirINwsPhZuBmTlh4TBklV_qvzB2kvtC8Z4sg95BwhxNlis22kdlEfEjqJcj0WGLZTVA1vbzD9zMTC5jxO5NPy746g1Yv_9_0024TRCgeve_aDX1Lc3sDaA>().dictionary_0.Add(new BkEYtCXOGBC5a3sGZc1baFBEZsix9sHaNV7UHBoeB8aKxFK0bpuW1JKVsL3eaRtFkQ(wQ6mrkDog7tAEXGul0Y8Sv0.GetComponent<PhotonView>()).UqrvyFYAsLATo10rDjF7eQA.Int32_0, num);
							return false;
						}
						return false;
					}
					return false;
				}
				yT7HpVIzmqW54W307WgJtr4 = smethod_0(0.1f);
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
				throw smethod_4();
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

			internal static NetworkPlayer smethod_3(NetworkView networkView_0)
			{
				return networkView_0.owner;
			}

			internal static NotSupportedException smethod_4()
			{
				return new NotSupportedException();
			}
		}

		[HarmonyPostfix]
		internal static void FeUAVwFbW6wGJJdNimZY9yI(MachineAdjuster __instance)
		{
			smethod_0((MonoBehaviour)__instance, loViH1fVEjVoPDpyXMWiC40(__instance));
		}

		internal static IEnumerator loViH1fVEjVoPDpyXMWiC40(MachineAdjuster __instance)
		{
			while (xQLlXwvQfU73AR7DWoyqeJuLCRqnj13zuRYIyyC9v9m7QPwjDg3nx_oYRgkfGe9iVrRDE2QgYEAczSfgELOYx6gl6V5AFWlN__0024I9HqkAoqL2.smethod_2(xQLlXwvQfU73AR7DWoyqeJuLCRqnj13zuRYIyyC9v9m7QPwjDg3nx_oYRgkfGe9iVrRDE2QgYEAczSfgELOYx6gl6V5AFWlN__0024I9HqkAoqL2.smethod_1((Component)__instance), global::_003CModule_003E.smethod_28<string>(420003428u)))
			{
				yield return xQLlXwvQfU73AR7DWoyqeJuLCRqnj13zuRYIyyC9v9m7QPwjDg3nx_oYRgkfGe9iVrRDE2QgYEAczSfgELOYx6gl6V5AFWlN__0024I9HqkAoqL2.smethod_0(0.1f);
			}
			if (!fYYx8vN1tah7ZgfygXMH9eJU0YzjaUsqtv9tADffziMcu1k1It8P_0024ZciTdZuxN09bA.f0cTFWbzuyWZNgKY9qJLSeA || JKGKJLLFMLE.EGFHGHKLNAO != JKGKJLLFMLE.LENPCAMMAEP.TeamBattle || JKGKJLLFMLE.JNOGNOMLMEA == null || JKGKJLLFMLE.JNOGNOMLMEA.details.GetValueSafe(global::_003CModule_003E.smethod_28<string>(3166952407u)) == 0)
			{
				yield break;
			}
			int key = ((HNJDDKJLHMM.FHLGOMHPDLN != HNJDDKJLHMM.HKGAACMIPIH.Legacy) ? new BkEYtCXOGBC5a3sGZc1baFBEZsix9sHaNV7UHBoeB8aKxFK0bpuW1JKVsL3eaRtFkQ(__instance.GetComponent<PhotonView>()).UqrvyFYAsLATo10rDjF7eQA.Int32_0 : xQLlXwvQfU73AR7DWoyqeJuLCRqnj13zuRYIyyC9v9m7QPwjDg3nx_oYRgkfGe9iVrRDE2QgYEAczSfgELOYx6gl6V5AFWlN__0024I9HqkAoqL2.smethod_3(__instance.GetComponent<NetworkView>()).guid.GetHashCode());
			if ((SceneMan.JFAOKFIDAGK as Battle).GetComponent<HPes7ND2CaDqC2mrirINwsPhZuBmTlh4TBklV_qvzB2kvtC8Z4sg95BwhxNlis22kdlEfEjqJcj0WGLZTVA1vbzD9zMTC5jxO5NPy746g1Yv_9_0024TRCgeve_aDX1Lc3sDaA>() != null && (SceneMan.JFAOKFIDAGK as Battle).GetComponent<HPes7ND2CaDqC2mrirINwsPhZuBmTlh4TBklV_qvzB2kvtC8Z4sg95BwhxNlis22kdlEfEjqJcj0WGLZTVA1vbzD9zMTC5jxO5NPy746g1Yv_9_0024TRCgeve_aDX1Lc3sDaA>().dictionary_0.ContainsKey(key))
			{
				yield break;
			}
			float num = 0f;
			foreach (GameObject item in __instance.CIPOPAGDJDE.FHLICBAMEMC)
			{
				if (!(item == null))
				{
					BlockData jNKEKNOAPHO = item.GetComponent<BlockController>().JNKEKNOAPHO;
					mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_29<string>(1188886141u) + jNKEKNOAPHO.type, bool_0: true);
					switch (jNKEKNOAPHO.type)
					{
					case BlockData.AAHMDBHDCDK.Thruster:
						num += (float)JKGKJLLFMLE.JNOGNOMLMEA.details.GetValueSafe(global::_003CModule_003E.smethod_27<string>(3985346349u));
						break;
					case BlockData.AAHMDBHDCDK.AGDevice:
						num += (float)JKGKJLLFMLE.JNOGNOMLMEA.details.GetValueSafe(global::_003CModule_003E.smethod_25<string>(4073792453u));
						break;
					case BlockData.AAHMDBHDCDK.Sword:
						num += (float)JKGKJLLFMLE.JNOGNOMLMEA.details.GetValueSafe(global::_003CModule_003E.smethod_25<string>(3463627531u));
						break;
					case BlockData.AAHMDBHDCDK.Wheel:
						num += (float)JKGKJLLFMLE.JNOGNOMLMEA.details.GetValueSafe(global::_003CModule_003E.smethod_29<string>(875490800u));
						break;
					case BlockData.AAHMDBHDCDK.Shaft:
						num += (float)JKGKJLLFMLE.JNOGNOMLMEA.details.GetValueSafe(global::_003CModule_003E.smethod_29<string>(3255551813u));
						break;
					case BlockData.AAHMDBHDCDK.JointTS:
					case BlockData.AAHMDBHDCDK.JointPS:
					case BlockData.AAHMDBHDCDK.JointBS:
					case BlockData.AAHMDBHDCDK.JointTA:
					case BlockData.AAHMDBHDCDK.JointPA:
					case BlockData.AAHMDBHDCDK.JointBA:
						num += (float)JKGKJLLFMLE.JNOGNOMLMEA.details.GetValueSafe(global::_003CModule_003E.smethod_27<string>(849043586u));
						break;
					case BlockData.AAHMDBHDCDK.Discharger:
						num += (float)JKGKJLLFMLE.JNOGNOMLMEA.details.GetValueSafe(global::_003CModule_003E.smethod_29<string>(1665143571u));
						break;
					case BlockData.AAHMDBHDCDK.Tracker:
						num += (float)JKGKJLLFMLE.JNOGNOMLMEA.details.GetValueSafe(global::_003CModule_003E.smethod_26<string>(638236189u));
						break;
					case BlockData.AAHMDBHDCDK.Mover:
						num += (float)JKGKJLLFMLE.JNOGNOMLMEA.details.GetValueSafe(global::_003CModule_003E.smethod_26<string>(3685526850u));
						break;
					case BlockData.AAHMDBHDCDK.Cannon1:
					case BlockData.AAHMDBHDCDK.Cannon2:
						num += (float)JKGKJLLFMLE.JNOGNOMLMEA.details.GetValueSafe(global::_003CModule_003E.smethod_27<string>(1374857122u));
						break;
					case BlockData.AAHMDBHDCDK.Beamer:
						num += (float)JKGKJLLFMLE.JNOGNOMLMEA.details.GetValueSafe(global::_003CModule_003E.smethod_28<string>(1178934741u));
						break;
					case BlockData.AAHMDBHDCDK.Shield:
						num += (float)JKGKJLLFMLE.JNOGNOMLMEA.details.GetValueSafe(global::_003CModule_003E.smethod_28<string>(723635236u));
						break;
					default:
						num += (float)JKGKJLLFMLE.JNOGNOMLMEA.details.GetValueSafe(global::_003CModule_003E.smethod_26<string>(959003627u));
						break;
					case BlockData.AAHMDBHDCDK.Launcher:
						num += (float)JKGKJLLFMLE.JNOGNOMLMEA.details.GetValueSafe(global::_003CModule_003E.smethod_27<string>(1807609308u));
						break;
					}
				}
			}
			num /= 10f;
			mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_25<string>(1526689285u) + num, bool_0: true);
			if (HNJDDKJLHMM.FHLGOMHPDLN == HNJDDKJLHMM.HKGAACMIPIH.Legacy)
			{
				(Arena.OEDCBNHNGMJ as Game).GetComponent<NetworkView>().RPC(global::_003CModule_003E.smethod_29<string>(1582385189u), __instance.GetComponent<NetworkView>().owner, global::_003CModule_003E.smethod_26<string>(2764888070u) + num + global::_003CModule_003E.smethod_29<string>(2422714379u), -1);
			}
			else
			{
				(Arena.OEDCBNHNGMJ as Game).GetComponent<PhotonView>().RPC(global::_003CModule_003E.smethod_29<string>(1582385189u), new BkEYtCXOGBC5a3sGZc1baFBEZsix9sHaNV7UHBoeB8aKxFK0bpuW1JKVsL3eaRtFkQ(__instance.GetComponent<PhotonView>()).K52jeLH_0024D_rsZoa7xVSPVPk, global::_003CModule_003E.smethod_26<string>(2764888070u) + num + global::_003CModule_003E.smethod_26<string>(1642202037u), -1);
			}
			Battle battle = SceneMan.JFAOKFIDAGK as Battle;
			if (battle.GetComponent<HPes7ND2CaDqC2mrirINwsPhZuBmTlh4TBklV_qvzB2kvtC8Z4sg95BwhxNlis22kdlEfEjqJcj0WGLZTVA1vbzD9zMTC5jxO5NPy746g1Yv_9_0024TRCgeve_aDX1Lc3sDaA>() == null)
			{
				battle.gameObject.AddComponent<HPes7ND2CaDqC2mrirINwsPhZuBmTlh4TBklV_qvzB2kvtC8Z4sg95BwhxNlis22kdlEfEjqJcj0WGLZTVA1vbzD9zMTC5jxO5NPy746g1Yv_9_0024TRCgeve_aDX1Lc3sDaA>();
				mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_28<string>(480759431u));
			}
			battle.GetComponent<HPes7ND2CaDqC2mrirINwsPhZuBmTlh4TBklV_qvzB2kvtC8Z4sg95BwhxNlis22kdlEfEjqJcj0WGLZTVA1vbzD9zMTC5jxO5NPy746g1Yv_9_0024TRCgeve_aDX1Lc3sDaA>().dictionary_0.Add(new BkEYtCXOGBC5a3sGZc1baFBEZsix9sHaNV7UHBoeB8aKxFK0bpuW1JKVsL3eaRtFkQ(__instance.GetComponent<PhotonView>()).UqrvyFYAsLATo10rDjF7eQA.Int32_0, num);
		}

		internal static Coroutine smethod_0(MonoBehaviour monoBehaviour_0, IEnumerator ienumerator_0)
		{
			return monoBehaviour_0.StartCoroutine(ienumerator_0);
		}
	}

	internal class HPes7ND2CaDqC2mrirINwsPhZuBmTlh4TBklV_qvzB2kvtC8Z4sg95BwhxNlis22kdlEfEjqJcj0WGLZTVA1vbzD9zMTC5jxO5NPy746g1Yv_9_0024TRCgeve_aDX1Lc3sDaA : MonoBehaviour
	{
		internal Dictionary<int, float> dictionary_0 = new Dictionary<int, float>();

		[RPC]
		[BBNLOHJIPHJ]
		public void RPC_CountDeathB(int GECCDMGLAOB, DBMLFPDNFAB info)
		{
			if (!HNJDDKJLHMM.IOOILBCOFMF)
			{
				return;
			}
			mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng("counting with my own death function!", bool_0: true);
			Class15 @class = new Class15(info.FMIBNEKIIKA);
			if (!dictionary_0.ContainsKey(@class.Int32_0))
			{
				mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng("tried to get cost of invalid actorID " + @class.Int32_0);
				return;
			}
			float num = dictionary_0[@class.Int32_0];
			BattleRuler component = GetComponent<BattleRuler>();
			if (component == null)
			{
				mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng("battleruler is null");
				return;
			}
			switch (GECCDMGLAOB)
			{
			case 1:
			{
				float num3 = Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<float>("AIIICLKJEAN", component);
				mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng("team " + GECCDMGLAOB + " score: " + num3, bool_0: true);
				num3 += num * 0.01f;
				Class17.DMoImhPC2jNUt2tgmSezeT_IK_0024XK5Df9PmexG_CaKb1R("AIIICLKJEAN", component, num3);
				mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng("team " + GECCDMGLAOB + " score is now: " + num3, bool_0: true);
				break;
			}
			case 2:
			{
				float num2 = Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<float>("FIMOAPNBKFA", component);
				mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng("team " + GECCDMGLAOB + " score: " + num2, bool_0: true);
				num2 += num * 0.01f;
				Class17.DMoImhPC2jNUt2tgmSezeT_IK_0024XK5Df9PmexG_CaKb1R("FIMOAPNBKFA", component, num2);
				mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng("team " + GECCDMGLAOB + " score is now: " + num2, bool_0: true);
				break;
			}
			default:
				mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng("invalid teamID: " + GECCDMGLAOB);
				break;
			}
		}
	}
}
