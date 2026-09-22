using System;
using System.IO;

namespace MPatcherFork.CustomPatches
{
	// Local checkpoint extension. No Unity objects, key-down events or script VM references.
	internal sealed class LegacyMachineControlState
	{
		internal string Layout;
		internal int Frame;
		internal ulong MainToggles, AlternateToggles;
		internal ulong MainMask, AlternateMask;
		internal bool AlternateActive;
		internal int[] MainToggleKeys, AlternateToggleKeys, AltHeldKeys;
		internal LegacyControlPartState[] Parts;
	}

	internal sealed class LegacyControlPartState
	{
		internal bool Available;
		internal float[] Floats;
		internal int[] Integers;
		internal ulong Flags;
		internal LegacyControlActionState[] Actions;
		internal int JointKind, JointFlags;
		internal float[] JointValues;
	}

	internal struct LegacyControlActionState
	{
		internal int Id, Phase, State, Repeats;
		internal float Delay;
		internal bool Flag;
	}

	internal static class LegacyMachineControlStateCodec
	{
		internal const int MaximumParts = 32768;
		internal const ulong ToggleBits = (1UL << 60) - 1;
		internal const ulong ActionBits = (1UL << 62) - 1;

		internal static void Write(BinaryWriter writer, LegacyMachineControlState state)
		{
			writer.Write(state != null);
			if (state == null) return;
			writer.Write(1); // Extension schema, independent of the existing network protocol.
			writer.Write(state.Layout); writer.Write(state.Frame);
			writer.Write(state.MainToggles); writer.Write(state.AlternateToggles);
			writer.Write(state.MainMask); writer.Write(state.AlternateMask);
			writer.Write(state.AlternateActive);
			WriteKeys(writer, state.MainToggleKeys); WriteKeys(writer, state.AlternateToggleKeys);
			WriteKeys(writer, state.AltHeldKeys);
			writer.Write(state.Parts.Length);
			foreach (LegacyControlPartState part in state.Parts)
			{
				writer.Write(part.Available);
				if (!part.Available) continue;
				WriteFloats(writer, part.Floats);
				writer.Write(part.Integers.Length);
				foreach (int value in part.Integers) writer.Write(value);
				writer.Write(part.Flags);
				writer.Write(part.Actions.Length);
				foreach (LegacyControlActionState action in part.Actions)
				{
					writer.Write(action.Id); writer.Write(action.Phase); writer.Write(action.State);
					writer.Write(action.Repeats); writer.Write(action.Delay); writer.Write(action.Flag);
				}
				writer.Write(part.JointKind); writer.Write(part.JointFlags);
				WriteFloats(writer, part.JointValues);
			}
		}

		internal static LegacyMachineControlState Read(BinaryReader reader)
		{
			if (!ReadBool(reader)) return null;
			if (reader.ReadInt32() != 1) throw new InvalidDataException("control-version");
			// SHA256 in hex: fixed wire length prevents unbounded BinaryReader.ReadString allocation.
			if (reader.ReadByte() != 64) throw new InvalidDataException("control-layout-length");
			byte[] hash = reader.ReadBytes(64);
			if (hash.Length != 64) throw new EndOfStreamException();
			LegacyMachineControlState state = new LegacyMachineControlState();
			state.Layout = System.Text.Encoding.ASCII.GetString(hash);
			state.Frame = reader.ReadInt32();
			state.MainToggles = reader.ReadUInt64(); state.AlternateToggles = reader.ReadUInt64();
			state.MainMask = reader.ReadUInt64(); state.AlternateMask = reader.ReadUInt64();
			state.AlternateActive = ReadBool(reader);
			state.MainToggleKeys = ReadKeys(reader); state.AlternateToggleKeys = ReadKeys(reader);
			state.AltHeldKeys = ReadKeys(reader);
			state.Parts = new LegacyControlPartState[Count(reader, MaximumParts)];
			for (int i = 0; i < state.Parts.Length; i++)
			{
				LegacyControlPartState part = new LegacyControlPartState();
				state.Parts[i] = part;
				part.Available = ReadBool(reader);
				if (!part.Available) continue;
				part.Floats = ReadFloats(reader, 64);
				part.Integers = new int[Count(reader, 32)];
				for (int j = 0; j < part.Integers.Length; j++) part.Integers[j] = reader.ReadInt32();
				part.Flags = reader.ReadUInt64();
				part.Actions = new LegacyControlActionState[Count(reader, 9)];
				for (int j = 0; j < part.Actions.Length; j++)
				{
					LegacyControlActionState action = new LegacyControlActionState();
					action.Id = reader.ReadInt32(); action.Phase = reader.ReadInt32();
					action.State = reader.ReadInt32(); action.Repeats = reader.ReadInt32();
					action.Delay = reader.ReadSingle(); action.Flag = ReadBool(reader);
					part.Actions[j] = action;
				}
				part.JointKind = reader.ReadInt32(); part.JointFlags = reader.ReadInt32();
				part.JointValues = ReadFloats(reader, 64);
			}
			return state;
		}

