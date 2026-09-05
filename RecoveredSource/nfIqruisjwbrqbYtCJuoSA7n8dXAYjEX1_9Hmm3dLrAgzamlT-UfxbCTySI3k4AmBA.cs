using System.Collections.Generic;
using UnityEngine;

internal class nfIqruisjwbrqbYtCJuoSA7n8dXAYjEX1_9Hmm3dLrAgzamlT_0024UfxbCTySI3k4AmBA : pQCv4GWEmO7suQD936qIBn22jIu63BZDtto8pweiUAZY
{
	private List<GameObject> mGsD_aMic82mpSwXGYyn184 = new List<GameObject>();

	private PrimitiveData[] primitiveData_0;

	internal xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA[] rEBjKMgUQVEE8m2fhGbnJb4;

	private Construct xcBvxcM_0024ckBeZyvdSoAkJoM;

	private bool bool_0;

	internal nfIqruisjwbrqbYtCJuoSA7n8dXAYjEX1_9Hmm3dLrAgzamlT_0024UfxbCTySI3k4AmBA(PrimitiveData[] data, bool isLocal, Construct instance)
	{
		xcBvxcM_0024ckBeZyvdSoAkJoM = instance;
		primitiveData_0 = data;
		bool_0 = isLocal;
	}

	public void bv4xsECkipN_002441Wa7mcfeqY()
	{
		List<PrimitiveController> list = Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<List<PrimitiveController>>(global::_003CModule_003E.smethod_28<string>(1986730662u), xcBvxcM_0024ckBeZyvdSoAkJoM);
		List<GameObject> list2 = Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<List<GameObject>>(global::_003CModule_003E.smethod_28<string>(3732020730u), xcBvxcM_0024ckBeZyvdSoAkJoM);
		List<xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA> list3 = new List<xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA>();
		PrimitiveData[] array = primitiveData_0;
		foreach (PrimitiveData primitiveData_ in array)
		{
			list.Clear();
			list2.Clear();
			Vector3 vector = smethod_0(primitiveData_);
			if (bool_0)
			{
				smethod_1(primitiveData_, vector + AJ95PT5qH7SI3aQp44bnfe9rXdzyiYcVJnP8p242egZvaWA5f7vGynYUg9dQSkzYlA.Sl3qb7_zclIQRMX7sWCu5EE);
			}
			AJ95PT5qH7SI3aQp44bnfe9rXdzyiYcVJnP8p242egZvaWA5f7vGynYUg9dQSkzYlA.nCP8ytlafJMco8J36Ij3H7U(primitiveData_);
			xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA2 = null;
			if (list.Count <= 0)
			{
				if (list2.Count > 0)
				{
					xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA2 = new xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA(list2[0].GetComponent<PointController>());
				}
			}
			else
			{
				xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA2 = new xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA(list[0]);
			}
			mGsD_aMic82mpSwXGYyn184.Add(xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA2.eWUzF3zpMMjP5r9PB6rj474);
			list3.Add(xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA2);
			if (bool_0)
			{
				smethod_1(primitiveData_, vector);
			}
		}
		rEBjKMgUQVEE8m2fhGbnJb4 = list3.ToArray();
	}

	public void M_UYuO7m_5zc0NRNG1OJLAE()
	{
		foreach (GameObject item in mGsD_aMic82mpSwXGYyn184)
		{
			smethod_2((Object)item);
		}
	}

	internal static Vector3 smethod_0(PrimitiveData primitiveData_1)
	{
		return primitiveData_1.GetPos();
	}

	internal static void smethod_1(PrimitiveData primitiveData_1, Vector3 vector3_0)
	{
		primitiveData_1.SetPos(vector3_0);
	}

	internal static void smethod_2(Object object_0)
	{
		Object.Destroy(object_0);
	}
}
