using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace RuntimeInspectorNamespace;

[RequireComponent(typeof(ScrollRect))]
public class RecycledListView : MonoBehaviour
{
	[SerializeField]
	private RectTransform Ooq85_E5xAGTvfSoVoMTYbYR8igVQe7CgOR1Nsr6Q12v;

	[SerializeField]
	private RectTransform Y6AcQ_gA6pnv3tp1jahZ10X9d3Kn2YWPyiZaAPnAXcfE;

	private float float_0;

	private float float_1;

	private float Yoic2pR3UCsomZSQXrgF46k;

	private float KwTsiHLcr1v8yd90DVS96oJ8faq53T9_0024O6NP_0024hxtxez_;

	private readonly Dictionary<int, RecycledListItem> vMK3GqZ5dLn66P6W8Bh1GjI = new Dictionary<int, RecycledListItem>();

	private readonly Stack<RecycledListItem> stack_0 = new Stack<RecycledListItem>();

	private IListViewAdapter WyZGbTAsrnDliiXt0TZpvkQ;

	private bool mKOWVtLlOSmnM7_0024LUNQUa88;

	private int Xjh1C7CjstaVmE6MsxJX5D0 = -1;

	private int it_0024To0c0FQMLNleke3XFIDYEF9Zz2kfPGi2S2Czfnr9c = -1;

	public float ViewportWidth => Yoic2pR3UCsomZSQXrgF46k;

	public float ViewportHeight => KwTsiHLcr1v8yd90DVS96oJ8faq53T9_0024O6NP_0024hxtxez_;

	private void sp_GCK595YHY1vrEPNGiSrQ()
	{
		smethod_0(GetComponent<ScrollRect>()).AddListener(delegate
		{
			B56GMpvh62uxz8MOHRu3XCO4pBrJ09b3wT8ivjZ5R9ar();
		});
	}

	private void method_0()
	{
		if (mKOWVtLlOSmnM7_0024LUNQUa88)
		{
			Vector2 size = smethod_1(Ooq85_E5xAGTvfSoVoMTYbYR8igVQe7CgOR1Nsr6Q12v).size;
			Yoic2pR3UCsomZSQXrgF46k = size.x;
			KwTsiHLcr1v8yd90DVS96oJ8faq53T9_0024O6NP_0024hxtxez_ = size.y;
			mKOWVtLlOSmnM7_0024LUNQUa88 = false;
			B56GMpvh62uxz8MOHRu3XCO4pBrJ09b3wT8ivjZ5R9ar();
		}
	}

	public void SetAdapter(IListViewAdapter adapter)
	{
		WyZGbTAsrnDliiXt0TZpvkQ = adapter;
		float_0 = adapter.ItemHeight;
		float_1 = 1f / float_0;
	}

	public void UpdateList(bool resetContentPosition = true)
	{
		if (resetContentPosition)
		{
			smethod_2(Y6AcQ_gA6pnv3tp1jahZ10X9d3Kn2YWPyiZaAPnAXcfE, Vector2.zero);
		}
		float y = Mathf.Max(1f, (float)WyZGbTAsrnDliiXt0TZpvkQ.Count * float_0);
		Y6AcQ_gA6pnv3tp1jahZ10X9d3Kn2YWPyiZaAPnAXcfE.sizeDelta = new Vector2(smethod_3(Y6AcQ_gA6pnv3tp1jahZ10X9d3Kn2YWPyiZaAPnAXcfE).x, y);
		Vector2 size = Ooq85_E5xAGTvfSoVoMTYbYR8igVQe7CgOR1Nsr6Q12v.rect.size;
		Yoic2pR3UCsomZSQXrgF46k = size.x;
		KwTsiHLcr1v8yd90DVS96oJ8faq53T9_0024O6NP_0024hxtxez_ = size.y;
		B56GMpvh62uxz8MOHRu3XCO4pBrJ09b3wT8ivjZ5R9ar(bool_0: true);
	}

