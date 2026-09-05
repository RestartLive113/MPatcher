using HarmonyLib;
using UnityEngine;

[HarmonyPatch("ALMFOELJEIJ")]
[HarmonyPatch(typeof(Game))]
internal class u8Ar2V0yBXu1MN706lBpr0cloYcEfFWi2mOKspfamqUe_cd8Q8IzHpYyhevoqoPeRwzeHCG7cneLxvJIVa5JjOk
{
	[HarmonyPrefix]
	internal static void smethod_0(Game __instance, GameObject ___BHEPGBODMGI, ref int ___DBMOILNIJKE)
	{
		if (JKGKJLLFMLE.EGFHGHKLNAO != JKGKJLLFMLE.LENPCAMMAEP.Meeting && smethod_1((Object)___BHEPGBODMGI) && smethod_2(___BHEPGBODMGI) && smethod_3(KeyCode.F5))
		{
			if (smethod_4(SystemData.EHLMFKOOHLI.Modifier) && (JKGKJLLFMLE.EGFHGHKLNAO == JKGKJLLFMLE.LENPCAMMAEP.Meeting || smethod_5()))
			{
				___DBMOILNIJKE = (___DBMOILNIJKE + 1) & 1;
			}
			Game.IGEAEEAMAPM = true;
		}
	}

	internal static bool smethod_1(Object object_0)
	{
		return object_0;
	}

	internal static bool smethod_2(GameObject gameObject_0)
	{
		return gameObject_0.activeSelf;
	}

	internal static bool smethod_3(KeyCode keyCode_0)
	{
		return HOCGCCAIPFF.FGCCNKAIKAI(keyCode_0);
	}

	internal static bool smethod_4(SystemData.EHLMFKOOHLI ehlmfkoohli_0)
	{
		return HOCGCCAIPFF.AFLJECMLJDL(ehlmfkoohli_0);
	}

	internal static bool smethod_5()
	{
		return JONBPAFNPBD.JNLBBLEEPBJ;
	}
}
