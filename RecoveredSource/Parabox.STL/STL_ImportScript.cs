using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace Parabox.STL;

public class STL_ImportScript : MonoBehaviour
{
	private class Fh0REPSxYksZnjxp_bSZs4pn5okpYGpQrJJYO45q0OuM7ESPICKyoMrDNkX6Le4EzQ
	{
		public Vector3 vector3_0;

		public Vector3 YW4_00249UhLsnaLX5EfXiIZUrI;

		public Vector3 YqZYSHflFBL4r_NYQmm5obI;

		public Vector3 Y057NiFN1V_0024z1GSgr9HhJOs;

		public virtual string mHyrjjAfBJ8_YA1Ogdzh4Wc()
		{
			return smethod_0(global::_003CModule_003E.smethod_28<string>(1617452020u), new object[4] { vector3_0, YW4_00249UhLsnaLX5EfXiIZUrI, YqZYSHflFBL4r_NYQmm5obI, Y057NiFN1V_0024z1GSgr9HhJOs });
		}

		internal static string smethod_0(string string_0, object[] object_0)
		{
			return string.Format(string_0, object_0);
		}
	}

	private const int U4kwezrnUJDdOWwwSWVx884x3WV5ketDbEPh6lXjjSvJ = 21845;

	private const int Y_tqRYM58pjIq72uF7oVMG8 = 1;

	private const int f6fAKpeHcSSaDvm_Fldtf3U = 2;

	private const int C5mrZqk1cPmGgHvX1lT2xuc = 3;

	private const int OJvWlRJ5E4Cs48wx5ivqKEE = 4;

	private const int Ew_i_2MG4PR4pXVxQNR57iw = 5;

	private const int oDG4jXwtfwSwN9CL_0024vgPcGg = 6;

	private const int hGjBvmhzGNyq8ONJxJvBcO4 = 7;

	private const int Ga_puN6IjAYollFZKnFxlm8 = 0;

	public static Mesh[] Import(string path)
	{
		try
		{
			return km28lmyV7JxqwdT7oVoSpdM(path);
		}
		catch (Exception object_)
		{
			mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(smethod_3(global::_003CModule_003E.smethod_29<string>(3929003168u), (object)path, (object)smethod_2((object)object_)));
			return null;
		}
	}

	public static Mesh[] Import(Stream s)
	{
		try
		{
			return km28lmyV7JxqwdT7oVoSpdM(s);
		}
		catch (Exception object_)
		{
			mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(smethod_4(global::_003CModule_003E.smethod_26<string>(3588682777u), (object)smethod_2((object)object_)));
			return null;
		}
	}

	private static Mesh[] km28lmyV7JxqwdT7oVoSpdM(string string_0)
	{
		FileStream fileStream = smethod_5(string_0, FileMode.Open, FileAccess.Read);
		try
		{
			return km28lmyV7JxqwdT7oVoSpdM(fileStream);
		}
		finally
		{
			if (fileStream != null)
			{
				smethod_6((IDisposable)fileStream);
			}
		}
	}

