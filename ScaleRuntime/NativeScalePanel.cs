using MPatcherFork.CustomPatches;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MPatcherFork.ScaleRuntime
{
    // Fresh controls share the game's visual assets, never its event listeners.
    internal sealed class NativeScalePanel
    {
        private readonly Build owner;
        private readonly ConnectedShapeScaleV2 scaler;
        private readonly Image buttonStyle, panelStyle, inputStyle, toggleStyle, checkStyle;
        private readonly Text textStyle, inputTextStyle;
        private readonly Selectable selectableStyle, toggleSelectableStyle;
        private readonly InputField nativeInput;
        private GameObject root;
        private RectTransform panel;
        private InputField coefficient;
        private Toggle center, anchor, preview, single;
        private Button apply;
        private Text summary, status, couplers;
        private readonly Dictionary<Text, string> captions = new Dictionary<Text, string>();
        private Text[] panelTexts;
        private bool? displayedJapanese;
        private Font displayedFont, displayedInputFont;
        private bool refreshing;
        private string panelSizeSource = "compact-fallback";

        internal bool NumericInputFocused
        {
            get { return root != null && root.activeInHierarchy && coefficient != null && coefficient.isFocused; }
        }

        internal bool ContainsPointer(Vector2 position)
        {
            return root != null && root.activeInHierarchy && panel != null
                && RectTransformUtility.RectangleContainsScreenPoint(panel, position, null);
        }

        internal NativeScalePanel(Build owner, Canvas sourceCanvas, GameObject sculptToggle,
            ConnectedShapeScaleV2 scaler)
        {
            if (owner == null || sourceCanvas == null || sculptToggle == null || scaler == null)
                throw new ArgumentNullException("Native Scale UI source");
            this.owner = owner;
            this.scaler = scaler;
            Toggle nativeToggle = sculptToggle.GetComponent<Toggle>();
            toggleSelectableStyle = nativeToggle;
            GameObject undoObject = owner.GetBTN("Undo");
            Button nativeButton = undoObject == null ? null : undoObject.GetComponent<Button>();
            selectableStyle = nativeButton != null ? (Selectable)nativeButton : nativeToggle;
            buttonStyle = nativeButton == null ? null : nativeButton.targetGraphic as Image;
            if (!HasSprite(buttonStyle)) buttonStyle = nativeToggle == null ? null : nativeToggle.targetGraphic as Image;
            if (!HasSprite(buttonStyle)) buttonStyle = FindSpriteImage(sculptToggle, false);
            if (!HasSprite(buttonStyle)) throw new MissingMemberException("Native Sculpt button sprite");
            textStyle = sculptToggle.GetComponentInChildren<Text>(true);
            if (textStyle == null || textStyle.font == null)
            {
                foreach (Text candidate in sourceCanvas.GetComponentsInChildren<Text>(true))
                    if (candidate.font != null) { textStyle = candidate; break; }
            }
            if (textStyle == null || textStyle.font == null)
                throw new MissingMemberException("Native Build font");
            panelStyle = FindPanelStyle(owner) ?? buttonStyle;
            InputField[] nativeInputs = sourceCanvas.GetComponentsInChildren<InputField>(true);
            nativeInput = nativeInputs.Length == 0 ? null : nativeInputs[0];
            foreach (InputField candidate in nativeInputs)
                if (candidate.name == "INP_Angle") { nativeInput = candidate; break; }
            inputStyle = nativeInput == null ? null : nativeInput.targetGraphic as Image;
            if (!HasSprite(inputStyle) && nativeInput != null) inputStyle = FindSpriteImage(nativeInput.gameObject, false);
            if (!HasSprite(inputStyle)) inputStyle = buttonStyle;
            inputTextStyle = nativeInput != null && nativeInput.textComponent != null ? nativeInput.textComponent : textStyle;
            toggleStyle = nativeToggle == null ? null : nativeToggle.targetGraphic as Image;
            if (!HasSprite(toggleStyle)) toggleStyle = buttonStyle;
            checkStyle = nativeToggle == null ? null : nativeToggle.graphic as Image;
            if (!HasSprite(checkStyle))
            {
                foreach (Toggle candidate in sourceCanvas.GetComponentsInChildren<Toggle>(true))
                {
                    Image check = candidate.graphic as Image;
                    Image background = candidate.targetGraphic as Image;
                    if (!HasSprite(check)) continue;
                    checkStyle = check;
                    if (HasSprite(background)) toggleStyle = background;
                    break;
                }
            }
            if (!HasSprite(checkStyle)) checkStyle = buttonStyle;
            try
            {
                Create(sourceCanvas);
                panelTexts = root.GetComponentsInChildren<Text>(true);
                Refresh();
                root.SetActive(false);
                Log("NATIVE_PANEL_READY panelSprite=" + SpriteName(panelStyle)
                    + " buttonSprite=" + SpriteName(buttonStyle) + " inputSprite=" + SpriteName(inputStyle)
                    + " toggleSprite=" + SpriteName(toggleStyle) + " checkSprite=" + SpriteName(checkStyle)
                    + " font=" + textStyle.font.name + " controls=fresh-native-assets"
                    + " size=" + panel.sizeDelta.ToString("F0") + " sizeSource=" + panelSizeSource);
            }
            catch
            {
                Dispose();
                throw;
            }
        }

        internal void Show()
        {
            if (root == null) throw new ObjectDisposedException("NativeScalePanel");
            Refresh();
            root.SetActive(true);
        }

        internal void Hide()
        {
            if (root != null) root.SetActive(false);
        }

        internal void Refresh()
        {
            if (root == null) return;
            refreshing = true;
            try
            {
                RefreshLanguage();
                bool selected = scaler.HasSelection;
                string value = scaler.ScaleText ?? string.Empty;
                // An unchanged assignment keeps a focused field's caret/selection intact.
                if (coefficient.text != value) coefficient.text = value;
                coefficient.interactable = selected;
                center.isOn = !scaler.UseAnchorPivot;
                anchor.isOn = scaler.UseAnchorPivot;
                preview.isOn = scaler.PreviewVisible;
                single.isOn = scaler.SingleCouplersOnly;
                center.interactable = anchor.interactable = preview.interactable = single.interactable = selected;
                summary.text = ScaleLocalization.Display(scaler.SummaryText);
                couplers.text = ScaleLocalization.Display(scaler.CouplerSummary);
                status.text = ScaleLocalization.Display(scaler.StatusText);
                apply.interactable = selected && scaler.CanApply;
            }
            finally { refreshing = false; }
        }

        private void RefreshLanguage()
        {
            bool japanese = HelpDefs.isJ;
            if (displayedJapanese != japanese)
            {
                foreach (KeyValuePair<Text, string> caption in captions)
                    caption.Key.text = ScaleLocalization.Display(caption.Value, japanese);
                displayedJapanese = japanese;
                Log("LANGUAGE_REFRESH language=" + (japanese ? "ja" : "en") + " source=HelpDefs.isJ");
            }
            // Native localization can change the font on the existing style
            // object. Reuse that font, including the coefficient input style.
            if (displayedFont != textStyle.font || displayedInputFont != inputTextStyle.font)
            {
                foreach (Text text in panelTexts)
                {
                    Text source = text == coefficient.textComponent ? inputTextStyle : textStyle;
                    text.font = source.font;
                    text.material = source.material;
                }
                displayedFont = textStyle.font;
                displayedInputFont = inputTextStyle.font;
            }
        }

        internal void Dispose()
        {
            if (root != null) UnityEngine.Object.Destroy(root);
            root = null;
            panel = null;
            coefficient = null;
        }

        private void Create(Canvas sourceCanvas)
        {
            root = new GameObject("MPatcher.NativeScaleCanvas", typeof(RectTransform), typeof(Canvas),
                typeof(CanvasScaler), typeof(GraphicRaycaster));
            Canvas canvas = root.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = 10000;
            canvas.pixelPerfect = sourceCanvas.pixelPerfect;
            CanvasScaler sourceScaler = sourceCanvas.GetComponent<CanvasScaler>();
            CanvasScaler destinationScaler = root.GetComponent<CanvasScaler>();
            if (sourceScaler != null)
            {
                destinationScaler.uiScaleMode = sourceScaler.uiScaleMode;
                destinationScaler.referenceResolution = sourceScaler.referenceResolution;
                destinationScaler.screenMatchMode = sourceScaler.screenMatchMode;
                destinationScaler.matchWidthOrHeight = sourceScaler.matchWidthOrHeight;
                destinationScaler.scaleFactor = sourceScaler.scaleFactor;
                destinationScaler.referencePixelsPerUnit = sourceScaler.referencePixelsPerUnit;
                destinationScaler.dynamicPixelsPerUnit = sourceScaler.dynamicPixelsPerUnit;
                destinationScaler.physicalUnit = sourceScaler.physicalUnit;
                destinationScaler.fallbackScreenDPI = sourceScaler.fallbackScreenDPI;
                destinationScaler.defaultSpriteDPI = sourceScaler.defaultSpriteDPI;
            }
            else canvas.scaleFactor = sourceCanvas.scaleFactor;
            // CanvasScaler updates on its own tick. Use the current native scale
            // while measuring the freshly created canvas during Build.Start.
            canvas.scaleFactor = sourceCanvas.scaleFactor;
            GameObject background = new GameObject("ScalePanel", typeof(RectTransform), typeof(Image), typeof(WidgetController));
            panel = background.GetComponent<RectTransform>();
            panel.SetParent(root.transform, false);
            panel.anchorMin = panel.anchorMax = new Vector2(1f, 1f);
            panel.pivot = new Vector2(1f, 1f);
            panel.anchoredPosition = new Vector2(-16f, -58f);
            panel.sizeDelta = ResolvePanelSize(sourceCanvas, canvas);
            CopyImage(background.GetComponent<Image>(), panelStyle, true);
            float width = panel.sizeDelta.x, height = panel.sizeDelta.y;
            float inner = width - 24f, half = (width - 28f) * 0.5f;
            AddText("Title", ScaleLocalization.Source("MPatcher: Scale", "MPatcher: スケール"), 12f, 4f, inner, 20f, TextAnchor.MiddleCenter, textStyle);
            AddText("SelectionTip", ScaleLocalization.Source("Ctrl+LMB + / Ctrl+RMB -", "Ctrl+左クリック + / Ctrl+右クリック -"), 12f, 24f, inner, 16f, TextAnchor.MiddleCenter, textStyle);
            summary = AddText("Summary", "", 12f, 42f, inner, 19f, TextAnchor.MiddleLeft, textStyle);
            AddText("CoefficientLabel", ScaleLocalization.Source("Factor", "倍率"), 12f, 65f, 92f, 26f, TextAnchor.MiddleLeft, textStyle);
            coefficient = MakeInput(108f, 65f, width - 120f, 26f);
            center = MakeToggle("CenterPivot", ScaleLocalization.Source("Group center", "グループの中心"), 12f, 96f, half, delegate(bool value)
            {
                if (refreshing) return;
                if (value) scaler.UseAnchorPivot = false;
                Refresh();
            });
            anchor = MakeToggle("AnchorPivot", ScaleLocalization.Source("Anchor block", "基準ブロック"), 16f + half, 96f, half, delegate(bool value)
            {
                if (refreshing) return;
                if (value) scaler.UseAnchorPivot = true;
                Refresh();
            });
            preview = MakeToggle("Preview", ScaleLocalization.Source("Shape preview", "形状プレビュー"), 12f, 124f, inner, delegate(bool value)
            {
                if (!refreshing) scaler.PreviewVisible = value;
            });
            single = MakeToggle("SingleCoupler", ScaleLocalization.Source("Single Couplers only", "Single Coupler のみ"), 12f, 152f, inner, delegate(bool value)
            {
                if (!refreshing) scaler.SingleCouplersOnly = value;
            });
            couplers = AddText("CouplerSummary", "", 12f, 180f, inner, 49f, TextAnchor.MiddleLeft, textStyle);
            float footer = height - 36f;
            status = AddText("Status", "", 12f, 234f, inner, footer - 240f, TextAnchor.UpperLeft, textStyle);
            status.horizontalOverflow = HorizontalWrapMode.Wrap;
            status.verticalOverflow = VerticalWrapMode.Truncate;
            apply = MakeButton("Apply", ScaleLocalization.Source("Apply", "適用"), 12f, footer, half, 28f,
                delegate { if (scaler.HasSelection && scaler.CanApply) scaler.ApplyFromInterface(); }, false);
            MakeButton("ReturnSculpt", ScaleLocalization.Source("Cancel", "キャンセル"), 16f + half, footer, half, 28f,
                delegate { SculptScaleUi.ReturnToSculpt(owner, "button"); }, false);
        }

        private Button MakeButton(string name, string caption, float x, float y, float width, float height,
            UnityEngine.Events.UnityAction action, bool selected)
        {
            GameObject element = new GameObject(name, typeof(RectTransform), typeof(Image), typeof(Button), typeof(WidgetController));
            Position(element.GetComponent<RectTransform>(), panel, x, y, width, height);
            Image image = element.GetComponent<Image>();
            CopyImage(image, buttonStyle, true);
            Button button = element.GetComponent<Button>();
            CopySelectable(button, selectableStyle);
            button.targetGraphic = image;
            button.onClick.AddListener(action);
            if (selected && selectableStyle != null)
            {
                ColorBlock colors = button.colors;
                colors.normalColor = selectableStyle.colors.pressedColor;
                colors.highlightedColor = selectableStyle.colors.pressedColor;
                button.colors = colors;
                if (selectableStyle.spriteState.pressedSprite != null)
                    image.overrideSprite = selectableStyle.spriteState.pressedSprite;
            }
            Text label = AddTextTo(element.transform, "Label", caption, textStyle);
            captions[label] = caption;
            Stretch(label.rectTransform, 4f, 1f);
            label.alignment = TextAnchor.MiddleCenter;
            return button;
        }

        private InputField MakeInput(float x, float y, float width, float height)
        {
            GameObject element = new GameObject("Coefficient", typeof(RectTransform), typeof(Image),
                typeof(InputField), typeof(WidgetController));
            Position(element.GetComponent<RectTransform>(), panel, x, y, width, height);
            Image image = element.GetComponent<Image>();
            CopyImage(image, inputStyle, true);
            InputField input = element.GetComponent<InputField>();
            CopySelectable(input, nativeInput != null ? (Selectable)nativeInput : selectableStyle);
            input.targetGraphic = image;
            Text label = AddTextTo(element.transform, "Text", "", inputTextStyle);
            Stretch(label.rectTransform, 7f, 1f);
            label.alignment = TextAnchor.MiddleLeft;
            input.textComponent = label;
            input.contentType = InputField.ContentType.Standard;
            input.lineType = InputField.LineType.SingleLine;
            input.characterValidation = InputField.CharacterValidation.None;
            if (nativeInput != null)
            {
                input.caretBlinkRate = nativeInput.caretBlinkRate;
                input.selectionColor = nativeInput.selectionColor;
            }
            input.onValueChanged.AddListener(delegate(string value)
            {
                if (!refreshing) scaler.ScaleText = value;
            });
            return input;
        }

        private Toggle MakeToggle(string name, string caption, float x, float y, float width,
            UnityEngine.Events.UnityAction<bool> changed)
        {
            GameObject element = new GameObject(name, typeof(RectTransform), typeof(Toggle), typeof(WidgetController));
            Position(element.GetComponent<RectTransform>(), panel, x, y, width, 26f);
            GameObject background = new GameObject("Background", typeof(RectTransform), typeof(Image));
            Position(background.GetComponent<RectTransform>(), element.transform, 0f, 2f, 22f, 22f);
            Image backgroundImage = background.GetComponent<Image>();
            CopyImage(backgroundImage, toggleStyle, true);
            GameObject mark = new GameObject("Checkmark", typeof(RectTransform), typeof(Image));
            Position(mark.GetComponent<RectTransform>(), background.transform, 2f, 2f, 18f, 18f);
            Image check = mark.GetComponent<Image>();
            CopyImage(check, checkStyle, false);
            Toggle toggle = element.GetComponent<Toggle>();
            CopySelectable(toggle, toggleSelectableStyle ?? selectableStyle);
            toggle.targetGraphic = backgroundImage;
            toggle.graphic = check;
            toggle.onValueChanged.AddListener(changed);
            Text label = AddTextTo(element.transform, "Label", caption, textStyle);
            captions[label] = caption;
            Position(label.rectTransform, element.transform, 28f, 0f, width - 28f, 26f);
            label.alignment = TextAnchor.MiddleLeft;
            // The label participates in hits, so the complete native-style row is clickable.
            label.raycastTarget = true;
            return toggle;
        }

        private Text AddText(string name, string value, float x, float y, float width, float height,
            TextAnchor alignment, Text source)
        {
            Text text = AddTextTo(panel, name, value, source);
            if (!string.IsNullOrEmpty(value)) captions[text] = value;
            Position(text.rectTransform, panel, x, y, width, height);
            text.alignment = alignment;
            return text;
        }

        private static Text AddTextTo(Transform parent, string name, string value, Text source)
        {
            GameObject element = new GameObject(name, typeof(RectTransform), typeof(Text));
            element.transform.SetParent(parent, false);
            Text text = element.GetComponent<Text>();
            text.font = source.font;
            text.fontSize = source.fontSize;
            text.fontStyle = source.fontStyle;
            text.color = source.color;
            text.material = source.material;
            text.lineSpacing = source.lineSpacing;
            text.supportRichText = source.supportRichText;
            text.resizeTextForBestFit = true;
            text.resizeTextMinSize = 12;
            text.resizeTextMaxSize = Mathf.Max(text.resizeTextMinSize, Mathf.Min(source.fontSize, 18));
            text.horizontalOverflow = HorizontalWrapMode.Wrap;
            text.verticalOverflow = VerticalWrapMode.Truncate;
            text.raycastTarget = false;
            text.text = value;
            Outline nativeOutline = source.GetComponent<Outline>();
            if (nativeOutline != null)
            {
                Outline outline = element.AddComponent<Outline>();
                outline.effectColor = nativeOutline.effectColor;
                outline.effectDistance = nativeOutline.effectDistance;
                outline.useGraphicAlpha = nativeOutline.useGraphicAlpha;
            }
            return text;
        }

        private static void CopySelectable(Selectable destination, Selectable source)
        {
            if (source == null) throw new MissingMemberException("Native Sculpt selectable style");
            destination.transition = source.transition;
            destination.colors = source.colors;
            destination.spriteState = source.spriteState;
            Navigation navigation = source.navigation;
            navigation.mode = Navigation.Mode.None;
            destination.navigation = navigation;
        }

        private static void CopyImage(Image destination, Image source, bool raycast)
        {
            if (!HasSprite(source)) throw new MissingMemberException("Native Build UI sprite");
            destination.sprite = source.sprite;
            destination.overrideSprite = source.overrideSprite;
            destination.type = source.type;
            destination.color = source.color;
            destination.material = source.material;
            destination.preserveAspect = source.preserveAspect;
            destination.fillCenter = source.fillCenter;
            destination.fillMethod = source.fillMethod;
            destination.fillAmount = source.fillAmount;
            destination.fillClockwise = source.fillClockwise;
            destination.fillOrigin = source.fillOrigin;
            destination.raycastTarget = raycast;
        }

        private static void Position(RectTransform rect, Transform parent, float x, float y, float width, float height)
        {
            rect.SetParent(parent, false);
            rect.anchorMin = rect.anchorMax = new Vector2(0f, 1f);
            rect.pivot = new Vector2(0f, 1f);
            rect.anchoredPosition = new Vector2(x, -y);
            rect.sizeDelta = new Vector2(width, height);
        }

        private static void Stretch(RectTransform rect, float horizontal, float vertical)
        {
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = new Vector2(horizontal, vertical);
            rect.offsetMax = new Vector2(-horizontal, -vertical);
        }

        private static bool HasSprite(Image image)
        {
            return image != null && (image.sprite != null || image.overrideSprite != null);
        }

        private Vector2 ResolvePanelSize(Canvas sourceCanvas, Canvas destinationCanvas)
        {
            foreach (string name in new string[] { "SetupC", "SetupBox", "SetupCap", "Setup" })
            {
                GameObject source;
                try { source = owner.GetPNL(name); }
                catch (Exception) { continue; }
                RectTransform rect = source == null ? null : source.GetComponent<RectTransform>();
                if (rect == null) continue;
                Vector3[] corners = new Vector3[4];
                rect.GetWorldCorners(corners);
                Camera camera = sourceCanvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : sourceCanvas.worldCamera;
                Vector2 lower = RectTransformUtility.WorldToScreenPoint(camera, corners[0]);
                Vector2 upper = RectTransformUtility.WorldToScreenPoint(camera, corners[1]);
                Vector2 right = RectTransformUtility.WorldToScreenPoint(camera, corners[2]);
                float scale = Mathf.Max(0.01f, destinationCanvas.scaleFactor);
                float width = Vector2.Distance(upper, right) / scale;
                float height = Vector2.Distance(lower, upper) / scale;
                if (!ValidDimension(width) || !ValidDimension(height)) continue;
                panelSizeSource = "PNL_" + name + "/rendered-bounds";
                return CompactPanelSize(width, height);
            }
            return CompactPanelSize(0f, 0f);
        }

        internal static Vector2 CompactPanelSize(float nativeWidth, float nativeHeight)
        {
            return new Vector2(ValidDimension(nativeWidth) ? Mathf.Clamp(nativeWidth, 240f, 320f) : 260f,
                ValidDimension(nativeHeight) ? Mathf.Clamp(nativeHeight, 340f, 360f) : 340f);
        }

        private static bool ValidDimension(float value)
        { return value > 0f && !float.IsNaN(value) && !float.IsInfinity(value); }

        private static Image FindPanelStyle(Build build)
        {
            Image best = null;
            foreach (string name in new string[] { "SetupC", "Setup", "System", "Paint", "Form" })
            {
                GameObject source = build.GetPNL(name);
                Image ownImage = source == null ? null : source.GetComponent<Image>();
                if (HasSprite(ownImage)) return ownImage;
                Image candidate = source == null ? null : FindSpriteImage(source, true);
                if (candidate != null && (best == null || Area(candidate) > Area(best))) best = candidate;
            }
            return best;
        }

        private static Image FindSpriteImage(GameObject source, bool borderedOnly)
        {
            Image best = null;
            foreach (Image image in source.GetComponentsInChildren<Image>(true))
            {
                if (!HasSprite(image)) continue;
                Sprite sprite = image.overrideSprite != null ? image.overrideSprite : image.sprite;
                if (borderedOnly && sprite.border == Vector4.zero) continue;
                if (best == null || Area(image) > Area(best)) best = image;
            }
            return best;
        }

        private static float Area(Image image)
        {
            RectTransform rect = image.GetComponent<RectTransform>();
            return rect == null ? 0f : Mathf.Abs(rect.rect.width * rect.rect.height);
        }

        private static string SpriteName(Image image)
        {
            Sprite sprite = image == null ? null : (image.overrideSprite != null ? image.overrideSprite : image.sprite);
            return sprite == null ? "unavailable" : sprite.name;
        }

        private static void Log(string message)
        {
            try { ScaleRuntimeHost.Log("[SCULPT-SCALE-UI] " + message); }
            catch (Exception) { }
        }
    }
}
