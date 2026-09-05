using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using McnCraft;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
    internal static class SetupPrecisionNetwork
    {
        private sealed class State
        {
            internal bool Send;
            internal bool Receive;
            internal int Protocol;
            internal SetupPrecisionCodec.Snapshot Pending;
            internal readonly SetupPrecisionCodec.Pose[] Last = new SetupPrecisionCodec.Pose[64];
            internal readonly bool[] Sent = new bool[64];
            internal readonly int[] Age = new int[64];
            internal readonly SetupPrecisionCodec.Pose[] Received = new SetupPrecisionCodec.Pose[64];
            internal readonly bool[] HasReceived = new bool[64];
            internal readonly List<SetupPrecisionCodec.Pose> Samples = new List<SetupPrecisionCodec.Pose>(64);
            internal readonly HashSet<NMLMDCCDFPN> Groups = new HashSet<NMLMDCCDFPN>();
            internal int WriteLog;
            internal int ReadLog;
        }

        private sealed class ReadContext
        {
            internal MachineController Machine;
            internal SetupPrecisionCodec.Snapshot Snapshot;
            internal int Attached;
            internal int Rotations;
            internal int Offsets;
        }

        private static readonly Dictionary<MachineSerializer, State> States = new Dictionary<MachineSerializer, State>();
        private static readonly HashSet<NMLMDCCDFPN> PreciseGroups = new HashSet<NMLMDCCDFPN>();
        [ThreadStatic] private static ReadContext current;
        private static readonly Type Codec = typeof(MachineSerializer).Assembly.GetType("IPFOLBIPILG", true);
        private static readonly MethodInfo Compress = AccessTools.Method(Codec, "JBKJCBDNCGI", new Type[] { typeof(byte[]), typeof(int) });
        private static readonly MethodInfo Decompress = AccessTools.Method(Codec, "LJPINOEINPE", new Type[] { typeof(byte[]) });

        internal static void Register(Harmony patcher)
        {
            Stopwatch startup = Stopwatch.StartNew();
            long checkpoint = 0;
            Type iterator = typeof(MachineSerializer).GetNestedType("<MakeStructureNB>c__Iterator0", BindingFlags.NonPublic);
            if (iterator == null || Compress == null || Decompress == null) throw new MissingMemberException("SETUP structure codec");
            bool sharedStructure = CouplerRotationNetwork.SharedSetupPrecisionRegistered;
            if (!sharedStructure) Transpile(patcher, typeof(MachineSerializer), "SyncStructure", "WriterTranspiler");
            LogStartupTiming(startup, sharedStructure ? "SyncStructure.shared-reuse" : "SyncStructure", ref checkpoint);
            Transpile(patcher, typeof(MachineSerializer), "RPC_SyncStructure", "RpcTranspiler");
            LogStartupTiming(startup, "RPC_SyncStructure", ref checkpoint);
            if (!sharedStructure) Transpile(patcher, iterator, "MoveNext", "ReaderTranspiler");
            LogStartupTiming(startup, sharedStructure ? "MakeStructureNB.MoveNext.shared-reuse" : "MakeStructureNB.MoveNext", ref checkpoint);
            patcher.Patch(AccessTools.Method(typeof(MachineSerializer), "EFCHCLNMPMD"), null,
                new HarmonyMethod(typeof(SetupPrecisionNetwork), "CoroutinePostfix"), null, null);
            LogStartupTiming(startup, "EFCHCLNMPMD", ref checkpoint);
            patcher.Patch(AccessTools.Method(typeof(NMLMDCCDFPN), "MDAAHPEENMJ"),
                new HarmonyMethod(typeof(SetupPrecisionNetwork), "InitialRotationPrefix"), null, null, null);
            LogStartupTiming(startup, "MDAAHPEENMJ", ref checkpoint);
            patcher.Patch(AccessTools.Method(typeof(NMLMDCCDFPN), "AGGDIGBFEBC"),
                new HarmonyMethod(typeof(SetupPrecisionNetwork), "InitialOffsetPrefix"), null, null, null);
            LogStartupTiming(startup, "AGGDIGBFEBC", ref checkpoint);
            MethodInfo serialize = AccessTools.Method(typeof(MachineSerializer), "OnSerializeNetworkView");
            if (serialize == null) throw new MissingMethodException("MachineSerializer.OnSerializeNetworkView");
            HarmonyMethod motion = new HarmonyMethod(typeof(SetupPrecisionNetwork), "MotionTranspiler");
            motion.priority = Priority.Last;
            patcher.Patch(serialize, new HarmonyMethod(typeof(SetupPrecisionNetwork), "SerializePrefix"), null, motion, null);
            LogStartupTiming(startup, "OnSerializeNetworkView.combined", ref checkpoint);
            patcher.Patch(AccessTools.Method(typeof(MachineSerializer), "NNKKNGFHHBB"),
                new HarmonyMethod(typeof(SetupPrecisionNetwork), "NativeSamplesPrefix"), null, null, null);
            LogStartupTiming(startup, "NNKKNGFHHBB", ref checkpoint);
            patcher.Patch(AccessTools.Method(typeof(MachineSerializer), "ALCBIFFPNPJ"),
                new HarmonyMethod(typeof(SetupPrecisionNetwork), "NativeApplyPrefix"), null, null, null);
            LogStartupTiming(startup, "ALCBIFFPNPJ", ref checkpoint);
            Transpile(patcher, typeof(NMLMDCCDFPN), "KMFHOKOOEPG", "InterpolationTranspiler");
            LogStartupTiming(startup, "KMFHOKOOEPG", ref checkpoint);
            MethodInfo destroy = AccessTools.Method(typeof(MachineSerializer), "OnDestroy");
            if (destroy == null) throw new MissingMethodException("MachineSerializer.OnDestroy");
            patcher.Patch(destroy, new HarmonyMethod(typeof(SetupPrecisionNetwork), "DestroyPrefix"), null, null, null);
            LogStartupTiming(startup, "OnDestroy", ref checkpoint);
        }

        private static void LogStartupTiming(Stopwatch timer, string target, ref long checkpoint)
        {
            long elapsed = timer.ElapsedMilliseconds;
            SetupPrecision.Log("STARTUP_TIMING network=" + target + " stepMs=" + (elapsed - checkpoint) + " totalMs=" + elapsed);
            checkpoint = elapsed;
        }

        private static void Transpile(Harmony patcher, Type type, string name, string method)
        {
            MethodInfo target = AccessTools.Method(type, name);
            if (target == null) throw new MissingMethodException(type.Name, name);
            HarmonyMethod transpiler = new HarmonyMethod(typeof(SetupPrecisionNetwork), method);
            transpiler.priority = Priority.Last;
            patcher.Patch(target, null, null, transpiler, null);
        }

        private static State GetState(MachineSerializer owner)
        {
            State result;
            if (!States.TryGetValue(owner, out result)) { result = new State(); States.Add(owner, result); }
            return result;
        }

        private static byte[] Pack(byte[] native, int mode, MachineSerializer owner)
        {
            if (!SetupPrecision.IsRegistered)
                return (byte[])Compress.Invoke(null, new object[] { native, mode });
            MachineController machine = owner.GetComponent<MachineController>();
            SetupPrecisionCodec.Snapshot snapshot = Capture(machine);
            State state = GetState(owner);
            state.Send = snapshot.Blocks.Count > 0;
            if (state.Send) state.Protocol = SetupPrecisionCodec.CurrentVersion;
            else if (!state.Receive) state.Protocol = 0;
            Array.Clear(state.Sent, 0, state.Sent.Length);
            byte[] envelope = SetupPrecisionCodec.Encode(native, snapshot);
            if (state.Send) SetupPrecision.Log("NETWORK_WRITE protocol=SETUP-v" + snapshot.Version + " blocks=" + snapshot.Blocks.Count
                + " poses=" + snapshot.Poses.Count + " nativeBytes=" + native.Length + " envelopeBytes=" + envelope.Length);
            return (byte[])Compress.Invoke(null, new object[] { envelope, mode });
        }

        private static byte[] Unpack(byte[] packet, MachineSerializer owner)
        {
            State state = GetState(owner);
            state.Receive = false;
            state.Protocol = 0;
            state.Pending = null;
            ResetReceivedMotion(state);
            foreach (NMLMDCCDFPN group in state.Groups) PreciseGroups.Remove(group);
            state.Groups.Clear();
            try
            {
                byte[] decoded = (byte[])Decompress.Invoke(null, new object[] { packet });
                SetupPrecisionCodec.Snapshot snapshot;
                byte[] native = SetupPrecisionCodec.Decode(decoded, out snapshot);
                state.Pending = snapshot;
                state.Receive = snapshot != null;
                if (snapshot != null)
                {
                    state.Protocol = snapshot.Version;
                    SetupPrecision.Log("NETWORK_READ protocol=SETUP-v" + snapshot.Version + " blocks=" + snapshot.Blocks.Count + " poses=" + snapshot.Poses.Count);
                }
                return native;
            }
            catch (Exception error)
            {
                SetupPrecision.Log("NETWORK_REJECTED protocol=SETUP-v1/v2 " + error.Message);
                throw;
            }
        }

        private static SetupPrecisionCodec.Snapshot Capture(MachineController machine)
        {
            SetupPrecisionCodec.Snapshot result = new SetupPrecisionCodec.Snapshot();
            foreach (GameObject body in machine.KBLANAFAJFP)
                for (int i = 0; i < body.transform.childCount; i++)
                {
                    BlockController controller = body.transform.GetChild(i).GetComponent<BlockController>();
                    if (controller == null || !SetupPrecisionData.HasAny(controller.JNKEKNOAPHO)) continue;
                    BlockData block = controller.JNKEKNOAPHO;
                    result.Blocks.Add(SetupPrecisionCodec.Key(block), block);
                }
            if (result.Blocks.Count == 0) return result;
            HDBLLPODNLN graph = machine.EPGELCMKKOC;
            for (int group = 1; group < machine.KBLANAFAJFP.Count; group++)
            {
                int parent = graph.PKBPJPCJAID[group];
                if (parent < 0) continue;
                int kind = machine.ILBAAENKMBL[group].ANAHNNNBKFC;
                SetupPrecisionCodec.Pose pose = new SetupPrecisionCodec.Pose { Index = group };
                if (kind == 15)
                {
                    pose.Kind = 15;
                    pose.Rotation = -CouplerRotationMath.ToBoxEuler(CouplerRotationMath.FromRotationVector(graph.MJBDKMNEKML[group]));
                    pose.Offset = graph.NFOEKNHCNBM[group];
                }
                else if (kind < 6)
                {
                    pose.Kind = 0;
                    Matrix4x4 relative = machine.KBLANAFAJFP[parent].transform.worldToLocalMatrix * machine.KBLANAFAJFP[group].transform.localToWorldMatrix;
                    pose.Rotation = BDLEJBBJJOI.PJHDCFEPAOP(relative).eulerAngles;
                }
                else
                {
                    pose.Kind = 1;
                    int axis = kind < 8 ? 0 : kind < 10 ? 1 : 2;
                    pose.Offset[axis] = graph.NNNBCKKNONF[group][axis];
                }
                result.Poses.Add(group, pose);
            }
            return result;
        }

        private static void CoroutinePostfix(MachineSerializer __instance, ref IEnumerator __result)
        {
            State state;
            if (!States.TryGetValue(__instance, out state) || state.Pending == null) return;
            ReadContext context = new ReadContext { Machine = __instance.GetComponent<MachineController>(), Snapshot = state.Pending };
            state.Pending = null;
            __result = Scoped(__result, context);
        }

        private static IEnumerator Scoped(IEnumerator original, ReadContext context)
        {
            try
            {
                while (true)
                {
                    ReadContext previous = current;
                    bool next;
                    try { current = context; next = original.MoveNext(); }
                    finally { current = previous; }
                    if (!next) break;
                    yield return original.Current;
                }
                SetupPrecision.Log("NETWORK_APPLIED blocks=" + context.Attached + " rotations=" + context.Rotations
                    + " offsets=" + context.Offsets + " protocol=SETUP-v" + context.Snapshot.Version);
            }
            finally
            {
                IDisposable disposable = original as IDisposable;
                if (disposable != null) disposable.Dispose();
            }
        }

        private static BlockData Attach(BlockData block)
        {
            if (!SetupPrecision.IsRegistered) return block;
            BlockData exact;
            if (current != null && current.Snapshot.Blocks.TryGetValue(SetupPrecisionCodec.Key(block), out exact))
            {
                Array.Copy(exact.actionID, block.actionID, 8);
                Array.Copy(exact.actionParam, block.actionParam, 8);
                SetupPrecisionData.Copy(block, exact);
                if (block.type == BlockData.AAHMDBHDCDK.Coupler) CouplerRotationOrder.Set(block, CouplerRotationOrder.Read(exact));
                current.Attached++;
            }
            return block;
        }

        private static bool InitialPose(NMLMDCCDFPN group, out SetupPrecisionCodec.Pose pose)
        {
            pose = default(SetupPrecisionCodec.Pose);
            return current != null && current.Machine != null
                && current.Snapshot.Poses.TryGetValue(current.Machine.BPKNDFJCENJ.IndexOf(group), out pose);
        }

        private static void InitialRotationPrefix(NMLMDCCDFPN __instance, ref float __0, ref float __1, ref float __2)
        {
            SetupPrecisionCodec.Pose pose;
            if (!InitialPose(__instance, out pose) || pose.Kind != 0 && pose.Kind != 15) return;
            __0 = pose.Rotation.x; __1 = pose.Rotation.y; __2 = pose.Rotation.z;
            current.Rotations++;
        }

        private static void InitialOffsetPrefix(NMLMDCCDFPN __instance, ref Vector3 __0)
        {
            SetupPrecisionCodec.Pose pose;
            if (!InitialPose(__instance, out pose) || pose.Kind != 1 && pose.Kind != 15) return;
            __0 = pose.Offset;
            current.Offsets++;
        }

        internal static IEnumerable<CodeInstruction> WriterTranspiler(IEnumerable<CodeInstruction> instructions)
        {
            return ReplaceCodec(instructions, Compress, "Pack");
        }

        private static IEnumerable<CodeInstruction> RpcTranspiler(IEnumerable<CodeInstruction> instructions)
        {
            return ReplaceCodec(instructions, Decompress, "Unpack");
        }

        private static IEnumerable<CodeInstruction> ReplaceCodec(IEnumerable<CodeInstruction> instructions, MethodInfo original, string replacement)
        {
            List<CodeInstruction> codes = Clone(instructions);
            int index = UniqueCall(codes, original);
            codes[index].operand = AccessTools.Method(typeof(SetupPrecisionNetwork), replacement);
            Insert(codes, index, new CodeInstruction(OpCodes.Ldarg_0));
            return codes;
        }

        internal static IEnumerable<CodeInstruction> ReaderTranspiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> codes = Clone(instructions);
            MethodInfo create = AccessTools.Method(typeof(PAEHEMJNPND), "PKLHNJNFKFH", new Type[] { typeof(BlockData), typeof(bool) });
            int index = UniqueCall(codes, create);
            if (index < 1 || codes[index - 1].opcode != OpCodes.Ldc_I4_0) throw new InvalidOperationException("SETUP prefab arguments changed");
            Insert(codes, index - 1, new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(SetupPrecisionNetwork), "Attach")));
            return codes;
        }

        private static void SerializePrefix(MachineSerializer __instance, BitStream __0, ref int ___JIBELECGPCC)
        {
            State state;
            if (__0.isWriting && States.TryGetValue(__instance, out state) && HasOutgoingMotion(state)) ___JIBELECGPCC = 0;
        }

        private static bool NativeSamplesPrefix(MachineSerializer __instance, ref int ___JIBELECGPCC)
        {
            State state;
            if (!States.TryGetValue(__instance, out state) || !state.Send) return true;
            ___JIBELECGPCC = 0;
            return false;
        }

        private static bool NativeApplyPrefix(MachineSerializer __instance)
        {
            State state;
            return !States.TryGetValue(__instance, out state) || !state.Receive;
        }

        private static void WriteMotion(MachineSerializer owner, BitStream stream)
        {
            State state;
            if (!States.TryGetValue(owner, out state) || !HasOutgoingMotion(state)) return;
            CollectMotion(state, state.Send ? owner.GetComponent<MachineController>() : null);
            int count = state.Samples.Count;
            stream.Serialize(ref count);
            foreach (SetupPrecisionCodec.Pose sample in state.Samples)
            {
                int header = sample.Index | sample.Kind << 8;
                stream.Serialize(ref header);
                if (sample.Kind == 2) continue;
                Vector3 value = sample.Kind == 0 ? sample.Rotation : sample.Offset;
                stream.Serialize(ref value);
            }
            MotionLog(state.Send ? "MOTION_WRITE" : "MOTION_RELAY", count, owner, state.Protocol, ref state.WriteLog);
        }

        private static bool HasOutgoingMotion(State state)
        {
            // Legacy invokes the writer on the server for remote-owned views too.
            return state.Send || state.Receive;
        }

        private static void ResetReceivedMotion(State state)
        {
            Array.Clear(state.HasReceived, 0, state.HasReceived.Length);
            Array.Clear(state.Sent, 0, state.Sent.Length);
            Array.Clear(state.Age, 0, state.Age.Length);
            state.Samples.Clear();
        }

        private static void RememberMotion(State state, SetupPrecisionCodec.Pose pose)
        {
            state.Received[pose.Index] = pose;
            state.HasReceived[pose.Index] = true;
        }

        private static void AddMotionSample(State state, SetupPrecisionCodec.Pose pose)
        {
            int index = pose.Index;
            if (state.Sent[index] && pose.Same(state.Last[index]) && ++state.Age[index] <= 100) return;
            state.Last[index] = pose; state.Sent[index] = true; state.Age[index] = 0;
            state.Samples.Add(pose);
        }

        private static void CollectMotion(State state, MachineController machine)
        {
            state.Samples.Clear();
            if (state.Send)
            {
                CaptureOwnedMotion(state, machine);
                return;
            }
            // Keep the owner's exact samples across reads. Several incoming
            // updates may arrive between server writes, and the server's
            // displayed transforms are already interpolated.
            if (state.Receive)
                for (int index = 0; index < state.Received.Length; index++)
                    if (state.HasReceived[index]) AddMotionSample(state, state.Received[index]);
        }

        private static void CaptureOwnedMotion(State state, MachineController machine)
        {
            for (int index = 0; index < Math.Min(machine.ILBAAENKMBL.Count - 1, 64); index++)
            {
                BodyController body = machine.ILBAAENKMBL[index + 1];
                Transform basis = machine.MMDCPMAKLPL[index];
                SetupPrecisionCodec.Pose pose = new SetupPrecisionCodec.Pose { Index = index, Kind = 2 };
                if (body != null && basis != null && body.NFMPBACKJOJ != null && !body.EINNGJBAMAP)
                {
                    if (body.ANAHNNNBKFC < 6 && body.NFMPBACKJOJ.GetComponent<HingeJoint>() != null)
                    {
                        pose.Kind = 0;
                        Matrix4x4 relative = basis.worldToLocalMatrix * body.transform.localToWorldMatrix;
                        pose.Rotation = SetupPrecisionCodec.Round(BDLEJBBJJOI.PJHDCFEPAOP(relative).eulerAngles, 1000);
                    }
                    else if (body.ANAHNNNBKFC >= 6 && body.NFMPBACKJOJ.GetComponent<ConfigurableJoint>() != null)
                    {
                        pose.Kind = 1;
                        int axis = body.ANAHNNNBKFC < 8 ? 0 : body.ANAHNNNBKFC < 10 ? 1 : 2;
                        pose.Offset[axis] = basis.worldToLocalMatrix.MultiplyPoint(body.transform.position)[axis];
                        pose.Offset = SetupPrecisionCodec.Round(pose.Offset, 100000);
                    }
                }
                if (!SetupPrecisionCodec.Valid(pose, true)) continue;
                AddMotionSample(state, pose);
            }
        }

        private static void ReadMotion(MachineSerializer owner, BitStream stream)
        {
            State state;
            if (!States.TryGetValue(owner, out state) || !state.Receive) return;
            int count = 0;
            stream.Serialize(ref count);
            if (count < 0 || count > 64) throw new InvalidDataException("SETUP motion count");
            MachineController machine = owner.GetComponent<MachineController>();
            for (int i = 0; i < count; i++)
            {
                int header = 0;
                stream.Serialize(ref header);
                SetupPrecisionCodec.Pose pose = new SetupPrecisionCodec.Pose { Index = header & 255, Kind = header >> 8 };
                if (pose.Kind == 0) stream.Serialize(ref pose.Rotation);
                else if (pose.Kind == 1) stream.Serialize(ref pose.Offset);
                else if (pose.Kind != 2) throw new InvalidDataException("SETUP motion kind");
                if (!SetupPrecisionCodec.Valid(pose, true)) throw new InvalidDataException("SETUP motion value");
                RememberMotion(state, pose);
                if (pose.Index >= machine.CFECMHAACID.Count) continue;
                NMLMDCCDFPN group = machine.CFECMHAACID[pose.Index];
                state.Groups.Add(group); PreciseGroups.Add(group);
                ApplyMotion(group, pose);
            }
            MotionLog("MOTION_READ", count, owner, state.Protocol, ref state.ReadLog);
        }

        private static void ApplyMotion(NMLMDCCDFPN group, SetupPrecisionCodec.Pose pose)
        {
            if (group.NGLBLAGMBLN == null || group.DBOFGDKGCBM == null) return;
            bool active = pose.Kind != 2;
            if (group.NGLBLAGMBLN.activeSelf != active) group.NGLBLAGMBLN.SetActive(active);
            if (group.DBOFGDKGCBM.activeSelf != active) group.DBOFGDKGCBM.SetActive(active);
            group.IDNIOMPGLOP = active ? Time.realtimeSinceStartup : -1;
            if (!active) return;
            if (pose.Kind == 0 && group.KMCBOCKBEAJ < 6)
            {
                float delta = group.FNBOELMCJON(pose.Rotation.x, pose.Rotation.y, pose.Rotation.z);
                group.GMJLIMJDGJC.x = 999f;
                group.HCDNAIOLMDM = delta > 0.1f ? 1f / (0.11f + group.LIBPIJNCIOB / delta) : 1f / 0.11f;
            }
            else if (pose.Kind == 1 && group.KMCBOCKBEAJ >= 6)
            {
                group.JFLGDNEILPL = pose.Offset;
                group.FFFBEMDLIJG.x = 999f;
            }
        }

        private static float ClampInterpolation(float value, NMLMDCCDFPN group)
        {
            // The original angular extrapolation otherwise stops beyond the last
            // received target, erasing the final hundredth even with an exact packet.
            return PreciseGroups.Contains(group) ? Mathf.Clamp01(value) : value;
        }

        private static IEnumerable<CodeInstruction> InterpolationTranspiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> codes = Clone(instructions);
            int index = UniqueCall(codes, AccessTools.Method(typeof(Quaternion), "SlerpUnclamped"));
            Insert(codes, index, new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(SetupPrecisionNetwork), "ClampInterpolation")));
            return codes;
        }

        private static IEnumerable<CodeInstruction> MotionTranspiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> codes = Clone(instructions);
            MethodInfo apply = AccessTools.Method(typeof(MachineSerializer), "ALCBIFFPNPJ");
            int read = UniqueCall(codes, apply);
            Insert(codes, read + 1, new CodeInstruction(OpCodes.Ldarg_0), new CodeInstruction(OpCodes.Ldarg_1),
                new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(SetupPrecisionNetwork), "ReadMotion")));
            int write = -1;
            for (int i = 0; i < codes.Count; i++)
                if (codes[i].opcode == OpCodes.Ldc_I4 && Equals(codes[i].operand, -67108864))
                {
                    // The writer's sentinel is followed by stloc; the reader's by a branch comparison.
                    if (i + 1 >= codes.Count || !codes[i + 1].opcode.Name.StartsWith("stloc", StringComparison.Ordinal)) continue;
                    for (int j = i + 2; j < i + 7 && j < codes.Count; j++)
                        if (codes[j].operand is MethodInfo && ((MethodInfo)codes[j].operand).Name == "Serialize") { write = j; break; }
                    break;
                }
            if (write < 0) throw new InvalidOperationException("SETUP motion sentinel changed");
            Insert(codes, write + 1, new CodeInstruction(OpCodes.Ldarg_0), new CodeInstruction(OpCodes.Ldarg_1),
                new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(SetupPrecisionNetwork), "WriteMotion")));
            return codes;
        }

        private static void DestroyPrefix(MachineSerializer __instance)
        {
            State state;
            if (!States.TryGetValue(__instance, out state)) return;
            foreach (NMLMDCCDFPN group in state.Groups) PreciseGroups.Remove(group);
            States.Remove(__instance);
        }

        private static void MotionLog(string name, int count, MachineSerializer owner, int protocol, ref int last)
        {
            if (count == 0) return;
            int now = Environment.TickCount;
            if (last != 0 && unchecked(now - last) < 10000) return;
            last = now;
            SetupPrecision.Log(name + " samples=" + count + " view=" + owner.GetComponent<NetworkView>().viewID
                + " rotationStep=0.001 positionStep=0.00001 protocol=SETUP-v" + protocol);
        }

        private static List<CodeInstruction> Clone(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> result = new List<CodeInstruction>();
            foreach (CodeInstruction code in instructions) result.Add(new CodeInstruction(code));
            return result;
        }

        private static int UniqueCall(List<CodeInstruction> codes, MethodInfo method)
        {
            if (method == null) throw new MissingMethodException("SETUP call target");
            int found = -1;
            for (int i = 0; i < codes.Count; i++)
                if ((codes[i].opcode == OpCodes.Call || codes[i].opcode == OpCodes.Callvirt) && Equals(codes[i].operand, method))
                {
                    if (found >= 0) throw new InvalidOperationException("Ambiguous SETUP call: " + method.Name);
                    found = i;
                }
            if (found < 0) throw new InvalidOperationException("Missing SETUP call: " + method.Name);
            return found;
        }

        private static void Insert(List<CodeInstruction> codes, int index, params CodeInstruction[] added)
        {
            added[0].labels.AddRange(codes[index].labels); codes[index].labels.Clear();
            added[0].blocks.AddRange(codes[index].blocks); codes[index].blocks.Clear();
            codes.InsertRange(index, added);
        }
    }
}
