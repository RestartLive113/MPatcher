using System.Collections.Generic;
using McnCraft;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
	// MachineController.Initialize passes this exact list to DestroyTrash after
	// EOJLILALBGB has disabled redundant construction objects. Native deletion is
	// spread over frames (16 objects/frame); these are not pending live controls.
	internal static class LegacyNativeConstructionTrash
	{
		private static readonly Dictionary<MachineController, HashSet<GameObject>> pending =
			new Dictionary<MachineController, HashSet<GameObject>>();

		internal static void Remember(MachineController machine, List<GameObject> objects)
		{
			if (machine == null) return;
			pending.Remove(machine);
			if (objects == null || objects.Count == 0) return;
			HashSet<GameObject> roots = new HashSet<GameObject>();
			foreach (GameObject item in objects) if (item != null) roots.Add(item);
			pending[machine] = roots;
			LegacyTransientReconnect.Log("CLIENT_NATIVE_TRASH_TRACKED objects=" + roots.Count
				+ " source=MachineController.DestroyTrash nativeDeletion=unchanged");
		}

		internal static bool Contains(MachineController machine, PartsController part)
		{
			HashSet<GameObject> roots;
			if (machine == null || part == null || part.gameObject.activeInHierarchy
				|| !pending.TryGetValue(machine, out roots)) return false;
			for (Transform current = part.transform; current != null; current = current.parent)
				if (roots.Contains(current.gameObject)) return true;
			return false;
		}

		internal static void Forget(MachineController machine)
		{
			if (!object.ReferenceEquals(machine, null)) pending.Remove(machine);
		}

		internal static void Clear() { pending.Clear(); }
	}
}
