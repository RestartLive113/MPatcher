using System;
using System.Reflection;
using System.Runtime.InteropServices;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
    // Read-only, exact Unity5.6.2f1 x86 profile. Main thread only, sampled at bounded probe phases.
    // Does not change anchors, bodies, poses, sleep state, constraints, or solver settings.
    internal static class MagnificationJointPoseNative
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)] private static extern IntPtr GetModuleHandle(string name);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] private delegate void GetActors(IntPtr self, out IntPtr a0, out IntPtr a1);
        private static GetActors getActors;
        private static readonly FieldInfo cached = typeof(UnityEngine.Object).GetField("m_CachedPtr", BindingFlags.Instance | BindingFlags.NonPublic);
        private static readonly MethodInfo stubFactory = typeof(MagnificationJointMassNative).GetMethod("CdeclStub", BindingFlags.Static | BindingFlags.NonPublic);
        private struct Pose { internal Quaternion Q; internal Vector3 P; }
        internal struct Snapshot
        {
            internal bool OwnFirst;
            internal Vector3 NativeOwn, NativeOther, PublicOwn, PublicOther;
            internal float NativeGap, PublicGap, OwnPointDelta, OtherPointDelta;
            internal float OwnFrameCacheDelta, OtherFrameCacheDelta;
            internal float OwnActorPositionDelta, OtherActorPositionDelta;
            internal float OwnActorRotationDelta, OtherActorRotationDelta;
            internal float OwnComDelta, OtherComDelta;
            internal Vector3 OwnLeverFromCom, OtherLeverFromCom;
            internal Vector3 OwnLocalNative, OtherLocalNative;
            internal Vector3 OwnC2b, OtherC2b;
            public override string ToString()
            {
                return "ownFirst=" + OwnFirst + " nativeGap=" + F(NativeGap) + " publicGap=" + F(PublicGap)
                    + " ownPointDelta=" + F(OwnPointDelta) + " otherPointDelta=" + F(OtherPointDelta)
                    + " ownFrameCacheDelta=" + F(OwnFrameCacheDelta) + " otherFrameCacheDelta=" + F(OtherFrameCacheDelta)
                    + " ownActorPositionDelta=" + F(OwnActorPositionDelta) + " otherActorPositionDelta=" + F(OtherActorPositionDelta)
                    + " ownActorRotationDelta=" + F(OwnActorRotationDelta) + " otherActorRotationDelta=" + F(OtherActorRotationDelta)
                    + " ownComDelta=" + F(OwnComDelta) + " otherComDelta=" + F(OtherComDelta)
                    + " nativeOwn=" + NativeOwn.ToString("R") + " nativeOther=" + NativeOther.ToString("R")
                    + " publicOwn=" + PublicOwn.ToString("R") + " publicOther=" + PublicOther.ToString("R")
                    + " ownLeverFromCom=" + OwnLeverFromCom.ToString("R") + " otherLeverFromCom=" + OtherLeverFromCom.ToString("R")
                    + " ownLocalNative=" + OwnLocalNative.ToString("R") + " otherLocalNative=" + OtherLocalNative.ToString("R")
                    + " ownC2b=" + OwnC2b.ToString("R") + " otherC2b=" + OtherC2b.ToString("R");
            }
        }
        private static string F(float value) { return value.ToString("R", System.Globalization.CultureInfo.InvariantCulture); }
        private static bool Finite(float f) { return !float.IsNaN(f) && !float.IsInfinity(f); }
        private static IntPtr Native(UnityEngine.Object value)
        {
            if (cached == null || cached.FieldType != typeof(IntPtr)) throw new InvalidOperationException("cached-pointer-layout");
            IntPtr pointer = (IntPtr)cached.GetValue(value);
            if (pointer == IntPtr.Zero) throw new InvalidOperationException("missing-native-object");
            return pointer;
        }
        private static long VA(IntPtr pointer, IntPtr module) { return pointer.ToInt64() - module.ToInt64() + 0x400000L; }
        private static void Slot(IntPtr table, int offset, int expected, IntPtr module)
        {
            if (VA(Marshal.ReadIntPtr(table, offset), module) != expected) throw new InvalidOperationException("slot-mismatch-" + offset.ToString("X"));
        }
        private static Pose ReadPose(IntPtr pointer, int offset)
        {
            float[] values = new float[7];
            Marshal.Copy(new IntPtr(pointer.ToInt64() + offset), values, 0, 7);
            for (int i = 0; i < 7; i++) if (!Finite(values[i])) throw new InvalidOperationException("nonfinite-pose");
            float norm = values[0]*values[0] + values[1]*values[1] + values[2]*values[2] + values[3]*values[3];
            if (!(norm > .98f && norm < 1.02f)) throw new InvalidOperationException("nonunit-pose-quaternion-" + F(norm));
            Pose pose; pose.Q = new Quaternion(values[0], values[1], values[2], values[3]); pose.P = new Vector3(values[4], values[5], values[6]); return pose;
        }
        private static Vector3 Point(Pose pose, Vector3 point) { return pose.P + pose.Q * point; }
        private static Pose ActorFrame(IntPtr actor, IntPtr module, out Pose bodyWorld, out Pose comLocal)
        {
            if (actor == IntPtr.Zero) throw new InvalidOperationException("world-or-static-actor-unsupported");
            IntPtr table = Marshal.ReadIntPtr(actor);
            if (VA(table, module) != 0x01355d58) throw new InvalidOperationException("not-profiled-rigid-dynamic");
            Slot(table, 0x50, 0x00dd16a0, module); Slot(table, 0x78, 0x00dd0e70, module);
            // Exact getCMassLocalPose00DD0E70 selects buffered COM when flag0x200 is set.
            if ((Marshal.ReadInt32(actor, 0x11c) & 0x200) == 0) comLocal = ReadPose(actor, 0x70);
            else
            {
                IntPtr buffer = Marshal.ReadIntPtr(actor, 0x38);
                if (buffer == IntPtr.Zero) throw new InvalidOperationException("missing-actor-com-buffer");
                comLocal = ReadPose(buffer, 0x90);
            }
            // getGlobalPose00DD16A0 composes this COM-world pose with inverse COM-local.
            bodyWorld = ReadPose(actor, 0xe0);
            Pose actorWorld; actorWorld.Q = bodyWorld.Q * Quaternion.Inverse(comLocal.Q);
            actorWorld.P = bodyWorld.P - actorWorld.Q * comLocal.P;
            return actorWorld;
        }
        internal static bool TryRead(Joint joint, out Snapshot result, out string failure)
        {
            result = new Snapshot(); failure = null;
            try
            {
                if (joint == null) throw new ArgumentNullException("joint");
                Rigidbody own = joint.GetComponent<Rigidbody>(), other = joint.connectedBody;
                if (own == null || other == null) throw new InvalidOperationException("two-rigidbodies-required");
                // Performs existing SHA256 and x86 verification BEFORE offset access.
                IntPtr px = MagnificationJointMassNative.JointIdentity(joint);
                if (px == IntPtr.Zero) throw new InvalidOperationException("no-physx-joint");
                IntPtr module = GetModuleHandle(null), table = Marshal.ReadIntPtr(px);
                long tableVa = VA(table, module);
                // All four exact-binary vtables share identical verified base-pose functions/layout.
                if (tableVa != 0x0134eaa0 && tableVa != 0x0134ec30 && tableVa != 0x0134ed78 && tableVa != 0x013502c8)
                    throw new InvalidOperationException("unprofiled-joint-vtable-" + tableVa.ToString("X8"));
                Slot(table, 0x18, 0x00d9ead0, module); Slot(table, 0x1c, 0x00d94490, module); Slot(table, 0x20, 0x00d937a0, module);
                if (getActors == null)
                {
                    if (stubFactory == null) throw new InvalidOperationException("existing-cdecl-stub-unavailable");
                    IntPtr stub = (IntPtr)stubFactory.Invoke(null, new object[] { Marshal.ReadIntPtr(table, 0x18), 2 });
                    getActors = (GetActors)Marshal.GetDelegateForFunctionPointer(stub, typeof(GetActors));
                }
                IntPtr actor0, actor1; getActors(px, out actor0, out actor1);
                IntPtr ownActor = Marshal.ReadIntPtr(Native(own), 0x34), otherActor = Marshal.ReadIntPtr(Native(other), 0x34);
                bool ownFirst = actor0 == ownActor && actor1 == otherActor;
                if (!ownFirst && !(actor1 == ownActor && actor0 == otherActor)) throw new InvalidOperationException("actor-order-mismatch");
                IntPtr data = Marshal.ReadIntPtr(px, 0x50);
                if (data == IntPtr.Zero) throw new InvalidOperationException("no-joint-data");
                int ownIndex = ownFirst ? 0 : 1, otherIndex = 1 - ownIndex;
                Pose ownLocal = ReadPose(px, 0x14 + ownIndex * 0x1c), otherLocal = ReadPose(px, 0x14 + otherIndex * 0x1c);
                Pose ownC2b = ReadPose(data, ownIndex * 0x1c), otherC2b = ReadPose(data, otherIndex * 0x1c);
                Pose ownBody, otherBody, ownCom, otherCom;
                Pose ownWorld = ActorFrame(ownActor, module, out ownBody, out ownCom), otherWorld = ActorFrame(otherActor, module, out otherBody, out otherCom);
                result.OwnFirst = ownFirst;
                result.NativeOwn = Point(ownBody, ownC2b.P); result.NativeOther = Point(otherBody, otherC2b.P);
                result.PublicOwn = joint.transform.TransformPoint(joint.anchor); result.PublicOther = other.transform.TransformPoint(joint.connectedAnchor);
                result.NativeGap = Vector3.Distance(result.NativeOwn, result.NativeOther); result.PublicGap = Vector3.Distance(result.PublicOwn, result.PublicOther);
                result.OwnPointDelta = Vector3.Distance(result.NativeOwn, result.PublicOwn); result.OtherPointDelta = Vector3.Distance(result.NativeOther, result.PublicOther);
                result.OwnFrameCacheDelta = Vector3.Distance(result.NativeOwn, Point(ownWorld, ownLocal.P));
                result.OtherFrameCacheDelta = Vector3.Distance(result.NativeOther, Point(otherWorld, otherLocal.P));
                result.OwnActorPositionDelta = Vector3.Distance(ownWorld.P, own.position); result.OtherActorPositionDelta = Vector3.Distance(otherWorld.P, other.position);
                result.OwnActorRotationDelta = Quaternion.Angle(ownWorld.Q, own.rotation); result.OtherActorRotationDelta = Quaternion.Angle(otherWorld.Q, other.rotation);
                result.OwnComDelta = Vector3.Distance(ownBody.P, own.worldCenterOfMass); result.OtherComDelta = Vector3.Distance(otherBody.P, other.worldCenterOfMass);
                result.OwnLeverFromCom = result.NativeOwn - ownBody.P; result.OtherLeverFromCom = result.NativeOther - otherBody.P;
                result.OwnLocalNative = ownLocal.P; result.OtherLocalNative = otherLocal.P;
                result.OwnC2b = ownC2b.P; result.OtherC2b = otherC2b.P;
                return true;
            }
            catch (Exception ex) { failure = ex.GetType().Name + ":" + ex.Message; return false; }
        }
    }
}
