using UnityEngine;

namespace RuntimeInspectorNamespace;

public class HierarchyDataTransform : HierarchyData
{
	private string hUsbWOgCrWgrgJc5e4qxrg0;

	private Transform TIWjI8FsBk2nlZk9NO4HNOE;

	private bool bool_0;

	public override string Name
	{
		get
		{
			if (hUsbWOgCrWgrgJc5e4qxrg0 == null)
			{
				hUsbWOgCrWgrgJc5e4qxrg0 = (smethod_4((Object)TIWjI8FsBk2nlZk9NO4HNOE) ? smethod_5((Object)TIWjI8FsBk2nlZk9NO4HNOE) : global::_003CModule_003E.smethod_28<string>(3548863479u));
			}
			return hUsbWOgCrWgrgJc5e4qxrg0;
		}
	}

	public override int ChildCount
	{
		get
		{
			if (!bool_0 && smethod_4((Object)TIWjI8FsBk2nlZk9NO4HNOE))
			{
				return smethod_6(TIWjI8FsBk2nlZk9NO4HNOE);
			}
			return 0;
		}
	}

	public override Transform BoundTransform => TIWjI8FsBk2nlZk9NO4HNOE;

	public override bool IsActive
	{
		get
		{
			if (!smethod_4((Object)TIWjI8FsBk2nlZk9NO4HNOE))
			{
				return true;
			}
			return smethod_8(smethod_7((Component)TIWjI8FsBk2nlZk9NO4HNOE));
		}
	}

	public void Initialize(Transform transform, bool isSearchEntry)
	{
		TIWjI8FsBk2nlZk9NO4HNOE = transform;
		bool_0 = isSearchEntry;
	}

	public override Transform GetChild(int index)
	{
		return smethod_9(TIWjI8FsBk2nlZk9NO4HNOE, index);
	}

	public void ResetCachedName()
	{
		hUsbWOgCrWgrgJc5e4qxrg0 = null;
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
		if ((object)TIWjI8FsBk2nlZk9NO4HNOE == target)
		{
			hUsbWOgCrWgrgJc5e4qxrg0 = smethod_5((Object)target);
		}
		else if (children != null)
		{
			for (int num = children.Count - 1; num >= 0; num--)
			{
				children[num].RefreshNameOf(target);
			}
		}
	}

	public void PoolData()
	{
		parent = null;
		hUsbWOgCrWgrgJc5e4qxrg0 = null;
		m_depth = 0;
		m_height = 0;
		PoolChildrenList();
	}

	internal static bool smethod_4(Object object_0)
	{
		return object_0;
	}

	internal static string smethod_5(Object object_0)
	{
		return object_0.name;
	}

	internal static int smethod_6(Transform transform_0)
	{
		return transform_0.childCount;
	}

	internal static GameObject smethod_7(Component component_0)
	{
		return component_0.gameObject;
	}

	internal static bool smethod_8(GameObject gameObject_0)
	{
		return gameObject_0.activeInHierarchy;
	}

	internal static Transform smethod_9(Transform transform_0, int int_0)
	{
		return transform_0.GetChild(int_0);
	}
}