		internal static bool Validate(LegacyMachineControlState state)
		{
			if (state == null) return true; // v1/body-only coverage is explicitly reported by the caller.
			if (state.Layout == null || state.Layout.Length != 64 || state.Frame < 0) return false;
			foreach (char c in state.Layout) if (!((c >= '0' && c <= '9') || (c >= 'A' && c <= 'F'))) return false;
			if ((state.MainToggles & ~ToggleBits) != 0 || (state.AlternateToggles & ~ToggleBits) != 0
				|| (state.MainMask & ~ActionBits) != 0 || (state.AlternateMask & ~ActionBits) != 0) return false;
			if (!ValidKeys(state.MainToggleKeys) || !ValidKeys(state.AlternateToggleKeys) || !ValidKeys(state.AltHeldKeys)
				|| state.Parts == null || state.Parts.Length > MaximumParts) return false;
			foreach (LegacyControlPartState part in state.Parts)
			{
				if (part == null) return false;
				if (!part.Available) continue;
				if (!ValidFloats(part.Floats, 64) || part.Integers == null || part.Integers.Length > 32
					|| part.Actions == null || part.Actions.Length > 9) return false;
				ulong ids = 0;
				foreach (LegacyControlActionState action in part.Actions)
				{
					if (action.Id < 0 || action.Id > 61 || (ids & (1UL << action.Id)) != 0
						|| action.Phase < -1000000 || action.Phase > 1000000000
						|| action.State < 0 || action.State > 4 || action.Repeats < 0 || action.Repeats > 1000000
						|| !Finite(action.Delay) || action.Delay < -1000000f || action.Delay > 1000000f) return false;
					ids |= 1UL << action.Id;
				}
				int length = part.JointKind == 0 || part.JointKind == 3 || part.JointKind == 4 ? 0
					: part.JointKind == 1 ? 26 : part.JointKind == 2 ? 19 : -1;
				if (!ValidFloats(part.JointValues, 64) || part.JointValues.Length != length
					|| part.JointFlags < 0 || part.JointFlags > 511) return false;
				if (part.JointKind == 1 && (part.JointFlags > 7 || (part.JointValues[23] != 0f && part.JointValues[23] != 1f))) return false;
				if (part.JointKind == 2 && (part.JointFlags > 42 || (part.JointFlags & 3) > 2
					|| ((part.JointFlags >> 2) & 3) > 2 || ((part.JointFlags >> 4) & 3) > 2)) return false;
			}
			return true;
		}

		private static bool Finite(float value) { return !float.IsNaN(value) && !float.IsInfinity(value); }
		private static bool ValidFloats(float[] values, int max)
		{
			if (values == null || values.Length > max) return false;
			foreach (float value in values) if (!Finite(value)) return false;
			return true;
		}
		private static bool ValidKeys(int[] keys)
		{
			if (keys == null || keys.Length > 512) return false;
			int previous = 0;
			foreach (int key in keys) { if (key <= previous || key > 512) return false; previous = key; }
			return true;
		}
		private static int Count(BinaryReader reader, int maximum)
		{
			int count = reader.ReadInt32();
			if (count < 0 || count > maximum) throw new InvalidDataException("control-count");
			return count;
		}
		private static bool ReadBool(BinaryReader reader)
		{
			byte value = reader.ReadByte();
			if (value > 1) throw new InvalidDataException("control-bool");
			return value != 0;
		}
		private static void WriteKeys(BinaryWriter writer, int[] keys)
		{
			writer.Write(keys.Length); foreach (int key in keys) writer.Write(key);
		}
		private static int[] ReadKeys(BinaryReader reader)
		{
			int[] keys = new int[Count(reader, 512)];
			for (int i = 0; i < keys.Length; i++) keys[i] = reader.ReadInt32();
			return keys;
		}
		private static void WriteFloats(BinaryWriter writer, float[] values)
		{
			writer.Write(values.Length); foreach (float value in values) writer.Write(value);
		}
		private static float[] ReadFloats(BinaryReader reader, int maximum)
		{
			float[] values = new float[Count(reader, maximum)];
			for (int i = 0; i < values.Length; i++) values[i] = reader.ReadSingle();
			return values;
		}
	}

	// The output-mask half of native LHIDPININIM/JBGPBPIMJOJ, evaluated with latched
	// keys only. Never synthesize GetKeyDown or replay toggle edges during recovery.
	internal static class LegacyPersistentAssignMask
	{
		internal static ulong Build(ulong toggles, int[] first, int[] second, int[][] conflicts,
			bool exclusive, int[] toggleKeys, int[] altKeys)
		{
			ulong result = toggles & LegacyMachineControlStateCodec.ToggleBits;
			bool moving = false, movingChord = false;
			for (int i = 0; i < 10; i++)
			{
				if (first[i] == 0) continue;
				if ((result & (1UL << i)) != 0) moving = true;
				else if (Held(first[i], toggleKeys, altKeys))
				{
					if (second[i] == 0) moving = true;
					else if (Held(second[i], toggleKeys, altKeys)) movingChord = moving = true;
				}
			}
			for (int i = 0; i < 62; i++)
			{
				if (first[i] == 0) continue;
				if (i == 61 && moving)
				{
					bool allow = true;
					if (exclusive && !movingChord && conflicts[i] != null)
						for (int j = 0; j < conflicts[i].Length; j += 2)
							if (Held(conflicts[i][j], toggleKeys, altKeys) && Held(conflicts[i][j + 1], toggleKeys, altKeys)) allow = false;
					if (allow) result |= 1UL << i;
				}
				else if (i < 60 && Held(first[i], toggleKeys, altKeys))
				{
					bool allow = second[i] == 0 || Held(second[i], toggleKeys, altKeys);
					if (second[i] == 0 && exclusive && conflicts[i] != null)
						foreach (int key in conflicts[i]) if (Held(key, toggleKeys, altKeys)) allow = false;
					if (allow) result |= 1UL << i;
				}
			}
			return result;
		}
		private static bool Held(int key, int[] toggleKeys, int[] altKeys)
		{
			return key != 0 && (Array.BinarySearch(toggleKeys, key) >= 0 || Array.BinarySearch(altKeys, key) >= 0);
		}
	}
}
