using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RuntimeInspectorNamespace;

public class ArrayField : ExpandableInspectorField, IEventSystemHandler, IDropHandler
{
	[CompilerGenerated]
	private sealed class ePX4zyrcXsAAb313rcZ5nIBostwqsfUsWese0_Ets3F_0024vXchWXniffl31fedKZx2VnUuqUHUXNIMvGerHXYMOv4
	{
		public int agWIG15_0024PoywajNqgfZ4ch8;

		public ArrayField SKCFxHGAEbVQbKCDB_0024Jj8p4;

		internal object method_0()
		{
			return smethod_0((Array)SKCFxHGAEbVQbKCDB_0024Jj8p4.Value, agWIG15_0024PoywajNqgfZ4ch8);
		}

		internal void method_1(object object_0)
		{
			Array array = (Array)SKCFxHGAEbVQbKCDB_0024Jj8p4.Value;
			smethod_1(array, object_0, agWIG15_0024PoywajNqgfZ4ch8);
			SKCFxHGAEbVQbKCDB_0024Jj8p4.Value = array;
		}

		internal static object smethod_0(Array array_0, int int_0)
		{
			return array_0.GetValue(int_0);
		}

		internal static void smethod_1(Array array_0, object object_0, int int_0)
		{
			array_0.SetValue(object_0, int_0);
		}
	}

	[CompilerGenerated]
	private sealed class eUsSKw_9Q3f_QIma3mlhmC5xWMSwNTc6a9SCRA9vnA3tR6YUqSP2FiPV49WEZzvVa_0024q55tEWpA3ElRrjTkWqRgA
	{
		public int agWIG15_0024PoywajNqgfZ4ch8;

		public ArrayField SKCFxHGAEbVQbKCDB_0024Jj8p4;

		internal object method_0()
		{
			return smethod_0((IList)SKCFxHGAEbVQbKCDB_0024Jj8p4.Value, agWIG15_0024PoywajNqgfZ4ch8);
		}

		internal void method_1(object object_0)
		{
			IList list = (IList)SKCFxHGAEbVQbKCDB_0024Jj8p4.Value;
			smethod_1(list, agWIG15_0024PoywajNqgfZ4ch8, object_0);
			SKCFxHGAEbVQbKCDB_0024Jj8p4.Value = list;
		}

		internal static object smethod_0(IList ilist_0, int int_0)
		{
			return ilist_0[int_0];
		}

		internal static void smethod_1(IList ilist_0, int int_0, object object_0)
		{
			ilist_0[int_0] = object_0;
		}
	}

	[SerializeField]
	private LayoutElement RQuKXRhnTUqzjZv3EMcnyZOcEpbwm1MuxHKBAN4qoz1k;

	[SerializeField]
	private Text erwtHn_p0gMs_nKjYJprdRo;

	[SerializeField]
	private BoundInputField FSU0_0024Yq3shfsbc5jd8fcWSg;

	private bool X9V4_0024oMC3sLhet5NkSW3PEI;

	private Type sjC_c35_IQ8WTiK7931GStU;

	private readonly List<bool> IlmeVmfluFnE9QxKLwXA9vwipYe5ExOjuGEChtqHwhqv = new List<bool>();

	protected override int Length
	{
		get
		{
			if (X9V4_0024oMC3sLhet5NkSW3PEI)
			{
				Array array = (Array)base.Value;
				if (array != null)
				{
					return smethod_33(array);
				}
			}
			else
			{
				IList list = (IList)base.Value;
				if (list != null)
				{
					return smethod_34((ICollection)list);
				}
			}
			return 0;
		}
	}

