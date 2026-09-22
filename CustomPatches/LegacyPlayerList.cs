using System;
using System.Collections.Generic;
using McnCraft;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
	internal static class LegacyPlayerListPolicy
	{
		internal static bool CanRetireReplica(bool client, bool exactOldId, bool differentMachine,
			bool remoteClone, bool localMachine, bool ownedView)
		{
			return client && exactOldId && differentMachine && remoteClone && !localMachine && !ownedView;
		}

		internal static T RecordRebind<T>(Dictionary<T, T> aliases, T oldId, T newId)
		{
			EqualityComparer<T> comparer = EqualityComparer<T>.Default;
			T next;
			// An older buffered A -> B can arrive after B -> C. Never undo C.
			for (int remaining = aliases.Count; remaining > 0 && aliases.TryGetValue(newId, out next); remaining--)
			{
				if (comparer.Equals(next, newId)) break;
				newId = next;
			}
			List<T> previous = new List<T>();
			foreach (KeyValuePair<T, T> pair in aliases)
				if (comparer.Equals(pair.Value, oldId)) previous.Add(pair.Key);
			for (int index = 0; index < previous.Count; index++) aliases[previous[index]] = newId;
			if (!comparer.Equals(oldId, newId)) aliases[oldId] = newId;
			return newId;
		}

		internal static List<T> ForgetRebind<T>(Dictionary<T, T> aliases, T oldId, T newId)
		{
			EqualityComparer<T> comparer = EqualityComparer<T>.Default;
			List<T> retired = new List<T>();
			foreach (KeyValuePair<T, T> pair in aliases)
				if (comparer.Equals(pair.Key, oldId) || comparer.Equals(pair.Key, newId)
					|| comparer.Equals(pair.Value, oldId) || comparer.Equals(pair.Value, newId)) retired.Add(pair.Key);
			for (int index = 0; index < retired.Count; index++) aliases.Remove(retired[index]);
			return retired;
		}

		// The UI appends its own local-player row after the remote list. Preserve
		// the first occurrence and order of every distinct remote object.
		internal static int Normalize<T>(List<T> entries, T local) where T : class
		{
			int removed = 0;
			for (int index = 0; index < entries.Count; )
			{
				T entry = entries[index];
				bool discard = object.ReferenceEquals(entry, null) || object.ReferenceEquals(entry, local);
				for (int previous = 0; !discard && previous < index; previous++)
					discard = object.ReferenceEquals(entries[previous], entry);
				if (discard) { entries.RemoveAt(index); removed++; }
				else index++;
			}
			return removed;
		}

		internal static int Remove<T>(List<T> entries, T target) where T : class
		{
			int removed = 0;
			for (int index = entries.Count - 1; index >= 0; index--)
			{
				if (!object.ReferenceEquals(entries[index], target)) continue;
				entries.RemoveAt(index);
				removed++;
			}
			return removed;
		}
	}

	internal static class LegacyPlayerList
	{
		internal static void Normalize(string stage, MachineController subject)
		{
			if (!LegacyTransientReconnect.IsLegacy()) return;
			try
			{
				List<MachineController> entries = Arena.PBBCHKBJAEA;
				Game game = Arena.OEDCBNHNGMJ as Game;
				MachineController local = game == null ? null : game.FICMBCLEFDL;
				int before = entries.Count;
				int subjectReferences = CountReferences(entries, subject);
				// Unity's destroyed objects can still have non-null managed wrappers.
				for (int index = entries.Count - 1; index >= 0; index--)
					if (entries[index] == null) entries.RemoveAt(index);
				LegacyPlayerListPolicy.Normalize(entries, local);
				int removed = before - entries.Count;
				if (removed > 0) Game.IGEAEEAMAPM = true;
				LegacyTransientReconnect.Log("PLAYER_LIST_NORMALIZED stage=" + stage
					+ " role=" + Role() + " before=" + before + " after=" + entries.Count
					+ " removed=" + removed + " subjectReferencesBefore=" + subjectReferences
					+ " subjectReferencesAfter=" + CountReferences(entries, subject)
					+ " subject=" + Describe(subject));
				// Event-driven inventory distinguishes a repeated reference from two
				// actual replicas; never merge players by their display name or ID.
				if (removed > 0 || stage != "native-name")
					LogEntries(stage, entries, local);
			}
			catch (Exception error)
			{
				LegacyTransientReconnect.Log("PLAYER_LIST_NORMALIZE_FAILED stage=" + stage
					+ " type=" + error.GetType().Name);
			}
		}

		internal static void Remove(MachineController machine, string stage)
		{
			if (!LegacyTransientReconnect.IsLegacy() || object.ReferenceEquals(machine, null)) return;
			DestroyTag(machine, stage);
			try
			{
				int removed = LegacyPlayerListPolicy.Remove(Arena.PBBCHKBJAEA, machine);
				if (removed == 0) return;
				Game.IGEAEEAMAPM = true;
				LegacyTransientReconnect.Log("PLAYER_LIST_REMOVED stage=" + stage + " role=" + Role()
					+ " removed=" + removed + " remaining=" + Arena.PBBCHKBJAEA.Count
					+ " subject=" + Describe(machine));
			}
			catch (Exception error)
			{
				LegacyTransientReconnect.Log("PLAYER_LIST_REMOVE_FAILED stage=" + stage
					+ " type=" + error.GetType().Name);
			}
		}

		internal static bool DestroyTag(MachineController machine, string stage)
		{
			if (object.ReferenceEquals(machine, null)) return false;
			try
			{
				GameObject tag = machine.IBIEMAGMJAG;
				if (tag == null) return false;
				int tagInstance = tag.GetInstanceID();
				machine.IBIEMAGMJAG = null;
				UnityEngine.Object.Destroy(tag);
				LegacyTransientReconnect.Log("PLAYER_TAG_DESTROYED stage=" + stage
					+ " machineInstance=" + machine.GetInstanceID() + " tagInstance=" + tagInstance);
				return true;
			}
			catch (Exception error)
			{
				LegacyTransientReconnect.Log("PLAYER_TAG_DESTROY_FAILED stage=" + stage
					+ " type=" + error.GetType().Name);
				return false;
			}
		}

		internal static void ObserveRebind(NetworkViewID oldId, NetworkViewID newId, NetworkView current)
		{
			try
			{
				MachineController machine = LegacyTransientReconnect.FindMachineForView(current);
				if (Network.isClient && current != null && current.viewID == newId
					&& machine != null && oldId != newId && oldId != NetworkViewID.unassigned)
				{
					Game game = Arena.OEDCBNHNGMJ as Game;
					MachineController local = game == null ? null : game.FICMBCLEFDL;
					UnityEngine.Object[] views = UnityEngine.Object.FindObjectsOfType(typeof(NetworkView));
					for (int index = 0; index < views.Length; index++)
					{
						NetworkView old = views[index] as NetworkView;
						if (old == null || old == current || old.viewID != oldId) continue;
						// Only remove a whole remote machine named by the server's
						// old -> new mapping, never a child view or the preserved owner.
						MachineController obsolete = old.GetComponent<MachineController>();
						if (obsolete == null) continue;
						bool retire = LegacyPlayerListPolicy.CanRetireReplica(Network.isClient,
							old.viewID == oldId, !object.ReferenceEquals(obsolete, machine),
							obsolete.GEOICBCHPNG == EntityController.OCFNGMHHLLM.NetClone,
							object.ReferenceEquals(obsolete, local), old.isMine);
						LegacyTransientReconnect.Log("PLAYER_LIST_DISTINCT_REPLICA old=" + Describe(obsolete)
							+ " current=" + Describe(machine) + " action=" + (retire ? "retire-local-replica" : "preserve"));
						if (!retire) continue;
						Remove(obsolete, "superseded-replay");
						// Local cleanup must not send Network.Destroy for the buffered
						// original, which still represents the live retained machine.
						UnityEngine.Object.Destroy(old.gameObject);
					}
				}
				Normalize("peer-rebind", machine);
			}
			catch (Exception error)
			{
				LegacyTransientReconnect.Log("PLAYER_LIST_REBIND_AUDIT_FAILED type=" + error.GetType().Name);
			}
		}

		private static int CountReferences(List<MachineController> entries, MachineController machine)
		{
			int count = 0;
			for (int index = 0; index < entries.Count; index++)
				if (object.ReferenceEquals(entries[index], machine)) count++;
			return count;
		}

		private static void LogEntries(string stage, List<MachineController> entries, MachineController local)
		{
			LegacyTransientReconnect.Log("PLAYER_LIST_LOCAL stage=" + stage + " subject=" + Describe(local));
			for (int index = 0; index < entries.Count && index < 64; index++)
				LegacyTransientReconnect.Log("PLAYER_LIST_ENTRY stage=" + stage + " index=" + index
					+ " subject=" + Describe(entries[index]));
		}

		private static string Role()
		{
			return Network.isServer ? "host" : Network.isClient ? "client" : "disconnected";
		}

		private static string Describe(MachineController machine)
		{
			if (machine == null) return "missing";
			NetworkView view = machine.GetComponent<NetworkView>();
			return "instance=" + machine.GetInstanceID() + ",name="
				+ LegacyTransientReconnect.Clean(machine.name) + ",kind=" + machine.GEOICBCHPNG
				+ ",playerId=" + machine.AKAFEPJIFKC + ",localId=" + machine.LCKDHPKIPEI
				+ ",view=" + (view == null ? "missing" : LegacyTransientReconnect.IdLabel(view.viewID))
				+ ",owner=" + (view == null ? "missing" : LegacyTransientReconnect.PlayerLabel(view.owner));
		}
	}
}
