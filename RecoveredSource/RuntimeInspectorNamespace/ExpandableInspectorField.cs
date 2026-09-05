using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace RuntimeInspectorNamespace;

public abstract class ExpandableInspectorField : InspectorField
{
	[SerializeField]
	protected RectTransform drawArea;

	[SerializeField]
	private PointerEventListener Dio9gza_0024qoJVEKQZOKo2aiI;

	private RectTransform lohXif4iOct1rDqZsLNhI_0024wP0mP0M5sypL8HRGgGrNcA;

	[SerializeField]
	private LayoutGroup KQgFCu5mCvHtuAgQhjdfDlc;

	[SerializeField]
	private Image t4uHfbtZWoxMU6vX9SaY3fc;

	protected readonly List<InspectorField> elements = new List<InspectorField>(8);

	private readonly List<ExposedMethodField> bs4CdvyFN6T1eTlGCsI03SI = new List<ExposedMethodField>();

	private bool bool_0;

	private RuntimeInspector.HeaderVisibility oZgKPYrNKGq9TyYWUscrE1Lm8nui6P2LJVff9NkG9W9g;

	protected virtual int Length => elements.Count;

	public override bool ShouldRefresh => true;

	public bool IsExpanded
	{
		get
		{
			return bool_0;
		}
		set
		{
			bool_0 = value;
			smethod_17(smethod_16((Component)drawArea), bool_0);
			if (smethod_18((UnityEngine.Object)t4uHfbtZWoxMU6vX9SaY3fc, (UnityEngine.Object)null))
			{
				smethod_19((Graphic)t4uHfbtZWoxMU6vX9SaY3fc).localEulerAngles = (bool_0 ? new Vector3(0f, 0f, -90f) : Vector3.zero);
			}
			if (bool_0)
			{
				Refresh();
			}
		}
	}

	public RuntimeInspector.HeaderVisibility HeaderVisibility
	{
		get
		{
			return oZgKPYrNKGq9TyYWUscrE1Lm8nui6P2LJVff9NkG9W9g;
		}
		set
		{
			if (oZgKPYrNKGq9TyYWUscrE1Lm8nui6P2LJVff9NkG9W9g == value)
			{
				return;
			}
			if (oZgKPYrNKGq9TyYWUscrE1Lm8nui6P2LJVff9NkG9W9g == RuntimeInspector.HeaderVisibility.Hidden)
			{
				base.Depth++;
				smethod_21(smethod_20(KQgFCu5mCvHtuAgQhjdfDlc), base.Skin.LineHeight);
				smethod_17(smethod_16((Component)Dio9gza_0024qoJVEKQZOKo2aiI), bool_1: true);
			}
			else if (value == RuntimeInspector.HeaderVisibility.Hidden)
			{
				base.Depth--;
				smethod_21(smethod_20(KQgFCu5mCvHtuAgQhjdfDlc), 0);
				smethod_17(smethod_16((Component)Dio9gza_0024qoJVEKQZOKo2aiI), bool_1: false);
			}
			oZgKPYrNKGq9TyYWUscrE1Lm8nui6P2LJVff9NkG9W9g = value;
			if (oZgKPYrNKGq9TyYWUscrE1Lm8nui6P2LJVff9NkG9W9g != RuntimeInspector.HeaderVisibility.Collapsible)
			{
				if (oZgKPYrNKGq9TyYWUscrE1Lm8nui6P2LJVff9NkG9W9g == RuntimeInspector.HeaderVisibility.AlwaysVisible)
				{
					if (t4uHfbtZWoxMU6vX9SaY3fc != null)
					{
						t4uHfbtZWoxMU6vX9SaY3fc.gameObject.SetActive(value: false);
					}
					((RectTransform)variableNameText.transform).sizeDelta = new Vector2(0f, 0f);
					if (!bool_0)
					{
						IsExpanded = true;
					}
				}
				else if (!bool_0)
				{
					IsExpanded = true;
				}
			}
			else
			{
				if (smethod_18((UnityEngine.Object)t4uHfbtZWoxMU6vX9SaY3fc, (UnityEngine.Object)null))
				{
					smethod_17(smethod_16((Component)t4uHfbtZWoxMU6vX9SaY3fc), bool_1: true);
				}
				((RectTransform)smethod_22((Component)variableNameText)).sizeDelta = new Vector2(-35f, 0f);
			}
		}
	}