	public override void Initialize()
	{
		base.Initialize();
		FSU0_0024Yq3shfsbc5jd8fcWSg.Initialize();
		BoundInputField fSU0_0024Yq3shfsbc5jd8fcWSg = FSU0_0024Yq3shfsbc5jd8fcWSg;
		fSU0_0024Yq3shfsbc5jd8fcWSg.OnValueChanged = (BoundInputField.OnValueChangedDelegate)smethod_35((Delegate)fSU0_0024Yq3shfsbc5jd8fcWSg.OnValueChanged, (Delegate)new BoundInputField.OnValueChangedDelegate(method_0));
		BoundInputField fSU0_0024Yq3shfsbc5jd8fcWSg2 = FSU0_0024Yq3shfsbc5jd8fcWSg;
		fSU0_0024Yq3shfsbc5jd8fcWSg2.OnValueSubmitted = (BoundInputField.OnValueChangedDelegate)smethod_35((Delegate)fSU0_0024Yq3shfsbc5jd8fcWSg2.OnValueSubmitted, (Delegate)new BoundInputField.OnValueChangedDelegate(sJNdGble4OHV9pUoLiDOzEo));
		FSU0_0024Yq3shfsbc5jd8fcWSg.DefaultEmptyValue = global::_003CModule_003E.smethod_29<string>(1755123841u);
		FSU0_0024Yq3shfsbc5jd8fcWSg.CacheTextOnValueChange = false;
	}

	public override bool SupportsType(Type type)
	{
		if (smethod_36(type) && smethod_37(type) == 1)
		{
			return true;
		}
		if (!smethod_38(type))
		{
			return false;
		}
		return smethod_39(type) == smethod_40(typeof(List<>).TypeHandle);
	}

	protected override void OnBound(MemberInfo variable)
	{
		base.OnBound(variable);
		X9V4_0024oMC3sLhet5NkSW3PEI = smethod_36(base.BoundVariableType);
		sjC_c35_IQ8WTiK7931GStU = (X9V4_0024oMC3sLhet5NkSW3PEI ? smethod_42(base.BoundVariableType) : smethod_41(base.BoundVariableType)[0]);
	}

	protected override void OnUnbound()
	{
		base.OnUnbound();
		FSU0_0024Yq3shfsbc5jd8fcWSg.Text = global::_003CModule_003E.smethod_26<string>(341714943u);
		IlmeVmfluFnE9QxKLwXA9vwipYe5ExOjuGEChtqHwhqv.Clear();
	}

	protected override void OnSkinChanged()
	{
		base.OnSkinChanged();
		FSU0_0024Yq3shfsbc5jd8fcWSg.Skin = base.Skin;
		RQuKXRhnTUqzjZv3EMcnyZOcEpbwm1MuxHKBAN4qoz1k.SetHeight(base.Skin.LineHeight);
		erwtHn_p0gMs_nKjYJprdRo.SetSkinText(base.Skin);
		Vector2 vector2_ = new Vector2(base.Skin.LabelWidthPercentage, 0f);
		smethod_44(smethod_43((Graphic)variableNameMask), vector2_);
		smethod_44((RectTransform)smethod_45((Component)FSU0_0024Yq3shfsbc5jd8fcWSg), vector2_);
	}

	protected override void OnDepthChanged()
	{
		base.OnDepthChanged();
		smethod_43((Graphic)erwtHn_p0gMs_nKjYJprdRo).sizeDelta = new Vector2(-base.Skin.IndentAmount * (base.Depth + 1), 0f);
	}

	protected override void ClearElements()
	{
		IlmeVmfluFnE9QxKLwXA9vwipYe5ExOjuGEChtqHwhqv.Clear();
		for (int i = 0; i < elements.Count; i++)
		{
			IlmeVmfluFnE9QxKLwXA9vwipYe5ExOjuGEChtqHwhqv.Add(elements[i] is ExpandableInspectorField && ((ExpandableInspectorField)elements[i]).IsExpanded);
		}
		base.ClearElements();
	}

