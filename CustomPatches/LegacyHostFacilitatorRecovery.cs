using System;
using System.Globalization;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
	// One recovery owner shared by auto-port verification and the published host.
	// Only the existing facilitator Connect call is repeated; the game peer lives.
	internal static class LegacyHostFacilitatorRecovery
	{
		private static readonly LegacyHostFacilitatorHealthPolicy health = new LegacyHostFacilitatorHealthPolicy();
		private static readonly LegacyHostFacilitatorRetryPolicy retry = new LegacyHostFacilitatorRetryPolicy();
		private static string identity;
		private static float nextTick;
		private static float nextHeartbeat;
		internal static bool Pending { get { return retry.Pending; } }
		internal static int Attempts { get { return retry.Attempts; } }
		internal static LegacyHostFacilitatorHealth Health { get { return health.Health; } }

		internal static void Reset(string reason)
		{
			identity = null;
			nextTick = nextHeartbeat = 0f;
			health.Begin(Time.realtimeSinceStartup);
			retry.Reset(reason);
		}

		internal static void Update(bool ownsLiveHost, string source)
		{
			if (!ownsLiveHost || !LegacyHostStability.IsLegacyHost() || LegacyHostMigration.Active)
			{
				Reset("captured host stopped or changed");
				return;
			}
			string guid = Network.player.guid;
			int port = Network.player.port;
			if (string.IsNullOrEmpty(guid) || guid == "0" || port <= 0) return;
			string current = guid + "/" + port + "/" + Network.natFacilitatorIP + "/" + Network.natFacilitatorPort;
			float now = Time.realtimeSinceStartup;
			if (identity != current)
			{
				Reset("host identity or facilitator endpoint changed");
				identity = current;
				health.Begin(now);
				nextHeartbeat = now + 30f;
				Log("FACILITATOR_HEALTH_BOUND guid=" + guid + " port=" + port + " source=" + source
					+ " settleSeconds=10 catalogueIsHealthEvidence=false");
			}
			if (now < nextTick) return;
			nextTick = now + 0.25f;
			try
			{
				IntPtr manager;
				bool nativePending = LegacyHostFacilitatorReconnectFix.TryGetPending(out manager);
				if (health.ProbeDue(now))
				{
					LegacyHostFacilitatorHealth previous = health.Health;
					LegacyHostFacilitatorHealth observed = LegacyHostFacilitatorReconnectFix.ReadHealth(out manager);
					bool confirmedMissing = health.Observe(now, observed);
					if (previous != observed || confirmedMissing || now >= nextHeartbeat)
						Log("FACILITATOR_HEALTH_SAMPLE native=" + observed + " missingSamples=" + health.MissingSamples
							+ " nativeLossFlag=" + nativePending + " source=" + source + " remoteAdmission=unverified");
					if (observed == LegacyHostFacilitatorHealth.Connected)
					{
						LegacyHostFacilitatorReconnectFix.ClearPending();
						nativePending = false;
					}
					else if (confirmedMissing && !nativePending && LegacyHostFacilitatorReconnectFix.MarkMissing(manager))
					{
						nativePending = true;
						Log("FACILITATOR_HEALTH_MISSING reason=two-native-terminal-samples nativeLossCallbackRequired=false source=" + source);
					}
				}
				bool wasPending = retry.Pending;
				int previousAttempts = retry.Attempts;
				// Native first-attempt remains intact. Pending cannot cause a new
				// Connect while the verified peer is connecting/closing/unknown.
				LegacyHostFacilitatorAction action = retry.Tick(now, true, nativePending,
					health.Health == LegacyHostFacilitatorHealth.Missing);
				if (!wasPending && retry.Pending)
					Log("FACILITATOR_LOST reason=native-loss-or-confirmed-missing nativeFirstAttempt=unchanged retryDue="
						+ retry.DueAt.ToString("F3", CultureInfo.InvariantCulture));
				else if (wasPending && !retry.Pending)
				{
					LegacyHostFacilitatorHealth restored = LegacyHostFacilitatorReconnectFix.ReadHealth(out manager);
					if (restored == LegacyHostFacilitatorHealth.Connected)
						Log("FACILITATOR_RESTORED retries=" + previousAttempts + " native=Connected"
							+ " sessionReinitialized=false existingPlayersDisconnected=false");
					else
						Log("FACILITATOR_RECOVERY_RESET retries=" + previousAttempts + " native=" + restored
							+ " restorationConfirmed=false reason=native-loss-flag-unavailable");
				}
				if (action == LegacyHostFacilitatorAction.Reconnect)
				{
					int result = LegacyHostFacilitatorReconnectFix.TryReconnect(manager);
					Log("FACILITATOR_RETRY attempt=" + retry.Attempts + " nativeQueued=" + (result > 0)
						+ " nativeResult=" + result + " nextDue=" + retry.DueAt.ToString("F3", CultureInfo.InvariantCulture)
						+ " source=" + source + " sessionReinitialized=false existingPlayersDisconnected=false");
				}
				if (now >= nextHeartbeat)
				{
					nextHeartbeat = now + 30f;
					Log("FACILITATOR_HEALTH_HEARTBEAT native=" + health.Health + " pending=" + retry.Pending
						+ " attempts=" + retry.Attempts + " guid=" + guid + " connections=" + Network.connections.Length
						+ " source=" + source + " remoteAdmission=unverified");
				}
			}
			catch (Exception error)
			{
				health.Begin(now);
				Log("FACILITATOR_HEALTH_FAILED type=" + error.GetType().Name + " message=" + LegacyHostStability.Clean(error.Message));
			}
		}

		private static void Log(string message) { LegacyHostStability.Log(message); }
	}
}
