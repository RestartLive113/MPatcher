using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using HarmonyLib;
using MPatchrMain;
using McnCraft;
using RestSharp.Contrib;
using Steamworks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

internal class Y_KnsAySLoCDb4SHb_0024ozdg9Jhandbv5CjRS2vvjE93HwxrEnN8gRKN_3gH8ZJ8dFtwLfFaqEhK1SoCTTPpPBgcQ
{
	internal struct U1GmT9ZMi6XvQgaRUcEHGTJ2ox6n_00242ODVjhi9OQlSQvI8A2HT5RLkW_0024sQ5gp2RyZESs49PI9r7mOSRMi5MsAhOI
	{
		internal string hJJJX3iFC8vYXCIx0eUwbqk;

		internal string string_0;

		internal string string_1;

		internal ulong K52jeLH_0024D_rsZoa7xVSPVPk;

		internal string yxqgtybS7ik_0024wDkg_BT5Bpw;

		internal PublishedFileId_t nxch5NN7yn_2gEoq38N_tzo;

		internal bool WZh4cN51oA5qlxlkwH76ZoE;

		internal bool YhiMemOciw_0024oL1zy4ye1Ets;

		internal bool ztm50T3oggPrw0vZ5QDsMCg;

		internal bool Tu1GkDmtr6gDT5y1PaHx4fI;

		internal bool E3t32rKV1_0024UUxFOcPYAfjvg;
	}

	internal static class aIaZtYI7wNQDacAAHL34p0Nl7kMZhMIwBjo5rC4fBnZ8de3uDeiL_0024F1XWjt1eSdJKxXnftNZci6jmYbeKvtCJtklYiGVafxup2rfGZJZKqmo
	{
		[Serializable]
		[CompilerGenerated]
		private sealed class Class28
		{
			public static readonly Class28 _003C_003E9 = new Class28();

			public static Callback<RemoteStoragePublishedFileSubscribed_t>.DispatchDelegate _003C_003E9__3_0;

			public static CallResult<SteamUGCQueryCompleted_t>.APIDispatchDelegate _003C_003E9__3_2;

			public static Callback<DownloadItemResult_t>.DispatchDelegate _003C_003E9__3_1;

			internal void ipCRmORborTUUcVmpdiTZgsZlHgP4Ur0iZb8X8UQZEHA(RemoteStoragePublishedFileSubscribed_t sub)
			{
				MPatchr.ShowDebugMsg(smethod_0(global::_003CModule_003E.smethod_26<string>(773955978u), xh28Je5vGZzmlyaYTv4leqahX_0024d76HU68fsrFhxkr_aiwJvGWxEZr2ULd9ujX_W1gg.UzhVmO15k8UHVkcjd2D2eFw(xh28Je5vGZzmlyaYTv4leqahX_0024d76HU68fsrFhxkr_aiwJvGWxEZr2ULd9ujX_W1gg.eEsL_CqOa2k0Oy16TqJNFVJPuzSIPe8Trw5760Yzq7e5ghLIwg7ZyOeyUc9A2E9z_0024mUEQCN09awhbakTSEXMPkahOFo0RcQ91HbGZkSM_Bhl.workshop_newSubsDownloading)));
				smethod_1(sub.m_nPublishedFileId, bool_0: true);
			}

			internal void iyLJ9qBtGUaXuock5CeqgQC3qNjAob_V4oV_JKcMLT9S(DownloadItemResult_t item)
			{
				if (Q5GS7XFtq4kccIJybzbeAWY == item.m_nPublishedFileId)
				{
					return;
				}
				SteamAPICall_t hAPICall = smethod_3(smethod_2(new PublishedFileId_t[1] { item.m_nPublishedFileId }, 1u));
				CallResult<SteamUGCQueryCompleted_t>.Create(delegate(SteamUGCQueryCompleted_t param, bool fail)
				{
					SteamUGCDetails_t steamUGCDetails_t_ = default(SteamUGCDetails_t);
					smethod_15(param.m_handle, 0u, ref steamUGCDetails_t_);
					MPatchr.ShowDebugMsg(smethod_16(smethod_0(global::_003CModule_003E.smethod_27<string>(25467547u), xh28Je5vGZzmlyaYTv4leqahX_0024d76HU68fsrFhxkr_aiwJvGWxEZr2ULd9ujX_W1gg.UzhVmO15k8UHVkcjd2D2eFw(xh28Je5vGZzmlyaYTv4leqahX_0024d76HU68fsrFhxkr_aiwJvGWxEZr2ULd9ujX_W1gg.eEsL_CqOa2k0Oy16TqJNFVJPuzSIPe8Trw5760Yzq7e5ghLIwg7ZyOeyUc9A2E9z_0024mUEQCN09awhbakTSEXMPkahOFo0RcQ91HbGZkSM_Bhl.workshop_readyToUse)), (object)steamUGCDetails_t_.m_rgchTitle));
				}).Set(hAPICall);
				smethod_4(item.m_nPublishedFileId);
				ulong ulong_ = default(ulong);
				string string_ = default(string);
				uint uint_ = default(uint);
				smethod_5(item.m_nPublishedFileId, ref ulong_, ref string_, 1024u, ref uint_);
				mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(smethod_6(global::_003CModule_003E.smethod_29<string>(4195737836u), new object[5] { item.m_unAppID, item.m_nPublishedFileId, item.m_eResult, ulong_, uint_ }));
				string[] array = smethod_7(string_);
				foreach (string string_2 in array)
				{
					string text = smethod_8(string_2);
					if (smethod_9(string_2, global::_003CModule_003E.smethod_28<string>(405740721u)))
					{
						text = smethod_0(smethod_11(text, 0, smethod_10(text) - 4), global::_003CModule_003E.smethod_26<string>(3946237241u));
					}
					string text2 = smethod_12(JKGKJLLFMLE.LAOHLAOMCPN, global::_003CModule_003E.smethod_26<string>(613572259u), text);
					if (smethod_13(text2))
					{
						mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(smethod_12(global::_003CModule_003E.smethod_26<string>(3227645714u), text, global::_003CModule_003E.smethod_26<string>(459458957u)));
						continue;
					}
					smethod_14(string_2, text2);
					mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(smethod_12(global::_003CModule_003E.smethod_27<string>(165059572u), text, global::_003CModule_003E.smethod_25<string>(543059243u)));
				}
			}

			internal void jLKwVV3Mzyjra0pXXxBFzkwuepOhfwPUZE7bayirk5bX(SteamUGCQueryCompleted_t param, bool fail)
			{
				SteamUGCDetails_t steamUGCDetails_t_ = default(SteamUGCDetails_t);
				smethod_15(param.m_handle, 0u, ref steamUGCDetails_t_);
				MPatchr.ShowDebugMsg(smethod_16(smethod_0(global::_003CModule_003E.smethod_27<string>(25467547u), xh28Je5vGZzmlyaYTv4leqahX_0024d76HU68fsrFhxkr_aiwJvGWxEZr2ULd9ujX_W1gg.UzhVmO15k8UHVkcjd2D2eFw(xh28Je5vGZzmlyaYTv4leqahX_0024d76HU68fsrFhxkr_aiwJvGWxEZr2ULd9ujX_W1gg.eEsL_CqOa2k0Oy16TqJNFVJPuzSIPe8Trw5760Yzq7e5ghLIwg7ZyOeyUc9A2E9z_0024mUEQCN09awhbakTSEXMPkahOFo0RcQ91HbGZkSM_Bhl.workshop_readyToUse)), (object)steamUGCDetails_t_.m_rgchTitle));
			}

			internal static string smethod_0(string string_0, string string_1)
			{
				return string_0 + string_1;
			}

			internal static bool smethod_1(PublishedFileId_t publishedFileId_t_0, bool bool_0)
			{
				return SteamUGC.DownloadItem(publishedFileId_t_0, bool_0);
			}

			internal static UGCQueryHandle_t smethod_2(PublishedFileId_t[] publishedFileId_t_0, uint uint_0)
			{
				return SteamUGC.CreateQueryUGCDetailsRequest(publishedFileId_t_0, uint_0);
			}

			internal static SteamAPICall_t smethod_3(UGCQueryHandle_t ugcqueryHandle_t_0)
			{
				return SteamUGC.SendQueryUGCRequest(ugcqueryHandle_t_0);
			}

			internal static uint smethod_4(PublishedFileId_t publishedFileId_t_0)
			{
				return SteamUGC.GetItemState(publishedFileId_t_0);
			}

			internal static bool smethod_5(PublishedFileId_t publishedFileId_t_0, ref ulong ulong_0, ref string string_0, uint uint_0, ref uint uint_1)
			{
				return SteamUGC.GetItemInstallInfo(publishedFileId_t_0, out ulong_0, out string_0, uint_0, out uint_1);
			}

			internal static string smethod_6(string string_0, object[] object_0)
			{
				return string.Format(string_0, object_0);
			}

			internal static string[] smethod_7(string string_0)
			{
				return Directory.GetFiles(string_0);
			}

			internal static string smethod_8(string string_0)
			{
				return Path.GetFileName(string_0);
			}

			internal static bool smethod_9(string string_0, string string_1)
			{
				return string_0.EndsWith(string_1);
			}

			internal static int smethod_10(string string_0)
			{
				return string_0.Length;
			}

			internal static string smethod_11(string string_0, int int_0, int int_1)
			{
				return string_0.Substring(int_0, int_1);
			}

			internal static string smethod_12(string string_0, string string_1, string string_2)
			{
				return string_0 + string_1 + string_2;
			}

			internal static bool smethod_13(string string_0)
			{
				return File.Exists(string_0);
			}

			internal static void smethod_14(string string_0, string string_1)
			{
				File.Copy(string_0, string_1);
			}

			internal static bool smethod_15(UGCQueryHandle_t ugcqueryHandle_t_0, uint uint_0, ref SteamUGCDetails_t steamUGCDetails_t_0)
			{
				return SteamUGC.GetQueryUGCResult(ugcqueryHandle_t_0, uint_0, out steamUGCDetails_t_0);
			}

			internal static string smethod_16(string string_0, object object_0)
			{
				return string.Format(string_0, object_0);
			}
		}

		internal static Callback<RemoteStoragePublishedFileSubscribed_t> callback_0;

		internal static Callback<RemoteStorageUnsubscribePublishedFileResult_t> callback_1;

		internal static Callback<DownloadItemResult_t> lUw18lpkGTVm1og49zTlnjxuEgUnqjyUdjZtFl666QxW;

		internal static void KnAOJbw49k2cqOyUYNexTSY()
		{
			callback_0 = Callback<RemoteStoragePublishedFileSubscribed_t>.Create(delegate(RemoteStoragePublishedFileSubscribed_t sub)
			{
				MPatchr.ShowDebugMsg(Class28.smethod_0(global::_003CModule_003E.smethod_26<string>(773955978u), xh28Je5vGZzmlyaYTv4leqahX_0024d76HU68fsrFhxkr_aiwJvGWxEZr2ULd9ujX_W1gg.UzhVmO15k8UHVkcjd2D2eFw(xh28Je5vGZzmlyaYTv4leqahX_0024d76HU68fsrFhxkr_aiwJvGWxEZr2ULd9ujX_W1gg.eEsL_CqOa2k0Oy16TqJNFVJPuzSIPe8Trw5760Yzq7e5ghLIwg7ZyOeyUc9A2E9z_0024mUEQCN09awhbakTSEXMPkahOFo0RcQ91HbGZkSM_Bhl.workshop_newSubsDownloading)));
				Class28.smethod_1(sub.m_nPublishedFileId, bool_0: true);
			});
			lUw18lpkGTVm1og49zTlnjxuEgUnqjyUdjZtFl666QxW = Callback<DownloadItemResult_t>.Create(delegate(DownloadItemResult_t item)
			{
				if (!(Q5GS7XFtq4kccIJybzbeAWY == item.m_nPublishedFileId))
				{
					SteamAPICall_t hAPICall = Class28.smethod_3(Class28.smethod_2(new PublishedFileId_t[1] { item.m_nPublishedFileId }, 1u));
					CallResult<SteamUGCQueryCompleted_t>.Create(delegate(SteamUGCQueryCompleted_t param, bool fail)
					{
						SteamUGCDetails_t steamUGCDetails_t_ = default(SteamUGCDetails_t);
						Class28.smethod_15(param.m_handle, 0u, ref steamUGCDetails_t_);
						MPatchr.ShowDebugMsg(Class28.smethod_16(Class28.smethod_0(global::_003CModule_003E.smethod_27<string>(25467547u), xh28Je5vGZzmlyaYTv4leqahX_0024d76HU68fsrFhxkr_aiwJvGWxEZr2ULd9ujX_W1gg.UzhVmO15k8UHVkcjd2D2eFw(xh28Je5vGZzmlyaYTv4leqahX_0024d76HU68fsrFhxkr_aiwJvGWxEZr2ULd9ujX_W1gg.eEsL_CqOa2k0Oy16TqJNFVJPuzSIPe8Trw5760Yzq7e5ghLIwg7ZyOeyUc9A2E9z_0024mUEQCN09awhbakTSEXMPkahOFo0RcQ91HbGZkSM_Bhl.workshop_readyToUse)), (object)steamUGCDetails_t_.m_rgchTitle));
					}).Set(hAPICall);
					Class28.smethod_4(item.m_nPublishedFileId);
					ulong ulong_ = default(ulong);
					string string_ = default(string);
					uint uint_ = default(uint);
					Class28.smethod_5(item.m_nPublishedFileId, ref ulong_, ref string_, 1024u, ref uint_);
					mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(Class28.smethod_6(global::_003CModule_003E.smethod_29<string>(4195737836u), new object[5] { item.m_unAppID, item.m_nPublishedFileId, item.m_eResult, ulong_, uint_ }));
					string[] array = Class28.smethod_7(string_);
					foreach (string string_2 in array)
					{
						string text = Class28.smethod_8(string_2);
						if (Class28.smethod_9(string_2, global::_003CModule_003E.smethod_28<string>(405740721u)))
						{
							text = Class28.smethod_0(Class28.smethod_11(text, 0, Class28.smethod_10(text) - 4), global::_003CModule_003E.smethod_26<string>(3946237241u));
						}
						string text2 = Class28.smethod_12(JKGKJLLFMLE.LAOHLAOMCPN, global::_003CModule_003E.smethod_26<string>(613572259u), text);
						if (Class28.smethod_13(text2))
						{
							mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(Class28.smethod_12(global::_003CModule_003E.smethod_26<string>(3227645714u), text, global::_003CModule_003E.smethod_26<string>(459458957u)));
						}
						else
						{
							Class28.smethod_14(string_2, text2);
							mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(Class28.smethod_12(global::_003CModule_003E.smethod_27<string>(165059572u), text, global::_003CModule_003E.smethod_25<string>(543059243u)));
						}
					}
				}
			});
		}
	}

	[HarmonyPatch("Initialize")]
	[HarmonyPatch(typeof(MachineController))]
	internal class BmX9fkX90Trh4MiCFQ9HUq6Bf23SL0OB3yAPfwsaL7EbEGh22P6F6ygh7saJ5JgFCpe49M01gAXl8wnxSj3NQcZaFmNbdCsuuk37PKwTmIWo
	{
		internal static AssignData KAhaFpfVvyK7iQzKoWEe86c;

		[HarmonyPrefix]
		internal static void smethod_0(ref BuildData IHMCFFHELHL, ref AssignData HMILIMPBBCB)
		{
			if (y5kmSOqVaOjhMvxRjeJAZmo != null)
			{
				IHMCFFHELHL = y5kmSOqVaOjhMvxRjeJAZmo;
				if (nu9wqOavhI9USqcxKp2D2JdN6BvnIdeDULV1uF0wgOAy != null)
				{
					HMILIMPBBCB = nu9wqOavhI9USqcxKp2D2JdN6BvnIdeDULV1uF0wgOAy;
				}
				else
				{
					HMILIMPBBCB = smethod_1();
				}
				KAhaFpfVvyK7iQzKoWEe86c = smethod_2(JKGKJLLFMLE.MIIGKEBFKKD);
				JKGKJLLFMLE.MIIGKEBFKKD = HMILIMPBBCB;
			}
		}

		internal static AssignData smethod_1()
		{
			return new AssignData();
		}

		internal static AssignData smethod_2(AssignData assignData_0)
		{
			return assignData_0.Clone();
		}
	}

