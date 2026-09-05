using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace RuntimeInspectorNamespace;

public class Vector4Field : InspectorField
{
	[SerializeField]
	private BoundInputField GgXGaDsjt4_n3sGzlLI82Dc;

	[SerializeField]
	private BoundInputField G0XHHxKbz8TPs8faUHORXhA;

	[SerializeField]
	private BoundInputField HA0kwHXcjfW13LE9b94fFHY;

	[SerializeField]
	private BoundInputField Gdkq64fTaDzLWPZE1zXg1OM;

	[SerializeField]
	private Text tCzEBBshxyng9fvji7iTbU8;

	[SerializeField]
	private Text tf_e8ZMjt2LYyq5_0024CD9zrmY;

	[SerializeField]
	private Text tlDpHlAt05PDI70hUlLvmIE;

	[SerializeField]
	private Text s01GFubnUZbYnLUyIFr1Xrs;

	private bool PB7o6FQlOtS_fZlELZm0jn0;

	protected override float HeightMultiplier => 2f;

	public override void Initialize()
	{
		base.Initialize();
		GgXGaDsjt4_n3sGzlLI82Dc.Initialize();
		G0XHHxKbz8TPs8faUHORXhA.Initialize();
		HA0kwHXcjfW13LE9b94fFHY.Initialize();
		Gdkq64fTaDzLWPZE1zXg1OM.Initialize();
		BoundInputField ggXGaDsjt4_n3sGzlLI82Dc = GgXGaDsjt4_n3sGzlLI82Dc;
		ggXGaDsjt4_n3sGzlLI82Dc.OnValueChanged = (BoundInputField.OnValueChangedDelegate)smethod_16((Delegate)ggXGaDsjt4_n3sGzlLI82Dc.OnValueChanged, (Delegate)new BoundInputField.OnValueChangedDelegate(NmnFeMoJhsIMhJ_LHKqb2aY));
		BoundInputField g0XHHxKbz8TPs8faUHORXhA = G0XHHxKbz8TPs8faUHORXhA;
		g0XHHxKbz8TPs8faUHORXhA.OnValueChanged = (BoundInputField.OnValueChangedDelegate)smethod_16((Delegate)g0XHHxKbz8TPs8faUHORXhA.OnValueChanged, (Delegate)new BoundInputField.OnValueChangedDelegate(NmnFeMoJhsIMhJ_LHKqb2aY));
		BoundInputField hA0kwHXcjfW13LE9b94fFHY = HA0kwHXcjfW13LE9b94fFHY;
		hA0kwHXcjfW13LE9b94fFHY.OnValueChanged = (BoundInputField.OnValueChangedDelegate)smethod_16((Delegate)hA0kwHXcjfW13LE9b94fFHY.OnValueChanged, (Delegate)new BoundInputField.OnValueChangedDelegate(NmnFeMoJhsIMhJ_LHKqb2aY));
		BoundInputField gdkq64fTaDzLWPZE1zXg1OM = Gdkq64fTaDzLWPZE1zXg1OM;
		gdkq64fTaDzLWPZE1zXg1OM.OnValueChanged = (BoundInputField.OnValueChangedDelegate)smethod_16((Delegate)gdkq64fTaDzLWPZE1zXg1OM.OnValueChanged, (Delegate)new BoundInputField.OnValueChangedDelegate(NmnFeMoJhsIMhJ_LHKqb2aY));
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
		BoundInputField hA0kwHXcjfW13LE9b94fFHY2 = HA0kwHXcjfW13LE9b94fFHY;
		hA0kwHXcjfW13LE9b94fFHY2.OnValueSubmitted = (BoundInputField.OnValueChangedDelegate)smethod_16((Delegate)hA0kwHXcjfW13LE9b94fFHY2.OnValueSubmitted, (Delegate)(BoundInputField.OnValueChangedDelegate)delegate(BoundInputField boundInputField_0, string string_0)
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
		GgXGaDsjt4_n3sGzlLI82Dc.DefaultEmptyValue = global::_003CModule_003E.smethod_27<string>(215347164u);
		G0XHHxKbz8TPs8faUHORXhA.DefaultEmptyValue = global::_003CModule_003E.smethod_26<string>(341714943u);
		HA0kwHXcjfW13LE9b94fFHY.DefaultEmptyValue = global::_003CModule_003E.smethod_26<string>(341714943u);
		Gdkq64fTaDzLWPZE1zXg1OM.DefaultEmptyValue = global::_003CModule_003E.smethod_25<string>(641366478u);
	}

	public override bool SupportsType(Type type)
	{
		if (type != smethod_17(typeof(Vector4).TypeHandle))
		{
			return type == smethod_17(typeof(Quaternion).TypeHandle);
		}
		return true;
	}

	protected override void OnBound(MemberInfo variable)
	{
		base.OnBound(variable);
		PB7o6FQlOtS_fZlELZm0jn0 = base.BoundVariableType == smethod_17(typeof(Quaternion).TypeHandle);
		if (PB7o6FQlOtS_fZlELZm0jn0)
		{
			Quaternion quaternion = (Quaternion)base.Value;
			GgXGaDsjt4_n3sGzlLI82Dc.Text = quaternion.x.ToString();
			G0XHHxKbz8TPs8faUHORXhA.Text = quaternion.y.ToString();
			HA0kwHXcjfW13LE9b94fFHY.Text = quaternion.z.ToString();
			Gdkq64fTaDzLWPZE1zXg1OM.Text = quaternion.z.ToString();
		}
		else
		{
			Vector4 vector = (Vector4)base.Value;
			GgXGaDsjt4_n3sGzlLI82Dc.Text = vector.x.ToString();
			G0XHHxKbz8TPs8faUHORXhA.Text = vector.y.ToString();
			HA0kwHXcjfW13LE9b94fFHY.Text = vector.z.ToString();
			Gdkq64fTaDzLWPZE1zXg1OM.Text = vector.z.ToString();
		}
	}

	private bool NmnFeMoJhsIMhJ_LHKqb2aY(BoundInputField boundInputField_0, string string_0)
	{
		if (float.TryParse(string_0, out var result))
		{
			if (PB7o6FQlOtS_fZlELZm0jn0)
			{
				Quaternion quaternion = (Quaternion)base.Value;
				if (!smethod_18((UnityEngine.Object)boundInputField_0, (UnityEngine.Object)GgXGaDsjt4_n3sGzlLI82Dc))
				{
					if (smethod_18((UnityEngine.Object)boundInputField_0, (UnityEngine.Object)G0XHHxKbz8TPs8faUHORXhA))
					{
						quaternion.y = result;
					}
					else if (smethod_18((UnityEngine.Object)boundInputField_0, (UnityEngine.Object)HA0kwHXcjfW13LE9b94fFHY))
					{
						quaternion.z = result;
					}
					else
					{
						quaternion.w = result;
					}
				}
				else
				{
					quaternion.x = result;
				}
				base.Value = quaternion;
			}
			else
			{
				Vector4 vector = (Vector4)base.Value;
				if (!smethod_18((UnityEngine.Object)boundInputField_0, (UnityEngine.Object)GgXGaDsjt4_n3sGzlLI82Dc))
				{
					if (!smethod_18((UnityEngine.Object)boundInputField_0, (UnityEngine.Object)G0XHHxKbz8TPs8faUHORXhA))
					{
						if (!smethod_18((UnityEngine.Object)boundInputField_0, (UnityEngine.Object)HA0kwHXcjfW13LE9b94fFHY))
						{
							vector.w = result;
						}
						else
						{
							vector.z = result;
						}
					}
					else
					{
						vector.y = result;
					}
				}
				else
				{
					vector.x = result;
				}
				base.Value = vector;
			}
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
		tlDpHlAt05PDI70hUlLvmIE.SetSkinText(base.Skin);
		s01GFubnUZbYnLUyIFr1Xrs.SetSkinText(base.Skin);
		GgXGaDsjt4_n3sGzlLI82Dc.Skin = base.Skin;
		G0XHHxKbz8TPs8faUHORXhA.Skin = base.Skin;
		HA0kwHXcjfW13LE9b94fFHY.Skin = base.Skin;
		Gdkq64fTaDzLWPZE1zXg1OM.Skin = base.Skin;
		float num = (1f - base.Skin.LabelWidthPercentage) / 3f;
		Vector2 vector = new Vector2(base.Skin.LabelWidthPercentage + num, 0f);
		Vector2 anchorMax = new Vector2(base.Skin.LabelWidthPercentage + 2f * num, 1f);
		smethod_20(smethod_19((Graphic)variableNameMask), vector);
		((RectTransform)smethod_21((Component)GgXGaDsjt4_n3sGzlLI82Dc)).SetAnchorMinMaxInputField(smethod_19((Graphic)tCzEBBshxyng9fvji7iTbU8), new Vector2(vector.x, 0.5f), anchorMax);
		((RectTransform)HA0kwHXcjfW13LE9b94fFHY.transform).SetAnchorMinMaxInputField(tlDpHlAt05PDI70hUlLvmIE.rectTransform, vector, new Vector2(anchorMax.x, 0.5f));
		vector.x += num;
		anchorMax.x = 1f;
		((RectTransform)G0XHHxKbz8TPs8faUHORXhA.transform).SetAnchorMinMaxInputField(tf_e8ZMjt2LYyq5_0024CD9zrmY.rectTransform, new Vector2(vector.x, 0.5f), anchorMax);
		((RectTransform)Gdkq64fTaDzLWPZE1zXg1OM.transform).SetAnchorMinMaxInputField(s01GFubnUZbYnLUyIFr1Xrs.rectTransform, vector, new Vector2(anchorMax.x, 0.5f));
	}

	public override void Refresh()
	{
		if (PB7o6FQlOtS_fZlELZm0jn0)
		{
			Quaternion quaternion = (Quaternion)base.Value;
			base.Refresh();
			Quaternion quaternion2 = (Quaternion)base.Value;
			if (quaternion2.x != quaternion.x)
			{
				GgXGaDsjt4_n3sGzlLI82Dc.Text = quaternion2.x.ToString();
			}
			if (quaternion2.y != quaternion.y)
			{
				G0XHHxKbz8TPs8faUHORXhA.Text = quaternion2.y.ToString();
			}
			if (quaternion2.z != quaternion.z)
			{
				HA0kwHXcjfW13LE9b94fFHY.Text = quaternion2.z.ToString();
			}
			if (quaternion2.w != quaternion.w)
			{
				Gdkq64fTaDzLWPZE1zXg1OM.Text = quaternion2.z.ToString();
			}
		}
		else
		{
			Vector4 vector = (Vector4)base.Value;
			base.Refresh();
			Vector4 vector2 = (Vector4)base.Value;
			if (vector2.x != vector.x)
			{
				GgXGaDsjt4_n3sGzlLI82Dc.Text = vector2.x.ToString();
			}
			if (vector2.y != vector.y)
			{
				G0XHHxKbz8TPs8faUHORXhA.Text = vector2.y.ToString();
			}
			if (vector2.z != vector.z)
			{
				HA0kwHXcjfW13LE9b94fFHY.Text = vector2.z.ToString();
			}
			if (vector2.w != vector.w)
			{
				Gdkq64fTaDzLWPZE1zXg1OM.Text = vector2.z.ToString();
			}
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
