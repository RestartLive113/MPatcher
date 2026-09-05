using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace RuntimeInspectorNamespace;

public class ObjectReferencePicker : SkinnedWindow, IListViewAdapter
{
	public delegate void OnReferenceChanged(UnityEngine.Object reference);

	[Serializable]
	[CompilerGenerated]
	private sealed class MqZgnqEKlr6oETrZ05DrVXstQJOU7tQE9_0024NinlC0pcdvdCHFK_4NFa4xLJs5uzCgiNM6e36xK4TQvQ0eSJ4NAT8
	{
		public static readonly MqZgnqEKlr6oETrZ05DrVXstQJOU7tQE9_0024NinlC0pcdvdCHFK_4NFa4xLJs5uzCgiNM6e36xK4TQvQ0eSJ4NAT8 _003C_003E9 = new MqZgnqEKlr6oETrZ05DrVXstQJOU7tQE9_0024NinlC0pcdvdCHFK_4NFa4xLJs5uzCgiNM6e36xK4TQvQ0eSJ4NAT8();

		public static Comparison<UnityEngine.Object> _003C_003E9__34_0;

		internal int HKg5xYm64vGw58R8WOyR_0024Ws6FFqHpRs86ZQ6ei7wo7Q4(UnityEngine.Object object_0, UnityEngine.Object object_1)
		{
			return smethod_0(object_0.GetName(), object_1.GetName());
		}

		internal static int smethod_0(string string_0, string string_1)
		{
			return string_0.CompareTo(string_1);
		}
	}

	private const string string_0 = "SpriteAtlasTexture-";

	private static ObjectReferencePicker K1hFj5zLZuQMBpgW_EkwUtE;

	private OnReferenceChanged vExz_0024mSjFni6Q1qvCbahfd_0024dgo9jNT8oUQBUoTqhLCwT;

	[SerializeField]
	private Image JMy24cZsilDTO4e_0024231jlXk;

	[SerializeField]
	private Image qi8DE97r0WFt7fw2xtqHwQY;

	[SerializeField]
	private InputField y7tO5xgyRcKT7H7lG4krsEQ;

	[SerializeField]
	private Image lQNg6_0024CRRZVdaAmgLUot_50;

	[SerializeField]
	private Image Vatd62XFDV6heXffszpl_0024T9g7bVA9BD6cSTuOVxtz5h7;

	[SerializeField]
	private Text IZY_0024xcQ2gsv0sepRCdj_T2VmBbCMTo0ek5_0024t_0024WSh1QMx;

	[SerializeField]
	private LayoutElement lx_E2ZqtJppp66tA0konjcrMG0uXgwnbrF_0024KFD_bOJDM;

	[SerializeField]
	private LayoutElement dVcX6SuSx_0024kGqLGokraly5ZuQetKibReHJR7Lg2nEiaD;

	[SerializeField]
	private Button AKwP8uWl1WlZSC4gKmG7L2A;

	[SerializeField]
	private Button rmWSiGwV1eJSSIxHbaQx6co;

	[SerializeField]
	private RecycledListView recycledListView_0;

	[SerializeField]
	private Image dVgM1RaAn0cLr1dhCAJgVU4lzeR2_n19LjCbHzub3qEP;

	[SerializeField]
	private ObjectReferencePickerItem qnU6VwTrt_QLDkqIVMnU1Bw0toxex1W_wa_0024EQtJf4_0024FY;

	private Canvas Kw6kqIgvhzUS4V6ocIEIxYY;

	private readonly List<UnityEngine.Object> fGSv2l9NTeRq9fx7dhnqOX8 = new List<UnityEngine.Object>(64);

	private readonly List<UnityEngine.Object> C09pSyOk4u6CPW4jAcdrahjilnFFcVP4H0NVU8r0TA6r = new List<UnityEngine.Object>(64);

	private UnityEngine.Object YQchHFKnioVlxqspaiQkP8M;

	private UnityEngine.Object sLbNUdYMW_VjCW_xwbBt6shIiSE56rCK4qn3YTnO6Qa7;

	private ObjectReferencePickerItem NBi89vZWap6vPC3LqaSxyLz9v_iXGbprHv4WiDp0s0Ww;

