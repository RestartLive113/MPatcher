using System;
using MPatchrMain;
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
		private static ListController channelList;
		private static MPatcherUpdateChannelWatcher channelWatcher;
		private static GameObject mainMenuNotice;
		private static Text mainMenuNoticeText;
		private static MPatcherUpdateNoticeLifetime mainMenuNoticeLifetime;
		private static readonly string[] ChannelNames = new string[] { "Stable", "Alpha" };
		private static readonly Vector3 ChannelPosition = new Vector3(400f, -172f);
		private static readonly Vector3 UpdateButtonPosition = new Vector3(0f, -350f);

		internal static void Create(Transform settingsRoot)
		{
			if (settingsRoot == null)
				return;
			channelList = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.nN2N4qjnQLwFOaONUPeRAdg(
				"MPatcherUpdateChannel", MPatcherUpdater.Localize("Канал обновлений", "Update channel", "更新チャンネル"),
				ChannelPosition, ChannelNames, settingsRoot);
			string selectedName = GetChannelName(MPatcherUpdater.SelectedChannel);
			channelList.SetSelectedItem(selectedName);
			channelWatcher = channelList.gameObject.AddComponent<MPatcherUpdateChannelWatcher>();
			channelWatcher.Initialize(channelList, selectedName);

			updateButton = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.P3tLroX6fcPfQha_0024JdbpYXg(
				"Button_MPatchrUpdate", UpdateButtonPosition, string.Empty,
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
			MPatcherUpdater.Log("SETTINGS_CONTROLS_CREATED channel=" + selectedName
				+ " channelPlacement=under-vr-settings channelPosition=400,-172 buttonPosition=0,-350");
			Refresh();
		}

		internal static void BindLauncher(Control0 settingsLauncher)
		{
			if (settingsLauncher == null)
				return;
			launcherText = settingsLauncher.GetComponentInChildren<Text>();
			launcherBaseText = launcherText == null ? string.Empty : launcherText.text;
			Refresh();
		}

		internal static void BindMainMenu(Menu menu)
		{
			if (menu == null)
				return;
			Text template = FindMainMenuText(menu);
			Canvas canvas = template == null ? null : template.canvas;
			if (canvas == null)
			{
				MPatcherUpdater.Log("NOTICE_CREATE_SKIPPED reason=main-menu-canvas-missing");
				return;
			}
			if (mainMenuNotice != null)
				UnityEngine.Object.Destroy(mainMenuNotice);

			mainMenuNotice = new GameObject("MPatcherUpdateNotice");
			RectTransform rect = mainMenuNotice.AddComponent<RectTransform>();
			RectTransform templateRect = template.transform as RectTransform;
			rect.SetParent(template.transform.parent, false);
			rect.anchorMin = templateRect.anchorMin;
			rect.anchorMax = templateRect.anchorMax;
			rect.pivot = templateRect.pivot;
			rect.anchoredPosition = templateRect.anchoredPosition + new Vector2(-135f, 11f);
			rect.sizeDelta = new Vector2(Mathf.Max(templateRect.sizeDelta.x, 520f),
				Mathf.Max(templateRect.sizeDelta.y, 30f));
			rect.localScale = templateRect.localScale;
			rect.localRotation = templateRect.localRotation;
			CanvasGroup group = mainMenuNotice.AddComponent<CanvasGroup>();
			group.interactable = false;
			group.blocksRaycasts = false;
			mainMenuNoticeText = mainMenuNotice.AddComponent<Text>();
			mainMenuNoticeText.font = template.font;
			mainMenuNoticeText.fontSize = Mathf.Max(template.fontSize, 16);
			mainMenuNoticeText.fontStyle = FontStyle.Bold;
			mainMenuNoticeText.alignment = template.alignment;
			mainMenuNoticeText.horizontalOverflow = HorizontalWrapMode.Overflow;
			mainMenuNoticeText.verticalOverflow = VerticalWrapMode.Overflow;
			mainMenuNoticeText.color = new Color(1f, 0.68f, 0.18f, 1f);
			mainMenuNoticeText.raycastTarget = false;
			Outline outline = mainMenuNotice.AddComponent<Outline>();
			outline.effectColor = new Color(0f, 0f, 0f, 0.85f);
			outline.effectDistance = new Vector2(1f, -1f);
			mainMenuNoticeLifetime = mainMenuNotice.AddComponent<MPatcherUpdateNoticeLifetime>();
			mainMenuNoticeLifetime.Initialize(group);
			mainMenuNotice.transform.SetAsLastSibling();
			mainMenuNotice.SetActive(false);
			MPatcherUpdater.Log("NOTICE_CREATED background=none placement=version-stack offsetX=-135 offsetY=11 visibleSeconds=20 fadeSeconds=3 raycast=False");
			Refresh();
		}

		private static Text FindMainMenuText(Menu menu)
		{
			Text fallback = null;
			Text[] menuTexts = menu.GetComponentsInChildren<Text>(true);
			for (int i = 0; i < menuTexts.Length; i++)
			{
				Text candidate = menuTexts[i];
				if (candidate == null)
					continue;
				if (string.Equals(candidate.name, "Version", StringComparison.Ordinal))
					return candidate;
				if (candidate.text != null && candidate.text.IndexOf("Ver.0.248c", StringComparison.Ordinal) >= 0)
					fallback = candidate;
			}
			if (fallback != null)
				return fallback;

			Text[] allTexts = Resources.FindObjectsOfTypeAll<Text>();
			for (int i = 0; i < allTexts.Length; i++)
			{
				Text candidate = allTexts[i];
				if (candidate == null || candidate.canvas == null)
					continue;
				if (string.Equals(candidate.name, "Version", StringComparison.Ordinal)
					|| (candidate.text != null && candidate.text.IndexOf("Ver.0.248c", StringComparison.Ordinal) >= 0))
					return candidate;
			}
			return null;
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
			if (channelList != null)
			{
				string expectedChannel = GetChannelName(MPatcherUpdater.SelectedChannel);
				if (!string.Equals(channelList.GetSelectedItem(), expectedChannel, StringComparison.Ordinal))
					channelList.SetSelectedItem(expectedChannel);
			}

			bool available = state == MPatcherUpdaterState.Available;
			if (launcherText != null)
				launcherText.text = available
					? launcherBaseText + "  [UPDATE]"
					: launcherBaseText;

			if (mainMenuNotice != null)
			{
				bool updated = !available && !string.IsNullOrEmpty(MPatcherUpdater.LastInstalledVersion);
				if (available)
				{
					mainMenuNoticeText.color = new Color(1f, 0.68f, 0.18f, 1f);
					mainMenuNoticeText.text = MPatcherUpdater.Localize(
						"Доступно обновление MPatcher " + MPatcherUpdater.AvailableVersion,
						"MPatcher " + MPatcherUpdater.AvailableVersion + " available",
						"MPatcher " + MPatcherUpdater.AvailableVersion + " の更新");
					mainMenuNoticeLifetime.Show(GetChannelName(MPatcherUpdater.SelectedChannel)
						+ ":" + MPatcherUpdater.AvailableVersion);
				}
				else if (updated)
				{
					mainMenuNoticeText.color = new Color(0.45f, 1f, 0.55f, 1f);
					mainMenuNoticeText.text = MPatcherUpdater.Localize(
						"MPatcher успешно обновлён до " + MPatcherUpdater.LastInstalledVersion + ".",
						"MPatcher was updated successfully to " + MPatcherUpdater.LastInstalledVersion + ".",
						"MPatcher を " + MPatcherUpdater.LastInstalledVersion + " に更新しました。");
					mainMenuNoticeLifetime.Show("updated:" + MPatcherUpdater.LastInstalledVersion);
				}
				else
					mainMenuNoticeLifetime.ResetNotice();
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

		internal static void OnChannelSelectionChanged(string selected)
		{
			MPatcherReleaseChannel channel;
			if (string.Equals(selected, "Alpha", StringComparison.Ordinal))
				channel = MPatcherReleaseChannel.Alpha;
			else if (string.Equals(selected, "Stable", StringComparison.Ordinal))
				channel = MPatcherReleaseChannel.Stable;
			else
				return;
			settingsIngame settings = MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68;
			if (settings != null)
			{
				settings.updateChannel = channel == MPatcherReleaseChannel.Alpha
					? settingsIngame.updateChannels.alpha
					: settingsIngame.updateChannels.stable;
				settings.UUiRNMwxRbfk_Fs4cDErRoM();
			}
			MPatcherUpdater.Log("SETTINGS_CHANNEL_SELECTED value=" + selected);
			MPatcherUpdater.ChangeChannel(channel);
		}

		private static string GetChannelName(MPatcherReleaseChannel channel)
		{
			return channel == MPatcherReleaseChannel.Alpha ? "Alpha" : "Stable";
		}
	}

	internal sealed class MPatcherUpdateChannelWatcher : MonoBehaviour
	{
		private ListController list;
		private string lastValue;

		internal void Initialize(ListController target, string initialValue)
		{
			list = target;
			lastValue = initialValue;
		}

		private void Update()
		{
			if (list == null)
				return;
			string selected = list.GetSelectedItem();
			if (string.IsNullOrEmpty(selected) || string.Equals(selected, lastValue, StringComparison.Ordinal))
				return;
			lastValue = selected;
			MPatcherUpdaterUi.OnChannelSelectionChanged(selected);
		}
	}

	internal sealed class MPatcherUpdateNoticeLifetime : MonoBehaviour
	{
		private const float VisibleSeconds = 20f;
		private const float FadeSeconds = 3f;
		private CanvasGroup group;
		private float shownAt;
		private string shownKey = string.Empty;

		internal void Initialize(CanvasGroup target)
		{
			group = target;
			enabled = false;
		}

		internal void Show(string key)
		{
			if (string.Equals(shownKey, key, StringComparison.Ordinal))
				return;
			shownKey = key;
			shownAt = Time.realtimeSinceStartup;
			if (group != null)
				group.alpha = 1f;
			gameObject.SetActive(true);
			enabled = true;
			MPatcherUpdater.Log("NOTICE_SHOWN key=" + key + " visibleSeconds=20 fadeSeconds=3");
		}

		internal void ResetNotice()
		{
			shownKey = string.Empty;
			enabled = false;
			gameObject.SetActive(false);
		}

		private void Update()
		{
			float elapsed = Time.realtimeSinceStartup - shownAt;
			if (elapsed <= VisibleSeconds)
				return;
			float fade = (elapsed - VisibleSeconds) / FadeSeconds;
			if (fade < 1f)
			{
				if (group != null)
					group.alpha = 1f - fade;
				return;
			}
			if (group != null)
				group.alpha = 0f;
			enabled = false;
			gameObject.SetActive(false);
			MPatcherUpdater.Log("NOTICE_HIDDEN reason=timeout visibleSeconds=20 fadeSeconds=3");
		}
	}
}
