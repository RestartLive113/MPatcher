using System;
using System.Collections.Generic;
using HarmonyLib;
using MPatchrMain;
using McnCraft;
using UnityEngine;
using UnityEngine.SceneManagement;

[HarmonyPatch("FixedUpdate")]
[HarmonyPatch(typeof(TrackerController))]
internal static class Class58
{
	internal static Dictionary<int, TrackerController> slmaTdHfkN8EbY9v1gjKREs = new Dictionary<int, TrackerController>();

	internal static void wDVFaxdBzm8fRhHk_5cuz8FErclPLUkQy_0024XwhACkV1zd(TrackerController __instance)
	{
		Vector3 forward = Vector3.forward;
		Vector3 lhs = Vector3.Cross(smethod_2(smethod_1((Component)__instance)), forward);
		Vector3 vector = ((__instance.NOLIFIMDABE != 0) ? smethod_4(smethod_1((Component)__instance)) : smethod_3(smethod_1((Component)__instance)));
		float num = Vector3.Dot(lhs, vector);
		lhs = vector * num;
		if (Vector3.Dot(__instance.DLMKKFCHFNC.LPFGMHIBAJJ, lhs) >= 0f)
		{
			float kBPOIKOFMIA = __instance.DLMKKFCHFNC.KBPOIKOFMIA;
			if (kBPOIKOFMIA < 1f && __instance.FPEBEEJGFPI.IGPDBKHABEF > 0 && __instance.FPEBEEJGFPI.DDONNBCMAKO >= 0)
			{
				kBPOIKOFMIA = Mathf.Min(kBPOIKOFMIA * 5f, 1f);
			}
			float num2 = num * Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<float>(global::_003CModule_003E.smethod_29<string>(1495880929u), __instance) * __instance.DLMKKFCHFNC.KBPOIKOFMIA;
			if (JKGKJLLFMLE.JGJPEBCEGFK)
			{
				float num3 = smethod_5(__instance.DLMKKFCHFNC) * __instance.DLMKKFCHFNC.IAIIPCOMJEE;
				if (num3 > 10000f)
				{
					num3 -= 10000f;
					float num4 = 1f / (num3 * 0.0001f + 1f);
					num2 *= num4;
					if (JKGKJLLFMLE.EGFHGHKLNAO == JKGKJLLFMLE.LENPCAMMAEP.Practice && smethod_6() - Class17.Ft_0024nB0s4dAgugQeebb6PgzB4yHCRQNxXyCsH1X_0024ugky6<float>(smethod_7(typeof(TrackerController).TypeHandle), global::_003CModule_003E.smethod_25<string>(844092682u)) > 10f)
					{
						int num5 = Mathf.RoundToInt(num4 * 100f);
						if (HelpDefs.isJ)
						{
							smethod_10((object)smethod_9(new object[4]
							{
								global::_003CModule_003E.smethod_25<string>(1371626966u),
								num5,
								global::_003CModule_003E.smethod_26<string>(316492580u),
								smethod_8(__instance.JNKEKNOAPHO, bool_0: true)
							}), 1);
						}
						else
						{
							smethod_10((object)smethod_9(new object[5]
							{
								global::_003CModule_003E.smethod_27<string>(209392142u),
								smethod_8(__instance.JNKEKNOAPHO, bool_0: true),
								global::_003CModule_003E.smethod_29<string>(1257752214u),
								num5,
								global::_003CModule_003E.smethod_26<string>(1160074709u)
							}), 1);
						}
						Class17._0024kbvtNkp7Zv_00240Loc7zI_sdWmxQCKsS2GtiWeaeYYD9UE(smethod_7(typeof(TrackerController).TypeHandle), global::_003CModule_003E.smethod_27<string>(3206102880u), smethod_6());
					}
				}
			}
			if (Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<float>(global::_003CModule_003E.smethod_27<string>(456021628u), __instance) < 1f)
			{
				num2 *= (1f - Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<float>(global::_003CModule_003E.smethod_28<string>(3545010097u), __instance)) / (Mathf.Abs(num) * 5f + 1f) + 1f;
			}
			Vector3 vector2 = vector * (num2 * 100f / (__instance.DLMKKFCHFNC.ADIJGLLECKF * 100f + 1f));
			if ((__instance.FPEBEEJGFPI.IGPDBKHABEF == 0 || __instance.FPEBEEJGFPI.DDONNBCMAKO < 0) && (__instance.DLMKKFCHFNC.IDLKNFBGFEB == 1 || __instance.DLMKKFCHFNC.GPLLMGEDLJO > 4))
			{
				float num6 = Vector3.Dot(vector2, smethod_11(__instance.FPEBEEJGFPI.NFMPBACKJOJ));
				if (num6 > 0f)
				{
					vector2 /= num6 * __instance.DLMKKFCHFNC.IAIIPCOMJEE * 0.002f + 1f;
				}
			}
			Rigidbody nFMPBACKJOJ = __instance.DLMKKFCHFNC.NFMPBACKJOJ;
			smethod_12(nFMPBACKJOJ, vector2, ForceMode.Acceleration);
			smethod_13(nFMPBACKJOJ, smethod_11(nFMPBACKJOJ) * 0.9f);
		}
		smethod_14(__instance.DLMKKFCHFNC, Vector3.one);
	}

