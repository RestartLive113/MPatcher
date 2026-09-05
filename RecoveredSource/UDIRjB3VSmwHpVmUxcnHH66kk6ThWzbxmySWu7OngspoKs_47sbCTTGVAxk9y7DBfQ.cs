using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using HarmonyLib;
using McnCraft;
using UnityEngine;

internal static class UDIRjB3VSmwHpVmUxcnHH66kk6ThWzbxmySWu7OngspoKs_47sbCTTGVAxk9y7DBfQ
{
	[HarmonyPatch(typeof(BodyController))]
	[HarmonyPatch("OnTriggerStay")]
	internal static class BapBggtVQ_zz0yHfrbAIedzYaFFBakH3QJO7dF00lWjCOcFjk_zIcZA_Kz6biNDdwkkAXNbVOkNevpVGmoKELcmR6abGlXLIqBcxN4_0024ZtbuT
	{
		[HarmonyPrefix]
		internal static void smethod_0(Collider LLLCAFCOBHB)
		{
			if (smethod_2(smethod_1((Component)LLLCAFCOBHB)) == fhrfRxVubQ7jQHGH5tZ1Xn4)
			{
				MeshRenderer component = LLLCAFCOBHB.GetComponent<MeshRenderer>();
				if (smethod_3((UnityEngine.Object)component, (UnityEngine.Object)null) && smethod_7(smethod_6((UnityEngine.Object)smethod_5(smethod_4((Renderer)component))), global::_003CModule_003E.smethod_25<string>(2455986016u)))
				{
					JKGKJLLFMLE.EFCELHBJFCJ = true;
					YDmVJiOSCLZVK6h7aRHycYc = smethod_8();
				}
			}
		}

		internal static GameObject smethod_1(Component component_0)
		{
			return component_0.gameObject;
		}

		internal static int smethod_2(GameObject gameObject_0)
		{
			return gameObject_0.layer;
		}

		internal static bool smethod_3(UnityEngine.Object object_0, UnityEngine.Object object_1)
		{
			return object_0 != object_1;
		}

		internal static Material smethod_4(Renderer renderer_0)
		{
			return renderer_0.material;
		}

		internal static Shader smethod_5(Material material_0)
		{
			return material_0.shader;
		}

		internal static string smethod_6(UnityEngine.Object object_0)
		{
			return object_0.name;
		}

		internal static bool smethod_7(string string_0, string string_1)
		{
			return string_0.Contains(string_1);
		}

		internal static float smethod_8()
		{
			return Time.realtimeSinceStartup;
		}
	}

	[HarmonyPatch(typeof(BodyController))]
	[HarmonyPatch("FixedUpdate")]
	internal static class ax_0024v9_0024SAexqtnivpn2xbJvBNQXgcREocz5Od3npk66lRA5IniL27VLMerN_Vm1J8hkDOs3YQv5i0zmQLIWIgFpE
	{
		[Serializable]
		[CompilerGenerated]
		private sealed class D57hV3mVkWAJiFW2N3NYS4vNN3TJ7APlc5vNbqYpW5TkfxTjsQpVludOJSZPCOXMEHj3PmBTUJkJK0rNKm_UBTfS5dDbf2eTIA0qc3KaHnOKUGHeJwx5NJDaXkLwidbbXD6awmhZpCVADHwvulaDtwGfPNfuwZgcFeHLTDR44fPuevzsXcZtuEEZduJOrICGPw
		{
			public static readonly D57hV3mVkWAJiFW2N3NYS4vNN3TJ7APlc5vNbqYpW5TkfxTjsQpVludOJSZPCOXMEHj3PmBTUJkJK0rNKm_UBTfS5dDbf2eTIA0qc3KaHnOKUGHeJwx5NJDaXkLwidbbXD6awmhZpCVADHwvulaDtwGfPNfuwZgcFeHLTDR44fPuevzsXcZtuEEZduJOrICGPw _003C_003E9 = new D57hV3mVkWAJiFW2N3NYS4vNN3TJ7APlc5vNbqYpW5TkfxTjsQpVludOJSZPCOXMEHj3PmBTUJkJK0rNKm_UBTfS5dDbf2eTIA0qc3KaHnOKUGHeJwx5NJDaXkLwidbbXD6awmhZpCVADHwvulaDtwGfPNfuwZgcFeHLTDR44fPuevzsXcZtuEEZduJOrICGPw();
		}

