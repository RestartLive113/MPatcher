using System;
using System.IO;
using System.Text;

namespace MPatcherFork.CustomPatches
{
	// Keeps the owner's full physical-body snapshot beside the active-session
	// marker. The server replica only has a reduced body/visual representation,
	// so it cannot be used to rebuild the returning owner's articulated machine.
	internal static class LegacyCrashOwnerCheckpoint
	{
		internal const string FileName = "legacy-owner-checkpoint-v1.bin";
		private const uint Magic = 0x314f434d; // MCO1
		private const int Version = 1;
		private const int MaximumTargetBytes = 4096;
		private const int MaximumEnvelopeBytes = LegacyMachineRecoverySnapshotCodec.MaximumBytes + 8192;

		internal static bool Save(string token, string target,
			LegacyMachineRecoverySnapshot snapshot, out int bytes, out string reason)
		{
			bytes = 0;
			reason = null;
			byte[] payload;
			if (!LegacyMachineRecoverySnapshotCodec.TryEncode(snapshot, out payload, out reason))
			{
				reason = "snapshot-" + reason;
				return false;
			}
			byte[] envelope;
			if (!TryEncodeEnvelope(token, target, DateTime.UtcNow.Ticks, payload,
				out envelope, out reason)) return false;
			try
			{
				string path = CheckpointPath();
				string directory = Path.GetDirectoryName(path);
				Directory.CreateDirectory(directory);
				string temporary = path + ".tmp";
				string backup = path + ".bak";
				File.WriteAllBytes(temporary, envelope);
				if (File.Exists(path))
				{
					if (File.Exists(backup)) File.Delete(backup);
					try { File.Replace(temporary, path, backup); }
					catch
					{
						if (File.Exists(path)) File.Delete(path);
						File.Move(temporary, path);
					}
				}
				else File.Move(temporary, path);
				if (File.Exists(backup)) File.Delete(backup);
				bytes = envelope.Length;
				return true;
			}
			catch (Exception error)
			{
				reason = "checkpoint-write-" + error.GetType().Name;
				return false;
			}
		}

		internal static bool TryLoad(string token, string target,
			out LegacyMachineRecoverySnapshot snapshot, out long capturedUtcTicks,
			out int bytes, out string reason)
		{
			snapshot = null;
			capturedUtcTicks = 0;
			bytes = 0;
			reason = null;
			try
			{
				string path = CheckpointPath();
				string[] candidates = new string[] { path, path + ".tmp", path + ".bak" };
				DateTime newest = DateTime.MinValue;
				string lastReason = "checkpoint-missing";
				for (int index = 0; index < candidates.Length; index++)
				{
					string candidate = candidates[index];
					if (!File.Exists(candidate)) continue;
					if (new FileInfo(candidate).Length > MaximumEnvelopeBytes)
					{
						lastReason = "checkpoint-envelope-size";
						continue;
					}
					byte[] envelope = File.ReadAllBytes(candidate);
					string storedToken;
					string storedTarget;
					long storedTicks;
					byte[] payload;
					string decodeReason;
					if (!TryDecodeEnvelope(envelope, out storedToken, out storedTarget,
						out storedTicks, out payload, out decodeReason))
					{
						lastReason = decodeReason;
						continue;
					}
					if (!string.Equals(storedToken, token, StringComparison.OrdinalIgnoreCase)
						|| !string.Equals(storedTarget, target, StringComparison.Ordinal))
					{
						lastReason = "checkpoint-session-mismatch";
						continue;
					}
					LegacyMachineRecoverySnapshot decoded;
					if (!LegacyMachineRecoverySnapshotCodec.TryDecode(payload, out decoded,
						out decodeReason))
					{
						lastReason = "snapshot-" + decodeReason;
						continue;
					}
					DateTime writeTime = File.GetLastWriteTimeUtc(candidate);
					if (snapshot != null && writeTime <= newest) continue;
					newest = writeTime;
					snapshot = decoded;
					capturedUtcTicks = storedTicks;
					bytes = envelope.Length;
				}
				if (snapshot != null) return true;
				reason = lastReason;
				return false;
			}
			catch (Exception error)
			{
				reason = "checkpoint-read-" + error.GetType().Name;
				return false;
			}
		}

		internal static bool Clear(out string reason)
		{
			reason = null;
			try
			{
				string path = CheckpointPath();
				bool existed = false;
				string[] candidates = new string[] { path, path + ".tmp", path + ".bak" };
				for (int index = 0; index < candidates.Length; index++)
					if (File.Exists(candidates[index]))
					{
						existed = true;
						File.Delete(candidates[index]);
					}
				reason = existed ? "cleared" : "already-clear";
				return true;
			}
			catch (Exception error)
			{
				reason = "checkpoint-delete-" + error.GetType().Name;
				return false;
			}
		}

