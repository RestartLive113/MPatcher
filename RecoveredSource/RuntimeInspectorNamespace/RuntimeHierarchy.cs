using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace RuntimeInspectorNamespace;

public class RuntimeHierarchy : SkinnedWindow, IListViewAdapter
{
	public delegate void SelectionChangedDelegate(Transform selection);

	public delegate void DoubleClickDelegate(Transform selection);

	public delegate bool GameObjectFilterDelegate(Transform transform);

	[SerializeField]
	private float CrZlWqocnlRmRcQpOF5yoWHxk5tfM4JaQJpQqoqHyavL;

	[SerializeField]
	private float ZZWewlVRRkZuwpb5CBVUi8FuSaxaUi023H7cbwaQfnXD = 10f;

	[SerializeField]
	private float PpITHGbeF6QwuE8hsZo2juOc1qHO9lbqineVif2TNs4j = 5f;

	private float Ukbt7tfJz2rHToDLhoDZKBQNDKEzoMVBfvcUDQ8tJMdp = -1f;

	private float JrKny_0024rdYgUjgSWdykHg62GWNZji9OTWfBbdfQZJm6dG = -1f;

	private float MYLEBDpVUxmvFEp7xtrLc4ONdUD_0024ktFlZGSBbrKhvKAe = -1f;

	[SerializeField]
	private bool dhFH_00246tgykZD3d4DdzpoVN1GXPWf0xrzQUU9lirYq20S = true;

	[SerializeField]
	private bool EEMoD1RlAtyRG8DSbLKdoYxiEOLLRD0F9mItQ2RUrbNw = true;

	[SerializeField]
	private string[] aSOspOCts56qI1CA9BIiRuo9KZn4wr_00241SEbDRrSHJpFE;

	[SerializeField]
	private bool _WqRohYzLhqZHQawUsBgScB0rpty3vKAoQUn_0024qTi2Hol = true;

	[SerializeField]
	private float float_0 = 0.4f;

	[SerializeField]
	private bool uH5F3DnLHP8t_vA1hhsq4_hst_0024Dq1CFYZjHiqJ4DcPTZ;

	[SerializeField]
	private float float_1 = 0.5f;

	[SerializeField]
	private bool bool_0;

	private bool WKGFd1ZzVV26DbZw6zrxqdviIQBeo5MVpLccxbtD59Vy;

	[SerializeField]
	private RuntimeInspector FEsTTtcGwZvBLUtuoP3TGZL_xVyKux7R6Xan4neCmuxd;

	[Header("Internal Variables")]
	[SerializeField]
	private ScrollRect UizBv3bMRdfbyNqELufNvRs;

	[SerializeField]
	private RectTransform FQBgn89DjhMudA260zu9JIg;

	[SerializeField]
	private RecycledListView recycledListView_0;

	[SerializeField]
	private Image slJt0vtJTZ_pZ4HFn1Pm0w0;

	[SerializeField]
	private Image image_0;

	[SerializeField]
	private Image image_1;

	[SerializeField]
	private InputField PM1Dd_WTl_47r29k51M6KC2bQR2r61GvlKxq0n7_00240dhP;

	[SerializeField]
	private Image lQNg6_0024CRRZVdaAmgLUot_50;

	[SerializeField]
	private Image image_2;

	[SerializeField]
	private LayoutElement lx_E2ZqtJppp66tA0konjcrMG0uXgwnbrF_0024KFD_bOJDM;

	[SerializeField]
	private Image HiS9QpvaaHsuJVY6NuZ059De4FgSh_cELCT8zWCc_mx7;

	[SerializeField]
	private Text text_0;

	[SerializeField]
	private HierarchyDragDropListener q_NvO5_j_0024P_0024wCcaFlqpTkEEGk8FQSAPXMcFVOYfH6G_0024Q;

	[SerializeField]
	private HierarchyField Ze0nQhxYsIP6XusbhzlukXc;

	[SerializeField]
	private Sprite Bb0nwe2TdJ1vyKhdhFj3uTMp1eTB8BW8AVR_NxiVlJY5;

	[SerializeField]
	private Sprite sprite_0;

	private static int WqVg3F9vhDpZEfnhXHXn9SOpUaFO5SxT7FGV5YSC99Pz;

	private readonly List<HierarchyField> jmrMHuT_0024qNiP5B6xGnLIs6A = new List<HierarchyField>(32);

	private readonly List<HierarchyDataRoot> Yoby5VsgysI8mVRTb_002480S5I = new List<HierarchyDataRoot>(8);

	private readonly List<HierarchyDataRoot> ho2p6y3DnpjbBn3tNxtq_0024Ic = new List<HierarchyDataRoot>(8);

	private readonly Dictionary<string, HierarchyDataRootPseudoScene> ArqBHYR_0024fKBtM5YrQKc0w9qRQp8icmY8w45HO2AQixjV = new Dictionary<string, HierarchyDataRootPseudoScene>();

	private int YO_Ue8OZmoB2U0qV7_0024jhmmM;

	private bool yWaw2FljDWMaN_0024xfLGyyjxY = true;

	private bool iQtTcHIDcV0R0ooi016KIxTX4Xh5YhyBsDAC_jefbYIH;

	private float Y_sU9o8I1ABIRjVMr9upO_A;

	private HierarchyField _0024b_0024gZhRn_CojH5zHnYgJ_5CKMBO_00246FpwY9jUYHS9ufFA;

	private float udgWvhZdS9P8MvsfS_0024n7455j_0024lnVMkC0OGD_pFUqzRzeWA6xoDTxpMxCv565_dfg5g;

	private PointerEventData fMxROHmzlOgEY6LlZ9XQiT8rso40J2R82zxisf4rESdK;

	private Canvas trJy0poPN23QufiviU1damw;

	private float gQVN3Ibl4b1wT4_RpQ4lS7_U4zsTDpAkOOAbeBELubLj;

	private PointerEventData Kq_0024ea77_0024IaXEO6Z89nJ_0024tOpNqgywg4jcpVzYzACH34rH;

	public SelectionChangedDelegate OnSelectionChanged;

	public DoubleClickDelegate OnItemDoubleClicked;

	private Transform DbiE3JilHDAZFfI_iBWMSfSZvdIeDccTVWef2xdHXi7c;

	private GameObjectFilterDelegate eCXvt90DhOI9r8fTZEZEokmxVroZtnwZicZgoZF6C0FE;

	public float RefreshInterval
	{
		get
		{
			return CrZlWqocnlRmRcQpOF5yoWHxk5tfM4JaQJpQqoqHyavL;
		}
		set
		{
			CrZlWqocnlRmRcQpOF5yoWHxk5tfM4JaQJpQqoqHyavL = value;
		}
	}

	public float ObjectNamesRefreshInterval
	{
		get
		{
			return ZZWewlVRRkZuwpb5CBVUi8FuSaxaUi023H7cbwaQfnXD;
		}
		set
		{
			ZZWewlVRRkZuwpb5CBVUi8FuSaxaUi023H7cbwaQfnXD = value;
		}
	}

	public float SearchRefreshInterval
	{
		get
		{
			return PpITHGbeF6QwuE8hsZo2juOc1qHO9lbqineVif2TNs4j;
		}
		set
		{
			PpITHGbeF6QwuE8hsZo2juOc1qHO9lbqineVif2TNs4j = value;
		}
	}

	public bool ExposeUnityScenes
	{
		get
		{
			return dhFH_00246tgykZD3d4DdzpoVN1GXPWf0xrzQUU9lirYq20S;
		}
		set
		{
			if (dhFH_00246tgykZD3d4DdzpoVN1GXPWf0xrzQUU9lirYq20S == value)
			{
				return;
			}
			dhFH_00246tgykZD3d4DdzpoVN1GXPWf0xrzQUU9lirYq20S = value;
			for (int i = 0; i < smethod_4(); i++)
			{
				if (value)
				{
					Pmq0aRxp9fUcuOl3_0024LDARC0(smethod_3(i), LoadSceneMode.Single);
				}
				else
				{
					S59og2oEEyXIKdrDPWGm1xA(smethod_3(i));
				}
			}
		}
	}

