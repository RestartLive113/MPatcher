using System;
using System.Reflection;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;

namespace MPatcherFork.CustomPatches
{
	internal static class MainMenuVersionLabel
	{
		private const string PatchId = "MPatcherFork.MainMenuVersionLabel";
		private const string GameVersionObjectName = "Version";
		private const string LabelObjectName = "MPatcherVersion";
		private const float VerticalOffset = 23f;
		private static Harmony harmony;

		internal static void TryRegister()
		{
			if (harmony != null)
				return;
			try
			{
				MethodInfo target = AccessTools.Method(typeof(Menu), "Start", Type.EmptyTypes);
				MethodInfo postfix = AccessTools.Method(typeof(MainMenuVersionLabel), "MenuStartPostfix");
				if (target == null || postfix == null)
					throw new MissingMethodException("Menu.Start");

				harmony = new Harmony(PatchId);
				harmony.Patch(target, null, new HarmonyMethod(postfix), null, null);
				Log("REGISTERED target=Menu.Start source=" + GameVersionObjectName + " offsetY=" + VerticalOffset);
			}
			catch (Exception error)
			{
				Log("REGISTER_FAILED type=" + error.GetType().Name + " message=" + error.Message);
			}
		}

		private static void MenuStartPostfix(Menu __instance)
		{
			try
			{
				Text gameVersionText = FindGameVersionText(__instance);
				if (gameVersionText == null)
				{
					Log("CREATE_SKIPPED reason=game-version-text-missing");
					return;
				}

				GameObject gameVersionObject = gameVersionText.gameObject;
				RectTransform gameVersionRect = gameVersionObject.transform as RectTransform;
				Transform parent = gameVersionObject.transform.parent;
				if (gameVersionRect == null || parent == null)
				{
					Log("CREATE_SKIPPED reason=invalid-game-version-template");
					return;
				}

				Transform existing = parent.Find(LabelObjectName);
				GameObject labelObject;
				if (existing != null)
				{
					labelObject = existing.gameObject;
				}
				else
				{
					labelObject = UnityEngine.Object.Instantiate(gameVersionObject, parent, false);
					labelObject.name = LabelObjectName;
				}

				RectTransform labelRect = labelObject.transform as RectTransform;
				Text labelText = labelObject.GetComponent<Text>();
				if (labelRect == null || labelText == null)
				{
					UnityEngine.Object.Destroy(labelObject);
					Log("CREATE_FAILED reason=cloned-label-components-missing");
					return;
				}

				labelRect.anchorMin = gameVersionRect.anchorMin;
				labelRect.anchorMax = gameVersionRect.anchorMax;
				labelRect.pivot = gameVersionRect.pivot;
				labelRect.sizeDelta = gameVersionRect.sizeDelta;
				labelRect.anchoredPosition = gameVersionRect.anchoredPosition + new Vector2(0f, VerticalOffset);
				labelRect.localScale = gameVersionRect.localScale;
				labelRect.localRotation = gameVersionRect.localRotation;
				labelText.text = "MP Ver." + ReadPatcherVersion();
				labelText.raycastTarget = false;
				labelObject.SetActive(true);
				labelObject.transform.SetSiblingIndex(gameVersionObject.transform.GetSiblingIndex() + 1);
				Log("CREATED text=\"" + labelText.text + "\" sourceText=\"" + gameVersionText.text
					+ "\" offsetY=" + VerticalOffset + " fontSize=" + labelText.fontSize);
			}
			catch (Exception error)
			{
				Log("CREATE_FAILED type=" + error.GetType().Name + " message=" + error.Message);
			}
		}

		private static string ReadPatcherVersion()
		{
			object[] attributes = typeof(MainMenuVersionLabel).Assembly.GetCustomAttributes(
				typeof(AssemblyInformationalVersionAttribute), false);
			if (attributes.Length == 1)
			{
				string version = ((AssemblyInformationalVersionAttribute)attributes[0]).InformationalVersion;
				if (!string.IsNullOrEmpty(version))
					return version;
			}
			return MPatcherUpdater.CurrentVersion;
		}

		private static Text FindGameVersionText(Menu menu)
		{
			Text byText = null;
			if (menu != null)
			{
				Text[] menuTexts = menu.GetComponentsInChildren<Text>(true);
				for (int i = 0; i < menuTexts.Length; i++)
				{
					Text candidate = menuTexts[i];
					if (candidate == null)
						continue;
					if (string.Equals(candidate.name, GameVersionObjectName, StringComparison.Ordinal))
						return candidate;
					if (candidate.text != null && candidate.text.IndexOf("Ver.0.248c", StringComparison.Ordinal) >= 0)
						byText = candidate;
				}
			}
			if (byText != null)
				return byText;

			Text[] allTexts = Resources.FindObjectsOfTypeAll<Text>();
			for (int i = 0; i < allTexts.Length; i++)
			{
				Text candidate = allTexts[i];
				if (candidate == null)
					continue;
				if (string.Equals(candidate.name, GameVersionObjectName, StringComparison.Ordinal)
					|| (candidate.text != null && candidate.text.IndexOf("Ver.0.248c", StringComparison.Ordinal) >= 0))
					return candidate;
			}
			return null;
		}

		private static void Log(string message)
		{
			mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(
				"[MAIN-MENU-VERSION] " + message);
		}
	}
}
