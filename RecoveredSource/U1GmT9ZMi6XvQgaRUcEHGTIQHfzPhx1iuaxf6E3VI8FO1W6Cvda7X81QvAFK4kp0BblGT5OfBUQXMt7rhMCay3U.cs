using HarmonyLib;
using UnityEngine;

[HarmonyPatch(typeof(TagController))]
[HarmonyPatch("LateUpdate")]
internal class U1GmT9ZMi6XvQgaRUcEHGTIQHfzPhx1iuaxf6E3VI8FO1W6Cvda7X81QvAFK4kp0BblGT5OfBUQXMt7rhMCay3U
{
	[HarmonyPostfix]
	private static void FeUAVwFbW6wGJJdNimZY9yI(ref bool ___IPDNALAPIBA, ref Renderer[] ___NKCENNPHFHL)
	{
		if (JKGKJLLFMLE.EGFHGHKLNAO != JKGKJLLFMLE.LENPCAMMAEP.Meeting && smethod_0())
		{
			___IPDNALAPIBA = true;
			Renderer[] array = ___NKCENNPHFHL;
			for (int i = 0; i < array.Length; i++)
			{
				smethod_1(array[i], bool_0: true);
			}
		}
	}

	internal static bool smethod_0()
	{
		return JONBPAFNPBD.JNLBBLEEPBJ;
	}

	internal static void smethod_1(Renderer renderer_0, bool bool_0)
	{
		renderer_0.enabled = bool_0;
	}
}
