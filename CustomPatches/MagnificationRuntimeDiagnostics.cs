using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using McnCraft;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
	// Observe all detached physical body roots without changing their state.
	internal static class MagnificationRuntimeDiagnostics
	{
		private static int nextId;

		internal static void Attach(MachineController machine)
		{
			if (machine == null) return;
			float scale = MagnificationPhysicsNormalization.MachineScale(machine);
			if (machine.GetComponent<MagnificationDiagnosticsRunner>() != null) return;
			MagnificationDiagnosticsRunner runner = machine.gameObject.AddComponent<MagnificationDiagnosticsRunner>();
			runner.Initialize(machine, ++nextId, scale);
		}

		internal static void ObserveRespawn(MachineController machine)
		{
			if (machine == null) return;
			Attach(machine);
			MagnificationDownscale.Log("RESPAWN_ENDPOINTS_READY machine=" + machine.GetInstanceID() + " source=Restore2-SetJoint");
			machine.GetComponent<MagnificationDiagnosticsRunner>().Snapshot("respawn");
		}
	}

	internal sealed class MagnificationDiagnosticsRunner : MonoBehaviour
	{
		private static readonly FieldInfo PistonJointField = typeof(PistonController).GetField(
			"PDNNFOJPIDM", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
		private MachineController machine;
		private int id;
		private int fixedFrames;
		private float scale;
		private float startedAt;

		internal void Initialize(MachineController value, int diagnosticId, float machineScale)
		{
			machine = value;
			id = diagnosticId;
			scale = machineScale;
			startedAt = Time.realtimeSinceStartup;
			MagnificationDownscale.Log("DIAGNOSTICS_ATTACHED revision=3 id=" + id + " scale=" + F(scale)
				+ " samples=fixed-1,2,5,25,125,time-10s roots=KBLANAFAJFP baseline100=included mutation=none"
				+ " script=" + (machine.HHGILAIOCLG == null ? "unknown" : machine.HHGILAIOCLG.luaName));
		}

		private void FixedUpdate()
		{
			fixedFrames++;
			if (fixedFrames == 1 || fixedFrames == 2 || fixedFrames == 5 || fixedFrames == 25 || fixedFrames == 125)
				Snapshot("fixed-" + fixedFrames);
			if (Time.realtimeSinceStartup - startedAt >= 10f)
			{
				Snapshot("time-10s");
				Destroy(this);
			}
		}

		internal void Snapshot(string phase)
		{
			if (machine == null) return;
			try
			{
				List<GameObject> roots = PhysicalRoots(machine);
				List<Collider> colliders = Collect<Collider>(roots);
				List<Rigidbody> bodies = Collect<Rigidbody>(roots);
				List<Joint> joints = Collect<Joint>(roots);
				List<ConfigurableJoint> configurable = Collect<ConfigurableJoint>(roots);
				List<PistonController> pistons = Collect<PistonController>(roots);
				List<JointController> controllers = Collect<JointController>(roots);

				float minContact = float.PositiveInfinity, maxContact = 0f;
				float minExtent = float.PositiveInfinity, maxExtent = 0f;
				int enabledColliders = 0;
				for (int i = 0; i < colliders.Count; i++)
				{
					Collider collider = colliders[i];
					if (collider == null) continue;
					minContact = Mathf.Min(minContact, collider.contactOffset);
					maxContact = Mathf.Max(maxContact, collider.contactOffset);
					if (!collider.enabled) continue;
					enabledColliders++;
					Vector3 extents = collider.bounds.extents;
					float smallest = SmallestPositive(extents.x, extents.y, extents.z);
					if (smallest < float.PositiveInfinity) minExtent = Mathf.Min(minExtent, smallest);
					maxExtent = Mathf.Max(maxExtent, extents.x, extents.y, extents.z);
				}

				float minMass = float.PositiveInfinity, maxMass = 0f;
				float maxVelocity = 0f, maxAngularVelocity = 0f;
				float rootVelocity = 0f, rootAngularVelocity = 0f;
				string fastestBody = "none", fastestAngularBody = "none";
				HashSet<int> rootBodies = new HashSet<int>();
				foreach (GameObject root in roots)
				{
					Rigidbody rootBody = root.GetComponent<Rigidbody>();
					if (rootBody != null) rootBodies.Add(rootBody.GetInstanceID());
				}
				int sleeping = 0, minSolver = int.MaxValue, maxSolver = 0;
				int minVelocitySolver = int.MaxValue, maxVelocitySolver = 0;
				for (int i = 0; i < bodies.Count; i++)
				{
					Rigidbody body = bodies[i];
					if (body == null) continue;
					minMass = Mathf.Min(minMass, body.mass);
					maxMass = Mathf.Max(maxMass, body.mass);
					float velocity = body.velocity.magnitude, angularVelocity = body.angularVelocity.magnitude;
					if (velocity > maxVelocity) { maxVelocity = velocity; fastestBody = BodyLabel(body, rootBodies); }
					if (angularVelocity > maxAngularVelocity) { maxAngularVelocity = angularVelocity; fastestAngularBody = BodyLabel(body, rootBodies); }
					if (rootBodies.Contains(body.GetInstanceID()))
					{
						rootVelocity = Mathf.Max(rootVelocity, velocity);
						rootAngularVelocity = Mathf.Max(rootAngularVelocity, angularVelocity);
					}
					if (body.IsSleeping()) sleeping++;
					minSolver = Math.Min(minSolver, body.solverIterations);
					maxSolver = Math.Max(maxSolver, body.solverIterations);
					minVelocitySolver = Math.Min(minVelocitySolver, body.solverVelocityIterations);
					maxVelocitySolver = Math.Max(maxVelocitySolver, body.solverVelocityIterations);
				}

				float maxAnchorError = 0f;
				string worstJoint = "none";
				Joint worstComponent = null;
				for (int i = 0; i < joints.Count; i++)
				{
					Joint joint = joints[i];
					float error = AnchorError(joint);
					if (error > maxAnchorError)
					{
						maxAnchorError = error;
						worstJoint = joint.GetType().Name + ":" + joint.name;
						worstComponent = joint;
					}
				}

				float minLimit = float.PositiveInfinity, maxLimit = 0f;
				float maxTarget = 0f, maxProjection = 0f;
				for (int i = 0; i < configurable.Count; i++)
				{
					ConfigurableJoint joint = configurable[i];
					if (joint == null) continue;
					float limit = joint.linearLimit.limit;
					minLimit = Mathf.Min(minLimit, limit);
					maxLimit = Mathf.Max(maxLimit, limit);
					maxTarget = Mathf.Max(maxTarget, joint.targetPosition.magnitude);
					maxProjection = Mathf.Max(maxProjection, joint.projectionDistance);
				}

				JointSummary summary = SummarizeControllers(controllers, pistons);
				float contactRatio = minExtent < float.PositiveInfinity && minExtent > 0f ? maxContact / minExtent : 0f;
				MagnificationDownscale.Log("PHYSICS_SNAPSHOT id=" + id + " phase=" + phase + " scale=" + F(scale)
					+ " roots=" + roots.Count + " bodyControllers=" + Count(machine.ILBAAENKMBL)
					+ " colliders=" + colliders.Count + "/" + enabledColliders
					+ " contact=" + Range(minContact, maxContact) + " extent=" + Range(minExtent, maxExtent)
					+ " contactToMinExtent=" + F(contactRatio)
					+ " bodies=" + bodies.Count + " sleeping=" + sleeping + " mass=" + Range(minMass, maxMass)
					+ " velocityMax=" + F(maxVelocity) + " angularVelocityMax=" + F(maxAngularVelocity)
					+ " velocityBody=" + fastestBody + " angularVelocityBody=" + fastestAngularBody
					+ " physicalRootBodies=" + rootBodies.Count + " rootVelocityMax=" + F(rootVelocity)
					+ " rootAngularVelocityMax=" + F(rootAngularVelocity)
					+ " solver=" + IntRange(minSolver, maxSolver) + " velocitySolver=" + IntRange(minVelocitySolver, maxVelocitySolver)
					+ " joints=" + joints.Count + " anchorErrorMax=" + F(maxAnchorError) + " worst=" + worstJoint
					+ " configurable=" + configurable.Count + " linearLimit=" + Range(minLimit, maxLimit)
					+ " targetMax=" + F(maxTarget) + " projectionMax=" + F(maxProjection)
					+ " pistons=" + pistons.Count + " defaultContact=" + F(Physics.defaultContactOffset)
					+ " defaultSolver=" + Physics.defaultSolverIterations + "/" + Physics.defaultSolverVelocityIterations
					+ " controllers=" + summary.Count + " types=" + summary.Types
					+ " hingeExpectedMax=" + F(summary.HingeExpectedMax)
					+ " hingeErrorMax=" + F(summary.HingeErrorMax)
					+ " hardExpectedMax=" + F(summary.HardExpectedMax)
					+ " hardErrorMax=" + F(summary.HardErrorMax)
					+ " hingeHardPivotMax=" + F(summary.HingeHardPivotMax)
					+ " pistonExpectedMax=" + F(summary.PistonExpectedMax)
					+ " pistonAnchorErrorMax=" + F(summary.PistonAnchorErrorMax));
				if (worstComponent != null)
				{
					ConfigurableJoint worstConfigurable = worstComponent as ConfigurableJoint;
					MagnificationDownscale.Log("PHYSICS_WORST_JOINT id=" + id + " phase=" + phase
						+ " joint=" + worstJoint + " own=" + BodyState(worstComponent.GetComponent<Rigidbody>())
						+ " connected=" + BodyState(worstComponent.connectedBody)
						+ (worstConfigurable == null ? "" : " linearMotion=" + worstConfigurable.xMotion + "," + worstConfigurable.yMotion + "," + worstConfigurable.zMotion
							+ " limit=" + F(worstConfigurable.linearLimit.limit) + " target=" + Vector(worstConfigurable.targetPosition))
						+ " metric=endpoint-separation-not-limit-violation");
				}
				LogMassEdges(joints, phase);
				if (phase == "fixed-125" || phase == "time-10s") LogPistonDrives(pistons, phase);
			}
			catch (Exception error)
			{
				MagnificationDownscale.Log("PHYSICS_SNAPSHOT_FAILED id=" + id + " phase=" + phase
					+ " type=" + error.GetType().Name + " message=" + error.Message);
			}
		}

		private void LogPistonDrives(List<PistonController> pistons, string phase)
		{
			if (PistonJointField == null) return;
			foreach (PistonController piston in pistons)
			{
				ConfigurableJoint joint = piston == null ? null : PistonJointField.GetValue(piston) as ConfigurableJoint;
				if (joint == null || joint.connectedBody == null) continue;
				// Report the connected-minus-own displacement in an orthonormal joint frame.
				// TargetPosition uses world-length units; do not apply transform scale twice.
				Vector3 x = joint.axis.normalized;
				Vector3 z = Vector3.Cross(x, joint.secondaryAxis).normalized;
				Vector3 y = Vector3.Cross(z, x).normalized;
				Vector3 delta = joint.connectedBody.transform.TransformPoint(joint.connectedAnchor) - WorldAnchor(joint);
				Vector3 offset = new Vector3(Vector3.Dot(delta, joint.transform.TransformDirection(x)),
					Vector3.Dot(delta, joint.transform.TransformDirection(y)), Vector3.Dot(delta, joint.transform.TransformDirection(z)));
				int axis = joint.xMotion == ConfigurableJointMotion.Limited ? 0 : joint.yMotion == ConfigurableJointMotion.Limited ? 1 : joint.zMotion == ConfigurableJointMotion.Limited ? 2 : -1;
				JointDrive drive = axis == 0 ? joint.xDrive : axis == 1 ? joint.yDrive : joint.zDrive;
				float target = axis < 0 ? 0f : joint.targetPosition[axis];
				float displacement = axis < 0 ? 0f : offset[axis];
				float residual = displacement - target;
				MagnificationDownscale.Log("PISTON_DRIVE_SNAPSHOT id=" + id + " phase=" + phase
					+ " joint=" + joint.name + " block=" + Vector(piston.JNKEKNOAPHO == null ? Vector3.zero : piston.JNKEKNOAPHO.GetPos())
					+ " axis=" + axis + " scale=" + F(scale) + " displacement=" + F(displacement) + " target=" + F(target)
					+ " targetResidual=" + F(residual) + " targetResidualCraft=" + F(scale > 0f ? residual / scale : 0f)
					+ " offset=" + Vector(offset) + " limit=" + F(joint.linearLimit.limit)
					+ " spring=" + F(drive.positionSpring) + " damper=" + F(drive.positionDamper) + " maxForce=" + F(drive.maximumForce)
					+ " gravity=" + Vector(Physics.gravity) + " own=" + BodyState(joint.GetComponent<Rigidbody>())
					+ " connected=" + BodyState(joint.connectedBody));
			}
		}

		private JointSummary SummarizeControllers(List<JointController> controllers, List<PistonController> pistons)
		{
			JointSummary result = new JointSummary();
			Dictionary<string, int> types = new Dictionary<string, int>(StringComparer.Ordinal);
			if (controllers != null)
			{
				for (int i = 0; i < controllers.Count; i++)
				{
					JointController controller = controllers[i];
					if (controller == null) continue;
					result.Count++;
					string type = controller.JNKEKNOAPHO == null ? controller.GetType().Name : controller.JNKEKNOAPHO.type.ToString();
					int count;
					types.TryGetValue(type, out count);
					types[type] = count + 1;
					Vector3 expected = controller.JNKEKNOAPHO == null ? Vector3.zero : controller.JNKEKNOAPHO.GetPos();
					HingeJoint hinge = controller.ALGHGLCBLDJ;
					ConfigurableJoint hard = controller.IPFGIMFBDNM;
					if (hinge != null) result.HingeExpectedMax = Mathf.Max(result.HingeExpectedMax, Vector3.Distance(hinge.anchor, expected));
					if (hard != null) result.HardExpectedMax = Mathf.Max(result.HardExpectedMax, Vector3.Distance(hard.anchor, expected));
					if (hinge != null) result.HingeErrorMax = Mathf.Max(result.HingeErrorMax, AnchorError(hinge));
					if (hard != null) result.HardErrorMax = Mathf.Max(result.HardErrorMax, AnchorError(hard));
					if (hinge != null && hard != null)
						result.HingeHardPivotMax = Mathf.Max(result.HingeHardPivotMax,
							Vector3.Distance(WorldAnchor(hinge), WorldAnchor(hard)));
				}
			}
			result.Types = TypeCounts(types);

			if (PistonJointField != null)
			{
				for (int i = 0; i < pistons.Count; i++)
				{
					PistonController piston = pistons[i];
					ConfigurableJoint joint = piston == null ? null : PistonJointField.GetValue(piston) as ConfigurableJoint;
					if (joint == null) continue;
					Vector3 expected = piston.JNKEKNOAPHO == null ? Vector3.zero : piston.JNKEKNOAPHO.GetPos();
					result.PistonExpectedMax = Mathf.Max(result.PistonExpectedMax, Vector3.Distance(joint.anchor, expected));
					result.PistonAnchorErrorMax = Mathf.Max(result.PistonAnchorErrorMax, AnchorError(joint));
				}
			}
			return result;
		}

		private static List<GameObject> PhysicalRoots(MachineController value)
		{
			List<GameObject> roots = new List<GameObject>();
			HashSet<int> seen = new HashSet<int>();
			AddRoot(value.gameObject, roots, seen);
			if (value.KBLANAFAJFP != null)
				for (int i = 0; i < value.KBLANAFAJFP.Count; i++) AddRoot(value.KBLANAFAJFP[i], roots, seen);
			return roots;
		}

		private static void AddRoot(GameObject root, List<GameObject> roots, HashSet<int> seen)
		{
			if (root != null && seen.Add(root.GetInstanceID())) roots.Add(root);
		}

		private static List<T> Collect<T>(List<GameObject> roots) where T : Component
		{
			List<T> result = new List<T>();
			HashSet<int> seen = new HashSet<int>();
			for (int i = 0; i < roots.Count; i++)
			{
				T[] components = roots[i].GetComponentsInChildren<T>(true);
				for (int j = 0; j < components.Length; j++)
					if (components[j] != null && seen.Add(components[j].GetInstanceID())) result.Add(components[j]);
			}
			return result;
		}

		private static Vector3 WorldAnchor(Joint joint) { return joint.transform.TransformPoint(joint.anchor); }
		private static string Vector(Vector3 value) { return F(value.x) + "," + F(value.y) + "," + F(value.z); }
		private static string BodyState(Rigidbody body)
		{
			if (body == null) return "world";
			return "[" + body.name.Replace(' ', '_') + ":" + body.GetInstanceID()
				+ " mass=" + F(body.mass) + " inertia=" + Vector(body.inertiaTensor)
				+ " com=" + Vector(body.centerOfMass) + " scale=" + Vector(body.transform.lossyScale)
				+ " velocity=" + Vector(body.velocity) + " angular=" + Vector(body.angularVelocity) + "]";
		}
		private static string BodyLabel(Rigidbody body, HashSet<int> roots)
		{
			return body.name.Replace(' ', '_') + ":" + body.GetInstanceID()
				+ ":" + (roots.Contains(body.GetInstanceID()) ? "root" : "child") + ":mass=" + F(body.mass);
		}
		private void LogMassEdges(List<Joint> joints, string phase)
		{
			// Collapse Hinge + hard Configurable duplicates: they connect the same
			// pair. Report observations only; a high ratio is not proof of causality.
			Dictionary<string, MassEdge> pairs = new Dictionary<string, MassEdge>();
			foreach (Joint joint in joints)
			{
				Rigidbody own = joint.GetComponent<Rigidbody>(), other = joint.connectedBody;
				if (own == null || other == null || own == other) continue;
				int a = own.GetInstanceID(), b = other.GetInstanceID();
				string key = Math.Min(a, b) + ":" + Math.Max(a, b);
				MassEdge edge;
				if (!pairs.TryGetValue(key, out edge))
				{
					edge = new MassEdge { Own = own, Other = other,
						Ratio = Mathf.Max(own.mass, other.mass) / Mathf.Max(0.0000001f, Mathf.Min(own.mass, other.mass)) };
					pairs.Add(key, edge);
				}
				edge.Joints++;
			}
			List<MassEdge> ordered = new List<MassEdge>(pairs.Values);
			ordered.Sort(delegate(MassEdge a, MassEdge b) { return b.Ratio.CompareTo(a.Ratio); });
			for (int i = 0; i < Math.Min(5, ordered.Count); i++)
			{
				MassEdge edge = ordered[i];
				MagnificationDownscale.Log("PHYSICS_MASS_EDGE id=" + id + " phase=" + phase + " rank=" + (i + 1)
					+ " ratio=" + F(edge.Ratio) + " joints=" + edge.Joints + " uniquePairs=" + ordered.Count
					+ " own=" + BodyState(edge.Own) + " connected=" + BodyState(edge.Other));
			}
		}
		private sealed class MassEdge
		{
			internal Rigidbody Own, Other;
			internal float Ratio;
			internal int Joints;
		}
		private static float AnchorError(Joint joint)
		{
			if (joint == null) return 0f;
			Vector3 own = WorldAnchor(joint);
			Vector3 connected = joint.connectedBody == null
				? joint.connectedAnchor : joint.connectedBody.transform.TransformPoint(joint.connectedAnchor);
			return Vector3.Distance(own, connected);
		}

		private static string TypeCounts(Dictionary<string, int> values)
		{
			List<string> names = new List<string>(values.Keys);
			names.Sort(StringComparer.Ordinal);
			StringBuilder result = new StringBuilder();
			for (int i = 0; i < names.Count; i++)
			{
				if (i != 0) result.Append(',');
				result.Append(names[i]).Append(':').Append(values[names[i]]);
			}
			return result.Length == 0 ? "none" : result.ToString();
		}

		private static int Count<T>(ICollection<T> values) { return values == null ? 0 : values.Count; }
		private static float SmallestPositive(float a, float b, float c)
		{
			float result = float.PositiveInfinity;
			if (a > 0.0000001f) result = Mathf.Min(result, a);
			if (b > 0.0000001f) result = Mathf.Min(result, b);
			if (c > 0.0000001f) result = Mathf.Min(result, c);
			return result;
		}
		private static string Range(float min, float max) { return (min < float.PositiveInfinity ? F(min) : "none") + ".." + F(max); }
		private static string IntRange(int min, int max)
		{
			return (min == int.MaxValue ? "none" : min.ToString(CultureInfo.InvariantCulture))
				+ ".." + max.ToString(CultureInfo.InvariantCulture);
		}
		private static string F(float value) { return value.ToString("R", CultureInfo.InvariantCulture); }

		private sealed class JointSummary
		{
			internal int Count;
			internal string Types = "none";
			internal float HingeExpectedMax;
			internal float HingeErrorMax;
			internal float HardExpectedMax;
			internal float HardErrorMax;
			internal float HingeHardPivotMax;
			internal float PistonExpectedMax;
			internal float PistonAnchorErrorMax;
		}
	}
}
