using System;
using System.Collections;
using System.Globalization;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MPatcherFork.CustomPatches
{
	internal sealed class MPatcherStartupGate : MonoBehaviour
	{
		private const float RuntimeReadyTimeoutSeconds = 30f;
		private static bool runtimeReady;
		private Action initialize;
		private EventSystem blockedEventSystem;
		private bool eventSystemWasEnabled;
		private GameObject overlay;
		private Text statusText;

		internal static void Begin(Action initializeAction)
		{
			if (initializeAction == null)
				throw new ArgumentNullException("initializeAction");
			try
			{
				runtimeReady = false;
				GameObject gateObject = new GameObject("MPatcherStartupGate");
				UnityEngine.Object.DontDestroyOnLoad(gateObject);
				MPatcherStartupGate gate = gateObject.AddComponent<MPatcherStartupGate>();
				gate.StartGate(initializeAction);
			}
			catch (Exception error)
			{
				Log("CREATE_FAILED fallback=synchronous type=" + error.GetType().Name + " message=" + error.Message);
				initializeAction();
			}
		}

		internal static void MarkRuntimeReady()
		{
			runtimeReady = true;
			Log("RUNTIME_READY");
		}

		private void StartGate(Action initializeAction)
		{
			initialize = initializeAction;
			CreateOverlay();
			BlockInput();
			Log("VISIBLE inputBlocked=True");
			StartCoroutine(Run());
		}

		private IEnumerator Run()
		{
			// Load() is called on Unity's main thread. Let one complete frame present
			// the overlay before the synchronous Harmony registration work begins.
			yield return null;
			float registrationStarted = Time.realtimeSinceStartup;
			SetStatus(Localize("Загрузка патчей...", "Loading patches...", "パッチを読み込んでいます..."));
			try
			{
				initialize();
			}
			catch (Exception error)
			{
				FailClosed("INITIALIZATION_FAILED type=" + error.GetType().Name + " message=" + error.Message);
				yield break;
			}

			float registrationSeconds = Time.realtimeSinceStartup - registrationStarted;
			Log("PATCH_REGISTRATION_COMPLETE elapsedMs="
				+ Mathf.RoundToInt(registrationSeconds * 1000f).ToString(CultureInfo.InvariantCulture));
			SetStatus(Localize("Запуск MPatcher...", "Starting MPatcher...", "MPatcherを起動しています..."));

			float readyDeadline = Time.realtimeSinceStartup + RuntimeReadyTimeoutSeconds;
			while (!runtimeReady && Time.realtimeSinceStartup < readyDeadline)
				yield return null;
			if (!runtimeReady)
			{
				FailClosed("RUNTIME_READY_TIMEOUT seconds="
					+ RuntimeReadyTimeoutSeconds.ToString(CultureInfo.InvariantCulture));
				yield break;
			}

			// Keep the blocker through the frame in which MPatchr.Start completed.
			yield return null;
			RestoreInput();
			Log("HIDDEN inputBlocked=False totalMs="
				+ Mathf.RoundToInt((Time.realtimeSinceStartup - registrationStarted) * 1000f)
					.ToString(CultureInfo.InvariantCulture));
			if (overlay != null)
				Destroy(overlay);
			Destroy(gameObject);
		}

		private void CreateOverlay()
		{
			overlay = new GameObject("MPatcherLoadingScreen");
			UnityEngine.Object.DontDestroyOnLoad(overlay);
			Canvas canvas = overlay.AddComponent<Canvas>();
			canvas.renderMode = RenderMode.ScreenSpaceOverlay;
			canvas.sortingOrder = 32760;
			CanvasScaler scaler = overlay.AddComponent<CanvasScaler>();
			scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
			scaler.referenceResolution = new Vector2(1280f, 720f);
			scaler.matchWidthOrHeight = 0.5f;
			overlay.AddComponent<GraphicRaycaster>();

			GameObject blocker = new GameObject("InputBlocker");
			RectTransform blockerRect = blocker.AddComponent<RectTransform>();
			blockerRect.SetParent(overlay.transform, false);
			blockerRect.anchorMin = Vector2.zero;
			blockerRect.anchorMax = Vector2.one;
			blockerRect.offsetMin = Vector2.zero;
			blockerRect.offsetMax = Vector2.zero;
			Image background = blocker.AddComponent<Image>();
			background.color = new Color(0.025f, 0.03f, 0.045f, 0.97f);
			background.raycastTarget = true;

			Font font = Resources.GetBuiltinResource<Font>("Arial.ttf");
			Text title = CreateText("Title", blocker.transform, font, 34, FontStyle.Bold,
				new Vector2(0.15f, 0.51f), new Vector2(0.85f, 0.66f));
			title.text = "MPatcher";
			title.color = new Color(0.96f, 0.72f, 0.2f, 1f);
			statusText = CreateText("Status", blocker.transform, font, 20, FontStyle.Normal,
				new Vector2(0.15f, 0.39f), new Vector2(0.85f, 0.52f));
			SetStatus(Localize("Подготовка патчей...", "Preparing patches...", "パッチを準備しています..."));
		}

		private static Text CreateText(string name, Transform parent, Font font, int size,
			FontStyle style, Vector2 anchorMin, Vector2 anchorMax)
		{
			GameObject textObject = new GameObject(name);
			RectTransform rect = textObject.AddComponent<RectTransform>();
			rect.SetParent(parent, false);
			rect.anchorMin = anchorMin;
			rect.anchorMax = anchorMax;
			rect.offsetMin = Vector2.zero;
			rect.offsetMax = Vector2.zero;
			Text text = textObject.AddComponent<Text>();
			text.font = font;
			text.fontSize = size;
			text.fontStyle = style;
			text.alignment = TextAnchor.MiddleCenter;
			text.color = Color.white;
			text.raycastTarget = false;
			text.horizontalOverflow = HorizontalWrapMode.Wrap;
			text.verticalOverflow = VerticalWrapMode.Overflow;
			return text;
		}

		private void BlockInput()
		{
			blockedEventSystem = EventSystem.current;
			if (blockedEventSystem == null)
				return;
			eventSystemWasEnabled = blockedEventSystem.enabled;
			blockedEventSystem.enabled = false;
		}

		private void Update()
		{
			EventSystem current = EventSystem.current;
			if (current == null || current == blockedEventSystem)
				return;
			blockedEventSystem = current;
			eventSystemWasEnabled = current.enabled;
			current.enabled = false;
			Log("INPUT_SYSTEM_REBOUND blocked=True");
		}

		private void RestoreInput()
		{
			if (blockedEventSystem != null)
				blockedEventSystem.enabled = eventSystemWasEnabled;
		}

		private void FailClosed(string diagnostic)
		{
			Log(diagnostic + " inputBlocked=True");
			SetStatus(Localize(
				"Не удалось загрузить MPatcher. Перезапустите игру и проверьте файл лога.",
				"MPatcher could not be loaded. Restart the game and check the log file.",
				"MPatcherを読み込めませんでした。ゲームを再起動してログを確認してください。"));
		}

		private void SetStatus(string value)
		{
			if (statusText != null)
				statusText.text = value;
		}

		private static string Localize(string russian, string english, string japanese)
		{
			string language = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
			if (string.Equals(language, "ru", StringComparison.OrdinalIgnoreCase)) return russian;
			if (string.Equals(language, "ja", StringComparison.OrdinalIgnoreCase)) return japanese;
			return english;
		}

		private static void Log(string message)
		{
			mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(
				"[MPatcher.StartupGate] " + message);
		}
	}
}
