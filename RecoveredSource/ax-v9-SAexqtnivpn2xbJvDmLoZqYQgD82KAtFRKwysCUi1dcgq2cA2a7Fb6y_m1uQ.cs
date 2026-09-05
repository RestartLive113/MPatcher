using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using HarmonyLib;
using MPatchrMain;
using UnityEngine;
using UnityEngine.Rendering;

internal static class ax_0024v9_0024SAexqtnivpn2xbJvDmLoZqYQgD82KAtFRKwysCUi1dcgq2cA2a7Fb6y_m1uQ
{
	[HarmonyPatch(typeof(PAEHEMJNPND))]
	[HarmonyPatch("PKLHNJNFKFH")]
	internal static class kK8bkyuB8Zu60PtPS9BeRK7qd6HKZbGSkRF01RokQsRwpuj_JoA7VKWEtK894NznFK5CAN4VWz59xIIBt_4rTrpwsRiOH1djfhz3XvqKVhEbO4iPcd1Sm_0024aJ9tPZiDInFA
	{
		private static Dictionary<int, Material> tw5oqihY6r8qZmDQJGEGobw = new Dictionary<int, Material>();

		private static Dictionary<int, Material> N5Tbqa7KCJgt1hqDoJ94SoA = new Dictionary<int, Material>();

		internal static void Y4YMQ0FWTtVy4vb2vAlNAYU(GameObject __result, BlockData data = null)
		{
			try
			{
				if (data == null)
				{
					BlockController component = __result.GetComponent<BlockController>();
					if (smethod_0((UnityEngine.Object)component, (UnityEngine.Object)null))
					{
						return;
					}
					data = component.JNKEKNOAPHO;
				}
				int rgbI = data.rgbI;
				if (smethod_1(data))
				{
					return;
				}
				Material material;
				if (!tw5oqihY6r8qZmDQJGEGobw.ContainsKey(rgbI))
				{
					material = UnityEngine.Object.Instantiate(MPatchr.n5wPFlpwFJrXE8uDgzL1YDc.LoadAsset<Material>(global::_003CModule_003E.smethod_29<string>(1406035593u)));
					material.name = global::_003CModule_003E.smethod_26<string>(400795819u) + rgbI;
					material.color = data.GetColor() * 2f;
					material.enableInstancing = true;
					if (data.smethod_1())
					{
						material.SetColor(global::_003CModule_003E.smethod_26<string>(1885912824u), data.GetEmitColor());
						material.SetTexture(global::_003CModule_003E.smethod_29<string>(3645439917u), null);
						material.EnableKeyword(global::_003CModule_003E.smethod_26<string>(763226791u));
					}
					if (data.smethod_0())
					{
						material.SetFloat(global::_003CModule_003E.smethod_28<string>(1330750645u), 1f);
					}
					tw5oqihY6r8qZmDQJGEGobw.Add(rgbI, material);
					if ((data.type == BlockData.AAHMDBHDCDK.Mover || data.type == BlockData.AAHMDBHDCDK.Discharger || data.type == BlockData.AAHMDBHDCDK.Scope || data.type == BlockData.AAHMDBHDCDK.Tracker || data.type == BlockData.AAHMDBHDCDK.AGDevice || data.type == BlockData.AAHMDBHDCDK.Shield || data.type == BlockData.AAHMDBHDCDK.Wheel) && !N5Tbqa7KCJgt1hqDoJ94SoA.ContainsKey(data.rgbI))
					{
						Material material2 = UnityEngine.Object.Instantiate(material);
						material2.SetColor(global::_003CModule_003E.smethod_27<string>(248747153u), data.CalcBlazeColor() * 3f);
						material2.SetTexture(global::_003CModule_003E.smethod_25<string>(2375181258u), null);
						material2.EnableKeyword(global::_003CModule_003E.smethod_25<string>(2902715542u));
						N5Tbqa7KCJgt1hqDoJ94SoA.Add(data.rgbI, material2);
					}
				}
				else
				{
					material = tw5oqihY6r8qZmDQJGEGobw[rgbI];
				}
				if (__result.GetComponent<MeshRenderer>() != null)
				{
					__result.GetComponent<MeshRenderer>().sharedMaterial = material;
				}
				MeshRenderer[] componentsInChildren = __result.GetComponentsInChildren<MeshRenderer>(includeInactive: true);
				foreach (MeshRenderer meshRenderer in componentsInChildren)
				{
					if ((!(meshRenderer.name == global::_003CModule_003E.smethod_26<string>(2569111234u)) && !(meshRenderer.name == global::_003CModule_003E.smethod_25<string>(1938215226u)) && !(meshRenderer.name == global::_003CModule_003E.smethod_29<string>(1449220256u)) && !(meshRenderer.name == global::_003CModule_003E.smethod_28<string>(3060888764u)) && !(meshRenderer.name == global::_003CModule_003E.smethod_27<string>(1160782200u)) && !(meshRenderer.name == global::_003CModule_003E.smethod_28<string>(2605589259u))) || !N5Tbqa7KCJgt1hqDoJ94SoA.ContainsKey(data.rgbI))
					{
						meshRenderer.sharedMaterial = material;
					}
					else
					{
						meshRenderer.sharedMaterial = N5Tbqa7KCJgt1hqDoJ94SoA[data.rgbI];
					}
				}
			}
			catch (Exception)
			{
			}
		}

