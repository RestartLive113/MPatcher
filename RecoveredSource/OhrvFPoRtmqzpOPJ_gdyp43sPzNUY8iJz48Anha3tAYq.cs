using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using HarmonyLib;

internal static class OhrvFPoRtmqzpOPJ_gdyp43sPzNUY8iJz48Anha3tAYq
{
	[CompilerGenerated]
	private sealed class KTQCACg3icogzMlCM8_0024XUqx3P7xQkEv3bRN_uvLRfVNyq7TzmF1pIMTNtiXeCrpI4Mk0ssBE7Hzqaih_7sTRBH4
	{
		public string r7lh6mDSmj85joUg2_0024t28Sg;

		internal bool VzVH9Ss3RYwRaKJUVX9jGgkmxjk9yTahM7T_nxtBpcpb(Type type_0)
		{
			if (!smethod_1(smethod_0(type_0), r7lh6mDSmj85joUg2_0024t28Sg))
			{
				return smethod_1(smethod_2((MemberInfo)type_0), r7lh6mDSmj85joUg2_0024t28Sg);
			}
			return true;
		}

		internal static string smethod_0(Type type_0)
		{
			return type_0.FullName;
		}

		internal static bool smethod_1(string string_0, string string_1)
		{
			return string_0 == string_1;
		}

		internal static string smethod_2(MemberInfo memberInfo_0)
		{
			return memberInfo_0.Name;
		}
	}

	public static List<Type> j9O0UPvu4fmbYepM3CYIC_0024s = new List<Type>();

	public static void MIO3Ksr2DNhtW5NmqDuIAyo(this Harmony harmony_0, Type type_0)
	{
		if (j9O0UPvu4fmbYepM3CYIC_0024s.Contains(type_0))
		{
			return;
		}
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(smethod_1(global::_003CModule_003E.smethod_25<string>(1210017450u), smethod_0(type_0)), bool_0: true);
		try
		{
			smethod_3(smethod_2(harmony_0, type_0));
			j9O0UPvu4fmbYepM3CYIC_0024s.Add(type_0);
		}
		catch (Exception exception_)
		{
			mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(smethod_5(global::_003CModule_003E.smethod_28<string>(1440223049u), smethod_4((MemberInfo)type_0), global::_003CModule_003E.smethod_29<string>(2132683108u)));
			mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(smethod_5(smethod_6(exception_), global::_003CModule_003E.smethod_27<string>(2085947933u), smethod_7(exception_)));
			if (smethod_8(exception_) != null)
			{
				mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(smethod_9(global::_003CModule_003E.smethod_27<string>(1565243994u), smethod_6(smethod_8(exception_)), global::_003CModule_003E.smethod_25<string>(1308721947u), smethod_7(smethod_8(exception_))));
			}
		}
	}

	public static Type qchzh7DyBAJlR8QH4jej1O0(string string_0)
	{
		return smethod_10().p3nbSpNsjKdbH6O8i2Fww0ahj07ygzoRqiSdbQqKuD59().FirstOrDefault((Type type_0) => KTQCACg3icogzMlCM8_0024XUqx3P7xQkEv3bRN_uvLRfVNyq7TzmF1pIMTNtiXeCrpI4Mk0ssBE7Hzqaih_7sTRBH4.smethod_1(KTQCACg3icogzMlCM8_0024XUqx3P7xQkEv3bRN_uvLRfVNyq7TzmF1pIMTNtiXeCrpI4Mk0ssBE7Hzqaih_7sTRBH4.smethod_0(type_0), string_0) || KTQCACg3icogzMlCM8_0024XUqx3P7xQkEv3bRN_uvLRfVNyq7TzmF1pIMTNtiXeCrpI4Mk0ssBE7Hzqaih_7sTRBH4.smethod_1(KTQCACg3icogzMlCM8_0024XUqx3P7xQkEv3bRN_uvLRfVNyq7TzmF1pIMTNtiXeCrpI4Mk0ssBE7Hzqaih_7sTRBH4.smethod_2((MemberInfo)type_0), string_0));
	}

	internal static string smethod_0(Type type_0)
	{
		return type_0.FullName;
	}

	internal static string smethod_1(string string_0, string string_1)
	{
		return string_0 + string_1;
	}

	internal static PatchClassProcessor smethod_2(Harmony harmony_0, Type type_0)
	{
		return harmony_0.ProcessorForAnnotatedClass(type_0);
	}

	internal static List<MethodInfo> smethod_3(PatchClassProcessor patchClassProcessor_0)
	{
		return patchClassProcessor_0.Patch();
	}

	internal static string smethod_4(MemberInfo memberInfo_0)
	{
		return memberInfo_0.Name;
	}

	internal static string smethod_5(string string_0, string string_1, string string_2)
	{
		return string_0 + string_1 + string_2;
	}

	internal static string smethod_6(Exception exception_0)
	{
		return exception_0.Message;
	}

	internal static string smethod_7(Exception exception_0)
	{
		return exception_0.StackTrace;
	}

	internal static Exception smethod_8(Exception exception_0)
	{
		return exception_0.InnerException;
	}

	internal static string smethod_9(string string_0, string string_1, string string_2, string string_3)
	{
		return string_0 + string_1 + string_2 + string_3;
	}

	internal static Assembly smethod_10()
	{
		return Assembly.GetExecutingAssembly();
	}
}
