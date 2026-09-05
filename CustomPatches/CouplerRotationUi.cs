using System;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;

namespace MPatcherFork.CustomPatches
{
	internal static class CouplerRotationUi
	{
		private sealed class AxisSelector : Dropdown
		{
			private GameObject visibleList;

			internal void CloseIfOpen()
			{
				if (visibleList != null)
					Hide();
			}

			protected override GameObject CreateDropdownList(GameObject template)
			{
				visibleList = base.CreateDropdownList(template);
				CouplerRotation.Log("UI_LIST_OPEN choices=" + string.Join(",", options.ConvertAll(option => option.text).ToArray()));
				return visibleList;
			}

			protected override void DestroyDropdownList(GameObject dropdownList)
			{
				visibleList = null;
				base.DestroyDropdownList(dropdownList);
			}

			protected override GameObject CreateBlocker(Canvas rootCanvas)
			{
				GameObject blocker = base.CreateBlocker(rootCanvas);
				blocker.layer = gameObject.layer;
				blocker.AddComponent<WidgetController>();
				return blocker;
			}
		}

		private sealed class AxisRow
		{
			internal int Axis;
			internal RectTransform Root;
			internal Vector2 Position;
			internal Text Caption;
			internal string CaptionText;
			internal Vector2 CaptionSize;
			internal Vector2 CaptionPosition;
			internal int CaptionFontSize;
			internal AxisSelector Selector;
			internal int[] Choices;
			internal CanvasGroup ReadOnlyAppearance;
		}

		private static FieldInfo selectedBlock;
		private static FieldInfo construction;
		private static FieldInfo changed;
		private static FieldInfo initializing;
		private static FieldInfo updatingControls;
		private static FieldInfo previewBlocks;
		private static FieldInfo previewIndices;
		private static FieldInfo previewObjects;
		private static MethodInfo saveUndo;
		private static MethodInfo rebuild;
		private static MethodInfo populatePanel;
		private static Build owner;
		private static BuildData synchronizedBuild;
		private static bool activationPending;
		private static AxisRow[] rows;
		private static RectTransform profileSwitch;
		private static Button freeButton;
		private static Button vanillaButton;
		private static BlockData displayedBlock;
		private static Build copyBufferOwner;
		private static BlockData copyBufferSource;
		private static bool copyBufferSettings;
		private static BlockData displayedCopy;
		private static CouplerRotationProfiles.Rotation displayedCopyRotation;
		private static bool displayedCopyFree;
		private static int displayedOrder = -1;
		private static bool displayedEnabled;
		private static bool displayedReadOnly;
		private static bool displayedVanilla;
		private static bool displayedSwitch;
		private static bool updating;
		private static int nextAttemptFrame;
		private static string lastFailure;

		internal static void Register(Harmony patcher)
		{
			MethodInfo update = AccessTools.Method(typeof(Build), "Update", Type.EmptyTypes);
			selectedBlock = AccessTools.Field(typeof(Build), "LBBOFMGMMFF");
			construction = AccessTools.Field(typeof(Build), "FFJDGJFPLAD");
			changed = AccessTools.Field(typeof(Build), "BHCOKCDPDNB");
			initializing = AccessTools.Field(typeof(Build), "LIOOKHCGPIO");
			updatingControls = AccessTools.Field(typeof(Build), "OIAOGDEPDCO");
			previewBlocks = AccessTools.Field(typeof(HIPBCCKFFAG), "KLOGIIBKDEM");
			previewIndices = AccessTools.Field(typeof(HIPBCCKFFAG), "HLAFDKCFFGD");
			previewObjects = AccessTools.Field(typeof(HIPBCCKFFAG), "FHLICBAMEMC");
			saveUndo = AccessTools.Method(typeof(Build), "ANDINIMKBLL", new Type[] { typeof(bool), typeof(bool) });
			rebuild = AccessTools.Method(typeof(Build), "ALGEALFIOMP", Type.EmptyTypes);
			populatePanel = AccessTools.Method(typeof(Build), "OPMMCNOHEMC", Type.EmptyTypes);
			MethodInfo updatePreview = AccessTools.Method(typeof(HIPBCCKFFAG), "CHAJBDNKDNJ", new Type[] { typeof(BlockController) });
			MethodInfo showBuffer = AccessTools.Method(typeof(Build), "IDBCKLADDIM", new Type[] { typeof(BlockData), typeof(bool) });
			if (update == null || selectedBlock == null || selectedBlock.FieldType != typeof(BlockData)
				|| showBuffer == null
				|| construction == null || construction.FieldType != typeof(HIPBCCKFFAG)
				|| changed == null || changed.FieldType != typeof(bool)
				|| initializing == null || initializing.FieldType != typeof(int)
				|| updatingControls == null || updatingControls.FieldType != typeof(bool) || saveUndo == null || rebuild == null || populatePanel == null
				|| previewBlocks == null || previewBlocks.FieldType != typeof(List<BlockData>)
				|| previewIndices == null || previewIndices.FieldType != typeof(List<int>)
				|| previewObjects == null || previewObjects.FieldType != typeof(List<GameObject>) || updatePreview == null)
				throw new MissingMemberException("Coupler order editor hooks");
			HarmonyMethod postfix = new HarmonyMethod(AccessTools.Method(typeof(CouplerRotationUi), "UpdatePostfix"));
			postfix.priority = Priority.Last;
			if (!StartupHarmonyBatch.Registered)
				patcher.Patch(update, null, postfix, null, null);
			patcher.Patch(updatePreview, null, new HarmonyMethod(AccessTools.Method(typeof(CouplerRotationUi), "PreviewPropertiesPostfix")), null, null);
			patcher.Patch(showBuffer, null, new HarmonyMethod(AccessTools.Method(typeof(CouplerRotationUi), "BufferShownPostfix")), null, null);
		}

