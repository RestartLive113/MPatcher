using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RuntimeInspectorNamespace;

public class RuntimeInspector : SkinnedWindow
{
	public enum VariableVisibility
	{
		None,
		SerializableOnly,
		All
	}

	public enum HeaderVisibility
	{
		Collapsible,
		AlwaysVisible,
		Hidden
	}

	public delegate object InspectedObjectChangingDelegate(object previousInspectedObject, object newInspectedObject);

	public delegate void ComponentFilterDelegate(GameObject gameObject, List<Component> components);

	private const string iGF5GbCxlefyM0ipL6stevGsZIr6SqjVcGgFpwhRVTrK = "RuntimeInspectorPool";

	[SerializeField]
	private float qN8FjfH1K_12kY9HRsxGPw8;

	private float lXIeOPTK0MqUmPKcH6tl8yQ = -1f;

	[SerializeField]
	private VariableVisibility b_0024q1nYo81juou9PA3y2mPrU = VariableVisibility.All;

	[SerializeField]
	private VariableVisibility qY51peG6KDIxo6ZWMMP6xqGVkQfjsOqTGFEGpVTIUPXd = VariableVisibility.All;

	[SerializeField]
	private bool pecFiEfEEfG5P7P4_g2yxioGeDl1jutLEh1k_tEWz75A;

	[SerializeField]
	private bool bool_0;

	[SerializeField]
	private bool bool_1;

	[SerializeField]
	private float gicvr9O8_pDjUvsCz0oagkY = 1f;

	[SerializeField]
	private int cQsjVriAZZRwob9cCPvdagE = 5;

	[SerializeField]
	private HeaderVisibility N4rhVkF2yxmMqu8BigiTBTvmYMxEYgERK_0024wfH_FpCd7eRL9p_0024JkUp5lh917RRvlaSA;

	[SerializeField]
	private int AsY6CPAhy9PP_S5UB1YWUZY = 10;

	private Transform transform_0;

	[SerializeField]
	private RuntimeHierarchy brbeRHiB0fYPn8HRCU3aLsuioitEeOdejnP5dX4AVYj2;

	[SerializeField]
	internal RuntimeInspectorSettings[] d2pQr3XMk7zMhqWE5dMM_3A;

	[SerializeField]
	[Header("Internal Variables")]
	private ScrollRect UizBv3bMRdfbyNqELufNvRs;

	private RectTransform FQBgn89DjhMudA260zu9JIg;

	[SerializeField]
	private Image slJt0vtJTZ_pZ4HFn1Pm0w0;

	[SerializeField]
	private Image qi8DE97r0WFt7fw2xtqHwQY;

	private static int ffWjt8bsC78jOJKY2j6HJsk = 0;

	private readonly Dictionary<Type, InspectorField[]> dictionary_0 = new Dictionary<Type, InspectorField[]>(89);

	private readonly Dictionary<Type, InspectorField[]> HLv8Ac2tzpwPOvkuClTI7hh12Nze3kMIPuKsvCsKu2A7 = new Dictionary<Type, InspectorField[]>(89);

	private readonly List<InspectorField> _wDMB1WieQWD_Hcz1F6TVJw = new List<InspectorField>(4);

	private static readonly Dictionary<Type, List<InspectorField>> _pncm5aICs2gqTVriJ0a7tg = new Dictionary<Type, List<InspectorField>>();

	private readonly List<VariableSet> neWeZNYxgXJ_u090RDH51hU = new List<VariableSet>(32);

	private readonly List<VariableSet> W_xn1OAeoB4vMsOCHaRArvozH2kxt1DhLgr7xvWhfqF8 = new List<VariableSet>(32);

	private InspectorField fiYILsSN2rMHz_D1owMDFrQ;

	private bool x5OXVfNWUs6rLwsTHGPaAGM;

	private bool mKOWVtLlOSmnM7_0024LUNQUa88;

	private InspectorField KM8ROVsxwoOUATQ335tuOTU;

	private PointerEventData pointerEventData_0;

	private float float_0;

	private object HOXgCOk8HiQ6U6JwRfP25nJJjdLLw4iqpuco6NByVXgk;

	private Canvas trJy0poPN23QufiviU1damw;

	public InspectedObjectChangingDelegate OnInspectedObjectChanging;

