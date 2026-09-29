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
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate void SetTensor(IntPtr self, ref Vector3 value);
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate void UpdateMassProperties(IntPtr self);
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate void AddActorForce(IntPtr self, ref Vector3 value, int mode, int autoWake);
		[StructLayout(LayoutKind.Explicit)]
		private struct FloatBits
		{
			[FieldOffset(0)] internal int Bits;
			[FieldOffset(0)] internal float Value;
		}
		private static SetTensor setTensor;
		private static bool loggedTensor;
		private static SetScale setMass;
		private static UpdateMassProperties updateMassProperties;
		private static bool loggedSmallMass;
		private static AddActorForce addActorForce;
		private static bool loggedGravityForce;
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
			setTensor = null;
			setMass = null;
			updateMassProperties = null;
			addActorForce = null;
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
		internal static void InspectInertia(Rigidbody body)
		{
			ModuleIdentity module = VerifyModule();
			IntPtr actor = Marshal.ReadIntPtr(Native(body), 0x34), table = Marshal.ReadIntPtr(actor);
			IntPtr setter = Marshal.ReadIntPtr(table, 0x88);
			byte[] bytes = new byte[32]; Marshal.Copy(setter, bytes, 0, bytes.Length);
			MagnificationDownscale.Log("INERTIA_NATIVE_PROFILE tableVA=" + (table.ToInt64() - module.BaseAddress.ToInt64() + 0x400000).ToString("X8")
				+ " setterVA=" + (setter.ToInt64() - module.BaseAddress.ToInt64() + 0x400000).ToString("X8") + " bytes=" + BitConverter.ToString(bytes));
		}
		internal static void WriteSmallInertia(Rigidbody body, Vector3 value)
		{
			if (!(value.x > 0f && value.y > 0f && value.z > 0f)
				|| float.IsInfinity(value.x + value.y + value.z) || float.IsInfinity(1f / value.x + 1f / value.y + 1f / value.z))
				throw new ArgumentOutOfRangeException("value", "positive finite tensor and inverse required");
			ModuleIdentity module = VerifyModule();
			IntPtr native = Native(body);
			if (native == IntPtr.Zero) throw new InvalidOperationException("no-native-rigidbody");
			IntPtr actor = Marshal.ReadIntPtr(native, 0x34);
			if (actor == IntPtr.Zero) throw new InvalidOperationException("no-physx-actor");
			IntPtr table = Marshal.ReadIntPtr(actor);
			if (table.ToInt64() - module.BaseAddress.ToInt64() + 0x400000 != 0x01355d58)
				throw new InvalidOperationException("unexpected-inertia-actor-type");
			IntPtr function = Function(table, 0x88, 0x00dd0f70);
			if (setTensor == null)
			{
				byte[] expected = { 0x55, 0x8b, 0xec, 0x8b, 0x55, 0x08, 0xf3, 0x0f, 0x10, 0x4a, 0x08, 0x0f, 0x57, 0xd2 };
				for (int i = 0; i < expected.Length; i++) if (Marshal.ReadByte(function, i) != expected[i]) throw new InvalidOperationException("inertia-setter-code-mismatch");
				setTensor = (SetTensor)Marshal.GetDelegateForFunctionPointer(CdeclStub(function, 1), typeof(SetTensor));
			}
			// Unity 00635a00 does exactly these two operations, but rejects values
			// <= float epsilon. Keep its manual-tensor flag and call the same PhysX
			// setter, whose checked assembly accepts nonzero finite components.
			byte implicitTensor = Marshal.ReadByte(native, 0x51);
			if (implicitTensor > 1) throw new InvalidOperationException("inertia-automatic-flag-layout");
			Marshal.WriteByte(native, 0x51, 0);
			setTensor(actor, ref value);
			if (!MagnificationInertiaBoundary.RelativeEqual(body.inertiaTensor, value))
				throw new InvalidOperationException("native-inertia-readback requested=" + value.ToString("R") + " actual=" + body.inertiaTensor.ToString("R"));
			if (!loggedTensor)
			{
				loggedTensor = true;
				MagnificationDownscale.Log("INERTIA_SMALL_NATIVE_ACTIVE profile=Unity-5.6.2f1-x86 setterVA=00DD0F70 positivity=checked units=unchanged readback=match exeFile=unchanged");
			}
		}
		internal static void WriteSmallMass(Rigidbody body, float value)
		{
			// Unity's cache clamps mass to 1e-7, but PhysX accepts smaller positive
			// masses. Reproduce Unity's original auto/manual update path with the
			// requested cache value. This is intentionally not a general mass setter.
			if (!(value > 0f && value < 1e-7f) || float.IsInfinity(1f / value))
				throw new ArgumentOutOfRangeException("value", "mass below Unity floor and finite positive inverse required");
			ModuleIdentity module = VerifyModule();
			if (body == null) throw new ArgumentNullException("body");
			IntPtr native = Native(body);
			if (native == IntPtr.Zero) throw new InvalidOperationException("no-native-rigidbody");
			IntPtr actor = Marshal.ReadIntPtr(native, 0x34);
			if (actor == IntPtr.Zero) throw new InvalidOperationException("no-physx-actor");
			IntPtr table = Marshal.ReadIntPtr(actor);
			if (table.ToInt64() - module.BaseAddress.ToInt64() + 0x400000 != 0x01355d58)
				throw new InvalidOperationException("unexpected-mass-actor-type");
			IntPtr massSetter = Function(table, 0x7c, 0x00dd0ec0);
			Function(table, 0x80, 0x00dd0ef0); // Rigidbody.mass returns the true PhysX mass.
			byte automaticTensor = Marshal.ReadByte(native, 0x51);
			byte automaticCenter = Marshal.ReadByte(native, 0x52);
			byte deferredMass = Marshal.ReadByte(native, 0x54);
			if (automaticTensor > 1 || automaticCenter > 1 || deferredMass > 1)
				throw new InvalidOperationException("mass-property-flag-layout");
			// The optional linked-object callback at +0x38 has not been profiled.
			// Fail before any write rather than leave that consumer inconsistent.
			if (Marshal.ReadIntPtr(native, 0x38) != IntPtr.Zero)
				throw new InvalidOperationException("small-mass-linked-object-unsupported");
			if (setMass == null || updateMassProperties == null)
			{
				byte[] setterPrefix = { 0x55, 0x8b, 0xec, 0xf3, 0x0f, 0x10, 0x4d, 0x08, 0x0f, 0x57, 0xc0, 0x0f, 0x2f, 0xc8, 0x76, 0x0c };
				for (int i = 0; i < setterPrefix.Length; i++)
					if (Marshal.ReadByte(massSetter, i) != setterPrefix[i]) throw new InvalidOperationException("mass-setter-code-mismatch");
				IntPtr updater = new IntPtr(module.BaseAddress.ToInt64() + 0x00640f30 - 0x400000);
				byte[] updaterPrefix = { 0x55, 0x8b, 0xec, 0x83, 0xec, 0x54, 0x53, 0x56, 0x8b, 0xf1, 0x80, 0x7e, 0x54, 0x00, 0x57, 0x74, 0x1f };
				for (int i = 0; i < updaterPrefix.Length; i++)
					if (Marshal.ReadByte(updater, i) != updaterPrefix[i]) throw new InvalidOperationException("mass-properties-updater-code-mismatch");
				setMass = (SetScale)Marshal.GetDelegateForFunctionPointer(CdeclStub(massSetter, 1), typeof(SetScale));
				updateMassProperties = (UpdateMassProperties)Marshal.GetDelegateForFunctionPointer(CdeclStub(updater, 0), typeof(UpdateMassProperties));
			}
			// 00641210 writes native+44, then calls 00640F30 if auto-inertia is on;
			// otherwise it calls actor vtable+7C. Preserve the inertia/COM mode flags.
			Marshal.WriteInt32(native, 0x44, BitConverter.ToInt32(BitConverter.GetBytes(value), 0));
			if (automaticTensor != 0) updateMassProperties(native);
			else setMass(actor, value);
			float actual = body.mass;
			if (!(actual > 0f) || float.IsInfinity(actual) || Math.Abs((double)actual - value) > (double)value * 1e-5)
				throw new InvalidOperationException("native-mass-readback requested=" + value.ToString("R") + " actual=" + actual.ToString("R"));
			if (Marshal.ReadInt32(native, 0x44) != BitConverter.ToInt32(BitConverter.GetBytes(value), 0)
				|| Marshal.ReadByte(native, 0x51) != automaticTensor || Marshal.ReadByte(native, 0x52) != automaticCenter
				|| Marshal.ReadByte(native, 0x54) != deferredMass)
				throw new InvalidOperationException("native-mass-cache-or-flags-readback");
			if (!loggedSmallMass)
			{
				loggedSmallMass = true;
				MagnificationDownscale.Log("MASS_SMALL_NATIVE_ACTIVE profile=Unity-5.6.2f1-x86 setterVA=00DD0EC0 autoUpdaterVA=00640F30 cacheOffset=44 positivity=checked inverse=finite units=unchanged readback=physx-match flags=preserved exeFile=unchanged");
			}
		}
		internal static void AddAccelerationWithoutWake(Rigidbody body, Vector3 acceleration)
		{
			// Internal gravity does not reset an awake body's sleep timer. Unity's
			// public AddForce always requests autowake, so use the same PhysX entry
			// with only that flag changed. Call on Unity's physics/main thread.
			if (body == null || body.isKinematic || body.IsSleeping()) return;
			if (acceleration.x == 0f && acceleration.y == 0f && acceleration.z == 0f) return;
			if (float.IsNaN(acceleration.x) || float.IsNaN(acceleration.y) || float.IsNaN(acceleration.z)
				|| float.IsInfinity(acceleration.x) || float.IsInfinity(acceleration.y) || float.IsInfinity(acceleration.z))
				throw new ArgumentOutOfRangeException("acceleration", "finite acceleration required");
			ModuleIdentity module = VerifyModule();
			IntPtr native = Native(body);
			if (native == IntPtr.Zero) throw new InvalidOperationException("no-native-rigidbody");
			// Unity00637F60 rejects bodies outside active simulation before calling
			// PhysX. Keep that gate: the PhysX force entry requires a valid scene.
			byte activeSimulation = Marshal.ReadByte(native, 0x50);
			if (activeSimulation == 0) return;
			if (activeSimulation != 1) throw new InvalidOperationException("gravity-active-flag-layout");
			IntPtr actor = Marshal.ReadIntPtr(native, 0x34);
			if (actor == IntPtr.Zero) throw new InvalidOperationException("no-physx-actor");
			IntPtr table = Marshal.ReadIntPtr(actor);
			if (table.ToInt64() - module.BaseAddress.ToInt64() + 0x400000 != 0x01355d58)
				throw new InvalidOperationException("unexpected-gravity-actor-type");
			IntPtr function = Function(table, 0xa4, 0x00dd25d0);
			if (addActorForce == null)
			{
				byte[] expected = { 0x55, 0x8b, 0xec, 0x8b, 0x45, 0x0c, 0x53, 0x56, 0x57, 0x8b, 0x7d, 0x08, 0x50, 0x6a, 0x00, 0x57, 0x8b, 0xf1 };
				for (int i = 0; i < expected.Length; i++)
					if (Marshal.ReadByte(function, i) != expected[i]) throw new InvalidOperationException("gravity-force-code-mismatch");
				addActorForce = (AddActorForce)Marshal.GetDelegateForFunctionPointer(CdeclStub(function, 3), typeof(AddActorForce));
			}
			// Verified Unity00635E10: Acceleration(5) multiplies each component by
			// the Unity mass cache, then invokes PhysX eFORCE(0). FloatBits avoids
			// allocating BitConverter arrays for every body on every physics step.
			float mass = new FloatBits { Bits = Marshal.ReadInt32(native, 0x44) }.Value;
			if (!(mass > 0f) || float.IsInfinity(mass)) throw new InvalidOperationException("invalid-gravity-mass-cache");
			Vector3 force = new Vector3((float)((double)acceleration.x * mass), (float)((double)acceleration.y * mass), (float)((double)acceleration.z * mass));
			if (float.IsInfinity(force.x) || float.IsInfinity(force.y) || float.IsInfinity(force.z))
				throw new ArgumentOutOfRangeException("acceleration", "finite converted force required");
			addActorForce(actor, ref force, 0, 0);
			if (!loggedGravityForce)
			{
				loggedGravityForce = true;
				MagnificationDownscale.Log("GRAVITY_NATIVE_FORCE_ACTIVE profile=Unity-5.6.2f1-x86 entryVA=00DD25D0 conversion=unity-cache-mass pxMode=Force autowake=false sleepGuard=true globals=unchanged exeFile=unchanged");
			}
		}
		private static IntPtr Function(IntPtr table, int slot, int expectedVA)
		{
			IntPtr pointer = Marshal.ReadIntPtr(table, slot);
			if (pointer.ToInt64() - VerifyModule().BaseAddress.ToInt64() + 0x400000 != expectedVA)
				throw new InvalidOperationException("unexpected-vtable-slot-" + slot.ToString("X"));
			return pointer;
		}
        // Same physical equations, differently scaled solver rows. Diagnostic D6-only entry.
        internal static void UniformD6Unit(ConfigurableJoint joint, float unit, bool apply, bool expectOriginal)
        {
            if (!MagnificationJointUnitProbe.Requested || MagnificationCraftProbe.RunIndex != 2 || unit != 1000000f)
                throw new InvalidOperationException("uniform-unit-not-authorized-probe");
            ModuleIdentity module = VerifyModule();
            IntPtr px = Marshal.ReadIntPtr(Native(joint), 0x20);
            if (px == IntPtr.Zero) throw new InvalidOperationException("uniform-unit-no-px");
            IntPtr table = Marshal.ReadIntPtr(px);
            if (table.ToInt64() - module.BaseAddress.ToInt64() + 0x400000 != 0x0134ec30) throw new InvalidOperationException("uniform-unit-not-D6");
            int[] slots = { 0x44, 0x4c, 0x54, 0x5c }, expected = { 0x00d99d70, 0x00d99d90, 0x00d93720, 0x00d93740 };
            IntPtr[] functions = new IntPtr[4];
            for (int i = 0; i < 4; i++) functions[i] = Function(table, slots[i], expected[i]);
            IntPtr data = Marshal.ReadIntPtr(px, 0x50);
            if (data == IntPtr.Zero) throw new InvalidOperationException("uniform-unit-no-data");
            float[] values = new float[4]; Marshal.Copy(new IntPtr(data.ToInt64() + 0x38), values, 0, 4);
            const float inverseUnit = 0.000001f;
            int wantedBits = expectOriginal ? 0x3f800000 : 0x358637bd;
            for (int i = 0; i < 4; i++) if (new FloatBits { Value = values[i] }.Bits != wantedBits) throw new InvalidOperationException("uniform-unit-before-or-persistence-mismatch index=" + i + " actualBits=" + new FloatBits { Value = values[i] }.Bits.ToString("X8") + " expectedBits=" + wantedBits.ToString("X8"));
            if (!apply) return;
            Rigidbody own = joint.GetComponent<Rigidbody>(), other = joint.connectedBody;
            float m0 = own.mass, m1 = other.mass; Vector3 i0 = own.inertiaTensor, i1 = other.inertiaTensor;
            for (int i = 0; i < 4; i++)
            {
                SetScale setter = (SetScale)Marshal.GetDelegateForFunctionPointer(CdeclStub(functions[i], 1), typeof(SetScale));
                setter(px, inverseUnit);
            }
            Marshal.Copy(new IntPtr(data.ToInt64() + 0x38), values, 0, 4);
            for (int i = 0; i < 4; i++) if (new FloatBits { Value = values[i] }.Bits != 0x358637bd) throw new InvalidOperationException("uniform-unit-write-readback index=" + i + " actualBits=" + new FloatBits { Value = values[i] }.Bits.ToString("X8") + " expectedBits=358637BD");
            if (own.mass != m0 || other.mass != m1 || own.inertiaTensor != i0 || other.inertiaTensor != i1) throw new InvalidOperationException("uniform-unit-body-changed");
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
