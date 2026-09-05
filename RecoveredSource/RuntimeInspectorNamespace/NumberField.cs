using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace RuntimeInspectorNamespace;

public class NumberField : InspectorField
{
	private static readonly HashSet<Type> O36L1Ox2IdnUKcD3ddUVJws = new HashSet<Type>
	{
		smethod_18(typeof(int).TypeHandle),
		smethod_18(typeof(uint).TypeHandle),
		smethod_18(typeof(long).TypeHandle),
		smethod_18(typeof(ulong).TypeHandle),
		smethod_18(typeof(byte).TypeHandle),
		smethod_18(typeof(sbyte).TypeHandle),
		smethod_18(typeof(short).TypeHandle),
		smethod_18(typeof(ushort).TypeHandle),
		smethod_18(typeof(char).TypeHandle),
		smethod_18(typeof(float).TypeHandle),
		smethod_18(typeof(double).TypeHandle),
		smethod_18(typeof(decimal).TypeHandle)
	};

	[SerializeField]
	protected BoundInputField input;

	protected INumberHandler numberHandler;

	public override void Initialize()
	{
		base.Initialize();
		input.Initialize();
		BoundInputField boundInputField = input;
		boundInputField.OnValueChanged = (BoundInputField.OnValueChangedDelegate)smethod_16((Delegate)boundInputField.OnValueChanged, (Delegate)new BoundInputField.OnValueChangedDelegate(OnValueChanged));
		BoundInputField boundInputField2 = input;
		boundInputField2.OnValueSubmitted = (BoundInputField.OnValueChangedDelegate)smethod_16((Delegate)boundInputField2.OnValueSubmitted, (Delegate)(BoundInputField.OnValueChangedDelegate)delegate(BoundInputField boundInputField_0, string string_0)
		{
			base.Inspector.RefreshDelayed();
			return OnValueChanged(boundInputField_0, string_0);
		});
		input.DefaultEmptyValue = global::_003CModule_003E.smethod_29<string>(1755123841u);
	}

	public override bool SupportsType(Type type)
	{
		return O36L1Ox2IdnUKcD3ddUVJws.Contains(type);
	}

	protected override void OnBound(MemberInfo variable)
	{
		base.OnBound(variable);
		numberHandler = NumberHandlers.Get(base.BoundVariableType);
		input.Text = smethod_17(base.Value);
		if (base.BoundVariableType != smethod_18(typeof(float).TypeHandle) && base.BoundVariableType != smethod_18(typeof(double).TypeHandle) && base.BoundVariableType != smethod_18(typeof(decimal).TypeHandle))
		{
			smethod_19(input.BackingField, InputField.ContentType.IntegerNumber);
		}
		else
		{
			smethod_19(input.BackingField, InputField.ContentType.DecimalNumber);
		}
	}

	protected virtual bool OnValueChanged(BoundInputField source, string input)
	{
		if (numberHandler.TryParse(input, out var value))
		{
			base.Value = value;
			return true;
		}
		return false;
	}

	private bool Rceqom_YsqLrN8MoagXKMKndWtBHRs3Z_0024AxOEbpdaeBZ(BoundInputField boundInputField_0, string string_0)
	{
		base.Inspector.RefreshDelayed();
		return OnValueChanged(boundInputField_0, string_0);
	}

	protected override void OnSkinChanged()
	{
		base.OnSkinChanged();
		input.Skin = base.Skin;
		Vector2 vector2_ = new Vector2(base.Skin.LabelWidthPercentage, 0f);
		smethod_21(smethod_20((Graphic)variableNameMask), vector2_);
		smethod_21((RectTransform)smethod_22((Component)input), vector2_);
	}

	public override void Refresh()
	{
		object value = base.Value;
		base.Refresh();
		if (!numberHandler.ValuesAreEqual(base.Value, value))
		{
			input.Text = smethod_17(base.Value);
		}
	}

	internal static Delegate smethod_16(Delegate delegate_0, Delegate delegate_1)
	{
		return Delegate.Combine(delegate_0, delegate_1);
	}

	internal static string smethod_17(object object_0)
	{
		return object_0.ToString();
	}

	internal static Type smethod_18(RuntimeTypeHandle runtimeTypeHandle_0)
	{
		return Type.GetTypeFromHandle(runtimeTypeHandle_0);
	}

	internal static void smethod_19(InputField inputField_0, InputField.ContentType contentType_0)
	{
		inputField_0.contentType = contentType_0;
	}

	internal static RectTransform smethod_20(Graphic graphic_0)
	{
		return graphic_0.rectTransform;
	}

	internal static void smethod_21(RectTransform rectTransform_0, Vector2 vector2_0)
	{
		rectTransform_0.anchorMin = vector2_0;
	}

	internal static Transform smethod_22(Component component_0)
	{
		return component_0.transform;
	}
}
