using System;
using UnityEngine;

[Serializable]
public struct HSBColor
{
	public float h;

	public float s;

	public float b;

	public float a;

	public HSBColor(float float_0, float float_1, float float_2, float float_3)
	{
		h = float_0;
		s = float_1;
		b = float_2;
		a = float_3;
	}

	public HSBColor(float float_0, float float_1, float float_2)
	{
		h = float_0;
		s = float_1;
		b = float_2;
		a = 1f;
	}

	public HSBColor(Color color_0)
	{
		HSBColor hSBColor = FromColor(color_0);
		h = hSBColor.h;
		s = hSBColor.s;
		b = hSBColor.b;
		a = hSBColor.a;
	}

	public static HSBColor FromColor(Color color)
	{
		HSBColor result = new HSBColor(0f, 0f, 0f, color.a);
		float r = color.r;
		float g = color.g;
		float num = color.b;
		float num2 = Mathf.Max(r, Mathf.Max(g, num));
		if (num2 > 0f)
		{
			float num3 = Mathf.Min(r, Mathf.Min(g, num));
			float num4 = num2 - num3;
			if (num2 <= num3)
			{
				result.h = 0f;
			}
			else
			{
				if (g != num2)
				{
					if (num != num2)
					{
						if (num <= g)
						{
							result.h = (g - num) / num4 * 60f;
						}
						else
						{
							result.h = (g - num) / num4 * 60f + 360f;
						}
					}
					else
					{
						result.h = (r - g) / num4 * 60f + 240f;
					}
				}
				else
				{
					result.h = (num - r) / num4 * 60f + 120f;
				}
				if (result.h < 0f)
				{
					result.h += 360f;
				}
			}
			result.h *= 0.0027777778f;
			result.s = num4 / num2 * 1f;
			result.b = num2;
			return result;
		}
		return result;
	}

	public static Color ToColor(HSBColor hsbColor)
	{
		float value = hsbColor.b;
		float value2 = hsbColor.b;
		float value3 = hsbColor.b;
		if (hsbColor.s != 0f)
		{
			float num = hsbColor.b;
			float num2 = hsbColor.b * hsbColor.s;
			float num3 = hsbColor.b - num2;
			float num4 = hsbColor.h * 360f;
			if (num4 >= 60f)
			{
				if (num4 < 120f)
				{
					value = (0f - (num4 - 120f)) * num2 / 60f + num3;
					value2 = num;
					value3 = num3;
				}
				else if (num4 < 180f)
				{
					value = num3;
					value2 = num;
					value3 = (num4 - 120f) * num2 / 60f + num3;
				}
				else if (num4 < 240f)
				{
					value = num3;
					value2 = (0f - (num4 - 240f)) * num2 / 60f + num3;
					value3 = num;
				}
				else if (num4 >= 300f)
				{
					if (num4 <= 360f)
					{
						value = num;
						value2 = num3;
						value3 = (0f - (num4 - 360f)) * num2 / 60f + num3;
					}
					else
					{
						value = 0f;
						value2 = 0f;
						value3 = 0f;
					}
				}
				else
				{
					value = (num4 - 240f) * num2 / 60f + num3;
					value2 = num3;
					value3 = num;
				}
			}
			else
			{
				value = num;
				value2 = num4 * num2 / 60f + num3;
				value3 = num3;
			}
		}
		return new Color(Mathf.Clamp01(value), Mathf.Clamp01(value2), Mathf.Clamp01(value3), hsbColor.a);
	}

	public Color ToColor()
	{
		return ToColor(this);
	}

	public override string ToString()
	{
		return global::_003CModule_003E.smethod_25<string>(31201556u) + h + global::_003CModule_003E.smethod_28<string>(1333121957u) + s + global::_003CModule_003E.smethod_27<string>(3616857866u) + b;
	}

	public static HSBColor Lerp(HSBColor a, HSBColor b, float t)
	{
		float float_;
		float float_2;
		if (a.b == 0f)
		{
			float_ = b.h;
			float_2 = b.s;
		}
		else if (b.b == 0f)
		{
			float_ = a.h;
			float_2 = a.s;
		}
		else
		{
			if (a.s == 0f)
			{
				float_ = b.h;
			}
			else if (b.s == 0f)
			{
				float_ = a.h;
			}
			else
			{
				float num;
				for (num = Mathf.LerpAngle(a.h * 360f, b.h * 360f, t); num < 0f; num += 360f)
				{
				}
				while (num > 360f)
				{
					num -= 360f;
				}
				float_ = num / 360f;
			}
			float_2 = Mathf.Lerp(a.s, b.s, t);
		}
		return new HSBColor(float_, float_2, Mathf.Lerp(a.b, b.b, t), Mathf.Lerp(a.a, b.a, t));
	}

	public static void Test()
	{
		HSBColor hSBColor = new HSBColor(Color.red);
		string string_ = global::_003CModule_003E.smethod_26<string>(2682954911u);
		HSBColor hSBColor2 = hSBColor;
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(smethod_0(string_, hSBColor2.ToString()), bool_0: true);
		hSBColor = new HSBColor(Color.green);
		string string_2 = global::_003CModule_003E.smethod_29<string>(2015457956u);
		hSBColor2 = hSBColor;
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(smethod_0(string_2, hSBColor2.ToString()), bool_0: true);
		hSBColor = new HSBColor(Color.blue);
		string string_3 = global::_003CModule_003E.smethod_25<string>(2924702504u);
		hSBColor2 = hSBColor;
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(smethod_0(string_3, hSBColor2.ToString()), bool_0: true);
		hSBColor = new HSBColor(Color.grey);
		string string_4 = global::_003CModule_003E.smethod_28<string>(3063260076u);
		hSBColor2 = hSBColor;
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(smethod_0(string_4, hSBColor2.ToString()), bool_0: true);
		hSBColor = new HSBColor(Color.white);
		string string_5 = global::_003CModule_003E.smethod_25<string>(2223969330u);
		hSBColor2 = hSBColor;
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(smethod_0(string_5, hSBColor2.ToString()), bool_0: true);
		hSBColor = new HSBColor(new Color(0.4f, 1f, 0.84f, 1f));
		string text = global::_003CModule_003E.smethod_27<string>(1592688961u);
		hSBColor2 = hSBColor;
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(text + hSBColor2.ToString(), bool_0: true);
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_26<string>(2041420035u) + ToColor(new HSBColor(new Color(0.643137f, 0.321568f, 0.329411f))).ToString(), bool_0: true);
	}

	internal static string smethod_0(string string_0, string string_1)
	{
		return string_0 + string_1;
	}
}