		internal static bool TryEncodeEnvelope(string token, string target,
			long capturedUtcTicks, byte[] payload, out byte[] data, out string reason)
		{
			data = null;
			reason = null;
			if (!LegacyTransientReconnect.ValidToken(token) || string.IsNullOrEmpty(target)
				|| Encoding.UTF8.GetByteCount(target) > MaximumTargetBytes
				|| capturedUtcTicks <= 0 || payload == null || payload.Length <= 0
				|| payload.Length > LegacyMachineRecoverySnapshotCodec.MaximumBytes)
			{
				reason = "checkpoint-envelope-values";
				return false;
			}
			try
			{
				using (MemoryStream stream = new MemoryStream())
				using (BinaryWriter writer = new BinaryWriter(stream, Encoding.UTF8))
				{
					writer.Write(Magic);
					writer.Write(Version);
					writer.Write(capturedUtcTicks);
					WriteString(writer, token.ToLowerInvariant());
					WriteString(writer, target);
					writer.Write(payload.Length);
					writer.Write(payload);
					writer.Flush();
					byte[] withoutChecksum = stream.ToArray();
					writer.Write(Checksum(withoutChecksum, withoutChecksum.Length));
					writer.Flush();
					if (stream.Length > MaximumEnvelopeBytes)
					{
						reason = "checkpoint-envelope-too-large";
						return false;
					}
					data = stream.ToArray();
					return true;
				}
			}
			catch (Exception error)
			{
				reason = "checkpoint-envelope-encode-" + error.GetType().Name;
				return false;
			}
		}

		internal static bool TryDecodeEnvelope(byte[] data, out string token,
			out string target, out long capturedUtcTicks, out byte[] payload,
			out string reason)
		{
			token = null;
			target = null;
			capturedUtcTicks = 0;
			payload = null;
			reason = null;
			if (data == null || data.Length < 64 || data.Length > MaximumEnvelopeBytes)
			{
				reason = "checkpoint-envelope-size";
				return false;
			}
			uint expected = BitConverter.ToUInt32(data, data.Length - 4);
			if (expected != Checksum(data, data.Length - 4))
			{
				reason = "checkpoint-envelope-checksum";
				return false;
			}
			try
			{
				using (MemoryStream stream = new MemoryStream(data, 0, data.Length - 4, false))
				using (BinaryReader reader = new BinaryReader(stream, Encoding.UTF8))
				{
					if (reader.ReadUInt32() != Magic) { reason = "checkpoint-envelope-magic"; return false; }
					if (reader.ReadInt32() != Version) { reason = "checkpoint-envelope-version"; return false; }
					capturedUtcTicks = reader.ReadInt64();
					token = ReadString(reader, 64);
					target = ReadString(reader, MaximumTargetBytes);
					int length = reader.ReadInt32();
					if (capturedUtcTicks <= 0 || !LegacyTransientReconnect.ValidToken(token)
						|| string.IsNullOrEmpty(target) || length <= 0
						|| length > LegacyMachineRecoverySnapshotCodec.MaximumBytes
						|| length > stream.Length - stream.Position)
					{
						reason = "checkpoint-envelope-values";
						return false;
					}
					payload = reader.ReadBytes(length);
					if (payload.Length != length || stream.Position != stream.Length)
					{
						reason = "checkpoint-envelope-trailing-data";
						return false;
					}
					return true;
				}
			}
			catch (EndOfStreamException)
			{
				reason = "checkpoint-envelope-truncated";
				return false;
			}
			catch (Exception error)
			{
				reason = "checkpoint-envelope-decode-" + error.GetType().Name;
				return false;
			}
		}

		private static string CheckpointPath()
		{
			return Path.Combine(Path.Combine(Path.Combine(Directory.GetCurrentDirectory(),
				"UserData"), "_mpatcher"), FileName);
		}

		private static void WriteString(BinaryWriter writer, string value)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(value);
			writer.Write(bytes.Length);
			writer.Write(bytes);
		}

		private static string ReadString(BinaryReader reader, int maximumBytes)
		{
			int length = reader.ReadInt32();
			if (length <= 0 || length > maximumBytes) throw new InvalidDataException("string-length");
			byte[] bytes = reader.ReadBytes(length);
			if (bytes.Length != length) throw new EndOfStreamException();
			return Encoding.UTF8.GetString(bytes);
		}

		private static uint Checksum(byte[] data, int length)
		{
			uint hash = 2166136261u;
			for (int index = 0; index < length; index++)
				hash = unchecked((hash ^ data[index]) * 16777619u);
			return hash;
		}
	}
}
