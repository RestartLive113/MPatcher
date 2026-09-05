using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using HarmonyLib;
using MPatchrMain;
using McnCraft;
using UnityEngine;

internal static class boq2Pu1wVUKBmYhCHYqNyeIJToBLrl9v4fX1Wj2K7fW80xAZGrjtRfrLlnuFUPPFo7r3VDm3ADUhB_r2BgwLSN0
{
	[HarmonyPatch("Start")]
	[HarmonyPatch(typeof(Host))]
	internal static class _0024sJ3urUFa23oy83oXCnfFmDVwTEIW_08ibLGJPG_0024GJkLL3Oj0DBI6OZZOkWgAUQeRSVwlhmpK37zEPeMyVmcGxZQhZ36pMiXfojyBtEj4MR_
	{
		[Serializable]
		[CompilerGenerated]
		private sealed class SCsLTt6zoNpXZ7z1iGuF58U27lrLjcCcsQ_FBldRZrRfKYL7znimihDQMgcMEV3XvABQItQX7CtkOM8dbxDvk7T23J4yICArIDbLc_0024Q1XVtz
		{
			public static readonly SCsLTt6zoNpXZ7z1iGuF58U27lrLjcCcsQ_FBldRZrRfKYL7znimihDQMgcMEV3XvABQItQX7CtkOM8dbxDvk7T23J4yICArIDbLc_0024Q1XVtz _003C_003E9 = new SCsLTt6zoNpXZ7z1iGuF58U27lrLjcCcsQ_FBldRZrRfKYL7znimihDQMgcMEV3XvABQItQX7CtkOM8dbxDvk7T23J4yICArIDbLc_0024Q1XVtz();

			public static Action<lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw> _003C_003E9__0_0;

			internal void aJB1bIYDRm_0024alz4_00246Y6_epI(lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw me)
			{
				smethod_0(Arena.OEDCBNHNGMJ, global::_003CModule_003E.smethod_26<string>(1722323549u), global::_003CModule_003E.smethod_25<string>(4160311683u), 0);
				QTkZL40_nfuTYtAocpZAVE8();
			}

			internal static void smethod_0(Arena arena_0, string string_0, string string_1, int int_0)
			{
				arena_0.AddScriptLog(string_0, string_1, int_0);
			}
		}

		[HarmonyPostfix]
		internal static void FeUAVwFbW6wGJJdNimZY9yI(Host __instance)
		{
			if (!smethod_0(MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.hostScripts))
			{
				Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.P3tLroX6fcPfQha_0024JdbpYXg(global::_003CModule_003E.smethod_27<string>(1125860446u), new Vector3(440f, 305f), global::_003CModule_003E.smethod_27<string>(739638935u), delegate
				{
					SCsLTt6zoNpXZ7z1iGuF58U27lrLjcCcsQ_FBldRZrRfKYL7znimihDQMgcMEV3XvABQItQX7CtkOM8dbxDvk7T23J4yICArIDbLc_0024Q1XVtz.smethod_0(Arena.OEDCBNHNGMJ, global::_003CModule_003E.smethod_26<string>(1722323549u), global::_003CModule_003E.smethod_25<string>(4160311683u), 0);
					QTkZL40_nfuTYtAocpZAVE8();
				}, GameObject.Find(global::_003CModule_003E.smethod_26<string>(3103631283u)).smethod_0(global::_003CModule_003E.smethod_29<string>(2346221616u)).transform.parent, 20).UzVS61irgJn5Pnqwx0lThng(new Vector2(150f, 40f));
			}
			QTkZL40_nfuTYtAocpZAVE8();
		}

