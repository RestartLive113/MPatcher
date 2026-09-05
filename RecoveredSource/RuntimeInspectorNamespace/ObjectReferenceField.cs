using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RuntimeInspectorNamespace;

public class ObjectReferenceField : InspectorField, IEventSystemHandler, IDropHandler
{
	[SerializeField]
	private RectTransform xslOMz1i_0024_ihBLTZ_rN5dNHRo8OS3S7ymf72WkEy9FUL;

	[SerializeField]
	private PointerEventListener lhP1dtEaJBf0cSBprqcD3IA;

	[SerializeField]
	private PointerEventListener uQ1idDgnLWOJo9BYPS2DnMfwfw8B3jqfGEruwpXHI_Uy;

	private Image nCCCRyFKu5lmcqkS7SeuCwejxL90cFVfsP_EcHffFbcu;

	[SerializeField]
	protected Image background;

	[SerializeField]
	protected Text referenceNameText;

	public override void Initialize()
	{
		base.Initialize();
		lhP1dtEaJBf0cSBprqcD3IA.PointerClick += delegate
		{
			UnityEngine.Object[] references = smethod_19(base.BoundVariableType);
			ObjectReferencePicker.Instance.Skin = base.Inspector.Skin;
			ObjectReferencePicker.Instance.Show(OnReferenceChanged, base.BoundVariableType, references, (UnityEngine.Object)base.Value, base.Inspector.Canvas);
		};
		if (smethod_16((UnityEngine.Object)uQ1idDgnLWOJo9BYPS2DnMfwfw8B3jqfGEruwpXHI_Uy, (UnityEngine.Object)null))
		{
			uQ1idDgnLWOJo9BYPS2DnMfwfw8B3jqfGEruwpXHI_Uy.PointerClick += method_0;
			nCCCRyFKu5lmcqkS7SeuCwejxL90cFVfsP_EcHffFbcu = uQ1idDgnLWOJo9BYPS2DnMfwfw8B3jqfGEruwpXHI_Uy.GetComponent<Image>();
		}
	}

	public override bool SupportsType(Type type)
	{
		return smethod_18(smethod_17(typeof(UnityEngine.Object).TypeHandle), type);
	}

	private void OJJ_00242jxBnJXNk7Re1TLFqYZBcCYS8Pxphx_0024Jdh8VDFtv(PointerEventData pointerEventData_0)
	{
		UnityEngine.Object[] references = smethod_19(base.BoundVariableType);
		ObjectReferencePicker.Instance.Skin = base.Inspector.Skin;
		ObjectReferencePicker.Instance.Show(OnReferenceChanged, base.BoundVariableType, references, (UnityEngine.Object)base.Value, base.Inspector.Canvas);
	}

	private void method_0(PointerEventData pointerEventData_0)
	{
		if (base.Value != null && !smethod_20(base.Value, (object)null))
		{
			if (base.Value is Component)
			{
				base.Inspector.Inspect(smethod_21((Component)base.Value));
			}
			else
			{
				base.Inspector.Inspect(base.Value);
			}
		}
	}

	protected override void OnBound(MemberInfo variable)
	{
		base.OnBound(variable);
		OnReferenceChanged((UnityEngine.Object)base.Value);
	}

	protected virtual void OnReferenceChanged(UnityEngine.Object reference)
	{
		if (smethod_16((UnityEngine.Object)base.Value, reference))
		{
			base.Value = reference;
		}
		if (smethod_16((UnityEngine.Object)referenceNameText, (UnityEngine.Object)null))
		{
			smethod_22(referenceNameText, reference.GetNameWithType(base.BoundVariableType));
		}
		if (smethod_16((UnityEngine.Object)uQ1idDgnLWOJo9BYPS2DnMfwfw8B3jqfGEruwpXHI_Uy, (UnityEngine.Object)null))
		{
			smethod_23(smethod_21((Component)uQ1idDgnLWOJo9BYPS2DnMfwfw8B3jqfGEruwpXHI_Uy), base.Value != null && !smethod_20(base.Value, (object)null));
		}
		base.Inspector.RefreshDelayed();
	}

