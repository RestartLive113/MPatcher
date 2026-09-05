using System;
using System.Collections.Generic;

namespace RuntimeInspectorNamespace;

public class NumberHandlers
{
	private class attf7_jUXZSPLLBMhFItiU8sMgh5HcwGJlpu3SKSgmCBmzf1T6uN41k_0024GQFXTF5dI0FTAmv_0024KcCmeycxiMt19nE : INumberHandler
	{
		public float MinValue => -2.1474836E+09f;

		public float MaxValue => 2.1474836E+09f;

		public bool TryParse(string input, out object value)
		{
			int result2;
			bool result = int.TryParse(input, out result2);
			value = result2;
			return result;
		}

		public bool ValuesAreEqual(object value1, object value2)
		{
			return (int)value1 == (int)value2;
		}

		public object ConvertFromFloat(float value)
		{
			return (int)value;
		}

		public float ConvertToFloat(object value)
		{
			return (int)value;
		}
	}

	private class zc4t_0024WgroDEU7u2pH_0c8NVIGvzu_0024u8V0FVjNHlbDK1nfeDVh2HQoS1v9pQtvn79uP0_Q6FnXFOhX6Un9frXGmM : INumberHandler
	{
		public float MinValue => 0f;

		public float MaxValue => 4.2949673E+09f;

		public bool TryParse(string input, out object value)
		{
			uint result2;
			bool result = uint.TryParse(input, out result2);
			value = result2;
			return result;
		}

		public bool ValuesAreEqual(object value1, object value2)
		{
			return (uint)value1 == (uint)value2;
		}

		public object ConvertFromFloat(float value)
		{
			return (uint)value;
		}

		public float ConvertToFloat(object value)
		{
			return (uint)value;
		}
	}

	private class fzBT4wuFE1CR5_0024jtnIdDB6_yJQRkPSBM7mH50Ltsu_g4WzCiATSI5qPRArLZCX_wncwWK6ad3YM_0024BDu3STqLmpU : INumberHandler
	{
		public float MinValue => -9.223372E+18f;

		public float MaxValue => 9.223372E+18f;

		public bool TryParse(string input, out object value)
		{
			long result2;
			bool result = long.TryParse(input, out result2);
			value = result2;
			return result;
		}

		public bool ValuesAreEqual(object value1, object value2)
		{
			return (long)value1 == (long)value2;
		}

		public object ConvertFromFloat(float value)
		{
			return (long)value;
		}

		public float ConvertToFloat(object value)
		{
			return (long)value;
		}
	}

	private class qNUeqU_xJjfZ3UUor9k4IOdT_QSghpLoLPYO_0024Ak8JWYUemeTVwnYh4gaH46SyAIl5bgrHi7p_0024cKqWQnmRS9PXo0 : INumberHandler
	{
		public float MinValue => 0f;

		public float MaxValue => 1.8446744E+19f;

		public bool TryParse(string input, out object value)
		{
			ulong result2;
			bool result = ulong.TryParse(input, out result2);
			value = result2;
			return result;
		}

		public bool ValuesAreEqual(object value1, object value2)
		{
			return (ulong)value1 == (ulong)value2;
		}

		public object ConvertFromFloat(float value)
		{
			return (ulong)value;
		}

		public float ConvertToFloat(object value)
		{
			return (ulong)value;
		}
	}

	private class cxSmYUy1ZzlJeFRSGTNxZT9oTv1plv3H_pFSWlf2VcGMrDktbHHC0SOY1nbOXYTO_0024VsS1q2rAAYbgWTQdf0PHSs : INumberHandler
	{
		public float MinValue => 0f;

		public float MaxValue => 255f;

		public bool TryParse(string input, out object value)
		{
			byte result2;
			bool result = byte.TryParse(input, out result2);
			value = result2;
			return result;
		}

		public bool ValuesAreEqual(object value1, object value2)
		{
			return (byte)value1 == (byte)value2;
		}

		public object ConvertFromFloat(float value)
		{
			return (byte)value;
		}

		public float ConvertToFloat(object value)
		{
			return (int)(byte)value;
		}
	}

	private class pmq8n5AEpbeFNtPxbPbJzsrJ5tZJavUR4SWvHONyQBKkDiTs7IRCaZZ3aPiIlYw_0024sGPeA3LbtyPwuHx2aBB9IIU : INumberHandler
	{
		public float MinValue => -128f;

