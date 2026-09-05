using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace RuntimeInspectorNamespace;

[Serializable]
public class VariableSet
{
	private const string INCLUDE_ALL_VARIABLES = "*";

	[SerializeField]
	private string m_type;

	public Type type;

	[SerializeField]
	private string[] m_variables;

	public HashSet<string> variables;

	public bool Init()
	{
		type = RuntimeInspectorUtils.GetType(m_type);
		if (type == null)
		{
			return false;
		}
		variables = new HashSet<string>();
		for (int i = 0; i < m_variables.Length; i++)
		{
			if (smethod_0(m_variables[i], global::_003CModule_003E.smethod_26<string>(4053392763u)))
			{
				variables.Add(m_variables[i]);
				continue;
			}
			LCgV_0024wsgQ7E8vMS0i0ihq1M4G_vdLYhBAPn0SLy9Ow1W();
			break;
		}
		return true;
	}

	private void LCgV_0024wsgQ7E8vMS0i0ihq1M4G_vdLYhBAPn0SLy9Ow1W()
	{
		MemberInfo[] allVariables = type.GetAllVariables();
		if (allVariables != null)
		{
			for (int i = 0; i < allVariables.Length; i++)
			{
				variables.Add(smethod_1(allVariables[i]));
			}
		}
	}

	internal static bool smethod_0(string string_0, string string_1)
	{
		return string_0 != string_1;
	}

	internal static string smethod_1(MemberInfo memberInfo_0)
	{
		return memberInfo_0.Name;
	}
}
