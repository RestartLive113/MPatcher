using System;
using MPatchrMain;
using UnityEngine;
using UnityEngine.UI;

namespace MPatcherFork.CustomPatches
{
    internal sealed class MagnificationPhysicsModeUi : MonoBehaviour
    {
        private static MagnificationPhysicsModeUi runner;
        private Build build;
        private Toggle toggle;
        private mcpd boundData;
        private bool suppress;
        private float retryAt;

        internal static void Start()
        {
            if (runner != null) return;
            GameObject host = new GameObject("MPatcher_MagnificationPhysicsModeUi");
            host.hideFlags = HideFlags.HideAndDontSave;
            DontDestroyOnLoad(host);
            runner = host.AddComponent<MagnificationPhysicsModeUi>();
        }

        private void Update()
        {
            Build current = build;
            if (current == null || !current.isActiveAndEnabled) current = UnityEngine.Object.FindObjectOfType<Build>();
            if (current == null)
            {
                build = null;
                toggle = null;
                boundData = null;
                return;
            }
            if (!ReferenceEquals(current, build))
            {
                build = current;
                toggle = null;
                boundData = null;
                retryAt = 0f;
            }
            if (toggle == null && Time.realtimeSinceStartup >= retryAt)
            {
                retryAt = Time.realtimeSinceStartup + 1f;
                TryCreate();
            }
            // The runner remains active when the native widget is hidden. The
            // policy also applies before the Misc panel has created its controls.
            BindCurrentMachine();
        }

        private void TryCreate()
        {
            GameObject slider = build.GetSLD("Mag");
            GameObject template = build.GetTGL("Takeoff");
            if (slider == null || template == null) return;
            RectTransform sliderRect = slider.GetComponent<RectTransform>();
            RectTransform templateRect = template.GetComponent<RectTransform>();
            RectTransform groupRect = slider.transform.parent as RectTransform;
            RectTransform panelRect = groupRect == null ? null : groupRect.parent as RectTransform;
            RectTransform orientationRect = panelRect == null ? null : panelRect.Find("TGL_Orient") as RectTransform;
            RectTransform invertAimRect = panelRect == null ? null : panelRect.Find("TGL_InvAimX") as RectTransform;
            if (sliderRect == null || templateRect == null || panelRect == null
                || orientationRect == null || invertAimRect == null
                || template.GetComponent<Toggle>() == null) return;

            // The native Misc panel leaves a full row between Invert Aim and
            // ScopeOrientation. Use that row without moving either native control.
            float upperBottom = invertAimRect.anchoredPosition.y
                - invertAimRect.rect.height * invertAimRect.pivot.y;
            float lowerTop = orientationRect.anchoredPosition.y
                + orientationRect.rect.height * (1f - orientationRect.pivot.y);
            float toggleHeight = templateRect.rect.height;
            if (upperBottom - lowerTop < toggleHeight + 12f)
            {
                MagnificationDownscale.Log("PHYSICS_UI_NO_SPACE upperBottom=" + upperBottom
                    + " lowerTop=" + lowerTop + " toggleHeight=" + toggleHeight);
                return;
            }

            GameObject widget = Instantiate(template);
            widget.name = "TGL_MPatchrMagPhysics";
            widget.SetActive(false);
            widget.transform.SetParent(panelRect, false);
            ToggleController nativeController = widget.GetComponent<ToggleController>();
            if (nativeController != null) { nativeController.enabled = false; Destroy(nativeController); }
            toggle = widget.GetComponent<Toggle>();
            toggle.onValueChanged = new Toggle.ToggleEvent();
            toggle.onValueChanged.AddListener(OnChanged);
            Text label = widget.transform.Find("Label") == null ? null : widget.transform.Find("Label").GetComponent<Text>();
            if (label != null) label.text = HelpDefs.isJ ? "物理倍率" : "Scale physics";
            RectTransform rect = widget.GetComponent<RectTransform>();
            rect.anchorMin = orientationRect.anchorMin;
            rect.anchorMax = orientationRect.anchorMax;
            rect.pivot = orientationRect.pivot;
            rect.sizeDelta = new Vector2(sliderRect.rect.width > 0f ? sliderRect.rect.width : 160f,
                toggleHeight);
            rect.anchoredPosition = new Vector2(orientationRect.anchoredPosition.x,
                (upperBottom + lowerTop) * 0.5f);
            boundData = null;
            BindCurrentMachine();
            MagnificationDownscale.Log("PHYSICS_UI_READY slider=" + sliderRect.anchoredPosition
                + " sliderSize=" + sliderRect.rect.size + " toggle=" + rect.anchoredPosition
                + " toggleSize=" + rect.rect.size + " upperBottom=" + upperBottom
                + " lowerTop=" + lowerTop + " parent=" + panelRect.name);
        }

