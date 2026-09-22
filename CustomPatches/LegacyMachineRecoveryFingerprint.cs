using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using McnCraft;

namespace MPatcherFork.CustomPatches
{
	// Produces the same construction identifier on the owner's machine and on
	// the server clone. Runtime transforms, health and Unity object identity are
	// deliberately excluded.
	internal static class LegacyMachineRecoveryFingerprint
	{
		private const int MaximumDepth = 24;
		// A valid 12,964-block craft already exceeds the old 200,000-node cap.
		// Keep a finite graph bound, with room for construction fields and actions.
		private const int MaximumItems = 4000000;
		private static readonly Dictionary<Type, TypeLayout> layouts = new Dictionary<Type, TypeLayout>();
		private static readonly object layoutLock = new object();

		internal static string ControlConfiguration(AssignData main, AssignData alternate,
			int mainMode, int alternateMode)
		{
			CanonicalHash hash = new CanonicalHash();
			hash.Write("MPatcher.ControlConfiguration.v1");
			hash.WriteObject(main, 0); hash.WriteObject(alternate, 0);
			hash.WriteObject(mainMode, 0); hash.WriteObject(alternateMode, 0);
			return hash.Value.ToString("X16", CultureInfo.InvariantCulture);
		}

		// Crash recovery owns only the local machine. The alternate globals describe
		// the currently selected remote/enemy machine and can legitimately change
		// across a reconnect even when the owner's construction is byte-for-byte equal.
		internal static string OwnerControlConfiguration(AssignData owner, int ownerMode)
		{
			CanonicalHash hash = new CanonicalHash();
			hash.Write("MPatcher.OwnerControlConfiguration.v1");
			hash.WriteObject(owner, 0);
			hash.WriteObject(ownerMode, 0);
			return hash.Value.ToString("X16", CultureInfo.InvariantCulture);
		}

		internal static bool TryCompute(MachineController machine, out string fingerprint, out string reason)
		{
			fingerprint = null;
			reason = null;
			if (machine == null || machine.HHGILAIOCLG == null || machine.MIIGKEBFKKD == null)
			{
				reason = "construction-not-ready";
				return false;
			}
			try
			{
				fingerprint = ComputeData(machine.HHGILAIOCLG, machine.MIIGKEBFKKD);
				return true;
			}
			catch (Exception error)
			{
				reason = "fingerprint-" + error.GetType().Name + ":" + error.Message;
				return false;
			}
		}

		internal static string ComputeData(BuildData build, AssignData assign)
		{
			if (build == null || assign == null) throw new ArgumentNullException("construction");
			CanonicalHash hash = new CanonicalHash();
			hash.Write("MPatcher.LegacyMachineRecovery.Fingerprint.v1");
			hash.WriteObject(build, 0);
			hash.WriteObject(assign, 0);
			return hash.Value.ToString("X16", CultureInfo.InvariantCulture);
		}

		private sealed class FieldLayout
		{
			internal FieldInfo Field;
			internal byte[] Name;
		}

		private sealed class TypeLayout
		{
			internal byte[] Name;
			internal FieldLayout[] Fields;
		}

		private static TypeLayout GetLayout(Type type)
		{
			lock (layoutLock)
			{
				TypeLayout layout;
				if (layouts.TryGetValue(type, out layout)) return layout;
				FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public);
				Array.Sort(fields, delegate(FieldInfo left, FieldInfo right)
				{
					return string.CompareOrdinal(left.Name, right.Name);
				});
				layout = new TypeLayout();
				layout.Name = Encoding.UTF8.GetBytes(type.FullName);
				layout.Fields = new FieldLayout[fields.Length];
				for (int i = 0; i < fields.Length; i++)
					layout.Fields[i] = new FieldLayout { Field = fields[i], Name = Encoding.UTF8.GetBytes(fields[i].Name) };
				layouts.Add(type, layout);
				return layout;
			}
		}

		private sealed class CanonicalHash
		{
			private const ulong Offset = 14695981039346656037UL;
			private const ulong Prime = 1099511628211UL;
			private readonly List<object> stack = new List<object>();
			private int itemCount;
			internal ulong Value = Offset;

