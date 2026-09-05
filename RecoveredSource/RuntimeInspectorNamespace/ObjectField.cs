using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace RuntimeInspectorNamespace;

public class ObjectField : ExpandableInspectorField
{
	[SerializeField]
	private Button GQsvbbDuRhn_966_Wury2UvyfgiyBtYGeiBt8RqQvekP;

	private bool kaJEnYQeSAMljocQMCHHWU_0024YgIGBGBqHGgRKLMX5becD;

	protected override int Length
	{
		get
		{
			if (base.Value.IsNull())
			{
				if (smethod_34(smethod_33((Component)GQsvbbDuRhn_966_Wury2UvyfgiyBtYGeiBt8RqQvekP)))
				{
					return 0;
				}
				return -1;
			}
			if (!smethod_34(smethod_33((Component)GQsvbbDuRhn_966_Wury2UvyfgiyBtYGeiBt8RqQvekP)))
			{
				if (kaJEnYQeSAMljocQMCHHWU_0024YgIGBGBqHGgRKLMX5becD)
				{
					return elements.Count;
				}
				kaJEnYQeSAMljocQMCHHWU_0024YgIGBGBqHGgRKLMX5becD = true;
				return -1;
			}
			return -1;
		}
	}

	public override void Initialize()
	{
		base.Initialize();
		smethod_36((UnityEvent)smethod_35(GQsvbbDuRhn_966_Wury2UvyfgiyBtYGeiBt8RqQvekP), (UnityAction)delegate
		{
			if (C0HIQUJhadESt5Q7t3cq2GYXPuwaID4Kaoi0HFcl1i8_0024())
			{
				base.Value = base.BoundVariableType.Instantiate();
				RegenerateElements();
				base.IsExpanded = true;
			}
		});
	}

	public override bool SupportsType(Type type)
	{
		return true;
	}

	protected override void OnBound(MemberInfo variable)
	{
		kaJEnYQeSAMljocQMCHHWU_0024YgIGBGBqHGgRKLMX5becD = false;
		base.OnBound(variable);
	}

	protected override void GenerateElements()
	{
		if (base.Value.IsNull())
		{
			smethod_37(smethod_33((Component)GQsvbbDuRhn_966_Wury2UvyfgiyBtYGeiBt8RqQvekP), C0HIQUJhadESt5Q7t3cq2GYXPuwaID4Kaoi0HFcl1i8_0024());
			return;
		}
		smethod_37(smethod_33((Component)GQsvbbDuRhn_966_Wury2UvyfgiyBtYGeiBt8RqQvekP), bool_1: false);
		IEnumerator<MemberInfo> enumerator = base.Inspector.method_0(smethod_38(base.Value)).GetEnumerator();
		try
		{
			while (smethod_39((IEnumerator)enumerator))
			{
				MemberInfo current = enumerator.Current;
				CreateDrawerForVariable(current);
			}
		}
		finally
		{
			if (enumerator != null)
			{
				smethod_40((IDisposable)enumerator);
			}
		}
	}

	protected override void OnSkinChanged()
	{
		base.OnSkinChanged();
		GQsvbbDuRhn_966_Wury2UvyfgiyBtYGeiBt8RqQvekP.SetSkinButton(base.Skin);
	}

	private bool C0HIQUJhadESt5Q7t3cq2GYXPuwaID4Kaoi0HFcl1i8_0024()
	{
		if (!smethod_41(base.BoundVariableType) && !smethod_42(base.BoundVariableType))
		{
			if (smethod_44(smethod_43(typeof(ScriptableObject).TypeHandle), base.BoundVariableType))
			{
				return true;
			}
			if (smethod_44(smethod_43(typeof(UnityEngine.Object).TypeHandle), base.BoundVariableType))
			{
				return false;
			}
			if (smethod_45(base.BoundVariableType))
			{
				return false;
			}
			if (smethod_46(base.BoundVariableType) && smethod_47(base.BoundVariableType) == smethod_43(typeof(List<>).TypeHandle))
			{
				return false;
			}
			return true;
		}
		return false;
	}

	private void O_0024xoLpnZe3b1J3LXVNxpDF_0esnjjePVn_0024uM9SvjWoTw()
	{
		if (C0HIQUJhadESt5Q7t3cq2GYXPuwaID4Kaoi0HFcl1i8_0024())
		{
			base.Value = base.BoundVariableType.Instantiate();
			RegenerateElements();
			base.IsExpanded = true;
		}
	}

	internal static GameObject smethod_33(Component component_0)
	{
		return component_0.gameObject;
	}

	internal static bool smethod_34(GameObject gameObject_0)
	{
		return gameObject_0.activeSelf;
	}

	internal static Button.ButtonClickedEvent smethod_35(Button button_0)
	{
		return button_0.onClick;
	}

	internal static void smethod_36(UnityEvent unityEvent_0, UnityAction unityAction_0)
	{
		unityEvent_0.AddListener(unityAction_0);
	}

	internal static void smethod_37(GameObject gameObject_0, bool bool_1)
	{
		gameObject_0.SetActive(bool_1);
	}

	internal static Type smethod_38(object object_0)
	{
		return object_0.GetType();
	}

	internal static bool smethod_39(IEnumerator ienumerator_0)
	{
		return ienumerator_0.MoveNext();
	}

	internal static void smethod_40(IDisposable idisposable_0)
	{
		idisposable_0.Dispose();
	}

	internal static bool smethod_41(Type type_0)
	{
		return type_0.IsAbstract;
	}

	internal static bool smethod_42(Type type_0)
	{
		return type_0.IsInterface;
	}

	internal static Type smethod_43(RuntimeTypeHandle runtimeTypeHandle_0)
	{
		return Type.GetTypeFromHandle(runtimeTypeHandle_0);
	}

	internal static bool smethod_44(Type type_0, Type type_1)
	{
		return type_0.IsAssignableFrom(type_1);
	}

	internal static bool smethod_45(Type type_0)
	{
		return type_0.IsArray;
	}

	internal static bool smethod_46(Type type_0)
	{
		return type_0.IsGenericType;
	}

	internal static Type smethod_47(Type type_0)
	{
		return type_0.GetGenericTypeDefinition();
	}
}