	public override void Initialize()
	{
		base.Initialize();
		lohXif4iOct1rDqZsLNhI_0024wP0mP0M5sypL8HRGgGrNcA = (RectTransform)smethod_22((Component)Dio9gza_0024qoJVEKQZOKo2aiI);
		Dio9gza_0024qoJVEKQZOKo2aiI.PointerClick += delegate
		{
			if (oZgKPYrNKGq9TyYWUscrE1Lm8nui6P2LJVff9NkG9W9g == RuntimeInspector.HeaderVisibility.Collapsible)
			{
				IsExpanded = !bool_0;
			}
		};
		IsExpanded = bool_0;
	}

	protected override void OnUnbound()
	{
		base.OnUnbound();
		IsExpanded = false;
		ClearElements();
	}

	protected override void OnSkinChanged()
	{
		base.OnSkinChanged();
		Vector2 vector2_ = smethod_23(lohXif4iOct1rDqZsLNhI_0024wP0mP0M5sypL8HRGgGrNcA);
		vector2_.y = base.Skin.LineHeight;
		smethod_24(lohXif4iOct1rDqZsLNhI_0024wP0mP0M5sypL8HRGgGrNcA, vector2_);
		if (oZgKPYrNKGq9TyYWUscrE1Lm8nui6P2LJVff9NkG9W9g != RuntimeInspector.HeaderVisibility.Hidden)
		{
			smethod_21(smethod_20(KQgFCu5mCvHtuAgQhjdfDlc), base.Skin.LineHeight);
		}
		if (smethod_18((UnityEngine.Object)t4uHfbtZWoxMU6vX9SaY3fc, (UnityEngine.Object)null))
		{
			smethod_25((Graphic)t4uHfbtZWoxMU6vX9SaY3fc, base.Skin.ExpandArrowColor);
		}
		for (int i = 0; i < elements.Count; i++)
		{
			elements[i].Skin = base.Skin;
		}
		for (int j = 0; j < bs4CdvyFN6T1eTlGCsI03SI.Count; j++)
		{
			bs4CdvyFN6T1eTlGCsI03SI[j].Skin = base.Skin;
		}
	}

	protected override void OnDepthChanged()
	{
		Vector2 vector2_ = smethod_23(lohXif4iOct1rDqZsLNhI_0024wP0mP0M5sypL8HRGgGrNcA);
		vector2_.x = -base.Skin.IndentAmount * base.Depth;
		smethod_24(lohXif4iOct1rDqZsLNhI_0024wP0mP0M5sypL8HRGgGrNcA, vector2_);
		for (int i = 0; i < elements.Count; i++)
		{
			elements[i].Depth = base.Depth + 1;
		}
	}

	protected void RegenerateElements()
	{
		if (elements.Count > 0 || bs4CdvyFN6T1eTlGCsI03SI.Count > 0)
		{
			ClearElements();
		}
		if (base.Depth < base.Inspector.NestLimit)
		{
			smethod_17(smethod_16((Component)drawArea), bool_1: true);
			GenerateElements();
			yeXjufI8RlttmOkY6Uq7oN4();
			smethod_17(smethod_16((Component)drawArea), bool_0);
		}
	}

	protected abstract void GenerateElements();

	private void yeXjufI8RlttmOkY6Uq7oN4()
	{
		ExposedMethod[] exposedMethods = base.BoundVariableType.GetExposedMethods();
		if (exposedMethods == null)
		{
			return;
		}
		bool flag = base.Value != null && !smethod_26(base.Value, (object)null);
		for (int i = 0; i < exposedMethods.Length; i++)
		{
			ExposedMethod boundMethod = exposedMethods[i];
			if ((!flag || !boundMethod.VisibleWhenInitialized) && (flag || !boundMethod.VisibleWhenUninitialized))
			{
				continue;
			}
			ExposedMethodField exposedMethodField = (ExposedMethodField)base.Inspector.CreateDrawerForType(smethod_27(typeof(ExposedMethod).TypeHandle), drawArea, base.Depth + 1, drawObjectsAsFields: false);
			if (smethod_18((UnityEngine.Object)exposedMethodField, (UnityEngine.Object)null))
			{
				exposedMethodField.BindTo(smethod_27(typeof(ExposedMethod).TypeHandle), string.Empty, () => base.Value, delegate(object object_0)
				{
					base.Value = object_0;
				});
				exposedMethodField.SetBoundMethod(boundMethod);
				bs4CdvyFN6T1eTlGCsI03SI.Add(exposedMethodField);
			}
		}
	}

	protected virtual void ClearElements()
	{
		for (int i = 0; i < elements.Count; i++)
		{
			elements[i].Unbind();
		}
		for (int j = 0; j < bs4CdvyFN6T1eTlGCsI03SI.Count; j++)
		{
			bs4CdvyFN6T1eTlGCsI03SI[j].Unbind();
		}
		elements.Clear();
		bs4CdvyFN6T1eTlGCsI03SI.Clear();
	}