	protected override void GenerateElements()
	{
		if (base.Value == null)
		{
			return;
		}
		if (!X9V4_0024oMC3sLhet5NkSW3PEI)
		{
			IList list = (IList)base.Value;
			for (int i = 0; i < list.Count; i++)
			{
				InspectorField inspectorField = base.Inspector.CreateDrawerForType(sjC_c35_IQ8WTiK7931GStU, drawArea, base.Depth + 1);
				if (inspectorField == null)
				{
					break;
				}
				int int_ = i;
				string variableName = (base.Inspector.ArrayIndicesStartAtOne ? (i + 1 + global::_003CModule_003E.smethod_28<string>(2167813015u)) : (i + global::_003CModule_003E.smethod_27<string>(3737871438u)));
				inspectorField.BindTo(sjC_c35_IQ8WTiK7931GStU, variableName, () => eUsSKw_9Q3f_QIma3mlhmC5xWMSwNTc6a9SCRA9vnA3tR6YUqSP2FiPV49WEZzvVa_0024q55tEWpA3ElRrjTkWqRgA.smethod_0((IList)base.Value, int_), delegate(object object_0)
				{
					IList list2 = (IList)base.Value;
					eUsSKw_9Q3f_QIma3mlhmC5xWMSwNTc6a9SCRA9vnA3tR6YUqSP2FiPV49WEZzvVa_0024q55tEWpA3ElRrjTkWqRgA.smethod_1(list2, int_, object_0);
					base.Value = list2;
				});
				if (i < IlmeVmfluFnE9QxKLwXA9vwipYe5ExOjuGEChtqHwhqv.Count && IlmeVmfluFnE9QxKLwXA9vwipYe5ExOjuGEChtqHwhqv[i] && inspectorField is ExpandableInspectorField)
				{
					((ExpandableInspectorField)inspectorField).IsExpanded = true;
				}
				elements.Add(inspectorField);
			}
		}
		else
		{
			Array array = (Array)base.Value;
			for (int num = 0; num < array.Length; num++)
			{
				InspectorField inspectorField2 = base.Inspector.CreateDrawerForType(sjC_c35_IQ8WTiK7931GStU, drawArea, base.Depth + 1);
				if (smethod_46((UnityEngine.Object)inspectorField2, (UnityEngine.Object)null))
				{
					break;
				}
				int int_2 = num;
				inspectorField2.BindTo(sjC_c35_IQ8WTiK7931GStU, string.Empty, () => ePX4zyrcXsAAb313rcZ5nIBostwqsfUsWese0_Ets3F_0024vXchWXniffl31fedKZx2VnUuqUHUXNIMvGerHXYMOv4.smethod_0((Array)base.Value, int_2), delegate(object object_0)
				{
					Array array2 = (Array)base.Value;
					ePX4zyrcXsAAb313rcZ5nIBostwqsfUsWese0_Ets3F_0024vXchWXniffl31fedKZx2VnUuqUHUXNIMvGerHXYMOv4.smethod_1(array2, object_0, int_2);
					base.Value = array2;
				});
				if (num < IlmeVmfluFnE9QxKLwXA9vwipYe5ExOjuGEChtqHwhqv.Count && IlmeVmfluFnE9QxKLwXA9vwipYe5ExOjuGEChtqHwhqv[num] && inspectorField2 is ExpandableInspectorField)
				{
					((ExpandableInspectorField)inspectorField2).IsExpanded = true;
				}
				inspectorField2.NameRaw = (base.Inspector.ArrayIndicesStartAtOne ? (num + 1 + global::_003CModule_003E.smethod_27<string>(3737871438u)) : (num + global::_003CModule_003E.smethod_28<string>(2167813015u)));
				elements.Add(inspectorField2);
			}
		}
		FSU0_0024Yq3shfsbc5jd8fcWSg.Text = Length.ToString();
		IlmeVmfluFnE9QxKLwXA9vwipYe5ExOjuGEChtqHwhqv.Clear();
	}

