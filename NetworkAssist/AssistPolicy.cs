using System;
using System.Collections.Generic;

namespace MPatcher.NetworkAssist
{
    // Shared by the service and isolated tests. A lease belongs to a process generation.
    internal sealed class AssistLeases
    {
        internal const int LifetimeSeconds = 20;
        private sealed class Lease { internal long Generation; internal DateTime Expires; }
        private readonly Dictionary<int, Lease> entries = new Dictionary<int, Lease>();
        internal int Count { get { return entries.Count; } }
        internal void Refresh(int pid, long generation, DateTime now)
        {
            entries[pid] = new Lease { Generation = generation, Expires = now.AddSeconds(LifetimeSeconds) };
        }
        internal void Remove(int pid) { entries.Remove(pid); }
        internal void Clear() { entries.Clear(); }
        internal void Prune(DateTime now, Func<int, long, bool> alive)
        {
            List<int> expired = new List<int>();
            foreach (KeyValuePair<int, Lease> pair in entries)
                if (now >= pair.Value.Expires || !alive(pair.Key, pair.Value.Generation)) expired.Add(pair.Key);
            foreach (int pid in expired) entries.Remove(pid);
        }
    }

    internal static class AssistContract
    {
        internal const string ServiceName = "MPatcherLegacyZapret";
        internal const string PipeName = "MPatcher.LegacyZapret.v1";
        internal const string RegistryPath = @"SOFTWARE\MPatcher\LegacyZapret";
        internal const string GameHash = "4AF49BD45AD8274B0269552B89EA0068C3397208869C478580C9C2CF283105EE";
        // The kernel filter itself is restricted; unrelated UDP never enters winws.
        internal const string Filter = "ip and udp and ((outbound and ip.DstAddr == 173.230.144.203 and (udp.DstPort == 23466 or udp.DstPort == 50005)) or (inbound and ip.SrcAddr == 173.230.144.203 and (udp.SrcPort == 23466 or udp.SrcPort == 50005)))";
        internal const string Arguments = "--wf-raw=\"" + Filter + "\" --filter-udp=23466,50005 --ipset=\"ipset-machinecraft.txt\" --dpi-desync=fake --dpi-desync-repeats=12 --dpi-desync-any-protocol=1 --dpi-desync-fake-unknown-udp=\"quic_initial_dbankcloud_ru.bin\" --dpi-desync-cutoff=n2";
    }
}
