using ExitGames.Client.Photon;
using HarmonyLib;
using MPatchrMain;

[HarmonyPatch(typeof(Game))]
[HarmonyPatch("Start")]
internal class BDkfokxR71_dfvtXHNIeppLRPcz23oyvMspdw87a8wbKe3K5WnLuQqq4hNm5_0024cdlYA
{
	[HarmonyPrefix]
	internal static void smethod_0()
	{
		OPLNFKECCLE oplnfkeccle_ = smethod_1();
		Hashtable hashtable = smethod_2();
		if (MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.vcSupported)
		{
			hashtable.Add(global::_003CModule_003E.smethod_28<string>(3731872523u), global::_003CModule_003E.smethod_25<string>(3707867685u));
		}
		else
		{
			hashtable.Add(global::_003CModule_003E.smethod_26<string>(1403508453u), "");
		}
		smethod_3(oplnfkeccle_, hashtable, (Hashtable)null, bool_0: false);
	}

	internal static OPLNFKECCLE smethod_1()
	{
		return JONBPAFNPBD.DBLGHCEAEHC;
	}

	internal static Hashtable smethod_2()
	{
		return new Hashtable();
	}

	internal static void smethod_3(OPLNFKECCLE oplnfkeccle_0, Hashtable hashtable_0, Hashtable hashtable_1, bool bool_0)
	{
		oplnfkeccle_0.JCOEPEIHLNO(hashtable_0, hashtable_1, bool_0);
	}
}
