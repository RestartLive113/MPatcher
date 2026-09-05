using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RuntimeInspectorNamespace;

public class HierarchyField : RecycledListItem
{
	private enum vEHeAtcG7mnxnVN_K_JKa5qzL9slt9ikUxl8o6cJy_00247amvYsLQ27MfY6L4XJ8aAvi_00245bt6O2FSv83LBRCly7BeU
	{
		Collapsed,
		Expanded,
		ArrowHidden
	}

	private const float QEJ_00243UeG_QB5xUpW3aspcZtnrp3XjE9Wrk7q5xTKH8st = 0.57f;

	private const float float_0 = 35f;

	[SerializeField]
	private RectTransform Y6AcQ_gA6pnv3tp1jahZ10X9d3Kn2YWPyiZaAPnAXcfE;

	[SerializeField]
	private Text pO7UkOSEug5PyJ_cLPA5pMo;

	[SerializeField]
	private PointerEventListener xPz5nR_00249spe8xbvSDSh3uko;

	[SerializeField]
	private PointerEventListener Dio9gza_0024qoJVEKQZOKo2aiI;

	[SerializeField]
	private Image t4uHfbtZWoxMU6vX9SaY3fc;

	private RectTransform vHfn1ppWs5NVru2AA2jCOew;

	private Image slJt0vtJTZ_pZ4HFn1Pm0w0;

	private int tez8QKQVeFGVS4AMHMsbzyw;

	private UISkin E58c_5PzPLk6LleLXcBTp_0024M;

	private bool Q5KAiFtbXERFusj8Dg41uJo;

	private bool njyYlqszcucP5sFiLynJDb4;

	private vEHeAtcG7mnxnVN_K_JKa5qzL9slt9ikUxl8o6cJy_00247amvYsLQ27MfY6L4XJ8aAvi_00245bt6O2FSv83LBRCly7BeU vEHeAtcG7mnxnVN_K_JKa5qzL9slt9ikUxl8o6cJy_00247amvYsLQ27MfY6L4XJ8aAvi_00245bt6O2FSv83LBRCly7BeU_0;

	[CompilerGenerated]
	private float TWiPNCnDuU0KOPgUyOYuawqKlQuS3ftLnzBlkhIX8eym;

	[CompilerGenerated]
	private RuntimeHierarchy runtimeHierarchy_0;

	[CompilerGenerated]
	private HierarchyData TgZhQgwN79HBMQGhpcVJ__0024iLTjRjTtHMlrGliI4NqMr3;

	public UISkin Skin
	{
		get
		{
			return E58c_5PzPLk6LleLXcBTp_0024M;
		}
		set
		{
			if (smethod_0((Object)E58c_5PzPLk6LleLXcBTp_0024M, (Object)value) || tez8QKQVeFGVS4AMHMsbzyw != E58c_5PzPLk6LleLXcBTp_0024M.Version)
			{
				E58c_5PzPLk6LleLXcBTp_0024M = value;
				tez8QKQVeFGVS4AMHMsbzyw = E58c_5PzPLk6LleLXcBTp_0024M.Version;
				vHfn1ppWs5NVru2AA2jCOew.sizeDelta = new Vector2(0f, Skin.LineHeight);
				pO7UkOSEug5PyJ_cLPA5pMo.SetSkinText(Skin);
				t4uHfbtZWoxMU6vX9SaY3fc.color = Skin.ExpandArrowColor;
			}
		}
	}

	public bool IsSelected
	{
		get
		{
			return Q5KAiFtbXERFusj8Dg41uJo;
		}
		set
		{
			Q5KAiFtbXERFusj8Dg41uJo = value;
			Color color_;
			if (!Q5KAiFtbXERFusj8Dg41uJo)
			{
				smethod_1((Graphic)slJt0vtJTZ_pZ4HFn1Pm0w0, (Data.Depth == 0) ? Skin.BackgroundColor.Tint(0.075f) : Color.clear);
				color_ = Skin.TextColor;
			}
			else
			{
				smethod_1((Graphic)slJt0vtJTZ_pZ4HFn1Pm0w0, Skin.SelectedItemBackgroundColor);
				color_ = Skin.SelectedItemTextColor;
			}
			color_.a = (njyYlqszcucP5sFiLynJDb4 ? 1f : 0.57f);
			smethod_1((Graphic)pO7UkOSEug5PyJ_cLPA5pMo, color_);
		}
	}

