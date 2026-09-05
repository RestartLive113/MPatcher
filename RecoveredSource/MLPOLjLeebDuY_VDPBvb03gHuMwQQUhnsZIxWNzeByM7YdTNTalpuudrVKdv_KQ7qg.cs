using System;
using System.Collections.Generic;
using System.Reflection;
using CSharpCompiler;
using HarmonyLib;

[HarmonyPatch(new Type[] { typeof(IEnumerable<string>) })]
[HarmonyPatch(typeof(BDDCFNLNBKJ))]
[HarmonyPatch("BHMLNIJBDPI")]
internal class MLPOLjLeebDuY_VDPBvb03gHuMwQQUhnsZIxWNzeByM7YdTNTalpuudrVKdv_KQ7qg
{
	[HarmonyPrefix]
	internal static bool smethod_0(ref BDDCFNLNBKJ __instance, ref BDDCFNLNBKJ.KNMBANDPIPK __result, ref List<BDDCFNLNBKJ.KNMBANDPIPK> ___NGCOIDGFABO, IEnumerable<string> DKKKLIMFDEE)
	{
		try
		{
			BDDCFNLNBKJ.KNMBANDPIPK kNMBANDPIPK = smethod_1(__instance, DKKKLIMFDEE);
			___NGCOIDGFABO.Add(kNMBANDPIPK);
			__result = kNMBANDPIPK;
		}
		catch (TargetInvocationException)
		{
		}
		return false;
	}

	internal static BDDCFNLNBKJ.KNMBANDPIPK smethod_1(BDDCFNLNBKJ bddcfnlnbkj_0, IEnumerable<string> ienumerable_0)
	{
		return new BDDCFNLNBKJ.KNMBANDPIPK(bddcfnlnbkj_0, ienumerable_0);
	}
}