	void IDropHandler.OnDrop(PointerEventData eventData)
	{
		UnityEngine.Object assignableObjectFromDraggedReferenceItem = RuntimeInspectorUtils.GetAssignableObjectFromDraggedReferenceItem(eventData, sjC_c35_IQ8WTiK7931GStU);
		if (smethod_47(assignableObjectFromDraggedReferenceItem, (UnityEngine.Object)null) && sJNdGble4OHV9pUoLiDOzEo(null, (Length + 1).ToString()))
		{
			if (X9V4_0024oMC3sLhet5NkSW3PEI)
			{
				Array array = (Array)base.Value;
				array.SetValue(assignableObjectFromDraggedReferenceItem, Length - 1);
				base.Value = array;
			}
			else
			{
				IList list = (IList)base.Value;
				list[Length - 1] = assignableObjectFromDraggedReferenceItem;
				base.Value = list;
			}
			if (!base.IsExpanded)
			{
				base.IsExpanded = true;
			}
		}
	}

	private bool method_0(BoundInputField boundInputField_0, string string_0)
	{
		if (int.TryParse(string_0, out var result) && result >= 0)
		{
			return true;
		}
		return false;
	}

	private bool sJNdGble4OHV9pUoLiDOzEo(BoundInputField boundInputField_0, string string_0)
	{
		if (int.TryParse(string_0, out var result) && result >= 0)
		{
			int length = Length;
			if (length != result)
			{
				if (X9V4_0024oMC3sLhet5NkSW3PEI)
				{
					Array array = (Array)base.Value;
					Array array2 = smethod_48(smethod_42(base.BoundVariableType), result);
					if (result > length)
					{
						if (array != null)
						{
							smethod_49(array, 0, array2, 0, length);
						}
						for (int i = length; i < result; i++)
						{
							object obj = cG7Lhou31a3QPJgFxV8dl4g3TD4wkVsEnB4KgUj4rl7A(array);
							if (obj != null)
							{
								smethod_50(array2, obj, i);
							}
						}
					}
					else
					{
						smethod_49(array, 0, array2, 0, result);
					}
					base.Value = array2;
				}
				else
				{
					IList list = (IList)base.Value;
					int num = result - length;
					if (num <= 0)
					{
						for (int num2 = 0; num2 > num; num2--)
						{
							smethod_54(list, smethod_34((ICollection)list) - 1);
						}
					}
					else
					{
						if (list == null)
						{
							list = (IList)smethod_52(smethod_51(smethod_40(typeof(List<>).TypeHandle), new Type[1] { smethod_41(base.BoundVariableType)[0] }));
						}
						for (int j = 0; j < num; j++)
						{
							smethod_53(list, cG7Lhou31a3QPJgFxV8dl4g3TD4wkVsEnB4KgUj4rl7A(list));
						}
					}
					base.Value = list;
				}
				base.Inspector.RefreshDelayed();
			}
			return true;
		}
		return false;
	}

	private object cG7Lhou31a3QPJgFxV8dl4g3TD4wkVsEnB4KgUj4rl7A(object object_0)
	{
		Array array = null;
		IList list = null;
		if (X9V4_0024oMC3sLhet5NkSW3PEI)
		{
			array = (Array)object_0;
		}
		else
		{
			list = (IList)object_0;
		}
		object obj = null;
		Type type = (X9V4_0024oMC3sLhet5NkSW3PEI ? smethod_42(base.BoundVariableType) : smethod_41(base.BoundVariableType)[0]);
		if (!smethod_55(type))
		{
			if (!smethod_58(smethod_40(typeof(UnityEngine.Object).TypeHandle), type))
			{
				if (smethod_36(type))
				{
					return smethod_48(type, 0);
				}
				if (!smethod_38(type) || smethod_39(type) != smethod_40(typeof(List<>).TypeHandle))
				{
					return type.Instantiate();
				}
				return smethod_52(smethod_51(smethod_40(typeof(List<>).TypeHandle), new Type[1] { type }));
			}
			if (X9V4_0024oMC3sLhet5NkSW3PEI && array != null && smethod_33(array) > 0)
			{
				return smethod_56(array, smethod_33(array) - 1);
			}
			if (!X9V4_0024oMC3sLhet5NkSW3PEI && list != null && smethod_34((ICollection)list) > 0)
			{
				return smethod_57(list, smethod_34((ICollection)list) - 1);
			}
			return null;
		}
		if (!X9V4_0024oMC3sLhet5NkSW3PEI || array == null || smethod_33(array) <= 0)
		{
			if (!X9V4_0024oMC3sLhet5NkSW3PEI && list != null && smethod_34((ICollection)list) > 0)
			{
				return smethod_57(list, smethod_34((ICollection)list) - 1);
			}
			return smethod_52(type);
		}
		return smethod_56(array, smethod_33(array) - 1);
	}

