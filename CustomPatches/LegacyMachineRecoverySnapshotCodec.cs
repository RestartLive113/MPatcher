using System;
using System.IO;
using System.Text;

namespace MPatcherFork.CustomPatches
{
	internal sealed class LegacyMachineRecoverySnapshot
	{
		internal int Generation;
		internal long Sequence;
		internal double CapturedAt;
		internal string Fingerprint;
		internal LegacyMachineRecoveryBodyState[] Bodies;
		internal LegacyMachineControlState Controls;
	}

	internal struct LegacyMachineRecoveryBodyState
	{
		internal float PositionX;
		internal float PositionY;
		internal float PositionZ;
		internal float RotationX;
		internal float RotationY;
		internal float RotationZ;
		internal float RotationW;
		internal float VelocityX;
		internal float VelocityY;
		internal float VelocityZ;
		internal float AngularX;
		internal float AngularY;
		internal float AngularZ;
		internal float Health;
		internal float BonusHealth;
		internal bool Broken;
		internal bool Suspended;
		internal bool IsKinematic;
		internal int SuspendFrames;
	}

	// The wire codec is deliberately independent from Unity so malformed or stale
	// recovery payloads can be rejected before any live machine is changed.
	internal static class LegacyMachineRecoverySnapshotCodec
	{
		internal const int ProtocolVersion = 1; // Existing body-only network protocol stays compatible.
		internal const int OwnerProtocolVersion = 2;
		internal const int MaximumBodies = 2048;
		internal const int MaximumBytes = 16 * 1024 * 1024;
		private const int Magic = 0x3152434D; // MCR1
		private const int MaximumFingerprintBytes = 128;

		internal static bool TryEncode(LegacyMachineRecoverySnapshot snapshot, out byte[] data, out string reason)
		{
			data = null;
			reason = null;
			if (!Validate(snapshot, out reason))
				return false;

			try
			{
				using (MemoryStream stream = new MemoryStream())
				using (BinaryWriter writer = new BinaryWriter(stream, Encoding.UTF8))
				{
					writer.Write(Magic);
					writer.Write(snapshot.Controls == null ? ProtocolVersion : OwnerProtocolVersion);
					writer.Write(snapshot.Generation);
					writer.Write(snapshot.Sequence);
					writer.Write(snapshot.CapturedAt);
					WriteString(writer, snapshot.Fingerprint);
					writer.Write(snapshot.Bodies.Length);
					for (int index = 0; index < snapshot.Bodies.Length; index++)
						WriteBody(writer, snapshot.Bodies[index]);
					if (snapshot.Controls != null) LegacyMachineControlStateCodec.Write(writer, snapshot.Controls);
					writer.Flush();
					if (stream.Length > MaximumBytes)
					{
						reason = "snapshot-too-large";
						return false;
					}
					data = stream.ToArray();
					return true;
				}
			}
			catch (Exception error)
			{
				reason = "encode-" + error.GetType().Name;
				return false;
			}
		}

		internal static bool TryDecode(byte[] data, out LegacyMachineRecoverySnapshot snapshot, out string reason)
		{
			snapshot = null;
			reason = null;
			if (data == null || data.Length == 0)
			{
				reason = "snapshot-empty";
				return false;
			}
			if (data.Length > MaximumBytes)
			{
				reason = "snapshot-too-large";
				return false;
			}

			try
			{
				using (MemoryStream stream = new MemoryStream(data, false))
				using (BinaryReader reader = new BinaryReader(stream, Encoding.UTF8))
				{
					if (reader.ReadInt32() != Magic)
					{
						reason = "bad-magic";
						return false;
					}
					int version = reader.ReadInt32();
					if (version != ProtocolVersion && version != OwnerProtocolVersion)
					{
						reason = "bad-version";
						return false;
					}

					LegacyMachineRecoverySnapshot candidate = new LegacyMachineRecoverySnapshot();
					candidate.Generation = reader.ReadInt32();
					candidate.Sequence = reader.ReadInt64();
					candidate.CapturedAt = reader.ReadDouble();
					candidate.Fingerprint = ReadString(reader);
					int bodyCount = reader.ReadInt32();
					if (bodyCount <= 0 || bodyCount > MaximumBodies)
					{
						reason = "bad-body-count";
						return false;
					}
					candidate.Bodies = new LegacyMachineRecoveryBodyState[bodyCount];
					for (int index = 0; index < bodyCount; index++)
						candidate.Bodies[index] = ReadBody(reader);
					if (version >= 2) candidate.Controls = LegacyMachineControlStateCodec.Read(reader);
					if (stream.Position != stream.Length)
					{
						reason = "trailing-data";
						return false;
					}
					if (!Validate(candidate, out reason))
						return false;
					snapshot = candidate;
					return true;
				}
			}
			catch (EndOfStreamException)
			{
				reason = "snapshot-truncated";
				return false;
			}
			catch (Exception error)
			{
				reason = "decode-" + error.GetType().Name;
				return false;
			}
		}

