using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
    internal static class LegacyMasterQueryFix
    {
        private const string ExpectedHash = "4AF49BD45AD8274B0269552B89EA0068C3397208869C478580C9C2CF283105EE";
        private static bool attempted;
        private static IntPtr block;
        private static int reportedReuse, reportedFailure;
        internal static bool Applied { get; private set; }

        internal static void TryRegister()
        {
            if (attempted) return;
            attempted = true;
            if (Environment.GetEnvironmentVariable("MPATCHER_LEGACY_MASTER_QUERY_FIX_DISABLE") == "1")
            { Log("DISABLED_BY_PROCESS_ENVIRONMENT"); return; }
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
                    if (hash != ExpectedHash || module.ModuleMemorySize <= 0x8d205b)
                        throw new NotSupportedException("Unknown Unity player profile: " + hash);
                    IntPtr initialize = Add(module.BaseAddress, 0x4615af);
                    IntPtr failure = Add(module.BaseAddress, 0x47307c);
                    IntPtr connect = Add(module.BaseAddress, 0x461606);
                    byte[] initializeBytes = { 0x6a,0,0x6a,0,0x8d,0x4d,0xd0 };
                    byte[] failureBytes = { 0xc6,0x43,0x2c,0,0xc6,0x43,0x2e,0 };
                    Ensure(initialize, initializeBytes);
                    Ensure(failure, failureBytes);
                    Ensure(connect, new byte[] { 0x8b,0x4e,0x1c,0x8b,0x11,0x8b,0x92,0x3c,0x01,0,0 });
                    Ensure(Add(module.BaseAddress, 0x8d2050), new byte[] { 0x8a,0x41,4,0x33,0xc9,0x84,0xc0,0x0f,0x94,0xc0,0xc3 });
                    block = VirtualAlloc(IntPtr.Zero, new UIntPtr(4096), 0x3000, 0x40);
                    if (block == IntPtr.Zero) throw Win32();
                    IntPtr initStub = Add(block, 32), failStub = Add(block, 128);
                    byte[] code = LegacyMasterQueryLayout.BuildInitializeStub(initStub, Add(initialize, 7), connect, block);
                    Marshal.Copy(code, 0, initStub, code.Length);
                    code = LegacyMasterQueryLayout.BuildFailureStub(failStub, Add(failure, 8), Add(block, 4));
                    Marshal.Copy(code, 0, failStub, code.Length);
                    Flush(process, block, 4096);
                    // Check every site before any write; roll both back if installation fails.
                    bool initTouched = false, failTouched = false;
                    try
                    {
                        initTouched = true;
                        Write(process, initialize, LegacyHostNativePatchLayout.JumpPatch(initialize, initStub, 7));
                        failTouched = true;
                        Write(process, failure, LegacyHostNativePatchLayout.JumpPatch(failure, failStub, 8));
                        Applied = true;
                    }
                    catch
                    {
                        bool restored = true;
                        try { if (failTouched) Write(process, failure, failureBytes); }
                        catch (Exception error) { restored = false; Log("ROLLBACK_FAILED site=failure message=" + error.Message); }
                        try { if (initTouched) Write(process, initialize, initializeBytes); }
                        catch (Exception error) { restored = false; Log("ROLLBACK_FAILED site=initialize message=" + error.Message); }
                        // A possible remaining branch must never target freed memory.
                        if (restored) { VirtualFree(block, UIntPtr.Zero, 0x8000); block = IntPtr.Zero; }
                        throw;
                    }
                }
                Log("APPLIED version=1 profile=Unity-5.6.2f1-x86 initializeRva=0x4615AF failureRva=0x47307C gameTransport=unchanged scope=current-process-only exeFile=unchanged");
                GameObject root = new GameObject("MPatcher.LegacyMasterQuery");
                UnityEngine.Object.DontDestroyOnLoad(root);
                root.AddComponent<LegacyMasterQueryLogDriver>();
            }
            catch (Exception error) { Log("REGISTER_FAILED applied=" + Applied + " type=" + error.GetType().Name + " message=" + error.Message); }
        }

        internal static void ReportChanges()
        {
            if (!Applied) return;
            Report(0, "QUERY_PEER_REUSED", ref reportedReuse);
            Report(4, "FAILED_QUERY_RELEASED", ref reportedFailure);
        }
        private static void Report(int offset, string label, ref int previous)
        {
            int count = Marshal.ReadInt32(block, offset);
            if (count == previous) return;
            Log(label + " total=" + count + " delta=" + unchecked(count - previous));
            previous = count;
        }
        private static IntPtr Add(IntPtr address, int offset) { return LegacyHostNativePatchLayout.Add(address, offset); }
        private static Exception Win32() { return new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error()); }
        private static void Ensure(IntPtr address, byte[] bytes)
        {
            for (int i = 0; i < bytes.Length; i++)
                if (Marshal.ReadByte(address, i) != bytes[i]) throw new InvalidOperationException("Master query signature mismatch at " + i);
        }
        private static void Write(Process process, IntPtr address, byte[] bytes)
        {
            uint protection;
            if (!VirtualProtect(address, new UIntPtr((uint)bytes.Length), 0x40, out protection)) throw Win32();
            try { Marshal.Copy(bytes, 0, address, bytes.Length); Flush(process, address, bytes.Length); Ensure(address, bytes); }
            finally
            {
                uint ignored;
                if (!VirtualProtect(address, new UIntPtr((uint)bytes.Length), protection, out ignored))
                    Log("PROTECTION_RESTORE_FAILED win32=" + Marshal.GetLastWin32Error());
            }
        }
        private static void Flush(Process process, IntPtr address, int count)
        {
            if (!FlushInstructionCache(process.Handle, address, new UIntPtr((uint)count))) throw Win32();
        }
        private static void Log(string value)
        {
            try { mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng("[LEGACY-MASTER-QUERY] " + value); }
            catch { }
        }
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr VirtualAlloc(IntPtr address, UIntPtr size, uint type, uint protection);
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool VirtualFree(IntPtr address, UIntPtr size, uint type);
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool VirtualProtect(IntPtr address, UIntPtr size, uint protection, out uint oldProtection);
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool FlushInstructionCache(IntPtr process, IntPtr address, UIntPtr size);
    }

    internal sealed class LegacyMasterQueryLogDriver : MonoBehaviour
    {
        private float next;
        private void Update()
        {
            if (Time.realtimeSinceStartup < next) return;
            next = Time.realtimeSinceStartup + 0.25f;
            LegacyMasterQueryFix.ReportChanges();
        }
        private void OnApplicationQuit() { LegacyMasterQueryFix.ReportChanges(); }
    }
}