	public bool ExposeDontDestroyOnLoadScene
	{
		get
		{
			return EEMoD1RlAtyRG8DSbLKdoYxiEOLLRD0F9mItQ2RUrbNw;
		}
		set
		{
			if (EEMoD1RlAtyRG8DSbLKdoYxiEOLLRD0F9mItQ2RUrbNw != value)
			{
				EEMoD1RlAtyRG8DSbLKdoYxiEOLLRD0F9mItQ2RUrbNw = value;
				if (!value)
				{
					S59og2oEEyXIKdrDPWGm1xA(MIubVPn35HMxpi9EJhF5TD0H4pZV1KHbpV746Q3Qm2Ru());
				}
				else
				{
					Pmq0aRxp9fUcuOl3_0024LDARC0(MIubVPn35HMxpi9EJhF5TD0H4pZV1KHbpV746Q3Qm2Ru(), LoadSceneMode.Single);
				}
			}
		}
	}

	public bool CreateDraggedReferenceOnHold
	{
		get
		{
			return _WqRohYzLhqZHQawUsBgScB0rpty3vKAoQUn_0024qTi2Hol;
		}
		set
		{
			_WqRohYzLhqZHQawUsBgScB0rpty3vKAoQUn_0024qTi2Hol = value;
		}
	}

	public float DraggedReferenceHoldTime
	{
		get
		{
			return float_0;
		}
		set
		{
			float_0 = value;
		}
	}

	public bool CanReorganizeItems
	{
		get
		{
			return uH5F3DnLHP8t_vA1hhsq4_hst_0024Dq1CFYZjHiqJ4DcPTZ;
		}
		set
		{
			uH5F3DnLHP8t_vA1hhsq4_hst_0024Dq1CFYZjHiqJ4DcPTZ = value;
		}
	}

	public float DoubleClickThreshold
	{
		get
		{
			return float_1;
		}
		set
		{
			float_1 = value;
		}
	}

	public bool ShowHorizontalScrollbar
	{
		get
		{
			return bool_0;
		}
		set
		{
			if (bool_0 == value)
			{
				return;
			}
			bool_0 = value;
			if (value)
			{
				for (int num = jmrMHuT_0024qNiP5B6xGnLIs6A.Count - 1; num >= 0; num--)
				{
					if (jmrMHuT_0024qNiP5B6xGnLIs6A[num].gameObject.activeSelf)
					{
						jmrMHuT_0024qNiP5B6xGnLIs6A[num].RefreshName();
					}
				}
				iQtTcHIDcV0R0ooi016KIxTX4Xh5YhyBsDAC_jefbYIH = true;
			}
			else
			{
				smethod_5(UizBv3bMRdfbyNqELufNvRs).sizeDelta = new Vector2(0f, smethod_6(smethod_5(UizBv3bMRdfbyNqELufNvRs)).y);
				UizBv3bMRdfbyNqELufNvRs.horizontalNormalizedPosition = 0f;
			}
			UizBv3bMRdfbyNqELufNvRs.horizontal = value;
		}
	}

	public string SearchTerm
	{
		get
		{
			return smethod_7(PM1Dd_WTl_47r29k51M6KC2bQR2r61GvlKxq0n7_00240dhP);
		}
		set
		{
			smethod_8(PM1Dd_WTl_47r29k51M6KC2bQR2r61GvlKxq0n7_00240dhP, value);
		}
	}

	public bool IsInSearchMode => WKGFd1ZzVV26DbZw6zrxqdviIQBeo5MVpLccxbtD59Vy;

	public RuntimeInspector ConnectedInspector
	{
		get
		{
			return FEsTTtcGwZvBLUtuoP3TGZL_xVyKux7R6Xan4neCmuxd;
		}
		set
		{
			if (smethod_9((UnityEngine.Object)FEsTTtcGwZvBLUtuoP3TGZL_xVyKux7R6Xan4neCmuxd, (UnityEngine.Object)value))
			{
				FEsTTtcGwZvBLUtuoP3TGZL_xVyKux7R6Xan4neCmuxd = value;
				if (smethod_10((UnityEngine.Object)DbiE3JilHDAZFfI_iBWMSfSZvdIeDccTVWef2xdHXi7c))
				{
					FEsTTtcGwZvBLUtuoP3TGZL_xVyKux7R6Xan4neCmuxd.Inspect(smethod_11((Component)DbiE3JilHDAZFfI_iBWMSfSZvdIeDccTVWef2xdHXi7c));
				}
			}
		}
	}

	internal Sprite SceneDrawerBackground => Bb0nwe2TdJ1vyKhdhFj3uTMp1eTB8BW8AVR_NxiVlJY5;

	internal Sprite TransformDrawerBackground => sprite_0;

	internal int ItemCount => YO_Ue8OZmoB2U0qV7_0024jhmmM;

	public Canvas Canvas => trJy0poPN23QufiviU1damw;

	internal float AutoScrollSpeed
	{
		set
		{
			gQVN3Ibl4b1wT4_RpQ4lS7_U4zsTDpAkOOAbeBELubLj = value;
		}
	}

	public Transform CurrentSelection
	{
		get
		{
			return DbiE3JilHDAZFfI_iBWMSfSZvdIeDccTVWef2xdHXi7c;
		}
		private set
		{
			if (!smethod_10((UnityEngine.Object)value))
			{
				value = null;
			}
			if (smethod_9((UnityEngine.Object)DbiE3JilHDAZFfI_iBWMSfSZvdIeDccTVWef2xdHXi7c, (UnityEngine.Object)value))
			{
				DbiE3JilHDAZFfI_iBWMSfSZvdIeDccTVWef2xdHXi7c = value;
				if (OnSelectionChanged != null)
				{
					OnSelectionChanged(DbiE3JilHDAZFfI_iBWMSfSZvdIeDccTVWef2xdHXi7c);
				}
			}
		}
	}

	public GameObjectFilterDelegate GameObjectFilter
	{
		get
		{
			return eCXvt90DhOI9r8fTZEZEokmxVroZtnwZicZgoZF6C0FE;
		}
		set
		{
			eCXvt90DhOI9r8fTZEZEokmxVroZtnwZicZgoZF6C0FE = value;
			for (int i = 0; i < Yoby5VsgysI8mVRTb_002480S5I.Count; i++)
			{
				if (Yoby5VsgysI8mVRTb_002480S5I[i].IsExpanded)
				{
					Yoby5VsgysI8mVRTb_002480S5I[i].IsExpanded = false;
					Yoby5VsgysI8mVRTb_002480S5I[i].IsExpanded = true;
				}
			}
			if (!WKGFd1ZzVV26DbZw6zrxqdviIQBeo5MVpLccxbtD59Vy)
			{
				return;
			}
			for (int j = 0; j < ho2p6y3DnpjbBn3tNxtq_0024Ic.Count; j++)
			{
				if (ho2p6y3DnpjbBn3tNxtq_0024Ic[j].IsExpanded)
				{
					ho2p6y3DnpjbBn3tNxtq_0024Ic[j].IsExpanded = false;
					ho2p6y3DnpjbBn3tNxtq_0024Ic[j].IsExpanded = true;
				}
			}
		}
	}

	int IListViewAdapter.Count => YO_Ue8OZmoB2U0qV7_0024jhmmM;

	float IListViewAdapter.ItemHeight => base.Skin.LineHeight;