	private static Mesh[] km28lmyV7JxqwdT7oVoSpdM(Stream stream_0)
	{
		List<Fh0REPSxYksZnjxp_bSZs4pn5okpYGpQrJJYO45q0OuM7ESPICKyoMrDNkX6Le4EzQ> list = new List<Fh0REPSxYksZnjxp_bSZs4pn5okpYGpQrJJYO45q0OuM7ESPICKyoMrDNkX6Le4EzQ>();
		BinaryReader binaryReader = smethod_8(stream_0, (Encoding)smethod_7());
		try
		{
			while (smethod_15(smethod_14(binaryReader)) < smethod_16(smethod_14(binaryReader)))
			{
				smethod_9(binaryReader, 80);
				uint num = smethod_10(binaryReader);
				for (uint num2 = 0u; num2 < num; num2++)
				{
					try
					{
						Fh0REPSxYksZnjxp_bSZs4pn5okpYGpQrJJYO45q0OuM7ESPICKyoMrDNkX6Le4EzQ fh0REPSxYksZnjxp_bSZs4pn5okpYGpQrJJYO45q0OuM7ESPICKyoMrDNkX6Le4EzQ = new Fh0REPSxYksZnjxp_bSZs4pn5okpYGpQrJJYO45q0OuM7ESPICKyoMrDNkX6Le4EzQ();
						fh0REPSxYksZnjxp_bSZs4pn5okpYGpQrJJYO45q0OuM7ESPICKyoMrDNkX6Le4EzQ.vector3_0.x = smethod_11(binaryReader);
						fh0REPSxYksZnjxp_bSZs4pn5okpYGpQrJJYO45q0OuM7ESPICKyoMrDNkX6Le4EzQ.vector3_0.y = smethod_11(binaryReader);
						fh0REPSxYksZnjxp_bSZs4pn5okpYGpQrJJYO45q0OuM7ESPICKyoMrDNkX6Le4EzQ.vector3_0.z = smethod_11(binaryReader);
						fh0REPSxYksZnjxp_bSZs4pn5okpYGpQrJJYO45q0OuM7ESPICKyoMrDNkX6Le4EzQ.YW4_00249UhLsnaLX5EfXiIZUrI.x = smethod_11(binaryReader);
						fh0REPSxYksZnjxp_bSZs4pn5okpYGpQrJJYO45q0OuM7ESPICKyoMrDNkX6Le4EzQ.YW4_00249UhLsnaLX5EfXiIZUrI.y = smethod_11(binaryReader);
						fh0REPSxYksZnjxp_bSZs4pn5okpYGpQrJJYO45q0OuM7ESPICKyoMrDNkX6Le4EzQ.YW4_00249UhLsnaLX5EfXiIZUrI.z = smethod_11(binaryReader);
						fh0REPSxYksZnjxp_bSZs4pn5okpYGpQrJJYO45q0OuM7ESPICKyoMrDNkX6Le4EzQ.YqZYSHflFBL4r_NYQmm5obI.x = smethod_11(binaryReader);
						fh0REPSxYksZnjxp_bSZs4pn5okpYGpQrJJYO45q0OuM7ESPICKyoMrDNkX6Le4EzQ.YqZYSHflFBL4r_NYQmm5obI.y = smethod_11(binaryReader);
						fh0REPSxYksZnjxp_bSZs4pn5okpYGpQrJJYO45q0OuM7ESPICKyoMrDNkX6Le4EzQ.YqZYSHflFBL4r_NYQmm5obI.z = smethod_11(binaryReader);
						fh0REPSxYksZnjxp_bSZs4pn5okpYGpQrJJYO45q0OuM7ESPICKyoMrDNkX6Le4EzQ.Y057NiFN1V_0024z1GSgr9HhJOs.x = smethod_11(binaryReader);
						fh0REPSxYksZnjxp_bSZs4pn5okpYGpQrJJYO45q0OuM7ESPICKyoMrDNkX6Le4EzQ.Y057NiFN1V_0024z1GSgr9HhJOs.y = smethod_11(binaryReader);
						fh0REPSxYksZnjxp_bSZs4pn5okpYGpQrJJYO45q0OuM7ESPICKyoMrDNkX6Le4EzQ.Y057NiFN1V_0024z1GSgr9HhJOs.z = smethod_11(binaryReader);
						list.Add(fh0REPSxYksZnjxp_bSZs4pn5okpYGpQrJJYO45q0OuM7ESPICKyoMrDNkX6Le4EzQ);
						smethod_12(binaryReader);
					}
					catch (Exception exception_)
					{
						mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(smethod_13(exception_));
					}
				}
			}
		}
		finally
		{
			if (binaryReader != null)
			{
				smethod_6((IDisposable)binaryReader);
			}
		}
		return smethod_1(list);
	}

	private static int N68D1uwN1h2r7RNO4IKtMFc(string string_0)
	{
		if (smethod_17(string_0, global::_003CModule_003E.smethod_27<string>(3448130024u)))
		{
			return 1;
		}
		if (smethod_17(string_0, global::_003CModule_003E.smethod_27<string>(3061908513u)))
		{
			return 2;
		}
		if (smethod_17(string_0, global::_003CModule_003E.smethod_26<string>(1301647177u)))
		{
			return 3;
		}
		if (smethod_17(string_0, global::_003CModule_003E.smethod_27<string>(744579447u)))
		{
			return 4;
		}
		if (!smethod_17(string_0, global::_003CModule_003E.smethod_28<string>(2436991129u)))
		{
			if (smethod_17(string_0, global::_003CModule_003E.smethod_27<string>(3248031213u)))
			{
				return 6;
			}
			if (smethod_17(string_0, global::_003CModule_003E.smethod_28<string>(1890631723u)))
			{
				return 7;
			}
			return 0;
		}
		return 5;
	}

