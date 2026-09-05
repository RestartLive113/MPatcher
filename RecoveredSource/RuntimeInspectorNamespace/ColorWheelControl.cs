using System;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RuntimeInspectorNamespace;

public class ColorWheelControl : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IDragHandler, IPointerUpHandler
{
	public delegate void OnColorChangedDelegate(Color32 color);

	private const float float_0 = 2f / (float)Math.PI;

	private const float float_1 = (float)Math.PI * 2f / 3f;

	private const float srd_0024_0024on8KPITx56ga42UpcA = 4.1887903f;

	private Color JfkYgNmREh7BDPYCJQKWJ7Q;

	[CompilerGenerated]
	private float cp99D05Je984pVZlfbZ34S2l26BMcrc4qQc7ZFRPSBIg;

	private RectTransform vHfn1ppWs5NVru2AA2jCOew;

	[SerializeField]
	private RectTransform rectTransform_0;

	[SerializeField]
	private RectTransform DHmoGJu4CNDA_2i0Ptyt0Uo;

	[SerializeField]
	private WindowDragHandler YQjO3v9RvCRXVvRsbpIGHB6AQ2LNeTqdOtikoSjy1KqZ;

	private float K5X9wkew_6oQwo4Y_jkGO1I;

	private Vector2 UqUupV8KwspW85b2nQOnr_0024k;

	private Material bI2brPAlXsrzT5DHrzCi4uw;

	private bool ICBpRFi06gNmn3NDomstV5U;

	private bool R3TVjhhSTdsV6dpkp_0024epdb8;

	private float wIW0H4_XSoxmONOwJHHi7H4;

	private float float_2;

	private float TjsFanEqF8Q0q2rr78pcG6LtshUJG8BwAby_ZrgEBhgS;

	private float RxgljC9YILpuXz_0024pizrZzKp1W27905PG0c7x0cDEy7Xg;

	private int vOUl46QYoRjg8PBakr_0024nMK8 = -98765;

	[CompilerGenerated]
	private OnColorChangedDelegate HFkXxT0Buc6CseuYpD6bntk;

	public Color Color
	{
		get
		{
			return JfkYgNmREh7BDPYCJQKWJ7Q;
		}
		private set
		{
			if (JfkYgNmREh7BDPYCJQKWJ7Q != value)
			{
				JfkYgNmREh7BDPYCJQKWJ7Q = value;
				JfkYgNmREh7BDPYCJQKWJ7Q.a = Alpha;
				if (HFkXxT0Buc6CseuYpD6bntk != null)
				{
					HFkXxT0Buc6CseuYpD6bntk(JfkYgNmREh7BDPYCJQKWJ7Q);
				}
			}
		}
	}

	public float Alpha
	{
		[CompilerGenerated]
		get
		{
			return cp99D05Je984pVZlfbZ34S2l26BMcrc4qQc7ZFRPSBIg;
		}
		[CompilerGenerated]
		set
		{
			cp99D05Je984pVZlfbZ34S2l26BMcrc4qQc7ZFRPSBIg = value;
		}
	}

	public event OnColorChangedDelegate OnColorChanged
	{
		[CompilerGenerated]
		add
		{
			OnColorChangedDelegate onColorChangedDelegate = HFkXxT0Buc6CseuYpD6bntk;
			OnColorChangedDelegate onColorChangedDelegate2;
			do
			{
				onColorChangedDelegate2 = onColorChangedDelegate;
				OnColorChangedDelegate value2 = (OnColorChangedDelegate)smethod_0((Delegate)onColorChangedDelegate2, (Delegate)value);
				onColorChangedDelegate = Interlocked.CompareExchange(ref HFkXxT0Buc6CseuYpD6bntk, value2, onColorChangedDelegate2);
			}
			while ((object)onColorChangedDelegate != onColorChangedDelegate2);
		}
		[CompilerGenerated]
		remove
		{
			OnColorChangedDelegate onColorChangedDelegate = HFkXxT0Buc6CseuYpD6bntk;
			OnColorChangedDelegate onColorChangedDelegate2;
			do
			{
				onColorChangedDelegate2 = onColorChangedDelegate;
				OnColorChangedDelegate value2 = (OnColorChangedDelegate)smethod_1((Delegate)onColorChangedDelegate2, (Delegate)value);
				onColorChangedDelegate = Interlocked.CompareExchange(ref HFkXxT0Buc6CseuYpD6bntk, value2, onColorChangedDelegate2);
			}
			while ((object)onColorChangedDelegate != onColorChangedDelegate2);
		}
	}

