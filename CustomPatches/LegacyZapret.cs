using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using MPatchrMain;
using UnityEngine;
using UnityEngine.UI;

namespace MPatcherFork.CustomPatches
{
    // Only the protected service can start winws. No elevation, shell, or executable paths from the game.
    internal static class LegacyZapret
    {
        private static LegacyZapretDriver driver;
        private static volatile bool desired;
        private static volatile bool quit;
        private static readonly AutoResetEvent changed = new AutoResetEvent(false);
        private static readonly object stateGate = new object();
        private static string pendingState;
        private static string currentState = "OFF";
        private static Text statusLabel;
        private static bool registered;
        internal static bool Enabled { get { return MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68 != null && MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.legacyZapret; } }
        internal static void TryRegister()
        {
            if (registered) return;
            try
            {
                GameObject root = new GameObject("MPatcher.LegacyZapret");
                UnityEngine.Object.DontDestroyOnLoad(root);
                driver = root.AddComponent<LegacyZapretDriver>();
                LegacyZapretProbe.TryRegister(root);
                desired = Enabled; quit = false;
                Thread worker = new Thread(Worker); worker.IsBackground = true; worker.Name = "MPatcher.LegacyZapret"; worker.Start();
                registered = true;
                Log("REGISTERED version=1 enabled=" + desired + " default=false scope=173.230.144.203:23466,50005 helper=service heartbeat=5 lease=20 manualToggle=true");
            }
            catch (Exception error) { Log("REGISTER_FAILED " + error); }
        }
        internal static void SetEnabled(bool value)
        {
            if (MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68 == null) return;
            MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.legacyZapret = value;
            MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.UUiRNMwxRbfk_Fs4cDErRoM();
            desired = value; changed.Set();
            Log("SETTING enabled=" + value + " saved=true");
        }
        internal static void CreateRow(Transform root)
        {
            Class35.smethod_41("Toggle_LegacyZapret", "Zapret - Legacy", root, SetEnabled, Enabled, reInit: false);
            // The status is part of the settings page, never a gameplay overlay.
            // The native factory adds its own prefix to the supplied object name.
            Transform toggle = null;
            foreach (Transform child in Class35.list_0[Class35.int_0])
                if (child.name.EndsWith("Toggle_LegacyZapret", StringComparison.Ordinal)) { toggle = child; break; }
            if (toggle == null) { Log("SETTINGS_UI_MISSING"); return; }
            Text reference = toggle.GetComponentInChildren<Text>();
            if (reference != null) { reference.rectTransform.anchoredPosition = new Vector2(0f, 5f); reference.rectTransform.sizeDelta = new Vector2(230f, 14f); }
            GameObject label = new GameObject("NetworkAssistStatus");
            RectTransform rect = label.AddComponent<RectTransform>(); rect.SetParent(toggle, false);
            rect.anchoredPosition = new Vector2(0f, -8f); rect.sizeDelta = new Vector2(230f, 10f);
            statusLabel = label.AddComponent<Text>();
            if (reference != null) statusLabel.font = reference.font;
            statusLabel.fontSize = 8; statusLabel.alignment = TextAnchor.MiddleCenter;
            statusLabel.color = Color.white; statusLabel.raycastTarget = false;
            statusLabel.text = DisplayStatus();
            Log("SETTINGS_UI name=" + toggle.name + " status=true");
        }
        private static string DisplayStatus()
        {
            if (!desired) return "Off";
            if (currentState.StartsWith("RUNNING") || currentState.StartsWith("STARTING")) return "Active (disable when using VPN)";
            if (currentState.StartsWith("EXTERNAL_ZAPRET")) return "External Zapret detected";
            if (currentState.StartsWith("SERVICE_UNAVAILABLE")) return "Helper unavailable - run the installer";
            if (currentState.StartsWith("REJECTED")) return "Game not registered - run the installer";
            if (currentState.StartsWith("ERROR")) return "Helper error - see logs";
            return "Starting...";
        }
        internal static void Tick()
        {
            string state;
            lock (stateGate) { state = pendingState; pendingState = null; }
            if (state != null && state != currentState) { currentState = state; Log("STATE " + state); }
            if (statusLabel != null) statusLabel.text = DisplayStatus();
        }
        internal static void Shutdown() { quit = true; desired = false; changed.Set(); }
        internal static string State { get { return currentState; } }
        private static void Worker()
        {
            bool acquired = false;
            do
            {
                bool on = desired && !quit;
                string result = "OFF";
                if (on || acquired)
                {
                    result = Exchange(on ? (byte)'1' : (byte)'0');
                    acquired = on;
                }
                lock (stateGate) pendingState = result;
                if (quit) break;
                changed.WaitOne(on ? 5000 : Timeout.Infinite);
            } while (true);
        }
        internal static string Exchange(byte command)
        {
            const string pipe = @"\\.\pipe\MPatcher.LegacyZapret.v1";
            IntPtr handle = new IntPtr(-1);
            try
            {
                if (!WaitNamedPipe(pipe, 250)) return "SERVICE_UNAVAILABLE error=" + Marshal.GetLastWin32Error();
                // Identification-only prevents a spoofed local pipe server from impersonating the game user.
                handle = CreateFile(pipe, 0xC0000000, 0, IntPtr.Zero, 3, 0x00110000, IntPtr.Zero);
                if (handle == new IntPtr(-1)) return "SERVICE_UNAVAILABLE error=" + Marshal.GetLastWin32Error();
                uint written;
                if (!WriteFile(handle, new byte[] { command }, 1, out written, IntPtr.Zero) || written != 1) return "ERROR_WRITE";
                DateTime deadline = DateTime.UtcNow.AddMilliseconds(2000);
                while (DateTime.UtcNow < deadline)
                {
                    uint available;
                    if (!PeekNamedPipe(handle, IntPtr.Zero, 0, IntPtr.Zero, out available, IntPtr.Zero)) return "ERROR_PIPE";
                    if (available > 0)
                    {
                        byte[] buffer = new byte[Math.Min((int)available, 256)]; uint count;
                        if (!ReadFile(handle, buffer, (uint)buffer.Length, out count, IntPtr.Zero)) return "ERROR_READ";
                        return Encoding.ASCII.GetString(buffer, 0, (int)count).Trim();
                    }
                    Thread.Sleep(20);
                }
                return "ERROR_TIMEOUT";
            }
            catch (Exception error) { return "ERROR_" + error.GetType().Name; }
            finally { if (handle != new IntPtr(-1)) CloseHandle(handle); }
        }
        private static void Log(string message) { mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng("[LEGACY-ZAPRET] " + message); }
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)] private static extern bool WaitNamedPipe(string name, uint timeout);
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)] private static extern IntPtr CreateFile(string name, uint access, uint share, IntPtr security, uint creation, uint flags, IntPtr template);
        [DllImport("kernel32.dll", SetLastError = true)] private static extern bool WriteFile(IntPtr handle, byte[] bytes, uint count, out uint written, IntPtr overlapped);
        [DllImport("kernel32.dll", SetLastError = true)] private static extern bool ReadFile(IntPtr handle, byte[] bytes, uint count, out uint read, IntPtr overlapped);
        [DllImport("kernel32.dll", SetLastError = true)] private static extern bool PeekNamedPipe(IntPtr handle, IntPtr buffer, uint size, IntPtr read, out uint available, IntPtr left);
        [DllImport("kernel32.dll")] private static extern bool CloseHandle(IntPtr handle);
    }
    internal sealed class LegacyZapretDriver : MonoBehaviour
    {
        private void Update() { LegacyZapret.Tick(); }
        private void OnApplicationQuit() { LegacyZapret.Shutdown(); }
    }
}
