using System;
using MPatchrMain;
using UnityEngine;
using UnityEngine.UI;

namespace MPatcherFork.CustomPatches
{
    internal static class PatchSettingsPage
    {
        internal const float HeaderY = 245f;
        internal const float BackX = -220f;
        internal const float GearIconSize = 20f;

        private static GameObject activePage;
        private static string activeId;
        private static int returnPage;
        private static Sprite gearSprite;

        internal static GameObject Create(string id, string title, Transform root)
        {
            if (root == null) throw new ArgumentNullException("root");
            GameObject page = new GameObject("Panel_PatchSettings_" + id);
            RectTransform rect = page.AddComponent<RectTransform>();
            rect.SetParent(root, false);
            rect.localPosition = Vector3.zero;
            rect.localScale = Vector3.one;
            rect.sizeDelta = Class35.vector2_0;

            lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw back =
                Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.P3tLroX6fcPfQha_0024JdbpYXg(
                    "Button_" + id + "SettingsBack", new Vector3(BackX, HeaderY), "Back", delegate { Close(); }, page.transform, 14);
            back.UzVS61irgJn5Pnqwx0lThng(new Vector2(70f, 30f));
            Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.YU_pwpP3pKH76IHZTea_SXk(
                "Label_" + id + "SettingsTitle", new Vector3(0f, HeaderY), title, page.transform,
                rmOutline: false, fontSize: 18, style: FontStyle.Bold, alignment: TextAnchor.MiddleCenter,
                textColor: Color.white, resizeRect: true, resizeRectTo: new Vector2(330f, 35f));
            page.SetActive(false);
            Log("CREATE id=" + id + " title=" + title);
            return page;
        }

        internal static void Open(GameObject page, string id)
        {
            if (page == null) return;
            if (activePage != null && activePage != page) activePage.SetActive(false);
            returnPage = Class35.int_0;
            SetPatchPages(false);
            SetPageButtons(false);
            activePage = page;
            activeId = id;
            page.SetActive(true);
            page.transform.SetAsLastSibling();
            Log("OPEN id=" + id + " returnPage=" + (returnPage + 1));
        }

        internal static void Close()
        {
            if (activePage == null && string.IsNullOrEmpty(activeId)) return;
            string closedId = activeId;
            if (activePage != null) activePage.SetActive(false);
            activePage = null;
            activeId = null;

            int count = Class35.list_0.Count;
            int selected = count == 0 ? 0 : Mathf.Clamp(returnPage, 0, count - 1);
            Class35.int_0 = selected;
            for (int index = 0; index < count; index++)
            {
                Transform patchPage = Class35.list_0[index];
                if (patchPage != null) patchPage.gameObject.SetActive(index == selected);
            }
            for (int index = 0; index < Class35.JqKDtyiFnJcdoNFYMikviGo.Count; index++)
            {
                Control0 button = Class35.JqKDtyiFnJcdoNFYMikviGo[index];
                if (button == null) continue;
                button.gameObject.SetActive(true);
                button.hLxnG9Hq33zU_YUsu_00240_zak = index == selected;
            }
            Log("CLOSE id=" + closedId + " restorePage=" + (selected + 1));
        }

        private static void SetPatchPages(bool visible)
        {
            for (int index = 0; index < Class35.list_0.Count; index++)
            {
                Transform patchPage = Class35.list_0[index];
                if (patchPage != null) patchPage.gameObject.SetActive(visible && index == Class35.int_0);
            }
        }

        private static void SetPageButtons(bool visible)
        {
            for (int index = 0; index < Class35.JqKDtyiFnJcdoNFYMikviGo.Count; index++)
            {
                Control0 button = Class35.JqKDtyiFnJcdoNFYMikviGo[index];
                if (button != null) button.gameObject.SetActive(visible);
            }
        }

        internal static void AddGearIcon(Transform button)
        {
            if (button == null) return;
            GameObject icon = new GameObject("Icon_Gear");
            RectTransform rect = icon.AddComponent<RectTransform>();
            rect.SetParent(button, false);
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.anchoredPosition = Vector2.zero;
            rect.sizeDelta = new Vector2(GearIconSize, GearIconSize);
            Image image = icon.AddComponent<Image>();
            image.sprite = GetGearSprite();
            image.color = Color.white;
            image.raycastTarget = false;
        }

        private static Sprite GetGearSprite()
        {
            if (gearSprite != null) return gearSprite;
            const int size = 32;
            Texture2D texture = new Texture2D(size, size, TextureFormat.ARGB32, false);
            texture.name = "MPatcher_GearIcon";
            texture.filterMode = FilterMode.Bilinear;
            texture.wrapMode = TextureWrapMode.Clamp;
            Color32[] pixels = new Color32[size * size];
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    double dx = x + 0.5 - size * 0.5;
                    double dy = y + 0.5 - size * 0.5;
                    double radius = Math.Sqrt(dx * dx + dy * dy);
                    double angle = Math.Atan2(dy, dx);
                    double outer = Math.Cos(angle * 8.0) > 0.25 ? 14.5 : 11.75;
                    bool opaque = radius >= 4.25 && radius <= outer;
                    pixels[y * size + x] = opaque
                        ? new Color32(255, 255, 255, 255)
                        : new Color32(255, 255, 255, 0);
                }
            }
            texture.SetPixels32(pixels);
            texture.Apply();
            gearSprite = Sprite.Create(texture, new Rect(0f, 0f, size, size), new Vector2(0.5f, 0.5f), size);
            gearSprite.name = "MPatcher_GearIcon";
            return gearSprite;
        }

        private static void Log(string message)
        {
            mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(
                "[MPatcher.Settings.Page] " + message);
        }
    }
}
