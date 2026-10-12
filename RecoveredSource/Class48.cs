using HarmonyLib;
using UnityEngine;

[HarmonyPriority(800)]
[HarmonyPatch(typeof(Option), "Update")]
internal class Class48
{
	[HarmonyPrefix]
	internal static bool smethod_0()
	{
		return !(smethod_1() < 1f);
	}

	internal static float smethod_1()
	{
		return Time.time;
	}
}