		internal static void OnSettingChanged()
		{
			activationPending = CouplerRotation.Enabled;
			synchronizedBuild = null;
			displayedOrder = -1;
			displayedCopy = null;
		}

		internal static void UpdatePostfix(Build __instance)
		{
			if (!CouplerRotation.IsRegistered || updating)
				return;
			try
			{
				EnsureOwner(__instance);
				RestoreFreeProfiles(__instance);
				UpdateControls(__instance);
				UpdateCopyBuffer(__instance);
			}
			catch (Exception error)
			{
				ReportFailure("UI_UPDATE_FAILED", error);
			}
		}

		private static void EnsureOwner(Build build)
		{
			if (owner != build)
			{
				DestroyRows();
				owner = build;
				nextAttemptFrame = 0;
				synchronizedBuild = null;
				activationPending = CouplerRotation.Enabled;
			}
		}

		private static void UpdateControls(Build build)
		{
			EnsureOwner(build);
			BlockData block = selectedBlock.GetValue(build) as BlockData;
			GameObject panel = build.GetPNL("SetupC");
			if (block == null || block.type != BlockData.AAHMDBHDCDK.Coupler || panel == null || !panel.activeInHierarchy)
			{
				if (displayedBlock != null)
					HideLists();
				if (profileSwitch != null)
					profileSwitch.gameObject.SetActive(false);
				displayedBlock = null;
				return;
			}
			if (rows == null)
			{
				if (Time.frameCount < nextAttemptFrame)
					return;
				nextAttemptFrame = Time.frameCount + 120;
				CreateRows(build, panel.transform);
			}
			int order = CouplerRotationOrder.Read(block);
			bool enabled = CouplerRotation.Enabled;
			bool readOnly = CouplerRotation.IsRotationReadOnly(block);
			bool vanilla = CouplerRotationProfiles.IsVanilla(block);
			bool showSwitch = ShouldShowProfileSwitch(block, enabled);
			bool freeLayout = enabled || (CouplerRotationProfiles.HasFreeValues(block) && !vanilla);
			if (ReferenceEquals(displayedBlock, block) && displayedOrder == order && displayedEnabled == enabled && displayedReadOnly == readOnly
				&& displayedVanilla == vanilla && displayedSwitch == showSwitch)
				return;
			updating = true;
			try
			{
				HideLists();
				for (int position = 0; position < 3; position++)
				{
					int axis = freeLayout ? CouplerRotationOrder.Axis(order, position) : position;
					AxisRow row = rows[axis];
					row.Root.anchoredPosition = rows[position].Position;
					row.Caption.text = freeLayout ? "RotOffset" : row.CaptionText;
					row.Caption.rectTransform.sizeDelta = freeLayout ? row.CaptionSize - new Vector2(48f, 0f) : row.CaptionSize;
					row.Caption.rectTransform.anchoredPosition = freeLayout ? row.CaptionPosition - new Vector2(24f, 0f) : row.CaptionPosition;
					row.Caption.fontSize = freeLayout ? Math.Min(row.CaptionFontSize, 24) : row.CaptionFontSize;
					row.Selector.gameObject.SetActive(freeLayout);
					row.Choices = CouplerRotationOrder.AvailableAxes(order, position);
					row.Selector.options.Clear();
					foreach (int choice in row.Choices)
						row.Selector.options.Add(new Dropdown.OptionData(((char)('X' + choice)).ToString()));
					row.Selector.value = Array.IndexOf(row.Choices, axis);
					row.Selector.RefreshShownValue();
					row.Selector.interactable = enabled && !readOnly && row.Choices.Length > 1;
					row.ReadOnlyAppearance.interactable = !readOnly;
					row.ReadOnlyAppearance.alpha = readOnly ? 0.55f : 1f;
				}
				if (showSwitch && profileSwitch == null)
					CreateProfileSwitch(build, panel.GetComponent<RectTransform>());
				if (profileSwitch != null)
				{
					profileSwitch.gameObject.SetActive(showSwitch);
					SetProfileButton(freeButton, !vanilla, CouplerRotationProfiles.CanSwitch(block, false));
					SetProfileButton(vanillaButton, vanilla, CouplerRotationProfiles.CanSwitch(block, true));
				}
				displayedBlock = block;
				displayedOrder = order;
				displayedEnabled = enabled;
				displayedReadOnly = readOnly;
				displayedVanilla = vanilla;
				displayedSwitch = showSwitch;
				CouplerRotation.Log("UI_ORDER order=" + CouplerRotationOrder.Name(order)
					+ " choices=3,2,1 enabled=" + enabled + " readOnly=" + readOnly + " profile=" + (vanilla || !freeLayout ? "Vanilla" : "Free")
					+ " switch=" + showSwitch + " block=" + block.x + "," + block.y + "," + block.z);
			}
			finally
			{
				updating = false;
			}
		}

