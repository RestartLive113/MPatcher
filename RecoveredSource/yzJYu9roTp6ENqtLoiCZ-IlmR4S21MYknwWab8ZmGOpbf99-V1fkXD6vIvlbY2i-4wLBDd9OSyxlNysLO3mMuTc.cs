using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using HarmonyLib;

[HarmonyPatch(typeof(Game))]
[HarmonyPatch("RPC_Chat")]
[HarmonyPatch(new Type[]
{
	typeof(string),
	typeof(int)
})]
internal class yzJYu9roTp6ENqtLoiCZ_0024IlmR4S21MYknwWab8ZmGOpbf99_0024V1fkXD6vIvlbY2i_00244wLBDd9OSyxlNysLO3mMuTc
{
	[HarmonyTranspiler]
	private static IEnumerable<CodeInstruction> ME8a9fXTPeYc1JTUjzrWW9E(IEnumerable<CodeInstruction> instructions)
	{
		CodeInstruction[] array = instructions.ToArray();
		List<CodeInstruction> list = new List<CodeInstruction>();
		List<CodeInstruction> list2 = new List<CodeInstruction>();
		list.Add(smethod_0(array[74]));
		list.Add(smethod_0(array[75]));
		array[75].operand = smethod_1(global::_003CModule_003E.smethod_27<string>(2533690740u), UUi9CES6jT0ZzKFkxbFyR2jEgvhxrm6WMabMV4KBwk4K_0024MAe7FKbv3UblWLVLXi1aw.string_0, global::_003CModule_003E.smethod_25<string>(714233622u));
		list.Add(smethod_0(array[76]));
		array[76].opcode = OpCodes.Ldstr;
		array[76].operand = global::_003CModule_003E.smethod_28<string>(1374872629u);
		list.Add(smethod_0(array[77]));
		list.Add(smethod_0(array[78]));
		IEnumerator<CodeInstruction> enumerator = instructions.GetEnumerator();
		try
		{
			while (smethod_2((IEnumerator)enumerator))
			{
				CodeInstruction current = enumerator.Current;
				if (current == array[79])
				{
					foreach (CodeInstruction item in list)
					{
						list2.Add(item);
					}
				}
				list2.Add(current);
			}
			return list2;
		}
		finally
		{
			if (enumerator != null)
			{
				smethod_3((IDisposable)enumerator);
			}
		}
	}

	internal static CodeInstruction smethod_0(CodeInstruction codeInstruction_0)
	{
		return codeInstruction_0.Clone();
	}

	internal static string smethod_1(string string_0, string string_1, string string_2)
	{
		return string_0 + string_1 + string_2;
	}

	internal static bool smethod_2(IEnumerator ienumerator_0)
	{
		return ienumerator_0.MoveNext();
	}

	internal static void smethod_3(IDisposable idisposable_0)
	{
		idisposable_0.Dispose();
	}
}
