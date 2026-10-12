using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
    // Main-thread read-only observer for bounded Car graph samples.
    // Reuses the existing exact-EXE/x86 verifier; never updates mass properties.
    internal static class MagnificationMassCacheNative
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)] private static extern IntPtr GetModuleHandle(string name);
        private static readonly MethodInfo VerifyModule = typeof(MagnificationJointMassNative).GetMethod("VerifyModule", BindingFlags.Static | BindingFlags.NonPublic);
        private static readonly FieldInfo CachedPointer = typeof(UnityEngine.Object).GetField("m_CachedPtr", BindingFlags.Instance | BindingFlags.NonPublic);
        [StructLayout(LayoutKind.Explicit)]
        private struct FloatBits
        {
            [FieldOffset(0)] internal int Bits;
            [FieldOffset(0)] internal float Value;
        }
        internal struct Snapshot
        {
            internal float UnityCachedMass, PhysXMass;
            internal double CacheToPhysXRatio, RelativeDifference;
            internal bool BitEqual;
            internal byte AutomaticInertia, AutomaticCenterOfMass, DeferredMass;
        }
        private static long VA(IntPtr pointer, IntPtr module) { return pointer.ToInt64() - module.ToInt64() + 0x400000L; }
        private static bool PositiveFinite(float value) { return value > 0f && !float.IsInfinity(value) && !float.IsNaN(value); }
        private static void Bytes(IntPtr function, byte[] expected)
        {
            for (int i = 0; i < expected.Length; i++) if (Marshal.ReadByte(function, i) != expected[i]) throw new InvalidOperationException("mass-cache-code-profile-mismatch");
        }
        internal static bool TryReadMassCache(Rigidbody body, out float mass, out string failure)
        {
            Snapshot snapshot;
            bool read = TryRead(body, out snapshot, out failure);
            mass = read ? snapshot.UnityCachedMass : 0f;
            return read;
        }
        internal static bool TryRead(Rigidbody body, out Snapshot result, out string failure)
        {
            result = new Snapshot(); failure = null;
            try
            {
                if (body == null) throw new ArgumentNullException("body");
                if (VerifyModule == null || VerifyModule.GetParameters().Length != 0) throw new InvalidOperationException("existing-module-verifier-unavailable");
                // SHA256 and x86 validation happen BEFORE any native pointer/offset read.
                if (VerifyModule.Invoke(null, null) == null) throw new InvalidOperationException("existing-module-verifier-returned-null");
                if (CachedPointer == null || CachedPointer.FieldType != typeof(IntPtr)) throw new InvalidOperationException("cached-pointer-layout-mismatch");
                IntPtr native = (IntPtr)CachedPointer.GetValue(body);
                if (native == IntPtr.Zero) throw new InvalidOperationException("no-native-rigidbody");
                IntPtr module = GetModuleHandle(null);
                if (module == IntPtr.Zero) throw new InvalidOperationException("module-handle-unavailable");
                // Exact Unity mass getter: native+34 -> actor vtable+80.
                Bytes(new IntPtr(module.ToInt64() + 0x00635900 - 0x400000L), new byte[] { 0x8b,0x49,0x34,0x8b,0x01,0x8b,0x90,0x80,0x00,0x00,0x00,0xff,0xe2 });
                // Exact native mass setter segment verifies cached-mass+44 and automatic-inertia+51.
                // We READ its bytes only; the setter and updater are never invoked.
                Bytes(new IntPtr(module.ToInt64() + 0x00641256 - 0x400000L), new byte[] { 0x80,0x7e,0x51,0x00,0xf3,0x0f,0x11,0x46,0x44 });
                // Existing updater profile guards the flag layout also used by WriteSmallMass.
                Bytes(new IntPtr(module.ToInt64() + 0x00640f30 - 0x400000L), new byte[] { 0x55,0x8b,0xec,0x83,0xec,0x54,0x53,0x56,0x8b,0xf1,0x80,0x7e,0x54,0x00,0x57,0x74,0x1f });
                IntPtr actor = Marshal.ReadIntPtr(native, 0x34);
                if (actor == IntPtr.Zero) throw new InvalidOperationException("no-physx-actor");
                IntPtr table = Marshal.ReadIntPtr(actor);
                if (VA(table, module) != 0x01355d58) throw new InvalidOperationException("unprofiled-rigid-dynamic");
                IntPtr getter = Marshal.ReadIntPtr(table, 0x80);
                if (VA(getter, module) != 0x00dd0ef0) throw new InvalidOperationException("unexpected-physx-mass-getter");
                Bytes(getter, new byte[] { 0x55,0x8b,0xec,0x51,0xf6,0x81,0x1c,0x01,0x00,0x00,0x01,0x74,0x0a,0x8b,0x41,0x38 });
                FloatBits cache = new FloatBits { Bits = Marshal.ReadInt32(native, 0x44) };
                byte automaticInertia = Marshal.ReadByte(native, 0x51), automaticCenter = Marshal.ReadByte(native, 0x52), deferredMass = Marshal.ReadByte(native, 0x54);
                if (automaticInertia > 1 || automaticCenter > 1 || deferredMass > 1) throw new InvalidOperationException("mass-property-flag-layout");
                // Ordinary getter is authoritative Px mass on this verified profile;
                // it does not use a source-units bridge or recompute the mass.
                float actual = body.mass;
                if (!PositiveFinite(cache.Value) || !PositiveFinite(actual)) throw new InvalidOperationException("mass-cache-or-physx-not-positive-finite");
                result.UnityCachedMass = cache.Value; result.PhysXMass = actual;
                result.CacheToPhysXRatio = (double)cache.Value / (double)actual;
                result.RelativeDifference = ((double)cache.Value - (double)actual) / (double)actual;
                result.BitEqual = cache.Bits == new FloatBits { Value = actual }.Bits;
                result.AutomaticInertia = automaticInertia; result.AutomaticCenterOfMass = automaticCenter; result.DeferredMass = deferredMass;
                return true;
            }
            catch (Exception error)
            {
                TargetInvocationException invocation = error as TargetInvocationException;
                if (invocation != null && invocation.InnerException != null) error = invocation.InnerException;
                failure = error.GetType().Name + ":" + error.Message; return false;
            }
        }
        internal static string GraphFields(Rigidbody body)
        {
            Snapshot s; string failure;
            if (!TryRead(body, out s, out failure)) return "\tnativeMassCacheRead=failed\tnativeMassCacheFailure=" + failure.Replace('\t', ' ').Replace('\r', ' ').Replace('\n', ' ');
            return "\tnativeMassCacheRead=ok\tnativeMassCacheProfile=Unity5.6.2f1-x86\tnativeMassCacheMutation=none"
                + "\tunityCachedMass=" + s.UnityCachedMass.ToString("R", CultureInfo.InvariantCulture) + "\tphysxMass=" + s.PhysXMass.ToString("R", CultureInfo.InvariantCulture)
                + "\tcacheToPhysxMassRatio=" + s.CacheToPhysXRatio.ToString("R", CultureInfo.InvariantCulture)
                + "\tcacheToPhysxMassRelativeDifference=" + s.RelativeDifference.ToString("R", CultureInfo.InvariantCulture)
                + "\tcachePhysxMassBitEqual=" + (s.BitEqual ? "1" : "0") + "\tnativeAutomaticInertia=" + s.AutomaticInertia
                + "\tnativeAutomaticCenterOfMass=" + s.AutomaticCenterOfMass + "\tnativeDeferredMass=" + s.DeferredMass;
        }
    }
}
