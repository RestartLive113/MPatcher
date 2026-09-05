using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HarmonyLib;

internal static class Class17
{
	private static Dictionary<string, FieldInfo> r0bd0arV7M3J3HcDHRs_0024YUY = new Dictionary<string, FieldInfo>();

	private static Dictionary<string, MethodInfo> AjqkZr2D4bFGCIqYzGhDqmI = new Dictionary<string, MethodInfo>();

	internal static T i0duGIJ6M7BXC7c2cbeyvXM1hCrcoNKNJzlqDrhwjoxM<T>(this object object_0, string string_0)
	{
		return iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<T>(string_0, object_0);
	}

	internal static T D7dqMa4OGuhdUnXHjJZM6vrBYDmyw_0024HWYteNzGVsTh5k<T>(this object object_0, string string_0, T gparam_0)
	{
		return DMoImhPC2jNUt2tgmSezeT_IK_0024XK5Df9PmexG_CaKb1R(string_0, object_0, gparam_0);
	}

	internal static T AMv3blqvprCVsYIr_00243bnU9PutMvqR_0024t23D7MAoFAY5Vr<T>(this object object_0, string string_0, params object[] object_1)
	{
		return DQGZpGELKqeKmXweFYGEcaXR73CqGywHQI6u2TATGL80<object, T>(string_0, object_0, object_1);
	}

	internal static void mMOtaBaCAdAJENkfiJ_1fdbLwBEsBnP8lprp9wMogCpK(this object object_0, string string_0, params object[] object_1)
	{
		DQGZpGELKqeKmXweFYGEcaXR73CqGywHQI6u2TATGL80(string_0, object_0, object_1);
	}

	internal static T iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<T>(string string_0, object object_0)
	{
		Type type = smethod_0(object_0);
		string text = smethod_2(smethod_1((MemberInfo)type), global::_003CModule_003E.smethod_29<string>(4275841543u), string_0);
		if (!r0bd0arV7M3J3HcDHRs_0024YUY.Keys.Contains(text))
		{
			r0bd0arV7M3J3HcDHRs_0024YUY.Add(text, smethod_3(type, string_0));
		}
		if (r0bd0arV7M3J3HcDHRs_0024YUY[text] == null)
		{
			throw smethod_4(global::_003CModule_003E.smethod_26<string>(601589858u));
		}
		return (T)smethod_5(r0bd0arV7M3J3HcDHRs_0024YUY[text], object_0);
	}

	internal static U iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<T, U>(string string_0, T gparam_0)
	{
		string text = smethod_2(smethod_6((object)smethod_1((MemberInfo)gparam_0.GetType())), global::_003CModule_003E.smethod_29<string>(4275841543u), string_0);
		if (!r0bd0arV7M3J3HcDHRs_0024YUY.Keys.Contains(text))
		{
			r0bd0arV7M3J3HcDHRs_0024YUY.Add(text, smethod_3(gparam_0.GetType(), string_0));
		}
		if (r0bd0arV7M3J3HcDHRs_0024YUY[text] == null)
		{
			throw smethod_4(global::_003CModule_003E.smethod_29<string>(40066158u));
		}
		return (U)smethod_5(r0bd0arV7M3J3HcDHRs_0024YUY[text], (object)gparam_0);
	}

	internal static void _0024kbvtNkp7Zv_00240Loc7zI_sdWmxQCKsS2GtiWeaeYYD9UE<T>(string string_0, string string_1, T gparam_0)
	{
		_0024kbvtNkp7Zv_00240Loc7zI_sdWmxQCKsS2GtiWeaeYYD9UE(smethod_7(string_0), string_1, gparam_0);
	}

	internal static void _0024kbvtNkp7Zv_00240Loc7zI_sdWmxQCKsS2GtiWeaeYYD9UE<T>(Type type_0, string string_0, T gparam_0)
	{
		string text = smethod_2(smethod_1((MemberInfo)type_0), global::_003CModule_003E.smethod_26<string>(3311948953u), string_0);
		if (!r0bd0arV7M3J3HcDHRs_0024YUY.Keys.Contains(text))
		{
			r0bd0arV7M3J3HcDHRs_0024YUY.Add(text, smethod_3(type_0, string_0));
		}
		if (r0bd0arV7M3J3HcDHRs_0024YUY[text] == null)
		{
			throw smethod_4(global::_003CModule_003E.smethod_28<string>(620683940u));
		}
		smethod_8(r0bd0arV7M3J3HcDHRs_0024YUY[text], (object)null, (object)gparam_0);
	}

