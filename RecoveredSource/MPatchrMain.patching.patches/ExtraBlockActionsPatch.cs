using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using HarmonyLib;
using UnityEngine;

namespace MPatchrMain.patching.patches;

public static class ExtraBlockActionsPatch
{
	[HarmonyPatch(typeof(JKGKJLLFMLE))]
	[HarmonyPatch("BOMAFGLNGMI")]
	public static class saveMachinePatch
	{
		[HarmonyPrefix]
		internal static void smethod_0()
		{
			BuildData hHGILAIOCLG = JKGKJLLFMLE.HHGILAIOCLG;
			for (int i = 0; i < hHGILAIOCLG.blockData.Count; i++)
			{
				if (hHGILAIOCLG.blockData[i].actionID.Length <= 8)
				{
					BlockData bd = hHGILAIOCLG.blockData[i];
					oIuuMMCo9UmUIWmYFQEqxXAOJy8LYqspmW2FKyHMiXKdcT1cBP8p9QxEsmrfao0MIw.h42VsxFMP4yzP0oyRVsymO0(ref bd, global::_003CModule_003E.smethod_29<string>(4013122621u));
					oIuuMMCo9UmUIWmYFQEqxXAOJy8LYqspmW2FKyHMiXKdcT1cBP8p9QxEsmrfao0MIw.h42VsxFMP4yzP0oyRVsymO0(ref bd, global::_003CModule_003E.smethod_26<string>(1006937578u));
					hHGILAIOCLG.blockData[i] = bd;
					continue;
				}
				int[] array = new int[hHGILAIOCLG.blockData[i].actionID.Length - 8];
				int[] array2 = new int[hHGILAIOCLG.blockData[i].actionID.Length - 8];
				smethod_1((Array)hHGILAIOCLG.blockData[i].actionID, 8, (Array)array, 0, array.Length);
				smethod_1((Array)hHGILAIOCLG.blockData[i].actionParam, 8, (Array)array2, 0, array2.Length);
				Array.Resize(ref hHGILAIOCLG.blockData[i].actionID, 8);
				Array.Resize(ref hHGILAIOCLG.blockData[i].actionParam, 8);
				BlockData bd2 = hHGILAIOCLG.blockData[i];
				oIuuMMCo9UmUIWmYFQEqxXAOJy8LYqspmW2FKyHMiXKdcT1cBP8p9QxEsmrfao0MIw.ioLu5ucx0cm9MvyfVTOkUsE(ref bd2, global::_003CModule_003E.smethod_28<string>(3516188269u), smethod_2(global::_003CModule_003E.smethod_27<string>(95179017u), array.Select((int j) => j.ToString()).ToArray()));
				oIuuMMCo9UmUIWmYFQEqxXAOJy8LYqspmW2FKyHMiXKdcT1cBP8p9QxEsmrfao0MIw.ioLu5ucx0cm9MvyfVTOkUsE(ref bd2, global::_003CModule_003E.smethod_29<string>(3234572550u), smethod_2(global::_003CModule_003E.smethod_29<string>(713854848u), array2.Select((int j) => j.ToString()).ToArray()));
				hHGILAIOCLG.blockData[i] = bd2;
			}
		}

		internal static void smethod_1(Array array_0, int int_0, Array array_1, int int_1, int int_2)
		{
			Array.Copy(array_0, int_0, array_1, int_1, int_2);
		}

		internal static string smethod_2(string string_0, string[] string_1)
		{
			return string.Join(string_0, string_1);
		}
	}

	[HarmonyPatch("JMDPCIBHEML")]
	[HarmonyPatch(typeof(JKGKJLLFMLE))]
	public static class loadMachinePatch
	{
		[Serializable]
		[CompilerGenerated]
		private sealed class Fo1SSdEf4pn7sb1x_spzI1urOBZPQxR_WF92v6CeNoPnf2SwGwP1svZZccfpxy7nZqrrgKImSwAb6jE6PcnOM2c09Iuc4EB8WylALhVxMVP9
		{
			public static readonly Fo1SSdEf4pn7sb1x_spzI1urOBZPQxR_WF92v6CeNoPnf2SwGwP1svZZccfpxy7nZqrrgKImSwAb6jE6PcnOM2c09Iuc4EB8WylALhVxMVP9 _003C_003E9 = new Fo1SSdEf4pn7sb1x_spzI1urOBZPQxR_WF92v6CeNoPnf2SwGwP1svZZccfpxy7nZqrrgKImSwAb6jE6PcnOM2c09Iuc4EB8WylALhVxMVP9();

