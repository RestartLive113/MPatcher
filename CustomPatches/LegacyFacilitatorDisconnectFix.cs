using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace MPatcherFork.CustomPatches
{
	// Unity keeps the NAT facilitator as an auxiliary RakNet connection after
	// punchthrough. In the client role, loss of that auxiliary connection falls
	// through to the normal game-server disconnect callback. Redirect only that
	// exact branch to the existing ignore epilogue.
	internal static class LegacyFacilitatorDisconnectFix
	{
		private const string ExpectedExeSha256 = "4AF49BD45AD8274B0269552B89EA0068C3397208869C478580C9C2CF283105EE";
		private const int ClientFacilitatorLossBranchRva = 0x471254;
		private static bool attempted;

		internal static void TryRegister()
		{
			if (attempted) return;
			attempted = true;
			if (Environment.GetEnvironmentVariable("MPATCHER_LEGACY_FACILITATOR_FIX_DISABLE") == "1")
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
					if (hash != ExpectedExeSha256 || module.ModuleMemorySize <= ClientFacilitatorLossBranchRva + 6)
						throw new NotSupportedException("Unknown Unity player profile: " + hash);

					IntPtr branch = new IntPtr(module.BaseAddress.ToInt64() + ClientFacilitatorLossBranchRva);
					byte[] expected = { 0x0F, 0x85, 0xB6, 0x00, 0x00, 0x00 }; // jne 0x871310
					byte[] patched = { 0x0F, 0x85, 0xF8, 0x08, 0x00, 0x00 };  // jne 0x871B52
					byte[] actual = new byte[expected.Length];
					Marshal.Copy(branch, actual, 0, actual.Length);
					EnsureEqual(actual, expected, "facilitator disconnect signature mismatch");
					Log("REGISTERED version=1 profile=Unity-5.6.2f1-x86 sha256=" + hash);

					uint oldProtection;
					if (!VirtualProtect(branch, new UIntPtr((uint)expected.Length), 0x40, out oldProtection))
						throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
					bool wrote = false;
					try
					{
						Marshal.Copy(patched, 0, branch, patched.Length);
						wrote = true;
						if (!FlushInstructionCache(process.Handle, branch, new UIntPtr((uint)patched.Length)))
							throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
						Marshal.Copy(branch, actual, 0, actual.Length);
						EnsureEqual(actual, patched, "facilitator disconnect patch verification failed");
					}
					catch
					{
						if (wrote)
						{
							Marshal.Copy(expected, 0, branch, expected.Length);
							FlushInstructionCache(process.Handle, branch, new UIntPtr((uint)expected.Length));
							Log("ROLLED_BACK originalBranch=restored");
						}
						throw;
					}
					finally
					{
						uint unused;
						if (!VirtualProtect(branch, new UIntPtr((uint)expected.Length), oldProtection, out unused))
							Log("PROTECTION_RESTORE_FAILED win32=" + Marshal.GetLastWin32Error());
					}
					Log("APPLIED version=1 rva=0x471254 events=DisconnectionNotification,ConnectionLost address=nat-facilitator role=client action=ignore-auxiliary-loss gameServerDisconnect=unchanged hostReconnect=unchanged scope=current-process-only exeFile=unchanged");
				}
			}
			catch (Exception error) { Log("FAILED type=" + error.GetType().Name + " message=" + error.Message); }
		}

		private static void EnsureEqual(byte[] actual, byte[] expected, string message)
		{
			if (actual.Length != expected.Length) throw new InvalidOperationException(message);
			for (int i = 0; i < actual.Length; i++)
				if (actual[i] != expected[i]) throw new InvalidOperationException(message);
		}

		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool VirtualProtect(IntPtr address, UIntPtr size, uint protection, out uint oldProtection);

		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool FlushInstructionCache(IntPtr process, IntPtr address, UIntPtr size);

		private static void Log(string message)
		{
			try { mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng("[LEGACY-FACILITATOR] " + message); }
			catch { }
		}
	}
}
