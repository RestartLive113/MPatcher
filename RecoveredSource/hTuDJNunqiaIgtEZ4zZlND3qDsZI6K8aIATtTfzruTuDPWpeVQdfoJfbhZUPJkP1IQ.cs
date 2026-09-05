using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using UnityEngine;

internal class hTuDJNunqiaIgtEZ4zZlND3qDsZI6K8aIATtTfzruTuDPWpeVQdfoJfbhZUPJkP1IQ
{
	[HarmonyPatch("NDJNNPFOCNN")]
	[HarmonyPatch(typeof(RideCameraController))]
	internal class Ub_v24plhMmVSac_0024GzzVjhuUmE_0024UCqc_USZ3biaVX5yEUSTxLefIWJkOkgQOIDSFu2Jz134aZSFOBze_7VlJ4oE
	{
		internal static float GetAxis(string axisName)
		{
			if (Gg155i91S6yyfnaswla_0024ldg && IdG30eRAlMla8XzhZhCv_Qo.ContainsKey(axisName))
			{
				return IdG30eRAlMla8XzhZhCv_Qo[axisName];
			}
			return Input.GetAxis(axisName);
		}

		[HarmonyTranspiler]
		public static IEnumerable<CodeInstruction> ME8a9fXTPeYc1JTUjzrWW9E(IEnumerable<CodeInstruction> ienumerable_0)
		{
			List<CodeInstruction> list = new List<CodeInstruction>(ienumerable_0);
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].opcode == OpCodes.Call && (i == 53 || i == 61))
				{
					list[i].operand = smethod_1(smethod_0(typeof(Ub_v24plhMmVSac_0024GzzVjhuUmE_0024UCqc_USZ3biaVX5yEUSTxLefIWJkOkgQOIDSFu2Jz134aZSFOBze_7VlJ4oE).TypeHandle), global::_003CModule_003E.smethod_27<string>(3086780158u), new Type[1] { smethod_0(typeof(string).TypeHandle) }, (Type[])null);
				}
			}
			return list.AsEnumerable();
		}

		internal static Type smethod_0(RuntimeTypeHandle runtimeTypeHandle_0)
		{
			return Type.GetTypeFromHandle(runtimeTypeHandle_0);
		}

		internal static MethodInfo smethod_1(Type type_0, string string_0, Type[] type_1, Type[] type_2)
		{
			return AccessTools.Method(type_0, string_0, type_1, type_2);
		}
	}

	private static bool Gg155i91S6yyfnaswla_0024ldg = false;

	private static Dictionary<string, float> IdG30eRAlMla8XzhZhCv_Qo = new Dictionary<string, float>();

	internal static void zzToKxYpZepZ4jDSGkZiZvA(string string_0, float float_0, bool bool_0 = true)
	{
		if (!IdG30eRAlMla8XzhZhCv_Qo.ContainsKey(string_0))
		{
			IdG30eRAlMla8XzhZhCv_Qo.Add(string_0, float_0);
		}
		else
		{
			IdG30eRAlMla8XzhZhCv_Qo[string_0] = float_0;
		}
		if (bool_0)
		{
			Gg155i91S6yyfnaswla_0024ldg = true;
		}
	}

	internal static void IjzFhrXDC8Hyn2Gro5LgxnE(string string_0)
	{
		if (IdG30eRAlMla8XzhZhCv_Qo.ContainsKey(string_0))
		{
			IdG30eRAlMla8XzhZhCv_Qo.Remove(string_0);
		}
	}

	internal static void xYQM3zh4cZTSxBGaK4_zvWY_0024PDUnanJq3rNYCS5kTCZ_0024()
	{
		IdG30eRAlMla8XzhZhCv_Qo.Clear();
	}

	internal static void ONOEOkTy9FTv8PQtRPsUpB8(bool bool_0, bool bool_1 = true)
	{
		Gg155i91S6yyfnaswla_0024ldg = bool_0;
		if (bool_1 && !Gg155i91S6yyfnaswla_0024ldg)
		{
			xYQM3zh4cZTSxBGaK4_zvWY_0024PDUnanJq3rNYCS5kTCZ_0024();
		}
	}
}
