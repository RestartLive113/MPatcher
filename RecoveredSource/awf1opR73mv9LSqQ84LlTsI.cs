using System;
using RuntimeInspectorNamespace;
using UnityEngine;

internal static class awf1opR73mv9LSqQ84LlTsI
{
	private static AssetBundle hdtSf6SIxOnQmagu57_GZNs;

	internal static T cqWoMNveroNrLO3XzL3B_0024XA<T>(string string_0) where T : UnityEngine.Object
	{
		if (smethod_0((UnityEngine.Object)hdtSf6SIxOnQmagu57_GZNs, (UnityEngine.Object)null))
		{
			hdtSf6SIxOnQmagu57_GZNs = smethod_1(global::_003CModule_003E.smethod_26<string>(2462096407u));
		}
		return (T)smethod_3(hdtSf6SIxOnQmagu57_GZNs, string_0, smethod_2(typeof(T).TypeHandle));
	}

	internal static void oJN_00244IcEU0waAX7Zww3G6zI()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(cqWoMNveroNrLO3XzL3B_0024XA<GameObject>(global::_003CModule_003E.smethod_25<string>(1903011641u)), smethod_5(smethod_4(global::_003CModule_003E.smethod_27<string>(862174272u))));
		Transform transform = smethod_5(gameObject);
		transform.localPosition = smethod_6(transform) - new Vector3(500f, 0f);
		GameObject gameObject2 = UnityEngine.Object.Instantiate(cqWoMNveroNrLO3XzL3B_0024XA<GameObject>(global::_003CModule_003E.smethod_26<string>(14677088u)), GameObject.Find(global::_003CModule_003E.smethod_29<string>(618767629u)).transform);
		gameObject2.transform.localPosition -= new Vector3(-500f, 0f);
		gameObject2.GetComponent<RuntimeInspector>().d2pQr3XMk7zMhqWE5dMM_3A[0].HiddenVariables = new VariableSet[0];
		gameObject2.GetComponent<RuntimeInspector>().d2pQr3XMk7zMhqWE5dMM_3A[0].ExposedVariables = new VariableSet[0];
		gameObject2.GetComponent<RuntimeInspector>().ConnectedHierarchy = gameObject.GetComponent<RuntimeHierarchy>();
		gameObject.GetComponent<RuntimeHierarchy>().ConnectedInspector = gameObject2.GetComponent<RuntimeInspector>();
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

	internal static GameObject smethod_4(string string_0)
	{
		return GameObject.Find(string_0);
	}

	internal static Transform smethod_5(GameObject gameObject_0)
	{
		return gameObject_0.transform;
	}

	internal static Vector3 smethod_6(Transform transform_0)
	{
		return transform_0.localPosition;
	}
}
