using HarmonyLib;
using MPatchrMain;

[HarmonyPatch(typeof(PhotonView))]
[HarmonyPatch("Awake")]
internal static class MLPOLjLeebDuY_VDPBvb03j_0024WB7OWYG07MV3isC1kCg_0024
{
	[HarmonyPostfix]
	internal static void FeUAVwFbW6wGJJdNimZY9yI(ref PhotonView __instance)
	{
		if (MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.serverBusyMitigation)
		{
			__instance.IHKHKFDKDIP = PCNGIAEKFAP.UnreliableOnChange;
		}
	}
}