		private static void CreateRows(Build build, Transform panel)
		{
			rows = new AxisRow[3];
			try
			{
				for (int axis = 0; axis < 3; axis++)
				{
					string axisName = ((char)('X' + axis)).ToString();
					GameObject slider = build.GetSLD("ParamC" + (axis + 3));
					RectTransform root = slider == null ? null : slider.transform.parent as RectTransform;
					Transform captionObject = root == null ? null : root.Find("txt_Rot" + axisName);
					Text caption = captionObject == null ? null : captionObject.GetComponent<Text>();
					if (root == null || root.parent != panel || caption == null || root.Find("ARW_ParamC" + (axis + 3)) == null
						|| caption.text.IndexOf("RotOffset" + axisName, StringComparison.Ordinal) < 0 || caption.rectTransform.rect.width < 100f)
						throw new InvalidOperationException("Coupler row layout changed: axis=" + axisName);
					rows[axis] = new AxisRow
					{
						Axis = axis,
						Root = root,
						Position = root.anchoredPosition,
						Caption = caption,
						CaptionText = caption.text,
						CaptionSize = caption.rectTransform.sizeDelta,
						CaptionPosition = caption.rectTransform.anchoredPosition,
						CaptionFontSize = caption.fontSize
					};
				}
				foreach (AxisRow row in rows)
				{
					row.ReadOnlyAppearance = row.Root.gameObject.AddComponent<CanvasGroup>();
					row.Selector = CreateSelector(row);
				}
				displayedOrder = -1;
				CouplerRotation.Log("UI_CREATED rows=3 labels=RotOffset selectors=XYZ filtered=true nativeSliders=true");
			}
			catch
			{
				DestroyRows();
				throw;
			}
		}

		internal static bool ShouldShowProfileSwitch(BlockData block, bool enabled)
		{
			return !enabled && CouplerRotationProfiles.HasFreeValues(block);
		}

		private static void CreateProfileSwitch(Build build, RectTransform panel)
		{
			RectTransform slider = build.GetSLD("ParamC5").GetComponent<RectTransform>();
			RectTransform remote = build.GetTGL("Remote").GetComponent<RectTransform>();
			float rotationBottom = rows[2].Position.y + slider.anchoredPosition.y - slider.rect.height * 0.5f;
			float remoteTop = remote.anchoredPosition.y + remote.rect.height * 0.5f;
			if (rotationBottom - remoteTop < 32f)
				throw new InvalidOperationException("No safe space between Coupler rotation and Remote controls");
			profileSwitch = CreateRect("MP_CouplerProfiles", panel);
			profileSwitch.gameObject.SetActive(false);
			profileSwitch.anchorMin = profileSwitch.anchorMax = new Vector2(0.5f, 0.5f);
			profileSwitch.sizeDelta = new Vector2(rows[0].CaptionSize.x, 28f);
			profileSwitch.anchoredPosition = new Vector2(rows[0].Position.x, (rotationBottom + remoteTop) * 0.5f);
			freeButton = CreateProfileButton("Free", false);
			vanillaButton = CreateProfileButton("Vanilla", true);
			CouplerRotation.Log("UI_PROFILE_SWITCH_CREATED visibleOnly=disabled-with-free-history");
		}

