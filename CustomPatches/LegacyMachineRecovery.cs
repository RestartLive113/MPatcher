using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using HarmonyLib;
using McnCraft;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
	// A reconnecting Legacy client owns a new NetworkView. The old native object
	// cannot be transferred, so the owner periodically deposits an authenticated
	// machine snapshot on the host. The host applies it only to a new view owned
	// by the same installation and carrying the same construction fingerprint.
	internal static class LegacyMachineRecovery
	{
		private const string PatchId = "mpatcher.legacy-machine-recovery.v1";
		private const float RetentionSeconds = 300f;
		private const int StatusCapture = 1;
		private const int StatusReject = 2;
		private static readonly Dictionary<string, RecoveryRecord> records =
			new Dictionary<string, RecoveryRecord>(StringComparer.Ordinal);
		private static Harmony harmony;
		private static int nextGeneration = 1;
		private static float nextCleanup;

		internal static void TryRegister()
		{
			if (harmony != null) return;
			Harmony candidate = new Harmony(PatchId);
			try
			{
				Patch(candidate, typeof(MachineController), "Awake", "MachineAwakePostfix", Type.EmptyTypes, true);
				Patch(candidate, typeof(MachineController), "Initialize", "MachineInitializePostfix",
					new Type[] { typeof(string), typeof(BuildData), typeof(AssignData), typeof(Vector3) }, true);
				Patch(candidate, typeof(Game), "Start", "GameStartPostfix", Type.EmptyTypes, true);
				Patch(candidate, typeof(Game), "OnDestroy", "GameDestroyPrefix", Type.EmptyTypes, false);
				Patch(candidate, typeof(Game), "OnPlayerDisconnected", "PlayerDisconnectedPrefix",
					new Type[] { typeof(NetworkPlayer) }, false);
				harmony = candidate;
				Log("REGISTERED version=1 transport=Unity-Legacy retentionSeconds=300 ownerSnapshot=true constructionFingerprint=true serverCloneRequiresConstructionData=false nativeOwnershipTransfer=false");
			}
			catch (Exception error)
			{
				candidate.UnpatchAll(PatchId);
				Log("REGISTER_FAILED type=" + error.GetType().Name + " message=" + Clean(error.Message));
			}
		}

		private static void Patch(Harmony candidate, Type type, string methodName, string hookName,
			Type[] parameters, bool postfix)
		{
			MethodInfo target = AccessTools.Method(type, methodName, parameters);
			MethodInfo hook = AccessTools.Method(typeof(LegacyMachineRecovery), hookName);
			if (target == null) throw new MissingMethodException(type.FullName, methodName);
			if (hook == null) throw new MissingMethodException(typeof(LegacyMachineRecovery).FullName, hookName);
			if (postfix) candidate.Patch(target, null, new HarmonyMethod(hook), null);
			else candidate.Patch(target, new HarmonyMethod(hook), null, null);
		}

		private static void MachineAwakePostfix(MachineController __instance)
		{
			Attach(__instance);
		}

		private static void MachineInitializePostfix(MachineController __instance)
		{
			LegacyMachineRecoveryAgent agent = Attach(__instance);
			if (agent != null) agent.NotifyInitialized();
		}

		private static LegacyMachineRecoveryAgent Attach(MachineController machine)
		{
			if (machine == null || !IsLegacy()) return null;
			try
			{
				LegacyMachineRecoveryAgent agent = machine.GetComponent<LegacyMachineRecoveryAgent>();
				if (agent == null) agent = machine.gameObject.AddComponent<LegacyMachineRecoveryAgent>();
				return agent;
			}
			catch (Exception error)
			{
				Log("ATTACH_FAILED type=" + error.GetType().Name + " message=" + Clean(error.Message));
				return null;
			}
		}

		private static void GameStartPostfix()
		{
			if (!IsLegacy() || !Network.isServer) return;
			records.Clear();
			nextCleanup = Time.realtimeSinceStartup + 5f;
			Log("SESSION_START records=0");
		}

		private static void GameDestroyPrefix()
		{
			if (records.Count == 0) return;
			Log("SESSION_END records=" + records.Count);
			records.Clear();
		}

		private static void PlayerDisconnectedPrefix(NetworkPlayer __0)
		{
			if (!IsLegacy() || !Network.isServer) return;
			CleanupExpired();
			foreach (KeyValuePair<string, RecoveryRecord> item in records)
			{
				RecoveryRecord record = item.Value;
				if (!record.Active || record.Player != __0) continue;
				if (record.Snapshot != null && record.Agent != null)
				{
					string refreshReason;
					if (record.Agent.RefreshMotion(record.Snapshot, out refreshReason))
					{
						byte[] refreshed;
						if (LegacyMachineRecoverySnapshotCodec.TryEncode(record.Snapshot, out refreshed, out refreshReason))
							record.Payload = refreshed;
					}
					else
					{
						Log("DISCONNECT_REFRESH_SKIPPED token=" + TokenLabel(item.Key)
							+ " reason=" + Clean(refreshReason));
					}
				}
				record.Active = false;
				record.Agent = null;
				record.AwaitingAck = false;
				record.RetainUntil = Time.realtimeSinceStartup + RetentionSeconds;
				Log("RETAINED token=" + TokenLabel(item.Key) + " generation=" + record.Generation
					+ " fingerprint=" + record.Fingerprint + " bodies=" + record.BodyCount
					+ " snapshot=" + (record.Snapshot != null) + " retentionSeconds=300");
			}
		}

		internal static void HandleHello(LegacyMachineRecoveryAgent agent, int protocol, string token,
			string fingerprint, int bodyCount, NetworkMessageInfo info)
		{
			if (!Network.isServer || !ValidateOwner(agent, info.sender)) return;
			CleanupExpired();
			if (protocol != LegacyMachineRecoverySnapshotCodec.ProtocolVersion || !ValidToken(token)
				|| !ValidFingerprint(fingerprint) || bodyCount <= 0
				|| bodyCount > LegacyMachineRecoverySnapshotCodec.MaximumBodies)
			{
				Log("HELLO_REJECTED token=" + TokenLabel(token) + " reason=invalid-metadata");
				agent.SendStatus(info.sender, token, 1, StatusReject, "invalid-metadata");
				return;
			}
			RecoveryRecord record;
			if (!records.TryGetValue(token, out record))
			{
				record = NewRecord(fingerprint, bodyCount, info.sender, agent);
				records.Add(token, record);
				Log("CAPTURE_BEGIN token=" + TokenLabel(token) + " generation=" + record.Generation
					+ " fingerprint=" + fingerprint + " bodies=" + bodyCount + " reason=first-join");
				agent.SendStatus(info.sender, token, record.Generation, StatusCapture, "first-join");
				return;
			}

			if (record.Active)
			{
				if (record.Player != info.sender)
				{
					Log("HELLO_REJECTED token=" + TokenLabel(token) + " reason=token-already-active");
					agent.SendStatus(info.sender, token, record.Generation, StatusReject, "token-already-active");
					return;
				}
				if (!ReferenceEquals(record.Agent, agent) || record.Fingerprint != fingerprint
					|| record.BodyCount != bodyCount)
				{
					ResetForActiveMachine(record, fingerprint, bodyCount, info.sender, agent);
					Log("CAPTURE_BEGIN token=" + TokenLabel(token) + " generation=" + record.Generation
						+ " fingerprint=" + fingerprint + " bodies=" + bodyCount + " reason=machine-replaced");
					agent.SendStatus(info.sender, token, record.Generation, StatusCapture, "machine-replaced");
					return;
				}
				if (record.AwaitingAck && record.Payload != null)
				{
					agent.SendApply(info.sender, token, record.Generation, fingerprint, record.Payload);
					return;
				}
				agent.SendStatus(info.sender, token, record.Generation, StatusCapture, "already-active");
				return;
			}

			bool compatible = Time.realtimeSinceStartup <= record.RetainUntil
				&& record.Payload != null && record.Snapshot != null
				&& record.Fingerprint == fingerprint && record.BodyCount == bodyCount;
			if (!compatible)
			{
				string reason = record.Fingerprint != fingerprint || record.BodyCount != bodyCount
					? "construction-mismatch" : "snapshot-unavailable";
				ResetForActiveMachine(record, fingerprint, bodyCount, info.sender, agent);
				Log("RESTORE_SKIPPED token=" + TokenLabel(token) + " generation=" + record.Generation
					+ " reason=" + reason + " captureFresh=true");
				agent.SendStatus(info.sender, token, record.Generation, StatusCapture, reason);
				return;
			}

			record.Active = true;
			record.Player = info.sender;
			record.Agent = agent;
			record.Generation = NextGeneration();
			record.Snapshot.Generation = record.Generation;
			string encodeReason;
			if (!LegacyMachineRecoverySnapshotCodec.TryEncode(record.Snapshot, out record.Payload, out encodeReason))
			{
				ResetForActiveMachine(record, fingerprint, bodyCount, info.sender, agent);
				Log("RESTORE_SKIPPED token=" + TokenLabel(token) + " generation=" + record.Generation
					+ " reason=" + Clean(encodeReason) + " captureFresh=true");
				agent.SendStatus(info.sender, token, record.Generation, StatusCapture, "snapshot-invalid");
				return;
			}
			record.AwaitingAck = true;
			record.RetainUntil = 0f;
			Log("RESTORE_SENT token=" + TokenLabel(token) + " generation=" + record.Generation
				+ " fingerprint=" + fingerprint + " bodies=" + bodyCount + " bytes=" + record.Payload.Length);
			agent.ApplyServerReplica(record.Generation, fingerprint, record.Payload);
			agent.SendReplica(record.Generation, fingerprint, record.Payload);
			agent.SendApply(info.sender, token, record.Generation, fingerprint, record.Payload);
		}

		internal static void HandleState(LegacyMachineRecoveryAgent agent, int protocol, string token,
			int generation, byte[] payload, NetworkMessageInfo info)
		{
			if (!Network.isServer || !ValidateOwner(agent, info.sender) || !ValidToken(token)
				|| protocol != LegacyMachineRecoverySnapshotCodec.ProtocolVersion) return;
			CleanupExpired();
			RecoveryRecord record;
			if (!records.TryGetValue(token, out record) || !record.Active || record.Player != info.sender
				|| !ReferenceEquals(record.Agent, agent) || record.Generation != generation || record.AwaitingAck)
				return;

			LegacyMachineRecoverySnapshot snapshot;
			string reason;
			if (!LegacyMachineRecoverySnapshotCodec.TryDecode(payload, out snapshot, out reason)
				|| snapshot.Generation != generation || snapshot.Fingerprint != record.Fingerprint
				|| snapshot.Bodies.Length != record.BodyCount || snapshot.Sequence <= record.LastSequence)
			{
				if (Time.realtimeSinceStartup >= record.NextRejectLog)
				{
					record.NextRejectLog = Time.realtimeSinceStartup + 10f;
					Log("SNAPSHOT_REJECTED token=" + TokenLabel(token) + " generation=" + generation
						+ " reason=" + Clean(reason ?? "metadata-or-sequence"));
				}
				return;
			}
			record.Snapshot = snapshot;
			record.Payload = payload;
			record.LastSequence = snapshot.Sequence;
			if (!record.HasSnapshotLog || Time.realtimeSinceStartup >= record.NextSnapshotLog)
			{
				record.HasSnapshotLog = true;
				record.NextSnapshotLog = Time.realtimeSinceStartup + 30f;
				Log("SNAPSHOT_STORED token=" + TokenLabel(token) + " generation=" + generation
					+ " sequence=" + snapshot.Sequence + " bodies=" + snapshot.Bodies.Length
					+ " bytes=" + payload.Length);
			}
		}

		internal static void HandleAck(LegacyMachineRecoveryAgent agent, int protocol, string token,
			int generation, int success, string reason, NetworkMessageInfo info)
		{
			if (!Network.isServer || !ValidateOwner(agent, info.sender) || !ValidToken(token)
				|| protocol != LegacyMachineRecoverySnapshotCodec.ProtocolVersion) return;
			RecoveryRecord record;
			if (!records.TryGetValue(token, out record) || !record.Active || record.Player != info.sender
				|| !ReferenceEquals(record.Agent, agent) || record.Generation != generation || !record.AwaitingAck)
				return;
			record.AwaitingAck = false;
			if (success == 1)
			{
				Log("RESTORE_ACK token=" + TokenLabel(token) + " generation=" + generation
					+ " result=applied reason=" + Clean(reason));
			}
			else
			{
				record.Snapshot = null;
				record.Payload = null;
				record.LastSequence = -1;
				Log("RESTORE_ACK token=" + TokenLabel(token) + " generation=" + generation
					+ " result=failed reason=" + Clean(reason) + " captureFresh=true");
			}
			agent.SendStatus(info.sender, token, generation, StatusCapture,
				success == 1 ? "restore-complete" : "restore-failed");
		}

		private static RecoveryRecord NewRecord(string fingerprint, int bodyCount,
			NetworkPlayer player, LegacyMachineRecoveryAgent agent)
		{
			RecoveryRecord record = new RecoveryRecord();
			ResetForActiveMachine(record, fingerprint, bodyCount, player, agent);
			return record;
		}

		private static void ResetForActiveMachine(RecoveryRecord record, string fingerprint, int bodyCount,
			NetworkPlayer player, LegacyMachineRecoveryAgent agent)
		{
			record.Fingerprint = fingerprint;
			record.BodyCount = bodyCount;
			record.Player = player;
			record.Agent = agent;
			record.Active = true;
			record.AwaitingAck = false;
			record.Generation = NextGeneration();
			record.Snapshot = null;
			record.Payload = null;
			record.LastSequence = -1;
			record.RetainUntil = 0f;
			record.HasSnapshotLog = false;
			record.NextSnapshotLog = 0f;
			record.NextRejectLog = 0f;
		}

		private static int NextGeneration()
		{
			if (nextGeneration == int.MaxValue) nextGeneration = 1;
			return nextGeneration++;
		}

		private static bool ValidateOwner(LegacyMachineRecoveryAgent agent, NetworkPlayer sender)
		{
			try
			{
				NetworkView view = agent == null ? null : agent.View;
				return view != null && view.owner == sender;
			}
			catch { return false; }
		}

		private static void CleanupExpired()
		{
			float now = Time.realtimeSinceStartup;
			if (now < nextCleanup) return;
			nextCleanup = now + 5f;
			List<string> expired = null;
			foreach (KeyValuePair<string, RecoveryRecord> item in records)
			{
				if (!item.Value.Active && now > item.Value.RetainUntil)
				{
					if (expired == null) expired = new List<string>();
					expired.Add(item.Key);
				}
			}
			if (expired == null) return;
			for (int i = 0; i < expired.Count; i++)
			{
				Log("EXPIRED token=" + TokenLabel(expired[i]));
				records.Remove(expired[i]);
			}
		}

		internal static bool IsLegacy()
		{
			try { return HNJDDKJLHMM.FHLGOMHPDLN == HNJDDKJLHMM.HKGAACMIPIH.Legacy; }
			catch { return false; }
		}

		internal static bool ValidToken(string token)
		{
			if (token == null || token.Length != 32) return false;
			for (int i = 0; i < token.Length; i++)
			{
				char value = token[i];
				if (!((value >= '0' && value <= '9') || (value >= 'a' && value <= 'f')
					|| (value >= 'A' && value <= 'F'))) return false;
			}
			return true;
		}

		private static bool ValidFingerprint(string fingerprint)
		{
			if (fingerprint == null || fingerprint.Length != 16) return false;
			for (int i = 0; i < fingerprint.Length; i++)
			{
				char value = fingerprint[i];
				if (!((value >= '0' && value <= '9') || (value >= 'A' && value <= 'F'))) return false;
			}
			return true;
		}

		internal static string TokenLabel(string token)
		{
			if (string.IsNullOrEmpty(token)) return "invalid";
			unchecked
			{
				uint hash = 2166136261u;
				for (int i = 0; i < token.Length; i++) { hash ^= token[i]; hash *= 16777619u; }
				return hash.ToString("X8", CultureInfo.InvariantCulture);
			}
		}

		internal static string Clean(string value)
		{
			return (value ?? "").Replace("\r", " ").Replace("\n", " ");
		}

		internal static void Log(string message)
		{
			try { mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng("[LEGACY-RECOVERY] " + message); }
			catch { }
		}

		private sealed class RecoveryRecord
		{
			internal string Fingerprint;
			internal int BodyCount;
			internal NetworkPlayer Player;
			internal LegacyMachineRecoveryAgent Agent;
			internal bool Active;
			internal bool AwaitingAck;
			internal int Generation;
			internal LegacyMachineRecoverySnapshot Snapshot;
			internal byte[] Payload;
			internal long LastSequence = -1;
			internal float RetainUntil;
			internal bool HasSnapshotLog;
			internal float NextSnapshotLog;
			internal float NextRejectLog;
		}
	}

	internal sealed class LegacyMachineRecoveryAgent : MonoBehaviour
	{
		private const float SnapshotInterval = 1f;
		private const float HelloInterval = 3f;
		private const string TokenDirectory = "_mpatcher";
		private const string TokenFile = "legacy-recovery-token.txt";
		private static string cachedToken;
		private MachineController machine;
		private NetworkView view;
		private string fingerprint;
		private int bodyCount;
		private int generation;
		private long sequence;
		private float nextHello;
		private float nextSnapshot;
		private float nextReadyLog;
		private float nextCaptureErrorLog;
		private bool ready;
		private bool helloLogged;
		private bool captureEnabled;
		private bool applyInProgress;
		private int applyingGeneration;
		private int lastAppliedGeneration;

		internal NetworkView View { get { return view; } }

		private void Awake()
		{
			machine = GetComponent<MachineController>();
			view = GetComponent<NetworkView>();
			nextHello = Time.realtimeSinceStartup + 0.25f;
		}

		internal void NotifyInitialized()
		{
			ready = false;
			fingerprint = null;
			bodyCount = 0;
			captureEnabled = false;
			helloLogged = false;
			sequence = 0;
			nextHello = Time.realtimeSinceStartup + 0.25f;
		}

		private void Update()
		{
			if (!LegacyMachineRecovery.IsLegacy()) return;
			if (machine == null) machine = GetComponent<MachineController>();
			if (view == null) view = GetComponent<NetworkView>();
			if (machine == null || view == null || !view.isMine || !Network.isClient) return;
			string reason;
			if (!EnsureOwnerReady(out reason))
			{
				if (Time.realtimeSinceStartup >= nextReadyLog)
				{
					nextReadyLog = Time.realtimeSinceStartup + 10f;
					LegacyMachineRecovery.Log("CLIENT_WAIT_READY reason=" + LegacyMachineRecovery.Clean(reason));
				}
				return;
			}
			string token = GetToken();
			if (!LegacyMachineRecovery.ValidToken(token)) return;
			float now = Time.realtimeSinceStartup;
			if (!captureEnabled && !applyInProgress && now >= nextHello)
			{
				nextHello = now + HelloInterval;
				view.RPC("MPatcherRecoveryHelloV1", RPCMode.Server,
					LegacyMachineRecoverySnapshotCodec.ProtocolVersion, token, fingerprint, bodyCount);
				if (!helloLogged)
				{
					helloLogged = true;
					LegacyMachineRecovery.Log("CLIENT_HELLO token=" + LegacyMachineRecovery.TokenLabel(token)
						+ " fingerprint=" + fingerprint + " bodies=" + bodyCount);
				}
			}
			if (!captureEnabled || applyInProgress || now < nextSnapshot) return;
			nextSnapshot = now + SnapshotInterval;
			LegacyMachineRecoverySnapshot snapshot;
			if (!TryCapture(generation, ++sequence, out snapshot, out reason))
			{
				if (now >= nextCaptureErrorLog)
				{
					nextCaptureErrorLog = now + 10f;
					LegacyMachineRecovery.Log("CLIENT_CAPTURE_FAILED generation=" + generation
						+ " reason=" + LegacyMachineRecovery.Clean(reason));
				}
				return;
			}
			byte[] payload;
			if (!LegacyMachineRecoverySnapshotCodec.TryEncode(snapshot, out payload, out reason))
			{
				if (now >= nextCaptureErrorLog)
				{
					nextCaptureErrorLog = now + 10f;
					LegacyMachineRecovery.Log("CLIENT_ENCODE_FAILED generation=" + generation
						+ " reason=" + LegacyMachineRecovery.Clean(reason));
				}
				return;
			}
			view.RPC("MPatcherRecoveryStateV1", RPCMode.Server,
				LegacyMachineRecoverySnapshotCodec.ProtocolVersion, token, generation, payload);
		}

		[RPC]
		private void MPatcherRecoveryHelloV1(int protocol, string token, string construction,
			int bodies, NetworkMessageInfo info)
		{
			LegacyMachineRecovery.HandleHello(this, protocol, token, construction, bodies, info);
		}

		[RPC]
		private void MPatcherRecoveryStateV1(int protocol, string token, int incomingGeneration,
			byte[] payload, NetworkMessageInfo info)
		{
			LegacyMachineRecovery.HandleState(this, protocol, token, incomingGeneration, payload, info);
		}

		[RPC]
		private void MPatcherRecoveryAckV1(int protocol, string token, int incomingGeneration,
			int success, string reason, NetworkMessageInfo info)
		{
			LegacyMachineRecovery.HandleAck(this, protocol, token, incomingGeneration, success, reason, info);
		}

		[RPC]
		private void MPatcherRecoveryStatusV1(int protocol, string token, int incomingGeneration,
			int status, string reason)
		{
			if (protocol != LegacyMachineRecoverySnapshotCodec.ProtocolVersion || token != GetToken()
				|| incomingGeneration <= 0 || view == null || !view.isMine || !Network.isClient) return;
			if (status == 1)
			{
				generation = incomingGeneration;
				sequence = 0;
				captureEnabled = true;
				applyInProgress = false;
				nextSnapshot = Time.realtimeSinceStartup + 0.1f;
				LegacyMachineRecovery.Log("CLIENT_CAPTURE_ENABLED token=" + LegacyMachineRecovery.TokenLabel(token)
					+ " generation=" + generation + " reason=" + LegacyMachineRecovery.Clean(reason));
			}
			else if (status == 2)
			{
				captureEnabled = false;
				nextHello = float.MaxValue;
				LegacyMachineRecovery.Log("CLIENT_REJECTED token=" + LegacyMachineRecovery.TokenLabel(token)
					+ " reason=" + LegacyMachineRecovery.Clean(reason));
			}
		}

		[RPC]
		private void MPatcherRecoveryApplyV1(int protocol, string token, int incomingGeneration,
			string construction, byte[] payload)
		{
			if (protocol != LegacyMachineRecoverySnapshotCodec.ProtocolVersion || token != GetToken()
				|| incomingGeneration <= 0 || view == null || !view.isMine || !Network.isClient) return;
			if (lastAppliedGeneration == incomingGeneration)
			{
				SendAck(token, incomingGeneration, true, "already-applied");
				return;
			}
			if (applyInProgress && applyingGeneration == incomingGeneration) return;
			captureEnabled = false;
			applyInProgress = true;
			applyingGeneration = incomingGeneration;
			StartCoroutine(ApplySnapshot(incomingGeneration, construction, payload, true, token));
		}

		[RPC]
		private void MPatcherRecoveryReplicaV1(int protocol, int incomingGeneration,
			string construction, byte[] payload)
		{
			if (protocol != LegacyMachineRecoverySnapshotCodec.ProtocolVersion || incomingGeneration <= 0
				|| view == null || view.isMine || !Network.isClient || lastAppliedGeneration == incomingGeneration) return;
			if (applyInProgress && applyingGeneration == incomingGeneration) return;
			applyInProgress = true;
			applyingGeneration = incomingGeneration;
			StartCoroutine(ApplySnapshot(incomingGeneration, construction, payload, false, null));
		}

		internal void SendStatus(NetworkPlayer target, string token, int outgoingGeneration,
			int status, string reason)
		{
			if (view == null) return;
			view.RPC("MPatcherRecoveryStatusV1", target,
				LegacyMachineRecoverySnapshotCodec.ProtocolVersion, token, outgoingGeneration, status, reason);
		}

		internal void SendApply(NetworkPlayer target, string token, int outgoingGeneration,
			string construction, byte[] payload)
		{
			if (view == null) return;
			view.RPC("MPatcherRecoveryApplyV1", target,
				LegacyMachineRecoverySnapshotCodec.ProtocolVersion, token, outgoingGeneration, construction, payload);
		}

		internal void SendReplica(int outgoingGeneration, string construction, byte[] payload)
		{
			if (view == null) return;
			view.RPC("MPatcherRecoveryReplicaV1", RPCMode.Others,
				LegacyMachineRecoverySnapshotCodec.ProtocolVersion, outgoingGeneration, construction, payload);
		}

		internal void ApplyServerReplica(int outgoingGeneration, string construction, byte[] payload)
		{
			if (!Network.isServer || applyInProgress) return;
			applyInProgress = true;
			applyingGeneration = outgoingGeneration;
			StartCoroutine(ApplySnapshot(outgoingGeneration, construction, payload, false, null));
		}

		internal bool RefreshMotion(LegacyMachineRecoverySnapshot snapshot, out string reason)
		{
			reason = null;
			if (snapshot == null || snapshot.Bodies == null || machine == null
				|| machine.ILBAAENKMBL == null || machine.ILBAAENKMBL.Count != snapshot.Bodies.Length)
			{
				reason = "body-count-mismatch";
				return false;
			}
			for (int i = 0; i < snapshot.Bodies.Length; i++)
			{
				BodyController body = machine.ILBAAENKMBL[i];
				Rigidbody rigidbody = body == null ? null : body.NFMPBACKJOJ;
				if (rigidbody == null) { reason = "body-not-ready-" + i; return false; }
				LegacyMachineRecoveryBodyState state = snapshot.Bodies[i];
				CopyMotion(rigidbody, ref state);
				snapshot.Bodies[i] = state;
			}
			return true;
		}

		private bool EnsureOwnerReady(out string reason)
		{
			reason = null;
			if (ready && machine != null && machine.ILBAAENKMBL != null
				&& machine.ILBAAENKMBL.Count == bodyCount) return true;
			if (machine == null)
			{
				reason = "owner-machine-null";
				return false;
			}
			if (machine.HHGILAIOCLG == null || machine.MIIGKEBFKKD == null)
			{
				reason = "owner-construction-not-ready";
				return false;
			}
			if (machine.ILBAAENKMBL == null || machine.KBLANAFAJFP == null
				|| machine.ILBAAENKMBL.Count <= 0 || machine.ILBAAENKMBL.Count != machine.KBLANAFAJFP.Count)
			{
				reason = "owner-body-lists-not-ready";
				return false;
			}
			for (int i = 0; i < machine.ILBAAENKMBL.Count; i++)
			{
				if (machine.ILBAAENKMBL[i] == null || machine.ILBAAENKMBL[i].NFMPBACKJOJ == null)
				{
					reason = "body-not-ready-" + i;
					return false;
				}
			}
			if (!LegacyMachineRecoveryFingerprint.TryCompute(machine, out fingerprint, out reason)) return false;
			bodyCount = machine.ILBAAENKMBL.Count;
			ready = true;
			return true;
		}

		private bool EnsurePhysicalReady(int expectedBodies, out string reason)
		{
			reason = null;
			if (machine == null)
			{
				reason = "replica-machine-null";
				return false;
			}
			if (machine.ILBAAENKMBL == null || machine.KBLANAFAJFP == null)
			{
				reason = "replica-body-lists-null";
				return false;
			}
			if (expectedBodies <= 0 || machine.ILBAAENKMBL.Count != expectedBodies
				|| machine.KBLANAFAJFP.Count != expectedBodies)
			{
				reason = "replica-body-count-" + machine.ILBAAENKMBL.Count + "-roots-"
					+ machine.KBLANAFAJFP.Count + "-expected-" + expectedBodies;
				return false;
			}
			for (int i = 0; i < expectedBodies; i++)
			{
				if (machine.ILBAAENKMBL[i] == null || machine.ILBAAENKMBL[i].NFMPBACKJOJ == null
					|| machine.KBLANAFAJFP[i] == null)
				{
					reason = "replica-body-not-ready-" + i;
					return false;
				}
			}
			bodyCount = expectedBodies;
			return true;
		}

		private bool TryCapture(int outgoingGeneration, long outgoingSequence,
			out LegacyMachineRecoverySnapshot snapshot, out string reason)
		{
			snapshot = null;
			if (!EnsureOwnerReady(out reason)) return false;
			LegacyMachineRecoveryBodyState[] bodies = new LegacyMachineRecoveryBodyState[bodyCount];
			for (int i = 0; i < bodies.Length; i++)
			{
				BodyController body = machine.ILBAAENKMBL[i];
				Rigidbody rigidbody = body.NFMPBACKJOJ;
				LegacyMachineRecoveryBodyState state = new LegacyMachineRecoveryBodyState();
				CopyMotion(rigidbody, ref state);
				state.Health = body.MEJNIODBGFI;
				state.BonusHealth = body.HPOMNJCEJIP;
				state.Broken = body.IsBroken();
				state.Suspended = body.EFCBCPOCOBB;
				state.IsKinematic = rigidbody.isKinematic;
				state.SuspendFrames = Math.Max(0, body.OKGLHFEAEEP);
				bodies[i] = state;
			}
			snapshot = new LegacyMachineRecoverySnapshot();
			snapshot.Generation = outgoingGeneration;
			snapshot.Sequence = outgoingSequence;
			snapshot.CapturedAt = Time.realtimeSinceStartup;
			snapshot.Fingerprint = fingerprint;
			snapshot.Bodies = bodies;
			return true;
		}

		private IEnumerator ApplySnapshot(int incomingGeneration, string construction,
			byte[] payload, bool acknowledge, string token)
		{
			string reason = null;
			LegacyMachineRecoverySnapshot snapshot;
			if (!LegacyMachineRecoverySnapshotCodec.TryDecode(payload, out snapshot, out reason)
				|| snapshot.Generation != incomingGeneration || snapshot.Fingerprint != construction)
			{
				FinishApply(incomingGeneration, acknowledge, token, false, reason ?? "snapshot-metadata-mismatch");
				yield break;
			}

			float deadline = Time.realtimeSinceStartup + 10f;
			bool applyReady = false;
			while (Time.realtimeSinceStartup < deadline)
			{
				if (acknowledge)
				{
					applyReady = EnsureOwnerReady(out reason);
					if (applyReady && construction != fingerprint)
					{
						applyReady = false;
						reason = "owner-construction-mismatch";
					}
				}
				else
				{
					applyReady = EnsurePhysicalReady(snapshot.Bodies.Length, out reason);
				}
				if (applyReady) break;
				yield return null;
			}
			if (!applyReady || snapshot.Bodies.Length != bodyCount)
			{
				FinishApply(incomingGeneration, acknowledge, token, false, reason ?? "machine-not-ready");
				yield break;
			}

			Rigidbody[] rigidbodies = new Rigidbody[bodyCount];
			bool[] originalKinematic = new bool[bodyCount];
			List<GameObject> brokenRoots = new List<GameObject>();
			for (int i = 0; i < bodyCount; i++)
			{
				BodyController body = machine.ILBAAENKMBL[i];
				Rigidbody rigidbody = body == null ? null : body.NFMPBACKJOJ;
				if (rigidbody == null)
				{
					FinishApply(incomingGeneration, acknowledge, token, false, "body-lost-" + i);
					yield break;
				}
				rigidbodies[i] = rigidbody;
				originalKinematic[i] = rigidbody.isKinematic;
				if (snapshot.Bodies[i].Broken || snapshot.Bodies[i].Suspended)
					brokenRoots.Add(machine.KBLANAFAJFP[i]);
			}
			try
			{
				for (int i = 0; i < rigidbodies.Length; i++)
				{
					rigidbodies[i].velocity = Vector3.zero;
					rigidbodies[i].angularVelocity = Vector3.zero;
					rigidbodies[i].isKinematic = true;
				}
				if (machine.IBAOOHNFBCO != null)
				{
					for (int i = machine.IBAOOHNFBCO.Count - 1; i >= 0; i--)
					{
						JointController joint = machine.IBAOOHNFBCO[i];
						if (joint != null && (brokenRoots.Contains(joint.COKMFLEAMEK)
							|| brokenRoots.Contains(joint.NMBGGPPICME))) joint.BreakJoint();
					}
				}
			}
			catch (Exception error)
			{
				ReleaseFrozen(rigidbodies, originalKinematic);
				FinishApply(incomingGeneration, acknowledge, token, false,
					"freeze-or-joint-" + error.GetType().Name);
				yield break;
			}
			yield return null;
			if (machine == null || machine.ILBAAENKMBL == null || machine.ILBAAENKMBL.Count != bodyCount)
			{
				ReleaseFrozen(rigidbodies, originalKinematic);
				FinishApply(incomingGeneration, acknowledge, token, false, "machine-lost-during-apply");
				yield break;
			}
			try
			{
				for (int i = 0; i < bodyCount; i++)
				{
					BodyController body = machine.ILBAAENKMBL[i];
					LegacyMachineRecoveryBodyState state = snapshot.Bodies[i];
					body.MEJNIODBGFI = state.Health;
					body.HPOMNJCEJIP = state.BonusHealth;
					body.BCPNCJMHCAA = false;
					body.OKGLHFEAEEP = state.SuspendFrames;
					if (state.Suspended && !body.EFCBCPOCOBB) body.Suspend();
					Rigidbody rigidbody = rigidbodies[i];
					Vector3 position = new Vector3(state.PositionX, state.PositionY, state.PositionZ);
					Quaternion rotation = new Quaternion(state.RotationX, state.RotationY, state.RotationZ, state.RotationW);
					rigidbody.position = position;
					rigidbody.rotation = rotation;
					body.transform.position = position;
					body.transform.rotation = rotation;
					rigidbody.isKinematic = state.IsKinematic;
					rigidbody.velocity = new Vector3(state.VelocityX, state.VelocityY, state.VelocityZ);
					rigidbody.angularVelocity = new Vector3(state.AngularX, state.AngularY, state.AngularZ);
				}
				machine.UpdateHealthEnergy();
				machine.SelectBiggestBody();
			}
			catch (Exception error)
			{
				ReleaseFrozen(rigidbodies, originalKinematic);
				FinishApply(incomingGeneration, acknowledge, token, false,
					"state-apply-" + error.GetType().Name);
				yield break;
			}
			lastAppliedGeneration = incomingGeneration;
			FinishApply(incomingGeneration, acknowledge, token, true, "position-rotation-damage-restored");
		}

		private static void ReleaseFrozen(Rigidbody[] rigidbodies, bool[] originalKinematic)
		{
			for (int i = 0; i < rigidbodies.Length; i++)
			{
				try
				{
					if (rigidbodies[i] != null) rigidbodies[i].isKinematic = originalKinematic[i];
				}
				catch { }
			}
		}

		private void FinishApply(int incomingGeneration, bool acknowledge, string token,
			bool success, string reason)
		{
			applyInProgress = false;
			applyingGeneration = 0;
			LegacyMachineRecovery.Log((success ? "APPLY_COMPLETE" : "APPLY_FAILED")
				+ " generation=" + incomingGeneration + " owner=" + (view != null && view.isMine)
				+ " bodies=" + bodyCount + " reason=" + LegacyMachineRecovery.Clean(reason));
			if (acknowledge) SendAck(token, incomingGeneration, success, reason);
		}

		private void SendAck(string token, int outgoingGeneration, bool success, string reason)
		{
			if (view == null || !LegacyMachineRecovery.ValidToken(token)) return;
			view.RPC("MPatcherRecoveryAckV1", RPCMode.Server,
				LegacyMachineRecoverySnapshotCodec.ProtocolVersion, token, outgoingGeneration,
				success ? 1 : 0, reason);
		}

		private static void CopyMotion(Rigidbody rigidbody, ref LegacyMachineRecoveryBodyState state)
		{
			Vector3 position = rigidbody.position;
			Quaternion rotation = rigidbody.rotation;
			Vector3 velocity = rigidbody.velocity;
			Vector3 angular = rigidbody.angularVelocity;
			state.PositionX = position.x; state.PositionY = position.y; state.PositionZ = position.z;
			state.RotationX = rotation.x; state.RotationY = rotation.y;
			state.RotationZ = rotation.z; state.RotationW = rotation.w;
			state.VelocityX = velocity.x; state.VelocityY = velocity.y; state.VelocityZ = velocity.z;
			state.AngularX = angular.x; state.AngularY = angular.y; state.AngularZ = angular.z;
			state.IsKinematic = rigidbody.isKinematic;
		}

		private static string GetToken()
		{
			if (LegacyMachineRecovery.ValidToken(cachedToken)) return cachedToken;
			try
			{
				string directory = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), "UserData"), TokenDirectory);
				string path = Path.Combine(directory, TokenFile);
				if (File.Exists(path))
				{
					string existing = File.ReadAllText(path).Trim();
					if (LegacyMachineRecovery.ValidToken(existing))
					{
						cachedToken = existing.ToLowerInvariant();
						return cachedToken;
					}
				}
				Directory.CreateDirectory(directory);
				cachedToken = Guid.NewGuid().ToString("N");
				File.WriteAllText(path, cachedToken);
				LegacyMachineRecovery.Log("TOKEN_CREATED token=" + LegacyMachineRecovery.TokenLabel(cachedToken)
					+ " path=UserData/_mpatcher/" + TokenFile);
				return cachedToken;
			}
			catch (Exception error)
			{
				if (!LegacyMachineRecovery.ValidToken(cachedToken)) cachedToken = Guid.NewGuid().ToString("N");
				LegacyMachineRecovery.Log("TOKEN_PERSIST_FAILED type=" + error.GetType().Name
					+ " token=" + LegacyMachineRecovery.TokenLabel(cachedToken));
				return cachedToken;
			}
		}
	}
}