	public override void Refresh()
	{
		base.Refresh();
		if (!bool_0)
		{
			return;
		}
		if (Length != elements.Count)
		{
			RegenerateElements();
		}
		for (int i = 0; i < elements.Count; i++)
		{
			if (elements[i].ShouldRefresh)
			{
				elements[i].Refresh();
			}
		}
	}

	protected InspectorField CreateDrawerForComponent(Component component, string variableName = null)
	{
		InspectorField inspectorField = base.Inspector.CreateDrawerForType(smethod_28((object)component), drawArea, base.Depth + 1, drawObjectsAsFields: false);
		if (smethod_18((UnityEngine.Object)inspectorField, (UnityEngine.Object)null))
		{
			if (variableName == null)
			{
				variableName = smethod_30(smethod_29((MemberInfo)smethod_28((object)component)), global::_003CModule_003E.smethod_29<string>(738445055u));
			}
			inspectorField.BindTo(smethod_28((object)component), string.Empty, () => component, delegate
			{
			});
			inspectorField.NameRaw = variableName;
			elements.Add(inspectorField);
		}
		return inspectorField;
	}

	protected InspectorField CreateDrawerForVariable(MemberInfo variable, string variableName = null)
	{
		Type type = ((variable is FieldInfo) ? smethod_32((FieldInfo)variable) : smethod_31((PropertyInfo)variable));
		InspectorField inspectorField = base.Inspector.CreateDrawerForType(type, drawArea, base.Depth + 1, drawObjectsAsFields: true, variable);
		if (smethod_18((UnityEngine.Object)inspectorField, (UnityEngine.Object)null))
		{
			inspectorField.BindTo(this, variable, variableName);
			elements.Add(inspectorField);
		}
		return inspectorField;
	}

	protected InspectorField CreateDrawer(Type variableType, string variableName, Getter getter, Setter setter, bool drawObjectsAsFields = true)
	{
		InspectorField inspectorField = base.Inspector.CreateDrawerForType(variableType, drawArea, base.Depth + 1, drawObjectsAsFields);
		if (smethod_18((UnityEngine.Object)inspectorField, (UnityEngine.Object)null))
		{
			inspectorField.BindTo(variableType, variableName, getter, setter);
			elements.Add(inspectorField);
		}
		return inspectorField;
	}

	internal static GameObject smethod_16(Component component_0)
	{
		return component_0.gameObject;
	}

	internal static void smethod_17(GameObject gameObject_0, bool bool_1)
	{
		gameObject_0.SetActive(bool_1);
	}

	internal static bool smethod_18(UnityEngine.Object object_0, UnityEngine.Object object_1)
	{
		return object_0 != object_1;
	}

	internal static RectTransform smethod_19(Graphic graphic_0)
	{
		return graphic_0.rectTransform;
	}

	internal static RectOffset smethod_20(LayoutGroup layoutGroup_0)
	{
		return layoutGroup_0.padding;
	}

	internal static void smethod_21(RectOffset rectOffset_0, int int_0)
	{
		rectOffset_0.top = int_0;
	}

	internal static Transform smethod_22(Component component_0)
	{
		return component_0.transform;
	}

	internal static Vector2 smethod_23(RectTransform rectTransform_0)
	{
		return rectTransform_0.sizeDelta;
	}

	internal static void smethod_24(RectTransform rectTransform_0, Vector2 vector2_0)
	{
		rectTransform_0.sizeDelta = vector2_0;
	}

	internal static void smethod_25(Graphic graphic_0, Color color_0)
	{
		graphic_0.color = color_0;
	}

	internal static bool smethod_26(object object_0, object object_1)
	{
		return object_0.Equals(object_1);
	}

	internal static Type smethod_27(RuntimeTypeHandle runtimeTypeHandle_0)
	{
		return Type.GetTypeFromHandle(runtimeTypeHandle_0);
	}

	internal static Type smethod_28(object object_0)
	{
		return object_0.GetType();
	}

	internal static string smethod_29(MemberInfo memberInfo_0)
	{
		return memberInfo_0.Name;
	}

	internal static string smethod_30(string string_0, string string_1)
	{
		return string_0 + string_1;
	}

	internal static Type smethod_31(PropertyInfo propertyInfo_0)
	{
		return propertyInfo_0.PropertyType;
	}

	internal static Type smethod_32(FieldInfo fieldInfo_0)
	{
		return fieldInfo_0.FieldType;
	}
}