		private static Button CreateProfileButton(string label, bool vanilla)
		{
			RectTransform rectangle = CreateRect("MP_CouplerProfile" + label, profileSwitch);
			rectangle.anchorMin = new Vector2(vanilla ? 0.5f : 0f, 0f);
			rectangle.anchorMax = new Vector2(vanilla ? 1f : 0.5f, 1f);
			rectangle.offsetMin = new Vector2(vanilla ? 2f : 0f, 0f);
			rectangle.offsetMax = new Vector2(vanilla ? 0f : -2f, 0f);
			Image background = rectangle.gameObject.AddComponent<Image>();
			Button button = rectangle.gameObject.AddComponent<Button>();
			button.targetGraphic = background;
			ColorBlock colors = button.colors;
			colors.disabledColor = Color.white;
			button.colors = colors;
			rectangle.gameObject.AddComponent<WidgetController>();
			Text caption = CreateText("Label", rectangle, rows[0].Caption.font, Color.white);
			caption.text = label;
			caption.fontSize = 18;
			caption.alignment = TextAnchor.MiddleCenter;
			button.onClick.AddListener(() => SelectProfile(vanilla));
			return button;
		}

		private static void SetProfileButton(Button button, bool selected, bool canSwitch)
		{
			button.interactable = canSwitch;
			button.GetComponent<Image>().color = selected ? new Color(0.18f, 0.42f, 0.58f, 1f) : new Color(0.18f, 0.18f, 0.18f, 0.98f);
		}

		private static void SelectProfile(bool vanilla)
		{
			if (updating || owner == null || CouplerRotation.Enabled)
				return;
			try
			{
				BlockData before = selectedBlock.GetValue(owner) as BlockData;
				if (!ReferenceEquals(before, displayedBlock) || !CouplerRotationProfiles.CanSwitch(before, vanilla)
					|| (int)initializing.GetValue(owner) != 0 || (bool)updatingControls.GetValue(owner))
					return;
				saveUndo.Invoke(owner, new object[] { false, true });
				BlockData block = selectedBlock.GetValue(owner) as BlockData;
				if (block == null || block.type != before.type || block.x != before.x || block.y != before.y || block.z != before.z)
					throw new InvalidOperationException("Coupler selection changed while saving profile undo");
				if (!CouplerRotationProfiles.Switch(block, vanilla))
					throw new InvalidOperationException("Coupler profile changed while saving undo");
				changed.SetValue(owner, true);
				HIPBCCKFFAG assembly = construction.GetValue(owner) as HIPBCCKFFAG;
				if (assembly != null)
				{
					if (assembly.HCMMJPFOIHD)
						rebuild.Invoke(owner, null);
					assembly.BGKPIEPJLON();
				}
				RefreshPanel(owner);
				displayedOrder = -1;
				UpdateControls(owner);
				CouplerRotation.Log("UI_PROFILE_CHANGED active=" + (vanilla ? "Vanilla" : "Free") + " undo=native");
			}
			catch (Exception error)
			{
				ReportFailure("UI_PROFILE_CHANGE_FAILED", error);
			}
		}

		private static void BufferShownPostfix(Build __instance, BlockData NGOGBGEBBOG, bool EDKGDLDJOJD)
		{
			copyBufferOwner = __instance;
			copyBufferSource = NGOGBGEBBOG;
			copyBufferSettings = EDKGDLDJOJD;
			displayedCopy = null;
			if (!CouplerRotation.IsRegistered)
				return;
			try
			{
				UpdateCopyBuffer(__instance);
			}
			catch (Exception error)
			{
				ReportFailure("COPY_BUFFER_FAILED", error);
			}
		}

