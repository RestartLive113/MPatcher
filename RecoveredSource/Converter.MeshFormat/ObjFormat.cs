using System.Collections.Generic;
using Converter.Utils;
using UnityEngine;

namespace Converter.MeshFormat;

public class ObjFormat
{
	public class Face
	{
		public readonly List<int> GeometricVertexReferences;

		public readonly List<int> TextureVertexReferences;

		public readonly List<int> NormalVertexReferences;

		public Face()
		{
			GeometricVertexReferences = new List<int>();
			TextureVertexReferences = new List<int>();
			NormalVertexReferences = new List<int>();
		}
	}

	public readonly List<Vector4> GeometricVertices;

	public readonly List<Vector3> TextureVertices;

	public readonly List<Vector3> VertexNormals;

	public readonly List<Face> Faces;

	public ObjFormat()
	{
		GeometricVertices = new List<Vector4>();
		TextureVertices = new List<Vector3>();
		VertexNormals = new List<Vector3>();
		Faces = new List<Face>();
	}

	public static Mesh ToMesh(ObjFormat source)
	{
		List<Mesh.Triangle> list = new List<Mesh.Triangle>();
		foreach (Face face in source.Faces)
		{
			if (face.GeometricVertexReferences.Count > 3)
			{
				foreach (Mesh.Triangle item in m0JctFpJ1kxsmYSMqnyryGbqV4lhsdAcs_c9_39wRkaC(source.GeometricVertices, face.GeometricVertexReferences))
				{
					list.Add(item);
				}
				continue;
			}
			Vector3 vector = source.GeometricVertices[face.GeometricVertexReferences[0] - 1].ToVector3();
			Vector3 vector2 = source.GeometricVertices[face.GeometricVertexReferences[1] - 1].ToVector3();
			Vector3 vector3 = source.GeometricVertices[face.GeometricVertexReferences[2] - 1].ToVector3();
			Vector3 vector3_ = ZbQ7_0024Y_0024LBLAGeX2JUUSGA2wyUvOU5z4XeAIGc97AEnhp(vector, vector2, vector3);
			list.Add(new Mesh.Triangle(new Vector3[3] { vector, vector2, vector3 }, vector3_));
		}
		return new Mesh(list);
	}

	internal static Vector3 ZbQ7_0024Y_0024LBLAGeX2JUUSGA2wyUvOU5z4XeAIGc97AEnhp(Vector3 vector3_0, Vector3 vector3_1, Vector3 vector3_2)
	{
		Vector3 lhs = vector3_1 - vector3_0;
		Vector3 rhs = vector3_2 - vector3_0;
		return Vector3.Normalize(Vector3.Cross(lhs, rhs));
	}

	internal static List<Mesh.Triangle> m0JctFpJ1kxsmYSMqnyryGbqV4lhsdAcs_c9_39wRkaC(List<Vector4> list_0, List<int> list_1)
	{
		int count = list_1.Count;
		Vector3[] array = new Vector3[count];
		for (int i = 0; i < count; i++)
		{
			Vector4 vector = list_0[list_1[i] - 1];
			array[i] = new Vector3(vector.x, vector.y, vector.z);
		}
		List<Mesh.Triangle> list = new List<Mesh.Triangle>();
		for (int j = 1; j < array.Length - 1; j++)
		{
			Vector3 vector3_ = ZbQ7_0024Y_0024LBLAGeX2JUUSGA2wyUvOU5z4XeAIGc97AEnhp(array[0], array[j], array[j + 1]);
			list.Add(new Mesh.Triangle(new Vector3[3]
			{
				array[0],
				array[j],
				array[j + 1]
			}, vector3_));
		}
		return list;
	}
}
