using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
    // The native arrays and FORM grid remain integers. Immutable, guarded metadata
    // carries thousandths through MPatcher's existing Clone/Set/JSON props support.
    internal static class SetupPrecisionData
    {
        internal const string PropertyName = "setupPrecision";
        internal const int Scale = 1000;
        internal const int Maximum = 500;
        internal const int MaxScaled = Maximum * Scale;
        private const int CurrentVersion = 2;

        internal static bool IsMechanism(BlockData block)
        {
            if (block == null) return false;
            switch (block.type)
            {
                case BlockData.AAHMDBHDCDK.JointTS: case BlockData.AAHMDBHDCDK.JointTA:
                case BlockData.AAHMDBHDCDK.JointPS: case BlockData.AAHMDBHDCDK.JointPA:
                case BlockData.AAHMDBHDCDK.JointBS: case BlockData.AAHMDBHDCDK.JointBA:
                case BlockData.AAHMDBHDCDK.PistonS: case BlockData.AAHMDBHDCDK.PistonL: return true;
                default: return false;
            }
        }

        internal static bool Supports(BlockData block, int slot)
        {
            if (block == null || slot < 0 || slot > 8 || block.actionParam == null || block.actionParam.Length < 8
                || block.actionID == null || block.actionID.Length < 8) return false;
            if (block.type == BlockData.AAHMDBHDCDK.BoxGen) return true;
            if (block.type == BlockData.AAHMDBHDCDK.CapGen) return slot != 2; // slot 2 is the shape selector.
            if (block.type == BlockData.AAHMDBHDCDK.Coupler) return slot < 6;
            if (!IsMechanism(block) || slot == 8) return false;
            int action = block.actionID[slot];
            return (action >= 0 && action < 62) || action == 65 || action == 66 || action == 70;
        }

        private static int Native(BlockData block, int slot)
        {
            return slot == 8 ? block.actionID[7] : block.actionParam[slot];
        }

        private static int Guard(BlockData block, int slot)
        {
            return IsMechanism(block) ? block.actionID[slot] : -1;
        }

        internal static bool IsSize(BlockData block, int slot)
        {
            return block != null && slot >= 0 && (block.type == BlockData.AAHMDBHDCDK.BoxGen && slot < 3
                || block.type == BlockData.AAHMDBHDCDK.CapGen && slot < 2);
        }

        internal static float SizeMinimum(BlockData block, int slot)
        {
            return slot == 1 && (block.type == BlockData.AAHMDBHDCDK.BoxGen || block.actionParam[2] == 0) ? 0f : 0.001f;
        }

        private static int Baseline(BlockData block, int slot, int scaled)
        {
            // A positive sub-unit size uses native 1 so CorrectError/AdjustParam
            // cannot clamp a zero placeholder and invalidate the saved fraction.
            return IsSize(block, slot) && scaled > 0 && scaled < Scale ? 1 : scaled / Scale;
        }

        internal static int Quantize(float value)
        {
            if (float.IsNaN(value) || float.IsInfinity(value) || Math.Abs(value) > MaxScaled / Scale)
                throw new ArgumentOutOfRangeException("value");
            return (int)Math.Round((double)value * Scale, MidpointRounding.AwayFromZero);
        }

        internal static string Format(float value) { return value.ToString("0.###", CultureInfo.InvariantCulture); }

        private static int[] Decode(BlockData block)
        {
            object raw;
            string data = block != null && block.props != null && block.props.TryGetValue(PropertyName, out raw) ? raw as string : null;
            return Decode(block, data);
        }

        private static int[] Decode(BlockData block, string data)
        {
            int[] values = new int[9];
            for (int i = 0; i < values.Length; i++) values[i] = int.MinValue;
            int version = MetadataVersion(data);
            if (version == 0 || data.Length > 512) return values;
            int storedScale = version == 1 ? 100 : Scale;
            string[] entries = data.Substring(2).Split('|');
            if (entries.Length > 9) return values;
            foreach (string entry in entries)
            {
                string[] parts = entry.Split(',');
                int slot, baseline, stored, guard;
                if (parts.Length != 4 || !Integer(parts[0], out slot) || !Integer(parts[1], out baseline)
                    || !Integer(parts[2], out stored) || !Integer(parts[3], out guard)
                    || !Supports(block, slot) || Math.Abs((long)stored) > Maximum * (long)storedScale) continue;
                int scaled = stored * (Scale / storedScale);
                if (scaled < -MaxScaled || scaled > MaxScaled
                    || Baseline(block, slot, scaled) != baseline || scaled % Scale == 0
                    || IsSize(block, slot) && (scaled < 0 || scaled > 250 * Scale)
                    || Native(block, slot) != baseline || Guard(block, slot) != guard) continue;
                values[slot] = scaled;
            }
            return values;
        }

        internal static int MetadataVersion(string data)
        {
            return data != null && data.StartsWith("1|", StringComparison.Ordinal) ? 1
                : data != null && data.StartsWith("2|", StringComparison.Ordinal) ? 2 : 0;
        }

        private static bool Integer(string text, out int value)
        {
            return int.TryParse(text, NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out value);
        }

        internal static float Read(BlockData block, int slot)
        {
            if (!Supports(block, slot) || block.props == null || !block.props.ContainsKey(PropertyName)) return Native(block, slot);
            int scaled = Decode(block)[slot];
            return scaled == int.MinValue ? Native(block, slot) : scaled / (float)Scale;
        }

        internal static float ReadActionAngle(BlockData block, int slot)
        {
            return slot == 7 && Supports(block, 8) ? Read(block, 8) : block.actionID[slot];
        }

        internal static Vector3 Angles(BlockData block)
        {
            return new Vector3(Read(block, 3), Read(block, 4), Read(block, 5));
        }

        internal static void SetAngles(BlockData block, Vector3 angles)
        {
            Set(block, 3, angles.x); Set(block, 4, angles.y); Set(block, 5, angles.z);
        }

        internal static bool Set(BlockData block, int slot, float value)
        {
            if (!Supports(block, slot)) return false;
            int scaled = Quantize(value);
            int[] values = Decode(block);
            int previous = values[slot] == int.MinValue ? Native(block, slot) * Scale : values[slot];
            if (previous == scaled && (block.props == null || !block.props.ContainsKey(PropertyName))) return false;
            // Truncation keeps FREE/STOP neighbouring integer sentinels out of a
            // fractional numeric interval (for example 100.99 must not become 101).
            if (IsSize(block, slot) && scaled % Scale != 0 && (scaled < 0 || scaled > 250 * Scale))
                throw new ArgumentOutOfRangeException("value");
            if (slot == 8) block.actionID[7] = Baseline(block, slot, scaled);
            else block.actionParam[slot] = Baseline(block, slot, scaled);
            values[slot] = scaled % Scale == 0 ? int.MinValue : scaled;
            Write(block, values);
            return previous != scaled;
        }

        private static string Encode(BlockData block, int[] values) { return Encode(block, values, CurrentVersion); }

        private static string Encode(BlockData block, int[] values, int version)
        {
            if (version != 1 && version != CurrentVersion) return null;
            StringBuilder data = new StringBuilder(version.ToString(CultureInfo.InvariantCulture));
            for (int slot = 0; slot < values.Length; slot++)
                if (values[slot] != int.MinValue && Supports(block, slot))
                {
                    if (version == 1 && values[slot] % 10 != 0) return null;
                    data.Append('|').Append(slot.ToString(CultureInfo.InvariantCulture)).Append(',')
                        .Append(Baseline(block, slot, values[slot]).ToString(CultureInfo.InvariantCulture)).Append(',')
                        .Append((version == 1 ? values[slot] / 10 : values[slot]).ToString(CultureInfo.InvariantCulture)).Append(',')
                        .Append(Guard(block, slot).ToString(CultureInfo.InvariantCulture));
                }
            return data.Length == 1 ? null : data.ToString();
        }

        internal static bool IsCanonical(BlockData block, string data)
        {
            int version = MetadataVersion(data);
            if (block == null || version == 0) return false;
            int[] values = Decode(block, data);
            string encoded = Encode(block, values, version);
            return encoded != null && encoded == data;
        }

        private static void Write(BlockData block, int[] values) { WriteEncoded(block, Encode(block, values)); }

        private static void WriteEncoded(BlockData block, string data)
        {
            Dictionary<string, object> props = block.props == null ? new Dictionary<string, object>() : new Dictionary<string, object>(block.props);
            if (data == null) props.Remove(PropertyName);
            else props[PropertyName] = data;
            block.props = props.Count == 0 ? null : props;
        }

        internal static void Prune(BlockData block)
        {
            object raw;
            if (block == null || block.props == null || !block.props.TryGetValue(PropertyName, out raw)) return;
            string canonical = Encode(block, Decode(block));
            if (!Equals(raw, canonical)) WriteEncoded(block, canonical);
        }

        internal static bool HasAny(BlockData block)
        {
            if (block == null || block.props == null || !block.props.ContainsKey(PropertyName)) return false;
            foreach (int value in Decode(block)) if (value != int.MinValue) return true;
            return false;
        }

        internal static bool Matches(BlockData first, BlockData second, bool offsetsOnly = false)
        {
            if (first == null || second == null || first.type != second.type) return false;
            for (int slot = 0; slot < 9; slot++)
                if ((!offsetsOnly || slot < 3) && Supports(first, slot) && Supports(second, slot)
                    && !SameValue(Read(first, slot), Read(second, slot))) return false;
            return true;
        }

        private static bool SameValue(float first, float second)
        {
            if (Math.Abs(first) > Maximum || Math.Abs(second) > Maximum) return first == second;
            return Quantize(first) == Quantize(second);
        }

        internal static bool Same(float first, float second) { return SameValue(first, second); }

        internal static void Copy(BlockData destination, BlockData source, bool offsetsOnly = false)
        {
            if (destination == null || source == null || destination.type != source.type || ReferenceEquals(destination, source)) return;
            for (int slot = 0; slot < 9; slot++)
                if ((!offsetsOnly || slot < 3) && Supports(source, slot) && Supports(destination, slot))
                {
                    float value = Read(source, slot);
                    if (Math.Abs(value) <= Maximum) Set(destination, slot, value);
                    else
                    {
                        if (slot == 8) destination.actionID[7] = (int)value;
                        else destination.actionParam[slot] = (int)value;
                        int[] values = Decode(destination);
                        values[slot] = int.MinValue;
                        Write(destination, values);
                    }
                }
        }

        internal static string Export(BlockData block)
        {
            if (!HasAny(block)) return null;
            BlockData clean = new BlockData();
            clean.type = block.type;
            Array.Copy(block.actionID, clean.actionID, 8);
            Array.Copy(block.actionParam, clean.actionParam, 8);
            Write(clean, Decode(block));
            return (string)clean.props[PropertyName];
        }

        internal static bool Import(BlockData block, string data)
        {
            if (block == null || data == null || data.Length > 512) return false;
            Dictionary<string, object> previous = block.props;
            block.props = previous == null ? new Dictionary<string, object>() : new Dictionary<string, object>(previous);
            block.props[PropertyName] = data;
            int[] values = Decode(block, data);
            bool valid = false;
            foreach (int value in values) valid |= value != int.MinValue;
            if (valid) Write(block, values);
            else block.props = previous;
            return valid;
        }
    }
}