		private static bool Validate(LegacyMachineRecoverySnapshot snapshot, out string reason)
		{
			reason = null;
			if (snapshot == null)
			{
				reason = "snapshot-null";
				return false;
			}
			if (snapshot.Generation <= 0 || snapshot.Sequence < 0)
			{
				reason = "bad-generation";
				return false;
			}
			if (double.IsNaN(snapshot.CapturedAt) || double.IsInfinity(snapshot.CapturedAt))
			{
				reason = "bad-time";
				return false;
			}
			if (string.IsNullOrEmpty(snapshot.Fingerprint)
				|| Encoding.UTF8.GetByteCount(snapshot.Fingerprint) > MaximumFingerprintBytes)
			{
				reason = "bad-fingerprint";
				return false;
			}
			if (snapshot.Bodies == null || snapshot.Bodies.Length <= 0
				|| snapshot.Bodies.Length > MaximumBodies)
			{
				reason = "bad-body-count";
				return false;
			}
			for (int index = 0; index < snapshot.Bodies.Length; index++)
			{
				if (!ValidateBody(snapshot.Bodies[index]))
				{
					reason = "bad-body-" + index;
					return false;
				}
			}
			if (!LegacyMachineControlStateCodec.Validate(snapshot.Controls))
			{
				reason = "bad-controls";
				return false;
			}
			return true;
		}

		private static bool ValidateBody(LegacyMachineRecoveryBodyState body)
		{
			if (!Bounded(body.PositionX, 50000000f) || !Bounded(body.PositionY, 50000000f)
				|| !Bounded(body.PositionZ, 50000000f)) return false;
			if (!Bounded(body.RotationX, 2f) || !Bounded(body.RotationY, 2f)
				|| !Bounded(body.RotationZ, 2f) || !Bounded(body.RotationW, 2f)) return false;
			float rotationMagnitude = body.RotationX * body.RotationX + body.RotationY * body.RotationY
				+ body.RotationZ * body.RotationZ + body.RotationW * body.RotationW;
			if (rotationMagnitude < 0.000001f) return false;
			if (!Bounded(body.VelocityX, 1000000f) || !Bounded(body.VelocityY, 1000000f)
				|| !Bounded(body.VelocityZ, 1000000f)) return false;
			if (!Bounded(body.AngularX, 1000000f) || !Bounded(body.AngularY, 1000000f)
				|| !Bounded(body.AngularZ, 1000000f)) return false;
			if (!Bounded(body.Health, 10000000f) || !Bounded(body.BonusHealth, 10000000f)) return false;
			return body.SuspendFrames >= 0 && body.SuspendFrames <= 1000000;
		}

		private static bool Bounded(float value, float absoluteLimit)
		{
			return !float.IsNaN(value) && !float.IsInfinity(value)
				&& value >= -absoluteLimit && value <= absoluteLimit;
		}

		private static void WriteString(BinaryWriter writer, string value)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(value);
			writer.Write(bytes.Length);
			writer.Write(bytes);
		}

		private static string ReadString(BinaryReader reader)
		{
			int length = reader.ReadInt32();
			if (length <= 0 || length > MaximumFingerprintBytes)
				throw new InvalidDataException("Invalid fingerprint length.");
			byte[] bytes = reader.ReadBytes(length);
			if (bytes.Length != length)
				throw new EndOfStreamException();
			return Encoding.UTF8.GetString(bytes);
		}

		private static void WriteBody(BinaryWriter writer, LegacyMachineRecoveryBodyState body)
		{
			writer.Write(body.PositionX); writer.Write(body.PositionY); writer.Write(body.PositionZ);
			writer.Write(body.RotationX); writer.Write(body.RotationY); writer.Write(body.RotationZ); writer.Write(body.RotationW);
			writer.Write(body.VelocityX); writer.Write(body.VelocityY); writer.Write(body.VelocityZ);
			writer.Write(body.AngularX); writer.Write(body.AngularY); writer.Write(body.AngularZ);
			writer.Write(body.Health); writer.Write(body.BonusHealth);
			writer.Write(body.Broken); writer.Write(body.Suspended); writer.Write(body.IsKinematic);
			writer.Write(body.SuspendFrames);
		}

		private static LegacyMachineRecoveryBodyState ReadBody(BinaryReader reader)
		{
			LegacyMachineRecoveryBodyState body = new LegacyMachineRecoveryBodyState();
			body.PositionX = reader.ReadSingle(); body.PositionY = reader.ReadSingle(); body.PositionZ = reader.ReadSingle();
			body.RotationX = reader.ReadSingle(); body.RotationY = reader.ReadSingle();
			body.RotationZ = reader.ReadSingle(); body.RotationW = reader.ReadSingle();
			body.VelocityX = reader.ReadSingle(); body.VelocityY = reader.ReadSingle(); body.VelocityZ = reader.ReadSingle();
			body.AngularX = reader.ReadSingle(); body.AngularY = reader.ReadSingle(); body.AngularZ = reader.ReadSingle();
			body.Health = reader.ReadSingle(); body.BonusHealth = reader.ReadSingle();
			body.Broken = reader.ReadBoolean(); body.Suspended = reader.ReadBoolean(); body.IsKinematic = reader.ReadBoolean();
			body.SuspendFrames = reader.ReadInt32();
			return body;
		}
	}
}
