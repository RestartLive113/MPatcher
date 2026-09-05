using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace RuntimeInspectorNamespace;

public class StringField : InspectorField
{
	public enum Mode
	{
		OnValueChange,
		OnSubmit
	}

	[SerializeField]
	private BoundInputField lhP1dtEaJBf0cSBprqcD3IA;

	private Mode mode_0;

	private int z11gaS_pIdtTSK_HS6YDXMo = 1;

	public Mode SetterMode
	{
		get
		{
			return mode_0;
		}
		set
		{
			mode_0 = value;
			lhP1dtEaJBf0cSBprqcD3IA.CacheTextOnValueChange = mode_0 == Mode.OnValueChange;
		}
	}

	protected override float HeightMultiplier => z11gaS_pIdtTSK_HS6YDXMo;

	public override void Initialize()
	{
		base.Initialize();
		lhP1dtEaJBf0cSBprqcD3IA.Initialize();
		BoundInputField boundInputField = lhP1dtEaJBf0cSBprqcD3IA;
		boundInputField.OnValueChanged = (BoundInputField.OnValueChangedDelegate)smethod_16((Delegate)boundInputField.OnValueChanged, (Delegate)new BoundInputField.OnValueChangedDelegate(NmnFeMoJhsIMhJ_LHKqb2aY));
		BoundInputField boundInputField2 = lhP1dtEaJBf0cSBprqcD3IA;
		boundInputField2.OnValueSubmitted = (BoundInputField.OnValueChangedDelegate)smethod_16((Delegate)boundInputField2.OnValueSubmitted, (Delegate)(BoundInputField.OnValueChangedDelegate)delegate(BoundInputField boundInputField_0, string string_0)
		{
			if (mode_0 == Mode.OnSubmit)
			{
				base.Value = string_0;
			}
			base.Inspector.RefreshDelayed();
			return true;
		});
		lhP1dtEaJBf0cSBprqcD3IA.DefaultEmptyValue = string.Empty;
	}

	public override bool SupportsType(Type type)
	{
		return type == smethod_17(typeof(string).TypeHandle);
	}

	protected override void OnBound(MemberInfo variable)
	{
		base.OnBound(variable);
		int num = z11gaS_pIdtTSK_HS6YDXMo;
		if (variable == null)
		{
			z11gaS_pIdtTSK_HS6YDXMo = 1;
		}
		else
		{
			MultilineAttribute attribute = variable.GetAttribute<MultilineAttribute>();
			if (attribute != null)
			{
				z11gaS_pIdtTSK_HS6YDXMo = Mathf.Max(1, attribute.lines);
			}
			else if (variable.HasAttribute<TextAreaAttribute>())
			{
				z11gaS_pIdtTSK_HS6YDXMo = 3;
			}
			else
			{
				z11gaS_pIdtTSK_HS6YDXMo = 1;
			}
		}
		if (num != z11gaS_pIdtTSK_HS6YDXMo)
		{
			smethod_18(lhP1dtEaJBf0cSBprqcD3IA.BackingField, (z11gaS_pIdtTSK_HS6YDXMo > 1) ? InputField.LineType.MultiLineNewline : InputField.LineType.SingleLine);
			smethod_20(smethod_19(lhP1dtEaJBf0cSBprqcD3IA.BackingField), (z11gaS_pIdtTSK_HS6YDXMo <= 1) ? TextAnchor.MiddleLeft : TextAnchor.UpperLeft);
			OnSkinChanged();
		}
	}

	private bool NmnFeMoJhsIMhJ_LHKqb2aY(BoundInputField boundInputField_0, string string_0)
	{
		if (mode_0 == Mode.OnValueChange)
		{
			base.Value = string_0;
		}
		return true;
	}

	private bool Rceqom_YsqLrN8MoagXKMKndWtBHRs3Z_0024AxOEbpdaeBZ(BoundInputField boundInputField_0, string string_0)
	{
		if (mode_0 == Mode.OnSubmit)
		{
			base.Value = string_0;
		}
		base.Inspector.RefreshDelayed();
		return true;
	}

	protected override void OnSkinChanged()
	{
		base.OnSkinChanged();
		lhP1dtEaJBf0cSBprqcD3IA.Skin = base.Skin;
		Vector2 vector2_ = new Vector2(base.Skin.LabelWidthPercentage, 0f);
		smethod_22(smethod_21((Graphic)variableNameMask), vector2_);
		smethod_22((RectTransform)smethod_23((Component)lhP1dtEaJBf0cSBprqcD3IA), vector2_);
	}

	public override void Refresh()
	{
		base.Refresh();
		if (base.Value != null)
		{
			lhP1dtEaJBf0cSBprqcD3IA.Text = (string)base.Value;
		}
		else
		{
			lhP1dtEaJBf0cSBprqcD3IA.Text = string.Empty;
		}
	}

	internal static Delegate smethod_16(Delegate delegate_0, Delegate delegate_1)
	{
		return Delegate.Combine(delegate_0, delegate_1);
	}

	internal static Type smethod_17(RuntimeTypeHandle runtimeTypeHandle_0)
	{
		return Type.GetTypeFromHandle(runtimeTypeHandle_0);
	}

	internal static void smethod_18(InputField inputField_0, InputField.LineType lineType_0)
	{
		inputField_0.lineType = lineType_0;
	}

	internal static Text smethod_19(InputField inputField_0)
	{
		return inputField_0.textComponent;
	}

	internal static void smethod_20(Text text_0, TextAnchor textAnchor_0)
	{
		text_0.alignment = textAnchor_0;
	}

	internal static RectTransform smethod_21(Graphic graphic_0)
	{
		return graphic_0.rectTransform;
	}

	internal static void smethod_22(RectTransform rectTransform_0, Vector2 vector2_0)
	{
		rectTransform_0.anchorMin = vector2_0;
	}

	internal static Transform smethod_23(Component component_0)
	{
		return component_0.transform;
	}
}