	private ComponentFilterDelegate PwEpbvQZfTgT_nEgNN8FXRqx8AjBq0JdKs93EwTvGtfK;

	public VariableVisibility ExposeFields
	{
		get
		{
			return b_0024q1nYo81juou9PA3y2mPrU;
		}
		set
		{
			if (b_0024q1nYo81juou9PA3y2mPrU != value)
			{
				b_0024q1nYo81juou9PA3y2mPrU = value;
				mKOWVtLlOSmnM7_0024LUNQUa88 = true;
			}
		}
	}

	public VariableVisibility ExposeProperties
	{
		get
		{
			return qY51peG6KDIxo6ZWMMP6xqGVkQfjsOqTGFEGpVTIUPXd;
		}
		set
		{
			if (qY51peG6KDIxo6ZWMMP6xqGVkQfjsOqTGFEGpVTIUPXd != value)
			{
				qY51peG6KDIxo6ZWMMP6xqGVkQfjsOqTGFEGpVTIUPXd = value;
				mKOWVtLlOSmnM7_0024LUNQUa88 = true;
			}
		}
	}

	public bool ArrayIndicesStartAtOne
	{
		get
		{
			return pecFiEfEEfG5P7P4_g2yxioGeDl1jutLEh1k_tEWz75A;
		}
		set
		{
			if (pecFiEfEEfG5P7P4_g2yxioGeDl1jutLEh1k_tEWz75A != value)
			{
				pecFiEfEEfG5P7P4_g2yxioGeDl1jutLEh1k_tEWz75A = value;
				mKOWVtLlOSmnM7_0024LUNQUa88 = true;
			}
		}
	}

	public bool UseTitleCaseNaming
	{
		get
		{
			return bool_0;
		}
		set
		{
			if (bool_0 != value)
			{
				bool_0 = value;
				mKOWVtLlOSmnM7_0024LUNQUa88 = true;
			}
		}
	}

	public bool ShowTooltips => bool_1;

	public float TooltipDelay
	{
		get
		{
			return gicvr9O8_pDjUvsCz0oagkY;
		}
		set
		{
			gicvr9O8_pDjUvsCz0oagkY = value;
		}
	}

	public int NestLimit
	{
		get
		{
			return cQsjVriAZZRwob9cCPvdagE;
		}
		set
		{
			if (cQsjVriAZZRwob9cCPvdagE != value)
			{
				cQsjVriAZZRwob9cCPvdagE = value;
				mKOWVtLlOSmnM7_0024LUNQUa88 = true;
			}
		}
	}

	public HeaderVisibility InspectedObjectHeaderVisibility
	{
		get
		{
			return N4rhVkF2yxmMqu8BigiTBTvmYMxEYgERK_0024wfH_FpCd7eRL9p_0024JkUp5lh917RRvlaSA;
		}
		set
		{
			if (N4rhVkF2yxmMqu8BigiTBTvmYMxEYgERK_0024wfH_FpCd7eRL9p_0024JkUp5lh917RRvlaSA != value)
			{
				N4rhVkF2yxmMqu8BigiTBTvmYMxEYgERK_0024wfH_FpCd7eRL9p_0024JkUp5lh917RRvlaSA = value;
				if (smethod_3((UnityEngine.Object)fiYILsSN2rMHz_D1owMDFrQ, (UnityEngine.Object)null) && fiYILsSN2rMHz_D1owMDFrQ is ExpandableInspectorField)
				{
					((ExpandableInspectorField)fiYILsSN2rMHz_D1owMDFrQ).HeaderVisibility = N4rhVkF2yxmMqu8BigiTBTvmYMxEYgERK_0024wfH_FpCd7eRL9p_0024JkUp5lh917RRvlaSA;
				}
			}
		}
	}

	public RuntimeHierarchy ConnectedHierarchy
	{
		get
		{
			return brbeRHiB0fYPn8HRCU3aLsuioitEeOdejnP5dX4AVYj2;
		}
		set
		{
			brbeRHiB0fYPn8HRCU3aLsuioitEeOdejnP5dX4AVYj2 = value;
		}
	}

	public object InspectedObject => HOXgCOk8HiQ6U6JwRfP25nJJjdLLw4iqpuco6NByVXgk;

	public bool IsBound => !HOXgCOk8HiQ6U6JwRfP25nJJjdLLw4iqpuco6NByVXgk.IsNull();