	internal static T Ft_0024nB0s4dAgugQeebb6PgzB4yHCRQNxXyCsH1X_0024ugky6<T>(string string_0, string string_1)
	{
		return Ft_0024nB0s4dAgugQeebb6PgzB4yHCRQNxXyCsH1X_0024ugky6<T>(smethod_7(string_0), string_1);
	}

	internal static T Ft_0024nB0s4dAgugQeebb6PgzB4yHCRQNxXyCsH1X_0024ugky6<T>(Type type_0, string string_0)
	{
		string text = smethod_2(smethod_1((MemberInfo)type_0), global::_003CModule_003E.smethod_25<string>(3271100284u), string_0);
		if (!r0bd0arV7M3J3HcDHRs_0024YUY.Keys.Contains(text))
		{
			r0bd0arV7M3J3HcDHRs_0024YUY.Add(text, smethod_3(type_0, string_0));
		}
		if (r0bd0arV7M3J3HcDHRs_0024YUY[text] == null)
		{
			throw smethod_4(global::_003CModule_003E.smethod_29<string>(40066158u));
		}
		return (T)smethod_5(r0bd0arV7M3J3HcDHRs_0024YUY[text], (object)null);
	}

	internal static U DMoImhPC2jNUt2tgmSezeT_IK_0024XK5Df9PmexG_CaKb1R<T, U>(string string_0, T gparam_0, U gparam_1)
	{
		string text = smethod_2(smethod_6((object)smethod_1((MemberInfo)gparam_0.GetType())), global::_003CModule_003E.smethod_27<string>(3737871438u), string_0);
		if (!r0bd0arV7M3J3HcDHRs_0024YUY.Keys.Contains(text))
		{
			r0bd0arV7M3J3HcDHRs_0024YUY.Add(text, smethod_3(gparam_0.GetType(), string_0));
		}
		if (r0bd0arV7M3J3HcDHRs_0024YUY[text] == null)
		{
			throw smethod_4(global::_003CModule_003E.smethod_27<string>(1179022483u));
		}
		smethod_8(r0bd0arV7M3J3HcDHRs_0024YUY[text], (object)gparam_0, (object)gparam_1);
		return gparam_1;
	}

	internal static T DMoImhPC2jNUt2tgmSezeT_IK_0024XK5Df9PmexG_CaKb1R<T>(string string_0, object object_0, T gparam_0)
	{
		Type type = smethod_0(object_0);
		string text = smethod_2(smethod_1((MemberInfo)type), global::_003CModule_003E.smethod_27<string>(3737871438u), string_0);
		if (!r0bd0arV7M3J3HcDHRs_0024YUY.Keys.Contains(text))
		{
			r0bd0arV7M3J3HcDHRs_0024YUY.Add(text, smethod_3(type, string_0));
		}
		if (r0bd0arV7M3J3HcDHRs_0024YUY[text] == null)
		{
			throw smethod_4(global::_003CModule_003E.smethod_26<string>(601589858u));
		}
		smethod_8(r0bd0arV7M3J3HcDHRs_0024YUY[text], object_0, (object)gparam_0);
		return gparam_0;
	}

	internal static U DQGZpGELKqeKmXweFYGEcaXR73CqGywHQI6u2TATGL80<T, U>(string string_0, T gparam_0, params object[] object_0)
	{
		Type[] array = new Type[object_0.Length];
		for (int i = 0; i < object_0.Length; i++)
		{
			array[i] = smethod_0(object_0[i]);
		}
		return DQGZpGELKqeKmXweFYGEcaXR73CqGywHQI6u2TATGL80<T, U>(string_0, gparam_0, array, object_0);
	}

	internal static T B7nZ2IpiGUnaAxD41aPsZeindMl37RLuJ53DHCrGhW9j<T>(Type type_0, string string_0, params object[] object_0)
	{
		Type[] array = new Type[object_0.Length];
		for (int i = 0; i < object_0.Length; i++)
		{
			array[i] = smethod_0(object_0[i]);
		}
		return B7nZ2IpiGUnaAxD41aPsZeindMl37RLuJ53DHCrGhW9j<T>(type_0, string_0, array, object_0);
	}

