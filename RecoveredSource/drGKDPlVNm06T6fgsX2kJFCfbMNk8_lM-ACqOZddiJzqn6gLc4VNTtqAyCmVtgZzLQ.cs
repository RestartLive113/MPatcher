using System;
using HarmonyLib;
using McnCraft;
using UnityEngine;

[HarmonyPatch(typeof(MachineController))]
[HarmonyPatch("Initialize")]
[HarmonyPatch(new Type[]
{
	typeof(string),
	typeof(BuildData),
	typeof(AssignData),
	typeof(Vector3)
})]
internal class drGKDPlVNm06T6fgsX2kJFCfbMNk8_lM_0024ACqOZddiJzqn6gLc4VNTtqAyCmVtgZzLQ
{
	[HarmonyPostfix]
	public static void FeUAVwFbW6wGJJdNimZY9yI(MachineController __instance, string IIHCBGGIFDC, BuildData IHMCFFHELHL, AssignData HMILIMPBBCB, Vector3 PJAEMFPMNCO)
	{
		if (smethod_0(IIHCBGGIFDC, global::_003CModule_003E.smethod_27<string>(2210042082u)))
		{
			smethod_1((Component)__instance).AddComponent<gFDKM7L7DPdtlWCXlOB2N1XMdnb1esUg7YunDptXQOkgwlv5QQQg16hMgugsXX5C2g>().SM7qaOhP3D7z3ieetwFwbok(__instance);
		}
	}

	internal static bool smethod_0(string string_0, string string_1)
	{
		return string_0 == string_1;
	}

	internal static GameObject smethod_1(Component component_0)
	{
		return component_0.gameObject;
	}
}
