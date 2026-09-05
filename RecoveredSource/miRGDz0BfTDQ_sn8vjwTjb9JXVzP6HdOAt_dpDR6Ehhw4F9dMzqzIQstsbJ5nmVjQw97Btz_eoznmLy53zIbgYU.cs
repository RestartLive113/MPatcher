using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using HarmonyLib;
using MPatchrMain;
using McnCraft;
using UnityEngine;
using UnityEngine.SceneManagement;

[HarmonyPatch(typeof(MachineController))]
[HarmonyPatch("RPC_SyncPlayerName")]
internal static class miRGDz0BfTDQ_sn8vjwTjb9JXVzP6HdOAt_dpDR6Ehhw4F9dMzqzIQstsbJ5nmVjQw97Btz_eoznmLy53zIbgYU
{
	[CompilerGenerated]
	private sealed class BLDS1egckqqAMoijEMpSktaDLN2cxybFEugvMmbeE48iI3xbhWPhuFnbDan_00247sbVxJl7jVSZZ33kWlM9roMmU_0024_0024a2S7kd7ZtAIpw3tqthmti27bkmrDQY9BrQ_q8LM7L6_o52ehYV231zb9Jzz6TSbc : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int SjlBM8inVA_YE4YVlr_0024gluY;

		private object yT7HpVIzmqW54W307WgJtr4;

		public MachineController wQ6mrkDog7tAEXGul0Y8Sv0;

		public bool bool_0;

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
		public BLDS1egckqqAMoijEMpSktaDLN2cxybFEugvMmbeE48iI3xbhWPhuFnbDan_00247sbVxJl7jVSZZ33kWlM9roMmU_0024_0024a2S7kd7ZtAIpw3tqthmti27bkmrDQY9BrQ_q8LM7L6_o52ehYV231zb9Jzz6TSbc(int _003C_003E1__state)
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
			case 1:
				SjlBM8inVA_YE4YVlr_0024gluY = -1;
				break;
			default:
				return false;
			case 0:
				SjlBM8inVA_YE4YVlr_0024gluY = -1;
				break;
			}
			if (wQ6mrkDog7tAEXGul0Y8Sv0.KBLANAFAJFP.Count == 0)
			{
				yT7HpVIzmqW54W307WgJtr4 = smethod_0(1f);
				SjlBM8inVA_YE4YVlr_0024gluY = 1;
				return true;
			}
			if (!bool_0)
			{
				fM8akW_NCNObVpnHZDsn9ZpvaBT_00242EZI_0024IX7xApy8Ppu5lo_bKdEPM8rXjLtGIhLxvyaFhnCfADn6F2MZVB9G2w.ld6AXUgS2ayU6_0024pzUEyMQ2o_IqDuQEME8gZoDobzlZrQ(wQ6mrkDog7tAEXGul0Y8Sv0);
			}
			else
			{
				fM8akW_NCNObVpnHZDsn9ZpvaBT_00242EZI_0024IX7xApy8Ppu5lo_bKdEPM8rXjLtGIhLxvyaFhnCfADn6F2MZVB9G2w.Lpp9T_00243HIEfu54IStegVPWU(wQ6mrkDog7tAEXGul0Y8Sv0);
			}
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw smethod_1();
		}

		internal static WaitForSecondsRealtime smethod_0(float float_0)
		{
			return new WaitForSecondsRealtime(float_0);
		}

		internal static NotSupportedException smethod_1()
		{
			return new NotSupportedException();
		}
	}

	[HarmonyPostfix]
	internal static void FeUAVwFbW6wGJJdNimZY9yI(MachineController __instance, int IFHONJFMKEP, int AALDBPFJBMA)
	{
		if (JKGKJLLFMLE.EGFHGHKLNAO == JKGKJLLFMLE.LENPCAMMAEP.Meeting && !(smethod_0().name != global::_003CModule_003E.smethod_28<string>(695999064u)) && (MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.persistantBlock || MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.defaultCollisionsOff))
		{
			if (MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.blockedPlayers.ContainsKey(IFHONJFMKEP.ToString()) && MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.persistantBlock)
			{
				__instance.StartCoroutine(_00241JIHmjReVpWC8rsUt0eErQ(__instance, IFHONJFMKEP, AALDBPFJBMA, MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.blockedPlayers[IFHONJFMKEP.ToString()]));
			}
			else if (MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.defaultCollisionsOff)
			{
				__instance.StartCoroutine(_00241JIHmjReVpWC8rsUt0eErQ(__instance, IFHONJFMKEP, AALDBPFJBMA, block: false));
			}
		}
	}

	internal static IEnumerator _00241JIHmjReVpWC8rsUt0eErQ(MachineController __instance, int uuid, int plrId, bool block)
	{
		while (__instance.KBLANAFAJFP.Count == 0)
		{
			yield return BLDS1egckqqAMoijEMpSktaDLN2cxybFEugvMmbeE48iI3xbhWPhuFnbDan_00247sbVxJl7jVSZZ33kWlM9roMmU_0024_0024a2S7kd7ZtAIpw3tqthmti27bkmrDQY9BrQ_q8LM7L6_o52ehYV231zb9Jzz6TSbc.smethod_0(1f);
		}
		if (!block)
		{
			fM8akW_NCNObVpnHZDsn9ZpvaBT_00242EZI_0024IX7xApy8Ppu5lo_bKdEPM8rXjLtGIhLxvyaFhnCfADn6F2MZVB9G2w.ld6AXUgS2ayU6_0024pzUEyMQ2o_IqDuQEME8gZoDobzlZrQ(__instance);
		}
		else
		{
			fM8akW_NCNObVpnHZDsn9ZpvaBT_00242EZI_0024IX7xApy8Ppu5lo_bKdEPM8rXjLtGIhLxvyaFhnCfADn6F2MZVB9G2w.Lpp9T_00243HIEfu54IStegVPWU(__instance);
		}
	}

	internal static Scene smethod_0()
	{
		return SceneManager.GetActiveScene();
	}
}
