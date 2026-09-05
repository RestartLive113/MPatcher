using System;
using System.Security.Policy;
using HarmonyLib;

[HarmonyPatch(new Type[]
{
	typeof(string),
	typeof(Evidence),
	typeof(bool)
})]
[HarmonyPatch("Load")]
[HarmonyPatch(typeof(AppDomain))]
internal class LWySeHCFt_0024EXG_0024kEBP6D8Lf4OuiveYnYMQgKyaEfigZlCMrdZjWqJZ51CUaNI1q02Q
{
	[HarmonyPrefix]
	public static void smethod_0(string assemblyString, Evidence assemblySecurity, bool refonly)
	{
		if (assemblyString == null || smethod_1(assemblyString) == 0)
		{
			mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(smethod_2(global::_003CModule_003E.smethod_26<string>(2714724734u), assemblyString));
			mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(smethod_3());
		}
	}

	internal static int smethod_1(string string_0)
	{
		return string_0.Length;
	}

	internal static string smethod_2(string string_0, string string_1)
	{
		return string_0 + string_1;
	}

	internal static string smethod_3()
	{
		return Environment.StackTrace;
	}
}
