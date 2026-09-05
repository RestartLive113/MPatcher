using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace RuntimeInspectorNamespace;

public class ColorPicker : SkinnedWindow
{
	private static ColorPicker K1hFj5zLZuQMBpgW_EkwUtE;

	[SerializeField]
	private Image JMy24cZsilDTO4e_0024231jlXk;

	[SerializeField]
	private ColorWheelControl MLuyB7Q_0024QroOtOholke2aiQ;

	[SerializeField]
	private ColorPickerAlphaSlider GyhUab8AR_0JCTzTRPgaHGs;

	[SerializeField]
	private Text MfAjFp6vXMLJBo17EdMRfCY;

	[SerializeField]
	private BoundInputField rHC2zbpeT7masu5PZdniorY;

	[SerializeField]
	private BoundInputField O2lnRat3gvbMa3FtRJ6VkrI;

	[SerializeField]
	private BoundInputField fMZA_0024H91t1Sn3TCKNgnycf4;

	[SerializeField]
	private BoundInputField iTbALEAFgZNZCPsWsUGdqmg;

	[SerializeField]
	private LayoutElement is6VHld6IP1rkQkoC_0024n4qRlvt6RakWw_kNtK_vIZOhWN;

	[SerializeField]
	private LayoutElement dVcX6SuSx_0024kGqLGokraly5ZuQetKibReHJR7Lg2nEiaD;

	[SerializeField]
	private Button AKwP8uWl1WlZSC4gKmG7L2A;

	[SerializeField]
	private Button rmWSiGwV1eJSSIxHbaQx6co;

	private Canvas Kw6kqIgvhzUS4V6ocIEIxYY;

	private Color YQchHFKnioVlxqspaiQkP8M;

	private ColorWheelControl.OnColorChangedDelegate fG6WzjHolrbm0dCESUu_0024K7U;

	public static ColorPicker Instance
	{
		get
		{
			if (!smethod_3((UnityEngine.Object)K1hFj5zLZuQMBpgW_EkwUtE))
			{
				K1hFj5zLZuQMBpgW_EkwUtE = UnityEngine.Object.Instantiate(awf1opR73mv9LSqQ84LlTsI.cqWoMNveroNrLO3XzL3B_0024XA<GameObject>(global::_003CModule_003E.smethod_28<string>(361766944u))).GetComponent<ColorPicker>();
				smethod_5(smethod_4((Component)K1hFj5zLZuQMBpgW_EkwUtE), bool_0: false);
				RuntimeInspectorUtils.IgnoredTransformsInHierarchy.Add(smethod_6((Component)K1hFj5zLZuQMBpgW_EkwUtE));
			}
			return K1hFj5zLZuQMBpgW_EkwUtE;
		}
	}

	protected override void Awake()
	{
		base.Awake();
		rHC2zbpeT7masu5PZdniorY.Initialize();
		O2lnRat3gvbMa3FtRJ6VkrI.Initialize();
		fMZA_0024H91t1Sn3TCKNgnycf4.Initialize();
		iTbALEAFgZNZCPsWsUGdqmg.Initialize();
		smethod_8((UnityEvent)smethod_7(AKwP8uWl1WlZSC4gKmG7L2A), (UnityAction)Cancel);
		smethod_8((UnityEvent)smethod_7(rmWSiGwV1eJSSIxHbaQx6co), (UnityAction)Close);
	}

