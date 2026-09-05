using HarmonyLib;

[HarmonyPatch("OnFailedToConnectToPhoton")]
[HarmonyPatch(typeof(Connect))]
internal class _0024ICGGEmuSHiS4yKkMRFcT8e35reXck3MpVcnhLf251m9XytrQcHGzwATCnZLORd7rQ
{
	[HarmonyPrefix]
	internal static bool smethod_0(Connect __instance, JCOKOKKJKCN MOOOFNNCFJM)
	{
		smethod_2(smethod_1(global::_003CModule_003E.smethod_29<string>(1152855899u), MOOOFNNCFJM.ToString()));
		if (!smethod_3())
		{
			smethod_4();
			return false;
		}
		return false;
	}

	internal static string smethod_1(string string_0, string string_1)
	{
		return string_0 + string_1;
	}

	internal static void smethod_2(string string_0)
	{
		DP.CDF(string_0);
	}

	internal static bool smethod_3()
	{
		return JONBPAFNPBD.JNLBBLEEPBJ;
	}

	internal static bool smethod_4()
	{
		return JONBPAFNPBD.HFMCAAFANFO;
	}
}
