using System.Collections.Generic;
using HarmonyLib;
using MPatchrMain;
using McnCraft;
using UnityEngine;

internal class j11cGBDYP8UYQEHVTkChdIOmgQumIJyS_qg07j_tssO5oe12WhiMFv9DLxz68nMrQg
{
	[HarmonyPatch("Awake")]
	[HarmonyPatch(typeof(BulletController))]
	internal class exQgSGVqTUVXODHL2vUf2jUBoIsVYD9gr0MdsEjIk1Xjtbvd8WHkDq8fHFO8oB78KpcP8DmDYSyIZczZnFIILsAwawUiawVhlr6b_2lhPBmYzBb0QYhfmdSRPc5JBC_mKA
	{
		internal class sHyrD5GTL_oVtozT_00243fo_19GBnMshE0TlYGJQDZiOMo5GteC5zRu4yGxNivqwZPV3djFPNIiytbDOsD74NeDtqEJvcjnGto58gXrkQL9WxqSL69XY1bSsKtEmFi2qd0RxSIP8bbVoYdqWAn54rm6rNIL_Bd3DY01b8tV1vcGJEzzZhGVob0EdSC_udSNm3qMURP9JbulSCxgH_00240xUXD4m2qaesxMDOsTU_0024ti_vkqD4V4i9ZTbtipjKuumtP0WXa_0024WibOPiquG7JE3Nmi8vI7y2v6GXfko3xgafOchUcGvRKQ : MonoBehaviour
		{
			internal BulletController rIE_0024tWtTCQTa9P7fSb2vmoA;

			internal Light light_0;

			public void Update()
			{
				if (MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.graphicsPlus)
				{
					if (light_0 == null)
					{
						light_0 = rIE_0024tWtTCQTa9P7fSb2vmoA.gameObject.AddComponent<Light>();
						light_0.type = LightType.Point;
						light_0.range = 20f;
						light_0.intensity = 5f;
						light_0.renderMode = LightRenderMode.ForcePixel;
					}
					light_0.color = rIE_0024tWtTCQTa9P7fSb2vmoA.BJLNMOHHHCL;
				}
			}
		}

		internal BulletController rIE_0024tWtTCQTa9P7fSb2vmoA;

		internal Light light_0;

		[HarmonyPostfix]
		internal static void FeUAVwFbW6wGJJdNimZY9yI(BulletController __instance)
		{
			if (MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.graphicsPlus)
			{
				smethod_0((Component)__instance).AddComponent<sHyrD5GTL_oVtozT_00243fo_19GBnMshE0TlYGJQDZiOMo5GteC5zRu4yGxNivqwZPV3djFPNIiytbDOsD74NeDtqEJvcjnGto58gXrkQL9WxqSL69XY1bSsKtEmFi2qd0RxSIP8bbVoYdqWAn54rm6rNIL_Bd3DY01b8tV1vcGJEzzZhGVob0EdSC_udSNm3qMURP9JbulSCxgH_00240xUXD4m2qaesxMDOsTU_0024ti_vkqD4V4i9ZTbtipjKuumtP0WXa_0024WibOPiquG7JE3Nmi8vI7y2v6GXfko3xgafOchUcGvRKQ>().rIE_0024tWtTCQTa9P7fSb2vmoA = __instance;
			}
		}

		internal static GameObject smethod_0(Component component_0)
		{
			return component_0.gameObject;
		}
	}

	[HarmonyPatch(typeof(ShellController))]
	[HarmonyPatch("Awake")]
	internal class Class43
	{
		internal class lNpIzpGWMiPQ_5gAwjtlI6RZkqrbpopOXKZi_qBDVqJLMCrxiI6JGkm9SqUjOVgqLs0JjEaROMcik94h8dzdyUpp29oVdVmChEDxz4qSwDNaemqtx7JuJzFIGOCHPuOYpA : MonoBehaviour
		{
			internal ShellController rIE_0024tWtTCQTa9P7fSb2vmoA;

			internal Light light_0;

