using UnityEngine;

internal class XBS7OSIVgwiqp2YsikrDtq6zcM87cw3nXOw4Pre7ULHrFpjhoDlZmc_0024_dixBV9c1sQ : pQCv4GWEmO7suQD936qIBn22jIu63BZDtto8pweiUAZY
{
	private GameObject[] mGsD_aMic82mpSwXGYyn184;

	private Vector3[] HenHGDciQEdG6n_0024b0WalHO4;

	private Vector3[] fCYD9ue5qIlZ5X9jc_0024XUMGc;

	private Vector3[] uxOREKEnqpcRfWzMjlLlQrI;

	private Vector3[] EtvDS7aynZVw6wILgHAYu4U;

	private Quaternion[] MN8YoTCYhWVgHU7kzqzk_2s;

	private Quaternion[] j7lg_00246sTurbdwzePLswLFEo;

	internal XBS7OSIVgwiqp2YsikrDtq6zcM87cw3nXOw4Pre7ULHrFpjhoDlZmc_0024_dixBV9c1sQ(GameObject[] obj, Vector3[] prevPos, Quaternion[] prevRot, Vector3[] newPos, Quaternion[] newRot, Vector3[] prevScale, Vector3[] newScale)
	{
		mGsD_aMic82mpSwXGYyn184 = obj;
		HenHGDciQEdG6n_0024b0WalHO4 = prevPos;
		MN8YoTCYhWVgHU7kzqzk_2s = prevRot;
		fCYD9ue5qIlZ5X9jc_0024XUMGc = newPos;
		j7lg_00246sTurbdwzePLswLFEo = newRot;
		uxOREKEnqpcRfWzMjlLlQrI = prevScale;
		EtvDS7aynZVw6wILgHAYu4U = newScale;
	}

	public void bv4xsECkipN_002441Wa7mcfeqY()
	{
		for (int i = 0; i < mGsD_aMic82mpSwXGYyn184.Length; i++)
		{
			smethod_1(smethod_0(mGsD_aMic82mpSwXGYyn184[i]), fCYD9ue5qIlZ5X9jc_0024XUMGc[i]);
			smethod_2(smethod_0(mGsD_aMic82mpSwXGYyn184[i]), j7lg_00246sTurbdwzePLswLFEo[i]);
			if (smethod_3((Object)mGsD_aMic82mpSwXGYyn184[i].GetComponent<PointController>(), (Object)null))
			{
				smethod_4(smethod_0(mGsD_aMic82mpSwXGYyn184[i]), EtvDS7aynZVw6wILgHAYu4U[i]);
			}
		}
	}

	public void M_UYuO7m_5zc0NRNG1OJLAE()
	{
		for (int i = 0; i < mGsD_aMic82mpSwXGYyn184.Length; i++)
		{
			smethod_1(smethod_0(mGsD_aMic82mpSwXGYyn184[i]), HenHGDciQEdG6n_0024b0WalHO4[i]);
			smethod_2(smethod_0(mGsD_aMic82mpSwXGYyn184[i]), MN8YoTCYhWVgHU7kzqzk_2s[i]);
			if (smethod_3((Object)mGsD_aMic82mpSwXGYyn184[i].GetComponent<PointController>(), (Object)null))
			{
				smethod_4(smethod_0(mGsD_aMic82mpSwXGYyn184[i]), uxOREKEnqpcRfWzMjlLlQrI[i]);
			}
		}
	}

	internal static Transform smethod_0(GameObject gameObject_0)
	{
		return gameObject_0.transform;
	}

	internal static void smethod_1(Transform transform_0, Vector3 vector3_0)
	{
		transform_0.position = vector3_0;
	}

	internal static void smethod_2(Transform transform_0, Quaternion quaternion_0)
	{
		transform_0.rotation = quaternion_0;
	}

	internal static bool smethod_3(Object object_0, Object object_1)
	{
		return object_0 == object_1;
	}

	internal static void smethod_4(Transform transform_0, Vector3 vector3_0)
	{
		transform_0.localScale = vector3_0;
	}
}