		internal static string[,] BufferRows(CouplerRotationProfiles.Rotation rotation, bool free)
		{
			Vector3 angles = rotation.Angles;
			string[] colors = { "red", "lime", "#0080ff" };
			string[,] result = new string[3, 2];
			for (int row = 0; row < 3; row++)
			{
				int axis = free ? CouplerRotationOrder.Axis(rotation.Order, row) : row;
				result[row, 0] = "<color=" + colors[axis] + ">RotOffset" + (char)('X' + axis) + "</color>";
				result[row, 1] = SetupPrecisionData.Format(angles[axis]);
			}
			return result;
		}

		private static void UpdateCopyBuffer(Build build)
		{
			BlockData source = copyBufferSource;
			if (!ReferenceEquals(copyBufferOwner, build) || source == null || source.type != BlockData.AAHMDBHDCDK.Coupler)
			{
				displayedCopy = null;
				return;
			}
			CouplerRotationProfiles.Rotation rotation;
			bool free;
			if (!CouplerRotationCopy.TryGetBufferRotation(source, copyBufferSettings, CouplerRotation.Enabled, out rotation, out free))
				return;
			if (ReferenceEquals(displayedCopy, source) && displayedCopyFree == free && displayedCopyRotation.Same(rotation))
				return;
			GameObject buffer = build.GetPNL("Buffer");
			if (buffer == null || !buffer.activeInHierarchy)
				return;
			string[,] bufferRows = BufferRows(rotation, free);
			for (int row = 0; row < bufferRows.GetLength(0); row++)
			{
				buffer.transform.Find("TXT_Action" + (row + 3)).GetComponent<Text>().text = bufferRows[row, 0];
				buffer.transform.Find("TXT_Param" + (row + 3)).GetComponent<Text>().text = bufferRows[row, 1];
			}
			string order = free ? CouplerRotationOrder.Name(rotation.Order) : "XYZ";
			string mode = free ? "Free " + order : "Vanilla";
			GameObject title = build.GetTXT("Buffer");
			if (title != null)
				title.GetComponent<Text>().text = "Coupler (" + mode + ")";
			displayedCopy = source;
			displayedCopyFree = free;
			displayedCopyRotation = rotation;
			CouplerRotation.Log("COPY_BUFFER route=" + (copyBufferSettings ? "settings" : "block") + " mode=" + mode
				+ " xyz=" + rotation.X + "," + rotation.Y + "," + rotation.Z
				+ " rows=" + order[0] + ":" + bufferRows[0, 1] + "," + order[1] + ":" + bufferRows[1, 1] + "," + order[2] + ":" + bufferRows[2, 1]);
		}

		internal static bool TryCopySettings(Build build, BlockData source)
		{
			if (!CouplerRotationCopy.CanCopy(selectedBlock.GetValue(build) as BlockData, source))
				return false;
			try
			{
				BlockData block = selectedBlock.GetValue(build) as BlockData;
				if (!CouplerRotationCopy.ValidSettings(block, source))
				{
					CouplerRotation.Log("COPY_REJECTED route=settings reason=invalid-profile");
					return true;
				}
				if (CouplerRotationCopy.MatchesSettings(block, source))
					return true;
				saveUndo.Invoke(build, new object[] { false, true });
				block = selectedBlock.GetValue(build) as BlockData;
				if (!CouplerRotationCopy.CopySettings(block, source))
					throw new InvalidOperationException("Coupler settings changed while saving copy undo");
				changed.SetValue(build, true);
				HIPBCCKFFAG assembly = construction.GetValue(build) as HIPBCCKFFAG;
				if (assembly != null)
				{
					if (assembly.HCMMJPFOIHD)
						rebuild.Invoke(build, null);
					assembly.BGKPIEPJLON();
				}
				RefreshPanel(build);
				displayedOrder = -1;
				CouplerRotation.Log("COPY_SETTINGS_UPDATED undo=native preview=" + (assembly != null && assembly.HCMMJPFOIHD));
			}
			catch (Exception error)
			{
				ReportFailure("COPY_REFRESH_FAILED", error);
			}
			return true;
		}

		private static void RefreshPanel(Build build)
		{
			BlockData block = selectedBlock.GetValue(build) as BlockData;
			GameObject panel = build.GetPNL("SetupC");
			if (block == null || block.type != BlockData.AAHMDBHDCDK.Coupler || panel == null || !panel.activeInHierarchy)
				return;
			int previousInitializing = (int)initializing.GetValue(build);
			bool previousUpdating = (bool)updatingControls.GetValue(build);
			try
			{
				initializing.SetValue(build, Math.Max(2, previousInitializing));
				updatingControls.SetValue(build, true);
				populatePanel.Invoke(build, null);
			}
			finally
			{
				initializing.SetValue(build, previousInitializing);
				updatingControls.SetValue(build, previousUpdating);
			}
		}

