using System;
using System.IO;
using System.Reflection;
using HarmonyLib;
using MPatchrMain;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MPatcherFork.CustomPatches
{
	// Restores MPatcher's .mcbd <-> .mzbd feature. Conversion is transactional:
	// the source survives until the temporary output has been decoded and checked.
	internal static class MachineCompression
	{
		private const string PatchId = "local.moddev.machinecraft.compression.v1";
		private const string ButtonObjectName = "BTN_MPCompression";
		private const string BuildExtension = ".mcbd";
		private const string CompressedExtension = ".mzbd";
		private const float ButtonBottomOffset = 14f;

		private static Harmony harmony;
		private static GameObject buttonObject;
		private static Button button;
		private static Text buttonText;
		private static Menu currentMenu;
		private static MethodInfo refreshListMethod;
		private static Button workshopButton;
		private static bool workshopStateBeforeCompression;
		private static bool workshopDisabledByCompression;
		private static int nextUiCreateAttemptFrame;

		internal static void TryRegister()
		{
			if (harmony != null)
				return;

			try
			{
				MethodInfo menuUpdate = AccessTools.Method(typeof(Menu), "Update", Type.EmptyTypes);
				MethodInfo loadBuild = AccessTools.Method(typeof(JKGKJLLFMLE), "JMDPCIBHEML", Type.EmptyTypes);
				MethodInfo saveBuild = AccessTools.Method(typeof(JKGKJLLFMLE), "BOMAFGLNGMI", Type.EmptyTypes);
				MethodInfo machineExists = AccessTools.Method(typeof(JKGKJLLFMLE), "MIHDENOOGLP", new Type[] { typeof(string), typeof(bool) });
				MethodInfo deleteMachine = AccessTools.Method(typeof(JKGKJLLFMLE), "JFAFKBHOOCE", Type.EmptyTypes);
				refreshListMethod = AccessTools.Method(typeof(SceneMan), "NNMOPNJABNE", new Type[] { typeof(string), typeof(string), typeof(string) });

				if (menuUpdate == null || loadBuild == null || saveBuild == null
					|| machineExists == null || deleteMachine == null || refreshListMethod == null)
				{
					throw new MissingMemberException("Compression targets");
				}

				harmony = new Harmony(PatchId);
				PatchPostfix(menuUpdate, "MenuUpdatePostfix");
				PatchPrefix(loadBuild, "LoadBuildPrefix");
				PatchPrefix(saveBuild, "SaveBuildPrefix");
				PatchPostfix(saveBuild, "SaveBuildPostfix");
				PatchPrefix(machineExists, "MachineExistsPrefix");
				PatchPrefix(deleteMachine, "DeleteMachinePrefix");
				PatchPostfix(deleteMachine, "DeleteMachinePostfix");
				LogRecoveredListContract();
				Log("REGISTERED codec=transactional loader=.mzbd ui=Menu.Update");
			}
			catch (Exception error)
			{
				Log("REGISTER_FAILED type=" + error.GetType().Name + " message=" + error.Message);
			}
		}

		private static void LogRecoveredListContract()
		{
			string first = global::_003CModule_003E.smethod_26<string>(2986582050u);
			string second = global::_003CModule_003E.smethod_26<string>(1822232483u);
			string italic = global::_003CModule_003E.smethod_27<string>(967689968u);
			bool hasBuild = string.Equals(first, BuildExtension, StringComparison.OrdinalIgnoreCase)
				|| string.Equals(second, BuildExtension, StringComparison.OrdinalIgnoreCase);
			bool hasCompressed = string.Equals(first, CompressedExtension, StringComparison.OrdinalIgnoreCase)
				|| string.Equals(second, CompressedExtension, StringComparison.OrdinalIgnoreCase);
			bool italicCompressed = string.Equals(italic, CompressedExtension, StringComparison.OrdinalIgnoreCase);
			Log("LIST_CONTRACT build=" + hasBuild + " compressed=" + hasCompressed + " italic=" + italicCompressed);
		}

		private static void MenuUpdatePostfix(Menu __instance)
		{
			try
			{
				if (currentMenu != __instance || buttonObject == null || button == null)
				{
					if (Time.frameCount < nextUiCreateAttemptFrame)
						return;
					nextUiCreateAttemptFrame = Time.frameCount + 120;
					currentMenu = __instance;
					CreateButton(__instance);
					Log("UI_LAZY_CREATE_OK frame=" + Time.frameCount);
				}
				UpdateButtonState(__instance);
			}
			catch (Exception error)
			{
				Log("UI_UPDATE_FAILED type=" + error.GetType().Name + " message=" + error.Message);
			}
		}

		private static void CreateButton(Menu menu)
		{
			RestoreWorkshopState();
			if (buttonObject != null)
				UnityEngine.Object.Destroy(buttonObject);

			GameObject reference = menu.GetBTN("Workshop") ?? menu.GetBTN("Copy") ?? menu.GetBTN("Delete");
			if (reference == null)
				throw new MissingMemberException("Menu reference button");

			Canvas canvas = reference.GetComponentInParent<Canvas>();
			Transform buttonParent = canvas != null ? canvas.transform : reference.transform.parent;
			buttonObject = UnityEngine.Object.Instantiate(reference, buttonParent, false);
			buttonObject.name = ButtonObjectName;
			RectTransform buttonRect = buttonObject.GetComponent<RectTransform>();
			if (canvas != null && buttonRect != null)
			{
				buttonRect.anchorMin = new Vector2(0.5f, 0f);
				buttonRect.anchorMax = new Vector2(0.5f, 0f);
				buttonRect.pivot = new Vector2(0.5f, 0f);
				buttonRect.anchoredPosition = new Vector2(0f, ButtonBottomOffset);
				buttonRect.localRotation = Quaternion.identity;
				buttonRect.localScale = Vector3.one;
				buttonRect.SetAsLastSibling();
			}
			else
			{
				buttonObject.transform.localPosition = reference.transform.localPosition + new Vector3(280f, 0f, 0f);
				buttonObject.transform.localScale = reference.transform.localScale;
			}

			ButtonController originalController = buttonObject.GetComponent<ButtonController>();
			if (originalController != null)
				UnityEngine.Object.Destroy(originalController);

			button = buttonObject.GetComponent<Button>();
			if (button == null)
				throw new MissingComponentException("Compression Button");

			button.onClick.RemoveAllListeners();
			button.onClick.AddListener(new UnityAction(ToggleSelectedMachine));
			buttonText = FindButtonText(buttonObject.transform);
			Log("UI_CREATED reference=" + reference.name + " parent=" + buttonParent.name
				+ " anchored=" + (buttonRect != null ? buttonRect.anchoredPosition.ToString() : "n/a"));
		}

		private static Text FindButtonText(Transform root)
		{
			Transform named = root.Find("Text");
			if (named != null)
			{
				Text direct = named.GetComponent<Text>();
				if (direct != null)
					return direct;
			}
			return root.GetComponentInChildren<Text>(true);
		}

		private static void UpdateButtonState(Menu menu)
		{
			if (buttonObject == null || button == null)
				return;

			string buildPath = GetSelectedPath(BuildExtension);
			string compressedPath = GetSelectedPath(CompressedExtension);
			bool hasBuild = buildPath != null && File.Exists(buildPath);
			bool hasCompressed = compressedPath != null && File.Exists(compressedPath);
			bool enabled = IsEnabled && !string.IsNullOrEmpty(GetSelectedMachineName());

			buttonObject.SetActive(enabled);
			button.interactable = enabled && hasBuild != hasCompressed;
			if (buttonText != null)
				buttonText.text = hasCompressed && !hasBuild ? "DECOMPRESS" : "COMPRESS";

			GameObject workshop = menu.GetBTN("Workshop");
			Button currentWorkshopButton = workshop != null ? workshop.GetComponent<Button>() : null;
			if (currentWorkshopButton != workshopButton)
			{
				RestoreWorkshopState();
				workshopButton = currentWorkshopButton;
			}
			if (workshopButton != null && enabled && hasCompressed && !hasBuild)
			{
				if (!workshopDisabledByCompression)
				{
					workshopStateBeforeCompression = workshopButton.interactable;
					workshopDisabledByCompression = true;
				}
				workshopButton.interactable = false;
			}
			else
			{
				RestoreWorkshopState();
			}
		}

		private static void RestoreWorkshopState()
		{
			if (workshopDisabledByCompression && workshopButton != null)
				workshopButton.interactable = workshopStateBeforeCompression;
			workshopDisabledByCompression = false;
		}

		private static bool LoadBuildPrefix(ref bool __result)
		{
			if (!IsEnabled)
				return true;

			string normalPath = GetSelectedPath(BuildExtension, true);
			string compressedPath = GetSelectedPath(CompressedExtension, true);
			if (normalPath == null || compressedPath == null || File.Exists(normalPath) || !File.Exists(compressedPath))
				return true;

			try
			{
				BuildData build = Decode(File.ReadAllBytes(compressedPath));
				if (build == null)
					throw new InvalidDataException("Decoder returned null");
				build.CorrectVersion();
				JKGKJLLFMLE.HHGILAIOCLG = build;
				JKGKJLLFMLE.IGOBPLOLHEP.folderName = JKGKJLLFMLE.AEOGMEAKNOL;
				__result = true;
				Log("LOAD_OK path=" + compressedPath + " blocks=" + build.blockData.Count);
			}
			catch (Exception error)
			{
				__result = false;
				Log("LOAD_FAILED path=" + compressedPath + " type=" + error.GetType().Name + " message=" + error.Message);
			}
			return false;
		}

		private static void SaveBuildPrefix(ref string __state)
		{
			__state = IsEnabled ? GetSelectedPath(CompressedExtension) : null;
		}

		private static void SaveBuildPostfix(string __state)
		{
			if (__state == null || !File.Exists(__state))
				return;

			try
			{
				string buildPath = Path.ChangeExtension(__state, BuildExtension);
				if (!File.Exists(buildPath))
					return;
				File.Delete(__state);
				Log("SAVE_REPLACED_COMPRESSED build=" + buildPath + " removed=" + __state);
			}
			catch (Exception error)
			{
				Log("SAVE_CLEANUP_FAILED type=" + error.GetType().Name + " message=" + error.Message);
			}
		}

		private static bool MachineExistsPrefix(string CBNCLLHJONG, bool KOEKDPFICBJ, ref bool __result)
		{
			if (!IsEnabled || string.IsNullOrEmpty(CBNCLLHJONG))
				return true;

			string folder = KOEKDPFICBJ ? string.Empty : GetSelectedFolderName();
			string path = GetPath(folder, CBNCLLHJONG, CompressedExtension);
			if (!File.Exists(path))
				return true;

			__result = true;
			return false;
		}

		private static void DeleteMachinePrefix(ref string __state)
		{
			__state = IsEnabled ? GetSelectedPath(CompressedExtension) : null;
		}

		private static void DeleteMachinePostfix(string __state)
		{
			try
			{
				if (__state != null && File.Exists(__state))
				{
					File.Delete(__state);
					Log("DELETE_OK path=" + __state);
				}
			}
			catch (Exception error)
			{
				Log("DELETE_FAILED type=" + error.GetType().Name + " message=" + error.Message);
			}
		}

		private static void ToggleSelectedMachine()
		{
			string buildPath = GetSelectedPath(BuildExtension);
			string compressedPath = GetSelectedPath(CompressedExtension);
			try
			{
				if (buildPath == null || compressedPath == null)
					throw new InvalidOperationException("No selected machine");
				if (File.Exists(buildPath) && File.Exists(compressedPath))
					throw new IOException("Both .mcbd and .mzbd exist; refusing to overwrite either file");

				if (File.Exists(buildPath))
					Compress(buildPath, compressedPath);
				else if (File.Exists(compressedPath))
					Decompress(compressedPath, buildPath);
				else
					throw new FileNotFoundException("Selected machine has no .mcbd or .mzbd file");

				RefreshList();
				UpdateButtonState(currentMenu);
			}
			catch (Exception error)
			{
				Log("TOGGLE_FAILED type=" + error.GetType().Name + " message=" + error.Message);
			}
		}

		internal static void Compress(string sourcePath, string targetPath)
		{
			string tempPath = targetPath + ".tmp";
			DeleteTemp(tempPath);
			try
			{
				string sourceJson = File.ReadAllText(sourcePath);
				BuildData source = LNGKNOGOIKL.FMAGAEMFION<BuildData>(sourceJson);
				if (source == null)
					throw new InvalidDataException("Could not parse .mcbd");

				byte[] compressed = Encode(source);
				File.WriteAllBytes(tempPath, compressed);
				BuildData verified = Decode(File.ReadAllBytes(tempPath));
				VerifyEquivalent(source, verified);

				File.Move(tempPath, targetPath);
				File.Delete(sourcePath);
				Log("COMPRESS_OK from=" + sourcePath + " to=" + targetPath
					+ " sourceBytes=" + sourceJson.Length + " compressedBytes=" + compressed.Length);
			}
			catch
			{
				DeleteTemp(tempPath);
				throw;
			}
		}

		internal static void Decompress(string sourcePath, string targetPath)
		{
			string tempPath = targetPath + ".tmp";
			long compressedBytes = new FileInfo(sourcePath).Length;
			DeleteTemp(tempPath);
			try
			{
				BuildData source = Decode(File.ReadAllBytes(sourcePath));
				if (source == null)
					throw new InvalidDataException("Could not decode .mzbd");

				string json = LNGKNOGOIKL.AOPMIBBFLKH(source);
				File.WriteAllText(tempPath, json);
				BuildData verified = LNGKNOGOIKL.FMAGAEMFION<BuildData>(File.ReadAllText(tempPath));
				VerifyEquivalent(source, verified);

				File.Move(tempPath, targetPath);
				File.Delete(sourcePath);
				Log("DECOMPRESS_OK from=" + sourcePath + " to=" + targetPath
					+ " compressedBytes=" + compressedBytes + " jsonChars=" + json.Length);
			}
			catch
			{
				DeleteTemp(tempPath);
				throw;
			}
		}

		private static void VerifyEquivalent(BuildData expected, BuildData actual)
		{
			if (expected == null || actual == null)
				throw new InvalidDataException("Round-trip returned null BuildData");

			string expectedJson = LNGKNOGOIKL.AOPMIBBFLKH(expected);
			string actualJson = LNGKNOGOIKL.AOPMIBBFLKH(actual);
			if (!string.Equals(expectedJson, actualJson, StringComparison.Ordinal))
				throw new InvalidDataException("Round-trip BuildData mismatch");
		}

		private static byte[] Encode(BuildData build)
		{
			return KjZ6k5EOvx8ehHa8KpN7D9CVL2r1svVmATmUt3PKPSEN2gwgw888a55kuCgtdRKS2Q.TeFH4ifj99LlUm32GP4kcVk(build);
		}

		private static BuildData Decode(byte[] bytes)
		{
			return KjZ6k5EOvx8ehHa8KpN7D9CVL2r1svVmATmUt3PKPSEN2gwgw888a55kuCgtdRKS2Q.smethod_0(bytes);
		}

		private static void RefreshList()
		{
			if (currentMenu == null || refreshListMethod == null)
				return;
			refreshListMethod.Invoke(currentMenu, new object[]
			{
				GetSelectedFolderName(),
				GetSelectedMachineName(),
				null
			});
		}

		private static string GetSelectedPath(string extension, bool useActiveLoadFolder = false)
		{
			string machine = GetSelectedMachineName();
			if (string.IsNullOrEmpty(machine))
				return null;
			string folder = useActiveLoadFolder ? JKGKJLLFMLE.AEOGMEAKNOL : GetSelectedFolderName();
			return GetPath(folder, machine, extension);
		}

		private static string GetPath(string folder, string machine, string extension)
		{
			string directory = string.IsNullOrEmpty(folder)
				? JKGKJLLFMLE.LAOHLAOMCPN
				: Path.Combine(JKGKJLLFMLE.LAOHLAOMCPN, folder);
			return Path.Combine(directory, machine + extension);
		}

		private static string GetSelectedFolderName()
		{
			if (JKGKJLLFMLE.IGOBPLOLHEP == null)
				return string.Empty;
			return JKGKJLLFMLE.IGOBPLOLHEP.folderName ?? string.Empty;
		}

		private static string GetSelectedMachineName()
		{
			if (JKGKJLLFMLE.IGOBPLOLHEP == null)
				return string.Empty;
			return JKGKJLLFMLE.IGOBPLOLHEP.machineName ?? string.Empty;
		}

		private static bool IsEnabled
		{
			get
			{
				return MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68 != null
					&& MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.compression;
			}
		}

		private static void DeleteTemp(string path)
		{
			if (File.Exists(path))
				File.Delete(path);
		}

		private static void PatchPrefix(MethodInfo original, string methodName)
		{
			MethodInfo patch = AccessTools.Method(typeof(MachineCompression), methodName);
			if (patch == null)
				throw new MissingMethodException(methodName);
			HarmonyMethod prefix = new HarmonyMethod(patch);
			prefix.priority = Priority.First;
			harmony.Patch(original, prefix, null, null, null);
		}

		private static void PatchPostfix(MethodInfo original, string methodName)
		{
			MethodInfo patch = AccessTools.Method(typeof(MachineCompression), methodName);
			if (patch == null)
				throw new MissingMethodException(methodName);
			harmony.Patch(original, null, new HarmonyMethod(patch), null, null);
		}

		private static void Log(string message)
		{
			string text = "[COMPRESSION] " + message;
			try { global::mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(text); }
			catch (Exception) { }
		}
	}
}
