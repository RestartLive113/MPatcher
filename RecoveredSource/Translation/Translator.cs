using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using LitJson;
using MPatchrMain;
using UnityEngine;

namespace Translation;

public class Translator
{
	[CompilerGenerated]
	private sealed class Class10 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int SjlBM8inVA_YE4YVlr_0024gluY;

		private object yT7HpVIzmqW54W307WgJtr4;

		public string FbnO8HG7JJb6_7cX8QibFaI;

		public string pZEKY5TzLd4S3z2lXESoRnw;

		public Action<string, string> IR1BlfUg0M3aZH8RXPUkABs;

		public Action HRt_0024QR6MjTV4IGCKnAKi20Y;

		private WWW XYaiZHCx0el0CMEErmO9HBw;

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
		public Class10(int int_0)
		{
			SjlBM8inVA_YE4YVlr_0024gluY = int_0;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			XYaiZHCx0el0CMEErmO9HBw = null;
			SjlBM8inVA_YE4YVlr_0024gluY = -2;
		}

		private bool MoveNext()
		{
			switch (SjlBM8inVA_YE4YVlr_0024gluY)
			{
			case 0:
			{
				SjlBM8inVA_YE4YVlr_0024gluY = -1;
				WWWForm wwwform_ = smethod_0();
				smethod_2(wwwform_, global::_003CModule_003E.smethod_27<string>(4166190367u), smethod_1(FbnO8HG7JJb6_7cX8QibFaI));
				smethod_2(wwwform_, global::_003CModule_003E.smethod_25<string>(2815004526u), pZEKY5TzLd4S3z2lXESoRnw);
				XYaiZHCx0el0CMEErmO9HBw = smethod_3(global::_003CModule_003E.smethod_29<string>(2833514279u), wwwform_);
				yT7HpVIzmqW54W307WgJtr4 = XYaiZHCx0el0CMEErmO9HBw;
				SjlBM8inVA_YE4YVlr_0024gluY = 1;
				return true;
			}
			case 1:
				SjlBM8inVA_YE4YVlr_0024gluY = -1;
				if (!smethod_5(smethod_4(XYaiZHCx0el0CMEErmO9HBw)))
				{
					HRt_0024QR6MjTV4IGCKnAKi20Y();
				}
				else
				{
					JsonData jsonData_ = smethod_7(smethod_6(XYaiZHCx0el0CMEErmO9HBw));
					string text = smethod_9((object)smethod_8(jsonData_, global::_003CModule_003E.smethod_27<string>(1755799951u)));
					string arg = smethod_9((object)smethod_8(jsonData_, global::_003CModule_003E.smethod_28<string>(878267073u)));
					if (smethod_10(smethod_1(FbnO8HG7JJb6_7cX8QibFaI), smethod_1(text)))
					{
						return false;
					}
					IR1BlfUg0M3aZH8RXPUkABs(arg, text);
				}
				return false;
			default:
				return false;
			}
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw smethod_11();
		}

		internal static WWWForm smethod_0()
		{
			return new WWWForm();
		}

		internal static string smethod_1(string string_0)
		{
			return string_0.ToUpper();
		}

		internal static void smethod_2(WWWForm wwwform_0, string string_0, string string_1)
		{
			wwwform_0.AddField(string_0, string_1);
		}

		internal static WWW smethod_3(string string_0, WWWForm wwwform_0)
		{
			return new WWW(string_0, wwwform_0);
		}

		internal static string smethod_4(WWW www_0)
		{
			return www_0.error;
		}

		internal static bool smethod_5(string string_0)
		{
			return string.IsNullOrEmpty(string_0);
		}

		internal static string smethod_6(WWW www_0)
		{
			return www_0.text;
		}

		internal static JsonData smethod_7(string string_0)
		{
			return JsonMapper.ToObject(string_0);
		}

		internal static JsonData smethod_8(JsonData jsonData_0, string string_0)
		{
			return jsonData_0[string_0];
		}

