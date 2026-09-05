using HarmonyLib;
using MPatchrMain;
using UnityEngine;

[HarmonyPatch(typeof(EffectDistortion))]
[HarmonyPatch("OnRenderImage")]
internal static class m0HM_uVPHuetIhO98ec0YYXakAbCJhU7jw0q6FI9lBE6NNfOlW0PtzeNiryZBW4hGBKIOl92ebLgbYsZGLkYn2s
{
	[HarmonyPrefix]
	internal static bool smethod_0(ref RenderTexture GNKGDPAPIDP, ref RenderTexture FCHEHFKJKLG, EffectDistortion __instance)
	{
		if (!MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.waterEffectRange)
		{
			return true;
		}
		if (MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4 != null && MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.waterEffectHideRange != 0)
		{
			Arena arena = SceneMan.JFAOKFIDAGK as Arena;
			if (!smethod_1((Object)arena, (Object)null))
			{
				if (smethod_2((Object)arena.FICMBCLEFDL, (Object)null) && arena.FICMBCLEFDL.BBKOMHJGBPA < (float)MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.waterEffectHideRange)
				{
					FCHEHFKJKLG = GNKGDPAPIDP;
					return false;
				}
				return true;
			}
			return true;
		}
		return true;
	}

	internal static bool smethod_1(Object object_0, Object object_1)
	{
		return object_0 == object_1;
	}

	internal static bool smethod_2(Object object_0, Object object_1)
	{
		return object_0 != object_1;
	}
}
