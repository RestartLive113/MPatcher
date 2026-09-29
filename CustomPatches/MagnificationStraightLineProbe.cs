using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
    // Read-only per-phase measurements. The caller retains its controls, sample
    // windows and existing public-anchor residual calculation without changes.
    internal sealed class MagnificationStraightLineProbe
    {
        private sealed class Stat
        {
            internal int Count, Invalid;
            internal double Last = double.NaN;
            internal double Mean, M2, Squares, Min = double.PositiveInfinity, Max = double.NegativeInfinity, Peak;
            internal void Add(double value)
            {
                if (double.IsNaN(value) || double.IsInfinity(value)) { Invalid++; return; }
                Last = value; Count++; double delta = value - Mean; Mean += delta / Count; M2 += delta * (value - Mean);
                Squares += value * value; Min = Math.Min(Min, value); Max = Math.Max(Max, value); Peak = Math.Max(Peak, Math.Abs(value));
            }
            internal double Rms { get { return Count == 0 ? double.NaN : Math.Sqrt(Squares / Count); } }
            internal double Variance { get { return Count == 0 ? double.NaN : Math.Max(0, M2 / Count); } }
            internal double Std { get { return Math.Sqrt(Variance); } }
        }
        private sealed class Piston
        {
            internal string Part;
            internal int Id, Axis;
            internal float FirstBodyRelativeRight, Last;
            internal bool Seen;
            internal readonly Stat Residual = new Stat(), Step = new Stat();
        }
        private readonly Dictionary<int, Piston> pistons = new Dictionary<int, Piston>();
        private string craft, phase;
        private int percent, startFrame, firstSample, lastFrame;
        private float scale, lastHeadingAngle;
        private double unwrappedHeading;
        private Vector3 origin, heading, right, lastOmega;
        private bool headingValid;
        private Stat lateral, along, yawRate, yawAngle, angular, wx, wy, wz, angularStep;
        internal void Reset() { phase = null; pistons.Clear(); }
        internal void Begin(string craftName, int percentValue, string phaseName, float scaleValue, Rigidbody body, int frame)
        {
            Reset(); craft = craftName; percent = percentValue; phase = phaseName; scale = scaleValue;
            startFrame = lastFrame = frame; firstSample = -1; origin = body.position;
            heading = body.rotation * Vector3.forward; heading.y = 0f;
            headingValid = heading.sqrMagnitude > 1e-12f;
            heading = headingValid ? heading.normalized : Vector3.zero;
            right = Vector3.Cross(Vector3.up, heading); lastHeadingAngle = 0f; unwrappedHeading = 0;
            lastOmega = body.angularVelocity;
            lateral = new Stat(); along = new Stat(); yawRate = new Stat(); yawAngle = new Stat();
            angular = new Stat(); wx = new Stat(); wy = new Stat(); wz = new Stat(); angularStep = new Stat();
            Log("STRAIGHT_PROBE_PHASE " + Label() + " frame=" + frame + " origin=" + V(origin)
                + " headingWorld=" + V(heading) + " rightWorld=" + V(right) + " headingValid=" + headingValid
                + " reference=phase-start-horizontal-root-body-forward inputPhases=unchanged samples=existing-frame-over500");
        }
        internal void Observe(Rigidbody body, int frame, bool sample)
        {
            if (phase == null) return;
            lastFrame = frame;
            Vector3 forward = body.rotation * Vector3.forward; forward.y = 0f;
            bool validNow = headingValid && forward.sqrMagnitude > 1e-12f;
            if (validNow)
            {
                forward.Normalize();
                float angle = Mathf.Atan2(Vector3.Dot(forward, right), Vector3.Dot(forward, heading)) * Mathf.Rad2Deg;
                unwrappedHeading += Mathf.DeltaAngle(lastHeadingAngle, angle); lastHeadingAngle = angle;
            }
            Vector3 omega = body.angularVelocity;
            float omegaDelta = (omega - lastOmega).magnitude; lastOmega = omega;
            if (!sample) return;
            if (firstSample < 0) firstSample = frame;
            if (headingValid)
            {
                Vector3 displacement = (body.position - origin) / scale;
                lateral.Add(Vector3.Dot(displacement, right)); along.Add(Vector3.Dot(displacement, heading));
            }
            if (validNow) yawAngle.Add(unwrappedHeading);
            yawRate.Add(omega.y); angular.Add(omega.magnitude);
            wx.Add(omega.x); wy.Add(omega.y); wz.Add(omega.z); angularStep.Add(omegaDelta);
        }
        internal void ObservePiston(PistonController piston, Rigidbody root, int axis, float signedResidual)
        {
            if (phase == null) return;
            int id = piston.GetInstanceID(); Piston entry;
            if (!pistons.TryGetValue(id, out entry))
                pistons[id] = entry = new Piston { Id = id, Part = piston.JNKEKNOAPHO.GetPosString(true), Axis = axis,
                    FirstBodyRelativeRight = headingValid ? Vector3.Dot(piston.transform.position - root.position, right) / scale : float.NaN };
            entry.Residual.Add(signedResidual);
            if (entry.Seen) entry.Step.Add(signedResidual - entry.Last);
            entry.Last = signedResidual; entry.Seen = true;
        }
        internal void Flush()
        {
            if (phase == null) return;
            Log("STRAIGHT_PROBE_RESULT " + Label() + " startFrame=" + startFrame + " firstSample=" + firstSample + " lastFrame=" + lastFrame
                + " samples=" + angular.Count + " headingValid=" + headingValid + " headingWorld=" + V(heading)
                + " lateralSamples=" + lateral.Count + " lateralEndCraft=" + F(lateral.Last) + " forwardEndCraft=" + F(along.Last)
                + " lateralMeanCraft=" + F(lateral.Mean) + " lateralRmsCraft=" + F(lateral.Rms) + " lateralStdCraft=" + F(lateral.Std)
                + " lateralPeakCraft=" + F(lateral.Peak) + " lateralMinCraft=" + F(lateral.Min) + " lateralMaxCraft=" + F(lateral.Max)
                + " forwardMeanCraft=" + F(along.Mean) + " forwardMinCraft=" + F(along.Min) + " forwardMaxCraft=" + F(along.Max)
                + " yawRateMean=" + F(yawRate.Mean) + " yawRateRms=" + F(yawRate.Rms) + " yawRateStd=" + F(yawRate.Std)
                + " yawAngleEndDegrees=" + F(yawAngle.Last) + " yawAngleRmsDegrees=" + F(yawAngle.Rms) + " yawAngleStdDegrees=" + F(yawAngle.Std) + " yawAnglePeakDegrees=" + F(yawAngle.Peak)
                + " angularRms=" + F(angular.Rms) + " angularStd=" + F(angular.Std)
                + " angularVectorStd=" + F(Math.Sqrt(wx.Variance + wy.Variance + wz.Variance)) + " angularStepDeltaRms=" + F(angularStep.Rms)
                + " headingSamples=" + yawAngle.Count + " invalidSamples=" + (angular.Invalid + lateral.Invalid + yawRate.Invalid)
                + " angularUnits=rad-per-second headingUnits=degrees-unwrapped sampleCadence=existing-probe");
            List<Piston> ordered = new List<Piston>(pistons.Values);
            ordered.Sort(delegate(Piston a, Piston b) { int key = string.CompareOrdinal(a.Part, b.Part); return key != 0 ? key : a.Id.CompareTo(b.Id); });
            foreach (Piston entry in ordered)
            {
                Stat value = entry.Residual;
                Log("STRAIGHT_PROBE_PISTON " + Label() + " part=" + entry.Part + " id=" + entry.Id + " axis=" + entry.Axis
                    + " firstBodyRelativeRightCraft=" + F(entry.FirstBodyRelativeRight) + " samples=" + value.Count
                    + " residualMeanCraft=" + F(value.Mean) + " residualRmsCraft=" + F(value.Rms) + " residualStdCraft=" + F(value.Std)
                    + " residualPeakAbsCraft=" + F(value.Peak) + " residualMinCraft=" + F(value.Min) + " residualMaxCraft=" + F(value.Max)
                    + " residualRangeCraft=" + F(value.Max - value.Min) + " residualStepDeltaRmsCraft=" + F(entry.Step.Rms)
                    + " invalidSamples=" + value.Invalid + " meaning=residual-axis-not-literal-suspension-sag anchors=existing-public-calculation");
            }
            phase = null;
        }
        private string Label() { return "craft=" + craft + " percent=" + percent + " phase=" + phase + " scale=" + F(scale); }
        private static string F(double value) { return value.ToString("R", CultureInfo.InvariantCulture); }
        private static string V(Vector3 value) { return F(value.x) + "," + F(value.y) + "," + F(value.z); }
        private static void Log(string message) { MagnificationDownscale.Log(message); }
    }
}
