using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
	// Known static load, two parallel springs. Compare direct attachment with
	// transmission through light bodies. No machine/script/contact/controller.
	// The similarity flag changes physical units only. The old flag retains
	// the original constant-mass/gravity load experiment and its 32-case matrix.
	internal sealed class MagnificationCoupledDriveProbe : MonoBehaviour
	{
		private sealed class Rig
		{
			internal Rigidbody Main;
			internal readonly List<GameObject> Objects = new List<GameObject>();
			internal readonly List<ConfigurableJoint> Drives = new List<ConfigurableJoint>();
			internal float Scale, Cap, Sum, PeakVelocity, Force, Unit;
			internal int Id, Solver, Samples;
			internal bool Relay, Similarity;
		}
		private readonly List<Rig> rigs = new List<Rig>();
		private float originalStep, elapsed, unit = 1f;
		private int phase;
		private bool done, similarity;
		private static string Mode(bool value) { return value ? "same-time" : "legacy-load"; }
		private void Start()
		{
			try
			{
				similarity = Array.IndexOf(Environment.GetCommandLineArgs(), "--mpatcher-coupled-similarity-probe") >= 0;
				bool unit2 = Array.IndexOf(Environment.GetCommandLineArgs(), "--mpatcher-coupled-unit2") >= 0;
				if (unit2 && !similarity) throw new ArgumentException("coupled-unit2-requires-coupled-similarity-probe");
				// Diagnostic only: a common mass unit applied equally at 100% and
				// 5% moves tiny relay masses above Unity's 1e-7 setter floor while
				// retaining mass ratios and the equations of motion. No body floor.
				unit = unit2 ? 2f : 1f;
				originalStep = Time.fixedDeltaTime;
				Application.runInBackground = true;
				Create();
			}
			catch (Exception error) { Finish(error); }
		}
		private void Create()
		{
			Time.fixedDeltaTime = phase == 0 ? .01f : .002f; elapsed = 0f;
			foreach (float scale in new[] { 1f, .05f })
				foreach (bool relay in new[] { false, true })
					foreach (int solver in new[] { 30, 120 })
						foreach (float cap in new[] { 100f, 1000f })
						{
							Rig rig = new Rig { Id = phase * 16 + rigs.Count, Scale = scale, Cap = cap, Solver = solver, Relay = relay, Similarity = similarity, Unit = unit };
							rig.Force = 104f * rig.Unit * (similarity ? MagnificationSimilarityUnits.Force(scale) : 1f);
							rig.Main = Body(rig, "load", 1.3f, new Vector3(0f, 10f, rigs.Count * 2f));
							foreach (float side in new[] { -1f, 1f })
							{
								Vector3 anchor = new Vector3(side * .1f * scale, 0f, 0f);
								Rigidbody upper = rig.Main, lower = null;
								if (relay)
								{
									upper = Body(rig, "upper", .0005f, rig.Main.position + anchor);
									lower = Body(rig, "lower", .0005f, upper.position);
									FixedJoint upperLink = upper.gameObject.AddComponent<FixedJoint>();
									upperLink.autoConfigureConnectedAnchor = false; upperLink.connectedBody = rig.Main;
									upperLink.anchor = Vector3.zero; upperLink.connectedAnchor = anchor;
									FixedJoint lowerLink = lower.gameObject.AddComponent<FixedJoint>();
									lowerLink.autoConfigureConnectedAnchor = false;
									lowerLink.anchor = Vector3.zero; lowerLink.connectedAnchor = lower.position;
								}
								ConfigurableJoint joint = upper.gameObject.AddComponent<ConfigurableJoint>();
								joint.autoConfigureConnectedAnchor = false;
								joint.connectedBody = lower;
								joint.anchor = relay ? Vector3.zero : anchor;
								joint.connectedAnchor = relay ? Vector3.zero : rig.Main.position + anchor;
								joint.xMotion = joint.zMotion = ConfigurableJointMotion.Locked;
								joint.yMotion = ConfigurableJointMotion.Free;
								joint.angularXMotion = joint.angularYMotion = joint.angularZMotion = ConfigurableJointMotion.Locked;
								JointDrive requested = similarity
									? new JointDrive { positionSpring = 212.8f * rig.Unit * MagnificationSimilarityUnits.LinearSpring(scale), positionDamper = .4f * rig.Unit * MagnificationSimilarityUnits.LinearDamper(scale), maximumForce = cap * rig.Unit * MagnificationSimilarityUnits.Force(scale) }
									: new JointDrive { positionSpring = 212.8f / scale, positionDamper = .4f / Mathf.Sqrt(scale), maximumForce = cap };
								joint.yDrive = requested;
								rig.Drives.Add(joint);
								JointDrive actual = joint.yDrive;
								MagnificationDownscale.Log("COUPLED_DRIVE_PROBE_DRIVE mode=" + Mode(similarity) + " unit=" + F(rig.Unit) + " rig=" + rig.Id + " side=" + F(side)
									+ " scale=" + F(scale) + " relay=" + relay + " requestedK=" + F(requested.positionSpring) + " actualK=" + F(actual.positionSpring)
									+ " requestedC=" + F(requested.positionDamper) + " actualC=" + F(actual.positionDamper)
									+ " requestedMaxForce=" + F(requested.maximumForce) + " actualMaxForce=" + F(actual.maximumForce));
							}
							rigs.Add(rig);
						}
			MagnificationDownscale.Log("COUPLED_DRIVE_PROBE_START phase=" + phase + " rigs=" + rigs.Count + " step=" + F(Time.fixedDeltaTime)
				+ " duration=8 load=104 sourceSpringEach=212.8 relayMass=0.0005 expectedSagCraft=" + F(104f / (2f * 212.8f))
				+ " rawUnity=True mode=" + Mode(similarity) + " unit=" + F(unit) + " unitScope=diagnostic-all-mass-dimensional-coefficients-both-scales"
				+ " policy=" + (similarity ? "length=s,mass=s3,inertia=s5,force=s4,spring=s3,damper=s3,time=1" : "mass=1,inertia=s2,force=1,spring=1/s,damper=1/sqrt(s)"));
		}
		private static Rigidbody Body(Rig rig, string label, float mass, Vector3 position)
		{
			GameObject obj = new GameObject("MPatcher_CoupledDrive_" + label); rig.Objects.Add(obj); obj.transform.position = position;
			float requestedMass = mass * rig.Unit * (rig.Similarity ? MagnificationSimilarityUnits.Mass(rig.Scale) : 1f);
			float requestedInertia = rig.Unit * (rig.Similarity ? MagnificationSimilarityUnits.Inertia(rig.Scale) : rig.Scale * rig.Scale);
			Rigidbody body = obj.AddComponent<Rigidbody>(); body.mass = requestedMass;
			if (rig.Similarity && requestedMass < 1e-7f) MagnificationJointMassNative.WriteSmallMass(body, requestedMass);
			body.useGravity = body.detectCollisions = false; body.drag = body.angularDrag = body.sleepThreshold = 0f;
			// Source tensor is (1,1,1), including the relay bodies. At 5% the
			// requested 3.125e-7 * Unit exceeds the verified Unity setter cutoff.
			body.inertiaTensor = Vector3.one * requestedInertia;
			body.solverIterations = rig.Solver; body.solverVelocityIterations = rig.Solver == 30 ? 20 : 120;
			body.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
			MagnificationDownscale.Log("COUPLED_DRIVE_PROBE_BODY mode=" + Mode(rig.Similarity) + " unit=" + F(rig.Unit) + " rig=" + rig.Id + " body=" + label + " index=" + (rig.Objects.Count - 1)
				+ " scale=" + F(rig.Scale) + " sourceMass=" + F(mass) + " requestedMass=" + F(requestedMass) + " actualMass=" + F(body.mass)
				+ " requestedInertia=" + F(requestedInertia) + " actualInertia=" + body.inertiaTensor.ToString("R")
				+ " requestedSolver=" + rig.Solver + "/" + (rig.Solver == 30 ? 20 : 120) + " actualSolver=" + body.solverIterations + "/" + body.solverVelocityIterations
				+ " useGravity=" + body.useGravity + " detectCollisions=" + body.detectCollisions);
			return body;
		}
		private void FixedUpdate()
		{
			if (done || rigs.Count == 0) return;
			try
			{
				if (elapsed >= 7f) foreach (Rig rig in rigs)
				{
					rig.Sum += (10f - rig.Main.position.y) / rig.Scale; rig.Samples++;
					rig.PeakVelocity = Mathf.Max(rig.PeakVelocity, Mathf.Abs(rig.Main.velocity.y));
				}
				if (elapsed >= 8f)
				{
					foreach (Rig rig in rigs)
					{
						JointDrive drive = rig.Drives[0].yDrive;
						// For either policy: load/(2*K*scale) = 104/(2*212.8).
						// This is static equilibrium, not a claim that the last-second
						// transient mean has converged or that Unity obeyed requests.
						MagnificationDownscale.Log("COUPLED_DRIVE_PROBE_RESULT step=" + F(Time.fixedDeltaTime) + " scale=" + F(rig.Scale)
							+ " relay=" + rig.Relay + " solver=" + rig.Solver + " maxForce=" + F(rig.Cap) + " sagCraftMean=" + F(rig.Sum / rig.Samples)
							+ " expected=" + F(104f / (2f * 212.8f)) + " velocityPeak=" + F(rig.PeakVelocity) + " mode=" + Mode(similarity) + " unit=" + F(rig.Unit) + " rig=" + rig.Id + " velocityPeakCraft=" + F(rig.PeakVelocity / rig.Scale)
							+ " actualMainMass=" + F(rig.Main.mass) + " actualMainInertia=" + rig.Main.inertiaTensor.ToString("R")
							+ " actualSolver=" + rig.Main.solverIterations + "/" + rig.Main.solverVelocityIterations
							+ " actualK=" + F(drive.positionSpring) + " actualC=" + F(drive.positionDamper) + " actualMaxForce=" + F(drive.maximumForce)
							+ " physicalLoad=" + F(rig.Force) + " equilibriumFromReadback=" + F(rig.Force / (2f * drive.positionSpring * rig.Scale)));
						foreach (GameObject obj in rig.Objects) Destroy(obj);
					}
					rigs.Clear(); phase++;
					if (phase == 2) { MagnificationDownscale.Log("COUPLED_DRIVE_PROBE_COMPLETE completed=True cases=32 mode=" + Mode(similarity) + " unit=" + F(unit)); Finish(null); }
					else Create();
					return;
				}
				foreach (Rig rig in rigs) rig.Main.AddForce(Vector3.down * rig.Force, ForceMode.Force);
				elapsed += Time.fixedDeltaTime;
			}
			catch (Exception error) { Finish(error); }
		}
		private void Finish(Exception error)
		{
			done = true; if (originalStep > 0f) Time.fixedDeltaTime = originalStep;
			if (error != null) MagnificationDownscale.Log("COUPLED_DRIVE_PROBE_FAILED mode=" + Mode(similarity) + " unit=" + F(unit) + " " + error);
			Application.Quit();
		}
		private static string F(float value) { return value.ToString("R", CultureInfo.InvariantCulture); }
	}
}
