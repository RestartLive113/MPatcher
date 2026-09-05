using System;

internal static class Y0682_EmDASRYkXiQKRSXHE
{
	public const int rjPCRFJ8SO9Y8jFEQMpiprKsBLnw5DlwamJjLmn6GKsa = 1;

	public const int qoJxQDsh9dJ3tMk5DjqpIMTiqXZjmXaO5u_0024LajK0aFol = 5;

	public const int int_0 = 0;

	public const int hS5UDWD3wpU4gwPK8IR9vI7SCntyjPUHtNqcQ0_rBecX = 0;

	public const int T1oI33sl8dAZEIyZfBOYKSE = 0;

	private const int Uz1qsYY_00248_0024oBz8f9cUZstio = 4096;

	private const int wQiJ_VcB_bqMTUCNJOcllkA = 2;

	private const int op5xn4Q72keaGsWi7_BjiiWyFxU8gBxoOt0B752uhFNr = 6;

	private const int dnTd_0024wNs106a1tTykLKUI_00246w3nU_y1dC4o7TRqAniApF = 4;

	private const int fxlmX7p1wb3gF4OcEaCoQRg = 4;

	private const int psiM0YAYLl5qmgzbqQcg96gUf4kpbNJeUYtIGHGdDk52 = 9;

	private const int FI5pubxGSLm7YbbTtMghANU = 1;

	private const int FsGmS7Owwd2sEwwTj_RO_0024yU = 16;

	private static int qMXAfJiiu8ZQs4W_0024qFik9Q4(byte[] byte_0)
	{
		if ((byte_0[0] & 2) != 2)
		{
			return 3;
		}
		return 9;
	}

	public static int T6sUldtwgK72ksilJRIE833as8UiijYSbElGWKbEaxsk(byte[] byte_0)
	{
		if (qMXAfJiiu8ZQs4W_0024qFik9Q4(byte_0) == 9)
		{
			return byte_0[5] | (byte_0[6] << 8) | (byte_0[7] << 16) | (byte_0[8] << 24);
		}
		return byte_0[2];
	}

	public static int LpNFJm_Q8mY8YYAHQtF_zl8(byte[] byte_0)
	{
		if (qMXAfJiiu8ZQs4W_0024qFik9Q4(byte_0) == 9)
		{
			return byte_0[1] | (byte_0[2] << 8) | (byte_0[3] << 16) | (byte_0[4] << 24);
		}
		return byte_0[1];
	}

	private static void aYy0EM5Q4IUH3Y5o91eX_0024og(byte[] byte_0, int int_1, bool bool_0, int int_2, int int_3)
	{
		byte_0[0] = (byte)(2u | (bool_0 ? 1u : 0u));
		byte_0[0] |= (byte)(int_1 << 2);
		byte_0[0] |= 64;
		byte_0[0] |= 0;
		hMks2fmLNmoUej5peTdTvuc(byte_0, 1, int_3, 4);
		hMks2fmLNmoUej5peTdTvuc(byte_0, 5, int_2, 4);
	}

