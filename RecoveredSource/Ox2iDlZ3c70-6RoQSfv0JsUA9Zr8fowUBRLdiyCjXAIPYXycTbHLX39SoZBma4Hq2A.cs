using UnityEngine;
using UnityEngine.UI;

internal class Ox2iDlZ3c70_00246RoQSfv0JsUA9Zr8fowUBRLdiyCjXAIPYXycTbHLX39SoZBma4Hq2A : MonoBehaviour
{
	private Text dLh9izc27wu0xesQ1MY43LI;

	private RectTransform B2a_BkbfjRHV13ibNrr4sPo;

	private RectTransform gx1_0024WRUHMmZ5V5XxxrONspU;

	private Vector2 bjHrAp91Pwl8_0024i82Vqfc8_0024o;

	private float xV9ndWp9WSDKaBbxO_00240ELZ0;

	internal void method_0(Text label, RectTransform checkmark, RectTransform mask)
	{
		dLh9izc27wu0xesQ1MY43LI = label;
		B2a_BkbfjRHV13ibNrr4sPo = checkmark;
		gx1_0024WRUHMmZ5V5XxxrONspU = mask;
		bjHrAp91Pwl8_0024i82Vqfc8_0024o = smethod_1((RectTransform)smethod_0((Component)this));
	}

	private void vYjVNpbF_0024bYW40eplk9_jdA(float pct)
	{
		xV9ndWp9WSDKaBbxO_00240ELZ0 = pct;
		ujUDYD_0024n1A2Ws_0024KaQhRDISc();
	}

	private void ujUDYD_0024n1A2Ws_0024KaQhRDISc()
	{
		smethod_2(gx1_0024WRUHMmZ5V5XxxrONspU, RectTransform.Axis.Horizontal, bjHrAp91Pwl8_0024i82Vqfc8_0024o.x * xV9ndWp9WSDKaBbxO_00240ELZ0);
		smethod_2(gx1_0024WRUHMmZ5V5XxxrONspU, RectTransform.Axis.Vertical, smethod_1(gx1_0024WRUHMmZ5V5XxxrONspU).y);
	}

	internal void method_1(float pct)
	{
		vYjVNpbF_0024bYW40eplk9_jdA(pct / 100f);
	}

	internal void method_2(float value, float total)
	{
		vYjVNpbF_0024bYW40eplk9_jdA(value / total);
	}

	internal void Uyxr04ltGE_xJflB_0024UyNu8w(string text)
	{
		smethod_3(dLh9izc27wu0xesQ1MY43LI, text);
	}

	internal void DmPZGWxJ26_0024f_0024QOvQiqpmW8(Vector2 size)
	{
		RectTransform rectTransform_ = (RectTransform)smethod_0((Component)this);
		smethod_4(rectTransform_, smethod_1(rectTransform_) + size);
		bjHrAp91Pwl8_0024i82Vqfc8_0024o += size;
		smethod_2(B2a_BkbfjRHV13ibNrr4sPo, RectTransform.Axis.Horizontal, bjHrAp91Pwl8_0024i82Vqfc8_0024o.x);
		smethod_2(B2a_BkbfjRHV13ibNrr4sPo, RectTransform.Axis.Vertical, bjHrAp91Pwl8_0024i82Vqfc8_0024o.y);
		ujUDYD_0024n1A2Ws_0024KaQhRDISc();
	}

	internal void UzVS61irgJn5Pnqwx0lThng(Vector2 size)
	{
		smethod_4((RectTransform)smethod_0((Component)this), size);
		bjHrAp91Pwl8_0024i82Vqfc8_0024o = size;
		smethod_2(B2a_BkbfjRHV13ibNrr4sPo, RectTransform.Axis.Horizontal, bjHrAp91Pwl8_0024i82Vqfc8_0024o.x);
		smethod_2(B2a_BkbfjRHV13ibNrr4sPo, RectTransform.Axis.Vertical, bjHrAp91Pwl8_0024i82Vqfc8_0024o.y);
		ujUDYD_0024n1A2Ws_0024KaQhRDISc();
	}

	internal static Transform smethod_0(Component component_0)
	{
		return component_0.transform;
	}

	internal static Vector2 smethod_1(RectTransform rectTransform_0)
	{
		return rectTransform_0.sizeDelta;
	}

	internal static void smethod_2(RectTransform rectTransform_0, RectTransform.Axis axis_0, float float_0)
	{
		rectTransform_0.SetSizeWithCurrentAnchors(axis_0, float_0);
	}

	internal static void smethod_3(Text text_0, string string_0)
	{
		text_0.text = string_0;
	}

	internal static void smethod_4(RectTransform rectTransform_0, Vector2 vector2_0)
	{
		rectTransform_0.sizeDelta = vector2_0;
	}
}
