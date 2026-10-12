using System;
using System.Collections;
using System.Collections.Generic;
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
		private readonly Dictionary<EventSystem, bool> blockedInputStates = new Dictionary<EventSystem, bool>();
		private readonly List<GameObject> ownedUiObjects = new List<GameObject>();
		private bool blockingActive;
		private bool retired;
		private string creationStage = "component";
		private GameObject overlay;
		private Text statusText;

		internal static void Begin(Action initializeAction)
		{
			if (initializeAction == null)
				throw new ArgumentNullException("initializeAction");
			GameObject gateObject = null;
			MPatcherStartupGate gate = null;
			string stage = "gate-object";
			try
			{
				runtimeReady = false;
				gateObject = new GameObject("MPatcherStartupGate");
				stage = "gate-persistence";
				UnityEngine.Object.DontDestroyOnLoad(gateObject);
				stage = "gate-component";
				gate = gateObject.AddComponent<MPatcherStartupGate>();
				gate.enabled = false;
				gate.StartGate(initializeAction);
			}
			catch (Exception error)
			{
				if (gate != null)
				{
					stage = gate.creationStage;
					gate.Cleanup("creation-failed");
				}
				if (gateObject != null)
				{
					gateObject.SetActive(false);
					Destroy(gateObject);
				}
				Log("CREATE_FAILED stage=" + stage + " fallback=synchronous type=" + error.GetType().Name
					+ " message=" + error.Message + " exception=" + error);
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
			creationStage = "overlay-show";
			overlay.SetActive(true);
			creationStage = "input-block";
			BlockInput();
			blockingActive = true;
			enabled = true;
			Log("VISIBLE inputBlocked=True");
			creationStage = "coroutine-start";
			StartCoroutine(Run());
			creationStage = "waiting-for-registration";
		}

		private IEnumerator Run()
		{
			// Load() is called on Unity's main thread. Let one complete frame present
			// the overlay before the synchronous Harmony registration work begins.
			yield return null;
			if (retired)
				yield break;
			float registrationStarted = Time.realtimeSinceStartup;
			SetStatus(Localize("Загрузка патчей...", "Loading patches...", "パッチを読み込んでいます..."));
			try
			{
				initialize();
			}
			catch (Exception error)
			{
				FailClosed("INITIALIZATION_FAILED type=" + error.GetType().Name + " message=" + error.Message
					+ " exception=" + error);
				yield break;
			}

			float registrationSeconds = Time.realtimeSinceStartup - registrationStarted;
			Log("PATCH_REGISTRATION_COMPLETE elapsedMs="
				+ Mathf.RoundToInt(registrationSeconds * 1000f).ToString(CultureInfo.InvariantCulture));
			SetStatus(Localize("Запуск MPatcher...", "Starting MPatcher...", "MPatcherを起動しています..."));

			float readyDeadline = Time.realtimeSinceStartup + RuntimeReadyTimeoutSeconds;
			while (!retired && !runtimeReady && Time.realtimeSinceStartup < readyDeadline)
				yield return null;
			if (retired)
				yield break;
			if (!runtimeReady)
			{
				FailClosed("RUNTIME_READY_TIMEOUT seconds="
					+ RuntimeReadyTimeoutSeconds.ToString(CultureInfo.InvariantCulture));
				yield break;
			}

			// Keep the blocker through the frame in which MPatchr.Start completed.
			yield return null;
			if (retired)
				yield break;
			Cleanup("ready");
			Log("HIDDEN inputBlocked=False totalMs="
				+ Mathf.RoundToInt((Time.realtimeSinceStartup - registrationStarted) * 1000f)
					.ToString(CultureInfo.InvariantCulture));
			Destroy(gameObject);
		}

		private void CreateOverlay()
		{
			creationStage = "overlay-object";
			overlay = CreateOwnedUiObject("MPatcherLoadingScreen");
			overlay.SetActive(false);
			creationStage = "overlay-persistence";
			UnityEngine.Object.DontDestroyOnLoad(overlay);
			creationStage = "overlay-canvas";
			Canvas canvas = overlay.AddComponent<Canvas>();
			canvas.renderMode = RenderMode.ScreenSpaceOverlay;
			canvas.sortingOrder = 32760;
			creationStage = "overlay-scaler";
			CanvasScaler scaler = overlay.AddComponent<CanvasScaler>();
			scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
			scaler.referenceResolution = new Vector2(1280f, 720f);
			scaler.matchWidthOrHeight = 0.5f;
			creationStage = "overlay-raycaster";
			overlay.AddComponent<GraphicRaycaster>();

			creationStage = "blocker-object";
			GameObject blocker = CreateOwnedUiObject("InputBlocker");
			creationStage = "blocker-rectangle";
			RectTransform blockerRect = blocker.AddComponent<RectTransform>();
			blockerRect.SetParent(overlay.transform, false);
			blockerRect.anchorMin = Vector2.zero;
			blockerRect.anchorMax = Vector2.one;
			blockerRect.offsetMin = Vector2.zero;
			blockerRect.offsetMax = Vector2.zero;
			creationStage = "blocker-image";
			Image background = blocker.AddComponent<Image>();
			background.color = new Color(0.025f, 0.03f, 0.045f, 0.97f);
			background.raycastTarget = true;

			creationStage = "overlay-font";
			Font font = Resources.GetBuiltinResource<Font>("Arial.ttf");
			Text title = CreateText("Title", blocker.transform, font, 34, FontStyle.Bold,
				new Vector2(0.15f, 0.51f), new Vector2(0.85f, 0.66f));
			title.text = "MPatcher";
			title.color = new Color(0.96f, 0.72f, 0.2f, 1f);
			statusText = CreateText("Status", blocker.transform, font, 20, FontStyle.Normal,
				new Vector2(0.15f, 0.39f), new Vector2(0.85f, 0.52f));
			SetStatus(Localize("Подготовка патчей...", "Preparing patches...", "パッチを準備しています..."));
		}

		private GameObject CreateOwnedUiObject(string name)
		{
			GameObject result = new GameObject(name);
			ownedUiObjects.Add(result);
			return result;
		}

		private Text CreateText(string name, Transform parent, Font font, int size,
			FontStyle style, Vector2 anchorMin, Vector2 anchorMax)
		{
			creationStage = "text-" + name + "-object";
			GameObject textObject = CreateOwnedUiObject(name);
			creationStage = "text-" + name + "-rectangle";
			RectTransform rect = textObject.AddComponent<RectTransform>();
			rect.SetParent(parent, false);
			rect.anchorMin = anchorMin;
			rect.anchorMax = anchorMax;
			rect.offsetMin = Vector2.zero;
			rect.offsetMax = Vector2.zero;
			creationStage = "text-" + name + "-component";
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
			EventSystem current = EventSystem.current;
			if (current == null)
				return;
			if (!blockedInputStates.ContainsKey(current))
				blockedInputStates.Add(current, current.enabled);
			current.enabled = false;
		}

		private void Update()
		{
			if (!blockingActive || retired)
				return;
			EventSystem current = EventSystem.current;
			if (current == null)
				return;
			bool rebound = !blockedInputStates.ContainsKey(current);
			BlockInput();
			if (rebound)
				Log("INPUT_SYSTEM_REBOUND blocked=True");
		}

		private void RestoreInput()
		{
			foreach (KeyValuePair<EventSystem, bool> saved in blockedInputStates)
			{
				try
				{
					if (saved.Key != null)
						saved.Key.enabled = saved.Value;
				}
				catch (Exception error)
				{
					Log("INPUT_RESTORE_FAILED exception=" + error);
				}
			}
			blockedInputStates.Clear();
		}

		private void Cleanup(string reason)
		{
			if (retired)
				return;
			retired = true;
			blockingActive = false;
			try { enabled = false; }
			catch (Exception error) { Log("COMPONENT_DISABLE_FAILED exception=" + error); }
			try { StopAllCoroutines(); }
			catch (Exception error) { Log("COROUTINE_STOP_FAILED exception=" + error); }
			int restoredSystems = blockedInputStates.Count;
			RestoreInput();
			for (int i = ownedUiObjects.Count - 1; i >= 0; i--)
			{
				GameObject owned = ownedUiObjects[i];
				if (owned == null)
					continue;
				try { owned.SetActive(false); }
				catch (Exception error) { Log("OVERLAY_DISABLE_FAILED exception=" + error); }
				try { Destroy(owned); }
				catch (Exception error) { Log("OVERLAY_DESTROY_FAILED exception=" + error); }
			}
			ownedUiObjects.Clear();
			overlay = null;
			statusText = null;
			Log("CLEANUP reason=" + reason + " restoredInputSystems="
				+ restoredSystems.ToString(CultureInfo.InvariantCulture) + " inputBlocked=False");
		}

		private void OnDisable()
		{
			if (blockingActive || overlay != null)
				Cleanup("disabled");
		}

		private void OnDestroy()
		{
			Cleanup("destroyed");
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
