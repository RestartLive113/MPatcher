using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace RuntimeInspectorNamespace;

public class NumberRangeField : NumberField
{
	[SerializeField]
	private BoundSlider jZIFr50VTsXC_0024VRvW02e2U4;

	public override void Initialize()
	{
		base.Initialize();
		BoundSlider boundSlider = jZIFr50VTsXC_0024VRvW02e2U4;
		boundSlider.OnValueChanged = (BoundSlider.OnValueChangedDelegate)smethod_23((Delegate)boundSlider.OnValueChanged, (Delegate)new BoundSlider.OnValueChangedDelegate(method_0));
	}

	public override bool CanBindTo(Type type, MemberInfo variable)
	{
		return variable?.HasAttribute<RangeAttribute>() ?? false;
	}

	protected override void OnBound(MemberInfo variable)
	{
		base.OnBound(variable);
		RangeAttribute attribute = variable.GetAttribute<RangeAttribute>();
		jZIFr50VTsXC_0024VRvW02e2U4.SetRange(Mathf.Max(attribute.min, numberHandler.MinValue), Mathf.Min(attribute.max, numberHandler.MaxValue));
		smethod_25(jZIFr50VTsXC_0024VRvW02e2U4.BackingField, base.BoundVariableType != smethod_24(typeof(float).TypeHandle) && base.BoundVariableType != smethod_24(typeof(double).TypeHandle) && base.BoundVariableType != smethod_24(typeof(decimal).TypeHandle));
	}

	protected override bool OnValueChanged(BoundInputField source, string input)
	{
		if (numberHandler.TryParse(input, out var value))
		{
			float num = numberHandler.ConvertToFloat(value);
			if (num >= smethod_26(jZIFr50VTsXC_0024VRvW02e2U4.BackingField) && num <= smethod_27(jZIFr50VTsXC_0024VRvW02e2U4.BackingField))
			{
				base.Value = value;
				return true;
			}
		}
		return false;
	}

	private void method_0(BoundSlider boundSlider_0, float float_0)
	{
		if (!smethod_28(input.BackingField))
		{
			base.Value = numberHandler.ConvertFromFloat(float_0);
			input.Text = smethod_29(base.Value);
			base.Inspector.RefreshDelayed();
		}
	}

	protected override void OnSkinChanged()
	{
		base.OnSkinChanged();
		jZIFr50VTsXC_0024VRvW02e2U4.Skin = base.Skin;
		float num = (1f - base.Skin.LabelWidthPercentage) / 3f;
		Vector2 vector2_ = new Vector2(base.Skin.LabelWidthPercentage, 0f);
		smethod_31(smethod_30((Graphic)variableNameMask), vector2_);
		smethod_31((RectTransform)smethod_32((Component)jZIFr50VTsXC_0024VRvW02e2U4), vector2_);
		((RectTransform)smethod_32((Component)jZIFr50VTsXC_0024VRvW02e2U4)).anchorMax = new Vector2(1f - num, 1f);
		((RectTransform)input.transform).anchorMin = new Vector2(1f - num, 0f);
	}

	public override void Refresh()
	{
		base.Refresh();
		jZIFr50VTsXC_0024VRvW02e2U4.Value = numberHandler.ConvertToFloat(base.Value);
	}

	internal static Delegate smethod_23(Delegate delegate_0, Delegate delegate_1)
	{
		return Delegate.Combine(delegate_0, delegate_1);
	}

	internal static Type smethod_24(RuntimeTypeHandle runtimeTypeHandle_0)
	{
		return Type.GetTypeFromHandle(runtimeTypeHandle_0);
	}

	internal static void smethod_25(Slider slider_0, bool bool_0)
	{
		slider_0.wholeNumbers = bool_0;
	}

	internal static float smethod_26(Slider slider_0)
	{
		return slider_0.minValue;
	}

	internal static float smethod_27(Slider slider_0)
	{
		return slider_0.maxValue;
	}

	internal static bool smethod_28(InputField inputField_0)
	{
		return inputField_0.isFocused;
	}

	internal static string smethod_29(object object_0)
	{
		return object_0.ToString();
	}

	internal static RectTransform smethod_30(Graphic graphic_0)
	{
		return graphic_0.rectTransform;
	}

	internal static void smethod_31(RectTransform rectTransform_0, Vector2 vector2_0)
	{
		rectTransform_0.anchorMin = vector2_0;
	}

	internal static Transform smethod_32(Component component_0)
	{
		return component_0.transform;
	}
}
