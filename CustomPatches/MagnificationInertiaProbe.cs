using System;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
	internal sealed class MagnificationInertiaProbe : MonoBehaviour
	{
		private void Start()
		{
			try
			{
				GameObject obj = new GameObject("MPatcher_InertiaBoundaryProbe");
				obj.AddComponent<BoxCollider>();
				Rigidbody body = obj.AddComponent<Rigidbody>(); body.useGravity = false;
				MagnificationJointMassNative.InspectInertia(body);
				foreach (float value in new[] { 1f, 1e-5f, 1e-6f, 2e-7f, 1.1920928955078125e-7f, 1e-7f, 1e-8f, 1e-10f })
				{
					body.inertiaTensor = Vector3.one;
					body.inertiaTensor = Vector3.one * value;
					MagnificationDownscale.Log("INERTIA_THRESHOLD_PROBE requested=" + value.ToString("R") + " actual=" + body.inertiaTensor.ToString("R"));
					MagnificationJointMassNative.WriteSmallInertia(body, Vector3.one * value);
					MagnificationDownscale.Log("INERTIA_NATIVE_READBACK requested=" + value.ToString("R") + " actual=" + body.inertiaTensor.ToString("R"));
				}
				MagnificationDownscale.Log("INERTIA_NATIVE_PROBE_COMPLETE success=True");
			}
			catch (Exception ex) { MagnificationDownscale.Log("INERTIA_NATIVE_PROBE_FAILED " + ex); }
			Application.Quit();
		}
	}
}
