using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
	// File diagnostics for the actual Individual connect path. The associated
	// retry helper repeats only transient Legacy GUID/NAT attempts.
	internal static class LegacyConnectionDiagnostics
	{
		private const string PatchId = "mpatcher.legacy-connection-diagnostics.v1";
		private static Harmony harmony;
		private static FieldInfo timerField;
		private static bool pending;
		private static bool natAttempt;
		private static int sequence;
		private static float startedAt;
		private static NetworkPeerType lastPeer;

		internal static void TryRegister()
		{
			if (harmony != null) return;
			Harmony candidate = new Harmony(PatchId);
			try
			{
				timerField = AccessTools.Field(typeof(Connect), "OCFLJMCKNHB");
				if (timerField == null || timerField.FieldType != typeof(float))
					throw new MissingFieldException("Connect.OCFLJMCKNHB");
				Patch(candidate, typeof(Lobby), "BDKIMPEDKCJ", null, "EntryPostfix", "EntryTranspiler",
					new Type[] { typeof(string), typeof(GameObject) });
				Patch(candidate, typeof(Connect), "OnFailedToConnect", "FailedPrefix", null, null,
					new Type[] { typeof(NetworkConnectionError) });
				Patch(candidate, typeof(Lobby), "OnConnectedToServer", "ConnectedPrefix", null, null, Type.EmptyTypes);
				Patch(candidate, typeof(Lobby), "BFMGHLNCKJK", "ResetPrefix", null, null, Type.EmptyTypes);
				Patch(candidate, typeof(Lobby), "Update", "UpdatePrefix", null, null, Type.EmptyTypes);
				LegacyConnectionRetryFix.Initialize(timerField);
				harmony = candidate;
				Log("REGISTERED version=4 transport=Legacy perAttemptTimeout=unchanged autoRetry=GUID-NAT/3-attempts directHostData=unchanged");
			}
			catch (Exception error)
			{
				candidate.UnpatchAll(PatchId);
				Log("REGISTER_FAILED type=" + error.GetType().Name + " message=" + error.Message);
			}
		}

		private static void Patch(Harmony candidate, Type type, string name, string prefix,
			string postfix, string transpiler, Type[] parameters)
		{
			MethodInfo target = AccessTools.Method(type, name, parameters);
			if (target == null) throw new MissingMethodException(type.Name, name);
			candidate.Patch(target, Hook(prefix), Hook(postfix), Hook(transpiler));
		}

		private static HarmonyMethod Hook(string name)
		{
			return name == null ? null : new HarmonyMethod(AccessTools.Method(typeof(LegacyConnectionDiagnostics), name));
		}

		private static IEnumerable<CodeInstruction> EntryTranspiler(IEnumerable<CodeInstruction> instructions)
		{
			List<CodeInstruction> code = new List<CodeInstruction>(instructions);
			MethodInfo guid = AccessTools.Method(typeof(Network), "Connect", new Type[] { typeof(string) });
			MethodInfo host = AccessTools.Method(typeof(Network), "Connect", new Type[] { typeof(HostData) });
			int guidCalls = 0, hostCalls = 0;
			foreach (CodeInstruction instruction in code)
			{
				if (instruction.opcode != OpCodes.Call) continue;
				if (Equals(instruction.operand, guid))
				{
					instruction.operand = AccessTools.Method(typeof(LegacyConnectionDiagnostics), "ConnectGuid");
					guidCalls++;
				}
				else if (Equals(instruction.operand, host))
				{
					instruction.operand = AccessTools.Method(typeof(LegacyConnectionDiagnostics), "ConnectHost");
					hostCalls++;
				}
			}
			if (guidCalls != 1 || hostCalls != 1)
				throw new InvalidOperationException("Individual connect IL changed: guid=" + guidCalls + " host=" + hostCalls);
			return code;
		}

		private static NetworkConnectionError ConnectGuid(string guid)
		{
			HostData host = null;
			try
			{
				foreach (HostData item in MasterServer.PollHostList())
					if (item.guid == guid) { host = item; break; }
			}
			catch (Exception error) { Log("SNAPSHOT_FAILED type=" + error.GetType().Name); }
			Begin(host, guid, true);
			NetworkConnectionError result = Network.Connect(guid);
			Immediate(result);
			return result;
		}

		private static NetworkConnectionError ConnectHost(HostData host)
		{
			Begin(host, host == null ? "" : host.guid, host != null && host.useNat);
			NetworkConnectionError result = Network.Connect(host);
			Immediate(result);
			return result;
		}

		private static void Begin(HostData host, string guid, bool nat)
		{
			LegacyTransientReconnect.RememberTarget(host, guid, nat);
			LegacyConnectionRetryFix.Begin(guid, nat);
			try
			{
				sequence++;
				startedAt = Time.realtimeSinceStartup;
				pending = true;
				natAttempt = nat;
				lastPeer = Network.peerType;
				Log("BEGIN attempt=" + sequence + " guid=" + Clean(guid) + " useNat=" + nat
					+ " name=" + (host == null ? "unavailable" : Clean(host.gameName))
					+ " ip=" + (host == null || host.ip == null ? "unavailable" : Clean(string.Join(",", host.ip)))
					+ " port=" + (host == null ? 0 : host.port)
					+ " master=" + MasterServer.ipAddress + ":" + MasterServer.port
					+ " facilitatorAddress=" + Network.natFacilitatorIP + " facilitatorPort=" + Network.natFacilitatorPort
					+ " localAddress=" + Network.player.ipAddress + " peer=" + lastPeer);
			}
			catch (Exception error) { Log("BEGIN_LOG_FAILED type=" + error.GetType().Name); }
		}

		private static void Immediate(NetworkConnectionError result)
		{
			LegacyConnectionRetryFix.InitialResult(result);
			Log("CONNECT_RETURN attempt=" + sequence + " result=" + result);
			if (result != NetworkConnectionError.NoError) pending = false;
		}

		private static void EntryPostfix(Lobby __instance)
		{
			LegacyConnectionRetryFix.Attach(__instance);
			if (!pending) return;
			try
			{
				float timer = (float)timerField.GetValue(__instance);
				Log("UI_WAIT attempt=" + sequence + " seconds=" + Number(timer));
			}
			catch (Exception error) { Log("WAIT_LOG_FAILED type=" + error.GetType().Name); }
		}

		private static void UpdatePrefix(Lobby __instance)
		{
			LegacyConnectionRetryFix.Update(__instance);
			if (!pending) return;
			try
			{
				NetworkPeerType current = Network.peerType;
				if (current == lastPeer) return;
				lastPeer = current;
				Log("STATE attempt=" + sequence + " elapsed=" + Elapsed() + " peer=" + current);
			}
			catch (Exception error) { Log("STATE_LOG_FAILED type=" + error.GetType().Name); pending = false; }
		}

		private static bool FailedPrefix(NetworkConnectionError __0)
		{
			bool allowOriginal = LegacyConnectionRetryFix.HandleFailure(__0);
			if (pending)
			{
				Log("NATIVE_FAILED attempt=" + sequence + " elapsed=" + Elapsed() + " result=" + __0
					+ " automaticRetry=" + (!allowOriginal));
				if (allowOriginal) pending = false;
			}
			return allowOriginal;
		}

		private static void ConnectedPrefix()
		{
			LegacyConnectionRetryFix.Connected();
			if (!pending) return;
			Log("CONNECTED attempt=" + sequence + " elapsed=" + Elapsed() + " useNat=" + natAttempt);
			pending = false;
		}

		private static bool ResetPrefix(Lobby __instance)
		{
			if (!LegacyConnectionRetryFix.BeforeReset(__instance)) return false;
			if (!pending) return true;
			Log("UI_RESET attempt=" + sequence + " elapsed=" + Elapsed() + " peer=" + Network.peerType);
			pending = false;
			return true;
		}

		private static string Elapsed() { return Number(Time.realtimeSinceStartup - startedAt); }
		private static string Number(float value) { return value.ToString("0.000", CultureInfo.InvariantCulture); }
		private static string Clean(string value) { return (value ?? "").Replace("\r", " ").Replace("\n", " "); }
		private static void Log(string message)
		{
			try { mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng("[LEGACY-CONNECT] " + message); }
			catch { } // Diagnostic output must never interrupt native connection logic.
		}
	}
}
