using System;
using System.Collections.Generic;

namespace MPatcherFork.CustomPatches
{
    internal static class LegacyMasterLifecycleLayout
    {
        // UnregisterHost has already cleared metadata and detached its publication plugin.
        // ESI is the MasterServer context. Preserve registers and flags, including +2D
        // (the separate query peer); replay the displaced submission-pending store.
        internal static byte[] BuildCancelStub(IntPtr stub, IntPtr resume, IntPtr counter)
        {
            List<byte> code = new List<byte>(new byte[] {
                0x9c,                         // pushfd
                0x80, 0x7e, 0x2c, 0x00,       // cmp byte [esi+2c],0
                0x75, 0x06,                   // jne record
                0x80, 0x7e, 0x2e, 0x00,       // cmp byte [esi+2e],0
                0x74, 0x06,                   // je clear
                0xff, 0x05                    // record: inc dword [counter]
            });
            code.AddRange(BitConverter.GetBytes(counter.ToInt32()));
            code.AddRange(new byte[] {
                0xc6, 0x46, 0x2c, 0x00,       // clear: mov byte [esi+2c],0
                0xc6, 0x46, 0x2e, 0x00,       // mov byte [esi+2e],0
                0x9d,                         // popfd
                0xc6, 0x86, 0x98, 0, 0, 0, 0 // displaced mov byte [esi+98],0
            });
            code.AddRange(LegacyHostNativePatchLayout.JumpPatch(
                LegacyHostNativePatchLayout.Add(stub, code.Count), resume, 5));
            return code.ToArray();
        }
    }
}
