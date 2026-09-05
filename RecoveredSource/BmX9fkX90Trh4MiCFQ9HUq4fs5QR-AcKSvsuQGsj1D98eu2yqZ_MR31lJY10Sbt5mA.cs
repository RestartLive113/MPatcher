using System.Collections.Generic;
using System.Linq;
using HarmonyLib;

[HarmonyPatch("BKACIJDGAPP")]
[HarmonyPatch(typeof(Build))]
internal class BmX9fkX90Trh4MiCFQ9HUq4fs5QR_0024AcKSvsuQGsj1D98eu2yqZ_MR31lJY10Sbt5mA
{
	[HarmonyTranspiler]
	internal static IEnumerable<CodeInstruction> ME8a9fXTPeYc1JTUjzrWW9E(IEnumerable<CodeInstruction> instructions)
	{
		CodeInstruction[] array = instructions.ToArray();
		List<CodeInstruction> list = new List<CodeInstruction>();
		for (int i = 0; i < array.Length; i++)
		{
			if ((i < 359 || i > 372) && (i < 405 || i > 418))
			{
				list.Add(array[i]);
			}
		}
		return list;
	}
}
