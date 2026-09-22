using System;
using System.Collections.Generic;

namespace MPatcherFork.CustomPatches
{
    // A catalogue outage is evidence to attempt recovery, not proof of a bad
    // game port. Bound the disruption; one failed cycle disables automatic
    // retries until fresh catalogue success, with a ten-minute cooldown.
    internal sealed class LegacyHostMigrationPolicy
    {
        private double absentSince = -1, cooldown;
        private bool failedCycle;
        internal void Observe(double now, bool listed)
        { if (listed) { absentSince = -1; failedCycle = false; } else if (absentSince < 0) absentSince = now; }
        internal bool Due(double now, int failures)
        { return !failedCycle && failures >= 3 && absentSince >= 0 && now - absentSince >= 120 && now >= cooldown; }
        internal void Attempted(double now) { cooldown = now + 600; }
        internal void Finished(bool verified) { failedCycle = !verified; absentSince = -1; }
        internal static int[] Alternatives(int currentPort)
        {
            List<int> result = new List<int>();
            foreach (int port in new int[] { 23466, 35017, 25551 }) if (port != currentPort) result.Add(port);
            return result.ToArray();
        }
    }

    internal sealed class LegacyMigrationBarrier
    {
        private readonly HashSet<string> expected = new HashSet<string>();
        private readonly HashSet<string> arrived = new HashSet<string>();
        internal readonly string Epoch;
        internal LegacyMigrationBarrier(string epoch, IEnumerable<string> tokens)
        { Epoch = epoch; foreach (string token in tokens) if (!expected.Add(token)) throw new ArgumentException("Duplicate migration participant"); }
        internal bool Accept(string epoch, string token, bool authenticated)
        { return authenticated && epoch == Epoch && expected.Contains(token) && arrived.Add(token); }
        internal bool Contains(string token) { return expected.Contains(token); }
        internal bool Complete { get { return arrived.Count == expected.Count; } }
        internal int Count { get { return expected.Count; } }
        internal int Arrived { get { return arrived.Count; } }
    }
}
