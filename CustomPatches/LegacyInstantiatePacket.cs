using System;

namespace MPatcherFork.CustomPatches
{
    // Unity 5.6.2f1 x86: 0086D840/0086C4C0, 00860700/00860810.
    // Fixed Instantiate prefix: group, GUID, component, position/quaternion,
    // view count (big endian). Compact view IDs use RakNet native byte order.
    internal static class LegacyInstantiatePacket
    {
        internal static bool MatchesRoot(byte[] bytes, int bitCount, int a, int b, int c)
        {
            if (bytes == null || bitCount < 440 || bitCount > bytes.Length * 8 || b == 0 || c != 0) return false;
            uint count = ((uint)bytes[49] << 24) | ((uint)bytes[50] << 16) | ((uint)bytes[51] << 8) | bytes[52];
            if (count == 0 || count > 1024) return false;
            int offset = 424;
            bool match = false;
            for (uint i = 0; i < count; i++)
            {
                if (offset + 16 > bitCount) return false;
                bool wide = Bits(bytes, ref offset, 1) != 0;
                if (wide && Bits(bytes, ref offset, 1) != 0) return false; // unsupported 64-bit encoding
                bool allocated = Bits(bytes, ref offset, 1) != 0;
                int prefixBits = allocated ? 0 : (wide ? 15 : 4);
                int idBits = allocated ? (wide ? 29 : 14) : (wide ? 14 : 10);
                if (offset + prefixBits + idBits > bitCount) return false;
                int prefix = Native(bytes, ref offset, prefixBits), id = Native(bytes, ref offset, idBits);
                if (i == 0) match = allocated && prefix == a && id == b;
            }
            return match && offset == bitCount;
        }
        private static int Native(byte[] bytes, ref int offset, int count)
        {
            int value = 0, shift = 0;
            while (count > 0)
            {
                int n = Math.Min(count, 8);
                value |= Bits(bytes, ref offset, n) << shift;
                count -= n; shift += 8;
            }
            return value;
        }
        private static int Bits(byte[] bytes, ref int offset, int count)
        {
            int value = 0;
            while (count-- > 0) { value = (value << 1) | ((bytes[offset >> 3] >> (7 - (offset & 7))) & 1); offset++; }
            return value;
        }
    }
}