	internal static T B7nZ2IpiGUnaAxD41aPsZeindMl37RLuJ53DHCrGhW9j<T>(Type type_0, string string_0, Type[] type_1, object[] object_0)
	{
		string text = smethod_2(smethod_1((MemberInfo)type_0), global::_003CModule_003E.smethod_28<string>(2167813015u), string_0);
		if (!AjqkZr2D4bFGCIqYzGhDqmI.Keys.Contains(text))
		{
			AjqkZr2D4bFGCIqYzGhDqmI.Add(text, smethod_9(type_0, string_0, type_1, (Type[])null));
		}
		if (AjqkZr2D4bFGCIqYzGhDqmI[text] == null)
		{
			throw smethod_4(global::_003CModule_003E.smethod_28<string>(620683940u));
		}
		return (T)smethod_10((MethodBase)AjqkZr2D4bFGCIqYzGhDqmI[text], (object)null, object_0);
	}

	internal static U DQGZpGELKqeKmXweFYGEcaXR73CqGywHQI6u2TATGL80<T, U>(string string_0, T gparam_0, Type[] type_0, object[] object_0)
	{
		string text = smethod_2(smethod_6((object)smethod_1((MemberInfo)gparam_0.GetType())), global::_003CModule_003E.smethod_26<string>(3311948953u), string_0);
		if (!AjqkZr2D4bFGCIqYzGhDqmI.Keys.Contains(text))
		{
			AjqkZr2D4bFGCIqYzGhDqmI.Add(text, smethod_9(gparam_0.GetType(), string_0, type_0, (Type[])null));
		}
		if (AjqkZr2D4bFGCIqYzGhDqmI[text] == null)
		{
			throw smethod_4(global::_003CModule_003E.smethod_29<string>(40066158u));
		}
		return (U)smethod_10((MethodBase)AjqkZr2D4bFGCIqYzGhDqmI[text], (object)gparam_0, object_0);
	}

	internal static void DQGZpGELKqeKmXweFYGEcaXR73CqGywHQI6u2TATGL80<T>(string string_0, T gparam_0, params object[] object_0)
	{
		DQGZpGELKqeKmXweFYGEcaXR73CqGywHQI6u2TATGL80<T, object>(string_0, gparam_0, object_0);
	}

	internal static void DQGZpGELKqeKmXweFYGEcaXR73CqGywHQI6u2TATGL80<T>(string string_0, T gparam_0, Type[] type_0, params object[] object_0)
	{
		DQGZpGELKqeKmXweFYGEcaXR73CqGywHQI6u2TATGL80<T, object>(string_0, gparam_0, type_0, object_0);
	}

	internal static Type smethod_0(object object_0)
	{
		return object_0.GetType();
	}

	internal static string smethod_1(MemberInfo memberInfo_0)
	{
		return memberInfo_0.Name;
	}

	internal static string smethod_2(string string_0, string string_1, string string_2)
	{
		return string_0 + string_1 + string_2;
	}

	internal static FieldInfo smethod_3(Type type_0, string string_0)
	{
		return AccessTools.Field(type_0, string_0);
	}

	internal static Exception smethod_4(string string_0)
	{
		return new Exception(string_0);
	}

	internal static object smethod_5(FieldInfo fieldInfo_0, object object_0)
	{
		return fieldInfo_0.GetValue(object_0);
	}

	internal static string smethod_6(object object_0)
	{
		return object_0.ToString();
	}

	internal static Type smethod_7(string string_0)
	{
		return AccessTools.TypeByName(string_0);
	}

	internal static void smethod_8(FieldInfo fieldInfo_0, object object_0, object object_1)
	{
		fieldInfo_0.SetValue(object_0, object_1);
	}

	internal static MethodInfo smethod_9(Type type_0, string string_0, Type[] type_1, Type[] type_2)
	{
		return AccessTools.Method(type_0, string_0, type_1, type_2);
	}

	internal static object smethod_10(MethodBase methodBase_0, object object_0, object[] object_1)
	{
		return methodBase_0.Invoke(object_0, object_1);
	}
}
