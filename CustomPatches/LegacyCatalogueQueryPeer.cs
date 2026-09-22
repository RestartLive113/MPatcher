using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
    // Main-thread cleanup of the separate catalogue peer only, after terminal
    // failures. Never called from the native callback/packet processing stack.
    internal static class LegacyCatalogueQueryPeer
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] private delegate void StopPeer(IntPtr peer);
        private static StopPeer stop;
        private static IntPtr thunk;
        internal static string ReleaseOrphanedQuery()
        {
            if (Network.peerType != NetworkPeerType.Disconnected || !LegacyMasterQueryFix.Applied
                || !LegacySocketSelfTestFix.Applied) return null;
            LegacyMasterNativeState.Initialize();
            LegacyMasterStateSnapshot before = LegacyMasterNativeState.Read();
            if (before.QueryPeer == IntPtr.Zero || !LegacyCatalogueOrphanPolicy.CanRelease(true,
                Marshal.ReadByte(before.QueryPeer, 4) == 1, before.QueryPending,
                before.RegistrationPending, before.UpdatePending, before.SubmissionPending,
                before.TypeLength, before.NameLength, before.CommentLength, before.RowId)) return null;
            // Main thread, immediately before the normal RequestHostList call.
            // Network teardown removed the type and stopped the peer but left
            // +2D set. There is no pending packet to finish this abandoned job.
            Marshal.WriteByte(before.Context, 0x2d, 0);
            return before.ToString();
        }
        internal static string Restart()
        {
            if (Network.peerType != NetworkPeerType.Disconnected) throw new InvalidOperationException("Game peer is not disconnected");
            if (!LegacyMasterQueryFix.Applied || !LegacySocketSelfTestFix.Applied)
                throw new InvalidOperationException("Required native fixes absent");
            LegacyMasterNativeState.Initialize(); // Exact EXE hash + getter guard.
            LegacyMasterStateSnapshot before = LegacyMasterNativeState.Read();
            if (before.QueryPending != 0 || before.RegistrationPending != 0 || before.UpdatePending != 0 || before.SubmissionPending != 0
                || before.NameLength != 0 || before.CommentLength != 0 || before.RowId != -1)
                throw new InvalidOperationException("Pending native operation; restart refused");
            using (Process process = Process.GetCurrentProcess())
            {
                IntPtr address = LegacyHostNativePatchLayout.Add(process.MainModule.BaseAddress, 0x8d6399);
                if (before.QueryPeer == IntPtr.Zero || Marshal.ReadIntPtr(Marshal.ReadIntPtr(before.QueryPeer), 0x38) != address)
                    throw new InvalidOperationException("Unexpected query shutdown vtable");
                byte[] signature = { 0x55,0x8b,0xec,0x83,0xec,0x10,0x53,0x56,0x8b,0xf1 };
                for(int i=0;i<signature.Length;i++)
                    if(Marshal.ReadByte(address,i)!=signature[i])throw new InvalidOperationException("Shutdown signature changed");
                if(stop == null)
                {
                    thunk = VirtualAlloc(IntPtr.Zero,new UIntPtr(4096),0x3000,0x40);
                    if(thunk == IntPtr.Zero)throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
                    // cdecl(peer) -> native thiscall(peer, block=0, channel=0, priority=3).
                    List<byte> code=new List<byte>(new byte[]{0x55,0x8b,0xec,0x8b,0x4d,8,0x6a,3,0x6a,0,0x6a,0,0xb8});
                    code.AddRange(BitConverter.GetBytes(address.ToInt32()));
                    code.AddRange(new byte[]{0xff,0xd0,0x5d,0xc3});
                    Marshal.Copy(code.ToArray(),0,thunk,code.Count);
                    if(!FlushInstructionCache(process.Handle,thunk,new UIntPtr((uint)code.Count)))throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
                    stop=(StopPeer)Marshal.GetDelegateForFunctionPointer(thunk,typeof(StopPeer));
                }
                Stopwatch clock=Stopwatch.StartNew();
                stop(before.QueryPeer);
                clock.Stop();
                LegacyMasterStateSnapshot after = LegacyMasterNativeState.Read();
                return "durationMs="+clock.ElapsedMilliseconds+" inactive="+(Marshal.ReadByte(after.QueryPeer,4)!=0)
                    +" samePeer="+(after.QueryPeer==before.QueryPeer)+" gamePeer="+Network.peerType+" "+after;
            }
        }
        [DllImport("kernel32.dll",SetLastError=true)] private static extern IntPtr VirtualAlloc(IntPtr p,UIntPtr n,uint type,uint protect);
        [DllImport("kernel32.dll",SetLastError=true)] private static extern bool FlushInstructionCache(IntPtr process,IntPtr p,UIntPtr n);
    }
}
