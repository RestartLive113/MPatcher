using System;
using System.Collections.Generic;

namespace UnityEngine.AI;

[ExecuteInEditMode]
[AddComponentMenu("Navigation/NavMeshLink", 33)]
[DefaultExecutionOrder(-101)]
[HelpURL("https://github.com/Unity-Technologies/NavMeshComponents#documentation-draft")]
public class NavMeshLink : MonoBehaviour
{
	[SerializeField]
	private int FJY_0024lFTDF0Kxcxna2d2x6oQ;

	[SerializeField]
	private Vector3 ANokIU0e5HW6VKpfoor5HbY = new Vector3(0f, 0f, -2.5f);

	[SerializeField]
	private Vector3 e6ZOYTJS1rnlRci7KCpbl0E = new Vector3(0f, 0f, 2.5f);

	[SerializeField]
	private float dDo3ExMX8GnLEOCulxtGJxI;

	[SerializeField]
	private int SosI4XeG_0024zvhzx0g7WFMv7Q = -1;

	[SerializeField]
	private bool bool_0 = true;

	[SerializeField]
	private bool Bz76bomsMJVqcUKtb0N2p9NnN5E93yRv5v3YvwWEC6ue;

	[SerializeField]
	private int int_0;

	private NavMeshLinkInstance VR__ntNX_0024EhXL9NMjRYae_M;

	private Vector3 UVmPSNm_GFRc2y3GsJGyO14 = Vector3.zero;

	private Quaternion quaternion_0 = Quaternion.identity;

	private static readonly List<NavMeshLink> yrFWZl_K3YNNWD1TXMzTnWc = new List<NavMeshLink>();

	public int agentTypeID
	{
		get
		{
			return FJY_0024lFTDF0Kxcxna2d2x6oQ;
		}
		set
		{
			FJY_0024lFTDF0Kxcxna2d2x6oQ = value;
			UpdateLink();
		}
	}

	public Vector3 startPoint
	{
		get
		{
			return ANokIU0e5HW6VKpfoor5HbY;
		}
		set
		{
			ANokIU0e5HW6VKpfoor5HbY = value;
			UpdateLink();
		}
	}

	public Vector3 endPoint
	{
		get
		{
			return e6ZOYTJS1rnlRci7KCpbl0E;
		}
		set
		{
			e6ZOYTJS1rnlRci7KCpbl0E = value;
			UpdateLink();
		}
	}

	public float width
	{
		get
		{
			return dDo3ExMX8GnLEOCulxtGJxI;
		}
		set
		{
			dDo3ExMX8GnLEOCulxtGJxI = value;
			UpdateLink();
		}
	}

	public int costModifier
	{
		get
		{
			return SosI4XeG_0024zvhzx0g7WFMv7Q;
		}
		set
		{
			SosI4XeG_0024zvhzx0g7WFMv7Q = value;
			UpdateLink();
		}
	}

	public bool bidirectional
	{
		get
		{
			return bool_0;
		}
		set
		{
			bool_0 = value;
			UpdateLink();
		}
	}

	public bool autoUpdate
	{
		get
		{
			return Bz76bomsMJVqcUKtb0N2p9NnN5E93yRv5v3YvwWEC6ue;
		}
		set
		{
			method_0(value);
		}
	}

	public int area
	{
		get
		{
			return int_0;
		}
		set
		{
			int_0 = value;
			UpdateLink();
		}
	}

	private void xuEhI9_0024WIiXt4kVEz6N_t4k()
	{
		E53zsxT7eMsWzxDTm9gsd6Q();
		if (Bz76bomsMJVqcUKtb0N2p9NnN5E93yRv5v3YvwWEC6ue && VR__ntNX_0024EhXL9NMjRYae_M.valid)
		{
			yBXZ2_0024xRMvata3sPmCbAmsU(this);
		}
	}

	private void cc0I1X4J1KiSaf1sAukKVyE()
	{
		R75HUfwgba58PchJnZRO0_8(this);
		VR__ntNX_0024EhXL9NMjRYae_M.Remove();
	}

	public void UpdateLink()
	{
		VR__ntNX_0024EhXL9NMjRYae_M.Remove();
		E53zsxT7eMsWzxDTm9gsd6Q();
	}

