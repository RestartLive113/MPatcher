using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Converter.MeshFormat;

public class StlFormat
{
	public class Triangle
	{
		public readonly Vector3[] Vertices;

		public readonly Vector3 Norm;

		public readonly ushort AttributeByteCount;

		public Triangle(Vector3 vector3_0, Vector3[] vector3_1)
		{
			Vertices = vector3_1;
			Norm = vector3_0;
			AttributeByteCount = 0;
		}
	}

	[Serializable]
	[CompilerGenerated]
	private sealed class Class61
	{
		public static readonly Class61 _003C_003E9 = new Class61();

		public static Func<Mesh.Triangle, Triangle> _003C_003E9__3_0;

		internal Triangle bQWp6QwCebuuHJI3ygR4llM_0024Ec_zfXnItSj8jsqVDm8S(Mesh.Triangle triangle_0)
		{
			return new Triangle(triangle_0.Norm, triangle_0.Vertices);
		}
	}

	public readonly List<Triangle> Triangles;

	public StlFormat(List<Triangle> list_0)
	{
		Triangles = list_0;
	}

	public static StlFormat FromMesh(Mesh mesh)
	{
		return new StlFormat(mesh.Triangles.Select((Mesh.Triangle triangle_0) => new Triangle(triangle_0.Norm, triangle_0.Vertices)).ToList());
	}
}
