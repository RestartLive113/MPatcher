using HarmonyLib;
using UnityEngine;

[HarmonyPatch(typeof(BAOJJGLHNCB))]
[HarmonyPatch("HLJMIPELKNI")]
internal class Class22
{
	[HarmonyPostfix]
	public static void FeUAVwFbW6wGJJdNimZY9yI(string AIOFKJCHNKM)
	{
		if (smethod_0((Object)(SceneMan.JFAOKFIDAGK as Arena), (Object)null) && smethod_1(AIOFKJCHNKM, global::_003CModule_003E.smethod_26<string>(4025105539u)))
		{
			smethod_3(smethod_2(global::_003CModule_003E.smethod_27<string>(793139142u)).GetComponent<RideCameraController>(), bool_0: true);
		}
	}

	internal static bool smethod_0(Object object_0, Object object_1)
	{
		return object_0 != object_1;
	}

	internal static bool smethod_1(string string_0, string string_1)
	{
		return string_0 == string_1;
	}

	internal static GameObject smethod_2(string string_0)
	{
		return GameObject.Find(string_0);
	}

	internal static void smethod_3(RideCameraController rideCameraController_0, bool bool_0)
	{
		rideCameraController_0.SetViewOption(bool_0);
	}
}
