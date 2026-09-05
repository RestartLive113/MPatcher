using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using HarmonyLib;
using MPatchrMain;
using McnCraft;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

internal static class p15TFflpW2KKy78hPxhNbiPljDEt9MtSH5pTlOs7Y_0024LnvAl3Cjkk3Gzzu32qTCrlBA
{
	[HarmonyPatch(typeof(MachineController))]
	[HarmonyPatch("HFLENBHPABB")]
	internal static class LeFDAbekdb1DVzsC9Sq2VK4486SIi5z3e5avCWG8PcqAHGtNzs1OTSIuQemA0JEOu6zX7L358Mt7rDWuL_fWmlN7duiNUTAwRxnPLgUHxkCJ9tl4z_ETCd2yM9zebzkk_Q
	{
		[HarmonyPrefix]
		internal static void smethod_0()
		{
			mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_29<string>(790347611u), bool_0: true);
			wt0lWN0P7W6H0Viq8pkjxxQ = true;
		}
	}

	[HarmonyPatch(typeof(Build))]
	[HarmonyPatch(new Type[]
	{
		typeof(bool),
		typeof(bool)
	})]
	[HarmonyPatch("ANDINIMKBLL")]
	internal class E0sz_00246BEV0Xn5QLzeZaAyotYeonhEkrEXu1BivzXDxm3UQrMTcPL_Wkpghp_vPQurQaYhPLhbgKl6S2G1y_00240H_0024c9AUbT3cbQtx50VWX_2DefyqROcpK_0024dgDnVkj9CesScg
	{
		[Serializable]
		[CompilerGenerated]
		private sealed class Class18
		{
			public static readonly Class18 _003C_003E9 = new Class18();

			public static Action _003C_003E9__0_0;

			internal void aJB1bIYDRm_0024alz4_00246Y6_epI()
			{
				if (smethod_0((UnityEngine.Object)vVn7afGBU8BLmNjGePpOYo89VaBf_0024RaeOHzWZmSqv8cA, (UnityEngine.Object)null))
				{
					return;
				}
				BlockData blockData_ = aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.BlockData_0;
				BlockController[,,] array = vVn7afGBU8BLmNjGePpOYo89VaBf_0024RaeOHzWZmSqv8cA.GetComponentInChildren<MachineController>().EPGELCMKKOC.i0duGIJ6M7BXC7c2cbeyvXM1hCrcoNKNJzlqDrhwjoxM<BlockController[,,]>(global::_003CModule_003E.smethod_26<string>(740374508u));
				smethod_1(array[blockData_.x + 49, blockData_.y, blockData_.z + 49].JNKEKNOAPHO, blockData_);
				array[blockData_.x + 49, blockData_.y, blockData_.z + 49].i0duGIJ6M7BXC7c2cbeyvXM1hCrcoNKNJzlqDrhwjoxM<List<PartsController.HILPGPFCIGP>>(global::_003CModule_003E.smethod_26<string>(2106771328u)).Clear();
				array[blockData_.x + 49, blockData_.y, blockData_.z + 49].D7dqMa4OGuhdUnXHjJZM6vrBYDmyw_0024HWYteNzGVsTh5k<PartsController.HILPGPFCIGP>(global::_003CModule_003E.smethod_25<string>(1652262491u), null);
				(array[blockData_.x + 49, blockData_.y, blockData_.z + 49] as PartsController).mMOtaBaCAdAJENkfiJ_1fdbLwBEsBnP8lprp9wMogCpK(global::_003CModule_003E.smethod_27<string>(3096492097u));
				foreach (PartsController.HILPGPFCIGP item in vVn7afGBU8BLmNjGePpOYo89VaBf_0024RaeOHzWZmSqv8cA.GetComponentInChildren<MachineController>().HINGJCGNNEO)
				{
					item.ENEHEJFCFLC = 2;
					item.FOKGJIMFGGN = 0;
					item.CPHBKJKAJED = 0;
				}
				bool_0 = true;
			}

			internal static bool smethod_0(UnityEngine.Object object_0, UnityEngine.Object object_1)
			{
				return object_0 == object_1;
			}

			internal static void smethod_1(BlockData blockData_0, BlockData blockData_1)
			{
				blockData_0.CopyAction(blockData_1);
			}
		}

		[HarmonyPostfix]
		internal static void FeUAVwFbW6wGJJdNimZY9yI()
		{
			smethod_0((MonoBehaviour)MPatchr.xcBvxcM_0024ckBeZyvdSoAkJoM, H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.Thlobfo2pnKXbCcdII7cOic(0.5f, delegate
			{
				if (!Class18.smethod_0((UnityEngine.Object)vVn7afGBU8BLmNjGePpOYo89VaBf_0024RaeOHzWZmSqv8cA, (UnityEngine.Object)null))
				{
					BlockData blockData_ = aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.BlockData_0;
					BlockController[,,] array = vVn7afGBU8BLmNjGePpOYo89VaBf_0024RaeOHzWZmSqv8cA.GetComponentInChildren<MachineController>().EPGELCMKKOC.i0duGIJ6M7BXC7c2cbeyvXM1hCrcoNKNJzlqDrhwjoxM<BlockController[,,]>(global::_003CModule_003E.smethod_26<string>(740374508u));
					Class18.smethod_1(array[blockData_.x + 49, blockData_.y, blockData_.z + 49].JNKEKNOAPHO, blockData_);
					array[blockData_.x + 49, blockData_.y, blockData_.z + 49].i0duGIJ6M7BXC7c2cbeyvXM1hCrcoNKNJzlqDrhwjoxM<List<PartsController.HILPGPFCIGP>>(global::_003CModule_003E.smethod_26<string>(2106771328u)).Clear();
					array[blockData_.x + 49, blockData_.y, blockData_.z + 49].D7dqMa4OGuhdUnXHjJZM6vrBYDmyw_0024HWYteNzGVsTh5k<PartsController.HILPGPFCIGP>(global::_003CModule_003E.smethod_25<string>(1652262491u), null);
					(array[blockData_.x + 49, blockData_.y, blockData_.z + 49] as PartsController).mMOtaBaCAdAJENkfiJ_1fdbLwBEsBnP8lprp9wMogCpK(global::_003CModule_003E.smethod_27<string>(3096492097u));
					foreach (PartsController.HILPGPFCIGP item in vVn7afGBU8BLmNjGePpOYo89VaBf_0024RaeOHzWZmSqv8cA.GetComponentInChildren<MachineController>().HINGJCGNNEO)
					{
						item.ENEHEJFCFLC = 2;
						item.FOKGJIMFGGN = 0;
						item.CPHBKJKAJED = 0;
					}
					bool_0 = true;
				}
			}));
		}

		internal static Coroutine smethod_0(MonoBehaviour monoBehaviour_0, IEnumerator ienumerator_0)
		{
			return monoBehaviour_0.StartCoroutine(ienumerator_0);
		}
	}

	[HarmonyPatch(typeof(MachineController))]
	[HarmonyPatch("Update")]
	internal static class TwmqlqBLWYBXcErN_ZNGWmgM8LpwpZOQQMFY4Fl_OimudZrpOpZDbnRkhz3D2Xf1HpM9guPECfD_QPxCI3nO_P_jtL6ErlOPB1BrsZKXsO0nbLdyJzo2Fwmz9_0024_g1NeqZQ
	{
		[HarmonyTranspiler]
		internal static IEnumerable<CodeInstruction> ME8a9fXTPeYc1JTUjzrWW9E(IEnumerable<CodeInstruction> instructions)
		{
			List<CodeInstruction> list = new List<CodeInstruction>(instructions.ToArray());
			list[378].opcode = OpCodes.Nop;
			list[379].opcode = OpCodes.Nop;
			list[380].opcode = OpCodes.Nop;
			return list;
		}
	}

	[Serializable]
	[CompilerGenerated]
	private sealed class B10q8uby4y8GaSf_Y_0024u7RDZieDjbqDWT9_0024ArVHxvPlqo1Q0FiReTEriv8DVdnsyA3OhBFwoMR39TKuSP6Yw0DUem5cFtiv_0024Jnfz8K26I5nVu
	{
		public static readonly B10q8uby4y8GaSf_Y_0024u7RDZieDjbqDWT9_0024ArVHxvPlqo1Q0FiReTEriv8DVdnsyA3OhBFwoMR39TKuSP6Yw0DUem5cFtiv_0024Jnfz8K26I5nVu _003C_003E9 = new B10q8uby4y8GaSf_Y_0024u7RDZieDjbqDWT9_0024ArVHxvPlqo1Q0FiReTEriv8DVdnsyA3OhBFwoMR39TKuSP6Yw0DUem5cFtiv_0024Jnfz8K26I5nVu();

		public static Action<lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw> _003C_003E9__13_0;

		public static Action<lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw> _003C_003E9__13_1;

		public static Action<lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw> _003C_003E9__13_5;

		public static Action _003C_003E9__14_0;

		public static Action<AmMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F> _003C_003E9__14_1;

		internal void IFbGAkWGtRl7fR1dZ3_0024b6ixNqEdLBEn213X4oBjnF5i3(lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw btn)
		{
			if (smethod_1((Selectable)smethod_0(SceneMan.JFAOKFIDAGK, global::_003CModule_003E.smethod_29<string>(227720855u)).GetComponent<Button>()))
			{
				smethod_2((MonoBehaviour)MPatchr.xcBvxcM_0024ckBeZyvdSoAkJoM, yLd1c6Ma8pKtWU8tHQUuzO58jr1MMkDNlceIdNNTV40R());
			}
		}

		internal void IWQxiEgRhSgrSTpzO4n3D1rgoeday30GDgjG406kMZnQ(lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw me)
		{
			egSFr3vBzo8SOVsjZcvWlkA();
		}

		internal void JUKzPgyrsQq7yBPHMdEmy_GzYhn0BMFv_jm3UiQ3yqSM(lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw me)
		{
			smethod_3((Behaviour)SceneMan.JFAOKFIDAGK, bool_0: true);
		}

		internal void KIxPFT6c4R8mf5O_0024_0024zuxEV5_adOpSqzAFS7hQxtnEXKB9Iyp35LWiAxBNx0OsXpVxA()
		{
			float num = smethod_4();
			SceneMan.JFAOKFIDAGK.mMOtaBaCAdAJENkfiJ_1fdbLwBEsBnP8lprp9wMogCpK(global::_003CModule_003E.smethod_27<string>(392941520u), (float)(smethod_5() / 2 - 100), (float)(smethod_6() / 2 - 16), global::_003CModule_003E.smethod_28<string>(3455580473u), 1f, 32, Mathf.Sin(num), Mathf.Sin(num + (float)Math.PI * 2f / 3f), Mathf.Sin(num + 4.1887903f));
		}

		internal void KcWkp6cdo8jVwe6U1_CBZQPpSPYdk9NIofyEIId6azbuVEacnl_JkrFOb8KIHzhgHw(AmMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F me)
		{
			HOCGCCAIPFF.IICBFEKPLJL = false;
			if (bool_0)
			{
				bool_0 = false;
				HOCGCCAIPFF.JNPMJANFGNH = 0uL;
			}
			else if (_0024_r0hqHHH7HaViWiREzGiV8)
			{
				HOCGCCAIPFF.JNPMJANFGNH = SLn0VTFkPWFtkFzCvegsS6I;
			}
		}

		internal static GameObject smethod_0(SceneMan sceneMan_0, string string_0)
		{
			return sceneMan_0.GetBTN(string_0);
		}

		internal static bool smethod_1(Selectable selectable_0)
		{
			return selectable_0.interactable;
		}

		internal static Coroutine smethod_2(MonoBehaviour monoBehaviour_0, IEnumerator ienumerator_0)
		{
			return monoBehaviour_0.StartCoroutine(ienumerator_0);
		}

		internal static void smethod_3(Behaviour behaviour_0, bool bool_0)
		{
			behaviour_0.enabled = bool_0;
		}

		internal static float smethod_4()
		{
			return Time.realtimeSinceStartup;
		}

		internal static int smethod_5()
		{
			return Screen.width;
		}

		internal static int smethod_6()
		{
			return Screen.height;
		}
	}

	[CompilerGenerated]
	private sealed class v4H1aHdOhdQg4cvr6BvZhiITrQ2dCVPB1bgNxrId41GgrLRkxqlgucsnI5_mIn_0024RbSr0oOIA3ksX17gcMWYjtBIw7137znJVms0DHx059ZDTRLXtD64wkmtmu_0024YUVLB1Ng
	{
		public Text o8zhWMp6JqD2MQkqYrKk5_0024E;

		internal void GrQydrcWZbF_HEC_0024i276Vuy4rMSSOkAJ7tObXwcS9Njh(AmMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F me)
		{
			if (!me.AqTKGFxfR1r6eAzrvm4_0024bck(global::_003CModule_003E.smethod_29<string>(833532274u)))
			{
				me.clf0br4v0HxmaJWIcm_0024_0024GUg(global::_003CModule_003E.smethod_26<string>(698710974u), 1f);
			}
			if (smethod_0())
			{
				if (smethod_1(KeyCode.Escape))
				{
					_0024_r0hqHHH7HaViWiREzGiV8 = false;
					smethod_2((Behaviour)SceneMan.JFAOKFIDAGK, bool_0: true);
					smethod_4((UnityEngine.Object)smethod_3((Component)me));
				}
				float num = me.hpiqzm2jQTswCo32f7jvrQ4<float>(global::_003CModule_003E.smethod_25<string>(3845030265u)) - smethod_5();
				if (num < 0f)
				{
					smethod_6();
					SLn0VTFkPWFtkFzCvegsS6I = HOCGCCAIPFF.JNPMJANFGNH;
					_0024_r0hqHHH7HaViWiREzGiV8 = true;
					smethod_2((Behaviour)SceneMan.JFAOKFIDAGK, bool_0: true);
					smethod_4((UnityEngine.Object)smethod_3((Component)me));
				}
				me.clf0br4v0HxmaJWIcm_0024_0024GUg(global::_003CModule_003E.smethod_27<string>(2510171775u), num);
				o8zhWMp6JqD2MQkqYrKk5_0024E.text = global::_003CModule_003E.smethod_27<string>(1737728753u) + Mathf.Round(num * 100f) + global::_003CModule_003E.smethod_29<string>(3963672339u);
			}
			else
			{
				me.clf0br4v0HxmaJWIcm_0024_0024GUg(global::_003CModule_003E.smethod_27<string>(2510171775u), 1f);
				o8zhWMp6JqD2MQkqYrKk5_0024E.text = global::_003CModule_003E.smethod_25<string>(4199365659u);
			}
		}

		internal static bool smethod_0()
		{
			return Input.anyKey;
		}

		internal static bool smethod_1(KeyCode keyCode_0)
		{
			return Input.GetKeyDown(keyCode_0);
		}

		internal static void smethod_2(Behaviour behaviour_0, bool bool_0)
		{
			behaviour_0.enabled = bool_0;
		}

		internal static GameObject smethod_3(Component component_0)
		{
			return component_0.gameObject;
		}

		internal static void smethod_4(UnityEngine.Object object_0)
		{
			UnityEngine.Object.Destroy(object_0);
		}

		internal static float smethod_5()
		{
			return Time.deltaTime;
		}

		internal static void smethod_6()
		{
			HOCGCCAIPFF.HAMLNBEBDOB();
		}
	}

	[CompilerGenerated]
	private sealed class Nr0ejQEJWARWwtZZB_00249GfUBqyyljzMsUnfAnxDRTiq6FsEmoLfGoP6Rs9L33v2Km_6Mn6injxUPScWPxZpO_L_0024yzs16fI0BfKoO1J_ZnCbeF
	{
		public GameObject E_pRMsiwL0e7z38GQ0yp_0024z4;

		public FreeCameraController N5h4vPyAJx53Xmfvxm5_00240NH7b636sL0lvXBukG7KGCKY;

		public GameObject vHTkjhuQ_n117aM_fukkeqw;

		public RectTransform POrzo6yL6vzrGXopoAufEQQ;

		public AmMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F cM12unMzt4mdBs_00241zt16EoE;

		internal void mecCysFcBI0cZT0CqfJLVCxrD4dGo4kFBtUSXdzZBoIw(lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw me)
		{
			smethod_0((UnityEngine.Object)vVn7afGBU8BLmNjGePpOYo89VaBf_0024RaeOHzWZmSqv8cA);
			smethod_0((UnityEngine.Object)E_pRMsiwL0e7z38GQ0yp_0024z4);
		}

		internal void mqLum776M_0024w1Kl7DK5BnuecPDWZD2KsCGjitG3f8IW1h(AmMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F me)
		{
			N5h4vPyAJx53Xmfvxm5_00240NH7b636sL0lvXBukG7KGCKY.EBOMCDGDKCP = camera_0;
			N5h4vPyAJx53Xmfvxm5_00240NH7b636sL0lvXBukG7KGCKY.D7dqMa4OGuhdUnXHjJZM6vrBYDmyw_0024HWYteNzGVsTh5k(global::_003CModule_003E.smethod_29<string>(3072936598u), vHTkjhuQ_n117aM_fukkeqw.smethod_0(global::_003CModule_003E.smethod_26<string>(2706642670u)));
			N5h4vPyAJx53Xmfvxm5_00240NH7b636sL0lvXBukG7KGCKY.FMGOKAGJMJH = false;
		}

		internal void myEXZ8gyWrCuVuORyeesJYF2figNpNT68qhF4EEiR5dR(lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw me)
		{
			if (!smethod_1(SystemData.EHLMFKOOHLI.Modifier))
			{
				bool_0 = true;
			}
			else
			{
				smethod_3(smethod_2(vHTkjhuQ_n117aM_fukkeqw), _00248_os5izJ7wHEiF3KBq8NWnTQOew_VLoMlU_nbp_Z9wx);
			}
		}

		internal void nWdmKhnsHKpegNqJhmFlgOOEy5Cpq5u22jGpFkFJkbS5(lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw me)
		{
			E_pRMsiwL0e7z38GQ0yp_0024z4.GetComponent<AmMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F>().clf0br4v0HxmaJWIcm_0024_0024GUg(global::_003CModule_003E.smethod_26<string>(57176098u), smethod_4(E_pRMsiwL0e7z38GQ0yp_0024z4.GetComponent<RectTransform>()));
			E_pRMsiwL0e7z38GQ0yp_0024z4.GetComponent<AmMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F>().clf0br4v0HxmaJWIcm_0024_0024GUg(global::_003CModule_003E.smethod_27<string>(1304976567u), smethod_8(smethod_6(smethod_5(global::_003CModule_003E.smethod_25<string>(806627754u)).GetComponent<Canvas>()), smethod_7()));
			E_pRMsiwL0e7z38GQ0yp_0024z4.GetComponent<AmMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F>().clf0br4v0HxmaJWIcm_0024_0024GUg(global::_003CModule_003E.smethod_26<string>(1744340356u), gparam_0: true);
		}

		internal void ngmxa6q5OrLzFixcZOzETbA_0024waHjq_gLAax_0024yXlrsV2W(AmMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F me)
		{
			if (smethod_9(1))
			{
				Vector2 vector2_ = default(Vector2);
				smethod_10(POrzo6yL6vzrGXopoAufEQQ, (Vector2)smethod_7(), smethod_6(smethod_5(global::_003CModule_003E.smethod_29<string>(618767629u)).GetComponent<Canvas>()), ref vector2_);
				if (smethod_11(POrzo6yL6vzrGXopoAufEQQ).Contains(vector2_))
				{
					N5h4vPyAJx53Xmfvxm5_00240NH7b636sL0lvXBukG7KGCKY.FMGOKAGJMJH = true;
					SceneMan.JFAOKFIDAGK.enabled = false;
				}
			}
			else if (Input.GetMouseButtonUp(1))
			{
				N5h4vPyAJx53Xmfvxm5_00240NH7b636sL0lvXBukG7KGCKY.FMGOKAGJMJH = false;
				SceneMan.JFAOKFIDAGK.enabled = true;
			}
		}

		internal void n4GlwMrWT9d2ilqSQgG5tk_0024yhyrQHU5OuBfzUOxJ0C_c(AmMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F me)
		{
			if (smethod_9(0))
			{
				Vector2 vector2_ = default(Vector2);
				smethod_10(POrzo6yL6vzrGXopoAufEQQ, (Vector2)smethod_7(), smethod_6(smethod_5(global::_003CModule_003E.smethod_27<string>(862174272u)).GetComponent<Canvas>()), ref vector2_);
				if (!smethod_11(POrzo6yL6vzrGXopoAufEQQ).Contains(vector2_))
				{
					return;
				}
				me.clf0br4v0HxmaJWIcm_0024_0024GUg(global::_003CModule_003E.smethod_26<string>(1702676822u), GameObject.Find(global::_003CModule_003E.smethod_28<string>(3214927773u)).GetComponent<Canvas>().worldCamera.ScreenToWorldPoint(Input.mousePosition));
				me.clf0br4v0HxmaJWIcm_0024_0024GUg(global::_003CModule_003E.smethod_27<string>(3422206822u), me.GetComponent<RectTransform>().position);
				me.clf0br4v0HxmaJWIcm_0024_0024GUg(global::_003CModule_003E.smethod_25<string>(514563285u), me.GetComponent<RectTransform>().sizeDelta);
				cM12unMzt4mdBs_00241zt16EoE.clf0br4v0HxmaJWIcm_0024_0024GUg(global::_003CModule_003E.smethod_27<string>(3143022772u), gparam_0: true);
			}
			else if (Input.GetMouseButtonUp(0))
			{
				cM12unMzt4mdBs_00241zt16EoE.clf0br4v0HxmaJWIcm_0024_0024GUg(global::_003CModule_003E.smethod_28<string>(541663641u), gparam_0: false);
				cM12unMzt4mdBs_00241zt16EoE.clf0br4v0HxmaJWIcm_0024_0024GUg(global::_003CModule_003E.smethod_25<string>(2089228523u), gparam_0: false);
			}
			if (!cM12unMzt4mdBs_00241zt16EoE.hpiqzm2jQTswCo32f7jvrQ4<bool>(global::_003CModule_003E.smethod_29<string>(1818129097u)))
			{
				if (cM12unMzt4mdBs_00241zt16EoE.hpiqzm2jQTswCo32f7jvrQ4<bool>(global::_003CModule_003E.smethod_28<string>(268483938u)))
				{
					Vector3 vector = GameObject.Find(global::_003CModule_003E.smethod_29<string>(618767629u)).GetComponent<Canvas>().worldCamera.ScreenToWorldPoint(Input.mousePosition) - me.hpiqzm2jQTswCo32f7jvrQ4<Vector3>(global::_003CModule_003E.smethod_26<string>(1702676822u));
					float num = Mathf.Max(vector.x, 0f - vector.y);
					me.GetComponent<RectTransform>().sizeDelta = me.hpiqzm2jQTswCo32f7jvrQ4<Vector2>(global::_003CModule_003E.smethod_25<string>(514563285u)) + new Vector2(num, num);
					Vector2 sizeDelta = me.GetComponent<RectTransform>().sizeDelta;
					if (sizeDelta.x < 300f || sizeDelta.y < 300f)
					{
						me.GetComponent<RectTransform>().sizeDelta = new Vector2(300f, 300f);
					}
				}
			}
			else
			{
				Vector3 vector2 = GameObject.Find(global::_003CModule_003E.smethod_25<string>(806627754u)).GetComponent<Canvas>().worldCamera.ScreenToWorldPoint(Input.mousePosition) - me.hpiqzm2jQTswCo32f7jvrQ4<Vector3>(global::_003CModule_003E.smethod_25<string>(1479063601u));
				me.GetComponent<RectTransform>().position = me.hpiqzm2jQTswCo32f7jvrQ4<Vector3>(global::_003CModule_003E.smethod_26<string>(2671249553u)) + new Vector3(vector2.x, vector2.y);
			}
		}

		internal static void smethod_0(UnityEngine.Object object_0)
		{
			UnityEngine.Object.Destroy(object_0);
		}

		internal static bool smethod_1(SystemData.EHLMFKOOHLI ehlmfkoohli_0)
		{
			return HOCGCCAIPFF.AFLJECMLJDL(ehlmfkoohli_0);
		}

		internal static Transform smethod_2(GameObject gameObject_0)
		{
			return gameObject_0.transform;
		}

		internal static void smethod_3(Transform transform_0, Vector3 vector3_0)
		{
			transform_0.position = vector3_0;
		}

		internal static Vector2 smethod_4(RectTransform rectTransform_0)
		{
			return rectTransform_0.sizeDelta;
		}

		internal static GameObject smethod_5(string string_0)
		{
			return GameObject.Find(string_0);
		}

		internal static Camera smethod_6(Canvas canvas_0)
		{
			return canvas_0.worldCamera;
		}

		internal static Vector3 smethod_7()
		{
			return Input.mousePosition;
		}

		internal static Vector3 smethod_8(Camera camera_0, Vector3 vector3_0)
		{
			return camera_0.ScreenToWorldPoint(vector3_0);
		}

		internal static bool smethod_9(int int_0)
		{
			return Input.GetMouseButtonDown(int_0);
		}

		internal static bool smethod_10(RectTransform rectTransform_0, Vector2 vector2_0, Camera camera_0, ref Vector2 vector2_1)
		{
			return RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform_0, vector2_0, camera_0, out vector2_1);
		}

		internal static Rect smethod_11(RectTransform rectTransform_0)
		{
			return rectTransform_0.rect;
		}
	}

	[CompilerGenerated]
	private sealed class Q8DI9Mhi0jQAuK5ny1lJkZlcqZU_jeULJyu84Zz9jWjJjHiimw9ig6Zvr3jgms8_kYsaXFtByngobg1ZGDCb3sFVFfvI_0024iM_00241dPAgQsL5PffDYYCOZU8PNM6aPoqN6Up27oTMQIOJ3U_0024JwMhFbyDf8g : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int SjlBM8inVA_YE4YVlr_0024gluY;

		private object yT7HpVIzmqW54W307WgJtr4;

		private bool jykyL9tTi_002432Dr2zwoVB6RdRcudGFA7lwjFS1IxFdul6;

		private bool FZNwATyeqQEjPQAwrxpYbCOsXta__u3KwiGirfzsTe60;

		private Vector3 WdfpZikJM22AvNDUxtkANBs8VEwZjrW5g_00246GPRyN4W36;

		private Quaternion VpgYT6gF3NEk8XR7z_YR3Yo;

		private float VhuqWGxPW_0024p_002416QzPjQGMgZkEu5r4yP5Bx8QHFRIOSaA;

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
		public Q8DI9Mhi0jQAuK5ny1lJkZlcqZU_jeULJyu84Zz9jWjJjHiimw9ig6Zvr3jgms8_kYsaXFtByngobg1ZGDCb3sFVFfvI_0024iM_00241dPAgQsL5PffDYYCOZU8PNM6aPoqN6Up27oTMQIOJ3U_0024JwMhFbyDf8g(int _003C_003E1__state)
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
			Color gparam_2;
			switch (SjlBM8inVA_YE4YVlr_0024gluY)
			{
			default:
				return false;
			case 0:
				SjlBM8inVA_YE4YVlr_0024gluY = -1;
				mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_29<string>(1731759771u));
				if (smethod_0((UnityEngine.Object)vVn7afGBU8BLmNjGePpOYo89VaBf_0024RaeOHzWZmSqv8cA, (UnityEngine.Object)null))
				{
					smethod_1((UnityEngine.Object)vVn7afGBU8BLmNjGePpOYo89VaBf_0024RaeOHzWZmSqv8cA);
					mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_27<string>(1104877756u));
				}
				vVn7afGBU8BLmNjGePpOYo89VaBf_0024RaeOHzWZmSqv8cA = null;
				JKGKJLLFMLE.EGFHGHKLNAO = JKGKJLLFMLE.LENPCAMMAEP.Practice;
				JKGKJLLFMLE.JMOEMCPIEJL = JKGKJLLFMLE.IGOBPLOLHEP.constructWorldType;
				JKGKJLLFMLE.NMGPDCIMFPN = JKGKJLLFMLE.BJIMLKIAEHD.Free;
				JKGKJLLFMLE.EPJKDGGFDIF = 0;
				JKGKJLLFMLE.FOCFAHGFEOB = 16;
				JKGKJLLFMLE.CDEIANEIODO = 1f;
				JKGKJLLFMLE.HOLDKCHPGJL = 1f;
				JKGKJLLFMLE.MHJBJGEFECP = 9.99f;
				JKGKJLLFMLE.AKEMCMINMBC = 0.5f;
				JKGKJLLFMLE.KHLHLNLENNB = 999999;
				wt0lWN0P7W6H0Viq8pkjxxQ = false;
				mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_28<string>(2484373667u));
				jykyL9tTi_002432Dr2zwoVB6RdRcudGFA7lwjFS1IxFdul6 = MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.OBJPlan;
				FZNwATyeqQEjPQAwrxpYbCOsXta__u3KwiGirfzsTe60 = MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.tracing;
				if (jykyL9tTi_002432Dr2zwoVB6RdRcudGFA7lwjFS1IxFdul6)
				{
					MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.OBJPlan = false;
				}
				if (FZNwATyeqQEjPQAwrxpYbCOsXta__u3KwiGirfzsTe60)
				{
					MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.tracing = false;
				}
				JKGKJLLFMLE.IGOBPLOLHEP.enemyMachineName = global::_003CModule_003E.smethod_29<string>(499090203u);
				ATCyo_0024qpPFqNSISN7PgDzuPM7bZZWP2fcNoTkefIy5A2WLvcWT1w9UCw3Yq7lMwlug.YMAh_TshgArfNEGhpCaDjcg(global::_003CModule_003E.smethod_26<string>(2833027181u), bool_0: true);
				goto IL_0152;
			case 1:
				SjlBM8inVA_YE4YVlr_0024gluY = -1;
				goto IL_0152;
			case 2:
				SjlBM8inVA_YE4YVlr_0024gluY = -1;
				goto IL_019e;
			case 3:
			{
				SjlBM8inVA_YE4YVlr_0024gluY = -1;
				mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_27<string>(811717595u));
				Arena.OEDCBNHNGMJ.LockSelf(BHCKMFDEBBH: true);
				vVn7afGBU8BLmNjGePpOYo89VaBf_0024RaeOHzWZmSqv8cA = (SceneMan.JFAOKFIDAGK as Arena).FICMBCLEFDL.transform.parent.gameObject;
				mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_27<string>(2016912803u));
				WdfpZikJM22AvNDUxtkANBs8VEwZjrW5g_00246GPRyN4W36 = (SceneMan.JFAOKFIDAGK as Arena).FICMBCLEFDL.transform.position - Camera.main.transform.position;
				VpgYT6gF3NEk8XR7z_YR3Yo = Camera.main.transform.rotation;
				AutoPilot[] componentsInChildren = vVn7afGBU8BLmNjGePpOYo89VaBf_0024RaeOHzWZmSqv8cA.GetComponentsInChildren<AutoPilot>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					componentsInChildren[i].enabled = false;
				}
				if (vVn7afGBU8BLmNjGePpOYo89VaBf_0024RaeOHzWZmSqv8cA.GetComponent<AutoPilot>() != null)
				{
					vVn7afGBU8BLmNjGePpOYo89VaBf_0024RaeOHzWZmSqv8cA.GetComponent<AutoPilot>().enabled = false;
				}
				UnityEngine.Object.DontDestroyOnLoad(vVn7afGBU8BLmNjGePpOYo89VaBf_0024RaeOHzWZmSqv8cA);
				VhuqWGxPW_0024p_002416QzPjQGMgZkEu5r4yP5Bx8QHFRIOSaA = 1.5f;
				MPatchr.smethod_0(global::_003CModule_003E.smethod_28<string>(2757553370u), delegate
				{
					float num2 = B10q8uby4y8GaSf_Y_0024u7RDZieDjbqDWT9_0024ArVHxvPlqo1Q0FiReTEriv8DVdnsyA3OhBFwoMR39TKuSP6Yw0DUem5cFtiv_0024Jnfz8K26I5nVu.smethod_4();
					SceneMan.JFAOKFIDAGK.mMOtaBaCAdAJENkfiJ_1fdbLwBEsBnP8lprp9wMogCpK(global::_003CModule_003E.smethod_27<string>(392941520u), (float)(B10q8uby4y8GaSf_Y_0024u7RDZieDjbqDWT9_0024ArVHxvPlqo1Q0FiReTEriv8DVdnsyA3OhBFwoMR39TKuSP6Yw0DUem5cFtiv_0024Jnfz8K26I5nVu.smethod_5() / 2 - 100), (float)(B10q8uby4y8GaSf_Y_0024u7RDZieDjbqDWT9_0024ArVHxvPlqo1Q0FiReTEriv8DVdnsyA3OhBFwoMR39TKuSP6Yw0DUem5cFtiv_0024Jnfz8K26I5nVu.smethod_6() / 2 - 16), global::_003CModule_003E.smethod_28<string>(3455580473u), 1f, 32, Mathf.Sin(num2), Mathf.Sin(num2 + (float)Math.PI * 2f / 3f), Mathf.Sin(num2 + 4.1887903f));
				});
				goto IL_037e;
			}
			case 4:
			{
				SjlBM8inVA_YE4YVlr_0024gluY = -1;
				VhuqWGxPW_0024p_002416QzPjQGMgZkEu5r4yP5Bx8QHFRIOSaA -= Time.deltaTime;
				Color gparam_ = SceneMan.JFAOKFIDAGK.i0duGIJ6M7BXC7c2cbeyvXM1hCrcoNKNJzlqDrhwjoxM<Color>(global::_003CModule_003E.smethod_29<string>(282008218u));
				gparam_.a = 1f;
				SceneMan.JFAOKFIDAGK.D7dqMa4OGuhdUnXHjJZM6vrBYDmyw_0024HWYteNzGVsTh5k(global::_003CModule_003E.smethod_29<string>(282008218u), gparam_);
				goto IL_037e;
			}
			case 5:
				SjlBM8inVA_YE4YVlr_0024gluY = -1;
				mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_28<string>(1012263302u));
				ATCyo_0024qpPFqNSISN7PgDzuPM7bZZWP2fcNoTkefIy5A2WLvcWT1w9UCw3Yq7lMwlug.YMAh_TshgArfNEGhpCaDjcg(global::_003CModule_003E.smethod_29<string>(2573180164u), bool_0: true);
				break;
			case 6:
				{
					SjlBM8inVA_YE4YVlr_0024gluY = -1;
					break;
				}
				IL_0152:
				if (!(smethod_3().name != global::_003CModule_003E.smethod_25<string>(3051704328u)))
				{
					mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_26<string>(2029714677u));
					goto IL_019e;
				}
				yT7HpVIzmqW54W307WgJtr4 = smethod_2();
				SjlBM8inVA_YE4YVlr_0024gluY = 1;
				return true;
				IL_019e:
				if (wt0lWN0P7W6H0Viq8pkjxxQ)
				{
					yT7HpVIzmqW54W307WgJtr4 = new WaitForEndOfFrame();
					SjlBM8inVA_YE4YVlr_0024gluY = 3;
					return true;
				}
				gparam_2 = SceneMan.JFAOKFIDAGK.i0duGIJ6M7BXC7c2cbeyvXM1hCrcoNKNJzlqDrhwjoxM<Color>(global::_003CModule_003E.smethod_26<string>(865365110u));
				gparam_2.a = 1f;
				SceneMan.JFAOKFIDAGK.D7dqMa4OGuhdUnXHjJZM6vrBYDmyw_0024HWYteNzGVsTh5k(global::_003CModule_003E.smethod_28<string>(1118475152u), gparam_2);
				yT7HpVIzmqW54W307WgJtr4 = new WaitForEndOfFrame();
				SjlBM8inVA_YE4YVlr_0024gluY = 2;
				return true;
				IL_037e:
				if (!(VhuqWGxPW_0024p_002416QzPjQGMgZkEu5r4yP5Bx8QHFRIOSaA <= 0f))
				{
					yT7HpVIzmqW54W307WgJtr4 = new WaitForEndOfFrame();
					SjlBM8inVA_YE4YVlr_0024gluY = 4;
					return true;
				}
				MPatchr.Oo1ruUb5AAVwJnBX0QWSyZhuNBfTSYAlNOATacVohQ5n(global::_003CModule_003E.smethod_28<string>(2757553370u));
				yT7HpVIzmqW54W307WgJtr4 = new WaitForEndOfFrame();
				SjlBM8inVA_YE4YVlr_0024gluY = 5;
				return true;
			}
			if (SceneManager.GetActiveScene().name != global::_003CModule_003E.smethod_29<string>(2573180164u))
			{
				yT7HpVIzmqW54W307WgJtr4 = new WaitForEndOfFrame();
				SjlBM8inVA_YE4YVlr_0024gluY = 6;
				return true;
			}
			mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_26<string>(2308818581u));
			MachineController componentInChildren = vVn7afGBU8BLmNjGePpOYo89VaBf_0024RaeOHzWZmSqv8cA.GetComponentInChildren<MachineController>();
			componentInChildren.enabled = false;
			vVn7afGBU8BLmNjGePpOYo89VaBf_0024RaeOHzWZmSqv8cA.GetComponentInChildren<MachineAdjuster>().enabled = false;
			if (jykyL9tTi_002432Dr2zwoVB6RdRcudGFA7lwjFS1IxFdul6)
			{
				MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.OBJPlan = true;
			}
			if (FZNwATyeqQEjPQAwrxpYbCOsXta__u3KwiGirfzsTe60)
			{
				MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.tracing = true;
			}
			_0024_r0hqHHH7HaViWiREzGiV8 = false;
			vVn7afGBU8BLmNjGePpOYo89VaBf_0024RaeOHzWZmSqv8cA.AddComponent<AmMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F>().U_0024eIoX6e3N9Ag_us_EcGHBI(B10q8uby4y8GaSf_Y_0024u7RDZieDjbqDWT9_0024ArVHxvPlqo1Q0FiReTEriv8DVdnsyA3OhBFwoMR39TKuSP6Yw0DUem5cFtiv_0024Jnfz8K26I5nVu._003C_003E9.KcWkp6cdo8jVwe6U1_CBZQPpSPYdk9NIofyEIId6azbuVEacnl_JkrFOb8KIHzhgHw);
			mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_27<string>(2356603639u));
			Vector3 translation = _00248_os5izJ7wHEiF3KBq8NWnTQOew_VLoMlU_nbp_Z9wx - vVn7afGBU8BLmNjGePpOYo89VaBf_0024RaeOHzWZmSqv8cA.smethod_0(global::_003CModule_003E.smethod_28<string>(3607396377u)).transform.position;
			vVn7afGBU8BLmNjGePpOYo89VaBf_0024RaeOHzWZmSqv8cA.transform.Translate(translation);
			componentInChildren.SetKinematic(BHCKMFDEBBH: false, HBHDHABLLIA: false);
			componentInChildren.SetKinematic(BHCKMFDEBBH: true, HBHDHABLLIA: true);
			for (int num = componentInChildren.ILBAAENKMBL.Count - 1; num >= 0; num--)
			{
				componentInChildren.ILBAAENKMBL[num].ClearVelocity();
			}
			if (camera_0 != null)
			{
				camera_0.transform.position += WdfpZikJM22AvNDUxtkANBs8VEwZjrW5g_00246GPRyN4W36 / 2f;
				camera_0.transform.rotation = VpgYT6gF3NEk8XR7z_YR3Yo;
			}
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw smethod_4();
		}

		internal static bool smethod_0(UnityEngine.Object object_0, UnityEngine.Object object_1)
		{
			return object_0 != object_1;
		}

		internal static void smethod_1(UnityEngine.Object object_0)
		{
			UnityEngine.Object.Destroy(object_0);
		}

		internal static WaitForEndOfFrame smethod_2()
		{
			return new WaitForEndOfFrame();
		}

		internal static Scene smethod_3()
		{
			return SceneManager.GetActiveScene();
		}

		internal static NotSupportedException smethod_4()
		{
			return new NotSupportedException();
		}
	}

	internal static GameObject vVn7afGBU8BLmNjGePpOYo89VaBf_0024RaeOHzWZmSqv8cA;

	private static bool wt0lWN0P7W6H0Viq8pkjxxQ = false;

	private static bool bool_0 = false;

	private static bool _0024_r0hqHHH7HaViWiREzGiV8 = false;

	private static ulong SLn0VTFkPWFtkFzCvegsS6I = ulong.MaxValue;

	internal static RenderTexture zgvxwmgMm8S4Mm1AIIlLkqE = smethod_9(512, 512, 24);

	private static Vector3 _00248_os5izJ7wHEiF3KBq8NWnTQOew_VLoMlU_nbp_Z9wx = new Vector3(0f, 5000f, 0f);

	private static Camera camera_0;

	internal static void tYz179S_0024LNHwdFhVLfLeYwE()
	{
		smethod_0();
		SLn0VTFkPWFtkFzCvegsS6I = HOCGCCAIPFF.JNPMJANFGNH;
		_0024_r0hqHHH7HaViWiREzGiV8 = true;
	}

	private static void egSFr3vBzo8SOVsjZcvWlkA()
	{
		if (!smethod_1((Behaviour)SceneMan.JFAOKFIDAGK))
		{
			return;
		}
		GameObject gameObject = UnityEngine.Object.Instantiate(vcvWq48of1PpnetEh2AY81tkRkpDe4qOaCwTttO9vBY6.ts9_0024hOMpHope_fSUdeN3MnM(global::_003CModule_003E.smethod_25<string>(3671831375u)));
		smethod_4(smethod_2(gameObject), smethod_2(smethod_3(global::_003CModule_003E.smethod_27<string>(862174272u))));
		smethod_5(smethod_2(gameObject));
		smethod_6(gameObject, bool_1: true);
		smethod_7(smethod_2(gameObject), Vector3.zero);
		smethod_8(smethod_2(gameObject), Vector3.one);
		gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(1f, 1f);
		gameObject.smethod_0(global::_003CModule_003E.smethod_27<string>(1705174189u)).GetComponent<Text>().text = global::_003CModule_003E.smethod_26<string>(3948048888u);
		Text component = gameObject.smethod_0(global::_003CModule_003E.smethod_25<string>(4067482088u)).GetComponent<Text>();
		component.text = global::_003CModule_003E.smethod_27<string>(3589751069u);
		gameObject.AddComponent<AmMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F>().U_0024eIoX6e3N9Ag_us_EcGHBI(delegate(AmMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F me)
		{
			if (!me.AqTKGFxfR1r6eAzrvm4_0024bck(global::_003CModule_003E.smethod_29<string>(833532274u)))
			{
				me.clf0br4v0HxmaJWIcm_0024_0024GUg(global::_003CModule_003E.smethod_26<string>(698710974u), 1f);
			}
			if (v4H1aHdOhdQg4cvr6BvZhiITrQ2dCVPB1bgNxrId41GgrLRkxqlgucsnI5_mIn_0024RbSr0oOIA3ksX17gcMWYjtBIw7137znJVms0DHx059ZDTRLXtD64wkmtmu_0024YUVLB1Ng.smethod_0())
			{
				if (v4H1aHdOhdQg4cvr6BvZhiITrQ2dCVPB1bgNxrId41GgrLRkxqlgucsnI5_mIn_0024RbSr0oOIA3ksX17gcMWYjtBIw7137znJVms0DHx059ZDTRLXtD64wkmtmu_0024YUVLB1Ng.smethod_1(KeyCode.Escape))
				{
					_0024_r0hqHHH7HaViWiREzGiV8 = false;
					v4H1aHdOhdQg4cvr6BvZhiITrQ2dCVPB1bgNxrId41GgrLRkxqlgucsnI5_mIn_0024RbSr0oOIA3ksX17gcMWYjtBIw7137znJVms0DHx059ZDTRLXtD64wkmtmu_0024YUVLB1Ng.smethod_2((Behaviour)SceneMan.JFAOKFIDAGK, bool_0: true);
					v4H1aHdOhdQg4cvr6BvZhiITrQ2dCVPB1bgNxrId41GgrLRkxqlgucsnI5_mIn_0024RbSr0oOIA3ksX17gcMWYjtBIw7137znJVms0DHx059ZDTRLXtD64wkmtmu_0024YUVLB1Ng.smethod_4((UnityEngine.Object)v4H1aHdOhdQg4cvr6BvZhiITrQ2dCVPB1bgNxrId41GgrLRkxqlgucsnI5_mIn_0024RbSr0oOIA3ksX17gcMWYjtBIw7137znJVms0DHx059ZDTRLXtD64wkmtmu_0024YUVLB1Ng.smethod_3((Component)me));
				}
				float num = me.hpiqzm2jQTswCo32f7jvrQ4<float>(global::_003CModule_003E.smethod_25<string>(3845030265u)) - v4H1aHdOhdQg4cvr6BvZhiITrQ2dCVPB1bgNxrId41GgrLRkxqlgucsnI5_mIn_0024RbSr0oOIA3ksX17gcMWYjtBIw7137znJVms0DHx059ZDTRLXtD64wkmtmu_0024YUVLB1Ng.smethod_5();
				if (num < 0f)
				{
					v4H1aHdOhdQg4cvr6BvZhiITrQ2dCVPB1bgNxrId41GgrLRkxqlgucsnI5_mIn_0024RbSr0oOIA3ksX17gcMWYjtBIw7137znJVms0DHx059ZDTRLXtD64wkmtmu_0024YUVLB1Ng.smethod_6();
					SLn0VTFkPWFtkFzCvegsS6I = HOCGCCAIPFF.JNPMJANFGNH;
					_0024_r0hqHHH7HaViWiREzGiV8 = true;
					v4H1aHdOhdQg4cvr6BvZhiITrQ2dCVPB1bgNxrId41GgrLRkxqlgucsnI5_mIn_0024RbSr0oOIA3ksX17gcMWYjtBIw7137znJVms0DHx059ZDTRLXtD64wkmtmu_0024YUVLB1Ng.smethod_2((Behaviour)SceneMan.JFAOKFIDAGK, bool_0: true);
					v4H1aHdOhdQg4cvr6BvZhiITrQ2dCVPB1bgNxrId41GgrLRkxqlgucsnI5_mIn_0024RbSr0oOIA3ksX17gcMWYjtBIw7137znJVms0DHx059ZDTRLXtD64wkmtmu_0024YUVLB1Ng.smethod_4((UnityEngine.Object)v4H1aHdOhdQg4cvr6BvZhiITrQ2dCVPB1bgNxrId41GgrLRkxqlgucsnI5_mIn_0024RbSr0oOIA3ksX17gcMWYjtBIw7137znJVms0DHx059ZDTRLXtD64wkmtmu_0024YUVLB1Ng.smethod_3((Component)me));
				}
				me.clf0br4v0HxmaJWIcm_0024_0024GUg(global::_003CModule_003E.smethod_27<string>(2510171775u), num);
				component.text = global::_003CModule_003E.smethod_27<string>(1737728753u) + Mathf.Round(num * 100f) + global::_003CModule_003E.smethod_29<string>(3963672339u);
			}
			else
			{
				me.clf0br4v0HxmaJWIcm_0024_0024GUg(global::_003CModule_003E.smethod_27<string>(2510171775u), 1f);
				component.text = global::_003CModule_003E.smethod_25<string>(4199365659u);
			}
		});
		SceneMan.JFAOKFIDAGK.enabled = false;
	}

	internal static void KX8jtCPdJb997Zi2Dvgkpuc()
	{
		Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.P3tLroX6fcPfQha_0024JdbpYXg(global::_003CModule_003E.smethod_25<string>(1305864711u), new Vector3(0f, 135f), global::_003CModule_003E.smethod_29<string>(3721932680u), delegate
		{
			if (B10q8uby4y8GaSf_Y_0024u7RDZieDjbqDWT9_0024ArVHxvPlqo1Q0FiReTEriv8DVdnsyA3OhBFwoMR39TKuSP6Yw0DUem5cFtiv_0024Jnfz8K26I5nVu.smethod_1((Selectable)B10q8uby4y8GaSf_Y_0024u7RDZieDjbqDWT9_0024ArVHxvPlqo1Q0FiReTEriv8DVdnsyA3OhBFwoMR39TKuSP6Yw0DUem5cFtiv_0024Jnfz8K26I5nVu.smethod_0(SceneMan.JFAOKFIDAGK, global::_003CModule_003E.smethod_29<string>(227720855u)).GetComponent<Button>()))
			{
				B10q8uby4y8GaSf_Y_0024u7RDZieDjbqDWT9_0024ArVHxvPlqo1Q0FiReTEriv8DVdnsyA3OhBFwoMR39TKuSP6Yw0DUem5cFtiv_0024Jnfz8K26I5nVu.smethod_2((MonoBehaviour)MPatchr.xcBvxcM_0024ckBeZyvdSoAkJoM, yLd1c6Ma8pKtWU8tHQUuzO58jr1MMkDNlceIdNNTV40R());
			}
		}, GameObject.Find(global::_003CModule_003E.smethod_26<string>(3103631283u)).smethod_0(global::_003CModule_003E.smethod_27<string>(499978981u)).transform).UzVS61irgJn5Pnqwx0lThng(new Vector2(200f, 40f));
		if (vVn7afGBU8BLmNjGePpOYo89VaBf_0024RaeOHzWZmSqv8cA == null)
		{
			return;
		}
		GameObject gameObject = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.qSsYOsEQb7x452a9Y45dVEk(new Vector3(400f, 168f), new Vector2(300f, 300f), GameObject.Find(global::_003CModule_003E.smethod_28<string>(3214927773u)).smethod_0(global::_003CModule_003E.smethod_29<string>(919901600u)).transform);
		GameObject gameObject2 = new GameObject(global::_003CModule_003E.smethod_28<string>(511359743u));
		gameObject2.transform.parent = gameObject.transform;
		RawImage rawImage = gameObject2.AddComponent<RawImage>();
		gameObject2.transform.localPosition = new Vector2(0f, 15f);
		RectTransform component = gameObject2.GetComponent<RectTransform>();
		component.anchorMin = new Vector2(0f, 0f);
		component.anchorMax = new Vector2(1f, 1f);
		component.offsetMin = new Vector2(15f, 44f);
		component.offsetMax = new Vector2(-15f, -15f);
		component.localScale = Vector3.one;
		rawImage.texture = zgvxwmgMm8S4Mm1AIIlLkqE;
		lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw obj = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.P3tLroX6fcPfQha_0024JdbpYXg(global::_003CModule_003E.smethod_26<string>(2742035787u), new Vector3(0f, 25f), global::_003CModule_003E.smethod_27<string>(3822404444u), delegate
		{
			egSFr3vBzo8SOVsjZcvWlkA();
		}, gameObject.transform);
		obj.UzVS61irgJn5Pnqwx0lThng(new Vector2(200f, 30f));
		obj.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0f);
		obj.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0f);
		lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw obj2 = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.P3tLroX6fcPfQha_0024JdbpYXg(global::_003CModule_003E.smethod_26<string>(1738069939u), Vector3.zero, global::_003CModule_003E.smethod_28<string>(2241497862u), delegate
		{
			Nr0ejQEJWARWwtZZB_00249GfUBqyyljzMsUnfAnxDRTiq6FsEmoLfGoP6Rs9L33v2Km_6Mn6injxUPScWPxZpO_L_0024yzs16fI0BfKoO1J_ZnCbeF.smethod_0((UnityEngine.Object)vVn7afGBU8BLmNjGePpOYo89VaBf_0024RaeOHzWZmSqv8cA);
			Nr0ejQEJWARWwtZZB_00249GfUBqyyljzMsUnfAnxDRTiq6FsEmoLfGoP6Rs9L33v2Km_6Mn6injxUPScWPxZpO_L_0024yzs16fI0BfKoO1J_ZnCbeF.smethod_0((UnityEngine.Object)gameObject);
		}, gameObject.transform);
		obj2.UzVS61irgJn5Pnqwx0lThng(new Vector2(30f, 30f));
		obj2.GetComponent<RectTransform>().anchorMin = Vector2.one;
		obj2.GetComponent<RectTransform>().anchorMax = Vector2.one;
		GameObject vHTkjhuQ_n117aM_fukkeqw = UnityEngine.Object.Instantiate(Camera.main.gameObject);
		vHTkjhuQ_n117aM_fukkeqw.transform.position = _00248_os5izJ7wHEiF3KBq8NWnTQOew_VLoMlU_nbp_Z9wx;
		camera_0 = vHTkjhuQ_n117aM_fukkeqw.GetComponent<Camera>();
		camera_0.targetTexture = zgvxwmgMm8S4Mm1AIIlLkqE;
		FreeCameraController component2 = camera_0.GetComponent<FreeCameraController>();
		AmMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F amMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F = vHTkjhuQ_n117aM_fukkeqw.AddComponent<AmMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F>();
		amMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F.method_0(delegate
		{
			component2.EBOMCDGDKCP = camera_0;
			component2.D7dqMa4OGuhdUnXHjJZM6vrBYDmyw_0024HWYteNzGVsTh5k(global::_003CModule_003E.smethod_29<string>(3072936598u), vHTkjhuQ_n117aM_fukkeqw.smethod_0(global::_003CModule_003E.smethod_26<string>(2706642670u)));
			component2.FMGOKAGJMJH = false;
		});
		lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw obj3 = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.P3tLroX6fcPfQha_0024JdbpYXg(global::_003CModule_003E.smethod_26<string>(3264850478u), new Vector3(30f, 26f), global::_003CModule_003E.smethod_25<string>(3894283198u), delegate
		{
			if (!Nr0ejQEJWARWwtZZB_00249GfUBqyyljzMsUnfAnxDRTiq6FsEmoLfGoP6Rs9L33v2Km_6Mn6injxUPScWPxZpO_L_0024yzs16fI0BfKoO1J_ZnCbeF.smethod_1(SystemData.EHLMFKOOHLI.Modifier))
			{
				bool_0 = true;
			}
			else
			{
				Nr0ejQEJWARWwtZZB_00249GfUBqyyljzMsUnfAnxDRTiq6FsEmoLfGoP6Rs9L33v2Km_6Mn6injxUPScWPxZpO_L_0024yzs16fI0BfKoO1J_ZnCbeF.smethod_3(Nr0ejQEJWARWwtZZB_00249GfUBqyyljzMsUnfAnxDRTiq6FsEmoLfGoP6Rs9L33v2Km_6Mn6injxUPScWPxZpO_L_0024yzs16fI0BfKoO1J_ZnCbeF.smethod_2(vHTkjhuQ_n117aM_fukkeqw), _00248_os5izJ7wHEiF3KBq8NWnTQOew_VLoMlU_nbp_Z9wx);
			}
		}, gameObject.transform);
		obj3.UzVS61irgJn5Pnqwx0lThng(new Vector2(30f, 30f));
		obj3.GetComponent<RectTransform>().anchorMin = Vector2.zero;
		obj3.GetComponent<RectTransform>().anchorMax = Vector2.zero;
		lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw obj4 = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.P3tLroX6fcPfQha_0024JdbpYXg(global::_003CModule_003E.smethod_25<string>(2229049708u), new Vector3(-29f, 24f), global::_003CModule_003E.smethod_26<string>(1583956637u), delegate
		{
			B10q8uby4y8GaSf_Y_0024u7RDZieDjbqDWT9_0024ArVHxvPlqo1Q0FiReTEriv8DVdnsyA3OhBFwoMR39TKuSP6Yw0DUem5cFtiv_0024Jnfz8K26I5nVu.smethod_3((Behaviour)SceneMan.JFAOKFIDAGK, bool_0: true);
		}, gameObject.transform, -1, delegate
		{
			gameObject.GetComponent<AmMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F>().clf0br4v0HxmaJWIcm_0024_0024GUg(global::_003CModule_003E.smethod_26<string>(57176098u), Nr0ejQEJWARWwtZZB_00249GfUBqyyljzMsUnfAnxDRTiq6FsEmoLfGoP6Rs9L33v2Km_6Mn6injxUPScWPxZpO_L_0024yzs16fI0BfKoO1J_ZnCbeF.smethod_4(gameObject.GetComponent<RectTransform>()));
			gameObject.GetComponent<AmMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F>().clf0br4v0HxmaJWIcm_0024_0024GUg(global::_003CModule_003E.smethod_27<string>(1304976567u), Nr0ejQEJWARWwtZZB_00249GfUBqyyljzMsUnfAnxDRTiq6FsEmoLfGoP6Rs9L33v2Km_6Mn6injxUPScWPxZpO_L_0024yzs16fI0BfKoO1J_ZnCbeF.smethod_8(Nr0ejQEJWARWwtZZB_00249GfUBqyyljzMsUnfAnxDRTiq6FsEmoLfGoP6Rs9L33v2Km_6Mn6injxUPScWPxZpO_L_0024yzs16fI0BfKoO1J_ZnCbeF.smethod_6(Nr0ejQEJWARWwtZZB_00249GfUBqyyljzMsUnfAnxDRTiq6FsEmoLfGoP6Rs9L33v2Km_6Mn6injxUPScWPxZpO_L_0024yzs16fI0BfKoO1J_ZnCbeF.smethod_5(global::_003CModule_003E.smethod_25<string>(806627754u)).GetComponent<Canvas>()), Nr0ejQEJWARWwtZZB_00249GfUBqyyljzMsUnfAnxDRTiq6FsEmoLfGoP6Rs9L33v2Km_6Mn6injxUPScWPxZpO_L_0024yzs16fI0BfKoO1J_ZnCbeF.smethod_7()));
			gameObject.GetComponent<AmMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F>().clf0br4v0HxmaJWIcm_0024_0024GUg(global::_003CModule_003E.smethod_26<string>(1744340356u), gparam_0: true);
		});
		obj4.UzVS61irgJn5Pnqwx0lThng(new Vector2(30f, 30f));
		obj4.GetComponent<RectTransform>().anchorMin = new Vector2(1f, 0f);
		obj4.GetComponent<RectTransform>().anchorMax = new Vector2(1f, 0f);
		RectTransform POrzo6yL6vzrGXopoAufEQQ = rawImage.GetComponent<RectTransform>();
		amMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F.U_0024eIoX6e3N9Ag_us_EcGHBI(delegate
		{
			if (Nr0ejQEJWARWwtZZB_00249GfUBqyyljzMsUnfAnxDRTiq6FsEmoLfGoP6Rs9L33v2Km_6Mn6injxUPScWPxZpO_L_0024yzs16fI0BfKoO1J_ZnCbeF.smethod_9(1))
			{
				Vector2 vector2_ = default(Vector2);
				Nr0ejQEJWARWwtZZB_00249GfUBqyyljzMsUnfAnxDRTiq6FsEmoLfGoP6Rs9L33v2Km_6Mn6injxUPScWPxZpO_L_0024yzs16fI0BfKoO1J_ZnCbeF.smethod_10(POrzo6yL6vzrGXopoAufEQQ, (Vector2)Nr0ejQEJWARWwtZZB_00249GfUBqyyljzMsUnfAnxDRTiq6FsEmoLfGoP6Rs9L33v2Km_6Mn6injxUPScWPxZpO_L_0024yzs16fI0BfKoO1J_ZnCbeF.smethod_7(), Nr0ejQEJWARWwtZZB_00249GfUBqyyljzMsUnfAnxDRTiq6FsEmoLfGoP6Rs9L33v2Km_6Mn6injxUPScWPxZpO_L_0024yzs16fI0BfKoO1J_ZnCbeF.smethod_6(Nr0ejQEJWARWwtZZB_00249GfUBqyyljzMsUnfAnxDRTiq6FsEmoLfGoP6Rs9L33v2Km_6Mn6injxUPScWPxZpO_L_0024yzs16fI0BfKoO1J_ZnCbeF.smethod_5(global::_003CModule_003E.smethod_29<string>(618767629u)).GetComponent<Canvas>()), ref vector2_);
				if (Nr0ejQEJWARWwtZZB_00249GfUBqyyljzMsUnfAnxDRTiq6FsEmoLfGoP6Rs9L33v2Km_6Mn6injxUPScWPxZpO_L_0024yzs16fI0BfKoO1J_ZnCbeF.smethod_11(POrzo6yL6vzrGXopoAufEQQ).Contains(vector2_))
				{
					component2.FMGOKAGJMJH = true;
					SceneMan.JFAOKFIDAGK.enabled = false;
				}
			}
			else if (Input.GetMouseButtonUp(1))
			{
				component2.FMGOKAGJMJH = false;
				SceneMan.JFAOKFIDAGK.enabled = true;
			}
		});
		AmMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F amMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F2 = gameObject.AddComponent<AmMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F>();
		amMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F2.clf0br4v0HxmaJWIcm_0024_0024GUg(global::_003CModule_003E.smethod_29<string>(1818129097u), gparam_0: false);
		amMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F2.clf0br4v0HxmaJWIcm_0024_0024GUg(global::_003CModule_003E.smethod_28<string>(268483938u), gparam_0: false);
		amMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F2.U_0024eIoX6e3N9Ag_us_EcGHBI(delegate(AmMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F me)
		{
			if (Nr0ejQEJWARWwtZZB_00249GfUBqyyljzMsUnfAnxDRTiq6FsEmoLfGoP6Rs9L33v2Km_6Mn6injxUPScWPxZpO_L_0024yzs16fI0BfKoO1J_ZnCbeF.smethod_9(0))
			{
				Vector2 vector2_ = default(Vector2);
				Nr0ejQEJWARWwtZZB_00249GfUBqyyljzMsUnfAnxDRTiq6FsEmoLfGoP6Rs9L33v2Km_6Mn6injxUPScWPxZpO_L_0024yzs16fI0BfKoO1J_ZnCbeF.smethod_10(POrzo6yL6vzrGXopoAufEQQ, (Vector2)Nr0ejQEJWARWwtZZB_00249GfUBqyyljzMsUnfAnxDRTiq6FsEmoLfGoP6Rs9L33v2Km_6Mn6injxUPScWPxZpO_L_0024yzs16fI0BfKoO1J_ZnCbeF.smethod_7(), Nr0ejQEJWARWwtZZB_00249GfUBqyyljzMsUnfAnxDRTiq6FsEmoLfGoP6Rs9L33v2Km_6Mn6injxUPScWPxZpO_L_0024yzs16fI0BfKoO1J_ZnCbeF.smethod_6(Nr0ejQEJWARWwtZZB_00249GfUBqyyljzMsUnfAnxDRTiq6FsEmoLfGoP6Rs9L33v2Km_6Mn6injxUPScWPxZpO_L_0024yzs16fI0BfKoO1J_ZnCbeF.smethod_5(global::_003CModule_003E.smethod_27<string>(862174272u)).GetComponent<Canvas>()), ref vector2_);
				if (!Nr0ejQEJWARWwtZZB_00249GfUBqyyljzMsUnfAnxDRTiq6FsEmoLfGoP6Rs9L33v2Km_6Mn6injxUPScWPxZpO_L_0024yzs16fI0BfKoO1J_ZnCbeF.smethod_11(POrzo6yL6vzrGXopoAufEQQ).Contains(vector2_))
				{
					return;
				}
				me.clf0br4v0HxmaJWIcm_0024_0024GUg(global::_003CModule_003E.smethod_26<string>(1702676822u), GameObject.Find(global::_003CModule_003E.smethod_28<string>(3214927773u)).GetComponent<Canvas>().worldCamera.ScreenToWorldPoint(Input.mousePosition));
				me.clf0br4v0HxmaJWIcm_0024_0024GUg(global::_003CModule_003E.smethod_27<string>(3422206822u), me.GetComponent<RectTransform>().position);
				me.clf0br4v0HxmaJWIcm_0024_0024GUg(global::_003CModule_003E.smethod_25<string>(514563285u), me.GetComponent<RectTransform>().sizeDelta);
				amMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F2.clf0br4v0HxmaJWIcm_0024_0024GUg(global::_003CModule_003E.smethod_27<string>(3143022772u), gparam_0: true);
			}
			else if (Input.GetMouseButtonUp(0))
			{
				amMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F2.clf0br4v0HxmaJWIcm_0024_0024GUg(global::_003CModule_003E.smethod_28<string>(541663641u), gparam_0: false);
				amMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F2.clf0br4v0HxmaJWIcm_0024_0024GUg(global::_003CModule_003E.smethod_25<string>(2089228523u), gparam_0: false);
			}
			if (!amMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F2.hpiqzm2jQTswCo32f7jvrQ4<bool>(global::_003CModule_003E.smethod_29<string>(1818129097u)))
			{
				if (amMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F2.hpiqzm2jQTswCo32f7jvrQ4<bool>(global::_003CModule_003E.smethod_28<string>(268483938u)))
				{
					Vector3 vector = GameObject.Find(global::_003CModule_003E.smethod_29<string>(618767629u)).GetComponent<Canvas>().worldCamera.ScreenToWorldPoint(Input.mousePosition) - me.hpiqzm2jQTswCo32f7jvrQ4<Vector3>(global::_003CModule_003E.smethod_26<string>(1702676822u));
					float num = Mathf.Max(vector.x, 0f - vector.y);
					me.GetComponent<RectTransform>().sizeDelta = me.hpiqzm2jQTswCo32f7jvrQ4<Vector2>(global::_003CModule_003E.smethod_25<string>(514563285u)) + new Vector2(num, num);
					Vector2 sizeDelta = me.GetComponent<RectTransform>().sizeDelta;
					if (sizeDelta.x < 300f || sizeDelta.y < 300f)
					{
						me.GetComponent<RectTransform>().sizeDelta = new Vector2(300f, 300f);
					}
				}
			}
			else
			{
				Vector3 vector2 = GameObject.Find(global::_003CModule_003E.smethod_25<string>(806627754u)).GetComponent<Canvas>().worldCamera.ScreenToWorldPoint(Input.mousePosition) - me.hpiqzm2jQTswCo32f7jvrQ4<Vector3>(global::_003CModule_003E.smethod_25<string>(1479063601u));
				me.GetComponent<RectTransform>().position = me.hpiqzm2jQTswCo32f7jvrQ4<Vector3>(global::_003CModule_003E.smethod_26<string>(2671249553u)) + new Vector3(vector2.x, vector2.y);
			}
		});
	}

	internal static IEnumerator yLd1c6Ma8pKtWU8tHQUuzO58jr1MMkDNlceIdNNTV40R()
	{
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_29<string>(1731759771u));
		if (Q8DI9Mhi0jQAuK5ny1lJkZlcqZU_jeULJyu84Zz9jWjJjHiimw9ig6Zvr3jgms8_kYsaXFtByngobg1ZGDCb3sFVFfvI_0024iM_00241dPAgQsL5PffDYYCOZU8PNM6aPoqN6Up27oTMQIOJ3U_0024JwMhFbyDf8g.smethod_0((UnityEngine.Object)vVn7afGBU8BLmNjGePpOYo89VaBf_0024RaeOHzWZmSqv8cA, (UnityEngine.Object)null))
		{
			Q8DI9Mhi0jQAuK5ny1lJkZlcqZU_jeULJyu84Zz9jWjJjHiimw9ig6Zvr3jgms8_kYsaXFtByngobg1ZGDCb3sFVFfvI_0024iM_00241dPAgQsL5PffDYYCOZU8PNM6aPoqN6Up27oTMQIOJ3U_0024JwMhFbyDf8g.smethod_1((UnityEngine.Object)vVn7afGBU8BLmNjGePpOYo89VaBf_0024RaeOHzWZmSqv8cA);
			mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_27<string>(1104877756u));
		}
		vVn7afGBU8BLmNjGePpOYo89VaBf_0024RaeOHzWZmSqv8cA = null;
		JKGKJLLFMLE.EGFHGHKLNAO = JKGKJLLFMLE.LENPCAMMAEP.Practice;
		JKGKJLLFMLE.JMOEMCPIEJL = JKGKJLLFMLE.IGOBPLOLHEP.constructWorldType;
		JKGKJLLFMLE.NMGPDCIMFPN = JKGKJLLFMLE.BJIMLKIAEHD.Free;
		JKGKJLLFMLE.EPJKDGGFDIF = 0;
		JKGKJLLFMLE.FOCFAHGFEOB = 16;
		JKGKJLLFMLE.CDEIANEIODO = 1f;
		JKGKJLLFMLE.HOLDKCHPGJL = 1f;
		JKGKJLLFMLE.MHJBJGEFECP = 9.99f;
		JKGKJLLFMLE.AKEMCMINMBC = 0.5f;
		JKGKJLLFMLE.KHLHLNLENNB = 999999;
		wt0lWN0P7W6H0Viq8pkjxxQ = false;
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_28<string>(2484373667u));
		bool oBJPlan = MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.OBJPlan;
		bool tracing = MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.tracing;
		if (oBJPlan)
		{
			MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.OBJPlan = false;
		}
		if (tracing)
		{
			MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.tracing = false;
		}
		JKGKJLLFMLE.IGOBPLOLHEP.enemyMachineName = global::_003CModule_003E.smethod_29<string>(499090203u);
		ATCyo_0024qpPFqNSISN7PgDzuPM7bZZWP2fcNoTkefIy5A2WLvcWT1w9UCw3Yq7lMwlug.YMAh_TshgArfNEGhpCaDjcg(global::_003CModule_003E.smethod_26<string>(2833027181u), bool_0: true);
		while (Q8DI9Mhi0jQAuK5ny1lJkZlcqZU_jeULJyu84Zz9jWjJjHiimw9ig6Zvr3jgms8_kYsaXFtByngobg1ZGDCb3sFVFfvI_0024iM_00241dPAgQsL5PffDYYCOZU8PNM6aPoqN6Up27oTMQIOJ3U_0024JwMhFbyDf8g.smethod_3().name != global::_003CModule_003E.smethod_25<string>(3051704328u))
		{
			yield return Q8DI9Mhi0jQAuK5ny1lJkZlcqZU_jeULJyu84Zz9jWjJjHiimw9ig6Zvr3jgms8_kYsaXFtByngobg1ZGDCb3sFVFfvI_0024iM_00241dPAgQsL5PffDYYCOZU8PNM6aPoqN6Up27oTMQIOJ3U_0024JwMhFbyDf8g.smethod_2();
		}
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_26<string>(2029714677u));
		while (!wt0lWN0P7W6H0Viq8pkjxxQ)
		{
			Color gparam_ = SceneMan.JFAOKFIDAGK.i0duGIJ6M7BXC7c2cbeyvXM1hCrcoNKNJzlqDrhwjoxM<Color>(global::_003CModule_003E.smethod_26<string>(865365110u));
			gparam_.a = 1f;
			SceneMan.JFAOKFIDAGK.D7dqMa4OGuhdUnXHjJZM6vrBYDmyw_0024HWYteNzGVsTh5k(global::_003CModule_003E.smethod_28<string>(1118475152u), gparam_);
			yield return new WaitForEndOfFrame();
		}
		yield return new WaitForEndOfFrame();
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_27<string>(811717595u));
		Arena.OEDCBNHNGMJ.LockSelf(BHCKMFDEBBH: true);
		vVn7afGBU8BLmNjGePpOYo89VaBf_0024RaeOHzWZmSqv8cA = (SceneMan.JFAOKFIDAGK as Arena).FICMBCLEFDL.transform.parent.gameObject;
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_27<string>(2016912803u));
		Vector3 vector = (SceneMan.JFAOKFIDAGK as Arena).FICMBCLEFDL.transform.position - Camera.main.transform.position;
		Quaternion rotation = Camera.main.transform.rotation;
		AutoPilot[] componentsInChildren = vVn7afGBU8BLmNjGePpOYo89VaBf_0024RaeOHzWZmSqv8cA.GetComponentsInChildren<AutoPilot>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].enabled = false;
		}
		if (vVn7afGBU8BLmNjGePpOYo89VaBf_0024RaeOHzWZmSqv8cA.GetComponent<AutoPilot>() != null)
		{
			vVn7afGBU8BLmNjGePpOYo89VaBf_0024RaeOHzWZmSqv8cA.GetComponent<AutoPilot>().enabled = false;
		}
		UnityEngine.Object.DontDestroyOnLoad(vVn7afGBU8BLmNjGePpOYo89VaBf_0024RaeOHzWZmSqv8cA);
		float num = 1.5f;
		MPatchr.smethod_0(global::_003CModule_003E.smethod_28<string>(2757553370u), delegate
		{
			float num3 = B10q8uby4y8GaSf_Y_0024u7RDZieDjbqDWT9_0024ArVHxvPlqo1Q0FiReTEriv8DVdnsyA3OhBFwoMR39TKuSP6Yw0DUem5cFtiv_0024Jnfz8K26I5nVu.smethod_4();
			SceneMan.JFAOKFIDAGK.mMOtaBaCAdAJENkfiJ_1fdbLwBEsBnP8lprp9wMogCpK(global::_003CModule_003E.smethod_27<string>(392941520u), (float)(B10q8uby4y8GaSf_Y_0024u7RDZieDjbqDWT9_0024ArVHxvPlqo1Q0FiReTEriv8DVdnsyA3OhBFwoMR39TKuSP6Yw0DUem5cFtiv_0024Jnfz8K26I5nVu.smethod_5() / 2 - 100), (float)(B10q8uby4y8GaSf_Y_0024u7RDZieDjbqDWT9_0024ArVHxvPlqo1Q0FiReTEriv8DVdnsyA3OhBFwoMR39TKuSP6Yw0DUem5cFtiv_0024Jnfz8K26I5nVu.smethod_6() / 2 - 16), global::_003CModule_003E.smethod_28<string>(3455580473u), 1f, 32, Mathf.Sin(num3), Mathf.Sin(num3 + (float)Math.PI * 2f / 3f), Mathf.Sin(num3 + 4.1887903f));
		});
		while (!(num <= 0f))
		{
			yield return new WaitForEndOfFrame();
			num -= Time.deltaTime;
			Color gparam_2 = SceneMan.JFAOKFIDAGK.i0duGIJ6M7BXC7c2cbeyvXM1hCrcoNKNJzlqDrhwjoxM<Color>(global::_003CModule_003E.smethod_29<string>(282008218u));
			gparam_2.a = 1f;
			SceneMan.JFAOKFIDAGK.D7dqMa4OGuhdUnXHjJZM6vrBYDmyw_0024HWYteNzGVsTh5k(global::_003CModule_003E.smethod_29<string>(282008218u), gparam_2);
		}
		MPatchr.Oo1ruUb5AAVwJnBX0QWSyZhuNBfTSYAlNOATacVohQ5n(global::_003CModule_003E.smethod_28<string>(2757553370u));
		yield return new WaitForEndOfFrame();
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_28<string>(1012263302u));
		ATCyo_0024qpPFqNSISN7PgDzuPM7bZZWP2fcNoTkefIy5A2WLvcWT1w9UCw3Yq7lMwlug.YMAh_TshgArfNEGhpCaDjcg(global::_003CModule_003E.smethod_29<string>(2573180164u), bool_0: true);
		while (SceneManager.GetActiveScene().name != global::_003CModule_003E.smethod_29<string>(2573180164u))
		{
			yield return new WaitForEndOfFrame();
		}
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_26<string>(2308818581u));
		MachineController componentInChildren = vVn7afGBU8BLmNjGePpOYo89VaBf_0024RaeOHzWZmSqv8cA.GetComponentInChildren<MachineController>();
		componentInChildren.enabled = false;
		vVn7afGBU8BLmNjGePpOYo89VaBf_0024RaeOHzWZmSqv8cA.GetComponentInChildren<MachineAdjuster>().enabled = false;
		if (oBJPlan)
		{
			MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.OBJPlan = true;
		}
		if (tracing)
		{
			MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.tracing = true;
		}
		_0024_r0hqHHH7HaViWiREzGiV8 = false;
		vVn7afGBU8BLmNjGePpOYo89VaBf_0024RaeOHzWZmSqv8cA.AddComponent<AmMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F>().U_0024eIoX6e3N9Ag_us_EcGHBI(delegate
		{
			HOCGCCAIPFF.IICBFEKPLJL = false;
			if (bool_0)
			{
				bool_0 = false;
				HOCGCCAIPFF.JNPMJANFGNH = 0uL;
			}
			else if (_0024_r0hqHHH7HaViWiREzGiV8)
			{
				HOCGCCAIPFF.JNPMJANFGNH = SLn0VTFkPWFtkFzCvegsS6I;
			}
		});
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_27<string>(2356603639u));
		Vector3 translation = _00248_os5izJ7wHEiF3KBq8NWnTQOew_VLoMlU_nbp_Z9wx - vVn7afGBU8BLmNjGePpOYo89VaBf_0024RaeOHzWZmSqv8cA.smethod_0(global::_003CModule_003E.smethod_28<string>(3607396377u)).transform.position;
		vVn7afGBU8BLmNjGePpOYo89VaBf_0024RaeOHzWZmSqv8cA.transform.Translate(translation);
		componentInChildren.SetKinematic(BHCKMFDEBBH: false, HBHDHABLLIA: false);
		componentInChildren.SetKinematic(BHCKMFDEBBH: true, HBHDHABLLIA: true);
		for (int num2 = componentInChildren.ILBAAENKMBL.Count - 1; num2 >= 0; num2--)
		{
			componentInChildren.ILBAAENKMBL[num2].ClearVelocity();
		}
		if (camera_0 != null)
		{
			camera_0.transform.position += vector / 2f;
			camera_0.transform.rotation = rotation;
		}
	}

	internal static void smethod_0()
	{
		HOCGCCAIPFF.HAMLNBEBDOB();
	}

	internal static bool smethod_1(Behaviour behaviour_0)
	{
		return behaviour_0.enabled;
	}

	internal static Transform smethod_2(GameObject gameObject_0)
	{
		return gameObject_0.transform;
	}

	internal static GameObject smethod_3(string string_0)
	{
		return GameObject.Find(string_0);
	}

	internal static void smethod_4(Transform transform_0, Transform transform_1)
	{
		transform_0.SetParent(transform_1);
	}

	internal static void smethod_5(Transform transform_0)
	{
		transform_0.SetAsLastSibling();
	}

	internal static void smethod_6(GameObject gameObject_0, bool bool_1)
	{
		gameObject_0.SetActive(bool_1);
	}

	internal static void smethod_7(Transform transform_0, Vector3 vector3_0)
	{
		transform_0.localPosition = vector3_0;
	}

	internal static void smethod_8(Transform transform_0, Vector3 vector3_0)
	{
		transform_0.localScale = vector3_0;
	}

	internal static RenderTexture smethod_9(int int_0, int int_1, int int_2)
	{
		return new RenderTexture(int_0, int_1, int_2);
	}
}
