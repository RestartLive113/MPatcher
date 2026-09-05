using System.Collections.Generic;
using UnityEngine;

namespace Converter.MeshFormat;

public class Mesh
{
	public class Triangle
	{
		public readonly Vector3[] Vertices;

		public readonly Vector3 Norm;

		public Triangle(Vector3[] vector3_0, Vector3 vector3_1)
		{
			Vertices = vector3_0;
			Norm = vector3_1;
		}
	}

	public readonly List<Triangle> Triangles;

	public Mesh(List<Triangle> list_0)
	{
		Triangles = list_0;
	}

	public float CalculateArea()
	{
		float num = 0f;
		foreach (Triangle triangle in Triangles)
		{
			Vector3 lhs = triangle.Vertices[1] - triangle.Vertices[0];
			Vector3 rhs = triangle.Vertices[2] - triangle.Vertices[0];
			num += Vector3.Cross(lhs, rhs).magnitude;
		}
		return num;
	}

	public float CalculateVolume()
	{
		float num = 0f;
		foreach (Triangle triangle in Triangles)
		{
			float num2 = Vector3.Dot(triangle.Vertices[0], Vector3.Cross(triangle.Vertices[1], triangle.Vertices[3])) * 1f / 6f;
			num += num2;
		}
		return num;
	}
}