	public void OnDrop(PointerEventData eventData)
	{
		UnityEngine.Object assignableObjectFromDraggedReferenceItem = RuntimeInspectorUtils.GetAssignableObjectFromDraggedReferenceItem(eventData, base.BoundVariableType);
		if (smethod_16(assignableObjectFromDraggedReferenceItem, (UnityEngine.Object)null))
		{
			OnReferenceChanged(assignableObjectFromDraggedReferenceItem);
		}
	}

	protected override void OnSkinChanged()
	{
		base.OnSkinChanged();
		smethod_24((Graphic)background, base.Skin.InputFieldNormalBackgroundColor.Tint(0.075f));
		referenceNameText.SetSkinInputFieldText(base.Skin);
		smethod_25(referenceNameText, Mathf.Max(2, base.Skin.FontSize - 2));
		smethod_26(referenceNameText, base.Skin.FontSize);
		if (smethod_27((UnityEngine.Object)nCCCRyFKu5lmcqkS7SeuCwejxL90cFVfsP_EcHffFbcu))
		{
			smethod_24((Graphic)nCCCRyFKu5lmcqkS7SeuCwejxL90cFVfsP_EcHffFbcu, base.Skin.TextColor.Tint(0.1f));
			nCCCRyFKu5lmcqkS7SeuCwejxL90cFVfsP_EcHffFbcu.GetComponent<LayoutElement>().SetWidth(Mathf.Max(base.Skin.LineHeight - 8, 6));
		}
		if (smethod_27((UnityEngine.Object)xslOMz1i_0024_ihBLTZ_rN5dNHRo8OS3S7ymf72WkEy9FUL))
		{
			Vector2 vector2_ = new Vector2(base.Skin.LabelWidthPercentage, 0f);
			smethod_29(smethod_28((Graphic)variableNameMask), vector2_);
			smethod_29(xslOMz1i_0024_ihBLTZ_rN5dNHRo8OS3S7ymf72WkEy9FUL, vector2_);
		}
	}

	public override void Refresh()
	{
		object value = base.Value;
		base.Refresh();
		if (value != base.Value)
		{
			OnReferenceChanged((UnityEngine.Object)base.Value);
		}
	}

	internal static bool smethod_16(UnityEngine.Object object_0, UnityEngine.Object object_1)
	{
		return object_0 != object_1;
	}

	internal static Type smethod_17(RuntimeTypeHandle runtimeTypeHandle_0)
	{
		return Type.GetTypeFromHandle(runtimeTypeHandle_0);
	}

	internal static bool smethod_18(Type type_0, Type type_1)
	{
		return type_0.IsAssignableFrom(type_1);
	}

	internal static UnityEngine.Object[] smethod_19(Type type_0)
	{
		return Resources.FindObjectsOfTypeAll(type_0);
	}

	internal static bool smethod_20(object object_0, object object_1)
	{
		return object_0.Equals(object_1);
	}

	internal static GameObject smethod_21(Component component_0)
	{
		return component_0.gameObject;
	}

	internal static void smethod_22(Text text_0, string string_0)
	{
		text_0.text = string_0;
	}

	internal static void smethod_23(GameObject gameObject_0, bool bool_0)
	{
		gameObject_0.SetActive(bool_0);
	}

	internal static void smethod_24(Graphic graphic_0, Color color_0)
	{
		graphic_0.color = color_0;
	}

	internal static void smethod_25(Text text_0, int int_0)
	{
		text_0.resizeTextMinSize = int_0;
	}

	internal static void smethod_26(Text text_0, int int_0)
	{
		text_0.resizeTextMaxSize = int_0;
	}

	internal static bool smethod_27(UnityEngine.Object object_0)
	{
		return object_0;
	}

	internal static RectTransform smethod_28(Graphic graphic_0)
	{
		return graphic_0.rectTransform;
	}

	internal static void smethod_29(RectTransform rectTransform_0, Vector2 vector2_0)
	{
		rectTransform_0.anchorMin = vector2_0;
	}
}