	public Canvas Canvas => trJy0poPN23QufiviU1damw;

	public ComponentFilterDelegate ComponentFilter
	{
		get
		{
			return PwEpbvQZfTgT_nEgNN8FXRqx8AjBq0JdKs93EwTvGtfK;
		}
		set
		{
			PwEpbvQZfTgT_nEgNN8FXRqx8AjBq0JdKs93EwTvGtfK = value;
			Refresh();
		}
	}

	protected override void Awake()
	{
		base.Awake();
		FQBgn89DjhMudA260zu9JIg = smethod_4(UizBv3bMRdfbyNqELufNvRs);
		trJy0poPN23QufiviU1damw = GetComponentInParent<Canvas>();
		GameObject gameObject = smethod_5(global::_003CModule_003E.smethod_29<string>(3345532083u));
		if (smethod_6((UnityEngine.Object)gameObject, (UnityEngine.Object)null))
		{
			gameObject = smethod_7(global::_003CModule_003E.smethod_29<string>(3345532083u));
			smethod_8((UnityEngine.Object)gameObject);
		}
		transform_0 = smethod_9(gameObject);
		ffWjt8bsC78jOJKY2j6HJsk++;
		for (int i = 0; i < d2pQr3XMk7zMhqWE5dMM_3A.Length; i++)
		{
			VariableSet[] hiddenVariables = d2pQr3XMk7zMhqWE5dMM_3A[i].HiddenVariables;
			if (hiddenVariables != null)
			{
				foreach (VariableSet variableSet in hiddenVariables)
				{
					if (variableSet.Init())
					{
						neWeZNYxgXJ_u090RDH51hU.Add(variableSet);
					}
				}
			}
			VariableSet[] exposedVariables = d2pQr3XMk7zMhqWE5dMM_3A[i].ExposedVariables;
			if (exposedVariables == null)
			{
				continue;
			}
			foreach (VariableSet variableSet2 in exposedVariables)
			{
				if (variableSet2.Init())
				{
					W_xn1OAeoB4vMsOCHaRArvozH2kxt1DhLgr7xvWhfqF8.Add(variableSet2);
				}
			}
		}
		RuntimeInspectorUtils.IgnoredTransformsInHierarchy.Add(FQBgn89DjhMudA260zu9JIg);
		RuntimeInspectorUtils.IgnoredTransformsInHierarchy.Add(transform_0);
	}

	private void U2FLoXbtzOTI8u5GsQUvnCQ()
	{
		if (--ffWjt8bsC78jOJKY2j6HJsk == 0)
		{
			if (smethod_10((UnityEngine.Object)transform_0))
			{
				RuntimeInspectorUtils.IgnoredTransformsInHierarchy.Remove(transform_0);
				smethod_12((UnityEngine.Object)smethod_11((Component)transform_0));
			}
			ColorPicker.DestroyInstance();
			ObjectReferencePicker.DestroyInstance();
			_pncm5aICs2gqTVriJ0a7tg.Clear();
		}
		RuntimeInspectorUtils.IgnoredTransformsInHierarchy.Remove(FQBgn89DjhMudA260zu9JIg);
	}

	private void jcPq5m3c7FL_B4giC_0024d_2cFRkqzkH6HLtBb2RY2ro6qT()
	{
		trJy0poPN23QufiviU1damw = GetComponentInParent<Canvas>();
	}

