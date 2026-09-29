using System;
using System.Collections.Generic;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
	// Native integration of the mass/force invariant. This is deliberately not
	// presented as an AGDeviceController, energy, water, or script acceptance test.
	internal sealed class MagnificationForceProbe : MonoBehaviour
	{
		private sealed class Sample { internal Rigidbody Body; internal float Scale, Fraction, Force; }
		private readonly List<Sample> samples = new List<Sample>();
		private int steps;
		private float simulatedTime;
		private bool done;
		private void Start()
		{
			try
			{
				foreach (float scale in new[] { .01f, .05f, .1f, .5f, 1f, 2f, 3f })
				foreach (float fraction in new[] { 0f, .5f, 1f })
				{
					GameObject obj = new GameObject("MPatcher_MassForceProbe");
					obj.transform.position = new Vector3(samples.Count * 10f, 5000f, 0f);
					Rigidbody body = obj.AddComponent<Rigidbody>();
					body.useGravity = false; body.detectCollisions = false; body.drag = body.angularDrag = 0f;
					body.mass = .6f * MagnificationScalePolicy.Mass(scale);
					float tensor = .2f * MagnificationScalePolicy.Inertia(scale);
					if (tensor <= 1.1920928955078125e-7f) MagnificationJointMassNative.WriteSmallInertia(body, Vector3.one * tensor);
					else body.inertiaTensor = Vector3.one * tensor;
					float force = (48f * fraction - 48f) * MagnificationForceUnits.LinearFactor(scale, ForceMode.Force);
					samples.Add(new Sample { Body = body, Scale = scale, Fraction = fraction, Force = force });
				}
				MagnificationDownscale.Log("MASS_FORCE_PROBE_START samples=" + samples.Count + " sourceMass=600 sourceUnityMass=0.6 gravity=80 controller=synthetic");
			}
			catch (Exception ex) { Fail(ex); }
		}
		private void FixedUpdate()
		{
			if (done || samples.Count == 0) return;
			try
			{
				if (steps == 20)
				{
					foreach (Sample sample in samples)
					{
						float expected = -80f * (1f - sample.Fraction) * simulatedTime;
						float actual = sample.Body.velocity.y;
						bool pass = Math.Abs(actual - expected) < .0005f;
						MagnificationDownscale.Log("MASS_FORCE_PROBE_RESULT scale=" + sample.Scale + " command=" + (600f * sample.Fraction)
							+ " physicalMass=" + sample.Body.mass + " expectedVelocity=" + expected + " actualVelocity=" + actual + " pass=" + pass);
						if (!pass) throw new InvalidOperationException("mass-force-integration-mismatch");
					}
					done = true;
					MagnificationDownscale.Log("MASS_FORCE_PROBE_COMPLETE success=True controllerAcceptance=False");
					Application.Quit();
					return;
				}
				foreach (Sample sample in samples) sample.Body.AddForce(Vector3.up * sample.Force, ForceMode.Force);
				simulatedTime += Time.fixedDeltaTime; steps++;
			}
			catch (Exception ex) { Fail(ex); }
		}
		private void Fail(Exception ex) { done = true; MagnificationDownscale.Log("MASS_FORCE_PROBE_FAILED " + ex); Application.Quit(); }
	}
}