		public float MaxValue => 127f;

		public bool TryParse(string input, out object value)
		{
			sbyte result2;
			bool result = sbyte.TryParse(input, out result2);
			value = result2;
			return result;
		}

		public bool ValuesAreEqual(object value1, object value2)
		{
			return (sbyte)value1 == (sbyte)value2;
		}

		public object ConvertFromFloat(float value)
		{
			return (sbyte)value;
		}

		public float ConvertToFloat(object value)
		{
			return (sbyte)value;
		}
	}

	private class wcccneFUmmG5JvhCd3z9UmSeeGGZmvCL1B5CE4GogDY4jWKNRHaYlAjeyu05fdFEIM_rHbsNwA_Fmfn6FeB79d4 : INumberHandler
	{
		public float MinValue => -32768f;

		public float MaxValue => 32767f;

		public bool TryParse(string input, out object value)
		{
			short result2;
			bool result = short.TryParse(input, out result2);
			value = result2;
			return result;
		}

		public bool ValuesAreEqual(object value1, object value2)
		{
			return (short)value1 == (short)value2;
		}

		public object ConvertFromFloat(float value)
		{
			return (short)value;
		}

		public float ConvertToFloat(object value)
		{
			return (short)value;
		}
	}

	private class PKtvS66D3uJ6qTFuc18XmtHPYEdyV3r6WJ04peHYDm4rDum4fMx9TDMOUciHLf6Fe117Y3Ej1lxAN0BPWs9ngaM : INumberHandler
	{
		public float MinValue => 0f;

		public float MaxValue => 65535f;

		public bool TryParse(string input, out object value)
		{
			ushort result2;
			bool result = ushort.TryParse(input, out result2);
			value = result2;
			return result;
		}

		public bool ValuesAreEqual(object value1, object value2)
		{
			return (ushort)value1 == (ushort)value2;
		}

		public object ConvertFromFloat(float value)
		{
			return (ushort)value;
		}

		public float ConvertToFloat(object value)
		{
			return (int)(ushort)value;
		}
	}

	private class JZ9DOH9EFBOjUclNUroR_DFV8OTVNX8kBaA_cNatw42zjigklfKgpXL5m7WMqRDcaNoYpciC0dSWgmu6rTsvcek : INumberHandler
	{
		public float MinValue => 0f;

		public float MaxValue => 65535f;

		public bool TryParse(string input, out object value)
		{
			char result2;
			bool result = char.TryParse(input, out result2);
			value = result2;
			return result;
		}

		public bool ValuesAreEqual(object value1, object value2)
		{
			return (char)value1 == (char)value2;
		}

		public object ConvertFromFloat(float value)
		{
			return (char)value;
		}

		public float ConvertToFloat(object value)
		{
			return (int)(char)value;
		}
	}

	private class Class8 : INumberHandler
	{
		public float MinValue => float.MinValue;

		public float MaxValue => float.MaxValue;

		public bool TryParse(string input, out object value)
		{
			float result2;
			bool result = float.TryParse(input, out result2);
			value = result2;
			return result;
		}

		public bool ValuesAreEqual(object value1, object value2)
		{
			return (float)value1 == (float)value2;
		}

		public object ConvertFromFloat(float value)
		{
			return value;
		}

		public float ConvertToFloat(object value)
		{
			return (float)value;
		}
	}

	private class Class9 : INumberHandler
	{
		public float MinValue => float.MinValue;

		public float MaxValue => float.MaxValue;

		public bool TryParse(string input, out object value)
		{
			double result2;
			bool result = double.TryParse(input, out result2);
			value = result2;
			return result;
		}

		public bool ValuesAreEqual(object value1, object value2)
		{
			return (double)value1 == (double)value2;
		}

		public object ConvertFromFloat(float value)
		{
			return (double)value;
		}

		public float ConvertToFloat(object value)
		{
			return (float)(double)value;
		}
	}

	private class RNU33NuIOZztDIrXl8hRKDp2y9Q31j9RX_ZTVKl04z_v5WLc28mUtRx39qzJiRXXs0o_9EKCktwQz8RYBowop4U : INumberHandler
	{
		public float MinValue => float.MinValue;

