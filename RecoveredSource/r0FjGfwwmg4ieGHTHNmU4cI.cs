using System;
using UnityEngine;

internal static class r0FjGfwwmg4ieGHTHNmU4cI
{
	private static AssetBundle hdtSf6SIxOnQmagu57_GZNs;

	internal static T cqWoMNveroNrLO3XzL3B_0024XA<T>(string string_0) where T : UnityEngine.Object
	{
		if (smethod_0((UnityEngine.Object)hdtSf6SIxOnQmagu57_GZNs, (UnityEngine.Object)null))
		{
			hdtSf6SIxOnQmagu57_GZNs = smethod_1(global::_003CModule_003E.smethod_26<string>(1524875761u));
		}
		T val = (T)smethod_3(hdtSf6SIxOnQmagu57_GZNs, string_0, smethod_2(typeof(T).TypeHandle));
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(string_0 + global::_003CModule_003E.smethod_28<string>(2926744328u) + smethod_0((UnityEngine.Object)val, (UnityEngine.Object)null), bool_0: true);
		return val;
	}

	internal static object cqWoMNveroNrLO3XzL3B_0024XA(string string_0)
	{
		if (smethod_0((UnityEngine.Object)hdtSf6SIxOnQmagu57_GZNs, (UnityEngine.Object)null))
		{
			hdtSf6SIxOnQmagu57_GZNs = smethod_1(global::_003CModule_003E.smethod_26<string>(1524875761u));
		}
		return smethod_4(hdtSf6SIxOnQmagu57_GZNs, string_0);
	}

	internal static bool smethod_0(UnityEngine.Object object_0, UnityEngine.Object object_1)
	{
		return object_0 == object_1;
	}

	internal static AssetBundle smethod_1(string string_0)
	{
		return AssetBundle.LoadFromFile(string_0);
	}

	internal static Type smethod_2(RuntimeTypeHandle runtimeTypeHandle_0)
	{
		return Type.GetTypeFromHandle(runtimeTypeHandle_0);
	}

	internal static UnityEngine.Object smethod_3(AssetBundle assetBundle_0, string string_0, Type type_0)
	{
		return assetBundle_0.LoadAsset(string_0, type_0);
	}

	internal static UnityEngine.Object smethod_4(AssetBundle assetBundle_0, string string_0)
	{
		return assetBundle_0.LoadAsset(string_0);
	}
}
