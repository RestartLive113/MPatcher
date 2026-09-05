using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using HarmonyLib;
using MPatchrMain;
using McnCraft;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

internal class Q8GK9vSf_0024VEM692w50G2d5SmUcj4PtKNzDGwgaTN75YKao1YzfuGvFueAht8oPEnuw
{
	private enum vpxRY7V6xxVwrtWzUazGbAijYYgy_j9aOURndFg_MPmV1XpOIQGYmugcr08FohKkWZtGTI6r2M_fFifxmanSRrgmSgq6IJSK9HbxY_0024RtywzhAZtQnNleJNvuto3IE49fVw
	{
		move,
		scale,
		rotate,
		paint
	}

	[HarmonyPatch(typeof(Construct))]
	[HarmonyPatch("OBBFPAMAMDE")]
	internal static class Class42
	{
		[HarmonyPrefix]
		internal static bool smethod_0(Construct __instance)
		{
			if (Boolean_0)
			{
				AJ95PT5qH7SI3aQp44bnfe9rXdzyiYcVJnP8p242egZvaWA5f7vGynYUg9dQSkzYlA.YZ_0024PBkn7rausEmKONRxFfNZQD6SNoR50G7r60U9WBOJ4.LENDBHNDHHH = AJ95PT5qH7SI3aQp44bnfe9rXdzyiYcVJnP8p242egZvaWA5f7vGynYUg9dQSkzYlA.Sl3qb7_zclIQRMX7sWCu5EE;
				smethod_1(AJ95PT5qH7SI3aQp44bnfe9rXdzyiYcVJnP8p242egZvaWA5f7vGynYUg9dQSkzYlA.YZ_0024PBkn7rausEmKONRxFfNZQD6SNoR50G7r60U9WBOJ4, Vector3.up);
				AJ95PT5qH7SI3aQp44bnfe9rXdzyiYcVJnP8p242egZvaWA5f7vGynYUg9dQSkzYlA.YZ_0024PBkn7rausEmKONRxFfNZQD6SNoR50G7r60U9WBOJ4.KMBBCMEJCKE = true;
				return false;
			}
			return true;
		}

		internal static void smethod_1(RideCameraController rideCameraController_0, Vector3 vector3_0)
		{
			rideCameraController_0.SetUpDir(vector3_0);
		}
	}

	[HarmonyPatch(typeof(Construct))]
	[HarmonyPatch("Start")]
	internal static class k2WIaTyroTQRMvU79wRrXLqyfoUcMlf_SqqbTddesrILWNpgMk5KTj37bDhltkgSQINCosnE02O3824W1B1qS0efo8Kd7BTcVGwDSCuVrW7xzJqJfVdK9QcRY2bAb9gWyw
	{
		[HarmonyPostfix]
		internal static void FeUAVwFbW6wGJJdNimZY9yI()
		{
			Transform transform = smethod_1(smethod_0(global::_003CModule_003E.smethod_29<string>(618767629u)).smethod_0(global::_003CModule_003E.smethod_28<string>(1332232715u)));
			transform.position = smethod_2(transform) + new Vector3(999f, 999f);
			GameObject.Find(global::_003CModule_003E.smethod_26<string>(3103631283u)).smethod_0(global::_003CModule_003E.smethod_27<string>(1152084771u)).transform.position += new Vector3(999f, 999f);
			GameObject.Find(global::_003CModule_003E.smethod_27<string>(862174272u)).smethod_0(global::_003CModule_003E.smethod_26<string>(2250155443u)).transform.position += new Vector3(999f, 999f);
			GameObject.Find(global::_003CModule_003E.smethod_27<string>(862174272u)).smethod_0(global::_003CModule_003E.smethod_27<string>(2603909465u)).transform.position += new Vector3(999f, 999f);
			_0024CMex_0024vAX35hsObIY7ThtQI = new List<pQCv4GWEmO7suQD936qIBn22jIu63BZDtto8pweiUAZY>();
			XU_0024J1o9n9AOB0F3rIkTIPFo();
		}

		internal static GameObject smethod_0(string string_0)
		{
			return GameObject.Find(string_0);
		}

		internal static Transform smethod_1(GameObject gameObject_0)
		{
			return gameObject_0.transform;
		}

		internal static Vector3 smethod_2(Transform transform_0)
		{
			return transform_0.position;
		}
	}

	[HarmonyPatch("GJFNAAMKIKA")]
	[HarmonyPatch(typeof(Construct))]
	internal static class bxVbt4NSfHWpKne7t5VufJftUp_Hf7zbDHCXkxZ2fyBrGLCgUqOIvTNgQUrNiW3c0xKbKfoAGpp2LDNO0TyOm8nJVvnTM_e3GTu11ER9X51si1hgDB_00249y0iFPxKiV9XiHQ
	{
		[HarmonyPrefix]
		internal static bool smethod_0(bool ___JHDDDPFPANC, ConstructData JNKEKNOAPHO)
		{
			if (!___JHDDDPFPANC)
			{
				return true;
			}
			XU_0024J1o9n9AOB0F3rIkTIPFo();
			EmzpBqNhuUJvMBi03aDSG_w = JNKEKNOAPHO;
			return false;
		}
	}

	[HarmonyPatch("BDKIMPEDKCJ")]
	[HarmonyPatch(typeof(Construct))]
	internal class kpIgfIkCVRvmymFgFktallLmRWh9OUSe52g6A_afk_0024eAo_zNqRenTPmmNY6RGascdPlshLxrK9EhxwOtFixRP5_Dl_0024ugUGX7bcfREsaS06sQ6EapISkWSnQJOvUOfE_bTw
	{
		[HarmonyPrefix]
		internal static bool smethod_0(string DPGKEOAGONA, Construct __instance)
		{
			if (!smethod_1(DPGKEOAGONA, global::_003CModule_003E.smethod_27<string>(1059023421u)))
			{
				if (!smethod_1(DPGKEOAGONA, global::_003CModule_003E.smethod_29<string>(1819355234u)))
				{
					return true;
				}
				_0024CMex_0024vAX35hsObIY7ThtQI.Clear();
				return true;
			}
			oPscfRQFZWvUSR8BZxgbHJMQBRDQTKjYlqK7tljpOgdF();
			_X2PQ1Ly_CMNLsRnRNN_vybYUDyvl1x6gIuOELMr0x7x().M_UYuO7m_5zc0NRNG1OJLAE();
			return false;
		}

		internal static bool smethod_1(string string_0, string string_1)
		{
			return string_0 == string_1;
		}
	}

	[HarmonyPatch("Update")]
	[HarmonyPatch(typeof(Construct))]
	internal static class up6FXLYVyjEksJXtTa1iFHsb2iCdhPfIw_oEhmBFk00sM_0024tJG7SP0Jry0qdwDeo_0024o6cqCsygIsldUfqTzgjsAyvX0hDuy5QUXpXFAI9DWyC4YxWBX2hZKqoKpFJXIShpSQ
	{
		[Serializable]
		[CompilerGenerated]
		private sealed class CJMhv67dSW_yQIGoFtPlWcbgax_OQ5_0024Fp37aG7YfGb4sgG2anRt83J5mUK3_0024zPVqEeh_0024o4C68bhHfF2mrd4hR_rnT8aeby_0024sgZI9VBYschbMognZXTsqSk8yWwRsn2mY7A
		{
			public static readonly CJMhv67dSW_yQIGoFtPlWcbgax_OQ5_0024Fp37aG7YfGb4sgG2anRt83J5mUK3_0024zPVqEeh_0024o4C68bhHfF2mrd4hR_rnT8aeby_0024sgZI9VBYschbMognZXTsqSk8yWwRsn2mY7A _003C_003E9 = new CJMhv67dSW_yQIGoFtPlWcbgax_OQ5_0024Fp37aG7YfGb4sgG2anRt83J5mUK3_0024zPVqEeh_0024o4C68bhHfF2mrd4hR_rnT8aeby_0024sgZI9VBYschbMognZXTsqSk8yWwRsn2mY7A();

			public static Predicate<xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA> _003C_003E9__1_0;

			public static Predicate<c2wojWPyeJ90X8UxbVw8Ie7t2iSXT4W6AM5UctjLMkqq> _003C_003E9__1_1;

			public static Action<lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw> _003C_003E9__1_2;

			public static Action<lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw> _003C_003E9__1_3;

			public static Action<lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw> _003C_003E9__1_4;

			public static Action<lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw> _003C_003E9__1_5;

			public static Action<lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw> _003C_003E9__1_6;

			public static Action<lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw> _003C_003E9__1_7;

			public static Action<bool> _003C_003E9__1_8;

			public static Action<bool> _003C_003E9__1_9;

			public static Action<bool> _003C_003E9__1_10;

			public static Action<bool> _003C_003E9__1_11;

			public static Action<string> _003C_003E9__1_12;

			public static Action<string> _003C_003E9__1_13;

			public static Action<string> _003C_003E9__1_14;

			public static UnityAction<float> _003C_003E9__1_15;

			public static UnityAction<float> _003C_003E9__1_16;

			public static UnityAction<float> _003C_003E9__1_17;

			public static UnityAction<float> _003C_003E9__1_18;

			public static UnityAction<float> _003C_003E9__1_19;

			public static UnityAction<float> _003C_003E9__1_20;

			public static Action<bool> _003C_003E9__1_22;

			public static Action<bool> _003C_003E9__1_23;

			public static UnityAction<float> _003C_003E9__1_30;

			public static UnityAction<float> _003C_003E9__1_32;

			public static UnityAction<float> _003C_003E9__1_34;

			internal bool iFLCbIMZgAyvV76aAg1BiDk(xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA item)
			{
				return smethod_0((UnityEngine.Object)item.TIWjI8FsBk2nlZk9NO4HNOE, (UnityEngine.Object)null);
			}

			internal bool iYqGLBRYDArus6ClwJYwSYc(c2wojWPyeJ90X8UxbVw8Ie7t2iSXT4W6AM5UctjLMkqq item)
			{
				return smethod_0((UnityEngine.Object)item, (UnityEngine.Object)null);
			}

			internal void iqvCVjjxBbhln_Rq6jyJo3A(lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw button)
			{
				XBS7OSIVgwiqp2YsikrDtq6zcM87cw3nXOw4Pre7ULHrBi6l5_25VvKQC1MdNr_0024s2g xBS7OSIVgwiqp2YsikrDtq6zcM87cw3nXOw4Pre7ULHrBi6l5_25VvKQC1MdNr_0024s2g = new XBS7OSIVgwiqp2YsikrDtq6zcM87cw3nXOw4Pre7ULHrBi6l5_25VvKQC1MdNr_0024s2g(AJ95PT5qH7SI3aQp44bnfe9rXdzyiYcVJnP8p242egZvaWA5f7vGynYUg9dQSkzYlA.Sl3qb7_zclIQRMX7sWCu5EE, Quaternion.identity, Construct_0, XBS7OSIVgwiqp2YsikrDtq6zcM87cw3nXOw4Pre7ULHrBi6l5_25VvKQC1MdNr_0024s2g.Enum1.box);
				xBS7OSIVgwiqp2YsikrDtq6zcM87cw3nXOw4Pre7ULHrBi6l5_25VvKQC1MdNr_0024s2g.bv4xsECkipN_002441Wa7mcfeqY();
				_0024CMex_0024vAX35hsObIY7ThtQI.Add(xBS7OSIVgwiqp2YsikrDtq6zcM87cw3nXOw4Pre7ULHrBi6l5_25VvKQC1MdNr_0024s2g);
			}

			internal void izpvZaZUCSLwmueeHu9ZZJ0(lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw button)
			{
				XBS7OSIVgwiqp2YsikrDtq6zcM87cw3nXOw4Pre7ULHrBi6l5_25VvKQC1MdNr_0024s2g xBS7OSIVgwiqp2YsikrDtq6zcM87cw3nXOw4Pre7ULHrBi6l5_25VvKQC1MdNr_0024s2g = new XBS7OSIVgwiqp2YsikrDtq6zcM87cw3nXOw4Pre7ULHrBi6l5_25VvKQC1MdNr_0024s2g(AJ95PT5qH7SI3aQp44bnfe9rXdzyiYcVJnP8p242egZvaWA5f7vGynYUg9dQSkzYlA.Sl3qb7_zclIQRMX7sWCu5EE, Quaternion.identity, Construct_0, XBS7OSIVgwiqp2YsikrDtq6zcM87cw3nXOw4Pre7ULHrBi6l5_25VvKQC1MdNr_0024s2g.Enum1.spawnpoint);
				xBS7OSIVgwiqp2YsikrDtq6zcM87cw3nXOw4Pre7ULHrBi6l5_25VvKQC1MdNr_0024s2g.bv4xsECkipN_002441Wa7mcfeqY();
				_0024CMex_0024vAX35hsObIY7ThtQI.Add(xBS7OSIVgwiqp2YsikrDtq6zcM87cw3nXOw4Pre7ULHrBi6l5_25VvKQC1MdNr_0024s2g);
			}

			internal void jMcNZcDJqfLGhOI_DNdF3gM(lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw button)
			{
				XBS7OSIVgwiqp2YsikrDtq6zcM87cw3nXOw4Pre7ULHrBi6l5_25VvKQC1MdNr_0024s2g xBS7OSIVgwiqp2YsikrDtq6zcM87cw3nXOw4Pre7ULHrBi6l5_25VvKQC1MdNr_0024s2g = new XBS7OSIVgwiqp2YsikrDtq6zcM87cw3nXOw4Pre7ULHrBi6l5_25VvKQC1MdNr_0024s2g(AJ95PT5qH7SI3aQp44bnfe9rXdzyiYcVJnP8p242egZvaWA5f7vGynYUg9dQSkzYlA.Sl3qb7_zclIQRMX7sWCu5EE, Quaternion.identity, Construct_0, XBS7OSIVgwiqp2YsikrDtq6zcM87cw3nXOw4Pre7ULHrBi6l5_25VvKQC1MdNr_0024s2g.Enum1.gate);
				xBS7OSIVgwiqp2YsikrDtq6zcM87cw3nXOw4Pre7ULHrBi6l5_25VvKQC1MdNr_0024s2g.bv4xsECkipN_002441Wa7mcfeqY();
				_0024CMex_0024vAX35hsObIY7ThtQI.Add(xBS7OSIVgwiqp2YsikrDtq6zcM87cw3nXOw4Pre7ULHrBi6l5_25VvKQC1MdNr_0024s2g);
			}

			internal void ja5Tn2TNbssSbDvwJmR2Ogc(lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw _)
			{
				dz3caJwShuGQHB7LBjmInSE();
			}

			internal void jqKTZS_tJcxAgCe7_00246zNvCs(lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw _)
			{
				XU_0024J1o9n9AOB0F3rIkTIPFo();
			}

			internal void j0Cgap_bYAt6XN2ay9l_5M4(lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw _)
			{
				iwaUWG_oqbQT6zAcJU5iwzU = !iwaUWG_oqbQT6zAcJU5iwzU;
				E4e53KKkkQc5_yliBk0AUfU = AJ95PT5qH7SI3aQp44bnfe9rXdzyiYcVJnP8p242egZvaWA5f7vGynYUg9dQSkzYlA.Sl3qb7_zclIQRMX7sWCu5EE;
			}

			internal void kJcRHiucfUeb5HfwJNUaf64(bool _)
			{
				BFMkrNuUjUFB4B6P9DkaJ_s = vpxRY7V6xxVwrtWzUazGbAijYYgy_j9aOURndFg_MPmV1XpOIQGYmugcr08FohKkWZtGTI6r2M_fFifxmanSRrgmSgq6IJSK9HbxY_0024RtywzhAZtQnNleJNvuto3IE49fVw.move;
			}

			internal void kaerF79CX5dNZYdi8YsOdFs(bool _)
			{
				BFMkrNuUjUFB4B6P9DkaJ_s = vpxRY7V6xxVwrtWzUazGbAijYYgy_j9aOURndFg_MPmV1XpOIQGYmugcr08FohKkWZtGTI6r2M_fFifxmanSRrgmSgq6IJSK9HbxY_0024RtywzhAZtQnNleJNvuto3IE49fVw.rotate;
			}

			internal void y7E3jyiQNQ5gdvFwTdj95H0(bool _)
			{
				BFMkrNuUjUFB4B6P9DkaJ_s = vpxRY7V6xxVwrtWzUazGbAijYYgy_j9aOURndFg_MPmV1XpOIQGYmugcr08FohKkWZtGTI6r2M_fFifxmanSRrgmSgq6IJSK9HbxY_0024RtywzhAZtQnNleJNvuto3IE49fVw.scale;
			}

			internal void zIEP55DSdelRrFNx6ucy7i0(bool _)
			{
				BFMkrNuUjUFB4B6P9DkaJ_s = vpxRY7V6xxVwrtWzUazGbAijYYgy_j9aOURndFg_MPmV1XpOIQGYmugcr08FohKkWZtGTI6r2M_fFifxmanSRrgmSgq6IJSK9HbxY_0024RtywzhAZtQnNleJNvuto3IE49fVw.paint;
			}

			internal void zR_F_0024dxqymq8XTlTmLDkLIs(string text)
			{
				if (smethod_3((UnityEngine.Object)smethod_2(smethod_1()), (UnityEngine.Object)null))
				{
					bm4qNpKN8ThlleNq_0024v_0024DpHZBJQSkCOXr8EFjC57_0024ief7(new Vector3(float.Parse(text), smethod_5(smethod_4(ra5MAEm12eKHvW9LN8brPaE)).y, smethod_5(smethod_4(ra5MAEm12eKHvW9LN8brPaE)).z));
				}
			}

			internal void zgtSxiy6WPb_Cg7ydrpyq6E(string text)
			{
				if (smethod_3((UnityEngine.Object)smethod_2(smethod_1()), (UnityEngine.Object)null))
				{
					bm4qNpKN8ThlleNq_0024v_0024DpHZBJQSkCOXr8EFjC57_0024ief7(new Vector3(smethod_5(smethod_4(ra5MAEm12eKHvW9LN8brPaE)).x, float.Parse(text), smethod_5(smethod_4(ra5MAEm12eKHvW9LN8brPaE)).z));
				}
			}

			internal void zycGA_0024Mnd6J70DJOrgJOwyM(string text)
			{
				if (smethod_3((UnityEngine.Object)smethod_2(smethod_1()), (UnityEngine.Object)null))
				{
					bm4qNpKN8ThlleNq_0024v_0024DpHZBJQSkCOXr8EFjC57_0024ief7(new Vector3(smethod_5(smethod_4(ra5MAEm12eKHvW9LN8brPaE)).x, smethod_5(smethod_4(ra5MAEm12eKHvW9LN8brPaE)).y, float.Parse(text)));
				}
			}

			internal void method_0(float value)
			{
				if (smethod_6(smethod_1()))
				{
					if (value >= 90f)
					{
						value = 89f;
					}
					else if (value <= -90f)
					{
						value = -89f;
					}
					Vector3 eulerAngles = smethod_7(fbSm64A_0024FzBvAjtith640zQ[0].TIWjI8FsBk2nlZk9NO4HNOE).eulerAngles;
					fbSm64A_0024FzBvAjtith640zQ[0].TIWjI8FsBk2nlZk9NO4HNOE.rotation = Quaternion.Euler(value, eulerAngles.y, eulerAngles.z);
				}
			}

			internal void method_1(float value)
			{
				if (smethod_6(smethod_1()))
				{
					Vector3 eulerAngles = smethod_7(fbSm64A_0024FzBvAjtith640zQ[0].TIWjI8FsBk2nlZk9NO4HNOE).eulerAngles;
					fbSm64A_0024FzBvAjtith640zQ[0].TIWjI8FsBk2nlZk9NO4HNOE.rotation = Quaternion.Euler(eulerAngles.x, value, eulerAngles.z);
				}
			}

			internal void method_2(float value)
			{
				if (smethod_6(smethod_1()))
				{
					Vector3 eulerAngles = smethod_7(fbSm64A_0024FzBvAjtith640zQ[0].TIWjI8FsBk2nlZk9NO4HNOE).eulerAngles;
					fbSm64A_0024FzBvAjtith640zQ[0].TIWjI8FsBk2nlZk9NO4HNOE.rotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y, value);
				}
			}

			internal void method_3(float value)
			{
				if (smethod_6(smethod_1()))
				{
					if (fbSm64A_0024FzBvAjtith640zQ[0].MrZDDetpveRMT__0024biC7h8tU != xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA.Enum0.gate)
					{
						Vector3 vector = smethod_8(fbSm64A_0024FzBvAjtith640zQ[0].TIWjI8FsBk2nlZk9NO4HNOE);
						fbSm64A_0024FzBvAjtith640zQ[0].TIWjI8FsBk2nlZk9NO4HNOE.localScale = new Vector3(value, vector.y, vector.z);
						fbSm64A_0024FzBvAjtith640zQ[0].method_0();
					}
					else
					{
						Vector3 localScale = fbSm64A_0024FzBvAjtith640zQ[0].cRC1fFhZVgamcERb3o0WavI.transform.localScale;
						fbSm64A_0024FzBvAjtith640zQ[0].cRC1fFhZVgamcERb3o0WavI.transform.localScale = new Vector3(value, localScale.y, localScale.z);
					}
				}
			}

			internal void method_4(float value)
			{
				if (smethod_6(smethod_1()))
				{
					if (fbSm64A_0024FzBvAjtith640zQ[0].MrZDDetpveRMT__0024biC7h8tU != xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA.Enum0.gate)
					{
						Vector3 vector = smethod_8(fbSm64A_0024FzBvAjtith640zQ[0].TIWjI8FsBk2nlZk9NO4HNOE);
						fbSm64A_0024FzBvAjtith640zQ[0].TIWjI8FsBk2nlZk9NO4HNOE.localScale = new Vector3(vector.x, value, vector.z);
						fbSm64A_0024FzBvAjtith640zQ[0].method_0();
					}
					else
					{
						Vector3 localScale = fbSm64A_0024FzBvAjtith640zQ[0].cRC1fFhZVgamcERb3o0WavI.transform.localScale;
						fbSm64A_0024FzBvAjtith640zQ[0].cRC1fFhZVgamcERb3o0WavI.transform.localScale = new Vector3(localScale.x, value, localScale.z);
					}
				}
			}

			internal void zlvHFtI6Br5kTKIb_0024Gf3f_0024g(float value)
			{
				if (smethod_6(smethod_1()))
				{
					Vector3 vector = smethod_8(fbSm64A_0024FzBvAjtith640zQ[0].TIWjI8FsBk2nlZk9NO4HNOE);
					fbSm64A_0024FzBvAjtith640zQ[0].TIWjI8FsBk2nlZk9NO4HNOE.localScale = new Vector3(vector.x, vector.y, value);
					fbSm64A_0024FzBvAjtith640zQ[0].method_0();
				}
			}

			internal void method_5(bool _)
			{
				IdwU0_ARpD_wnWGSdVq151k.a = 1f;
			}

			internal void method_6(bool _)
			{
				IdwU0_ARpD_wnWGSdVq151k.a = 0f;
			}

			internal void method_7(float value)
			{
				IdwU0_ARpD_wnWGSdVq151k.r = value / 255f;
			}

			internal void method_8(float value)
			{
				IdwU0_ARpD_wnWGSdVq151k.g = value / 255f;
			}

			internal void method_9(float value)
			{
				IdwU0_ARpD_wnWGSdVq151k.b = value / 255f;
			}

			internal static bool smethod_0(UnityEngine.Object object_0, UnityEngine.Object object_1)
			{
				return object_0 == object_1;
			}

			internal static EventSystem smethod_1()
			{
				return EventSystem.current;
			}

			internal static GameObject smethod_2(EventSystem eventSystem_0)
			{
				return eventSystem_0.currentSelectedGameObject;
			}

			internal static bool smethod_3(UnityEngine.Object object_0, UnityEngine.Object object_1)
			{
				return object_0 != object_1;
			}

			internal static Transform smethod_4(GameObject gameObject_0)
			{
				return gameObject_0.transform;
			}

			internal static Vector3 smethod_5(Transform transform_0)
			{
				return transform_0.position;
			}

			internal static bool smethod_6(EventSystem eventSystem_0)
			{
				return eventSystem_0.IsPointerOverGameObject();
			}

			internal static Quaternion smethod_7(Transform transform_0)
			{
				return transform_0.rotation;
			}

			internal static Vector3 smethod_8(Transform transform_0)
			{
				return transform_0.localScale;
			}
		}

		[CompilerGenerated]
		private sealed class oGcOeB_JND2Yf4MrR30ymvpG_yhHQAKmTc9io3pzJtpnEZby5iOT3rhoy3VFnd_757iISUfMa5mF7FEj10c7V2oBfq_0024Tv3CMsaMghn7I98pCGadZphq8UvJZ_0024_QjFZSW11y_Iqlc_0024NOKJQYyGUK30ME
		{
			public RectTransform OexJ484iKCgUPnPT5r_0024MIHw;

			public RawImage YvRrQLXRVCG4kwr5EKSrKUA;

			public Control0 a33W7qtUsLkdEvrMqRh76cI;

			public Control0 sJHJHLbGY5pCahRjwxFWV8Y;

			public Control0 _8oassVGhyJhzvMve_RCFEc;

			public Control0 FrhaIjlOZnCeLu6RAcU9oTU;

			public Control0 r7bkh1RM_YDzdrhuv21BFaY;

			public Control0 XddBW_ndkTCvg7AWjg4X9_0024Y;

			public Control0 EG2RuiZozaDMQatkBF5_CPc;

			public Image Xz1wKOB0iImarofJ3M1e0ew;

			public SliderController vEtQdo8p_AEmER5CcHm5SuY;

			public SliderController kxEO3iCdS41Pdgor06VGp9k;

			public SliderController DCWStzN3LM5SCegNzKVghlM;

			internal void nZZStBIHSVetmDFzPsDCiF0(AmMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F me)
			{
				if (smethod_0(0))
				{
					Vector2 vector2_ = default(Vector2);
					smethod_4(OexJ484iKCgUPnPT5r_0024MIHw, (Vector2)smethod_1(), smethod_3(smethod_2(global::_003CModule_003E.smethod_29<string>(618767629u)).GetComponent<Canvas>()), ref vector2_);
					if (smethod_5(OexJ484iKCgUPnPT5r_0024MIHw).Contains(vector2_))
					{
						vector2_ += new Vector2(OexJ484iKCgUPnPT5r_0024MIHw.sizeDelta.x * OexJ484iKCgUPnPT5r_0024MIHw.pivot.x, OexJ484iKCgUPnPT5r_0024MIHw.sizeDelta.y * OexJ484iKCgUPnPT5r_0024MIHw.pivot.y);
						float a = IdwU0_ARpD_wnWGSdVq151k.a;
						IdwU0_ARpD_wnWGSdVq151k = ((Texture2D)YvRrQLXRVCG4kwr5EKSrKUA.texture).GetPixel((int)(vector2_.x / OexJ484iKCgUPnPT5r_0024MIHw.sizeDelta.x * (float)YvRrQLXRVCG4kwr5EKSrKUA.texture.width), (int)(vector2_.y / OexJ484iKCgUPnPT5r_0024MIHw.sizeDelta.y * (float)YvRrQLXRVCG4kwr5EKSrKUA.texture.height));
						IdwU0_ARpD_wnWGSdVq151k.a = a;
						KEFHJCGICLE.HNAHBIMJDCB(global::_003CModule_003E.smethod_25<string>(2341604922u));
					}
				}
			}

			internal void oJu3NfCyyM9hD71SJmPJGs4(bool toggled)
			{
				if (a33W7qtUsLkdEvrMqRh76cI.hLxnG9Hq33zU_YUsu_00240_zak)
				{
					a33W7qtUsLkdEvrMqRh76cI.kBPtltqxQQyZ5ym0_0024wsbeUc();
				}
				if (sJHJHLbGY5pCahRjwxFWV8Y.hLxnG9Hq33zU_YUsu_00240_zak)
				{
					sJHJHLbGY5pCahRjwxFWV8Y.kBPtltqxQQyZ5ym0_0024wsbeUc();
				}
				if (_8oassVGhyJhzvMve_RCFEc.hLxnG9Hq33zU_YUsu_00240_zak)
				{
					_8oassVGhyJhzvMve_RCFEc.kBPtltqxQQyZ5ym0_0024wsbeUc();
				}
				if (FrhaIjlOZnCeLu6RAcU9oTU.hLxnG9Hq33zU_YUsu_00240_zak)
				{
					FrhaIjlOZnCeLu6RAcU9oTU.kBPtltqxQQyZ5ym0_0024wsbeUc();
				}
				if ((r7bkh1RM_YDzdrhuv21BFaY.hLxnG9Hq33zU_YUsu_00240_zak || XddBW_ndkTCvg7AWjg4X9_0024Y.hLxnG9Hq33zU_YUsu_00240_zak) && toggled)
				{
					EG2RuiZozaDMQatkBF5_CPc.hLxnG9Hq33zU_YUsu_00240_zak = false;
				}
			}

			internal void ob_CqMwb2AsbqKLInhknZoc(bool _)
			{
				IdwU0_ARpD_wnWGSdVq151k.a = kILN_0024q_tbcSASqORkmJ7BBsDUjxuSBqI3VGW6VcIauaSSt6_Qk8HVbbHG_0024WDerWUiLyHmclIfPXtnf3BNWVqydk.XMUQ93RVvJVE5FPCbS_00248Hxs - (EG2RuiZozaDMQatkBF5_CPc.hLxnG9Hq33zU_YUsu_00240_zak ? (4f / 51f) : 0f);
			}

			internal void otoGWko7yP665ecgGaa_0024Zr8(bool _)
			{
				IdwU0_ARpD_wnWGSdVq151k.a = kILN_0024q_tbcSASqORkmJ7BBsDUjxuSBqI3VGW6VcIauaSSt6_Qk8HVbbHG_0024WDerWUiLyHmclIfPXtnf3BNWVqydk.C5AcE8qkQZfZumQ__0024d8JU2A - (EG2RuiZozaDMQatkBF5_CPc.hLxnG9Hq33zU_YUsu_00240_zak ? (4f / 51f) : 0f);
			}

			internal void o1nnlNVCd17TviQTi4x3RZk(bool _)
			{
				IdwU0_ARpD_wnWGSdVq151k.a = kILN_0024q_tbcSASqORkmJ7BBsDUjxuSBqI3VGW6VcIauaSSt6_Qk8HVbbHG_0024WDerWUiLyHmclIfPXtnf3BNWVqydk.creNoS1TDqnabY284sL4AzA - (EG2RuiZozaDMQatkBF5_CPc.hLxnG9Hq33zU_YUsu_00240_zak ? (4f / 51f) : 0f);
			}

