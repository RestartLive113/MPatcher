using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
	// Isolated diagnostic only: free translation, fixed rotation, no gravity,
	// colliders, motors, or craft scripts. Detect the solver's anchor units.
	internal sealed class MagnificationDynamicProbe : MonoBehaviour
	{
		private readonly List<Rig> rigs = new List<Rig>();
		private int frame;
		private bool complete, exitRequested;
		private void Start()
		{
			foreach (float scale in new[] { 0.05f, 0.5f, 1f, 2f, 3f })
				foreach (bool hinge in new[] { true, false })
					foreach (bool worldUnits in new[] { false, true })
					foreach (bool lateScale in new[] { false, true })
					{
						Rig rig = new Rig();
						rig.Name = (hinge ? "hinge" : "hard") + "/" + (worldUnits ? "unscaled-frame" : "body-local") + "/" + F(scale) + (lateScale ? "/late-scale" : "/early-scale");
						rig.Start = new Vector3(0f, 1000f, rigs.Count * 40f);
						rig.Own = Body(rig.Name + "/dynamic", rig.Start, lateScale ? 1f : scale, false);
						rig.Other = Body(rig.Name + "/kinematic", rig.Start + Vector3.right * (10f * scale), lateScale ? 1f : scale, true);
						rig.Joint = hinge ? (Joint)rig.Own.gameObject.AddComponent<HingeJoint>()
							: rig.Own.gameObject.AddComponent<ConfigurableJoint>();
						rig.Joint.connectedBody = rig.Other;
						rig.Joint.autoConfigureConnectedAnchor = false;
						ConfigurableJoint hard = rig.Joint as ConfigurableJoint;
						if (hard != null)
						{
							hard.xMotion = hard.yMotion = hard.zMotion = ConfigurableJointMotion.Locked;
							hard.angularXMotion = hard.angularYMotion = hard.angularZMotion = ConfigurableJointMotion.Free;
						}
						Vector3 local = new Vector3(4f, 8f, -3f);
						if (lateScale)
						{
							rig.Joint.anchor = local * scale;
							rig.Joint.connectedAnchor = rig.Other.transform.InverseTransformPoint(rig.Own.transform.TransformPoint(rig.Joint.anchor));
							rig.Own.transform.localScale = rig.Other.transform.localScale = Vector3.one * scale;
						}
						Vector3 world = rig.Own.transform.TransformPoint(local);
						rig.Joint.anchor = worldUnits ? local * scale : local;
						rig.Joint.connectedAnchor = worldUnits
							? Quaternion.Inverse(rig.Other.rotation) * (world - rig.Other.position)
							: rig.Other.transform.InverseTransformPoint(world);
						rigs.Add(rig);
					}
			Log("DYNAMIC_PROBE_START revision=3 rigs=" + rigs.Count + " gravity=false colliders=none rotation=frozen translation=free hardLinear=locked impulseAt=20 lateScale=covered");
			Snapshot("created");
		}
		private static Rigidbody Body(string name, Vector3 position, float scale, bool kinematic)
		{
			GameObject obj = new GameObject("DynamicProbe/" + name);
			obj.transform.position = position;
			obj.transform.localScale = Vector3.one * scale;
			Rigidbody body = obj.AddComponent<Rigidbody>();
			body.mass = 1f;
			body.useGravity = false;
			body.isKinematic = kinematic;
			body.constraints = RigidbodyConstraints.FreezeRotation;
			return body;
		}
		private void FixedUpdate()
		{
			if (complete) return;
			frame++;
			if (frame == 20)
				foreach (Rig rig in rigs) rig.Own.AddForce(Vector3.right, ForceMode.VelocityChange);
			if (frame == 1 || frame == 2 || frame == 10 || frame == 20 || frame == 21 || frame == 125) Snapshot("fixed-" + frame);
			if (frame >= 125)
			{
				complete = true;
				foreach (Rig rig in rigs) { Destroy(rig.Own.gameObject); Destroy(rig.Other.gameObject); }
				Log("DYNAMIC_PROBE_COMPLETE frames=" + frame);
			}
		}
		private void LateUpdate()
		{
			if (complete && !exitRequested && Time.realtimeSinceStartup >= 75f)
			{
				exitRequested = true;
				Log("DYNAMIC_PROBE_EXIT");
				Application.Quit();
			}
		}
		private void Snapshot(string phase)
		{
			foreach (Rig rig in rigs)
			{
				Vector3 ownLocalWorld = rig.Own.transform.TransformPoint(rig.Joint.anchor);
				Vector3 otherLocalWorld = rig.Other.transform.TransformPoint(rig.Joint.connectedAnchor);
				Vector3 ownUnscaledWorld = rig.Own.position + rig.Own.rotation * rig.Joint.anchor;
				Vector3 otherUnscaledWorld = rig.Other.position + rig.Other.rotation * rig.Joint.connectedAnchor;
				Log("DYNAMIC_PROBE phase=" + phase + " rig=" + rig.Name
					+ " displacement=" + V(rig.Own.position - rig.Start) + " velocity=" + V(rig.Own.velocity)
					+ " transformPointError=" + F(Vector3.Distance(ownLocalWorld, otherLocalWorld))
					+ " unscaledFrameError=" + F(Vector3.Distance(ownUnscaledWorld, otherUnscaledWorld))
					+ " anchor=" + V(rig.Joint.anchor) + " connected=" + V(rig.Joint.connectedAnchor));
			}
		}
		private static void Log(string message) { MagnificationDownscale.Log(message); }
		private static string F(float value) { return value.ToString("R", CultureInfo.InvariantCulture); }
		private static string V(Vector3 value) { return F(value.x) + "," + F(value.y) + "," + F(value.z); }
		private sealed class Rig
		{
			internal string Name;
			internal Vector3 Start;
			internal Rigidbody Own, Other;
			internal Joint Joint;
		}
	}
}
