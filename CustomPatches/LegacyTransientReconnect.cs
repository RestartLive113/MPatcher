using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using McnCraft;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
	// Keeps a Legacy client in the Meeting scene after an unexpected game-link
	// loss. This first runtime stage retains and freezes the server objects,
	// reconnects to the original endpoint and probes native NetworkView ownership.
	internal static partial class LegacyTransientReconnect
	{
		private const string PatchId = "mpatcher.legacy-transient-reconnect.v40";
		private const int ProtocolVersion = 2;
		private const int CrashProtocolVersion = 3;
		private const int CrashConsentProtocolVersion = 6;
		private const int MachineNetworkGroup = 1;
		private const float RetentionSeconds = 300f;
		private const float CrashConsentSeconds = 60f;
		private static readonly Dictionary<string, ResumeRecord> records =
			new Dictionary<string, ResumeRecord>(StringComparer.Ordinal);
		private static Harmony harmony;
		private static LegacyTransientReconnectController controller;
		private static bool serverEnding;
		private static int nextGhostSnapshotId = 1;

		internal static void TryRegister()
		{
			if (harmony != null) return;
			Harmony candidate = new Harmony(PatchId);
			try
			{
				LegacyTransientReconnectController.ValidateOwnerMotionFields();
				LegacyMachineControlRecovery.ValidateBindings();
				Patch(candidate, typeof(MachineController), "Warp", "CrashWarpPrefix", "CrashWarpPostfix",
					new Type[] { typeof(Vector3), typeof(Quaternion), typeof(bool) });
				Patch(candidate, typeof(Game), "Start", null, "GameStartPostfix", Type.EmptyTypes);
				Transpile(candidate, typeof(Game), "Update", "GameUpdateTranspiler", Type.EmptyTypes);
				Patch(candidate, typeof(Game), "Exit", "GameExitPrefix", "GameExitPostfix", Type.EmptyTypes);
				Patch(candidate, typeof(Game), "OnDestroy", "GameDestroyPrefix", null, Type.EmptyTypes);
				Patch(candidate, typeof(Game), "OnDisconnectedFromServer", "GameDisconnectedPrefix", null,
					new Type[] { typeof(NetworkDisconnection) });
				Patch(candidate, typeof(Game), "OnPlayerConnected", "PlayerConnectedPrefix", null,
					new Type[] { typeof(NetworkPlayer) });
				Patch(candidate, typeof(Game), "OnPlayerDisconnected", "PlayerDisconnectedPrefix", null,
					new Type[] { typeof(NetworkPlayer) });
				Patch(candidate, typeof(Game), "RPC_SyncGuidelineE", "ReconnectGuidelinePrefix", null,
					new Type[] { typeof(string) });
				Patch(candidate, typeof(Game), "RPC_SyncGuidelineJ", "ReconnectGuidelinePrefix", null,
					new Type[] { typeof(string) });
				Patch(candidate, typeof(MachineSerializer), "RPC_SyncStructure", "MachineCatchUpPrefix", null,
					new Type[] { typeof(byte[]) });
				Patch(candidate, typeof(MachineSerializer), "RPC_SyncCollider", "MachineCatchUpPrefix", null,
					new Type[] { typeof(byte[]) });
				Patch(candidate, typeof(MachineController), "RPC_SyncStampTexture",
					"MachineControllerCatchUpPrefix", null, new Type[] { typeof(string) });
				Patch(candidate, typeof(MachineController), "RPC_SyncPlayerName",
					"MachineControllerCatchUpPrefix", "MachineControllerNamePostfix", new Type[] { typeof(string), typeof(int),
						typeof(int), typeof(Vector3), typeof(int), typeof(int) });
				Patch(candidate, typeof(MachineController), "OnDestroy", "MachineDestroyedPrefix", null,
					Type.EmptyTypes);
				Patch(candidate, typeof(MachineController), "DestroyTrash", "MachineDestroyTrashPrefix", null,
					new Type[] { typeof(List<GameObject>) });
				Patch(candidate, typeof(MachineSerializer), "OnSerializeNetworkView", "MachineSerializePrefix", null,
					new Type[] { typeof(BitStream), typeof(NetworkMessageInfo) });
				Patch(candidate, typeof(NetworkView), "RPC", "NetworkViewRpcPrefix", null,
					new Type[] { typeof(string), typeof(RPCMode), typeof(object[]) });
				Patch(candidate, typeof(NetworkView), "RPC", "NetworkViewRpcPrefix", null,
					new Type[] { typeof(string), typeof(NetworkPlayer), typeof(object[]) });
				harmony = candidate;
				Log("REGISTERED version=48 machineChangeAnchor=native-bounds-without-missile-templates restoreDelivery=idempotent-per-session-view-owner-generation spawnFinalize=await-native-ready-result retirement=original+previous+current controlReadiness=exclude-native-construction-trash ownerIdentity=controls-ready-before-hash restorePreparation=cached-snapshot+readiness-before-fingerprint manualExit=cancel+native-scene-fallback controls=owner-toggle-assign+owner-togglekeys+owner-alt-latch+part-phases+joint-targets+secondary-input-preserved checkpointPhase=LateUpdate playerList=event-driven-reference-identity-all-peers tagRetire=explicit-before-machine-destroy crashConsentInput=unity-button+raw-mouse+keyboard+auto-cursor+modal-game-input+camera-controller-suspended crashConsentUi=native-dialog-white-normal-en-ja transport=Unity-Legacy trigger=LostConnection keepScene=true localSimulation=true serverGhost=frozen retentionSeconds=300 reconnectDelaySeconds=10 reconnect=live-session-catalogue+original-target previousConnection=cleared-after-crash-restore machineGroupGate=1 ownershipRebind=claim-ready+1s-drain rebindReplay=targeted-on-hello+buffered-id lateJoinGhost=targeted-body+visual-pose+health crashResume=persistent-session-marker+state-transplant+owner-server-ack crashOwnerPose=persistent-full-body-checkpoint ownerCheckpoint=250ms-atomic crashDamage=owner-checkpoint crashRootState=intrinsic-not-detached crashMachineIdentity=owner-checkpoint+fresh-owner-preflight+fresh-owner-decision+owner-apply crashConsent=same-construction-owner-choice-before-pose remotePresentation=root-frame-first+early-host-registration+native-live-pose+lod+bound+tag+deduped-host-list ownerMotionHistory=native-warp-sentinel crashAdmissionRetry=marker-preserved crashHelloTimeout=v6-to-v3-5s+v3-to-v2-30s ghostRetire=buffered-and-live-id+tracked-network+local reboundRetire=server-authoritative reconnectGuideline=suppress-buffered applicationQuit=goodbye+200ms-drain freshJoin=replace-retained disconnectedRpc=suppress emptyPing=safe staleSession=reject recoveryTimeoutSeconds=300 cleanServerClose=native testFault=server-close");
			}
			catch (Exception error)
			{
				candidate.UnpatchAll(PatchId);
				Log("REGISTER_FAILED type=" + error.GetType().Name + " message=" + Clean(error.Message));
			}
		}

		private static void Patch(Harmony candidate, Type type, string name, string prefix,
			string postfix, Type[] parameters)
		{
			MethodInfo target = AccessTools.Method(type, name, parameters);
			if (target == null) throw new MissingMethodException(type.FullName, name);
			candidate.Patch(target, Hook(prefix), Hook(postfix), null);
		}

		private static HarmonyMethod Hook(string name)
		{
			if (name == null) return null;
			MethodInfo method = AccessTools.Method(typeof(LegacyTransientReconnect), name);
			if (method == null) throw new MissingMethodException(typeof(LegacyTransientReconnect).FullName, name);
			return new HarmonyMethod(method);
		}

		private static void Transpile(Harmony candidate, Type type, string name, string transpiler,
			Type[] parameters)
		{
			MethodInfo target = AccessTools.Method(type, name, parameters);
			if (target == null) throw new MissingMethodException(type.FullName, name);
			candidate.Patch(target, null, null, Hook(transpiler));
		}

		private static IEnumerable<CodeInstruction> GameUpdateTranspiler(IEnumerable<CodeInstruction> instructions)
		{
			List<CodeInstruction> codes = new List<CodeInstruction>();
			foreach (CodeInstruction instruction in instructions) codes.Add(new CodeInstruction(instruction));
			MethodInfo connections = AccessTools.PropertyGetter(typeof(Network), "connections");
			MethodInfo averagePing = AccessTools.Method(typeof(Network), "GetAveragePing",
				new Type[] { typeof(NetworkPlayer) });
			MethodInfo safeAveragePing = AccessTools.Method(typeof(LegacyTransientReconnect), "SafeAveragePing");
			int match = -1;
			int matches = 0;
			for (int i = 0; i + 4 < codes.Count; i++)
			{
				if (!Calls(codes[i], connections) || codes[i + 1].opcode != OpCodes.Ldc_I4_0
					|| codes[i + 2].opcode != OpCodes.Ldelema || codes[i + 2].operand != typeof(NetworkPlayer)
					|| codes[i + 3].opcode != OpCodes.Ldobj || codes[i + 3].operand != typeof(NetworkPlayer)
					|| !Calls(codes[i + 4], averagePing)) continue;
				match = i;
				matches++;
			}
			if (matches != 1) throw new InvalidOperationException("Game.Update average-ping pattern count=" + matches);
			CodeInstruction replacement = new CodeInstruction(OpCodes.Call, safeAveragePing);
			for (int i = 0; i < 5; i++)
			{
				replacement.labels.AddRange(codes[match + i].labels);
				replacement.blocks.AddRange(codes[match + i].blocks);
			}
			codes.RemoveRange(match, 5);
			codes.Insert(match, replacement);
			return codes;
		}

		private static bool Calls(CodeInstruction instruction, MethodInfo method)
		{
			return method != null && (instruction.opcode == OpCodes.Call || instruction.opcode == OpCodes.Callvirt)
				&& Equals(instruction.operand, method);
		}

		private static int SafeAveragePing()
		{
			NetworkPlayer[] connections = Network.connections;
			if (connections == null || connections.Length == 0)
			{
				if (controller != null) controller.ObserveEmptyPingGuard();
				return 0;
			}
			return Network.GetAveragePing(connections[0]);
		}

		private static void GameStartPostfix(Game __instance)
		{
			if (!IsLegacy() || __instance == null) return;
			bool clientEnabled = Network.isClient && AutoReconnect.Enabled;
			bool serverEnabled = Network.isServer && LegacyIndivFixBundle.Enabled;
			if (!clientEnabled && !serverEnabled)
			{
				Log("SESSION_BYPASS client=" + Network.isClient + " server=" + Network.isServer
					+ " autoReconnect=" + AutoReconnect.Enabled + " indivFix=" + LegacyIndivFixBundle.Enabled);
				return;
			}
			serverEnding = false;
			if (Network.isServer)
			{
				records.Clear();
				ClearMigrationCapabilities();
				nextGhostSnapshotId = 1;
			}
			controller = __instance.GetComponent<LegacyTransientReconnectController>();
			if (controller == null) controller = __instance.gameObject.AddComponent<LegacyTransientReconnectController>();
			controller.Initialize(__instance, Network.isClient, Network.isServer);
		}

		private static void GameExitPrefix(out bool __state)
		{
			LegacyHostMigration.Stop("voluntary-exit");
			__state = false;
			if (!IsLegacy()) return;
			if (Network.isServer) serverEnding = true;
			if (controller != null)
			{
				__state = controller.NeedsRecoveryExit;
				controller.NotifyVoluntaryExit();
				if (__state) controller.ArmRecoveryExit();
			}
		}

		private static void GameExitPostfix(bool __state)
		{
			if (__state && controller != null) controller.CompleteRecoveryExit();
		}

		private static void GameDestroyPrefix(Game __instance)
		{
			LegacyNativeConstructionTrash.Clear();
			if (controller != null && controller.Game == __instance) controller.NotifySceneDestroy();
			if (Network.isServer || serverEnding) records.Clear();
			controller = null;
		}

		private static bool GameDisconnectedPrefix(Game __instance, NetworkDisconnection __0)
		{
			if (LegacyHostMigration.SuppressDisconnect) return false;
			if (controller == null || controller.Game != __instance) return true;
			// Game.Exit changes the global mode to Offline before a callback can arrive.
			if (controller.ObserveRecoveryExitCallback(__0)) return true;
			if (!IsLegacy()) return true;
			if (Network.isClient && !AutoReconnect.Enabled)
			{
				controller.ObserveNativeDisconnect(__0);
				Log("CLIENT_RECOVERY_BYPASS reason=setting-disabled disconnect=" + __0);
				return true;
			}
			if (!serverEnding && controller.KeepLiveHostScene && Network.isServer)
			{
				Log("SERVER_LIVE_DISCONNECT_CALLBACK_IGNORED reason=" + __0
					+ " scene=kept transportRole=Server sessionReinitialized=false");
				return false;
			}
			if (!controller.ShouldResumeDisconnect(__0) || !LegacyReconnectTarget.HasTarget)
			{
				controller.ObserveNativeDisconnect(__0);
				return true;
			}
			controller.BeginRecovery(__0);
			Log("CLIENT_NATIVE_DISCONNECT_SUPPRESSED reason=" + __0
				+ " scene=kept transportMode=Legacy target=" + LegacyReconnectTarget.Description);
			return false;
		}

		private static bool PlayerDisconnectedPrefix(NetworkPlayer __0)
		{
			if (LegacyHostMigration.SuppressDisconnect) return false;
			if (!IsLegacy() || !Network.isServer || serverEnding || controller == null) return true;
			return !TryRetainPlayer(__0, controller);
		}

		private static bool PlayerConnectedPrefix(NetworkPlayer __0)
		{
			if (LegacyHostMigration.Current != null && !LegacyHostMigration.Current.AdmitParticipant(__0)) return false;
			if (!IsLegacy() || !Network.isServer || serverEnding || controller == null) return true;
			PrepareReconnectPlayer(__0, controller);
			return true;
		}

		private static bool ReconnectGuidelinePrefix(Game __instance, string __0,
			MethodBase __originalMethod)
		{
			if (!IsLegacy()) return true;
			if (controller == null || controller.Game != __instance)
			{
				string markerReason;
				if (!LegacyCrashResumeMarker.MatchesTarget(LegacyReconnectTarget.Description,
					out markerReason)) return true;
				Log("CLIENT_CRASH_STARTUP_GUIDELINE_SUPPRESSED rpc="
					+ (__originalMethod == null ? "RPC_SyncGuideline" : __originalMethod.Name)
					+ " characters=" + (__0 == null ? 0 : __0.Length) + " reason=" + markerReason);
				return false;
			}
			return !controller.ShouldSuppressReconnectGuideline(
				__originalMethod == null ? "RPC_SyncGuideline" : __originalMethod.Name, __0);
		}

		private static bool MachineCatchUpPrefix(MachineSerializer __instance, MethodBase __originalMethod)
		{
			return controller == null || !controller.ShouldSuppressNativeMachineCatchUp(__instance,
				__originalMethod == null ? "buffered-machine-rpc" : __originalMethod.Name);
		}

		private static bool MachineControllerCatchUpPrefix(MachineController __instance,
			MethodBase __originalMethod)
		{
			return controller == null || !controller.ShouldSuppressNativeMachineCatchUp(__instance,
				__originalMethod == null ? "buffered-machine-controller-rpc" : __originalMethod.Name);
		}

		private static void MachineControllerNamePostfix(MachineController __instance)
		{
			LegacyPlayerList.Normalize("native-name", __instance);
			if (controller != null) controller.ObserveNativePlayerName(__instance);
		}

		private static void MachineDestroyedPrefix(MachineController __instance)
		{
			LegacyNativeConstructionTrash.Forget(__instance);
			LegacyPlayerList.Remove(__instance, "native-destroy");
		}

		private static void MachineDestroyTrashPrefix(MachineController __instance, List<GameObject> __0)
		{
			try
			{
				if (IsLegacy() && Network.isClient && !Network.isServer && __instance != null && __instance.tag == "self")
					LegacyNativeConstructionTrash.Remember(__instance, __0);
			}
			catch (Exception error)
			{
				LegacyNativeConstructionTrash.Forget(__instance);
				Log("CLIENT_NATIVE_TRASH_TRACK_FAILED type=" + error.GetType().Name + " readiness=wait-native");
			}
		}

		private static void CrashWarpPrefix(MachineController __instance, Vector3 __0, bool __2)
		{
			if (controller != null) controller.TraceOwnerWarp(__instance, "before", __0, __2);
		}

		private static void CrashWarpPostfix(MachineController __instance, Vector3 __0, bool __2)
		{
			if (controller != null) controller.TraceOwnerWarp(__instance, "after", __0, __2);
		}

		private static bool MachineSerializePrefix(MachineSerializer __instance, BitStream __0)
		{
			return controller == null || __0 == null || !__0.isReading
				|| !controller.ShouldSuppressNativeMachineSerialize(__instance, "OnSerializeNetworkView.read");
		}

		private static bool NetworkViewRpcPrefix(NetworkView __instance, string __0)
		{
			return controller == null || !controller.ShouldSuppressOutgoingRpc(__instance, __0);
		}

		internal static void RememberTarget(HostData host, string guid, bool useNat)
		{
			LegacyReconnectTarget.Remember(host, guid, useNat);
		}

		internal static bool TryRetireReboundView(NetworkView view)
		{
			return IsLegacy() && controller != null && controller.TryRequestReboundViewRetire(view);
		}

		internal static bool MachineChangeMustWait()
		{
			return LegacyMigrationFreeze.Active || controller != null && controller.RecoveryInProgress;
		}

		internal static bool IsRetained(NetworkPlayer player)
		{
			foreach (ResumeRecord record in records.Values)
				if (record.Retained && record.Player == player) return true;
			return false;
		}

		private static void PrepareReconnectPlayer(NetworkPlayer player,
			LegacyTransientReconnectController source)
		{
			string playerGuid = PlayerGuid(player);
			if (string.IsNullOrEmpty(playerGuid)) return;
			foreach (ResumeRecord record in records.Values)
			{
				if (!record.Retained || record.Active || record.PlayerGuid != playerGuid) continue;
				record.PendingPlayer = player;
				record.PendingUntil = Time.realtimeSinceStartup + 12f;
				int scoped = 0;
				for (int i = 0; i < record.Views.Count; i++)
				{
					NetworkView view = record.Views[i];
					if (view == null) continue;
					try { if (view.SetScope(player, false)) scoped++; }
					catch { }
				}
				bool groupGate = source.SessionView == null || source.SessionView.group != MachineNetworkGroup;
				if (groupGate)
				{
					try { Network.SetSendingEnabled(player, MachineNetworkGroup, false); }
					catch { }
				}
				Log("SERVER_RECONNECT_GATE token=" + TokenLabel(record.Token) + " player="
					+ PlayerLabel(player) + " machineGroup=1 sending=" + (groupGate ? "false" : "unchanged")
					+ " controlGroup=" + (source.SessionView == null ? -1 : source.SessionView.group)
					+ " scopedViews=" + scoped
					+ " expectedViews=" + record.Views.Count);
				source.LogServerInventory("after-reconnect-gate", player, record);
				return;
			}
		}

		internal static void HandleHello(LegacyTransientReconnectController source, int protocol,
			string token, string playerName, int reconnecting, NetworkMessageInfo info)
		{
			if (!Network.isServer || source == null || protocol != ProtocolVersion || !ValidToken(token)) return;
			CleanupExpired();
			playerName = SafeName(playerName);
			ResumeRecord record;
			if (!records.TryGetValue(token, out record))
			{
				if (reconnecting != 0)
				{
					Log("SERVER_HELLO_REJECTED token=" + TokenLabel(token) + " sender="
						+ PlayerLabel(info.sender) + " reason=no-retained-session");
					source.SendStatus(info.sender, token, 3, "session-unavailable");
					return;
				}
				record = new ResumeRecord();
				record.Token = token;
				record.Name = playerName;
				record.Player = info.sender;
				record.PlayerGuid = PlayerGuid(info.sender);
				record.Active = true;
				record.RegisteredAt = Time.realtimeSinceStartup;
				record.LastHelloAt = record.RegisteredAt;
				record.Source = source;
				records.Add(token, record);
				SendActiveRebinds(source, info.sender);
				SendRetainedGhostSnapshots(source, info.sender, token);
				Log("SERVER_SESSION_REGISTERED token=" + TokenLabel(token) + " player=" + PlayerLabel(info.sender)
					+ " name=" + Quote(playerName) + " reconnecting=" + reconnecting);
				source.SendStatus(info.sender, token, 1, "registered");
				return;
			}

			if (record.Active && record.Player != info.sender)
			{
				if (reconnecting != 0)
				{
					Log("SERVER_RECONNECT_WAIT token=" + TokenLabel(token)
						+ " reason=previous-connection-still-active");
					source.SendStatus(info.sender, token, 5, "previous-connection-still-active");
					return;
				}
				Log("SERVER_HELLO_REJECTED token=" + TokenLabel(token) + " sender=" + PlayerLabel(info.sender)
					+ " reason=token-active-on-another-player");
				source.SendStatus(info.sender, token, 3, "token-active");
				return;
			}
			SendActiveRebinds(source, info.sender);
			SendRetainedGhostSnapshots(source, info.sender, token);

			if (!record.Retained)
			{
				record.Player = info.sender;
				record.PlayerGuid = PlayerGuid(info.sender);
				record.Name = playerName;
				record.Active = true;
				record.LastHelloAt = Time.realtimeSinceStartup;
				record.Source = source;
				source.SendStatus(info.sender, token, 1, "already-registered");
				return;
			}

			if (reconnecting == 0)
			{
				NetworkPlayer retainedOwner = record.PreviousPlayer != default(NetworkPlayer)
					? record.PreviousPlayer : record.Player;
				DestroyRetained(record, "fresh-join-replaces-retained");
				records.Remove(token);
				try { Network.SetSendingEnabled(info.sender, MachineNetworkGroup, true); }
				catch { }
				record = new ResumeRecord();
				record.Token = token;
				record.Name = playerName;
				record.Player = info.sender;
				record.PlayerGuid = PlayerGuid(info.sender);
				record.Active = true;
				record.RegisteredAt = Time.realtimeSinceStartup;
				record.LastHelloAt = record.RegisteredAt;
				record.Source = source;
				records.Add(token, record);
				Log("SERVER_RETAINED_SESSION_REPLACED token=" + TokenLabel(token) + " previous="
					+ PlayerLabel(retainedOwner) + " current=" + PlayerLabel(info.sender)
					+ " reason=fresh-join machineGroup=1 sending=true");
				source.SendStatus(info.sender, token, 1, "registered-after-retained-discard");
				return;
			}

			if (record.RebindInProgress && record.Player == info.sender)
			{
				record.LastHelloAt = Time.realtimeSinceStartup;
				source.SendStatus(info.sender, token, 4, "claim-in-progress");
				return;
			}

			NetworkPlayer previous = record.Player;
			if (record.PreviousPlayer == default(NetworkPlayer)) record.PreviousPlayer = previous;
			record.Player = info.sender;
			record.PlayerGuid = PlayerGuid(info.sender);
			record.Name = playerName;
			record.Active = true;
			record.LastHelloAt = Time.realtimeSinceStartup;
			record.Source = source;
			record.ResumeCount++;
			record.RebindInProgress = true;
			record.Claimed = new bool[record.Views.Count];
			int liveViews = 0;
			for (int i = 0; i < record.Views.Count; i++)
			{
				NetworkView view = record.Views[i];
				if (view == null) continue;
				liveViews++;
			}
			Log("SERVER_RECONNECT_HELLO token=" + TokenLabel(token) + " previous=" + PlayerLabel(previous)
				+ " current=" + PlayerLabel(info.sender) + " retainedViews=" + record.Views.Count
				+ " liveViews=" + liveViews + " explicitRebind=awaiting-claims");
			source.LogServerInventory("after-reconnect-hello", info.sender, record);
			source.SendStatus(info.sender, token, 4, "claim-ready");
		}

		internal static void HandleCrashHello(LegacyTransientReconnectController source, int protocol,
			string token, string playerName, NetworkViewID freshViewId,
			string ownerCheckpointFingerprint, string freshOwnerFingerprint,
			bool requireConsent, NetworkMessageInfo info)
		{
			int expectedProtocol = requireConsent ? CrashConsentProtocolVersion : CrashProtocolVersion;
			if (!Network.isServer || source == null || protocol != expectedProtocol
				|| !ValidToken(token)) return;
			CleanupExpired();
			playerName = SafeName(playerName);
			NetworkView freshView = FindViewById(freshViewId);
			MachineController freshMachine = freshView == null ? null : FindMachineForView(freshView);
			if (freshViewId == NetworkViewID.unassigned || freshViewId.owner != info.sender
				|| freshView == null || freshView.owner != info.sender
				|| freshMachine == null)
			{
				Log("SERVER_CRASH_RESUME_WAIT token=" + TokenLabel(token) + " sender="
					+ PlayerLabel(info.sender) + " fresh=" + IdLabel(freshViewId)
					+ " reason=fresh-machine-not-ready");
				source.SendCrashStatus(info.sender, token, 1, "fresh-machine-not-ready");
				return;
			}

			ResumeRecord record;
			if (!records.TryGetValue(token, out record))
			{
				RegisterCrashFallback(source, token, playerName, info.sender, freshView,
					"no-retained-session");
				return;
			}
			if (record.Active && record.Player != info.sender)
			{
				Log("SERVER_CRASH_RESUME_WAIT token=" + TokenLabel(token) + " sender="
					+ PlayerLabel(info.sender) + " active=" + PlayerLabel(record.Player)
					+ " reason=previous-connection-still-active");
				source.SendCrashStatus(info.sender, token, 1, "previous-connection-still-active");
				return;
			}
			if (!record.Retained)
			{
				if (record.Player == info.sender)
				{
					record.Active = true;
					record.LastHelloAt = Time.realtimeSinceStartup;
					source.SendCrashStatus(info.sender, token, 4, "already-registered");
					return;
				}
				RegisterCrashFallback(source, token, playerName, info.sender, freshView,
					"session-not-retained");
				return;
			}

			if (record.CrashConsentPending)
			{
				if (record.Player != info.sender || record.CrashFreshViewId != freshViewId)
				{
					Log("SERVER_CRASH_CONSENT_REJECTED token=" + TokenLabel(token) + " sender="
						+ PlayerLabel(info.sender) + " reason=consent-owned-by-another-connection");
					source.SendCrashStatus(info.sender, token, 5,
						"consent-owned-by-another-connection");
					return;
				}
				record.LastHelloAt = Time.realtimeSinceStartup;
				source.SendCrashStatus(info.sender, token, 6, "restore-consent-required");
				return;
			}

			if (record.CrashRestoreInProgress)
			{
				if (record.Player != info.sender || record.CrashFreshViewId != freshViewId)
				{
					Log("SERVER_CRASH_RESUME_REJECTED token=" + TokenLabel(token) + " sender="
						+ PlayerLabel(info.sender) + " reason=restore-owned-by-another-connection");
					source.SendCrashStatus(info.sender, token, 5, "restore-owned-by-another-connection");
					return;
				}
				record.LastHelloAt = Time.realtimeSinceStartup;
				source.BroadcastCrashRestorePose(record);
				source.SendCrashStatus(info.sender, token, 2, "restore-in-progress");
				return;
			}

			bool sameConstruction;
			string retainedFingerprint;
			string freshFingerprint;
			if (requireConsent)
			{
				int retainedBodyCount;
				int freshBodyCount;
				if (!TryExtractOwnerConstructionIdentity(ownerCheckpointFingerprint,
					out retainedFingerprint, out retainedBodyCount))
				{
					Log("SERVER_CRASH_MACHINE_UNAVAILABLE token=" + TokenLabel(token) + " sender="
						+ PlayerLabel(info.sender) + " fresh=" + IdLabel(freshViewId)
						+ " reason=owner-checkpoint-fingerprint-invalid action=fresh-fallback");
					ReplaceRetainedWithCrashFallback(source, record, token, playerName, info.sender,
						freshView, "owner-checkpoint-fingerprint-invalid");
					return;
				}
				if (!TryExtractOwnerConstructionIdentity(freshOwnerFingerprint,
					out freshFingerprint, out freshBodyCount))
				{
					Log("SERVER_CRASH_MACHINE_UNAVAILABLE token=" + TokenLabel(token) + " sender="
						+ PlayerLabel(info.sender) + " fresh=" + IdLabel(freshViewId)
						+ " reason=fresh-owner-fingerprint-invalid action=fresh-fallback");
					ReplaceRetainedWithCrashFallback(source, record, token, playerName, info.sender,
						freshView, "fresh-owner-fingerprint-invalid");
					return;
				}
				sameConstruction = retainedBodyCount == freshBodyCount
					&& string.Equals(retainedFingerprint, freshFingerprint, StringComparison.Ordinal);
			}
			else
			{
				bool retryable;
				string comparisonReason;
				if (!TryCompareCrashMachineConstruction(record, freshMachine, null,
					out sameConstruction, out retryable, out retainedFingerprint,
					out freshFingerprint, out comparisonReason))
				{
					if (retryable)
					{
						Log("SERVER_CRASH_MACHINE_WAIT token=" + TokenLabel(token) + " sender="
							+ PlayerLabel(info.sender) + " fresh=" + IdLabel(freshViewId)
							+ " reason=" + Clean(comparisonReason));
						source.SendCrashStatus(info.sender, token, 1, comparisonReason);
						return;
					}
					Log("SERVER_CRASH_MACHINE_UNAVAILABLE token=" + TokenLabel(token) + " sender="
						+ PlayerLabel(info.sender) + " fresh=" + IdLabel(freshViewId)
						+ " reason=" + Clean(comparisonReason) + " action=fresh-fallback");
					ReplaceRetainedWithCrashFallback(source, record, token, playerName, info.sender,
						freshView, "machine-identity-unavailable");
					return;
				}
			}
			if (!sameConstruction)
			{
				Log("SERVER_CRASH_MACHINE_MISMATCH token=" + TokenLabel(token) + " sender="
					+ PlayerLabel(info.sender) + " fresh=" + IdLabel(freshViewId)
					+ " retainedFingerprint=" + retainedFingerprint + " freshFingerprint="
					+ freshFingerprint + " action=fresh-fallback");
				ReplaceRetainedWithCrashFallback(source, record, token, playerName, info.sender,
					freshView, "machine-changed-after-crash");
				return;
			}
			Log("SERVER_CRASH_MACHINE_MATCH token=" + TokenLabel(token) + " sender="
				+ PlayerLabel(info.sender) + " fresh=" + IdLabel(freshViewId)
				+ " fingerprint=" + freshFingerprint + " action="
				+ (requireConsent ? "request-owner-consent" : "restore-retained-state"));

			if (requireConsent)
			{
				NetworkPlayer previous = record.Player;
				if (record.PreviousPlayer == default(NetworkPlayer)) record.PreviousPlayer = previous;
				record.Player = info.sender;
				record.PlayerGuid = PlayerGuid(info.sender);
				record.Name = playerName;
				record.Active = true;
				record.Voluntary = false;
				record.Source = source;
				record.LastHelloAt = Time.realtimeSinceStartup;
				record.RetainUntil = Math.Max(record.RetainUntil,
					Time.realtimeSinceStartup + CrashConsentSeconds);
				record.CrashConsentPending = true;
				record.CrashConsentUntil = Time.realtimeSinceStartup + CrashConsentSeconds;
				record.CrashExpectedConstructionFingerprint = ownerCheckpointFingerprint;
				record.CrashFreshView = freshView;
				record.CrashFreshViewId = freshViewId;
				Log("SERVER_CRASH_CONSENT_REQUIRED token=" + TokenLabel(token) + " previous="
					+ PlayerLabel(previous) + " current=" + PlayerLabel(info.sender) + " fresh="
					+ IdLabel(freshViewId) + " timeoutSeconds=60");
				source.SendCrashStatus(info.sender, token, 6, "restore-consent-required");
				return;
			}

			BeginCrashRestore(source, record, token, playerName, info.sender, freshView);
		}

		internal static void HandleCrashDecision(LegacyTransientReconnectController source,
			int protocol, string token, NetworkViewID freshViewId, bool accept,
			string freshOwnerFingerprint,
			NetworkMessageInfo info)
		{
			if (!Network.isServer || source == null || protocol != CrashConsentProtocolVersion
				|| !ValidToken(token)) return;
			CleanupExpired();
			ResumeRecord record;
			if (!records.TryGetValue(token, out record) || !record.Retained
				|| !record.CrashConsentPending || record.Player != info.sender
				|| record.CrashFreshViewId != freshViewId || freshViewId.owner != info.sender)
			{
				Log("SERVER_CRASH_CONSENT_DECISION_REJECTED token=" + TokenLabel(token)
					+ " sender=" + PlayerLabel(info.sender) + " fresh=" + IdLabel(freshViewId)
					+ " reason=consent-session-not-pending");
				source.SendCrashStatus(info.sender, token, 5, "consent-session-not-pending");
				return;
			}

			NetworkView freshView = FindViewById(freshViewId);
			MachineController freshMachine = freshView == null ? null : FindMachineForView(freshView);
			if (freshView == null || freshView.owner != info.sender || freshMachine == null)
			{
				Log("SERVER_CRASH_CONSENT_DECISION_WAIT token=" + TokenLabel(token)
					+ " sender=" + PlayerLabel(info.sender) + " fresh=" + IdLabel(freshViewId)
					+ " reason=fresh-machine-not-ready");
				source.SendCrashStatus(info.sender, token, 6, "restore-consent-required");
				return;
			}

			if (!accept)
			{
				Log("SERVER_CRASH_CONSENT_DECISION token=" + TokenLabel(token) + " player="
					+ PlayerLabel(info.sender) + " fresh=" + IdLabel(freshViewId)
					+ " accepted=false action=fresh-fallback");
				ReplaceRetainedWithCrashFallback(source, record, token, record.Name, info.sender,
					freshView, "restore-declined-by-owner");
				return;
			}

			string retainedFingerprint;
			string freshFingerprint;
			int retainedBodyCount;
			int freshBodyCount;
			if (!TryExtractOwnerConstructionIdentity(record.CrashExpectedConstructionFingerprint,
				out retainedFingerprint, out retainedBodyCount)
				|| !TryExtractOwnerConstructionIdentity(freshOwnerFingerprint,
					out freshFingerprint, out freshBodyCount))
			{
				Log("SERVER_CRASH_CONSENT_DECISION_INVALID token=" + TokenLabel(token)
					+ " sender=" + PlayerLabel(info.sender) + " fresh=" + IdLabel(freshViewId)
					+ " reason=fresh-owner-fingerprint-invalid action=fresh-fallback");
				ReplaceRetainedWithCrashFallback(source, record, token, record.Name, info.sender,
					freshView, "machine-identity-unavailable-after-consent");
				return;
			}
			bool sameConstruction = retainedBodyCount == freshBodyCount
				&& string.Equals(retainedFingerprint, freshFingerprint, StringComparison.Ordinal);
			if (!sameConstruction)
			{
				Log("SERVER_CRASH_MACHINE_MISMATCH_AFTER_CONSENT token=" + TokenLabel(token)
					+ " sender=" + PlayerLabel(info.sender) + " fresh=" + IdLabel(freshViewId)
					+ " retainedFingerprint=" + retainedFingerprint + " freshFingerprint="
					+ freshFingerprint + " action=fresh-fallback");
				ReplaceRetainedWithCrashFallback(source, record, token, record.Name, info.sender,
					freshView, "machine-changed-during-consent");
				return;
			}

			Log("SERVER_CRASH_CONSENT_DECISION token=" + TokenLabel(token) + " player="
				+ PlayerLabel(info.sender) + " fresh=" + IdLabel(freshViewId)
				+ " accepted=true action=restore-retained-state");
			BeginCrashRestore(source, record, token, record.Name, info.sender, freshView);
		}

		private static void BeginCrashRestore(LegacyTransientReconnectController source,
			ResumeRecord record, string token, string playerName, NetworkPlayer player,
			NetworkView freshView)
		{
			NetworkViewID freshViewId = freshView.viewID;

			GhostViewSnapshot captured = null;
			for (int index = 0; index < record.GhostSnapshots.Count; index++)
				if (record.GhostSnapshots[index] != null && record.GhostSnapshots[index].Chunks != null)
				{
					captured = record.GhostSnapshots[index];
					break;
				}
			if (captured == null)
			{
				ReplaceRetainedWithCrashFallback(source, record, token, playerName, player,
					freshView, "retained-snapshot-missing");
				return;
			}

			byte[] payload;
			string failure;
			LegacyGhostPoseSnapshot snapshot;
			if (!LegacyGhostPoseTransportCodec.TryJoin(captured.Chunks, out payload, out failure)
				|| !LegacyGhostPoseSnapshotCodec.TryDecode(payload, out snapshot, out failure))
			{
				ReplaceRetainedWithCrashFallback(source, record, token, playerName, player,
					freshView, "retained-snapshot-" + Clean(failure));
				return;
			}
			snapshot.SnapshotId = NextGhostSnapshotId();
			snapshot.Fingerprint = "view=" + IdLabel(freshViewId) + ";bodies="
				+ snapshot.Bodies.Length + ";visuals=" + snapshot.Visuals.Length;
			if (!LegacyGhostPoseSnapshotCodec.TryEncode(snapshot, out payload, out failure))
			{
				ReplaceRetainedWithCrashFallback(source, record, token, playerName, player,
					freshView, "restore-snapshot-" + Clean(failure));
				return;
			}

			NetworkPlayer previous = record.PreviousPlayer != default(NetworkPlayer)
				? record.PreviousPlayer : record.Player;
			if (record.PreviousPlayer == default(NetworkPlayer)) record.PreviousPlayer = previous;
			record.Player = player;
			record.PlayerGuid = PlayerGuid(player);
			record.Name = playerName;
			record.Active = true;
			record.Voluntary = false;
			record.Source = source;
			record.LastHelloAt = Time.realtimeSinceStartup;
			record.RetainUntil = Math.Max(record.RetainUntil, Time.realtimeSinceStartup + 60f);
			record.PendingPlayer = default(NetworkPlayer);
			record.PendingUntil = 0f;
			record.CrashConsentPending = false;
			record.CrashConsentUntil = 0f;
			record.CrashRestoreInProgress = true;
			record.CrashFreshView = freshView;
			record.CrashFreshViewId = freshViewId;
			record.CrashSnapshotId = snapshot.SnapshotId;
			record.CrashBodyCount = snapshot.Bodies.Length;
			record.CrashVisualCount = snapshot.Visuals.Length;
			record.CrashChunks = LegacyGhostPoseTransportCodec.Split(payload);
			record.CrashOwnerApplied = false;
			record.CrashServerApplied = false;
			record.CrashRestoreUntil = Time.realtimeSinceStartup + 45f;
			ReplayCrashRetainedIds(source, record, player);
			for (int index = 0; index < record.Views.Count; index++)
			{
				try { if (record.Views[index] != null) record.Views[index].SetScope(player, false); }
				catch { }
			}
			try { Network.SetSendingEnabled(player, MachineNetworkGroup, true); }
			catch { }
			SendActiveRebinds(source, player);
			SendRetainedGhostSnapshots(source, player, token);
			Log("SERVER_CRASH_RESUME_ACCEPTED token=" + TokenLabel(token) + " previous="
				+ PlayerLabel(previous) + " current=" + PlayerLabel(player) + " fresh="
				+ IdLabel(freshViewId) + " snapshot=" + snapshot.SnapshotId + " bodies="
				+ snapshot.Bodies.Length + " visuals=" + snapshot.Visuals.Length + " chunks="
				+ record.CrashChunks.Length);
			source.BroadcastCrashRestorePose(record);
			source.SendCrashStatus(player, token, 2, "restore-pose-sent");
		}

		private static bool TryCompareCrashMachineConstruction(ResumeRecord record,
			MachineController freshMachine, string ownerCheckpointFingerprint,
			out bool sameConstruction, out bool retryable,
			out string retainedFingerprint, out string freshFingerprint, out string reason)
		{
			sameConstruction = false;
			retryable = false;
			retainedFingerprint = null;
			freshFingerprint = null;
			reason = null;
			string expectedConstruction;
			if (!string.IsNullOrEmpty(ownerCheckpointFingerprint))
			{
				if (!TryExtractOwnerConstructionFingerprint(ownerCheckpointFingerprint,
					out expectedConstruction))
				{
					reason = "owner-checkpoint-fingerprint-invalid";
					return false;
				}
				string freshReason;
				if (!LegacyMachineRecoveryFingerprint.TryCompute(freshMachine,
					out freshFingerprint, out freshReason))
				{
					retryable = freshReason == "construction-not-ready";
					reason = "fresh-" + Clean(freshReason);
					return false;
				}
				retainedFingerprint = expectedConstruction;
				sameConstruction = string.Equals(expectedConstruction, freshFingerprint,
					StringComparison.Ordinal);
				reason = sameConstruction ? "owner-checkpoint-construction-match"
					: "owner-checkpoint-construction-mismatch";
				return true;
			}

			MachineController retainedMachine = null;
			if (record != null)
				for (int index = 0; index < record.Views.Count; index++)
				{
					NetworkView retainedView = record.Views[index];
					MachineController candidate = retainedView == null ? null : FindMachineForView(retainedView);
					if (candidate == null || object.ReferenceEquals(candidate, freshMachine)) continue;
					retainedMachine = candidate;
					break;
				}
			if (retainedMachine == null)
			{
				retryable = true;
				reason = "retained-machine-not-ready";
				return false;
			}
			string fingerprintReason;
			if (!LegacyMachineRecoveryFingerprint.TryCompute(retainedMachine,
				out retainedFingerprint, out fingerprintReason))
			{
				retryable = fingerprintReason == "construction-not-ready";
				reason = "retained-" + Clean(fingerprintReason);
				return false;
			}
			if (!LegacyMachineRecoveryFingerprint.TryCompute(freshMachine,
				out freshFingerprint, out fingerprintReason))
			{
				retryable = fingerprintReason == "construction-not-ready";
				reason = "fresh-" + Clean(fingerprintReason);
				return false;
			}
			sameConstruction = string.Equals(retainedFingerprint, freshFingerprint,
				StringComparison.Ordinal);
			reason = sameConstruction ? "construction-match" : "construction-mismatch";
			return true;
		}

		internal static bool TryExtractOwnerConstructionFingerprint(string value,
			out string constructionFingerprint)
		{
			constructionFingerprint = null;
			if (string.IsNullOrEmpty(value)) return false;
			string candidate;
			if (value.Length == 16) candidate = value;
			else
			{
				int bodyCount;
				return TryExtractOwnerConstructionIdentity(value, out constructionFingerprint,
					out bodyCount);
			}
			return IsConstructionFingerprint(candidate, out constructionFingerprint);
		}

		internal static bool TryExtractOwnerConstructionIdentity(string value,
			out string constructionFingerprint, out int bodyCount)
		{
			constructionFingerprint = null;
			bodyCount = 0;
			if (string.IsNullOrEmpty(value)
				|| !value.StartsWith("owner=", StringComparison.Ordinal)) return false;
			int separator = value.IndexOf(';', 6);
			if (separator != 22 || !value.Substring(separator).StartsWith(";bodies=",
				StringComparison.Ordinal)) return false;
			if (!int.TryParse(value.Substring(separator + 8), NumberStyles.None,
				CultureInfo.InvariantCulture, out bodyCount) || bodyCount <= 0) return false;
			return IsConstructionFingerprint(value.Substring(6, 16),
				out constructionFingerprint);
		}

		private static bool IsConstructionFingerprint(string candidate,
			out string constructionFingerprint)
		{
			constructionFingerprint = null;
			if (string.IsNullOrEmpty(candidate) || candidate.Length != 16) return false;
			for (int index = 0; index < candidate.Length; index++)
			{
				char character = candidate[index];
				if (!((character >= '0' && character <= '9')
					|| (character >= 'A' && character <= 'F'))) return false;
			}
			constructionFingerprint = candidate;
			return true;
		}

		internal static void HandleCrashOwnerApplied(LegacyTransientReconnectController source,
			int protocol, string token, NetworkViewID freshViewId, int snapshotId,
			NetworkMessageInfo info)
		{
			if (!Network.isServer || source == null || protocol != CrashProtocolVersion
				|| !ValidToken(token)) return;
			ResumeRecord record;
			if (!records.TryGetValue(token, out record) || !record.CrashRestoreInProgress
				|| record.Player != info.sender || record.CrashFreshViewId != freshViewId
				|| record.CrashSnapshotId != snapshotId)
			{
				Log("SERVER_CRASH_OWNER_ACK_REJECTED token=" + TokenLabel(token) + " sender="
					+ PlayerLabel(info.sender) + " fresh=" + IdLabel(freshViewId)
					+ " snapshot=" + snapshotId);
				return;
			}
			record.CrashOwnerApplied = true;
			Log("SERVER_CRASH_OWNER_ACK token=" + TokenLabel(token) + " player="
				+ PlayerLabel(info.sender) + " fresh=" + IdLabel(freshViewId)
				+ " snapshot=" + snapshotId + " serverApplied=" + record.CrashServerApplied);
			TryCompleteCrashRestore(source, record);
		}

		internal static void HandleCrashServerApplied(LegacyTransientReconnectController source,
			string token, NetworkViewID freshViewId, int snapshotId)
		{
			if (!Network.isServer || source == null || !ValidToken(token)) return;
			ResumeRecord record;
			if (!records.TryGetValue(token, out record) || !record.CrashRestoreInProgress
				|| record.CrashFreshViewId != freshViewId || record.CrashSnapshotId != snapshotId) return;
			record.CrashServerApplied = true;
			Log("SERVER_CRASH_POSE_APPLIED token=" + TokenLabel(token) + " fresh="
				+ IdLabel(freshViewId) + " snapshot=" + snapshotId + " ownerApplied="
				+ record.CrashOwnerApplied);
			TryCompleteCrashRestore(source, record);
		}

		private static void TryCompleteCrashRestore(LegacyTransientReconnectController source,
			ResumeRecord record)
		{
			if (!record.CrashRestoreInProgress || !record.CrashOwnerApplied
				|| !record.CrashServerApplied) return;
			NetworkPlayer player = record.Player;
			NetworkView freshView = record.CrashFreshView;
			NetworkViewID freshViewId = record.CrashFreshViewId;
			int snapshotId = record.CrashSnapshotId;
			DestroyRetained(record, "crash-state-transplanted");
			source.CompleteRemoteCrashPresentation(freshViewId, snapshotId);
			ResetCrashRestore(record);
			// The previous peer index is no longer a retained owner after the old
			// ghost and all of its buffered IDs have been retired. Keeping it here
			// makes the completed active record look as if the disconnected peer is
			// still part of the server session and can target later cleanup at it.
			record.PreviousPlayer = default(NetworkPlayer);
			record.Player = player;
			record.PlayerGuid = PlayerGuid(player);
			record.Active = true;
			record.Retained = false;
			record.Voluntary = false;
			record.LastHelloAt = Time.realtimeSinceStartup;
			if (freshView != null)
			{
				record.Views.Add(freshView);
				record.OldViewIds.Add(freshViewId);
				record.BufferedViewIds.Add(freshViewId);
				try { freshView.SetScope(player, true); }
				catch { }
			}
			try { Network.SetSendingEnabled(player, MachineNetworkGroup, true); }
			catch { }
			Log("SERVER_CRASH_RESUME_COMPLETE token=" + TokenLabel(record.Token) + " player="
				+ PlayerLabel(player) + " fresh=" + IdLabel(freshViewId) + " snapshot="
				+ snapshotId + " oldGhostDestroyed=true previousPlayerCleared=true machineGroup=1 sending=true");
			source.SendCrashStatus(player, record.Token, 3, "restore-complete");
			Broadcast(source, record.Name, true);
		}

		private static void RegisterCrashFallback(LegacyTransientReconnectController source,
			string token, string playerName, NetworkPlayer player, NetworkView freshView, string reason)
		{
			ResumeRecord record = new ResumeRecord();
			record.Token = token;
			record.Name = playerName;
			record.Player = player;
			record.PlayerGuid = PlayerGuid(player);
			record.Active = true;
			record.RegisteredAt = Time.realtimeSinceStartup;
			record.LastHelloAt = record.RegisteredAt;
			record.Source = source;
			if (freshView != null)
			{
				record.Views.Add(freshView);
				record.OldViewIds.Add(freshView.viewID);
				record.BufferedViewIds.Add(freshView.viewID);
			}
			records[token] = record;
			try { Network.SetSendingEnabled(player, MachineNetworkGroup, true); }
			catch { }
			SendActiveRebinds(source, player);
			SendRetainedGhostSnapshots(source, player, token);
			Log("SERVER_CRASH_RESUME_FALLBACK token=" + TokenLabel(token) + " player="
				+ PlayerLabel(player) + " fresh=" + IdLabel(freshView == null
					? NetworkViewID.unassigned : freshView.viewID) + " reason=" + Clean(reason));
			source.SendCrashStatus(player, token, 4, reason);
		}

		private static void ReplaceRetainedWithCrashFallback(
			LegacyTransientReconnectController source, ResumeRecord record, string token,
			string playerName, NetworkPlayer player, NetworkView freshView, string reason)
		{
			PromoteRetainedToCrashFallback(source, record, playerName, player, freshView, reason);
		}

		private static void PromoteRetainedToCrashFallback(
			LegacyTransientReconnectController source, ResumeRecord record, string playerName,
			NetworkPlayer player, NetworkView freshView, string reason)
		{
			DestroyRetained(record, "crash-fallback-" + Clean(reason));
			record.Name = playerName;
			record.Player = player;
			record.PlayerGuid = PlayerGuid(player);
			record.PreviousPlayer = default(NetworkPlayer);
			record.Active = true;
			record.Retained = false;
			record.Voluntary = false;
			record.RegisteredAt = Time.realtimeSinceStartup;
			record.LastHelloAt = record.RegisteredAt;
			record.Source = source;
			if (freshView != null)
			{
				record.Views.Add(freshView);
				record.OldViewIds.Add(freshView.viewID);
				record.BufferedViewIds.Add(freshView.viewID);
			}
			try { Network.SetSendingEnabled(player, MachineNetworkGroup, true); }
			catch { }
			Log("SERVER_CRASH_RESUME_FALLBACK token=" + TokenLabel(record.Token) + " player="
				+ PlayerLabel(player) + " fresh=" + IdLabel(freshView == null
					? NetworkViewID.unassigned : freshView.viewID) + " reason=" + Clean(reason));
			source.SendCrashStatus(player, record.Token, 4, reason);
		}

		private static void ResetCrashRestore(ResumeRecord record)
		{
			record.CrashConsentPending = false;
			record.CrashConsentUntil = 0f;
			record.CrashExpectedConstructionFingerprint = null;
			record.CrashRestoreInProgress = false;
			record.CrashFreshView = null;
			record.CrashFreshViewId = NetworkViewID.unassigned;
			record.CrashSnapshotId = 0;
			record.CrashBodyCount = 0;
			record.CrashVisualCount = 0;
			record.CrashChunks = null;
			record.CrashOwnerApplied = false;
			record.CrashServerApplied = false;
			record.CrashRestoreUntil = 0f;
		}

		internal static void HandleClaim(LegacyTransientReconnectController source, int protocol,
			string token, int claimIndex, int claimCount, NetworkViewID oldId, NetworkViewID newId,
			NetworkMessageInfo info)
		{
			if (!Network.isServer || source == null || protocol != ProtocolVersion || !ValidToken(token)) return;
			ResumeRecord record;
			if (!records.TryGetValue(token, out record) || !record.Retained || !record.Active
				|| record.Player != info.sender || !record.RebindInProgress)
			{
				Log("SERVER_CLAIM_REJECTED token=" + TokenLabel(token) + " sender="
					+ PlayerLabel(info.sender) + " reason=session-not-ready");
				return;
			}
			if (claimCount != record.Views.Count || claimIndex < 0 || claimIndex >= claimCount
				|| record.OldViewIds.Count != record.Views.Count
				|| record.BufferedViewIds.Count != record.Views.Count || record.Claimed == null
				|| record.Claimed.Length != record.Views.Count)
			{
				Log("SERVER_CLAIM_REJECTED token=" + TokenLabel(token) + " index=" + claimIndex
					+ " count=" + claimCount + " expected=" + record.Views.Count + " reason=shape-mismatch");
				return;
			}
			int recordIndex = -1;
			for (int i = 0; i < record.OldViewIds.Count; i++)
				if (record.OldViewIds[i] == oldId) { recordIndex = i; break; }
			if (recordIndex < 0 || newId == NetworkViewID.unassigned || newId.owner != info.sender)
			{
				Log("SERVER_CLAIM_REJECTED token=" + TokenLabel(token) + " index=" + claimIndex
					+ " old=" + IdLabel(oldId) + " matchedIndex=" + recordIndex
					+ " new=" + IdLabel(newId) + " newOwner=" + PlayerLabel(newId.owner)
					+ " reason=id-validation");
				return;
			}

			NetworkView view = record.Views[recordIndex];
			if (view == null)
			{
				Log("SERVER_CLAIM_REJECTED token=" + TokenLabel(token) + " index=" + claimIndex
					+ " reason=retained-view-missing");
				return;
			}
			if (!record.Claimed[recordIndex])
			{
				NetworkView collision = FindViewById(newId);
				if (collision != null && collision != view)
				{
					Log("SERVER_CLAIM_REJECTED token=" + TokenLabel(token) + " index=" + claimIndex
						+ " new=" + IdLabel(newId) + " reason=new-id-collision");
					return;
				}
				try { view.viewID = newId; }
				catch (Exception error)
				{
					Log("SERVER_CLAIM_ASSIGN_FAILED token=" + TokenLabel(token) + " index=" + claimIndex
						+ " type=" + error.GetType().Name + " message=" + Clean(error.Message));
					return;
				}
				if (view.viewID != newId || view.owner != info.sender)
				{
					Log("SERVER_CLAIM_ASSIGN_FAILED token=" + TokenLabel(token) + " index=" + claimIndex
						+ " assigned=" + IdLabel(view.viewID) + " owner=" + PlayerLabel(view.owner));
					return;
				}
				record.Claimed[recordIndex] = true;
				source.BroadcastRebind(oldId, newId);
				Log("SERVER_CLAIM_ACCEPTED token=" + TokenLabel(token) + " index=" + claimIndex
					+ " old=" + IdLabel(oldId) + " new=" + IdLabel(newId)
					+ " owner=" + PlayerLabel(view.owner));
			}

			for (int i = 0; i < record.Claimed.Length; i++) if (!record.Claimed[i]) return;
			CompleteServerRebind(source, record, info.sender);
		}

		internal static void HandleRetire(LegacyTransientReconnectController source, int protocol,
			string token, NetworkViewID oldId, NetworkViewID newId, NetworkMessageInfo info)
		{
			if (!Network.isServer || source == null || protocol != ProtocolVersion || !ValidToken(token)) return;
			ResumeRecord record;
			if (!records.TryGetValue(token, out record) || !record.Active || record.Retained
				|| record.RebindInProgress || record.Player != info.sender
				|| record.Views.Count != record.OldViewIds.Count
				|| record.Views.Count != record.BufferedViewIds.Count)
			{
				Log("SERVER_REBOUND_RETIRE_REJECTED token=" + TokenLabel(token) + " sender="
					+ PlayerLabel(info.sender) + " old=" + IdLabel(oldId) + " new=" + IdLabel(newId)
					+ " reason=session-not-ready");
				return;
			}

			int recordIndex = -1;
			NetworkView view = null;
			for (int i = 0; i < record.Views.Count; i++)
			{
				NetworkView candidate = record.Views[i];
				if (candidate == null || record.OldViewIds[i] != oldId || candidate.viewID != newId) continue;
				recordIndex = i;
				view = candidate;
				break;
			}
			if (recordIndex < 0 || view == null || oldId == NetworkViewID.unassigned
				|| newId == NetworkViewID.unassigned || newId.owner != info.sender || view.owner != info.sender)
			{
				Log("SERVER_REBOUND_RETIRE_REJECTED token=" + TokenLabel(token) + " sender="
					+ PlayerLabel(info.sender) + " old=" + IdLabel(oldId) + " new=" + IdLabel(newId)
					+ " matchedIndex=" + recordIndex + " reason=id-or-owner-validation");
				return;
			}

			GameObject networkObject = view.gameObject;
			NetworkViewID bufferedId = record.BufferedViewIds[recordIndex];
			List<NetworkViewID> retirementIds = LegacyRetirementIds.Collect(bufferedId,
				oldId, newId, NetworkViewID.unassigned);
			LegacyPlayerList.Remove(FindMachineForView(view), "server-retirement");
			source.BroadcastRetire(oldId, newId);
			if (bufferedId != NetworkViewID.unassigned && bufferedId != oldId && bufferedId != newId)
				source.BroadcastRetire(bufferedId, newId);
			bool networkDestroy = true;
			try
			{

				Network.Destroy(networkObject);
			}
			catch (Exception error)
			{
				networkDestroy = false;
				Log("SERVER_REBOUND_RETIRE_NETWORK_DESTROY_FAILED token=" + TokenLabel(token)
					+ " type=" + error.GetType().Name + " message=" + Clean(error.Message));
			}
			bool buffersRemoved = true;
			foreach (NetworkViewID retirementId in retirementIds)
				buffersRemoved = RemoveViewBuffer(retirementId, "retirement", token) && buffersRemoved;
			try { if (networkObject != null) UnityEngine.Object.Destroy(networkObject); }
			catch (Exception error)
			{
				Log("SERVER_REBOUND_RETIRE_LOCAL_DESTROY_FAILED token=" + TokenLabel(token)
					+ " type=" + error.GetType().Name + " message=" + Clean(error.Message));
			}
			record.Views.RemoveAt(recordIndex);
			record.OldViewIds.RemoveAt(recordIndex);
			record.BufferedViewIds.RemoveAt(recordIndex);
			record.Claimed = null;
			Log("SERVER_REBOUND_RETIRE_ACCEPTED token=" + TokenLabel(token) + " player="
				+ PlayerLabel(info.sender) + " old=" + IdLabel(oldId) + " new=" + IdLabel(newId)
				+ " networkDestroy=" + networkDestroy + " original=" + IdLabel(bufferedId)
				+ " distinctBuffers=" + retirementIds.Count + " buffersRemoved=" + buffersRemoved
				+ " remainingMappings=" + record.Views.Count);
		}

		private static bool RemoveViewBuffer(NetworkViewID viewId, string stage, string token)
		{
			try
			{
				Network.RemoveRPCs(viewId);
				LegacyHostMigrationNative.RetireInstantiate(viewId);
				return true;
			}
			catch (Exception error)
			{
				Log("SERVER_REBOUND_RETIRE_REMOVE_RPCS_FAILED token=" + TokenLabel(token) + " stage="
					+ stage + " view=" + IdLabel(viewId) + " type=" + error.GetType().Name
					+ " message=" + Clean(error.Message));
				return false;
			}
		}

		private static void CompleteServerRebind(LegacyTransientReconnectController source,
			ResumeRecord record, NetworkPlayer player)
		{
			if (!record.RebindInProgress) return;
			for (int i = 0; i < record.Views.Count; i++)
			{
				NetworkView view = record.Views[i];
				if (view == null || view.owner != player) return;
				try { view.SetScope(player, true); }
				catch { }
			}
			if (source.SessionView == null || source.SessionView.group != MachineNetworkGroup)
			{
				try { Network.SetSendingEnabled(player, MachineNetworkGroup, true); }
				catch { }
			}
			ReleaseFrozen(record, "explicit-client-viewid");
			record.GhostSnapshots.Clear();
			record.Retained = false;
			record.RebindInProgress = false;
			record.PreviousPlayer = default(NetworkPlayer);
			record.PendingPlayer = default(NetworkPlayer);
			record.PendingUntil = 0f;
			Log("SERVER_REBIND_COMPLETE token=" + TokenLabel(record.Token) + " player="
				+ PlayerLabel(player) + " views=" + record.Claimed.Length + " machineGroup=1 sending=true");
			source.SendStatus(player, record.Token, 2, "explicit-rebind-complete");
			Broadcast(source, record.Name, true);
		}

		internal static void HandleGoodbye(LegacyTransientReconnectController source, int protocol,
			string token, NetworkMessageInfo info)
		{
			if (!Network.isServer || protocol != ProtocolVersion || !ValidToken(token)) return;
			ResumeRecord record;
			if (!records.TryGetValue(token, out record) || !record.Active || record.Player != info.sender) return;
			record.Voluntary = true;
			Log("SERVER_GOODBYE token=" + TokenLabel(token) + " player=" + PlayerLabel(info.sender)
				+ " retainedGhost=" + record.Retained);
			// Completed rebinds still own original buffered Instantiate/RPC IDs.
			// Native disconnect only removes the current peer slot's objects.
			if (record.Views.Count > 0 || (record.Retained && record.PreviousPlayer != default(NetworkPlayer)))
			{
				DestroyRetained(record, "voluntary-exit-after-reconnect");
				records.Remove(token);
			}
		}

		internal static void HandleTestDrop(LegacyTransientReconnectController source, int protocol,
			string token, NetworkMessageInfo info)
		{
			if (!Network.isServer || source == null || protocol != ProtocolVersion || !ValidToken(token)) return;
			ResumeRecord record;
			if (!records.TryGetValue(token, out record) || !record.Active || record.Player != info.sender)
			{
				Log("SERVER_TEST_DROP_REJECTED token=" + TokenLabel(token) + " sender="
					+ PlayerLabel(info.sender) + " reason=session-not-active");
				return;
			}
			Log("SERVER_TEST_DROP_ACCEPTED token=" + TokenLabel(token) + " player="
				+ PlayerLabel(info.sender));
			SendSystem(source, "[MPatcher] " + record.Name
				+ ": controlled connection-loss test started.");
			source.ScheduleTestDrop(info.sender, token);
		}

		private static bool TryRetainPlayer(NetworkPlayer player, LegacyTransientReconnectController source)
		{
			ResumeRecord record = null;
			foreach (ResumeRecord candidate in records.Values)
			{
				if (candidate.Active && candidate.Player == player) { record = candidate; break; }
			}
			if (record == null) return false;
			if (record.Voluntary)
			{
				Log("SERVER_NATIVE_DISCONNECT_ALLOWED token=" + TokenLabel(record.Token)
					+ " player=" + PlayerLabel(player) + " reason=voluntary-exit");
				records.Remove(record.Token);
				return false;
			}
			if (record.Retained && (record.CrashConsentPending || record.CrashRestoreInProgress))
			{
				NetworkPlayer retainedOwner = record.PreviousPlayer;
				Log("SERVER_CRASH_RESUME_INTERRUPTED token=" + TokenLabel(record.Token)
					+ " player=" + PlayerLabel(player) + " fresh=" + IdLabel(record.CrashFreshViewId)
					+ " consentPending=" + record.CrashConsentPending
					+ " action=discard-fresh-keep-old-ghost");
				ResetCrashRestore(record);
				record.Player = retainedOwner;
				record.PlayerGuid = PlayerGuid(retainedOwner);
				record.PreviousPlayer = default(NetworkPlayer);
				record.Active = false;
				record.RetainUntil = Time.realtimeSinceStartup + RetentionSeconds;
				return false;
			}
			if (record.Retained)
			{
				record.Active = false;
				record.RebindInProgress = false;
				record.PendingPlayer = default(NetworkPlayer);
				record.PendingUntil = 0f;
				record.OldViewIds.Clear();
				for (int i = 0; i < record.Views.Count; i++)
					record.OldViewIds.Add(record.Views[i] == null
						? NetworkViewID.unassigned : record.Views[i].viewID);
				record.Claimed = null;
				record.RetainUntil = Time.realtimeSinceStartup + RetentionSeconds;
				Log("SERVER_RECONNECT_INTERRUPTED token=" + TokenLabel(record.Token)
					+ " player=" + PlayerLabel(player) + " retainedViews=" + record.Views.Count
					+ " frozenBodies=" + record.Bodies.Count + " retentionSeconds=300");
				SendSystem(source, "[MPatcher] " + record.Name
					+ ": connection lost again; machine remains frozen.");
				return true;
			}

			record.Active = false;
			record.Retained = true;
			record.RetainUntil = Time.realtimeSinceStartup + RetentionSeconds;
			record.PreviousPlayer = default(NetworkPlayer);
			record.Bodies.Clear();
			record.GhostSnapshots.Clear();
			record.Claimed = null;
			record.RebindInProgress = false;
			record.PendingPlayer = default(NetworkPlayer);
			record.PendingUntil = 0f;
			FreezePlayerObjects(player, record);
			Log("SERVER_NATIVE_CLEANUP_SUPPRESSED token=" + TokenLabel(record.Token)
				+ " player=" + PlayerLabel(player) + " views=" + record.Views.Count
				+ " rigidbodies=" + record.Bodies.Count + " retentionSeconds=300");
			source.LogServerInventory("after-freeze", player, record);
			SendSystem(source, "[MPatcher] " + record.Name
				+ ": connection lost; machine frozen, waiting for reconnection.");
			return true;
		}

		private static void FreezePlayerObjects(NetworkPlayer player, ResumeRecord record)
		{
			Dictionary<int, NetworkViewID> bufferedByInstance = new Dictionary<int, NetworkViewID>();
			int rememberedCount = Math.Min(record.Views.Count, record.BufferedViewIds.Count);
			for (int index = 0; index < rememberedCount; index++)
			{
				NetworkView remembered = record.Views[index];
				NetworkViewID bufferedId = record.BufferedViewIds[index];
				if (remembered == null || bufferedId == NetworkViewID.unassigned) continue;
				bufferedByInstance[remembered.GetInstanceID()] = bufferedId;
			}
			record.Views.Clear();
			record.OldViewIds.Clear();
			record.BufferedViewIds.Clear();
			HashSet<int> seenBodies = new HashSet<int>();
			HashSet<int> seenMachines = new HashSet<int>();
			UnityEngine.Object[] found = UnityEngine.Object.FindObjectsOfType(typeof(NetworkView));
			for (int i = 0; i < found.Length; i++)
			{
				NetworkView view = found[i] as NetworkView;
				if (view == null || view.owner != player) continue;
				NetworkViewID bufferedViewId;
				if (!bufferedByInstance.TryGetValue(view.GetInstanceID(), out bufferedViewId))
					bufferedViewId = view.viewID;
				record.Views.Add(view);
				record.OldViewIds.Add(view.viewID);
				record.BufferedViewIds.Add(bufferedViewId);
				MachineController machine = FindMachineForView(view);
				if (machine != null && seenMachines.Add(machine.GetInstanceID()))
				{
					NetworkView snapshotView = machine.GetComponent<NetworkView>();
					if (snapshotView == null || snapshotView.owner != player) snapshotView = view;
					List<BodyController> machineBodies;
					string collectionSource;
					string collectionReason;
					if (TryCollectMachineBodies(machine, out machineBodies, out collectionSource,
						out collectionReason))
					{
						NetworkViewID snapshotBufferedId;
						if (!bufferedByInstance.TryGetValue(snapshotView.GetInstanceID(),
							out snapshotBufferedId)) snapshotBufferedId = snapshotView.viewID;
						CaptureGhostSnapshot(record, snapshotView, snapshotBufferedId, machine,
							machineBodies, collectionSource);
						for (int bodyIndex = 0; bodyIndex < machineBodies.Count; bodyIndex++)
						{
							BodyController controller = machineBodies[bodyIndex];
							FreezeBody(record, controller.NFMPBACKJOJ, seenBodies);
							Rigidbody[] linked = controller.IFCNJLPLNGF;
							if (linked == null) continue;
							for (int linkedIndex = 0; linkedIndex < linked.Length; linkedIndex++)
								FreezeBody(record, linked[linkedIndex], seenBodies);
						}
					}
					else
						Log("SERVER_GHOST_BODY_COLLECTION_FAILED token=" + TokenLabel(record.Token)
							+ " view=" + IdLabel(snapshotView.viewID) + " reason=" + Clean(collectionReason));
				}
				Rigidbody[] bodies = view.GetComponentsInChildren<Rigidbody>(true);
				for (int bodyIndex = 0; bodyIndex < bodies.Length; bodyIndex++)
					FreezeBody(record, bodies[bodyIndex], seenBodies);
			}
		}

		internal static MachineController FindMachineForView(NetworkView view)
		{
			if (view == null) return null;
			Transform current = view.transform;
			while (current != null)
			{
				MachineController machine = current.GetComponent<MachineController>();
				if (machine != null) return machine;
				current = current.parent;
			}
			return view.GetComponentInChildren<MachineController>(true);
		}

		internal static bool TryCollectMachineBodies(MachineController machine,
			out List<BodyController> bodies, out string source, out string reason)
		{
			bodies = new List<BodyController>();
			source = "list=0,scene=0,unique=0";
			reason = null;
			if (machine == null) { reason = "machine-null"; return false; }
			HashSet<int> seen = new HashSet<int>();
			int listCount = machine.ILBAAENKMBL == null ? 0 : machine.ILBAAENKMBL.Count;
			if (machine.ILBAAENKMBL != null)
				for (int index = 0; index < machine.ILBAAENKMBL.Count; index++)
					AddMachineBody(machine.ILBAAENKMBL[index], bodies, seen);
			AddMachineBody(machine, bodies, seen);
			int sceneCount = 0;
			UnityEngine.Object[] found = UnityEngine.Object.FindObjectsOfType(typeof(BodyController));
			for (int index = 0; index < found.Length; index++)
			{
				BodyController candidate = found[index] as BodyController;
				if (candidate == null || (candidate != machine && candidate.CIPOPAGDJDE != machine)) continue;
				sceneCount++;
				AddMachineBody(candidate, bodies, seen);
			}
			bodies.Sort(delegate(BodyController left, BodyController right)
			{
				int compared = left.BBLGKLFBJGE.CompareTo(right.BBLGKLFBJGE);
				return compared != 0 ? compared : left.GetInstanceID().CompareTo(right.GetInstanceID());
			});
			source = "list=" + listCount + ",scene=" + sceneCount + ",unique=" + bodies.Count;
			if (bodies.Count == 0) { reason = "no-bodies-" + source; return false; }
			int previousIndex = -1;
			for (int index = 0; index < bodies.Count; index++)
			{
				BodyController body = bodies[index];
				if (body.BBLGKLFBJGE < 0) { reason = "invalid-body-index-" + body.BBLGKLFBJGE; return false; }
				if (index > 0 && body.BBLGKLFBJGE == previousIndex)
				{
					reason = "duplicate-body-index-" + body.BBLGKLFBJGE;
					return false;
				}
				if (body.NFMPBACKJOJ == null)
				{
					reason = "body-rigidbody-not-ready-" + body.BBLGKLFBJGE + "-" + source;
					return false;
				}
				previousIndex = body.BBLGKLFBJGE;
			}
			return true;
		}

		private static void AddMachineBody(BodyController body, List<BodyController> bodies,
			HashSet<int> seen)
		{
			if (body != null && seen.Add(body.GetInstanceID())) bodies.Add(body);
		}

		private static void FreezeBody(ResumeRecord record, Rigidbody body, HashSet<int> seenBodies)
		{
			if (body == null || !seenBodies.Add(body.GetInstanceID())) return;
			FrozenBody frozen = new FrozenBody();
			frozen.Body = body;
			frozen.WasKinematic = body.isKinematic;
			record.Bodies.Add(frozen);
			body.velocity = Vector3.zero;
			body.angularVelocity = Vector3.zero;
			body.isKinematic = true;
		}

		private static void CaptureGhostSnapshot(ResumeRecord record, NetworkView view,
			NetworkViewID bufferedViewId, MachineController machine,
			List<BodyController> machineBodies, string collectionSource)
		{
			string reason;
			LegacyGhostPoseBodyState[] bodies = new LegacyGhostPoseBodyState[machineBodies.Count];
			try
			{
				for (int index = 0; index < bodies.Length; index++)
				{
					BodyController body = machineBodies[index];
					Rigidbody rigidbody = body.NFMPBACKJOJ;
					LegacyGhostPoseBodyState state = new LegacyGhostPoseBodyState();
					state.Index = body.BBLGKLFBJGE;
					Vector3 position = rigidbody.position;
					Quaternion rotation = rigidbody.rotation;
					state.PositionX = position.x; state.PositionY = position.y; state.PositionZ = position.z;
					state.RotationX = rotation.x; state.RotationY = rotation.y;
					state.RotationZ = rotation.z; state.RotationW = rotation.w;
					state.Health = body.MEJNIODBGFI;
					state.BonusHealth = body.HPOMNJCEJIP;
					state.Broken = body.IsBroken();
					state.Suspended = body.EFCBCPOCOBB;
					state.SuspendFrames = Math.Max(0, body.OKGLHFEAEEP);
					bodies[index] = state;
				}
				string visualSource;
				LegacyGhostPoseVisualState[] visuals = CaptureGhostVisualStates(machine, out visualSource);
				string fingerprint = "view=" + IdLabel(view.viewID) + ";bodies=" + machineBodies.Count
					+ ";visuals=" + visuals.Length;
				LegacyGhostPoseSnapshot snapshot = new LegacyGhostPoseSnapshot();
				snapshot.SnapshotId = NextGhostSnapshotId();
				snapshot.Fingerprint = fingerprint;
				snapshot.Bodies = bodies;
				snapshot.Visuals = visuals;
				byte[] payload;
				if (!LegacyGhostPoseSnapshotCodec.TryEncode(snapshot, out payload, out reason))
				{
					Log("SERVER_GHOST_POSE_CAPTURE_SKIPPED token=" + TokenLabel(record.Token)
						+ " view=" + IdLabel(view.viewID) + " reason=" + Clean(reason));
					return;
				}
				GhostViewSnapshot captured = new GhostViewSnapshot();
				captured.View = view;
				captured.ViewId = view.viewID;
				captured.BufferedViewId = bufferedViewId;
				captured.SnapshotId = snapshot.SnapshotId;
				captured.BodyCount = bodies.Length;
				captured.VisualCount = visuals.Length;
				captured.Fingerprint = fingerprint;
				captured.Chunks = LegacyGhostPoseTransportCodec.Split(payload);
				record.GhostSnapshots.Add(captured);
				LegacyGhostPoseBodyState firstState = bodies[0];
				Log("SERVER_GHOST_POSE_CAPTURED token=" + TokenLabel(record.Token)
					+ " view=" + IdLabel(view.viewID) + " snapshot=" + captured.SnapshotId
					+ " bufferedView=" + IdLabel(bufferedViewId)
					+ " bodies=" + captured.BodyCount + " visuals=" + captured.VisualCount
					+ " chunks=" + captured.Chunks.Length
					+ " bytes=" + payload.Length + " fingerprint=" + fingerprint
					+ " bodySource=" + collectionSource + " visualSource=" + visualSource
					+ " firstIndex=" + firstState.Index
					+ " firstPos=" + firstState.PositionX.ToString("0.000", CultureInfo.InvariantCulture)
					+ "," + firstState.PositionY.ToString("0.000", CultureInfo.InvariantCulture)
					+ "," + firstState.PositionZ.ToString("0.000", CultureInfo.InvariantCulture)
					+ " firstHealth=" + firstState.Health.ToString("0.000", CultureInfo.InvariantCulture)
					+ " firstBonusHealth=" + firstState.BonusHealth.ToString("0.000", CultureInfo.InvariantCulture)
					+ " firstBroken=" + firstState.Broken + " firstSuspended=" + firstState.Suspended
					+ DescribeFirstVisual(visuals));
			}
			catch (Exception error)
			{
				Log("SERVER_GHOST_POSE_CAPTURE_SKIPPED token=" + TokenLabel(record.Token)
					+ " view=" + IdLabel(view.viewID) + " reason=capture-" + error.GetType().Name);
			}
		}

		private static LegacyGhostPoseVisualState[] CaptureGhostVisualStates(MachineController machine,
			out string source)
		{
			List<LegacyGhostPoseVisualState> states = new List<LegacyGhostPoseVisualState>();
			int listCount = machine == null || machine.CFECMHAACID == null ? 0 : machine.CFECMHAACID.Count;
			int auxiliaryCount = 0;
			if (machine != null && machine.CFECMHAACID != null)
			{
				for (int index = LegacyGhostPoseSnapshotCodec.RootVisualIndex;
					index < machine.CFECMHAACID.Count; index++)
				{
					NMLMDCCDFPN visual = index == LegacyGhostPoseSnapshotCodec.RootVisualIndex
						? (machine.BPKNDFJCENJ == null || machine.BPKNDFJCENJ.Count == 0
							? null : machine.BPKNDFJCENJ[0]) : machine.CFECMHAACID[index];
					if (visual == null) continue;
					bool primaryPresent = visual.NGLBLAGMBLN != null;
					bool secondaryPresent = visual.DBOFGDKGCBM != null;
					if (!primaryPresent && !secondaryPresent) continue;
					LegacyGhostPoseVisualState state = new LegacyGhostPoseVisualState();
					state.Index = index;
					state.PrimaryPresent = primaryPresent;
					if (primaryPresent) state.Primary = CaptureGhostTransform(visual.NGLBLAGMBLN.transform);
					state.SecondaryPresent = secondaryPresent;
					if (secondaryPresent) state.Secondary = CaptureGhostTransform(visual.DBOFGDKGCBM.transform);
					List<LegacyGhostPoseAuxiliaryState> auxiliaries = new List<LegacyGhostPoseAuxiliaryState>();
					if (visual.FLFCAMNNBIO != null)
					{
						for (int auxiliaryIndex = 0; auxiliaryIndex < visual.FLFCAMNNBIO.Count; auxiliaryIndex++)
						{
							Transform auxiliary = visual.FLFCAMNNBIO[auxiliaryIndex];
							if (auxiliary == null) continue;
							Vector3 scale = auxiliary.localScale;
							LegacyGhostPoseAuxiliaryState auxiliaryState = new LegacyGhostPoseAuxiliaryState();
							auxiliaryState.Index = auxiliaryIndex;
							auxiliaryState.ScaleX = scale.x;
							auxiliaryState.ScaleY = scale.y;
							auxiliaryState.ScaleZ = scale.z;
							auxiliaries.Add(auxiliaryState);
						}
					}
					state.Auxiliaries = auxiliaries.ToArray();
					auxiliaryCount += state.Auxiliaries.Length;
					states.Add(state);
				}
			}
			source = "list=" + listCount + ",captured=" + states.Count + ",auxiliaries=" + auxiliaryCount;
			return states.ToArray();
		}

		private static LegacyGhostPoseTransformState CaptureGhostTransform(Transform transform)
		{
			LegacyGhostPoseTransformState state = new LegacyGhostPoseTransformState();
			Vector3 position = transform.position;
			Quaternion rotation = transform.rotation;
			Vector3 scale = transform.localScale;
			state.PositionX = position.x; state.PositionY = position.y; state.PositionZ = position.z;
			state.RotationX = rotation.x; state.RotationY = rotation.y;
			state.RotationZ = rotation.z; state.RotationW = rotation.w;
			state.ScaleX = scale.x; state.ScaleY = scale.y; state.ScaleZ = scale.z;
			return state;
		}

		private static string DescribeFirstVisual(LegacyGhostPoseVisualState[] visuals)
		{
			if (visuals == null || visuals.Length == 0) return " firstVisual=none";
			LegacyGhostPoseVisualState first = visuals[0];
			LegacyGhostPoseTransformState state = first.PrimaryPresent ? first.Primary : first.Secondary;
			return " firstVisualIndex=" + first.Index + " firstVisualPos="
				+ state.PositionX.ToString("0.000", CultureInfo.InvariantCulture) + ","
				+ state.PositionY.ToString("0.000", CultureInfo.InvariantCulture) + ","
				+ state.PositionZ.ToString("0.000", CultureInfo.InvariantCulture)
				+ " firstVisualPrimary=" + first.PrimaryPresent + " firstVisualSecondary="
				+ first.SecondaryPresent + " firstVisualAuxiliaries=" + first.Auxiliaries.Length;
		}

		private static int NextGhostSnapshotId()
		{
			if (nextGhostSnapshotId <= 0 || nextGhostSnapshotId == int.MaxValue) nextGhostSnapshotId = 1;
			return nextGhostSnapshotId++;
		}

		private static void ReleaseFrozen(ResumeRecord record, string reason)
		{
			for (int i = 0; i < record.Bodies.Count; i++)
			{
				FrozenBody frozen = record.Bodies[i];
				try
				{
					if (frozen.Body == null) continue;
					frozen.Body.velocity = Vector3.zero;
					frozen.Body.angularVelocity = Vector3.zero;
					frozen.Body.isKinematic = frozen.WasKinematic;
				}
				catch { }
			}
			Log("SERVER_GHOST_RELEASED token=" + TokenLabel(record.Token)
				+ " bodies=" + record.Bodies.Count + " reason=" + reason);
			record.Bodies.Clear();
		}

		private static void SendActiveRebinds(LegacyTransientReconnectController source, NetworkPlayer target)
		{
			int sent = 0;
			foreach (ResumeRecord record in records.Values)
			{
				if (!record.Active || record.Retained || record.RebindInProgress
					|| record.Views.Count != record.BufferedViewIds.Count) continue;
				for (int i = 0; i < record.Views.Count; i++)
				{
					NetworkView view = record.Views[i];
					NetworkViewID oldId = record.BufferedViewIds[i];
					if (view == null || oldId == NetworkViewID.unassigned || oldId == view.viewID) continue;
					if (source.SendRebind(target, oldId, view.viewID, "hello-replay")) sent++;
				}
			}
			if (sent > 0)
				Log("SERVER_REBIND_REPLAY target=" + PlayerLabel(target) + " mappings=" + sent);
		}

		private static void SendRetainedGhostSnapshots(LegacyTransientReconnectController source,
			NetworkPlayer target, string targetToken)
		{
			int snapshots = 0;
			int chunks = 0;
			int rebinds = 0;
			foreach (ResumeRecord record in records.Values)
			{
				if (!record.Retained || record.Token == targetToken) continue;
				for (int index = 0; index < record.GhostSnapshots.Count; index++)
				{
					GhostViewSnapshot snapshot = record.GhostSnapshots[index];
					if (snapshot == null || snapshot.View == null || snapshot.Chunks == null) continue;
					NetworkViewID currentId = snapshot.View.viewID;
					if (currentId == NetworkViewID.unassigned) continue;
					if (snapshot.BufferedViewId != NetworkViewID.unassigned
						&& snapshot.BufferedViewId != currentId
						&& source.SendRebind(target, snapshot.BufferedViewId, currentId,
							"retained-ghost-replay")) rebinds++;
					if (!source.SendGhostPose(target, currentId, snapshot.SnapshotId,
						snapshot.BodyCount, snapshot.VisualCount, snapshot.Chunks)) continue;
					snapshots++;
					chunks += snapshot.Chunks.Length;
				}
			}
			Log("SERVER_GHOST_POSE_REPLAY target=" + PlayerLabel(target) + " snapshots="
				+ snapshots + " chunks=" + chunks + " rebinds=" + rebinds);
		}

		internal static NetworkView FindViewById(NetworkViewID viewId)
		{
			if (viewId == NetworkViewID.unassigned) return null;
			UnityEngine.Object[] found;
			try { found = UnityEngine.Object.FindObjectsOfType(typeof(NetworkView)); }
			catch { return null; }
			for (int i = 0; i < found.Length; i++)
			{
				NetworkView view = found[i] as NetworkView;
				if (view != null && view.viewID == viewId) return view;
			}
			return null;
		}

		internal static void CleanupExpired()
		{
			if (!Network.isServer || records.Count == 0) return;
			float now = Time.realtimeSinceStartup;
			List<string> expired = null;
			List<ResumeRecord> restoreTimeouts = null;
			foreach (KeyValuePair<string, ResumeRecord> item in records)
			{
				ResumeRecord record = item.Value;
				if (record.CrashConsentPending && record.CrashConsentUntil > 0f
					&& now > record.CrashConsentUntil)
				{
					if (restoreTimeouts == null) restoreTimeouts = new List<ResumeRecord>();
					restoreTimeouts.Add(record);
					continue;
				}
				if (record.CrashRestoreInProgress && record.CrashRestoreUntil > 0f
					&& now > record.CrashRestoreUntil)
				{
					if (restoreTimeouts == null) restoreTimeouts = new List<ResumeRecord>();
					restoreTimeouts.Add(record);
					continue;
				}
				if (record.PendingPlayer != default(NetworkPlayer) && record.PendingUntil > 0f
					&& now > record.PendingUntil)
				{
					NetworkPlayer stalled = record.PendingPlayer;
					record.PendingPlayer = default(NetworkPlayer);
					record.PendingUntil = 0f;
					try { Network.SetSendingEnabled(stalled, MachineNetworkGroup, true); }
					catch { }
					for (int i = 0; i < record.Views.Count; i++)
					{
						try { if (record.Views[i] != null) record.Views[i].SetScope(stalled, true); }
						catch { }
					}
					Log("SERVER_RECONNECT_GATE_TIMEOUT token=" + TokenLabel(record.Token)
						+ " player=" + PlayerLabel(stalled) + " action=close-and-retry");
					try { Network.CloseConnection(stalled, false); }
					catch { }
				}
				if (!record.Retained || now <= record.RetainUntil) continue;
				DestroyRetained(record, "retention-expired");
				if (expired == null) expired = new List<string>();
				expired.Add(item.Key);
			}
			if (expired != null)
				for (int i = 0; i < expired.Count; i++) records.Remove(expired[i]);
			if (restoreTimeouts == null) return;
			for (int i = 0; i < restoreTimeouts.Count; i++)
			{
				ResumeRecord record = restoreTimeouts[i];
				NetworkView freshView = record.CrashFreshView;
				NetworkPlayer player = record.Player;
				string playerName = record.Name;
				LegacyTransientReconnectController source = record.Source;
				string reason = record.CrashConsentPending
					? "restore-consent-timeout" : "restore-timeout";
				if (source != null && freshView != null)
					PromoteRetainedToCrashFallback(source, record, playerName, player, freshView,
						reason);
				else
				{
					DestroyRetained(record, "crash-" + reason);
					records.Remove(record.Token);
				}
			}
		}

		private static void DestroyRetained(ResumeRecord record, string reason)
		{
			NetworkPlayer owner = record.PreviousPlayer != default(NetworkPlayer)
				? record.PreviousPlayer : record.Player;
			int trackedViewsDestroyed = DestroyTrackedRetainedViews(record, reason);
			// Rebound peer indices may already belong to another connected player.
			// Tracked identities include the original Instantiate; never broaden
			// their retirement to every object buffered by an old peer index.
			if (trackedViewsDestroyed == 0)
			{
			try { Network.RemoveRPCs(owner); }
			catch (Exception error) { Log("SERVER_EXPIRE_REMOVE_RPCS_FAILED type=" + error.GetType().Name); }
			try { Network.DestroyPlayerObjects(owner); }
			catch (Exception error) { Log("SERVER_EXPIRE_DESTROY_FAILED type=" + error.GetType().Name); }
			}
			Log("SERVER_GHOST_DESTROYED token=" + TokenLabel(record.Token) + " owner=" + PlayerLabel(owner)
				+ " trackedViews=" + trackedViewsDestroyed + " reason=" + reason);
			record.Retained = false;
			record.RebindInProgress = false;
			record.Views.Clear();
			record.OldViewIds.Clear();
			record.BufferedViewIds.Clear();
			record.Bodies.Clear();
			record.GhostSnapshots.Clear();
			record.Claimed = null;
			ResetCrashRestore(record);
		}

		private static void ReplayCrashRetainedIds(LegacyTransientReconnectController source,
			ResumeRecord record, NetworkPlayer target)
		{
			// A restarted peer replays the original Instantiate ID, whereas the
			// server may have rebound that object several times before the crash.
			for (int index = 0; index < record.Views.Count; index++)
			{
				NetworkView view = record.Views[index];
				if (view == null || index >= record.BufferedViewIds.Count) continue;
				NetworkViewID buffered = record.BufferedViewIds[index];
				if (buffered != NetworkViewID.unassigned && buffered != view.viewID)
					source.SendRebind(target, buffered, view.viewID, "crash-retained-id-before-retirement");
			}
		}

		private static int DestroyTrackedRetainedViews(ResumeRecord record, string reason)
		{
			int destroyed = 0;
			for (int index = 0; index < record.Views.Count; index++)
			{
				NetworkView view = record.Views[index];
				NetworkViewID liveId = view == null ? NetworkViewID.unassigned : view.viewID;
				GameObject networkObject = view == null ? null : view.gameObject;
				NetworkViewID bufferedId = index < record.BufferedViewIds.Count
					? record.BufferedViewIds[index] : liveId;
				NetworkViewID previousId = index < record.OldViewIds.Count
					? record.OldViewIds[index] : liveId;
				List<NetworkViewID> retirementIds = LegacyRetirementIds.Collect(bufferedId,
					previousId, liveId, NetworkViewID.unassigned);
				LegacyPlayerList.Remove(FindMachineForView(view), "tracked-retirement");
				if (record.Source != null) record.Source.BroadcastRetire(bufferedId, liveId);
				if (record.Source != null && previousId != bufferedId && previousId != liveId
					&& previousId != NetworkViewID.unassigned)
					record.Source.BroadcastRetire(previousId, liveId);
				bool networkDestroyed = false;
				try
				{
					if (networkObject != null)
					{

						Network.Destroy(networkObject);
						networkDestroyed = true;
					}
				}
				catch (Exception error)
				{
					Log("SERVER_GHOST_TRACKED_NETWORK_DESTROY_FAILED token=" + TokenLabel(record.Token)
						+ " view=" + IdLabel(liveId) + " type=" + error.GetType().Name
						+ " message=" + Clean(error.Message));
				}
				bool buffersRemoved = true;
				foreach (NetworkViewID retirementId in retirementIds)
					buffersRemoved = RemoveViewBuffer(retirementId, "tracked-retirement", record.Token) && buffersRemoved;
				try { if (networkObject != null) UnityEngine.Object.Destroy(networkObject); }
				catch (Exception error)
				{
					Log("SERVER_GHOST_TRACKED_LOCAL_DESTROY_FAILED token=" + TokenLabel(record.Token)
						+ " view=" + IdLabel(liveId) + " type=" + error.GetType().Name
						+ " message=" + Clean(error.Message));
				}
				destroyed++;
				Log("SERVER_GHOST_TRACKED_DESTROY token=" + TokenLabel(record.Token)
					+ " view=" + IdLabel(liveId) + " network=" + networkDestroyed
					+ " original=" + IdLabel(bufferedId) + " previous=" + IdLabel(previousId)
					+ " distinctBuffers=" + retirementIds.Count + " buffersRemoved=" + buffersRemoved
					+ " reason=" + Clean(reason));
			}
			return destroyed;
		}

		private static void Broadcast(LegacyTransientReconnectController source, string playerName, bool ownerReused)
		{
			SendSystem(source, "[MPatcher] " + playerName + (ownerReused
				? ": connection restored; machine control resumed."
				: ": connection restored; machine synchronization in progress."));
		}

		private static void SendSystem(LegacyTransientReconnectController source, string message)
		{
			try
			{
				source.SessionView.RPC("RPC_SysMsg", RPCMode.All, message);
				Log("SERVER_STATUS_SENT message=" + Clean(message));
			}
			catch (Exception error) { Log("SERVER_STATUS_FAILED type=" + error.GetType().Name); }
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

		internal static string TokenLabel(string token)
		{
			if (!ValidToken(token)) return "invalid";
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

		private static string SafeName(string value)
		{
			value = Clean(value).Replace("<", "(").Replace(">", ")").Trim();
			if (value.Length == 0) value = "Player";
			if (value.Length > 40) value = value.Substring(0, 40);
			return value;
		}

		internal static string PlayerLabel(NetworkPlayer player)
		{
			try { return Clean(player.ToString()) + "/" + Clean(player.guid); }
			catch { return "unavailable"; }
		}

		internal static string PlayerGuid(NetworkPlayer player)
		{
			try { return Clean(player.guid); }
			catch { return ""; }
		}

		internal static string IdLabel(NetworkViewID viewId)
		{
			try { return Clean(viewId.ToString()); }
			catch { return "unavailable"; }
		}

		private static string Quote(string value) { return "\"" + Clean(value).Replace("\"", "'") + "\""; }

		internal static void Log(string message)
		{
			try { mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng("[LEGACY-RESUME] " + message); }
			catch { }
		}

		internal sealed class ResumeRecord
		{
			internal string Token;
			internal string Name;
			internal string PlayerGuid;
			internal NetworkPlayer Player;
			internal NetworkPlayer PreviousPlayer;
			internal NetworkPlayer PendingPlayer;
			internal bool Active;
			internal bool Retained;
			internal bool Voluntary;
			internal bool RebindInProgress;
			internal bool CrashRestoreInProgress;
			internal bool CrashConsentPending;
			internal bool CrashOwnerApplied;
			internal bool CrashServerApplied;
			internal float RegisteredAt;
			internal float LastHelloAt;
			internal float RetainUntil;
			internal float PendingUntil;
			internal float CrashRestoreUntil;
			internal float CrashConsentUntil;
			internal string CrashExpectedConstructionFingerprint;
			internal int ResumeCount;
			internal int CrashSnapshotId;
			internal int CrashBodyCount;
			internal int CrashVisualCount;
			internal bool[] Claimed;
			internal LegacyTransientReconnectController Source;
			internal NetworkView CrashFreshView;
			internal NetworkViewID CrashFreshViewId;
			internal byte[][] CrashChunks;
			internal readonly List<NetworkView> Views = new List<NetworkView>();
			internal readonly List<NetworkViewID> OldViewIds = new List<NetworkViewID>();
			internal readonly List<NetworkViewID> BufferedViewIds = new List<NetworkViewID>();
			internal readonly List<FrozenBody> Bodies = new List<FrozenBody>();
			internal readonly List<GhostViewSnapshot> GhostSnapshots = new List<GhostViewSnapshot>();
		}

		internal sealed class FrozenBody
		{
			internal Rigidbody Body;
			internal bool WasKinematic;
		}

		internal sealed class GhostViewSnapshot
		{
			internal NetworkView View;
			internal NetworkViewID ViewId;
			internal NetworkViewID BufferedViewId;
			internal int SnapshotId;
			internal int BodyCount;
			internal int VisualCount;
			internal string Fingerprint;
			internal byte[][] Chunks;
		}
	}

	internal static partial class LegacyReconnectTarget
	{
		private static string guid;
		private static string[] addresses;
		private static int port;
		private static bool useNat;
		private static bool valid;

		internal static bool HasTarget { get { return valid; } }
		internal static string Description
		{
			get
			{
				if (!valid) return "unavailable";
				return useNat ? "guid:" + LegacyTransientReconnect.Clean(guid)
					: "direct:" + LegacyTransientReconnect.Clean(addresses == null ? "" : string.Join(",", addresses)) + ":" + port;
			}
		}

		internal static void Remember(HostData host, string requestedGuid, bool nat)
		{
			string[] capturedAddresses = null;
			int capturedPort = 0;
			string capturedGuid = requestedGuid;
			if (host != null)
			{
				if (!string.IsNullOrEmpty(host.guid)) capturedGuid = host.guid;
				capturedPort = host.port;
				if (host.ip != null && host.ip.Length > 0)
				{
					capturedAddresses = new string[host.ip.Length];
					Array.Copy(host.ip, capturedAddresses, host.ip.Length);
				}
			}
			bool canUseNat = nat && !string.IsNullOrEmpty(capturedGuid);
			bool canUseDirect = !nat && capturedAddresses != null && capturedAddresses.Length > 0 && capturedPort > 0;
			if (!canUseNat && !canUseDirect)
			{
				valid = false;
				LegacyTransientReconnect.Log("TARGET_REJECTED useNat=" + nat + " guid="
					+ LegacyTransientReconnect.Clean(capturedGuid) + " addresses="
					+ (capturedAddresses == null ? 0 : capturedAddresses.Length) + " port=" + capturedPort);
				return;
			}
			guid = capturedGuid;
			addresses = capturedAddresses;
			port = capturedPort;
			useNat = nat;
			valid = true;
			ConfigureDiscovery(host);
			LegacyTransientReconnect.Log("TARGET_SAVED target=" + Description);
		}

		internal static NetworkConnectionError Connect()
		{
			if (!valid) return NetworkConnectionError.EmptyConnectTarget;
			return useNat ? Network.Connect(guid) : Network.Connect(addresses, port);
		}
	}

	internal sealed partial class LegacyTransientReconnectController : MonoBehaviour
	{
		private const float HelloInterval = 2f;
		private const float FirstReconnectDelaySeconds = 10f;
		private const float RebindDrainSeconds = 1f;
		private const float RecoveryTimeoutSeconds = 300f;
		private const float NativeExitFallbackSeconds = 2f;
		private const float ReconnectGuidelineGraceSeconds = 5f;
		private const float CrashConsentCompatibilitySeconds = 5f;
		private const float CrashProtocolFallbackSeconds = 30f;
		private const float OwnerCheckpointIntervalSeconds = 0.25f;
		private const int MachineNetworkGroup = 1;
		private const string TokenDirectory = "_mpatcher";
		private const string TokenFile = "legacy-recovery-token.txt";
		private const string TestDropFile = "legacy-transient-test-drop.request";
		private static readonly FieldInfo NativeStructureReadyField =
			AccessTools.Field(typeof(MachineSerializer), "NNKNLKCBAPN");
		private static readonly FieldInfo OwnerPreviousCenterField =
			AccessTools.Field(typeof(MachineController), "EFLFFGGHIEG");
		private static readonly FieldInfo OwnerPreviousRotationField =
			AccessTools.Field(typeof(MachineController), "FNMCPFGJECL");
		private static readonly FieldInfo OwnerSmoothedMotionField =
			AccessTools.Field(typeof(MachineController), "BGHGIMDKLPC");
		private MachineController tracedOwner;
		private LegacyMachineRecoverySnapshot tracedOwnerSnapshot;
		private float ownerTraceUntil;
		private float nextOwnerTrace;
		private int ownerTraceSamples;
		private static string cachedToken;
		private Game game;
		private NetworkView sessionView;
		private bool clientRole;
		private bool serverRole;
		private bool initialized;
		private bool voluntary;
		private bool registered;
		private bool recovering;
		private bool connecting;
		private bool connectedCallbackSeen;
		private bool machineTrafficGated;
		private bool rebindAuthorized;
		private bool terminatingRecovery;
		private bool crashResumePending;
		private bool crashStatusSeen;
		private bool crashTrafficGated;
		private bool crashFallbackHello;
		private bool crashUsingV3;
		private bool crashConsentDecisionSent;
		private bool crashConsentAccepted;
		private bool crashCheckpointIdentityAttempted;
		private string crashCheckpointIdentity;
		private string crashCheckpointIdentityReason;
		private string crashFreshOwnerIdentity;
		private int suppressedNativeMachinePackets;
		private int suppressedOutgoingRpcs;
		private int suppressedReconnectGuidelines;
		private int emptyPingGuards;
		private int attempts;
		private float nextHello;
		private float nextRetry;
		private float recoveryBeganAt;
		private float recoveryDeadline;
		private float rebindNotBefore;
		private float nativeExitFallbackAt;
		private float suppressReconnectGuidelineUntil;
		private float connectDeadline;
		private float nextClientStateLog;
		private float nextServerCleanup;
		private float nextTestDropCheck;
		private float testDropArmUntil;
		private float crashProtocolFallbackAt;
		private float crashV3FallbackAt;
		private float nextOwnerCheckpoint;
		private float nextOwnerCheckpointLog;
		private float nextOwnerFingerprintRetry;
		private MachineController ownerFingerprintAttemptMachine;
		private NetworkPlayer lastLocalPlayer;
		private NetworkPlayer pendingTestDropPlayer;
		private Vector3 lossPosition;
		private bool haveLossPosition;
		private bool testDropArmed;
		private bool testDropPending;
		private string token;
		private string pendingTestDropToken;
		private NetworkViewID crashFreshViewId;
		private MachineController crashFreshMachine;
		private LegacyCrashResumeConsentUi crashConsentUi;
		internal bool RecoveryInProgress { get { return recovering || crashResumePending; } }
		private readonly LegacyRestoreDeliveryLedger<NetworkViewID> restoreDeliveries =
			new LegacyRestoreDeliveryLedger<NetworkViewID>();
		private int ownerCheckpointSequence;
		private MachineController ownerCheckpointMachine;
		private LegacyMachineControlRecovery ownerControls;
		private LegacyMachineControlRecovery tracedOwnerControls;
		private float nextOwnerControlRetry;
		private bool ownerControlSyncPending;
		private string ownerCheckpointFingerprint;
		private readonly List<ClientViewClaim> clientClaims = new List<ClientViewClaim>();
		private readonly List<PendingPeerRebind> pendingPeerRebinds = new List<PendingPeerRebind>();
		private readonly List<PendingGhostPose> pendingGhostPoses = new List<PendingGhostPose>();
		private readonly List<PendingRemotePresentationRefresh> pendingRemotePresentationRefreshes =
			new List<PendingRemotePresentationRefresh>();
		private readonly List<ClientGhostFreeze> clientGhostFreezes = new List<ClientGhostFreeze>();

		private sealed class ClientViewClaim
		{
			internal NetworkView View;
			internal NetworkViewID OldId;
			internal NetworkViewID NewId;
			internal bool Prepared;
			internal bool RetireRequested;
		}

		private sealed class PendingPeerRebind
		{
			internal NetworkViewID OldId;
			internal NetworkViewID NewId;
			internal float NextAttempt;
			internal float Expires;
		}

		private sealed class PendingGhostPose
		{
			internal NetworkViewID ViewId;
			internal int SnapshotId;
			internal int BodyCount;
			internal byte[][] Chunks;
			internal int ReceivedChunks;
			internal LegacyGhostPoseSnapshot Snapshot;
			internal float NextAttempt;
			internal float NextLog;
			internal float Expires;
			internal string WaitReason;
			internal bool LiveRestore;
			internal string RestoreToken;
			internal LegacyMachineRecoverySnapshot OwnerCheckpoint;
			internal long OwnerCapturedUtcTicks;
			internal int OwnerCheckpointBytes;
			internal int OwnerRestoreAttempts;
			internal float OwnerRestoreStarted;
			internal int OwnerRestoreStartFrame;
			internal double OwnerPreparationMilliseconds;
			internal MachineController TargetMachine;
		}

		private sealed class PendingRemotePresentationRefresh
		{
			internal NetworkViewID ViewId;
			internal int SnapshotId;
			internal LegacyGhostPoseSnapshot Snapshot;
			internal int Attempt;
			internal float NextAttempt;
			internal float NextWaitLog;
			internal float Expires;
			internal bool EarlyRegistrationPending;
			internal bool AwaitNativeRegistration;
			internal bool NativeNameObserved;
			internal bool NativeRegistrationReady;
		}

		private sealed class ClientGhostFreeze
		{
			internal NetworkViewID ViewId;
			internal int SnapshotId;
			internal NetworkView View;
			internal MachineController Machine;
			internal readonly List<ClientFrozenBody> Bodies = new List<ClientFrozenBody>();
			internal readonly List<ClientGhostBodyPose> Poses = new List<ClientGhostBodyPose>();
			internal readonly List<ClientGhostVisualPose> VisualPoses = new List<ClientGhostVisualPose>();
			internal int CorrectionCount;
			internal float NextCorrectionLog;
		}

		private sealed class ClientGhostBodyPose
		{
			internal BodyController Body;
			internal LegacyGhostPoseBodyState State;
		}

		private sealed class ClientGhostVisualPose
		{
			internal NMLMDCCDFPN Visual;
			internal LegacyGhostPoseVisualState State;
		}

		private sealed class ClientFrozenBody
		{
			internal Rigidbody Body;
			internal bool WasKinematic;
		}

		internal Game Game { get { return game; } }
		internal NetworkView SessionView { get { return sessionView; } }
		internal bool CanResumeClient { get { return initialized && clientRole && !voluntary; } }
		internal bool KeepLiveHostScene { get { return initialized && serverRole && !voluntary; } }
		internal bool ShouldSuppressNativeMachineCatchUp(Component component, string source)
		{
			if (!initialized || !clientRole || component == null) return false;
			NetworkView componentView = component.GetComponent<NetworkView>();
			MachineController componentMachine = component as MachineController;
			if (componentMachine == null) componentMachine = component.GetComponent<MachineController>();
			if (recovering && game != null && game.FICMBCLEFDL != null)
			{
				MachineController localMachine = game.FICMBCLEFDL;
				MachineSerializer localSerializer = localMachine.GetComponent<MachineSerializer>();
				if (component == localMachine || component == localSerializer)
				{
					suppressedNativeMachinePackets++;
					if (suppressedNativeMachinePackets <= 12)
						LegacyTransientReconnect.Log("CLIENT_NATIVE_MACHINE_PACKET_SUPPRESSED source=" + source
							+ " count=" + suppressedNativeMachinePackets + " view="
							+ ViewLabel(componentView));
					return true;
				}
			}
			for (int index = 0; index < clientGhostFreezes.Count; index++)
			{
				ClientGhostFreeze ghost = clientGhostFreezes[index];
				if (ghost.Machine != componentMachine && ghost.View != componentView
					&& (componentView == null || ghost.ViewId != componentView.viewID)) continue;
				suppressedNativeMachinePackets++;
				if (suppressedNativeMachinePackets <= 12)
					LegacyTransientReconnect.Log("CLIENT_GHOST_NATIVE_PACKET_SUPPRESSED source=" + source
						+ " count=" + suppressedNativeMachinePackets + " view=" + ViewLabel(componentView)
						+ " snapshot=" + ghost.SnapshotId);
				return true;
			}
			if (ShouldSuppressMigrationMachineReplay(component as MachineSerializer, source)) return true;
			return false;
		}

		internal bool ShouldSuppressNativeMachineSerialize(MachineSerializer serializer, string source)
		{
			return ShouldSuppressNativeMachineCatchUp(serializer, source);
		}

		internal bool ShouldSuppressOutgoingRpc(NetworkView view, string name)
		{
			if (!initialized || !clientRole || !recovering) return false;
			bool transportReady;
			try { transportReady = Network.isClient || Network.isServer; }
			catch { transportReady = false; }
			bool controlView = view != null && view == sessionView;
			if (transportReady && controlView) return false;
			suppressedOutgoingRpcs++;
			if (suppressedOutgoingRpcs <= 20)
				LegacyTransientReconnect.Log("CLIENT_OUTGOING_RPC_SUPPRESSED name="
					+ LegacyTransientReconnect.Clean(name) + " count=" + suppressedOutgoingRpcs
					+ " transportReady=" + transportReady + " controlView=" + controlView
					+ " view=" + ViewLabel(view));
			return true;
		}

		internal bool ShouldSuppressReconnectGuideline(string rpcName, string text)
		{
			if (!initialized || !clientRole) return false;
			float now = Time.realtimeSinceStartup;
			bool grace = suppressReconnectGuidelineUntil > 0f && now <= suppressReconnectGuidelineUntil;
			if (!recovering && !crashResumePending && !grace) return false;
			suppressedReconnectGuidelines++;
			LegacyTransientReconnect.Log("CLIENT_RECONNECT_GUIDELINE_SUPPRESSED rpc="
				+ LegacyTransientReconnect.Clean(rpcName) + " characters=" + (text == null ? 0 : text.Length)
				+ " recovering=" + recovering + " crashResume=" + crashResumePending + " grace=" + grace
				+ " count=" + suppressedReconnectGuidelines);
			return true;
		}

		internal bool TryRequestReboundViewRetire(NetworkView view)
		{
			if (!initialized || !clientRole || recovering || !registered || view == null
				|| sessionView == null || !Network.isClient || !LegacyTransientReconnect.ValidToken(token)) return false;
			for (int i = 0; i < clientClaims.Count; i++)
			{
				ClientViewClaim claim = clientClaims[i];
				if (!claim.Prepared || claim.View != view || claim.NewId != view.viewID
					|| claim.OldId == NetworkViewID.unassigned) continue;
				if (claim.RetireRequested) return true;
				try
				{
					sessionView.RPC("MPatcherResumeRetireV2", RPCMode.Server, 2, token,
						claim.OldId, claim.NewId);
					claim.RetireRequested = true;
					LegacyTransientReconnect.Log("CLIENT_REBOUND_RETIRE_REQUESTED index=" + i + " old="
						+ LegacyTransientReconnect.IdLabel(claim.OldId) + " new="
						+ LegacyTransientReconnect.IdLabel(claim.NewId));
					return true;
				}
				catch (Exception error)
				{
					LegacyTransientReconnect.Log("CLIENT_REBOUND_RETIRE_REQUEST_FAILED index=" + i
						+ " type=" + error.GetType().Name + " message="
						+ LegacyTransientReconnect.Clean(error.Message));
					return false;
				}
			}
			return false;
		}

		internal void ObserveEmptyPingGuard()
		{
			if (!initialized || !clientRole || !recovering) return;
			emptyPingGuards++;
			if (emptyPingGuards == 1)
				LegacyTransientReconnect.Log("CLIENT_EMPTY_CONNECTION_PING_GUARDED recovery=true result=0");
		}

		internal bool ShouldResumeDisconnect(NetworkDisconnection reason)
		{
			return CanResumeClient && (MigrationArmed || reason == NetworkDisconnection.LostConnection
				|| (testDropArmed && Time.realtimeSinceStartup <= testDropArmUntil));
		}

		internal static bool ShouldPreserveCrashMarkerForRetry(bool pending, bool voluntaryExit)
		{
			return pending && !voluntaryExit;
		}

		internal void Initialize(Game value, bool wasClient, bool wasServer)
		{
			CloseCrashRestoreConsent("initialize");
			game = value;
			sessionView = value == null ? null : value.GetComponent<NetworkView>();
			clientRole = wasClient;
			serverRole = wasServer;
			initialized = true;
			voluntary = false;
			registered = false;
			recovering = false;
			connecting = false;
			connectedCallbackSeen = false;
			machineTrafficGated = false;
			rebindAuthorized = false;
			terminatingRecovery = false;
			crashResumePending = false;
			crashStatusSeen = false;
			crashTrafficGated = false;
			crashFallbackHello = false;
			crashUsingV3 = false;
			crashConsentDecisionSent = false;
			crashConsentAccepted = false;
			crashCheckpointIdentityAttempted = false;
			crashCheckpointIdentity = null;
			crashCheckpointIdentityReason = null;
			crashFreshOwnerIdentity = null;
			suppressedNativeMachinePackets = 0;
			suppressedOutgoingRpcs = 0;
			suppressedReconnectGuidelines = 0;
			emptyPingGuards = 0;
			attempts = 0;
			clientClaims.Clear();
			pendingPeerRebinds.Clear();
			ReleaseAllClientGhosts("initialize");
			pendingGhostPoses.Clear();
			pendingRemotePresentationRefreshes.Clear();
			nextHello = Time.realtimeSinceStartup + 0.5f;
			restoreDeliveries.Clear();
			crashFreshMachine = null;
			recoveryBeganAt = -100f;
			recoveryDeadline = float.MaxValue;
			rebindNotBefore = float.MaxValue;
			nativeExitFallbackAt = float.MaxValue;
			suppressReconnectGuidelineUntil = 0f;
			nextServerCleanup = Time.realtimeSinceStartup + 5f;
			nextTestDropCheck = Time.realtimeSinceStartup + 0.5f;
			nextOwnerCheckpoint = Time.realtimeSinceStartup + OwnerCheckpointIntervalSeconds;
			nextOwnerCheckpointLog = Time.realtimeSinceStartup;
			ownerCheckpointSequence = 0;
			ownerCheckpointMachine = null;
			ownerControls = null;
			tracedOwnerControls = null;
			nextOwnerControlRetry = 0f;
			ownerControlSyncPending = false;
			ownerCheckpointFingerprint = null;
			nextOwnerFingerprintRetry = 0f;
			ownerFingerprintAttemptMachine = null;
			testDropArmed = false;
			testDropPending = false;
			crashFreshViewId = NetworkViewID.unassigned;
			token = GetToken();
			string markerReason = "server-role";
			if (clientRole)
			{
				crashResumePending = LegacyCrashResumeMarker.Matches(token,
					LegacyReconnectTarget.Description, out markerReason);
				crashProtocolFallbackAt = float.MaxValue;
				crashV3FallbackAt = float.MaxValue;
				if (crashResumePending) suppressReconnectGuidelineUntil = float.MaxValue;
				LegacyTransientReconnect.Log("CLIENT_CRASH_MARKER_CHECK token="
					+ LegacyTransientReconnect.TokenLabel(token) + " target="
					+ LegacyReconnectTarget.Description + " matched=" + crashResumePending
					+ " reason=" + LegacyTransientReconnect.Clean(markerReason));
			}
			else
			{
				crashProtocolFallbackAt = float.MaxValue;
				crashV3FallbackAt = float.MaxValue;
			}
			if (clientRole)
			{
				try { lastLocalPlayer = Network.player; }
				catch { lastLocalPlayer = default(NetworkPlayer); }
			}
			LegacyTransientReconnect.Log("CONTROLLER_READY clientRole=" + clientRole + " serverRole=" + serverRole
				+ " gameView=" + ViewLabel(sessionView) + " gameViewGroup="
				+ (sessionView == null ? -1 : sessionView.group) + " target=" + LegacyReconnectTarget.Description
				+ " token=" + LegacyTransientReconnect.TokenLabel(token)
				+ " crashResumePending=" + crashResumePending);
		}

		private void Update()
		{
			if (!initialized) return;
			float now = Time.realtimeSinceStartup;
			MigrationTick(now);
			// A manual Exit already switched Legacy to Offline. Finish its scene exit
			// even when no transport callback exists (Disconnected/Connecting).
			if (terminatingRecovery)
			{
				if (now >= nativeExitFallbackAt) InvokeNativeDisconnect("disconnect-callback-timeout");
				return;
			}
			if (voluntary || !LegacyTransientReconnect.IsLegacy()) return;
			ApplyPendingPeerRebinds(now);
			TraceOwnerPose(now);
			ApplyPendingRemoteCrashPresentationRefreshes(now);
			CleanupClientGhosts();
			if (serverRole && Network.isServer && !LegacyHostMigration.Active && now >= nextServerCleanup)
			{
				nextServerCleanup = now + 5f;
				LegacyTransientReconnect.CleanupExpired();
			}
			if (serverRole && Network.isServer && testDropPending) ExecuteTestDrop();
			if (!clientRole || voluntary) return;
			if (recovering) LegacyReconnectTarget.TickDiscovery(now);
			if (recovering && now >= recoveryDeadline)
			{
				AbortRecovery("server-unavailable-timeout");
				return;
			}

			if (Network.isClient)
			{
				lastLocalPlayer = Network.player;
				if (crashResumePending && !crashStatusSeen && !crashUsingV3
					&& now >= crashV3FallbackAt)
				{
					crashUsingV3 = true;
					crashV3FallbackAt = float.MaxValue;
					nextHello = now;
					LegacyTransientReconnect.Log("CLIENT_CRASH_PROTOCOL_FALLBACK action=v3-hello"
						+ " reason=no-v4-status-after-hello timeoutSeconds=5");
				}
				if (crashResumePending && !crashStatusSeen && now >= crashProtocolFallbackAt)
				{
					CloseCrashRestoreConsent("protocol-fallback");
					crashResumePending = false;
					crashFallbackHello = true;
					crashFreshMachine = null;
					crashProtocolFallbackAt = float.MaxValue;
					suppressReconnectGuidelineUntil = now + ReconnectGuidelineGraceSeconds;
					ReleaseCrashResumeTraffic();
					registered = false;
					nextHello = now;
					LegacyTransientReconnect.Log("CLIENT_CRASH_PROTOCOL_FALLBACK action=fresh-v2-hello"
						+ " reason=no-v3-status-after-hello timeoutSeconds=30");
				}
				if (recovering && rebindAuthorized && now >= rebindNotBefore)
				{
					rebindAuthorized = false;
					PrepareClientRebind();
					SendClaims();
				}
				if (!recovering && !crashResumePending && registered && now >= nextTestDropCheck)
				{
					nextTestDropCheck = now + 0.25f;
					CheckTestDropRequest();
				}
				if (now >= nextHello && (!registered || recovering || crashResumePending)) SendHello();
				if (recovering && now >= nextClientStateLog)
				{
					nextClientStateLog = now + 2f;
					LogClientInventory("reconnected-waiting-status");
				}
				return;
			}

			if (!recovering) return;
			if (now >= nextClientStateLog)
			{
				nextClientStateLog = now + 2f;
				LogClientInventory("offline-local-simulation");
			}
			if (connecting)
			{
				if (Network.peerType != NetworkPeerType.Disconnected || now < connectDeadline) return;
				connecting = false;
				ScheduleRetry("connect-deadline");
			}
			if (!connecting && now >= nextRetry) TryReconnect();
		}

		private void LateUpdate()
		{
			if (!initialized || voluntary || !LegacyTransientReconnect.IsLegacy()) return;
			float now = Time.realtimeSinceStartup;
			// All native Start/Update/reset writers finish before the coherent body+control
			// capture or restore. The next physics step runs from the restored targets.
			ApplyPendingGhostPoses(now);
			if (clientRole && clientGhostFreezes.Count != 0) EnforceClientGhostPoses(now);
			if (!clientRole || voluntary || !Network.isClient) return;
			if (ownerControlSyncPending && !crashTrafficGated && !crashResumePending)
			{
				MachineController machine = game == null ? null : game.FICMBCLEFDL;
				if (machine != null)
				{
					machine.KNFGGGFAKML = ~HOCGCCAIPFF.JNPMJANFGNH;
					machine.SyncInputMask();
					ownerControlSyncPending = false;
					LegacyTransientReconnect.Log("CLIENT_CRASH_CONTROL_SYNC transport=native-input-mask gate=released");
				}
			}
			if (!recovering && !crashResumePending && registered && now >= nextOwnerCheckpoint)
				CaptureOwnerCheckpoint(now);
		}

		private void EnforceClientGhostPoses(float now)
		{
			for (int ghostIndex = clientGhostFreezes.Count - 1; ghostIndex >= 0; ghostIndex--)
			{
				ClientGhostFreeze ghost = clientGhostFreezes[ghostIndex];
				float maximumBodyDelta = 0f;
				float maximumVisualPositionDelta = 0f;
				float maximumVisualAngleDelta = 0f;
				float maximumVisualScaleDelta = 0f;
				try
				{
					for (int poseIndex = 0; poseIndex < ghost.Poses.Count; poseIndex++)
						maximumBodyDelta = Math.Max(maximumBodyDelta,
							ApplyClientGhostBody(ghost.Poses[poseIndex], true));
					for (int poseIndex = 0; poseIndex < ghost.VisualPoses.Count; poseIndex++)
						ApplyClientGhostVisual(ghost.VisualPoses[poseIndex], ref maximumVisualPositionDelta,
							ref maximumVisualAngleDelta, ref maximumVisualScaleDelta);
					if (maximumBodyDelta <= 0.02f && maximumVisualPositionDelta <= 0.02f
						&& maximumVisualAngleDelta <= 0.25f && maximumVisualScaleDelta <= 0.01f) continue;
					ghost.CorrectionCount++;
					if (ghost.CorrectionCount <= 3 || now >= ghost.NextCorrectionLog)
					{
						ghost.NextCorrectionLog = now + 5f;
						LegacyTransientReconnect.Log("CLIENT_GHOST_POSE_REENFORCED view="
							+ LegacyTransientReconnect.IdLabel(ghost.ViewId) + " snapshot="
							+ ghost.SnapshotId + " correction=" + ghost.CorrectionCount + " bodyDelta="
							+ maximumBodyDelta.ToString("0.000", CultureInfo.InvariantCulture)
							+ " visualPositionDelta="
							+ maximumVisualPositionDelta.ToString("0.000", CultureInfo.InvariantCulture)
							+ " visualAngleDelta="
							+ maximumVisualAngleDelta.ToString("0.000", CultureInfo.InvariantCulture)
							+ " visualScaleDelta="
							+ maximumVisualScaleDelta.ToString("0.000", CultureInfo.InvariantCulture));
					}
				}
				catch (Exception error)
				{
					if (ghost.CorrectionCount++ == 0)
						LegacyTransientReconnect.Log("CLIENT_GHOST_POSE_REENFORCE_FAILED view="
							+ LegacyTransientReconnect.IdLabel(ghost.ViewId) + " snapshot="
							+ ghost.SnapshotId + " reason=" + error.GetType().Name);
				}
			}
		}

		internal void BeginRecovery(NetworkDisconnection reason)
		{
			if (voluntary) return;
			float now = Time.realtimeSinceStartup;
			if (recovering)
			{
				if (now - recoveryBeganAt < 1f)
				{
					LegacyTransientReconnect.Log("CLIENT_RECOVERY_DUPLICATE_IGNORED reason=" + reason);
					return;
				}
				registered = false;
				connecting = false;
				connectedCallbackSeen = false;
				attempts++;
				nextRetry = now + 2f;
				nextHello = float.MaxValue;
				recoveryBeganAt = now;
				CaptureClientClaims();
				rebindAuthorized = false;
				rebindNotBefore = float.MaxValue;
				suppressReconnectGuidelineUntil = float.MaxValue;
				LegacyTransientReconnect.Log("CLIENT_RECOVERY_INTERRUPTED reason=" + reason
					+ " retryDelay=2.0 capturedViews=" + clientClaims.Count);
				return;
			}
			recovering = true;
			LegacyReconnectTarget.BeginDiscovery(now);
			recoveryBeganAt = now;
			recoveryDeadline = now + RecoveryTimeoutSeconds;
			suppressedNativeMachinePackets = 0;
			suppressedOutgoingRpcs = 0;
			suppressedReconnectGuidelines = 0;
			emptyPingGuards = 0;
			testDropArmed = false;
			registered = false;
			connecting = false;
			connectedCallbackSeen = false;
			attempts = 0;
			rebindAuthorized = false;
			rebindNotBefore = float.MaxValue;
			suppressReconnectGuidelineUntil = float.MaxValue;
			nextRetry = now + FirstReconnectDelaySeconds;
			nextHello = float.MaxValue;
			nextClientStateLog = now;
			CaptureLossPosition();
			CaptureClientClaims();
			BeginRetainedMachineReplay();
			MigrationRecoveryStarted();
			if (!MigrationArmed) ShowLocalStatus("[MPatcher] Connection lost. Local control remains active; reconnecting in 10 seconds...");
			LegacyTransientReconnect.Log("CLIENT_RECOVERY_BEGIN reason=" + reason + " oldPlayer="
				+ LegacyTransientReconnect.PlayerLabel(lastLocalPlayer) + " target=" + LegacyReconnectTarget.Description
				+ " firstDelaySeconds=" + (MigrationArmed ? 2 : 10) + " timeoutSeconds=300 capturedViews=" + clientClaims.Count);
			LogClientInventory("lost-callback");
		}

		internal void ObserveNativeDisconnect(NetworkDisconnection reason)
		{
			if (terminatingRecovery)
			{
				ClearActiveSession("recovery-native-exit");
				terminatingRecovery = false;
				nativeExitFallbackAt = float.MaxValue;
				LegacyTransientReconnect.Log("CLIENT_RECOVERY_NATIVE_EXIT reason=" + reason
					+ " source=disconnect-callback");
				return;
			}
			if (ShouldPreserveCrashMarkerForRetry(crashResumePending, voluntary))
				LogCrashMarkerPreserved("native-disconnect-" + reason);
			else ClearActiveSession("native-disconnect-" + reason);
			LegacyTransientReconnect.Log("CLIENT_NATIVE_DISCONNECT_PASSTHROUGH reason=" + reason
				+ " recovering=" + recovering + " voluntary=" + voluntary);
		}

		private void AbortRecovery(string reason)
		{
			if (!recovering || terminatingRecovery) return;
			CloseCrashRestoreConsent("recovery-abort");
			EndClientMigration("recovery-abort");
			recovering = false;
			connecting = false;
			registered = false;
			voluntary = true;
			terminatingRecovery = true;
			recoveryDeadline = float.MaxValue;
			rebindAuthorized = false;
			rebindNotBefore = float.MaxValue;
			suppressReconnectGuidelineUntil = 0f;
			ReleaseClientMachineTraffic();
			ReleaseCrashResumeTraffic();
			ClearActiveSession("recovery-abort");
			ShowLocalStatus("[MPatcher] The original server session is no longer available.");
			LegacyTransientReconnect.Log("CLIENT_RECOVERY_ABORT reason="
				+ LegacyTransientReconnect.Clean(reason) + " attempts=" + attempts
				+ " peer=" + Network.peerType + " nativeExit=true");
			if (Network.peerType == NetworkPeerType.Disconnected)
			{
				nativeExitFallbackAt = Time.realtimeSinceStartup;
				return;
			}
			nativeExitFallbackAt = Time.realtimeSinceStartup + NativeExitFallbackSeconds;
			try { Network.Disconnect(); }
			catch (Exception error)
			{
				LegacyTransientReconnect.Log("CLIENT_RECOVERY_DISCONNECT_FAILED type="
					+ error.GetType().Name + " message=" + LegacyTransientReconnect.Clean(error.Message));
				nativeExitFallbackAt = Time.realtimeSinceStartup;
			}
		}

		private void InvokeNativeDisconnect(string source)
		{
			terminatingRecovery = false;
			nativeExitFallbackAt = float.MaxValue;
			try
			{
				MethodInfo method = AccessTools.Method(typeof(Game), "OnDisconnectedFromServer",
					new Type[] { typeof(NetworkDisconnection) });
				if (method == null) throw new MissingMethodException(typeof(Game).FullName,
					"OnDisconnectedFromServer");
				LegacyTransientReconnect.Log("CLIENT_RECOVERY_NATIVE_EXIT reason=Disconnected source=" + source);
				method.Invoke(game, new object[] { NetworkDisconnection.Disconnected });
			}
			catch (Exception error)
			{
				LegacyTransientReconnect.Log("CLIENT_RECOVERY_NATIVE_EXIT_FAILED type="
					+ error.GetType().Name + " message=" + LegacyTransientReconnect.Clean(error.Message));
			}
		}

		internal void ScheduleTestDrop(NetworkPlayer player, string incomingToken)
		{
			if (!serverRole || !Network.isServer || testDropPending) return;
			pendingTestDropPlayer = player;
			pendingTestDropToken = incomingToken;
			testDropPending = true;
		}

		private void ExecuteTestDrop()
		{
			NetworkPlayer player = pendingTestDropPlayer;
			string outgoingToken = pendingTestDropToken;
			testDropPending = false;
			pendingTestDropToken = null;
			LegacyTransientReconnect.Log("SERVER_TEST_DROP_EXECUTED token="
				+ LegacyTransientReconnect.TokenLabel(outgoingToken) + " player="
				+ LegacyTransientReconnect.PlayerLabel(player) + " notifyClient=false");
			try { Network.CloseConnection(player, false); }
			catch (Exception error)
			{
				LegacyTransientReconnect.Log("SERVER_TEST_DROP_FAILED type=" + error.GetType().Name
					+ " message=" + LegacyTransientReconnect.Clean(error.Message));
			}
		}

		private void CheckTestDropRequest()
		{
			string path = TestDropPath();
			if (!File.Exists(path)) return;
			try
			{
				File.Delete(path);
				testDropArmed = true;
				testDropArmUntil = Time.realtimeSinceStartup + 30f;
				sessionView.RPC("MPatcherResumeTestDropV2", RPCMode.Server, 2, token);
				LegacyTransientReconnect.Log("CLIENT_TEST_DROP_REQUESTED token="
					+ LegacyTransientReconnect.TokenLabel(token) + " armSeconds=30");
			}
			catch (Exception error)
			{
				testDropArmed = false;
				LegacyTransientReconnect.Log("CLIENT_TEST_DROP_FAILED type=" + error.GetType().Name
					+ " message=" + LegacyTransientReconnect.Clean(error.Message));
			}
		}

		internal void NotifyVoluntaryExit()
		{
			CloseCrashRestoreConsent("voluntary-exit");
			EndClientMigration("voluntary-exit");
			ClearActiveSession("voluntary-exit");
			voluntary = true;
			recovering = false;
			connecting = false;
			registered = false;
			crashResumePending = false;
			pendingGhostPoses.Clear();
			crashFreshMachine = null;
			pendingPeerRebinds.Clear();
			pendingRemotePresentationRefreshes.Clear();
			ownerControlSyncPending = false;
			rebindAuthorized = false;
			rebindNotBefore = float.MaxValue;
			suppressReconnectGuidelineUntil = 0f;
			ReleaseClientMachineTraffic();
			ReleaseCrashResumeTraffic();
			if (clientRole && Network.isClient && sessionView != null && LegacyTransientReconnect.ValidToken(token))
			{
				try
				{
					sessionView.RPC("MPatcherResumeGoodbyeV2", RPCMode.Server, 2, token);
					LegacyTransientReconnect.Log("CLIENT_GOODBYE_SENT token=" + LegacyTransientReconnect.TokenLabel(token));
				}
				catch (Exception error)
				{
					LegacyTransientReconnect.Log("CLIENT_GOODBYE_FAILED type=" + error.GetType().Name);
				}
			}
		}

		internal bool NeedsRecoveryExit
		{
			get { return ShouldCompleteRecoveryExit(initialized, clientRole, voluntary,
				recovering, connecting, crashResumePending, terminatingRecovery); }
		}

		internal static bool ShouldCompleteRecoveryExit(bool initialized, bool client, bool voluntary,
			bool recovering, bool connecting, bool crashPending, bool terminating)
		{
			return initialized && client && !voluntary && (recovering || connecting || crashPending || terminating);
		}

		internal void ArmRecoveryExit()
		{
			terminatingRecovery = true;
			nativeExitFallbackAt = Time.realtimeSinceStartup + NativeExitFallbackSeconds;
		}

		internal void CompleteRecoveryExit()
		{
			// A synchronous native callback may already have completed the exit.
			if (!terminatingRecovery) return;
			LegacyTransientReconnect.Log("CLIENT_RECOVERY_CANCEL source=Exit peer=" + Network.peerType
				+ " retries=cancelled checkpoint=cleared sceneExit=native fallbackSeconds=2");
			if (Network.peerType == NetworkPeerType.Disconnected)
			{
				InvokeNativeDisconnect("manual-exit-disconnected");
				return;
			}
			// Native LNJBNMLMLJE skips Disconnect while Connecting. Cancel that attempt too.
			try { Network.Disconnect(200); }
			catch (Exception error)
			{
				LegacyTransientReconnect.Log("CLIENT_RECOVERY_CANCEL_DISCONNECT_FAILED type=" + error.GetType().Name);
				InvokeNativeDisconnect("manual-exit-disconnect-failed");
			}
		}

		internal bool ObserveRecoveryExitCallback(NetworkDisconnection reason)
		{
			if (!terminatingRecovery || !voluntary) return false;
			ObserveNativeDisconnect(reason);
			return true;
		}

		private void OnApplicationQuit()
		{
			if (!initialized || voluntary) return;
			bool connected = false;
			try { connected = clientRole && Network.isClient; }
			catch { }
			LegacyTransientReconnect.Log("CLIENT_APPLICATION_QUIT connected=" + connected
				+ " recovering=" + recovering + " goodbyePossible="
				+ (connected && sessionView != null && LegacyTransientReconnect.ValidToken(token)));
			NotifyVoluntaryExit();
			if (!connected) return;
			try
			{
				Network.Disconnect(200);
				LegacyTransientReconnect.Log("CLIENT_APPLICATION_QUIT_DISCONNECT drainMilliseconds=200");
			}
			catch (Exception error)
			{
				LegacyTransientReconnect.Log("CLIENT_APPLICATION_QUIT_DISCONNECT_FAILED type="
					+ error.GetType().Name);
			}
		}

		internal void NotifySceneDestroy()
		{
			CloseCrashRestoreConsent("scene-destroy");
			EndClientMigration("scene-destroy");
			if (clientRole)
			{
				if (ShouldPreserveCrashMarkerForRetry(crashResumePending, voluntary))
					LogCrashMarkerPreserved("scene-destroy");
				else ClearActiveSession("scene-destroy");
			}
			ReleaseAllClientGhosts("scene-destroy");
			pendingGhostPoses.Clear();
			pendingRemotePresentationRefreshes.Clear();
			LegacyTransientReconnect.Log("CONTROLLER_DESTROY scene=" + (game == null ? "unavailable" : game.name)
				+ " recovering=" + recovering + " voluntary=" + voluntary);
			initialized = false;
		}

		private void TryReconnect()
		{
			attempts++;
			NetworkConnectionError result;
			try { result = LegacyReconnectTarget.Connect(); }
			catch (Exception error)
			{
				LegacyTransientReconnect.Log("CLIENT_RECONNECT_THROW attempt=" + attempts + " type="
					+ error.GetType().Name + " message=" + LegacyTransientReconnect.Clean(error.Message));
				ScheduleRetry("exception");
				return;
			}
			LegacyTransientReconnect.Log("CLIENT_RECONNECT_ATTEMPT attempt=" + attempts + " target="
				+ LegacyReconnectTarget.Description + " result=" + result);
			if (result == NetworkConnectionError.NoError)
			{
				connecting = true;
				connectDeadline = Time.realtimeSinceStartup + 12f;
				return;
			}
			ScheduleRetry("immediate-" + result);
		}

		private void ScheduleRetry(string reason)
		{
			connecting = false;
			float delay = attempts <= 1 ? 1f : attempts == 2 ? 2f : attempts == 3 ? 5f : 10f;
			nextRetry = Time.realtimeSinceStartup + delay;
			LegacyTransientReconnect.Log("CLIENT_RECONNECT_SCHEDULED completed=" + attempts
				+ " delay=" + delay.ToString("0.0", CultureInfo.InvariantCulture) + " reason=" + reason);
		}

		private void OnConnectedToServer()
		{
			if (!initialized || !clientRole || voluntary) return;
			connecting = false;
			connectedCallbackSeen = true;
			try { Network.isMessageQueueRunning = true; }
			catch { }
			if (recovering)
			{
				GateClientMachineTraffic();
			}
			LegacyTransientReconnect.Log("CLIENT_CONNECTED_CALLBACK recovering=" + recovering + " attempt="
				+ attempts + " newPlayer=" + LegacyTransientReconnect.PlayerLabel(Network.player)
				+ " machineGroupGated=" + machineTrafficGated + " claims=" + clientClaims.Count
				+ " rebind=awaiting-claim-ready");
			LogClientInventory("connected-callback");
			nextHello = Time.realtimeSinceStartup;
			SendHello();
		}

		private void OnFailedToConnect(NetworkConnectionError error)
		{
			if (!recovering || voluntary) return;
			LegacyTransientReconnect.Log("CLIENT_RECONNECT_FAILED attempt=" + attempts + " result=" + error);
			ScheduleRetry("callback-" + error);
		}

		private void OnMasterServerEvent(MasterServerEvent value)
		{
			if (recovering && !voluntary && value == MasterServerEvent.HostListReceived)
				LegacyReconnectTarget.DiscoveryReply(Time.realtimeSinceStartup);
		}

		private void OnFailedToConnectToMasterServer(NetworkConnectionError error)
		{
			if (recovering && !voluntary)
				LegacyReconnectTarget.DiscoveryFailed(Time.realtimeSinceStartup, error.ToString());
		}

		private void OnDisconnectedFromServer(NetworkDisconnection reason)
		{
			if (ShouldResumeDisconnect(reason)) BeginRecovery(reason);
		}

		private void SendHello()
		{
			if (sessionView == null) sessionView = game == null ? null : game.GetComponent<NetworkView>();
			if (sessionView == null || !Network.isClient || !LegacyTransientReconnect.ValidToken(token)) return;
			nextHello = Time.realtimeSinceStartup + HelloInterval;
			if (crashResumePending)
			{
				NetworkView freshView;
				string waitReason;
				if (!TryGetCrashFreshView(out freshView, out waitReason))
				{
					LegacyTransientReconnect.Log("CLIENT_CRASH_HELLO_WAIT reason="
						+ LegacyTransientReconnect.Clean(waitReason));
					return;
				}
				crashFreshViewId = freshView.viewID;
				crashFreshMachine = LegacyTransientReconnect.FindMachineForView(freshView);
				try
				{
					if (crashUsingV3)
					{
						sessionView.RPC("MPatcherCrashResumeHelloV3", RPCMode.Server, 3, token,
							SafeLocalName(), crashFreshViewId);
						StartCrashProtocolFallbackTimer();
					}
					else
					{
						string checkpointIdentity = GetCrashCheckpointIdentity(
							out crashCheckpointIdentityReason);
						string freshIdentityReason;
						if (!TryGetCrashFreshOwnerIdentity(out crashFreshOwnerIdentity,
							out freshIdentityReason))
						{
							LegacyTransientReconnect.Log("CLIENT_CRASH_HELLO_WAIT reason="
								+ LegacyTransientReconnect.Clean(freshIdentityReason));
							return;
						}
						sessionView.RPC("MPatcherCrashResumeHelloV6", RPCMode.Server, 6, token,
							SafeLocalName(), crashFreshViewId, checkpointIdentity ?? "",
							crashFreshOwnerIdentity);
						StartCrashConsentCompatibilityTimer();
					}
					LegacyTransientReconnect.Log("CLIENT_CRASH_HELLO_SENT protocol="
						+ (crashUsingV3 ? 3 : 6) + " token="
						+ LegacyTransientReconnect.TokenLabel(token) + " player="
						+ LegacyTransientReconnect.PlayerLabel(Network.player) + " fresh="
						+ LegacyTransientReconnect.IdLabel(crashFreshViewId)
						+ (crashUsingV3 ? "" : " checkpointIdentity="
							+ (string.IsNullOrEmpty(crashCheckpointIdentity) ? "unavailable:"
								+ LegacyTransientReconnect.Clean(crashCheckpointIdentityReason)
								: crashCheckpointIdentity) + " freshOwnerIdentity="
							+ crashFreshOwnerIdentity));
				}
				catch (Exception error)
				{
					LegacyTransientReconnect.Log("CLIENT_CRASH_HELLO_FAILED type="
						+ error.GetType().Name + " message="
						+ LegacyTransientReconnect.Clean(error.Message));
				}
				return;
			}
			try
			{
				sessionView.RPC("MPatcherResumeHelloV2", RPCMode.Server, 2, token,
					SafeLocalName(), recovering ? 1 : 0);
				LegacyTransientReconnect.Log("CLIENT_HELLO_SENT token=" + LegacyTransientReconnect.TokenLabel(token)
					+ " recovering=" + recovering + " player=" + LegacyTransientReconnect.PlayerLabel(Network.player)
					+ " gameView=" + ViewLabel(sessionView));
				if (recovering) SendClaims();
			}
			catch (Exception error)
			{
				LegacyTransientReconnect.Log("CLIENT_HELLO_FAILED type=" + error.GetType().Name
					+ " message=" + LegacyTransientReconnect.Clean(error.Message));
			}
		}

		private void StartCrashConsentCompatibilityTimer()
		{
			if (crashV3FallbackAt != float.MaxValue) return;
			crashV3FallbackAt = Time.realtimeSinceStartup + CrashConsentCompatibilitySeconds;
			LegacyTransientReconnect.Log("CLIENT_CRASH_PROTOCOL_TIMER_STARTED source=v6-hello-sent"
				+ " timeoutSeconds=5 fallback=v3");
		}

		private void StartCrashProtocolFallbackTimer()
		{
			if (crashProtocolFallbackAt != float.MaxValue) return;
			crashProtocolFallbackAt = Time.realtimeSinceStartup + CrashProtocolFallbackSeconds;
			LegacyTransientReconnect.Log("CLIENT_CRASH_PROTOCOL_TIMER_STARTED source=hello-sent"
				+ " timeoutSeconds=30");
		}

		private bool TryGetCrashFreshView(out NetworkView view, out string reason)
		{
			view = null;
			reason = null;
			MachineController machine = game == null ? null : game.FICMBCLEFDL;
			if (machine == null) { reason = "local-machine-not-ready"; return false; }
			NetworkView preferred = machine.GetComponent<NetworkView>();
			if (CrashViewUsable(preferred)) view = preferred;
			NetworkView[] views = machine.GetComponentsInChildren<NetworkView>(true);
			if (view == null)
				for (int index = 0; index < views.Length; index++)
					if (CrashViewUsable(views[index])) { view = views[index]; break; }
			if (view == null)
			{
				reason = "owned-view-not-ready-count-" + views.Length;
				return false;
			}
			return TryGetLocalCrashMachineReadiness(machine, out reason);
		}

		private bool TryGetCrashFreshOwnerIdentity(out string identity, out string reason)
		{
			identity = null;
			MachineController machine = crashFreshMachine != null ? crashFreshMachine
				: (game == null ? null : game.FICMBCLEFDL);
			if (!TryGetLocalCrashMachineReadiness(machine, out reason)) return false;
			string constructionFingerprint;
			if (!LegacyMachineRecoveryFingerprint.TryCompute(machine,
				out constructionFingerprint, out reason))
			{
				reason = "fresh-owner-" + LegacyTransientReconnect.Clean(reason);
				return false;
			}
			int bodyCount = machine.ILBAAENKMBL == null ? 0 : machine.ILBAAENKMBL.Count;
			if (bodyCount <= 0)
			{
				reason = "fresh-owner-body-count-invalid";
				return false;
			}
			identity = BuildOwnerCheckpointFingerprint(constructionFingerprint, bodyCount);
			reason = "ready";
			return true;
		}

		private static bool TryGetLocalCrashMachineReadiness(MachineController machine,
			out string reason)
		{
			reason = null;
			if (machine == null) { reason = "local-machine-not-ready"; return false; }
			if (machine.GetComponent<MachineSerializer>() == null)
			{
				reason = "local-serializer-not-ready";
				return false;
			}
			if (machine.CLBBODLKEJI == null)
			{
				reason = "local-core-not-ready";
				return false;
			}
			if (machine.KBLANAFAJFP == null || machine.KBLANAFAJFP.Count == 0)
			{
				reason = "local-body-roots-not-ready";
				return false;
			}
			if (machine.ILBAAENKMBL == null || machine.ILBAAENKMBL.Count == 0)
			{
				reason = "local-bodies-not-ready";
				return false;
			}
			if (machine.KBLANAFAJFP.Count != machine.ILBAAENKMBL.Count)
			{
				reason = "local-body-count-mismatch-roots-" + machine.KBLANAFAJFP.Count
					+ "-bodies-" + machine.ILBAAENKMBL.Count;
				return false;
			}
			if (machine.MMDCPMAKLPL == null
				|| machine.MMDCPMAKLPL.Count != machine.ILBAAENKMBL.Count - 1)
			{
				reason = "local-articulation-count-mismatch-parents-"
					+ (machine.MMDCPMAKLPL == null ? 0 : machine.MMDCPMAKLPL.Count)
					+ "-bodies-" + machine.ILBAAENKMBL.Count;
				return false;
			}
			for (int index = 0; index < machine.ILBAAENKMBL.Count; index++)
			{
				BodyController body = machine.ILBAAENKMBL[index];
				if (machine.KBLANAFAJFP[index] == null || body == null
					|| body.NFMPBACKJOJ == null || body.BBLGKLFBJGE != index)
				{
					reason = "local-body-not-ready-" + index;
					return false;
				}
				if (index > 0 && machine.MMDCPMAKLPL[index - 1] == null)
				{
					reason = "local-articulation-parent-not-ready-" + (index - 1);
					return false;
				}
			}
			return true;
		}

		private void CaptureOwnerCheckpoint(float now)
		{
			long startedAt = System.Diagnostics.Stopwatch.GetTimestamp();
			nextOwnerCheckpoint = now + OwnerCheckpointIntervalSeconds;
			MachineController machine = game == null ? null : game.FICMBCLEFDL;
			string reason;
			if (!TryGetLocalCrashMachineReadiness(machine, out reason)
				|| !LegacyReconnectTarget.HasTarget || !LegacyTransientReconnect.ValidToken(token))
			{
				if (now >= nextOwnerCheckpointLog)
				{
					nextOwnerCheckpointLog = now + 5f;
					LegacyTransientReconnect.Log("CLIENT_OWNER_CHECKPOINT_WAIT reason="
						+ LegacyTransientReconnect.Clean(reason ?? "session-not-ready"));
				}
				return;
			}
			// Capture and restore must hash the same lifecycle phase. Native part
			// initialization may still mutate construction data after bodies exist.
			if (now < nextOwnerControlRetry) return;
			LegacyMachineControlState controlState;
			bool controlsRebuilt = false;
			try
			{
				if (ownerControls == null || ownerControls.Machine != machine || !ownerControls.ConfigurationReferencesMatch)
				{
					ownerControls = new LegacyMachineControlRecovery(machine);
					// A replaced Assign/control configuration invalidates the old identity too.
					ownerCheckpointFingerprint = null;
					controlsRebuilt = true;
					LegacyTransientReconnect.Log("CLIENT_OWNER_CONTROLS_READY schema=2 parts=" + ownerControls.PartCount
						+ " ownerLayout=" + ownerControls.LayoutHash + " scope=owner-only cached=true");
				}
				controlState = ownerControls.Capture();
			}
			catch (Exception error)
			{
				nextOwnerControlRetry = now + 5f;
				LegacyTransientReconnect.Log("CLIENT_OWNER_CONTROLS_WAIT reason=" + LegacyTransientReconnect.Clean(error.Message)
					+ " retrySeconds=5 checkpoint=preserved fingerprint=not-captured");
				return;
			}
			bool newMachine = controlsRebuilt || ownerCheckpointMachine != machine
				|| string.IsNullOrEmpty(ownerCheckpointFingerprint);
			if (newMachine)
			{
				if (ownerFingerprintAttemptMachine != machine)
				{
					ownerFingerprintAttemptMachine = machine;
					nextOwnerFingerprintRetry = 0f;
				}
				if (now < nextOwnerFingerprintRetry) return;
				string constructionFingerprint;
				if (!LegacyMachineRecoveryFingerprint.TryCompute(machine,
					out constructionFingerprint, out reason))
				{
					// Invalid/incomplete construction must not trigger an expensive
					// full traversal at the 250 ms physical-snapshot cadence.
					nextOwnerFingerprintRetry = Time.realtimeSinceStartup + 5f;
					if (now >= nextOwnerCheckpointLog)
					{
						nextOwnerCheckpointLog = now + 5f;
						LegacyTransientReconnect.Log("CLIENT_OWNER_CHECKPOINT_WAIT reason="
							+ LegacyTransientReconnect.Clean(reason)
							+ " retrySeconds=5 elapsedMs=" + CheckpointElapsedMilliseconds(startedAt));
					}
					return;
				}
				ownerCheckpointMachine = machine;
				ownerCheckpointFingerprint = BuildOwnerCheckpointFingerprint(
					constructionFingerprint, machine.ILBAAENKMBL.Count);
				nextOwnerFingerprintRetry = 0f;
				LegacyTransientReconnect.Log("CLIENT_OWNER_FINGERPRINT_READY version=2 blocks="
					+ (machine.HHGILAIOCLG.blockData == null ? 0 : machine.HHGILAIOCLG.blockData.Count) + " elapsedMs="
					+ CheckpointElapsedMilliseconds(startedAt) + " cached=true format=v1 phase=controls-ready fingerprint="
					+ ownerCheckpointFingerprint);
			}
			LegacyMachineRecoveryBodyState[] bodies =
				new LegacyMachineRecoveryBodyState[machine.ILBAAENKMBL.Count];
			int brokenBodies = 0;
			for (int index = 0; index < bodies.Length; index++)
			{
				BodyController body = machine.ILBAAENKMBL[index];
				Rigidbody rigidbody = body.NFMPBACKJOJ;
				LegacyMachineRecoveryBodyState state = new LegacyMachineRecoveryBodyState();
				Vector3 position = rigidbody.position;
				Quaternion rotation = rigidbody.rotation;
				Vector3 velocity = rigidbody.velocity;
				Vector3 angular = rigidbody.angularVelocity;
				state.PositionX = position.x; state.PositionY = position.y; state.PositionZ = position.z;
				state.RotationX = rotation.x; state.RotationY = rotation.y;
				state.RotationZ = rotation.z; state.RotationW = rotation.w;
				state.VelocityX = velocity.x; state.VelocityY = velocity.y; state.VelocityZ = velocity.z;
				state.AngularX = angular.x; state.AngularY = angular.y; state.AngularZ = angular.z;
				state.Health = body.MEJNIODBGFI;
				state.BonusHealth = body.HPOMNJCEJIP;
				state.Broken = body.IsBroken();
				state.Suspended = body.EFCBCPOCOBB;
				state.IsKinematic = rigidbody.isKinematic;
				state.SuspendFrames = Math.Max(0, body.OKGLHFEAEEP);
				if (IsOwnerCheckpointDetachedBody(index, state)) brokenBodies++;
				bodies[index] = state;
			}
			if (ownerCheckpointSequence == int.MaxValue) ownerCheckpointSequence = 0;
			LegacyMachineRecoverySnapshot snapshot = new LegacyMachineRecoverySnapshot();
			snapshot.Generation = 1;
			snapshot.Sequence = ++ownerCheckpointSequence;
			snapshot.CapturedAt = DateTime.UtcNow.Ticks;
			snapshot.Fingerprint = ownerCheckpointFingerprint;
			snapshot.Bodies = bodies;
			snapshot.Controls = controlState;
			int bytes;
			bool saved = LegacyCrashOwnerCheckpoint.Save(token, LegacyReconnectTarget.Description,
				snapshot, out bytes, out reason);
			if (!saved || newMachine || ownerCheckpointSequence == 1 || now >= nextOwnerCheckpointLog)
			{
				nextOwnerCheckpointLog = now + 5f;
				LegacyMachineRecoveryBodyState rootState = bodies[0];
				LegacyTransientReconnect.Log("CLIENT_OWNER_CHECKPOINT_" + (saved ? "SAVED" : "FAILED")
					+ " sequence=" + snapshot.Sequence + " controls=1 frame=" + snapshot.Controls.Frame
					+ " parts=" + snapshot.Controls.Parts.Length + " bodies=" + bodies.Length
					+ " broken=" + brokenBodies + " bytes=" + bytes + " fingerprint="
					+ ownerCheckpointFingerprint + " rootPos="
					+ rootState.PositionX.ToString("0.000", CultureInfo.InvariantCulture) + ","
					+ rootState.PositionY.ToString("0.000", CultureInfo.InvariantCulture) + ","
					+ rootState.PositionZ.ToString("0.000", CultureInfo.InvariantCulture)
					+ " rootHealth=" + rootState.Health.ToString("0.000", CultureInfo.InvariantCulture)
					+ " rootBroken=" + rootState.Broken + " rootSuspended=" + rootState.Suspended
					+ " reason=" + LegacyTransientReconnect.Clean(reason)
					+ " elapsedMs=" + CheckpointElapsedMilliseconds(startedAt));
			}
		}

		private static string CheckpointElapsedMilliseconds(long startedAt)
		{
			return ((System.Diagnostics.Stopwatch.GetTimestamp() - startedAt) * 1000d
				/ System.Diagnostics.Stopwatch.Frequency).ToString("0.000", CultureInfo.InvariantCulture);
		}

		private string GetCrashCheckpointIdentity(out string reason)
		{
			if (crashCheckpointIdentityAttempted)
			{
				reason = crashCheckpointIdentityReason;
				return crashCheckpointIdentity;
			}
			crashCheckpointIdentityAttempted = true;
			crashCheckpointIdentity = null;
			LegacyMachineRecoverySnapshot snapshot;
			long capturedUtcTicks;
			int bytes;
			if (!LegacyReconnectTarget.HasTarget)
			{
				reason = crashCheckpointIdentityReason = "owner-checkpoint-session-not-ready";
				return null;
			}
			if (!LegacyCrashOwnerCheckpoint.TryLoad(token, LegacyReconnectTarget.Description,
				out snapshot, out capturedUtcTicks, out bytes, out reason))
			{
				crashCheckpointIdentityReason = reason;
				return null;
			}
			string constructionFingerprint;
			if (snapshot == null || !LegacyTransientReconnect.TryExtractOwnerConstructionFingerprint(
				snapshot.Fingerprint, out constructionFingerprint))
			{
				reason = crashCheckpointIdentityReason = "owner-checkpoint-fingerprint-invalid";
				return null;
			}
			crashCheckpointIdentity = snapshot.Fingerprint;
			reason = crashCheckpointIdentityReason = "loaded";
			LegacyTransientReconnect.Log("CLIENT_CRASH_CHECKPOINT_IDENTITY_LOADED fingerprint="
				+ crashCheckpointIdentity + " bytes=" + bytes);
			return crashCheckpointIdentity;
		}

		private static bool TryLoadOwnerCheckpoint(PendingGhostPose pending,
			out LegacyMachineRecoverySnapshot snapshot, out float ageSeconds, out int bytes,
			out string reason)
		{
			snapshot = null;
			ageSeconds = -1f;
			bytes = 0;
			if (!LegacyReconnectTarget.HasTarget)
			{
				reason = "owner-checkpoint-session-not-ready";
				return false;
			}
			// The pending server-authorized restore owns this decoded, session-checked
			// snapshot. It is discarded with the pending request on expiry/session exit.
			if (pending.OwnerCheckpoint == null)
			{
				long started = System.Diagnostics.Stopwatch.GetTimestamp();
				if (!LegacyCrashOwnerCheckpoint.TryLoad(pending.RestoreToken, LegacyReconnectTarget.Description,
					out snapshot, out pending.OwnerCapturedUtcTicks, out pending.OwnerCheckpointBytes, out reason)) return false;
				pending.OwnerCheckpoint = snapshot;
				LegacyTransientReconnect.Log("CLIENT_CRASH_OWNER_CHECKPOINT_LOADED cached=true sequence="
					+ snapshot.Sequence + " bytes=" + pending.OwnerCheckpointBytes
					+ " elapsedMs=" + CheckpointElapsedMilliseconds(started));
			}
			snapshot = pending.OwnerCheckpoint;
			bytes = pending.OwnerCheckpointBytes;
			ageSeconds = (float)Math.Max(0d, (DateTime.UtcNow.Ticks - pending.OwnerCapturedUtcTicks)
				/ (double)TimeSpan.TicksPerSecond);
			reason = "loaded";
			return true;
		}

		private static bool ValidateOwnerCheckpoint(MachineController machine,
			LegacyMachineRecoverySnapshot snapshot, out string reason)
		{
			// Run the full construction hash only after controls are ready, immediately
			// before mutation. In-place edits during the wait still fail validation.
			string constructionFingerprint;
			if (!LegacyMachineRecoveryFingerprint.TryCompute(machine, out constructionFingerprint,
				out reason)) return false;
			string expected = BuildOwnerCheckpointFingerprint(constructionFingerprint,
				machine.ILBAAENKMBL == null ? 0 : machine.ILBAAENKMBL.Count);
			if (snapshot == null || snapshot.Fingerprint != expected)
			{
				reason = "owner-checkpoint-fingerprint-mismatch saved=" + (snapshot == null ? "missing" : snapshot.Fingerprint)
					+ " actual=" + expected + " phase=controls-ready";
				return false;
			}
			if (machine.ILBAAENKMBL == null || snapshot.Bodies == null
				|| snapshot.Bodies.Length != machine.ILBAAENKMBL.Count)
			{
				reason = "owner-checkpoint-body-count-mismatch";
				return false;
			}
			reason = "validated";
			return true;
		}

		private static string BuildOwnerCheckpointFingerprint(string constructionFingerprint,
			int bodyCount)
		{
			return "owner=" + constructionFingerprint + ";bodies=" + bodyCount;
		}

		private static bool CrashViewUsable(NetworkView view)
		{
			return view != null && view.viewID != NetworkViewID.unassigned && view.isMine
				&& view.owner == Network.player;
		}

		private void GateCrashResumeTraffic()
		{
			if (crashTrafficGated) return;
			if (sessionView != null && sessionView.group == MachineNetworkGroup)
			{
				LegacyTransientReconnect.Log("CLIENT_CRASH_MACHINE_GATE_SKIPPED group=1"
					+ " reason=control-view-shares-group");
				return;
			}
			try { Network.SetSendingEnabled(MachineNetworkGroup, false); }
			catch { }
			crashTrafficGated = true;
			LegacyTransientReconnect.Log("CLIENT_CRASH_MACHINE_GATE group=1 sending=false receiving=true");
		}

		private void ReleaseCrashResumeTraffic()
		{
			if (!crashTrafficGated) return;
			try { Network.SetSendingEnabled(MachineNetworkGroup, true); }
			catch { }
			crashTrafficGated = false;
			LegacyTransientReconnect.Log("CLIENT_CRASH_MACHINE_GATE_RELEASED group=1 sending=true");
		}

		private void CaptureClientClaims()
		{
			clientClaims.Clear();
			MachineController machine = game == null ? null : game.FICMBCLEFDL;
			if (machine == null) return;
			NetworkView[] views = machine.GetComponentsInChildren<NetworkView>(true);
			for (int i = 0; i < views.Length; i++)
			{
				NetworkView view = views[i];
				if (view == null) continue;
				ClientViewClaim claim = new ClientViewClaim();
				claim.View = view;
				claim.OldId = view.viewID;
				claim.NewId = NetworkViewID.unassigned;
				clientClaims.Add(claim);
				LegacyTransientReconnect.Log("CLIENT_VIEW_CAPTURED index=" + (clientClaims.Count - 1)
					+ " old=" + LegacyTransientReconnect.IdLabel(claim.OldId)
					+ " owner=" + LegacyTransientReconnect.PlayerLabel(view.owner)
					+ " path=" + TransformPath(view.transform));
			}
		}

		private void GateClientMachineTraffic()
		{
			if (sessionView != null && sessionView.group == MachineNetworkGroup)
			{
				machineTrafficGated = false;
				LegacyTransientReconnect.Log("CLIENT_MACHINE_GROUP_GATE_SKIPPED group=1 reason=control-view-shares-group");
				return;
			}
			try { Network.SetSendingEnabled(MachineNetworkGroup, false); }
			catch { }
			NetworkPlayer[] peers;
			try { peers = Network.connections; }
			catch { peers = new NetworkPlayer[0]; }
			for (int i = 0; i < peers.Length; i++)
			{
				try { Network.SetReceivingEnabled(peers[i], MachineNetworkGroup, false); }
				catch { }
			}
			machineTrafficGated = true;
			LegacyTransientReconnect.Log("CLIENT_MACHINE_GROUP_GATE group=1 sending=false receiving=false peers="
				+ peers.Length);
		}

		private void PrepareClientRebind()
		{
			if (clientClaims.Count == 0) CaptureClientClaims();
			for (int i = 0; i < clientClaims.Count; i++)
			{
				ClientViewClaim claim = clientClaims[i];
				if (claim.Prepared) continue;
				if (claim.View == null)
				{
					LegacyTransientReconnect.Log("CLIENT_VIEW_ALLOCATE_FAILED index=" + i
						+ " reason=preserved-view-missing");
					continue;
				}
				NetworkViewID newId;
				try { newId = Network.AllocateViewID(); }
				catch (Exception error)
				{
					LegacyTransientReconnect.Log("CLIENT_VIEW_ALLOCATE_FAILED index=" + i + " type="
						+ error.GetType().Name + " message=" + LegacyTransientReconnect.Clean(error.Message));
					continue;
				}
				if (newId == NetworkViewID.unassigned)
				{
					LegacyTransientReconnect.Log("CLIENT_VIEW_ALLOCATE_FAILED index=" + i
						+ " reason=unassigned");
					continue;
				}
				try { claim.View.viewID = newId; }
				catch (Exception error)
				{
					LegacyTransientReconnect.Log("CLIENT_VIEW_ASSIGN_FAILED index=" + i + " type="
						+ error.GetType().Name + " message=" + LegacyTransientReconnect.Clean(error.Message));
					continue;
				}
				claim.NewId = newId;
				claim.Prepared = claim.View.viewID == newId && claim.View.isMine
					&& claim.View.owner == Network.player;
				LegacyTransientReconnect.Log("CLIENT_VIEW_PREPARED index=" + i + " old="
					+ LegacyTransientReconnect.IdLabel(claim.OldId) + " new="
					+ LegacyTransientReconnect.IdLabel(newId) + " owner="
					+ LegacyTransientReconnect.PlayerLabel(claim.View.owner) + " mine="
					+ claim.View.isMine + " prepared=" + claim.Prepared);
			}
		}

		private bool ClientClaimsPrepared()
		{
			if (clientClaims.Count == 0) return false;
			for (int i = 0; i < clientClaims.Count; i++)
				if (!clientClaims[i].Prepared) return false;
			return true;
		}

		private void AuthorizeClientRebind(string reason)
		{
			if (!recovering || ClientClaimsPrepared()) return;
			if (rebindAuthorized) return;
			rebindAuthorized = true;
			rebindNotBefore = Time.realtimeSinceStartup + RebindDrainSeconds;
			LegacyTransientReconnect.Log("CLIENT_REBIND_DRAIN_SCHEDULED delay=1.0 reason="
				+ LegacyTransientReconnect.Clean(reason) + " oldViews=" + clientClaims.Count);
		}

		private void SendClaims()
		{
			if (sessionView == null || !Network.isClient) return;
			for (int i = 0; i < clientClaims.Count; i++)
			{
				ClientViewClaim claim = clientClaims[i];
				if (!claim.Prepared) continue;
				try
				{
					sessionView.RPC("MPatcherResumeClaimV2", RPCMode.Server, 2, token, i,
						clientClaims.Count, claim.OldId, claim.NewId);
					LegacyTransientReconnect.Log("CLIENT_CLAIM_SENT index=" + i + " count="
						+ clientClaims.Count + " old=" + LegacyTransientReconnect.IdLabel(claim.OldId)
						+ " new=" + LegacyTransientReconnect.IdLabel(claim.NewId));
				}
				catch (Exception error)
				{
					LegacyTransientReconnect.Log("CLIENT_CLAIM_SEND_FAILED index=" + i + " type="
						+ error.GetType().Name + " message=" + LegacyTransientReconnect.Clean(error.Message));
				}
			}
		}

		private bool ClientClaimsOwned()
		{
			if (clientClaims.Count == 0) return false;
			for (int i = 0; i < clientClaims.Count; i++)
			{
				ClientViewClaim claim = clientClaims[i];
				if (!claim.Prepared || claim.View == null || claim.View.viewID != claim.NewId
					|| !claim.View.isMine || claim.View.owner != Network.player) return false;
			}
			return true;
		}

		private void ReleaseClientMachineTraffic()
		{
			if (!machineTrafficGated) return;
			try { Network.SetSendingEnabled(MachineNetworkGroup, true); }
			catch { }
			NetworkPlayer[] peers;
			try { peers = Network.connections; }
			catch { peers = new NetworkPlayer[0]; }
			for (int i = 0; i < peers.Length; i++)
			{
				try { Network.SetReceivingEnabled(peers[i], MachineNetworkGroup, true); }
				catch { }
			}
			machineTrafficGated = false;
		}

		private void ReceiveGhostPose(int protocol, NetworkViewID viewId, int snapshotId,
			int bodyCount, int chunkIndex, int chunkCount, byte[] chunk)
		{
			ReceiveGhostPoseCore(protocol, viewId, snapshotId, bodyCount, chunkIndex, chunkCount,
				chunk, false, null);
		}

		private void ReceiveGhostPoseCore(int protocol, NetworkViewID viewId, int snapshotId,
			int bodyCount, int chunkIndex, int chunkCount, byte[] chunk, bool liveRestore,
			string restoreToken)
		{
			bool roleReady = liveRestore
				? clientRole && Network.isClient || serverRole && Network.isServer
				: clientRole && Network.isClient;
			if (protocol != (liveRestore ? 3 : 2) || !initialized || !roleReady
				|| liveRestore && !LegacyTransientReconnect.ValidToken(restoreToken)
				|| viewId == NetworkViewID.unassigned || snapshotId <= 0
				|| bodyCount <= 0 || bodyCount > LegacyGhostPoseSnapshotCodec.MaximumBodies
				|| chunkCount <= 0 || chunkCount > LegacyGhostPoseTransportCodec.MaximumChunks
				|| chunkIndex < 0 || chunkIndex >= chunkCount || chunk == null || chunk.Length <= 0
				|| chunk.Length > LegacyGhostPoseTransportCodec.MaximumChunkBytes)
			{
				LegacyTransientReconnect.Log((liveRestore ? "CRASH_RESTORE" : "CLIENT_GHOST")
					+ "_POSE_REJECTED view="
					+ LegacyTransientReconnect.IdLabel(viewId) + " snapshot=" + snapshotId
					+ " reason=invalid-envelope");
				return;
			}

			if (liveRestore)
			{
				int delivery = restoreDeliveries.Observe(viewId, restoreToken, snapshotId);
				if (delivery != 0)
				{
					// Only the first chunk repeats an ACK; never run body/control writes again.
					if (delivery == 1 && chunkIndex == 0)
					{
						NetworkView appliedView = LegacyTransientReconnect.FindViewById(viewId);
						MachineController appliedMachine = appliedView == null ? null
							: LegacyTransientReconnect.FindMachineForView(appliedView);
						if (appliedMachine != null && restoreDeliveries.MatchesGeneration(viewId,
							restoreToken, snapshotId, appliedMachine))
						{
							if (clientRole && appliedView.isMine && restoreToken == token)
								SendCrashOwnerApplied(restoreToken, viewId, snapshotId);
							else if (serverRole && Network.isServer)
								LegacyTransientReconnect.HandleCrashServerApplied(this, restoreToken, viewId, snapshotId);
						}
					}
					if (chunkIndex == 0) LegacyTransientReconnect.Log("CRASH_RESTORE_DELIVERY_IGNORED view="
						+ LegacyTransientReconnect.IdLabel(viewId) + " snapshot=" + snapshotId
						+ " reason=" + (delivery == 1 ? "already-applied" : "stale"));
					return;
				}
				if (clientRole && restoreToken == token && (!crashResumePending || viewId != crashFreshViewId))
				{
					if (chunkIndex == 0) LegacyTransientReconnect.Log("CRASH_RESTORE_DELIVERY_IGNORED reason=no-active-owner-request");
					return;
				}
			}
			PendingGhostPose pending = null;
			for (int index = pendingGhostPoses.Count - 1; index >= 0; index--)
			{
				PendingGhostPose candidate = pendingGhostPoses[index];
				if (candidate.ViewId != viewId || candidate.LiveRestore != liveRestore) continue;
				if (candidate.SnapshotId == snapshotId
					&& candidate.RestoreToken == restoreToken) { pending = candidate; continue; }
				pendingGhostPoses.RemoveAt(index);
				LegacyTransientReconnect.Log("CLIENT_GHOST_POSE_REPLACED view="
					+ LegacyTransientReconnect.IdLabel(viewId) + " oldSnapshot="
					+ candidate.SnapshotId + " newSnapshot=" + snapshotId);
			}
			if (pending == null)
			{
				ReleaseClientGhost(viewId, viewId, "snapshot-replaced");
				pending = new PendingGhostPose();
				pending.ViewId = viewId;
				pending.SnapshotId = snapshotId;
				pending.BodyCount = bodyCount;
				pending.Chunks = new byte[chunkCount][];
				pending.NextAttempt = float.MaxValue;
				pending.NextLog = Time.realtimeSinceStartup;
				pending.Expires = Time.realtimeSinceStartup + 45f;
				pending.LiveRestore = liveRestore;
				pending.RestoreToken = restoreToken;
				pendingGhostPoses.Add(pending);
			}
			if (pending.Snapshot != null && pending.Chunks == null) return;
			if (pending.BodyCount != bodyCount || pending.Chunks == null
				|| pending.Chunks.Length != chunkCount)
			{
				pendingGhostPoses.Remove(pending);
				LegacyTransientReconnect.Log("CLIENT_GHOST_POSE_REJECTED view="
					+ LegacyTransientReconnect.IdLabel(viewId) + " snapshot=" + snapshotId
					+ " reason=inconsistent-envelope");
				return;
			}
			if (pending.Chunks[chunkIndex] != null) return;
			byte[] copy = new byte[chunk.Length];
			Buffer.BlockCopy(chunk, 0, copy, 0, chunk.Length);
			pending.Chunks[chunkIndex] = copy;
			pending.ReceivedChunks++;
			if (pending.ReceivedChunks == 1 || pending.ReceivedChunks == chunkCount)
				LegacyTransientReconnect.Log("CLIENT_GHOST_POSE_CHUNK view="
					+ LegacyTransientReconnect.IdLabel(viewId) + " snapshot=" + snapshotId
					+ " received=" + pending.ReceivedChunks + "/" + chunkCount);
			if (pending.ReceivedChunks != chunkCount) return;

			byte[] payload;
			string reason;
			LegacyGhostPoseSnapshot snapshot;
			if (!LegacyGhostPoseTransportCodec.TryJoin(pending.Chunks, out payload, out reason)
				|| !LegacyGhostPoseSnapshotCodec.TryDecode(payload, out snapshot, out reason)
				|| snapshot.SnapshotId != snapshotId || snapshot.Bodies == null || snapshot.Visuals == null
				|| snapshot.Bodies.Length != bodyCount)
			{
				pendingGhostPoses.Remove(pending);
				LegacyTransientReconnect.Log("CLIENT_GHOST_POSE_REJECTED view="
					+ LegacyTransientReconnect.IdLabel(viewId) + " snapshot=" + snapshotId
					+ " reason=" + LegacyTransientReconnect.Clean(reason ?? "metadata-mismatch"));
				return;
			}
			pending.Snapshot = snapshot;
			pending.Chunks = null;
			pending.NextAttempt = Time.realtimeSinceStartup + 0.75f;
			LegacyTransientReconnect.Log((liveRestore ? "CRASH_RESTORE" : "CLIENT_GHOST")
				+ "_POSE_QUEUED view="
				+ LegacyTransientReconnect.IdLabel(viewId) + " snapshot=" + snapshotId
				+ " bodies=" + bodyCount + " visuals=" + snapshot.Visuals.Length
				+ " bytes=" + payload.Length
				+ " fingerprint=" + snapshot.Fingerprint);
		}

		private void ApplyPendingGhostPoses(float now)
		{
			if (pendingGhostPoses.Count == 0 || !clientRole && !serverRole) return;
			for (int index = pendingGhostPoses.Count - 1; index >= 0; index--)
			{
				PendingGhostPose pending = pendingGhostPoses[index];
				if (!clientRole && !pending.LiveRestore) continue;
				if (now >= pending.Expires)
				{
					LegacyTransientReconnect.Log("CLIENT_GHOST_POSE_EXPIRED view="
						+ LegacyTransientReconnect.IdLabel(pending.ViewId) + " snapshot="
						+ pending.SnapshotId + " received=" + pending.ReceivedChunks + "/"
						+ (pending.Chunks == null ? pending.ReceivedChunks : pending.Chunks.Length)
						+ " reason=" + LegacyTransientReconnect.Clean(pending.WaitReason));
					pendingGhostPoses.RemoveAt(index);
					continue;
				}
				if (pending.Snapshot == null || now < pending.NextAttempt) continue;
				string reason;
				bool permanent;
				if (TryApplyGhostPose(pending, out reason, out permanent))
				{
					pendingGhostPoses.RemoveAt(index);
					continue;
				}
				if (permanent)
				{
					LegacyTransientReconnect.Log("CLIENT_GHOST_POSE_REJECTED view="
						+ LegacyTransientReconnect.IdLabel(pending.ViewId) + " snapshot="
						+ pending.SnapshotId + " reason=" + LegacyTransientReconnect.Clean(reason));
					pendingGhostPoses.RemoveAt(index);
					continue;
				}
				pending.NextAttempt = now + 0.25f;
				if (pending.WaitReason != reason || now >= pending.NextLog)
				{
					pending.WaitReason = reason;
					pending.NextLog = now + 2f;
					LegacyTransientReconnect.Log("CLIENT_GHOST_POSE_WAIT view="
						+ LegacyTransientReconnect.IdLabel(pending.ViewId) + " snapshot="
						+ pending.SnapshotId + " reason=" + LegacyTransientReconnect.Clean(reason));
				}
			}
		}

		private void ScheduleRemoteCrashPresentationRefresh(PendingGhostPose pending,
			MachineController machine)
		{
			if (pending == null || machine == null) return;
			ReconcileRemoteCrashPresentation(machine, pending.ViewId, pending.SnapshotId, 0);
			float now = Time.realtimeSinceStartup;
			for (int index = pendingRemotePresentationRefreshes.Count - 1; index >= 0; index--)
			{
				PendingRemotePresentationRefresh refresh = pendingRemotePresentationRefreshes[index];
				if (refresh.ViewId != pending.ViewId || refresh.SnapshotId != pending.SnapshotId) continue;
				refresh.Snapshot = pending.Snapshot;
				refresh.Attempt = 1;
				refresh.NextAttempt = now + 0.5f;
				if (!refresh.EarlyRegistrationPending && !refresh.AwaitNativeRegistration)
					refresh.Expires = now + 6f;
				return;
			}
			PendingRemotePresentationRefresh scheduled = new PendingRemotePresentationRefresh();
			scheduled.ViewId = pending.ViewId;
			scheduled.SnapshotId = pending.SnapshotId;
			scheduled.Snapshot = pending.Snapshot;
			scheduled.Attempt = 1;
			scheduled.NextAttempt = now + 0.5f;
			scheduled.Expires = now + 6f;
			pendingRemotePresentationRefreshes.Add(scheduled);
		}

		internal void CompleteRemoteCrashPresentation(NetworkViewID viewId, int snapshotId)
		{
			if (!serverRole || !Network.isServer) return;
			float now = Time.realtimeSinceStartup;
			for (int index = pendingRemotePresentationRefreshes.Count - 1; index >= 0; index--)
			{
				PendingRemotePresentationRefresh refresh = pendingRemotePresentationRefreshes[index];
				if (refresh.ViewId != viewId || refresh.SnapshotId != snapshotId) continue;
				refresh.EarlyRegistrationPending = true;
				refresh.AwaitNativeRegistration = true;
				float registrationAt = now + 0.25f;
				if (refresh.NextAttempt > registrationAt) refresh.NextAttempt = registrationAt;
				refresh.NextWaitLog = now;
				if (refresh.Expires < now + 45f) refresh.Expires = now + 45f;
				LegacyTransientReconnect.Log("REMOTE_CRASH_HOST_EARLY_REGISTRATION_QUEUED view="
					+ LegacyTransientReconnect.IdLabel(viewId) + " snapshot=" + snapshotId
					+ " delaySeconds=0.25 nativeWaitTimeoutSeconds=45 existing=true");
				return;
			}
			PendingRemotePresentationRefresh scheduled = new PendingRemotePresentationRefresh();
			scheduled.ViewId = viewId;
			scheduled.SnapshotId = snapshotId;
			scheduled.Attempt = 1;
			scheduled.NextAttempt = now + 0.25f;
			scheduled.NextWaitLog = now;
			scheduled.Expires = now + 45f;
			scheduled.EarlyRegistrationPending = true;
			scheduled.AwaitNativeRegistration = true;
			pendingRemotePresentationRefreshes.Add(scheduled);
			LegacyTransientReconnect.Log("REMOTE_CRASH_HOST_EARLY_REGISTRATION_QUEUED view="
				+ LegacyTransientReconnect.IdLabel(viewId) + " snapshot=" + snapshotId
				+ " delaySeconds=0.25 nativeWaitTimeoutSeconds=45 existing=false");
		}

		internal void ObserveNativePlayerName(MachineController machine)
		{
			if (initialized && clientRole && machine != null)
			{
				NetworkView namedView = machine.GetComponent<NetworkView>();
				if (namedView != null)
					foreach (KeyValuePair<NetworkViewID, NetworkViewID> pair in peerRebindIds)
						if (pair.Key == namedView.viewID || pair.Value == namedView.viewID)
							TryApplyPeerRebind(pair.Key, pair.Value, "native-name-replay");
			}
			if (!serverRole || !Network.isServer || machine == null) return;
			NetworkView view = machine.GetComponent<NetworkView>();
			if (view == null) return;
			float now = Time.realtimeSinceStartup;
			for (int index = pendingRemotePresentationRefreshes.Count - 1; index >= 0; index--)
			{
				PendingRemotePresentationRefresh pending = pendingRemotePresentationRefreshes[index];
				if (pending.ViewId != view.viewID) continue;
				pending.NativeNameObserved = true;
				pending.NextAttempt = now;
				if (pending.Expires < now + 8f) pending.Expires = now + 8f;
				LegacyTransientReconnect.Log("REMOTE_CRASH_NATIVE_PLAYER_NAME view="
					+ LegacyTransientReconnect.IdLabel(view.viewID) + " snapshot=" + pending.SnapshotId
					+ " name=" + LegacyTransientReconnect.Clean(machine.name) + " playerId="
					+ machine.AKAFEPJIFKC + " localId=" + machine.LCKDHPKIPEI + " occurrences="
					+ CountRemoteMachineOccurrences(machine));
			}
		}

		private void ApplyPendingRemoteCrashPresentationRefreshes(float now)
		{
			for (int index = pendingRemotePresentationRefreshes.Count - 1; index >= 0; index--)
			{
				PendingRemotePresentationRefresh pending = pendingRemotePresentationRefreshes[index];
				if (now >= pending.Expires)
				{
					LegacyTransientReconnect.Log("REMOTE_CRASH_PRESENTATION_REFRESH_EXPIRED view="
						+ LegacyTransientReconnect.IdLabel(pending.ViewId) + " snapshot="
						+ pending.SnapshotId + " attempts=" + pending.Attempt
						+ " earlyRegistrationPending=" + pending.EarlyRegistrationPending
						+ " awaitingNativeRegistration=" + pending.AwaitNativeRegistration
						+ " nativeNameObserved=" + pending.NativeNameObserved);
					pendingRemotePresentationRefreshes.RemoveAt(index);
					continue;
				}
				if (now < pending.NextAttempt) continue;
				NetworkView view = LegacyTransientReconnect.FindViewById(pending.ViewId);
				MachineController machine = LegacyTransientReconnect.FindMachineForView(view);
				if (machine == null)
				{
					pending.NextAttempt = now + 0.25f;
					continue;
				}
				if (pending.EarlyRegistrationPending && serverRole && Network.isServer)
				{
					if (!EnsureRemoteCrashHostEarlyRegistration(machine, pending.ViewId,
						pending.SnapshotId))
					{
						pending.NextAttempt = now + 0.25f;
						continue;
					}
					pending.EarlyRegistrationPending = false;
					pending.NextWaitLog = now;
				}
				if (pending.AwaitNativeRegistration && serverRole && Network.isServer)
				{
					int occurrences = CountRemoteMachineOccurrences(machine);
					if (!pending.NativeNameObserved)
					{
						if (now >= pending.NextWaitLog)
						{
							pending.NextWaitLog = now + 2f;
							LegacyTransientReconnect.Log("REMOTE_CRASH_HOST_NATIVE_REGISTRATION_WAIT view="
								+ LegacyTransientReconnect.IdLabel(pending.ViewId) + " snapshot="
								+ pending.SnapshotId + " nativeNameObserved="
								+ pending.NativeNameObserved + " occurrences=" + occurrences);
						}
						pending.NextAttempt = now + 0.5f;
						continue;
					}
					if (!NormalizeRemoteCrashHostRegistration(machine, pending.ViewId,
						pending.SnapshotId, pending.NativeNameObserved))
					{
						pending.NextAttempt = now + 0.25f;
						continue;
					}
					pending.AwaitNativeRegistration = false;
					pending.NativeRegistrationReady = true;
					pending.Attempt = 0;
					pending.Expires = now + 6f;
				}
				// RPC_SyncPlayerName only positions the root LoD group locally. The
				// owner's live stream now owns body/section motion; replaying the crash
				// snapshot in world space here splits bounds when the owner moves.
				if (pending.NativeRegistrationReady && pending.Attempt == 0)
					LegacyTransientReconnect.Log("REMOTE_CRASH_NATIVE_LIVE_POSE view="
						+ LegacyTransientReconnect.IdLabel(pending.ViewId)
						+ " snapshot=" + pending.SnapshotId + " savedWorldReplay=false");
				ReconcileRemoteCrashPresentation(machine, pending.ViewId, pending.SnapshotId,
					pending.Attempt);
				pending.Attempt++;
				if (pending.Attempt >= 4)
				{
					pendingRemotePresentationRefreshes.RemoveAt(index);
					continue;
				}
				pending.NextAttempt = now + (pending.Attempt == 2 ? 1f : 1.5f);
			}
		}

		private static bool EnsureRemoteCrashHostEarlyRegistration(MachineController machine,
			NetworkViewID viewId, int snapshotId)
		{
			if (machine == null) return false;
			bool added = false;
			bool listed = false;
			int listCount = -1;
			string failure = "none";
			try
			{
				if (!Arena.PBBCHKBJAEA.Contains(machine))
				{
					Arena.PBBCHKBJAEA.Add(machine);
					added = true;
				}
				listed = Arena.PBBCHKBJAEA.Contains(machine);
				listCount = Arena.PBBCHKBJAEA.Count;
				if (listed) Game.IGEAEEAMAPM = true;
			}
			catch (Exception error) { failure = error.GetType().Name; }
			LegacyTransientReconnect.Log("REMOTE_CRASH_HOST_EARLY_REGISTRATION view="
				+ LegacyTransientReconnect.IdLabel(viewId) + " snapshot=" + snapshotId
				+ " added=" + added + " listed=" + listed + " listCount=" + listCount
				+ " entityKind=" + machine.GEOICBCHPNG + " playerId=" + machine.AKAFEPJIFKC
				+ " localId=" + machine.LCKDHPKIPEI + " failure=" + failure);
			return listed;
		}

		private static int CountRemoteMachineOccurrences(MachineController machine)
		{
			if (machine == null) return 0;
			int count = 0;
			try
			{
				for (int index = 0; index < Arena.PBBCHKBJAEA.Count; index++)
					if (object.ReferenceEquals(Arena.PBBCHKBJAEA[index], machine)) count++;
			}
			catch { }
			return count;
		}

		private static bool NormalizeRemoteCrashHostRegistration(MachineController machine,
			NetworkViewID viewId, int snapshotId, bool nativeNameObserved)
		{
			if (machine == null) return false;
			bool listed = false;
			int occurrencesBefore = 0;
			int duplicatesRemoved = 0;
			int listCount = -1;
			string failure = "none";
			try
			{
				for (int index = Arena.PBBCHKBJAEA.Count - 1; index >= 0; index--)
				{
					if (!object.ReferenceEquals(Arena.PBBCHKBJAEA[index], machine)) continue;
					occurrencesBefore++;
					if (occurrencesBefore <= 1) continue;
					Arena.PBBCHKBJAEA.RemoveAt(index);
					duplicatesRemoved++;
				}
				listed = Arena.PBBCHKBJAEA.Contains(machine);
				listCount = Arena.PBBCHKBJAEA.Count;
				if (listed) Game.IGEAEEAMAPM = true;
			}
			catch (Exception error) { failure = error.GetType().Name; }
			LegacyTransientReconnect.Log("REMOTE_CRASH_HOST_NATIVE_REGISTRATION view="
				+ LegacyTransientReconnect.IdLabel(viewId) + " snapshot=" + snapshotId
				+ " nativeNameObserved=" + nativeNameObserved + " listed=" + listed
				+ " occurrencesBefore=" + occurrencesBefore + " duplicatesRemoved="
				+ duplicatesRemoved + " listCount=" + listCount
				+ " entityKind=" + machine.GEOICBCHPNG + " playerId=" + machine.AKAFEPJIFKC
				+ " localId=" + machine.LCKDHPKIPEI + " failure=" + failure);
			return listed;
		}

		private static void ReconcileRemoteCrashPresentation(MachineController machine,
			NetworkViewID viewId, int snapshotId, int attempt)
		{
			if (machine == null) return;
			bool tagTargetUpdated = false;
			bool boundUpdated = false;
			string boundFailure = "none";
			GameObject tagObject = null;
			try
			{
				tagObject = machine.IBIEMAGMJAG;
				TagController tag = tagObject == null ? null : tagObject.GetComponent<TagController>();
				if (tag != null)
				{
					tag.SetTarget(machine);
					tagTargetUpdated = true;
				}
			}
			catch (Exception error) { boundFailure = "tag-" + error.GetType().Name; }

			try
			{
				machine.UpdateBound();
				machine.JGNHKGBMPOG = machine.ALOGCHKBFCM.center;
				boundUpdated = true;
			}
			catch (Exception error) { boundFailure = "bound-" + error.GetType().Name; }

			float distance = -1f;
			int lodDistance = -1;
			bool highDetail = false;
			bool lodApplied = false;
			int lodGroups = 0;
			int highRenderers = 0;
			int highRenderersEnabled = 0;
			int lowRenderers = 0;
			int lowRenderersEnabled = 0;
			string lodFailure = "none";
			try
			{
				Transform camera = Arena.KKMCEAAFKLA;
				if (camera != null)
				{
					distance = Vector3.Distance(camera.position, machine.JGNHKGBMPOG);
					machine.BBKOMHJGBPA = distance;
					lodDistance = JKGKJLLFMLE.IGOBPLOLHEP == null
						? -1 : JKGKJLLFMLE.IGOBPLOLHEP.lodDistance;
					if (lodDistance > 0 && machine.LIAKECLNHBL == null
						&& machine.BPKNDFJCENJ != null)
					{
						highDetail = distance <= lodDistance + 10f;
						for (int groupIndex = machine.BPKNDFJCENJ.Count - 1;
							groupIndex >= 0; groupIndex--)
						{
							NMLMDCCDFPN group = machine.BPKNDFJCENJ[groupIndex];
							if (group == null) continue;
							lodGroups++;
							group.POOIMIBHKBO = !highDetail;
							group.MHPPLEHPJHH(highDetail);
							if (group.CMGPPCCLOIK != null)
								for (int rendererIndex = 0; rendererIndex < group.CMGPPCCLOIK.Count;
									rendererIndex++)
								{
									MeshRenderer renderer = group.CMGPPCCLOIK[rendererIndex];
									if (renderer == null) continue;
									highRenderers++;
									if (renderer.enabled) highRenderersEnabled++;
								}
							if (group.AHKKOFELKFM != null)
								for (int rendererIndex = 0; rendererIndex < group.AHKKOFELKFM.Count;
									rendererIndex++)
								{
									MeshRenderer renderer = group.AHKKOFELKFM[rendererIndex];
									if (renderer == null) continue;
									lowRenderers++;
									if (renderer.enabled) lowRenderersEnabled++;
								}
						}
						lodApplied = true;
					}
					machine.PGLHDPCAGEM = distance;
				}
			}
			catch (Exception error) { lodFailure = error.GetType().Name; }

			try
			{
				machine.UpdateBound();
				machine.JGNHKGBMPOG = machine.ALOGCHKBFCM.center;
				boundUpdated = true;
			}
			catch (Exception error) { boundFailure = "final-bound-" + error.GetType().Name; }

			bool deadListed = false;
			bool remoteListed = false;
			try { deadListed = Game.IALNHPEKDON.Contains(machine.LCKDHPKIPEI); }
			catch { }
			try { remoteListed = Arena.PBBCHKBJAEA.Contains(machine); }
			catch { }
			if ((attempt == 0 || attempt == 3) && machine.BPKNDFJCENJ != null)
				for (int index = 0; index < machine.BPKNDFJCENJ.Count; index++)
				{
					NMLMDCCDFPN group = machine.BPKNDFJCENJ[index];
					if (group == null) continue;
					Vector3 minimum = Vector3.one * 999999f;
					Vector3 maximum = -minimum;
					try { group.JEDKGGNOEMI(ref minimum, ref maximum); }
					catch (Exception error)
					{
						LegacyTransientReconnect.Log("REMOTE_CRASH_LOD_GROUP_UNAVAILABLE group="
							+ index + " reason=" + error.GetType().Name);
						continue;
					}
					LegacyTransientReconnect.Log("REMOTE_CRASH_LOD_GROUP view="
						+ LegacyTransientReconnect.IdLabel(viewId) + " attempt=" + attempt + " group=" + index
						+ " primary=" + (group.NGLBLAGMBLN == null ? "null" : PoseVector(group.NGLBLAGMBLN.transform.position))
						+ " secondary=" + (group.DBOFGDKGCBM == null ? "null" : PoseVector(group.DBOFGDKGCBM.transform.position))
						+ " primaryLocal=" + (group.NGLBLAGMBLN == null ? "null" : PoseVector(group.NGLBLAGMBLN.transform.localPosition))
						+ " secondaryLocal=" + (group.DBOFGDKGCBM == null ? "null" : PoseVector(group.DBOFGDKGCBM.transform.localPosition))
						+ " min=" + PoseVector(minimum) + " max=" + PoseVector(maximum));
				}
			bool tagActive = tagObject != null && tagObject.activeSelf;
			LegacyTransientReconnect.Log("REMOTE_CRASH_PRESENTATION_REFRESH view="
				+ LegacyTransientReconnect.IdLabel(viewId) + " snapshot=" + snapshotId
				+ " attempt=" + attempt + " bound=" + boundUpdated + " boundFailure="
				+ boundFailure + " lodApplied=" + lodApplied + " lodFailure=" + lodFailure
				+ " distance=" + distance.ToString("0.000", CultureInfo.InvariantCulture)
				+ " lodDistance=" + lodDistance + " highDetail=" + highDetail + " lodGroups="
				+ lodGroups + " highRenderers=" + highRenderersEnabled + "/" + highRenderers
				+ " lowRenderers=" + lowRenderersEnabled + "/" + lowRenderers
				+ " tagTarget=" + tagTargetUpdated + " tagActive=" + tagActive
				+ " deadListed=" + deadListed + " remoteListed=" + remoteListed
				+ " root=" + PoseVector(machine.transform.position)
				+ " camera=" + (Arena.KKMCEAAFKLA == null ? "null" : PoseVector(Arena.KKMCEAAFKLA.position))
				+ " boundCenter=" + PoseVector(machine.ALOGCHKBFCM.center)
				+ " boundExtents=" + PoseVector(machine.ALOGCHKBFCM.extents)
				+ " rootSuspended=" + machine.EFCBCPOCOBB);
		}

		private bool TryApplyGhostPose(PendingGhostPose pending, out string reason, out bool permanent)
		{
			reason = null;
			permanent = false;
			NetworkView view = LegacyTransientReconnect.FindViewById(pending.ViewId);
			if (view == null) { reason = "view-not-ready"; return false; }
			if (!pending.LiveRestore && view.isMine)
			{
				reason = "view-is-local";
				permanent = true;
				return false;
			}
			MachineController machine = LegacyTransientReconnect.FindMachineForView(view);
			if (machine == null) { reason = "machine-not-ready"; return false; }
			if (pending.TargetMachine == null && object.ReferenceEquals(pending.TargetMachine, null))
				pending.TargetMachine = machine;
			if (!object.ReferenceEquals(pending.TargetMachine, machine))
			{
				reason = "machine-generation-changed";
				permanent = true;
				return false;
			}
			bool localOwnerRestore = pending.LiveRestore && clientRole && Network.isClient
				&& view.isMine && pending.RestoreToken == token;
			if (pending.LiveRestore && view.isMine && !localOwnerRestore)
			{
				reason = "restore-owner-token-mismatch";
				permanent = true;
				return false;
			}
			string fingerprint = "view=" + LegacyTransientReconnect.IdLabel(pending.ViewId)
				+ ";bodies=" + pending.BodyCount + ";visuals=" + pending.Snapshot.Visuals.Length;
			if (fingerprint != pending.Snapshot.Fingerprint)
			{
				reason = "ghost-view-identity-mismatch";
				permanent = true;
				return false;
			}
			if (localOwnerRestore)
			{
				if (!crashResumePending || pending.ViewId != crashFreshViewId
					|| !object.ReferenceEquals(machine, crashFreshMachine))
				{
					reason = "owner-restore-request-ended-or-generation-changed";
					permanent = true;
					return false;
				}
				if (game == null || game.FICMBCLEFDL != machine)
				{
					reason = "local-owned-machine-mismatch";
					return false;
				}
				if (!TryGetLocalCrashMachineReadiness(machine, out reason)) return false;
				return TryApplyOwnerCheckpointPose(pending, view, machine, out reason, out permanent);
			}
			MachineSerializer serializer = machine.GetComponent<MachineSerializer>();
			if (serializer == null) { reason = "machine-serializer-not-ready"; return false; }
			if (NativeStructureReadyField == null)
			{
				reason = "native-structure-ready-field-missing";
				permanent = true;
				return false;
			}
			try
			{
				if (!(bool)NativeStructureReadyField.GetValue(serializer))
				{
					reason = "native-structure-not-ready";
					return false;
				}
			}
			catch (Exception error)
			{
				reason = "native-structure-ready-" + error.GetType().Name;
				permanent = true;
				return false;
			}
			List<BodyController> machineBodies;
			string collectionSource;
			if (!LegacyTransientReconnect.TryCollectMachineBodies(machine, out machineBodies,
				out collectionSource, out reason)) return false;
			Dictionary<int, BodyController> bodiesByIndex = new Dictionary<int, BodyController>();
			for (int bodyIndex = 0; bodyIndex < machineBodies.Count; bodyIndex++)
				bodiesByIndex.Add(machineBodies[bodyIndex].BBLGKLFBJGE, machineBodies[bodyIndex]);
			BodyController[] matchedBodies = new BodyController[pending.BodyCount];
			for (int bodyIndex = 0; bodyIndex < pending.BodyCount; bodyIndex++)
			{
				LegacyGhostPoseBodyState state = pending.Snapshot.Bodies[bodyIndex];
				BodyController body;
				if (!bodiesByIndex.TryGetValue(state.Index, out body))
				{
					reason = "body-index-not-ready-" + state.Index + "-" + collectionSource;
					return false;
				}
				matchedBodies[bodyIndex] = body;
			}
			NMLMDCCDFPN[] matchedVisuals = new NMLMDCCDFPN[pending.Snapshot.Visuals.Length];
			if (pending.Snapshot.Visuals.Length > 0 && machine.CFECMHAACID == null)
			{
				reason = "visual-list-not-ready";
				return false;
			}
			for (int visualIndex = 0; visualIndex < pending.Snapshot.Visuals.Length; visualIndex++)
			{
				LegacyGhostPoseVisualState state = pending.Snapshot.Visuals[visualIndex];
				bool rootVisual = state.Index == LegacyGhostPoseSnapshotCodec.RootVisualIndex;
				if (rootVisual && (machine.BPKNDFJCENJ == null || machine.BPKNDFJCENJ.Count == 0))
				{
					reason = "visual-root-not-ready";
					return false;
				}
				if (!rootVisual && (state.Index < 0 || state.Index >= machine.CFECMHAACID.Count))
				{
					reason = "visual-index-not-ready-" + state.Index + "-list="
						+ machine.CFECMHAACID.Count;
					return false;
				}
				NMLMDCCDFPN visual = rootVisual ? machine.BPKNDFJCENJ[0]
					: machine.CFECMHAACID[state.Index];
				if (visual == null)
				{
					reason = "visual-record-not-ready-" + state.Index;
					return false;
				}
				if (state.PrimaryPresent && visual.NGLBLAGMBLN == null)
				{
					reason = "visual-primary-not-ready-" + state.Index;
					return false;
				}
				if (state.SecondaryPresent && visual.DBOFGDKGCBM == null)
				{
					reason = "visual-secondary-not-ready-" + state.Index;
					return false;
				}
				if (state.Auxiliaries.Length > 0 && visual.FLFCAMNNBIO == null)
				{
					reason = "visual-auxiliaries-not-ready-" + state.Index;
					return false;
				}
				for (int auxiliaryIndex = 0; auxiliaryIndex < state.Auxiliaries.Length; auxiliaryIndex++)
				{
					int nativeIndex = state.Auxiliaries[auxiliaryIndex].Index;
					if (nativeIndex < 0 || nativeIndex >= visual.FLFCAMNNBIO.Count
						|| visual.FLFCAMNNBIO[nativeIndex] == null)
					{
						reason = "visual-auxiliary-not-ready-" + state.Index + "-" + nativeIndex;
						return false;
					}
				}
				matchedVisuals[visualIndex] = visual;
			}
			ClientGhostFreeze ghost = new ClientGhostFreeze();
			ghost.ViewId = pending.ViewId;
			ghost.SnapshotId = pending.SnapshotId;
			ghost.View = view;
			ghost.Machine = machine;
			HashSet<int> seenBodies = new HashSet<int>();
			float maximumBodyDelta = 0f;
			float maximumVisualPositionDelta = 0f;
			float maximumVisualAngleDelta = 0f;
			float maximumVisualScaleDelta = 0f;
			bool applySnapshotDamage = ShouldApplySnapshotDamage(pending);
			try
			{
				for (int bodyIndex = 0; bodyIndex < pending.BodyCount; bodyIndex++)
				{
					BodyController body = matchedBodies[bodyIndex];
					FreezeClientBody(ghost, body.NFMPBACKJOJ, seenBodies);
					Rigidbody[] linked = body.IFCNJLPLNGF;
					if (linked != null)
						for (int linkedIndex = 0; linkedIndex < linked.Length; linkedIndex++)
							FreezeClientBody(ghost, linked[linkedIndex], seenBodies);
					ClientGhostBodyPose pose = new ClientGhostBodyPose();
					pose.Body = body;
					pose.State = pending.Snapshot.Bodies[bodyIndex];
					ghost.Poses.Add(pose);
				}
				for (int visualIndex = 0; visualIndex < matchedVisuals.Length; visualIndex++)
				{
					ClientGhostVisualPose pose = new ClientGhostVisualPose();
					pose.Visual = matchedVisuals[visualIndex];
					pose.State = pending.Snapshot.Visuals[visualIndex];
					ghost.VisualPoses.Add(pose);
				}
				for (int bodyIndex = 0; bodyIndex < ghost.Poses.Count; bodyIndex++)
					maximumBodyDelta = Math.Max(maximumBodyDelta,
						ApplyClientGhostBody(ghost.Poses[bodyIndex], applySnapshotDamage));
				for (int visualIndex = 0; visualIndex < ghost.VisualPoses.Count; visualIndex++)
					ApplyClientGhostVisual(ghost.VisualPoses[visualIndex], ref maximumVisualPositionDelta,
						ref maximumVisualAngleDelta, ref maximumVisualScaleDelta);
				if (applySnapshotDamage) machine.UpdateHealthEnergy();
				if (pending.LiveRestore)
				{
					machine.SelectBiggestBody();
					ReleaseFrozenClientBodies(ghost);
					ScheduleRemoteCrashPresentationRefresh(pending, machine);
					bool serverApplied = serverRole && Network.isServer;
					restoreDeliveries.Applied(pending.ViewId, pending.RestoreToken, pending.SnapshotId, machine);
					LegacyTransientReconnect.Log("CRASH_RESTORE_POSE_APPLIED token="
						+ LegacyTransientReconnect.TokenLabel(pending.RestoreToken) + " view="
						+ LegacyTransientReconnect.IdLabel(pending.ViewId) + " snapshot="
						+ pending.SnapshotId + " bodies=" + pending.BodyCount + " visuals="
						+ pending.Snapshot.Visuals.Length + " owner=False server="
						+ serverApplied + " frozen=false applyPath=remote-visuals"
						+ " damage=filtered-remote-clone source=" + collectionSource);
					if (serverApplied)
						LegacyTransientReconnect.HandleCrashServerApplied(this, pending.RestoreToken,
							pending.ViewId, pending.SnapshotId);
					return true;
				}
				clientGhostFreezes.Add(ghost);
				LegacyGhostPoseBodyState firstState = pending.Snapshot.Bodies[0];
				LegacyTransientReconnect.Log("CLIENT_GHOST_POSE_APPLIED view="
					+ LegacyTransientReconnect.IdLabel(pending.ViewId) + " snapshot="
					+ pending.SnapshotId + " bodies=" + pending.BodyCount + " visuals="
					+ ghost.VisualPoses.Count + " rigidbodies=" + ghost.Bodies.Count + " bodyDelta="
					+ maximumBodyDelta.ToString("0.000", CultureInfo.InvariantCulture)
					+ " visualPositionDelta="
					+ maximumVisualPositionDelta.ToString("0.000", CultureInfo.InvariantCulture)
					+ " visualAngleDelta="
					+ maximumVisualAngleDelta.ToString("0.000", CultureInfo.InvariantCulture)
					+ " visualScaleDelta="
					+ maximumVisualScaleDelta.ToString("0.000", CultureInfo.InvariantCulture)
					+ " damage=health+bonus+state frozen=true source=" + collectionSource
					+ " firstIndex=" + firstState.Index + " firstPos="
					+ firstState.PositionX.ToString("0.000", CultureInfo.InvariantCulture) + ","
					+ firstState.PositionY.ToString("0.000", CultureInfo.InvariantCulture) + ","
					+ firstState.PositionZ.ToString("0.000", CultureInfo.InvariantCulture)
					+ " firstBroken=" + firstState.Broken + " firstSuspended=" + firstState.Suspended);
				return true;
			}
			catch (Exception error)
			{
				ReleaseFrozenClientBodies(ghost);
				reason = "apply-" + error.GetType().Name;
				permanent = true;
				return false;
			}
		}

		private bool TryApplyOwnerCheckpointPose(PendingGhostPose pending, NetworkView view,
			MachineController machine, out string reason, out bool permanent)
		{
			long started = System.Diagnostics.Stopwatch.GetTimestamp();
			bool restored = false;
			if (pending.OwnerRestoreAttempts++ == 0)
			{
				pending.OwnerRestoreStarted = Time.realtimeSinceStartup;
				pending.OwnerRestoreStartFrame = Time.frameCount;
			}
			try
			{
				restored = ApplyOwnerCheckpointWhenReady(pending, view, machine, out reason, out permanent);
				return restored;
			}
			finally
			{
				pending.OwnerPreparationMilliseconds += (System.Diagnostics.Stopwatch.GetTimestamp() - started)
					* 1000d / System.Diagnostics.Stopwatch.Frequency;
				if (restored) LegacyTransientReconnect.Log("CLIENT_CRASH_RESTORE_FINISHED attempts=" + pending.OwnerRestoreAttempts
					+ " preparationTotalMs=" + pending.OwnerPreparationMilliseconds.ToString("0.000", CultureInfo.InvariantCulture)
					+ " waitSeconds=" + (Time.realtimeSinceStartup - pending.OwnerRestoreStarted).ToString("0.000", CultureInfo.InvariantCulture));
			}
		}

		private bool ApplyOwnerCheckpointWhenReady(PendingGhostPose pending, NetworkView view,
			MachineController machine, out string reason, out bool permanent)
		{
			reason = null;
			permanent = false;
			LegacyMachineRecoverySnapshot snapshot;
			float checkpointAgeSeconds;
			int checkpointBytes;
			if (!TryLoadOwnerCheckpoint(pending, out snapshot,
				out checkpointAgeSeconds, out checkpointBytes, out reason))
			{
				reason = "owner-checkpoint-" + (reason ?? "unavailable");
				return false;
			}

			LegacyMachineControlRecovery controls = null;
			if (snapshot.Controls != null)
			{
				try { controls = new LegacyMachineControlRecovery(machine); }
				catch (Exception error)
				{
					reason = "owner-controls-not-ready-" + error.Message;
					return false;
				}
			}
			if (!ValidateOwnerCheckpoint(machine, snapshot, out reason)) return false;
			if (controls != null)
			{
				try { controls.ValidateRestore(snapshot.Controls); }
				catch (Exception error)
				{
					reason = "owner-controls-rejected-" + error.Message;
					permanent = true;
					return false;
				}
			}
			else LegacyTransientReconnect.Log("CLIENT_CRASH_CONTROLS_UNAVAILABLE coverage=body-only source=checkpoint-v1");
			LegacyTransientReconnect.Log("CLIENT_CRASH_RESTORE_READY attempts=" + pending.OwnerRestoreAttempts
				+ " waitSeconds=" + (Time.realtimeSinceStartup - pending.OwnerRestoreStarted).ToString("0.000", CultureInfo.InvariantCulture)
				+ " frames=" + (Time.frameCount - pending.OwnerRestoreStartFrame)
				+ " previousAttemptsMs=" + pending.OwnerPreparationMilliseconds.ToString("0.000", CultureInfo.InvariantCulture)
				+ " checkpointReads=1 fullFingerprint=after-controls-ready");

			int bodyCount = snapshot.Bodies.Length;
			BodyController[] bodies = new BodyController[bodyCount];
			Rigidbody[] rigidbodies = new Rigidbody[bodyCount];
			bool[] originalKinematic = new bool[bodyCount];
			for (int index = 0; index < bodyCount; index++)
			{
				BodyController body = machine.ILBAAENKMBL[index];
				Rigidbody rigidbody = body == null ? null : body.NFMPBACKJOJ;
				if (body == null || rigidbody == null || body.BBLGKLFBJGE != index)
				{
					reason = "owner-checkpoint-body-not-ready-" + index;
					return false;
				}
				bodies[index] = body;
				rigidbodies[index] = rigidbody;
				originalKinematic[index] = rigidbody.isKinematic;
			}

			bool applied = false;
			float maximumBodyDelta = 0f;
			int brokenBodies = 0;
			int brokenJoints = 0;
			try
			{
				for (int index = 0; index < rigidbodies.Length; index++)
				{
					rigidbodies[index].velocity = Vector3.zero;
					rigidbodies[index].angularVelocity = Vector3.zero;
					rigidbodies[index].isKinematic = true;
				}
				for (int index = 0; index < bodies.Length; index++)
				{
					LegacyMachineRecoveryBodyState state = snapshot.Bodies[index];
					if (IsOwnerCheckpointDetachedBody(index, state)) brokenBodies++;
					maximumBodyDelta = Math.Max(maximumBodyDelta,
						ApplyOwnerCheckpointBody(bodies[index], state, index));
				}
				brokenJoints = ApplyOwnerCheckpointBrokenJoints(machine, snapshot);
				if (controls != null) controls.Restore(snapshot.Controls);
				machine.UpdateHealthEnergy();
				machine.SelectBiggestBody();
				for (int index = 0; index < rigidbodies.Length; index++)
					rigidbodies[index].isKinematic = snapshot.Bodies[index].IsKinematic;
				ResetOwnerMotionHistory(machine);
				applied = true;
			}
			catch (Exception error)
			{
				reason = "owner-checkpoint-apply-" + error.GetType().Name;
				permanent = true;
				return false;
			}
			finally
			{
				if (!applied)
					for (int index = 0; index < rigidbodies.Length; index++)
						try
						{
							if (rigidbodies[index] != null)
								rigidbodies[index].isKinematic = originalKinematic[index];
						}
						catch { }
			}

			LegacyMachineRecoveryBodyState rootState = snapshot.Bodies[0];
			LegacyTransientReconnect.Log("CRASH_RESTORE_POSE_APPLIED token="
				+ LegacyTransientReconnect.TokenLabel(pending.RestoreToken) + " view="
				+ LegacyTransientReconnect.IdLabel(pending.ViewId) + " snapshot="
				+ pending.SnapshotId + " bodies=" + bodyCount + " remoteBodies="
				+ pending.BodyCount + " visuals=0 remoteVisuals="
				+ pending.Snapshot.Visuals.Length + " owner=True server=False frozen=false"
				+ " applyPath=owner-checkpoint damage=owner-checkpoint source=local-physical-bodies"
				+ " checkpointSequence=" + snapshot.Sequence + " checkpointAgeSeconds="
				+ checkpointAgeSeconds.ToString("0.000", CultureInfo.InvariantCulture)
				+ " checkpointBytes=" + checkpointBytes + " brokenBodies=" + brokenBodies
				+ " brokenJoints=" + brokenJoints + " bodyDelta="
				+ maximumBodyDelta.ToString("0.000", CultureInfo.InvariantCulture)
				+ " rootPos=" + rootState.PositionX.ToString("0.000", CultureInfo.InvariantCulture)
				+ "," + rootState.PositionY.ToString("0.000", CultureInfo.InvariantCulture)
				+ "," + rootState.PositionZ.ToString("0.000", CultureInfo.InvariantCulture)
				+ " rootHealth=" + rootState.Health.ToString("0.000", CultureInfo.InvariantCulture)
				+ " rootBroken=" + rootState.Broken + " rootSuspended=" + rootState.Suspended);
			tracedOwner = machine;
			tracedOwnerSnapshot = snapshot;
			tracedOwnerControls = controls;
			ownerControls = controls;
			ownerControlSyncPending = controls != null;
			ownerTraceUntil = Time.realtimeSinceStartup + 30f;
			nextOwnerTrace = Time.realtimeSinceStartup;
			ownerTraceSamples = 0;
			restoreDeliveries.Applied(pending.ViewId, pending.RestoreToken, pending.SnapshotId, machine);
			SendCrashOwnerApplied(pending.RestoreToken, view.viewID, pending.SnapshotId);
			return true;
		}

		internal static void ValidateOwnerMotionFields()
		{
			if (OwnerPreviousCenterField == null || OwnerPreviousCenterField.FieldType != typeof(Vector3)
				|| OwnerPreviousRotationField == null || OwnerPreviousRotationField.FieldType != typeof(Quaternion)
				|| OwnerSmoothedMotionField == null || OwnerSmoothedMotionField.FieldType != typeof(Vector3))
				throw new MissingFieldException("MachineController crash restore motion history");
		}

		private static void ResetOwnerMotionHistory(MachineController machine)
		{
			Vector3 previous = (Vector3)OwnerPreviousCenterField.GetValue(machine);
			Vector3 center = machine.NFMPBACKJOJ.worldCenterOfMass;
			// Warp and Slide clear this sentinel after intentional relocation. The next
			// native FixedUpdate seeds the current COM instead of warping to old spawn.
			OwnerPreviousCenterField.SetValue(machine, Vector3.zero);
			OwnerPreviousRotationField.SetValue(machine, machine.NFMPBACKJOJ.rotation);
			OwnerSmoothedMotionField.SetValue(machine, Vector3.zero);
			LegacyTransientReconnect.Log("CLIENT_CRASH_MOTION_HISTORY_RESET previous=" + PoseVector(previous)
				+ " restoredCenter=" + PoseVector(center) + " previousDelta="
				+ Vector3.Distance(previous, center).ToString("0.000", CultureInfo.InvariantCulture)
				+ " nextBaseline=native-fixed-update rootKinematic=" + machine.NFMPBACKJOJ.isKinematic
				+ " rootSuspended=" + machine.EFCBCPOCOBB);
		}

		internal void TraceOwnerWarp(MachineController machine, string phase, Vector3 target, bool adjust)
		{
			if (machine == null || machine != tracedOwner || Time.realtimeSinceStartup > ownerTraceUntil) return;
			LegacyTransientReconnect.Log("CLIENT_CRASH_NATIVE_WARP phase=" + phase
				+ " target=" + PoseVector(target) + " adjust=" + adjust
				+ " root=" + PoseVector(machine.NFMPBACKJOJ.position)
				+ " previous=" + PoseVector((Vector3)OwnerPreviousCenterField.GetValue(machine))
				+ (phase == "before" ? " caller=" + Environment.StackTrace.Replace('\r', ' ').Replace('\n', '|') : ""));
		}

		private void TraceOwnerPose(float now)
		{
			if (tracedOwner == null || tracedOwnerSnapshot == null) return;
			if (now > ownerTraceUntil)
			{
				tracedOwner = null;
				tracedOwnerSnapshot = null;
				return;
			}
			if (now < nextOwnerTrace) return;
			ownerTraceSamples++;
			nextOwnerTrace = now + (ownerTraceSamples < 4 ? 0.02f : 1f);
			float maximumDelta = 0f;
			int bodies = Math.Min(tracedOwner.ILBAAENKMBL.Count, tracedOwnerSnapshot.Bodies.Length);
			for (int index = 0; index < bodies; index++)
			{
				BodyController body = tracedOwner.ILBAAENKMBL[index];
				if (body == null || body.NFMPBACKJOJ == null) continue;
				LegacyMachineRecoveryBodyState state = tracedOwnerSnapshot.Bodies[index];
				maximumDelta = Math.Max(maximumDelta, Vector3.Distance(body.NFMPBACKJOJ.position,
					new Vector3(state.PositionX, state.PositionY, state.PositionZ)));
			}
			LegacyTransientReconnect.Log("CLIENT_CRASH_OWNER_POSE_TRACE sample=" + ownerTraceSamples
				+ " bodies=" + bodies + " root=" + PoseVector(tracedOwner.NFMPBACKJOJ.position)
				+ " center=" + PoseVector(tracedOwner.NFMPBACKJOJ.worldCenterOfMass)
				+ " previous=" + PoseVector((Vector3)OwnerPreviousCenterField.GetValue(tracedOwner))
				+ " maxCheckpointDelta=" + maximumDelta.ToString("0.000", CultureInfo.InvariantCulture)
				+ (tracedOwnerControls == null ? " controls=unavailable" : tracedOwnerControls.Trace()));
		}

		private static string PoseVector(Vector3 value)
		{
			return value.x.ToString("0.000", CultureInfo.InvariantCulture) + ","
				+ value.y.ToString("0.000", CultureInfo.InvariantCulture) + ","
				+ value.z.ToString("0.000", CultureInfo.InvariantCulture);
		}

		private static int ApplyOwnerCheckpointBrokenJoints(MachineController machine,
			LegacyMachineRecoverySnapshot snapshot)
		{
			if (machine == null || machine.KBLANAFAJFP == null || machine.IBAOOHNFBCO == null
				|| snapshot == null || snapshot.Bodies == null) return 0;
			HashSet<GameObject> brokenRoots = new HashSet<GameObject>();
			int count = Math.Min(machine.KBLANAFAJFP.Count, snapshot.Bodies.Length);
			for (int index = 0; index < count; index++)
				if (IsOwnerCheckpointDetachedBody(index, snapshot.Bodies[index])
					&& machine.KBLANAFAJFP[index] != null)
					brokenRoots.Add(machine.KBLANAFAJFP[index]);
			if (brokenRoots.Count == 0) return 0;
			int brokenJoints = 0;
			for (int index = machine.IBAOOHNFBCO.Count - 1; index >= 0; index--)
			{
				JointController joint = machine.IBAOOHNFBCO[index];
				if (joint == null || !brokenRoots.Contains(joint.COKMFLEAMEK)
					&& !brokenRoots.Contains(joint.NMBGGPPICME)) continue;
				joint.BreakJoint();
				brokenJoints++;
			}
			return brokenJoints;
		}

		private static bool IsOwnerCheckpointDetachedBody(int bodyIndex,
			LegacyMachineRecoveryBodyState state)
		{
			// Body zero is the MachineController's root wrapper. In normal play it can
			// carry EFCBCPOCOBB=true while healthy and visible, so that flag must not
			// be interpreted as a detached body or used to break every root joint.
			return bodyIndex > 0 && (state.Broken || state.Suspended);
		}

		private static float ApplyOwnerCheckpointBody(BodyController body,
			LegacyMachineRecoveryBodyState state, int bodyIndex)
		{
			Rigidbody rigidbody = body.NFMPBACKJOJ;
			Vector3 position = new Vector3(state.PositionX, state.PositionY, state.PositionZ);
			Quaternion rotation = new Quaternion(state.RotationX, state.RotationY,
				state.RotationZ, state.RotationW);
			float delta = Vector3.Distance(rigidbody.position, position);
			body.MEJNIODBGFI = state.Health;
			body.HPOMNJCEJIP = state.BonusHealth;
			body.BCPNCJMHCAA = false;
			body.OKGLHFEAEEP = state.SuspendFrames;
			if (bodyIndex == 0)
			{
				// Preserve the root wrapper's intrinsic state without calling Suspend(),
				// which hides the main renderer and moves the object by ~99999 units.
				body.EFCBCPOCOBB = state.Suspended;
			}
			else if (state.Suspended)
			{
				if (!body.EFCBCPOCOBB) body.Suspend();
			}
			else body.EFCBCPOCOBB = false;
			body.transform.position = position;
			body.transform.rotation = rotation;
			rigidbody.position = position;
			rigidbody.rotation = rotation;
			rigidbody.velocity = Vector3.zero;
			rigidbody.angularVelocity = Vector3.zero;
			rigidbody.isKinematic = true;
			return delta;
		}

		private static bool ShouldApplySnapshotDamage(PendingGhostPose pending)
		{
			return pending != null && !pending.LiveRestore;
		}

		private static void FreezeClientBody(ClientGhostFreeze ghost, Rigidbody body,
			HashSet<int> seenBodies)
		{
			if (body == null || !seenBodies.Add(body.GetInstanceID())) return;
			ClientFrozenBody frozen = new ClientFrozenBody();
			frozen.Body = body;
			frozen.WasKinematic = body.isKinematic;
			ghost.Bodies.Add(frozen);
			body.velocity = Vector3.zero;
			body.angularVelocity = Vector3.zero;
			body.isKinematic = true;
		}

		private static float ApplyClientGhostBody(ClientGhostBodyPose pose, bool applyDamage)
		{
			if (pose == null || pose.Body == null || pose.Body.NFMPBACKJOJ == null) return 0f;
			BodyController body = pose.Body;
			LegacyGhostPoseBodyState state = pose.State;
			Vector3 position = new Vector3(state.PositionX, state.PositionY, state.PositionZ);
			Quaternion rotation = new Quaternion(state.RotationX, state.RotationY,
				state.RotationZ, state.RotationW);
			float delta = Vector3.Distance(body.NFMPBACKJOJ.position, position);
			if (applyDamage)
			{
				body.MEJNIODBGFI = state.Health;
				body.HPOMNJCEJIP = state.BonusHealth;
				body.BCPNCJMHCAA = false;
				body.EFCBCPOCOBB = state.Suspended;
				body.OKGLHFEAEEP = state.SuspendFrames;
				if (state.Suspended)
				{
					if (!body.EINNGJBAMAP) body.SetVisibility(false);
				}
				else if (body.EINNGJBAMAP) body.SetVisibility(true);
			}
			body.transform.position = position;
			body.transform.rotation = rotation;
			body.NFMPBACKJOJ.position = position;
			body.NFMPBACKJOJ.rotation = rotation;
			body.NFMPBACKJOJ.velocity = Vector3.zero;
			body.NFMPBACKJOJ.angularVelocity = Vector3.zero;
			body.NFMPBACKJOJ.isKinematic = true;
			Rigidbody[] linked = body.IFCNJLPLNGF;
			if (linked != null)
			{
				for (int linkedIndex = 0; linkedIndex < linked.Length; linkedIndex++)
				{
					Rigidbody rigidbody = linked[linkedIndex];
					if (rigidbody == null) continue;
					rigidbody.position = position;
					rigidbody.rotation = rotation;
					rigidbody.velocity = Vector3.zero;
					rigidbody.angularVelocity = Vector3.zero;
					rigidbody.isKinematic = true;
				}
			}
			return delta;
		}

		private static void ApplyClientGhostVisual(ClientGhostVisualPose pose,
			ref float maximumPositionDelta, ref float maximumAngleDelta, ref float maximumScaleDelta)
		{
			if (pose == null || pose.Visual == null) return;
			LegacyGhostPoseVisualState state = pose.State;
			// Codec v3 guarantees that the root frame is first. Restoring children
			// before this parent would create compensating 99999-unit local offsets.
			if (state.PrimaryPresent && pose.Visual.NGLBLAGMBLN != null)
				ApplyClientGhostTransform(pose.Visual.NGLBLAGMBLN.transform, state.Primary,
					ref maximumPositionDelta, ref maximumAngleDelta, ref maximumScaleDelta);
			if (state.SecondaryPresent && pose.Visual.DBOFGDKGCBM != null)
				ApplyClientGhostTransform(pose.Visual.DBOFGDKGCBM.transform, state.Secondary,
					ref maximumPositionDelta, ref maximumAngleDelta, ref maximumScaleDelta);
			if (state.Auxiliaries == null || state.Auxiliaries.Length == 0
				|| pose.Visual.FLFCAMNNBIO == null) return;
			for (int index = 0; index < state.Auxiliaries.Length; index++)
			{
				LegacyGhostPoseAuxiliaryState auxiliary = state.Auxiliaries[index];
				if (auxiliary.Index < 0 || auxiliary.Index >= pose.Visual.FLFCAMNNBIO.Count) continue;
				Transform transform = pose.Visual.FLFCAMNNBIO[auxiliary.Index];
				if (transform == null) continue;
				Vector3 scale = new Vector3(auxiliary.ScaleX, auxiliary.ScaleY, auxiliary.ScaleZ);
				maximumScaleDelta = Math.Max(maximumScaleDelta,
					Vector3.Distance(transform.localScale, scale));
				transform.localScale = scale;
			}
		}

		private static void ApplyClientGhostTransform(Transform transform,
			LegacyGhostPoseTransformState state, ref float maximumPositionDelta,
			ref float maximumAngleDelta, ref float maximumScaleDelta)
		{
			if (transform == null) return;
			Vector3 position = new Vector3(state.PositionX, state.PositionY, state.PositionZ);
			Quaternion rotation = new Quaternion(state.RotationX, state.RotationY,
				state.RotationZ, state.RotationW);
			Vector3 scale = new Vector3(state.ScaleX, state.ScaleY, state.ScaleZ);
			maximumPositionDelta = Math.Max(maximumPositionDelta,
				Vector3.Distance(transform.position, position));
			maximumAngleDelta = Math.Max(maximumAngleDelta,
				Quaternion.Angle(transform.rotation, rotation));
			maximumScaleDelta = Math.Max(maximumScaleDelta,
				Vector3.Distance(transform.localScale, scale));
			transform.position = position;
			transform.rotation = rotation;
			transform.localScale = scale;
		}

		private void ReleaseClientGhost(NetworkViewID oldId, NetworkViewID newId, string reason)
		{
			for (int index = clientGhostFreezes.Count - 1; index >= 0; index--)
			{
				ClientGhostFreeze ghost = clientGhostFreezes[index];
				NetworkViewID currentId = ghost.View == null ? NetworkViewID.unassigned : ghost.View.viewID;
				if (ghost.ViewId != oldId && ghost.ViewId != newId
					&& currentId != oldId && currentId != newId) continue;
				ReleaseFrozenClientBodies(ghost);
				clientGhostFreezes.RemoveAt(index);
				LegacyTransientReconnect.Log("CLIENT_GHOST_RELEASED old="
					+ LegacyTransientReconnect.IdLabel(oldId) + " new="
					+ LegacyTransientReconnect.IdLabel(newId) + " snapshot=" + ghost.SnapshotId
					+ " bodies=" + ghost.Bodies.Count + " visuals=" + ghost.VisualPoses.Count + " reason="
					+ LegacyTransientReconnect.Clean(reason));
			}
			for (int index = pendingGhostPoses.Count - 1; index >= 0; index--)
				if (pendingGhostPoses[index].ViewId == oldId || pendingGhostPoses[index].ViewId == newId)
					pendingGhostPoses.RemoveAt(index);
		}

		private static void ReleaseFrozenClientBodies(ClientGhostFreeze ghost)
		{
			for (int index = 0; index < ghost.Bodies.Count; index++)
			{
				ClientFrozenBody frozen = ghost.Bodies[index];
				try
				{
					if (frozen.Body == null) continue;
					frozen.Body.velocity = Vector3.zero;
					frozen.Body.angularVelocity = Vector3.zero;
					frozen.Body.isKinematic = frozen.WasKinematic;
				}
				catch { }
			}
		}

		private void ReleaseAllClientGhosts(string reason)
		{
			for (int index = clientGhostFreezes.Count - 1; index >= 0; index--)
			{
				ClientGhostFreeze ghost = clientGhostFreezes[index];
				ReleaseFrozenClientBodies(ghost);
				LegacyTransientReconnect.Log("CLIENT_GHOST_RELEASED old="
					+ LegacyTransientReconnect.IdLabel(ghost.ViewId) + " new=unassigned snapshot="
					+ ghost.SnapshotId + " bodies=" + ghost.Bodies.Count + " visuals="
					+ ghost.VisualPoses.Count + " reason="
					+ LegacyTransientReconnect.Clean(reason));
			}
			clientGhostFreezes.Clear();
		}

		private void CleanupClientGhosts()
		{
			for (int index = clientGhostFreezes.Count - 1; index >= 0; index--)
			{
				ClientGhostFreeze ghost = clientGhostFreezes[index];
				if (ghost.View != null) continue;
				clientGhostFreezes.RemoveAt(index);
				LegacyTransientReconnect.Log("CLIENT_GHOST_RELEASED old="
					+ LegacyTransientReconnect.IdLabel(ghost.ViewId) + " new=unassigned snapshot="
					+ ghost.SnapshotId + " bodies=" + ghost.Bodies.Count + " visuals="
					+ ghost.VisualPoses.Count + " reason=view-destroyed");
			}
		}

		private readonly Dictionary<NetworkViewID, NetworkViewID> peerRebindIds =
			new Dictionary<NetworkViewID, NetworkViewID>();

		private void ApplyOrQueuePeerRebind(NetworkViewID oldId, NetworkViewID newId)
		{
			if (oldId == NetworkViewID.unassigned || newId == NetworkViewID.unassigned) return;
			// Carry older buffered aliases through repeated reconnects. Entries
			// live only with this scene controller and are cleared on retirement.
			newId = LegacyPlayerListPolicy.RecordRebind(peerRebindIds, oldId, newId);
			ReleaseClientGhost(oldId, newId, "peer-rebind");
			if (TryApplyPeerRebind(oldId, newId, "rpc")) return;
			for (int i = 0; i < pendingPeerRebinds.Count; i++)
				if (pendingPeerRebinds[i].OldId == oldId && pendingPeerRebinds[i].NewId == newId) return;
			PendingPeerRebind pending = new PendingPeerRebind();
			pending.OldId = oldId;
			pending.NewId = newId;
			pending.NextAttempt = Time.realtimeSinceStartup + 0.5f;
			pending.Expires = Time.realtimeSinceStartup + 20f;
			pendingPeerRebinds.Add(pending);
			LegacyTransientReconnect.Log("PEER_REBIND_QUEUED old=" + LegacyTransientReconnect.IdLabel(oldId)
				+ " new=" + LegacyTransientReconnect.IdLabel(newId));
		}

		private void ApplyPendingPeerRebinds(float now)
		{
			if (pendingPeerRebinds.Count == 0 || Network.peerType == NetworkPeerType.Disconnected) return;
			for (int i = pendingPeerRebinds.Count - 1; i >= 0; i--)
			{
				PendingPeerRebind pending = pendingPeerRebinds[i];
				if (now < pending.NextAttempt) continue;
				pending.NewId = LegacyPlayerListPolicy.RecordRebind(peerRebindIds, pending.OldId, pending.NewId);
				if (TryApplyPeerRebind(pending.OldId, pending.NewId, "deferred") || now >= pending.Expires)
				{
					if (now >= pending.Expires)
						LegacyTransientReconnect.Log("PEER_REBIND_EXPIRED old="
							+ LegacyTransientReconnect.IdLabel(pending.OldId) + " new="
							+ LegacyTransientReconnect.IdLabel(pending.NewId));
					pendingPeerRebinds.RemoveAt(i);
				}
				else pending.NextAttempt = now + 0.5f;
			}
		}

		private static bool TryApplyPeerRebind(NetworkViewID oldId, NetworkViewID newId, string stage)
		{
			NetworkView current = null;
			current = LegacyTransientReconnect.FindViewById(newId);
			if (current != null)
			{
				LegacyPlayerList.ObserveRebind(oldId, newId, current);
				LegacyTransientReconnect.Log("PEER_REBIND_PRESENT stage=" + stage + " new="
					+ LegacyTransientReconnect.IdLabel(newId) + " owner="
					+ LegacyTransientReconnect.PlayerLabel(current.owner) + " mine=" + current.isMine);
				return true;
			}
			NetworkView oldView = null;
			oldView = LegacyTransientReconnect.FindViewById(oldId);
			if (oldView == null) return false;
			try { oldView.viewID = newId; }
			catch (Exception error)
			{
				LegacyTransientReconnect.Log("PEER_REBIND_FAILED stage=" + stage + " type="
					+ error.GetType().Name + " message=" + LegacyTransientReconnect.Clean(error.Message));
				return false;
			}
			bool applied = oldView.viewID == newId;
			if (applied) LegacyPlayerList.ObserveRebind(oldId, newId, oldView);
			LegacyTransientReconnect.Log("PEER_REBIND_APPLIED stage=" + stage + " old="
				+ LegacyTransientReconnect.IdLabel(oldId) + " new="
				+ LegacyTransientReconnect.IdLabel(newId) + " owner="
				+ LegacyTransientReconnect.PlayerLabel(oldView.owner) + " mine=" + oldView.isMine
				+ " applied=" + applied);
			return applied;
		}

		[RPC]
		private void MPatcherResumeHelloV2(int protocol, string incomingToken, string playerName,
			int reconnecting, NetworkMessageInfo info)
		{
			LegacyTransientReconnect.HandleHello(this, protocol, incomingToken, playerName, reconnecting, info);
		}

		[RPC]
		private void MPatcherCrashResumeHelloV3(int protocol, string incomingToken, string playerName,
			NetworkViewID freshViewId, NetworkMessageInfo info)
		{
			LegacyTransientReconnect.HandleCrashHello(this, protocol, incomingToken, playerName,
				freshViewId, null, null, false, info);
		}

		[RPC]
		private void MPatcherCrashResumeHelloV4(int protocol, string incomingToken, string playerName,
			NetworkViewID freshViewId, NetworkMessageInfo info)
		{
			LegacyTransientReconnect.HandleCrashHello(this, protocol, incomingToken, playerName,
				freshViewId, null, null, true, info);
		}

		[RPC]
		private void MPatcherCrashResumeHelloV5(int protocol, string incomingToken, string playerName,
			NetworkViewID freshViewId, string ownerCheckpointFingerprint, NetworkMessageInfo info)
		{
			LegacyTransientReconnect.HandleCrashHello(this, protocol, incomingToken, playerName,
				freshViewId, ownerCheckpointFingerprint, null, true, info);
		}

		[RPC]
		private void MPatcherCrashResumeHelloV6(int protocol, string incomingToken, string playerName,
			NetworkViewID freshViewId, string ownerCheckpointFingerprint,
			string freshOwnerFingerprint, NetworkMessageInfo info)
		{
			LegacyTransientReconnect.HandleCrashHello(this, protocol, incomingToken, playerName,
				freshViewId, ownerCheckpointFingerprint, freshOwnerFingerprint, true, info);
		}

		[RPC]
		private void MPatcherCrashResumeDecisionV4(int protocol, string incomingToken,
			NetworkViewID freshViewId, bool accept, NetworkMessageInfo info)
		{
			LegacyTransientReconnect.HandleCrashDecision(this, protocol, incomingToken,
				freshViewId, accept, null, info);
		}

		[RPC]
		private void MPatcherCrashResumeDecisionV5(int protocol, string incomingToken,
			NetworkViewID freshViewId, bool accept, NetworkMessageInfo info)
		{
			LegacyTransientReconnect.HandleCrashDecision(this, protocol, incomingToken,
				freshViewId, accept, null, info);
		}

		[RPC]
		private void MPatcherCrashResumeDecisionV6(int protocol, string incomingToken,
			NetworkViewID freshViewId, bool accept, string freshOwnerFingerprint,
			NetworkMessageInfo info)
		{
			LegacyTransientReconnect.HandleCrashDecision(this, protocol, incomingToken,
				freshViewId, accept, freshOwnerFingerprint, info);
		}

		[RPC]
		private void MPatcherCrashRestoreAppliedV3(int protocol, string incomingToken,
			NetworkViewID freshViewId, int snapshotId, NetworkMessageInfo info)
		{
			LegacyTransientReconnect.HandleCrashOwnerApplied(this, protocol, incomingToken,
				freshViewId, snapshotId, info);
		}

		[RPC]
		private void MPatcherCrashRestorePoseV3(int protocol, string incomingToken,
			NetworkViewID freshViewId, int snapshotId, int bodyCount, int chunkIndex,
			int chunkCount, byte[] chunk)
		{
			ReceiveGhostPoseCore(protocol, freshViewId, snapshotId, bodyCount, chunkIndex,
				chunkCount, chunk, true, incomingToken);
		}

		[RPC]
		private void MPatcherCrashResumeStatusV3(int protocol, string incomingToken, int status,
			string reason)
		{
			if (protocol != 3 || incomingToken != token || !clientRole || !crashResumePending) return;
			crashStatusSeen = true;
			if (status == 3 || status == 4)
			{
				CloseCrashRestoreConsent("terminal-status-" + status);
				crashResumePending = false;
				crashFreshMachine = null;
				crashProtocolFallbackAt = float.MaxValue;
				crashV3FallbackAt = float.MaxValue;
				crashUsingV3 = false;
				crashConsentDecisionSent = false;
				registered = true;
				suppressReconnectGuidelineUntil = Time.realtimeSinceStartup
					+ ReconnectGuidelineGraceSeconds;
				ReleaseCrashResumeTraffic();
				ArmActiveSession(status == 3 ? "crash-restore-complete" : "crash-fallback-fresh");
				if (status == 3)
					ShowLocalStatus("[MPatcher] Previous machine position and pose restored after crash.");
				LegacyTransientReconnect.Log("CLIENT_CRASH_RESUME_COMPLETE restored=" + (status == 3)
					+ " reason=" + LegacyTransientReconnect.Clean(reason) + " fresh="
					+ LegacyTransientReconnect.IdLabel(crashFreshViewId)
					+ " machineGroup=1 sending=true");
			}
			else if (status == 5)
			{
				LegacyTransientReconnect.Log("CLIENT_CRASH_RESUME_REJECTED reason="
					+ LegacyTransientReconnect.Clean(reason));
			}
			else if (status == 2)
			{
				CloseCrashRestoreConsent("restore-started");
				GateCrashResumeTraffic();
				LegacyTransientReconnect.Log("CLIENT_CRASH_RESUME_STATUS status=2 reason="
					+ LegacyTransientReconnect.Clean(reason));
			}
			else if (status == 6)
			{
				GateCrashResumeTraffic();
				if (crashConsentDecisionSent) SendCrashRestoreConsentDecision();
				else ShowCrashRestoreConsent();
				LegacyTransientReconnect.Log("CLIENT_CRASH_RESUME_STATUS status=6 reason="
					+ LegacyTransientReconnect.Clean(reason) + " decisionSent="
					+ crashConsentDecisionSent);
			}
			else
				LegacyTransientReconnect.Log("CLIENT_CRASH_RESUME_STATUS status=" + status
					+ " reason=" + LegacyTransientReconnect.Clean(reason));
		}

		[RPC]
		private void MPatcherResumeGoodbyeV2(int protocol, string incomingToken, NetworkMessageInfo info)
		{
			LegacyTransientReconnect.HandleGoodbye(this, protocol, incomingToken, info);
		}

		[RPC]
		private void MPatcherResumeTestDropV2(int protocol, string incomingToken, NetworkMessageInfo info)
		{
			LegacyTransientReconnect.HandleTestDrop(this, protocol, incomingToken, info);
		}

		[RPC]
		private void MPatcherResumeClaimV2(int protocol, string incomingToken, int claimIndex,
			int claimCount, NetworkViewID oldId, NetworkViewID newId, NetworkMessageInfo info)
		{
			LegacyTransientReconnect.HandleClaim(this, protocol, incomingToken, claimIndex,
				claimCount, oldId, newId, info);
		}

		[RPC]
		private void MPatcherResumeRetireV2(int protocol, string incomingToken, NetworkViewID oldId,
			NetworkViewID newId, NetworkMessageInfo info)
		{
			LegacyTransientReconnect.HandleRetire(this, protocol, incomingToken, oldId, newId, info);
		}

		[RPC]
		private void MPatcherResumeRebindV2(NetworkViewID oldId, NetworkViewID newId, NetworkMessageInfo info)
		{
			if (!IsServerMessage(info))
			{
				LegacyTransientReconnect.Log("PEER_REBIND_REJECTED reason=non-server");
				return;
			}
			ApplyOrQueuePeerRebind(oldId, newId);
		}

		private static bool IsServerMessage(NetworkMessageInfo info)
		{
			if (!Network.isClient) return false;
			NetworkPlayer[] peers = Network.connections;
			return peers != null && peers.Length == 1 && peers[0] == info.sender;
		}

		[RPC]
		private void MPatcherResumeRetiredV2(NetworkViewID oldId, NetworkViewID newId, NetworkMessageInfo info)
		{
			if (!IsServerMessage(info))
			{
				LegacyTransientReconnect.Log("PEER_RETIRE_REJECTED reason=non-server");
				return;
			}
			List<NetworkViewID> retiredAliases = LegacyPlayerListPolicy.ForgetRebind(peerRebindIds, oldId, newId);
			ReleaseClientGhost(oldId, newId, "peer-retired");
			for (int index = pendingPeerRebinds.Count - 1; index >= 0; index--)
			{
				PendingPeerRebind pending = pendingPeerRebinds[index];
				if (pending.OldId == oldId || pending.OldId == newId
					|| pending.NewId == oldId || pending.NewId == newId
					|| retiredAliases.Contains(pending.OldId) || retiredAliases.Contains(pending.NewId))
					pendingPeerRebinds.RemoveAt(index);
			}
			bool found = RetireClientView(newId);
			if (oldId != newId) found = RetireClientView(oldId) || found;
			for (int index = 0; index < retiredAliases.Count; index++)
				if (retiredAliases[index] != oldId && retiredAliases[index] != newId)
					found = RetireClientView(retiredAliases[index]) || found;
			LegacyTransientReconnect.Log("PEER_REBOUND_RETIRED old="
				+ LegacyTransientReconnect.IdLabel(oldId) + " new="
				+ LegacyTransientReconnect.IdLabel(newId) + " found=" + found);
		}

		private static bool RetireClientView(NetworkViewID viewId)
		{
			if (viewId == NetworkViewID.unassigned) return false;
			bool found = false;
			UnityEngine.Object[] views;
			try { views = UnityEngine.Object.FindObjectsOfType(typeof(NetworkView)); }
			catch (Exception error)
			{
				LegacyTransientReconnect.Log("PEER_REBOUND_RETIRE_SCAN_FAILED type=" + error.GetType().Name);
				return false;
			}
			for (int index = 0; index < views.Length; index++)
			{
				NetworkView view = views[index] as NetworkView;
				if (view == null || view.viewID != viewId) continue;
				found = true;
				GameObject root = view.gameObject;
				LegacyPlayerList.Remove(view.GetComponent<MachineController>(), "peer-retire");
				LegacyTransientReconnect.Log("PEER_RETIRED_OBJECT view=" + LegacyTransientReconnect.IdLabel(viewId)
					+ " name=" + LegacyTransientReconnect.Clean(root.name) + " position=" + Vector(root.transform.position));
				try { UnityEngine.Object.Destroy(root); }
				catch (Exception error)
				{
					LegacyTransientReconnect.Log("PEER_REBOUND_RETIRE_FAILED old="
						+ LegacyTransientReconnect.IdLabel(viewId) + " type=" + error.GetType().Name);
					continue;
				}
			}
			LegacyPlayerList.Normalize("peer-retire-complete", null);
			return found;
		}

		[RPC]
		private void MPatcherResumeGhostPoseV2(int protocol, NetworkViewID viewId, int snapshotId,
			int bodyCount, int chunkIndex, int chunkCount, byte[] chunk)
		{
			ReceiveGhostPose(protocol, viewId, snapshotId, bodyCount, chunkIndex, chunkCount, chunk);
		}

		[RPC]
		private void MPatcherResumeStatusV2(int protocol, string incomingToken, int status, string reason)
		{
			if (protocol != 2 || incomingToken != token || !clientRole) return;
			if (crashResumePending)
			{
				LegacyTransientReconnect.Log("CLIENT_STATUS_V2_IGNORED status=" + status
					+ " reason=crash-resume-pending");
				return;
			}
			if (status == 3)
			{
				LegacyTransientReconnect.Log("CLIENT_STATUS_REJECTED reason=" + LegacyTransientReconnect.Clean(reason));
				if (recovering) AbortRecovery("server-rejected-" + reason);
				return;
			}
			if (status == 5 && recovering)
			{
				LegacyTransientReconnect.Log("CLIENT_RECOVERY_WAIT reason=" + LegacyTransientReconnect.Clean(reason)
					+ " machineTrafficGated=" + machineTrafficGated);
				return;
			}
			bool firstPendingStatus = !registered;
			registered = true;
			if (status == 2)
			{
				bool owned = ClientClaimsOwned();
				LegacyPlayerList.Normalize("owner-rebind-complete", game == null ? null : game.FICMBCLEFDL);
				ReleaseClientMachineTraffic();
				if (!MigrationArmed) ReleaseMigrationMachines("rebind-complete");
				recovering = false;
				connecting = false;
				recoveryDeadline = float.MaxValue;
				rebindAuthorized = false;
				rebindNotBefore = float.MaxValue;
				suppressReconnectGuidelineUntil = Time.realtimeSinceStartup
					+ ReconnectGuidelineGraceSeconds;
				if (!MigrationArmed) ShowLocalStatus("[MPatcher] Connection restored. Machine control resumed.");
				LegacyTransientReconnect.Log("CLIENT_REBIND_COMPLETE claims=" + clientClaims.Count
					+ " allOwned=" + owned + " machineGroup=1 sending=true receiving=true");
				LogClientInventory("resume-explicit-rebind");
				ArmActiveSession("rebind-complete");
			}
			else if (status == 1)
			{
				crashFallbackHello = false;
				ArmActiveSession("registered");
			}
			else if (status == 4)
			{
				if (firstPendingStatus && !MigrationArmed)
					ShowLocalStatus("[MPatcher] Connection restored. Rebinding machine control...");
				AuthorizeClientRebind(reason);
				if (ClientClaimsPrepared()) SendClaims();
				LogClientInventory("resume-rebind-pending");
			}
			LegacyTransientReconnect.Log("CLIENT_STATUS status=" + status + " reason="
				+ LegacyTransientReconnect.Clean(reason) + " connectedCallback=" + connectedCallbackSeen);
		}

		internal void SendStatus(NetworkPlayer target, string outgoingToken, int status, string reason)
		{
			if (sessionView == null) return;
			try { sessionView.RPC("MPatcherResumeStatusV2", target, 2, outgoingToken, status, reason); }
			catch (Exception error) { LegacyTransientReconnect.Log("SERVER_STATUS_ACK_FAILED type=" + error.GetType().Name); }
		}

		internal void SendCrashStatus(NetworkPlayer target, string outgoingToken, int status,
			string reason)
		{
			if (sessionView == null) return;
			try
			{
				sessionView.RPC("MPatcherCrashResumeStatusV3", target, 3, outgoingToken, status, reason);
			}
			catch (Exception error)
			{
				LegacyTransientReconnect.Log("SERVER_CRASH_STATUS_FAILED type=" + error.GetType().Name);
			}
		}

		internal bool BroadcastCrashRestorePose(LegacyTransientReconnect.ResumeRecord record)
		{
			if (sessionView == null || record == null || record.CrashChunks == null
				|| record.CrashChunks.Length == 0) return false;
			try
			{
				for (int index = 0; index < record.CrashChunks.Length; index++)
					sessionView.RPC("MPatcherCrashRestorePoseV3", RPCMode.All, 3, record.Token,
						record.CrashFreshViewId, record.CrashSnapshotId, record.CrashBodyCount,
						index, record.CrashChunks.Length, record.CrashChunks[index]);
				LegacyTransientReconnect.Log("SERVER_CRASH_POSE_BROADCAST token="
					+ LegacyTransientReconnect.TokenLabel(record.Token) + " fresh="
					+ LegacyTransientReconnect.IdLabel(record.CrashFreshViewId) + " snapshot="
					+ record.CrashSnapshotId + " bodies=" + record.CrashBodyCount + " visuals="
					+ record.CrashVisualCount + " chunks=" + record.CrashChunks.Length);
				return true;
			}
			catch (Exception error)
			{
				LegacyTransientReconnect.Log("SERVER_CRASH_POSE_BROADCAST_FAILED token="
					+ LegacyTransientReconnect.TokenLabel(record.Token) + " type="
					+ error.GetType().Name + " message="
					+ LegacyTransientReconnect.Clean(error.Message));
				return false;
			}
		}

		private void SendCrashOwnerApplied(string outgoingToken, NetworkViewID freshViewId,
			int snapshotId)
		{
			if (sessionView == null || !Network.isClient) return;
			try
			{
				sessionView.RPC("MPatcherCrashRestoreAppliedV3", RPCMode.Server, 3,
					outgoingToken, freshViewId, snapshotId);
				LegacyTransientReconnect.Log("CLIENT_CRASH_OWNER_ACK_SENT token="
					+ LegacyTransientReconnect.TokenLabel(outgoingToken) + " fresh="
					+ LegacyTransientReconnect.IdLabel(freshViewId) + " snapshot=" + snapshotId);
			}
			catch (Exception error)
			{
				LegacyTransientReconnect.Log("CLIENT_CRASH_OWNER_ACK_FAILED type="
					+ error.GetType().Name + " message="
					+ LegacyTransientReconnect.Clean(error.Message));
			}
		}

		internal bool SendGhostPose(NetworkPlayer target, NetworkViewID viewId, int snapshotId,
			int bodyCount, int visualCount, byte[][] chunks)
		{
			if (sessionView == null || chunks == null || chunks.Length == 0) return false;
			try
			{
				for (int index = 0; index < chunks.Length; index++)
					sessionView.RPC("MPatcherResumeGhostPoseV2", target, 2, viewId, snapshotId,
						bodyCount, index, chunks.Length, chunks[index]);
				LegacyTransientReconnect.Log("SERVER_GHOST_POSE_SENT target="
					+ LegacyTransientReconnect.PlayerLabel(target) + " view="
					+ LegacyTransientReconnect.IdLabel(viewId) + " snapshot=" + snapshotId
					+ " bodies=" + bodyCount + " visuals=" + visualCount + " chunks=" + chunks.Length);
				return true;
			}
			catch (Exception error)
			{
				LegacyTransientReconnect.Log("SERVER_GHOST_POSE_SEND_FAILED target="
					+ LegacyTransientReconnect.PlayerLabel(target) + " view="
					+ LegacyTransientReconnect.IdLabel(viewId) + " snapshot=" + snapshotId
					+ " type=" + error.GetType().Name + " message="
					+ LegacyTransientReconnect.Clean(error.Message));
				return false;
			}
		}

		internal void BroadcastRebind(NetworkViewID oldId, NetworkViewID newId)
		{
			if (sessionView == null) return;
			try
			{
				sessionView.RPC("MPatcherResumeRebindV2", RPCMode.Others, oldId, newId);
				LegacyTransientReconnect.Log("SERVER_REBIND_BROADCAST old="
					+ LegacyTransientReconnect.IdLabel(oldId) + " new="
					+ LegacyTransientReconnect.IdLabel(newId));
			}
			catch (Exception error)
			{
				LegacyTransientReconnect.Log("SERVER_REBIND_BROADCAST_FAILED type=" + error.GetType().Name
					+ " message=" + LegacyTransientReconnect.Clean(error.Message));
			}
		}

		internal bool SendRebind(NetworkPlayer target, NetworkViewID oldId, NetworkViewID newId,
			string reason)
		{
			if (sessionView == null) return false;
			try
			{
				sessionView.RPC("MPatcherResumeRebindV2", target, oldId, newId);
				LegacyTransientReconnect.Log("SERVER_REBIND_TARGETED target="
					+ LegacyTransientReconnect.PlayerLabel(target) + " old="
					+ LegacyTransientReconnect.IdLabel(oldId) + " new="
					+ LegacyTransientReconnect.IdLabel(newId) + " reason="
					+ LegacyTransientReconnect.Clean(reason));
				return true;
			}
			catch (Exception error)
			{
				LegacyTransientReconnect.Log("SERVER_REBIND_TARGETED_FAILED target="
					+ LegacyTransientReconnect.PlayerLabel(target) + " type=" + error.GetType().Name);
				return false;
			}
		}

		internal void BroadcastRetire(NetworkViewID oldId, NetworkViewID newId)
		{
			if (sessionView == null) return;
			try
			{
				sessionView.RPC("MPatcherResumeRetiredV2", RPCMode.Others, oldId, newId);
				LegacyTransientReconnect.Log("SERVER_REBOUND_RETIRE_BROADCAST old="
					+ LegacyTransientReconnect.IdLabel(oldId) + " new="
					+ LegacyTransientReconnect.IdLabel(newId));
			}
			catch (Exception error)
			{
				LegacyTransientReconnect.Log("SERVER_REBOUND_RETIRE_BROADCAST_FAILED type="
					+ error.GetType().Name + " message=" + LegacyTransientReconnect.Clean(error.Message));
			}
		}

		internal void LogServerInventory(string stage, NetworkPlayer player, LegacyTransientReconnect.ResumeRecord record)
		{
			int totalViews = 0;
			int ownedViews = 0;
			try
			{
				UnityEngine.Object[] found = UnityEngine.Object.FindObjectsOfType(typeof(NetworkView));
				totalViews = found.Length;
				for (int i = 0; i < found.Length; i++)
				{
					NetworkView view = found[i] as NetworkView;
					if (view != null && view.owner == player) ownedViews++;
				}
			}
			catch { }
			LegacyTransientReconnect.Log("SERVER_INVENTORY stage=" + stage + " player="
				+ LegacyTransientReconnect.PlayerLabel(player) + " allViews=" + totalViews + " ownedNow="
				+ ownedViews + " retainedViews=" + record.Views.Count + " frozenBodies=" + record.Bodies.Count);
			for (int i = 0; i < record.Views.Count; i++)
			{
				NetworkView view = record.Views[i];
				if (view == null)
				{
					LegacyTransientReconnect.Log("SERVER_VIEW stage=" + stage + " index=" + i + " alive=false");
					continue;
				}
				LegacyTransientReconnect.Log("SERVER_VIEW stage=" + stage + " index=" + i + " alive=true id="
					+ ViewLabel(view) + " owner=" + LegacyTransientReconnect.PlayerLabel(view.owner)
					+ " mine=" + view.isMine + " path=" + TransformPath(view.transform));
			}
		}

		private void LogClientInventory(string stage)
		{
			MachineController machine = game == null ? null : game.FICMBCLEFDL;
			int bodyControllers = machine == null || machine.ILBAAENKMBL == null ? -1 : machine.ILBAAENKMBL.Count;
			int bodyRoots = machine == null || machine.KBLANAFAJFP == null ? -1 : machine.KBLANAFAJFP.Count;
			NetworkView[] views = machine == null ? new NetworkView[0] : machine.GetComponentsInChildren<NetworkView>(true);
			Rigidbody[] bodies = machine == null ? new Rigidbody[0] : machine.GetComponentsInChildren<Rigidbody>(true);
			Vector3 position = MachinePosition(machine);
			float delta = haveLossPosition ? Vector3.Distance(position, lossPosition) : 0f;
			LegacyTransientReconnect.Log("CLIENT_INVENTORY stage=" + stage + " peer=" + Network.peerType
				+ " isClient=" + Network.isClient + " machineAlive=" + (machine != null)
				+ " active=" + (machine != null && machine.gameObject.activeInHierarchy)
				+ " views=" + views.Length + " rigidbodies=" + bodies.Length + " bodyControllers="
				+ bodyControllers + " bodyRoots=" + bodyRoots + " position=" + Vector(position)
				+ " deltaFromLoss=" + delta.ToString("0.000", CultureInfo.InvariantCulture));
			for (int i = 0; i < views.Length; i++)
			{
				NetworkView view = views[i];
				LegacyTransientReconnect.Log("CLIENT_VIEW stage=" + stage + " index=" + i + " id="
					+ ViewLabel(view) + " owner=" + LegacyTransientReconnect.PlayerLabel(view.owner)
					+ " mine=" + view.isMine + " group=" + view.group
					+ " path=" + TransformPath(view.transform));
			}
		}

		private void CaptureLossPosition()
		{
			MachineController machine = game == null ? null : game.FICMBCLEFDL;
			if (machine == null) { haveLossPosition = false; return; }
			lossPosition = MachinePosition(machine);
			haveLossPosition = true;
		}

		private static Vector3 MachinePosition(MachineController machine)
		{
			if (machine == null) return Vector3.zero;
			try
			{
				if (machine.CLBBODLKEJI != null) return machine.CLBBODLKEJI.transform.position;
			}
			catch { }
			return machine.transform.position;
		}

		private void ShowCrashRestoreConsent()
		{
			if (crashConsentUi != null || !crashResumePending) return;
			if (AutoReconnect.AutomaticCrashRestore)
			{
				LegacyTransientReconnect.Log("CLIENT_CRASH_CONSENT_AUTO accepted=true fresh="
					+ LegacyTransientReconnect.IdLabel(crashFreshViewId));
				SubmitCrashRestoreConsent(true);
				return;
			}
			crashConsentUi = LegacyCrashResumeConsentUi.Create(game,
				delegate(bool accept) { SubmitCrashRestoreConsent(accept); });
			LegacyTransientReconnect.Log("CLIENT_CRASH_CONSENT_UI result="
				+ (crashConsentUi == null ? "native-ui-not-ready" : "shown")
				+ " fresh=" + LegacyTransientReconnect.IdLabel(crashFreshViewId));
		}

		internal void SubmitCrashRestoreConsent(bool accept)
		{
			if (!clientRole || !crashResumePending || crashConsentDecisionSent) return;
			crashConsentDecisionSent = true;
			crashConsentAccepted = accept;
			CloseCrashRestoreConsent(accept ? "accepted" : "declined");
			SendCrashRestoreConsentDecision();
		}

		private void SendCrashRestoreConsentDecision()
		{
			if (sessionView == null || !Network.isClient || !crashResumePending) return;
			try
			{
				string freshIdentityReason;
				if (crashConsentAccepted && !TryGetCrashFreshOwnerIdentity(
					out crashFreshOwnerIdentity, out freshIdentityReason))
				{
					LegacyTransientReconnect.Log("CLIENT_CRASH_CONSENT_SEND_WAIT reason="
						+ LegacyTransientReconnect.Clean(freshIdentityReason));
					return;
				}
				sessionView.RPC("MPatcherCrashResumeDecisionV6", RPCMode.Server, 6, token,
					crashFreshViewId, crashConsentAccepted, crashFreshOwnerIdentity ?? "");
				LegacyTransientReconnect.Log("CLIENT_CRASH_CONSENT_SENT accepted="
					+ crashConsentAccepted + " fresh="
					+ LegacyTransientReconnect.IdLabel(crashFreshViewId)
					+ " freshOwnerIdentity=" + (crashConsentAccepted
						? crashFreshOwnerIdentity : "not-required"));
			}
			catch (Exception error)
			{
				LegacyTransientReconnect.Log("CLIENT_CRASH_CONSENT_SEND_FAILED type="
					+ error.GetType().Name + " message="
					+ LegacyTransientReconnect.Clean(error.Message));
			}
		}

		private void CloseCrashRestoreConsent(string reason)
		{
			if (crashConsentUi == null) return;
			crashConsentUi.Close();
			crashConsentUi = null;
			LegacyTransientReconnect.Log("CLIENT_CRASH_CONSENT_UI_CLOSED reason="
				+ LegacyTransientReconnect.Clean(reason));
		}

		private void ShowLocalStatus(string message)
		{
			try
			{
				MethodInfo method = AccessTools.Method(typeof(Game), "RPC_SysMsg", new Type[] { typeof(string) });
				if (method != null && game != null)
				{
					method.Invoke(game, new object[] { message });
					LegacyTransientReconnect.Log("CLIENT_LOCAL_STATUS_SHOWN message="
						+ LegacyTransientReconnect.Clean(message));
				}
			}
			catch (Exception error) { LegacyTransientReconnect.Log("CLIENT_LOCAL_STATUS_FAILED type=" + error.GetType().Name); }
		}

		private void ArmActiveSession(string source)
		{
			if (!clientRole || !LegacyTransientReconnect.ValidToken(token)
				|| !LegacyReconnectTarget.HasTarget) return;
			string reason;
			bool armed = LegacyCrashResumeMarker.Arm(token, LegacyReconnectTarget.Description, out reason);
			if (armed) nextOwnerCheckpoint = Time.realtimeSinceStartup;
			LegacyTransientReconnect.Log("CLIENT_CRASH_MARKER_ARM source="
				+ LegacyTransientReconnect.Clean(source) + " token="
				+ LegacyTransientReconnect.TokenLabel(token) + " target="
				+ LegacyReconnectTarget.Description + " result=" + armed + " reason="
				+ LegacyTransientReconnect.Clean(reason));
		}

		private void ClearActiveSession(string source)
		{
			if (!clientRole) return;
			string reason;
			bool cleared = LegacyCrashResumeMarker.Clear(out reason);
			LegacyTransientReconnect.Log("CLIENT_CRASH_MARKER_CLEAR source="
				+ LegacyTransientReconnect.Clean(source) + " result=" + cleared + " reason="
				+ LegacyTransientReconnect.Clean(reason));
			string checkpointReason;
			bool checkpointCleared = LegacyCrashOwnerCheckpoint.Clear(out checkpointReason);
			ownerCheckpointMachine = null;
			ownerControls = null;
			tracedOwnerControls = null;
			nextOwnerControlRetry = 0f;
			ownerControlSyncPending = false;
			ownerCheckpointFingerprint = null;
			LegacyTransientReconnect.Log("CLIENT_OWNER_CHECKPOINT_CLEAR source="
				+ LegacyTransientReconnect.Clean(source) + " result=" + checkpointCleared
				+ " reason=" + LegacyTransientReconnect.Clean(checkpointReason));
		}

		private void LogCrashMarkerPreserved(string source)
		{
			LegacyTransientReconnect.Log("CLIENT_CRASH_MARKER_PRESERVED source="
				+ LegacyTransientReconnect.Clean(source) + " token="
				+ LegacyTransientReconnect.TokenLabel(token)
				+ " reason=resume-pending");
		}

		private static string GetToken()
		{
			if (LegacyTransientReconnect.ValidToken(cachedToken)) return cachedToken;
			try
			{
				string directory = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), "UserData"), TokenDirectory);
				string path = Path.Combine(directory, TokenFile);
				if (File.Exists(path))
				{
					string existing = File.ReadAllText(path).Trim();
					if (LegacyTransientReconnect.ValidToken(existing))
					{
						cachedToken = existing.ToLowerInvariant();
						return cachedToken;
					}
				}
				Directory.CreateDirectory(directory);
				cachedToken = Guid.NewGuid().ToString("N");
				File.WriteAllText(path, cachedToken);
				LegacyTransientReconnect.Log("TOKEN_CREATED token=" + LegacyTransientReconnect.TokenLabel(cachedToken)
					+ " path=UserData/_mpatcher/" + TokenFile);
				return cachedToken;
			}
			catch (Exception error)
			{
				if (!LegacyTransientReconnect.ValidToken(cachedToken)) cachedToken = Guid.NewGuid().ToString("N");
				LegacyTransientReconnect.Log("TOKEN_PERSIST_FAILED type=" + error.GetType().Name
					+ " token=" + LegacyTransientReconnect.TokenLabel(cachedToken));
				return cachedToken;
			}
		}

		private static string TestDropPath()
		{
			return Path.Combine(Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), "UserData"),
				TokenDirectory), TestDropFile);
		}

		private static string SafeLocalName()
		{
			try { return JKGKJLLFMLE.IGOBPLOLHEP.userName ?? "Player"; }
			catch { return "Player"; }
		}

		private static string ViewLabel(NetworkView view)
		{
			try { return view == null ? "none" : LegacyTransientReconnect.Clean(view.viewID.ToString()); }
			catch { return "unavailable"; }
		}

		private static string TransformPath(Transform value)
		{
			try
			{
				string path = value == null ? "none" : value.name;
				Transform parent = value == null ? null : value.parent;
				int depth = 0;
				while (parent != null && depth++ < 8)
				{
					path = parent.name + "/" + path;
					parent = parent.parent;
				}
				return LegacyTransientReconnect.Clean(path);
			}
			catch { return "unavailable"; }
		}

		private static string Vector(Vector3 value)
		{
			return value.x.ToString("0.000", CultureInfo.InvariantCulture) + ","
				+ value.y.ToString("0.000", CultureInfo.InvariantCulture) + ","
				+ value.z.ToString("0.000", CultureInfo.InvariantCulture);
		}
	}
}
