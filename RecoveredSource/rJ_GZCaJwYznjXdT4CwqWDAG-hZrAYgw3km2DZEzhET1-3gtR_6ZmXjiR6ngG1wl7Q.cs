using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using CSharpCompiler;
using UnityEngine;

internal static class rJ_GZCaJwYznjXdT4CwqWDAG_0024hZrAYgw3km2DZEzhET1_00243gtR_6ZmXjiR6ngG1wl7Q
{
	internal static Regex ReoV9UNIxeHlB1RbSEEF9vM = smethod_35(global::_003CModule_003E.smethod_25<string>(4036365726u), RegexOptions.IgnoreCase | RegexOptions.Compiled);

	public static string[] ggbxzN3ox2nm77Z9_LRy8Yc()
	{
		AppDomain appDomain_ = smethod_2();
		List<string> list = new List<string>();
		Assembly[] array = smethod_3(appDomain_);
		for (int i = 0; i < array.Length; i++)
		{
			if (!smethod_5(smethod_4(array[i]), global::_003CModule_003E.smethod_26<string>(3367824273u)) && !smethod_6(smethod_4(array[i]), global::_003CModule_003E.smethod_29<string>(4226323794u)) && !smethod_6(smethod_4(array[i]), global::_003CModule_003E.smethod_29<string>(4085667105u)) && !smethod_6(smethod_4(array[i]), global::_003CModule_003E.smethod_27<string>(679132149u)) && !smethod_6(smethod_4(array[i]), global::_003CModule_003E.smethod_26<string>(2245138240u)) && !smethod_6(smethod_4(array[i]), global::_003CModule_003E.smethod_28<string>(4141864388u)) && !smethod_6(smethod_4(array[i]), global::_003CModule_003E.smethod_27<string>(1914139514u)))
			{
				mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(smethod_8(global::_003CModule_003E.smethod_28<string>(342465199u), smethod_4(array[i]), global::_003CModule_003E.smethod_27<string>(3109622783u), smethod_7(array[i])), bool_0: true);
				list.Add(smethod_4(array[i]));
			}
		}
		return list.ToArray();
	}

	internal static CompilerResults smethod_0(string[] files)
	{
		CompilerParameters compilerParameters_ = smethod_9();
		smethod_11(smethod_10(compilerParameters_), ggbxzN3ox2nm77Z9_LRy8Yc());
		smethod_12(compilerParameters_, bool_0: true);
		smethod_13(compilerParameters_, bool_0: false);
		CompilerResults compilerResults = method_0(smethod_14(), compilerParameters_, files);
		IEnumerator enumerator = smethod_16((CollectionBase)smethod_15(compilerResults));
		try
		{
			while (smethod_21(enumerator))
			{
				string string_ = smethod_19(smethod_18(smethod_17(enumerator)), global::_003CModule_003E.smethod_29<string>(2917228930u), global::_003CModule_003E.smethod_29<string>(2776572241u));
				string_ = smethod_19(string_, global::_003CModule_003E.smethod_29<string>(1748790755u), global::_003CModule_003E.smethod_27<string>(385971988u));
				smethod_20(Arena.OEDCBNHNGMJ, string_, global::_003CModule_003E.smethod_27<string>(2566245304u), 0);
			}
			return compilerResults;
		}
		finally
		{
			if (enumerator is IDisposable idisposable_)
			{
				smethod_22(idisposable_);
			}
		}
	}

	public static CompilerResults smethod_1(string[] baseFiles)
	{
		List<string> reqFiles = new List<string>();
		string string_ = smethod_25(smethod_23(), 0, smethod_24(smethod_23(), '/'));
		string_ = smethod_26(string_, global::_003CModule_003E.smethod_26<string>(3646928177u));
		foreach (string file in baseFiles)
		{
			if (!SkFARYSxN8R_v7K86HT64QY(string_, file, ref reqFiles))
			{
				return null;
			}
		}
		return smethod_0(reqFiles.ToArray());
	}

