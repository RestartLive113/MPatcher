using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace RuntimeInspectorNamespace;

public class Vector2Field : InspectorField
{
	[SerializeField]
	private BoundInputField GgXGaDsjt4_n3sGzlLI82Dc;

	[SerializeField]
	private BoundInputField G0XHHxKbz8TPs8faUHORXhA;

	[SerializeField]
	private Text tCzEBBshxyng9fvji7iTbU8;

	[SerializeField]
	private Text tf_e8ZMjt2LYyq5_0024CD9zrmY;

	public override void Initialize()
	{
		base.Initialize();
		GgXGaDsjt4_n3sGzlLI82Dc.Initialize();
		G0XHHxKbz8TPs8faUHORXhA.Initialize();
		BoundInputField ggXGaDsjt4_n3sGzlLI82Dc = GgXGaDsjt4_n3sGzlLI82Dc;
		ggXGaDsjt4_n3sGzlLI82Dc.OnValueChanged = (BoundInputField.OnValueChangedDelegate)smethod_16((Delegate)ggXGaDsjt4_n3sGzlLI82Dc.OnValueChanged, (Delegate)new BoundInputField.OnValueChangedDelegate(NmnFeMoJhsIMhJ_LHKqb2aY));
		BoundInputField g0XHHxKbz8TPs8faUHORXhA = G0XHHxKbz8TPs8faUHORXhA;
		g0XHHxKbz8TPs8faUHORXhA.OnValueChanged = (BoundInputField.OnValueChangedDelegate)smethod_16((Delegate)g0XHHxKbz8TPs8faUHORXhA.OnValueChanged, (Delegate)new BoundInputField.OnValueChangedDelegate(NmnFeMoJhsIMhJ_LHKqb2aY));
		BoundInputField ggXGaDsjt4_n3sGzlLI82Dc2 = GgXGaDsjt4_n3sGzlLI82Dc;
		ggXGaDsjt4_n3sGzlLI82Dc2.OnValueSubmitted = (BoundInputField.OnValueChangedDelegate)smethod_16((Delegate)ggXGaDsjt4_n3sGzlLI82Dc2.OnValueSubmitted, (Delegate)(BoundInputField.OnValueChangedDelegate)delegate(BoundInputField boundInputField_0, string string_0)
		{
			base.Inspector.RefreshDelayed();
			return NmnFeMoJhsIMhJ_LHKqb2aY(boundInputField_0, string_0);
		});
		BoundInputField g0XHHxKbz8TPs8faUHORXhA2 = G0XHHxKbz8TPs8faUHORXhA;
		g0XHHxKbz8TPs8faUHORXhA2.OnValueSubmitted = (BoundInputField.OnValueChangedDelegate)smethod_16((Delegate)g0XHHxKbz8TPs8faUHORXhA2.OnValueSubmitted, (Delegate)(BoundInputField.OnValueChangedDelegate)delegate(BoundInputField boundInputField_0, string string_0)
		{
			base.Inspector.RefreshDelayed();
			return NmnFeMoJhsIMhJ_LHKqb2aY(boundInputField_0, string_0);
		});
		GgXGaDsjt4_n3sGzlLI82Dc.DefaultEmptyValue = global::_003CModule_003E.smethod_26<string>(341714943u);
		G0XHHxKbz8TPs8faUHORXhA.DefaultEmptyValue = global::_003CModule_003E.smethod_28<string>(2349932817u);
	}

	public override bool SupportsType(Type type)
	{
		return type == smethod_17(typeof(Vector2).TypeHandle);
	}

	protected override void OnBound(MemberInfo variable)
	{
		base.OnBound(variable);
		Vector2 vector = (Vector2)base.Value;
		GgXGaDsjt4_n3sGzlLI82Dc.Text = vector.x.ToString();
		G0XHHxKbz8TPs8faUHORXhA.Text = vector.y.ToString();
	}

	private bool NmnFeMoJhsIMhJ_LHKqb2aY(BoundInputField boundInputField_0, string string_0)
	{
		if (float.TryParse(string_0, out var result))
		{
			Vector2 vector = (Vector2)base.Value;
			if (!smethod_18((UnityEngine.Object)boundInputField_0, (UnityEngine.Object)GgXGaDsjt4_n3sGzlLI82Dc))
			{
				vector.y = result;
			}
			else
			{
				vector.x = result;
			}
			base.Value = vector;
			return true;
		}
		return false;
	}

	private bool Rceqom_YsqLrN8MoagXKMKndWtBHRs3Z_0024AxOEbpdaeBZ(BoundInputField boundInputField_0, string string_0)
	{
		base.Inspector.RefreshDelayed();
		return NmnFeMoJhsIMhJ_LHKqb2aY(boundInputField_0, string_0);
	}

	protected override void OnSkinChanged()
	{
		base.OnSkinChanged();
		tCzEBBshxyng9fvji7iTbU8.SetSkinText(base.Skin);
		tf_e8ZMjt2LYyq5_0024CD9zrmY.SetSkinText(base.Skin);
		GgXGaDsjt4_n3sGzlLI82Dc.Skin = base.Skin;
		G0XHHxKbz8TPs8faUHORXhA.Skin = base.Skin;
		float num = (1f - base.Skin.LabelWidthPercentage) / 3f;
		Vector2 vector = new Vector2(base.Skin.LabelWidthPercentage + num, 0f);
		Vector2 anchorMax = new Vector2(base.Skin.LabelWidthPercentage + 2f * num, 1f);
		smethod_20(smethod_19((Graphic)variableNameMask), vector);
		((RectTransform)smethod_21((Component)GgXGaDsjt4_n3sGzlLI82Dc)).SetAnchorMinMaxInputField(smethod_19((Graphic)tCzEBBshxyng9fvji7iTbU8), vector, anchorMax);
		vector.x += num;
		anchorMax.x = 1f;
		((RectTransform)smethod_21((Component)G0XHHxKbz8TPs8faUHORXhA)).SetAnchorMinMaxInputField(smethod_19((Graphic)tf_e8ZMjt2LYyq5_0024CD9zrmY), vector, anchorMax);
	}

	public override void Refresh()
	{
		Vector2 vector = (Vector2)base.Value;
		base.Refresh();
		Vector2 vector2 = (Vector2)base.Value;
		if (vector2.x != vector.x)
		{
			GgXGaDsjt4_n3sGzlLI82Dc.Text = vector2.x.ToString();
		}
		if (vector2.y != vector.y)
		{
			G0XHHxKbz8TPs8faUHORXhA.Text = vector2.y.ToString();
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

	internal static bool smethod_18(UnityEngine.Object object_0, UnityEngine.Object object_1)
	{
		return object_0 == object_1;
	}

	internal static RectTransform smethod_19(Graphic graphic_0)
	{
		return graphic_0.rectTransform;
	}

	internal static void smethod_20(RectTransform rectTransform_0, Vector2 vector2_0)
	{
		rectTransform_0.anchorMin = vector2_0;
	}

	internal static Transform smethod_21(Component component_0)
	{
		return component_0.transform;
	}
}
