using System;
using System.Reflection;
using HarmonyLib;
using UnityEngine;
using UnityStandardAssets.Water;

namespace MPatcherFork.CustomPatches
{
	// The original More View Distance patch only widens the local slider. Game
	// then clamps the camera, fog and terrain to the room's ViewDistanceLimit.
	// For Individual/Legacy, restore the player's selected local distance after
	// RideCameraController applies that clamp.
	internal static class LegacyMoreViewDistance
	{
		private const string PatchId = "local.moddev.machinecraft.more-view-distance-legacy.v1";
		private static Harmony harmony;
		private static int lastSceneId;
		private static int lastRequested;
		private static int lastRoomLimit;

		internal static void TryRegister()
		{
			if (harmony != null)
				return;

			try
			{
				MethodInfo target = AccessTools.Method(typeof(RideCameraController), "SetViewOption", new Type[] { typeof(bool) });
				MethodInfo postfix = AccessTools.Method(typeof(LegacyMoreViewDistance), "SetViewOptionPostfix");
				if (target == null || postfix == null)
					throw new MissingMethodException("RideCameraController.SetViewOption(bool)");

				harmony = new Harmony(PatchId);
				harmony.Patch(target, null, new HarmonyMethod(postfix), null, null);
				Log("REGISTERED target=RideCameraController.SetViewOption scope=Legacy");
			}
			catch (Exception error)
			{
				Log("REGISTER_FAILED type=" + error.GetType().Name + " message=" + error.Message);
			}
		}

		private static void SetViewOptionPostfix(RideCameraController __instance)
		{
			try
			{
				if (!Enabled
					|| HNJDDKJLHMM.FHLGOMHPDLN != HNJDDKJLHMM.HKGAACMIPIH.Legacy
					|| !(Arena.OEDCBNHNGMJ is Game)
					|| JKGKJLLFMLE.NIOIBMAMHKP == null)
				{
					return;
				}

				int requested = JKGKJLLFMLE.NIOIBMAMHKP.viewDistance;
				int roomLimit = BAOJJGLHNCB.ODDAPHEADEN;
				if (requested <= roomLimit || requested <= 0)
					return;

				if (__instance.BOIEJCIBHKI != null)
					__instance.BOIEJCIBHKI.farClipPlane = requested;
				if (__instance.JPGNHHJCOFA != null)
					__instance.JPGNHHJCOFA.farClipPlane = requested;

				RenderSettings.fogStartDistance = requested * 0.5f;
				RenderSettings.fogEndDistance = requested * 0.99f;
				Shader.SetGlobalFloat("ModInvViewDist", 3f / requested);

				Terrain[] terrains = UnityEngine.Object.FindObjectsOfType<Terrain>();
				float terrainDistance = requested >= 2001 ? requested * 0.5f : 0f;
				foreach (Terrain terrain in terrains)
					terrain.basemapDistance = terrainDistance;

				PlanarReflection reflection = UnityEngine.Object.FindObjectOfType<PlanarReflection>();
				if (reflection != null)
					reflection.SetCullDistance();

				int sceneId = Arena.OEDCBNHNGMJ.GetInstanceID();
				if (sceneId != lastSceneId || requested != lastRequested || roomLimit != lastRoomLimit)
				{
					lastSceneId = sceneId;
					lastRequested = requested;
					lastRoomLimit = roomLimit;
					Log("APPLIED requested=" + requested + " roomLimit=" + roomLimit
						+ " cameras=" + ((__instance.BOIEJCIBHKI != null ? 1 : 0) + (__instance.JPGNHHJCOFA != null ? 1 : 0))
						+ " terrains=" + terrains.Length);
				}
			}
			catch (Exception error)
			{
				Log("APPLY_FAILED type=" + error.GetType().Name + " message=" + error.Message);
			}
		}

		private static bool Enabled
		{
			get
			{
				return MPatchrMain.MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68 != null
					&& MPatchrMain.MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.moreViewDistance;
			}
		}

		private static void Log(string message)
		{
			string text = "[MORE-VIEW-DISTANCE-LEGACY] " + message;
			try { global::mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(text); }
			catch (Exception) { }
		}
	}
}