			internal void pPPaaKxLu78_0024KdPxLUam84E(bool _)
			{
				IdwU0_ARpD_wnWGSdVq151k.a = kILN_0024q_tbcSASqORkmJ7BBsDUjxuSBqI3VGW6VcIauaSSt6_Qk8HVbbHG_0024WDerWUiLyHmclIfPXtnf3BNWVqydk.dzadJ16eTlgbYulPr_znFoE - (EG2RuiZozaDMQatkBF5_CPc.hLxnG9Hq33zU_YUsu_00240_zak ? (4f / 51f) : 0f);
			}

			internal void pYtm4yqh23ogHh1vyDvfD94(AmMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F me)
			{
				Xz1wKOB0iImarofJ3M1e0ew.color = new Color(IdwU0_ARpD_wnWGSdVq151k.r, IdwU0_ARpD_wnWGSdVq151k.g, IdwU0_ARpD_wnWGSdVq151k.b);
				if (!MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.moreStageboxMats)
				{
					r7bkh1RM_YDzdrhuv21BFaY.hLxnG9Hq33zU_YUsu_00240_zak = IdwU0_ARpD_wnWGSdVq151k.a >= 0.9f;
				}
				else
				{
					r7bkh1RM_YDzdrhuv21BFaY.hLxnG9Hq33zU_YUsu_00240_zak = kILN_0024q_tbcSASqORkmJ7BBsDUjxuSBqI3VGW6VcIauaSSt6_Qk8HVbbHG_0024WDerWUiLyHmclIfPXtnf3BNWVqydk.zfXLeeKUOMpnTqAgpbUcX9g(IdwU0_ARpD_wnWGSdVq151k.a, 1f);
					a33W7qtUsLkdEvrMqRh76cI.hLxnG9Hq33zU_YUsu_00240_zak = kILN_0024q_tbcSASqORkmJ7BBsDUjxuSBqI3VGW6VcIauaSSt6_Qk8HVbbHG_0024WDerWUiLyHmclIfPXtnf3BNWVqydk.zfXLeeKUOMpnTqAgpbUcX9g(IdwU0_ARpD_wnWGSdVq151k.a, kILN_0024q_tbcSASqORkmJ7BBsDUjxuSBqI3VGW6VcIauaSSt6_Qk8HVbbHG_0024WDerWUiLyHmclIfPXtnf3BNWVqydk.XMUQ93RVvJVE5FPCbS_00248Hxs);
					sJHJHLbGY5pCahRjwxFWV8Y.hLxnG9Hq33zU_YUsu_00240_zak = kILN_0024q_tbcSASqORkmJ7BBsDUjxuSBqI3VGW6VcIauaSSt6_Qk8HVbbHG_0024WDerWUiLyHmclIfPXtnf3BNWVqydk.zfXLeeKUOMpnTqAgpbUcX9g(IdwU0_ARpD_wnWGSdVq151k.a, kILN_0024q_tbcSASqORkmJ7BBsDUjxuSBqI3VGW6VcIauaSSt6_Qk8HVbbHG_0024WDerWUiLyHmclIfPXtnf3BNWVqydk.C5AcE8qkQZfZumQ__0024d8JU2A);
					_8oassVGhyJhzvMve_RCFEc.hLxnG9Hq33zU_YUsu_00240_zak = kILN_0024q_tbcSASqORkmJ7BBsDUjxuSBqI3VGW6VcIauaSSt6_Qk8HVbbHG_0024WDerWUiLyHmclIfPXtnf3BNWVqydk.zfXLeeKUOMpnTqAgpbUcX9g(IdwU0_ARpD_wnWGSdVq151k.a, kILN_0024q_tbcSASqORkmJ7BBsDUjxuSBqI3VGW6VcIauaSSt6_Qk8HVbbHG_0024WDerWUiLyHmclIfPXtnf3BNWVqydk.creNoS1TDqnabY284sL4AzA);
					FrhaIjlOZnCeLu6RAcU9oTU.hLxnG9Hq33zU_YUsu_00240_zak = kILN_0024q_tbcSASqORkmJ7BBsDUjxuSBqI3VGW6VcIauaSSt6_Qk8HVbbHG_0024WDerWUiLyHmclIfPXtnf3BNWVqydk.zfXLeeKUOMpnTqAgpbUcX9g(IdwU0_ARpD_wnWGSdVq151k.a, kILN_0024q_tbcSASqORkmJ7BBsDUjxuSBqI3VGW6VcIauaSSt6_Qk8HVbbHG_0024WDerWUiLyHmclIfPXtnf3BNWVqydk.dzadJ16eTlgbYulPr_znFoE);
					if (!r7bkh1RM_YDzdrhuv21BFaY.hLxnG9Hq33zU_YUsu_00240_zak && !XddBW_ndkTCvg7AWjg4X9_0024Y.hLxnG9Hq33zU_YUsu_00240_zak)
					{
						EG2RuiZozaDMQatkBF5_CPc.hLxnG9Hq33zU_YUsu_00240_zak = kILN_0024q_tbcSASqORkmJ7BBsDUjxuSBqI3VGW6VcIauaSSt6_Qk8HVbbHG_0024WDerWUiLyHmclIfPXtnf3BNWVqydk.RABTF797cxk849f9Wpnefo0(IdwU0_ARpD_wnWGSdVq151k.a, includeOnlyDummyWater: true);
					}
					else
					{
						EG2RuiZozaDMQatkBF5_CPc.hLxnG9Hq33zU_YUsu_00240_zak = false;
					}
				}
				XddBW_ndkTCvg7AWjg4X9_0024Y.hLxnG9Hq33zU_YUsu_00240_zak = IdwU0_ARpD_wnWGSdVq151k.a == 0f;
			}

			internal void oAiVx2zR_MzXX1dFD68rBEI(AmMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F me)
			{
				smethod_6(H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.H1Pr8mmPslEwoIU22reQcXE(vEtQdo8p_AEmER5CcHm5SuY), (float)(int)(IdwU0_ARpD_wnWGSdVq151k.r * 255f));
			}

			internal void oqC93mTmVGrcCxT4HbqAQ_0024E(AmMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F me)
			{
				smethod_6(H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.H1Pr8mmPslEwoIU22reQcXE(kxEO3iCdS41Pdgor06VGp9k), (float)(int)(IdwU0_ARpD_wnWGSdVq151k.g * 255f));
			}

			internal void pEbOSIVEvuPdnSME17e0HpY(AmMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F me)
			{
				smethod_6(H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.H1Pr8mmPslEwoIU22reQcXE(DCWStzN3LM5SCegNzKVghlM), (float)(int)(IdwU0_ARpD_wnWGSdVq151k.b * 255f));
			}

			internal static bool smethod_0(int int_0)
			{
				return Input.GetMouseButtonDown(int_0);
			}

			internal static Vector3 smethod_1()
			{
				return Input.mousePosition;
			}

			internal static GameObject smethod_2(string string_0)
			{
				return GameObject.Find(string_0);
			}

			internal static Camera smethod_3(Canvas canvas_0)
			{
				return canvas_0.worldCamera;
			}

			internal static bool smethod_4(RectTransform rectTransform_0, Vector2 vector2_0, Camera camera_0, ref Vector2 vector2_1)
			{
				return RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform_0, vector2_0, camera_0, out vector2_1);
			}

			internal static Rect smethod_5(RectTransform rectTransform_0)
			{
				return rectTransform_0.rect;
			}

