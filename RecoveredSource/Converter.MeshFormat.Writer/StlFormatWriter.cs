using System;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Converter.MeshFormat.Writer;

public class StlFormatWriter : IMeshFormatWriter
{
	[CompilerGenerated]
	private sealed class Class62
	{
		public StlFormatWriter SKCFxHGAEbVQbKCDB_0024Jj8p4;

		public BinaryWriter z55DexaUeoj4h_0024yDKRkVapk;

		internal void qlqje2FKd5RdFFijwHxyoO0BcMYyyoEBPqOZOtD4gkf4(StlFormat.Triangle triangle_0)
		{
			SKCFxHGAEbVQbKCDB_0024Jj8p4.method_0(triangle_0, z55DexaUeoj4h_0024yDKRkVapk);
		}
	}

	[CompilerGenerated]
	private sealed class Class63
	{
		public StlFormatWriter SKCFxHGAEbVQbKCDB_0024Jj8p4;

		public BinaryWriter z55DexaUeoj4h_0024yDKRkVapk;

		internal void vCBSefDWXVCi3CrX4w9jPMQ_0a07mSRfrqFPGyKlt10W(StlFormat.Triangle triangle_0)
		{
			SKCFxHGAEbVQbKCDB_0024Jj8p4.method_0(triangle_0, z55DexaUeoj4h_0024yDKRkVapk);
		}
	}

	private const int Aulpm49FommLo4D1H7SmZ6o = 80;

	public string Tag => global::_003CModule_003E.smethod_27<string>(451419286u);

	internal void method_0(StlFormat.Triangle triangle_0, BinaryWriter binaryWriter_0)
	{
		OSEV9i8jo9Rbj5NGhsxjnnU(triangle_0.Norm, binaryWriter_0);
		for (int i = 0; i < 3; i++)
		{
			OSEV9i8jo9Rbj5NGhsxjnnU(triangle_0.Vertices[i], binaryWriter_0);
		}
		smethod_0(binaryWriter_0, triangle_0.AttributeByteCount);
	}

	internal void OSEV9i8jo9Rbj5NGhsxjnnU(Vector3 vector3_0, BinaryWriter binaryWriter_0)
	{
		smethod_1(binaryWriter_0, vector3_0.x);
		smethod_1(binaryWriter_0, vector3_0.y);
		smethod_1(binaryWriter_0, vector3_0.z);
	}

	public void WriteToStream(Mesh mesh, Stream outputStream)
	{
		StlFormat stlFormat = StlFormat.FromMesh(mesh);
		BinaryWriter binaryWriter = smethod_2(outputStream);
		try
		{
			byte[] byte_ = new byte[80];
			smethod_3(binaryWriter, byte_);
			smethod_4(binaryWriter, (uint)stlFormat.Triangles.Count);
			stlFormat.Triangles.ForEach(delegate(StlFormat.Triangle triangle_0)
			{
				method_0(triangle_0, binaryWriter);
			});
		}
		finally
		{
			if (binaryWriter != null)
			{
				smethod_5((IDisposable)binaryWriter);
			}
		}
	}

	public void WriteToStreamNodispose(Mesh mesh, Stream outputStream)
	{
		StlFormat stlFormat = StlFormat.FromMesh(mesh);
		BinaryWriter binaryWriter_ = smethod_2(outputStream);
		byte[] byte_ = new byte[80];
		smethod_3(binaryWriter_, byte_);
		smethod_4(binaryWriter_, (uint)stlFormat.Triangles.Count);
		stlFormat.Triangles.ForEach(delegate(StlFormat.Triangle triangle_0)
		{
			method_0(triangle_0, binaryWriter_);
		});
	}

	internal static void smethod_0(BinaryWriter binaryWriter_0, ushort ushort_0)
	{
		binaryWriter_0.Write(ushort_0);
	}

	internal static void smethod_1(BinaryWriter binaryWriter_0, float float_0)
	{
		binaryWriter_0.Write(float_0);
	}

	internal static BinaryWriter smethod_2(Stream stream_0)
	{
		return new BinaryWriter(stream_0);
	}

	internal static void smethod_3(BinaryWriter binaryWriter_0, byte[] byte_0)
	{
		binaryWriter_0.Write(byte_0);
	}

	internal static void smethod_4(BinaryWriter binaryWriter_0, uint uint_0)
	{
		binaryWriter_0.Write(uint_0);
	}

	internal static void smethod_5(IDisposable idisposable_0)
	{
		idisposable_0.Dispose();
	}
}
