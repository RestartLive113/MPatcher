using System;
using System.Collections.Generic;

namespace MPatcherFork.CustomPatches
{
    internal static class LegacySocketSelfTestLayout
    {
        // Initialize uses EDI; both Shutdown wake sends use EAX for the socket
        // record. +8 contains getsockname's network-order address.
        // Both native SendTo overloads use thiscall, six arguments, ret 18h.
        internal static byte[] BuildStub(IntPtr stub, IntPtr textSend, IntPtr addressSend, IntPtr counter, bool recordInEax)
        {
            List<byte> code = new List<byte>(new byte[] {
                0x8b,(byte)(recordInEax ? 0x40 : 0x47),0x08, // mov eax,[eax/edi+8]
                0x85,0xc0,                   // test eax,eax
                0x74,0x14,                   // je original
                0x83,0xf8,0xff,              // cmp eax,-1 (invalid getsockname)
                0x74,0x0f,                   // je original
                0xff,0x05                    // inc dword [counter]
            });
            code.AddRange(BitConverter.GetBytes(counter.ToInt32()));
            code.AddRange(new byte[] { 0x89,0x44,0x24,0x10 }); // fourth stack argument: address
            code.AddRange(LegacyHostNativePatchLayout.JumpPatch(LegacyHostNativePatchLayout.Add(stub,code.Count),addressSend,5));
            code.AddRange(LegacyHostNativePatchLayout.JumpPatch(LegacyHostNativePatchLayout.Add(stub,code.Count),textSend,5));
            return code.ToArray();
        }
    }
}
