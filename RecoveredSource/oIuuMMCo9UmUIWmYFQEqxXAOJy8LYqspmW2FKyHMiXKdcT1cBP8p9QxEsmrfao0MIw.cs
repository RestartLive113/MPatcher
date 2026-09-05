using System;
using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;
using UnityEngine.SceneManagement;

internal static class oIuuMMCo9UmUIWmYFQEqxXAOJy8LYqspmW2FKyHMiXKdcT1cBP8p9QxEsmrfao0MIw
{
	internal class ipRDe9DAQJ7IgOYEgQRfJkkVmCvz_rS4oGSurFFHJR1aFJH_Sp4KZ8Grs_0024QwNPdRKDyGowpC2Xxz7YU1IC4mmgmGAvdjUQnW7SWZPS96InobqwROs8QCH3VDbzmyyTKUMw : MonoBehaviour
	{
		private GameObject q_0024DFCzlY6s6OLa621D_0024oFzM;

		internal void OJQjJPW1_0024P_00241XcKPhnxDmss(BlockData myData)
		{
			if (!(smethod_0().name == global::_003CModule_003E.smethod_29<string>(2573180164u)) || !(base.transform.parent == null) || myData.type != BlockData.AAHMDBHDCDK.Tracker)
			{
				return;
			}
			if (!(bool)myData.props.Y_0024dZy3YTHmOYBqFB7lE7fD_0024vb6NsuWzjnbcG6_CoYN52(global::_003CModule_003E.smethod_26<string>(1915035524u), false))
			{
				if (q_0024DFCzlY6s6OLa621D_0024oFzM != null)
				{
					UnityEngine.Object.Destroy(q_0024DFCzlY6s6OLa621D_0024oFzM);
					q_0024DFCzlY6s6OLa621D_0024oFzM = null;
				}
				return;
			}
			if (q_0024DFCzlY6s6OLa621D_0024oFzM == null)
			{
				q_0024DFCzlY6s6OLa621D_0024oFzM = UnityEngine.Object.Instantiate(yJT1gK8OIDr03twQ0UsF3X03laCCNIeL85eTHNnX5V_o.njjDGmIhAD4tMIIjpgvq6aQ);
				q_0024DFCzlY6s6OLa621D_0024oFzM.name = global::_003CModule_003E.smethod_28<string>(526215278u);
				q_0024DFCzlY6s6OLa621D_0024oFzM.transform.parent = base.transform;
				q_0024DFCzlY6s6OLa621D_0024oFzM.layer = 9;
				Transform[] componentsInChildren = q_0024DFCzlY6s6OLa621D_0024oFzM.GetComponentsInChildren<Transform>();
				foreach (Transform obj in componentsInChildren)
				{
					obj.gameObject.layer = 9;
					obj.name = global::_003CModule_003E.smethod_28<string>(526215278u);
				}
				q_0024DFCzlY6s6OLa621D_0024oFzM.transform.localPosition = Vector3.zero;
			}
			int num = (int)myData.props.Y_0024dZy3YTHmOYBqFB7lE7fD_0024vb6NsuWzjnbcG6_CoYN52(global::_003CModule_003E.smethod_28<string>(344095476u), 0);
			string text = num switch
			{
				6 => global::_003CModule_003E.smethod_28<string>(4274823168u), 
				9 => global::_003CModule_003E.smethod_29<string>(4229045936u), 
				_ => num.ToString(), 
			};
			TextMesh[] componentsInChildren2 = q_0024DFCzlY6s6OLa621D_0024oFzM.GetComponentsInChildren<TextMesh>();
			foreach (TextMesh obj2 in componentsInChildren2)
			{
				obj2.text = text;
				obj2.GetComponent<MeshRenderer>().material.color = (((bool)myData.props.Y_0024dZy3YTHmOYBqFB7lE7fD_0024vb6NsuWzjnbcG6_CoYN52(global::_003CModule_003E.smethod_29<string>(2454796342u), false)) ? Color.red : Color.white);
			}
		}

		internal static Scene smethod_0()
		{
			return SceneManager.GetActiveScene();
		}
	}

