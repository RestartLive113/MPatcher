using MPatchrMain.discordRPC;

internal class U_0024Y3HeQRR_0024vHVl515guIm5pFz5zddo_EEiypiKocRd4SQiyP7ZyPcC8WZZRlllFLCg
{
	private static DiscordRpc.EventHandlers eR9lQRvwiFfWSEw8mY9msEY;

	private static DiscordRpc.RichPresence xzfPH4o_SUBckHxUAGUsASc = new DiscordRpc.RichPresence();

	private static bool PafCCA77BFEJYaAdgSJv7Ys = false;

	public static void t6spNnuaouGakCO9rsclpT4()
	{
		DiscordRpc.Discord_ClearPresence();
		DiscordRpc.Discord_Shutdown();
	}

	public static void pJ2WrdDSAqkC2YTkrnQ0W_U(ref DiscordRpc.DiscordUser discordUser_0)
	{
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(smethod_1(global::_003CModule_003E.smethod_26<string>(837218581u), (object)discordUser_0.username, (object)discordUser_0.discriminator, (object)discordUser_0.userId));
		PafCCA77BFEJYaAdgSJv7Ys = true;
	}

	public static void sLBHDVdAhc01lz07nJtynTrc6_0024uiVcifghOx6EPdxGRa(int int_0, string string_0)
	{
		mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(smethod_2(global::_003CModule_003E.smethod_27<string>(2789186740u), (object)int_0, (object)string_0));
		PafCCA77BFEJYaAdgSJv7Ys = false;
	}

	public static void oJN_00244IcEU0waAX7Zww3G6zI()
	{
		eR9lQRvwiFfWSEw8mY9msEY = default(DiscordRpc.EventHandlers);
		eR9lQRvwiFfWSEw8mY9msEY.readyCallback = pJ2WrdDSAqkC2YTkrnQ0W_U;
		eR9lQRvwiFfWSEw8mY9msEY.disconnectedCallback = delegate(int int_0, string string_0)
		{
			mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(smethod_2(global::_003CModule_003E.smethod_27<string>(2789186740u), (object)int_0, (object)string_0));
			PafCCA77BFEJYaAdgSJv7Ys = false;
		};
		DiscordRpc.Discord_Initialize(global::_003CModule_003E.smethod_26<string>(962209183u), ref eR9lQRvwiFfWSEw8mY9msEY, autoRegister: true, null);
	}

	public static void smethod_0()
	{
		DiscordRpc.Discord_RunCallbacks();
	}

	public static void vMMClivVEHZ_0024ZzGxvSE30m8(string string_0, string string_1)
	{
		if (PafCCA77BFEJYaAdgSJv7Ys)
		{
			xzfPH4o_SUBckHxUAGUsASc.largeImageKey = global::_003CModule_003E.smethod_29<string>(286845299u);
			xzfPH4o_SUBckHxUAGUsASc.state = string_1;
			xzfPH4o_SUBckHxUAGUsASc.details = string_0;
			DiscordRpc.UpdatePresence(xzfPH4o_SUBckHxUAGUsASc);
		}
	}

	internal static string smethod_1(string string_0, object object_0, object object_1, object object_2)
	{
		return string.Format(string_0, object_0, object_1, object_2);
	}

	internal static string smethod_2(string string_0, object object_0, object object_1)
	{
		return string.Format(string_0, object_0, object_1);
	}
}