		[HarmonyTranspiler]
		internal static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
		{
			List<CodeInstruction> list = new List<CodeInstruction>();
			List<CodeInstruction> list2 = new List<CodeInstruction>(instructions);
			for (int i = 0; i < list2.Count; i++)
			{
				if (i < 62 || i >= 263)
				{
					list.Add(list2[i]);
					continue;
				}
				switch (i)
				{
				default:
					list.Add(smethod_5(OpCodes.Nop, (object)null));
					break;
				case 62:
				{
					MethodInfo object_ = ((Action<BodyController>)C8DNZpzKjPK7v0b5djavXno).Method;
					list.Add(smethod_5(OpCodes.Ldarg_0, (object)null));
					list.Add(smethod_5(OpCodes.Call, (object)object_));
					break;
				}
				case 63:
					break;
				}
			}
			return list;
		}

		internal static void C8DNZpzKjPK7v0b5djavXno(BodyController __instance)
		{
			int num = 0;
			float num2 = 0f;
			Vector3 zero = Vector3.zero;
			Vector3 vector = Vector3.up;
			Plane? plane = null;
			if (D_Vus4oHDsZlAhsVoCPEDbQ && Physics.Raycast(new Ray(smethod_7(smethod_6((Component)__instance)) + new Vector3(0f, 260f, 0f), -Vector3.up), out var hitInfo, 520f, 1 << fhrfRxVubQ7jQHGH5tZ1Xn4, QueryTriggerInteraction.UseGlobal))
			{
				MeshRenderer component = hitInfo.transform.GetComponent<MeshRenderer>();
				if (component != null && component.gameObject.layer == fhrfRxVubQ7jQHGH5tZ1Xn4 && component.material.shader.name.Contains(global::_003CModule_003E.smethod_29<string>(2761993531u)))
				{
					plane = new Plane(hitInfo.normal, hitInfo.point);
					vector = hitInfo.normal;
				}
			}
			for (int num3 = __instance.DEKDNJPGGOP.Count - 1; num3 >= 0; num3--)
			{
				float num4 = 0f;
				Vector3 vector2 = __instance.transform.TransformPoint(__instance.DEKDNJPGGOP[num3]);
				if (D_Vus4oHDsZlAhsVoCPEDbQ && plane.HasValue)
				{
					vector = plane.Value.normal;
					plane.Value.Raycast(new Ray(vector2, Vector3.up), out var enter);
					num4 = vector2.y + enter;
				}
				if (vector2.y < num4 + 0.5f)
				{
					float num5 = Mathf.Min(num4 + 0.5f - vector2.y, 1f);
					num2 += num5;
					zero += vector2 * num5;
					__instance.NFMPBACKJOJ.AddForceAtPosition(vector * num5 * __instance.BHGEHEHDDHN[num3], vector2);
					if (vector2.y > 0f - (num4 + 0.5f))
					{
						num++;
					}
				}
			}
			if (num2 > 0f)
			{
				Vector3 vector3 = zero / num2;
				Vector3 pointVelocity = __instance.NFMPBACKJOJ.GetPointVelocity(vector3);
				pointVelocity.y *= 2f;
				float num6 = Mathf.Sqrt(num2);
				__instance.NFMPBACKJOJ.AddForceAtPosition(pointVelocity * (0f - num6) * 0.001f * (pointVelocity.magnitude + 1f), vector3);
				float magnitude = __instance.NFMPBACKJOJ.angularVelocity.magnitude;
				__instance.NFMPBACKJOJ.angularVelocity /= num6 * magnitude * 0.002f + 1f;
				if (Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<int>(global::_003CModule_003E.smethod_25<string>(1775215293u), __instance) > 0)
				{
					Class17.DMoImhPC2jNUt2tgmSezeT_IK_0024XK5Df9PmexG_CaKb1R(global::_003CModule_003E.smethod_26<string>(245706346u), (object)__instance, Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<int>(global::_003CModule_003E.smethod_26<string>(245706346u), __instance) - 1);
				}
				else if (num > 0)
				{
					float num7 = pointVelocity.magnitude * 0.004f + magnitude * 0.1f;
					if (num7 > 0.1f)
					{
						float num8 = Mathf.Sqrt(num);
						num7 += num8 * 0.01f;
						float pDBNEGOFDCE = 1f / (num8 * 0.1f + 1f);
						KEFHJCGICLE.BKODCLJILAO(EELHGJDCAHF: true, global::_003CModule_003E.smethod_26<string>(4068580720u), __instance.NFMPBACKJOJ.worldCenterOfMass, 0f - num7, null, PAIECBEEBNH: false, pDBNEGOFDCE);
						Class17.DMoImhPC2jNUt2tgmSezeT_IK_0024XK5Df9PmexG_CaKb1R(global::_003CModule_003E.smethod_25<string>(1775215293u), (object)__instance, UnityEngine.Random.Range(20, 50));
					}
				}
			}
			if (num2 > 0f && !__instance.JBPBJMHLJOJ)
			{
				__instance.MCBBFDAECCI++;
			}
			else
			{
				__instance.MCBBFDAECCI = 0;
			}
		}

