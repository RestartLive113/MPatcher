using HarmonyLib;
using MPatchrMain;
using MPatchrMain.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

[HarmonyPatch(typeof(BlockData))]
[HarmonyPatch("GetWeight")]
internal static class cueQi_wQP26TRIVnZP9Z8aIXZH4JvDO0r_vy_0024_0024NeyIlt6nScWImBH6O_ma4h8XogcA
{
	[HarmonyPostfix]
	internal static void FeUAVwFbW6wGJJdNimZY9yI(BlockData __instance, ref float __result)
	{
		if ((HNJDDKJLHMM.FHLGOMHPDLN != HNJDDKJLHMM.HKGAACMIPIH.Offline && smethod_0().name != global::_003CModule_003E.smethod_25<string>(1518354409u)) || SceneManager.GetActiveScene().name == global::_003CModule_003E.smethod_27<string>(1490592012u) || !MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.weightChange || MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4 == null)
		{
			return;
		}
		if (!MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.noInvisWeight)
		{
			if (MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.noCouplerWeight && __instance.type == BlockData.AAHMDBHDCDK.Coupler)
			{
				__result = 0f;
			}
			return;
		}
		Color color = __instance.GetColor();
		if (color.r == 0f && color.g == 0f && color.b == 0f && __instance.IsTrans())
		{
			__result = 0f;
		}
	}

	internal static void KX8jtCPdJb997Zi2Dvgkpuc(Build __instance)
	{
		RectTransform parent = smethod_2(smethod_1(global::_003CModule_003E.smethod_28<string>(3214927773u)).smethod_0(global::_003CModule_003E.smethod_29<string>(2865663709u))).ToRect();
		Control0 control = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.uEsWMK_pFkCY_0024M5zt8zLsQk(global::_003CModule_003E.smethod_28<string>(1315450489u), new Vector3(105f, 140f), global::_003CModule_003E.smethod_25<string>(999155001u), parent, resetGroup: true, delegate(bool toggled)
		{
			if (MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4 == null)
			{
				MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4 = new mcpd();
			}
			MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.noCouplerWeight = toggled;
			MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.UUiRNMwxRbfk_Fs4cDErRoM();
		});
		Control0 control2 = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.uEsWMK_pFkCY_0024M5zt8zLsQk(global::_003CModule_003E.smethod_29<string>(3612131817u), new Vector3(105f, 90f), global::_003CModule_003E.smethod_26<string>(304927917u), parent, resetGroup: true, delegate(bool toggled)
		{
			if (MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4 == null)
			{
				MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4 = new mcpd();
			}
			MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.noInvisWeight = toggled;
			MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.UUiRNMwxRbfk_Fs4cDErRoM();
		});
		control.UzVS61irgJn5Pnqwx0lThng(new Vector2(190f, 40f));
		control2.UzVS61irgJn5Pnqwx0lThng(new Vector2(190f, 40f));
		if (MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4 != null)
		{
			control.hLxnG9Hq33zU_YUsu_00240_zak = MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.noCouplerWeight;
			control2.hLxnG9Hq33zU_YUsu_00240_zak = MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.noInvisWeight;
		}
	}

	internal static Scene smethod_0()
	{
		return SceneManager.GetActiveScene();
	}

	internal static GameObject smethod_1(string string_0)
	{
		return GameObject.Find(string_0);
	}

	internal static Transform smethod_2(GameObject gameObject_0)
	{
		return gameObject_0.transform;
	}
}
