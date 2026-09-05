using System;
using System.Diagnostics;
using System.Reflection;
using HarmonyLib;
using UnityEngine;
using UnityEngine.SceneManagement;

internal class fyuG4GW3IraSRLT0CLbDKQ09fF3B9ergVMo51j9Rl0_0024r5ffufWNoL4IzyNljW0l7_0024w
{
	[HarmonyPatch(typeof(Build))]
	[HarmonyPatch(new Type[]
	{
		typeof(bool),
		typeof(bool)
	})]
	[HarmonyPatch("ANDINIMKBLL")]
	internal class Iy0yDHSokn7A1acUSob1vPaa5JfPGtXOyI2wMz0l1hdznKe08xppB3tj0JMHUhj3dWO8i5ptFcclTjSfPVgCCWdFuwwdtbfnYkiPRGFYUYP0uYVAyrHEiSH_0024hooW8m1wtg
	{
		[HarmonyPrefix]
		internal static void smethod_0(bool DAAPHICBEBN, bool HDCALLBGDLA)
		{
			if (!DdyZZFeYiqqAGWhntQSIWGo())
			{
				return;
			}
			StackFrame[] array = smethod_2(smethod_1());
			foreach (StackFrame stackFrame_ in array)
			{
				if (smethod_4((MemberInfo)smethod_3(stackFrame_)) == smethod_5(typeof(aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw).TypeHandle) || smethod_4((MemberInfo)smethod_3(stackFrame_)) == smethod_5(typeof(Class30).TypeHandle) || (smethod_7(smethod_6((MemberInfo)smethod_3(stackFrame_)), global::_003CModule_003E.smethod_26<string>(463499989u)) && (smethod_8(stackFrame_) == WVz2BWgPo5uZh3mbBcxtuunXICDlsLedWznXmNz4_YRQ || smethod_8(stackFrame_) == int_0)))
				{
					return;
				}
			}
			aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.V_NGBHwDrkTOfihwsi3eOOBhNumbeCNe_jj52dboz3u2();
		}

		internal static StackTrace smethod_1()
		{
			return new StackTrace();
		}

		internal static StackFrame[] smethod_2(StackTrace stackTrace_0)
		{
			return stackTrace_0.GetFrames();
		}

		internal static MethodBase smethod_3(StackFrame stackFrame_0)
		{
			return stackFrame_0.GetMethod();
		}

		internal static Type smethod_4(MemberInfo memberInfo_0)
		{
			return memberInfo_0.DeclaringType;
		}

		internal static Type smethod_5(RuntimeTypeHandle runtimeTypeHandle_0)
		{
			return Type.GetTypeFromHandle(runtimeTypeHandle_0);
		}

		internal static string smethod_6(MemberInfo memberInfo_0)
		{
			return memberInfo_0.Name;
		}

		internal static bool smethod_7(string string_0, string string_1)
		{
			return string_0.Contains(string_1);
		}

		internal static int smethod_8(StackFrame stackFrame_0)
		{
			return stackFrame_0.GetNativeOffset();
		}
	}

	[HarmonyPatch(new Type[]
	{
		typeof(string),
		typeof(GameObject)
	})]
	[HarmonyPatch(typeof(Build))]
	[HarmonyPatch("GMKBKFPBKPF")]
	internal class xKBuYdyDbBHJjstz9dcxj1vO43xpZUHAF3yv1i95Db01bNK4TLZ2Mnx5CrTh_xmAsGU4e1s5Eep_00247KSnrQAWGLd9r9h9T_0024oODPdVEAB8p85B
	{
		[HarmonyPrefix]
		internal static void smethod_0(string DPGKEOAGONA, GameObject NGLBLAGMBLN)
		{
			if (DdyZZFeYiqqAGWhntQSIWGo())
			{
				aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.V_NGBHwDrkTOfihwsi3eOOBhNumbeCNe_jj52dboz3u2();
			}
		}
	}

