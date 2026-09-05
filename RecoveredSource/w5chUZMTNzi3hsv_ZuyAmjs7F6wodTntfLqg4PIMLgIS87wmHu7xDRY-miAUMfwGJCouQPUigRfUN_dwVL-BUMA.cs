using HarmonyLib;

[HarmonyPatch("ICFKPPABNKO")]
[HarmonyPatch(typeof(SceneMan))]
internal class w5chUZMTNzi3hsv_ZuyAmjs7F6wodTntfLqg4PIMLgIS87wmHu7xDRY_0024miAUMfwGJCouQPUigRfUN_dwVL_0024BUMA
{
	[HarmonyPrefix]
	internal static void smethod_0(string EJKBFAFAEHM, ref bool BHCKMFDEBBH)
	{
		if (smethod_1(EJKBFAFAEHM, global::_003CModule_003E.smethod_29<string>(2215441490u)))
		{
			BHCKMFDEBBH = true;
		}
	}

	internal static bool smethod_1(string string_0, string string_1)
	{
		return string_0 == string_1;
	}
}
