namespace MPatcherFork.CustomPatches
{
    // Keep this ABI and the Harmony callback bodies in the core unchanged for
    // ordinary Scale/UI edits. The core has no reference to the module DLL.
    public interface IScaleRuntime
    {
        int ApiVersion { get; }
        void Initialize();
        void Attach(Build owner);
        bool FilterMappedKeyDown(SystemData.EHLMFKOOHLI action, bool pressed);
        void BeforeNativeTab(Build owner);
        void BeforeNativeMask(Build owner);
        bool IsScaleActive(Build owner);
        bool NumericInputFocused(Build owner);
        bool OwnsCamera(CameraController camera);
        bool CameraKeyboardAllowed { get; }
        bool CameraLookHeld { get; }
    }
}