		private static void RestoreFreeProfiles(Build build)
		{
			if (!CouplerRotation.Enabled || Build.GFJLEEJELOL == null || (int)initializing.GetValue(build) != 0
				|| (bool)updatingControls.GetValue(build))
				return;
			BlockData selected = selectedBlock.GetValue(build) as BlockData;
			if (!activationPending && ReferenceEquals(synchronizedBuild, Build.GFJLEEJELOL) && !CouplerRotationProfiles.IsVanilla(selected))
				return;
			List<int> restored = new List<int>();
			for (int index = 0; index < Build.GFJLEEJELOL.blockData.Count; index++)
				if (CouplerRotationProfiles.CanSwitch(Build.GFJLEEJELOL.blockData[index], false))
					restored.Add(index);
			bool saveActivationUndo = activationPending;
			activationPending = false;
			synchronizedBuild = Build.GFJLEEJELOL;
			if (restored.Count == 0)
				return;
			updating = true;
			try
			{
				if (saveActivationUndo)
					saveUndo.Invoke(build, new object[] { false, true });
				foreach (int index in restored)
					CouplerRotationProfiles.Switch(Build.GFJLEEJELOL.blockData[index], false);
				HIPBCCKFFAG assembly = construction.GetValue(build) as HIPBCCKFFAG;
				if (assembly != null)
				{
					if (assembly.HCMMJPFOIHD)
						RefreshPreviewProfiles(assembly, restored);
					assembly.BGKPIEPJLON();
				}
				changed.SetValue(build, true);
				RefreshPanel(build);
				synchronizedBuild = Build.GFJLEEJELOL;
				displayedOrder = -1;
				CouplerRotation.Log("PROFILES_AUTO_FREE count=" + restored.Count + " scope=local-build vanilla=preserved undo="
					+ (saveActivationUndo ? "native" : "current-edit"));
			}
			finally
			{
				updating = false;
			}
		}

		private static void RefreshPreviewProfiles(HIPBCCKFFAG assembly, List<int> restored)
		{
			List<BlockData> blocks = (List<BlockData>)previewBlocks.GetValue(assembly);
			List<int> indices = (List<int>)previewIndices.GetValue(assembly);
			List<BlockData> refreshed = new List<BlockData>();
			for (int index = 0; index < blocks.Count; index++)
			{
				if (index >= indices.Count || !restored.Contains(indices[index]))
					continue;
				BlockData source = Build.GFJLEEJELOL.blockData[indices[index]];
				BlockData target = blocks[index];
				if (target.type != BlockData.AAHMDBHDCDK.Coupler)
					continue;
				Array.Copy(source.actionParam, 3, target.actionParam, 3, 3);
				CouplerRotationOrder.Set(target, CouplerRotationOrder.Read(source));
				CouplerRotationProfiles.Copy(target, source);
				SetupPrecisionData.Copy(target, source);
				refreshed.Add(target);
			}
			foreach (GameObject previewObject in (List<GameObject>)previewObjects.GetValue(assembly))
			{
				BlockController controller = previewObject == null ? null : previewObject.GetComponent<BlockController>();
				if (controller != null && refreshed.Contains(controller.JNKEKNOAPHO))
					assembly.CHAJBDNKDNJ(controller);
			}
		}

		private static void PreviewPropertiesPostfix(HIPBCCKFFAG __instance, BlockController HLEKLIGJLDL)
		{
			if (CouplerRotation.IsRegistered && HLEKLIGJLDL != null)
				PersistPreviewProperties(__instance, HLEKLIGJLDL.JNKEKNOAPHO);
		}

		internal static void PersistPreviewProperties(HIPBCCKFFAG assembly, BlockData block)
		{
			if (assembly == null || !assembly.HCMMJPFOIHD || block == null || block.type != BlockData.AAHMDBHDCDK.Coupler
				|| Build.GFJLEEJELOL == null)
				return;
			if (CouplerRotationProfiles.CopyToBuild(block, (List<BlockData>)previewBlocks.GetValue(assembly),
				(List<int>)previewIndices.GetValue(assembly), Build.GFJLEEJELOL.blockData))
				CouplerRotation.Log("PROFILES_PREVIEW_SAVED order=" + CouplerRotationOrder.Name(CouplerRotationOrder.Read(block)));
		}