	private void sp_GCK595YHY1vrEPNGiSrQ()
	{
		MLuyB7Q_0024QroOtOholke2aiQ.OnColorChanged += S1HBCwykHDfYcMIdeqh59_WJnt5GKdmHMaifbrKiZsAs;
		ColorPickerAlphaSlider gyhUab8AR_0JCTzTRPgaHGs = GyhUab8AR_0JCTzTRPgaHGs;
		gyhUab8AR_0JCTzTRPgaHGs.OnValueChanged = (ColorPickerAlphaSlider.OnValueChangedDelegate)smethod_9((Delegate)gyhUab8AR_0JCTzTRPgaHGs.OnValueChanged, (Delegate)new ColorPickerAlphaSlider.OnValueChangedDelegate(Fd31xv4TlP58kqxKjDf8tX4));
		rHC2zbpeT7masu5PZdniorY.DefaultEmptyValue = global::_003CModule_003E.smethod_25<string>(641366478u);
		O2lnRat3gvbMa3FtRJ6VkrI.DefaultEmptyValue = global::_003CModule_003E.smethod_26<string>(341714943u);
		fMZA_0024H91t1Sn3TCKNgnycf4.DefaultEmptyValue = global::_003CModule_003E.smethod_29<string>(1755123841u);
		iTbALEAFgZNZCPsWsUGdqmg.DefaultEmptyValue = global::_003CModule_003E.smethod_28<string>(2349932817u);
		rHC2zbpeT7masu5PZdniorY.Skin = base.Skin;
		O2lnRat3gvbMa3FtRJ6VkrI.Skin = base.Skin;
		fMZA_0024H91t1Sn3TCKNgnycf4.Skin = base.Skin;
		iTbALEAFgZNZCPsWsUGdqmg.Skin = base.Skin;
		BoundInputField boundInputField = rHC2zbpeT7masu5PZdniorY;
		boundInputField.OnValueChanged = (BoundInputField.OnValueChangedDelegate)smethod_9((Delegate)boundInputField.OnValueChanged, (Delegate)(BoundInputField.OnValueChangedDelegate)delegate(BoundInputField boundInputField_0, string string_0)
		{
			if (byte.TryParse(string_0, out var result))
			{
				Color32 color = MLuyB7Q_0024QroOtOholke2aiQ.Color;
				if (!smethod_16((UnityEngine.Object)boundInputField_0, (UnityEngine.Object)rHC2zbpeT7masu5PZdniorY))
				{
					if (smethod_16((UnityEngine.Object)boundInputField_0, (UnityEngine.Object)O2lnRat3gvbMa3FtRJ6VkrI))
					{
						color.g = result;
					}
					else if (!smethod_16((UnityEngine.Object)boundInputField_0, (UnityEngine.Object)fMZA_0024H91t1Sn3TCKNgnycf4))
					{
						color.a = result;
						GyhUab8AR_0JCTzTRPgaHGs.Value = (float)(int)result / 255f;
					}
					else
					{
						color.b = result;
					}
				}
				else
				{
					color.r = result;
				}
				GyhUab8AR_0JCTzTRPgaHGs.Color = color;
				MLuyB7Q_0024QroOtOholke2aiQ.PickColor(color);
				return true;
			}
			return false;
		});
		BoundInputField o2lnRat3gvbMa3FtRJ6VkrI = O2lnRat3gvbMa3FtRJ6VkrI;
		o2lnRat3gvbMa3FtRJ6VkrI.OnValueChanged = (BoundInputField.OnValueChangedDelegate)smethod_9((Delegate)o2lnRat3gvbMa3FtRJ6VkrI.OnValueChanged, (Delegate)(BoundInputField.OnValueChangedDelegate)delegate(BoundInputField boundInputField_0, string string_0)
		{
			if (byte.TryParse(string_0, out var result))
			{
				Color32 color = MLuyB7Q_0024QroOtOholke2aiQ.Color;
				if (!smethod_16((UnityEngine.Object)boundInputField_0, (UnityEngine.Object)rHC2zbpeT7masu5PZdniorY))
				{
					if (smethod_16((UnityEngine.Object)boundInputField_0, (UnityEngine.Object)O2lnRat3gvbMa3FtRJ6VkrI))
					{
						color.g = result;
					}
					else if (!smethod_16((UnityEngine.Object)boundInputField_0, (UnityEngine.Object)fMZA_0024H91t1Sn3TCKNgnycf4))
					{
						color.a = result;
						GyhUab8AR_0JCTzTRPgaHGs.Value = (float)(int)result / 255f;
					}
					else
					{
						color.b = result;
					}
				}
				else
				{
					color.r = result;
				}
				GyhUab8AR_0JCTzTRPgaHGs.Color = color;
				MLuyB7Q_0024QroOtOholke2aiQ.PickColor(color);
				return true;
			}
			return false;
		});
		BoundInputField boundInputField2 = fMZA_0024H91t1Sn3TCKNgnycf4;
		boundInputField2.OnValueChanged = (BoundInputField.OnValueChangedDelegate)smethod_9((Delegate)boundInputField2.OnValueChanged, (Delegate)(BoundInputField.OnValueChangedDelegate)delegate(BoundInputField boundInputField_0, string string_0)
		{
			if (byte.TryParse(string_0, out var result))
			{
				Color32 color = MLuyB7Q_0024QroOtOholke2aiQ.Color;
				if (!smethod_16((UnityEngine.Object)boundInputField_0, (UnityEngine.Object)rHC2zbpeT7masu5PZdniorY))
				{
					if (smethod_16((UnityEngine.Object)boundInputField_0, (UnityEngine.Object)O2lnRat3gvbMa3FtRJ6VkrI))
					{
						color.g = result;
					}
					else if (!smethod_16((UnityEngine.Object)boundInputField_0, (UnityEngine.Object)fMZA_0024H91t1Sn3TCKNgnycf4))
					{
						color.a = result;
						GyhUab8AR_0JCTzTRPgaHGs.Value = (float)(int)result / 255f;
					}
					else
					{
						color.b = result;
					}
				}
				else
				{
					color.r = result;
				}
				GyhUab8AR_0JCTzTRPgaHGs.Color = color;
				MLuyB7Q_0024QroOtOholke2aiQ.PickColor(color);
				return true;
			}
			return false;
		});
		BoundInputField boundInputField3 = iTbALEAFgZNZCPsWsUGdqmg;
		boundInputField3.OnValueChanged = (BoundInputField.OnValueChangedDelegate)smethod_9((Delegate)boundInputField3.OnValueChanged, (Delegate)(BoundInputField.OnValueChangedDelegate)delegate(BoundInputField boundInputField_0, string string_0)
		{
			if (byte.TryParse(string_0, out var result))
			{
				Color32 color = MLuyB7Q_0024QroOtOholke2aiQ.Color;
				if (!smethod_16((UnityEngine.Object)boundInputField_0, (UnityEngine.Object)rHC2zbpeT7masu5PZdniorY))
				{
					if (smethod_16((UnityEngine.Object)boundInputField_0, (UnityEngine.Object)O2lnRat3gvbMa3FtRJ6VkrI))
					{
						color.g = result;
					}
					else if (!smethod_16((UnityEngine.Object)boundInputField_0, (UnityEngine.Object)fMZA_0024H91t1Sn3TCKNgnycf4))
					{
						color.a = result;
						GyhUab8AR_0JCTzTRPgaHGs.Value = (float)(int)result / 255f;
					}
					else
					{
						color.b = result;
					}
				}
				else
				{
					color.r = result;
				}
				GyhUab8AR_0JCTzTRPgaHGs.Color = color;
				MLuyB7Q_0024QroOtOholke2aiQ.PickColor(color);
				return true;
			}
			return false;
		});
		S1HBCwykHDfYcMIdeqh59_WJnt5GKdmHMaifbrKiZsAs(MLuyB7Q_0024QroOtOholke2aiQ.Color);
	}

