using System.Collections.Generic;
using UnityEngine;

internal static class vcvWq48of1PpnetEh2AY81tkRkpDe4qOaCwTttO9vBY6
{
	private static GameObject ovcTxNaLJN9w_0024OzXTmIqde8;

	internal static Dictionary<string, GameObject> dictionary_0 = new Dictionary<string, GameObject>();

	private static void FulEIqsLedAamOu2LcwvlA8()
	{
		if (smethod_0((Object)ovcTxNaLJN9w_0024OzXTmIqde8, (Object)null))
		{
			ovcTxNaLJN9w_0024OzXTmIqde8 = smethod_1(global::_003CModule_003E.smethod_27<string>(2635449519u));
			smethod_2((Object)ovcTxNaLJN9w_0024OzXTmIqde8);
		}
	}

	internal static void cHLFJkgQ04bePoiDb6MAQaM(string string_0, GameObject gameObject_0)
	{
		if (smethod_0((Object)gameObject_0, (Object)null))
		{
			mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(smethod_3(global::_003CModule_003E.smethod_25<string>(3603051454u), string_0));
			return;
		}
		FulEIqsLedAamOu2LcwvlA8();
		if (!dictionary_0.ContainsKey(string_0))
		{
			GameObject gameObject = Object.Instantiate(gameObject_0);
			smethod_4(gameObject, bool_0: false);
			smethod_6(smethod_5(gameObject), smethod_5(ovcTxNaLJN9w_0024OzXTmIqde8));
			smethod_8((Object)gameObject, smethod_7((Object)gameObject_0));
			dictionary_0.Add(string_0, gameObject);
			mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(smethod_9(global::_003CModule_003E.smethod_28<string>(1759006806u), smethod_7((Object)gameObject), global::_003CModule_003E.smethod_25<string>(2630613524u), string_0), bool_0: true);
		}
	}

	internal static GameObject ts9_0024hOMpHope_fSUdeN3MnM(string string_0)
	{
		if (!dictionary_0.ContainsKey(string_0))
		{
			return null;
		}
		return dictionary_0[string_0];
	}

	internal static bool smethod_0(Object object_0, Object object_1)
	{
		return object_0 == object_1;
	}

	internal static GameObject smethod_1(string string_0)
	{
		return new GameObject(string_0);
	}

	internal static void smethod_2(Object object_0)
	{
		Object.DontDestroyOnLoad(object_0);
	}

	internal static string smethod_3(string string_0, string string_1)
	{
		return string_0 + string_1;
	}

	internal static void smethod_4(GameObject gameObject_0, bool bool_0)
	{
		gameObject_0.SetActive(bool_0);
	}

	internal static Transform smethod_5(GameObject gameObject_0)
	{
		return gameObject_0.transform;
	}

	internal static void smethod_6(Transform transform_0, Transform transform_1)
	{
		transform_0.SetParent(transform_1);
	}

	internal static string smethod_7(Object object_0)
	{
		return object_0.name;
	}

	internal static void smethod_8(Object object_0, string string_0)
	{
		object_0.name = string_0;
	}

	internal static string smethod_9(string string_0, string string_1, string string_2, string string_3)
	{
		return string_0 + string_1 + string_2 + string_3;
	}
}
