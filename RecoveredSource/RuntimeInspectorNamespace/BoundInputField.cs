using System;
using UnityEngine;
using UnityEngine.UI;

namespace RuntimeInspectorNamespace;

public class BoundInputField : MonoBehaviour
{
	public delegate bool OnValueChangedDelegate(BoundInputField source, string input);

	private bool ELqGdI0DqLmFirmwZsUXVAM;

	private bool PowKlPG9sTskFdG_0024OGsi7L0 = true;

	private bool _3IW8PU526q_3BTA_0024H3RJEk;

	private InputField inputField_0;

	private Image oniZk6SbT1WxqXY2rc5cB7USaB76nLEyEBkNcbMIRkBd;

	[NonSerialized]
	public string DefaultEmptyValue = string.Empty;

	[NonSerialized]
	public bool CacheTextOnValueChange = true;

	private string XB2J68A4DDsuLUHihsd2wp0 = string.Empty;

	private int tez8QKQVeFGVS4AMHMsbzyw;

	private UISkin E58c_5PzPLk6LleLXcBTp_0024M;

	public OnValueChangedDelegate OnValueChanged;

	public OnValueChangedDelegate OnValueSubmitted;

	public InputField BackingField => inputField_0;

	public string Text
	{
		get
		{
			return smethod_0(inputField_0);
		}
		set
		{
			XB2J68A4DDsuLUHihsd2wp0 = value;
			if (!smethod_1(inputField_0))
			{
				PowKlPG9sTskFdG_0024OGsi7L0 = true;
				smethod_2(inputField_0, value);
				smethod_3((Graphic)oniZk6SbT1WxqXY2rc5cB7USaB76nLEyEBkNcbMIRkBd, Skin.InputFieldNormalBackgroundColor);
			}
		}
	}

	public UISkin Skin
	{
		get
		{
			return E58c_5PzPLk6LleLXcBTp_0024M;
		}
		set
		{
			if (smethod_4((UnityEngine.Object)E58c_5PzPLk6LleLXcBTp_0024M, (UnityEngine.Object)value) || tez8QKQVeFGVS4AMHMsbzyw != E58c_5PzPLk6LleLXcBTp_0024M.Version)
			{
				Initialize();
				E58c_5PzPLk6LleLXcBTp_0024M = value;
				tez8QKQVeFGVS4AMHMsbzyw = E58c_5PzPLk6LleLXcBTp_0024M.Version;
				smethod_5(inputField_0).SetSkinInputFieldText(E58c_5PzPLk6LleLXcBTp_0024M);
				smethod_3((Graphic)oniZk6SbT1WxqXY2rc5cB7USaB76nLEyEBkNcbMIRkBd, E58c_5PzPLk6LleLXcBTp_0024M.InputFieldNormalBackgroundColor);
				Text text = smethod_6(inputField_0) as Text;
				if (smethod_4((UnityEngine.Object)text, (UnityEngine.Object)null))
				{
					float a = smethod_7((Graphic)text).a;
					text.SetSkinInputFieldText(E58c_5PzPLk6LleLXcBTp_0024M);
					Color color_ = smethod_7((Graphic)text);
					color_.a = a;
					smethod_3((Graphic)text, color_);
				}
			}
		}
	}

	private void LfaiZbNwRELPQoddbWEUzjg()
	{
		Initialize();
	}

	public void Initialize()
	{
		if (ELqGdI0DqLmFirmwZsUXVAM)
		{
			return;
		}
		inputField_0 = GetComponent<InputField>();
		oniZk6SbT1WxqXY2rc5cB7USaB76nLEyEBkNcbMIRkBd = GetComponent<Image>();
		smethod_8(inputField_0).AddListener(delegate(string string_0)
		{
			if (smethod_1(inputField_0))
			{
				_3IW8PU526q_3BTA_0024H3RJEk = true;
				if (string_0 == null || smethod_10(string_0) == 0)
				{
					string_0 = DefaultEmptyValue;
				}
				if (OnValueChanged != null)
				{
					PowKlPG9sTskFdG_0024OGsi7L0 = OnValueChanged(this, string_0);
					if (PowKlPG9sTskFdG_0024OGsi7L0 && CacheTextOnValueChange)
					{
						XB2J68A4DDsuLUHihsd2wp0 = string_0;
					}
					smethod_3((Graphic)oniZk6SbT1WxqXY2rc5cB7USaB76nLEyEBkNcbMIRkBd, PowKlPG9sTskFdG_0024OGsi7L0 ? Skin.InputFieldNormalBackgroundColor : Skin.InputFieldInvalidBackgroundColor);
				}
			}
		});
		smethod_9(inputField_0).AddListener(MgM09VNRuzfF7rvOWAjmjzQES5gy8kkvTMaTQ1jAGGE7);
		ELqGdI0DqLmFirmwZsUXVAM = true;
	}