	public static bool SkFARYSxN8R_v7K86HT64QY(string basefolder, string file, ref List<string> reqFiles)
	{
		file = smethod_19(smethod_19(file, global::_003CModule_003E.smethod_26<string>(3443209972u), ""), global::_003CModule_003E.smethod_26<string>(1607785091u), "");
		file = smethod_26(basefolder, file);
		if (!reqFiles.Contains(file))
		{
			reqFiles.Add(file);
			string string_ = smethod_27(file);
			string oIHFGPOPPDD = smethod_28(string_);
			if ((0u | (e84kbcMeXHjbVOm4MIR2I7WdpzdEPTaOhTtNGp4M8HYF(oIHFGPOPPDD, global::_003CModule_003E.smethod_28<string>(1253064209u), global::_003CModule_003E.smethod_28<string>(979884506u)) ? 1u : 0u) | (e84kbcMeXHjbVOm4MIR2I7WdpzdEPTaOhTtNGp4M8HYF(oIHFGPOPPDD, global::_003CModule_003E.smethod_27<string>(3042991890u), global::_003CModule_003E.smethod_26<string>(1199508858u)) ? 1u : 0u) | (e84kbcMeXHjbVOm4MIR2I7WdpzdEPTaOhTtNGp4M8HYF(oIHFGPOPPDD, global::_003CModule_003E.smethod_28<string>(2983202328u), global::_003CModule_003E.smethod_26<string>(1680660015u)) ? 1u : 0u) | (e84kbcMeXHjbVOm4MIR2I7WdpzdEPTaOhTtNGp4M8HYF(oIHFGPOPPDD, global::_003CModule_003E.smethod_28<string>(2345783021u), global::_003CModule_003E.smethod_25<string>(2939981839u)) ? 1u : 0u) | (e84kbcMeXHjbVOm4MIR2I7WdpzdEPTaOhTtNGp4M8HYF(oIHFGPOPPDD, global::_003CModule_003E.smethod_27<string>(525564013u), global::_003CModule_003E.smethod_27<string>(2889423754u)) ? 1u : 0u)) != 0)
			{
				return false;
			}
			IEnumerator enumerator = smethod_30(smethod_29(ReoV9UNIxeHlB1RbSEEF9vM, string_));
			try
			{
				while (smethod_21(enumerator))
				{
					Match match_ = (Match)smethod_17(enumerator);
					SkFARYSxN8R_v7K86HT64QY(basefolder, smethod_33((Capture)smethod_32(smethod_31(match_), 1)), ref reqFiles);
				}
			}
			finally
			{
				if (enumerator is IDisposable idisposable_)
				{
					smethod_22(idisposable_);
				}
			}
			return true;
		}
		return true;
	}

	private static bool e84kbcMeXHjbVOm4MIR2I7WdpzdEPTaOhTtNGp4M8HYF(string OIHFGPOPPDD, string AGEMKGMJDMI, string POMLGLIOFAB)
	{
		if (smethod_6(OIHFGPOPPDD, AGEMKGMJDMI))
		{
			smethod_20(Arena.OEDCBNHNGMJ, smethod_34(global::_003CModule_003E.smethod_28<string>(919128503u), POMLGLIOFAB, global::_003CModule_003E.smethod_28<string>(2740326523u)), global::_003CModule_003E.smethod_29<string>(2593000757u), 0);
			return true;
		}
		return false;
	}

	internal static AppDomain smethod_2()
	{
		return AppDomain.CurrentDomain;
	}

	internal static Assembly[] smethod_3(AppDomain appDomain_0)
	{
		return appDomain_0.GetAssemblies();
	}

	internal static string smethod_4(Assembly assembly_0)
	{
		return assembly_0.FullName;
	}

	internal static bool smethod_5(string string_0, string string_1)
	{
		return string_0.StartsWith(string_1);
	}

