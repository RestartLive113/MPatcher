using System;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Converter.MeshFormat.Reader;

public class ObjFormatReader : IMeshFormatReader
{
	internal static readonly Regex WW1JCkHL6BcWx1_RTmmeDZU = smethod_17(global::_003CModule_003E.smethod_27<string>(3094463077u));

	internal static readonly Regex mvcIzqVeHh9mrEgS6JMk8EFSBp26OQ0fHI1_0024BYk0sAz1 = smethod_17(global::_003CModule_003E.smethod_25<string>(1319119535u));

	internal static readonly Regex jRwwfuoC6K4xit6_0024E8r5QYTDtQHvT8yPW7PVkA5BfjmB = smethod_17(global::_003CModule_003E.smethod_28<string>(813213067u));

	internal static readonly Regex Ed6GnAMg6sWJ1jLcfxP_dXo = smethod_17(global::_003CModule_003E.smethod_27<string>(577035200u));

	public string Tag => global::_003CModule_003E.smethod_26<string>(3917532279u);

	public Mesh ReadFromStream(Stream inputStream)
	{
		ObjFormat objFormat = new ObjFormat();
		StreamReader streamReader = smethod_0(inputStream);
		try
		{
			while (smethod_10((TextReader)streamReader) > -1)
			{
				string text = smethod_1((TextReader)streamReader);
				string string_ = ((text != null) ? smethod_2(text) : null);
				if (smethod_3(string_) || smethod_4(string_, 0) == '#')
				{
					continue;
				}
				int num = smethod_5(string_, ' ');
				if (num > -1)
				{
					string string_2 = smethod_6(string_, 0, num);
					int int_ = smethod_7(string_) - smethod_7(string_2);
					string string_3 = smethod_8(smethod_6(string_, num, int_));
					if (smethod_9(string_2, global::_003CModule_003E.smethod_29<string>(181948984u)))
					{
						objFormat.GeometricVertices.Add(Bo2CKvoibYIPwN1H9J2TufSEeHRDgdLFHW0lnXaQfwoj(string_3));
					}
					else if (smethod_9(string_2, global::_003CModule_003E.smethod_29<string>(4264603909u)))
					{
						objFormat.Faces.Add(FCGbmd82oFV7i05TdTA8ghw(string_3, objFormat));
					}
					else if (smethod_9(string_2, global::_003CModule_003E.smethod_26<string>(3113802037u)))
					{
						objFormat.TextureVertices.Add(cLh3hlw_1iJWRwXaq_0024yrXFVJv7OOQ0DHY4I4tM7EpW4u(string_3));
					}
					else if (smethod_9(string_2, global::_003CModule_003E.smethod_25<string>(4238060574u)))
					{
						objFormat.VertexNormals.Add(xvsKRm9zHlzvftX_lyfGIeXlkI0R6X0D0no7MxJpxH9q(string_3));
					}
				}
			}
		}
		finally
		{
			if (streamReader != null)
			{
				smethod_11((IDisposable)streamReader);
			}
		}
		return ObjFormat.ToMesh(objFormat);
	}

	internal Vector3 xvsKRm9zHlzvftX_lyfGIeXlkI0R6X0D0no7MxJpxH9q(string string_0)
	{
		string[] array = smethod_12(string_0, new char[1] { ' ' });
		if (array.Length == 3)
		{
			if (!float.TryParse(array[0], out var result) || !float.TryParse(array[1], out var result2) || !float.TryParse(array[2], out var result3))
			{
				throw new FormatException(global::_003CModule_003E.smethod_26<string>(2709707531u));
			}
			return new Vector3(result, result2, result3);
		}
		throw smethod_14(smethod_13(global::_003CModule_003E.smethod_26<string>(464335465u), (object)array.Length));
	}

