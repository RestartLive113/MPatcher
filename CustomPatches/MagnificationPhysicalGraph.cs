using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using McnCraft;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
	// Research observer only. Call Capture(machine, craftName, probeFixedFrame)
	// from the SAME place in native/full probes. No Harmony hook or MonoBehaviour.
	// All physics reads are direct Unity getters. In particular, never call the
	// mass/inertia source bridges, material (instantiates), Reset*, WakeUp or setters.
	internal static class MagnificationPhysicalGraph
	{
		private const int MaximumEntities = 8192;
		private static readonly FieldInfo SledBody = typeof(SledController).GetField("KBCHBCMHFEH", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
		private static readonly bool Requested = Array.IndexOf(Environment.GetCommandLineArgs(), "--mpatcher-craft-graph-probe") >= 0;
		private static MachineController previous;
		private static int samples;

		internal static void Capture(MachineController machine, string craftName, int frame)
		{
			if (!Requested || machine == null || (frame != 1 && frame != 5 && frame != 100)) return;
			if (!ReferenceEquals(previous, machine)) { previous = machine; samples = 0; }
			int bit = frame == 1 ? 1 : frame == 5 ? 2 : 4;
			if ((samples & bit) != 0) return;
			samples |= bit;
			try { new Snapshot(machine, craftName, frame).Run(); }
			catch (Exception error) { MagnificationDownscale.Log("PHYSICAL_GRAPH_FAILED frame=" + frame + " type=" + error.GetType().Name + " message=" + Clean(error.Message)); }
		}

		private sealed class Snapshot
		{
			private readonly MachineController machine;
			private readonly string prefix;
			private readonly bool nativeD6Car;
			private readonly List<GameObject> roots = new List<GameObject>();
			private readonly List<string> rootKeys = new List<string>();
			private readonly Dictionary<int, Rigidbody> bodies = new Dictionary<int, Rigidbody>();
			private readonly Dictionary<int, string> bodyKeys = new Dictionary<int, string>();
			private readonly Dictionary<int, string> roles = new Dictionary<int, string>();
			private readonly List<string> rows = new List<string>();
			private readonly float scale, massFactor, inertiaFactor, forceFactor;
			private readonly Vector3 origin;
			private readonly Quaternion inverseRootRotation;
			private int entities, omitted, failures, jointCount, colliderCount, auxiliaryBodies;

			internal Snapshot(MachineController owner, string craft, int frame)
			{
				machine = owner;
				nativeD6Car = string.Equals(craft, "Car", StringComparison.Ordinal);
				scale = MagnificationPhysicsNormalization.MachineScale(owner);
				float unit = MagnificationCraftProbe.Model == 22 && scale != 1f ? MagnificationCraftProbe.MassUnit : 1f;
				massFactor = unit * scale * scale * scale;
				inertiaFactor = massFactor * scale * scale;
				forceFactor = massFactor * scale;
				origin = owner.transform.position;
				inverseRootRotation = Quaternion.Inverse(owner.transform.rotation);
				prefix = "PHYSICAL_GRAPH\tv=1\tcraft=" + Clean(craft) + "\tframe=" + frame + "\tmodel=" + MagnificationCraftProbe.Model + "\ts=" + F(scale);
				AddRoot(owner.gameObject, "machine");
				if (owner.KBLANAFAJFP != null) for (int i = 0; i < owner.KBLANAFAJFP.Count; i++) AddRoot(owner.KBLANAFAJFP[i], "bodyRoot[" + i.ToString("D4", CultureInfo.InvariantCulture) + "]");
			}

			internal void Run()
			{
				AddBody(machine.NFMPBACKJOJ, "machine", null);
				foreach (GameObject root in roots)
				{
					AddBody(root.GetComponent<Rigidbody>(), "physicalRoot", null);
					foreach (BodyController body in root.GetComponentsInChildren<BodyController>(true)) AddBody(body.NFMPBACKJOJ, "bodyController", null);
					foreach (WheelController wheel in root.GetComponentsInChildren<WheelController>(true))
						AddBody(wheel.EKHOCFCEGCB, "liveTire", Key(wheel) + "/liveTire");
					foreach (SledController sled in root.GetComponentsInChildren<SledController>(true))
						if (SledBody != null) AddBody(SledBody.GetValue(sled) as Rigidbody, "sledRoller", Key(sled) + "/sledRoller");
					foreach (FixedJoint joint in root.GetComponentsInChildren<FixedJoint>(true))
						if (joint.connectedBody != null) AddBody(joint.GetComponent<Rigidbody>(), "fixedJointHelper", null);
				}
				List<GameObject> scan = new List<GameObject>(roots);
				foreach (Rigidbody body in bodies.Values) if (!scan.Contains(body.gameObject)) scan.Add(body.gameObject);
				List<Rigidbody> sorted = new List<Rigidbody>(bodies.Values);
				sorted.Sort(delegate(Rigidbody a, Rigidbody b) { return StringComparer.Ordinal.Compare(BodyKey(a), BodyKey(b)); });
				foreach (Rigidbody body in sorted) SafeBody(body);
				HashSet<int> seenJoints = new HashSet<int>(), seenColliders = new HashSet<int>(), seenAux = new HashSet<int>();
				List<Joint> joints = new List<Joint>(); List<Collider> colliders = new List<Collider>();
				foreach (GameObject root in scan)
				{
					foreach (Joint joint in root.GetComponentsInChildren<Joint>(true))
					{
						Rigidbody own = joint.GetComponent<Rigidbody>();
						if (own != null && bodies.ContainsKey(own.GetInstanceID()) && seenJoints.Add(joint.GetInstanceID())) joints.Add(joint);
					}
					foreach (Collider collider in root.GetComponentsInChildren<Collider>(true))
						if ((collider.attachedRigidbody == null || bodies.ContainsKey(collider.attachedRigidbody.GetInstanceID())) && seenColliders.Add(collider.GetInstanceID())) colliders.Add(collider);
					foreach (Rigidbody body in root.GetComponentsInChildren<Rigidbody>(true))
						if (!bodies.ContainsKey(body.GetInstanceID()) && seenAux.Add(body.GetInstanceID()))
						{ auxiliaryBodies++; Emit("auxiliary", Key(body), "name=" + Clean(body.name) + "\treason=not-canonical-body-live-tire-sled-fixed\tkinematic=" + B(body.isKinematic)); }
				}
				joints.Sort(delegate(Joint a, Joint b) { return StringComparer.Ordinal.Compare(Key(a), Key(b)); });
				colliders.Sort(delegate(Collider a, Collider b) { return StringComparer.Ordinal.Compare(Key(a), Key(b)); });
				foreach (Joint joint in joints) SafeJoint(joint);
				foreach (Collider collider in colliders) SafeCollider(collider);
				string header = prefix + "\tkind=begin\tnormalizedLaw=L_s,M_s3,T_1\tmassFactor=" + F(massFactor) + "\tinertiaFactor=" + F(inertiaFactor) + "\tforceFactor=" + F(forceFactor)
					+ "\torigin=" + V(origin) + "\trootRotation=" + Q(machine.transform.rotation) + "\tgravity=" + V(Physics.gravity)
					+ "\tglobalGravityOverS_notEffective=" + V(Physics.gravity / scale) + "\tfixedDelta=" + F(Time.fixedDeltaTime) + "\tfixedTime=" + F(Time.fixedTime)
					+ "\tdefaultContact=" + F(Physics.defaultContactOffset) + "\tdefaultSolver=" + Physics.defaultSolverIterations + "," + Physics.defaultSolverVelocityIterations
					+ "\trootCount=" + roots.Count + "\tmaxLinearVelocity=not-in-this-Unity-API\tmutation=none\tposeFrame=root-rotation-only-no-scale";
				// One log call per sample, rather than one synchronous logger call per entity.
				// This still allocates/logs; use it for graph inspection, not timing benchmarks.
				StringBuilder text = new StringBuilder(header);
				foreach (string row in rows) text.Append('\n').Append(row);
				text.Append('\n').Append(prefix).Append("\tkind=end\tbodies=").Append(bodies.Count).Append("\tjoints=").Append(jointCount).Append("\tcolliders=").Append(colliderCount)
					.Append("\tauxiliaryBodies=").Append(auxiliaryBodies).Append("\tentities=").Append(entities).Append("\tomitted=").Append(omitted).Append("\tfailures=").Append(failures);
				MagnificationDownscale.Log(text.ToString());
			}

			private void AddRoot(GameObject root, string role)
			{
				if (root == null || roots.Contains(root)) return;
				roots.Add(root); rootKeys.Add(role + ":" + Clean(root.name));
			}
			private void AddBody(Rigidbody body, string role, string preferred)
			{
				if (body == null) return;
				int id = body.GetInstanceID(); string existing;
				if (roles.TryGetValue(id, out existing)) { if (existing.IndexOf(role, StringComparison.Ordinal) < 0) roles[id] = existing + "," + role; return; }
				bodies[id] = body; roles[id] = role; bodyKeys[id] = preferred == null ? Key(body) : preferred + ":" + Clean(body.name) + "#Rigidbody[0]";
			}
			private string BodyKey(Rigidbody body)
			{
				if (body == null) return "world";
				string key; return bodyKeys.TryGetValue(body.GetInstanceID(), out key) ? key : "external:" + Key(body);
			}
			private string Key(Component component)
			{
				Transform current = component.transform;
				List<string> segments = new List<string>();
				while (current != null)
				{
					int rootIndex = roots.IndexOf(current.gameObject);
					if (rootIndex >= 0) { segments.Add(rootKeys[rootIndex]); break; }
					int ordinal = 0;
					if (current.parent != null) for (int i = 0; i < current.parent.childCount; i++)
					{
						Transform sibling = current.parent.GetChild(i); if (sibling == current) break;
						if (sibling.name == current.name) ordinal++;
					}
					segments.Add(Clean(current.name) + "[" + ordinal + "]"); current = current.parent;
				}
				segments.Reverse();
				int componentOrdinal = 0;
				foreach (Component sibling in component.GetComponents(component.GetType())) { if (ReferenceEquals(sibling, component)) break; componentOrdinal++; }
				return string.Join("/", segments.ToArray()) + "#" + component.GetType().Name + "[" + componentOrdinal + "]";
			}
			private Vector3 RootPoint(Vector3 world) { return inverseRootRotation * (world - origin) / scale; }
			private Vector3 RootVector(Vector3 world) { return inverseRootRotation * world; }
			private string Pose(Transform transform)
			{
				return "\tlocalPosition=" + V(transform.localPosition) + "\tlocalRotation=" + Q(transform.localRotation) + "\tlocalScale=" + V(transform.localScale)
					+ "\tlossyScale=" + V(transform.lossyScale) + "\tworldPosition=" + V(transform.position) + "\trootPositionCraft=" + V(RootPoint(transform.position))
					+ "\trootRotation=" + Q(inverseRootRotation * transform.rotation) + "\tactive=" + B(transform.gameObject.activeInHierarchy);
			}
			private void Emit(string kind, string key, string fields)
			{
				if (entities++ >= MaximumEntities) { omitted++; return; }
				rows.Add(prefix + "\tkind=" + kind + "\tkey=" + key + "\t" + fields);
			}
			private void Failed(Component component, Exception error)
			{
				failures++; Emit("error", Key(component), "exception=" + error.GetType().Name + "\tmessage=" + Clean(error.Message));
			}
			private void SafeBody(Rigidbody body)
			{
				try
				{
					Vector3 com = body.worldCenterOfMass;
					Emit("body", BodyKey(body), "role=" + roles[body.GetInstanceID()] + "\tname=" + Clean(body.name) + "\tmass=" + F(body.mass) + "\tmassCraft=" + F(body.mass / massFactor)
						+ "\tinertia=" + V(body.inertiaTensor) + "\tinertiaCraft=" + V(body.inertiaTensor / inertiaFactor) + "\tinertiaRotation=" + Q(body.inertiaTensorRotation)
						+ "\tcomLocal=" + V(body.centerOfMass) + "\tcomOwnCraft=" + V(Quaternion.Inverse(body.rotation) * (com - body.position) / scale) + "\tcomRootCraft=" + V(RootPoint(com))
						+ "\trigidbodyPositionCraft=" + V(RootPoint(body.position)) + "\trigidbodyRotation=" + Q(inverseRootRotation * body.rotation)
						+ "\tvelocity=" + V(body.velocity) + "\tvelocityCraft=" + V(RootVector(body.velocity) / scale) + "\tangularVelocity=" + V(RootVector(body.angularVelocity))
						+ "\tconstraints=" + body.constraints + "\tsolver=" + body.solverIterations + "," + body.solverVelocityIterations + "\tgravity=" + B(body.useGravity)
						+ "\tdrag=" + F(body.drag) + "\tangularDrag=" + F(body.angularDrag) + "\tsleeping=" + B(body.IsSleeping()) + "\tsleepThreshold=" + F(body.sleepThreshold)
						+ "\tsleepThresholdCraft=" + F(body.sleepThreshold / (scale * scale)) + "\tmaxAngularVelocity=" + F(body.maxAngularVelocity)
						+ "\tmaxDepenetrationVelocity=" + F(body.maxDepenetrationVelocity) + "\tmaxDepenetrationVelocityCraft=" + F(body.maxDepenetrationVelocity / scale)
						+ (nativeD6Car ? MagnificationMassCacheNative.GraphFields(body) : "")
						+ "\tkinematic=" + B(body.isKinematic) + "\tdetectCollisions=" + B(body.detectCollisions) + "\tcollisionMode=" + body.collisionDetectionMode + "\tinterpolation=" + body.interpolation + Pose(body.transform));
				}
				catch (Exception error) { Failed(body, error); }
			}
			private void SafeJoint(Joint joint)
			{
				try
				{
					jointCount++;
					Rigidbody own = joint.GetComponent<Rigidbody>(), other = joint.connectedBody;
					Vector3 anchor = joint.transform.TransformPoint(joint.anchor);
					Vector3 connected = other == null ? joint.connectedAnchor : other.transform.TransformPoint(joint.connectedAnchor);
					string fields = "type=" + joint.GetType().Name + "\town=" + BodyKey(own) + "\tconnected=" + BodyKey(other) + "\tanchorLocal=" + V(joint.anchor) + "\tconnectedAnchorLocal=" + V(joint.connectedAnchor)
						+ "\tanchorRootCraft=" + V(RootPoint(anchor)) + "\tconnectedAnchorRootCraft=" + V(RootPoint(connected)) + "\tendpointDeltaCraft=" + V(RootVector(connected - anchor) / scale)
						+ "\taxis=" + V(joint.axis) + "\taxisRoot=" + V(RootVector(joint.transform.TransformDirection(joint.axis))) + "\tautoAnchor=" + B(joint.autoConfigureConnectedAnchor)
						+ "\tenableCollision=" + B(joint.enableCollision) + "\tpreprocessing=" + B(joint.enablePreprocessing) + "\tbreakForce=" + F(joint.breakForce) + "\tbreakForceCraft=" + F(joint.breakForce / forceFactor)
						+ "\tbreakTorque=" + F(joint.breakTorque) + "\tbreakTorqueCraft=" + F(joint.breakTorque / inertiaFactor) + "\tcurrentForceCraft=" + V(RootVector(joint.currentForce) / forceFactor)
						+ "\tcurrentTorqueCraft=" + V(RootVector(joint.currentTorque) / inertiaFactor);
					ConfigurableJoint configurable = joint as ConfigurableJoint; HingeJoint hinge = joint as HingeJoint; SpringJoint spring = joint as SpringJoint; CharacterJoint character = joint as CharacterJoint;
					if (configurable != null) fields += Configurable(configurable) + (nativeD6Car ? MagnificationD6LimitNative.GraphFields(configurable, scale) : "");
					else if (hinge != null) fields += Hinge(hinge);
					else if (spring != null) fields += "\tspring=" + F(spring.spring) + "\tspringCraft=" + F(spring.spring / massFactor) + "\tdamper=" + F(spring.damper) + "\tdamperCraft=" + F(spring.damper / massFactor)
						+ "\tminDistance=" + F(spring.minDistance) + "\tminDistanceCraft=" + F(spring.minDistance / scale) + "\tmaxDistance=" + F(spring.maxDistance) + "\tmaxDistanceCraft=" + F(spring.maxDistance / scale) + "\ttolerance=" + F(spring.tolerance) + "\ttoleranceCraft=" + F(spring.tolerance / scale);
					else if (character != null) fields += Character(character);
					else if (!(joint is FixedJoint)) fields += "\ttypedCoverage=unsupported";
					Emit("joint", Key(joint), fields);
				}
				catch (Exception error) { Failed(joint, error); }
			}
			private string Configurable(ConfigurableJoint j)
			{
				return "\tsecondaryAxis=" + V(j.secondaryAxis) + "\tmotion=" + j.xMotion + "," + j.yMotion + "," + j.zMotion + "\tangularMotion=" + j.angularXMotion + "," + j.angularYMotion + "," + j.angularZMotion
					+ "\tconfiguredWorld=" + B(j.configuredInWorldSpace) + "\tswapBodies=" + B(j.swapBodies)
					+ "\ttargetPosition=" + V(j.targetPosition) + "\ttargetPositionCraft=" + V(j.targetPosition / scale) + "\ttargetVelocity=" + V(j.targetVelocity) + "\ttargetVelocityCraft=" + V(j.targetVelocity / scale)
					+ "\ttargetRotation=" + Q(j.targetRotation) + "\ttargetAngularVelocity=" + V(j.targetAngularVelocity) + "\trotationDriveMode=" + j.rotationDriveMode
					+ Limit("linearLimit", j.linearLimit, scale) + Limit("lowAngularX", j.lowAngularXLimit, 1f) + Limit("highAngularX", j.highAngularXLimit, 1f) + Limit("angularY", j.angularYLimit, 1f) + Limit("angularZ", j.angularZLimit, 1f)
					+ LimitSpring("linearLimitSpring", j.linearLimitSpring, massFactor) + LimitSpring("angularXLimitSpring", j.angularXLimitSpring, inertiaFactor) + LimitSpring("angularYZLimitSpring", j.angularYZLimitSpring, inertiaFactor)
					+ Drive("xDrive", j.xDrive, false) + Drive("yDrive", j.yDrive, false) + Drive("zDrive", j.zDrive, false)
					+ Drive("angularXDrive", j.angularXDrive, true) + Drive("angularYZDrive", j.angularYZDrive, true) + Drive("slerpDrive", j.slerpDrive, true)
					+ "\tprojectionMode=" + j.projectionMode + "\tprojectionDistance=" + F(j.projectionDistance) + "\tprojectionDistanceCraft=" + F(j.projectionDistance / scale) + "\tprojectionAngle=" + F(j.projectionAngle);
			}
			private string Hinge(HingeJoint j)
			{
				JointMotor motor = j.motor; JointSpring spring = j.spring; JointLimits limit = j.limits;
				return "\tuseMotor=" + B(j.useMotor) + "\tuseSpring=" + B(j.useSpring) + "\tuseLimits=" + B(j.useLimits) + "\tangle=" + F(j.angle) + "\tangularSpeed=" + F(j.velocity)
					+ "\tmotorForce=" + F(motor.force) + "\tmotorForceCraft=" + F(motor.force / inertiaFactor) + "\tmotorTargetVelocity=" + F(motor.targetVelocity) + "\tmotorFreeSpin=" + B(motor.freeSpin)
					+ "\tspring=" + F(spring.spring) + "\tspringCraft=" + F(spring.spring / inertiaFactor) + "\tdamper=" + F(spring.damper) + "\tdamperCraft=" + F(spring.damper / inertiaFactor) + "\ttargetAngle=" + F(spring.targetPosition)
					+ "\tlimits=" + F(limit.min) + "," + F(limit.max) + "\tlimitBounce=" + F(limit.bounciness) + "\tlimitBounceMinVelocity=" + F(limit.bounceMinVelocity) + "\tlimitContactDistance=" + F(limit.contactDistance);
			}
			private string Character(CharacterJoint j)
			{
				return "\tswingAxis=" + V(j.swingAxis) + LimitSpring("twistLimitSpring", j.twistLimitSpring, inertiaFactor) + LimitSpring("swingLimitSpring", j.swingLimitSpring, inertiaFactor)
					+ Limit("lowTwist", j.lowTwistLimit, 1f) + Limit("highTwist", j.highTwistLimit, 1f) + Limit("swing1", j.swing1Limit, 1f) + Limit("swing2", j.swing2Limit, 1f)
					+ "\tenableProjection=" + B(j.enableProjection) + "\tprojectionDistance=" + F(j.projectionDistance) + "\tprojectionDistanceCraft=" + F(j.projectionDistance / scale) + "\tprojectionAngle=" + F(j.projectionAngle)
					+ "\trotationDrive=unsupported-in-Unity5\ttargetRotation=unsupported-in-Unity5\ttargetAngularVelocity=unsupported-in-Unity5";
			}
			private string Drive(string name, JointDrive drive, bool angular)
			{
				float k = angular ? inertiaFactor : massFactor, f = angular ? inertiaFactor : forceFactor;
				return "\t" + name + "=" + F(drive.positionSpring) + "," + F(drive.positionDamper) + "," + F(drive.maximumForce)
					+ "\t" + name + "Craft=" + F(drive.positionSpring / k) + "," + F(drive.positionDamper / k) + "," + F(drive.maximumForce / f);
			}
			private string Limit(string name, SoftJointLimit limit, float divisor)
			{
				return "\t" + name + "=" + F(limit.limit) + "," + F(limit.bounciness) + "," + F(limit.contactDistance)
					+ "\t" + name + "Craft=" + F(limit.limit / divisor) + "," + F(limit.bounciness) + "," + F(limit.contactDistance / divisor);
			}
			private string LimitSpring(string name, SoftJointLimitSpring spring, float divisor)
			{
				return "\t" + name + "=" + F(spring.spring) + "," + F(spring.damper) + "\t" + name + "Craft=" + F(spring.spring / divisor) + "," + F(spring.damper / divisor);
			}
			private void SafeCollider(Collider collider)
			{
				try
				{
					colliderCount++;
					Bounds bounds = collider.bounds;
					string fields = "type=" + collider.GetType().Name + "\tbody=" + BodyKey(collider.attachedRigidbody) + "\tenabled=" + B(collider.enabled) + "\ttrigger=" + B(collider.isTrigger)
						+ "\tlayer=" + collider.gameObject.layer + "\tcontactOffset=" + F(collider.contactOffset) + "\tcontactOffsetCraft=" + F(collider.contactOffset / scale)
						+ "\tboundsCenterCraft=" + V(RootPoint(bounds.center)) + "\tboundsExtentsWorldCraft=" + V(bounds.extents / scale) + Pose(collider.transform);
					BoxCollider box = collider as BoxCollider; SphereCollider sphere = collider as SphereCollider; CapsuleCollider capsule = collider as CapsuleCollider; MeshCollider mesh = collider as MeshCollider; WheelCollider wheel = collider as WheelCollider; TerrainCollider terrain = collider as TerrainCollider;
					if (box != null) fields += "\tcenterLocal=" + V(box.center) + "\tsizeLocal=" + V(box.size) + "\tcenterCraft=" + V(RootPoint(box.transform.TransformPoint(box.center))) + "\tsizeAxesCraft=" + V(Vector3.Scale(box.size, Abs(box.transform.lossyScale)) / scale);
					else if (sphere != null) fields += "\tcenterLocal=" + V(sphere.center) + "\tradiusLocal=" + F(sphere.radius) + "\tcenterCraft=" + V(RootPoint(sphere.transform.TransformPoint(sphere.center))) + "\tradiusAxesCraft=" + V(Abs(sphere.transform.lossyScale) * (sphere.radius / scale));
					else if (capsule != null) fields += "\tcenterLocal=" + V(capsule.center) + "\tradiusLocal=" + F(capsule.radius) + "\theightLocal=" + F(capsule.height) + "\tdirection=" + capsule.direction + "\tcenterCraft=" + V(RootPoint(capsule.transform.TransformPoint(capsule.center))) + "\tradiusAxesCraft=" + V(Abs(capsule.transform.lossyScale) * (capsule.radius / scale)) + "\theightAxesCraft=" + V(Abs(capsule.transform.lossyScale) * (capsule.height / scale));
					else if (mesh != null)
					{
						Mesh shape = mesh.sharedMesh;
						fields += "\tconvex=" + B(mesh.convex) + "\tinflateMesh=" + B(mesh.inflateMesh) + "\tskinWidth=" + F(mesh.skinWidth) + "\tskinWidthCraft=" + F(mesh.skinWidth / scale);
						if (shape != null) fields += "\tmesh=" + Clean(shape.name) + "\tmeshVertices=" + shape.vertexCount + "\tmeshSubmeshes=" + shape.subMeshCount + "\tmeshBoundsCenter=" + V(shape.bounds.center) + "\tmeshBoundsSize=" + V(shape.bounds.size) + "\tmeshSizeAxesCraft=" + V(Vector3.Scale(shape.bounds.size, Abs(mesh.transform.lossyScale)) / scale);
					}
					else if (wheel != null) fields += Wheel(wheel);
					else if (terrain != null && terrain.terrainData != null) fields += "\tterrainSize=" + V(terrain.terrainData.size) + "\tterrainSizeCraft=" + V(Vector3.Scale(terrain.terrainData.size, Abs(terrain.transform.lossyScale)) / scale);
					else fields += "\ttypedCoverage=unsupported";
					PhysicMaterial material = collider.sharedMaterial;
					if (material == null) fields += "\tmaterial=default";
					else fields += "\tmaterial=" + Clean(material.name) + "\tdynamicFriction=" + F(material.dynamicFriction) + "\tstaticFriction=" + F(material.staticFriction) + "\tbounciness=" + F(material.bounciness) + "\tfrictionCombine=" + material.frictionCombine + "\tbounceCombine=" + material.bounceCombine;
					Emit("collider", Key(collider), fields);
				}
				catch (Exception error) { Failed(collider, error); }
			}
			private string Wheel(WheelCollider wheel)
			{
				JointSpring spring = wheel.suspensionSpring;
				return "\tcenterLocal=" + V(wheel.center) + "\tcenterCraft=" + V(RootPoint(wheel.transform.TransformPoint(wheel.center))) + "\tradius=" + F(wheel.radius) + "\tradiusAxesCraft=" + V(Abs(wheel.transform.lossyScale) * (wheel.radius / scale))
					+ "\tsuspensionDistance=" + F(wheel.suspensionDistance) + "\tsuspensionDistanceOverS=" + F(wheel.suspensionDistance / scale) + "\tforceAppPointDistance=" + F(wheel.forceAppPointDistance) + "\tforceAppPointDistanceOverS=" + F(wheel.forceAppPointDistance / scale)
					+ "\twheelMass=" + F(wheel.mass) + "\twheelMassCraft=" + F(wheel.mass / massFactor) + "\tsprungMass=" + F(wheel.sprungMass) + "\tsprungMassCraft=" + F(wheel.sprungMass / massFactor)
					+ "\tsuspensionSpring=" + F(spring.spring) + "," + F(spring.damper) + "," + F(spring.targetPosition) + "\tsuspensionSpringCraft=" + F(spring.spring / massFactor) + "," + F(spring.damper / massFactor) + "," + F(spring.targetPosition)
					+ "\twheelDampingRate=" + F(wheel.wheelDampingRate) + "\tmotorTorque=" + F(wheel.motorTorque) + "\tmotorTorqueCraft=" + F(wheel.motorTorque / inertiaFactor) + "\tbrakeTorque=" + F(wheel.brakeTorque) + "\tbrakeTorqueCraft=" + F(wheel.brakeTorque / inertiaFactor)
					+ "\tsteerAngle=" + F(wheel.steerAngle) + "\trpm=" + F(wheel.rpm) + "\tisGrounded=" + B(wheel.isGrounded) + Friction("forwardFriction", wheel.forwardFriction) + Friction("sidewaysFriction", wheel.sidewaysFriction);
			}
			private string Friction(string name, WheelFrictionCurve f) { return "\t" + name + "=" + F(f.extremumSlip) + "," + F(f.extremumValue) + "," + F(f.asymptoteSlip) + "," + F(f.asymptoteValue) + "," + F(f.stiffness); }
		}
		private static Vector3 Abs(Vector3 value) { return new Vector3(Mathf.Abs(value.x), Mathf.Abs(value.y), Mathf.Abs(value.z)); }
		private static string B(bool value) { return value ? "1" : "0"; }
		private static string F(float value) { return value.ToString("R", CultureInfo.InvariantCulture); }
		private static string V(Vector3 value) { return F(value.x) + "," + F(value.y) + "," + F(value.z); }
		private static string Q(Quaternion value)
		{
			// q and -q are the same pose; canonical sign makes independent diffs useful.
			if (value.w < 0f || (value.w == 0f && (value.z < 0f || (value.z == 0f && (value.y < 0f || (value.y == 0f && value.x < 0f)))))) value = new Quaternion(-value.x, -value.y, -value.z, -value.w);
			return F(value.x) + "," + F(value.y) + "," + F(value.z) + "," + F(value.w);
		}
		private static string Clean(string value) { return (value ?? "").Replace("%", "%25").Replace("\t", "%09").Replace("\r", "%0D").Replace("\n", "%0A").Replace("/", "%2F").Replace("#", "%23"); }
	}
}