		internal static void QTkZL40_nfuTYtAocpZAVE8()
		{
			foreach (HostScript xIX1nY_0024eHA9QSBbIs6EBuz in xIX1nY_0024eHA9QSBbIs6EBuzs)
			{
				try
				{
					xIX1nY_0024eHA9QSBbIs6EBuz.onDestroy();
				}
				catch (Exception exception_)
				{
					smethod_2(Arena.OEDCBNHNGMJ, smethod_1(exception_), global::_003CModule_003E.smethod_25<string>(2982091682u), 0);
					smethod_2(Arena.OEDCBNHNGMJ, smethod_3(exception_), global::_003CModule_003E.smethod_25<string>(2982091682u), 0);
				}
			}
			xIX1nY_0024eHA9QSBbIs6EBuzs.Clear();
			if (smethod_0(MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.hostScripts))
			{
				return;
			}
			CompilerResults compilerResults_ = rJ_GZCaJwYznjXdT4CwqWDAG_0024hZrAYgw3km2DZEzhET1_00243gtR_6ZmXjiR6ngG1wl7Q.smethod_1(smethod_4(MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.hostScripts, new char[1] { ';' }));
			if (smethod_5(compilerResults_) == null || smethod_7((CollectionBase)smethod_6(compilerResults_)) > 0)
			{
				return;
			}
			smethod_2(Arena.OEDCBNHNGMJ, global::_003CModule_003E.smethod_27<string>(2856869190u), global::_003CModule_003E.smethod_27<string>(1311983146u), 0);
			Assembly assembly_ = smethod_5(compilerResults_);
			List<HostScript> list = new List<HostScript>();
			Type[] array = smethod_8(assembly_);
			foreach (Type type_ in array)
			{
				if (smethod_10(type_, smethod_9(typeof(HostScript).TypeHandle)))
				{
					list.Add((HostScript)smethod_11(type_));
				}
			}
			foreach (HostScript item in list)
			{
				if (item.pluginCreator != null && item.pluginName != null)
				{
					xIX1nY_0024eHA9QSBbIs6EBuzs.Add(item);
					smethod_2(Arena.OEDCBNHNGMJ, smethod_12(global::_003CModule_003E.smethod_25<string>(2800160654u), (object)item.pluginName, (object)item.pluginCreator), global::_003CModule_003E.smethod_29<string>(2300314811u), 0);
				}
				else
				{
					smethod_2(Arena.OEDCBNHNGMJ, smethod_15(smethod_14((MemberInfo)smethod_13((object)item)), global::_003CModule_003E.smethod_25<string>(1662461448u)), global::_003CModule_003E.smethod_29<string>(2624812852u), 0);
				}
				try
				{
					item.onInit();
				}
				catch (Exception exception_2)
				{
					smethod_2(Arena.OEDCBNHNGMJ, smethod_1(exception_2), global::_003CModule_003E.smethod_28<string>(10900805u), 0);
					smethod_2(Arena.OEDCBNHNGMJ, smethod_3(exception_2), global::_003CModule_003E.smethod_29<string>(2593000757u), 0);
				}
			}
		}

		internal static bool smethod_0(string string_0)
		{
			return string.IsNullOrEmpty(string_0);
		}

		internal static string smethod_1(Exception exception_0)
		{
			return exception_0.Message;
		}

		internal static void smethod_2(Arena arena_0, string string_0, string string_1, int int_0)
		{
			arena_0.AddScriptLog(string_0, string_1, int_0);
		}

		internal static string smethod_3(Exception exception_0)
		{
			return exception_0.StackTrace;
		}

		internal static string[] smethod_4(string string_0, char[] char_0)
		{
			return string_0.Split(char_0);
		}

		internal static Assembly smethod_5(CompilerResults compilerResults_0)
		{
			return compilerResults_0.CompiledAssembly;
		}

		internal static CompilerErrorCollection smethod_6(CompilerResults compilerResults_0)
		{
			return compilerResults_0.Errors;
		}

		internal static int smethod_7(CollectionBase collectionBase_0)
		{
			return collectionBase_0.Count;
		}

		internal static Type[] smethod_8(Assembly assembly_0)
		{
			return assembly_0.GetTypes();
		}

		internal static Type smethod_9(RuntimeTypeHandle runtimeTypeHandle_0)
		{
			return Type.GetTypeFromHandle(runtimeTypeHandle_0);
		}

		internal static bool smethod_10(Type type_0, Type type_1)
		{
			return type_0.IsSubclassOf(type_1);
		}

		internal static object smethod_11(Type type_0)
		{
			return Activator.CreateInstance(type_0);
		}

		internal static string smethod_12(string string_0, object object_0, object object_1)
		{
			return string.Format(string_0, object_0, object_1);
		}

		internal static Type smethod_13(object object_0)
		{
			return object_0.GetType();
		}

		internal static string smethod_14(MemberInfo memberInfo_0)
		{
			return memberInfo_0.Name;
		}

		internal static string smethod_15(string string_0, string string_1)
		{
			return string_0 + string_1;
		}
	}