	protected override void Update()
	{
		base.Update();
		if (IsBound)
		{
			float num = smethod_13();
			if (!mKOWVtLlOSmnM7_0024LUNQUa88)
			{
				if (num > lXIeOPTK0MqUmPKcH6tl8yQ)
				{
					lXIeOPTK0MqUmPKcH6tl8yQ = num + qN8FjfH1K_12kY9HRsxGPw8;
					Refresh();
				}
			}
			else
			{
				object hOXgCOk8HiQ6U6JwRfP25nJJjdLLw4iqpuco6NByVXgk = HOXgCOk8HiQ6U6JwRfP25nJJjdLLw4iqpuco6NByVXgk;
				StopInspect();
				Inspect(hOXgCOk8HiQ6U6JwRfP25nJJjdLLw4iqpuco6NByVXgk);
				mKOWVtLlOSmnM7_0024LUNQUa88 = false;
				lXIeOPTK0MqUmPKcH6tl8yQ = num + qN8FjfH1K_12kY9HRsxGPw8;
			}
			if (pointerEventData_0 == null)
			{
				return;
			}
			Vector2 vector = smethod_14(pointerEventData_0);
			if (vector.x == 0f && vector.y == 0f)
			{
				if (num > float_0)
				{
					if (!smethod_10((UnityEngine.Object)KM8ROVsxwoOUATQ335tuOTU) || !smethod_15(smethod_11((Component)KM8ROVsxwoOUATQ335tuOTU)))
					{
						KM8ROVsxwoOUATQ335tuOTU = null;
						pointerEventData_0 = null;
					}
					else
					{
						RuntimeInspectorUtils.ShowTooltip(KM8ROVsxwoOUATQ335tuOTU.NameRaw, pointerEventData_0, base.Skin, trJy0poPN23QufiviU1damw);
						float_0 = float.PositiveInfinity;
					}
				}
			}
			else
			{
				float_0 = num + gicvr9O8_pDjUvsCz0oagkY;
			}
		}
		else if (smethod_3((UnityEngine.Object)fiYILsSN2rMHz_D1owMDFrQ, (UnityEngine.Object)null))
		{
			StopInspect();
		}
	}

	public void Refresh()
	{
		if (IsBound)
		{
			if (smethod_6((UnityEngine.Object)fiYILsSN2rMHz_D1owMDFrQ, (UnityEngine.Object)null))
			{
				HOXgCOk8HiQ6U6JwRfP25nJJjdLLw4iqpuco6NByVXgk = null;
			}
			else
			{
				fiYILsSN2rMHz_D1owMDFrQ.Refresh();
			}
		}
	}

	public void RefreshDelayed()
	{
		lXIeOPTK0MqUmPKcH6tl8yQ = 0f;
	}

	protected override void RefreshSkin()
	{
		smethod_16((Graphic)slJt0vtJTZ_pZ4HFn1Pm0w0, base.Skin.BackgroundColor);
		smethod_16((Graphic)qi8DE97r0WFt7fw2xtqHwQY, base.Skin.ScrollbarColor);
		if (IsBound && !mKOWVtLlOSmnM7_0024LUNQUa88)
		{
			fiYILsSN2rMHz_D1owMDFrQ.Skin = base.Skin;
		}
	}

	public void Inspect(object obj)
	{
		if (x5OXVfNWUs6rLwsTHGPaAGM)
		{
			return;
		}
		mKOWVtLlOSmnM7_0024LUNQUa88 = false;
		if (OnInspectedObjectChanging != null)
		{
			obj = OnInspectedObjectChanging(HOXgCOk8HiQ6U6JwRfP25nJJjdLLw4iqpuco6NByVXgk, obj);
		}
		if (HOXgCOk8HiQ6U6JwRfP25nJJjdLLw4iqpuco6NByVXgk == obj)
		{
			return;
		}
		StopInspect();
		x5OXVfNWUs6rLwsTHGPaAGM = true;
		try
		{
			HOXgCOk8HiQ6U6JwRfP25nJJjdLLw4iqpuco6NByVXgk = obj;
			if (obj.IsNull())
			{
				return;
			}
			if (!smethod_18(smethod_17(obj)))
			{
				if (smethod_15(smethod_20((Component)this)))
				{
					InspectorField inspectorField = CreateDrawerForType(smethod_17(obj), FQBgn89DjhMudA260zu9JIg, 0, drawObjectsAsFields: false);
					if (!smethod_3((UnityEngine.Object)inspectorField, (UnityEngine.Object)null))
					{
						HOXgCOk8HiQ6U6JwRfP25nJJjdLLw4iqpuco6NByVXgk = null;
						return;
					}
					inspectorField.BindTo(smethod_17(obj), string.Empty, () => HOXgCOk8HiQ6U6JwRfP25nJJjdLLw4iqpuco6NByVXgk, delegate(object object_0)
					{
						HOXgCOk8HiQ6U6JwRfP25nJJjdLLw4iqpuco6NByVXgk = object_0;
					});
					inspectorField.NameRaw = obj.GetNameWithType();
					inspectorField.Refresh();
					if (inspectorField is ExpandableInspectorField)
					{
						((ExpandableInspectorField)inspectorField).IsExpanded = true;
					}
					fiYILsSN2rMHz_D1owMDFrQ = inspectorField;
					if (fiYILsSN2rMHz_D1owMDFrQ is ExpandableInspectorField)
					{
						((ExpandableInspectorField)fiYILsSN2rMHz_D1owMDFrQ).HeaderVisibility = N4rhVkF2yxmMqu8BigiTBTvmYMxEYgERK_0024wfH_FpCd7eRL9p_0024JkUp5lh917RRvlaSA;
					}
					GameObject gameObject = HOXgCOk8HiQ6U6JwRfP25nJJjdLLw4iqpuco6NByVXgk as GameObject;
					if (smethod_10((UnityEngine.Object)gameObject) && HOXgCOk8HiQ6U6JwRfP25nJJjdLLw4iqpuco6NByVXgk is Component)
					{
						gameObject = smethod_11((Component)HOXgCOk8HiQ6U6JwRfP25nJJjdLLw4iqpuco6NByVXgk);
					}
					if (smethod_10((UnityEngine.Object)ConnectedHierarchy) && (!smethod_10((UnityEngine.Object)gameObject) || !ConnectedHierarchy.Select(smethod_9(gameObject))))
					{
						ConnectedHierarchy.Deselect();
					}
				}
				else
				{
					HOXgCOk8HiQ6U6JwRfP25nJJjdLLw4iqpuco6NByVXgk = null;
					smethod_19((object)global::_003CModule_003E.smethod_27<string>(29224464u));
				}
			}
			else
			{
				HOXgCOk8HiQ6U6JwRfP25nJJjdLLw4iqpuco6NByVXgk = null;
				smethod_19((object)global::_003CModule_003E.smethod_27<string>(3118996552u));
			}
		}
		finally
		{
			x5OXVfNWUs6rLwsTHGPaAGM = false;
		}
	}

