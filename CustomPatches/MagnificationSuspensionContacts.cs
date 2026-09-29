using System.Globalization;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
	// Installed only by the isolated craft runner. Observe actual ground support;
	// do not change colliders, craft data, forces or scripts.
	internal sealed class MagnificationSuspensionContacts : MonoBehaviour
	{
		private Rigidbody body;
		private float begin, impulseY, minPoint = float.PositiveInfinity;
		private int callbacks, points;
        private readonly System.Collections.Generic.HashSet<int> surfaces = new System.Collections.Generic.HashSet<int>();
		internal void Bind(Rigidbody value) { body = value; begin = Time.fixedTime; }
		internal void OverrideSolver(int iterations)
		{
			if (body == null) return;
			body.solverIterations = iterations;
			body.solverVelocityIterations = iterations;
		}
		private void OnCollisionEnter(Collision collision) { Observe(collision); }
		private void OnCollisionStay(Collision collision) { Observe(collision); }
		private void Observe(Collision collision)
		{
			if (collision.collider == null) return;
            if (MagnificationAuxiliaryProbe.NativeGround)
            {
                if (collision.collider.attachedRigidbody != null || collision.collider.isTrigger) return;
                if (surfaces.Add(collision.collider.GetInstanceID()))
                    MagnificationDownscale.Log("CRAFT_NATIVE_SUPPORT body=" + body.name + " surface=" + collision.collider.name + " layer=" + collision.collider.gameObject.layer
                        + " type=" + collision.collider.GetType().Name + " contactOffset=" + F(collision.collider.contactOffset)
                        + " material=" + (collision.collider.sharedMaterial == null ? "default" : collision.collider.sharedMaterial.name));
            }
            else if (collision.collider.name != "MPatcher_CraftProbe_FlatGround") return;
			callbacks++; impulseY += Mathf.Abs(collision.impulse.y);
			ContactPoint[] contacts = collision.contacts; points += contacts.Length;
			foreach (ContactPoint contact in contacts) minPoint = Mathf.Min(minPoint, contact.point.y);
		}
		internal void Snapshot(int frame, float scale)
		{
			if (body == null) return;
			float duration = Time.fixedTime - begin;
			if (callbacks != 0 || frame == 1000)
			{
				float bottom = float.PositiveInfinity; int colliders = 0;
				foreach (Collider collider in body.GetComponentsInChildren<Collider>())
					if (collider.enabled && !collider.isTrigger && collider.attachedRigidbody == body)
					{ bottom = Mathf.Min(bottom, collider.bounds.min.y); colliders++; }
				MagnificationDownscale.Log("CRAFT_PROBE_SUPPORT frame=" + frame + " body=" + body.name + " id=" + body.GetInstanceID()
					+ " mass=" + F(body.mass) + " position=" + body.position.ToString("R") + " com=" + body.worldCenterOfMass.ToString("R")
					+ " bottomCraft=" + F((bottom - MagnificationCraftProbe.GroundY) / scale) + " colliders=" + colliders + " contacts=" + points + " callbacks=" + callbacks
					+ " groundForceYMean=" + F(duration > 0f ? impulseY / duration : 0f) + " groundPointMin=" + F(minPoint)
					+ " solver=" + body.solverIterations + "/" + body.solverVelocityIterations);
			}
			begin = Time.fixedTime; impulseY = 0f; callbacks = points = 0; minPoint = float.PositiveInfinity;
		}
		private static string F(float value) { return value.ToString("R", CultureInfo.InvariantCulture); }
	}
}
