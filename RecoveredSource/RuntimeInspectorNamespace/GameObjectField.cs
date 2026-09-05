using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace RuntimeInspectorNamespace;

public class GameObjectField : ExpandableInspectorField
{
	private string TbL7Xr6unjazio0E_0024dFFdR4;

	private StringField i_002487Qh8YbJ6sy1rl8rAcjIA;

	private StringField aDB4xLXIcsn23NVFn2iIAZk;

	private Getter w_F1x_vzWl3OWAzKdTa_FNA;

	private Getter ovUSLDbR1SoO_0024r_Ju4JA6gE;

	private Getter OWzgzIVNAdt7behyBNHCPSw;

	private Setter J7qCG1ma2HJB2tV4uBzIhSA;

	private Setter BlMxZvtl0pYihwt1_00248xnLSA;

	private Setter nUW73WZ_0024kRxxLo6Ma2YM1CY;

	private PropertyInfo SO16HCI0TQ5CZvApmq47YzM;

	private readonly List<Component> Nq9DRc2uQ2Gs4K6APoVXEsE = new List<Component>(8);

	private readonly List<bool> rSkBFndXpQ0LrmPoQH9fAK5nbSGCtCs901VSKRJWmb0y = new List<bool>();

	protected override int Length => Nq9DRc2uQ2Gs4K6APoVXEsE.Count + 4;

	public override void Initialize()
	{
		base.Initialize();
		w_F1x_vzWl3OWAzKdTa_FNA = () => smethod_37((GameObject)base.Value);
		J7qCG1ma2HJB2tV4uBzIhSA = delegate(object object_0)
		{
			smethod_38((GameObject)base.Value, (bool)object_0);
		};
		ovUSLDbR1SoO_0024r_Ju4JA6gE = () => smethod_39((UnityEngine.Object)(GameObject)base.Value);
		BlMxZvtl0pYihwt1_00248xnLSA = delegate(object object_0)
		{
			smethod_40((UnityEngine.Object)(GameObject)base.Value, (string)object_0);
			base.NameRaw = base.Value.GetNameWithType();
			RuntimeHierarchy connectedHierarchy = base.Inspector.ConnectedHierarchy;
			if (smethod_36((UnityEngine.Object)connectedHierarchy))
			{
				connectedHierarchy.RefreshNameOf(smethod_41((GameObject)base.Value));
			}
		};
		OWzgzIVNAdt7behyBNHCPSw = delegate
		{
			GameObject gameObject_ = (GameObject)base.Value;
			if (!smethod_42(gameObject_, TbL7Xr6unjazio0E_0024dFFdR4))
			{
				TbL7Xr6unjazio0E_0024dFFdR4 = smethod_34(gameObject_);
			}
			return TbL7Xr6unjazio0E_0024dFFdR4;
		};
		nUW73WZ_0024kRxxLo6Ma2YM1CY = delegate(object object_0)
		{
			smethod_43((GameObject)base.Value, (string)object_0);
		};
		SO16HCI0TQ5CZvApmq47YzM = ((GameObjectField)(object)smethod_33(typeof(GameObject).TypeHandle)).method_0(global::_003CModule_003E.smethod_29<string>(446028977u));
	}

	public override bool SupportsType(Type type)
	{
		return type == smethod_33(typeof(GameObject).TypeHandle);
	}

	protected override void OnBound(MemberInfo variable)
	{
		base.OnBound(variable);
		TbL7Xr6unjazio0E_0024dFFdR4 = smethod_34((GameObject)base.Value);
	}

	protected override void OnUnbound()
	{
		base.OnUnbound();
		Nq9DRc2uQ2Gs4K6APoVXEsE.Clear();
		rSkBFndXpQ0LrmPoQH9fAK5nbSGCtCs901VSKRJWmb0y.Clear();
	}

	protected override void ClearElements()
	{
		if (smethod_35((UnityEngine.Object)i_002487Qh8YbJ6sy1rl8rAcjIA, (UnityEngine.Object)null))
		{
			i_002487Qh8YbJ6sy1rl8rAcjIA.SetterMode = StringField.Mode.OnValueChange;
			i_002487Qh8YbJ6sy1rl8rAcjIA = null;
		}
		if (smethod_35((UnityEngine.Object)aDB4xLXIcsn23NVFn2iIAZk, (UnityEngine.Object)null))
		{
			aDB4xLXIcsn23NVFn2iIAZk.SetterMode = StringField.Mode.OnValueChange;
			aDB4xLXIcsn23NVFn2iIAZk = null;
		}
		rSkBFndXpQ0LrmPoQH9fAK5nbSGCtCs901VSKRJWmb0y.Clear();
		for (int i = 0; i < elements.Count; i++)
		{
			rSkBFndXpQ0LrmPoQH9fAK5nbSGCtCs901VSKRJWmb0y.Add(elements[i] is ExpandableInspectorField && ((ExpandableInspectorField)elements[i]).IsExpanded);
		}
		base.ClearElements();
	}

