using Steamworks;

internal static class BVzwUED_lI8LYZ1VYbVfpY8_XEX8q1mymRkkFvCM9QtF72TvSNrVMo20Z_00247bZN8LUg
{
	public static void MxbKOz3Z12n9GthAFgew1PA(string string_0)
	{
		smethod_0(string_0);
	}

	public static bool ZNfyS8TMNZ8c4kK7hxvORN8(string string_0)
	{
		return smethod_1(string_0);
	}

	public static byte[] sGBLz5ppbj9Xk8N7mVktjRU(string string_0)
	{
		byte[] array = new byte[smethod_2(string_0)];
		smethod_3(string_0, array, array.Length);
		return array;
	}

	public static bool FVgTq5mhzp_NApR0l5yeSxg(string string_0, byte[] byte_0)
	{
		return smethod_4(string_0, byte_0, byte_0.Length);
	}

	internal static bool smethod_0(string string_0)
	{
		return SteamRemoteStorage.FileDelete(string_0);
	}

	internal static bool smethod_1(string string_0)
	{
		return SteamRemoteStorage.FileExists(string_0);
	}

	internal static int smethod_2(string string_0)
	{
		return SteamRemoteStorage.GetFileSize(string_0);
	}

	internal static int smethod_3(string string_0, byte[] byte_0, int int_0)
	{
		return SteamRemoteStorage.FileRead(string_0, byte_0, int_0);
	}

	internal static bool smethod_4(string string_0, byte[] byte_0, int int_0)
	{
		return SteamRemoteStorage.FileWrite(string_0, byte_0, int_0);
	}
}
