using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using HarmonyLib;
using McnCraft;
using UnityEngine;

[HarmonyPatch(typeof(MachineController))]
[HarmonyPatch("RPC_Chat")]
[HarmonyPatch(new Type[]
{
	typeof(string),
	typeof(string)
})]
internal class fB_UAU3VnG0wrUs2vz3KliVPPZGNBZGJ1WMs94YsvVgRq16uNvACiTMzlWLTlwKpvA
{
	internal static readonly string G7khXKfjk4KTXg71C_00242Nx3I = global::_003CModule_003E.smethod_26<string>(3249244783u);

	internal static readonly string m_ne3lilYmB2u1AUzL2dOj0 = global::_003CModule_003E.smethod_29<string>(795184692u);

	private static readonly string KU8hCW5BP76mNOVee6EWV9I = global::_003CModule_003E.smethod_28<string>(636576924u);
	internal static Vector3[] rV3NLoyUlBog_42slx5OCoA;
	internal static Vector3[] vector3_0;
	internal static Vector3[] vector3_1;
	internal static Vector3[] vector3_2;
	internal static bool oJN_00244IcEU0waAX7Zww3G6zI = false;

	[HarmonyPrefix]
	internal static void smethod_0(MachineController __instance, string DDMLCAJGAID, string KNNKJJMKAAI)
	{
		if (!oJN_00244IcEU0waAX7Zww3G6zI)
		{
			return;
		}
		BkEYtCXOGBC5a3sGZc1baFBEZsix9sHaNV7UHBoeB8aKxFK0bpuW1JKVsL3eaRtFkQ bkEYtCXOGBC5a3sGZc1baFBEZsix9sHaNV7UHBoeB8aKxFK0bpuW1JKVsL3eaRtFkQ = new BkEYtCXOGBC5a3sGZc1baFBEZsix9sHaNV7UHBoeB8aKxFK0bpuW1JKVsL3eaRtFkQ(__instance.GetComponent<PhotonView>());
		if (!smethod_3(smethod_2(KNNKJJMKAAI), global::_003CModule_003E.smethod_29<string>(2807563001u)))
		{
			if (smethod_3(smethod_2(KNNKJJMKAAI), global::_003CModule_003E.smethod_28<string>(3520041651u)))
			{
				smethod_5(smethod_4(global::_003CModule_003E.smethod_25<string>(2627359026u)).GetComponent<PhotonView>(), global::_003CModule_003E.smethod_29<string>(5531921u), bkEYtCXOGBC5a3sGZc1baFBEZsix9sHaNV7UHBoeB8aKxFK0bpuW1JKVsL3eaRtFkQ.K52jeLH_0024D_rsZoa7xVSPVPk, new object[2] { vector3_0, vector3_2 });
				smethod_5(__instance.GetComponent<PhotonView>(), KU8hCW5BP76mNOVee6EWV9I, bkEYtCXOGBC5a3sGZc1baFBEZsix9sHaNV7UHBoeB8aKxFK0bpuW1JKVsL3eaRtFkQ.K52jeLH_0024D_rsZoa7xVSPVPk, new object[3]
				{
					1,
					float.MaxValue,
					0
				});
			}
		}
		else
		{
			smethod_5(smethod_4(global::_003CModule_003E.smethod_25<string>(2627359026u)).GetComponent<PhotonView>(), global::_003CModule_003E.smethod_27<string>(1583991532u), bkEYtCXOGBC5a3sGZc1baFBEZsix9sHaNV7UHBoeB8aKxFK0bpuW1JKVsL3eaRtFkQ.K52jeLH_0024D_rsZoa7xVSPVPk, new object[2] { rV3NLoyUlBog_42slx5OCoA, vector3_1 });
			smethod_5(__instance.GetComponent<PhotonView>(), KU8hCW5BP76mNOVee6EWV9I, bkEYtCXOGBC5a3sGZc1baFBEZsix9sHaNV7UHBoeB8aKxFK0bpuW1JKVsL3eaRtFkQ.K52jeLH_0024D_rsZoa7xVSPVPk, new object[3]
			{
				1,
				float.MaxValue,
				0
			});
		}
	}

