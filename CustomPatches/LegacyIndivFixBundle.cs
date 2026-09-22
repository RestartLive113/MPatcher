using MPatchrMain;

namespace MPatcherFork.CustomPatches
{
	internal static class LegacyIndivFixBundle
	{
		private static bool announced;
		private static bool activated;

		internal static bool Enabled
		{
			get
			{
				settingsIngame settings = MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68;
				return settings != null && settings.indivFix;
			}
		}

		internal static void TryRegister()
		{
			if (!announced)
			{
				announced = true;
				Log("REGISTERED serverBundle=connection-retry+mtu+facilitator+master-lifecycle"
					+ "+master-query+socket-self-test+catalogue-recovery+host-stability"
					+ "+auto-port+host-migration enabled=" + Enabled);
			}
			if (Enabled) Activate();
		}

		internal static void SettingChanged(bool enabled)
		{
			Log("SETTING enabled=" + enabled + " savedBy=native-IndivFix");
			if (enabled) Activate();
			else if (activated) Log("DEACTIVATE_DEFERRED reason=native-patches-live-until-restart");
		}

		private static void Activate()
		{
			if (activated) return;
			LegacyConnectionDiagnostics.TryRegister();
			LegacyMtuProbeFix.TryRegister();
			LegacyFacilitatorDisconnectFix.TryRegister();
			LegacyHostFacilitatorReconnectFix.TryRegister();
			LegacyMasterLifecycleFix.TryRegister();
			LegacyMasterQueryFix.TryRegister();
			LegacySocketSelfTestFix.TryRegister();
			LegacyCatalogueRecovery.TryRegister();
			LegacyHostStability.TryRegister();
			LegacyHostAutoPort.TryRegister();
			LegacyHostMigration.TryRegister();
			activated = true;
			Log("ACTIVATED enabled=" + Enabled);
		}

		private static void Log(string message)
		{
			mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(
				"[MPatcher.IndivFix] " + message);
		}
	}
}
