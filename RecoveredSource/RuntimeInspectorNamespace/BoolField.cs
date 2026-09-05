using System;
using UnityEngine;
using UnityEngine.UI;

namespace RuntimeInspectorNamespace;

public class BoolField : InspectorField
{
	[SerializeField]
	private Image soGsrLhisgDNueHr1A5ZZ55yLHjleMpDe6D0idi698S9;

	[SerializeField]
	private Toggle lhP1dtEaJBf0cSBprqcD3IA;

	public override void Initialize()
	{
		base.Initialize();
		lhP1dtEaJBf0cSBprqcD3IA.onValueChanged.AddListener(NmnFeMoJhsIMhJ_LHKqb2aY);
	}

	public override bool SupportsType(Type type)
	{
		return type == smethod_16(typeof(bool).TypeHandle);
	}

	private void NmnFeMoJhsIMhJ_LHKqb2aY(bool bool_0)
	{
		base.Value = bool_0;
		base.Inspector.RefreshDelayed();
	}

	protected override void OnSkinChanged()
	{
		base.OnSkinChanged();
		smethod_17((Graphic)soGsrLhisgDNueHr1A5ZZ55yLHjleMpDe6D0idi698S9, base.Skin.InputFieldNormalBackgroundColor);
		smethod_17(lhP1dtEaJBf0cSBprqcD3IA.graphic, base.Skin.ToggleCheckmarkColor);
		Vector2 vector2_ = new Vector2(base.Skin.LabelWidthPercentage, 0f);
		smethod_19(smethod_18((Graphic)variableNameMask), vector2_);
		smethod_19((RectTransform)smethod_20((Component)lhP1dtEaJBf0cSBprqcD3IA), vector2_);
	}

	public override void Refresh()
	{
		base.Refresh();
		smethod_21(lhP1dtEaJBf0cSBprqcD3IA, (bool)base.Value);
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

	internal static void smethod_19(RectTransform rectTransform_0, Vector2 vector2_0)
	{
		rectTransform_0.anchorMin = vector2_0;
	}

	internal static Transform smethod_20(Component component_0)
	{
		return component_0.transform;
	}

	internal static void smethod_21(Toggle toggle_0, bool bool_0)
	{
		toggle_0.isOn = bool_0;
	}
}