	private bool IsActive
	{
		get
		{
			return njyYlqszcucP5sFiLynJDb4;
		}
		set
		{
			if (njyYlqszcucP5sFiLynJDb4 != value)
			{
				njyYlqszcucP5sFiLynJDb4 = value;
				Color color_ = smethod_2((Graphic)pO7UkOSEug5PyJ_cLPA5pMo);
				color_.a = (njyYlqszcucP5sFiLynJDb4 ? 1f : 0.57f);
				smethod_1((Graphic)pO7UkOSEug5PyJ_cLPA5pMo, color_);
			}
		}
	}

	private vEHeAtcG7mnxnVN_K_JKa5qzL9slt9ikUxl8o6cJy_00247amvYsLQ27MfY6L4XJ8aAvi_00245bt6O2FSv83LBRCly7BeU IsExpanded
	{
		get
		{
			return vEHeAtcG7mnxnVN_K_JKa5qzL9slt9ikUxl8o6cJy_00247amvYsLQ27MfY6L4XJ8aAvi_00245bt6O2FSv83LBRCly7BeU_0;
		}
		set
		{
			if (vEHeAtcG7mnxnVN_K_JKa5qzL9slt9ikUxl8o6cJy_00247amvYsLQ27MfY6L4XJ8aAvi_00245bt6O2FSv83LBRCly7BeU_0 != value)
			{
				vEHeAtcG7mnxnVN_K_JKa5qzL9slt9ikUxl8o6cJy_00247amvYsLQ27MfY6L4XJ8aAvi_00245bt6O2FSv83LBRCly7BeU_0 = value;
				if (vEHeAtcG7mnxnVN_K_JKa5qzL9slt9ikUxl8o6cJy_00247amvYsLQ27MfY6L4XJ8aAvi_00245bt6O2FSv83LBRCly7BeU_0 == vEHeAtcG7mnxnVN_K_JKa5qzL9slt9ikUxl8o6cJy_00247amvYsLQ27MfY6L4XJ8aAvi_00245bt6O2FSv83LBRCly7BeU.ArrowHidden)
				{
					smethod_4(smethod_3((Component)Dio9gza_0024qoJVEKQZOKo2aiI), bool_0: false);
					return;
				}
				smethod_4(smethod_3((Component)Dio9gza_0024qoJVEKQZOKo2aiI), bool_0: true);
				smethod_5((Graphic)t4uHfbtZWoxMU6vX9SaY3fc).localEulerAngles = ((vEHeAtcG7mnxnVN_K_JKa5qzL9slt9ikUxl8o6cJy_00247amvYsLQ27MfY6L4XJ8aAvi_00245bt6O2FSv83LBRCly7BeU_0 == vEHeAtcG7mnxnVN_K_JKa5qzL9slt9ikUxl8o6cJy_00247amvYsLQ27MfY6L4XJ8aAvi_00245bt6O2FSv83LBRCly7BeU.Expanded) ? new Vector3(0f, 0f, -90f) : Vector3.zero);
			}
		}
	}

	public float PreferredWidth
	{
		[CompilerGenerated]
		get
		{
			return TWiPNCnDuU0KOPgUyOYuawqKlQuS3ftLnzBlkhIX8eym;
		}
		[CompilerGenerated]
		private set
		{
			TWiPNCnDuU0KOPgUyOYuawqKlQuS3ftLnzBlkhIX8eym = value;
		}
	}

	public RuntimeHierarchy Hierarchy
	{
		[CompilerGenerated]
		get
		{
			return runtimeHierarchy_0;
		}
		[CompilerGenerated]
		private set
		{
			runtimeHierarchy_0 = value;
		}
	}

	public HierarchyData Data
	{
		[CompilerGenerated]
		get
		{
			return TgZhQgwN79HBMQGhpcVJ__0024iLTjRjTtHMlrGliI4NqMr3;
		}
		[CompilerGenerated]
		private set
		{
			TgZhQgwN79HBMQGhpcVJ__0024iLTjRjTtHMlrGliI4NqMr3 = value;
		}
	}

	public void Initialize(RuntimeHierarchy hierarchy)
	{
		Hierarchy = hierarchy;
		vHfn1ppWs5NVru2AA2jCOew = (RectTransform)smethod_6((Component)this);
		slJt0vtJTZ_pZ4HFn1Pm0w0 = xPz5nR_00249spe8xbvSDSh3uko.GetComponent<Image>();
		Dio9gza_0024qoJVEKQZOKo2aiI.PointerClick += delegate
		{
			PFrvbMv8T5Rfi01AFvGIeFrYnm8ZLM8EjRIBulr_WUmL();
		};
		xPz5nR_00249spe8xbvSDSh3uko.PointerClick += delegate
		{
			OnClick();
		};
		xPz5nR_00249spe8xbvSDSh3uko.PointerDown += delegate(PointerEventData pointerEventData_0)
		{
			Hierarchy.OnDrawerPointerEvent(this, pointerEventData_0, isPointerDown: true);
		};
		xPz5nR_00249spe8xbvSDSh3uko.PointerUp += fSwRpZip2aqw0czL0yBA9dk;
	}

