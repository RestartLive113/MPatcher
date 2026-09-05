using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RuntimeInspectorNamespace;

public abstract class PopupBase : MonoBehaviour
{
	private const float wGkmFAtLH5YfnyarUxC9_00249V_0024EpYlXyYiSREv_0024xMO_00245d4 = 5f;

	[SerializeField]
	private LayoutElement NjbG_0024pca2Ycucf9vfNFPglQokPIpzDaCTBMAch4OF1gy;

	[SerializeField]
	private Image slJt0vtJTZ_pZ4HFn1Pm0w0;

	[SerializeField]
	protected Text label;

	private RectTransform vHfn1ppWs5NVru2AA2jCOew;

	private RectTransform LE9jjEDE3H8uujYy7nwv6ME;

	private Camera Iwb6gPmOAOuGMV9lQMfy_Dc;

	protected PointerEventData pointer;

	private float float_0;

	private int tez8QKQVeFGVS4AMHMsbzyw;

	private UISkin E58c_5PzPLk6LleLXcBTp_0024M;

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
				tez8QKQVeFGVS4AMHMsbzyw = E58c_5PzPLk6LleLXcBTp_0024M.Version;
				NjbG_0024pca2Ycucf9vfNFPglQokPIpzDaCTBMAch4OF1gy.SetHeight((float)E58c_5PzPLk6LleLXcBTp_0024M.LineHeight * 2.5f);
				smethod_1(slJt0vtJTZ_pZ4HFn1Pm0w0.GetComponent<LayoutElement>(), (float)E58c_5PzPLk6LleLXcBTp_0024M.LineHeight);
				float a = smethod_2((Graphic)slJt0vtJTZ_pZ4HFn1Pm0w0).a;
				Color color_ = E58c_5PzPLk6LleLXcBTp_0024M.InputFieldNormalBackgroundColor.Tint(0.05f);
				color_.a = a;
				smethod_3((Graphic)slJt0vtJTZ_pZ4HFn1Pm0w0, color_);
				label.SetSkinInputFieldText(E58c_5PzPLk6LleLXcBTp_0024M);
			}
		}
	}

	public void Initialize(Canvas canvas)
	{
		vHfn1ppWs5NVru2AA2jCOew = (RectTransform)smethod_4((Component)this);
		LE9jjEDE3H8uujYy7nwv6ME = (RectTransform)smethod_5((Component)canvas);
		if (smethod_6(canvas) != RenderMode.ScreenSpaceOverlay && (smethod_6(canvas) != RenderMode.ScreenSpaceCamera || !smethod_8((Object)smethod_7(canvas), (Object)null)))
		{
			Iwb6gPmOAOuGMV9lQMfy_Dc = (smethod_9((Object)smethod_7(canvas)) ? smethod_7(canvas) : smethod_10());
		}
		else
		{
			Iwb6gPmOAOuGMV9lQMfy_Dc = null;
		}
	}

	protected void SetPointer(PointerEventData pointer)
	{
		this.pointer = pointer;
		float_0 = 5f;
		RepositionSelf();
	}

	protected void RepositionSelf()
	{
		Vector2 vector2_ = default(Vector2);
		if (smethod_12(LE9jjEDE3H8uujYy7nwv6ME, smethod_11(pointer), Iwb6gPmOAOuGMV9lQMfy_Dc, ref vector2_))
		{
			smethod_13(vHfn1ppWs5NVru2AA2jCOew, vector2_);
		}
	}

	protected abstract void DestroySelf();

	private void method_0()
	{
		float_0 -= smethod_14();
		if (float_0 <= 0f)
		{
			float_0 = 5f;
			if (!pointer.IsPointerValid())
			{
				DestroySelf();
			}
		}
	}

	internal static bool smethod_0(Object object_0, Object object_1)
	{
		return object_0 != object_1;
	}

	internal static void smethod_1(LayoutElement layoutElement_0, float float_1)
	{
		layoutElement_0.minHeight = float_1;
	}

	internal static Color smethod_2(Graphic graphic_0)
	{
		return graphic_0.color;
	}

	internal static void smethod_3(Graphic graphic_0, Color color_0)
	{
		graphic_0.color = color_0;
	}

	internal static Transform smethod_4(Component component_0)
	{
		return component_0.transform;
	}

	internal static Transform smethod_5(Component component_0)
	{
		return component_0.transform;
	}

	internal static RenderMode smethod_6(Canvas canvas_0)
	{
		return canvas_0.renderMode;
	}

	internal static Camera smethod_7(Canvas canvas_0)
	{
		return canvas_0.worldCamera;
	}

	internal static bool smethod_8(Object object_0, Object object_1)
	{
		return object_0 == object_1;
	}

	internal static bool smethod_9(Object object_0)
	{
		return object_0;
	}

	internal static Camera smethod_10()
	{
		return Camera.main;
	}

	internal static Vector2 smethod_11(PointerEventData pointerEventData_0)
	{
		return pointerEventData_0.position;
	}

	internal static bool smethod_12(RectTransform rectTransform_0, Vector2 vector2_0, Camera camera_0, ref Vector2 vector2_1)
	{
		return RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform_0, vector2_0, camera_0, out vector2_1);
	}

	internal static void smethod_13(RectTransform rectTransform_0, Vector2 vector2_0)
	{
		rectTransform_0.anchoredPosition = vector2_0;
	}

	internal static float smethod_14()
	{
		return Time.unscaledDeltaTime;
	}
}
