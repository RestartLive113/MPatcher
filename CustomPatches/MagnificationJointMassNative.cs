using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
	// Exact Unity 5.6.2 x86 profile. Verified PhysX calls use explicit cdecl-to-thiscall stubs.
	internal static class MagnificationJointMassNative
	{
		private sealed class ModuleIdentity { internal IntPtr BaseAddress; internal int ModuleMemorySize; }
		private static ModuleIdentity verifiedModule;
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
		private static extern IntPtr GetModuleHandle(string name);
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
		private static extern int GetModuleFileName(IntPtr module, StringBuilder name, int capacity);
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate void SetScale(IntPtr self, float value);
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate void GetActors(IntPtr self, out IntPtr first, out IntPtr second);
		private static readonly Dictionary<int, IntPtr> stubs = new Dictionary<int, IntPtr>();
		[DllImport("kernel32.dll", SetLastError = true)] private static extern IntPtr VirtualAlloc(IntPtr p, UIntPtr n, uint type, uint protect);
		[DllImport("kernel32.dll", SetLastError = true)] private static extern bool VirtualProtect(IntPtr p, UIntPtr n, uint protect, out uint oldProtect);
		[DllImport("kernel32.dll", SetLastError = true)] private static extern bool FlushInstructionCache(IntPtr process, IntPtr p, UIntPtr n);
		[DllImport("kernel32.dll", SetLastError = true)] private static extern bool VirtualFree(IntPtr p, UIntPtr n, uint type);
		private static IntPtr CdeclStub(IntPtr target, int argumentCount)
		{
			IntPtr stub;
			if (stubs.TryGetValue(target.ToInt32(), out stub)) return stub;
			// Explicit x86 cdecl(self, args...) -> thiscall(args...) bridge. Do not rely on old Mono's ThisCall marshaler.
			List<byte> code = new List<byte>(new byte[] { 0x55, 0x8b, 0xec, 0x8b, 0x4d, 0x08 });
			for (int i = argumentCount; i >= 1; i--) code.AddRange(new byte[] { 0xff, 0x75, (byte)(8 + i * 4) });
			code.Add(0xb8); code.AddRange(BitConverter.GetBytes(target.ToInt32()));
			code.AddRange(new byte[] { 0xff, 0xd0, 0x5d, 0xc3 });
			stub = VirtualAlloc(IntPtr.Zero, new UIntPtr(4096), 0x3000, 0x04);
			if (stub == IntPtr.Zero) throw new InvalidOperationException("stub-allocation-failed");
			Marshal.Copy(code.ToArray(), 0, stub, code.Count);
			uint oldProtect;
			if (!VirtualProtect(stub, new UIntPtr(4096), 0x20, out oldProtect) || !FlushInstructionCache(new IntPtr(-1), stub, new UIntPtr((uint)code.Count)))
			{
				VirtualFree(stub, UIntPtr.Zero, 0x8000);
				throw new InvalidOperationException("stub-protection-failed");
			}
			stubs.Add(target.ToInt32(), stub);
			return stub;
		}
		internal static void Release()
		{
			foreach (IntPtr stub in stubs.Values) VirtualFree(stub, UIntPtr.Zero, 0x8000);
			stubs.Clear();
		}
		private static ModuleIdentity VerifyModule()
		{
			if (verifiedModule != null) return verifiedModule;
			if (IntPtr.Size != 4) throw new InvalidOperationException("not-x86");
			ModuleIdentity module = new ModuleIdentity();
			module.BaseAddress = GetModuleHandle(null);
			StringBuilder fileName = new StringBuilder(32768);
			if (module.BaseAddress == IntPtr.Zero || GetModuleFileName(module.BaseAddress, fileName, fileName.Capacity) <= 0)
				throw new InvalidOperationException("module-identity-unavailable");
			string hash;
			using (SHA256 sha = SHA256.Create())
			using (FileStream input = File.OpenRead(fileName.ToString()))
				hash = BitConverter.ToString(sha.ComputeHash(input)).Replace("-", "");
			if (hash != "4AF49BD45AD8274B0269552B89EA0068C3397208869C478580C9C2CF283105EE")
				throw new InvalidOperationException("unknown-executable");
			int peOffset = Marshal.ReadInt32(module.BaseAddress, 0x3c);
			module.ModuleMemorySize = Marshal.ReadInt32(module.BaseAddress, peOffset + 0x50);
			verifiedModule = module;
			MagnificationDownscale.Log("JOINT_MASS_NATIVE_READY profile=Unity-5.6.2f1-x86 bridge=cdecl-to-thiscall sha256=" + hash + " exeFile=unchanged");
			return module;
		}
		private static Func<UnityEngine.Object, IntPtr> cachedPointer;
		private static IntPtr Native(UnityEngine.Object obj)
		{
			if (cachedPointer == null)
			{
				FieldInfo field = typeof(UnityEngine.Object).GetField("m_CachedPtr", BindingFlags.Instance | BindingFlags.NonPublic);
				if (field == null || field.FieldType != typeof(IntPtr)) throw new InvalidOperationException("cached-pointer-layout-mismatch");
				DynamicMethod getter = new DynamicMethod("MPatcherJointNativePointer", typeof(IntPtr), new[] { typeof(UnityEngine.Object) }, typeof(MagnificationJointMassNative), true);
				ILGenerator il = getter.GetILGenerator();
				il.Emit(OpCodes.Ldarg_0); il.Emit(OpCodes.Ldfld, field); il.Emit(OpCodes.Ret);
				cachedPointer = (Func<UnityEngine.Object, IntPtr>)getter.CreateDelegate(typeof(Func<UnityEngine.Object, IntPtr>));
			}
			return cachedPointer(obj);
		}
		internal static IntPtr JointIdentity(Joint joint)
		{
			VerifyModule();
			if (joint == null) return IntPtr.Zero;
			IntPtr native = Native(joint);
			return native == IntPtr.Zero ? IntPtr.Zero : Marshal.ReadIntPtr(native, 0x20);
		}
		private static IntPtr Function(IntPtr table, int slot, int expectedVA)
		{
			IntPtr pointer = Marshal.ReadIntPtr(table, slot);
			if (pointer.ToInt64() - VerifyModule().BaseAddress.ToInt64() + 0x400000 != expectedVA)
				throw new InvalidOperationException("unexpected-vtable-slot-" + slot.ToString("X"));
			return pointer;
		}
		internal static bool Balance(Joint joint, float ratio, bool inertia, bool quiet = false)
		{
			try
			{
				VerifyModule();
				Rigidbody own = joint.GetComponent<Rigidbody>(), other = joint.connectedBody;
				float ownMass = own.mass, otherMass = other.mass;
				Vector3 ownInertia = own.inertiaTensor, otherInertia = other.inertiaTensor;
				if (ratio < 1f || float.IsNaN(ratio) || float.IsInfinity(ratio) || ownMass <= 0f || otherMass <= 0f
					|| float.IsNaN(ownMass) || float.IsInfinity(ownMass) || float.IsNaN(otherMass) || float.IsInfinity(otherMass))
					throw new InvalidOperationException("invalid-mass-ratio");
				IntPtr px = Marshal.ReadIntPtr(Native(joint), 0x20);
				if (px == IntPtr.Zero) throw new InvalidOperationException("no-physx-joint");
				IntPtr table = Marshal.ReadIntPtr(px);
				if (table.ToInt64() - VerifyModule().BaseAddress.ToInt64() + 0x400000 != 0x0134ec30)
					throw new InvalidOperationException("unexpected-physx-joint-type");
				int[] slots = { 0x44, 0x4c, 0x54, 0x5c };
				int[] expected = { 0x00d99d70, 0x00d99d90, 0x00d93720, 0x00d93740 };
				SetScale[] setters = new SetScale[4];
				for (int i = 0; i < 4; i++) setters[i] = (SetScale)Marshal.GetDelegateForFunctionPointer(CdeclStub(Function(table, slots[i], expected[i]), 1), typeof(SetScale));
				GetActors get = (GetActors)Marshal.GetDelegateForFunctionPointer(CdeclStub(Function(table, 0x18, 0x00d9ead0), 2), typeof(GetActors));
				IntPtr actor0, actor1;
				get(px, out actor0, out actor1);
				IntPtr ownActor = Marshal.ReadIntPtr(Native(own), 0x34), otherActor = Marshal.ReadIntPtr(Native(other), 0x34);
				bool ownFirst = actor0 == ownActor && actor1 == otherActor;
				if (!ownFirst && !(actor1 == ownActor && actor0 == otherActor))
					throw new InvalidOperationException("actor-order-mismatch a0=" + actor0.ToInt64().ToString("X") + " a1=" + actor1.ToInt64().ToString("X")
						+ " own=" + ownActor.ToInt64().ToString("X") + " other=" + otherActor.ToInt64().ToString("X"));
				float floor = Mathf.Max(own.mass, other.mass) / ratio;
				float ownScale = own.mass / Mathf.Max(own.mass, floor), otherScale = other.mass / Mathf.Max(other.mass, floor);
				float first = ownFirst ? ownScale : otherScale, second = ownFirst ? otherScale : ownScale;
				setters[0](px, first); setters[1](px, inertia ? first : 1f);
				setters[2](px, second); setters[3](px, inertia ? second : 1f);
				IntPtr data = Marshal.ReadIntPtr(px, 0x50);
				float[] actual = new float[4];
				Marshal.Copy(new IntPtr(data.ToInt64() + 0x38), actual, 0, 4);
				if (actual[0] != first || actual[1] != (inertia ? first : 1f) || actual[2] != second || actual[3] != (inertia ? second : 1f))
					throw new InvalidOperationException("native-readback-mismatch");
				if (own.mass != ownMass || other.mass != otherMass || own.inertiaTensor != ownInertia || other.inertiaTensor != otherInertia)
					throw new InvalidOperationException("rigidbody-properties-changed");
				if (!quiet) MagnificationDownscale.Log("NATIVE_JOINT_BALANCED type=" + joint.GetType().Name + " ownFirst=" + ownFirst
					+ " ratio=" + ratio + " ownScale=" + ownScale + " otherScale=" + otherScale + " inertia=" + inertia + " bodyMass=unchanged readback=match");
				return true;
			}
			catch (Exception ex) { MagnificationDownscale.Log("NATIVE_JOINT_BALANCE_FAILED " + ex); return false; }
		}
		internal static void Inspect(Joint joint)
		{
			try
			{
				ModuleIdentity module = VerifyModule();
				FieldInfo cached = typeof(UnityEngine.Object).GetField("m_CachedPtr", BindingFlags.Instance | BindingFlags.NonPublic);
				IntPtr native = (IntPtr)cached.GetValue(joint);
				// Verified against this executable's Joint::set_enablePreprocessing (00620c90).
				IntPtr px = Marshal.ReadIntPtr(native, 0x20);
				if (px == IntPtr.Zero) throw new InvalidOperationException("no-physx-joint");
				IntPtr table = Marshal.ReadIntPtr(px);
				long start = module.BaseAddress.ToInt64(), end = start + module.ModuleMemorySize;
				if (table.ToInt64() < start || table.ToInt64() + 0x80 >= end)
					throw new InvalidOperationException("vtable-outside-executable");
				StringBuilder entries = new StringBuilder();
				for (int offset = 0; offset <= 0x70; offset += 4)
				{
					if (offset != 0) entries.Append(',');
					long address = Marshal.ReadIntPtr(table, offset).ToInt64();
					entries.Append(offset.ToString("X2")).Append(':').Append((address - start + 0x400000).ToString("X8"));
				}
				MagnificationDownscale.Log("NATIVE_JOINT_PROBE type=" + joint.GetType().Name + " tableVA="
					+ (table.ToInt64() - start + 0x400000).ToString("X8") + " entries=" + entries + " mode=read-only");
			}
			catch (Exception ex) { MagnificationDownscale.Log("NATIVE_JOINT_PROBE_FAILED " + ex); }
		}
	}
}