	[HarmonyPrefix]
	internal static bool smethod_0(TrackerController __instance)
	{
		if (smethod_15().name == global::_003CModule_003E.smethod_28<string>(2987648538u) && __instance.tag != global::_003CModule_003E.smethod_26<string>(227871266u))
		{
			wDVFaxdBzm8fRhHk_5cuz8FErclPLUkQy_0024XwhACkV1zd(__instance);
			return false;
		}
		if (!MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.customTrackerAim || !oIuuMMCo9UmUIWmYFQEqxXAOJy8LYqspmW2FKyHMiXKdcT1cBP8p9QxEsmrfao0MIw.smethod_0(__instance))
		{
			return true;
		}
		try
		{
			bool flag = false;
			bool flag2 = false;
			int key = 0;
			bool flag3 = false;
			if (oIuuMMCo9UmUIWmYFQEqxXAOJy8LYqspmW2FKyHMiXKdcT1cBP8p9QxEsmrfao0MIw.smethod_0(__instance))
			{
				Dictionary<string, object> idictionary_ = oIuuMMCo9UmUIWmYFQEqxXAOJy8LYqspmW2FKyHMiXKdcT1cBP8p9QxEsmrfao0MIw.smethod_2(__instance);
				bool num = (bool)idictionary_.Y_0024dZy3YTHmOYBqFB7lE7fD_0024vb6NsuWzjnbcG6_CoYN52(global::_003CModule_003E.smethod_28<string>(890454882u), false);
				flag = num;
				flag2 = num && (bool)idictionary_.Y_0024dZy3YTHmOYBqFB7lE7fD_0024vb6NsuWzjnbcG6_CoYN52(global::_003CModule_003E.smethod_25<string>(2219484845u), false);
				key = (int)idictionary_.Y_0024dZy3YTHmOYBqFB7lE7fD_0024vb6NsuWzjnbcG6_CoYN52(global::_003CModule_003E.smethod_29<string>(961860126u), 0);
				if (flag2)
				{
					if (!slmaTdHfkN8EbY9v1gjKREs.ContainsKey(key))
					{
						slmaTdHfkN8EbY9v1gjKREs.Add(key, __instance);
						mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_27<string>(688675003u) + key + global::_003CModule_003E.smethod_25<string>(596200768u), bool_0: true);
					}
					else if (slmaTdHfkN8EbY9v1gjKREs[key] == null)
					{
						slmaTdHfkN8EbY9v1gjKREs[key] = __instance;
					}
				}
				else
				{
					flag3 = slmaTdHfkN8EbY9v1gjKREs.ContainsKey(key) && slmaTdHfkN8EbY9v1gjKREs[key] != null;
				}
			}
			if (flag2)
			{
				return false;
			}
			if (oIuuMMCo9UmUIWmYFQEqxXAOJy8LYqspmW2FKyHMiXKdcT1cBP8p9QxEsmrfao0MIw.smethod_0(__instance) && (bool)oIuuMMCo9UmUIWmYFQEqxXAOJy8LYqspmW2FKyHMiXKdcT1cBP8p9QxEsmrfao0MIw.smethod_2(__instance).Y_0024dZy3YTHmOYBqFB7lE7fD_0024vb6NsuWzjnbcG6_CoYN52(global::_003CModule_003E.smethod_25<string>(2219484845u), false))
			{
				return false;
			}
			__instance.JNIKJHPNFKH = -2f;
			if (__instance.NOLIFIMDABE == 2)
			{
				return false;
			}
			if (__instance.FPEBEEJGFPI == null)
			{
				return false;
			}
			if (__instance.FPEBEEJGFPI.IsFailure())
			{
				return false;
			}
			if (__instance.DLMKKFCHFNC.IsBroken())
			{
				return false;
			}
			if (__instance.DLMKKFCHFNC.NFMPBACKJOJ.isKinematic)
			{
				return false;
			}
			if (HOCGCCAIPFF.HJLGLEOIJLF != 0)
			{
				return false;
			}
			if (Arena.OEDCBNHNGMJ.MGDKMMIPJEM == 2)
			{
				return false;
			}
			AutoPilot iHJFCDLFFBM = __instance.FPEBEEJGFPI.IHJFCDLFFBM;
			bool flag4 = (bool)iHJFCDLFFBM && iHJFCDLFFBM.aimPoss[__instance.JNKEKNOAPHO.gid] != Vector3.zero;
			bool flag5;
			if (!(flag5 = (bool)iHJFCDLFFBM && iHJFCDLFFBM.enabled && (flag4 || iHJFCDLFFBM.aimDir != Vector3.zero)) && Arena.OEDCBNHNGMJ.FICMBCLEFDL != __instance.FPEBEEJGFPI)
			{
				return false;
			}
			if (__instance.NOLIFIMDABE < 0)
			{
				return false;
			}
			float num2 = Class17.DQGZpGELKqeKmXweFYGEcaXR73CqGywHQI6u2TATGL80<PartsController, float>(global::_003CModule_003E.smethod_27<string>(3452732366u), __instance, new object[1] { true });
			if (num2 < -9000f)
			{
				return false;
			}
			__instance.DLMKKFCHFNC.IAIIPCOMJEE += 1f;
			if (Class17.DQGZpGELKqeKmXweFYGEcaXR73CqGywHQI6u2TATGL80<PartsController, bool>(global::_003CModule_003E.smethod_29<string>(663043495u), __instance, new object[2]
			{
				Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<float>(global::_003CModule_003E.smethod_28<string>(3545010097u), __instance) * 0.003f,
				true
			}))
			{
				Vector3 vector;
				if (!flag5)
				{
					if (!Arena.GLHCLEDLLIL)
					{
						vector = __instance.transform.up;
					}
					else if (flag && !flag2 && flag3)
					{
						vector = (slmaTdHfkN8EbY9v1gjKREs[key].transform.position - __instance.transform.position).normalized;
						if (__instance.DLMKKFCHFNC.NNNIOCDPCOG)
						{
							__instance.JNIKJHPNFKH = Vector3.Dot(vector, __instance.transform.up);
						}
					}
					else
					{
						vector = BDLEJBBJJOI.IGMEGICHKOE(Vector3.Cross(__instance.FPEBEEJGFPI.HIPPHFEKAOD, Arena.GLHCLEDLLIL.forward), num2 * (float)Math.PI / 180f).MultiplyVector(Arena.OEDCBNHNGMJ.BOIEJCIBHKI.BKMMOCJFEHG);
					}
				}
				else if (flag4)
				{
					vector = (iHJFCDLFFBM.aimPoss[__instance.JNKEKNOAPHO.gid] - __instance.transform.position).normalized;
					if (__instance.DLMKKFCHFNC.NNNIOCDPCOG)
					{
						__instance.JNIKJHPNFKH = Vector3.Dot(vector, __instance.transform.up);
					}
				}
				else
				{
					vector = BDLEJBBJJOI.IGMEGICHKOE(__instance.transform.right, num2 * (float)Math.PI / 1800f).MultiplyVector(__instance.FPEBEEJGFPI.IHJFCDLFFBM.eyeDir);
				}
				Vector3 lhs = Vector3.Cross(__instance.transform.up, vector);
				Vector3 vector2 = ((__instance.NOLIFIMDABE != 0) ? __instance.transform.forward : __instance.transform.right);
				float num3 = Vector3.Dot(lhs, vector2);
				lhs = vector2 * num3;
				if (Vector3.Dot(__instance.DLMKKFCHFNC.LPFGMHIBAJJ, lhs) >= 0f)
				{
					float kBPOIKOFMIA = __instance.DLMKKFCHFNC.KBPOIKOFMIA;
					if (kBPOIKOFMIA < 1f && __instance.FPEBEEJGFPI.IGPDBKHABEF > 0 && __instance.FPEBEEJGFPI.DDONNBCMAKO >= 0)
					{
						kBPOIKOFMIA = Mathf.Min(kBPOIKOFMIA * 5f, 1f);
					}
					float num4 = num3 * Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<float>(global::_003CModule_003E.smethod_25<string>(4174559662u), __instance) * __instance.DLMKKFCHFNC.KBPOIKOFMIA;
					if (JKGKJLLFMLE.JGJPEBCEGFK)
					{
						float num5 = __instance.DLMKKFCHFNC.SumThrustR() * __instance.DLMKKFCHFNC.IAIIPCOMJEE;
						if (num5 > 10000f)
						{
							num5 -= 10000f;
							float num6 = 1f / (num5 * 0.0001f + 1f);
							num4 *= num6;
							if (JKGKJLLFMLE.EGFHGHKLNAO == JKGKJLLFMLE.LENPCAMMAEP.Practice && Time.realtimeSinceStartup - Class17.Ft_0024nB0s4dAgugQeebb6PgzB4yHCRQNxXyCsH1X_0024ugky6<float>(typeof(TrackerController), global::_003CModule_003E.smethod_27<string>(3206102880u)) > 10f)
							{
								int num7 = Mathf.RoundToInt(num6 * 100f);
								if (!HelpDefs.isJ)
								{
									DP.D(global::_003CModule_003E.smethod_28<string>(1981839831u) + __instance.JNKEKNOAPHO.GetPosString() + global::_003CModule_003E.smethod_27<string>(2047438347u) + num7 + global::_003CModule_003E.smethod_28<string>(3803037851u));
								}
								else
								{
									DP.D(global::_003CModule_003E.smethod_25<string>(1371626966u) + num7 + global::_003CModule_003E.smethod_27<string>(3685385741u) + __instance.JNKEKNOAPHO.GetPosString());
								}
								Class17._0024kbvtNkp7Zv_00240Loc7zI_sdWmxQCKsS2GtiWeaeYYD9UE(typeof(TrackerController), global::_003CModule_003E.smethod_28<string>(3180770493u), Time.realtimeSinceStartup);
							}
						}
					}
					if (Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<float>(global::_003CModule_003E.smethod_29<string>(1495880929u), __instance) < 1f)
					{
						num4 *= (1f - Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<float>(global::_003CModule_003E.smethod_25<string>(4174559662u), __instance)) / (Mathf.Abs(num3) * 5f + 1f) + 1f;
					}
					Vector3 vector3 = vector2 * (num4 * 100f / (__instance.DLMKKFCHFNC.ADIJGLLECKF * 100f + 1f));
					if ((__instance.FPEBEEJGFPI.IGPDBKHABEF == 0 || __instance.FPEBEEJGFPI.DDONNBCMAKO < 0) && (__instance.DLMKKFCHFNC.IDLKNFBGFEB == 1 || __instance.DLMKKFCHFNC.GPLLMGEDLJO > 4))
					{
						float num8 = Vector3.Dot(vector3, __instance.FPEBEEJGFPI.NFMPBACKJOJ.angularVelocity);
						if (num8 > 0f)
						{
							vector3 /= num8 * __instance.DLMKKFCHFNC.IAIIPCOMJEE * 0.002f + 1f;
						}
					}
					Rigidbody nFMPBACKJOJ = __instance.DLMKKFCHFNC.NFMPBACKJOJ;
					nFMPBACKJOJ.AddTorque(vector3, ForceMode.Acceleration);
					nFMPBACKJOJ.angularVelocity *= 0.9f;
				}
				__instance.DLMKKFCHFNC.SetTemporalIT(Vector3.one);
			}
		}
		catch (Exception ex)
		{
			mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_27<string>(2959473394u) + ex.Message + global::_003CModule_003E.smethod_28<string>(119039346u) + ex.StackTrace);
		}
		return false;
	}

	internal static Transform smethod_1(Component component_0)
	{
		return component_0.transform;
	}

	internal static Vector3 smethod_2(Transform transform_0)
	{
		return transform_0.up;
	}

	internal static Vector3 smethod_3(Transform transform_0)
	{
		return transform_0.right;
	}

	internal static Vector3 smethod_4(Transform transform_0)
	{
		return transform_0.forward;
	}

	internal static float smethod_5(BodyController bodyController_0)
	{
		return bodyController_0.SumThrustR();
	}

	internal static float smethod_6()
	{
		return Time.realtimeSinceStartup;
	}

	internal static Type smethod_7(RuntimeTypeHandle runtimeTypeHandle_0)
	{
		return Type.GetTypeFromHandle(runtimeTypeHandle_0);
	}

	internal static string smethod_8(BlockData blockData_0, bool bool_0)
	{
		return blockData_0.GetPosString(bool_0);
	}

	internal static string smethod_9(object[] object_0)
	{
		return string.Concat(object_0);
	}

	internal static void smethod_10(object object_0, int int_0)
	{
		DP.D(object_0, int_0);
	}

	internal static Vector3 smethod_11(Rigidbody rigidbody_0)
	{
		return rigidbody_0.angularVelocity;
	}

	internal static void smethod_12(Rigidbody rigidbody_0, Vector3 vector3_0, ForceMode forceMode_0)
	{
		rigidbody_0.AddTorque(vector3_0, forceMode_0);
	}

	internal static void smethod_13(Rigidbody rigidbody_0, Vector3 vector3_0)
	{
		rigidbody_0.angularVelocity = vector3_0;
	}

	internal static void smethod_14(BodyController bodyController_0, Vector3 vector3_0)
	{
		bodyController_0.SetTemporalIT(vector3_0);
	}

	internal static Scene smethod_15()
	{
		return SceneManager.GetActiveScene();
	}
}
