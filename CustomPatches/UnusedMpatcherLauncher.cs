using UnityEngine;

namespace MPatcherFork.CustomPatches
{
    // Class35 creates the MPATCHER launcher for both Build/Misc and Option tabs,
    // then hands the exact Control0 to MPatcherUpdaterUi.BindLauncher. Hide that
    // object there instead of patching the obfuscated creation helper.
    internal static class UnusedMpatcherLauncher
    {
        private static bool registered;

        internal static void TryRegister()
        {
            if (registered)
                return;

            registered = true;
            Log("REGISTERED source=MPatcherUpdaterUi.BindLauncher");
        }

        internal static void Hide(Control0 launcher)
        {
            if (!registered || launcher == null)
                return;

            GameObject button = launcher.gameObject;
            button.SetActive(false);
            Log("HIDDEN object=" + button.name + " scene="
                + UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }

        private static void Log(string message)
        {
            mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(
                "[UNUSED-MPATCHER-LAUNCHER] " + message);
        }
    }
}