	public static byte[] Hnc3QYGDRf1W8TRMMLwwZ48(byte[] byte_0, int int_1)
	{
		int i = 0;
		int num = 13;
		uint num2 = 2147483648u;
		int int_2 = 9;
		byte[] array = new byte[byte_0.Length + 400];
		int[] array2 = new int[4096];
		byte[] array3 = new byte[4096];
		int num3 = 0;
		int num4 = byte_0.Length - 6 - 4 - 1;
		int num5 = 0;
		if (int_1 != 1 && int_1 != 3)
		{
			throw smethod_0(global::_003CModule_003E.smethod_25<string>(1523236156u));
		}
		int[,] array4 = ((int_1 == 1) ? new int[4096, 1] : new int[4096, 16]);
		if (byte_0.Length == 0)
		{
			return new byte[0];
		}
		if (i <= num4)
		{
			num3 = byte_0[i] | (byte_0[i + 1] << 8) | (byte_0[i + 2] << 16);
		}
		byte[] array5;
		while (true)
		{
			if (i <= num4)
			{
				if ((num2 & 1) == 1)
				{
					if (i > byte_0.Length >> 1 && num > i - (i >> 5))
					{
						break;
					}
					hMks2fmLNmoUej5peTdTvuc(array, int_2, (int)(num2 >> 1) | int.MinValue, 4);
					int_2 = num;
					num += 4;
					num2 = 2147483648u;
				}
				if (int_1 == 1)
				{
					int num6 = ((num3 >> 12) ^ num3) & 0xFFF;
					int num7 = array4[num6, 0];
					int num8 = array2[num6] ^ num3;
					array2[num6] = num3;
					array4[num6, 0] = i;
					if (num8 == 0 && array3[num6] != 0 && (i - num7 > 2 || (i == num7 + 1 && num5 >= 3 && i > 3 && byte_0[i] == byte_0[i - 3] && byte_0[i] == byte_0[i - 2] && byte_0[i] == byte_0[i - 1] && byte_0[i] == byte_0[i + 1] && byte_0[i] == byte_0[i + 2])))
					{
						num2 = (num2 >> 1) | 0x80000000u;
						if (byte_0[num7 + 3] != byte_0[i + 3])
						{
							int num9 = 1 | (num6 << 4);
							array[num] = (byte)num9;
							array[num + 1] = (byte)(num9 >> 8);
							i += 3;
							num += 2;
						}
						else
						{
							int num10 = i;
							int num11 = ((byte_0.Length - 4 - i + 1 - 1 > 255) ? 255 : (byte_0.Length - 4 - i + 1 - 1));
							i += 4;
							if (byte_0[num7 + i - num10] == byte_0[i])
							{
								i++;
								if (byte_0[num7 + i - num10] == byte_0[i])
								{
									for (i++; byte_0[num7 + (i - num10)] == byte_0[i] && i - num10 < num11; i++)
									{
									}
								}
							}
							int num12 = i - num10;
							num6 <<= 4;
							if (num12 >= 18)
							{
								hMks2fmLNmoUej5peTdTvuc(array, num, num6 | (num12 << 16), 3);
								num += 3;
							}
							else
							{
								int num13 = num6 | (num12 - 2);
								array[num] = (byte)num13;
								array[num + 1] = (byte)(num13 >> 8);
								num += 2;
							}
						}
						num3 = byte_0[i] | (byte_0[i + 1] << 8) | (byte_0[i + 2] << 16);
						num5 = 0;
					}
					else
					{
						num5++;
						array3[num6] = 1;
						array[num] = byte_0[i];
						num2 >>= 1;
						i++;
						num++;
						num3 = ((num3 >> 8) & 0xFFFF) | (byte_0[i + 2] << 16);
					}
					continue;
				}
				num3 = byte_0[i] | (byte_0[i + 1] << 8) | (byte_0[i + 2] << 16);
				int num14 = ((byte_0.Length - 4 - i + 1 - 1 > 255) ? 255 : (byte_0.Length - 4 - i + 1 - 1));
				int num15 = ((num3 >> 12) ^ num3) & 0xFFF;
				byte b = array3[num15];
				int num16 = 0;
				int num17 = 0;
				int num18;
				for (int j = 0; j < 16 && b > j; j++)
				{
					num18 = array4[num15, j];
					if ((byte)num3 == byte_0[num18] && (byte)(num3 >> 8) == byte_0[num18 + 1] && (byte)(num3 >> 16) == byte_0[num18 + 2] && num18 < i - 2)
					{
						int k;
						for (k = 3; byte_0[num18 + k] == byte_0[i + k] && k < num14; k++)
						{
						}
						if (k > num16 || (k == num16 && num18 > num17))
						{
							num17 = num18;
							num16 = k;
						}
					}
				}
				num18 = num17;
				array4[num15, b & 0xF] = i;
				b++;
				array3[num15] = b;
				if (num16 >= 3 && i - num18 < 131071)
				{
					int num19 = i - num18;
					for (int l = 1; l < num16; l++)
					{
						num3 = byte_0[i + l] | (byte_0[i + l + 1] << 8) | (byte_0[i + l + 2] << 16);
						num15 = ((num3 >> 12) ^ num3) & 0xFFF;
						array4[num15, array3[num15]++ & 0xF] = i + l;
					}
					i += num16;
					num2 = (num2 >> 1) | 0x80000000u;
					if (num16 != 3 || num19 > 63)
					{
						if (num16 == 3 && num19 <= 16383)
						{
							hMks2fmLNmoUej5peTdTvuc(array, num, (num19 << 2) | 1, 2);
							num += 2;
						}
						else if (num16 > 18 || num19 > 1023)
						{
							if (num16 <= 33)
							{
								hMks2fmLNmoUej5peTdTvuc(array, num, (num16 - 2 << 2) | (num19 << 7) | 3, 3);
								num += 3;
							}
							else
							{
								hMks2fmLNmoUej5peTdTvuc(array, num, (num16 - 3 << 7) | (num19 << 15) | 3, 4);
								num += 4;
							}
						}
						else
						{
							hMks2fmLNmoUej5peTdTvuc(array, num, (num16 - 3 << 2) | (num19 << 6) | 2, 2);
							num += 2;
						}
					}
					else
					{
						hMks2fmLNmoUej5peTdTvuc(array, num, num19 << 2, 1);
						num++;
					}
					num5 = 0;
				}
				else
				{
					array[num] = byte_0[i];
					num2 >>= 1;
					i++;
					num++;
				}
				continue;
			}
			while (i <= byte_0.Length - 1)
			{
				if ((num2 & 1) == 1)
				{
					hMks2fmLNmoUej5peTdTvuc(array, int_2, (int)(num2 >> 1) | int.MinValue, 4);
					int_2 = num;
					num += 4;
					num2 = 2147483648u;
				}
				array[num] = byte_0[i];
				i++;
				num++;
				num2 >>= 1;
			}
			while ((num2 & 1) != 1)
			{
				num2 >>= 1;
			}
			hMks2fmLNmoUej5peTdTvuc(array, int_2, (int)(num2 >> 1) | int.MinValue, 4);
			aYy0EM5Q4IUH3Y5o91eX_0024og(array, int_1, bool_0: true, byte_0.Length, num);
			array5 = new byte[num];
			smethod_2((Array)array, (Array)array5, num);
			return array5;
		}
		array5 = new byte[byte_0.Length + 9];
		aYy0EM5Q4IUH3Y5o91eX_0024og(array5, int_1, bool_0: false, byte_0.Length, byte_0.Length + 9);
		smethod_1((Array)byte_0, 0, (Array)array5, 9, byte_0.Length);
		return array5;
	}

