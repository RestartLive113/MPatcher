using HarmonyLib;
using UnityEngine;

[HarmonyPriority(800)]
[HarmonyPatch("Update")]
[HarmonyPatch(typeof(Option))]
[HarmonyPatch("Start")]
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