	internal Vector3 cLh3hlw_1iJWRwXaq_0024yrXFVJv7OOQ0DHY4I4tM7EpW4u(string string_0)
	{
		string[] array = smethod_12(string_0, new char[1] { ' ' });
		if (array.Length >= 2)
		{
			if (float.TryParse(array[0], out var result) && float.TryParse(array[1], out var result2))
			{
				Vector3 result3 = new Vector3(result, result2, 1f);
				if (array.Length == 3 && float.TryParse(array[2], out var result4))
				{
					result3.z = result4;
				}
				return result3;
			}
			throw smethod_14(global::_003CModule_003E.smethod_27<string>(4113535585u));
		}
		throw smethod_14(smethod_13(global::_003CModule_003E.smethod_25<string>(3932978113u), (object)array.Length));
	}

	internal Vector4 Bo2CKvoibYIPwN1H9J2TufSEeHRDgdLFHW0lnXaQfwoj(string string_0)
	{
		string[] array = smethod_12(string_0, new char[1] { ' ' });
		if (array.Length != 3)
		{
			throw smethod_14(smethod_13(global::_003CModule_003E.smethod_25<string>(1360434854u), (object)array.Length));
		}
		if (float.TryParse(array[0], out var result) && float.TryParse(array[1], out var result2) && float.TryParse(array[2], out var result3))
		{
			Vector4 result4 = new Vector4(result, result2, result3, 1f);
			if (array.Length == 4 && float.TryParse(array[3], out var result5))
			{
				result4.w = result5;
			}
			return result4;
		}
		throw smethod_14(global::_003CModule_003E.smethod_27<string>(1596107708u));
	}

	private int nNouv9ADJI03bksfmsfTqIzfzjXpSd1SrRggbaRwtuJx(string string_0, int int_0)
	{
		int num = int.Parse(string_0);
		if (num < 0)
		{
			num += int_0 + 1;
		}
		return num;
	}

	internal ObjFormat.Face FCGbmd82oFV7i05TdTA8ghw(string string_0, ObjFormat objFormat_0)
	{
		Regex regex = method_0(string_0);
		string[] array = smethod_12(string_0, new char[1] { ' ' });
		ObjFormat.Face face = new ObjFormat.Face();
		string[] array2 = array;
		foreach (string text in array2)
		{
			if (regex != null && smethod_15(regex, text))
			{
				if (regex != WW1JCkHL6BcWx1_RTmmeDZU)
				{
					if (regex != Ed6GnAMg6sWJ1jLcfxP_dXo)
					{
						if (regex == mvcIzqVeHh9mrEgS6JMk8EFSBp26OQ0fHI1_0024BYk0sAz1)
						{
							string[] array3 = smethod_12(text, new char[1] { '/' });
							face.GeometricVertexReferences.Add(nNouv9ADJI03bksfmsfTqIzfzjXpSd1SrRggbaRwtuJx(array3[0], objFormat_0.GeometricVertices.Count));
							face.NormalVertexReferences.Add(nNouv9ADJI03bksfmsfTqIzfzjXpSd1SrRggbaRwtuJx(array3[2], objFormat_0.VertexNormals.Count));
						}
						else if (regex == jRwwfuoC6K4xit6_0024E8r5QYTDtQHvT8yPW7PVkA5BfjmB)
						{
							string[] array4 = smethod_12(text, new char[1] { '/' });
							face.GeometricVertexReferences.Add(nNouv9ADJI03bksfmsfTqIzfzjXpSd1SrRggbaRwtuJx(array4[0], objFormat_0.GeometricVertices.Count));
							face.TextureVertexReferences.Add(nNouv9ADJI03bksfmsfTqIzfzjXpSd1SrRggbaRwtuJx(array4[1], objFormat_0.TextureVertices.Count));
						}
					}
					else
					{
						string[] array5 = smethod_12(text, new char[1] { '/' });
						face.GeometricVertexReferences.Add(nNouv9ADJI03bksfmsfTqIzfzjXpSd1SrRggbaRwtuJx(array5[0], objFormat_0.GeometricVertices.Count));
						face.TextureVertexReferences.Add(nNouv9ADJI03bksfmsfTqIzfzjXpSd1SrRggbaRwtuJx(array5[1], objFormat_0.TextureVertices.Count));
						face.NormalVertexReferences.Add(nNouv9ADJI03bksfmsfTqIzfzjXpSd1SrRggbaRwtuJx(array5[2], objFormat_0.VertexNormals.Count));
					}
				}
				else
				{
					int item = nNouv9ADJI03bksfmsfTqIzfzjXpSd1SrRggbaRwtuJx(text, objFormat_0.GeometricVertices.Count);
					face.GeometricVertexReferences.Add(item);
				}
				continue;
			}
			throw smethod_14(smethod_16(global::_003CModule_003E.smethod_26<string>(2074443072u), text));
		}
		return face;
	}

