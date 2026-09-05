using System;
using HarmonyLib;
using MPatchrMain;
using UnityEngine;

[HarmonyPatch(typeof(Build))]
[HarmonyPatch("OPMMCNOHEMC")]
internal class GgrMl0eHjkTKzAqKC9Taf8i6N22UmQUCQv5QJo1e9j3Jw_0024HppxtR0oIfLMckJcgWz3YAZmq1UkdrbzocepnYQnM
{
	[HarmonyPostfix]
	internal static void FeUAVwFbW6wGJJdNimZY9yI()
	{
		try
		{
			GameObject panelSetup = smethod_0(global::_003CModule_003E.smethod_25<string>(806627754u)).smethod_0(global::_003CModule_003E.smethod_28<string>(1391506648u));
			BlockData blockData_ = aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.BlockData_0;
			BlockController component = aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.PE5sYlzltGGVQg4HL2k4HC8IHLb_pbX477PyFTi4cBWYR4CijFhQ7jSAn1NoFkOd_0024A.GetComponent<BlockController>();
			if (MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.scopeBodyHide)
			{
				zE60wVxOgZhBbFe_0024L2dQQnBpi9PrY5xO_LcZQVhXG_0024JnB399sbSEMqDfCXIKMB5QMYje4jK4C_HFa6uGRpqTj5fiCtnsPiP3e027YIjVAP0B.d0feay_b5lVTNLXp6cDGkn0(blockData_, component, panelSetup);
			}
			if (MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.graphicsPlus)
			{
				Class59.d0feay_b5lVTNLXp6cDGkn0(blockData_, component, panelSetup);
			}
			if (MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.customTrackerAim)
			{
				fWvgZH1Uh15yXdrDTaG_0024OCVvtntxUL5vVl5s_mKNw8ih7I1CqX7Tn9LLZlZaIp8eN6vfAJGyRIBM8HzaZS5chtbR4ozeMdp4xXQ_00241vwe7lP2.d0feay_b5lVTNLXp6cDGkn0(blockData_, component, panelSetup);
			}
		}
		catch (Exception exception_)
		{
			mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(smethod_3(global::_003CModule_003E.smethod_28<string>(1574515692u), smethod_1(exception_), global::_003CModule_003E.smethod_25<string>(1308721947u), smethod_2(exception_)));
		}
	}

	internal static GameObject smethod_0(string string_0)
	{
		return GameObject.Find(string_0);
	}

	internal static string smethod_1(Exception exception_0)
	{
		return exception_0.Message;
	}

	internal static string smethod_2(Exception exception_0)
	{
		return exception_0.StackTrace;
	}

	internal static string smethod_3(string string_0, string string_1, string string_2, string string_3)
	{
		return string_0 + string_1 + string_2 + string_3;
	}
}