	public void Show(ColorWheelControl.OnColorChangedDelegate onColorChanged, Color initialColor, Canvas referenceCanvas)
	{
		YQchHFKnioVlxqspaiQkP8M = initialColor;
		fG6WzjHolrbm0dCESUu_0024K7U = null;
		MLuyB7Q_0024QroOtOholke2aiQ.PickColor(initialColor);
		GyhUab8AR_0JCTzTRPgaHGs.Color = initialColor;
		GyhUab8AR_0JCTzTRPgaHGs.Value = initialColor.a;
		fG6WzjHolrbm0dCESUu_0024K7U = onColorChanged;
		if (smethod_3((UnityEngine.Object)referenceCanvas) && smethod_10((UnityEngine.Object)Kw6kqIgvhzUS4V6ocIEIxYY, (UnityEngine.Object)referenceCanvas))
		{
			Kw6kqIgvhzUS4V6ocIEIxYY = referenceCanvas;
			Canvas component = GetComponent<Canvas>();
			component.CopyValuesFrom(referenceCanvas);
			smethod_12(component, Mathf.Max(1000, smethod_11(referenceCanvas) + 100));
		}
		smethod_13((RectTransform)smethod_6((Component)JMy24cZsilDTO4e_0024231jlXk), Vector2.zero);
		smethod_5(smethod_14((Component)this), bool_0: true);
	}

