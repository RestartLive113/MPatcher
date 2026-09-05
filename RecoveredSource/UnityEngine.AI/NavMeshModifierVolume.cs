using System.Collections.Generic;

namespace UnityEngine.AI;

[AddComponentMenu("Navigation/NavMeshModifierVolume", 31)]
[HelpURL("https://github.com/Unity-Technologies/NavMeshComponents#documentation-draft")]
[ExecuteInEditMode]
public class NavMeshModifierVolume : MonoBehaviour
{
	[SerializeField]
	private Vector3 y7pijjGDfzLI4VPDC5ksegE = new Vector3(4f, 3f, 4f);

	[SerializeField]
	private Vector3 kySW7Ox6C6B6A8sLtWj0SMY = new Vector3(0f, 1f, 0f);

	[SerializeField]
	private int int_0;

	[SerializeField]
	private List<int> tEadOQpw4qXpQA8585jaSayRRyGLQKr3VIDc8rqcc2mm = new List<int>(new int[1] { -1 });

	private static readonly List<NavMeshModifierVolume> list_0 = new List<NavMeshModifierVolume>();

	public Vector3 size
	{
		get
		{
			return y7pijjGDfzLI4VPDC5ksegE;
		}
		set
		{
			y7pijjGDfzLI4VPDC5ksegE = value;
		}
	}

	public Vector3 center
	{
		get
		{
			return kySW7Ox6C6B6A8sLtWj0SMY;
		}
		set
		{
			kySW7Ox6C6B6A8sLtWj0SMY = value;
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

	public static List<NavMeshModifierVolume> activeModifiers => list_0;

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
		if (tEadOQpw4qXpQA8585jaSayRRyGLQKr3VIDc8rqcc2mm[0] != -1)
		{
			return tEadOQpw4qXpQA8585jaSayRRyGLQKr3VIDc8rqcc2mm.IndexOf(agentTypeID) != -1;
		}
		return true;
	}
}