	public void StopInspect()
	{
		if (x5OXVfNWUs6rLwsTHGPaAGM)
		{
			return;
		}
		if (smethod_3((UnityEngine.Object)fiYILsSN2rMHz_D1owMDFrQ, (UnityEngine.Object)null))
		{
			if (fiYILsSN2rMHz_D1owMDFrQ is ExpandableInspectorField)
			{
				((ExpandableInspectorField)fiYILsSN2rMHz_D1owMDFrQ).HeaderVisibility = HeaderVisibility.Collapsible;
			}
			fiYILsSN2rMHz_D1owMDFrQ.Unbind();
			fiYILsSN2rMHz_D1owMDFrQ = null;
		}
		HOXgCOk8HiQ6U6JwRfP25nJJjdLLw4iqpuco6NByVXgk = null;
		smethod_21(UizBv3bMRdfbyNqELufNvRs, 1f);
		ColorPicker.Instance.Close();
		ObjectReferencePicker.Instance.Close();
	}

	public InspectorField CreateDrawerForType(Type type, Transform drawerParent, int depth, bool drawObjectsAsFields = true, MemberInfo variable = null)
	{
		InspectorField[] array = YyAvW_CxE2mX2XpJ6pI12e8ZGIOiJLpLNT5Tlkq46xGO(type, drawObjectsAsFields);
		if (array != null)
		{
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].CanBindTo(type, variable))
				{
					InspectorField inspectorField = l_nnW_7PTCQd3NTnv3ZhAC8KVaSO47JRwCOA3SyrbjOd(array[i], drawerParent);
					inspectorField.Inspector = this;
					inspectorField.Skin = base.Skin;
					inspectorField.Depth = depth;
					return inspectorField;
				}
			}
		}
		return null;
	}

	private InspectorField l_nnW_7PTCQd3NTnv3ZhAC8KVaSO47JRwCOA3SyrbjOd(InspectorField inspectorField_0, Transform transform_1)
	{
		if (_pncm5aICs2gqTVriJ0a7tg.TryGetValue(smethod_17((object)inspectorField_0), out var value))
		{
			int num = value.Count - 1;
			while (num >= 0)
			{
				InspectorField inspectorField = value[num];
				value.RemoveAt(num);
				if (!smethod_10((UnityEngine.Object)inspectorField))
				{
					num--;
					continue;
				}
				smethod_23(smethod_22((Component)inspectorField), transform_1, bool_2: false);
				smethod_24(smethod_11((Component)inspectorField), bool_2: true);
				return inspectorField;
			}
		}
		InspectorField inspectorField2 = UnityEngine.Object.Instantiate(inspectorField_0, transform_1, worldPositionStays: false);
		inspectorField2.Initialize();
		return inspectorField2;
	}

	private InspectorField[] YyAvW_CxE2mX2XpJ6pI12e8ZGIOiJLpLNT5Tlkq46xGO(Type type_0, bool bool_2)
	{
		bool flag;
		if ((!(flag = bool_2 && smethod_26(smethod_25(typeof(UnityEngine.Object).TypeHandle), type_0)) || !HLv8Ac2tzpwPOvkuClTI7hh12Nze3kMIPuKsvCsKu2A7.TryGetValue(type_0, out var value)) && (flag || !dictionary_0.TryGetValue(type_0, out value)))
		{
			Dictionary<Type, InspectorField[]> dictionary = (flag ? HLv8Ac2tzpwPOvkuClTI7hh12Nze3kMIPuKsvCsKu2A7 : dictionary_0);
			_wDMB1WieQWD_Hcz1F6TVJw.Clear();
			for (int num = d2pQr3XMk7zMhqWE5dMM_3A.Length - 1; num >= 0; num--)
			{
				InspectorField[] array = (flag ? d2pQr3XMk7zMhqWE5dMM_3A[num].ReferenceDrawers : d2pQr3XMk7zMhqWE5dMM_3A[num].StandardDrawers);
				for (int num2 = array.Length - 1; num2 >= 0; num2--)
				{
					if (array[num2].SupportsType(type_0))
					{
						_wDMB1WieQWD_Hcz1F6TVJw.Add(array[num2]);
					}
				}
			}
			return dictionary[type_0] = ((_wDMB1WieQWD_Hcz1F6TVJw.Count > 0) ? _wDMB1WieQWD_Hcz1F6TVJw.ToArray() : null);
		}
		return value;
	}

	internal void OYu8MhBMQoTrQV4yEcQKTzM(InspectorField inspectorField_0)
	{
		if (!_pncm5aICs2gqTVriJ0a7tg.TryGetValue(smethod_17((object)inspectorField_0), out var value))
		{
			value = new List<InspectorField>(AsY6CPAhy9PP_S5UB1YWUZY);
			_pncm5aICs2gqTVriJ0a7tg[smethod_17((object)inspectorField_0)] = value;
		}
		if (value.Count >= AsY6CPAhy9PP_S5UB1YWUZY)
		{
			smethod_27((UnityEngine.Object)smethod_11((Component)inspectorField_0));
			return;
		}
		smethod_24(smethod_11((Component)inspectorField_0), bool_2: false);
		smethod_23(smethod_22((Component)inspectorField_0), transform_0, bool_2: false);
		value.Add(inspectorField_0);
	}

	internal void u49tk_5o69hcBJaEsqKE4KM(InspectorField inspectorField_0, PointerEventData pointerEventData_1, bool bool_2)
	{
		RuntimeInspectorUtils.HideTooltip();
		if (!bool_2)
		{
			if (smethod_6((UnityEngine.Object)KM8ROVsxwoOUATQ335tuOTU, (UnityEngine.Object)inspectorField_0))
			{
				KM8ROVsxwoOUATQ335tuOTU = null;
				pointerEventData_0 = null;
			}
		}
		else
		{
			KM8ROVsxwoOUATQ335tuOTU = inspectorField_0;
			pointerEventData_0 = pointerEventData_1;
			float_0 = smethod_13() + gicvr9O8_pDjUvsCz0oagkY;
		}
	}

	internal ExposedVariablesEnumerator method_0(Type type_0)
	{
		MemberInfo[] allVariables = type_0.GetAllVariables();
		if (allVariables == null)
		{
			return new ExposedVariablesEnumerator(null, null, null, VariableVisibility.None, VariableVisibility.None);
		}
		List<VariableSet> list = null;
		List<VariableSet> list2 = null;
		for (int i = 0; i < neWeZNYxgXJ_u090RDH51hU.Count; i++)
		{
			if (smethod_26(neWeZNYxgXJ_u090RDH51hU[i].type, type_0))
			{
				if (list != null)
				{
					list.Add(neWeZNYxgXJ_u090RDH51hU[i]);
					continue;
				}
				list = new List<VariableSet> { neWeZNYxgXJ_u090RDH51hU[i] };
			}
		}
		for (int j = 0; j < W_xn1OAeoB4vMsOCHaRArvozH2kxt1DhLgr7xvWhfqF8.Count; j++)
		{
			if (smethod_26(W_xn1OAeoB4vMsOCHaRArvozH2kxt1DhLgr7xvWhfqF8[j].type, type_0))
			{
				if (list2 == null)
				{
					list2 = new List<VariableSet> { W_xn1OAeoB4vMsOCHaRArvozH2kxt1DhLgr7xvWhfqF8[j] };
				}
				else
				{
					list2.Add(W_xn1OAeoB4vMsOCHaRArvozH2kxt1DhLgr7xvWhfqF8[j]);
				}
			}
		}
		return new ExposedVariablesEnumerator(allVariables, list, list2, b_0024q1nYo81juou9PA3y2mPrU, qY51peG6KDIxo6ZWMMP6xqGVkQfjsOqTGFEGpVTIUPXd);
	}

	[CompilerGenerated]
	private void rMDXkJO4sRd9xT9Mm8vbGaihDyUsGIX3akhmOXiJohBe(object object_0)
	{
		HOXgCOk8HiQ6U6JwRfP25nJJjdLLw4iqpuco6NByVXgk = object_0;
	}

	internal static bool smethod_3(UnityEngine.Object object_0, UnityEngine.Object object_1)
	{
		return object_0 != object_1;
	}

	internal static RectTransform smethod_4(ScrollRect scrollRect_0)
	{
		return scrollRect_0.content;
	}

	internal static GameObject smethod_5(string string_0)
	{
		return GameObject.Find(string_0);
	}

	internal static bool smethod_6(UnityEngine.Object object_0, UnityEngine.Object object_1)
	{
		return object_0 == object_1;
	}

	internal static GameObject smethod_7(string string_0)
	{
		return new GameObject(string_0);
	}

	internal static void smethod_8(UnityEngine.Object object_0)
	{
		UnityEngine.Object.DontDestroyOnLoad(object_0);
	}

	internal static Transform smethod_9(GameObject gameObject_0)
	{
		return gameObject_0.transform;
	}

	internal static bool smethod_10(UnityEngine.Object object_0)
	{
		return object_0;
	}

	internal static GameObject smethod_11(Component component_0)
	{
		return component_0.gameObject;
	}

	internal static void smethod_12(UnityEngine.Object object_0)
	{
		UnityEngine.Object.DestroyImmediate(object_0);
	}

	internal static float smethod_13()
	{
		return Time.realtimeSinceStartup;
	}

	internal static Vector2 smethod_14(PointerEventData pointerEventData_1)
	{
		return pointerEventData_1.delta;
	}

	internal static bool smethod_15(GameObject gameObject_0)
	{
		return gameObject_0.activeSelf;
	}

	internal static void smethod_16(Graphic graphic_0, Color color_0)
	{
		graphic_0.color = color_0;
	}

	internal static Type smethod_17(object object_0)
	{
		return object_0.GetType();
	}

	internal static bool smethod_18(Type type_0)
	{
		return type_0.IsValueType;
	}

	internal static void smethod_19(object object_0)
	{
		Debug.LogError(object_0);
	}

	internal static GameObject smethod_20(Component component_0)
	{
		return component_0.gameObject;
	}

	internal static void smethod_21(ScrollRect scrollRect_0, float float_1)
	{
		scrollRect_0.verticalNormalizedPosition = float_1;
	}

	internal static Transform smethod_22(Component component_0)
	{
		return component_0.transform;
	}

	internal static void smethod_23(Transform transform_1, Transform transform_2, bool bool_2)
	{
		transform_1.SetParent(transform_2, bool_2);
	}

	internal static void smethod_24(GameObject gameObject_0, bool bool_2)
	{
		gameObject_0.SetActive(bool_2);
	}

	internal static Type smethod_25(RuntimeTypeHandle runtimeTypeHandle_0)
	{
		return Type.GetTypeFromHandle(runtimeTypeHandle_0);
	}

	internal static bool smethod_26(Type type_0, Type type_1)
	{
		return type_0.IsAssignableFrom(type_1);
	}

	internal static void smethod_27(UnityEngine.Object object_0)
	{
		UnityEngine.Object.Destroy(object_0);
	}
}