        private void BindCurrentMachine()
        {
            mcpd data = MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4;
            bool selected = Build.GFJLEEJELOL != null && JKGKJLLFMLE.IGOBPLOLHEP != null
                && !string.IsNullOrEmpty(JKGKJLLFMLE.IGOBPLOLHEP.machineName);
            bool nativePercent = selected && MagnificationPhysicsMode.IsIdentityPercent(Build.GFJLEEJELOL.magnification);
            if (nativePercent && (data == null || data.magnificationPhysics))
            {
                if (data == null) MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4 = data = new mcpd();
                // Commit OFF in memory before attempting disk persistence. A
                // failed write must not leave hidden physics enabled or retry
                // the same write on every frame.
                data.magnificationPhysics = false;
                SaveCurrentData(data, false, "identity-percent");
            }
            if (toggle == null) return;
            bool visible = selected && !nativePercent;
            bool value = visible && (data == null || data.magnificationPhysics);
            bool changed = !ReferenceEquals(data, boundData) || toggle.isOn != value
                || toggle.interactable != visible || toggle.gameObject.activeSelf != visible;
            suppress = true;
            try
            {
                toggle.interactable = visible;
                toggle.isOn = value;
                if (toggle.gameObject.activeSelf != visible) toggle.gameObject.SetActive(visible);
            }
            finally { suppress = false; }
            boundData = data;
            if (changed) MagnificationDownscale.Log("PHYSICS_UI_BIND machine="
                + (selected ? JKGKJLLFMLE.IGOBPLOLHEP.machineName : "<none>") + " apply=" + value
                + " visible=" + visible + " percent=" + (selected ? Build.GFJLEEJELOL.magnification : 0)
                + " policy=100-native-off");
        }

        private void OnChanged(bool apply)
        {
            if (suppress) return;
            if (Build.GFJLEEJELOL == null || JKGKJLLFMLE.IGOBPLOLHEP == null
                || string.IsNullOrEmpty(JKGKJLLFMLE.IGOBPLOLHEP.machineName)
                || MagnificationPhysicsMode.IsIdentityPercent(Build.GFJLEEJELOL.magnification))
            {
                // A stale or programmatic callback cannot enable the hidden
                // control after a slider change or machine switch.
                BindCurrentMachine();
                return;
            }
            mcpd data = MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4;
            bool created = data == null;
            if (data == null) MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4 = data = new mcpd();
            if (created || data.magnificationPhysics != apply)
            {
                data.magnificationPhysics = apply;
                SaveCurrentData(data, apply, "user");
            }
            BindCurrentMachine();
        }

        private void SaveCurrentData(mcpd data, bool apply, string source)
        {
            try
            {
                data.UUiRNMwxRbfk_Fs4cDErRoM();
                boundData = data;
                MagnificationDownscale.Log("PHYSICS_UI_SAVED machine=" + JKGKJLLFMLE.IGOBPLOLHEP.machineName
                    + " apply=" + apply + " percent=" + Build.GFJLEEJELOL.magnification
                    + " source=" + source
                    + " mode=" + (apply ? "magnification" : "native")
                    + " activation=new-machine-initialize independentSettings=preserved");
            }
            catch (Exception error)
            {
                MagnificationDownscale.Log("PHYSICS_UI_SAVE_FAILED machine=" + JKGKJLLFMLE.IGOBPLOLHEP.machineName
                    + " apply=" + apply + " percent=" + Build.GFJLEEJELOL.magnification
                    + " source=" + source + " persisted=false memoryApply=" + data.magnificationPhysics
                    + " " + error);
            }
        }
    }
}
