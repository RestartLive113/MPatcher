using UnityEngine;

namespace RuntimeInspectorNamespace;

public abstract class SkinnedWindow : MonoBehaviour
{
	[SerializeField]
	private UISkin E58c_5PzPLk6LleLXcBTp_0024M;

	private int tez8QKQVeFGVS4AMHMsbzyw;

	public UISkin Skin
	{
		get
		{
			return E58c_5PzPLk6LleLXcBTp_0024M;
		}
		set
		{
			if (smethod_0((Object)value, (Object)null) && smethod_0((Object)E58c_5PzPLk6LleLXcBTp_0024M, (Object)value))
			{
				E58c_5PzPLk6LleLXcBTp_0024M = value;
				tez8QKQVeFGVS4AMHMsbzyw = E58c_5PzPLk6LleLXcBTp_0024M.Version - 1;
			}
		}
	}

	protected virtual void Awake()
	{
		tez8QKQVeFGVS4AMHMsbzyw = Skin.Version - 1;
		smethod_2(smethod_1((Component)this), bool_0: false);
		smethod_2(smethod_1((Component)this), bool_0: true);
	}

	protected virtual void Update()
	{
		if (tez8QKQVeFGVS4AMHMsbzyw != Skin.Version)
		{
			tez8QKQVeFGVS4AMHMsbzyw = Skin.Version;
			RefreshSkin();
		}
	}

	protected abstract void RefreshSkin();

	internal static bool smethod_0(Object object_0, Object object_1)
	{
		return object_0 != object_1;
	}

	internal static GameObject smethod_1(Component component_0)
	{
		return component_0.gameObject;
	}

	internal static void smethod_2(GameObject gameObject_0, bool bool_0)
	{
		gameObject_0.SetActive(bool_0);
	}
}
