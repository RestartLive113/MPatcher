using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace RuntimeInspectorNamespace;

public abstract class InspectorField : MonoBehaviour
{
	public delegate object Getter();

	public delegate void Setter(object value);

	[CompilerGenerated]
	private sealed class Jv_D8RiwejFmae97_gpccsUGN0e4TQoboWS8vAIMw7CAbYDHXw5Z8EYMxC_0024kptM0HvIQwX34RUCYEaE45F2jU4M
	{
		public InspectorField Uih6Qw3PXLiUfOw1wbzKC_U;
	}

	[CompilerGenerated]
	private sealed class J_0024KDWMcBIDqIwJ3uI8QWlGEYkNnTupwRe_cwtS_hSaxBEgxmu9CtGKYz3S4mJtOpjzryuoi9YKIE8RXeYXB0LS8
	{
		public FieldInfo jlkguRl3NGG5witjl_tIwd0;

		public Jv_D8RiwejFmae97_gpccsUGN0e4TQoboWS8vAIMw7CAbYDHXw5Z8EYMxC_0024kptM0HvIQwX34RUCYEaE45F2jU4M g0mQ_0024tSzvWAIzvC2JeS9zbk;

		internal object JNPkb7W429sQOI5sHyIaz_0024U()
		{
			return smethod_0(jlkguRl3NGG5witjl_tIwd0, g0mQ_0024tSzvWAIzvC2JeS9zbk.Uih6Qw3PXLiUfOw1wbzKC_U.Value);
		}

		internal void JSvA0w6_0024t0OmJyC_0024rfHkoEQ(object object_0)
		{
			smethod_1(jlkguRl3NGG5witjl_tIwd0, g0mQ_0024tSzvWAIzvC2JeS9zbk.Uih6Qw3PXLiUfOw1wbzKC_U.Value, object_0);
		}

		internal object JlQmfs5cr7MobBC4mWGRR9w()
		{
			return smethod_0(jlkguRl3NGG5witjl_tIwd0, g0mQ_0024tSzvWAIzvC2JeS9zbk.Uih6Qw3PXLiUfOw1wbzKC_U.Value);
		}

		internal void J6mclYBeE9hd3gHFMS1nAJk(object object_0)
		{
			smethod_1(jlkguRl3NGG5witjl_tIwd0, g0mQ_0024tSzvWAIzvC2JeS9zbk.Uih6Qw3PXLiUfOw1wbzKC_U.Value, object_0);
			g0mQ_0024tSzvWAIzvC2JeS9zbk.Uih6Qw3PXLiUfOw1wbzKC_U.Value = g0mQ_0024tSzvWAIzvC2JeS9zbk.Uih6Qw3PXLiUfOw1wbzKC_U.Value;
		}

		internal static object smethod_0(FieldInfo fieldInfo_0, object object_0)
		{
			return fieldInfo_0.GetValue(object_0);
		}

		internal static void smethod_1(FieldInfo fieldInfo_0, object object_0, object object_1)
		{
			fieldInfo_0.SetValue(object_0, object_1);
		}
	}

	[CompilerGenerated]
	private sealed class KNwddQhNbriB7aVZU8HpfkigH_uDSlHNN6Zxs_Lid1bHcGP5VFohQYZdE_0024lbvn5lFBhwrU_0024VCZWsLizrTtJsTtQ
	{
		public PropertyInfo vSGuJdYhWIbvxnmdxcu2UkA;

		public Jv_D8RiwejFmae97_gpccsUGN0e4TQoboWS8vAIMw7CAbYDHXw5Z8EYMxC_0024kptM0HvIQwX34RUCYEaE45F2jU4M hDeu1sVZsyQwc6gjFVdSJyM;

		internal object KOsyrvupWax5FYJ5u8DsvKY()
		{
			return smethod_0(vSGuJdYhWIbvxnmdxcu2UkA, hDeu1sVZsyQwc6gjFVdSJyM.Uih6Qw3PXLiUfOw1wbzKC_U.Value, (object[])null);
		}