	internal static int smethod_33(Array array_0)
	{
		return array_0.Length;
	}

	internal static int smethod_34(ICollection icollection_0)
	{
		return icollection_0.Count;
	}

	internal static Delegate smethod_35(Delegate delegate_0, Delegate delegate_1)
	{
		return Delegate.Combine(delegate_0, delegate_1);
	}

	internal static bool smethod_36(Type type_0)
	{
		return type_0.IsArray;
	}

	internal static int smethod_37(Type type_0)
	{
		return type_0.GetArrayRank();
	}

	internal static bool smethod_38(Type type_0)
	{
		return type_0.IsGenericType;
	}

	internal static Type smethod_39(Type type_0)
	{
		return type_0.GetGenericTypeDefinition();
	}

	internal static Type smethod_40(RuntimeTypeHandle runtimeTypeHandle_0)
	{
		return Type.GetTypeFromHandle(runtimeTypeHandle_0);
	}

	internal static Type[] smethod_41(Type type_0)
	{
		return type_0.GetGenericArguments();
	}

	internal static Type smethod_42(Type type_0)
	{
		return type_0.GetElementType();
	}

	internal static RectTransform smethod_43(Graphic graphic_0)
	{
		return graphic_0.rectTransform;
	}

	internal static void smethod_44(RectTransform rectTransform_0, Vector2 vector2_0)
	{
		rectTransform_0.anchorMin = vector2_0;
	}

	internal static Transform smethod_45(Component component_0)
	{
		return component_0.transform;
	}

	internal static bool smethod_46(UnityEngine.Object object_0, UnityEngine.Object object_1)
	{
		return object_0 == object_1;
	}

	internal static bool smethod_47(UnityEngine.Object object_0, UnityEngine.Object object_1)
	{
		return object_0 != object_1;
	}

	internal static Array smethod_48(Type type_0, int int_0)
	{
		return Array.CreateInstance(type_0, int_0);
	}

	internal static void smethod_49(Array array_0, int int_0, Array array_1, int int_1, int int_2)
	{
		Array.ConstrainedCopy(array_0, int_0, array_1, int_1, int_2);
	}

	internal static void smethod_50(Array array_0, object object_0, int int_0)
	{
		array_0.SetValue(object_0, int_0);
	}

	internal static Type smethod_51(Type type_0, Type[] type_1)
	{
		return type_0.MakeGenericType(type_1);
	}

	internal static object smethod_52(Type type_0)
	{
		return Activator.CreateInstance(type_0);
	}

	internal static int smethod_53(IList ilist_0, object object_0)
	{
		return ilist_0.Add(object_0);
	}

	internal static void smethod_54(IList ilist_0, int int_0)
	{
		ilist_0.RemoveAt(int_0);
	}

	internal static bool smethod_55(Type type_0)
	{
		return type_0.IsValueType;
	}

	internal static object smethod_56(Array array_0, int int_0)
	{
		return array_0.GetValue(int_0);
	}

	internal static object smethod_57(IList ilist_0, int int_0)
	{
		return ilist_0[int_0];
	}

	internal static bool smethod_58(Type type_0, Type type_1)
	{
		return type_0.IsAssignableFrom(type_1);
	}
}
