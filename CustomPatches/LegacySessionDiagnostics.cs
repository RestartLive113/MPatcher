using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using HarmonyLib;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
	// Observes the Legacy session after Network.Connect succeeds. This patch does
	// not resend RPCs, retry downloads, disconnect peers or change timeouts.
	internal static class LegacySessionDiagnostics
	{
		private const string PatchId = "mpatcher.legacy-session-diagnostics.v1";
		private static readonly List<PendingAsset> pendingAssets = new List<PendingAsset>();
		private static Harmony harmony;
		private static float connectedAt = -1f;

		internal static void TryRegister()
		{
			if (harmony != null) return;
			Harmony candidate = new Harmony(PatchId);
			try
			{
				Patch(candidate, typeof(Lobby), "OnConnectedToServer", "ConnectedPrefix", Type.EmptyTypes);
				Patch(candidate, typeof(Game), "Start", "GameStartPrefix", Type.EmptyTypes);
				Patch(candidate, typeof(Game), "Update", "GameUpdatePrefix", Type.EmptyTypes);
				Patch(candidate, typeof(Game), "RPC_SyncTextureWorld", "TextureRpcPrefix",
					new Type[] { typeof(string), typeof(string), typeof(int) });
				Patch(candidate, typeof(Game), "RPC_DownloadStage", "StageRpcPrefix",
					new Type[] { typeof(string), typeof(int) });
				MethodInfo stage = AccessTools.Method(typeof(Game), "KDIHOKHHIID", new Type[] { typeof(string), typeof(int) });
				if (stage == null || stage.ReturnType != typeof(IEnumerator)) throw new MissingMethodException("Game stage iterator");
				candidate.Patch(stage, null, new HarmonyMethod(AccessTools.Method(typeof(LegacySessionDiagnostics), "StageIteratorPostfix")));
				Patch(candidate, typeof(Arena), "LoadTexture", "LoadTexturePrefix",
					new Type[] { typeof(string), typeof(int) });
				Patch(candidate, typeof(Arena), "LoadWorld", "LoadWorldPrefix",
					new Type[] { typeof(string), typeof(int) });
				Patch(candidate, typeof(Game), "OnDisconnectedFromServer", "GameDisconnectedPrefix",
					new Type[] { typeof(NetworkDisconnection) });
				Patch(candidate, typeof(Configure), "OnDisconnectedFromServer", "ConfigureDisconnectedPrefix",
					new Type[] { typeof(NetworkDisconnection) });
				Patch(candidate, typeof(Connect), "OnMasterServerEvent", "MasterEventPrefix",
					new Type[] { typeof(MasterServerEvent) });
				Patch(candidate, typeof(Connect), "OnFailedToConnectToMasterServer", "MasterFailedPrefix",
					new Type[] { typeof(NetworkConnectionError) });
				harmony = candidate;
				Log("REGISTERED version=2 behavior=observe-only assets=texture,world,stage stageDownload=progress+error disconnectReason=enabled masterEvents=enabled");
			}
			catch (Exception error)
			{
				candidate.UnpatchAll(PatchId);
				Log("REGISTER_FAILED type=" + error.GetType().Name + " message=" + Clean(error.Message));
			}
		}

		private static void Patch(Harmony candidate, Type type, string name, string prefix, Type[] parameters)
		{
			MethodInfo target = AccessTools.Method(type, name, parameters);
			if (target == null) throw new MissingMethodException(type.Name, name);
			MethodInfo hook = AccessTools.Method(typeof(LegacySessionDiagnostics), prefix);
			if (hook == null) throw new MissingMethodException(typeof(LegacySessionDiagnostics).Name, prefix);
			candidate.Patch(target, new HarmonyMethod(hook), null, null);
		}

		private static void ConnectedPrefix()
		{
			connectedAt = Time.realtimeSinceStartup;
			pendingAssets.Clear();
			Log("SESSION_CONNECTED peer=" + Network.peerType + " local=" + Clean(Network.player.ipAddress));
		}

		private static void GameStartPrefix()
		{
			if (!IsLegacy()) return;
			Log("GAME_START elapsed=" + SessionElapsed() + " peer=" + Network.peerType
				+ " isClient=" + Network.isClient + " isServer=" + Network.isServer);
		}

		private static void TextureRpcPrefix(string __0, string __1, int __2)
		{
			if (!IsLegacy()) return;
			Track("texture", __1, __2, true);
			Log("TEXTURE_RPC name=" + Clean(__0) + " url=" + SafeUri(__1) + " responseHash=" + __2);
		}

		private static void StageRpcPrefix(string __0, int __1)
		{
			if (!IsLegacy()) return;
			string url = __0;
			bool mirrored = !string.IsNullOrEmpty(url) && url[0] == '*';
			if (mirrored) url = url.Substring(1);
			Track("stage", url, __1, false);
			Log("STAGE_RPC url=" + SafeUri(url) + " responseHash=" + __1 + " mirrored=" + mirrored);
		}

		private static void StageIteratorPostfix(string __0, ref IEnumerator __result)
		{
			if (IsLegacy()) __result = ObserveStage(__result, __0);
		}
		private static IEnumerator ObserveStage(IEnumerator original, string url)
		{
			float started = Time.realtimeSinceStartup;
			bool completed = false;
			Log("STAGE_ITERATOR_BEGIN url=" + SafeUri(url.TrimStart('*')));
			try
			{
				while (StageMoveNext(original))
				{
					WWW download = original.Current as WWW;
					if (download == null) { yield return original.Current; continue; }
					float next = 0f;
					while (!download.isDone)
					{
						if (Time.realtimeSinceStartup >= next)
						{
							next = Time.realtimeSinceStartup + 10f;
							Log("STAGE_HTTP_PENDING elapsed=" + Number(Time.realtimeSinceStartup - started) + " progress=" + Number(download.progress));
						}
						yield return null;
					}
					Log("STAGE_HTTP_COMPLETE elapsed=" + Number(Time.realtimeSinceStartup - started)
						+ " error=" + Clean(download.error) + " bytes=" + download.bytes.Length);
				}
				completed = true;
			}
			finally
			{
				IDisposable disposable = original as IDisposable;
				if (disposable != null) disposable.Dispose();
				Log("STAGE_ITERATOR_END completed=" + completed + " elapsed=" + Number(Time.realtimeSinceStartup - started));
			}
		}
		private static bool StageMoveNext(IEnumerator original)
		{
			try { return original.MoveNext(); }
			catch (Exception error) { Log("STAGE_ITERATOR_ERROR " + error); throw; }
		}

		private static void LoadTexturePrefix(string __0, int __1)
		{
			if (IsLegacy()) Log("LOAD_TEXTURE url=" + SafeUri(__0) + " responseHash=" + __1);
		}

		private static void LoadWorldPrefix(string __0, int __1)
		{
			if (IsLegacy()) Log("LOAD_WORLD url=" + SafeUri(__0) + " responseHash=" + __1);
		}

		private static void GameUpdatePrefix()
		{
			if (pendingAssets.Count == 0) return;
			float now = Time.realtimeSinceStartup;
			for (int i = pendingAssets.Count - 1; i >= 0; i--)
			{
				PendingAsset asset = pendingAssets[i];
				try
				{
					if (File.Exists(asset.Path))
					{
						long size = new FileInfo(asset.Path).Length;
						Log("CACHE_READY type=" + asset.Kind + " elapsed=" + Number(now - asset.StartedAt)
							+ " bytes=" + size + " file=" + Clean(Path.GetFileName(asset.Path)));
						pendingAssets.RemoveAt(i);
					}
					else if (now - asset.StartedAt >= 60f)
					{
						Log("CACHE_TIMEOUT type=" + asset.Kind + " elapsed=" + Number(now - asset.StartedAt)
							+ " file=" + Clean(Path.GetFileName(asset.Path)));
						pendingAssets.RemoveAt(i);
					}
				}
				catch (Exception error)
				{
					Log("CACHE_CHECK_FAILED type=" + asset.Kind + " error=" + error.GetType().Name);
					pendingAssets.RemoveAt(i);
				}
			}
		}

		private static void GameDisconnectedPrefix(NetworkDisconnection __0)
		{
			LogDisconnect("GAME_DISCONNECTED", __0);
		}

		private static void ConfigureDisconnectedPrefix(NetworkDisconnection __0)
		{
			LogDisconnect("CONFIGURE_DISCONNECTED", __0);
		}

		private static void LogDisconnect(string label, NetworkDisconnection reason)
		{
			if (!IsLegacy() && connectedAt < 0f) return;
			Log(label + " reason=" + reason + " elapsed=" + SessionElapsed() + " peer=" + Network.peerType
				+ " pendingAssets=" + pendingAssets.Count);
			for (int i = 0; i < pendingAssets.Count; i++)
			{
				PendingAsset asset = pendingAssets[i];
				Log("CACHE_PENDING_AT_DISCONNECT type=" + asset.Kind + " elapsed="
					+ Number(Time.realtimeSinceStartup - asset.StartedAt)
					+ " exists=" + File.Exists(asset.Path) + " file=" + Clean(Path.GetFileName(asset.Path)));
			}
		}

		private static void MasterEventPrefix(MasterServerEvent __0)
		{
			Log("MASTER_EVENT event=" + __0 + " master=" + MasterServer.ipAddress + ":" + MasterServer.port
				+ " peer=" + Network.peerType + " elapsed=" + SessionElapsed());
		}

		private static void MasterFailedPrefix(NetworkConnectionError __0)
		{
			Log("MASTER_FAILED error=" + __0 + " master=" + MasterServer.ipAddress + ":" + MasterServer.port
				+ " peer=" + Network.peerType + " elapsed=" + SessionElapsed());
		}

		private static void Track(string kind, string url, int responseHash, bool includeFileName)
		{
			try
			{
				if (string.IsNullOrEmpty(url))
				{
					Log("CACHE_EXPECTED type=" + kind + " unavailable=empty-url");
					return;
				}
				string path = CachePath(url, responseHash, includeFileName);
				bool exists = File.Exists(path);
				long size = exists ? new FileInfo(path).Length : 0L;
				Log("CACHE_EXPECTED type=" + kind + " exists=" + exists + " bytes=" + size
					+ " file=" + Clean(Path.GetFileName(path)));
				if (!exists)
					pendingAssets.Add(new PendingAsset(kind, path, Time.realtimeSinceStartup));
			}
			catch (Exception error)
			{
				Log("CACHE_EXPECTED_FAILED type=" + kind + " error=" + error.GetType().Name);
			}
		}

		private static string CachePath(string url, int responseHash, bool includeFileName)
		{
			return FJLJNEKHKKH.OKBHDGLODJD(url, responseHash, includeFileName);
		}

		private static bool IsLegacy()
		{
			try { return HNJDDKJLHMM.FHLGOMHPDLN == HNJDDKJLHMM.HKGAACMIPIH.Legacy; }
			catch { return false; }
		}

		private static string SessionElapsed()
		{
			return connectedAt < 0f ? "unavailable" : Number(Time.realtimeSinceStartup - connectedAt);
		}

		private static string SafeUri(string value)
		{
			if (string.IsNullOrEmpty(value)) return "empty";
			try
			{
				Uri uri = new Uri(value, UriKind.Absolute);
				return Clean(uri.GetLeftPart(UriPartial.Path));
			}
			catch { return "non-absolute/hash=" + value.GetHashCode().ToString(CultureInfo.InvariantCulture); }
		}

		private static string Number(float value) { return value.ToString("0.000", CultureInfo.InvariantCulture); }
		private static string Clean(string value) { return (value ?? "").Replace("\r", " ").Replace("\n", " "); }

		private static void Log(string message)
		{
			try { mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng("[LEGACY-SESSION] " + message); }
			catch { }
		}

		private sealed class PendingAsset
		{
			internal readonly string Kind;
			internal readonly string Path;
			internal readonly float StartedAt;

			internal PendingAsset(string kind, string path, float startedAt)
			{
				Kind = kind;
				Path = path;
				StartedAt = startedAt;
			}
		}
	}
}
