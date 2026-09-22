using System;
using System.IO;

namespace MPatcherFork.CustomPatches
{
	// Unity Legacy RPCs are kept small and reliable. The machine snapshot codec
	// validates the reconstructed payload before any scene object is changed.
	internal static class LegacyGhostPoseTransportCodec
	{
		internal const int MaximumChunkBytes = 4096;
		internal const int MaximumChunks = 128;
		internal const int MaximumPayloadBytes = MaximumChunkBytes * MaximumChunks;

		internal static byte[][] Split(byte[] payload)
		{
			if (payload == null || payload.Length == 0 || payload.Length > MaximumPayloadBytes)
				throw new InvalidDataException("ghost-pose-payload-size");
			int count = (payload.Length + MaximumChunkBytes - 1) / MaximumChunkBytes;
			byte[][] chunks = new byte[count][];
			for (int index = 0; index < count; index++)
			{
				int offset = index * MaximumChunkBytes;
				int length = Math.Min(MaximumChunkBytes, payload.Length - offset);
				chunks[index] = new byte[length];
				Buffer.BlockCopy(payload, offset, chunks[index], 0, length);
			}
			return chunks;
		}

		internal static bool TryJoin(byte[][] chunks, out byte[] payload, out string reason)
		{
			payload = null;
			reason = null;
			if (chunks == null || chunks.Length == 0 || chunks.Length > MaximumChunks)
			{
				reason = "ghost-pose-chunk-count";
				return false;
			}
			int total = 0;
			for (int index = 0; index < chunks.Length; index++)
			{
				byte[] chunk = chunks[index];
				if (chunk == null || chunk.Length == 0 || chunk.Length > MaximumChunkBytes
					|| index + 1 < chunks.Length && chunk.Length != MaximumChunkBytes)
				{
					reason = "ghost-pose-chunk-shape-" + index;
					return false;
				}
				total += chunk.Length;
				if (total > MaximumPayloadBytes)
				{
					reason = "ghost-pose-payload-size";
					return false;
				}
			}
			payload = new byte[total];
			int destination = 0;
			for (int index = 0; index < chunks.Length; index++)
			{
				Buffer.BlockCopy(chunks[index], 0, payload, destination, chunks[index].Length);
				destination += chunks[index].Length;
			}
			return true;
		}
	}
}
