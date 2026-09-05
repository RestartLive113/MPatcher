using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HarmonyLib;
using MPatchrMain;
using UnityEngine;
using UnityEngine.UI;

internal static class w5chUZMTNzi3hsv_ZuyAmjsrIqV_JS4UmvQj9ChlfHgM0YkamwAOecvqmdcCMPuZpA
{
	[HarmonyPatch("PKLHNJNFKFH")]
	[HarmonyPatch(typeof(PAEHEMJNPND))]
	internal static class Class23
	{
		[HarmonyPostfix]
		internal static void FeUAVwFbW6wGJJdNimZY9yI(GameObject __result, BlockData JNKEKNOAPHO)
		{
			if (JNKEKNOAPHO.index == 255 && JNKEKNOAPHO.press == 63 && JNKEKNOAPHO.type == BlockData.AAHMDBHDCDK.Chassis && JNKEKNOAPHO.props != null && JNKEKNOAPHO.props.ContainsKey(global::_003CModule_003E.smethod_27<string>(2701065902u)))
			{
				Color item = smethod_1(smethod_0(__result.GetComponent<MeshFilter>()))[0];
				MeshFilter component = __result.GetComponent<MeshFilter>();
				smethod_2(component, Ij5tGwaag_TEfFIAUYEMcoU);
				List<Color> list = new List<Color>();
				for (int i = 0; i < smethod_3(smethod_0(component)).Length; i++)
				{
					list.Add(item);
				}
				smethod_4(smethod_0(component), list);
				smethod_5(__result.GetComponent<BlockController>());
			}
		}

		internal static Mesh smethod_0(MeshFilter meshFilter_0)
		{
			return meshFilter_0.mesh;
		}

		internal static Color[] smethod_1(Mesh mesh_0)
		{
			return mesh_0.colors;
		}

		internal static void smethod_2(MeshFilter meshFilter_0, Mesh mesh_0)
		{
			meshFilter_0.mesh = mesh_0;
		}

		internal static Vector3[] smethod_3(Mesh mesh_0)
		{
			return mesh_0.vertices;
		}

		internal static void smethod_4(Mesh mesh_0, List<Color> list_0)
		{
			mesh_0.SetColors(list_0);
		}

		internal static void smethod_5(BlockController blockController_0)
		{
			blockController_0.PrepColor();
		}
	}

	[HarmonyPatch(typeof(PAEHEMJNPND))]
	[HarmonyPatch("IDJGGLOMDDI")]
	internal static class Class24
	{
		[HarmonyPostfix]
		internal static void FeUAVwFbW6wGJJdNimZY9yI(ref GameObject __result, int DMPDBCHILNB, Vector3 PJAEMFPMNCO, int NGFJEGCOMAH, int GBNOAOLPDCA)
		{
			if (smethod_0((UnityEngine.Object)(SceneMan.JFAOKFIDAGK as Build), (UnityEngine.Object)null) && (SceneMan.JFAOKFIDAGK as Build).i0duGIJ6M7BXC7c2cbeyvXM1hCrcoNKNJzlqDrhwjoxM<BlockData>(global::_003CModule_003E.smethod_29<string>(584300859u)) != null)
			{
				Dictionary<string, object> props = (SceneMan.JFAOKFIDAGK as Build).i0duGIJ6M7BXC7c2cbeyvXM1hCrcoNKNJzlqDrhwjoxM<BlockData>(global::_003CModule_003E.smethod_27<string>(3543220394u)).props;
				if (props == null)
				{
					__result.GetComponent<BlockController>().JNKEKNOAPHO.props = null;
				}
				else
				{
					__result.GetComponent<BlockController>().JNKEKNOAPHO.props = new Dictionary<string, object>(props);
				}
			}
			BlockData jNKEKNOAPHO = __result.GetComponent<BlockController>().JNKEKNOAPHO;
			if (MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.stickyNotes)
			{
				Class23.FeUAVwFbW6wGJJdNimZY9yI(__result, jNKEKNOAPHO);
			}
		}

		internal static bool smethod_0(UnityEngine.Object object_0, UnityEngine.Object object_1)
		{
			return object_0 != object_1;
		}
	}