		[HarmonyPostfix]
		internal static void FeUAVwFbW6wGJJdNimZY9yI(GameObject __result)
		{
			if (MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.tracing && MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.tracingSupported && !smethod_0((UnityEngine.Object)(SceneMan.JFAOKFIDAGK as Arena), (UnityEngine.Object)null))
			{
				Y4YMQ0FWTtVy4vb2vAlNAYU(__result);
			}
		}

		internal static bool smethod_0(UnityEngine.Object object_0, UnityEngine.Object object_1)
		{
			return object_0 == object_1;
		}

		internal static bool smethod_1(BlockData blockData_0)
		{
			return blockData_0.IsTrans();
		}
	}

	[HarmonyPatch("MakeBox")]
	[HarmonyPatch(typeof(BoxGenController))]
	internal static class pYTpos8Hp6bKZk1KuMzbKYS27TBMwtq7mBP67nEZpPfc1DDlNNJ4yd7DsJl0sKq4TjMH_rEn8Ne_0024omyYmAxWzp1jgntOhf2z_SxajsUnDZKRXj1pBBvTYuhEtk_n9Dv7iw
	{
		[HarmonyPostfix]
		internal static void FeUAVwFbW6wGJJdNimZY9yI(BoxGenController __instance)
		{
			if (MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.tracing && MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.tracingSupported && !smethod_0((UnityEngine.Object)(SceneMan.JFAOKFIDAGK as Arena), (UnityEngine.Object)null))
			{
				kK8bkyuB8Zu60PtPS9BeRK7qd6HKZbGSkRF01RokQsRwpuj_JoA7VKWEtK894NznFK5CAN4VWz59xIIBt_4rTrpwsRiOH1djfhz3XvqKVhEbO4iPcd1Sm_0024aJ9tPZiDInFA.Y4YMQ0FWTtVy4vb2vAlNAYU(__instance.NGLBLAGMBLN, __instance.JNKEKNOAPHO);
			}
		}

		internal static bool smethod_0(UnityEngine.Object object_0, UnityEngine.Object object_1)
		{
			return object_0 == object_1;
		}
	}

	[HarmonyPatch("MakeCapsule")]
	[HarmonyPatch(typeof(CapGenController))]
	internal static class o4N6sDPgGylHiQdonYk6x1TKIrgjlvBb9NabAXKd54Qll0kbYI2fG9q4tTOswRLwiidwo8cmVKI_crhliohxV8qjwNebsbArnYwo4UdX3zDsbvGE2Bb8rHnJl3mK5WkVig
	{
		[HarmonyPostfix]
		internal static void FeUAVwFbW6wGJJdNimZY9yI(CapGenController __instance)
		{
			if (MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.tracing && MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.tracingSupported && !smethod_0((UnityEngine.Object)(SceneMan.JFAOKFIDAGK as Arena), (UnityEngine.Object)null))
			{
				kK8bkyuB8Zu60PtPS9BeRK7qd6HKZbGSkRF01RokQsRwpuj_JoA7VKWEtK894NznFK5CAN4VWz59xIIBt_4rTrpwsRiOH1djfhz3XvqKVhEbO4iPcd1Sm_0024aJ9tPZiDInFA.Y4YMQ0FWTtVy4vb2vAlNAYU(__instance.i0duGIJ6M7BXC7c2cbeyvXM1hCrcoNKNJzlqDrhwjoxM<GameObject>(global::_003CModule_003E.smethod_27<string>(2117667u)), __instance.JNKEKNOAPHO);
			}
		}

		internal static bool smethod_0(UnityEngine.Object object_0, UnityEngine.Object object_1)
		{
			return object_0 == object_1;
		}
	}

	[HarmonyPatch(typeof(RideCameraController))]
	[HarmonyPatch("JFFDGOFCGOJ")]
	internal static class KyntqBibyWTkeiAbBoA_0024nppi_0024tp_AR94Y6Mg2IVP_tbQuQ5lzyX57WLntLQSaLYe8JoU3oI2vKYVMubKtyi1o2DI3kPUxMOzG1NXq6fO8V3LqRgo6t48u4jjBfE1X_0024vK_Q
	{
		[HarmonyPrefix]
		internal static bool smethod_0()
		{
			if (MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.tracing && MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.tracingSupported)
			{
				return false;
			}
			return true;
		}
	}

