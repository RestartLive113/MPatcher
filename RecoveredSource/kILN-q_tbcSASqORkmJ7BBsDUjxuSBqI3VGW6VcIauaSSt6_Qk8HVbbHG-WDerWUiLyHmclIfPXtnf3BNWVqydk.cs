using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HarmonyLib;
using MPatchrMain;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

internal static class kILN_0024q_tbcSASqORkmJ7BBsDUjxuSBqI3VGW6VcIauaSSt6_Qk8HVbbHG_0024WDerWUiLyHmclIfPXtnf3BNWVqydk
{
	[HarmonyPatch(typeof(PrimNodeController))]
	[HarmonyPatch("MHMOMGFJHPE")]
	internal static class MWNHoohRGa4s8_0024E5_0024BQnWn3ijbG1uSnbYyYLKXAqwwUZ4nlVt6oBIbwDsjoESQ_0024p8myzj3I4Vw6vNTzvtcXVek26EDzirEZqgURXKFJQbD0db_HRwPgR_duZiLLUXXvAlrbP0uPDp1ixId7zVcoCW8Q
	{
		[HarmonyPrefix]
		internal static bool smethod_0(PrimNodeController __instance)
		{
			bool tracing = MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.tracing;
			List<CombineInstance> list = new List<CombineInstance>();
			List<CombineInstance> list2 = new List<CombineInstance>();
			Matrix4x4 matrix4x = smethod_2(smethod_1((Component)__instance));
			List<MeshFilter> list3 = new List<MeshFilter>();
			MeshFilter[] componentsInChildren = __instance.GetComponentsInChildren<MeshFilter>(includeInactive: true);
			foreach (MeshFilter meshFilter in componentsInChildren)
			{
				if (!MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.moreStageboxMats || (!smethod_4(smethod_3((UnityEngine.Object)meshFilter), global::_003CModule_003E.smethod_25<string>(1672622204u)) && !smethod_5((UnityEngine.Object)meshFilter.GetComponent<PrimitiveController>(), (UnityEngine.Object)null) && meshFilter.GetComponent<PrimitiveController>().HJEGNEIKDDO.a != XMUQ93RVvJVE5FPCbS_00248Hxs && meshFilter.GetComponent<PrimitiveController>().HJEGNEIKDDO.a != C5AcE8qkQZfZumQ__0024d8JU2A && meshFilter.GetComponent<PrimitiveController>().HJEGNEIKDDO.a != creNoS1TDqnabY284sL4AzA))
				{
					list3.Add(meshFilter);
				}
			}
			MeshFilter[] array = list3.ToArray();
			componentsInChildren = array;
			foreach (MeshFilter meshFilter2 in componentsInChildren)
			{
				if (!smethod_7((UnityEngine.Object)smethod_6(meshFilter2), (UnityEngine.Object)null))
				{
					continue;
				}
				CombineInstance item = new CombineInstance
				{
					mesh = smethod_6(meshFilter2),
					transform = matrix4x * meshFilter2.transform.localToWorldMatrix
				};
				item.mesh.tangents = null;
				if (!tracing)
				{
					list.Add(item);
				}
				else
				{
					PrimitiveController component = meshFilter2.GetComponent<PrimitiveController>();
					if (component != null && component.HJEGNEIKDDO.a < 0.5f)
					{
						list2.Add(item);
					}
					else
					{
						list.Add(item);
					}
				}
				meshFilter2.GetComponent<MeshRenderer>().enabled = false;
			}
			Material material = null;
			MeshRenderer[] componentsInChildren2 = __instance.GetComponentsInChildren<MeshRenderer>();
			for (int i = 0; i < componentsInChildren2.Length; i++)
			{
				material = componentsInChildren2[i].sharedMaterial;
				if (material.shader.name == global::_003CModule_003E.smethod_29<string>(1679924682u) || material.name.Equals(global::_003CModule_003E.smethod_29<string>(652143196u)))
				{
					break;
				}
			}
			if (material == null)
			{
				material = __instance.GetComponentInChildren<MeshRenderer>().sharedMaterial;
			}
			int count = list.Count;
			if (count > 2730)
			{
				int num = count / 2730 + 1;
				int num2 = count;
				int num3 = count / num + 1;
				for (int j = 0; j < num; j++)
				{
					Class17.DQGZpGELKqeKmXweFYGEcaXR73CqGywHQI6u2TATGL80(global::_003CModule_003E.smethod_25<string>(271155856u), __instance, material, list.GetRange(num3 * j, (j != num - 1) ? num3 : num2).ToArray(), j > 0);
					num2 -= num3;
				}
			}
			else
			{
				Class17.DQGZpGELKqeKmXweFYGEcaXR73CqGywHQI6u2TATGL80(global::_003CModule_003E.smethod_28<string>(3396454747u), __instance, material, list.ToArray(), false);
			}
			if (tracing)
			{
				Material material2 = MPatchr.n5wPFlpwFJrXE8uDgzL1YDc.LoadAsset<Material>(global::_003CModule_003E.smethod_27<string>(3892792254u));
				count = list2.Count;
				if (count <= 2730)
				{
					Class17.DQGZpGELKqeKmXweFYGEcaXR73CqGywHQI6u2TATGL80(global::_003CModule_003E.smethod_25<string>(271155856u), __instance, material2, list2.ToArray(), __instance.GetComponent<MeshFilter>() != null);
				}
				else
				{
					int num4 = count / 2730 + 1;
					int num5 = count;
					int num6 = count / num4 + 1;
					for (int k = 0; k < num4; k++)
					{
						Class17.DQGZpGELKqeKmXweFYGEcaXR73CqGywHQI6u2TATGL80(global::_003CModule_003E.smethod_26<string>(588072853u), __instance, material2, list2.GetRange(num6 * k, (k != num4 - 1) ? num6 : num5).ToArray(), k > 0 || __instance.GetComponent<MeshFilter>() != null);
						num5 -= num6;
					}
				}
			}
			for (int num7 = array.Length - 1; num7 >= 0; num7--)
			{
				array[num7].gameObject.layer = 20;
			}
			return false;
		}

