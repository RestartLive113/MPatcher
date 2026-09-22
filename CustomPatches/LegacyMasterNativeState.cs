using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace MPatcherFork.CustomPatches
{
    // Exact Unity player only; accessor is called on Unity's main thread after initialization.
    internal static class LegacyMasterNativeState
    {
        private const string ExpectedHash = "4AF49BD45AD8274B0269552B89EA0068C3397208869C478580C9C2CF283105EE";
        private const int GetterRva = 0x460190;
        private static MasterGetter getter;
        private static IntPtr moduleBase;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr MasterGetter();

        internal static void Initialize()
        {
            if (getter != null) return;
            if (IntPtr.Size != 4) throw new NotSupportedException("Master state requires x86 Unity");
            using (Process process = Process.GetCurrentProcess())
            {
                ProcessModule module = process.MainModule;
                string hash;
                using (FileStream file = new FileStream(module.FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (SHA256 sha = SHA256.Create())
                    hash = BitConverter.ToString(sha.ComputeHash(file)).Replace("-", "");
                if (hash != ExpectedHash || module.ModuleMemorySize < GetterRva + 11)
                    throw new NotSupportedException("Unsupported master state profile: " + hash);
                IntPtr address = Add(module.BaseAddress, GetterRva);
                if (Marshal.ReadByte(address) != 0x6a || Marshal.ReadByte(address, 1) != 0x0e
                    || Marshal.ReadByte(address, 10) != 0xc3)
                    throw new NotSupportedException("Master getter signature differs");
                getter = (MasterGetter)Marshal.GetDelegateForFunctionPointer(address, typeof(MasterGetter));
                moduleBase = module.BaseAddress;
            }
        }

        internal static LegacyMasterStateSnapshot Read()
        {
            if (getter == null) throw new InvalidOperationException("Master accessor not initialized");
            IntPtr context = getter();
            if (context == IntPtr.Zero) throw new InvalidOperationException("Master context absent");
            IntPtr queryPeer = Marshal.ReadIntPtr(context, 0x1c);
            IntPtr table = queryPeer == IntPtr.Zero ? IntPtr.Zero : Marshal.ReadIntPtr(queryPeer);
            return new LegacyMasterStateSnapshot {
                Context = context,
                QueryPeer = queryPeer,
                QueryInitializeRva = table == IntPtr.Zero ? 0 : unchecked(Marshal.ReadIntPtr(table, 4).ToInt32() - moduleBase.ToInt32()),
                QueryActiveRva = table == IntPtr.Zero ? 0 : unchecked(Marshal.ReadIntPtr(table, 0x3c).ToInt32() - moduleBase.ToInt32()),
                RegistrationPending = Marshal.ReadByte(context, 0x2c),
                QueryPending = Marshal.ReadByte(context, 0x2d),
                UpdatePending = Marshal.ReadByte(context, 0x2e),
                TypeLength = Marshal.ReadInt32(context, 0x44),
                NameLength = Marshal.ReadInt32(context, 0x60),
                CommentLength = Marshal.ReadInt32(context, 0x7c),
                RowId = Marshal.ReadInt32(context, 0x94),
                SubmissionPending = Marshal.ReadByte(context, 0x98)
            };
        }

        private static IntPtr Add(IntPtr pointer, int offset)
        {
            return new IntPtr(unchecked(pointer.ToInt32() + offset));
        }
    }

    internal struct LegacyMasterStateSnapshot
    {
        internal IntPtr Context;
        internal IntPtr QueryPeer;
        internal int QueryInitializeRva, QueryActiveRva;
        internal int RegistrationPending, QueryPending, UpdatePending;
        internal int TypeLength, NameLength, CommentLength, RowId, SubmissionPending;
        public override string ToString()
        {
            return "context=0x" + Context.ToInt32().ToString("X8") + " registerPending=" + RegistrationPending
                + " queryPending=" + QueryPending + " updatePending=" + UpdatePending
                + " typeLength=" + TypeLength + " nameLength=" + NameLength + " commentLength=" + CommentLength
                + " row=" + RowId + " submissionPending=" + SubmissionPending;
        }
    }
}