	public void ResetList()
	{
		float_0 = WyZGbTAsrnDliiXt0TZpvkQ.ItemHeight;
		float_1 = 1f / float_0;
		if (Xjh1C7CjstaVmE6MsxJX5D0 > -1 && it_0024To0c0FQMLNleke3XFIDYEF9Zz2kfPGi2S2Czfnr9c > -1)
		{
			if (it_0024To0c0FQMLNleke3XFIDYEF9Zz2kfPGi2S2Czfnr9c > WyZGbTAsrnDliiXt0TZpvkQ.Count - 1)
			{
				it_0024To0c0FQMLNleke3XFIDYEF9Zz2kfPGi2S2Czfnr9c = WyZGbTAsrnDliiXt0TZpvkQ.Count - 1;
			}
			nWzfL2LNb1BhDMYHodpwcj6CihGTqjn_HsEb_I_0024OkJS_(Xjh1C7CjstaVmE6MsxJX5D0, it_0024To0c0FQMLNleke3XFIDYEF9Zz2kfPGi2S2Czfnr9c);
			Xjh1C7CjstaVmE6MsxJX5D0 = -1;
			it_0024To0c0FQMLNleke3XFIDYEF9Zz2kfPGi2S2Czfnr9c = -1;
		}
		UpdateList();
	}

	private void method_1()
	{
		mKOWVtLlOSmnM7_0024LUNQUa88 = true;
	}

	private void B56GMpvh62uxz8MOHRu3XCO4pBrJ09b3wT8ivjZ5R9ar(bool bool_0 = false)
	{
		if (WyZGbTAsrnDliiXt0TZpvkQ == null)
		{
			return;
		}
		if (WyZGbTAsrnDliiXt0TZpvkQ.Count <= 0)
		{
			if (Xjh1C7CjstaVmE6MsxJX5D0 != -1)
			{
				nWzfL2LNb1BhDMYHodpwcj6CihGTqjn_HsEb_I_0024OkJS_(Xjh1C7CjstaVmE6MsxJX5D0, it_0024To0c0FQMLNleke3XFIDYEF9Zz2kfPGi2S2Czfnr9c);
				Xjh1C7CjstaVmE6MsxJX5D0 = -1;
			}
			return;
		}
		float num = smethod_4(Y6AcQ_gA6pnv3tp1jahZ10X9d3Kn2YWPyiZaAPnAXcfE).y - 1f;
		int num2 = (int)(num * float_1);
		int num3 = (int)((num + KwTsiHLcr1v8yd90DVS96oJ8faq53T9_0024O6NP_0024hxtxez_ + 2f) * float_1);
		if (num2 < 0)
		{
			num2 = 0;
		}
		if (num3 > WyZGbTAsrnDliiXt0TZpvkQ.Count - 1)
		{
			num3 = WyZGbTAsrnDliiXt0TZpvkQ.Count - 1;
		}
		if (Xjh1C7CjstaVmE6MsxJX5D0 == -1)
		{
			bool_0 = true;
			Xjh1C7CjstaVmE6MsxJX5D0 = num2;
			it_0024To0c0FQMLNleke3XFIDYEF9Zz2kfPGi2S2Czfnr9c = num3;
			g83IAnsRf7PSYqkEgf65d33Ok1dZgSKWfC5ieg5CiQPb(num2, num3);
		}
		else
		{
			if (num3 >= Xjh1C7CjstaVmE6MsxJX5D0 && num2 <= it_0024To0c0FQMLNleke3XFIDYEF9Zz2kfPGi2S2Czfnr9c)
			{
				if (num2 > Xjh1C7CjstaVmE6MsxJX5D0)
				{
					nWzfL2LNb1BhDMYHodpwcj6CihGTqjn_HsEb_I_0024OkJS_(Xjh1C7CjstaVmE6MsxJX5D0, num2 - 1);
				}
				if (num3 < it_0024To0c0FQMLNleke3XFIDYEF9Zz2kfPGi2S2Czfnr9c)
				{
					nWzfL2LNb1BhDMYHodpwcj6CihGTqjn_HsEb_I_0024OkJS_(num3 + 1, it_0024To0c0FQMLNleke3XFIDYEF9Zz2kfPGi2S2Czfnr9c);
				}
				if (num2 < Xjh1C7CjstaVmE6MsxJX5D0)
				{
					g83IAnsRf7PSYqkEgf65d33Ok1dZgSKWfC5ieg5CiQPb(num2, Xjh1C7CjstaVmE6MsxJX5D0 - 1);
					if (!bool_0)
					{
						method_2(num2, Xjh1C7CjstaVmE6MsxJX5D0 - 1);
					}
				}
				if (num3 > it_0024To0c0FQMLNleke3XFIDYEF9Zz2kfPGi2S2Czfnr9c)
				{
					g83IAnsRf7PSYqkEgf65d33Ok1dZgSKWfC5ieg5CiQPb(it_0024To0c0FQMLNleke3XFIDYEF9Zz2kfPGi2S2Czfnr9c + 1, num3);
					if (!bool_0)
					{
						method_2(it_0024To0c0FQMLNleke3XFIDYEF9Zz2kfPGi2S2Czfnr9c + 1, num3);
					}
				}
			}
			else
			{
				bool_0 = true;
				nWzfL2LNb1BhDMYHodpwcj6CihGTqjn_HsEb_I_0024OkJS_(Xjh1C7CjstaVmE6MsxJX5D0, it_0024To0c0FQMLNleke3XFIDYEF9Zz2kfPGi2S2Czfnr9c);
				g83IAnsRf7PSYqkEgf65d33Ok1dZgSKWfC5ieg5CiQPb(num2, num3);
			}
			Xjh1C7CjstaVmE6MsxJX5D0 = num2;
			it_0024To0c0FQMLNleke3XFIDYEF9Zz2kfPGi2S2Czfnr9c = num3;
		}
		if (bool_0)
		{
			method_2(Xjh1C7CjstaVmE6MsxJX5D0, it_0024To0c0FQMLNleke3XFIDYEF9Zz2kfPGi2S2Czfnr9c);
		}
	}