	protected override void Awake()
	{
		base.Awake();
		recycledListView_0.SetAdapter(this);
		WqVg3F9vhDpZEfnhXHXn9SOpUaFO5SxT7FGV5YSC99Pz++;
		trJy0poPN23QufiviU1damw = GetComponentInParent<Canvas>();
		Kq_0024ea77_0024IaXEO6Z89nJ_0024tOpNqgywg4jcpVzYzACH34rH = smethod_12((EventSystem)null);
		smethod_13(PM1Dd_WTl_47r29k51M6KC2bQR2r61GvlKxq0n7_00240dhP).AddListener(ef4EvKNuVFaSQnC4AlsjyM2pheZ9BclwVtsvL7vPAGL2);
		OnSelectionChanged = (SelectionChangedDelegate)smethod_14((Delegate)OnSelectionChanged, (Delegate)(SelectionChangedDelegate)delegate(Transform transform_0)
		{
			if (smethod_10((UnityEngine.Object)ConnectedInspector))
			{
				if (!smethod_10((UnityEngine.Object)transform_0))
				{
					ConnectedInspector.StopInspect();
				}
				else
				{
					ConnectedInspector.Inspect(smethod_11((Component)transform_0));
				}
			}
		});
		bool_0 = !bool_0;
		ShowHorizontalScrollbar = !bool_0;
		RuntimeInspectorUtils.IgnoredTransformsInHierarchy.Add(FQBgn89DjhMudA260zu9JIg);
	}

	private void sp_GCK595YHY1vrEPNGiSrQ()
	{
		smethod_15((UnityAction<Scene, LoadSceneMode>)delegate(Scene scene_0, LoadSceneMode loadSceneMode_0)
		{
			if (ExposeUnityScenes && scene_0.IsValid())
			{
				int num2 = 0;
				while (true)
				{
					if (num2 >= Yoby5VsgysI8mVRTb_002480S5I.Count)
					{
						HierarchyDataRootScene hierarchyDataRootScene = new HierarchyDataRootScene(this, scene_0);
						hierarchyDataRootScene.Refresh();
						int index = Yoby5VsgysI8mVRTb_002480S5I.Count - ArqBHYR_0024fKBtM5YrQKc0w9qRQp8icmY8w45HO2AQixjV.Count;
						Yoby5VsgysI8mVRTb_002480S5I.Insert(index, hierarchyDataRootScene);
						ho2p6y3DnpjbBn3tNxtq_0024Ic.Insert(index, new HierarchyDataRootSearch(this, hierarchyDataRootScene));
						yWaw2FljDWMaN_0024xfLGyyjxY = true;
						break;
					}
					if (Yoby5VsgysI8mVRTb_002480S5I[num2] is HierarchyDataRootScene && ((HierarchyDataRootScene)Yoby5VsgysI8mVRTb_002480S5I[num2]).Scene == scene_0)
					{
						break;
					}
					num2++;
				}
			}
		});
		smethod_16((UnityAction<Scene>)S59og2oEEyXIKdrDPWGm1xA);
		if (ExposeUnityScenes)
		{
			for (int num = 0; num < smethod_4(); num++)
			{
				Pmq0aRxp9fUcuOl3_0024LDARC0(smethod_3(num), LoadSceneMode.Single);
			}
		}
		if (ExposeDontDestroyOnLoadScene)
		{
			Pmq0aRxp9fUcuOl3_0024LDARC0(MIubVPn35HMxpi9EJhF5TD0H4pZV1KHbpV746Q3Qm2Ru(), LoadSceneMode.Single);
		}
	}

	private void U2FLoXbtzOTI8u5GsQUvnCQ()
	{
		smethod_17((UnityAction<Scene, LoadSceneMode>)delegate(Scene scene_0, LoadSceneMode loadSceneMode_0)
		{
			if (ExposeUnityScenes && scene_0.IsValid())
			{
				int num = 0;
				while (true)
				{
					if (num >= Yoby5VsgysI8mVRTb_002480S5I.Count)
					{
						HierarchyDataRootScene hierarchyDataRootScene = new HierarchyDataRootScene(this, scene_0);
						hierarchyDataRootScene.Refresh();
						int index = Yoby5VsgysI8mVRTb_002480S5I.Count - ArqBHYR_0024fKBtM5YrQKc0w9qRQp8icmY8w45HO2AQixjV.Count;
						Yoby5VsgysI8mVRTb_002480S5I.Insert(index, hierarchyDataRootScene);
						ho2p6y3DnpjbBn3tNxtq_0024Ic.Insert(index, new HierarchyDataRootSearch(this, hierarchyDataRootScene));
						yWaw2FljDWMaN_0024xfLGyyjxY = true;
						break;
					}
					if (Yoby5VsgysI8mVRTb_002480S5I[num] is HierarchyDataRootScene && ((HierarchyDataRootScene)Yoby5VsgysI8mVRTb_002480S5I[num]).Scene == scene_0)
					{
						break;
					}
					num++;
				}
			}
		});
		smethod_18((UnityAction<Scene>)S59og2oEEyXIKdrDPWGm1xA);
		if (--WqVg3F9vhDpZEfnhXHXn9SOpUaFO5SxT7FGV5YSC99Pz == 0)
		{
			HierarchyData.ClearPool();
		}
		RuntimeInspectorUtils.IgnoredTransformsInHierarchy.Remove(FQBgn89DjhMudA260zu9JIg);
	}

	private void method_0()
	{
		iQtTcHIDcV0R0ooi016KIxTX4Xh5YhyBsDAC_jefbYIH = true;
	}

	private void jcPq5m3c7FL_B4giC_0024d_2cFRkqzkH6HLtBb2RY2ro6qT()
	{
		trJy0poPN23QufiviU1damw = GetComponentInParent<Canvas>();
	}

