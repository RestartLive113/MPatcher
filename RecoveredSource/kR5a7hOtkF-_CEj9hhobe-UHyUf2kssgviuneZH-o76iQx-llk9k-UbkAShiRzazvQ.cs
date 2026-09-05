using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;

[HarmonyPatch(typeof(SceneMan))]
[HarmonyPatch("SetDebugLog")]
internal class kR5a7hOtkF_0024_CEj9hhobe_0024UHyUf2kssgviuneZH_0024o76iQx_0024llk9k_0024UbkAShiRzazvQ
{
	[HarmonyPostfix]
	internal static void FeUAVwFbW6wGJJdNimZY9yI(string OIHFGPOPPDD, ref List<object> ___NIEBCMMHKIB)
	{
		if (smethod_0(OIHFGPOPPDD, global::_003CModule_003E.smethod_29<string>(1035833148u)))
		{
			object object_ = ((___NIEBCMMHKIB.Count <= 0) ? null : ___NIEBCMMHKIB[___NIEBCMMHKIB.Count - 1]);
			Class17.DMoImhPC2jNUt2tgmSezeT_IK_0024XK5Df9PmexG_CaKb1R(global::_003CModule_003E.smethod_27<string>(2296435023u), object_, Color.magenta);
		}
	}

	internal static bool smethod_0(string string_0, string string_1)
	{
		return string_0.StartsWith(string_1);
	}
}
