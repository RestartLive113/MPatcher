using UnityEngine;
using UnityEngine.EventSystems;

namespace RuntimeInspectorNamespace;

public class WindowDragHandler : MonoBehaviour, IEventSystemHandler, IDragHandler, IEndDragHandler, IBeginDragHandler
{
	private const int IQXM6cgds_00242x96tLOI3m263m4kJo4lN3G7JDh5Xyk_Sf = -98456;

	private RectTransform vHfn1ppWs5NVru2AA2jCOew;

	private int vOUl46QYoRjg8PBakr_0024nMK8 = -98456;

	private Vector2 vbAoH0TVGvW_T0XXdT5im6k;

	private void LfaiZbNwRELPQoddbWEUzjg()
	{
		vHfn1ppWs5NVru2AA2jCOew = (RectTransform)smethod_0((Component)this);
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		if (vOUl46QYoRjg8PBakr_0024nMK8 != -98456)
		{
			smethod_1(eventData, (GameObject)null);
			return;
		}
		vOUl46QYoRjg8PBakr_0024nMK8 = smethod_2(eventData);
		smethod_5(vHfn1ppWs5NVru2AA2jCOew, smethod_3(eventData), smethod_4(eventData), ref vbAoH0TVGvW_T0XXdT5im6k);
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (smethod_2(eventData) == vOUl46QYoRjg8PBakr_0024nMK8)
		{
			Vector2 vector2_ = default(Vector2);
			smethod_5(vHfn1ppWs5NVru2AA2jCOew, smethod_3(eventData), smethod_4(eventData), ref vector2_);
			RectTransform rectTransform_ = vHfn1ppWs5NVru2AA2jCOew;
			smethod_7(rectTransform_, smethod_6(rectTransform_) + (vector2_ - vbAoH0TVGvW_T0XXdT5im6k));
		}
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if (smethod_2(eventData) == vOUl46QYoRjg8PBakr_0024nMK8)
		{
			vOUl46QYoRjg8PBakr_0024nMK8 = -98456;
		}
	}

	internal static Transform smethod_0(Component component_0)
	{
		return component_0.transform;
	}

	internal static void smethod_1(PointerEventData pointerEventData_0, GameObject gameObject_0)
	{
		pointerEventData_0.pointerDrag = gameObject_0;
	}

	internal static int smethod_2(PointerEventData pointerEventData_0)
	{
		return pointerEventData_0.pointerId;
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
		return rectTransform_0.anchoredPosition;
	}

	internal static void smethod_7(RectTransform rectTransform_0, Vector2 vector2_0)
	{
		rectTransform_0.anchoredPosition = vector2_0;
	}
}
