using System;
using System.Reflection;
using HarmonyLib;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
	// Completes the Server Scripts API for Individual/Legacy rooms. The original
	// implementation assumes PhotonPlayer/PhotonView even when the game is using
	// Unity's legacy NetworkView transport.
	internal static class LegacyServerScripts
	{
		private const string PatchId = "local.moddev.machinecraft.server-scripts-legacy.v1";
		private static Harmony harmony;

		internal static void TryRegister()
		{
			if (harmony != null)
				return;

			try
			{
				MethodInfo target = AccessTools.Method(typeof(Game), "OnPlayerDisconnected", new Type[] { typeof(NetworkPlayer) });
				MethodInfo postfix = AccessTools.Method(typeof(LegacyServerScripts), "OnPlayerDisconnectedPostfix");
				if (target == null || postfix == null)
					throw new MissingMethodException("Game.OnPlayerDisconnected(NetworkPlayer)");

				harmony = new Harmony(PatchId);
				harmony.Patch(target, null, new HarmonyMethod(postfix), null, null);
				Log("REGISTERED transport=Legacy callbacks=join,switch,chat,death,leave");
			}
			catch (Exception error)
			{
				Log("REGISTER_FAILED type=" + error.GetType().Name + " message=" + error.Message);
			}
		}

		private static void OnPlayerDisconnectedPostfix(NetworkPlayer __0)
		{
			NetworkPlayer player = __0;
			if (HNJDDKJLHMM.FHLGOMHPDLN != HNJDDKJLHMM.HKGAACMIPIH.Legacy
				|| !HNJDDKJLHMM.NIKEKIIPJFI)
			{
				return;
			}

			try
			{
				MCNPlayer departing = null;
				MCNPlayer[] snapshot = MCNServer.smethod_0(true);
				foreach (MCNPlayer candidate in snapshot)
				{
					if (candidate != null && candidate.MatchesLegacyPlayer(player))
					{
						departing = candidate;
						break;
					}
				}

				if (departing == null)
				{
					Log("LEAVE_SKIPPED guid=" + player.guid + " reason=player-not-tracked");
					return;
				}

				foreach (HostScript script in boq2Pu1wVUKBmYhCHYqNyeIJToBLrl9v4fX1Wj2K7fW80xAZGrjtRfrLlnuFUPPFo7r3VDm3ADUhB_r2BgwLSN0.xIX1nY_0024eHA9QSBbIs6EBuzs)
				{
					try
					{
						script.onPlayerLeave(departing);
					}
					catch (Exception error)
					{
						Log("CALLBACK_FAILED callback=onPlayerLeave type=" + error.GetType().Name + " message=" + error.Message);
						if (Arena.OEDCBNHNGMJ != null)
							Arena.OEDCBNHNGMJ.AddScriptLog(error.ToString(), "Server Scripts", 0);
					}
				}

				MCNServer.dPzzlzSuv9qXe46XqJWKwDU.Remove(departing);
				boq2Pu1wVUKBmYhCHYqNyeIJToBLrl9v4fX1Wj2K7fW80xAZGrjtRfrLlnuFUPPFo7r3VDm3ADUhB_r2BgwLSN0.q6xNvtRV9GPJik_Y9l8WB_s.Remove(departing.plrID);
				Log("LEAVE player=" + departing.plrID + " name=" + Quote(departing.playerName) + " guid=" + player.guid);
			}
			catch (Exception error)
			{
				Log("LEAVE_FAILED type=" + error.GetType().Name + " message=" + error.Message);
			}
		}

		internal static void LogTransport(string message)
		{
			Log(message);
		}

		private static string Quote(string value)
		{
			return value == null ? "<null>" : "\"" + value.Replace("\"", "'") + "\"";
		}

		private static void Log(string message)
		{
			string text = "[SERVER-SCRIPTS-LEGACY] " + message;
			try { global::mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(text); }
			catch (Exception) { }
		}
	}
}
