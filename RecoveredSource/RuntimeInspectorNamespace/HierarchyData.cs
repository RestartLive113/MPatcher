using System.Collections.Generic;
using UnityEngine;

namespace RuntimeInspectorNamespace;

public abstract class HierarchyData
{
	private static readonly List<HierarchyDataTransform> zjGFrrVRoJy4oxpaZAYYJFwImZ2abdszY6Ztr9wR8vOc = new List<HierarchyDataTransform>(32);

	private static readonly List<List<HierarchyDataTransform>> _ZNyiurTrGiY6wLPamgyGHCL2QPTfplV9CLqyuAn6Mko = new List<List<HierarchyDataTransform>>(32);

	protected List<HierarchyDataTransform> children;

	protected HierarchyData parent;

	protected int m_index;

	protected int m_height = 1;

	protected int m_depth;

	public abstract string Name { get; }

	public abstract bool IsActive { get; }

	public abstract int ChildCount { get; }

	public abstract Transform BoundTransform { get; }

	public HierarchyDataRoot Root
	{
		get
		{
			HierarchyData hierarchyData = this;
			while (hierarchyData.parent != null)
			{
				hierarchyData = hierarchyData.parent;
			}
			return (HierarchyDataRoot)hierarchyData;
		}
	}

	public int Index => m_index;

	public int AbsoluteIndex
	{
		get
		{
			int num = m_index;
			for (HierarchyData hierarchyData = parent; hierarchyData != null; hierarchyData = hierarchyData.parent)
			{
				num += hierarchyData.m_index + 1;
			}
			return num;
		}
	}

	public int Height => m_height;

	public int Depth => m_depth;

	public bool CanExpand => ChildCount > 0;

	public bool IsExpanded
	{
		get
		{
			return children != null;
		}
		set
		{
			if (IsExpanded == value)
			{
				return;
			}
			if (value)
			{
				if (ChildCount == 0)
				{
					return;
				}
				PopChildrenList();
			}
			else
			{
				PoolChildrenList();
			}
			int height = m_height;
			Refresh();
			int num = m_height - height;
			if (num == 0)
			{
				return;
			}
			if (parent != null)
			{
				HierarchyData hierarchyData = this;
				for (HierarchyData hierarchyData2 = parent; hierarchyData2 != null; hierarchyData2 = hierarchyData2.parent)
				{
					List<HierarchyDataTransform> list = hierarchyData2.children;
					int i = list.IndexOf((HierarchyDataTransform)hierarchyData) + 1;
					for (int count = list.Count; i < count; i++)
					{
						list[i].m_index += num;
					}
					hierarchyData2.m_height += num;
					hierarchyData = hierarchyData2;
				}
			}
			Root?.Hierarchy.SetListViewDirty();
		}
	}

	public virtual bool Refresh()
	{
		if (m_depth < 0)
		{
			return false;
		}
		m_height = 1;
		bool flag = false;
		int childCount = ChildCount;
		if (IsExpanded)
		{
			if (childCount != children.Count)
			{
				flag = true;
			}
			RuntimeHierarchy runtimeHierarchy = null;
			for (int i = 0; i < childCount; i++)
			{
				Transform child = GetChild(i);
				if (children.Count <= i)
				{
					if (smethod_0((Object)runtimeHierarchy, (Object)null))
					{
						runtimeHierarchy = Root.Hierarchy;
					}
					Mo1lmkoHr6MpVMyvqYCIdadOXKj9Hb9c088FmaGri6MQ(child, i, runtimeHierarchy);
				}
				else if (smethod_1((Object)children[i].BoundTransform, (Object)child))
				{
					int j;
					for (j = 0; j < children.Count && !smethod_0((Object)children[j].BoundTransform, (Object)child); j++)
					{
					}
					if (j != children.Count)
					{
						HierarchyDataTransform item = children[j];
						children.RemoveAt(j);
						children.Insert(i, item);
					}
					else
					{
						if (smethod_0((Object)runtimeHierarchy, (Object)null))
						{
							runtimeHierarchy = Root.Hierarchy;
						}
						Mo1lmkoHr6MpVMyvqYCIdadOXKj9Hb9c088FmaGri6MQ(child, i, runtimeHierarchy);
					}
					flag = true;
				}
				flag |= children[i].Refresh();
				children[i].m_index = m_height - 1;
				m_height += children[i].m_height;
			}
			for (int num = children.Count - 1; num >= childCount; num--)
			{
				D5awv_9GIQ5YRTijM2raAzU(num);
			}
		}
		return flag;
	}

