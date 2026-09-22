using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using McnCraft;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
	// Restores the Legacy/Individual path that the recovered MPatcher explicitly
	// rejects. Legacy replacement has no gameplay cooldown and destroys the
	// outgoing machine before allocating the selected one.
	internal static class LegacyMachineChangeIngame
	{
		private sealed class MachineSelectionSnapshot
		{
			internal string MachineName;
			internal string FolderName;
			internal BuildData Build;
			internal AssignData Assign;
			internal bool MachineDataLoaded;
			internal bool MachineAudioReady;
			internal GameObject ActiveRoot;
			internal MachineController ActiveController;
			internal HashSet<int> ControllerIds;
			internal Meeting Meeting;
			internal GameObject OutgoingRoot;
			internal Vector3 Position;
			internal Quaternion Rotation;
		}

		private const string PatchId = "local.moddev.machinecraft.machinechange-legacy.v12";
		private const int SpawnReadyTimeoutSeconds = 5;
		private static LegacySpawnFinalization spawnFinalization;
		private static readonly FieldInfo SledContactSensorField = AccessTools.Field(typeof(SledController), "IHHNBGLEBMH");
		private const int CleanupFrameLimit = 6;
		private const long HighVirtualMemoryBytes = 3L * 1024L * 1024L * 1024L;

		private static Harmony harmony;
		private static bool registered;
		private static FieldInfo cooldownField;
		private static GameObject legacyNetworkDestroyedRoot;
		private static MachineSelectionSnapshot pendingSelection;
		private static bool replacementInProgress;
		private static bool allowDeferredCommit;
		private static bool previousCooldownValid;
		private static float previousCooldown;
		private static MachineController attackBypassController;
		private static bool suppressRegulationRollback;
		private static int suppressDebugMessageFrame = -1;
		private static bool directSpawnPositionValid;
		private static Vector3 directSpawnPosition;
		private static Quaternion directSpawnRotation;
		private static bool networkSpawnPositionApplied;
		private static bool initializeSpawnPositionApplied;
		private static bool networkAnchorRepaired;
		private static int repairedNetworkAnchorBlocks;
		private static Vector3 repairedNetworkAnchor;

		internal static void TryRegister()
		{
			if (registered)
				return;

			try
			{
				Type patchType = typeof(global::dyl7NQFWvb8SnwY4dXogp_aCQhx2Y7dLGUBgzCus25T9Wpo6h01g9Y342KDBl8ctV_NzWUOypgYMsbS0RAESeaU);
				MethodInfo meetingAction = AccessTools.Method(typeof(Meeting), "BDKIMPEDKCJ", new Type[] { typeof(string), typeof(GameObject) });
				MethodInfo networkDestroy = AccessTools.Method(patchType, "smethod_19", new Type[] { typeof(GameObject) });
				MethodInfo localDestroy = AccessTools.Method(patchType, "smethod_20", new Type[] { typeof(UnityEngine.Object) });
				MethodInfo networkInstantiate = AccessTools.Method(patchType, "smethod_22",
					new Type[] { typeof(UnityEngine.Object), typeof(Vector3), typeof(Quaternion), typeof(int) });
				MethodInfo initializeMachine = AccessTools.Method(patchType, "smethod_26",
					new Type[] { typeof(MachineController), typeof(string), typeof(BuildData), typeof(AssignData), typeof(Vector3) });
				MethodInfo showDebugMessage = AccessTools.Method(typeof(MPatchrMain.MPatchr), "ShowDebugMsg", new Type[] { typeof(string) });
				cooldownField = AccessTools.Field(patchType, "float_0");

				MethodInfo meetingActionPrefix = AccessTools.Method(typeof(LegacyMachineChangeIngame), "MeetingActionPrefix");
				MethodInfo meetingActionPostfix = AccessTools.Method(typeof(LegacyMachineChangeIngame), "MeetingActionPostfix");
				MethodInfo networkDestroyPrefix = AccessTools.Method(typeof(LegacyMachineChangeIngame), "NetworkDestroyPrefix");
				MethodInfo localDestroyPrefix = AccessTools.Method(typeof(LegacyMachineChangeIngame), "LocalDestroyPrefix");
				MethodInfo networkInstantiatePrefix = AccessTools.Method(typeof(LegacyMachineChangeIngame), "NetworkInstantiatePrefix");
				MethodInfo initializeMachinePrefix = AccessTools.Method(typeof(LegacyMachineChangeIngame), "InitializeMachinePrefix");
				MethodInfo initializeMachinePostfix = AccessTools.Method(typeof(LegacyMachineChangeIngame), "InitializeMachinePostfix");
				MethodInfo showDebugMessagePrefix = AccessTools.Method(typeof(LegacyMachineChangeIngame), "ShowDebugMessagePrefix");

				if (meetingAction == null || networkDestroy == null || localDestroy == null || networkInstantiate == null
					|| initializeMachine == null || showDebugMessage == null || cooldownField == null
					|| meetingActionPrefix == null || meetingActionPostfix == null || networkDestroyPrefix == null
					|| localDestroyPrefix == null || networkInstantiatePrefix == null || initializeMachinePrefix == null
					|| initializeMachinePostfix == null
					|| showDebugMessagePrefix == null)
				{
					throw new MissingMethodException("Recovered MachineChangeIngame methods");
				}

				harmony = new Harmony(PatchId);
				PatchPrefix(meetingAction, meetingActionPrefix);
				PatchPostfix(meetingAction, meetingActionPostfix);
				PatchPrefix(networkDestroy, networkDestroyPrefix);
				PatchPrefix(localDestroy, localDestroyPrefix);
				PatchPrefix(networkInstantiate, networkInstantiatePrefix);
				PatchPrefix(initializeMachine, initializeMachinePrefix);
				PatchPostfix(initializeMachine, initializeMachinePostfix);
				harmony.Patch(AccessTools.Method(typeof(MachineController), "Initialize",
					new Type[] { typeof(string), typeof(BuildData), typeof(AssignData), typeof(Vector3) }),
					transpiler: new HarmonyMethod(AccessTools.Method(typeof(LegacyMachineNetworkAnchor), "InitializeTranspiler")));
				PatchPrefix(AccessTools.Method(typeof(MachineSerializer), "SyncStructure", new Type[] { typeof(bool[]) }),
					AccessTools.Method(typeof(LegacyMachineChangeIngame), "BeforeSyncStructure"));
				PatchPrefix(showDebugMessage, showDebugMessagePrefix);

				registered = true;
				Log("REGISTERED entry=Meeting.BDKIMPEDKCJ restrictions=none cooldownSeconds=0"
					+ " replacement=validation-dispatch/deferred-cleanup spawnPosition=target/ready-warp"
					+ " networkAnchor=native-bounds-excluding-missile-template-hierarchy"
					+ " destroy=Unity-Network/rebound-server-retire scriptHelpers=SpringControl");
			}
			catch (Exception error)
			{
				registered = false;
				try
				{
					if (harmony != null)
						harmony.UnpatchAll(PatchId);
				}
				catch (Exception rollbackError)
				{
					Log("REGISTER_ROLLBACK_FAILED type=" + rollbackError.GetType().Name
						+ " message=" + rollbackError.Message);
				}
				harmony = null;
				Log("REGISTER_FAILED type=" + error.GetType().Name + " message=" + error.Message);
			}
		}

		private static bool MeetingActionPrefix(string DPGKEOAGONA, GameObject NGLBLAGMBLN, Meeting __instance)
		{
			RestoreAttackBypass();
			if (HNJDDKJLHMM.FHLGOMHPDLN != HNJDDKJLHMM.HKGAACMIPIH.Legacy
				|| JKGKJLLFMLE.EGFHGHKLNAO != JKGKJLLFMLE.LENPCAMMAEP.Meeting)
			{
				return true;
			}

			string pending = global::dyl7NQFWvb8SnwY4dXogp_aCQhx2Y7dLGUBgzCus25T9Wpo6h01g9Y342KDBl8ctV_NzWUOypgYMsbS0RAESeaU.Class50.WCKsvBPB6cSYds0fexVu_00247Y;
			string selectedText = string.Empty;
			string selectedColor = string.Empty;
			UnityEngine.UI.Text selectedLabel = null;
			try
			{
				if (NGLBLAGMBLN && NGLBLAGMBLN.transform.childCount > 0)
				{
					selectedLabel = NGLBLAGMBLN.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>();
					if (selectedLabel)
					{
						selectedText = selectedLabel.text;
						selectedColor = selectedLabel.color.ToString();
					}
				}
			}
			catch (Exception error)
			{
				selectedText = "<read-failed:" + error.GetType().Name + ">";
			}

			Log("EVENT action=" + Quote(DPGKEOAGONA)
				+ " pending=" + Quote(pending)
				+ " object=" + Quote(NGLBLAGMBLN ? NGLBLAGMBLN.name : null)
				+ " text=" + Quote(selectedText)
				+ " color=" + Quote(selectedColor)
				+ " machine=" + Quote(JKGKJLLFMLE.IGOBPLOLHEP.machineName));

			if (replacementInProgress
				&& string.Equals(DPGKEOAGONA, "File", StringComparison.Ordinal)
				&& string.Equals(pending, "SelectMachine", StringComparison.Ordinal))
			{
				Log("EVENT ignored reason=replacement-in-progress cooldownSeconds=0");
				return false;
			}

			if (string.Equals(DPGKEOAGONA, "File", StringComparison.Ordinal)
				&& string.Equals(pending, "SelectMachine", StringComparison.Ordinal)
				&& selectedLabel && selectedLabel.color != Color.yellow)
			{
				ApplyZeroCooldown();
			}

			if (string.Equals(DPGKEOAGONA, "File", StringComparison.Ordinal)
				&& string.Equals(pending, "SelectMachine", StringComparison.Ordinal)
				&& __instance && __instance.FICMBCLEFDL && __instance.FICMBCLEFDL.KDODJPCDEHO)
			{
				attackBypassController = __instance.FICMBCLEFDL;
				attackBypassController.KDODJPCDEHO = false;
				Log("LIMIT bypassed=attack mode=Legacy restrictions=none cooldownSeconds=0");
			}

			return true;
		}

		private static void MeetingActionPostfix()
		{
			RestoreAttackBypass();
		}

		private static void RestoreAttackBypass()
		{
			MachineController controller = attackBypassController;
			attackBypassController = null;
			if (controller)
				controller.KDODJPCDEHO = true;
		}

		private static void ApplyZeroCooldown()
		{
			if (cooldownField == null)
				return;

			try
			{
				float previous = (float)cooldownField.GetValue(null);
				if (previous <= -2000000000f)
					return;

				cooldownField.SetValue(null, -2147483648f);
				Log("LIMIT bypassed=cooldown previous=" + previous.ToString("0.###") + " cooldownSeconds=0");
			}
			catch (Exception error)
			{
				Log("COOLDOWN bypass-failed type=" + error.GetType().Name + " message=" + Quote(error.Message));
			}
		}

		private static string Quote(string value)
		{
			if (value == null)
				return "<null>";
			return "\"" + value.Replace("\\", "\\\\").Replace("\r", "\\r").Replace("\n", "\\n").Replace("\"", "\\\"") + "\"";
		}

		private static void PatchPrefix(MethodInfo original, MethodInfo prefixMethod)
		{
			HarmonyMethod prefix = new HarmonyMethod(prefixMethod);
			prefix.priority = Priority.First;
			harmony.Patch(original, prefix, null, null, null);
		}

		private static void PatchPostfix(MethodInfo original, MethodInfo postfixMethod)
		{
			HarmonyMethod postfix = new HarmonyMethod(postfixMethod);
			postfix.priority = Priority.Last;
			harmony.Patch(original, null, postfix, null, null);
		}

		internal static bool ValidateCurrentSelection()
		{
			BuildData build = JKGKJLLFMLE.HHGILAIOCLG;
			if (!registered || HNJDDKJLHMM.FHLGOMHPDLN != HNJDDKJLHMM.HKGAACMIPIH.Legacy)
				return true;

			MachineSelectionSnapshot snapshot = pendingSelection;
			if (snapshot == null || !snapshot.Meeting || !snapshot.OutgoingRoot)
			{
				Log("VALIDATE result=REJECT reason=invalid-deferred-context transaction=" + (snapshot != null)
					+ " meeting=" + (snapshot != null && snapshot.Meeting)
					+ " outgoingRoot=" + (snapshot != null && snapshot.OutgoingRoot));
				RollbackSelectionTransaction("invalid-deferred-context");
				suppressRegulationRollback = true;
				suppressDebugMessageFrame = Time.frameCount;
				return false;
			}

			bool handled = BeginDeferredReplacement(snapshot.Meeting, snapshot.OutgoingRoot,
				snapshot.Position, snapshot.Rotation);
			if (!handled)
				return true;

			suppressRegulationRollback = true;
			suppressDebugMessageFrame = Time.frameCount;
			Log("VALIDATE result=DEFERRED mode=unrestricted cooldownSeconds=0 machine="
				+ Quote(JKGKJLLFMLE.IGOBPLOLHEP.machineName)
				+ " blocks=" + (build == null || build.blockData == null ? -1 : build.blockData.Count)
				+ " target=" + FormatPosition(snapshot.Position));
			return false;
		}

		internal static void BeginSelectionTransaction(Meeting meeting)
		{
			HashSet<int> controllerIds = new HashSet<int>();
			MachineController[] controllers = UnityEngine.Object.FindObjectsOfType<MachineController>();
			foreach (MachineController controller in controllers)
			{
				if (controller)
					controllerIds.Add(controller.GetInstanceID());
			}

			GameObject activeRoot = meeting ? meeting.JPIAFJHAPHM : null;
			Transform activeTransform = activeRoot ? activeRoot.transform : null;
			Transform activeParent = activeTransform ? activeTransform.parent : null;

			pendingSelection = new MachineSelectionSnapshot
			{
				MachineName = JKGKJLLFMLE.IGOBPLOLHEP.machineName,
				FolderName = JKGKJLLFMLE.IGOBPLOLHEP.folderName,
				Build = JKGKJLLFMLE.HHGILAIOCLG,
				Assign = JKGKJLLFMLE.MIIGKEBFKKD,
				MachineDataLoaded = JKGKJLLFMLE.MLBCKBAPAMJ,
				MachineAudioReady = JKGKJLLFMLE.KAOJMNJNLLM,
				ActiveRoot = activeRoot,
				ActiveController = meeting ? meeting.FICMBCLEFDL : null,
				ControllerIds = controllerIds,
				Meeting = meeting,
				OutgoingRoot = activeParent ? activeParent.gameObject : activeRoot,
				Position = activeTransform ? activeTransform.position : Vector3.zero,
				Rotation = activeTransform ? activeTransform.rotation : Quaternion.identity
			};

			Log("TRANSACTION begin machine=" + Quote(pendingSelection.MachineName)
				+ " folder=" + Quote(pendingSelection.FolderName)
				+ " activeRoot=" + Quote(pendingSelection.ActiveRoot ? pendingSelection.ActiveRoot.name : null)
				+ " outgoingRoot=" + Quote(pendingSelection.OutgoingRoot ? pendingSelection.OutgoingRoot.name : null)
				+ " activePosition=" + FormatPosition(pendingSelection.ActiveController)
				+ " controllers=" + controllerIds.Count);
		}

		internal static void CommitSelectionTransaction()
		{
			if (replacementInProgress && !allowDeferredCommit)
			{
				Log("TRANSACTION commit-deferred reason=replacement-in-progress");
				return;
			}

			MachineSelectionSnapshot snapshot = pendingSelection;
			pendingSelection = null;
			Log("TRANSACTION commit previous=" + Quote(snapshot == null ? null : snapshot.MachineName)
				+ " current=" + Quote(JKGKJLLFMLE.IGOBPLOLHEP.machineName));
		}

		internal static bool BeginDeferredReplacement(Meeting meeting, GameObject oldRoot, Vector3 position, Quaternion rotation)
		{
			if (LegacyTransientReconnect.MachineChangeMustWait())
			{
				Log("REPLACE_START rejected reason=recovery-in-progress");
				RollbackSelectionTransaction("recovery-in-progress");
				return true;
			}
			if (replacementInProgress)
			{
				Log("REPLACE_START rejected reason=already-in-progress");
				RollbackSelectionTransaction("replacement-already-in-progress");
				return true;
			}

			if (!meeting || !oldRoot || pendingSelection == null)
			{
				Log("REPLACE_START rejected reason=invalid-context meeting=" + (bool)meeting
					+ " oldRoot=" + (bool)oldRoot
					+ " transaction=" + (pendingSelection != null));
				RollbackSelectionTransaction("invalid-replacement-context");
				return true;
			}

			replacementInProgress = true;
			CapturePreviousCooldown();
			Log("REPLACE_START mode=Legacy machine=" + Quote(JKGKJLLFMLE.IGOBPLOLHEP.machineName)
				+ " oldRoot=" + Quote(oldRoot.name)
				+ " target=" + FormatPosition(position));
			LogMemory("before-destroy");

			try
			{
				meeting.StartCoroutine(DeferredLegacyReplacement(meeting, oldRoot, position, rotation));
				return true;
			}
			catch (Exception error)
			{
				replacementInProgress = false;
				RollbackSelectionTransaction("coroutine-start-failed-" + error.GetType().Name);
				RestorePreviousCooldown();
				Log("REPLACE_START_FAILED type=" + error.GetType().Name
					+ " message=" + Quote(error.Message)
					+ " stack=" + Quote(error.StackTrace));
				return true;
			}
		}

		private static IEnumerator DeferredLegacyReplacement(Meeting meeting, GameObject oldRoot,
			Vector3 position, Quaternion rotation)
		{
			string requestedMachine = JKGKJLLFMLE.IGOBPLOLHEP.machineName;
			try
			{
				int scriptHelpers = CleanupSpringControlHelpers(oldRoot, false);
				Exception destroyError = DestroyOutgoingMachine(oldRoot);

				int waitedFrames = 0;
				do
				{
					yield return new WaitForEndOfFrame();
					waitedFrames++;
				}
				while (oldRoot && waitedFrames < CleanupFrameLimit);
				if (oldRoot)
				{
					RollbackSelectionTransaction("destroy-timeout");
					RestorePreviousCooldown();
					Log("REPLACE_FAILED phase=destroy machine=" + Quote(requestedMachine)
						+ " reason=old-root-still-alive waitedFrames=" + waitedFrames
						+ " type=" + (destroyError == null ? "<none>" : destroyError.GetType().Name)
						+ " message=" + Quote(destroyError == null ? null : destroyError.Message));
					yield break;
				}
				if (destroyError != null)
				{
					Log("DESTROY_WARNING type=" + destroyError.GetType().Name
						+ " message=" + Quote(destroyError.Message));
				}

				int staleHelpers = CleanupSpringControlHelpers(null, true);
				ForceManagedCollection();
				long virtualMemory = GetVirtualMemoryBytes();
				bool unloadedAssets = virtualMemory >= HighVirtualMemoryBytes;
				if (unloadedAssets)
				{
					AsyncOperation unloadOperation = null;
					try
					{
						unloadOperation = Resources.UnloadUnusedAssets();
					}
					catch (Exception error)
					{
						Log("MEMORY unload-failed type=" + error.GetType().Name
							+ " message=" + Quote(error.Message));
					}

					if (unloadOperation != null)
						yield return unloadOperation;
					ForceManagedCollection();
				}

				Log("CLEANUP complete oldRootDestroyed=" + (!oldRoot)
					+ " waitedFrames=" + waitedFrames
					+ " associatedSpringControls=" + scriptHelpers
					+ " staleSpringControls=" + staleHelpers
					+ " unloadUnusedAssets=" + unloadedAssets);
				LogMemory("before-spawn");

				Exception spawnError;
				bool spawned = TryCompleteLegacyReplacement(meeting, position, rotation, out spawnError);
				LegacySpawnFinalization finalization = spawnFinalization;
				if (spawned)
				{
					yield return AwaitSpawnFinalization(finalization);
					spawned = finalization != null && finalization.Succeeded;
					if (!spawned) spawnError = finalization == null
						? new InvalidOperationException("Spawn did not schedule finalization") : finalization.Error;
				}
				if (spawned)
				{
					try
					{
						CompleteDeferredSelection();
					}
					catch (Exception error)
					{
						Log("REPLACE_COMMIT_WARNING type=" + error.GetType().Name
							+ " message=" + Quote(error.Message));
					}

					Log("REPLACE_COMPLETE result=success machine=" + Quote(requestedMachine)
						+ " cooldownSeconds=0");
					previousCooldownValid = false;
					LogMemory("after-spawn");
					yield break;
				}

				GameObject failedRoot = meeting ? meeting.JPIAFJHAPHM : null;
				DestroyFailedReplacement(meeting, failedRoot, oldRoot);
				RollbackSelectionTransaction("spawn-failed-" + (spawnError == null ? "unknown" : spawnError.GetType().Name));
				RestorePreviousCooldown();
				Log("REPLACE_FAILED phase=spawn machine=" + Quote(requestedMachine)
					+ " type=" + (spawnError == null ? "Unknown" : spawnError.GetType().Name)
					+ " message=" + Quote(spawnError == null ? null : spawnError.Message));

				yield return new WaitForEndOfFrame();
				CleanupSpringControlHelpers(null, true);
				ForceManagedCollection();

				Exception restoreError;
				bool restored = TryCompleteLegacyReplacement(meeting, position, rotation, out restoreError);
				LegacySpawnFinalization rollbackFinalization = spawnFinalization;
				if (restored)
				{
					yield return AwaitSpawnFinalization(rollbackFinalization);
					restored = rollbackFinalization != null && rollbackFinalization.Succeeded;
					if (!restored) restoreError = rollbackFinalization == null
						? new InvalidOperationException("Rollback did not schedule finalization") : rollbackFinalization.Error;
				}
				Log("REPLACE_ROLLBACK result=" + (restored ? "restored" : "failed")
					+ " machine=" + Quote(JKGKJLLFMLE.IGOBPLOLHEP.machineName)
					+ " type=" + (restoreError == null ? "<none>" : restoreError.GetType().Name)
					+ " message=" + Quote(restoreError == null ? null : restoreError.Message));
				LogMemory("after-rollback");
			}
			finally
			{
				if (pendingSelection != null)
				{
					try
					{
						RollbackSelectionTransaction("replacement-coroutine-ended");
					}
					catch (Exception error)
					{
						Log("TRANSACTION emergency-rollback-failed type=" + error.GetType().Name
							+ " message=" + Quote(error.Message));
					}
					RestorePreviousCooldown();
				}
				replacementInProgress = false;
			}
		}

		private static Exception DestroyOutgoingMachine(GameObject oldRoot)
		{
			Exception firstError = null;
			try
			{
				global::dyl7NQFWvb8SnwY4dXogp_aCQhx2Y7dLGUBgzCus25T9Wpo6h01g9Y342KDBl8ctV_NzWUOypgYMsbS0RAESeaU.smethod_19(oldRoot);
			}
			catch (Exception error)
			{
				firstError = error;
			}

			try
			{
				global::dyl7NQFWvb8SnwY4dXogp_aCQhx2Y7dLGUBgzCus25T9Wpo6h01g9Y342KDBl8ctV_NzWUOypgYMsbS0RAESeaU.smethod_20(oldRoot);
			}
			catch (Exception error)
			{
				if (firstError == null)
					firstError = error;
			}

			try
			{
				global::dyl7NQFWvb8SnwY4dXogp_aCQhx2Y7dLGUBgzCus25T9Wpo6h01g9Y342KDBl8ctV_NzWUOypgYMsbS0RAESeaU.smethod_21();
			}
			catch (Exception error)
			{
				if (firstError == null)
					firstError = error;
			}

			return firstError;
		}

		private static bool TryCompleteLegacyReplacement(Meeting meeting, Vector3 position,
			Quaternion rotation, out Exception error)
		{
			error = null;
			spawnFinalization = null;
			GameObject anchorRoot = null;
			try
			{
				anchorRoot = new GameObject("MPatcherMachineChangeAnchorRoot");
				GameObject anchorMachine = new GameObject("MPatcherMachineChangeAnchor");
				anchorMachine.transform.parent = anchorRoot.transform;
				anchorMachine.transform.position = position;
				anchorMachine.transform.rotation = rotation;
				meeting.JPIAFJHAPHM = anchorMachine;

				directSpawnPosition = position;
				directSpawnRotation = rotation;
				networkSpawnPositionApplied = false;
				initializeSpawnPositionApplied = false;
				networkAnchorRepaired = false;
				repairedNetworkAnchorBlocks = 0;
				repairedNetworkAnchor = Vector3.zero;
				directSpawnPositionValid = true;
				global::dyl7NQFWvb8SnwY4dXogp_aCQhx2Y7dLGUBgzCus25T9Wpo6h01g9Y342KDBl8ctV_NzWUOypgYMsbS0RAESeaU.mDZmboOC2mLx2MmBzu_00244bNw(meeting);
				if (!meeting.JPIAFJHAPHM || !meeting.FICMBCLEFDL)
				{
					error = new InvalidOperationException("Legacy replacement did not create a MachineController.");
					return false;
				}
				return true;
			}
			catch (Exception caught)
			{
				error = caught;
				return false;
			}
			finally
			{
				directSpawnPositionValid = false;
				LegacyMachineNetworkAnchor.Clear();
				if (error != null && spawnFinalization != null) spawnFinalization.Fail(error);
				if (anchorRoot)
				{
					anchorRoot.SetActive(false);
					UnityEngine.Object.Destroy(anchorRoot);
				}
			}
		}

		private static void CompleteDeferredSelection()
		{
			allowDeferredCommit = true;
			try
			{
				CommitSelectionTransaction();
				global::zAOrzM_2ysNo3jthAClNSQ3POH9nkmbIAQjQeFsGY2hYwlsUfYcNDVio3ZwNGLR_00245A.smethod_0();
			}
			finally
			{
				allowDeferredCommit = false;
			}
		}

		private static void CapturePreviousCooldown()
		{
			previousCooldownValid = false;
			try
			{
				previousCooldown = (float)cooldownField.GetValue(null);
				previousCooldownValid = true;
				Log("COOLDOWN captured previous=" + previousCooldown.ToString("0.###"));
			}
			catch (Exception error)
			{
				Log("COOLDOWN capture-failed type=" + error.GetType().Name
					+ " message=" + Quote(error.Message));
			}
		}

		private static void RestorePreviousCooldown()
		{
			if (!previousCooldownValid)
				return;

			try
			{
				cooldownField.SetValue(null, previousCooldown);
				Log("COOLDOWN restored value=" + previousCooldown.ToString("0.###"));
			}
			catch (Exception error)
			{
				Log("COOLDOWN restore-failed type=" + error.GetType().Name
					+ " message=" + Quote(error.Message));
			}
			finally
			{
				previousCooldownValid = false;
			}
		}

		private static void DestroyFailedReplacement(Meeting meeting, GameObject failedRoot, GameObject oldRoot)
		{
			if (failedRoot && !object.ReferenceEquals(failedRoot, oldRoot))
			{
				NetworkView networkView = failedRoot.GetComponent<NetworkView>();
				if (!networkView)
					networkView = failedRoot.GetComponentInChildren<NetworkView>(true);
				DestroyRejectedController(failedRoot, networkView);
			}

			if (meeting)
			{
				meeting.JPIAFJHAPHM = null;
				meeting.FICMBCLEFDL = null;
			}
		}

		private static int CleanupSpringControlHelpers(GameObject outgoingRoot, bool staleOnly)
		{
			int inspected = 0;
			int removed = 0;
			int failures = 0;
			HashSet<int> removedObjects = new HashSet<int>();
			MonoBehaviour[] behaviours = UnityEngine.Object.FindObjectsOfType<MonoBehaviour>();
			foreach (MonoBehaviour behaviour in behaviours)
			{
				if (!behaviour)
					continue;

				Type type = behaviour.GetType();
				if (!string.Equals(type.FullName, "LibSawUtil_reg.SpringControl", StringComparison.Ordinal))
					continue;

				inspected++;
				try
				{
					FieldInfo jointField = type.GetField("joint", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
					FieldInfo enabledField = type.GetField("isEnabled", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
					HingeJoint joint = jointField == null ? null : jointField.GetValue(behaviour) as HingeJoint;
					bool controlsDestroyedJoint = !joint
						&& enabledField != null
						&& enabledField.FieldType == typeof(bool)
						&& (bool)enabledField.GetValue(behaviour);
					bool belongsToOutgoing = !staleOnly && joint && IsUnderRoot(joint.transform, outgoingRoot);
					if (!belongsToOutgoing && !controlsDestroyedJoint)
						continue;

					if (enabledField != null && enabledField.FieldType == typeof(bool))
						enabledField.SetValue(behaviour, false);
					behaviour.enabled = false;

					GameObject helper = behaviour.gameObject;
					if (!helper || !removedObjects.Add(helper.GetInstanceID()))
						continue;

					helper.SetActive(false);
					UnityEngine.Object.Destroy(helper);
					removed++;
					Log("SCRIPT_HELPER removed type=" + Quote(type.FullName)
						+ " reason=" + (belongsToOutgoing ? "outgoing-joint" : "destroyed-joint")
						+ " object=" + Quote(helper.name));
				}
				catch (Exception error)
				{
					failures++;
					Log("SCRIPT_HELPER cleanup-failed type=" + Quote(type.FullName)
						+ " error=" + error.GetType().Name
						+ " message=" + Quote(error.Message));
				}
			}

			if (inspected > 0 || removed > 0 || failures > 0)
				Log("SCRIPT_HELPER summary inspected=" + inspected + " removed=" + removed + " failures=" + failures);
			return removed;
		}

		private static bool IsUnderRoot(Transform transform, GameObject root)
		{
			if (!transform || !root)
				return false;

			Transform rootTransform = root.transform;
			Transform current = transform;
			while (current)
			{
				if (current == rootTransform)
					return true;
				current = current.parent;
			}
			return false;
		}

		private static void ForceManagedCollection()
		{
			try
			{
				GC.Collect();
			}
			catch (Exception error)
			{
				Log("MEMORY collect-failed type=" + error.GetType().Name
					+ " message=" + Quote(error.Message));
			}
		}

		private static long GetVirtualMemoryBytes()
		{
			System.Diagnostics.Process process = null;
			try
			{
				process = System.Diagnostics.Process.GetCurrentProcess();
				return process.VirtualMemorySize64;
			}
			catch (Exception)
			{
				return 0L;
			}
			finally
			{
				if (process != null)
					process.Close();
			}
		}

		private static void LogMemory(string phase)
		{
			System.Diagnostics.Process process = null;
			try
			{
				process = System.Diagnostics.Process.GetCurrentProcess();
				Log("MEMORY phase=" + phase
					+ " workingMiB=" + ToMiB(process.WorkingSet64)
					+ " privateMiB=" + ToMiB(process.PrivateMemorySize64)
					+ " virtualMiB=" + ToMiB(process.VirtualMemorySize64)
					+ " managedMiB=" + ToMiB(GC.GetTotalMemory(false)));
			}
			catch (Exception error)
			{
				Log("MEMORY phase=" + phase + " unavailable=" + error.GetType().Name);
			}
			finally
			{
				if (process != null)
					process.Close();
			}
		}

		private static string ToMiB(long bytes)
		{
			return (bytes / (1024d * 1024d)).ToString("0.0");
		}

		internal static void RollbackSelectionTransaction(string reason)
		{
			if (suppressRegulationRollback && string.Equals(reason, "regulation-denied", StringComparison.Ordinal))
			{
				suppressRegulationRollback = false;
				Log("TRANSACTION rollback-suppressed reason=deferred-dispatch");
				return;
			}
			suppressRegulationRollback = false;

			MachineSelectionSnapshot snapshot = pendingSelection;
			pendingSelection = null;
			if (snapshot == null)
			{
				Log("TRANSACTION rollback-skipped reason=" + Quote(reason) + " snapshot=missing");
				return;
			}

			string rejectedMachine = JKGKJLLFMLE.IGOBPLOLHEP.machineName;
			JKGKJLLFMLE.IGOBPLOLHEP.machineName = snapshot.MachineName;
			JKGKJLLFMLE.IGOBPLOLHEP.folderName = snapshot.FolderName;
			JKGKJLLFMLE.HHGILAIOCLG = snapshot.Build;
			JKGKJLLFMLE.MIIGKEBFKKD = snapshot.Assign;
			JKGKJLLFMLE.MLBCKBAPAMJ = snapshot.MachineDataLoaded;
			JKGKJLLFMLE.KAOJMNJNLLM = snapshot.MachineAudioReady;
			int removedControllers = CleanupRejectedControllers(snapshot, rejectedMachine);

			Log("TRANSACTION rollback reason=" + Quote(reason)
				+ " rejected=" + Quote(rejectedMachine)
				+ " restored=" + Quote(snapshot.MachineName)
				+ " buildRestored=" + object.ReferenceEquals(JKGKJLLFMLE.HHGILAIOCLG, snapshot.Build)
				+ " assignRestored=" + object.ReferenceEquals(JKGKJLLFMLE.MIIGKEBFKKD, snapshot.Assign)
				+ " rejectedControllersRemoved=" + removedControllers);
		}

		internal static IEnumerator FinalizeAcceptedReplacement(MachineController replacement, Vector3 position, Quaternion rotation)
		{
			bool networkPositionApplied = networkSpawnPositionApplied;
			bool initializePositionApplied = initializeSpawnPositionApplied;
			bool anchorRepaired = networkAnchorRepaired;
			int anchorBlocks = repairedNetworkAnchorBlocks;
			Vector3 anchor = repairedNetworkAnchor;
			networkSpawnPositionApplied = false;
			initializeSpawnPositionApplied = false;
			networkAnchorRepaired = false;
			repairedNetworkAnchorBlocks = 0;
			repairedNetworkAnchor = Vector3.zero;
			spawnFinalization = new LegacySpawnFinalization();
			return FinalizeAcceptedReplacementCore(replacement, position, rotation, spawnFinalization,
				networkPositionApplied, initializePositionApplied, anchorRepaired, anchorBlocks, anchor);
		}

		private static IEnumerator AwaitSpawnFinalization(LegacySpawnFinalization result)
		{
			if (result == null) yield break;
			float deadline = Time.realtimeSinceStartup + SpawnReadyTimeoutSeconds + 2f;
			while (!result.Completed && Time.realtimeSinceStartup < deadline) yield return null;
			if (!result.Completed) result.Fail(new TimeoutException("Spawn finalizer did not complete"));
		}

		private static IEnumerator FinalizeAcceptedReplacementCore(MachineController replacement,
			Vector3 position, Quaternion rotation, LegacySpawnFinalization result,
			bool networkPositionApplied, bool initializePositionApplied,
			bool anchorRepaired, int anchorBlocks, Vector3 anchor)
		{
			string rootName = replacement ? replacement.gameObject.name : null;
			bool direct = networkPositionApplied && initializePositionApplied && anchorRepaired;
			// Ordinary native construction at y=4000 already has its native anchor.
			bool anchorReady = anchorRepaired || !initializePositionApplied;
			float started = Time.realtimeSinceStartup;
			int missingSensors = -1;
			IEnumerator operation = result.Run(
				delegate { return replacement; },
				delegate { return AreSledSensorsReady(replacement, out missingSensors)
					&& anchorReady; },
				delegate { replacement.Warp(position, rotation, true); },
				delegate { return Time.realtimeSinceStartup; }, SpawnReadyTimeoutSeconds);
			try { while (operation.MoveNext()) yield return operation.Current; }
			finally { ((IDisposable)operation).Dispose(); }
			if (result.Succeeded)
				Log("SPAWN_FINALIZE result=" + (direct ? "DIRECT_WARP" : "FALLBACK_WARP")
					+ " root=" + Quote(rootName) + " position=" + FormatPosition(replacement)
					+ " missingSledSensors=" + missingSensors + " anchorBlocks=" + anchorBlocks
					+ " waitedSeconds=" + (Time.realtimeSinceStartup - started));
			else
				Log("SPAWN_FINALIZE_FAILED phase=" + (direct ? "direct-warp" : "fallback-warp")
					+ " root=" + Quote(rootName) + " missingSledSensors=" + missingSensors
					+ " error=" + Quote(result.Error == null ? "unknown" : result.Error.ToString()));
		}

		private static bool AreSledSensorsReady(MachineController controller, out int missingSensors)
		{
			missingSensors = 0;
			if (!controller || controller.KBLANAFAJFP == null)
			{
				missingSensors = -1;
				return false;
			}

			foreach (GameObject body in controller.KBLANAFAJFP)
			{
				if (!body || !body.GetComponent<BodyController>())
				{
					missingSensors = -1;
					return false;
				}

				SledController[] sleds = body.GetComponentsInChildren<SledController>();
				foreach (SledController sled in sleds)
				{
					if (!sled || !(SledContactSensorField.GetValue(sled) as ContactSensor))
						missingSensors++;
				}
			}

			return missingSensors == 0;
		}

		private static int CleanupRejectedControllers(MachineSelectionSnapshot snapshot, string rejectedMachine)
		{
			int removed = 0;
			MachineController[] controllers = UnityEngine.Object.FindObjectsOfType<MachineController>();
			foreach (MachineController controller in controllers)
			{
				if (!controller || controller == snapshot.ActiveController
					|| snapshot.ControllerIds.Contains(controller.GetInstanceID()))
				{
					continue;
				}

				GameObject root = controller.gameObject;
				Vector3 position = controller.transform.position;
				NetworkView networkView = root.GetComponent<NetworkView>();
				if (!networkView)
					networkView = root.GetComponentInChildren<NetworkView>(true);

				bool owned = !networkView || networkView.isMine;
				bool nameMatches = !string.IsNullOrEmpty(rejectedMachine)
					&& string.Equals(root.name, rejectedMachine, StringComparison.OrdinalIgnoreCase);
				bool atStagingPosition = Mathf.Abs(position.x) < 32f
					&& position.y > 3500f && position.y < 4500f
					&& Mathf.Abs(position.z) < 32f;

				Log("ROLLBACK_CONTROLLER root=" + Quote(root.name)
					+ " position=" + FormatPosition(position)
					+ " owned=" + owned
					+ " nameMatches=" + nameMatches
					+ " staging=" + atStagingPosition);

				if (!owned || (!nameMatches && !atStagingPosition))
					continue;

				DestroyRejectedController(root, networkView);
				removed++;
			}

			return removed;
		}

		private static void DestroyRejectedController(GameObject root, NetworkView networkView)
		{
			if (networkView)
			{
				try { Network.RemoveRPCs(networkView.viewID); }
				catch (Exception error) { Log("ROLLBACK_REMOVE_RPCS_FAILED type=" + error.GetType().Name + " message=" + error.Message); }

				try { Network.Destroy(networkView.gameObject); }
				catch (Exception error) { Log("ROLLBACK_NETWORK_DESTROY_FAILED type=" + error.GetType().Name + " message=" + error.Message); }
			}

			if (root && (!networkView || networkView.gameObject != root))
				UnityEngine.Object.Destroy(root);

			Log("ROLLBACK_CONTROLLER_REMOVED root=" + Quote(root ? root.name : null));
		}

		private static string FormatPosition(MachineController controller)
		{
			return controller ? FormatPosition(controller.transform.position) : "<null>";
		}

		private static string FormatPosition(Vector3 position)
		{
			return "(" + position.x.ToString("0.###") + "," + position.y.ToString("0.###") + "," + position.z.ToString("0.###") + ")";
		}

		private static bool NetworkDestroyPrefix(GameObject gameObject_0)
		{
			if (HNJDDKJLHMM.FHLGOMHPDLN != HNJDDKJLHMM.HKGAACMIPIH.Legacy)
				return true;

			legacyNetworkDestroyedRoot = null;
			if (!gameObject_0)
			{
				Log("DESTROY fallback=local reason=root-missing");
				return false;
			}

			NetworkView networkView = gameObject_0.GetComponent<NetworkView>();
			if (!networkView)
				networkView = gameObject_0.GetComponentInChildren<NetworkView>(true);
			if (!networkView)
			{
				Log("DESTROY fallback=local reason=network-view-missing root=" + gameObject_0.name);
				return false;
			}
			string rootName = gameObject_0.name;
			GameObject networkObject = networkView.gameObject;
			string networkObjectName = networkObject.name;
			NetworkViewID viewId = networkView.viewID;
			bool isMine = networkView.isMine;
			if (LegacyTransientReconnect.TryRetireReboundView(networkView))
			{
				Log("DESTROY transport=Legacy mode=rebound-server-retire root=" + rootName
					+ " networkObject=" + networkObjectName + " view=" + viewId + " isMine=" + isMine);
				return false;
			}

			try
			{
				Network.RemoveRPCs(viewId);
			}
			catch (Exception error)
			{
				Log("REMOVE_RPCS_FAILED type=" + error.GetType().Name + " message=" + error.Message);
			}

			try
			{
				Network.Destroy(networkObject);
				legacyNetworkDestroyedRoot = networkObject;
				Log("DESTROY transport=Legacy root=" + rootName
					+ " networkObject=" + networkObjectName
					+ " view=" + viewId
					+ " isMine=" + isMine);
			}
			catch (Exception error)
			{
				Log("DESTROY_FAILED fallback=local type=" + error.GetType().Name + " message=" + error.Message);
			}

			return false;
		}

		private static void NetworkInstantiatePrefix(ref Vector3 vector3_0, ref Quaternion quaternion_0)
		{
			if (HNJDDKJLHMM.FHLGOMHPDLN != HNJDDKJLHMM.HKGAACMIPIH.Legacy || !directSpawnPositionValid)
				return;

			Vector3 requestedPosition = vector3_0;
			vector3_0 = directSpawnPosition;
			quaternion_0 = directSpawnRotation;
			networkSpawnPositionApplied = true;
			Log("NETWORK_SPAWN_POSITION requested=" + FormatPosition(requestedPosition)
				+ " target=" + FormatPosition(vector3_0));
		}

		private static void InitializeMachinePrefix(MachineController machineController_0, ref Vector3 vector3_0)
		{
			if (HNJDDKJLHMM.FHLGOMHPDLN != HNJDDKJLHMM.HKGAACMIPIH.Legacy || !directSpawnPositionValid)
				return;

			Vector3 requestedPosition = vector3_0;
			vector3_0 = directSpawnPosition;
			initializeSpawnPositionApplied = true;
			LegacyMachineNetworkAnchor.Begin(machineController_0);
			Log("INITIALIZE_POSITION root=" + Quote(machineController_0 ? machineController_0.gameObject.name : null)
				+ " requested=" + FormatPosition(requestedPosition)
				+ " target=" + FormatPosition(vector3_0));
		}

		private static void InitializeMachinePostfix(MachineController machineController_0)
		{
			if (HNJDDKJLHMM.FHLGOMHPDLN != HNJDDKJLHMM.HKGAACMIPIH.Legacy || !directSpawnPositionValid)
				return;

			// Initialize merges meshes and destroys BlockControllers after SyncStructure.
			// Preserve the complete pre-cleanup anchor; a post-cleanup scan may be empty.
			if (networkAnchorRepaired) return;
			throw new InvalidOperationException("Network anchor was not captured before native structure serialization.");
		}

		private static void BeforeSyncStructure(MachineSerializer __instance)
		{
			if (HNJDDKJLHMM.FHLGOMHPDLN != HNJDDKJLHMM.HKGAACMIPIH.Legacy || !directSpawnPositionValid) return;
			CaptureNetworkAnchor(__instance.GetComponent<MachineController>(), "before-structure");
			if (!networkAnchorRepaired)
				throw new InvalidOperationException("Native network anchor is unavailable; replacement structure was not sent.");
		}

		private static void CaptureNetworkAnchor(MachineController machineController_0, string phase)
		{
			MachineSerializer serializer = machineController_0 ? machineController_0.GetComponent<MachineSerializer>() : null;
			Vector3 previous = serializer ? serializer.JJMDFCDDJBA : Vector3.zero;
			int blocks;
			Vector3 anchor;
			networkAnchorRepaired = LegacyMachineNetworkAnchor.TryCapture(machineController_0, out blocks, out anchor);
			repairedNetworkAnchorBlocks = blocks;
			repairedNetworkAnchor = anchor;
			NetworkView view = machineController_0 ? machineController_0.GetComponent<NetworkView>() : null;
			Log("NETWORK_ANCHOR result=" + (networkAnchorRepaired ? "CAPTURED" : "FAILED")
				+ " phase=" + phase
				+ " root=" + Quote(machineController_0 ? machineController_0.gameObject.name : null)
				+ " view=" + (view ? view.viewID.ToString() : "<none>")
				+ " blocks=" + blocks
				+ " previousAnchor=" + FormatPosition(previous)
				+ " anchor=" + FormatPosition(anchor));
		}

		private static bool ShowDebugMessagePrefix(string msg)
		{
			if (suppressDebugMessageFrame < 0)
				return true;

			int frame = suppressDebugMessageFrame;
			suppressDebugMessageFrame = -1;
			if (frame != Time.frameCount)
				return true;

			Log("MESSAGE suppressed reason=deferred-dispatch text=" + Quote(msg));
			return false;
		}

		private static bool LocalDestroyPrefix(UnityEngine.Object object_0)
		{
			if (HNJDDKJLHMM.FHLGOMHPDLN != HNJDDKJLHMM.HKGAACMIPIH.Legacy)
				return true;

			if (!object.ReferenceEquals(legacyNetworkDestroyedRoot, null)
				&& object.ReferenceEquals(object_0, legacyNetworkDestroyedRoot))
			{
				legacyNetworkDestroyedRoot = null;
				return false;
			}

			legacyNetworkDestroyedRoot = null;
			return true;
		}

		private static void Log(string message)
		{
			string text = "[MACHINECHANGE-LEGACY] " + message;
			try { global::mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(text); }
			catch (Exception) { }
		}
	}
}
