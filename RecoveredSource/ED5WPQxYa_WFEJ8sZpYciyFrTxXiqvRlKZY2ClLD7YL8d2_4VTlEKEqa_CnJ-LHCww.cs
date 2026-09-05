using System;
using HarmonyLib;
using MPatchrMain;
using UnityEngine;

[HarmonyPatch(typeof(SceneMan))]
[HarmonyPatch(new Type[] { typeof(string) })]
[HarmonyPatch("SetDebugLog")]
internal class ED5WPQxYa_WFEJ8sZpYciyFrTxXiqvRlKZY2ClLD7YL8d2_4VTlEKEqa_CnJ_0024LHCww
{
	[HarmonyPatch("HAMLNBEBDOB")]
	[HarmonyPatch(typeof(KEFHJCGICLE))]
	internal class hn_0024ELiQrP8aH_0024bKB9pUwsE_0024osUUSCrHDVRVhmKMuL2fpIas23d1_0024l4PLLbyw7gKMUC5jfB4pGqpMJXAZUkLFFs_00240GoiElm78j8lH4V5xEl2n
	{
		[HarmonyPrefix]
		internal static void smethod_0()
		{
			if (MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.tracing && !smethod_2((UnityEngine.Object)smethod_1(), (UnityEngine.Object)null))
			{
				string text = SceneMan.JFAOKFIDAGK.i0duGIJ6M7BXC7c2cbeyvXM1hCrcoNKNJzlqDrhwjoxM<string>(global::_003CModule_003E.smethod_26<string>(1794085954u));
				if (!smethod_3(text, global::_003CModule_003E.smethod_26<string>(3439586678u)) && !smethod_2((UnityEngine.Object)smethod_1().GetComponent<SEGI>(), (UnityEngine.Object)null))
				{
					text = text + global::_003CModule_003E.smethod_29<string>(1214769952u) + (int)smethod_1().GetComponent<SEGI>().vramUsage + global::_003CModule_003E.smethod_28<string>(2303735935u);
					SceneMan.JFAOKFIDAGK.D7dqMa4OGuhdUnXHjJZM6vrBYDmyw_0024HWYteNzGVsTh5k(global::_003CModule_003E.smethod_25<string>(2942204981u), text);
				}
			}
		}

		internal static Camera smethod_1()
		{
			return Camera.main;
		}

		internal static bool smethod_2(UnityEngine.Object object_0, UnityEngine.Object object_1)
		{
			return object_0 == object_1;
		}

		internal static bool smethod_3(string string_0, string string_1)
		{
			return string_0.Contains(string_1);
		}
	}

	[HarmonyPrefix]
	internal static bool smethod_0(ref string OIHFGPOPPDD)
	{
		if (smethod_1(OIHFGPOPPDD, global::_003CModule_003E.smethod_27<string>(3027287882u)))
		{
			return false;
		}
		return true;
	}

	internal static bool smethod_1(string string_0, string string_1)
	{
		return string_0.StartsWith(string_1);
	}
}
