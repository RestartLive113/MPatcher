using System;
using System.Runtime.CompilerServices;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;

[HarmonyPatch(typeof(Configure))]
[HarmonyPatch("OPMMCNOHEMC")]
internal class Class54
{
	[CompilerGenerated]
	private sealed class JcLqAy8TrKCYStYwwp96zbOhYNWn3kM_0024t7rrl_0024ZrVf5Om0Owrt6nGusnyg2nwMNHWm4MvzLpU6iOnfwRRgims4hWKKSi1V5sX3wAIYafZPPzDUoCSD5t_0024kOqHNXZNRbdhS3crSjCoKemq0MBMdBZQEs
	{
		public Slider cxx2zyrg_7SSAGGYxaI893c;

		public Configure wQ6mrkDog7tAEXGul0Y8Sv0;

		internal void Igw1p5BTtR8vumMgD8M16qo(AmMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F _)
		{
			if (smethod_0(cxx2zyrg_7SSAGGYxaI893c) > 20000f && smethod_2(smethod_1((SceneMan)wQ6mrkDog7tAEXGul0Y8Sv0, global::_003CModule_003E.smethod_25<string>(2574454333u))) == 0)
			{
				smethod_3(cxx2zyrg_7SSAGGYxaI893c, 20000f);
			}
		}

		internal static float smethod_0(Slider slider_0)
		{
			return slider_0.value;
		}

		internal static string smethod_1(SceneMan sceneMan_0, string string_0)
		{
			return sceneMan_0.GetInputText(string_0);
		}

		internal static int smethod_2(string string_0)
		{
			return string_0.Length;
		}

		internal static void smethod_3(Slider slider_0, float float_0)
		{
			slider_0.value = float_0;
		}
	}

	[HarmonyPrefix]
	internal static void smethod_0(Configure __instance)
	{
		Class17._0024kbvtNkp7Zv_00240Loc7zI_sdWmxQCKsS2GtiWeaeYYD9UE(smethod_1(typeof(KEFHJCGICLE).TypeHandle), global::_003CModule_003E.smethod_25<string>(216425283u), smethod_2());
		SliderController component = smethod_3((SceneMan)__instance, global::_003CModule_003E.smethod_25<string>(4239687823u)).GetComponent<SliderController>();
		Slider cxx2zyrg_7SSAGGYxaI893c = H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.H1Pr8mmPslEwoIU22reQcXE(component);
		int maxCost = JKGKJLLFMLE.JNOGNOMLMEA.maxCost;
		component.g23H_TLekrfTgNSrRFLT4ZA(0);
		smethod_4(cxx2zyrg_7SSAGGYxaI893c, 99999f);
		smethod_5(cxx2zyrg_7SSAGGYxaI893c, 1000f);
		smethod_6(cxx2zyrg_7SSAGGYxaI893c, (float)maxCost);
		smethod_7((Component)component).AddComponent<AmMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F>().U_0024eIoX6e3N9Ag_us_EcGHBI(delegate
		{
			if (JcLqAy8TrKCYStYwwp96zbOhYNWn3kM_0024t7rrl_0024ZrVf5Om0Owrt6nGusnyg2nwMNHWm4MvzLpU6iOnfwRRgims4hWKKSi1V5sX3wAIYafZPPzDUoCSD5t_0024kOqHNXZNRbdhS3crSjCoKemq0MBMdBZQEs.smethod_0(cxx2zyrg_7SSAGGYxaI893c) > 20000f && JcLqAy8TrKCYStYwwp96zbOhYNWn3kM_0024t7rrl_0024ZrVf5Om0Owrt6nGusnyg2nwMNHWm4MvzLpU6iOnfwRRgims4hWKKSi1V5sX3wAIYafZPPzDUoCSD5t_0024kOqHNXZNRbdhS3crSjCoKemq0MBMdBZQEs.smethod_2(JcLqAy8TrKCYStYwwp96zbOhYNWn3kM_0024t7rrl_0024ZrVf5Om0Owrt6nGusnyg2nwMNHWm4MvzLpU6iOnfwRRgims4hWKKSi1V5sX3wAIYafZPPzDUoCSD5t_0024kOqHNXZNRbdhS3crSjCoKemq0MBMdBZQEs.smethod_1((SceneMan)__instance, global::_003CModule_003E.smethod_25<string>(2574454333u))) == 0)
			{
				JcLqAy8TrKCYStYwwp96zbOhYNWn3kM_0024t7rrl_0024ZrVf5Om0Owrt6nGusnyg2nwMNHWm4MvzLpU6iOnfwRRgims4hWKKSi1V5sX3wAIYafZPPzDUoCSD5t_0024kOqHNXZNRbdhS3crSjCoKemq0MBMdBZQEs.smethod_3(cxx2zyrg_7SSAGGYxaI893c, 20000f);
			}
		});
	}

	internal static Type smethod_1(RuntimeTypeHandle runtimeTypeHandle_0)
	{
		return Type.GetTypeFromHandle(runtimeTypeHandle_0);
	}

	internal static int smethod_2()
	{
		return Time.frameCount;
	}

	internal static GameObject smethod_3(SceneMan sceneMan_0, string string_0)
	{
		return sceneMan_0.GetSLD(string_0);
	}

	internal static void smethod_4(Slider slider_0, float float_0)
	{
		slider_0.maxValue = float_0;
	}

	internal static void smethod_5(Slider slider_0, float float_0)
	{
		slider_0.minValue = float_0;
	}

	internal static void smethod_6(Slider slider_0, float float_0)
	{
		slider_0.value = float_0;
	}

	internal static GameObject smethod_7(Component component_0)
	{
		return component_0.gameObject;
	}
}
