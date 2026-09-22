using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace MPatcherFork.CustomPatches
{
	// Unity 5.6.2f1's Windows SocketLayer::SendTo returns positive WSAGetLastError.
	// Its RakPeer MTU retry branch incorrectly compares that result with -10040.
	// Repair only that comparison; preserve the native 1492/1200/576 probe ladder.
	internal static class LegacyMtuProbeFix
	{
		private const string ExpectedExeSha256 = "4AF49BD45AD8274B0269552B89EA0068C3397208869C478580C9C2CF283105EE";
		private const int CompareRva = 0x8D4ABF;
		private static bool attempted;

		internal static void TryRegister()
		{
			if (attempted) return;
			attempted = true;
			if (Environment.GetEnvironmentVariable("MPATCHER_LEGACY_MTU_DISABLE") == "1")
			{
				Log("DISABLED_BY_PROCESS_ENVIRONMENT nativeCode=unchanged");
				return;
			}
			try
			{
				if (IntPtr.Size != 4) throw new NotSupportedException("Requires x86 Unity player");
				using (Process process = Process.GetCurrentProcess())
				{
					ProcessModule module = process.MainModule;
					string hash;
					using (FileStream stream = new FileStream(module.FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
					using (SHA256 sha = SHA256.Create())
						hash = BitConverter.ToString(sha.ComputeHash(stream)).Replace("-", "");
					if (hash != ExpectedExeSha256 || module.ModuleMemorySize <= CompareRva + 7)
						throw new NotSupportedException("Unknown Unity player profile: " + hash);
					IntPtr compare = new IntPtr(module.BaseAddress.ToInt64() + CompareRva);
					byte[] expected = { 0x3D, 0xC8, 0xD8, 0xFF, 0xFF, 0x75, 0x23 };
					byte[] actual = new byte[expected.Length];
					Marshal.Copy(compare, actual, 0, actual.Length);
					for (int i = 0; i < actual.Length; i++)
						if (actual[i] != expected[i]) throw new InvalidOperationException("MTU comparison signature mismatch");
					Log("REGISTERED version=1 profile=Unity-5.6.2f1-x86 sha256=" + hash);
					uint oldProtection;
					if (!VirtualProtect(compare, new UIntPtr((uint)expected.Length), 0x40, out oldProtection))
						throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
					try
					{
						Marshal.WriteInt32(new IntPtr(compare.ToInt64() + 1), 10040);
						if (!FlushInstructionCache(process.Handle, compare, new UIntPtr((uint)expected.Length)))
							throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
						if (Marshal.ReadInt32(new IntPtr(compare.ToInt64() + 1)) != 10040)
							throw new InvalidOperationException("MTU comparison verification failed");
					}
					catch
					{
						Marshal.WriteInt32(new IntPtr(compare.ToInt64() + 1), -10040);
						FlushInstructionCache(process.Handle, compare, new UIntPtr((uint)expected.Length));
						Log("ROLLED_BACK originalComparison=restored");
						throw;
					}
					finally
					{
						uint unused;
						if (!VirtualProtect(compare, new UIntPtr((uint)expected.Length), oldProtection, out unused))
							Log("PROTECTION_RESTORE_FAILED win32=" + Marshal.GetLastWin32Error());
					}
					Log("APPLIED version=1 rva=0x8D4ABF old=-10040 new=10040 probes=1492,1200,576 scope=current-process-only exeFile=unchanged");
				}
			}
			catch (Exception error) { Log("FAILED type=" + error.GetType().Name + " message=" + error.Message); }
		}

		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool VirtualProtect(IntPtr address, UIntPtr size, uint protection, out uint oldProtection);

		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool FlushInstructionCache(IntPtr process, IntPtr address, UIntPtr size);

		private static void Log(string message)
		{
			try { mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng("[LEGACY-MTU] " + message); }
			catch { }
		}
	}
}