		internal static Transform smethod_1(Component component_0)
		{
			return component_0.transform;
		}

		internal static Matrix4x4 smethod_2(Transform transform_0)
		{
			return transform_0.worldToLocalMatrix;
		}

		internal static string smethod_3(UnityEngine.Object object_0)
		{
			return object_0.name;
		}

		internal static bool smethod_4(string string_0, string string_1)
		{
			return string_0 == string_1;
		}

		internal static bool smethod_5(UnityEngine.Object object_0, UnityEngine.Object object_1)
		{
			return object_0 == object_1;
		}

		internal static Mesh smethod_6(MeshFilter meshFilter_0)
		{
			return meshFilter_0.mesh;
		}

		internal static bool smethod_7(UnityEngine.Object object_0, UnityEngine.Object object_1)
		{
			return object_0 != object_1;
		}
	}

	[HarmonyPatch("SetParam")]
	[HarmonyPatch(typeof(PrimitiveController))]
	internal static class Class40
	{
		[CompilerGenerated]
		private sealed class WaNnfPjp_YzH_0024_0024wQCLv5U3YObEBEFZaXN80SBATD3wa3R5E7ioVbcYQIOZUzGusjK8IZocoJT20oeY0Ex_N4Ndx2xmpKx6pU63Y9E9a2mhE_0024iKWOs8ASHX6Evu3HJhxwZS911m_0024_0OL6jEfOAa5E9jTl5qKDXCTbY0l9uelpmJ9lzT0yb8hbFQmWi3fArm48y3CsikQluSZek0QWz_zvJB2A5A5yJKFH1qyEBOEOU0hhCjg4wrUb4XxF_Elyjx8mclbApEfwyU2UfUhS9tga7Omo1vEufVk2J92E6JVHW8Mg
		{
			public Texture2D DhLWNLvykeMaRi3AJZn0iMs;

			internal void AzXv5n3b5Yj091z8G_xhDz0(AmMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F me)
			{
				if (smethod_0((UnityEngine.Object)pTbXl0BQalSWSABflrYhjNmk_A7RB7wKIBoc_0024LjdnXpzOQoqdduhV2bTbESRxybyAA._ksMqmG_0024wFoYcgR7FFZgQraIKCA58Zjx0aPJS7A87WYMf_YJQatyE__OuCCeaka_HN475HlADCiG3Iz_0024gQUJSblZTieLn7y2Ck5b1i9EHeNndh_00244L56uMXtY5UFYRbIC6w.SFOosMzb_XbDTwovJ3J28wg, (UnityEngine.Object)null))
				{
					smethod_1((Renderer)me.GetComponent<MeshRenderer>(), UnityEngine.Object.Instantiate(pTbXl0BQalSWSABflrYhjNmk_A7RB7wKIBoc_0024LjdnXpzOQoqdduhV2bTbESRxybyAA._ksMqmG_0024wFoYcgR7FFZgQraIKCA58Zjx0aPJS7A87WYMf_YJQatyE__OuCCeaka_HN475HlADCiG3Iz_0024gQUJSblZTieLn7y2Ck5b1i9EHeNndh_00244L56uMXtY5UFYRbIC6w.SFOosMzb_XbDTwovJ3J28wg));
					smethod_3(smethod_2((Renderer)me.GetComponent<MeshRenderer>()), (Texture)DhLWNLvykeMaRi3AJZn0iMs);
					smethod_4((UnityEngine.Object)me);
				}
			}