	internal static bool smethod_6(string string_0, string string_1)
	{
		return string_0.Contains(string_1);
	}

	internal static string smethod_7(Assembly assembly_0)
	{
		return assembly_0.Location;
	}

	internal static string smethod_8(string string_0, string string_1, string string_2, string string_3)
	{
		return string_0 + string_1 + string_2 + string_3;
	}

	internal static CompilerParameters smethod_9()
	{
		return new CompilerParameters();
	}

	internal static StringCollection smethod_10(CompilerParameters compilerParameters_0)
	{
		return compilerParameters_0.ReferencedAssemblies;
	}

	internal static void smethod_11(StringCollection stringCollection_0, string[] string_0)
	{
		stringCollection_0.AddRange(string_0);
	}

	internal static void smethod_12(CompilerParameters compilerParameters_0, bool bool_0)
	{
		compilerParameters_0.GenerateInMemory = bool_0;
	}

	internal static void smethod_13(CompilerParameters compilerParameters_0, bool bool_0)
	{
		compilerParameters_0.GenerateExecutable = bool_0;
	}

	internal static AMDKKOGEGJL smethod_14()
	{
		return new AMDKKOGEGJL();
	}

	static CompilerResults method_0(AMDKKOGEGJL provider, CompilerParameters compilerParameters_0, string[] string_0)
	{
		return provider.CompileAssemblyFromFileBatch(compilerParameters_0, string_0);
	}

	internal static CompilerErrorCollection smethod_15(CompilerResults compilerResults_0)
	{
		return compilerResults_0.Errors;
	}

	internal static IEnumerator smethod_16(CollectionBase collectionBase_0)
	{
		return collectionBase_0.GetEnumerator();
	}

	internal static object smethod_17(IEnumerator ienumerator_0)
	{
		return ienumerator_0.Current;
	}

	internal static string smethod_18(object object_0)
	{
		return object_0.ToString();
	}

	internal static string smethod_19(string string_0, string string_1, string string_2)
	{
		return string_0.Replace(string_1, string_2);
	}

	internal static void smethod_20(Arena arena_0, string string_0, string string_1, int int_0)
	{
		arena_0.AddScriptLog(string_0, string_1, int_0);
	}

	internal static bool smethod_21(IEnumerator ienumerator_0)
	{
		return ienumerator_0.MoveNext();
	}

	internal static void smethod_22(IDisposable idisposable_0)
	{
		idisposable_0.Dispose();
	}

	internal static string smethod_23()
	{
		return Application.dataPath;
	}

	internal static int smethod_24(string string_0, char char_0)
	{
		return string_0.LastIndexOf(char_0);
	}

	internal static string smethod_25(string string_0, int int_0, int int_1)
	{
		return string_0.Substring(int_0, int_1);
	}

	internal static string smethod_26(string string_0, string string_1)
	{
		return string_0 + string_1;
	}

	internal static string smethod_27(string string_0)
	{
		return File.ReadAllText(string_0);
	}

	internal static string smethod_28(string string_0)
	{
		return string_0.ToLower();
	}

	internal static MatchCollection smethod_29(Regex regex_0, string string_0)
	{
		return regex_0.Matches(string_0);
	}

	internal static IEnumerator smethod_30(MatchCollection matchCollection_0)
	{
		return matchCollection_0.GetEnumerator();
	}

	internal static GroupCollection smethod_31(Match match_0)
	{
		return match_0.Groups;
	}

	internal static Group smethod_32(GroupCollection groupCollection_0, int int_0)
	{
		return groupCollection_0[int_0];
	}

	internal static string smethod_33(Capture capture_0)
	{
		return capture_0.Value;
	}

	internal static string smethod_34(string string_0, string string_1, string string_2)
	{
		return string_0 + string_1 + string_2;
	}

	internal static Regex smethod_35(string string_0, RegexOptions regexOptions_0)
	{
		return new Regex(string_0, regexOptions_0);
	}
}
