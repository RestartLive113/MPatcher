using System.Collections.Generic;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
	// Read the current global gravity after ordinary scripts (including JZZ).
	// Never change Physics.gravity: each craft can have a different size.
	[DefaultExecutionOrder(32000)]
	internal sealed class MagnificationGravitySimilarity : MonoBehaviour
	{
		private sealed class Entry
		{
			internal Rigidbody Body;
			internal float Scale;
			internal bool Observed, UseGravity;
			internal Vector3 Gravity;
		}
		private static readonly Dictionary<int, Entry> entries = new Dictionary<int, Entry>();
		private static readonly List<int> expired = new List<int>();
		private static MagnificationGravitySimilarity runner;
		internal static void Track(Rigidbody body, float scale)
		{
			if (!MagnificationScalePolicy.SameTime || body == null) return;
			if (MagnificationMassBoundary.IsVanilla(body)) { entries.Remove(body.GetInstanceID()); return; }
			if (MagnificationScalePolicy.Bypass(scale)) { entries.Remove(body.GetInstanceID()); return; }
			int id = body.GetInstanceID(); Entry entry;
			if (!entries.TryGetValue(id, out entry) || !ReferenceEquals(entry.Body, body))
				entries[id] = entry = new Entry { Body = body };
			entry.Scale = MagnificationScalePolicy.Valid(scale);
			if (runner == null)
			{
				GameObject host = new GameObject("MPatcher_GravitySimilarity");
				DontDestroyOnLoad(host);
				runner = host.AddComponent<MagnificationGravitySimilarity>();
				MagnificationDownscale.Log("GRAVITY_SIMILARITY_REGISTERED model=22 order=32000 globalGravity=unchanged nativeAcceleration=separate-source-boundary");
			}
		}
		private void FixedUpdate()
		{
			if (!MagnificationScalePolicy.SameTime) return;
			Vector3 gravity = Physics.gravity;
			expired.Clear();
			foreach (KeyValuePair<int, Entry> pair in entries)
			{
				Entry entry = pair.Value; Rigidbody body = entry.Body;
				if (body == null || MagnificationMassBoundary.IsVanilla(body)) { expired.Add(pair.Key); continue; }
				bool useGravity = body.useGravity;
				if (!entry.Observed || entry.UseGravity != useGravity || entry.Gravity != gravity)
				{
					entry.Observed = true; entry.UseGravity = useGravity; entry.Gravity = gravity;
					MagnificationDownscale.Log("GRAVITY_SIMILARITY_BODY body=" + body.name + " scale=" + entry.Scale
						+ " useGravity=" + useGravity + " global=" + gravity.ToString("R")
						+ " effective=" + (useGravity ? gravity * entry.Scale : Vector3.zero).ToString("R"));
				}
				if (!useGravity || body.isKinematic || body.IsSleeping()) continue;
				// Unity supplies g; add only its missing correction, (s-1)*g.
				MagnificationJointMassNative.AddAccelerationWithoutWake(body, gravity * (entry.Scale - 1f));
			}
			foreach (int id in expired) entries.Remove(id);
		}
		internal static void Clear() { entries.Clear(); expired.Clear(); }
	}
}
