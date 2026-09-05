using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RuntimeInspectorNamespace;

public class ColorField : InspectorField
{
	[SerializeField]
	private RectTransform rectTransform_0;

	[SerializeField]
	private PointerEventListener pointerEventListener_0;

	private Image wMMK4vbmSRsae6FhWNHHEv0;

	private bool UL6p35ezJxQtvtC2E9akJNg;

	public override void Initialize()
	{
		base.Initialize();
		wMMK4vbmSRsae6FhWNHHEv0 = pointerEventListener_0.GetComponent<Image>();
		pointerEventListener_0.PointerClick += mB_Rj6w3xr7uXJkwnYEb3Mw;
	}

	public override bool SupportsType(Type type)
	{
		if (type != smethod_16(typeof(Color).TypeHandle))
		{
			return type == smethod_16(typeof(Color32).TypeHandle);
		}
		return true;
	}

	protected override void OnBound(MemberInfo variable)
	{
		base.OnBound(variable);
		UL6p35ezJxQtvtC2E9akJNg = base.BoundVariableType == smethod_16(typeof(Color32).TypeHandle);
	}

	private void mB_Rj6w3xr7uXJkwnYEb3Mw(PointerEventData pointerEventData_0)
	{
		Color initialColor = (UL6p35ezJxQtvtC2E9akJNg ? ((Color)(Color32)base.Value) : ((Color)base.Value));
		ColorPicker.Instance.Skin = base.Inspector.Skin;
		ColorPicker.Instance.Show(HFkXxT0Buc6CseuYpD6bntk, initialColor, base.Inspector.Canvas);
	}

	private void HFkXxT0Buc6CseuYpD6bntk(Color32 color32_0)
	{
		smethod_17((Graphic)wMMK4vbmSRsae6FhWNHHEv0, (Color)color32_0);
		if (UL6p35ezJxQtvtC2E9akJNg)
		{
			base.Value = color32_0;
		}
		else
		{
			base.Value = (Color)color32_0;
		}
	}

	protected override void OnSkinChanged()
	{
		base.OnSkinChanged();
		Vector2 vector2_ = new Vector2(base.Skin.LabelWidthPercentage, 0f);
		smethod_19(smethod_18((Graphic)variableNameMask), vector2_);
		smethod_19(rectTransform_0, vector2_);
	}

	public override void Refresh()
	{
		base.Refresh();
		if (UL6p35ezJxQtvtC2E9akJNg)
		{
			smethod_17((Graphic)wMMK4vbmSRsae6FhWNHHEv0, (Color)(Color32)base.Value);
		}
		else
		{
			smethod_17((Graphic)wMMK4vbmSRsae6FhWNHHEv0, (Color)base.Value);
		}
	}

	internal static Type smethod_16(RuntimeTypeHandle runtimeTypeHandle_0)
	{
		return Type.GetTypeFromHandle(runtimeTypeHandle_0);
	}

	internal static void smethod_17(Graphic graphic_0, Color color_0)
	{
		graphic_0.color = color_0;
	}

	internal static RectTransform smethod_18(Graphic graphic_0)
	{
		return graphic_0.rectTransform;
	}

	internal static void smethod_19(RectTransform rectTransform_1, Vector2 vector2_0)
	{
		rectTransform_1.anchorMin = vector2_0;
	}
}