	[HarmonyPatch(typeof(PKMPJBLHGMB))]
	[HarmonyPatch("MHMOMGFJHPE")]
	internal static class XNC_0024X9uoQycz6Br_Ur2QkNHcz5CqwMF0XMyzsaU_0024WkaZDMGcC05iebaedaxYNLpe_ywCHO6wg1lfmYJkyal0Dc4UaIJ_0024ym5WduY6j6nSMWTkKlDh_0024dTQJm0WIfaazZ2oOQ
	{
		[HarmonyPrefix]
		internal static bool smethod_0(GameObject NGLBLAGMBLN, string BNEFJAAJOLP, Material[] NMGKOOIKLOC)
		{
			if (MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.tracing && MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.tracingSupported)
			{
				if (smethod_1((UnityEngine.Object)(SceneMan.JFAOKFIDAGK as Arena), (UnityEngine.Object)null))
				{
					return true;
				}
				Dictionary<Material, List<CombineInstance>> dictionary = new Dictionary<Material, List<CombineInstance>>();
				MeshRenderer[] componentsInChildren = NGLBLAGMBLN.GetComponentsInChildren<MeshRenderer>(includeInactive: true);
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					Material[] array = smethod_2((Renderer)componentsInChildren[i]);
					foreach (Material material in array)
					{
						if (smethod_3((UnityEngine.Object)material, (UnityEngine.Object)null) && !dictionary.ContainsKey(material))
						{
							dictionary.Add(material, new List<CombineInstance>());
						}
					}
				}
				Matrix4x4 matrix4x = smethod_5(smethod_4(NGLBLAGMBLN));
				MeshFilter[] componentsInChildren2 = NGLBLAGMBLN.GetComponentsInChildren<MeshFilter>(includeInactive: true);
				foreach (MeshFilter meshFilter in componentsInChildren2)
				{
					if (smethod_1((UnityEngine.Object)smethod_6(meshFilter), (UnityEngine.Object)null))
					{
						continue;
					}
					Vector3 vector = smethod_8(smethod_7((Component)meshFilter));
					if (vector.y < 0.02f || vector.x < 0.02f || vector.z < 0.02f)
					{
						Vector2[] array2 = smethod_10(smethod_9(meshFilter));
						for (int num = array2.Length - 1; num >= 0; num--)
						{
							Vector2[] array3 = array2;
							int num2 = num;
							array3[num2].x = array3[num2].x * 0.5f;
						}
						smethod_11(smethod_9(meshFilter), array2);
					}
					CombineInstance item = default(CombineInstance);
					item.mesh = smethod_9(meshFilter);
					item.mesh.tangents = null;
					item.transform = matrix4x * meshFilter.transform.localToWorldMatrix;
					dictionary[meshFilter.GetComponent<MeshRenderer>().sharedMaterial].Add(item);
					meshFilter.GetComponent<MeshRenderer>().enabled = false;
				}
				foreach (Material key in dictionary.Keys)
				{
					if (key.name == global::_003CModule_003E.smethod_29<string>(1914374986u))
					{
						continue;
					}
					key.name.EndsWith(global::_003CModule_003E.smethod_28<string>(1076279859u));
					key.name.EndsWith(global::_003CModule_003E.smethod_28<string>(404999686u));
					List<CombineInstance> list = new List<CombineInstance>();
					int num3 = 0;
					for (int num4 = dictionary[key].Count - 1; num4 >= 0; num4--)
					{
						CombineInstance item2 = dictionary[key][num4];
						if (num3 + item2.mesh.vertexCount >= 65536)
						{
							PKMPJBLHGMB.CJELLJGAMEC(PKMPJBLHGMB.LDLIGDEOKGJ(NGLBLAGMBLN, BNEFJAAJOLP, key, list.ToArray()), 1f, NALEIJGIDAC: false);
							list.Clear();
							num3 = 0;
						}
						list.Add(item2);
						num3 += item2.mesh.vertexCount;
					}
					if (num3 > 0)
					{
						PKMPJBLHGMB.CJELLJGAMEC(PKMPJBLHGMB.LDLIGDEOKGJ(NGLBLAGMBLN, BNEFJAAJOLP, key, list.ToArray()), 1f, NALEIJGIDAC: false);
					}
				}
				return false;
			}
			return true;
		}

		internal static bool smethod_1(UnityEngine.Object object_0, UnityEngine.Object object_1)
		{
			return object_0 == object_1;
		}

		internal static Material[] smethod_2(Renderer renderer_0)
		{
			return renderer_0.sharedMaterials;
		}

		internal static bool smethod_3(UnityEngine.Object object_0, UnityEngine.Object object_1)
		{
			return object_0 != object_1;
		}

		internal static Transform smethod_4(GameObject gameObject_0)
		{
			return gameObject_0.transform;
		}

		internal static Matrix4x4 smethod_5(Transform transform_0)
		{
			return transform_0.worldToLocalMatrix;
		}

		internal static Mesh smethod_6(MeshFilter meshFilter_0)
		{
			return meshFilter_0.sharedMesh;
		}

