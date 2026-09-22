using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
	// Keeps a live Unity Legacy host published in the master catalogue. NAT
	// facilitator recovery is tracked separately and is not emulated by
	// reinitializing the server, because that would disconnect current players.
	internal static class LegacyHostStability
	{
		private const string PatchId = "mpatcher.legacy-host-stability.v2";
		private const string DriverName = "MPatcher.LegacyHostStability";
		private static Harmony harmony;
		private static LegacyHostStabilityDriver driver;
		internal static bool Registered { get { return harmony != null; } }
		internal static LegacyHostStabilityDriver Driver { get { return driver; } }

		internal static void TryRegister()
		{
			if (harmony != null) return;
			Harmony candidate = new Harmony(PatchId);
			try
			{
				MethodInfo target = AccessTools.Method(typeof(Configure), "EOHFEIHAMHD", Type.EmptyTypes);
				MethodInfo hook = AccessTools.Method(typeof(LegacyHostStability), "CaptureRegistrationCall");
				if (target == null || hook == null) throw new MissingMethodException("Legacy host registration call site");
				HarmonyMethod transpiler = new HarmonyMethod(hook);
				transpiler.priority = Priority.Last;
				candidate.Patch(target, null, null, transpiler, null);
				harmony = candidate;
				Log("REGISTERED version=3 scope=Legacy-host catalogueCheck=15 replyTimeout=12 retry=2,5,10,30 registrationAck=optional-with-bounded-list-verification endpointRefresh=network-change+60s liveSession=metadata facilitatorReconnect=separate-native-fix serverReinitialize=never");
			}
			catch (Exception error)
			{
				candidate.UnpatchAll(PatchId);
				Log("REGISTER_FAILED type=" + error.GetType().Name + " message=" + Clean(error.Message));
			}
		}

		private static IEnumerable<CodeInstruction> CaptureRegistrationCall(IEnumerable<CodeInstruction> instructions)
		{
			List<CodeInstruction> result = new List<CodeInstruction>();
			MethodInfo original = AccessTools.Method(typeof(MasterServer), "RegisterHost",
				new Type[] { typeof(string), typeof(string), typeof(string) });
			MethodInfo wrapper = AccessTools.Method(typeof(LegacyHostStability), "RegisterOriginalAndCapture");
			int count = 0;
			foreach (CodeInstruction instruction in instructions)
			{
				CodeInstruction copy = new CodeInstruction(instruction);
				if (copy.opcode == OpCodes.Call && Equals(copy.operand, original))
				{
					copy.operand = wrapper;
					count++;
				}
				result.Add(copy);
			}
			if (count != 1) throw new InvalidOperationException("Expected one Legacy RegisterHost call, found " + count);
			return result;
		}

		internal static void RegisterOriginalAndCapture(string gameType, string gameName, string comment)
		{
			if (LegacyHostAutoPort.CaptureRegistration(gameType, gameName, comment)) return;
			try
			{
				if (IsLegacyHost())
				{
					LegacyHostStabilityDriver current = EnsureDriver();
					current.Capture(gameType, gameName, comment);
					comment = current.PublishedComment();
				}
			}
			catch (Exception error)
			{
				Log("CAPTURE_FAILED type=" + error.GetType().Name + " message=" + Clean(error.Message));
			}
			MasterServer.RegisterHost(gameType, gameName, comment);
		}

		private static LegacyHostStabilityDriver EnsureDriver()
		{
			if (driver != null) return driver;
			GameObject root = GameObject.Find(DriverName);
			if (root == null)
			{
				root = new GameObject(DriverName);
				UnityEngine.Object.DontDestroyOnLoad(root);
			}
			driver = root.GetComponent<LegacyHostStabilityDriver>();
			if (driver == null) driver = root.AddComponent<LegacyHostStabilityDriver>();
			return driver;
		}

		internal static bool IsLegacyHost()
		{
			try
			{
				return HNJDDKJLHMM.FHLGOMHPDLN == HNJDDKJLHMM.HKGAACMIPIH.Legacy
					&& HNJDDKJLHMM.IOOILBCOFMF && Network.isServer;
			}
			catch { return false; }
		}

		internal static void DriverDestroyed(LegacyHostStabilityDriver value)
		{
			if (ReferenceEquals(driver, value)) driver = null;
		}

		internal static string Clean(string value)
		{
			return (value ?? "").Replace("\r", " ").Replace("\n", " ");
		}

		internal static void Log(string message)
		{
			try { mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng("[LEGACY-HOST] " + message); }
			catch { }
		}
	}

	internal sealed class LegacyHostStabilityDriver : MonoBehaviour
	{
		internal string MigrationGameType { get { return gameType; } }
		internal string MigrationGameName { get { return gameName; } }
		internal string MigrationSession { get { return liveSession; } }
		internal void MigrationRebound() { BindOrRefreshIdentity(); policy.IdentityChanged(Elapsed()); }
		private readonly LegacyHostDirectoryPolicy policy = new LegacyHostDirectoryPolicy();
		private readonly LegacyHostFacilitatorRetryPolicy facilitatorPolicy = new LegacyHostFacilitatorRetryPolicy();
		private string gameType;
		private string gameName;
		private string comment;
		private string hostGuid;
		private string masterAddress;
		private string facilitatorAddress;
		private int masterPort;
		private int facilitatorPort;
		private int gamePort;
		private bool dedicated;
		private float startedAt;
		private float nextTick;
		private float nextHeartbeat;
		private string lastState;
		private string liveSession;
		private string[] publishedAddresses = new string[0];
		private float nextAddressCheck;
		private float nextPublicationRefresh;
		private bool addressReadFailed;

		internal void Capture(string type, string name, string metadata)
		{
			ClearSession();
			if (string.IsNullOrEmpty(type) || string.IsNullOrEmpty(name))
			{
				policy.Stop("empty game type or name");
				LegacyHostStability.Log("CAPTURE_REJECTED reason=empty-metadata originalRegistration=unchanged");
				return;
			}
			gameType = type;
			gameName = name;
			comment = metadata;
			liveSession = Guid.NewGuid().ToString("N");
			nextAddressCheck = 0f;
			nextPublicationRefresh = Time.realtimeSinceStartup + 60f;
			RefreshAddresses(false);
			masterAddress = MasterServer.ipAddress;
			masterPort = MasterServer.port;
			facilitatorAddress = Network.natFacilitatorIP;
			facilitatorPort = Network.natFacilitatorPort;
			dedicated = MasterServer.dedicatedServer;
			startedAt = Time.realtimeSinceStartup;
			nextTick = startedAt;
			nextHeartbeat = startedAt + 30f;
			policy.Capture(0d);
			BindOrRefreshIdentity();
			LegacyHostStability.Log("CAPTURED type=" + LegacyHostStability.Clean(gameType)
				+ " name=" + LegacyHostStability.Clean(gameName) + " commentOmitted=true"
				+ " master=" + masterAddress + ":" + masterPort
				+ " facilitator=" + facilitatorAddress + ":" + facilitatorPort
				+ " guid=" + LegacyHostStability.Clean(hostGuid) + " port=" + gamePort);
			ReportState();
		}

		private void Update()
		{
			if (LegacyHostMigration.Active) return;
			if (LegacyHostAutoPort.Current != null) return;
			if (gameType == null || Time.realtimeSinceStartup < nextTick) return;
			nextTick = Time.realtimeSinceStartup + 0.25f;
			try
			{
				BindOrRefreshIdentity();
				double now = Elapsed();
				bool sameLiveHost = SameLiveHost();
				if (sameLiveHost)
				{
					RefreshAddresses(true);
					if (Time.realtimeSinceStartup >= nextPublicationRefresh)
					{
						nextPublicationRefresh = Time.realtimeSinceStartup + 60f;
						policy.PublicationChanged(now);
					}
				}
				LegacyHostDirectoryAction action = policy.Tracking
					? policy.Tick(now, sameLiveHost) : LegacyHostDirectoryAction.None;
				LegacyTransientReconnectController reconnect = LegacyTransientReconnect.MigrationController;
				LegacyHostMigrationDriver migration = reconnect == null ? null : reconnect.GetComponent<LegacyHostMigrationDriver>();
				if (sameLiveHost && migration != null)
					migration.ObserveCatalogue(policy.Evidence == LegacyHostDirectoryEvidence.ListedInReply, policy.Failures);
				if (LegacyHostMigration.Active) return;
				if (!policy.Tracking && !sameLiveHost)
				{
					StopTracking(policy.Reason);
					return;
				}
				if (action != LegacyHostDirectoryAction.None)
				{
					if (!sameLiveHost)
					{
						StopTracking("host changed before catalogue operation");
						return;
					}
					if (action == LegacyHostDirectoryAction.RequestList)
					{
						LegacyHostStability.Log("QUERY_SENT guid=" + LegacyHostStability.Clean(hostGuid)
							+ " reason=" + LegacyHostStability.Clean(policy.Reason));
						MasterServer.ClearHostList();
						MasterServer.RequestHostList(gameType);
					}
					else
					{
						LegacyHostStability.Log("REGISTER_SENT attempt=" + policy.RegistrationAttempts
							+ " guid=" + LegacyHostStability.Clean(hostGuid) + " transportUnchanged=true");
						MasterServer.RegisterHost(gameType, gameName, PublishedComment());
						nextPublicationRefresh = Time.realtimeSinceStartup + 60f;
					}
				}
				UpdateFacilitatorRecovery(now, sameLiveHost);
				ReportState();
				if (Time.realtimeSinceStartup >= nextHeartbeat)
				{
					nextHeartbeat = Time.realtimeSinceStartup + 30f;
					LegacyHostStability.Log("HEARTBEAT " + StateText() + " guid="
						+ LegacyHostStability.Clean(hostGuid) + " connections="
						+ (Network.isServer ? Network.connections.Length : 0)
						+ " nativeFacilitatorFix=" + LegacyHostFacilitatorReconnectFix.Applied);
				}
			}
			catch (Exception error)
			{
				StopTracking("adapter failure: " + error.GetType().Name);
				LegacyHostStability.Log("ADAPTER_FAILED type=" + error.GetType().Name
					+ " message=" + LegacyHostStability.Clean(error.Message));
			}
		}

		private void UpdateFacilitatorRecovery(double now, bool sameLiveHost)
		{
			IntPtr networkManager;
			bool nativePending = LegacyHostFacilitatorReconnectFix.TryGetPending(out networkManager);
			bool wasPending = facilitatorPolicy.Pending;
			int previousAttempts = facilitatorPolicy.Attempts;
			LegacyHostFacilitatorAction action = facilitatorPolicy.Tick(now, sameLiveHost, nativePending);
			if (!wasPending && facilitatorPolicy.Pending)
			{
				LegacyHostStability.Log("FACILITATOR_LOST nativeFirstAttempt=unchanged retryDue="
					+ facilitatorPolicy.DueAt.ToString("F3", CultureInfo.InvariantCulture));
			}
			else if (wasPending && !facilitatorPolicy.Pending && sameLiveHost)
			{
				LegacyHostStability.Log("FACILITATOR_RESTORED retries=" + previousAttempts
					+ " sessionReinitialized=false existingPlayersDisconnected=false");
			}
			if (action != LegacyHostFacilitatorAction.Reconnect) return;
			try
			{
				int result = LegacyHostFacilitatorReconnectFix.TryReconnect(networkManager);
				LegacyHostStability.Log("FACILITATOR_RETRY attempt=" + facilitatorPolicy.Attempts
					+ " nativeQueued=" + (result > 0) + " nativeResult=" + result
					+ " nextDue=" + facilitatorPolicy.DueAt.ToString("F3", CultureInfo.InvariantCulture));
			}
			catch (Exception error)
			{
				LegacyHostStability.Log("FACILITATOR_RETRY_FAILED attempt=" + facilitatorPolicy.Attempts
					+ " type=" + error.GetType().Name + " message=" + LegacyHostStability.Clean(error.Message));
			}
		}

		internal string PublishedComment()
		{
			return LegacyHostRoutePolicy.Decorate("~MPC1" + comment, liveSession, publishedAddresses);
		}

		private void RefreshAddresses(bool notify)
		{
			if (Time.realtimeSinceStartup < nextAddressCheck) return;
			nextAddressCheck = Time.realtimeSinceStartup + 2f;
			try
			{
				string[] current = LegacyHostNetworkState.ReadAddresses();
				bool changed = string.Join(",", current) != string.Join(",", publishedAddresses);
				if (changed || addressReadFailed)
				{
					publishedAddresses = current;
					if (notify) policy.PublicationChanged(Elapsed());
					LegacyHostStability.Log("NETWORK_ADDRESSES_CHANGED count=" + current.Length
						+ " addresses=" + string.Join(",", current) + " republish=" + notify
						+ " sessionReinitialized=false");
				}
				addressReadFailed = false;
			}
			catch (Exception error)
			{
				if (!addressReadFailed) LegacyHostStability.Log("NETWORK_ADDRESS_READ_FAILED type="
					+ error.GetType().Name + " nativeAddressesRetained=true");
				addressReadFailed = true;
			}
		}

		private void BindOrRefreshIdentity()
		{
			if (!CoreHostMatches()) return;
			string currentGuid = Network.player.guid;
			int currentPort = Network.player.port;
			if (string.IsNullOrEmpty(currentGuid) || currentGuid == "0" || currentPort <= 0) return;
			if (hostGuid == null)
			{
				hostGuid = currentGuid;
				gamePort = currentPort;
				LegacyHostStability.Log("IDENTITY_BOUND guid=" + LegacyHostStability.Clean(hostGuid) + " port=" + gamePort);
				return;
			}
			if (hostGuid != currentGuid || gamePort != currentPort)
			{
				string previous = hostGuid;
				hostGuid = currentGuid;
				gamePort = currentPort;
				policy.IdentityChanged(Elapsed());
				LegacyHostStability.Log("IDENTITY_CHANGED oldGuid=" + LegacyHostStability.Clean(previous)
					+ " newGuid=" + LegacyHostStability.Clean(hostGuid) + " port=" + gamePort);
			}
		}

		private bool CoreHostMatches()
		{
			return gameType != null && LegacyHostStability.IsLegacyHost()
				&& MasterServer.ipAddress == masterAddress && MasterServer.port == masterPort
				&& Network.natFacilitatorIP == facilitatorAddress && Network.natFacilitatorPort == facilitatorPort
				&& MasterServer.dedicatedServer == dedicated && HNJDDKJLHMM.JCOLMIBIGOP == gameName;
		}

		private bool SameLiveHost()
		{
			return CoreHostMatches() && hostGuid != null && Network.player.guid == hostGuid
				&& Network.player.port == gamePort;
		}

		private void OnMasterServerEvent(MasterServerEvent value)
		{
			if (LegacyHostMigration.Active) return;
			if (LegacyHostAutoPort.Current != null) return;
			if (!policy.Tracking) return;
			try
			{
				LegacyHostStability.Log("MASTER_EVENT value=" + value + " phase=" + policy.Phase);
				if (value == MasterServerEvent.RegistrationSucceeded)
				{
					policy.RegistrationAcknowledged(Elapsed());
				}
				else if (value == MasterServerEvent.HostListReceived
					&& policy.Phase == LegacyHostDirectoryPhase.WaitingList)
				{
					HostData[] hosts = MasterServer.PollHostList();
					bool found = false;
					for (int index = 0; index < hosts.Length; index++)
						if (hosts[index] != null && hosts[index].guid == hostGuid) { found = true; break; }
					policy.ListReceived(Elapsed(), found);
					LegacyHostStability.Log("LIST_REPLY ownGuidFound=" + found + " entries=" + hosts.Length
						+ " guid=" + LegacyHostStability.Clean(hostGuid));
				}
				else if (value == MasterServerEvent.RegistrationFailedGameName
					|| value == MasterServerEvent.RegistrationFailedGameType)
				{
					policy.Failure(Elapsed(), "invalid registration metadata", true);
				}
				else if (value == MasterServerEvent.RegistrationFailedNoServer)
				{
					policy.Failure(Elapsed(), "master rejected registration: no server", false);
				}
				ReportState();
			}
			catch (Exception error)
			{
				StopTracking("master callback failure: " + error.GetType().Name);
			}
		}

		private void OnFailedToConnectToMasterServer(NetworkConnectionError error)
		{
			if (LegacyHostMigration.Active) return;
			if (!policy.Tracking) return;
			policy.Failure(Elapsed(), "master connection failed: " + error, false);
			LegacyHostStability.Log("MASTER_FAILED error=" + error + " phase=" + policy.Phase);
			ReportState();
		}

		private void OnDisconnectedFromServer(NetworkDisconnection reason)
		{
			if (LegacyHostMigration.Active) return;
			if (policy.Tracking && SameLiveHost())
			{
				policy.PublicationChanged(Elapsed());
				LegacyHostStability.Log("DISCONNECT_CALLBACK_LIVE_HOST reason=" + reason
					+ " keepTracking=true sessionReinitialized=false");
				return;
			}
			if (policy.Tracking) StopTracking("Legacy transport disconnected: " + reason);
		}

		private void OnApplicationQuit()
		{
			if (policy.Tracking) StopTracking("application quit");
		}

		private void OnDestroy()
		{
			if (policy.Tracking) policy.Stop("driver destroyed");
			LegacyHostStability.DriverDestroyed(this);
		}

		private double Elapsed()
		{
			return Math.Max(0d, Time.realtimeSinceStartup - startedAt);
		}

		private void StopTracking(string reason)
		{
			policy.Stop(reason);
			LegacyHostStability.Log("STOP reason=" + LegacyHostStability.Clean(reason)
				+ " serverReinitialized=false existingPlayersDisconnected=false");
			ClearSession();
		}

		private void ClearSession()
		{
			facilitatorPolicy.Reset("host session cleared");
			LegacyHostFacilitatorReconnectFix.ClearPending();
			gameType = null;
			gameName = null;
			comment = null;
			liveSession = null;
			publishedAddresses = new string[0];
			addressReadFailed = false;
			hostGuid = null;
			masterAddress = null;
			facilitatorAddress = null;
			masterPort = 0;
			facilitatorPort = 0;
			gamePort = 0;
			lastState = null;
		}

		private string StateText()
		{
			return "phase=" + policy.Phase + " evidence=" + policy.Evidence
				+ " failures=" + policy.Failures + " reason=" + LegacyHostStability.Clean(policy.Reason)
				+ " facilitatorPending=" + facilitatorPolicy.Pending
				+ " facilitatorAttempts=" + facilitatorPolicy.Attempts;
		}

		private void ReportState()
		{
			string current = StateText();
			if (current == lastState) return;
			lastState = current;
			LegacyHostStability.Log("STATE " + current);
		}
	}
}