			internal static bool smethod_0(UnityEngine.Object object_0, UnityEngine.Object object_1)
			{
				return object_0 != object_1;
			}

			internal static void smethod_1(Renderer renderer_0, Material material_0)
			{
				renderer_0.material = material_0;
			}

			internal static Material smethod_2(Renderer renderer_0)
			{
				return renderer_0.material;
			}

			internal static void smethod_3(Material material_0, Texture texture_0)
			{
				material_0.mainTexture = texture_0;
			}

			internal static void smethod_4(UnityEngine.Object object_0)
			{
				UnityEngine.Object.Destroy(object_0);
			}
		}

		[HarmonyPostfix]
		internal static void FeUAVwFbW6wGJJdNimZY9yI(Color OPHBOKNDHPB, PrimitiveController __instance)
		{
			if (MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.tracing && smethod_3(smethod_2((UnityEngine.Object)smethod_1(smethod_0((Renderer)__instance.GetComponent<MeshRenderer>()))), global::_003CModule_003E.smethod_27<string>(3506570743u)))
			{
				if (__instance.HJEGNEIKDDO.a < 0.5f)
				{
					smethod_4((Renderer)__instance.GetComponent<MeshRenderer>(), MPatchr.n5wPFlpwFJrXE8uDgzL1YDc.LoadAsset<Material>(global::_003CModule_003E.smethod_29<string>(1257954615u)));
				}
				else if (__instance.HJEGNEIKDDO.a > 0.5f)
				{
					smethod_4((Renderer)__instance.GetComponent<MeshRenderer>(), MPatchr.n5wPFlpwFJrXE8uDgzL1YDc.LoadAsset<Material>(global::_003CModule_003E.smethod_28<string>(3669634450u)));
				}
			}
			if (!MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.moreStageboxMats || (smethod_7((UnityEngine.Object)smethod_6(smethod_5((Component)__instance)), (UnityEngine.Object)null) && smethod_8(smethod_2((UnityEngine.Object)smethod_6(smethod_5((Component)__instance))), global::_003CModule_003E.smethod_27<string>(2487498235u))) || (smethod_9((UnityEngine.Object)smethod_6(smethod_5((Component)__instance)), (UnityEngine.Object)null) && smethod_10().name == global::_003CModule_003E.smethod_26<string>(1668677614u) && __instance.gameObject.tag == global::_003CModule_003E.smethod_25<string>(3725568793u)))
			{
				return;
			}
			if (!zfXLeeKUOMpnTqAgpbUcX9g(OPHBOKNDHPB.a, XMUQ93RVvJVE5FPCbS_00248Hxs))
			{
				if (zfXLeeKUOMpnTqAgpbUcX9g(OPHBOKNDHPB.a, C5AcE8qkQZfZumQ__0024d8JU2A))
				{
					GameObject gameObject = __instance.gameObject.smethod_0(global::_003CModule_003E.smethod_26<string>(225641881u));
					if (gameObject == null)
					{
						gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
						gameObject.name = global::_003CModule_003E.smethod_28<string>(1575256727u);
						UnityEngine.Object.Destroy(gameObject.GetComponent<BoxCollider>());
						gameObject.transform.SetParent(__instance.transform);
						gameObject.transform.localScale = Vector3.one;
						gameObject.transform.localPosition = Vector3.zero;
						gameObject.transform.localRotation = Quaternion.identity;
						gameObject.layer = 20;
						gameObject.tag = global::_003CModule_003E.smethod_29<string>(1404674522u);
						MeshRenderer component = gameObject.GetComponent<MeshRenderer>();
						component.shadowCastingMode = ShadowCastingMode.Off;
						Texture2D DhLWNLvykeMaRi3AJZn0iMs = new Texture2D(1, 1);
						DhLWNLvykeMaRi3AJZn0iMs.SetPixel(0, 0, new Color32(0, byte.MaxValue, 0, byte.MaxValue));
						DhLWNLvykeMaRi3AJZn0iMs.Apply();
						if (pTbXl0BQalSWSABflrYhjNmk_A7RB7wKIBoc_0024LjdnXpzOQoqdduhV2bTbESRxybyAA._ksMqmG_0024wFoYcgR7FFZgQraIKCA58Zjx0aPJS7A87WYMf_YJQatyE__OuCCeaka_HN475HlADCiG3Iz_0024gQUJSblZTieLn7y2Ck5b1i9EHeNndh_00244L56uMXtY5UFYRbIC6w.SFOosMzb_XbDTwovJ3J28wg == null)
						{
							gameObject.AddComponent<AmMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F>().U_0024eIoX6e3N9Ag_us_EcGHBI(delegate(AmMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F me)
							{
								if (WaNnfPjp_YzH_0024_0024wQCLv5U3YObEBEFZaXN80SBATD3wa3R5E7ioVbcYQIOZUzGusjK8IZocoJT20oeY0Ex_N4Ndx2xmpKx6pU63Y9E9a2mhE_0024iKWOs8ASHX6Evu3HJhxwZS911m_0024_0OL6jEfOAa5E9jTl5qKDXCTbY0l9uelpmJ9lzT0yb8hbFQmWi3fArm48y3CsikQluSZek0QWz_zvJB2A5A5yJKFH1qyEBOEOU0hhCjg4wrUb4XxF_Elyjx8mclbApEfwyU2UfUhS9tga7Omo1vEufVk2J92E6JVHW8Mg.smethod_0((UnityEngine.Object)pTbXl0BQalSWSABflrYhjNmk_A7RB7wKIBoc_0024LjdnXpzOQoqdduhV2bTbESRxybyAA._ksMqmG_0024wFoYcgR7FFZgQraIKCA58Zjx0aPJS7A87WYMf_YJQatyE__OuCCeaka_HN475HlADCiG3Iz_0024gQUJSblZTieLn7y2Ck5b1i9EHeNndh_00244L56uMXtY5UFYRbIC6w.SFOosMzb_XbDTwovJ3J28wg, (UnityEngine.Object)null))
								{
									WaNnfPjp_YzH_0024_0024wQCLv5U3YObEBEFZaXN80SBATD3wa3R5E7ioVbcYQIOZUzGusjK8IZocoJT20oeY0Ex_N4Ndx2xmpKx6pU63Y9E9a2mhE_0024iKWOs8ASHX6Evu3HJhxwZS911m_0024_0OL6jEfOAa5E9jTl5qKDXCTbY0l9uelpmJ9lzT0yb8hbFQmWi3fArm48y3CsikQluSZek0QWz_zvJB2A5A5yJKFH1qyEBOEOU0hhCjg4wrUb4XxF_Elyjx8mclbApEfwyU2UfUhS9tga7Omo1vEufVk2J92E6JVHW8Mg.smethod_1((Renderer)me.GetComponent<MeshRenderer>(), UnityEngine.Object.Instantiate(pTbXl0BQalSWSABflrYhjNmk_A7RB7wKIBoc_0024LjdnXpzOQoqdduhV2bTbESRxybyAA._ksMqmG_0024wFoYcgR7FFZgQraIKCA58Zjx0aPJS7A87WYMf_YJQatyE__OuCCeaka_HN475HlADCiG3Iz_0024gQUJSblZTieLn7y2Ck5b1i9EHeNndh_00244L56uMXtY5UFYRbIC6w.SFOosMzb_XbDTwovJ3J28wg));
									WaNnfPjp_YzH_0024_0024wQCLv5U3YObEBEFZaXN80SBATD3wa3R5E7ioVbcYQIOZUzGusjK8IZocoJT20oeY0Ex_N4Ndx2xmpKx6pU63Y9E9a2mhE_0024iKWOs8ASHX6Evu3HJhxwZS911m_0024_0OL6jEfOAa5E9jTl5qKDXCTbY0l9uelpmJ9lzT0yb8hbFQmWi3fArm48y3CsikQluSZek0QWz_zvJB2A5A5yJKFH1qyEBOEOU0hhCjg4wrUb4XxF_Elyjx8mclbApEfwyU2UfUhS9tga7Omo1vEufVk2J92E6JVHW8Mg.smethod_3(WaNnfPjp_YzH_0024_0024wQCLv5U3YObEBEFZaXN80SBATD3wa3R5E7ioVbcYQIOZUzGusjK8IZocoJT20oeY0Ex_N4Ndx2xmpKx6pU63Y9E9a2mhE_0024iKWOs8ASHX6Evu3HJhxwZS911m_0024_0OL6jEfOAa5E9jTl5qKDXCTbY0l9uelpmJ9lzT0yb8hbFQmWi3fArm48y3CsikQluSZek0QWz_zvJB2A5A5yJKFH1qyEBOEOU0hhCjg4wrUb4XxF_Elyjx8mclbApEfwyU2UfUhS9tga7Omo1vEufVk2J92E6JVHW8Mg.smethod_2((Renderer)me.GetComponent<MeshRenderer>()), (Texture)DhLWNLvykeMaRi3AJZn0iMs);
									WaNnfPjp_YzH_0024_0024wQCLv5U3YObEBEFZaXN80SBATD3wa3R5E7ioVbcYQIOZUzGusjK8IZocoJT20oeY0Ex_N4Ndx2xmpKx6pU63Y9E9a2mhE_0024iKWOs8ASHX6Evu3HJhxwZS911m_0024_0OL6jEfOAa5E9jTl5qKDXCTbY0l9uelpmJ9lzT0yb8hbFQmWi3fArm48y3CsikQluSZek0QWz_zvJB2A5A5yJKFH1qyEBOEOU0hhCjg4wrUb4XxF_Elyjx8mclbApEfwyU2UfUhS9tga7Omo1vEufVk2J92E6JVHW8Mg.smethod_4((UnityEngine.Object)me);
								}
							});
						}
						else
						{
							component.material = UnityEngine.Object.Instantiate(pTbXl0BQalSWSABflrYhjNmk_A7RB7wKIBoc_0024LjdnXpzOQoqdduhV2bTbESRxybyAA._ksMqmG_0024wFoYcgR7FFZgQraIKCA58Zjx0aPJS7A87WYMf_YJQatyE__OuCCeaka_HN475HlADCiG3Iz_0024gQUJSblZTieLn7y2Ck5b1i9EHeNndh_00244L56uMXtY5UFYRbIC6w.SFOosMzb_XbDTwovJ3J28wg);
						}
						component.lightProbeUsage = LightProbeUsage.Off;
						component.reflectionProbeUsage = ReflectionProbeUsage.BlendProbesAndSkybox;
						component.material.mainTexture = DhLWNLvykeMaRi3AJZn0iMs;
						component.gameObject.layer = 9;
						if (SceneManager.GetActiveScene().name == global::_003CModule_003E.smethod_29<string>(1545398678u))
						{
							WorldTable worldTable = Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<WorldTable>(global::_003CModule_003E.smethod_27<string>(3306471932u), SceneMan.JFAOKFIDAGK as Construct);
							__instance.GetComponent<MeshRenderer>().material = worldTable.APOALNMPNPP.GetComponent<MeshRenderer>().material;
						}
					}
					gameObject.SetActive(value: true);
					__instance.GetComponent<MeshRenderer>().enabled = false;
				}
				else if (!zfXLeeKUOMpnTqAgpbUcX9g(OPHBOKNDHPB.a, creNoS1TDqnabY284sL4AzA))
				{
					if (SceneManager.GetActiveScene().name == global::_003CModule_003E.smethod_28<string>(1060238668u))
					{
						WorldTable worldTable2 = Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<WorldTable>(global::_003CModule_003E.smethod_25<string>(1664684590u), SceneMan.JFAOKFIDAGK as Construct);
						__instance.GetComponent<MeshRenderer>().material = worldTable2.APOALNMPNPP.GetComponent<MeshRenderer>().material;
					}
					if (zfXLeeKUOMpnTqAgpbUcX9g(OPHBOKNDHPB.a, dzadJ16eTlgbYulPr_znFoE))
					{
						__instance.GetComponent<MeshRenderer>().material.mainTexture = BWHIL5wdnR4xjEFCXwNroigNrxAykiqlFsHJMrMbU3pw.oPoOeyx7nfZcVVhd8CSfgNA(KH64M3TewhA_Yw2TjbFeRV_kcghAaUSHXuDlWZpBfh_lhm_0024Y_0024No6GYBMPMIyT_0024M8ZQ.Z9C_hZjfRJLQVSS2_BdBFww);
					}
					__instance.GetComponent<MeshRenderer>().enabled = true;
					if (__instance.gameObject.smethod_0(global::_003CModule_003E.smethod_25<string>(1672622204u)) != null)
					{
						__instance.gameObject.smethod_0(global::_003CModule_003E.smethod_25<string>(1672622204u)).SetActive(value: false);
					}
				}
				else
				{
					WorldTable worldTable3 = Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<WorldTable>(global::_003CModule_003E.smethod_26<string>(713063455u), SceneMan.JFAOKFIDAGK as Arena);
					__instance.GetComponent<MeshRenderer>().material = worldTable3.KFFEBBNPAJF.GetComponentInChildren<MeshRenderer>().material;
					__instance.GetComponent<MeshRenderer>().material.SetColor(global::_003CModule_003E.smethod_27<string>(1761585888u), new Color(OPHBOKNDHPB.r, OPHBOKNDHPB.g, OPHBOKNDHPB.b, 0.753f));
					__instance.GetComponent<BoxCollider>().isTrigger = true;
					if (__instance.gameObject.smethod_0(global::_003CModule_003E.smethod_26<string>(225641881u)) != null)
					{
						__instance.gameObject.smethod_0(global::_003CModule_003E.smethod_29<string>(2567049479u)).SetActive(value: false);
					}
					__instance.GetComponent<MeshRenderer>().enabled = true;
				}
			}
			else
			{
				__instance.GetComponent<MeshRenderer>().material = new Material(Shader.Find(global::_003CModule_003E.smethod_28<string>(180536384u)));
				__instance.GetComponent<MeshRenderer>().material.EnableKeyword(global::_003CModule_003E.smethod_27<string>(4064431588u));
				__instance.GetComponent<MeshRenderer>().material.SetColor(global::_003CModule_003E.smethod_25<string>(2243297687u), OPHBOKNDHPB);
				__instance.GetComponent<MeshRenderer>().material.color = OPHBOKNDHPB;
				__instance.GetComponent<MeshRenderer>().enabled = true;
				if (__instance.gameObject.smethod_0(global::_003CModule_003E.smethod_27<string>(2008215374u)) != null)
				{
					__instance.gameObject.smethod_0(global::_003CModule_003E.smethod_29<string>(2567049479u)).SetActive(value: false);
				}
			}
			__instance.gameObject.layer = (RABTF797cxk849f9Wpnefo0(OPHBOKNDHPB.a) ? UDIRjB3VSmwHpVmUxcnHH66kk6ThWzbxmySWu7OngspoKs_47sbCTTGVAxk9y7DBfQ.fhrfRxVubQ7jQHGH5tZ1Xn4 : 20);
			__instance.GetComponent<BoxCollider>().isTrigger = RABTF797cxk849f9Wpnefo0(OPHBOKNDHPB.a);
		}