	protected override void GenerateElements()
	{
		if (Nq9DRc2uQ2Gs4K6APoVXEsE.Count == 0)
		{
			return;
		}
		CreateDrawer(smethod_33(typeof(bool).TypeHandle), global::_003CModule_003E.smethod_28<string>(3533711530u), w_F1x_vzWl3OWAzKdTa_FNA, J7qCG1ma2HJB2tV4uBzIhSA);
		i_002487Qh8YbJ6sy1rl8rAcjIA = CreateDrawer(smethod_33(typeof(string).TypeHandle), global::_003CModule_003E.smethod_27<string>(694630025u), ovUSLDbR1SoO_0024r_Ju4JA6gE, BlMxZvtl0pYihwt1_00248xnLSA) as StringField;
		aDB4xLXIcsn23NVFn2iIAZk = CreateDrawer(smethod_33(typeof(string).TypeHandle), global::_003CModule_003E.smethod_26<string>(3318219370u), OWzgzIVNAdt7behyBNHCPSw, nUW73WZ_0024kRxxLo6Ma2YM1CY) as StringField;
		CreateDrawerForVariable(SO16HCI0TQ5CZvApmq47YzM, global::_003CModule_003E.smethod_27<string>(3444711277u));
		int i = 0;
		int num = elements.Count;
		for (; i < Nq9DRc2uQ2Gs4K6APoVXEsE.Count; i++)
		{
			InspectorField inspectorField = CreateDrawerForComponent(Nq9DRc2uQ2Gs4K6APoVXEsE[i]);
			if (smethod_35((UnityEngine.Object)inspectorField, (UnityEngine.Object)null))
			{
				if (num < rSkBFndXpQ0LrmPoQH9fAK5nbSGCtCs901VSKRJWmb0y.Count && rSkBFndXpQ0LrmPoQH9fAK5nbSGCtCs901VSKRJWmb0y[num] && inspectorField is ExpandableInspectorField)
				{
					((ExpandableInspectorField)inspectorField).IsExpanded = true;
				}
				num++;
			}
		}
		if (smethod_35((UnityEngine.Object)i_002487Qh8YbJ6sy1rl8rAcjIA, (UnityEngine.Object)null))
		{
			i_002487Qh8YbJ6sy1rl8rAcjIA.SetterMode = StringField.Mode.OnSubmit;
		}
		if (smethod_35((UnityEngine.Object)aDB4xLXIcsn23NVFn2iIAZk, (UnityEngine.Object)null))
		{
			aDB4xLXIcsn23NVFn2iIAZk.SetterMode = StringField.Mode.OnSubmit;
		}
		rSkBFndXpQ0LrmPoQH9fAK5nbSGCtCs901VSKRJWmb0y.Clear();
	}

	public override void Refresh()
	{
		base.Refresh();
		Nq9DRc2uQ2Gs4K6APoVXEsE.Clear();
		GameObject gameObject = base.Value as GameObject;
		if (!smethod_36((UnityEngine.Object)gameObject))
		{
			return;
		}
		gameObject.GetComponents(Nq9DRc2uQ2Gs4K6APoVXEsE);
		for (int num = Nq9DRc2uQ2Gs4K6APoVXEsE.Count - 1; num >= 0; num--)
		{
			if (!smethod_36((UnityEngine.Object)Nq9DRc2uQ2Gs4K6APoVXEsE[num]))
			{
				Nq9DRc2uQ2Gs4K6APoVXEsE.RemoveAt(num);
			}
		}
		if (base.Inspector.ComponentFilter != null)
		{
			base.Inspector.ComponentFilter(gameObject, Nq9DRc2uQ2Gs4K6APoVXEsE);
		}
	}

	[CompilerGenerated]
	private void ZXctp1xyvXibvej2MYvw5DXj9dKKWqoxAoKZyeit_YmH(object object_0)
	{
		smethod_38((GameObject)base.Value, (bool)object_0);
	}

	[CompilerGenerated]
	private object ZvaHyrntU26TeuwfX6RlCcwoKcd3mnUMtqRrW67BvXeH()
	{
		return smethod_39((UnityEngine.Object)(GameObject)base.Value);
	}

	[CompilerGenerated]
	private void Z0poXLxttovG6XTjnDLG4tV7ulBcoS_X3YzDxqwhrH1I(object object_0)
	{
		smethod_40((UnityEngine.Object)(GameObject)base.Value, (string)object_0);
		base.NameRaw = base.Value.GetNameWithType();
		RuntimeHierarchy connectedHierarchy = base.Inspector.ConnectedHierarchy;
		if (smethod_36((UnityEngine.Object)connectedHierarchy))
		{
			connectedHierarchy.RefreshNameOf(smethod_41((GameObject)base.Value));
		}
	}

	internal static Type smethod_33(RuntimeTypeHandle runtimeTypeHandle_0)
	{
		return Type.GetTypeFromHandle(runtimeTypeHandle_0);
	}

	PropertyInfo method_0(string string_0)
	{
		return ((Type)(object)this).GetProperty(string_0);
	}

	internal static string smethod_34(GameObject gameObject_0)
	{
		return gameObject_0.tag;
	}

	internal static bool smethod_35(UnityEngine.Object object_0, UnityEngine.Object object_1)
	{
		return object_0 != object_1;
	}

	internal static bool smethod_36(UnityEngine.Object object_0)
	{
		return object_0;
	}

	internal static bool smethod_37(GameObject gameObject_0)
	{
		return gameObject_0.activeSelf;
	}

	internal static void smethod_38(GameObject gameObject_0, bool bool_1)
	{
		gameObject_0.SetActive(bool_1);
	}

	internal static string smethod_39(UnityEngine.Object object_0)
	{
		return object_0.name;
	}

	internal static void smethod_40(UnityEngine.Object object_0, string string_0)
	{
		object_0.name = string_0;
	}

	internal static Transform smethod_41(GameObject gameObject_0)
	{
		return gameObject_0.transform;
	}

	internal static bool smethod_42(GameObject gameObject_0, string string_0)
	{
		return gameObject_0.CompareTag(string_0);
	}

	internal static void smethod_43(GameObject gameObject_0, string string_0)
	{
		gameObject_0.tag = string_0;
	}
}
