using System;
using System.Runtime.InteropServices;

internal static class WoMCpYvRGZJu7ct6lRwjse4DzmbHyL83TNVjta45XMMb
{
	[DllImport("__Internal.dll", CharSet = CharSet.Unicode)]
	private static extern IntPtr showFileDialog(IntPtr intptr_0, IntPtr intptr_1);

	internal static string bkqIyKyA4b9vSqK05WSuZig(string string_0, string string_1)
	{
		IntPtr intptr_ = smethod_0(string_1);
		IntPtr intPtr = smethod_0(string_0);
		IntPtr ptr = showFileDialog(intptr_, intPtr);
		smethod_1(intptr_);
		smethod_1(intPtr);
		if (ptr.ToInt32() == 0)
		{
			return null;
		}
		return Marshal.PtrToStringAnsi(ptr);
	}

	internal static IntPtr smethod_0(string string_0)
	{
		return Marshal.StringToHGlobalAnsi(string_0);
	}

	internal static void smethod_1(IntPtr intptr_0)
	{
		Marshal.FreeHGlobal(intptr_0);
	}
}
