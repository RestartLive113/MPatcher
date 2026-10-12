using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace MPatcherFork.CustomPatches
{
	// Captures the exact native Unity NetworkManager after a facilitator packet
	// has been identified for the Host role. Capturing the connected event also
	// permits independent native-state checks when the loss event was missed.
	// Unity's original immediate reconnect remains in place. The managed
	// host driver can then repeat that same RakPeer Connect call without invoking
	// Network.InitializeServer or replacing the live server session.
	internal static class LegacyHostFacilitatorReconnectFix
	{
		private const string ExpectedExeSha256 = "4AF49BD45AD8274B0269552B89EA0068C3397208869C478580C9C2CF283105EE";
		private const int HostFacilitatorConnectedRva = 0x470CF4;
		private const int HostFacilitatorLossRva = 0x47125A;
		private const int AddressToStringRva = 0x8C7560;
		private const int AddressOrGuidConstructorRva = 0x45FFB0;
		private const int IsConnectedRva = 0x8CF194;
		// GetManager(13), VA00580590, reads manager table VA014396B0 + 13*4.
		private const int NetworkManagerSlotRva = 0x10396E4;
		private const uint MemCommitReserve = 0x3000;
		private const uint MemRelease = 0x8000;
		private const uint PageExecuteReadWrite = 0x40;
		private static bool attempted;
		private static bool applied;
		private static IntPtr nativeBlock;
		private static IntPtr managerSlot;
		private static IntPtr pendingSlot;
		private static NativeReconnectDelegate reconnect;
		private static NativeConnectivityDelegate connectivity;
		private static IntPtr liveManagerSlot;
		private static IntPtr expectedIsConnected;

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate int NativeReconnectDelegate(IntPtr networkManager);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate int NativeConnectivityDelegate(IntPtr networkManager, int includeConnecting, int includeDisconnecting);

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
				if (hash != ExpectedExeSha256 || module.ModuleMemorySize <= NetworkManagerSlotRva + 4)
					throw new NotSupportedException("Unknown Unity player profile: " + hash);

				lossAddress = LegacyHostNativePatchLayout.Add(module.BaseAddress, HostFacilitatorLossRva);
				connectedAddress = LegacyHostNativePatchLayout.Add(module.BaseAddress, HostFacilitatorConnectedRva);
				EnsureEqual(Read(lossAddress, lossExpected.Length), lossExpected, "host facilitator loss signature mismatch");
				EnsureEqual(Read(connectedAddress, connectedExpected.Length), connectedExpected, "host facilitator connected signature mismatch");
				EnsureEqual(Read(LegacyHostNativePatchLayout.Add(module.BaseAddress, AddressOrGuidConstructorRva), 10),
					new byte[] { 0x55, 0x8B, 0xEC, 0x56, 0x8B, 0xF1, 0xB8, 0xFF, 0xFF, 0x00 }, "AddressOrGUID constructor signature mismatch");
				expectedIsConnected = LegacyHostNativePatchLayout.Add(module.BaseAddress, IsConnectedRva);
				EnsureEqual(Read(expectedIsConnected, 10), new byte[] { 0x55, 0x8B, 0xEC, 0x83, 0xEC, 0x1C, 0x80, 0x7D, 0x20, 0x00 },
					"RakPeer IsConnected signature mismatch");
				liveManagerSlot = LegacyHostNativePatchLayout.Add(module.BaseAddress, NetworkManagerSlotRva);

				nativeBlock = VirtualAlloc(IntPtr.Zero, new UIntPtr(4096), MemCommitReserve, PageExecuteReadWrite);
				if (nativeBlock == IntPtr.Zero) throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
				managerSlot = nativeBlock;
				pendingSlot = LegacyHostNativePatchLayout.Add(nativeBlock, 4);
				IntPtr lossStub = LegacyHostNativePatchLayout.Add(nativeBlock, 32);
				IntPtr connectedStub = LegacyHostNativePatchLayout.Add(nativeBlock, 96);
				IntPtr reconnectStub = LegacyHostNativePatchLayout.Add(nativeBlock, 160);
				IntPtr connectivityStub = LegacyHostNativePatchLayout.Add(nativeBlock, 256);
				Marshal.WriteIntPtr(managerSlot, IntPtr.Zero);
				Marshal.WriteInt32(pendingSlot, 0);

				WriteRaw(lossStub, LegacyHostNativePatchLayout.BuildLossStub(lossStub, lossAddress, managerSlot, pendingSlot));
				WriteRaw(connectedStub, LegacyHostNativePatchLayout.BuildConnectedStub(connectedStub, connectedAddress, managerSlot, pendingSlot));
				WriteRaw(reconnectStub, LegacyHostNativePatchLayout.BuildReconnectStub(
					LegacyHostNativePatchLayout.Add(module.BaseAddress, AddressToStringRva)));
				WriteRaw(connectivityStub, LegacyHostNativePatchLayout.BuildConnectivityStub(
					LegacyHostNativePatchLayout.Add(module.BaseAddress, AddressOrGuidConstructorRva)));
				if (!FlushInstructionCache(process.Handle, nativeBlock, new UIntPtr(4096)))
					throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());

				lossPatched = true;
				WritePatch(process, lossAddress, lossExpected, LegacyHostNativePatchLayout.JumpPatch(lossAddress, lossStub, lossExpected.Length));
				connectedPatched = true;
				WritePatch(process, connectedAddress, connectedExpected, LegacyHostNativePatchLayout.JumpPatch(connectedAddress, connectedStub, connectedExpected.Length));
				reconnect = (NativeReconnectDelegate)Marshal.GetDelegateForFunctionPointer(reconnectStub, typeof(NativeReconnectDelegate));
				connectivity = (NativeConnectivityDelegate)Marshal.GetDelegateForFunctionPointer(connectivityStub, typeof(NativeConnectivityDelegate));
				applied = true;
				Log("APPLIED version=2 profile=Unity-5.6.2f1-x86 lossRva=0x47125A connectedRva=0x470CF4 independentHealth=RakPeer-IsConnected healthInterval=5 healthMissingSamples=2 nativeFirstAttempt=unchanged managedRetry=8,15,30,max30 sessionReinitialize=never scope=current-process-only exeFile=unchanged sha256=" + hash);
			}
			catch (Exception error)
			{
				bool restored = true;
				if (process != null)
				{
					if (connectedPatched) restored &= TryRestore(process, connectedAddress, connectedExpected, "connected");
					if (lossPatched) restored &= TryRestore(process, lossAddress, lossExpected, "loss");
				}
				// A branch that could not be restored must never target freed memory.
				if (restored && nativeBlock != IntPtr.Zero) VirtualFree(nativeBlock, UIntPtr.Zero, MemRelease);
				nativeBlock = IntPtr.Zero;
				managerSlot = IntPtr.Zero;
				pendingSlot = IntPtr.Zero;
				reconnect = null;
				connectivity = null;
				liveManagerSlot = IntPtr.Zero;
				expectedIsConnected = IntPtr.Zero;
				applied = false;
				Log("FAILED type=" + error.GetType().Name + " rollbackVerified=" + restored + " stubRetained=" + !restored + " message=" + Clean(error.Message));
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
			return TryGetHostManager(out networkManager);
		}

		private static bool TryGetHostManager(out IntPtr networkManager)
		{
			networkManager = IntPtr.Zero;
			if (!applied || liveManagerSlot == IntPtr.Zero) return false;
			IntPtr observed = Marshal.ReadIntPtr(managerSlot);
			// Check the current Unity singleton before dereferencing an old capture.
			if (observed == IntPtr.Zero || Marshal.ReadIntPtr(liveManagerSlot) != observed) return false;
			if (Marshal.ReadInt32(observed, 0x28) != 1 || Marshal.ReadByte(observed, 0x214) == 0) return false;
			IntPtr peer = Marshal.ReadIntPtr(observed, 0x38);
			if (peer == IntPtr.Zero || Marshal.ReadByte(peer, 4) != 0) return false;
			IntPtr table = Marshal.ReadIntPtr(peer);
			if (table == IntPtr.Zero || Marshal.ReadIntPtr(table, 0x9C) != expectedIsConnected) return false;
			if (Marshal.ReadInt32(observed, 0x2F8) == 0 || Marshal.ReadInt16(observed, 0x2FC) == 0) return false;
			networkManager = observed;
			return true;
		}

		internal static LegacyHostFacilitatorHealth ReadHealth(out IntPtr networkManager)
		{
			if (!TryGetHostManager(out networkManager) || connectivity == null) return LegacyHostFacilitatorHealth.Unknown;
			if (connectivity(networkManager, 0, 0) != 0) return LegacyHostFacilitatorHealth.Connected;
			if (connectivity(networkManager, 1, 0) != 0) return LegacyHostFacilitatorHealth.Connecting;
			if (connectivity(networkManager, 0, 1) != 0) return LegacyHostFacilitatorHealth.Disconnecting;
			return LegacyHostFacilitatorHealth.Missing;
		}

		internal static bool MarkMissing(IntPtr networkManager)
		{
			IntPtr current;
			if (ReadHealth(out current) != LegacyHostFacilitatorHealth.Missing || current != networkManager) return false;
			Marshal.WriteInt32(pendingSlot, 1);
			return true;
		}

		internal static int TryReconnect(IntPtr networkManager)
		{
			if (!applied || reconnect == null || networkManager == IntPtr.Zero) return -1;
			if (Marshal.ReadInt32(pendingSlot) != 1 || Marshal.ReadIntPtr(managerSlot) != networkManager) return -1;
			IntPtr current;
			if (ReadHealth(out current) != LegacyHostFacilitatorHealth.Missing || current != networkManager) return -2;
			return reconnect(networkManager);
		}

		internal static void ClearPending()
		{
			if (!applied) return;
			Marshal.WriteInt32(pendingSlot, 0);
			// Preserve the live binding when auto-port hands this peer to stability.
		}

		internal static void ForgetHost()
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

		private static bool TryRestore(Process process, IntPtr address, byte[] original, string name)
		{
			try
			{
				uint oldProtection;
				if (!VirtualProtect(address, new UIntPtr((uint)original.Length), PageExecuteReadWrite, out oldProtection)) return false;
				try
				{
					Marshal.Copy(original, 0, address, original.Length);
					FlushInstructionCache(process.Handle, address, new UIntPtr((uint)original.Length));
					EnsureEqual(Read(address, original.Length), original, "rollback bytes differ");
					Log("ROLLED_BACK site=" + name);
				}
				finally
				{
					uint unused;
					VirtualProtect(address, new UIntPtr((uint)original.Length), oldProtection, out unused);
				}
				return true;
			}
			catch { return false; }
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
