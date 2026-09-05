using System;
using System.IO;
using System.Text;
using UnityEngine;

public class ObjExporter
{
	public static string MeshToString(MeshFilter mf)
	{
		Mesh mesh_ = smethod_0(mf);
		Material[] array = smethod_1((Renderer)mf.GetComponent<MeshRenderer>());
		StringBuilder stringBuilder = smethod_2();
		smethod_3(smethod_3(smethod_3(stringBuilder, global::_003CModule_003E.smethod_27<string>(2858390955u)), smethod_4((UnityEngine.Object)mf)), global::_003CModule_003E.smethod_26<string>(3443209972u));
		Vector3[] array2 = smethod_5(mesh_);
		for (int i = 0; i < array2.Length; i++)
		{
			Vector3 vector = array2[i];
			smethod_3(stringBuilder, smethod_6(global::_003CModule_003E.smethod_25<string>(3938455753u), (object)vector.x, (object)vector.y, (object)vector.z));
		}
		smethod_3(stringBuilder, global::_003CModule_003E.smethod_26<string>(3443209972u));
		array2 = smethod_7(mesh_);
		for (int i = 0; i < array2.Length; i++)
		{
			Vector3 vector2 = array2[i];
			smethod_3(stringBuilder, smethod_6(global::_003CModule_003E.smethod_28<string>(3776587335u), (object)vector2.x, (object)vector2.y, (object)vector2.z));
		}
		smethod_3(stringBuilder, global::_003CModule_003E.smethod_29<string>(2566982012u));
		Vector2[] array3 = smethod_8(mesh_);
		for (int i = 0; i < array3.Length; i++)
		{
			Vector3 vector3 = array3[i];
			smethod_3(stringBuilder, smethod_9(global::_003CModule_003E.smethod_26<string>(68881456u), (object)vector3.x, (object)vector3.y));
		}
		for (int j = 0; j < smethod_11(mesh_); j++)
		{
			smethod_3(stringBuilder, global::_003CModule_003E.smethod_29<string>(2566982012u));
			smethod_3(smethod_3(smethod_3(stringBuilder, global::_003CModule_003E.smethod_26<string>(1714382180u)), smethod_4((UnityEngine.Object)array[j])), global::_003CModule_003E.smethod_25<string>(1308721947u));
			smethod_3(smethod_3(smethod_3(stringBuilder, global::_003CModule_003E.smethod_26<string>(550032613u)), smethod_4((UnityEngine.Object)array[j])), global::_003CModule_003E.smethod_25<string>(1308721947u));
			int[] array4 = smethod_10(mesh_, j);
			for (int k = 0; k < array4.Length; k += 3)
			{
				smethod_3(stringBuilder, smethod_6(global::_003CModule_003E.smethod_28<string>(1029638356u), (object)(array4[k] + 1), (object)(array4[k + 1] + 1), (object)(array4[k + 2] + 1)));
			}
		}
		return smethod_12((object)stringBuilder);
	}

	public static void MeshToFile(MeshFilter mf, string filename)
	{
		StreamWriter streamWriter = smethod_13(filename);
		try
		{
			smethod_14((TextWriter)streamWriter, MeshToString(mf));
		}
		finally
		{
			if (streamWriter != null)
			{
				smethod_15((IDisposable)streamWriter);
			}
		}
	}

	internal static Mesh smethod_0(MeshFilter meshFilter_0)
	{
		return meshFilter_0.mesh;
	}

	internal static Material[] smethod_1(Renderer renderer_0)
	{
		return renderer_0.sharedMaterials;
	}

	internal static StringBuilder smethod_2()
	{
		return new StringBuilder();
	}

	internal static StringBuilder smethod_3(StringBuilder stringBuilder_0, string string_0)
	{
		return stringBuilder_0.Append(string_0);
	}

	internal static string smethod_4(UnityEngine.Object object_0)
	{
		return object_0.name;
	}

	internal static Vector3[] smethod_5(Mesh mesh_0)
	{
		return mesh_0.vertices;
	}

	internal static string smethod_6(string string_0, object object_0, object object_1, object object_2)
	{
		return string.Format(string_0, object_0, object_1, object_2);
	}

	internal static Vector3[] smethod_7(Mesh mesh_0)
	{
		return mesh_0.normals;
	}

	internal static Vector2[] smethod_8(Mesh mesh_0)
	{
		return mesh_0.uv;
	}

	internal static string smethod_9(string string_0, object object_0, object object_1)
	{
		return string.Format(string_0, object_0, object_1);
	}

	internal static int[] smethod_10(Mesh mesh_0, int int_0)
	{
		return mesh_0.GetTriangles(int_0);
	}

	internal static int smethod_11(Mesh mesh_0)
	{
		return mesh_0.subMeshCount;
	}

	internal static string smethod_12(object object_0)
	{
		return object_0.ToString();
	}

	internal static StreamWriter smethod_13(string string_0)
	{
		return new StreamWriter(string_0);
	}

	internal static void smethod_14(TextWriter textWriter_0, string string_0)
	{
		textWriter_0.Write(string_0);
	}

	internal static void smethod_15(IDisposable idisposable_0)
	{
		idisposable_0.Dispose();
	}
}
