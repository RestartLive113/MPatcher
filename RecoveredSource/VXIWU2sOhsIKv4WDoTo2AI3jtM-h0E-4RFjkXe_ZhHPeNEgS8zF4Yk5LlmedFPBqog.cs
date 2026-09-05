using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

internal static class VXIWU2sOhsIKv4WDoTo2AI3jtM_0024h0E_00244RFjkXe_ZhHPeNEgS8zF4Yk5LlmedFPBqog
{
	[CompilerGenerated]
	private sealed class JZMfhTRgs4YSq9_ASCjtZ5Dh8D3Lx422DTy_IeAwiXxTfSmir17hyjfmAiuwF73MjGiisYhteQ_rLEUTIEGo2rm4aUli4a1k0eFUFldNKlRW : IEnumerator<object>, IDisposable, IEnumerator
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
		public JZMfhTRgs4YSq9_ASCjtZ5Dh8D3Lx422DTy_IeAwiXxTfSmir17hyjfmAiuwF73MjGiisYhteQ_rLEUTIEGo2rm4aUli4a1k0eFUFldNKlRW(int int_0)
		{
			SjlBM8inVA_YE4YVlr_0024gluY = int_0;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			SjlBM8inVA_YE4YVlr_0024gluY = -2;
		}

		private bool MoveNext()
		{
			int num = SjlBM8inVA_YE4YVlr_0024gluY;
			if (num == 0)
			{
				SjlBM8inVA_YE4YVlr_0024gluY = -1;
			}
			else
			{
				if (num != 1)
				{
					return false;
				}
				SjlBM8inVA_YE4YVlr_0024gluY = -1;
				string[] array = smethod_1(global::_003CModule_003E.smethod_28<string>(1304448336u));
				for (int i = 0; i < array.Length; i++)
				{
					BVzwUED_lI8LYZ1VYbVfpY8_XEX8q1mymRkkFvCM9QtF72TvSNrVMo20Z_00247bZN8LUg.ZNfyS8TMNZ8c4kK7hxvORN8(smethod_2(array[i]));
				}
			}
			yT7HpVIzmqW54W307WgJtr4 = smethod_0(5f);
			SjlBM8inVA_YE4YVlr_0024gluY = 1;
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
			throw smethod_3();
		}

		internal static WaitForSecondsRealtime smethod_0(float float_0)
		{
			return new WaitForSecondsRealtime(float_0);
		}

		internal static string[] smethod_1(string string_0)
		{
			return Directory.GetFiles(string_0);
		}

		internal static string smethod_2(string string_0)
		{
			return Path.GetFileName(string_0);
		}

		internal static NotSupportedException smethod_3()
		{
			return new NotSupportedException();
		}
	}

	private static IEnumerator B0km_r8ubgyk9zH3HZknWq_0024iSkT_00244TVNLg0wbm8eZf9f()
	{
		while (true)
		{
			yield return JZMfhTRgs4YSq9_ASCjtZ5Dh8D3Lx422DTy_IeAwiXxTfSmir17hyjfmAiuwF73MjGiisYhteQ_rLEUTIEGo2rm4aUli4a1k0eFUFldNKlRW.smethod_0(5f);
			string[] array = JZMfhTRgs4YSq9_ASCjtZ5Dh8D3Lx422DTy_IeAwiXxTfSmir17hyjfmAiuwF73MjGiisYhteQ_rLEUTIEGo2rm4aUli4a1k0eFUFldNKlRW.smethod_1(global::_003CModule_003E.smethod_28<string>(1304448336u));
			for (int i = 0; i < array.Length; i++)
			{
				BVzwUED_lI8LYZ1VYbVfpY8_XEX8q1mymRkkFvCM9QtF72TvSNrVMo20Z_00247bZN8LUg.ZNfyS8TMNZ8c4kK7hxvORN8(JZMfhTRgs4YSq9_ASCjtZ5Dh8D3Lx422DTy_IeAwiXxTfSmir17hyjfmAiuwF73MjGiisYhteQ_rLEUTIEGo2rm4aUli4a1k0eFUFldNKlRW.smethod_2(array[i]));
			}
		}
	}
}
