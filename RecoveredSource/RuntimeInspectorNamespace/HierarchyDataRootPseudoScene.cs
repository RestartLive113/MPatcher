using System.Collections.Generic;
using UnityEngine;

namespace RuntimeInspectorNamespace;

public class HierarchyDataRootPseudoScene : HierarchyDataRoot
{
	private readonly string r7lh6mDSmj85joUg2_0024t28Sg;

	private readonly List<Transform> list_0 = new List<Transform>();

	public override string Name => r7lh6mDSmj85joUg2_0024t28Sg;

	public override int ChildCount => list_0.Count;

	public HierarchyDataRootPseudoScene(RuntimeHierarchy runtimeHierarchy_1, string string_0)
		: base(runtimeHierarchy_1)
	{
		r7lh6mDSmj85joUg2_0024t28Sg = string_0;
	}

	public void AddChild(Transform child)
	{
		if (!list_0.Contains(child))
		{
			list_0.Add(child);
		}
	}

	public void InsertChild(int index, Transform child)
	{
		index = Mathf.Clamp(index, 0, list_0.Count);
		list_0.Insert(index, child);
		int num = list_0.Count - 1;
		while (true)
		{
			if (num >= 0)
			{
				if (num != index && smethod_4((Object)list_0[num], (Object)child))
				{
					break;
				}
				num--;
				continue;
			}
			return;
		}
		list_0.RemoveAt(num);
	}

	public void RemoveChild(Transform child)
	{
		list_0.Remove(child);
	}

	public override void RefreshContent()
	{
		for (int num = list_0.Count - 1; num >= 0; num--)
		{
			if (!smethod_5((Object)list_0[num]))
			{
				list_0.RemoveAt(num);
			}
		}
	}

	public override Transform GetChild(int index)
	{
		return list_0[index];
	}

	internal static bool smethod_4(Object object_0, Object object_1)
	{
		return object_0 == object_1;
	}

	internal static bool smethod_5(Object object_0)
	{
		return object_0;
	}
}