	protected override void Update()
	{
		base.Update();
		float num = smethod_19();
		if (WKGFd1ZzVV26DbZw6zrxqdviIQBeo5MVpLccxbtD59Vy)
		{
			if (num > MYLEBDpVUxmvFEp7xtrLc4ONdUD_0024ktFlZGSBbrKhvKAe)
			{
				RefreshSearchResults();
			}
		}
		else if (num > Ukbt7tfJz2rHToDLhoDZKBQNDKEzoMVBfvcUDQ8tJMdp)
		{
			Refresh();
		}
		if (yWaw2FljDWMaN_0024xfLGyyjxY)
		{
			Zi_0024YJJ_J80nUIr_0024RAsVNcPI();
		}
		if (smethod_9((UnityEngine.Object)CurrentSelection, (UnityEngine.Object)null) && smethod_20(KeyCode.Delete))
		{
			smethod_21((UnityEngine.Object)smethod_11((Component)CurrentSelection));
		}
		if (num > JrKny_0024rdYgUjgSWdykHg62GWNZji9OTWfBbdfQZJm6dG)
		{
			JrKny_0024rdYgUjgSWdykHg62GWNZji9OTWfBbdfQZJm6dG = num + ZZWewlVRRkZuwpb5CBVUi8FuSaxaUi023H7cbwaQfnXD;
			for (int num2 = Yoby5VsgysI8mVRTb_002480S5I.Count - 1; num2 >= 0; num2--)
			{
				Yoby5VsgysI8mVRTb_002480S5I[num2].ResetCachedNames();
			}
			for (int num3 = ho2p6y3DnpjbBn3tNxtq_0024Ic.Count - 1; num3 >= 0; num3--)
			{
				ho2p6y3DnpjbBn3tNxtq_0024Ic[num3].ResetCachedNames();
			}
			for (int num4 = jmrMHuT_0024qNiP5B6xGnLIs6A.Count - 1; num4 >= 0; num4--)
			{
				if (smethod_22(smethod_11((Component)jmrMHuT_0024qNiP5B6xGnLIs6A[num4])))
				{
					jmrMHuT_0024qNiP5B6xGnLIs6A[num4].RefreshName();
				}
			}
			iQtTcHIDcV0R0ooi016KIxTX4Xh5YhyBsDAC_jefbYIH = true;
		}
		if (bool_0 && iQtTcHIDcV0R0ooi016KIxTX4Xh5YhyBsDAC_jefbYIH)
		{
			iQtTcHIDcV0R0ooi016KIxTX4Xh5YhyBsDAC_jefbYIH = false;
			float num5 = 0f;
			for (int num6 = jmrMHuT_0024qNiP5B6xGnLIs6A.Count - 1; num6 >= 0; num6--)
			{
				if (smethod_22(smethod_11((Component)jmrMHuT_0024qNiP5B6xGnLIs6A[num6])))
				{
					float preferredWidth = jmrMHuT_0024qNiP5B6xGnLIs6A[num6].PreferredWidth;
					if (preferredWidth > num5)
					{
						num5 = preferredWidth;
					}
				}
			}
			float num7 = recycledListView_0.ViewportWidth + smethod_23(UizBv3bMRdfbyNqELufNvRs);
			if (num5 > num7)
			{
				smethod_5(UizBv3bMRdfbyNqELufNvRs).sizeDelta = new Vector2(num5 - num7, smethod_6(smethod_5(UizBv3bMRdfbyNqELufNvRs)).y);
			}
			else
			{
				UizBv3bMRdfbyNqELufNvRs.content.sizeDelta = new Vector2(0f, UizBv3bMRdfbyNqELufNvRs.content.sizeDelta.y);
			}
		}
		if (_WqRohYzLhqZHQawUsBgScB0rpty3vKAoQUn_0024qTi2Hol && (bool)_0024b_0024gZhRn_CojH5zHnYgJ_5CKMBO_00246FpwY9jUYHS9ufFA && num > udgWvhZdS9P8MvsfS_0024n7455j_0024lnVMkC0OGD_pFUqzRzeWA6xoDTxpMxCv565_dfg5g)
		{
			if (_0024b_0024gZhRn_CojH5zHnYgJ_5CKMBO_00246FpwY9jUYHS9ufFA.gameObject.activeSelf && (bool)_0024b_0024gZhRn_CojH5zHnYgJ_5CKMBO_00246FpwY9jUYHS9ufFA.Data.BoundTransform && (bool)RuntimeInspectorUtils.CreateDraggedReferenceItem(_0024b_0024gZhRn_CojH5zHnYgJ_5CKMBO_00246FpwY9jUYHS9ufFA.Data.BoundTransform, fMxROHmzlOgEY6LlZ9XQiT8rso40J2R82zxisf4rESdK, base.Skin, trJy0poPN23QufiviU1damw))
			{
				((IPointerEnterHandler)q_NvO5_j_0024P_0024wCcaFlqpTkEEGk8FQSAPXMcFVOYfH6G_0024Q).OnPointerEnter(fMxROHmzlOgEY6LlZ9XQiT8rso40J2R82zxisf4rESdK);
			}
			_0024b_0024gZhRn_CojH5zHnYgJ_5CKMBO_00246FpwY9jUYHS9ufFA = null;
			fMxROHmzlOgEY6LlZ9XQiT8rso40J2R82zxisf4rESdK = null;
		}
		if (gQVN3Ibl4b1wT4_RpQ4lS7_U4zsTDpAkOOAbeBELubLj != 0f)
		{
			UizBv3bMRdfbyNqELufNvRs.verticalNormalizedPosition = Mathf.Clamp01(UizBv3bMRdfbyNqELufNvRs.verticalNormalizedPosition + gQVN3Ibl4b1wT4_RpQ4lS7_U4zsTDpAkOOAbeBELubLj * Time.unscaledDeltaTime / (float)YO_Ue8OZmoB2U0qV7_0024jhmmM);
		}
	}

	public void Refresh()
	{
		if (WKGFd1ZzVV26DbZw6zrxqdviIQBeo5MVpLccxbtD59Vy)
		{
			return;
		}
		Ukbt7tfJz2rHToDLhoDZKBQNDKEzoMVBfvcUDQ8tJMdp = smethod_19() + CrZlWqocnlRmRcQpOF5yoWHxk5tfM4JaQJpQqoqHyavL;
		bool flag = false;
		for (int i = 0; i < Yoby5VsgysI8mVRTb_002480S5I.Count; i++)
		{
			flag |= Yoby5VsgysI8mVRTb_002480S5I[i].Refresh();
		}
		if (!flag)
		{
			for (int num = jmrMHuT_0024qNiP5B6xGnLIs6A.Count - 1; num >= 0; num--)
			{
				if (smethod_22(smethod_11((Component)jmrMHuT_0024qNiP5B6xGnLIs6A[num])))
				{
					jmrMHuT_0024qNiP5B6xGnLIs6A[num].Refresh();
				}
			}
		}
		else
		{
			yWaw2FljDWMaN_0024xfLGyyjxY = true;
		}
	}

	private void Zi_0024YJJ_J80nUIr_0024RAsVNcPI()
	{
		yWaw2FljDWMaN_0024xfLGyyjxY = false;
		YO_Ue8OZmoB2U0qV7_0024jhmmM = 0;
		if (!WKGFd1ZzVV26DbZw6zrxqdviIQBeo5MVpLccxbtD59Vy)
		{
			for (int num = Yoby5VsgysI8mVRTb_002480S5I.Count - 1; num >= 0; num--)
			{
				YO_Ue8OZmoB2U0qV7_0024jhmmM += Yoby5VsgysI8mVRTb_002480S5I[num].Height;
			}
		}
		else
		{
			for (int num2 = ho2p6y3DnpjbBn3tNxtq_0024Ic.Count - 1; num2 >= 0; num2--)
			{
				YO_Ue8OZmoB2U0qV7_0024jhmmM += ho2p6y3DnpjbBn3tNxtq_0024Ic[num2].Height;
			}
		}
		recycledListView_0.UpdateList(resetContentPosition: false);
		smethod_24(UizBv3bMRdfbyNqELufNvRs, Kq_0024ea77_0024IaXEO6Z89nJ_0024tOpNqgywg4jcpVzYzACH34rH);
	}

	public void SetListViewDirty()
	{
		yWaw2FljDWMaN_0024xfLGyyjxY = true;
	}

	public void RefreshSearchResults()
	{
		if (!WKGFd1ZzVV26DbZw6zrxqdviIQBeo5MVpLccxbtD59Vy)
		{
			return;
		}
		MYLEBDpVUxmvFEp7xtrLc4ONdUD_0024ktFlZGSBbrKhvKAe = smethod_19() + PpITHGbeF6QwuE8hsZo2juOc1qHO9lbqineVif2TNs4j;
		for (int i = 0; i < ho2p6y3DnpjbBn3tNxtq_0024Ic.Count; i++)
		{
			HierarchyDataRootSearch hierarchyDataRootSearch = (HierarchyDataRootSearch)ho2p6y3DnpjbBn3tNxtq_0024Ic[i];
			bool canExpand = hierarchyDataRootSearch.CanExpand;
			hierarchyDataRootSearch.Refresh();
			if (hierarchyDataRootSearch.CanExpand && !canExpand)
			{
				hierarchyDataRootSearch.IsExpanded = true;
			}
			yWaw2FljDWMaN_0024xfLGyyjxY = true;
		}
	}

	public void RefreshNameOf(Transform target)
	{
		if (!smethod_10((UnityEngine.Object)target))
		{
			return;
		}
		Scene scene = smethod_25(smethod_11((Component)target));
		for (int num = Yoby5VsgysI8mVRTb_002480S5I.Count - 1; num >= 0; num--)
		{
			HierarchyDataRoot hierarchyDataRoot = Yoby5VsgysI8mVRTb_002480S5I[num];
			if (hierarchyDataRoot is HierarchyDataRootPseudoScene || ((HierarchyDataRootScene)hierarchyDataRoot).Scene == scene)
			{
				Yoby5VsgysI8mVRTb_002480S5I[num].RefreshNameOf(target);
			}
		}
		if (WKGFd1ZzVV26DbZw6zrxqdviIQBeo5MVpLccxbtD59Vy)
		{
			RefreshSearchResults();
			for (int num2 = ho2p6y3DnpjbBn3tNxtq_0024Ic.Count - 1; num2 >= 0; num2--)
			{
				ho2p6y3DnpjbBn3tNxtq_0024Ic[num2].RefreshNameOf(target);
			}
		}
		for (int num3 = jmrMHuT_0024qNiP5B6xGnLIs6A.Count - 1; num3 >= 0; num3--)
		{
			if (smethod_22(smethod_11((Component)jmrMHuT_0024qNiP5B6xGnLIs6A[num3])) && smethod_26((UnityEngine.Object)jmrMHuT_0024qNiP5B6xGnLIs6A[num3].Data.BoundTransform, (UnityEngine.Object)target))
			{
				jmrMHuT_0024qNiP5B6xGnLIs6A[num3].RefreshName();
			}
		}
		iQtTcHIDcV0R0ooi016KIxTX4Xh5YhyBsDAC_jefbYIH = true;
	}

