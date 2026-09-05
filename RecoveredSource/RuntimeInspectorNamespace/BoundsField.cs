using System;
using System.Reflection;
using UnityEngine;

namespace RuntimeInspectorNamespace;

public class BoundsField : InspectorField
{
	[SerializeField]
	private Vector3Field A4mScsq2AIf8CYDODAWxB6Q;

	[SerializeField]
	private Vector3Field vector3Field_0;

	private MemberInfo DYdYKhVx77BkjRl1PzjV8AY;

	private MemberInfo XTgDSp3_0024_0024mxgOoIJwvowYSY;

	protected override float HeightMultiplier => 3f;

	public override void Initialize()
	{
		base.Initialize();
		DYdYKhVx77BkjRl1PzjV8AY = ((BoundsField)(object)smethod_16(typeof(Bounds).TypeHandle)).method_0(global::_003CModule_003E.smethod_28<string>(1985693213u));
		XTgDSp3_0024_0024mxgOoIJwvowYSY = ((BoundsField)(object)smethod_16(typeof(Bounds).TypeHandle)).method_0(global::_003CModule_003E.smethod_26<string>(3193228768u));
	}

	public override bool SupportsType(Type type)
	{
		return type == smethod_16(typeof(Bounds).TypeHandle);
	}

	protected override void OnBound(MemberInfo variable)
	{
		base.OnBound(variable);
		A4mScsq2AIf8CYDODAWxB6Q.BindTo(this, DYdYKhVx77BkjRl1PzjV8AY, global::_003CModule_003E.smethod_26<string>(2028879201u));
		vector3Field_0.BindTo(this, XTgDSp3_0024_0024mxgOoIJwvowYSY, global::_003CModule_003E.smethod_29<string>(586685666u));
	}

	protected override void OnInspectorChanged()
	{
		base.OnInspectorChanged();
		A4mScsq2AIf8CYDODAWxB6Q.Inspector = base.Inspector;
		vector3Field_0.Inspector = base.Inspector;
	}

	protected override void OnSkinChanged()
	{
		base.OnSkinChanged();
		A4mScsq2AIf8CYDODAWxB6Q.Skin = base.Skin;
		vector3Field_0.Skin = base.Skin;
	}

	protected override void OnDepthChanged()
	{
		base.OnDepthChanged();
		A4mScsq2AIf8CYDODAWxB6Q.Depth = base.Depth + 1;
		vector3Field_0.Depth = base.Depth + 1;
	}

	public override void Refresh()
	{
		base.Refresh();
		A4mScsq2AIf8CYDODAWxB6Q.Refresh();
		vector3Field_0.Refresh();
	}

	internal static Type smethod_16(RuntimeTypeHandle runtimeTypeHandle_0)
	{
		return Type.GetTypeFromHandle(runtimeTypeHandle_0);
	}

	PropertyInfo method_0(string string_0)
	{
		return ((Type)(object)this).GetProperty(string_0);
	}
}