		internal static MethodBase smethod_0(RuntimeMethodHandle runtimeMethodHandle_0)
		{
			return MethodBase.GetMethodFromHandle(runtimeMethodHandle_0);
		}

		internal static Type smethod_1(RuntimeTypeHandle runtimeTypeHandle_0)
		{
			return Type.GetTypeFromHandle(runtimeTypeHandle_0);
		}

		internal static ConstantExpression smethod_2(object object_0, Type type_0)
		{
			return Expression.Constant(object_0, type_0);
		}

		internal static MethodCallExpression smethod_3(Expression expression_0, MethodInfo methodInfo_0, Expression[] expression_1)
		{
			return Expression.Call(expression_0, methodInfo_0, expression_1);
		}

		internal static MethodInfo smethod_4(Expression<Action> expression_0)
		{
			return SymbolExtensions.GetMethodInfo(expression_0);
		}

		internal static CodeInstruction smethod_5(OpCode opCode_0, object object_0)
		{
			return new CodeInstruction(opCode_0, object_0);
		}

		internal static Transform smethod_6(Component component_0)
		{
			return component_0.transform;
		}

		internal static Vector3 smethod_7(Transform transform_0)
		{
			return transform_0.position;
		}
	}

	[HarmonyPatch(typeof(AGDeviceController))]
	[HarmonyPatch("FixedUpdate")]
	internal static class AXdZOpi5bHv3uj8JPpkS5wIcK3ayxkAHNgE4_QMsyJPTUT_nJsJBN6sW_SsiQrOHtZTXdF7Scfi6AufhowjTYFc
	{
		[HarmonyTranspiler]
		internal static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
		{
			List<CodeInstruction> list = new List<CodeInstruction>();
			List<CodeInstruction> list2 = new List<CodeInstruction>(instructions);
			for (int i = 0; i < list2.Count; i++)
			{
				if (i == 56)
				{
					MethodInfo object_ = ((Func<PartsController, float>)UDIRjB3VSmwHpVmUxcnHH66kk6ThWzbxmySWu7OngspoKs_47sbCTTGVAxk9y7DBfQ.l4_0024McNj8pVEDHVqZjo40pELjXvIUF8ntff3BBikz8O0b).Method;
					list.Add(smethod_5(OpCodes.Ldarg_0, (object)null));
					list.Add(smethod_5(OpCodes.Call, (object)object_));
				}
				else
				{
					list.Add(list2[i]);
				}
			}
			mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_26<string>(1891207070u) + instructions.Count() + global::_003CModule_003E.smethod_28<string>(1860476032u) + list.Count(), bool_0: true);
			return list;
		}

