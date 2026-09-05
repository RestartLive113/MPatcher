using System;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using MPatchrMain;
using McnCraft;
using UnityEngine;

[HarmonyPatch(new Type[] { typeof(string) })]
[HarmonyPatch("JDDINBDEAHF")]
[HarmonyPatch(typeof(Game))]
internal class LWySeHCFt_0024EXG_0024kEBP6D8LdsoKS_0024IozE_LXqo1KfGYHLzV507qRs_A_b1EHLxwhVAg
{
	private static Dictionary<string, int> TtlziRx2WOBZfcxv9_0024JJaPc = new Dictionary<string, int>();

	[HarmonyPostfix]
	public static void FeUAVwFbW6wGJJdNimZY9yI(Game __instance, string INGDDFODJPD)
	{
		if (!MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.hardKick)
		{
			return;
		}
		int value = 0;
		if (TtlziRx2WOBZfcxv9_0024JJaPc.ContainsKey(INGDDFODJPD))
		{
			TtlziRx2WOBZfcxv9_0024JJaPc.TryGetValue(INGDDFODJPD, out value);
		}
		value++;
		if (value >= 3)
		{
			try
			{
				Host object_ = (Host)__instance;
				MachineController[] array = Arena.PBBCHKBJAEA.ToArray();
				List<int> list = (List<int>)smethod_2(smethod_1(smethod_0(typeof(Host).TypeHandle), global::_003CModule_003E.smethod_27<string>(3315676616u)), (object)object_);
				MachineController[] array2 = array;
				foreach (MachineController machineController in array2)
				{
					if (list.Contains(machineController.AKAFEPJIFKC))
					{
						PhotonView component = smethod_3((Component)machineController).GetComponent<PhotonView>();
						OPLNFKECCLE oplnfkeccle_ = smethod_4(component);
						smethod_5(smethod_4(component));
						smethod_6(oplnfkeccle_);
					}
				}
				return;
			}
			catch (Exception exception_)
			{
				mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(smethod_8(global::_003CModule_003E.smethod_26<string>(2674872847u), smethod_7(exception_)));
				return;
			}
		}
		TtlziRx2WOBZfcxv9_0024JJaPc[INGDDFODJPD] = value;
	}

	internal static Type smethod_0(RuntimeTypeHandle runtimeTypeHandle_0)
	{
		return Type.GetTypeFromHandle(runtimeTypeHandle_0);
	}

	internal static FieldInfo smethod_1(Type type_0, string string_0)
	{
		return AccessTools.Field(type_0, string_0);
	}

	internal static object smethod_2(FieldInfo fieldInfo_0, object object_0)
	{
		return fieldInfo_0.GetValue(object_0);
	}

	internal static GameObject smethod_3(Component component_0)
	{
		return component_0.gameObject;
	}

	internal static OPLNFKECCLE smethod_4(PhotonView photonView_0)
	{
		return photonView_0.MFNPHJFOIGC;
	}

	internal static void smethod_5(OPLNFKECCLE oplnfkeccle_0)
	{
		JONBPAFNPBD.HENOJKBLJKK(oplnfkeccle_0);
	}

	internal static bool smethod_6(OPLNFKECCLE oplnfkeccle_0)
	{
		return JONBPAFNPBD.DNPEBHAHKPA(oplnfkeccle_0);
	}

	internal static string smethod_7(Exception exception_0)
	{
		return exception_0.Message;
	}

	internal static string smethod_8(string string_0, string string_1)
	{
		return string_0 + string_1;
	}
}
