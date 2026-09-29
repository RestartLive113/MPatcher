using System;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
	// Models 20/21 retain the historical constant-gravity comparison.
	// Model 22 preserves geometry AND elapsed time: L=s, M=s^3, T=1.
	internal static class MagnificationScalePolicy
	{
		internal static bool SameTime { get { return MagnificationCraftProbe.Model == 22; } }
		internal static bool Enabled { get { return MagnificationCraftProbe.Model == 20 || MagnificationCraftProbe.Model == 21 || SameTime; } }
		private static float Unit { get { return Enabled ? MagnificationCraftProbe.MassUnit : 1f; } }
		internal static float Valid(float scale) { return MagnificationSimilarityUnits.Valid(scale); }
		internal static bool Bypass(float scale) { return SameTime && Valid(scale) == 1f; }
		private static float UnitAt(float scale) { return Bypass(scale) ? 1f : Unit; }
		internal static float Mass(float scale) { return UnitAt(scale) * MagnificationSimilarityUnits.Mass(scale); }
		internal static float Inertia(float scale) { return UnitAt(scale) * MagnificationSimilarityUnits.Inertia(scale); }
		internal static float Force(float scale) { return SameTime ? UnitAt(scale) * MagnificationSimilarityUnits.Force(scale) : Mass(scale); }
		internal static float Acceleration(float scale) { return SameTime ? MagnificationSimilarityUnits.Acceleration(scale) : 1f; }
		internal static float LinearSpring(float scale) { float s = Valid(scale); return SameTime ? UnitAt(s) * MagnificationSimilarityUnits.LinearSpring(s) : Unit * s * s; }
		internal static float LinearDamper(float scale) { float s = Valid(scale); return SameTime ? UnitAt(s) * MagnificationSimilarityUnits.LinearDamper(s) : Unit * s * s * (float)Math.Sqrt(s); }
		internal static float Torque(float scale) { return SameTime ? UnitAt(scale) * MagnificationSimilarityUnits.Torque(scale) : Mass(scale) * Valid(scale); }
		internal static float AngularDamper(float scale) { return SameTime ? UnitAt(scale) * MagnificationSimilarityUnits.AngularDamper(scale) : Torque(scale) * (float)Math.Sqrt(Valid(scale)); }
		internal static JointDrive LinearDrive(JointDrive value, float scale)
		{
			if (Bypass(scale)) return value;
			value.positionSpring *= LinearSpring(scale);
			value.positionDamper *= LinearDamper(scale);
			value.maximumForce *= Force(scale);
			return value;
		}
		internal static JointSpring AngularSpring(JointSpring value, float scale, bool toWorld)
		{
			if (Bypass(scale)) return value;
			float k = Torque(scale), c = AngularDamper(scale);
			value.spring = toWorld ? value.spring * k : value.spring / k;
			value.damper = toWorld ? value.damper * c : value.damper / c;
			return value;
		}
	}
}
