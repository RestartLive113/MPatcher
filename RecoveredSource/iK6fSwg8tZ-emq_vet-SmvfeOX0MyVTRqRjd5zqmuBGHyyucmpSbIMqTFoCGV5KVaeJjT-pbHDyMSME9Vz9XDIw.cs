using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;

[HarmonyPatch("Start")]
[HarmonyPatch(typeof(TrackerController))]
internal static class iK6fSwg8tZ_0024emq_vet_0024SmvfeOX0MyVTRqRjd5zqmuBGHyyucmpSbIMqTFoCGV5KVaeJjT_0024pbHDyMSME9Vz9XDIw
{
	[HarmonyTranspiler]
	internal static IEnumerable<CodeInstruction> ME8a9fXTPeYc1JTUjzrWW9E(IEnumerable<CodeInstruction> instructions)
	{
		List<CodeInstruction> list = new List<CodeInstruction>(instructions);
		for (int i = 0; i < list.Count; i++)
		{
			if (i <= 37 && i >= 6)
			{
				list[i].opcode = OpCodes.Nop;
			}
		}
		return list;
	}

	[HarmonyPrefix]
	internal static bool smethod_0(TrackerController __instance)
	{
		if (oIuuMMCo9UmUIWmYFQEqxXAOJy8LYqspmW2FKyHMiXKdcT1cBP8p9QxEsmrfao0MIw.smethod_0(__instance))
		{
			Dictionary<string, object> dictionary = oIuuMMCo9UmUIWmYFQEqxXAOJy8LYqspmW2FKyHMiXKdcT1cBP8p9QxEsmrfao0MIw.smethod_2(__instance);
			if (dictionary.ContainsKey(global::_003CModule_003E.smethod_27<string>(1295771883u)) && (bool)dictionary[global::_003CModule_003E.smethod_28<string>(2529533100u)])
			{
				return false;
			}
		}
		return true;
	}
}
