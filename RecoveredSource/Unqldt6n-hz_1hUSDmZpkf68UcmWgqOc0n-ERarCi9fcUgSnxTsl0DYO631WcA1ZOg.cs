using HarmonyLib;
using MPatchrMain;
using McnCraft;
using UnityEngine;

[HarmonyPatch(typeof(MachineController))]
[HarmonyPatch("Start")]
internal static class Unqldt6n_0024hz_1hUSDmZpkf68UcmWgqOc0n_0024ERarCi9fcUgSnxTsl0DYO631WcA1ZOg
{
	[HarmonyPostfix]
	internal static void smethod_0(MachineController __instance)
	{
		if (smethod_2(smethod_1((Component)__instance), global::_003CModule_003E.smethod_25<string>(2898666520u)) && MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4 != null)
		{
			KEjZwYcSzxf2DUM4tsmq7LTU5Rz8bWNX1Ud16EuZVShS.SS((float)MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.vrScale / 100f);
		}
	}

	internal static string smethod_1(Component component_0)
	{
		return component_0.tag;
	}

	internal static bool smethod_2(string string_0, string string_1)
	{
		return string_0 == string_1;
	}
}
