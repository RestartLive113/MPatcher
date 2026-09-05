using UnityEngine;
using UnityEngine.EventSystems;

namespace RuntimeInspectorNamespace;

public class TooltipArea : MonoBehaviour, IEventSystemHandler, IPointerEnterHandler, IPointerExitHandler
{
	private InspectorField CcjkXFPLb9dNGCgjjYhxfWM;

	public void Initialize(InspectorField drawer)
	{
		CcjkXFPLb9dNGCgjjYhxfWM = drawer;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (!smethod_0(eventData))
		{
			CcjkXFPLb9dNGCgjjYhxfWM.Inspector.u49tk_5o69hcBJaEsqKE4KM(CcjkXFPLb9dNGCgjjYhxfWM, eventData, bool_2: true);
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		CcjkXFPLb9dNGCgjjYhxfWM.Inspector.u49tk_5o69hcBJaEsqKE4KM(CcjkXFPLb9dNGCgjjYhxfWM, eventData, bool_2: false);
	}

	internal static bool smethod_0(PointerEventData pointerEventData_0)
	{
		return pointerEventData_0.dragging;
	}
}
