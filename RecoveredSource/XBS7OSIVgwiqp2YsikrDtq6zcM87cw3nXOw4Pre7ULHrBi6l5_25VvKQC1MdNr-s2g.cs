using System.Collections.Generic;
using UnityEngine;

internal class XBS7OSIVgwiqp2YsikrDtq6zcM87cw3nXOw4Pre7ULHrBi6l5_25VvKQC1MdNr_0024s2g : pQCv4GWEmO7suQD936qIBn22jIu63BZDtto8pweiUAZY
{
	internal enum Enum1
	{
		box,
		spawnpoint,
		gate
	}

	private List<GameObject> mGsD_aMic82mpSwXGYyn184 = new List<GameObject>();

	private Vector3 sC7TVlJaywRg_0024a_5dqujETk;

	private Quaternion w9K4d9QT8cXOAZKHQEp0fRg;

	private Construct xcBvxcM_0024ckBeZyvdSoAkJoM;

	private Enum1 MrZDDetpveRMT__0024biC7h8tU;

	internal XBS7OSIVgwiqp2YsikrDtq6zcM87cw3nXOw4Pre7ULHrBi6l5_25VvKQC1MdNr_0024s2g(Vector3 pos, Quaternion rot, Construct inst, Enum1 type)
	{
		sC7TVlJaywRg_0024a_5dqujETk = pos;
		w9K4d9QT8cXOAZKHQEp0fRg = rot;
		xcBvxcM_0024ckBeZyvdSoAkJoM = inst;
		MrZDDetpveRMT__0024biC7h8tU = type;
	}

	public void bv4xsECkipN_002441Wa7mcfeqY()
	{
		switch (MrZDDetpveRMT__0024biC7h8tU)
		{
		case Enum1.box:
		{
			GameObject gameObject2 = Object.Instantiate(Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<WorldTable>(global::_003CModule_003E.smethod_27<string>(3306471932u), xcBvxcM_0024ckBeZyvdSoAkJoM).APOALNMPNPP, sC7TVlJaywRg_0024a_5dqujETk, w9K4d9QT8cXOAZKHQEp0fRg);
			smethod_0(gameObject2.GetComponent<PrimitiveController>(), Vector3.one, Color.white, 0);
			smethod_1((Object)gameObject2.GetComponent<PhotonView>());
			mGsD_aMic82mpSwXGYyn184.Add(gameObject2);
			break;
		}
		case Enum1.spawnpoint:
		{
			int num2 = -1;
			for (int j = 0; j < 30; j++)
			{
				if (!GameObject.Find(global::_003CModule_003E.smethod_29<string>(2432456008u) + j))
				{
					num2 = j;
					break;
				}
			}
			if (num2 >= 0)
			{
				GameObject gameObject3 = Object.Instantiate(xcBvxcM_0024ckBeZyvdSoAkJoM.DPOGGCGDIII, sC7TVlJaywRg_0024a_5dqujETk, w9K4d9QT8cXOAZKHQEp0fRg);
				gameObject3.name = global::_003CModule_003E.smethod_26<string>(2034731880u) + num2;
				mGsD_aMic82mpSwXGYyn184.Add(gameObject3);
			}
			break;
		}
		case Enum1.gate:
		{
			int num = -1;
			for (int i = 0; i < 100; i++)
			{
				if (!GameObject.Find(global::_003CModule_003E.smethod_25<string>(1063885900u) + i))
				{
					num = i;
					break;
				}
			}
			if (num >= 0)
			{
				GameObject gameObject = Object.Instantiate(xcBvxcM_0024ckBeZyvdSoAkJoM.DNHJICDBHJL, AJ95PT5qH7SI3aQp44bnfe9rXdzyiYcVJnP8p242egZvaWA5f7vGynYUg9dQSkzYlA.Sl3qb7_zclIQRMX7sWCu5EE, Quaternion.identity);
				gameObject.name = global::_003CModule_003E.smethod_28<string>(985071751u) + num;
				gameObject.transform.GetChild(0).localScale = new Vector3(10f, 10f);
				mGsD_aMic82mpSwXGYyn184.Add(gameObject);
			}
			break;
		}
		}
	}

	public void M_UYuO7m_5zc0NRNG1OJLAE()
	{
		foreach (GameObject item in mGsD_aMic82mpSwXGYyn184)
		{
			smethod_2(item, bool_0: false);
		}
	}

	internal static void smethod_0(PrimitiveController primitiveController_0, Vector3 vector3_0, Color color_0, int int_0)
	{
		primitiveController_0.SetParam(vector3_0, color_0, int_0);
	}

	internal static void smethod_1(Object object_0)
	{
		Object.Destroy(object_0);
	}

	internal static void smethod_2(GameObject gameObject_0, bool bool_0)
	{
		gameObject_0.SetActive(bool_0);
	}
}