	public void Cancel()
	{
		if (MLuyB7Q_0024QroOtOholke2aiQ.Color != YQchHFKnioVlxqspaiQkP8M && fG6WzjHolrbm0dCESUu_0024K7U != null)
		{
			fG6WzjHolrbm0dCESUu_0024K7U(YQchHFKnioVlxqspaiQkP8M);
		}
		Close();
	}

	public void Close()
	{
		fG6WzjHolrbm0dCESUu_0024K7U = null;
		smethod_5(smethod_14((Component)this), bool_0: false);
	}

	protected override void RefreshSkin()
	{
		smethod_15((Graphic)JMy24cZsilDTO4e_0024231jlXk, base.Skin.WindowColor);
		is6VHld6IP1rkQkoC_0024n4qRlvt6RakWw_kNtK_vIZOhWN.SetHeight(base.Skin.LineHeight);
		dVcX6SuSx_0024kGqLGokraly5ZuQetKibReHJR7Lg2nEiaD.SetHeight(Mathf.Min(45f, (float)base.Skin.LineHeight * 1.5f));
		MfAjFp6vXMLJBo17EdMRfCY.SetSkinText(base.Skin);
		rHC2zbpeT7masu5PZdniorY.Skin = base.Skin;
		O2lnRat3gvbMa3FtRJ6VkrI.Skin = base.Skin;
		fMZA_0024H91t1Sn3TCKNgnycf4.Skin = base.Skin;
		iTbALEAFgZNZCPsWsUGdqmg.Skin = base.Skin;
		AKwP8uWl1WlZSC4gKmG7L2A.SetSkinButton(base.Skin);
		rmWSiGwV1eJSSIxHbaQx6co.SetSkinButton(base.Skin);
	}

	private void S1HBCwykHDfYcMIdeqh59_WJnt5GKdmHMaifbrKiZsAs(Color32 color32_0)
	{
		rHC2zbpeT7masu5PZdniorY.Text = color32_0.r.ToString();
		O2lnRat3gvbMa3FtRJ6VkrI.Text = color32_0.g.ToString();
		fMZA_0024H91t1Sn3TCKNgnycf4.Text = color32_0.b.ToString();
		iTbALEAFgZNZCPsWsUGdqmg.Text = color32_0.a.ToString();
		GyhUab8AR_0JCTzTRPgaHGs.Color = color32_0;
		if (fG6WzjHolrbm0dCESUu_0024K7U != null)
		{
			fG6WzjHolrbm0dCESUu_0024K7U(color32_0);
		}
	}

	private void Fd31xv4TlP58kqxKjDf8tX4(float float_0)
	{
		iTbALEAFgZNZCPsWsUGdqmg.Text = ((int)(float_0 * 255f)).ToString();
		MLuyB7Q_0024QroOtOholke2aiQ.Alpha = float_0;
		Color color = MLuyB7Q_0024QroOtOholke2aiQ.Color;
		color.a = float_0;
		if (fG6WzjHolrbm0dCESUu_0024K7U != null)
		{
			fG6WzjHolrbm0dCESUu_0024K7U(color);
		}
	}

