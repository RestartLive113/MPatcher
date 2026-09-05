using UnityEngine;

internal static class ZVmMBmN3dIHG7u7B20FQXZ9agEoRSBS017F43aKnHUDm
{
	internal enum JbC7EEoTLFzUGcUjM5LVTslIR3vGX6Sr5oD2OVLNAvl4deIpWKIY5h5SWq_uYBnURHxAwouswPtILMHv8bo6ji8
	{
		Opaque,
		Cutout,
		Fade,
		Transparent
	}

	internal static void laPvoIuj2BnMu5ivLRRDnVJrUhb__0024L4pDUokXomuY9FP(this Material material_0, JbC7EEoTLFzUGcUjM5LVTslIR3vGX6Sr5oD2OVLNAvl4deIpWKIY5h5SWq_uYBnURHxAwouswPtILMHv8bo6ji8 jbC7EEoTLFzUGcUjM5LVTslIR3vGX6Sr5oD2OVLNAvl4deIpWKIY5h5SWq_uYBnURHxAwouswPtILMHv8bo6ji8_0)
	{
		switch (jbC7EEoTLFzUGcUjM5LVTslIR3vGX6Sr5oD2OVLNAvl4deIpWKIY5h5SWq_uYBnURHxAwouswPtILMHv8bo6ji8_0)
		{
		case JbC7EEoTLFzUGcUjM5LVTslIR3vGX6Sr5oD2OVLNAvl4deIpWKIY5h5SWq_uYBnURHxAwouswPtILMHv8bo6ji8.Opaque:
			smethod_0(material_0, global::_003CModule_003E.smethod_27<string>(211082992u), "");
			smethod_1(material_0, global::_003CModule_003E.smethod_27<string>(3486977780u), 1);
			smethod_1(material_0, global::_003CModule_003E.smethod_29<string>(3687263509u), 0);
			smethod_1(material_0, global::_003CModule_003E.smethod_26<string>(745391711u), 1);
			smethod_2(material_0, global::_003CModule_003E.smethod_26<string>(2230508716u));
			smethod_2(material_0, global::_003CModule_003E.smethod_28<string>(3428537129u));
			smethod_2(material_0, global::_003CModule_003E.smethod_28<string>(2973237624u));
			smethod_3(material_0, -1);
			break;
		case JbC7EEoTLFzUGcUjM5LVTslIR3vGX6Sr5oD2OVLNAvl4deIpWKIY5h5SWq_uYBnURHxAwouswPtILMHv8bo6ji8.Cutout:
			smethod_0(material_0, global::_003CModule_003E.smethod_27<string>(211082992u), global::_003CModule_003E.smethod_28<string>(2426878218u));
			smethod_1(material_0, global::_003CModule_003E.smethod_26<string>(1749357559u), 1);
			smethod_1(material_0, global::_003CModule_003E.smethod_25<string>(16754946u), 0);
			smethod_1(material_0, global::_003CModule_003E.smethod_26<string>(745391711u), 1);
			smethod_4(material_0, global::_003CModule_003E.smethod_29<string>(3546606820u));
			smethod_2(material_0, global::_003CModule_003E.smethod_27<string>(1076587364u));
			smethod_2(material_0, global::_003CModule_003E.smethod_26<string>(3917672974u));
			smethod_3(material_0, 2450);
			break;
		case JbC7EEoTLFzUGcUjM5LVTslIR3vGX6Sr5oD2OVLNAvl4deIpWKIY5h5SWq_uYBnURHxAwouswPtILMHv8bo6ji8.Fade:
			smethod_0(material_0, global::_003CModule_003E.smethod_27<string>(211082992u), global::_003CModule_003E.smethod_29<string>(1209730470u));
			smethod_1(material_0, global::_003CModule_003E.smethod_27<string>(3486977780u), 5);
			smethod_1(material_0, global::_003CModule_003E.smethod_29<string>(3687263509u), 10);
			smethod_1(material_0, global::_003CModule_003E.smethod_25<string>(1418221294u), 0);
			smethod_2(material_0, global::_003CModule_003E.smethod_25<string>(3215338355u));
			smethod_4(material_0, global::_003CModule_003E.smethod_28<string>(3428537129u));
			smethod_2(material_0, global::_003CModule_003E.smethod_26<string>(3917672974u));
			smethod_3(material_0, 3000);
			break;
		case JbC7EEoTLFzUGcUjM5LVTslIR3vGX6Sr5oD2OVLNAvl4deIpWKIY5h5SWq_uYBnURHxAwouswPtILMHv8bo6ji8.Transparent:
			smethod_0(material_0, global::_003CModule_003E.smethod_28<string>(651136045u), global::_003CModule_003E.smethod_27<string>(104045531u));
			smethod_1(material_0, global::_003CModule_003E.smethod_29<string>(2940795401u), 1);
			smethod_1(material_0, global::_003CModule_003E.smethod_28<string>(1607339109u), 10);
			smethod_1(material_0, global::_003CModule_003E.smethod_29<string>(138764321u), 0);
			smethod_2(material_0, global::_003CModule_003E.smethod_26<string>(2230508716u));
			smethod_2(material_0, global::_003CModule_003E.smethod_29<string>(1631700537u));
			smethod_4(material_0, global::_003CModule_003E.smethod_25<string>(3042139465u));
			smethod_3(material_0, 3000);
			break;
		}
	}

	internal static void smethod_0(Material material_0, string string_0, string string_1)
	{
		material_0.SetOverrideTag(string_0, string_1);
	}

	internal static void smethod_1(Material material_0, string string_0, int int_0)
	{
		material_0.SetInt(string_0, int_0);
	}

	internal static void smethod_2(Material material_0, string string_0)
	{
		material_0.DisableKeyword(string_0);
	}

	internal static void smethod_3(Material material_0, int int_0)
	{
		material_0.renderQueue = int_0;
	}

	internal static void smethod_4(Material material_0, string string_0)
	{
		material_0.EnableKeyword(string_0);
	}
}
