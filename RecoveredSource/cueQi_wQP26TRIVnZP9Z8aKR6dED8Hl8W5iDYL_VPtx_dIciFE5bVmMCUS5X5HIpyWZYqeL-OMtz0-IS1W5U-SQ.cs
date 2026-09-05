using HarmonyLib;
using MPatchrMain;

[HarmonyPatch(typeof(Build))]
[HarmonyPatch("ANDINIMKBLL")]
internal class cueQi_wQP26TRIVnZP9Z8aKR6dED8Hl8W5iDYL_VPtx_dIciFE5bVmMCUS5X5HIpyWZYqeL_0024OMtz0_0024IS1W5U_0024SQ
{
	[HarmonyPostfix]
	internal static void FeUAVwFbW6wGJJdNimZY9yI()
	{
		if (MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4 != null)
		{
			MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.UUiRNMwxRbfk_Fs4cDErRoM();
		}
	}
}