	private void LfaiZbNwRELPQoddbWEUzjg()
	{
		vHfn1ppWs5NVru2AA2jCOew = (RectTransform)smethod_2((Component)this);
		Image component = GetComponent<Image>();
		bI2brPAlXsrzT5DHrzCi4uw = smethod_4(smethod_3((Graphic)component));
		smethod_5((Graphic)component, bI2brPAlXsrzT5DHrzCi4uw);
		method_1();
	}

	private void method_0()
	{
		if (!smethod_6((UnityEngine.Object)vHfn1ppWs5NVru2AA2jCOew, (UnityEngine.Object)null))
		{
			method_1();
			k4Y2AljUQkBaE3rO2e5eRHA();
		}
	}

	private void method_1()
	{
		wIW0H4_XSoxmONOwJHHi7H4 = smethod_7(vHfn1ppWs5NVru2AA2jCOew).size.x * 0.5f;
		float_2 = wIW0H4_XSoxmONOwJHHi7H4 * wIW0H4_XSoxmONOwJHHi7H4;
		TjsFanEqF8Q0q2rr78pcG6LtshUJG8BwAby_ZrgEBhgS = float_2 * 0.75f * 0.75f;
		RxgljC9YILpuXz_0024pizrZzKp1W27905PG0c7x0cDEy7Xg = wIW0H4_XSoxmONOwJHHi7H4 * 0.5f;
	}

	public void PickColor(Color c)
	{
		Alpha = c.a;
		Color.RGBToHSV(c, out var H, out var S, out var V);
		K5X9wkew_6oQwo4Y_jkGO1I = H * 2f * (float)Math.PI;
		UqUupV8KwspW85b2nQOnr_0024k.x = 1f - S;
		UqUupV8KwspW85b2nQOnr_0024k.y = 1f - V;
		k4Y2AljUQkBaE3rO2e5eRHA();
		Color = c;
		smethod_8(bI2brPAlXsrzT5DHrzCi4uw, global::_003CModule_003E.smethod_29<string>(1106127759u), s9ghKo2f1iLw1xUPaI_XXDj1WpEXIoHut78o0Yv3TYUF());
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		Vector2 vector2_ = default(Vector2);
		if (!smethod_11(vHfn1ppWs5NVru2AA2jCOew, smethod_9(eventData), smethod_10(eventData), ref vector2_))
		{
			return;
		}
		float sqrMagnitude = vector2_.sqrMagnitude;
		if (!(sqrMagnitude <= float_2) || sqrMagnitude < TjsFanEqF8Q0q2rr78pcG6LtshUJG8BwAby_ZrgEBhgS)
		{
			if (!(Mathf.Abs(vector2_.x) <= RxgljC9YILpuXz_0024pizrZzKp1W27905PG0c7x0cDEy7Xg) || !(Mathf.Abs(vector2_.y) <= RxgljC9YILpuXz_0024pizrZzKp1W27905PG0c7x0cDEy7Xg))
			{
				return;
			}
			R3TVjhhSTdsV6dpkp_0024epdb8 = true;
		}
		else
		{
			ICBpRFi06gNmn3NDomstV5U = true;
		}
		RmOf9JLq_Wnlx6cppeJVRX6lnjBI_8HRNGinCe8u9Z3W(vector2_);
		vOUl46QYoRjg8PBakr_0024nMK8 = eventData.pointerId;
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (vOUl46QYoRjg8PBakr_0024nMK8 != smethod_12(eventData))
		{
			smethod_14(eventData, smethod_13((Component)YQjO3v9RvCRXVvRsbpIGHB6AQ2LNeTqdOtikoSjy1KqZ));
			YQjO3v9RvCRXVvRsbpIGHB6AQ2LNeTqdOtikoSjy1KqZ.OnBeginDrag(eventData);
		}
		else
		{
			Vector2 vector2_ = default(Vector2);
			smethod_11(vHfn1ppWs5NVru2AA2jCOew, smethod_9(eventData), smethod_10(eventData), ref vector2_);
			RmOf9JLq_Wnlx6cppeJVRX6lnjBI_8HRNGinCe8u9Z3W(vector2_);
		}
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (vOUl46QYoRjg8PBakr_0024nMK8 == smethod_12(eventData))
		{
			Vector2 vector2_ = default(Vector2);
			smethod_11(vHfn1ppWs5NVru2AA2jCOew, smethod_9(eventData), smethod_10(eventData), ref vector2_);
			RmOf9JLq_Wnlx6cppeJVRX6lnjBI_8HRNGinCe8u9Z3W(vector2_);
			ICBpRFi06gNmn3NDomstV5U = false;
			R3TVjhhSTdsV6dpkp_0024epdb8 = false;
			vOUl46QYoRjg8PBakr_0024nMK8 = -98765;
		}
	}

