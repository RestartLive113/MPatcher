using System;
using System.Reflection;
using UnityEngine;

namespace RuntimeInspectorNamespace;

public class TransformField : ExpandableInspectorField
{
	private PropertyInfo cDF_0024cZ55ofFufLCP9dsL9yg;

	private PropertyInfo QUy9_0024f8T1quoy6_00245kYbdLns;

	private PropertyInfo propertyInfo_0;

	protected override int Length => 3;

	public override void Initialize()
	{
		base.Initialize();
		cDF_0024cZ55ofFufLCP9dsL9yg = ((TransformField)(object)smethod_33(typeof(Transform).TypeHandle)).method_0(global::_003CModule_003E.smethod_28<string>(3837195131u));
		QUy9_0024f8T1quoy6_00245kYbdLns = ((TransformField)(object)smethod_33(typeof(Transform).TypeHandle)).method_0(global::_003CModule_003E.smethod_28<string>(3381895626u));
		propertyInfo_0 = ((TransformField)(object)smethod_33(typeof(Transform).TypeHandle)).method_0(global::_003CModule_003E.smethod_26<string>(787472983u));
	}

	public override bool SupportsType(Type type)
	{
		return type == smethod_33(typeof(Transform).TypeHandle);
	}

	protected override void GenerateElements()
	{
		CreateDrawerForVariable(cDF_0024cZ55ofFufLCP9dsL9yg, global::_003CModule_003E.smethod_29<string>(1950067893u));
		CreateDrawerForVariable(QUy9_0024f8T1quoy6_00245kYbdLns, global::_003CModule_003E.smethod_25<string>(3221847351u));
		CreateDrawerForVariable(propertyInfo_0, global::_003CModule_003E.smethod_27<string>(494531214u));
	}

	internal static Type smethod_33(RuntimeTypeHandle runtimeTypeHandle_0)
	{
		return Type.GetTypeFromHandle(runtimeTypeHandle_0);
	}

	PropertyInfo method_0(string string_0)
	{
		return ((Type)(object)this).GetProperty(string_0);
	}
}
