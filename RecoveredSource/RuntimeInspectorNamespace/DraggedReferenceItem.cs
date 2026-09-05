using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RuntimeInspectorNamespace;

public class DraggedReferenceItem : PopupBase, IEventSystemHandler, IDragHandler, IEndDragHandler
{
	private Object NfNLpq6TEVx7X05RbDEMYkc;

	public Object Reference => NfNLpq6TEVx7X05RbDEMYkc;

	public void SetContent(Object reference, PointerEventData draggingPointer)
	{
		NfNLpq6TEVx7X05RbDEMYkc = reference;
		smethod_15(label, reference.GetNameWithType());
		smethod_17(draggingPointer, smethod_16((Component)this));
		smethod_18(draggingPointer, bool_0: true);
		SetPointer(draggingPointer);
	}

	protected override void DestroySelf()
	{
		RuntimeInspectorUtils.PoolDraggedReferenceItem(this);
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (smethod_19(eventData) == smethod_19(pointer))
		{
			RepositionSelf();
		}
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		RuntimeInspectorUtils.PoolDraggedReferenceItem(this);
	}

	internal static void smethod_15(Text text_0, string string_0)
	{
		text_0.text = string_0;
	}

	internal static GameObject smethod_16(Component component_0)
	{
		return component_0.gameObject;
	}

	internal static void smethod_17(PointerEventData pointerEventData_0, GameObject gameObject_0)
	{
		pointerEventData_0.pointerDrag = gameObject_0;
	}

	internal static void smethod_18(PointerEventData pointerEventData_0, bool bool_0)
	{
		pointerEventData_0.dragging = bool_0;
	}

	internal static int smethod_19(PointerEventData pointerEventData_0)
	{
		return pointerEventData_0.pointerId;
	}
}