		internal void KaJtXrEb_0024cphINPWr44WY98(object object_0)
		{
			smethod_1(vSGuJdYhWIbvxnmdxcu2UkA, hDeu1sVZsyQwc6gjFVdSJyM.Uih6Qw3PXLiUfOw1wbzKC_U.Value, object_0, (object[])null);
		}

		internal object KuBzcA_00249BOW5aGshYq3BGqM()
		{
			return smethod_0(vSGuJdYhWIbvxnmdxcu2UkA, hDeu1sVZsyQwc6gjFVdSJyM.Uih6Qw3PXLiUfOw1wbzKC_U.Value, (object[])null);
		}

		internal void KxBjUhaNmm3LSdFVBSe4D3w(object object_0)
		{
			smethod_1(vSGuJdYhWIbvxnmdxcu2UkA, hDeu1sVZsyQwc6gjFVdSJyM.Uih6Qw3PXLiUfOw1wbzKC_U.Value, object_0, (object[])null);
			hDeu1sVZsyQwc6gjFVdSJyM.Uih6Qw3PXLiUfOw1wbzKC_U.Value = hDeu1sVZsyQwc6gjFVdSJyM.Uih6Qw3PXLiUfOw1wbzKC_U.Value;
		}

		internal static object smethod_0(PropertyInfo propertyInfo_0, object object_0, object[] object_1)
		{
			return propertyInfo_0.GetValue(object_0, object_1);
		}

		internal static void smethod_1(PropertyInfo propertyInfo_0, object object_0, object object_1, object[] object_2)
		{
			propertyInfo_0.SetValue(object_0, object_1, object_2);
		}
	}

	[SerializeField]
	protected LayoutElement layoutElement;

	[SerializeField]
	protected Text variableNameText;

	[SerializeField]
	protected Image variableNameMask;

	[SerializeField]
	private MaskableGraphic T4TPXDO8yxKVyzw_0024yt3Pvcw;

	private RuntimeInspector _0024Rz_eGrINAgdzqgWavxV2l0;

	private int tez8QKQVeFGVS4AMHMsbzyw;

	private UISkin E58c_5PzPLk6LleLXcBTp_0024M;

	private Type VnxSC8SntWx46kXzvwoay9p3hhdH5eWo_002483MJNLaAyNn;

	private object s1ANaTLunWHUv6cYJ69Glko;

	private int kZwc00FBtBW3l5sBg8ra_0024w4 = -1;

	private bool zvHFh6h8G3ioM3P7Lj6mxvw = true;

	private Getter q5Ii18yuUo_0024mZqCxGNQdiLk;

	private Setter Dx0sTnEdhf1wjKdotXGdwcI;

	public RuntimeInspector Inspector
	{
		get
		{
			return _0024Rz_eGrINAgdzqgWavxV2l0;
		}
		set
		{
			if (smethod_0((UnityEngine.Object)_0024Rz_eGrINAgdzqgWavxV2l0, (UnityEngine.Object)value))
			{
				_0024Rz_eGrINAgdzqgWavxV2l0 = value;
				OnInspectorChanged();
			}
		}
	}

	public UISkin Skin
	{
		get
		{
			return E58c_5PzPLk6LleLXcBTp_0024M;
		}
		set
		{
			if (smethod_0((UnityEngine.Object)E58c_5PzPLk6LleLXcBTp_0024M, (UnityEngine.Object)value) || tez8QKQVeFGVS4AMHMsbzyw != E58c_5PzPLk6LleLXcBTp_0024M.Version)
			{
				E58c_5PzPLk6LleLXcBTp_0024M = value;
				tez8QKQVeFGVS4AMHMsbzyw = E58c_5PzPLk6LleLXcBTp_0024M.Version;
				OnSkinChanged();
				OnDepthChanged();
			}
		}
	}

	protected Type BoundVariableType => VnxSC8SntWx46kXzvwoay9p3hhdH5eWo_002483MJNLaAyNn;

