using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
	// Opt-in isolated solver experiment: rotating loaded chains, no colliders.
	internal sealed class MagnificationStressProbe : MonoBehaviour
	{
		private readonly List<Rig> rigs = new List<Rig>();
		private int frame;
		private bool initialized, done, exiting;
		private IEnumerator Start()
		{
			while (Time.realtimeSinceStartup < 60f) yield return null;
			CreateRigs();
			initialized = true;
		}
		private void CreateRigs()
		{
			bool managedSuite = Array.IndexOf(Environment.GetCommandLineArgs(), "--mpatcher-stress-managed-suite") >= 0;
			bool inertiaSuite = Array.IndexOf(Environment.GetCommandLineArgs(), "--mpatcher-stress-inertia-suite") >= 0;
			string[] policies = inertiaSuite
				? new[] { "native", "driver-both10", "driver-inertia-square", "driver-inertia-square-angular", "driver-inertia-linear" }
				: managedSuite
				? new[] { "native", "iterations", "projection", "combined", "preprocessing-off", "inertia", "inertia-no-preprocessing", "hinge-only", "reverse-order", "mass-ratio10", "mass-ratio100", "soft-spring" }
				: new[] { "native", "joint-both10", "driver-both10", "driver-both10-rebuild", "driver-both10-masschange" };
			foreach (float scale in new[] { 0.05f, 1f })
				foreach (bool uneven in new[] { false, true })
					foreach (string policy in policies)
					{
						bool scaleInertia = policy.StartsWith("inertia", StringComparison.Ordinal);
						bool noPreprocessing = policy == "preprocessing-off" || policy == "inertia-no-preprocessing";
						float inertiaFactor = scaleInertia || policy.StartsWith("driver-inertia-square", StringComparison.Ordinal) ? scale * scale
							: policy == "driver-inertia-linear" ? scale : 1f;
						float angularSpringFactor = scaleInertia ? inertiaFactor : policy == "driver-inertia-square-angular" ? scale : 1f;
						float angularDamperFactor = scaleInertia ? inertiaFactor : policy == "driver-inertia-square-angular" ? scale * Mathf.Sqrt(scale) : 1f;
						Rig rig = new Rig();
						rig.Policy = policy;
						rig.Scale = scale;
						rig.Name = F(scale) + "/" + (uneven ? "ratio2600" : "equal") + "/" + policy;
						Vector3 origin = new Vector3(0, 100, rigs.Count * 20);
						for (int i = 0; i < 4; i++)
						{
							GameObject obj = new GameObject("StressProbe/" + rig.Name + "/" + i);
							obj.transform.position = origin;
							obj.transform.localScale = Vector3.one * scale;
							Rigidbody body = obj.AddComponent<Rigidbody>();
							body.useGravity = false;
							body.mass = uneven ? (i % 2 == 0 ? 1.3f : 0.0005f) : 1.3f;
							if (policy == "mass-ratio10") body.mass = Mathf.Max(body.mass, 0.13f);
							if (policy == "mass-ratio100") body.mass = Mathf.Max(body.mass, 0.013f);
							body.centerOfMass = new Vector3(i * 5f, 0, 0) * scale;
							body.inertiaTensor = Vector3.one * (uneven && i % 2 != 0 ? 0.0522f : 55f) * inertiaFactor;
							body.maxAngularVelocity = 100;
							body.solverIterations = policy == "iterations" || policy == "combined" ? 120 : 30;
							body.solverVelocityIterations = policy == "iterations" || policy == "combined" ? 80 : 20;
							rig.Bodies.Add(body);
							if (i == 0) body.gameObject.AddComponent<FixedJoint>().enablePreprocessing = !noPreprocessing;
							else
							{
								GameObject jointOwner = policy == "reverse-order" ? rig.Bodies[i - 1].gameObject : obj;
								HingeJoint hinge = jointOwner.AddComponent<HingeJoint>();
								hinge.axis = Vector3.forward;
								hinge.spring = new JointSpring { spring = 3000f * angularSpringFactor, damper = 300f * angularDamperFactor, targetPosition = 0f };
								hinge.useSpring = true;
								if (policy == "soft-spring") hinge.useSpring = false;
								ConfigurableJoint hard = jointOwner.AddComponent<ConfigurableJoint>();
								hard.xMotion = hard.yMotion = hard.zMotion = ConfigurableJointMotion.Locked;
								hard.angularXMotion = hard.angularYMotion = hard.angularZMotion = ConfigurableJointMotion.Free;
								hard.projectionMode = JointProjectionMode.PositionAndRotation;
								hard.projectionDistance = 0.001f * (policy == "projection" || policy == "combined" ? scale : 1f);
								foreach (Joint joint in new Joint[] { hinge, hard })
								{
									joint.connectedBody = policy == "reverse-order" ? body : rig.Bodies[i - 1];
									joint.autoConfigureConnectedAnchor = false;
									joint.enablePreprocessing = !noPreprocessing;
									joint.anchor = joint.connectedAnchor = new Vector3(i * 5f - 2.5f, 0, 0);
									rig.Joints.Add(joint);
									if (rigs.Count == 0 && i == 1) MagnificationNativeJointProbe.Inspect(joint);
								}
								if (policy == "hinge-only") { rig.Joints.Remove(hard); Destroy(hard); }
							}
						}
						if (policy.StartsWith("driver-", StringComparison.Ordinal)) rig.Driver = MagnificationJointMassBalance.AttachProbe(rig.Bodies, scale);
						rigs.Add(rig);
					}
			Log("STRESS_PROBE_START revision=9 suite=" + (inertiaSuite ? "inertia" : managedSuite ? "managed" : "native") + " rigs=" + rigs.Count + " rotation=free acceleration=80 colliders=none frames=1000 fixedDelta=" + F(Time.fixedDeltaTime));
		}
		private void FixedUpdate()
		{
			if (!initialized || done) return;
			frame++;
			foreach (Rig rig in rigs)
			{
				if (frame == 500 && rig.Policy.EndsWith("-masschange", StringComparison.Ordinal))
				{
					rig.Bodies[0].mass = rig.Bodies[2].mass = 2.6f;
					rig.Bodies[1].mass = rig.Bodies[3].mass = 0.00025f;
					Log("STRESS_PROBE_LIFECYCLE action=masschange rig=" + rig.Name + " ratio=10400");
				}
				if (frame == 500 && rig.Policy.EndsWith("-rebuild", StringComparison.Ordinal))
				{
					foreach (Joint joint in rig.Joints) Destroy(joint);
					rig.Joints.Clear();
					Log("STRESS_PROBE_LIFECYCLE action=destroy-joints rig=" + rig.Name);
				}
				if (frame == 502 && rig.Policy.EndsWith("-rebuild", StringComparison.Ordinal))
				{
					for (int i = 1; i < rig.Bodies.Count; i++)
					{
						HingeJoint hinge = rig.Bodies[i].gameObject.AddComponent<HingeJoint>();
						hinge.axis = Vector3.forward;
						hinge.spring = new JointSpring { spring = 3000f, damper = 300f, targetPosition = 0f };
						hinge.useSpring = true;
						ConfigurableJoint hard = rig.Bodies[i].gameObject.AddComponent<ConfigurableJoint>();
						hard.xMotion = hard.yMotion = hard.zMotion = ConfigurableJointMotion.Locked;
						hard.angularXMotion = hard.angularYMotion = hard.angularZMotion = ConfigurableJointMotion.Free;
						hard.projectionMode = JointProjectionMode.PositionAndRotation;
						hard.projectionDistance = 0.001f;
						foreach (Joint joint in new Joint[] { hinge, hard })
						{
							joint.connectedBody = rig.Bodies[i - 1];
							joint.autoConfigureConnectedAnchor = false;
							joint.anchor = joint.connectedAnchor = new Vector3(i * 5f - 2.5f, 0, 0);
							rig.Joints.Add(joint);
						}
					}
					Log("STRESS_PROBE_LIFECYCLE action=recreate-joints rig=" + rig.Name);
					if (rig.Driver != null) rig.Driver.RequestRefresh();
				}
				bool refresh = frame == 2 || (frame == 500 && rig.Policy == "joint-both10-masschange") || (frame == 503 && rig.Policy == "joint-both10-rebuild");
				if (refresh && rig.Policy.StartsWith("joint-", StringComparison.Ordinal))
					foreach (Joint joint in rig.Joints)
						if (!MagnificationNativeJointProbe.Balance(joint, rig.Policy == "joint-mass100" ? 100f : 10f, rig.Policy.StartsWith("joint-both", StringComparison.Ordinal))) rig.Failed = true;
				float gap = 0, velocity = 0, angular = 0;
				foreach (Joint joint in rig.Joints)
					gap = Mathf.Max(gap, Vector3.Distance(joint.transform.TransformPoint(joint.anchor), joint.connectedBody.transform.TransformPoint(joint.connectedAnchor)));
				foreach (Rigidbody body in rig.Bodies)
				{
					velocity = Mathf.Max(velocity, body.velocity.magnitude);
					angular = Mathf.Max(angular, body.angularVelocity.magnitude);
					body.AddForce(Vector3.down * 80f, ForceMode.Acceleration);
				}
				if (frame > 5) rig.Peak = Mathf.Max(rig.Peak, gap / rig.Scale);
				if (frame == 125 || frame == 500 || frame == 1000)
					Log("STRESS_PROBE frame=" + frame + " rig=" + rig.Name + " gap=" + F(gap)
						+ " relativeGap=" + F(gap / rig.Scale) + " peakRelativeGap=" + F(rig.Peak)
						+ " velocity=" + F(velocity) + " angular=" + F(angular)
						+ " solver=" + rig.Bodies[1].solverIterations + "/" + rig.Bodies[1].solverVelocityIterations
						+ " preprocessing=" + (rig.Joints.Count != 0 && rig.Joints[0].enablePreprocessing) + " failed=" + rig.Failed
						+ " fixedDelta=" + F(Time.fixedDeltaTime) + " defaultSolver=" + Physics.defaultSolverIterations + "/" + Physics.defaultSolverVelocityIterations);
			}
			if (frame >= 1000)
			{
				done = true;
				MagnificationNativeJointProbe.Release();
				foreach (Rig rig in rigs) foreach (Rigidbody body in rig.Bodies) Destroy(body.gameObject);
				Log("STRESS_PROBE_COMPLETE");
			}
		}
		private void LateUpdate()
		{
			if (done && !exiting && Time.realtimeSinceStartup >= 75f)
			{
				exiting = true;
				Log("STRESS_PROBE_EXIT");
				Application.Quit();
			}
		}
		private static void Log(string message) { MagnificationDownscale.Log(message); }
		private static string F(float value) { return value.ToString("R", CultureInfo.InvariantCulture); }
		private sealed class Rig
		{
			internal string Name, Policy;
			internal float Scale, Peak;
			internal bool Failed;
			internal MagnificationJointMassBalance Driver;
			internal readonly List<Rigidbody> Bodies = new List<Rigidbody>();
			internal readonly List<Joint> Joints = new List<Joint>();
		}
	}
}
