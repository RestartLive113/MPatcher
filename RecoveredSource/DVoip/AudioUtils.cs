using UnityEngine;

namespace DVoip;

internal static class AudioUtils
{
	public static void ApplyGain(float[] samples, float gain)
	{
		for (int i = 0; i < samples.Length; i++)
		{
			samples[i] *= gain;
		}
	}

	public static float GetMaxAmplitude(float[] samples)
	{
		float num = 0f;
		for (int i = 0; i < samples.Length; i++)
		{
			num = Mathf.Max(num, Mathf.Abs(samples[i]));
		}
		return num;
	}

	public static BandMode GetMode(string deviceName = null)
	{
		int int_ = default(int);
		int int_2 = default(int);
		smethod_0(deviceName, ref int_, ref int_2);
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng("Device " + int_ + " -> " + int_2);
		if (int_ == GetFrequency(BandMode.UltraWide))
		{
			return BandMode.UltraWide;
		}
		if (int_ != GetFrequency(BandMode.Wide))
		{
			return BandMode.Narrow;
		}
		return BandMode.Wide;
	}

	public static int GetFrequency(BandMode mode)
	{
		return mode switch
		{
			BandMode.Narrow => 8000, 
			BandMode.Wide => 16000, 
			BandMode.UltraWide => 32000, 
			_ => 8000, 
		};
	}

	public static void Downsample(float[] source, float[] target)
	{
		int num = source.Length / target.Length;
		for (int i = 0; i < target.Length; i++)
		{
			target[i] = source[i * num];
		}
	}

	internal static void smethod_0(string string_0, ref int int_0, ref int int_1)
	{
		Microphone.GetDeviceCaps(string_0, out int_0, out int_1);
	}
}
