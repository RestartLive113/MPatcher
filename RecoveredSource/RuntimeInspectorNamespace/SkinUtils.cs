using UnityEngine;
using UnityEngine.UI;

namespace RuntimeInspectorNamespace;

public static class SkinUtils
{
	public static void SetSkinText(this Text text, UISkin skin)
	{
		smethod_0((Graphic)text, skin.TextColor);
		smethod_1(text, skin.Font);
		smethod_2(text, skin.FontSize);
	}

	public static void SetSkinInputFieldText(this Text text, UISkin skin)
	{
		smethod_0((Graphic)text, skin.InputFieldTextColor);
		smethod_1(text, skin.Font);
		smethod_2(text, skin.FontSize);
	}

	public static void SetSkinButtonText(this Text text, UISkin skin)
	{
		smethod_0((Graphic)text, skin.ButtonTextColor);
		smethod_1(text, skin.Font);
		smethod_2(text, skin.FontSize);
	}

	public static void SetSkinButton(this Button button, UISkin skin)
	{
		smethod_0(smethod_3((Selectable)button), skin.ButtonBackgroundColor);
		button.GetComponentInChildren<Text>().SetSkinButtonText(skin);
	}

	public static void SetWidth(this LayoutElement layoutElement, float width)
	{
		smethod_4(layoutElement, width);
		smethod_5(layoutElement, width);
	}

	public static void SetHeight(this LayoutElement layoutElement, float height)
	{
		smethod_6(layoutElement, height);
		smethod_7(layoutElement, height);
	}

	public static void SetAnchorMinMaxInputField(this RectTransform inputField, RectTransform label, Vector2 anchorMin, Vector2 anchorMax)
	{
		smethod_8(inputField, anchorMin);
		smethod_9(inputField, anchorMax);
		smethod_8(label, anchorMin);
		label.anchorMax = new Vector2(anchorMin.x, anchorMax.y);
	}

	internal static void smethod_0(Graphic graphic_0, Color color_0)
	{
		graphic_0.color = color_0;
	}

	internal static void smethod_1(Text text_0, Font font_0)
	{
		text_0.font = font_0;
	}

	internal static void smethod_2(Text text_0, int int_0)
	{
		text_0.fontSize = int_0;
	}

	internal static Graphic smethod_3(Selectable selectable_0)
	{
		return selectable_0.targetGraphic;
	}

	internal static void smethod_4(LayoutElement layoutElement_0, float float_0)
	{
		layoutElement_0.minWidth = float_0;
	}

	internal static void smethod_5(LayoutElement layoutElement_0, float float_0)
	{
		layoutElement_0.preferredWidth = float_0;
	}

	internal static void smethod_6(LayoutElement layoutElement_0, float float_0)
	{
		layoutElement_0.minHeight = float_0;
	}

	internal static void smethod_7(LayoutElement layoutElement_0, float float_0)
	{
		layoutElement_0.preferredHeight = float_0;
	}

	internal static void smethod_8(RectTransform rectTransform_0, Vector2 vector2_0)
	{
		rectTransform_0.anchorMin = vector2_0;
	}

	internal static void smethod_9(RectTransform rectTransform_0, Vector2 vector2_0)
	{
		rectTransform_0.anchorMax = vector2_0;
	}
}
