using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace MPatcherFork.CustomPatches
{
	// Captures the exact native Unity NetworkManager only after the packet source
	// was identified as the NAT facilitator and the peer role was identified as
	// Host. Unity's original immediate reconnect remains in place. The managed
	// host driver can then repeat that same RakPeer Connect call without invoking
	// Network.InitializeServer or replacing the live server session.
	internal static class LegacyHostFacilitatorReconnectFix
	{
		private const string ExpectedExeSha256 = "4AF49BD45AD8274B0269552B89EA0068C3397208869C478580C9C2CF283105EE";
		private const int HostFacilitatorConnectedRva = 0x470CF4;
		private const int HostFacilitatorLossRva = 0x47125A;
		private const int AddressToStringRva = 0x8C7560;
		private const uint MemCommitReserve = 0x3000;
		private const uint MemRelease = 0x8000;
		private const uint PageExecuteReadWrite = 0x40;
		private static bool attempted;
		private static bool applied;
		private static IntPtr nativeBlock;
		private static IntPtr managerSlot;
		private static IntPtr pendingSlot;
		private static NativeReconnectDelegate reconnect;

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate int NativeReconnectDelegate(IntPtr networkManager);

		internal static bool Applied
		{
			get { return applied; }
		}

		internal static void TryRegister()
		{
			if (attempted) return;
			attempted = true;
			if (Environment.GetEnvironmentVariable("MPATCHER_LEGACY_HOST_FACILITATOR_RETRY_DISABLE") == "1")
			{
				Log("DISABLED_BY_PROCESS_ENVIRONMENT nativeCode=unchanged");
				return;
			}
			Process process = null;
			IntPtr lossAddress = IntPtr.Zero;
			IntPtr connectedAddress = IntPtr.Zero;
			byte[] lossExpected = { 0x0F, 0xB7, 0x86, 0xFC, 0x02, 0x00, 0x00 };
			byte[] connectedExpected = { 0x8B, 0x4E, 0x3C, 0x6A, 0x01 };
			bool lossPatched = false;
			bool connectedPatched = false;
			try
			{
				if (IntPtr.Size != 4) throw new NotSupportedException("Requires x86 Unity player");
				process = Process.GetCurrentProcess();
				ProcessModule module = process.MainModule;
				string hash;
				using (FileStream stream = new FileStream(module.FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
				using (SHA256 sha = SHA256.Create())
					hash = BitConverter.ToString(sha.ComputeHash(stream)).Replace("-", "");
				if (hash != ExpectedExeSha256 || module.ModuleMemorySize <= AddressToStringRva + 16)
					throw new NotSupportedException("Unknown Unity player profile: " + hash);

				lossAddress = LegacyHostNativePatchLayout.Add(module.BaseAddress, HostFacilitatorLossRva);
				connectedAddress = LegacyHostNativePatchLayout.Add(module.BaseAddress, HostFacilitatorConnectedRva);
				EnsureEqual(Read(lossAddress, lossExpected.Length), lossExpected, "host facilitator loss signature mismatch");
				EnsureEqual(Read(connectedAddress, connectedExpected.Length), connectedExpected, "host facilitator connected signature mismatch");

				nativeBlock = VirtualAlloc(IntPtr.Zero, new UIntPtr(4096), MemCommitReserve, PageExecuteReadWrite);
				if (nativeBlock == IntPtr.Zero) throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
				managerSlot = nativeBlock;
				pendingSlot = LegacyHostNativePatchLayout.Add(nativeBlock, 4);
				IntPtr lossStub = LegacyHostNativePatchLayout.Add(nativeBlock, 32);
				IntPtr connectedStub = LegacyHostNativePatchLayout.Add(nativeBlock, 96);
				IntPtr reconnectStub = LegacyHostNativePatchLayout.Add(nativeBlock, 160);
				Marshal.WriteIntPtr(managerSlot, IntPtr.Zero);
				Marshal.WriteInt32(pendingSlot, 0);

				WriteRaw(lossStub, LegacyHostNativePatchLayout.BuildLossStub(lossStub, lossAddress, managerSlot, pendingSlot));
				WriteRaw(connectedStub, LegacyHostNativePatchLayout.BuildConnectedStub(connectedStub, connectedAddress, pendingSlot));
				WriteRaw(reconnectStub, LegacyHostNativePatchLayout.BuildReconnectStub(
					LegacyHostNativePatchLayout.Add(module.BaseAddress, AddressToStringRva)));
				if (!FlushInstructionCache(process.Handle, nativeBlock, new UIntPtr(4096)))
					throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());

				WritePatch(process, lossAddress, lossExpected, LegacyHostNativePatchLayout.JumpPatch(lossAddress, lossStub, lossExpected.Length));
				lossPatched = true;
				WritePatch(process, connectedAddress, connectedExpected, LegacyHostNativePatchLayout.JumpPatch(connectedAddress, connectedStub, connectedExpected.Length));
				connectedPatched = true;
				reconnect = (NativeReconnectDelegate)Marshal.GetDelegateForFunctionPointer(reconnectStub, typeof(NativeReconnectDelegate));
				applied = true;
				Log("APPLIED version=1 profile=Unity-5.6.2f1-x86 lossRva=0x47125A connectedRva=0x470CF4 nativeFirstAttempt=unchanged managedRetry=8,15,30,max30 sessionReinitialize=never scope=current-process-only exeFile=unchanged sha256=" + hash);
			}
			catch (Exception error)
			{
				if (process != null)
				{
					if (connectedPatched) TryRestore(process, connectedAddress, connectedExpected, "connected");
					if (lossPatched) TryRestore(process, lossAddress, lossExpected, "loss");
				}
				if (nativeBlock != IntPtr.Zero) VirtualFree(nativeBlock, UIntPtr.Zero, MemRelease);
				nativeBlock = IntPtr.Zero;
				managerSlot = IntPtr.Zero;
				pendingSlot = IntPtr.Zero;
				reconnect = null;
				applied = false;
				Log("FAILED type=" + error.GetType().Name + " message=" + Clean(error.Message));
			}
			finally
			{
				if (process != null) process.Dispose();
			}
		}

		internal static bool TryGetPending(out IntPtr networkManager)
		{
			networkManager = IntPtr.Zero;
			if (!applied) return false;
			if (Marshal.ReadInt32(pendingSlot) != 1) return false;
			networkManager = Marshal.ReadIntPtr(managerSlot);
			return networkManager != IntPtr.Zero;
		}

		internal static int TryReconnect(IntPtr networkManager)
		{
			if (!applied || reconnect == null || networkManager == IntPtr.Zero) return -1;
			if (Marshal.ReadInt32(pendingSlot) != 1 || Marshal.ReadIntPtr(managerSlot) != networkManager) return -1;
			return reconnect(networkManager);
		}

		internal static void ClearPending()
		{
			if (!applied) return;
			Marshal.WriteInt32(pendingSlot, 0);
			Marshal.WriteIntPtr(managerSlot, IntPtr.Zero);
		}

		private static byte[] Read(IntPtr address, int length)
		{
			byte[] value = new byte[length];
			Marshal.Copy(address, value, 0, value.Length);
			return value;
		}

		private static void WriteRaw(IntPtr address, byte[] value)
		{
			Marshal.Copy(value, 0, address, value.Length);
		}

		private static void WritePatch(Process process, IntPtr address, byte[] expected, byte[] replacement)
		{
			EnsureEqual(Read(address, expected.Length), expected, "native patch precondition changed");
			uint oldProtection;
			if (!VirtualProtect(address, new UIntPtr((uint)replacement.Length), PageExecuteReadWrite, out oldProtection))
				throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
			bool wrote = false;
			try
			{
				Marshal.Copy(replacement, 0, address, replacement.Length);
				wrote = true;
				if (!FlushInstructionCache(process.Handle, address, new UIntPtr((uint)replacement.Length)))
					throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
				EnsureEqual(Read(address, replacement.Length), replacement, "native patch verification failed");
			}
			catch
			{
				if (wrote)
				{
					Marshal.Copy(expected, 0, address, expected.Length);
					FlushInstructionCache(process.Handle, address, new UIntPtr((uint)expected.Length));
				}
				throw;
			}
			finally
			{
				uint unused;
				if (!VirtualProtect(address, new UIntPtr((uint)replacement.Length), oldProtection, out unused))
					Log("PROTECTION_RESTORE_FAILED win32=" + Marshal.GetLastWin32Error());
			}
		}

		private static void TryRestore(Process process, IntPtr address, byte[] original, string name)
		{
			try
			{
				uint oldProtection;
				if (!VirtualProtect(address, new UIntPtr((uint)original.Length), PageExecuteReadWrite, out oldProtection)) return;
				try
				{
					Marshal.Copy(original, 0, address, original.Length);
					FlushInstructionCache(process.Handle, address, new UIntPtr((uint)original.Length));
					Log("ROLLED_BACK site=" + name);
				}
				finally
				{
					uint unused;
					VirtualProtect(address, new UIntPtr((uint)original.Length), oldProtection, out unused);
				}
			}
			catch { }
		}

		private static void EnsureEqual(byte[] actual, byte[] expected, string message)
		{
			if (actual.Length != expected.Length) throw new InvalidOperationException(message);
			for (int index = 0; index < actual.Length; index++)
				if (actual[index] != expected[index]) throw new InvalidOperationException(message);
		}

		private static string Clean(string value)
		{
			return (value ?? "").Replace("\r", " ").Replace("\n", " ");
		}

		private static void Log(string message)
		{
			try { mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng("[LEGACY-HOST-FACILITATOR] " + message); }
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
}
