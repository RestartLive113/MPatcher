using System;
using System.Collections.Generic;
using UnityEngine;

namespace RuntimeInspectorNamespace;

public class HierarchyDataRootSearch : HierarchyDataRoot
{
	private readonly List<Transform> zZ_0024Ou2Fq2syVkGt5oTIQOCI = new List<Transform>();

	private readonly HierarchyDataRoot A7WKX4GwH0GFrbeguX0UT_0024Q;

	private string string_0;

	public override string Name => A7WKX4GwH0GFrbeguX0UT_0024Q.Name;

	public override int ChildCount => zZ_0024Ou2Fq2syVkGt5oTIQOCI.Count;

	public HierarchyDataRootSearch(RuntimeHierarchy runtimeHierarchy_1, HierarchyDataRoot hierarchyDataRoot_0)
		: base(runtimeHierarchy_1)
	{
		A7WKX4GwH0GFrbeguX0UT_0024Q = hierarchyDataRoot_0;
	}

	public override void RefreshContent()
	{
		if (!base.Hierarchy.IsInSearchMode)
		{
			return;
		}
		zZ_0024Ou2Fq2syVkGt5oTIQOCI.Clear();
		string_0 = base.Hierarchy.SearchTerm;
		int childCount = A7WKX4GwH0GFrbeguX0UT_0024Q.ChildCount;
		for (int i = 0; i < childCount; i++)
		{
			Transform child = A7WKX4GwH0GFrbeguX0UT_0024Q.GetChild(i);
			if (smethod_4((UnityEngine.Object)child) && !RuntimeInspectorUtils.IgnoredTransformsInHierarchy.Contains(smethod_5((Component)child)))
			{
				if (smethod_7(smethod_6((UnityEngine.Object)child), string_0, StringComparison.OrdinalIgnoreCase) >= 0)
				{
					zZ_0024Ou2Fq2syVkGt5oTIQOCI.Add(child);
				}
				q06qOPIS3cJ4pYN_ZickmYYX7H2_mC1Y8uDFRFdV5oK_(smethod_5((Component)child));
			}
		}
	}

	public override bool Refresh()
	{
		m_depth = 0;
		bool result = base.Refresh();
		if (m_height == 1)
		{
			m_height = 0;
			m_depth = -1;
		}
		return result;
	}

	private void q06qOPIS3cJ4pYN_ZickmYYX7H2_mC1Y8uDFRFdV5oK_(Transform transform_0)
	{
		for (int i = 0; i < smethod_9(transform_0); i++)
		{
			Transform transform = smethod_8(transform_0, i);
			if (!RuntimeInspectorUtils.IgnoredTransformsInHierarchy.Contains(transform))
			{
				if (smethod_7(smethod_6((UnityEngine.Object)transform), string_0, StringComparison.OrdinalIgnoreCase) >= 0)
				{
					zZ_0024Ou2Fq2syVkGt5oTIQOCI.Add(transform);
				}
				q06qOPIS3cJ4pYN_ZickmYYX7H2_mC1Y8uDFRFdV5oK_(transform);
			}
		}
	}

	public override Transform GetChild(int index)
	{
		return zZ_0024Ou2Fq2syVkGt5oTIQOCI[index];
	}

	internal static bool smethod_4(UnityEngine.Object object_0)
	{
		return object_0;
	}

	internal static Transform smethod_5(Component component_0)
	{
		return component_0.transform;
	}

	internal static string smethod_6(UnityEngine.Object object_0)
	{
		return object_0.name;
	}

	internal static int smethod_7(string string_1, string string_2, StringComparison stringComparison_0)
	{
		return string_1.IndexOf(string_2, stringComparison_0);
	}

	internal static Transform smethod_8(Transform transform_0, int int_0)
	{
		return transform_0.GetChild(int_0);
	}

	internal static int smethod_9(Transform transform_0)
	{
		return transform_0.childCount;
	}
}