	[HarmonyPatch(typeof(Build))]
	[HarmonyPatch("EBNNCJFENOP")]
	internal static class EW64Igh84u6I4kLPhvZCaryAfzR0C0zYc33fzYPkADdSy_Kf_aA_0024_qS0s8EuTjNxfiEYl_0024ovaQmGuVl3B6DgCuT1A1n_SCcPAJoBTabakzp5nGONUcuNI0hrHl1RUJhRPg
	{
		[HarmonyPrefix]
		internal static void smethod_0()
		{
			if (smethod_1((UnityEngine.Object)gameObject_0, (UnityEngine.Object)null))
			{
				smethod_2(gameObject_0, bool_0: false);
			}
			HOCGCCAIPFF.NDIOFGDJAJO = false;
			(SceneMan.JFAOKFIDAGK as Build).D7dqMa4OGuhdUnXHjJZM6vrBYDmyw_0024HWYteNzGVsTh5k<GameObject>(global::_003CModule_003E.smethod_25<string>(776703178u), null);
		}

		internal static bool smethod_1(UnityEngine.Object object_0, UnityEngine.Object object_1)
		{
			return object_0 != object_1;
		}

		internal static void smethod_2(GameObject gameObject_0, bool bool_0)
		{
			gameObject_0.SetActive(bool_0);
		}
	}

	[HarmonyPatch(typeof(Build))]
	[HarmonyPatch("JOHIPODALCN")]
	internal static class ZaPtz1XbmxIr9Gg_l8jFEtOyYpePV_0024eSA6CbHt_OILZrRco40bZ2fFF2aqOpAhkwnNqWjn64ZpiZnq_apiQHarimF4RwG7xNBIjbY2m3PPb0AeYLJaYnEg_WojuhwF3jgg
	{
		[HarmonyPostfix]
		internal static void FeUAVwFbW6wGJJdNimZY9yI(Build __instance, BlockData ___DFAAJHFLMJL, BlockData ___LBBOFMGMMFF, ref bool ___OIAOGDEPDCO, FreeCameraController ___BOIEJCIBHKI, ref float ___JBBCIHLBDNL)
		{
			bool flag = smethod_0(SystemData.EHLMFKOOHLI.BlockPanel);
			if (!___BOIEJCIBHKI.yORc5mAq969v9kZYfhyjAiM())
			{
				if (smethod_1(1))
				{
					___JBBCIHLBDNL = 0.1f;
				}
			}
			else if ((flag || smethod_1(0)) && ___DFAAJHFLMJL == null)
			{
				BlockData blockData_ = aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.BlockData_0;
				if (blockData_ != null && blockData_.index == 255 && blockData_.press == 63 && blockData_.type == BlockData.AAHMDBHDCDK.Chassis && blockData_.props != null && blockData_.props.ContainsKey(global::_003CModule_003E.smethod_27<string>(2701065902u)))
				{
					smethod_2(global::_003CModule_003E.smethod_29<string>(626259385u), 1f);
					smethod_3(gameObject_0, bool_0: true);
					HOCGCCAIPFF.NDIOFGDJAJO = true;
					___BOIEJCIBHKI.yNEegNKtGknTMZqZvM5dqjw(bool_0: false);
					lbe677RXObar_0024WSUQWRdgNA.pZEKY5TzLd4S3z2lXESoRnw = smethod_4(aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.BlockData_0.props[global::_003CModule_003E.smethod_28<string>(1785901943u)]);
				}
			}
		}

		internal static bool smethod_0(SystemData.EHLMFKOOHLI ehlmfkoohli_0)
		{
			return HOCGCCAIPFF.FGCCNKAIKAI(ehlmfkoohli_0);
		}

		internal static bool smethod_1(int int_0)
		{
			return Input.GetMouseButtonDown(int_0);
		}

		internal static void smethod_2(string string_0, float float_0)
		{
			KEFHJCGICLE.HNAHBIMJDCB(string_0, float_0);
		}

		internal static void smethod_3(GameObject gameObject_0, bool bool_0)
		{
			gameObject_0.SetActive(bool_0);
		}

		internal static string smethod_4(object object_0)
		{
			return object_0.ToString();
		}
	}

	[HarmonyPatch("IPFFLBAMPNG")]
	[HarmonyPatch(typeof(Build))]
	internal static class fWZd_0024jPwaa4EJAeJ8CwEK2PgKc7Bg9QmtUjU4O7MoAFn3czxtnIhzXkiVu_00248wgBdqqStYr_E7kutH00veSQ73qqzlaox4QtPi_0024dHHqpcvbhf8uxcpoIghQwXIc0yBDtprQ
	{
		[HarmonyPrefix]
		internal static bool smethod_0()
		{
			if (aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.BlockData_0 != null && aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.BlockData_0.index == 255 && aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.BlockData_0.press == 63 && aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.BlockData_0.type == BlockData.AAHMDBHDCDK.Chassis && aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.BlockData_0.props != null && aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.BlockData_0.props.ContainsKey(global::_003CModule_003E.smethod_29<string>(1654040871u)))
			{
				return false;
			}
			return true;
		}
	}

