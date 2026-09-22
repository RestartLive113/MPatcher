using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using HarmonyLib;
using McnCraft;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
	// Joint.anchor and Joint.connectedAnchor are independent native values in the
	// legacy Unity build. Scaling only one endpoint makes PhysX pull the bodies
	// apart. Correct both endpoints after the game's complete SetJoint setup.
	internal static class MagnificationJointAnchors
	{
		private static readonly FieldInfo PistonJointField = AccessTools.Field(typeof(PistonController), "PDNNFOJPIDM");
		private static readonly HashSet<int> LoggedBaseMachines = new HashSet<int>();
		private static readonly HashSet<int> LoggedPistonMachines = new HashSet<int>();
		private static readonly HashSet<int> LoggedRebuiltBaseMachines = new HashSet<int>();
		private static readonly HashSet<int> LoggedRebuiltPistonMachines = new HashSet<int>();
		private static readonly Dictionary<int, PistonEndpoints> PendingPistonEndpoints = new Dictionary<int, PistonEndpoints>();

		internal static void Register(Harmony harmony)
		{
			Type[] arguments = { typeof(GameObject), typeof(GameObject) };
			MethodInfo baseTarget = AccessTools.DeclaredMethod(typeof(JointController), "SetJoint", arguments);
			MethodInfo pistonTarget = AccessTools.DeclaredMethod(typeof(PistonController), "SetJoint", arguments);
			MethodInfo basePostfix = AccessTools.Method(typeof(MagnificationJointAnchors), "BaseSetJointPostfix");
			MethodInfo pistonPostfix = AccessTools.Method(typeof(MagnificationJointAnchors), "PistonSetJointPostfix");
			if (baseTarget == null || basePostfix == null) throw new MissingMethodException("JointController", "SetJoint");
			if (pistonTarget == null || pistonPostfix == null || PistonJointField == null)
				throw new MissingMethodException("PistonController", "SetJoint/PDNNFOJPIDM");
			harmony.Patch(baseTarget, null, new HarmonyMethod(basePostfix), null, null);
			harmony.Patch(pistonTarget, null, new HarmonyMethod(pistonPostfix), null, null);
			MagnificationDownscale.Log("HOOK target=JointController.SetJoint correction=both-anchor-endpoints");
			MagnificationDownscale.Log("HOOK target=PistonController.SetJoint correction=scaled-anchor-and-travel-endpoints");
		}

		private static void BaseSetJointPostfix(JointController __instance)
		{
			if (__instance == null || __instance.FPEBEEJGFPI == null || __instance.ALGHGLCBLDJ == null) return;
			float scale = MagnificationPhysicsNormalization.MachineScale(__instance.FPEBEEJGFPI);
			if (scale == 1f) return;

			HingeJoint hinge = __instance.ALGHGLCBLDJ;
			Vector3 before = hinge.anchor;
			float bodyScale = hinge.transform.lossyScale.x;
			float endpointFactor = EndpointFactor(scale, bodyScale);
			Vector3 localAnchor = before * endpointFactor;
			SetSamePivot(hinge, localAnchor);
			if (__instance.IPFGIMFBDNM != null)
				SetSamePivot(__instance.IPFGIMFBDNM, localAnchor);

			int machineId = __instance.FPEBEEJGFPI.GetInstanceID();
			if ((endpointFactor == 1f ? LoggedRebuiltBaseMachines : LoggedBaseMachines).Add(machineId))
				MagnificationDownscale.Log("JOINT_ENDPOINT_CORRECTION_ACTIVE machine=" + machineId
					+ " scale=" + F(scale) + " firstType=" + TypeName(__instance)
					+ " bodyScale=" + F(bodyScale) + " endpointFactor=" + F(endpointFactor)
					+ " phase=" + (endpointFactor == 1f ? "post-scale-rebuild" : "pre-scale-initialize")
					+ " before=" + V(before) + " after=" + V(hinge.anchor)
					+ " anchorError=" + F(AnchorError(hinge)));
		}

		private static void PistonSetJointPostfix(PistonController __instance)
		{
			if (__instance == null || __instance.FPEBEEJGFPI == null) return;
			float scale = MagnificationPhysicsNormalization.MachineScale(__instance.FPEBEEJGFPI);
			if (scale == 1f) return;
			ConfigurableJoint joint = PistonJointField.GetValue(__instance) as ConfigurableJoint;
			if (joint == null) return;

			Vector3 beforeAnchor = joint.anchor;
			float bodyScale = joint.transform.lossyScale.x;
			float endpointFactor = EndpointFactor(scale, bodyScale);
			Vector3 connectedWorld = ConnectedWorld(joint);
			Vector3 connectedInSource = joint.transform.InverseTransformPoint(connectedWorld);
			if (endpointFactor != 1f) PendingPistonEndpoints[joint.GetInstanceID()] = new PistonEndpoints
			{
				Joint = joint,
				Source = joint.transform,
				Anchor = beforeAnchor,
				Connected = connectedInSource
			};
			joint.autoConfigureConnectedAnchor = false;
			joint.anchor = beforeAnchor * endpointFactor;
			SetConnectedWorld(joint, joint.transform.TransformPoint(connectedInSource * endpointFactor));

			int machineId = __instance.FPEBEEJGFPI.GetInstanceID();
			if ((endpointFactor == 1f ? LoggedRebuiltPistonMachines : LoggedPistonMachines).Add(machineId))
				MagnificationDownscale.Log("PISTON_ENDPOINT_CORRECTION_ACTIVE machine=" + machineId
					+ " scale=" + F(scale) + " before=" + V(beforeAnchor) + " after=" + V(joint.anchor)
					+ " bodyScale=" + F(bodyScale) + " endpointFactor=" + F(endpointFactor)
					+ " phase=" + (endpointFactor == 1f ? "post-scale-rebuild" : "pre-scale-initialize")
					+ " separation=" + F(AnchorError(joint)));
		}

		// Respawn calls Restore2 -> SetJoint again with the root already scaled.
		// Convert world scale to the root's actual local units, not a second scale.
		internal static float EndpointFactor(float machineScale, float bodyScale)
		{
			if (bodyScale <= 0f || float.IsNaN(bodyScale) || float.IsInfinity(bodyScale))
				throw new InvalidOperationException("Invalid physical root scale");
			if (Math.Abs(machineScale - bodyScale) <= Math.Max(machineScale, bodyScale) * 0.000001f) return 1f;
			return machineScale / bodyScale;
		}

		// MachineController.Initialize applies the final scale to every detached
		// physical root after SetJoint has completed. That transform change makes
		// the connected anchors calculated above stale. Rebuild both endpoints only
		// after the native Initialize method has finished.
		internal static void FinalizeMachine(MachineController machine)
		{
			if (machine == null) return;
			float scale = MagnificationPhysicsNormalization.MachineScale(machine);
			if (scale == 1f) return;

			List<GameObject> roots = PhysicalRoots(machine);
			HashSet<int> seen = new HashSet<int>();
			int controllers = 0, hinges = 0, hard = 0, pistons = 0;
			float hingeErrorMax = 0f, hardErrorMax = 0f, pistonErrorMax = 0f;
			for (int i = 0; i < roots.Count; i++)
			{
				JointController[] found = roots[i].GetComponentsInChildren<JointController>(true);
				for (int j = 0; j < found.Length; j++)
				{
					JointController controller = found[j];
					if (controller == null || !seen.Add(controller.GetInstanceID())) continue;
					controllers++;
					// The physical root already has localScale=scale here. Joint anchors
					// remain in the root's unscaled local coordinate system; multiplying
					// them again would apply Magnification twice.
					Vector3 expected = controller.JNKEKNOAPHO == null
						? Vector3.zero : controller.JNKEKNOAPHO.GetPos();
					if (controller.ALGHGLCBLDJ != null)
					{
						SetSamePivot(controller.ALGHGLCBLDJ, expected);
						hinges++;
						hingeErrorMax = Mathf.Max(hingeErrorMax, AnchorError(controller.ALGHGLCBLDJ));
					}
					if (controller.IPFGIMFBDNM != null)
					{
						SetSamePivot(controller.IPFGIMFBDNM, expected);
						hard++;
						hardErrorMax = Mathf.Max(hardErrorMax, AnchorError(controller.IPFGIMFBDNM));
					}

					PistonController piston = controller as PistonController;
					if (piston == null || PistonJointField == null) continue;
					ConfigurableJoint pistonJoint = PistonJointField.GetValue(piston) as ConfigurableJoint;
					PistonEndpoints endpoints;
					if (pistonJoint == null || !PendingPistonEndpoints.TryGetValue(pistonJoint.GetInstanceID(), out endpoints)
						|| endpoints.Joint != pistonJoint || endpoints.Source == null) continue;
					pistonJoint.autoConfigureConnectedAnchor = false;
					pistonJoint.anchor = endpoints.Anchor;
					SetConnectedWorld(pistonJoint, endpoints.Source.TransformPoint(endpoints.Connected));
					PendingPistonEndpoints.Remove(pistonJoint.GetInstanceID());
					pistons++;
					pistonErrorMax = Mathf.Max(pistonErrorMax, AnchorError(pistonJoint));
				}
			}
			MagnificationDownscale.Log("JOINT_ENDPOINTS_FINALIZED machine=" + machine.GetInstanceID()
				+ " scale=" + F(scale) + " roots=" + roots.Count + " controllers=" + controllers
				+ " hinges=" + hinges + " hard=" + hard + " pistons=" + pistons
				+ " hingeErrorMax=" + F(hingeErrorMax) + " hardErrorMax=" + F(hardErrorMax)
				+ " pistonErrorMax=" + F(pistonErrorMax) + " endpointUnits=body-local-after-scale");
		}

		private static List<GameObject> PhysicalRoots(MachineController machine)
		{
			List<GameObject> roots = new List<GameObject>();
			HashSet<int> seen = new HashSet<int>();
			AddRoot(machine.gameObject, roots, seen);
			if (machine.KBLANAFAJFP != null)
				for (int i = 0; i < machine.KBLANAFAJFP.Count; i++) AddRoot(machine.KBLANAFAJFP[i], roots, seen);
			return roots;
		}

		private static void AddRoot(GameObject root, List<GameObject> roots, HashSet<int> seen)
		{
			if (root != null && seen.Add(root.GetInstanceID())) roots.Add(root);
		}

		internal static void SetSamePivot(Joint joint, Vector3 localAnchor)
		{
			joint.autoConfigureConnectedAnchor = false;
			joint.anchor = localAnchor;
			SetConnectedWorld(joint, joint.transform.TransformPoint(localAnchor));
		}

		private static void SetConnectedWorld(Joint joint, Vector3 world)
		{
			joint.connectedAnchor = joint.connectedBody == null
				? world : joint.connectedBody.transform.InverseTransformPoint(world);
		}

		private static Vector3 ConnectedWorld(Joint joint)
		{
			return joint.connectedBody == null
				? joint.connectedAnchor : joint.connectedBody.transform.TransformPoint(joint.connectedAnchor);
		}

		private static float AnchorError(Joint joint)
		{
			return Vector3.Distance(joint.transform.TransformPoint(joint.anchor), ConnectedWorld(joint));
		}

		private static string TypeName(JointController controller)
		{
			return controller.JNKEKNOAPHO == null ? "unknown" : controller.JNKEKNOAPHO.type.ToString();
		}

		private static string V(Vector3 value) { return F(value.x) + "," + F(value.y) + "," + F(value.z); }
		private static string F(float value) { return value.ToString("R", CultureInfo.InvariantCulture); }

		private sealed class PistonEndpoints
		{
			internal ConfigurableJoint Joint;
			internal Transform Source;
			internal Vector3 Anchor;
			internal Vector3 Connected;
		}
	}
}
