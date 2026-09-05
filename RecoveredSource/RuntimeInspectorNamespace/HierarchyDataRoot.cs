using System.Runtime.CompilerServices;
using UnityEngine;

namespace RuntimeInspectorNamespace;

public abstract class HierarchyDataRoot : HierarchyData
{
	[CompilerGenerated]
	private RuntimeHierarchy runtimeHierarchy_0;

	public override Transform BoundTransform => null;

	public override bool IsActive => true;

	public RuntimeHierarchy Hierarchy
	{
		[CompilerGenerated]
		get
		{
			return runtimeHierarchy_0;
		}
		[CompilerGenerated]
		private set
		{
			runtimeHierarchy_0 = value;
		}
	}

	protected HierarchyDataRoot(RuntimeHierarchy runtimeHierarchy_1)
	{
		Hierarchy = runtimeHierarchy_1;
		PopChildrenList();
	}

	public abstract void RefreshContent();

	public override bool Refresh()
	{
		RefreshContent();
		return base.Refresh();
	}

	public void ResetCachedNames()
	{
		if (children != null)
		{
			for (int num = children.Count - 1; num >= 0; num--)
			{
				children[num].ResetCachedName();
			}
		}
	}

	public void RefreshNameOf(Transform target)
	{
		if (children != null)
		{
			for (int num = children.Count - 1; num >= 0; num--)
			{
				children[num].RefreshNameOf(target);
			}
		}
	}
}
