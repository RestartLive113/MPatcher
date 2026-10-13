using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using dnlib.DotNet;
using UnityEngine;

// Runs the compiled MPatcher methods with disposable BlockData only. It does not
// create Unity objects, patch the game, install files, or claim multiplayer QA.
internal static class SetupPrecisionExpandedNetworkChecks
{
    private const BindingFlags All = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance;
    private static string references;
    private static Type blockType, dataType, codecType, snapshotType, poseType, networkType;
    private static int checks, sequence;

    private static object Call(Type type, string name, params object[] arguments)
    {
        MethodInfo method = type.GetMethod(name, All);
        if (method == null) throw new MissingMethodException(type.FullName, name);
        try { return method.Invoke(null, arguments); }
        catch (TargetInvocationException error) { throw error.InnerException; }
    }
    private static void Require(bool condition, string name)
    {
        if (!condition) throw new InvalidOperationException(name);
        checks++;
        Console.WriteLine("PASS " + name);
    }
    private static object Block(string name)
    {
        object block = Activator.CreateInstance(blockType);
        FieldInfo type = blockType.GetField("type");
        type.SetValue(block, Enum.Parse(type.FieldType, name));
        blockType.GetField("x").SetValue(block, sequence++ % 127);
        return block;
    }
    private static int[] Parameters(object block) { return (int[])blockType.GetField("actionParam").GetValue(block); }
    private static int[] Actions(object block) { return (int[])blockType.GetField("actionID").GetValue(block); }
    private static Dictionary<string, object> Props(object block)
    {
        return (Dictionary<string, object>)blockType.GetField("props").GetValue(block);
    }
    private static void Property(object block, string name, object value)
    {
        Dictionary<string, object> props = Props(block) ?? new Dictionary<string, object>();
        props[name] = value;
        blockType.GetField("props").SetValue(block, props);
    }
    private static bool Select(object block)
    {
        // Alpha0.3.9.14 Capture used HasAny directly. Keep that historical path
        // testable so the baseline fails on the lost-value case, not a missing API.
        MethodInfo selector = networkType.GetMethod("NeedsExactBlock", All);
        return selector == null ? (bool)Call(dataType, "HasAny", block) : (bool)Call(networkType, "NeedsExactBlock", block);
    }
    private static object Snapshot(params object[] blocks)
    {
        object snapshot = Activator.CreateInstance(snapshotType, true);
        IDictionary records = (IDictionary)snapshotType.GetField("Blocks", All).GetValue(snapshot);
        foreach (object block in blocks) records.Add(Call(codecType, "Key", block), block);
        return snapshot;
    }
    private static object Decode(byte[] packet, byte[] native)
    {
        object[] args = { packet, null };
        byte[] returned = (byte[])Call(codecType, "Decode", args);
        Require(returned.SequenceEqual(native), "native structure body preserved");
        return args[1];
    }
    private static object Lookup(object snapshot, object block)
    {
        return ((IDictionary)snapshotType.GetField("Blocks", All).GetValue(snapshot))[Call(codecType, "Key", block)];
    }
    private static bool SameArrays(object first, object second)
    {
        return Parameters(first).SequenceEqual(Parameters(second)) && Actions(first).SequenceEqual(Actions(second));
    }
    private static object NativeShapeRoundtrip(object block)
    {
        object native = Block(blockType.GetField("type").GetValue(block).ToString());
        foreach (string coordinate in new[] { "x", "y", "z" })
            blockType.GetField(coordinate).SetValue(native, blockType.GetField(coordinate).GetValue(block));
        Array.Copy(Parameters(block), Parameters(native), 8);
        Array.Copy(Actions(block), Actions(native), 8);
        int dimensions = blockType.GetField("type").GetValue(block).ToString() == "BoxGen" ? 3 : 2;
        for (int slot = 0; slot < dimensions; slot++) Parameters(native)[slot] &= 255;
        Parameters(native)[6] = unchecked((sbyte)(Parameters(block)[6] & 255));
        Parameters(native)[7] = unchecked((sbyte)(Parameters(block)[7] & 255));
        Actions(native)[7] = unchecked((short)(Actions(block)[7] & 65535));
        int packed = unchecked((Parameters(block)[3] + 500) | ((Parameters(block)[4] + 500) << 10) | ((Parameters(block)[5] + 500) << 20));
        for (int axis = 0; axis < 3; axis++) Parameters(native)[axis + 3] = ((packed >> (axis * 10)) & 1023) - 500;
        return native;
    }
    private static object Attach(object snapshot, object native)
    {
        Type precision = dataType.Assembly.GetType("MPatcherFork.CustomPatches.SetupPrecision", true);
        PropertyInfo registered = precision.GetProperty("IsRegistered", All);
        bool previousRegistered = (bool)registered.GetValue(null);
        FieldInfo current = networkType.GetField("current", All);
        object previousContext = current.GetValue(null);
        Type contextType = networkType.GetNestedType("ReadContext", All);
        object context = Activator.CreateInstance(contextType, true);
        contextType.GetField("Snapshot", All).SetValue(context, snapshot);
        try
        {
            registered.SetValue(null, true);
            current.SetValue(null, context);
            return Call(networkType, "Attach", native);
        }
        finally
        {
            current.SetValue(null, previousContext);
            registered.SetValue(null, previousRegistered);
        }
    }
    private static void CheckSelectorAndNativeLoss()
    {
        foreach (string type in new[] { "BoxGen", "CapGen" })
        {
            int dimensions = type == "BoxGen" ? 3 : 2;
            for (int slot = 0; slot < dimensions; slot++)
                foreach (int size in new[] { 0, 1, 250, 255, 256, 600, 1000 })
                {
                    object block = Block(type); Parameters(block)[slot] = size;
                    Require(Select(block) == (size > 255), type + " size lane" + slot + "=" + size + " selection follows unsigned byte");
                }
            for (int slot = 3; slot < 6; slot++)
                foreach (int offset in type == "BoxGen" ? new[] { -10000, -1000, -600, -501, -500, 0, 500, 501, 523, 524, 600, 1000, 10000 }
                    : new[] { -1000, -600, -501, -500, 0, 500, 501, 523, 524, 600, 1000 })
                {
                    object block = Block(type); Parameters(block)[slot] = offset;
                    bool loss = !SameArrays(block, NativeShapeRoundtrip(block));
                    Require(Select(block) == loss, type + " offset lane" + slot + "=" + offset + " selection matches packed roundtrip including cross-axis bleed");
                }
            for (int slot = 6; slot < 8; slot++)
                foreach (int angle in new[] { -180, -129, -128, 0, 127, 128, 180 })
                {
                    object block = Block(type); Parameters(block)[slot] = angle;
                    Require(Select(block) == (angle < -128 || angle > 127), type + " angle lane" + slot + "=" + angle + " selection follows signed byte");
                }
            foreach (int yaw in new[] { -32769, -32768, -180, 0, 180, 32767, 32768 })
            {
                object block = Block(type); Actions(block)[7] = yaw;
                Require(Select(block) == (yaw < -32768 || yaw > 32767), type + " yaw=" + yaw + " selection follows signed short");
            }
            object fractional = Block(type); Call(dataType, "Set", fractional, 3, 1.125f);
            Require(Select(fractional), type + " existing fractional metadata selected");
            object nativeShape = Block(type); Parameters(nativeShape)[0] = 255; Parameters(nativeShape)[3] = 523;
            Require(!Select(nativeShape), type + " native-fit endpoint size255/offset523 remains native");
        }
        for (int slot = 0; slot < 3; slot++)
            foreach (int offset in new[] { -10000, -501, -500, 0, 500, 501, 10000 })
            {
                object block = Block("Coupler"); Parameters(block)[slot] = offset;
                Require(Select(block) == (Math.Abs(offset) > 500), "Coupler expanded editor offset lane" + slot + "=" + offset + " selected without fractions");
            }
        Require(!Select(Block("Chassis")) && !Select(Block("Wheel")) && !Select(null), "unrelated and absent blocks keep native capture");
        using (ModuleDefMD module = ModuleDefMD.Load(dataType.Assembly.Location))
        {
            MethodDef capture = module.Find("MPatcherFork.CustomPatches.SetupPrecisionNetwork", false).FindMethod("Capture");
            Require(capture.Body.Instructions.Any(i => i.Operand is IMethod && ((IMethod)i.Operand).Name == "NeedsExactBlock"),
                "compiled Capture uses tested selector before record filtering");
        }
    }
    private static void CheckCodecAndAttach()
    {
        byte[] native = Enumerable.Range(1, 64).Select(i => (byte)i).ToArray();
        List<object> blocks = new List<object>();
        foreach (string type in new[] { "BoxGen", "CapGen" })
        {
            for (int slot = 3; slot < 6; slot++)
                foreach (int offset in type == "BoxGen" ? new[] { -10000, -600, 600, 10000 } : new[] { -600, 600 })
                {
                    object block = Block(type); Parameters(block)[slot] = offset; blocks.Add(block);
                }
            object large = Block(type);
            Parameters(large)[0] = 1000; Parameters(large)[1] = 600;
            if (type == "BoxGen") Parameters(large)[2] = 256;
            Parameters(large)[6] = 180; Parameters(large)[7] = -180; Actions(large)[7] = 90;
            blocks.Add(large);
            foreach (string detail in type == "BoxGen" ? new[] { "1|B|1", "1|B|2" }
                : new[] { "1|C|1", "1|C|2", "2|C|0|0", "2|C|1|0", "2|C|2|0" })
            {
                object block = Block(type); Actions(block)[0] = 0;
                Property(block, "shapeDetail", detail); blocks.Add(block);
                Require(Select(block), type + " canonical retained detail " + detail + " selected at whole values");
            }
        }
        object mixed = Block("BoxGen"); Parameters(mixed)[3] = 10000;
        Call(dataType, "Set", mixed, 0, 1.125f); Actions(mixed)[0] = 0;
        Property(mixed, "shapeDetail", "1|B|2"); blocks.Add(mixed);
        object coupler = Block("Coupler"); Parameters(coupler)[0] = 10000; Parameters(coupler)[1] = -10000;
        blocks.Add(coupler);
        object snapshot = Snapshot(blocks.ToArray());
        object pose = Activator.CreateInstance(poseType);
        poseType.GetField("Index", All).SetValue(pose, 1);
        poseType.GetField("Kind", All).SetValue(pose, 15);
        poseType.GetField("Rotation", All).SetValue(pose, new Vector3(0.001f, 45.125f, -89.999f));
        poseType.GetField("Offset", All).SetValue(pose, new Vector3(100f, -100f, 0.12345f));
        ((IDictionary)snapshotType.GetField("Poses", All).GetValue(snapshot)).Add(1, pose);
        byte[] packet = (byte[])Call(codecType, "Encode", native, snapshot);
        Require(BitConverter.ToInt32(packet, 4) == 3, "mixed integer/fraction/detail snapshot emits SETUP-v3");
        object read = Decode(packet, native);
        foreach (object original in blocks)
        {
            object returned = Lookup(read, original);
            Require(SameArrays(original, returned), "v3 full action arrays retained for " + blockType.GetField("type").GetValue(original));
            object destination = blockType.GetField("type").GetValue(original).ToString() == "Coupler" ? Block("Coupler") : NativeShapeRoundtrip(original);
            foreach (string coordinate in new[] { "x", "y", "z" })
                blockType.GetField(coordinate).SetValue(destination, blockType.GetField(coordinate).GetValue(original));
            Property(destination, "unrelated", "kept");
            Attach(read, destination);
            Require(SameArrays(original, destination) && Equals(Props(destination)["unrelated"], "kept"),
                "Attach restores native-corrupted arrays before geometry and keeps unrelated properties");
            Require((bool)Call(dataType, "Matches", destination, original, false), "Attach restores all exact fractional values");
            if (Props(original) != null && Props(original).ContainsKey("shapeDetail"))
                Require(Equals(Props(original)["shapeDetail"], Props(destination)["shapeDetail"]), "Attach preserves retained detail/family");
        }
        object readPose = ((IDictionary)snapshotType.GetField("Poses", All).GetValue(read))[1];
        Require((bool)poseType.GetMethod("Same", All).Invoke(pose, new[] { readPose }), "v3 static Coupler pose remains exact in world units");
        Require(((byte[])Call(codecType, "Encode", native, read)).SequenceEqual(packet), "v3 deterministic decode and re-encode");
        byte[] unextended = new byte[4];
        Require(ReferenceEquals(Call(codecType, "Encode", unextended, Snapshot()), unextended), "ordinary empty snapshot keeps identical native bytes");
        // Existing fractional-only records must use the same v2 format even when
        // their complete arrays also contain an expanded integer on another lane.
        object fraction = Block("BoxGen"); blockType.GetField("x").SetValue(fraction, 0);
        Parameters(fraction)[3] = 600; Call(dataType, "Set", fraction, 0, 1.125f);
        byte[] v2 = (byte[])Call(codecType, "Encode", unextended, Snapshot(fraction));
        Require(BitConverter.ToInt32(v2, 4) == 2, "fraction-only metadata keeps SETUP-v2 envelope");
        object v2Read = Decode(v2, unextended);
        Require(((byte[])Call(codecType, "Encode", unextended, v2Read)).SequenceEqual(v2), "v2 byte identity after decode/re-encode");
        string frozenV2 = Environment.GetEnvironmentVariable("MPATCHER_FROZEN_SETUP_V2");
        if (!string.IsNullOrEmpty(frozenV2)) Require(File.ReadAllBytes(frozenV2).SequenceEqual(v2), "v2 bytes identical to original Alpha0.3.9.14 fixture");
        CheckRejectedRecords(native, packet);
    }
    private static void Reject(Action operation, string name)
    {
        bool rejected = false;
        try { operation(); }
        catch (InvalidDataException) { rejected = true; }
        Require(rejected, name);
    }
    private static void CheckTransportMetadata()
    {
        using (ModuleDefMD game = ModuleDefMD.Load(Path.Combine(references, "Assembly-CSharp.dll")))
        using (ModuleDefMD patch = ModuleDefMD.Load(dataType.Assembly.Location))
        {
            TypeDef transport = game.Find("HNJDDKJLHMM", false).NestedTypes.Single(type => type.Name == "HKGAACMIPIH");
            int legacy = Convert.ToInt32(transport.Fields.Single(field => field.Name == "Legacy").Constant.Value);
            TypeDef network = patch.Find("MPatcherFork.CustomPatches.SetupPrecisionNetwork", false);
            foreach (string name in new[] { "NativeSamplesPrefix", "NativeApplyPrefix" })
            {
                var instructions = network.FindMethod(name).Body.Instructions;
                IField field = instructions[0].Operand as IField;
                bool equalBranch = instructions[2].OpCode.Code == dnlib.DotNet.Emit.Code.Beq || instructions[2].OpCode.Code == dnlib.DotNet.Emit.Code.Beq_S;
                Require(instructions[0].OpCode.Code == dnlib.DotNet.Emit.Code.Ldsfld
                    && field != null && field.DeclaringType.FullName == "HNJDDKJLHMM" && field.Name == "FHLGOMHPDLN"
                    && instructions[1].IsLdcI4() && instructions[1].GetLdcI4Value() == legacy
                    && equalBranch && ReferenceEquals(instructions[2].Operand, instructions[5])
                    && instructions[3].IsLdcI4() && instructions[3].GetLdcI4Value() == 1
                    && instructions[4].OpCode.Code == dnlib.DotNet.Emit.Code.Ret,
                    name + " returns native=true for non-Legacy transport before consulting precision state");
            }
            var pack = network.FindMethod("Pack").Body.Instructions;
            int encode = Enumerable.Range(0, pack.Count).Single(i => pack[i].Operand is IMethod
                && ((IMethod)pack[i].Operand).DeclaringType.FullName == codecType.FullName && ((IMethod)pack[i].Operand).Name == "Encode");
            int protocol = Enumerable.Range(0, pack.Count).First(i => pack[i].OpCode.Code == dnlib.DotNet.Emit.Code.Stfld
                && pack[i].Operand is IField && ((IField)pack[i].Operand).Name == "Protocol");
            Require(encode < protocol, "Pack records actual encoded envelope version after v2/v3 selection");
        }
    }
    private static void CheckMachineFixture()
    {
        string path = Environment.GetEnvironmentVariable("MPATCHER_SETUP_MACHINE_FIXTURE");
        if (string.IsNullOrEmpty(path)) return;
        using (JsonDocument document = JsonDocument.Parse(File.ReadAllText(path)))
        {
            List<object> blocks = new List<object>();
            foreach (JsonElement item in document.RootElement.GetProperty("blockData").EnumerateArray())
            {
                object block = Activator.CreateInstance(blockType);
                FieldInfo type = blockType.GetField("type");
                type.SetValue(block, Enum.ToObject(type.FieldType, item.GetProperty("type").GetInt32()));
                foreach (string coordinate in new[] { "x", "y", "z", "index", "rgbI", "gid", "press" })
                    blockType.GetField(coordinate).SetValue(block, item.GetProperty(coordinate).GetInt32());
                blockType.GetField("actionID").SetValue(block, item.GetProperty("actionID").EnumerateArray().Select(value => value.GetInt32()).ToArray());
                blockType.GetField("actionParam").SetValue(block, item.GetProperty("actionParam").EnumerateArray().Select(value => value.GetInt32()).ToArray());
                Require(item.GetProperty("props").ValueKind == JsonValueKind.Null, "NewMachine_17 source block uses native arrays and no props");
                blocks.Add(block);
            }
            Require(blocks.Count == 24 && blocks.All(block => !(bool)Call(dataType, "HasAny", block)),
                "NewMachine_17 concrete 24-block fixture has no fractional metadata");
            object[] selected = blocks.Where(Select).ToArray();
            Require(selected.Length == 3 && selected.Count(block => blockType.GetField("type").GetValue(block).ToString() == "BoxGen") == 2
                && selected.Count(block => blockType.GetField("type").GetValue(block).ToString() == "Coupler") == 1,
                "NewMachine_17 capture selects exactly two BOX and one Coupler despite whole-number-only values");
            object[] shapes = selected.Where(block => blockType.GetField("type").GetValue(block).ToString() == "BoxGen").ToArray();
            Require(shapes.All(block => Parameters(block)[1] == 1000) && shapes.Count(block => Parameters(block)[5] == 10000) == 1,
                "NewMachine_17 source BOX values include dimension1000 and offsetZ10000");
            object coupler = selected.Single(block => blockType.GetField("type").GetValue(block).ToString() == "Coupler");
            Require(Parameters(coupler)[2] == 10000, "NewMachine_17 source Coupler offsetZ10000 is captured independently");
            byte[] nativeBody = new byte[4];
            byte[] packet = (byte[])Call(codecType, "Encode", nativeBody, Snapshot(selected));
            Require(BitConverter.ToInt32(packet, 4) == 3, "NewMachine_17 whole-number-only snapshot emits SETUP-v3");
            object snapshot = Decode(packet, nativeBody);
            foreach (object source in selected)
                Require(SameArrays(source, Lookup(snapshot, source)), "NewMachine_17 exact source arrays survive envelope roundtrip");
            foreach (object source in shapes)
            {
                object native = NativeShapeRoundtrip(source);
                Require(Parameters(native)[1] == 232 && !SameArrays(source, native), "NewMachine_17 native wire narrows dimension1000 to232");
                if (Parameters(source)[5] == 10000)
                    Require(Parameters(native)[5] == -240, "NewMachine_17 native wire wraps offsetZ10000 to-240");
                Attach(snapshot, native);
                Require(SameArrays(source, native) && Parameters(native)[1] == 1000,
                    "NewMachine_17 actual Attach restores both BOX full arrays before geometry generation");
            }
            Require(((byte[])Call(codecType, "Encode", nativeBody, snapshot)).SequenceEqual(packet),
                "NewMachine_17 concrete v3 packet re-encodes deterministically");
            Console.WriteLine("MACHINE_FIXTURE_CHECKED=" + Path.GetFullPath(path) + " BLOCKS=24 SELECTED=3 GAME_RUNTIME_TESTED=False");
        }
    }
    private static void CheckRejectedRecords(byte[] native, byte[] valid)
    {
        Reject(() => Call(codecType, "Encode", native, Snapshot(Block("BoxGen"))), "empty ordinary integer record rejected");
        object unsupported = Block("Wheel"); Parameters(unsupported)[0] = 1000;
        Reject(() => Call(codecType, "Encode", native, Snapshot(unsupported)), "unsupported whole-record type rejected");
        foreach (int length in new[] { 4, 12, 20, valid.Length - 1 })
            Reject(() => Call(codecType, "Decode", new object[] { valid.Take(length).ToArray(), null }), "truncated v3 packet rejected at " + length);
        byte[] damaged = (byte[])valid.Clone(); damaged[damaged.Length / 2] ^= 128;
        Reject(() => Call(codecType, "Decode", new object[] { damaged, null }), "v3 checksum corruption rejected");
        // Make a valid checksum for semantically invalid detail, so rejection is
        // from validation of the record rather than merely the checksum guard.
        byte[] malformed = (byte[])valid.Clone();
        byte[] needle = Encoding.UTF8.GetBytes("1|B|1");
        int start = Enumerable.Range(0, malformed.Length - needle.Length).First(i => malformed.Skip(i).Take(needle.Length).SequenceEqual(needle));
        malformed[start + needle.Length - 1] = (byte)'9';
        uint checksum = 2166136261;
        for (int i = 0; i < malformed.Length - 4; i++) checksum = unchecked((checksum ^ malformed[i]) * 16777619);
        Array.Copy(BitConverter.GetBytes(checksum), 0, malformed, malformed.Length - 4, 4);
        Reject(() => Call(codecType, "Decode", new object[] { malformed, null }), "semantically invalid detail rejected despite valid checksum");
    }
    private static int Main(string[] args)
    {
        if (args.Length != 2) { Console.Error.WriteLine("Usage: SetupPrecisionExpandedNetworkChecks <references> <MPatchrMain.dll>"); return 2; }
        references = Path.GetFullPath(args[0]);
        AppDomain.CurrentDomain.AssemblyResolve += (sender, arguments) => {
            string path = Path.Combine(references, new AssemblyName(arguments.Name).Name + ".dll");
            return File.Exists(path) ? Assembly.LoadFrom(path) : null;
        };
        try
        {
            Assembly game = Assembly.LoadFrom(Path.Combine(references, "Assembly-CSharp.dll"));
            Assembly patch = Assembly.LoadFrom(Path.GetFullPath(args[1]));
            blockType = game.GetType("BlockData", true);
            dataType = patch.GetType("MPatcherFork.CustomPatches.SetupPrecisionData", true);
            codecType = patch.GetType("MPatcherFork.CustomPatches.SetupPrecisionCodec", true);
            snapshotType = codecType.GetNestedType("Snapshot", All);
            poseType = codecType.GetNestedType("Pose", All);
            networkType = patch.GetType("MPatcherFork.CustomPatches.SetupPrecisionNetwork", true);
            CheckMachineFixture();
            CheckSelectorAndNativeLoss();
            CheckCodecAndAttach();
            CheckTransportMetadata();
            Console.WriteLine("SETUP_EXPANDED_NETWORK_CHECKS=PASS COUNT=" + checks + " GAME_RUNTIME_TESTED=False");
            return 0;
        }
        catch (Exception error)
        {
            Console.Error.WriteLine("SETUP_EXPANDED_NETWORK_CHECKS=FAIL");
            Console.Error.WriteLine(error);
            return 1;
        }
    }
}
