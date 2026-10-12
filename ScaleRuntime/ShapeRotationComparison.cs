using MPatcherFork.CustomPatches;
using System;
using UnityEngine;

namespace MPatcherFork.ScaleRuntime
{
    // Unity's float Quaternion.Angle assumes exact unit length. A native 80deg
    // float quaternion can have Dot(q,q)=0.99999994 and appear to rotate 0.0396deg
    // against itself. Compare orientation in normalized double precision while
    // keeping the caller's strict angle threshold; never change geometric poses.
    internal static class ShapeRotationComparison
    {
        internal static float AngleDegrees(Quaternion first, Quaternion second)
        {
            double ax = first.x, ay = first.y, az = first.z, aw = first.w;
            double bx = second.x, by = second.y, bz = second.z, bw = second.w;
            double firstNorm = ax * ax + ay * ay + az * az + aw * aw;
            double secondNorm = bx * bx + by * by + bz * bz + bw * bw;
            if (firstNorm <= 0d || secondNorm <= 0d || double.IsNaN(firstNorm)
                || double.IsNaN(secondNorm) || double.IsInfinity(firstNorm) || double.IsInfinity(secondNorm))
                return float.PositiveInfinity;
            double dot = ax * bx + ay * by + az * bz + aw * bw;
            double cosine = Math.Abs(dot) / Math.Sqrt(firstNorm * secondNorm);
            if (double.IsNaN(cosine) || double.IsInfinity(cosine)) return float.PositiveInfinity;
            if (cosine >= 1d) return 0f;
            return (float)(Math.Acos(Math.Max(0d, cosine)) * (360d / Math.PI));
        }
    }
}
