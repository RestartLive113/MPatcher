using System;
using System.Globalization;
using System.Runtime.InteropServices;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
    // Research observer. Exact Unity5.6.2f1 x86 only, on the main thread.
    // No delegates, native setters, pointer writes, mutation hooks or simulation calls.
    internal static class MagnificationD6LimitNative
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)] private static extern IntPtr GetModuleHandle(string name);
        private static readonly bool Requested = Array.IndexOf(Environment.GetCommandLineArgs(), "--mpatcher-magnification-craft-probe") >= 0
            && Array.IndexOf(Environment.GetCommandLineArgs(), "--mpatcher-craft-graph-probe") >= 0;
        internal struct Snapshot
        {
            internal float Restitution, BounceThreshold, Spring, Damper, ContactDistance, Extent;
            internal float ProjectionDistance, ProjectionAngleRadians;
            internal float[] DriveValues; // x,y,z,twist,swing,slerp: spring,damper,maxForce
            internal int[] DriveFlags, Motions;
            internal bool RecomputeMotion, RecomputeLimits;
        }
        private static string F(float value) { return value.ToString("R", CultureInfo.InvariantCulture); }
        private static bool Finite(float value) { return !float.IsNaN(value) && !float.IsInfinity(value); }
        private static long VA(IntPtr pointer, IntPtr module) { return pointer.ToInt64() - module.ToInt64() + 0x400000L; }
        private static void Guard(IntPtr table, int offset, int expected, byte[] prefix, IntPtr module)
        {
            IntPtr code = Marshal.ReadIntPtr(table, offset);
            if (VA(code, module) != expected) throw new InvalidOperationException("slot-" + offset.ToString("X") + "-mismatch");
            for (int i = 0; i < prefix.Length; i++) if (Marshal.ReadByte(code, i) != prefix[i]) throw new InvalidOperationException("code-" + expected.ToString("X8") + "-mismatch");
        }
        private static float[] Floats(IntPtr data, int offset, int count)
        {
            float[] values = new float[count];
            Marshal.Copy(new IntPtr(data.ToInt64() + offset), values, 0, count);
            for (int i = 0; i < count; i++) if (!Finite(values[i])) throw new InvalidOperationException("nonfinite-data-" + (offset + 4 * i).ToString("X"));
            return values;
        }
        internal static bool TryRead(ConfigurableJoint joint, out Snapshot result, out string failure)
        {
            result = new Snapshot(); failure = null;
            try
            {
                if (joint == null) throw new ArgumentNullException("joint");
                // Existing bridge verifies x86 and exact EXE SHA before native object access.
                IntPtr px = MagnificationJointMassNative.JointIdentity(joint);
                if (px == IntPtr.Zero) throw new InvalidOperationException("no-physx-joint");
                IntPtr module = GetModuleHandle(null), table = Marshal.ReadIntPtr(px);
                if (module == IntPtr.Zero || VA(table, module) != 0x0134ec30) throw new InvalidOperationException("unprofiled-d6-vtable");
                Guard(table, 0x78, 0x00d947d0, new byte[] { 0x55,0x8b,0xec,0x8b,0x41,0x50,0x8b,0x4d,0x08,0x8b,0x44,0x88,0x48 }, module);
                Guard(table, 0x88, 0x00d94db0, new byte[] { 0x55,0x8b,0xec,0x8b,0x41,0x50,0x8b,0x55,0x08,0xf3,0x0f,0x7e,0x02,0x66,0x0f,0xd6,0x40,0x60 }, module);
                Guard(table, 0x8c, 0x00d947f0, new byte[] { 0x55,0x8b,0xec,0x8b,0x49,0x50,0x8b,0x45,0x08,0xf3,0x0f,0x7e,0x41,0x60 }, module);
                Guard(table, 0xa4, 0x00d97aa0, new byte[] { 0x55,0x8b,0xec,0x8b,0x55,0x0c,0x8b,0x45,0x08,0x83,0xc2,0x0b,0xc1,0xe2,0x04,0x03,0x51,0x50 }, module);
                Guard(table, 0xbc, 0x00d94900, new byte[] { 0x8b,0x41,0x50,0xd9,0x80,0x78,0x01,0x00,0x00,0xc3 }, module);
                Guard(table, 0xc4, 0x00d948f0, new byte[] { 0x8b,0x41,0x50,0xd9,0x80,0x7c,0x01,0x00,0x00,0xc3 }, module);
                IntPtr data = Marshal.ReadIntPtr(px, 0x50);
                if (data == IntPtr.Zero || (data.ToInt64() & 3L) != 0) throw new InvalidOperationException("missing-or-unaligned-d6-data");
                // Same 24-byte block copied by D6 getLinearLimit00D947F0.
                float[] limit = Floats(data, 0x60, 6);
                result.Restitution = limit[0]; result.BounceThreshold = limit[1]; result.Spring = limit[2];
                result.Damper = limit[3]; result.ContactDistance = limit[4]; result.Extent = limit[5];
                if (result.Restitution < 0f || result.Restitution > 1f || result.BounceThreshold < 0f || result.Spring < 0f
                    || result.Damper < 0f || result.ContactDistance < 0f || result.Extent < 0f) throw new InvalidOperationException("invalid-d6-linear-limit");
                result.Motions = new int[6]; result.DriveFlags = new int[6]; result.DriveValues = new float[18];
                for (int i = 0; i < 6; i++)
                {
                    result.Motions[i] = Marshal.ReadInt32(data, 0x48 + 4 * i);
                    if (result.Motions[i] < 0 || result.Motions[i] > 2) throw new InvalidOperationException("invalid-motion");
                    float[] drive = Floats(data, 0xb0 + 16 * i, 3);
                    for (int j = 0; j < 3; j++) result.DriveValues[3 * i + j] = drive[j];
                    result.DriveFlags[i] = Marshal.ReadInt32(data, 0xbc + 16 * i);
                }
                float[] projection = Floats(data, 0x178, 2);
                result.ProjectionDistance = projection[0]; result.ProjectionAngleRadians = projection[1];
                result.RecomputeMotion = Marshal.ReadByte(px, 0x54) != 0; result.RecomputeLimits = Marshal.ReadByte(px, 0x55) != 0;
                return true;
            }
            catch (Exception error) { failure = error.GetType().Name + ":" + error.Message; return false; }
        }
        internal static string GraphFields(ConfigurableJoint joint, float scale)
        {
            if (!Requested) return "";
            Snapshot s; string failure;
            if (!TryRead(joint, out s, out failure)) return "\tnativeD6Read=failed\tnativeD6Failure=" + failure.Replace('\t', ' ').Replace('\r', ' ').Replace('\n', ' ');
            string fields = "\tnativeD6Read=ok\tnativeD6Profile=Unity5.6.2f1-x86-0134EC30\tnativeD6Mutation=none"
                + "\tnativeLinearLimit=" + F(s.Extent) + "\tnativeLinearRestitution=" + F(s.Restitution) + "\tnativeLinearBounceThreshold=" + F(s.BounceThreshold)
                + "\tnativeLinearLimitSpring=" + F(s.Spring) + "," + F(s.Damper) + "\tnativeLinearContactDistance=" + F(s.ContactDistance)
                + "\tnativeLinearContactDistanceCraft=" + F(s.ContactDistance / scale) + "\tnativeLinearContactToExtent=" + (s.Extent > 0f ? F(s.ContactDistance / s.Extent) : "inactive-zero-extent")
                + "\tnativeLinearHardActivationDistance=" + F(s.Extent - s.ContactDistance) + "\tnativeLinearSoft=" + (s.Spring > 0f || s.Damper > 0f ? "1" : "0")
                + "\tnativeProjectionDistance=" + F(s.ProjectionDistance) + "\tnativeProjectionDistanceCraft=" + F(s.ProjectionDistance / scale)
                + "\tnativeProjectionAngleRadians=" + F(s.ProjectionAngleRadians) + "\tnativeRecomputeMotion=" + (s.RecomputeMotion ? "1" : "0") + "\tnativeRecomputeLimits=" + (s.RecomputeLimits ? "1" : "0");
            string[] names = { "X", "Y", "Z", "Twist", "Swing", "Slerp" };
            for (int i = 0; i < 6; i++) fields += "\tnativeMotion" + i + "=" + s.Motions[i]
                + "\tnativeDrive" + names[i] + "=" + F(s.DriveValues[3 * i]) + "," + F(s.DriveValues[3 * i + 1]) + "," + F(s.DriveValues[3 * i + 2])
                + "\tnativeDrive" + names[i] + "Flags=" + s.DriveFlags[i];
            return fields;
        }
    }
}
