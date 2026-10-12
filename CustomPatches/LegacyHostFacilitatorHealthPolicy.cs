namespace MPatcherFork.CustomPatches
{
	internal enum LegacyHostFacilitatorHealth
	{
		Unknown = -1,
		Missing = 0,
		Connected = 1,
		Connecting = 2,
		Disconnecting = 3
	}

	// Catalogue visibility is deliberately not an input. Two separated native
	// terminal observations are required; unknown/transitional states never count.
	internal sealed class LegacyHostFacilitatorHealthPolicy
	{
		internal const double ProbeIntervalSeconds = 5d;
		internal const double InitialSettleSeconds = 10d;
		private double nextProbe;
		private int missingSamples;
		internal LegacyHostFacilitatorHealth Health { get; private set; }
		internal int MissingSamples { get { return missingSamples; } }

		internal void Begin(double now)
		{
			Health = LegacyHostFacilitatorHealth.Unknown;
			missingSamples = 0;
			nextProbe = now + InitialSettleSeconds;
		}

		internal bool ProbeDue(double now) { return now >= nextProbe; }

		internal bool Observe(double now, LegacyHostFacilitatorHealth health)
		{
			if (!ProbeDue(now)) return false;
			nextProbe = now + ProbeIntervalSeconds;
			Health = health;
			missingSamples = health == LegacyHostFacilitatorHealth.Missing
				? System.Math.Min(2, missingSamples + 1) : 0;
			return missingSamples >= 2;
		}
	}
}
