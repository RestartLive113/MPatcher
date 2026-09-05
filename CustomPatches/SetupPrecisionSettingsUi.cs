using UnityEngine;
using UnityEngine.UI;
using MPatchrMain;

namespace MPatcherFork.CustomPatches
{
    internal static class SetupPrecisionSettingsUi
    {
        internal const float RowWidth = 240f;
        internal const float ToggleWidth = 198f;
        internal const float GearWidth = 38f;
        internal const float Gap = 4f;

        private static Control0 slider;
        private static Control0 digitSpinner;
        private static Control0 hybrid;
        private static GameObject page;
        private static ToggleGroup modeGroup;
        private static bool syncing;

        internal static void CreateRow(Transform parent, Transform root, Vector3 position)
        {
            float toggleX = position.x - (GearWidth + Gap) * 0.5f;
            float gearX = position.x + (ToggleWidth + Gap) * 0.5f;
            Class35.smethod_40("Toggle_SetupPrecision", new Vector3(toggleX, position.y), "Setup Precision", parent,
                SetupPrecision.SetEnabled, new Vector2(ToggleWidth, 30f),
                MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.setupPrecision, reInit: false);

            lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw gear =
                Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.P3tLroX6fcPfQha_0024JdbpYXg(
                    "Button_SetupPrecisionSettings", new Vector3(gearX, position.y), "", delegate { OpenPage(); }, parent, 18);
            gear.UzVS61irgJn5Pnqwx0lThng(new Vector2(GearWidth, 30f));
            PatchSettingsPage.AddGearIcon(gear.transform);

            page = PatchSettingsPage.Create("SetupPrecision", "Setup Precision", root);
            modeGroup = page.AddComponent<ToggleGroup>();
            modeGroup.allowSwitchOff = false;
            slider = CreateModeToggle("Toggle_SetupPrecisionSlider", "Slider", new Vector3(0f, 160f), 0);
            digitSpinner = CreateModeToggle("Toggle_SetupPrecisionDigitSpinner", "Digit Spinner", new Vector3(0f, 120f), 1);
            hybrid = CreateModeToggle("Toggle_SetupPrecisionHybrid", "Slider+Digit Spinner", new Vector3(0f, 80f), 2);
            Sync();
            SetupPrecision.Log("SETTINGS_UI rowWidth=240 toggleWidth=198 gearWidth=38 gap=4 page=Setup Precision modes=Slider|Digit Spinner|Slider+Digit Spinner exclusive=true gear=generated-sprite mode="
                + SetupPrecision.EditorModeName);
        }

        private static Control0 CreateModeToggle(string name, string text, Vector3 position, int mode)
        {
            Control0 control = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.uEsWMK_pFkCY_0024M5zt8zLsQk(
                name, position, text, page.transform, resetGroup: true, onClick: delegate(bool toggled)
                {
                    if (syncing || !toggled) return;
                    SetupPrecision.SetEditorMode(mode);
                }, onUnToggle: null, group: modeGroup);
            control.UzVS61irgJn5Pnqwx0lThng(new Vector2(240f, 30f));
            return control;
        }

        private static void OpenPage()
        {
            if (page == null) return;
            Sync();
            PatchSettingsPage.Open(page, "SetupPrecision");
            SetupPrecision.Log("SETTINGS_PAGE opened mode=" + SetupPrecision.EditorModeName);
        }

        internal static void Sync()
        {
            if (slider == null || digitSpinner == null || hybrid == null || modeGroup == null) return;
            syncing = true;
            try
            {
                modeGroup.allowSwitchOff = true;
                slider.hLxnG9Hq33zU_YUsu_00240_zak = SetupPrecision.EditorMode == SetupPrecisionEditorMode.VanillaSlider;
                digitSpinner.hLxnG9Hq33zU_YUsu_00240_zak = SetupPrecision.EditorMode == SetupPrecisionEditorMode.DigitSpinner;
                hybrid.hLxnG9Hq33zU_YUsu_00240_zak = SetupPrecision.EditorMode == SetupPrecisionEditorMode.SliderDigitSpinner;
            }
            finally
            {
                modeGroup.allowSwitchOff = false;
                syncing = false;
            }
        }
    }
}
