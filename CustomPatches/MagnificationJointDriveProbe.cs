using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
	// Isolated native Unity experiment: establish drive dimensions independently
	// of the machine's hierarchy, scripts, contacts and research conversions.
	internal sealed class MagnificationJointDriveProbe : MonoBehaviour
	{
		private sealed class Sample
		{
			internal string Name;
			internal Rigidbody Body;
			internal ConfigurableJoint Joint;
			internal float Load, Expected, Sum;
			internal bool Limited;
		}
		private readonly List<Sample> samples = new List<Sample>();
		private int step;
		private bool done;
		private float previousStep;
		private void Start()
		{
			try
			{
				previousStep = Time.fixedDeltaTime; Time.fixedDeltaTime = .01f;
				Application.runInBackground = true;
				Add("mass1", 1f, 1000f, 100f, 1000f, 2f, false);
				Add("mass10", 10f, 1000f, 100f, 1000f, 20f, false);
				Add("density10-unit1", .001f, 10f, .316227766f, 1f, .002f, false);
				Add("density10-unit8000", 8f, 80000f, 2529.82213f, 8000f, 16f, false);
				Add("limited-force1", 1f, 1000f, 100f, 1f, 2f, true);
				MagnificationDownscale.Log("JOINT_DRIVE_PROBE_START samples=" + samples.Count + " step=0.01 duration=5 rawUnity=True");
			}
			catch (Exception ex) { Finish(ex); }
		}
		private void Add(string name, float mass, float spring, float damper, float maximumForce, float load, bool limited)
		{
			GameObject obj = new GameObject("MPatcher_JointDriveProbe_" + name);
			obj.transform.position = new Vector3(0f, 100f + samples.Count * 10f, 0f);
			Rigidbody body = obj.AddComponent<Rigidbody>();
			body.useGravity = false; body.detectCollisions = false;
			body.drag = body.angularDrag = body.sleepThreshold = 0f;
			body.mass = mass; body.inertiaTensor = Vector3.one;
			body.solverIterations = 30; body.solverVelocityIterations = 20;
			ConfigurableJoint joint = obj.AddComponent<ConfigurableJoint>();
			joint.autoConfigureConnectedAnchor = false;
			joint.anchor = Vector3.zero; joint.connectedAnchor = body.position;
			joint.xMotion = ConfigurableJointMotion.Free;
			joint.yMotion = joint.zMotion = ConfigurableJointMotion.Locked;
			joint.angularXMotion = joint.angularYMotion = joint.angularZMotion = ConfigurableJointMotion.Locked;
			joint.xDrive = new JointDrive { positionSpring = spring, positionDamper = damper, maximumForce = maximumForce };
			samples.Add(new Sample { Name = name, Body = body, Joint = joint, Load = load, Expected = load / spring, Limited = limited });
		}
		private void FixedUpdate()
		{
			if (done || samples.Count == 0) return;
			try
			{
				if (step >= 400 && step < 500) foreach (Sample sample in samples) sample.Sum += sample.Body.position.x;
				if (step == 500)
				{
					bool springs = true, forceCap = true;
					foreach (Sample sample in samples)
					{
						float mean = sample.Sum / 100f;
						bool pass = sample.Limited ? sample.Body.velocity.x > 4f : Math.Abs(mean - sample.Expected) < sample.Expected * .03f;
						if (sample.Limited) forceCap &= pass; else springs &= pass;
						MagnificationDownscale.Log("JOINT_DRIVE_PROBE_RESULT name=" + sample.Name + " mass=" + F(sample.Body.mass)
							+ " spring=" + F(sample.Joint.xDrive.positionSpring) + " maxForce=" + F(sample.Joint.xDrive.maximumForce)
							+ " load=" + F(sample.Load) + " forceBasedExpected=" + F(sample.Expected) + " displacementMean=" + F(mean)
							+ " velocity=" + F(sample.Body.velocity.x) + " limited=" + sample.Limited + " forceModelPass=" + pass);
					}
					MagnificationDownscale.Log("JOINT_DRIVE_PROBE_COMPLETE completed=True forceBasedSpring=" + springs + " forceBasedLimit=" + forceCap);
					Finish(null); return;
				}
				foreach (Sample sample in samples) sample.Body.AddForce(Vector3.right * sample.Load, ForceMode.Force);
				step++;
			}
			catch (Exception ex) { Finish(ex); }
		}
		private void Finish(Exception error)
		{
			done = true;
			if (previousStep > 0f) Time.fixedDeltaTime = previousStep;
			if (error != null) MagnificationDownscale.Log("JOINT_DRIVE_PROBE_FAILED " + error);
			Application.Quit();
		}
		private static string F(float value) { return value.ToString("R", CultureInfo.InvariantCulture); }
	}
}
