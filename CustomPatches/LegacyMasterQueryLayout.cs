using System;
using System.Collections.Generic;

namespace MPatcherFork.CustomPatches
{
    internal static class LegacyMasterQueryLayout
    {
        // ESI = MasterServer. Its +1C query peer is separate from the game peer.
        // Exact RakPeer::IsActive returns peer[4] == 0. A second Initialize returns
        // false even though the socket works. Reuse it and continue native Connect.
        internal static byte[] BuildInitializeStub(IntPtr stub, IntPtr initialize, IntPtr connect, IntPtr counter)
        {
            List<byte> code = new List<byte>(new byte[] {
                0x9c, 0x50,                   // pushfd; push eax
                0x8b, 0x46, 0x1c,             // mov eax,[esi+1c]
                0x80, 0x78, 0x04, 0x00,       // cmp byte [eax+4],0
                0x75, 0x0d,                   // jne initialize
                0xff, 0x05                    // inc dword [counter]
            });
            code.AddRange(BitConverter.GetBytes(counter.ToInt32()));
            code.AddRange(new byte[] { 0x58, 0x9d });
            Jump(code, stub, connect);
            code.AddRange(new byte[] { 0x58, 0x9d, 0x6a, 0x00, 0x6a, 0x00, 0x8d, 0x4d, 0xd0 });
            Jump(code, stub, initialize);
            return code.ToArray();
        }

        // Native connection-attempt failure already clears +2C/+2E/+98, but
        // omits +2D. Release the query latch too, so later polling can retry.
        internal static byte[] BuildFailureStub(IntPtr stub, IntPtr resume, IntPtr counter)
        {
            List<byte> code = new List<byte>(new byte[] {
                0x9c,
                0x80, 0x7b, 0x2d, 0x00,       // cmp byte [ebx+2d],0
                0x74, 0x06,
                0xff, 0x05
            });
            code.AddRange(BitConverter.GetBytes(counter.ToInt32()));
            code.AddRange(new byte[] {
                0xc6, 0x43, 0x2d, 0x00,
                0x9d,
                0xc6, 0x43, 0x2c, 0x00,       // displaced native stores
                0xc6, 0x43, 0x2e, 0x00
            });
            Jump(code, stub, resume);
            return code.ToArray();
        }

        private static void Jump(List<byte> code, IntPtr stub, IntPtr target)
        {
            code.AddRange(LegacyHostNativePatchLayout.JumpPatch(
                LegacyHostNativePatchLayout.Add(stub, code.Count), target, 5));
        }
    }
}
