using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Text;

namespace RestSharp.Contrib;

public sealed class HttpUtility
{
	private sealed class uNiJoZCeIVzpy9AjsWfw_m0NkU15KE_0024A0jmdehswJT3bpimMa6reJdJ0gt0htRgcPQ : NameValueCollection
	{
		public string mHyrjjAfBJ8_YA1Ogdzh4Wc()
		{
			int num = smethod_0((NameObjectCollectionBase)this);
			if (num == 0)
			{
				return "";
			}
			StringBuilder stringBuilder = smethod_1();
			string[] array = smethod_2((NameValueCollection)this);
			for (int i = 0; i < num; i++)
			{
				smethod_4(stringBuilder, global::_003CModule_003E.smethod_28<string>(2866581153u), (object)array[i], (object)smethod_3((NameValueCollection)this, array[i]));
			}
			if (smethod_5(stringBuilder) > 0)
			{
				int num2 = smethod_5(stringBuilder);
				smethod_6(stringBuilder, num2 - 1);
			}
			return smethod_7((object)stringBuilder);
		}

		internal static int smethod_0(NameObjectCollectionBase nameObjectCollectionBase_0)
		{
			return nameObjectCollectionBase_0.Count;
		}

		internal static StringBuilder smethod_1()
		{
			return new StringBuilder();
		}

		internal static string[] smethod_2(NameValueCollection nameValueCollection_0)
		{
			return nameValueCollection_0.AllKeys;
		}

		internal static string smethod_3(NameValueCollection nameValueCollection_0, string string_0)
		{
			return nameValueCollection_0[string_0];
		}

		internal static StringBuilder smethod_4(StringBuilder stringBuilder_0, string string_0, object object_0, object object_1)
		{
			return stringBuilder_0.AppendFormat(string_0, object_0, object_1);
		}

		internal static int smethod_5(StringBuilder stringBuilder_0)
		{
			return stringBuilder_0.Length;
		}

		internal static void smethod_6(StringBuilder stringBuilder_0, int int_0)
		{
			stringBuilder_0.Length = int_0;
		}

		internal static string smethod_7(object object_0)
		{
			return object_0.ToString();
		}
	}

	public static void HtmlAttributeEncode(string s, TextWriter output)
	{
		if (output == null)
		{
			throw smethod_1(global::_003CModule_003E.smethod_27<string>(1146129749u));
		}
		smethod_2(output, e3bYTbA5Gz_00247eYOyWWEMD5zb1FSjwWVV08_WpOF28HKM.n7OLnG8giPJuT8ry9pblVCTABeHin3J4UzwppKOvkEy5(s));
	}

	public static string HtmlAttributeEncode(string s)
	{
		return e3bYTbA5Gz_00247eYOyWWEMD5zb1FSjwWVV08_WpOF28HKM.n7OLnG8giPJuT8ry9pblVCTABeHin3J4UzwppKOvkEy5(s);
	}

	public static string UrlDecode(string str)
	{
		return UrlDecode(str, smethod_3());
	}

	private static char[] o2Yjnnv52IJDOYUivOvBvYI(MemoryStream memoryStream_0, Encoding encoding_0)
	{
		return smethod_6(encoding_0, smethod_4(memoryStream_0), 0, (int)smethod_5((Stream)memoryStream_0));
	}

	private static void glz696cBSG5sRHhtd1zzJGo(IList ilist_0, char char_0, Encoding encoding_0)
	{
		if (char_0 > 'ÿ')
		{
			byte[] array = smethod_7(encoding_0, new char[1] { char_0 });
			foreach (byte b in array)
			{
				smethod_8(ilist_0, (object)b);
			}
		}
		else
		{
			smethod_8(ilist_0, (object)(byte)char_0);
		}
	}

