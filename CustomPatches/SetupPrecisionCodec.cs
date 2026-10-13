using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
    internal static class SetupPrecisionCodec
    {
        private const uint Magic = 0x5053504d; // MPSP, followed by a separate version.
        internal const int CurrentVersion = 3;
        private const int PrecisionVersion = 2;
        private const int MaximumMetadataBytes = 512;
        private const int MaximumDetailBytes = 32;
        private const int MaximumBytes = 16 * 1024 * 1024;
        internal const int MaximumRecords = 65535;

        internal sealed class Snapshot
        {
            internal int Version;
            internal readonly Dictionary<int, BlockData> Blocks = new Dictionary<int, BlockData>();
            internal readonly Dictionary<int, Pose> Poses = new Dictionary<int, Pose>();
        }

        internal struct Pose
        {
            internal int Index;
            internal int Kind; // 0 = rotation, 1 = piston offset, 2 = hidden, 15 = static Coupler.
            internal Vector3 Rotation;
            internal Vector3 Offset;

            internal bool Same(Pose other)
            {
                return Index == other.Index && Kind == other.Kind && Rotation.x == other.Rotation.x && Rotation.y == other.Rotation.y
                    && Rotation.z == other.Rotation.z && Offset.x == other.Offset.x && Offset.y == other.Offset.y && Offset.z == other.Offset.z;
            }
        }

        internal static int Key(BlockData block)
        {
            // The native structure packet uses signed bytes for FORM coordinates.
            return (int)block.type << 24 | (block.x & 255) << 16 | (block.y & 255) << 8 | block.z & 255;
        }

        internal static byte[] Encode(byte[] native, Snapshot snapshot)
        {
            if (snapshot == null || snapshot.Blocks.Count == 0) return native;
            if (native == null || native.Length > MaximumBytes || native.Length % 4 != 0
                || snapshot.Blocks.Count > MaximumRecords || snapshot.Poses.Count > MaximumRecords)
                throw new InvalidDataException("SETUP structure size");
            // Keep historical fractional-only packets byte-for-byte identical.
            // The envelope version is independent from the props metadata version.
            List<int> keys = new List<int>(snapshot.Blocks.Keys); keys.Sort();
            Dictionary<int, string> metadata = new Dictionary<int, string>();
            Dictionary<int, string> details = new Dictionary<int, string>();
            int version = PrecisionVersion;
            foreach (int key in keys)
            {
                BlockData block = snapshot.Blocks[key];
                if (block == null || block.actionID == null || block.actionID.Length < 8
                    || block.actionParam == null || block.actionParam.Length < 8 || Key(block) != key)
                    throw new InvalidDataException("SETUP block arrays/key");
                string precision = SetupPrecisionData.Export(block);
                string detail = ExportDetail(block);
                if (precision != null && (SetupPrecisionData.MetadataVersion(precision) != PrecisionVersion
                    || !SetupPrecisionData.IsCanonical(block, precision)))
                    throw new InvalidDataException("SETUP block metadata");
                if (precision == null && (!ArrayOnlyType(block)
                    || !SetupPrecisionData.NeedsNativeExtension(block) && detail == null))
                    throw new InvalidDataException("Unnecessary/unsupported SETUP array-only block");
                metadata.Add(key, precision);
                details.Add(key, detail);
                if (precision == null || detail != null) version = CurrentVersion;
            }
            using (MemoryStream stream = new MemoryStream())
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                snapshot.Version = version;
                writer.Write(Magic); writer.Write(version); writer.Write(native.Length); writer.Write(native);
                writer.Write(snapshot.Blocks.Count);
                foreach (int key in keys)
                {
                    BlockData block = snapshot.Blocks[key];
                    writer.Write(key);
                    for (int i = 0; i < 8; i++) { writer.Write(block.actionID[i]); writer.Write(block.actionParam[i]); }
                    writer.Write((byte)CouplerRotationOrder.Read(block));
                    WriteText(writer, metadata[key], MaximumMetadataBytes);
                    if (version == CurrentVersion) WriteText(writer, details[key], MaximumDetailBytes);
                }
                writer.Write(snapshot.Poses.Count);
                keys = new List<int>(snapshot.Poses.Keys); keys.Sort();
                foreach (int index in keys)
                {
                    Pose pose = snapshot.Poses[index];
                    if (pose.Index != index || !Valid(pose, false)) throw new InvalidDataException("SETUP initial pose");
                    writer.Write(pose.Index); writer.Write(pose.Kind);
                    WriteVector(writer, pose.Rotation); WriteVector(writer, pose.Offset);
                }
                if (stream.Length > MaximumBytes) throw new InvalidDataException("SETUP envelope too large");
                byte[] bytes = stream.ToArray();
                writer.Write(Checksum(bytes, bytes.Length));
                return stream.ToArray();
            }
        }

        internal static byte[] Decode(byte[] packet, out Snapshot snapshot)
        {
            snapshot = null;
            if (packet == null) throw new InvalidDataException("Missing structure");
            if (packet.Length < 4 || BitConverter.ToUInt32(packet, 0) != Magic) return packet;
            if (packet.Length < 24 || packet.Length > MaximumBytes + 4
                || BitConverter.ToUInt32(packet, packet.Length - 4) != Checksum(packet, packet.Length - 4))
                throw new InvalidDataException("SETUP envelope length/checksum");
            using (MemoryStream stream = new MemoryStream(packet, false))
            using (BinaryReader reader = new BinaryReader(stream))
            {
                reader.ReadUInt32();
                int version = reader.ReadInt32();
                if (version != 1 && version != PrecisionVersion && version != CurrentVersion)
                    throw new InvalidDataException("Unsupported SETUP protocol");
                int length = Count(reader, MaximumBytes, 1);
                if (length < 4 || length % 4 != 0) throw new InvalidDataException("SETUP native body length");
                byte[] native = reader.ReadBytes(length);
                Snapshot result = new Snapshot { Version = version };
                int count = Count(reader, MaximumRecords, version == CurrentVersion ? 73 : 72);
                if (count == 0) throw new InvalidDataException("Empty SETUP envelope");
                for (int record = 0; record < count; record++)
                {
                    EnsureEnough(reader, 69);
                    int key = reader.ReadInt32();
                    BlockData block = new BlockData();
                    block.type = (BlockData.AAHMDBHDCDK)((key >> 24) & 255);
                    block.x = (sbyte)((key >> 16) & 255); block.y = (sbyte)((key >> 8) & 255); block.z = (sbyte)(key & 255);
                    for (int i = 0; i < 8; i++) { block.actionID[i] = reader.ReadInt32(); block.actionParam[i] = reader.ReadInt32(); }
                    int order = reader.ReadByte();
                    string data = ReadText(reader, MaximumMetadataBytes, version == CurrentVersion);
                    string detail = version == CurrentVersion ? ReadText(reader, MaximumDetailBytes, true) : null;
                    int precisionVersion = version == 1 ? 1 : PrecisionVersion;
                    if (!CouplerRotationOrder.IsValid(order)
                        || block.type != BlockData.AAHMDBHDCDK.Coupler && order != CouplerRotationOrder.Default
                        || data != null && (SetupPrecisionData.MetadataVersion(data) != precisionVersion
                            || !SetupPrecisionData.IsCanonical(block, data) || !SetupPrecisionData.Import(block, data))
                        || result.Blocks.ContainsKey(key))
                        throw new InvalidDataException("Invalid/duplicate SETUP block");
                    if (detail != null)
                    {
                        if (block.props == null) block.props = new Dictionary<string, object>();
                        block.props[ShapeDetailRetention.PropertyName] = detail;
                        if (!ShapeDetailRetention.HasRetainedDetail(block))
                            throw new InvalidDataException("Invalid SETUP shape detail");
                    }
                    if (data == null && (version != CurrentVersion || !ArrayOnlyType(block)
                        || !SetupPrecisionData.NeedsNativeExtension(block) && detail == null))
                        throw new InvalidDataException("Unnecessary/unsupported SETUP array-only block");
                    if (block.type == BlockData.AAHMDBHDCDK.Coupler) CouplerRotationOrder.Set(block, order);
                    result.Blocks.Add(key, block);
                }
                count = Count(reader, MaximumRecords, 32);
                for (int record = 0; record < count; record++)
                {
                    EnsureEnough(reader, 32);
                    Pose pose = new Pose { Index = reader.ReadInt32(), Kind = reader.ReadInt32(), Rotation = ReadVector(reader), Offset = ReadVector(reader) };
                    if (!Valid(pose, false) || result.Poses.ContainsKey(pose.Index)) throw new InvalidDataException("Invalid/duplicate SETUP pose");
                    result.Poses.Add(pose.Index, pose);
                }
                if (stream.Position != stream.Length - 4) throw new InvalidDataException("SETUP trailing bytes");
                snapshot = result;
                return native;
            }
        }

        private static bool ArrayOnlyType(BlockData block)
        {
            return block.type == BlockData.AAHMDBHDCDK.BoxGen || block.type == BlockData.AAHMDBHDCDK.CapGen
                || block.type == BlockData.AAHMDBHDCDK.Coupler;
        }

        private static string ExportDetail(BlockData block)
        {
            object value;
            return ShapeDetailRetention.HasRetainedDetail(block) && block.props != null
                && block.props.TryGetValue(ShapeDetailRetention.PropertyName, out value) ? value as string : null;
        }

        private static void WriteText(BinaryWriter writer, string value, int maximum)
        {
            byte[] bytes = value == null ? new byte[0] : Encoding.UTF8.GetBytes(value);
            if (bytes.Length > maximum || bytes.Length == 1) throw new InvalidDataException("SETUP text length");
            writer.Write((ushort)bytes.Length); writer.Write(bytes);
        }

        private static string ReadText(BinaryReader reader, int maximum, bool optional)
        {
            EnsureEnough(reader, 2);
            int length = reader.ReadUInt16();
            if (length > maximum || length < 2 && !(optional && length == 0)
                || length > reader.BaseStream.Length - reader.BaseStream.Position - 4)
                throw new InvalidDataException("SETUP text length");
            return length == 0 ? null : Encoding.UTF8.GetString(reader.ReadBytes(length));
        }

        private static int Count(BinaryReader reader, int maximum, int minimumBytes)
        {
            EnsureEnough(reader, 4);
            int count = reader.ReadInt32();
            if (count < 0 || count > maximum || (long)count * minimumBytes > reader.BaseStream.Length - reader.BaseStream.Position - 4)
                throw new InvalidDataException("SETUP record count");
            return count;
        }

        private static void EnsureEnough(BinaryReader reader, int bytes)
        {
            if (reader.BaseStream.Length - reader.BaseStream.Position - 4 < bytes)
                throw new InvalidDataException("Truncated SETUP record");
        }

        internal static bool Valid(Pose pose, bool motion)
        {
            return pose.Index >= 0 && pose.Index < (motion ? 64 : MaximumRecords)
                && (pose.Kind == 0 || pose.Kind == 1 || pose.Kind == 2 || !motion && pose.Kind == 15)
                && Finite(pose.Rotation, 720f) && Finite(pose.Offset, 100000f);
        }

        private static bool Finite(Vector3 vector, float bound)
        {
            return !float.IsNaN(vector.x) && !float.IsNaN(vector.y) && !float.IsNaN(vector.z)
                && Math.Abs(vector.x) <= bound && Math.Abs(vector.y) <= bound && Math.Abs(vector.z) <= bound;
        }

        internal static Vector3 Round(Vector3 vector, int scale)
        {
            return new Vector3((float)(Math.Round((double)vector.x * scale) / scale),
                (float)(Math.Round((double)vector.y * scale) / scale), (float)(Math.Round((double)vector.z * scale) / scale));
        }

        private static void WriteVector(BinaryWriter writer, Vector3 vector) { writer.Write(vector.x); writer.Write(vector.y); writer.Write(vector.z); }
        private static Vector3 ReadVector(BinaryReader reader) { return new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle()); }
        private static uint Checksum(byte[] bytes, int length)
        {
            uint hash = 2166136261;
            for (int i = 0; i < length; i++) hash = unchecked((hash ^ bytes[i]) * 16777619);
            return hash;
        }
    }
}