	[HarmonyPatch(typeof(BlockData))]
	[HarmonyPatch("Set")]
	internal static class MSyH_0024okjKPSxRMqNzlsd3YBGfjWvxKaiubOn3yUyagF79j3392u8k5zM_3orK9ruNiXmaLy2I7eYP_phgn89XuSYPUvrg0pzE75d6eGNGc_0024WTCNO4PYoSAfad7E9n_fBKA
	{
		[HarmonyPostfix]
		internal static void FeUAVwFbW6wGJJdNimZY9yI(ref BlockData __instance, ref BlockData org)
		{
			if (org.props != null)
			{
				__instance.props = new Dictionary<string, object>(org.props);
			}
			else
			{
				__instance.props = null;
			}
		}
	}

	[HarmonyPatch(typeof(BlockData))]
	[HarmonyPatch("Clone")]
	internal static class Class25
	{
		[HarmonyPrefix]
		public static bool smethod_0(ref BlockData __instance, ref BlockData __result)
		{
			BlockData blockData = smethod_1();
			blockData.type = __instance.type;
			blockData.x = __instance.x;
			blockData.y = __instance.y;
			blockData.z = __instance.z;
			blockData.rgbI = __instance.rgbI;
			blockData.index = __instance.index;
			if (__instance.actionID.Length > blockData.actionID.Length)
			{
				blockData.actionID = new int[__instance.actionID.Length];
			}
			if (__instance.actionParam.Length > blockData.actionParam.Length)
			{
				blockData.actionParam = new int[__instance.actionParam.Length];
			}
			smethod_2((Array)__instance.actionID, (Array)blockData.actionID, 0);
			smethod_2((Array)__instance.actionParam, (Array)blockData.actionParam, 0);
			blockData.flag = __instance.flag;
			blockData.gid = __instance.gid;
			blockData.press = __instance.press;
			__result = blockData;
			if (__instance.props == null)
			{
				__result.props = null;
			}
			else
			{
				__result.props = new Dictionary<string, object>(__instance.props);
			}
			return false;
		}

		internal static BlockData smethod_1()
		{
			return new BlockData();
		}

		internal static void smethod_2(Array array_0, Array array_1, int int_0)
		{
			array_0.CopyTo(array_1, int_0);
		}
	}

	[HarmonyPatch("_CheckMatchAction")]
	[HarmonyPatch(typeof(BlockData))]
	internal static class IlK_0024FqFWvguOQcD1hOhuTMmcKoMDQDQGcWJDhR22ZSfJYg6LYWNEc5DYNU55nZjaM9DsEA6xM122tSaC76M0HF6RM_WnspmuSwurV23OvfSCpJ2zYVjb4Ks791U5Si7gIQ
	{
		[HarmonyPostfix]
		internal static void FeUAVwFbW6wGJJdNimZY9yI(BlockData other, BlockData __instance, ref bool __result)
		{
			if (__result && (__instance.props != null || other.props != null) && (__instance.props == null || other.props == null || __instance.props.Count != other.props.Count))
			{
				__result = false;
			}
		}
	}

	[HarmonyPatch("Start")]
	[HarmonyPatch(typeof(TrackerController))]
	internal static class S7_m3TAFHY1_0024wffzliowm_wZkh1HVH0IPp52rpMAxsizcFCS5Fz6rABzxNYebKSlS7ngw1_0024v6BKOEtD501KBux7UmyMFGs6Kcjl7aX5jckJ7RU1Glvukoh0zijn4bUQ03A
	{
		[HarmonyPostfix]
		internal static void FeUAVwFbW6wGJJdNimZY9yI(TrackerController __instance)
		{
			if (smethod_0(__instance))
			{
				__instance.OJQjJPW1_0024P_00241XcKPhnxDmss();
			}
		}
	}

	internal static bool xGPuOxA2rBW91iXaZdgcO18(BlockController bd, string key)
	{
		if (smethod_3((UnityEngine.Object)bd, (UnityEngine.Object)null))
		{
			return false;
		}
		return bd.JNKEKNOAPHO.props.ContainsKey(key);
	}

	internal static bool xGPuOxA2rBW91iXaZdgcO18(BlockData bd, string key)
	{
		if (bd != null && bd.props != null)
		{
			return bd.props.ContainsKey(key);
		}
		return false;
	}

	internal static T AIE60Kky2bU2sqqKbuxgR7LcNb5l6bc5BCXD6nzVkX_0024t<T>(BlockController bd, string key, T defaultVal)
	{
		if (smethod_3((UnityEngine.Object)bd, (UnityEngine.Object)null))
		{
			return defaultVal;
		}
		if (bd.JNKEKNOAPHO.props != null)
		{
			if (!bd.JNKEKNOAPHO.props.ContainsKey(key))
			{
				return defaultVal;
			}
			return (T)bd.JNKEKNOAPHO.props[key];
		}
		bd.JNKEKNOAPHO.props = new Dictionary<string, object>();
		return defaultVal;
	}