	public static string UrlDecode(string s, Encoding e)
	{
		if (s == null)
		{
			return null;
		}
		if (smethod_9(s, '%') == -1 && smethod_9(s, '+') == -1)
		{
			return s;
		}
		if (e == null)
		{
			e = smethod_3();
		}
		long num = smethod_10(s);
		List<byte> list = new List<byte>();
		for (int i = 0; i < num; i++)
		{
			char c = smethod_11(s, i);
			if (c == '%' && i + 2 < num && smethod_11(s, i + 1) != '%')
			{
				int num2;
				if (smethod_11(s, i + 1) == 'u' && i + 5 < num)
				{
					num2 = EK2klADBGJ5gpsZDugnlZ_00244(s, i + 2, 4);
					if (num2 == -1)
					{
						glz696cBSG5sRHhtd1zzJGo(list, '%', e);
						continue;
					}
					glz696cBSG5sRHhtd1zzJGo(list, (char)num2, e);
					i += 5;
				}
				else if ((num2 = EK2klADBGJ5gpsZDugnlZ_00244(s, i + 1, 2)) == -1)
				{
					glz696cBSG5sRHhtd1zzJGo(list, '%', e);
				}
				else
				{
					glz696cBSG5sRHhtd1zzJGo(list, (char)num2, e);
					i += 2;
				}
			}
			else if (c != '+')
			{
				glz696cBSG5sRHhtd1zzJGo(list, c, e);
			}
			else
			{
				glz696cBSG5sRHhtd1zzJGo(list, ' ', e);
			}
		}
		byte[] byte_ = list.ToArray();
		list = null;
		return smethod_12(e, byte_);
	}

	public static string UrlDecode(byte[] bytes, Encoding e)
	{
		if (bytes == null)
		{
			return null;
		}
		return UrlDecode(bytes, 0, bytes.Length, e);
	}

	internal static int smethod_0(byte byte_0)
	{
		char c = (char)byte_0;
		if (c >= '0' && c <= '9')
		{
			return c - 48;
		}
		if (c >= 'a' && c <= 'f')
		{
			return c - 97 + 10;
		}
		if (c >= 'A' && c <= 'F')
		{
			return c - 65 + 10;
		}
		return -1;
	}

	private static int EK2klADBGJ5gpsZDugnlZ_00244(byte[] byte_0, int int_0, int int_1)
	{
		int num = 0;
		int num2 = int_1 + int_0;
		int num3 = int_0;
		while (true)
		{
			if (num3 < num2)
			{
				int num4 = smethod_0(byte_0[num3]);
				if (num4 == -1)
				{
					break;
				}
				num = (num << 4) + num4;
				num3++;
				continue;
			}
			return num;
		}
		return -1;
	}

	private static int EK2klADBGJ5gpsZDugnlZ_00244(string string_0, int int_0, int int_1)
	{
		int num = 0;
		int num2 = int_1 + int_0;
		for (int i = int_0; i < num2; i++)
		{
			char c = smethod_11(string_0, i);
			if (c <= '\u007f')
			{
				int num3 = smethod_0((byte)c);
				if (num3 != -1)
				{
					num = (num << 4) + num3;
					continue;
				}
				return -1;
			}
			return -1;
		}
		return num;
	}

