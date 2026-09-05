using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Serialization;

namespace UnityEngine.AI;

[ExecuteInEditMode]
[AddComponentMenu("Navigation/NavMeshSurface", 30)]
[DefaultExecutionOrder(-102)]
[HelpURL("https://github.com/Unity-Technologies/NavMeshComponents#documentation-draft")]
public class NavMeshSurface : MonoBehaviour
{
	[Serializable]
	[CompilerGenerated]
	private sealed class KgHQq5GNh_0024aVfH9u1blUm6BjjieahlvseNoOKUZq9Kn0m8otVauu5GDNKa_0024lM3D8rA
	{
		public static readonly KgHQq5GNh_0024aVfH9u1blUm6BjjieahlvseNoOKUZq9Kn0m8otVauu5GDNKa_0024lM3D8rA _003C_003E9 = new KgHQq5GNh_0024aVfH9u1blUm6BjjieahlvseNoOKUZq9Kn0m8otVauu5GDNKa_0024lM3D8rA();

		public static Predicate<NavMeshModifierVolume> _003C_003E9__76_0;

		public static Predicate<NavMeshModifier> _003C_003E9__77_0;

		public static Predicate<NavMeshBuildSource> _003C_003E9__77_1;

		public static Predicate<NavMeshBuildSource> _003C_003E9__77_2;

		internal bool OIoH0wUVpmS8hwZTMwR9R50pMFz2jbDthatLvlnXFyC7(NavMeshModifierVolume navMeshModifierVolume_0)
		{
			return !smethod_0((Behaviour)navMeshModifierVolume_0);
		}

		internal bool M8q7uJ_FEP_0024DLawi2kl8hPQ_0024txdpUPoFOIHyxm1_0024cgrt(NavMeshModifier navMeshModifier_0)
		{
			return !smethod_0((Behaviour)navMeshModifier_0);
		}

		internal bool NOaS5TGBg4LQ7iwQGjltCQN4LyBK_0024PrvLRUJzdrMLCnv(NavMeshBuildSource navMeshBuildSource_0)
		{
			if (navMeshBuildSource_0.component != null)
			{
				return navMeshBuildSource_0.component.gameObject.GetComponent<NavMeshAgent>() != null;
			}
			return false;
		}

		internal bool NdYPckKxMSO8oACmkFES_0024iHdOQMiy4sLBZqaSEd_x_km(NavMeshBuildSource navMeshBuildSource_0)
		{
			if (navMeshBuildSource_0.component != null)
			{
				return navMeshBuildSource_0.component.gameObject.GetComponent<NavMeshObstacle>() != null;
			}
			return false;
		}

		internal static bool smethod_0(Behaviour behaviour_0)
		{
			return behaviour_0.isActiveAndEnabled;
		}
	}

	[SerializeField]
	private int FJY_0024lFTDF0Kxcxna2d2x6oQ;

	[SerializeField]
	private CollectObjects pLHxtHT1c1AwJp5xHvGvQmzdZBOvjyJbR7wg4dtdDfdc;

	[SerializeField]
	private Vector3 y7pijjGDfzLI4VPDC5ksegE = new Vector3(10f, 10f, 10f);

	[SerializeField]
	private Vector3 kySW7Ox6C6B6A8sLtWj0SMY = new Vector3(0f, 2f, 0f);

	[SerializeField]
	private LayerMask d_y_0024cFKTCwUHK7teFyZSxtI = -1;

	[SerializeField]
	private NavMeshCollectGeometry navMeshCollectGeometry_0;

	[SerializeField]
	private int int_0;

	[SerializeField]
	private bool bool_0 = true;

	[SerializeField]
	private bool Q4FxWXQr85xEkfHnWVikrJbxWSB1YUDRAY6x_0024MSh8pfd = true;

	[SerializeField]
	private bool JTM5F7PFzJCOwQ0IwErmhbiwgBYufPiAu_4cjPX7yqet;

	[SerializeField]
	private int wZDo2vCXPS4S26kp_b5IVaw = 256;

	[SerializeField]
	private bool wYRQ9yURqX8dtTjcODxxssmNWBtMPUgjOWY8Pyb_0024J8D4;

	[SerializeField]
	private float lSECJr0gcjGs2a9dhOUO9yc;