	public static ObjectReferencePicker Instance
	{
		get
		{
			if (!smethod_3((UnityEngine.Object)K1hFj5zLZuQMBpgW_EkwUtE))
			{
				K1hFj5zLZuQMBpgW_EkwUtE = UnityEngine.Object.Instantiate(awf1opR73mv9LSqQ84LlTsI.cqWoMNveroNrLO3XzL3B_0024XA<GameObject>(global::_003CModule_003E.smethod_26<string>(145938107u))).GetComponent<ObjectReferencePicker>();
				smethod_5(smethod_4((Component)K1hFj5zLZuQMBpgW_EkwUtE), bool_0: false);
				RuntimeInspectorUtils.IgnoredTransformsInHierarchy.Add(smethod_6((Component)K1hFj5zLZuQMBpgW_EkwUtE));
			}
			return K1hFj5zLZuQMBpgW_EkwUtE;
		}
	}

	int IListViewAdapter.Count => C09pSyOk4u6CPW4jAcdrahjilnFFcVP4H0NVU8r0TA6r.Count;

	float IListViewAdapter.ItemHeight => base.Skin.LineHeight;

	protected override void Awake()
	{
		base.Awake();
		recycledListView_0.SetAdapter(this);
		smethod_7(y7tO5xgyRcKT7H7lG4krsEQ).AddListener(DAtN_fng90nP0mWTpppE89Xx0935zcPM6AJGE7YgaELk);
		smethod_9((UnityEvent)smethod_8(AKwP8uWl1WlZSC4gKmG7L2A), (UnityAction)Cancel);
		smethod_9((UnityEvent)smethod_8(rmWSiGwV1eJSSIxHbaQx6co), (UnityAction)Close);
	}

	public void Show(OnReferenceChanged onReferenceChanged, Type referenceType, UnityEngine.Object[] references, UnityEngine.Object initialReference, Canvas referenceCanvas)
	{
		YQchHFKnioVlxqspaiQkP8M = initialReference;
		vExz_0024mSjFni6Q1qvCbahfd_0024dgo9jNT8oUQBUoTqhLCwT = onReferenceChanged;
		if (smethod_3((UnityEngine.Object)referenceCanvas) && smethod_10((UnityEngine.Object)Kw6kqIgvhzUS4V6ocIEIxYY, (UnityEngine.Object)referenceCanvas))
		{
			Kw6kqIgvhzUS4V6ocIEIxYY = referenceCanvas;
			Canvas component = GetComponent<Canvas>();
			component.CopyValuesFrom(referenceCanvas);
			smethod_12(component, Mathf.Max(1000, smethod_11(referenceCanvas) + 100));
		}
		smethod_14(smethod_13((Graphic)JMy24cZsilDTO4e_0024231jlXk), Vector2.zero);
		smethod_5(smethod_15((Component)this), bool_0: true);
		smethod_18(IZY_0024xcQ2gsv0sepRCdj_T2VmBbCMTo0ek5_0024t_0024WSh1QMx, smethod_17(global::_003CModule_003E.smethod_26<string>(1951822550u), smethod_16((MemberInfo)referenceType)));
		sLbNUdYMW_VjCW_xwbBt6shIiSE56rCK4qn3YTnO6Qa7 = initialReference;
		hnieDS1SNEIcAUFeXF_AIba1orsU1lFBosN_0024fzUs1AUk(references, referenceType);
	}

	public void Cancel()
	{
		if (smethod_10(sLbNUdYMW_VjCW_xwbBt6shIiSE56rCK4qn3YTnO6Qa7, YQchHFKnioVlxqspaiQkP8M) && vExz_0024mSjFni6Q1qvCbahfd_0024dgo9jNT8oUQBUoTqhLCwT != null)
		{
			vExz_0024mSjFni6Q1qvCbahfd_0024dgo9jNT8oUQBUoTqhLCwT(YQchHFKnioVlxqspaiQkP8M);
		}
		Close();
	}

	public void Close()
	{
		vExz_0024mSjFni6Q1qvCbahfd_0024dgo9jNT8oUQBUoTqhLCwT = null;
		YQchHFKnioVlxqspaiQkP8M = null;
		sLbNUdYMW_VjCW_xwbBt6shIiSE56rCK4qn3YTnO6Qa7 = null;
		NBi89vZWap6vPC3LqaSxyLz9v_iXGbprHv4WiDp0s0Ww = null;
		fGSv2l9NTeRq9fx7dhnqOX8.Clear();
		C09pSyOk4u6CPW4jAcdrahjilnFFcVP4H0NVU8r0TA6r.Clear();
		smethod_5(smethod_15((Component)this), bool_0: false);
	}

