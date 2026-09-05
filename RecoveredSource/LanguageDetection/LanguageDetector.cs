using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace LanguageDetection;

public class LanguageDetector
{
	public class DetectedLanguage
	{
		[CompilerGenerated]
		private string FAlV7cSLB_0024XskOtzmQZtjt0_0024Y6CvZO25fUJmCuSsTanJ;

		[CompilerGenerated]
		private double double_0;

		public string Language
		{
			[CompilerGenerated]
			get
			{
				return FAlV7cSLB_0024XskOtzmQZtjt0_0024Y6CvZO25fUJmCuSsTanJ;
			}
			[CompilerGenerated]
			set
			{
				FAlV7cSLB_0024XskOtzmQZtjt0_0024Y6CvZO25fUJmCuSsTanJ = value;
			}
		}

		public double Probability
		{
			[CompilerGenerated]
			get
			{
				return double_0;
			}
			[CompilerGenerated]
			set
			{
				double_0 = value;
			}
		}
	}

	private class Class11
	{
		public const int iPMhE0MO_00246ltW0AKWzH4Wd4 = 3;

		private StringBuilder BLPJFJswLTR_0024hBLwWmAP5sg = smethod_5(global::_003CModule_003E.smethod_26<string>(1847872584u), 3);

		private bool JE4CgItG9JPDAzh05IlB_0024XI;

		public void method_0(char char_0)
		{
			char c = smethod_1(BLPJFJswLTR_0024hBLwWmAP5sg, smethod_0(BLPJFJswLTR_0024hBLwWmAP5sg) - 1);
			if (c == ' ')
			{
				BLPJFJswLTR_0024hBLwWmAP5sg = smethod_2(global::_003CModule_003E.smethod_25<string>(3135962215u));
				JE4CgItG9JPDAzh05IlB_0024XI = false;
				if (char_0 == ' ')
				{
					return;
				}
			}
			else if (smethod_0(BLPJFJswLTR_0024hBLwWmAP5sg) >= 3)
			{
				smethod_3(BLPJFJswLTR_0024hBLwWmAP5sg, 0, 1);
			}
			smethod_4(BLPJFJswLTR_0024hBLwWmAP5sg, char_0);
			if (char.IsUpper(char_0))
			{
				if (char.IsUpper(c))
				{
					JE4CgItG9JPDAzh05IlB_0024XI = true;
				}
			}
			else
			{
				JE4CgItG9JPDAzh05IlB_0024XI = false;
			}
		}

		public string IhPlpxaqqom9ZQxwqXlBmR4(int int_0)
		{
			if (JE4CgItG9JPDAzh05IlB_0024XI)
			{
				return null;
			}
			if (int_0 < 1 || int_0 > 3 || smethod_0(BLPJFJswLTR_0024hBLwWmAP5sg) < int_0)
			{
				return null;
			}
			if (int_0 != 1)
			{
				return BLPJFJswLTR_0024hBLwWmAP5sg.ToString(BLPJFJswLTR_0024hBLwWmAP5sg.Length - int_0, int_0);
			}
			char c = smethod_1(BLPJFJswLTR_0024hBLwWmAP5sg, smethod_0(BLPJFJswLTR_0024hBLwWmAP5sg) - 1);
			if (c != ' ')
			{
				return c.ToString();
			}
			return null;
		}

		internal static int smethod_0(StringBuilder stringBuilder_0)
		{
			return stringBuilder_0.Length;
		}

		internal static char smethod_1(StringBuilder stringBuilder_0, int int_0)
		{
			return stringBuilder_0[int_0];
		}

		internal static StringBuilder smethod_2(string string_0)
		{
			return new StringBuilder(string_0);
		}

		internal static StringBuilder smethod_3(StringBuilder stringBuilder_0, int int_0, int int_1)
		{
			return stringBuilder_0.Remove(int_0, int_1);
		}

		internal static StringBuilder smethod_4(StringBuilder stringBuilder_0, char char_0)
		{
			return stringBuilder_0.Append(char_0);
		}

		internal static StringBuilder smethod_5(string string_0, int int_0)
		{
			return new StringBuilder(string_0, int_0);
		}
	}

	internal static readonly Regex LKwO2Hvn_0024jbVPnC3ziMW9JQ = smethod_17(global::_003CModule_003E.smethod_26<string>(523139298u), RegexOptions.Compiled);