	[HarmonyPatch(new Type[]
	{
		typeof(string),
		typeof(GameObject)
	})]
	[HarmonyPatch("OnSelect")]
	[HarmonyPatch(typeof(SceneMan))]
	internal class NITp1tLbTWES2Ob6kMhtDtPwvH602qGymXMfH_0024CEMgKuAv7OVF9f0mzKSxZ1NgPwsZSvWzyjeshABfM0CJHrny5nf_yHLSKeM0DEKstOpERjwFRcTI6of8GIKwWM2oUMhjtrHr4gOeVSd4wCkZ9eTa8
	{
		[HarmonyPostfix]
		internal static void FeUAVwFbW6wGJJdNimZY9yI(string DPGKEOAGONA, GameObject NGLBLAGMBLN)
		{
			if (smethod_0().name == global::_003CModule_003E.smethod_26<string>(1973698662u))
			{
				Class29.zzabkMO3HsNHze3BG8AoLTE(DPGKEOAGONA);
			}
		}

		internal static Scene smethod_0()
		{
			return SceneManager.GetActiveScene();
		}
	}

	[HarmonyPatch(typeof(Workshop))]
	[HarmonyPatch("Start")]
	internal class Class29
	{
		internal enum QtKrB_00245y1Dl8GOwmq4Cab2Ho5BtrUNpXBsRzTZc0BRPLuH9xbAqmxxuctYjiK_00242PJpDrdVkZg7aEYkNtowdef01kwJy9gX7OJUNVndZBA5zmp72EA58FF5Suwpw2rjkEBKEwkRotAnuGmQG2PuD73gdKkpk6pY1DpnAPK90qOPnRhf4dMEoD5L7EimuUBNrWs4s9U039qAPIH829tmZ634Z6yPPCzbr4nBZ_Xb9LphG_00247XY9_0024tPCwbfCK_Sz3daDdeFmVTyiZwVC9KpLqM4c0x4p5Sa2WyCUEcE5oIg9Lg7gYq4YkevvxoCqc7TXK56URNbWpjJZXz3fJ5fCqeJcL6g
		{
			MostPopular,
			MostRecent,
			MostSubscribed
		}

		internal enum QI4dAcq_0024lvENeJ7F01V8DuUyACR_6ygoX3OmWG6t7bRsI1oTQdanHscQNaT6Ys5jc1j8cHirIS8ImlW7t8bxlx7xKSJOIZM9uce9OxmKwEeX
		{
			Today,
			PastWeek,
			ThreeMonths,
			SixMonths,
			OneYear,
			AllTime
		}

		[Serializable]
		[CompilerGenerated]
		private sealed class LHwQmhRSazqcfLZmKoRoR_xZ6BkldlRECYZpOx2FFxPfzc79pNqUfFHGOEfv8AXYiLydB2ouSrpJIWzYKwlwWjw60LLWZrYC_0024A5dpnJV9MQFTyrq79ROvXEBb_0024MUpBfb0h_0024VZFjWza9oEFZmihVJF_00246c09RP_C_TdYCkeXSm_0024pOyReP_HrA_GEMNJeTZmYGJSQVQkwxUQqykgF_0024LKwXTzGu1xJQzBi50SD5T6VL1d1o1cm_0024ERBz97WUEunMpuqzoCAzX0IW0O8AqfLHixPO0Jrjy6FaUSnecLNNDdmdqhdm2V8QTglSwmSmZGuOW_0024OsBSxP_0024SgWfHd2zgA3je9ikDd0
		{
			public static readonly LHwQmhRSazqcfLZmKoRoR_xZ6BkldlRECYZpOx2FFxPfzc79pNqUfFHGOEfv8AXYiLydB2ouSrpJIWzYKwlwWjw60LLWZrYC_0024A5dpnJV9MQFTyrq79ROvXEBb_0024MUpBfb0h_0024VZFjWza9oEFZmihVJF_00246c09RP_C_TdYCkeXSm_0024pOyReP_HrA_GEMNJeTZmYGJSQVQkwxUQqykgF_0024LKwXTzGu1xJQzBi50SD5T6VL1d1o1cm_0024ERBz97WUEunMpuqzoCAzX0IW0O8AqfLHixPO0Jrjy6FaUSnecLNNDdmdqhdm2V8QTglSwmSmZGuOW_0024OsBSxP_0024SgWfHd2zgA3je9ikDd0 _003C_003E9 = new LHwQmhRSazqcfLZmKoRoR_xZ6BkldlRECYZpOx2FFxPfzc79pNqUfFHGOEfv8AXYiLydB2ouSrpJIWzYKwlwWjw60LLWZrYC_0024A5dpnJV9MQFTyrq79ROvXEBb_0024MUpBfb0h_0024VZFjWza9oEFZmihVJF_00246c09RP_C_TdYCkeXSm_0024pOyReP_HrA_GEMNJeTZmYGJSQVQkwxUQqykgF_0024LKwXTzGu1xJQzBi50SD5T6VL1d1o1cm_0024ERBz97WUEunMpuqzoCAzX0IW0O8AqfLHixPO0Jrjy6FaUSnecLNNDdmdqhdm2V8QTglSwmSmZGuOW_0024OsBSxP_0024SgWfHd2zgA3je9ikDd0();

			public static Action<lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw> _003C_003E9__21_6;

			public static UnityAction<string> _003C_003E9__21_0;

			public static Action<lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw> _003C_003E9__21_1;

			public static Action<lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw> _003C_003E9__21_2;

			public static Action<lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw> _003C_003E9__21_3;

			public static Action<lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw> _003C_003E9__21_4;

			public static Action<lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw> _003C_003E9__21_5;

			internal void method_0(lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw me)
			{
				OB3U2zm9e2wliliNbaRFPAY = false;
				ATCyo_0024qpPFqNSISN7PgDzuPM7bZZWP2fcNoTkefIy5A2WLvcWT1w9UCw3Yq7lMwlug.YMAh_TshgArfNEGhpCaDjcg(global::_003CModule_003E.smethod_29<string>(2973012298u), bool_0: false);
			}

			internal void method_1(string text)
			{
				cGcAEvkY80q7_lj2YRmI1uk((QtKrB_00245y1Dl8GOwmq4Cab2Ho5BtrUNpXBsRzTZc0BRPLuH9xbAqmxxuctYjiK_00242PJpDrdVkZg7aEYkNtowdef01kwJy9gX7OJUNVndZBA5zmp72EA58FF5Suwpw2rjkEBKEwkRotAnuGmQG2PuD73gdKkpk6pY1DpnAPK90qOPnRhf4dMEoD5L7EimuUBNrWs4s9U039qAPIH829tmZ634Z6yPPCzbr4nBZ_Xb9LphG_00247XY9_0024tPCwbfCK_Sz3daDdeFmVTyiZwVC9KpLqM4c0x4p5Sa2WyCUEcE5oIg9Lg7gYq4YkevvxoCqc7TXK56URNbWpjJZXz3fJ5fCqeJcL6g)listController_0.EeNYggqZWtiar0rSt8jVD1eSfb4J4GSFkxD2kxLGahLU(), (QI4dAcq_0024lvENeJ7F01V8DuUyACR_6ygoX3OmWG6t7bRsI1oTQdanHscQNaT6Ys5jc1j8cHirIS8ImlW7t8bxlx7xKSJOIZM9uce9OxmKwEeX)listController_1.EeNYggqZWtiar0rSt8jVD1eSfb4J4GSFkxD2kxLGahLU(), text);
			}

			internal void method_2(lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw btn)
			{
				OB3U2zm9e2wliliNbaRFPAY = true;
				ATCyo_0024qpPFqNSISN7PgDzuPM7bZZWP2fcNoTkefIy5A2WLvcWT1w9UCw3Yq7lMwlug.YMAh_TshgArfNEGhpCaDjcg(global::_003CModule_003E.smethod_27<string>(2929116935u), bool_0: false);
			}

			internal void method_3(lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw btn)
			{
				lZKkq6Pc8pSN7ZBtxX7pR4s--;
				B7E3yGIUYRHvMdXen005f5U();
			}

			internal void method_4(lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw btn)
			{
				lZKkq6Pc8pSN7ZBtxX7pR4s++;
				B7E3yGIUYRHvMdXen005f5U();
			}

			internal void method_5(lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw btn)
			{
				smethod_0(kk_0024srNdLfgKNQLYd7jaYjyg, bool_0: false);
			}

			internal void method_6(lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw btn)
			{
				Y_KnsAySLoCDb4SHb_0024ozdg9Jhandbv5CjRS2vvjE93HwxrEnN8gRKN_3gH8ZJ8dFtwLfFaqEhK1SoCTTPpPBgcQ.smethod_0();
			}

			internal static void smethod_0(GameObject gameObject_0, bool bool_0)
			{
				gameObject_0.SetActive(bool_0);
			}
		}

		private const float Vhc1ua3e6V_0024uH2u7ym3m_00249s = 195f;

		private const float VxImP_0024bRSeWB8I_lVc_TsRs = -260f;

		private const float znvDi_0024COYvSwhozbUDmlAxI = -395f;

		private const float z5lfOJsrnr4ma1axPfVZpTc = 150f;

		private static ListController listController_0;

		private static ListController listController_1;

		private static ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ _00247xYLrKQKiHUVJb7_YKTEhk;

		private static ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ bLGoufWxxTjuUVy_eBPqqyU;

		private static lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw EawkyrVySrwUz8iXwAy3a1o;

		private static lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw PWShNz5mPUMTrvEbX1xUUO0;

		private static lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw VCDa6P1xHed_0024OYqYthk5O6Y;

		private static Text CELyhJyLzVHZGI0KsTiq3fw;

		internal static GameObject kk_0024srNdLfgKNQLYd7jaYjyg;

		private static RawImage pzHT1hxUS4UVu_0024PfHCqJQgc;

		private static RenderTexture _0024YQ79Bjv_SyJSjOLx19tt7oyA4rPCEedq1vzGQYyRjwM;

		private static Camera UaU8O_0024S38WSWP9sBbjmmhtQ;

		private static List<lNpIzpGWMiPQ_5gAwjtlI6S6Js86Pudxf8nfbTsW_0024TcvJlB0LYzijk9i4IYc3PbHGAzwIeWv5bbP_0024YvJI16Sino> H_VsQ_AEW_0024pgd7LYesvnpyg;

		private static List<U1GmT9ZMi6XvQgaRUcEHGTJ2ox6n_00242ODVjhi9OQlSQvI8A2HT5RLkW_0024sQ5gp2RyZESs49PI9r7mOSRMi5MsAhOI> pPX_nHY_MIGP3ftDkHfjs8E = new List<U1GmT9ZMi6XvQgaRUcEHGTJ2ox6n_00242ODVjhi9OQlSQvI8A2HT5RLkW_0024sQ5gp2RyZESs49PI9r7mOSRMi5MsAhOI>();

		private static int lZKkq6Pc8pSN7ZBtxX7pR4s = 0;