	private bool HVd_5B0LJR_0024VEhmiAyEoJjw(BoundInputField boundInputField_0, string string_0)
	{
		if (byte.TryParse(string_0, out var result))
		{
			Color32 color = MLuyB7Q_0024QroOtOholke2aiQ.Color;
			if (!smethod_16((UnityEngine.Object)boundInputField_0, (UnityEngine.Object)rHC2zbpeT7masu5PZdniorY))
			{
				if (smethod_16((UnityEngine.Object)boundInputField_0, (UnityEngine.Object)O2lnRat3gvbMa3FtRJ6VkrI))
				{
					color.g = result;
				}
				else if (!smethod_16((UnityEngine.Object)boundInputField_0, (UnityEngine.Object)fMZA_0024H91t1Sn3TCKNgnycf4))
				{
					color.a = result;
					GyhUab8AR_0JCTzTRPgaHGs.Value = (float)(int)result / 255f;
				}
				else
				{
					color.b = result;
				}
			}
			else
			{
				color.r = result;
			}
			GyhUab8AR_0JCTzTRPgaHGs.Color = color;
			MLuyB7Q_0024QroOtOholke2aiQ.PickColor(color);
			return true;
		}
		return false;
	}

	public static void DestroyInstance()
	{
		if (smethod_3((UnityEngine.Object)K1hFj5zLZuQMBpgW_EkwUtE))
		{
			RuntimeInspectorUtils.IgnoredTransformsInHierarchy.Remove(smethod_6((Component)K1hFj5zLZuQMBpgW_EkwUtE));
			smethod_17((UnityEngine.Object)K1hFj5zLZuQMBpgW_EkwUtE);
			K1hFj5zLZuQMBpgW_EkwUtE = null;
		}
	}

	internal static bool smethod_3(UnityEngine.Object object_0)
	{
		return object_0;
	}

	internal static GameObject smethod_4(Component component_0)
	{
		return component_0.gameObject;
	}

	internal static void smethod_5(GameObject gameObject_0, bool bool_0)
	{
		gameObject_0.SetActive(bool_0);
	}

	internal static Transform smethod_6(Component component_0)
	{
		return component_0.transform;
	}

	internal static Button.ButtonClickedEvent smethod_7(Button button_0)
	{
		return button_0.onClick;
	}

	internal static void smethod_8(UnityEvent unityEvent_0, UnityAction unityAction_0)
	{
		unityEvent_0.AddListener(unityAction_0);
	}

	internal static Delegate smethod_9(Delegate delegate_0, Delegate delegate_1)
	{
		return Delegate.Combine(delegate_0, delegate_1);
	}

	internal static bool smethod_10(UnityEngine.Object object_0, UnityEngine.Object object_1)
	{
		return object_0 != object_1;
	}

	internal static int smethod_11(Canvas canvas_0)
	{
		return canvas_0.sortingOrder;
	}

	internal static void smethod_12(Canvas canvas_0, int int_0)
	{
		canvas_0.sortingOrder = int_0;
	}

	internal static void smethod_13(RectTransform rectTransform_0, Vector2 vector2_0)
	{
		rectTransform_0.anchoredPosition = vector2_0;
	}

	internal static GameObject smethod_14(Component component_0)
	{
		return component_0.gameObject;
	}

	internal static void smethod_15(Graphic graphic_0, Color color_0)
	{
		graphic_0.color = color_0;
	}

	internal static bool smethod_16(UnityEngine.Object object_0, UnityEngine.Object object_1)
	{
		return object_0 == object_1;
	}

	internal static void smethod_17(UnityEngine.Object object_0)
	{
		UnityEngine.Object.Destroy(object_0);
	}
}
