using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
	// Raw opt-in Unity/PhysX experiment, started before research Harmony hooks.
	// Tests the actual inverse mass through F=m*a, not only a getter/cache.
	// The second phase checks a geometry change and Unity large -> native tiny
	// mass rewrite without resetting the original auto/manual inertia mode.
	// Center-applied force needs no rotation constraints. Such constraints
	// changed manual inertia readback even in the unpatched Unity controls.
	internal sealed class MagnificationMassNativeProbe : MonoBehaviour
	{
		private sealed class Sample
		{
			internal GameObject Object;
			internal Rigidbody Body;
			internal BoxCollider Box;
			internal Vector3 Origin;
			internal float Mass;
			internal bool Manual, PhasePass = true;
			internal int Id;
		}
		private const int ForceSteps = 100;
		private const float FixedStep = .01f;
		private static readonly Vector3 ManualTensor = new Vector3(2f, 3f, 4f);
		private readonly List<Sample> samples = new List<Sample>();
		private float originalStep;
		private int phase, appliedSteps, failures, completed;
		private bool done;

		private void Start()
		{
			try
			{
				originalStep = Time.fixedDeltaTime;
				Time.fixedDeltaTime = FixedStep;
				Application.runInBackground = true;
				foreach (float mass in new[] { .001f, 1e-7f, 6.25e-8f, 1e-9f, 1e-10f })
				foreach (bool manual in new[] { false, true })
				{
					Sample sample = new Sample { Id = samples.Count, Mass = mass, Manual = manual };
					samples.Add(sample);
					sample.Origin = new Vector3(0f, 0f, sample.Id * 3f);
					sample.Object = new GameObject("MPatcher_MassNativeProbe_" + sample.Id);
					sample.Object.transform.position = sample.Origin;
					sample.Box = sample.Object.AddComponent<BoxCollider>();
					sample.Box.size = Vector3.one;
					sample.Body = sample.Object.AddComponent<Rigidbody>();
					sample.Body.useGravity = false;
					sample.Body.detectCollisions = false;
					sample.Body.drag = sample.Body.angularDrag = sample.Body.sleepThreshold = 0f;
					sample.Body.mass = .001f;
					if (manual) sample.Body.inertiaTensor = ManualTensor;
					else sample.Body.ResetInertiaTensor();
					ApplyRequestedMass(sample, "initial");
				}
				MagnificationDownscale.Log("MASS_NATIVE_PROBE_START samples=" + samples.Count
					+ " phases=2 stepsPerPhase=" + ForceSteps + " step=" + F(FixedStep)
					+ " acceleration=2 expectedVelocity=2 expectedDisplacement=1.01 gravity=False collisions=False constraints=None torque=0 inertiaModes=auto,manual nativeRoute=mass<1e-7 rawUnity=True");
			}
			catch (Exception error) { Finish(error); }
		}

		private void ApplyRequestedMass(Sample sample, string reason)
		{
			// Record the stock setter result first, then test the native bridge.
			// Threshold and larger masses deliberately remain Unity controls.
			sample.Body.mass = sample.Mass;
			float stock = sample.Body.mass;
			Vector3 stockTensor = sample.Body.inertiaTensor;
			bool native = sample.Mass < 1e-7f;
			if (native) MagnificationJointMassNative.WriteSmallMass(sample.Body, sample.Mass);
			float actual = sample.Body.mass;
			Vector3 tensor = sample.Body.inertiaTensor;
			Vector3 expectedTensor = sample.Manual ? ManualTensor : BoxTensor(sample.Mass, sample.Box.size);
			bool massPass = Relative(actual, sample.Mass, .0001f), tensorPass = TensorMatches(tensor, expectedTensor);
			Check(sample, massPass && tensorPass);
			MagnificationDownscale.Log("MASS_NATIVE_PROBE_READBACK phase=" + phase + " sample=" + sample.Id + " reason=" + reason
				+ " manualInertia=" + sample.Manual + " route=" + (native ? "native-small" : "unity-control")
				+ " requestedMass=" + F(sample.Mass) + " stockMass=" + F(stock) + " actualMass=" + F(actual)
				+ " stockInertia=" + stockTensor.ToString("R") + " expectedInertia=" + expectedTensor.ToString("R") + " actualInertia=" + tensor.ToString("R")
				+ " massPass=" + massPass + " inertiaPass=" + tensorPass);
		}

		private void FixedUpdate()
		{
			if (done || samples.Count == 0) return;
			try
			{
				if (Math.Abs(Time.fixedDeltaTime - FixedStep) > 1e-7f) throw new InvalidOperationException("mass-native-probe-fixed-step-changed");
				if (appliedSteps == 1)
					foreach (Sample sample in samples)
					{
						bool pass = Relative(sample.Body.mass, sample.Mass, .0001f);
						Check(sample, pass);
						MagnificationDownscale.Log("MASS_NATIVE_PROBE_AFTER_STEP phase=" + phase + " sample=" + sample.Id
							+ " requestedMass=" + F(sample.Mass) + " actualMass=" + F(sample.Body.mass)
							+ " actualInertia=" + sample.Body.inertiaTensor.ToString("R") + " massPass=" + pass);
					}
				if (appliedSteps == ForceSteps)
				{
					ReportPhase();
					if (phase == 1) { Finish(null); return; }
					phase++;
					appliedSteps = 0;
					foreach (Sample sample in samples) PrepareRewrite(sample);
					// Let the reconfigured actor settle through one idle physics step.
					// No force is issued here, so the next 100-force count stays exact.
					return;
				}
				foreach (Sample sample in samples)
					sample.Body.AddForce(Vector3.right * (sample.Mass * 2f), ForceMode.Force);
				appliedSteps++;
			}
			catch (Exception error) { Finish(error); }
		}

		private void PrepareRewrite(Sample sample)
		{
			sample.PhasePass = true;
			sample.Body.velocity = sample.Body.angularVelocity = Vector3.zero;
			sample.Body.position = sample.Origin;
			sample.Box.size = new Vector3(2f, 1f, 1f);
			// Do not reapply the manual tensor or call ResetInertiaTensor here:
			// either would hide a bridge that damaged the original inertia mode.
			sample.Body.mass = .002f;
			Vector3 expected = sample.Manual ? ManualTensor : BoxTensor(.002f, sample.Box.size);
			Vector3 actual = sample.Body.inertiaTensor;
			bool modePass = TensorMatches(actual, expected);
			Check(sample, modePass);
			MagnificationDownscale.Log("MASS_NATIVE_PROBE_LARGE_REWRITE phase=" + phase + " sample=" + sample.Id
				+ " manualInertia=" + sample.Manual + " box=" + sample.Box.size.ToString("R")
				+ " actualMass=" + F(sample.Body.mass) + " expectedInertia=" + expected.ToString("R") + " actualInertia=" + actual.ToString("R")
				+ " inertiaModePreserved=" + modePass);
			ApplyRequestedMass(sample, "geometry-large-to-small");
		}

		private void ReportPhase()
		{
			// Exactly 100 forces have already been integrated. With semi-implicit
			// Euler, v=a*N*h=2 and x=a*h*h*N*(N+1)/2=1.01 at this observation.
			float expectedVelocity = 2f;
			float expectedPosition = 1.01f;
			foreach (Sample sample in samples)
			{
				float velocity = sample.Body.velocity.x;
				float position = sample.Body.position.x - sample.Origin.x;
				float measuredAcceleration = velocity / (ForceSteps * FixedStep);
				bool velocityPass = Math.Abs(velocity - expectedVelocity) <= .001f;
				bool positionPass = Math.Abs(position - expectedPosition) <= .001f;
				bool massPass = Relative(sample.Body.mass, sample.Mass, .0001f);
				Vector3 tensor = sample.Body.inertiaTensor;
				Vector3 expectedTensor = sample.Manual ? ManualTensor : BoxTensor(sample.Mass, sample.Box.size);
				bool inertiaPass = TensorMatches(tensor, expectedTensor);
				bool angularPass = sample.Body.angularVelocity.sqrMagnitude <= 1e-12f;
				Check(sample, velocityPass && positionPass && massPass && inertiaPass && angularPass);
				completed++;
				MagnificationDownscale.Log("MASS_NATIVE_PROBE_RESULT phase=" + phase + " sample=" + sample.Id + " manualInertia=" + sample.Manual
					+ " requestedMass=" + F(sample.Mass) + " actualMass=" + F(sample.Body.mass) + " expectedInertia=" + expectedTensor.ToString("R") + " actualInertia=" + tensor.ToString("R")
					+ " appliedSteps=" + appliedSteps + " expectedAcceleration=2 measuredAcceleration=" + F(measuredAcceleration)
					+ " expectedVelocity=" + F(expectedVelocity) + " actualVelocity=" + F(velocity)
					+ " expectedDisplacement=" + F(expectedPosition) + " actualDisplacement=" + F(position)
					+ " angularVelocity=" + sample.Body.angularVelocity.ToString("R")
					+ " velocityPass=" + velocityPass + " positionPass=" + positionPass + " massPass=" + massPass + " inertiaPass=" + inertiaPass + " angularPass=" + angularPass + " pass=" + sample.PhasePass);
			}
		}

		private void Check(Sample sample, bool pass)
		{
			if (pass) return;
			sample.PhasePass = false;
			failures++;
		}
		private static Vector3 BoxTensor(float mass, Vector3 size)
		{
			float value = mass / 12f;
			return new Vector3(value * (size.y * size.y + size.z * size.z), value * (size.x * size.x + size.z * size.z), value * (size.x * size.x + size.y * size.y));
		}
		private static bool Relative(float actual, float expected, float tolerance)
		{
			return !float.IsNaN(actual) && !float.IsInfinity(actual) && expected > 0f && Math.Abs(actual / expected - 1f) <= tolerance;
		}
		private static bool TensorMatches(Vector3 actual, Vector3 expected)
		{
			return Relative(actual.x, expected.x, .001f) && Relative(actual.y, expected.y, .001f) && Relative(actual.z, expected.z, .001f);
		}
		private void Finish(Exception error)
		{
			if (done) return;
			done = true;
			if (originalStep > 0f) Time.fixedDeltaTime = originalStep;
			foreach (Sample sample in samples) if (sample.Object != null) Destroy(sample.Object);
			if (error != null) MagnificationDownscale.Log("MASS_NATIVE_PROBE_FAILED phase=" + phase + " appliedSteps=" + appliedSteps + " " + error);
			MagnificationDownscale.Log("MASS_NATIVE_PROBE_COMPLETE success=" + (error == null && failures == 0 && completed == 20)
				+ " completed=" + completed + " expectedCases=20 failedChecks=" + failures + " timestepRestored=" + F(Time.fixedDeltaTime)
				+ " rawUnity=True gameplayAcceptance=False");
			Application.Quit();
		}
		private static string F(float value) { return value.ToString("R", CultureInfo.InvariantCulture); }
	}
}
