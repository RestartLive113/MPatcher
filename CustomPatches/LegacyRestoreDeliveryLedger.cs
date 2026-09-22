using System;
using System.Collections.Generic;

namespace MPatcherFork.CustomPatches
{
	// Owned by one scene/session controller. Keep receipts after ACK/status completion:
	// a reliable retransmission is not another request to restore physical state.
	internal sealed class LegacyRestoreDeliveryLedger<TView>
	{
		private sealed class Receipt
		{
			internal int Latest;
			internal int Applied;
			internal WeakReference Machine;
		}
		private readonly Dictionary<TView, Dictionary<string, Receipt>> views =
			new Dictionary<TView, Dictionary<string, Receipt>>();

		// 0 = receive/continue, 1 = already applied, 2 = stale.
		internal int Observe(TView view, string owner, int snapshot)
		{
			Receipt receipt = Get(view, owner);
			if (snapshot < receipt.Latest) return 2;
			if (snapshot == receipt.Applied) return 1;
			receipt.Latest = snapshot;
			return 0;
		}

		internal void Applied(TView view, string owner, int snapshot, object machine)
		{
			Receipt receipt = Get(view, owner);
			receipt.Latest = Math.Max(receipt.Latest, snapshot);
			receipt.Applied = snapshot;
			receipt.Machine = new WeakReference(machine);
		}

		internal bool MatchesGeneration(TView view, string owner, int snapshot, object machine)
		{
			Receipt receipt = Get(view, owner);
			return receipt.Applied == snapshot && receipt.Machine != null
				&& object.ReferenceEquals(receipt.Machine.Target, machine);
		}

		internal void Clear() { views.Clear(); }

		private Receipt Get(TView view, string owner)
		{
			Dictionary<string, Receipt> owners;
			if (!views.TryGetValue(view, out owners))
			{
				owners = new Dictionary<string, Receipt>(StringComparer.Ordinal);
				views.Add(view, owners);
			}
			Receipt receipt;
			if (!owners.TryGetValue(owner, out receipt))
			{
				receipt = new Receipt();
				owners.Add(owner, receipt);
			}
			return receipt;
		}
	}

	internal static class LegacyRetirementIds
	{
		internal static List<T> Collect<T>(T original, T previous, T current, T unassigned)
		{
			List<T> result = new List<T>();
			foreach (T id in new T[] { original, previous, current })
				if (!EqualityComparer<T>.Default.Equals(id, unassigned) && !result.Contains(id)) result.Add(id);
			return result;
		}
	}
}
