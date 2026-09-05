using System;
using System.IO;
using HarmonyLib;
using UnityEngine;

[HarmonyPatch("FOODDBIFPGK")]
[HarmonyPatch(new Type[] { typeof(string) })]
[HarmonyPatch(typeof(TextureLoader))]
internal class psdRdI7hc081t9dJJ5z_0024OdB8xbQGLZVsl590oxhjP7f4VlD_n3_0024h4tuXN_0024gEeO_qnA
{
	internal static readonly float cNpCOVrnTHBotobXpn4SoqVUaM6jzYImF684aCRdTXWu = 2f;

	[HarmonyPrefix]
	internal static bool smethod_0(string LKOPHLJEHMM, ref byte[] __result, ref TextureLoader __instance)
	{
		float num = (float)smethod_2(smethod_1(LKOPHLJEHMM)) / 1024f / 1024f;
		if (num > cNpCOVrnTHBotobXpn4SoqVUaM6jzYImF684aCRdTXWu)
		{
			DP.D(global::_003CModule_003E.smethod_26<string>(2859779801u) + num + global::_003CModule_003E.smethod_27<string>(1430423396u) + cNpCOVrnTHBotobXpn4SoqVUaM6jzYImF684aCRdTXWu + global::_003CModule_003E.smethod_27<string>(1583484277u));
			return false;
		}
		byte[] array = File.ReadAllBytes(LKOPHLJEHMM);
		Texture2D texture2D = new Texture2D(0, 0, TextureFormat.ARGB32, mipmap: true);
		if (!texture2D.LoadImage(array))
		{
			DP.D(global::_003CModule_003E.smethod_28<string>(843813379u) + LKOPHLJEHMM + global::_003CModule_003E.smethod_26<string>(602007596u));
			__result = null;
			return false;
		}
		int width = texture2D.width;
		int height = texture2D.height;
		if ((width & 7) != 0 || (height & 7) != 0)
		{
			if (!HelpDefs.isJ)
			{
				DP.D(global::_003CModule_003E.smethod_26<string>(3886597932u) + width + global::_003CModule_003E.smethod_27<string>(3283291137u) + height + global::_003CModule_003E.smethod_29<string>(2132683108u));
			}
			else
			{
				DP.D(global::_003CModule_003E.smethod_29<string>(1583476392u) + width + global::_003CModule_003E.smethod_27<string>(3283291137u) + height + global::_003CModule_003E.smethod_29<string>(2132683108u));
			}
		}
		if ((bool)__instance.CIPOPAGDJDE)
		{
			__instance.CIPOPAGDJDE.SetStampTexture(texture2D);
		}
		DP.C(global::_003CModule_003E.smethod_28<string>(1784568080u) + LKOPHLJEHMM + global::_003CModule_003E.smethod_27<string>(1583484277u));
		__result = array;
		return false;
	}

	internal static FileInfo smethod_1(string string_0)
	{
		return new FileInfo(string_0);
	}

	internal static long smethod_2(FileInfo fileInfo_0)
	{
		return fileInfo_0.Length;
	}
}