	private static void yBXZ2_0024xRMvata3sPmCbAmsU(NavMeshLink navMeshLink_0)
	{
		if (yrFWZl_K3YNNWD1TXMzTnWc.Count == 0)
		{
			NavMesh.onPreUpdate = (NavMesh.OnNavMeshPreUpdate)smethod_0((Delegate)NavMesh.onPreUpdate, (Delegate)(NavMesh.OnNavMeshPreUpdate)delegate
			{
				foreach (NavMeshLink item in yrFWZl_K3YNNWD1TXMzTnWc)
				{
					if (item.gmVXR5_mnri_0024cyl2lj6oBd6Ki1m3AfndbPA8eQwMnYgA())
					{
						item.UpdateLink();
					}
				}
			});
		}
		yrFWZl_K3YNNWD1TXMzTnWc.Add(navMeshLink_0);
	}

	private static void R75HUfwgba58PchJnZRO0_8(NavMeshLink navMeshLink_0)
	{
		yrFWZl_K3YNNWD1TXMzTnWc.Remove(navMeshLink_0);
		if (yrFWZl_K3YNNWD1TXMzTnWc.Count != 0)
		{
			return;
		}
		NavMesh.onPreUpdate = (NavMesh.OnNavMeshPreUpdate)smethod_1((Delegate)NavMesh.onPreUpdate, (Delegate)(NavMesh.OnNavMeshPreUpdate)delegate
		{
			foreach (NavMeshLink item in yrFWZl_K3YNNWD1TXMzTnWc)
			{
				if (item.gmVXR5_mnri_0024cyl2lj6oBd6Ki1m3AfndbPA8eQwMnYgA())
				{
					item.UpdateLink();
				}
			}
		});
	}

	private void method_0(bool bool_1)
	{
		if (Bz76bomsMJVqcUKtb0N2p9NnN5E93yRv5v3YvwWEC6ue != bool_1)
		{
			Bz76bomsMJVqcUKtb0N2p9NnN5E93yRv5v3YvwWEC6ue = bool_1;
			if (!bool_1)
			{
				R75HUfwgba58PchJnZRO0_8(this);
			}
			else
			{
				yBXZ2_0024xRMvata3sPmCbAmsU(this);
			}
		}
	}

	private void E53zsxT7eMsWzxDTm9gsd6Q()
	{
		VR__ntNX_0024EhXL9NMjRYae_M = NavMesh.AddLink(new NavMeshLinkData
		{
			startPosition = ANokIU0e5HW6VKpfoor5HbY,
			endPosition = e6ZOYTJS1rnlRci7KCpbl0E,
			width = dDo3ExMX8GnLEOCulxtGJxI,
			costModifier = SosI4XeG_0024zvhzx0g7WFMv7Q,
			bidirectional = bool_0,
			area = int_0,
			agentTypeID = FJY_0024lFTDF0Kxcxna2d2x6oQ
		}, base.transform.position, base.transform.rotation);
		if (VR__ntNX_0024EhXL9NMjRYae_M.valid)
		{
			VR__ntNX_0024EhXL9NMjRYae_M.owner = this;
		}
		UVmPSNm_GFRc2y3GsJGyO14 = base.transform.position;
		quaternion_0 = base.transform.rotation;
	}

	private bool gmVXR5_mnri_0024cyl2lj6oBd6Ki1m3AfndbPA8eQwMnYgA()
	{
		if (UVmPSNm_GFRc2y3GsJGyO14 != smethod_3(smethod_2((Component)this)))
		{
			return true;
		}
		if (quaternion_0 != smethod_4(smethod_2((Component)this)))
		{
			return true;
		}
		return false;
	}

	private void uUStUob0SV6nldkZvz1UxPByzeTYpwvO3hr5WF9gbyDQ()
	{
		UpdateLink();
	}

	private static void HR_0024WCgwR1o3Ln7sQlsa0syg7YDbi_0024SO0e_Gh0uhfPwGs()
	{
		foreach (NavMeshLink item in yrFWZl_K3YNNWD1TXMzTnWc)
		{
			if (item.gmVXR5_mnri_0024cyl2lj6oBd6Ki1m3AfndbPA8eQwMnYgA())
			{
				item.UpdateLink();
			}
		}
	}

	internal static Delegate smethod_0(Delegate delegate_0, Delegate delegate_1)
	{
		return Delegate.Combine(delegate_0, delegate_1);
	}

	internal static Delegate smethod_1(Delegate delegate_0, Delegate delegate_1)
	{
		return Delegate.Remove(delegate_0, delegate_1);
	}

	internal static Transform smethod_2(Component component_0)
	{
		return component_0.transform;
	}

	internal static Vector3 smethod_3(Transform transform_0)
	{
		return transform_0.position;
	}

	internal static Quaternion smethod_4(Transform transform_0)
	{
		return transform_0.rotation;
	}
}
