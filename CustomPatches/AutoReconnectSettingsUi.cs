using MPatchrMain;
using UnityEngine;
using UnityEngine.UI;

namespace MPatcherFork.CustomPatches
{
	internal static class AutoReconnectSettingsUi
	{
		private const float ToggleWidth = 198f;
		private const float GearWidth = 38f;
		private const float Gap = 4f;

		private static Control0 automatic;
		private static Control0 manual;
		private static GameObject page;
		private static ToggleGroup modeGroup;
		private static bool syncing;

		internal static void CreateRow(Transform parent, Transform root, Vector3 position)
		{
			float toggleX = position.x - (GearWidth + Gap) * 0.5f;
			float gearX = position.x + (ToggleWidth + Gap) * 0.5f;
			Class35.smethod_40("Toggle_AutoReconect", new Vector3(toggleX, position.y),
				"AutoReconect", parent, AutoReconnect.SetEnabled, new Vector2(ToggleWidth, 30f),
				AutoReconnect.Enabled, reInit: false);

			lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw gear =
				Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.P3tLroX6fcPfQha_0024JdbpYXg(
					"Button_AutoReconectSettings", new Vector3(gearX, position.y), "",
					delegate { OpenPage(); }, parent, 18);
			gear.UzVS61irgJn5Pnqwx0lThng(new Vector2(GearWidth, 30f));
			PatchSettingsPage.AddGearIcon(gear.transform);

			page = PatchSettingsPage.Create("AutoReconect", "AutoReconect", root);
			modeGroup = page.AddComponent<ToggleGroup>();
			modeGroup.allowSwitchOff = false;
			automatic = CreateModeToggle("Toggle_AutoReconnectAutomatic",
				"Auto restore after crash", new Vector3(0f, 160f), true);
			manual = CreateModeToggle("Toggle_AutoReconnectManual",
				"Restore manually", new Vector3(0f, 120f), false);
			Sync();
			AutoReconnect.Log("SETTINGS_UI page=AutoReconect modes=automatic|manual default=manual");
		}

		private static Control0 CreateModeToggle(string name, string text, Vector3 position,
			bool automaticMode)
		{
			Control0 control = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.uEsWMK_pFkCY_0024M5zt8zLsQk(
				name, position, text, page.transform, resetGroup: true, onClick: delegate(bool toggled)
				{
					if (syncing || !toggled) return;
					AutoReconnect.SetAutomaticCrashRestore(automaticMode);
				}, onUnToggle: null, group: modeGroup);
			control.UzVS61irgJn5Pnqwx0lThng(new Vector2(300f, 30f));
			return control;
		}

		private static void OpenPage()
		{
			if (page == null) return;
			Sync();
			PatchSettingsPage.Open(page, "AutoReconect");
			AutoReconnect.Log("SETTINGS_PAGE opened crashRestore="
				+ (AutoReconnect.AutomaticCrashRestore ? "automatic" : "manual"));
		}

		internal static void Sync()
		{
			if (automatic == null || manual == null || modeGroup == null) return;
			syncing = true;
			try
			{
				modeGroup.allowSwitchOff = true;
				automatic.hLxnG9Hq33zU_YUsu_00240_zak = AutoReconnect.AutomaticCrashRestore;
				manual.hLxnG9Hq33zU_YUsu_00240_zak = !AutoReconnect.AutomaticCrashRestore;
			}
			finally
			{
				modeGroup.allowSwitchOff = false;
				syncing = false;
			}
		}
	}
}