	private void RmOf9JLq_Wnlx6cppeJVRX6lnjBI_8HRNGinCe8u9Z3W(Vector2 vector2_0)
	{
		if (ICBpRFi06gNmn3NDomstV5U)
		{
			Vector2 vector = -vector2_0.normalized;
			K5X9wkew_6oQwo4Y_jkGO1I = Mathf.Atan2(0f - vector.x, 0f - vector.y);
			orgKyVtI3PK6o0Mp_00245KdfKc();
		}
		else if (R3TVjhhSTdsV6dpkp_0024epdb8)
		{
			Vector2 vector2 = -vector2_0;
			vector2.x = Mathf.Clamp(vector2.x, 0f - RxgljC9YILpuXz_0024pizrZzKp1W27905PG0c7x0cDEy7Xg, RxgljC9YILpuXz_0024pizrZzKp1W27905PG0c7x0cDEy7Xg) + RxgljC9YILpuXz_0024pizrZzKp1W27905PG0c7x0cDEy7Xg;
			vector2.y = Mathf.Clamp(vector2.y, 0f - RxgljC9YILpuXz_0024pizrZzKp1W27905PG0c7x0cDEy7Xg, RxgljC9YILpuXz_0024pizrZzKp1W27905PG0c7x0cDEy7Xg) + RxgljC9YILpuXz_0024pizrZzKp1W27905PG0c7x0cDEy7Xg;
			UqUupV8KwspW85b2nQOnr_0024k = vector2 / wIW0H4_XSoxmONOwJHHi7H4;
			orgKyVtI3PK6o0Mp_00245KdfKc();
		}
		k4Y2AljUQkBaE3rO2e5eRHA();
	}

	private void orgKyVtI3PK6o0Mp_00245KdfKc()
	{
		Color color = s9ghKo2f1iLw1xUPaI_XXDj1WpEXIoHut78o0Yv3TYUF();
		smethod_8(bI2brPAlXsrzT5DHrzCi4uw, global::_003CModule_003E.smethod_27<string>(2006862694u), color);
		color = Color.Lerp(color, Color.white, UqUupV8KwspW85b2nQOnr_0024k.x);
		color = Color.Lerp(color, Color.black, UqUupV8KwspW85b2nQOnr_0024k.y);
		Color = color;
	}