	internal static void z7OwYM7_0024aGyIOEaOYREGnoU(Game __instance, string OIHFGPOPPDD)
	{
		if (!smethod_6())
		{
			smethod_7(__instance, m_ne3lilYmB2u1AUzL2dOj0, -1);
			return;
		}
		rV3NLoyUlBog_42slx5OCoA = new Vector3[30];
		vector3_0 = new Vector3[30];
		vector3_1 = new Vector3[30];
		vector3_2 = new Vector3[30];
		Vector3[] rots;
		Vector3[] array = smethod_1(out rots);
		Vector3[] rots2;
		Vector3[] array2 = v2ZqnTVxlWgZ_CROtL2F3FE(out rots2);
		smethod_7(__instance, smethod_8(global::_003CModule_003E.smethod_25<string>(3855626484u), (object)array.Length), -1);
		smethod_7(__instance, smethod_8(global::_003CModule_003E.smethod_27<string>(611449699u), (object)array2.Length), -1);
		smethod_9((Array)array, (Array)rV3NLoyUlBog_42slx5OCoA, array.Length);
		smethod_9((Array)rots, (Array)vector3_1, rots.Length);
		smethod_9((Array)array2, (Array)vector3_0, array2.Length);
		smethod_9((Array)rots2, (Array)vector3_2, rots2.Length);
		Hashtable hashtable = smethod_10();
		hashtable.Add(global::_003CModule_003E.smethod_26<string>(4253210631u), 64);
		fYYx8vN1tah7ZgfygXMH9eJU0YzjaUsqtv9tADffziMcu1k1It8P_0024ZciTdZuxN09bA.MCJUb7mzEcz9seOWPbquoos.eqcw8jU_g5UyCpFcD2HSL5O3iJjZefN8oeBDqEku7iqb(hashtable);
		smethod_7(__instance, global::_003CModule_003E.smethod_27<string>(132166838u), -1);
		oJN_00244IcEU0waAX7Zww3G6zI = true;
	}

	internal static Vector3[] smethod_1(out Vector3[] rots)
	{
		PrimitiveController[] array = UnityEngine.Object.FindObjectsOfType<PrimitiveController>();
		List<Vector3> list = new List<Vector3>();
		List<Vector3> list2 = new List<Vector3>();
		PrimitiveController[] array2 = array;
		foreach (PrimitiveController primitiveController in array2)
		{
			if ((double)primitiveController.HJEGNEIKDDO.r >= 0.95 && primitiveController.HJEGNEIKDDO.g <= 0.05f && primitiveController.HJEGNEIKDDO.b <= 0.05f && smethod_12(smethod_11((Component)primitiveController)) == Vector3.one)
			{
				list.Add(smethod_13(smethod_11((Component)primitiveController)));
				list2.Add(smethod_14(smethod_11((Component)primitiveController)).eulerAngles);
			}
		}
		rots = list2.ToArray();
		return list.ToArray();
	}
	internal static Vector3[] v2ZqnTVxlWgZ_CROtL2F3FE(out Vector3[] rots)
	{
		PrimitiveController[] array = UnityEngine.Object.FindObjectsOfType<PrimitiveController>();
		List<Vector3> list = new List<Vector3>();
		List<Vector3> list2 = new List<Vector3>();
		PrimitiveController[] array2 = array;
		foreach (PrimitiveController primitiveController in array2)
		{
			if ((double)primitiveController.HJEGNEIKDDO.b >= 0.95 && primitiveController.HJEGNEIKDDO.g <= 0.05f && primitiveController.HJEGNEIKDDO.r <= 0.05f && smethod_12(smethod_11((Component)primitiveController)) == Vector3.one)
			{
				list.Add(smethod_13(smethod_11((Component)primitiveController)));
				list2.Add(smethod_14(smethod_11((Component)primitiveController)).eulerAngles);
			}
		}
		rots = list2.ToArray();
		return list.ToArray();
	}

	internal static string smethod_2(string string_0)
	{
		return string_0.ToLower();
	}

	internal static bool smethod_3(string string_0, string string_1)
	{
		return string_0.Equals(string_1);
	}

	internal static GameObject smethod_4(string string_0)
	{
		return GameObject.Find(string_0);
	}

	internal static void smethod_5(PhotonView photonView_0, string string_0, OPLNFKECCLE oplnfkeccle_0, object[] object_0)
	{
		photonView_0.RPC(string_0, oplnfkeccle_0, object_0);
	}

	internal static bool smethod_6()
	{
		return JONBPAFNPBD.JNLBBLEEPBJ;
	}

	internal static void smethod_7(Game game_0, string string_0, int int_0)
	{
		game_0.RPC_Chat(string_0, int_0);
	}

	internal static string smethod_8(string string_0, object object_0)
	{
		return string.Format(string_0, object_0);
	}

	internal static void smethod_9(Array array_0, Array array_1, int int_0)
	{
		Array.Copy(array_0, array_1, int_0);
	}

	internal static Hashtable smethod_10()
	{
		return new Hashtable();
	}

	internal static Transform smethod_11(Component component_0)
	{
		return component_0.transform;
	}

	internal static Vector3 smethod_12(Transform transform_0)
	{
		return transform_0.localScale;
	}

	internal static Vector3 smethod_13(Transform transform_0)
	{
		return transform_0.position;
	}

	internal static Quaternion smethod_14(Transform transform_0)
	{
		return transform_0.rotation;
	}
}
