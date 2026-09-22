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
		private static readonly FieldInfo HingeAngle = AccessTools.Field(typeof(JointController), "OPKPNLOMBDI"), HingeTarget = AccessTools.Field(typeof(JointController), "GEKMCAJOPNK");
		private Harmony trace;
		private MachineController machine;
		private readonly List<PistonController> pistons = new List<PistonController>();
		private readonly List<JointController> hinges = new List<JointController>();
		private string sourceBuild, sourceAssign, originalSystem, craft = "Car", folder = "_samples";
		private int percent, run, frame, phaseIndex = -1, phaseSamples;
		private bool ready, done, failed;
		private float phaseSpeed, phaseYaw, phaseVertical, phasePiston, phasePeak, phaseAngular, phasePistonMean, phasePitch;
		private float deadline;
		private int[] scales = { 100, 5, 5, 5, 5 };
		private int[] models = { 0, 1, 4, 7, 8 };
		internal static int Model { get { return active == null ? 0 : active.models[Math.Min(active.run, active.models.Length - 1)]; } }
		private IEnumerator Start()
		{
			DontDestroyOnLoad(gameObject);
			active = this;
			deadline = Time.realtimeSinceStartup + 900f;
			while (Time.realtimeSinceStartup < 60f) yield return null;
			try { Setup(); LoadRun(); }
			catch (Exception error) { Fail(error); }
		}
		private void Setup()
		{
			string root = Path.GetFullPath(Path.Combine(Application.dataPath, ".."));
			string expected = Path.Combine(Environment.GetEnvironmentVariable("USERPROFILE") ?? "", "Downloads\\MachineCraftMPatcherTest");
			if (!string.Equals(root.TrimEnd('\\', '/'), expected, StringComparison.OrdinalIgnoreCase)) throw new InvalidOperationException("not-test-root");
			foreach (string arg in Environment.GetCommandLineArgs())
			{
				if (arg.StartsWith("--mpatcher-craft="))
				{
					craft = arg.Substring("--mpatcher-craft=".Length);
					if (craft != "Car" && craft != "JZZ30_Special2" && craft != "Q_7101" && craft != "Test_DummyAll" && craft != "Test_DummyEnds") throw new ArgumentException("unknown-probe-craft");
					folder = craft == "Car" ? "_samples" : "";
				}
				if (arg == "--mpatcher-craft-short") { scales = new[] { 100, 5, 5 }; models = new[] { 0, 1, 7 }; }
				if (arg == "--mpatcher-craft-linear") { scales = new[] { 100, 5, 5, 5 }; models = new[] { 0, 1, 7, 9 }; }
				if (arg == "--mpatcher-craft-conditioning") { scales = new[] { 100, 5, 5, 5 }; models = new[] { 0, 1, 7, 10 }; }
				if (arg.StartsWith("--mpatcher-craft-model="))
				{
					int selected = int.Parse(arg.Substring("--mpatcher-craft-model=".Length), CultureInfo.InvariantCulture);
					if (selected != 0 && selected != 1 && selected != 7 && selected != 9 && selected != 10) throw new ArgumentException("unknown-model");
					models = new[] { selected }; scales = new[] { selected == 0 ? 100 : 5 };
				}
			}
			string path = Path.Combine(Path.Combine(root, "UserData"), folder);
			sourceBuild = File.ReadAllText(Path.Combine(path, craft + ".mcbd"));
			sourceAssign = File.ReadAllText(Path.Combine(path, craft + ".mcad"));
			originalSystem = LNGKNOGOIKL.AOPMIBBFLKH(JKGKJLLFMLE.IGOBPLOLHEP);
			trace = new Harmony("local.mpatcher.isolated.craft-probe");
			MagnificationCraftModelProbe.Register(trace);
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
			Log("CRAFT_PROBE_START revision=10 craft=" + folder + "/" + craft + " runs=" + scales.Length + " input=native-key-queries+compiled-script-calls save=blocked coverageMethods=" + methodNames.Count);
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
			active.ready = false;
			active.frame = 0;
			active.phaseIndex = -1;
			Log("CRAFT_PROBE_CREATED run=" + active.run + " percent=" + active.percent + " scale=" + F(__instance.DJCDFLDHPHK));
		}
		private void LoadRun()
		{
			MagnificationInertiaBoundary.Clear();
			MagnificationManagedBridge.ResetCounters();
			percent = scales[run];
			ready = false; machine = null; pistons.Clear(); hinges.Clear(); frame = 0;
			JKGKJLLFMLE.IGOBPLOLHEP.folderName = folder;
			JKGKJLLFMLE.IGOBPLOLHEP.machineName = craft;
			JKGKJLLFMLE.IGOBPLOLHEP.practiceWorldType = JKGKJLLFMLE.CDIAGJLJCJC.Field;
			JKGKJLLFMLE.IGOBPLOLHEP.practiceAreaMode = JKGKJLLFMLE.BJIMLKIAEHD.Free;
			JKGKJLLFMLE.HHGILAIOCLG = LNGKNOGOIKL.FMAGAEMFION<BuildData>(sourceBuild);
			JKGKJLLFMLE.HHGILAIOCLG.CorrectVersion();
			JKGKJLLFMLE.HHGILAIOCLG.magnification = percent;
			JKGKJLLFMLE.MIIGKEBFKKD = LNGKNOGOIKL.FMAGAEMFION<AssignData>(sourceAssign);
			JKGKJLLFMLE.MIIGKEBFKKD.CorrectVersion();
			if (run == 0)
			{
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
				PrepareFlatGround();
				MagnificationCraftModelProbe.ApplyInertia(machine);
				ready = true;
				Log("CRAFT_PROBE_READY craft=" + craft + " percent=" + percent + " pistons=" + pistons.Count + " body=" + machine.NFMPBACKJOJ.mass + " fixedDelta=" + F(Time.fixedDeltaTime));
			}
			catch (Exception error) { Fail(error); }
		}
		private void PrepareFlatGround()
		{
			HashSet<int> own = new HashSet<int>();
			List<GameObject> roots = new List<GameObject> { machine.gameObject }; roots.AddRange(machine.KBLANAFAJFP);
			foreach (GameObject root in roots) if (root != null) foreach (Collider collider in root.GetComponentsInChildren<Collider>()) own.Add(collider.GetInstanceID());
			PhysicMaterial material = null; float largest = 0f; int disabled = 0;
			foreach (Collider collider in FindObjectsOfType<Collider>())
			{
				if (own.Contains(collider.GetInstanceID())) continue;
				if (collider.attachedRigidbody == null && !collider.isTrigger && collider.bounds.size.x > largest) { material = collider.sharedMaterial; largest = collider.bounds.size.x; }
				collider.enabled = false; disabled++;
			}
			GameObject ground = new GameObject("MPatcher_CraftProbe_FlatGround");
			ground.layer = 0; ground.transform.position = new Vector3(0f, -0.5f, 0f);
			BoxCollider box = ground.AddComponent<BoxCollider>(); box.size = new Vector3(100000f, 1f, 100000f); box.sharedMaterial = material;
			Log("CRAFT_PROBE_GROUND fixture=flat-world-y0 nativeCollidersDisabled=" + disabled + " material=" + (material == null ? "Unity-default" : material.name));
		}
		private void FixedUpdate()
		{
			if (done || !ready || machine == null) return;
			try
			{
				frame++;
				int phase = frame <= 1000 ? 0 : frame <= 1500 ? 1 : frame <= 1800 ? 2 : frame <= 2300 ? 3 : frame <= 2600 ? 4 : 5;
				if (phase != phaseIndex) { FlushPhase(); phaseIndex = phase; Log("CRAFT_PROBE_PHASE percent=" + percent + " phase=" + PhaseName(phase)); }
				Rigidbody rootBody = machine.NFMPBACKJOJ;
				bool sample = frame > 500;
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
					error = Mathf.Max(error, Mathf.Abs(offset[axis] - joint.targetPosition[axis]) / machine.DJCDFLDHPHK);
				}
				if (sample) { phasePiston += error * error; phasePistonMean += error; phasePeak = Mathf.Max(phasePeak, error); phaseSamples++; }
				if (frame % 100 == 0)
				{
					float hingeRate = 0f, hingeError = 0f;
					foreach (JointController joint in hinges)
					{
						if (joint.ALGHGLCBLDJ == null || joint.ALGHGLCBLDJ.connectedBody == null) continue;
						Rigidbody ownBody = joint.ALGHGLCBLDJ.GetComponent<Rigidbody>();
						hingeRate = Mathf.Max(hingeRate, Mathf.Abs(Vector3.Dot(ownBody.angularVelocity - joint.ALGHGLCBLDJ.connectedBody.angularVelocity, joint.ALGHGLCBLDJ.transform.TransformDirection(joint.ALGHGLCBLDJ.axis))));
						hingeError = Mathf.Max(hingeError, Mathf.Abs(Mathf.DeltaAngle((float)HingeAngle.GetValue(joint), (float)HingeTarget.GetValue(joint))));
					}
					Log("CRAFT_PROBE_TICK percent=" + percent + " frame=" + frame + " speed=" + F(rootBody.velocity.magnitude) + " vertical=" + F(rootBody.velocity.y)
						+ " yaw=" + F(rootBody.angularVelocity.y) + " pistonErrorCraft=" + F(error) + " power=" + machine.KNKMDKKBHBA + " keys=" + HOCGCCAIPFF.JNPMJANFGNH)
						;
					Log("CRAFT_PROBE_ROTATION percent=" + percent + " frame=" + frame + " count=" + hinges.Count + " hingeRateMax=" + F(hingeRate) + " hingeTargetErrorMax=" + F(hingeError)
						+ " mainAngular=" + rootBody.angularVelocity.ToString("R") + " mainPhysicalI=" + rootBody.inertiaTensor.ToString("R") + " mainCraftI=" + MagnificationInertiaBoundary.Read(rootBody).ToString("R"));
				}
				if (frame % 100 == 0 && Model >= 3) MagnificationInertiaBoundary.CheckRoundtrip(rootBody);
				if (frame >= 3200)
				{
					FlushPhase(); ready = false; run++;
					if (run < scales.Length) LoadRun(); else Finish();
				}
			}
			catch (Exception error) { Fail(error); }
		}
		private void FlushPhase()
		{
			if (phaseSamples == 0) return;
			Log("CRAFT_PROBE_RESULT percent=" + percent + " phase=" + PhaseName(phaseIndex) + " samples=" + phaseSamples
				+ " speedMean=" + F(phaseSpeed / phaseSamples) + " yawMean=" + F(phaseYaw / phaseSamples)
				+ " verticalRms=" + F(Mathf.Sqrt(phaseVertical / phaseSamples)) + " pistonErrorRms=" + F(Mathf.Sqrt(phasePiston / phaseSamples))
				+ " pistonErrorStd=" + F(Mathf.Sqrt(Mathf.Max(0f, phasePiston / phaseSamples - Mathf.Pow(phasePistonMean / phaseSamples, 2f))))
				+ " pitchRollRms=" + F(Mathf.Sqrt(phasePitch / phaseSamples))
				+ " pistonErrorPeak=" + F(phasePeak) + " angularPeak=" + F(phaseAngular)
				+ " scriptInertiaReads=" + MagnificationManagedBridge.InertiaReads + " scriptInertiaWrites=" + MagnificationManagedBridge.InertiaWrites + " scriptKeyReads=" + MagnificationManagedBridge.KeyReads);
			phaseSamples = 0; phaseSpeed = phaseYaw = phaseVertical = phasePiston = phasePeak = phaseAngular = phasePitch = phasePistonMean = 0f;
		}
		private static string PhaseName(int index) { return index == 0 ? "idle" : index == 1 ? "forward" : index == 2 ? "coast" : index == 3 ? "reverse" : index == 4 ? "coast2" : "forward-left"; }
		private static bool ReadKey(KeyCode key)
		{
			if (active == null || !active.ready || active.done) return false;
			int phase = active.phaseIndex;
			return (key == KeyCode.W && (phase == 1 || phase == 5)) || (key == KeyCode.S && phase == 3) || (key == KeyCode.A && phase == 5);
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