		internal static string smethod_9(object object_0)
		{
			return object_0.ToString();
		}

		internal static bool smethod_10(string string_0, string string_1)
		{
			return string_0 == string_1;
		}

		internal static NotSupportedException smethod_11()
		{
			return new NotSupportedException();
		}
	}

	private static readonly string O9_0024MfnSrjLJ5tGnucgcJGZk = global::_003CModule_003E.smethod_25<string>(1486603953u);

	private const string kmH7eTP2k6ggS4Por37mmzs = "https://www.bing.com/ttranslatev3";

	private static string ifKEQoNrT7h3PrxiwOeQ3VQ = "";

	private static string string_0 = "";

	private static string WsHH6i_0024tA1mJxFJEeWc4iT0 = "";

	private static int int_0;

	private static int int_1 = 0;

	public static void Run(string text, string target, settingsIngame.translationEngines engine, Action<string, string> result)
	{
		if (engine != settingsIngame.translationEngines.microsoft && !smethod_2(smethod_1(target), global::_003CModule_003E.smethod_27<string>(3030706629u)))
		{
			smethod_3((MonoBehaviour)MPatchr.xcBvxcM_0024ckBeZyvdSoAkJoM, smethod_0(text, target, result, delegate
			{
				Run(text, target, settingsIngame.translationEngines.microsoft, result);
			}));
		}
		else
		{
			smethod_3((MonoBehaviour)MPatchr.xcBvxcM_0024ckBeZyvdSoAkJoM, aXEdzwz5nkjN_kx4HJ7xUBTkN0QGb42KV4K2ZV9W0GlR(text, target, result));
		}
	}

	private static int PTK0v92XBVWraV_BbA4H7zI()
	{
		DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		return (int)(DateTime.UtcNow - dateTime).TotalSeconds;
	}

	private static IEnumerator aXEdzwz5nkjN_kx4HJ7xUBTkN0QGb42KV4K2ZV9W0GlR(string string_1, string string_2, Action<string, string> action_0)
	{
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_27<string>(3733438181u) + PTK0v92XBVWraV_BbA4H7zI() + global::_003CModule_003E.smethod_29<string>(2270887523u) + int_1 + global::_003CModule_003E.smethod_25<string>(3037456349u) + int_0 / 1000);
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_25<string>(4001956665u) + (PTK0v92XBVWraV_BbA4H7zI() - int_1) + global::_003CModule_003E.smethod_29<string>(355981240u) + int_0 / 1000);
		if (PTK0v92XBVWraV_BbA4H7zI() - int_1 >= int_0 / 1000)
		{
			UnityEngine.Debug.Log(global::_003CModule_003E.smethod_27<string>(3147117859u));
			WWW wWW = new WWW(global::_003CModule_003E.smethod_28<string>(316755718u));
			yield return wWW;
			UnityEngine.Debug.Log(global::_003CModule_003E.smethod_27<string>(350505932u));
			if (wWW.responseHeaders.ContainsKey(global::_003CModule_003E.smethod_27<string>(3240179209u)))
			{
				WsHH6i_0024tA1mJxFJEeWc4iT0 = wWW.responseHeaders[global::_003CModule_003E.smethod_25<string>(3029518735u)];
			}
			MatchCollection matchCollection = Regex.Matches(wWW.text, global::_003CModule_003E.smethod_28<string>(2638709090u));
			if (matchCollection.Count <= 0)
			{
				UnityEngine.Debug.Log(global::_003CModule_003E.smethod_25<string>(2287470242u));
				yield break;
			}
			UnityEngine.Debug.Log(global::_003CModule_003E.smethod_25<string>(2724436274u));
			GroupCollection groups = matchCollection[0].Groups;
			ifKEQoNrT7h3PrxiwOeQ3VQ = groups[1].Value;
			string_0 = groups[2].Value;
			int_0 = int.Parse(groups[3].Value);
			mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_28<string>(3549308100u) + ifKEQoNrT7h3PrxiwOeQ3VQ + global::_003CModule_003E.smethod_27<string>(3626400720u) + string_0 + global::_003CModule_003E.smethod_26<string>(1562498263u) + int_0);
			int_1 = PTK0v92XBVWraV_BbA4H7zI();
		}
		WWWForm wWWForm = new WWWForm();
		wWWForm.AddField(global::_003CModule_003E.smethod_27<string>(3426301909u), global::_003CModule_003E.smethod_26<string>(600195949u));
		wWWForm.AddField(global::_003CModule_003E.smethod_28<string>(2805676943u), string_2);
		wWWForm.AddField(global::_003CModule_003E.smethod_25<string>(2815004526u), string_1);
		wWWForm.AddField(global::_003CModule_003E.smethod_26<string>(920963387u), string_0);
		wWWForm.AddField(global::_003CModule_003E.smethod_26<string>(2406080392u), ifKEQoNrT7h3PrxiwOeQ3VQ);
		WWW wWW2 = new WWW(global::_003CModule_003E.smethod_25<string>(2419353813u), wWWForm.data, new Dictionary<string, string>
		{
			{
				global::_003CModule_003E.smethod_25<string>(3045393963u),
				global::_003CModule_003E.smethod_26<string>(3855804280u)
			},
			{
				global::_003CModule_003E.smethod_25<string>(2740311502u),
				WsHH6i_0024tA1mJxFJEeWc4iT0
			}
		});
		yield return wWW2;
		if (!string.IsNullOrEmpty(wWW2.error))
		{
			action_0(null, null);
			yield break;
		}
		JsonData jsonData = JsonMapper.ToObject(wWW2.text);
		if (!jsonData.IsArray || jsonData.Count == 0)
		{
			action_0(null, null);
			yield break;
		}
		JsonData jsonData2 = jsonData[0][global::_003CModule_003E.smethod_29<string>(2985273668u)];
		if (jsonData2.Count != 0)
		{
			action_0((string)jsonData2[0][global::_003CModule_003E.smethod_27<string>(2235082812u)], jsonData[0][global::_003CModule_003E.smethod_27<string>(2407229401u)][global::_003CModule_003E.smethod_26<string>(362755579u)].ToString());
		}
		else
		{
			action_0(null, null);
		}
	}

	internal static IEnumerator smethod_0(string string_1, string string_2, Action<string, string> action_0, Action action_1 = null)
	{
		WWWForm wwwform_ = Class10.smethod_0();
		Class10.smethod_2(wwwform_, global::_003CModule_003E.smethod_27<string>(4166190367u), Class10.smethod_1(string_2));
		Class10.smethod_2(wwwform_, global::_003CModule_003E.smethod_25<string>(2815004526u), string_1);
		WWW wWW = Class10.smethod_3(global::_003CModule_003E.smethod_29<string>(2833514279u), wwwform_);
		yield return wWW;
		if (!Class10.smethod_5(Class10.smethod_4(wWW)))
		{
			action_1();
			yield break;
		}
		JsonData jsonData_ = Class10.smethod_7(Class10.smethod_6(wWW));
		string arg = Class10.smethod_9((object)Class10.smethod_8(jsonData_, global::_003CModule_003E.smethod_27<string>(1755799951u)));
		string arg2 = Class10.smethod_9((object)Class10.smethod_8(jsonData_, global::_003CModule_003E.smethod_28<string>(878267073u)));
		if (!Class10.smethod_10(Class10.smethod_1(string_2), Class10.smethod_1(arg)))
		{
			action_0(arg2, arg);
		}
	}

	internal static string smethod_1(string string_1)
	{
		return string_1.ToUpper();
	}

	internal static bool smethod_2(string string_1, string string_2)
	{
		return string_1 == string_2;
	}

	internal static Coroutine smethod_3(MonoBehaviour monoBehaviour_0, IEnumerator ienumerator_0)
	{
		return monoBehaviour_0.StartCoroutine(ienumerator_0);
	}
}