	internal static readonly Regex y7y_l_0024_Rp82xz8Pgt8wN_0024tI = smethod_17(global::_003CModule_003E.smethod_29<string>(2390564949u), RegexOptions.Compiled);

	private const string C8lNVfhW4etiJefM1sto2L6HsyKdzjZCKm5gRMn4u2Kq = "LanguageDetection.Profiles.";

	private List<VBGvGFOsmw5aR9Pc8Td1LSYJPgq8Le3DRGORTCrjoJvo8tol2e0JowjQUMlAiUUeoQ> G7Z66FTM8KxLiaNng_00241mmZE;

	private Dictionary<string, Dictionary<VBGvGFOsmw5aR9Pc8Td1LSYJPgq8Le3DRGORTCrjoJvo8tol2e0JowjQUMlAiUUeoQ, double>> _0024d9LvQO8jZ5xkzbupIUUnEnnIBTm8aKMpH1wRzbvUGku;

	[CompilerGenerated]
	private double cp99D05Je984pVZlfbZ34S2l26BMcrc4qQc7ZFRPSBIg;

	[CompilerGenerated]
	private int? DOpDUcpzcIxhUYbfGkDhp87npbsZK7sM5IGj0L8pyGxS;

	[CompilerGenerated]
	private int ndc_UefcbXlCMsRco1KAQWDdKIRtd_QVTMEAu6P8nfKk;

	[CompilerGenerated]
	private int X5cjpLbv87N3Z_frge3w4UUVO5U0N_j7mKVz9gknrxqh;

	[CompilerGenerated]
	private int F4LtQyjxfUR6P_0024cGiuArGgQDcDYEHj07i5cetAvr9eS_0024;

	[CompilerGenerated]
	private double mEIPZGuMOTzvql4KbizwYNbmEiApWmbf__hI5GOOvEBf;

	[CompilerGenerated]
	private int int_0;

	[CompilerGenerated]
	private double _0024td0J7j7QZx2_0024bcsKo9IPWqts4lGpqfIvw5HVES8NmVfcNCIwc6VeTyaubuFjXi9Bg;

	[CompilerGenerated]
	private double QgKFgMVLWmVXUMgHIdgJRoKifXRUO7jmJPOLQXtOQUsXQ91ILg3Qjl_0024rjbRczWG_NQ;

	[CompilerGenerated]
	private int Y0m0gQuaR_wGsH5vs_9W8scVlEIOMTfmgfPN_gGxnDZi;

	public double Alpha
	{
		[CompilerGenerated]
		get
		{
			return cp99D05Je984pVZlfbZ34S2l26BMcrc4qQc7ZFRPSBIg;
		}
		[CompilerGenerated]
		set
		{
			cp99D05Je984pVZlfbZ34S2l26BMcrc4qQc7ZFRPSBIg = value;
		}
	}

	public int? RandomSeed
	{
		[CompilerGenerated]
		get
		{
			return DOpDUcpzcIxhUYbfGkDhp87npbsZK7sM5IGj0L8pyGxS;
		}
		[CompilerGenerated]
		set
		{
			DOpDUcpzcIxhUYbfGkDhp87npbsZK7sM5IGj0L8pyGxS = value;
		}
	}

	public int Trials
	{
		[CompilerGenerated]
		get
		{
			return ndc_UefcbXlCMsRco1KAQWDdKIRtd_QVTMEAu6P8nfKk;
		}
		[CompilerGenerated]
		set
		{
			ndc_UefcbXlCMsRco1KAQWDdKIRtd_QVTMEAu6P8nfKk = value;
		}
	}

	public int NGramLength
	{
		[CompilerGenerated]
		get
		{
			return X5cjpLbv87N3Z_frge3w4UUVO5U0N_j7mKVz9gknrxqh;
		}
		[CompilerGenerated]
		set
		{
			X5cjpLbv87N3Z_frge3w4UUVO5U0N_j7mKVz9gknrxqh = value;
		}
	}

	public int MaxTextLength
	{
		[CompilerGenerated]
		get
		{
			return F4LtQyjxfUR6P_0024cGiuArGgQDcDYEHj07i5cetAvr9eS_0024;
		}
		[CompilerGenerated]
		set
		{
			F4LtQyjxfUR6P_0024cGiuArGgQDcDYEHj07i5cetAvr9eS_0024 = value;
		}
	}