		internal static Material smethod_0(Renderer renderer_0)
		{
			return renderer_0.material;
		}

		internal static Shader smethod_1(Material material_0)
		{
			return material_0.shader;
		}

		internal static string smethod_2(UnityEngine.Object object_0)
		{
			return object_0.name;
		}

		internal static bool smethod_3(string string_0, string string_1)
		{
			return string_0.Contains(string_1);
		}

		internal static void smethod_4(Renderer renderer_0, Material material_0)
		{
			renderer_0.material = material_0;
		}

		internal static Transform smethod_5(Component component_0)
		{
			return component_0.transform;
		}

		internal static Transform smethod_6(Transform transform_0)
		{
			return transform_0.parent;
		}

		internal static bool smethod_7(UnityEngine.Object object_0, UnityEngine.Object object_1)
		{
			return object_0 != object_1;
		}

		internal static bool smethod_8(string string_0, string string_1)
		{
			return string_0 == string_1;
		}

		internal static bool smethod_9(UnityEngine.Object object_0, UnityEngine.Object object_1)
		{
			return object_0 == object_1;
		}

		internal static Scene smethod_10()
		{
			return SceneManager.GetActiveScene();
		}
	}

	[HarmonyPatch(typeof(PrimitiveController))]
	[HarmonyPatch("SetColor")]
	internal static class D_wOoOtOlIgcgktWR5FkIGrx3gk_0024RFfquiBdHerTG8cv_cUKX27mxGzjKcGhN7jKZndlHnPU26qcsDqaoAfuFj2b4_BxxVsM58X2QVDtb9cbxoms7NCgVKZFSYxuOX93nexDX3gy7pfpNl1KxdB5Yoc
	{
		[HarmonyPostfix]
		internal static void FeUAVwFbW6wGJJdNimZY9yI(Color OPHBOKNDHPB, PrimitiveController __instance)
		{
			Class40.FeUAVwFbW6wGJJdNimZY9yI(OPHBOKNDHPB, __instance);
		}
	}