			public static Func<string, int> _003C_003E9__0_0;

			public static Func<string, int> _003C_003E9__0_1;

			internal int aJB1bIYDRm_0024alz4_00246Y6_epI(string j)
			{
				return int.Parse(j);
			}

			internal int aZBYf4fLREbw1bozUAMM0UA(string j)
			{
				return int.Parse(j);
			}
		}

		[HarmonyPostfix]
		private static void FeUAVwFbW6wGJJdNimZY9yI()
		{
			BuildData hHGILAIOCLG = JKGKJLLFMLE.HHGILAIOCLG;
			for (int i = 0; i < hHGILAIOCLG.blockData.Count; i++)
			{
				BlockData bd = hHGILAIOCLG.blockData[i];
				int[] array;
				int[] array2;
				if (!oIuuMMCo9UmUIWmYFQEqxXAOJy8LYqspmW2FKyHMiXKdcT1cBP8p9QxEsmrfao0MIw.xGPuOxA2rBW91iXaZdgcO18(bd, global::_003CModule_003E.smethod_27<string>(4250554288u)) || !oIuuMMCo9UmUIWmYFQEqxXAOJy8LYqspmW2FKyHMiXKdcT1cBP8p9QxEsmrfao0MIw.xGPuOxA2rBW91iXaZdgcO18(bd, global::_003CModule_003E.smethod_28<string>(2909072860u)))
				{
					array = new int[0];
					array2 = new int[0];
				}
				else
				{
					array = (from j in smethod_0(oIuuMMCo9UmUIWmYFQEqxXAOJy8LYqspmW2FKyHMiXKdcT1cBP8p9QxEsmrfao0MIw.AIE60Kky2bU2sqqKbuxgR7LcNb5l6bc5BCXD6nzVkX_0024t(ref bd, global::_003CModule_003E.smethod_25<string>(1064283162u), ""), new char[1] { ',' })
						select int.Parse(j)).ToArray();
					array2 = (from j in smethod_0(oIuuMMCo9UmUIWmYFQEqxXAOJy8LYqspmW2FKyHMiXKdcT1cBP8p9QxEsmrfao0MIw.AIE60Kky2bU2sqqKbuxgR7LcNb5l6bc5BCXD6nzVkX_0024t(ref bd, global::_003CModule_003E.smethod_28<string>(2909072860u), ""), new char[1] { ',' })
						select int.Parse(j)).ToArray();
				}
				if (array.Length == 0)
				{
					array = new int[int_0];
					for (int num = 0; num < int_0; num++)
					{
						array[num] = -1;
					}
				}
				if (array2.Length == 0)
				{
					array2 = new int[int_0];
				}
				if (array != null && array2 != null && array.Length != 0 && array2.Length != 0)
				{
					Array.Resize(ref hHGILAIOCLG.blockData[i].actionID, 8 + array.Length);
					Array.Resize(ref hHGILAIOCLG.blockData[i].actionParam, 8 + array2.Length);
					smethod_1((Array)array, 0, (Array)hHGILAIOCLG.blockData[i].actionID, 8, array.Length);
					smethod_1((Array)array2, 0, (Array)hHGILAIOCLG.blockData[i].actionParam, 8, array2.Length);
				}
			}
		}

		internal static string[] smethod_0(string string_0, char[] char_0)
		{
			return string_0.Split(char_0);
		}

		internal static void smethod_1(Array array_0, int int_0, Array array_1, int int_1, int int_2)
		{
			Array.Copy(array_0, int_0, array_1, int_1, int_2);
		}
	}

