namespace MPatcherFork.CustomPatches
{
	internal enum LegacyHostFacilitatorAction
	{
		None,
		Reconnect
	}

	// The native Unity attempt lasts about six seconds (12 sends, 500 ms apart),
	// so retries start after eight seconds and never run more often than every
	// thirty seconds during a prolonged outage.
	internal sealed class LegacyHostFacilitatorRetryPolicy
	{
		private static readonly double[] RetryDelaysSeconds = { 8d, 15d, 30d };

		internal bool Pending { get; private set; }
		internal int Attempts { get; private set; }
		internal double DueAt { get; private set; }
		internal string Reason { get; private set; }

		internal LegacyHostFacilitatorRetryPolicy()
		{
			Reason = "native facilitator state has not been observed";
		}

		internal LegacyHostFacilitatorAction Tick(double now, bool sameLiveHost, bool nativeReconnectPending)
		{
			return Tick(now, sameLiveHost, nativeReconnectPending, true);
		}

		internal LegacyHostFacilitatorAction Tick(double now, bool sameLiveHost, bool nativeReconnectPending, bool canReconnect)
		{
			if (!sameLiveHost)
			{
				Reset("captured host stopped or changed");
				return LegacyHostFacilitatorAction.None;
			}
			if (!nativeReconnectPending)
			{
				Reset(Pending ? "native facilitator connection restored" : "no native facilitator loss flag");
				return LegacyHostFacilitatorAction.None;
			}
			if (!Pending)
			{
				Pending = true;
				Attempts = 0;
				DueAt = now + RetryDelaysSeconds[0];
				Reason = "native loss observed; Unity first reconnect left unchanged";
				return LegacyHostFacilitatorAction.None;
			}
			if (now < DueAt) return LegacyHostFacilitatorAction.None;
			if (!canReconnect) return LegacyHostFacilitatorAction.None;

			Attempts++;
			int nextIndex = System.Math.Min(Attempts, RetryDelaysSeconds.Length - 1);
			DueAt = now + RetryDelaysSeconds[nextIndex];
			Reason = "native reconnect retry due";
			return LegacyHostFacilitatorAction.Reconnect;
		}

		internal void Reset(string reason)
		{
			Pending = false;
			Attempts = 0;
			DueAt = 0d;
			Reason = reason;
		}
	}
}
