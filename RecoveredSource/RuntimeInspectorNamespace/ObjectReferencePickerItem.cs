using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RuntimeInspectorNamespace;

public class ObjectReferencePickerItem : RecycledListItem
{
	[SerializeField]
	private Image slJt0vtJTZ_pZ4HFn1Pm0w0;

	[SerializeField]
	private RawImage _0024VRbZ6hkSQPfTPSDLEX3EJ4;

	private LayoutElement gc_tGrqtaA00qUxj5PFKpl5JptBlELdB4_4XBRPEstA4;

	[SerializeField]
	private Text J3kcnqTppfn65xEOeDDjNxUKAdNJ_dce5qDbVOJEzrOE;

	[CompilerGenerated]
	private Object V5NSqIr_00249bmQiSjI2X51jaQIGHCCcHreBMtT8iXTMrXw;

	private int tez8QKQVeFGVS4AMHMsbzyw;

	private UISkin E58c_5PzPLk6LleLXcBTp_0024M;

	private bool Q5KAiFtbXERFusj8Dg41uJo;

	public Object Reference
	{
		[CompilerGenerated]
		get
		{
			return V5NSqIr_00249bmQiSjI2X51jaQIGHCCcHreBMtT8iXTMrXw;
		}
		[CompilerGenerated]
		private set
		{
			V5NSqIr_00249bmQiSjI2X51jaQIGHCCcHreBMtT8iXTMrXw = value;
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
			if (smethod_0((Object)E58c_5PzPLk6LleLXcBTp_0024M, (Object)value) || tez8QKQVeFGVS4AMHMsbzyw != E58c_5PzPLk6LleLXcBTp_0024M.Version)
			{
				E58c_5PzPLk6LleLXcBTp_0024M = value;
				((RectTransform)smethod_1((Component)this)).sizeDelta = new Vector2(0f, Skin.LineHeight);
				int num = Mathf.Max(5, Skin.LineHeight - 7);
				gc_tGrqtaA00qUxj5PFKpl5JptBlELdB4_4XBRPEstA4.SetWidth(num);
				gc_tGrqtaA00qUxj5PFKpl5JptBlELdB4_4XBRPEstA4.SetHeight(num);
				J3kcnqTppfn65xEOeDDjNxUKAdNJ_dce5qDbVOJEzrOE.SetSkinText(E58c_5PzPLk6LleLXcBTp_0024M);
				IsSelected = Q5KAiFtbXERFusj8Dg41uJo;
			}
		}
	}

	public bool IsSelected
	{
		get
		{
			return Q5KAiFtbXERFusj8Dg41uJo;
		}
		set
		{
			Q5KAiFtbXERFusj8Dg41uJo = value;
			if (Q5KAiFtbXERFusj8Dg41uJo)
			{
				smethod_2((Graphic)slJt0vtJTZ_pZ4HFn1Pm0w0, Skin.SelectedItemBackgroundColor);
				smethod_2((Graphic)J3kcnqTppfn65xEOeDDjNxUKAdNJ_dce5qDbVOJEzrOE, Skin.SelectedItemTextColor);
			}
			else
			{
				smethod_2((Graphic)slJt0vtJTZ_pZ4HFn1Pm0w0, Color.clear);
				smethod_2((Graphic)J3kcnqTppfn65xEOeDDjNxUKAdNJ_dce5qDbVOJEzrOE, Skin.TextColor);
			}
		}
	}

	private void LfaiZbNwRELPQoddbWEUzjg()
	{
		gc_tGrqtaA00qUxj5PFKpl5JptBlELdB4_4XBRPEstA4 = _0024VRbZ6hkSQPfTPSDLEX3EJ4.GetComponent<LayoutElement>();
		GetComponent<PointerEventListener>().PointerClick += delegate
		{
			OnClick();
		};
	}

	public void SetContent(Object reference)
	{
		Reference = reference;
		smethod_3(J3kcnqTppfn65xEOeDDjNxUKAdNJ_dce5qDbVOJEzrOE, reference.GetNameWithType());
		Texture texture = reference.GetTexture();
		if (!smethod_0((Object)texture, (Object)null))
		{
			smethod_5(smethod_4((Component)_0024VRbZ6hkSQPfTPSDLEX3EJ4), bool_0: false);
			return;
		}
		smethod_5(smethod_4((Component)_0024VRbZ6hkSQPfTPSDLEX3EJ4), bool_0: true);
		smethod_6(_0024VRbZ6hkSQPfTPSDLEX3EJ4, texture);
	}

	[CompilerGenerated]
	private void IPc8Xu1G4iHi_WKZNH49Fk8(PointerEventData pointerEventData_0)
	{
		OnClick();
	}

	internal static bool smethod_0(Object object_1, Object object_2)
	{
		return object_1 != object_2;
	}

	internal static Transform smethod_1(Component component_0)
	{
		return component_0.transform;
	}

	internal static void smethod_2(Graphic graphic_0, Color color_0)
	{
		graphic_0.color = color_0;
	}

	internal static void smethod_3(Text text_0, string string_0)
	{
		text_0.text = string_0;
	}

	internal static GameObject smethod_4(Component component_0)
	{
		return component_0.gameObject;
	}

	internal static void smethod_5(GameObject gameObject_0, bool bool_0)
	{
		gameObject_0.SetActive(bool_0);
	}

	internal static void smethod_6(RawImage rawImage_0, Texture texture_0)
	{
		rawImage_0.texture = texture_0;
	}
}