	public static string UrlDecode(byte[] bytes, int offset, int count, Encoding e)
	{
		if (bytes == null)
		{
			return null;
		}
		if (count != 0)
		{
			if (bytes != null)
			{
				if (offset >= 0 && offset <= bytes.Length)
				{
					if (count >= 0 && offset + count <= bytes.Length)
					{
						StringBuilder stringBuilder = smethod_15();
						MemoryStream memoryStream = smethod_16();
						int num = count + offset;
						for (int i = offset; i < num; i++)
						{
							if (bytes[i] == 37 && i + 2 < count && bytes[i + 1] != 37)
							{
								int num2;
								if (bytes[i + 1] == 117 && i + 5 < num)
								{
									if (smethod_5((Stream)memoryStream) > 0L)
									{
										smethod_17(stringBuilder, o2Yjnnv52IJDOYUivOvBvYI(memoryStream, e));
										smethod_18((Stream)memoryStream, 0L);
									}
									num2 = EK2klADBGJ5gpsZDugnlZ_00244(bytes, i + 2, 4);
									if (num2 != -1)
									{
										smethod_19(stringBuilder, (char)num2);
										i += 5;
										continue;
									}
								}
								else if ((num2 = EK2klADBGJ5gpsZDugnlZ_00244(bytes, i + 1, 2)) != -1)
								{
									smethod_20((Stream)memoryStream, (byte)num2);
									i += 2;
									continue;
								}
							}
							if (smethod_5((Stream)memoryStream) > 0L)
							{
								smethod_17(stringBuilder, o2Yjnnv52IJDOYUivOvBvYI(memoryStream, e));
								smethod_18((Stream)memoryStream, 0L);
							}
							if (bytes[i] == 43)
							{
								smethod_19(stringBuilder, ' ');
							}
							else
							{
								smethod_19(stringBuilder, (char)bytes[i]);
							}
						}
						if (smethod_5((Stream)memoryStream) > 0L)
						{
							smethod_17(stringBuilder, o2Yjnnv52IJDOYUivOvBvYI(memoryStream, e));
						}
						memoryStream = null;
						return smethod_21((object)stringBuilder);
					}
					throw smethod_14(global::_003CModule_003E.smethod_26<string>(2895313613u));
				}
				throw smethod_14(global::_003CModule_003E.smethod_25<string>(466540339u));
			}
			throw smethod_13(global::_003CModule_003E.smethod_28<string>(1590853297u));
		}
		return string.Empty;
	}

	public static byte[] UrlDecodeToBytes(byte[] bytes)
	{
		if (bytes == null)
		{
			return null;
		}
		return UrlDecodeToBytes(bytes, 0, bytes.Length);
	}

	public static byte[] UrlDecodeToBytes(string str)
	{
		return UrlDecodeToBytes(str, smethod_3());
	}

	public static byte[] UrlDecodeToBytes(string str, Encoding e)
	{
		if (str == null)
		{
			return null;
		}
		if (e == null)
		{
			throw smethod_13(global::_003CModule_003E.smethod_29<string>(1482460889u));
		}
		return UrlDecodeToBytes(smethod_22(e, str));
	}

	public static byte[] UrlDecodeToBytes(byte[] bytes, int offset, int count)
	{
		if (bytes == null)
		{
			return null;
		}
		if (count != 0)
		{
			int num = bytes.Length;
			if (offset < 0 || offset >= num)
			{
				throw smethod_14(global::_003CModule_003E.smethod_28<string>(1317673594u));
			}
			if (count >= 0 && offset <= num - count)
			{
				MemoryStream memoryStream = smethod_16();
				int num2 = offset + count;
				for (int i = offset; i < num2; i++)
				{
					char c = (char)bytes[i];
					switch (c)
					{
					case '%':
						if (i < num2 - 2)
						{
							int num3 = EK2klADBGJ5gpsZDugnlZ_00244(bytes, i + 1, 2);
							if (num3 != -1)
							{
								c = (char)num3;
								i += 2;
							}
						}
						break;
					case '+':
						c = ' ';
						break;
					}
					smethod_20((Stream)memoryStream, (byte)c);
				}
				return smethod_23(memoryStream);
			}
			throw smethod_14(global::_003CModule_003E.smethod_26<string>(2895313613u));
		}
		return new byte[0];
	}

	public static string UrlEncode(string str)
	{
		return UrlEncode(str, smethod_3());
	}

