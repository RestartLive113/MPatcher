using System.Collections.Generic;
using System.IO;

internal class CK1GMyRHIE5KyvvFyrOx6VJPIKu5V0GvrNJUjmuLjN3d
{
	private struct IS5FmkRKimQylN9660UHlPgtDlGiLnFMN6Le7PlZgAGXbqwkTz71beRbWh0Pf9IsPjntv5Bt71Gf4_0024khpW1rk6U
	{
		internal string r7lh6mDSmj85joUg2_0024t28Sg;

		internal int aeK4XfFtH4Rk6VMllyUXIeQ;

		internal long sC7TVlJaywRg_0024a_5dqujETk;
	}

	private string _bprWeQ20Hh2U_a_XQFSeAY;

	private int BZQuBAZKGg3ZArpufUpjd2Y;

	private BinaryReader mD0obrPy9SdttIZRHX_0024wCDM;

	private List<IS5FmkRKimQylN9660UHlPgtDlGiLnFMN6Le7PlZgAGXbqwkTz71beRbWh0Pf9IsPjntv5Bt71Gf4_0024khpW1rk6U> xyw9N5w4G4ePZgbb54Co3FA = new List<IS5FmkRKimQylN9660UHlPgtDlGiLnFMN6Le7PlZgAGXbqwkTz71beRbWh0Pf9IsPjntv5Bt71Gf4_0024khpW1rk6U>();

	internal CK1GMyRHIE5KyvvFyrOx6VJPIKu5V0GvrNJUjmuLjN3d(string string_0)
	{
		if (!smethod_0(string_0))
		{
			throw smethod_1(global::_003CModule_003E.smethod_27<string>(4208118700u));
		}
		_bprWeQ20Hh2U_a_XQFSeAY = string_0;
		mD0obrPy9SdttIZRHX_0024wCDM = smethod_3((Stream)smethod_2(string_0, FileMode.Open));
		BZQuBAZKGg3ZArpufUpjd2Y = smethod_4(mD0obrPy9SdttIZRHX_0024wCDM);
		for (int i = 0; i < BZQuBAZKGg3ZArpufUpjd2Y; i++)
		{
			IS5FmkRKimQylN9660UHlPgtDlGiLnFMN6Le7PlZgAGXbqwkTz71beRbWh0Pf9IsPjntv5Bt71Gf4_0024khpW1rk6U item = new IS5FmkRKimQylN9660UHlPgtDlGiLnFMN6Le7PlZgAGXbqwkTz71beRbWh0Pf9IsPjntv5Bt71Gf4_0024khpW1rk6U
			{
				r7lh6mDSmj85joUg2_0024t28Sg = smethod_5(mD0obrPy9SdttIZRHX_0024wCDM),
				aeK4XfFtH4Rk6VMllyUXIeQ = smethod_4(mD0obrPy9SdttIZRHX_0024wCDM),
				sC7TVlJaywRg_0024a_5dqujETk = smethod_7(smethod_6(mD0obrPy9SdttIZRHX_0024wCDM))
			};
			Stream stream_ = smethod_6(mD0obrPy9SdttIZRHX_0024wCDM);
			smethod_8(stream_, smethod_7(stream_) + item.aeK4XfFtH4Rk6VMllyUXIeQ);
			xyw9N5w4G4ePZgbb54Co3FA.Add(item);
		}
	}

	private IS5FmkRKimQylN9660UHlPgtDlGiLnFMN6Le7PlZgAGXbqwkTz71beRbWh0Pf9IsPjntv5Bt71Gf4_0024khpW1rk6U? nlaag_4hn3_0024dv1Pp4pGKgyg(string string_0)
	{
		foreach (IS5FmkRKimQylN9660UHlPgtDlGiLnFMN6Le7PlZgAGXbqwkTz71beRbWh0Pf9IsPjntv5Bt71Gf4_0024khpW1rk6U item in xyw9N5w4G4ePZgbb54Co3FA)
		{
			if (smethod_9(string_0, item.r7lh6mDSmj85joUg2_0024t28Sg))
			{
				return item;
			}
		}
		return null;
	}

