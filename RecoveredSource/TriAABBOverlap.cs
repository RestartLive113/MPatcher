using UnityEngine;

public static class TriAABBOverlap
{
	public struct Triangle
	{
		public Vector3 vertA;

		public Vector3 vertB;

		public Vector3 vertC;

		public Vector3 normal;

		public Bounds bound;

		public bool didHit;
	}

	public struct BoundHierarchy
	{
		public Bounds bound;

		public BoundHierarchy[] subBounds;

		public Triangle triList;
	}

	private static Vector3 vector3_0 = Vector3.zero;

	private static Vector3 vector3_1 = Vector3.zero;

	private static Vector3 nCLzJnYaGJQNeQeg8U91V1U = Vector3.zero;

	private static Vector3 nXUtn5dZcC5RHDAo9wxYPYA = Vector3.zero;

	private static Vector3 nohwYpLhrnoMqbMT_R7dt2Y = Vector3.zero;

	private static Vector3 kDDlip6C_4eyLL9kX_F4RH4 = Vector3.zero;

	private static Vector3 kSu4pdRVEd7wfOI44F70fHs = Vector3.zero;

	private static Vector3 kiOd2faeW1xYBkB9xhOwS_0024U = Vector3.zero;

	private static bool fEjJUQnQ8BBvSYfpFrcoQW4(Vector3 vector3_2, Vector3 vector3_3, Vector3 vector3_4)
	{
		float num = 0f;
		vector3_0.z = 0f;
		float y = num;
		num = 0f;
		vector3_0.y = y;
		vector3_0.x = num;
		num = 0f;
		vector3_1.z = 0f;
		float y2 = num;
		num = 0f;
		vector3_1.y = y2;
		vector3_1.x = num;
		float x = vector3_3.x;
		float y3 = vector3_3.y;
		float z = vector3_3.z;
		float x2 = vector3_4.x;
		float y4 = vector3_4.y;
		float z2 = vector3_4.z;
		if (vector3_2.x > 0f)
		{
			vector3_0.x = 0f - x2 - x;
			vector3_1.x = x2 - x;
		}
		else
		{
			vector3_0.x = x2 - x;
			vector3_1.x = 0f - x2 - x;
		}
		if (vector3_2.y <= 0f)
		{
			vector3_0.y = y4 - y3;
			vector3_1.y = 0f - y4 - y3;
		}
		else
		{
			vector3_0.y = 0f - y4 - y3;
			vector3_1.y = y4 - y3;
		}
		if (vector3_2.z > 0f)
		{
			vector3_0.z = 0f - z2 - z;
			vector3_1.z = z2 - z;
		}
		else
		{
			vector3_0.z = z2 - z;
			vector3_1.z = 0f - z2 - z;
		}
		if (Vector3.Dot(vector3_2, vector3_0) <= 0f)
		{
			if (Vector3.Dot(vector3_2, vector3_1) >= 0f)
			{
				return true;
			}
			return false;
		}
		return false;
	}

	internal static void smethod_0(float float_0, float float_1, float float_2, out float float_3, out float float_4)
	{
		float_3 = ((!(float_0 < float_1)) ? ((float_1 < float_2) ? float_1 : float_2) : ((float_0 < float_2) ? float_0 : float_2));
		float_4 = ((!(float_0 > float_1)) ? ((float_1 > float_2) ? float_1 : float_2) : ((float_0 > float_2) ? float_0 : float_2));
	}

	public static bool Project(float ea1, float ea2, float v1a1, float v1a2, float v2a1, float v2a2, float rad)
	{
		float num = ea1 * v1a2 - ea2 * v1a1;
		float num2 = ea1 * v2a2 - ea2 * v2a1;
		float num3 = ((num < num2) ? num : num2);
		float num4 = ((num > num2) ? num : num2);
		if (num3 > rad || num4 < 0f - rad)
		{
			return true;
		}
		return false;
	}

	public static bool Project(Vector3 vert1, Vector3 vert2, Vector3 edge, int axis1, int axis2, float rad)
	{
		float num = edge[axis1] * vert1[axis2] - edge[axis2] * vert1[axis1];
		float num2 = edge[axis1] * vert2[axis2] - edge[axis2] * vert2[axis1];
		float num3 = ((num < num2) ? num : num2);
		float num4 = ((num > num2) ? num : num2);
		if (!(num3 > rad) && num4 >= 0f - rad)
		{
			return false;
		}
		return true;
	}

	public static float Abs(float val)
	{
		if (!(val < 0f))
		{
			return val;
		}
		return 0f - val;
	}