	protected override void RefreshSkin()
	{
		smethod_27((Graphic)slJt0vtJTZ_pZ4HFn1Pm0w0, base.Skin.BackgroundColor);
		smethod_27((Graphic)image_0, base.Skin.ScrollbarColor);
		smethod_27((Graphic)image_1, base.Skin.ScrollbarColor);
		smethod_28(PM1Dd_WTl_47r29k51M6KC2bQR2r61GvlKxq0n7_00240dhP).SetSkinInputFieldText(base.Skin);
		smethod_27((Graphic)image_2, base.Skin.InputFieldNormalBackgroundColor.Tint(0.08f));
		smethod_27((Graphic)lQNg6_0024CRRZVdaAmgLUot_50, base.Skin.ButtonTextColor);
		lx_E2ZqtJppp66tA0konjcrMG0uXgwnbrF_0024KFD_bOJDM.SetHeight(base.Skin.LineHeight);
		smethod_27((Graphic)HiS9QpvaaHsuJVY6NuZ059De4FgSh_cELCT8zWCc_mx7, base.Skin.BackgroundColor.Tint(0.1f));
		text_0.SetSkinButtonText(base.Skin);
		Text text = smethod_29(PM1Dd_WTl_47r29k51M6KC2bQR2r61GvlKxq0n7_00240dhP) as Text;
		if (smethod_9((UnityEngine.Object)text, (UnityEngine.Object)null))
		{
			float a = smethod_30((Graphic)text).a;
			text.SetSkinInputFieldText(base.Skin);
			Color color_ = smethod_30((Graphic)text);
			color_.a = a;
			smethod_27((Graphic)text, color_);
		}
		smethod_31(FQBgn89DjhMudA260zu9JIg);
		recycledListView_0.ResetList();
	}

	void IListViewAdapter.SetItemContent(RecycledListItem item)
	{
		if (yWaw2FljDWMaN_0024xfLGyyjxY)
		{
			Zi_0024YJJ_J80nUIr_0024RAsVNcPI();
		}
		HierarchyField hierarchyField = (HierarchyField)item;
		HierarchyData hierarchyData = method_1(hierarchyField.Position);
		if (hierarchyData != null)
		{
			hierarchyField.Skin = base.Skin;
			hierarchyField.SetContent(hierarchyData);
			hierarchyField.IsSelected = smethod_10((UnityEngine.Object)DbiE3JilHDAZFfI_iBWMSfSZvdIeDccTVWef2xdHXi7c) && smethod_26((UnityEngine.Object)DbiE3JilHDAZFfI_iBWMSfSZvdIeDccTVWef2xdHXi7c, (UnityEngine.Object)hierarchyData.BoundTransform);
			hierarchyField.Refresh();
			iQtTcHIDcV0R0ooi016KIxTX4Xh5YhyBsDAC_jefbYIH = true;
		}
	}

	private void bA2nXE0dVfEZ2p9wht4oTdIK3ZyuFmMmFr057wiGnKkdNrEe5ifiWHj7P3KTVfuMy93rGp7WayZ3QRBZSjIkA4g(RecycledListItem item)
	{
		HierarchyField hierarchyField = (HierarchyField)item;
		if (!smethod_10((UnityEngine.Object)hierarchyField))
		{
			if (!smethod_10((UnityEngine.Object)DbiE3JilHDAZFfI_iBWMSfSZvdIeDccTVWef2xdHXi7c))
			{
				return;
			}
			for (int num = jmrMHuT_0024qNiP5B6xGnLIs6A.Count - 1; num >= 0; num--)
			{
				if (smethod_22(smethod_11((Component)jmrMHuT_0024qNiP5B6xGnLIs6A[num])) && smethod_26((UnityEngine.Object)jmrMHuT_0024qNiP5B6xGnLIs6A[num].Data.BoundTransform, (UnityEngine.Object)DbiE3JilHDAZFfI_iBWMSfSZvdIeDccTVWef2xdHXi7c))
				{
					jmrMHuT_0024qNiP5B6xGnLIs6A[num].IsSelected = false;
				}
			}
			CurrentSelection = null;
		}
		else if (!smethod_26((UnityEngine.Object)DbiE3JilHDAZFfI_iBWMSfSZvdIeDccTVWef2xdHXi7c, (UnityEngine.Object)hierarchyField.Data.BoundTransform))
		{
			Transform transform = hierarchyField.Data.BoundTransform;
			for (int num2 = jmrMHuT_0024qNiP5B6xGnLIs6A.Count - 1; num2 >= 0; num2--)
			{
				if (smethod_22(smethod_11((Component)jmrMHuT_0024qNiP5B6xGnLIs6A[num2])))
				{
					Transform boundTransform = jmrMHuT_0024qNiP5B6xGnLIs6A[num2].Data.BoundTransform;
					if (smethod_26((UnityEngine.Object)boundTransform, (UnityEngine.Object)DbiE3JilHDAZFfI_iBWMSfSZvdIeDccTVWef2xdHXi7c))
					{
						jmrMHuT_0024qNiP5B6xGnLIs6A[num2].IsSelected = false;
					}
					else if (smethod_26((UnityEngine.Object)boundTransform, (UnityEngine.Object)transform) && smethod_10((UnityEngine.Object)transform))
					{
						jmrMHuT_0024qNiP5B6xGnLIs6A[num2].IsSelected = true;
					}
				}
			}
			Y_sU9o8I1ABIRjVMr9upO_A = smethod_19();
			CurrentSelection = transform;
			if (WKGFd1ZzVV26DbZw6zrxqdviIQBeo5MVpLccxbtD59Vy && smethod_10((UnityEngine.Object)transform))
			{
				StringBuilder orPIIOQlqp5UvEq_w10vC = RuntimeInspectorUtils.OrPIIOQlqp5UvEq_w10vC58;
				smethod_32(orPIIOQlqp5UvEq_w10vC, 0);
				smethod_33(orPIIOQlqp5UvEq_w10vC, global::_003CModule_003E.smethod_27<string>(1620641183u));
				while (smethod_10((UnityEngine.Object)transform))
				{
					smethod_33(smethod_34(orPIIOQlqp5UvEq_w10vC, global::_003CModule_003E.smethod_28<string>(3730983281u)), smethod_35((UnityEngine.Object)transform));
					transform = smethod_36(transform);
				}
				smethod_38(text_0, smethod_37((object)smethod_34(smethod_34(orPIIOQlqp5UvEq_w10vC, global::_003CModule_003E.smethod_25<string>(1515298542u)), hierarchyField.Data.Root.Name)));
				smethod_39(smethod_11((Component)HiS9QpvaaHsuJVY6NuZ059De4FgSh_cELCT8zWCc_mx7), bool_1: true);
			}
		}
		else
		{
			if (OnItemDoubleClicked == null)
			{
				return;
			}
			if (smethod_19() - Y_sU9o8I1ABIRjVMr9upO_A <= float_1)
			{
				Y_sU9o8I1ABIRjVMr9upO_A = 0f;
				if (smethod_10((UnityEngine.Object)DbiE3JilHDAZFfI_iBWMSfSZvdIeDccTVWef2xdHXi7c))
				{
					OnItemDoubleClicked(DbiE3JilHDAZFfI_iBWMSfSZvdIeDccTVWef2xdHXi7c);
				}
			}
			else
			{
				Y_sU9o8I1ABIRjVMr9upO_A = smethod_19();
			}
		}
	}

