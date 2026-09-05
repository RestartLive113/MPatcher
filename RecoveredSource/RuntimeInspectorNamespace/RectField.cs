using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace RuntimeInspectorNamespace;

public class RectField : InspectorField
{
	[SerializeField]
	private BoundInputField GgXGaDsjt4_n3sGzlLI82Dc;

	[SerializeField]
	private BoundInputField G0XHHxKbz8TPs8faUHORXhA;

	[SerializeField]
	private BoundInputField Gdkq64fTaDzLWPZE1zXg1OM;

	[SerializeField]
	private BoundInputField Cg7Qh2CWIySF9iiXfY2GPn0;

	[SerializeField]
	private Text tCzEBBshxyng9fvji7iTbU8;

	[SerializeField]
	private Text tf_e8ZMjt2LYyq5_0024CD9zrmY;

	[SerializeField]
	private Text s01GFubnUZbYnLUyIFr1Xrs;

	[SerializeField]
	private Text pF9fGbONQg1M1x6Xo51JHus;

	protected override float HeightMultiplier => 2f;

	public override void Initialize()
	{
		base.Initialize();
		GgXGaDsjt4_n3sGzlLI82Dc.Initialize();
		G0XHHxKbz8TPs8faUHORXhA.Initialize();
		Gdkq64fTaDzLWPZE1zXg1OM.Initialize();
		Cg7Qh2CWIySF9iiXfY2GPn0.Initialize();
		BoundInputField ggXGaDsjt4_n3sGzlLI82Dc = GgXGaDsjt4_n3sGzlLI82Dc;
		ggXGaDsjt4_n3sGzlLI82Dc.OnValueChanged = (BoundInputField.OnValueChangedDelegate)smethod_16((Delegate)ggXGaDsjt4_n3sGzlLI82Dc.OnValueChanged, (Delegate)new BoundInputField.OnValueChangedDelegate(NmnFeMoJhsIMhJ_LHKqb2aY));
		BoundInputField g0XHHxKbz8TPs8faUHORXhA = G0XHHxKbz8TPs8faUHORXhA;
		g0XHHxKbz8TPs8faUHORXhA.OnValueChanged = (BoundInputField.OnValueChangedDelegate)smethod_16((Delegate)g0XHHxKbz8TPs8faUHORXhA.OnValueChanged, (Delegate)new BoundInputField.OnValueChangedDelegate(NmnFeMoJhsIMhJ_LHKqb2aY));
		BoundInputField gdkq64fTaDzLWPZE1zXg1OM = Gdkq64fTaDzLWPZE1zXg1OM;
		gdkq64fTaDzLWPZE1zXg1OM.OnValueChanged = (BoundInputField.OnValueChangedDelegate)smethod_16((Delegate)gdkq64fTaDzLWPZE1zXg1OM.OnValueChanged, (Delegate)new BoundInputField.OnValueChangedDelegate(NmnFeMoJhsIMhJ_LHKqb2aY));
		BoundInputField cg7Qh2CWIySF9iiXfY2GPn = Cg7Qh2CWIySF9iiXfY2GPn0;
		cg7Qh2CWIySF9iiXfY2GPn.OnValueChanged = (BoundInputField.OnValueChangedDelegate)smethod_16((Delegate)cg7Qh2CWIySF9iiXfY2GPn.OnValueChanged, (Delegate)new BoundInputField.OnValueChangedDelegate(NmnFeMoJhsIMhJ_LHKqb2aY));
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
		BoundInputField gdkq64fTaDzLWPZE1zXg1OM2 = Gdkq64fTaDzLWPZE1zXg1OM;
		gdkq64fTaDzLWPZE1zXg1OM2.OnValueSubmitted = (BoundInputField.OnValueChangedDelegate)smethod_16((Delegate)gdkq64fTaDzLWPZE1zXg1OM2.OnValueSubmitted, (Delegate)(BoundInputField.OnValueChangedDelegate)delegate(BoundInputField boundInputField_0, string string_0)
		{
			base.Inspector.RefreshDelayed();
			return NmnFeMoJhsIMhJ_LHKqb2aY(boundInputField_0, string_0);
		});
		BoundInputField cg7Qh2CWIySF9iiXfY2GPn2 = Cg7Qh2CWIySF9iiXfY2GPn0;
		cg7Qh2CWIySF9iiXfY2GPn2.OnValueSubmitted = (BoundInputField.OnValueChangedDelegate)smethod_16((Delegate)cg7Qh2CWIySF9iiXfY2GPn2.OnValueSubmitted, (Delegate)(BoundInputField.OnValueChangedDelegate)delegate(BoundInputField boundInputField_0, string string_0)
		{
			base.Inspector.RefreshDelayed();
			return NmnFeMoJhsIMhJ_LHKqb2aY(boundInputField_0, string_0);
		});
		GgXGaDsjt4_n3sGzlLI82Dc.DefaultEmptyValue = global::_003CModule_003E.smethod_26<string>(341714943u);
		G0XHHxKbz8TPs8faUHORXhA.DefaultEmptyValue = global::_003CModule_003E.smethod_25<string>(641366478u);
		Gdkq64fTaDzLWPZE1zXg1OM.DefaultEmptyValue = global::_003CModule_003E.smethod_29<string>(1755123841u);
		Cg7Qh2CWIySF9iiXfY2GPn0.DefaultEmptyValue = global::_003CModule_003E.smethod_27<string>(215347164u);
	}

