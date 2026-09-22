using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
    // Unity 5.6.2f1 x86 only. Evidence: NetworkManager::Disconnect 0086E9A0,
    // RemoveRPCs 0086C270, InitializeServer 00870680, allocator reset 0086B940.
    // The buffered-RPC list owns both instantiate and buffered RPC messages.
    // Detach its nodes only during the synchronous restart. No code bytes change.
    internal sealed class LegacyHostMigrationNative : IDisposable
    {
        private const string ExeHash = "4AF49BD45AD8274B0269552B89EA0068C3397208869C478580C9C2CF283105EE";
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] private delegate IntPtr GetManager();
        private static GetManager getter;
        private readonly IntPtr manager, head, first, last;
        private readonly int rpcCount;
        private readonly VectorCopy[] allocator;
        private readonly byte[] scalars;
        private bool detached;
        internal int BufferedCount { get { return rpcCount; } }

        internal static void CheckProfile()
        {
            if (getter != null) return;
            if (IntPtr.Size != 4) throw new NotSupportedException("Migration requires x86 Unity");
            using (Process p = Process.GetCurrentProcess())
            {
                ProcessModule m = p.MainModule;
                string hash;
                using (FileStream f = File.OpenRead(m.FileName))
                using (SHA256 sha = SHA256.Create()) hash = BitConverter.ToString(sha.ComputeHash(f)).Replace("-", "");
                if (hash != ExeHash) throw new NotSupportedException("Unknown migration native profile " + hash);
                // Exact restart and list-access sites, also reject other in-memory hooks.
                Check(Add(m.BaseAddress, 0x470680), new byte[] { 0x55, 0x8B, 0xEC, 0x83, 0xEC, 0x4C });
                getter = (GetManager)Marshal.GetDelegateForFunctionPointer(Add(m.BaseAddress, 0x4603A0), typeof(GetManager));
            }
        }
        internal static bool UseNat()
        { CheckProfile(); return Marshal.ReadByte(getter(), 0x214) != 0; }
        internal static int RetireInstantiate(NetworkViewID id)
        {
            if (!Network.isServer || id == NetworkViewID.unassigned) return 0;
            CheckProfile();
            if (Marshal.SizeOf(typeof(NetworkViewID)) != 12) throw new NotSupportedException("NetworkViewID layout changed");
            IntPtr identity = Marshal.AllocHGlobal(12);
            int a, b, c;
            try
            {
                Marshal.StructureToPtr(id, identity, false);
                a = Marshal.ReadInt32(identity); b = Marshal.ReadInt32(identity, 4); c = Marshal.ReadInt32(identity, 8);
            }
            finally { Marshal.FreeHGlobal(identity); }
            IntPtr manager = getter(), head = Marshal.ReadIntPtr(manager, 0x190);
            int count = Marshal.ReadInt32(manager, 0x194);
            if (head == IntPtr.Zero || count < 0 || count > 1000000) throw new InvalidOperationException("Invalid retirement RPC list");
            var targets = new System.Collections.Generic.List<IntPtr>();
            IntPtr node = Marshal.ReadIntPtr(head), previous = head;
            for (int i = 0; i < count; i++)
            {
                if (node == head || node == IntPtr.Zero || Marshal.ReadIntPtr(node, 4) != previous)
                    throw new InvalidOperationException("Invalid retirement RPC links");
                IntPtr name = Marshal.ReadIntPtr(node, 8);
                if (name == IntPtr.Zero) name = Add(node, 12);
                if (Marshal.PtrToStringAnsi(name) == "__RPCNetworkInstantiate"
                    && Marshal.ReadInt32(node, 0x24) == 0 && Marshal.ReadInt32(node, 0x28) == 0 && Marshal.ReadInt32(node, 0x2c) == 0)
                {
                    IntPtr stream = Marshal.ReadIntPtr(node, 0x38);
                    if (stream == IntPtr.Zero) throw new InvalidOperationException("Instantiate payload absent");
                    int bits = Marshal.ReadInt32(stream);
                    if (bits < 0 || bits > 32768) throw new InvalidOperationException("Unexpected Instantiate payload size");
                    byte[] bytes = Read(Marshal.ReadIntPtr(stream, 12), (bits + 7) / 8);
                    if (LegacyInstantiatePacket.MatchesRoot(bytes, bits, a, b, c)) targets.Add(node);
                }
                previous = node; node = Marshal.ReadIntPtr(node);
            }
            if (node != head || Marshal.ReadIntPtr(head, 4) != previous) throw new InvalidOperationException("Retirement RPC count differs");
            // All validation precedes mutation. Give only the verified creation
            // nodes their real root ID; Unity then owns unlink/destruction/free.
            foreach (IntPtr target in targets)
            {
                Marshal.WriteInt32(target, 0x24, a); Marshal.WriteInt32(target, 0x28, b); Marshal.WriteInt32(target, 0x2c, c);
            }
            if (targets.Count != 0)
            {
                Network.RemoveRPCs(id);
                if (Marshal.ReadInt32(manager, 0x194) != count - targets.Count)
                    throw new InvalidOperationException("Instantiate retirement count mismatch");
                LegacyTransientReconnect.Log("SERVER_INSTANTIATE_BUFFER_RETIRED view=" + id + " nodes=" + targets.Count + " remaining=" + (count - targets.Count));
            }
            return targets.Count;
        }
        // Read-only, explicitly armed probe. Node layout verified against
        // 0086CC20 (insert) and 0086C270 (RemoveRPCs), exact executable profile.
        internal static string InspectBufferedRpc()
        {
            CheckProfile();
            IntPtr manager = getter(), head = Marshal.ReadIntPtr(manager, 0x190);
            int count = Marshal.ReadInt32(manager, 0x194);
            if (head == IntPtr.Zero || count < 0 || count > 1000000)
                throw new InvalidOperationException("Invalid diagnostic RPC list");
            System.Text.StringBuilder text = new System.Text.StringBuilder("count=" + count + "\n");
            IntPtr node = Marshal.ReadIntPtr(head), previous = head;
            for (int i = 0; i < count; i++)
            {
                if (node == head || node == IntPtr.Zero || Marshal.ReadIntPtr(node, 4) != previous)
                    throw new InvalidOperationException("Invalid diagnostic RPC links");
                IntPtr name = Marshal.ReadIntPtr(node, 8);
                if (name == IntPtr.Zero) name = Add(node, 12);
                text.Append(i).Append(" name=").Append(Marshal.PtrToStringAnsi(name))
                    .Append(" id=").Append(Marshal.ReadInt32(node, 0x24)).Append(',')
                    .Append(Marshal.ReadInt32(node, 0x28)).Append(',').Append(Marshal.ReadInt32(node, 0x2c))
                    .Append(" sender=").Append(Marshal.ReadInt32(node, 0x30))
                    .Append(" group=").Append(Marshal.ReadInt32(node, 0x34)).Append('\n');
                if (Marshal.PtrToStringAnsi(name) == "__RPCNetworkInstantiate")
                {
                    IntPtr stream = Marshal.ReadIntPtr(node, 0x38);
                    int bits = Marshal.ReadInt32(stream);
                    if (bits < 0 || bits > 32768) throw new InvalidOperationException("Unexpected Instantiate payload size");
                    text.Append("payload bits=").Append(bits).Append(" hex=")
                        .Append(BitConverter.ToString(Read(Marshal.ReadIntPtr(stream, 12), (bits + 7) / 8))).Append('\n');
                }
                previous = node; node = Marshal.ReadIntPtr(node);
            }
            if (node != head || Marshal.ReadIntPtr(head, 4) != previous)
                throw new InvalidOperationException("Diagnostic RPC count differs");
            return text.ToString();
        }
        internal LegacyHostMigrationNative()
        {
            CheckProfile(); manager = getter();
            if (manager == IntPtr.Zero) throw new InvalidOperationException("Network manager absent");
            head = Marshal.ReadIntPtr(manager, 0x190);
            rpcCount = Marshal.ReadInt32(manager, 0x194);
            if (head == IntPtr.Zero || rpcCount < 0 || rpcCount > 1000000)
                throw new InvalidOperationException("Invalid buffered RPC list");
            first = Marshal.ReadIntPtr(head); last = Marshal.ReadIntPtr(head, 4);
            if (Marshal.ReadIntPtr(first, 4) != head || Marshal.ReadIntPtr(last) != head)
                throw new InvalidOperationException("RPC list links invalid");
            allocator = new VectorCopy[] { new VectorCopy(Add(manager, 0x1C8), 4),
                new VectorCopy(Add(manager, 0x1D8), 4), new VectorCopy(Add(manager, 0x1E8), 8) };
            scalars = Read(Add(manager, 0x1F8), 20);
            // All allocations and validation precede the mutation; Dispose is synchronous.
            Marshal.WriteIntPtr(head, head); Marshal.WriteIntPtr(head, 4, head);
            Marshal.WriteInt32(manager, 0x194, 0); detached = true;
        }
        public void Dispose()
        {
            if (!detached) return;
            // InitializeServer only clears these POD vectors (capacity is retained).
            // Restore through the CURRENT pointers in case native startup grew one.
            if (Marshal.ReadIntPtr(manager, 0x190) != head || Marshal.ReadInt32(manager, 0x194) != 0)
                throw new InvalidOperationException("Unexpected RPC mutation during synchronous migration");
            Marshal.WriteIntPtr(head, first); Marshal.WriteIntPtr(head, 4, last);
            Marshal.WriteInt32(manager, 0x194, rpcCount); detached = false;
            foreach (VectorCopy vector in allocator) vector.Restore();
            Marshal.Copy(scalars, 0, Add(manager, 0x1F8), scalars.Length);
        }
        private sealed class VectorCopy
        {
            private readonly IntPtr header;
            private readonly byte[] bytes;
            internal VectorCopy(IntPtr address, int elementSize)
            {
                header = address;
                IntPtr begin = Marshal.ReadIntPtr(header), end = Marshal.ReadIntPtr(header, 4), cap = Marshal.ReadIntPtr(header, 8);
                long size = end.ToInt64() - begin.ToInt64(), capacity = cap.ToInt64() - begin.ToInt64();
                if (size < 0 || size > capacity || capacity > 16777216 || size % elementSize != 0)
                    throw new InvalidOperationException("Invalid network ID allocator vector");
                bytes = Read(begin, (int)size);
            }
            internal void Restore()
            {
                IntPtr begin = Marshal.ReadIntPtr(header), cap = Marshal.ReadIntPtr(header, 8);
                if (cap.ToInt64() - begin.ToInt64() < bytes.Length)
                    throw new InvalidOperationException("Network ID allocator unexpectedly shrank");
                if (bytes.Length > 0) Marshal.Copy(bytes, 0, begin, bytes.Length);
                Marshal.WriteIntPtr(header, 4, Add(begin, bytes.Length));
            }
        }
        private static IntPtr Add(IntPtr p, int offset) { return new IntPtr(p.ToInt64() + offset); }
        private static byte[] Read(IntPtr p, int length)
        { byte[] b = new byte[length]; if (length > 0) Marshal.Copy(p, b, 0, length); return b; }
        private static void Check(IntPtr p, byte[] expected)
        {
            byte[] actual = Read(p, expected.Length);
            for (int i = 0; i < actual.Length; i++) if (actual[i] != expected[i])
                throw new NotSupportedException("Migration native signature changed");
        }
    }
}
