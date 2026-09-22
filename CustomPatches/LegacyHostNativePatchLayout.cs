using System;
using System.Collections.Generic;

namespace MPatcherFork.CustomPatches
{
	// Pure x86 byte layout used by the runtime patch and by offline checks.
	internal static class LegacyHostNativePatchLayout
	{
		internal static byte[] BuildLossStub(IntPtr stub, IntPtr returnAddress, IntPtr managerSlot, IntPtr pendingSlot)
		{
			List<byte> code = new List<byte>();
			Emit(code, 0x89, 0x35); EmitInt32(code, managerSlot.ToInt32());
			Emit(code, 0xC7, 0x05); EmitInt32(code, pendingSlot.ToInt32()); EmitInt32(code, 1);
			Emit(code, 0x0F, 0xB7, 0x86, 0xFC, 0x02, 0x00, 0x00);
			EmitJump(code, Add(stub, code.Count), Add(returnAddress, 7));
			return code.ToArray();
		}

		internal static byte[] BuildConnectedStub(IntPtr stub, IntPtr returnAddress, IntPtr pendingSlot)
		{
			List<byte> code = new List<byte>();
			Emit(code, 0xC7, 0x05); EmitInt32(code, pendingSlot.ToInt32()); EmitInt32(code, 0);
			Emit(code, 0x8B, 0x4E, 0x3C, 0x6A, 0x01);
			EmitJump(code, Add(stub, code.Count), Add(returnAddress, 5));
			return code.ToArray();
		}

		internal static byte[] BuildReconnectStub(IntPtr addressToString)
		{
			List<byte> code = new List<byte>();
			Emit(code, 0x55, 0x8B, 0xEC, 0x53, 0x56, 0x8B, 0x75, 0x08);
			Emit(code, 0x0F, 0xB7, 0x86, 0xFC, 0x02, 0x00, 0x00);
			Emit(code, 0x8B, 0x56, 0x38, 0x8B, 0x1A);
			Emit(code, 0x6A, 0x00, 0x6A, 0x00, 0x68); EmitInt32(code, 500);
			Emit(code, 0x6A, 0x0C, 0x6A, 0x00, 0x6A, 0x00, 0x6A, 0x00, 0x50, 0x6A, 0x00);
			Emit(code, 0x8D, 0x8E, 0xF8, 0x02, 0x00, 0x00, 0xBA); EmitInt32(code, addressToString.ToInt32());
			Emit(code, 0xFF, 0xD2, 0x50, 0x8B, 0x4E, 0x38, 0x8B, 0x53, 0x30, 0xFF, 0xD2);
			Emit(code, 0x0F, 0xB6, 0xC0, 0x5E, 0x5B, 0x5D, 0xC3);
			return code.ToArray();
		}

		internal static byte[] JumpPatch(IntPtr source, IntPtr target, int length)
		{
			if (length < 5) throw new ArgumentOutOfRangeException("length");
			byte[] result = new byte[length];
			result[0] = 0xE9;
			Buffer.BlockCopy(BitConverter.GetBytes(Relative(source, target)), 0, result, 1, 4);
			for (int index = 5; index < result.Length; index++) result[index] = 0x90;
			return result;
		}

		internal static IntPtr Add(IntPtr value, int offset)
		{
			return new IntPtr(value.ToInt64() + offset);
		}

		private static void EmitJump(List<byte> code, IntPtr source, IntPtr target)
		{
			code.Add(0xE9);
			EmitInt32(code, Relative(source, target));
		}

		private static int Relative(IntPtr source, IntPtr target)
		{
			return unchecked((int)(target.ToInt64() - source.ToInt64() - 5L));
		}

		private static void Emit(List<byte> code, params byte[] bytes)
		{
			code.AddRange(bytes);
		}

		private static void EmitInt32(List<byte> code, int value)
		{
			code.AddRange(BitConverter.GetBytes(value));
		}
	}
}