	private static void hMks2fmLNmoUej5peTdTvuc(byte[] byte_0, int int_1, int int_2, int int_3)
	{
		for (int i = 0; i < int_3; i++)
		{
			byte_0[int_1 + i] = (byte)(int_2 >> i * 8);
		}
	}

	public static byte[] T6FSlkYzsIbsmI_74jwTbog(byte[] byte_0)
	{
		int num = T6sUldtwgK72ksilJRIE833as8UiijYSbElGWKbEaxsk(byte_0);
		int num2 = qMXAfJiiu8ZQs4W_0024qFik9Q4(byte_0);
		int num3 = 0;
		uint num4 = 1u;
		byte[] array = new byte[num];
		int[] array2 = new int[4096];
		byte[] array3 = new byte[4096];
		int num5 = num - 6 - 4 - 1;
		int num6 = -1;
		uint num7 = 0u;
		int num8 = (byte_0[0] >> 2) & 3;
		if (num8 != 1 && num8 != 3)
		{
			throw smethod_0(global::_003CModule_003E.smethod_27<string>(2597785358u));
		}
		if ((byte_0[0] & 1) != 1)
		{
			byte[] array4 = new byte[num];
			smethod_1((Array)byte_0, qMXAfJiiu8ZQs4W_0024qFik9Q4(byte_0), (Array)array4, 0, num);
			return array4;
		}
		while (true)
		{
			if (num4 == 1)
			{
				num4 = (uint)(byte_0[num2] | (byte_0[num2 + 1] << 8) | (byte_0[num2 + 2] << 16) | (byte_0[num2 + 3] << 24));
				num2 += 4;
				if (num3 <= num5)
				{
					num7 = (uint)((num8 == 1) ? (byte_0[num2] | (byte_0[num2 + 1] << 8) | (byte_0[num2 + 2] << 16)) : (byte_0[num2] | (byte_0[num2 + 1] << 8) | (byte_0[num2 + 2] << 16) | (byte_0[num2 + 3] << 24)));
				}
			}
			if ((num4 & 1) != 1)
			{
				if (num3 > num5)
				{
					break;
				}
				array[num3] = byte_0[num2];
				num3++;
				num2++;
				num4 >>= 1;
				if (num8 != 1)
				{
					num7 = (uint)(((num7 >> 8) & 0xFFFF) | (byte_0[num2 + 2] << 16) | (byte_0[num2 + 3] << 24));
					continue;
				}
				while (num6 < num3 - 3)
				{
					num6++;
					int num9 = array[num6] | (array[num6 + 1] << 8) | (array[num6 + 2] << 16);
					int num10 = ((num9 >> 12) ^ num9) & 0xFFF;
					array2[num10] = num6;
					array3[num10] = 1;
				}
				num7 = (uint)(((num7 >> 8) & 0xFFFF) | (byte_0[num2 + 2] << 16));
				continue;
			}
			num4 >>= 1;
			uint num11;
			uint num12;
			if (num8 == 1)
			{
				int num10 = ((int)num7 >> 4) & 0xFFF;
				num11 = (uint)array2[num10];
				if ((num7 & 0xF) != 0)
				{
					num12 = (num7 & 0xF) + 2;
					num2 += 2;
				}
				else
				{
					num12 = byte_0[num2 + 2];
					num2 += 3;
				}
			}
			else
			{
				uint num13;
				if ((num7 & 3) != 0)
				{
					if ((num7 & 2) != 0)
					{
						if ((num7 & 1) != 0)
						{
							if ((num7 & 0x7F) != 3)
							{
								num13 = (num7 >> 7) & 0x1FFFF;
								num12 = ((num7 >> 2) & 0x1F) + 2;
								num2 += 3;
							}
							else
							{
								num13 = num7 >> 15;
								num12 = ((num7 >> 7) & 0xFF) + 3;
								num2 += 4;
							}
						}
						else
						{
							num13 = (num7 & 0xFFFF) >> 6;
							num12 = ((num7 >> 2) & 0xF) + 3;
							num2 += 2;
						}
					}
					else
					{
						num13 = (num7 & 0xFFFF) >> 2;
						num12 = 3u;
						num2 += 2;
					}
				}
				else
				{
					num13 = (num7 & 0xFF) >> 2;
					num12 = 3u;
					num2++;
				}
				num11 = (uint)(num3 - num13);
			}
			array[num3] = array[num11];
			array[num3 + 1] = array[num11 + 1];
			array[num3 + 2] = array[num11 + 2];
			for (int i = 3; i < num12; i++)
			{
				array[num3 + i] = array[num11 + i];
			}
			num3 += (int)num12;
			if (num8 != 1)
			{
				num7 = (uint)(byte_0[num2] | (byte_0[num2 + 1] << 8) | (byte_0[num2 + 2] << 16) | (byte_0[num2 + 3] << 24));
			}
			else
			{
				num7 = (uint)(array[num6 + 1] | (array[num6 + 2] << 8) | (array[num6 + 3] << 16));
				while (num6 < num3 - num12)
				{
					num6++;
					int num10 = (int)(((num7 >> 12) ^ num7) & 0xFFF);
					array2[num10] = num6;
					array3[num10] = 1;
					num7 = (uint)(((num7 >> 8) & 0xFFFF) | (array[num6 + 3] << 16));
				}
				num7 = (uint)(byte_0[num2] | (byte_0[num2 + 1] << 8) | (byte_0[num2 + 2] << 16));
			}
			num6 = num3 - 1;
		}
		while (num3 <= num - 1)
		{
			if (num4 == 1)
			{
				num2 += 4;
				num4 = 2147483648u;
			}
			array[num3] = byte_0[num2];
			num3++;
			num2++;
			num4 >>= 1;
		}
		return array;
	}

	internal static ArgumentException smethod_0(string string_0)
	{
		return new ArgumentException(string_0);
	}

	internal static void smethod_1(Array array_0, int int_1, Array array_1, int int_2, int int_3)
	{
		Array.Copy(array_0, int_1, array_1, int_2, int_3);
	}

	internal static void smethod_2(Array array_0, Array array_1, int int_1)
	{
		Array.Copy(array_0, array_1, int_1);
	}
}
