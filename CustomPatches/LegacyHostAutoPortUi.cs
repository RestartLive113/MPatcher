using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MPatcherFork.CustomPatches
{
    // Startup feedback lives only on Configure, never on the game screen.
    internal sealed class LegacyHostAutoPortUi
    {
        private GameObject root;
        private Text status;
        private Button skip;
        internal static LegacyHostAutoPortUi Create(Configure owner, LegacyHostAutoPortDriver driver)
        {
            GameObject reference = owner.GetBTN("Start");
            Canvas canvas = reference == null ? null : reference.GetComponentInParent<Canvas>();
            if (canvas == null) throw new MissingComponentException("Configure StartServer canvas");
            LegacyHostAutoPortUi ui = new LegacyHostAutoPortUi();
            ui.root = new GameObject("MPatcher_HostPortSelection");
            RectTransform cover = ui.root.AddComponent<RectTransform>(); cover.SetParent(canvas.transform, false);
            cover.anchorMin = Vector2.zero; cover.anchorMax = Vector2.one; cover.offsetMin = cover.offsetMax = Vector2.zero;
            cover.SetAsLastSibling();
            Image shade = ui.root.AddComponent<Image>(); shade.color = new Color(0, 0, 0, 0.9f); shade.raycastTarget = true;
            Text nativeText = reference.GetComponentInChildren<Text>(true);
            ui.status = MakeText(cover, nativeText, new Vector2(0, 35), new Vector2(480, 160), "Checking host publication...");
            MakeButton(cover, reference, new Vector2(-125, -80), "Cancel", delegate { driver.Cancel("user cancelled"); });
            ui.skip = MakeButton(cover, reference, new Vector2(125, -80), "Start without check", delegate { driver.Commit(false); });
            ui.skip.interactable = false;
            LegacyHostAutoPort.Log("UI_CREATED reference=" + reference.name + " scope=Configure");
            return ui;
        }
        private static Text MakeText(Transform parent, Text source, Vector2 position, Vector2 size, string caption)
        {
            GameObject item = new GameObject("Label"); RectTransform rect = item.AddComponent<RectTransform>();
            rect.SetParent(parent, false); rect.anchoredPosition = position; rect.sizeDelta = size;
            Text text = item.AddComponent<Text>(); text.font = source.font; text.fontSize = 15;
            text.color = Color.white; text.alignment = TextAnchor.MiddleCenter; text.raycastTarget = false; text.text = caption;
            return text;
        }
        private static Button MakeButton(Transform parent, GameObject source, Vector2 position, string caption, UnityAction action)
        {
            GameObject item = new GameObject(caption); RectTransform rect = item.AddComponent<RectTransform>();
            rect.SetParent(parent, false); rect.anchoredPosition = position; rect.sizeDelta = new Vector2(220, 36);
            Image image = item.AddComponent<Image>(); Image nativeImage = source.GetComponent<Image>();
            if (nativeImage != null) { image.sprite = nativeImage.sprite; image.type = nativeImage.type; image.color = nativeImage.color; }
            Button button = item.AddComponent<Button>(); Button nativeButton = source.GetComponent<Button>();
            button.targetGraphic = image; if (nativeButton != null) button.colors = nativeButton.colors;
            button.onClick.AddListener(action);
            MakeText(rect, source.GetComponentInChildren<Text>(true), Vector2.zero, new Vector2(215, 32), caption);
            return button;
        }
        internal void Refresh(LegacyHostAutoPortDriver driver)
        {
            if (root == null) return;
            LegacyHostPortPolicy policy = driver.Policy;
            bool exhausted = policy.Phase == LegacyHostPortPhase.Exhausted;
            status.text = exhausted
                ? "Host publication could not be confirmed.\nCancel to return to Host settings and try again.\nA timeout does not prove that a port is blocked."
                : "Checking host publication\nPort " + policy.Port + "  (" + policy.Attempt + "/" + policy.Count + ")"
                + "\nFresh listings: " + policy.PositiveReplies + "/3"
                + (policy.Evidence == LegacyHostPortEvidence.Unknown ? "\nWaiting for the catalogue..." : "\nChecking the connection over time...")
                + "\nPlayers can join after this check.\nStarting without a check does not confirm visibility.";
            skip.interactable = driver.CanStartUnchecked;
        }
        internal void Close() { if (root != null) { root.SetActive(false); UnityEngine.Object.Destroy(root); root = null; } }
    }
}