	protected override void RefreshSkin()
	{
		smethod_19((Graphic)JMy24cZsilDTO4e_0024231jlXk, base.Skin.WindowColor);
		smethod_19((Graphic)dVgM1RaAn0cLr1dhCAJgVU4lzeR2_n19LjCbHzub3qEP, base.Skin.BackgroundColor);
		smethod_19((Graphic)qi8DE97r0WFt7fw2xtqHwQY, base.Skin.ScrollbarColor);
		IZY_0024xcQ2gsv0sepRCdj_T2VmBbCMTo0ek5_0024t_0024WSh1QMx.SetSkinText(base.Skin);
		smethod_20(y7tO5xgyRcKT7H7lG4krsEQ).SetSkinButtonText(base.Skin);
		smethod_19((Graphic)Vatd62XFDV6heXffszpl_0024T9g7bVA9BD6cSTuOVxtz5h7, base.Skin.ButtonBackgroundColor);
		smethod_19((Graphic)lQNg6_0024CRRZVdaAmgLUot_50, base.Skin.ButtonTextColor);
		lx_E2ZqtJppp66tA0konjcrMG0uXgwnbrF_0024KFD_bOJDM.SetHeight(base.Skin.LineHeight);
		dVcX6SuSx_0024kGqLGokraly5ZuQetKibReHJR7Lg2nEiaD.SetHeight(Mathf.Min(45f, (float)base.Skin.LineHeight * 1.5f));
		AKwP8uWl1WlZSC4gKmG7L2A.SetSkinButton(base.Skin);
		rmWSiGwV1eJSSIxHbaQx6co.SetSkinButton(base.Skin);
		recycledListView_0.ResetList();
	}

	private void hnieDS1SNEIcAUFeXF_AIba1orsU1lFBosN_0024fzUs1AUk(UnityEngine.Object[] object_0, Type type_0)
	{
		fGSv2l9NTeRq9fx7dhnqOX8.Clear();
		C09pSyOk4u6CPW4jAcdrahjilnFFcVP4H0NVU8r0TA6r.Clear();
		smethod_21(y7tO5xgyRcKT7H7lG4krsEQ, string.Empty);
		fGSv2l9NTeRq9fx7dhnqOX8.Add(null);
		Array.Sort(object_0, (UnityEngine.Object obj, UnityEngine.Object obj2) => MqZgnqEKlr6oETrZ05DrVXstQJOU7tQE9_0024NinlC0pcdvdCHFK_4NFa4xLJs5uzCgiNM6e36xK4TQvQ0eSJ4NAT8.smethod_0(obj.GetName(), obj2.GetName()));
		bool flag = type_0 == smethod_22(typeof(Texture).TypeHandle) || type_0 == smethod_22(typeof(Texture).TypeHandle) || type_0 == smethod_22(typeof(Sprite).TypeHandle);
		for (int num = 0; num < object_0.Length; num++)
		{
			if (smethod_3(object_0[num]) && (smethod_23(object_0[num]) == HideFlags.None || smethod_23(object_0[num]) == HideFlags.NotEditable || smethod_23(object_0[num]) == HideFlags.HideInHierarchy || smethod_23(object_0[num]) == HideFlags.HideInInspector) && (!flag || !smethod_25(smethod_24(object_0[num]), global::_003CModule_003E.smethod_26<string>(120856439u))))
			{
				fGSv2l9NTeRq9fx7dhnqOX8.Add(object_0[num]);
			}
		}
		DAtN_fng90nP0mWTpppE89Xx0935zcPM6AJGE7YgaELk(string.Empty);
		recycledListView_0.UpdateList();
	}