	void IListViewAdapter.OnItemClicked(RecycledListItem item)
	{
		//ILSpy generated this explicit interface implementation from .override directive in bA2nXE0dVfEZ2p9wht4oTdIK3ZyuFmMmFr057wiGnKkdNrEe5ifiWHj7P3KTVfuMy93rGp7WayZ3QRBZSjIkA4g
		this.bA2nXE0dVfEZ2p9wht4oTdIK3ZyuFmMmFr057wiGnKkdNrEe5ifiWHj7P3KTVfuMy93rGp7WayZ3QRBZSjIkA4g(item);
	}

	internal HierarchyData method_1(int int_0)
	{
		List<HierarchyDataRoot> list = ((!WKGFd1ZzVV26DbZw6zrxqdviIQBeo5MVpLccxbtD59Vy) ? Yoby5VsgysI8mVRTb_002480S5I : ho2p6y3DnpjbBn3tNxtq_0024Ic);
		int num = 0;
		while (true)
		{
			if (num < list.Count)
			{
				if (list[num].Depth >= 0)
				{
					if (int_0 < list[num].Height)
					{
						break;
					}
					int_0 -= list[num].Height;
				}
				num++;
				continue;
			}
			return null;
		}
		if (int_0 > 0)
		{
			return list[num].FindDataAtIndex(int_0 - 1);
		}
		return list[num];
	}

	public void OnDrawerPointerEvent(HierarchyField drawer, PointerEventData eventData, bool isPointerDown)
	{
		if (!isPointerDown)
		{
			_0024b_0024gZhRn_CojH5zHnYgJ_5CKMBO_00246FpwY9jUYHS9ufFA = null;
			fMxROHmzlOgEY6LlZ9XQiT8rso40J2R82zxisf4rESdK = null;
		}
		else if (_WqRohYzLhqZHQawUsBgScB0rpty3vKAoQUn_0024qTi2Hol)
		{
			_0024b_0024gZhRn_CojH5zHnYgJ_5CKMBO_00246FpwY9jUYHS9ufFA = drawer;
			fMxROHmzlOgEY6LlZ9XQiT8rso40J2R82zxisf4rESdK = eventData;
			udgWvhZdS9P8MvsfS_0024n7455j_0024lnVMkC0OGD_pFUqzRzeWA6xoDTxpMxCv565_dfg5g = smethod_19() + float_0;
		}
	}

	public bool Select(Transform selection, bool forceSelection = false)
	{
		if (!smethod_10((UnityEngine.Object)selection))
		{
			Deselect();
			return true;
		}
		if (forceSelection || !smethod_26((UnityEngine.Object)selection, (UnityEngine.Object)DbiE3JilHDAZFfI_iBWMSfSZvdIeDccTVWef2xdHXi7c))
		{
			CurrentSelection = selection;
			Refresh();
			Scene scene = smethod_25(smethod_11((Component)selection));
			int num = 0;
			HierarchyDataTransform hierarchyDataTransform;
			while (true)
			{
				if (num < Yoby5VsgysI8mVRTb_002480S5I.Count)
				{
					HierarchyDataRoot hierarchyDataRoot = Yoby5VsgysI8mVRTb_002480S5I[num];
					if (hierarchyDataRoot is HierarchyDataRootPseudoScene || ((HierarchyDataRootScene)hierarchyDataRoot).Scene == scene)
					{
						hierarchyDataTransform = Yoby5VsgysI8mVRTb_002480S5I[num].FindTransform(selection);
						if (hierarchyDataTransform != null)
						{
							break;
						}
					}
					num++;
					continue;
				}
				return false;
			}
			Zi_0024YJJ_J80nUIr_0024RAsVNcPI();
			int num2 = hierarchyDataTransform.AbsoluteIndex;
			for (int i = 0; i < num; i++)
			{
				num2 += Yoby5VsgysI8mVRTb_002480S5I[num].Height;
			}
			smethod_31(FQBgn89DjhMudA260zu9JIg);
			smethod_40(UizBv3bMRdfbyNqELufNvRs, Mathf.Clamp01(1f - (float)num2 / (float)YO_Ue8OZmoB2U0qV7_0024jhmmM));
			return true;
		}
		return true;
	}

	public void Deselect()
	{
		((IListViewAdapter)this).OnItemClicked((RecycledListItem)null);
	}

	private void ef4EvKNuVFaSQnC4AlsjyM2pheZ9BclwVtsvL7vPAGL2(string string_0)
	{
		if (string_0 != null)
		{
			string_0 = smethod_41(string_0);
		}
		if (!smethod_42(string_0))
		{
			if (WKGFd1ZzVV26DbZw6zrxqdviIQBeo5MVpLccxbtD59Vy)
			{
				RefreshSearchResults();
				return;
			}
			smethod_40(UizBv3bMRdfbyNqELufNvRs, 1f);
			MYLEBDpVUxmvFEp7xtrLc4ONdUD_0024ktFlZGSBbrKhvKAe = smethod_19() + PpITHGbeF6QwuE8hsZo2juOc1qHO9lbqineVif2TNs4j;
			yWaw2FljDWMaN_0024xfLGyyjxY = true;
			WKGFd1ZzVV26DbZw6zrxqdviIQBeo5MVpLccxbtD59Vy = true;
			RefreshSearchResults();
			for (int i = 0; i < ho2p6y3DnpjbBn3tNxtq_0024Ic.Count; i++)
			{
				ho2p6y3DnpjbBn3tNxtq_0024Ic[i].IsExpanded = true;
			}
		}
		else if (WKGFd1ZzVV26DbZw6zrxqdviIQBeo5MVpLccxbtD59Vy)
		{
			for (int j = 0; j < ho2p6y3DnpjbBn3tNxtq_0024Ic.Count; j++)
			{
				ho2p6y3DnpjbBn3tNxtq_0024Ic[j].IsExpanded = false;
			}
			smethod_40(UizBv3bMRdfbyNqELufNvRs, 1f);
			smethod_39(smethod_11((Component)HiS9QpvaaHsuJVY6NuZ059De4FgSh_cELCT8zWCc_mx7), bool_1: false);
			yWaw2FljDWMaN_0024xfLGyyjxY = true;
			WKGFd1ZzVV26DbZw6zrxqdviIQBeo5MVpLccxbtD59Vy = false;
			if (smethod_10((UnityEngine.Object)DbiE3JilHDAZFfI_iBWMSfSZvdIeDccTVWef2xdHXi7c))
			{
				Select(DbiE3JilHDAZFfI_iBWMSfSZvdIeDccTVWef2xdHXi7c, forceSelection: true);
			}
		}
	}

	private void Pmq0aRxp9fUcuOl3_0024LDARC0(Scene scene_0, LoadSceneMode loadSceneMode_0)
	{
		if (!ExposeUnityScenes || !scene_0.IsValid())
		{
			return;
		}
		int num = 0;
		while (true)
		{
			if (num < Yoby5VsgysI8mVRTb_002480S5I.Count)
			{
				if (!(Yoby5VsgysI8mVRTb_002480S5I[num] is HierarchyDataRootScene) || !(((HierarchyDataRootScene)Yoby5VsgysI8mVRTb_002480S5I[num]).Scene == scene_0))
				{
					num++;
					continue;
				}
				break;
			}
			HierarchyDataRootScene hierarchyDataRootScene = new HierarchyDataRootScene(this, scene_0);
			hierarchyDataRootScene.Refresh();
			int index = Yoby5VsgysI8mVRTb_002480S5I.Count - ArqBHYR_0024fKBtM5YrQKc0w9qRQp8icmY8w45HO2AQixjV.Count;
			Yoby5VsgysI8mVRTb_002480S5I.Insert(index, hierarchyDataRootScene);
			ho2p6y3DnpjbBn3tNxtq_0024Ic.Insert(index, new HierarchyDataRootSearch(this, hierarchyDataRootScene));
			yWaw2FljDWMaN_0024xfLGyyjxY = true;
			break;
		}
	}

