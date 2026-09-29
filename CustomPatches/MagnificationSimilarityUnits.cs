namespace MPatcherFork.CustomPatches
{
	// Geometric similarity at the same elapsed time: L=s, M=s^3, T=1.
	// These are unit factors, not stored craft settings or solver mass clamps.
	// Gravity/linear acceleration must also scale with s for this policy.
	internal static class MagnificationSimilarityUnits
	{
		internal static float Valid(float scale) { return scale >= .01f && scale <= 3f ? scale : 1f; }
		internal static float Mass(float scale) { float s = Valid(scale); return s * s * s; }
		internal static float Inertia(float scale) { float s = Valid(scale); return Mass(s) * s * s; }
		internal static float Force(float scale) { float s = Valid(scale); return Mass(s) * s; }
		internal static float Torque(float scale) { return Inertia(scale); }
		internal static float LinearSpring(float scale) { return Mass(scale); }
		internal static float LinearDamper(float scale) { return Mass(scale); }
		internal static float AngularSpring(float scale) { return Inertia(scale); }
		internal static float AngularDamper(float scale) { return Inertia(scale); }
		internal static float Acceleration(float scale) { return Valid(scale); }
	}
}