		internal static Transform smethod_7(Component component_0)
		{
			return component_0.transform;
		}

		internal static Vector3 smethod_8(Transform transform_0)
		{
			return transform_0.localScale;
		}

		internal static Mesh smethod_9(MeshFilter meshFilter_0)
		{
			return meshFilter_0.mesh;
		}

		internal static Vector2[] smethod_10(Mesh mesh_0)
		{
			return mesh_0.uv;
		}

		internal static void smethod_11(Mesh mesh_0, Vector2[] vector2_0)
		{
			mesh_0.uv = vector2_0;
		}
	}

	[HarmonyPatch]
	internal static class lWL6zB_A30b36LWXtRFH7_0gCBdAXCjkoZy7_pc30v4AlubSKEJa1wha5bOaW40MX23gfRaEMdh7du_0024C6aMP2RUmKN5HUJZNvyi5SJzYpmIYtQ4J_taaa_0024L29QL_tJWCyg
	{
		[Serializable]
		[CompilerGenerated]
		private sealed class U3Q49iNDamRoGPqNvLKqiyt6CW4__nzxUE56n89NJVBBNzNOrHu7ClFlf0V0UgNHNm_0024VeIxA8o6bNjDVJiXCv1KsFK5oOJkM6dM3MPpf8Il8lhs_oj9iG4MM6Ig5K72eZw
		{
			public static readonly U3Q49iNDamRoGPqNvLKqiyt6CW4__nzxUE56n89NJVBBNzNOrHu7ClFlf0V0UgNHNm_0024VeIxA8o6bNjDVJiXCv1KsFK5oOJkM6dM3MPpf8Il8lhs_oj9iG4MM6Ig5K72eZw _003C_003E9 = new U3Q49iNDamRoGPqNvLKqiyt6CW4__nzxUE56n89NJVBBNzNOrHu7ClFlf0V0UgNHNm_0024VeIxA8o6bNjDVJiXCv1KsFK5oOJkM6dM3MPpf8Il8lhs_oj9iG4MM6Ig5K72eZw();

			internal bool f8zsjco5_0024D6dFUEXnUAXfME(Type type)
			{
				if (type.biP8NxgaNXgfx40jcevtRtY<CompilerGeneratedAttribute>())
				{
					return smethod_1(smethod_0((MemberInfo)type), global::_003CModule_003E.smethod_25<string>(1196166733u));
				}
				return false;
			}

			internal static string smethod_0(MemberInfo memberInfo_0)
			{
				return memberInfo_0.Name;
			}

			internal static bool smethod_1(string string_0, string string_1)
			{
				return string_0.Contains(string_1);
			}
		}

		internal static readonly Type xv3Kzc1X_00242wySmHL4toD_y8 = smethod_2(smethod_1(typeof(MachineSerializer).TypeHandle), (Func<Type, bool>)((Type type) => type.biP8NxgaNXgfx40jcevtRtY<CompilerGeneratedAttribute>() && U3Q49iNDamRoGPqNvLKqiyt6CW4__nzxUE56n89NJVBBNzNOrHu7ClFlf0V0UgNHNm_0024VeIxA8o6bNjDVJiXCv1KsFK5oOJkM6dM3MPpf8Il8lhs_oj9iG4MM6Ig5K72eZw.smethod_1(U3Q49iNDamRoGPqNvLKqiyt6CW4__nzxUE56n89NJVBBNzNOrHu7ClFlf0V0UgNHNm_0024VeIxA8o6bNjDVJiXCv1KsFK5oOJkM6dM3MPpf8Il8lhs_oj9iG4MM6Ig5K72eZw.smethod_0((MemberInfo)type), global::_003CModule_003E.smethod_25<string>(1196166733u))));

		[HarmonyTargetMethod]
		public static MethodBase TargetMethod()
		{
			if (xv3Kzc1X_00242wySmHL4toD_y8 == null)
			{
				mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng("Couldn't find iterator class -- This should never be reached.");
				return null;
			}
			MethodInfo methodInfo = AccessTools.Method(xv3Kzc1X_00242wySmHL4toD_y8, "MoveNext");
			if (methodInfo == null)
			{
				mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng("Couldn't find MoveNext");
			}
			return methodInfo;
		}

		[HarmonyTranspiler]
		internal static IEnumerable<CodeInstruction> ME8a9fXTPeYc1JTUjzrWW9E(IEnumerable<CodeInstruction> instructions)
		{
			CodeInstruction[] array = instructions.ToArray();
			List<CodeInstruction> list = new List<CodeInstruction>(array);
			CodeInstruction codeInstruction = smethod_0(array[2571]);
			list[2660].opcode = codeInstruction.opcode;
			list[2660].operand = codeInstruction.operand;
			list[2661].opcode = OpCodes.Nop;
			list[2662].opcode = OpCodes.Nop;
			list[2663].opcode = OpCodes.Nop;
			list[2664].opcode = OpCodes.Nop;
			list[2780].opcode = codeInstruction.opcode;
			list[2780].operand = codeInstruction.operand;
			list[2781].opcode = OpCodes.Nop;
			list[2782].opcode = OpCodes.Nop;
			list[2783].opcode = OpCodes.Nop;
			list[2784].opcode = OpCodes.Nop;
			codeInstruction = smethod_0(array[3191]);
			list[3400].opcode = codeInstruction.opcode;
			list[3280].operand = codeInstruction.operand;
			list[3281].opcode = OpCodes.Nop;
			list[3282].opcode = OpCodes.Nop;
			list[3283].opcode = OpCodes.Nop;
			list[3284].opcode = OpCodes.Nop;
			list[3400].opcode = codeInstruction.opcode;
			list[3400].operand = codeInstruction.operand;
			list[3401].opcode = OpCodes.Nop;
			list[3402].opcode = OpCodes.Nop;
			list[3403].opcode = OpCodes.Nop;
			list[3404].opcode = OpCodes.Nop;
			return list;
		}

		internal static CodeInstruction smethod_0(CodeInstruction codeInstruction_0)
		{
			return codeInstruction_0.Clone();
		}

		internal static Type smethod_1(RuntimeTypeHandle runtimeTypeHandle_0)
		{
			return Type.GetTypeFromHandle(runtimeTypeHandle_0);
		}

		internal static Type smethod_2(Type type_0, Func<Type, bool> func_0)
		{
			return AccessTools.FirstInner(type_0, func_0);
		}
	}

	[Serializable]
	[CompilerGenerated]
	private sealed class KLCaxCvJdgudKiEcHKwyGngrgo6lTVRQmbrP_iqcVq5gmnO_00244cCOzGQJC4kDzNjCifVzFIdNeTT7KPchDSmY9tZZmIiPvNeRBriiV2wMSrjm
	{
		public static readonly KLCaxCvJdgudKiEcHKwyGngrgo6lTVRQmbrP_iqcVq5gmnO_00244cCOzGQJC4kDzNjCifVzFIdNeTT7KPchDSmY9tZZmIiPvNeRBriiV2wMSrjm _003C_003E9 = new KLCaxCvJdgudKiEcHKwyGngrgo6lTVRQmbrP_iqcVq5gmnO_00244cCOzGQJC4kDzNjCifVzFIdNeTT7KPchDSmY9tZZmIiPvNeRBriiV2wMSrjm();

		public static Action<bool> _003C_003E9__3_0;

		public static Action<bool> _003C_003E9__3_1;

		public static Action<bool> _003C_003E9__3_2;

		internal void tyOIg9zWy6iuxLYS9xe1aS_0024v3iN8zH6aQOLStqEldRE1(bool toggled)
		{
			smethod_0().GetComponent<SEGI>().visualizeGI = toggled;
		}

		internal void uKBrzV8pBQ6FPPzdmqFg2u7yU5LjTU9nOaU8alaF8bhv(bool toggled)
		{
			smethod_0().GetComponent<SEGI>().voxelResolution = (toggled ? SEGI.VoxelResolution.low : SEGI.VoxelResolution.high);
		}

		internal void uR5eEMApu78qZxcets5BYe2eRcwZYqw_0024iqdNUiaoRDzK(bool toggled)
		{
			if (toggled)
			{
				int_0 = 75;
				_IN0d953fCPmhPnoNSWKMWWLcGNYMhHqwl8v_zibtty3 = 100;
			}
			else
			{
				int_0 = 500;
				_IN0d953fCPmhPnoNSWKMWWLcGNYMhHqwl8v_zibtty3 = 500;
			}
		}

		internal static Camera smethod_0()
		{
			return Camera.main;
		}
	}

	[CompilerGenerated]
	private sealed class CYTEsozUjTgZLgO4L4AMe_0024sc7yneTkyKmeM_0024mRfq2ps9_0024BqknWIvhGadjSz1bQi224BP68UKAKPfZJ2IJz4W_sb5y0zDf4il0SOJAe2clPQ_0024l8_Gdoarxsw5dXklJ9irQA
	{
		public Camera C7snNM2eD305CAM0G7qoArk;

		public SEGI oelvV8IFYSoj_bO4A_K0CuA;

		internal void PdSMfmFQJQNoPOGkB3H2b7J4jCAnFSTKM9U_0024PG03i3Dm(AmMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F me)
		{
			if (smethod_0((UnityEngine.Object)Arena.OEDCBNHNGMJ, (UnityEngine.Object)null) && smethod_0((UnityEngine.Object)Arena.OEDCBNHNGMJ.BOIEJCIBHKI, (UnityEngine.Object)null))
			{
				if (Arena.OEDCBNHNGMJ.BOIEJCIBHKI.ONGNOMCJBGE)
				{
					oelvV8IFYSoj_bO4A_K0CuA.voxelSpaceSize = _IN0d953fCPmhPnoNSWKMWWLcGNYMhHqwl8v_zibtty3;
					oelvV8IFYSoj_bO4A_K0CuA.followTransform = smethod_1((Component)C7snNM2eD305CAM0G7qoArk);
				}
				else
				{
					oelvV8IFYSoj_bO4A_K0CuA.voxelSpaceSize = int_0;
					oelvV8IFYSoj_bO4A_K0CuA.followTransform = smethod_2(Arena.OEDCBNHNGMJ.JPIAFJHAPHM);
				}
			}
			if (!smethod_0((UnityEngine.Object)oelvV8IFYSoj_bO4A_K0CuA.sun, (UnityEngine.Object)null) || (smethod_3((Behaviour)oelvV8IFYSoj_bO4A_K0CuA.sun) && smethod_4(oelvV8IFYSoj_bO4A_K0CuA.sun) >= 0.01f))
			{
				oelvV8IFYSoj_bO4A_K0CuA.giGain = 1.36f;
				oelvV8IFYSoj_bO4A_K0CuA.reflectionOcclusionPower = 1f;
				oelvV8IFYSoj_bO4A_K0CuA.nearLightGain = 0.1f;
			}
			else
			{
				oelvV8IFYSoj_bO4A_K0CuA.giGain = 4f;
				oelvV8IFYSoj_bO4A_K0CuA.reflectionOcclusionPower = 0.5f;
				oelvV8IFYSoj_bO4A_K0CuA.nearLightGain = 0.5f;
			}
		}

		internal static bool smethod_0(UnityEngine.Object object_0, UnityEngine.Object object_1)
		{
			return object_0 != object_1;
		}

		internal static Transform smethod_1(Component component_0)
		{
			return component_0.transform;
		}

		internal static Transform smethod_2(GameObject gameObject_0)
		{
			return gameObject_0.transform;
		}

		internal static bool smethod_3(Behaviour behaviour_0)
		{
			return behaviour_0.enabled;
		}

		internal static float smethod_4(Light light_0)
		{
			return light_0.intensity;
		}
	}

	internal static int int_0 = 500;

	internal static int _IN0d953fCPmhPnoNSWKMWWLcGNYMhHqwl8v_zibtty3 = 500;

	internal static void M_0024QnBmhKaMXj6kxXd5wv4R0()
	{
		if (!MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.tracing || !MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.tracingSupported || !smethod_0((UnityEngine.Object)(SceneMan.JFAOKFIDAGK as Arena), (UnityEngine.Object)null))
		{
			return;
		}
		int_0 = 500;
		_IN0d953fCPmhPnoNSWKMWWLcGNYMhHqwl8v_zibtty3 = 500;
		if (smethod_1(BuiltinShaderType.DeferredShading) == BuiltinShaderMode.Disabled)
		{
			smethod_2(BuiltinShaderType.DeferredShading, BuiltinShaderMode.UseCustom);
			smethod_3(BuiltinShaderType.DeferredShading, MPatchr.n5wPFlpwFJrXE8uDgzL1YDc.LoadAsset<Shader>(global::_003CModule_003E.smethod_29<string>(3884794769u)));
		}
		Camera C7snNM2eD305CAM0G7qoArk = smethod_4();
		if (!smethod_6((UnityEngine.Object)smethod_5((Component)C7snNM2eD305CAM0G7qoArk).GetComponent<SEGI>(), (UnityEngine.Object)null))
		{
			return;
		}
		smethod_7(C7snNM2eD305CAM0G7qoArk, RenderingPath.DeferredShading);
		SEGI oelvV8IFYSoj_bO4A_K0CuA = smethod_5((Component)C7snNM2eD305CAM0G7qoArk).AddComponent<SEGI>();
		smethod_8(0f);
		oelvV8IFYSoj_bO4A_K0CuA.ApplyPreset(r0FjGfwwmg4ieGHTHNmU4cI.cqWoMNveroNrLO3XzL3B_0024XA<SEGIPreset>(global::_003CModule_003E.smethod_27<string>(2635787689u)));
		oelvV8IFYSoj_bO4A_K0CuA.infiniteBounces = false;
		oelvV8IFYSoj_bO4A_K0CuA.voxelSpaceSize = int_0;
		AzureSky_Controller azureSky_Controller = UnityEngine.Object.FindObjectOfType<AzureSky_Controller>();
		if (smethod_0((UnityEngine.Object)azureSky_Controller, (UnityEngine.Object)null) && smethod_0((UnityEngine.Object)azureSky_Controller.MJHCKGEKBGL, (UnityEngine.Object)null) && smethod_0((UnityEngine.Object)azureSky_Controller.MJHCKGEKBGL.GetComponent<Light>(), (UnityEngine.Object)null))
		{
			oelvV8IFYSoj_bO4A_K0CuA.sun = azureSky_Controller.MJHCKGEKBGL.GetComponent<Light>();
		}
		else
		{
			GameObject gameObject = smethod_9(global::_003CModule_003E.smethod_26<string>(2392145649u));
			if (smethod_6((UnityEngine.Object)gameObject, (UnityEngine.Object)null))
			{
				return;
			}
			GameObject gameObject2 = gameObject.smethod_0(global::_003CModule_003E.smethod_29<string>(54982203u));
			if (smethod_0((UnityEngine.Object)gameObject2, (UnityEngine.Object)null) && smethod_0((UnityEngine.Object)gameObject2.GetComponent<Light>(), (UnityEngine.Object)null))
			{
				oelvV8IFYSoj_bO4A_K0CuA.sun = gameObject2.GetComponent<Light>();
			}
		}
		oelvV8IFYSoj_bO4A_K0CuA.reflectionSteps = 128;
		oelvV8IFYSoj_bO4A_K0CuA.reflectionOcclusionPower = 1f;
		oelvV8IFYSoj_bO4A_K0CuA.gaussianMipFilter = true;
		KX8jtCPdJb997Zi2Dvgkpuc();
		smethod_8(0f);
		smethod_10(Color.black);
		smethod_5((Component)C7snNM2eD305CAM0G7qoArk).AddComponent<AmMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F>().U_0024eIoX6e3N9Ag_us_EcGHBI(delegate
		{
			if (CYTEsozUjTgZLgO4L4AMe_0024sc7yneTkyKmeM_0024mRfq2ps9_0024BqknWIvhGadjSz1bQi224BP68UKAKPfZJ2IJz4W_sb5y0zDf4il0SOJAe2clPQ_0024l8_Gdoarxsw5dXklJ9irQA.smethod_0((UnityEngine.Object)Arena.OEDCBNHNGMJ, (UnityEngine.Object)null) && CYTEsozUjTgZLgO4L4AMe_0024sc7yneTkyKmeM_0024mRfq2ps9_0024BqknWIvhGadjSz1bQi224BP68UKAKPfZJ2IJz4W_sb5y0zDf4il0SOJAe2clPQ_0024l8_Gdoarxsw5dXklJ9irQA.smethod_0((UnityEngine.Object)Arena.OEDCBNHNGMJ.BOIEJCIBHKI, (UnityEngine.Object)null))
			{
				if (Arena.OEDCBNHNGMJ.BOIEJCIBHKI.ONGNOMCJBGE)
				{
					oelvV8IFYSoj_bO4A_K0CuA.voxelSpaceSize = _IN0d953fCPmhPnoNSWKMWWLcGNYMhHqwl8v_zibtty3;
					oelvV8IFYSoj_bO4A_K0CuA.followTransform = CYTEsozUjTgZLgO4L4AMe_0024sc7yneTkyKmeM_0024mRfq2ps9_0024BqknWIvhGadjSz1bQi224BP68UKAKPfZJ2IJz4W_sb5y0zDf4il0SOJAe2clPQ_0024l8_Gdoarxsw5dXklJ9irQA.smethod_1((Component)C7snNM2eD305CAM0G7qoArk);
				}
				else
				{
					oelvV8IFYSoj_bO4A_K0CuA.voxelSpaceSize = int_0;
					oelvV8IFYSoj_bO4A_K0CuA.followTransform = CYTEsozUjTgZLgO4L4AMe_0024sc7yneTkyKmeM_0024mRfq2ps9_0024BqknWIvhGadjSz1bQi224BP68UKAKPfZJ2IJz4W_sb5y0zDf4il0SOJAe2clPQ_0024l8_Gdoarxsw5dXklJ9irQA.smethod_2(Arena.OEDCBNHNGMJ.JPIAFJHAPHM);
				}
			}
			if (!CYTEsozUjTgZLgO4L4AMe_0024sc7yneTkyKmeM_0024mRfq2ps9_0024BqknWIvhGadjSz1bQi224BP68UKAKPfZJ2IJz4W_sb5y0zDf4il0SOJAe2clPQ_0024l8_Gdoarxsw5dXklJ9irQA.smethod_0((UnityEngine.Object)oelvV8IFYSoj_bO4A_K0CuA.sun, (UnityEngine.Object)null) || (CYTEsozUjTgZLgO4L4AMe_0024sc7yneTkyKmeM_0024mRfq2ps9_0024BqknWIvhGadjSz1bQi224BP68UKAKPfZJ2IJz4W_sb5y0zDf4il0SOJAe2clPQ_0024l8_Gdoarxsw5dXklJ9irQA.smethod_3((Behaviour)oelvV8IFYSoj_bO4A_K0CuA.sun) && CYTEsozUjTgZLgO4L4AMe_0024sc7yneTkyKmeM_0024mRfq2ps9_0024BqknWIvhGadjSz1bQi224BP68UKAKPfZJ2IJz4W_sb5y0zDf4il0SOJAe2clPQ_0024l8_Gdoarxsw5dXklJ9irQA.smethod_4(oelvV8IFYSoj_bO4A_K0CuA.sun) >= 0.01f))
			{
				oelvV8IFYSoj_bO4A_K0CuA.giGain = 1.36f;
				oelvV8IFYSoj_bO4A_K0CuA.reflectionOcclusionPower = 1f;
				oelvV8IFYSoj_bO4A_K0CuA.nearLightGain = 0.1f;
			}
			else
			{
				oelvV8IFYSoj_bO4A_K0CuA.giGain = 4f;
				oelvV8IFYSoj_bO4A_K0CuA.reflectionOcclusionPower = 0.5f;
				oelvV8IFYSoj_bO4A_K0CuA.nearLightGain = 0.5f;
			}
		});
	}

	internal static void KX8jtCPdJb997Zi2Dvgkpuc()
	{
		if (!MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.tracing || !MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.tracingSupported)
		{
			return;
		}
		GameObject gameObject = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.qSsYOsEQb7x452a9Y45dVEk(new Vector3(-390f, 0f), new Vector2(250f, 430f), GameObject.Find(global::_003CModule_003E.smethod_25<string>(806627754u)).smethod_0(global::_003CModule_003E.smethod_25<string>(2583385102u)).transform);
		Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.uEsWMK_pFkCY_0024M5zt8zLsQk(global::_003CModule_003E.smethod_26<string>(63446515u), new Vector3(0f, 170f), global::_003CModule_003E.smethod_28<string>(3774364230u), gameObject.transform, resetGroup: true, delegate(bool toggled)
		{
			KLCaxCvJdgudKiEcHKwyGngrgo6lTVRQmbrP_iqcVq5gmnO_00244cCOzGQJC4kDzNjCifVzFIdNeTT7KPchDSmY9tZZmIiPvNeRBriiV2wMSrjm.smethod_0().GetComponent<SEGI>().visualizeGI = toggled;
		});
		Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.uEsWMK_pFkCY_0024M5zt8zLsQk(global::_003CModule_003E.smethod_27<string>(2975478525u), new Vector3(0f, 100f), global::_003CModule_003E.smethod_26<string>(1750610773u), gameObject.transform, resetGroup: true, delegate(bool toggled)
		{
			KLCaxCvJdgudKiEcHKwyGngrgo6lTVRQmbrP_iqcVq5gmnO_00244cCOzGQJC4kDzNjCifVzFIdNeTT7KPchDSmY9tZZmIiPvNeRBriiV2wMSrjm.smethod_0().GetComponent<SEGI>().voxelResolution = (toggled ? SEGI.VoxelResolution.low : SEGI.VoxelResolution.high);
		});
		Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.uEsWMK_pFkCY_0024M5zt8zLsQk(global::_003CModule_003E.smethod_29<string>(3787322743u), new Vector3(0f, 50f), global::_003CModule_003E.smethod_26<string>(586261206u), gameObject.transform, resetGroup: true, delegate(bool toggled)
		{
			if (toggled)
			{
				int_0 = 75;
				_IN0d953fCPmhPnoNSWKMWWLcGNYMhHqwl8v_zibtty3 = 100;
			}
			else
			{
				int_0 = 500;
				_IN0d953fCPmhPnoNSWKMWWLcGNYMhHqwl8v_zibtty3 = 500;
			}
		});
	}

	internal static bool smethod_0(UnityEngine.Object object_0, UnityEngine.Object object_1)
	{
		return object_0 != object_1;
	}

	internal static BuiltinShaderMode smethod_1(BuiltinShaderType builtinShaderType_0)
	{
		return GraphicsSettings.GetShaderMode(builtinShaderType_0);
	}

	internal static void smethod_2(BuiltinShaderType builtinShaderType_0, BuiltinShaderMode builtinShaderMode_0)
	{
		GraphicsSettings.SetShaderMode(builtinShaderType_0, builtinShaderMode_0);
	}

	internal static void smethod_3(BuiltinShaderType builtinShaderType_0, Shader shader_0)
	{
		GraphicsSettings.SetCustomShader(builtinShaderType_0, shader_0);
	}

	internal static Camera smethod_4()
	{
		return Camera.main;
	}

	internal static GameObject smethod_5(Component component_0)
	{
		return component_0.gameObject;
	}

	internal static bool smethod_6(UnityEngine.Object object_0, UnityEngine.Object object_1)
	{
		return object_0 == object_1;
	}

	internal static void smethod_7(Camera camera_0, RenderingPath renderingPath_0)
	{
		camera_0.renderingPath = renderingPath_0;
	}

	internal static void smethod_8(float float_0)
	{
		RenderSettings.ambientIntensity = float_0;
	}

	internal static GameObject smethod_9(string string_0)
	{
		return GameObject.Find(string_0);
	}

	internal static void smethod_10(Color color_0)
	{
		RenderSettings.ambientSkyColor = color_0;
	}
}
