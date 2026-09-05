using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using MPatchrMain;

[HarmonyPatch(new Type[] { typeof(string) })]
[HarmonyPatch(typeof(SceneMan))]
[HarmonyPatch("SetDebugLog")]
internal class Ojep9_ywtLMuNK7LiDZuXM7hMYHRARN0jK4l2Eu3p_0024Jx6MTX_qSNPCRKPRl_gGCzJA
{
	[HarmonyPrefix]
	public static bool smethod_0(string OIHFGPOPPDD, SceneMan __instance, ref List<object> ___NIEBCMMHKIB)
	{
		for (int i = 0; i < ___NIEBCMMHKIB.Count; i++)
		{
			object object_ = ___NIEBCMMHKIB[i];
			string string_ = (string)smethod_3(smethod_2(smethod_1(object_), global::_003CModule_003E.smethod_28<string>(4197729560u)), object_);
			if (smethod_4(string_, global::_003CModule_003E.smethod_25<string>(1082419733u)))
			{
				string value = smethod_5(smethod_5(string_, global::_003CModule_003E.smethod_28<string>(1940533780u), ""), global::_003CModule_003E.smethod_27<string>(1583484277u), "");
				if (MPatchr.string_2.Contains(value))
				{
					JKGKJLLFMLE.JNOHOLDLAKD = false;
					___NIEBCMMHKIB.RemoveAt(i);
				}
			}
		}
		return true;
	}

	internal static Type smethod_1(object object_0)
	{
		return object_0.GetType();
	}

	internal static FieldInfo smethod_2(Type type_0, string string_0)
	{
		return AccessTools.Field(type_0, string_0);
	}

	internal static object smethod_3(FieldInfo fieldInfo_0, object object_0)
	{
		return fieldInfo_0.GetValue(object_0);
	}

	internal static bool smethod_4(string string_0, string string_1)
	{
		return string_0.Contains(string_1);
	}

	internal static string smethod_5(string string_0, string string_1, string string_2)
	{
		return string_0.Replace(string_1, string_2);
	}
}
