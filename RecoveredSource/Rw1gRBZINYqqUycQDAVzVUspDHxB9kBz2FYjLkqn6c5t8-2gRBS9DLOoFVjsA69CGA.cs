using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

internal class Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA
{
	internal enum objectType
	{
		list,
		panel,
		button,
		toggle,
		input,
		label,
		slider,
		pickBlockButton
	}

	internal static GameObject smethod_0(objectType typ)
	{
		string name = smethod_1().name;
		if (name != null)
		{
			switch (name.Length)
			{
			case 4:
				if (name == global::_003CModule_003E.smethod_26<string>(1306246642u))
				{
					switch (typ)
					{
					case objectType.panel:
						return GameObject.Find(global::_003CModule_003E.smethod_29<string>(2528769364u));
					case objectType.button:
						return GameObject.Find(global::_003CModule_003E.smethod_26<string>(3206187340u));
					case objectType.toggle:
						return GameObject.Find(global::_003CModule_003E.smethod_27<string>(862174272u)).smethod_0(global::_003CModule_003E.smethod_29<string>(2106799297u));
					case objectType.input:
						return GameObject.Find(global::_003CModule_003E.smethod_27<string>(1710114701u));
					case objectType.label:
						return GameObject.Find(global::_003CModule_003E.smethod_27<string>(2854803123u));
					}
				}
				break;
			case 5:
				switch (name[0])
				{
				case 'L':
					if (name == global::_003CModule_003E.smethod_29<string>(2151210097u))
					{
						switch (typ)
						{
						case objectType.button:
							return GameObject.Find(global::_003CModule_003E.smethod_29<string>(2346221616u));
						case objectType.input:
							return GameObject.Find(global::_003CModule_003E.smethod_29<string>(2896452068u));
						}
					}
					break;
				case 'B':
					if (name == global::_003CModule_003E.smethod_27<string>(3514760917u))
					{
						switch (typ)
						{
						case objectType.list:
							return GameObject.Find(global::_003CModule_003E.smethod_26<string>(3103631283u)).smethod_0(global::_003CModule_003E.smethod_26<string>(198330828u));
						case objectType.panel:
							return SceneMan.JFAOKFIDAGK.GetPNL(global::_003CModule_003E.smethod_25<string>(4291362529u));
						case objectType.button:
							return GameObject.Find(global::_003CModule_003E.smethod_28<string>(3214927773u)).smethod_0(global::_003CModule_003E.smethod_27<string>(2277856570u));
						case objectType.toggle:
							return GameObject.Find(global::_003CModule_003E.smethod_25<string>(806627754u)).smethod_0(global::_003CModule_003E.smethod_29<string>(59886751u));
						case objectType.input:
							return GameObject.Find(global::_003CModule_003E.smethod_28<string>(3214927773u)).smethod_0(global::_003CModule_003E.smethod_25<string>(1667939088u));
						case objectType.label:
							return GameObject.Find(global::_003CModule_003E.smethod_29<string>(618767629u)).smethod_0(global::_003CModule_003E.smethod_29<string>(1552822967u));
						case objectType.slider:
							return GameObject.Find(global::_003CModule_003E.smethod_25<string>(806627754u)).smethod_0(global::_003CModule_003E.smethod_26<string>(1564727648u));
						case objectType.pickBlockButton:
							return GameObject.Find(global::_003CModule_003E.smethod_28<string>(3214927773u)).smethod_0(global::_003CModule_003E.smethod_26<string>(204601245u));
						}
					}
					break;
				}
				break;
			case 6:
				if (name == global::_003CModule_003E.smethod_28<string>(3214038531u))
				{
					switch (typ)
					{
					case objectType.list:
						return GameObject.Find(global::_003CModule_003E.smethod_28<string>(3214927773u)).smethod_0(global::_003CModule_003E.smethod_26<string>(1364909780u));
					case objectType.panel:
						return SceneMan.JFAOKFIDAGK.GetPNL(global::_003CModule_003E.smethod_27<string>(4101926664u));
					case objectType.button:
						return GameObject.Find(global::_003CModule_003E.smethod_26<string>(2711101440u));
					case objectType.toggle:
						return GameObject.Find(global::_003CModule_003E.smethod_25<string>(550996857u));
					case objectType.input:
						return vcvWq48of1PpnetEh2AY81tkRkpDe4qOaCwTttO9vBY6.ts9_0024hOMpHope_fSUdeN3MnM(global::_003CModule_003E.smethod_26<string>(681711370u));
					case objectType.label:
						return GameObject.Find(global::_003CModule_003E.smethod_27<string>(862174272u)).smethod_0(global::_003CModule_003E.smethod_26<string>(3008181119u));
					case objectType.slider:
						return GameObject.Find(global::_003CModule_003E.smethod_25<string>(806627754u)).smethod_0(global::_003CModule_003E.smethod_26<string>(1362680395u));
					}
				}
				break;
			case 7:
				if (!(name == global::_003CModule_003E.smethod_27<string>(3421699567u)) || typ != objectType.button)
				{
					break;
				}
				return GameObject.Find(global::_003CModule_003E.smethod_27<string>(862174272u)).smethod_0(global::_003CModule_003E.smethod_26<string>(887799655u));
			case 8:
				switch (name[0])
				{
				case 'P':
					if (name == global::_003CModule_003E.smethod_28<string>(1333418371u))
					{
						switch (typ)
						{
						case objectType.slider:
							return GameObject.Find(global::_003CModule_003E.smethod_25<string>(806627754u)).smethod_0(global::_003CModule_003E.smethod_26<string>(2331253126u));
						case objectType.panel:
							return GameObject.Find(global::_003CModule_003E.smethod_29<string>(618767629u)).smethod_0(global::_003CModule_003E.smethod_25<string>(2228851077u));
						}
					}
					break;
				case 'W':
					if (name == global::_003CModule_003E.smethod_29<string>(2973012298u))
					{
						switch (typ)
						{
						case objectType.list:
							return vcvWq48of1PpnetEh2AY81tkRkpDe4qOaCwTttO9vBY6.ts9_0024hOMpHope_fSUdeN3MnM(global::_003CModule_003E.smethod_25<string>(449236493u));
						case objectType.panel:
							return GameObject.Find(global::_003CModule_003E.smethod_27<string>(862174272u)).smethod_0(global::_003CModule_003E.smethod_27<string>(2421881852u));
						case objectType.button:
							return GameObject.Find(global::_003CModule_003E.smethod_29<string>(618767629u)).smethod_0(global::_003CModule_003E.smethod_27<string>(4259928057u));
						case objectType.toggle:
							return GameObject.Find(global::_003CModule_003E.smethod_28<string>(3214927773u)).smethod_0(global::_003CModule_003E.smethod_28<string>(2970866312u));
						case objectType.input:
							return GameObject.Find(global::_003CModule_003E.smethod_28<string>(3214927773u)).smethod_0(global::_003CModule_003E.smethod_26<string>(2129205873u));
						case objectType.label:
							return vcvWq48of1PpnetEh2AY81tkRkpDe4qOaCwTttO9vBY6.ts9_0024hOMpHope_fSUdeN3MnM(global::_003CModule_003E.smethod_25<string>(754318954u));
						}
					}
					break;
				}
				break;
			case 9:
				switch (name[3])
				{
				case 's':
					if (name == global::_003CModule_003E.smethod_25<string>(553854093u))
					{
						switch (typ)
						{
						case objectType.list:
							return GameObject.Find(global::_003CModule_003E.smethod_28<string>(3214927773u)).smethod_0(global::_003CModule_003E.smethod_26<string>(483705149u));
						case objectType.panel:
							return GameObject.Find(global::_003CModule_003E.smethod_27<string>(862174272u)).smethod_0(global::_003CModule_003E.smethod_26<string>(2533300379u));
						case objectType.button:
							return GameObject.Find(global::_003CModule_003E.smethod_25<string>(806627754u)).smethod_0(global::_003CModule_003E.smethod_26<string>(887799655u));
						case objectType.toggle:
							return GameObject.Find(global::_003CModule_003E.smethod_26<string>(3103631283u)).smethod_0(global::_003CModule_003E.smethod_25<string>(126651555u));
						case objectType.input:
							return GameObject.Find(global::_003CModule_003E.smethod_29<string>(618767629u)).smethod_0(global::_003CModule_003E.smethod_27<string>(2733958636u));
						case objectType.label:
							return vcvWq48of1PpnetEh2AY81tkRkpDe4qOaCwTttO9vBY6.ts9_0024hOMpHope_fSUdeN3MnM(global::_003CModule_003E.smethod_25<string>(754318954u));
						case objectType.slider:
							return GameObject.Find(global::_003CModule_003E.smethod_26<string>(3103631283u)).smethod_0(global::_003CModule_003E.smethod_28<string>(3623437568u));
						}
					}
					break;
				case 'f':
					if (name == global::_003CModule_003E.smethod_27<string>(318796793u))
					{
						switch (typ)
						{
						case objectType.toggle:
							return GameObject.Find(global::_003CModule_003E.smethod_29<string>(633616207u));
						case objectType.input:
							return GameObject.Find(global::_003CModule_003E.smethod_27<string>(1337869301u));
						case objectType.slider:
							return GameObject.Find(global::_003CModule_003E.smethod_26<string>(3103631283u)).smethod_0(global::_003CModule_003E.smethod_26<string>(3335218974u));
						}
					}
					break;
				}
				break;
			}
		}
		if (SceneMan.JFAOKFIDAGK as Arena != null)
		{
			switch (typ)
			{
			case objectType.list:
				return GameObject.Find(global::_003CModule_003E.smethod_29<string>(618767629u)).smethod_0(global::_003CModule_003E.smethod_29<string>(2916205194u));
			case objectType.button:
				return GameObject.Find(global::_003CModule_003E.smethod_27<string>(862174272u)).smethod_0(global::_003CModule_003E.smethod_27<string>(4259928057u));
			case objectType.toggle:
				return GameObject.Find(global::_003CModule_003E.smethod_25<string>(806627754u)).smethod_0(global::_003CModule_003E.smethod_27<string>(1375195292u));
			}
		}
		switch (typ)
		{
		case objectType.panel:
			return vcvWq48of1PpnetEh2AY81tkRkpDe4qOaCwTttO9vBY6.ts9_0024hOMpHope_fSUdeN3MnM(global::_003CModule_003E.smethod_29<string>(8051662u));
		case objectType.button:
			return vcvWq48of1PpnetEh2AY81tkRkpDe4qOaCwTttO9vBY6.ts9_0024hOMpHope_fSUdeN3MnM(global::_003CModule_003E.smethod_25<string>(2551436015u));
		case objectType.toggle:
			return vcvWq48of1PpnetEh2AY81tkRkpDe4qOaCwTttO9vBY6.ts9_0024hOMpHope_fSUdeN3MnM(global::_003CModule_003E.smethod_28<string>(1028600907u));
		default:
			mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_29<string>(3662673302u) + typ.ToString() + global::_003CModule_003E.smethod_29<string>(860642222u) + SceneManager.GetActiveScene().name, bool_0: true);
			return null;
		case objectType.label:
			return vcvWq48of1PpnetEh2AY81tkRkpDe4qOaCwTttO9vBY6.ts9_0024hOMpHope_fSUdeN3MnM(global::_003CModule_003E.smethod_26<string>(2808363251u));
		}
	}

	internal static ListController nN2N4qjnQLwFOaONUPeRAdg(string name, string label, Vector3 pos, string[] choices, Transform parent = null)
	{
		GameObject gameObject = smethod_0(objectType.list);
		string string_ = smethod_3(smethod_2((UnityEngine.Object)gameObject), 4);
		if (smethod_4((UnityEngine.Object)parent, (UnityEngine.Object)null))
		{
			parent = smethod_6(smethod_5(gameObject));
		}
		gameObject = UnityEngine.Object.Instantiate(gameObject, parent, worldPositionStays: true);
		smethod_8((UnityEngine.Object)gameObject, smethod_7(global::_003CModule_003E.smethod_26<string>(323321430u), name));
		smethod_9(smethod_5(gameObject), pos);
		smethod_10(gameObject, bool_0: true);
		GameObject gameObject2 = smethod_12((Component)smethod_11(smethod_5(gameObject), smethod_7(global::_003CModule_003E.smethod_26<string>(2456243728u), string_)));
		smethod_8((UnityEngine.Object)gameObject2, smethod_7(global::_003CModule_003E.smethod_29<string>(1715685056u), name));
		GameObject gameObject3 = gameObject.smethod_0(smethod_7(global::_003CModule_003E.smethod_29<string>(4236402758u), string_));
		if (smethod_13((UnityEngine.Object)gameObject3, (UnityEngine.Object)null))
		{
			smethod_8((UnityEngine.Object)gameObject3, smethod_7(global::_003CModule_003E.smethod_26<string>(1131510442u), name));
			smethod_14(gameObject3.GetComponent<Text>(), label);
		}
		GameObject gameObject_ = smethod_12((Component)smethod_11(smethod_5(gameObject), global::_003CModule_003E.smethod_28<string>(4126416025u)));
		for (int i = 0; i < smethod_17(smethod_5(gameObject_)); i++)
		{
			smethod_16((UnityEngine.Object)smethod_12((Component)smethod_15(smethod_5(gameObject_), i)));
		}
		ListController component = gameObject2.GetComponent<ListController>();
		smethod_18(component, choices[0], choices);
		return component;
	}

	internal static SliderController tjX92nnz9Ioc_0024izMOOLq4fI(string name, string label, Vector3 pos, int min, int max, int currvalue, UnityAction<float> onValueChanged = null, Transform parent = null)
	{
		GameObject gameObject = smethod_0(objectType.slider);
		string string_ = smethod_3(smethod_2((UnityEngine.Object)gameObject), 4);
		if (smethod_4((UnityEngine.Object)parent, (UnityEngine.Object)null))
		{
			parent = smethod_6(smethod_5(gameObject));
		}
		gameObject = UnityEngine.Object.Instantiate(gameObject, parent, worldPositionStays: true);
		smethod_8((UnityEngine.Object)gameObject, smethod_7(global::_003CModule_003E.smethod_25<string>(1882453297u), name));
		smethod_9(smethod_5(gameObject), pos);
		smethod_10(gameObject, bool_0: true);
		GameObject gameObject2 = smethod_19(gameObject.smethod_0(smethod_7(global::_003CModule_003E.smethod_26<string>(2770740749u), string_)));
		smethod_8((UnityEngine.Object)gameObject2, smethod_7(global::_003CModule_003E.smethod_27<string>(1249579378u), name));
		smethod_8((UnityEngine.Object)gameObject.smethod_0(smethod_7(global::_003CModule_003E.smethod_28<string>(542701090u), string_)), smethod_7(global::_003CModule_003E.smethod_26<string>(4101744452u), name));
		GameObject gameObject3 = gameObject.smethod_0(smethod_7(global::_003CModule_003E.smethod_26<string>(1131510442u), string_));
		if (smethod_13((UnityEngine.Object)gameObject3, (UnityEngine.Object)null))
		{
			if (smethod_20(label))
			{
				smethod_16((UnityEngine.Object)gameObject3);
			}
			else
			{
				smethod_8((UnityEngine.Object)gameObject3, smethod_7(global::_003CModule_003E.smethod_28<string>(724820892u), name));
				smethod_14(gameObject3.GetComponent<Text>(), label);
			}
		}
		SliderController component = gameObject2.GetComponent<SliderController>();
		Slider slider = H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.H1Pr8mmPslEwoIU22reQcXE(component);
		Text text = Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<Text>(global::_003CModule_003E.smethod_25<string>(2397366851u), component);
		if (smethod_4((UnityEngine.Object)text, (UnityEngine.Object)null))
		{
			text = smethod_15(smethod_15(smethod_11(smethod_21((Component)component), global::_003CModule_003E.smethod_26<string>(2777011166u)), 0), 0).GetComponent<Text>();
		}
		if (smethod_13((UnityEngine.Object)text, (UnityEngine.Object)null))
		{
			text.text = min.ToString();
		}
		Class17.DMoImhPC2jNUt2tgmSezeT_IK_0024XK5Df9PmexG_CaKb1R(global::_003CModule_003E.smethod_27<string>(1514787317u), (object)component, min.ToString());
		slider.minValue = min;
		slider.maxValue = max;
		if (text != null)
		{
			text.text = currvalue.ToString();
		}
		Class17.DMoImhPC2jNUt2tgmSezeT_IK_0024XK5Df9PmexG_CaKb1R(global::_003CModule_003E.smethod_26<string>(3097778604u), (object)component, currvalue.ToString());
		slider.value = currvalue;
		if (onValueChanged != null)
		{
			slider.onValueChanged.AddListener(onValueChanged);
		}
		return component;
	}

	internal static ListController nN2N4qjnQLwFOaONUPeRAdg(string name, Vector3 pos, string[] choices, Transform parent = null)
	{
		GameObject gameObject = smethod_0(objectType.list);
		string string_ = smethod_3(smethod_2((UnityEngine.Object)gameObject), 4);
		if (smethod_4((UnityEngine.Object)parent, (UnityEngine.Object)null))
		{
			parent = smethod_6(smethod_5(gameObject));
		}
		gameObject = UnityEngine.Object.Instantiate(gameObject, parent, worldPositionStays: true);
		smethod_8((UnityEngine.Object)gameObject, smethod_7(global::_003CModule_003E.smethod_27<string>(2101107639u), name));
		smethod_9(smethod_5(gameObject), pos);
		smethod_10(gameObject, bool_0: true);
		GameObject gameObject2 = smethod_12((Component)smethod_11(smethod_5(gameObject), smethod_7(global::_003CModule_003E.smethod_28<string>(906940694u), string_)));
		smethod_8((UnityEngine.Object)gameObject2, smethod_7(global::_003CModule_003E.smethod_27<string>(1854478153u), name));
		smethod_12((Component)smethod_11(smethod_5(gameObject), global::_003CModule_003E.smethod_25<string>(501346662u)));
		ListController component = gameObject2.GetComponent<ListController>();
		smethod_18(component, choices[0], choices);
		return component;
	}

	internal static GameObject qSsYOsEQb7x452a9Y45dVEk(Vector3 pos, Vector2 size, Transform parent = null)
	{
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(smethod_22(global::_003CModule_003E.smethod_28<string>(2363899110u), (object)pos, (object)size, (object)parent), bool_0: true);
		GameObject gameObject = smethod_0(objectType.panel);
		if (smethod_4((UnityEngine.Object)parent, (UnityEngine.Object)null))
		{
			parent = smethod_6(smethod_5(gameObject));
		}
		GameObject gameObject2 = UnityEngine.Object.Instantiate(gameObject, parent, worldPositionStays: true);
		IEnumerator enumerator = smethod_23(smethod_5(gameObject2));
		try
		{
			while (smethod_25(enumerator))
			{
				smethod_16((UnityEngine.Object)smethod_12((Component)(Transform)smethod_24(enumerator)));
			}
		}
		finally
		{
			if (enumerator is IDisposable idisposable_)
			{
				smethod_26(idisposable_);
			}
		}
		smethod_9(smethod_5(gameObject2), pos);
		smethod_10(gameObject2, bool_0: true);
		smethod_27((RectTransform)smethod_5(gameObject2), size);
		if (smethod_28((UnityEngine.Object)gameObject2.GetComponent<ToggleGroup>()))
		{
			smethod_16((UnityEngine.Object)gameObject2.GetComponent<ToggleGroup>());
		}
		return gameObject2;
	}

	internal static lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw P3tLroX6fcPfQha_0024JdbpYXg(string name, Vector3 localPos, string text, Action<lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw> onClick, Transform parent = null, int fontsize = -1, Action<lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw> onMouseDown = null, bool pickBlock = false)
	{
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(smethod_29(global::_003CModule_003E.smethod_27<string>(1221627156u), new object[4] { name, localPos, text, parent }), bool_0: true);
		GameObject gameObject = smethod_0((!pickBlock) ? objectType.button : objectType.pickBlockButton);
		if (smethod_4((UnityEngine.Object)parent, (UnityEngine.Object)null))
		{
			parent = smethod_6(smethod_5(gameObject));
		}
		gameObject = UnityEngine.Object.Instantiate(gameObject, parent, worldPositionStays: true);
		smethod_8((UnityEngine.Object)gameObject, smethod_7(global::_003CModule_003E.smethod_26<string>(1730546308u), name));
		smethod_9(smethod_5(gameObject), localPos);
		smethod_14(smethod_11(smethod_5(gameObject), global::_003CModule_003E.smethod_27<string>(3040756738u)).GetComponent<Text>(), text);
		smethod_16((UnityEngine.Object)gameObject.GetComponent<ButtonController>());
		lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw2 = gameObject.AddComponent<lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw>();
		lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw2.t2iJT_tBPyB6QRMBLAdXYUs(onClick, onMouseDown);
		smethod_10(smethod_12((Component)lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw2), bool_0: true);
		if (fontsize != -1)
		{
			smethod_30(smethod_11(smethod_21((Component)lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw2), global::_003CModule_003E.smethod_26<string>(2312859613u)).GetComponent<Text>(), fontsize);
		}
		return lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw2;
	}

	internal static Ox2iDlZ3c70_00246RoQSfv0JsUA9Zr8fowUBRLdiyCjXAIPYXycTbHLX39SoZBma4Hq2A ZM_0024v0fYSxVbN4TDn9D55Ev7u_0024qEN6_e_xmFvC96KpSiq(Control0 tgl)
	{
		GameObject gameObject = smethod_12((Component)tgl);
		Image component = gameObject.smethod_0(global::_003CModule_003E.smethod_25<string>(3404809735u)).GetComponent<Image>();
		smethod_8((UnityEngine.Object)smethod_12((Component)component), global::_003CModule_003E.smethod_25<string>(233095035u));
		smethod_32(smethod_31((Graphic)component));
		RectTransform rectTransform = smethod_33((Graphic)component);
		rectTransform.pivot = new Vector2(0f, 1f);
		rectTransform.anchorMin = new Vector2(0f, 1f);
		rectTransform.anchorMax = new Vector2(0f, 1f);
		rectTransform.anchoredPosition = Vector3.zero;
		rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, ((RectTransform)gameObject.transform).sizeDelta.x);
		rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, ((RectTransform)gameObject.transform).sizeDelta.y);
		GameObject gameObject2 = UnityEngine.Object.Instantiate(component.gameObject, component.transform.parent);
		UnityEngine.Object.Destroy(gameObject2.GetComponent<Image>());
		UnityEngine.Object.Destroy(gameObject2.GetComponent<CanvasRenderer>());
		gameObject2.AddComponent<RectMask2D>();
		component.transform.SetParent(gameObject2.transform);
		Ox2iDlZ3c70_00246RoQSfv0JsUA9Zr8fowUBRLdiyCjXAIPYXycTbHLX39SoZBma4Hq2A ox2iDlZ3c70_00246RoQSfv0JsUA9Zr8fowUBRLdiyCjXAIPYXycTbHLX39SoZBma4Hq2A = gameObject.AddComponent<Ox2iDlZ3c70_00246RoQSfv0JsUA9Zr8fowUBRLdiyCjXAIPYXycTbHLX39SoZBma4Hq2A>();
		ox2iDlZ3c70_00246RoQSfv0JsUA9Zr8fowUBRLdiyCjXAIPYXycTbHLX39SoZBma4Hq2A.method_0(gameObject.transform.Find(global::_003CModule_003E.smethod_29<string>(1055586274u)).GetComponent<Text>(), rectTransform, (RectTransform)gameObject2.transform);
		return ox2iDlZ3c70_00246RoQSfv0JsUA9Zr8fowUBRLdiyCjXAIPYXycTbHLX39SoZBma4Hq2A;
	}

	internal static Ox2iDlZ3c70_00246RoQSfv0JsUA9Zr8fowUBRLdiyCjXAIPYXycTbHLX39SoZBma4Hq2A sK2j6t4cq7U9_0024R2QSBY5HWA(string name, Vector3 pos, string text, Transform parent = null)
	{
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(smethod_29(global::_003CModule_003E.smethod_29<string>(914929585u), new object[4] { name, pos, text, parent }), bool_0: true);
		GameObject gameObject = smethod_0(objectType.toggle);
		if (smethod_4((UnityEngine.Object)parent, (UnityEngine.Object)null))
		{
			parent = smethod_6(smethod_5(gameObject));
		}
		gameObject = UnityEngine.Object.Instantiate(gameObject, parent, worldPositionStays: true);
		smethod_8((UnityEngine.Object)gameObject, smethod_7(global::_003CModule_003E.smethod_25<string>(530239882u), name));
		smethod_9(smethod_5(gameObject), pos);
		smethod_14(smethod_11(smethod_5(gameObject), global::_003CModule_003E.smethod_26<string>(769079470u)).GetComponent<Text>(), text);
		smethod_16((UnityEngine.Object)gameObject.GetComponent<ToggleController>());
		smethod_16((UnityEngine.Object)gameObject.GetComponent<Toggle>());
		gameObject.smethod_0(global::_003CModule_003E.smethod_28<string>(922092643u)).GetComponent<Image>().color = new Color(0.2509804f, 0.5019608f, 1f);
		Image component = gameObject.smethod_0(global::_003CModule_003E.smethod_26<string>(1319622952u)).GetComponent<Image>();
		component.gameObject.name = global::_003CModule_003E.smethod_27<string>(2719982525u);
		component.canvasRenderer.Clear();
		RectTransform rectTransform = component.rectTransform;
		rectTransform.pivot = new Vector2(0f, 1f);
		rectTransform.anchorMin = new Vector2(0f, 1f);
		rectTransform.anchorMax = new Vector2(0f, 1f);
		rectTransform.anchoredPosition = Vector3.zero;
		rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, ((RectTransform)gameObject.transform).sizeDelta.x);
		rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, ((RectTransform)gameObject.transform).sizeDelta.y);
		GameObject gameObject2 = UnityEngine.Object.Instantiate(component.gameObject, component.transform.parent);
		UnityEngine.Object.Destroy(gameObject2.GetComponent<Image>());
		UnityEngine.Object.Destroy(gameObject2.GetComponent<CanvasRenderer>());
		gameObject2.AddComponent<RectMask2D>();
		component.transform.SetParent(gameObject2.transform);
		Ox2iDlZ3c70_00246RoQSfv0JsUA9Zr8fowUBRLdiyCjXAIPYXycTbHLX39SoZBma4Hq2A ox2iDlZ3c70_00246RoQSfv0JsUA9Zr8fowUBRLdiyCjXAIPYXycTbHLX39SoZBma4Hq2A = gameObject.AddComponent<Ox2iDlZ3c70_00246RoQSfv0JsUA9Zr8fowUBRLdiyCjXAIPYXycTbHLX39SoZBma4Hq2A>();
		ox2iDlZ3c70_00246RoQSfv0JsUA9Zr8fowUBRLdiyCjXAIPYXycTbHLX39SoZBma4Hq2A.method_0(gameObject.transform.Find(global::_003CModule_003E.smethod_29<string>(1055586274u)).GetComponent<Text>(), rectTransform, (RectTransform)gameObject2.transform);
		return ox2iDlZ3c70_00246RoQSfv0JsUA9Zr8fowUBRLdiyCjXAIPYXycTbHLX39SoZBma4Hq2A;
	}

	internal static Control0 uEsWMK_pFkCY_0024M5zt8zLsQk(string name, Vector3 pos, string text, Transform parent = null, bool resetGroup = true, Action<bool> onClick = null, Action onUnToggle = null, ToggleGroup group = null)
	{
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(smethod_29(global::_003CModule_003E.smethod_29<string>(1207345663u), new object[4] { name, pos, text, parent }), bool_0: true);
		GameObject gameObject = smethod_0(objectType.toggle);
		if (smethod_4((UnityEngine.Object)parent, (UnityEngine.Object)null))
		{
			parent = smethod_6(smethod_5(gameObject));
		}
		gameObject = UnityEngine.Object.Instantiate(gameObject, parent, worldPositionStays: true);
		smethod_8((UnityEngine.Object)gameObject, smethod_7(global::_003CModule_003E.smethod_28<string>(1741631752u), name));
		smethod_9(smethod_5(gameObject), pos);
		smethod_14(smethod_11(smethod_5(gameObject), global::_003CModule_003E.smethod_26<string>(769079470u)).GetComponent<Text>(), text);
		smethod_16((UnityEngine.Object)gameObject.GetComponent<ToggleController>());
		Control0 control = gameObject.AddComponent<Control0>();
		control.t2iJT_tBPyB6QRMBLAdXYUs(onClick);
		control.method_0(onUnToggle);
		if (resetGroup)
		{
			smethod_34(control.Tz4h_68oANQj5xAU0vtoknA, (ToggleGroup)null);
		}
		if (smethod_13((UnityEngine.Object)group, (UnityEngine.Object)null))
		{
			smethod_34(control.Tz4h_68oANQj5xAU0vtoknA, group);
		}
		smethod_10(smethod_12((Component)control), bool_0: true);
		return control;
	}

	internal static ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ bqYYMQnP2SDqYH85wmN_0024evI(string name, Vector3 pos, string label, string placeholder, Transform parent = null)
	{
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(smethod_29(global::_003CModule_003E.smethod_25<string>(3819391543u), new object[4] { name, pos, placeholder, parent }), bool_0: true);
		string string_ = smethod_2((UnityEngine.Object)smethod_0(objectType.input));
		GameObject gameObject = smethod_0(objectType.input);
		if (smethod_4((UnityEngine.Object)parent, (UnityEngine.Object)null))
		{
			parent = smethod_6(smethod_5(gameObject));
		}
		gameObject = UnityEngine.Object.Instantiate(gameObject, parent, worldPositionStays: true);
		smethod_8((UnityEngine.Object)gameObject, smethod_7(global::_003CModule_003E.smethod_29<string>(2602809853u), name));
		smethod_9(smethod_5(gameObject), pos);
		smethod_10(gameObject, bool_0: true);
		smethod_14(smethod_11(smethod_5(gameObject), global::_003CModule_003E.smethod_26<string>(988126327u)).GetComponent<Text>(), placeholder);
		Transform transform = smethod_11(smethod_5(gameObject), smethod_7(global::_003CModule_003E.smethod_28<string>(724820892u), smethod_35(string_, global::_003CModule_003E.smethod_26<string>(692022819u), "")));
		if (smethod_13((UnityEngine.Object)transform, (UnityEngine.Object)null))
		{
			smethod_8((UnityEngine.Object)transform, smethod_7(global::_003CModule_003E.smethod_29<string>(4236402758u), name));
			smethod_14(transform.GetComponent<Text>(), label);
		}
		smethod_16((UnityEngine.Object)gameObject.GetComponent<InputController>());
		ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ obj = gameObject.AddComponent<ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ>();
		obj.pZEKY5TzLd4S3z2lXESoRnw = string.Empty;
		return obj;
	}

	internal static ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ bqYYMQnP2SDqYH85wmN_0024evI(string name, Vector3 pos, string placeholder, Transform parent = null, string text = null)
	{
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(smethod_29(global::_003CModule_003E.smethod_29<string>(1531843704u), new object[4] { name, pos, placeholder, parent }), bool_0: true);
		GameObject gameObject = smethod_0(objectType.input);
		if (smethod_4((UnityEngine.Object)parent, (UnityEngine.Object)null))
		{
			parent = smethod_6(smethod_5(gameObject));
		}
		gameObject = UnityEngine.Object.Instantiate(gameObject, parent, worldPositionStays: true);
		smethod_8((UnityEngine.Object)gameObject, smethod_7(global::_003CModule_003E.smethod_29<string>(2602809853u), name));
		smethod_9(smethod_5(gameObject), pos);
		if (smethod_13((UnityEngine.Object)smethod_11(smethod_5(gameObject), global::_003CModule_003E.smethod_29<string>(1935354249u)), (UnityEngine.Object)null))
		{
			smethod_14(smethod_11(smethod_5(gameObject), global::_003CModule_003E.smethod_28<string>(2696500953u)).GetComponent<Text>(), placeholder);
		}
		smethod_16((UnityEngine.Object)gameObject.GetComponent<InputController>());
		ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ2 = gameObject.AddComponent<ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ>();
		if (text != null)
		{
			ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ2.pZEKY5TzLd4S3z2lXESoRnw = text;
		}
		else
		{
			ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ2.pZEKY5TzLd4S3z2lXESoRnw = "";
		}
		return ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ2;
	}

	internal static GameObject YU_pwpP3pKH76IHZTea_SXk(string name, Vector3 localPos, string text, Transform parent = null, bool rmOutline = false, int fontSize = -1, FontStyle style = FontStyle.Normal, TextAnchor alignment = TextAnchor.LowerLeft, Color textColor = default(Color), bool resizeRect = false, Vector2 resizeRectTo = default(Vector2))
	{
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(smethod_29(global::_003CModule_003E.smethod_26<string>(2995640285u), new object[4] { name, localPos, text, parent }), bool_0: true);
		GameObject gameObject = smethod_0(objectType.label);
		if (smethod_4((UnityEngine.Object)parent, (UnityEngine.Object)null))
		{
			parent = smethod_6(smethod_5(gameObject));
		}
		gameObject = UnityEngine.Object.Instantiate(gameObject, parent, worldPositionStays: true);
		smethod_8((UnityEngine.Object)gameObject, smethod_7(global::_003CModule_003E.smethod_29<string>(4236402758u), name));
		smethod_9(smethod_5(gameObject), localPos);
		Text component = gameObject.GetComponent<Text>();
		if (fontSize != -1)
		{
			smethod_30(component, fontSize);
		}
		if (textColor != default(Color))
		{
			smethod_36((Graphic)component, textColor);
		}
		smethod_37(component, style);
		smethod_38(component, alignment);
		smethod_14(component, text);
		if (resizeRect)
		{
			smethod_27(component.GetComponent<RectTransform>(), resizeRectTo);
		}
		if (rmOutline && smethod_13((UnityEngine.Object)gameObject.GetComponent<Outline>(), (UnityEngine.Object)null))
		{
			smethod_16((UnityEngine.Object)gameObject.GetComponent<Outline>());
		}
		smethod_10(gameObject, bool_0: true);
		return gameObject;
	}

	internal static Scene smethod_1()
	{
		return SceneManager.GetActiveScene();
	}

	internal static string smethod_2(UnityEngine.Object object_0)
	{
		return object_0.name;
	}

	internal static string smethod_3(string string_0, int int_0)
	{
		return string_0.Substring(int_0);
	}

	internal static bool smethod_4(UnityEngine.Object object_0, UnityEngine.Object object_1)
	{
		return object_0 == object_1;
	}

	internal static Transform smethod_5(GameObject gameObject_0)
	{
		return gameObject_0.transform;
	}

	internal static Transform smethod_6(Transform transform_0)
	{
		return transform_0.parent;
	}

	internal static string smethod_7(string string_0, string string_1)
	{
		return string_0 + string_1;
	}

	internal static void smethod_8(UnityEngine.Object object_0, string string_0)
	{
		object_0.name = string_0;
	}

	internal static void smethod_9(Transform transform_0, Vector3 vector3_0)
	{
		transform_0.localPosition = vector3_0;
	}

	internal static void smethod_10(GameObject gameObject_0, bool bool_0)
	{
		gameObject_0.SetActive(bool_0);
	}

	internal static Transform smethod_11(Transform transform_0, string string_0)
	{
		return transform_0.Find(string_0);
	}

	internal static GameObject smethod_12(Component component_0)
	{
		return component_0.gameObject;
	}

	internal static bool smethod_13(UnityEngine.Object object_0, UnityEngine.Object object_1)
	{
		return object_0 != object_1;
	}

	internal static void smethod_14(Text text_0, string string_0)
	{
		text_0.text = string_0;
	}

	internal static Transform smethod_15(Transform transform_0, int int_0)
	{
		return transform_0.GetChild(int_0);
	}

	internal static void smethod_16(UnityEngine.Object object_0)
	{
		UnityEngine.Object.Destroy(object_0);
	}

	internal static int smethod_17(Transform transform_0)
	{
		return transform_0.childCount;
	}

	internal static void smethod_18(ListController listController_0, string string_0, string[] string_1)
	{
		listController_0.SetItemsAndSelect(string_0, string_1);
	}

	internal static GameObject smethod_19(GameObject gameObject_0)
	{
		return gameObject_0.gameObject;
	}

	internal static bool smethod_20(string string_0)
	{
		return string.IsNullOrEmpty(string_0);
	}

	internal static Transform smethod_21(Component component_0)
	{
		return component_0.transform;
	}

	internal static string smethod_22(string string_0, object object_0, object object_1, object object_2)
	{
		return string.Format(string_0, object_0, object_1, object_2);
	}

	internal static IEnumerator smethod_23(Transform transform_0)
	{
		return transform_0.GetEnumerator();
	}

	internal static object smethod_24(IEnumerator ienumerator_0)
	{
		return ienumerator_0.Current;
	}

	internal static bool smethod_25(IEnumerator ienumerator_0)
	{
		return ienumerator_0.MoveNext();
	}

	internal static void smethod_26(IDisposable idisposable_0)
	{
		idisposable_0.Dispose();
	}

	internal static void smethod_27(RectTransform rectTransform_0, Vector2 vector2_0)
	{
		rectTransform_0.sizeDelta = vector2_0;
	}

	internal static bool smethod_28(UnityEngine.Object object_0)
	{
		return object_0;
	}

	internal static string smethod_29(string string_0, object[] object_0)
	{
		return string.Format(string_0, object_0);
	}

	internal static void smethod_30(Text text_0, int int_0)
	{
		text_0.fontSize = int_0;
	}

	internal static CanvasRenderer smethod_31(Graphic graphic_0)
	{
		return graphic_0.canvasRenderer;
	}

	internal static void smethod_32(CanvasRenderer canvasRenderer_0)
	{
		canvasRenderer_0.Clear();
	}

	internal static RectTransform smethod_33(Graphic graphic_0)
	{
		return graphic_0.rectTransform;
	}

	internal static void smethod_34(Toggle toggle_0, ToggleGroup toggleGroup_0)
	{
		toggle_0.group = toggleGroup_0;
	}

	internal static string smethod_35(string string_0, string string_1, string string_2)
	{
		return string_0.Replace(string_1, string_2);
	}

	internal static void smethod_36(Graphic graphic_0, Color color_0)
	{
		graphic_0.color = color_0;
	}

	internal static void smethod_37(Text text_0, FontStyle fontStyle_0)
	{
		text_0.fontStyle = fontStyle_0;
	}

	internal static void smethod_38(Text text_0, TextAnchor textAnchor_0)
	{
		text_0.alignment = textAnchor_0;
	}
}
