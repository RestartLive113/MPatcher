using MPatchrMain;
using UnityEngine;
using UnityEngine.UI;

internal class rR_1N7RjZ9cFTh9TDZDK25yKTCP9_aFycwr_xeHo48o18_0024KuFxwwqB2bCalyR3HV4w
{
	internal static void FeUAVwFbW6wGJJdNimZY9yI(Arena __instance)
	{
		if (MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.moreViewDistance)
		{
			GameObject gameObject_ = smethod_0(global::_003CModule_003E.smethod_29<string>(618767629u));
			Slider component = gameObject_.smethod_0(global::_003CModule_003E.smethod_25<string>(1765016336u)).GetComponent<Slider>();
			Slider component2 = gameObject_.smethod_0(global::_003CModule_003E.smethod_28<string>(3789367972u)).GetComponent<Slider>();
			smethod_1(component, 35f);
			smethod_2(component2, 5f);
		}
	}

	internal static GameObject smethod_0(string string_0)
	{
		return GameObject.Find(string_0);
	}

	internal static void smethod_1(Slider slider_0, float float_0)
	{
		slider_0.maxValue = float_0;
	}

	internal static void smethod_2(Slider slider_0, float float_0)
	{
		slider_0.minValue = float_0;
	}
}