	private RecycledListItem ODVL6hEHtxJ1hbG12loRGsajTIjh_sbobfpZlHh2C0gK_LI_ykQhXvjJVdY_mBlzGUAvLrnJoFAvBTGg669zcz0(Transform parent)
	{
		ObjectReferencePickerItem objectReferencePickerItem = UnityEngine.Object.Instantiate(qnU6VwTrt_QLDkqIVMnU1Bw0toxex1W_wa_0024EQtJf4_0024FY, parent, worldPositionStays: false);
		objectReferencePickerItem.Skin = base.Skin;
		return objectReferencePickerItem;
	}

	RecycledListItem IListViewAdapter.CreateItem(Transform parent)
	{
		//ILSpy generated this explicit interface implementation from .override directive in ODVL6hEHtxJ1hbG12loRGsajTIjh_sbobfpZlHh2C0gK_LI_ykQhXvjJVdY_mBlzGUAvLrnJoFAvBTGg669zcz0
		return this.ODVL6hEHtxJ1hbG12loRGsajTIjh_sbobfpZlHh2C0gK_LI_ykQhXvjJVdY_mBlzGUAvLrnJoFAvBTGg669zcz0(parent);
	}

	private void DAtN_fng90nP0mWTpppE89Xx0935zcPM6AJGE7YgaELk(string string_1)
	{
		C09pSyOk4u6CPW4jAcdrahjilnFFcVP4H0NVU8r0TA6r.Clear();
		string_1 = smethod_26(string_1);
		for (int i = 0; i < fGSv2l9NTeRq9fx7dhnqOX8.Count; i++)
		{
			if (smethod_27(smethod_26(fGSv2l9NTeRq9fx7dhnqOX8[i].GetName()), string_1))
			{
				C09pSyOk4u6CPW4jAcdrahjilnFFcVP4H0NVU8r0TA6r.Add(fGSv2l9NTeRq9fx7dhnqOX8[i]);
			}
		}
		recycledListView_0.UpdateList();
	}

	void IListViewAdapter.SetItemContent(RecycledListItem item)
	{
		ObjectReferencePickerItem objectReferencePickerItem = (ObjectReferencePickerItem)item;
		objectReferencePickerItem.SetContent(C09pSyOk4u6CPW4jAcdrahjilnFFcVP4H0NVU8r0TA6r[objectReferencePickerItem.Position]);
		if (smethod_28(objectReferencePickerItem.Reference, sLbNUdYMW_VjCW_xwbBt6shIiSE56rCK4qn3YTnO6Qa7))
		{
			objectReferencePickerItem.IsSelected = true;
			NBi89vZWap6vPC3LqaSxyLz9v_iXGbprHv4WiDp0s0Ww = objectReferencePickerItem;
		}
		else
		{
			objectReferencePickerItem.IsSelected = false;
		}
		objectReferencePickerItem.Skin = base.Skin;
	}

	private void bA2nXE0dVfEZ2p9wht4oTdIK3ZyuFmMmFr057wiGnKkdNrEe5ifiWHj7P3KTVfuMy93rGp7WayZ3QRBZSjIkA4g(RecycledListItem item)
	{
		if (smethod_10((UnityEngine.Object)NBi89vZWap6vPC3LqaSxyLz9v_iXGbprHv4WiDp0s0Ww, (UnityEngine.Object)null))
		{
			NBi89vZWap6vPC3LqaSxyLz9v_iXGbprHv4WiDp0s0Ww.IsSelected = false;
		}
		NBi89vZWap6vPC3LqaSxyLz9v_iXGbprHv4WiDp0s0Ww = (ObjectReferencePickerItem)item;
		sLbNUdYMW_VjCW_xwbBt6shIiSE56rCK4qn3YTnO6Qa7 = NBi89vZWap6vPC3LqaSxyLz9v_iXGbprHv4WiDp0s0Ww.Reference;
		NBi89vZWap6vPC3LqaSxyLz9v_iXGbprHv4WiDp0s0Ww.IsSelected = true;
		if (vExz_0024mSjFni6Q1qvCbahfd_0024dgo9jNT8oUQBUoTqhLCwT != null)
		{
			vExz_0024mSjFni6Q1qvCbahfd_0024dgo9jNT8oUQBUoTqhLCwT(NBi89vZWap6vPC3LqaSxyLz9v_iXGbprHv4WiDp0s0Ww.Reference);
		}
	}