		[HarmonyPrefix]
		internal static bool smethod_0(Workshop __instance)
		{
			if (OB3U2zm9e2wliliNbaRFPAY)
			{
				GameObject gameObject = smethod_1(global::_003CModule_003E.smethod_26<string>(3103631283u)).smethod_0(global::_003CModule_003E.smethod_26<string>(3185982180u));
				smethod_2((UnityEngine.Object)gameObject.GetComponent<ButtonController>());
				gameObject.AddComponent<lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw>().t2iJT_tBPyB6QRMBLAdXYUs(delegate
				{
					OB3U2zm9e2wliliNbaRFPAY = false;
					ATCyo_0024qpPFqNSISN7PgDzuPM7bZZWP2fcNoTkefIy5A2WLvcWT1w9UCw3Yq7lMwlug.YMAh_TshgArfNEGhpCaDjcg(global::_003CModule_003E.smethod_29<string>(2973012298u), bool_0: false);
				});
				return true;
			}
			BNY2Hoxd9qRiNISDjVrLHPM = null;
			H_VsQ_AEW_0024pgd7LYesvnpyg = new List<lNpIzpGWMiPQ_5gAwjtlI6S6Js86Pudxf8nfbTsW_0024TcvJlB0LYzijk9i4IYc3PbHGAzwIeWv5bbP_0024YvJI16Sino>();
			pPX_nHY_MIGP3ftDkHfjs8E.Clear();
			lZKkq6Pc8pSN7ZBtxX7pR4s = 0;
			xH535ybWa6_0024ItQpcTo8vW9E = smethod_3();
			Class17.DMoImhPC2jNUt2tgmSezeT_IK_0024XK5Df9PmexG_CaKb1R(global::_003CModule_003E.smethod_25<string>(1070593527u), (object)__instance, smethod_4());
			smethod_5(bool_0: false);
			smethod_6(bool_0: true);
			GameObject gameObject_ = smethod_1(global::_003CModule_003E.smethod_29<string>(618767629u));
			smethod_7(gameObject_.smethod_0(global::_003CModule_003E.smethod_27<string>(1509846805u)), bool_0: false);
			smethod_7(gameObject_.smethod_0(global::_003CModule_003E.smethod_26<string>(738562861u)), bool_0: false);
			smethod_7(gameObject_.smethod_0(global::_003CModule_003E.smethod_28<string>(1331639887u)), bool_0: false);
			smethod_7(gameObject_.smethod_0(global::_003CModule_003E.smethod_27<string>(1416785455u)), bool_0: false);
			smethod_7(gameObject_.smethod_0(global::_003CModule_003E.smethod_29<string>(2800273646u)), bool_0: false);
			smethod_7(gameObject_.smethod_0(global::_003CModule_003E.smethod_26<string>(55364451u)), bool_0: false);
			smethod_7(gameObject_.smethod_0(global::_003CModule_003E.smethod_29<string>(993942089u)), bool_0: false);
			smethod_7(gameObject_.smethod_0(global::_003CModule_003E.smethod_27<string>(3966767896u)), bool_0: false);
			smethod_7(gameObject_.smethod_0(global::_003CModule_003E.smethod_26<string>(4154554911u)), bool_0: false);
			smethod_7(gameObject_.smethod_0(global::_003CModule_003E.smethod_26<string>(1344704620u)), bool_0: false);
			GameObject gameObject2 = gameObject_.smethod_0(global::_003CModule_003E.smethod_27<string>(565088109u));
			for (int num = 0; num < smethod_13(smethod_8(gameObject2)); num++)
			{
				if (smethod_11(smethod_10((UnityEngine.Object)smethod_9(smethod_8(gameObject2), num)), global::_003CModule_003E.smethod_28<string>(1453003686u)))
				{
					smethod_7(smethod_12((Component)smethod_9(smethod_8(gameObject2), num)), bool_0: false);
				}
			}
			_00247xYLrKQKiHUVJb7_YKTEhk = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.bqYYMQnP2SDqYH85wmN_0024evI(global::_003CModule_003E.smethod_26<string>(1546751873u), new Vector3(110f, 310f), "", global::_003CModule_003E.smethod_27<string>(2328820502u), gameObject2.transform);
			_00247xYLrKQKiHUVJb7_YKTEhk.BSdnl9DYm6Rd4cVhJ555c_A.onEndEdit.AddListener(delegate(string text)
			{
				cGcAEvkY80q7_lj2YRmI1uk((QtKrB_00245y1Dl8GOwmq4Cab2Ho5BtrUNpXBsRzTZc0BRPLuH9xbAqmxxuctYjiK_00242PJpDrdVkZg7aEYkNtowdef01kwJy9gX7OJUNVndZBA5zmp72EA58FF5Suwpw2rjkEBKEwkRotAnuGmQG2PuD73gdKkpk6pY1DpnAPK90qOPnRhf4dMEoD5L7EimuUBNrWs4s9U039qAPIH829tmZ634Z6yPPCzbr4nBZ_Xb9LphG_00247XY9_0024tPCwbfCK_Sz3daDdeFmVTyiZwVC9KpLqM4c0x4p5Sa2WyCUEcE5oIg9Lg7gYq4YkevvxoCqc7TXK56URNbWpjJZXz3fJ5fCqeJcL6g)listController_0.EeNYggqZWtiar0rSt8jVD1eSfb4J4GSFkxD2kxLGahLU(), (QI4dAcq_0024lvENeJ7F01V8DuUyACR_6ygoX3OmWG6t7bRsI1oTQdanHscQNaT6Ys5jc1j8cHirIS8ImlW7t8bxlx7xKSJOIZM9uce9OxmKwEeX)listController_1.EeNYggqZWtiar0rSt8jVD1eSfb4J4GSFkxD2kxLGahLU(), text);
			});
			VCDa6P1xHed_0024OYqYthk5O6Y = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.P3tLroX6fcPfQha_0024JdbpYXg(global::_003CModule_003E.smethod_29<string>(1318440130u), new Vector3(115f, -250f), global::_003CModule_003E.smethod_29<string>(1177783441u), delegate
			{
				OB3U2zm9e2wliliNbaRFPAY = true;
				ATCyo_0024qpPFqNSISN7PgDzuPM7bZZWP2fcNoTkefIy5A2WLvcWT1w9UCw3Yq7lMwlug.YMAh_TshgArfNEGhpCaDjcg(global::_003CModule_003E.smethod_27<string>(2929116935u), bool_0: false);
			}, gameObject2.transform);
			listController_0 = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.nN2N4qjnQLwFOaONUPeRAdg(global::_003CModule_003E.smethod_25<string>(4219924003u), xh28Je5vGZzmlyaYTv4leqahX_0024d76HU68fsrFhxkr_aiwJvGWxEZr2ULd9ujX_W1gg.UzhVmO15k8UHVkcjd2D2eFw(xh28Je5vGZzmlyaYTv4leqahX_0024d76HU68fsrFhxkr_aiwJvGWxEZr2ULd9ujX_W1gg.eEsL_CqOa2k0Oy16TqJNFVJPuzSIPe8Trw5760Yzq7e5ghLIwg7ZyOeyUc9A2E9z_0024mUEQCN09awhbakTSEXMPkahOFo0RcQ91HbGZkSM_Bhl.workshop_SortBy), new Vector3(110f, 150f), new string[3]
			{
				xh28Je5vGZzmlyaYTv4leqahX_0024d76HU68fsrFhxkr_aiwJvGWxEZr2ULd9ujX_W1gg.UzhVmO15k8UHVkcjd2D2eFw(xh28Je5vGZzmlyaYTv4leqahX_0024d76HU68fsrFhxkr_aiwJvGWxEZr2ULd9ujX_W1gg.eEsL_CqOa2k0Oy16TqJNFVJPuzSIPe8Trw5760Yzq7e5ghLIwg7ZyOeyUc9A2E9z_0024mUEQCN09awhbakTSEXMPkahOFo0RcQ91HbGZkSM_Bhl.workshop_sortPopular),
				xh28Je5vGZzmlyaYTv4leqahX_0024d76HU68fsrFhxkr_aiwJvGWxEZr2ULd9ujX_W1gg.UzhVmO15k8UHVkcjd2D2eFw(xh28Je5vGZzmlyaYTv4leqahX_0024d76HU68fsrFhxkr_aiwJvGWxEZr2ULd9ujX_W1gg.eEsL_CqOa2k0Oy16TqJNFVJPuzSIPe8Trw5760Yzq7e5ghLIwg7ZyOeyUc9A2E9z_0024mUEQCN09awhbakTSEXMPkahOFo0RcQ91HbGZkSM_Bhl.workshop_sortRecent),
				xh28Je5vGZzmlyaYTv4leqahX_0024d76HU68fsrFhxkr_aiwJvGWxEZr2ULd9ujX_W1gg.UzhVmO15k8UHVkcjd2D2eFw(xh28Je5vGZzmlyaYTv4leqahX_0024d76HU68fsrFhxkr_aiwJvGWxEZr2ULd9ujX_W1gg.eEsL_CqOa2k0Oy16TqJNFVJPuzSIPe8Trw5760Yzq7e5ghLIwg7ZyOeyUc9A2E9z_0024mUEQCN09awhbakTSEXMPkahOFo0RcQ91HbGZkSM_Bhl.workshop_sortMostSubscribed)
			}, gameObject2.transform);
			listController_1 = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.nN2N4qjnQLwFOaONUPeRAdg(global::_003CModule_003E.smethod_27<string>(3147794199u), xh28Je5vGZzmlyaYTv4leqahX_0024d76HU68fsrFhxkr_aiwJvGWxEZr2ULd9ujX_W1gg.UzhVmO15k8UHVkcjd2D2eFw(xh28Je5vGZzmlyaYTv4leqahX_0024d76HU68fsrFhxkr_aiwJvGWxEZr2ULd9ujX_W1gg.eEsL_CqOa2k0Oy16TqJNFVJPuzSIPe8Trw5760Yzq7e5ghLIwg7ZyOeyUc9A2E9z_0024mUEQCN09awhbakTSEXMPkahOFo0RcQ91HbGZkSM_Bhl.workshop_OverTime), new Vector3(110f, -30f), new string[6]
			{
				xh28Je5vGZzmlyaYTv4leqahX_0024d76HU68fsrFhxkr_aiwJvGWxEZr2ULd9ujX_W1gg.UzhVmO15k8UHVkcjd2D2eFw(xh28Je5vGZzmlyaYTv4leqahX_0024d76HU68fsrFhxkr_aiwJvGWxEZr2ULd9ujX_W1gg.eEsL_CqOa2k0Oy16TqJNFVJPuzSIPe8Trw5760Yzq7e5ghLIwg7ZyOeyUc9A2E9z_0024mUEQCN09awhbakTSEXMPkahOFo0RcQ91HbGZkSM_Bhl.workshop_sortTimeToday),
				xh28Je5vGZzmlyaYTv4leqahX_0024d76HU68fsrFhxkr_aiwJvGWxEZr2ULd9ujX_W1gg.UzhVmO15k8UHVkcjd2D2eFw(xh28Je5vGZzmlyaYTv4leqahX_0024d76HU68fsrFhxkr_aiwJvGWxEZr2ULd9ujX_W1gg.eEsL_CqOa2k0Oy16TqJNFVJPuzSIPe8Trw5760Yzq7e5ghLIwg7ZyOeyUc9A2E9z_0024mUEQCN09awhbakTSEXMPkahOFo0RcQ91HbGZkSM_Bhl.workshop_sortTimePastWeek),
				xh28Je5vGZzmlyaYTv4leqahX_0024d76HU68fsrFhxkr_aiwJvGWxEZr2ULd9ujX_W1gg.UzhVmO15k8UHVkcjd2D2eFw(xh28Je5vGZzmlyaYTv4leqahX_0024d76HU68fsrFhxkr_aiwJvGWxEZr2ULd9ujX_W1gg.eEsL_CqOa2k0Oy16TqJNFVJPuzSIPe8Trw5760Yzq7e5ghLIwg7ZyOeyUc9A2E9z_0024mUEQCN09awhbakTSEXMPkahOFo0RcQ91HbGZkSM_Bhl.workshop_sortTime3mo),
				xh28Je5vGZzmlyaYTv4leqahX_0024d76HU68fsrFhxkr_aiwJvGWxEZr2ULd9ujX_W1gg.UzhVmO15k8UHVkcjd2D2eFw(xh28Je5vGZzmlyaYTv4leqahX_0024d76HU68fsrFhxkr_aiwJvGWxEZr2ULd9ujX_W1gg.eEsL_CqOa2k0Oy16TqJNFVJPuzSIPe8Trw5760Yzq7e5ghLIwg7ZyOeyUc9A2E9z_0024mUEQCN09awhbakTSEXMPkahOFo0RcQ91HbGZkSM_Bhl.workshop_sortTime6mo),
				xh28Je5vGZzmlyaYTv4leqahX_0024d76HU68fsrFhxkr_aiwJvGWxEZr2ULd9ujX_W1gg.UzhVmO15k8UHVkcjd2D2eFw(xh28Je5vGZzmlyaYTv4leqahX_0024d76HU68fsrFhxkr_aiwJvGWxEZr2ULd9ujX_W1gg.eEsL_CqOa2k0Oy16TqJNFVJPuzSIPe8Trw5760Yzq7e5ghLIwg7ZyOeyUc9A2E9z_0024mUEQCN09awhbakTSEXMPkahOFo0RcQ91HbGZkSM_Bhl.workshop_sortTime1y),
				xh28Je5vGZzmlyaYTv4leqahX_0024d76HU68fsrFhxkr_aiwJvGWxEZr2ULd9ujX_W1gg.UzhVmO15k8UHVkcjd2D2eFw(xh28Je5vGZzmlyaYTv4leqahX_0024d76HU68fsrFhxkr_aiwJvGWxEZr2ULd9ujX_W1gg.eEsL_CqOa2k0Oy16TqJNFVJPuzSIPe8Trw5760Yzq7e5ghLIwg7ZyOeyUc9A2E9z_0024mUEQCN09awhbakTSEXMPkahOFo0RcQ91HbGZkSM_Bhl.workshop_sortTimeAll)
			}, gameObject2.transform);
			listController_0.transform.parent.localScale = Vector3.one;
			listController_1.transform.parent.localScale = Vector3.one;
			listController_1.FdKQJ0_0024IXUMd2Sl2psAEa2aauKmVEPLpTQDj8u9Kpq6w(1);
			GameObject gameObject3 = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.qSsYOsEQb7x452a9Y45dVEk(new Vector3(100f, 10f), new Vector2(1025f, 600f));
			EawkyrVySrwUz8iXwAy3a1o = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.P3tLroX6fcPfQha_0024JdbpYXg(global::_003CModule_003E.smethod_28<string>(2727842300u), new Vector3(-390f, -260f), global::_003CModule_003E.smethod_25<string>(584374562u), delegate
			{
				lZKkq6Pc8pSN7ZBtxX7pR4s--;
				B7E3yGIUYRHvMdXen005f5U();
			}, gameObject3.transform);
			PWShNz5mPUMTrvEbX1xUUO0 = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.P3tLroX6fcPfQha_0024JdbpYXg(global::_003CModule_003E.smethod_28<string>(2272542795u), new Vector3(390f, -260f), global::_003CModule_003E.smethod_25<string>(1985840910u), delegate
			{
				lZKkq6Pc8pSN7ZBtxX7pR4s++;
				B7E3yGIUYRHvMdXen005f5U();
			}, gameObject3.transform);
			CELyhJyLzVHZGI0KsTiq3fw = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.YU_pwpP3pKH76IHZTea_SXk(global::_003CModule_003E.smethod_29<string>(2389406279u), new Vector3(100f, -280f), string.Format(xh28Je5vGZzmlyaYTv4leqahX_0024d76HU68fsrFhxkr_aiwJvGWxEZr2ULd9ujX_W1gg.UzhVmO15k8UHVkcjd2D2eFw(xh28Je5vGZzmlyaYTv4leqahX_0024d76HU68fsrFhxkr_aiwJvGWxEZr2ULd9ujX_W1gg.eEsL_CqOa2k0Oy16TqJNFVJPuzSIPe8Trw5760Yzq7e5ghLIwg7ZyOeyUc9A2E9z_0024mUEQCN09awhbakTSEXMPkahOFo0RcQ91HbGZkSM_Bhl.workshop_page), 1, 5), gameObject3.transform, rmOutline: false, -1, FontStyle.Normal, TextAnchor.MiddleCenter).GetComponent<Text>();
			float y = 150f;
			MziaTDkwFA_9ZACCxSD3QPI(new Vector3(-395f, y), gameObject3.transform);
			MziaTDkwFA_9ZACCxSD3QPI(new Vector3(-200f, y), gameObject3.transform);
			MziaTDkwFA_9ZACCxSD3QPI(new Vector3(-5f, y), gameObject3.transform);
			MziaTDkwFA_9ZACCxSD3QPI(new Vector3(190f, y), gameObject3.transform);
			MziaTDkwFA_9ZACCxSD3QPI(new Vector3(385f, y), gameObject3.transform);
			y = -110f;
			MziaTDkwFA_9ZACCxSD3QPI(new Vector3(-395f, y), gameObject3.transform);
			MziaTDkwFA_9ZACCxSD3QPI(new Vector3(-200f, y), gameObject3.transform);
			MziaTDkwFA_9ZACCxSD3QPI(new Vector3(-5f, y), gameObject3.transform);
			MziaTDkwFA_9ZACCxSD3QPI(new Vector3(190f, y), gameObject3.transform);
			MziaTDkwFA_9ZACCxSD3QPI(new Vector3(385f, y), gameObject3.transform);
			cGcAEvkY80q7_lj2YRmI1uk((QtKrB_00245y1Dl8GOwmq4Cab2Ho5BtrUNpXBsRzTZc0BRPLuH9xbAqmxxuctYjiK_00242PJpDrdVkZg7aEYkNtowdef01kwJy9gX7OJUNVndZBA5zmp72EA58FF5Suwpw2rjkEBKEwkRotAnuGmQG2PuD73gdKkpk6pY1DpnAPK90qOPnRhf4dMEoD5L7EimuUBNrWs4s9U039qAPIH829tmZ634Z6yPPCzbr4nBZ_Xb9LphG_00247XY9_0024tPCwbfCK_Sz3daDdeFmVTyiZwVC9KpLqM4c0x4p5Sa2WyCUEcE5oIg9Lg7gYq4YkevvxoCqc7TXK56URNbWpjJZXz3fJ5fCqeJcL6g)listController_0.EeNYggqZWtiar0rSt8jVD1eSfb4J4GSFkxD2kxLGahLU(), (QI4dAcq_0024lvENeJ7F01V8DuUyACR_6ygoX3OmWG6t7bRsI1oTQdanHscQNaT6Ys5jc1j8cHirIS8ImlW7t8bxlx7xKSJOIZM9uce9OxmKwEeX)listController_1.EeNYggqZWtiar0rSt8jVD1eSfb4J4GSFkxD2kxLGahLU(), _00247xYLrKQKiHUVJb7_YKTEhk.pZEKY5TzLd4S3z2lXESoRnw);
			_0024YQ79Bjv_SyJSjOLx19tt7oyA4rPCEedq1vzGQYyRjwM = new RenderTexture(512, 512, 24);
			UaU8O_0024S38WSWP9sBbjmmhtQ = new GameObject(global::_003CModule_003E.smethod_28<string>(1635123488u)).AddComponent<Camera>();
			UaU8O_0024S38WSWP9sBbjmmhtQ.transform.position = new Vector3(0f, 60f, -100f);
			UaU8O_0024S38WSWP9sBbjmmhtQ.transform.rotation = new Quaternion(0.1f, 0f, 0f, 1f);
			UaU8O_0024S38WSWP9sBbjmmhtQ.targetTexture = _0024YQ79Bjv_SyJSjOLx19tt7oyA4rPCEedq1vzGQYyRjwM;
			UaU8O_0024S38WSWP9sBbjmmhtQ.clearFlags = CameraClearFlags.Color;
			kk_0024srNdLfgKNQLYd7jaYjyg = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.qSsYOsEQb7x452a9Y45dVEk(new Vector3(0f, 0f), new Vector2(400f, 400f));
			kk_0024srNdLfgKNQLYd7jaYjyg.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.9f);
			GameObject gameObject4 = new GameObject(global::_003CModule_003E.smethod_25<string>(300049076u));
			gameObject4.transform.parent = kk_0024srNdLfgKNQLYd7jaYjyg.transform;
			gameObject4.AddComponent<RectTransform>();
			gameObject4.AddComponent<RawImage>().texture = _0024YQ79Bjv_SyJSjOLx19tt7oyA4rPCEedq1vzGQYyRjwM;
			gameObject4.transform.localPosition = Vector3.zero;
			gameObject4.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 300f);
			gameObject4.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 300f);
			Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.P3tLroX6fcPfQha_0024JdbpYXg(global::_003CModule_003E.smethod_29<string>(2248749590u), new Vector3(170f, 170f), global::_003CModule_003E.smethod_25<string>(2624700421u), delegate
			{
				LHwQmhRSazqcfLZmKoRoR_xZ6BkldlRECYZpOx2FFxPfzc79pNqUfFHGOEfv8AXYiLydB2ouSrpJIWzYKwlwWjw60LLWZrYC_0024A5dpnJV9MQFTyrq79ROvXEBb_0024MUpBfb0h_0024VZFjWza9oEFZmihVJF_00246c09RP_C_TdYCkeXSm_0024pOyReP_HrA_GEMNJeTZmYGJSQVQkwxUQqykgF_0024LKwXTzGu1xJQzBi50SD5T6VL1d1o1cm_0024ERBz97WUEunMpuqzoCAzX0IW0O8AqfLHixPO0Jrjy6FaUSnecLNNDdmdqhdm2V8QTglSwmSmZGuOW_0024OsBSxP_0024SgWfHd2zgA3je9ikDd0.smethod_0(kk_0024srNdLfgKNQLYd7jaYjyg, bool_0: false);
			}, kk_0024srNdLfgKNQLYd7jaYjyg.transform).UzVS61irgJn5Pnqwx0lThng(new Vector2(30f, 30f));
			Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.P3tLroX6fcPfQha_0024JdbpYXg(global::_003CModule_003E.smethod_27<string>(2468412527u), new Vector3(-140f, 170f), global::_003CModule_003E.smethod_26<string>(661506210u), delegate
			{
				Y_KnsAySLoCDb4SHb_0024ozdg9Jhandbv5CjRS2vvjE93HwxrEnN8gRKN_3gH8ZJ8dFtwLfFaqEhK1SoCTTPpPBgcQ.smethod_0();
			}, kk_0024srNdLfgKNQLYd7jaYjyg.transform).UzVS61irgJn5Pnqwx0lThng(new Vector2(70f, 30f));
			kk_0024srNdLfgKNQLYd7jaYjyg.SetActive(value: false);
			return false;
		}

		internal static void zzabkMO3HsNHze3BG8AoLTE(string name)
		{
			cGcAEvkY80q7_lj2YRmI1uk((QtKrB_00245y1Dl8GOwmq4Cab2Ho5BtrUNpXBsRzTZc0BRPLuH9xbAqmxxuctYjiK_00242PJpDrdVkZg7aEYkNtowdef01kwJy9gX7OJUNVndZBA5zmp72EA58FF5Suwpw2rjkEBKEwkRotAnuGmQG2PuD73gdKkpk6pY1DpnAPK90qOPnRhf4dMEoD5L7EimuUBNrWs4s9U039qAPIH829tmZ634Z6yPPCzbr4nBZ_Xb9LphG_00247XY9_0024tPCwbfCK_Sz3daDdeFmVTyiZwVC9KpLqM4c0x4p5Sa2WyCUEcE5oIg9Lg7gYq4YkevvxoCqc7TXK56URNbWpjJZXz3fJ5fCqeJcL6g)listController_0.EeNYggqZWtiar0rSt8jVD1eSfb4J4GSFkxD2kxLGahLU(), (QI4dAcq_0024lvENeJ7F01V8DuUyACR_6ygoX3OmWG6t7bRsI1oTQdanHscQNaT6Ys5jc1j8cHirIS8ImlW7t8bxlx7xKSJOIZM9uce9OxmKwEeX)listController_1.EeNYggqZWtiar0rSt8jVD1eSfb4J4GSFkxD2kxLGahLU(), _00247xYLrKQKiHUVJb7_YKTEhk.pZEKY5TzLd4S3z2lXESoRnw);
		}

		internal static void B7E3yGIUYRHvMdXen005f5U()
		{
			EawkyrVySrwUz8iXwAy3a1o.FLSdXom6uNTfN55f5nxTsH8 = lZKkq6Pc8pSN7ZBtxX7pR4s > 0;
			smethod_15(CELyhJyLzVHZGI0KsTiq3fw, smethod_14(xh28Je5vGZzmlyaYTv4leqahX_0024d76HU68fsrFhxkr_aiwJvGWxEZr2ULd9ujX_W1gg.UzhVmO15k8UHVkcjd2D2eFw(xh28Je5vGZzmlyaYTv4leqahX_0024d76HU68fsrFhxkr_aiwJvGWxEZr2ULd9ujX_W1gg.eEsL_CqOa2k0Oy16TqJNFVJPuzSIPe8Trw5760Yzq7e5ghLIwg7ZyOeyUc9A2E9z_0024mUEQCN09awhbakTSEXMPkahOFo0RcQ91HbGZkSM_Bhl.workshop_page), (object)(lZKkq6Pc8pSN7ZBtxX7pR4s + 1), (object)Mathf.CeilToInt((float)pPX_nHY_MIGP3ftDkHfjs8E.Count * 1f / (float)H_VsQ_AEW_0024pgd7LYesvnpyg.Count * 1f)));
			int num = lZKkq6Pc8pSN7ZBtxX7pR4s * 10;
			bool flag = false;
			foreach (lNpIzpGWMiPQ_5gAwjtlI6S6Js86Pudxf8nfbTsW_0024TcvJlB0LYzijk9i4IYc3PbHGAzwIeWv5bbP_0024YvJI16Sino item in H_VsQ_AEW_0024pgd7LYesvnpyg)
			{
				item.ZzKQ_nyipnINy9MDt2NFQPk();
				if (num < pPX_nHY_MIGP3ftDkHfjs8E.Count)
				{
					smethod_7(smethod_12((Component)item), bool_0: true);
					item.PhOJBv5ufEFl4v8YBeynXCw(pPX_nHY_MIGP3ftDkHfjs8E[num]);
				}
				else
				{
					smethod_7(smethod_12((Component)item), bool_0: false);
					flag = true;
				}
				num++;
			}
			PWShNz5mPUMTrvEbX1xUUO0.FLSdXom6uNTfN55f5nxTsH8 = !flag && lZKkq6Pc8pSN7ZBtxX7pR4s != 4;
		}

		internal static void cGcAEvkY80q7_lj2YRmI1uk(QtKrB_00245y1Dl8GOwmq4Cab2Ho5BtrUNpXBsRzTZc0BRPLuH9xbAqmxxuctYjiK_00242PJpDrdVkZg7aEYkNtowdef01kwJy9gX7OJUNVndZBA5zmp72EA58FF5Suwpw2rjkEBKEwkRotAnuGmQG2PuD73gdKkpk6pY1DpnAPK90qOPnRhf4dMEoD5L7EimuUBNrWs4s9U039qAPIH829tmZ634Z6yPPCzbr4nBZ_Xb9LphG_00247XY9_0024tPCwbfCK_Sz3daDdeFmVTyiZwVC9KpLqM4c0x4p5Sa2WyCUEcE5oIg9Lg7gYq4YkevvxoCqc7TXK56URNbWpjJZXz3fJ5fCqeJcL6g sort, QI4dAcq_0024lvENeJ7F01V8DuUyACR_6ygoX3OmWG6t7bRsI1oTQdanHscQNaT6Ys5jc1j8cHirIS8ImlW7t8bxlx7xKSJOIZM9uce9OxmKwEeX time, string search)
		{
			pPX_nHY_MIGP3ftDkHfjs8E.Clear();
			lZKkq6Pc8pSN7ZBtxX7pR4s = 0;
			EawkyrVySrwUz8iXwAy3a1o.FLSdXom6uNTfN55f5nxTsH8 = false;
			PWShNz5mPUMTrvEbX1xUUO0.FLSdXom6uNTfN55f5nxTsH8 = false;
			EUGCQuery eugcquery_ = EUGCQuery.k_EUGCQuery_RankedByTrend;
			switch (sort)
			{
			case QtKrB_00245y1Dl8GOwmq4Cab2Ho5BtrUNpXBsRzTZc0BRPLuH9xbAqmxxuctYjiK_00242PJpDrdVkZg7aEYkNtowdef01kwJy9gX7OJUNVndZBA5zmp72EA58FF5Suwpw2rjkEBKEwkRotAnuGmQG2PuD73gdKkpk6pY1DpnAPK90qOPnRhf4dMEoD5L7EimuUBNrWs4s9U039qAPIH829tmZ634Z6yPPCzbr4nBZ_Xb9LphG_00247XY9_0024tPCwbfCK_Sz3daDdeFmVTyiZwVC9KpLqM4c0x4p5Sa2WyCUEcE5oIg9Lg7gYq4YkevvxoCqc7TXK56URNbWpjJZXz3fJ5fCqeJcL6g.MostPopular:
				eugcquery_ = EUGCQuery.k_EUGCQuery_RankedByTrend;
				break;
			case QtKrB_00245y1Dl8GOwmq4Cab2Ho5BtrUNpXBsRzTZc0BRPLuH9xbAqmxxuctYjiK_00242PJpDrdVkZg7aEYkNtowdef01kwJy9gX7OJUNVndZBA5zmp72EA58FF5Suwpw2rjkEBKEwkRotAnuGmQG2PuD73gdKkpk6pY1DpnAPK90qOPnRhf4dMEoD5L7EimuUBNrWs4s9U039qAPIH829tmZ634Z6yPPCzbr4nBZ_Xb9LphG_00247XY9_0024tPCwbfCK_Sz3daDdeFmVTyiZwVC9KpLqM4c0x4p5Sa2WyCUEcE5oIg9Lg7gYq4YkevvxoCqc7TXK56URNbWpjJZXz3fJ5fCqeJcL6g.MostRecent:
				eugcquery_ = EUGCQuery.k_EUGCQuery_RankedByPublicationDate;
				break;
			case QtKrB_00245y1Dl8GOwmq4Cab2Ho5BtrUNpXBsRzTZc0BRPLuH9xbAqmxxuctYjiK_00242PJpDrdVkZg7aEYkNtowdef01kwJy9gX7OJUNVndZBA5zmp72EA58FF5Suwpw2rjkEBKEwkRotAnuGmQG2PuD73gdKkpk6pY1DpnAPK90qOPnRhf4dMEoD5L7EimuUBNrWs4s9U039qAPIH829tmZ634Z6yPPCzbr4nBZ_Xb9LphG_00247XY9_0024tPCwbfCK_Sz3daDdeFmVTyiZwVC9KpLqM4c0x4p5Sa2WyCUEcE5oIg9Lg7gYq4YkevvxoCqc7TXK56URNbWpjJZXz3fJ5fCqeJcL6g.MostSubscribed:
				eugcquery_ = EUGCQuery.k_EUGCQuery_RankedByTotalUniqueSubscriptions;
				break;
			}
			UGCQueryHandle_t ugcqueryHandle_t_ = smethod_16(eugcquery_, EUGCMatchingUGCType.k_EUGCMatchingUGCType_Items_ReadyToUse, Steam.OEDCBNHNGMJ.OMFPIMHBOKE, Steam.OEDCBNHNGMJ.OMFPIMHBOKE, 1u);
			if (sort == QtKrB_00245y1Dl8GOwmq4Cab2Ho5BtrUNpXBsRzTZc0BRPLuH9xbAqmxxuctYjiK_00242PJpDrdVkZg7aEYkNtowdef01kwJy9gX7OJUNVndZBA5zmp72EA58FF5Suwpw2rjkEBKEwkRotAnuGmQG2PuD73gdKkpk6pY1DpnAPK90qOPnRhf4dMEoD5L7EimuUBNrWs4s9U039qAPIH829tmZ634Z6yPPCzbr4nBZ_Xb9LphG_00247XY9_0024tPCwbfCK_Sz3daDdeFmVTyiZwVC9KpLqM4c0x4p5Sa2WyCUEcE5oIg9Lg7gYq4YkevvxoCqc7TXK56URNbWpjJZXz3fJ5fCqeJcL6g.MostPopular && time != QI4dAcq_0024lvENeJ7F01V8DuUyACR_6ygoX3OmWG6t7bRsI1oTQdanHscQNaT6Ys5jc1j8cHirIS8ImlW7t8bxlx7xKSJOIZM9uce9OxmKwEeX.AllTime)
			{
				uint uint_ = 7u;
				switch (time)
				{
				case QI4dAcq_0024lvENeJ7F01V8DuUyACR_6ygoX3OmWG6t7bRsI1oTQdanHscQNaT6Ys5jc1j8cHirIS8ImlW7t8bxlx7xKSJOIZM9uce9OxmKwEeX.PastWeek:
					uint_ = 7u;
					break;
				case QI4dAcq_0024lvENeJ7F01V8DuUyACR_6ygoX3OmWG6t7bRsI1oTQdanHscQNaT6Ys5jc1j8cHirIS8ImlW7t8bxlx7xKSJOIZM9uce9OxmKwEeX.ThreeMonths:
					uint_ = 92u;
					break;
				case QI4dAcq_0024lvENeJ7F01V8DuUyACR_6ygoX3OmWG6t7bRsI1oTQdanHscQNaT6Ys5jc1j8cHirIS8ImlW7t8bxlx7xKSJOIZM9uce9OxmKwEeX.SixMonths:
					uint_ = 183u;
					break;
				case QI4dAcq_0024lvENeJ7F01V8DuUyACR_6ygoX3OmWG6t7bRsI1oTQdanHscQNaT6Ys5jc1j8cHirIS8ImlW7t8bxlx7xKSJOIZM9uce9OxmKwEeX.OneYear:
					uint_ = 365u;
					break;
				}
				smethod_17(ugcqueryHandle_t_, uint_);
			}
			if (!smethod_18(search))
			{
				smethod_19(ugcqueryHandle_t_, search);
			}
			CallResult<SteamUGCQueryCompleted_t>.Create(delegate(SteamUGCQueryCompleted_t a, bool b)
			{
				mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_25<string>(3345991939u) + a.m_unNumResultsReturned + global::_003CModule_003E.smethod_25<string>(4105542909u) + a.m_unTotalMatchingResults + global::_003CModule_003E.smethod_27<string>(1286567199u));
				for (int i = 0; i < a.m_unNumResultsReturned; i++)
				{
					SteamUGC.GetQueryUGCResult(a.m_handle, (uint)i, out var pDetails);
					SteamUGC.GetQueryUGCPreviewURL(a.m_handle, (uint)i, out var pchURL, 10000u);
					EItemState itemState = (EItemState)SteamUGC.GetItemState(pDetails.m_nPublishedFileId);
					U1GmT9ZMi6XvQgaRUcEHGTJ2ox6n_00242ODVjhi9OQlSQvI8A2HT5RLkW_0024sQ5gp2RyZESs49PI9r7mOSRMi5MsAhOI item = new U1GmT9ZMi6XvQgaRUcEHGTJ2ox6n_00242ODVjhi9OQlSQvI8A2HT5RLkW_0024sQ5gp2RyZESs49PI9r7mOSRMi5MsAhOI
					{
						hJJJX3iFC8vYXCIx0eUwbqk = pDetails.m_rgchTitle,
						string_0 = pDetails.m_rgchURL,
						nxch5NN7yn_2gEoq38N_tzo = pDetails.m_nPublishedFileId,
						string_1 = pDetails.m_rgchDescription,
						K52jeLH_0024D_rsZoa7xVSPVPk = pDetails.m_ulSteamIDOwner,
						yxqgtybS7ik_0024wDkg_BT5Bpw = pchURL,
						WZh4cN51oA5qlxlkwH76ZoE = true,
						YhiMemOciw_0024oL1zy4ye1Ets = ((itemState & EItemState.k_EItemStateSubscribed) == EItemState.k_EItemStateSubscribed),
						ztm50T3oggPrw0vZ5QDsMCg = ((itemState & EItemState.k_EItemStateInstalled) == EItemState.k_EItemStateInstalled),
						Tu1GkDmtr6gDT5y1PaHx4fI = ((itemState & EItemState.k_EItemStateNeedsUpdate) == EItemState.k_EItemStateNeedsUpdate),
						E3t32rKV1_0024UUxFOcPYAfjvg = ((itemState & EItemState.k_EItemStateDownloadPending) == EItemState.k_EItemStateDownloadPending)
					};
					pPX_nHY_MIGP3ftDkHfjs8E.Add(item);
				}
				B7E3yGIUYRHvMdXen005f5U();
			}).Set(smethod_20(ugcqueryHandle_t_));
		}

		internal static void DRSz5gFgf0HPW70WZ_0024EW7RUbiN8Y4D5_iDUS_0024NENgxTb(SteamUGCQueryCompleted_t a, bool b)
		{
			mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_25<string>(3345991939u) + a.m_unNumResultsReturned + global::_003CModule_003E.smethod_25<string>(4105542909u) + a.m_unTotalMatchingResults + global::_003CModule_003E.smethod_27<string>(1286567199u));
			for (int i = 0; i < a.m_unNumResultsReturned; i++)
			{
				SteamUGC.GetQueryUGCResult(a.m_handle, (uint)i, out var pDetails);
				SteamUGC.GetQueryUGCPreviewURL(a.m_handle, (uint)i, out var pchURL, 10000u);
				EItemState itemState = (EItemState)SteamUGC.GetItemState(pDetails.m_nPublishedFileId);
				U1GmT9ZMi6XvQgaRUcEHGTJ2ox6n_00242ODVjhi9OQlSQvI8A2HT5RLkW_0024sQ5gp2RyZESs49PI9r7mOSRMi5MsAhOI item = new U1GmT9ZMi6XvQgaRUcEHGTJ2ox6n_00242ODVjhi9OQlSQvI8A2HT5RLkW_0024sQ5gp2RyZESs49PI9r7mOSRMi5MsAhOI
				{
					hJJJX3iFC8vYXCIx0eUwbqk = pDetails.m_rgchTitle,
					string_0 = pDetails.m_rgchURL,
					nxch5NN7yn_2gEoq38N_tzo = pDetails.m_nPublishedFileId,
					string_1 = pDetails.m_rgchDescription,
					K52jeLH_0024D_rsZoa7xVSPVPk = pDetails.m_ulSteamIDOwner,
					yxqgtybS7ik_0024wDkg_BT5Bpw = pchURL,
					WZh4cN51oA5qlxlkwH76ZoE = true,
					YhiMemOciw_0024oL1zy4ye1Ets = ((itemState & EItemState.k_EItemStateSubscribed) == EItemState.k_EItemStateSubscribed),
					ztm50T3oggPrw0vZ5QDsMCg = ((itemState & EItemState.k_EItemStateInstalled) == EItemState.k_EItemStateInstalled),
					Tu1GkDmtr6gDT5y1PaHx4fI = ((itemState & EItemState.k_EItemStateNeedsUpdate) == EItemState.k_EItemStateNeedsUpdate),
					E3t32rKV1_0024UUxFOcPYAfjvg = ((itemState & EItemState.k_EItemStateDownloadPending) == EItemState.k_EItemStateDownloadPending)
				};
				pPX_nHY_MIGP3ftDkHfjs8E.Add(item);
			}
			B7E3yGIUYRHvMdXen005f5U();
		}

		internal static void MziaTDkwFA_9ZACCxSD3QPI(Vector3 pos, Transform parent)
		{
			GameObject gameObject = smethod_21();
			smethod_22(smethod_8(gameObject), parent);
			smethod_23(smethod_8(gameObject), pos);
			GameObject gameObject2 = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.qSsYOsEQb7x452a9Y45dVEk(Vector3.zero, new Vector2(190f, 250f), gameObject.transform);
			GameObject gameObject3 = new GameObject(global::_003CModule_003E.smethod_27<string>(2877983918u));
			RectTransform rectTransform = gameObject3.AddComponent<RectTransform>();
			Image image = gameObject3.AddComponent<Image>();
			image.preserveAspect = true;
			image.transform.parent = gameObject2.transform;
			image.transform.localPosition = Vector3.zero;
			rectTransform.sizeDelta = new Vector2(120f, 100f);
			rectTransform.localPosition = new Vector3(0f, 40f);
			Text component = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.YU_pwpP3pKH76IHZTea_SXk(global::_003CModule_003E.smethod_29<string>(3459146291u), Vector2.zero, "", gameObject2.transform, rmOutline: false, -1, FontStyle.Bold, TextAnchor.MiddleCenter).GetComponent<Text>();
			component.rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
			component.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
			component.rectTransform.anchoredPosition = new Vector2(75f, -50f);
			component.resizeTextForBestFit = true;
			component.resizeTextMinSize = 1;
			component.resizeTextMaxSize = 20;
			component.horizontalOverflow = HorizontalWrapMode.Wrap;
			component.verticalOverflow = VerticalWrapMode.Truncate;
			component.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 20f);
			component.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, rectTransform.rect.width);
			Text component2 = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.YU_pwpP3pKH76IHZTea_SXk(global::_003CModule_003E.smethod_29<string>(3459146291u), Vector2.zero, "", gameObject2.transform, rmOutline: false, -1, FontStyle.Italic, TextAnchor.MiddleCenter).GetComponent<Text>();
			component2.rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
			component2.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
			component2.rectTransform.anchoredPosition = new Vector2(75f, -70f);
			component2.resizeTextForBestFit = true;
			component2.resizeTextMinSize = 1;
			component2.resizeTextMaxSize = 15;
			component2.horizontalOverflow = HorizontalWrapMode.Wrap;
			component2.verticalOverflow = VerticalWrapMode.Truncate;
			component2.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 20f);
			component2.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, rectTransform.rect.width);
			Control0 control = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.uEsWMK_pFkCY_0024M5zt8zLsQk(global::_003CModule_003E.smethod_28<string>(2196486636u), new Vector3(65f, -93f), global::_003CModule_003E.smethod_28<string>(1832247032u), gameObject2.transform);
			control.hLxnG9Hq33zU_YUsu_00240_zak = false;
			control.UzVS61irgJn5Pnqwx0lThng(new Vector2(30f, 30f));
			control.gameObject.smethod_0(global::_003CModule_003E.smethod_29<string>(1544240008u)).GetComponent<Image>().color = Color.green;
			lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw2 = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.P3tLroX6fcPfQha_0024JdbpYXg(global::_003CModule_003E.smethod_29<string>(626259385u), new Vector3(-65f, -93f), global::_003CModule_003E.smethod_27<string>(853815013u), null, gameObject2.transform);
			lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw2.UzVS61irgJn5Pnqwx0lThng(new Vector2(30f, 30f));
			lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw3 = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.P3tLroX6fcPfQha_0024JdbpYXg(global::_003CModule_003E.smethod_28<string>(1103767824u), new Vector3(0f, -93f), global::_003CModule_003E.smethod_28<string>(3744504953u), null, gameObject2.transform, 15);
			lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw3.UzVS61irgJn5Pnqwx0lThng(new Vector2(90f, 30f));
			lNpIzpGWMiPQ_5gAwjtlI6S6Js86Pudxf8nfbTsW_0024TcvJlB0LYzijk9i4IYc3PbHGAzwIeWv5bbP_0024YvJI16Sino lNpIzpGWMiPQ_5gAwjtlI6S6Js86Pudxf8nfbTsW_0024TcvJlB0LYzijk9i4IYc3PbHGAzwIeWv5bbP_0024YvJI16Sino = gameObject.AddComponent<lNpIzpGWMiPQ_5gAwjtlI6S6Js86Pudxf8nfbTsW_0024TcvJlB0LYzijk9i4IYc3PbHGAzwIeWv5bbP_0024YvJI16Sino>();
			lNpIzpGWMiPQ_5gAwjtlI6S6Js86Pudxf8nfbTsW_0024TcvJlB0LYzijk9i4IYc3PbHGAzwIeWv5bbP_0024YvJI16Sino.method_0(image, component, component2, control, lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw2, lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw3);
			H_VsQ_AEW_0024pgd7LYesvnpyg.Add(lNpIzpGWMiPQ_5gAwjtlI6S6Js86Pudxf8nfbTsW_0024TcvJlB0LYzijk9i4IYc3PbHGAzwIeWv5bbP_0024YvJI16Sino);
		}

		internal static GameObject smethod_1(string string_0)
		{
			return GameObject.Find(string_0);
		}

		internal static void smethod_2(UnityEngine.Object object_0)
		{
			UnityEngine.Object.Destroy(object_0);
		}

		internal static HIPBCCKFFAG smethod_3()
		{
			return new HIPBCCKFFAG();
		}

		internal static float smethod_4()
		{
			return Time.realtimeSinceStartup;
		}

		internal static void smethod_5(bool bool_0)
		{
			JKGKJLLFMLE.LKOKBMIBILN(bool_0);
		}

		internal static void smethod_6(bool bool_0)
		{
			KEFHJCGICLE.AJMIDAFCECE(bool_0);
		}

		internal static void smethod_7(GameObject gameObject_0, bool bool_0)
		{
			gameObject_0.SetActive(bool_0);
		}

		internal static Transform smethod_8(GameObject gameObject_0)
		{
			return gameObject_0.transform;
		}

		internal static Transform smethod_9(Transform transform_0, int int_0)
		{
			return transform_0.GetChild(int_0);
		}

		internal static string smethod_10(UnityEngine.Object object_0)
		{
			return object_0.name;
		}

		internal static bool smethod_11(string string_0, string string_1)
		{
			return string_0 != string_1;
		}

		internal static GameObject smethod_12(Component component_0)
		{
			return component_0.gameObject;
		}

		internal static int smethod_13(Transform transform_0)
		{
			return transform_0.childCount;
		}

		internal static string smethod_14(string string_0, object object_0, object object_1)
		{
			return string.Format(string_0, object_0, object_1);
		}

		internal static void smethod_15(Text text_0, string string_0)
		{
			text_0.text = string_0;
		}

		internal static UGCQueryHandle_t smethod_16(EUGCQuery eugcquery_0, EUGCMatchingUGCType eugcmatchingUGCType_0, AppId_t appId_t_0, AppId_t appId_t_1, uint uint_0)
		{
			return SteamUGC.CreateQueryAllUGCRequest(eugcquery_0, eugcmatchingUGCType_0, appId_t_0, appId_t_1, uint_0);
		}

		internal static bool smethod_17(UGCQueryHandle_t ugcqueryHandle_t_0, uint uint_0)
		{
			return SteamUGC.SetRankedByTrendDays(ugcqueryHandle_t_0, uint_0);
		}

		internal static bool smethod_18(string string_0)
		{
			return string.IsNullOrEmpty(string_0);
		}

		internal static bool smethod_19(UGCQueryHandle_t ugcqueryHandle_t_0, string string_0)
		{
			return SteamUGC.SetSearchText(ugcqueryHandle_t_0, string_0);
		}

		internal static SteamAPICall_t smethod_20(UGCQueryHandle_t ugcqueryHandle_t_0)
		{
			return SteamUGC.SendQueryUGCRequest(ugcqueryHandle_t_0);
		}

		internal static GameObject smethod_21()
		{
			return new GameObject();
		}

		internal static void smethod_22(Transform transform_0, Transform transform_1)
		{
			transform_0.SetParent(transform_1);
		}

		internal static void smethod_23(Transform transform_0, Vector3 vector3_0)
		{
			transform_0.localPosition = vector3_0;
		}
	}

	[HarmonyPatch(typeof(Workshop))]
	[HarmonyPatch("Update")]
	internal class qC9jPXa9x5w7_4FIGyr6StdFsakjWRwnySHoa8RO9gH8mjXUEqbnJu1k2WohVbC5d91lAw9KvgAn_lMpenOZne6U97LRMpXA6wf0wZFduAqT2uAcHAskpVU_0024HsV5EKFVSSjE4vSCy2SF2ZCNt7MTsms
	{
		private static Action<SceneMan> action_0;

		[HarmonyPrefix]
		internal static bool smethod_0()
		{
			if (OB3U2zm9e2wliliNbaRFPAY)
			{
				return true;
			}
			invokeBaseUpdate(SceneMan.JFAOKFIDAGK);
			if (smethod_1((UnityEngine.Object)E6YS52rBYK_lITFDcP_00244LpQ, (UnityEngine.Object)null))
			{
				smethod_3(smethod_2(E6YS52rBYK_lITFDcP_00244LpQ), Vector3.up);
			}
			if (smethod_4(KeyCode.Escape) && smethod_1((UnityEngine.Object)Class29.kk_0024srNdLfgKNQLYd7jaYjyg, (UnityEngine.Object)null) && smethod_5(Class29.kk_0024srNdLfgKNQLYd7jaYjyg))
			{
				smethod_6(Class29.kk_0024srNdLfgKNQLYd7jaYjyg, bool_0: false);
			}
			return false;
		}

		private static void invokeBaseUpdate(SceneMan sceneMan)
		{
			if (action_0 == null)
			{
				MethodInfo method = typeof(SceneMan).GetMethod("Update", AccessTools.all);
				DynamicMethod dynamicMethod = new DynamicMethod("InvokedUpd", null, new Type[1] { typeof(SceneMan) }, typeof(SceneMan));
				ILGenerator iLGenerator = dynamicMethod.GetILGenerator();
				iLGenerator.Emit(OpCodes.Ldarg_0);
				iLGenerator.Emit(OpCodes.Call, method);
				iLGenerator.Emit(OpCodes.Ret);
				action_0 = (Action<SceneMan>)dynamicMethod.CreateDelegate(typeof(Action<SceneMan>));
			}
			action_0(sceneMan);
		}

		internal static bool smethod_1(UnityEngine.Object object_0, UnityEngine.Object object_1)
		{
			return object_0 != object_1;
		}

		internal static Transform smethod_2(GameObject gameObject_0)
		{
			return gameObject_0.transform;
		}

		internal static void smethod_3(Transform transform_0, Vector3 vector3_0)
		{
			transform_0.Rotate(vector3_0);
		}

		internal static bool smethod_4(KeyCode keyCode_0)
		{
			return Input.GetKeyDown(keyCode_0);
		}

		internal static bool smethod_5(GameObject gameObject_0)
		{
			return gameObject_0.activeInHierarchy;
		}

		internal static void smethod_6(GameObject gameObject_0, bool bool_0)
		{
			gameObject_0.SetActive(bool_0);
		}
	}

	private class lNpIzpGWMiPQ_5gAwjtlI6S6Js86Pudxf8nfbTsW_0024TcvJlB0LYzijk9i4IYc3PbHGAzwIeWv5bbP_0024YvJI16Sino : MonoBehaviour
	{
		[CompilerGenerated]
		private sealed class iRKkttvNnP57a4eDUGpehAYibg6o91gaoyif01fMvDmUBIHCPHk3H5WMI0vHgf_wjWeven3x3hgu3N2mIS5n8wFxU4QGwK0eNyxCXNcd17Skb30PfbZ7DFagCih5RRmQvJriMFpdW0AHOmD3yfF1Zbe2N5pR90SI3L64LnVsngnGyHTm1boWPVCZNgCt8yDCfj4an_vx2W_0024Y7Omn9cQb9ugpef5EbYPM2TsEqUhBTz1EFDYRPei_ybcp_ktOAd9kpp1baTU5E5n2lW5K48dwf_o
		{
			public U1GmT9ZMi6XvQgaRUcEHGTJ2ox6n_00242ODVjhi9OQlSQvI8A2HT5RLkW_0024sQ5gp2RyZESs49PI9r7mOSRMi5MsAhOI u1GmT9ZMi6XvQgaRUcEHGTJ2ox6n_00242ODVjhi9OQlSQvI8A2HT5RLkW_0024sQ5gp2RyZESs49PI9r7mOSRMi5MsAhOI_0;

			internal void hhoCrFNGcBQcw7zrbVxw8u9kRv49s4exoEJThPYbISrM(lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw _)
			{
				string string_ = global::_003CModule_003E.smethod_29<string>(3783644332u);
				PublishedFileId_t nxch5NN7yn_2gEoq38N_tzo = u1GmT9ZMi6XvQgaRUcEHGTJ2ox6n_00242ODVjhi9OQlSQvI8A2HT5RLkW_0024sQ5gp2RyZESs49PI9r7mOSRMi5MsAhOI_0.nxch5NN7yn_2gEoq38N_tzo;
				smethod_1(smethod_0(string_, nxch5NN7yn_2gEoq38N_tzo.ToString()));
			}

			internal void h4v__veP6N3yl2hJSRQ4Roi4WZZ851NUB2TOWrD_0024L0Zt(lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw button)
			{
				if (smethod_2(Class29.kk_0024srNdLfgKNQLYd7jaYjyg))
				{
					MPatchr.ShowDebugMsg(smethod_0(global::_003CModule_003E.smethod_25<string>(2282985757u), xh28Je5vGZzmlyaYTv4leqahX_0024d76HU68fsrFhxkr_aiwJvGWxEZr2ULd9ujX_W1gg.UzhVmO15k8UHVkcjd2D2eFw(xh28Je5vGZzmlyaYTv4leqahX_0024d76HU68fsrFhxkr_aiwJvGWxEZr2ULd9ujX_W1gg.eEsL_CqOa2k0Oy16TqJNFVJPuzSIPe8Trw5760Yzq7e5ghLIwg7ZyOeyUc9A2E9z_0024mUEQCN09awhbakTSEXMPkahOFo0RcQ91HbGZkSM_Bhl.workshop_closeExistingPreview)));
					return;
				}
				MPatchr.ShowDebugMsg(smethod_0(global::_003CModule_003E.smethod_29<string>(1675020134u), xh28Je5vGZzmlyaYTv4leqahX_0024d76HU68fsrFhxkr_aiwJvGWxEZr2ULd9ujX_W1gg.UzhVmO15k8UHVkcjd2D2eFw(xh28Je5vGZzmlyaYTv4leqahX_0024d76HU68fsrFhxkr_aiwJvGWxEZr2ULd9ujX_W1gg.eEsL_CqOa2k0Oy16TqJNFVJPuzSIPe8Trw5760Yzq7e5ghLIwg7ZyOeyUc9A2E9z_0024mUEQCN09awhbakTSEXMPkahOFo0RcQ91HbGZkSM_Bhl.workshop_previewLoading)));
				string text = global::_003CModule_003E.smethod_27<string>(4083179126u);
				PublishedFileId_t nxch5NN7yn_2gEoq38N_tzo = u1GmT9ZMi6XvQgaRUcEHGTJ2ox6n_00242ODVjhi9OQlSQvI8A2HT5RLkW_0024sQ5gp2RyZESs49PI9r7mOSRMi5MsAhOI_0.nxch5NN7yn_2gEoq38N_tzo;
				mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(text + nxch5NN7yn_2gEoq38N_tzo.ToString() + global::_003CModule_003E.smethod_26<string>(3009992766u) + smethod_3(u1GmT9ZMi6XvQgaRUcEHGTJ2ox6n_00242ODVjhi9OQlSQvI8A2HT5RLkW_0024sQ5gp2RyZESs49PI9r7mOSRMi5MsAhOI_0.nxch5NN7yn_2gEoq38N_tzo, bool_0: true));
				Q5GS7XFtq4kccIJybzbeAWY = u1GmT9ZMi6XvQgaRUcEHGTJ2ox6n_00242ODVjhi9OQlSQvI8A2HT5RLkW_0024sQ5gp2RyZESs49PI9r7mOSRMi5MsAhOI_0.nxch5NN7yn_2gEoq38N_tzo;
				if (BNY2Hoxd9qRiNISDjVrLHPM != null)
				{
					return;
				}
				BNY2Hoxd9qRiNISDjVrLHPM = Callback<DownloadItemResult_t>.Create(delegate(DownloadItemResult_t item)
				{
					if (!(item.m_unAppID != Steam.OEDCBNHNGMJ.OMFPIMHBOKE) && !(Y_KnsAySLoCDb4SHb_0024ozdg9Jhandbv5CjRS2vvjE93HwxrEnN8gRKN_3gH8ZJ8dFtwLfFaqEhK1SoCTTPpPBgcQ.smethod_1().name != global::_003CModule_003E.smethod_28<string>(102257120u)) && !(Q5GS7XFtq4kccIJybzbeAWY != item.m_nPublishedFileId))
					{
						Q5GS7XFtq4kccIJybzbeAWY = default(PublishedFileId_t);
						bool flag = (SteamUGC.GetItemState(item.m_nPublishedFileId) & 1) == 1;
						SteamUGC.GetItemInstallInfo(item.m_nPublishedFileId, out var punSizeOnDisk, out var pchFolder, 1024u, out var punTimeStamp);
						mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(string.Format(global::_003CModule_003E.smethod_25<string>(930772342u), item.m_unAppID, item.m_nPublishedFileId, item.m_eResult, punSizeOnDisk, punTimeStamp));
						string[] files = Directory.GetFiles(pchFolder);
						foreach (string text2 in files)
						{
							if (text2.EndsWith(global::_003CModule_003E.smethod_26<string>(976003231u)) || text2.EndsWith(global::_003CModule_003E.smethod_28<string>(223620919u)))
							{
								y5kmSOqVaOjhMvxRjeJAZmo = LNGKNOGOIKL.FMAGAEMFION<BuildData>(File.ReadAllText(text2));
								mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_27<string>(3068708960u) + text2, bool_0: true);
								break;
							}
						}
						files = Directory.GetFiles(pchFolder);
						foreach (string text3 in files)
						{
							if (text3.EndsWith(global::_003CModule_003E.smethod_28<string>(2682238246u)))
							{
								nu9wqOavhI9USqcxKp2D2JdN6BvnIdeDULV1uF0wgOAy = LNGKNOGOIKL.FMAGAEMFION<AssignData>(File.ReadAllText(text3));
								mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_26<string>(2621503955u) + text3, bool_0: true);
								break;
							}
						}
						if (!flag)
						{
							Directory.Delete(pchFolder, recursive: true);
						}
						if (y5kmSOqVaOjhMvxRjeJAZmo != null)
						{
							BuildData buildData = null;
							if (JKGKJLLFMLE.HHGILAIOCLG != null)
							{
								buildData = JKGKJLLFMLE.HHGILAIOCLG.Clone();
								buildData.isReady = JKGKJLLFMLE.HHGILAIOCLG.isReady;
							}
							JKGKJLLFMLE.HHGILAIOCLG = y5kmSOqVaOjhMvxRjeJAZmo;
							xH535ybWa6_0024ItQpcTo8vW9E.ACMGPBMMKNI(LBOKOPEGKGE: true, LGBGNLDPMNN: true);
							JKGKJLLFMLE.HHGILAIOCLG = buildData;
							Bounds mFGJHOHNCDB = xH535ybWa6_0024ItQpcTo8vW9E.MFGJHOHNCDB;
							if (E6YS52rBYK_lITFDcP_00244LpQ != null)
							{
								UnityEngine.Object.Destroy(E6YS52rBYK_lITFDcP_00244LpQ);
								E6YS52rBYK_lITFDcP_00244LpQ = null;
							}
							E6YS52rBYK_lITFDcP_00244LpQ = new GameObject(global::_003CModule_003E.smethod_26<string>(2502783770u));
							float num = mFGJHOHNCDB.size.y * 0.5f;
							Transform transform = xH535ybWa6_0024ItQpcTo8vW9E.CLNMBHMCPGB[0].transform;
							transform.localPosition = Vector3.up * num - mFGJHOHNCDB.center;
							E6YS52rBYK_lITFDcP_00244LpQ.transform.localScale = Vector3.one;
							transform.parent = E6YS52rBYK_lITFDcP_00244LpQ.transform;
							float f = Mathf.Max(Mathf.Max(mFGJHOHNCDB.size.x, mFGJHOHNCDB.size.z), num);
							E6YS52rBYK_lITFDcP_00244LpQ.transform.localScale = Vector3.one * (10f / Mathf.Sqrt(f));
							BlockController[] componentsInChildren = E6YS52rBYK_lITFDcP_00244LpQ.transform.GetComponentsInChildren<BlockController>();
							for (int num2 = componentsInChildren.Length - 1; num2 >= 0; num2--)
							{
								UnityEngine.Object.Destroy(componentsInChildren[num2]);
							}
							Class29.kk_0024srNdLfgKNQLYd7jaYjyg.SetActive(value: true);
						}
					}
				});
			}

			internal void iHYUGU0kcpZ6VKgd1CheUzP0frHeKFZMBxPRXhyDutOa(bool enabled)
			{
				if (!enabled)
				{
					smethod_4(u1GmT9ZMi6XvQgaRUcEHGTJ2ox6n_00242ODVjhi9OQlSQvI8A2HT5RLkW_0024sQ5gp2RyZESs49PI9r7mOSRMi5MsAhOI_0.nxch5NN7yn_2gEoq38N_tzo);
				}
				else
				{
					smethod_5(u1GmT9ZMi6XvQgaRUcEHGTJ2ox6n_00242ODVjhi9OQlSQvI8A2HT5RLkW_0024sQ5gp2RyZESs49PI9r7mOSRMi5MsAhOI_0.nxch5NN7yn_2gEoq38N_tzo);
				}
			}

			internal static string smethod_0(string string_0, string string_1)
			{
				return string_0 + string_1;
			}

			internal static void smethod_1(string string_0)
			{
				Application.OpenURL(string_0);
			}

			internal static bool smethod_2(GameObject gameObject_0)
			{
				return gameObject_0.activeInHierarchy;
			}

			internal static bool smethod_3(PublishedFileId_t publishedFileId_t_0, bool bool_0)
			{
				return SteamUGC.DownloadItem(publishedFileId_t_0, bool_0);
			}

			internal static SteamAPICall_t smethod_4(PublishedFileId_t publishedFileId_t_0)
			{
				return SteamUGC.UnsubscribeItem(publishedFileId_t_0);
			}

			internal static SteamAPICall_t smethod_5(PublishedFileId_t publishedFileId_t_0)
			{
				return SteamUGC.SubscribeItem(publishedFileId_t_0);
			}
		}

		[CompilerGenerated]
		private sealed class O3fQXeTow8fxJd2T2fs3h45kbDRCT2SKd1htDC73B_cyXGcIKtB7p8a_7KEm0587y0VwmQu2jBHnUzPwN81WISjtEVeCrgsAsosPlJGbDktzsu3xAuqq7QOTGNq8L9plJrazekJ3Wr9EpM2nQHQU0KYCMRQDMUHokT1UbXDvR4wU8a06_0024U4N1QhxBzZmpB_iAxi4MSSaS1u9mB89WldvasoGJnWjBXCYvOEAXkiQGQs100xw0ljkNoQjW4DdGJ_002444A : IEnumerator<object>, IDisposable, IEnumerator
		{
			private int SjlBM8inVA_YE4YVlr_0024gluY;

			private object yT7HpVIzmqW54W307WgJtr4;

			public ulong shzwKqAT1IuKyKF_0024NhSVbNU;

			public lNpIzpGWMiPQ_5gAwjtlI6S6Js86Pudxf8nfbTsW_0024TcvJlB0LYzijk9i4IYc3PbHGAzwIeWv5bbP_0024YvJI16Sino SKCFxHGAEbVQbKCDB_0024Jj8p4;

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
			public O3fQXeTow8fxJd2T2fs3h45kbDRCT2SKd1htDC73B_cyXGcIKtB7p8a_7KEm0587y0VwmQu2jBHnUzPwN81WISjtEVeCrgsAsosPlJGbDktzsu3xAuqq7QOTGNq8L9plJrazekJ3Wr9EpM2nQHQU0KYCMRQDMUHokT1UbXDvR4wU8a06_0024U4N1QhxBzZmpB_iAxi4MSSaS1u9mB89WldvasoGJnWjBXCYvOEAXkiQGQs100xw0ljkNoQjW4DdGJ_002444A(int _003C_003E1__state)
			{
				SjlBM8inVA_YE4YVlr_0024gluY = _003C_003E1__state;
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				int num = SjlBM8inVA_YE4YVlr_0024gluY;
				if (num == -3 || num == 1)
				{
					try
					{
					}
					finally
					{
						ITybmnn_CCVC5Wu_0024wHlWVVQ();
					}
				}
				CGuar6f91zneC6jjs_GZO2I = null;
				SjlBM8inVA_YE4YVlr_0024gluY = -2;
			}

			private bool MoveNext()
			{
				try
				{
					int num = SjlBM8inVA_YE4YVlr_0024gluY;
					lNpIzpGWMiPQ_5gAwjtlI6S6Js86Pudxf8nfbTsW_0024TcvJlB0LYzijk9i4IYc3PbHGAzwIeWv5bbP_0024YvJI16Sino lNpIzpGWMiPQ_5gAwjtlI6S6Js86Pudxf8nfbTsW_0024TcvJlB0LYzijk9i4IYc3PbHGAzwIeWv5bbP_0024YvJI16Sino = SKCFxHGAEbVQbKCDB_0024Jj8p4;
					switch (num)
					{
					case 0:
						SjlBM8inVA_YE4YVlr_0024gluY = -1;
						if (o03j6WRQeHhb9kL_0024l_0024Cg6EQ.ContainsKey(shzwKqAT1IuKyKF_0024NhSVbNU))
						{
							smethod_0(lNpIzpGWMiPQ_5gAwjtlI6S6Js86Pudxf8nfbTsW_0024TcvJlB0LYzijk9i4IYc3PbHGAzwIeWv5bbP_0024YvJI16Sino.gDNZ_gBPYQjMs01VpkqvLd0, o03j6WRQeHhb9kL_0024l_0024Cg6EQ[shzwKqAT1IuKyKF_0024NhSVbNU]);
							return false;
						}
						CGuar6f91zneC6jjs_GZO2I = new WWW(global::_003CModule_003E.smethod_28<string>(86956964u) + shzwKqAT1IuKyKF_0024NhSVbNU);
						SjlBM8inVA_YE4YVlr_0024gluY = -3;
						yT7HpVIzmqW54W307WgJtr4 = CGuar6f91zneC6jjs_GZO2I;
						SjlBM8inVA_YE4YVlr_0024gluY = 1;
						return true;
					case 1:
					{
						SjlBM8inVA_YE4YVlr_0024gluY = -3;
						string text = CGuar6f91zneC6jjs_GZO2I.text;
						Match match = new Regex(kGl_EDY_CJKAZFiLrhwh0m8cpixcw9V3GGk4qftOqlZ_).Match(text);
						if (match.Success)
						{
							string text2 = HttpUtility.HtmlDecode(match.Groups[1].Value);
							o03j6WRQeHhb9kL_0024l_0024Cg6EQ.Add(shzwKqAT1IuKyKF_0024NhSVbNU, text2);
							lNpIzpGWMiPQ_5gAwjtlI6S6Js86Pudxf8nfbTsW_0024TcvJlB0LYzijk9i4IYc3PbHGAzwIeWv5bbP_0024YvJI16Sino.gDNZ_gBPYQjMs01VpkqvLd0.text = text2;
						}
						ITybmnn_CCVC5Wu_0024wHlWVVQ();
						CGuar6f91zneC6jjs_GZO2I = null;
						return false;
					}
					default:
						return false;
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
				if (CGuar6f91zneC6jjs_GZO2I != null)
				{
					smethod_1((IDisposable)CGuar6f91zneC6jjs_GZO2I);
				}
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw smethod_2();
			}

			internal static void smethod_0(Text text_0, string string_0)
			{
				text_0.text = string_0;
			}

			internal static void smethod_1(IDisposable idisposable_0)
			{
				idisposable_0.Dispose();
			}

			internal static NotSupportedException smethod_2()
			{
				return new NotSupportedException();
			}
		}

		[CompilerGenerated]
		private sealed class _74xULDVpeY_0024qkdFzCT7aZxhFvoy8l_0024lUVOPlMEvuOiapQVa0mAA4eLhXSor9bPY1cBw2_0024JnNJrggLJNPPLrZ91o4pgMuDzEllwFEuaOxyK8rJtv6s6Hj25T0NcPqL1wxwExkt_0024PSA3rP8CTXVzWBpjPNtEQOnCRFtGcYP9QH5FJv_0024EhKwBCw81NrGTYSeANR8vnwm_K1wHCTW8_uhKLo_0024N2aFSaMJ4orE2r6FihASXrvWTJ_0024t2R2h_UokoA14tc8KxuOAHv6bRpHT5LyPJ1Df8 : IEnumerator<object>, IDisposable, IEnumerator
		{
			private int SjlBM8inVA_YE4YVlr_0024gluY;

			private object yT7HpVIzmqW54W307WgJtr4;

			public string string_0;

			public lNpIzpGWMiPQ_5gAwjtlI6S6Js86Pudxf8nfbTsW_0024TcvJlB0LYzijk9i4IYc3PbHGAzwIeWv5bbP_0024YvJI16Sino SKCFxHGAEbVQbKCDB_0024Jj8p4;

			private string Z_0024x49E_fXLGwkQ__0024z_n_XDw;

			private Texture2D GR_0024utE5AHfYoLAl0nWfPa7E;

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
			public _74xULDVpeY_0024qkdFzCT7aZxhFvoy8l_0024lUVOPlMEvuOiapQVa0mAA4eLhXSor9bPY1cBw2_0024JnNJrggLJNPPLrZ91o4pgMuDzEllwFEuaOxyK8rJtv6s6Hj25T0NcPqL1wxwExkt_0024PSA3rP8CTXVzWBpjPNtEQOnCRFtGcYP9QH5FJv_0024EhKwBCw81NrGTYSeANR8vnwm_K1wHCTW8_uhKLo_0024N2aFSaMJ4orE2r6FihASXrvWTJ_0024t2R2h_UokoA14tc8KxuOAHv6bRpHT5LyPJ1Df8(int _003C_003E1__state)
			{
				SjlBM8inVA_YE4YVlr_0024gluY = _003C_003E1__state;
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				int num = SjlBM8inVA_YE4YVlr_0024gluY;
				if (num == -3 || (uint)(num - 2) <= 1u)
				{
					try
					{
					}
					finally
					{
						ITybmnn_CCVC5Wu_0024wHlWVVQ();
					}
				}
				Z_0024x49E_fXLGwkQ__0024z_n_XDw = null;
				GR_0024utE5AHfYoLAl0nWfPa7E = null;
				Ciu_0024YtWQUtSHTEMGz_0024d7hKU = null;
				SjlBM8inVA_YE4YVlr_0024gluY = -2;
			}

			private bool MoveNext()
			{
				try
				{
					int num = SjlBM8inVA_YE4YVlr_0024gluY;
					lNpIzpGWMiPQ_5gAwjtlI6S6Js86Pudxf8nfbTsW_0024TcvJlB0LYzijk9i4IYc3PbHGAzwIeWv5bbP_0024YvJI16Sino lNpIzpGWMiPQ_5gAwjtlI6S6Js86Pudxf8nfbTsW_0024TcvJlB0LYzijk9i4IYc3PbHGAzwIeWv5bbP_0024YvJI16Sino = SKCFxHGAEbVQbKCDB_0024Jj8p4;
					switch (num)
					{
					default:
						return false;
					case 0:
						SjlBM8inVA_YE4YVlr_0024gluY = -1;
						Z_0024x49E_fXLGwkQ__0024z_n_XDw = global::_003CModule_003E.smethod_29<string>(3947597624u) + smethod_0((object)string_0);
						if (File.Exists(Z_0024x49E_fXLGwkQ__0024z_n_XDw))
						{
							GR_0024utE5AHfYoLAl0nWfPa7E = new Texture2D(1, 1);
							GR_0024utE5AHfYoLAl0nWfPa7E.LoadImage(File.ReadAllBytes(Z_0024x49E_fXLGwkQ__0024z_n_XDw));
							yT7HpVIzmqW54W307WgJtr4 = null;
							SjlBM8inVA_YE4YVlr_0024gluY = 1;
							return true;
						}
						goto IL_00d6;
					case 1:
						SjlBM8inVA_YE4YVlr_0024gluY = -1;
						lNpIzpGWMiPQ_5gAwjtlI6S6Js86Pudxf8nfbTsW_0024TcvJlB0LYzijk9i4IYc3PbHGAzwIeWv5bbP_0024YvJI16Sino.image_0.sprite = GR_0024utE5AHfYoLAl0nWfPa7E.PLX3X99qjMiqi8ErUqFTqg0();
						GR_0024utE5AHfYoLAl0nWfPa7E = null;
						goto IL_00d6;
					case 2:
					{
						SjlBM8inVA_YE4YVlr_0024gluY = -3;
						byte[] bytes = Ciu_0024YtWQUtSHTEMGz_0024d7hKU.bytes;
						if (Directory.Exists(global::_003CModule_003E.smethod_25<string>(4000329416u)))
						{
							File.WriteAllBytes(Z_0024x49E_fXLGwkQ__0024z_n_XDw, bytes);
						}
						yT7HpVIzmqW54W307WgJtr4 = null;
						SjlBM8inVA_YE4YVlr_0024gluY = 3;
						return true;
					}
					case 3:
						{
							SjlBM8inVA_YE4YVlr_0024gluY = -3;
							lNpIzpGWMiPQ_5gAwjtlI6S6Js86Pudxf8nfbTsW_0024TcvJlB0LYzijk9i4IYc3PbHGAzwIeWv5bbP_0024YvJI16Sino.image_0.sprite = Ciu_0024YtWQUtSHTEMGz_0024d7hKU.texture.PLX3X99qjMiqi8ErUqFTqg0();
							ITybmnn_CCVC5Wu_0024wHlWVVQ();
							Ciu_0024YtWQUtSHTEMGz_0024d7hKU = null;
							return false;
						}
						IL_00d6:
						Ciu_0024YtWQUtSHTEMGz_0024d7hKU = new WWW(string_0);
						SjlBM8inVA_YE4YVlr_0024gluY = -3;
						yT7HpVIzmqW54W307WgJtr4 = Ciu_0024YtWQUtSHTEMGz_0024d7hKU;
						SjlBM8inVA_YE4YVlr_0024gluY = 2;
						return true;
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
				if (Ciu_0024YtWQUtSHTEMGz_0024d7hKU != null)
				{
					smethod_1((IDisposable)Ciu_0024YtWQUtSHTEMGz_0024d7hKU);
				}
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw smethod_2();
			}

			internal static int smethod_0(object object_0)
			{
				return object_0.GetHashCode();
			}

			internal static void smethod_1(IDisposable idisposable_0)
			{
				idisposable_0.Dispose();
			}

			internal static NotSupportedException smethod_2()
			{
				return new NotSupportedException();
			}
		}

		private Image image_0;

		private Text cbaWBsWvNw94pK2LJRR5vB0;

		private Text gDNZ_gBPYQjMs01VpkqvLd0;

		private Control0 control0_0;

		private lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw IG45l9MmsjL1aIEfiO4N_0024y4;

		private lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw KBwjyTVa2t3BXDhnC5V2ffM;

		internal void method_0(Image preview, Text nameLabel, Text creatorLabel, Control0 subToggle, lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw openLinkBtn, lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw previewBtn)
		{
			image_0 = preview;
			cbaWBsWvNw94pK2LJRR5vB0 = nameLabel;
			gDNZ_gBPYQjMs01VpkqvLd0 = creatorLabel;
			control0_0 = subToggle;
			IG45l9MmsjL1aIEfiO4N_0024y4 = openLinkBtn;
			KBwjyTVa2t3BXDhnC5V2ffM = previewBtn;
		}

		internal void ZzKQ_nyipnINy9MDt2NFQPk()
		{
			smethod_1(image_0, smethod_0().PLX3X99qjMiqi8ErUqFTqg0());
			if (!smethod_3((UnityEngine.Object)smethod_2(image_0), (UnityEngine.Object)null))
			{
				smethod_1(image_0, smethod_4().PLX3X99qjMiqi8ErUqFTqg0());
				smethod_5(cbaWBsWvNw94pK2LJRR5vB0, "");
				smethod_5(gDNZ_gBPYQjMs01VpkqvLd0, "");
			}
		}

		internal void PhOJBv5ufEFl4v8YBeynXCw(U1GmT9ZMi6XvQgaRUcEHGTJ2ox6n_00242ODVjhi9OQlSQvI8A2HT5RLkW_0024sQ5gp2RyZESs49PI9r7mOSRMi5MsAhOI wsi)
		{
			smethod_5(cbaWBsWvNw94pK2LJRR5vB0, wsi.hJJJX3iFC8vYXCIx0eUwbqk);
			smethod_5(gDNZ_gBPYQjMs01VpkqvLd0, "");
			control0_0.hLxnG9Hq33zU_YUsu_00240_zak = wsi.YhiMemOciw_0024oL1zy4ye1Ets;
			IG45l9MmsjL1aIEfiO4N_0024y4.t2iJT_tBPyB6QRMBLAdXYUs(delegate
			{
				string string_ = global::_003CModule_003E.smethod_29<string>(3783644332u);
				PublishedFileId_t nxch5NN7yn_2gEoq38N_tzo = wsi.nxch5NN7yn_2gEoq38N_tzo;
				iRKkttvNnP57a4eDUGpehAYibg6o91gaoyif01fMvDmUBIHCPHk3H5WMI0vHgf_wjWeven3x3hgu3N2mIS5n8wFxU4QGwK0eNyxCXNcd17Skb30PfbZ7DFagCih5RRmQvJriMFpdW0AHOmD3yfF1Zbe2N5pR90SI3L64LnVsngnGyHTm1boWPVCZNgCt8yDCfj4an_vx2W_0024Y7Omn9cQb9ugpef5EbYPM2TsEqUhBTz1EFDYRPei_ybcp_ktOAd9kpp1baTU5E5n2lW5K48dwf_o.smethod_1(iRKkttvNnP57a4eDUGpehAYibg6o91gaoyif01fMvDmUBIHCPHk3H5WMI0vHgf_wjWeven3x3hgu3N2mIS5n8wFxU4QGwK0eNyxCXNcd17Skb30PfbZ7DFagCih5RRmQvJriMFpdW0AHOmD3yfF1Zbe2N5pR90SI3L64LnVsngnGyHTm1boWPVCZNgCt8yDCfj4an_vx2W_0024Y7Omn9cQb9ugpef5EbYPM2TsEqUhBTz1EFDYRPei_ybcp_ktOAd9kpp1baTU5E5n2lW5K48dwf_o.smethod_0(string_, nxch5NN7yn_2gEoq38N_tzo.ToString()));
			});
			KBwjyTVa2t3BXDhnC5V2ffM.t2iJT_tBPyB6QRMBLAdXYUs(delegate
			{
				if (iRKkttvNnP57a4eDUGpehAYibg6o91gaoyif01fMvDmUBIHCPHk3H5WMI0vHgf_wjWeven3x3hgu3N2mIS5n8wFxU4QGwK0eNyxCXNcd17Skb30PfbZ7DFagCih5RRmQvJriMFpdW0AHOmD3yfF1Zbe2N5pR90SI3L64LnVsngnGyHTm1boWPVCZNgCt8yDCfj4an_vx2W_0024Y7Omn9cQb9ugpef5EbYPM2TsEqUhBTz1EFDYRPei_ybcp_ktOAd9kpp1baTU5E5n2lW5K48dwf_o.smethod_2(Class29.kk_0024srNdLfgKNQLYd7jaYjyg))
				{
					MPatchr.ShowDebugMsg(iRKkttvNnP57a4eDUGpehAYibg6o91gaoyif01fMvDmUBIHCPHk3H5WMI0vHgf_wjWeven3x3hgu3N2mIS5n8wFxU4QGwK0eNyxCXNcd17Skb30PfbZ7DFagCih5RRmQvJriMFpdW0AHOmD3yfF1Zbe2N5pR90SI3L64LnVsngnGyHTm1boWPVCZNgCt8yDCfj4an_vx2W_0024Y7Omn9cQb9ugpef5EbYPM2TsEqUhBTz1EFDYRPei_ybcp_ktOAd9kpp1baTU5E5n2lW5K48dwf_o.smethod_0(global::_003CModule_003E.smethod_25<string>(2282985757u), xh28Je5vGZzmlyaYTv4leqahX_0024d76HU68fsrFhxkr_aiwJvGWxEZr2ULd9ujX_W1gg.UzhVmO15k8UHVkcjd2D2eFw(xh28Je5vGZzmlyaYTv4leqahX_0024d76HU68fsrFhxkr_aiwJvGWxEZr2ULd9ujX_W1gg.eEsL_CqOa2k0Oy16TqJNFVJPuzSIPe8Trw5760Yzq7e5ghLIwg7ZyOeyUc9A2E9z_0024mUEQCN09awhbakTSEXMPkahOFo0RcQ91HbGZkSM_Bhl.workshop_closeExistingPreview)));
				}
				else
				{
					MPatchr.ShowDebugMsg(iRKkttvNnP57a4eDUGpehAYibg6o91gaoyif01fMvDmUBIHCPHk3H5WMI0vHgf_wjWeven3x3hgu3N2mIS5n8wFxU4QGwK0eNyxCXNcd17Skb30PfbZ7DFagCih5RRmQvJriMFpdW0AHOmD3yfF1Zbe2N5pR90SI3L64LnVsngnGyHTm1boWPVCZNgCt8yDCfj4an_vx2W_0024Y7Omn9cQb9ugpef5EbYPM2TsEqUhBTz1EFDYRPei_ybcp_ktOAd9kpp1baTU5E5n2lW5K48dwf_o.smethod_0(global::_003CModule_003E.smethod_29<string>(1675020134u), xh28Je5vGZzmlyaYTv4leqahX_0024d76HU68fsrFhxkr_aiwJvGWxEZr2ULd9ujX_W1gg.UzhVmO15k8UHVkcjd2D2eFw(xh28Je5vGZzmlyaYTv4leqahX_0024d76HU68fsrFhxkr_aiwJvGWxEZr2ULd9ujX_W1gg.eEsL_CqOa2k0Oy16TqJNFVJPuzSIPe8Trw5760Yzq7e5ghLIwg7ZyOeyUc9A2E9z_0024mUEQCN09awhbakTSEXMPkahOFo0RcQ91HbGZkSM_Bhl.workshop_previewLoading)));
					string text = global::_003CModule_003E.smethod_27<string>(4083179126u);
					PublishedFileId_t nxch5NN7yn_2gEoq38N_tzo = wsi.nxch5NN7yn_2gEoq38N_tzo;
					mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(text + nxch5NN7yn_2gEoq38N_tzo.ToString() + global::_003CModule_003E.smethod_26<string>(3009992766u) + iRKkttvNnP57a4eDUGpehAYibg6o91gaoyif01fMvDmUBIHCPHk3H5WMI0vHgf_wjWeven3x3hgu3N2mIS5n8wFxU4QGwK0eNyxCXNcd17Skb30PfbZ7DFagCih5RRmQvJriMFpdW0AHOmD3yfF1Zbe2N5pR90SI3L64LnVsngnGyHTm1boWPVCZNgCt8yDCfj4an_vx2W_0024Y7Omn9cQb9ugpef5EbYPM2TsEqUhBTz1EFDYRPei_ybcp_ktOAd9kpp1baTU5E5n2lW5K48dwf_o.smethod_3(wsi.nxch5NN7yn_2gEoq38N_tzo, bool_0: true));
					Q5GS7XFtq4kccIJybzbeAWY = wsi.nxch5NN7yn_2gEoq38N_tzo;
					if (BNY2Hoxd9qRiNISDjVrLHPM == null)
					{
						BNY2Hoxd9qRiNISDjVrLHPM = Callback<DownloadItemResult_t>.Create(delegate(DownloadItemResult_t item)
						{
							if (!(item.m_unAppID != Steam.OEDCBNHNGMJ.OMFPIMHBOKE) && !(Y_KnsAySLoCDb4SHb_0024ozdg9Jhandbv5CjRS2vvjE93HwxrEnN8gRKN_3gH8ZJ8dFtwLfFaqEhK1SoCTTPpPBgcQ.smethod_1().name != global::_003CModule_003E.smethod_28<string>(102257120u)) && !(Q5GS7XFtq4kccIJybzbeAWY != item.m_nPublishedFileId))
							{
								Q5GS7XFtq4kccIJybzbeAWY = default(PublishedFileId_t);
								bool flag = (SteamUGC.GetItemState(item.m_nPublishedFileId) & 1) == 1;
								SteamUGC.GetItemInstallInfo(item.m_nPublishedFileId, out var punSizeOnDisk, out var pchFolder, 1024u, out var punTimeStamp);
								mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(string.Format(global::_003CModule_003E.smethod_25<string>(930772342u), item.m_unAppID, item.m_nPublishedFileId, item.m_eResult, punSizeOnDisk, punTimeStamp));
								string[] files = Directory.GetFiles(pchFolder);
								foreach (string text2 in files)
								{
									if (text2.EndsWith(global::_003CModule_003E.smethod_26<string>(976003231u)) || text2.EndsWith(global::_003CModule_003E.smethod_28<string>(223620919u)))
									{
										y5kmSOqVaOjhMvxRjeJAZmo = LNGKNOGOIKL.FMAGAEMFION<BuildData>(File.ReadAllText(text2));
										mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_27<string>(3068708960u) + text2, bool_0: true);
										break;
									}
								}
								files = Directory.GetFiles(pchFolder);
								foreach (string text3 in files)
								{
									if (text3.EndsWith(global::_003CModule_003E.smethod_28<string>(2682238246u)))
									{
										nu9wqOavhI9USqcxKp2D2JdN6BvnIdeDULV1uF0wgOAy = LNGKNOGOIKL.FMAGAEMFION<AssignData>(File.ReadAllText(text3));
										mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_26<string>(2621503955u) + text3, bool_0: true);
										break;
									}
								}
								if (!flag)
								{
									Directory.Delete(pchFolder, recursive: true);
								}
								if (y5kmSOqVaOjhMvxRjeJAZmo != null)
								{
									BuildData buildData = null;
									if (JKGKJLLFMLE.HHGILAIOCLG != null)
									{
										buildData = JKGKJLLFMLE.HHGILAIOCLG.Clone();
										buildData.isReady = JKGKJLLFMLE.HHGILAIOCLG.isReady;
									}
									JKGKJLLFMLE.HHGILAIOCLG = y5kmSOqVaOjhMvxRjeJAZmo;
									xH535ybWa6_0024ItQpcTo8vW9E.ACMGPBMMKNI(LBOKOPEGKGE: true, LGBGNLDPMNN: true);
									JKGKJLLFMLE.HHGILAIOCLG = buildData;
									Bounds mFGJHOHNCDB = xH535ybWa6_0024ItQpcTo8vW9E.MFGJHOHNCDB;
									if (E6YS52rBYK_lITFDcP_00244LpQ != null)
									{
										UnityEngine.Object.Destroy(E6YS52rBYK_lITFDcP_00244LpQ);
										E6YS52rBYK_lITFDcP_00244LpQ = null;
									}
									E6YS52rBYK_lITFDcP_00244LpQ = new GameObject(global::_003CModule_003E.smethod_26<string>(2502783770u));
									float num = mFGJHOHNCDB.size.y * 0.5f;
									Transform obj = xH535ybWa6_0024ItQpcTo8vW9E.CLNMBHMCPGB[0].transform;
									obj.localPosition = Vector3.up * num - mFGJHOHNCDB.center;
									E6YS52rBYK_lITFDcP_00244LpQ.transform.localScale = Vector3.one;
									obj.parent = E6YS52rBYK_lITFDcP_00244LpQ.transform;
									float f = Mathf.Max(Mathf.Max(mFGJHOHNCDB.size.x, mFGJHOHNCDB.size.z), num);
									E6YS52rBYK_lITFDcP_00244LpQ.transform.localScale = Vector3.one * (10f / Mathf.Sqrt(f));
									BlockController[] componentsInChildren = E6YS52rBYK_lITFDcP_00244LpQ.transform.GetComponentsInChildren<BlockController>();
									for (int num2 = componentsInChildren.Length - 1; num2 >= 0; num2--)
									{
										UnityEngine.Object.Destroy(componentsInChildren[num2]);
									}
									Class29.kk_0024srNdLfgKNQLYd7jaYjyg.SetActive(value: true);
								}
							}
						});
					}
				}
			});
			control0_0.t2iJT_tBPyB6QRMBLAdXYUs(delegate(bool enabled)
			{
				if (!enabled)
				{
					iRKkttvNnP57a4eDUGpehAYibg6o91gaoyif01fMvDmUBIHCPHk3H5WMI0vHgf_wjWeven3x3hgu3N2mIS5n8wFxU4QGwK0eNyxCXNcd17Skb30PfbZ7DFagCih5RRmQvJriMFpdW0AHOmD3yfF1Zbe2N5pR90SI3L64LnVsngnGyHTm1boWPVCZNgCt8yDCfj4an_vx2W_0024Y7Omn9cQb9ugpef5EbYPM2TsEqUhBTz1EFDYRPei_ybcp_ktOAd9kpp1baTU5E5n2lW5K48dwf_o.smethod_4(wsi.nxch5NN7yn_2gEoq38N_tzo);
				}
				else
				{
					iRKkttvNnP57a4eDUGpehAYibg6o91gaoyif01fMvDmUBIHCPHk3H5WMI0vHgf_wjWeven3x3hgu3N2mIS5n8wFxU4QGwK0eNyxCXNcd17Skb30PfbZ7DFagCih5RRmQvJriMFpdW0AHOmD3yfF1Zbe2N5pR90SI3L64LnVsngnGyHTm1boWPVCZNgCt8yDCfj4an_vx2W_0024Y7Omn9cQb9ugpef5EbYPM2TsEqUhBTz1EFDYRPei_ybcp_ktOAd9kpp1baTU5E5n2lW5K48dwf_o.smethod_5(wsi.nxch5NN7yn_2gEoq38N_tzo);
				}
			});
			smethod_6((MonoBehaviour)this);
			smethod_7((MonoBehaviour)this, mdN32ztxlkJWT1Fck6hAVJM(wsi.yxqgtybS7ik_0024wDkg_BT5Bpw));
			smethod_7((MonoBehaviour)this, method_1(wsi.K52jeLH_0024D_rsZoa7xVSPVPk));
		}

		private IEnumerator method_1(ulong authorID)
		{
			if (o03j6WRQeHhb9kL_0024l_0024Cg6EQ.ContainsKey(authorID))
			{
				O3fQXeTow8fxJd2T2fs3h45kbDRCT2SKd1htDC73B_cyXGcIKtB7p8a_7KEm0587y0VwmQu2jBHnUzPwN81WISjtEVeCrgsAsosPlJGbDktzsu3xAuqq7QOTGNq8L9plJrazekJ3Wr9EpM2nQHQU0KYCMRQDMUHokT1UbXDvR4wU8a06_0024U4N1QhxBzZmpB_iAxi4MSSaS1u9mB89WldvasoGJnWjBXCYvOEAXkiQGQs100xw0ljkNoQjW4DdGJ_002444A.smethod_0(gDNZ_gBPYQjMs01VpkqvLd0, o03j6WRQeHhb9kL_0024l_0024Cg6EQ[authorID]);
				yield break;
			}
			WWW wWW = new WWW(global::_003CModule_003E.smethod_28<string>(86956964u) + authorID);
			try
			{
				yield return wWW;
				string text = wWW.text;
				Match match = new Regex(kGl_EDY_CJKAZFiLrhwh0m8cpixcw9V3GGk4qftOqlZ_).Match(text);
				if (match.Success)
				{
					string text2 = HttpUtility.HtmlDecode(match.Groups[1].Value);
					o03j6WRQeHhb9kL_0024l_0024Cg6EQ.Add(authorID, text2);
					gDNZ_gBPYQjMs01VpkqvLd0.text = text2;
				}
			}
			finally
			{
				if (wWW != null)
				{
					O3fQXeTow8fxJd2T2fs3h45kbDRCT2SKd1htDC73B_cyXGcIKtB7p8a_7KEm0587y0VwmQu2jBHnUzPwN81WISjtEVeCrgsAsosPlJGbDktzsu3xAuqq7QOTGNq8L9plJrazekJ3Wr9EpM2nQHQU0KYCMRQDMUHokT1UbXDvR4wU8a06_0024U4N1QhxBzZmpB_iAxi4MSSaS1u9mB89WldvasoGJnWjBXCYvOEAXkiQGQs100xw0ljkNoQjW4DdGJ_002444A.smethod_1((IDisposable)wWW);
				}
			}
		}

		private IEnumerator mdN32ztxlkJWT1Fck6hAVJM(string url)
		{
			string path = global::_003CModule_003E.smethod_29<string>(3947597624u) + _74xULDVpeY_0024qkdFzCT7aZxhFvoy8l_0024lUVOPlMEvuOiapQVa0mAA4eLhXSor9bPY1cBw2_0024JnNJrggLJNPPLrZ91o4pgMuDzEllwFEuaOxyK8rJtv6s6Hj25T0NcPqL1wxwExkt_0024PSA3rP8CTXVzWBpjPNtEQOnCRFtGcYP9QH5FJv_0024EhKwBCw81NrGTYSeANR8vnwm_K1wHCTW8_uhKLo_0024N2aFSaMJ4orE2r6FihASXrvWTJ_0024t2R2h_UokoA14tc8KxuOAHv6bRpHT5LyPJ1Df8.smethod_0((object)url);
			if (File.Exists(path))
			{
				Texture2D texture2D = new Texture2D(1, 1);
				texture2D.LoadImage(File.ReadAllBytes(path));
				yield return null;
				image_0.sprite = texture2D.PLX3X99qjMiqi8ErUqFTqg0();
			}
			WWW wWW = new WWW(url);
			try
			{
				yield return wWW;
				byte[] bytes = wWW.bytes;
				if (Directory.Exists(global::_003CModule_003E.smethod_25<string>(4000329416u)))
				{
					File.WriteAllBytes(path, bytes);
				}
				yield return null;
				image_0.sprite = wWW.texture.PLX3X99qjMiqi8ErUqFTqg0();
			}
			finally
			{
				if (wWW != null)
				{
					_74xULDVpeY_0024qkdFzCT7aZxhFvoy8l_0024lUVOPlMEvuOiapQVa0mAA4eLhXSor9bPY1cBw2_0024JnNJrggLJNPPLrZ91o4pgMuDzEllwFEuaOxyK8rJtv6s6Hj25T0NcPqL1wxwExkt_0024PSA3rP8CTXVzWBpjPNtEQOnCRFtGcYP9QH5FJv_0024EhKwBCw81NrGTYSeANR8vnwm_K1wHCTW8_uhKLo_0024N2aFSaMJ4orE2r6FihASXrvWTJ_0024t2R2h_UokoA14tc8KxuOAHv6bRpHT5LyPJ1Df8.smethod_1((IDisposable)wWW);
				}
			}
		}

		internal static Texture2D smethod_0()
		{
			return Texture2D.whiteTexture;
		}

		internal static void smethod_1(Image image_1, Sprite sprite_0)
		{
			image_1.sprite = sprite_0;
		}

		internal static Sprite smethod_2(Image image_1)
		{
			return image_1.sprite;
		}

		internal static bool smethod_3(UnityEngine.Object object_0, UnityEngine.Object object_1)
		{
			return object_0 == object_1;
		}

		internal static Texture2D smethod_4()
		{
			return Texture2D.blackTexture;
		}

		internal static void smethod_5(Text text_0, string string_0)
		{
			text_0.text = string_0;
		}

		internal static void smethod_6(MonoBehaviour monoBehaviour_0)
		{
			monoBehaviour_0.StopAllCoroutines();
		}

		internal static Coroutine smethod_7(MonoBehaviour monoBehaviour_0, IEnumerator ienumerator_0)
		{
			return monoBehaviour_0.StartCoroutine(ienumerator_0);
		}
	}

	private static readonly string kGl_EDY_CJKAZFiLrhwh0m8cpixcw9V3GGk4qftOqlZ_ = global::_003CModule_003E.smethod_27<string>(3748090632u);

	private static HIPBCCKFFAG xH535ybWa6_0024ItQpcTo8vW9E;

	internal static BuildData y5kmSOqVaOjhMvxRjeJAZmo;

	internal static AssignData nu9wqOavhI9USqcxKp2D2JdN6BvnIdeDULV1uF0wgOAy;

	private static GameObject E6YS52rBYK_lITFDcP_00244LpQ;

	private static Dictionary<ulong, string> o03j6WRQeHhb9kL_0024l_0024Cg6EQ = new Dictionary<ulong, string>();

	private static PublishedFileId_t Q5GS7XFtq4kccIJybzbeAWY = default(PublishedFileId_t);

	internal static bool OB3U2zm9e2wliliNbaRFPAY = false;

	private static Callback<DownloadItemResult_t> BNY2Hoxd9qRiNISDjVrLHPM;

	internal static void smethod_0()
	{
		SystemData iGOBPLOLHEP = JKGKJLLFMLE.IGOBPLOLHEP;
		JKGKJLLFMLE.EGFHGHKLNAO = JKGKJLLFMLE.LENPCAMMAEP.Practice;
		JKGKJLLFMLE.JMOEMCPIEJL = iGOBPLOLHEP.practiceWorldType;
		JKGKJLLFMLE.NMGPDCIMFPN = iGOBPLOLHEP.practiceAreaMode;
		JKGKJLLFMLE.EPJKDGGFDIF = 0;
		JKGKJLLFMLE.FOCFAHGFEOB = 16;
		JKGKJLLFMLE.CDEIANEIODO = 1f;
		JKGKJLLFMLE.HOLDKCHPGJL = 1f;
		JKGKJLLFMLE.MHJBJGEFECP = (float)iGOBPLOLHEP.practiceChargeRate * 0.01f;
		JKGKJLLFMLE.AKEMCMINMBC = 0.5f;
		ATCyo_0024qpPFqNSISN7PgDzuPM7bZZWP2fcNoTkefIy5A2WLvcWT1w9UCw3Yq7lMwlug.YMAh_TshgArfNEGhpCaDjcg(global::_003CModule_003E.smethod_27<string>(1057839826u), bool_0: true);
	}

	internal static void cWprFpE_0024XxnWwovl_0024W4wiJPl35YRVsMzGgMrTst5Fl4_(DownloadItemResult_t item)
	{
		if (item.m_unAppID != Steam.OEDCBNHNGMJ.OMFPIMHBOKE || smethod_1().name != global::_003CModule_003E.smethod_28<string>(102257120u) || Q5GS7XFtq4kccIJybzbeAWY != item.m_nPublishedFileId)
		{
			return;
		}
		Q5GS7XFtq4kccIJybzbeAWY = default(PublishedFileId_t);
		bool flag = (SteamUGC.GetItemState(item.m_nPublishedFileId) & 1) == 1;
		SteamUGC.GetItemInstallInfo(item.m_nPublishedFileId, out var punSizeOnDisk, out var pchFolder, 1024u, out var punTimeStamp);
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(string.Format(global::_003CModule_003E.smethod_25<string>(930772342u), item.m_unAppID, item.m_nPublishedFileId, item.m_eResult, punSizeOnDisk, punTimeStamp));
		string[] files = Directory.GetFiles(pchFolder);
		foreach (string text in files)
		{
			if (text.EndsWith(global::_003CModule_003E.smethod_26<string>(976003231u)) || text.EndsWith(global::_003CModule_003E.smethod_28<string>(223620919u)))
			{
				y5kmSOqVaOjhMvxRjeJAZmo = LNGKNOGOIKL.FMAGAEMFION<BuildData>(File.ReadAllText(text));
				mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_27<string>(3068708960u) + text, bool_0: true);
				break;
			}
		}
		files = Directory.GetFiles(pchFolder);
		foreach (string text2 in files)
		{
			if (text2.EndsWith(global::_003CModule_003E.smethod_28<string>(2682238246u)))
			{
				nu9wqOavhI9USqcxKp2D2JdN6BvnIdeDULV1uF0wgOAy = LNGKNOGOIKL.FMAGAEMFION<AssignData>(File.ReadAllText(text2));
				mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_26<string>(2621503955u) + text2, bool_0: true);
				break;
			}
		}
		if (!flag)
		{
			Directory.Delete(pchFolder, recursive: true);
		}
		if (y5kmSOqVaOjhMvxRjeJAZmo != null)
		{
			BuildData buildData = null;
			if (JKGKJLLFMLE.HHGILAIOCLG != null)
			{
				buildData = JKGKJLLFMLE.HHGILAIOCLG.Clone();
				buildData.isReady = JKGKJLLFMLE.HHGILAIOCLG.isReady;
			}
			JKGKJLLFMLE.HHGILAIOCLG = y5kmSOqVaOjhMvxRjeJAZmo;
			xH535ybWa6_0024ItQpcTo8vW9E.ACMGPBMMKNI(LBOKOPEGKGE: true, LGBGNLDPMNN: true);
			JKGKJLLFMLE.HHGILAIOCLG = buildData;
			Bounds mFGJHOHNCDB = xH535ybWa6_0024ItQpcTo8vW9E.MFGJHOHNCDB;
			if (E6YS52rBYK_lITFDcP_00244LpQ != null)
			{
				UnityEngine.Object.Destroy(E6YS52rBYK_lITFDcP_00244LpQ);
				E6YS52rBYK_lITFDcP_00244LpQ = null;
			}
			E6YS52rBYK_lITFDcP_00244LpQ = new GameObject(global::_003CModule_003E.smethod_26<string>(2502783770u));
			float num = mFGJHOHNCDB.size.y * 0.5f;
			Transform transform = xH535ybWa6_0024ItQpcTo8vW9E.CLNMBHMCPGB[0].transform;
			transform.localPosition = Vector3.up * num - mFGJHOHNCDB.center;
			E6YS52rBYK_lITFDcP_00244LpQ.transform.localScale = Vector3.one;
			transform.parent = E6YS52rBYK_lITFDcP_00244LpQ.transform;
			float f = Mathf.Max(Mathf.Max(mFGJHOHNCDB.size.x, mFGJHOHNCDB.size.z), num);
			E6YS52rBYK_lITFDcP_00244LpQ.transform.localScale = Vector3.one * (10f / Mathf.Sqrt(f));
			BlockController[] componentsInChildren = E6YS52rBYK_lITFDcP_00244LpQ.transform.GetComponentsInChildren<BlockController>();
			for (int num2 = componentsInChildren.Length - 1; num2 >= 0; num2--)
			{
				UnityEngine.Object.Destroy(componentsInChildren[num2]);
			}
			Class29.kk_0024srNdLfgKNQLYd7jaYjyg.SetActive(value: true);
		}
	}

	internal static Scene smethod_1()
	{
		return SceneManager.GetActiveScene();
	}
}
