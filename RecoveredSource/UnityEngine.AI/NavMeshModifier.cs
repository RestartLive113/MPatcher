using System.Collections.Generic;

namespace UnityEngine.AI;

[AddComponentMenu("Navigation/NavMeshModifier", 32)]
[HelpURL("https://github.com/Unity-Technologies/NavMeshComponents#documentation-draft")]
[ExecuteInEditMode]
public class NavMeshModifier : MonoBehaviour
{
	[SerializeField]
	private bool FwcqJkkl36D3f7WPzI8RpRQ;

	[SerializeField]
	private int int_0;

	[SerializeField]
	private bool CB8hca4TIUeoEY3pLidxyYkptpxJg_HboYPKHMhVQdMj;

	[SerializeField]
	private List<int> tEadOQpw4qXpQA8585jaSayRRyGLQKr3VIDc8rqcc2mm = new List<int>(new int[1] { -1 });

	private static readonly List<NavMeshModifier> list_0 = new List<NavMeshModifier>();

	public bool overrideArea
	{
		get
		{
			return FwcqJkkl36D3f7WPzI8RpRQ;
		}
		set
		{
			FwcqJkkl36D3f7WPzI8RpRQ = value;
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
		}
	}

	public bool ignoreFromBuild
	{
		get
		{
			return CB8hca4TIUeoEY3pLidxyYkptpxJg_HboYPKHMhVQdMj;
		}
		set
		{
			CB8hca4TIUeoEY3pLidxyYkptpxJg_HboYPKHMhVQdMj = value;
		}
	}

	public static List<NavMeshModifier> activeModifiers => list_0;

	private void xuEhI9_0024WIiXt4kVEz6N_t4k()
	{
		if (!list_0.Contains(this))
		{
			list_0.Add(this);
		}
	}

	private void cc0I1X4J1KiSaf1sAukKVyE()
	{
		list_0.Remove(this);
	}

	public bool AffectsAgentType(int agentTypeID)
	{
		if (tEadOQpw4qXpQA8585jaSayRRyGLQKr3VIDc8rqcc2mm.Count == 0)
		{
			return false;
		}
		if (tEadOQpw4qXpQA8585jaSayRRyGLQKr3VIDc8rqcc2mm[0] == -1)
		{
			return true;
		}
		return tEadOQpw4qXpQA8585jaSayRRyGLQKr3VIDc8rqcc2mm.IndexOf(agentTypeID) != -1;
	}
}
