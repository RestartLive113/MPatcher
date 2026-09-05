using UnityEngine;

namespace MPatchrMain.Utils;

public static class RectTransformExtensions
{
	public static RectTransform ToRect(this Transform t)
	{
		return (RectTransform)t;
	}

	public static void SetLeft(this RectTransform rt, float left)
	{
		rt.offsetMin = new Vector2(left, smethod_0(rt).y);
	}

	public static void SetRight(this RectTransform rt, float right)
	{
		rt.offsetMax = new Vector2(0f - right, smethod_1(rt).y);
	}

	public static void SetTop(this RectTransform rt, float top)
	{
		rt.offsetMax = new Vector2(smethod_1(rt).x, 0f - top);
	}

	public static void SetBottom(this RectTransform rt, float bottom)
	{
		rt.offsetMin = new Vector2(smethod_0(rt).x, bottom);
	}

	internal static Vector2 smethod_0(RectTransform rectTransform_0)
	{
		return rectTransform_0.offsetMin;
	}

	internal static Vector2 smethod_1(RectTransform rectTransform_0)
	{
		return rectTransform_0.offsetMax;
	}
}