	private Color s9ghKo2f1iLw1xUPaI_XXDj1WpEXIoHut78o0Yv3TYUF()
	{
		Color white = Color.white;
		white.r = Mathf.Clamp(2f / (float)Math.PI * Mathf.Asin(Mathf.Cos(K5X9wkew_6oQwo4Y_jkGO1I)) * 1.5f + 0.5f, 0f, 1f);
		white.g = Mathf.Clamp(2f / (float)Math.PI * Mathf.Asin(Mathf.Cos((float)Math.PI * 2f / 3f - K5X9wkew_6oQwo4Y_jkGO1I)) * 1.5f + 0.5f, 0f, 1f);
		white.b = Mathf.Clamp(2f / (float)Math.PI * Mathf.Asin(Mathf.Cos(4.1887903f - K5X9wkew_6oQwo4Y_jkGO1I)) * 1.5f + 0.5f, 0f, 1f);
		return white;
	}

	private void k4Y2AljUQkBaE3rO2e5eRHA()
	{
		rectTransform_0.anchoredPosition = new Vector2(Mathf.Sin(K5X9wkew_6oQwo4Y_jkGO1I) * wIW0H4_XSoxmONOwJHHi7H4 * 0.85f, Mathf.Cos(K5X9wkew_6oQwo4Y_jkGO1I) * wIW0H4_XSoxmONOwJHHi7H4 * 0.85f);
		DHmoGJu4CNDA_2i0Ptyt0Uo.anchoredPosition = new Vector2(RxgljC9YILpuXz_0024pizrZzKp1W27905PG0c7x0cDEy7Xg - UqUupV8KwspW85b2nQOnr_0024k.x * wIW0H4_XSoxmONOwJHHi7H4, RxgljC9YILpuXz_0024pizrZzKp1W27905PG0c7x0cDEy7Xg - UqUupV8KwspW85b2nQOnr_0024k.y * wIW0H4_XSoxmONOwJHHi7H4);
	}

	internal static Delegate smethod_0(Delegate delegate_0, Delegate delegate_1)
	{
		return Delegate.Combine(delegate_0, delegate_1);
	}

	internal static Delegate smethod_1(Delegate delegate_0, Delegate delegate_1)
	{
		return Delegate.Remove(delegate_0, delegate_1);
	}

	internal static Transform smethod_2(Component component_0)
	{
		return component_0.transform;
	}

	internal static Material smethod_3(Graphic graphic_0)
	{
		return graphic_0.material;
	}

	internal static Material smethod_4(Material material_0)
	{
		return new Material(material_0);
	}

	internal static void smethod_5(Graphic graphic_0, Material material_0)
	{
		graphic_0.material = material_0;
	}

	internal static bool smethod_6(UnityEngine.Object object_0, UnityEngine.Object object_1)
	{
		return object_0 == object_1;
	}

	internal static Rect smethod_7(RectTransform rectTransform_1)
	{
		return rectTransform_1.rect;
	}

	internal static void smethod_8(Material material_0, string string_0, Color color_0)
	{
		material_0.SetColor(string_0, color_0);
	}

	internal static Vector2 smethod_9(PointerEventData pointerEventData_0)
	{
		return pointerEventData_0.position;
	}

	internal static Camera smethod_10(PointerEventData pointerEventData_0)
	{
		return pointerEventData_0.pressEventCamera;
	}

	internal static bool smethod_11(RectTransform rectTransform_1, Vector2 vector2_0, Camera camera_0, ref Vector2 vector2_1)
	{
		return RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform_1, vector2_0, camera_0, out vector2_1);
	}

	internal static int smethod_12(PointerEventData pointerEventData_0)
	{
		return pointerEventData_0.pointerId;
	}

	internal static GameObject smethod_13(Component component_0)
	{
		return component_0.gameObject;
	}

	internal static void smethod_14(PointerEventData pointerEventData_0, GameObject gameObject_0)
	{
		pointerEventData_0.pointerDrag = gameObject_0;
	}
}
