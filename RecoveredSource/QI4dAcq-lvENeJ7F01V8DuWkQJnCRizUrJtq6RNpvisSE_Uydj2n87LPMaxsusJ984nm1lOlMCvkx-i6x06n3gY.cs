using System.IO;
using HarmonyLib;

[HarmonyPatch("JFAFKBHOOCE")]
[HarmonyPatch(typeof(JKGKJLLFMLE))]
internal class QI4dAcq_0024lvENeJ7F01V8DuWkQJnCRizUrJtq6RNpvisSE_Uydj2n87LPMaxsusJ984nm1lOlMCvkx_0024i6x06n3gY
{
	[HarmonyPrefix]
	internal static void smethod_0()
	{
		string string_ = JKGKJLLFMLE.LAOHLAOMCPN;
		if (smethod_1(JKGKJLLFMLE.IGOBPLOLHEP.folderName, string.Empty))
		{
			string_ = smethod_2(string_, JKGKJLLFMLE.IGOBPLOLHEP.folderName, global::_003CModule_003E.smethod_27<string>(2681980194u));
		}
		string_ = smethod_3(string_, JKGKJLLFMLE.IGOBPLOLHEP.machineName);
		if (smethod_4(smethod_3(string_, global::_003CModule_003E.smethod_29<string>(3585978138u))))
		{
			smethod_5(smethod_3(string_, global::_003CModule_003E.smethod_27<string>(4159897175u)));
		}
		if (smethod_4(smethod_3(string_, global::_003CModule_003E.smethod_29<string>(492757117u))))
		{
			smethod_5(smethod_3(string_, global::_003CModule_003E.smethod_28<string>(205653037u)));
		}
	}

	internal static bool smethod_1(string string_0, string string_1)
	{
		return string_0 != string_1;
	}

	internal static string smethod_2(string string_0, string string_1, string string_2)
	{
		return string_0 + string_1 + string_2;
	}

	internal static string smethod_3(string string_0, string string_1)
	{
		return string_0 + string_1;
	}

	internal static bool smethod_4(string string_0)
	{
		return File.Exists(string_0);
	}

	internal static void smethod_5(string string_0)
	{
		File.Delete(string_0);
	}
}
