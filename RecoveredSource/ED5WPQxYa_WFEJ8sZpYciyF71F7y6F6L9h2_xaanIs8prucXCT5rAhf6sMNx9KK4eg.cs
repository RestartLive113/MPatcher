using HarmonyLib;
using MPatchrMain;
using UnityEngine;

[HarmonyPatch(typeof(Connect))]
[HarmonyPatch("Awake")]
internal class ED5WPQxYa_WFEJ8sZpYciyF71F7y6F6L9h2_xaanIs8prucXCT5rAhf6sMNx9KK4eg
{
	[HarmonyPostfix]
	private static void FeUAVwFbW6wGJJdNimZY9yI(Connect __instance)
	{
		smethod_0(MPatchr.string_0);
		smethod_1(MPatchr.string_0);
	}

	internal static void smethod_0(string string_0)
	{
		MasterServer.ipAddress = string_0;
	}

	internal static void smethod_1(string string_0)
	{
		Network.natFacilitatorIP = string_0;
	}
}