	private void S59og2oEEyXIKdrDPWGm1xA(Scene scene_0)
	{
		int num = 0;
		while (true)
		{
			if (num < Yoby5VsgysI8mVRTb_002480S5I.Count)
			{
				if (Yoby5VsgysI8mVRTb_002480S5I[num] is HierarchyDataRootScene && ((HierarchyDataRootScene)Yoby5VsgysI8mVRTb_002480S5I[num]).Scene == scene_0)
				{
					break;
				}
				num++;
				continue;
			}
			return;
		}
		Yoby5VsgysI8mVRTb_002480S5I[num].IsExpanded = false;
		Yoby5VsgysI8mVRTb_002480S5I.RemoveAt(num);
		ho2p6y3DnpjbBn3tNxtq_0024Ic[num].IsExpanded = false;
		ho2p6y3DnpjbBn3tNxtq_0024Ic.RemoveAt(num);
		yWaw2FljDWMaN_0024xfLGyyjxY = true;
	}

	private Scene MIubVPn35HMxpi9EJhF5TD0H4pZV1KHbpV746Q3Qm2Ru()
	{
		GameObject gameObject = null;
		try
		{
			gameObject = smethod_43();
			smethod_44((UnityEngine.Object)gameObject);
			Scene result = smethod_25(gameObject);
			smethod_45((UnityEngine.Object)gameObject);
			gameObject = null;
			return result;
		}
		catch (Exception exception_)
		{
			smethod_46(exception_);
			return default(Scene);
		}
		finally
		{
			if (smethod_9((UnityEngine.Object)gameObject, (UnityEngine.Object)null))
			{
				smethod_45((UnityEngine.Object)gameObject);
			}
		}
	}

	public void AddToPseudoScene(string scene, Transform transform)
	{
		PEDBWOsaz1blUgn_0024kwPvzME(scene, bool_1: true).AddChild(transform);
	}

	public void AddToPseudoScene(string scene, IEnumerable<Transform> transforms)
	{
		HierarchyDataRootPseudoScene hierarchyDataRootPseudoScene = PEDBWOsaz1blUgn_0024kwPvzME(scene, bool_1: true);
		IEnumerator<Transform> enumerator = transforms.GetEnumerator();
		try
		{
			while (smethod_47((IEnumerator)enumerator))
			{
				Transform current = enumerator.Current;
				hierarchyDataRootPseudoScene.AddChild(current);
			}
		}
		finally
		{
			if (enumerator != null)
			{
				smethod_48((IDisposable)enumerator);
			}
		}
	}

	public void RemoveFromPseudoScene(string scene, Transform transform, bool deleteSceneIfEmpty)
	{
		HierarchyDataRootPseudoScene hierarchyDataRootPseudoScene = PEDBWOsaz1blUgn_0024kwPvzME(scene, bool_1: false);
		if (hierarchyDataRootPseudoScene != null)
		{
			hierarchyDataRootPseudoScene.RemoveChild(transform);
			if (deleteSceneIfEmpty && hierarchyDataRootPseudoScene.ChildCount == 0)
			{
				DeletePseudoScene(scene);
			}
		}
	}

	public void RemoveFromPseudoScene(string scene, IEnumerable<Transform> transforms, bool deleteSceneIfEmpty)
	{
		HierarchyDataRootPseudoScene hierarchyDataRootPseudoScene = PEDBWOsaz1blUgn_0024kwPvzME(scene, bool_1: false);
		if (hierarchyDataRootPseudoScene == null)
		{
			return;
		}
		IEnumerator<Transform> enumerator = transforms.GetEnumerator();
		try
		{
			while (smethod_47((IEnumerator)enumerator))
			{
				Transform current = enumerator.Current;
				hierarchyDataRootPseudoScene.RemoveChild(current);
			}
		}
		finally
		{
			if (enumerator != null)
			{
				smethod_48((IDisposable)enumerator);
			}
		}
		if (deleteSceneIfEmpty && hierarchyDataRootPseudoScene.ChildCount == 0)
		{
			DeletePseudoScene(scene);
		}
	}

	private HierarchyDataRootPseudoScene PEDBWOsaz1blUgn_0024kwPvzME(string string_0, bool bool_1)
	{
		if (ArqBHYR_0024fKBtM5YrQKc0w9qRQp8icmY8w45HO2AQixjV.TryGetValue(string_0, out var value))
		{
			return value;
		}
		if (!bool_1)
		{
			return null;
		}
		return H4LNvuY4y_002475vfC182f4mnvKJ_0024nh2wQDiUbpnYGIelAN(string_0);
	}

	public void CreatePseudoScene(string scene)
	{
		if (!ArqBHYR_0024fKBtM5YrQKc0w9qRQp8icmY8w45HO2AQixjV.ContainsKey(scene))
		{
			H4LNvuY4y_002475vfC182f4mnvKJ_0024nh2wQDiUbpnYGIelAN(scene);
		}
	}

	private HierarchyDataRootPseudoScene H4LNvuY4y_002475vfC182f4mnvKJ_0024nh2wQDiUbpnYGIelAN(string string_0)
	{
		int num = 0;
		if (aSOspOCts56qI1CA9BIiRuo9KZn4wr_00241SEbDRrSHJpFE.Length == 0)
		{
			num = ArqBHYR_0024fKBtM5YrQKc0w9qRQp8icmY8w45HO2AQixjV.Count;
		}
		else
		{
			for (int i = 0; i < aSOspOCts56qI1CA9BIiRuo9KZn4wr_00241SEbDRrSHJpFE.Length && !smethod_49(aSOspOCts56qI1CA9BIiRuo9KZn4wr_00241SEbDRrSHJpFE[i], string_0); i++)
			{
				if (ArqBHYR_0024fKBtM5YrQKc0w9qRQp8icmY8w45HO2AQixjV.ContainsKey(aSOspOCts56qI1CA9BIiRuo9KZn4wr_00241SEbDRrSHJpFE[i]))
				{
					num++;
				}
			}
		}
		HierarchyDataRootPseudoScene hierarchyDataRootPseudoScene = new HierarchyDataRootPseudoScene(this, string_0);
		num += Yoby5VsgysI8mVRTb_002480S5I.Count - ArqBHYR_0024fKBtM5YrQKc0w9qRQp8icmY8w45HO2AQixjV.Count;
		Yoby5VsgysI8mVRTb_002480S5I.Insert(num, hierarchyDataRootPseudoScene);
		ho2p6y3DnpjbBn3tNxtq_0024Ic.Insert(num, new HierarchyDataRootSearch(this, hierarchyDataRootPseudoScene));
		ArqBHYR_0024fKBtM5YrQKc0w9qRQp8icmY8w45HO2AQixjV[string_0] = hierarchyDataRootPseudoScene;
		yWaw2FljDWMaN_0024xfLGyyjxY = true;
		return hierarchyDataRootPseudoScene;
	}

	public void DeleteAllPseudoScenes()
	{
		for (int num = Yoby5VsgysI8mVRTb_002480S5I.Count - 1; num >= 0; num--)
		{
			if (Yoby5VsgysI8mVRTb_002480S5I[num] is HierarchyDataRootPseudoScene)
			{
				Yoby5VsgysI8mVRTb_002480S5I[num].IsExpanded = false;
				Yoby5VsgysI8mVRTb_002480S5I.RemoveAt(num);
				ho2p6y3DnpjbBn3tNxtq_0024Ic[num].IsExpanded = false;
				ho2p6y3DnpjbBn3tNxtq_0024Ic.RemoveAt(num);
			}
		}
		ArqBHYR_0024fKBtM5YrQKc0w9qRQp8icmY8w45HO2AQixjV.Clear();
		yWaw2FljDWMaN_0024xfLGyyjxY = true;
	}