		internal static MethodBase smethod_0(RuntimeMethodHandle runtimeMethodHandle_0)
		{
			return MethodBase.GetMethodFromHandle(runtimeMethodHandle_0);
		}

		internal static Type smethod_1(RuntimeTypeHandle runtimeTypeHandle_0)
		{
			return Type.GetTypeFromHandle(runtimeTypeHandle_0);
		}

		internal static ConstantExpression smethod_2(object object_0, Type type_0)
		{
			return Expression.Constant(object_0, type_0);
		}

		internal static MethodCallExpression smethod_3(Expression expression_0, MethodInfo methodInfo_0, Expression[] expression_1)
		{
			return Expression.Call(expression_0, methodInfo_0, expression_1);
		}

		internal static MethodInfo smethod_4(Expression<Action> expression_0)
		{
			return SymbolExtensions.GetMethodInfo(expression_0);
		}

		internal static CodeInstruction smethod_5(OpCode opCode_0, object object_0)
		{
			return new CodeInstruction(opCode_0, object_0);
		}
	}

	[HarmonyPatch(typeof(ThrusterController))]
	[HarmonyPatch("FixedUpdate")]
	internal static class DMzITGEX1p2AB0rI_0024ovDSLwwvTLgzIan63Pk608ygcwFJlX_0024WMVZm7f_xImiWySFnaP5WMg6Whrcdrlw48W9ZaA
	{
		[HarmonyTranspiler]
		internal static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
		{
			List<CodeInstruction> list = new List<CodeInstruction>();
			List<CodeInstruction> list2 = new List<CodeInstruction>(instructions);
			for (int i = 0; i < list2.Count; i++)
			{
				if (i != 18)
				{
					list.Add(list2[i]);
					continue;
				}
				MethodInfo object_ = ((Func<PartsController, float>)UDIRjB3VSmwHpVmUxcnHH66kk6ThWzbxmySWu7OngspoKs_47sbCTTGVAxk9y7DBfQ.l4_0024McNj8pVEDHVqZjo40pELjXvIUF8ntff3BBikz8O0b).Method;
				list.Add(smethod_5(OpCodes.Ldarg_0, (object)null));
				list.Add(smethod_5(OpCodes.Call, (object)object_));
			}
			mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_26<string>(928904756u) + instructions.Count() + global::_003CModule_003E.smethod_26<string>(887241222u) + list.Count(), bool_0: true);
			return list;
		}

		internal static MethodBase smethod_0(RuntimeMethodHandle runtimeMethodHandle_0)
		{
			return MethodBase.GetMethodFromHandle(runtimeMethodHandle_0);
		}

		internal static Type smethod_1(RuntimeTypeHandle runtimeTypeHandle_0)
		{
			return Type.GetTypeFromHandle(runtimeTypeHandle_0);
		}

		internal static ConstantExpression smethod_2(object object_0, Type type_0)
		{
			return Expression.Constant(object_0, type_0);
		}

		internal static MethodCallExpression smethod_3(Expression expression_0, MethodInfo methodInfo_0, Expression[] expression_1)
		{
			return Expression.Call(expression_0, methodInfo_0, expression_1);
		}

		internal static MethodInfo smethod_4(Expression<Action> expression_0)
		{
			return SymbolExtensions.GetMethodInfo(expression_0);
		}

		internal static CodeInstruction smethod_5(OpCode opCode_0, object object_0)
		{
			return new CodeInstruction(opCode_0, object_0);
		}
	}

	[HarmonyPatch("FixedUpdate")]
	[HarmonyPatch(typeof(BallController))]
	internal static class D_0024dw1eRPSF4yp_0024UxZQjRCroUoH1XOK81Sbvq7nAuOLIvqdVTFuN0_CQ1eoBpHnAWuq78kcigmAxkLL_0024mzPOOsQGDl1vJkX854bUCHitdZUda
	{
		[HarmonyTranspiler]
		internal static IEnumerable<CodeInstruction> ME8a9fXTPeYc1JTUjzrWW9E(IEnumerable<CodeInstruction> instructions)
		{
			List<CodeInstruction> list = new List<CodeInstruction>();
			List<CodeInstruction> list2 = new List<CodeInstruction>(instructions);
			list.Add(list2[98]);
			list.Add(list2[99]);
			list.Add(list2[100]);
			return list;
		}

		[HarmonyPrefix]
		internal static void smethod_0(BallController __instance)
		{
			if (!__instance.FLNDCNECIOI)
			{
				return;
			}
			float num = __instance.i0duGIJ6M7BXC7c2cbeyvXM1hCrcoNKNJzlqDrhwjoxM<float>(global::_003CModule_003E.smethod_26<string>(85322627u));
			float num2 = 0f;
			Vector3 vector = Vector3.up;
			int layerMask = 1 << fhrfRxVubQ7jQHGH5tZ1Xn4;
			if (Physics.Raycast(new Ray(smethod_2(smethod_1((Component)__instance)) + new Vector3(0f, 260f, 0f), -Vector3.up), out var hitInfo, 256f, layerMask, QueryTriggerInteraction.UseGlobal))
			{
				MeshRenderer component = hitInfo.transform.GetComponent<MeshRenderer>();
				if (component != null && component.material.shader.name.Contains(global::_003CModule_003E.smethod_25<string>(2455986016u)) && hitInfo.point.y > num2)
				{
					num2 = hitInfo.point.y;
					if (hitInfo.normal.y != 1f)
					{
						vector = hitInfo.normal;
					}
				}
			}
			if (JKGKJLLFMLE.EFCELHBJFCJ && __instance.transform.position.y < num + num2)
			{
				float num3 = Mathf.Max(num - (__instance.transform.position.y - num2), num) / num;
				float num4 = 1f - num3 * 0.01f;
				Vector3 velocity = __instance.NFMPBACKJOJ.velocity;
				velocity.x *= 0.95f;
				velocity.y *= num4;
				velocity.z *= 0.95f;
				__instance.NFMPBACKJOJ.velocity = velocity;
				__instance.NFMPBACKJOJ.angularVelocity *= num4;
				__instance.NFMPBACKJOJ.velocity += vector * num3 * 0.7f;
				__instance.NFMPBACKJOJ.angularVelocity += Vector3.Cross(vector, __instance.NFMPBACKJOJ.velocity) * (1f - num3) * -0.01f;
			}
		}

		internal static Transform smethod_1(Component component_0)
		{
			return component_0.transform;
		}

		internal static Vector3 smethod_2(Transform transform_0)
		{
			return transform_0.position;
		}
	}

	[HarmonyPatch(typeof(BombController))]
	[HarmonyPatch("FixedUpdate")]
	internal static class BOj4l6TYT0cMEcyp0kn5iLxmWghs2F_JIYskR3PIcMxl4xbAs397F0zWr4dk0O_0024ULOVIwZz4usFBzgOdwfmatzvaIFXepmg0__0024tREydAHVhF
	{
		[HarmonyTranspiler]
		internal static IEnumerable<CodeInstruction> ME8a9fXTPeYc1JTUjzrWW9E(IEnumerable<CodeInstruction> instructions)
		{
			List<CodeInstruction> list = new List<CodeInstruction>();
			List<CodeInstruction> list2 = new List<CodeInstruction>(instructions);
			list.Add(list2[138]);
			list.Add(list2[139]);
			list.Add(list2[140]);
			return list;
		}

		[HarmonyPrefix]
		internal static void smethod_0(BombController __instance)
		{
			if (!__instance.FLNDCNECIOI)
			{
				return;
			}
			float num = 0f;
			Vector3 vector = Vector3.up;
			int layerMask = 1 << fhrfRxVubQ7jQHGH5tZ1Xn4;
			if (Physics.Raycast(new Ray(smethod_2(smethod_1((Component)__instance)) + new Vector3(0f, 260f, 0f), -Vector3.up), out var hitInfo, 256f, layerMask, QueryTriggerInteraction.UseGlobal))
			{
				MeshRenderer component = hitInfo.transform.GetComponent<MeshRenderer>();
				if (component != null && component.material.shader.name.Contains(global::_003CModule_003E.smethod_26<string>(2316900645u)) && hitInfo.point.y > num)
				{
					num = hitInfo.point.y;
					if (hitInfo.normal.y != 1f)
					{
						vector = hitInfo.normal;
					}
				}
			}
			if (!JKGKJLLFMLE.EFCELHBJFCJ || !(__instance.transform.position.y < num + 9f))
			{
				return;
			}
			float num2 = 0f;
			Vector3 position = default(Vector3);
			for (int i = 0; i < 8; i++)
			{
				position.x = (float)((i & 1) * 10) - 5f;
				position.y = ((i >> 1) & 1) * 10;
				position.z = (float)(((i >> 2) & 1) * 10) - 5f;
				Vector3 position2 = __instance.transform.TransformPoint(position);
				if (position2.y < num)
				{
					float num3 = Mathf.Min(0f - (position2.y - num), num + 9f);
					num2 += num3;
					__instance.NFMPBACKJOJ.AddForceAtPosition(vector * num3 * 0.05f, position2);
				}
			}
			if (num2 > 0f)
			{
				__instance.NFMPBACKJOJ.AddForce(vector * num2 * 0.2f);
				Vector3 velocity = __instance.NFMPBACKJOJ.velocity;
				velocity.x *= 0.95f;
				velocity.y *= 1f - num2 * 0.0005f;
				velocity.z *= 0.95f;
				__instance.NFMPBACKJOJ.velocity = velocity;
				__instance.NFMPBACKJOJ.angularVelocity *= 1f - num2 * 0.005f;
			}
		}

		internal static Transform smethod_1(Component component_0)
		{
			return component_0.transform;
		}

		internal static Vector3 smethod_2(Transform transform_0)
		{
			return transform_0.position;
		}
	}

	internal static readonly int fhrfRxVubQ7jQHGH5tZ1Xn4 = 3;

	private static float YDmVJiOSCLZVK6h7aRHycYc = 0f;

	internal static bool D_Vus4oHDsZlAhsVoCPEDbQ => smethod_0() - YDmVJiOSCLZVK6h7aRHycYc < 0.05f;

	internal static float l4_0024McNj8pVEDHVqZjo40pELjXvIUF8ntff3BBikz8O0b(PartsController pc)
	{
		return pc.l4_0024McNj8pVEDHVqZjo40pELjXvIUF8ntff3BBikz8O0b(startAtHalf: true);
	}

	private static float l4_0024McNj8pVEDHVqZjo40pELjXvIUF8ntff3BBikz8O0b(this PartsController pc, bool startAtHalf = true)
	{
		float num = 0.5f;
		if (!startAtHalf)
		{
			num = 0f;
		}
		if (D_Vus4oHDsZlAhsVoCPEDbQ)
		{
			int layerMask = 1 << fhrfRxVubQ7jQHGH5tZ1Xn4;
			if (Physics.Raycast(new Ray(smethod_2(smethod_1((Component)pc)) + new Vector3(0f, 260f, 0f), -Vector3.up), out var hitInfo, 520f, layerMask, QueryTriggerInteraction.UseGlobal))
			{
				MeshRenderer component = hitInfo.transform.GetComponent<MeshRenderer>();
				if (component != null && component.gameObject.layer == fhrfRxVubQ7jQHGH5tZ1Xn4 && component.material.shader.name.Contains(global::_003CModule_003E.smethod_28<string>(406629963u)) && hitInfo.point.y > num)
				{
					num = hitInfo.point.y;
				}
			}
		}
		return num;
	}

	internal static float smethod_0()
	{
		return Time.realtimeSinceStartup;
	}

	internal static Transform smethod_1(Component component_0)
	{
		return component_0.transform;
	}

	internal static Vector3 smethod_2(Transform transform_0)
	{
		return transform_0.position;
	}
}
