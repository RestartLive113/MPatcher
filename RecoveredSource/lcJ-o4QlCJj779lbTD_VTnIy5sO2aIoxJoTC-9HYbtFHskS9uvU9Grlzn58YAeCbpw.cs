using System;
using UnityEngine;
using UnityEngine.UI;

internal class lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw : WidgetController
{
	private Action<lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw> g2nxBfEELbYmP1Ar9f9qNEE;

	private Action<lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw> QigxkrZelYMYF7luo5IboUVz05adanJkLmitm4Ztswrs;

	private Button Q3Vx2ibNO1ITP7vpGaErLcI;

	internal string pZEKY5TzLd4S3z2lXESoRnw
	{
		get
		{
			return smethod_2(smethod_1(smethod_0((Component)this), global::_003CModule_003E.smethod_28<string>(2878620755u)).GetComponent<Text>());
		}
		set
		{
			smethod_3(smethod_1(smethod_0((Component)this), global::_003CModule_003E.smethod_25<string>(3406436984u)).GetComponent<Text>(), value);
		}
	}

	internal bool FLSdXom6uNTfN55f5nxTsH8
	{
		get
		{
			return smethod_4((Selectable)Button_0);
		}
		set
		{
			smethod_5((Selectable)Button_0, value);
		}
	}

	internal Button Button_0
	{
		get
		{
			if (smethod_6((UnityEngine.Object)Q3Vx2ibNO1ITP7vpGaErLcI, (UnityEngine.Object)null))
			{
				Q3Vx2ibNO1ITP7vpGaErLcI = GetComponent<Button>();
			}
			return Q3Vx2ibNO1ITP7vpGaErLcI;
		}
	}

	internal void DmPZGWxJ26_0024f_0024QOvQiqpmW8(Vector2 size)
	{
		RectTransform rectTransform_ = (RectTransform)smethod_7((Component)Button_0);
		smethod_9(rectTransform_, smethod_8(rectTransform_) + size);
	}

	internal void UzVS61irgJn5Pnqwx0lThng(Vector2 size)
	{
		smethod_9((RectTransform)smethod_7((Component)Button_0), size);
	}

	internal void t2iJT_tBPyB6QRMBLAdXYUs(Action<lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw> onClick, Action<lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw> onMouseDown = null)
	{
		if (onClick != null)
		{
			g2nxBfEELbYmP1Ar9f9qNEE = onClick;
		}
		if (onMouseDown != null)
		{
			QigxkrZelYMYF7luo5IboUVz05adanJkLmitm4Ztswrs = onMouseDown;
		}
	}

	protected virtual void slAohyJbgvnQR_0024kdjOrtJBQ()
	{
		if (g2nxBfEELbYmP1Ar9f9qNEE != null && FLSdXom6uNTfN55f5nxTsH8)
		{
			g2nxBfEELbYmP1Ar9f9qNEE(this);
			smethod_10(global::_003CModule_003E.smethod_28<string>(572856781u), 1f);
		}
	}

	protected virtual void EKmj2wjaAOQ9HFvKkrJJ_4U()
	{
		if (QigxkrZelYMYF7luo5IboUVz05adanJkLmitm4Ztswrs != null)
		{
			QigxkrZelYMYF7luo5IboUVz05adanJkLmitm4Ztswrs(this);
		}
	}

	protected virtual void muLYqcKag6qbwQ4qIBhj4mY()
	{
		base.CJLBGKELNHP();
	}

	protected virtual void OvP2yXRDJ_0wb2Bkvo533Fw()
	{
		base.AGEIMACGIEL();
	}

	internal static Transform smethod_0(Component component_0)
	{
		return component_0.transform;
	}

	internal static Transform smethod_1(Transform transform_0, string string_0)
	{
		return transform_0.Find(string_0);
	}

	internal static string smethod_2(Text text_0)
	{
		return text_0.text;
	}

	internal static void smethod_3(Text text_0, string string_0)
	{
		text_0.text = string_0;
	}

	internal static bool smethod_4(Selectable selectable_0)
	{
		return selectable_0.IsInteractable();
	}

	internal static void smethod_5(Selectable selectable_0, bool bool_0)
	{
		selectable_0.interactable = bool_0;
	}

	internal static bool smethod_6(UnityEngine.Object object_0, UnityEngine.Object object_1)
	{
		return object_0 == object_1;
	}

	internal static Transform smethod_7(Component component_0)
	{
		return component_0.transform;
	}

	internal static Vector2 smethod_8(RectTransform rectTransform_0)
	{
		return rectTransform_0.sizeDelta;
	}

	internal static void smethod_9(RectTransform rectTransform_0, Vector2 vector2_0)
	{
		rectTransform_0.sizeDelta = vector2_0;
	}

	internal static void smethod_10(string string_0, float float_0)
	{
		KEFHJCGICLE.HNAHBIMJDCB(string_0, float_0);
	}
}
