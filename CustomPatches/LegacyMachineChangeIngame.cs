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
	// rejects, while preserving the same machine-regulation checks used by Lobby.
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
		}

		private const string PatchId = "local.moddev.machinecraft.machinechange-legacy.v5";
		private const int SpawnReadyTimeoutSeconds = 5;

		private static Harmony harmony;
		private static GameObject legacyNetworkDestroyedRoot;
		private static MachineSelectionSnapshot pendingSelection;

		internal static void TryRegister()
		{
			if (harmony != null)
				return;

			try
			{
				Type patchType = typeof(global::dyl7NQFWvb8SnwY4dXogp_aCQhx2Y7dLGUBgzCus25T9Wpo6h01g9Y342KDBl8ctV_NzWUOypgYMsbS0RAESeaU);
				MethodInfo meetingAction = AccessTools.Method(typeof(Meeting), "BDKIMPEDKCJ", new Type[] { typeof(string), typeof(GameObject) });
				MethodInfo networkDestroy = AccessTools.Method(patchType, "smethod_19", new Type[] { typeof(GameObject) });
				MethodInfo localDestroy = AccessTools.Method(patchType, "smethod_20", new Type[] { typeof(UnityEngine.Object) });

				MethodInfo meetingActionPrefix = AccessTools.Method(typeof(LegacyMachineChangeIngame), "MeetingActionPrefix");
				MethodInfo networkDestroyPrefix = AccessTools.Method(typeof(LegacyMachineChangeIngame), "NetworkDestroyPrefix");
				MethodInfo localDestroyPrefix = AccessTools.Method(typeof(LegacyMachineChangeIngame), "LocalDestroyPrefix");

				if (meetingAction == null || networkDestroy == null || localDestroy == null
					|| meetingActionPrefix == null || networkDestroyPrefix == null || localDestroyPrefix == null)
				{
					throw new MissingMethodException("Recovered MachineChangeIngame methods");
				}

				harmony = new Harmony(PatchId);
				PatchPrefix(meetingAction, meetingActionPrefix);
				PatchPrefix(networkDestroy, networkDestroyPrefix);
				PatchPrefix(localDestroy, localDestroyPrefix);

				Log("REGISTERED entry=Meeting.BDKIMPEDKCJ validator=Legacy-regulation destroy=Unity-Network");
			}
			catch (Exception error)
			{
				Log("REGISTER_FAILED type=" + error.GetType().Name + " message=" + error.Message);
			}
		}

		private static bool MeetingActionPrefix(string DPGKEOAGONA, GameObject NGLBLAGMBLN, Meeting __instance)
		{
			if (HNJDDKJLHMM.FHLGOMHPDLN != HNJDDKJLHMM.HKGAACMIPIH.Legacy
				|| JKGKJLLFMLE.EGFHGHKLNAO != JKGKJLLFMLE.LENPCAMMAEP.Meeting)
			{
				return true;
			}

			string pending = global::dyl7NQFWvb8SnwY4dXogp_aCQhx2Y7dLGUBgzCus25T9Wpo6h01g9Y342KDBl8ctV_NzWUOypgYMsbS0RAESeaU.Class50.WCKsvBPB6cSYds0fexVu_00247Y;
			string selectedText = string.Empty;
			string selectedColor = string.Empty;
			try
			{
				if (NGLBLAGMBLN && NGLBLAGMBLN.transform.childCount > 0)
				{
					UnityEngine.UI.Text text = NGLBLAGMBLN.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>();
					if (text)
					{
						selectedText = text.text;
						selectedColor = text.color.ToString();
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

			return true;
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

		internal static bool ValidateCurrentSelection()
		{
			HashSet<int> activeObjectsBefore = CaptureActiveGameObjects();
			try
			{
				bool allowed = ValidateLegacyMachine();
				int leakedRoots = CleanupValidationLeaks(activeObjectsBefore);
				if (leakedRoots > 0)
					Log("VALIDATE_CLEANUP result=removed roots=" + leakedRoots + " outcome=" + (allowed ? "ALLOW" : "DENY"));
				return allowed;
			}
			catch (Exception error)
			{
				int leakedRoots = CleanupValidationLeaks(activeObjectsBefore);
				Log("VALIDATE_FAILED type=" + error.GetType().Name
					+ " message=" + error.Message
					+ " leakedRootsRemoved=" + leakedRoots
					+ " stack=" + Quote(error.StackTrace));
				return false;
			}
		}

		private static HashSet<int> CaptureActiveGameObjects()
		{
			HashSet<int> ids = new HashSet<int>();
			GameObject[] objects = UnityEngine.Object.FindObjectsOfType<GameObject>();
			foreach (GameObject gameObject in objects)
			{
				if (gameObject)
					ids.Add(gameObject.GetInstanceID());
			}
			return ids;
		}

		private static int CleanupValidationLeaks(HashSet<int> activeObjectsBefore)
		{
			Dictionary<int, GameObject> newRoots = new Dictionary<int, GameObject>();
			GameObject[] activeObjectsAfter = UnityEngine.Object.FindObjectsOfType<GameObject>();
			foreach (GameObject gameObject in activeObjectsAfter)
			{
				if (!gameObject || activeObjectsBefore.Contains(gameObject.GetInstanceID()))
					continue;

				Transform rootTransform = gameObject.transform;
				while (rootTransform.parent
					&& !activeObjectsBefore.Contains(rootTransform.parent.gameObject.GetInstanceID()))
				{
					rootTransform = rootTransform.parent;
				}

				GameObject root = rootTransform.gameObject;
				if (!activeObjectsBefore.Contains(root.GetInstanceID()))
					newRoots[root.GetInstanceID()] = root;
			}

			int removed = 0;
			foreach (GameObject root in newRoots.Values)
			{
				if (!root)
					continue;

				MachineController machine = root.GetComponentInChildren<MachineController>(true);
				NetworkView networkView = root.GetComponentInChildren<NetworkView>(true);
				if (machine || networkView)
				{
					Log("VALIDATE_LEAK skipped root=" + Quote(root.name)
						+ " position=" + FormatPosition(root.transform.position)
						+ " machine=" + (bool)machine
						+ " network=" + (bool)networkView);
					continue;
				}

				BlockController block = root.GetComponentInChildren<BlockController>(true);
				string rootName = root.name;
				Vector3 rootPosition = root.transform.position;
				root.SetActive(false);
				UnityEngine.Object.Destroy(root);
				removed++;
				Log("VALIDATE_LEAK removed root=" + Quote(rootName)
					+ " position=" + FormatPosition(rootPosition)
					+ " block=" + (bool)block);
			}

			return removed;
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

			pendingSelection = new MachineSelectionSnapshot
			{
				MachineName = JKGKJLLFMLE.IGOBPLOLHEP.machineName,
				FolderName = JKGKJLLFMLE.IGOBPLOLHEP.folderName,
				Build = JKGKJLLFMLE.HHGILAIOCLG,
				Assign = JKGKJLLFMLE.MIIGKEBFKKD,
				MachineDataLoaded = JKGKJLLFMLE.MLBCKBAPAMJ,
				MachineAudioReady = JKGKJLLFMLE.KAOJMNJNLLM,
				ActiveRoot = meeting ? meeting.JPIAFJHAPHM : null,
				ActiveController = meeting ? meeting.FICMBCLEFDL : null,
				ControllerIds = controllerIds
			};

			Log("TRANSACTION begin machine=" + Quote(pendingSelection.MachineName)
				+ " folder=" + Quote(pendingSelection.FolderName)
				+ " activeRoot=" + Quote(pendingSelection.ActiveRoot ? pendingSelection.ActiveRoot.name : null)
				+ " activePosition=" + FormatPosition(pendingSelection.ActiveController)
				+ " controllers=" + controllerIds.Count);
		}

		internal static void CommitSelectionTransaction()
		{
			MachineSelectionSnapshot snapshot = pendingSelection;
			pendingSelection = null;
			Log("TRANSACTION commit previous=" + Quote(snapshot == null ? null : snapshot.MachineName)
				+ " current=" + Quote(JKGKJLLFMLE.IGOBPLOLHEP.machineName));
		}

		internal static void RollbackSelectionTransaction(string reason)
		{
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
			string rootName = replacement ? replacement.gameObject.name : null;
			Vector3 stagingPosition = replacement ? replacement.transform.position : Vector3.zero;
			bool ready = false;
			int missingSensors = -1;
			int waitedSeconds = 0;

			while (replacement && waitedSeconds < SpawnReadyTimeoutSeconds)
			{
				yield return new WaitForSeconds(1f);
				waitedSeconds++;
				ready = AreSledSensorsReady(replacement, out missingSensors);
				if (ready)
					break;
			}

			if (!replacement)
			{
				Log("SPAWN_FINALIZE skipped reason=replacement-destroyed root=" + Quote(rootName));
				yield break;
			}

			Vector3 beforeWarp = replacement.transform.position;
			try
			{
				replacement.Warp(position, rotation, true);
				Log("SPAWN_FINALIZE result=WARP root=" + Quote(rootName)
					+ " staging=" + FormatPosition(stagingPosition)
					+ " before=" + FormatPosition(beforeWarp)
					+ " target=" + FormatPosition(position)
					+ " after=" + FormatPosition(replacement.transform.position)
					+ " waitedSeconds=" + waitedSeconds
					+ " sledSensorsReady=" + ready
					+ " missingSensors=" + missingSensors
					+ (ready ? string.Empty : " timeout=True"));
			}
			catch (Exception error)
			{
				Log("SPAWN_FINALIZE_FAILED root=" + Quote(rootName)
					+ " type=" + error.GetType().Name + " message=" + error.Message);
			}
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
				if (!body)
					continue;

				SledController[] sleds = body.GetComponentsInChildren<SledController>();
				foreach (SledController sled in sleds)
				{
					if (!sled || !sled.GetComponent<ContactSensor>())
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

		private static bool ValidateLegacyMachine()
		{
			BuildData build = JKGKJLLFMLE.HHGILAIOCLG;
			RegulationData regulation = JKGKJLLFMLE.JNOGNOMLMEA;
			if (build == null || !build.isReady || build.blockData == null || build.blockData.Count == 0)
			{
				Log("VALIDATE result=DENY reason=build-not-ready");
				return false;
			}
			if (regulation == null)
			{
				Log("VALIDATE result=DENY reason=regulation-unavailable");
				return false;
			}

			List<GameObject> temporaryBlocks = new List<GameObject>();
			try
			{
				HDBLLPODNLN summary = global::dyl7NQFWvb8SnwY4dXogp_aCQhx2Y7dLGUBgzCus25T9Wpo6h01g9Y342KDBl8ctV_NzWUOypgYMsbS0RAESeaU.smethod_1();
				HIPBCCKFFAG bounds = global::dyl7NQFWvb8SnwY4dXogp_aCQhx2Y7dLGUBgzCus25T9Wpo6h01g9Y342KDBl8ctV_NzWUOypgYMsbS0RAESeaU.smethod_2();
				global::dyl7NQFWvb8SnwY4dXogp_aCQhx2Y7dLGUBgzCus25T9Wpo6h01g9Y342KDBl8ctV_NzWUOypgYMsbS0RAESeaU.smethod_3(summary, false);

				int blockIndex = 0;
				foreach (BlockData block in build.blockData)
				{
					GameObject blockObject;
					try
					{
						blockObject = global::dyl7NQFWvb8SnwY4dXogp_aCQhx2Y7dLGUBgzCus25T9Wpo6h01g9Y342KDBl8ctV_NzWUOypgYMsbS0RAESeaU.smethod_4(block, false);
					}
					catch (Exception error)
					{
						Log("VALIDATE_BLOCK_FAILED index=" + blockIndex
							+ " type=" + block.type
							+ " gid=" + block.gid
							+ " typeError=" + error.GetType().Name
							+ " message=" + error.Message);
						throw;
					}
					if (!blockObject)
					{
						blockIndex++;
						continue;
					}

					temporaryBlocks.Add(blockObject);
					blockObject.SetActive(false);
					summary.HDLEKABOEFL(blockObject.GetComponent<BlockController>());
					blockIndex++;
				}

				summary.ANBKLJFHMOB(null);
				summary.JKAJGAGDMAJ();
				summary.ACBKKKLCJCH();

				if (build.size == 0)
				{
					Exception boundsError = null;
					try
					{
						try
						{
							bounds.ACMGPBMMKNI(true, false);
						}
						catch (Exception error)
						{
							// The original MPatcher deliberately continued here. Some valid
							// builds (for example Steam/Workshop exports) leave the bounds
							// calculator with a partial result before it throws.
							boundsError = error;
						}

						build.size = bounds.CBEGHPGKNNI;
						build.spawnAltOffset = Mathf.RoundToInt(0f - bounds.MFGJHOHNCDB.min.y) + 1;
						if (boundsError != null)
						{
							Log("VALIDATE_BOUNDS_PARTIAL machine=" + Quote(JKGKJLLFMLE.IGOBPLOLHEP.machineName)
								+ " type=" + boundsError.GetType().Name
								+ " message=" + Quote(boundsError.Message)
								+ " size=" + build.size
								+ " spawnAltOffset=" + build.spawnAltOffset);
						}
					}
					finally
					{
						bounds.MEDPEFNEGIG(false);
					}
					JKGKJLLFMLE.BOMAFGLNGMI();
				}

				List<string> failures = new List<string>();
				int bodies = summary.KADEOCMCJLA();
				int cost = summary.OHNPKPMDHGK();
				float weight = summary.PCFKNOAKFHD;

				if (bodies > 65)
					failures.Add("bodies=" + bodies + ">65");

				bool bossExemption = (regulation.gameType == JKGKJLLFMLE.LENPCAMMAEP.BossHunt
					|| regulation.gameType == JKGKJLLFMLE.LENPCAMMAEP.Meeting)
					&& JKGKJLLFMLE.IGOBPLOLHEP.machineName.StartsWith("BOSS_", StringComparison.Ordinal);

				if (!bossExemption)
				{
					CheckMaximum(failures, "cost", cost, regulation.maxCost);
					CheckMaximum(failures, "size", build.size, regulation.maxSize);
					if (weight < regulation.minWeight)
						failures.Add("weight=" + weight + "<" + regulation.minWeight);
					if (weight > regulation.maxWeight)
						failures.Add("weight=" + weight + ">" + regulation.maxWeight);

					CheckBlockMaximum(failures, summary, BlockData.AAHMDBHDCDK.JointTS, regulation.maxJoint, "joint");
					CheckBlockMaximum(failures, summary, BlockData.AAHMDBHDCDK.Thruster, regulation.maxThruster, "thruster");
					CheckBlockMaximum(failures, summary, BlockData.AAHMDBHDCDK.AGDevice, regulation.maxAGD, "agd");
					CheckBlockMaximum(failures, summary, BlockData.AAHMDBHDCDK.Wheel, regulation.maxWheel, "wheel");
					CheckBlockMaximum(failures, summary, BlockData.AAHMDBHDCDK.Shaft, regulation.maxShaft, "shaft");
					CheckBlockMaximum(failures, summary, BlockData.AAHMDBHDCDK.Mover, regulation.maxMover, "mover");
					int cannons = summary.BGIDJHJBICM[(int)BlockData.AAHMDBHDCDK.Cannon1]
						+ summary.BGIDJHJBICM[(int)BlockData.AAHMDBHDCDK.Cannon2];
					CheckMaximum(failures, "cannon", cannons, regulation.maxCannon);
					CheckBlockMaximum(failures, summary, BlockData.AAHMDBHDCDK.Sword, regulation.maxSword, "sword");
					CheckBlockMaximum(failures, summary, BlockData.AAHMDBHDCDK.Discharger, regulation.maxDischarger, "discharger");
					CheckBlockMaximum(failures, summary, BlockData.AAHMDBHDCDK.Launcher, regulation.maxLauncher, "launcher");
					CheckBlockMaximum(failures, summary, BlockData.AAHMDBHDCDK.Beamer, regulation.maxBeamer, "beamer");
					CheckBlockMaximum(failures, summary, BlockData.AAHMDBHDCDK.Shield, regulation.maxShield, "shield");
					CheckBlockMaximum(failures, summary, BlockData.AAHMDBHDCDK.Tracker, regulation.maxTracker, "tracker");
					CheckMaximum(failures, "tire", summary.GPKANGPHHMC, regulation.maxTireSize);
					CheckMaximum(failures, "wing", summary.CAACNMEKOOC, regulation.maxWingSize);
				}

				if (!JKGKJLLFMLE.IGOBPLOLHEP.isExpert && (int)regulation.worldType == 5)
					failures.Add("space-requires-expert");

				bool allowed = failures.Count == 0;
				Log("VALIDATE result=" + (allowed ? "ALLOW" : "DENY")
					+ " machine=" + JKGKJLLFMLE.IGOBPLOLHEP.machineName
					+ " blocks=" + build.blockData.Count
					+ " bodies=" + bodies
					+ " cost=" + cost
					+ " weight=" + weight
					+ (allowed ? string.Empty : " reasons=" + string.Join(",", failures.ToArray())));
				return allowed;
			}
			finally
			{
				foreach (GameObject temporaryBlock in temporaryBlocks)
				{
					if (temporaryBlock)
						UnityEngine.Object.Destroy(temporaryBlock);
				}
			}
		}

		private static void CheckBlockMaximum(List<string> failures, HDBLLPODNLN summary,
			BlockData.AAHMDBHDCDK type, int maximum, string label)
		{
			CheckMaximum(failures, label, summary.BGIDJHJBICM[(int)type], maximum);
		}

		private static void CheckMaximum(List<string> failures, string label, int actual, int maximum)
		{
			if (actual > maximum)
				failures.Add(label + "=" + actual + ">" + maximum);
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