			public void Update()
			{
				if (MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.graphicsPlus)
				{
					if (light_0 == null)
					{
						light_0 = rIE_0024tWtTCQTa9P7fSb2vmoA.gameObject.AddComponent<Light>();
						light_0.type = LightType.Point;
						light_0.range = 20f;
						light_0.intensity = 5f;
						light_0.renderMode = LightRenderMode.ForcePixel;
					}
					light_0.color = rIE_0024tWtTCQTa9P7fSb2vmoA.BJLNMOHHHCL;
				}
			}
		}

		[HarmonyPostfix]
		internal static void FeUAVwFbW6wGJJdNimZY9yI(ShellController __instance)
		{
			if (MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.graphicsPlus)
			{
				smethod_0((Component)__instance).AddComponent<lNpIzpGWMiPQ_5gAwjtlI6RZkqrbpopOXKZi_qBDVqJLMCrxiI6JGkm9SqUjOVgqLs0JjEaROMcik94h8dzdyUpp29oVdVmChEDxz4qSwDNaemqtx7JuJzFIGOCHPuOYpA>().rIE_0024tWtTCQTa9P7fSb2vmoA = __instance;
			}
		}

		internal static GameObject smethod_0(Component component_0)
		{
			return component_0.gameObject;
		}
	}

	[HarmonyPatch("Awake")]
	[HarmonyPatch(typeof(PlasmaController))]
	internal class XBS7OSIVgwiqp2YsikrDtq4BQmpUvr84rfCKNKcGbAc9UfXB8jDM3L0Wpz7fZcn_EGBZDh_0024Z94v9pz54Cd0SD2U
	{
		internal class Class44 : MonoBehaviour
		{
			internal PlasmaController rIE_0024tWtTCQTa9P7fSb2vmoA;

			internal Light light_0;

			public void Update()
			{
				if (MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.graphicsPlus)
				{
					if (light_0 == null)
					{
						light_0 = rIE_0024tWtTCQTa9P7fSb2vmoA.gameObject.AddComponent<Light>();
						light_0.type = LightType.Point;
						light_0.range = 15f;
						light_0.intensity = 3f;
						light_0.renderMode = LightRenderMode.ForcePixel;
					}
					light_0.intensity = rIE_0024tWtTCQTa9P7fSb2vmoA.transform.localScale.x;
					light_0.color = rIE_0024tWtTCQTa9P7fSb2vmoA.BJLNMOHHHCL;
				}
			}
		}

		[HarmonyPostfix]
		internal static void FeUAVwFbW6wGJJdNimZY9yI(PlasmaController __instance)
		{
			if (MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.graphicsPlus)
			{
				smethod_0((Component)__instance).AddComponent<Class44>().rIE_0024tWtTCQTa9P7fSb2vmoA = __instance;
			}
		}

		internal static GameObject smethod_0(Component component_0)
		{
			return component_0.gameObject;
		}
	}

	[HarmonyPatch("Update")]
	[HarmonyPatch(typeof(LampController))]
	internal class Class45
	{
		[HarmonyPrefix]
		internal static void smethod_0(LampController __instance, ref float __state)
		{
			if (MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.graphicsPlus)
			{
				__state = Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<float>(global::_003CModule_003E.smethod_27<string>(1805242118u), __instance);
			}
		}

