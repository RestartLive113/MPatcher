using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
    // Redirects only RakPeer.Initialize/Shutdown self-sends to their non-wildcard bind
    // address. Game packets and room endpoints are not redirected.
    internal static class LegacySocketSelfTestFix
    {
        private const string ExpectedHash = "4AF49BD45AD8274B0269552B89EA0068C3397208869C478580C9C2CF283105EE";
        private const int CallRva = 0x8d606a;
        private const uint ExecuteReadWrite = 0x40;
        private static bool attempted;
        private static IntPtr block;
        private static int reported;
        private static int reportedWake;
        internal static bool Applied { get; private set; }

        internal static void TryRegister()
        {
            if (attempted) return;
            attempted = true;
            if (Environment.GetEnvironmentVariable("MPATCHER_LEGACY_SOCKET_SELF_TEST_FIX_DISABLE") == "1")
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
                    if (hash != ExpectedHash || module.ModuleMemorySize <= CallRva + 13)
                        throw new NotSupportedException("Unknown Unity player profile: " + hash + " module=" + module.FileName + " size=" + module.ModuleMemorySize);
                    int[] sites = { CallRva, 0x8d64b3, 0x8d6623 };
                    byte[][] originals = new byte[sites.Length][];
                    for (int i = 0; i < sites.Length; i++)
                    {
                        IntPtr site = LegacyHostNativePatchLayout.Add(module.BaseAddress, sites[i]);
                        originals[i] = LegacyHostNativePatchLayout.JumpPatch(site, LegacyHostNativePatchLayout.Add(module.BaseAddress, 0x8c991c), 5);
                        originals[i][0] = 0xe8;
                        EnsureBytes(site, originals[i]);
                    }
                    EnsureBytes(LegacyHostNativePatchLayout.Add(module.BaseAddress, 0x8c98af),
                        new byte[] { 0x55,0x8b,0xec,0x51,0x51,0x56,0x8b,0xf1,0x83,0x3e,0,0x74,0x26,0xff,0x75,0x18 });
                    block = VirtualAlloc(IntPtr.Zero, new UIntPtr(4096), 0x3000, ExecuteReadWrite);
                    if (block == IntPtr.Zero) throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
                    Marshal.WriteInt32(block, 0);
                    Marshal.WriteInt32(block, 4, 0);
                    for (int i = 0; i < sites.Length; i++)
                    {
                        IntPtr stub = LegacyHostNativePatchLayout.Add(block, 32 + i * 64);
                        byte[] stubCode = LegacySocketSelfTestLayout.BuildStub(stub,
                            LegacyHostNativePatchLayout.Add(module.BaseAddress, 0x8c991c),
                            LegacyHostNativePatchLayout.Add(module.BaseAddress, 0x8c98af),
                            LegacyHostNativePatchLayout.Add(block, i == 0 ? 0 : 4), i != 0);
                        Marshal.Copy(stubCode, 0, stub, stubCode.Length);
                    }
                    Flush(process, block, 4096);
                    int installed = 0;
                    try
                    {
                        for (; installed < sites.Length; installed++)
                        {
                            IntPtr site = LegacyHostNativePatchLayout.Add(module.BaseAddress, sites[installed]);
                            byte[] call = LegacyHostNativePatchLayout.JumpPatch(site, LegacyHostNativePatchLayout.Add(block, 32 + installed * 64), 5);
                            call[0] = 0xe8;
                            WritePatch(process, site, originals[installed], call);
                        }
                    }
                    catch
                    {
                        for (int i = installed - 1; i >= 0; i--)
                        {
                            IntPtr site = LegacyHostNativePatchLayout.Add(module.BaseAddress, sites[i]);
                            byte[] call = LegacyHostNativePatchLayout.JumpPatch(site, LegacyHostNativePatchLayout.Add(block, 32 + i * 64), 5);
                            call[0] = 0xe8;
                            try { WritePatch(process, site, call, originals[i]); }
                            catch (Exception rollback) { Log("ROLLBACK_FAILED site=" + sites[i].ToString("X") + " type=" + rollback.GetType().Name); }
                        }
                        throw;
                    }
                    Applied = true;
                }
                Log("APPLIED version=2 profile=Unity-5.6.2f1-x86 calls=8D606A,8D64B3,8D6623 scope=Initialize+Shutdown-self-send-only wildcardBind=unchanged gamePackets=unchanged exeFile=unchanged");
                GameObject root = new GameObject("MPatcher.LegacySocketSelfTest");
                UnityEngine.Object.DontDestroyOnLoad(root);
                root.AddComponent<LegacySocketSelfTestLogDriver>();
            }
            catch (Exception error)
            {
                // Keep this process-lifetime block even if rollback itself failed.
                // A potentially referenced native stub must never be freed.
                Log("REGISTER_FAILED applied=" + Applied + " type=" + error.GetType().Name
                    + " message=" + error.Message.Replace("\r", " ").Replace("\n", " "));
            }
        }

        internal static void ReportChanges()
        {
            if (!Applied) return;
            int count = Marshal.ReadInt32(block);
            int wake = Marshal.ReadInt32(block, 4);
            if (count == reported && wake == reportedWake) return;
            Log("BOUND_ADDRESS_SELF_TEST total=" + count + " delta=" + unchecked(count - reported)
                + " wakeSends=" + wake + " wakeDelta=" + unchecked(wake - reportedWake) + " gamePackets=unchanged");
            reported = count;
            reportedWake = wake;
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
                    throw new InvalidOperationException("Socket self-test native signature mismatch at " + i);
        }
        private static void Log(string value)
        {
            try { mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng("[LEGACY-SOCKET-SELFTEST] " + value); }
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

    internal sealed class LegacySocketSelfTestLogDriver : MonoBehaviour
    {
        private float next;
        private void Update()
        {
            if (Time.realtimeSinceStartup < next) return;
            next = Time.realtimeSinceStartup + 0.25f;
            LegacySocketSelfTestFix.ReportChanges();
        }
        private void OnApplicationQuit() { LegacySocketSelfTestFix.ReportChanges(); }
    }
}