	public HierarchyData FindDataAtIndex(int index)
	{
		int num = children.Count - 1;
		if (index <= num && children[index].m_index == index)
		{
			int i;
			for (i = index; i < num && index == children[i + 1].m_index; i++)
			{
			}
			return children[i];
		}
		int num2 = 0;
		int j = num;
		while (num2 <= j)
		{
			int k = (num2 + j) / 2;
			int index2 = children[k].m_index;
			if (index != index2)
			{
				if (index >= index2)
				{
					num2 = k + 1;
				}
				else
				{
					j = k - 1;
				}
				continue;
			}
			for (; k < num && index == children[k + 1].m_index; k++)
			{
			}
			return children[k];
		}
		if (j < 0)
		{
			j = 0;
		}
		for (; j < num && index >= children[j + 1].m_index; j++)
		{
		}
		return children[j].FindDataAtIndex(index - 1 - children[j].m_index);
	}

	public HierarchyDataTransform FindTransform(Transform target, Transform nextInPath = null)
	{
		bool flag;
		if (flag = smethod_0((Object)nextInPath, (Object)null))
		{
			nextInPath = smethod_2(target);
			((HierarchyDataRoot)this).RefreshContent();
		}
		int num = IndexOf(nextInPath);
		if (num < 0)
		{
			if (!flag || !(this is HierarchyDataRootPseudoScene))
			{
				return null;
			}
			nextInPath = target;
			num = IndexOf(nextInPath);
			while (num < 0 && smethod_1((Object)nextInPath, (Object)null))
			{
				nextInPath = smethod_3(nextInPath);
				num = IndexOf(nextInPath);
			}
			if (num < 0)
			{
				return null;
			}
		}
		if (!CanExpand)
		{
			return null;
		}
		bool isExpanded;
		if (!(isExpanded = IsExpanded))
		{
			IsExpanded = true;
		}
		HierarchyDataTransform hierarchyDataTransform = children[num];
		if (smethod_0((Object)hierarchyDataTransform.BoundTransform, (Object)target))
		{
			return hierarchyDataTransform;
		}
		HierarchyDataTransform hierarchyDataTransform2 = null;
		if (smethod_0((Object)hierarchyDataTransform.BoundTransform, (Object)nextInPath))
		{
			Transform transform = target;
			Transform transform2 = smethod_3(transform);
			while (smethod_1((Object)transform2, (Object)null) && smethod_1((Object)transform2, (Object)nextInPath))
			{
				transform = transform2;
				transform2 = smethod_3(transform);
			}
			if (smethod_1((Object)transform2, (Object)null))
			{
				hierarchyDataTransform2 = hierarchyDataTransform.FindTransform(target, transform);
			}
		}
		if (hierarchyDataTransform2 != null && hierarchyDataTransform2.m_depth < 0)
		{
			hierarchyDataTransform2 = null;
		}
		if (hierarchyDataTransform2 == null && !isExpanded)
		{
			IsExpanded = false;
		}
		return hierarchyDataTransform2;
	}

	public abstract Transform GetChild(int index);

	public int IndexOf(Transform transform)
	{
		int num = ChildCount - 1;
		while (true)
		{
			if (num >= 0)
			{
				if ((object)GetChild(num) == transform)
				{
					break;
				}
				num--;
				continue;
			}
			return -1;
		}
		return num;
	}

