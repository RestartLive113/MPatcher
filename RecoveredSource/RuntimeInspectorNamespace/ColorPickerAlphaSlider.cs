using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RuntimeInspectorNamespace;

public class ColorPickerAlphaSlider : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IDragHandler
{
	public delegate void OnValueChangedDelegate(float alpha);

	private RectTransform vHfn1ppWs5NVru2AA2jCOew;

	[SerializeField]
	private Image lQd4bbo4wFJTa6cAQUh_0024C_U;

	[SerializeField]
	private RectTransform y3hl036uzkfbZStnt21yzH4;

	private float s1ANaTLunWHUv6cYJ69Glko;

	public OnValueChangedDelegate OnValueChanged;

	public float Value
	{
		get
		{
			return s1ANaTLunWHUv6cYJ69Glko;
		}
		set
		{
			s1ANaTLunWHUv6cYJ69Glko = value;
			y3hl036uzkfbZStnt21yzH4.anchorMin = new Vector2(s1ANaTLunWHUv6cYJ69Glko, 0.5f);
			y3hl036uzkfbZStnt21yzH4.anchorMax = new Vector2(s1ANaTLunWHUv6cYJ69Glko, 0.5f);
		}
	}

	public Color Color
	{
		get
		{
			return smethod_0((Graphic)lQd4bbo4wFJTa6cAQUh_0024C_U);
		}
		set
		{
			value.a = 1f;
			smethod_1((Graphic)lQd4bbo4wFJTa6cAQUh_0024C_U, value);
		}
	}

	private void LfaiZbNwRELPQoddbWEUzjg()
	{
		vHfn1ppWs5NVru2AA2jCOew = (RectTransform)smethod_2((Component)this);
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		OnDrag(eventData);
	}

	public void OnDrag(PointerEventData eventData)
	{
		Vector2 vector2_ = default(Vector2);
		smethod_5(vHfn1ppWs5NVru2AA2jCOew, smethod_3(eventData), smethod_4(eventData), ref vector2_);
		Value = Mathf.Clamp01(vector2_.x / smethod_6(vHfn1ppWs5NVru2AA2jCOew).x);
		if (OnValueChanged != null)
		{
			OnValueChanged(s1ANaTLunWHUv6cYJ69Glko);
		}
	}

	internal static Color smethod_0(Graphic graphic_0)
	{
		return graphic_0.color;
	}

	internal static void smethod_1(Graphic graphic_0, Color color_0)
	{
		graphic_0.color = color_0;
	}

	internal static Transform smethod_2(Component component_0)
	{
		return component_0.transform;
	}

	internal static Vector2 smethod_3(PointerEventData pointerEventData_0)
	{
		return pointerEventData_0.position;
	}

	internal static Camera smethod_4(PointerEventData pointerEventData_0)
	{
		return pointerEventData_0.pressEventCamera;
	}

	internal static bool smethod_5(RectTransform rectTransform_0, Vector2 vector2_0, Camera camera_0, ref Vector2 vector2_1)
	{
		return RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform_0, vector2_0, camera_0, out vector2_1);
	}

	internal static Vector2 smethod_6(RectTransform rectTransform_0)
	{
		return rectTransform_0.sizeDelta;
	}
}
