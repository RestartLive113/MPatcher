using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MPatcherFork.CustomPatches
{
	internal sealed class LegacyGhostPoseSnapshot
	{
		internal int SnapshotId;
		internal string Fingerprint;
		internal LegacyGhostPoseBodyState[] Bodies;
		internal LegacyGhostPoseVisualState[] Visuals;
	}

	internal struct LegacyGhostPoseBodyState
	{
		internal int Index;
		internal float PositionX;
		internal float PositionY;
		internal float PositionZ;
		internal float RotationX;
		internal float RotationY;
		internal float RotationZ;
		internal float RotationW;
		internal float Health;
		internal float BonusHealth;
		internal bool Broken;
		internal bool Suspended;
		internal int SuspendFrames;
	}

	internal struct LegacyGhostPoseTransformState
	{
		internal float PositionX;
		internal float PositionY;
		internal float PositionZ;
		internal float RotationX;
		internal float RotationY;
		internal float RotationZ;
		internal float RotationW;
		internal float ScaleX;
		internal float ScaleY;
		internal float ScaleZ;
	}

	internal struct LegacyGhostPoseAuxiliaryState
	{
		internal int Index;
		internal float ScaleX;
		internal float ScaleY;
		internal float ScaleZ;
	}

	internal struct LegacyGhostPoseVisualState
	{
		internal int Index;
		internal bool PrimaryPresent;
		internal LegacyGhostPoseTransformState Primary;
		internal bool SecondaryPresent;
		internal LegacyGhostPoseTransformState Secondary;
		internal LegacyGhostPoseAuxiliaryState[] Auxiliaries;
	}

	internal static class LegacyGhostPoseSnapshotCodec
	{
		internal const int ProtocolVersion = 3;
		// Root LoD group precedes the indexed CFECMHAACID sections. It is their
		// coordinate frame and is not itself a member of that native list.
		internal const int RootVisualIndex = -1;
		internal const int MaximumBodies = 2048;
		internal const int MaximumVisuals = 4096;
		internal const int MaximumAuxiliaryTransforms = 8192;
		internal const int MaximumBytes = 512 * 1024;
		private const uint Magic = 0x3250474d; // MGP2
		private const int MaximumFingerprintBytes = 128;

		internal static bool TryEncode(LegacyGhostPoseSnapshot snapshot, out byte[] data, out string reason)
		{
			data = null;
			if (!Validate(snapshot, out reason)) return false;
			try
			{
				using (MemoryStream stream = new MemoryStream())
				using (BinaryWriter writer = new BinaryWriter(stream, Encoding.UTF8))
				{
					writer.Write(Magic);
					writer.Write(ProtocolVersion);
					writer.Write(snapshot.SnapshotId);
					WriteString(writer, snapshot.Fingerprint);
					writer.Write(snapshot.Bodies.Length);
					for (int index = 0; index < snapshot.Bodies.Length; index++)
						WriteBody(writer, snapshot.Bodies[index]);
					writer.Write(snapshot.Visuals.Length);
					for (int index = 0; index < snapshot.Visuals.Length; index++)
						WriteVisual(writer, snapshot.Visuals[index]);
					writer.Flush();
					byte[] withoutChecksum = stream.ToArray();
					writer.Write(Checksum(withoutChecksum, withoutChecksum.Length));
					writer.Flush();
					if (stream.Length > MaximumBytes)
					{
						reason = "ghost-pose-snapshot-too-large";
						return false;
					}
					data = stream.ToArray();
					return true;
				}
			}
			catch (Exception error)
			{
				reason = "ghost-pose-encode-" + error.GetType().Name;
				return false;
			}
		}

		internal static bool TryDecode(byte[] data, out LegacyGhostPoseSnapshot snapshot, out string reason)
		{
			snapshot = null;
			reason = null;
			if (data == null || data.Length < 28 || data.Length > MaximumBytes)
			{
				reason = "ghost-pose-snapshot-size";
				return false;
			}
			uint expected = BitConverter.ToUInt32(data, data.Length - 4);
			if (expected != Checksum(data, data.Length - 4))
			{
				reason = "ghost-pose-checksum";
				return false;
			}
			try
			{
				using (MemoryStream stream = new MemoryStream(data, 0, data.Length - 4, false))
				using (BinaryReader reader = new BinaryReader(stream, Encoding.UTF8))
				{
					if (reader.ReadUInt32() != Magic) { reason = "ghost-pose-magic"; return false; }
					if (reader.ReadInt32() != ProtocolVersion) { reason = "ghost-pose-version"; return false; }
					LegacyGhostPoseSnapshot candidate = new LegacyGhostPoseSnapshot();
					candidate.SnapshotId = reader.ReadInt32();
					candidate.Fingerprint = ReadString(reader);
					int bodyCount = reader.ReadInt32();
					if (bodyCount <= 0 || bodyCount > MaximumBodies
						|| (long)bodyCount * 48 > stream.Length - stream.Position)
					{
						reason = "ghost-pose-body-count";
						return false;
					}
					candidate.Bodies = new LegacyGhostPoseBodyState[bodyCount];
					for (int index = 0; index < bodyCount; index++) candidate.Bodies[index] = ReadBody(reader);
					int visualCount = reader.ReadInt32();
					if (visualCount < 0 || visualCount > MaximumVisuals
						|| (long)visualCount * 12 > stream.Length - stream.Position)
					{
						reason = "ghost-pose-visual-count";
						return false;
					}
					candidate.Visuals = new LegacyGhostPoseVisualState[visualCount];
					for (int index = 0; index < visualCount; index++) candidate.Visuals[index] = ReadVisual(reader);
					if (stream.Position != stream.Length) { reason = "ghost-pose-trailing-data"; return false; }
					if (!Validate(candidate, out reason)) return false;
					snapshot = candidate;
					return true;
				}
			}
			catch (EndOfStreamException)
			{
				reason = "ghost-pose-truncated";
				return false;
			}
			catch (Exception error)
			{
				reason = "ghost-pose-decode-" + error.GetType().Name;
				return false;
			}
		}

		private static bool Validate(LegacyGhostPoseSnapshot snapshot, out string reason)
		{
			reason = null;
			if (snapshot == null || snapshot.SnapshotId <= 0)
			{
				reason = "ghost-pose-snapshot-id";
				return false;
			}
			if (string.IsNullOrEmpty(snapshot.Fingerprint)
				|| Encoding.UTF8.GetByteCount(snapshot.Fingerprint) > MaximumFingerprintBytes)
			{
				reason = "ghost-pose-fingerprint";
				return false;
			}
			if (snapshot.Bodies == null || snapshot.Bodies.Length <= 0
				|| snapshot.Bodies.Length > MaximumBodies)
			{
				reason = "ghost-pose-body-count";
				return false;
			}
			HashSet<int> bodyIndices = new HashSet<int>();
			for (int index = 0; index < snapshot.Bodies.Length; index++)
			{
				LegacyGhostPoseBodyState body = snapshot.Bodies[index];
				if (body.Index < 0 || body.Index > 65535 || !bodyIndices.Add(body.Index)
					|| !Bounded(body.PositionX, 50000000f) || !Bounded(body.PositionY, 50000000f)
					|| !Bounded(body.PositionZ, 50000000f) || !Bounded(body.RotationX, 2f)
					|| !Bounded(body.RotationY, 2f) || !Bounded(body.RotationZ, 2f)
					|| !Bounded(body.RotationW, 2f) || !Bounded(body.Health, 10000000f)
					|| !Bounded(body.BonusHealth, 10000000f) || body.SuspendFrames < 0
					|| body.SuspendFrames > 1000000 || !RotationValid(body.RotationX,
						body.RotationY, body.RotationZ, body.RotationW))
				{
					reason = "ghost-pose-body-" + index;
					return false;
				}
			}
			if (snapshot.Visuals == null || snapshot.Visuals.Length > MaximumVisuals)
			{
				reason = "ghost-pose-visual-count";
				return false;
			}
			HashSet<int> visualIndices = new HashSet<int>();
			int auxiliaryCount = 0;
			for (int index = 0; index < snapshot.Visuals.Length; index++)
			{
				LegacyGhostPoseVisualState visual = snapshot.Visuals[index];
				if (visual.Index < RootVisualIndex || visual.Index > 65535
					|| visual.Index == RootVisualIndex && index != 0 || !visualIndices.Add(visual.Index)
					|| !visual.PrimaryPresent && !visual.SecondaryPresent
					|| visual.PrimaryPresent && !TransformValid(visual.Primary)
					|| visual.SecondaryPresent && !TransformValid(visual.Secondary)
					|| visual.Auxiliaries == null)
				{
					reason = "ghost-pose-visual-" + index;
					return false;
				}
				auxiliaryCount += visual.Auxiliaries.Length;
				if (auxiliaryCount > MaximumAuxiliaryTransforms)
				{
					reason = "ghost-pose-auxiliary-count";
					return false;
				}
				HashSet<int> auxiliaryIndices = new HashSet<int>();
				for (int auxiliaryIndex = 0; auxiliaryIndex < visual.Auxiliaries.Length; auxiliaryIndex++)
				{
					LegacyGhostPoseAuxiliaryState auxiliary = visual.Auxiliaries[auxiliaryIndex];
					if (auxiliary.Index < 0 || auxiliary.Index > 65535
						|| !auxiliaryIndices.Add(auxiliary.Index) || !ScaleValid(auxiliary.ScaleX,
							auxiliary.ScaleY, auxiliary.ScaleZ))
					{
						reason = "ghost-pose-visual-" + index + "-auxiliary-" + auxiliaryIndex;
						return false;
					}
				}
			}
			return true;
		}

		private static bool TransformValid(LegacyGhostPoseTransformState state)
		{
			return Bounded(state.PositionX, 50000000f) && Bounded(state.PositionY, 50000000f)
				&& Bounded(state.PositionZ, 50000000f) && Bounded(state.RotationX, 2f)
				&& Bounded(state.RotationY, 2f) && Bounded(state.RotationZ, 2f)
				&& Bounded(state.RotationW, 2f) && RotationValid(state.RotationX,
					state.RotationY, state.RotationZ, state.RotationW)
				&& ScaleValid(state.ScaleX, state.ScaleY, state.ScaleZ);
		}

		private static bool ScaleValid(float x, float y, float z)
		{
			return Bounded(x, 1000000f) && Bounded(y, 1000000f) && Bounded(z, 1000000f);
		}

		private static bool RotationValid(float x, float y, float z, float w)
		{
			return x * x + y * y + z * z + w * w >= 0.000001f;
		}

		private static bool Bounded(float value, float limit)
		{
			return !float.IsNaN(value) && !float.IsInfinity(value) && value >= -limit && value <= limit;
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
			if (length <= 0 || length > MaximumFingerprintBytes) throw new InvalidDataException("fingerprint-length");
			byte[] bytes = reader.ReadBytes(length);
			if (bytes.Length != length) throw new EndOfStreamException();
			return Encoding.UTF8.GetString(bytes);
		}

		private static void WriteBody(BinaryWriter writer, LegacyGhostPoseBodyState body)
		{
			writer.Write(body.Index);
			writer.Write(body.PositionX); writer.Write(body.PositionY); writer.Write(body.PositionZ);
			writer.Write(body.RotationX); writer.Write(body.RotationY);
			writer.Write(body.RotationZ); writer.Write(body.RotationW);
			writer.Write(body.Health); writer.Write(body.BonusHealth);
			int flags = (body.Broken ? 1 : 0) | (body.Suspended ? 2 : 0);
			writer.Write(flags);
			writer.Write(body.SuspendFrames);
		}

		private static LegacyGhostPoseBodyState ReadBody(BinaryReader reader)
		{
			LegacyGhostPoseBodyState body = new LegacyGhostPoseBodyState();
			body.Index = reader.ReadInt32();
			body.PositionX = reader.ReadSingle(); body.PositionY = reader.ReadSingle(); body.PositionZ = reader.ReadSingle();
			body.RotationX = reader.ReadSingle(); body.RotationY = reader.ReadSingle();
			body.RotationZ = reader.ReadSingle(); body.RotationW = reader.ReadSingle();
			body.Health = reader.ReadSingle(); body.BonusHealth = reader.ReadSingle();
			int flags = reader.ReadInt32();
			if ((flags & ~3) != 0) throw new InvalidDataException("body-flags");
			body.Broken = (flags & 1) != 0;
			body.Suspended = (flags & 2) != 0;
			body.SuspendFrames = reader.ReadInt32();
			return body;
		}

		private static void WriteVisual(BinaryWriter writer, LegacyGhostPoseVisualState visual)
		{
			writer.Write(visual.Index);
			int flags = (visual.PrimaryPresent ? 1 : 0) | (visual.SecondaryPresent ? 2 : 0);
			writer.Write(flags);
			if (visual.PrimaryPresent) WriteTransform(writer, visual.Primary);
			if (visual.SecondaryPresent) WriteTransform(writer, visual.Secondary);
			writer.Write(visual.Auxiliaries.Length);
			for (int index = 0; index < visual.Auxiliaries.Length; index++)
			{
				LegacyGhostPoseAuxiliaryState auxiliary = visual.Auxiliaries[index];
				writer.Write(auxiliary.Index);
				writer.Write(auxiliary.ScaleX); writer.Write(auxiliary.ScaleY); writer.Write(auxiliary.ScaleZ);
			}
		}

		private static LegacyGhostPoseVisualState ReadVisual(BinaryReader reader)
		{
			LegacyGhostPoseVisualState visual = new LegacyGhostPoseVisualState();
			visual.Index = reader.ReadInt32();
			int flags = reader.ReadInt32();
			if ((flags & ~3) != 0 || flags == 0) throw new InvalidDataException("visual-flags");
			visual.PrimaryPresent = (flags & 1) != 0;
			visual.SecondaryPresent = (flags & 2) != 0;
			if (visual.PrimaryPresent) visual.Primary = ReadTransform(reader);
			if (visual.SecondaryPresent) visual.Secondary = ReadTransform(reader);
			int auxiliaryCount = reader.ReadInt32();
			if (auxiliaryCount < 0 || auxiliaryCount > MaximumAuxiliaryTransforms)
				throw new InvalidDataException("auxiliary-count");
			visual.Auxiliaries = new LegacyGhostPoseAuxiliaryState[auxiliaryCount];
			for (int index = 0; index < auxiliaryCount; index++)
			{
				LegacyGhostPoseAuxiliaryState auxiliary = new LegacyGhostPoseAuxiliaryState();
				auxiliary.Index = reader.ReadInt32();
				auxiliary.ScaleX = reader.ReadSingle(); auxiliary.ScaleY = reader.ReadSingle();
				auxiliary.ScaleZ = reader.ReadSingle();
				visual.Auxiliaries[index] = auxiliary;
			}
			return visual;
		}

		private static void WriteTransform(BinaryWriter writer, LegacyGhostPoseTransformState state)
		{
			writer.Write(state.PositionX); writer.Write(state.PositionY); writer.Write(state.PositionZ);
			writer.Write(state.RotationX); writer.Write(state.RotationY);
			writer.Write(state.RotationZ); writer.Write(state.RotationW);
			writer.Write(state.ScaleX); writer.Write(state.ScaleY); writer.Write(state.ScaleZ);
		}

		private static LegacyGhostPoseTransformState ReadTransform(BinaryReader reader)
		{
			LegacyGhostPoseTransformState state = new LegacyGhostPoseTransformState();
			state.PositionX = reader.ReadSingle(); state.PositionY = reader.ReadSingle();
			state.PositionZ = reader.ReadSingle(); state.RotationX = reader.ReadSingle();
			state.RotationY = reader.ReadSingle(); state.RotationZ = reader.ReadSingle();
			state.RotationW = reader.ReadSingle(); state.ScaleX = reader.ReadSingle();
			state.ScaleY = reader.ReadSingle(); state.ScaleZ = reader.ReadSingle();
			return state;
		}

		private static uint Checksum(byte[] data, int length)
		{
			uint hash = 2166136261u;
			for (int index = 0; index < length; index++) hash = unchecked((hash ^ data[index]) * 16777619u);
			return hash;
		}
	}
}