	public double AlphaWidth
	{
		[CompilerGenerated]
		get
		{
			return mEIPZGuMOTzvql4KbizwYNbmEiApWmbf__hI5GOOvEBf;
		}
		[CompilerGenerated]
		set
		{
			mEIPZGuMOTzvql4KbizwYNbmEiApWmbf__hI5GOOvEBf = value;
		}
	}

	public int MaxIterations
	{
		[CompilerGenerated]
		get
		{
			return int_0;
		}
		[CompilerGenerated]
		set
		{
			int_0 = value;
		}
	}

	public double ProbabilityThreshold
	{
		[CompilerGenerated]
		get
		{
			return _0024td0J7j7QZx2_0024bcsKo9IPWqts4lGpqfIvw5HVES8NmVfcNCIwc6VeTyaubuFjXi9Bg;
		}
		[CompilerGenerated]
		set
		{
			_0024td0J7j7QZx2_0024bcsKo9IPWqts4lGpqfIvw5HVES8NmVfcNCIwc6VeTyaubuFjXi9Bg = value;
		}
	}

	public double ConvergenceThreshold
	{
		[CompilerGenerated]
		get
		{
			return QgKFgMVLWmVXUMgHIdgJRoKifXRUO7jmJPOLQXtOQUsXQ91ILg3Qjl_0024rjbRczWG_NQ;
		}
		[CompilerGenerated]
		set
		{
			QgKFgMVLWmVXUMgHIdgJRoKifXRUO7jmJPOLQXtOQUsXQ91ILg3Qjl_0024rjbRczWG_NQ = value;
		}
	}

	public int BaseFrequency
	{
		[CompilerGenerated]
		get
		{
			return Y0m0gQuaR_wGsH5vs_9W8scVlEIOMTfmgfPN_gGxnDZi;
		}
		[CompilerGenerated]
		set
		{
			Y0m0gQuaR_wGsH5vs_9W8scVlEIOMTfmgfPN_gGxnDZi = value;
		}
	}

	public LanguageDetector()
	{
		AlphaWidth = 0.05;
		MaxIterations = 1000;
		ProbabilityThreshold = 0.1;
		ConvergenceThreshold = 0.99999;
		BaseFrequency = 10000;
		Alpha = 0.5;
		RandomSeed = null;
		Trials = 7;
		NGramLength = 3;
		MaxTextLength = 10000;
		G7Z66FTM8KxLiaNng_00241mmZE = new List<VBGvGFOsmw5aR9Pc8Td1LSYJPgq8Le3DRGORTCrjoJvo8tol2e0JowjQUMlAiUUeoQ>();
		_0024d9LvQO8jZ5xkzbupIUUnEnnIBTm8aKMpH1wRzbvUGku = new Dictionary<string, Dictionary<VBGvGFOsmw5aR9Pc8Td1LSYJPgq8Le3DRGORTCrjoJvo8tol2e0JowjQUMlAiUUeoQ, double>>();
	}

	public void AddAllLanguages()
	{
		AddLanguage(KH64M3TewhA_Yw2TjbFeRV_kcghAaUSHXuDlWZpBfh_lhm_0024Y_0024No6GYBMPMIyT_0024M8ZQ.I523h_LJbgQYnRZqhKijnGA);
		AddLanguage(KH64M3TewhA_Yw2TjbFeRV_kcghAaUSHXuDlWZpBfh_lhm_0024Y_0024No6GYBMPMIyT_0024M8ZQ.KtrU2EMYwD0cGg6QfqkwejQ);
		AddLanguage(KH64M3TewhA_Yw2TjbFeRV_kcghAaUSHXuDlWZpBfh_lhm_0024Y_0024No6GYBMPMIyT_0024M8ZQ.JZEC7_0024OI_0024U8rvt9Os3HudsY);
		AddLanguage(KH64M3TewhA_Yw2TjbFeRV_kcghAaUSHXuDlWZpBfh_lhm_0024Y_0024No6GYBMPMIyT_0024M8ZQ.NshHLQIavXivYYhxSlti7Eg);
		AddLanguage(KH64M3TewhA_Yw2TjbFeRV_kcghAaUSHXuDlWZpBfh_lhm_0024Y_0024No6GYBMPMIyT_0024M8ZQ.UTIkthwZSM2n7ikw1_a3qsk);
	}

