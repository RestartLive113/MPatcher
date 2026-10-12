using System;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;

namespace MPatcherFork.CustomPatches
{
    // Used only for crafts carrying a retained native shape template. The original
    // .mzbd codec has no props lane; a bounded native-JSON envelope keeps the
    // shape floor and all other metadata in that craft together.
    internal static class MachineCompressionMetadata
    {
        private static readonly byte[] Magic = Encoding.ASCII.GetBytes("MPSHDATA");
        private const int Version = 1;
        private const int DigestLength = 32;
        private const int HeaderLength = 8 + 4 + 4 + 4 + DigestLength;
        private const int MaximumJsonBytes = 128 * 1024 * 1024;
        private const int MaximumCompressedBytes = MaximumJsonBytes + 1024 * 1024;
        private static readonly Encoding JsonEncoding = new UTF8Encoding(false, true);

        internal static bool Required(BuildData build)
        {
            if (build == null || build.blockData == null) return false;
            foreach (BlockData block in build.blockData)
                if (ShapeDetailRetention.HasRetainedDetail(block)) return true;
            return false;
        }

        internal static bool IsEnvelope(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0) return false;
            int length = Math.Min(bytes.Length, Magic.Length);
            for (int i = 0; i < length; i++) if (bytes[i] != Magic[i]) return false;
            // A partial magic is ours too, so truncation cannot enter the old codec.
            return true;
        }

        internal static byte[] Encode(BuildData build)
        {
            byte[] json = JsonEncoding.GetBytes(LNGKNOGOIKL.AOPMIBBFLKH(build));
            if (json.Length == 0 || json.Length > MaximumJsonBytes)
                throw new InvalidDataException("Shape metadata JSON exceeds the .mzbd envelope bound");
            byte[] compressed;
            using (MemoryStream stream = new MemoryStream())
            {
                using (GZipStream zip = new GZipStream(stream, CompressionMode.Compress))
                    zip.Write(json, 0, json.Length);
                compressed = stream.ToArray();
            }
            if (compressed.Length == 0 || compressed.Length > MaximumCompressedBytes)
                throw new InvalidDataException("Shape metadata gzip exceeds the .mzbd envelope bound");
            byte[] digest;
            using (SHA256 hash = SHA256.Create()) digest = hash.ComputeHash(json);
            using (MemoryStream stream = new MemoryStream())
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                writer.Write(Magic);
                writer.Write(Version);
                writer.Write(json.Length);
                writer.Write(compressed.Length);
                writer.Write(digest);
                writer.Write(compressed);
                return stream.ToArray();
            }
        }

        internal static BuildData Decode(byte[] bytes)
        {
            if (!IsEnvelope(bytes) || bytes.Length < HeaderLength)
                throw new InvalidDataException("Truncated shape metadata .mzbd envelope");
            int jsonLength, compressedLength;
            byte[] expectedDigest;
            using (BinaryReader reader = new BinaryReader(new MemoryStream(bytes, false)))
            {
                reader.ReadBytes(Magic.Length);
                if (reader.ReadInt32() != Version)
                    throw new InvalidDataException("Unsupported shape metadata .mzbd version");
                jsonLength = reader.ReadInt32(); compressedLength = reader.ReadInt32();
                if (jsonLength <= 0 || jsonLength > MaximumJsonBytes
                    || compressedLength <= 0 || compressedLength > MaximumCompressedBytes
                    || (long)HeaderLength + compressedLength != bytes.Length)
                    throw new InvalidDataException("Invalid shape metadata .mzbd length");
                expectedDigest = reader.ReadBytes(DigestLength);
            }
            byte[] json;
            try
            {
                using (MemoryStream input = new MemoryStream(bytes, HeaderLength, compressedLength, false))
                using (GZipStream zip = new GZipStream(input, CompressionMode.Decompress))
                using (MemoryStream output = new MemoryStream(Math.Min(jsonLength, 8192)))
                {
                    byte[] buffer = new byte[8192]; int count;
                    while ((count = zip.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        if (output.Length + count > jsonLength)
                            throw new InvalidDataException("Shape metadata .mzbd exceeds its decoded length");
                        output.Write(buffer, 0, count);
                    }
                    if (output.Length != jsonLength)
                        throw new InvalidDataException("Truncated shape metadata .mzbd gzip");
                    json = output.ToArray();
                }
            }
            catch (IOException error)
            {
                throw new InvalidDataException("Invalid shape metadata .mzbd gzip", error);
            }
            byte[] digest;
            using (SHA256 hash = SHA256.Create()) digest = hash.ComputeHash(json);
            for (int i = 0; i < DigestLength; i++)
                if (digest[i] != expectedDigest[i])
                    throw new InvalidDataException("Shape metadata .mzbd checksum mismatch");
            BuildData result;
            try { result = LNGKNOGOIKL.FMAGAEMFION<BuildData>(JsonEncoding.GetString(json)); }
            catch (Exception error) { throw new InvalidDataException("Invalid shape metadata .mzbd JSON", error); }
            if (result == null || result.blockData == null)
                throw new InvalidDataException("Shape metadata .mzbd has no BuildData");
            return result;
        }
    }
}