	public static bool Check(Bounds bounds, Triangle triangle)
	{
		Vector3 center = bounds.center;
		Vector3 extents = bounds.extents;
		nCLzJnYaGJQNeQeg8U91V1U.x = triangle.vertA.x - center.x;
		nCLzJnYaGJQNeQeg8U91V1U.y = triangle.vertA.y - center.y;
		nCLzJnYaGJQNeQeg8U91V1U.z = triangle.vertA.z - center.z;
		nXUtn5dZcC5RHDAo9wxYPYA.x = triangle.vertB.x - center.x;
		nXUtn5dZcC5RHDAo9wxYPYA.y = triangle.vertB.y - center.y;
		nXUtn5dZcC5RHDAo9wxYPYA.z = triangle.vertB.z - center.z;
		nohwYpLhrnoMqbMT_R7dt2Y.x = triangle.vertC.x - center.x;
		nohwYpLhrnoMqbMT_R7dt2Y.y = triangle.vertC.y - center.y;
		nohwYpLhrnoMqbMT_R7dt2Y.z = triangle.vertC.z - center.z;
		smethod_0(nCLzJnYaGJQNeQeg8U91V1U.x, nXUtn5dZcC5RHDAo9wxYPYA.x, nohwYpLhrnoMqbMT_R7dt2Y.x, out var float_, out var float_2);
		if (float_ > extents.x || float_2 < 0f - extents.x)
		{
			return false;
		}
		smethod_0(nCLzJnYaGJQNeQeg8U91V1U.y, nXUtn5dZcC5RHDAo9wxYPYA.y, nohwYpLhrnoMqbMT_R7dt2Y.y, out float_, out float_2);
		if (!(float_ > extents.y) && float_2 >= 0f - extents.y)
		{
			smethod_0(nCLzJnYaGJQNeQeg8U91V1U.z, nXUtn5dZcC5RHDAo9wxYPYA.z, nohwYpLhrnoMqbMT_R7dt2Y.z, out float_, out float_2);
			if (!(float_ > extents.z) && float_2 >= 0f - extents.z)
			{
				kDDlip6C_4eyLL9kX_F4RH4.x = nXUtn5dZcC5RHDAo9wxYPYA.x - nCLzJnYaGJQNeQeg8U91V1U.x;
				kDDlip6C_4eyLL9kX_F4RH4.y = nXUtn5dZcC5RHDAo9wxYPYA.y - nCLzJnYaGJQNeQeg8U91V1U.y;
				kDDlip6C_4eyLL9kX_F4RH4.z = nXUtn5dZcC5RHDAo9wxYPYA.z - nCLzJnYaGJQNeQeg8U91V1U.z;
				kSu4pdRVEd7wfOI44F70fHs.x = nohwYpLhrnoMqbMT_R7dt2Y.x - nXUtn5dZcC5RHDAo9wxYPYA.x;
				kSu4pdRVEd7wfOI44F70fHs.y = nohwYpLhrnoMqbMT_R7dt2Y.y - nXUtn5dZcC5RHDAo9wxYPYA.y;
				kSu4pdRVEd7wfOI44F70fHs.z = nohwYpLhrnoMqbMT_R7dt2Y.z - nXUtn5dZcC5RHDAo9wxYPYA.z;
				kiOd2faeW1xYBkB9xhOwS_0024U.x = nCLzJnYaGJQNeQeg8U91V1U.x - nohwYpLhrnoMqbMT_R7dt2Y.x;
				kiOd2faeW1xYBkB9xhOwS_0024U.y = nCLzJnYaGJQNeQeg8U91V1U.y - nohwYpLhrnoMqbMT_R7dt2Y.y;
				kiOd2faeW1xYBkB9xhOwS_0024U.z = nCLzJnYaGJQNeQeg8U91V1U.z - nohwYpLhrnoMqbMT_R7dt2Y.z;
				float num = Abs(kDDlip6C_4eyLL9kX_F4RH4.x);
				float num2 = Abs(kDDlip6C_4eyLL9kX_F4RH4.y);
				float num3 = Abs(kDDlip6C_4eyLL9kX_F4RH4.z);
				if (!Project(kDDlip6C_4eyLL9kX_F4RH4.z, kDDlip6C_4eyLL9kX_F4RH4.y, nCLzJnYaGJQNeQeg8U91V1U.z, nCLzJnYaGJQNeQeg8U91V1U.y, nohwYpLhrnoMqbMT_R7dt2Y.z, nohwYpLhrnoMqbMT_R7dt2Y.y, num3 * extents.y + num2 * extents.z))
				{
					if (Project(0f - kDDlip6C_4eyLL9kX_F4RH4.z, 0f - kDDlip6C_4eyLL9kX_F4RH4.x, nCLzJnYaGJQNeQeg8U91V1U.z, nCLzJnYaGJQNeQeg8U91V1U.x, nohwYpLhrnoMqbMT_R7dt2Y.z, nohwYpLhrnoMqbMT_R7dt2Y.x, num3 * extents.x + num * extents.z))
					{
						return false;
					}
					if (Project(kDDlip6C_4eyLL9kX_F4RH4.y, kDDlip6C_4eyLL9kX_F4RH4.x, nXUtn5dZcC5RHDAo9wxYPYA.y, nXUtn5dZcC5RHDAo9wxYPYA.x, nohwYpLhrnoMqbMT_R7dt2Y.y, nohwYpLhrnoMqbMT_R7dt2Y.x, num2 * extents.x + num * extents.y))
					{
						return false;
					}
					num = Abs(kSu4pdRVEd7wfOI44F70fHs.x);
					num2 = Abs(kSu4pdRVEd7wfOI44F70fHs.y);
					num3 = Abs(kSu4pdRVEd7wfOI44F70fHs.z);
					if (Project(kSu4pdRVEd7wfOI44F70fHs.z, kSu4pdRVEd7wfOI44F70fHs.y, nCLzJnYaGJQNeQeg8U91V1U.z, nCLzJnYaGJQNeQeg8U91V1U.y, nohwYpLhrnoMqbMT_R7dt2Y.z, nohwYpLhrnoMqbMT_R7dt2Y.y, num3 * extents.y + num2 * extents.z))
					{
						return false;
					}
					if (Project(0f - kSu4pdRVEd7wfOI44F70fHs.z, 0f - kSu4pdRVEd7wfOI44F70fHs.x, nCLzJnYaGJQNeQeg8U91V1U.z, nCLzJnYaGJQNeQeg8U91V1U.x, nohwYpLhrnoMqbMT_R7dt2Y.z, nohwYpLhrnoMqbMT_R7dt2Y.x, num3 * extents.x + num * extents.z))
					{
						return false;
					}
					if (Project(kSu4pdRVEd7wfOI44F70fHs.y, kSu4pdRVEd7wfOI44F70fHs.x, nCLzJnYaGJQNeQeg8U91V1U.y, nCLzJnYaGJQNeQeg8U91V1U.x, nXUtn5dZcC5RHDAo9wxYPYA.y, nXUtn5dZcC5RHDAo9wxYPYA.x, num2 * extents.x + num * extents.y))
					{
						return false;
					}
					num = Abs(kiOd2faeW1xYBkB9xhOwS_0024U.x);
					num2 = Abs(kiOd2faeW1xYBkB9xhOwS_0024U.y);
					num3 = Abs(kiOd2faeW1xYBkB9xhOwS_0024U.z);
					if (!Project(kiOd2faeW1xYBkB9xhOwS_0024U.z, kiOd2faeW1xYBkB9xhOwS_0024U.y, nCLzJnYaGJQNeQeg8U91V1U.z, nCLzJnYaGJQNeQeg8U91V1U.y, nXUtn5dZcC5RHDAo9wxYPYA.z, nXUtn5dZcC5RHDAo9wxYPYA.y, num3 * extents.y + num2 * extents.z))
					{
						if (!Project(0f - kiOd2faeW1xYBkB9xhOwS_0024U.z, 0f - kiOd2faeW1xYBkB9xhOwS_0024U.x, nCLzJnYaGJQNeQeg8U91V1U.z, nCLzJnYaGJQNeQeg8U91V1U.x, nXUtn5dZcC5RHDAo9wxYPYA.z, nXUtn5dZcC5RHDAo9wxYPYA.x, num3 * extents.x + num * extents.z))
						{
							if (Project(kiOd2faeW1xYBkB9xhOwS_0024U.y, kiOd2faeW1xYBkB9xhOwS_0024U.x, nXUtn5dZcC5RHDAo9wxYPYA.y, nXUtn5dZcC5RHDAo9wxYPYA.x, nohwYpLhrnoMqbMT_R7dt2Y.y, nohwYpLhrnoMqbMT_R7dt2Y.x, num2 * extents.x + num * extents.y))
							{
								return false;
							}
							if (fEjJUQnQ8BBvSYfpFrcoQW4(triangle.normal, nCLzJnYaGJQNeQeg8U91V1U, extents))
							{
								return true;
							}
							return false;
						}
						return false;
					}
					return false;
				}
				return false;
			}
			return false;
		}
		return false;
	}
}