	internal Regex method_0(string string_0)
	{
		int num = smethod_5(string_0, ' ');
		Regex result = null;
		if (num <= 0)
		{
			throw smethod_14(smethod_16(global::_003CModule_003E.smethod_29<string>(2825955056u), string_0));
		}
		string string_1 = smethod_6(string_0, 0, num);
		if (smethod_15(WW1JCkHL6BcWx1_RTmmeDZU, string_1))
		{
			result = WW1JCkHL6BcWx1_RTmmeDZU;
		}
		else if (smethod_15(Ed6GnAMg6sWJ1jLcfxP_dXo, string_1))
		{
			result = Ed6GnAMg6sWJ1jLcfxP_dXo;
		}
		else if (smethod_15(mvcIzqVeHh9mrEgS6JMk8EFSBp26OQ0fHI1_0024BYk0sAz1, string_1))
		{
			result = mvcIzqVeHh9mrEgS6JMk8EFSBp26OQ0fHI1_0024BYk0sAz1;
		}
		else if (smethod_15(jRwwfuoC6K4xit6_0024E8r5QYTDtQHvT8yPW7PVkA5BfjmB, string_1))
		{
			result = jRwwfuoC6K4xit6_0024E8r5QYTDtQHvT8yPW7PVkA5BfjmB;
		}
		return result;
	}

	internal static StreamReader smethod_0(Stream stream_0)
	{
		return new StreamReader(stream_0);
	}

	internal static string smethod_1(TextReader textReader_0)
	{
		return textReader_0.ReadLine();
	}

	internal static string smethod_2(string string_0)
	{
		return string_0.Trim();
	}

	internal static bool smethod_3(string string_0)
	{
		return string.IsNullOrEmpty(string_0);
	}

	internal static char smethod_4(string string_0, int int_0)
	{
		return string_0[int_0];
	}

	internal static int smethod_5(string string_0, char char_0)
	{
		return string_0.IndexOf(char_0);
	}

	internal static string smethod_6(string string_0, int int_0, int int_1)
	{
		return string_0.Substring(int_0, int_1);
	}

	internal static int smethod_7(string string_0)
	{
		return string_0.Length;
	}

	internal static string smethod_8(string string_0)
	{
		return string_0.Trim();
	}

	internal static bool smethod_9(string string_0, string string_1)
	{
		return string_0 == string_1;
	}

	internal static int smethod_10(TextReader textReader_0)
	{
		return textReader_0.Peek();
	}

	internal static void smethod_11(IDisposable idisposable_0)
	{
		idisposable_0.Dispose();
	}

	internal static string[] smethod_12(string string_0, char[] char_0)
	{
		return string_0.Split(char_0);
	}

	internal static string smethod_13(string string_0, object object_0)
	{
		return string.Format(string_0, object_0);
	}

	internal static FormatException smethod_14(string string_0)
	{
		return new FormatException(string_0);
	}

	internal static bool smethod_15(Regex regex_0, string string_0)
	{
		return regex_0.IsMatch(string_0);
	}

	internal static string smethod_16(string string_0, string string_1)
	{
		return string_0 + string_1;
	}

	internal static Regex smethod_17(string string_0)
	{
		return new Regex(string_0);
	}
}
