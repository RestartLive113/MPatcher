using System;
using HarmonyLib;

[HarmonyPatch(new Type[] { })]
[HarmonyPatch(typeof(FJLJNEKHKKH))]
[HarmonyPatch("GPPEBHBCFMP")]
internal class Yh0MC1EmkntIzqy080BKjSgpLa2CI4PTcamj03GzVohKShTG_0024_0024auwPq0_0024GE7v0_0024TcQ
{
	[HarmonyPrefix]
	public static bool smethod_0(ref int __result)
	{
		__result = 0;
		return false;
	}
}
