using System;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
	internal static partial class LegacyReconnectTarget
	{
		internal static bool MigrationAdvertised { get; private set; }
		internal static string MigrationSession { get { return discoverySession; } }
		private static string discoverySession;
		private static string discoveryType;
		private static string discoveryMaster;
		private static int discoveryMasterPort;
		private static bool discoveryPending;
		private static float discoveryDeadline;
		private static float nextDiscovery;

		private static void ConfigureDiscovery(HostData host)
		{
			MigrationAdvertised = host != null && LegacyHostRoutePolicy.ReadField(host.comment, "MPC") == "1";
			discoverySession = host == null ? null : LegacyHostRoutePolicy.Session(host.comment);
			discoveryType = host == null ? null : host.gameType;
			if (string.IsNullOrEmpty(discoveryType)) discoveryType = JKGKJLLFMLE.KIEMANLPECC ? "MC_TEST" : "McnC";
			discoveryMaster = MasterServer.ipAddress;
			discoveryMasterPort = MasterServer.port;
			discoveryPending = false;
			LegacyTransientReconnect.Log("TARGET_DISCOVERY_CONFIGURED identity="
				+ (discoverySession == null ? "guid" : "live-session") + " type=" + discoveryType);
		}

		internal static void BeginDiscovery(float now)
		{
			discoveryPending = false;
			nextDiscovery = now;
		}

		internal static void TickDiscovery(float now)
		{
			if (!valid || string.IsNullOrEmpty(discoveryType)
				|| discoverySession == null && (string.IsNullOrEmpty(guid) || guid == "0")) return;
			if (discoveryMaster != MasterServer.ipAddress || discoveryMasterPort != MasterServer.port) return;
			if (discoveryPending)
			{
				if (now >= discoveryDeadline) DiscoveryFailed(now, "reply-timeout");
				return;
			}
			if (now < nextDiscovery) return;
			discoveryPending = true;
			discoveryDeadline = now + 12f;
			nextDiscovery = now + 10f;
			try
			{
				MasterServer.ClearHostList();
				MasterServer.RequestHostList(discoveryType);
				LegacyTransientReconnect.Log("CLIENT_TARGET_QUERY type=" + discoveryType
					+ " previousTarget=" + Description);
			}
			catch (Exception error) { DiscoveryFailed(now, error.GetType().Name); }
		}

		internal static void DiscoveryReply(float now)
		{
			if (!discoveryPending || now > discoveryDeadline || discoveryMaster != MasterServer.ipAddress
				|| discoveryMasterPort != MasterServer.port) return;
			try
			{
				HostData match = null;
				foreach (HostData candidate in MasterServer.PollHostList())
				{
					if (candidate == null || candidate.gameType != discoveryType
						|| !LegacyHostRoutePolicy.Matches(discoverySession, guid, candidate.comment, candidate.guid)) continue;
					if (match != null)
					{
						LegacyTransientReconnect.Log("CLIENT_TARGET_QUERY_REJECTED reason=ambiguous-live-session");
						return;
					}
					match = candidate;
				}
				if (match == null)
				{
					LegacyTransientReconnect.Log("CLIENT_TARGET_QUERY_MISSING originalTargetRetained=true");
					return;
				}
				string[] refreshed = LegacyHostRoutePolicy.Addresses(match.comment, match.ip);
				if (string.IsNullOrEmpty(match.guid) || match.guid == "0"
					|| !useNat && (match.port <= 0 || match.port > 65535 || refreshed.Length == 0)) return;
				string before = Description;
				guid = match.guid;
				if (refreshed.Length > 0) addresses = refreshed;
				if (match.port > 0 && match.port <= 65535) port = match.port;
				LegacyTransientReconnect.Log("CLIENT_TARGET_REFRESHED changed=" + (before != Description)
					+ " target=" + Description + " identity=" + (discoverySession == null ? "guid" : "live-session")
					+ " connectionMode=preserved");
			}
			catch (Exception error) { DiscoveryFailed(now, error.GetType().Name); }
			finally { discoveryPending = false; nextDiscovery = now + 10f; }
		}

		internal static void DiscoveryFailed(float now, string reason)
		{
			// This callback only changes discovery, never the active game transport.
			if (!discoveryPending && now < nextDiscovery) return;
			discoveryPending = false;
			nextDiscovery = now + 10f;
			LegacyTransientReconnect.Log("CLIENT_TARGET_QUERY_FAILED reason="
				+ LegacyTransientReconnect.Clean(reason) + " originalTargetRetained=true");
		}
	}
}
