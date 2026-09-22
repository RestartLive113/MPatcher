using System;
using System.IO;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
    // Explicit local test control. No automatic activation and no network service probing.
    internal sealed class LegacyZapretProbe : MonoBehaviour
    {
        private string file;
        private string last;
        private float next;
        internal static void TryRegister(GameObject root)
        {
            string value = Environment.GetEnvironmentVariable("MPATCHER_LEGACY_ZAPRET_PROBE");
            if (string.IsNullOrEmpty(value)) return;
            if (!Path.IsPathRooted(value)) throw new ArgumentException("Absolute diagnostic control file required");
            LegacyZapretProbe probe = root.AddComponent<LegacyZapretProbe>(); probe.file = value;
        }
        private void Update()
        {
            if (Time.realtimeSinceStartup < next) return;
            next = Time.realtimeSinceStartup + 0.5f;
            try
            {
                string command = File.Exists(file) ? File.ReadAllText(file).Trim() : "";
                if (command != last)
                {
                    last = command;
                    if (command.StartsWith("ON ")) LegacyZapret.SetEnabled(true);
                    else if (command.StartsWith("OFF ")) LegacyZapret.SetEnabled(false);
                    else if (command.StartsWith("QUIT ")) { Application.Quit(); return; }
                }
                int toggles = 0;
                foreach (Control0 control in Resources.FindObjectsOfTypeAll<Control0>()) if (control.name.EndsWith("Toggle_LegacyZapret", StringComparison.Ordinal)) toggles++;
                File.WriteAllText(file + ".state", "pid=" + System.Diagnostics.Process.GetCurrentProcess().Id
                    + "\nenabled=" + LegacyZapret.Enabled + "\nstate=" + LegacyZapret.State + "\ntoggles=" + toggles + "\ncommand=" + last);
            }
            catch (Exception error) { Debug.Log("[LEGACY-ZAPRET-PROBE] " + error.GetType().Name); }
        }
    }
}
