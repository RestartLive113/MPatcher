using MPatcherFork.CustomPatches;

namespace MPatcherFork.ScaleRuntime
{
    public sealed class ScaleRuntimeModule : IScaleRuntime
    {
        public int ApiVersion { get { return 1; } }
        public void Initialize()
        {
            SculptScaleUi.InitializeRuntime();
            ConnectedShapeScaleV2.InitializeRuntime();
        }
        public void Attach(Build owner) { ConnectedShapeScaleV2.AttachRuntime(owner); }
        public bool FilterMappedKeyDown(SystemData.EHLMFKOOHLI action, bool pressed)
        { return SculptScaleUi.FilterMappedKeyDown(action, pressed); }
        public void BeforeNativeTab(Build owner) { SculptScaleUi.BeforeNativeTab(owner); }
        public void BeforeNativeMask(Build owner) { SculptScaleUi.BeforeNativeMask(owner); }
        public bool IsScaleActive(Build owner) { return SculptScaleUi.IsScaleActive(owner); }
        public bool NumericInputFocused(Build owner) { return SculptScaleUi.NumericInputFocused(owner); }
        public bool OwnsCamera(CameraController camera) { return SculptScaleUi.OwnsCamera(camera); }
        public bool CameraKeyboardAllowed { get { return SculptScaleUi.CameraKeyboardAllowed; } }
        public bool CameraLookHeld { get { return SculptScaleUi.CameraLookHeld; } }
    }
}