	public void SetContent(HierarchyData data)
	{
		Data = data;
		Y6AcQ_gA6pnv3tp1jahZ10X9d3Kn2YWPyiZaAPnAXcfE.anchoredPosition = new Vector2(Skin.IndentAmount * data.Depth, 0f);
		slJt0vtJTZ_pZ4HFn1Pm0w0.sprite = ((data.Depth == 0) ? Hierarchy.SceneDrawerBackground : Hierarchy.TransformDrawerBackground);
		RefreshName();
	}

	private void PFrvbMv8T5Rfi01AFvGIeFrYnm8ZLM8EjRIBulr_WUmL()
	{
		Data.IsExpanded = !Data.IsExpanded;
	}

	public void Refresh()
	{
		IsActive = Data.IsActive;
		IsExpanded = ((!Data.CanExpand) ? vEHeAtcG7mnxnVN_K_JKa5qzL9slt9ikUxl8o6cJy_00247amvYsLQ27MfY6L4XJ8aAvi_00245bt6O2FSv83LBRCly7BeU.ArrowHidden : (Data.IsExpanded ? vEHeAtcG7mnxnVN_K_JKa5qzL9slt9ikUxl8o6cJy_00247amvYsLQ27MfY6L4XJ8aAvi_00245bt6O2FSv83LBRCly7BeU.Expanded : vEHeAtcG7mnxnVN_K_JKa5qzL9slt9ikUxl8o6cJy_00247amvYsLQ27MfY6L4XJ8aAvi_00245bt6O2FSv83LBRCly7BeU.Collapsed));
	}

	public void RefreshName()
	{
		smethod_7(pO7UkOSEug5PyJ_cLPA5pMo, Data.Name);
		if (Hierarchy.ShowHorizontalScrollbar)
		{
			smethod_8(smethod_5((Graphic)pO7UkOSEug5PyJ_cLPA5pMo));
			PreferredWidth = (float)(Data.Depth * E58c_5PzPLk6LleLXcBTp_0024M.IndentAmount) + 35f + smethod_9(smethod_5((Graphic)pO7UkOSEug5PyJ_cLPA5pMo)).x;
		}
	}

	private void ZBfln3AI3x8ZC_0024jznhwhwGE(PointerEventData pointerEventData_0)
	{
		Hierarchy.OnDrawerPointerEvent(this, pointerEventData_0, isPointerDown: true);
	}

	private void fSwRpZip2aqw0czL0yBA9dk(PointerEventData pointerEventData_0)
	{
		Hierarchy.OnDrawerPointerEvent(this, pointerEventData_0, isPointerDown: false);
	}

	[CompilerGenerated]
	private void yCfHXUSLyF4dbuxYSPBxXJuTBJ7Bcy1cI5GyPxdSwGRc(PointerEventData pointerEventData_0)
	{
		OnClick();
	}

	internal static bool smethod_0(Object object_1, Object object_2)
	{
		return object_1 != object_2;
	}

	internal static void smethod_1(Graphic graphic_0, Color color_0)
	{
		graphic_0.color = color_0;
	}

	internal static Color smethod_2(Graphic graphic_0)
	{
		return graphic_0.color;
	}

	internal static GameObject smethod_3(Component component_0)
	{
		return component_0.gameObject;
	}

	internal static void smethod_4(GameObject gameObject_0, bool bool_0)
	{
		gameObject_0.SetActive(bool_0);
	}

	internal static RectTransform smethod_5(Graphic graphic_0)
	{
		return graphic_0.rectTransform;
	}

	internal static Transform smethod_6(Component component_0)
	{
		return component_0.transform;
	}

	internal static void smethod_7(Text text_0, string string_0)
	{
		text_0.text = string_0;
	}

	internal static void smethod_8(RectTransform rectTransform_0)
	{
		LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform_0);
	}

	internal static Vector2 smethod_9(RectTransform rectTransform_0)
	{
		return rectTransform_0.sizeDelta;
	}
}
