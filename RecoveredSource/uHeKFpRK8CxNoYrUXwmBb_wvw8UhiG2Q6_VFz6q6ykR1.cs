using System.Collections.Generic;
using MPatchrMain;
using UnityEngine;

internal static class uHeKFpRK8CxNoYrUXwmBb_wvw8UhiG2Q6_VFz6q6ykR1
{
	private static Dictionary<string, AudioClip> KwSGNekH_GrR_00245D8lX7qnf8 = new Dictionary<string, AudioClip>();

	internal static void RTyDolIM2931YSJW3Hap4C4(string string_0, float float_0 = 1f)
	{
		AudioSource audioSource = MPatchr.xcBvxcM_0024ckBeZyvdSoAkJoM.GetComponent<AudioSource>();
		if (smethod_0((Object)audioSource, (Object)null))
		{
			audioSource = smethod_1((Component)MPatchr.xcBvxcM_0024ckBeZyvdSoAkJoM).AddComponent<AudioSource>();
			smethod_2(audioSource, 1f);
			smethod_3(audioSource, 1f);
		}
		if (!KwSGNekH_GrR_00245D8lX7qnf8.ContainsKey(string_0))
		{
			KwSGNekH_GrR_00245D8lX7qnf8.Add(string_0, smethod_5(smethod_4(global::_003CModule_003E.smethod_27<string>(3007694919u), string_0)) as AudioClip);
		}
		smethod_6(audioSource);
		if (float_0 < 0f)
		{
			smethod_8(audioSource, smethod_7(KwSGNekH_GrR_00245D8lX7qnf8[string_0]));
		}
		else
		{
			smethod_8(audioSource, 0f);
		}
		smethod_9(audioSource, float_0);
		smethod_10(audioSource, 1f);
		smethod_11(audioSource, KwSGNekH_GrR_00245D8lX7qnf8[string_0]);
		smethod_12(audioSource);
	}

	internal static bool smethod_0(Object object_0, Object object_1)
	{
		return object_0 == object_1;
	}

	internal static GameObject smethod_1(Component component_0)
	{
		return component_0.gameObject;
	}

	internal static void smethod_2(AudioSource audioSource_0, float float_0)
	{
		audioSource_0.minDistance = float_0;
	}

	internal static void smethod_3(AudioSource audioSource_0, float float_0)
	{
		audioSource_0.maxDistance = float_0;
	}

	internal static string smethod_4(string string_0, string string_1)
	{
		return string_0 + string_1;
	}

	internal static Object smethod_5(string string_0)
	{
		return Resources.Load(string_0);
	}

	internal static void smethod_6(AudioSource audioSource_0)
	{
		audioSource_0.Stop();
	}

	internal static float smethod_7(AudioClip audioClip_0)
	{
		return audioClip_0.length;
	}

	internal static void smethod_8(AudioSource audioSource_0, float float_0)
	{
		audioSource_0.time = float_0;
	}

	internal static void smethod_9(AudioSource audioSource_0, float float_0)
	{
		audioSource_0.pitch = float_0;
	}

	internal static void smethod_10(AudioSource audioSource_0, float float_0)
	{
		audioSource_0.volume = float_0;
	}

	internal static void smethod_11(AudioSource audioSource_0, AudioClip audioClip_0)
	{
		audioSource_0.clip = audioClip_0;
	}

	internal static void smethod_12(AudioSource audioSource_0)
	{
		audioSource_0.Play();
	}
}
