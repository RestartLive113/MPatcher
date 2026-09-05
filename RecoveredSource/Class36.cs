using System;
using System.Collections.Generic;
using HarmonyLib;

[HarmonyPatch(typeof(Game))]
[HarmonyPatch("JLCKEKFPCBM")]
[HarmonyPatch(new Type[] { typeof(string) })]
internal class Class36
{
	internal static Dictionary<string, Action<Game, string>> hJS8kPKIDNOtELzBVeai1g8 = new Dictionary<string, Action<Game, string>>();

	[HarmonyPrefix]
	public static bool smethod_0(Game __instance, string OIHFGPOPPDD, ref bool __result)
	{
		foreach (string key in hJS8kPKIDNOtELzBVeai1g8.Keys)
		{
			if (smethod_2(OIHFGPOPPDD, smethod_1(global::_003CModule_003E.smethod_26<string>(3415898919u), key)))
			{
				hJS8kPKIDNOtELzBVeai1g8[key](__instance, OIHFGPOPPDD);
				__result = true;
				return false;
			}
		}
		return true;
	}

	internal static string smethod_1(string string_0, string string_1)
	{
		return string_0 + string_1;
	}

	internal static bool smethod_2(string string_0, string string_1)
	{
		return string_0.StartsWith(string_1);
	}
}
