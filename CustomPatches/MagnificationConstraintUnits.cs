using System.Collections.Generic;
using McnCraft;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
	internal static class MagnificationConstraintUnits
	{
		private sealed class Entry { internal Component Target; internal float Original; }
		private static readonly Dictionary<int, Entry> entries = new Dictionary<int, Entry>();
		private sealed class BodyDefaults { internal Rigidbody Target; internal float Sleep, Depenetration; }
		private static readonly Dictionary<int, BodyDefaults> bodies = new Dictionary<int, BodyDefaults>();
		internal static void ApplyBody(Rigidbody body, float scale)
		{
			if (!MagnificationScalePolicy.Enabled || MagnificationScalePolicy.Bypass(scale)) return;
			BodyDefaults source;
			int id = body.GetInstanceID();
			if (!bodies.TryGetValue(id, out source) || !ReferenceEquals(source.Target, body))
				bodies[id] = source = new BodyDefaults { Target = body, Sleep = body.sleepThreshold, Depenetration = body.maxDepenetrationVelocity };
			// Sleep is kinetic energy per mass. v scales with s and I/m with s²;
			// both translation and constant-RPM rotation therefore use s² here.
			float s = MagnificationScalePolicy.Valid(scale);
			body.sleepThreshold = source.Sleep * s * s;
			body.maxDepenetrationVelocity = source.Depenetration * s;
		}
		internal static void Apply(MachineController machine)
		{
			if (!MagnificationScalePolicy.Enabled) return;
			float scale = MagnificationPhysicsNormalization.MachineScale(machine);
			if (MagnificationScalePolicy.Bypass(scale)) return;
			HashSet<int> seen = new HashSet<int>();
			List<GameObject> roots = new List<GameObject> { machine.gameObject }; roots.AddRange(machine.KBLANAFAJFP);
			int contacts = 0, projections = 0;
			foreach (GameObject root in roots) if (root != null)
			{
				foreach (Collider collider in root.GetComponentsInChildren<Collider>())
					if (seen.Add(collider.GetInstanceID())) { collider.contactOffset = Source(collider, collider.contactOffset) * scale; contacts++; }
				foreach (ConfigurableJoint joint in root.GetComponentsInChildren<ConfigurableJoint>())
					if (seen.Add(joint.GetInstanceID())) { joint.projectionDistance = Source(joint, joint.projectionDistance) * scale; projections++; }
			}
			MagnificationDownscale.Log("CONSTRAINT_UNITS contacts=" + contacts + " projections=" + projections + " scale=" + scale + " angularLimits=unchanged repeatedApply=source-cache");
		}
		        internal static void ApplyHierarchy(GameObject root, float scale, string phase)
        {
            if (!MagnificationScalePolicy.SameTime || MagnificationScalePolicy.Bypass(scale) || root == null) return;
            int contacts = 0, projections = 0;
            foreach (Collider collider in root.GetComponentsInChildren<Collider>())
            { collider.contactOffset = Source(collider, collider.contactOffset) * scale; contacts++; }
            foreach (ConfigurableJoint joint in root.GetComponentsInChildren<ConfigurableJoint>())
            { joint.projectionDistance = Source(joint, joint.projectionDistance) * scale; projections++; }
            MagnificationDownscale.Log("LATE_CONSTRAINT_UNITS phase=" + phase + " contacts=" + contacts + " projections=" + projections + " scale=" + scale + " repeatedApply=source-cache");
        }
        private static float Source(Component target, float current)
		{
			int id = target.GetInstanceID(); Entry entry;
			if (!entries.TryGetValue(id, out entry) || !ReferenceEquals(entry.Target, target)) entries[id] = entry = new Entry { Target = target, Original = current };
			return entry.Original;
		}
		internal static void Clear() { entries.Clear(); bodies.Clear(); }
	}
}
