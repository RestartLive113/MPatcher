using System.IO;
using UnityEngine;
using UnityEngine.UI;

internal class GOXSJR8XXxmzrImPcK3VZJ81Hx__l3nItnVvDSQtAc23FYbfvMk7gPDWmMjAs9idsccXk_wGlMiqRF8xDlD5818 : SceneMan
{
	private GameObject Wm_00245p1ah1SRX7mXPdeUsjXo;

	private static readonly int R7Aal8NoLm4njwrRBoSSSfc = 170;

	private static readonly int SHOGlpGln7qCBrpM_0024fDgi_8 = 140;

	private static readonly int Vhc1ua3e6V_0024uH2u7ym3m_00249s = R7Aal8NoLm4njwrRBoSSSfc + 10;

	private static readonly int VxImP_0024bRSeWB8I_lVc_TsRs = -SHOGlpGln7qCBrpM_0024fDgi_8 - 10;

	private static readonly int qMdrrrZloEsBZzlVx46g9dI = 3;

	private static readonly int qQMTBbKw9IITnfxqBIiALtk = 5;

	private int int_0;

	private int int_1;

	protected virtual void LfaiZbNwRELPQoddbWEUzjg()
	{
		base.Awake();
		CanvasScaler canvasScaler = smethod_0(global::_003CModule_003E.smethod_29<string>(618767629u)).AddComponent<CanvasScaler>();
		smethod_1(canvasScaler, CanvasScaler.ScaleMode.ScaleWithScreenSize);
		canvasScaler.referenceResolution = new Vector2(800f, 600f);
		Class17.DMoImhPC2jNUt2tgmSezeT_IK_0024XK5Df9PmexG_CaKb1R(global::_003CModule_003E.smethod_28<string>(2408910336u), (object)SceneMan.JFAOKFIDAGK, Object.Instantiate(vcvWq48of1PpnetEh2AY81tkRkpDe4qOaCwTttO9vBY6.ts9_0024hOMpHope_fSUdeN3MnM(global::_003CModule_003E.smethod_26<string>(440647706u))).GetComponent<Text>());
		GameObject obj = new GameObject(global::_003CModule_003E.smethod_29<string>(3372776965u));
		obj.transform.parent = GameObject.Find(global::_003CModule_003E.smethod_25<string>(806627754u)).transform;
		Image image = obj.AddComponent<Image>();
		Texture2D texture2D = new Texture2D(1, 1);
		texture2D.LoadImage(File.ReadAllBytes(global::_003CModule_003E.smethod_27<string>(3450328129u)));
		image.sprite = Sprite.Create(texture2D, new Rect(0f, 0f, 320f, 120f), new Vector2(0.5f, 0.5f));
		image.type = Image.Type.Tiled;
		image.preserveAspect = false;
		image.fillCenter = true;
		image.fillMethod = Image.FillMethod.Radial360;
		image.rectTransform.anchorMin = Vector2.zero;
		image.rectTransform.anchorMax = Vector2.one;
		image.rectTransform.anchoredPosition = Vector2.zero;
		image.color = new Color(0.251f, 0.251f, 0.251f, 1f);
		GameObject gameObject = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.qSsYOsEQb7x452a9Y45dVEk(new Vector2(0f, 0f), new Vector2(160f, 550f), GameObject.Find(global::_003CModule_003E.smethod_25<string>(806627754u)).transform);
		gameObject.GetComponent<RectTransform>().anchorMin = new Vector2(0f, 0.5f);
		gameObject.GetComponent<RectTransform>().anchorMax = new Vector2(0f, 0.5f);
		gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(60f, 0f);
		Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.P3tLroX6fcPfQha_0024JdbpYXg(global::_003CModule_003E.smethod_29<string>(2204338790u), new Vector2(0f, -210f), global::_003CModule_003E.smethod_27<string>(3636450829u), delegate
		{
			ATCyo_0024qpPFqNSISN7PgDzuPM7bZZWP2fcNoTkefIy5A2WLvcWT1w9UCw3Yq7lMwlug.YMAh_TshgArfNEGhpCaDjcg(global::_003CModule_003E.smethod_25<string>(3356786789u), bool_0: false);
		}, gameObject.transform, 24).UzVS61irgJn5Pnqwx0lThng(new Vector2(100f, 40f));
		ToggleGroup toggleGroup = GameObject.Find(global::_003CModule_003E.smethod_29<string>(618767629u)).AddComponent<ToggleGroup>();
		toggleGroup.allowSwitchOff = false;
		Control0 control = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.uEsWMK_pFkCY_0024M5zt8zLsQk(global::_003CModule_003E.smethod_27<string>(2864007807u), new Vector2(0f, 200f), global::_003CModule_003E.smethod_29<string>(3697275006u), gameObject.transform, resetGroup: true, null, null, toggleGroup);
		Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.uEsWMK_pFkCY_0024M5zt8zLsQk(global::_003CModule_003E.smethod_29<string>(148775818u), new Vector2(0f, 160f), global::_003CModule_003E.smethod_27<string>(1612281924u), gameObject.transform, resetGroup: true, null, null, toggleGroup);
		control.hLxnG9Hq33zU_YUsu_00240_zak = true;
		Wm_00245p1ah1SRX7mXPdeUsjXo = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.qSsYOsEQb7x452a9Y45dVEk(new Vector2(0f, 0f), new Vector2(600f, 500f), GameObject.Find(global::_003CModule_003E.smethod_25<string>(806627754u)).transform);
		method_0(global::_003CModule_003E.smethod_28<string>(3395417298u));
		method_0(global::_003CModule_003E.smethod_29<string>(4162429736u));
		method_0(global::_003CModule_003E.smethod_29<string>(613930548u));
		method_0(global::_003CModule_003E.smethod_28<string>(3987380758u));
	}

	private void method_0(string name)
	{
		GameObject gameObject = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.qSsYOsEQb7x452a9Y45dVEk(new Vector2(-180 + Vhc1ua3e6V_0024uH2u7ym3m_00249s * int_0, 150 + VxImP_0024bRSeWB8I_lVc_TsRs * int_1), new Vector2(R7Aal8NoLm4njwrRBoSSSfc, SHOGlpGln7qCBrpM_0024fDgi_8), Wm_00245p1ah1SRX7mXPdeUsjXo.transform);
		Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.YU_pwpP3pKH76IHZTea_SXk(global::_003CModule_003E.smethod_28<string>(3623141154u), new Vector2(60f, -50f), name, gameObject.transform, rmOutline: true, 15, FontStyle.Normal, TextAnchor.LowerCenter, Color.white).GetComponent<Text>();
		int_0++;
		if (int_0 >= qMdrrrZloEsBZzlVx46g9dI)
		{
			int_0 = 0;
			int_1++;
		}
	}

	internal static GameObject smethod_0(string string_0)
	{
		return GameObject.Find(string_0);
	}

	internal static void smethod_1(CanvasScaler canvasScaler_0, CanvasScaler.ScaleMode scaleMode_0)
	{
		canvasScaler_0.uiScaleMode = scaleMode_0;
	}
}
