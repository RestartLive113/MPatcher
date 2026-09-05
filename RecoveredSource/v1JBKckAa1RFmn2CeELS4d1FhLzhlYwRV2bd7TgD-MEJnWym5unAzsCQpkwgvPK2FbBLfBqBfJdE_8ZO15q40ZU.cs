using System;
using System.Reflection;
using HarmonyLib;
using UnityEngine;

[HarmonyPatch("CLDHJDELGEB")]
[HarmonyPatch(typeof(Lobby))]
internal class v1JBKckAa1RFmn2CeELS4d1FhLzhlYwRV2bd7TgD_0024MEJnWym5unAzsCQpkwgvPK2FbBLfBqBfJdE_8ZO15q40ZU
{
	internal static ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ LC0iMCkMK03PiX6mz5DQcnM;

	[HarmonyPrefix]
	internal static void smethod_0(Lobby __instance)
	{
		try
		{
			// The original ROOM CODE transport below is Photon-only. Individual is
			// handled by LegacyPrivateRooms through a code-specific MasterServer type.
			if (JKGKJLLFMLE.IGOBPLOLHEP.photonRegion == 4)
			{
				return;
			}
			if (!smethod_1((UnityEngine.Object)LC0iMCkMK03PiX6mz5DQcnM, (UnityEngine.Object)null))
			{
				return;
			}
			object object_ = smethod_4(smethod_3(smethod_2(typeof(JONBPAFNPBD).TypeHandle), global::_003CModule_003E.smethod_29<string>(2862941567u)), (object)null);
			if (smethod_8(((PEKCNAMEPIA)smethod_7(smethod_6(smethod_5(object_), global::_003CModule_003E.smethod_28<string>(3969561083u)), object_, new object[0])).CEFIDLFEHNE, LC0iMCkMK03PiX6mz5DQcnM.pZEKY5TzLd4S3z2lXESoRnw))
			{
				if (smethod_9(LC0iMCkMK03PiX6mz5DQcnM.pZEKY5TzLd4S3z2lXESoRnw))
				{
					fYYx8vN1tah7ZgfygXMH9eJU0YzjaUsqtv9tADffziMcu1k1It8P_0024ZciTdZuxN09bA.DGVijAeYURJ4Od_0024dBWRTwIk();
				}
				else
				{
					fYYx8vN1tah7ZgfygXMH9eJU0YzjaUsqtv9tADffziMcu1k1It8P_0024ZciTdZuxN09bA.DGVijAeYURJ4Od_0024dBWRTwIk(LC0iMCkMK03PiX6mz5DQcnM.pZEKY5TzLd4S3z2lXESoRnw);
				}
			}
		}
		catch (Exception)
		{
		}
	}

	internal static bool smethod_1(UnityEngine.Object object_0, UnityEngine.Object object_1)
	{
		return object_0 != object_1;
	}

	internal static Type smethod_2(RuntimeTypeHandle runtimeTypeHandle_0)
	{
		return Type.GetTypeFromHandle(runtimeTypeHandle_0);
	}

	internal static FieldInfo smethod_3(Type type_0, string string_0)
	{
		return AccessTools.Field(type_0, string_0);
	}

	internal static object smethod_4(FieldInfo fieldInfo_0, object object_0)
	{
		return fieldInfo_0.GetValue(object_0);
	}

	internal static Type smethod_5(object object_0)
	{
		return object_0.GetType();
	}

	internal static PropertyInfo smethod_6(Type type_0, string string_0)
	{
		return AccessTools.Property(type_0, string_0);
	}

	internal static object smethod_7(PropertyInfo propertyInfo_0, object object_0, object[] object_1)
	{
		return propertyInfo_0.GetValue(object_0, object_1);
	}

	internal static bool smethod_8(string string_0, string string_1)
	{
		return string_0 != string_1;
	}

	internal static bool smethod_9(string string_0)
	{
		return string.IsNullOrEmpty(string_0);
	}
}
