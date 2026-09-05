using System;
using UnityEngine;
using UnityEngine.UI;

internal class Control0 : WidgetController
{
	private Action<bool> g2nxBfEELbYmP1Ar9f9qNEE;

	private Action YL85Bj2ucnQKoP9tlnvouT4;

	private bool bool_0;

	internal bool hLxnG9Hq33zU_YUsu_00240_zak
	{
		get
		{
			return smethod_1(GetComponent<Toggle>());
		}
		set
		{
			smethod_0(GetComponent<Toggle>(), value);
		}
	}

	internal bool FLSdXom6uNTfN55f5nxTsH8
	{
		get
		{
			return smethod_3((Selectable)GetComponent<Toggle>());
		}
		set
		{
			smethod_2((Selectable)GetComponent<Toggle>(), value);
		}
	}

	internal Toggle Tz4h_68oANQj5xAU0vtoknA => GetComponent<Toggle>();

	protected override void Update()
	{
		base.Update();
		if (bool_0 != hLxnG9Hq33zU_YUsu_00240_zak)
		{
			if (bool_0 && YL85Bj2ucnQKoP9tlnvouT4 != null)
			{
				YL85Bj2ucnQKoP9tlnvouT4();
			}
			bool_0 = hLxnG9Hq33zU_YUsu_00240_zak;
		}
	}

	internal void kBPtltqxQQyZ5ym0_0024wsbeUc()
	{
		if (g2nxBfEELbYmP1Ar9f9qNEE != null)
		{
			g2nxBfEELbYmP1Ar9f9qNEE(hLxnG9Hq33zU_YUsu_00240_zak);
		}
	}

	internal void t2iJT_tBPyB6QRMBLAdXYUs(Action<bool> onClick)
	{
		g2nxBfEELbYmP1Ar9f9qNEE = onClick;
	}

	internal void method_0(Action untoggle)
	{
		YL85Bj2ucnQKoP9tlnvouT4 = untoggle;
	}

	internal void DmPZGWxJ26_0024f_0024QOvQiqpmW8(Vector2 size)
	{
		RectTransform rectTransform_ = (RectTransform)smethod_4((Component)Tz4h_68oANQj5xAU0vtoknA);
		smethod_6(rectTransform_, smethod_5(rectTransform_) + size);
	}

	internal void UzVS61irgJn5Pnqwx0lThng(Vector2 size)
	{
		smethod_6((RectTransform)smethod_4((Component)Tz4h_68oANQj5xAU0vtoknA), size);
	}

	protected virtual void slAohyJbgvnQR_0024kdjOrtJBQ()
	{
		if (smethod_3((Selectable)GetComponent<Toggle>()))
		{
			if (g2nxBfEELbYmP1Ar9f9qNEE != null)
			{
				g2nxBfEELbYmP1Ar9f9qNEE(hLxnG9Hq33zU_YUsu_00240_zak);
			}
			smethod_7(global::_003CModule_003E.smethod_28<string>(3304653811u), 1f);
		}
	}

	internal static void smethod_0(Toggle toggle_0, bool bool_1)
	{
		toggle_0.isOn = bool_1;
	}

	internal static bool smethod_1(Toggle toggle_0)
	{
		return toggle_0.isOn;
	}

	internal static void smethod_2(Selectable selectable_0, bool bool_1)
	{
		selectable_0.interactable = bool_1;
	}

	internal static bool smethod_3(Selectable selectable_0)
	{
		return selectable_0.IsInteractable();
	}

	internal static Transform smethod_4(Component component_0)
	{
		return component_0.transform;
	}

	internal static Vector2 smethod_5(RectTransform rectTransform_0)
	{
		return rectTransform_0.sizeDelta;
	}

	internal static void smethod_6(RectTransform rectTransform_0, Vector2 vector2_0)
	{
		rectTransform_0.sizeDelta = vector2_0;
	}

	internal static void smethod_7(string string_0, float float_0)
	{
		KEFHJCGICLE.HNAHBIMJDCB(string_0, float_0);
	}
}