	public void DeletePseudoScene(string scene)
	{
		for (int i = 0; i < Yoby5VsgysI8mVRTb_002480S5I.Count; i++)
		{
			if (Yoby5VsgysI8mVRTb_002480S5I[i] is HierarchyDataRootPseudoScene hierarchyDataRootPseudoScene && smethod_49(hierarchyDataRootPseudoScene.Name, scene))
			{
				ArqBHYR_0024fKBtM5YrQKc0w9qRQp8icmY8w45HO2AQixjV.Remove(hierarchyDataRootPseudoScene.Name);
				Yoby5VsgysI8mVRTb_002480S5I[i].IsExpanded = false;
				Yoby5VsgysI8mVRTb_002480S5I.RemoveAt(i);
				ho2p6y3DnpjbBn3tNxtq_0024Ic[i].IsExpanded = false;
				ho2p6y3DnpjbBn3tNxtq_0024Ic.RemoveAt(i);
				yWaw2FljDWMaN_0024xfLGyyjxY = true;
				break;
			}
		}
	}

	private RecycledListItem ODVL6hEHtxJ1hbG12loRGsajTIjh_sbobfpZlHh2C0gK_LI_ykQhXvjJVdY_mBlzGUAvLrnJoFAvBTGg669zcz0(Transform parent)
	{
		HierarchyField hierarchyField = UnityEngine.Object.Instantiate(Ze0nQhxYsIP6XusbhzlukXc, parent, worldPositionStays: false);
		hierarchyField.Initialize(this);
		hierarchyField.Skin = base.Skin;
		jmrMHuT_0024qNiP5B6xGnLIs6A.Add(hierarchyField);
		return hierarchyField;
	}

	RecycledListItem IListViewAdapter.CreateItem(Transform parent)
	{
		//ILSpy generated this explicit interface implementation from .override directive in ODVL6hEHtxJ1hbG12loRGsajTIjh_sbobfpZlHh2C0gK_LI_ykQhXvjJVdY_mBlzGUAvLrnJoFAvBTGg669zcz0
		return this.ODVL6hEHtxJ1hbG12loRGsajTIjh_sbobfpZlHh2C0gK_LI_ykQhXvjJVdY_mBlzGUAvLrnJoFAvBTGg669zcz0(parent);
	}

	[CompilerGenerated]
	private void method_2(Transform transform_0)
	{
		if (smethod_10((UnityEngine.Object)ConnectedInspector))
		{
			if (!smethod_10((UnityEngine.Object)transform_0))
			{
				ConnectedInspector.StopInspect();
			}
			else
			{
				ConnectedInspector.Inspect(smethod_11((Component)transform_0));
			}
		}
	}

	internal static Scene smethod_3(int int_0)
	{
		return SceneManager.GetSceneAt(int_0);
	}

	internal static int smethod_4()
	{
		return SceneManager.sceneCount;
	}

	internal static RectTransform smethod_5(ScrollRect scrollRect_0)
	{
		return scrollRect_0.content;
	}

	internal static Vector2 smethod_6(RectTransform rectTransform_0)
	{
		return rectTransform_0.sizeDelta;
	}

	internal static string smethod_7(InputField inputField_0)
	{
		return inputField_0.text;
	}

	internal static void smethod_8(InputField inputField_0, string string_0)
	{
		inputField_0.text = string_0;
	}

	internal static bool smethod_9(UnityEngine.Object object_0, UnityEngine.Object object_1)
	{
		return object_0 != object_1;
	}

	internal static bool smethod_10(UnityEngine.Object object_0)
	{
		return object_0;
	}

	internal static GameObject smethod_11(Component component_0)
	{
		return component_0.gameObject;
	}

	internal static PointerEventData smethod_12(EventSystem eventSystem_0)
	{
		return new PointerEventData(eventSystem_0);
	}

	internal static InputField.OnChangeEvent smethod_13(InputField inputField_0)
	{
		return inputField_0.onValueChanged;
	}

	internal static Delegate smethod_14(Delegate delegate_0, Delegate delegate_1)
	{
		return Delegate.Combine(delegate_0, delegate_1);
	}

	internal static void smethod_15(UnityAction<Scene, LoadSceneMode> unityAction_0)
	{
		SceneManager.sceneLoaded += unityAction_0;
	}

	internal static void smethod_16(UnityAction<Scene> unityAction_0)
	{
		SceneManager.sceneUnloaded += unityAction_0;
	}

	internal static void smethod_17(UnityAction<Scene, LoadSceneMode> unityAction_0)
	{
		SceneManager.sceneLoaded -= unityAction_0;
	}

	internal static void smethod_18(UnityAction<Scene> unityAction_0)
	{
		SceneManager.sceneUnloaded -= unityAction_0;
	}

	internal static float smethod_19()
	{
		return Time.realtimeSinceStartup;
	}

	internal static bool smethod_20(KeyCode keyCode_0)
	{
		return Input.GetKeyDown(keyCode_0);
	}

	internal static void smethod_21(UnityEngine.Object object_0)
	{
		UnityEngine.Object.Destroy(object_0);
	}

	internal static bool smethod_22(GameObject gameObject_0)
	{
		return gameObject_0.activeSelf;
	}

	internal static float smethod_23(ScrollRect scrollRect_0)
	{
		return scrollRect_0.verticalScrollbarSpacing;
	}

	internal static void smethod_24(ScrollRect scrollRect_0, PointerEventData pointerEventData_0)
	{
		scrollRect_0.OnScroll(pointerEventData_0);
	}

	internal static Scene smethod_25(GameObject gameObject_0)
	{
		return gameObject_0.scene;
	}

	internal static bool smethod_26(UnityEngine.Object object_0, UnityEngine.Object object_1)
	{
		return object_0 == object_1;
	}

	internal static void smethod_27(Graphic graphic_0, Color color_0)
	{
		graphic_0.color = color_0;
	}

	internal static Text smethod_28(InputField inputField_0)
	{
		return inputField_0.textComponent;
	}

	internal static Graphic smethod_29(InputField inputField_0)
	{
		return inputField_0.placeholder;
	}

	internal static Color smethod_30(Graphic graphic_0)
	{
		return graphic_0.color;
	}

	internal static void smethod_31(RectTransform rectTransform_0)
	{
		LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform_0);
	}

	internal static void smethod_32(StringBuilder stringBuilder_0, int int_0)
	{
		stringBuilder_0.Length = int_0;
	}

	internal static StringBuilder smethod_33(StringBuilder stringBuilder_0, string string_0)
	{
		return stringBuilder_0.AppendLine(string_0);
	}

	internal static StringBuilder smethod_34(StringBuilder stringBuilder_0, string string_0)
	{
		return stringBuilder_0.Append(string_0);
	}

	internal static string smethod_35(UnityEngine.Object object_0)
	{
		return object_0.name;
	}

	internal static Transform smethod_36(Transform transform_0)
	{
		return transform_0.parent;
	}

	internal static string smethod_37(object object_0)
	{
		return object_0.ToString();
	}

	internal static void smethod_38(Text text_1, string string_0)
	{
		text_1.text = string_0;
	}

	internal static void smethod_39(GameObject gameObject_0, bool bool_1)
	{
		gameObject_0.SetActive(bool_1);
	}

	internal static void smethod_40(ScrollRect scrollRect_0, float float_2)
	{
		scrollRect_0.verticalNormalizedPosition = float_2;
	}

	internal static string smethod_41(string string_0)
	{
		return string_0.Trim();
	}

	internal static bool smethod_42(string string_0)
	{
		return string.IsNullOrEmpty(string_0);
	}

	internal static GameObject smethod_43()
	{
		return new GameObject();
	}

	internal static void smethod_44(UnityEngine.Object object_0)
	{
		UnityEngine.Object.DontDestroyOnLoad(object_0);
	}

	internal static void smethod_45(UnityEngine.Object object_0)
	{
		UnityEngine.Object.DestroyImmediate(object_0);
	}

	internal static void smethod_46(Exception exception_0)
	{
		Debug.LogException(exception_0);
	}

	internal static bool smethod_47(IEnumerator ienumerator_0)
	{
		return ienumerator_0.MoveNext();
	}

	internal static void smethod_48(IDisposable idisposable_0)
	{
		idisposable_0.Dispose();
	}

	internal static bool smethod_49(string string_0, string string_1)
	{
		return string_0 == string_1;
	}
}