	[SerializeField]
	private bool TJadeORbs2vgAn5_0024m1_UoCdrOXPQi95UO8wGYlK5byzN;

	[SerializeField]
	[FormerlySerializedAs("m_BakedNavMeshData")]
	private NavMeshData SKBHDWFPFyCWrqwIX0rkdd4;

	private NavMeshDataInstance rRHBpULgXuM_00249LfmKWBADJ0euZromwy_LTGkNqgPMIk5;

	private Vector3 UVmPSNm_GFRc2y3GsJGyO14 = Vector3.zero;

	private Quaternion quaternion_0 = Quaternion.identity;

	private static readonly List<NavMeshSurface> buF9LQETFQkabAgVijCHX8wGtURHr0VmUkXsO8wEr4A3 = new List<NavMeshSurface>();

	public int agentTypeID
	{
		get
		{
			return FJY_0024lFTDF0Kxcxna2d2x6oQ;
		}
		set
		{
			FJY_0024lFTDF0Kxcxna2d2x6oQ = value;
		}
	}

	public CollectObjects collectObjects
	{
		get
		{
			return pLHxtHT1c1AwJp5xHvGvQmzdZBOvjyJbR7wg4dtdDfdc;
		}
		set
		{
			pLHxtHT1c1AwJp5xHvGvQmzdZBOvjyJbR7wg4dtdDfdc = value;
		}
	}

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

	public LayerMask layerMask
	{
		get
		{
			return d_y_0024cFKTCwUHK7teFyZSxtI;
		}
		set
		{
			d_y_0024cFKTCwUHK7teFyZSxtI = value;
		}
	}

	public NavMeshCollectGeometry useGeometry
	{
		get
		{
			return navMeshCollectGeometry_0;
		}
		set
		{
			navMeshCollectGeometry_0 = value;
		}
	}

	public int defaultArea
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

	public bool ignoreNavMeshAgent
	{
		get
		{
			return bool_0;
		}
		set
		{
			bool_0 = value;
		}
	}

	public bool ignoreNavMeshObstacle
	{
		get
		{
			return Q4FxWXQr85xEkfHnWVikrJbxWSB1YUDRAY6x_0024MSh8pfd;
		}
		set
		{
			Q4FxWXQr85xEkfHnWVikrJbxWSB1YUDRAY6x_0024MSh8pfd = value;
		}
	}

	public bool overrideTileSize
	{
		get
		{
			return JTM5F7PFzJCOwQ0IwErmhbiwgBYufPiAu_4cjPX7yqet;
		}
		set
		{
			JTM5F7PFzJCOwQ0IwErmhbiwgBYufPiAu_4cjPX7yqet = value;
		}
	}

	public int tileSize
	{
		get
		{
			return wZDo2vCXPS4S26kp_b5IVaw;
		}
		set
		{
			wZDo2vCXPS4S26kp_b5IVaw = value;
		}
	}

	public bool overrideVoxelSize
	{
		get
		{
			return wYRQ9yURqX8dtTjcODxxssmNWBtMPUgjOWY8Pyb_0024J8D4;
		}
		set
		{
			wYRQ9yURqX8dtTjcODxxssmNWBtMPUgjOWY8Pyb_0024J8D4 = value;
		}
	}

	public float voxelSize
	{
		get
		{
			return lSECJr0gcjGs2a9dhOUO9yc;
		}
		set
		{
			lSECJr0gcjGs2a9dhOUO9yc = value;
		}
	}

	public bool buildHeightMesh
	{
		get
		{
			return TJadeORbs2vgAn5_0024m1_UoCdrOXPQi95UO8wGYlK5byzN;
		}
		set
		{
			TJadeORbs2vgAn5_0024m1_UoCdrOXPQi95UO8wGYlK5byzN = value;
		}
	}

	public NavMeshData navMeshData
	{
		get
		{
			return SKBHDWFPFyCWrqwIX0rkdd4;
		}
		set
		{
			SKBHDWFPFyCWrqwIX0rkdd4 = value;
		}
	}

	public static List<NavMeshSurface> activeSurfaces => buF9LQETFQkabAgVijCHX8wGtURHr0VmUkXsO8wEr4A3;

	private void xuEhI9_0024WIiXt4kVEz6N_t4k()
	{
		smethod_0(this);
		AddData();
	}