	void IListViewAdapter.OnItemClicked(RecycledListItem item)
	{
		//ILSpy generated this explicit interface implementation from .override directive in bA2nXE0dVfEZ2p9wht4oTdIK3ZyuFmMmFr057wiGnKkdNrEe5ifiWHj7P3KTVfuMy93rGp7WayZ3QRBZSjIkA4g
		this.bA2nXE0dVfEZ2p9wht4oTdIK3ZyuFmMmFr057wiGnKkdNrEe5ifiWHj7P3KTVfuMy93rGp7WayZ3QRBZSjIkA4g(item);
	}

	public static void DestroyInstance()
	{
		if (smethod_3((UnityEngine.Object)K1hFj5zLZuQMBpgW_EkwUtE))
		{
			RuntimeInspectorUtils.IgnoredTransformsInHierarchy.Remove(smethod_6((Component)K1hFj5zLZuQMBpgW_EkwUtE));
			smethod_29((UnityEngine.Object)K1hFj5zLZuQMBpgW_EkwUtE);
			K1hFj5zLZuQMBpgW_EkwUtE = null;
		}
	}

	internal static bool smethod_3(UnityEngine.Object object_0)
	{
		return object_0;
	}

	internal static GameObject smethod_4(Component component_0)
	{
		return component_0.gameObject;
	}

	internal static void smethod_5(GameObject gameObject_0, bool bool_0)
	{
		gameObject_0.SetActive(bool_0);
	}

	internal static Transform smethod_6(Component component_0)
	{
		return component_0.transform;
	}

	internal static InputField.OnChangeEvent smethod_7(InputField inputField_0)
	{
		return inputField_0.onValueChanged;
	}

	internal static Button.ButtonClickedEvent smethod_8(Button button_0)
	{
		return button_0.onClick;
	}

	internal static void smethod_9(UnityEvent unityEvent_0, UnityAction unityAction_0)
	{
		unityEvent_0.AddListener(unityAction_0);
	}

	internal static bool smethod_10(UnityEngine.Object object_0, UnityEngine.Object object_1)
	{
		return object_0 != object_1;
	}

	internal static int smethod_11(Canvas canvas_0)
	{
		return canvas_0.sortingOrder;
	}

	internal static void smethod_12(Canvas canvas_0, int int_0)
	{
		canvas_0.sortingOrder = int_0;
	}

	internal static RectTransform smethod_13(Graphic graphic_0)
	{
		return graphic_0.rectTransform;
	}

	internal static void smethod_14(RectTransform rectTransform_0, Vector2 vector2_0)
	{
		rectTransform_0.anchoredPosition = vector2_0;
	}

	internal static GameObject smethod_15(Component component_0)
	{
		return component_0.gameObject;
	}

	internal static string smethod_16(MemberInfo memberInfo_0)
	{
		return memberInfo_0.Name;
	}

	internal static string smethod_17(string string_1, string string_2)
	{
		return string_1 + string_2;
	}

	internal static void smethod_18(Text text_0, string string_1)
	{
		text_0.text = string_1;
	}

	internal static void smethod_19(Graphic graphic_0, Color color_0)
	{
		graphic_0.color = color_0;
	}

	internal static Text smethod_20(InputField inputField_0)
	{
		return inputField_0.textComponent;
	}

	internal static void smethod_21(InputField inputField_0, string string_1)
	{
		inputField_0.text = string_1;
	}

	internal static Type smethod_22(RuntimeTypeHandle runtimeTypeHandle_0)
	{
		return Type.GetTypeFromHandle(runtimeTypeHandle_0);
	}

	internal static HideFlags smethod_23(UnityEngine.Object object_0)
	{
		return object_0.hideFlags;
	}

	internal static string smethod_24(UnityEngine.Object object_0)
	{
		return object_0.name;
	}

	internal static bool smethod_25(string string_1, string string_2)
	{
		return string_1.StartsWith(string_2);
	}

	internal static string smethod_26(string string_1)
	{
		return string_1.ToLowerInvariant();
	}

	internal static bool smethod_27(string string_1, string string_2)
	{
		return string_1.Contains(string_2);
	}

	internal static bool smethod_28(UnityEngine.Object object_0, UnityEngine.Object object_1)
	{
		return object_0 == object_1;
	}

	internal static void smethod_29(UnityEngine.Object object_0)
	{
		UnityEngine.Object.Destroy(object_0);
	}
}
