using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
	// Isolated, opt-in dimensional checks. No craft or global physics settings are changed.
	internal sealed class MagnificationSuspensionProbe : MonoBehaviour
	{
		private readonly List<Rig> rigs = new List<Rig>();
		private int frame;
		private bool ready, done, exiting;
		private IEnumerator Start()
		{
			while (Time.realtimeSinceStartup < 60f) yield return null;
			foreach (float scale in new[] { 1f, 0.1f, 0.05f })
				foreach (float mass in new[] { 0.325f, 1.3f })
					foreach (string policy in new[] { "native", "spring-inverse-scale", "spring-damper-inverse-scale" })
					{
						Rig rig = Create(scale, mass, "piston/" + policy);
						rig.Joint = rig.Body.gameObject.AddComponent<ConfigurableJoint>();
						rig.Joint.autoConfigureConnectedAnchor = false;
						rig.Joint.anchor = Vector3.zero;
						rig.Joint.connectedAnchor = rig.Origin;
						rig.Joint.xMotion = rig.Joint.zMotion = ConfigurableJointMotion.Locked;
						rig.Joint.yMotion = ConfigurableJointMotion.Limited;
						rig.Joint.angularXMotion = rig.Joint.angularYMotion = rig.Joint.angularZMotion = ConfigurableJointMotion.Locked;
						rig.Joint.linearLimit = new SoftJointLimit { limit = 5f * scale };
						float spring = policy == "native" ? 600f : 600f / scale;
						float damper = policy == "native" ? 6f : 6f / (policy == "spring-inverse-scale" ? Mathf.Sqrt(scale) : scale);
						rig.Joint.yDrive = policy == "spring-inverse-scale"
							? MagnificationPistonDrive.NormalizeDrive(new JointDrive { positionSpring = 600f, positionDamper = 6f, maximumForce = 100f }, scale)
							: new JointDrive { positionSpring = spring, positionDamper = damper, maximumForce = 100f };
						// A nonzero target also verifies the sign and units of the gameplay diagnostics.
						rig.Joint.targetPosition = new Vector3(0f, scale, 0f);
						rig.Body.position = rig.Origin - Vector3.up * scale;
					}
			foreach (float scale in new[] { 1f, 0.05f })
				foreach (string policy in new[] { "native-inertia", "scaled-inertia" })
				{
					Rig rig = Create(scale, 1.3f, "rotor/" + policy);
					rig.Rotor = true;
					rig.Body.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
					rig.Body.inertiaTensor = Vector3.one * 55f * (policy == "scaled-inertia" ? scale * scale : 1f);
				}
			Log("SUSPENSION_PROBE_START revision=2 rigs=" + rigs.Count + " fixedDelta=" + F(Time.fixedDeltaTime) + " gravity=80 mass=unchanged spring=600 damper=6 forceCap=100 constraints=world singlePiston=true correction=production-helper");
			ready = true;
		}
		private Rig Create(float scale, float mass, string policy)
		{
			Rig rig = new Rig { Scale = scale, Name = policy + "/s" + F(scale) + "/m" + F(mass), Origin = new Vector3(rigs.Count * 20f, 100f, 0f) };
			GameObject obj = new GameObject("SuspensionProbe/" + rig.Name);
			obj.transform.position = rig.Origin;
			obj.transform.localScale = Vector3.one * scale;
			rig.Body = obj.AddComponent<Rigidbody>();
			rig.Body.mass = mass;
			rig.Body.useGravity = false;
			rig.Body.drag = rig.Body.angularDrag = 0f;
			rig.Body.centerOfMass = Vector3.zero;
			rig.Body.inertiaTensor = Vector3.one;
			rig.Body.maxAngularVelocity = 1000f;
			rig.Body.solverIterations = 30;
			rig.Body.solverVelocityIterations = 20;
			rigs.Add(rig);
			return rig;
		}
		private void FixedUpdate()
		{
			if (!ready || done) return;
			frame++;
			foreach (Rig rig in rigs)
			{
				if (rig.Rotor)
				{
					// Constant force at a proportionally shorter lever: torque scales linearly.
					if (frame <= 100) rig.Body.AddTorque(Vector3.forward * (55f * rig.Scale), ForceMode.Force);
				}
				else rig.Body.AddForce(Vector3.down * 80f, ForceMode.Acceleration);
				float axial = rig.Origin.y - rig.Body.position.y;
				float error = rig.Rotor ? 0f : (axial - rig.Joint.targetPosition.y) / rig.Scale;
				if (frame > 800) { rig.ErrorSum += error; rig.VelocityPeak = Mathf.Max(rig.VelocityPeak, rig.Body.velocity.magnitude); rig.Samples++; }
				if (frame == 100 || frame == 250 || frame == 1000)
					Log("SUSPENSION_PROBE frame=" + frame + " rig=" + rig.Name + " axial=" + F(axial)
						+ " target=" + F(rig.Rotor ? 0f : rig.Joint.targetPosition.y) + " errorCraft=" + F(error)
						+ " finalMeanError=" + F(rig.Samples == 0 ? 0f : rig.ErrorSum / rig.Samples)
						+ " velocity=" + F(rig.Body.velocity.magnitude) + " finalVelocityPeak=" + F(rig.VelocityPeak)
						+ " angular=" + F(rig.Body.angularVelocity.z) + " inertia=" + F(rig.Body.inertiaTensor.z)
						+ " spring=" + F(rig.Rotor ? 0f : rig.Joint.yDrive.positionSpring)
						+ " damper=" + F(rig.Rotor ? 0f : rig.Joint.yDrive.positionDamper));
			}
			if (frame == 1000)
			{
				done = true;
				foreach (Rig rig in rigs) Destroy(rig.Body.gameObject);
				Log("SUSPENSION_PROBE_COMPLETE frames=" + frame);
			}
		}
		private void Update()
		{
			if (done && !exiting && Time.realtimeSinceStartup >= 75f) { exiting = true; Log("SUSPENSION_PROBE_EXIT"); Application.Quit(); }
		}
		private static string F(float value) { return value.ToString("R", CultureInfo.InvariantCulture); }
		private static void Log(string value) { MagnificationDownscale.Log(value); }
		private sealed class Rig
		{
			internal string Name;
			internal float Scale, ErrorSum, VelocityPeak;
			internal int Samples;
			internal bool Rotor;
			internal Vector3 Origin;
			internal Rigidbody Body;
			internal ConfigurableJoint Joint;
		}
	}
}
