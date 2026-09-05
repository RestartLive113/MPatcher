using HarmonyLib;
using UnityEngine;

[HarmonyPatch("LateUpdate")]
[HarmonyPatch(typeof(TagController))]
internal class q11OkRCNnJSc7B9oAm2iiDy_dxuAP_XQuqvU6kvp3YxcC3E9HoBBDUaHX4F1nUnZzg
{
	[HarmonyPostfix]
	internal static void FeUAVwFbW6wGJJdNimZY9yI(ref TagController __instance)
	{
		Color color_ = __instance.i0duGIJ6M7BXC7c2cbeyvXM1hCrcoNKNJzlqDrhwjoxM<Color>(global::_003CModule_003E.smethod_26<string>(2017173843u));
		color_.a = 1f;
		smethod_1(smethod_0((Renderer)__instance.i0duGIJ6M7BXC7c2cbeyvXM1hCrcoNKNJzlqDrhwjoxM<GameObject>(global::_003CModule_003E.smethod_27<string>(2924345508u)).GetComponent<MeshRenderer>()), color_);
		smethod_2(__instance.i0duGIJ6M7BXC7c2cbeyvXM1hCrcoNKNJzlqDrhwjoxM<GameObject>(global::_003CModule_003E.smethod_26<string>(734104091u)).GetComponent<TextMesh>(), color_);
		int num = ((Vector3.Distance(smethod_4(smethod_3((Component)__instance)), smethod_4(smethod_3((Component)__instance.i0duGIJ6M7BXC7c2cbeyvXM1hCrcoNKNJzlqDrhwjoxM<RideCameraController>(global::_003CModule_003E.smethod_29<string>(119146129u))))) < 100f) ? 1 : 5);
		smethod_3((Component)__instance).localScale = new Vector3(num, num, 1f);
	}

	internal static Material smethod_0(Renderer renderer_0)
	{
		return renderer_0.material;
	}

	internal static void smethod_1(Material material_0, Color color_0)
	{
		material_0.color = color_0;
	}

	internal static void smethod_2(TextMesh textMesh_0, Color color_0)
	{
		textMesh_0.color = color_0;
	}

	internal static Transform smethod_3(Component component_0)
	{
		return component_0.transform;
	}

	internal static Vector3 smethod_4(Transform transform_0)
	{
		return transform_0.position;
	}
}
