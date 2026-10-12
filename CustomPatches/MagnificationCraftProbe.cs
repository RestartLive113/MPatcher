using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using McnCraft;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
	// Native offline Practice, native Car and native control processing. Explicit
	// test-root flag only; input is deterministic and native UserData saves are blocked.
	internal sealed class MagnificationCraftProbe : MonoBehaviour
	{
		private static MagnificationCraftProbe active;
		private static readonly List<string> methodNames = new List<string>();
		private static readonly List<int> methodCounts = new List<int>();
		private static readonly FieldInfo PistonJoint = AccessTools.Field(typeof(PistonController), "PDNNFOJPIDM");
		private static readonly FieldInfo WheelJoint = AccessTools.Field(typeof(WheelController), "ALGHGLCBLDJ");
		private static readonly FieldInfo HingeAngle = AccessTools.Field(typeof(JointController), "OPKPNLOMBDI"), HingeTarget = AccessTools.Field(typeof(JointController), "GEKMCAJOPNK");
		private Harmony trace;
		private MachineController machine;
		private readonly MagnificationStraightLineProbe straightLine = new MagnificationStraightLineProbe();
		private readonly List<PistonController> pistons = new List<PistonController>();
		private readonly List<JointController> hinges = new List<JointController>();
		private readonly List<WheelController> wheels = new List<WheelController>();
		private string sourceBuild, sourceAssign, originalSystem, craft = "Car", folder = "_samples";
		private int percent, run, frame, phaseIndex = -1, phaseSamples;
		private bool ready, done, failed, nativeControl, sourceStart;
        private bool firstLoad = true;
		internal static bool NativeControl { get { return active != null && active.nativeControl; } }
		internal static int RunIndex { get { return active == null ? -1 : active.run; } }
		private float groundY;
		internal static float GroundY { get { return active == null ? 0f : active.groundY; } }
		private float phaseSpeed, phaseYaw, phaseVertical, phasePiston, phasePeak, phaseAngular, phasePitch;
		private double pistonMean, pistonM2;
		private float deadline;
		private float fixedStep, originalFixedStep;
		private double elapsed;
		private float massUnit = 1f;
		private int solverIterations;
		private readonly List<MagnificationSuspensionContacts> contactProbes = new List<MagnificationSuspensionContacts>();
		private int[] scales = { 100, 5, 5, 5, 5 };
		private int[] models = { 0, 1, 4, 7, 8 };
		internal static int Model { get { return active == null ? (MagnificationGravityNativeProbe.Running || MagnificationResearchBootstrap.Manual ? 22 : 0) : active.models[Math.Min(active.run, active.models.Length - 1)]; } }
		internal static float MassUnit { get { return active == null ? MagnificationResearchBootstrap.ManualMassUnit : MagnificationAuxiliaryProbe.MassUnit(active.run, active.massUnit); } }
		private IEnumerator Start()
		{
			DontDestroyOnLoad(gameObject);
			active = this;
			deadline = Time.realtimeSinceStartup + 900f;
			while (Time.realtimeSinceStartup < 60f) yield return null;
			try { Setup(); if (MagnificationJointUnitProbe.Requested && MagnificationJointUnitProbe.Resume) { run = 2; Log("JOINT_UNIT_RESUME firstRun=2 remaining=2 baselineLogs=R65b"); } LoadRun(); }
			catch (Exception error) { Fail(error); }
		}
		private void Setup()
		{
			int selectedPercent = 0;
			nativeControl = Array.IndexOf(Environment.GetCommandLineArgs(), "--mpatcher-craft-native-control") >= 0;
			sourceStart = Array.IndexOf(Environment.GetCommandLineArgs(), "--mpatcher-craft-source-start") >= 0;
			string root = Path.GetFullPath(Path.Combine(Application.dataPath, ".."));
			string expected = Path.Combine(Environment.GetEnvironmentVariable("USERPROFILE") ?? "", "Downloads\\MachineCraftMPatcherTest");
			if (!MagnificationInuProbe.IsTestRoot(root)) throw new InvalidOperationException("not-test-root");
			foreach (string arg in Environment.GetCommandLineArgs())
			{
				if (arg.StartsWith("--mpatcher-craft-solver="))
				{
					solverIterations = int.Parse(arg.Substring("--mpatcher-craft-solver=".Length), CultureInfo.InvariantCulture);
					if (solverIterations < 1 || solverIterations > 255) throw new ArgumentException("invalid-probe-solver");
				}
				if (arg.StartsWith("--mpatcher-craft-mass-unit="))
				{
					massUnit = float.Parse(arg.Substring("--mpatcher-craft-mass-unit=".Length), CultureInfo.InvariantCulture);
					if (!(massUnit >= 1f && massUnit <= 1000000f)) throw new ArgumentException("invalid-probe-mass-unit");
				}
				if (arg.StartsWith("--mpatcher-craft-fixed-step="))
				{
					fixedStep = float.Parse(arg.Substring("--mpatcher-craft-fixed-step=".Length), CultureInfo.InvariantCulture);
					if (!(fixedStep >= .001f && fixedStep <= .01f)) throw new ArgumentException("invalid-probe-fixed-step");
				}
				if (arg.StartsWith("--mpatcher-craft-percent="))
				{
					selectedPercent = int.Parse(arg.Substring("--mpatcher-craft-percent=".Length), CultureInfo.InvariantCulture);
					if (selectedPercent < 1 || selectedPercent > 300) throw new ArgumentException("invalid-probe-percent");
				}
				if (arg.StartsWith("--mpatcher-craft="))
				{
					craft = arg.Substring("--mpatcher-craft=".Length);
					if (craft == "AyraSled") craft = "Ayra\u00b3        ";
					if (craft.Trim() != "Ayra\u00b3" && craft != "Car" && craft != "JZZ30_Special2" && craft != "Q_7101" && craft != "Test_DummyAll" && craft != "Test_DummyEnds" && craft != "Inu-Sangue") throw new ArgumentException("unknown-probe-craft");
					folder = craft == "Inu-Sangue" ? "_gifts" : craft == "Car" ? "_samples" : craft.Trim() == "Ayra\u00b3" ? "Catoniger" : "";
				}
				if (arg == "--mpatcher-craft-short") { scales = new[] { 100, 5, 5 }; models = new[] { 0, 1, 7 }; }
				if (arg == "--mpatcher-craft-linear") { scales = new[] { 100, 5, 5, 5 }; models = new[] { 0, 1, 7, 9 }; }
				if (arg == "--mpatcher-craft-conditioning") { scales = new[] { 100, 5, 5, 5 }; models = new[] { 0, 1, 7, 10 }; }
				if (arg == "--mpatcher-craft-wheel-cap") { scales = new[] { 100, 5, 5 }; models = new[] { 0, 7, 11 }; }
				if (arg == "--mpatcher-craft-length-units") { scales = new[] { 100, 5, 5 }; models = new[] { 0, 11, 12 }; }
				if (arg == "--mpatcher-craft-contact") { scales = new[] { 100, 5, 5, 5 }; models = new[] { 0, 11, 12, 13 }; }
				if (arg == "--mpatcher-craft-rotors") { scales = new[] { 100, 5, 5, 5, 5 }; models = new[] { 0, 13, 14, 15, 16 }; }
				if (arg == "--mpatcher-craft-sizedrive") { scales = new[] { 100, 5, 10, 50, 200 }; models = new[] { 0, 17, 17, 17, 17 }; }
				if (arg == "--mpatcher-craft-native-joints") { scales = new[] { 100, 5, 5 }; models = new[] { 0, 17, 18 }; }
				if (arg == "--mpatcher-craft-similarity") { scales = new[] { 100, 100, 5, 200 }; models = new[] { 0, 22, 22, 22 }; }
				if (arg == "--mpatcher-craft-density") { scales = new[] { 100, 5, 5 }; models = new[] { 0, 20, 21 }; }
				if (arg.StartsWith("--mpatcher-craft-model="))
				{
					int selected = int.Parse(arg.Substring("--mpatcher-craft-model=".Length), CultureInfo.InvariantCulture);
					if (selected != 0 && selected != 1 && selected != 7 && selected != 9 && selected != 10 && selected != 11 && selected != 12 && selected != 13 && selected != 14 && selected != 15 && selected != 16 && selected != 17 && selected != 18 && selected != 19 && selected != 20 && selected != 21 && selected != 22) throw new ArgumentException("unknown-model");
					models = new[] { selected }; scales = new[] { selected == 0 ? 100 : 5 };
				}
			}
			MagnificationRotorIsolationProbe.Configure(root, craft, ref scales, ref models);
			if (selectedPercent != 0)
			{
				if (scales.Length != 1) throw new ArgumentException("percent-override-requires-single-model");
				scales[0] = selectedPercent;
			}
			if (massUnit != 1f && (models.Length != 1 || models[0] != 20)) throw new ArgumentException("mass-unit-comparison-requires-density-model20");
			MagnificationCraftPassiveProbe.Configure(craft, nativeControl, massUnit, ref scales, ref models);
			MagnificationD6LimitProbe.Configure(craft, nativeControl, massUnit, ref scales, ref models);
			MagnificationAuxiliaryProbe.Configure(craft, nativeControl, ref scales, ref models);
            MagnificationInuProbe.Configure(craft, nativeControl, ref scales, ref models, ref massUnit);
			string path = Path.Combine(Path.Combine(root, "UserData"), folder);
			sourceBuild = File.ReadAllText(Path.Combine(path, craft + ".mcbd"));
			sourceAssign = File.ReadAllText(Path.Combine(path, craft + ".mcad"));
			originalSystem = LNGKNOGOIKL.AOPMIBBFLKH(JKGKJLLFMLE.IGOBPLOLHEP);
			trace = new Harmony("local.mpatcher.isolated.craft-probe");
			if (nativeControl)
			{
				if ((!MagnificationAuxiliaryProbe.StraightComparison && scales.Length != 1) || Array.Exists(scales, delegate(int v) { return v != 100; }) || Array.Exists(models, delegate(int v) { return v != 0; }) || massUnit != 1f || fixedStep != 0f || solverIterations != 0)
					throw new InvalidOperationException("native-control-requires-model0-100-native-step-solver");
				if (craft != "Car") MagnificationInertiaBoundary.RegisterCompiler(trace);
				Log("CRAFT_NATIVE_CONTROL physicsHooks=disabled scripting=input-only observersAndFlatGround=identical");
			}
			else MagnificationCraftModelProbe.Register(trace);
			MagnificationSafetyObserver.Register(trace);
            MagnificationInuProbe.Register(trace);
			trace.Patch(AccessTools.Method(typeof(JKGKJLLFMLE), "GPNHJDMLHOK"),
				new HarmonyMethod(AccessTools.Method(typeof(MagnificationCraftProbe), "BlockSave")), null, null, null);
			foreach (MethodInfo method in typeof(HOCGCCAIPFF).GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
				if (method.Name == "AFLJECMLJDL" || method.Name == "FGCCNKAIKAI" || (method.Name == "OMAOJLCCNNK" && method.GetParameters().Length == 2))
					trace.Patch(method, null, null, new HarmonyMethod(AccessTools.Method(typeof(MagnificationCraftProbe), "InputTranspiler")), null);
			trace.Patch(AccessTools.Method(typeof(MachineController), "Initialize"),
				new HarmonyMethod(AccessTools.Method(typeof(MagnificationCraftProbe), "InitializePrefix")),
				new HarmonyMethod(AccessTools.Method(typeof(MagnificationCraftProbe), "InitializePostfix")), null, null);
			// Only known void-return lifecycle/control methods: patching obfuscator
			// decoys with struct returns can crash old Mono inside Harmony SizeOf.
			foreach (Type type in new[] { typeof(MachineController), typeof(BodyController), typeof(JointController), typeof(PistonController),
				typeof(WheelController), typeof(ShaftController), typeof(SledController), typeof(ThrusterController), typeof(AGDeviceController), typeof(ContactSensor) })
				foreach (MethodInfo method in type.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly))
				{
					if (method.GetMethodBody() == null || method.IsGenericMethod || method.ReturnType != typeof(void)) continue;
					if (method.Name != "Start" && method.Name != "FixedUpdate" && method.Name != "Update" && method.Name != "LateUpdate"
						&& method.Name != "SetJoint" && method.Name != "ResetSpring" && method.Name != "SetSpring" && method.Name != "SetDamper") continue;
					Log("CRAFT_PROBE_HOOK method=" + type.Name + "." + method.Name);
					trace.Patch(method, null, null, new HarmonyMethod(AccessTools.Method(typeof(MagnificationCraftProbe), "CoverageTranspiler")), null);
				}
			Application.runInBackground = true;
			Log("CRAFT_PROBE_START protocol=2 craft=" + folder + "/" + craft + " runs=" + scales.Length + " massUnit=" + F(massUnit)
				+ " massUnitScope=isolated-static-ground-comparison input=native-key-queries+compiled-script-calls save=blocked coverageMethods=" + methodNames.Count);
		}
		private static bool BlockSave() { Log("CRAFT_PROBE_SAVE_BLOCKED"); return false; }
		private static void InitializePrefix(string __0, BuildData __1)
		{
			if (active != null && __0 == "self") __1.magnification = active.percent;
		}
		private static void InitializePostfix(MachineController __instance, string __0)
		{
			if (active == null || __0 != "self") return;
			active.machine = __instance;
			if (active.nativeControl) MagnificationRuntimeDiagnostics.Attach(__instance);
			active.ready = false;
			active.frame = 0;
			active.elapsed = 0f;
			active.phaseIndex = -1;
			Log("CRAFT_PROBE_CREATED run=" + active.run + " percent=" + active.percent + " scale=" + F(__instance.DJCDFLDHPHK));
		}
		private void LoadRun()
		{
			MagnificationCraftPassiveProbe.Reset();
			MagnificationD6LimitProbe.Reset();
			MagnificationInertiaBoundary.Clear();
			MagnificationGravitySimilarity.Clear();
			MagnificationManagedBridge.ResetCounters();
			MagnificationContactProbe.Reset();
			MagnificationRotorProbe.Clear();
			MagnificationRotorIsolationProbe.Reset();
			straightLine.Reset();
			percent = scales[run];
            MagnificationInuProbe.EnergySettings();
			ready = false; machine = null; pistons.Clear(); hinges.Clear(); wheels.Clear(); contactProbes.Clear(); frame = 0;
			elapsed = 0f;
			JKGKJLLFMLE.IGOBPLOLHEP.folderName = folder;
			JKGKJLLFMLE.IGOBPLOLHEP.machineName = craft;
			JKGKJLLFMLE.IGOBPLOLHEP.practiceWorldType = JKGKJLLFMLE.CDIAGJLJCJC.Field;
			JKGKJLLFMLE.IGOBPLOLHEP.practiceAreaMode = JKGKJLLFMLE.BJIMLKIAEHD.Free;
			JKGKJLLFMLE.HHGILAIOCLG = LNGKNOGOIKL.FMAGAEMFION<BuildData>(sourceBuild);
			JKGKJLLFMLE.HHGILAIOCLG.CorrectVersion();
			JKGKJLLFMLE.HHGILAIOCLG.magnification = percent;
			JKGKJLLFMLE.MIIGKEBFKKD = LNGKNOGOIKL.FMAGAEMFION<AssignData>(sourceAssign);
			JKGKJLLFMLE.MIIGKEBFKKD.CorrectVersion();
			if (firstLoad)
			{
				firstLoad = false;
				Menu menu = FindObjectOfType<Menu>();
				if (menu == null) throw new InvalidOperationException("menu-not-ready");
				AccessTools.Method(typeof(Menu), "BDKIMPEDKCJ").Invoke(menu, new object[] { "Practice", null });
			}
			else AccessTools.Method(typeof(SceneMan), "CJLFFPJICPC").Invoke(Arena.OEDCBNHNGMJ, new object[] { "Practice", false });
			Log("CRAFT_PROBE_LOAD run=" + run + " percent=" + percent);
		}
		private void Update()
		{
			if (!done && Time.realtimeSinceStartup > deadline) { Fail(new TimeoutException("craft-probe-deadline")); return; }
			if (done || machine == null || ready) return;
			if (machine.NFMPBACKJOJ == null || machine.NFMPBACKJOJ.isKinematic || machine.transform.position.y > 1000f) return;
			HashSet<int> seen = new HashSet<int>();
			foreach (GameObject root in machine.KBLANAFAJFP)
				if (root != null) foreach (PistonController piston in root.GetComponentsInChildren<PistonController>())
					if (seen.Add(piston.GetInstanceID())) pistons.Add(piston);
			foreach (GameObject root in machine.KBLANAFAJFP)
				if (root != null) foreach (JointController joint in root.GetComponentsInChildren<JointController>())
					if (!(joint is PistonController) && seen.Add(joint.GetInstanceID())) hinges.Add(joint);
			try
			{
				HashSet<int> wheelIds = new HashSet<int>();
				foreach (GameObject root in machine.KBLANAFAJFP) if (root != null)
					foreach (WheelController wheel in root.GetComponentsInChildren<WheelController>())
						if (wheelIds.Add(wheel.GetInstanceID())) wheels.Add(wheel);
				PrepareFlatGround();
				HashSet<int> bodyIds = new HashSet<int>();
				List<GameObject> physicalRoots = new List<GameObject> { machine.gameObject }; physicalRoots.AddRange(machine.KBLANAFAJFP);
				foreach (GameObject root in physicalRoots) if (root != null)
					foreach (Rigidbody body in root.GetComponentsInChildren<Rigidbody>())
						if (bodyIds.Add(body.GetInstanceID()))
						{
							MagnificationSuspensionContacts observer = body.gameObject.AddComponent<MagnificationSuspensionContacts>();
							observer.Bind(body); contactProbes.Add(observer);
						}
				Log("CRAFT_PROBE_CONTACTS_READY bodies=" + contactProbes.Count + " solverOverride=" + solverIterations + " scope=isolated-diagnostic-only");
				MagnificationCraftModelProbe.ApplyInertia(machine);
				if (fixedStep > 0f)
				{
					if (originalFixedStep == 0f) originalFixedStep = Time.fixedDeltaTime;
					Time.fixedDeltaTime = fixedStep;
					Log("CRAFT_PROBE_STEP_OVERRIDE source=" + F(originalFixedStep) + " actual=" + F(fixedStep) + " scope=isolated-diagnostic-only");
				}
				MagnificationCraftPassiveProbe.Attach(machine, trace);
				MagnificationD6LimitProbe.Ready(machine);
				MagnificationAuxiliaryProbe.Ready(machine);
				ready = true;
				Log("CRAFT_PROBE_READY craft=" + craft + " percent=" + percent + " pistons=" + pistons.Count + " body=" + machine.NFMPBACKJOJ.mass + " fixedDelta=" + F(Time.fixedDeltaTime));
				MagnificationRotorIsolationProbe.Arm(machine);
			}
			catch (Exception error) { Fail(error); }
		}
		private void PrepareFlatGround()
		{
			groundY = 0f;
            if (MagnificationAuxiliaryProbe.NativeGround || MagnificationInuProbe.Requested)
            {
                float closest = float.PositiveInfinity; Collider support = null;
                foreach (RaycastHit hit in Physics.RaycastAll(machine.transform.position + Vector3.up * 1000f, Vector3.down, 2000f))
                    if (hit.collider != null && !hit.collider.isTrigger && hit.collider.attachedRigidbody == null && hit.normal.y > .5f && hit.distance < closest)
                    { closest = hit.distance; support = hit.collider; groundY = hit.point.y; }
                Log("CRAFT_NATIVE_GROUND surface=" + (support == null ? "not-found" : support.name) + " layer=" + (support == null ? -1 : support.gameObject.layer)
                    + " groundY=" + F(groundY) + " contactOffset=" + (support == null ? "none" : F(support.contactOffset)) + " mutation=none idleSeconds=20");
                if (support == null) throw new InvalidOperationException("native-ground-no-static-support");
                return;
            }
			HashSet<int> own = new HashSet<int>();
			List<GameObject> roots = new List<GameObject> { machine.gameObject }; roots.AddRange(machine.KBLANAFAJFP);
			foreach (GameObject root in roots) if (root != null) foreach (Collider collider in root.GetComponentsInChildren<Collider>()) own.Add(collider.GetInstanceID());
			PhysicMaterial material = null; float largest = 0f; int disabled = 0;
            Collider matched = null;
            if (MagnificationAuxiliaryProbe.MatchedGround)
            {
                float closest = float.PositiveInfinity;
                foreach (RaycastHit hit in Physics.RaycastAll(machine.transform.position + Vector3.up * 1000f, Vector3.down, 2000f))
                    if (hit.collider != null && !hit.collider.isTrigger && hit.collider.attachedRigidbody == null && hit.normal.y > .999f && hit.distance < closest)
                    { closest = hit.distance; matched = hit.collider; }
                if (matched == null || !(matched is BoxCollider)) throw new InvalidOperationException("matched-ground-no-flat-box-support");
                material = matched.sharedMaterial;
            }
			foreach (Collider collider in FindObjectsOfType<Collider>())
			{
				if (own.Contains(collider.GetInstanceID())) continue;
				if (!MagnificationAuxiliaryProbe.MatchedGround && collider.attachedRigidbody == null && !collider.isTrigger && collider.bounds.size.x > largest) { material = collider.sharedMaterial; largest = collider.bounds.size.x; }
				collider.enabled = false; disabled++;
			}
			GameObject ground = new GameObject("MPatcher_CraftProbe_FlatGround");
			ground.layer = 0; ground.transform.position = new Vector3(0f, -0.5f, 0f);
			BoxCollider box = ground.AddComponent<BoxCollider>(); box.size = new Vector3(100000f, 1f, 100000f); box.sharedMaterial = material;
            if (matched != null)
            {
                ground.layer = matched.gameObject.layer; box.contactOffset = matched.contactOffset;
                Log("CRAFT_MATCHED_GROUND source=" + matched.name + " layer=" + ground.layer + " contactOffset=" + F(box.contactOffset)
                    + " material=" + (material == null ? "Unity-default" : material.name) + " flatClone=True ramps=False craftUntouched=True");
            }
            MagnificationAuxiliaryProbe.Ground(box, machine.DJCDFLDHPHK);
			Log("CRAFT_PROBE_GROUND fixture=flat-world-y0 nativeCollidersDisabled=" + disabled + " material=" + (material == null ? "Unity-default" : material.name));
			if (sourceStart)
			{
				float bottom = float.PositiveInfinity; string lowest = "none"; int count = 0;
				foreach (GameObject root in roots) if (root != null)
				{
					foreach (Collider collider in root.GetComponentsInChildren<Collider>())
						if (collider.enabled && !collider.isTrigger && collider.attachedRigidbody != null && collider.attachedRigidbody.detectCollisions)
						{
							count++;
							if (collider.bounds.min.y < bottom) { bottom = collider.bounds.min.y; lowest = collider.name; }
						}
				}
				if (float.IsInfinity(bottom) || count == 0) throw new InvalidOperationException("source-start-no-physical-collider");
				float scale = machine.DJCDFLDHPHK;
				// Move only the test plane, not craft poses, velocity or native
				// controller position history. Both variants keep the same clearance.
				groundY = bottom - .1f * scale;
				ground.transform.position = new Vector3(0f, groundY - .5f, 0f);
				Log("CRAFT_SOURCE_START clearanceCraft=0.1 priorBottom=" + F(bottom) + " groundY=" + F(groundY) + " scale=" + F(scale)
					+ " physicalColliders=" + count + " lowest=" + lowest + " craftPoseAndVelocity=unchanged fixtureOnly=True");
			}
		}
		private void FixedUpdate()
		{
			if (done || !ready || machine == null) return;
			try
			{
				if (solverIterations != 0) foreach (MagnificationSuspensionContacts observer in contactProbes)
					observer.OverrideSolver(solverIterations);
				// Keep controls and sample windows at the same simulated seconds
				// when diagnosing step sensitivity. Production timing is untouched.
				elapsed += Time.fixedDeltaTime;
				int nextFrame = (int)Math.Floor((double)elapsed * 100.0 + .001);
				if (nextFrame <= frame) return;
				frame = nextFrame;
				if (frame == 1 || frame == 5 || frame == 100) MagnificationJointPoseProbe.SampleMachine(machine, "craft-" + frame);
                MagnificationPhysicalGraph.Capture(machine, craft, frame);
                MagnificationAuxiliaryProbe.Sample(machine, frame);
                MagnificationInuProbe.Sample(machine, frame);
                MagnificationJointUnitProbe.Sample(frame);
				if (frame == 1 || frame == 5 || frame == 100 || frame == 1000 || frame == 1500 || frame == 2300 || frame == 3200) MagnificationD6LimitProbe.Sample(machine, "craft-" + frame);
				if (frame % 100 == 0) foreach (MagnificationSuspensionContacts observer in contactProbes) observer.Snapshot(frame, machine.DJCDFLDHPHK);
				int phase = frame <= 1000 ? 0 : frame <= 1500 ? 1 : frame <= 1800 ? 2 : frame <= 2300 ? 3 : frame <= 2600 ? 4 : 5;
                if (MagnificationAuxiliaryProbe.ToleranceComparison || MagnificationJointUnitProbe.Requested) phase = frame <= (MagnificationAuxiliaryProbe.LongRest ? 2000 : 1000) ? 0 : 1;
                if (MagnificationAuxiliaryProbe.StraightComparison) phase = frame <= (MagnificationAuxiliaryProbe.LongRest ? 2000 : 1000) ? 0 : run % 2 == 0 ? 1 : 3;
				if (phase != phaseIndex) { FlushPhase(); phaseIndex = phase; Log("CRAFT_PROBE_PHASE percent=" + percent + " phase=" + PhaseName(phase)); straightLine.Begin(craft, percent, PhaseName(phase), machine.DJCDFLDHPHK, machine.NFMPBACKJOJ, frame); }
				Rigidbody rootBody = machine.NFMPBACKJOJ;
				bool sample = frame > (MagnificationAuxiliaryProbe.LongRest ? 1500 : 500);
				straightLine.Observe(rootBody, frame, sample);
				if (sample)
				{
					phaseSpeed += rootBody.velocity.magnitude;
					phaseYaw += rootBody.angularVelocity.y;
					phaseVertical += rootBody.velocity.y * rootBody.velocity.y;
					phasePitch += rootBody.angularVelocity.x * rootBody.angularVelocity.x + rootBody.angularVelocity.z * rootBody.angularVelocity.z;
					phaseAngular = Mathf.Max(phaseAngular, rootBody.angularVelocity.magnitude);
				}
				float error = 0f;
				foreach (PistonController piston in pistons)
				{
					ConfigurableJoint joint = PistonJoint.GetValue(piston) as ConfigurableJoint;
					if (joint == null || joint.connectedBody == null) continue;
					Vector3 offset = Quaternion.Inverse(joint.transform.rotation) * (joint.connectedBody.transform.TransformPoint(joint.connectedAnchor) - joint.transform.TransformPoint(joint.anchor));
					int axis = joint.xMotion == ConfigurableJointMotion.Limited ? 0 : joint.yMotion == ConfigurableJointMotion.Limited ? 1 : 2;
					float signedError = (offset[axis] - joint.targetPosition[axis]) / machine.DJCDFLDHPHK;
					if (sample) straightLine.ObservePiston(piston, rootBody, axis, signedError);
					error = Mathf.Max(error, Mathf.Abs(signedError));
					if (frame == 1000 || frame == 1500 || frame == 2300 || frame == 3200)
					{
						JointDrive drive = axis == 0 ? joint.xDrive : axis == 1 ? joint.yDrive : joint.zDrive;
						Vector3 localAxis = axis == 0 ? Vector3.right : axis == 1 ? Vector3.up : Vector3.forward;
						Vector3 worldAxis = joint.transform.TransformDirection(localAxis);
						Rigidbody own = joint.GetComponent<Rigidbody>();
						Log("CRAFT_PROBE_PISTON frame=" + frame + " part=" + piston.JNKEKNOAPHO.GetPosString(true)
							+ " axis=" + axis + " residualCraft=" + F(signedError) + " targetWorld=" + F(joint.targetPosition[axis])
							+ " spring=" + F(drive.positionSpring) + " damper=" + F(drive.positionDamper) + " maxForce=" + F(drive.maximumForce)
							+ " axisForce=" + F(Vector3.Dot(joint.currentForce, worldAxis))
							+ " worldAxis=" + worldAxis.ToString("R") + " ownBody=" + own.name + " otherBody=" + joint.connectedBody.name
							+ " ownAnchor=" + joint.transform.TransformPoint(joint.anchor).ToString("R")
							+ " otherAnchor=" + joint.connectedBody.transform.TransformPoint(joint.connectedAnchor).ToString("R")
							+ " relativeVelocity=" + F(Vector3.Dot(joint.connectedBody.velocity - own.velocity, worldAxis))
							+ " ownMass=" + F(own.mass) + " otherMass=" + F(joint.connectedBody.mass));
					}
				}
				if (sample)
				{
					phasePiston += error * error; phasePeak = Mathf.Max(phasePeak, error); phaseSamples++;
					// Preserve very small oscillations around a nonzero static sag.
					double delta = error - pistonMean;
					pistonMean += delta / phaseSamples;
					pistonM2 += delta * (error - pistonMean);
				}
				if (frame % 100 == 0)
				{
					foreach (WheelController wheel in wheels)
					{
						Rigidbody tire = wheel.EKHOCFCEGCB;
						if (tire == null) continue;
						float spin = Vector3.Dot(tire.angularVelocity, wheel.transform.up);
						float radius = wheel.JHADHFJDPFD * .5f * machine.DJCDFLDHPHK;
						ConfigurableJoint axle = WheelJoint.GetValue(wheel) as ConfigurableJoint;
						Log("CRAFT_PROBE_WHEEL frame=" + frame + " part=" + wheel.JNKEKNOAPHO.GetPosString(true)
							+ " spin=" + F(spin) + " cap=" + F(tire.maxAngularVelocity) + " radius=" + F(radius)
							+ " treadSpeed=" + F(spin * radius) + " targetRPM=" + F(wheel.EPJKGGHAOAI)
							+ " axleMotion=" + (axle == null ? "none" : axle.xMotion.ToString()));
					}
					float hingeRate = 0f, hingeError = 0f;
					foreach (JointController joint in hinges)
					{
						MagnificationRotorProbe.Snapshot(joint, frame);
						if (joint.ALGHGLCBLDJ == null || joint.ALGHGLCBLDJ.connectedBody == null) continue;
						Rigidbody ownBody = joint.ALGHGLCBLDJ.GetComponent<Rigidbody>();
						hingeRate = Mathf.Max(hingeRate, Mathf.Abs(Vector3.Dot(ownBody.angularVelocity - joint.ALGHGLCBLDJ.connectedBody.angularVelocity, joint.ALGHGLCBLDJ.transform.TransformDirection(joint.ALGHGLCBLDJ.axis))));
						hingeError = Mathf.Max(hingeError, Mathf.Abs(Mathf.DeltaAngle((float)HingeAngle.GetValue(joint), (float)HingeTarget.GetValue(joint))));
					}
					int brokenBodies = 0;
					foreach (BodyController body in machine.ILBAAENKMBL) if (body != null && body.IsBroken()) brokenBodies++;
					Log("CRAFT_PROBE_TICK percent=" + percent + " frame=" + frame + " position=" + rootBody.position.ToString("R") + " speed=" + F(rootBody.velocity.magnitude) + " vertical=" + F(rootBody.velocity.y)
						+ " failure=" + machine.IsFailure() + " brokenBodies=" + brokenBodies
						+ " yaw=" + F(rootBody.angularVelocity.y) + " pistonErrorCraft=" + F(error) + " power=" + machine.KNKMDKKBHBA + " keys=" + HOCGCCAIPFF.JNPMJANFGNH)
						;
					Log("CRAFT_PROBE_ROTATION percent=" + percent + " frame=" + frame + " count=" + hinges.Count + " hingeRateMax=" + F(hingeRate) + " hingeTargetErrorMax=" + F(hingeError)
						+ " mainAngular=" + rootBody.angularVelocity.ToString("R") + " mainPhysicalI=" + rootBody.inertiaTensor.ToString("R") + " mainCraftI=" + MagnificationInertiaBoundary.Read(rootBody).ToString("R"));
				}
				if (frame % 100 == 0 && Model >= 3 && !MagnificationScalePolicy.SameTime) MagnificationInertiaBoundary.CheckRoundtrip(rootBody);
				if (frame >= (MagnificationInuProbe.Requested ? 3600 : MagnificationAuxiliaryProbe.SledGeometry ? 1000 : (MagnificationAuxiliaryProbe.StraightComparison || MagnificationAuxiliaryProbe.ToleranceComparison || MagnificationJointUnitProbe.Requested) ? (MagnificationAuxiliaryProbe.LongRest ? 2500 : 1500) : 3200))
				{
					FlushPhase(); ready = false; run++;
					if (run < scales.Length) LoadRun(); else Finish();
				}
			}
			catch (Exception error) { Fail(error); }
		}
		private void FlushPhase()
		{
			straightLine.Flush();
			MagnificationRotorIsolationProbe.Snapshot(PhaseName(phaseIndex), frame);
			if (phaseSamples == 0) return;
			Log("CRAFT_PROBE_RESULT percent=" + percent + " phase=" + PhaseName(phaseIndex) + " samples=" + phaseSamples
				+ " speedMean=" + F(phaseSpeed / phaseSamples) + " yawMean=" + F(phaseYaw / phaseSamples)
				+ " verticalRms=" + F(Mathf.Sqrt(phaseVertical / phaseSamples)) + " pistonErrorRms=" + F(Mathf.Sqrt(phasePiston / phaseSamples))
				+ " pistonErrorStd=" + F((float)Math.Sqrt(Math.Max(0.0, pistonM2 / phaseSamples)))
				+ " pitchRollRms=" + F(Mathf.Sqrt(phasePitch / phaseSamples))
				+ " pistonErrorPeak=" + F(phasePeak) + " angularPeak=" + F(phaseAngular)
				+ " scriptInertiaReads=" + MagnificationManagedBridge.InertiaReads + " scriptInertiaWrites=" + MagnificationManagedBridge.InertiaWrites + " scriptKeyReads=" + MagnificationManagedBridge.KeyReads
				+ " scriptMotorReads=" + MagnificationScriptJointUnits.MotorReads + " scriptMotorWrites=" + MagnificationScriptJointUnits.MotorWrites
				+ " scriptSpringReads=" + MagnificationScriptJointUnits.SpringReads + " scriptSpringWrites=" + MagnificationScriptJointUnits.SpringWrites
				+ " scriptMassReads=" + MagnificationMassBoundary.ScriptReads + " scriptMassWrites=" + MagnificationMassBoundary.ScriptWrites + " convertedForceCalls=" + MagnificationForceUnits.Calls
				+ " contactDisableRequests=" + MagnificationContactProbe.DisableRequests + " contactDisableSuppressed=" + MagnificationContactProbe.Suppressed);
			phaseSamples = 0; phaseSpeed = phaseYaw = phaseVertical = phasePiston = phasePeak = phaseAngular = phasePitch = 0f;
			pistonMean = pistonM2 = 0.0;
		}
		private static string PhaseName(int index) { return MagnificationCraftPassiveProbe.Active ? "passive-window-" + index : index == 0 ? "idle" : index == 1 ? "forward" : index == 2 ? "coast" : index == 3 ? "reverse" : index == 4 ? "coast2" : MagnificationInuProbe.Requested ? "sprint-shift-only" : "forward-left"; }
		private static bool ReadKey(KeyCode key)
		{
			if (MagnificationCraftPassiveProbe.Active || MagnificationAuxiliaryProbe.SledGeometry) return false;
			if (active == null || !active.ready || active.done) return false;
			int phase = active.phaseIndex;
			return (key == KeyCode.W && (phase == 1 || (!MagnificationInuProbe.Requested && phase == 5))) || (key == KeyCode.S && phase == 3) || (key == (MagnificationInuProbe.Requested ? KeyCode.LeftShift : KeyCode.A) && phase == 5);
		}
		private static bool ReadKeyDown(KeyCode key) { return false; }
		internal static bool ScriptReadKey(KeyCode key) { return ReadKey(key); }
		internal static bool ScriptReadKeyEdge(KeyCode key) { return false; }
		internal static float ScriptReadAxis(string name) { return 0f; }
		private static IEnumerable<CodeInstruction> InputTranspiler(IEnumerable<CodeInstruction> instructions)
		{
			foreach (CodeInstruction original in instructions)
			{
				CodeInstruction code = new CodeInstruction(original);
				MethodInfo method = code.operand as MethodInfo;
				if (method != null && method.DeclaringType == typeof(Input) && (method.Name == "GetKey" || method.Name == "GetKeyDown"))
					code.operand = AccessTools.Method(typeof(MagnificationCraftProbe), method.Name == "GetKey" ? "ReadKey" : "ReadKeyDown");
				yield return code;
			}
		}
		private static IEnumerable<CodeInstruction> CoverageTranspiler(IEnumerable<CodeInstruction> instructions, MethodBase __originalMethod)
		{
			int id = methodNames.Count;
			methodNames.Add(__originalMethod.MetadataToken.ToString("X8") + ":" + __originalMethod.DeclaringType.FullName + "." + __originalMethod.Name); methodCounts.Add(0);
			yield return new CodeInstruction(OpCodes.Ldc_I4, id);
			yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(MagnificationCraftProbe), "Record"));
			foreach (CodeInstruction code in instructions) yield return new CodeInstruction(code);
		}
		private static void Record(int id) { methodCounts[id]++; }
		private void Finish()
		{
			done = true;
			MagnificationSafetyObserver.Flush();
            MagnificationInuProbe.Flush();
			if (originalFixedStep > 0f) Time.fixedDeltaTime = originalFixedStep;
			for (int i = 0; i < methodNames.Count; i++) if (methodCounts[i] != 0) Log("CRAFT_PROBE_COVERAGE method=" + methodNames[i] + " calls=" + methodCounts[i]);
			if (originalSystem != null) JKGKJLLFMLE.IGOBPLOLHEP = LNGKNOGOIKL.FMAGAEMFION<SystemData>(originalSystem);
			Log("CRAFT_PROBE_COMPLETE success=" + (!failed && run == scales.Length) + " craft=" + craft);
			Application.Quit();
		}
		private void Fail(Exception error) { failed = true; Log("CRAFT_PROBE_FAILED " + error); Finish(); }
		private static string F(float value) { return value.ToString("R", CultureInfo.InvariantCulture); }
		private static void Log(string text) { MagnificationDownscale.Log(text + " model=" + Model); }
	}
}


