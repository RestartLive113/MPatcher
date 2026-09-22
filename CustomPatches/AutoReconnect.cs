using MPatchrMain;

namespace MPatcherFork.CustomPatches
{
	internal static class AutoReconnect
	{
		internal static bool Enabled
		{
			get
			{
				settingsIngame settings = MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68;
				return settings != null && settings.autoReconnect;
			}
		}

		internal static bool AutomaticCrashRestore
		{
			get
			{
				settingsIngame settings = MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68;
				return settings != null && settings.autoReconnectCrashRestore;
			}
		}

		internal static void SetEnabled(bool enabled)
		{
			settingsIngame settings = MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68;
			if (settings == null) return;
			settings.autoReconnect = enabled;
			settings.UUiRNMwxRbfk_Fs4cDErRoM();
			Log("SETTING enabled=" + enabled + " saved=true");
		}

		internal static void SetAutomaticCrashRestore(bool automatic)
		{
			settingsIngame settings = MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68;
			if (settings == null) return;
			settings.autoReconnectCrashRestore = automatic;
			settings.UUiRNMwxRbfk_Fs4cDErRoM();
			AutoReconnectSettingsUi.Sync();
			Log("CRASH_RESTORE_MODE value=" + (automatic ? "automatic" : "manual")
				+ " saved=true");
		}

		internal static void Log(string message)
		{
			mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(
				"[MPatcher.AutoReconnect] " + message);
		}
	}
}