	public static string UrlEncode(string s, Encoding Enc)
	{
		if (s == null)
		{
			return null;
		}
		if (smethod_24(s, string.Empty))
		{
			return string.Empty;
		}
		bool flag = false;
		int num = smethod_10(s);
		for (int i = 0; i < num; i++)
		{
			char c = smethod_11(s, i);
			if ((c < '0' || (c < 'A' && c > '9') || (c > 'Z' && c < 'a') || c > 'z') && !e3bYTbA5Gz_00247eYOyWWEMD5zb1FSjwWVV08_WpOF28HKM._0024yO_00245VuuyzBUnRL70OiY8L8(c))
			{
				flag = true;
				break;
			}
		}
		if (flag)
		{
			byte[] array = new byte[smethod_25(Enc, smethod_10(s))];
			int count = smethod_26(Enc, s, 0, smethod_10(s), array, 0);
			return smethod_12(smethod_27(), UrlEncodeToBytes(array, 0, count));
		}
		return s;
	}

	public static string UrlEncode(byte[] bytes)
	{
		if (bytes == null)
		{
			return null;
		}
		if (bytes.Length == 0)
		{
			return string.Empty;
		}
		return smethod_12(smethod_27(), UrlEncodeToBytes(bytes, 0, bytes.Length));
	}

	public static string UrlEncode(byte[] bytes, int offset, int count)
	{
		if (bytes == null)
		{
			return null;
		}
		if (bytes.Length != 0)
		{
			return smethod_12(smethod_27(), UrlEncodeToBytes(bytes, offset, count));
		}
		return string.Empty;
	}

	public static byte[] UrlEncodeToBytes(string str)
	{
		return UrlEncodeToBytes(str, smethod_3());
	}

	public static byte[] UrlEncodeToBytes(string str, Encoding e)
	{
		if (str == null)
		{
			return null;
		}
		if (smethod_10(str) != 0)
		{
			byte[] array = smethod_22(e, str);
			return UrlEncodeToBytes(array, 0, array.Length);
		}
		return new byte[0];
	}

	public static byte[] UrlEncodeToBytes(byte[] bytes)
	{
		if (bytes == null)
		{
			return null;
		}
		if (bytes.Length != 0)
		{
			return UrlEncodeToBytes(bytes, 0, bytes.Length);
		}
		return new byte[0];
	}

	public static byte[] UrlEncodeToBytes(byte[] bytes, int offset, int count)
	{
		if (bytes == null)
		{
			return null;
		}
		return e3bYTbA5Gz_00247eYOyWWEMD5zb1FSjwWVV08_WpOF28HKM.q78VQlziti3KdFnO_0024gfzcZD5Id2sNF87OoaeAkvt9_EB(bytes, offset, count);
	}

	public static string UrlEncodeUnicode(string str)
	{
		if (str == null)
		{
			return null;
		}
		return smethod_12(smethod_27(), UrlEncodeUnicodeToBytes(str));
	}

	public static byte[] UrlEncodeUnicodeToBytes(string str)
	{
		if (str == null)
		{
			return null;
		}
		if (smethod_10(str) == 0)
		{
			return new byte[0];
		}
		MemoryStream memoryStream = smethod_28(smethod_10(str));
		for (int i = 0; i < smethod_10(str); i++)
		{
			e3bYTbA5Gz_00247eYOyWWEMD5zb1FSjwWVV08_WpOF28HKM.e_wJElkLP1OPJQmOrcLsvMI(smethod_11(str, i), memoryStream, bool_0: true);
		}
		return smethod_23(memoryStream);
	}

	public static string HtmlDecode(string s)
	{
		return e3bYTbA5Gz_00247eYOyWWEMD5zb1FSjwWVV08_WpOF28HKM.smethod_1(s);
	}

	public static void HtmlDecode(string s, TextWriter output)
	{
		if (output == null)
		{
			throw smethod_1(global::_003CModule_003E.smethod_28<string>(1136443034u));
		}
		if (!smethod_29(s))
		{
			smethod_2(output, e3bYTbA5Gz_00247eYOyWWEMD5zb1FSjwWVV08_WpOF28HKM.smethod_1(s));
		}
	}

