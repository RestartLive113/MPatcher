using System;
using HarmonyLib;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
    // MPatcher's cloned dropdowns reuse labels (OFF, language codes, etc.). Native
    // SetItems registers every label in SceneMan's global button dictionary, where
    // a native scene or another dropdown can already own that key. Dropdown clicks
    // are routed through ButtonController.NIMIHAFMJDB; they need no global key.
    internal static class SettingsListIsolation
    {
        private static bool registered;
        [ThreadStatic] private static int scope;
        internal static void TryRegister()
        {
            if (registered) return;
            const string id = "mpatcher.settings-list-isolation.v1";
            Harmony candidate = new Harmony(id);
            try
            {
                candidate.Patch(AccessTools.Method(typeof(SceneMan), "CreateButton",
                    new Type[] { typeof(string), typeof(GameObject), typeof(int), typeof(int), typeof(GameObject) }),
                    new HarmonyMethod(typeof(SettingsListIsolation), "Prefix"), new HarmonyMethod(typeof(SettingsListIsolation), "Postfix"));
                registered = true; Log("REGISTERED scope=MPatcher-dropdown-construction native-scenes=unchanged");
            }
            catch (Exception error) { candidate.UnpatchAll(id); Log("REGISTER_FAILED " + error); }
        }
        internal static void SetItemsAndSelect(ListController control, string selected, string[] choices)
        {
            scope++;
            try { control.SetItemsAndSelect(selected, choices); }
            finally { scope--; }
            Log("LOCAL_CHOICES count=" + choices.Length + " control=" + control.name);
        }
        private static void Prefix(ref string __0, out string __state)
        {
            __state = null;
            if (scope <= 0 || !__0.StartsWith("BTN_", StringComparison.Ordinal)) return;
            __state = __0; __0 = "MPatcherLocal_" + __0;
        }
        private static void Postfix(GameObject __result, string __state)
        {
            if (__state != null && __result != null) __result.name = __state;
        }
        private static void Log(string value)
        { mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng("[SETTINGS-LISTS] " + value); }
    }
}
