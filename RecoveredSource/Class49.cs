using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;

[HarmonyPatch(typeof(SceneMan))]
[HarmonyPatch("CBJFLEKFOBG")]
internal class Class49
{
	[HarmonyPostfix]
	internal static void FeUAVwFbW6wGJJdNimZY9yI(GameObject NGLBLAGMBLN, SceneMan __instance)
	{
		if (smethod_1(smethod_0((Object)NGLBLAGMBLN), global::_003CModule_003E.smethod_26<string>(741209984u)))
		{
			InputField component = NGLBLAGMBLN.GetComponent<InputField>();
			if (smethod_2(component) == 40)
			{
				smethod_3(component, 140);
			}
		}
		if (smethod_1(smethod_0((Object)NGLBLAGMBLN), global::_003CModule_003E.smethod_25<string>(1902415748u)) || smethod_1(smethod_0((Object)NGLBLAGMBLN), global::_003CModule_003E.smethod_29<string>(2591707153u)))
		{
			InputField component2 = NGLBLAGMBLN.GetComponent<InputField>();
			if (smethod_2(component2) <= 17)
			{
				smethod_3(component2, 40);
			}
		}
	}

	internal static string smethod_0(Object object_0)
	{
		return object_0.name;
	}

	internal static bool smethod_1(string string_0, string string_1)
	{
		return string_0 == string_1;
	}

	internal static int smethod_2(InputField inputField_0)
	{
		return inputField_0.characterLimit;
	}

	internal static void smethod_3(InputField inputField_0, int int_0)
	{
		inputField_0.characterLimit = int_0;
	}
}