	private void Mo1lmkoHr6MpVMyvqYCIdadOXKj9Hb9c088FmaGri6MQ(Transform transform_0, int int_0, RuntimeHierarchy runtimeHierarchy_0)
	{
		bool flag;
		if ((flag = !RuntimeInspectorUtils.IgnoredTransformsInHierarchy.Contains(transform_0)) && runtimeHierarchy_0.GameObjectFilter != null)
		{
			flag = runtimeHierarchy_0.GameObjectFilter(transform_0);
		}
		int num = zjGFrrVRoJy4oxpaZAYYJFwImZ2abdszY6Ztr9wR8vOc.Count - 1;
		HierarchyDataTransform hierarchyDataTransform;
		if (num >= 0)
		{
			hierarchyDataTransform = zjGFrrVRoJy4oxpaZAYYJFwImZ2abdszY6Ztr9wR8vOc[num];
			zjGFrrVRoJy4oxpaZAYYJFwImZ2abdszY6Ztr9wR8vOc.RemoveAt(num);
		}
		else
		{
			hierarchyDataTransform = new HierarchyDataTransform();
		}
		hierarchyDataTransform.Initialize(transform_0, this is HierarchyDataRootSearch);
		hierarchyDataTransform.parent = this;
		if (!flag)
		{
			hierarchyDataTransform.m_depth = -1;
			hierarchyDataTransform.m_height = 0;
		}
		else
		{
			hierarchyDataTransform.m_depth = m_depth + 1;
			hierarchyDataTransform.m_height = 1;
		}
		children.Insert(int_0, hierarchyDataTransform);
	}

	private void D5awv_9GIQ5YRTijM2raAzU(int int_0)
	{
		children[int_0].PoolData();
		zjGFrrVRoJy4oxpaZAYYJFwImZ2abdszY6Ztr9wR8vOc.Add(children[int_0]);
		children.RemoveAt(int_0);
	}

	protected void PoolChildrenList()
	{
		if (children != null)
		{
			for (int num = children.Count - 1; num >= 0; num--)
			{
				children[num].PoolData();
				zjGFrrVRoJy4oxpaZAYYJFwImZ2abdszY6Ztr9wR8vOc.Add(children[num]);
			}
			children.Clear();
			_ZNyiurTrGiY6wLPamgyGHCL2QPTfplV9CLqyuAn6Mko.Add(children);
			children = null;
		}
	}

	protected void PopChildrenList()
	{
		int childCount = ChildCount;
		int num = -1;
		int num2 = int.MaxValue;
		for (int num3 = _ZNyiurTrGiY6wLPamgyGHCL2QPTfplV9CLqyuAn6Mko.Count - 1; num3 >= 0; num3--)
		{
			int num4 = _ZNyiurTrGiY6wLPamgyGHCL2QPTfplV9CLqyuAn6Mko[num3].Capacity - childCount;
			if (num4 < 0)
			{
				num4 = -num4;
			}
			if (num4 < num2)
			{
				num2 = num4;
				num = num3;
			}
		}
		if (num < 0)
		{
			children = new List<HierarchyDataTransform>(ChildCount);
			return;
		}
		children = _ZNyiurTrGiY6wLPamgyGHCL2QPTfplV9CLqyuAn6Mko[num];
		_ZNyiurTrGiY6wLPamgyGHCL2QPTfplV9CLqyuAn6Mko.RemoveAt(num);
	}

	public static void ClearPool()
	{
		_ZNyiurTrGiY6wLPamgyGHCL2QPTfplV9CLqyuAn6Mko.Clear();
		zjGFrrVRoJy4oxpaZAYYJFwImZ2abdszY6Ztr9wR8vOc.Clear();
		if (_ZNyiurTrGiY6wLPamgyGHCL2QPTfplV9CLqyuAn6Mko.Capacity > 128)
		{
			_ZNyiurTrGiY6wLPamgyGHCL2QPTfplV9CLqyuAn6Mko.Capacity = 128;
		}
		if (zjGFrrVRoJy4oxpaZAYYJFwImZ2abdszY6Ztr9wR8vOc.Capacity > 128)
		{
			zjGFrrVRoJy4oxpaZAYYJFwImZ2abdszY6Ztr9wR8vOc.Capacity = 128;
		}
	}

	internal static bool smethod_0(Object object_0, Object object_1)
	{
		return object_0 == object_1;
	}

	internal static bool smethod_1(Object object_0, Object object_1)
	{
		return object_0 != object_1;
	}

	internal static Transform smethod_2(Transform transform_0)
	{
		return transform_0.root;
	}

	internal static Transform smethod_3(Transform transform_0)
	{
		return transform_0.parent;
	}
}