	private void _0024_0024VAzhNcshQxGDgq0K6S9q3ybLvQ812pLxgGSn20AFKb(string string_0)
	{
		if (!smethod_1(inputField_0))
		{
			return;
		}
		_3IW8PU526q_3BTA_0024H3RJEk = true;
		if (string_0 == null || smethod_10(string_0) == 0)
		{
			string_0 = DefaultEmptyValue;
		}
		if (OnValueChanged != null)
		{
			PowKlPG9sTskFdG_0024OGsi7L0 = OnValueChanged(this, string_0);
			if (PowKlPG9sTskFdG_0024OGsi7L0 && CacheTextOnValueChange)
			{
				XB2J68A4DDsuLUHihsd2wp0 = string_0;
			}
			smethod_3((Graphic)oniZk6SbT1WxqXY2rc5cB7USaB76nLEyEBkNcbMIRkBd, PowKlPG9sTskFdG_0024OGsi7L0 ? Skin.InputFieldNormalBackgroundColor : Skin.InputFieldInvalidBackgroundColor);
		}
	}

	private void MgM09VNRuzfF7rvOWAjmjzQES5gy8kkvTMaTQ1jAGGE7(string string_0)
	{
		smethod_3((Graphic)oniZk6SbT1WxqXY2rc5cB7USaB76nLEyEBkNcbMIRkBd, Skin.InputFieldNormalBackgroundColor);
		if (_3IW8PU526q_3BTA_0024H3RJEk)
		{
			_3IW8PU526q_3BTA_0024H3RJEk = false;
			if (string_0 == null || smethod_10(string_0) == 0)
			{
				string_0 = DefaultEmptyValue;
			}
			if (OnValueSubmitted == null)
			{
				if (PowKlPG9sTskFdG_0024OGsi7L0)
				{
					XB2J68A4DDsuLUHihsd2wp0 = string_0;
				}
			}
			else if (OnValueSubmitted(this, string_0))
			{
				XB2J68A4DDsuLUHihsd2wp0 = string_0;
			}
			smethod_2(inputField_0, XB2J68A4DDsuLUHihsd2wp0);
			PowKlPG9sTskFdG_0024OGsi7L0 = true;
		}
		else
		{
			smethod_2(inputField_0, XB2J68A4DDsuLUHihsd2wp0);
		}
	}

	internal static string smethod_0(InputField inputField_1)
	{
		return inputField_1.text;
	}

	internal static bool smethod_1(InputField inputField_1)
	{
		return inputField_1.isFocused;
	}

	internal static void smethod_2(InputField inputField_1, string string_0)
	{
		inputField_1.text = string_0;
	}

	internal static void smethod_3(Graphic graphic_0, Color color_0)
	{
		graphic_0.color = color_0;
	}

	internal static bool smethod_4(UnityEngine.Object object_0, UnityEngine.Object object_1)
	{
		return object_0 != object_1;
	}

	internal static Text smethod_5(InputField inputField_1)
	{
		return inputField_1.textComponent;
	}

	internal static Graphic smethod_6(InputField inputField_1)
	{
		return inputField_1.placeholder;
	}

	internal static Color smethod_7(Graphic graphic_0)
	{
		return graphic_0.color;
	}

	internal static InputField.OnChangeEvent smethod_8(InputField inputField_1)
	{
		return inputField_1.onValueChanged;
	}

	internal static InputField.SubmitEvent smethod_9(InputField inputField_1)
	{
		return inputField_1.onEndEdit;
	}

	internal static int smethod_10(string string_0)
	{
		return string_0.Length;
	}
}