	[Serializable]
	[CompilerGenerated]
	private sealed class naqYVuuaMjBY4iEZO8oD_PC7NgZXvrwiCevnhiho0_JLFfTnVEUOobI2rDbq9tD5_0024luAKQI6AV0eYPxYSz3Eney_0024UxiN3ZpzuGf9Krr3sUs3
	{
		public static readonly naqYVuuaMjBY4iEZO8oD_PC7NgZXvrwiCevnhiho0_JLFfTnVEUOobI2rDbq9tD5_0024luAKQI6AV0eYPxYSz3Eney_0024UxiN3ZpzuGf9Krr3sUs3 _003C_003E9 = new naqYVuuaMjBY4iEZO8oD_PC7NgZXvrwiCevnhiho0_JLFfTnVEUOobI2rDbq9tD5_0024luAKQI6AV0eYPxYSz3Eney_0024UxiN3ZpzuGf9Krr3sUs3();

		public static Action<lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw> _003C_003E9__9_0;

		public static Action<string> _003C_003E9__9_1;

		internal void method_0(lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw me)
		{
			Build build = SceneMan.JFAOKFIDAGK as Build;
			smethod_0(h8IObtktr55YbRkXPAh2ezAKudfCUtso9iPPAXfQe_K9);
			h8IObtktr55YbRkXPAh2ezAKudfCUtso9iPPAXfQe_K9.index = 255;
			h8IObtktr55YbRkXPAh2ezAKudfCUtso9iPPAXfQe_K9.press = 63;
			h8IObtktr55YbRkXPAh2ezAKudfCUtso9iPPAXfQe_K9.props = new Dictionary<string, object>();
			h8IObtktr55YbRkXPAh2ezAKudfCUtso9iPPAXfQe_K9.props.Add(global::_003CModule_003E.smethod_29<string>(1654040871u), "");
			build.AMv3blqvprCVsYIr_00243bnU9PutMvqR_0024t23D7MAoFAY5Vr<bool>(global::_003CModule_003E.smethod_28<string>(3971339567u), new object[2] { h8IObtktr55YbRkXPAh2ezAKudfCUtso9iPPAXfQe_K9.type, false });
			if (smethod_2(smethod_1((SceneMan)build, global::_003CModule_003E.smethod_27<string>(1835561530u))))
			{
				build.D7dqMa4OGuhdUnXHjJZM6vrBYDmyw_0024HWYteNzGVsTh5k(global::_003CModule_003E.smethod_29<string>(2119195601u), 0.2f);
			}
		}

		internal void method_1(string text)
		{
			aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.BlockData_0.props[global::_003CModule_003E.smethod_29<string>(1654040871u)] = text;
		}

		internal static void smethod_0(BlockData blockData_0)
		{
			blockData_0.Initialize();
		}

		internal static GameObject smethod_1(SceneMan sceneMan_0, string string_0)
		{
			return sceneMan_0.GetPNL(string_0);
		}

		internal static bool smethod_2(GameObject gameObject_0)
		{
			return gameObject_0.activeSelf;
		}
	}

	[CompilerGenerated]
	private sealed class Z64D_0024rqCAhIOYOVYlWLh5Y6JN7kAZzZS9xHay0unAYDHrJ_0024gWorAYNSeChQLZ0jIqb37VWJQEgdcGRcTskiWoNTLtajwNAcYkwptEfFG56y6kbZf7_0024qk43DVuPslK74Vkg
	{
		public Image Xz1wKOB0iImarofJ3M1e0ew;

		public GameObject XrfZbHvdr5efJVeQiWlUXUM;

