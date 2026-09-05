using System.Collections.Generic;
using System.Linq;
using HarmonyLib;

[HarmonyPatch("LateUpdate")]
[HarmonyPatch(typeof(FreeCameraController))]
internal static class Class21
{
	[HarmonyTranspiler]
	internal static IEnumerable<CodeInstruction> ME8a9fXTPeYc1JTUjzrWW9E(IEnumerable<CodeInstruction> instructions)
	{
		List<CodeInstruction> list = new List<CodeInstruction>(instructions.ToArray());
		list[344].operand = 0.1f;
		return list;
	}
}