	public static string HtmlEncode(string s)
	{
		return e3bYTbA5Gz_00247eYOyWWEMD5zb1FSjwWVV08_WpOF28HKM.vZwKCch_0024OsKBkIgfRJDBjTU(s);
	}

	public static void HtmlEncode(string s, TextWriter output)
	{
		if (output == null)
		{
			throw smethod_1(global::_003CModule_003E.smethod_29<string>(3397367172u));
		}
		if (!smethod_29(s))
		{
			smethod_2(output, e3bYTbA5Gz_00247eYOyWWEMD5zb1FSjwWVV08_WpOF28HKM.vZwKCch_0024OsKBkIgfRJDBjTU(s));
		}
	}

	public static string UrlPathEncode(string s)
	{
		return e3bYTbA5Gz_00247eYOyWWEMD5zb1FSjwWVV08_WpOF28HKM.Yi4Dy3ncj_00248_DSHFz13sZII(s);
	}

	public static NameValueCollection ParseQueryString(string query)
	{
		return ParseQueryString(query, smethod_3());
	}

	public static NameValueCollection ParseQueryString(string query, Encoding encoding)
	{
		if (query == null)
		{
			throw smethod_13(global::_003CModule_003E.smethod_25<string>(1618487524u));
		}
		if (encoding == null)
		{
			throw smethod_13(global::_003CModule_003E.smethod_29<string>(3116053794u));
		}
		if (smethod_10(query) != 0 && (smethod_10(query) != 1 || smethod_11(query, 0) != '?'))
		{
			if (smethod_11(query, 0) == '?')
			{
				query = smethod_31(query, 1);
			}
			NameValueCollection nameValueCollection = new uNiJoZCeIVzpy9AjsWfw_m0NkU15KE_0024A0jmdehswJT3bpimMa6reJdJ0gt0htRgcPQ();
			orwD3W6_0024tmOMwudOwN1FqX__0024f92BBa0kOYLfeBge0eEs(query, encoding, nameValueCollection);
			return nameValueCollection;
		}
		return smethod_30();
	}

	internal static void orwD3W6_0024tmOMwudOwN1FqX__0024f92BBa0kOYLfeBge0eEs(string string_0, Encoding encoding_0, NameValueCollection nameValueCollection_0)
	{
		if (smethod_10(string_0) == 0)
		{
			return;
		}
		string string_1 = HtmlDecode(string_0);
		int num = smethod_10(string_1);
		int num2 = 0;
		bool flag = true;
		while (num2 <= num)
		{
			int num3 = -1;
			int num4 = -1;
			for (int i = num2; i < num; i++)
			{
				if (num3 == -1 && smethod_11(string_1, i) == '=')
				{
					num3 = i + 1;
				}
				else if (smethod_11(string_1, i) == '&')
				{
					num4 = i;
					break;
				}
			}
			if (flag)
			{
				flag = false;
				if (smethod_11(string_1, num2) == '?')
				{
					num2++;
				}
			}
			string string_2;
			if (num3 != -1)
			{
				string_2 = UrlDecode(smethod_32(string_1, num2, num3 - num2 - 1), encoding_0);
			}
			else
			{
				string_2 = null;
				num3 = num2;
			}
			if (num4 < 0)
			{
				num2 = -1;
				num4 = smethod_10(string_1);
			}
			else
			{
				num2 = num4 + 1;
			}
			string string_3 = UrlDecode(smethod_32(string_1, num3, num4 - num3), encoding_0);
			smethod_33(nameValueCollection_0, string_2, string_3);
			if (num2 == -1)
			{
				break;
			}
		}
	}

	internal static NullReferenceException smethod_1(string string_0)
	{
		return new NullReferenceException(string_0);
	}

	internal static void smethod_2(TextWriter textWriter_0, string string_0)
	{
		textWriter_0.Write(string_0);
	}

