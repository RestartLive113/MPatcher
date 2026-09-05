using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

internal class VBGvGFOsmw5aR9Pc8Td1LSYJPgq8Le3DRGORTCrjoJvo8tol2e0JowjQUMlAiUUeoQ
{
	[CompilerGenerated]
	private string RS3ZdolaqeLdFiTSGCqH6xh97JQ64r912VkhQCYNQfZW;

	[CompilerGenerated]
	private Dictionary<string, int> njXUVT6eXJj0BjhJvFex6eoJDNHJvMczWDy8CRpOShqu;

	[CompilerGenerated]
	private int[] QZ1T7_gepXRK1EfpsU0eFm2Q8vVJWgv2tcAFjkBlk6Nw;

	public string iQXQlC5p5DB4hkwaqZnLmP4
	{
		[CompilerGenerated]
		get
		{
			return RS3ZdolaqeLdFiTSGCqH6xh97JQ64r912VkhQCYNQfZW;
		}
		[CompilerGenerated]
		set
		{
			RS3ZdolaqeLdFiTSGCqH6xh97JQ64r912VkhQCYNQfZW = value;
		}
	}

	public Dictionary<string, int> ymwbvtpGl4UoHAavMEc2HZ4
	{
		[CompilerGenerated]
		get
		{
			return njXUVT6eXJj0BjhJvFex6eoJDNHJvMczWDy8CRpOShqu;
		}
		[CompilerGenerated]
		set
		{
			njXUVT6eXJj0BjhJvFex6eoJDNHJvMczWDy8CRpOShqu = value;
		}
	}

	public int[] TQ4hiwix8LFuA8cl7gL3MDU
	{
		[CompilerGenerated]
		get
		{
			return QZ1T7_gepXRK1EfpsU0eFm2Q8vVJWgv2tcAFjkBlk6Nw;
		}
		[CompilerGenerated]
		set
		{
			QZ1T7_gepXRK1EfpsU0eFm2Q8vVJWgv2tcAFjkBlk6Nw = value;
		}
	}

	public void cqWoMNveroNrLO3XzL3B_0024XA(Stream stream_0)
	{
		BinaryReader binaryReader = smethod_0(stream_0);
		try
		{
			iQXQlC5p5DB4hkwaqZnLmP4 = smethod_1(binaryReader);
			int num = smethod_2(binaryReader);
			ymwbvtpGl4UoHAavMEc2HZ4 = new Dictionary<string, int>(num);
			for (int i = 0; i < num; i++)
			{
				ymwbvtpGl4UoHAavMEc2HZ4.Add(smethod_1(binaryReader), smethod_2(binaryReader));
			}
			int num2 = smethod_2(binaryReader);
			TQ4hiwix8LFuA8cl7gL3MDU = new int[num2];
			for (int j = 0; j < num2; j++)
			{
				TQ4hiwix8LFuA8cl7gL3MDU[j] = smethod_2(binaryReader);
			}
		}
		finally
		{
			if (binaryReader != null)
			{
				smethod_3((IDisposable)binaryReader);
			}
		}
	}

	public void method_0(Stream stream_0)
	{
		BinaryWriter binaryWriter = smethod_4(stream_0);
		try
		{
			smethod_5(binaryWriter, iQXQlC5p5DB4hkwaqZnLmP4);
			smethod_6(binaryWriter, ymwbvtpGl4UoHAavMEc2HZ4.Count);
			foreach (string key in ymwbvtpGl4UoHAavMEc2HZ4.Keys)
			{
				smethod_5(binaryWriter, key);
				smethod_6(binaryWriter, ymwbvtpGl4UoHAavMEc2HZ4[key]);
			}
			smethod_6(binaryWriter, TQ4hiwix8LFuA8cl7gL3MDU.Length);
			int[] tQ4hiwix8LFuA8cl7gL3MDU = TQ4hiwix8LFuA8cl7gL3MDU;
			foreach (int int_ in tQ4hiwix8LFuA8cl7gL3MDU)
			{
				smethod_6(binaryWriter, int_);
			}
		}
		finally
		{
			if (binaryWriter != null)
			{
				smethod_3((IDisposable)binaryWriter);
			}
		}
	}

	internal static BinaryReader smethod_0(Stream stream_0)
	{
		return new BinaryReader(stream_0);
	}

	internal static string smethod_1(BinaryReader binaryReader_0)
	{
		return binaryReader_0.ReadString();
	}

	internal static int smethod_2(BinaryReader binaryReader_0)
	{
		return binaryReader_0.ReadInt32();
	}

	internal static void smethod_3(IDisposable idisposable_0)
	{
		idisposable_0.Dispose();
	}

	internal static BinaryWriter smethod_4(Stream stream_0)
	{
		return new BinaryWriter(stream_0);
	}

	internal static void smethod_5(BinaryWriter binaryWriter_0, string string_0)
	{
		binaryWriter_0.Write(string_0);
	}

	internal static void smethod_6(BinaryWriter binaryWriter_0, int int_0)
	{
		binaryWriter_0.Write(int_0);
	}
}