	public void AddLanguage(byte[] data)
	{
		MemoryStream memoryStream = smethod_2(data);
		try
		{
			Stream stream = smethod_3((Stream)memoryStream, CompressionMode.Decompress);
			try
			{
				VBGvGFOsmw5aR9Pc8Td1LSYJPgq8Le3DRGORTCrjoJvo8tol2e0JowjQUMlAiUUeoQ vBGvGFOsmw5aR9Pc8Td1LSYJPgq8Le3DRGORTCrjoJvo8tol2e0JowjQUMlAiUUeoQ = new VBGvGFOsmw5aR9Pc8Td1LSYJPgq8Le3DRGORTCrjoJvo8tol2e0JowjQUMlAiUUeoQ();
				vBGvGFOsmw5aR9Pc8Td1LSYJPgq8Le3DRGORTCrjoJvo8tol2e0JowjQUMlAiUUeoQ.cqWoMNveroNrLO3XzL3B_0024XA(stream);
				PLf_0024GHdbnZJLGyvPu9hYdc78PUTqSvzbZxxTE15IYOS1(vBGvGFOsmw5aR9Pc8Td1LSYJPgq8Le3DRGORTCrjoJvo8tol2e0JowjQUMlAiUUeoQ);
			}
			finally
			{
				if (stream != null)
				{
					smethod_4((IDisposable)stream);
				}
			}
		}
		finally
		{
			if (memoryStream != null)
			{
				smethod_4((IDisposable)memoryStream);
			}
		}
	}

	private void PLf_0024GHdbnZJLGyvPu9hYdc78PUTqSvzbZxxTE15IYOS1(VBGvGFOsmw5aR9Pc8Td1LSYJPgq8Le3DRGORTCrjoJvo8tol2e0JowjQUMlAiUUeoQ vbgvGFOsmw5aR9Pc8Td1LSYJPgq8Le3DRGORTCrjoJvo8tol2e0JowjQUMlAiUUeoQ_0)
	{
		G7Z66FTM8KxLiaNng_00241mmZE.Add(vbgvGFOsmw5aR9Pc8Td1LSYJPgq8Le3DRGORTCrjoJvo8tol2e0JowjQUMlAiUUeoQ_0);
		foreach (string key in vbgvGFOsmw5aR9Pc8Td1LSYJPgq8Le3DRGORTCrjoJvo8tol2e0JowjQUMlAiUUeoQ_0.ymwbvtpGl4UoHAavMEc2HZ4.Keys)
		{
			if (!_0024d9LvQO8jZ5xkzbupIUUnEnnIBTm8aKMpH1wRzbvUGku.ContainsKey(key))
			{
				_0024d9LvQO8jZ5xkzbupIUUnEnnIBTm8aKMpH1wRzbvUGku[key] = new Dictionary<VBGvGFOsmw5aR9Pc8Td1LSYJPgq8Le3DRGORTCrjoJvo8tol2e0JowjQUMlAiUUeoQ, double>();
			}
			if (smethod_5(key) >= 1 && smethod_5(key) <= NGramLength)
			{
				double value = (double)vbgvGFOsmw5aR9Pc8Td1LSYJPgq8Le3DRGORTCrjoJvo8tol2e0JowjQUMlAiUUeoQ_0.ymwbvtpGl4UoHAavMEc2HZ4[key] / (double)vbgvGFOsmw5aR9Pc8Td1LSYJPgq8Le3DRGORTCrjoJvo8tol2e0JowjQUMlAiUUeoQ_0.TQ4hiwix8LFuA8cl7gL3MDU[smethod_5(key) - 1];
				_0024d9LvQO8jZ5xkzbupIUUnEnnIBTm8aKMpH1wRzbvUGku[key][vbgvGFOsmw5aR9Pc8Td1LSYJPgq8Le3DRGORTCrjoJvo8tol2e0JowjQUMlAiUUeoQ_0] = value;
			}
		}
	}

	public string Detect(string text)
	{
		return DetectAll(text).FirstOrDefault()?.Language;
	}