	[HarmonyPatch("Update")]
	[HarmonyPatch(typeof(PrimitiveController))]
	internal static class l5JX6nBieZEhKSPiShCrEwh7hxzumDvDl6w7GPMTOtkkisjdrIASl6hb7ebIhj19cc_0024niXHIRlOhQdTq5pWUdqjtJBx72FsGO5KZPhrLGTV3vN_0024yiYkRfVVKVfuC2tp8I4mOQrCQDhMKpj5QPo9hk18
	{
		[HarmonyPostfix]
		internal static void FeUAVwFbW6wGJJdNimZY9yI(PrimitiveController __instance)
		{
			if (__instance.NPFBKGKPPAG == 0 && __instance.EINBJDHFLOL == -2 && __instance.i0duGIJ6M7BXC7c2cbeyvXM1hCrcoNKNJzlqDrhwjoxM<int>(global::_003CModule_003E.smethod_26<string>(3724960999u)) == 0)
			{
				smethod_2((Collider)__instance.GetComponent<BoxCollider>(), smethod_1(smethod_0((Component)__instance)) == UDIRjB3VSmwHpVmUxcnHH66kk6ThWzbxmySWu7OngspoKs_47sbCTTGVAxk9y7DBfQ.fhrfRxVubQ7jQHGH5tZ1Xn4);
			}
		}