	private void cc0I1X4J1KiSaf1sAukKVyE()
	{
		RemoveData();
		smethod_1(this);
	}

	public void AddData()
	{
		if (!rRHBpULgXuM_00249LfmKWBADJ0euZromwy_LTGkNqgPMIk5.valid)
		{
			if (SKBHDWFPFyCWrqwIX0rkdd4 != null)
			{
				rRHBpULgXuM_00249LfmKWBADJ0euZromwy_LTGkNqgPMIk5 = NavMesh.AddNavMeshData(SKBHDWFPFyCWrqwIX0rkdd4, base.transform.position, base.transform.rotation);
				rRHBpULgXuM_00249LfmKWBADJ0euZromwy_LTGkNqgPMIk5.owner = this;
			}
			UVmPSNm_GFRc2y3GsJGyO14 = base.transform.position;
			quaternion_0 = base.transform.rotation;
		}
	}

	public void RemoveData()
	{
		rRHBpULgXuM_00249LfmKWBADJ0euZromwy_LTGkNqgPMIk5.Remove();
		rRHBpULgXuM_00249LfmKWBADJ0euZromwy_LTGkNqgPMIk5 = default(NavMeshDataInstance);
	}

	public NavMeshBuildSettings GetBuildSettings()
	{
		NavMeshBuildSettings result = smethod_4(FJY_0024lFTDF0Kxcxna2d2x6oQ);
		if (result.agentTypeID == -1)
		{
			Debug.LogWarning(global::_003CModule_003E.smethod_26<string>(2728659477u) + agentTypeID, this);
			result.agentTypeID = FJY_0024lFTDF0Kxcxna2d2x6oQ;
		}
		if (overrideTileSize)
		{
			result.overrideTileSize = true;
			result.tileSize = tileSize;
		}
		if (overrideVoxelSize)
		{
			result.overrideVoxelSize = true;
			result.voxelSize = voxelSize;
		}
		return result;
	}

	public void BuildNavMesh()
	{
		List<NavMeshBuildSource> list_ = loYXgS8iz_0024zwZto_00244Dpvfag();
		Bounds bounds_ = new Bounds(kySW7Ox6C6B6A8sLtWj0SMY, smethod_2(y7pijjGDfzLI4VPDC5ksegE));
		if (pLHxtHT1c1AwJp5xHvGvQmzdZBOvjyJbR7wg4dtdDfdc == CollectObjects.All || pLHxtHT1c1AwJp5xHvGvQmzdZBOvjyJbR7wg4dtdDfdc == CollectObjects.Children)
		{
			bounds_ = EfTltsxxExPEoMkBgobW488owF3Kq0iLCR6io70rCzCT(list_);
		}
		NavMeshData navMeshData = smethod_8(GetBuildSettings(), list_, bounds_, smethod_6(smethod_5((Component)this)), smethod_7(smethod_5((Component)this)));
		if (smethod_9((Object)navMeshData, (Object)null))
		{
			smethod_12((Object)navMeshData, smethod_11((Object)smethod_10((Component)this)));
			RemoveData();
			SKBHDWFPFyCWrqwIX0rkdd4 = navMeshData;
			if (smethod_13((Behaviour)this))
			{
				AddData();
			}
		}
	}

	public AsyncOperation UpdateNavMesh(NavMeshData data)
	{
		List<NavMeshBuildSource> list_ = loYXgS8iz_0024zwZto_00244Dpvfag();
		Bounds bounds_ = new Bounds(kySW7Ox6C6B6A8sLtWj0SMY, smethod_2(y7pijjGDfzLI4VPDC5ksegE));
		if (pLHxtHT1c1AwJp5xHvGvQmzdZBOvjyJbR7wg4dtdDfdc == CollectObjects.All || pLHxtHT1c1AwJp5xHvGvQmzdZBOvjyJbR7wg4dtdDfdc == CollectObjects.Children)
		{
			bounds_ = EfTltsxxExPEoMkBgobW488owF3Kq0iLCR6io70rCzCT(list_);
		}
		return smethod_14(data, GetBuildSettings(), list_, bounds_);
	}

