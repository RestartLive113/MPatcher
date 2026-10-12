using MPatcherFork.CustomPatches;
using System;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MPatcherFork.ScaleRuntime
{
    internal sealed class SculptScaleUi : MonoBehaviour
    {
        private const string PatchId = "local.moddev.machinecraft.sculpt-scale-ui.v1";
        private static readonly FieldInfo Mode = AccessTools.Field(typeof(Build), "MKJJGPDCKFF");
        private static readonly FieldInfo SculptMode = AccessTools.Field(typeof(Build), "IFNEEGFBHAI");
        private static readonly FieldInfo Assembly = AccessTools.Field(typeof(Build), "FFJDGJFPLAD");
        private static readonly FieldInfo CameraControl = AccessTools.Field(typeof(Build), "BOIEJCIBHKI");
        private static readonly MethodInfo OpenNative = AccessTools.Method(typeof(Build), "FBBJKNANFAG");
        private static readonly MethodInfo HideNativeEditors = AccessTools.Method(typeof(Build), "HPEIPAKOMJB", Type.EmptyTypes);
        private static readonly MethodInfo RawKeyDown = AccessTools.Method(typeof(HOCGCCAIPFF), "FGCCNKAIKAI",
            new Type[] { typeof(KeyCode) });
        private static SculptScaleUi current;
        private static bool registered;

        private Build build;
        private ConnectedShapeScaleV2 scaler;
        private Canvas nativeCanvas;
        private GameObject tabStrip;
        private RectTransform nativeTab, stripRect;
        private CanvasGroup nativeTabVisibility;
        private float originalAlpha;
        private bool originalBlocksRaycasts, originalInteractable, addedVisibility;
        private bool tabHidden;
        private bool active;
        private bool previewObserved;
        private bool updatingTabs;
        private Toggle sculptTab, scaleTab;
        private Text nativeTabText, sculptTabText, scaleTabText;
        private bool? displayedJapanese;
        private Font displayedTabFont;
        private bool cameraLookHeld;
        private bool viewportPress;
        private int viewportButton = -1;
        private bool viewportControl;
        private Vector2 viewportPressPosition;
        private float viewportPressTime;
        private readonly List<RaycastResult> cameraUiHits = new List<RaycastResult>();
        private int swallowedFrame = -1;
        private object savedSculptMode;
        private bool savedCanvasActive, savedCursorVisible, savedCameraEnabled, savedCameraCapture;
        private CursorLockMode savedCursorLock;
        private FreeCameraController cameraControl;
        private NativeScalePanel nativePanel;
        private ShapeScaleVisibility scaleVisibility;

        internal static void InitializeRuntime()
        {
            registered = true;
            Log("RUNTIME_INITIALIZED nativeHooks=stable-core entry=Sculpt-Scale key=2");
        }

        private static void NativeTabPrefix(Build __instance)
        {
            BeforeNativeTab(__instance);
        }

        internal static void BeforeNativeTab(Build owner)
        {
            if (current != null && current.build == owner && current.active)
                current.LeaveScale("native-tab", false);
        }

        private static void NativeMaskPrefix(Build __instance)
        {
            BeforeNativeMask(__instance);
        }

        internal static void BeforeNativeMask(Build owner)
        {
            if (current != null && current.build == owner && current.active)
                current.LeaveScale("native-mask", true);
        }

        internal static void Attach(Build owner, ConnectedShapeScaleV2 scale)
        {
            if (!registered || owner == null || scale == null) return;
            SculptScaleUi ui = owner.GetComponent<SculptScaleUi>();
            if (ui == null) ui = owner.gameObject.AddComponent<SculptScaleUi>();
            ui.build = owner;
            ui.scaler = scale;
            current = ui;
            try { ui.CreateTabs(); }
            catch (Exception error) { Log("TABS_FAILED " + error); }
            Log("BUILD_READY entry=2 cursor=automatic");
        }

        internal static bool IsScaleActive(Build owner)
        {
            return current != null && current.build == owner && current.active;
        }

        internal static bool NumericInputFocused(Build owner)
        {
            return IsScaleActive(owner) && current.nativePanel != null && current.nativePanel.NumericInputFocused;
        }

        internal static BlockController PickOwner(Transform transform)
        {
            return current != null && current.active && current.scaleVisibility != null
                ? current.scaleVisibility.PickOwner(transform) : null;
        }

        internal static bool TryCarrierSurface(BlockController owner, Ray ray, out float distance)
        {
            distance = float.PositiveInfinity;
            return current != null && current.active && current.scaleVisibility != null
                && current.scaleVisibility.TryCarrierSurface(owner, ray, out distance);
        }

        internal static bool OwnsCamera(CameraController camera)
        {
            return current != null && current.active && current.cameraControl == camera;
        }

        internal static bool CameraKeyboardAllowed
        {
            get { return current != null && current.active && current.savedCameraEnabled
                && Application.isFocused && !current.TextFocused(); }
        }

        internal static bool CameraLookHeld
        {
            get { return CameraKeyboardAllowed && !current.viewportControl && current.cameraLookHeld; }
        }

        internal static bool FilterMappedKeyDown(SystemData.EHLMFKOOHLI action, bool pressed)
        {
            SculptScaleUi ui = current;
            if (!registered || ui == null || ui.build == null || !pressed) return pressed;
            bool typing = ui.TextFocused();
            if (action == SystemData.EHLMFKOOHLI.Preview)
            {
                if (ui.active && typing) return false;
                if (SculptScaleInputPolicy.LeaveForPreview(ui.active, pressed, typing))
                    ui.LeaveScale("native-preview-key", false);
                return pressed;
            }
            if (ui.swallowedFrame == Time.frameCount
                && SculptScaleInputPolicy.IsNativeSheetAction(action)) return false;
            if (!ui.active && action == SystemData.EHLMFKOOHLI.Sculpt
                && Input.GetKeyDown(KeyCode.Alpha2)
                && SculptScaleInputPolicy.DecideTwo(false, ui.NativeSculptActive(), typing) == SculptScaleCommand.EnterScale)
            {
                ui.EnterScale("key2");
                return false;
            }
            SculptScaleNativeSheetCommand command = SculptScaleInputPolicy.DecideNativeSheet(action, pressed, ui.active, typing);
            if (command != SculptScaleNativeSheetCommand.None)
            {
                bool reopenSculpt = command == SculptScaleNativeSheetCommand.LeaveScaleForSculptOrMask;
                ui.LeaveScale("native-key-" + action, reopenSculpt);
                Log("NATIVE_SHEET_RETURN action=" + action + " reopenSculpt=" + reopenSculpt + " nativeTarget=pending");
                // Reopening already handled Sculpt. Native masks still run after
                // that restoration; the other sheets use their original opener.
                if (action == SystemData.EHLMFKOOHLI.Sculpt) return false;
                return true;
            }
            bool allowed = SculptScaleInputPolicy.FilterNativeSheetHotkey(action, pressed, ui.active, typing);
            if (!allowed) Log("NATIVE_SHEET_BLOCKED action=" + action + " typing=" + typing);
            return allowed;
        }

        private bool NativeSculptActive()
        {
            return build != null && Convert.ToInt32(Mode.GetValue(build)) == 1;
        }

        private bool NativePreviewActive()
        {
            HIPBCCKFFAG assembly = build == null ? null : Assembly.GetValue(build) as HIPBCCKFFAG;
            return assembly != null && assembly.HCMMJPFOIHD;
        }

        private bool SynchronizePreviewMode()
        {
            bool preview = NativePreviewActive();
            if (preview != previewObserved)
            {
                previewObserved = preview;
                Log("PREVIEW_TRANSITION native=" + preview + " scale=" + active + " owner=native-preview");
            }
            if (preview && active) LeaveScale("native-preview-state", false);
            return preview;
        }

        private bool TextFocused()
        {
            if (active && scaler != null && scaler.NumericInputFocused) return true;
            EventSystem events = EventSystem.current;
            GameObject selected = events == null ? null : events.currentSelectedGameObject;
            InputField input = selected == null ? null : selected.GetComponent<InputField>();
            return input != null && input.isFocused && input.gameObject.activeInHierarchy;
        }

        private void Update()
        {
            if (build == null) return;
            SynchronizePreviewMode();
            if (!active) { ShowNativeTabs(true); return; }
            UpdateCameraGesture();
            if (swallowedFrame == Time.frameCount) return;
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SculptScaleCommand command = SculptScaleInputPolicy.DecideTwo(true, false, TextFocused());
                if (command == SculptScaleCommand.ReturnSculpt) LeaveScale("key2", true);
                else Log("DIGIT_INPUT key=2 mode=Scale");
            }
            else if (Input.GetKeyDown(KeyCode.Escape)) LeaveScale("escape", true);
        }

        private void LateUpdate()
        {
            if (build == null) return;
            if (SynchronizePreviewMode()) { ShowNativeTabs(true); return; }
            if (!active) return;
            Mode.SetValue(build, Enum.ToObject(Mode.FieldType, -1));
            ShowNativeTabs(true);
            if (cameraControl != null)
            {
                cameraControl.enabled = savedCameraEnabled;
                cameraControl.FMGOKAGJMJH = CameraKeyboardAllowed;
            }
            Cursor.lockState = CameraLookHeld ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !CameraLookHeld;
            if (nativePanel != null) nativePanel.Refresh();
            if (scaleVisibility != null) scaleVisibility.Refresh();
        }

        private void UpdateCameraGesture()
        {
            if (viewportButton >= 0 && Input.GetMouseButtonUp(viewportButton))
            {
                ShapeSelectionOperation operation;
                bool select = SculptScaleInputPolicy.TryViewportSelection(viewportButton, viewportControl,
                    viewportPress, cameraLookHeld, CameraKeyboardAllowed,
                    Vector2.Distance(viewportPressPosition, Input.mousePosition),
                    Time.unscaledTime - viewportPressTime, out operation);
                ResetViewportGesture();
                if (select && scaler != null)
                {
                    Log("SELECTION_GESTURE operation=" + operation + " source=short-viewport-tap control=captured-at-press");
                    scaler.SelectFromPointer(operation);
                }
                return;
            }
            if (!CameraKeyboardAllowed)
            {
                ResetViewportGesture();
                return;
            }
            if (viewportButton >= 0)
            {
                if (!Input.GetMouseButton(viewportButton)) { ResetViewportGesture(); return; }
                if (SculptScaleInputPolicy.ShouldStartCameraLook(viewportButton, viewportControl, viewportPress,
                    CameraKeyboardAllowed, Vector2.Distance(viewportPressPosition, Input.mousePosition),
                    Time.unscaledTime - viewportPressTime)) cameraLookHeld = true;
                return;
            }
            bool control = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);
            int button = Input.GetMouseButtonDown(0) ? 0 : control && Input.GetMouseButtonDown(1) ? 1 : -1;
            if (button < 0) return; // Ordinary RMB keeps its existing Scale behavior.
            EventSystem events = EventSystem.current;
            bool overUi = nativePanel != null && nativePanel.ContainsPointer(Input.mousePosition);
            if (events != null && !overUi)
            {
                cameraUiHits.Clear();
                events.RaycastAll(new PointerEventData(events) { position = Input.mousePosition }, cameraUiHits);
                foreach (RaycastResult hit in cameraUiHits)
                    if (hit.gameObject != null && (hit.gameObject.GetComponentInParent<Selectable>() != null
                        || hit.gameObject.GetComponentInParent<WidgetController>() != null))
                    { overUi = true; break; }
            }
            viewportPress = !overUi;
            viewportButton = button;
            viewportControl = control;
            cameraLookHeld = false;
            viewportPressPosition = Input.mousePosition;
            viewportPressTime = Time.unscaledTime;
            Log("CAMERA_GESTURE candidate=" + viewportPress + " pointerOverUi=" + overUi
                + " button=" + viewportButton + " controlAtPress=" + viewportControl);
        }

        private void ResetViewportGesture()
        {
            viewportPress = cameraLookHeld = viewportControl = false;
            viewportButton = -1;
        }

        private void EnterScale(string source)
        {
            if (active || build == null || scaler == null || NativePreviewActive() || !NativeSculptActive()) return;
            try
            {
                if (nativeCanvas == null || nativePanel == null) CreateTabs();
                if (nativeCanvas == null || nativeCanvas.gameObject == build.gameObject
                    || build.transform.IsChildOf(nativeCanvas.transform))
                    throw new InvalidOperationException("Native Build canvas is unavailable.");
                savedSculptMode = SculptMode.GetValue(build);
                cameraControl = CameraControl.GetValue(build) as FreeCameraController;
                savedCanvasActive = nativeCanvas.gameObject.activeSelf;
                savedCursorVisible = Cursor.visible;
                savedCursorLock = Cursor.lockState;
                savedCameraEnabled = cameraControl != null && cameraControl.enabled;
                savedCameraCapture = cameraControl != null && cameraControl.FMGOKAGJMJH;
                active = true;
                if (scaleVisibility == null) scaleVisibility = new ShapeScaleVisibility(build, scaler);
                swallowedFrame = Time.frameCount;
                scaler.BeginManualSelection();
                scaleVisibility.Refresh();
                EventSystem events = EventSystem.current;
                if (events != null) events.SetSelectedGameObject(null);
                HideNativeEditors.Invoke(build, null);
                Mode.SetValue(build, Enum.ToObject(Mode.FieldType, -1));
                nativePanel.Show();
                LateUpdate();
                Log("MODE_CHANGED mode=Scale source=" + source + " sculptSubmode=" + savedSculptMode
                    + " nativeCanvas=visible editorPanels=hidden cursor=free selected=" + scaler.HasSelection);
            }
            catch (Exception error)
            {
                Log("ENTER_FAILED " + error);
                if (active) LeaveScale("entry-failed", true);
            }
        }

        internal static void ReturnToSculpt(Build owner, string source)
        {
            if (current != null && current.build == owner) current.LeaveScale(source, true);
        }

        private void LeaveScale(string source, bool reopenSculpt)
        {
            if (!active) return;
            bool nativePreview = NativePreviewActive();
            if (nativePreview) reopenSculpt = false;
            active = false;
            ResetViewportGesture();
            swallowedFrame = Time.frameCount;
            try
            {
                try { if (scaleVisibility != null) scaleVisibility.Restore(); }
                catch (Exception error) { Log("VISIBILITY_RESTORE_FAILED " + error); }
                try { if (nativePanel != null) nativePanel.Hide(); }
                catch (Exception error) { Log("PANEL_CLOSE_FAILED " + error); }
                try { if (scaler != null) scaler.CloseFromMode(); }
                catch (Exception error) { Log("CLOSE_FAILED " + error); }
                try { if (!nativePreview && nativeCanvas != null) nativeCanvas.gameObject.SetActive(savedCanvasActive); }
                catch (Exception error) { Log("CANVAS_RESTORE_FAILED " + error); }
                try
                {
                    if (build != null && savedSculptMode != null)
                    {
                        SculptMode.SetValue(build, savedSculptMode);
                        if (reopenSculpt && build.gameObject.activeInHierarchy)
                            OpenNative.Invoke(build, new object[] { Enum.ToObject(Mode.FieldType, 1) });
                        else Mode.SetValue(build, Enum.ToObject(Mode.FieldType, 1));
                    }
                }
                catch (Exception error)
                {
                    Log("SCULPT_RESTORE_FAILED " + error);
                    if (build != null) Mode.SetValue(build, Enum.ToObject(Mode.FieldType, 1));
                }
            }
            finally
            {
                if (!nativePreview && cameraControl != null)
                {
                    cameraControl.enabled = savedCameraEnabled;
                    cameraControl.FMGOKAGJMJH = savedCameraCapture;
                }
                if (!nativePreview)
                {
                    Cursor.lockState = savedCursorLock;
                    Cursor.visible = savedCursorVisible;
                }
                try { ShowNativeTabs(true); }
                catch (Exception error) { Log("TABS_RESTORE_FAILED " + error); }
                Log("MODE_CHANGED mode=" + (nativePreview ? "Preview" : "Sculpt") + " source=" + source
                    + " sculptSubmode=" + savedSculptMode + " nativeCanvas=" + (nativePreview ? "preview-owned" : "restored")
                    + " cursorRestored=" + (!nativePreview));
            }
        }

        private void CreateTabs()
        {
            GameObject source = build.GetTGL("Sculpt");
            if (source == null) throw new MissingMemberException("Build Sculpt toggle");
            nativeTab = source.GetComponent<RectTransform>();
            Canvas[] parents = source.GetComponentsInParent<Canvas>(true);
            if (nativeTab == null || parents.Length == 0) throw new MissingMemberException("Build Sculpt canvas");
            nativeCanvas = parents[parents.Length - 1];
            if (nativePanel == null) nativePanel = new NativeScalePanel(build, nativeCanvas, source, scaler);
            if (tabStrip != null) return;
            nativeTabVisibility = source.GetComponent<CanvasGroup>();
            addedVisibility = nativeTabVisibility == null;
            if (addedVisibility) nativeTabVisibility = source.AddComponent<CanvasGroup>();
            originalAlpha = nativeTabVisibility.alpha;
            originalBlocksRaycasts = nativeTabVisibility.blocksRaycasts;
            originalInteractable = nativeTabVisibility.interactable;
            tabStrip = new GameObject("MPatcher.SculptScaleTabs", typeof(RectTransform));
            stripRect = tabStrip.GetComponent<RectTransform>();
            stripRect.SetParent(nativeTab.parent, false);
            tabStrip.AddComponent<LayoutElement>().ignoreLayout = true;
            Text nativeText = source.GetComponentInChildren<Text>(true);
            if (nativeText == null || nativeText.font == null) throw new MissingMemberException("Native Sculpt font");
            nativeTabText = nativeText;
            Toggle nativeToggle = source.GetComponent<Toggle>();
            Image nativeImage = nativeToggle == null ? source.GetComponent<Image>() : nativeToggle.targetGraphic as Image;
            if (nativeImage == null) nativeImage = source.GetComponentInChildren<Image>(true);
            if (nativeImage == null || nativeImage.sprite == null) throw new MissingMemberException("Native Sculpt button texture");
            ColorBlock colors = nativeToggle == null ? ColorBlock.defaultColorBlock : nativeToggle.colors;
            ToggleGroup group = tabStrip.AddComponent<ToggleGroup>();
            group.allowSwitchOff = true;
            sculptTab = CreateTab("Sculpt", 0f, 0.5f, nativeText, nativeImage, colors, nativeToggle, group,
                delegate { if (active) LeaveScale("tab", true); else SelectNativeSculpt(); });
            scaleTab = CreateTab("Scale", 0.5f, 1f, nativeText, nativeImage, colors, nativeToggle, group,
                delegate { if (!active) { SelectNativeSculpt(); EnterScale("tab"); } });
            sculptTabText = sculptTab.GetComponentInChildren<Text>(true);
            scaleTabText = scaleTab.GetComponentInChildren<Text>(true);
            ShowNativeTabs(true);
            Log("TABS_READY anchor=" + source.name + " layout=native-toggle-rectangle sprite=" + nativeImage.sprite.name);
        }

        private void SelectNativeSculpt()
        {
            if (build == null || active || NativePreviewActive()) return;
            Toggle original = nativeTab == null ? null : nativeTab.GetComponent<Toggle>();
            if (original != null && !original.isOn) original.isOn = true;
            if (!NativeSculptActive())
                OpenNative.Invoke(build, new object[] { Enum.ToObject(Mode.FieldType, 1) });
        }

        private Toggle CreateTab(string label, float left, float right, Text sourceText, Image sourceImage,
            ColorBlock colors, Toggle sourceToggle, ToggleGroup group, UnityEngine.Events.UnityAction click)
        {
            GameObject button = new GameObject(label, typeof(RectTransform), typeof(Image), typeof(Toggle));
            RectTransform rect = button.GetComponent<RectTransform>();
            rect.SetParent(stripRect, false);
            rect.anchorMin = new Vector2(left, 0f); rect.anchorMax = new Vector2(right, 1f);
            rect.offsetMin = Vector2.zero; rect.offsetMax = Vector2.zero;
            Image background = button.GetComponent<Image>();
            background.sprite = sourceImage.sprite;
            background.type = sourceImage.type;
            background.material = sourceImage.material;
            background.color = sourceImage.color;
            Toggle control = button.GetComponent<Toggle>();
            control.targetGraphic = background;
            control.colors = colors;
            control.group = group;
            if (sourceToggle != null)
            {
                control.transition = sourceToggle.transition;
                control.spriteState = sourceToggle.spriteState;
            }
            if (sourceToggle != null && sourceToggle.graphic is Image)
            {
                Image sourceCheck = (Image)sourceToggle.graphic;
                GameObject mark = new GameObject("Checkmark", typeof(RectTransform), typeof(Image));
                RectTransform markRect = mark.GetComponent<RectTransform>();
                markRect.SetParent(rect, false);
                markRect.anchorMin = Vector2.zero; markRect.anchorMax = Vector2.one;
                markRect.offsetMin = Vector2.zero; markRect.offsetMax = Vector2.zero;
                Image check = mark.GetComponent<Image>();
                check.sprite = sourceCheck.sprite; check.overrideSprite = sourceCheck.overrideSprite;
                check.type = sourceCheck.type; check.material = sourceCheck.material;
                check.color = sourceCheck.color; check.raycastTarget = false;
                control.graphic = check;
                control.toggleTransition = sourceToggle.toggleTransition;
            }
            control.onValueChanged.AddListener(delegate(bool value) { if (value && !updatingTabs) click(); });
            button.AddComponent<WidgetController>();
            GameObject caption = new GameObject("Label", typeof(RectTransform), typeof(Text));
            RectTransform textRect = caption.GetComponent<RectTransform>();
            textRect.SetParent(rect, false); textRect.anchorMin = Vector2.zero; textRect.anchorMax = Vector2.one;
            textRect.offsetMin = new Vector2(2f, 0f); textRect.offsetMax = new Vector2(-2f, 0f);
            Text text = caption.GetComponent<Text>();
            text.text = label; text.font = sourceText.font; text.fontSize = 16; text.color = sourceText.color;
            text.fontStyle = sourceText.fontStyle; text.material = sourceText.material;
            text.alignment = TextAnchor.MiddleCenter; text.raycastTarget = false;
            text.resizeTextForBestFit = true; text.resizeTextMinSize = 10; text.resizeTextMaxSize = 16;
            Outline originalOutline = sourceText.GetComponent<Outline>();
            if (originalOutline != null)
            {
                Outline outline = caption.AddComponent<Outline>();
                outline.effectColor = originalOutline.effectColor;
                outline.effectDistance = originalOutline.effectDistance;
                outline.useGraphicAlpha = originalOutline.useGraphicAlpha;
            }
            return control;
        }

        private void ShowNativeTabs(bool show)
        {
            if (tabStrip == null || nativeTabVisibility == null || nativeTab == null) return;
            RefreshTabLanguage();
            stripRect.anchorMin = nativeTab.anchorMin; stripRect.anchorMax = nativeTab.anchorMax;
            stripRect.pivot = nativeTab.pivot; stripRect.sizeDelta = nativeTab.sizeDelta;
            stripRect.anchoredPosition = nativeTab.anchoredPosition;
            stripRect.localScale = nativeTab.localScale; stripRect.localRotation = nativeTab.localRotation;
            updatingTabs = true;
            try
            {
                bool preview = NativePreviewActive();
                SculptScaleTabSelection selected = SculptScaleInputPolicy.TabSelection(active, NativeSculptActive(), preview);
                if (sculptTab != null && sculptTab.group != null) sculptTab.group.allowSwitchOff = true;
                tabStrip.SetActive(show);
                if (show && sculptTab != null && scaleTab != null)
                {
                    bool sculptSelected = selected == SculptScaleTabSelection.Sculpt;
                    bool scaleSelected = selected == SculptScaleTabSelection.Scale;
                    sculptTab.isOn = sculptSelected;
                    scaleTab.isOn = scaleSelected;
                    sculptTab.interactable = scaleTab.interactable = !preview;
                    sculptTab.group.allowSwitchOff = !sculptSelected && !scaleSelected;
                }
            }
            finally { updatingTabs = false; }
            nativeTabVisibility.alpha = show ? 0f : originalAlpha;
            nativeTabVisibility.blocksRaycasts = show ? false : originalBlocksRaycasts;
            nativeTabVisibility.interactable = show ? false : originalInteractable;
            tabHidden = show;
        }

        private void RefreshTabLanguage()
        {
            if (nativeTabText == null || sculptTabText == null || scaleTabText == null) return;
            bool japanese = HelpDefs.isJ;
            if (displayedJapanese != japanese)
            {
                sculptTabText.text = ScaleLocalization.Display(ScaleLocalization.Source("Sculpt", "スカルプト"), japanese);
                scaleTabText.text = ScaleLocalization.Display(ScaleLocalization.Source("Scale", "スケール"), japanese);
                displayedJapanese = japanese;
            }
            if (displayedTabFont != nativeTabText.font)
            {
                sculptTabText.font = scaleTabText.font = nativeTabText.font;
                sculptTabText.material = scaleTabText.material = nativeTabText.material;
                displayedTabFont = nativeTabText.font;
            }
        }

        private void OnDisable() { if (active) LeaveScale("disabled", false); }

        private void OnDestroy()
        {
            if (active) LeaveScale("destroy", false);
            if (tabHidden) ShowNativeTabs(false);
            if (tabStrip != null) Destroy(tabStrip);
            if (nativePanel != null) nativePanel.Dispose();
            if (addedVisibility && nativeTabVisibility != null) Destroy(nativeTabVisibility);
            if (current == this) current = null;
        }

        private static void Log(string message)
        {
            try { ScaleRuntimeHost.Log("[SCULPT-SCALE-UI] " + message); }
            catch (Exception) { }
        }
    }
}
