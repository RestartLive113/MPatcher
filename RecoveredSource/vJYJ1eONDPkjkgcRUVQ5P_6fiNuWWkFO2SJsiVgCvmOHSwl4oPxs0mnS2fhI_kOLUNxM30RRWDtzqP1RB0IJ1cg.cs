using System;
using HarmonyLib;
using UnityEngine;

[HarmonyPatch("Start")]
[HarmonyPatch(typeof(SceneMan))]
internal class vJYJ1eONDPkjkgcRUVQ5P_6fiNuWWkFO2SJsiVgCvmOHSwl4oPxs0mnS2fhI_kOLUNxM30RRWDtzqP1RB0IJ1cg
{
	internal static bool IFHXlp0U7HTAqoVWOohoMMs;

	[HarmonyPostfix]
	internal static void FeUAVwFbW6wGJJdNimZY9yI(SceneMan __instance)
	{
		if (smethod_0() < 1f && smethod_1((object)__instance) != smethod_2(typeof(Menu).TypeHandle))
		{
			return;
		}
		if (smethod_1((object)__instance) == smethod_2(typeof(Build).TypeHandle))
		{
			tx5ezQjnJ588lHZb2y8_uGsRPxt8Gday9OvZVymtg997MmWK3bB9ajZOlUrRC07uCQhupP7i68Fu_hxQVh_EgfY.FeUAVwFbW6wGJJdNimZY9yI((Build)__instance);
		}
		else if (smethod_1((object)__instance) == smethod_2(typeof(Lobby).TypeHandle))
		{
			H2eRtg3j8LQbRFA5W4lA82a14657bmCSdMuY9EVVSHvtldL3sQGBzRpyzLFv3s9gUTKmzMfVSxsgWxXtkcUqLFk.FeUAVwFbW6wGJJdNimZY9yI((Lobby)__instance);
		}
		else if (smethod_1((object)__instance) == smethod_2(typeof(Configure).TypeHandle))
		{
			Class34.FeUAVwFbW6wGJJdNimZY9yI((Configure)__instance);
		}
		else if (smethod_1((object)__instance) == smethod_2(typeof(Menu).TypeHandle))
		{
			IFHXlp0U7HTAqoVWOohoMMs = true;
			Yh0MC1EmkntIzqy080BKjShiynuLMqAhQNZcSdNXa8NCs5hh969QYHVb1xRZpPUR1yCJJxvvjVN4EdwzA0sRu_U.FeUAVwFbW6wGJJdNimZY9yI((Menu)__instance);
		}
		else if (smethod_1((object)__instance) != smethod_2(typeof(Practice).TypeHandle))
		{
			if (smethod_1((object)__instance) == smethod_2(typeof(Meeting).TypeHandle))
			{
				tnEo2lQHuv72GoUQCRuo3XpzqJNq9wGnGwXKTgj2D_Tr2gLLgiLCAsHvK_0024muhHpMlUAwInSl_ayNxtzKmeKDXmE.FeUAVwFbW6wGJJdNimZY9yI((Meeting)__instance);
			}
			else if (smethod_1((object)__instance) == smethod_2(typeof(Construct).TypeHandle))
			{
				cueQi_wQP26TRIVnZP9Z8aKuGe8STPH1J69SyYpd7QAcLkTgNnDboqxHBenun9_0024iUyNPL_0yB16b1_0024sswP4Rryc.FeUAVwFbW6wGJJdNimZY9yI((Construct)__instance);
			}
		}
		else
		{
			qDuYWfwetcT__nj0tRnRsqTXK_0024vcku6SDcaPd3IHTKcDRKrbh7cnM7g9EFXlCgUN2AYyxU31a1vMLv21siAsQ_00248.FeUAVwFbW6wGJJdNimZY9yI((Practice)__instance);
		}
	}

	internal static float smethod_0()
	{
		return Time.realtimeSinceStartup;
	}

	internal static Type smethod_1(object object_0)
	{
		return object_0.GetType();
	}

	internal static Type smethod_2(RuntimeTypeHandle runtimeTypeHandle_0)
	{
		return Type.GetTypeFromHandle(runtimeTypeHandle_0);
	}
}