	internal static void smethod_0(NavMeshSurface navMeshSurface_0)
	{
		if (buF9LQETFQkabAgVijCHX8wGtURHr0VmUkXsO8wEr4A3.Count == 0)
		{
			NavMesh.onPreUpdate = (NavMesh.OnNavMeshPreUpdate)smethod_15((Delegate)NavMesh.onPreUpdate, (Delegate)new NavMesh.OnNavMeshPreUpdate(R1szwU2b0QR61vxi_VFnigQ));
		}
		if (!buF9LQETFQkabAgVijCHX8wGtURHr0VmUkXsO8wEr4A3.Contains(navMeshSurface_0))
		{
			buF9LQETFQkabAgVijCHX8wGtURHr0VmUkXsO8wEr4A3.Add(navMeshSurface_0);
		}
	}

	internal static void smethod_1(NavMeshSurface navMeshSurface_0)
	{
		buF9LQETFQkabAgVijCHX8wGtURHr0VmUkXsO8wEr4A3.Remove(navMeshSurface_0);
		if (buF9LQETFQkabAgVijCHX8wGtURHr0VmUkXsO8wEr4A3.Count == 0)
		{
			NavMesh.onPreUpdate = (NavMesh.OnNavMeshPreUpdate)smethod_16((Delegate)NavMesh.onPreUpdate, (Delegate)new NavMesh.OnNavMeshPreUpdate(R1szwU2b0QR61vxi_VFnigQ));
		}
	}

	private static void R1szwU2b0QR61vxi_VFnigQ()
	{
		for (int i = 0; i < buF9LQETFQkabAgVijCHX8wGtURHr0VmUkXsO8wEr4A3.Count; i++)
		{
			buF9LQETFQkabAgVijCHX8wGtURHr0VmUkXsO8wEr4A3[i].lGMRk2OLu4BMJe8wO1U_KS5taEyUPqt2ccNKLrjQnOek();
		}
	}

	private void THJJi6hQ7m_NZ01eHOxaYqqveeHzkzPj1yiT55JGA0GN(ref List<NavMeshBuildSource> list_0)
	{
		List<NavMeshModifierVolume> list;
		if (pLHxtHT1c1AwJp5xHvGvQmzdZBOvjyJbR7wg4dtdDfdc == CollectObjects.Children)
		{
			list = new List<NavMeshModifierVolume>(GetComponentsInChildren<NavMeshModifierVolume>());
			list.RemoveAll((NavMeshModifierVolume navMeshModifierVolume_0) => !KgHQq5GNh_0024aVfH9u1blUm6BjjieahlvseNoOKUZq9Kn0m8otVauu5GDNKa_0024lM3D8rA.smethod_0((Behaviour)navMeshModifierVolume_0));
		}
		else
		{
			list = NavMeshModifierVolume.activeModifiers;
		}
		foreach (NavMeshModifierVolume item2 in list)
		{
			if (((int)d_y_0024cFKTCwUHK7teFyZSxtI & (1 << smethod_18(smethod_17((Component)item2)))) != 0 && item2.AffectsAgentType(FJY_0024lFTDF0Kxcxna2d2x6oQ))
			{
				Vector3 pos = smethod_20(smethod_19((Component)item2), item2.center);
				Vector3 vector = smethod_21(smethod_19((Component)item2));
				Vector3 vector2 = new Vector3(item2.size.x * Mathf.Abs(vector.x), item2.size.y * Mathf.Abs(vector.y), item2.size.z * Mathf.Abs(vector.z));
				NavMeshBuildSource item = new NavMeshBuildSource
				{
					shape = NavMeshBuildSourceShape.ModifierBox,
					transform = Matrix4x4.TRS(pos, item2.transform.rotation, Vector3.one),
					size = vector2,
					area = item2.area
				};
				list_0.Add(item);
			}
		}
	}