	protected object Value
	{
		get
		{
			return s1ANaTLunWHUv6cYJ69Glko;
		}
		set
		{
			try
			{
				Dx0sTnEdhf1wjKdotXGdwcI(value);
				s1ANaTLunWHUv6cYJ69Glko = value;
			}
			catch
			{
			}
		}
	}

	public int Depth
	{
		protected get
		{
			return kZwc00FBtBW3l5sBg8ra_0024w4;
		}
		set
		{
			if (kZwc00FBtBW3l5sBg8ra_0024w4 != value)
			{
				kZwc00FBtBW3l5sBg8ra_0024w4 = value;
				OnDepthChanged();
			}
		}
	}

	public bool IsVisible => zvHFh6h8G3ioM3P7Lj6mxvw;

	public string Name
	{
		get
		{
			if (smethod_1((UnityEngine.Object)variableNameText))
			{
				return smethod_2(variableNameText);
			}
			return string.Empty;
		}
		set
		{
			if (smethod_1((UnityEngine.Object)variableNameText))
			{
				smethod_3(variableNameText, Inspector.UseTitleCaseNaming ? value.ToTitleCase() : value);
			}
		}
	}

	public string NameRaw
	{
		get
		{
			if (smethod_1((UnityEngine.Object)variableNameText))
			{
				return smethod_2(variableNameText);
			}
			return string.Empty;
		}
		set
		{
			if (smethod_1((UnityEngine.Object)variableNameText))
			{
				smethod_3(variableNameText, value);
			}
		}
	}

	public virtual bool ShouldRefresh => zvHFh6h8G3ioM3P7Lj6mxvw;

	protected virtual float HeightMultiplier => 1f;

	public virtual void Initialize()
	{
		if (smethod_1((UnityEngine.Object)T4TPXDO8yxKVyzw_0024yt3Pvcw))
		{
			smethod_4(T4TPXDO8yxKVyzw_0024yt3Pvcw).AddListener(delegate(bool bool_0)
			{
				zvHFh6h8G3ioM3P7Lj6mxvw = !bool_0;
			});
		}
	}

	public abstract bool SupportsType(Type type);

	public virtual bool CanBindTo(Type type, MemberInfo variable)
	{
		return true;
	}

