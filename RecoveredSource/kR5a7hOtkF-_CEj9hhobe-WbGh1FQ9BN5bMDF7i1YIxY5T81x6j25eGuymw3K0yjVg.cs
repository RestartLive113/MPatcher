using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using HarmonyLib;
using UnityEngine;

internal static class kR5a7hOtkF_0024_CEj9hhobe_0024WbGh1FQ9BN5bMDF7i1YIxY5T81x6j25eGuymw3K0yjVg
{
	[HarmonyPatch("LateUpdate")]
	[HarmonyPatch(typeof(RideCameraController))]
	internal static class NBSSa3UOzg3vQZOrw3GdTwPMFeKbwKScd9b33CH9eASoo1tw68uCuqqdIJ6x4Ri_8EIiPPb0LVXhyZ77_00242sv_00245_ZDeuunPSeCu3PxpypyG1BlfZpifBLNouWJN4H7GDB2w
	{
		[HarmonyPrefix]
		internal static void smethod_0(ref Vector3 ___KMOGOCLJKGO)
		{
			if (___KMOGOCLJKGO.magnitude < 0.0001f)
			{
				___KMOGOCLJKGO.y = 0.001f;
			}
		}
	}

	[HarmonyPatch(typeof(HIPBCCKFFAG))]
	[HarmonyPatch("ACMGPBMMKNI")]
	internal static class KAFhrDJzdEfCrDGx4HtPO1h2EXkQ9HDiptu6SuATQ5js01GHtlUb97r_y0bVoPQ1TlcWtWHZCwAn9b3KJUd10n3fvEpzPaMWiGtetIP8sPr1eadAL1XObam4YBWeN2AO_g
	{
		[HarmonyTranspiler]
		internal static IEnumerable<CodeInstruction> ME8a9fXTPeYc1JTUjzrWW9E(IEnumerable<CodeInstruction> instructions)
		{
			List<CodeInstruction> list = new List<CodeInstruction>(instructions.ToArray());
			list[606].opcode = OpCodes.Nop;
			list[607].opcode = OpCodes.Nop;
			return list;
		}
	}
}