	private void g83IAnsRf7PSYqkEgf65d33Ok1dZgSKWfC5ieg5CiQPb(int int_0, int int_1)
	{
		for (int i = int_0; i <= int_1; i++)
		{
			aPyD_0024fUxz36P34_XOJHU0TEfDLJ2TqsfCVv68xtF_NEa(i);
		}
	}

	private void aPyD_0024fUxz36P34_XOJHU0TEfDLJ2TqsfCVv68xtF_NEa(int int_0)
	{
		RecycledListItem recycledListItem;
		if (stack_0.Count > 0)
		{
			recycledListItem = stack_0.Pop();
			smethod_6(smethod_5((Component)recycledListItem), bool_0: true);
		}
		else
		{
			recycledListItem = WyZGbTAsrnDliiXt0TZpvkQ.CreateItem(Y6AcQ_gA6pnv3tp1jahZ10X9d3Kn2YWPyiZaAPnAXcfE);
			recycledListItem.Vfgh0wMhcRKegI8WSbN1g_w(WyZGbTAsrnDliiXt0TZpvkQ);
		}
		((RectTransform)smethod_7((Component)recycledListItem)).anchoredPosition = new Vector2(0f, (float)(-int_0) * float_0);
		vMK3GqZ5dLn66P6W8Bh1GjI[int_0] = recycledListItem;
	}

	private void nWzfL2LNb1BhDMYHodpwcj6CihGTqjn_HsEb_I_0024OkJS_(int int_0, int int_1)
	{
		for (int i = int_0; i <= int_1; i++)
		{
			RecycledListItem recycledListItem = vMK3GqZ5dLn66P6W8Bh1GjI[i];
			smethod_6(smethod_5((Component)recycledListItem), bool_0: false);
			stack_0.Push(recycledListItem);
		}
	}

	private void method_2(int int_0, int int_1)
	{
		for (int i = int_0; i <= int_1; i++)
		{
			RecycledListItem recycledListItem = vMK3GqZ5dLn66P6W8Bh1GjI[i];
			recycledListItem.Position = i;
			WyZGbTAsrnDliiXt0TZpvkQ.SetItemContent(recycledListItem);
		}
	}

	[CompilerGenerated]
	private void vGEGwaAcAZaMSVZABOCr7m0(Vector2 vector2_0)
	{
		B56GMpvh62uxz8MOHRu3XCO4pBrJ09b3wT8ivjZ5R9ar();
	}

	internal static ScrollRect.ScrollRectEvent smethod_0(ScrollRect scrollRect_0)
	{
		return scrollRect_0.onValueChanged;
	}

	internal static Rect smethod_1(RectTransform rectTransform_0)
	{
		return rectTransform_0.rect;
	}

	internal static void smethod_2(RectTransform rectTransform_0, Vector2 vector2_0)
	{
		rectTransform_0.anchoredPosition = vector2_0;
	}

	internal static Vector2 smethod_3(RectTransform rectTransform_0)
	{
		return rectTransform_0.sizeDelta;
	}

	internal static Vector2 smethod_4(RectTransform rectTransform_0)
	{
		return rectTransform_0.anchoredPosition;
	}

	internal static GameObject smethod_5(Component component_0)
	{
		return component_0.gameObject;
	}

	internal static void smethod_6(GameObject gameObject_0, bool bool_0)
	{
		gameObject_0.SetActive(bool_0);
	}

	internal static Transform smethod_7(Component component_0)
	{
		return component_0.transform;
	}
}