		private static AxisSelector CreateSelector(AxisRow row)
		{
			GameObject selectorObject = new GameObject("MP_CouplerAxis" + (char)('X' + row.Axis), typeof(RectTransform));
			selectorObject.SetActive(false);
			RectTransform selectorRect = selectorObject.GetComponent<RectTransform>();
			selectorRect.SetParent(row.Root, false);
			selectorObject.layer = row.Root.gameObject.layer;
			selectorRect.anchorMin = selectorRect.anchorMax = new Vector2(0.5f, 0.5f);
			selectorRect.sizeDelta = new Vector2(42f, 30f);
			selectorRect.anchoredPosition = row.CaptionPosition + new Vector2((row.CaptionSize.x - 42f) * 0.5f, 0f);
			Image background = selectorObject.AddComponent<Image>();
			background.color = new Color(0.18f, 0.18f, 0.18f, 0.98f);
			AxisSelector dropdown = selectorObject.AddComponent<AxisSelector>();
			selectorObject.AddComponent<WidgetController>();
			row.Selector = dropdown;
			dropdown.targetGraphic = background;
			dropdown.captionText = CreateText("Axis", selectorRect, row.Caption.font, row.Caption.color);
			dropdown.captionText.rectTransform.offsetMin = new Vector2(6f, 0f);
			dropdown.captionText.rectTransform.offsetMax = new Vector2(-14f, 0f);
			Text arrow = CreateText("Arrow", selectorRect, row.Caption.font, Color.white);
			arrow.text = "▼";
			arrow.fontSize = 10;
			arrow.rectTransform.anchorMin = new Vector2(1f, 0f);
			arrow.rectTransform.offsetMin = new Vector2(-12f, 0f);
			arrow.rectTransform.offsetMax = new Vector2(-2f, 0f);

			RectTransform template = CreateRect("Template", selectorRect);
			template.anchorMin = new Vector2(0f, 0f);
			template.anchorMax = new Vector2(1f, 0f);
			template.pivot = new Vector2(0.5f, 1f);
			template.sizeDelta = new Vector2(0f, 94f);
			template.anchoredPosition = new Vector2(0f, -2f);
			template.gameObject.AddComponent<Image>().color = new Color(0.1f, 0.1f, 0.1f, 1f);
			template.gameObject.AddComponent<WidgetController>();
			RectTransform viewport = CreateRect("Viewport", template);
			viewport.offsetMin = new Vector2(2f, 2f);
			viewport.offsetMax = new Vector2(-2f, -2f);
			viewport.gameObject.AddComponent<RectMask2D>();
			RectTransform content = CreateRect("Content", viewport);
			content.anchorMin = new Vector2(0f, 1f);
			content.anchorMax = new Vector2(1f, 1f);
			content.pivot = new Vector2(0.5f, 1f);
			content.sizeDelta = new Vector2(0f, 30f);
			RectTransform item = CreateRect("Item", content);
			item.anchorMin = new Vector2(0f, 1f);
			item.anchorMax = new Vector2(1f, 1f);
			item.sizeDelta = new Vector2(0f, 30f);
			item.anchoredPosition = new Vector2(0f, -15f);
			Image itemBackground = item.gameObject.AddComponent<Image>();
			itemBackground.color = new Color(0.28f, 0.28f, 0.28f, 1f);
			Toggle toggle = item.gameObject.AddComponent<Toggle>();
			toggle.targetGraphic = itemBackground;
			item.gameObject.AddComponent<WidgetController>();
			Text itemText = CreateText("Label", item, row.Caption.font, Color.white);
			itemText.rectTransform.offsetMin = new Vector2(8f, 0f);
			dropdown.itemText = itemText;
			dropdown.template = template;
			template.gameObject.SetActive(false);
			dropdown.onValueChanged.AddListener(value => SelectAxis(row, value));
			selectorObject.SetActive(true);
			return dropdown;
		}

		private static RectTransform CreateRect(string name, RectTransform parent)
		{
			GameObject child = new GameObject(name, typeof(RectTransform));
			child.layer = parent.gameObject.layer;
			RectTransform rectangle = child.GetComponent<RectTransform>();
			rectangle.SetParent(parent, false);
			rectangle.anchorMin = Vector2.zero;
			rectangle.anchorMax = Vector2.one;
			rectangle.offsetMin = rectangle.offsetMax = Vector2.zero;
			return rectangle;
		}

