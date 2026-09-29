using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using McnCraft;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
    // Explicit Car diagnostic only. Controllers and piston drives remain active.
    internal static class MagnificationRotorIsolationProbe
    {
        private static bool requested, armed;
        private static MachineController machine;
        private static readonly Dictionary<int, HingeJoint> hinges = new Dictionary<int, HingeJoint>();
        private static readonly HashSet<int> loggedTorqueOwners = new HashSet<int>();
        private static int nativeTorqueCalls, nonzeroTorqueCalls, nativeSpringWrites, scriptSpringWrites, scriptMotorWrites;
        internal static void Configure(string root, string craft, ref int[] scales, ref int[] models)
        {
            string[] args = Environment.GetCommandLineArgs();
            requested = Array.IndexOf(args, "--mpatcher-craft-rotor-passive") >= 0;
            Reset();
            if (!requested) return;
            string expected = Path.GetFullPath(Path.Combine(Environment.GetEnvironmentVariable("USERPROFILE") ?? "", "Downloads\\MachineCraftMPatcherTest"));
            if (!string.Equals(Path.GetFullPath(root).TrimEnd('\\', '/'), expected.TrimEnd('\\', '/'), StringComparison.OrdinalIgnoreCase)
                || Array.IndexOf(args, "--mpatcher-magnification-craft-probe") < 0) throw new InvalidOperationException("rotor-isolation-requires-isolated-craft-probe");
            if (craft != "Car") throw new InvalidOperationException("rotor-isolation-current-scope-Car-only");
            if (Array.IndexOf(args, "--mpatcher-craft-passive") >= 0 || Array.IndexOf(args, "--mpatcher-craft-passive-comparison") >= 0 || Array.IndexOf(args, "--mpatcher-craft-native-control") >= 0)
                throw new InvalidOperationException("rotor-isolation-exclusive-with-passive-and-native-control");
            if (Array.IndexOf(args, "--mpatcher-craft-similarity") >= 0) { scales = new[] { 100, 5 }; models = new[] { 22, 22 }; }
            if (models.Length == 0 || models.Length != scales.Length) throw new InvalidOperationException("rotor-isolation-empty-or-mismatched-plan");
            foreach (int model in models) if (model != 22) throw new InvalidOperationException("rotor-isolation-requires-model22");
            Log("ROTOR_ISOLATION_CONFIGURED scope=Car-only model=22 runs=" + models.Length
                + " controllers=running pistonDrives=untouched scriptMass=untouched targetsAndFlags=preserved candidatePhysics=diagnostic-override");
        }
        internal static void Reset()
        {
            armed = false; machine = null; hinges.Clear(); loggedTorqueOwners.Clear();
            nativeTorqueCalls = nonzeroTorqueCalls = nativeSpringWrites = scriptSpringWrites = scriptMotorWrites = 0;
        }
        internal static void Arm(MachineController owner)
        {
            if (!requested) return;
            if (MagnificationCraftProbe.Model != 22 || owner == null) throw new InvalidOperationException("rotor-isolation-invalid-arm");
            Reset(); machine = owner;
            List<GameObject> roots = new List<GameObject> { owner.gameObject }; roots.AddRange(owner.KBLANAFAJFP);
            Dictionary<int, JointController> parts = new Dictionary<int, JointController>();
            foreach (GameObject root in roots) if (root != null)
                foreach (JointController part in root.GetComponentsInChildren<JointController>(true))
                    if (part.FPEBEEJGFPI == owner && part.ALGHGLCBLDJ != null) parts[part.ALGHGLCBLDJ.GetInstanceID()] = part;
            foreach (GameObject root in roots) if (root != null)
                foreach (HingeJoint joint in root.GetComponentsInChildren<HingeJoint>(true))
                    if (!hinges.ContainsKey(joint.GetInstanceID())) hinges.Add(joint.GetInstanceID(), joint);
            int nonzeroSprings = 0, activeSprings = 0, nonzeroMotors = 0, activeMotors = 0;
            foreach (HingeJoint joint in hinges.Values)
            {
                JointController part; parts.TryGetValue(joint.GetInstanceID(), out part);
                JointSpring spring = joint.spring; JointMotor motor = joint.motor;
                float originalK = spring.spring, originalC = spring.damper, originalForce = motor.force;
                bool springOn = joint.useSpring, motorOn = joint.useMotor;
                if (originalK != 0f || originalC != 0f) { nonzeroSprings++; if (springOn) activeSprings++; }
                if (originalForce != 0f) { nonzeroMotors++; if (motorOn) activeMotors++; }
                spring.spring = spring.damper = 0f; motor.force = 0f;
                joint.spring = spring; joint.motor = motor;
                Log("ROTOR_ISOLATION_INITIAL hinge=" + joint.GetInstanceID() + " body=" + joint.name
                    + " part=" + (part == null ? "unknown" : part.JNKEKNOAPHO.GetPosString(true))
                    + " partType=" + (part == null ? "unknown" : part.JNKEKNOAPHO.type.ToString())
                    + " originalK=" + F(originalK) + " originalC=" + F(originalC) + " originalMotorForce=" + F(originalForce)
                    + " targetAngle=" + F(spring.targetPosition) + " targetRPM=" + F(motor.targetVelocity / 6f)
                    + " useSpringBefore=" + springOn + " useSpringAfter=" + joint.useSpring
                    + " useMotorBefore=" + motorOn + " useMotorAfter=" + joint.useMotor + " freeSpin=" + motor.freeSpin);
            }
            armed = true;
            Log("ROTOR_ISOLATION_ARMED scale=" + F(owner.DJCDFLDHPHK) + " hinges=" + hinges.Count
                + " initialNonzeroSprings=" + nonzeroSprings + " initialActiveNonzeroSprings=" + activeSprings
                + " initialNonzeroMotorForces=" + nonzeroMotors + " initialActiveNonzeroMotors=" + activeMotors
                + " lifecycle=after-native-Start-and-READY springK=0 springC=0 motorForce=0 nativeWheelTorque=suppressed"
                + " pistonConfigurable=untouched controllers=running noPoseOrVelocityWrites=True scope=current-machine-initial-hinges");
        }
        private static bool Scope(HingeJoint joint)
        {
            if (!requested || !armed || machine == null || MagnificationCraftProbe.Model != 22 || joint == null) return false;
            HingeJoint selected;
            return hinges.TryGetValue(joint.GetInstanceID(), out selected) && ReferenceEquals(selected, joint);
        }
        internal static bool SuppressTorque(JointController owner, Vector3 value, ForceMode mode)
        {
            if (!requested || !armed || owner == null || owner.FPEBEEJGFPI != machine || !Scope(owner.ALGHGLCBLDJ)) return false;
            nativeTorqueCalls++;
            if (value.sqrMagnitude > 0f)
            {
                nonzeroTorqueCalls++;
                if (loggedTorqueOwners.Add(owner.GetInstanceID())) Log("ROTOR_ISOLATION_TORQUE_SUPPRESSED part=" + owner.JNKEKNOAPHO.GetPosString(true)
                    + " mode=" + mode + " requestedVector=" + value.ToString("R") + " appliedVector=0,0,0");
            }
            return true;
        }
        internal static JointSpring FilterSpring(HingeJoint joint, JointSpring value, bool script)
        {
            if (!Scope(joint)) return value;
            if (script) scriptSpringWrites++; else nativeSpringWrites++;
            value.spring = value.damper = 0f; return value;
        }
        internal static JointMotor FilterMotor(HingeJoint joint, JointMotor value)
        {
            if (!Scope(joint)) return value;
            scriptMotorWrites++; value.force = 0f; return value;
        }
        internal static void Snapshot(string phase, int frame)
        {
            if (!requested || !armed || machine == null) return;
            int live = 0, escaped = 0;
            foreach (HingeJoint joint in hinges.Values) if (joint != null)
            {
                live++; JointSpring spring = joint.spring; JointMotor motor = joint.motor;
                if (spring.spring != 0f || spring.damper != 0f || motor.force != 0f)
                {
                    escaped++; Log("ROTOR_ISOLATION_ESCAPE hinge=" + joint.GetInstanceID() + " frame=" + frame
                        + " K=" + F(spring.spring) + " C=" + F(spring.damper) + " motorForce=" + F(motor.force));
                }
            }
            Log("ROTOR_ISOLATION_RESULT phase=" + phase + " frame=" + frame + " scale=" + F(machine.DJCDFLDHPHK)
                + " liveHinges=" + live + " nonzeroDriveHinges=" + escaped + " nativeTorqueCallsSuppressed=" + nativeTorqueCalls
                + " nonzeroNativeTorqueCallsSuppressed=" + nonzeroTorqueCalls + " nativeSpringWritesFiltered=" + nativeSpringWrites
                + " scriptSpringWritesFiltered=" + scriptSpringWrites + " scriptMotorWritesFiltered=" + scriptMotorWrites
                + " counts=cumulative-this-run coverage=listed-boundaries-only acceptance=not-assigned");
        }
        private static string F(float value) { return value.ToString("R", CultureInfo.InvariantCulture); }
        private static void Log(string message) { MagnificationDownscale.Log(message); }
    }
}
