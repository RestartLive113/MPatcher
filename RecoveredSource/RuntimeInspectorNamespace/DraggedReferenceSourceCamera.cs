using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RuntimeInspectorNamespace;

[RequireComponent(typeof(Camera))]
public class DraggedReferenceSourceCamera : MonoBehaviour
{
	public delegate Object RaycastHitProcesserDelegate(RaycastHit hit);

	private Camera camera_0;

	[SerializeField]
	private UISkin Tk87AWktKBPxlXEouSMMrptEbcDu7sFaDRPwE_Y0IKcg;

	[SerializeField]
	private Canvas yeIePLvExcCTLy0RQoTsFFwSi9SAGI1d_S3bRfiRZDo2;

	[SerializeField]
	private float float_0 = 0.4f;

	[SerializeField]
	private LayerMask layerMask_0 = -1;

	[SerializeField]
	private float RMmw9JXA4YXHli2l2VXKMPs = float.MaxValue;

	private bool S8_1VCRt3ealpir_0024F1JI71U;

	private float float_1;

	private Vector2 eRcqKx4E68pdiNBMQvCVS0k;

	private Object Fhfav3Zyet8XLs3SVxDr9SI;

	private DraggedReferenceItem draggedReferenceItem_0;

	private PointerEventData cuXp3GZaS0_0024fUv5_0024zJCwtTY;

	public RaycastHitProcesserDelegate ProcessRaycastHit;

	private void LfaiZbNwRELPQoddbWEUzjg()
	{
		camera_0 = GetComponent<Camera>();
	}

	private void method_0()
	{
		RaycastHit hitInfo;
		if (cuXp3GZaS0_0024fUv5_0024zJCwtTY != null)
		{
			if (!smethod_0((Object)draggedReferenceItem_0) || !smethod_2(smethod_1((Component)draggedReferenceItem_0)))
			{
				cuXp3GZaS0_0024fUv5_0024zJCwtTY = null;
				return;
			}
			if (!smethod_3(0))
			{
				cuXp3GZaS0_0024fUv5_0024zJCwtTY.position = Input.mousePosition;
				ExecuteEvents.Execute(draggedReferenceItem_0.gameObject, cuXp3GZaS0_0024fUv5_0024zJCwtTY, ExecuteEvents.dragHandler);
				return;
			}
			ExecuteEvents.Execute(smethod_1((Component)draggedReferenceItem_0), cuXp3GZaS0_0024fUv5_0024zJCwtTY, smethod_4());
			if (smethod_6((Object)smethod_5(), (Object)null))
			{
				List<RaycastResult> list = new List<RaycastResult>();
				smethod_7(smethod_5(), cuXp3GZaS0_0024fUv5_0024zJCwtTY, list);
				for (int i = 0; i < list.Count && ExecuteEvents.ExecuteHierarchy(list[i].gameObject, cuXp3GZaS0_0024fUv5_0024zJCwtTY, ExecuteEvents.dropHandler) == null; i++)
				{
				}
			}
			cuXp3GZaS0_0024fUv5_0024zJCwtTY = null;
		}
		else if (S8_1VCRt3ealpir_0024F1JI71U)
		{
			if (Input.GetMouseButton(0))
			{
				if (((Vector2)Input.mousePosition - eRcqKx4E68pdiNBMQvCVS0k).sqrMagnitude >= 100f)
				{
					S8_1VCRt3ealpir_0024F1JI71U = false;
				}
				else
				{
					if (!(Time.realtimeSinceStartup - float_1 >= float_0))
					{
						return;
					}
					S8_1VCRt3ealpir_0024F1JI71U = false;
					if ((bool)Fhfav3Zyet8XLs3SVxDr9SI && EventSystem.current != null)
					{
						cuXp3GZaS0_0024fUv5_0024zJCwtTY = new PointerEventData(EventSystem.current)
						{
							pointerId = ((Input.touchCount > 0) ? Input.GetTouch(0).fingerId : (-1)),
							pressPosition = Input.mousePosition,
							position = Input.mousePosition,
							button = PointerEventData.InputButton.Left
						};
						draggedReferenceItem_0 = RuntimeInspectorUtils.CreateDraggedReferenceItem(Fhfav3Zyet8XLs3SVxDr9SI, cuXp3GZaS0_0024fUv5_0024zJCwtTY, Tk87AWktKBPxlXEouSMMrptEbcDu7sFaDRPwE_Y0IKcg, yeIePLvExcCTLy0RQoTsFFwSi9SAGI1d_S3bRfiRZDo2);
						if (draggedReferenceItem_0 == null)
						{
							S8_1VCRt3ealpir_0024F1JI71U = false;
							cuXp3GZaS0_0024fUv5_0024zJCwtTY = null;
						}
					}
				}
			}
			else if (Input.GetMouseButtonUp(0))
			{
				S8_1VCRt3ealpir_0024F1JI71U = false;
			}
		}
		else if (Input.GetMouseButtonDown(0) && (bool)EventSystem.current && !EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(camera_0.ScreenPointToRay(Input.mousePosition), out hitInfo, RMmw9JXA4YXHli2l2VXKMPs, layerMask_0))
		{
			Fhfav3Zyet8XLs3SVxDr9SI = ((ProcessRaycastHit != null) ? ProcessRaycastHit(hitInfo) : hitInfo.collider.gameObject);
			if ((bool)Fhfav3Zyet8XLs3SVxDr9SI)
			{
				S8_1VCRt3ealpir_0024F1JI71U = true;
				float_1 = Time.realtimeSinceStartup;
				eRcqKx4E68pdiNBMQvCVS0k = Input.mousePosition;
			}
		}
	}

	internal static bool smethod_0(Object object_0)
	{
		return object_0;
	}

	internal static GameObject smethod_1(Component component_0)
	{
		return component_0.gameObject;
	}

	internal static bool smethod_2(GameObject gameObject_0)
	{
		return gameObject_0.activeSelf;
	}

	internal static bool smethod_3(int int_0)
	{
		return Input.GetMouseButtonUp(int_0);
	}

	internal static ExecuteEvents.EventFunction<IEndDragHandler> smethod_4()
	{
		return ExecuteEvents.endDragHandler;
	}

	internal static EventSystem smethod_5()
	{
		return EventSystem.current;
	}

	internal static bool smethod_6(Object object_0, Object object_1)
	{
		return object_0 != object_1;
	}

	internal static void smethod_7(EventSystem eventSystem_0, PointerEventData pointerEventData_0, List<RaycastResult> list_0)
	{
		eventSystem_0.RaycastAll(pointerEventData_0, list_0);
	}
}