	private List<NavMeshBuildSource> loYXgS8iz_0024zwZto_00244Dpvfag()
	{
		List<NavMeshBuildSource> list_ = new List<NavMeshBuildSource>();
		List<NavMeshBuildMarkup> list = new List<NavMeshBuildMarkup>();
		List<NavMeshModifier> list2;
		if (pLHxtHT1c1AwJp5xHvGvQmzdZBOvjyJbR7wg4dtdDfdc != CollectObjects.Children)
		{
			list2 = NavMeshModifier.activeModifiers;
		}
		else
		{
			list2 = new List<NavMeshModifier>(GetComponentsInChildren<NavMeshModifier>());
			list2.RemoveAll((NavMeshModifier navMeshModifier_0) => !KgHQq5GNh_0024aVfH9u1blUm6BjjieahlvseNoOKUZq9Kn0m8otVauu5GDNKa_0024lM3D8rA.smethod_0((Behaviour)navMeshModifier_0));
		}
		foreach (NavMeshModifier item in list2)
		{
			if (((int)d_y_0024cFKTCwUHK7teFyZSxtI & (1 << smethod_18(smethod_17((Component)item)))) != 0 && item.AffectsAgentType(FJY_0024lFTDF0Kxcxna2d2x6oQ))
			{
				list.Add(new NavMeshBuildMarkup
				{
					root = smethod_19((Component)item),
					overrideArea = item.overrideArea,
					area = item.area,
					ignoreFromBuild = item.ignoreFromBuild
				});
			}
		}
		if (pLHxtHT1c1AwJp5xHvGvQmzdZBOvjyJbR7wg4dtdDfdc == CollectObjects.All)
		{
			NavMeshBuilder.CollectSources(null, d_y_0024cFKTCwUHK7teFyZSxtI, navMeshCollectGeometry_0, int_0, list, list_);
		}
		else if (pLHxtHT1c1AwJp5xHvGvQmzdZBOvjyJbR7wg4dtdDfdc == CollectObjects.Children)
		{
			NavMeshBuilder.CollectSources(base.transform, d_y_0024cFKTCwUHK7teFyZSxtI, navMeshCollectGeometry_0, int_0, list, list_);
		}
		else if (pLHxtHT1c1AwJp5xHvGvQmzdZBOvjyJbR7wg4dtdDfdc == CollectObjects.Volume)
		{
			NavMeshBuilder.CollectSources(smethod_3(Matrix4x4.TRS(base.transform.position, base.transform.rotation, Vector3.one), new Bounds(kySW7Ox6C6B6A8sLtWj0SMY, y7pijjGDfzLI4VPDC5ksegE)), d_y_0024cFKTCwUHK7teFyZSxtI, navMeshCollectGeometry_0, int_0, list, list_);
		}
		if (bool_0)
		{
			list_.RemoveAll((NavMeshBuildSource navMeshBuildSource_0) => navMeshBuildSource_0.component != null && navMeshBuildSource_0.component.gameObject.GetComponent<NavMeshAgent>() != null);
		}
		if (Q4FxWXQr85xEkfHnWVikrJbxWSB1YUDRAY6x_0024MSh8pfd)
		{
			list_.RemoveAll((NavMeshBuildSource navMeshBuildSource_0) => navMeshBuildSource_0.component != null && navMeshBuildSource_0.component.gameObject.GetComponent<NavMeshObstacle>() != null);
		}
		THJJi6hQ7m_NZ01eHOxaYqqveeHzkzPj1yiT55JGA0GN(ref list_);
		return list_;
	}

	internal static Vector3 smethod_2(Vector3 vector3_0)
	{
		return new Vector3(Mathf.Abs(vector3_0.x), Mathf.Abs(vector3_0.y), Mathf.Abs(vector3_0.z));
	}

	internal static Bounds smethod_3(Matrix4x4 matrix4x4_0, Bounds bounds_0)
	{
		Vector3 vector = smethod_2(matrix4x4_0.MultiplyVector(Vector3.right));
		Vector3 vector2 = smethod_2(matrix4x4_0.MultiplyVector(Vector3.up));
		Vector3 vector3 = smethod_2(matrix4x4_0.MultiplyVector(Vector3.forward));
		Vector3 vector4 = matrix4x4_0.MultiplyPoint(bounds_0.center);
		Vector3 vector5 = vector * bounds_0.size.x + vector2 * bounds_0.size.y + vector3 * bounds_0.size.z;
		return new Bounds(vector4, vector5);
	}

