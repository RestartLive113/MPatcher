using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RuntimeInspectorNamespace;

public class Tooltip : PopupBase
{
	public void SetContent(string tooltip, PointerEventData pointer)
	{
		smethod_15(label, tooltip);
		SetPointer(pointer);
	}

	protected override void DestroySelf()
	{
		smethod_17(smethod_16((Component)this), bool_0: false);
	}

	internal static void smethod_15(Text text_0, string string_0)
	{
		text_0.text = string_0;
	}

	internal static GameObject smethod_16(Component component_0)
	{
		return component_0.gameObject;
	}

	internal static void smethod_17(GameObject gameObject_0, bool bool_0)
	{
		gameObject_0.SetActive(bool_0);
	}
}
