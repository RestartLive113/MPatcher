using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace RuntimeInspectorNamespace;

public class EnumField : InspectorField
{
	[SerializeField]
	private Image slJt0vtJTZ_pZ4HFn1Pm0w0;

	[SerializeField]
	private Image UDQZKojyPNfL9zaOuS5r624;

	[SerializeField]
	private RectTransform lkIClUkxdB3TndpQg8ONMx20nfP8EIaZIjQmqqBuOuj0;

	[SerializeField]
	private Image kNgQ4ALjJWeRURpnCrS5jvoqaoUQrG_0024phvZnFf_0024v8zH5;

	[SerializeField]
	private Image H5MtxmKIKNbkh_0024KLZ3nxIvp6EgbnejWK8jsBs85O1wlo;

	[SerializeField]
	private Text s8go7GK4gCbnsT_0024YluJwkVo;

	[SerializeField]
	private Dropdown lhP1dtEaJBf0cSBprqcD3IA;

	private static readonly Dictionary<Type, List<string>> Qw5L1uPF5T_0024R6RttRyCL1As = new Dictionary<Type, List<string>>();

	private static readonly Dictionary<Type, List<object>> H_0024iFoE_0024CXYxJtp9PxI9ZLMI = new Dictionary<Type, List<object>>();

	private List<string> Za832ugiNnvE39xHJNO0LB8;

	private List<object> hZVAi65BdKgCsZkb2RqKBCs;

	public override void Initialize()
	{
		base.Initialize();
		smethod_16(lhP1dtEaJBf0cSBprqcD3IA).AddListener(NmnFeMoJhsIMhJ_LHKqb2aY);
	}

	public override bool SupportsType(Type type)
	{
		return smethod_17(type);
	}

	protected override void OnBound(MemberInfo variable)
	{
		base.OnBound(variable);
		if (!Qw5L1uPF5T_0024R6RttRyCL1As.TryGetValue(base.BoundVariableType, out Za832ugiNnvE39xHJNO0LB8) || !H_0024iFoE_0024CXYxJtp9PxI9ZLMI.TryGetValue(base.BoundVariableType, out hZVAi65BdKgCsZkb2RqKBCs))
		{
			string[] array = smethod_18(base.BoundVariableType);
			Array array_ = smethod_19(base.BoundVariableType);
			Za832ugiNnvE39xHJNO0LB8 = new List<string>(array.Length);
			hZVAi65BdKgCsZkb2RqKBCs = new List<object>(array.Length);
			for (int i = 0; i < array.Length; i++)
			{
				Za832ugiNnvE39xHJNO0LB8.Add(array[i]);
				hZVAi65BdKgCsZkb2RqKBCs.Add(smethod_20(array_, i));
			}
			Qw5L1uPF5T_0024R6RttRyCL1As[base.BoundVariableType] = Za832ugiNnvE39xHJNO0LB8;
			H_0024iFoE_0024CXYxJtp9PxI9ZLMI[base.BoundVariableType] = hZVAi65BdKgCsZkb2RqKBCs;
		}
		smethod_21(lhP1dtEaJBf0cSBprqcD3IA);
		smethod_22(lhP1dtEaJBf0cSBprqcD3IA, Za832ugiNnvE39xHJNO0LB8);
	}

	private void NmnFeMoJhsIMhJ_LHKqb2aY(int int_0)
	{
		base.Value = hZVAi65BdKgCsZkb2RqKBCs[int_0];
		base.Inspector.RefreshDelayed();
	}

	protected override void OnSkinChanged()
	{
		base.OnSkinChanged();
		Vector2 vector2_ = smethod_23(lkIClUkxdB3TndpQg8ONMx20nfP8EIaZIjQmqqBuOuj0);
		vector2_.y = base.Skin.LineHeight;
		smethod_24(lkIClUkxdB3TndpQg8ONMx20nfP8EIaZIjQmqqBuOuj0, vector2_);
		smethod_25((Graphic)slJt0vtJTZ_pZ4HFn1Pm0w0, base.Skin.InputFieldNormalBackgroundColor);
		smethod_25((Graphic)UDQZKojyPNfL9zaOuS5r624, base.Skin.TextColor.Tint(0.1f));
		smethod_26(lhP1dtEaJBf0cSBprqcD3IA).SetSkinInputFieldText(base.Skin);
		s8go7GK4gCbnsT_0024YluJwkVo.SetSkinInputFieldText(base.Skin);
		smethod_25((Graphic)kNgQ4ALjJWeRURpnCrS5jvoqaoUQrG_0024phvZnFf_0024v8zH5, base.Skin.InputFieldNormalBackgroundColor.Tint(0.075f));
		smethod_25((Graphic)H5MtxmKIKNbkh_0024KLZ3nxIvp6EgbnejWK8jsBs85O1wlo, base.Skin.ToggleCheckmarkColor);
		Vector2 vector2_2 = new Vector2(base.Skin.LabelWidthPercentage, 0f);
		smethod_28(smethod_27((Graphic)variableNameMask), vector2_2);
		smethod_28((RectTransform)smethod_29((Component)lhP1dtEaJBf0cSBprqcD3IA), vector2_2);
	}

	public override void Refresh()
	{
		base.Refresh();
		int num = hZVAi65BdKgCsZkb2RqKBCs.IndexOf(base.Value);
		if (num != -1)
		{
			smethod_30(lhP1dtEaJBf0cSBprqcD3IA, num);
		}
	}

	internal static Dropdown.DropdownEvent smethod_16(Dropdown dropdown_0)
	{
		return dropdown_0.onValueChanged;
	}

	internal static bool smethod_17(Type type_0)
	{
		return type_0.IsEnum;
	}

	internal static string[] smethod_18(Type type_0)
	{
		return Enum.GetNames(type_0);
	}

	internal static Array smethod_19(Type type_0)
	{
		return Enum.GetValues(type_0);
	}

	internal static object smethod_20(Array array_0, int int_0)
	{
		return array_0.GetValue(int_0);
	}

	internal static void smethod_21(Dropdown dropdown_0)
	{
		dropdown_0.ClearOptions();
	}

	internal static void smethod_22(Dropdown dropdown_0, List<string> list_0)
	{
		dropdown_0.AddOptions(list_0);
	}

	internal static Vector2 smethod_23(RectTransform rectTransform_0)
	{
		return rectTransform_0.sizeDelta;
	}

	internal static void smethod_24(RectTransform rectTransform_0, Vector2 vector2_0)
	{
		rectTransform_0.sizeDelta = vector2_0;
	}

	internal static void smethod_25(Graphic graphic_0, Color color_0)
	{
		graphic_0.color = color_0;
	}

	internal static Text smethod_26(Dropdown dropdown_0)
	{
		return dropdown_0.captionText;
	}

	internal static RectTransform smethod_27(Graphic graphic_0)
	{
		return graphic_0.rectTransform;
	}

	internal static void smethod_28(RectTransform rectTransform_0, Vector2 vector2_0)
	{
		rectTransform_0.anchorMin = vector2_0;
	}

	internal static Transform smethod_29(Component component_0)
	{
		return component_0.transform;
	}

	internal static void smethod_30(Dropdown dropdown_0, int int_0)
	{
		dropdown_0.value = int_0;
	}
}
