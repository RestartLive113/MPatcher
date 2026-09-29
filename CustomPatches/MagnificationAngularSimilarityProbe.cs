using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
	// Raw angular similarity experiment. No craft, scripts, gravity or contacts.
	internal sealed class MagnificationAngularSimilarityProbe : MonoBehaviour
	{
		private sealed class Sample
		{
			internal string Kind;
			internal float Scale, S3, S5, Torque, SourceAnchor;
			internal Rigidbody Main, Relay;
			internal HingeJoint Joint;
			internal float RotationMax, OmegaMax, AngleMax, PositionMax, VelocityMax, RelayPositionMax, ExpectedOmegaErrorMax;
			internal double RotationSquared, OmegaSquared, AngleSquared, PositionSquared;
			internal int Measurements;
		}
		private readonly List<Sample> samples = new List<Sample>();
		private readonly Dictionary<string, Sample> baselines = new Dictionary<string, Sample>();
		private const float Step = .01f;
		private const int TargetStep = 50, TorqueSteps = 100, FinalStep = 800;
		private const float SourceMass = 1.3f, RelayMass = .0005f, Anchor = .1f;
		private const float Spring = 2f, Damper = .2f, TargetAngle = 30f, TargetSpeed = 90f;
		private static readonly Vector3 SourceInertia = new Vector3(.02f, .03f, .04f);
		private static readonly Vector3 RelayInertia = new Vector3(.00002f, .00003f, .00004f);
		private float oldStep;
		private int step;
		private bool done;

		private void Start()
		{
			try
			{
				oldStep = Time.fixedDeltaTime; Time.fixedDeltaTime = Step;
				Application.runInBackground = true;
				foreach (string kind in new[] { "free-torque", "world-spring", "world-motor", "relay-spring", "world-stiff", "relay-stiff" })
					foreach (float scale in new[] { 1f, .05f, 2f }) Create(kind, scale);
				MagnificationDownscale.Log("ANGULAR_SIMILARITY_START cases=" + samples.Count + " dt=" + F(Step)
					+ " finalStep=" + FinalStep + " targetStep=" + TargetStep + " torqueSteps=" + TorqueSteps
					+ " targetDegrees=" + F(TargetAngle) + " motorTargetDegreesPerSecond=" + F(TargetSpeed)
					+ " sourceMass=" + F(SourceMass) + " sourceInertia=" + V(SourceInertia)
					+ " relayMass=" + F(RelayMass) + " relayInertia=" + V(RelayInertia)
					+ " sourceAnchor=" + F(Anchor) + " sourceSpring=" + F(Spring) + " sourceDamper=" + F(Damper)
					+ " stiffSourceInertia=0.02,0.03,0.0522 stiffAnchor=0 stiffSpring=300 stiffDamper=30"
					+ " mass=s3 inertia=s5 torque=s5 angularSpring=s5 angularDamper=s5 angularTargets=unchanged"
					+ " constraints=None gravity=False collisions=False drag=0 sleepThreshold=0 maxAngularVelocity=100 solver=30/20"
					+ " rawUnity=True comparisons=absolute-max-rms-no-acceptance-threshold");
			}
			catch (Exception error) { Finish(error); }
		}

		private void Create(string kind, float scale)
		{
			bool stiff = kind.IndexOf("stiff", StringComparison.Ordinal) >= 0;
			Sample sample = new Sample { Kind = kind, Scale = scale, S3 = scale * scale * scale, S5 = scale * scale * scale * scale * scale, SourceAnchor = stiff ? 0f : Anchor };
			sample.Main = Body(sample, "main", SourceMass, stiff ? new Vector3(.02f, .03f, .0522f) : SourceInertia, Vector3.zero);
			if (kind == "free-torque") sample.Torque = SourceInertia.z * 2f * sample.S5;
			else
			{
				Vector3 offset = Vector3.right * (sample.SourceAnchor * scale);
				if (kind.IndexOf("relay", StringComparison.Ordinal) == 0)
				{
					sample.Relay = Body(sample, "relay", RelayMass, RelayInertia, offset);
					FixedJoint link = sample.Relay.gameObject.AddComponent<FixedJoint>();
					link.autoConfigureConnectedAnchor = false;
					link.anchor = Vector3.zero; link.connectedAnchor = offset;
				}
				sample.Joint = sample.Main.gameObject.AddComponent<HingeJoint>();
				sample.Joint.autoConfigureConnectedAnchor = false;
				sample.Joint.connectedBody = sample.Relay;
				sample.Joint.anchor = offset;
				sample.Joint.connectedAnchor = sample.Relay == null ? offset : Vector3.zero;
				sample.Joint.axis = Vector3.forward;
				sample.Joint.useLimits = false;
				if (kind == "world-motor")
				{
					float sourceEffectiveInertia = SourceInertia.z + SourceMass * Anchor * Anchor;
					sample.Torque = 2f * sourceEffectiveInertia * sample.S5;
					sample.Joint.motor = new JointMotor { force = sample.Torque, targetVelocity = 0f, freeSpin = false };
					sample.Joint.useMotor = true;
				}
				else
				{
					sample.Joint.spring = new JointSpring { spring = (stiff ? 300f : Spring) * sample.S5, damper = (stiff ? 30f : Damper) * sample.S5, targetPosition = 0f };
					sample.Joint.useSpring = true;
				}
			}
			samples.Add(sample);
			if (scale == 1f) baselines.Add(kind, sample);
			Snapshot(sample, "created");
		}

		private static Rigidbody Body(Sample sample, string role, float sourceMass, Vector3 sourceInertia, Vector3 position)
		{
			GameObject obj = new GameObject("MPatcher_Angular_" + sample.Kind + "_" + F(sample.Scale) + "_" + role);
			obj.transform.position = position;
			Rigidbody body = obj.AddComponent<Rigidbody>();
			body.useGravity = body.detectCollisions = false;
			body.drag = body.angularDrag = body.sleepThreshold = 0f;
			body.constraints = RigidbodyConstraints.None;
			body.maxAngularVelocity = 100f;
			body.solverIterations = 30; body.solverVelocityIterations = 20;
			float mass = sourceMass * sample.S3;
			body.mass = mass;
			if (mass < 1e-7f) MagnificationJointMassNative.WriteSmallMass(body, mass);
			body.centerOfMass = Vector3.zero;
			body.inertiaTensorRotation = Quaternion.identity;
			Vector3 inertia = sourceInertia * sample.S5;
			if (Mathf.Min(inertia.x, Mathf.Min(inertia.y, inertia.z)) <= 1.1920928955078125e-7f)
				MagnificationJointMassNative.WriteSmallInertia(body, inertia);
			else body.inertiaTensor = inertia;
			MagnificationDownscale.Log("ANGULAR_SIMILARITY_BODY kind=" + sample.Kind + " scale=" + F(sample.Scale) + " role=" + role
				+ " massRequested=" + F(mass) + " massActual=" + F(body.mass)
				+ " inertiaRequested=" + V(inertia) + " inertiaActual=" + V(body.inertiaTensor)
				+ " position=" + V(body.position) + " constraints=" + body.constraints
				+ " useGravity=" + body.useGravity + " detectCollisions=" + body.detectCollisions);
			return body;
		}

		private void FixedUpdate()
		{
			if (done || samples.Count == 0) return;
			try
			{
				foreach (Sample sample in samples)
				{
					if (step > 0) Measure(sample);
					if (step % 20 == 0 || step == TargetStep || step == TargetStep + 1 || step == TorqueSteps) Snapshot(sample, "trajectory");
				}
				if (step == FinalStep)
				{
					foreach (Sample sample in samples) Result(sample);
					MagnificationDownscale.Log("ANGULAR_SIMILARITY_COMPLETE completed=True cases=" + samples.Count
						+ " measurementsPerCase=" + step + " acceptance=not-assigned compare=100-percent-same-case");
					Finish(null); return;
				}
				foreach (Sample sample in samples)
				{
					if (sample.Kind == "free-torque")
					{
						if (step < TorqueSteps) sample.Main.AddTorque(Vector3.forward * sample.Torque, ForceMode.Force);
					}
					else if (step == TargetStep)
					{
						if (sample.Kind == "world-motor")
						{
							JointMotor motor = sample.Joint.motor; motor.targetVelocity = TargetSpeed; sample.Joint.motor = motor;
						}
						else
						{
							JointSpring spring = sample.Joint.spring; spring.targetPosition = TargetAngle; sample.Joint.spring = spring;
						}
					}
				}
				step++;
			}
			catch (Exception error) { Finish(error); }
		}

		private void Measure(Sample sample)
		{
			Sample baseline = baselines[sample.Kind];
			float rotation = RotationDifference(baseline.Main.rotation, sample.Main.rotation);
			float omega = (baseline.Main.angularVelocity - sample.Main.angularVelocity).magnitude;
			float angle = sample.Joint == null ? 0f : Mathf.Abs(Mathf.DeltaAngle(baseline.Joint.angle, sample.Joint.angle));
			float position = (baseline.Main.position - sample.Main.position / sample.Scale).magnitude;
			float velocity = (baseline.Main.velocity - sample.Main.velocity / sample.Scale).magnitude;
			sample.RotationMax = Mathf.Max(sample.RotationMax, rotation);
			sample.OmegaMax = Mathf.Max(sample.OmegaMax, omega);
			sample.AngleMax = Mathf.Max(sample.AngleMax, angle);
			sample.PositionMax = Mathf.Max(sample.PositionMax, position);
			sample.VelocityMax = Mathf.Max(sample.VelocityMax, velocity);
			sample.RotationSquared += (double)rotation * rotation;
			sample.OmegaSquared += (double)omega * omega;
			sample.AngleSquared += (double)angle * angle;
			sample.PositionSquared += (double)position * position;
			if (sample.Relay != null) sample.RelayPositionMax = Mathf.Max(sample.RelayPositionMax,
				(sample.Relay.position / sample.Scale - Vector3.right * sample.SourceAnchor).magnitude);
			if (sample.Kind == "free-torque") sample.ExpectedOmegaErrorMax = Mathf.Max(sample.ExpectedOmegaErrorMax,
				Mathf.Abs(sample.Main.angularVelocity.z - 2f * Math.Min(step, TorqueSteps) * Step));
			sample.Measurements++;
		}

		private void Snapshot(Sample sample, string phase)
		{
			HingeJoint joint = sample.Joint;
			string drive = joint == null ? "joint=none torque=" + F(sample.Torque)
				: "angleDegrees=" + F(joint.angle) + " jointVelocity=" + F(joint.velocity)
				+ " spring=" + F(joint.spring.spring) + " damper=" + F(joint.spring.damper)
				+ " targetDegrees=" + F(joint.spring.targetPosition) + " motorForce=" + F(joint.motor.force)
				+ " motorTargetDegreesPerSecond=" + F(joint.motor.targetVelocity)
				+ " anchor=" + V(joint.anchor) + " connectedAnchor=" + V(joint.connectedAnchor);
			MagnificationDownscale.Log("ANGULAR_SIMILARITY_SAMPLE phase=" + phase + " step=" + step + " time=" + F(step * Step)
				+ " kind=" + sample.Kind + " scale=" + F(sample.Scale) + " " + drive
				+ " quaternion=" + Q(sample.Main.rotation) + " omega=" + V(sample.Main.angularVelocity)
				+ " positionCraft=" + V(sample.Main.position / sample.Scale) + " velocityCraft=" + V(sample.Main.velocity / sample.Scale)
				+ " mainMass=" + F(sample.Main.mass) + " mainInertia=" + V(sample.Main.inertiaTensor)
				+ " relayPositionCraft=" + (sample.Relay == null ? "none" : V(sample.Relay.position / sample.Scale))
				+ " relayOmega=" + (sample.Relay == null ? "none" : V(sample.Relay.angularVelocity)));
		}

		private void Result(Sample sample)
		{
			float targetError = sample.Joint == null || sample.Kind == "world-motor" ? 0f : Mathf.Abs(Mathf.DeltaAngle(TargetAngle, sample.Joint.angle));
			MagnificationDownscale.Log("ANGULAR_SIMILARITY_RESULT kind=" + sample.Kind + " scale=" + F(sample.Scale)
				+ " measurements=" + sample.Measurements + " rotationDifferenceMaxDegrees=" + F(sample.RotationMax)
				+ " rotationDifferenceRmsDegrees=" + Rms(sample.RotationSquared, sample.Measurements)
				+ " omegaDifferenceMaxRad=" + F(sample.OmegaMax) + " omegaDifferenceRmsRad=" + Rms(sample.OmegaSquared, sample.Measurements)
				+ " jointAngleDifferenceMaxDegrees=" + F(sample.AngleMax) + " jointAngleDifferenceRmsDegrees=" + Rms(sample.AngleSquared, sample.Measurements)
				+ " positionDifferenceMaxCraft=" + F(sample.PositionMax) + " positionDifferenceRmsCraft=" + Rms(sample.PositionSquared, sample.Measurements)
				+ " velocityDifferenceMaxCraft=" + F(sample.VelocityMax) + " relayPositionErrorMaxCraft=" + F(sample.RelayPositionMax)
				+ " freeTorqueExpectedOmegaErrorMax=" + F(sample.ExpectedOmegaErrorMax)
				+ " finalSpringTargetErrorDegrees=" + F(targetError) + " finalOmegaRad=" + V(sample.Main.angularVelocity)
				+ " finalMass=" + F(sample.Main.mass) + " finalInertia=" + V(sample.Main.inertiaTensor));
		}

		private void Finish(Exception error)
		{
			done = true;
			if (oldStep > 0f) Time.fixedDeltaTime = oldStep;
			if (error != null) MagnificationDownscale.Log("ANGULAR_SIMILARITY_FAILED " + error);
			Application.Quit();
		}
		private static string Rms(double sum, int n) { return Math.Sqrt(sum / Math.Max(1, n)).ToString("R", CultureInfo.InvariantCulture); }
		// Double normalized chord keeps tiny rotation residuals visible; Unity's
		// float acos(dot) can round small differences down to zero.
		private static float RotationDifference(Quaternion a, Quaternion b)
		{
			double an = Math.Sqrt((double)a.x * a.x + (double)a.y * a.y + (double)a.z * a.z + (double)a.w * a.w);
			double bn = Math.Sqrt((double)b.x * b.x + (double)b.y * b.y + (double)b.z * b.z + (double)b.w * b.w);
			double sign = (double)a.x * b.x + (double)a.y * b.y + (double)a.z * b.z + (double)a.w * b.w < 0 ? -1 : 1;
			double x = a.x / an - sign * b.x / bn, y = a.y / an - sign * b.y / bn;
			double z = a.z / an - sign * b.z / bn, w = a.w / an - sign * b.w / bn;
			return (float)(4 * Math.Asin(Math.Min(1, Math.Sqrt(x * x + y * y + z * z + w * w) * .5)) * 180 / Math.PI);
		}
		private static string F(float value) { return value.ToString("R", CultureInfo.InvariantCulture); }
		private static string V(Vector3 value) { return F(value.x) + "," + F(value.y) + "," + F(value.z); }
		private static string Q(Quaternion value) { return F(value.x) + "," + F(value.y) + "," + F(value.z) + "," + F(value.w); }
	}
}
