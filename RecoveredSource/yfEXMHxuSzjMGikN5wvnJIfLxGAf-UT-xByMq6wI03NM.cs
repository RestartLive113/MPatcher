using ExitGames.Client.Photon;
using UnityEngine;

internal class yfEXMHxuSzjMGikN5wvnJIfLxGAf_0024UT_0024xByMq6wI03NM
{
	internal OPLNFKECCLE ldHV3fEvHAF9jKoJSUJRyNA;

	internal NetworkPlayer HoqNE1pQwncjTwCeQcsOJUY;

	private bool bool_0;

	internal yfEXMHxuSzjMGikN5wvnJIfLxGAf_0024UT_0024xByMq6wI03NM(OPLNFKECCLE oplnfkeccle_0)
	{
		ldHV3fEvHAF9jKoJSUJRyNA = oplnfkeccle_0;
		bool_0 = false;
	}

	internal yfEXMHxuSzjMGikN5wvnJIfLxGAf_0024UT_0024xByMq6wI03NM(NetworkPlayer networkPlayer_0)
	{
		HoqNE1pQwncjTwCeQcsOJUY = networkPlayer_0;
		bool_0 = true;
	}

	internal bool c0vf5HapJrfk8M3KJ4YOXE8()
	{
		if (bool_0)
		{
			return true;
		}
		if (smethod_0(ldHV3fEvHAF9jKoJSUJRyNA).ContainsKey(global::_003CModule_003E.smethod_27<string>(3975634410u)))
		{
			object value = null;
			smethod_0(ldHV3fEvHAF9jKoJSUJRyNA).TryGetValue(global::_003CModule_003E.smethod_25<string>(1910750624u), out value);
			if (smethod_2(smethod_1(value), global::_003CModule_003E.smethod_25<string>(3707867685u)))
			{
				return true;
			}
		}
		return false;
	}

	internal static Hashtable smethod_0(OPLNFKECCLE oplnfkeccle_0)
	{
		return oplnfkeccle_0.LNEBIMBJJGA;
	}

	internal static string smethod_1(object object_0)
	{
		return object_0.ToString();
	}

	internal static bool smethod_2(string string_0, string string_1)
	{
		return string_0.Contains(string_1);
	}
}
