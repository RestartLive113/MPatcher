using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace RuntimeInspectorNamespace;

public class HierarchyDragDropListener : MonoBehaviour, IEventSystemHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
	private const float wGkmFAtLH5YfnyarUxC9_00249V_0024EpYlXyYiSREv_0024xMO_00245d4 = 5f;

	[SerializeField]
	private float ZQLdWMD6qxHN6dTpZpBdtqSt_H4sL_0024TA1k3Bh213P6ry = 5f;

	[SerializeField]
	private float XJXpaxSyv2ca11jMYbTI4is = 75f;

	private float dtGdBd6coFiXq_0024Ocpqse08lf8nUN_crYIns5Typ8XV8r;

	[SerializeField]
	private float float_0 = 75f;

	[SerializeField]
	private bool IArxVwSExBZM6WABkayi7o2IdwSnlTkLsk7tAkkT_00247yI;

	[SerializeField]
	private bool bool_0;

	[Header("Internal Variables")]
	[SerializeField]
	private RuntimeHierarchy ISJ9K8MFSi7GuATd9qxI5yk;

	[SerializeField]
	private RectTransform nTE6_0024hOWf_0024LE_vr4krytynM;

	[SerializeField]
	private Image oIZUGSjfs2oEI4q2Lqdd8MeZq9_SLspKdidh1l7SareY;

	private Canvas AIj_0024ZC8Kw9P2uoo9ZIdZIu4;

	private RectTransform vHfn1ppWs5NVru2AA2jCOew;

	private float C7DN6F3F3AJErPAdiQePOng;

	private PointerEventData pointerEventData_0;

	private Camera Iwb6gPmOAOuGMV9lQMfy_Dc;

	private float QvOSYMnXH7WTDSLM1zJMUBY;

	private float float_1;

	private void sp_GCK595YHY1vrEPNGiSrQ()
	{
		vHfn1ppWs5NVru2AA2jCOew = (RectTransform)smethod_0((Component)this);
		AIj_0024ZC8Kw9P2uoo9ZIdZIu4 = ISJ9K8MFSi7GuATd9qxI5yk.GetComponentInParent<Canvas>();
		dtGdBd6coFiXq_0024Ocpqse08lf8nUN_crYIns5Typ8XV8r = 1f / XJXpaxSyv2ca11jMYbTI4is;
	}

	private void method_0()
	{
		C7DN6F3F3AJErPAdiQePOng = 0f;
	}

	private void method_1()
	{
		if (pointerEventData_0 == null)
		{
			return;
		}
		float_1 -= smethod_1();
		if (float_1 <= 0f)
		{
			float_1 = 5f;
			if (!pointerEventData_0.IsPointerValid())
			{
				pointerEventData_0 = null;
				return;
			}
		}
		Vector2 vector2_ = default(Vector2);
		if (!smethod_3(vHfn1ppWs5NVru2AA2jCOew, smethod_2(pointerEventData_0), Iwb6gPmOAOuGMV9lQMfy_Dc, ref vector2_) || vector2_.y == QvOSYMnXH7WTDSLM1zJMUBY)
		{
			return;
		}
		QvOSYMnXH7WTDSLM1zJMUBY = 0f - vector2_.y;
		if (C7DN6F3F3AJErPAdiQePOng <= 0f)
		{
			C7DN6F3F3AJErPAdiQePOng = smethod_4(vHfn1ppWs5NVru2AA2jCOew).height;
		}
		float num = 0f;
		float qvOSYMnXH7WTDSLM1zJMUBY = QvOSYMnXH7WTDSLM1zJMUBY;
		if (QvOSYMnXH7WTDSLM1zJMUBY >= XJXpaxSyv2ca11jMYbTI4is)
		{
			if (QvOSYMnXH7WTDSLM1zJMUBY > C7DN6F3F3AJErPAdiQePOng - XJXpaxSyv2ca11jMYbTI4is)
			{
				num = (C7DN6F3F3AJErPAdiQePOng - XJXpaxSyv2ca11jMYbTI4is - qvOSYMnXH7WTDSLM1zJMUBY) * dtGdBd6coFiXq_0024Ocpqse08lf8nUN_crYIns5Typ8XV8r;
			}
		}
		else
		{
			num = (XJXpaxSyv2ca11jMYbTI4is - QvOSYMnXH7WTDSLM1zJMUBY) * dtGdBd6coFiXq_0024Ocpqse08lf8nUN_crYIns5Typ8XV8r;
		}
		float num2 = QvOSYMnXH7WTDSLM1zJMUBY + nTE6_0024hOWf_0024LE_vr4krytynM.anchoredPosition.y;
		if (num2 >= 0f)
		{
			if (num2 < (float)(ISJ9K8MFSi7GuATd9qxI5yk.ItemCount * ISJ9K8MFSi7GuATd9qxI5yk.Skin.LineHeight))
			{
				if (!oIZUGSjfs2oEI4q2Lqdd8MeZq9_SLspKdidh1l7SareY.gameObject.activeSelf)
				{
					oIZUGSjfs2oEI4q2Lqdd8MeZq9_SLspKdidh1l7SareY.rectTransform.SetAsLastSibling();
					oIZUGSjfs2oEI4q2Lqdd8MeZq9_SLspKdidh1l7SareY.gameObject.SetActive(value: true);
				}
				float num3 = num2 % (float)ISJ9K8MFSi7GuATd9qxI5yk.Skin.LineHeight;
				float num4 = 0f - num2 + num3;
				if (num3 >= ZQLdWMD6qxHN6dTpZpBdtqSt_H4sL_0024TA1k3Bh213P6ry)
				{
					if (num3 <= (float)ISJ9K8MFSi7GuATd9qxI5yk.Skin.LineHeight - ZQLdWMD6qxHN6dTpZpBdtqSt_H4sL_0024TA1k3Bh213P6ry)
					{
						oIZUGSjfs2oEI4q2Lqdd8MeZq9_SLspKdidh1l7SareY.rectTransform.anchoredPosition = new Vector2(0f, num4);
						oIZUGSjfs2oEI4q2Lqdd8MeZq9_SLspKdidh1l7SareY.rectTransform.sizeDelta = new Vector2(20f, ISJ9K8MFSi7GuATd9qxI5yk.Skin.LineHeight);
					}
					else
					{
						oIZUGSjfs2oEI4q2Lqdd8MeZq9_SLspKdidh1l7SareY.rectTransform.anchoredPosition = new Vector2(0f, num4 - (float)ISJ9K8MFSi7GuATd9qxI5yk.Skin.LineHeight + 2f);
						oIZUGSjfs2oEI4q2Lqdd8MeZq9_SLspKdidh1l7SareY.rectTransform.sizeDelta = new Vector2(20f, 4f);
					}
				}
				else
				{
					oIZUGSjfs2oEI4q2Lqdd8MeZq9_SLspKdidh1l7SareY.rectTransform.anchoredPosition = new Vector2(0f, num4 + 2f);
					oIZUGSjfs2oEI4q2Lqdd8MeZq9_SLspKdidh1l7SareY.rectTransform.sizeDelta = new Vector2(20f, 4f);
				}
			}
			else if (oIZUGSjfs2oEI4q2Lqdd8MeZq9_SLspKdidh1l7SareY.gameObject.activeSelf)
			{
				oIZUGSjfs2oEI4q2Lqdd8MeZq9_SLspKdidh1l7SareY.gameObject.SetActive(value: false);
			}
			ISJ9K8MFSi7GuATd9qxI5yk.AutoScrollSpeed = num * float_0;
		}
		else
		{
			if (oIZUGSjfs2oEI4q2Lqdd8MeZq9_SLspKdidh1l7SareY.gameObject.activeSelf)
			{
				oIZUGSjfs2oEI4q2Lqdd8MeZq9_SLspKdidh1l7SareY.gameObject.SetActive(value: false);
			}
			ISJ9K8MFSi7GuATd9qxI5yk.AutoScrollSpeed = 0f;
		}
	}

	void IDropHandler.OnDrop(PointerEventData eventData)
	{
		smethod_5((IPointerExitHandler)this, eventData);
		if (!ISJ9K8MFSi7GuATd9qxI5yk.CanReorganizeItems || ISJ9K8MFSi7GuATd9qxI5yk.IsInSearchMode)
		{
			return;
		}
		Transform transform = RuntimeInspectorUtils.GetAssignableObjectFromDraggedReferenceItem(eventData, smethod_6(typeof(Transform).TypeHandle)) as Transform;
		if (!smethod_7((UnityEngine.Object)transform))
		{
			return;
		}
		int num = -1;
		bool flag = false;
		float num2 = QvOSYMnXH7WTDSLM1zJMUBY + smethod_8(nTE6_0024hOWf_0024LE_vr4krytynM).y;
		int num3 = (int)num2 / ISJ9K8MFSi7GuATd9qxI5yk.Skin.LineHeight;
		HierarchyData hierarchyData = ISJ9K8MFSi7GuATd9qxI5yk.method_1(num3);
		if (hierarchyData != null)
		{
			float num4 = num2 % (float)ISJ9K8MFSi7GuATd9qxI5yk.Skin.LineHeight;
			int num5 = ((num4 < ZQLdWMD6qxHN6dTpZpBdtqSt_H4sL_0024TA1k3Bh213P6ry) ? (-1) : ((!(num4 <= (float)ISJ9K8MFSi7GuATd9qxI5yk.Skin.LineHeight - ZQLdWMD6qxHN6dTpZpBdtqSt_H4sL_0024TA1k3Bh213P6ry)) ? 1 : 0));
			if (num5 != 0 && !(hierarchyData is HierarchyDataTransform))
			{
				if (num5 < 0 && num3 > 0)
				{
					HierarchyData hierarchyData2 = ISJ9K8MFSi7GuATd9qxI5yk.method_1(num3 - 1);
					if (hierarchyData2 != null)
					{
						hierarchyData = hierarchyData2;
						num5 = 1;
					}
				}
				else if (num5 > 0 && num3 < ISJ9K8MFSi7GuATd9qxI5yk.ItemCount - 1)
				{
					HierarchyData hierarchyData3 = ISJ9K8MFSi7GuATd9qxI5yk.method_1(num3 + 1);
					if (hierarchyData3 != null && hierarchyData3 is HierarchyDataTransform)
					{
						hierarchyData = hierarchyData3;
						num5 = -1;
					}
				}
			}
			HierarchyDataRoot hierarchyDataRoot = null;
			if (hierarchyData is HierarchyDataTransform)
			{
				Transform transform2 = ((HierarchyDataTransform)hierarchyData).BoundTransform;
				if (!smethod_7((UnityEngine.Object)transform2) || smethod_10((UnityEngine.Object)transform, (UnityEngine.Object)transform2))
				{
					return;
				}
				if (num5 != 0)
				{
					if (num5 > 0 && hierarchyData.Height > 1)
					{
						num = 0;
					}
					else if (hierarchyData.Depth == 1 && hierarchyData.Root is HierarchyDataRootPseudoScene)
					{
						num = ((num5 >= 0) ? (((HierarchyDataRootPseudoScene)hierarchyData.Root).IndexOf(transform2) + 1) : ((HierarchyDataRootPseudoScene)hierarchyData.Root).IndexOf(transform2));
						transform2 = null;
					}
					else
					{
						num = ((num5 < 0) ? smethod_12(transform2) : (smethod_12(transform2) + 1));
						transform2 = smethod_9(transform2);
						if (smethod_10((UnityEngine.Object)transform2, (UnityEngine.Object)smethod_9(transform)) && (smethod_7((UnityEngine.Object)transform2) || (hierarchyData.Root is HierarchyDataRootScene && ((HierarchyDataRootScene)hierarchyData.Root).Scene == smethod_14(smethod_13((Component)transform)))) && num > smethod_12(transform))
						{
							num--;
						}
					}
				}
				if (smethod_7((UnityEngine.Object)transform2))
				{
					if (IArxVwSExBZM6WABkayi7o2IdwSnlTkLsk7tAkkT_00247yI)
					{
						Transform transform3 = transform2;
						while (smethod_16((UnityEngine.Object)smethod_9(transform3), (UnityEngine.Object)null) && smethod_16((UnityEngine.Object)smethod_9(transform3), (UnityEngine.Object)transform))
						{
							transform3 = smethod_9(transform3);
						}
						if (smethod_10((UnityEngine.Object)smethod_9(transform3), (UnityEngine.Object)transform))
						{
							if (smethod_10((UnityEngine.Object)smethod_9(transform), (UnityEngine.Object)null) && hierarchyData.Root is HierarchyDataRootPseudoScene)
							{
								if (!bool_0)
								{
									return;
								}
								HierarchyDataRootPseudoScene obj = (HierarchyDataRootPseudoScene)hierarchyData.Root;
								obj.InsertChild(obj.IndexOf(transform2), transform3);
								obj.RemoveChild(transform2);
							}
							int int_ = smethod_12(transform);
							smethod_11(transform3, smethod_9(transform), bool_1: true);
							smethod_17(transform3, int_);
							flag = true;
						}
					}
					else if (smethod_15(transform2, transform))
					{
						return;
					}
					smethod_11(transform, transform2, bool_1: true);
				}
				else
				{
					hierarchyDataRoot = hierarchyData.Root;
				}
			}
			else
			{
				hierarchyDataRoot = (HierarchyDataRoot)hierarchyData;
			}
			if (hierarchyDataRoot != null)
			{
				if (!(hierarchyDataRoot is HierarchyDataRootPseudoScene))
				{
					if (hierarchyDataRoot is HierarchyDataRootScene)
					{
						if (smethod_16((UnityEngine.Object)smethod_9(transform), (UnityEngine.Object)null))
						{
							smethod_11(transform, (Transform)null, bool_1: true);
						}
						Scene scene = ((HierarchyDataRootScene)hierarchyDataRoot).Scene;
						if (smethod_14(smethod_13((Component)transform)) != scene)
						{
							smethod_18(smethod_13((Component)transform), scene);
						}
						if (num < 0)
						{
							num = scene.rootCount + 1;
							flag = true;
						}
					}
				}
				else
				{
					if (!bool_0)
					{
						return;
					}
					if (num >= 0)
					{
						((HierarchyDataRootPseudoScene)hierarchyDataRoot).InsertChild(num, transform);
						num = -1;
						hierarchyData = hierarchyDataRoot;
					}
					else
					{
						((HierarchyDataRootPseudoScene)hierarchyDataRoot).AddChild(transform);
					}
				}
			}
			if (num >= 0)
			{
				transform.SetSiblingIndex(num);
			}
		}
		else
		{
			if (smethod_10((UnityEngine.Object)smethod_9(transform), (UnityEngine.Object)null))
			{
				return;
			}
			smethod_11(transform, (Transform)null, bool_1: true);
			flag = true;
		}
		if (!flag && (num >= 0 || hierarchyData.IsExpanded))
		{
			ISJ9K8MFSi7GuATd9qxI5yk.Refresh();
		}
		else
		{
			ISJ9K8MFSi7GuATd9qxI5yk.Select(transform, forceSelection: true);
		}
	}

	void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
	{
		if (smethod_19(eventData) && ISJ9K8MFSi7GuATd9qxI5yk.CanReorganizeItems && !ISJ9K8MFSi7GuATd9qxI5yk.IsInSearchMode && smethod_7(RuntimeInspectorUtils.GetAssignableObjectFromDraggedReferenceItem(eventData, smethod_6(typeof(Transform).TypeHandle))))
		{
			pointerEventData_0 = eventData;
			QvOSYMnXH7WTDSLM1zJMUBY = -1f;
			float_1 = 5f;
			if (smethod_20(AIj_0024ZC8Kw9P2uoo9ZIdZIu4) != RenderMode.ScreenSpaceOverlay && (smethod_20(AIj_0024ZC8Kw9P2uoo9ZIdZIu4) != RenderMode.ScreenSpaceCamera || !smethod_10((UnityEngine.Object)smethod_21(AIj_0024ZC8Kw9P2uoo9ZIdZIu4), (UnityEngine.Object)null)))
			{
				Iwb6gPmOAOuGMV9lQMfy_Dc = (smethod_7((UnityEngine.Object)smethod_21(AIj_0024ZC8Kw9P2uoo9ZIdZIu4)) ? smethod_21(AIj_0024ZC8Kw9P2uoo9ZIdZIu4) : smethod_22());
			}
			else
			{
				Iwb6gPmOAOuGMV9lQMfy_Dc = null;
			}
			method_1();
		}
	}

	void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
	{
		pointerEventData_0 = null;
		Iwb6gPmOAOuGMV9lQMfy_Dc = null;
		if (smethod_23(smethod_13((Component)oIZUGSjfs2oEI4q2Lqdd8MeZq9_SLspKdidh1l7SareY)))
		{
			smethod_24(smethod_13((Component)oIZUGSjfs2oEI4q2Lqdd8MeZq9_SLspKdidh1l7SareY), bool_1: false);
		}
		ISJ9K8MFSi7GuATd9qxI5yk.AutoScrollSpeed = 0f;
	}

	internal static Transform smethod_0(Component component_0)
	{
		return component_0.transform;
	}

	internal static float smethod_1()
	{
		return Time.unscaledDeltaTime;
	}

	internal static Vector2 smethod_2(PointerEventData pointerEventData_1)
	{
		return pointerEventData_1.position;
	}

	internal static bool smethod_3(RectTransform rectTransform_0, Vector2 vector2_0, Camera camera_0, ref Vector2 vector2_1)
	{
		return RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform_0, vector2_0, camera_0, out vector2_1);
	}

	internal static Rect smethod_4(RectTransform rectTransform_0)
	{
		return rectTransform_0.rect;
	}

	internal static void smethod_5(IPointerExitHandler ipointerExitHandler_0, PointerEventData pointerEventData_1)
	{
		ipointerExitHandler_0.OnPointerExit(pointerEventData_1);
	}

	internal static Type smethod_6(RuntimeTypeHandle runtimeTypeHandle_0)
	{
		return Type.GetTypeFromHandle(runtimeTypeHandle_0);
	}

	internal static bool smethod_7(UnityEngine.Object object_0)
	{
		return object_0;
	}

	internal static Vector2 smethod_8(RectTransform rectTransform_0)
	{
		return rectTransform_0.anchoredPosition;
	}

	internal static Transform smethod_9(Transform transform_0)
	{
		return transform_0.parent;
	}

	internal static bool smethod_10(UnityEngine.Object object_0, UnityEngine.Object object_1)
	{
		return object_0 == object_1;
	}

	internal static void smethod_11(Transform transform_0, Transform transform_1, bool bool_1)
	{
		transform_0.SetParent(transform_1, bool_1);
	}

	internal static int smethod_12(Transform transform_0)
	{
		return transform_0.GetSiblingIndex();
	}

	internal static GameObject smethod_13(Component component_0)
	{
		return component_0.gameObject;
	}

	internal static Scene smethod_14(GameObject gameObject_0)
	{
		return gameObject_0.scene;
	}

	internal static bool smethod_15(Transform transform_0, Transform transform_1)
	{
		return transform_0.IsChildOf(transform_1);
	}

	internal static bool smethod_16(UnityEngine.Object object_0, UnityEngine.Object object_1)
	{
		return object_0 != object_1;
	}

	internal static void smethod_17(Transform transform_0, int int_0)
	{
		transform_0.SetSiblingIndex(int_0);
	}

	internal static void smethod_18(GameObject gameObject_0, Scene scene_0)
	{
		SceneManager.MoveGameObjectToScene(gameObject_0, scene_0);
	}

	internal static bool smethod_19(PointerEventData pointerEventData_1)
	{
		return pointerEventData_1.dragging;
	}

	internal static RenderMode smethod_20(Canvas canvas_0)
	{
		return canvas_0.renderMode;
	}

	internal static Camera smethod_21(Canvas canvas_0)
	{
		return canvas_0.worldCamera;
	}

	internal static Camera smethod_22()
	{
		return Camera.main;
	}

	internal static bool smethod_23(GameObject gameObject_0)
	{
		return gameObject_0.activeSelf;
	}

	internal static void smethod_24(GameObject gameObject_0, bool bool_1)
	{
		gameObject_0.SetActive(bool_1);
	}
}