	[HarmonyPatch(typeof(MachineController))]
	[HarmonyPatch("RPC_SyncPlayerName")]
	internal static class Class46
	{
		[HarmonyPostfix]
		internal static void FeUAVwFbW6wGJJdNimZY9yI(MachineController __instance)
		{
			if (!HNJDDKJLHMM.NIKEKIIPJFI)
			{
				return;
			}
			MCNPlayer mCNPlayer = MCNServer.iyCMH8XqR8q4d_MbL6JGTluLbwIGhTxt9OQJCHTQYnTI(__instance);
			if (mCNPlayer == null)
			{
				return;
			}
			bool flag = false;
			if (q6xNvtRV9GPJik_Y9l8WB_s.Contains(mCNPlayer.plrID))
			{
				flag = true;
			}
			else
			{
				q6xNvtRV9GPJik_Y9l8WB_s.Add(mCNPlayer.plrID);
			}
			if (HNJDDKJLHMM.FHLGOMHPDLN == HNJDDKJLHMM.HKGAACMIPIH.Legacy)
				MPatcherFork.CustomPatches.LegacyServerScripts.LogTransport((flag ? "SWITCH" : "JOIN") + " player=" + mCNPlayer.plrID + " name=\"" + mCNPlayer.playerName + "\"");
			foreach (HostScript xIX1nY_0024eHA9QSBbIs6EBuz in xIX1nY_0024eHA9QSBbIs6EBuzs)
			{
				try
				{
					if (!flag)
					{
						xIX1nY_0024eHA9QSBbIs6EBuz.onPlayerJoin(mCNPlayer);
					}
					else
					{
						xIX1nY_0024eHA9QSBbIs6EBuz.onPlayerSwitchedMachine(mCNPlayer);
					}
				}
				catch (Exception exception_)
				{
					smethod_1(Arena.OEDCBNHNGMJ, smethod_0(exception_), global::_003CModule_003E.smethod_28<string>(10900805u), 0);
					smethod_1(Arena.OEDCBNHNGMJ, smethod_2(exception_), global::_003CModule_003E.smethod_26<string>(4075268875u), 0);
				}
			}
		}

		internal static string smethod_0(Exception exception_0)
		{
			return exception_0.Message;
		}

		internal static void smethod_1(Arena arena_0, string string_0, string string_1, int int_0)
		{
			arena_0.AddScriptLog(string_0, string_1, int_0);
		}

		internal static string smethod_2(Exception exception_0)
		{
			return exception_0.StackTrace;
		}
	}

	[HarmonyPatch(typeof(MachineController))]
	[HarmonyPatch("RPC_Chat")]
	internal static class fWvgZH1Uh15yXdrDTaG_0024OCUO8Sz9M82PV6wpYB6vYRkKIjbnwhlITCvKZXWJyuvN28h8bQY_tAhQnjUxLOqtGUPxKeX4NhzJIzLkwkBMQh1xJJx6B6_JZtzdl06S87si5w
	{
		[HarmonyPostfix]
		internal static void FeUAVwFbW6wGJJdNimZY9yI(MachineController __instance, string DDMLCAJGAID, string KNNKJJMKAAI)
		{
			if (!HNJDDKJLHMM.NIKEKIIPJFI)
			{
				return;
			}
			MCNPlayer player = MCNServer.iyCMH8XqR8q4d_MbL6JGTluLbwIGhTxt9OQJCHTQYnTI(__instance);
			if (HNJDDKJLHMM.FHLGOMHPDLN == HNJDDKJLHMM.HKGAACMIPIH.Legacy)
				MPatcherFork.CustomPatches.LegacyServerScripts.LogTransport("CHAT player=" + (player == null ? 0 : player.plrID) + " chars=" + (KNNKJJMKAAI == null ? 0 : KNNKJJMKAAI.Length));
			foreach (HostScript xIX1nY_0024eHA9QSBbIs6EBuz in xIX1nY_0024eHA9QSBbIs6EBuzs)
			{
				try
				{
					xIX1nY_0024eHA9QSBbIs6EBuz.onChatMessage(player, KNNKJJMKAAI);
				}
				catch (Exception exception_)
				{
					smethod_1(Arena.OEDCBNHNGMJ, smethod_0(exception_), global::_003CModule_003E.smethod_28<string>(10900805u), 0);
					smethod_1(Arena.OEDCBNHNGMJ, smethod_2(exception_), global::_003CModule_003E.smethod_26<string>(4075268875u), 0);
				}
			}
		}

		internal static string smethod_0(Exception exception_0)
		{
			return exception_0.Message;
		}

		internal static void smethod_1(Arena arena_0, string string_0, string string_1, int int_0)
		{
			arena_0.AddScriptLog(string_0, string_1, int_0);
		}

		internal static string smethod_2(Exception exception_0)
		{
			return exception_0.StackTrace;
		}
	}