			internal void WriteObject(object value, int depth)
			{
				if (depth > MaximumDepth) throw new InvalidOperationException("Object graph is too deep.");
				if (++itemCount > MaximumItems) throw new InvalidOperationException("Object graph is too large.");
				if (value == null)
				{
					WriteByte(0);
					return;
				}

				Type type = value.GetType();
				if (type == typeof(string)) { WriteByte(1); Write((string)value); return; }
				if (type == typeof(bool)) { WriteByte(2); WriteByte((bool)value ? (byte)1 : (byte)0); return; }
				if (type.IsEnum) { WriteByte(3); WriteText(GetLayout(type).Name); Write(Convert.ToInt64(value, CultureInfo.InvariantCulture)); return; }
				if (type == typeof(byte)) { WriteByte(4); WriteByte((byte)value); return; }
				if (type == typeof(sbyte)) { WriteByte(5); WriteByte(unchecked((byte)(sbyte)value)); return; }
				if (type == typeof(short)) { WriteByte(6); Write((long)(short)value); return; }
				if (type == typeof(ushort)) { WriteByte(7); Write((ulong)(ushort)value); return; }
				if (type == typeof(int)) { WriteByte(8); Write((long)(int)value); return; }
				if (type == typeof(uint)) { WriteByte(9); Write((ulong)(uint)value); return; }
				if (type == typeof(long)) { WriteByte(10); Write((long)value); return; }
				if (type == typeof(ulong)) { WriteByte(11); Write((ulong)value); return; }
				if (type == typeof(float)) { WriteByte(12); WriteBytes(BitConverter.GetBytes((float)value)); return; }
				if (type == typeof(double)) { WriteByte(13); WriteBytes(BitConverter.GetBytes((double)value)); return; }
				if (type == typeof(decimal)) { WriteByte(14); int[] bits = decimal.GetBits((decimal)value); for (int i = 0; i < bits.Length; i++) Write((long)bits[i]); return; }
				if (type == typeof(char)) { WriteByte(15); Write((ulong)(char)value); return; }

				if (!type.IsValueType && ContainsReference(value))
					throw new InvalidOperationException("Cyclic construction data.");
				if (!type.IsValueType) stack.Add(value);
				try
				{
					TypeLayout layout = GetLayout(type);
					IDictionary dictionary = value as IDictionary;
					if (dictionary != null)
					{
						WriteByte(16);
						WriteText(layout.Name);
						List<DictionaryEntry> entries = new List<DictionaryEntry>();
						foreach (DictionaryEntry entry in dictionary) entries.Add(entry);
						entries.Sort(delegate(DictionaryEntry left, DictionaryEntry right)
						{
							return string.CompareOrdinal(KeyText(left.Key), KeyText(right.Key));
						});
						Write((long)entries.Count);
						for (int i = 0; i < entries.Count; i++)
						{
							WriteObject(entries[i].Key, depth + 1);
							WriteObject(entries[i].Value, depth + 1);
						}
						return;
					}

					IEnumerable enumerable = value as IEnumerable;
					if (enumerable != null)
					{
						WriteByte(17);
						WriteText(layout.Name);
						ICollection collection = value as ICollection;
						Write((long)(collection == null ? -1 : collection.Count));
						foreach (object item in enumerable) WriteObject(item, depth + 1);
						return;
					}

					WriteByte(18);
					WriteText(layout.Name);
					FieldLayout[] fields = layout.Fields;
					Write((long)fields.Length);
					for (int i = 0; i < fields.Length; i++)
					{
						WriteText(fields[i].Name);
						WriteObject(fields[i].Field.GetValue(value), depth + 1);
					}
				}
				finally
				{
					if (!type.IsValueType) stack.RemoveAt(stack.Count - 1);
				}
			}

			internal void Write(string value)
			{
				if (value == null) { Write(-1L); return; }
				byte[] bytes = Encoding.UTF8.GetBytes(value);
				WriteText(bytes);
			}

			private void WriteText(byte[] bytes)
			{
				Write((long)bytes.Length);
				WriteBytes(bytes);
			}

			private static string KeyText(object value)
			{
				if (value == null) return string.Empty;
				string text = value as string;
				return text ?? Convert.ToString(value, CultureInfo.InvariantCulture);
			}

			private bool ContainsReference(object value)
			{
				for (int i = 0; i < stack.Count; i++)
					if (ReferenceEquals(stack[i], value)) return true;
				return false;
			}

			private void Write(long value) { Write(unchecked((ulong)value)); }
			private void Write(ulong value)
			{
				// Preserve the v1 byte stream without allocating an eight-byte array
				// for every number and string length in every construction block.
				if (BitConverter.IsLittleEndian)
					for (int i = 0; i < 8; i++) WriteByte((byte)(value >> (i * 8)));
				else
					for (int i = 7; i >= 0; i--) WriteByte((byte)(value >> (i * 8)));
			}

			private void WriteBytes(byte[] bytes)
			{
				for (int i = 0; i < bytes.Length; i++) WriteByte(bytes[i]);
			}

			private void WriteByte(byte value)
			{
				unchecked
				{
					Value ^= value;
					Value *= Prime;
				}
			}
		}
	}
}
