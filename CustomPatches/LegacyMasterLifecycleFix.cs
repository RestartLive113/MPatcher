using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
    // Fixes Unity 5.6's cancelled registration surviving UnregisterHost, including
    // Disconnect/InitializeServer in the same process. No transport is replaced.
    internal static class LegacyMasterLifecycleFix
    {
        private const string ExpectedHash = "4AF49BD45AD8274B0269552B89EA0068C3397208869C478580C9C2CF283105EE";
        private const int CancelRva = 0x4641ff;
        private const uint ExecuteReadWrite = 0x40;
        private static bool attempted;
        private static IntPtr block;
        private static int reported;
        internal static bool Applied { get; private set; }

        internal static void TryRegister()
        {
            if (attempted) return;
            attempted = true;
            if (Environment.GetEnvironmentVariable("MPATCHER_LEGACY_MASTER_LIFECYCLE_FIX_DISABLE") == "1")
            {
                Log("DISABLED_BY_PROCESS_ENVIRONMENT");
                return;
            }
            try
            {
                if (IntPtr.Size != 4) throw new NotSupportedException("Requires x86 Unity player");
                using (Process process = Process.GetCurrentProcess())
                {
                    ProcessModule module = process.MainModule;
                    string hash;
                    using (FileStream file = new FileStream(module.FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    using (SHA256 sha = SHA256.Create())
                        hash = BitConverter.ToString(sha.ComputeHash(file)).Replace("-", "");
                    if (hash != ExpectedHash || module.ModuleMemorySize <= CancelRva + 13)
                        throw new NotSupportedException("Unknown Unity player profile: " + hash + " module=" + module.FileName + " size=" + module.ModuleMemorySize);
                    IntPtr site = LegacyHostNativePatchLayout.Add(module.BaseAddress, CancelRva);
                    byte[] signature = { 0xc6, 0x86, 0x98, 0, 0, 0, 0, 0x5e, 0x5b, 0x8b, 0xe5, 0x5d, 0xc3 };
                    EnsureBytes(site, signature);
                    block = VirtualAlloc(IntPtr.Zero, new UIntPtr(4096), 0x3000, ExecuteReadWrite);
                    if (block == IntPtr.Zero) throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
                    IntPtr stub = LegacyHostNativePatchLayout.Add(block, 32);
                    byte[] stubCode = LegacyMasterLifecycleLayout.BuildCancelStub(stub,
                        LegacyHostNativePatchLayout.Add(site, 7), block);
                    Marshal.WriteInt32(block, 0);
                    Marshal.Copy(stubCode, 0, stub, stubCode.Length);
                    Flush(process, block, 4096);
                    byte[] original = new byte[7];
                    Array.Copy(signature, original, 7);
                    WritePatch(process, site, original, LegacyHostNativePatchLayout.JumpPatch(site, stub, 7));
                    Applied = true;
                }
                Log("APPLIED version=1 profile=Unity-5.6.2f1-x86 cancelRva=0x4641FF cleared=registration+update queryPending=preserved transport=unchanged scope=current-process-only exeFile=unchanged");
                GameObject root = new GameObject("MPatcher.LegacyMasterLifecycle");
                UnityEngine.Object.DontDestroyOnLoad(root);
                root.AddComponent<LegacyMasterLifecycleLogDriver>();
            }
            catch (Exception error)
            {
                // Never release an executable stub still referenced by a successful hook.
                if (!Applied && block != IntPtr.Zero)
                {
                    VirtualFree(block, UIntPtr.Zero, 0x8000);
                    block = IntPtr.Zero;
                }
                Log("REGISTER_FAILED applied=" + Applied + " type=" + error.GetType().Name
                    + " message=" + error.Message.Replace("\r", " ").Replace("\n", " "));
            }
        }

        internal static void ReportChanges()
        {
            if (!Applied) return;
            int count = Marshal.ReadInt32(block);
            if (count == reported) return;
            Log("CANCELLED_PENDING total=" + count + " delta=" + unchecked(count - reported)
                + " queryPending=preserved transport=unchanged");
            reported = count;
        }

        private static void WritePatch(Process process, IntPtr site, byte[] original, byte[] replacement)
        {
            EnsureBytes(site, original);
            uint oldProtection;
            if (!VirtualProtect(site, new UIntPtr((uint)replacement.Length), ExecuteReadWrite, out oldProtection))
                throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
            try
            {
                try
                {
                    Marshal.Copy(replacement, 0, site, replacement.Length);
                    Flush(process, site, replacement.Length);
                    EnsureBytes(site, replacement);
                }
                catch
                {
                    Marshal.Copy(original, 0, site, original.Length);
                    Flush(process, site, original.Length);
                    throw;
                }
            }
            finally
            {
                uint unused;
                if (!VirtualProtect(site, new UIntPtr((uint)replacement.Length), oldProtection, out unused))
                    Log("PROTECTION_RESTORE_FAILED win32=" + Marshal.GetLastWin32Error());
            }
        }

        private static void Flush(Process process, IntPtr address, int length)
        {
            if (!FlushInstructionCache(process.Handle, address, new UIntPtr((uint)length)))
                throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
        }
        private static void EnsureBytes(IntPtr address, byte[] expected)
        {
            for (int i = 0; i < expected.Length; i++)
                if (Marshal.ReadByte(address, i) != expected[i])
                    throw new InvalidOperationException("Master cancellation native signature mismatch at " + i);
        }
        private static void Log(string value)
        {
            try { mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng("[LEGACY-MASTER-LIFECYCLE] " + value); }
            catch { }
        }
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr VirtualAlloc(IntPtr address, UIntPtr size, uint allocationType, uint protection);
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool VirtualFree(IntPtr address, UIntPtr size, uint freeType);
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool VirtualProtect(IntPtr address, UIntPtr size, uint protection, out uint oldProtection);
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool FlushInstructionCache(IntPtr process, IntPtr address, UIntPtr size);
    }

    internal sealed class LegacyMasterLifecycleLogDriver : MonoBehaviour
    {
        private float next;
        private void Update()
        {
            if (Time.realtimeSinceStartup < next) return;
            next = Time.realtimeSinceStartup + 0.25f;
            LegacyMasterLifecycleFix.ReportChanges();
        }
        private void OnApplicationQuit() { LegacyMasterLifecycleFix.ReportChanges(); }
    }
}
