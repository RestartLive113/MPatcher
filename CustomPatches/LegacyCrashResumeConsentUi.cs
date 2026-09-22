using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MPatcherFork.CustomPatches
{
	internal sealed class LegacyCrashResumeConsentUi
	{
		internal const string PromptTextEnglish =
			"Restore your machine to its position and pose from before the crash?";
		internal const string YesTextEnglish = "YES";
		internal const string NoTextEnglish = "NO";
		internal const string PromptTextJapanese =
			"クラッシュ前の位置と姿勢にマシンを復元しますか？";
		internal const string YesTextJapanese = "はい";
		internal const string NoTextJapanese = "いいえ";

		private GameObject root;
		private LegacyCrashResumeConsentInput input;
		private Action<bool> decision;
		private bool submitted;

		internal static LegacyCrashResumeConsentUi Create(Game game, Action<bool> decision)
		{
			if (game == null || decision == null) return null;
			GameObject reference = game.GetBTN("Exit");
			Canvas canvas = reference == null ? null : reference.GetComponentInParent<Canvas>();
			Text nativeText = reference == null ? null : reference.GetComponentInChildren<Text>(true);
			if (reference == null || canvas == null || nativeText == null) return null;

			LegacyCrashResumeConsentUi ui = new LegacyCrashResumeConsentUi();
			ui.decision = decision;
			Button yes;
			Button no;
			string style;
			if (!ui.TryCreateNativeDialog(game, canvas, out yes, out no))
			{
				ui.CreateFallbackDialog(canvas, reference, nativeText, out yes, out no);
				style = "fallback-framed";
			}
			else
			{
				style = "native-overwrite-clone";
			}

			ui.input = ui.root.AddComponent<LegacyCrashResumeConsentInput>();
			ui.input.Configure(game, yes.GetComponent<RectTransform>(), no.GetComponent<RectTransform>(),
				canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera,
				delegate(bool accept, string source) { ui.Submit(accept, source); });
			ui.root.SetActive(true);
			ui.root.transform.SetAsLastSibling();
			LegacyTransientReconnect.Log("CLIENT_CRASH_CONSENT_UI_STYLE source=" + style
				+ " language=" + (HelpDefs.isJ ? "ja" : "en"));
			return ui;
		}

		private bool TryCreateNativeDialog(Game game, Canvas canvas, out Button yes, out Button no)
		{
			yes = null;
			no = null;
			GameObject template = game.GetPNL("Overwrite");
			if (template == null) template = game.GetPNL("Confirm");
			if (template == null) template = game.GetPNL("Delete");
			if (template == null) return false;

			GameObject clone = UnityEngine.Object.Instantiate(template) as GameObject;
			if (clone == null) return false;
			clone.name = "MPatcher_CrashRestoreConsent";
			RectTransform cover = clone.GetComponent<RectTransform>();
			if (cover == null)
			{
				UnityEngine.Object.Destroy(clone);
				return false;
			}
			cover.SetParent(canvas.transform, false);
			cover.anchorMin = Vector2.zero;
			cover.anchorMax = Vector2.one;
			cover.offsetMin = Vector2.zero;
			cover.offsetMax = Vector2.zero;

			Transform panel = FindDescendant(cover, "pnl_Delete");
			if (panel == null) panel = FindDescendant(cover, "pnl_Confirm");
			Transform yesTransform = FindDescendant(cover, "BTN_Yes");
			Transform noTransform = FindDescendant(cover, "BTN_No");
			Transform promptTransform = FindDescendant(cover, "txt_Delete");
			if (promptTransform == null) promptTransform = FindDescendant(cover, "txt_Confirm");
			if (panel == null || yesTransform == null || noTransform == null
				|| promptTransform == null)
			{
				UnityEngine.Object.Destroy(clone);
				return false;
			}

			yes = yesTransform.GetComponent<Button>();
			no = noTransform.GetComponent<Button>();
			Text prompt = promptTransform.GetComponent<Text>();
			Text yesText = yesTransform.GetComponentInChildren<Text>(true);
			Text noText = noTransform.GetComponentInChildren<Text>(true);
			if (yes == null || no == null || prompt == null || yesText == null || noText == null)
			{
				UnityEngine.Object.Destroy(clone);
				yes = null;
				no = null;
				return false;
			}

			RectTransform panelRect = panel.GetComponent<RectTransform>();
			RectTransform promptRect = prompt.GetComponent<RectTransform>();
			RectTransform yesRect = yes.GetComponent<RectTransform>();
			RectTransform noRect = no.GetComponent<RectTransform>();
			panelRect.anchoredPosition = new Vector2(0f, 45f);
			panelRect.sizeDelta = new Vector2(620f, 230f);
			promptRect.anchoredPosition = new Vector2(0f, 38f);
			promptRect.sizeDelta = new Vector2(560f, 112f);
			yesRect.anchoredPosition = new Vector2(-145f, -68f);
			yesRect.sizeDelta = new Vector2(240f, 50f);
			noRect.anchoredPosition = new Vector2(145f, -68f);
			noRect.sizeDelta = new Vector2(240f, 50f);

			ApplyCopy(prompt, yesText, noText);
			PrepareNativeButton(yes, delegate { Submit(true, "unity-button"); });
			PrepareNativeButton(no, delegate { Submit(false, "unity-button"); });
			root = clone;
			return true;
		}

		private void CreateFallbackDialog(Canvas canvas, GameObject reference, Text nativeText,
			out Button yes, out Button no)
		{
			root = new GameObject("MPatcher_CrashRestoreConsent");
			RectTransform cover = root.AddComponent<RectTransform>();
			cover.SetParent(canvas.transform, false);
			cover.anchorMin = Vector2.zero;
			cover.anchorMax = Vector2.one;
			cover.offsetMin = Vector2.zero;
			cover.offsetMax = Vector2.zero;
			Image shade = root.AddComponent<Image>();
			shade.color = new Color(0f, 0f, 0f, 0.42f);
			shade.raycastTarget = true;

			GameObject panelObject = new GameObject("Panel");
			RectTransform panel = panelObject.AddComponent<RectTransform>();
			panel.SetParent(cover, false);
			panel.anchorMin = panel.anchorMax = new Vector2(0.5f, 0.5f);
			panel.pivot = new Vector2(0.5f, 0.5f);
			panel.anchoredPosition = new Vector2(0f, 45f);
			panel.sizeDelta = new Vector2(620f, 230f);
			Image panelImage = panelObject.AddComponent<Image>();
			panelImage.color = new Color(0.08f, 0.13f, 0.17f, 0.98f);
			panelImage.raycastTarget = true;
			Outline outline = panelObject.AddComponent<Outline>();
			outline.effectColor = new Color(0.25f, 0.72f, 0.92f, 0.9f);
			outline.effectDistance = new Vector2(2f, -2f);

			GameObject accentObject = new GameObject("Accent");
			RectTransform accent = accentObject.AddComponent<RectTransform>();
			accent.SetParent(panel, false);
			accent.anchorMin = new Vector2(0f, 1f);
			accent.anchorMax = new Vector2(1f, 1f);
			accent.pivot = new Vector2(0.5f, 1f);
			accent.anchoredPosition = Vector2.zero;
			accent.sizeDelta = new Vector2(0f, 7f);
			Image accentImage = accentObject.AddComponent<Image>();
			accentImage.color = new Color(0.15f, 0.67f, 0.92f, 1f);

			Text prompt = MakeText(panel, nativeText, new Vector2(0f, 38f),
				new Vector2(560f, 112f), "");
			yes = MakeButton(panel, reference, new Vector2(-145f, -68f), "",
				delegate { Submit(true, "unity-button"); });
			no = MakeButton(panel, reference, new Vector2(145f, -68f), "",
				delegate { Submit(false, "unity-button"); });
			ApplyCopy(prompt, yes.GetComponentInChildren<Text>(true),
				no.GetComponentInChildren<Text>(true));
		}

		private static void ApplyCopy(Text prompt, Text yesText, Text noText)
		{
			bool japanese = HelpDefs.isJ;
			string question = japanese ? PromptTextJapanese : PromptTextEnglish;
			ApplyPromptStyle(prompt);
			prompt.text = question;
			yesText.text = japanese ? YesTextJapanese : YesTextEnglish;
			noText.text = japanese ? NoTextJapanese : NoTextEnglish;
		}

		private static void ApplyPromptStyle(Text prompt)
		{
			prompt.fontStyle = FontStyle.Normal;
			prompt.color = Color.white;
			prompt.supportRichText = false;
			Shadow[] effects = prompt.GetComponents<Shadow>();
			for (int i = 0; i < effects.Length; i++) effects[i].enabled = false;
		}

		private static void PrepareNativeButton(Button button, UnityAction action)
		{
			ButtonController nativeController = button.GetComponent<ButtonController>();
			if (nativeController != null) nativeController.enabled = false;
			button.onClick = new Button.ButtonClickedEvent();
			button.onClick.AddListener(action);
		}

		private static Transform FindDescendant(Transform rootTransform, string name)
		{
			if (rootTransform == null) return null;
			Transform[] descendants = rootTransform.GetComponentsInChildren<Transform>(true);
			for (int i = 0; i < descendants.Length; i++)
			{
				if (descendants[i] != null
					&& string.Equals(descendants[i].name, name, StringComparison.Ordinal))
					return descendants[i];
			}
			return null;
		}

		private void Submit(bool accept, string source)
		{
			if (submitted) return;
			submitted = true;
			LegacyTransientReconnect.Log("CLIENT_CRASH_CONSENT_INPUT source=" + source
				+ " accepted=" + accept);
			Action<bool> callback = decision;
			if (callback != null) callback(accept);
		}

		private static Text MakeText(Transform parent, Text source, Vector2 position,
			Vector2 size, string caption)
		{
			GameObject item = new GameObject("Label");
			RectTransform rect = item.AddComponent<RectTransform>();
			rect.SetParent(parent, false);
			rect.anchorMin = rect.anchorMax = new Vector2(0.5f, 0.5f);
			rect.anchoredPosition = position;
			rect.sizeDelta = size;
			Text text = item.AddComponent<Text>();
			text.font = source.font;
			text.fontSize = source.fontSize;
			text.fontStyle = source.fontStyle;
			text.color = source.color;
			text.alignment = source.alignment;
			text.horizontalOverflow = source.horizontalOverflow;
			text.verticalOverflow = source.verticalOverflow;
			text.supportRichText = source.supportRichText;
			text.raycastTarget = false;
			text.text = caption;
			return text;
		}

		private static Button MakeButton(Transform parent, GameObject source,
			Vector2 position, string caption, UnityAction action)
		{
			GameObject item = new GameObject("Button_" + caption);
			RectTransform rect = item.AddComponent<RectTransform>();
			rect.SetParent(parent, false);
			rect.anchorMin = rect.anchorMax = new Vector2(0.5f, 0.5f);
			rect.anchoredPosition = position;
			rect.sizeDelta = new Vector2(240f, 50f);
			Image image = item.AddComponent<Image>();
			Image nativeImage = source.GetComponent<Image>();
			if (nativeImage != null)
			{
				image.sprite = nativeImage.sprite;
				image.type = nativeImage.type;
				image.color = nativeImage.color;
			}
			Button button = item.AddComponent<Button>();
			Button nativeButton = source.GetComponent<Button>();
			button.targetGraphic = image;
			if (nativeButton != null)
			{
				button.transition = nativeButton.transition;
				button.colors = nativeButton.colors;
				button.spriteState = nativeButton.spriteState;
			}
			button.onClick.AddListener(action);
			MakeText(rect, source.GetComponentInChildren<Text>(true), Vector2.zero,
				new Vector2(230f, 46f), caption);
			return button;
		}

		internal void Close()
		{
			if (root == null) return;
			if (input != null) input.RestoreInteractiveState();
			root.SetActive(false);
			UnityEngine.Object.Destroy(root);
			root = null;
			input = null;
			decision = null;
		}
	}

	internal sealed class LegacyCrashResumeConsentInput : MonoBehaviour
	{
		private RectTransform yes;
		private RectTransform no;
		private Camera uiCamera;
		private MonoBehaviour coroutineHost;
		private Action<bool, string> submit;
		private bool interactiveStateCaptured;
		private bool interactiveStateRestored;
		private bool previousCursorVisible;
		private CursorLockMode previousCursorLockState;
		private bool previousInputDisabled;
		private RideCameraController cameraController;
		private bool cameraControllerStateCaptured;
		private bool previousCameraControllerEnabled;

		internal void Configure(MonoBehaviour host, RectTransform yesButton, RectTransform noButton,
			Camera camera, Action<bool, string> callback)
		{
			coroutineHost = host;
			yes = yesButton;
			no = noButton;
			uiCamera = camera;
			submit = callback;
			previousCursorVisible = Cursor.visible;
			previousCursorLockState = Cursor.lockState;
			previousInputDisabled = HOCGCCAIPFF.NDIOFGDJAJO;
			interactiveStateCaptured = true;
			interactiveStateRestored = false;
			HOCGCCAIPFF.OHCMPEJKDHJ(3);
			HoldInteractiveState();
			LegacyTransientReconnect.Log("CLIENT_CRASH_CONSENT_INTERACTION_ACQUIRED previousCursorVisible="
				+ previousCursorVisible + " previousCursorLock=" + previousCursorLockState
				+ " previousInputDisabled=" + previousInputDisabled);
		}

		private void Update()
		{
			HoldInteractiveState();
			Action<bool, string> callback = submit;
			if (callback == null) return;
			if (Input.GetKeyDown(KeyCode.Y) || Input.GetKeyDown(KeyCode.Return)
				|| Input.GetKeyDown(KeyCode.KeypadEnter))
			{
				callback(true, "raw-keyboard-yes");
				return;
			}
			if (Input.GetKeyDown(KeyCode.N) || Input.GetKeyDown(KeyCode.Escape))
			{
				callback(false, "raw-keyboard-no");
				return;
			}
			if (!Input.GetMouseButtonDown(0)) return;
			Vector2 position = Input.mousePosition;
			if (yes != null && RectTransformUtility.RectangleContainsScreenPoint(
				yes, position, uiCamera))
			{
				callback(true, "raw-mouse-yes");
				return;
			}
			if (no != null && RectTransformUtility.RectangleContainsScreenPoint(
				no, position, uiCamera))
				callback(false, "raw-mouse-no");
		}

		private void LateUpdate()
		{
			HoldInteractiveState();
		}

		private void HoldInteractiveState()
		{
			if (!interactiveStateCaptured || interactiveStateRestored) return;
			HOCGCCAIPFF.NDIOFGDJAJO = true;
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			HoldCameraController();
		}

		private void HoldCameraController()
		{
			if (!cameraControllerStateCaptured)
			{
				Arena arena = Arena.OEDCBNHNGMJ;
				RideCameraController current = arena == null ? null : arena.BOIEJCIBHKI;
				if (current != null)
				{
					cameraController = current;
					previousCameraControllerEnabled = current.enabled;
					cameraControllerStateCaptured = true;
					LegacyTransientReconnect.Log("CLIENT_CRASH_CONSENT_CAMERA_SUSPENDED previousEnabled="
						+ previousCameraControllerEnabled);
				}
			}
			if (cameraController != null) cameraController.enabled = false;
		}

		internal void RestoreInteractiveState()
		{
			if (!interactiveStateCaptured || interactiveStateRestored) return;
			interactiveStateRestored = true;
			HOCGCCAIPFF.NDIOFGDJAJO = previousInputDisabled;
			Cursor.lockState = previousCursorLockState;
			Cursor.visible = previousCursorVisible;
			RestoreCameraController();
			LegacyTransientReconnect.Log("CLIENT_CRASH_CONSENT_INTERACTION_RESTORED cursorVisible="
				+ previousCursorVisible + " cursorLock=" + previousCursorLockState
				+ " inputDisabled=" + previousInputDisabled
				+ " cameraCaptured=" + cameraControllerStateCaptured
				+ " cameraRestoreDeferred=" + (cameraControllerStateCaptured
					&& previousCameraControllerEnabled));
		}

		private void RestoreCameraController()
		{
			if (!cameraControllerStateCaptured || cameraController == null) return;
			RideCameraController captured = cameraController;
			if (!previousCameraControllerEnabled)
			{
				captured.enabled = false;
				return;
			}
			if (coroutineHost != null && coroutineHost.gameObject.activeInHierarchy)
			{
				coroutineHost.StartCoroutine(RestoreCameraAfterPointerFrame(captured));
				return;
			}
			captured.enabled = true;
			LegacyTransientReconnect.Log("CLIENT_CRASH_CONSENT_CAMERA_RESTORED deferred=false");
		}

		private static IEnumerator RestoreCameraAfterPointerFrame(RideCameraController captured)
		{
			yield return null;
			if (captured != null) captured.enabled = true;
			LegacyTransientReconnect.Log("CLIENT_CRASH_CONSENT_CAMERA_RESTORED deferred=true");
		}

		private void OnDisable()
		{
			RestoreInteractiveState();
		}

		private void OnDestroy()
		{
			RestoreInteractiveState();
		}
	}
}
