using System;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using MPatchrMain;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
	// The recovered renderer writes the configured offset before the normal
	// Stamp UV code, which immediately replaces _MainTex_ST on Plate stamps.
	// A LateUpdate driver reapplies the user offset after all renderer Updates.
	internal static class NormalMapStampOffsets
	{
		private const string TextureProperty = "_MainTex";
		private const string SmoothnessProperty = "_Glossiness";
		private const string MetallicProperty = "_Metallic";
		private const string DriverName = "MPatcher.NormalMapStampOffsets";
		private const int PreviewLayer = 31;
		private const int PreviewTextureSize = 300;
		private static readonly Vector3 PreviewRootPosition = new Vector3(123f, 456f, 789f);

		private static readonly Type TargetType = typeof(global::IK5FoqU27QNYKBuS9GCG4jJmggiDoTvlnEzqfuzDeGITj4Y7Swt3U7EZtdklA6tU2RFesc0NlU_0024tEranc1z8GYk);
		private static FieldInfo materialField;
		private static FieldInfo plateField;
		private static FieldInfo normalReadyField;
		private static NormalMapStampOffsetDriver driver;
		private static readonly List<TargetState> targets = new List<TargetState>();
		private static readonly HashSet<int> isolatedPreviewCameras = new HashSet<int>();
		private static float nextRefresh;
		private static int lastLoggedX = int.MinValue;
		private static int lastLoggedY = int.MinValue;
		private static int lastLoggedSmooth = int.MinValue;
		private static int lastLoggedMetal = int.MinValue;
		private static int observedX = int.MinValue;
		private static int observedY = int.MinValue;
		private static int observedSmooth = int.MinValue;
		private static int observedMetal = int.MinValue;
		private static float settingsLogAt;
		private static string lastFailure;

		internal static void TryRegister()
		{
			if (driver != null)
				return;

			try
			{
				materialField = AccessTools.Field(TargetType, "bI2brPAlXsrzT5DHrzCi4uw");
				plateField = AccessTools.Field(TargetType, "WcreQ$_w$YzbiNuU8E7VX2o");
				normalReadyField = AccessTools.Field(TargetType, "JfvfNa5n1GWtOzYRmeMuREk");
				if (materialField == null || plateField == null || normalReadyField == null)
					throw new MissingMemberException(TargetType.FullName, "private renderer fields");

				GameObject gameObject = new GameObject(DriverName);
				UnityEngine.Object.DontDestroyOnLoad(gameObject);
				driver = gameObject.AddComponent<NormalMapStampOffsetDriver>();
				Camera.onPreCull += OnCameraPreCull;
				Log("REGISTERED target=" + TargetType.FullName + " mode=LateUpdate properties="
					+ TextureProperty + "," + SmoothnessProperty + "," + MetallicProperty
					+ " preview=Camera.onPreCull");
			}
			catch (Exception error)
			{
				Log("REGISTER_FAILED type=" + error.GetType().Name + " message=" + error.Message);
			}
		}

		internal static void ApplyLateUpdate()
		{
			try
			{
				settingsIngame settings = MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68;
				mcpd data = MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4;
				if (settings == null || !settings.stampNormalMap || data == null || data.nmapHash == 0)
					return;

				if (Time.realtimeSinceStartup >= nextRefresh)
				{
					RefreshTargets();
					nextRefresh = Time.realtimeSinceStartup + 0.5f;
				}

				Vector2 configuredOffset = new Vector2(
					Mathf.Repeat(data.xOffset / 100f, 1f),
					Mathf.Repeat(data.yOffset / 100f, 1f));
				float configuredSmoothness = Mathf.Clamp01(data.smooth / 100f);
				float configuredMetallic = Mathf.Clamp01(data.metal / 100f);
				int appliedCount = 0;
				for (int index = targets.Count - 1; index >= 0; index--)
				{
					TargetState target = targets[index];
					if (target.Component == null || target.Material == null)
					{
						targets.RemoveAt(index);
						continue;
					}
					if (!(bool)normalReadyField.GetValue(target.Component))
						continue;

					Vector2 baseOffset = target.Material.GetTextureOffset(TextureProperty);
					target.Material.SetTextureOffset(TextureProperty, baseOffset + configuredOffset);
					target.Material.SetFloat(SmoothnessProperty, configuredSmoothness);
					target.Material.SetFloat(MetallicProperty, configuredMetallic);
					appliedCount++;
				}

				if (data.xOffset != observedX || data.yOffset != observedY
					|| data.smooth != observedSmooth || data.metal != observedMetal)
				{
					observedX = data.xOffset;
					observedY = data.yOffset;
					observedSmooth = data.smooth;
					observedMetal = data.metal;
					settingsLogAt = Time.realtimeSinceStartup + 0.25f;
				}

				if (appliedCount > 0 && Time.realtimeSinceStartup >= settingsLogAt
					&& (observedX != lastLoggedX || observedY != lastLoggedY
						|| observedSmooth != lastLoggedSmooth || observedMetal != lastLoggedMetal))
				{
					lastLoggedX = observedX;
					lastLoggedY = observedY;
					lastLoggedSmooth = observedSmooth;
					lastLoggedMetal = observedMetal;
					Log("APPLIED x=" + observedX + " y=" + observedY
						+ " smooth=" + observedSmooth + " metal=" + observedMetal
						+ " targets=" + appliedCount);
				}
			}
			catch (Exception error)
			{
				LogFailure(error);
			}
		}

		private static void OnCameraPreCull(Camera camera)
		{
			// This callback runs before the preview camera's first cull, so the
			// BUILD scene cannot flash in its RenderTexture for an initial frame.
			try
			{
				settingsIngame settings = MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68;
				mcpd data = MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4;
				if (settings != null && settings.stampNormalMap && data != null && data.nmapHash != 0)
					IsolatePreviewCamera(camera);
			}
			catch (Exception error)
			{
				LogFailure(error);
			}
		}

		private static void IsolatePreviewCamera(Camera camera)
		{
			if (camera == null || camera.targetTexture == null
				|| camera.targetTexture.width != PreviewTextureSize
				|| camera.targetTexture.height != PreviewTextureSize
				|| camera.transform.parent == null)
				return;

			Transform root = camera.transform.parent;
			if ((root.position - PreviewRootPosition).sqrMagnitude > 0.01f)
				return;

			int cameraId = camera.GetInstanceID();
			if (isolatedPreviewCameras.Contains(cameraId))
				return;

			int sphereRenderers = 0;
			MeshRenderer[] renderers = root.GetComponentsInChildren<MeshRenderer>();
			foreach (MeshRenderer renderer in renderers)
			{
				if (renderer != null && renderer.GetComponent<SphereCollider>() != null)
				{
					renderer.gameObject.layer = PreviewLayer;
					sphereRenderers++;
				}
			}

			if (sphereRenderers == 0)
				return;

			camera.clearFlags = CameraClearFlags.SolidColor;
			camera.backgroundColor = new Color(0.18f, 0.18f, 0.18f, 1f);
			camera.cullingMask = 1 << PreviewLayer;
			camera.useOcclusionCulling = false;
			isolatedPreviewCameras.Add(cameraId);
			Log("PREVIEW_ISOLATED phase=onPreCull layer=" + PreviewLayer
				+ " renderers=" + sphereRenderers + " size="
				+ camera.targetTexture.width + "x" + camera.targetTexture.height);
		}

		private static void RefreshTargets()
		{
			targets.Clear();
			UnityEngine.Object[] found = UnityEngine.Object.FindObjectsOfType(TargetType);
			foreach (UnityEngine.Object component in found)
			{
				if (component == null || plateField.GetValue(component) == null)
					continue;

				Material material = materialField.GetValue(component) as Material;
				if (material != null)
					targets.Add(new TargetState(component, material));
			}
		}

		private sealed class TargetState
		{
			internal readonly UnityEngine.Object Component;
			internal readonly Material Material;

			internal TargetState(UnityEngine.Object component, Material material)
			{
				Component = component;
				Material = material;
			}
		}

		private static void LogFailure(Exception error)
		{
			string failure = error.GetType().Name + ":" + error.Message;
			if (failure == lastFailure)
				return;

			lastFailure = failure;
			Log("APPLY_FAILED type=" + error.GetType().Name + " message=" + error.Message);
		}

		private static void Log(string message)
		{
			string text = "[NORMALMAP-STAMP-OFFSETS] " + message;
			try { global::mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(text); }
			catch (Exception) { }
		}
	}

	internal sealed class NormalMapStampOffsetDriver : MonoBehaviour
	{
		private void LateUpdate()
		{
			NormalMapStampOffsets.ApplyLateUpdate();
		}
	}
}