	[HarmonyPatch("OnSelect")]
	[HarmonyPatch(new Type[]
	{
		typeof(string),
		typeof(GameObject)
	})]
	[HarmonyPatch(typeof(Build))]
	internal class xW0dccGv_zVG8iMOKUmaxtTqNmFR3cN5v9KW3QD_lpQ3M22P1lFTdVMQg4FHkTOzASgItAABjWhwPVNZ9t_dxRXJoy0LYEPnl0zAGSKshXjY
	{
		[HarmonyPrefix]
		internal static void smethod_0(string DPGKEOAGONA, GameObject NGLBLAGMBLN)
		{
			if (DdyZZFeYiqqAGWhntQSIWGo())
			{
				aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.V_NGBHwDrkTOfihwsi3eOOBhNumbeCNe_jj52dboz3u2();
			}
		}
	}

	[HarmonyPatch(new Type[]
	{
		typeof(string),
		typeof(float)
	})]
	[HarmonyPatch(typeof(KEFHJCGICLE))]
	[HarmonyPatch("HNAHBIMJDCB")]
	internal class F3z9FVn3k2nDHv5n4doF0Uxn9J_3B2jIciT1Z0tAhMSMpvI4_0024JonvQDmiL_0024TvKx1yMQgKSjetA7g6G2CXU2FPWn4z1JW1w_0024eMHEQeYVj8ytU_aFpIcurBv8gZtpWHsavOA
	{
		[HarmonyPrefix]
		internal static void smethod_0(string CBNCLLHJONG, float NDHDALEEEOP)
		{
			if (!DdyZZFeYiqqAGWhntQSIWGo() || smethod_1().name != global::_003CModule_003E.smethod_26<string>(4276480652u))
			{
				return;
			}
			StackFrame[] frames = new StackTrace().GetFrames();
			for (int i = 0; i < frames.Length; i++)
			{
				if (frames[i].GetMethod().DeclaringType == typeof(aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw))
				{
					return;
				}
			}
			mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_28<string>(921499815u) + CBNCLLHJONG, bool_0: true);
			if (CBNCLLHJONG == global::_003CModule_003E.smethod_27<string>(341977588u) || CBNCLLHJONG == global::_003CModule_003E.smethod_29<string>(224042444u))
			{
				aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.V_NGBHwDrkTOfihwsi3eOOBhNumbeCNe_jj52dboz3u2();
			}
		}

