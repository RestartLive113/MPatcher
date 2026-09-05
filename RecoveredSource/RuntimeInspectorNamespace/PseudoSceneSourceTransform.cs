using System.Collections.Generic;
using UnityEngine;

namespace RuntimeInspectorNamespace;

public class PseudoSceneSourceTransform : MonoBehaviour
{
	[SerializeField]
	private RuntimeHierarchy Uy2HlwH2dzfyoReu8hLfkkQ;

	[SerializeField]
	private string kQU6alK9vCUFTylBIJxV1d0;

	[SerializeField]
	private bool sbFrDKXEF5ohJdzzzJAEAUc;

	private HashSet<Transform> hashSet_0 = new HashSet<Transform>();

	private HashSet<Transform> LZOqo_xL9_0024qdU5OC21pFC6E = new HashSet<Transform>();

	private bool bool_0;

	private bool x_cm7IPSOt3QItaHBlqY9zQ = true;

	private bool kwYjgwx1CNO_PSc5gl0nS74;

	public RuntimeHierarchy Hierarchy
	{
		get
		{
			return Uy2HlwH2dzfyoReu8hLfkkQ;
		}
		set
		{
			if (smethod_0((Object)Uy2HlwH2dzfyoReu8hLfkkQ, (Object)value))
			{
				kxUhG8eOq_0024t1kJ1K8xu35he49fr_0024WCimD8agc2cVdY5d();
				Uy2HlwH2dzfyoReu8hLfkkQ = value;
				rQ3OYSa0zrOn6hC1fuv3DSWYv4EJXaJUNgKxzldRd8Nl();
			}
		}
	}

	public string SceneName
	{
		get
		{
			return kQU6alK9vCUFTylBIJxV1d0;
		}
		set
		{
			if (smethod_1(kQU6alK9vCUFTylBIJxV1d0, value))
			{
				kxUhG8eOq_0024t1kJ1K8xu35he49fr_0024WCimD8agc2cVdY5d();
				kQU6alK9vCUFTylBIJxV1d0 = value;
				rQ3OYSa0zrOn6hC1fuv3DSWYv4EJXaJUNgKxzldRd8Nl();
			}
		}
	}

	public bool HideOnDisable
	{
		get
		{
			return sbFrDKXEF5ohJdzzzJAEAUc;
		}
		set
		{
			if (sbFrDKXEF5ohJdzzzJAEAUc == value)
			{
				return;
			}
			sbFrDKXEF5ohJdzzzJAEAUc = value;
			if (!x_cm7IPSOt3QItaHBlqY9zQ)
			{
				if (value)
				{
					kxUhG8eOq_0024t1kJ1K8xu35he49fr_0024WCimD8agc2cVdY5d();
				}
				else
				{
					rQ3OYSa0zrOn6hC1fuv3DSWYv4EJXaJUNgKxzldRd8Nl();
				}
			}
		}
	}

	private bool ShouldUpdateChildren
	{
		get
		{
			if ((x_cm7IPSOt3QItaHBlqY9zQ || !sbFrDKXEF5ohJdzzzJAEAUc) && smethod_2((Object)Hierarchy))
			{
				return !smethod_3(kQU6alK9vCUFTylBIJxV1d0);
			}
			return false;
		}
	}

	private void xuEhI9_0024WIiXt4kVEz6N_t4k()
	{
		x_cm7IPSOt3QItaHBlqY9zQ = true;
		bool_0 = true;
	}

	private void cc0I1X4J1KiSaf1sAukKVyE()
	{
		if (!kwYjgwx1CNO_PSc5gl0nS74)
		{
			x_cm7IPSOt3QItaHBlqY9zQ = false;
			if (sbFrDKXEF5ohJdzzzJAEAUc)
			{
				kxUhG8eOq_0024t1kJ1K8xu35he49fr_0024WCimD8agc2cVdY5d();
			}
		}
	}

	private void AEsuX0tvDY3Z80YFoAUpXUOCGbJ5cignL68b5czDDDWf()
	{
		kwYjgwx1CNO_PSc5gl0nS74 = true;
	}

	private void method_0()
	{
		bool_0 = true;
	}

	private void method_1()
	{
		if (!bool_0)
		{
			return;
		}
		bool_0 = false;
		if (!ShouldUpdateChildren)
		{
			return;
		}
		for (int i = 0; i < smethod_6(smethod_4((Component)this)); i++)
		{
			Transform item = smethod_5(smethod_4((Component)this), i);
			LZOqo_xL9_0024qdU5OC21pFC6E.Add(item);
			if (!hashSet_0.Remove(item))
			{
				Hierarchy.AddToPseudoScene(kQU6alK9vCUFTylBIJxV1d0, item);
			}
		}
		kxUhG8eOq_0024t1kJ1K8xu35he49fr_0024WCimD8agc2cVdY5d();
		HashSet<Transform> lZOqo_xL9_0024qdU5OC21pFC6E = hashSet_0;
		hashSet_0 = LZOqo_xL9_0024qdU5OC21pFC6E;
		LZOqo_xL9_0024qdU5OC21pFC6E = lZOqo_xL9_0024qdU5OC21pFC6E;
	}

	private void rQ3OYSa0zrOn6hC1fuv3DSWYv4EJXaJUNgKxzldRd8Nl()
	{
		if (!ShouldUpdateChildren)
		{
			return;
		}
		for (int i = 0; i < smethod_6(smethod_4((Component)this)); i++)
		{
			Transform item = smethod_5(smethod_4((Component)this), i);
			if (hashSet_0.Add(item))
			{
				Hierarchy.AddToPseudoScene(kQU6alK9vCUFTylBIJxV1d0, item);
			}
		}
	}

	private void kxUhG8eOq_0024t1kJ1K8xu35he49fr_0024WCimD8agc2cVdY5d()
	{
		if (!smethod_2((Object)Hierarchy) || smethod_3(kQU6alK9vCUFTylBIJxV1d0))
		{
			return;
		}
		foreach (Transform item in hashSet_0)
		{
			if (smethod_2((Object)item))
			{
				Hierarchy.RemoveFromPseudoScene(kQU6alK9vCUFTylBIJxV1d0, item, deleteSceneIfEmpty: true);
			}
		}
		hashSet_0.Clear();
	}

	internal static bool smethod_0(Object object_0, Object object_1)
	{
		return object_0 != object_1;
	}

	internal static bool smethod_1(string string_0, string string_1)
	{
		return string_0 != string_1;
	}

	internal static bool smethod_2(Object object_0)
	{
		return object_0;
	}

	internal static bool smethod_3(string string_0)
	{
		return string.IsNullOrEmpty(string_0);
	}

	internal static Transform smethod_4(Component component_0)
	{
		return component_0.transform;
	}

	internal static Transform smethod_5(Transform transform_0, int int_0)
	{
		return transform_0.GetChild(int_0);
	}

	internal static int smethod_6(Transform transform_0)
	{
		return transform_0.childCount;
	}
}