		[HarmonyPostfix]
		internal static void FeUAVwFbW6wGJJdNimZY9yI(LampController __instance, ref float __state)
		{
			if (!MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.graphicsPlus || Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<float>(global::_003CModule_003E.smethod_25<string>(1316063668u), __instance) == __state)
			{
				return;
			}
			GameObject gameObject = Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<GameObject>(global::_003CModule_003E.smethod_26<string>(2113877221u), __instance);
			GameObject gameObject2 = gameObject.smethod_0(global::_003CModule_003E.smethod_29<string>(2105370759u));
			Light light;
			if (!smethod_1((Object)gameObject2, (Object)null))
			{
				light = gameObject2.GetComponent<Light>();
			}
			else
			{
				gameObject2 = smethod_2(global::_003CModule_003E.smethod_25<string>(3682030332u));
				smethod_4(smethod_3(gameObject2), smethod_3(gameObject));
				smethod_5(smethod_3(gameObject2), Vector3.zero);
				smethod_6(smethod_3(gameObject2), Quaternion.Euler(-90f, 0f, 0f));
				smethod_7(smethod_3(gameObject2), Vector3.one);
				light = gameObject2.AddComponent<Light>();
				smethod_8(light, LightType.Point);
				smethod_9(light, 50f);
				smethod_10(light, 1f);
				smethod_11(light, LightRenderMode.ForcePixel);
			}
			if (MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4 != null)
			{
				Dictionary<string, object> idictionary_ = oIuuMMCo9UmUIWmYFQEqxXAOJy8LYqspmW2FKyHMiXKdcT1cBP8p9QxEsmrfao0MIw.smethod_2(__instance);
				smethod_8(light, (!(bool)idictionary_.Y_0024dZy3YTHmOYBqFB7lE7fD_0024vb6NsuWzjnbcG6_CoYN52(global::_003CModule_003E.smethod_26<string>(949527654u), false)) ? LightType.Point : LightType.Spot);
				smethod_10(light, (float)(int)idictionary_.Y_0024dZy3YTHmOYBqFB7lE7fD_0024vb6NsuWzjnbcG6_CoYN52(global::_003CModule_003E.smethod_25<string>(1711714381u), 100) / 100f);
				smethod_9(light, (float)(int)idictionary_.Y_0024dZy3YTHmOYBqFB7lE7fD_0024vb6NsuWzjnbcG6_CoYN52(global::_003CModule_003E.smethod_25<string>(2676214697u), 50));
				MeshRenderer component = gameObject.GetComponent<MeshRenderer>();
				if (smethod_12((Renderer)component) && (bool)idictionary_.Y_0024dZy3YTHmOYBqFB7lE7fD_0024vb6NsuWzjnbcG6_CoYN52(global::_003CModule_003E.smethod_25<string>(615330494u), false))
				{
					smethod_13((Renderer)component, bool_0: false);
				}
			}
			float num = Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<float>(global::_003CModule_003E.smethod_29<string>(3316993597u), gameObject.GetComponent<BulbController>());
			if (num == 0f)
			{
				smethod_14((Behaviour)light, bool_0: false);
				return;
			}
			smethod_14((Behaviour)light, bool_0: true);
			smethod_16(light, smethod_15(num));
		}

		internal static bool smethod_1(Object object_0, Object object_1)
		{
			return object_0 == object_1;
		}

		internal static GameObject smethod_2(string string_0)
		{
			return new GameObject(string_0);
		}

		internal static Transform smethod_3(GameObject gameObject_0)
		{
			return gameObject_0.transform;
		}

		internal static void smethod_4(Transform transform_0, Transform transform_1)
		{
			transform_0.SetParent(transform_1);
		}

		internal static void smethod_5(Transform transform_0, Vector3 vector3_0)
		{
			transform_0.localPosition = vector3_0;
		}

		internal static void smethod_6(Transform transform_0, Quaternion quaternion_0)
		{
			transform_0.localRotation = quaternion_0;
		}

		internal static void smethod_7(Transform transform_0, Vector3 vector3_0)
		{
			transform_0.localScale = vector3_0;
		}

		internal static void smethod_8(Light light_0, LightType lightType_0)
		{
			light_0.type = lightType_0;
		}

		internal static void smethod_9(Light light_0, float float_0)
		{
			light_0.range = float_0;
		}

		internal static void smethod_10(Light light_0, float float_0)
		{
			light_0.intensity = float_0;
		}

		internal static void smethod_11(Light light_0, LightRenderMode lightRenderMode_0)
		{
			light_0.renderMode = lightRenderMode_0;
		}

		internal static bool smethod_12(Renderer renderer_0)
		{
			return renderer_0.enabled;
		}

		internal static void smethod_13(Renderer renderer_0, bool bool_0)
		{
			renderer_0.enabled = bool_0;
		}

		internal static void smethod_14(Behaviour behaviour_0, bool bool_0)
		{
			behaviour_0.enabled = bool_0;
		}

		internal static Color smethod_15(float float_0)
		{
			return FJLJNEKHKKH.ENEKHPEBJEH(float_0);
		}

		internal static void smethod_16(Light light_0, Color color_0)
		{
			light_0.color = color_0;
		}
	}

