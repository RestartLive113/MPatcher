using System;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;

[HarmonyPatch(new Type[] { })]
[HarmonyPatch("GetAssemblies")]
[HarmonyPatch(typeof(AppDomain))]
internal class Lpl5cKlEdlmgwHo99bHllIQS_0024supw_0024emM0b0ne_0024dfzPvuuoUFZL_0024bIH77XLRKzmaXw
{
	[HarmonyPostfix]
	private static void FeUAVwFbW6wGJJdNimZY9yI(ref Assembly[] __result)
	{
		if (__result == null || !smethod_1(smethod_0(), global::_003CModule_003E.smethod_25<string>(415064264u)))
		{
			return;
		}
		List<Assembly> list = new List<Assembly>();
		Assembly[] array = __result;
		foreach (Assembly assembly in array)
		{
			if (!smethod_3(smethod_2(assembly), global::_003CModule_003E.smethod_25<string>(2989234772u)) && !smethod_3(smethod_2(assembly), global::_003CModule_003E.smethod_26<string>(557973982u)) && !smethod_3(smethod_2(assembly), global::_003CModule_003E.smethod_28<string>(2502786170u)) && !smethod_3(smethod_2(assembly), global::_003CModule_003E.smethod_25<string>(2827625256u)) && smethod_4(smethod_2(assembly)) != 0 && smethod_4(smethod_5(assembly)) != 0)
			{
				mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(smethod_6(global::_003CModule_003E.smethod_27<string>(4132114038u), smethod_2(assembly), global::_003CModule_003E.smethod_27<string>(3745892527u)), bool_0: true);
				list.Add(assembly);
			}
		}
		__result = list.ToArray();
	}

	internal static string smethod_0()
	{
		return Environment.StackTrace;
	}

	internal static bool smethod_1(string string_0, string string_1)
	{
		return string_0.Contains(string_1);
	}

	internal static string smethod_2(Assembly assembly_0)
	{
		return assembly_0.FullName;
	}

	internal static bool smethod_3(string string_0, string string_1)
	{
		return string_0.StartsWith(string_1);
	}

	internal static int smethod_4(string string_0)
	{
		return string_0.Length;
	}

	internal static string smethod_5(Assembly assembly_0)
	{
		return assembly_0.Location;
	}

	internal static string smethod_6(string string_0, string string_1, string string_2)
	{
		return string_0 + string_1 + string_2;
	}
}
