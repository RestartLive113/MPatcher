using HarmonyLib;
using UnityEngine;

[HarmonyPatch(typeof(OffsetRenderer))]
[HarmonyPatch("LLOEOFBFLNP")]
internal static class bOo8x9nQoPUnPLha_0024sUCFCqhjTVaFjLEDRZK9mDqIg9yrCGxqCOAt3DLrt_mrOyUYOUF4ILJGSQEyiaIRTSrMu8
{
	[HarmonyPrefix]
	internal static void smethod_0(ref Color HJEGNEIKDDO, ref Material ___NKNONABDCOI)
	{
		HJEGNEIKDDO = new Color(HJEGNEIKDDO.r, HJEGNEIKDDO.g, HJEGNEIKDDO.b, 0.05f);
		if (!___NKNONABDCOI.shader.name.Contains(global::_003CModule_003E.smethod_26<string>(1066018454u)))
		{
			___NKNONABDCOI = new Material(Shader.Find(global::_003CModule_003E.smethod_25<string>(1987070897u)));
		}
	}
}
