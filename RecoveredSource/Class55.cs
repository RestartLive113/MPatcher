using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using HarmonyLib;
using MPatchrMain;
using UnityEngine;
using UnityEngine.SceneManagement;

[HarmonyPatch("Start")]
[HarmonyPatch(new Type[] { })]
[HarmonyPatch(typeof(StampController))]
internal static class Class55
{
	[CompilerGenerated]
	private sealed class BDsu2gDm3UkKizGt9vn3_00243VQig_0p9NJeI4SlcLZzggylERAweD4L7HtSO620IgjKfru9rSe3eSoUr4REWoubFB1Mzf_0024ngIJOCCMdg1W7_0024kJlLd75nGiP2DLK8pQcriOLw : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int SjlBM8inVA_YE4YVlr_0024gluY;

		private object yT7HpVIzmqW54W307WgJtr4;

		public StampController wQ6mrkDog7tAEXGul0Y8Sv0;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return yT7HpVIzmqW54W307WgJtr4;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return yT7HpVIzmqW54W307WgJtr4;
			}
		}

		[DebuggerHidden]
		public BDsu2gDm3UkKizGt9vn3_00243VQig_0p9NJeI4SlcLZzggylERAweD4L7HtSO620IgjKfru9rSe3eSoUr4REWoubFB1Mzf_0024ngIJOCCMdg1W7_0024kJlLd75nGiP2DLK8pQcriOLw(int _003C_003E1__state)
		{
			SjlBM8inVA_YE4YVlr_0024gluY = _003C_003E1__state;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			SjlBM8inVA_YE4YVlr_0024gluY = -2;
		}

		private bool MoveNext()
		{
			switch (SjlBM8inVA_YE4YVlr_0024gluY)
			{
			default:
				return false;
			case 1:
			{
				SjlBM8inVA_YE4YVlr_0024gluY = -1;
				MeshRenderer component = wQ6mrkDog7tAEXGul0Y8Sv0.HDKLPEHKJNA.GetComponent<MeshRenderer>();
				wQ6mrkDog7tAEXGul0Y8Sv0.HDKLPEHKJNA.GetComponent<MeshFilter>();
				smethod_2(smethod_1((Component)component));
				if (smethod_3((UnityEngine.Object)XqZlo8a76S_fTAVbH0S0THg, (UnityEngine.Object)null))
				{
					XqZlo8a76S_fTAVbH0S0THg = smethod_4(global::_003CModule_003E.smethod_26<string>(2461955712u));
				}
				if (!smethod_3((UnityEngine.Object)XqZlo8a76S_fTAVbH0S0THg, (UnityEngine.Object)null))
				{
					smethod_7((UnityEngine.Object)smethod_6(smethod_5((Renderer)component)));
					if (MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.stampNormalMap)
					{
						if (smethod_9(smethod_8((Component)wQ6mrkDog7tAEXGul0Y8Sv0), global::_003CModule_003E.smethod_26<string>(1443219645u)))
						{
							return false;
						}
						string text = Class56.nImd3eE4fzkfSnSPsEWM5L88g7B6hCV1Os3TO5zmspa2();
						if (!smethod_10(text))
						{
							return false;
						}
						if (smethod_2(smethod_1((Component)component)).y < 0.09f)
						{
							return false;
						}
						GameObject gameObject = UnityEngine.Object.Instantiate(smethod_11((Component)component), smethod_1((Component)component), worldPositionStays: true);
						smethod_14(smethod_12(gameObject), smethod_13(smethod_1((Component)component)));
						smethod_16(smethod_12(gameObject), smethod_15(smethod_1((Component)component)));
						smethod_12(gameObject).localScale = new Vector3(1.001f, 1.001f, 1.001f);
						UnityEngine.Object.Destroy(gameObject.GetComponent<BoxCollider>());
						UnityEngine.Object.Destroy(gameObject.GetComponent<StampController>());
						MeshRenderer component2 = gameObject.GetComponent<MeshRenderer>();
						gameObject.GetComponent<MeshFilter>();
						Material material = new Material(XqZlo8a76S_fTAVbH0S0THg.LoadAsset<Material>(global::_003CModule_003E.smethod_29<string>(1572373692u)));
						Texture2D texture2D = new Texture2D(1, 1);
						texture2D.LoadImage(File.ReadAllBytes(text));
						wQ6mrkDog7tAEXGul0Y8Sv0.FPEBEEJGFPI.GetComponent<Class56>().texture2D_0 = texture2D;
						material.renderQueue = component.material.renderQueue + 2000;
						component2.material = material;
						gameObject.AddComponent<IK5FoqU27QNYKBuS9GCG4jJmggiDoTvlnEzqfuzDeGITj4Y7Swt3U7EZtdklA6tU2RFesc0NlU_0024tEranc1z8GYk>().Qc0PQF_0024d1GAkt_QHyg2FdlQ(wQ6mrkDog7tAEXGul0Y8Sv0, null, component);
					}
					return false;
				}
				return false;
			}
			case 0:
				SjlBM8inVA_YE4YVlr_0024gluY = -1;
				yT7HpVIzmqW54W307WgJtr4 = smethod_0(0.5f);
				SjlBM8inVA_YE4YVlr_0024gluY = 1;
				return true;
			}
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw smethod_17();
		}

		internal static WaitForSecondsRealtime smethod_0(float float_0)
		{
			return new WaitForSecondsRealtime(float_0);
		}

		internal static Transform smethod_1(Component component_0)
		{
			return component_0.transform;
		}

		internal static Vector3 smethod_2(Transform transform_0)
		{
			return transform_0.localScale;
		}

		internal static bool smethod_3(UnityEngine.Object object_0, UnityEngine.Object object_1)
		{
			return object_0 == object_1;
		}

		internal static AssetBundle smethod_4(string string_0)
		{
			return AssetBundle.LoadFromFile(string_0);
		}

		internal static Material smethod_5(Renderer renderer_0)
		{
			return renderer_0.material;
		}

		internal static Shader smethod_6(Material material_0)
		{
			return material_0.shader;
		}

		internal static string smethod_7(UnityEngine.Object object_0)
		{
			return object_0.name;
		}

		internal static string smethod_8(Component component_0)
		{
			return component_0.tag;
		}

		internal static bool smethod_9(string string_0, string string_1)
		{
			return string_0 != string_1;
		}

		internal static bool smethod_10(string string_0)
		{
			return File.Exists(string_0);
		}

		internal static GameObject smethod_11(Component component_0)
		{
			return component_0.gameObject;
		}

		internal static Transform smethod_12(GameObject gameObject_0)
		{
			return gameObject_0.transform;
		}

		internal static Vector3 smethod_13(Transform transform_0)
		{
			return transform_0.position;
		}

		internal static void smethod_14(Transform transform_0, Vector3 vector3_0)
		{
			transform_0.position = vector3_0;
		}

		internal static Quaternion smethod_15(Transform transform_0)
		{
			return transform_0.rotation;
		}

		internal static void smethod_16(Transform transform_0, Quaternion quaternion_0)
		{
			transform_0.rotation = quaternion_0;
		}

		internal static NotSupportedException smethod_17()
		{
			return new NotSupportedException();
		}
	}

	private static AssetBundle XqZlo8a76S_fTAVbH0S0THg;

	private static readonly float vkQd8af78lY_h7fsAiTPszk = 0.99f;

	private static float _7J4ZBbxmI_00245LIFjHu57KIg(float r, float g, float b)
	{
		if (r > g && r > b)
		{
			return r;
		}
		if (g > r && g > b)
		{
			return g;
		}
		return b;
	}

	[HarmonyPrefix]
	internal static void smethod_0(StampController __instance)
	{
		if (MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.stampNormalMap && !(smethod_1().name == global::_003CModule_003E.smethod_27<string>(3514760917u)))
		{
			__instance.StartCoroutine(iLj7RfnRl85WMRemInbdaYg(__instance));
		}
	}

	private static IEnumerator iLj7RfnRl85WMRemInbdaYg(StampController __instance)
	{
		yield return BDsu2gDm3UkKizGt9vn3_00243VQig_0p9NJeI4SlcLZzggylERAweD4L7HtSO620IgjKfru9rSe3eSoUr4REWoubFB1Mzf_0024ngIJOCCMdg1W7_0024kJlLd75nGiP2DLK8pQcriOLw.smethod_0(0.5f);
		MeshRenderer component = __instance.HDKLPEHKJNA.GetComponent<MeshRenderer>();
		__instance.HDKLPEHKJNA.GetComponent<MeshFilter>();
		BDsu2gDm3UkKizGt9vn3_00243VQig_0p9NJeI4SlcLZzggylERAweD4L7HtSO620IgjKfru9rSe3eSoUr4REWoubFB1Mzf_0024ngIJOCCMdg1W7_0024kJlLd75nGiP2DLK8pQcriOLw.smethod_2(BDsu2gDm3UkKizGt9vn3_00243VQig_0p9NJeI4SlcLZzggylERAweD4L7HtSO620IgjKfru9rSe3eSoUr4REWoubFB1Mzf_0024ngIJOCCMdg1W7_0024kJlLd75nGiP2DLK8pQcriOLw.smethod_1((Component)component));
		if (BDsu2gDm3UkKizGt9vn3_00243VQig_0p9NJeI4SlcLZzggylERAweD4L7HtSO620IgjKfru9rSe3eSoUr4REWoubFB1Mzf_0024ngIJOCCMdg1W7_0024kJlLd75nGiP2DLK8pQcriOLw.smethod_3((UnityEngine.Object)XqZlo8a76S_fTAVbH0S0THg, (UnityEngine.Object)null))
		{
			XqZlo8a76S_fTAVbH0S0THg = BDsu2gDm3UkKizGt9vn3_00243VQig_0p9NJeI4SlcLZzggylERAweD4L7HtSO620IgjKfru9rSe3eSoUr4REWoubFB1Mzf_0024ngIJOCCMdg1W7_0024kJlLd75nGiP2DLK8pQcriOLw.smethod_4(global::_003CModule_003E.smethod_26<string>(2461955712u));
		}
		if (BDsu2gDm3UkKizGt9vn3_00243VQig_0p9NJeI4SlcLZzggylERAweD4L7HtSO620IgjKfru9rSe3eSoUr4REWoubFB1Mzf_0024ngIJOCCMdg1W7_0024kJlLd75nGiP2DLK8pQcriOLw.smethod_3((UnityEngine.Object)XqZlo8a76S_fTAVbH0S0THg, (UnityEngine.Object)null))
		{
			yield break;
		}
		BDsu2gDm3UkKizGt9vn3_00243VQig_0p9NJeI4SlcLZzggylERAweD4L7HtSO620IgjKfru9rSe3eSoUr4REWoubFB1Mzf_0024ngIJOCCMdg1W7_0024kJlLd75nGiP2DLK8pQcriOLw.smethod_7((UnityEngine.Object)BDsu2gDm3UkKizGt9vn3_00243VQig_0p9NJeI4SlcLZzggylERAweD4L7HtSO620IgjKfru9rSe3eSoUr4REWoubFB1Mzf_0024ngIJOCCMdg1W7_0024kJlLd75nGiP2DLK8pQcriOLw.smethod_6(BDsu2gDm3UkKizGt9vn3_00243VQig_0p9NJeI4SlcLZzggylERAweD4L7HtSO620IgjKfru9rSe3eSoUr4REWoubFB1Mzf_0024ngIJOCCMdg1W7_0024kJlLd75nGiP2DLK8pQcriOLw.smethod_5((Renderer)component)));
		if (MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.stampNormalMap && !BDsu2gDm3UkKizGt9vn3_00243VQig_0p9NJeI4SlcLZzggylERAweD4L7HtSO620IgjKfru9rSe3eSoUr4REWoubFB1Mzf_0024ngIJOCCMdg1W7_0024kJlLd75nGiP2DLK8pQcriOLw.smethod_9(BDsu2gDm3UkKizGt9vn3_00243VQig_0p9NJeI4SlcLZzggylERAweD4L7HtSO620IgjKfru9rSe3eSoUr4REWoubFB1Mzf_0024ngIJOCCMdg1W7_0024kJlLd75nGiP2DLK8pQcriOLw.smethod_8((Component)__instance), global::_003CModule_003E.smethod_26<string>(1443219645u)))
		{
			string text = Class56.nImd3eE4fzkfSnSPsEWM5L88g7B6hCV1Os3TO5zmspa2();
			if (BDsu2gDm3UkKizGt9vn3_00243VQig_0p9NJeI4SlcLZzggylERAweD4L7HtSO620IgjKfru9rSe3eSoUr4REWoubFB1Mzf_0024ngIJOCCMdg1W7_0024kJlLd75nGiP2DLK8pQcriOLw.smethod_10(text) && !(BDsu2gDm3UkKizGt9vn3_00243VQig_0p9NJeI4SlcLZzggylERAweD4L7HtSO620IgjKfru9rSe3eSoUr4REWoubFB1Mzf_0024ngIJOCCMdg1W7_0024kJlLd75nGiP2DLK8pQcriOLw.smethod_2(BDsu2gDm3UkKizGt9vn3_00243VQig_0p9NJeI4SlcLZzggylERAweD4L7HtSO620IgjKfru9rSe3eSoUr4REWoubFB1Mzf_0024ngIJOCCMdg1W7_0024kJlLd75nGiP2DLK8pQcriOLw.smethod_1((Component)component)).y < 0.09f))
			{
				GameObject gameObject = UnityEngine.Object.Instantiate(BDsu2gDm3UkKizGt9vn3_00243VQig_0p9NJeI4SlcLZzggylERAweD4L7HtSO620IgjKfru9rSe3eSoUr4REWoubFB1Mzf_0024ngIJOCCMdg1W7_0024kJlLd75nGiP2DLK8pQcriOLw.smethod_11((Component)component), BDsu2gDm3UkKizGt9vn3_00243VQig_0p9NJeI4SlcLZzggylERAweD4L7HtSO620IgjKfru9rSe3eSoUr4REWoubFB1Mzf_0024ngIJOCCMdg1W7_0024kJlLd75nGiP2DLK8pQcriOLw.smethod_1((Component)component), worldPositionStays: true);
				BDsu2gDm3UkKizGt9vn3_00243VQig_0p9NJeI4SlcLZzggylERAweD4L7HtSO620IgjKfru9rSe3eSoUr4REWoubFB1Mzf_0024ngIJOCCMdg1W7_0024kJlLd75nGiP2DLK8pQcriOLw.smethod_14(BDsu2gDm3UkKizGt9vn3_00243VQig_0p9NJeI4SlcLZzggylERAweD4L7HtSO620IgjKfru9rSe3eSoUr4REWoubFB1Mzf_0024ngIJOCCMdg1W7_0024kJlLd75nGiP2DLK8pQcriOLw.smethod_12(gameObject), BDsu2gDm3UkKizGt9vn3_00243VQig_0p9NJeI4SlcLZzggylERAweD4L7HtSO620IgjKfru9rSe3eSoUr4REWoubFB1Mzf_0024ngIJOCCMdg1W7_0024kJlLd75nGiP2DLK8pQcriOLw.smethod_13(BDsu2gDm3UkKizGt9vn3_00243VQig_0p9NJeI4SlcLZzggylERAweD4L7HtSO620IgjKfru9rSe3eSoUr4REWoubFB1Mzf_0024ngIJOCCMdg1W7_0024kJlLd75nGiP2DLK8pQcriOLw.smethod_1((Component)component)));
				BDsu2gDm3UkKizGt9vn3_00243VQig_0p9NJeI4SlcLZzggylERAweD4L7HtSO620IgjKfru9rSe3eSoUr4REWoubFB1Mzf_0024ngIJOCCMdg1W7_0024kJlLd75nGiP2DLK8pQcriOLw.smethod_16(BDsu2gDm3UkKizGt9vn3_00243VQig_0p9NJeI4SlcLZzggylERAweD4L7HtSO620IgjKfru9rSe3eSoUr4REWoubFB1Mzf_0024ngIJOCCMdg1W7_0024kJlLd75nGiP2DLK8pQcriOLw.smethod_12(gameObject), BDsu2gDm3UkKizGt9vn3_00243VQig_0p9NJeI4SlcLZzggylERAweD4L7HtSO620IgjKfru9rSe3eSoUr4REWoubFB1Mzf_0024ngIJOCCMdg1W7_0024kJlLd75nGiP2DLK8pQcriOLw.smethod_15(BDsu2gDm3UkKizGt9vn3_00243VQig_0p9NJeI4SlcLZzggylERAweD4L7HtSO620IgjKfru9rSe3eSoUr4REWoubFB1Mzf_0024ngIJOCCMdg1W7_0024kJlLd75nGiP2DLK8pQcriOLw.smethod_1((Component)component)));
				BDsu2gDm3UkKizGt9vn3_00243VQig_0p9NJeI4SlcLZzggylERAweD4L7HtSO620IgjKfru9rSe3eSoUr4REWoubFB1Mzf_0024ngIJOCCMdg1W7_0024kJlLd75nGiP2DLK8pQcriOLw.smethod_12(gameObject).localScale = new Vector3(1.001f, 1.001f, 1.001f);
				UnityEngine.Object.Destroy(gameObject.GetComponent<BoxCollider>());
				UnityEngine.Object.Destroy(gameObject.GetComponent<StampController>());
				MeshRenderer component2 = gameObject.GetComponent<MeshRenderer>();
				gameObject.GetComponent<MeshFilter>();
				Material material = new Material(XqZlo8a76S_fTAVbH0S0THg.LoadAsset<Material>(global::_003CModule_003E.smethod_29<string>(1572373692u)));
				Texture2D texture2D = new Texture2D(1, 1);
				texture2D.LoadImage(File.ReadAllBytes(text));
				__instance.FPEBEEJGFPI.GetComponent<Class56>().texture2D_0 = texture2D;
				material.renderQueue = component.material.renderQueue + 2000;
				component2.material = material;
				gameObject.AddComponent<IK5FoqU27QNYKBuS9GCG4jJmggiDoTvlnEzqfuzDeGITj4Y7Swt3U7EZtdklA6tU2RFesc0NlU_0024tEranc1z8GYk>().Qc0PQF_0024d1GAkt_QHyg2FdlQ(__instance, null, component);
			}
		}
	}

	public static void G61B_2jvgxBdcbjFTJCYZZQ(GameObject target, Vector3 pivot, Vector3 newScale)
	{
		Vector3 vector = smethod_3(smethod_2(target)) - pivot;
		float num = newScale.x / smethod_4(smethod_2(target)).x;
		Vector3 vector3_ = pivot + vector * num;
		smethod_5(smethod_2(target), newScale);
		smethod_6(smethod_2(target), vector3_);
	}

	internal static void MB4V5ahgKNonxUqDz1C8X92J2fRrbY6gHbOaoQnjXfJw(MeshRenderer originalMeshRenderer, float scalex, float scaley, float scalez, bool triplanar = false)
	{
		if (smethod_4(smethod_7((Component)originalMeshRenderer)).y < 0.09f)
		{
			return;
		}
		GameObject gameObject = UnityEngine.Object.Instantiate(smethod_8((Component)originalMeshRenderer), smethod_7((Component)originalMeshRenderer));
		Component[] components = gameObject.GetComponents<Component>();
		foreach (Component object_ in components)
		{
			string string_ = smethod_10((MemberInfo)smethod_9((object)object_));
			if (smethod_11(string_, global::_003CModule_003E.smethod_29<string>(2881468556u)) && smethod_11(string_, global::_003CModule_003E.smethod_27<string>(1526227153u)) && smethod_11(string_, global::_003CModule_003E.smethod_28<string>(813361274u)))
			{
				smethod_12((UnityEngine.Object)object_);
			}
		}
		components = gameObject.GetComponents<Component>();
		foreach (Component object_2 in components)
		{
			string string_2 = smethod_10((MemberInfo)smethod_9((object)object_2));
			if (smethod_11(string_2, global::_003CModule_003E.smethod_29<string>(2881468556u)) && smethod_11(string_2, global::_003CModule_003E.smethod_28<string>(1177600878u)) && smethod_11(string_2, global::_003CModule_003E.smethod_25<string>(3275187507u)))
			{
				smethod_12((UnityEngine.Object)object_2);
			}
		}
		smethod_14(smethod_2(gameObject), smethod_13(smethod_7((Component)originalMeshRenderer)));
		smethod_16(smethod_2(gameObject), smethod_15(smethod_7((Component)originalMeshRenderer)));
		if (scalex != 1f || scaley != 1f || scalez != 1f)
		{
			G61B_2jvgxBdcbjFTJCYZZQ(smethod_17(gameObject), smethod_19(smethod_18(originalMeshRenderer.GetComponent<MeshFilter>())).center, new Vector3(scalex, scaley, scalez));
		}
		gameObject.transform.localScale = new Vector3(scalex, scaley, scalez);
		MeshRenderer component = gameObject.GetComponent<MeshRenderer>();
		gameObject.GetComponent<MeshFilter>();
		if (gameObject.GetComponent<PlateController>() != null)
		{
			UnityEngine.Object.Destroy(gameObject.GetComponent<PlateController>());
		}
		if (XqZlo8a76S_fTAVbH0S0THg == null)
		{
			XqZlo8a76S_fTAVbH0S0THg = AssetBundle.LoadFromFile(global::_003CModule_003E.smethod_27<string>(2205608825u));
		}
		Material material = ((!triplanar) ? XqZlo8a76S_fTAVbH0S0THg.LoadAsset<Material>(global::_003CModule_003E.smethod_25<string>(472254811u)) : XqZlo8a76S_fTAVbH0S0THg.LoadAsset<Material>(global::_003CModule_003E.smethod_27<string>(2252139500u)));
		material.renderQueue = originalMeshRenderer.material.renderQueue + 2000;
		component.material = new Material(material);
		gameObject.AddComponent<IK5FoqU27QNYKBuS9GCG4jJmggiDoTvlnEzqfuzDeGITj4Y7Swt3U7EZtdklA6tU2RFesc0NlU_0024tEranc1z8GYk>().Qc0PQF_0024d1GAkt_QHyg2FdlQ(null, originalMeshRenderer.GetComponent<PlateController>(), originalMeshRenderer);
	}

	internal static Scene smethod_1()
	{
		return SceneManager.GetActiveScene();
	}

	internal static Transform smethod_2(GameObject gameObject_0)
	{
		return gameObject_0.transform;
	}

	internal static Vector3 smethod_3(Transform transform_0)
	{
		return transform_0.localPosition;
	}

	internal static Vector3 smethod_4(Transform transform_0)
	{
		return transform_0.localScale;
	}

	internal static void smethod_5(Transform transform_0, Vector3 vector3_0)
	{
		transform_0.localScale = vector3_0;
	}

	internal static void smethod_6(Transform transform_0, Vector3 vector3_0)
	{
		transform_0.localPosition = vector3_0;
	}

	internal static Transform smethod_7(Component component_0)
	{
		return component_0.transform;
	}

	internal static GameObject smethod_8(Component component_0)
	{
		return component_0.gameObject;
	}

	internal static Type smethod_9(object object_0)
	{
		return object_0.GetType();
	}

	internal static string smethod_10(MemberInfo memberInfo_0)
	{
		return memberInfo_0.Name;
	}

	internal static bool smethod_11(string string_0, string string_1)
	{
		return string_0 != string_1;
	}

	internal static void smethod_12(UnityEngine.Object object_0)
	{
		UnityEngine.Object.Destroy(object_0);
	}

	internal static Vector3 smethod_13(Transform transform_0)
	{
		return transform_0.position;
	}

	internal static void smethod_14(Transform transform_0, Vector3 vector3_0)
	{
		transform_0.position = vector3_0;
	}

	internal static Quaternion smethod_15(Transform transform_0)
	{
		return transform_0.rotation;
	}

	internal static void smethod_16(Transform transform_0, Quaternion quaternion_0)
	{
		transform_0.rotation = quaternion_0;
	}

	internal static GameObject smethod_17(GameObject gameObject_0)
	{
		return gameObject_0.gameObject;
	}

	internal static Mesh smethod_18(MeshFilter meshFilter_0)
	{
		return meshFilter_0.mesh;
	}

	internal static Bounds smethod_19(Mesh mesh_0)
	{
		return mesh_0.bounds;
	}
}
