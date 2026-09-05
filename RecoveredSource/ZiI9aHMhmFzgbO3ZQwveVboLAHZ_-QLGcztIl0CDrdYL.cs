using UnityEngine;

internal static class ZiI9aHMhmFzgbO3ZQwveVboLAHZ__0024QLGcztIl0CDrdYL
{
	public static Vector3 smethod_0(this Matrix4x4 matrix4x4_0)
	{
		Vector4 column = matrix4x4_0.GetColumn(3);
		return new Vector3(column.x, column.y, column.z);
	}

	public static Quaternion dGAhN7gBx_0024oQjK_0024NvHvnUTY(this Matrix4x4 matrix4x4_0)
	{
		Quaternion result = new Quaternion
		{
			w = Mathf.Sqrt(Mathf.Max(0f, 1f + matrix4x4_0[0, 0] + matrix4x4_0[1, 1] + matrix4x4_0[2, 2])) / 2f,
			x = Mathf.Sqrt(Mathf.Max(0f, 1f + matrix4x4_0[0, 0] - matrix4x4_0[1, 1] - matrix4x4_0[2, 2])) / 2f,
			y = Mathf.Sqrt(Mathf.Max(0f, 1f - matrix4x4_0[0, 0] + matrix4x4_0[1, 1] - matrix4x4_0[2, 2])) / 2f,
			z = Mathf.Sqrt(Mathf.Max(0f, 1f - matrix4x4_0[0, 0] - matrix4x4_0[1, 1] + matrix4x4_0[2, 2])) / 2f
		};
		result.x *= Mathf.Sign(result.x * (matrix4x4_0[2, 1] - matrix4x4_0[1, 2]));
		result.y *= Mathf.Sign(result.y * (matrix4x4_0[0, 2] - matrix4x4_0[2, 0]));
		result.z *= Mathf.Sign(result.z * (matrix4x4_0[1, 0] - matrix4x4_0[0, 1]));
		return result;
	}

	public static Vector3 DAjFWpkF9_FBtc7d836mcrQ(this Matrix4x4 matrix4x4_0)
	{
		return new Vector3(matrix4x4_0.GetColumn(0).magnitude, matrix4x4_0.GetColumn(1).magnitude, matrix4x4_0.GetColumn(2).magnitude);
	}
}
