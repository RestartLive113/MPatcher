using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
	// Compare only floor horizontal extent: top, thickness and material agree.
	// No global Physics setting is written. Pair ignores isolate coincident rigs.
	internal sealed class MagnificationContactSimilarityProbe : MonoBehaviour
	{
		internal sealed class Sample
		{
			internal string Shape, FloorKind;
			internal float Scale, SourceMass, S4;
			internal Rigidbody Body;
			internal Collider Collider, Floor;
			internal int Id, WindowCallbacks, WindowPoints, TotalCallbacks, RestCallbacks, ForeignCallbacks, RestCount;
			internal float WindowImpulseY, WindowSeparationMin = float.PositiveInfinity, LifetimeSeparationMin = float.PositiveInfinity;
			internal float RestMin = float.PositiveInfinity, RestMax = float.NegativeInfinity, RestSpeedMax, RestAngularMax, RestImpulseY;
			internal double RestHeightSum, RestHeightSquared, RestVelocitySquared;
		}
		private const float Step = .01f, GroundY = 24f;
		private const int RestStart = 200, FinalStep = 600;
		private readonly List<Sample> samples = new List<Sample>();
		private float oldStep;
		private Vector3 gravity;
		private int step, lastSampleStep;
		private bool done;
		private PhysicMaterial material;
		private int bodyLayer, floorLayer;
		private readonly HashSet<int> ignoredExternalColliders = new HashSet<int>();
		internal int CurrentStep { get { return step; } }

		private void Start()
		{
			try
			{
				oldStep = Time.fixedDeltaTime; Time.fixedDeltaTime = Step;
				Application.runInBackground = true; gravity = Physics.gravity;
				if (!(gravity.y < 0f) || gravity.x != 0f || gravity.z != 0f) throw new InvalidOperationException("vertical-down-gravity-required " + V(gravity));
				SelectCollisionLayers();
				material = new PhysicMaterial("MPatcher_RawContactMaterial");
				material.bounciness = 0f; material.dynamicFriction = material.staticFriction = .6f;
				material.frictionCombine = material.bounceCombine = PhysicMaterialCombine.Average;
				foreach (string shape in new[] { "sphere", "box" })
					foreach (float mass in new[] { 1.3f, .01f, .0005f })
						foreach (string floor in new[] { "huge-world", "compact-scaled" })
							foreach (float scale in new[] { 1f, .05f, 2f }) Create(shape, mass, floor, scale);
				// Every dynamic collider sees exactly its own floor and no other rig.
				foreach (Sample a in samples) foreach (Sample b in samples)
				{
					if (a == b) continue;
					Physics.IgnoreCollision(a.Collider, b.Floor, true);
					if (a.Id < b.Id) Physics.IgnoreCollision(a.Collider, b.Collider, true);
				}
				IgnoreExternalColliders();
				MagnificationDownscale.Log("CONTACT_SIMILARITY_START cases=" + samples.Count + " dt=" + F(Step)
					+ " bodyLayer=" + bodyLayer + " floorLayer=" + floorLayer + " layerPairIgnored=" + Physics.GetIgnoreLayerCollision(bodyLayer, floorLayer)
					+ " groundY=" + F(GroundY) + " finalStep=" + FinalStep + " restStart=" + RestStart
					+ " globalGravity=" + V(gravity) + " globalGravityWritten=False bodyGravity=scale*native"
					+ " floorThicknessWorld=1 floorContactWorld=0.001 hugeHorizontalWorld=100000 compactHorizontal=100*scale"
					+ " bodyContact=0.001*scale initialGap=0.2*scale mass=s3 inertia=s5 sleep=0 constraints=None"
					+ " solver=6/1 friction=0.6 bounce=0 comparison=primary-values-no-acceptance-threshold");
			}
			catch (Exception error) { Finish(error); }
		}

		private void SelectCollisionLayers()
		{
			// Read the existing matrix; never change a global layer rule.
			for (int floor = 0; floor < 32; floor++)
				for (int body = 0; body < 32; body++)
					if (!Physics.GetIgnoreLayerCollision(body, floor))
					{ bodyLayer = body; floorLayer = floor; return; }
			throw new InvalidOperationException("no-enabled-collision-layer-pair");
		}

		private void IgnoreExternalColliders()
		{
			HashSet<int> own = new HashSet<int>();
			foreach (Sample s in samples) { own.Add(s.Collider.GetInstanceID()); own.Add(s.Floor.GetInstanceID()); }
			int added = 0;
			foreach (Collider other in UnityEngine.Object.FindObjectsOfType<Collider>())
			{
				if (other == null || own.Contains(other.GetInstanceID()) || !ignoredExternalColliders.Add(other.GetInstanceID())) continue;
				foreach (Sample s in samples) Physics.IgnoreCollision(s.Collider, other, true);
				added++;
			}
			MagnificationDownscale.Log("CONTACT_SIMILARITY_ISOLATION step=" + step + " externalCollidersAdded=" + added
				+ " externalCollidersTotal=" + ignoredExternalColliders.Count + " globalLayerMatrixWritten=False");
		}

		private void Create(string shape, float mass, string floor, float scale)
		{
			Sample s = new Sample { Id = samples.Count, Shape = shape, FloorKind = floor, SourceMass = mass, Scale = scale, S4 = scale * scale * scale * scale };
			GameObject floorObject = new GameObject("MPatcher_ContactFloor_" + s.Id);
			floorObject.layer = floorLayer;
			floorObject.transform.position = new Vector3(0, GroundY - .5f, 0);
			BoxCollider floorCollider = floorObject.AddComponent<BoxCollider>();
			float extent = floor == "huge-world" ? 100000f : 100f * scale;
			floorCollider.size = new Vector3(extent, 1f, extent);
			floorCollider.contactOffset = .001f; floorCollider.sharedMaterial = material; s.Floor = floorCollider;
			GameObject obj = new GameObject("MPatcher_ContactBody_" + s.Id);
			obj.layer = bodyLayer;
			obj.transform.position = new Vector3(0, GroundY + 1.2f * scale, 0);
			if (shape == "sphere") { SphereCollider c = obj.AddComponent<SphereCollider>(); c.radius = scale; s.Collider = c; }
			else { BoxCollider c = obj.AddComponent<BoxCollider>(); c.size = Vector3.one * (2f * scale); s.Collider = c; }
			s.Collider.contactOffset = .001f * scale; s.Collider.sharedMaterial = material;
			s.Body = obj.AddComponent<Rigidbody>();
			s.Body.useGravity = true; s.Body.drag = s.Body.angularDrag = s.Body.sleepThreshold = 0f;
			s.Body.constraints = RigidbodyConstraints.None;
			s.Body.solverIterations = 6; s.Body.solverVelocityIterations = 1;
			s.Body.maxAngularVelocity = 100f; s.Body.maxDepenetrationVelocity *= scale;
			float physicalMass = mass * scale * scale * scale;
			s.Body.mass = physicalMass;
			if (physicalMass < 1e-7f) MagnificationJointMassNative.WriteSmallMass(s.Body, physicalMass);
			s.Body.centerOfMass = Vector3.zero; s.Body.inertiaTensorRotation = Quaternion.identity;
			float sourceInertia = mass * (shape == "sphere" ? .4f : 2f / 3f);
			Vector3 inertia = Vector3.one * (sourceInertia * scale * scale * scale * scale * scale);
			if (inertia.x <= 1.1920928955078125e-7f) MagnificationJointMassNative.WriteSmallInertia(s.Body, inertia);
			else s.Body.inertiaTensor = inertia;
			MagnificationRawContactObserver observer = obj.AddComponent<MagnificationRawContactObserver>();
			observer.Owner = this; observer.Sample = s; samples.Add(s);
			MagnificationDownscale.Log("CONTACT_SIMILARITY_CREATED " + Label(s) + " mass=" + F(s.Body.mass)
				+ " inertia=" + V(s.Body.inertiaTensor) + " position=" + V(s.Body.position)
				+ " colliderBoundsMin=" + V(s.Collider.bounds.min) + " colliderBoundsMax=" + V(s.Collider.bounds.max)
				+ " floorBoundsMin=" + V(s.Floor.bounds.min) + " floorBoundsMax=" + V(s.Floor.bounds.max)
				+ " bodyContact=" + F(s.Collider.contactOffset) + " floorContact=" + F(s.Floor.contactOffset)
				+ " depenetration=" + F(s.Body.maxDepenetrationVelocity));
		}

		internal void Observe(Sample s, Collision collision)
		{
			if (collision.collider != s.Floor)
			{
				s.ForeignCallbacks++;
				if (s.ForeignCallbacks <= 3) MagnificationDownscale.Log("CONTACT_SIMILARITY_FOREIGN " + Label(s)
					+ " step=" + step + " collider=" + (collision.collider == null ? "null" : collision.collider.name)
					+ " layer=" + (collision.collider == null ? -1 : collision.collider.gameObject.layer));
				return;
			}
			s.WindowCallbacks++; s.TotalCallbacks++;
			s.WindowImpulseY += Mathf.Abs(collision.impulse.y);
			if (step > RestStart) { s.RestCallbacks++; s.RestImpulseY += Mathf.Abs(collision.impulse.y); }
			ContactPoint[] contacts = collision.contacts; s.WindowPoints += contacts.Length;
			foreach (ContactPoint point in contacts)
			{
				s.WindowSeparationMin = Mathf.Min(s.WindowSeparationMin, point.separation);
				s.LifetimeSeparationMin = Mathf.Min(s.LifetimeSeparationMin, point.separation);
			}
		}

		private void FixedUpdate()
		{
			if (done || samples.Count == 0) return;
			try
			{
				if (step == 0) IgnoreExternalColliders();
				foreach (Sample s in samples)
				{
					float height = (s.Body.position.y - GroundY) / s.Scale;
					if (float.IsNaN(height) || float.IsInfinity(height)) throw new InvalidOperationException("non-finite-body " + Label(s));
					if (step > RestStart)
					{
						s.RestCount++; s.RestHeightSum += height; s.RestHeightSquared += (double)height * height;
						float velocity = s.Body.velocity.y / s.Scale;
						s.RestVelocitySquared += (double)velocity * velocity;
						s.RestMin = Mathf.Min(s.RestMin, height); s.RestMax = Mathf.Max(s.RestMax, height);
						s.RestSpeedMax = Mathf.Max(s.RestSpeedMax, s.Body.velocity.magnitude / s.Scale);
						s.RestAngularMax = Mathf.Max(s.RestAngularMax, s.Body.angularVelocity.magnitude);
					}
					if (step % 20 == 0) Snapshot(s, height);
				}
				if (step % 20 == 0) lastSampleStep = step;
				if (step == FinalStep)
				{
					int invalid = 0;
					foreach (Sample s in samples)
					{
						Result(s);
						if (s.TotalCallbacks == 0 || s.RestCallbacks == 0 || s.ForeignCallbacks != 0) invalid++;
					}
					if (Physics.GetIgnoreLayerCollision(bodyLayer, floorLayer)) throw new InvalidOperationException("chosen-layer-pair-became-ignored");
					if (invalid != 0) throw new InvalidOperationException("invalid-contact-fixture cases=" + invalid + " own-floor-and-rest-contact-required foreign-contact-forbidden");
					MagnificationDownscale.Log("CONTACT_SIMILARITY_COMPLETE completed=True cases=" + samples.Count + " acceptance=not-assigned");
					Finish(null); return;
				}
				foreach (Sample s in samples)
					if (s.Scale != 1f && !s.Body.IsSleeping()) MagnificationJointMassNative.AddAccelerationWithoutWake(s.Body, gravity * (s.Scale - 1f));
				step++;
			}
			catch (Exception error) { Finish(error); }
		}

		private void Snapshot(Sample s, float height)
		{
			float duration = (step - lastSampleStep) * Step;
			MagnificationDownscale.Log("CONTACT_SIMILARITY_SAMPLE " + Label(s) + " step=" + step
				+ " heightCraft=" + F(height) + " bottomCraft=" + F((s.Collider.bounds.min.y - GroundY) / s.Scale)
				+ " position=" + V(s.Body.position) + " velocityCraft=" + V(s.Body.velocity / s.Scale) + " angular=" + V(s.Body.angularVelocity)
				+ " callbacks=" + s.WindowCallbacks + " points=" + s.WindowPoints
				+ " separationMinCraft=" + F(s.WindowSeparationMin / s.Scale)
				+ " forceYCraft=" + F(duration > 0 ? s.WindowImpulseY / duration / s.S4 : 0)
				+ " sleeping=" + s.Body.IsSleeping());
			s.WindowCallbacks = s.WindowPoints = 0; s.WindowImpulseY = 0; s.WindowSeparationMin = float.PositiveInfinity;
		}

		private void Result(Sample s)
		{
			double mean = s.RestHeightSum / s.RestCount;
			double variance = Math.Max(0, s.RestHeightSquared / s.RestCount - mean * mean);
			MagnificationDownscale.Log("CONTACT_SIMILARITY_RESULT " + Label(s) + " restCount=" + s.RestCount
				+ " heightCraftMean=" + D(mean) + " heightCraftStdDev=" + D(Math.Sqrt(variance))
				+ " heightCraftRange=" + F(s.RestMax - s.RestMin) + " verticalVelocityCraftRms=" + D(Math.Sqrt(s.RestVelocitySquared / s.RestCount))
				+ " velocityCraftMax=" + F(s.RestSpeedMax) + " angularMax=" + F(s.RestAngularMax)
				+ " separationMinCraft=" + F(s.LifetimeSeparationMin / s.Scale) + " totalCallbacks=" + s.TotalCallbacks + " restCallbacks=" + s.RestCallbacks + " foreignCallbacks=" + s.ForeignCallbacks
				+ " restForceYCraft=" + F(s.RestImpulseY / ((FinalStep - RestStart) * Step) / s.S4)
				+ " expectedForceYCraft=" + F(-gravity.y * s.SourceMass)
				+ " finalMass=" + F(s.Body.mass) + " finalInertia=" + V(s.Body.inertiaTensor));
		}
		private void Finish(Exception error)
		{
			done = true; if (oldStep > 0) Time.fixedDeltaTime = oldStep;
			if (error != null) MagnificationDownscale.Log("CONTACT_SIMILARITY_FAILED " + error);
			Application.Quit();
		}
		private static string Label(Sample s) { return "id=" + s.Id + " shape=" + s.Shape + " floor=" + s.FloorKind + " scale=" + F(s.Scale) + " sourceMass=" + F(s.SourceMass); }
		private static string F(float value) { return value.ToString("R", CultureInfo.InvariantCulture); }
		private static string D(double value) { return value.ToString("R", CultureInfo.InvariantCulture); }
		private static string V(Vector3 value) { return F(value.x) + "," + F(value.y) + "," + F(value.z); }
	}
	internal sealed class MagnificationRawContactObserver : MonoBehaviour
	{
		internal MagnificationContactSimilarityProbe Owner;
		internal MagnificationContactSimilarityProbe.Sample Sample;
		private void OnCollisionEnter(Collision collision) { if (Owner != null) Owner.Observe(Sample, collision); }
		private void OnCollisionStay(Collision collision) { if (Owner != null) Owner.Observe(Sample, collision); }
	}
}
