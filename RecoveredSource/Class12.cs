using UnityEngine;

internal static class Class12
{
	internal static GameObject smethod_0(float float_0)
	{
		GameObject gameObject = smethod_1(PrimitiveType.Sphere);
		Mesh mesh_ = smethod_2(gameObject.GetComponent<MeshFilter>());
		GameObject gameObject2 = smethod_3();
		smethod_4((Object)gameObject2, global::_003CModule_003E.smethod_25<string>(2691058569u));
		MeshFilter meshFilter_ = gameObject2.AddComponent<MeshFilter>();
		smethod_6(meshFilter_, smethod_5());
		Vector3[] array = smethod_7(mesh_);
		for (int i = 0; i < array.Length; i++)
		{
			array[i] *= float_0;
		}
		smethod_8(smethod_2(meshFilter_), array);
		int[] array2 = smethod_9(mesh_);
		for (int j = 0; j < array2.Length; j += 3)
		{
			int num = array2[j];
			array2[j] = array2[j + 2];
			array2[j + 2] = num;
		}
		smethod_10(smethod_2(meshFilter_), array2);
		Vector3[] array3 = smethod_11(mesh_);
		for (int k = 0; k < array3.Length; k++)
		{
			array3[k] = -array3[k];
		}
		smethod_12(smethod_2(meshFilter_), array3);
		smethod_14(smethod_2(meshFilter_), smethod_13(mesh_));
		smethod_16(smethod_2(meshFilter_), smethod_15(mesh_));
		smethod_17(smethod_2(meshFilter_));
		smethod_19((Renderer)gameObject2.AddComponent<MeshRenderer>(), smethod_18((Renderer)gameObject.GetComponent<MeshRenderer>()));
		smethod_20((Object)gameObject);
		return gameObject2;
	}

	internal static GameObject smethod_1(PrimitiveType primitiveType_0)
	{
		return GameObject.CreatePrimitive(primitiveType_0);
	}

	internal static Mesh smethod_2(MeshFilter meshFilter_0)
	{
		return meshFilter_0.sharedMesh;
	}

	internal static GameObject smethod_3()
	{
		return new GameObject();
	}

	internal static void smethod_4(Object object_0, string string_0)
	{
		object_0.name = string_0;
	}

	internal static Mesh smethod_5()
	{
		return new Mesh();
	}

	internal static void smethod_6(MeshFilter meshFilter_0, Mesh mesh_0)
	{
		meshFilter_0.sharedMesh = mesh_0;
	}

	internal static Vector3[] smethod_7(Mesh mesh_0)
	{
		return mesh_0.vertices;
	}

	internal static void smethod_8(Mesh mesh_0, Vector3[] vector3_0)
	{
		mesh_0.vertices = vector3_0;
	}

	internal static int[] smethod_9(Mesh mesh_0)
	{
		return mesh_0.triangles;
	}

	internal static void smethod_10(Mesh mesh_0, int[] int_0)
	{
		mesh_0.triangles = int_0;
	}

	internal static Vector3[] smethod_11(Mesh mesh_0)
	{
		return mesh_0.normals;
	}

	internal static void smethod_12(Mesh mesh_0, Vector3[] vector3_0)
	{
		mesh_0.normals = vector3_0;
	}

	internal static Vector2[] smethod_13(Mesh mesh_0)
	{
		return mesh_0.uv;
	}

	internal static void smethod_14(Mesh mesh_0, Vector2[] vector2_0)
	{
		mesh_0.uv = vector2_0;
	}

	internal static Vector2[] smethod_15(Mesh mesh_0)
	{
		return mesh_0.uv2;
	}

	internal static void smethod_16(Mesh mesh_0, Vector2[] vector2_0)
	{
		mesh_0.uv2 = vector2_0;
	}

	internal static void smethod_17(Mesh mesh_0)
	{
		mesh_0.RecalculateBounds();
	}

	internal static Material smethod_18(Renderer renderer_0)
	{
		return renderer_0.sharedMaterial;
	}

	internal static void smethod_19(Renderer renderer_0, Material material_0)
	{
		renderer_0.sharedMaterial = material_0;
	}

	internal static void smethod_20(Object object_0)
	{
		Object.Destroy(object_0);
	}
}
