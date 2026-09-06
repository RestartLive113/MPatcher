using UnityEngine;
using UnityEngine.UI;

namespace MPatcherFork.CustomPatches
{
	internal static class MPatcherUpdaterUi
	{
		private static lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw updateButton;
		private static Text updateButtonText;
		private static Button updateUnityButton;
		private static Text launcherText;
		private static string launcherBaseText;
		private static lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw toast;
		private static Text toastText;
		private static string lastToastKey;

		internal static void Create(Transform settingsRoot)
		{
			if (settingsRoot == null)
				return;
			updateButton = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.P3tLroX6fcPfQha_0024JdbpYXg(
				"Button_MPatchrUpdate", new Vector3(0f, -350f), string.Empty,
				delegate { MPatcherUpdater.HandleButton(); }, settingsRoot, 14);
			updateButton.UzVS61irgJn5Pnqwx0lThng(new Vector2(340f, 32f));
			updateButtonText = updateButton.GetComponentInChildren<Text>();
			updateUnityButton = updateButton.GetComponent<Button>();
			if (updateButtonText != null)
			{
				updateButtonText.resizeTextForBestFit = true;
				updateButtonText.resizeTextMinSize = 9;
				updateButtonText.resizeTextMaxSize = 14;
			}
			MPatcherUpdater.Log("SETTINGS_BUTTON_CREATED position=0,-350 size=340x32");
			Refresh();
		}

		internal static void BindLauncher(Control0 settingsLauncher)
		{
			if (settingsLauncher == null)
				return;
			launcherText = settingsLauncher.GetComponentInChildren<Text>();
			launcherBaseText = launcherText == null ? string.Empty : launcherText.text;
			Transform parent = settingsLauncher.transform.parent;
			if (parent != null)
			{
				toast = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.P3tLroX6fcPfQha_0024JdbpYXg(
					"Button_MPatchrUpdateNotice", new Vector3(0f, 300f), string.Empty,
					delegate
					{
						if (toast != null) toast.gameObject.SetActive(false);
					}, parent, 14);
				toast.UzVS61irgJn5Pnqwx0lThng(new Vector2(520f, 36f));
				toastText = toast.GetComponentInChildren<Text>();
				if (toastText != null)
				{
					toastText.resizeTextForBestFit = true;
					toastText.resizeTextMinSize = 9;
					toastText.resizeTextMaxSize = 14;
				}
				toast.gameObject.SetActive(false);
			}
			Refresh();
		}

		internal static void Refresh()
		{
			MPatcherUpdaterState state = MPatcherUpdater.State;
			string buttonText = GetButtonText(state);
			if (updateButtonText != null)
				updateButtonText.text = buttonText;
			if (updateUnityButton != null)
				updateUnityButton.interactable = state != MPatcherUpdaterState.Checking
					&& state != MPatcherUpdaterState.Downloading
					&& state != MPatcherUpdaterState.Launching;

			bool available = state == MPatcherUpdaterState.Available;
			if (launcherText != null)
				launcherText.text = available
					? launcherBaseText + "  [UPDATE]"
					: launcherBaseText;

			if (available)
			{
				string key = "available:" + MPatcherUpdater.AvailableVersion;
				ShowToastOnce(key, MPatcherUpdater.Localize(
					"Доступно обновление MPatcher " + MPatcherUpdater.AvailableVersion + ". Откройте Settings → MPatcher.",
					"MPatcher " + MPatcherUpdater.AvailableVersion + " is available. Open Settings → MPatcher.",
					"MPatcher " + MPatcherUpdater.AvailableVersion + " を利用できます。Settings → MPatcher を開いてください。"));
			}
			else if (!string.IsNullOrEmpty(MPatcherUpdater.LastInstalledVersion))
			{
				string installedVersion = MPatcherUpdater.LastInstalledVersion;
				string key = "updated:" + installedVersion;
				ShowToastOnce(key, MPatcherUpdater.Localize(
					"MPatcher успешно обновлён до " + installedVersion + ".",
					"MPatcher was updated successfully to " + installedVersion + ".",
					"MPatcher を " + installedVersion + " に更新しました。"));
			}
		}

		private static string GetButtonText(MPatcherUpdaterState state)
		{
			switch (state)
			{
				case MPatcherUpdaterState.Checking:
					return MPatcherUpdater.Localize("Проверка обновления...", "Checking for updates...", "更新を確認しています...");
				case MPatcherUpdaterState.UpToDate:
					return MPatcherUpdater.Localize(
						"MPatcher " + MPatcherUpdater.CurrentVersion + " — актуальная версия",
						"MPatcher " + MPatcherUpdater.CurrentVersion + " is up to date",
						"MPatcher " + MPatcherUpdater.CurrentVersion + " は最新です");
				case MPatcherUpdaterState.Available:
					return MPatcherUpdater.Localize(
						"Обновить MPatcher: " + MPatcherUpdater.CurrentVersion + " → " + MPatcherUpdater.AvailableVersion,
						"Update MPatcher: " + MPatcherUpdater.CurrentVersion + " → " + MPatcherUpdater.AvailableVersion,
						"MPatcherを更新: " + MPatcherUpdater.CurrentVersion + " → " + MPatcherUpdater.AvailableVersion);
				case MPatcherUpdaterState.Downloading:
					int percent = Mathf.RoundToInt(MPatchrUpdaterProgress() * 100f);
					return MPatcherUpdater.Localize("Загрузка обновления: ", "Downloading update: ", "更新をダウンロード中: ") + percent + "%";
				case MPatcherUpdaterState.Launching:
					return MPatcherUpdater.Localize("Перезапуск для обновления...", "Restarting to update...", "更新のため再起動しています...");
				case MPatcherUpdaterState.Failed:
					return MPatcherUpdater.Localize("Проверить обновление MPatcher", "Check for MPatcher update", "MPatcherの更新を確認");
				case MPatcherUpdaterState.Updated:
					return MPatcherUpdater.Localize("Обновление установлено", "Update installed", "更新をインストールしました");
				default:
					return MPatcherUpdater.Localize("Проверить обновление MPatcher", "Check for MPatcher update", "MPatcherの更新を確認");
			}
		}

		private static float MPatchrUpdaterProgress()
		{
			return MPatcherUpdater.DownloadProgress;
		}

		private static void ShowToastOnce(string key, string message)
		{
			if (toast == null || string.Equals(lastToastKey, key, System.StringComparison.Ordinal))
				return;
			lastToastKey = key;
			if (toastText != null)
				toastText.text = message;
			toast.gameObject.SetActive(true);
			toast.transform.SetAsLastSibling();
			MPatcherUpdater.Log("NOTICE_SHOWN key=" + key);
		}
	}
}
