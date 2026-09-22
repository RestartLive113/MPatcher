using System;
using System.Collections.Generic;
using McnCraft;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
	// V24 candidate: condition ordinary physical-root constraints without changing gameplay mass.
	internal sealed class MagnificationJointMassBalance : MonoBehaviour
	{
		private MachineController machine;
		private List<GameObject> probeRoots;
		private float probeScale;
		private int MachineId { get { return machine == null ? GetInstanceID() : machine.GetInstanceID(); } }
		private readonly List<Entry> entries = new List<Entry>();
		private bool dirty = true, failed;
		private int frame, appliedTotal, pendingLogChanges;
		private float nextLogAt;
		internal static MagnificationJointMassBalance AttachProbe(List<Rigidbody> bodies, float scale)
		{
			if (!MagnificationNativeJointProbe.IsIsolatedProcess()) throw new InvalidOperationException("not-isolated-stress-process");
			MagnificationJointMassBalance runner = bodies[0].gameObject.AddComponent<MagnificationJointMassBalance>();
			runner.probeRoots = new List<GameObject>();
			foreach (Rigidbody body in bodies) runner.probeRoots.Add(body.gameObject);
			runner.probeScale = scale;
			return runner;
		}
		internal void RequestRefresh() { dirty = true; }
		internal static void Track(MachineController machine)
		{
			if (machine == null) return;
			float scale = MagnificationPhysicsNormalization.MachineScale(machine);
			MagnificationJointMassBalance runner = machine.GetComponent<MagnificationJointMassBalance>();
			if (runner == null && !(scale > 0f && scale < 1f)) return;
			if (runner == null) runner = machine.gameObject.AddComponent<MagnificationJointMassBalance>();
			runner.machine = machine;
			runner.dirty = true;
		}
		private void FixedUpdate()
		{
			if (failed || (machine == null && probeRoots == null)) return;
			try
			{
				float scale = probeRoots == null ? MagnificationPhysicsNormalization.MachineScale(machine) : probeScale;
				if (!(scale > 0f && scale < 1f))
				{
					Restore();
					entries.Clear();
					return;
				}
				frame++;
				// Hooks invalidate on Initialize/Respawn. A low-frequency scan also finds script-created root joints.
				if (dirty || frame % 100 == 0) Refresh();
				int changed = 0;
				foreach (Entry entry in entries)
				{
					if (entry.Joint == null || entry.Own == null || entry.Other == null) { dirty = true; continue; }
					if (entry.Joint.connectedBody != entry.Other) { dirty = true; continue; }
					if (entry.Own.isKinematic || entry.Other.isKinematic) continue;
					float ownMass = entry.Own.mass, otherMass = entry.Other.mass;
					IntPtr identity = MagnificationJointMassNative.JointIdentity(entry.Joint);
					if (identity == IntPtr.Zero) continue;
					if (entry.Identity == identity && entry.OwnMass == ownMass && entry.OtherMass == otherMass) continue;
					float ratio = Mathf.Max(ownMass, otherMass) / Mathf.Min(ownMass, otherMass);
					if (ratio > 10f || entry.Modified)
					{
						entry.Modified = true; // Also roll back if a later readback rejects a partial update.
						if (!MagnificationJointMassNative.Balance(entry.Joint, 10f, true, true)) throw new InvalidOperationException("native-balance-refused");
						entry.Modified = ratio > 10f;
						changed++;
					}
					entry.Identity = identity;
					entry.OwnMass = ownMass; entry.OtherMass = otherMass;
				}
				appliedTotal += changed;
				pendingLogChanges += changed;
				if (frame == 1 || (pendingLogChanges != 0 && Time.realtimeSinceStartup >= nextLogAt))
				{
					MagnificationDownscale.Log("JOINT_MASS_BALANCE machine=" + MachineId + " scale=" + scale
						+ " tracked=" + entries.Count + " changed=" + pendingLogChanges + " total=" + appliedTotal
						+ " ratioLimit=10 mass=body-unchanged inertia=body-unchanged scope=hinge-configurable-physical-root-pairs candidate=v24");
					pendingLogChanges = 0;
					nextLogAt = Time.realtimeSinceStartup + 1f;
				}
			}
			catch (Exception error)
			{
				failed = true;
				try { Restore(); } catch (Exception restoreError) { MagnificationDownscale.Log("JOINT_MASS_RESTORE_FAILED " + restoreError.Message); }
				MagnificationDownscale.Log("JOINT_MASS_BALANCE_DISABLED machine=" + MachineId + " reason=" + error);
			}
		}
		private void Refresh()
		{
			dirty = false;
			List<GameObject> roots = probeRoots == null ? new List<GameObject> { machine.gameObject } : new List<GameObject>(probeRoots);
			if (probeRoots == null && machine.KBLANAFAJFP != null) roots.AddRange(machine.KBLANAFAJFP);
			HashSet<int> rootBodies = new HashSet<int>();
			foreach (GameObject root in roots)
			{
				if (root == null) continue;
				Rigidbody body = root.GetComponent<Rigidbody>();
				if (body != null) rootBodies.Add(body.GetInstanceID());
			}
			HashSet<int> seen = new HashSet<int>();
			Dictionary<int, Entry> previous = new Dictionary<int, Entry>();
			foreach (Entry old in entries) previous[old.Id] = old;
			HashSet<Entry> retainedEntries = new HashSet<Entry>();
			List<Entry> next = new List<Entry>();
			foreach (GameObject root in roots)
			{
				if (root == null) continue;
				foreach (Joint joint in root.GetComponents<Joint>())
				{
					if (!(joint is HingeJoint) && !(joint is ConfigurableJoint)) continue;
					int id = joint.GetInstanceID();
					if (!seen.Add(id)) continue;
					Rigidbody own = joint.GetComponent<Rigidbody>(), other = joint.connectedBody;
					if (own == null || other == null || !rootBodies.Contains(own.GetInstanceID()) || !rootBodies.Contains(other.GetInstanceID())) continue;
					Entry retained;
					if (!previous.TryGetValue(id, out retained) || retained.Joint != joint || retained.Own != own || retained.Other != other)
						retained = new Entry { Id = id, Joint = joint, Own = own, Other = other };
					next.Add(retained);
					retainedEntries.Add(retained);
				}
			}
			foreach (Entry old in entries) if (!retainedEntries.Contains(old)) RestoreEntry(old);
			entries.Clear(); entries.AddRange(next);
		}
		private void Restore()
		{
			foreach (Entry entry in entries) RestoreEntry(entry);
		}
		private static void RestoreEntry(Entry entry)
		{
			if (!entry.Modified || entry.Joint == null || entry.Joint.connectedBody == null) return;
			if (MagnificationJointMassNative.JointIdentity(entry.Joint) != IntPtr.Zero)
				MagnificationJointMassNative.Balance(entry.Joint, float.MaxValue, true, true);
			entry.Modified = false;
			entry.Identity = IntPtr.Zero;
		}
		private sealed class Entry
		{
			internal int Id;
			internal Joint Joint;
			internal Rigidbody Own, Other;
			internal IntPtr Identity;
			internal float OwnMass, OtherMass;
			internal bool Modified;
		}
	}
}