	internal static Encoding smethod_3()
	{
		return Encoding.UTF8;
	}

	internal static byte[] smethod_4(MemoryStream memoryStream_0)
	{
		return memoryStream_0.GetBuffer();
	}

	internal static long smethod_5(Stream stream_0)
	{
		return stream_0.Length;
	}

	internal static char[] smethod_6(Encoding encoding_0, byte[] byte_0, int int_0, int int_1)
	{
		return encoding_0.GetChars(byte_0, int_0, int_1);
	}

	internal static byte[] smethod_7(Encoding encoding_0, char[] char_0)
	{
		return encoding_0.GetBytes(char_0);
	}

	internal static int smethod_8(IList ilist_0, object object_0)
	{
		return ilist_0.Add(object_0);
	}

	internal static int smethod_9(string string_0, char char_0)
	{
		return string_0.IndexOf(char_0);
	}

	internal static int smethod_10(string string_0)
	{
		return string_0.Length;
	}

	internal static char smethod_11(string string_0, int int_0)
	{
		return string_0[int_0];
	}

	internal static string smethod_12(Encoding encoding_0, byte[] byte_0)
	{
		return encoding_0.GetString(byte_0);
	}

	internal static ArgumentNullException smethod_13(string string_0)
	{
		return new ArgumentNullException(string_0);
	}

	internal static ArgumentOutOfRangeException smethod_14(string string_0)
	{
		return new ArgumentOutOfRangeException(string_0);
	}

	internal static StringBuilder smethod_15()
	{
		return new StringBuilder();
	}

	internal static MemoryStream smethod_16()
	{
		return new MemoryStream();
	}

	internal static StringBuilder smethod_17(StringBuilder stringBuilder_0, char[] char_0)
	{
		return stringBuilder_0.Append(char_0);
	}

	internal static void smethod_18(Stream stream_0, long long_0)
	{
		stream_0.SetLength(long_0);
	}

	internal static StringBuilder smethod_19(StringBuilder stringBuilder_0, char char_0)
	{
		return stringBuilder_0.Append(char_0);
	}

	internal static void smethod_20(Stream stream_0, byte byte_0)
	{
		stream_0.WriteByte(byte_0);
	}

	internal static string smethod_21(object object_0)
	{
		return object_0.ToString();
	}

	internal static byte[] smethod_22(Encoding encoding_0, string string_0)
	{
		return encoding_0.GetBytes(string_0);
	}

	internal static byte[] smethod_23(MemoryStream memoryStream_0)
	{
		return memoryStream_0.ToArray();
	}

	internal static bool smethod_24(string string_0, string string_1)
	{
		return string_0 == string_1;
	}

	internal static int smethod_25(Encoding encoding_0, int int_0)
	{
		return encoding_0.GetMaxByteCount(int_0);
	}

	internal static int smethod_26(Encoding encoding_0, string string_0, int int_0, int int_1, byte[] byte_0, int int_2)
	{
		return encoding_0.GetBytes(string_0, int_0, int_1, byte_0, int_2);
	}

	internal static Encoding smethod_27()
	{
		return Encoding.ASCII;
	}

	internal static MemoryStream smethod_28(int int_0)
	{
		return new MemoryStream(int_0);
	}

	internal static bool smethod_29(string string_0)
	{
		return string.IsNullOrEmpty(string_0);
	}

	internal static NameValueCollection smethod_30()
	{
		return new NameValueCollection();
	}

	internal static string smethod_31(string string_0, int int_0)
	{
		return string_0.Substring(int_0);
	}

	internal static string smethod_32(string string_0, int int_0, int int_1)
	{
		return string_0.Substring(int_0, int_1);
	}

	internal static void smethod_33(NameValueCollection nameValueCollection_0, string string_0, string string_1)
	{
		nameValueCollection_0.Add(string_0, string_1);
	}
}