		internal static GameObject smethod_0(Component component_0)
		{
			return component_0.gameObject;
		}

		internal static int smethod_1(GameObject gameObject_0)
		{
			return gameObject_0.layer;
		}

		internal static void smethod_2(Collider collider_0, bool bool_0)
		{
			collider_0.isTrigger = bool_0;
		}
	}

	[HarmonyPatch(typeof(Construct))]
	[HarmonyPatch("Start")]
	internal static class drGKDPlVNm06T6fgsX2kJFCo2xHXzX8YWj4uxnNHFyFJF_0024C_0024RPEXrWmetvDxlbSqKAe_0024lmq1NIv4vOXFKCeSERWoRyqm12q_7mlFZN0DcrW3
	{
		[HarmonyPostfix]
		internal static void FeUAVwFbW6wGJJdNimZY9yI(Construct __instance)
		{
			if (!MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.constructPlus)
			{
				Dictionary<string, GameObject> dictionary = Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<Dictionary<string, GameObject>>(global::_003CModule_003E.smethod_29<string>(1550370693u), SceneMan.JFAOKFIDAGK);
				dictionary.Remove(global::_003CModule_003E.smethod_29<string>(2296838801u));
				dictionary.Remove(global::_003CModule_003E.smethod_25<string>(3024835619u));
				smethod_1(smethod_0((SceneMan)__instance, global::_003CModule_003E.smethod_26<string>(2881378870u)).GetComponent<ListController>(), new string[5]
				{
					global::_003CModule_003E.smethod_28<string>(3062519041u),
					global::_003CModule_003E.smethod_29<string>(1409714004u),
					global::_003CModule_003E.smethod_27<string>(2627090260u),
					global::_003CModule_003E.smethod_29<string>(2902650220u),
					global::_003CModule_003E.smethod_29<string>(2761993531u)
				});
			}
		}

