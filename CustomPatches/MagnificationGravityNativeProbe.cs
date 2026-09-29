using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
	// Isolated raw runtime probe; entered only through the research root/flag gate.
	// Uses the actual per-body gravity adapter and native integration. No user craft.
	[DefaultExecutionOrder(-32000)]
	internal sealed class MagnificationGravityNativeProbe : MonoBehaviour
	{
		internal static bool Running;
		private sealed class Sample
		{
			internal Rigidbody Body;
			internal float Scale, Compensation, Origin;
			internal int Id, FirstSleep = -1;
		}
		private readonly List<Sample> samples = new List<Sample>();
		private readonly List<GameObject> objects = new List<GameObject>();
		private Vector3 gravity;
		private float step;
		private int ticks, phase, failures;
		private bool done;
		private void Start()
		{
			try
			{
				Running = true;
				Application.runInBackground = true;
				step = Time.fixedDeltaTime; gravity = Physics.gravity;
				if (gravity.x != 0f || gravity.z != 0f || gravity.y >= 0f) throw new InvalidOperationException("unexpected-native-gravity");
				foreach (float scale in new[] { 1f, .05f, 2f })
				foreach (float compensation in new[] { 0f, .5f, 1f }) Create(scale, compensation, false);
				Log("START step=" + F(step) + " gravity=" + gravity.ToString("R") + " mode=22 freefallSamples=9 sleepSamples=3 timestepAndSolver=unchanged");
			}
			catch (Exception error) { Finish(error); }
		}
		private void Create(float scale, float compensation, bool rest)
		{
			Sample sample = new Sample { Id = samples.Count, Scale = scale, Compensation = compensation, Origin = rest ? .5f * scale : 100f * scale };
			GameObject obj = new GameObject("MPatcher_GravityProbe_" + phase + "_" + sample.Id); objects.Add(obj);
			obj.transform.position = new Vector3(sample.Id * 10f, sample.Origin, 0f);
			BoxCollider box = obj.AddComponent<BoxCollider>(); box.size = Vector3.one * scale; box.contactOffset *= scale;
			sample.Body = obj.AddComponent<Rigidbody>();
			sample.Body.mass = scale * scale * scale;
			sample.Body.useGravity = true; sample.Body.detectCollisions = rest;
			// MachineCraft disables sleep globally. This isolated rest experiment
			// needs an explicit positive source threshold on its own sample bodies.
			if (rest) sample.Body.sleepThreshold = .005f;
			MagnificationConstraintUnits.ApplyBody(sample.Body, scale);
			MagnificationGravitySimilarity.Track(sample.Body, scale);
			samples.Add(sample);
			Log("BODY phase=" + phase + " sample=" + sample.Id + " scale=" + F(scale) + " compensation=" + F(compensation) + " mass=" + F(sample.Body.mass)
				+ " sleep=" + F(sample.Body.sleepThreshold) + " contact=" + F(box.contactOffset) + " solver=" + sample.Body.solverIterations + "/" + sample.Body.solverVelocityIterations);
		}
		private void FixedUpdate()
		{
			if (done || !Running) return;
			try
			{
				if (Time.fixedDeltaTime != step || Physics.gravity != gravity) throw new InvalidOperationException("global-physics-changed");
				if (phase == 0)
				{
					if (ticks == 100)
					{
						foreach (Sample s in samples)
						{
							float expectedV = gravity.y * (1f - s.Compensation) * ticks * step;
							float expectedY = gravity.y * (1f - s.Compensation) * step * step * ticks * (ticks + 1) / 2f;
							float actualV = s.Body.velocity.y / s.Scale, actualY = (s.Body.position.y - s.Origin) / s.Scale;
							bool pass = Math.Abs(actualV - expectedV) < .003f && Math.Abs(actualY - expectedY) < .003f;
							if (!pass) failures++;
							Log("FALL sample=" + s.Id + " scale=" + F(s.Scale) + " compensation=" + F(s.Compensation) + " velocityCraft=" + F(actualV)
								+ " expectedVelocity=" + F(expectedV) + " displacementCraft=" + F(actualY) + " expectedDisplacement=" + F(expectedY) + " pass=" + pass);
						}
						MagnificationGravitySimilarity.Clear(); MagnificationConstraintUnits.Clear();
						foreach (Sample s in samples) { s.Body.isKinematic = true; Destroy(s.Body.gameObject); }
						samples.Clear(); phase = 1; ticks = 0;
						GameObject ground = new GameObject("MPatcher_GravityProbe_Ground"); objects.Add(ground);
						ground.transform.position = new Vector3(10f, -.5f, 0f); ground.AddComponent<BoxCollider>().size = new Vector3(100f, 1f, 100f);
						foreach (float scale in new[] { 1f, .05f, 2f }) Create(scale, 0f, true);
						return;
					}
					foreach (Sample s in samples) if (s.Compensation != 0f)
						s.Body.AddForce(-gravity * (s.Compensation * MagnificationSimilarityUnits.Force(s.Scale)), ForceMode.Force);
				}
				else
				{
					foreach (Sample s in samples)
						if (s.FirstSleep < 0 && s.Body.IsSleeping()) s.FirstSleep = ticks;
					if (ticks * step >= 15f)
					{
						foreach (Sample s in samples)
						{
							bool pass = s.FirstSleep >= 0 && s.Body.IsSleeping(); if (!pass) failures++;
							Log("SLEEP sample=" + s.Id + " scale=" + F(s.Scale) + " firstSleepSeconds=" + F(s.FirstSleep * step) + " sleeping=" + s.Body.IsSleeping()
								+ " velocityCraft=" + (s.Body.velocity / s.Scale).ToString("R") + " pass=" + pass);
						}
						Finish(null); return;
					}
				}
				ticks++;
			}
			catch (Exception error) { Finish(error); }
		}
		private void Finish(Exception error)
		{
			if (done) return; done = true;
			MagnificationGravitySimilarity.Clear(); MagnificationConstraintUnits.Clear(); Running = false;
			foreach (GameObject obj in objects) if (obj != null) Destroy(obj);
			if (error != null) Log("FAILED " + error);
			Log("COMPLETE success=" + (error == null && failures == 0 && phase == 1) + " failedChecks=" + failures + " rawUnity=True gameplayAcceptance=False");
			Application.Quit();
		}
		private static void Log(string message) { MagnificationDownscale.Log("GRAVITY_NATIVE_PROBE_" + message); }
		private static string F(float value) { return value.ToString("R", CultureInfo.InvariantCulture); }
	}
}
