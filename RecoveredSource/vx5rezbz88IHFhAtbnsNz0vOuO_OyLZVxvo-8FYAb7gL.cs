using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

internal static class vx5rezbz88IHFhAtbnsNz0vOuO_OyLZVxvo_00248FYAb7gL
{
	private static readonly string Fug5Mf3Nx2B3FwqedFV7obE = global::_003CModule_003E.smethod_25<string>(3259908172u);

	private static readonly int gCA8wl5fBFGUWqbtDkXM_rY = 1024;

	private static readonly int[] eAQsJ9cpoAQ9Z5xTnQRArTQ;

	internal static string Yk6MLlrjfOYg5zwrV0gtqWI(string string_0)
	{
		return Yk6MLlrjfOYg5zwrV0gtqWI(string_0, gCA8wl5fBFGUWqbtDkXM_rY, Fug5Mf3Nx2B3FwqedFV7obE);
	}

	internal static byte[] WIJ5B8GctpjOKEeKKfHHOdY(byte[] byte_0)
	{
		return WIJ5B8GctpjOKEeKKfHHOdY(byte_0, eAQsJ9cpoAQ9Z5xTnQRArTQ);
	}

	internal static byte[] WIJ5B8GctpjOKEeKKfHHOdY(byte[] byte_0, int[] int_0)
	{
		int num = 0;
		for (int i = 0; i < byte_0.Length; i++)
		{
			byte_0[i] = (byte)(byte_0[i] ^ int_0[num]);
			num++;
			if (num > int_0.Length - 1)
			{
				num = 0;
			}
		}
		return byte_0;
	}

	internal static string Yk6MLlrjfOYg5zwrV0gtqWI(string string_0, int int_0, string string_1)
	{
		RSACryptoServiceProvider rSACryptoServiceProvider = smethod_0(int_0);
		smethod_1((AsymmetricAlgorithm)rSACryptoServiceProvider, string_1);
		int num = int_0 / 8;
		byte[] array = smethod_3(smethod_2(), string_0);
		int num2 = num - 42;
		int num3 = array.Length;
		int num4 = num3 / num2;
		StringBuilder stringBuilder = smethod_4();
		for (int i = 0; i <= num4; i++)
		{
			byte[] array2 = new byte[(num3 - num2 * i > num2) ? num2 : (num3 - num2 * i)];
			smethod_5((Array)array, num2 * i, (Array)array2, 0, array2.Length);
			byte[] array3 = smethod_6(rSACryptoServiceProvider, array2, bool_0: true);
			smethod_7((Array)array3);
			smethod_9(stringBuilder, smethod_8(array3));
		}
		return smethod_10((object)stringBuilder);
	}

	static vx5rezbz88IHFhAtbnsNz0vOuO_OyLZVxvo_00248FYAb7gL()
	{
	int[] array_ = new int[13] { 2, 3, 7, 9, 9, 2, 2, 5, 3, 7, 4, 8, 6 };
		eAQsJ9cpoAQ9Z5xTnQRArTQ = array_;
	}

	internal static RSACryptoServiceProvider smethod_0(int int_0)
	{
		return new RSACryptoServiceProvider(int_0);
	}

	internal static void smethod_1(AsymmetricAlgorithm asymmetricAlgorithm_0, string string_0)
	{
		asymmetricAlgorithm_0.FromXmlString(string_0);
	}

	internal static Encoding smethod_2()
	{
		return Encoding.UTF32;
	}

	internal static byte[] smethod_3(Encoding encoding_0, string string_0)
	{
		return encoding_0.GetBytes(string_0);
	}

	internal static StringBuilder smethod_4()
	{
		return new StringBuilder();
	}

	internal static void smethod_5(Array array_0, int int_0, Array array_1, int int_1, int int_2)
	{
		Buffer.BlockCopy(array_0, int_0, array_1, int_1, int_2);
	}

	internal static byte[] smethod_6(RSACryptoServiceProvider rsacryptoServiceProvider_0, byte[] byte_0, bool bool_0)
	{
		return rsacryptoServiceProvider_0.Encrypt(byte_0, bool_0);
	}

	internal static void smethod_7(Array array_0)
	{
		Array.Reverse(array_0);
	}

	internal static string smethod_8(byte[] byte_0)
	{
		return Convert.ToBase64String(byte_0);
	}

	internal static StringBuilder smethod_9(StringBuilder stringBuilder_0, string string_0)
	{
		return stringBuilder_0.Append(string_0);
	}

	internal static string smethod_10(object object_0)
	{
		return object_0.ToString();
	}

	internal static void smethod_11(Array array_0, RuntimeFieldHandle runtimeFieldHandle_0)
	{
		RuntimeHelpers.InitializeArray(array_0, runtimeFieldHandle_0);
	}
}
