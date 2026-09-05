using UnityEngine;

internal class nnHuJswxvsz1pZEYGN_00241t_hRktuePQgaPa2hccgMZBGMarJabqUZUQ4omKAa5FU0eA
{
	internal Vector3 vector3_0;

	internal Quaternion cuQrE4_0024fAWbq6iJOcEMEB_I;

	internal Vector3 vector3_1;

	internal Bounds _0024W57KyA48Amwd1O4QGSgAGM;

	internal Vector3 I9Td_Z2HQmtjkbxZZDo38rg => new Vector3(Mathf.FloorToInt(vector3_1.x), Mathf.FloorToInt(vector3_1.y), Mathf.FloorToInt(vector3_1.z));

	internal nnHuJswxvsz1pZEYGN_00241t_hRktuePQgaPa2hccgMZBGMarJabqUZUQ4omKAa5FU0eA(Vector3 pos, Quaternion rot, Vector3 scale, Bounds b)
	{
		vector3_0 = pos;
		cuQrE4_0024fAWbq6iJOcEMEB_I = rot;
		vector3_1 = scale;
		_0024W57KyA48Amwd1O4QGSgAGM = new Bounds(b.center, b.size);
	}

	internal void YiFDCDp4BaBGy7SmGWZz5fI(Vector3 factor)
	{
		vector3_1.x *= factor.x;
		vector3_1.y *= factor.y;
		vector3_1.z *= factor.z;
	}

	internal void oQfOqZ1m_0024sxXiQxzdS_cQpI(Vector3 factor)
	{
		vector3_1.x /= factor.x;
		vector3_1.y /= factor.y;
		vector3_1.z /= factor.z;
	}

	internal void nkbNIqZJHBYXfxJlhsRemu8(Vector3 factor)
	{
		Vector3 size = _0024W57KyA48Amwd1O4QGSgAGM.size;
		size.x *= factor.x;
		size.y *= factor.y;
		size.z *= factor.z;
		_0024W57KyA48Amwd1O4QGSgAGM.size = size;
	}
}