		internal void mecCysFcBI0cZT0CqfJLVCxrD4dGo4kFBtUSXdzZBoIw(AmMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F me)
		{
			bool flag = aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.BlockData_0 != null && aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.BlockData_0.props != null && aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.BlockData_0.props.ContainsKey(global::_003CModule_003E.smethod_27<string>(2701065902u)) && !smethod_1(smethod_0(aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.BlockData_0.props[global::_003CModule_003E.smethod_25<string>(2441936668u)])) && (SceneMan.JFAOKFIDAGK as Build).i0duGIJ6M7BXC7c2cbeyvXM1hCrcoNKNJzlqDrhwjoxM<FreeCameraController>(global::_003CModule_003E.smethod_26<string>(2379604815u)).yORc5mAq969v9kZYfhyjAiM();
			if (smethod_2((Behaviour)Xz1wKOB0iImarofJ3M1e0ew) != flag)
			{
				smethod_3((Behaviour)Xz1wKOB0iImarofJ3M1e0ew, flag);
				smethod_4(XrfZbHvdr5efJVeQiWlUXUM, flag);
				if (flag)
				{
					smethod_5(XrfZbHvdr5efJVeQiWlUXUM.GetComponent<Text>(), smethod_0(aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.BlockData_0.props[global::_003CModule_003E.smethod_28<string>(1785901943u)]));
				}
			}
		}

		internal static string smethod_0(object object_0)
		{
			return object_0.ToString();
		}

		internal static bool smethod_1(string string_0)
		{
			return string.IsNullOrEmpty(string_0);
		}

		internal static bool smethod_2(Behaviour behaviour_0)
		{
			return behaviour_0.enabled;
		}

		internal static void smethod_3(Behaviour behaviour_0, bool bool_0)
		{
			behaviour_0.enabled = bool_0;
		}

		internal static void smethod_4(GameObject gameObject_0, bool bool_0)
		{
			gameObject_0.SetActive(bool_0);
		}

		internal static void smethod_5(Text text_0, string string_0)
		{
			text_0.text = string_0;
		}
	}

	private static GameObject YTrg43riKkHULvUc4I2EXr_0024LH9io2s48IND1_00245eevikn;

	private static GameObject gameObject_0;

	private static ax_0024v9_0024SAexqtnivpn2xbJvDiJH60f7H1LaLLn3Ct9ToDsxp_ZwO4d5b6LcBrpAgoWQ lbe677RXObar_0024WSUQWRdgNA;

	private static Mesh mesh_0;

	private static BlockData h8IObtktr55YbRkXPAh2ezAKudfCUtso9iPPAXfQe_K9
	{
		get
		{
			return (SceneMan.JFAOKFIDAGK as Build).i0duGIJ6M7BXC7c2cbeyvXM1hCrcoNKNJzlqDrhwjoxM<BlockData>(global::_003CModule_003E.smethod_25<string>(454118240u));
		}
		set
		{
			(SceneMan.JFAOKFIDAGK as Build).D7dqMa4OGuhdUnXHjJZM6vrBYDmyw_0024HWYteNzGVsTh5k(global::_003CModule_003E.smethod_26<string>(1975510309u), value);
		}
	}

	private static Mesh Ij5tGwaag_TEfFIAUYEMcoU
	{
		get
		{
			if (smethod_0((UnityEngine.Object)mesh_0, (UnityEngine.Object)null))
			{
				mesh_0 = smethod_1(MPatchr.n5wPFlpwFJrXE8uDgzL1YDc.LoadAsset<GameObject>(global::_003CModule_003E.smethod_25<string>(1418618556u)).smethod_0(global::_003CModule_003E.smethod_29<string>(2077237075u)).GetComponent<MeshFilter>());
			}
			return UnityEngine.Object.Instantiate(mesh_0);
		}
	}