	[HarmonyPatch("OPMMCNOHEMC")]
	[HarmonyPatch(typeof(Build))]
	internal class v1JBKckAa1RFmn2CeELS4d1ZbkxeNQbR7gQOyBNV_JFafPSiBW0iw_00248XkkMsB8e_0024YCCKNCwhEQ8Ujkbw_0024dpdM1WCfRua6GbinPHakKxasqfB
	{
		public static void smethod_0(Build __instance)
		{
			if (__instance.i0duGIJ6M7BXC7c2cbeyvXM1hCrcoNKNJzlqDrhwjoxM<int[]>(global::_003CModule_003E.smethod_26<string>(2652438302u)).Length == 8)
			{
				__instance.D7dqMa4OGuhdUnXHjJZM6vrBYDmyw_0024HWYteNzGVsTh5k(global::_003CModule_003E.smethod_29<string>(3981040658u), new int[8 + int_0]);
			}
			if (__instance.i0duGIJ6M7BXC7c2cbeyvXM1hCrcoNKNJzlqDrhwjoxM<int[]>(global::_003CModule_003E.smethod_28<string>(2180593652u)).Length == 8)
			{
				__instance.D7dqMa4OGuhdUnXHjJZM6vrBYDmyw_0024HWYteNzGVsTh5k(global::_003CModule_003E.smethod_27<string>(527931203u), new int[8 + int_0]);
			}
			mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_25<string>(3290428641u) + __instance.i0duGIJ6M7BXC7c2cbeyvXM1hCrcoNKNJzlqDrhwjoxM<int[]>(global::_003CModule_003E.smethod_29<string>(3981040658u)).Length, bool_0: true);
			mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_29<string>(1038352889u) + __instance.i0duGIJ6M7BXC7c2cbeyvXM1hCrcoNKNJzlqDrhwjoxM<int[]>(global::_003CModule_003E.smethod_29<string>(432541470u)).Length, bool_0: true);
		}

		[HarmonyTranspiler]
		private static IEnumerable<CodeInstruction> ME8a9fXTPeYc1JTUjzrWW9E(IEnumerable<CodeInstruction> instructions)
		{
			CodeInstruction[] array = instructions.ToArray();
			List<CodeInstruction> list = new List<CodeInstruction>();
			IEnumerator<CodeInstruction> enumerator = instructions.GetEnumerator();
			try
			{
				while (smethod_1((IEnumerator)enumerator))
				{
					CodeInstruction current = enumerator.Current;
					if (current == array[46] || current == array[867])
					{
						current.opcode = OpCodes.Ldc_I4;
						current.operand = 8 + int_0;
					}
					list.Add(current);
				}
				return list;
			}
			finally
			{
				if (enumerator != null)
				{
					smethod_2((IDisposable)enumerator);
				}
			}
		}

		internal static bool smethod_1(IEnumerator ienumerator_0)
		{
			return ienumerator_0.MoveNext();
		}

		internal static void smethod_2(IDisposable idisposable_0)
		{
			idisposable_0.Dispose();
		}
	}

	[CompilerGenerated]
	private sealed class Class19
	{
		public int pJ3U40RCGcqoRpJswPvzmsc;

		internal void l01WKsnWcR47he2mo2fPI7uyiBUV1IRE291Nou6XXMGq(bool toggled)
		{
			if (toggled)
			{
				EIpNMx84xK7ve9mEuSBOLNs(pJ3U40RCGcqoRpJswPvzmsc);
			}
		}
	}

	private static readonly int int_0 = 40;

	private static List<GameObject[]> list_0;

	internal static void EIpNMx84xK7ve9mEuSBOLNs(int pgNum)
	{
		for (int i = 0; i < list_0.Count; i++)
		{
			GameObject[] array = list_0[i];
			for (int j = 0; j < array.Length; j++)
			{
				smethod_0(array[j], i == pgNum);
			}
		}
	}

	internal static void KX8jtCPdJb997Zi2Dvgkpuc(Build __instance)
	{
	}

	internal static void smethod_0(GameObject gameObject_0, bool bool_0)
	{
		gameObject_0.SetActive(bool_0);
	}
}