	internal static T AIE60Kky2bU2sqqKbuxgR7LcNb5l6bc5BCXD6nzVkX_0024t<T>(ref BlockData bd, string key, T defaultVal)
	{
		if (bd != null && bd.props != null)
		{
			if (bd.props == null)
			{
				bd.props = new Dictionary<string, object>();
				return defaultVal;
			}
			if (bd.props.ContainsKey(key))
			{
				return (T)bd.props[key];
			}
			return defaultVal;
		}
		return defaultVal;
	}

	internal static void ioLu5ucx0cm9MvyfVTOkUsE(BlockController bd, string key, object obj)
	{
		ioLu5ucx0cm9MvyfVTOkUsE(ref bd.JNKEKNOAPHO, key, obj);
	}

	internal static void ioLu5ucx0cm9MvyfVTOkUsE(ref BlockData bd, string key, object obj)
	{
		if (bd.props == null)
		{
			bd.props = new Dictionary<string, object>();
		}
		if (bd.props.ContainsKey(key))
		{
			bd.props[key] = obj;
		}
		else
		{
			bd.props.Add(key, obj);
		}
	}

	internal static void h42VsxFMP4yzP0oyRVsymO0(BlockController bd, string key)
	{
		if (!smethod_3((UnityEngine.Object)bd, (UnityEngine.Object)null) && bd.JNKEKNOAPHO.props.ContainsKey(key))
		{
			bd.JNKEKNOAPHO.props.Remove(key);
		}
	}

	internal static void h42VsxFMP4yzP0oyRVsymO0(ref BlockData bd, string key)
	{
		if (bd != null && bd.props != null && bd.props.ContainsKey(key))
		{
			bd.props.Remove(key);
		}
	}

	internal static bool smethod_0(BlockController bc)
	{
		if (smethod_3((UnityEngine.Object)bc, (UnityEngine.Object)null))
		{
			return false;
		}
		return smethod_1(bc.JNKEKNOAPHO);
	}

	internal static bool smethod_1(BlockData bc)
	{
		if (bc != null && bc.props != null)
		{
			if (bc.props.Count != 0)
			{
				return true;
			}
			return false;
		}
		return false;
	}

	internal static Dictionary<string, object> smethod_2(BlockController bc)
	{
		if (bc.JNKEKNOAPHO != null && bc.JNKEKNOAPHO.props != null)
		{
			return bc.JNKEKNOAPHO.props;
		}
		return new Dictionary<string, object>();
	}

	internal static void OJQjJPW1_0024P_00241XcKPhnxDmss(this BlockController bc)
	{
		if (smethod_3((UnityEngine.Object)bc.GetComponent<ipRDe9DAQJ7IgOYEgQRfJkkVmCvz_rS4oGSurFFHJR1aFJH_Sp4KZ8Grs_0024QwNPdRKDyGowpC2Xxz7YU1IC4mmgmGAvdjUQnW7SWZPS96InobqwROs8QCH3VDbzmyyTKUMw>(), (UnityEngine.Object)null))
		{
			smethod_4((Component)bc).AddComponent<ipRDe9DAQJ7IgOYEgQRfJkkVmCvz_rS4oGSurFFHJR1aFJH_Sp4KZ8Grs_0024QwNPdRKDyGowpC2Xxz7YU1IC4mmgmGAvdjUQnW7SWZPS96InobqwROs8QCH3VDbzmyyTKUMw>();
		}
		bc.GetComponent<ipRDe9DAQJ7IgOYEgQRfJkkVmCvz_rS4oGSurFFHJR1aFJH_Sp4KZ8Grs_0024QwNPdRKDyGowpC2Xxz7YU1IC4mmgmGAvdjUQnW7SWZPS96InobqwROs8QCH3VDbzmyyTKUMw>().OJQjJPW1_0024P_00241XcKPhnxDmss(bc.JNKEKNOAPHO);
	}

	internal static bool smethod_3(UnityEngine.Object object_0, UnityEngine.Object object_1)
	{
		return object_0 == object_1;
	}

	internal static GameObject smethod_4(Component component_0)
	{
		return component_0.gameObject;
	}
}