			internal static void smethod_6(Slider slider_0, float float_0)
			{
				slider_0.value = float_0;
			}
		}

		[HarmonyTranspiler]
		private static IEnumerable<CodeInstruction> ME8a9fXTPeYc1JTUjzrWW9E(IEnumerable<CodeInstruction> instructions)
		{
			CodeInstruction[] array = instructions.ToArray();
			List<CodeInstruction> list = new List<CodeInstruction>();
			for (int i = 0; i < array.Length; i++)
			{
				if (i < 891 || i > 910)
				{
					list.Add(array[i]);
				}
			}
			return list;
		}

		[HarmonyPrefix]
		internal static bool smethod_0(Construct __instance, GameObject ___ALHJPMAMFJD)
		{
			smethod_2(smethod_1((SceneMan)__instance, global::_003CModule_003E.smethod_28<string>(2607071329u)), bool_0: false);
			if (!smethod_4(smethod_3((SceneMan)__instance, global::_003CModule_003E.smethod_25<string>(2300289603u))))
			{
				smethod_2(___ALHJPMAMFJD, smethod_5(smethod_3((SceneMan)__instance, global::_003CModule_003E.smethod_29<string>(791573748u)).GetComponent<Toggle>()));
			}
			if (!smethod_7(smethod_6((SceneMan)__instance, global::_003CModule_003E.smethod_25<string>(323464656u))) && Boolean_0)
			{
				fbSm64A_0024FzBvAjtith640zQ.RemoveAll((xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA item) => CJMhv67dSW_yQIGoFtPlWcbgax_OQ5_0024Fp37aG7YfGb4sgG2anRt83J5mUK3_0024zPVqEeh_0024o4C68bhHfF2mrd4hR_rnT8aeby_0024sgZI9VBYschbMognZXTsqSk8yWwRsn2mY7A.smethod_0((UnityEngine.Object)item.TIWjI8FsBk2nlZk9NO4HNOE, (UnityEngine.Object)null));
				fXmKrvqNNkqzpO5XpWPRVZI8_mbex_0024Y2G0UhxyZZLrx0.RemoveAll((c2wojWPyeJ90X8UxbVw8Ie7t2iSXT4W6AM5UctjLMkqq item) => CJMhv67dSW_yQIGoFtPlWcbgax_OQ5_0024Fp37aG7YfGb4sgG2anRt83J5mUK3_0024zPVqEeh_0024o4C68bhHfF2mrd4hR_rnT8aeby_0024sgZI9VBYschbMognZXTsqSk8yWwRsn2mY7A.smethod_0((UnityEngine.Object)item, (UnityEngine.Object)null));
				AJ95PT5qH7SI3aQp44bnfe9rXdzyiYcVJnP8p242egZvaWA5f7vGynYUg9dQSkzYlA.YZ_0024PBkn7rausEmKONRxFfNZQD6SNoR50G7r60U9WBOJ4.yNEegNKtGknTMZqZvM5dqjw(smethod_14(1) || smethod_15(SystemData.EHLMFKOOHLI.SystemMenu));
				if (AJ95PT5qH7SI3aQp44bnfe9rXdzyiYcVJnP8p242egZvaWA5f7vGynYUg9dQSkzYlA.YZ_0024PBkn7rausEmKONRxFfNZQD6SNoR50G7r60U9WBOJ4.yORc5mAq969v9kZYfhyjAiM())
				{
					AJ95PT5qH7SI3aQp44bnfe9rXdzyiYcVJnP8p242egZvaWA5f7vGynYUg9dQSkzYlA.Sl3qb7_zclIQRMX7sWCu5EE += nEQx4bSP4XevQkXYc14vvjtozH0DIbIMC6vTmSaFpKo7();
				}
				if (smethod_9((UnityEngine.Object)AZjgkDHxHA_hM7bKEiV7ES8, (UnityEngine.Object)null))
				{
					AZjgkDHxHA_hM7bKEiV7ES8 = smethod_16(global::_003CModule_003E.smethod_28<string>(2151771824u)).AddComponent<c2wojWPyeJ90X8UxbVw8Ie7t2iSXT4W6AM5UctjLMkqq>();
					AZjgkDHxHA_hM7bKEiV7ES8.u5ER09FBgDoEuNjNt6mdw_k = true;
					WsaQ0Aqmeh9ZQLlVWABNM60 = smethod_16(global::_003CModule_003E.smethod_26<string>(3741542865u));
					GameObject gameObject_ = xM7Nbz4HRE3trwkiP8MNNfQ(global::_003CModule_003E.smethod_28<string>(1271476712u), Color.red, Quaternion.Euler(0f, 0f, -90f));
					smethod_18(smethod_17(gameObject_), smethod_17(WsaQ0Aqmeh9ZQLlVWABNM60));
					smethod_17(gameObject_).localPosition = new Vector3(0.5f, 0f, 0f);
					GameObject gameObject = xM7Nbz4HRE3trwkiP8MNNfQ(global::_003CModule_003E.smethod_29<string>(618835096u), Color.red, Quaternion.Euler(0f, 0f, 90f));
					gameObject.transform.parent = WsaQ0Aqmeh9ZQLlVWABNM60.transform;
					gameObject.transform.localPosition = new Vector3(-0.5f, 0f, 0f);
					GameObject gameObject2 = xM7Nbz4HRE3trwkiP8MNNfQ(global::_003CModule_003E.smethod_26<string>(2577193298u), Color.green, Quaternion.Euler(0f, 0f, 0f));
					gameObject2.transform.parent = WsaQ0Aqmeh9ZQLlVWABNM60.transform;
					gameObject2.transform.localPosition = new Vector3(0f, 0.5f, 0f);
					GameObject gameObject3 = xM7Nbz4HRE3trwkiP8MNNfQ(global::_003CModule_003E.smethod_28<string>(725117306u), Color.green, Quaternion.Euler(0f, 0f, 180f));
					gameObject3.transform.parent = WsaQ0Aqmeh9ZQLlVWABNM60.transform;
					gameObject3.transform.localPosition = new Vector3(0f, -0.5f, 0f);
					GameObject gameObject4 = xM7Nbz4HRE3trwkiP8MNNfQ(global::_003CModule_003E.smethod_25<string>(3693818337u), Color.blue, Quaternion.Euler(90f, 0f, 0f));
					gameObject4.transform.parent = WsaQ0Aqmeh9ZQLlVWABNM60.transform;
					gameObject4.transform.localPosition = new Vector3(0f, 0f, 0.5f);
					GameObject gameObject5 = xM7Nbz4HRE3trwkiP8MNNfQ(global::_003CModule_003E.smethod_26<string>(2618856832u), Color.blue, Quaternion.Euler(-90f, 0f, 0f));
					gameObject5.transform.parent = WsaQ0Aqmeh9ZQLlVWABNM60.transform;
					gameObject5.transform.localPosition = new Vector3(0f, 0f, -0.5f);
					WsaQ0Aqmeh9ZQLlVWABNM60.SetActive(value: false);
					ra5MAEm12eKHvW9LN8brPaE = new GameObject(global::_003CModule_003E.smethod_29<string>(337521718u));
				}
				if (XVDZE4YUgxeonNUvpvABIb4 == null)
				{
					XVDZE4YUgxeonNUvpvABIb4 = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.qSsYOsEQb7x452a9Y45dVEk(Vector3.zero, new Vector2(420f, 660f), GameObject.Find(global::_003CModule_003E.smethod_28<string>(3214927773u)).transform);
					XVDZE4YUgxeonNUvpvABIb4.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 60f);
					Action<lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw> onClick = delegate
					{
						XBS7OSIVgwiqp2YsikrDtq6zcM87cw3nXOw4Pre7ULHrBi6l5_25VvKQC1MdNr_0024s2g xBS7OSIVgwiqp2YsikrDtq6zcM87cw3nXOw4Pre7ULHrBi6l5_25VvKQC1MdNr_0024s2g = new XBS7OSIVgwiqp2YsikrDtq6zcM87cw3nXOw4Pre7ULHrBi6l5_25VvKQC1MdNr_0024s2g(AJ95PT5qH7SI3aQp44bnfe9rXdzyiYcVJnP8p242egZvaWA5f7vGynYUg9dQSkzYlA.Sl3qb7_zclIQRMX7sWCu5EE, Quaternion.identity, Construct_0, XBS7OSIVgwiqp2YsikrDtq6zcM87cw3nXOw4Pre7ULHrBi6l5_25VvKQC1MdNr_0024s2g.Enum1.box);
						xBS7OSIVgwiqp2YsikrDtq6zcM87cw3nXOw4Pre7ULHrBi6l5_25VvKQC1MdNr_0024s2g.bv4xsECkipN_002441Wa7mcfeqY();
						_0024CMex_0024vAX35hsObIY7ThtQI.Add(xBS7OSIVgwiqp2YsikrDtq6zcM87cw3nXOw4Pre7ULHrBi6l5_25VvKQC1MdNr_0024s2g);
					};
					Action<lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw> onClick2 = delegate
					{
						XBS7OSIVgwiqp2YsikrDtq6zcM87cw3nXOw4Pre7ULHrBi6l5_25VvKQC1MdNr_0024s2g xBS7OSIVgwiqp2YsikrDtq6zcM87cw3nXOw4Pre7ULHrBi6l5_25VvKQC1MdNr_0024s2g = new XBS7OSIVgwiqp2YsikrDtq6zcM87cw3nXOw4Pre7ULHrBi6l5_25VvKQC1MdNr_0024s2g(AJ95PT5qH7SI3aQp44bnfe9rXdzyiYcVJnP8p242egZvaWA5f7vGynYUg9dQSkzYlA.Sl3qb7_zclIQRMX7sWCu5EE, Quaternion.identity, Construct_0, XBS7OSIVgwiqp2YsikrDtq6zcM87cw3nXOw4Pre7ULHrBi6l5_25VvKQC1MdNr_0024s2g.Enum1.spawnpoint);
						xBS7OSIVgwiqp2YsikrDtq6zcM87cw3nXOw4Pre7ULHrBi6l5_25VvKQC1MdNr_0024s2g.bv4xsECkipN_002441Wa7mcfeqY();
						_0024CMex_0024vAX35hsObIY7ThtQI.Add(xBS7OSIVgwiqp2YsikrDtq6zcM87cw3nXOw4Pre7ULHrBi6l5_25VvKQC1MdNr_0024s2g);
					};
					Action<lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw> onClick3 = delegate
					{
						XBS7OSIVgwiqp2YsikrDtq6zcM87cw3nXOw4Pre7ULHrBi6l5_25VvKQC1MdNr_0024s2g xBS7OSIVgwiqp2YsikrDtq6zcM87cw3nXOw4Pre7ULHrBi6l5_25VvKQC1MdNr_0024s2g = new XBS7OSIVgwiqp2YsikrDtq6zcM87cw3nXOw4Pre7ULHrBi6l5_25VvKQC1MdNr_0024s2g(AJ95PT5qH7SI3aQp44bnfe9rXdzyiYcVJnP8p242egZvaWA5f7vGynYUg9dQSkzYlA.Sl3qb7_zclIQRMX7sWCu5EE, Quaternion.identity, Construct_0, XBS7OSIVgwiqp2YsikrDtq6zcM87cw3nXOw4Pre7ULHrBi6l5_25VvKQC1MdNr_0024s2g.Enum1.gate);
						xBS7OSIVgwiqp2YsikrDtq6zcM87cw3nXOw4Pre7ULHrBi6l5_25VvKQC1MdNr_0024s2g.bv4xsECkipN_002441Wa7mcfeqY();
						_0024CMex_0024vAX35hsObIY7ThtQI.Add(xBS7OSIVgwiqp2YsikrDtq6zcM87cw3nXOw4Pre7ULHrBi6l5_25VvKQC1MdNr_0024s2g);
					};
					int num = 620;
					Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.P3tLroX6fcPfQha_0024JdbpYXg(global::_003CModule_003E.smethod_29<string>(1083989826u), new Vector2(-100f, 620f), global::_003CModule_003E.smethod_25<string>(2292351989u), onClick, XVDZE4YUgxeonNUvpvABIb4.transform);
					num = 570;
					Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.P3tLroX6fcPfQha_0024JdbpYXg(global::_003CModule_003E.smethod_29<string>(56208340u), new Vector2(-100f, 570f), global::_003CModule_003E.smethod_29<string>(802676448u), onClick2, XVDZE4YUgxeonNUvpvABIb4.transform);
					num = 520;
					Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.P3tLroX6fcPfQha_0024JdbpYXg(global::_003CModule_003E.smethod_27<string>(4134819398u), new Vector2(-100f, 520f), global::_003CModule_003E.smethod_27<string>(2589933354u), onClick3, XVDZE4YUgxeonNUvpvABIb4.transform);
					num = 470;
					num = 420;
					Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.P3tLroX6fcPfQha_0024JdbpYXg(global::_003CModule_003E.smethod_28<string>(3912213841u), new Vector2(-100f, 420f), global::_003CModule_003E.smethod_28<string>(3639034138u), delegate
					{
						dz3caJwShuGQHB7LBjmInSE();
					}, XVDZE4YUgxeonNUvpvABIb4.transform);
					num -= 50;
					Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.P3tLroX6fcPfQha_0024JdbpYXg(global::_003CModule_003E.smethod_25<string>(1550303496u), new Vector2(-100f, num), global::_003CModule_003E.smethod_26<string>(248494164u), delegate
					{
						XU_0024J1o9n9AOB0F3rIkTIPFo();
					}, XVDZE4YUgxeonNUvpvABIb4.transform);
					num -= 50;
					Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.P3tLroX6fcPfQha_0024JdbpYXg(global::_003CModule_003E.smethod_28<string>(1165264862u), new Vector2(-100f, num), global::_003CModule_003E.smethod_28<string>(892085159u), delegate
					{
						iwaUWG_oqbQT6zAcJU5iwzU = !iwaUWG_oqbQT6zAcJU5iwzU;
						E4e53KKkkQc5_yliBk0AUfU = AJ95PT5qH7SI3aQp44bnfe9rXdzyiYcVJnP8p242egZvaWA5f7vGynYUg9dQSkzYlA.Sl3qb7_zclIQRMX7sWCu5EE;
					}, XVDZE4YUgxeonNUvpvABIb4.transform);
					iTtUTzVJqoLove2Mlyfm4HQ = new GameObject(global::_003CModule_003E.smethod_27<string>(1910551682u));
					ToggleGroup toggleGroup = iTtUTzVJqoLove2Mlyfm4HQ.AddComponent<ToggleGroup>();
					iTtUTzVJqoLove2Mlyfm4HQ.transform.parent = GameObject.Find(global::_003CModule_003E.smethod_27<string>(862174272u)).transform;
					RectTransform rectTransform = iTtUTzVJqoLove2Mlyfm4HQ.AddComponent<RectTransform>();
					rectTransform.anchorMin = new Vector2(0f, 1f);
					rectTransform.anchorMax = new Vector2(0f, 1f);
					rectTransform.anchoredPosition = Vector2.zero;
					VEnLkFeX6Tle_M_0024bJO7QAfw = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.uEsWMK_pFkCY_0024M5zt8zLsQk(global::_003CModule_003E.smethod_27<string>(751887149u), new Vector2(200f, 0f), global::_003CModule_003E.smethod_26<string>(373484766u), iTtUTzVJqoLove2Mlyfm4HQ.transform, resetGroup: true, delegate
					{
						BFMkrNuUjUFB4B6P9DkaJ_s = vpxRY7V6xxVwrtWzUazGbAijYYgy_j9aOURndFg_MPmV1XpOIQGYmugcr08FohKkWZtGTI6r2M_fFifxmanSRrgmSgq6IJSK9HbxY_0024RtywzhAZtQnNleJNvuto3IE49fVw.move;
					}, null, toggleGroup);
					VEnLkFeX6Tle_M_0024bJO7QAfw.DmPZGWxJ26_0024f_0024QOvQiqpmW8(new Vector2(0f, 30f));
					VEnLkFeX6Tle_M_0024bJO7QAfw.gameObject.smethod_0(global::_003CModule_003E.smethod_27<string>(1947539503u)).transform.localPosition = new Vector3(0f, (0f - ((RectTransform)VEnLkFeX6Tle_M_0024bJO7QAfw.transform).sizeDelta.y) / 4f);
					JuznlPC_0024iVvWRp5Gx3m19rQ = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.uEsWMK_pFkCY_0024M5zt8zLsQk(global::_003CModule_003E.smethod_27<string>(3501968401u), new Vector2(285f, 0f), global::_003CModule_003E.smethod_25<string>(948076188u), iTtUTzVJqoLove2Mlyfm4HQ.transform, resetGroup: true, delegate
					{
						BFMkrNuUjUFB4B6P9DkaJ_s = vpxRY7V6xxVwrtWzUazGbAijYYgy_j9aOURndFg_MPmV1XpOIQGYmugcr08FohKkWZtGTI6r2M_fFifxmanSRrgmSgq6IJSK9HbxY_0024RtywzhAZtQnNleJNvuto3IE49fVw.rotate;
					}, null, toggleGroup);
					JuznlPC_0024iVvWRp5Gx3m19rQ.DmPZGWxJ26_0024f_0024QOvQiqpmW8(new Vector2(0f, 30f));
					JuznlPC_0024iVvWRp5Gx3m19rQ.gameObject.smethod_0(global::_003CModule_003E.smethod_26<string>(769079470u)).transform.localPosition = new Vector3(0f, (0f - ((RectTransform)VEnLkFeX6Tle_M_0024bJO7QAfw.transform).sizeDelta.y) / 4f);
					control0_0 = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.uEsWMK_pFkCY_0024M5zt8zLsQk(global::_003CModule_003E.smethod_25<string>(3577809994u), new Vector2(370f, 0f), global::_003CModule_003E.smethod_28<string>(3578426342u), iTtUTzVJqoLove2Mlyfm4HQ.transform, resetGroup: true, delegate
					{
						BFMkrNuUjUFB4B6P9DkaJ_s = vpxRY7V6xxVwrtWzUazGbAijYYgy_j9aOURndFg_MPmV1XpOIQGYmugcr08FohKkWZtGTI6r2M_fFifxmanSRrgmSgq6IJSK9HbxY_0024RtywzhAZtQnNleJNvuto3IE49fVw.scale;
					}, null, toggleGroup);
					control0_0.DmPZGWxJ26_0024f_0024QOvQiqpmW8(new Vector2(0f, 30f));
					control0_0.gameObject.smethod_0(global::_003CModule_003E.smethod_26<string>(769079470u)).transform.localPosition = new Vector3(0f, (0f - ((RectTransform)VEnLkFeX6Tle_M_0024bJO7QAfw.transform).sizeDelta.y) / 4f);
					UziXq1_0024N3sVQ1ybcZOKLrrk = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.uEsWMK_pFkCY_0024M5zt8zLsQk(global::_003CModule_003E.smethod_28<string>(2059822681u), new Vector2(455f, 0f), global::_003CModule_003E.smethod_26<string>(896299457u), iTtUTzVJqoLove2Mlyfm4HQ.transform, resetGroup: true, delegate
					{
						BFMkrNuUjUFB4B6P9DkaJ_s = vpxRY7V6xxVwrtWzUazGbAijYYgy_j9aOURndFg_MPmV1XpOIQGYmugcr08FohKkWZtGTI6r2M_fFifxmanSRrgmSgq6IJSK9HbxY_0024RtywzhAZtQnNleJNvuto3IE49fVw.paint;
					}, null, toggleGroup);
					UziXq1_0024N3sVQ1ybcZOKLrrk.DmPZGWxJ26_0024f_0024QOvQiqpmW8(new Vector2(0f, 30f));
					UziXq1_0024N3sVQ1ybcZOKLrrk.gameObject.smethod_0(global::_003CModule_003E.smethod_25<string>(2862828841u)).transform.localPosition = new Vector3(0f, (0f - ((RectTransform)VEnLkFeX6Tle_M_0024bJO7QAfw.transform).sizeDelta.y) / 4f);
					VEnLkFeX6Tle_M_0024bJO7QAfw.Tz4h_68oANQj5xAU0vtoknA.isOn = true;
					yHjr9GR2wCKklzOW2Xkl_Hi1ImsI52cDM857W_TH9pHz = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.qSsYOsEQb7x452a9Y45dVEk(Vector3.zero, new Vector2(420f, 660f), GameObject.Find(global::_003CModule_003E.smethod_29<string>(618767629u)).transform);
					yHjr9GR2wCKklzOW2Xkl_Hi1ImsI52cDM857W_TH9pHz.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 60f);
					Q6uRAB_0024_S89ANxgzZ0QH8mk = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.bqYYMQnP2SDqYH85wmN_0024evI(global::_003CModule_003E.smethod_29<string>(2025401986u), new Vector2(-100f, 600f), global::_003CModule_003E.smethod_25<string>(2624700421u), global::_003CModule_003E.smethod_27<string>(2542726339u), yHjr9GR2wCKklzOW2Xkl_Hi1ImsI52cDM857W_TH9pHz.transform);
					Q6uRAB_0024_S89ANxgzZ0QH8mk.BSdnl9DYm6Rd4cVhJ555c_A.contentType = InputField.ContentType.DecimalNumber;
					Q6uRAB_0024_S89ANxgzZ0QH8mk.BSdnl9DYm6Rd4cVhJ555c_A.characterLimit = 50;
					Q6uRAB_0024_S89ANxgzZ0QH8mk.JNMaMdWdD3fzh8iVBUwSGz4 = delegate(string text)
					{
						if (CJMhv67dSW_yQIGoFtPlWcbgax_OQ5_0024Fp37aG7YfGb4sgG2anRt83J5mUK3_0024zPVqEeh_0024o4C68bhHfF2mrd4hR_rnT8aeby_0024sgZI9VBYschbMognZXTsqSk8yWwRsn2mY7A.smethod_3((UnityEngine.Object)CJMhv67dSW_yQIGoFtPlWcbgax_OQ5_0024Fp37aG7YfGb4sgG2anRt83J5mUK3_0024zPVqEeh_0024o4C68bhHfF2mrd4hR_rnT8aeby_0024sgZI9VBYschbMognZXTsqSk8yWwRsn2mY7A.smethod_2(CJMhv67dSW_yQIGoFtPlWcbgax_OQ5_0024Fp37aG7YfGb4sgG2anRt83J5mUK3_0024zPVqEeh_0024o4C68bhHfF2mrd4hR_rnT8aeby_0024sgZI9VBYschbMognZXTsqSk8yWwRsn2mY7A.smethod_1()), (UnityEngine.Object)null))
						{
							bm4qNpKN8ThlleNq_0024v_0024DpHZBJQSkCOXr8EFjC57_0024ief7(new Vector3(float.Parse(text), CJMhv67dSW_yQIGoFtPlWcbgax_OQ5_0024Fp37aG7YfGb4sgG2anRt83J5mUK3_0024zPVqEeh_0024o4C68bhHfF2mrd4hR_rnT8aeby_0024sgZI9VBYschbMognZXTsqSk8yWwRsn2mY7A.smethod_5(CJMhv67dSW_yQIGoFtPlWcbgax_OQ5_0024Fp37aG7YfGb4sgG2anRt83J5mUK3_0024zPVqEeh_0024o4C68bhHfF2mrd4hR_rnT8aeby_0024sgZI9VBYschbMognZXTsqSk8yWwRsn2mY7A.smethod_4(ra5MAEm12eKHvW9LN8brPaE)).y, CJMhv67dSW_yQIGoFtPlWcbgax_OQ5_0024Fp37aG7YfGb4sgG2anRt83J5mUK3_0024zPVqEeh_0024o4C68bhHfF2mrd4hR_rnT8aeby_0024sgZI9VBYschbMognZXTsqSk8yWwRsn2mY7A.smethod_5(CJMhv67dSW_yQIGoFtPlWcbgax_OQ5_0024Fp37aG7YfGb4sgG2anRt83J5mUK3_0024zPVqEeh_0024o4C68bhHfF2mrd4hR_rnT8aeby_0024sgZI9VBYschbMognZXTsqSk8yWwRsn2mY7A.smethod_4(ra5MAEm12eKHvW9LN8brPaE)).z));
						}
					};
					Xrd9z2_WYChPpqRSvNFZWiw = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.bqYYMQnP2SDqYH85wmN_0024evI(global::_003CModule_003E.smethod_29<string>(1884745297u), new Vector2(-100f, 560f), global::_003CModule_003E.smethod_26<string>(2594192902u), global::_003CModule_003E.smethod_25<string>(3589200737u), yHjr9GR2wCKklzOW2Xkl_Hi1ImsI52cDM857W_TH9pHz.transform);
					Xrd9z2_WYChPpqRSvNFZWiw.BSdnl9DYm6Rd4cVhJ555c_A.contentType = InputField.ContentType.DecimalNumber;
					Xrd9z2_WYChPpqRSvNFZWiw.BSdnl9DYm6Rd4cVhJ555c_A.characterLimit = 50;
					Xrd9z2_WYChPpqRSvNFZWiw.JNMaMdWdD3fzh8iVBUwSGz4 = delegate(string text)
					{
						if (CJMhv67dSW_yQIGoFtPlWcbgax_OQ5_0024Fp37aG7YfGb4sgG2anRt83J5mUK3_0024zPVqEeh_0024o4C68bhHfF2mrd4hR_rnT8aeby_0024sgZI9VBYschbMognZXTsqSk8yWwRsn2mY7A.smethod_3((UnityEngine.Object)CJMhv67dSW_yQIGoFtPlWcbgax_OQ5_0024Fp37aG7YfGb4sgG2anRt83J5mUK3_0024zPVqEeh_0024o4C68bhHfF2mrd4hR_rnT8aeby_0024sgZI9VBYschbMognZXTsqSk8yWwRsn2mY7A.smethod_2(CJMhv67dSW_yQIGoFtPlWcbgax_OQ5_0024Fp37aG7YfGb4sgG2anRt83J5mUK3_0024zPVqEeh_0024o4C68bhHfF2mrd4hR_rnT8aeby_0024sgZI9VBYschbMognZXTsqSk8yWwRsn2mY7A.smethod_1()), (UnityEngine.Object)null))
						{
							bm4qNpKN8ThlleNq_0024v_0024DpHZBJQSkCOXr8EFjC57_0024ief7(new Vector3(CJMhv67dSW_yQIGoFtPlWcbgax_OQ5_0024Fp37aG7YfGb4sgG2anRt83J5mUK3_0024zPVqEeh_0024o4C68bhHfF2mrd4hR_rnT8aeby_0024sgZI9VBYschbMognZXTsqSk8yWwRsn2mY7A.smethod_5(CJMhv67dSW_yQIGoFtPlWcbgax_OQ5_0024Fp37aG7YfGb4sgG2anRt83J5mUK3_0024zPVqEeh_0024o4C68bhHfF2mrd4hR_rnT8aeby_0024sgZI9VBYschbMognZXTsqSk8yWwRsn2mY7A.smethod_4(ra5MAEm12eKHvW9LN8brPaE)).x, float.Parse(text), CJMhv67dSW_yQIGoFtPlWcbgax_OQ5_0024Fp37aG7YfGb4sgG2anRt83J5mUK3_0024zPVqEeh_0024o4C68bhHfF2mrd4hR_rnT8aeby_0024sgZI9VBYschbMognZXTsqSk8yWwRsn2mY7A.smethod_5(CJMhv67dSW_yQIGoFtPlWcbgax_OQ5_0024Fp37aG7YfGb4sgG2anRt83J5mUK3_0024zPVqEeh_0024o4C68bhHfF2mrd4hR_rnT8aeby_0024sgZI9VBYschbMognZXTsqSk8yWwRsn2mY7A.smethod_4(ra5MAEm12eKHvW9LN8brPaE)).z));
						}
					};
					eZaggCkfDTJE2dhuLSEvCSs = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.bqYYMQnP2SDqYH85wmN_0024evI(global::_003CModule_003E.smethod_26<string>(2702183900u), new Vector2(-100f, 520f), global::_003CModule_003E.smethod_29<string>(3278983350u), global::_003CModule_003E.smethod_26<string>(3960589722u), yHjr9GR2wCKklzOW2Xkl_Hi1ImsI52cDM857W_TH9pHz.transform);
					eZaggCkfDTJE2dhuLSEvCSs.BSdnl9DYm6Rd4cVhJ555c_A.contentType = InputField.ContentType.DecimalNumber;
					eZaggCkfDTJE2dhuLSEvCSs.BSdnl9DYm6Rd4cVhJ555c_A.characterLimit = 50;
					eZaggCkfDTJE2dhuLSEvCSs.JNMaMdWdD3fzh8iVBUwSGz4 = delegate(string text)
					{
						if (CJMhv67dSW_yQIGoFtPlWcbgax_OQ5_0024Fp37aG7YfGb4sgG2anRt83J5mUK3_0024zPVqEeh_0024o4C68bhHfF2mrd4hR_rnT8aeby_0024sgZI9VBYschbMognZXTsqSk8yWwRsn2mY7A.smethod_3((UnityEngine.Object)CJMhv67dSW_yQIGoFtPlWcbgax_OQ5_0024Fp37aG7YfGb4sgG2anRt83J5mUK3_0024zPVqEeh_0024o4C68bhHfF2mrd4hR_rnT8aeby_0024sgZI9VBYschbMognZXTsqSk8yWwRsn2mY7A.smethod_2(CJMhv67dSW_yQIGoFtPlWcbgax_OQ5_0024Fp37aG7YfGb4sgG2anRt83J5mUK3_0024zPVqEeh_0024o4C68bhHfF2mrd4hR_rnT8aeby_0024sgZI9VBYschbMognZXTsqSk8yWwRsn2mY7A.smethod_1()), (UnityEngine.Object)null))
						{
							bm4qNpKN8ThlleNq_0024v_0024DpHZBJQSkCOXr8EFjC57_0024ief7(new Vector3(CJMhv67dSW_yQIGoFtPlWcbgax_OQ5_0024Fp37aG7YfGb4sgG2anRt83J5mUK3_0024zPVqEeh_0024o4C68bhHfF2mrd4hR_rnT8aeby_0024sgZI9VBYschbMognZXTsqSk8yWwRsn2mY7A.smethod_5(CJMhv67dSW_yQIGoFtPlWcbgax_OQ5_0024Fp37aG7YfGb4sgG2anRt83J5mUK3_0024zPVqEeh_0024o4C68bhHfF2mrd4hR_rnT8aeby_0024sgZI9VBYschbMognZXTsqSk8yWwRsn2mY7A.smethod_4(ra5MAEm12eKHvW9LN8brPaE)).x, CJMhv67dSW_yQIGoFtPlWcbgax_OQ5_0024Fp37aG7YfGb4sgG2anRt83J5mUK3_0024zPVqEeh_0024o4C68bhHfF2mrd4hR_rnT8aeby_0024sgZI9VBYschbMognZXTsqSk8yWwRsn2mY7A.smethod_5(CJMhv67dSW_yQIGoFtPlWcbgax_OQ5_0024Fp37aG7YfGb4sgG2anRt83J5mUK3_0024zPVqEeh_0024o4C68bhHfF2mrd4hR_rnT8aeby_0024sgZI9VBYschbMognZXTsqSk8yWwRsn2mY7A.smethod_4(ra5MAEm12eKHvW9LN8brPaE)).y, float.Parse(text)));
						}
					};
					GameObject gameObject6 = UnityEngine.Object.Instantiate(GameObject.Find(global::_003CModule_003E.smethod_28<string>(3214927773u)).smethod_0(global::_003CModule_003E.smethod_27<string>(2436365218u)), yHjr9GR2wCKklzOW2Xkl_Hi1ImsI52cDM857W_TH9pHz.transform);
					gameObject6.transform.localPosition = new Vector2(-100f, 635f);
					gameObject6.GetComponent<Text>().text = global::_003CModule_003E.smethod_28<string>(558297660u);
					gameObject6.GetComponent<Text>().alignment = TextAnchor.LowerCenter;
					jq_0024gxTE9SdmPwMA9OoDUaD4 = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.tjX92nnz9Ioc_0024izMOOLq4fI(global::_003CModule_003E.smethod_29<string>(2349900027u), global::_003CModule_003E.smethod_26<string>(3783206399u), new Vector3(-100f, 460f), -90, 90, 0, null, yHjr9GR2wCKklzOW2Xkl_Hi1ImsI52cDM857W_TH9pHz.transform);
					H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.H1Pr8mmPslEwoIU22reQcXE(jq_0024gxTE9SdmPwMA9OoDUaD4).onValueChanged.AddListener(delegate(float num4)
					{
						if (CJMhv67dSW_yQIGoFtPlWcbgax_OQ5_0024Fp37aG7YfGb4sgG2anRt83J5mUK3_0024zPVqEeh_0024o4C68bhHfF2mrd4hR_rnT8aeby_0024sgZI9VBYschbMognZXTsqSk8yWwRsn2mY7A.smethod_6(CJMhv67dSW_yQIGoFtPlWcbgax_OQ5_0024Fp37aG7YfGb4sgG2anRt83J5mUK3_0024zPVqEeh_0024o4C68bhHfF2mrd4hR_rnT8aeby_0024sgZI9VBYschbMognZXTsqSk8yWwRsn2mY7A.smethod_1()))
						{
							if (num4 >= 90f)
							{
								num4 = 89f;
							}
							else if (num4 <= -90f)
							{
								num4 = -89f;
							}
							Vector3 eulerAngles = CJMhv67dSW_yQIGoFtPlWcbgax_OQ5_0024Fp37aG7YfGb4sgG2anRt83J5mUK3_0024zPVqEeh_0024o4C68bhHfF2mrd4hR_rnT8aeby_0024sgZI9VBYschbMognZXTsqSk8yWwRsn2mY7A.smethod_7(fbSm64A_0024FzBvAjtith640zQ[0].TIWjI8FsBk2nlZk9NO4HNOE).eulerAngles;
							fbSm64A_0024FzBvAjtith640zQ[0].TIWjI8FsBk2nlZk9NO4HNOE.rotation = Quaternion.Euler(num4, eulerAngles.y, eulerAngles.z);
						}
					});
					qSG8Li7AV2HL0xo0xYL2dhs = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.tjX92nnz9Ioc_0024izMOOLq4fI(global::_003CModule_003E.smethod_27<string>(1710452871u), "", new Vector3(-100f, 430f), 0, 359, 0, null, yHjr9GR2wCKklzOW2Xkl_Hi1ImsI52cDM857W_TH9pHz.transform);
					H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.H1Pr8mmPslEwoIU22reQcXE(qSG8Li7AV2HL0xo0xYL2dhs).onValueChanged.AddListener(delegate(float y)
					{
						if (CJMhv67dSW_yQIGoFtPlWcbgax_OQ5_0024Fp37aG7YfGb4sgG2anRt83J5mUK3_0024zPVqEeh_0024o4C68bhHfF2mrd4hR_rnT8aeby_0024sgZI9VBYschbMognZXTsqSk8yWwRsn2mY7A.smethod_6(CJMhv67dSW_yQIGoFtPlWcbgax_OQ5_0024Fp37aG7YfGb4sgG2anRt83J5mUK3_0024zPVqEeh_0024o4C68bhHfF2mrd4hR_rnT8aeby_0024sgZI9VBYschbMognZXTsqSk8yWwRsn2mY7A.smethod_1()))
						{
							Vector3 eulerAngles = CJMhv67dSW_yQIGoFtPlWcbgax_OQ5_0024Fp37aG7YfGb4sgG2anRt83J5mUK3_0024zPVqEeh_0024o4C68bhHfF2mrd4hR_rnT8aeby_0024sgZI9VBYschbMognZXTsqSk8yWwRsn2mY7A.smethod_7(fbSm64A_0024FzBvAjtith640zQ[0].TIWjI8FsBk2nlZk9NO4HNOE).eulerAngles;
							fbSm64A_0024FzBvAjtith640zQ[0].TIWjI8FsBk2nlZk9NO4HNOE.rotation = Quaternion.Euler(eulerAngles.x, y, eulerAngles.z);
						}
					});
					xPIozX5u7HFDqrFoaQvRcBc = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.tjX92nnz9Ioc_0024izMOOLq4fI(global::_003CModule_003E.smethod_29<string>(434993744u), "", new Vector3(-100f, 400f), 0, 359, 0, null, yHjr9GR2wCKklzOW2Xkl_Hi1ImsI52cDM857W_TH9pHz.transform);
					H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.H1Pr8mmPslEwoIU22reQcXE(xPIozX5u7HFDqrFoaQvRcBc).onValueChanged.AddListener(delegate(float z)
					{
						if (CJMhv67dSW_yQIGoFtPlWcbgax_OQ5_0024Fp37aG7YfGb4sgG2anRt83J5mUK3_0024zPVqEeh_0024o4C68bhHfF2mrd4hR_rnT8aeby_0024sgZI9VBYschbMognZXTsqSk8yWwRsn2mY7A.smethod_6(CJMhv67dSW_yQIGoFtPlWcbgax_OQ5_0024Fp37aG7YfGb4sgG2anRt83J5mUK3_0024zPVqEeh_0024o4C68bhHfF2mrd4hR_rnT8aeby_0024sgZI9VBYschbMognZXTsqSk8yWwRsn2mY7A.smethod_1()))
						{
							Vector3 eulerAngles = CJMhv67dSW_yQIGoFtPlWcbgax_OQ5_0024Fp37aG7YfGb4sgG2anRt83J5mUK3_0024zPVqEeh_0024o4C68bhHfF2mrd4hR_rnT8aeby_0024sgZI9VBYschbMognZXTsqSk8yWwRsn2mY7A.smethod_7(fbSm64A_0024FzBvAjtith640zQ[0].TIWjI8FsBk2nlZk9NO4HNOE).eulerAngles;
							fbSm64A_0024FzBvAjtith640zQ[0].TIWjI8FsBk2nlZk9NO4HNOE.rotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y, z);
						}
					});
					oZphDbXncaFSC1_0024Uxg8QFMo = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.tjX92nnz9Ioc_0024izMOOLq4fI(global::_003CModule_003E.smethod_28<string>(2015256076u), global::_003CModule_003E.smethod_25<string>(4195912530u), new Vector3(-100f, 350f), 1, 255, 1, null, yHjr9GR2wCKklzOW2Xkl_Hi1ImsI52cDM857W_TH9pHz.transform);
					H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.H1Pr8mmPslEwoIU22reQcXE(oZphDbXncaFSC1_0024Uxg8QFMo).onValueChanged.AddListener(delegate(float x)
					{
						if (CJMhv67dSW_yQIGoFtPlWcbgax_OQ5_0024Fp37aG7YfGb4sgG2anRt83J5mUK3_0024zPVqEeh_0024o4C68bhHfF2mrd4hR_rnT8aeby_0024sgZI9VBYschbMognZXTsqSk8yWwRsn2mY7A.smethod_6(CJMhv67dSW_yQIGoFtPlWcbgax_OQ5_0024Fp37aG7YfGb4sgG2anRt83J5mUK3_0024zPVqEeh_0024o4C68bhHfF2mrd4hR_rnT8aeby_0024sgZI9VBYschbMognZXTsqSk8yWwRsn2mY7A.smethod_1()))
						{
							if (fbSm64A_0024FzBvAjtith640zQ[0].MrZDDetpveRMT__0024biC7h8tU != xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA.Enum0.gate)
							{
								Vector3 vector2 = CJMhv67dSW_yQIGoFtPlWcbgax_OQ5_0024Fp37aG7YfGb4sgG2anRt83J5mUK3_0024zPVqEeh_0024o4C68bhHfF2mrd4hR_rnT8aeby_0024sgZI9VBYschbMognZXTsqSk8yWwRsn2mY7A.smethod_8(fbSm64A_0024FzBvAjtith640zQ[0].TIWjI8FsBk2nlZk9NO4HNOE);
								fbSm64A_0024FzBvAjtith640zQ[0].TIWjI8FsBk2nlZk9NO4HNOE.localScale = new Vector3(x, vector2.y, vector2.z);
								fbSm64A_0024FzBvAjtith640zQ[0].method_0();
							}
							else
							{
								Vector3 localScale = fbSm64A_0024FzBvAjtith640zQ[0].cRC1fFhZVgamcERb3o0WavI.transform.localScale;
								fbSm64A_0024FzBvAjtith640zQ[0].cRC1fFhZVgamcERb3o0WavI.transform.localScale = new Vector3(x, localScale.y, localScale.z);
							}
						}
					});
					vKv0P28kAhNLiR7WXKJDGO4 = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.tjX92nnz9Ioc_0024izMOOLq4fI(global::_003CModule_003E.smethod_27<string>(458726988u), "", new Vector3(-100f, 320f), 1, 255, 1, null, yHjr9GR2wCKklzOW2Xkl_Hi1ImsI52cDM857W_TH9pHz.transform);
					H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.H1Pr8mmPslEwoIU22reQcXE(vKv0P28kAhNLiR7WXKJDGO4).onValueChanged.AddListener(delegate(float y)
					{
						if (CJMhv67dSW_yQIGoFtPlWcbgax_OQ5_0024Fp37aG7YfGb4sgG2anRt83J5mUK3_0024zPVqEeh_0024o4C68bhHfF2mrd4hR_rnT8aeby_0024sgZI9VBYschbMognZXTsqSk8yWwRsn2mY7A.smethod_6(CJMhv67dSW_yQIGoFtPlWcbgax_OQ5_0024Fp37aG7YfGb4sgG2anRt83J5mUK3_0024zPVqEeh_0024o4C68bhHfF2mrd4hR_rnT8aeby_0024sgZI9VBYschbMognZXTsqSk8yWwRsn2mY7A.smethod_1()))
						{
							if (fbSm64A_0024FzBvAjtith640zQ[0].MrZDDetpveRMT__0024biC7h8tU != xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA.Enum0.gate)
							{
								Vector3 vector2 = CJMhv67dSW_yQIGoFtPlWcbgax_OQ5_0024Fp37aG7YfGb4sgG2anRt83J5mUK3_0024zPVqEeh_0024o4C68bhHfF2mrd4hR_rnT8aeby_0024sgZI9VBYschbMognZXTsqSk8yWwRsn2mY7A.smethod_8(fbSm64A_0024FzBvAjtith640zQ[0].TIWjI8FsBk2nlZk9NO4HNOE);
								fbSm64A_0024FzBvAjtith640zQ[0].TIWjI8FsBk2nlZk9NO4HNOE.localScale = new Vector3(vector2.x, y, vector2.z);
								fbSm64A_0024FzBvAjtith640zQ[0].method_0();
							}
							else
							{
								Vector3 localScale = fbSm64A_0024FzBvAjtith640zQ[0].cRC1fFhZVgamcERb3o0WavI.transform.localScale;
								fbSm64A_0024FzBvAjtith640zQ[0].cRC1fFhZVgamcERb3o0WavI.transform.localScale = new Vector3(localScale.x, y, localScale.z);
							}
						}
					});
					sliderController_0 = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.tjX92nnz9Ioc_0024izMOOLq4fI(global::_003CModule_003E.smethod_27<string>(72505477u), "", new Vector3(-100f, 290f), 1, 255, 1, null, yHjr9GR2wCKklzOW2Xkl_Hi1ImsI52cDM857W_TH9pHz.transform);
					H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.H1Pr8mmPslEwoIU22reQcXE(sliderController_0).onValueChanged.AddListener(delegate(float z)
					{
						if (CJMhv67dSW_yQIGoFtPlWcbgax_OQ5_0024Fp37aG7YfGb4sgG2anRt83J5mUK3_0024zPVqEeh_0024o4C68bhHfF2mrd4hR_rnT8aeby_0024sgZI9VBYschbMognZXTsqSk8yWwRsn2mY7A.smethod_6(CJMhv67dSW_yQIGoFtPlWcbgax_OQ5_0024Fp37aG7YfGb4sgG2anRt83J5mUK3_0024zPVqEeh_0024o4C68bhHfF2mrd4hR_rnT8aeby_0024sgZI9VBYschbMognZXTsqSk8yWwRsn2mY7A.smethod_1()))
						{
							Vector3 vector2 = CJMhv67dSW_yQIGoFtPlWcbgax_OQ5_0024Fp37aG7YfGb4sgG2anRt83J5mUK3_0024zPVqEeh_0024o4C68bhHfF2mrd4hR_rnT8aeby_0024sgZI9VBYschbMognZXTsqSk8yWwRsn2mY7A.smethod_8(fbSm64A_0024FzBvAjtith640zQ[0].TIWjI8FsBk2nlZk9NO4HNOE);
							fbSm64A_0024FzBvAjtith640zQ[0].TIWjI8FsBk2nlZk9NO4HNOE.localScale = new Vector3(vector2.x, vector2.y, z);
							fbSm64A_0024FzBvAjtith640zQ[0].method_0();
						}
					});
					yHjr9GR2wCKklzOW2Xkl_Hi1ImsI52cDM857W_TH9pHz.SetActive(value: false);
					RPPJ2nsw9RU2otM3BzDPzcY = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.qSsYOsEQb7x452a9Y45dVEk(Vector3.zero, new Vector2(420f, 660f), GameObject.Find(global::_003CModule_003E.smethod_29<string>(618767629u)).transform);
					RPPJ2nsw9RU2otM3BzDPzcY.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 60f);
					GameObject gameObject7 = new GameObject(global::_003CModule_003E.smethod_26<string>(979626525u));
					gameObject7.transform.parent = RPPJ2nsw9RU2otM3BzDPzcY.transform;
					RectTransform rectTransform2 = gameObject7.AddComponent<RectTransform>();
					rectTransform2.anchorMin = new Vector2(0f, 1f);
					rectTransform2.anchorMax = new Vector2(0f, 1f);
					rectTransform2.sizeDelta = new Vector2(50f, 25f);
					Image Xz1wKOB0iImarofJ3M1e0ew = gameObject7.AddComponent<Image>();
					Xz1wKOB0iImarofJ3M1e0ew.sprite = Sprite.Create(Texture2D.whiteTexture, new Rect(0f, 0f, 1f, 1f), new Vector2(0f, 0f));
					Xz1wKOB0iImarofJ3M1e0ew.color = Color.red;
					rectTransform2.anchoredPosition = new Vector2(110f, -50f);
					GameObject gameObject8 = new GameObject(global::_003CModule_003E.smethod_29<string>(868066511u));
					gameObject8.transform.parent = RPPJ2nsw9RU2otM3BzDPzcY.transform;
					RectTransform rectTransform3 = gameObject8.AddComponent<RectTransform>();
					rectTransform3.anchorMin = new Vector2(0f, 1f);
					rectTransform3.anchorMax = new Vector2(0f, 1f);
					rectTransform3.sizeDelta = new Vector2(150f, 150f);
					RawImage YvRrQLXRVCG4kwr5EKSrKUA = gameObject8.AddComponent<RawImage>();
					YvRrQLXRVCG4kwr5EKSrKUA.texture = new Texture2D(1, 1);
					((Texture2D)YvRrQLXRVCG4kwr5EKSrKUA.texture).LoadImage(KH64M3TewhA_Yw2TjbFeRV_kcghAaUSHXuDlWZpBfh_lhm_0024Y_0024No6GYBMPMIyT_0024M8ZQ.mbfMVOg85cq_Wv_TSAuw7_U);
					rectTransform3.anchoredPosition = new Vector2(110f, -190f);
					gameObject8.AddComponent<AmMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F>().U_0024eIoX6e3N9Ag_us_EcGHBI(delegate
					{
						if (oGcOeB_JND2Yf4MrR30ymvpG_yhHQAKmTc9io3pzJtpnEZby5iOT3rhoy3VFnd_757iISUfMa5mF7FEj10c7V2oBfq_0024Tv3CMsaMghn7I98pCGadZphq8UvJZ_0024_QjFZSW11y_Iqlc_0024NOKJQYyGUK30ME.smethod_0(0))
						{
							Vector2 vector2_ = default(Vector2);
							oGcOeB_JND2Yf4MrR30ymvpG_yhHQAKmTc9io3pzJtpnEZby5iOT3rhoy3VFnd_757iISUfMa5mF7FEj10c7V2oBfq_0024Tv3CMsaMghn7I98pCGadZphq8UvJZ_0024_QjFZSW11y_Iqlc_0024NOKJQYyGUK30ME.smethod_4(rectTransform3, (Vector2)oGcOeB_JND2Yf4MrR30ymvpG_yhHQAKmTc9io3pzJtpnEZby5iOT3rhoy3VFnd_757iISUfMa5mF7FEj10c7V2oBfq_0024Tv3CMsaMghn7I98pCGadZphq8UvJZ_0024_QjFZSW11y_Iqlc_0024NOKJQYyGUK30ME.smethod_1(), oGcOeB_JND2Yf4MrR30ymvpG_yhHQAKmTc9io3pzJtpnEZby5iOT3rhoy3VFnd_757iISUfMa5mF7FEj10c7V2oBfq_0024Tv3CMsaMghn7I98pCGadZphq8UvJZ_0024_QjFZSW11y_Iqlc_0024NOKJQYyGUK30ME.smethod_3(oGcOeB_JND2Yf4MrR30ymvpG_yhHQAKmTc9io3pzJtpnEZby5iOT3rhoy3VFnd_757iISUfMa5mF7FEj10c7V2oBfq_0024Tv3CMsaMghn7I98pCGadZphq8UvJZ_0024_QjFZSW11y_Iqlc_0024NOKJQYyGUK30ME.smethod_2(global::_003CModule_003E.smethod_29<string>(618767629u)).GetComponent<Canvas>()), ref vector2_);
							if (oGcOeB_JND2Yf4MrR30ymvpG_yhHQAKmTc9io3pzJtpnEZby5iOT3rhoy3VFnd_757iISUfMa5mF7FEj10c7V2oBfq_0024Tv3CMsaMghn7I98pCGadZphq8UvJZ_0024_QjFZSW11y_Iqlc_0024NOKJQYyGUK30ME.smethod_5(rectTransform3).Contains(vector2_))
							{
								vector2_ += new Vector2(rectTransform3.sizeDelta.x * rectTransform3.pivot.x, rectTransform3.sizeDelta.y * rectTransform3.pivot.y);
								float a = IdwU0_ARpD_wnWGSdVq151k.a;
								IdwU0_ARpD_wnWGSdVq151k = ((Texture2D)YvRrQLXRVCG4kwr5EKSrKUA.texture).GetPixel((int)(vector2_.x / rectTransform3.sizeDelta.x * (float)YvRrQLXRVCG4kwr5EKSrKUA.texture.width), (int)(vector2_.y / rectTransform3.sizeDelta.y * (float)YvRrQLXRVCG4kwr5EKSrKUA.texture.height));
								IdwU0_ARpD_wnWGSdVq151k.a = a;
								KEFHJCGICLE.HNAHBIMJDCB(global::_003CModule_003E.smethod_25<string>(2341604922u));
							}
						}
					});
					Control0 r7bkh1RM_YDzdrhuv21BFaY = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.uEsWMK_pFkCY_0024M5zt8zLsQk(global::_003CModule_003E.smethod_28<string>(4246149547u), new Vector3(-150f, 190f), global::_003CModule_003E.smethod_26<string>(1460777682u), RPPJ2nsw9RU2otM3BzDPzcY.transform, resetGroup: true, delegate
					{
						IdwU0_ARpD_wnWGSdVq151k.a = 1f;
					});
					Control0 control = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.uEsWMK_pFkCY_0024M5zt8zLsQk(global::_003CModule_003E.smethod_29<string>(586753133u), new Vector3(-50f, 190f), global::_003CModule_003E.smethod_28<string>(2136619875u), RPPJ2nsw9RU2otM3BzDPzcY.transform, resetGroup: true, delegate
					{
						IdwU0_ARpD_wnWGSdVq151k.a = 0f;
					});
					Control0 a33W7qtUsLkdEvrMqRh76cI = null;
					Control0 sJHJHLbGY5pCahRjwxFWV8Y = null;
					Control0 _8oassVGhyJhzvMve_RCFEc = null;
					Control0 FrhaIjlOZnCeLu6RAcU9oTU = null;
					Control0 EG2RuiZozaDMQatkBF5_CPc = null;
					if (MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.moreStageboxMats)
					{
						EG2RuiZozaDMQatkBF5_CPc = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.uEsWMK_pFkCY_0024M5zt8zLsQk(global::_003CModule_003E.smethod_25<string>(1558241110u), new Vector3(-100f, 50f), global::_003CModule_003E.smethod_27<string>(3688091101u), RPPJ2nsw9RU2otM3BzDPzcY.transform, resetGroup: true, delegate(bool toggled)
						{
							if (a33W7qtUsLkdEvrMqRh76cI.hLxnG9Hq33zU_YUsu_00240_zak)
							{
								a33W7qtUsLkdEvrMqRh76cI.kBPtltqxQQyZ5ym0_0024wsbeUc();
							}
							if (sJHJHLbGY5pCahRjwxFWV8Y.hLxnG9Hq33zU_YUsu_00240_zak)
							{
								sJHJHLbGY5pCahRjwxFWV8Y.kBPtltqxQQyZ5ym0_0024wsbeUc();
							}
							if (_8oassVGhyJhzvMve_RCFEc.hLxnG9Hq33zU_YUsu_00240_zak)
							{
								_8oassVGhyJhzvMve_RCFEc.kBPtltqxQQyZ5ym0_0024wsbeUc();
							}
							if (FrhaIjlOZnCeLu6RAcU9oTU.hLxnG9Hq33zU_YUsu_00240_zak)
							{
								FrhaIjlOZnCeLu6RAcU9oTU.kBPtltqxQQyZ5ym0_0024wsbeUc();
							}
							if ((r7bkh1RM_YDzdrhuv21BFaY.hLxnG9Hq33zU_YUsu_00240_zak || control.hLxnG9Hq33zU_YUsu_00240_zak) && toggled)
							{
								EG2RuiZozaDMQatkBF5_CPc.hLxnG9Hq33zU_YUsu_00240_zak = false;
							}
						});
						a33W7qtUsLkdEvrMqRh76cI = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.uEsWMK_pFkCY_0024M5zt8zLsQk(global::_003CModule_003E.smethod_27<string>(1756983546u), new Vector3(-150f, 145f), global::_003CModule_003E.smethod_26<string>(3468709378u), RPPJ2nsw9RU2otM3BzDPzcY.transform, resetGroup: true, delegate
						{
							IdwU0_ARpD_wnWGSdVq151k.a = kILN_0024q_tbcSASqORkmJ7BBsDUjxuSBqI3VGW6VcIauaSSt6_Qk8HVbbHG_0024WDerWUiLyHmclIfPXtnf3BNWVqydk.XMUQ93RVvJVE5FPCbS_00248Hxs - (EG2RuiZozaDMQatkBF5_CPc.hLxnG9Hq33zU_YUsu_00240_zak ? (4f / 51f) : 0f);
						});
						sJHJHLbGY5pCahRjwxFWV8Y = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.uEsWMK_pFkCY_0024M5zt8zLsQk(global::_003CModule_003E.smethod_25<string>(593740794u), new Vector3(-50f, 145f), global::_003CModule_003E.smethod_26<string>(225641881u), RPPJ2nsw9RU2otM3BzDPzcY.transform, resetGroup: true, delegate
						{
							IdwU0_ARpD_wnWGSdVq151k.a = kILN_0024q_tbcSASqORkmJ7BBsDUjxuSBqI3VGW6VcIauaSSt6_Qk8HVbbHG_0024WDerWUiLyHmclIfPXtnf3BNWVqydk.C5AcE8qkQZfZumQ__0024d8JU2A - (EG2RuiZozaDMQatkBF5_CPc.hLxnG9Hq33zU_YUsu_00240_zak ? (4f / 51f) : 0f);
						});
						_8oassVGhyJhzvMve_RCFEc = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.uEsWMK_pFkCY_0024M5zt8zLsQk(global::_003CModule_003E.smethod_28<string>(3502518390u), new Vector3(-150f, 100f), global::_003CModule_003E.smethod_25<string>(3751008884u), RPPJ2nsw9RU2otM3BzDPzcY.transform, resetGroup: true, delegate
						{
							IdwU0_ARpD_wnWGSdVq151k.a = kILN_0024q_tbcSASqORkmJ7BBsDUjxuSBqI3VGW6VcIauaSSt6_Qk8HVbbHG_0024WDerWUiLyHmclIfPXtnf3BNWVqydk.creNoS1TDqnabY284sL4AzA - (EG2RuiZozaDMQatkBF5_CPc.hLxnG9Hq33zU_YUsu_00240_zak ? (4f / 51f) : 0f);
						});
						FrhaIjlOZnCeLu6RAcU9oTU = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.uEsWMK_pFkCY_0024M5zt8zLsQk(global::_003CModule_003E.smethod_28<string>(2956158984u), new Vector3(-50f, 100f), global::_003CModule_003E.smethod_28<string>(2682979281u), RPPJ2nsw9RU2otM3BzDPzcY.transform, resetGroup: true, delegate
						{
							IdwU0_ARpD_wnWGSdVq151k.a = kILN_0024q_tbcSASqORkmJ7BBsDUjxuSBqI3VGW6VcIauaSSt6_Qk8HVbbHG_0024WDerWUiLyHmclIfPXtnf3BNWVqydk.dzadJ16eTlgbYulPr_znFoE - (EG2RuiZozaDMQatkBF5_CPc.hLxnG9Hq33zU_YUsu_00240_zak ? (4f / 51f) : 0f);
						});
						a33W7qtUsLkdEvrMqRh76cI.DmPZGWxJ26_0024f_0024QOvQiqpmW8(new Vector2(-10f, 0f));
						sJHJHLbGY5pCahRjwxFWV8Y.DmPZGWxJ26_0024f_0024QOvQiqpmW8(new Vector2(-10f, 0f));
						_8oassVGhyJhzvMve_RCFEc.DmPZGWxJ26_0024f_0024QOvQiqpmW8(new Vector2(-10f, 0f));
						FrhaIjlOZnCeLu6RAcU9oTU.DmPZGWxJ26_0024f_0024QOvQiqpmW8(new Vector2(-10f, 0f));
						a33W7qtUsLkdEvrMqRh76cI.gameObject.smethod_0(global::_003CModule_003E.smethod_28<string>(436489240u)).GetComponent<Text>().fontSize = 19;
						FrhaIjlOZnCeLu6RAcU9oTU.gameObject.smethod_0(global::_003CModule_003E.smethod_27<string>(1947539503u)).GetComponent<Text>().fontSize = 20;
					}
					r7bkh1RM_YDzdrhuv21BFaY.DmPZGWxJ26_0024f_0024QOvQiqpmW8(new Vector2(-10f, 0f));
					control.DmPZGWxJ26_0024f_0024QOvQiqpmW8(new Vector2(-10f, 0f));
					r7bkh1RM_YDzdrhuv21BFaY.hLxnG9Hq33zU_YUsu_00240_zak = true;
					gameObject7.AddComponent<AmMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F>().U_0024eIoX6e3N9Ag_us_EcGHBI(delegate
					{
						Xz1wKOB0iImarofJ3M1e0ew.color = new Color(IdwU0_ARpD_wnWGSdVq151k.r, IdwU0_ARpD_wnWGSdVq151k.g, IdwU0_ARpD_wnWGSdVq151k.b);
						if (!MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.moreStageboxMats)
						{
							r7bkh1RM_YDzdrhuv21BFaY.hLxnG9Hq33zU_YUsu_00240_zak = IdwU0_ARpD_wnWGSdVq151k.a >= 0.9f;
						}
						else
						{
							r7bkh1RM_YDzdrhuv21BFaY.hLxnG9Hq33zU_YUsu_00240_zak = kILN_0024q_tbcSASqORkmJ7BBsDUjxuSBqI3VGW6VcIauaSSt6_Qk8HVbbHG_0024WDerWUiLyHmclIfPXtnf3BNWVqydk.zfXLeeKUOMpnTqAgpbUcX9g(IdwU0_ARpD_wnWGSdVq151k.a, 1f);
							a33W7qtUsLkdEvrMqRh76cI.hLxnG9Hq33zU_YUsu_00240_zak = kILN_0024q_tbcSASqORkmJ7BBsDUjxuSBqI3VGW6VcIauaSSt6_Qk8HVbbHG_0024WDerWUiLyHmclIfPXtnf3BNWVqydk.zfXLeeKUOMpnTqAgpbUcX9g(IdwU0_ARpD_wnWGSdVq151k.a, kILN_0024q_tbcSASqORkmJ7BBsDUjxuSBqI3VGW6VcIauaSSt6_Qk8HVbbHG_0024WDerWUiLyHmclIfPXtnf3BNWVqydk.XMUQ93RVvJVE5FPCbS_00248Hxs);
							sJHJHLbGY5pCahRjwxFWV8Y.hLxnG9Hq33zU_YUsu_00240_zak = kILN_0024q_tbcSASqORkmJ7BBsDUjxuSBqI3VGW6VcIauaSSt6_Qk8HVbbHG_0024WDerWUiLyHmclIfPXtnf3BNWVqydk.zfXLeeKUOMpnTqAgpbUcX9g(IdwU0_ARpD_wnWGSdVq151k.a, kILN_0024q_tbcSASqORkmJ7BBsDUjxuSBqI3VGW6VcIauaSSt6_Qk8HVbbHG_0024WDerWUiLyHmclIfPXtnf3BNWVqydk.C5AcE8qkQZfZumQ__0024d8JU2A);
							_8oassVGhyJhzvMve_RCFEc.hLxnG9Hq33zU_YUsu_00240_zak = kILN_0024q_tbcSASqORkmJ7BBsDUjxuSBqI3VGW6VcIauaSSt6_Qk8HVbbHG_0024WDerWUiLyHmclIfPXtnf3BNWVqydk.zfXLeeKUOMpnTqAgpbUcX9g(IdwU0_ARpD_wnWGSdVq151k.a, kILN_0024q_tbcSASqORkmJ7BBsDUjxuSBqI3VGW6VcIauaSSt6_Qk8HVbbHG_0024WDerWUiLyHmclIfPXtnf3BNWVqydk.creNoS1TDqnabY284sL4AzA);
							FrhaIjlOZnCeLu6RAcU9oTU.hLxnG9Hq33zU_YUsu_00240_zak = kILN_0024q_tbcSASqORkmJ7BBsDUjxuSBqI3VGW6VcIauaSSt6_Qk8HVbbHG_0024WDerWUiLyHmclIfPXtnf3BNWVqydk.zfXLeeKUOMpnTqAgpbUcX9g(IdwU0_ARpD_wnWGSdVq151k.a, kILN_0024q_tbcSASqORkmJ7BBsDUjxuSBqI3VGW6VcIauaSSt6_Qk8HVbbHG_0024WDerWUiLyHmclIfPXtnf3BNWVqydk.dzadJ16eTlgbYulPr_znFoE);
							if (!r7bkh1RM_YDzdrhuv21BFaY.hLxnG9Hq33zU_YUsu_00240_zak && !control.hLxnG9Hq33zU_YUsu_00240_zak)
							{
								EG2RuiZozaDMQatkBF5_CPc.hLxnG9Hq33zU_YUsu_00240_zak = kILN_0024q_tbcSASqORkmJ7BBsDUjxuSBqI3VGW6VcIauaSSt6_Qk8HVbbHG_0024WDerWUiLyHmclIfPXtnf3BNWVqydk.RABTF797cxk849f9Wpnefo0(IdwU0_ARpD_wnWGSdVq151k.a, includeOnlyDummyWater: true);
							}
							else
							{
								EG2RuiZozaDMQatkBF5_CPc.hLxnG9Hq33zU_YUsu_00240_zak = false;
							}
						}
						control.hLxnG9Hq33zU_YUsu_00240_zak = IdwU0_ARpD_wnWGSdVq151k.a == 0f;
					});
					SliderController vEtQdo8p_AEmER5CcHm5SuY = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.tjX92nnz9Ioc_0024izMOOLq4fI(global::_003CModule_003E.smethod_28<string>(2409799578u), global::_003CModule_003E.smethod_26<string>(1781545120u), new Vector3(-100f, 330f), 0, 255, 0, null, RPPJ2nsw9RU2otM3BzDPzcY.transform);
					H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.H1Pr8mmPslEwoIU22reQcXE(vEtQdo8p_AEmER5CcHm5SuY).onValueChanged.AddListener(delegate(float num4)
					{
						IdwU0_ARpD_wnWGSdVq151k.r = num4 / 255f;
					});
					vEtQdo8p_AEmER5CcHm5SuY.gameObject.AddComponent<AmMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F>().U_0024eIoX6e3N9Ag_us_EcGHBI(delegate
					{
						oGcOeB_JND2Yf4MrR30ymvpG_yhHQAKmTc9io3pzJtpnEZby5iOT3rhoy3VFnd_757iISUfMa5mF7FEj10c7V2oBfq_0024Tv3CMsaMghn7I98pCGadZphq8UvJZ_0024_QjFZSW11y_Iqlc_0024NOKJQYyGUK30ME.smethod_6(H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.H1Pr8mmPslEwoIU22reQcXE(vEtQdo8p_AEmER5CcHm5SuY), (float)(int)(IdwU0_ARpD_wnWGSdVq151k.r * 255f));
					});
					SliderController kxEO3iCdS41Pdgor06VGp9k = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.tjX92nnz9Ioc_0024izMOOLq4fI(global::_003CModule_003E.smethod_25<string>(420541904u), global::_003CModule_003E.smethod_27<string>(737911038u), new Vector3(-100f, 280f), 0, 255, 0, null, RPPJ2nsw9RU2otM3BzDPzcY.transform);
					H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.H1Pr8mmPslEwoIU22reQcXE(kxEO3iCdS41Pdgor06VGp9k).onValueChanged.AddListener(delegate(float num4)
					{
						IdwU0_ARpD_wnWGSdVq151k.g = num4 / 255f;
					});
					kxEO3iCdS41Pdgor06VGp9k.gameObject.AddComponent<AmMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F>().U_0024eIoX6e3N9Ag_us_EcGHBI(delegate
					{
						oGcOeB_JND2Yf4MrR30ymvpG_yhHQAKmTc9io3pzJtpnEZby5iOT3rhoy3VFnd_757iISUfMa5mF7FEj10c7V2oBfq_0024Tv3CMsaMghn7I98pCGadZphq8UvJZ_0024_QjFZSW11y_Iqlc_0024NOKJQYyGUK30ME.smethod_6(H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.H1Pr8mmPslEwoIU22reQcXE(kxEO3iCdS41Pdgor06VGp9k), (float)(int)(IdwU0_ARpD_wnWGSdVq151k.g * 255f));
					});
					SliderController DCWStzN3LM5SCegNzKVghlM = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.tjX92nnz9Ioc_0024izMOOLq4fI(global::_003CModule_003E.smethod_25<string>(2189995732u), global::_003CModule_003E.smethod_26<string>(1834773317u), new Vector3(-100f, 230f), 0, 255, 0, null, RPPJ2nsw9RU2otM3BzDPzcY.transform);
					H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.H1Pr8mmPslEwoIU22reQcXE(DCWStzN3LM5SCegNzKVghlM).onValueChanged.AddListener(delegate(float num4)
					{
						IdwU0_ARpD_wnWGSdVq151k.b = num4 / 255f;
					});
					DCWStzN3LM5SCegNzKVghlM.gameObject.AddComponent<AmMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F>().U_0024eIoX6e3N9Ag_us_EcGHBI(delegate
					{
						oGcOeB_JND2Yf4MrR30ymvpG_yhHQAKmTc9io3pzJtpnEZby5iOT3rhoy3VFnd_757iISUfMa5mF7FEj10c7V2oBfq_0024Tv3CMsaMghn7I98pCGadZphq8UvJZ_0024_QjFZSW11y_Iqlc_0024NOKJQYyGUK30ME.smethod_6(H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.H1Pr8mmPslEwoIU22reQcXE(DCWStzN3LM5SCegNzKVghlM), (float)(int)(IdwU0_ARpD_wnWGSdVq151k.b * 255f));
					});
				}
				yHjr9GR2wCKklzOW2Xkl_Hi1ImsI52cDM857W_TH9pHz.SetActive(fbSm64A_0024FzBvAjtith640zQ.Count > 0);
				XVDZE4YUgxeonNUvpvABIb4.SetActive(fbSm64A_0024FzBvAjtith640zQ.Count == 0 && BFMkrNuUjUFB4B6P9DkaJ_s != vpxRY7V6xxVwrtWzUazGbAijYYgy_j9aOURndFg_MPmV1XpOIQGYmugcr08FohKkWZtGTI6r2M_fFifxmanSRrgmSgq6IJSK9HbxY_0024RtywzhAZtQnNleJNvuto3IE49fVw.paint);
				RPPJ2nsw9RU2otM3BzDPzcY.SetActive(BFMkrNuUjUFB4B6P9DkaJ_s == vpxRY7V6xxVwrtWzUazGbAijYYgy_j9aOURndFg_MPmV1XpOIQGYmugcr08FohKkWZtGTI6r2M_fFifxmanSRrgmSgq6IJSK9HbxY_0024RtywzhAZtQnNleJNvuto3IE49fVw.paint);
				AZjgkDHxHA_hM7bKEiV7ES8.u5ER09FBgDoEuNjNt6mdw_k = true;
				AZjgkDHxHA_hM7bKEiV7ES8.k9jTQ33irMfqZyYWrqgwpFA = (AJ95PT5qH7SI3aQp44bnfe9rXdzyiYcVJnP8p242egZvaWA5f7vGynYUg9dQSkzYlA.YZ_0024PBkn7rausEmKONRxFfNZQD6SNoR50G7r60U9WBOJ4.yORc5mAq969v9kZYfhyjAiM() ? color_0 : zDsAAaOZvTBh2CeBs1I1rX5KlkZGnZx4276UUH5MmEQR);
				if (EmzpBqNhuUJvMBi03aDSG_w != null)
				{
					foreach (PrimitiveData primDatum in EmzpBqNhuUJvMBi03aDSG_w.primData)
					{
						if (!EedNaX9y9ColHoMDljdNd80izPNg_0024767fWYnDU3yANis.ContainsKey(primDatum))
						{
							GameObject gameObject9 = GameObject.CreatePrimitive(PrimitiveType.Cube);
							UnityEngine.Object.Destroy(gameObject9.GetComponent<Collider>());
							gameObject9.transform.rotation = Quaternion.Euler(primDatum.GetEuler());
							gameObject9.transform.localScale = primDatum.GetSize();
							Material material = new Material(Shader.Find(global::_003CModule_003E.smethod_29<string>(1327090556u)));
							material.color = new Color(primDatum.GetColor().r, primDatum.GetColor().g, primDatum.GetColor().b, 0.5f);
							gameObject9.GetComponent<Renderer>().material = material;
							EedNaX9y9ColHoMDljdNd80izPNg_0024767fWYnDU3yANis.Add(primDatum, gameObject9);
						}
						EedNaX9y9ColHoMDljdNd80izPNg_0024767fWYnDU3yANis[primDatum].transform.position = primDatum.GetPos() + AJ95PT5qH7SI3aQp44bnfe9rXdzyiYcVJnP8p242egZvaWA5f7vGynYUg9dQSkzYlA.Sl3qb7_zclIQRMX7sWCu5EE;
					}
				}
				if (EventSystem.current.currentSelectedGameObject == null)
				{
					if (Input.GetKeyDown(KeyCode.Alpha1))
					{
						BFMkrNuUjUFB4B6P9DkaJ_s = vpxRY7V6xxVwrtWzUazGbAijYYgy_j9aOURndFg_MPmV1XpOIQGYmugcr08FohKkWZtGTI6r2M_fFifxmanSRrgmSgq6IJSK9HbxY_0024RtywzhAZtQnNleJNvuto3IE49fVw.move;
					}
					else if (Input.GetKeyDown(KeyCode.Alpha2))
					{
						BFMkrNuUjUFB4B6P9DkaJ_s = vpxRY7V6xxVwrtWzUazGbAijYYgy_j9aOURndFg_MPmV1XpOIQGYmugcr08FohKkWZtGTI6r2M_fFifxmanSRrgmSgq6IJSK9HbxY_0024RtywzhAZtQnNleJNvuto3IE49fVw.rotate;
					}
					else if (Input.GetKeyDown(KeyCode.Alpha3))
					{
						BFMkrNuUjUFB4B6P9DkaJ_s = vpxRY7V6xxVwrtWzUazGbAijYYgy_j9aOURndFg_MPmV1XpOIQGYmugcr08FohKkWZtGTI6r2M_fFifxmanSRrgmSgq6IJSK9HbxY_0024RtywzhAZtQnNleJNvuto3IE49fVw.scale;
						if (fbSm64A_0024FzBvAjtith640zQ.Count > 1)
						{
							oPscfRQFZWvUSR8BZxgbHJMQBRDQTKjYlqK7tljpOgdF();
						}
					}
					else if (!Input.GetKeyDown(KeyCode.Alpha4))
					{
						if (!Input.GetKeyDown(KeyCode.Delete) && !Input.GetKeyDown(KeyCode.Backspace))
						{
							if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.X)) && fbSm64A_0024FzBvAjtith640zQ.Count != 0)
							{
								uHeKFpRK8CxNoYrUXwmBb_wvw8UhiG2Q6_VFz6q6ykR1.RTyDolIM2931YSJW3Hap4C4(global::_003CModule_003E.smethod_28<string>(1753819561u));
								if (EmzpBqNhuUJvMBi03aDSG_w != null)
								{
									XU_0024J1o9n9AOB0F3rIkTIPFo();
								}
								s5PVtdgcvvCFWcziTuHCc0k();
								if (Input.GetKeyDown(KeyCode.X))
								{
									List<GameObject> list = new List<GameObject>();
									xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA[] array = fbSm64A_0024FzBvAjtith640zQ.ToArray();
									foreach (xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA2 in array)
									{
										fbSm64A_0024FzBvAjtith640zQ.Remove(xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA2);
										list.Add(xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA2.eWUzF3zpMMjP5r9PB6rj474);
										GgrMl0eHjkTKzAqKC9Taf8i2a4kabVFQXHtJQoLFRBUAsOCvMpzQHKVdH70BhjSZvQ ggrMl0eHjkTKzAqKC9Taf8i2a4kabVFQXHtJQoLFRBUAsOCvMpzQHKVdH70BhjSZvQ = new GgrMl0eHjkTKzAqKC9Taf8i2a4kabVFQXHtJQoLFRBUAsOCvMpzQHKVdH70BhjSZvQ(list.ToArray());
										ggrMl0eHjkTKzAqKC9Taf8i2a4kabVFQXHtJQoLFRBUAsOCvMpzQHKVdH70BhjSZvQ.bv4xsECkipN_002441Wa7mcfeqY();
										_0024CMex_0024vAX35hsObIY7ThtQI.Add(ggrMl0eHjkTKzAqKC9Taf8i2a4kabVFQXHtJQoLFRBUAsOCvMpzQHKVdH70BhjSZvQ);
									}
								}
							}
							else if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKeyDown(KeyCode.V) && EmzpBqNhuUJvMBi03aDSG_w != null)
							{
								uHeKFpRK8CxNoYrUXwmBb_wvw8UhiG2Q6_VFz6q6ykR1.RTyDolIM2931YSJW3Hap4C4(global::_003CModule_003E.smethod_25<string>(3418263190u), -1f);
								BFMkrNuUjUFB4B6P9DkaJ_s = vpxRY7V6xxVwrtWzUazGbAijYYgy_j9aOURndFg_MPmV1XpOIQGYmugcr08FohKkWZtGTI6r2M_fFifxmanSRrgmSgq6IJSK9HbxY_0024RtywzhAZtQnNleJNvuto3IE49fVw.move;
								dz3caJwShuGQHB7LBjmInSE();
							}
							else if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKeyDown(KeyCode.Z))
							{
								oPscfRQFZWvUSR8BZxgbHJMQBRDQTKjYlqK7tljpOgdF();
								_X2PQ1Ly_CMNLsRnRNN_vybYUDyvl1x6gIuOELMr0x7x().M_UYuO7m_5zc0NRNG1OJLAE();
							}
							else if (Input.GetKeyDown(KeyCode.Escape) && EmzpBqNhuUJvMBi03aDSG_w != null)
							{
								XU_0024J1o9n9AOB0F3rIkTIPFo();
							}
						}
						else
						{
							List<GameObject> list2 = new List<GameObject>();
							xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA[] array = fbSm64A_0024FzBvAjtith640zQ.ToArray();
							foreach (xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA3 in array)
							{
								fbSm64A_0024FzBvAjtith640zQ.Remove(xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA3);
								list2.Add(xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA3.eWUzF3zpMMjP5r9PB6rj474);
								GgrMl0eHjkTKzAqKC9Taf8i2a4kabVFQXHtJQoLFRBUAsOCvMpzQHKVdH70BhjSZvQ ggrMl0eHjkTKzAqKC9Taf8i2a4kabVFQXHtJQoLFRBUAsOCvMpzQHKVdH70BhjSZvQ2 = new GgrMl0eHjkTKzAqKC9Taf8i2a4kabVFQXHtJQoLFRBUAsOCvMpzQHKVdH70BhjSZvQ(list2.ToArray());
								ggrMl0eHjkTKzAqKC9Taf8i2a4kabVFQXHtJQoLFRBUAsOCvMpzQHKVdH70BhjSZvQ2.bv4xsECkipN_002441Wa7mcfeqY();
								_0024CMex_0024vAX35hsObIY7ThtQI.Add(ggrMl0eHjkTKzAqKC9Taf8i2a4kabVFQXHtJQoLFRBUAsOCvMpzQHKVdH70BhjSZvQ2);
							}
							KEFHJCGICLE.HNAHBIMJDCB(global::_003CModule_003E.smethod_29<string>(3836435690u));
						}
					}
					else
					{
						BFMkrNuUjUFB4B6P9DkaJ_s = vpxRY7V6xxVwrtWzUazGbAijYYgy_j9aOURndFg_MPmV1XpOIQGYmugcr08FohKkWZtGTI6r2M_fFifxmanSRrgmSgq6IJSK9HbxY_0024RtywzhAZtQnNleJNvuto3IE49fVw.paint;
						if (fbSm64A_0024FzBvAjtith640zQ.Count > 0)
						{
							oPscfRQFZWvUSR8BZxgbHJMQBRDQTKjYlqK7tljpOgdF();
						}
					}
				}
				VEnLkFeX6Tle_M_0024bJO7QAfw.Tz4h_68oANQj5xAU0vtoknA.isOn = BFMkrNuUjUFB4B6P9DkaJ_s == vpxRY7V6xxVwrtWzUazGbAijYYgy_j9aOURndFg_MPmV1XpOIQGYmugcr08FohKkWZtGTI6r2M_fFifxmanSRrgmSgq6IJSK9HbxY_0024RtywzhAZtQnNleJNvuto3IE49fVw.move;
				JuznlPC_0024iVvWRp5Gx3m19rQ.Tz4h_68oANQj5xAU0vtoknA.isOn = BFMkrNuUjUFB4B6P9DkaJ_s == vpxRY7V6xxVwrtWzUazGbAijYYgy_j9aOURndFg_MPmV1XpOIQGYmugcr08FohKkWZtGTI6r2M_fFifxmanSRrgmSgq6IJSK9HbxY_0024RtywzhAZtQnNleJNvuto3IE49fVw.rotate;
				control0_0.Tz4h_68oANQj5xAU0vtoknA.isOn = BFMkrNuUjUFB4B6P9DkaJ_s == vpxRY7V6xxVwrtWzUazGbAijYYgy_j9aOURndFg_MPmV1XpOIQGYmugcr08FohKkWZtGTI6r2M_fFifxmanSRrgmSgq6IJSK9HbxY_0024RtywzhAZtQnNleJNvuto3IE49fVw.scale;
				UziXq1_0024N3sVQ1ybcZOKLrrk.Tz4h_68oANQj5xAU0vtoknA.isOn = BFMkrNuUjUFB4B6P9DkaJ_s == vpxRY7V6xxVwrtWzUazGbAijYYgy_j9aOURndFg_MPmV1XpOIQGYmugcr08FohKkWZtGTI6r2M_fFifxmanSRrgmSgq6IJSK9HbxY_0024RtywzhAZtQnNleJNvuto3IE49fVw.paint;
				iTtUTzVJqoLove2Mlyfm4HQ.SetActive(value: true);
				Bounds bounds = HwpI_80ZJwnJXUb_00244d3CPk7TldbzmlfcEQ4_x5hopVfO();
				if (iwaUWG_oqbQT6zAcJU5iwzU)
				{
					JQpZ6lP9aBgIeq1zc9f_eT6euqKTWcd_R1Xq7K0V0oLZ.d5XvqNLNuVqmANrqHnMnO9c(E4e53KKkkQc5_yliBk0AUfU, Quaternion.identity, new Vector3(0.1f, 0.1f, 0.1f), Color.magenta);
				}
				if (!AJ95PT5qH7SI3aQp44bnfe9rXdzyiYcVJnP8p242egZvaWA5f7vGynYUg9dQSkzYlA.YZ_0024PBkn7rausEmKONRxFfNZQD6SNoR50G7r60U9WBOJ4.yORc5mAq969v9kZYfhyjAiM())
				{
					if (!string.IsNullOrEmpty(yEDvddoTlWFXT2Rd0jPleHQ) && Input.GetMouseButton(0) && !Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
					{
						Q8GK9vSf_0024VEM692w50G2d5SmUcj4PtKNzDGwgaTN75YKao1YzfuGvFueAht8oPEnuw.smethod_1(cpZ_kSFtSLkQRO3C4PD7Nz4, bounds);
					}
					else if (z6J_8qxbU01ZXTzAmVrFIwygE3yzywLHjQ4ZzTsbQVxK)
					{
						WsaQ0Aqmeh9ZQLlVWABNM60.SetActive(value: false);
						BFMkrNuUjUFB4B6P9DkaJ_s = vpxRY7V6xxVwrtWzUazGbAijYYgy_j9aOURndFg_MPmV1XpOIQGYmugcr08FohKkWZtGTI6r2M_fFifxmanSRrgmSgq6IJSK9HbxY_0024RtywzhAZtQnNleJNvuto3IE49fVw.move;
						if (Input.GetMouseButton(0))
						{
							RaycastHit[] array2 = FV8xGHqtg_wVkgsNgdvcQU4gbPt56Nyt_3knt_RdxcuK(9999f);
							RaycastHit raycastHit = default(RaycastHit);
							bool flag = false;
							RaycastHit[] array3 = array2;
							for (int num2 = 0; num2 < array3.Length; num2++)
							{
								RaycastHit raycastHit2 = array3[num2];
								if (!rWJdVfn9_tn1f5vskxsQRZc(raycastHit2.transform.gameObject) && raycastHit2.transform.gameObject.layer != int_0)
								{
									raycastHit = raycastHit2;
									flag = true;
									break;
								}
							}
							if (flag)
							{
								bm4qNpKN8ThlleNq_0024v_0024DpHZBJQSkCOXr8EFjC57_0024ief7(raycastHit.point);
							}
						}
						else
						{
							z6J_8qxbU01ZXTzAmVrFIwygE3yzywLHjQ4ZzTsbQVxK = false;
							List<GameObject> list3 = new List<GameObject>();
							List<Vector3> list4 = new List<Vector3>();
							List<Vector3> list5 = new List<Vector3>();
							List<Quaternion> list6 = new List<Quaternion>();
							List<Quaternion> list7 = new List<Quaternion>();
							List<Vector3> list8 = new List<Vector3>();
							List<Vector3> list9 = new List<Vector3>();
							foreach (xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA key2 in nG3WvkfGAg1Lg2PMdQH_OZg.Keys)
							{
								list3.Add(key2.eWUzF3zpMMjP5r9PB6rj474);
								list4.Add(nG3WvkfGAg1Lg2PMdQH_OZg[key2]);
								list6.Add(ry9dPoTc05OsEAQnPpZ2_0024hA[key2]);
								list8.Add(MlLIlV50uQCTKGEbiPTTnIk[key2]);
								list5.Add(key2.TIWjI8FsBk2nlZk9NO4HNOE.position);
								list7.Add(key2.TIWjI8FsBk2nlZk9NO4HNOE.rotation);
								if (key2.MrZDDetpveRMT__0024biC7h8tU != xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA.Enum0.gate)
								{
									list9.Add(key2.TIWjI8FsBk2nlZk9NO4HNOE.localScale);
								}
								else
								{
									list9.Add(key2.cRC1fFhZVgamcERb3o0WavI.transform.localScale);
								}
							}
							XBS7OSIVgwiqp2YsikrDtq6zcM87cw3nXOw4Pre7ULHrFpjhoDlZmc_0024_dixBV9c1sQ xBS7OSIVgwiqp2YsikrDtq6zcM87cw3nXOw4Pre7ULHrFpjhoDlZmc_0024_dixBV9c1sQ = new XBS7OSIVgwiqp2YsikrDtq6zcM87cw3nXOw4Pre7ULHrFpjhoDlZmc_0024_dixBV9c1sQ(list3.ToArray(), list4.ToArray(), list6.ToArray(), list5.ToArray(), list7.ToArray(), list8.ToArray(), list9.ToArray());
							xBS7OSIVgwiqp2YsikrDtq6zcM87cw3nXOw4Pre7ULHrFpjhoDlZmc_0024_dixBV9c1sQ.bv4xsECkipN_002441Wa7mcfeqY();
							_0024CMex_0024vAX35hsObIY7ThtQI.Add(xBS7OSIVgwiqp2YsikrDtq6zcM87cw3nXOw4Pre7ULHrFpjhoDlZmc_0024_dixBV9c1sQ);
						}
					}
					else
					{
						if (pyLJQKsynReKRiJ14xze8DZN__0024ET1SK6Rm6XJMoZhPAE)
						{
							CpTvMSD2oogKxeVlGQN_0024uksze27FqWgbw6JiV7rT2qXI();
							pyLJQKsynReKRiJ14xze8DZN__0024ET1SK6Rm6XJMoZhPAE = false;
						}
						cpZ_kSFtSLkQRO3C4PD7Nz4 = null;
						yEDvddoTlWFXT2Rd0jPleHQ = "";
						RaycastHit[] array4 = FV8xGHqtg_wVkgsNgdvcQU4gbPt56Nyt_3knt_RdxcuK(9999f);
						RaycastHit raycastHit3 = default(RaycastHit);
						bool flag2 = false;
						bool key = Input.GetKey(KeyCode.LeftAlt);
						MeshRenderer[] componentsInChildren = WsaQ0Aqmeh9ZQLlVWABNM60.GetComponentsInChildren<MeshRenderer>();
						for (int num2 = 0; num2 < componentsInChildren.Length; num2++)
						{
							componentsInChildren[num2].material.shader = Shader.Find(key ? global::_003CModule_003E.smethod_28<string>(589787214u) : global::_003CModule_003E.smethod_25<string>(1251332769u));
						}
						RaycastHit[] array3 = array4;
						for (int num2 = 0; num2 < array3.Length; num2++)
						{
							RaycastHit raycastHit4 = array3[num2];
							if ((raycastHit4.transform.gameObject.layer == int_0 || raycastHit4.collider.GetComponent<PointController>() != null || (raycastHit4.transform.gameObject.layer == UDIRjB3VSmwHpVmUxcnHH66kk6ThWzbxmySWu7OngspoKs_47sbCTTGVAxk9y7DBfQ.fhrfRxVubQ7jQHGH5tZ1Xn4 && raycastHit4.transform.GetComponent<PrimitiveController>() != null)) && (!key || !(WsaQ0Aqmeh9ZQLlVWABNM60 != null) || !WsaQ0Aqmeh9ZQLlVWABNM60.activeSelf || !(raycastHit4.transform.parent != null) || raycastHit4.transform.parent.name.StartsWith(global::_003CModule_003E.smethod_29<string>(1451672530u))))
							{
								raycastHit3 = raycastHit4;
								flag2 = true;
								break;
							}
						}
						if (flag2 && (EventSystem.current.currentSelectedGameObject != null || EventSystem.current.IsPointerOverGameObject()))
						{
							flag2 = false;
						}
						if (flag2)
						{
							if (!(raycastHit3.transform.parent != null) || !raycastHit3.transform.parent.name.StartsWith(global::_003CModule_003E.smethod_25<string>(3874954841u)) || Input.GetKey(KeyCode.V))
							{
								ACXXqeIVtNHPY3xLo852oh8(raycastHit3.transform, raycastHit3.point);
							}
							else
							{
								cpZ_kSFtSLkQRO3C4PD7Nz4 = raycastHit3.transform.parent;
								_ = cpZ_kSFtSLkQRO3C4PD7Nz4.name.Substring(3)[0];
								char c = cpZ_kSFtSLkQRO3C4PD7Nz4.name.Substring(3)[1];
								if (iwaUWG_oqbQT6zAcJU5iwzU)
								{
									ra5MAEm12eKHvW9LN8brPaE.transform.position = E4e53KKkkQc5_yliBk0AUfU;
								}
								else
								{
									ra5MAEm12eKHvW9LN8brPaE.transform.position = bounds.center;
								}
								ra5MAEm12eKHvW9LN8brPaE.transform.localScale = Vector3.one;
								if (BFMkrNuUjUFB4B6P9DkaJ_s != vpxRY7V6xxVwrtWzUazGbAijYYgy_j9aOURndFg_MPmV1XpOIQGYmugcr08FohKkWZtGTI6r2M_fFifxmanSRrgmSgq6IJSK9HbxY_0024RtywzhAZtQnNleJNvuto3IE49fVw.move && BFMkrNuUjUFB4B6P9DkaJ_s != vpxRY7V6xxVwrtWzUazGbAijYYgy_j9aOURndFg_MPmV1XpOIQGYmugcr08FohKkWZtGTI6r2M_fFifxmanSRrgmSgq6IJSK9HbxY_0024RtywzhAZtQnNleJNvuto3IE49fVw.rotate)
								{
									ra5MAEm12eKHvW9LN8brPaE.transform.position = fbSm64A_0024FzBvAjtith640zQ[0].TIWjI8FsBk2nlZk9NO4HNOE.position;
									ra5MAEm12eKHvW9LN8brPaE.transform.rotation = fbSm64A_0024FzBvAjtith640zQ[0].TIWjI8FsBk2nlZk9NO4HNOE.rotation;
									if (fbSm64A_0024FzBvAjtith640zQ[0].MrZDDetpveRMT__0024biC7h8tU == xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA.Enum0.gate)
									{
										ra5MAEm12eKHvW9LN8brPaE.transform.localScale = fbSm64A_0024FzBvAjtith640zQ[0].cRC1fFhZVgamcERb3o0WavI.transform.localScale;
									}
									else
									{
										ra5MAEm12eKHvW9LN8brPaE.transform.localScale = fbSm64A_0024FzBvAjtith640zQ[0].TIWjI8FsBk2nlZk9NO4HNOE.localScale;
									}
									bounds = HwpI_80ZJwnJXUb_00244d3CPk7TldbzmlfcEQ4_x5hopVfO();
								}
								else
								{
									Vector3 vector = Ycv8NzDyer8S_j_0024qIIEeExGtGcSZnc0IwQTxmy_Pd_0024co(c.ToString());
									ra5MAEm12eKHvW9LN8brPaE.transform.LookAt(sq3LR75Glc8ZmNWPHc9AJN1oe5yzJWHlY9OPLFStJJO_(cpZ_kSFtSLkQRO3C4PD7Nz4.position, vector), vector);
								}
								l6TwAdL5XExVQO7WyjsYgIc = raycastHit3.transform.parent.exhY6_0024AhIhNhSJdwumimcf0iHRTFwCXVc9Jdygwp0YSE(raycastHit3.point);
								jEMU6pSLJji8yTM1i2wQXNFdtjNYw3q1q7CFfiLrlsgP = new nnHuJswxvsz1pZEYGN_00241t_hRktuePQgaPa2hccgMZBGMarJabqUZUQ4omKAa5FU0eA(ra5MAEm12eKHvW9LN8brPaE.transform.position, ra5MAEm12eKHvW9LN8brPaE.transform.rotation, ra5MAEm12eKHvW9LN8brPaE.transform.localScale, bounds);
								Q8GK9vSf_0024VEM692w50G2d5SmUcj4PtKNzDGwgaTN75YKao1YzfuGvFueAht8oPEnuw.smethod_1(raycastHit3.transform.parent, bounds);
							}
						}
						else
						{
							ACXXqeIVtNHPY3xLo852oh8(null, Vector3.zero);
						}
					}
				}
				else
				{
					ACXXqeIVtNHPY3xLo852oh8(null, Vector3.zero);
				}
				if (Input.GetKeyUp(KeyCode.V))
				{
					iwaUWG_oqbQT6zAcJU5iwzU = false;
				}
				for (int num3 = 0; num3 < fbSm64A_0024FzBvAjtith640zQ.Count; num3++)
				{
					fXmKrvqNNkqzpO5XpWPRVZI8_mbex_0024Y2G0UhxyZZLrx0[num3].u5ER09FBgDoEuNjNt6mdw_k = true;
					fXmKrvqNNkqzpO5XpWPRVZI8_mbex_0024Y2G0UhxyZZLrx0[num3].k9jTQ33irMfqZyYWrqgwpFA = Q90OdRkyhGNvujZeyO47oyPTjunqX_fS_bBOnFmBTJzx.Evaluate(Time.unscaledTime % 1f);
					fXmKrvqNNkqzpO5XpWPRVZI8_mbex_0024Y2G0UhxyZZLrx0[num3].transform.localScale = fbSm64A_0024FzBvAjtith640zQ[num3].TIWjI8FsBk2nlZk9NO4HNOE.localScale;
					fXmKrvqNNkqzpO5XpWPRVZI8_mbex_0024Y2G0UhxyZZLrx0[num3].transform.position = fbSm64A_0024FzBvAjtith640zQ[num3].TIWjI8FsBk2nlZk9NO4HNOE.position;
					fXmKrvqNNkqzpO5XpWPRVZI8_mbex_0024Y2G0UhxyZZLrx0[num3].transform.rotation = fbSm64A_0024FzBvAjtith640zQ[num3].TIWjI8FsBk2nlZk9NO4HNOE.rotation;
				}
				if (fbSm64A_0024FzBvAjtith640zQ.Count <= 0)
				{
					WsaQ0Aqmeh9ZQLlVWABNM60.SetActive(value: false);
				}
				else
				{
					if (BFMkrNuUjUFB4B6P9DkaJ_s == vpxRY7V6xxVwrtWzUazGbAijYYgy_j9aOURndFg_MPmV1XpOIQGYmugcr08FohKkWZtGTI6r2M_fFifxmanSRrgmSgq6IJSK9HbxY_0024RtywzhAZtQnNleJNvuto3IE49fVw.scale)
					{
						WsaQ0Aqmeh9ZQLlVWABNM60.SetActive(fbSm64A_0024FzBvAjtith640zQ[0].MrZDDetpveRMT__0024biC7h8tU == xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA.Enum0.block);
					}
					else
					{
						WsaQ0Aqmeh9ZQLlVWABNM60.SetActive(!z6J_8qxbU01ZXTzAmVrFIwygE3yzywLHjQ4ZzTsbQVxK);
					}
					bounds = HwpI_80ZJwnJXUb_00244d3CPk7TldbzmlfcEQ4_x5hopVfO();
					if (!iwaUWG_oqbQT6zAcJU5iwzU)
					{
						if (fbSm64A_0024FzBvAjtith640zQ.Count != 1 || (BFMkrNuUjUFB4B6P9DkaJ_s != vpxRY7V6xxVwrtWzUazGbAijYYgy_j9aOURndFg_MPmV1XpOIQGYmugcr08FohKkWZtGTI6r2M_fFifxmanSRrgmSgq6IJSK9HbxY_0024RtywzhAZtQnNleJNvuto3IE49fVw.move && BFMkrNuUjUFB4B6P9DkaJ_s != vpxRY7V6xxVwrtWzUazGbAijYYgy_j9aOURndFg_MPmV1XpOIQGYmugcr08FohKkWZtGTI6r2M_fFifxmanSRrgmSgq6IJSK9HbxY_0024RtywzhAZtQnNleJNvuto3IE49fVw.scale) || !vS40ylI3Hbm7uPsVJpFsna0)
						{
							CGLANt8WtN7FK3KZMHNGJgw(bounds.center, Quaternion.identity, bounds.extents * 2f, BFMkrNuUjUFB4B6P9DkaJ_s, scaleWithCamera: true);
						}
						else
						{
							CGLANt8WtN7FK3KZMHNGJgw(fbSm64A_0024FzBvAjtith640zQ[0].TIWjI8FsBk2nlZk9NO4HNOE.position, fbSm64A_0024FzBvAjtith640zQ[0].TIWjI8FsBk2nlZk9NO4HNOE.rotation, fbSm64A_0024FzBvAjtith640zQ[0].TIWjI8FsBk2nlZk9NO4HNOE.localScale, BFMkrNuUjUFB4B6P9DkaJ_s, scaleWithCamera: true);
						}
					}
					else
					{
						CGLANt8WtN7FK3KZMHNGJgw(E4e53KKkkQc5_yliBk0AUfU, Quaternion.identity, Vector3.one, BFMkrNuUjUFB4B6P9DkaJ_s, scaleWithCamera: true);
					}
					if (EventSystem.current.currentSelectedGameObject == null)
					{
						Q6uRAB_0024_S89ANxgzZ0QH8mk.pZEKY5TzLd4S3z2lXESoRnw = bounds.center.x.ToString(global::_003CModule_003E.smethod_28<string>(1480639858u));
						Xrd9z2_WYChPpqRSvNFZWiw.pZEKY5TzLd4S3z2lXESoRnw = bounds.center.y.ToString(global::_003CModule_003E.smethod_27<string>(3550226973u));
						eZaggCkfDTJE2dhuLSEvCSs.pZEKY5TzLd4S3z2lXESoRnw = bounds.center.z.ToString(global::_003CModule_003E.smethod_28<string>(1480639858u));
						jq_0024gxTE9SdmPwMA9OoDUaD4.transform.parent.gameObject.SetActive(fbSm64A_0024FzBvAjtith640zQ.Count == 1);
						qSG8Li7AV2HL0xo0xYL2dhs.transform.parent.gameObject.SetActive(fbSm64A_0024FzBvAjtith640zQ.Count == 1);
						xPIozX5u7HFDqrFoaQvRcBc.transform.parent.gameObject.SetActive(fbSm64A_0024FzBvAjtith640zQ.Count == 1);
						oZphDbXncaFSC1_0024Uxg8QFMo.transform.parent.gameObject.SetActive(fbSm64A_0024FzBvAjtith640zQ.Count == 1 && fbSm64A_0024FzBvAjtith640zQ[0].MrZDDetpveRMT__0024biC7h8tU != xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA.Enum0.spawnpoint);
						vKv0P28kAhNLiR7WXKJDGO4.transform.parent.gameObject.SetActive(fbSm64A_0024FzBvAjtith640zQ.Count == 1 && fbSm64A_0024FzBvAjtith640zQ[0].MrZDDetpveRMT__0024biC7h8tU != xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA.Enum0.spawnpoint);
						sliderController_0.transform.parent.gameObject.SetActive(fbSm64A_0024FzBvAjtith640zQ.Count == 1 && fbSm64A_0024FzBvAjtith640zQ[0].MrZDDetpveRMT__0024biC7h8tU != xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA.Enum0.spawnpoint && fbSm64A_0024FzBvAjtith640zQ[0].MrZDDetpveRMT__0024biC7h8tU != xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA.Enum0.gate);
						if (fbSm64A_0024FzBvAjtith640zQ.Count == 1)
						{
							if (fbSm64A_0024FzBvAjtith640zQ[0].TIWjI8FsBk2nlZk9NO4HNOE.rotation.eulerAngles.x <= 270f)
							{
								H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.H1Pr8mmPslEwoIU22reQcXE(jq_0024gxTE9SdmPwMA9OoDUaD4).value = fbSm64A_0024FzBvAjtith640zQ[0].TIWjI8FsBk2nlZk9NO4HNOE.rotation.eulerAngles.x;
							}
							else
							{
								H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.H1Pr8mmPslEwoIU22reQcXE(jq_0024gxTE9SdmPwMA9OoDUaD4).value = fbSm64A_0024FzBvAjtith640zQ[0].TIWjI8FsBk2nlZk9NO4HNOE.rotation.eulerAngles.x - 360f;
							}
							H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.H1Pr8mmPslEwoIU22reQcXE(qSG8Li7AV2HL0xo0xYL2dhs).value = fbSm64A_0024FzBvAjtith640zQ[0].TIWjI8FsBk2nlZk9NO4HNOE.rotation.eulerAngles.y;
							H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.H1Pr8mmPslEwoIU22reQcXE(xPIozX5u7HFDqrFoaQvRcBc).value = fbSm64A_0024FzBvAjtith640zQ[0].TIWjI8FsBk2nlZk9NO4HNOE.rotation.eulerAngles.z;
							if (fbSm64A_0024FzBvAjtith640zQ[0].MrZDDetpveRMT__0024biC7h8tU != xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA.Enum0.gate)
							{
								H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.H1Pr8mmPslEwoIU22reQcXE(oZphDbXncaFSC1_0024Uxg8QFMo).maxValue = 255f;
								H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.H1Pr8mmPslEwoIU22reQcXE(vKv0P28kAhNLiR7WXKJDGO4).maxValue = 255f;
								H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.H1Pr8mmPslEwoIU22reQcXE(oZphDbXncaFSC1_0024Uxg8QFMo).minValue = 1f;
								H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.H1Pr8mmPslEwoIU22reQcXE(vKv0P28kAhNLiR7WXKJDGO4).minValue = 1f;
								H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.H1Pr8mmPslEwoIU22reQcXE(oZphDbXncaFSC1_0024Uxg8QFMo).value = fbSm64A_0024FzBvAjtith640zQ[0].TIWjI8FsBk2nlZk9NO4HNOE.localScale.x;
								H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.H1Pr8mmPslEwoIU22reQcXE(vKv0P28kAhNLiR7WXKJDGO4).value = fbSm64A_0024FzBvAjtith640zQ[0].TIWjI8FsBk2nlZk9NO4HNOE.localScale.y;
								H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.H1Pr8mmPslEwoIU22reQcXE(sliderController_0).value = fbSm64A_0024FzBvAjtith640zQ[0].TIWjI8FsBk2nlZk9NO4HNOE.localScale.z;
							}
							else
							{
								H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.H1Pr8mmPslEwoIU22reQcXE(oZphDbXncaFSC1_0024Uxg8QFMo).maxValue = 1000f;
								H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.H1Pr8mmPslEwoIU22reQcXE(vKv0P28kAhNLiR7WXKJDGO4).maxValue = 1000f;
								H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.H1Pr8mmPslEwoIU22reQcXE(oZphDbXncaFSC1_0024Uxg8QFMo).minValue = 10f;
								H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.H1Pr8mmPslEwoIU22reQcXE(vKv0P28kAhNLiR7WXKJDGO4).minValue = 10f;
								H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.H1Pr8mmPslEwoIU22reQcXE(oZphDbXncaFSC1_0024Uxg8QFMo).value = fbSm64A_0024FzBvAjtith640zQ[0].cRC1fFhZVgamcERb3o0WavI.transform.localScale.x;
								H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.H1Pr8mmPslEwoIU22reQcXE(vKv0P28kAhNLiR7WXKJDGO4).value = fbSm64A_0024FzBvAjtith640zQ[0].cRC1fFhZVgamcERb3o0WavI.transform.localScale.y;
							}
						}
					}
				}
				AJ95PT5qH7SI3aQp44bnfe9rXdzyiYcVJnP8p242egZvaWA5f7vGynYUg9dQSkzYlA.YZ_0024PBkn7rausEmKONRxFfNZQD6SNoR50G7r60U9WBOJ4.LENDBHNDHHH = AJ95PT5qH7SI3aQp44bnfe9rXdzyiYcVJnP8p242egZvaWA5f7vGynYUg9dQSkzYlA.Sl3qb7_zclIQRMX7sWCu5EE;
				AJ95PT5qH7SI3aQp44bnfe9rXdzyiYcVJnP8p242egZvaWA5f7vGynYUg9dQSkzYlA.YZ_0024PBkn7rausEmKONRxFfNZQD6SNoR50G7r60U9WBOJ4.smethod_1(AJ95PT5qH7SI3aQp44bnfe9rXdzyiYcVJnP8p242egZvaWA5f7vGynYUg9dQSkzYlA.Sl3qb7_zclIQRMX7sWCu5EE);
				__instance.SetTXT(global::_003CModule_003E.smethod_27<string>(2542726339u), AJ95PT5qH7SI3aQp44bnfe9rXdzyiYcVJnP8p242egZvaWA5f7vGynYUg9dQSkzYlA.Sl3qb7_zclIQRMX7sWCu5EE.x.ToString(global::_003CModule_003E.smethod_27<string>(3550226973u)));
				__instance.SetTXT(global::_003CModule_003E.smethod_27<string>(997840295u), AJ95PT5qH7SI3aQp44bnfe9rXdzyiYcVJnP8p242egZvaWA5f7vGynYUg9dQSkzYlA.Sl3qb7_zclIQRMX7sWCu5EE.y.ToString(global::_003CModule_003E.smethod_28<string>(1480639858u)));
				__instance.SetTXT(global::_003CModule_003E.smethod_26<string>(3960589722u), AJ95PT5qH7SI3aQp44bnfe9rXdzyiYcVJnP8p242egZvaWA5f7vGynYUg9dQSkzYlA.Sl3qb7_zclIQRMX7sWCu5EE.z.ToString(global::_003CModule_003E.smethod_27<string>(3550226973u)));
				oo_JlRDjpbsImWqzKiYAW2Lar_00247fgYlrUzew9ABs11el(__instance);
				return false;
			}
			if (smethod_8((UnityEngine.Object)AZjgkDHxHA_hM7bKEiV7ES8, (UnityEngine.Object)null))
			{
				if (!Boolean_0)
				{
					AZjgkDHxHA_hM7bKEiV7ES8.u5ER09FBgDoEuNjNt6mdw_k = false;
				}
				else
				{
					AZjgkDHxHA_hM7bKEiV7ES8.k9jTQ33irMfqZyYWrqgwpFA = zDsAAaOZvTBh2CeBs1I1rX5KlkZGnZx4276UUH5MmEQR;
				}
			}
			if (smethod_8((UnityEngine.Object)XVDZE4YUgxeonNUvpvABIb4, (UnityEngine.Object)null))
			{
				smethod_2(XVDZE4YUgxeonNUvpvABIb4, bool_0: false);
				smethod_2(yHjr9GR2wCKklzOW2Xkl_Hi1ImsI52cDM857W_TH9pHz, bool_0: false);
				smethod_2(iTtUTzVJqoLove2Mlyfm4HQ, bool_0: false);
				smethod_2(RPPJ2nsw9RU2otM3BzDPzcY, bool_0: false);
			}
			if (smethod_9((UnityEngine.Object)BgzEAaH8KDjUX0HV0on_Qy8, (UnityEngine.Object)null))
			{
				Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<Dictionary<string, GameObject>>(global::_003CModule_003E.smethod_28<string>(3426758645u), __instance).TryGetValue(global::_003CModule_003E.smethod_27<string>(1059023421u), out var value);
				if (smethod_8((UnityEngine.Object)value, (UnityEngine.Object)null))
				{
					BgzEAaH8KDjUX0HV0on_Qy8 = value.GetComponent<Button>();
				}
				else
				{
					BgzEAaH8KDjUX0HV0on_Qy8 = null;
				}
			}
			if (smethod_8((UnityEngine.Object)BgzEAaH8KDjUX0HV0on_Qy8, (UnityEngine.Object)null))
			{
				bool flag3 = Boolean_0 && _0024CMex_0024vAX35hsObIY7ThtQI.Count > 0;
				if (smethod_10((Selectable)BgzEAaH8KDjUX0HV0on_Qy8) != flag3)
				{
					smethod_11((Selectable)BgzEAaH8KDjUX0HV0on_Qy8, flag3);
				}
			}
			if (!Boolean_0 && fXmKrvqNNkqzpO5XpWPRVZI8_mbex_0024Y2G0UhxyZZLrx0.Count > 0)
			{
				c2wojWPyeJ90X8UxbVw8Ie7t2iSXT4W6AM5UctjLMkqq[] array5 = fXmKrvqNNkqzpO5XpWPRVZI8_mbex_0024Y2G0UhxyZZLrx0.ToArray();
				foreach (c2wojWPyeJ90X8UxbVw8Ie7t2iSXT4W6AM5UctjLMkqq c2wojWPyeJ90X8UxbVw8Ie7t2iSXT4W6AM5UctjLMkqq2 in array5)
				{
					fXmKrvqNNkqzpO5XpWPRVZI8_mbex_0024Y2G0UhxyZZLrx0.Remove(c2wojWPyeJ90X8UxbVw8Ie7t2iSXT4W6AM5UctjLMkqq2);
					smethod_13((UnityEngine.Object)smethod_12((Component)c2wojWPyeJ90X8UxbVw8Ie7t2iSXT4W6AM5UctjLMkqq2));
				}
				smethod_2(WsaQ0Aqmeh9ZQLlVWABNM60, bool_0: false);
			}
			foreach (GameObject value2 in EedNaX9y9ColHoMDljdNd80izPNg_0024767fWYnDU3yANis.Values)
			{
				smethod_13((UnityEngine.Object)value2);
			}
			EedNaX9y9ColHoMDljdNd80izPNg_0024767fWYnDU3yANis.Clear();
			return true;
		}

		internal static GameObject smethod_1(SceneMan sceneMan_0, string string_0)
		{
			return sceneMan_0.GetBTN(string_0);
		}

		internal static void smethod_2(GameObject gameObject_0, bool bool_0)
		{
			gameObject_0.SetActive(bool_0);
		}

		internal static GameObject smethod_3(SceneMan sceneMan_0, string string_0)
		{
			return sceneMan_0.GetTGL(string_0);
		}

		internal static bool smethod_4(GameObject gameObject_0)
		{
			return gameObject_0.activeInHierarchy;
		}

		internal static bool smethod_5(Toggle toggle_0)
		{
			return toggle_0.isOn;
		}

		internal static GameObject smethod_6(SceneMan sceneMan_0, string string_0)
		{
			return sceneMan_0.GetGRP(string_0);
		}

		internal static bool smethod_7(GameObject gameObject_0)
		{
			return gameObject_0.activeSelf;
		}

		internal static bool smethod_8(UnityEngine.Object object_0, UnityEngine.Object object_1)
		{
			return object_0 != object_1;
		}

		internal static bool smethod_9(UnityEngine.Object object_0, UnityEngine.Object object_1)
		{
			return object_0 == object_1;
		}

		internal static bool smethod_10(Selectable selectable_0)
		{
			return selectable_0.interactable;
		}

		internal static void smethod_11(Selectable selectable_0, bool bool_0)
		{
			selectable_0.interactable = bool_0;
		}

		internal static GameObject smethod_12(Component component_0)
		{
			return component_0.gameObject;
		}

		internal static void smethod_13(UnityEngine.Object object_0)
		{
			UnityEngine.Object.Destroy(object_0);
		}

		internal static bool smethod_14(int int_0)
		{
			return Input.GetMouseButton(int_0);
		}

		internal static bool smethod_15(SystemData.EHLMFKOOHLI ehlmfkoohli_0)
		{
			return HOCGCCAIPFF.FGCCNKAIKAI(ehlmfkoohli_0);
		}

		internal static GameObject smethod_16(string string_0)
		{
			return new GameObject(string_0);
		}

		internal static Transform smethod_17(GameObject gameObject_0)
		{
			return gameObject_0.transform;
		}

		internal static void smethod_18(Transform transform_0, Transform transform_1)
		{
			transform_0.parent = transform_1;
		}
	}

	[HarmonyPatch("OnSwitch")]
	[HarmonyPatch(typeof(SceneMan))]
	internal static class jLOxhHK9EVxooJKItvFQG0G7FOaLUlGVn5zFuPYyq6DUUj6BJXH6ATMSwuF8GTr7LEkMDBpu8a79O840FQjdSVUEs1_Y_0024DGHqnB9MKf_0024xZMpdlbx8AgC4_uzvhqLmIuNQA
	{
		[HarmonyPrefix]
		internal static bool smethod_0(GameObject NGLBLAGMBLN, SceneMan __instance)
		{
			if (smethod_1((object)__instance) != smethod_2(typeof(Construct).TypeHandle))
			{
				return true;
			}
			if (!smethod_4(smethod_3(NGLBLAGMBLN), global::_003CModule_003E.smethod_29<string>(2480680153u)))
			{
				return true;
			}
			Construct construct = (Construct)__instance;
			bool flag;
			if (!(flag = smethod_5(NGLBLAGMBLN.GetComponent<Toggle>())))
			{
				smethod_6((Arena)construct, bool_0: false, bool_1: false);
			}
			else
			{
				AJ95PT5qH7SI3aQp44bnfe9rXdzyiYcVJnP8p242egZvaWA5f7vGynYUg9dQSkzYlA.YZ_0024PBkn7rausEmKONRxFfNZQD6SNoR50G7r60U9WBOJ4.EANKCCAGMJD = 0;
				AJ95PT5qH7SI3aQp44bnfe9rXdzyiYcVJnP8p242egZvaWA5f7vGynYUg9dQSkzYlA.YZ_0024PBkn7rausEmKONRxFfNZQD6SNoR50G7r60U9WBOJ4.ONGNOMCJBGE = false;
			}
			if (!flag)
			{
				smethod_10(hEh6p6DPBEQL_ETI_0024wQQtEtSJ6RoJNEcKCHdSC5kM6sUcMYvs_Zy72CNlwLiq1PGFg.b0oQVxrK6OcBEF15Qp5Po1UYSE5jU1HDRExzTRGCdRAp, AJ95PT5qH7SI3aQp44bnfe9rXdzyiYcVJnP8p242egZvaWA5f7vGynYUg9dQSkzYlA.Sl3qb7_zclIQRMX7sWCu5EE, Quaternion.identity, bool_0: true);
			}
			else
			{
				AJ95PT5qH7SI3aQp44bnfe9rXdzyiYcVJnP8p242egZvaWA5f7vGynYUg9dQSkzYlA.Sl3qb7_zclIQRMX7sWCu5EE = smethod_8(smethod_7(hEh6p6DPBEQL_ETI_0024wQQtEtSJ6RoJNEcKCHdSC5kM6sUcMYvs_Zy72CNlwLiq1PGFg.rthvmLr4gScso3Qpneyh1LypI_0024Wi5MKTR0Q5CDkAnA8z));
				smethod_9(1);
			}
			smethod_11(!flag);
			HOCGCCAIPFF.HJLGLEOIJLF = (flag ? (-1) : 0);
			AJ95PT5qH7SI3aQp44bnfe9rXdzyiYcVJnP8p242egZvaWA5f7vGynYUg9dQSkzYlA.YZ_0024PBkn7rausEmKONRxFfNZQD6SNoR50G7r60U9WBOJ4.NDIOFGDJAJO = false;
			AJ95PT5qH7SI3aQp44bnfe9rXdzyiYcVJnP8p242egZvaWA5f7vGynYUg9dQSkzYlA.YZ_0024PBkn7rausEmKONRxFfNZQD6SNoR50G7r60U9WBOJ4.IIFFDLJDNJL = !flag;
			if (!smethod_13(smethod_12((SceneMan)construct, global::_003CModule_003E.smethod_25<string>(2885014434u))))
			{
				smethod_14((SceneMan)construct, global::_003CModule_003E.smethod_26<string>(4004064903u), flag);
				smethod_14((SceneMan)construct, global::_003CModule_003E.smethod_26<string>(3682321294u), flag);
			}
			smethod_16(smethod_15(__instance, global::_003CModule_003E.smethod_28<string>(1116400254u)), !flag);
			smethod_16(smethod_15(__instance, global::_003CModule_003E.smethod_25<string>(4118996364u)), !flag);
			smethod_16(smethod_17(__instance, global::_003CModule_003E.smethod_26<string>(4163472451u)), !flag);
			if (smethod_18((UnityEngine.Object)_0024UF3Dx_F00ogmssHXYlvwcQ, (UnityEngine.Object)null))
			{
				_0024UF3Dx_F00ogmssHXYlvwcQ = Q8GK9vSf_0024VEM692w50G2d5SmUcj4PtKNzDGwgaTN75YKao1YzfuGvFueAht8oPEnuw.smethod_0(setupScripts: true);
			}
			smethod_16(smethod_20((Component)smethod_19(smethod_7(_0024UF3Dx_F00ogmssHXYlvwcQ), global::_003CModule_003E.smethod_28<string>(2227827983u))), flag);
			smethod_21(global::_003CModule_003E.smethod_25<string>(2435427672u), 1f);
			return false;
		}

		internal static Type smethod_1(object object_0)
		{
			return object_0.GetType();
		}

		internal static Type smethod_2(RuntimeTypeHandle runtimeTypeHandle_0)
		{
			return Type.GetTypeFromHandle(runtimeTypeHandle_0);
		}

		internal static string smethod_3(GameObject gameObject_0)
		{
			return SceneMan.GetWidgetName(gameObject_0);
		}

		internal static bool smethod_4(string string_0, string string_1)
		{
			return string_0 == string_1;
		}

		internal static bool smethod_5(Toggle toggle_0)
		{
			return toggle_0.isOn;
		}

		internal static void smethod_6(Arena arena_0, bool bool_0, bool bool_1)
		{
			arena_0.LockSelf(bool_0, bool_1);
		}

		internal static Transform smethod_7(GameObject gameObject_0)
		{
			return gameObject_0.transform;
		}

		internal static Vector3 smethod_8(Transform transform_0)
		{
			return transform_0.position;
		}

		internal static void smethod_9(int int_0)
		{
			HOCGCCAIPFF.OHCMPEJKDHJ(int_0);
		}

		internal static void smethod_10(MachineController machineController_0, Vector3 vector3_0, Quaternion quaternion_0, bool bool_0)
		{
			machineController_0.Warp(vector3_0, quaternion_0, bool_0);
		}

		internal static void smethod_11(bool bool_0)
		{
			HOCGCCAIPFF.FGHEGHMMIEF(bool_0);
		}

		internal static GameObject smethod_12(SceneMan sceneMan_0, string string_0)
		{
			return sceneMan_0.GetPNL(string_0);
		}

		internal static bool smethod_13(GameObject gameObject_0)
		{
			return gameObject_0.activeSelf;
		}

		internal static void smethod_14(SceneMan sceneMan_0, string string_0, bool bool_0)
		{
			sceneMan_0.ValidatePNL(string_0, bool_0);
		}

		internal static GameObject smethod_15(SceneMan sceneMan_0, string string_0)
		{
			return sceneMan_0.GetTXT(string_0);
		}

		internal static void smethod_16(GameObject gameObject_0, bool bool_0)
		{
			gameObject_0.SetActive(bool_0);
		}

		internal static GameObject smethod_17(SceneMan sceneMan_0, string string_0)
		{
			return sceneMan_0.GetIMG(string_0);
		}

		internal static bool smethod_18(UnityEngine.Object object_0, UnityEngine.Object object_1)
		{
			return object_0 == object_1;
		}

		internal static Transform smethod_19(Transform transform_0, string string_0)
		{
			return transform_0.Find(string_0);
		}

		internal static GameObject smethod_20(Component component_0)
		{
			return component_0.gameObject;
		}

		internal static void smethod_21(string string_0, float float_0)
		{
			KEFHJCGICLE.HNAHBIMJDCB(string_0, float_0);
		}
	}

	[Serializable]
	[CompilerGenerated]
	private sealed class YWMdQ1cueAvkZ2aXP8YuFyNoFjp9s1KOe_EpaOavmJdKNbtxf_0024jzoQoGkAusaugxvYsa06fgpQOmWvfHGTCAQ4c
	{
		public static readonly YWMdQ1cueAvkZ2aXP8YuFyNoFjp9s1KOe_EpaOavmJdKNbtxf_0024jzoQoGkAusaugxvYsa06fgpQOmWvfHGTCAQ4c _003C_003E9 = new YWMdQ1cueAvkZ2aXP8YuFyNoFjp9s1KOe_EpaOavmJdKNbtxf_0024jzoQoGkAusaugxvYsa06fgpQOmWvfHGTCAQ4c();

		public static Action<AmMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F> _003C_003E9__56_0;

		public static Func<RaycastHit, float> _003C_003E9__64_0;

		internal void q_nbrxhBKt5p32WhkR_EqNiiCyQCXtbJpBTiYjlte_cm(AmMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F qdc)
		{
			smethod_2(smethod_0((Component)qdc), Quaternion.Inverse(smethod_1(smethod_0((Component)AJ95PT5qH7SI3aQp44bnfe9rXdzyiYcVJnP8p242egZvaWA5f7vGynYUg9dQSkzYlA.YZ_0024PBkn7rausEmKONRxFfNZQD6SNoR50G7r60U9WBOJ4))));
		}

		internal float vPOsF7F_0024CzfWjZPK_0024NAAgq2HOGipL_0024_0024gw3w25mTxSvij(RaycastHit h)
		{
			return h.distance;
		}

		internal static Transform smethod_0(Component component_0)
		{
			return component_0.transform;
		}

		internal static Quaternion smethod_1(Transform transform_0)
		{
			return transform_0.rotation;
		}

		internal static void smethod_2(Transform transform_0, Quaternion quaternion_0)
		{
			transform_0.rotation = quaternion_0;
		}
	}

	[CompilerGenerated]
	private sealed class lXNH19v4BsLbLbM9fwMd_0024cyDyhsKbg7kSoQR_0024jClcndiRofatoVpl45XaTKooU54tHhP0tiO9QPXwtfnMRxVXA6gUjybP6cVcdrATrUQFjDMHDD7KRx7qDH3ZOj_jEZA3Q
	{
		public Color k9jTQ33irMfqZyYWrqgwpFA;

		internal void WBmp9e0grcDN7yGo3GBZWWB_ikZN8g8R2sDiVJ11VI3B(AmMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F me)
		{
			JQpZ6lP9aBgIeq1zc9f_eT6euqKTWcd_R1Xq7K0V0oLZ.Bn_seeVFKvqZM_0024PkKfbal3s(new Vector3(smethod_2(smethod_1((Component)smethod_0())).x, smethod_2(smethod_1((Component)smethod_0())).y, smethod_2(smethod_1((Component)smethod_0())).z), me.transform.position, k9jTQ33irMfqZyYWrqgwpFA, AY6SJB6fI8W_fviAgyftZEKkU7ab64MZADLJQOSgmHhX(me.transform.position, new Vector3(0.01f, 0.01f, 0.01f)).x, bool_0: true);
		}

		internal static Camera smethod_0()
		{
			return Camera.main;
		}

		internal static Transform smethod_1(Component component_0)
		{
			return component_0.transform;
		}

		internal static Vector3 smethod_2(Transform transform_0)
		{
			return transform_0.forward;
		}
	}

	private static readonly int int_0 = 20;

	private static Action<Arena> action_0;

	private static Toggle k_lAL_0024BCTXRo8EuMHpiyyWo;

	private static GameObject _0024UF3Dx_F00ogmssHXYlvwcQ;

	private static c2wojWPyeJ90X8UxbVw8Ie7t2iSXT4W6AM5UctjLMkqq AZjgkDHxHA_hM7bKEiV7ES8;

	private static GameObject XVDZE4YUgxeonNUvpvABIb4;

	private static GameObject yHjr9GR2wCKklzOW2Xkl_Hi1ImsI52cDM857W_TH9pHz;

	private static GameObject RPPJ2nsw9RU2otM3BzDPzcY;

	private static ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ Q6uRAB_0024_S89ANxgzZ0QH8mk;

	private static ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ Xrd9z2_WYChPpqRSvNFZWiw;

	private static ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ eZaggCkfDTJE2dhuLSEvCSs;

	private static SliderController jq_0024gxTE9SdmPwMA9OoDUaD4;

	private static SliderController qSG8Li7AV2HL0xo0xYL2dhs;

	private static SliderController xPIozX5u7HFDqrFoaQvRcBc;

	private static SliderController oZphDbXncaFSC1_0024Uxg8QFMo;

	private static SliderController vKv0P28kAhNLiR7WXKJDGO4;

	private static SliderController sliderController_0;

	private static GameObject iTtUTzVJqoLove2Mlyfm4HQ;

	private static Control0 VEnLkFeX6Tle_M_0024bJO7QAfw;

	private static Control0 JuznlPC_0024iVvWRp5Gx3m19rQ;

	private static Control0 control0_0;

	private static Control0 UziXq1_0024N3sVQ1ybcZOKLrrk;

	private static Button BgzEAaH8KDjUX0HV0on_Qy8;

	private static Color color_0 = new Color(0f, 0f, 0f, 1f);

	private static Color zDsAAaOZvTBh2CeBs1I1rX5KlkZGnZx4276UUH5MmEQR = new Color(0f, 0f, 0f, 0.2f);

	private static List<xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA> fbSm64A_0024FzBvAjtith640zQ = new List<xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA>();

	private static List<c2wojWPyeJ90X8UxbVw8Ie7t2iSXT4W6AM5UctjLMkqq> fXmKrvqNNkqzpO5XpWPRVZI8_mbex_0024Y2G0UhxyZZLrx0 = new List<c2wojWPyeJ90X8UxbVw8Ie7t2iSXT4W6AM5UctjLMkqq>();

	private static Dictionary<PrimitiveData, GameObject> EedNaX9y9ColHoMDljdNd80izPNg_0024767fWYnDU3yANis = new Dictionary<PrimitiveData, GameObject>();

	private static GameObject ra5MAEm12eKHvW9LN8brPaE;

	private static ConstructData EmzpBqNhuUJvMBi03aDSG_w;

	private static bool vS40ylI3Hbm7uPsVJpFsna0 = false;

	private static string yEDvddoTlWFXT2Rd0jPleHQ = "";

	private static Transform cpZ_kSFtSLkQRO3C4PD7Nz4;

	private static Vector3 l6TwAdL5XExVQO7WyjsYgIc = Vector3.zero;

	private static vpxRY7V6xxVwrtWzUazGbAijYYgy_j9aOURndFg_MPmV1XpOIQGYmugcr08FohKkWZtGTI6r2M_fFifxmanSRrgmSgq6IJSK9HbxY_0024RtywzhAZtQnNleJNvuto3IE49fVw BFMkrNuUjUFB4B6P9DkaJ_s = vpxRY7V6xxVwrtWzUazGbAijYYgy_j9aOURndFg_MPmV1XpOIQGYmugcr08FohKkWZtGTI6r2M_fFifxmanSRrgmSgq6IJSK9HbxY_0024RtywzhAZtQnNleJNvuto3IE49fVw.move;

	private static bool pyLJQKsynReKRiJ14xze8DZN__0024ET1SK6Rm6XJMoZhPAE = false;

	private static bool z6J_8qxbU01ZXTzAmVrFIwygE3yzywLHjQ4ZzTsbQVxK = false;

	private static float LdFyfldU2s23FzaorWa_3xU = 45f;

	private static float UpRVTmvzmg5dcBLXM0coU8w = 1f;

	private static Color IdwU0_ARpD_wnWGSdVq151k = Color.white;

	private static bool iwaUWG_oqbQT6zAcJU5iwzU;

	private static Vector3 E4e53KKkkQc5_yliBk0AUfU;

	private static nnHuJswxvsz1pZEYGN_00241t_hRktuePQgaPa2hccgMZBGMarJabqUZUQ4omKAa5FU0eA jEMU6pSLJji8yTM1i2wQXNFdtjNYw3q1q7CFfiLrlsgP;

	private static Gradient Q90OdRkyhGNvujZeyO47oyPTjunqX_fS_bBOnFmBTJzx = null;

	private static GameObject WsaQ0Aqmeh9ZQLlVWABNM60;

	private static Dictionary<xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA, Vector3> nG3WvkfGAg1Lg2PMdQH_OZg = new Dictionary<xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA, Vector3>();

	private static Dictionary<xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA, Quaternion> ry9dPoTc05OsEAQnPpZ2_0024hA = new Dictionary<xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA, Quaternion>();

	private static Dictionary<xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA, Vector3> MlLIlV50uQCTKGEbiPTTnIk = new Dictionary<xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA, Vector3>();

	private static List<pQCv4GWEmO7suQD936qIBn22jIu63BZDtto8pweiUAZY> _0024CMex_0024vAX35hsObIY7ThtQI = new List<pQCv4GWEmO7suQD936qIBn22jIu63BZDtto8pweiUAZY>();

	private static Construct Construct_0 => (Construct)SceneMan.JFAOKFIDAGK;

	private static bool Boolean_0
	{
		get
		{
			if (smethod_2((UnityEngine.Object)k_lAL_0024BCTXRo8EuMHpiyyWo, (UnityEngine.Object)null))
			{
				k_lAL_0024BCTXRo8EuMHpiyyWo = smethod_3((SceneMan)Construct_0, global::_003CModule_003E.smethod_28<string>(2409947785u)).GetComponent<Toggle>();
			}
			if (smethod_4((UnityEngine.Object)k_lAL_0024BCTXRo8EuMHpiyyWo, (UnityEngine.Object)null))
			{
				return smethod_5(k_lAL_0024BCTXRo8EuMHpiyyWo);
			}
			return false;
		}
	}

	private static Vector3 nEQx4bSP4XevQkXYc14vvjtozH0DIbIMC6vTmSaFpKo7()
	{
		SystemData iGOBPLOLHEP = JKGKJLLFMLE.IGOBPLOLHEP;
		Vector3 zero = Vector3.zero;
		if (smethod_6(SystemData.EHLMFKOOHLI.Left))
		{
			zero.x -= 1f;
		}
		if (smethod_6(SystemData.EHLMFKOOHLI.Right))
		{
			zero.x += 1f;
		}
		if (smethod_6(SystemData.EHLMFKOOHLI.Down))
		{
			zero.y -= 1f;
		}
		if (smethod_6(SystemData.EHLMFKOOHLI.Up))
		{
			zero.y += 1f;
		}
		if (smethod_6(SystemData.EHLMFKOOHLI.Back))
		{
			zero.z -= 1f;
		}
		if (smethod_6(SystemData.EHLMFKOOHLI.Fore))
		{
			zero.z += 1f;
		}
		Vector3 dHPPLJMPAOB = smethod_7((CameraController)AJ95PT5qH7SI3aQp44bnfe9rXdzyiYcVJnP8p242egZvaWA5f7vGynYUg9dQSkzYlA.YZ_0024PBkn7rausEmKONRxFfNZQD6SNoR50G7r60U9WBOJ4, bool_0: false);
		dHPPLJMPAOB.y = 0f;
		dHPPLJMPAOB.Normalize();
		zero = BDLEJBBJJOI.CBNMHBPFKCE(Vector3.up, dHPPLJMPAOB).MultiplyVector(zero);
		float num = (float)iGOBPLOLHEP.moveRate * 0.01f + 0.2f;
		zero *= 100f * num * num * Time.deltaTime;
		if (HOCGCCAIPFF.AFLJECMLJDL(SystemData.EHLMFKOOHLI.Modifier))
		{
			zero *= 10f;
		}
		if (HOCGCCAIPFF.AFLJECMLJDL(SystemData.EHLMFKOOHLI.Reset))
		{
			zero *= 0.1f;
		}
		return zero;
	}

	private static GameObject jENAoM06PrbA0h0QBJoJeyw()
	{
		GameObject gameObject = smethod_8(global::_003CModule_003E.smethod_29<string>(706430559u));
		GameObject gameObject_ = smethod_9(PrimitiveType.Cube);
		smethod_11(smethod_10(gameObject_), smethod_10(gameObject));
		smethod_12(smethod_10(gameObject_), Vector3.zero);
		smethod_10(gameObject_).localScale = new Vector3(0.2f, 0.2f, 0.2f);
		GameObject gameObject2 = x2KzkW9JuGQXmzuCdgytKZhtDl_Q4F_kgfJY01G7qH2bwnaHFpw66hwDQa0zL5zVOw.o1Rvqe6BzXX706LinpRkiNE(10, 0f, 1f, 1f, 60f, bool_0: true, bool_1: true);
		gameObject2.transform.parent = gameObject.transform;
		gameObject2.transform.localPosition = Vector3.zero;
		gameObject2.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
		GameObject gameObject3 = x2KzkW9JuGQXmzuCdgytKZhtDl_Q4F_kgfJY01G7qH2bwnaHFpw66hwDQa0zL5zVOw.o1Rvqe6BzXX706LinpRkiNE(10, 0f, 1f, 1f, 60f, bool_0: true, bool_1: true);
		gameObject3.transform.parent = gameObject.transform;
		gameObject3.transform.localPosition = Vector3.zero;
		gameObject3.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
		gameObject3.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
		GameObject gameObject4 = x2KzkW9JuGQXmzuCdgytKZhtDl_Q4F_kgfJY01G7qH2bwnaHFpw66hwDQa0zL5zVOw.o1Rvqe6BzXX706LinpRkiNE(10, 0f, 1f, 1f, 60f, bool_0: true, bool_1: true);
		gameObject4.transform.parent = gameObject.transform;
		gameObject4.transform.localPosition = Vector3.zero;
		gameObject4.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
		gameObject4.transform.localRotation = Quaternion.Euler(-90f, 0f, 0f);
		Material material = new Material(Shader.Find(global::_003CModule_003E.smethod_29<string>(418851562u)))
		{
			color = new Color(0.16078432f, 29f / 85f, 0.8235294f)
		};
		Material material2 = new Material(Shader.Find(global::_003CModule_003E.smethod_26<string>(4255022278u)))
		{
			color = new Color(0.95686275f, 24f / 85f, 12f / 85f)
		};
		Material material3 = new Material(Shader.Find(global::_003CModule_003E.smethod_27<string>(1518713319u)))
		{
			color = new Color(0.5137255f, 0.9098039f, 0.23529412f)
		};
		gameObject2.GetComponent<MeshRenderer>().material = material;
		gameObject3.GetComponent<MeshRenderer>().material = material2;
		gameObject4.GetComponent<MeshRenderer>().material = material3;
		return gameObject;
	}

	internal static GameObject smethod_0(bool setupScripts)
	{
		GameObject gameObject = smethod_8(global::_003CModule_003E.smethod_27<string>(2966781096u));
		GameObject gameObject2 = jENAoM06PrbA0h0QBJoJeyw();
		smethod_11(smethod_10(gameObject2), smethod_10(gameObject));
		smethod_12(smethod_10(gameObject2), Vector3.zero);
		GameObject gameObject3 = smethod_8(global::_003CModule_003E.smethod_29<string>(3973616369u));
		smethod_11(smethod_10(gameObject3), smethod_10(gameObject));
		smethod_10(gameObject3).localPosition = new Vector3(0f, 0.04f, -1.25f);
		Camera camera = gameObject3.AddComponent<Camera>();
		camera.targetTexture = new RenderTexture(256, 256, 24);
		camera.clearFlags = CameraClearFlags.Color;
		GameObject gameObject4 = new GameObject(global::_003CModule_003E.smethod_27<string>(1035673541u));
		gameObject4.transform.parent = GameObject.Find(global::_003CModule_003E.smethod_27<string>(862174272u)).smethod_0(global::_003CModule_003E.smethod_29<string>(2479454016u)).transform;
		gameObject4.AddComponent<RawImage>().texture = camera.targetTexture;
		gameObject4.transform.localPosition = gameObject4.transform.parent.gameObject.smethod_0(global::_003CModule_003E.smethod_26<string>(4783377u)).transform.localPosition;
		gameObject.transform.position = new Vector3(1123f, 2456f, 3789f);
		if (setupScripts)
		{
			gameObject2.AddComponent<AmMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F>().U_0024eIoX6e3N9Ag_us_EcGHBI(delegate(AmMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F qdc)
			{
				YWMdQ1cueAvkZ2aXP8YuFyNoFjp9s1KOe_EpaOavmJdKNbtxf_0024jzoQoGkAusaugxvYsa06fgpQOmWvfHGTCAQ4c.smethod_2(YWMdQ1cueAvkZ2aXP8YuFyNoFjp9s1KOe_EpaOavmJdKNbtxf_0024jzoQoGkAusaugxvYsa06fgpQOmWvfHGTCAQ4c.smethod_0((Component)qdc), Quaternion.Inverse(YWMdQ1cueAvkZ2aXP8YuFyNoFjp9s1KOe_EpaOavmJdKNbtxf_0024jzoQoGkAusaugxvYsa06fgpQOmWvfHGTCAQ4c.smethod_1(YWMdQ1cueAvkZ2aXP8YuFyNoFjp9s1KOe_EpaOavmJdKNbtxf_0024jzoQoGkAusaugxvYsa06fgpQOmWvfHGTCAQ4c.smethod_0((Component)AJ95PT5qH7SI3aQp44bnfe9rXdzyiYcVJnP8p242egZvaWA5f7vGynYUg9dQSkzYlA.YZ_0024PBkn7rausEmKONRxFfNZQD6SNoR50G7r60U9WBOJ4))));
			});
		}
		return gameObject;
	}

	private static GameObject xM7Nbz4HRE3trwkiP8MNNfQ(string name, Color color, Quaternion rot)
	{
		GameObject gameObject = smethod_8(smethod_13(global::_003CModule_003E.smethod_25<string>(3874954841u), name));
		GameObject gameObject2 = smethod_9(PrimitiveType.Cylinder);
		smethod_11(smethod_10(gameObject2), smethod_10(gameObject));
		smethod_10(gameObject2).localPosition = new Vector3(0f, 0.5f, 0f);
		gameObject2.transform.localScale = new Vector3(0.1f, 0.5f, 0.1f);
		gameObject2.GetComponent<Collider>().isTrigger = true;
		gameObject2.layer = int_0;
		gameObject2.AddComponent<AmMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F>().U_0024eIoX6e3N9Ag_us_EcGHBI(delegate(AmMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F me)
		{
			JQpZ6lP9aBgIeq1zc9f_eT6euqKTWcd_R1Xq7K0V0oLZ.Bn_seeVFKvqZM_0024PkKfbal3s(new Vector3(lXNH19v4BsLbLbM9fwMd_0024cyDyhsKbg7kSoQR_0024jClcndiRofatoVpl45XaTKooU54tHhP0tiO9QPXwtfnMRxVXA6gUjybP6cVcdrATrUQFjDMHDD7KRx7qDH3ZOj_jEZA3Q.smethod_2(lXNH19v4BsLbLbM9fwMd_0024cyDyhsKbg7kSoQR_0024jClcndiRofatoVpl45XaTKooU54tHhP0tiO9QPXwtfnMRxVXA6gUjybP6cVcdrATrUQFjDMHDD7KRx7qDH3ZOj_jEZA3Q.smethod_1((Component)lXNH19v4BsLbLbM9fwMd_0024cyDyhsKbg7kSoQR_0024jClcndiRofatoVpl45XaTKooU54tHhP0tiO9QPXwtfnMRxVXA6gUjybP6cVcdrATrUQFjDMHDD7KRx7qDH3ZOj_jEZA3Q.smethod_0())).x, lXNH19v4BsLbLbM9fwMd_0024cyDyhsKbg7kSoQR_0024jClcndiRofatoVpl45XaTKooU54tHhP0tiO9QPXwtfnMRxVXA6gUjybP6cVcdrATrUQFjDMHDD7KRx7qDH3ZOj_jEZA3Q.smethod_2(lXNH19v4BsLbLbM9fwMd_0024cyDyhsKbg7kSoQR_0024jClcndiRofatoVpl45XaTKooU54tHhP0tiO9QPXwtfnMRxVXA6gUjybP6cVcdrATrUQFjDMHDD7KRx7qDH3ZOj_jEZA3Q.smethod_1((Component)lXNH19v4BsLbLbM9fwMd_0024cyDyhsKbg7kSoQR_0024jClcndiRofatoVpl45XaTKooU54tHhP0tiO9QPXwtfnMRxVXA6gUjybP6cVcdrATrUQFjDMHDD7KRx7qDH3ZOj_jEZA3Q.smethod_0())).y, lXNH19v4BsLbLbM9fwMd_0024cyDyhsKbg7kSoQR_0024jClcndiRofatoVpl45XaTKooU54tHhP0tiO9QPXwtfnMRxVXA6gUjybP6cVcdrATrUQFjDMHDD7KRx7qDH3ZOj_jEZA3Q.smethod_2(lXNH19v4BsLbLbM9fwMd_0024cyDyhsKbg7kSoQR_0024jClcndiRofatoVpl45XaTKooU54tHhP0tiO9QPXwtfnMRxVXA6gUjybP6cVcdrATrUQFjDMHDD7KRx7qDH3ZOj_jEZA3Q.smethod_1((Component)lXNH19v4BsLbLbM9fwMd_0024cyDyhsKbg7kSoQR_0024jClcndiRofatoVpl45XaTKooU54tHhP0tiO9QPXwtfnMRxVXA6gUjybP6cVcdrATrUQFjDMHDD7KRx7qDH3ZOj_jEZA3Q.smethod_0())).z), me.transform.position, color, AY6SJB6fI8W_fviAgyftZEKkU7ab64MZADLJQOSgmHhX(me.transform.position, new Vector3(0.01f, 0.01f, 0.01f)).x, bool_0: true);
		});
		GameObject gameObject3 = x2KzkW9JuGQXmzuCdgytKZhtDl_Q4F_kgfJY01G7qH2bwnaHFpw66hwDQa0zL5zVOw.o1Rvqe6BzXX706LinpRkiNE(10, 0f, 1f, 1f, 60f, bool_0: true, bool_1: true);
		gameObject3.transform.parent = gameObject.transform;
		gameObject3.transform.localPosition = new Vector3(0f, 1.2f, 0f);
		gameObject3.transform.localScale = new Vector3(0.2f, 0.2f, 0.38f);
		gameObject3.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
		gameObject3.layer = int_0;
		gameObject2.GetComponent<MeshRenderer>().material.color = color;
		gameObject3.GetComponent<MeshRenderer>().material = new Material(Shader.Find(global::_003CModule_003E.smethod_27<string>(1518713319u)));
		gameObject3.GetComponent<MeshRenderer>().material.color = color;
		GameObject gameObject4 = GameObject.CreatePrimitive(PrimitiveType.Cube);
		gameObject4.transform.parent = gameObject.transform;
		gameObject4.transform.localPosition = new Vector3(0f, 1f, 0f);
		gameObject4.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
		gameObject4.GetComponent<MeshRenderer>().material.color = color;
		gameObject4.transform.localRotation = Quaternion.identity;
		gameObject4.GetComponent<Collider>().isTrigger = true;
		gameObject4.layer = int_0;
		GameObject gameObject5 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		gameObject5.transform.parent = gameObject.transform;
		gameObject5.transform.localPosition = new Vector3(0f, 1f, 0f);
		gameObject5.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
		gameObject5.GetComponent<MeshRenderer>().material.color = color;
		gameObject5.transform.localRotation = Quaternion.identity;
		gameObject5.GetComponent<Collider>().isTrigger = true;
		gameObject5.layer = int_0;
		gameObject4.SetActive(value: false);
		gameObject5.SetActive(value: false);
		gameObject.transform.rotation = rot;
		gameObject2.transform.parent = null;
		gameObject3.transform.parent = null;
		gameObject4.transform.parent = null;
		gameObject5.transform.parent = null;
		gameObject.transform.rotation = Quaternion.identity;
		gameObject2.transform.parent = gameObject.transform;
		gameObject3.transform.parent = gameObject.transform;
		gameObject4.transform.parent = gameObject.transform;
		gameObject5.transform.parent = gameObject.transform;
		return gameObject;
	}

	private static Bounds HwpI_80ZJwnJXUb_00244d3CPk7TldbzmlfcEQ4_x5hopVfO()
	{
		if (fbSm64A_0024FzBvAjtith640zQ.Count == 0)
		{
			return new Bounds(Vector3.zero, Vector3.zero);
		}
		Bounds result = new Bounds(fbSm64A_0024FzBvAjtith640zQ[0].TIWjI8FsBk2nlZk9NO4HNOE.position, new Vector3(1f, 1f, 1f));
		foreach (xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA item in fbSm64A_0024FzBvAjtith640zQ)
		{
			Vector3[] vertices = item.TIWjI8FsBk2nlZk9NO4HNOE.GetComponent<MeshFilter>().mesh.vertices;
			foreach (Vector3 position in vertices)
			{
				result.Encapsulate(item.TIWjI8FsBk2nlZk9NO4HNOE.TransformPoint(position));
			}
		}
		return result;
	}

	private static void Xazqwq7g8mxiNVWJBFp6dYRNGLwSlCX_VVtqNx5140SG()
	{
		Q90OdRkyhGNvujZeyO47oyPTjunqX_fS_bBOnFmBTJzx = smethod_14();
		GradientColorKey[] array = new GradientColorKey[3];
		GradientAlphaKey[] array2 = new GradientAlphaKey[1];
		array[0].color = new Color(11f / 51f, 0.5254902f, 79f / 85f);
		array[0].time = 0f;
		array[1].color = new Color(13f / 85f, 32f / 85f, 57f / 85f);
		array[1].time = 0.5f;
		array[2].color = new Color(11f / 51f, 0.5254902f, 79f / 85f);
		array[2].time = 1f;
		array2[0].alpha = 1f;
		array2[0].time = 0f;
		Q90OdRkyhGNvujZeyO47oyPTjunqX_fS_bBOnFmBTJzx.SetKeys(array, array2);
	}

	private static void CGLANt8WtN7FK3KZMHNGJgw(Vector3 pos, Quaternion rot, Vector3 scale, vpxRY7V6xxVwrtWzUazGbAijYYgy_j9aOURndFg_MPmV1XpOIQGYmugcr08FohKkWZtGTI6r2M_fFifxmanSRrgmSgq6IJSK9HbxY_0024RtywzhAZtQnNleJNvuto3IE49fVw mode, bool scaleWithCamera)
	{
		smethod_15(smethod_10(WsaQ0Aqmeh9ZQLlVWABNM60), pos);
		smethod_16(smethod_10(WsaQ0Aqmeh9ZQLlVWABNM60), rot);
		smethod_17(smethod_10(WsaQ0Aqmeh9ZQLlVWABNM60), scale);
		for (int i = 0; i < WsaQ0Aqmeh9ZQLlVWABNM60.transform.childCount; i++)
		{
			smethod_18(smethod_10(WsaQ0Aqmeh9ZQLlVWABNM60), i).localScale = new Vector3(1f / scale.x, 1f / scale.y, 1f / scale.z);
		}
		for (int j = 0; j < WsaQ0Aqmeh9ZQLlVWABNM60.transform.childCount; j++)
		{
			for (int k = 0; k < WsaQ0Aqmeh9ZQLlVWABNM60.transform.GetChild(j).childCount; k++)
			{
				GameObject gameObject = WsaQ0Aqmeh9ZQLlVWABNM60.transform.GetChild(j).GetChild(k).gameObject;
				if (gameObject.name == global::_003CModule_003E.smethod_27<string>(1398714257u))
				{
					continue;
				}
				if (mode == vpxRY7V6xxVwrtWzUazGbAijYYgy_j9aOURndFg_MPmV1XpOIQGYmugcr08FohKkWZtGTI6r2M_fFifxmanSRrgmSgq6IJSK9HbxY_0024RtywzhAZtQnNleJNvuto3IE49fVw.move && gameObject.name == global::_003CModule_003E.smethod_26<string>(2188845182u))
				{
					gameObject.SetActive(value: true);
					continue;
				}
				gameObject.SetActive(value: false);
				if (mode != vpxRY7V6xxVwrtWzUazGbAijYYgy_j9aOURndFg_MPmV1XpOIQGYmugcr08FohKkWZtGTI6r2M_fFifxmanSRrgmSgq6IJSK9HbxY_0024RtywzhAZtQnNleJNvuto3IE49fVw.rotate || !(gameObject.name == global::_003CModule_003E.smethod_29<string>(3831733543u)))
				{
					gameObject.SetActive(value: false);
					if (mode != vpxRY7V6xxVwrtWzUazGbAijYYgy_j9aOURndFg_MPmV1XpOIQGYmugcr08FohKkWZtGTI6r2M_fFifxmanSRrgmSgq6IJSK9HbxY_0024RtywzhAZtQnNleJNvuto3IE49fVw.scale || !(gameObject.name == global::_003CModule_003E.smethod_25<string>(1945954209u)))
					{
						gameObject.SetActive(value: false);
					}
					else
					{
						gameObject.SetActive(value: true);
					}
				}
				else
				{
					gameObject.SetActive(value: true);
				}
			}
		}
		if (scaleWithCamera)
		{
			Plane plane = new Plane(Camera.main.transform.forward, Camera.main.transform.position);
			for (int l = 0; l < WsaQ0Aqmeh9ZQLlVWABNM60.transform.childCount; l++)
			{
				WsaQ0Aqmeh9ZQLlVWABNM60.transform.GetChild(l).localScale = AY6SJB6fI8W_fviAgyftZEKkU7ab64MZADLJQOSgmHhX(WsaQ0Aqmeh9ZQLlVWABNM60.transform.GetChild(l).position, WsaQ0Aqmeh9ZQLlVWABNM60.transform.GetChild(l).localScale, plane, 7f);
			}
		}
	}

	private static Vector3 AY6SJB6fI8W_fviAgyftZEKkU7ab64MZADLJQOSgmHhX(Vector3 pos, Vector3 scale, float divisor = 1f)
	{
		return AY6SJB6fI8W_fviAgyftZEKkU7ab64MZADLJQOSgmHhX(pos, scale, new Plane(smethod_21(smethod_20((Component)smethod_19())), smethod_22(smethod_20((Component)smethod_19()))), divisor);
	}

	private static Vector3 AY6SJB6fI8W_fviAgyftZEKkU7ab64MZADLJQOSgmHhX(Vector3 pos, Vector3 scale, Plane plane, float divisor = 1f)
	{
		float distanceToPoint = plane.GetDistanceToPoint(pos);
		return scale *= distanceToPoint / divisor;
	}

	private static void oo_JlRDjpbsImWqzKiYAW2Lar_00247fgYlrUzew9ABs11el(Arena arena)
	{
		if (action_0 == null)
		{
			MethodInfo methodInfo_ = method_0(smethod_23(typeof(Arena).TypeHandle), global::_003CModule_003E.smethod_27<string>(472534014u), AccessTools.all);
			DynamicMethod dynamicMethod_ = smethod_24(global::_003CModule_003E.smethod_27<string>(2078095929u), (Type)null, new Type[1] { smethod_23(typeof(Arena).TypeHandle) }, smethod_23(typeof(Arena).TypeHandle));
			ILGenerator ilgenerator_ = smethod_25(dynamicMethod_);
			smethod_26(ilgenerator_, OpCodes.Ldarg_0);
			smethod_27(ilgenerator_, OpCodes.Call, methodInfo_);
			smethod_26(ilgenerator_, OpCodes.Ret);
			action_0 = (Action<Arena>)smethod_28(dynamicMethod_, smethod_23(typeof(Action<Arena>).TypeHandle));
		}
		action_0(arena);
	}

	private static RaycastHit[] FV8xGHqtg_wVkgsNgdvcQU4gbPt56Nyt_3knt_RdxcuK(float maxDistance)
	{
		return (from h in smethod_31(smethod_30(smethod_19(), smethod_29()), maxDistance)
			orderby h.distance
			select h).ToArray();
	}

	private static Vector3 Ycv8NzDyer8S_j_0024qIIEeExGtGcSZnc0IwQTxmy_Pd_0024co(string axis)
	{
		Vector3 result = Vector3.zero;
		if (BFMkrNuUjUFB4B6P9DkaJ_s != vpxRY7V6xxVwrtWzUazGbAijYYgy_j9aOURndFg_MPmV1XpOIQGYmugcr08FohKkWZtGTI6r2M_fFifxmanSRrgmSgq6IJSK9HbxY_0024RtywzhAZtQnNleJNvuto3IE49fVw.move && BFMkrNuUjUFB4B6P9DkaJ_s != vpxRY7V6xxVwrtWzUazGbAijYYgy_j9aOURndFg_MPmV1XpOIQGYmugcr08FohKkWZtGTI6r2M_fFifxmanSRrgmSgq6IJSK9HbxY_0024RtywzhAZtQnNleJNvuto3IE49fVw.scale)
		{
			if (BFMkrNuUjUFB4B6P9DkaJ_s == vpxRY7V6xxVwrtWzUazGbAijYYgy_j9aOURndFg_MPmV1XpOIQGYmugcr08FohKkWZtGTI6r2M_fFifxmanSRrgmSgq6IJSK9HbxY_0024RtywzhAZtQnNleJNvuto3IE49fVw.rotate)
			{
				if (!(axis == global::_003CModule_003E.smethod_25<string>(808255003u)))
				{
					if (axis == global::_003CModule_003E.smethod_27<string>(4055734159u))
					{
						result = new Vector3(1f, 0f, 0f);
					}
					else if (axis == global::_003CModule_003E.smethod_29<string>(2663295368u))
					{
						result = new Vector3(0f, 1f, 0f);
					}
				}
				else
				{
					result = new Vector3(0f, 0f, 1f);
				}
			}
		}
		else
		{
			result = (smethod_32(axis, global::_003CModule_003E.smethod_28<string>(148454002u)) ? Vector3.up : new Vector3(0f, 0f, 1f));
		}
		return result;
	}

	private static Vector3 sq3LR75Glc8ZmNWPHc9AJN1oe5yzJWHlY9OPLFStJJO_(Vector3 planePos, Vector3 normal)
	{
		return g_0024KAi_yupqFwYe3oSCI5EkJReFgTbu_S1Z5m31EVW6Mx(smethod_29(), planePos, normal);
	}

	private static Vector3 g_0024KAi_yupqFwYe3oSCI5EkJReFgTbu_S1Z5m31EVW6Mx(Vector3 pos, Vector3 planePos, Vector3 normal)
	{
		Plane plane = new Plane(normal, planePos);
		Ray ray = smethod_30(smethod_19(), pos);
		float enter = 0f;
		Vector3 result = Vector3.zero;
		plane.Raycast(ray, out enter);
		if (enter != 0f)
		{
			result = ray.GetPoint(enter);
		}
		return result;
	}

	private static void AVAPdtEu8hPA7spgVRbhsrY()
	{
		nG3WvkfGAg1Lg2PMdQH_OZg.Clear();
		ry9dPoTc05OsEAQnPpZ2_0024hA.Clear();
		MlLIlV50uQCTKGEbiPTTnIk.Clear();
		foreach (xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA item in fbSm64A_0024FzBvAjtith640zQ)
		{
			nG3WvkfGAg1Lg2PMdQH_OZg.Add(item, smethod_22(item.TIWjI8FsBk2nlZk9NO4HNOE));
			ry9dPoTc05OsEAQnPpZ2_0024hA.Add(item, smethod_33(item.TIWjI8FsBk2nlZk9NO4HNOE));
			if (item.MrZDDetpveRMT__0024biC7h8tU != xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA.Enum0.gate)
			{
				MlLIlV50uQCTKGEbiPTTnIk.Add(item, smethod_34(item.TIWjI8FsBk2nlZk9NO4HNOE));
			}
			else
			{
				MlLIlV50uQCTKGEbiPTTnIk.Add(item, smethod_34(item.TIWjI8FsBk2nlZk9NO4HNOE));
			}
		}
		if (BFMkrNuUjUFB4B6P9DkaJ_s != vpxRY7V6xxVwrtWzUazGbAijYYgy_j9aOURndFg_MPmV1XpOIQGYmugcr08FohKkWZtGTI6r2M_fFifxmanSRrgmSgq6IJSK9HbxY_0024RtywzhAZtQnNleJNvuto3IE49fVw.scale)
		{
			foreach (xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA item2 in fbSm64A_0024FzBvAjtith640zQ)
			{
				smethod_11(item2.TIWjI8FsBk2nlZk9NO4HNOE, smethod_10(ra5MAEm12eKHvW9LN8brPaE));
			}
			return;
		}
		foreach (xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA item3 in fbSm64A_0024FzBvAjtith640zQ)
		{
			if (item3.MrZDDetpveRMT__0024biC7h8tU != xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA.Enum0.gate)
			{
				smethod_11(item3.TIWjI8FsBk2nlZk9NO4HNOE, smethod_10(ra5MAEm12eKHvW9LN8brPaE));
			}
			else
			{
				smethod_11(smethod_10(item3.cRC1fFhZVgamcERb3o0WavI), smethod_10(ra5MAEm12eKHvW9LN8brPaE));
			}
		}
	}

	private static void CpTvMSD2oogKxeVlGQN_0024uksze27FqWgbw6JiV7rT2qXI(bool doUndoStuff = true)
	{
		GameObject gameObject_ = smethod_35(global::_003CModule_003E.smethod_29<string>(889045774u));
		foreach (xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA item in fbSm64A_0024FzBvAjtith640zQ)
		{
			if (item.MrZDDetpveRMT__0024biC7h8tU != xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA.Enum0.block)
			{
				if (item.MrZDDetpveRMT__0024biC7h8tU == xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA.Enum0.gate)
				{
					if (BFMkrNuUjUFB4B6P9DkaJ_s != vpxRY7V6xxVwrtWzUazGbAijYYgy_j9aOURndFg_MPmV1XpOIQGYmugcr08FohKkWZtGTI6r2M_fFifxmanSRrgmSgq6IJSK9HbxY_0024RtywzhAZtQnNleJNvuto3IE49fVw.scale)
					{
						smethod_11(item.TIWjI8FsBk2nlZk9NO4HNOE, (Transform)null);
					}
					else
					{
						smethod_11(smethod_10(item.cRC1fFhZVgamcERb3o0WavI), item.TIWjI8FsBk2nlZk9NO4HNOE);
					}
				}
				else
				{
					smethod_11(item.TIWjI8FsBk2nlZk9NO4HNOE, (Transform)null);
				}
			}
			else
			{
				smethod_11(item.TIWjI8FsBk2nlZk9NO4HNOE, smethod_10(gameObject_));
			}
			item.method_0();
		}
		if (!doUndoStuff)
		{
			return;
		}
		List<GameObject> list = new List<GameObject>();
		List<Vector3> list2 = new List<Vector3>();
		List<Vector3> list3 = new List<Vector3>();
		List<Quaternion> list4 = new List<Quaternion>();
		List<Quaternion> list5 = new List<Quaternion>();
		List<Vector3> list6 = new List<Vector3>();
		List<Vector3> list7 = new List<Vector3>();
		foreach (xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA key in nG3WvkfGAg1Lg2PMdQH_OZg.Keys)
		{
			list.Add(key.eWUzF3zpMMjP5r9PB6rj474);
			list2.Add(nG3WvkfGAg1Lg2PMdQH_OZg[key]);
			list4.Add(ry9dPoTc05OsEAQnPpZ2_0024hA[key]);
			list6.Add(MlLIlV50uQCTKGEbiPTTnIk[key]);
			list3.Add(smethod_22(key.TIWjI8FsBk2nlZk9NO4HNOE));
			list5.Add(smethod_33(key.TIWjI8FsBk2nlZk9NO4HNOE));
			if (key.MrZDDetpveRMT__0024biC7h8tU == xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA.Enum0.gate)
			{
				list7.Add(smethod_34(smethod_10(key.cRC1fFhZVgamcERb3o0WavI)));
			}
			else
			{
				list7.Add(smethod_34(key.TIWjI8FsBk2nlZk9NO4HNOE));
			}
		}
		XBS7OSIVgwiqp2YsikrDtq6zcM87cw3nXOw4Pre7ULHrFpjhoDlZmc_0024_dixBV9c1sQ xBS7OSIVgwiqp2YsikrDtq6zcM87cw3nXOw4Pre7ULHrFpjhoDlZmc_0024_dixBV9c1sQ = new XBS7OSIVgwiqp2YsikrDtq6zcM87cw3nXOw4Pre7ULHrFpjhoDlZmc_0024_dixBV9c1sQ(list.ToArray(), list2.ToArray(), list4.ToArray(), list3.ToArray(), list5.ToArray(), list6.ToArray(), list7.ToArray());
		xBS7OSIVgwiqp2YsikrDtq6zcM87cw3nXOw4Pre7ULHrFpjhoDlZmc_0024_dixBV9c1sQ.bv4xsECkipN_002441Wa7mcfeqY();
		_0024CMex_0024vAX35hsObIY7ThtQI.Add(xBS7OSIVgwiqp2YsikrDtq6zcM87cw3nXOw4Pre7ULHrFpjhoDlZmc_0024_dixBV9c1sQ);
	}

	internal static void smethod_1(Transform arw, Bounds selectionBounds)
	{
		string text = (smethod_36(yEDvddoTlWFXT2Rd0jPleHQ) ? smethod_38(smethod_37((UnityEngine.Object)arw), 4) : yEDvddoTlWFXT2Rd0jPleHQ);
		Color color = (smethod_39(text, global::_003CModule_003E.smethod_27<string>(3283291137u)) ? Color.red : (smethod_39(text, global::_003CModule_003E.smethod_29<string>(1916827260u)) ? Color.green : Color.blue));
		bool flag = fbSm64A_0024FzBvAjtith640zQ.Count == 1 && vS40ylI3Hbm7uPsVJpFsna0 && (BFMkrNuUjUFB4B6P9DkaJ_s == vpxRY7V6xxVwrtWzUazGbAijYYgy_j9aOURndFg_MPmV1XpOIQGYmugcr08FohKkWZtGTI6r2M_fFifxmanSRrgmSgq6IJSK9HbxY_0024RtywzhAZtQnNleJNvuto3IE49fVw.move || BFMkrNuUjUFB4B6P9DkaJ_s == vpxRY7V6xxVwrtWzUazGbAijYYgy_j9aOURndFg_MPmV1XpOIQGYmugcr08FohKkWZtGTI6r2M_fFifxmanSRrgmSgq6IJSK9HbxY_0024RtywzhAZtQnNleJNvuto3IE49fVw.scale);
		bool flag2 = smethod_40(smethod_38(smethod_37((UnityEngine.Object)cpZ_kSFtSLkQRO3C4PD7Nz4), 3), 0) == '-';
		if (!smethod_41(0))
		{
			if (!smethod_42(0))
			{
				if (pyLJQKsynReKRiJ14xze8DZN__0024ET1SK6Rm6XJMoZhPAE)
				{
					CpTvMSD2oogKxeVlGQN_0024uksze27FqWgbw6JiV7rT2qXI();
				}
				pyLJQKsynReKRiJ14xze8DZN__0024ET1SK6Rm6XJMoZhPAE = false;
			}
		}
		else
		{
			AVAPdtEu8hPA7spgVRbhsrY();
			pyLJQKsynReKRiJ14xze8DZN__0024ET1SK6Rm6XJMoZhPAE = true;
		}
		if (smethod_32(yEDvddoTlWFXT2Rd0jPleHQ, text))
		{
			yEDvddoTlWFXT2Rd0jPleHQ = text;
		}
		Vector3 vector = ((!flag) ? Ycv8NzDyer8S_j_0024qIIEeExGtGcSZnc0IwQTxmy_Pd_0024co(yEDvddoTlWFXT2Rd0jPleHQ) : H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.oE0iISLu17q5U2guufpMSPD2VEAJN4_0024d9BQV5BVasnRT(Ycv8NzDyer8S_j_0024qIIEeExGtGcSZnc0IwQTxmy_Pd_0024co(yEDvddoTlWFXT2Rd0jPleHQ), Vector3.zero, smethod_33(fbSm64A_0024FzBvAjtith640zQ[0].TIWjI8FsBk2nlZk9NO4HNOE)));
		JQpZ6lP9aBgIeq1zc9f_eT6euqKTWcd_R1Xq7K0V0oLZ.Bn_seeVFKvqZM_0024PkKfbal3s(vector, smethod_22(smethod_10(ra5MAEm12eKHvW9LN8brPaE)), new Color(color.r, color.g, color.b, 0.1f), 5f);
		Vector3 worldPosition = sq3LR75Glc8ZmNWPHc9AJN1oe5yzJWHlY9OPLFStJJO_(arw.position, vector);
		if (!Input.GetMouseButton(0))
		{
			return;
		}
		Vector3 vector2 = Vector3.one;
		string text2 = yEDvddoTlWFXT2Rd0jPleHQ;
		if (text2 == global::_003CModule_003E.smethod_27<string>(3283291137u))
		{
			vector2 = new Vector3(1f, 0f, 0f);
		}
		else if (!(text2 == global::_003CModule_003E.smethod_25<string>(808255003u)))
		{
			if (text2 == global::_003CModule_003E.smethod_27<string>(4055734159u))
			{
				vector2 = new Vector3(0f, 0f, 1f);
			}
		}
		else
		{
			vector2 = new Vector3(0f, 1f, 0f);
		}
		if (iwaUWG_oqbQT6zAcJU5iwzU)
		{
			selectionBounds.center = E4e53KKkkQc5_yliBk0AUfU;
			selectionBounds.size = Vector3.one;
		}
		if (BFMkrNuUjUFB4B6P9DkaJ_s == vpxRY7V6xxVwrtWzUazGbAijYYgy_j9aOURndFg_MPmV1XpOIQGYmugcr08FohKkWZtGTI6r2M_fFifxmanSRrgmSgq6IJSK9HbxY_0024RtywzhAZtQnNleJNvuto3IE49fVw.move)
		{
			Vector3 zero = Vector3.zero;
			Vector3 a = (flag ? (fbSm64A_0024FzBvAjtith640zQ[0].TIWjI8FsBk2nlZk9NO4HNOE.lossyScale / ((!flag2) ? 2 : (-2))) : (selectionBounds.size / ((!flag2) ? 2 : (-2))));
			Vector3 vector3;
			Vector3 b;
			if (!flag)
			{
				vector3 = jEMU6pSLJji8yTM1i2wQXNFdtjNYw3q1q7CFfiLrlsgP.vector3_0 + Vector3.Scale(a, vector2);
				b = vector2;
			}
			else
			{
				b = fbSm64A_0024FzBvAjtith640zQ[0].TIWjI8FsBk2nlZk9NO4HNOE.TransformDirection(vector2);
				vector3 = fbSm64A_0024FzBvAjtith640zQ[0].TIWjI8FsBk2nlZk9NO4HNOE.position + Vector3.Scale(a, b);
			}
			zero = new Vector3(worldPosition.x - (vector3 + l6TwAdL5XExVQO7WyjsYgIc).x, worldPosition.y - (vector3 + l6TwAdL5XExVQO7WyjsYgIc).y, worldPosition.z - (vector3 + l6TwAdL5XExVQO7WyjsYgIc).z);
			zero = Vector3.Scale(zero, vector2);
			JQpZ6lP9aBgIeq1zc9f_eT6euqKTWcd_R1Xq7K0V0oLZ.IkbypDCNwbjaXcD3TQc7JBk(color, vector3 + new Vector3(100f * b.x, 100f * b.y, 100f * b.z), vector3 - new Vector3(100f * b.x, 100f * b.y, 100f * b.z));
			if (!flag)
			{
				jEMU6pSLJji8yTM1i2wQXNFdtjNYw3q1q7CFfiLrlsgP.vector3_0 += zero;
				if (!AJ95PT5qH7SI3aQp44bnfe9rXdzyiYcVJnP8p242egZvaWA5f7vGynYUg9dQSkzYlA.c_ykavSafjWcUsCpDGjxUH4)
				{
					ra5MAEm12eKHvW9LN8brPaE.transform.position = jEMU6pSLJji8yTM1i2wQXNFdtjNYw3q1q7CFfiLrlsgP.vector3_0;
				}
				else
				{
					ra5MAEm12eKHvW9LN8brPaE.transform.position = new Vector3(Mathf.Floor(jEMU6pSLJji8yTM1i2wQXNFdtjNYw3q1q7CFfiLrlsgP.vector3_0.x), Mathf.Floor(jEMU6pSLJji8yTM1i2wQXNFdtjNYw3q1q7CFfiLrlsgP.vector3_0.y), Mathf.Floor(jEMU6pSLJji8yTM1i2wQXNFdtjNYw3q1q7CFfiLrlsgP.vector3_0.z));
				}
			}
			else
			{
				ra5MAEm12eKHvW9LN8brPaE.transform.Translate(zero, fbSm64A_0024FzBvAjtith640zQ[0].TIWjI8FsBk2nlZk9NO4HNOE);
			}
			if (iwaUWG_oqbQT6zAcJU5iwzU)
			{
				E4e53KKkkQc5_yliBk0AUfU += zero;
			}
		}
		else if (BFMkrNuUjUFB4B6P9DkaJ_s == vpxRY7V6xxVwrtWzUazGbAijYYgy_j9aOURndFg_MPmV1XpOIQGYmugcr08FohKkWZtGTI6r2M_fFifxmanSRrgmSgq6IJSK9HbxY_0024RtywzhAZtQnNleJNvuto3IE49fVw.rotate)
		{
			ra5MAEm12eKHvW9LN8brPaE.transform.LookAt(worldPosition, vector);
			if (!AJ95PT5qH7SI3aQp44bnfe9rXdzyiYcVJnP8p242egZvaWA5f7vGynYUg9dQSkzYlA.c_ykavSafjWcUsCpDGjxUH4)
			{
				return;
			}
			Vector3 eulerAngles = ra5MAEm12eKHvW9LN8brPaE.transform.rotation.eulerAngles;
			text2 = yEDvddoTlWFXT2Rd0jPleHQ;
			if (!(text2 == global::_003CModule_003E.smethod_26<string>(2173098792u)))
			{
				if (text2 == global::_003CModule_003E.smethod_25<string>(808255003u) || text2 == global::_003CModule_003E.smethod_28<string>(4261301496u))
				{
					eulerAngles.x = (float)(Math.Round(eulerAngles.x / LdFyfldU2s23FzaorWa_3xU) * (double)LdFyfldU2s23FzaorWa_3xU);
				}
			}
			else
			{
				eulerAngles.y = (float)(Math.Round(eulerAngles.y / LdFyfldU2s23FzaorWa_3xU) * (double)LdFyfldU2s23FzaorWa_3xU);
			}
			if (eulerAngles.y > 89.9f)
			{
				eulerAngles.y = 89.9f;
			}
			ra5MAEm12eKHvW9LN8brPaE.transform.rotation = Quaternion.Euler(eulerAngles);
		}
		else
		{
			if (BFMkrNuUjUFB4B6P9DkaJ_s != vpxRY7V6xxVwrtWzUazGbAijYYgy_j9aOURndFg_MPmV1XpOIQGYmugcr08FohKkWZtGTI6r2M_fFifxmanSRrgmSgq6IJSK9HbxY_0024RtywzhAZtQnNleJNvuto3IE49fVw.scale)
			{
				return;
			}
			Vector3 zero2 = Vector3.zero;
			Vector3 vector4 = jEMU6pSLJji8yTM1i2wQXNFdtjNYw3q1q7CFfiLrlsgP._0024W57KyA48Amwd1O4QGSgAGM.center + jEMU6pSLJji8yTM1i2wQXNFdtjNYw3q1q7CFfiLrlsgP._0024W57KyA48Amwd1O4QGSgAGM.size / 2f;
			Vector3 vector5 = Vector3.zero;
			if (!(text == global::_003CModule_003E.smethod_29<string>(2663295368u)))
			{
				if (text == global::_003CModule_003E.smethod_27<string>(533209885u))
				{
					JQpZ6lP9aBgIeq1zc9f_eT6euqKTWcd_R1Xq7K0V0oLZ.IkbypDCNwbjaXcD3TQc7JBk(Color.green, arw.transform.position - new Vector3(0f, 100f, 0f), arw.transform.position + new Vector3(0f, 100f, 0f));
					zero2 = new Vector3(0f, worldPosition.y - (vector4 + l6TwAdL5XExVQO7WyjsYgIc).y, 0f);
					vector5 = new Vector3(1f, (jEMU6pSLJji8yTM1i2wQXNFdtjNYw3q1q7CFfiLrlsgP._0024W57KyA48Amwd1O4QGSgAGM.size.y + zero2.y * 2f) / jEMU6pSLJji8yTM1i2wQXNFdtjNYw3q1q7CFfiLrlsgP._0024W57KyA48Amwd1O4QGSgAGM.size.y, 1f);
				}
				else if (text == global::_003CModule_003E.smethod_29<string>(142577666u))
				{
					JQpZ6lP9aBgIeq1zc9f_eT6euqKTWcd_R1Xq7K0V0oLZ.IkbypDCNwbjaXcD3TQc7JBk(Color.blue, arw.transform.position - new Vector3(0f, 0f, 100f), arw.transform.position + new Vector3(0f, 0f, 100f));
					zero2 = new Vector3(0f, 0f, worldPosition.z - (vector4 + l6TwAdL5XExVQO7WyjsYgIc).z);
					vector5 = new Vector3(1f, 1f, (jEMU6pSLJji8yTM1i2wQXNFdtjNYw3q1q7CFfiLrlsgP._0024W57KyA48Amwd1O4QGSgAGM.size.z + zero2.z * 2f) / jEMU6pSLJji8yTM1i2wQXNFdtjNYw3q1q7CFfiLrlsgP._0024W57KyA48Amwd1O4QGSgAGM.size.z);
				}
			}
			else
			{
				JQpZ6lP9aBgIeq1zc9f_eT6euqKTWcd_R1Xq7K0V0oLZ.IkbypDCNwbjaXcD3TQc7JBk(Color.red, arw.transform.position - new Vector3(100f, 0f, 0f), arw.transform.position + new Vector3(100f, 0f, 0f));
				zero2 = new Vector3(worldPosition.x - (vector4 + l6TwAdL5XExVQO7WyjsYgIc).x, 0f, 0f);
				vector5 = new Vector3((jEMU6pSLJji8yTM1i2wQXNFdtjNYw3q1q7CFfiLrlsgP._0024W57KyA48Amwd1O4QGSgAGM.size.x + zero2.x * 2f) / jEMU6pSLJji8yTM1i2wQXNFdtjNYw3q1q7CFfiLrlsgP._0024W57KyA48Amwd1O4QGSgAGM.size.x, 1f, 1f);
			}
			bool flag3 = false;
			if (vector5.x < 0f)
			{
				vector5.x *= -1f;
				flag3 = true;
			}
			else if (vector5.y < 0f)
			{
				vector5.y *= -1f;
				flag3 = true;
			}
			else if (vector5.z < 0f)
			{
				vector5.z *= -1f;
				flag3 = true;
			}
			Vector3 vector6 = Vector3.Scale(fbSm64A_0024FzBvAjtith640zQ[0].TIWjI8FsBk2nlZk9NO4HNOE.localScale, Vector3.Scale(jEMU6pSLJji8yTM1i2wQXNFdtjNYw3q1q7CFfiLrlsgP.vector3_1, vector5));
			if (fbSm64A_0024FzBvAjtith640zQ[0].MrZDDetpveRMT__0024biC7h8tU == xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA.Enum0.block)
			{
				if (vector6.x < 1f || vector6.x > 255f || vector6.y < 1f || vector6.y > 255f || vector6.z < 1f || vector6.z > 255f)
				{
					return;
				}
			}
			else if (fbSm64A_0024FzBvAjtith640zQ[0].MrZDDetpveRMT__0024biC7h8tU == xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA.Enum0.gate && (vector6.x < 10f || vector6.x > 1000f || vector6.y < 10f || vector6.y > 1000f || vector6.z < 0f || !(vector6.z <= 0f)))
			{
				return;
			}
			if (!Input.GetMouseButtonDown(0))
			{
				jEMU6pSLJji8yTM1i2wQXNFdtjNYw3q1q7CFfiLrlsgP.YiFDCDp4BaBGy7SmGWZz5fI(vector5);
			}
			jEMU6pSLJji8yTM1i2wQXNFdtjNYw3q1q7CFfiLrlsgP.nkbNIqZJHBYXfxJlhsRemu8(vector5);
			Vector3 vector7 = jEMU6pSLJji8yTM1i2wQXNFdtjNYw3q1q7CFfiLrlsgP.I9Td_Z2HQmtjkbxZZDo38rg - ra5MAEm12eKHvW9LN8brPaE.transform.localScale;
			ra5MAEm12eKHvW9LN8brPaE.transform.localScale = jEMU6pSLJji8yTM1i2wQXNFdtjNYw3q1q7CFfiLrlsgP.I9Td_Z2HQmtjkbxZZDo38rg;
			if (fbSm64A_0024FzBvAjtith640zQ[0].MrZDDetpveRMT__0024biC7h8tU == xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA.Enum0.gate)
			{
				return;
			}
			if (text == global::_003CModule_003E.smethod_25<string>(2209721351u))
			{
				ra5MAEm12eKHvW9LN8brPaE.transform.Translate(vector7.x / ((!flag3) ? 2f : (-2f)), 0f, 0f);
			}
			else if (!(text == global::_003CModule_003E.smethod_28<string>(148454002u)))
			{
				if (text == global::_003CModule_003E.smethod_29<string>(142577666u))
				{
					ra5MAEm12eKHvW9LN8brPaE.transform.Translate(0f, 0f, vector7.z / ((!flag3) ? 2f : (-2f)));
				}
			}
			else
			{
				ra5MAEm12eKHvW9LN8brPaE.transform.Translate(0f, vector7.y / ((!flag3) ? 2f : (-2f)), 0f);
			}
		}
	}

	private static void bm4qNpKN8ThlleNq_0024v_0024DpHZBJQSkCOXr8EFjC57_0024ief7(Vector3 pos)
	{
		foreach (xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA item in fbSm64A_0024FzBvAjtith640zQ)
		{
			smethod_11(item.TIWjI8FsBk2nlZk9NO4HNOE, smethod_10(ra5MAEm12eKHvW9LN8brPaE));
		}
		smethod_15(smethod_10(ra5MAEm12eKHvW9LN8brPaE), pos);
		CpTvMSD2oogKxeVlGQN_0024uksze27FqWgbw6JiV7rT2qXI(doUndoStuff: false);
	}

	private static bool rWJdVfn9_tn1f5vskxsQRZc(GameObject p)
	{
		foreach (xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA item in fbSm64A_0024FzBvAjtith640zQ)
		{
			if (smethod_2((UnityEngine.Object)item.eWUzF3zpMMjP5r9PB6rj474, (UnityEngine.Object)p))
			{
				return true;
			}
		}
		return false;
	}

	private static bool rWJdVfn9_tn1f5vskxsQRZc(PrimitiveController p)
	{
		foreach (xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA item in fbSm64A_0024FzBvAjtith640zQ)
		{
			if (item.uZ_quI6GyZaDBH7Zba2IGp0(p))
			{
				return true;
			}
		}
		return false;
	}

	private static bool rWJdVfn9_tn1f5vskxsQRZc(PointController p)
	{
		foreach (xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA item in fbSm64A_0024FzBvAjtith640zQ)
		{
			if (item.uZ_quI6GyZaDBH7Zba2IGp0(p))
			{
				return true;
			}
		}
		return false;
	}

	private static void PpujdlCy9tJcYzwufFpUFBnU01R0tOIkliTqZaUnW2Xv(pQCv4GWEmO7suQD936qIBn22jIu63BZDtto8pweiUAZY item)
	{
		_0024CMex_0024vAX35hsObIY7ThtQI.Add(item);
		if (_0024CMex_0024vAX35hsObIY7ThtQI.Count > JKGKJLLFMLE.IGOBPLOLHEP.undoBufferSize)
		{
			_0024CMex_0024vAX35hsObIY7ThtQI.RemoveAt(0);
		}
	}

	private static pQCv4GWEmO7suQD936qIBn22jIu63BZDtto8pweiUAZY _X2PQ1Ly_CMNLsRnRNN_vybYUDyvl1x6gIuOELMr0x7x()
	{
		if (_0024CMex_0024vAX35hsObIY7ThtQI.Count == 0)
		{
			return null;
		}
		pQCv4GWEmO7suQD936qIBn22jIu63BZDtto8pweiUAZY result = _0024CMex_0024vAX35hsObIY7ThtQI[_0024CMex_0024vAX35hsObIY7ThtQI.Count - 1];
		_0024CMex_0024vAX35hsObIY7ThtQI.RemoveAt(_0024CMex_0024vAX35hsObIY7ThtQI.Count - 1);
		return result;
	}

	private static void XU_0024J1o9n9AOB0F3rIkTIPFo()
	{
		foreach (GameObject value in EedNaX9y9ColHoMDljdNd80izPNg_0024767fWYnDU3yANis.Values)
		{
			smethod_43((UnityEngine.Object)value);
		}
		EedNaX9y9ColHoMDljdNd80izPNg_0024767fWYnDU3yANis.Clear();
		EmzpBqNhuUJvMBi03aDSG_w = null;
	}

	private static void s5PVtdgcvvCFWcziTuHCc0k()
	{
		XU_0024J1o9n9AOB0F3rIkTIPFo();
		EmzpBqNhuUJvMBi03aDSG_w = smethod_44();
		int num = 0;
		foreach (xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA item in fbSm64A_0024FzBvAjtith640zQ)
		{
			Bounds bounds = HwpI_80ZJwnJXUb_00244d3CPk7TldbzmlfcEQ4_x5hopVfO();
			Vector3 position = smethod_22(item.TIWjI8FsBk2nlZk9NO4HNOE);
			Transform tIWjI8FsBk2nlZk9NO4HNOE = item.TIWjI8FsBk2nlZk9NO4HNOE;
			tIWjI8FsBk2nlZk9NO4HNOE.position = smethod_22(tIWjI8FsBk2nlZk9NO4HNOE) - bounds.center;
			if (item.MrZDDetpveRMT__0024biC7h8tU != xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA.Enum0.block)
			{
				if (item.MrZDDetpveRMT__0024biC7h8tU == xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA.Enum0.gate)
				{
					EmzpBqNhuUJvMBi03aDSG_w.AddGate(item.eWUzF3zpMMjP5r9PB6rj474, num);
					num++;
				}
				else if (item.MrZDDetpveRMT__0024biC7h8tU == xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA.Enum0.spawnpoint)
				{
					EmzpBqNhuUJvMBi03aDSG_w.AddPoint(item.eWUzF3zpMMjP5r9PB6rj474);
				}
			}
			else
			{
				EmzpBqNhuUJvMBi03aDSG_w.AddPrim(item.eWUzF3zpMMjP5r9PB6rj474);
			}
			item.TIWjI8FsBk2nlZk9NO4HNOE.position = position;
		}
	}

	private static void dz3caJwShuGQHB7LBjmInSE(bool saveUndo = true)
	{
		if (EmzpBqNhuUJvMBi03aDSG_w != null)
		{
			Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<List<PrimitiveController>>(global::_003CModule_003E.smethod_29<string>(939519792u), Construct_0);
			Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<List<GameObject>>(global::_003CModule_003E.smethod_27<string>(1062611253u), Construct_0);
			oPscfRQFZWvUSR8BZxgbHJMQBRDQTKjYlqK7tljpOgdF();
			AJ95PT5qH7SI3aQp44bnfe9rXdzyiYcVJnP8p242egZvaWA5f7vGynYUg9dQSkzYlA.CzpGcju2u6iLYL_0024Pt_wCS3Y = false;
			if (saveUndo)
			{
				nfIqruisjwbrqbYtCJuoSA7n8dXAYjEX1_9Hmm3dLrAgzamlT_0024UfxbCTySI3k4AmBA nfIqruisjwbrqbYtCJuoSA7n8dXAYjEX1_9Hmm3dLrAgzamlT_0024UfxbCTySI3k4AmBA2 = new nfIqruisjwbrqbYtCJuoSA7n8dXAYjEX1_9Hmm3dLrAgzamlT_0024UfxbCTySI3k4AmBA(EmzpBqNhuUJvMBi03aDSG_w.primData.ToArray(), isLocal: true, Construct_0);
				nfIqruisjwbrqbYtCJuoSA7n8dXAYjEX1_9Hmm3dLrAgzamlT_0024UfxbCTySI3k4AmBA2.bv4xsECkipN_002441Wa7mcfeqY();
				fbSm64A_0024FzBvAjtith640zQ.AddRange(nfIqruisjwbrqbYtCJuoSA7n8dXAYjEX1_9Hmm3dLrAgzamlT_0024UfxbCTySI3k4AmBA2.rEBjKMgUQVEE8m2fhGbnJb4);
				_0024CMex_0024vAX35hsObIY7ThtQI.Add(nfIqruisjwbrqbYtCJuoSA7n8dXAYjEX1_9Hmm3dLrAgzamlT_0024UfxbCTySI3k4AmBA2);
			}
			if (BFMkrNuUjUFB4B6P9DkaJ_s == vpxRY7V6xxVwrtWzUazGbAijYYgy_j9aOURndFg_MPmV1XpOIQGYmugcr08FohKkWZtGTI6r2M_fFifxmanSRrgmSgq6IJSK9HbxY_0024RtywzhAZtQnNleJNvuto3IE49fVw.scale)
			{
				BFMkrNuUjUFB4B6P9DkaJ_s = vpxRY7V6xxVwrtWzUazGbAijYYgy_j9aOURndFg_MPmV1XpOIQGYmugcr08FohKkWZtGTI6r2M_fFifxmanSRrgmSgq6IJSK9HbxY_0024RtywzhAZtQnNleJNvuto3IE49fVw.move;
			}
		}
	}

	private static void oPscfRQFZWvUSR8BZxgbHJMQBRDQTKjYlqK7tljpOgdF()
	{
		fbSm64A_0024FzBvAjtith640zQ.Clear();
	}

	private static void ACXXqeIVtNHPY3xLo852oh8(Transform hoveringTransform, Vector3 hitPos)
	{
		if (smethod_4((UnityEngine.Object)hoveringTransform, (UnityEngine.Object)null) && (smethod_4((UnityEngine.Object)hoveringTransform.GetComponent<PrimitiveController>(), (UnityEngine.Object)null) || smethod_4((UnityEngine.Object)hoveringTransform.GetComponent<PointController>(), (UnityEngine.Object)null)))
		{
			if (Q90OdRkyhGNvujZeyO47oyPTjunqX_fS_bBOnFmBTJzx == null)
			{
				Xazqwq7g8mxiNVWJBFp6dYRNGLwSlCX_VVtqNx5140SG();
			}
			if (smethod_4((UnityEngine.Object)hoveringTransform.GetComponent<PrimitiveController>(), (UnityEngine.Object)null))
			{
				PrimitiveController component = hoveringTransform.GetComponent<PrimitiveController>();
				AZjgkDHxHA_hM7bKEiV7ES8.k9jTQ33irMfqZyYWrqgwpFA = Color.magenta;
				smethod_15(smethod_20((Component)AZjgkDHxHA_hM7bKEiV7ES8), smethod_22(smethod_20((Component)hoveringTransform)));
				smethod_17(smethod_20((Component)AZjgkDHxHA_hM7bKEiV7ES8), smethod_34(smethod_20((Component)hoveringTransform)));
				smethod_16(smethod_20((Component)AZjgkDHxHA_hM7bKEiV7ES8), smethod_33(smethod_20((Component)hoveringTransform)));
				if (BFMkrNuUjUFB4B6P9DkaJ_s != vpxRY7V6xxVwrtWzUazGbAijYYgy_j9aOURndFg_MPmV1XpOIQGYmugcr08FohKkWZtGTI6r2M_fFifxmanSRrgmSgq6IJSK9HbxY_0024RtywzhAZtQnNleJNvuto3IE49fVw.paint)
				{
					if (!smethod_45(KeyCode.V) || fbSm64A_0024FzBvAjtith640zQ.Count <= 0)
					{
						if (fbSm64A_0024FzBvAjtith640zQ.Count > 0 && hoveringTransform.GetComponent<PrimitiveController>() != null && cpZ_kSFtSLkQRO3C4PD7Nz4 == null && rWJdVfn9_tn1f5vskxsQRZc(hoveringTransform.gameObject) && BFMkrNuUjUFB4B6P9DkaJ_s == vpxRY7V6xxVwrtWzUazGbAijYYgy_j9aOURndFg_MPmV1XpOIQGYmugcr08FohKkWZtGTI6r2M_fFifxmanSRrgmSgq6IJSK9HbxY_0024RtywzhAZtQnNleJNvuto3IE49fVw.move)
						{
							if (Input.GetMouseButtonDown(0))
							{
								nG3WvkfGAg1Lg2PMdQH_OZg.Clear();
								ry9dPoTc05OsEAQnPpZ2_0024hA.Clear();
								MlLIlV50uQCTKGEbiPTTnIk.Clear();
								foreach (xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA item in fbSm64A_0024FzBvAjtith640zQ)
								{
									nG3WvkfGAg1Lg2PMdQH_OZg.Add(item, item.TIWjI8FsBk2nlZk9NO4HNOE.position);
									ry9dPoTc05OsEAQnPpZ2_0024hA.Add(item, item.TIWjI8FsBk2nlZk9NO4HNOE.rotation);
									MlLIlV50uQCTKGEbiPTTnIk.Add(item, item.TIWjI8FsBk2nlZk9NO4HNOE.localScale);
								}
								ra5MAEm12eKHvW9LN8brPaE.transform.position = hoveringTransform.position;
								z6J_8qxbU01ZXTzAmVrFIwygE3yzywLHjQ4ZzTsbQVxK = true;
							}
						}
						else if (Input.GetMouseButtonDown(0) && !rWJdVfn9_tn1f5vskxsQRZc(component) && !EventSystem.current.IsPointerOverGameObject())
						{
							if ((!Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.RightControl) && !Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift)) || BFMkrNuUjUFB4B6P9DkaJ_s == vpxRY7V6xxVwrtWzUazGbAijYYgy_j9aOURndFg_MPmV1XpOIQGYmugcr08FohKkWZtGTI6r2M_fFifxmanSRrgmSgq6IJSK9HbxY_0024RtywzhAZtQnNleJNvuto3IE49fVw.scale)
							{
								oPscfRQFZWvUSR8BZxgbHJMQBRDQTKjYlqK7tljpOgdF();
							}
							fbSm64A_0024FzBvAjtith640zQ.Add(new xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA(component));
						}
						else if (!Input.GetMouseButtonDown(0) || !rWJdVfn9_tn1f5vskxsQRZc(component) || (!Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.RightControl)))
						{
							if (Input.GetMouseButton(0) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && !rWJdVfn9_tn1f5vskxsQRZc(component))
							{
								fbSm64A_0024FzBvAjtith640zQ.Add(new xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA(component));
							}
						}
						else
						{
							xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA[] array = fbSm64A_0024FzBvAjtith640zQ.ToArray();
							foreach (xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA2 in array)
							{
								if (xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA2.uZ_quI6GyZaDBH7Zba2IGp0(component))
								{
									fbSm64A_0024FzBvAjtith640zQ.Remove(xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA2);
									return;
								}
							}
						}
					}
					else if (smethod_42(0))
					{
						if (Input.GetMouseButtonDown(0))
						{
							ra5MAEm12eKHvW9LN8brPaE.transform.position = E4e53KKkkQc5_yliBk0AUfU;
						}
						RaycastHit[] array2 = FV8xGHqtg_wVkgsNgdvcQU4gbPt56Nyt_3knt_RdxcuK(9999f);
						RaycastHit raycastHit = default(RaycastHit);
						bool flag = false;
						RaycastHit[] array3 = array2;
						for (int i = 0; i < array3.Length; i++)
						{
							RaycastHit raycastHit2 = array3[i];
							if ((raycastHit2.transform.gameObject.layer == int_0 || raycastHit2.collider.GetComponent<PointController>() != null) && !rWJdVfn9_tn1f5vskxsQRZc(raycastHit2.transform.gameObject))
							{
								raycastHit = raycastHit2;
								flag = true;
								break;
							}
						}
						if (flag && raycastHit.transform.gameObject.GetComponent<MeshFilter>() != null)
						{
							bm4qNpKN8ThlleNq_0024v_0024DpHZBJQSkCOXr8EFjC57_0024ief7(E4e53KKkkQc5_yliBk0AUfU = raycastHit.transform.gameObject.GetComponent<MeshFilter>().NlkfDM1SXNxyLo_00243zZOqAWU(raycastHit.point));
						}
					}
					else
					{
						RaycastHit[] array4 = FV8xGHqtg_wVkgsNgdvcQU4gbPt56Nyt_3knt_RdxcuK(9999f);
						RaycastHit raycastHit3 = default(RaycastHit);
						bool flag2 = false;
						RaycastHit[] array3 = array4;
						for (int i = 0; i < array3.Length; i++)
						{
							RaycastHit raycastHit4 = array3[i];
							if ((raycastHit4.transform.gameObject.layer == int_0 || raycastHit4.collider.GetComponent<PointController>() != null) && rWJdVfn9_tn1f5vskxsQRZc(raycastHit4.transform.gameObject))
							{
								raycastHit3 = raycastHit4;
								flag2 = true;
								break;
							}
						}
						if (flag2 && raycastHit3.transform.gameObject.GetComponent<MeshFilter>() != null)
						{
							Vector3 e4e53KKkkQc5_yliBk0AUfU = raycastHit3.transform.gameObject.GetComponent<MeshFilter>().NlkfDM1SXNxyLo_00243zZOqAWU(raycastHit3.point);
							iwaUWG_oqbQT6zAcJU5iwzU = true;
							E4e53KKkkQc5_yliBk0AUfU = e4e53KKkkQc5_yliBk0AUfU;
						}
					}
				}
				else if (Input.GetMouseButtonDown(2))
				{
					IdwU0_ARpD_wnWGSdVq151k = component.HJEGNEIKDDO;
					KEFHJCGICLE.HNAHBIMJDCB(global::_003CModule_003E.smethod_28<string>(3623882189u));
				}
				else if (Input.GetMouseButtonDown(0))
				{
					gnRUQtSP6HaPYqmIMjTfoY15ycAUWN0n5_8sGSL87h8t1RYJn_002432i9qyBSGHYESZpQ gnRUQtSP6HaPYqmIMjTfoY15ycAUWN0n5_8sGSL87h8t1RYJn_002432i9qyBSGHYESZpQ2 = new gnRUQtSP6HaPYqmIMjTfoY15ycAUWN0n5_8sGSL87h8t1RYJn_002432i9qyBSGHYESZpQ(component, IdwU0_ARpD_wnWGSdVq151k);
					gnRUQtSP6HaPYqmIMjTfoY15ycAUWN0n5_8sGSL87h8t1RYJn_002432i9qyBSGHYESZpQ2.bv4xsECkipN_002441Wa7mcfeqY();
					_0024CMex_0024vAX35hsObIY7ThtQI.Add(gnRUQtSP6HaPYqmIMjTfoY15ycAUWN0n5_8sGSL87h8t1RYJn_002432i9qyBSGHYESZpQ2);
					KEFHJCGICLE.HNAHBIMJDCB(global::_003CModule_003E.smethod_26<string>(1592456439u));
				}
			}
			else if (hoveringTransform.GetComponent<PointController>() != null && hoveringTransform.name.StartsWith(global::_003CModule_003E.smethod_28<string>(1258251454u)))
			{
				PointController component2 = hoveringTransform.GetComponent<PointController>();
				AZjgkDHxHA_hM7bKEiV7ES8.k9jTQ33irMfqZyYWrqgwpFA = Color.cyan;
				AZjgkDHxHA_hM7bKEiV7ES8.transform.position = hoveringTransform.transform.position;
				AZjgkDHxHA_hM7bKEiV7ES8.transform.localScale = hoveringTransform.transform.localScale;
				AZjgkDHxHA_hM7bKEiV7ES8.transform.rotation = hoveringTransform.transform.rotation;
				if (BFMkrNuUjUFB4B6P9DkaJ_s != vpxRY7V6xxVwrtWzUazGbAijYYgy_j9aOURndFg_MPmV1XpOIQGYmugcr08FohKkWZtGTI6r2M_fFifxmanSRrgmSgq6IJSK9HbxY_0024RtywzhAZtQnNleJNvuto3IE49fVw.paint && Input.GetMouseButtonDown(0) && !rWJdVfn9_tn1f5vskxsQRZc(component2))
				{
					if ((!Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.RightControl)) || BFMkrNuUjUFB4B6P9DkaJ_s == vpxRY7V6xxVwrtWzUazGbAijYYgy_j9aOURndFg_MPmV1XpOIQGYmugcr08FohKkWZtGTI6r2M_fFifxmanSRrgmSgq6IJSK9HbxY_0024RtywzhAZtQnNleJNvuto3IE49fVw.scale)
					{
						oPscfRQFZWvUSR8BZxgbHJMQBRDQTKjYlqK7tljpOgdF();
					}
					fbSm64A_0024FzBvAjtith640zQ.Add(new xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA(component2));
				}
			}
			else if (hoveringTransform.GetComponent<PointController>() != null && hoveringTransform.name.StartsWith(global::_003CModule_003E.smethod_28<string>(1969652022u)))
			{
				PointController component3 = hoveringTransform.GetComponent<PointController>();
				AZjgkDHxHA_hM7bKEiV7ES8.k9jTQ33irMfqZyYWrqgwpFA = Color.green;
				AZjgkDHxHA_hM7bKEiV7ES8.transform.position = hoveringTransform.transform.position;
				AZjgkDHxHA_hM7bKEiV7ES8.transform.localScale = hoveringTransform.transform.Find(global::_003CModule_003E.smethod_27<string>(2673620935u)).localScale;
				AZjgkDHxHA_hM7bKEiV7ES8.transform.rotation = hoveringTransform.transform.rotation;
				if (BFMkrNuUjUFB4B6P9DkaJ_s != vpxRY7V6xxVwrtWzUazGbAijYYgy_j9aOURndFg_MPmV1XpOIQGYmugcr08FohKkWZtGTI6r2M_fFifxmanSRrgmSgq6IJSK9HbxY_0024RtywzhAZtQnNleJNvuto3IE49fVw.paint && Input.GetMouseButtonDown(0) && !rWJdVfn9_tn1f5vskxsQRZc(component3))
				{
					if ((!Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.RightControl)) || BFMkrNuUjUFB4B6P9DkaJ_s == vpxRY7V6xxVwrtWzUazGbAijYYgy_j9aOURndFg_MPmV1XpOIQGYmugcr08FohKkWZtGTI6r2M_fFifxmanSRrgmSgq6IJSK9HbxY_0024RtywzhAZtQnNleJNvuto3IE49fVw.scale)
					{
						oPscfRQFZWvUSR8BZxgbHJMQBRDQTKjYlqK7tljpOgdF();
					}
					fbSm64A_0024FzBvAjtith640zQ.Add(new xQLlXwvQfU73AR7DWoyqeJv6ZjVQBscarONfuuoCEEaT09MkuTY_0024OzaxmHcti2C7mA(component3));
				}
			}
		}
		else
		{
			AZjgkDHxHA_hM7bKEiV7ES8.transform.localScale = Vector3.one;
			AZjgkDHxHA_hM7bKEiV7ES8.transform.position = AJ95PT5qH7SI3aQp44bnfe9rXdzyiYcVJnP8p242egZvaWA5f7vGynYUg9dQSkzYlA.Sl3qb7_zclIQRMX7sWCu5EE;
			AZjgkDHxHA_hM7bKEiV7ES8.transform.rotation = Quaternion.identity;
			if (Input.GetMouseButtonDown(0) && EventSystem.current.currentSelectedGameObject == null && !EventSystem.current.IsPointerOverGameObject())
			{
				oPscfRQFZWvUSR8BZxgbHJMQBRDQTKjYlqK7tljpOgdF();
			}
		}
		while (fXmKrvqNNkqzpO5XpWPRVZI8_mbex_0024Y2G0UhxyZZLrx0.Count > fbSm64A_0024FzBvAjtith640zQ.Count)
		{
			UnityEngine.Object.Destroy(fXmKrvqNNkqzpO5XpWPRVZI8_mbex_0024Y2G0UhxyZZLrx0[fXmKrvqNNkqzpO5XpWPRVZI8_mbex_0024Y2G0UhxyZZLrx0.Count - 1].gameObject);
			fXmKrvqNNkqzpO5XpWPRVZI8_mbex_0024Y2G0UhxyZZLrx0.RemoveAt(fXmKrvqNNkqzpO5XpWPRVZI8_mbex_0024Y2G0UhxyZZLrx0.Count - 1);
		}
		while (fbSm64A_0024FzBvAjtith640zQ.Count > fXmKrvqNNkqzpO5XpWPRVZI8_mbex_0024Y2G0UhxyZZLrx0.Count)
		{
			fXmKrvqNNkqzpO5XpWPRVZI8_mbex_0024Y2G0UhxyZZLrx0.Add(new GameObject(global::_003CModule_003E.smethod_27<string>(4241856859u) + fXmKrvqNNkqzpO5XpWPRVZI8_mbex_0024Y2G0UhxyZZLrx0.Count).AddComponent<c2wojWPyeJ90X8UxbVw8Ie7t2iSXT4W6AM5UctjLMkqq>());
		}
	}

	internal static bool smethod_2(UnityEngine.Object object_0, UnityEngine.Object object_1)
	{
		return object_0 == object_1;
	}

	internal static GameObject smethod_3(SceneMan sceneMan_0, string string_0)
	{
		return sceneMan_0.GetTGL(string_0);
	}

	internal static bool smethod_4(UnityEngine.Object object_0, UnityEngine.Object object_1)
	{
		return object_0 != object_1;
	}

	internal static bool smethod_5(Toggle toggle_0)
	{
		return toggle_0.isOn;
	}

	internal static bool smethod_6(SystemData.EHLMFKOOHLI ehlmfkoohli_0)
	{
		return HOCGCCAIPFF.AFLJECMLJDL(ehlmfkoohli_0);
	}

	internal static Vector3 smethod_7(CameraController cameraController_0, bool bool_0)
	{
		return cameraController_0.GetEyeDir(bool_0);
	}

	internal static GameObject smethod_8(string string_0)
	{
		return new GameObject(string_0);
	}

	internal static GameObject smethod_9(PrimitiveType primitiveType_0)
	{
		return GameObject.CreatePrimitive(primitiveType_0);
	}

	internal static Transform smethod_10(GameObject gameObject_0)
	{
		return gameObject_0.transform;
	}

	internal static void smethod_11(Transform transform_0, Transform transform_1)
	{
		transform_0.parent = transform_1;
	}

	internal static void smethod_12(Transform transform_0, Vector3 vector3_0)
	{
		transform_0.localPosition = vector3_0;
	}

	internal static string smethod_13(string string_0, string string_1)
	{
		return string_0 + string_1;
	}

	internal static Gradient smethod_14()
	{
		return new Gradient();
	}

	internal static void smethod_15(Transform transform_0, Vector3 vector3_0)
	{
		transform_0.position = vector3_0;
	}

	internal static void smethod_16(Transform transform_0, Quaternion quaternion_0)
	{
		transform_0.rotation = quaternion_0;
	}

	internal static void smethod_17(Transform transform_0, Vector3 vector3_0)
	{
		transform_0.localScale = vector3_0;
	}

	internal static Transform smethod_18(Transform transform_0, int int_1)
	{
		return transform_0.GetChild(int_1);
	}

	internal static Camera smethod_19()
	{
		return Camera.main;
	}

	internal static Transform smethod_20(Component component_0)
	{
		return component_0.transform;
	}

	internal static Vector3 smethod_21(Transform transform_0)
	{
		return transform_0.forward;
	}

	internal static Vector3 smethod_22(Transform transform_0)
	{
		return transform_0.position;
	}

	internal static Type smethod_23(RuntimeTypeHandle runtimeTypeHandle_0)
	{
		return Type.GetTypeFromHandle(runtimeTypeHandle_0);
	}
	internal static MethodInfo method_0(Type provider, string string_0, BindingFlags bindingFlags_0)
	{
		return provider.GetMethod(string_0, bindingFlags_0);
	}

	internal static DynamicMethod smethod_24(string string_0, Type type_0, Type[] type_1, Type type_2)
	{
		return new DynamicMethod(string_0, type_0, type_1, type_2);
	}

	internal static ILGenerator smethod_25(DynamicMethod dynamicMethod_0)
	{
		return dynamicMethod_0.GetILGenerator();
	}

	internal static void smethod_26(ILGenerator ilgenerator_0, OpCode opCode_0)
	{
		ilgenerator_0.Emit(opCode_0);
	}

	internal static void smethod_27(ILGenerator ilgenerator_0, OpCode opCode_0, MethodInfo methodInfo_0)
	{
		ilgenerator_0.Emit(opCode_0, methodInfo_0);
	}

	internal static Delegate smethod_28(DynamicMethod dynamicMethod_0, Type type_0)
	{
		return dynamicMethod_0.CreateDelegate(type_0);
	}

	internal static Vector3 smethod_29()
	{
		return Input.mousePosition;
	}

	internal static Ray smethod_30(Camera camera_0, Vector3 vector3_0)
	{
		return camera_0.ScreenPointToRay(vector3_0);
	}

	internal static RaycastHit[] smethod_31(Ray ray_0, float float_0)
	{
		return Physics.RaycastAll(ray_0, float_0);
	}

	internal static bool smethod_32(string string_0, string string_1)
	{
		return string_0 != string_1;
	}

	internal static Quaternion smethod_33(Transform transform_0)
	{
		return transform_0.rotation;
	}

	internal static Vector3 smethod_34(Transform transform_0)
	{
		return transform_0.localScale;
	}

	internal static GameObject smethod_35(string string_0)
	{
		return GameObject.Find(string_0);
	}

	internal static bool smethod_36(string string_0)
	{
		return string.IsNullOrEmpty(string_0);
	}

	internal static string smethod_37(UnityEngine.Object object_0)
	{
		return object_0.name;
	}

	internal static string smethod_38(string string_0, int int_1)
	{
		return string_0.Substring(int_1);
	}

	internal static bool smethod_39(string string_0, string string_1)
	{
		return string_0 == string_1;
	}

	internal static char smethod_40(string string_0, int int_1)
	{
		return string_0[int_1];
	}

	internal static bool smethod_41(int int_1)
	{
		return Input.GetMouseButtonDown(int_1);
	}

	internal static bool smethod_42(int int_1)
	{
		return Input.GetMouseButton(int_1);
	}

	internal static void smethod_43(UnityEngine.Object object_0)
	{
		UnityEngine.Object.Destroy(object_0);
	}

	internal static ConstructData smethod_44()
	{
		return new ConstructData();
	}

	internal static bool smethod_45(KeyCode keyCode_0)
	{
		return Input.GetKey(keyCode_0);
	}
}