	public override bool SupportsType(Type type)
	{
		return type == smethod_17(typeof(Rect).TypeHandle);
	}

	protected override void OnBound(MemberInfo variable)
	{
		base.OnBound(variable);
		Rect rect = (Rect)base.Value;
		GgXGaDsjt4_n3sGzlLI82Dc.Text = rect.x.ToString();
		G0XHHxKbz8TPs8faUHORXhA.Text = rect.y.ToString();
		Gdkq64fTaDzLWPZE1zXg1OM.Text = rect.width.ToString();
		Cg7Qh2CWIySF9iiXfY2GPn0.Text = rect.height.ToString();
	}

	private bool NmnFeMoJhsIMhJ_LHKqb2aY(BoundInputField boundInputField_0, string string_0)
	{
		if (float.TryParse(string_0, out var result))
		{
			Rect rect = (Rect)base.Value;
			if (smethod_18((UnityEngine.Object)boundInputField_0, (UnityEngine.Object)GgXGaDsjt4_n3sGzlLI82Dc))
			{
				rect.x = result;
			}
			else if (boundInputField_0 == G0XHHxKbz8TPs8faUHORXhA)
			{
				rect.y = result;
			}
			else if (boundInputField_0 == Gdkq64fTaDzLWPZE1zXg1OM)
			{
				rect.width = result;
			}
			else
			{
				rect.height = result;
			}
			base.Value = rect;
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
		s01GFubnUZbYnLUyIFr1Xrs.SetSkinText(base.Skin);
		pF9fGbONQg1M1x6Xo51JHus.SetSkinText(base.Skin);
		GgXGaDsjt4_n3sGzlLI82Dc.Skin = base.Skin;
		G0XHHxKbz8TPs8faUHORXhA.Skin = base.Skin;
		Gdkq64fTaDzLWPZE1zXg1OM.Skin = base.Skin;
		Cg7Qh2CWIySF9iiXfY2GPn0.Skin = base.Skin;
		float num = (1f - base.Skin.LabelWidthPercentage) / 3f;
		Vector2 vector = new Vector2(base.Skin.LabelWidthPercentage + num, 0f);
		Vector2 anchorMax = new Vector2(base.Skin.LabelWidthPercentage + 2f * num, 1f);
		smethod_20(smethod_19((Graphic)variableNameMask), vector);
		((RectTransform)smethod_21((Component)GgXGaDsjt4_n3sGzlLI82Dc)).SetAnchorMinMaxInputField(smethod_19((Graphic)tCzEBBshxyng9fvji7iTbU8), new Vector2(vector.x, 0.5f), anchorMax);
		((RectTransform)Gdkq64fTaDzLWPZE1zXg1OM.transform).SetAnchorMinMaxInputField(s01GFubnUZbYnLUyIFr1Xrs.rectTransform, vector, new Vector2(anchorMax.x, 0.5f));
		vector.x += num;
		anchorMax.x = 1f;
		((RectTransform)G0XHHxKbz8TPs8faUHORXhA.transform).SetAnchorMinMaxInputField(tf_e8ZMjt2LYyq5_0024CD9zrmY.rectTransform, new Vector2(vector.x, 0.5f), anchorMax);
		((RectTransform)Cg7Qh2CWIySF9iiXfY2GPn0.transform).SetAnchorMinMaxInputField(pF9fGbONQg1M1x6Xo51JHus.rectTransform, vector, new Vector2(anchorMax.x, 0.5f));
	}

	public override void Refresh()
	{
		Rect rect = (Rect)base.Value;
		base.Refresh();
		Rect rect2 = (Rect)base.Value;
		if (rect2.x != rect.x)
		{
			GgXGaDsjt4_n3sGzlLI82Dc.Text = rect2.x.ToString();
		}
		if (rect2.y != rect.y)
		{
			G0XHHxKbz8TPs8faUHORXhA.Text = rect2.y.ToString();
		}
		if (rect2.width != rect.width)
		{
			Gdkq64fTaDzLWPZE1zXg1OM.Text = rect2.width.ToString();
		}
		if (rect2.height != rect.height)
		{
			Cg7Qh2CWIySF9iiXfY2GPn0.Text = rect2.height.ToString();
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
