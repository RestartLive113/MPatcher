using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using HarmonyLib;
using MPatchrMain;
using UnityEngine;

[HarmonyPatch("OFCIKBMPJEL")]
[HarmonyPatch(typeof(JKGKJLLFMLE))]
internal class RF3hUfOyLY1kfDwv1hf0PRkQC5xzGOKEdcXE2nfCjXWkSQMIXJh4VtWAWsodkC_00242og
{
	[CompilerGenerated]
	private sealed class P_00245sAR74g7b1oZzOgelzWTcwg0j5pEl9Zr23nLespMdAQ6MxroYRuemPVDd5_0024lLySAMycVorZoZ3UUKzdZQZi6s : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int SjlBM8inVA_YE4YVlr_0024gluY;

		private object yT7HpVIzmqW54W307WgJtr4;

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
		public P_00245sAR74g7b1oZzOgelzWTcwg0j5pEl9Zr23nLespMdAQ6MxroYRuemPVDd5_0024lLySAMycVorZoZ3UUKzdZQZi6s(int _003C_003E1__state)
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
			switch (SjlBM8inVA_YE4YVlr_0024gluY)
			{
			default:
				return false;
			case 0:
				SjlBM8inVA_YE4YVlr_0024gluY = -1;
				yT7HpVIzmqW54W307WgJtr4 = smethod_0(0.1f);
				SjlBM8inVA_YE4YVlr_0024gluY = 1;
				return true;
			case 1:
				SjlBM8inVA_YE4YVlr_0024gluY = -1;
				mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_28<string>(767757220u));
				break;
			case 2:
				SjlBM8inVA_YE4YVlr_0024gluY = -1;
				break;
			}
			if (!MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.resizableWindow)
			{
				return false;
			}
			if (BorderlessWindow.getFramedWindow() == 348782592)
			{
				BorderlessWindow.SetFramedWindow();
			}
			yT7HpVIzmqW54W307WgJtr4 = smethod_1(1f);
			SjlBM8inVA_YE4YVlr_0024gluY = 2;
			return true;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw smethod_2();
		}

		internal static WaitForSeconds smethod_0(float float_0)
		{
			return new WaitForSeconds(float_0);
		}

		internal static WaitForSecondsRealtime smethod_1(float float_0)
		{
			return new WaitForSecondsRealtime(float_0);
		}

		internal static NotSupportedException smethod_2()
		{
			return new NotSupportedException();
		}
	}

	internal static int jitvyACtH8_0024OuAF27TVcILM;

	internal static int rzD0aRqGqbffGtPSpW9MBqY;

	[HarmonyPrefix]
	internal static bool smethod_0()
	{
		if (!MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.resizableWindow)
		{
			return true;
		}
		if (smethod_1() >= 5f)
		{
			smethod_2(JKGKJLLFMLE.IGOBPLOLHEP.screenWidth, JKGKJLLFMLE.IGOBPLOLHEP.screenHeight, bool_0: false);
			smethod_3((MonoBehaviour)MPatchr.xcBvxcM_0024ckBeZyvdSoAkJoM, Q7PcOPlQiL1v1u8rCD5zc6Y());
			return false;
		}
		return false;
	}

	internal static IEnumerator Q7PcOPlQiL1v1u8rCD5zc6Y()
	{
		yield return P_00245sAR74g7b1oZzOgelzWTcwg0j5pEl9Zr23nLespMdAQ6MxroYRuemPVDd5_0024lLySAMycVorZoZ3UUKzdZQZi6s.smethod_0(0.1f);
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_28<string>(767757220u));
		while (MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.resizableWindow)
		{
			if (BorderlessWindow.getFramedWindow() == 348782592)
			{
				BorderlessWindow.SetFramedWindow();
			}
			yield return P_00245sAR74g7b1oZzOgelzWTcwg0j5pEl9Zr23nLespMdAQ6MxroYRuemPVDd5_0024lLySAMycVorZoZ3UUKzdZQZi6s.smethod_1(1f);
		}
	}

	internal static float smethod_1()
	{
		return Time.unscaledTime;
	}

	internal static void smethod_2(int int_0, int int_1, bool bool_0)
	{
		Screen.SetResolution(int_0, int_1, bool_0);
	}

	internal static Coroutine smethod_3(MonoBehaviour monoBehaviour_0, IEnumerator ienumerator_0)
	{
		return monoBehaviour_0.StartCoroutine(ienumerator_0);
	}
}
