using System;
using HarmonyLib;
using MPatchrMain;
using MPatchrMain.Utils;
using UnityEngine;

[HarmonyPatch("Act")]
[HarmonyPatch(typeof(CannonController))]
internal static class yzJYu9roTp6ENqtLoiCZ_0024IkzieVUW5qCWgOWO91IB0T1qP3hebt7hsycbIzVsKXt6A
{
	internal static void KX8jtCPdJb997Zi2Dvgkpuc(Build __instance)
	{
		RectTransform rectTransform = smethod_2(smethod_1(global::_003CModule_003E.smethod_26<string>(3103631283u)).smethod_0(global::_003CModule_003E.smethod_26<string>(3637592899u))).ToRect();
		Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.tjX92nnz9Ioc_0024izMOOLq4fI(global::_003CModule_003E.smethod_26<string>(1402950020u), global::_003CModule_003E.smethod_29<string>(3794747032u), new Vector3(107f, 180f), 100, 1000, (MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4 != null) ? MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.recoilMult : 100, delegate(float newVal)
		{
			if (MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4 == null)
			{
				MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4 = new mcpd();
			}
			MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.recoilMult = (int)newVal;
			MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.UUiRNMwxRbfk_Fs4cDErRoM();
		}, rectTransform.transform);
	}

	[HarmonyPrefix]
	internal static bool smethod_0(CannonController __instance, ref int ___KFDIPGJIMID, ref Color ___CINAFGDAGPJ, ref float ___FBDACMDDAKC, ref float ___HNEFLFIDDLM, ref float __result)
	{
		if (MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4 != null && MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.recoilMult != 100)
		{
			MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.recoilMult = Mathf.Clamp(MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.recoilMult, 100, 1000);
			Rigidbody nFMPBACKJOJ = __instance.DLMKKFCHFNC.NFMPBACKJOJ;
			Vector3 vector = smethod_4(smethod_3((Component)__instance));
			Vector3 vector2 = smethod_5(smethod_3((Component)__instance)) + vector * __instance.AIBNJHMPMJN * __instance.FPEBEEJGFPI.DJCDFLDHPHK;
			Vector3 vector3;
			switch (___KFDIPGJIMID)
			{
			case 1:
				vector3 = BDLEJBBJJOI.IGMEGICHKOE(UnityEngine.Random.insideUnitSphere * 0.3f).MultiplyVector(nFMPBACKJOJ.GetPointVelocity(vector2) * 0.5f);
				vector3 += vector * (Mathf.Sqrt(__instance.MCANPEFIGBL + 2) * 300f);
				break;
			default:
			{
				Vector3 vector4 = smethod_6(nFMPBACKJOJ, vector2) * 0.01f;
				vector3 = vector4 * 100f / Mathf.Max(Mathf.Sqrt(vector4.magnitude), 1f);
				vector3 += vector * Mathf.Sqrt((float)__instance.MCANPEFIGBL + 0.1f) * 150f;
				break;
			}
			case 0:
				vector3 = vector * (Mathf.Sqrt(__instance.MCANPEFIGBL + 8) * 250f);
				break;
			}
			float magnitude = vector3.magnitude;
			Vector3 normalized = (vector3 + BDLEJBBJJOI.IGMEGICHKOE(__instance.transform.up * UnityEngine.Random.Range(0f, (float)Math.PI * 2f)).MultiplyVector(__instance.transform.forward * UnityEngine.Random.value * ___FBDACMDDAKC)).normalized;
			vector3 = normalized * magnitude;
			Vector3 vector5 = vector2 + vector + vector3 * 0.005f;
			if (___KFDIPGJIMID > 0)
			{
				Vector3 vector6 = -vector3 * __instance.MHNDMMGGOFF * magnitude * ((!__instance.FPEBEEJGFPI.CHDEBOIIMCI) ? 0.0003f : 0.0001f);
				if (___KFDIPGJIMID == 2)
				{
					vector6 *= 2f;
				}
				float num = MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.recoilMult;
				num /= 100f;
				vector6 *= num;
				nFMPBACKJOJ.AddForceAtPosition(vector6 * (Mathf.Sqrt(__instance.FPEBEEJGFPI.CCFPDFAMGKC) * 0.002f + 0.02f), __instance.transform.position);
			}
			Game game = Arena.OEDCBNHNGMJ as Game;
			if (___KFDIPGJIMID != 0)
			{
				Vector3 vector7 = -__instance.FPEBEEJGFPI.DBLBJACAPCP;
				if (___KFDIPGJIMID == 1)
				{
					ShellController shellScr = __instance.FPEBEEJGFPI.GetShellScr();
					shellScr.BJLNMOHHHCL = ___CINAFGDAGPJ;
					if ((bool)game)
					{
						shellScr.ILHIGOPKKKC = game.SyncShell(vector5, vector3, vector7, ___CINAFGDAGPJ, __instance.MHNDMMGGOFF);
					}
					shellScr.Activate(vector5, vector3, vector7, vector2, vector, __instance.MHNDMMGGOFF, __instance.FPEBEEJGFPI.KDODJPCDEHO);
				}
				else
				{
					GrenadeController grenadeScr = __instance.FPEBEEJGFPI.GetGrenadeScr();
					grenadeScr.BJLNMOHHHCL = ___CINAFGDAGPJ;
					if ((bool)game)
					{
						grenadeScr.ILHIGOPKKKC = game.SyncGrenade(vector5, vector3, vector7, ___CINAFGDAGPJ, __instance.MHNDMMGGOFF);
					}
					grenadeScr.Activate(vector5, vector3, vector7, vector2, vector, __instance.MHNDMMGGOFF, __instance.FPEBEEJGFPI.KDODJPCDEHO);
				}
			}
			else
			{
				BulletController bulletScr = __instance.FPEBEEJGFPI.GetBulletScr();
				bulletScr.BJLNMOHHHCL = ___CINAFGDAGPJ;
				if ((bool)game)
				{
					bulletScr.ILHIGOPKKKC = game.SyncBullet(vector5, vector3, ___CINAFGDAGPJ);
				}
				if (__instance.FPEBEEJGFPI.KDODJPCDEHO)
				{
					float bCKFEPGMECF = Mathf.Clamp(Vector3.Dot(normalized, nFMPBACKJOJ.GetPointVelocity(vector2)) * 0.005f + 1f, 0f, 1f);
					bulletScr.Activate(vector5, vector3, vector2, vector, bCKFEPGMECF);
				}
				else
				{
					bulletScr.Activate(vector5, vector3, vector2, vector, 0f);
				}
			}
			__result = ___HNEFLFIDDLM;
			return false;
		}
		return true;
	}

	internal static GameObject smethod_1(string string_0)
	{
		return GameObject.Find(string_0);
	}

	internal static Transform smethod_2(GameObject gameObject_0)
	{
		return gameObject_0.transform;
	}

	internal static Transform smethod_3(Component component_0)
	{
		return component_0.transform;
	}

	internal static Vector3 smethod_4(Transform transform_0)
	{
		return transform_0.up;
	}

	internal static Vector3 smethod_5(Transform transform_0)
	{
		return transform_0.position;
	}

	internal static Vector3 smethod_6(Rigidbody rigidbody_0, Vector3 vector3_0)
	{
		return rigidbody_0.GetPointVelocity(vector3_0);
	}
}
