using MPatcherFork.CustomPatches;
namespace MPatcherFork.ScaleRuntime
{
    internal enum SculptScaleCommand
    {
        NativeFirstSculpt,
        EnterScale,
        ReturnSculpt,
        DigitTyping
    }

    internal enum SculptScaleNativeSheetCommand
    {
        None,
        LeaveScaleForNative,
        LeaveScaleForSculptOrMask
    }

    internal enum SculptScaleTabSelection { None, Sculpt, Scale }

    internal static class SculptScaleInputPolicy
    {
        internal static bool LeaveForPreview(bool scaleActive, bool previewPressed, bool textFocused)
        { return scaleActive && previewPressed && !textFocused; }

        internal static SculptScaleTabSelection TabSelection(bool scaleActive, bool sculptActive, bool previewActive)
        {
            if (previewActive) return SculptScaleTabSelection.None;
            return scaleActive ? SculptScaleTabSelection.Scale
                : sculptActive ? SculptScaleTabSelection.Sculpt : SculptScaleTabSelection.None;
        }

        internal static SculptScaleCommand DecideTwo(bool scaleActive, bool sculptActive, bool textFocused)
        {
            if (textFocused) return SculptScaleCommand.DigitTyping;
            if (scaleActive) return SculptScaleCommand.ReturnSculpt;
            return sculptActive ? SculptScaleCommand.EnterScale : SculptScaleCommand.NativeFirstSculpt;
        }

        internal static bool FilterNativeSheetHotkey(SystemData.EHLMFKOOHLI action, bool pressed,
            bool scaleActive, bool textFocused)
        {
            if (!pressed) return false;
            if (textFocused && IsNativeSheetAction(action)) return false;
            // Sculpt is routed through our return command; the raw repeated-2
            // cycling path must never receive that same key press.
            return !scaleActive || action != SystemData.EHLMFKOOHLI.Sculpt;
        }

        internal static bool IsNativeSheetAction(SystemData.EHLMFKOOHLI action)
        {
            return action == SystemData.EHLMFKOOHLI.Form
                || action == SystemData.EHLMFKOOHLI.Sculpt
                || action == SystemData.EHLMFKOOHLI.Paint
                || action == SystemData.EHLMFKOOHLI.Setup
                || action == SystemData.EHLMFKOOHLI.Group
                || IsNativeMaskAction(action);
        }

        internal static bool IsNativeMaskAction(SystemData.EHLMFKOOHLI action)
        {
            return action == SystemData.EHLMFKOOHLI.MaskA
                || action == SystemData.EHLMFKOOHLI.MaskB
                || action == SystemData.EHLMFKOOHLI.MaskC;
        }

        internal static SculptScaleNativeSheetCommand DecideNativeSheet(SystemData.EHLMFKOOHLI action,
            bool pressed, bool scaleActive, bool textFocused)
        {
            if (!pressed || !scaleActive || textFocused || !IsNativeSheetAction(action))
                return SculptScaleNativeSheetCommand.None;
            return action == SystemData.EHLMFKOOHLI.Sculpt || IsNativeMaskAction(action)
                ? SculptScaleNativeSheetCommand.LeaveScaleForSculptOrMask
                : SculptScaleNativeSheetCommand.LeaveScaleForNative;
        }

        internal static bool TryViewportSelection(int button, bool controlAtPress, bool viewportAccepted,
            bool cameraLook, bool inputAllowed, float moved, float held, out ShapeSelectionOperation operation)
        {
            operation = ShapeSelectionOperation.Replace;
            if (!viewportAccepted || cameraLook || !inputAllowed || !ValidGestureMeasure(moved)
                || !ValidGestureMeasure(held) || moved > 6f || held > 0.2f) return false;
            if (button == 0)
            {
                operation = controlAtPress ? ShapeSelectionOperation.Add : ShapeSelectionOperation.Replace;
                return true;
            }
            if (button != 1 || !controlAtPress) return false;
            operation = ShapeSelectionOperation.Remove;
            return true;
        }

        internal static bool ShouldStartCameraLook(int button, bool controlAtPress, bool viewportAccepted,
            bool inputAllowed, float moved, float held)
        {
            return button == 0 && !controlAtPress && viewportAccepted && inputAllowed
                && ValidGestureMeasure(moved) && ValidGestureMeasure(held) && (moved > 6f || held > 0.2f);
        }

        private static bool ValidGestureMeasure(float value)
        { return value >= 0f && !float.IsNaN(value) && !float.IsInfinity(value); }
    }
}
