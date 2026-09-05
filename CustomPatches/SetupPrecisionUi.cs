using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using HarmonyLib;
using MPatchrMain;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MPatcherFork.CustomPatches
{
    internal static class SetupPrecisionUi
    {
        private const int DigitCount = 6;
        private const string NormalizedBackgroundPngBase64 = "iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAYAAABzenr0AAACYUlEQVR42u2XMUhbURSGP+95eaHpIx3MIgGhEOnUkipFHCySya04iEs3oVAoQVuHDh272kGLhZK1gwhKN2ltEZzEQgNCQ3mbkMVJSEjQd+99XV4kpmkx4oNS8sOBx33n3POfc++5nDMAMDo6SgupVAprLY7jYK0FwFqLUope0G6jlEJrjVKKRqNxQa+3XWOAao++A3eAZeAboIGwR9GR7XK0V1c4f1h/rrV+XSwWb+TzeUZGRq5yBOL7/li5XB5bWVl5CrwC3nTqDXRkwE2lUh9nZmam5+bmyGaz15LmarXK+vo6W1tb241G4xFw1vonuVwO13VxXRcRWZ6dnX28uLhIOp2+tnNOp9NMTExQq9Vyvu/fVEp9chwHx3EYmJycbOndNcZ8393dlUQiEcuFOz09pVAomDAM7wE/AJQxhkjmFxYWYnMOkEwmKRaLIiLzIoKIXCjD6fHx8djLLvIxfV4Fbbf79tDQUOwEMpkM1tpstzJ040x/C57noZS69e+8hH0CfQJ9An0CfQJt32dBEMTuMOqK690IHB0fH8dOIPJx1I3A9v7+fuwEDg4OsNZ+ttZirf29Jdvb25NeO+DLIggCpqamjIjcBw4BVBiGRHKolHq3uroaW/Rra2sAb40xh61WUDKZDEEQtOSL7/sPTk5OcsPDw9fWGVerVUqlEpubm9vNZnNea2201mitkY42zIjIh0qlUtvY2HjoeV4imUwyODh4Jce+77Ozs8PS0lKzUqm8DMPwmdba/G0wwfO889HMWvsEKAD5KwZfBr4qpd4DPwHq9fqlRjMigxf//Uv4C5YRAB8nZ1itAAAAAElFTkSuQmCC";
        private const float CellHeight = 20f;
        private const float DigitWidth = 0.15f;
        private const int DigitFontSize = 14;
        private const int SignFontSize = 17;
        private const float PanelHorizontalExpansion = 8f;
        private const float PanelVerticalOffset = -7f;
        private const float HybridFractionWidth = 82f;
        private const float HybridGap = 4f;
        private const float HybridSliderExtraWidth = 10f;
        private const float HybridSliderVerticalOffset = PanelVerticalOffset;
        private const float HybridFractionInset = 8f;
        private const float InputFieldHeight = 24f;
        private const float VanillaTextInset = 2f;
        private const int VanillaTextMinFontSize = 8;
        private const int InputTextMinFontSize = 7;
        private const int InputFontVisualOffset = 1;
        private const float ArrowWidth = 14f;
        private const float ArrowHeight = 8f;
        private const float ArrowGap = 3f;
        private const float ArrowCenter = CellHeight * 0.5f + ArrowGap + ArrowHeight * 0.5f;
        private static readonly int[] PlaceScales = { 100000, 10000, 1000, 100, 10, 1 };
        private static readonly Color CellColor = new Color(154f / 255f, 154f / 255f, 154f / 255f, 1f);
        private static readonly Color HoverColor = new Color(245f / 255f, 245f / 255f, 245f / 255f, 1f);
        private static readonly Color SignOutlineColor = Color.white;
        private static Texture2D normalizedBackgroundTexture;
        private static Sprite normalizedBackgroundSprite;

        private sealed class Digit
        {
            internal RectTransform Root;
            internal Image Background;
            internal Text Value;
            internal Image Up;
            internal Image Down;
            internal Button UpButton;
            internal Button DownButton;
        }

        private sealed class Row
        {
            internal Build Build;
            internal string Group;
            internal int Slot;
            internal Slider Slider;
            internal SliderController Controller;
            internal Text NativeText;
            internal GameObject Panel;
            internal Text Sign;
            internal Text Separator;
            internal Text Special;
            internal Digit[] Digits = new Digit[DigitCount];
            internal GameObject HybridPanel;
            internal Text HybridSeparator;
            internal Digit[] FractionDigits = new Digit[3];
            internal GameObject InputRoot;
            internal InputField Input;
            internal InputSelectionDriver InputSelection;
            internal bool Editing;
            internal bool CancelEdit;
            internal bool FormattingInput;
            internal bool ResetFractionOnSliderCallback;
            internal int SliderPrecisionHeldDirection;
            internal float SliderPrecisionNextRepeat;
            internal int InputFontCeiling;
            internal GameObject[] NativeSliderParts;
            internal bool[] NativeSliderPartStates;
            internal GameObject NativeArrowRoot;
            internal RectTransform NativeArrowRect;
            internal RectTransform NativeIncreaseRect;
            internal RectTransform NativeDecreaseRect;
            internal Vector2 NativeArrowPosition;
            internal Vector2 NativeIncreasePosition;
            internal Vector2 NativeDecreasePosition;
            internal float NativeArrowButtonWidth;
            internal Image NativeSliderBackgroundImage;
            internal Image NativeIncreaseImage;
            internal Image NativeDecreaseImage;
            internal Button NativeIncreaseButton;
            internal Button NativeDecreaseButton;
            internal bool WholeNumbers;
            internal bool NativeSliderEnabled;
            internal bool NativeSliderInteractable;
            internal bool NativeTextEnabled;
            internal Vector2 NativeTextSize;
            internal bool NativeTextBestFit;
            internal int NativeTextMinSize;
            internal int NativeTextMaxSize;
            internal HorizontalWrapMode NativeTextHorizontalOverflow;
            internal VerticalWrapMode NativeTextVerticalOverflow;
            internal float VanillaTextWidth;
            internal float NativeMinimum;
            internal Vector2 SliderAnchorMin;
            internal Vector2 SliderAnchorMax;
            internal Vector2 SliderPivot;
            internal Vector2 SliderSize;
            internal Vector2 SliderPosition;
            internal float SliderVisualWidth;
            internal float SliderVisualHeight;
            internal int Mode = -1;
            internal bool Enabled;
            internal bool Interactable;
        }

        private sealed class DigitHit : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IScrollHandler
        {
            internal Row Row;
            internal int Place;
            internal int Direction;

            public void OnPointerEnter(PointerEventData pointer) { SetHover(Row, Place); }

            public void OnPointerExit(PointerEventData pointer)
            {
                Digit digit = ActiveDigit(Row, Place);
                if (digit == null) return;
                if (!RectTransformUtility.RectangleContainsScreenPoint(digit.Root, pointer.position, pointer.pressEventCamera))
                    ClearHover(Row, Place);
            }

            public void OnPointerClick(PointerEventData pointer)
            {
                if (pointer.button != PointerEventData.InputButton.Left || Direction == 0) return;
                Nudge(Row, Place, Direction, Direction > 0 ? "arrow-up" : "arrow-down", false);
                pointer.Use();
            }

            public void OnScroll(PointerEventData pointer)
            {
                if (pointer.scrollDelta.y == 0 || !MouseWheelEnabled()) return;
                Nudge(Row, Place, pointer.scrollDelta.y > 0 ? 1 : -1, "wheel", true);
                pointer.Use();
            }

            private void OnDisable() { ClearHover(Row, Place); }
        }

        private sealed class SliderDoubleClick : MonoBehaviour, IPointerClickHandler
        {
            internal Row Row;

            public void OnPointerClick(PointerEventData pointer)
            {
                if (pointer.button != PointerEventData.InputButton.Left || pointer.clickCount < 2
                    || Row == null || !SupportsDirectInput(Row.Mode)) return;
                BeginEdit(Row);
                pointer.Use();
            }
        }

        private sealed class InputSelectionDriver : MonoBehaviour
        {
            internal Row Row;
            private int anchor;
            private int focus;
            private int frames;

            internal void Schedule(int selectionAnchor, int selectionFocus)
            {
                anchor = selectionAnchor;
                focus = selectionFocus;
                frames = 2;
                enabled = true;
            }

            internal void Cancel()
            {
                frames = 0;
                enabled = false;
            }

            private void LateUpdate()
            {
                if (Row == null || !Row.Editing || Row.Input == null)
                {
                    Cancel();
                    return;
                }
                if (--frames > 0) return;
                int length = Row.Input.text == null ? 0 : Row.Input.text.Length;
                int selectionAnchor = Mathf.Clamp(anchor, 0, length);
                int selectionFocus = Mathf.Clamp(focus, selectionAnchor, length);
                Row.Input.selectionAnchorPosition = selectionAnchor;
                Row.Input.selectionFocusPosition = selectionFocus;
                Row.Input.ForceLabelUpdate();
                SetupPrecision.Log("INPUT_FRACTION_SELECTION_APPLIED group=" + Row.Group + " slot=" + Row.Slot
                    + " range=" + selectionAnchor + ".." + selectionFocus + " phase=post-activation");
                Cancel();
            }
        }

        private sealed class DigitDriver : MonoBehaviour
        {
            internal Row Row;
            private int heldDirection;
            private float nextRepeat;

            private void Update()
            {
                if (Row != null && hoveredRow == Row && !PointerInside(Row, hoveredPlace))
                    ClearHover(Row, hoveredPlace);
                if (Row == null || hoveredRow != Row || !Row.Enabled || !Row.Interactable)
                {
                    heldDirection = 0;
                    return;
                }
                bool center = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
                if (center)
                {
                    heldDirection = 0;
                    if (Input.GetKeyDown(KeyCode.S))
                        SetWholeValue(Row, hoveredPlace, true, "key-S");
                    else if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
                        SetWholeValue(Row, hoveredPlace, true, "key-Shift");
                    return;
                }
                if (Input.GetKey(KeyCode.W))
                {
                    heldDirection = 0;
                    if (Input.GetKeyDown(KeyCode.W)) SetWholeValue(Row, hoveredPlace, false, "key-W");
                    return;
                }
                bool decrease = Input.GetKey(KeyCode.A);
                bool increase = Input.GetKey(KeyCode.D);
                int direction = decrease == increase ? 0 : decrease ? -1 : 1;
                if (direction == 0)
                {
                    heldDirection = 0;
                    return;
                }
                float now = Time.unscaledTime;
                bool first = direction != heldDirection || direction < 0 && Input.GetKeyDown(KeyCode.A)
                    || direction > 0 && Input.GetKeyDown(KeyCode.D);
                if (first || now >= nextRepeat)
                {
                    Nudge(Row, hoveredPlace, direction, direction > 0 ? "key-D" : "key-A", true);
                    nextRepeat = now + (first ? 0.35f : 0.08f);
                }
                heldDirection = direction;
            }

            private void OnDisable() { heldDirection = 0; }
        }

        private static readonly Dictionary<SliderController, Row> Rows = new Dictionary<SliderController, Row>();
        private static readonly FieldInfo Label = AccessTools.Field(typeof(SliderController), "JJJCBNPHIFL");
        private static readonly FieldInfo PreviousLabel = AccessTools.Field(typeof(SliderController), "IPIKOIIFJJK");
        private static readonly FieldInfo Selected = AccessTools.Field(typeof(Build), "LBBOFMGMMFF");
        private static readonly FieldInfo Assembly = AccessTools.Field(typeof(Build), "FFJDGJFPLAD");
        private static readonly FieldInfo Changed = AccessTools.Field(typeof(Build), "BHCOKCDPDNB");
        private static readonly FieldInfo Initializing = AccessTools.Field(typeof(Build), "LIOOKHCGPIO");
        private static readonly FieldInfo Updating = AccessTools.Field(typeof(Build), "OIAOGDEPDCO");
        private static readonly MethodInfo Undo = AccessTools.Method(typeof(Build), "ANDINIMKBLL");
        private static readonly MethodInfo Rebuild = AccessTools.Method(typeof(Build), "ALGEALFIOMP");
        private static readonly Type GameSettingsOwner = typeof(Build).Assembly.GetType("JKGKJLLFMLE", false);
        private static readonly FieldInfo GameSettings = GameSettingsOwner == null ? null : AccessTools.Field(GameSettingsOwner, "IGOBPLOLHEP");
        private static readonly FieldInfo WheelSlider = GameSettings == null ? null : AccessTools.Field(GameSettings.FieldType, "isWheelSlider");
        private static readonly MethodInfo MappedKeyDown = AccessTools.Method(typeof(HOCGCCAIPFF), "FGCCNKAIKAI",
            new Type[] { typeof(SystemData.EHLMFKOOHLI) });
        private static Build owner;
        private static int suppress;
        private static string lastFailure;
        private static Row hoveredRow;
        private static int hoveredPlace = -1;

        internal static void Register(Harmony patcher)
        {
            Stopwatch startup = Stopwatch.StartNew();
            long checkpoint = 0;
            if (Selected == null || Assembly == null || Changed == null || Initializing == null || Updating == null || Undo == null || Rebuild == null
                || Label == null || PreviousLabel == null || GameSettings == null || WheelSlider == null || MappedKeyDown == null)
                throw new MissingMemberException("SETUP digit editor members");
            if (!StartupHarmonyBatch.Registered)
                patcher.Patch(AccessTools.Method(typeof(Build), "Update"), null,
                    new HarmonyMethod(typeof(SetupPrecisionUi), "UpdatePostfix"),
                    new HarmonyMethod(typeof(SetupPrecisionUi), "BuildUpdateTranspiler"), null);
            LogStartupTiming(startup, StartupHarmonyBatch.Registered ? "Build.Update.shared-reuse" : "Build.Update", ref checkpoint);
            patcher.Patch(AccessTools.Method(typeof(Build), "OPMMCNOHEMC"), new HarmonyMethod(typeof(SetupPrecisionUi), "PopulatePrefix"),
                null, null, new HarmonyMethod(typeof(SetupPrecisionUi), "PopulateFinalizer"));
            LogStartupTiming(startup, "Build.OPMMCNOHEMC", ref checkpoint);
            HarmonyMethod slider = new HarmonyMethod(typeof(SetupPrecisionUi), "BuildSliderPrefix");
            slider.priority = Priority.First;
            patcher.Patch(AccessTools.Method(typeof(Build), "BKACIJDGAPP"), slider, null, null, null);
            LogStartupTiming(startup, "Build.BKACIJDGAPP", ref checkpoint);
            patcher.Patch(AccessTools.Method(typeof(SliderController), "FAPHGEMKPGN"),
                new HarmonyMethod(typeof(SetupPrecisionUi), "SliderLabelPrefix"), null, null, null);
            LogStartupTiming(startup, "SliderController.FAPHGEMKPGN", ref checkpoint);
            MethodInfo sliderUpdate = AccessTools.Method(typeof(SliderController), "Update");
            if (sliderUpdate == null) throw new MissingMemberException("SETUP slider update member");
            patcher.Patch(sliderUpdate, new HarmonyMethod(typeof(SetupPrecisionUi), "SliderUpdatePrefix"),
                new HarmonyMethod(typeof(SetupPrecisionUi), "SliderUpdatePostfix"), null, null);
            LogStartupTiming(startup, "SliderController.Update", ref checkpoint);
        }

        private static void LogStartupTiming(Stopwatch timer, string target, ref long checkpoint)
        {
            long elapsed = timer.ElapsedMilliseconds;
            SetupPrecision.Log("STARTUP_TIMING ui=" + target + " stepMs=" + (elapsed - checkpoint) + " totalMs=" + elapsed);
            checkpoint = elapsed;
        }

        private static IEnumerable<CodeInstruction> BuildUpdateTranspiler(IEnumerable<CodeInstruction> instructions)
        {
            MethodInfo replacement = AccessTools.Method(typeof(SetupPrecisionUi), "BuildMappedKeyDown");
            int replaced = 0;
            foreach (CodeInstruction instruction in instructions)
            {
                if (IsMappedKeyDown(instruction.operand))
                {
                    instruction.operand = replacement;
                    replaced++;
                }
                yield return instruction;
            }
            if (replaced == 0) throw new InvalidOperationException("Build.Update mapped-key calls changed.");
            SetupPrecision.Log("INPUT_GUARD buildMappedCalls=" + replaced + " scope=direct-input/editor-modes");
        }

        internal static bool IsMappedKeyDown(object operand)
        {
            string unresolved = operand as string;
            if (unresolved != null)
                return unresolved.Contains("HOCGCCAIPFF::FGCCNKAIKAI") && unresolved.Contains("EHLMFKOOHLI");
            MethodInfo method = operand as MethodInfo;
            if (method == null || method.Name != MappedKeyDown.Name || method.DeclaringType == null
                || method.DeclaringType.FullName != typeof(HOCGCCAIPFF).FullName) return false;
            ParameterInfo[] parameters = method.GetParameters();
            return parameters.Length == 1 && parameters[0].ParameterType.FullName == typeof(SystemData.EHLMFKOOHLI).FullName;
        }

        private static bool BuildMappedKeyDown(SystemData.EHLMFKOOHLI action)
        {
            bool pressed = HOCGCCAIPFF.FGCCNKAIKAI(action);
            bool filtered = FilterBuildModeHotkey(action, pressed, DirectInputEditing());
            if (pressed && !filtered) SetupPrecision.Log("INPUT_HOTKEY_BLOCKED action=" + action);
            return filtered;
        }

        internal static bool FilterBuildModeHotkey(SystemData.EHLMFKOOHLI action, bool pressed, bool editing)
        {
            if (!pressed || !editing) return pressed;
            return action != SystemData.EHLMFKOOHLI.Form
                && action != SystemData.EHLMFKOOHLI.Sculpt
                && action != SystemData.EHLMFKOOHLI.Paint
                && action != SystemData.EHLMFKOOHLI.Setup
                && action != SystemData.EHLMFKOOHLI.Group;
        }

        private static bool DirectInputEditing()
        {
            foreach (Row row in Rows.Values)
                if (row.Editing && row.InputRoot != null && row.InputRoot.activeInHierarchy) return true;
            return false;
        }

        internal static void OnSettingChanged()
        {
            ClearHover(null, -1);
            if (owner == null) return;
            try
            {
                Refresh(owner);
                lastFailure = null;
            }
            catch (Exception error) { Report(error); }
        }

        private static void PopulatePrefix(Build __instance)
        {
            SetupPrecisionData.Prune(Selected.GetValue(__instance) as BlockData);
            suppress++;
        }

        private static Exception PopulateFinalizer(Exception __exception)
        {
            suppress--;
            return __exception;
        }

        internal static void UpdatePostfix(Build __instance)
        {
            if (!SetupPrecision.IsRegistered || suppress != 0) return;
            try { Refresh(__instance); lastFailure = null; }
            catch (Exception error) { Report(error); }
        }

        private static string Group(BlockData block)
        {
            if (block == null) return null;
            switch (block.type)
            {
                case BlockData.AAHMDBHDCDK.BoxGen: return "ParamBox";
                case BlockData.AAHMDBHDCDK.CapGen: return "ParamCap";
                case BlockData.AAHMDBHDCDK.Coupler: return "ParamC";
                default: return SetupPrecisionData.IsMechanism(block) ? "Param" : null;
            }
        }

        private static void Refresh(Build build)
        {
            if (owner != build)
            {
                ClearHover(null, -1);
                Rows.Clear();
                owner = build;
            }
            BlockData block = Selected.GetValue(build) as BlockData;
            string group = SetupPrecision.Enabled ? Group(block) : null;
            suppress++;
            try
            {
                if (group != null)
                    for (int slot = 0; slot < 9; slot++)
                    {
                        if (!SetupPrecisionData.Supports(block, slot)) continue;
                        GameObject widget = build.GetSLD(group + slot);
                        if (widget == null) continue;
                        SliderController controller = widget.GetComponent<SliderController>();
                        if (controller == null || Rows.ContainsKey(controller)) continue;
                        Row row = CreateRow(build, group, slot, controller);
                        if (row != null) Rows.Add(controller, row);
                    }
                foreach (Row row in Rows.Values)
                {
                    if (row.Slider == null || row.Panel == null) continue;
                    bool enabled = row.Group == group && SetupPrecisionData.Supports(block, row.Slot)
                        && row.Slider.gameObject.activeInHierarchy;
                    SetEnabled(row, enabled);
                    if (!enabled) continue;
                    if (SetupPrecisionData.IsSize(block, row.Slot)) row.Slider.minValue = SetupPrecisionData.SizeMinimum(block, row.Slot);
                    bool interactable = row.Slider.IsInteractable()
                        && !(group == "ParamC" && row.Slot >= 3 && CouplerRotation.IsRotationReadOnly(block));
                    if (row.Mode != (int)SetupPrecisionEditorMode.DigitSpinner)
                        interactable = row.NativeSliderInteractable
                            && !(group == "ParamC" && row.Slot >= 3 && CouplerRotation.IsRotationReadOnly(block));
                    SetInteractable(row, interactable);
                    float value = SetupPrecisionData.Read(block, row.Slot);
                    if (!row.Editing)
                    {
                        row.Slider.value = SliderPosition(row, value);
                        row.NativeText.text = row.Mode == (int)SetupPrecisionEditorMode.SliderDigitSpinner
                            ? HybridDisplay(row, value) : Display(row, value);
                        PreviousLabel.SetValue(row.Controller, row.NativeText.text);
                    }
                    else row.NativeText.enabled = false;
                    SetDigits(row, value);
                    if (row.Panel.activeSelf) row.Panel.transform.SetAsLastSibling();
                    if (row.HybridPanel.activeSelf) row.HybridPanel.transform.SetAsLastSibling();
                    if (row.InputRoot.activeSelf) row.InputRoot.transform.SetAsLastSibling();
                }
            }
            finally { suppress--; }
        }

        private static Row CreateRow(Build build, string group, int slot, SliderController controller)
        {
            Slider slider = controller.GetComponent<Slider>();
            Text native = (Text)Label.GetValue(controller);
            RectTransform sliderRect = slider == null ? null : slider.transform as RectTransform;
            if (native == null || sliderRect == null) return null;
            Transform nativeBackground = sliderRect.Find("Background");
            Image nativeBackgroundImage = nativeBackground == null ? null : nativeBackground.GetComponent<Image>();
            Transform nativeArrow = sliderRect.parent == null ? null : sliderRect.parent.Find("ARW_" + group + slot);
            Transform increase = nativeArrow == null ? null : nativeArrow.Find("btn_inc");
            Transform decrease = nativeArrow == null ? null : nativeArrow.Find("btn_dec");
            RectTransform nativeArrowRect = nativeArrow as RectTransform;
            RectTransform increaseRect = increase as RectTransform;
            RectTransform decreaseRect = decrease as RectTransform;
            Image increaseImage = increase == null ? null : increase.GetComponent<Image>();
            Image decreaseImage = decrease == null ? null : decrease.GetComponent<Image>();
            Button increaseButton = increase == null ? null : increase.GetComponent<Button>();
            Button decreaseButton = decrease == null ? null : decrease.GetComponent<Button>();
            if (nativeBackgroundImage == null || nativeArrowRect == null || increaseRect == null || decreaseRect == null
                || increaseImage == null || decreaseImage == null
                || increaseButton == null || decreaseButton == null)
                throw new InvalidOperationException("Native SETUP slider visuals changed: " + group + slot);
            GameObject[] nativeParts = new GameObject[sliderRect.childCount];
            bool[] nativePartStates = new bool[nativeParts.Length];
            for (int child = 0; child < nativeParts.Length; child++)
            {
                nativeParts[child] = sliderRect.GetChild(child).gameObject;
                nativePartStates[child] = nativeParts[child].activeSelf;
            }
            Row row = new Row
            {
                Build = build,
                Group = group,
                Slot = slot,
                Slider = slider,
                Controller = controller,
                NativeText = native,
                NativeSliderParts = nativeParts,
                NativeSliderPartStates = nativePartStates,
                NativeArrowRoot = nativeArrow.gameObject,
                NativeArrowRect = nativeArrowRect,
                NativeIncreaseRect = increaseRect,
                NativeDecreaseRect = decreaseRect,
                NativeArrowPosition = nativeArrowRect.anchoredPosition,
                NativeIncreasePosition = increaseRect.anchoredPosition,
                NativeDecreasePosition = decreaseRect.anchoredPosition,
                NativeArrowButtonWidth = Mathf.Max(increaseRect.rect.width, decreaseRect.rect.width),
                NativeSliderBackgroundImage = nativeBackgroundImage,
                NativeIncreaseImage = increaseImage,
                NativeDecreaseImage = decreaseImage,
                NativeIncreaseButton = increaseButton,
                NativeDecreaseButton = decreaseButton,
                WholeNumbers = slider.wholeNumbers,
                NativeSliderEnabled = slider.enabled,
                NativeSliderInteractable = slider.interactable,
                NativeTextEnabled = native.enabled,
                NativeTextSize = native.rectTransform.sizeDelta,
                NativeTextBestFit = native.resizeTextForBestFit,
                NativeTextMinSize = native.resizeTextMinSize,
                NativeTextMaxSize = native.resizeTextMaxSize,
                NativeTextHorizontalOverflow = native.horizontalOverflow,
                NativeTextVerticalOverflow = native.verticalOverflow,
                VanillaTextWidth = VanillaTextWidth(native.rectTransform.parent as RectTransform),
                NativeMinimum = slider.minValue,
                SliderAnchorMin = sliderRect.anchorMin,
                SliderAnchorMax = sliderRect.anchorMax,
                SliderPivot = sliderRect.pivot,
                SliderSize = sliderRect.sizeDelta,
                SliderPosition = sliderRect.anchoredPosition,
                SliderVisualWidth = sliderRect.rect.width,
                SliderVisualHeight = sliderRect.rect.height,
                Interactable = true
            };
            GameObject panel = new GameObject("MP_SetupDigitSpinner", typeof(RectTransform));
            panel.layer = slider.gameObject.layer;
            RectTransform panelRect = panel.GetComponent<RectTransform>();
            panelRect.SetParent(sliderRect, false);
            panelRect.anchorMin = Vector2.zero;
            panelRect.anchorMax = Vector2.one;
            panelRect.offsetMin = new Vector2(-PanelHorizontalExpansion, PanelVerticalOffset);
            panelRect.offsetMax = new Vector2(PanelHorizontalExpansion, PanelVerticalOffset);
            row.Panel = panel;
            row.Sign = CreateText(panel.transform, "Sign", native, new Vector2(0f, 0.15f), new Vector2(0.055f, 0.85f), SignFontSize);
            row.Sign.fontStyle = FontStyle.Bold;
            row.Sign.fontSize = SignFontSize;
            row.Sign.resizeTextForBestFit = false;
            AddSignOutline(row.Sign);
            row.Separator = CreateText(panel.transform, "Separator", native, new Vector2(0.50f, 0.15f), new Vector2(0.54f, 0.85f), 14);
            row.Separator.text = ",";
            float[] starts = { 0.06f, 0.21f, 0.36f, 0.54f, 0.69f, 0.84f };
            for (int place = 0; place < DigitCount; place++)
                row.Digits[place] = CreateDigit(row, panel.transform, place, starts[place], starts[place] + DigitWidth, native);
            row.Special = CreateText(panel.transform, "Special", native, new Vector2(0.06f, 0.15f), new Vector2(0.99f, 0.85f), 12);
            row.Special.gameObject.SetActive(false);
            panel.AddComponent<DigitDriver>().Row = row;
            panel.SetActive(false);

            GameObject hybridPanel = new GameObject("MP_SetupFractionSpinner", typeof(RectTransform));
            hybridPanel.layer = slider.gameObject.layer;
            RectTransform hybridRect = hybridPanel.GetComponent<RectTransform>();
            hybridRect.SetParent(sliderRect.parent, false);
            hybridRect.anchorMin = sliderRect.anchorMin;
            hybridRect.anchorMax = sliderRect.anchorMax;
            hybridRect.pivot = sliderRect.pivot;
            hybridRect.sizeDelta = new Vector2(HybridFractionWidth, row.SliderVisualHeight);
            hybridRect.anchoredPosition = row.SliderPosition;
            row.HybridPanel = hybridPanel;
            row.HybridSeparator = CreateText(hybridPanel.transform, "Separator", native,
                new Vector2(0f, 0.15f), new Vector2(0.12f, 0.85f), 14);
            row.HybridSeparator.text = ",";
            float[] fractionStarts = { 0.13f, 0.42f, 0.71f };
            for (int index = 0; index < row.FractionDigits.Length; index++)
                row.FractionDigits[index] = CreateDigit(row, hybridPanel.transform, index + 3,
                    fractionStarts[index], fractionStarts[index] + 0.28f, native);
            hybridPanel.AddComponent<DigitDriver>().Row = row;
            hybridPanel.SetActive(false);

            RectTransform nativeTextRect = native.rectTransform;
            Transform inputParent = InputParent(native, sliderRect);
            GameObject inputRoot = RectObject(inputParent, "MP_SetupNumberInput", nativeTextRect.anchorMin, nativeTextRect.anchorMax);
            RectTransform inputRect = inputRoot.GetComponent<RectTransform>();
            inputRect.pivot = nativeTextRect.pivot;
            inputRect.anchoredPosition = nativeTextRect.anchoredPosition;
            inputRect.sizeDelta = new Vector2(row.VanillaTextWidth, Mathf.Max(InputFieldHeight, nativeTextRect.rect.height));
            inputRect.localRotation = nativeTextRect.localRotation;
            inputRect.localScale = nativeTextRect.localScale;
            Image inputHit = inputRoot.AddComponent<Image>();
            inputHit.color = Color.clear;
            InputField input = inputRoot.AddComponent<InputField>();
            Text inputText = CreateText(inputRoot.transform, "Value", native, Vector2.zero, Vector2.one, 18);
            inputText.rectTransform.offsetMin = new Vector2(1f, 0f);
            inputText.rectTransform.offsetMax = new Vector2(-1f, 0f);
            ConfigureInputText(inputText);
            input.targetGraphic = inputHit;
            input.textComponent = inputText;
            input.contentType = InputField.ContentType.Standard;
            input.lineType = InputField.LineType.SingleLine;
            input.characterLimit = 8;
            input.onValidateInput = delegate(string current, int insertion, char added)
            {
                return ValidateRowInputCharacter(row, current, insertion, added);
            };
            Navigation inputNavigation = input.navigation;
            inputNavigation.mode = Navigation.Mode.None;
            input.navigation = inputNavigation;
            row.InputRoot = inputRoot;
            row.Input = input;
            input.onValueChanged.AddListener(delegate(string value) { OnInputChanged(row, value); });
            input.onEndEdit.AddListener(delegate(string value) { EndEdit(row, value); });
            InputSelectionDriver inputSelection = inputRoot.AddComponent<InputSelectionDriver>();
            inputSelection.Row = row;
            inputSelection.enabled = false;
            row.InputSelection = inputSelection;
            inputRoot.SetActive(false);
            slider.gameObject.AddComponent<SliderDoubleClick>().Row = row;
            SetupPrecision.Log("UI_DIGITS group=" + group + " slot=" + slot
                + " widget=digit-spinner cells=000,000 cellSize=24x20 cellColor=154,154,154 cellSprite="
                + (nativeBackgroundImage.overrideSprite == null ? nativeBackgroundImage.sprite == null ? "null" : nativeBackgroundImage.sprite.name : nativeBackgroundImage.overrideSprite.name)
                + " cellImageType=" + nativeBackgroundImage.type
                + " cellTexture=normalized-214-to-255 hover=245,245,245/fade-" + slider.transition
                + " focus=pointer-only/root-raycast+bounds width=+16 offsetY=-7 idleTint=white-base/absolute"
                + " sign=17/white-outline arrows=native-sprite/14x8/gap3 keys=A/D,S/Shift=center,W=signed-negate/unsigned-mirror sizeHotkeys=logical-0..250 safeMin=0.001 floatBounds=normalized wheel=setting alt=x10 ctrl=.100 ctrl+alt=.001 ctrlScope=A/D+wheel/all-editors carry=enabled input=vanilla-only/double-click/handle-contained/fixed-3/auto-comma nativeUi=mode-dependent panel=transparent hybridFractionWidth=82"
                + " vanillaTextWidth=" + row.VanillaTextWidth.ToString("0.##", CultureInfo.InvariantCulture)
                + " nativeTextSize=" + row.NativeTextSize.x.ToString("0.##", CultureInfo.InvariantCulture) + "x"
                + row.NativeTextSize.y.ToString("0.##", CultureInfo.InvariantCulture)
                + " nativeFont=" + native.fontSize + " bestFitMin=" + VanillaTextMinFontSize
                + " inputFont=fixed-" + VanillaTextMinFontSize);
            SetupPrecision.Log("UI_LAYOUT group=" + group + " slot=" + slot + " rect="
                + sliderRect.rect.width.ToString("0.##", CultureInfo.InvariantCulture) + "x"
                + sliderRect.rect.height.ToString("0.##", CultureInfo.InvariantCulture)
                + " nativeArrowX=" + nativeArrowRect.anchoredPosition.x.ToString("0.##", CultureInfo.InvariantCulture)
                + " nativeIncX=" + increaseRect.anchoredPosition.x.ToString("0.##", CultureInfo.InvariantCulture)
                + " nativeDecX=" + decreaseRect.anchoredPosition.x.ToString("0.##", CultureInfo.InvariantCulture)
                + " nativeArrowButtonWidth=" + row.NativeArrowButtonWidth.ToString("0.##", CultureInfo.InvariantCulture)
                + " hybridSliderExtra=10 hybridSliderOffsetY=-7 hybridFractionInset=8");
            return row;
        }

        internal static Transform InputParent(Text native, RectTransform sliderRect)
        {
            return native != null && native.rectTransform != null && native.rectTransform.parent != null
                ? native.rectTransform.parent : sliderRect;
        }

        internal static float VanillaTextWidth(RectTransform handle)
        {
            return Mathf.Max(1f, (handle == null ? 48f : handle.rect.width) - VanillaTextInset * 2f);
        }

        private static Digit CreateDigit(Row row, Transform parent, int place, float left, float right, Text native)
        {
            GameObject root = RectObject(parent, "Digit" + place, new Vector2(left, 0f), new Vector2(right, 1f));
            Digit digit = new Digit { Root = root.GetComponent<RectTransform>() };
            digit.Root.offsetMin = Vector2.zero;
            digit.Root.offsetMax = Vector2.zero;
            Image hitArea = root.AddComponent<Image>();
            hitArea.color = Color.clear;
            hitArea.raycastTarget = true;
            AddHit(root, row, place, 0);
            GameObject body = RectObject(root.transform, "Value", new Vector2(0f, 0.5f), new Vector2(1f, 0.5f));
            RectTransform bodyRect = body.GetComponent<RectTransform>();
            bodyRect.offsetMin = new Vector2(1f, -CellHeight * 0.5f);
            bodyRect.offsetMax = new Vector2(-1f, CellHeight * 0.5f);
            digit.Background = body.AddComponent<Image>();
            Image nativeBackground = row.NativeSliderBackgroundImage;
            Sprite nativeSprite = nativeBackground.overrideSprite == null ? nativeBackground.sprite : nativeBackground.overrideSprite;
            digit.Background.sprite = CreateNormalizedCellSprite(nativeSprite);
            digit.Background.material = nativeBackground.material;
            digit.Background.type = nativeBackground.type;
            digit.Background.preserveAspect = nativeBackground.preserveAspect;
            digit.Background.fillCenter = nativeBackground.fillCenter;
            digit.Background.fillMethod = nativeBackground.fillMethod;
            digit.Background.fillAmount = nativeBackground.fillAmount;
            digit.Background.fillClockwise = nativeBackground.fillClockwise;
            digit.Background.fillOrigin = nativeBackground.fillOrigin;
            digit.Background.color = Color.white;
            digit.Background.canvasRenderer.SetColor(CellColor);
            digit.Background.raycastTarget = true;
            AddHit(body, row, place, 0);
            digit.Value = CreateText(body.transform, "Number", native, Vector2.zero, Vector2.one, DigitFontSize);
            digit.Value.fontStyle = FontStyle.Bold;
            digit.Value.fontSize = DigitFontSize;
            digit.Value.resizeTextMaxSize = DigitFontSize;
            digit.Up = CreateArrow(root.transform, "Up", row.NativeIncreaseImage, row.NativeIncreaseButton,
                ArrowCenter, out digit.UpButton);
            AddHit(digit.UpButton.gameObject, row, place, 1);
            digit.Down = CreateArrow(root.transform, "Down", row.NativeDecreaseImage, row.NativeDecreaseButton,
                -ArrowCenter, out digit.DownButton);
            AddHit(digit.DownButton.gameObject, row, place, -1);
            return digit;
        }

        private static Sprite CreateNormalizedCellSprite(Sprite nativeSprite)
        {
            if (normalizedBackgroundSprite != null) return normalizedBackgroundSprite;
            if (nativeSprite == null) throw new InvalidOperationException("Native slider Background sprite is missing.");
            byte[] png = Convert.FromBase64String(NormalizedBackgroundPngBase64);
            Texture2D texture = new Texture2D(2, 2, TextureFormat.ARGB32, false);
            if (!texture.LoadImage(png)) throw new InvalidOperationException("Normalized slider Background texture failed to load.");
            texture.name = "MP_SetupDigitBackground";
            texture.filterMode = nativeSprite.texture.filterMode;
            texture.wrapMode = nativeSprite.texture.wrapMode;
            texture.anisoLevel = nativeSprite.texture.anisoLevel;
            texture.hideFlags = HideFlags.HideAndDontSave;
            Rect rect = new Rect(0f, 0f, texture.width, texture.height);
            Rect nativeRect = nativeSprite.rect;
            Vector2 pivot = new Vector2(nativeSprite.pivot.x / nativeRect.width, nativeSprite.pivot.y / nativeRect.height);
            normalizedBackgroundTexture = texture;
            normalizedBackgroundSprite = Sprite.Create(texture, rect, pivot, nativeSprite.pixelsPerUnit,
                0u, SpriteMeshType.FullRect, nativeSprite.border);
            normalizedBackgroundSprite.name = "MP_SetupDigitBackground";
            normalizedBackgroundSprite.hideFlags = HideFlags.HideAndDontSave;
            return normalizedBackgroundSprite;
        }

        private static GameObject RectObject(Transform parent, string name, Vector2 minimum, Vector2 maximum)
        {
            GameObject gameObject = new GameObject(name, typeof(RectTransform));
            gameObject.layer = parent.gameObject.layer;
            RectTransform rect = gameObject.GetComponent<RectTransform>();
            rect.SetParent(parent, false);
            rect.anchorMin = minimum;
            rect.anchorMax = maximum;
            rect.offsetMin = new Vector2(1f, 1f);
            rect.offsetMax = new Vector2(-1f, -1f);
            return gameObject;
        }

        private static Text CreateText(Transform parent, string name, Text native, Vector2 minimum, Vector2 maximum, int size)
        {
            GameObject gameObject = RectObject(parent, name, minimum, maximum);
            Text text = gameObject.AddComponent<Text>();
            text.font = native.font;
            text.fontSize = Math.Min(native.fontSize, size);
            text.fontStyle = native.fontStyle;
            text.lineSpacing = native.lineSpacing;
            text.alignment = TextAnchor.MiddleCenter;
            text.color = native.color;
            text.supportRichText = false;
            text.raycastTarget = false;
            text.resizeTextForBestFit = true;
            text.resizeTextMinSize = 6;
            text.resizeTextMaxSize = Math.Min(native.fontSize, size);
            return text;
        }

        private static void ConfigureInputText(Text text)
        {
            text.resizeTextForBestFit = false;
            text.resizeTextMinSize = InputTextMinFontSize;
            text.resizeTextMaxSize = Math.Max(InputTextMinFontSize, text.fontSize);
            text.fontSize = InputTextMinFontSize;
            text.horizontalOverflow = HorizontalWrapMode.Overflow;
            text.verticalOverflow = VerticalWrapMode.Overflow;
        }

        private static int RenderedNativeFontSize(Row row)
        {
            if (row == null || row.NativeText == null) return VanillaTextMinFontSize;
            try
            {
                Canvas.ForceUpdateCanvases();
                int used = row.NativeText.cachedTextGenerator.fontSizeUsedForBestFit;
                if (used > 0) return Mathf.Clamp(used, VanillaTextMinFontSize,
                    Math.Max(VanillaTextMinFontSize, row.NativeText.fontSize));
            }
            catch (Exception error) { Report(error); }
            return VanillaTextMinFontSize;
        }

        private static float PreferredInputWidth(Text text, string value, int fontSize)
        {
            if (text == null || string.IsNullOrEmpty(value)) return 0f;
            TextGenerationSettings settings = text.GetGenerationSettings(Vector2.zero);
            settings.resizeTextForBestFit = false;
            settings.fontSize = fontSize;
            settings.horizontalOverflow = HorizontalWrapMode.Overflow;
            settings.verticalOverflow = VerticalWrapMode.Overflow;
            TextGenerator generator = new TextGenerator(value.Length);
            float pixelsPerUnit = Mathf.Max(0.0001f, text.pixelsPerUnit);
            return generator.GetPreferredWidth(value, settings) / pixelsPerUnit;
        }

        private static int MatchingInputFontSize(Row row, string value)
        {
            if (row == null || row.Input == null || row.Input.textComponent == null) return InputTextMinFontSize;
            Text text = row.Input.textComponent;
            int maximum = Mathf.Clamp(row.InputFontCeiling, InputTextMinFontSize,
                Math.Max(InputTextMinFontSize, row.NativeText == null ? text.fontSize : row.NativeText.fontSize));
            float available = Mathf.Max(1f, text.rectTransform.rect.width);
            try
            {
                for (int size = maximum; size >= InputTextMinFontSize; size--)
                {
                    float preferred = PreferredInputWidth(text, value, size);
                    if (!float.IsNaN(preferred) && !float.IsInfinity(preferred) && preferred <= available + 0.01f)
                        return size;
                }
            }
            catch (Exception error) { Report(error); }
            return InputTextMinFontSize;
        }

        private static int ApplyInputFont(Row row, string value)
        {
            int size = MatchingInputFontSize(row, value);
            if (row != null && row.Input != null && row.Input.textComponent != null)
                row.Input.textComponent.fontSize = size;
            return size;
        }

        private static void AddSignOutline(Text sign)
        {
            Outline outline = sign.gameObject.AddComponent<Outline>();
            outline.effectColor = SignOutlineColor;
            outline.effectDistance = new Vector2(1f, -1f);
            outline.useGraphicAlpha = true;
        }

        private static Image CreateArrow(Transform parent, string name, Image sourceImage, Button sourceButton,
            float verticalPosition, out Button button)
        {
            GameObject gameObject = RectObject(parent, name, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f));
            RectTransform buttonRect = gameObject.GetComponent<RectTransform>();
            buttonRect.sizeDelta = new Vector2(ArrowWidth, ArrowHeight);
            buttonRect.anchoredPosition = new Vector2(0f, verticalPosition);
            GameObject graphic = RectObject(gameObject.transform, "Graphic", new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f));
            RectTransform graphicRect = graphic.GetComponent<RectTransform>();
            graphicRect.sizeDelta = new Vector2(ArrowHeight, ArrowWidth);
            graphicRect.anchoredPosition = Vector2.zero;
            graphic.transform.localScale = sourceImage.transform.localScale;
            graphic.transform.localEulerAngles = new Vector3(0f, 0f, 90f);
            Image image = graphic.AddComponent<Image>();
            image.sprite = sourceImage.overrideSprite == null ? sourceImage.sprite : sourceImage.overrideSprite;
            image.color = sourceImage.color;
            image.material = sourceImage.material;
            image.type = sourceImage.type;
            image.preserveAspect = sourceImage.preserveAspect;
            image.fillCenter = sourceImage.fillCenter;
            image.fillMethod = sourceImage.fillMethod;
            image.fillAmount = sourceImage.fillAmount;
            image.fillClockwise = sourceImage.fillClockwise;
            image.fillOrigin = sourceImage.fillOrigin;
            image.raycastTarget = true;
            button = gameObject.AddComponent<Button>();
            button.targetGraphic = image;
            button.transition = sourceButton.transition;
            button.colors = sourceButton.colors;
            button.spriteState = sourceButton.spriteState;
            button.animationTriggers = sourceButton.animationTriggers;
            Navigation navigation = button.navigation;
            navigation.mode = Navigation.Mode.None;
            button.navigation = navigation;
            return image;
        }

        private static void AddHit(GameObject gameObject, Row row, int place, int direction)
        {
            DigitHit hit = gameObject.AddComponent<DigitHit>();
            hit.Row = row;
            hit.Place = place;
            hit.Direction = direction;
        }

        private static void SetEnabled(Row row, bool enabled)
        {
            // SceneMan.IPKGDOPOHBJ owns the current row visibility and toggles
            // the slider plus its sibling arrow root together. ParamN widgets
            // are reused between selected blocks, so a value captured once in
            // CreateRow becomes stale when the next block disables that slot.
            // This patch never hides the slider GameObject itself, which makes
            // its current activeSelf the native source of truth for the arrows.
            bool nativeRowVisible = row.Slider.gameObject.activeSelf;
            int mode = enabled ? (int)SetupPrecision.EditorMode : -1;
            bool changed = row.Enabled != enabled || row.Mode != mode;
            if (changed) CancelInput(row);
            row.Enabled = enabled;
            row.Mode = mode;
            RestoreSliderLayout(row);
            RestoreNativeArrowLayout(row);
            RestoreNativeTextLayout(row);
            bool showDigits = enabled && mode == (int)SetupPrecisionEditorMode.DigitSpinner;
            bool showHybrid = enabled && mode == (int)SetupPrecisionEditorMode.SliderDigitSpinner;
            SetNativeParts(row, !showDigits);
            if (!enabled)
            {
                if (hoveredRow == row) ClearHover(row, -1);
                row.NativeText.enabled = row.NativeTextEnabled;
                row.Slider.enabled = row.NativeSliderEnabled;
                row.Slider.interactable = row.NativeSliderInteractable;
                row.Slider.wholeNumbers = row.WholeNumbers;
                row.Slider.minValue = row.NativeMinimum;
            }
            else if (mode == (int)SetupPrecisionEditorMode.DigitSpinner)
            {
                if (row.NativeArrowRoot != null) row.NativeArrowRoot.SetActive(false);
                row.NativeText.enabled = false;
                row.Slider.enabled = false;
                row.Slider.interactable = row.NativeSliderInteractable;
                row.Slider.wholeNumbers = false;
            }
            else
            {
                row.NativeText.enabled = row.NativeTextEnabled;
                row.Slider.enabled = row.NativeSliderEnabled;
                row.Slider.interactable = row.NativeSliderInteractable && row.Interactable;
                row.Slider.wholeNumbers = true;
                if (mode == (int)SetupPrecisionEditorMode.SliderDigitSpinner)
                {
                    ApplyHybridLayout(row);
                }
                else ApplyVanillaTextLayout(row);
            }
            if (row.NativeArrowRoot != null)
                row.NativeArrowRoot.SetActive(NativeArrowVisible(nativeRowVisible, enabled, mode));
            bool panelChanged = SetActiveIfNeeded(row.Panel, showDigits);
            bool hybridChanged = SetActiveIfNeeded(row.HybridPanel, showHybrid);
            if (changed || panelChanged || hybridChanged) Paint(row, true);
        }

        internal static bool NativeArrowVisible(bool nativeRowVisible, bool enabled, int mode)
        {
            return nativeRowVisible && (!enabled || mode != (int)SetupPrecisionEditorMode.DigitSpinner);
        }

        private static bool SetActiveIfNeeded(GameObject gameObject, bool active)
        {
            if (gameObject == null || gameObject.activeSelf == active) return false;
            gameObject.SetActive(active);
            return true;
        }

        private static void SetNativeParts(Row row, bool visible)
        {
            for (int part = 0; part < row.NativeSliderParts.Length; part++)
                if (row.NativeSliderParts[part] != null)
                    row.NativeSliderParts[part].SetActive(visible && row.NativeSliderPartStates[part]);
        }

        private static void RestoreSliderLayout(Row row)
        {
            RectTransform rect = row.Slider.transform as RectTransform;
            if (rect == null) return;
            rect.anchorMin = row.SliderAnchorMin;
            rect.anchorMax = row.SliderAnchorMax;
            rect.pivot = row.SliderPivot;
            rect.sizeDelta = row.SliderSize;
            rect.anchoredPosition = row.SliderPosition;
        }

        private static void RestoreNativeArrowLayout(Row row)
        {
            if (row.NativeArrowRect != null) row.NativeArrowRect.anchoredPosition = row.NativeArrowPosition;
            if (row.NativeIncreaseRect != null) row.NativeIncreaseRect.anchoredPosition = row.NativeIncreasePosition;
            if (row.NativeDecreaseRect != null) row.NativeDecreaseRect.anchoredPosition = row.NativeDecreasePosition;
        }

        private static void RestoreNativeTextLayout(Row row)
        {
            if (row == null || row.NativeText == null) return;
            row.NativeText.rectTransform.sizeDelta = row.NativeTextSize;
            row.NativeText.resizeTextForBestFit = row.NativeTextBestFit;
            row.NativeText.resizeTextMinSize = row.NativeTextMinSize;
            row.NativeText.resizeTextMaxSize = row.NativeTextMaxSize;
            row.NativeText.horizontalOverflow = row.NativeTextHorizontalOverflow;
            row.NativeText.verticalOverflow = row.NativeTextVerticalOverflow;
        }

        private static void ApplyVanillaTextLayout(Row row)
        {
            if (row == null || row.NativeText == null) return;
            row.NativeText.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, row.VanillaTextWidth);
            row.NativeText.resizeTextForBestFit = true;
            row.NativeText.resizeTextMinSize = VanillaTextMinFontSize;
            row.NativeText.resizeTextMaxSize = Math.Max(VanillaTextMinFontSize, row.NativeText.fontSize);
            row.NativeText.horizontalOverflow = HorizontalWrapMode.Wrap;
            row.NativeText.verticalOverflow = VerticalWrapMode.Truncate;
        }

        private static void ApplyHybridLayout(Row row)
        {
            RectTransform sliderRect = row.Slider.transform as RectTransform;
            RectTransform fractionRect = row.HybridPanel.transform as RectTransform;
            if (sliderRect == null || fractionRect == null) return;
            float sliderWidth = HybridSliderWidth(row.SliderVisualWidth);
            float sliderOffset = -(HybridFractionWidth + HybridGap) * 0.5f;
            sliderRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, sliderWidth);
            sliderRect.anchoredPosition = row.SliderPosition + new Vector2(sliderOffset, HybridSliderVerticalOffset);
            if (row.NativeArrowRect != null)
                row.NativeArrowRect.anchoredPosition = row.NativeArrowPosition + new Vector2(sliderOffset, HybridSliderVerticalOffset);
            float halfWidthChange = HybridArrowOffset(sliderWidth, row.SliderVisualWidth);
            if (row.NativeIncreaseRect != null)
                row.NativeIncreaseRect.anchoredPosition = row.NativeIncreasePosition + new Vector2(halfWidthChange, 0f);
            if (row.NativeDecreaseRect != null)
                row.NativeDecreaseRect.anchoredPosition = row.NativeDecreasePosition - new Vector2(halfWidthChange, 0f);
            fractionRect.anchorMin = row.SliderAnchorMin;
            fractionRect.anchorMax = row.SliderAnchorMax;
            fractionRect.pivot = row.SliderPivot;
            fractionRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, HybridFractionWidth);
            fractionRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, row.SliderVisualHeight);
            fractionRect.anchoredPosition = row.SliderPosition
                + new Vector2(HybridFractionOffset(sliderWidth, row.NativeArrowButtonWidth), PanelVerticalOffset);
        }

        internal static float HybridSliderWidth(float originalWidth)
        {
            float totalWidth = originalWidth + PanelHorizontalExpansion * 2f;
            return Mathf.Max(60f, totalWidth - HybridGap - HybridFractionWidth + HybridSliderExtraWidth);
        }

        internal static float HybridArrowOffset(float sliderWidth, float originalWidth)
        {
            return (sliderWidth - originalWidth) * 0.5f;
        }

        internal static float HybridFractionOffset(float sliderWidth, float arrowButtonWidth)
        {
            return (sliderWidth + HybridGap) * 0.5f + arrowButtonWidth - HybridFractionInset;
        }

        private static void SetInteractable(Row row, bool interactable)
        {
            if (row.Interactable == interactable)
            {
                if (row.Input != null) row.Input.interactable = interactable;
                return;
            }
            row.Interactable = interactable;
            if (row.Input != null) row.Input.interactable = interactable;
            if (row.Enabled && row.Mode != (int)SetupPrecisionEditorMode.DigitSpinner)
                row.Slider.interactable = row.NativeSliderInteractable && interactable;
            Paint(row);
        }

        private static void SetHover(Row row, int place)
        {
            if (row == null || !row.Enabled || ActiveDigit(row, place) == null) return;
            Row previous = hoveredRow;
            hoveredRow = row;
            hoveredPlace = place;
            if (previous != null && previous != row) Paint(previous);
            Paint(row);
        }

        private static void ClearHover(Row row, int place)
        {
            if (row != null && (hoveredRow != row || place >= 0 && hoveredPlace != place)) return;
            Row previous = hoveredRow;
            hoveredRow = null;
            hoveredPlace = -1;
            if (previous != null) Paint(previous);
        }

        private static bool PointerInside(Row row, int place)
        {
            Digit digit = ActiveDigit(row, place);
            if (digit == null || digit.Root == null) return false;
            Canvas canvas = digit.Root.GetComponentInParent<Canvas>();
            Camera camera = canvas != null && canvas.renderMode != RenderMode.ScreenSpaceOverlay ? canvas.worldCamera : null;
            Vector2 position = Input.mousePosition;
            return RectTransformUtility.RectangleContainsScreenPoint(digit.Root, position, camera)
                || digit.Up != null && RectTransformUtility.RectangleContainsScreenPoint(digit.Up.rectTransform, position, camera)
                || digit.Down != null && RectTransformUtility.RectangleContainsScreenPoint(digit.Down.rectTransform, position, camera);
        }

        private static bool SliderPointerInside(Row row)
        {
            RectTransform rect = row == null || row.Slider == null ? null : row.Slider.transform as RectTransform;
            if (rect == null) return false;
            Canvas canvas = rect.GetComponentInParent<Canvas>();
            Camera camera = canvas != null && canvas.renderMode != RenderMode.ScreenSpaceOverlay ? canvas.worldCamera : null;
            return RectTransformUtility.RectangleContainsScreenPoint(rect, Input.mousePosition, camera);
        }

        private static void Paint(Row row, bool instant = false)
        {
            if (row == null) return;
            ColorBlock colors = row.Slider.colors;
            PaintDigits(row, row.Digits, 0, colors, instant);
            PaintDigits(row, row.FractionDigits, 3, colors, instant);
        }

        private static void PaintDigits(Row row, Digit[] digits, int firstPlace, ColorBlock colors, bool instant)
        {
            if (digits == null) return;
            for (int index = 0; index < digits.Length; index++)
            {
                Digit digit = digits[index];
                if (digit == null) continue;
                bool hover = row.Interactable && hoveredRow == row && hoveredPlace == index + firstPlace
                    && ActiveDigit(row, index + firstPlace) == digit;
                Color target = hover ? HoverColor : CellColor;
                digit.Background.color = Color.white;
                if (instant)
                {
                    digit.Background.canvasRenderer.SetColor(target);
                }
                else digit.Background.CrossFadeColor(target, colors.fadeDuration, true, true);
                digit.UpButton.interactable = row.Interactable;
                digit.DownButton.interactable = row.Interactable;
            }
        }

        private static Digit ActiveDigit(Row row, int place)
        {
            if (row == null || place < 0 || place >= DigitCount) return null;
            if (row.Mode == (int)SetupPrecisionEditorMode.DigitSpinner)
                return row.Digits == null ? null : row.Digits[place];
            if (row.Mode == (int)SetupPrecisionEditorMode.SliderDigitSpinner && place >= 3)
                return row.FractionDigits == null ? null : row.FractionDigits[place - 3];
            return null;
        }

        private static string Display(Row row, float value)
        {
            if (value == row.Slider.minValue && row.Controller.GJDAJCALLPF == -12345f)
                return row.Controller.FAEJCNDLBLN == 1 ? "STOP" : "FREE";
            if (value == row.Slider.maxValue && row.Controller.PPFPKBJFNEH == 12345f)
                return row.Controller.KMIHHDIOPIA == 0 ? "X" : row.Controller.KMIHHDIOPIA == 1 ? "MAX" : "STOP";
            return SetupPrecisionData.Format(value).Replace('.', ',');
        }

        private static string HybridDisplay(Row row, float value)
        {
            if (IsSpecial(row, value)) return Display(row, value);
            int scaled = SetupPrecisionData.Quantize(value);
            int whole = Math.Abs(scaled) / SetupPrecisionData.Scale;
            return (scaled < 0 ? "-" : string.Empty) + whole.ToString(CultureInfo.InvariantCulture);
        }

        private static float SliderPosition(Row row, float value)
        {
            if (IsSpecial(row, value)) return value;
            return SetupPrecisionData.Quantize(value) / SetupPrecisionData.Scale;
        }

        private static bool IsSpecial(Row row, float value)
        {
            return value == row.Slider.minValue && row.Controller.GJDAJCALLPF == -12345f
                || value == row.Slider.maxValue && row.Controller.PPFPKBJFNEH == 12345f
                || float.IsNaN(value) || float.IsInfinity(value) || Math.Abs(value) > SetupPrecisionData.Maximum;
        }

        private static void SetDigits(Row row, float value)
        {
            string special = IsSpecial(row, value) ? Display(row, value) : null;
            string specialDigits = row.Group == "Param" ? DigitsForSpecial(special) : null;
            bool distributed = specialDigits != null;
            row.Special.gameObject.SetActive(special != null && !distributed);
            row.Special.text = special != null && !distributed ? special : string.Empty;
            row.Sign.text = string.Empty;
            row.Separator.text = special == null || distributed ? "," : string.Empty;
            row.HybridSeparator.text = special == null ? "," : string.Empty;
            if (distributed)
            {
                for (int place = 0; place < DigitCount; place++)
                    row.Digits[place].Value.text = specialDigits[place] == ' '
                        ? string.Empty : specialDigits[place].ToString();
            }
            else if (special != null)
            {
                for (int place = 0; place < DigitCount; place++) row.Digits[place].Value.text = string.Empty;
            }
            else
            {
                int scaled = SetupPrecisionData.Quantize(value);
                string digits = DigitsForScaled(scaled);
                row.Sign.text = scaled < 0 ? "-" : string.Empty;
                for (int place = 0; place < DigitCount; place++) row.Digits[place].Value.text = digits[place].ToString();
            }
            string fraction = special == null ? DigitsForScaled(SetupPrecisionData.Quantize(value)).Substring(3, 3) : string.Empty;
            for (int index = 0; index < row.FractionDigits.Length; index++)
                row.FractionDigits[index].Value.text = special == null ? fraction[index].ToString() : string.Empty;
        }

        internal static string DigitsForSpecial(string special)
        {
            return special == "STOP" || special == "FREE" ? " " + special + " " : null;
        }

        internal static string DigitsForScaled(int scaled)
        {
            if (scaled < -SetupPrecisionData.MaxScaled || scaled > SetupPrecisionData.MaxScaled)
                throw new ArgumentOutOfRangeException("scaled");
            return Math.Abs(scaled).ToString("D6", CultureInfo.InvariantCulture);
        }

        internal static int StepScaled(int place, bool alt)
        {
            return StepScaledWithModifiers(place, alt, false);
        }

        internal static int StepScaledWithModifiers(int place, bool alt, bool control)
        {
            if (place < 0 || place >= PlaceScales.Length) throw new ArgumentOutOfRangeException("place");
            if (control) return alt ? 1 : 100;
            return PlaceScales[place] * (alt ? 10 : 1);
        }

        internal static int AdjustScaled(int current, int place, int direction, bool alt, int minimum, int maximum)
        {
            return AdjustScaledWithModifiers(current, place, direction, alt, false, minimum, maximum);
        }

        internal static int AdjustScaledWithModifiers(int current, int place, int direction, bool alt, bool control,
            int minimum, int maximum)
        {
            if (direction != -1 && direction != 1 || minimum > maximum) throw new ArgumentOutOfRangeException("direction");
            long adjusted = current + (long)direction * StepScaledWithModifiers(place, alt, control);
            if (adjusted < minimum) return minimum;
            if (adjusted > maximum) return maximum;
            return (int)adjusted;
        }

        internal static int CenterScaled(int minimum, int maximum)
        {
            if (minimum > maximum) throw new ArgumentOutOfRangeException("minimum");
            return (int)Math.Round(((long)minimum + maximum) / 2.0, MidpointRounding.AwayFromZero);
        }

        internal static int MirrorScaled(int current, int minimum, int maximum)
        {
            if (minimum > maximum) throw new ArgumentOutOfRangeException("minimum");
            if (current < minimum || current > maximum) return current;
            long mirrored = minimum < 0 && maximum > 0
                ? -(long)current
                : (long)minimum + maximum - current;
            return mirrored < minimum || mirrored > maximum ? current : (int)mirrored;
        }

        internal static int BoundScaled(float value, bool lower)
        {
            double scaled = (double)value * SetupPrecisionData.Scale;
            double nearest = Math.Round(scaled, MidpointRounding.AwayFromZero);
            if (Math.Abs(scaled - nearest) < 0.0001d) return checked((int)nearest);
            return checked((int)(lower ? Math.Ceiling(scaled) : Math.Floor(scaled)));
        }

        internal static int WholeValueScaled(int current, bool center, bool zeroBased, int minimum, int maximum)
        {
            if (minimum > maximum) throw new ArgumentOutOfRangeException("minimum");
            int logicalMinimum = zeroBased ? 0 : minimum;
            if (logicalMinimum > maximum) logicalMinimum = minimum;
            int target = center ? CenterScaled(logicalMinimum, maximum)
                : MirrorScaled(current, logicalMinimum, maximum);
            if (target < minimum) return minimum;
            if (target > maximum) return maximum;
            return target;
        }

        private static void NumericLimits(Row row, out int minimum, out int maximum)
        {
            float numericMinimum = Mathf.Max(row.Slider.minValue + (row.Controller.GJDAJCALLPF != 0 ? 1f : 0f), -SetupPrecisionData.Maximum);
            float numericMaximum = Mathf.Min(row.Slider.maxValue - (row.Controller.PPFPKBJFNEH != 0 ? 1f : 0f), SetupPrecisionData.Maximum);
            minimum = BoundScaled(numericMinimum, true);
            maximum = BoundScaled(numericMaximum, false);
        }

        private static void Nudge(Row row, int place, int direction, string source, bool precisionModifiers)
        {
            if (row == null || !row.Enabled || !row.Interactable || ActiveDigit(row, place) == null) return;
            try
            {
                BlockData block = Selected.GetValue(row.Build) as BlockData;
                if (row.Group != Group(block) || !SetupPrecisionData.Supports(block, row.Slot)) return;
                float current = SetupPrecisionData.Read(block, row.Slot);
                float numericMinimum = Mathf.Max(row.Slider.minValue + (row.Controller.GJDAJCALLPF != 0 ? 1f : 0f), -SetupPrecisionData.Maximum);
                float numericMaximum = Mathf.Min(row.Slider.maxValue - (row.Controller.PPFPKBJFNEH != 0 ? 1f : 0f), SetupPrecisionData.Maximum);
                bool lowSpecial = row.Controller.GJDAJCALLPF != 0;
                bool highSpecial = row.Controller.PPFPKBJFNEH != 0;
                float target;
                if (current < -SetupPrecisionData.Maximum)
                    target = direction > 0 ? numericMinimum : current;
                else if (current > SetupPrecisionData.Maximum)
                    target = direction < 0 ? numericMaximum : current;
                else if (lowSpecial && current == row.Slider.minValue)
                    target = direction > 0 ? numericMinimum : current;
                else if (highSpecial && current == row.Slider.maxValue)
                    target = direction < 0 ? numericMaximum : current;
                else
                {
                    int minimum, maximum;
                    NumericLimits(row, out minimum, out maximum);
                    int currentScaled = SetupPrecisionData.Quantize(current);
                    bool alt = AltDown();
                    bool control = precisionModifiers && ControlDown();
                    long raw = currentScaled + (long)direction * StepScaledWithModifiers(place, alt, control);
                    if (raw < minimum && lowSpecial) target = row.Slider.minValue;
                    else if (raw > maximum && highSpecial) target = row.Slider.maxValue;
                    else target = AdjustScaledWithModifiers(currentScaled, place, direction, alt, control, minimum, maximum)
                        / (float)SetupPrecisionData.Scale;
                    source += control ? alt ? "+Ctrl+Alt" : "+Ctrl" : alt ? "+Alt" : string.Empty;
                }
                if (SetupPrecisionData.Same(current, target)) return;
                Commit(row, target, true, source + ":place=" + place);
            }
            catch (Exception error) { Report(error); }
        }

        private static void NudgeSliderPrecision(Row row, int direction, string source)
        {
            if (row == null || !row.Enabled || !row.Interactable || direction != -1 && direction != 1) return;
            try
            {
                BlockData block = Selected.GetValue(row.Build) as BlockData;
                if (row.Group != Group(block) || !SetupPrecisionData.Supports(block, row.Slot)) return;
                float current = SetupPrecisionData.Read(block, row.Slot);
                float numericMinimum = Mathf.Max(row.Slider.minValue + (row.Controller.GJDAJCALLPF != 0 ? 1f : 0f), -SetupPrecisionData.Maximum);
                float numericMaximum = Mathf.Min(row.Slider.maxValue - (row.Controller.PPFPKBJFNEH != 0 ? 1f : 0f), SetupPrecisionData.Maximum);
                bool lowSpecial = row.Controller.GJDAJCALLPF != 0;
                bool highSpecial = row.Controller.PPFPKBJFNEH != 0;
                bool alt = AltDown();
                float target;
                if (current < -SetupPrecisionData.Maximum)
                    target = direction > 0 ? numericMinimum : current;
                else if (current > SetupPrecisionData.Maximum)
                    target = direction < 0 ? numericMaximum : current;
                else if (lowSpecial && current == row.Slider.minValue)
                    target = direction > 0 ? numericMinimum : current;
                else if (highSpecial && current == row.Slider.maxValue)
                    target = direction < 0 ? numericMaximum : current;
                else
                {
                    int minimum, maximum;
                    NumericLimits(row, out minimum, out maximum);
                    int currentScaled = SetupPrecisionData.Quantize(current);
                    long raw = currentScaled + (long)direction * StepScaledWithModifiers(0, alt, true);
                    if (raw < minimum && lowSpecial) target = row.Slider.minValue;
                    else if (raw > maximum && highSpecial) target = row.Slider.maxValue;
                    else target = AdjustScaledWithModifiers(currentScaled, 0, direction, alt, true, minimum, maximum)
                        / (float)SetupPrecisionData.Scale;
                }
                if (SetupPrecisionData.Same(current, target)) return;
                Commit(row, target, true, source + (alt ? "+Ctrl+Alt" : "+Ctrl") + ":slider");
            }
            catch (Exception error) { Report(error); }
        }

        private static bool HandleSliderPrecisionInput(Row row)
        {
            if (row == null || row.Editing || !row.Interactable || !ControlDown() || !SliderPointerInside(row))
            {
                if (row != null) row.SliderPrecisionHeldDirection = 0;
                return false;
            }

            bool decrease = Input.GetKey(KeyCode.A);
            bool increase = Input.GetKey(KeyCode.D);
            int keyDirection = decrease == increase ? 0 : decrease ? -1 : 1;
            float wheel = MouseWheelEnabled() ? Input.GetAxis("MouseW") : 0f;
            int wheelDirection = wheel < -0.001f ? -1 : wheel > 0.001f ? 1 : 0;

            if (wheelDirection != 0)
            {
                row.SliderPrecisionHeldDirection = 0;
                NudgeSliderPrecision(row, wheelDirection, "wheel");
                return true;
            }
            if (keyDirection == 0)
            {
                row.SliderPrecisionHeldDirection = 0;
                return false;
            }

            float now = Time.unscaledTime;
            bool first = keyDirection != row.SliderPrecisionHeldDirection
                || keyDirection < 0 && Input.GetKeyDown(KeyCode.A)
                || keyDirection > 0 && Input.GetKeyDown(KeyCode.D);
            if (first || now >= row.SliderPrecisionNextRepeat)
            {
                NudgeSliderPrecision(row, keyDirection, keyDirection > 0 ? "key-D" : "key-A");
                row.SliderPrecisionNextRepeat = now + (first ? 0.35f : 0.08f);
            }
            row.SliderPrecisionHeldDirection = keyDirection;
            return true;
        }

        private static void SetWholeValue(Row row, int place, bool center, string source)
        {
            if (row == null || !row.Enabled || !row.Interactable) return;
            try
            {
                BlockData block = Selected.GetValue(row.Build) as BlockData;
                if (row.Group != Group(block) || !SetupPrecisionData.Supports(block, row.Slot)) return;
                float current = SetupPrecisionData.Read(block, row.Slot);
                int minimum, maximum;
                NumericLimits(row, out minimum, out maximum);
                if (!center)
                {
                    bool special = current == row.Slider.minValue && row.Controller.GJDAJCALLPF != 0
                        || current == row.Slider.maxValue && row.Controller.PPFPKBJFNEH != 0
                        || float.IsNaN(current) || float.IsInfinity(current) || Math.Abs(current) > SetupPrecisionData.Maximum;
                    if (special) return;
                }
                int target = WholeValueScaled(SetupPrecisionData.Quantize(current), center,
                    SetupPrecisionData.IsSize(block, row.Slot), minimum, maximum);
                float value = target / (float)SetupPrecisionData.Scale;
                if (SetupPrecisionData.Same(current, value)) return;
                Commit(row, value, false, source + ":place=" + place);
            }
            catch (Exception error) { Report(error); }
        }

        private static bool AltDown()
        {
            return Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt);
        }

        private static bool ControlDown()
        {
            return Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);
        }

        private static bool MouseWheelEnabled()
        {
            try
            {
                object settings = GameSettings.GetValue(null);
                return settings != null && (bool)WheelSlider.GetValue(settings);
            }
            catch (Exception error)
            {
                Report(error);
                return false;
            }
        }

        internal static bool TryParseInput(string text, out float value)
        {
            value = 0f;
            if (text == null) return false;
            text = text.Trim();
            if (text.Length == 0) return false;
            int index = 0;
            bool negative = false;
            if (text[0] == '-')
            {
                negative = true;
                index++;
                if (index == text.Length) return false;
            }
            long whole = 0;
            int fraction = 0;
            int fractionDigits = 0;
            bool separator = false;
            bool anyDigit = false;
            for (; index < text.Length; index++)
            {
                char character = text[index];
                if (character == '.' || character == ',')
                {
                    if (separator) return false;
                    separator = true;
                    continue;
                }
                if (character < '0' || character > '9') return false;
                anyDigit = true;
                int digit = character - '0';
                if (!separator)
                {
                    whole = whole * 10 + digit;
                    if (whole > SetupPrecisionData.Maximum) return false;
                }
                else
                {
                    if (++fractionDigits > 3) return false;
                    fraction = fraction * 10 + digit;
                }
            }
            if (!anyDigit) return false;
            while (fractionDigits < 3) { fraction *= 10; fractionDigits++; }
            long scaled = whole * SetupPrecisionData.Scale + fraction;
            if (scaled > SetupPrecisionData.MaxScaled) return false;
            if (negative) scaled = -scaled;
            value = scaled / (float)SetupPrecisionData.Scale;
            return true;
        }

        internal static string FormatInputValue(float value)
        {
            int scaled = SetupPrecisionData.Quantize(value);
            int absolute = Math.Abs(scaled);
            return (scaled < 0 ? "-" : string.Empty)
                + (absolute / SetupPrecisionData.Scale).ToString(CultureInfo.InvariantCulture)
                + "," + (absolute % SetupPrecisionData.Scale).ToString("D3", CultureInfo.InvariantCulture);
        }

        internal static string NormalizeInputText(string text)
        {
            if (string.IsNullOrEmpty(text)) return text;
            string normalized = text.Replace('.', ',');
            if (normalized.IndexOf(',') >= 0) return normalized;
            int firstDigit = normalized[0] == '-' ? 1 : 0;
            return normalized.Length - firstDigit > 3 ? normalized.Insert(firstDigit + 3, ",") : normalized;
        }

        internal static char ValidateInputCharacter(string current, int insertion, char added)
        {
            return ValidateInputReplacement(current, insertion, added, insertion, insertion);
        }

        internal static char ValidateInputReplacement(string current, int insertion, char added,
            int selectionAnchor, int selectionFocus)
        {
            if (added != '-' && added != '.' && added != ',' && (added < '0' || added > '9')) return '\0';
            char canonical = added == '.' ? ',' : added;
            current = NormalizeInputText(current) ?? string.Empty;
            int selectionStart = Mathf.Clamp(Math.Min(selectionAnchor, selectionFocus), 0, current.Length);
            int selectionEnd = Mathf.Clamp(Math.Max(selectionAnchor, selectionFocus), selectionStart, current.Length);
            if (selectionStart != selectionEnd)
            {
                current = current.Remove(selectionStart, selectionEnd - selectionStart);
                insertion = selectionStart;
            }
            if (insertion < 0 || insertion > current.Length) return '\0';
            string candidate = NormalizeInputText(current.Insert(insertion, canonical.ToString()));
            if (candidate.Length > 8) return '\0';
            int index = 0;
            if (candidate.Length > 0 && candidate[0] == '-') index = 1;
            int separators = 0;
            int fractionalDigits = 0;
            long whole = 0;
            bool afterSeparator = false;
            bool fractionNonZero = false;
            for (; index < candidate.Length; index++)
            {
                char character = candidate[index];
                if (character == '.' || character == ',')
                {
                    if (++separators > 1) return '\0';
                    afterSeparator = true;
                    continue;
                }
                if (character < '0' || character > '9') return '\0';
                int digit = character - '0';
                if (!afterSeparator)
                {
                    whole = whole * 10 + digit;
                    if (whole > SetupPrecisionData.Maximum) return '\0';
                }
                else
                {
                    if (++fractionalDigits > 3) return '\0';
                    if (digit != 0) fractionNonZero = true;
                }
            }
            if (whole == SetupPrecisionData.Maximum && fractionNonZero) return '\0';
            return canonical;
        }

        private static char ValidateRowInputCharacter(Row row, string current, int insertion, char added)
        {
            int anchor = insertion;
            int focus = insertion;
            if (row != null && row.Input != null)
            {
                anchor = row.Input.selectionAnchorPosition;
                focus = row.Input.selectionFocusPosition;
            }
            char accepted = ValidateInputReplacement(current, insertion, added, anchor, focus);
            if (accepted != '\0' && anchor != focus && row != null)
                SetupPrecision.Log("INPUT_REPLACE_SELECTION group=" + row.Group + " slot=" + row.Slot
                    + " range=" + Math.Min(anchor, focus) + ".." + Math.Max(anchor, focus));
            return accepted;
        }

        internal static int FractionSelectionStart(string text)
        {
            if (string.IsNullOrEmpty(text)) return 0;
            int separator = text.IndexOf(',');
            if (separator < 0) separator = text.IndexOf('.');
            return separator < 0 ? text.Length : Math.Min(separator + 1, text.Length);
        }

        private static void OnInputChanged(Row row, string text)
        {
            if (row == null || row.Input == null || row.FormattingInput) return;
            string normalized = NormalizeInputText(text);
            if (normalized != text)
            {
                int separator = normalized == null ? -1 : normalized.IndexOf(',');
                int caret = row.Input.caretPosition;
                row.FormattingInput = true;
                try { row.Input.text = normalized; }
                finally { row.FormattingInput = false; }
                int next = Mathf.Clamp(caret >= separator && separator >= 0 ? caret + 1 : caret, 0, normalized.Length);
                row.Input.selectionAnchorPosition = next;
                row.Input.selectionFocusPosition = next;
                SetupPrecision.Log("INPUT_AUTO_SEPARATOR group=" + row.Group + " slot=" + row.Slot + " digits=3");
            }
            ApplyInputFont(row, normalized);
        }

        internal static bool SupportsDirectInput(int mode)
        {
            return mode == (int)SetupPrecisionEditorMode.VanillaSlider;
        }

        private static void BeginEdit(Row row)
        {
            if (row == null || !row.Enabled || !row.Interactable
                || !SupportsDirectInput(row.Mode) || row.Input == null) return;
            try
            {
                BlockData block = Selected.GetValue(row.Build) as BlockData;
                if (row.Group != Group(block) || !SetupPrecisionData.Supports(block, row.Slot)) return;
                float current = SetupPrecisionData.Read(block, row.Slot);
                row.CancelEdit = false;
                row.Editing = true;
                int renderedNativeFontSize = RenderedNativeFontSize(row);
                row.InputFontCeiling = Math.Max(InputTextMinFontSize,
                    renderedNativeFontSize - InputFontVisualOffset);
                row.Input.text = IsSpecial(row, current) ? string.Empty : FormatInputValue(current);
                int inputFontSize = ApplyInputFont(row, row.Input.text);
                row.Input.interactable = true;
                row.InputRoot.SetActive(true);
                row.InputRoot.transform.SetAsLastSibling();
                row.NativeText.enabled = false;
                row.Input.Select();
                row.Input.ActivateInputField();
                int selectionStart = FractionSelectionStart(row.Input.text);
                row.Input.selectionAnchorPosition = selectionStart;
                row.Input.selectionFocusPosition = row.Input.text.Length;
                row.Input.ForceLabelUpdate();
                if (row.InputSelection != null)
                    row.InputSelection.Schedule(selectionStart, row.Input.text.Length);
                SetupPrecision.Log("INPUT_BEGIN group=" + row.Group + " slot=" + row.Slot + " mode=" + SetupPrecision.EditorModeName
                    + " normalFont=" + renderedNativeFontSize + " inputCeiling=" + row.InputFontCeiling
                    + " inputFont=" + inputFontSize + " visualOffset=-" + InputFontVisualOffset
                    + " preferredWidth=" + PreferredInputWidth(row.Input.textComponent, row.Input.text, inputFontSize).ToString("0.##", CultureInfo.InvariantCulture)
                    + " availableWidth=" + row.Input.textComponent.rectTransform.rect.width.ToString("0.##", CultureInfo.InvariantCulture)
                    + " selection=" + selectionStart + ".." + row.Input.text.Length
                    + " fit=rendered-ceiling-minus-one+preferred-width textLength=" + row.Input.text.Length);
            }
            catch (Exception error) { Report(error); }
        }

        private static void EndEdit(Row row, string text)
        {
            if (row == null || !row.Editing) return;
            bool cancelled = row.CancelEdit || row.Input.wasCanceled;
            row.Editing = false;
            row.CancelEdit = false;
            if (row.InputSelection != null) row.InputSelection.Cancel();
            row.InputRoot.SetActive(false);
            row.NativeText.enabled = row.Enabled && row.Mode != (int)SetupPrecisionEditorMode.DigitSpinner
                ? row.NativeTextEnabled : false;
            if (cancelled || suppress != 0 || !row.Enabled) { OnSettingChanged(); return; }
            try
            {
                float value;
                if (!TryParseInput(text, out value))
                {
                    SetupPrecision.Log("INPUT_REJECTED group=" + row.Group + " slot=" + row.Slot
                        + " reason=format-or-range decimals=3 range=-500..500");
                    Refresh(row.Build);
                    return;
                }
                int minimum, maximum;
                NumericLimits(row, out minimum, out maximum);
                int scaled = SetupPrecisionData.Quantize(value);
                if (scaled < minimum) scaled = minimum;
                if (scaled > maximum) scaled = maximum;
                Commit(row, scaled / (float)SetupPrecisionData.Scale, false, "double-click-input");
                Refresh(row.Build);
            }
            catch (Exception error) { Report(error); }
        }

        private static void CancelInput(Row row)
        {
            if (row == null || row.InputRoot == null) return;
            if (row.InputSelection != null) row.InputSelection.Cancel();
            if (row.Editing)
            {
                row.CancelEdit = true;
                row.Editing = false;
                row.Input.DeactivateInputField();
                row.CancelEdit = false;
            }
            row.InputRoot.SetActive(false);
        }

        internal static int MergeWholeScaled(int current, int whole, int minimum, int maximum)
        {
            if (minimum > maximum) throw new ArgumentOutOfRangeException("minimum");
            int fraction = Math.Abs(current % SetupPrecisionData.Scale);
            long merged = (long)whole * SetupPrecisionData.Scale;
            if (fraction != 0) merged += whole < 0 || whole == 0 && current < 0 ? -fraction : fraction;
            if (merged < minimum) return minimum;
            if (merged > maximum) return maximum;
            return (int)merged;
        }

        internal static int ClearFractionScaled(int scaled)
        {
            return scaled / SetupPrecisionData.Scale * SetupPrecisionData.Scale;
        }

        private static void ResetVanillaFraction(Row row)
        {
            if (row == null || !row.Enabled || row.Mode != (int)SetupPrecisionEditorMode.VanillaSlider || row.Editing) return;
            try
            {
                BlockData block = Selected.GetValue(row.Build) as BlockData;
                if (row.Group != Group(block) || !SetupPrecisionData.Supports(block, row.Slot)) return;
                float current = SetupPrecisionData.Read(block, row.Slot);
                if (IsSpecial(row, current)) return;
                int minimum, maximum;
                NumericLimits(row, out minimum, out maximum);
                int scaled = Mathf.Clamp(ClearFractionScaled(SetupPrecisionData.Quantize(current)), minimum, maximum);
                Commit(row, scaled / (float)SetupPrecisionData.Scale, false, "key-S-fraction-reset");
            }
            catch (Exception error) { Report(error); }
        }

        private static float SliderValue(Row row, float value)
        {
            float minimum = Mathf.Max(row.Slider.minValue, -SetupPrecisionData.Maximum);
            float maximum = Mathf.Min(row.Slider.maxValue, SetupPrecisionData.Maximum);
            value = Mathf.Clamp(value, minimum, maximum);
            value = SetupPrecisionData.Quantize(value) / (float)SetupPrecisionData.Scale;
            if (row.Controller.GJDAJCALLPF != 0 && value < row.Slider.minValue + 1)
                value = value < row.Slider.minValue + 0.5f ? row.Slider.minValue : row.Slider.minValue + 1;
            if (row.Controller.PPFPKBJFNEH != 0 && value > row.Slider.maxValue - 1)
                value = value > row.Slider.maxValue - 0.5f ? row.Slider.maxValue : row.Slider.maxValue - 1;
            return value;
        }

        private static float EditedSliderValue(Row row, float value)
        {
            if (row.Mode == (int)SetupPrecisionEditorMode.DigitSpinner) return SliderValue(row, value);
            if (row.Controller.GJDAJCALLPF != 0 && value == row.Slider.minValue) return row.Slider.minValue;
            if (row.Controller.PPFPKBJFNEH != 0 && value == row.Slider.maxValue) return row.Slider.maxValue;
            int minimum, maximum;
            NumericLimits(row, out minimum, out maximum);
            int whole = Mathf.RoundToInt(value);
            if (row.ResetFractionOnSliderCallback)
                return MergeWholeScaled(0, whole, minimum, maximum) / (float)SetupPrecisionData.Scale;
            BlockData block = Selected.GetValue(row.Build) as BlockData;
            float currentValue = block == null ? 0f : SetupPrecisionData.Read(block, row.Slot);
            int current = Math.Abs(currentValue) <= SetupPrecisionData.Maximum
                ? SetupPrecisionData.Quantize(currentValue) : 0;
            return MergeWholeScaled(current, whole, minimum, maximum) / (float)SetupPrecisionData.Scale;
        }

        private static bool SliderLabelPrefix(SliderController __instance, float DOCDPKDOCKB)
        {
            Row row;
            if (!SetupPrecision.IsRegistered || !Rows.TryGetValue(__instance, out row) || !row.Enabled) return true;
            if (suppress != 0) return false;
            try { Commit(row, EditedSliderValue(row, DOCDPKDOCKB), true, "slider-callback"); }
            catch (Exception error) { Report(error); }
            return false;
        }

        private static bool SliderUpdatePrefix(SliderController __instance, out bool __state)
        {
            __state = false;
            Row row;
            if (!Rows.TryGetValue(__instance, out row) || !row.Enabled) return true;
            bool runNative = row.Mode != (int)SetupPrecisionEditorMode.DigitSpinner && !row.Editing;
            if (runNative && HandleSliderPrecisionInput(row)) return false;
            if (runNative && row.Mode == (int)SetupPrecisionEditorMode.VanillaSlider
                && Input.GetKeyDown(KeyCode.S) && SliderPointerInside(row))
            {
                row.ResetFractionOnSliderCallback = true;
                __state = true;
            }
            return runNative;
        }

        private static void SliderUpdatePostfix(SliderController __instance, bool __state)
        {
            if (!__state) return;
            Row row;
            if (!Rows.TryGetValue(__instance, out row)) return;
            row.ResetFractionOnSliderCallback = false;
            ResetVanillaFraction(row);
        }

        private static bool BuildSliderPrefix(Build __instance, string DPGKEOAGONA, GameObject NGLBLAGMBLN)
        {
            BlockData block = Selected.GetValue(__instance) as BlockData;
            if (!SetupPrecision.Enabled || NGLBLAGMBLN == null) return true;
            Row row;
            bool known = Rows.TryGetValue(NGLBLAGMBLN.GetComponent<SliderController>(), out row);
            if (known && (row.Group != Group(block) || !row.Slider.gameObject.activeInHierarchy)) return false;
            if (DPGKEOAGONA != Group(block)) return true;
            int slot = SceneMan.GetWidgetID(NGLBLAGMBLN);
            if (!SetupPrecisionData.Supports(block, slot)) return true;
            if (suppress != 0 || (bool)Updating.GetValue(__instance) || (int)Initializing.GetValue(__instance) != 0) return false;
            if (known) Commit(row, EditedSliderValue(row, row.Slider.value), true, "build-slider");
            return false;
        }

        private static void Commit(Row row, float value, bool continuous, string source)
        {
            Build build = row.Build;
            BlockData block = Selected.GetValue(build) as BlockData;
            if (row.Group != Group(block) || !SetupPrecisionData.Supports(block, row.Slot)
                || (int)Initializing.GetValue(build) != 0 || (bool)Updating.GetValue(build)) return;
            bool coupler = block.type == BlockData.AAHMDBHDCDK.Coupler && row.Slot >= 3;
            if (coupler && CouplerRotation.IsRotationReadOnly(block)) { Refresh(build); return; }
            if (SetupPrecisionData.Same(SetupPrecisionData.Read(block, row.Slot), value)) return;
            CouplerRotationProfiles.Rotation vanilla = coupler ? CouplerRotationProfiles.InitialVanilla(block) : default(CouplerRotationProfiles.Rotation);
            suppress++;
            try
            {
                Undo.Invoke(build, new object[] { continuous, true });
                block = (BlockData)Selected.GetValue(build);
                SetupPrecisionData.Set(block, row.Slot, value);
                if (coupler && !CouplerRotation.Enabled && value != 0)
                    for (int slot = 3; slot < 6; slot++) if (slot != row.Slot) SetupPrecisionData.Set(block, slot, 0);
                if ((row.Group == "ParamBox" || row.Group == "ParamCap") && (row.Slot == 6 || row.Slot == 7)
                    && (MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68 == null || !MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.freeBoxRot))
                    SetupPrecisionData.Set(block, row.Slot == 6 ? 7 : 6, 0);
                if (coupler && CouplerRotation.Enabled) CouplerRotationProfiles.RememberFreeEdit(block, vanilla);
                ApplyPreview(build, block);
                float applied = SetupPrecisionData.Read(block, row.Slot);
                row.Slider.value = SliderPosition(row, applied);
                row.NativeText.text = row.Mode == (int)SetupPrecisionEditorMode.SliderDigitSpinner
                    ? HybridDisplay(row, applied) : Display(row, applied);
                PreviousLabel.SetValue(row.Controller, row.NativeText.text);
                SetDigits(row, applied);
                SetupPrecision.Log("EDIT type=" + block.type + " block=" + block.x + "," + block.y + "," + block.z
                    + " slot=" + row.Slot + " value=" + SetupPrecisionData.Format(applied) + " source=" + source + " undo=native");
            }
            finally { suppress--; }
        }

        private static void ApplyPreview(Build build, BlockData block)
        {
            Changed.SetValue(build, true);
            HIPBCCKFFAG assembly = Assembly.GetValue(build) as HIPBCCKFFAG;
            if (assembly == null || !assembly.HCMMJPFOIHD) return;
            SetupPrecision.PersistPreview(assembly, block);
            Rebuild.Invoke(build, null);
        }

        internal static bool TryCopySettings(Build build, BlockData source)
        {
            if (CouplerRotationUi.TryCopySettings(build, source)) return true;
            BlockData block = Selected.GetValue(build) as BlockData;
            if (!SetupPrecision.Enabled || Group(block) == null || source == null || source.type != block.type) return false;
            suppress++;
            try
            {
                Undo.Invoke(build, new object[] { false, true });
                block = (BlockData)Selected.GetValue(build);
                Array.Copy(source.actionID, block.actionID, 8);
                Array.Copy(source.actionParam, block.actionParam, 8);
                SetupPrecisionData.Copy(block, source);
                ApplyPreview(build, block);
                // JOHIPODALCN reaches this path only in the native world-paste
                // mode. OPMMCNOHEMC is the full SETUP panel population path;
                // invoking it here makes the same left click both paste the
                // clipboard and open the selected block's settings. The panel
                // will populate naturally when the user opens it later.
                SetupPrecision.Log("COPY_SETTINGS type=" + block.type
                    + " decimals=preserved undo=native panelRefresh=deferred pasteMode=preserved");
            }
            finally { suppress--; }
            return true;
        }

        private static void Report(Exception error)
        {
            string message = error.ToString();
            if (message == lastFailure) return;
            lastFailure = message;
            SetupPrecision.Log("UI_FAILED " + message);
        }
    }
}