		internal static GameObject smethod_0(SceneMan sceneMan_0, string string_0)
		{
			return sceneMan_0.GetLST(string_0);
		}

		internal static void smethod_1(ListController listController_0, string[] string_0)
		{
			listController_0.SetItems(string_0);
		}
	}

	[HarmonyPatch(typeof(Meeting))]
	[HarmonyPatch("Start")]
	internal static class Class41
	{
		[HarmonyPostfix]
		internal static void FeUAVwFbW6wGJJdNimZY9yI(Meeting __instance)
		{
			Dictionary<string, GameObject> dictionary = Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<Dictionary<string, GameObject>>(global::_003CModule_003E.smethod_27<string>(2440967560u), SceneMan.JFAOKFIDAGK);
			dictionary.Remove(global::_003CModule_003E.smethod_25<string>(1227718558u));
			dictionary.Remove(global::_003CModule_003E.smethod_25<string>(3024835619u));
			smethod_1(smethod_0((SceneMan)__instance, global::_003CModule_003E.smethod_27<string>(4032384279u)).GetComponent<ListController>(), new string[5]
			{
				global::_003CModule_003E.smethod_27<string>(896081516u),
				global::_003CModule_003E.smethod_27<string>(509860005u),
				global::_003CModule_003E.smethod_26<string>(1996133207u),
				global::_003CModule_003E.smethod_29<string>(2902650220u),
				global::_003CModule_003E.smethod_28<string>(406629963u)
			});
		}

		internal static GameObject smethod_0(SceneMan sceneMan_0, string string_0)
		{
			return sceneMan_0.GetLST(string_0);
		}

		internal static void smethod_1(ListController listController_0, string[] string_0)
		{
			listController_0.SetItems(string_0);
		}
	}

	[HarmonyPatch(new Type[] { typeof(GameObject) })]
	[HarmonyPatch("OnSelect")]
	[HarmonyPatch(typeof(SceneMan))]
	internal static class k1jjXoMSLucngJadkqOae4tqtDtA_Ar5NPUDWU_JEmoZU_0024pnOX7WSTZMWE6fXgq0UrfPf_vgGAV45LYNMO1ZqHoX5t_0024vlyoMqNjsGeJnej1_DqX81cfpx02j6izeMrnBkJtRG7IAOXJcu6xEml6MYr4
	{
		[HarmonyPostfix]
		internal static void FeUAVwFbW6wGJJdNimZY9yI(GameObject NGLBLAGMBLN, SceneMan __instance)
		{
			string name = smethod_0().name;
			if (!(name != global::_003CModule_003E.smethod_28<string>(1060238668u)) || !(name != global::_003CModule_003E.smethod_27<string>(3421699567u)))
			{
				string widgetName = SceneMan.GetWidgetName(NGLBLAGMBLN);
				string selectedItem = NGLBLAGMBLN.GetComponent<ListController>().GetSelectedItem();
				if (widgetName == global::_003CModule_003E.smethod_26<string>(2881378870u) && selectedItem == global::_003CModule_003E.smethod_28<string>(952989369u))
				{
					Color color = Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<Color>(global::_003CModule_003E.smethod_25<string>(4253103077u), __instance);
					Class17.DMoImhPC2jNUt2tgmSezeT_IK_0024XK5Df9PmexG_CaKb1R(gparam_1: new Color(color.r, color.g, color.b, XMUQ93RVvJVE5FPCbS_00248Hxs), string_0: global::_003CModule_003E.smethod_29<string>(1874868734u), gparam_0: __instance);
				}
				else if (widgetName == global::_003CModule_003E.smethod_29<string>(3930431706u) && selectedItem == global::_003CModule_003E.smethod_27<string>(2240868749u))
				{
					Color color2 = Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<Color>(global::_003CModule_003E.smethod_29<string>(1874868734u), __instance);
					Class17.DMoImhPC2jNUt2tgmSezeT_IK_0024XK5Df9PmexG_CaKb1R(gparam_1: new Color(color2.r, color2.g, color2.b, C5AcE8qkQZfZumQ__0024d8JU2A), string_0: global::_003CModule_003E.smethod_28<string>(133450260u), gparam_0: __instance);
				}
				else if (widgetName == global::_003CModule_003E.smethod_28<string>(2607219536u) && selectedItem == global::_003CModule_003E.smethod_26<string>(2316900645u))
				{
					Color color3 = Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<Color>(global::_003CModule_003E.smethod_29<string>(1874868734u), __instance);
					Class17.DMoImhPC2jNUt2tgmSezeT_IK_0024XK5Df9PmexG_CaKb1R(gparam_1: new Color(color3.r, color3.g, color3.b, creNoS1TDqnabY284sL4AzA), string_0: global::_003CModule_003E.smethod_25<string>(4253103077u), gparam_0: __instance);
				}
			}
		}

