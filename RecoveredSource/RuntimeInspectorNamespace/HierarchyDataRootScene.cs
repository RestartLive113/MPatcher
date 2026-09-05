using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RuntimeInspectorNamespace;

public class HierarchyDataRootScene : HierarchyDataRoot
{
	[CompilerGenerated]
	private Scene ZHyW0Ku_nJ9ki2QvV_fbfS4a29EJLGbaWI6t_0024nsrdWaQ;

	private readonly List<GameObject> list_0 = new List<GameObject>();

	public override string Name => Scene.name;

	public override int ChildCount => list_0.Count;

	public Scene Scene
	{
		[CompilerGenerated]
		get
		{
			return ZHyW0Ku_nJ9ki2QvV_fbfS4a29EJLGbaWI6t_0024nsrdWaQ;
		}
		[CompilerGenerated]
		private set
		{
			ZHyW0Ku_nJ9ki2QvV_fbfS4a29EJLGbaWI6t_0024nsrdWaQ = value;
		}
	}

	public HierarchyDataRootScene(RuntimeHierarchy runtimeHierarchy_1, Scene scene_0)
		: base(runtimeHierarchy_1)
	{
		Scene = scene_0;
	}

	public override void RefreshContent()
	{
		list_0.Clear();
		Scene.GetRootGameObjects(list_0);
	}

	public override Transform GetChild(int index)
	{
		return smethod_4(list_0[index]);
	}

	internal static Transform smethod_4(GameObject gameObject_0)
	{
		return gameObject_0.transform;
	}
}
