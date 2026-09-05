using System;
using System.Collections.Generic;
using HarmonyLib;

[HarmonyPatch(typeof(HelpDefs))]
[HarmonyPatch(new Type[] { })]
[HarmonyPatch("Read")]
internal class fuLOHWIE5UYsXf1i_0024U7ZrRt7wYu2JGe7flr2Cyk0jSHi7p3wG28vwmnGpDW496_0024OrA
{
	[HarmonyPrefix]
	internal static void smethod_0()
	{
		smethod_1(global::_003CModule_003E.smethod_28<string>(3745097781u), global::_003CModule_003E.smethod_25<string>(1175409758u), global::_003CModule_003E.smethod_29<string>(948237685u), HelpDefs.optionHelpDict);
		smethod_1(global::_003CModule_003E.smethod_28<string>(269669594u), global::_003CModule_003E.smethod_29<string>(1413392415u), global::_003CModule_003E.smethod_29<string>(396713629u), HelpDefs.optionHelpDict);
	}

	internal static void smethod_1(string objName, string textJ, string textE, Dictionary<string, string> var, bool isRed = false)
	{
		string string_ = ((!isRed) ? string.Empty : global::_003CModule_003E.smethod_28<string>(4275415996u));
		string string_2 = smethod_2(JKGKJLLFMLE.IGOBPLOLHEP, SystemData.EHLMFKOOHLI.Modifier);
		string_ = ((!HelpDefs.isJ) ? smethod_4(string_, smethod_3(smethod_3(textE, global::_003CModule_003E.smethod_28<string>(1407696046u), string_2), global::_003CModule_003E.smethod_26<string>(4214194220u), string_2)) : smethod_4(string_, smethod_3(smethod_3(textJ, global::_003CModule_003E.smethod_28<string>(466941345u), string_2), global::_003CModule_003E.smethod_25<string>(3242604326u), string_2)));
		if (isRed)
		{
			string_ = smethod_4(string_, global::_003CModule_003E.smethod_26<string>(895463981u));
		}
		var[objName] = string_;
		if (smethod_5(objName, global::_003CModule_003E.smethod_28<string>(3684193571u)))
		{
			var[smethod_4(global::_003CModule_003E.smethod_28<string>(3502073769u), smethod_6(objName, 4))] = var[objName];
		}
	}

	internal static string smethod_2(SystemData systemData_0, SystemData.EHLMFKOOHLI ehlmfkoohli_0)
	{
		return systemData_0.GetKeyName(ehlmfkoohli_0);
	}

	internal static string smethod_3(string string_0, string string_1, string string_2)
	{
		return string_0.Replace(string_1, string_2);
	}

	internal static string smethod_4(string string_0, string string_1)
	{
		return string_0 + string_1;
	}

	internal static bool smethod_5(string string_0, string string_1)
	{
		return string_0.StartsWith(string_1);
	}

	internal static string smethod_6(string string_0, int int_0)
	{
		return string_0.Substring(int_0);
	}
}
