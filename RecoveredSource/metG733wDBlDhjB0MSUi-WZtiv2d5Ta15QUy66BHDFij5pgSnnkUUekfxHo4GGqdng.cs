using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using ExitGames.Client.Photon;
using HarmonyLib;
using MPatchrMain;
using Steamworks;
using UnityEngine;

internal static class metG733wDBlDhjB0MSUi_0024WZtiv2d5Ta15QUy66BHDFij5pgSnnkUUekfxHo4GGqdng
{
	[HarmonyPatch(typeof(SteamAPI))]
	[HarmonyPatch("Init")]
	internal static class PCrZEnMBeTE_Ad8AsAfHu4lIZnc9gW0uRKh76v2TV2aBQO1YeBmVPNFNDHO1Kk1Rng
	{
		[HarmonyPrefix]
		internal static void smethod_0()
		{
			mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_29<string>(2511535979u));
			MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68 = settingsIngame.smethod_0();
			MPatcherFork.CustomPatches.CouplerRotation.TryRegister();
			MqnKZ_0024xlzvnL3l15SHWn86U();
			MPatcherFork.CustomPatches.SetupPrecision.TryRegister();
		}
	}

	[CompilerGenerated]
	private sealed class UUi9CES6jT0ZzKFkxbFyR2hi3gXjXAxodk4_IlDoeDAh_Isr5yiGHBGg5CHblUybBA : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int SjlBM8inVA_YE4YVlr_0024gluY;

		private object yT7HpVIzmqW54W307WgJtr4;

		private SEGI auS9AuckPgKwwCjaru5deGk;

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
		public UUi9CES6jT0ZzKFkxbFyR2hi3gXjXAxodk4_IlDoeDAh_Isr5yiGHBGg5CHblUybBA(int _003C_003E1__state)
		{
			SjlBM8inVA_YE4YVlr_0024gluY = _003C_003E1__state;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			auS9AuckPgKwwCjaru5deGk = null;
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
				auS9AuckPgKwwCjaru5deGk = smethod_0().GetComponent<SEGI>();
				break;
			}
			if (!smethod_1((UnityEngine.Object)Arena.OEDCBNHNGMJ.FICMBCLEFDL, (UnityEngine.Object)null))
			{
				return false;
			}
			auS9AuckPgKwwCjaru5deGk.voxelSpaceSize = Arena.OEDCBNHNGMJ.FICMBCLEFDL.BBKOMHJGBPA * 5f;
			yT7HpVIzmqW54W307WgJtr4 = null;
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
			throw smethod_2();
		}

		internal static Camera smethod_0()
		{
			return Camera.main;
		}

		internal static bool smethod_1(UnityEngine.Object object_0, UnityEngine.Object object_1)
		{
			return object_0 != object_1;
		}

		internal static NotSupportedException smethod_2()
		{
			return new NotSupportedException();
		}
	}

	internal static Harmony dh18OlujRtC2fZ7NxG9SAeI;

	private static readonly string string_0 = global::_003CModule_003E.smethod_28<string>(1406955011u);

	public static bool ELqGdI0DqLmFirmwZsUXVAM = false;

	public static bool b45w_0024W58SmxYqmBXMdyPewEzBnmFT4EuefSPY7GIofSF = false;

	public static void NkE4SICTmcYlwmIfpNh_0024xKw()
	{
		dh18OlujRtC2fZ7NxG9SAeI = smethod_0(string_0);
	}

	public static void bl10qLgwUVVComuGzPN5IogJQnTiOUknRHmdHIbVuGoP()
	{
		if (dh18OlujRtC2fZ7NxG9SAeI == null)
		{
			NkE4SICTmcYlwmIfpNh_0024xKw();
		}
		dh18OlujRtC2fZ7NxG9SAeI.MIO3Ksr2DNhtW5NmqDuIAyo(smethod_1(typeof(PCrZEnMBeTE_Ad8AsAfHu4lIZnc9gW0uRKh76v2TV2aBQO1YeBmVPNFNDHO1Kk1Rng).TypeHandle));
	}

	internal static void MqnKZ_0024xlzvnL3l15SHWn86U()
	{
		if (dh18OlujRtC2fZ7NxG9SAeI == null)
		{
			NkE4SICTmcYlwmIfpNh_0024xKw();
		}
		if (ELqGdI0DqLmFirmwZsUXVAM)
		{
			return;
		}
		ELqGdI0DqLmFirmwZsUXVAM = true;
		dh18OlujRtC2fZ7NxG9SAeI.MIO3Ksr2DNhtW5NmqDuIAyo(smethod_1(typeof(Ojep9_ywtLMuNK7LiDZuXM7hMYHRARN0jK4l2Eu3p_0024Jx6MTX_qSNPCRKPRl_gGCzJA).TypeHandle));
		dh18OlujRtC2fZ7NxG9SAeI.MIO3Ksr2DNhtW5NmqDuIAyo(smethod_1(typeof(fuLOHWIE5UYsXf1i_0024U7ZrRt7wYu2JGe7flr2Cyk0jSHi7p3wG28vwmnGpDW496_0024OrA).TypeHandle));
		dh18OlujRtC2fZ7NxG9SAeI.MIO3Ksr2DNhtW5NmqDuIAyo(smethod_1(typeof(Class48).TypeHandle));
		if (smethod_2(global::_003CModule_003E.smethod_26<string>(527180330u)))
		{
			try
			{
				Class36.hJS8kPKIDNOtELzBVeai1g8.Add(fB_UAU3VnG0wrUs2vz3KliVPPZGNBZGJ1WMs94YsvVgRq16uNvACiTMzlWLTlwKpvA.G7khXKfjk4KTXg71C_00242Nx3I, delegate(Game __instance, string OIHFGPOPPDD)
				{
					if (!fB_UAU3VnG0wrUs2vz3KliVPPZGNBZGJ1WMs94YsvVgRq16uNvACiTMzlWLTlwKpvA.smethod_6())
					{
						fB_UAU3VnG0wrUs2vz3KliVPPZGNBZGJ1WMs94YsvVgRq16uNvACiTMzlWLTlwKpvA.smethod_7(__instance, fB_UAU3VnG0wrUs2vz3KliVPPZGNBZGJ1WMs94YsvVgRq16uNvACiTMzlWLTlwKpvA.m_ne3lilYmB2u1AUzL2dOj0, -1);
					}
					else
					{
						fB_UAU3VnG0wrUs2vz3KliVPPZGNBZGJ1WMs94YsvVgRq16uNvACiTMzlWLTlwKpvA.rV3NLoyUlBog_42slx5OCoA = new Vector3[30];
						fB_UAU3VnG0wrUs2vz3KliVPPZGNBZGJ1WMs94YsvVgRq16uNvACiTMzlWLTlwKpvA.vector3_0 = new Vector3[30];
						fB_UAU3VnG0wrUs2vz3KliVPPZGNBZGJ1WMs94YsvVgRq16uNvACiTMzlWLTlwKpvA.vector3_1 = new Vector3[30];
						fB_UAU3VnG0wrUs2vz3KliVPPZGNBZGJ1WMs94YsvVgRq16uNvACiTMzlWLTlwKpvA.vector3_2 = new Vector3[30];
						Vector3[] rots;
						Vector3[] array = fB_UAU3VnG0wrUs2vz3KliVPPZGNBZGJ1WMs94YsvVgRq16uNvACiTMzlWLTlwKpvA.smethod_1(out rots);
						Vector3[] rots2;
						Vector3[] array2 = fB_UAU3VnG0wrUs2vz3KliVPPZGNBZGJ1WMs94YsvVgRq16uNvACiTMzlWLTlwKpvA.v2ZqnTVxlWgZ_CROtL2F3FE(out rots2);
						fB_UAU3VnG0wrUs2vz3KliVPPZGNBZGJ1WMs94YsvVgRq16uNvACiTMzlWLTlwKpvA.smethod_7(__instance, fB_UAU3VnG0wrUs2vz3KliVPPZGNBZGJ1WMs94YsvVgRq16uNvACiTMzlWLTlwKpvA.smethod_8(global::_003CModule_003E.smethod_25<string>(3855626484u), (object)array.Length), -1);
						fB_UAU3VnG0wrUs2vz3KliVPPZGNBZGJ1WMs94YsvVgRq16uNvACiTMzlWLTlwKpvA.smethod_7(__instance, fB_UAU3VnG0wrUs2vz3KliVPPZGNBZGJ1WMs94YsvVgRq16uNvACiTMzlWLTlwKpvA.smethod_8(global::_003CModule_003E.smethod_27<string>(611449699u), (object)array2.Length), -1);
						fB_UAU3VnG0wrUs2vz3KliVPPZGNBZGJ1WMs94YsvVgRq16uNvACiTMzlWLTlwKpvA.smethod_9((Array)array, (Array)fB_UAU3VnG0wrUs2vz3KliVPPZGNBZGJ1WMs94YsvVgRq16uNvACiTMzlWLTlwKpvA.rV3NLoyUlBog_42slx5OCoA, array.Length);
						fB_UAU3VnG0wrUs2vz3KliVPPZGNBZGJ1WMs94YsvVgRq16uNvACiTMzlWLTlwKpvA.smethod_9((Array)rots, (Array)fB_UAU3VnG0wrUs2vz3KliVPPZGNBZGJ1WMs94YsvVgRq16uNvACiTMzlWLTlwKpvA.vector3_1, rots.Length);
						fB_UAU3VnG0wrUs2vz3KliVPPZGNBZGJ1WMs94YsvVgRq16uNvACiTMzlWLTlwKpvA.smethod_9((Array)array2, (Array)fB_UAU3VnG0wrUs2vz3KliVPPZGNBZGJ1WMs94YsvVgRq16uNvACiTMzlWLTlwKpvA.vector3_0, array2.Length);
						fB_UAU3VnG0wrUs2vz3KliVPPZGNBZGJ1WMs94YsvVgRq16uNvACiTMzlWLTlwKpvA.smethod_9((Array)rots2, (Array)fB_UAU3VnG0wrUs2vz3KliVPPZGNBZGJ1WMs94YsvVgRq16uNvACiTMzlWLTlwKpvA.vector3_2, rots2.Length);
						ExitGames.Client.Photon.Hashtable hashtable = fB_UAU3VnG0wrUs2vz3KliVPPZGNBZGJ1WMs94YsvVgRq16uNvACiTMzlWLTlwKpvA.smethod_10();
						hashtable.Add(global::_003CModule_003E.smethod_26<string>(4253210631u), 64);
						fYYx8vN1tah7ZgfygXMH9eJU0YzjaUsqtv9tADffziMcu1k1It8P_0024ZciTdZuxN09bA.MCJUb7mzEcz9seOWPbquoos.eqcw8jU_g5UyCpFcD2HSL5O3iJjZefN8oeBDqEku7iqb(hashtable);
						fB_UAU3VnG0wrUs2vz3KliVPPZGNBZGJ1WMs94YsvVgRq16uNvACiTMzlWLTlwKpvA.smethod_7(__instance, global::_003CModule_003E.smethod_27<string>(132166838u), -1);
						fB_UAU3VnG0wrUs2vz3KliVPPZGNBZGJ1WMs94YsvVgRq16uNvACiTMzlWLTlwKpvA.oJN_00244IcEU0waAX7Zww3G6zI = true;
					}
				});
			}
			catch (ArgumentException)
			{
			}
			dh18OlujRtC2fZ7NxG9SAeI.MIO3Ksr2DNhtW5NmqDuIAyo(smethod_1(typeof(fB_UAU3VnG0wrUs2vz3KliVPPZGNBZGJ1WMs94YsvVgRq16uNvACiTMzlWLTlwKpvA).TypeHandle));
		}
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_25<string>(3169141289u));
		OfE_0024j68E7zxEuQWFgCK_0024GXWLPnYPk7OOHHmMGkX8hA836ISheZcGxIKvxKXwLjw68g.ozdM7P2Ys1T51t37eU4Zdr8();
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_28<string>(4293828499u));
		dh18OlujRtC2fZ7NxG9SAeI.MIO3Ksr2DNhtW5NmqDuIAyo(smethod_1(typeof(iK6fSwg8tZ_0024emq_vet_0024Smve_9IfrhVSwdP1a_jFOD7xJBpDFHXrMUreWEnISSbwJ4Q).TypeHandle));
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_28<string>(3474289390u));
		dh18OlujRtC2fZ7NxG9SAeI.MIO3Ksr2DNhtW5NmqDuIAyo(smethod_1(typeof(vJYJ1eONDPkjkgcRUVQ5P_6fiNuWWkFO2SJsiVgCvmOHSwl4oPxs0mnS2fhI_kOLUNxM30RRWDtzqP1RB0IJ1cg).TypeHandle));
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_29<string>(2341182134u));
		dh18OlujRtC2fZ7NxG9SAeI.MIO3Ksr2DNhtW5NmqDuIAyo(smethod_1(typeof(Class35).TypeHandle));
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_26<string>(2184804150u));
		dh18OlujRtC2fZ7NxG9SAeI.MIO3Ksr2DNhtW5NmqDuIAyo(smethod_1(typeof(Class35.__0024lja4xRbfBw6CfK30uhCTtos5zGXRk5CFdnwZBy8xztX3Y5UitBns7IuP1vkcm1_vY_0024tW4z4tkFBjkipasoRTWv3so_0024tMi_0024_0024OFn9kN_0024SVUa).TypeHandle));
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_28<string>(1820059223u));
		dh18OlujRtC2fZ7NxG9SAeI.MIO3Ksr2DNhtW5NmqDuIAyo(smethod_1(typeof(Class36).TypeHandle));
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_27<string>(1639895976u));
		dh18OlujRtC2fZ7NxG9SAeI.MIO3Ksr2DNhtW5NmqDuIAyo(smethod_1(typeof(GgrMl0eHjkTKzAqKC9Taf8i6N22UmQUCQv5QJo1e9j3Jw_0024HppxtR0oIfLMckJcgWz3YAZmq1UkdrbzocepnYQnM).TypeHandle));
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_28<string>(3911028185u));
		dh18OlujRtC2fZ7NxG9SAeI.MIO3Ksr2DNhtW5NmqDuIAyo(smethod_1(typeof(VAKKH25tiZ979R7HMdtVXnLNtNlDlxDBxoBifMx0Hi5OMoKRxud0sOMdYt_0024WLv66Gw).TypeHandle));
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_28<string>(3182548977u));
		dh18OlujRtC2fZ7NxG9SAeI.MIO3Ksr2DNhtW5NmqDuIAyo(smethod_1(typeof(LWySeHCFt_0024EXG_0024kEBP6D8Lf4OuiveYnYMQgKyaEfigZlCMrdZjWqJZ51CUaNI1q02Q).TypeHandle));
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_27<string>(3887513572u));
		dh18OlujRtC2fZ7NxG9SAeI.MIO3Ksr2DNhtW5NmqDuIAyo(smethod_1(typeof(Lpl5cKlEdlmgwHo99bHllIQS_0024supw_0024emM0b0ne_0024dfzPvuuoUFZL_0024bIH77XLRKzmaXw).TypeHandle));
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_26<string>(3723149352u));
		dh18OlujRtC2fZ7NxG9SAeI.MIO3Ksr2DNhtW5NmqDuIAyo(smethod_1(typeof(LWySeHCFt_0024EXG_0024kEBP6D8LdsoKS_0024IozE_LXqo1KfGYHLzV507qRs_A_b1EHLxwhVAg).TypeHandle));
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_29<string>(3798425443u));
		dh18OlujRtC2fZ7NxG9SAeI.MIO3Ksr2DNhtW5NmqDuIAyo(smethod_1(typeof(MLPOLjLeebDuY_VDPBvb03gHuMwQQUhnsZIxWNzeByM7YdTNTalpuudrVKdv_KQ7qg).TypeHandle));
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_29<string>(3517112065u));
		dh18OlujRtC2fZ7NxG9SAeI.MIO3Ksr2DNhtW5NmqDuIAyo(smethod_1(typeof(Yh0MC1EmkntIzqy080BKjSgpLa2CI4PTcamj03GzVohKShTG_0024_0024auwPq0_0024GE7v0_0024TcQ).TypeHandle));
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_29<string>(3376455376u));
		dh18OlujRtC2fZ7NxG9SAeI.MIO3Ksr2DNhtW5NmqDuIAyo(smethod_1(typeof(pTbXl0BQalSWSABflrYhjNmk_A7RB7wKIBoc_0024LjdnXpzOQoqdduhV2bTbESRxybyAA.AjldIet_ZgmqPhFe2_zL1VF_IzreX0P_ZeYtsbxVHW25oGvB5MHBm6VM2xEH6UZouusAh4iwfCMNWLrRubl63lkI5U_cLotTe61529dVwpp_0024enHG0lhGLCR_0024JIfHOzV4jA).TypeHandle));
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_25<string>(3827527788u));
		dh18OlujRtC2fZ7NxG9SAeI.MIO3Ksr2DNhtW5NmqDuIAyo(smethod_1(typeof(pTbXl0BQalSWSABflrYhjNmk_A7RB7wKIBoc_0024LjdnXpzOQoqdduhV2bTbESRxybyAA._ksMqmG_0024wFoYcgR7FFZgQraIKCA58Zjx0aPJS7A87WYMf_YJQatyE__OuCCeaka_HN475HlADCiG3Iz_0024gQUJSblZTieLn7y2Ck5b1i9EHeNndh_00244L56uMXtY5UFYRbIC6w).TypeHandle));
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_25<string>(1626822400u));
		dh18OlujRtC2fZ7NxG9SAeI.MIO3Ksr2DNhtW5NmqDuIAyo(smethod_1(typeof(ED5WPQxYa_WFEJ8sZpYciyFrTxXiqvRlKZY2ClLD7YL8d2_4VTlEKEqa_CnJ_0024LHCww).TypeHandle));
		dh18OlujRtC2fZ7NxG9SAeI.MIO3Ksr2DNhtW5NmqDuIAyo(smethod_1(typeof(ED5WPQxYa_WFEJ8sZpYciyFrTxXiqvRlKZY2ClLD7YL8d2_4VTlEKEqa_CnJ_0024LHCww.hn_0024ELiQrP8aH_0024bKB9pUwsE_0024osUUSCrHDVRVhmKMuL2fpIas23d1_0024l4PLLbyw7gKMUC5jfB4pGqpMJXAZUkLFFs_00240GoiElm78j8lH4V5xEl2n).TypeHandle));
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_29<string>(1753965171u));
		dh18OlujRtC2fZ7NxG9SAeI.MIO3Ksr2DNhtW5NmqDuIAyo(smethod_1(typeof(BDkfokxR71_dfvtXHNIeppLRPcz23oyvMspdw87a8wbKe3K5WnLuQqq4hNm5_0024cdlYA).TypeHandle));
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_29<string>(2359776590u));
		dh18OlujRtC2fZ7NxG9SAeI.MIO3Ksr2DNhtW5NmqDuIAyo(smethod_1(typeof(QI4dAcq_0024lvENeJ7F01V8DuWkQJnCRizUrJtq6RNpvisSE_Uydj2n87LPMaxsusJ984nm1lOlMCvkx_0024i6x06n3gY).TypeHandle));
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_25<string>(1981157794u));
		dh18OlujRtC2fZ7NxG9SAeI.MIO3Ksr2DNhtW5NmqDuIAyo(smethod_1(typeof(cueQi_wQP26TRIVnZP9Z8aKR6dED8Hl8W5iDYL_VPtx_dIciFE5bVmMCUS5X5HIpyWZYqeL_0024OMtz0_0024IS1W5U_0024SQ).TypeHandle));
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_29<string>(4177210847u));
		dh18OlujRtC2fZ7NxG9SAeI.MIO3Ksr2DNhtW5NmqDuIAyo(smethod_1(typeof(oIuuMMCo9UmUIWmYFQEqxXAOJy8LYqspmW2FKyHMiXKdcT1cBP8p9QxEsmrfao0MIw.S7_m3TAFHY1_0024wffzliowm_wZkh1HVH0IPp52rpMAxsizcFCS5Fz6rABzxNYebKSlS7ngw1_0024v6BKOEtD501KBux7UmyMFGs6Kcjl7aX5jckJ7RU1Glvukoh0zijn4bUQ03A).TypeHandle));
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_28<string>(1270291056u));
		dh18OlujRtC2fZ7NxG9SAeI.MIO3Ksr2DNhtW5NmqDuIAyo(smethod_1(typeof(oIuuMMCo9UmUIWmYFQEqxXAOJy8LYqspmW2FKyHMiXKdcT1cBP8p9QxEsmrfao0MIw.Class25).TypeHandle));
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_29<string>(1202441115u));
		dh18OlujRtC2fZ7NxG9SAeI.MIO3Ksr2DNhtW5NmqDuIAyo(smethod_1(typeof(oIuuMMCo9UmUIWmYFQEqxXAOJy8LYqspmW2FKyHMiXKdcT1cBP8p9QxEsmrfao0MIw.MSyH_0024okjKPSxRMqNzlsd3YBGfjWvxKaiubOn3yUyagF79j3392u8k5zM_3orK9ruNiXmaLy2I7eYP_phgn89XuSYPUvrg0pzE75d6eGNGc_0024WTCNO4PYoSAfad7E9n_fBKA).TypeHandle));
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_28<string>(4229811942u));
		dh18OlujRtC2fZ7NxG9SAeI.MIO3Ksr2DNhtW5NmqDuIAyo(smethod_1(typeof(oIuuMMCo9UmUIWmYFQEqxXAOJy8LYqspmW2FKyHMiXKdcT1cBP8p9QxEsmrfao0MIw.IlK_0024FqFWvguOQcD1hOhuTMmcKoMDQDQGcWJDhR22ZSfJYg6LYWNEc5DYNU55nZjaM9DsEA6xM122tSaC76M0HF6RM_WnspmuSwurV23OvfSCpJ2zYVjb4Ks791U5Si7gIQ).TypeHandle));
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_25<string>(1205731596u));
		dh18OlujRtC2fZ7NxG9SAeI.MIO3Ksr2DNhtW5NmqDuIAyo(smethod_1(typeof(XxAJ0vo10qaLovS4w9cCmko_0024VY60fHaHGA9858Yt4TL0r8NsjpNSMzO89lYqP89_00246EUCpyR3IgNa_0024kpjiKwbu6E).TypeHandle));
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_27<string>(3473339839u));
		dh18OlujRtC2fZ7NxG9SAeI.MIO3Ksr2DNhtW5NmqDuIAyo(smethod_1(typeof(XxAJ0vo10qaLovS4w9cCmko_0024VY60fHaHGA9858Yt4TL0r8NsjpNSMzO89lYqP89_00246EUCpyR3IgNa_0024kpjiKwbu6E.VTCBWB3Ryo2bP94njcplaeAsG5unvSI0dsLCPlzBTGoC_0024Rtr72ooetvguyDPDT5DvLlqkFCGrcAcuPu5lr_LZU2dEhnq6yjA6UBg8E9sOp0I).TypeHandle));
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_28<string>(2575581775u));
		dh18OlujRtC2fZ7NxG9SAeI.MIO3Ksr2DNhtW5NmqDuIAyo(smethod_1(typeof(ExPt_0024JMW8Hl4nzpohpXvahVyl7vQc0OWMdFPWvITtQMR).TypeHandle));
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_27<string>(3040587653u));
		dh18OlujRtC2fZ7NxG9SAeI.MIO3Ksr2DNhtW5NmqDuIAyo(smethod_1(typeof(kR5a7hOtkF_0024_CEj9hhobe_0024UHyUf2kssgviuneZH_0024o76iQx_0024llk9k_0024UbkAShiRzazvQ).TypeHandle));
	}

	internal static void Aab6uSDccQw2pntTBaGy7HzuanyXRRub_0024ffV4hDQeUto()
	{
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_27<string>(3999153375u));
		if (dh18OlujRtC2fZ7NxG9SAeI == null)
		{
			NkE4SICTmcYlwmIfpNh_0024xKw();
		}
		if (!b45w_0024W58SmxYqmBXMdyPewEzBnmFT4EuefSPY7GIofSF)
		{
			mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_29<string>(2003196586u));
			b45w_0024W58SmxYqmBXMdyPewEzBnmFT4EuefSPY7GIofSF = true;
		}
		if (smethod_3() < 5f && MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4 == null)
		{
			mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_29<string>(3355476113u));
			ExPt_0024JMW8Hl4nzpohpXvahVyl7vQc0OWMdFPWvITtQMR.FeUAVwFbW6wGJJdNimZY9yI();
		}
		FMpPDgPqT_0024MlkjDbhXAGLgVbz45OJagoxMsHXXVw14C6.TF_pZa3icvqb3FX9wdJrMOQ();
		if (MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.smoothUI != -1)
		{
			uJXJHpgO70ufC3wCKNGGi54JyfhyZCLleaJHGwdw02RKCMZKGw_0024Hmw3wMZXj_sPFYw.M_0024QnBmhKaMXj6kxXd5wv4R0();
		}
	}

	internal static IEnumerator UGh6GzG7q7xckmUE1fq3sAo()
	{
		SEGI component = UUi9CES6jT0ZzKFkxbFyR2hi3gXjXAxodk4_IlDoeDAh_Isr5yiGHBGg5CHblUybBA.smethod_0().GetComponent<SEGI>();
		while (UUi9CES6jT0ZzKFkxbFyR2hi3gXjXAxodk4_IlDoeDAh_Isr5yiGHBGg5CHblUybBA.smethod_1((UnityEngine.Object)Arena.OEDCBNHNGMJ.FICMBCLEFDL, (UnityEngine.Object)null))
		{
			component.voxelSpaceSize = Arena.OEDCBNHNGMJ.FICMBCLEFDL.BBKOMHJGBPA * 5f;
			yield return null;
		}
	}

	internal static void XUpZW_l_wxjLI0fqNPfEHxI()
	{
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_29<string>(2187037938u));
		smethod_4(dh18OlujRtC2fZ7NxG9SAeI, (string)null);
	}

	internal static Harmony smethod_0(string string_1)
	{
		return new Harmony(string_1);
	}

	internal static Type smethod_1(RuntimeTypeHandle runtimeTypeHandle_0)
	{
		return Type.GetTypeFromHandle(runtimeTypeHandle_0);
	}

	internal static bool smethod_2(string string_1)
	{
		return File.Exists(string_1);
	}

	internal static float smethod_3()
	{
		return Time.fixedTime;
	}

	internal static void smethod_4(Harmony harmony_0, string string_1)
	{
		harmony_0.UnpatchAll(string_1);
	}
}