	private Bounds EfTltsxxExPEoMkBgobW488owF3Kq0iLCR6io70rCzCT(List<NavMeshBuildSource> list_0)
	{
		Matrix4x4 inverse = Matrix4x4.TRS(smethod_6(smethod_5((Component)this)), smethod_7(smethod_5((Component)this)), Vector3.one).inverse;
		Bounds result = default(Bounds);
		foreach (NavMeshBuildSource item in list_0)
		{
			switch (item.shape)
			{
			case NavMeshBuildSourceShape.Mesh:
			{
				Mesh mesh = item.sourceObject as Mesh;
				result.Encapsulate(smethod_3(inverse * item.transform, mesh.bounds));
				break;
			}
			case NavMeshBuildSourceShape.Terrain:
			{
				TerrainData terrainData = item.sourceObject as TerrainData;
				result.Encapsulate(smethod_3(inverse * item.transform, new Bounds(0.5f * terrainData.size, terrainData.size)));
				break;
			}
			case NavMeshBuildSourceShape.Box:
			case NavMeshBuildSourceShape.Sphere:
			case NavMeshBuildSourceShape.Capsule:
			case NavMeshBuildSourceShape.ModifierBox:
				result.Encapsulate(smethod_3(inverse * item.transform, new Bounds(Vector3.zero, item.size)));
				break;
			}
		}
		result.Expand(0.1f);
		return result;
	}

	private bool gmVXR5_mnri_0024cyl2lj6oBd6Ki1m3AfndbPA8eQwMnYgA()
	{
		if (UVmPSNm_GFRc2y3GsJGyO14 != smethod_6(smethod_5((Component)this)))
		{
			return true;
		}
		if (!(quaternion_0 != smethod_7(smethod_5((Component)this))))
		{
			return false;
		}
		return true;
	}

	private void lGMRk2OLu4BMJe8wO1U_KS5taEyUPqt2ccNKLrjQnOek()
	{
		if (gmVXR5_mnri_0024cyl2lj6oBd6Ki1m3AfndbPA8eQwMnYgA())
		{
			RemoveData();
			AddData();
		}
	}

	internal static NavMeshBuildSettings smethod_4(int int_1)
	{
		return NavMesh.GetSettingsByID(int_1);
	}

	internal static Transform smethod_5(Component component_0)
	{
		return component_0.transform;
	}

	internal static Vector3 smethod_6(Transform transform_0)
	{
		return transform_0.position;
	}

	internal static Quaternion smethod_7(Transform transform_0)
	{
		return transform_0.rotation;
	}

	internal static NavMeshData smethod_8(NavMeshBuildSettings navMeshBuildSettings_0, List<NavMeshBuildSource> list_0, Bounds bounds_0, Vector3 vector3_0, Quaternion quaternion_1)
	{
		return NavMeshBuilder.BuildNavMeshData(navMeshBuildSettings_0, list_0, bounds_0, vector3_0, quaternion_1);
	}

	internal static bool smethod_9(Object object_0, Object object_1)
	{
		return object_0 != object_1;
	}

	internal static GameObject smethod_10(Component component_0)
	{
		return component_0.gameObject;
	}

	internal static string smethod_11(Object object_0)
	{
		return object_0.name;
	}

	internal static void smethod_12(Object object_0, string string_0)
	{
		object_0.name = string_0;
	}

	internal static bool smethod_13(Behaviour behaviour_0)
	{
		return behaviour_0.isActiveAndEnabled;
	}

	internal static AsyncOperation smethod_14(NavMeshData navMeshData_0, NavMeshBuildSettings navMeshBuildSettings_0, List<NavMeshBuildSource> list_0, Bounds bounds_0)
	{
		return NavMeshBuilder.UpdateNavMeshDataAsync(navMeshData_0, navMeshBuildSettings_0, list_0, bounds_0);
	}

	internal static Delegate smethod_15(Delegate delegate_0, Delegate delegate_1)
	{
		return Delegate.Combine(delegate_0, delegate_1);
	}

	internal static Delegate smethod_16(Delegate delegate_0, Delegate delegate_1)
	{
		return Delegate.Remove(delegate_0, delegate_1);
	}

	internal static GameObject smethod_17(Component component_0)
	{
		return component_0.gameObject;
	}

	internal static int smethod_18(GameObject gameObject_0)
	{
		return gameObject_0.layer;
	}

	internal static Transform smethod_19(Component component_0)
	{
		return component_0.transform;
	}

	internal static Vector3 smethod_20(Transform transform_0, Vector3 vector3_0)
	{
		return transform_0.TransformPoint(vector3_0);
	}

	internal static Vector3 smethod_21(Transform transform_0)
	{
		return transform_0.lossyScale;
	}
}