	[HarmonyPatch(typeof(MachineController))]
	[HarmonyPatch("RPC_SyncExplosion")]
	internal static class E0nx12SufeXO3_0024RORc_9HBJFGtbii5CdRbIG18PODSX4P2uoRyQ5d_0024tq0D3MrztITnUYdEXyL02gwAFReKj8j8tTD5CdvZtEfaW_0024L1kW8vCSP0NYMgh4bwxwPOekMGOlBCKQPDtX_35j51HDrimQs4U
	{
		[HarmonyPostfix]
		internal static void FeUAVwFbW6wGJJdNimZY9yI(MachineController __instance, int ILHIGOPKKKC)
		{
			if (!HNJDDKJLHMM.NIKEKIIPJFI || ILHIGOPKKKC != 0)
			{
				return;
			}
			MCNPlayer player = MCNServer.iyCMH8XqR8q4d_MbL6JGTluLbwIGhTxt9OQJCHTQYnTI(__instance);
			if (HNJDDKJLHMM.FHLGOMHPDLN == HNJDDKJLHMM.HKGAACMIPIH.Legacy)
				MPatcherFork.CustomPatches.LegacyServerScripts.LogTransport("DEATH player=" + (player == null ? 0 : player.plrID));
			foreach (HostScript xIX1nY_0024eHA9QSBbIs6EBuz in xIX1nY_0024eHA9QSBbIs6EBuzs)
			{
				try
				{
					xIX1nY_0024eHA9QSBbIs6EBuz.onDeath(player);
				}
				catch (Exception exception_)
				{
					smethod_1(Arena.OEDCBNHNGMJ, smethod_0(exception_), global::_003CModule_003E.smethod_25<string>(2982091682u), 0);
					smethod_1(Arena.OEDCBNHNGMJ, smethod_2(exception_), global::_003CModule_003E.smethod_25<string>(2982091682u), 0);
				}
			}
		}

		internal static string smethod_0(Exception exception_0)
		{
			return exception_0.Message;
		}

		internal static void smethod_1(Arena arena_0, string string_0, string string_1, int int_0)
		{
			arena_0.AddScriptLog(string_0, string_1, int_0);
		}

		internal static string smethod_2(Exception exception_0)
		{
			return exception_0.StackTrace;
		}
	}

	[HarmonyPatch(typeof(Game))]
	[HarmonyPatch("OnPhotonPlayerDisconnected")]
	internal static class YNvvIbIK3oKl15gmDYIsL3pVuqwZIJiSFZ5QsMxhKAmbwZ3pdHuixT4Gm15OW6CldXiVDLXKu05vXvw_NdzpnnTob23cmSjWsKTVD4JBsNtXLnWO5Gi6QPia77_0024e9qOW1w
	{
		[HarmonyPostfix]
		internal static void FeUAVwFbW6wGJJdNimZY9yI(OPLNFKECCLE DBLGHCEAEHC)
		{
			if (!HNJDDKJLHMM.NIKEKIIPJFI)
			{
				return;
			}
			MCNPlayer mCNPlayer = null;
			MCNPlayer[] array = MCNServer.smethod_0(bool_0: true);
			foreach (MCNPlayer mCNPlayer2 in array)
			{
				if (mCNPlayer2.plrID == smethod_0(DBLGHCEAEHC))
				{
					mCNPlayer = mCNPlayer2;
					break;
				}
			}
			if (mCNPlayer == null)
			{
				return;
			}
			foreach (HostScript xIX1nY_0024eHA9QSBbIs6EBuz in xIX1nY_0024eHA9QSBbIs6EBuzs)
			{
				try
				{
					xIX1nY_0024eHA9QSBbIs6EBuz.onPlayerLeave(mCNPlayer);
				}
				catch (Exception exception_)
				{
					smethod_2(Arena.OEDCBNHNGMJ, smethod_1(exception_), global::_003CModule_003E.smethod_28<string>(10900805u), 0);
					smethod_2(Arena.OEDCBNHNGMJ, smethod_3(exception_), global::_003CModule_003E.smethod_25<string>(2982091682u), 0);
				}
			}
			MCNServer.smethod_0();
		}

		internal static int smethod_0(OPLNFKECCLE oplnfkeccle_0)
		{
			return oplnfkeccle_0.PMCNNMLPGBB;
		}

		internal static string smethod_1(Exception exception_0)
		{
			return exception_0.Message;
		}

		internal static void smethod_2(Arena arena_0, string string_0, string string_1, int int_0)
		{
			arena_0.AddScriptLog(string_0, string_1, int_0);
		}

		internal static string smethod_3(Exception exception_0)
		{
			return exception_0.StackTrace;
		}
	}

	internal static List<HostScript> xIX1nY_0024eHA9QSBbIs6EBuzs = new List<HostScript>();

	internal static List<int> q6xNvtRV9GPJik_Y9l8WB_s = new List<int>();
}
