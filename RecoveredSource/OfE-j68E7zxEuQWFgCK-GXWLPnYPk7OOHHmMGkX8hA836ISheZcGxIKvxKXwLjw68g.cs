using System;
using LitJson;

internal static class OfE_0024j68E7zxEuQWFgCK_0024GXWLPnYPk7OOHHmMGkX8hA836ISheZcGxIKvxKXwLjw68g
{
	internal static void ozdM7P2Ys1T51t37eU4Zdr8()
	{
		JsonMapper.RegisterImporter(delegate(long val)
		{
			if (val < 0L)
			{
				throw new Exception(global::_003CModule_003E.smethod_27<string>(2747596577u) + val + global::_003CModule_003E.smethod_25<string>(3489067622u));
			}
			return (ulong)val;
		});
	}
}