		internal static Scene smethod_1()
		{
			return SceneManager.GetActiveScene();
		}
	}

	[HarmonyPatch("AddBlockData")]
	[HarmonyPatch(new Type[] { typeof(BlockData) })]
	[HarmonyPatch(typeof(BuildData))]
	internal class s5IyDQwGN4oJ7sCRj3JwQiURoQQgzMLO1h_7Nv1d6aknGhYzXJ4dMoJVg0y_UVRY0zkSuVKBAHwEJ8ZTggZ4fSRUoXyDbDMjsxMHAjzyd2XYkwSDCOZ5RKDY9WP9PBZl9Q
	{
		[HarmonyPostfix]
		internal static void FeUAVwFbW6wGJJdNimZY9yI(BlockData data)
		{
			if (DdyZZFeYiqqAGWhntQSIWGo())
			{
				aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.K_0024vpUUzAbQGz74d7UWL6CDA(data);
			}
		}
	}

	[HarmonyPatch("RemoveBlockData")]
	[HarmonyPatch(typeof(BuildData))]
	[HarmonyPatch(new Type[] { typeof(BlockData) })]
	internal class Class31
	{
		[HarmonyPostfix]
		internal static void FeUAVwFbW6wGJJdNimZY9yI(BlockData data)
		{
			if (!DdyZZFeYiqqAGWhntQSIWGo())
			{
				return;
			}
			StackFrame[] array = smethod_1(smethod_0());
			int num = 0;
			while (true)
			{
				if (num < array.Length)
				{
					if (!smethod_4(smethod_3((MemberInfo)smethod_2(array[num])), global::_003CModule_003E.smethod_27<string>(2319615818u)))
					{
						num++;
						continue;
					}
					break;
				}
				aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.smethod_3(data);
				break;
			}
		}

		internal static StackTrace smethod_0()
		{
			return new StackTrace();
		}

		internal static StackFrame[] smethod_1(StackTrace stackTrace_0)
		{
			return stackTrace_0.GetFrames();
		}

		internal static MethodBase smethod_2(StackFrame stackFrame_0)
		{
			return stackFrame_0.GetMethod();
		}

		internal static string smethod_3(MemberInfo memberInfo_0)
		{
			return memberInfo_0.Name;
		}

		internal static bool smethod_4(string string_0, string string_1)
		{
			return string_0.Contains(string_1);
		}
	}

	[HarmonyPatch(typeof(SceneMan))]
	[HarmonyPatch("OnJoinedLobby")]
	internal class Class32
	{
		[HarmonyPrefix]
		internal static bool smethod_0(SceneMan __instance)
		{
			return smethod_1((object)__instance) != smethod_2(typeof(Build).TypeHandle);
		}

		internal static Type smethod_1(object object_0)
		{
			return object_0.GetType();
		}

		internal static Type smethod_2(RuntimeTypeHandle runtimeTypeHandle_0)
		{
			return Type.GetTypeFromHandle(runtimeTypeHandle_0);
		}
	}

	[HarmonyPatch(typeof(Build))]
	[HarmonyPatch(new Type[]
	{
		typeof(int),
		typeof(int)
	})]
	[HarmonyPatch("KOGCGKIBAFN")]
	internal class DzasVnZU3q62SUdBxyHmufs3d1qiuDogSOVepunmMl95gKwar2bJ_78Z7f8wnLUWI2xlPk1tjGqw2JOuju77lpZ6AuoR_0024n2nQJUA_00246bURj0x
	{
		[HarmonyPrefix]
		internal static void smethod_0(int BINMCLLNJDC, int AIKCLAEMCHF)
		{
			aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.V_NGBHwDrkTOfihwsi3eOOBhNumbeCNe_jj52dboz3u2();
		}
	}

	[HarmonyPatch("BDKIMPEDKCJ")]
	[HarmonyPatch(typeof(Build))]
	internal class xW0dccGv_zVG8iMOKUmaxtTqNmFR3cN5v9KW3QD_lpQ3M22P1lFTdVMQg4FHkTOzASgItAABjWhwPVNZ9t_dxRVLtKVh8Y1OLdfL7bvb1mypYk0mO_0024Ry8xs4vNCvmFAnfA
	{
		[HarmonyPrefix]
		internal static bool smethod_0(string DPGKEOAGONA, Build __instance)
		{
			if (!DdyZZFeYiqqAGWhntQSIWGo())
			{
				return true;
			}
			aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.V_NGBHwDrkTOfihwsi3eOOBhNumbeCNe_jj52dboz3u2();
			return true;
		}
	}

	private static readonly int WVz2BWgPo5uZh3mbBcxtuunXICDlsLedWznXmNz4_YRQ = 4638;

	private static readonly int int_0 = 9734;

	private static bool DdyZZFeYiqqAGWhntQSIWGo()
	{
		if (smethod_0((UnityEngine.Object)aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw._mLDDjXswSCGaR35tynAvAg, (UnityEngine.Object)null))
		{
			if (aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw._mLDDjXswSCGaR35tynAvAg.hn0l0GNFrsPTtJLnYPdC5kufoQdroygW0c_0024coK_0024bqiHUyUKuNGxxEWa6iWJgwKxAk5py8zqyEY7YQFC_01oXiVU_0 != Class30.Hn0l0GNFrsPTtJLnYPdC5kufoQdroygW0c_0024coK_0024bqiHUyUKuNGxxEWa6iWJgwKxAk5py8zqyEY7YQFC_01oXiVU.connectedToRoom)
			{
				return aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw._mLDDjXswSCGaR35tynAvAg.hn0l0GNFrsPTtJLnYPdC5kufoQdroygW0c_0024coK_0024bqiHUyUKuNGxxEWa6iWJgwKxAk5py8zqyEY7YQFC_01oXiVU_0 == Class30.Hn0l0GNFrsPTtJLnYPdC5kufoQdroygW0c_0024coK_0024bqiHUyUKuNGxxEWa6iWJgwKxAk5py8zqyEY7YQFC_01oXiVU.hostingRoom;
			}
			return true;
		}
		return false;
	}

	internal static bool smethod_0(UnityEngine.Object object_0, UnityEngine.Object object_1)
	{
		return object_0 != object_1;
	}
}
