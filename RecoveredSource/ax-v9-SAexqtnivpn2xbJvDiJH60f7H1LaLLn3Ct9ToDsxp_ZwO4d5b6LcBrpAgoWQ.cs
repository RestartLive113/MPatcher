using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

internal class ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ : InputController
{
	private InputField TsqxtNnwIAzrvbAfzgyDZcg;

	private Action<string> yPPeBcyvSmYHWzzzZ1siB3A;

	private Action vHgAsJv_tuXdjJek__gZzvI;

	internal Action<string> JNMaMdWdD3fzh8iVBUwSGz4
	{
		private get
		{
			return yPPeBcyvSmYHWzzzZ1siB3A;
		}
		set
		{
			yPPeBcyvSmYHWzzzZ1siB3A = value;
		}
	}

	internal Action WKq1TUYmKJJXRZQEHbwVXPg
	{
		private get
		{
			return vHgAsJv_tuXdjJek__gZzvI;
		}
		set
		{
			vHgAsJv_tuXdjJek__gZzvI = value;
		}
	}

	internal bool FLSdXom6uNTfN55f5nxTsH8
	{
		get
		{
			return smethod_1((Selectable)GetComponent<InputField>());
		}
		set
		{
			smethod_0((Selectable)GetComponent<InputField>(), value);
		}
	}

	internal string pZEKY5TzLd4S3z2lXESoRnw
	{
		get
		{
			return smethod_2(BSdnl9DYm6Rd4cVhJ555c_A);
		}
		set
		{
			smethod_3(BSdnl9DYm6Rd4cVhJ555c_A, value);
		}
	}

	internal int lj_0024TxDPY_0024JjlwYIawpd1eys
	{
		get
		{
			return smethod_4(BSdnl9DYm6Rd4cVhJ555c_A);
		}
		set
		{
			smethod_5(BSdnl9DYm6Rd4cVhJ555c_A, value);
		}
	}

	internal InputField BSdnl9DYm6Rd4cVhJ555c_A
	{
		get
		{
			if (smethod_11((UnityEngine.Object)TsqxtNnwIAzrvbAfzgyDZcg, (UnityEngine.Object)null))
			{
				TsqxtNnwIAzrvbAfzgyDZcg = GetComponent<InputField>();
			}
			return TsqxtNnwIAzrvbAfzgyDZcg;
		}
	}

	internal void DmPZGWxJ26_0024f_0024QOvQiqpmW8(Vector2 size, bool sizetext = false)
	{
		RectTransform rectTransform_ = (RectTransform)smethod_6((Component)BSdnl9DYm6Rd4cVhJ555c_A);
		smethod_8(rectTransform_, smethod_7(rectTransform_) + size);
		GameObject gameObject = smethod_9((Component)this).smethod_0(global::_003CModule_003E.smethod_27<string>(3040756738u));
		if (sizetext && smethod_10((UnityEngine.Object)gameObject, (UnityEngine.Object)null))
		{
			smethod_8(gameObject.GetComponent<RectTransform>(), smethod_7((RectTransform)smethod_6((Component)BSdnl9DYm6Rd4cVhJ555c_A)));
		}
	}

	internal void UzVS61irgJn5Pnqwx0lThng(Vector2 size, bool sizetext = false)
	{
		smethod_8((RectTransform)smethod_6((Component)BSdnl9DYm6Rd4cVhJ555c_A), size);
		GameObject gameObject = smethod_9((Component)this).smethod_0(global::_003CModule_003E.smethod_29<string>(3709603843u));
		if (sizetext && smethod_10((UnityEngine.Object)gameObject, (UnityEngine.Object)null))
		{
			smethod_8(gameObject.GetComponent<RectTransform>(), smethod_7((RectTransform)smethod_6((Component)BSdnl9DYm6Rd4cVhJ555c_A)));
		}
	}

	internal void TJgoS_qAksEywwB0VyKhSGw()
	{
		smethod_12((Selectable)BSdnl9DYm6Rd4cVhJ555c_A);
		smethod_13(OAMBIPGGPEM(), smethod_9((Component)this));
	}

	protected override void HGLOKJCPMEH()
	{
		smethod_13(OAMBIPGGPEM(), smethod_9((Component)this));
	}

	public void Start()
	{
		smethod_14(BSdnl9DYm6Rd4cVhJ555c_A).AddListener(delegate(string val)
		{
			if (JNMaMdWdD3fzh8iVBUwSGz4 != null)
			{
				JNMaMdWdD3fzh8iVBUwSGz4(val);
			}
		});
	}

	protected override void Update()
	{
		base.Update();
		if (vHgAsJv_tuXdjJek__gZzvI != null)
		{
			vHgAsJv_tuXdjJek__gZzvI();
		}
	}

	[CompilerGenerated]
	private void xVI5bSVLZbQ7YbOGUl3l9bE(string val)
	{
		if (JNMaMdWdD3fzh8iVBUwSGz4 != null)
		{
			JNMaMdWdD3fzh8iVBUwSGz4(val);
		}
	}

	internal static void smethod_0(Selectable selectable_0, bool bool_0)
	{
		selectable_0.interactable = bool_0;
	}

	internal static bool smethod_1(Selectable selectable_0)
	{
		return selectable_0.IsInteractable();
	}

	internal static string smethod_2(InputField inputField_0)
	{
		return inputField_0.text;
	}

	internal static void smethod_3(InputField inputField_0, string string_0)
	{
		inputField_0.text = string_0;
	}

	internal static int smethod_4(InputField inputField_0)
	{
		return inputField_0.characterLimit;
	}

	internal static void smethod_5(InputField inputField_0, int int_0)
	{
		inputField_0.characterLimit = int_0;
	}

	internal static Transform smethod_6(Component component_0)
	{
		return component_0.transform;
	}

	internal static Vector2 smethod_7(RectTransform rectTransform_0)
	{
		return rectTransform_0.sizeDelta;
	}

	internal static void smethod_8(RectTransform rectTransform_0, Vector2 vector2_0)
	{
		rectTransform_0.sizeDelta = vector2_0;
	}

	internal static GameObject smethod_9(Component component_0)
	{
		return component_0.gameObject;
	}

	internal static bool smethod_10(UnityEngine.Object object_0, UnityEngine.Object object_1)
	{
		return object_0 != object_1;
	}

	internal static bool smethod_11(UnityEngine.Object object_0, UnityEngine.Object object_1)
	{
		return object_0 == object_1;
	}

	internal static void smethod_12(Selectable selectable_0)
	{
		selectable_0.Select();
	}

	internal static void smethod_13(SceneMan sceneMan_0, GameObject gameObject_0)
	{
		sceneMan_0.OnInput(gameObject_0);
	}

	internal static InputField.OnChangeEvent smethod_14(InputField inputField_0)
	{
		return inputField_0.onValueChanged;
	}
}