	public IEnumerable<DetectedLanguage> DetectAll(string text)
	{
		List<string> list = B5TM3oU5nnTLvRZ7i72pcns(NormalizeText(text));
		if (list.Count != 0)
		{
			double[] array = new double[G7Z66FTM8KxLiaNng_00241mmZE.Count];
			Random random_ = (RandomSeed.HasValue ? smethod_7(RandomSeed.Value) : smethod_6());
			for (int i = 0; i < Trials; i++)
			{
				double[] array2 = k04XU9zflG4P2nR8_RW1SAwX9khe7NKVSvdWkFrdb7Y9();
				double double_ = Alpha + smethod_8(random_) * AlphaWidth;
				int num = 0;
				while (true)
				{
					int index = smethod_9(random_, list.Count);
					VrOSGUVwtzHL1PnvPdGDmrfzx8Z5f1wVWGO_0024pf1ZjAHK(array2, list[index], double_);
					if (num % 5 != 0 || (!(smethod_1(array2) > ConvergenceThreshold) && num < MaxIterations))
					{
						num++;
						continue;
					}
					break;
				}
				for (int j = 0; j < array.Length; j++)
				{
					array[j] += array2[j] / (double)Trials;
				}
			}
			return vSy4fmonQ_0024dFILE4E2MPRMVIij6hhqcqa3Fk0h7m8zm0(array);
		}
		return new DetectedLanguage[0];
	}

	private List<string> B5TM3oU5nnTLvRZ7i72pcns(string string_0)
	{
		List<string> list = new List<string>();
		Class11 @class = new Class11();
		for (int i = 0; i < smethod_5(string_0); i++)
		{
			char char_ = smethod_10(string_0, i);
			@class.method_0(char_);
			for (int j = 1; j <= 3; j++)
			{
				string text = @class.IhPlpxaqqom9ZQxwqXlBmR4(j);
				if (text != null && _0024d9LvQO8jZ5xkzbupIUUnEnnIBTm8aKMpH1wRzbvUGku.ContainsKey(text))
				{
					list.Add(text);
				}
			}
		}
		return list;
	}

	protected virtual string NormalizeText(string text)
	{
		if (smethod_5(text) > MaxTextLength)
		{
			text = smethod_11(text, 0, MaxTextLength);
		}
		text = smethod_0(text);
		text = hMhCfT4bZN5qtFvwbqImYD_0024xTWiJtCbNF3eIM66BxCGr(text);
		text = qHTIWtFXPXHb2Mkx8cDz8_vS4clN_FA0Bm7GdHYo2S1i(text);
		text = Oki34qvqo2cicWSU5460EssmaJYgGDwKvmyjhY_C6vPG(text);
		return text;
	}

