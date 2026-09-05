using System.Collections.Generic;
using UnityEngine;

internal static class Vp8bxrhzgk3tEfSlzu8650AEt3ed5anlFVSaJI37AOGo
{
	internal static string[] QvaSI_lheaW9ZiaD4SVRLVc(this ListController listController_0)
	{
		List<string> list = new List<string>();
		Transform transform_ = smethod_2(smethod_1(smethod_0((Component)listController_0)), global::_003CModule_003E.smethod_29<string>(2528701897u));
		for (int i = 0; i < smethod_7(transform_); i++)
		{
			Transform object_ = smethod_3(transform_, i);
			if (smethod_5(smethod_4((Object)object_), global::_003CModule_003E.smethod_29<string>(1641577100u)))
			{
				list.Add(smethod_6(smethod_4((Object)object_), 4));
			}
		}
		return list.ToArray();
	}

	internal static void FdKQJ0_0024IXUMd2Sl2psAEa2aauKmVEPLpTQDj8u9Kpq6w(this ListController listController_0, int int_0)
	{
		string[] array = listController_0.QvaSI_lheaW9ZiaD4SVRLVc();
		if (int_0 < array.Length)
		{
			smethod_8(listController_0, array[int_0]);
		}
	}

	internal static int EeNYggqZWtiar0rSt8jVD1eSfb4J4GSFkxD2kxLGahLU(this ListController listController_0)
	{
		string[] array = listController_0.QvaSI_lheaW9ZiaD4SVRLVc();
		string string_ = smethod_9(listController_0);
		for (int i = 0; i < array.Length; i++)
		{
			if (smethod_10(array[i], string_))
			{
				return i;
			}
		}
		return -1;
	}

	internal static Transform smethod_0(Component component_0)
	{
		return component_0.transform;
	}

	internal static Transform smethod_1(Transform transform_0)
	{
		return transform_0.parent;
	}

	internal static Transform smethod_2(Transform transform_0, string string_0)
	{
		return transform_0.Find(string_0);
	}

	internal static Transform smethod_3(Transform transform_0, int int_0)
	{
		return transform_0.GetChild(int_0);
	}

	internal static string smethod_4(Object object_0)
	{
		return object_0.name;
	}

	internal static bool smethod_5(string string_0, string string_1)
	{
		return string_0.StartsWith(string_1);
	}

	internal static string smethod_6(string string_0, int int_0)
	{
		return string_0.Substring(int_0);
	}

	internal static int smethod_7(Transform transform_0)
	{
		return transform_0.childCount;
	}

	internal static void smethod_8(ListController listController_0, string string_0)
	{
		listController_0.SetSelectedItem(string_0);
	}

	internal static string smethod_9(ListController listController_0)
	{
		return listController_0.GetSelectedItem();
	}

	internal static bool smethod_10(string string_0, string string_1)
	{
		return string_0 == string_1;
	}
}