	public void BindTo(InspectorField parent, MemberInfo variable, string variableName = null)
	{
		if (variable is FieldInfo)
		{
			FieldInfo jlkguRl3NGG5witjl_tIwd0 = (FieldInfo)variable;
			if (variableName == null)
			{
				variableName = smethod_5((MemberInfo)jlkguRl3NGG5witjl_tIwd0);
			}
			if (!smethod_6(parent.BoundVariableType))
			{
				BindTo(smethod_7(jlkguRl3NGG5witjl_tIwd0), variableName, () => J_0024KDWMcBIDqIwJ3uI8QWlGEYkNnTupwRe_cwtS_hSaxBEgxmu9CtGKYz3S4mJtOpjzryuoi9YKIE8RXeYXB0LS8.smethod_0(jlkguRl3NGG5witjl_tIwd0, parent.Value), delegate(object object_0)
				{
					J_0024KDWMcBIDqIwJ3uI8QWlGEYkNnTupwRe_cwtS_hSaxBEgxmu9CtGKYz3S4mJtOpjzryuoi9YKIE8RXeYXB0LS8.smethod_1(jlkguRl3NGG5witjl_tIwd0, parent.Value, object_0);
				}, variable);
				return;
			}
			BindTo(smethod_7(jlkguRl3NGG5witjl_tIwd0), variableName, () => J_0024KDWMcBIDqIwJ3uI8QWlGEYkNnTupwRe_cwtS_hSaxBEgxmu9CtGKYz3S4mJtOpjzryuoi9YKIE8RXeYXB0LS8.smethod_0(jlkguRl3NGG5witjl_tIwd0, parent.Value), delegate(object object_0)
			{
				J_0024KDWMcBIDqIwJ3uI8QWlGEYkNnTupwRe_cwtS_hSaxBEgxmu9CtGKYz3S4mJtOpjzryuoi9YKIE8RXeYXB0LS8.smethod_1(jlkguRl3NGG5witjl_tIwd0, parent.Value, object_0);
				parent.Value = parent.Value;
			}, variable);
			return;
		}
		if (!(variable is PropertyInfo))
		{
			throw smethod_9(global::_003CModule_003E.smethod_27<string>(1513603722u));
		}
		PropertyInfo vSGuJdYhWIbvxnmdxcu2UkA = (PropertyInfo)variable;
		if (variableName == null)
		{
			variableName = smethod_5((MemberInfo)vSGuJdYhWIbvxnmdxcu2UkA);
		}
		if (smethod_6(parent.BoundVariableType))
		{
			BindTo(smethod_8(vSGuJdYhWIbvxnmdxcu2UkA), variableName, () => KNwddQhNbriB7aVZU8HpfkigH_uDSlHNN6Zxs_Lid1bHcGP5VFohQYZdE_0024lbvn5lFBhwrU_0024VCZWsLizrTtJsTtQ.smethod_0(vSGuJdYhWIbvxnmdxcu2UkA, parent.Value, (object[])null), delegate(object object_0)
			{
				KNwddQhNbriB7aVZU8HpfkigH_uDSlHNN6Zxs_Lid1bHcGP5VFohQYZdE_0024lbvn5lFBhwrU_0024VCZWsLizrTtJsTtQ.smethod_1(vSGuJdYhWIbvxnmdxcu2UkA, parent.Value, object_0, (object[])null);
				parent.Value = parent.Value;
			}, variable);
		}
		else
		{
			BindTo(smethod_8(vSGuJdYhWIbvxnmdxcu2UkA), variableName, () => KNwddQhNbriB7aVZU8HpfkigH_uDSlHNN6Zxs_Lid1bHcGP5VFohQYZdE_0024lbvn5lFBhwrU_0024VCZWsLizrTtJsTtQ.smethod_0(vSGuJdYhWIbvxnmdxcu2UkA, parent.Value, (object[])null), delegate(object object_0)
			{
				KNwddQhNbriB7aVZU8HpfkigH_uDSlHNN6Zxs_Lid1bHcGP5VFohQYZdE_0024lbvn5lFBhwrU_0024VCZWsLizrTtJsTtQ.smethod_1(vSGuJdYhWIbvxnmdxcu2UkA, parent.Value, object_0, (object[])null);
			}, variable);
		}
	}

	public void BindTo(Type variableType, string variableName, Getter getter, Setter setter, MemberInfo variable = null)
	{
		VnxSC8SntWx46kXzvwoay9p3hhdH5eWo_002483MJNLaAyNn = variableType;
		Name = variableName;
		q5Ii18yuUo_0024mZqCxGNQdiLk = getter;
		Dx0sTnEdhf1wjKdotXGdwcI = setter;
		OnBound(variable);
	}

	public void Unbind()
	{
		VnxSC8SntWx46kXzvwoay9p3hhdH5eWo_002483MJNLaAyNn = null;
		q5Ii18yuUo_0024mZqCxGNQdiLk = null;
		Dx0sTnEdhf1wjKdotXGdwcI = null;
		OnUnbound();
		Inspector.OYu8MhBMQoTrQV4yEcQKTzM(this);
	}

	protected virtual void OnBound(MemberInfo variable)
	{
		Qi0VdYOtmxZ5g2S2_3_0024kPBY();
	}

	protected virtual void OnUnbound()
	{
		s1ANaTLunWHUv6cYJ69Glko = null;
	}

