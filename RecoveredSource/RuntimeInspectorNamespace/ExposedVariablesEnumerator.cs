using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace RuntimeInspectorNamespace;

public class ExposedVariablesEnumerator : IEnumerator<MemberInfo>, IEnumerable<MemberInfo>, IDisposable, IEnumerator, IEnumerable
{
	private int _ts9wpdjIPBkmE0Qu489V0I;

	private readonly MemberInfo[] oyafsQDtGNs33Dh58z7iaYE;

	private readonly List<VariableSet> neWeZNYxgXJ_u090RDH51hU;

	private readonly List<VariableSet> W_xn1OAeoB4vMsOCHaRArvozH2kxt1DhLgr7xvWhfqF8;

	private readonly RuntimeInspector.VariableVisibility pOkql923DsMvYHnvpMXVS7Y;

	private readonly RuntimeInspector.VariableVisibility qxW3B2C4_RKSH3xyvdIS0tWS74P2mFPM7iPaeOGMuAQT;

	public MemberInfo Current => oyafsQDtGNs33Dh58z7iaYE[_ts9wpdjIPBkmE0Qu489V0I];

	object IEnumerator.Current => oyafsQDtGNs33Dh58z7iaYE[_ts9wpdjIPBkmE0Qu489V0I];

	public ExposedVariablesEnumerator(MemberInfo[] memberInfo_0, List<VariableSet> list_0, List<VariableSet> list_1, RuntimeInspector.VariableVisibility variableVisibility_0, RuntimeInspector.VariableVisibility variableVisibility_1)
	{
		_ts9wpdjIPBkmE0Qu489V0I = -1;
		oyafsQDtGNs33Dh58z7iaYE = memberInfo_0;
		neWeZNYxgXJ_u090RDH51hU = list_0;
		W_xn1OAeoB4vMsOCHaRArvozH2kxt1DhLgr7xvWhfqF8 = list_1;
		pOkql923DsMvYHnvpMXVS7Y = variableVisibility_0;
		qxW3B2C4_RKSH3xyvdIS0tWS74P2mFPM7iPaeOGMuAQT = variableVisibility_1;
	}

	public void Dispose()
	{
	}

	public IEnumerator<MemberInfo> GetEnumerator()
	{
		return this;
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return this;
	}

	public bool MoveNext()
	{
		if (oyafsQDtGNs33Dh58z7iaYE == null)
		{
			return false;
		}
		do
		{
			if (++_ts9wpdjIPBkmE0Qu489V0I >= oyafsQDtGNs33Dh58z7iaYE.Length)
			{
				return false;
			}
		}
		while (!b2IxvnQb_CHfifMm95I0WwgM9rHwHLh3sBMV8wM8R8Hk(oyafsQDtGNs33Dh58z7iaYE[_ts9wpdjIPBkmE0Qu489V0I]));
		return true;
	}

	public void Reset()
	{
		_ts9wpdjIPBkmE0Qu489V0I = -1;
	}

	private bool b2IxvnQb_CHfifMm95I0WwgM9rHwHLh3sBMV8wM8R8Hk(MemberInfo memberInfo_0)
	{
		string item = smethod_0(memberInfo_0);
		if (W_xn1OAeoB4vMsOCHaRArvozH2kxt1DhLgr7xvWhfqF8 != null)
		{
			for (int i = 0; i < W_xn1OAeoB4vMsOCHaRArvozH2kxt1DhLgr7xvWhfqF8.Count; i++)
			{
				if (W_xn1OAeoB4vMsOCHaRArvozH2kxt1DhLgr7xvWhfqF8[i].variables.Contains(item))
				{
					return true;
				}
			}
		}
		if (neWeZNYxgXJ_u090RDH51hU != null)
		{
			for (int j = 0; j < neWeZNYxgXJ_u090RDH51hU.Count; j++)
			{
				if (neWeZNYxgXJ_u090RDH51hU[j].variables.Contains(item))
				{
					return false;
				}
			}
		}
		if (memberInfo_0 is FieldInfo)
		{
			switch (pOkql923DsMvYHnvpMXVS7Y)
			{
			case RuntimeInspector.VariableVisibility.None:
				return false;
			case RuntimeInspector.VariableVisibility.SerializableOnly:
			{
				FieldInfo fieldInfo = (FieldInfo)memberInfo_0;
				if (!smethod_1(fieldInfo))
				{
					return fieldInfo.HasAttribute<SerializeField>();
				}
				return true;
			}
			case RuntimeInspector.VariableVisibility.All:
				return true;
			}
		}
		else
		{
			switch (qxW3B2C4_RKSH3xyvdIS0tWS74P2mFPM7iPaeOGMuAQT)
			{
			case RuntimeInspector.VariableVisibility.None:
				return false;
			case RuntimeInspector.VariableVisibility.SerializableOnly:
			{
				PropertyInfo propertyInfo = (PropertyInfo)memberInfo_0;
				if (smethod_3((MethodBase)smethod_2(propertyInfo, bool_0: true)))
				{
					return true;
				}
				return propertyInfo.HasAttribute<SerializeField>();
			}
			case RuntimeInspector.VariableVisibility.All:
				return true;
			}
		}
		return true;
	}

	internal static string smethod_0(MemberInfo memberInfo_0)
	{
		return memberInfo_0.Name;
	}

	internal static bool smethod_1(FieldInfo fieldInfo_0)
	{
		return fieldInfo_0.IsPublic;
	}

	internal static MethodInfo smethod_2(PropertyInfo propertyInfo_0, bool bool_0)
	{
		return propertyInfo_0.GetGetMethod(bool_0);
	}

	internal static bool smethod_3(MethodBase methodBase_0)
	{
		return methodBase_0.IsPublic;
	}
}