	internal byte[] method_0(string string_0, bool bool_0 = false)
	{
		IS5FmkRKimQylN9660UHlPgtDlGiLnFMN6Le7PlZgAGXbqwkTz71beRbWh0Pf9IsPjntv5Bt71Gf4_0024khpW1rk6U? iS5FmkRKimQylN9660UHlPgtDlGiLnFMN6Le7PlZgAGXbqwkTz71beRbWh0Pf9IsPjntv5Bt71Gf4_0024khpW1rk6U = nlaag_4hn3_0024dv1Pp4pGKgyg(string_0);
		if (iS5FmkRKimQylN9660UHlPgtDlGiLnFMN6Le7PlZgAGXbqwkTz71beRbWh0Pf9IsPjntv5Bt71Gf4_0024khpW1rk6U.HasValue)
		{
			long long_ = smethod_7(smethod_6(mD0obrPy9SdttIZRHX_0024wCDM));
			smethod_11(smethod_6(mD0obrPy9SdttIZRHX_0024wCDM), iS5FmkRKimQylN9660UHlPgtDlGiLnFMN6Le7PlZgAGXbqwkTz71beRbWh0Pf9IsPjntv5Bt71Gf4_0024khpW1rk6U.Value.sC7TVlJaywRg_0024a_5dqujETk, SeekOrigin.Begin);
			byte[] array = smethod_12(mD0obrPy9SdttIZRHX_0024wCDM, iS5FmkRKimQylN9660UHlPgtDlGiLnFMN6Le7PlZgAGXbqwkTz71beRbWh0Pf9IsPjntv5Bt71Gf4_0024khpW1rk6U.Value.aeK4XfFtH4Rk6VMllyUXIeQ);
			if (bool_0)
			{
				array = Y0682_EmDASRYkXiQKRSXHE.T6FSlkYzsIbsmI_74jwTbog(array);
			}
			smethod_11(smethod_6(mD0obrPy9SdttIZRHX_0024wCDM), long_, SeekOrigin.Begin);
			return array;
		}
		throw smethod_1(smethod_10(global::_003CModule_003E.smethod_26<string>(943815670u), string_0, global::_003CModule_003E.smethod_26<string>(1543687012u)));
	}

	internal void XHVHfNM87NCCuxeQm7Ot4ww()
	{
		smethod_13(mD0obrPy9SdttIZRHX_0024wCDM);
	}

	internal static bool smethod_0(string string_0)
	{
		return File.Exists(string_0);
	}

	internal static FileNotFoundException smethod_1(string string_0)
	{
		return new FileNotFoundException(string_0);
	}

	internal static FileStream smethod_2(string string_0, FileMode fileMode_0)
	{
		return new FileStream(string_0, fileMode_0);
	}

	internal static BinaryReader smethod_3(Stream stream_0)
	{
		return new BinaryReader(stream_0);
	}

	internal static int smethod_4(BinaryReader binaryReader_0)
	{
		return binaryReader_0.ReadInt32();
	}

	internal static string smethod_5(BinaryReader binaryReader_0)
	{
		return binaryReader_0.ReadString();
	}

	internal static Stream smethod_6(BinaryReader binaryReader_0)
	{
		return binaryReader_0.BaseStream;
	}

	internal static long smethod_7(Stream stream_0)
	{
		return stream_0.Position;
	}

	internal static void smethod_8(Stream stream_0, long long_0)
	{
		stream_0.Position = long_0;
	}

	internal static bool smethod_9(string string_0, string string_1)
	{
		return string_0 == string_1;
	}

	internal static string smethod_10(string string_0, string string_1, string string_2)
	{
		return string_0 + string_1 + string_2;
	}

	internal static long smethod_11(Stream stream_0, long long_0, SeekOrigin seekOrigin_0)
	{
		return stream_0.Seek(long_0, seekOrigin_0);
	}

	internal static byte[] smethod_12(BinaryReader binaryReader_0, int int_0)
	{
		return binaryReader_0.ReadBytes(int_0);
	}

	internal static void smethod_13(BinaryReader binaryReader_0)
	{
		binaryReader_0.Close();
	}
}
