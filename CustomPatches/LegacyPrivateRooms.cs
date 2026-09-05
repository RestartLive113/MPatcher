using System;
using System.Globalization;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using HarmonyLib;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
	// Extends MPatcher's ROOM CODE field to the Unity Legacy Master Server used by
	// the Individual tab. A non-empty code selects a separate, hashed game type;
	// therefore the host is absent from the ordinary McnC catalogue and is only
	// returned to clients that enter the same code.
	internal static class LegacyPrivateRooms
	{
		private const string PatchId = "local.moddev.machinecraft.private-rooms-legacy.v1";
		private const string ProductionPrivatePrefix = "MCPR";
		private const string TestPrivatePrefix = "MCTR";
		private const int HashHexLength = 12;

		private static Harmony harmony;
		private static FieldInfo gameTypeField;
		private static FieldInfo refreshTimerField;
		private static int lobbyInstanceId;
		private static string lobbyCode;
		private static string lobbyGameType;

		internal static void BindHostRoomCodeControl(
			global::ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ control)
		{
			if ((UnityEngine.Object)control == null)
			{
				Log("HOST_ROOM_CODE_BIND_FAILED reason=null-control");
				return;
			}

			string code = GetStoredHostCode();
			control.pZEKY5TzLd4S3z2lXESoRnw = code;
			control.JNMaMdWdD3fzh8iVBUwSGz4 = SaveHostRoomCode;
			Log("HOST_ROOM_CODE_RESTORED codeLength=" + code.Length);
		}

		internal static void TryRegister()
		{
			if (harmony != null)
				return;

			try
			{
				MethodInfo configureStart = AccessTools.Method(typeof(Configure), "EOHFEIHAMHD", Type.EmptyTypes);
				MethodInfo lobbyUpdate = AccessTools.Method(typeof(Lobby), "Update", Type.EmptyTypes);
				MethodInfo configurePrefix = AccessTools.Method(typeof(LegacyPrivateRooms), "ConfigureStartPrefix");
				MethodInfo lobbyPrefix = AccessTools.Method(typeof(LegacyPrivateRooms), "LobbyUpdatePrefix");
				gameTypeField = AccessTools.Field(typeof(Connect), "JEJJNNDDLID");
				refreshTimerField = AccessTools.Field(typeof(Connect), "AOGHPEKPLKK");

				if (configureStart == null || lobbyUpdate == null || configurePrefix == null || lobbyPrefix == null
					|| gameTypeField == null || refreshTimerField == null)
				{
					throw new MissingMemberException("Legacy PrivateRooms targets");
				}

				harmony = new Harmony(PatchId);
				PatchPrefix(configureStart, configurePrefix);
				PatchPrefix(lobbyUpdate, lobbyPrefix);
				Log("REGISTERED host=Configure.EOHFEIHAMHD catalogue=Lobby.Update");
			}
			catch (Exception error)
			{
				Log("REGISTER_FAILED type=" + error.GetType().Name + " message=" + error.Message);
			}
		}

		private static void ConfigureStartPrefix(Configure __instance)
		{
			try
			{
				if (!Enabled || HNJDDKJLHMM.FHLGOMHPDLN != HNJDDKJLHMM.HKGAACMIPIH.Legacy)
					return;

				string code = GetHostCode();
				string gameType = GetGameType(code);
				gameTypeField.SetValue(__instance, gameType);
				Log("HOST_REGISTER scope=" + (code.Length == 0 ? "public" : "private")
					+ " codeLength=" + code.Length + " gameType=" + gameType);
			}
			catch (Exception error)
			{
				Log("HOST_PREFIX_FAILED type=" + error.GetType().Name + " message=" + error.Message);
			}
		}

		private static void LobbyUpdatePrefix(Lobby __instance)
		{
			try
			{
				if (!Enabled || JKGKJLLFMLE.IGOBPLOLHEP.photonRegion != 4)
					return;

				string code = GetLobbyCode();
				string gameType = GetGameType(code);
				int instanceId = __instance.GetInstanceID();
				bool changed = instanceId != lobbyInstanceId
					|| !string.Equals(code, lobbyCode, StringComparison.Ordinal)
					|| !string.Equals(gameType, lobbyGameType, StringComparison.Ordinal);

				gameTypeField.SetValue(__instance, gameType);
				if (!changed)
					return;

				lobbyInstanceId = instanceId;
				lobbyCode = code;
				lobbyGameType = gameType;
				refreshTimerField.SetValue(__instance, 0f);
				Log("CATALOGUE_SWITCH scope=" + (code.Length == 0 ? "public" : "private")
					+ " codeLength=" + code.Length + " gameType=" + gameType);
			}
			catch (Exception error)
			{
				Log("LOBBY_PREFIX_FAILED type=" + error.GetType().Name + " message=" + error.Message);
			}
		}

		private static bool Enabled
		{
			get { return MPatchrMain.MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.hiddenRooms; }
		}

		private static string GetHostCode()
		{
			global::ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ control =
				global::kILN_0024q_tbcSASqORkmJ7BBsHZ_0024NvZSWw5PyFkcF9sNb4p2Xf_0024UVaPWSXF0_0024gn8lcUA.LC0iMCkMK03PiX6mz5DQcnM;
			return (UnityEngine.Object)control == null
				? GetStoredHostCode()
				: control.pZEKY5TzLd4S3z2lXESoRnw ?? string.Empty;
		}

		private static string GetLobbyCode()
		{
			global::ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ control =
				global::v1JBKckAa1RFmn2CeELS4d1FhLzhlYwRV2bd7TgD_0024MEJnWym5unAzsCQpkwgvPK2FbBLfBqBfJdE_8ZO15q40ZU.LC0iMCkMK03PiX6mz5DQcnM;
			return (UnityEngine.Object)control == null
				? string.Empty
				: control.pZEKY5TzLd4S3z2lXESoRnw ?? string.Empty;
		}

		private static string GetStoredHostCode()
		{
			settingsIngame settings = MPatchrMain.MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68;
			return settings == null || settings.roomCode == null ? string.Empty : settings.roomCode;
		}

		private static void SaveHostRoomCode(string value)
		{
			try
			{
				settingsIngame settings = MPatchrMain.MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68;
				if (settings == null)
					throw new InvalidOperationException("settings unavailable");

				string code = value ?? string.Empty;
				if (string.Equals(settings.roomCode ?? string.Empty, code, StringComparison.Ordinal))
					return;

				settings.roomCode = code;
				settings.UUiRNMwxRbfk_Fs4cDErRoM();
				Log("HOST_ROOM_CODE_SAVED codeLength=" + code.Length);
			}
			catch (Exception error)
			{
				Log("HOST_ROOM_CODE_SAVE_FAILED type=" + error.GetType().Name + " message=" + error.Message);
			}
		}

		private static string GetGameType(string code)
		{
			if (string.IsNullOrEmpty(code))
				return JKGKJLLFMLE.KIEMANLPECC ? "MC_TEST" : "McnC";

			byte[] input = Encoding.UTF8.GetBytes(code);
			byte[] digest;
			using (SHA256 algorithm = new SHA256Managed())
				digest = algorithm.ComputeHash(input);

			StringBuilder hash = new StringBuilder(HashHexLength);
			for (int index = 0; hash.Length < HashHexLength; index++)
				hash.Append(digest[index].ToString("x2", CultureInfo.InvariantCulture));

			return (JKGKJLLFMLE.KIEMANLPECC ? TestPrivatePrefix : ProductionPrivatePrefix) + hash;
		}

		private static void PatchPrefix(MethodInfo original, MethodInfo prefixMethod)
		{
			HarmonyMethod prefix = new HarmonyMethod(prefixMethod);
			prefix.priority = Priority.First;
			harmony.Patch(original, prefix, null, null, null);
		}

		private static void Log(string message)
		{
			string text = "[PRIVATEROOMS-LEGACY] " + message;
			try { global::mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(text); }
			catch (Exception) { }
		}
	}
}
