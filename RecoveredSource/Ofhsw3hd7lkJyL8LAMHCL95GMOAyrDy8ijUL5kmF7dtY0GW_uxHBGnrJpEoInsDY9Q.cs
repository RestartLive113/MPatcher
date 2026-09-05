using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[HarmonyPatch("mousePosition")]
[HarmonyPatch(MethodType.Getter)]
[HarmonyPatch(typeof(Input))]
internal class Ofhsw3hd7lkJyL8LAMHCL95GMOAyrDy8ijUL5kmF7dtY0GW_uxHBGnrJpEoInsDY9Q
{
	private static GameObject AIj_0024ZC8Kw9P2uoo9ZIdZIu4;

	private static GraphicRaycaster bkjoozaisOoo6vgoq3OUQIk;

	private static EventSystem xUZn5Or94F09Mw2Ke17X4Ts;

	private static PointerEventData Y1AClZb4w_3VWc_0024l9tZtMeE_cLDv0syR1vwcUhVRdou2;

	internal static bool Gg155i91S6yyfnaswla_0024ldg = false;

	internal static Vector2 sC7TVlJaywRg_0024a_5dqujETk = new Vector2(0f, 0f);

	[HarmonyPostfix]
	public static void FeUAVwFbW6wGJJdNimZY9yI(ref Vector3 vector3_0)
	{
		if (Gg155i91S6yyfnaswla_0024ldg)
		{
			vector3_0 = new Vector3(sC7TVlJaywRg_0024a_5dqujETk.x, sC7TVlJaywRg_0024a_5dqujETk.y, vector3_0.z);
		}
	}

	public static void EfiM_0024Zfw3_00243I4_0024VjkC4_0024Y58()
	{
		if (smethod_0((Object)AIj_0024ZC8Kw9P2uoo9ZIdZIu4, (Object)null))
		{
			AIj_0024ZC8Kw9P2uoo9ZIdZIu4 = smethod_1(global::_003CModule_003E.smethod_29<string>(618767629u));
		}
		if (smethod_0((Object)bkjoozaisOoo6vgoq3OUQIk, (Object)null))
		{
			bkjoozaisOoo6vgoq3OUQIk = AIj_0024ZC8Kw9P2uoo9ZIdZIu4.GetComponent<GraphicRaycaster>();
		}
		if (smethod_0((Object)xUZn5Or94F09Mw2Ke17X4Ts, (Object)null))
		{
			xUZn5Or94F09Mw2Ke17X4Ts = AIj_0024ZC8Kw9P2uoo9ZIdZIu4.GetComponent<EventSystem>();
		}
		Y1AClZb4w_3VWc_0024l9tZtMeE_cLDv0syR1vwcUhVRdou2 = smethod_2(xUZn5Or94F09Mw2Ke17X4Ts);
		smethod_4(Y1AClZb4w_3VWc_0024l9tZtMeE_cLDv0syR1vwcUhVRdou2, (Vector2)smethod_3());
		List<RaycastResult> list = new List<RaycastResult>();
		smethod_5((BaseRaycaster)bkjoozaisOoo6vgoq3OUQIk, Y1AClZb4w_3VWc_0024l9tZtMeE_cLDv0syR1vwcUhVRdou2, list);
		smethod_6(Y1AClZb4w_3VWc_0024l9tZtMeE_cLDv0syR1vwcUhVRdou2, PointerEventData.InputButton.Left);
		foreach (RaycastResult item in list)
		{
			if (item.gameObject.GetComponent<WidgetController>() != null)
			{
				item.gameObject.GetComponent<WidgetController>().OnPointerClick(Y1AClZb4w_3VWc_0024l9tZtMeE_cLDv0syR1vwcUhVRdou2);
			}
		}
	}

	internal static bool smethod_0(Object object_0, Object object_1)
	{
		return object_0 == object_1;
	}

	internal static GameObject smethod_1(string string_0)
	{
		return GameObject.Find(string_0);
	}

	internal static PointerEventData smethod_2(EventSystem eventSystem_0)
	{
		return new PointerEventData(eventSystem_0);
	}

	internal static Vector3 smethod_3()
	{
		return Input.mousePosition;
	}

	internal static void smethod_4(PointerEventData pointerEventData_0, Vector2 vector2_0)
	{
		pointerEventData_0.position = vector2_0;
	}

	internal static void smethod_5(BaseRaycaster baseRaycaster_0, PointerEventData pointerEventData_0, List<RaycastResult> list_0)
	{
		baseRaycaster_0.Raycast(pointerEventData_0, list_0);
	}

	internal static void smethod_6(PointerEventData pointerEventData_0, PointerEventData.InputButton inputButton_0)
	{
		pointerEventData_0.button = inputButton_0;
	}
}