		public float MaxValue => float.MaxValue;

		public bool TryParse(string input, out object value)
		{
			decimal result2;
			bool result = decimal.TryParse(input, out result2);
			value = result2;
			return result;
		}

		public bool ValuesAreEqual(object value1, object value2)
		{
			return (decimal)value1 == (decimal)value2;
		}

		public object ConvertFromFloat(float value)
		{
			return (decimal)value;
		}

		public float ConvertToFloat(object value)
		{
			return (float)(decimal)value;
		}
	}

	private static readonly Dictionary<Type, INumberHandler> eR9lQRvwiFfWSEw8mY9msEY = new Dictionary<Type, INumberHandler>(16);

	public static INumberHandler Get(Type type)
	{
		if (!eR9lQRvwiFfWSEw8mY9msEY.TryGetValue(type, out var value))
		{
			value = ((type == smethod_0(typeof(int).TypeHandle)) ? new attf7_jUXZSPLLBMhFItiU8sMgh5HcwGJlpu3SKSgmCBmzf1T6uN41k_0024GQFXTF5dI0FTAmv_0024KcCmeycxiMt19nE() : ((type == smethod_0(typeof(float).TypeHandle)) ? new Class8() : ((type == smethod_0(typeof(long).TypeHandle)) ? new fzBT4wuFE1CR5_0024jtnIdDB6_yJQRkPSBM7mH50Ltsu_g4WzCiATSI5qPRArLZCX_wncwWK6ad3YM_0024BDu3STqLmpU() : ((type == smethod_0(typeof(double).TypeHandle)) ? new Class9() : ((type == smethod_0(typeof(byte).TypeHandle)) ? new cxSmYUy1ZzlJeFRSGTNxZT9oTv1plv3H_pFSWlf2VcGMrDktbHHC0SOY1nbOXYTO_0024VsS1q2rAAYbgWTQdf0PHSs() : ((type == smethod_0(typeof(char).TypeHandle)) ? new JZ9DOH9EFBOjUclNUroR_DFV8OTVNX8kBaA_cNatw42zjigklfKgpXL5m7WMqRDcaNoYpciC0dSWgmu6rTsvcek() : ((type == smethod_0(typeof(short).TypeHandle)) ? new wcccneFUmmG5JvhCd3z9UmSeeGGZmvCL1B5CE4GogDY4jWKNRHaYlAjeyu05fdFEIM_rHbsNwA_Fmfn6FeB79d4() : ((type == smethod_0(typeof(uint).TypeHandle)) ? new zc4t_0024WgroDEU7u2pH_0c8NVIGvzu_0024u8V0FVjNHlbDK1nfeDVh2HQoS1v9pQtvn79uP0_Q6FnXFOhX6Un9frXGmM() : ((type == smethod_0(typeof(ulong).TypeHandle)) ? new qNUeqU_xJjfZ3UUor9k4IOdT_QSghpLoLPYO_0024Ak8JWYUemeTVwnYh4gaH46SyAIl5bgrHi7p_0024cKqWQnmRS9PXo0() : ((type == smethod_0(typeof(sbyte).TypeHandle)) ? new pmq8n5AEpbeFNtPxbPbJzsrJ5tZJavUR4SWvHONyQBKkDiTs7IRCaZZ3aPiIlYw_0024sGPeA3LbtyPwuHx2aBB9IIU() : ((type == smethod_0(typeof(ushort).TypeHandle)) ? ((INumberHandler)new PKtvS66D3uJ6qTFuc18XmtHPYEdyV3r6WJ04peHYDm4rDum4fMx9TDMOUciHLf6Fe117Y3Ej1lxAN0BPWs9ngaM()) : ((INumberHandler)((type == smethod_0(typeof(decimal).TypeHandle)) ? new RNU33NuIOZztDIrXl8hRKDp2y9Q31j9RX_ZTVKl04z_v5WLc28mUtRx39qzJiRXXs0o_9EKCktwQz8RYBowop4U() : null)))))))))))));
			eR9lQRvwiFfWSEw8mY9msEY[type] = value;
		}
		return value;
	}

	internal static Type smethod_0(RuntimeTypeHandle runtimeTypeHandle_0)
	{
		return Type.GetTypeFromHandle(runtimeTypeHandle_0);
	}
}
