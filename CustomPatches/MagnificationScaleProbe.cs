using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
	// Opt-in, isolated Unity experiments; each runner declares its own fixture.
	internal static class MagnificationScaleProbe
	{
		private static bool checkedArguments;
		internal static void TryStart()
		{
			if (checkedArguments) return;
			checkedArguments = true;
			bool dynamicProbe = Array.IndexOf(Environment.GetCommandLineArgs(), "--mpatcher-magnification-dynamic-probe") >= 0;
			bool stressProbe = Array.IndexOf(Environment.GetCommandLineArgs(), "--mpatcher-magnification-stress-probe") >= 0;
			bool suspensionProbe = Array.IndexOf(Environment.GetCommandLineArgs(), "--mpatcher-magnification-suspension-probe") >= 0;
			bool craftProbe = Array.IndexOf(Environment.GetCommandLineArgs(), "--mpatcher-magnification-craft-probe") >= 0;
			if (!craftProbe && !suspensionProbe && !stressProbe && !dynamicProbe && Array.IndexOf(Environment.GetCommandLineArgs(), "--mpatcher-magnification-scale-probe") < 0) return;
			string root = Path.GetFullPath(Path.Combine(Application.dataPath, ".."));
			string expected = Path.Combine(Environment.GetEnvironmentVariable("USERPROFILE") ?? string.Empty, "Downloads\\MachineCraftMPatcherTest");
			if (!string.Equals(root.TrimEnd('\\', '/'), expected, StringComparison.OrdinalIgnoreCase))
			{
				MagnificationDownscale.Log("SCALE_PROBE_REFUSED reason=not-isolated-test-root");
				return;
			}
			GameObject host = new GameObject("MPatcher_MagnificationScaleProbe");
			if (craftProbe) host.AddComponent<MagnificationCraftProbe>();
			else if (suspensionProbe) host.AddComponent<MagnificationSuspensionProbe>();
			else if (stressProbe) host.AddComponent<MagnificationStressProbe>();
			else if (dynamicProbe) host.AddComponent<MagnificationDynamicProbe>();
			else host.AddComponent<MagnificationScaleProbeRunner>();
			MagnificationDownscale.Log(craftProbe ? "SCALE_PROBE_STARTED scope=native-craft userData=fixture-copy nativeSaves=blocked exit=automatic" : "SCALE_PROBE_STARTED scope=synthetic userData=unchanged exit=automatic");
		}
	}

	internal sealed class MagnificationScaleProbeRunner : MonoBehaviour
	{
		private readonly List<Rig> rigs = new List<Rig>();
		private int fixedFrame;
		private int lateFrame;
		private float startTime;
		private bool done;
		private bool exitRequested;

		private void Start()
		{
			startTime = Time.realtimeSinceStartup;
			foreach (float scale in new[] { 0.05f, 1f, 2f })
				foreach (string mode in new[] { "v21", "create-after-scale", "settled-create", "settled-reapply", "rebuild-v21", "rebuild-v8" })
				{
					Rig rig = new Rig();
					rig.Name = mode + "/" + F(scale);
					rig.Mode = mode;
					rig.Scale = scale;
					rig.Own = MakeBody("ProbeOwn/" + rig.Name, new Vector3(rigs.Count * 30f, 1000f, 0f));
					rig.Other = MakeBody("ProbeOther/" + rig.Name, rig.Own.position + Vector3.right * (10f * scale));
					if (mode != "settled-create" && mode != "create-after-scale") CreateJoints(rig);
					rig.Own.transform.localScale = Vector3.one * scale;
					rig.Other.transform.localScale = Vector3.one * scale;
					if (mode == "create-after-scale") CreateJoints(rig);
					if (rig.Hinge != null) Apply(rig);
					rigs.Add(rig);
				}
			Snapshot("created");
		}

		private static Rigidbody MakeBody(string name, Vector3 position)
		{
			GameObject value = new GameObject(name);
			value.transform.position = position;
			Rigidbody body = value.AddComponent<Rigidbody>();
			body.useGravity = false;
			body.constraints = RigidbodyConstraints.FreezeAll;
			return body;
		}

		private static void CreateJoints(Rig rig)
		{
			rig.Hinge = rig.Own.gameObject.AddComponent<HingeJoint>();
			rig.Hard = rig.Own.gameObject.AddComponent<ConfigurableJoint>();
			rig.Hinge.connectedBody = rig.Other;
			rig.Hard.connectedBody = rig.Other;
		}

		private static void Apply(Rig rig)
		{
			foreach (Joint joint in new Joint[] { rig.Hinge, rig.Hard })
			{
				joint.autoConfigureConnectedAnchor = false;
				joint.anchor = new Vector3(4f, 8f, -3f);
				joint.connectedAnchor = rig.Other.transform.InverseTransformPoint(rig.Own.transform.TransformPoint(joint.anchor));
			}
		}

		private void FixedUpdate()
		{
			if (done) return;
			fixedFrame++;
			if (fixedFrame <= 4 || fixedFrame == 10) Snapshot("fixed-" + fixedFrame);
			if (fixedFrame == 3)
			{
				foreach (Rig rig in rigs)
				{
					if (rig.Mode == "settled-create") { CreateJoints(rig); Apply(rig); }
					else if (rig.Mode == "settled-reapply") Apply(rig);
					else if (rig.Mode == "rebuild-v21" || rig.Mode == "rebuild-v8")
					{
						DestroyImmediate(rig.Hinge);
						rig.Hinge = rig.Own.gameObject.AddComponent<HingeJoint>();
						rig.Hinge.connectedBody = rig.Other;
						float factor = rig.Mode == "rebuild-v21" ? rig.Scale
							: MagnificationJointAnchors.EndpointFactor(rig.Scale, rig.Own.transform.lossyScale.x);
						Vector3 anchor = new Vector3(4f, 8f, -3f) * factor;
						MagnificationJointAnchors.SetSamePivot(rig.Hinge, anchor);
						MagnificationJointAnchors.SetSamePivot(rig.Hard,
							rig.Mode == "rebuild-v21" ? rig.Hard.anchor : anchor);
					}
				}
				Snapshot("after-settled-write");
			}
		}

		private void LateUpdate()
		{
			lateFrame++;
			if (lateFrame <= 3) Snapshot("late-" + lateFrame);
			if (!done && (fixedFrame >= 10 || Time.realtimeSinceStartup - startTime > 10f))
			{
				done = true;
				Snapshot("complete");
				foreach (Rig rig in rigs) { Destroy(rig.Own.gameObject); Destroy(rig.Other.gameObject); }
				MagnificationDownscale.Log("SCALE_PROBE_COMPLETE fixedFrames=" + fixedFrame + " rigs=" + rigs.Count);
			}
			// Startup patches still run on the loader thread after Menu.Update starts.
			// Do not tear Unity down while those hooks are being installed.
			if (done && !exitRequested && Time.realtimeSinceStartup >= 75f)
			{
				exitRequested = true;
				MagnificationDownscale.Log("SCALE_PROBE_EXIT afterStartupSeconds=" + F(Time.realtimeSinceStartup));
				Application.Quit();
			}
		}

		private void Snapshot(string phase)
		{
			foreach (Rig rig in rigs)
			{
				if (rig.Hinge == null) continue;
				MagnificationDownscale.Log("SCALE_PROBE phase=" + phase + " rig=" + rig.Name
					+ " scale=" + V(rig.Own.transform.lossyScale)
					+ " hinge=" + V(rig.Hinge.anchor) + " hard=" + V(rig.Hard.anchor)
					+ " connectedHinge=" + V(rig.Hinge.connectedAnchor) + " connectedHard=" + V(rig.Hard.connectedAnchor)
					+ " hingeError=" + F(Error(rig.Hinge)) + " hardError=" + F(Error(rig.Hard))
					+ " pivotMismatch=" + F(Vector3.Distance(rig.Own.transform.TransformPoint(rig.Hinge.anchor), rig.Own.transform.TransformPoint(rig.Hard.anchor)))
					+ " scriptLookup=" + (rig.Hinge.anchor == new Vector3(4f, 8f, -3f)));
			}
		}

		private static float Error(Joint joint)
		{
			return Vector3.Distance(joint.transform.TransformPoint(joint.anchor), joint.connectedBody.transform.TransformPoint(joint.connectedAnchor));
		}
		private static string F(float value) { return value.ToString("R", CultureInfo.InvariantCulture); }
		private static string V(Vector3 value) { return F(value.x) + "," + F(value.y) + "," + F(value.z); }
		private sealed class Rig
		{
			internal string Name, Mode;
			internal float Scale;
			internal Rigidbody Own, Other;
			internal HingeJoint Hinge;
			internal ConfigurableJoint Hard;
		}
	}
}
