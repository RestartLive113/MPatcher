using System;
using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;

internal class DxOr8dXQgyp_0024o0E2VKABsO6g_0024PrF2knfo4d1GUGKxa9zdULS9tvdaW7_KOsKIOGLZw
{
	[HarmonyPatch(new Type[] { typeof(SystemData.EHLMFKOOHLI) })]
	[HarmonyPatch(typeof(HOCGCCAIPFF))]
	[HarmonyPatch("FGCCNKAIKAI")]
	internal class yLX9hmKnvHtQW7XsPidJ6oLxP8w5mvggqKKsl9odFob0pfnKkNsvd65YpeEGCpFAkZQLZLv8lU9tIicE8wklq1LG2HFRZGs5sdaSsVXD8U6BQB08kF03LskEUfHpY9lVxA
	{
		[HarmonyPostfix]
		public static void FeUAVwFbW6wGJJdNimZY9yI(ref SystemData.EHLMFKOOHLI ehlmfkoohli_0, ref bool bool_0)
		{
			if (FFO2y8eJ7prAu7zcWjCQfrYqiXV3mtv1W5nrwBCT_0024cfx.Contains(ehlmfkoohli_0))
			{
				bool_0 = !HOCGCCAIPFF.NDIOFGDJAJO && (!smethod_0((UnityEngine.Object)Arena.OEDCBNHNGMJ) || Arena.OEDCBNHNGMJ.MGDKMMIPJEM != 2);
			}
		}

		internal static bool smethod_0(UnityEngine.Object object_0)
		{
			return object_0;
		}
	}

	[HarmonyPatch("GetKeyDown")]
	[HarmonyPatch(typeof(Input))]
	[HarmonyPatch(new Type[] { typeof(KeyCode) })]
	internal class i3j_00249pBYiyV1NlWBJPQEr_D8DZtBLmAMUsguGJoZlrsZqzcji_0024n1GKxB8hDZ6dB_00240E1hGBjbp0MEnHzdgOru6UrwPCUDoBpVWSbLqXri8aCwJ3POWj14PxwfXjQaR3rCoY5DR4fsIwdIQjQzULQK7YU
	{
		[HarmonyPostfix]
		public static void FeUAVwFbW6wGJJdNimZY9yI(ref KeyCode keyCode_0, ref bool bool_0)
		{
			if (B1Oa2i6MWP7mDrQvjN_0024XMYujyG54g2uHdzFnayDhzXmK.Contains(keyCode_0))
			{
				bool_0 = true;
			}
		}
	}

	[HarmonyPatch(typeof(Input))]
	[HarmonyPatch(new Type[] { typeof(KeyCode) })]
	[HarmonyPatch("GetKey")]
	internal class FWJV4AzhGloJXzTHlwJgZjCfbG_0024jGfytQklrRqjJRT_CzmoSPWDIu_0024yB8Ne48m_CrCTcIeUxPfoixKjqQ3ftMTz3SjlzZ_00247rizAPTFeTuJuG_xLIdI8GPnrS9CHsKOf03pz1ybE5JaQfZYGrfREX7ro
	{
		[HarmonyPostfix]
		public static void FeUAVwFbW6wGJJdNimZY9yI(ref KeyCode keyCode_0, ref bool bool_0)
		{
			if (NUYeZmcDAiQP0dFSfRqzVMkfsrk1OsRzNC_LUwJMEx_0024y.Contains(keyCode_0))
			{
				bool_0 = true;
			}
		}
	}

	private static List<SystemData.EHLMFKOOHLI> FFO2y8eJ7prAu7zcWjCQfrYqiXV3mtv1W5nrwBCT_0024cfx = new List<SystemData.EHLMFKOOHLI>();

	private static List<KeyCode> B1Oa2i6MWP7mDrQvjN_0024XMYujyG54g2uHdzFnayDhzXmK = new List<KeyCode>();

	private static List<KeyCode> NUYeZmcDAiQP0dFSfRqzVMkfsrk1OsRzNC_LUwJMEx_0024y = new List<KeyCode>();

	public static void gcyFcvscFEpOJQ5riGkrGwY(SystemData.EHLMFKOOHLI ehlmfkoohli_0, bool bool_0)
	{
		if (bool_0 && !FFO2y8eJ7prAu7zcWjCQfrYqiXV3mtv1W5nrwBCT_0024cfx.Contains(ehlmfkoohli_0))
		{
			FFO2y8eJ7prAu7zcWjCQfrYqiXV3mtv1W5nrwBCT_0024cfx.Add(ehlmfkoohli_0);
		}
		else if (FFO2y8eJ7prAu7zcWjCQfrYqiXV3mtv1W5nrwBCT_0024cfx.Contains(ehlmfkoohli_0))
		{
			FFO2y8eJ7prAu7zcWjCQfrYqiXV3mtv1W5nrwBCT_0024cfx.Remove(ehlmfkoohli_0);
		}
	}

	public static void JuxrbyrV9dKSm1MmayrkIKU(KeyCode keyCode_0, bool bool_0)
	{
		if (bool_0 && !B1Oa2i6MWP7mDrQvjN_0024XMYujyG54g2uHdzFnayDhzXmK.Contains(keyCode_0))
		{
			B1Oa2i6MWP7mDrQvjN_0024XMYujyG54g2uHdzFnayDhzXmK.Add(keyCode_0);
		}
		else if (B1Oa2i6MWP7mDrQvjN_0024XMYujyG54g2uHdzFnayDhzXmK.Contains(keyCode_0))
		{
			B1Oa2i6MWP7mDrQvjN_0024XMYujyG54g2uHdzFnayDhzXmK.Remove(keyCode_0);
		}
	}

	public static void QGqUbAlvdFhQCyRDH7UBTMI(KeyCode keyCode_0, bool bool_0)
	{
		if (bool_0 && !NUYeZmcDAiQP0dFSfRqzVMkfsrk1OsRzNC_LUwJMEx_0024y.Contains(keyCode_0))
		{
			NUYeZmcDAiQP0dFSfRqzVMkfsrk1OsRzNC_LUwJMEx_0024y.Add(keyCode_0);
		}
		else if (!bool_0 && NUYeZmcDAiQP0dFSfRqzVMkfsrk1OsRzNC_LUwJMEx_0024y.Contains(keyCode_0))
		{
			NUYeZmcDAiQP0dFSfRqzVMkfsrk1OsRzNC_LUwJMEx_0024y.Remove(keyCode_0);
		}
	}
}
