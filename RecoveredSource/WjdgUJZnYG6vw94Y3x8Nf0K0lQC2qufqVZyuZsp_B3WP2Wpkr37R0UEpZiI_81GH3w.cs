using HarmonyLib;
using MPatchrMain;
using McnCraft;
using UnityEngine;

internal class WjdgUJZnYG6vw94Y3x8Nf0K0lQC2qufqVZyuZsp_B3WP2Wpkr37R0UEpZiI_81GH3w
{
	[HarmonyPatch("Update")]
	[HarmonyPatch(typeof(ScopeController))]
	internal class Class57
	{
		[HarmonyPrefix]
		internal static void smethod_0(ScopeController __instance)
		{
			if (!MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.scopeBodyHide || __instance.JNKEKNOAPHO.gid >= 7 || smethod_1((Object)__instance.FPEBEEJGFPI, (Object)null) || smethod_2(__instance.DLMKKFCHFNC) || !j5cKm_0024ddq656AXkBtKiMmpU(__instance))
			{
				return;
			}
			float num = Class17.DQGZpGELKqeKmXweFYGEcaXR73CqGywHQI6u2TATGL80<PartsController, float>(global::_003CModule_003E.smethod_29<string>(456996743u), __instance, new object[1] { true });
			int num2 = slJZYc0GtooLuQtYhdMrQNk(__instance);
			if (num2 >= __instance.FPEBEEJGFPI.ILBAAENKMBL.Count)
			{
				return;
			}
			foreach (MeshRenderer item in __instance.FPEBEEJGFPI.ILBAAENKMBL[num2].HDNFEKKLDKI)
			{
				smethod_3((Renderer)item, num < -999f);
			}
		}

		internal static bool smethod_1(Object object_0, Object object_1)
		{
			return object_0 == object_1;
		}

		internal static bool smethod_2(BodyController bodyController_0)
		{
			return bodyController_0.IsBroken();
		}

		internal static void smethod_3(Renderer renderer_0, bool bool_0)
		{
			renderer_0.enabled = bool_0;
		}
	}

	internal static bool j5cKm_0024ddq656AXkBtKiMmpU(BlockController bc)
	{
		if (!oIuuMMCo9UmUIWmYFQEqxXAOJy8LYqspmW2FKyHMiXKdcT1cBP8p9QxEsmrfao0MIw.smethod_0(bc))
		{
			return false;
		}
		return oIuuMMCo9UmUIWmYFQEqxXAOJy8LYqspmW2FKyHMiXKdcT1cBP8p9QxEsmrfao0MIw.smethod_2(bc).ContainsKey(global::_003CModule_003E.smethod_29<string>(4005495931u));
	}

	internal static int slJZYc0GtooLuQtYhdMrQNk(BlockController bc)
	{
		return (int)oIuuMMCo9UmUIWmYFQEqxXAOJy8LYqspmW2FKyHMiXKdcT1cBP8p9QxEsmrfao0MIw.smethod_2(bc).Y_0024dZy3YTHmOYBqFB7lE7fD_0024vb6NsuWzjnbcG6_CoYN52(global::_003CModule_003E.smethod_28<string>(3878945803u), 0);
	}
}