		internal static Scene smethod_0()
		{
			return SceneManager.GetActiveScene();
		}
	}

	internal static readonly float XMUQ93RVvJVE5FPCbS_00248Hxs = 0.99607843f;

	internal static readonly float C5AcE8qkQZfZumQ__0024d8JU2A = 0.99215686f;

	internal static readonly float creNoS1TDqnabY284sL4AzA = 84f / 85f;

	internal static readonly float dzadJ16eTlgbYulPr_znFoE = 0.9843137f;

	internal static readonly float E0qFtJrh2PPIzR_pbXWODoSDhOHOeOJcsHC1iizj7E89 = XMUQ93RVvJVE5FPCbS_00248Hxs - 4f / 51f;

	internal static readonly float oGEosfaxANuCCZXi3Vne9lOBAYJ6X6XL1XRJCPvTG2Fh = C5AcE8qkQZfZumQ__0024d8JU2A - 4f / 51f;

	internal static readonly float B2gy4EQ_0024rse8B688fgpTYCHmCrTZTQmI9hVADwlN39jP = creNoS1TDqnabY284sL4AzA - 4f / 51f;

	internal static readonly float Nl_0024s1Ikb0Gxr_0024DHRiZgj188M1Nk9nGWbEJk0eNNGJY5M = dzadJ16eTlgbYulPr_znFoE - 4f / 51f;

	internal static bool RABTF797cxk849f9Wpnefo0(float alpha, bool includeOnlyDummyWater = false)
	{
		if ((alpha != creNoS1TDqnabY284sL4AzA || includeOnlyDummyWater) && alpha != E0qFtJrh2PPIzR_pbXWODoSDhOHOeOJcsHC1iizj7E89 && alpha != oGEosfaxANuCCZXi3Vne9lOBAYJ6X6XL1XRJCPvTG2Fh && alpha != Nl_0024s1Ikb0Gxr_0024DHRiZgj188M1Nk9nGWbEJk0eNNGJY5M)
		{
			return alpha == B2gy4EQ_0024rse8B688fgpTYCHmCrTZTQmI9hVADwlN39jP && includeOnlyDummyWater;
		}
		return true;
	}

	public static bool Swgk_JzbbuzLTqRsNhZ8Jvv1iEmGt9G2BhAu6b3jzU7q(float a, float b, float epsilon)
	{
		float num = smethod_0(a);
		float num2 = smethod_0(b);
		float num3 = smethod_0(a - b);
		if (a == b)
		{
			return true;
		}
		if (a != 0f && b != 0f && num3 >= 1.1754944E-38f)
		{
			return num3 / smethod_1(num + num2, float.MaxValue) < epsilon;
		}
		return num3 < epsilon * 1.1754944E-38f;
	}

	internal static bool zfXLeeKUOMpnTqAgpbUcX9g(float alpha, float nonDummyCompare)
	{
		if (alpha != nonDummyCompare)
		{
			return Swgk_JzbbuzLTqRsNhZ8Jvv1iEmGt9G2BhAu6b3jzU7q(alpha + 4f / 51f, nonDummyCompare, 0.001f);
		}
		return true;
	}

	internal static float smethod_0(float float_0)
	{
		return Math.Abs(float_0);
	}

	internal static float smethod_1(float float_0, float float_1)
	{
		return Math.Min(float_0, float_1);
	}
}
