using System;
using HarmonyLib;
using MPatchrMain;

[HarmonyPatch(typeof(JKGKJLLFMLE))]
[HarmonyPatch(new Type[] { typeof(bool) })]
[HarmonyPatch("MIONNHPELLN")]
internal class ExPt_0024JMW8Hl4nzpohpXvahVyl7vQc0OWMdFPWvITtQMR
{
	[HarmonyPostfix]
	internal static void FeUAVwFbW6wGJJdNimZY9yI()
	{
		while (JKGKJLLFMLE.AEOGMEAKNOL == null || JKGKJLLFMLE.IGOBPLOLHEP == null)
		{
		}
		MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4 = null;
		string text = CeGPiEeLcGa_jAqF8L8J7S5Vi4pPh_0024X8_GlojvQ92gul.smethod_0(global::_003CModule_003E.smethod_29<string>(1037059285u));
		if (text != null)
		{
			mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_28<string>(529031211u));
			MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4 = mcpd.smethod_0(text);
		}
	}
}
