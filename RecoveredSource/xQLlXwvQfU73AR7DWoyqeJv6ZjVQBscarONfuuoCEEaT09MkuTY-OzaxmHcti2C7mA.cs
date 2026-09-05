using UnityEngine;

internal class xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA
{
	internal enum Enum0
	{
		block,
		spawnpoint,
		gate
	}

	private PrimitiveController euuopm9sP_00244445LMEFD3Ewo;

	private PointController lDAchuFb5pNunYFpSJD82OE;

	internal GameObject cRC1fFhZVgamcERb3o0WavI;

	internal Enum0 MrZDDetpveRMT__0024biC7h8tU;

	internal Transform TIWjI8FsBk2nlZk9NO4HNOE
	{
		get
		{
			if (MrZDDetpveRMT__0024biC7h8tU == Enum0.block)
			{
				if (smethod_0((Object)euuopm9sP_00244445LMEFD3Ewo, (Object)null))
				{
					return null;
				}
				return smethod_1((Component)euuopm9sP_00244445LMEFD3Ewo);
			}
			if (!smethod_0((Object)lDAchuFb5pNunYFpSJD82OE, (Object)null))
			{
				return smethod_1((Component)lDAchuFb5pNunYFpSJD82OE);
			}
			return null;
		}
	}

	internal GameObject eWUzF3zpMMjP5r9PB6rj474
	{
		get
		{
			if (MrZDDetpveRMT__0024biC7h8tU == Enum0.block)
			{
				return smethod_2((Component)euuopm9sP_00244445LMEFD3Ewo);
			}
			return smethod_2((Component)lDAchuFb5pNunYFpSJD82OE);
		}
	}

	internal xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA(PrimitiveController prim)
	{
		MrZDDetpveRMT__0024biC7h8tU = Enum0.block;
		euuopm9sP_00244445LMEFD3Ewo = prim;
	}

	internal xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA(PointController point)
	{
		lDAchuFb5pNunYFpSJD82OE = point;
		MrZDDetpveRMT__0024biC7h8tU = (smethod_4(smethod_3((Object)point), global::_003CModule_003E.smethod_28<string>(1258251454u)) ? Enum0.spawnpoint : Enum0.gate);
		if (MrZDDetpveRMT__0024biC7h8tU == Enum0.gate)
		{
			cRC1fFhZVgamcERb3o0WavI = smethod_2((Component)smethod_5(smethod_1((Component)point), global::_003CModule_003E.smethod_25<string>(922636097u)));
		}
	}

	internal void method_0()
	{
		if (MrZDDetpveRMT__0024biC7h8tU == Enum0.block)
		{
			smethod_6(euuopm9sP_00244445LMEFD3Ewo);
		}
	}

	internal bool uZ_quI6GyZaDBH7Zba2IGp0(PrimitiveController p)
	{
		if (smethod_7((Object)euuopm9sP_00244445LMEFD3Ewo, (Object)null))
		{
			return smethod_0((Object)p, (Object)euuopm9sP_00244445LMEFD3Ewo);
		}
		return false;
	}

	internal bool uZ_quI6GyZaDBH7Zba2IGp0(PointController p)
	{
		if (smethod_7((Object)lDAchuFb5pNunYFpSJD82OE, (Object)null))
		{
			return smethod_0((Object)p, (Object)lDAchuFb5pNunYFpSJD82OE);
		}
		return false;
	}

	internal static bool smethod_0(Object object_0, Object object_1)
	{
		return object_0 == object_1;
	}

	internal static Transform smethod_1(Component component_0)
	{
		return component_0.transform;
	}

	internal static GameObject smethod_2(Component component_0)
	{
		return component_0.gameObject;
	}

	internal static string smethod_3(Object object_0)
	{
		return object_0.name;
	}

	internal static bool smethod_4(string string_0, string string_1)
	{
		return string_0.StartsWith(string_1);
	}

	internal static Transform smethod_5(Transform transform_0, string string_0)
	{
		return transform_0.Find(string_0);
	}

	internal static void smethod_6(PrimitiveController primitiveController_0)
	{
		primitiveController_0.UpdateUV();
	}

	internal static bool smethod_7(Object object_0, Object object_1)
	{
		return object_0 != object_1;
	}
}