		private static Text CreateText(string name, RectTransform parent, Font font, Color color)
		{
			Text label = CreateRect(name, parent).gameObject.AddComponent<Text>();
			label.font = font;
			label.fontSize = 20;
			label.alignment = TextAnchor.MiddleLeft;
			label.color = color;
			label.raycastTarget = false;
			return label;
		}

		private static void SelectAxis(AxisRow row, int choice)
		{
			if (updating || owner == null || !CouplerRotation.Enabled || row.Choices == null || choice < 0 || choice >= row.Choices.Length)
				return;
			try
			{
				BlockData before = selectedBlock.GetValue(owner) as BlockData;
				if (before == null || before.type != BlockData.AAHMDBHDCDK.Coupler || !ReferenceEquals(before, displayedBlock)
					|| CouplerRotation.IsRotationReadOnly(before) || (int)initializing.GetValue(owner) != 0 || (bool)updatingControls.GetValue(owner))
				{
					displayedOrder = -1;
					UpdateControls(owner);
					return;
				}
				int oldOrder = CouplerRotationOrder.Read(before);
				int position = CouplerRotationOrder.Name(oldOrder).IndexOf((char)('X' + row.Axis));
				int newOrder = CouplerRotationOrder.SelectAxis(oldOrder, position, row.Choices[choice]);
				if (oldOrder == newOrder)
					return;
				CouplerRotationProfiles.Rotation vanilla = CouplerRotationProfiles.InitialVanilla(before);
				saveUndo.Invoke(owner, new object[] { false, true });
				BlockData block = selectedBlock.GetValue(owner) as BlockData;
				if (block == null || block.type != before.type || block.x != before.x || block.y != before.y || block.z != before.z)
					throw new InvalidOperationException("Coupler selection changed while saving undo");
				CouplerRotationProfiles.RememberFreeEdit(block, vanilla);
				CouplerRotationOrder.Set(block, newOrder);
				changed.SetValue(owner, true);
				HIPBCCKFFAG assembly = construction.GetValue(owner) as HIPBCCKFFAG;
				if (assembly != null)
				{
					if (assembly.HCMMJPFOIHD)
						rebuild.Invoke(owner, null);
					assembly.BGKPIEPJLON();
				}
				CouplerRotation.Log("ORDER_CHANGED from=" + CouplerRotationOrder.Name(oldOrder) + " to=" + CouplerRotationOrder.Name(newOrder)
					+ " xyz=" + block.actionParam[3] + "," + block.actionParam[4] + "," + block.actionParam[5] + " undo=native");
				displayedOrder = -1;
				UpdateControls(owner);
			}
			catch (Exception error)
			{
				ReportFailure("UI_CHANGE_FAILED", error);
			}
		}

		private static void HideLists()
		{
			if (rows == null)
				return;
			foreach (AxisRow row in rows)
				if (row != null && row.Selector != null)
					row.Selector.CloseIfOpen();
		}

		private static void DestroyRows()
		{
			HideLists();
			if (rows != null)
			{
				foreach (AxisRow row in rows)
				{
					if (row == null)
						continue;
					if (row.Root != null)
						row.Root.anchoredPosition = row.Position;
					if (row.Caption != null)
					{
						row.Caption.text = row.CaptionText;
						row.Caption.rectTransform.sizeDelta = row.CaptionSize;
						row.Caption.rectTransform.anchoredPosition = row.CaptionPosition;
						row.Caption.fontSize = row.CaptionFontSize;
					}
					if (row.Selector != null)
						UnityEngine.Object.Destroy(row.Selector.gameObject);
					if (row.ReadOnlyAppearance != null)
						UnityEngine.Object.Destroy(row.ReadOnlyAppearance);
				}
			}
			rows = null;
			if (profileSwitch != null)
				UnityEngine.Object.Destroy(profileSwitch.gameObject);
			profileSwitch = null;
			freeButton = null;
			vanillaButton = null;
			displayedBlock = null;
			displayedOrder = -1;
		}

		private static void ReportFailure(string eventName, Exception error)
		{
			if (error is TargetInvocationException && error.InnerException != null)
				error = error.InnerException;
			string failure = eventName + " " + error.GetType().Name + ": " + error.Message;
			if (failure == lastFailure)
				return;
			lastFailure = failure;
			CouplerRotation.Log(failure);
		}
	}
}