	internal static Vector3 smethod_0(string string_0)
	{
		string[] array = smethod_19(smethod_18(string_0), (char[])null);
		Vector3 result = default(Vector3);
		float.TryParse(array[0], out result.x);
		float.TryParse(array[1], out result.y);
		float.TryParse(array[2], out result.z);
		return result;
	}

	private static Mesh[] smethod_1(IList<Fh0REPSxYksZnjxp_bSZs4pn5okpYGpQrJJYO45q0OuM7ESPICKyoMrDNkX6Le4EzQ> ilist_0)
	{
		int count = ilist_0.Count;
		int num = 0;
		int int_ = 65535;
		Mesh[] array = new Mesh[count / 21845 + 1];
		for (int i = 0; i < array.Length; i++)
		{
			int num2 = smethod_20(int_, (count - num) * 3);
			Vector3[] array2 = new Vector3[num2];
			Vector3[] array3 = new Vector3[num2];
			int[] array4 = new int[num2];
			for (int j = 0; j < num2; j += 3)
			{
				array2[j] = ilist_0[num].YW4_00249UhLsnaLX5EfXiIZUrI;
				array2[j + 1] = ilist_0[num].YqZYSHflFBL4r_NYQmm5obI;
				array2[j + 2] = ilist_0[num].Y057NiFN1V_0024z1GSgr9HhJOs;
				array3[j] = ilist_0[num].vector3_0;
				array3[j + 1] = ilist_0[num].vector3_0;
				array3[j + 2] = ilist_0[num].vector3_0;
				array4[j] = j;
				array4[j + 1] = j + 1;
				array4[j + 2] = j + 2;
				num++;
			}
			array[i] = smethod_21();
			smethod_22(array[i], array2);
			smethod_23(array[i], array3);
			smethod_24(array[i], array4);
		}
		return array;
	}

	internal static string smethod_2(object object_0)
	{
		return object_0.ToString();
	}

	internal static string smethod_3(string string_0, object object_0, object object_1)
	{
		return string.Format(string_0, object_0, object_1);
	}

	internal static string smethod_4(string string_0, object object_0)
	{
		return string.Format(string_0, object_0);
	}

	internal static FileStream smethod_5(string string_0, FileMode fileMode_0, FileAccess fileAccess_0)
	{
		return new FileStream(string_0, fileMode_0, fileAccess_0);
	}

	internal static void smethod_6(IDisposable idisposable_0)
	{
		idisposable_0.Dispose();
	}

	internal static ASCIIEncoding smethod_7()
	{
		return new ASCIIEncoding();
	}

	internal static BinaryReader smethod_8(Stream stream_0, Encoding encoding_0)
	{
		return new BinaryReader(stream_0, encoding_0);
	}

	internal static byte[] smethod_9(BinaryReader binaryReader_0, int int_0)
	{
		return binaryReader_0.ReadBytes(int_0);
	}

	internal static uint smethod_10(BinaryReader binaryReader_0)
	{
		return binaryReader_0.ReadUInt32();
	}

	internal static float smethod_11(BinaryReader binaryReader_0)
	{
		return binaryReader_0.ReadSingle();
	}

	internal static ushort smethod_12(BinaryReader binaryReader_0)
	{
		return binaryReader_0.ReadUInt16();
	}

	internal static string smethod_13(Exception exception_0)
	{
		return exception_0.Message;
	}

	internal static Stream smethod_14(BinaryReader binaryReader_0)
	{
		return binaryReader_0.BaseStream;
	}

	internal static long smethod_15(Stream stream_0)
	{
		return stream_0.Position;
	}

	internal static long smethod_16(Stream stream_0)
	{
		return stream_0.Length;
	}

	internal static bool smethod_17(string string_0, string string_1)
	{
		return string_0.StartsWith(string_1);
	}

	internal static string smethod_18(string string_0)
	{
		return string_0.Trim();
	}

	internal static string[] smethod_19(string string_0, char[] char_0)
	{
		return string_0.Split(char_0);
	}

	internal static int smethod_20(int int_0, int int_1)
	{
		return Math.Min(int_0, int_1);
	}

	internal static Mesh smethod_21()
	{
		return new Mesh();
	}

	internal static void smethod_22(Mesh mesh_0, Vector3[] vector3_0)
	{
		mesh_0.vertices = vector3_0;
	}

	internal static void smethod_23(Mesh mesh_0, Vector3[] vector3_0)
	{
		mesh_0.normals = vector3_0;
	}

	internal static void smethod_24(Mesh mesh_0, int[] int_0)
	{
		mesh_0.triangles = int_0;
	}
}