	[HarmonyPatch(typeof(EmitterController))]
	[HarmonyPatch("CreateDecor")]
	internal class UDIRjB3VSmwHpVmUxcnHH65yPcxd5H6_00246yqnbce0mrXAqs8qxP9wsqVz0g8In_0024HEOhehd6xMECMcOiGbfmO6MRA
	{
		[HarmonyPostfix]
		internal static void FeUAVwFbW6wGJJdNimZY9yI(EmitterController __instance, MachineController CIPOPAGDJDE)
		{
			if (!MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.graphicsPlus)
			{
				return;
			}
			if (!(smethod_0(__instance.JNKEKNOAPHO).a < 0.49f))
			{
				ParticleSystem component = CIPOPAGDJDE.NKGOJFOGPFM.GetComponent<ParticleSystem>();
				ParticleSystem component2 = CIPOPAGDJDE.APGHCJHAHKE.GetComponent<ParticleSystem>();
				ParticleSystem.LightsModule lightsModule = smethod_1(component);
				lightsModule.enabled = true;
				lightsModule.light = new GameObject(global::_003CModule_003E.smethod_29<string>(1651318729u)).AddComponent<Light>();
				lightsModule.light.gameObject.SetActive(value: false);
				lightsModule.light.type = LightType.Point;
				lightsModule.light.range = 20f;
				lightsModule.light.intensity = 0.5f;
				lightsModule.light.renderMode = LightRenderMode.ForcePixel;
				lightsModule.ratio = 0.25f;
				lightsModule.useRandomDistribution = false;
				lightsModule.maxLights = 50;
				ParticleSystem.LightsModule lights = component2.lights;
				lights.enabled = true;
				lights.light = new GameObject(global::_003CModule_003E.smethod_27<string>(3103498676u)).AddComponent<Light>();
				lights.light.gameObject.SetActive(value: false);
				lights.light.type = LightType.Point;
				lights.light.range = 20f;
				lights.light.intensity = 0.5f;
				lights.light.renderMode = LightRenderMode.ForcePixel;
				lights.ratio = 0.25f;
				lights.maxLights = 50;
				lights.useRandomDistribution = false;
			}
		}

		internal static Color smethod_0(BlockData blockData_0)
		{
			return blockData_0.GetEmitColor();
		}

		internal static ParticleSystem.LightsModule smethod_1(ParticleSystem particleSystem_0)
		{
			return particleSystem_0.lights;
		}
	}

	[HarmonyPatch(typeof(LaserController))]
	[HarmonyPatch("SetEnabled")]
	internal class Nr0ejQEJWARWwtZZB_00249GfUB6Cx175Jqi5RL0RXaxCfU2HfW8KYVRab_WVvhAWM_5DpTOKqFENXMGQ3b9sAQC0b8
	{
		[HarmonyPostfix]
		internal static void FeUAVwFbW6wGJJdNimZY9yI(LaserController __instance, bool BHCKMFDEBBH)
		{
			if (!MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.graphicsPlus)
			{
				return;
			}
			Light light_ = __instance.GetComponent<Light>();
			if (smethod_0((Object)light_, (Object)null))
			{
				light_ = smethod_1((Component)__instance).AddComponent<Light>();
				smethod_2(light_, LightType.Spot);
				smethod_3(light_, 150f);
				smethod_4(light_, 50f);
				smethod_5(light_, LightRenderMode.ForcePixel);
			}
			light_.enabled = BHCKMFDEBBH;
		}

		internal static bool smethod_0(Object object_0, Object object_1)
		{
			return object_0 == object_1;
		}

		internal static GameObject smethod_1(Component component_0)
		{
			return component_0.gameObject;
		}

		internal static void smethod_2(Light light_0, LightType lightType_0)
		{
			light_0.type = lightType_0;
		}

		internal static void smethod_3(Light light_0, float float_0)
		{
			light_0.range = float_0;
		}

		internal static void smethod_4(Light light_0, float float_0)
		{
			light_0.intensity = float_0;
		}

		internal static void smethod_5(Light light_0, LightRenderMode lightRenderMode_0)
		{
			light_0.renderMode = lightRenderMode_0;
		}
	}
}
