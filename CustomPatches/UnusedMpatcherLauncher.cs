using UnityEngine;

namespace MPatcherFork.CustomPatches
{
    // Class35 reuses the scene's toggle template for this launcher. The native
    // Option scene owns a working settings tab; foreign scenes use a Build/Misc
    // template and do not own the native Key/KeyEx settings groups.
    internal static class UnusedMpatcherLauncher
    {
        private static bool registered;

        internal static void TryRegister()
        {
            if (registered)
                return;

            registered = true;
            Log("REGISTERED source=MPatcherUpdaterUi.BindLauncher preserve=Option-settings-tab");
        }

        internal static void Hide(Control0 launcher)
        {
            if (!registered || launcher == null)
                return;

            GameObject button = launcher.gameObject;
            string scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
            if (scene == "Option" && SceneMan.JFAOKFIDAGK is Option)
            {
                button.SetActive(true);
                Log("OPTIONS_TAB_VISIBLE object=" + button.name + " scene=" + scene);
                return;
            }
            button.SetActive(false);
            Log("HIDDEN object=" + button.name + " scene=" + scene);
        }

        private static void Log(string message)
        {
            mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(
                "[UNUSED-MPATCHER-LAUNCHER] " + message);
        }
    }
}