	private static string hMhCfT4bZN5qtFvwbqImYD_0024xTWiJtCbNF3eIM66BxCGr(string string_0)
	{
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < smethod_5(string_0); i++)
		{
			char c = smethod_10(string_0, i);
			if (c <= 'z' && c >= 'A')
			{
				num++;
			}
			else if (c >= '\u0300' && (c < 'Ḁ' || c > 'ỿ'))
			{
				num2++;
			}
		}
		if (num * 2 < num2)
		{
			StringBuilder stringBuilder = smethod_12();
			for (int j = 0; j < smethod_5(string_0); j++)
			{
				char c2 = smethod_10(string_0, j);
				if (c2 > 'z' || c2 < 'A')
				{
					smethod_13(stringBuilder, c2);
				}
			}
			string_0 = smethod_14((object)stringBuilder);
		}
		return string_0;
	}

	private static string qHTIWtFXPXHb2Mkx8cDz8_vS4clN_FA0Bm7GdHYo2S1i(string string_0)
	{
		return string_0;
	}

	private static string Oki34qvqo2cicWSU5460EssmaJYgGDwKvmyjhY_C6vPG(string string_0)
	{
		StringBuilder stringBuilder = smethod_15(smethod_5(string_0));
		char? c = null;
		for (int i = 0; i < smethod_5(string_0); i++)
		{
			char c2 = smethod_10(string_0, i);
			if (c2 != ' ' || c != ' ')
			{
				smethod_13(stringBuilder, c2);
			}
			c = c2;
		}
		return smethod_14((object)stringBuilder);
	}

	internal static string smethod_0(string string_0)
	{
		string_0 = smethod_16(LKwO2Hvn_0024jbVPnC3ziMW9JQ, string_0, global::_003CModule_003E.smethod_28<string>(2654009246u));
		string_0 = smethod_16(y7y_l_0024_Rp82xz8Pgt8wN_0024tI, string_0, global::_003CModule_003E.smethod_26<string>(1847872584u));
		return string_0;
	}

	private double[] k04XU9zflG4P2nR8_RW1SAwX9khe7NKVSvdWkFrdb7Y9()
	{
		double[] array = new double[G7Z66FTM8KxLiaNng_00241mmZE.Count];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = 1.0 / (double)G7Z66FTM8KxLiaNng_00241mmZE.Count;
		}
		return array;
	}

	private void VrOSGUVwtzHL1PnvPdGDmrfzx8Z5f1wVWGO_0024pf1ZjAHK(double[] double_0, string string_0, double double_1)
	{
		if (string_0 != null && _0024d9LvQO8jZ5xkzbupIUUnEnnIBTm8aKMpH1wRzbvUGku.ContainsKey(string_0))
		{
			Dictionary<VBGvGFOsmw5aR9Pc8Td1LSYJPgq8Le3DRGORTCrjoJvo8tol2e0JowjQUMlAiUUeoQ, double> dictionary = _0024d9LvQO8jZ5xkzbupIUUnEnnIBTm8aKMpH1wRzbvUGku[string_0];
			double num = double_1 / (double)BaseFrequency;
			for (int i = 0; i < double_0.Length; i++)
			{
				VBGvGFOsmw5aR9Pc8Td1LSYJPgq8Le3DRGORTCrjoJvo8tol2e0JowjQUMlAiUUeoQ key = G7Z66FTM8KxLiaNng_00241mmZE[i];
				double_0[i] *= num + (dictionary.ContainsKey(key) ? dictionary[key] : 0.0);
			}
		}
	}

	internal static double smethod_1(double[] double_0)
	{
		double num = 0.0;
		double num2 = 0.0;
		for (int i = 0; i < double_0.Length; i++)
		{
			num2 += double_0[i];
		}
		for (int j = 0; j < double_0.Length; j++)
		{
			double num3 = double_0[j] / num2;
			if (num < num3)
			{
				num = num3;
			}
			double_0[j] = num3;
		}
		return num;
	}

	private IEnumerable<DetectedLanguage> vSy4fmonQ_0024dFILE4E2MPRMVIij6hhqcqa3Fk0h7m8zm0(double[] double_0)
	{
		List<DetectedLanguage> list = new List<DetectedLanguage>();
		for (int i = 0; i < double_0.Length; i++)
		{
			double num = double_0[i];
			if (!(num > ProbabilityThreshold))
			{
				continue;
			}
			for (int j = 0; j <= list.Count; j++)
			{
				if (j == list.Count || !(list[j].Probability >= num))
				{
					list.Insert(j, new DetectedLanguage
					{
						Language = G7Z66FTM8KxLiaNng_00241mmZE[i].iQXQlC5p5DB4hkwaqZnLmP4,
						Probability = num
					});
					break;
				}
			}
		}
		return list;
	}

	internal static MemoryStream smethod_2(byte[] byte_0)
	{
		return new MemoryStream(byte_0);
	}

	internal static GZipStream smethod_3(Stream stream_0, CompressionMode compressionMode_0)
	{
		return new GZipStream(stream_0, compressionMode_0);
	}

	internal static void smethod_4(IDisposable idisposable_0)
	{
		idisposable_0.Dispose();
	}

	internal static int smethod_5(string string_0)
	{
		return string_0.Length;
	}

	internal static Random smethod_6()
	{
		return new Random();
	}

	internal static Random smethod_7(int int_1)
	{
		return new Random(int_1);
	}

	internal static double smethod_8(Random random_0)
	{
		return random_0.NextDouble();
	}

	internal static int smethod_9(Random random_0, int int_1)
	{
		return random_0.Next(int_1);
	}

	internal static char smethod_10(string string_0, int int_1)
	{
		return string_0[int_1];
	}

	internal static string smethod_11(string string_0, int int_1, int int_2)
	{
		return string_0.Substring(int_1, int_2);
	}

	internal static StringBuilder smethod_12()
	{
		return new StringBuilder();
	}

	internal static StringBuilder smethod_13(StringBuilder stringBuilder_0, char char_0)
	{
		return stringBuilder_0.Append(char_0);
	}

	internal static string smethod_14(object object_0)
	{
		return object_0.ToString();
	}

	internal static StringBuilder smethod_15(int int_1)
	{
		return new StringBuilder(int_1);
	}

	internal static string smethod_16(Regex regex_0, string string_0, string string_1)
	{
		return regex_0.Replace(string_0, string_1);
	}

	internal static Regex smethod_17(string string_0, RegexOptions regexOptions_0)
	{
		return new Regex(string_0, regexOptions_0);
	}
}