	protected virtual void OnInspectorChanged()
	{
		if (!smethod_1((UnityEngine.Object)variableNameText))
		{
			return;
		}
		if (_0024Rz_eGrINAgdzqgWavxV2l0.ShowTooltips)
		{
			if (!smethod_1((UnityEngine.Object)variableNameText.GetComponent<TooltipArea>()))
			{
				smethod_10((Component)variableNameText).AddComponent<TooltipArea>().Initialize(this);
				smethod_11((Graphic)variableNameText, bool_0: true);
			}
			return;
		}
		TooltipArea component = variableNameText.GetComponent<TooltipArea>();
		if (smethod_1((UnityEngine.Object)component))
		{
			smethod_12((UnityEngine.Object)component);
			smethod_11((Graphic)variableNameText, bool_0: false);
		}
	}

	protected virtual void OnSkinChanged()
	{
		if (smethod_1((UnityEngine.Object)layoutElement))
		{
			layoutElement.SetHeight((float)Skin.LineHeight * HeightMultiplier);
		}
		if (smethod_1((UnityEngine.Object)variableNameText))
		{
			variableNameText.SetSkinText(Skin);
		}
		if (smethod_1((UnityEngine.Object)variableNameMask))
		{
			smethod_13((Graphic)variableNameMask, Skin.BackgroundColor);
		}
	}

	protected virtual void OnDepthChanged()
	{
		if (smethod_0((UnityEngine.Object)variableNameText, (UnityEngine.Object)null))
		{
			smethod_14((Graphic)variableNameText).sizeDelta = new Vector2(-Skin.IndentAmount * Depth, 0f);
		}
	}

	public virtual void Refresh()
	{
		Qi0VdYOtmxZ5g2S2_3_0024kPBY();
	}

	private void Qi0VdYOtmxZ5g2S2_3_0024kPBY()
	{
		try
		{
			s1ANaTLunWHUv6cYJ69Glko = q5Ii18yuUo_0024mZqCxGNQdiLk();
		}
		catch
		{
			if (smethod_6(BoundVariableType))
			{
				s1ANaTLunWHUv6cYJ69Glko = smethod_15(BoundVariableType);
			}
			else
			{
				s1ANaTLunWHUv6cYJ69Glko = null;
			}
		}
	}

	internal static bool smethod_0(UnityEngine.Object object_0, UnityEngine.Object object_1)
	{
		return object_0 != object_1;
	}

	internal static bool smethod_1(UnityEngine.Object object_0)
	{
		return object_0;
	}

	internal static string smethod_2(Text text_0)
	{
		return text_0.text;
	}

	internal static void smethod_3(Text text_0, string string_0)
	{
		text_0.text = string_0;
	}

	internal static MaskableGraphic.CullStateChangedEvent smethod_4(MaskableGraphic maskableGraphic_0)
	{
		return maskableGraphic_0.onCullStateChanged;
	}

	internal static string smethod_5(MemberInfo memberInfo_0)
	{
		return memberInfo_0.Name;
	}

	internal static bool smethod_6(Type type_0)
	{
		return type_0.IsValueType;
	}

	internal static Type smethod_7(FieldInfo fieldInfo_0)
	{
		return fieldInfo_0.FieldType;
	}

	internal static Type smethod_8(PropertyInfo propertyInfo_0)
	{
		return propertyInfo_0.PropertyType;
	}

	internal static ArgumentException smethod_9(string string_0)
	{
		return new ArgumentException(string_0);
	}

	internal static GameObject smethod_10(Component component_0)
	{
		return component_0.gameObject;
	}

	internal static void smethod_11(Graphic graphic_0, bool bool_0)
	{
		graphic_0.raycastTarget = bool_0;
	}

	internal static void smethod_12(UnityEngine.Object object_0)
	{
		UnityEngine.Object.Destroy(object_0);
	}

	internal static void smethod_13(Graphic graphic_0, Color color_0)
	{
		graphic_0.color = color_0;
	}

	internal static RectTransform smethod_14(Graphic graphic_0)
	{
		return graphic_0.rectTransform;
	}

	internal static object smethod_15(Type type_0)
	{
		return Activator.CreateInstance(type_0);
	}
}