	internal static void KX8jtCPdJb997Zi2Dvgkpuc()
	{
		Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.P3tLroX6fcPfQha_0024JdbpYXg(global::_003CModule_003E.smethod_25<string>(1418618556u), new Vector3(-360f, -240f), global::_003CModule_003E.smethod_26<string>(2177557562u), delegate
		{
			Build build = SceneMan.JFAOKFIDAGK as Build;
			naqYVuuaMjBY4iEZO8oD_PC7NgZXvrwiCevnhiho0_JLFfTnVEUOobI2rDbq9tD5_0024luAKQI6AV0eYPxYSz3Eney_0024UxiN3ZpzuGf9Krr3sUs3.smethod_0(h8IObtktr55YbRkXPAh2ezAKudfCUtso9iPPAXfQe_K9);
			h8IObtktr55YbRkXPAh2ezAKudfCUtso9iPPAXfQe_K9.index = 255;
			h8IObtktr55YbRkXPAh2ezAKudfCUtso9iPPAXfQe_K9.press = 63;
			h8IObtktr55YbRkXPAh2ezAKudfCUtso9iPPAXfQe_K9.props = new Dictionary<string, object>();
			h8IObtktr55YbRkXPAh2ezAKudfCUtso9iPPAXfQe_K9.props.Add(global::_003CModule_003E.smethod_29<string>(1654040871u), "");
			build.AMv3blqvprCVsYIr_00243bnU9PutMvqR_0024t23D7MAoFAY5Vr<bool>(global::_003CModule_003E.smethod_28<string>(3971339567u), new object[2] { h8IObtktr55YbRkXPAh2ezAKudfCUtso9iPPAXfQe_K9.type, false });
			if (naqYVuuaMjBY4iEZO8oD_PC7NgZXvrwiCevnhiho0_JLFfTnVEUOobI2rDbq9tD5_0024luAKQI6AV0eYPxYSz3Eney_0024UxiN3ZpzuGf9Krr3sUs3.smethod_2(naqYVuuaMjBY4iEZO8oD_PC7NgZXvrwiCevnhiho0_JLFfTnVEUOobI2rDbq9tD5_0024luAKQI6AV0eYPxYSz3Eney_0024UxiN3ZpzuGf9Krr3sUs3.smethod_1((SceneMan)build, global::_003CModule_003E.smethod_27<string>(1835561530u))))
			{
				build.D7dqMa4OGuhdUnXHjJZM6vrBYDmyw_0024HWYteNzGVsTh5k(global::_003CModule_003E.smethod_29<string>(2119195601u), 0.2f);
			}
		}, null, 20, null, pickBlock: true);
		gameObject_0 = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.qSsYOsEQb7x452a9Y45dVEk(Vector3.zero, new Vector2(600f, 400f), GameObject.Find(global::_003CModule_003E.smethod_29<string>(618767629u)).smethod_0(global::_003CModule_003E.smethod_28<string>(1391506648u)).transform.parent);
		lbe677RXObar_0024WSUQWRdgNA = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.bqYYMQnP2SDqYH85wmN_0024evI(global::_003CModule_003E.smethod_28<string>(1027267044u), Vector3.zero, "", global::_003CModule_003E.smethod_26<string>(2312859613u), gameObject_0.transform);
		lbe677RXObar_0024WSUQWRdgNA.UzVS61irgJn5Pnqwx0lThng(new Vector2(560f, 350f));
		lbe677RXObar_0024WSUQWRdgNA.BSdnl9DYm6Rd4cVhJ555c_A.characterLimit = 0;
		lbe677RXObar_0024WSUQWRdgNA.BSdnl9DYm6Rd4cVhJ555c_A.lineType = InputField.LineType.MultiLineNewline;
		lbe677RXObar_0024WSUQWRdgNA.BSdnl9DYm6Rd4cVhJ555c_A.textComponent.alignment = TextAnchor.UpperLeft;
		lbe677RXObar_0024WSUQWRdgNA.gameObject.smethod_0(global::_003CModule_003E.smethod_28<string>(2696500953u)).GetComponent<Text>().alignment = TextAnchor.UpperLeft;
		lbe677RXObar_0024WSUQWRdgNA.JNMaMdWdD3fzh8iVBUwSGz4 = delegate(string text)
		{
			aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.BlockData_0.props[global::_003CModule_003E.smethod_29<string>(1654040871u)] = text;
		};
		gameObject_0.SetActive(value: false);
		YTrg43riKkHULvUc4I2EXr_0024LH9io2s48IND1_00245eevikn = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.qSsYOsEQb7x452a9Y45dVEk(Vector3.zero, new Vector2(500f, 300f), GameObject.Find(global::_003CModule_003E.smethod_29<string>(618767629u)).smethod_0(global::_003CModule_003E.smethod_28<string>(1391506648u)).transform.parent);
		Image Xz1wKOB0iImarofJ3M1e0ew = YTrg43riKkHULvUc4I2EXr_0024LH9io2s48IND1_00245eevikn.GetComponent<Image>();
		Xz1wKOB0iImarofJ3M1e0ew.color = new Color(0f, 0f, 0f, 0.5f);
		string name = global::_003CModule_003E.smethod_27<string>(723427672u);
		Vector3 zero = Vector3.zero;
		Transform transform = YTrg43riKkHULvUc4I2EXr_0024LH9io2s48IND1_00245eevikn.transform;
		Vector2 resizeRectTo = new Vector2(470f, 270f);
		GameObject XrfZbHvdr5efJVeQiWlUXUM = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.YU_pwpP3pKH76IHZTea_SXk(name, zero, "", transform, rmOutline: true, -1, FontStyle.Normal, TextAnchor.UpperLeft, default(Color), resizeRect: true, resizeRectTo);
		XrfZbHvdr5efJVeQiWlUXUM.GetComponent<Text>().resizeTextForBestFit = true;
		XrfZbHvdr5efJVeQiWlUXUM.GetComponent<Text>().resizeTextMaxSize = 18;
		XrfZbHvdr5efJVeQiWlUXUM.GetComponent<Text>().resizeTextMinSize = 10;
		YTrg43riKkHULvUc4I2EXr_0024LH9io2s48IND1_00245eevikn.AddComponent<AmMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F>().U_0024eIoX6e3N9Ag_us_EcGHBI(delegate
		{
			bool flag = aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.BlockData_0 != null && aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.BlockData_0.props != null && aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.BlockData_0.props.ContainsKey(global::_003CModule_003E.smethod_27<string>(2701065902u)) && !Z64D_0024rqCAhIOYOVYlWLh5Y6JN7kAZzZS9xHay0unAYDHrJ_0024gWorAYNSeChQLZ0jIqb37VWJQEgdcGRcTskiWoNTLtajwNAcYkwptEfFG56y6kbZf7_0024qk43DVuPslK74Vkg.smethod_1(Z64D_0024rqCAhIOYOVYlWLh5Y6JN7kAZzZS9xHay0unAYDHrJ_0024gWorAYNSeChQLZ0jIqb37VWJQEgdcGRcTskiWoNTLtajwNAcYkwptEfFG56y6kbZf7_0024qk43DVuPslK74Vkg.smethod_0(aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.BlockData_0.props[global::_003CModule_003E.smethod_25<string>(2441936668u)])) && (SceneMan.JFAOKFIDAGK as Build).i0duGIJ6M7BXC7c2cbeyvXM1hCrcoNKNJzlqDrhwjoxM<FreeCameraController>(global::_003CModule_003E.smethod_26<string>(2379604815u)).yORc5mAq969v9kZYfhyjAiM();
			if (Z64D_0024rqCAhIOYOVYlWLh5Y6JN7kAZzZS9xHay0unAYDHrJ_0024gWorAYNSeChQLZ0jIqb37VWJQEgdcGRcTskiWoNTLtajwNAcYkwptEfFG56y6kbZf7_0024qk43DVuPslK74Vkg.smethod_2((Behaviour)Xz1wKOB0iImarofJ3M1e0ew) != flag)
			{
				Z64D_0024rqCAhIOYOVYlWLh5Y6JN7kAZzZS9xHay0unAYDHrJ_0024gWorAYNSeChQLZ0jIqb37VWJQEgdcGRcTskiWoNTLtajwNAcYkwptEfFG56y6kbZf7_0024qk43DVuPslK74Vkg.smethod_3((Behaviour)Xz1wKOB0iImarofJ3M1e0ew, flag);
				Z64D_0024rqCAhIOYOVYlWLh5Y6JN7kAZzZS9xHay0unAYDHrJ_0024gWorAYNSeChQLZ0jIqb37VWJQEgdcGRcTskiWoNTLtajwNAcYkwptEfFG56y6kbZf7_0024qk43DVuPslK74Vkg.smethod_4(XrfZbHvdr5efJVeQiWlUXUM, flag);
				if (flag)
				{
					Z64D_0024rqCAhIOYOVYlWLh5Y6JN7kAZzZS9xHay0unAYDHrJ_0024gWorAYNSeChQLZ0jIqb37VWJQEgdcGRcTskiWoNTLtajwNAcYkwptEfFG56y6kbZf7_0024qk43DVuPslK74Vkg.smethod_5(XrfZbHvdr5efJVeQiWlUXUM.GetComponent<Text>(), Z64D_0024rqCAhIOYOVYlWLh5Y6JN7kAZzZS9xHay0unAYDHrJ_0024gWorAYNSeChQLZ0jIqb37VWJQEgdcGRcTskiWoNTLtajwNAcYkwptEfFG56y6kbZf7_0024qk43DVuPslK74Vkg.smethod_0(aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.BlockData_0.props[global::_003CModule_003E.smethod_28<string>(1785901943u)]));
				}
			}
		});
	}

	internal static bool smethod_0(UnityEngine.Object object_0, UnityEngine.Object object_1)
	{
		return object_0 == object_1;
	}

	internal static Mesh smethod_1(MeshFilter meshFilter_0)
	{
		return meshFilter_0.mesh;
	}
}
