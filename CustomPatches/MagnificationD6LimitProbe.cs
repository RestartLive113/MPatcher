using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using McnCraft;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
    // Opt-in Car experiment only. One native contact-distance write at READY for
    // run2/5%; all later samples are reads. Does not hook or suppress controllers.
    internal static class MagnificationD6LimitProbe
    {
        private static readonly bool Requested = Array.IndexOf(Environment.GetCommandLineArgs(), "--mpatcher-craft-limit-comparison") >= 0 || MagnificationAuxiliaryProbe.ToleranceComparison;
        private static readonly FieldInfo PistonJoint = typeof(PistonController).GetField("PDNNFOJPIDM", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        private static readonly MethodInfo StubFactory = typeof(MagnificationJointMassNative).GetMethod("CdeclStub", BindingFlags.Static | BindingFlags.NonPublic);
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)] private static extern IntPtr GetModuleHandle(string name);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] private delegate void SetLinearLimit(IntPtr self, IntPtr parameters);
        private sealed class Entry
        {
            internal PistonController Part;
            internal ConfigurableJoint Joint;
            internal IntPtr Identity;
            internal string Position;
            internal byte[] Original, Expected;
            internal SoftJointLimit PublicLimit;
            internal SoftJointLimitSpring PublicSpring;
        }
        private static readonly List<Entry> entries = new List<Entry>();
        private static readonly HashSet<string> sampled = new HashSet<string>(StringComparer.Ordinal);
        private static MachineController attached;
        private static bool configured, corrected;
        private static float scale;
        private static SetLinearLimit setter;
        private static IntPtr setterAddress;
        private static string F(float value) { return value.ToString("R", CultureInfo.InvariantCulture); }
        private static string Clean(string value) { return value.Replace('\r', ' ').Replace('\n', ' ').Replace('\t', ' '); }
        private static void Log(string value) { MagnificationDownscale.Log(value); }
        private static void RequireTestProcess()
        {
            string root = Path.GetFullPath(Path.Combine(Application.dataPath, ".."));
            string expected = Path.GetFullPath(Path.Combine(Environment.GetEnvironmentVariable("USERPROFILE") ?? "", "Downloads\\MachineCraftMPatcherTest"));
            if (!string.Equals(root.TrimEnd('\\', '/'), expected.TrimEnd('\\', '/'), StringComparison.OrdinalIgnoreCase)
                || Array.IndexOf(Environment.GetCommandLineArgs(), "--mpatcher-magnification-craft-probe") < 0)
                throw new InvalidOperationException("limit-comparison-requires-isolated-test-root-and-craft-probe");
        }
        internal static void Configure(string craft, bool nativeControl, float massUnit, ref int[] scales, ref int[] models)
        {
            if (!Requested) return;
            RequireTestProcess();
            string[] args = Environment.GetCommandLineArgs();
            if (MagnificationAuxiliaryProbe.ToleranceComparison)
            {
                if (craft != "Car" || nativeControl || massUnit != 1f) throw new InvalidOperationException("tolerance-d6-probe-invalid-config");
                scales = new[] { 5, 5, 5, 5 }; models = new[] { 22, 22, 22, 22 }; configured = true;
                Log("D6_TOLERANCE_PROBE_CONFIG correctedRuns=1,3 commonMassUnit=1e6 nativeWrites=contactDistance-only"); return;
            }
            if (craft != "Car" || nativeControl || massUnit != 1f || Array.IndexOf(args, "--mpatcher-craft-similarity") < 0)
                throw new InvalidOperationException("limit-comparison-requires-Car-model22-similarity-unit1");
            if (Array.IndexOf(args, "--mpatcher-craft-passive") >= 0 || Array.IndexOf(args, "--mpatcher-craft-passive-comparison") >= 0
                || Array.IndexOf(args, "--mpatcher-craft-rotor-passive") >= 0)
                throw new InvalidOperationException("limit-comparison-exclusive-with-controller-isolations");
            scales = new[] { 100, 5, 5 }; models = new[] { 22, 22, 22 }; configured = true;
            Log("D6_LIMIT_COMPARISON_CONFIG runs=3 order=ordinary100,ordinary5,contactDistance5 model=22 correctedRun=2 scope=Car-four-pistons-only correction=one-time-after-native-Start exact100=read-only allInputsAndControllers=unchanged");
        }
        internal static void Reset()
        {
            attached = null; corrected = false; scale = 0f; entries.Clear(); sampled.Clear();
            // No native values are restored: root calls Reset only before the old
            // scene is unloaded. No callback/hook or retained joint scope remains.
        }
        private static byte[] ReadBlock(ConfigurableJoint joint, out IntPtr identity, out MagnificationD6LimitNative.Snapshot snapshot)
        {
            string failure;
            if (!MagnificationD6LimitNative.TryRead(joint, out snapshot, out failure)) throw new InvalidOperationException("d6-profile-read-failed:" + failure);
            identity = MagnificationJointMassNative.JointIdentity(joint);
            IntPtr data = Marshal.ReadIntPtr(identity, 0x50);
            if (data == IntPtr.Zero) throw new InvalidOperationException("d6-data-disappeared");
            byte[] values = new byte[24]; Marshal.Copy(new IntPtr(data.ToInt64() + 0x60), values, 0, values.Length); return values;
        }
        private static bool Equal(byte[] a, byte[] b)
        {
            if (a.Length != b.Length) return false;
            for (int i = 0; i < a.Length; i++) if (a[i] != b[i]) return false;
            return true;
        }
        private static bool PublicUnchanged(Entry entry)
        {
            SoftJointLimit a = entry.Joint.linearLimit, b = entry.PublicLimit;
            SoftJointLimitSpring c = entry.Joint.linearLimitSpring, d = entry.PublicSpring;
            return a.limit == b.limit && a.bounciness == b.bounciness && a.contactDistance == b.contactDistance && c.spring == d.spring && c.damper == d.damper;
        }
        private static void ValidateDefault(Entry entry, MagnificationD6LimitNative.Snapshot s)
        {
            ConfigurableJoint j = entry.Joint;
            if (j.xMotion != ConfigurableJointMotion.Locked || j.yMotion != ConfigurableJointMotion.Limited || j.zMotion != ConfigurableJointMotion.Locked
                || j.angularXMotion != ConfigurableJointMotion.Locked || j.angularYMotion != ConfigurableJointMotion.Locked || j.angularZMotion != ConfigurableJointMotion.Locked)
                throw new InvalidOperationException("piston-motion-outside-experiment");
            int[] motions = { 0, 1, 0, 0, 0, 0 };
            for (int i = 0; i < 6; i++) if (s.Motions[i] != motions[i]) throw new InvalidOperationException("native-piston-motion-mismatch");
            if (entry.PublicLimit.bounciness != 0f || entry.PublicLimit.contactDistance != 0f || entry.PublicSpring.spring != 0f || entry.PublicSpring.damper != 0f
                || s.Restitution != 0f || s.BounceThreshold != 0f || s.Spring != 0f || s.Damper != 0f || s.ContactDistance != .2f
                || !(s.Extent > 0f) || s.Extent != entry.PublicLimit.limit)
                throw new InvalidOperationException("piston-not-default-hard-limit-profile");
        }
        internal static void Ready(MachineController machine)
        {
            if (!Requested) return;
            RequireTestProcess();
            if (!configured || machine == null || MagnificationCraftProbe.Model != 22 || MagnificationCraftProbe.MassUnit != (MagnificationAuxiliaryProbe.ToleranceComparison ? 1000000f : 1f))
                throw new InvalidOperationException("limit-comparison-invalid-ready");
            if (ReferenceEquals(attached, machine)) return;
            Reset(); scale = MagnificationScalePolicy.Valid(machine.DJCDFLDHPHK);
            int run = MagnificationCraftProbe.RunIndex;
            if (MagnificationAuxiliaryProbe.ToleranceComparison ? (run < 0 || run > 3 || Math.Abs(scale - .05f) > .000001f) : (run < 0 || run > 2 || (run == 0 ? scale != 1f : Math.Abs(scale - .05f) > .000001f)))
                throw new InvalidOperationException("limit-comparison-plan-scale-mismatch");
            if (PistonJoint == null || PistonJoint.FieldType != typeof(ConfigurableJoint)) throw new InvalidOperationException("piston-field-layout-mismatch");
            List<GameObject> roots = new List<GameObject> { machine.gameObject };
            if (machine.KBLANAFAJFP != null) roots.AddRange(machine.KBLANAFAJFP);
            HashSet<int> seen = new HashSet<int>();
            foreach (GameObject root in roots) if (root != null)
                foreach (PistonController part in root.GetComponentsInChildren<PistonController>(true))
                {
                    if (part.FPEBEEJGFPI != machine || !seen.Add(part.GetInstanceID())) continue;
                    ConfigurableJoint joint = PistonJoint.GetValue(part) as ConfigurableJoint;
                    if (joint == null || joint.connectedBody == null || joint.GetComponent<Rigidbody>() == null)
                        throw new InvalidOperationException("piston-joint-incomplete");
                    Entry entry = new Entry(); entry.Part = part; entry.Joint = joint; entry.Position = part.JNKEKNOAPHO.GetPosString(true);
                    entry.PublicLimit = joint.linearLimit; entry.PublicSpring = joint.linearLimitSpring;
                    MagnificationD6LimitNative.Snapshot snapshot;
                    entry.Original = ReadBlock(joint, out entry.Identity, out snapshot); entry.Expected = (byte[])entry.Original.Clone();
                    ValidateDefault(entry, snapshot); entries.Add(entry);
                }
            if (entries.Count != 4) throw new InvalidOperationException("limit-comparison-requires-exactly-four-pistons");
            HashSet<IntPtr> identities = new HashSet<IntPtr>();
            foreach (Entry entry in entries) if (!identities.Add(entry.Identity)) throw new InvalidOperationException("duplicate-piston-d6");
            // Finish all eligibility checks before the first write; runs0/1 only read.
            corrected = MagnificationAuxiliaryProbe.ToleranceComparison ? run % 2 == 1 : run == 2;
            if (corrected)
            {
                if (scale == 1f) throw new InvalidOperationException("forbidden-exact100-mutation");
                float contact = .2f * scale;
                if (!(contact > 0f) || float.IsInfinity(contact) || float.IsNaN(contact)) throw new InvalidOperationException("invalid-contact-distance");
                foreach (Entry entry in entries)
                {
                    byte[] bits = BitConverter.GetBytes(contact); Array.Copy(bits, 0, entry.Expected, 16, 4);
                    WriteLimit(entry, contact);
                }
            }
            attached = machine;
            Log("D6_LIMIT_COMPARISON_READY run=" + run + " scale=" + F(scale) + " corrected=" + corrected + " pistons=" + entries.Count
                + " nativeWrites=" + (corrected ? 4 : 0) + " lifecycle=after-native-Start-before-probe-ready sourcePublicContactDistance=0 nativeContactDistance=" + F(corrected ? .2f * scale : .2f)
                + " bytesChanged=contactDistance-only setters=PxD6Joint.setLinearLimit poseWrites=0 controllerChanges=0 acceptance=unassigned");
            Sample(machine, "ready");
        }
        private static void WriteLimit(Entry entry, float contact)
        {
            IntPtr identity; MagnificationD6LimitNative.Snapshot before;
            byte[] actual = ReadBlock(entry.Joint, out identity, out before);
            if (identity != entry.Identity || !Equal(actual, entry.Original) || !PublicUnchanged(entry)) throw new InvalidOperationException("piston-changed-before-write");
            IntPtr module = GetModuleHandle(null), table = Marshal.ReadIntPtr(identity), function = Marshal.ReadIntPtr(table, 0x88);
            if (function.ToInt64() - module.ToInt64() + 0x400000L != 0x00d94db0) throw new InvalidOperationException("d6-setter-mismatch");
            // TryRead has already checked the exact vtable and setter/getter bytes.
            // Reuse the existing, tested x86 cdecl(self,pointer)->thiscall(pointer) bridge.
            if (setter == null || setterAddress != function)
            {
                if (StubFactory == null) throw new InvalidOperationException("existing-cdecl-stub-unavailable");
                IntPtr bridge = (IntPtr)StubFactory.Invoke(null, new object[] { function, 1 });
                setter = (SetLinearLimit)Marshal.GetDelegateForFunctionPointer(bridge, typeof(SetLinearLimit)); setterAddress = function;
            }
            IntPtr block = Marshal.AllocHGlobal(24);
            try
            {
                // Copy only into our temporary argument buffer; no raw joint/cache writes.
                Marshal.Copy(entry.Expected, 0, block, 24); setter(identity, block);
            }
            finally { Marshal.FreeHGlobal(block); }
            MagnificationD6LimitNative.Snapshot after;
            actual = ReadBlock(entry.Joint, out identity, out after);
            if (identity != entry.Identity || !Equal(actual, entry.Expected) || !PublicUnchanged(entry))
                throw new InvalidOperationException("d6-contact-write-postcondition");
            Log("D6_LIMIT_COMPARISON_WRITE run=" + MagnificationCraftProbe.RunIndex + " piston=" + Clean(entry.Position) + " body=" + entry.Joint.name
                + " nativeBefore=" + F(before.ContactDistance) + " nativeAfter=" + F(after.ContactDistance) + " expected=" + F(contact)
                + " nativeExtent=" + F(after.Extent) + " restitution=" + F(after.Restitution) + " limitSpring=" + F(after.Spring) + "," + F(after.Damper)
                + " publicContactBefore=" + F(entry.PublicLimit.contactDistance) + " publicContactAfter=" + F(entry.Joint.linearLimit.contactDistance)
                + " other20BytesUnchanged=true full24ByteReadback=true");
        }
        internal static void Sample(MachineController machine, string phase)
        {
            if (!Requested || attached == null || !ReferenceEquals(attached, machine)) return;
            if (sampled.Count >= 16 || !sampled.Add(phase)) return;
            int escaped = 0;
            foreach (Entry entry in entries)
            {
                bool sameIdentity = false, sameBlock = false, samePublic = false;
                try
                {
                    IntPtr identity; MagnificationD6LimitNative.Snapshot snapshot;
                    byte[] actual = ReadBlock(entry.Joint, out identity, out snapshot);
                    sameIdentity = identity == entry.Identity; sameBlock = Equal(actual, entry.Expected); samePublic = PublicUnchanged(entry);
                    bool escape = !sameIdentity || !sameBlock || !samePublic; if (escape) escaped++;
                    Log("D6_LIMIT_COMPARISON_SAMPLE run=" + MagnificationCraftProbe.RunIndex + " phase=" + Clean(phase) + " scale=" + F(scale) + " corrected=" + corrected
                        + " piston=" + Clean(entry.Position) + " body=" + entry.Joint.name + " nativeContact=" + F(snapshot.ContactDistance) + " nativeContactCraft=" + F(snapshot.ContactDistance / scale)
                        + " extent=" + F(snapshot.Extent) + " contactToExtent=" + F(snapshot.ContactDistance / snapshot.Extent)
                        + " restitution=" + F(snapshot.Restitution) + " bounceThreshold=" + F(snapshot.BounceThreshold) + " limitSpring=" + F(snapshot.Spring) + "," + F(snapshot.Damper)
                        + " projectionDistance=" + F(snapshot.ProjectionDistance) + " publicContact=" + F(entry.Joint.linearLimit.contactDistance)
                        + " sameIdentity=" + sameIdentity + " same24Bytes=" + sameBlock + " publicUnchanged=" + samePublic + " escape=" + escape + " resubmit=false");
                }
                catch (Exception error) { escaped++; Log("D6_LIMIT_COMPARISON_READ_FAILED phase=" + Clean(phase) + " piston=" + Clean(entry.Position) + " error=" + Clean(error.GetType().Name + ":" + error.Message)); }
            }
            Log("D6_LIMIT_COMPARISON_SAMPLE_END run=" + MagnificationCraftProbe.RunIndex + " phase=" + Clean(phase) + " corrected=" + corrected + " pistons=" + entries.Count + " escaped=" + escaped + " mutation=none");
        }
    }
}
