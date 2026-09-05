using System;
using HarmonyLib;

namespace MPatchrMain.patching;

public class Patch
{
	public bool patched;

	public Action extraEnableActions;

	public Action extraDisableActions;

	public Type[] patchClasses;

	public string internalName;

	public string displayName;

	public void enable(Harmony harmony)
	{
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(smethod_0(global::_003CModule_003E.smethod_25<string>(2079663660u), internalName));
		if (!patched)
		{
			Type[] array = patchClasses;
			foreach (Type type_ in array)
			{
				harmony.MIO3Ksr2DNhtW5NmqDuIAyo(type_);
			}
			if (extraEnableActions != null)
			{
				extraEnableActions();
			}
			patched = true;
		}
	}

	public Patch(string internalName, string displayName, Type[] patchClasses, Action extraEnableActions = null, Action extraDisableActions = null)
	{
		this.patchClasses = patchClasses;
		this.extraEnableActions = extraEnableActions;
		this.extraDisableActions = extraDisableActions;
		this.internalName = internalName;
		this.displayName = displayName;
	}

	internal static string smethod_0(string string_0, string string_1)
	{
		return string_0 + string_1;
	}
}
