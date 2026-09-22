using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using McnCraft;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
	// Audited against Assembly-CSharp D188E389...: Parts.Start/CODDJFDCCPC,
	// Joint.FixedUpdate/Reset, Piston.FixedUpdate/Reset and HOCGCCAIPFF input rebuilding.
	// Cache the construction map and field accessors once. The 250 ms path never
	// traverses BuildData, searches the hierarchy or uses per-field reflection.
	internal sealed class LegacyMachineControlRecovery
	{
		private const BindingFlags InstanceFields = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly;
		private const BindingFlags StaticFields = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
		private static readonly FieldInfo MainToggles = InputField("NACEPMDCCPO", typeof(bool[]));
		private static readonly FieldInfo AlternateToggles = InputField("KNDELDDECBP", typeof(bool[]));
		private static readonly FieldInfo MainKeys = InputField("NEOANPPLPCH", typeof(List<KeyCode>));
		private static readonly FieldInfo AlternateKeys = InputField("DMEIGHAOBML", typeof(List<KeyCode>));
		private static readonly FieldInfo AltKeys = InputField("AGELMLNGEHB", typeof(List<KeyCode>));
		private static readonly FieldInfo ActionList = Field(typeof(PartsController), "CNIFPDEEHDJ");
		private static readonly FieldInfo NeutralAction = Field(typeof(PartsController), "GIEKPJPGAEJ");
		private static readonly FieldInfo SelfInput = Field(typeof(PartsController), "OEOIBDEEBKP");
		private static readonly FieldInfo JointSpringField = Field(typeof(JointController), "MECNMMIDEOL");
		private static readonly FieldInfo JointLimitsField = Field(typeof(JointController), "NEDLPPMOKEI");
		private static readonly FieldInfo JointOtherLimitsField = Field(typeof(JointController), "CLBOKNLFECK");
		private static readonly FieldInfo PistonJointField = Field(typeof(PistonController), "PDNNFOJPIDM");
		private static readonly FieldInfo PistonLimitField = Field(typeof(PistonController), "KGJEALJEOHP");
		private static readonly FieldInfo PistonDriveField = Field(typeof(PistonController), "EDEDBPALHEL");
		private static readonly FieldInfo PistonWarmup = Field(typeof(PistonController), "HEDEOMBHCCI");
		private static readonly FieldInfo LampCommand = Field(typeof(LampController), "EOMFNIEDKDF");
		private static readonly FieldInfo LampBulb = Field(typeof(LampController), "OIAHOOPPLKK");
		private static readonly FieldInfo EmitterCommand = Field(typeof(EmitterController), "EOMFNIEDKDF");
		private static readonly FieldInfo EmitterSmoke = Field(typeof(EmitterController), "PLJILHHFMLM");
		private static readonly MethodInfo BuildKeyConflicts = typeof(HOCGCCAIPFF).GetMethod("JBGPBPIMJOJ", StaticFields);
		private static readonly Dictionary<Type, ScalarLayout> ScalarLayouts = new Dictionary<Type, ScalarLayout>();
		private readonly MachineController machine;
		private readonly AssignData mainAssign;
		private readonly InputLayout mainInput;
		private readonly Part[] parts;
		private readonly string layoutHash;

		internal MachineController Machine { get { return machine; } }
		internal int PartCount { get { return parts.Length; } }
		internal string LayoutHash { get { return layoutHash; } }
		internal bool ConfigurationReferencesMatch
		{
			get { return ReferenceEquals(mainAssign, JKGKJLLFMLE.MIIGKEBFKKD); }
		}

		internal static void ValidateBindings()
		{
			if (BuildKeyConflicts == null || JointSpringField.FieldType != typeof(JointSpring)
				|| PistonDriveField.FieldType != typeof(JointDrive)) throw new MissingFieldException("control schema");
			GetScalarLayout(typeof(PartsController)); GetScalarLayout(typeof(JointController));
			GetScalarLayout(typeof(PistonController)); GetScalarLayout(typeof(WheelController));
			GetScalarLayout(typeof(ThrusterController));
		}

		internal LegacyMachineControlRecovery(MachineController owner)
		{
			machine = owner;
			mainAssign = JKGKJLLFMLE.MIIGKEBFKKD;
			if (machine == null || mainAssign == null || !ReferenceEquals(machine.MIIGKEBFKKD, mainAssign))
				throw new InvalidOperationException("owner-assign-not-ready");
			// Probe readiness before constructing action layouts or key-conflict maps.
			// A dormant part must not rebuild the ready prefix on every restore retry.
			List<PartsController> ready = new List<PartsController>();
			HashSet<PartsController> seen = new HashSet<PartsController>();
			int nativeTrashSkipped = 0;
			for (int body = 0; body < machine.ILBAAENKMBL.Count; body++)
				foreach (PartsController component in machine.ILBAAENKMBL[body].GetComponentsInChildren<PartsController>(true))
				{
					if (component.FPEBEEJGFPI != machine || !seen.Add(component)) continue;
					if (LegacyNativeConstructionTrash.Contains(machine, component)) { nativeTrashSkipped++; continue; }
					if (component.JNKEKNOAPHO == null || component.DLMKKFCHFNC == null
						|| !(bool)SelfInput.GetValue(component)) throw new InvalidOperationException(ReadinessDetail(component, "part-start-not-ready"));
					if (component is PistonController && (float)PistonWarmup.GetValue(component) < 0.999f)
						throw new InvalidOperationException(ReadinessDetail(component, "piston-warmup-not-ready"));
					ready.Add(component);
				}
			mainInput = new InputLayout(mainAssign, machine.HHGILAIOCLG);
			List<Part> found = new List<Part>(ready.Count);
			foreach (PartsController component in ready) found.Add(new Part(component));
			if (found.Count > LegacyMachineControlStateCodec.MaximumParts) throw new InvalidOperationException("too-many-parts");
			found.Sort(delegate(Part a, Part b) { return string.CompareOrdinal(a.Key, b.Key); });
			parts = found.ToArray();
			using (MemoryStream stream = new MemoryStream())
			using (BinaryWriter writer = new BinaryWriter(stream))
			{
				writer.Write("MPatcher.Controls.OwnerLayout.v2");
				writer.Write(LegacyMachineRecoveryFingerprint.OwnerControlConfiguration(mainAssign,
					mainInput.Mode));
				writer.Write(parts.Length);
				for (int i = 0; i < parts.Length; i++)
				{
					if (i > 0 && parts[i].Key == parts[i - 1].Key) throw new InvalidOperationException("ambiguous-control-part-" + parts[i].Key);
					parts[i].WriteLayout(writer);
				}
				writer.Flush();
				using (SHA256 hash = SHA256.Create())
					layoutHash = BitConverter.ToString(hash.ComputeHash(stream.ToArray())).Replace("-", "");
			}
			if (nativeTrashSkipped > 0) LegacyTransientReconnect.Log("CLIENT_CONTROL_NATIVE_TRASH_SKIPPED parts="
					+ nativeTrashSkipped + " liveParts=" + parts.Length + " ownerLayout=" + layoutHash + " source=native-delete-list");
		}

		private static string ReadinessDetail(PartsController component, string reason)
		{
			BlockData block = component.JNKEKNOAPHO;
			return reason + " type=" + component.GetType().Name
				+ " block=" + (block == null ? "missing" : block.index.ToString(CultureInfo.InvariantCulture))
				+ " body=" + (component.DLMKKFCHFNC == null ? "missing" : component.DLMKKFCHFNC.BBLGKLFBJGE.ToString(CultureInfo.InvariantCulture))
				+ " enabled=" + component.enabled + " active=" + component.gameObject.activeInHierarchy
				+ " selfInput=" + SelfInput.GetValue(component) + " tag=" + component.tag;
		}

		internal LegacyMachineControlState Capture()
		{
			if (!ConfigurationReferencesMatch) throw new InvalidOperationException("assign-replaced");
			LegacyMachineControlState result = new LegacyMachineControlState();
			result.Layout = layoutHash; result.Frame = Time.frameCount;
			result.MainToggles = ReadToggles(MainToggles); result.AlternateToggles = ReadToggles(AlternateToggles);
			result.MainMask = HOCGCCAIPFF.JNPMJANFGNH & LegacyMachineControlStateCodec.ActionBits;
			result.AlternateMask = HOCGCCAIPFF.OPBGHEJKICF & LegacyMachineControlStateCodec.ActionBits;
			result.AlternateActive = HOCGCCAIPFF.PKPJKJFBOGH;
			result.MainToggleKeys = ReadKeys(MainKeys); result.AlternateToggleKeys = ReadKeys(AlternateKeys);
			result.AltHeldKeys = ReadKeys(AltKeys);
			result.Parts = new LegacyControlPartState[parts.Length];
			for (int i = 0; i < parts.Length; i++) result.Parts[i] = parts[i].Capture();
			return result;
		}

		// Perform the entire schema/identity/availability preflight before changing bodies or input.
		internal void ValidateRestore(LegacyMachineControlState state)
		{
			if (!LegacyMachineControlStateCodec.Validate(state) || state == null || !ConfigurationReferencesMatch
				|| state.Layout != layoutHash || state.Parts.Length != parts.Length)
				throw new InvalidOperationException("control-layout-or-assign-mismatch");
			if (((bool[])MainToggles.GetValue(null)).Length != 60 || ((bool[])AlternateToggles.GetValue(null)).Length != 60)
				throw new InvalidOperationException("input-toggle-size");
			for (int i = 0; i < parts.Length; i++) parts[i].ValidateRestore(state.Parts[i]);
		}

		internal void Restore(LegacyMachineControlState state)
		{
			// Caller completed preflight before applying body damage. Broken native joints
			// may legitimately disappear between that preflight and this one-shot apply.
			WriteToggles(MainToggles, state.MainToggles);
			WriteKeys(MainKeys, state.MainToggleKeys);
			// The alternate bank belongs to a remote/enemy machine. It is intentionally
			// left in the state built by the new session; only owner-held keys are resumed.
			int[] ownerHeld = state.AlternateActive ? new int[0] : state.AltHeldKeys;
			WriteKeys(AltKeys, ownerHeld);
			HOCGCCAIPFF.PKPJKJFBOGH = false;
			HOCGCCAIPFF.JNPMJANFGNH = mainInput.Mask(state.MainToggles, state.MainToggleKeys, ownerHeld);
			// Native input has consumed this frame's key-down events. Leave its once-per-frame
			// guard alone: Arena.LateUpdate clears it for the following frame, in either script order.
			if (machine.GGEBPLOFDAM != null) machine.GGEBPLOFDAM.Clear();
			for (int i = 0; i < parts.Length; i++)
			{
				parts[i].Restore(state.Parts[i]);
				ThrusterController thruster = parts[i].Component as ThrusterController;
				if (state.Parts[i].Available && thruster != null && parts[i].Scalars.BoostRemaining(thruster) > 0
					&& machine.GGEBPLOFDAM != null) machine.GGEBPLOFDAM.Add(thruster);
			}
			// Invalidate the ordinary decor mask cache. Actual send is after the existing
			// machine-group gate is released, so a suppressed RPC cannot consume this invalidation.
			machine.KNFGGGFAKML = ~HOCGCCAIPFF.JNPMJANFGNH;
			LegacyTransientReconnect.Log("CLIENT_CRASH_CONTROLS_APPLIED schema=2 frame=" + state.Frame
				+ " parts=" + parts.Length + " ownerLayout=" + layoutHash + " mainToggles=" + state.MainToggles.ToString("X16")
				+ " toggleKeys=" + state.MainToggleKeys.Length + "/" + state.AlternateToggleKeys.Length
				+ " ownerAltHeld=" + ownerHeld.Length + " restoredMask=" + HOCGCCAIPFF.JNPMJANFGNH.ToString("X16")
				+ " secondaryInput=preserved-new-session physicalKeys=released phases=resume targets=resume apply=once");
		}

		internal string Trace()
		{
			string target = "none";
			for (int i = 0; i < parts.Length; i++)
			{
				JointController joint = parts[i].Component as JointController;
				if (joint == null) continue;
				PistonController piston = joint as PistonController;
				if (piston != null)
				{
					ConfigurableJoint native = PistonJointField.GetValue(piston) as ConfigurableJoint;
					if (native != null) target = "piston:" + native.targetPosition.ToString("F3");
				}
				else if (joint.ALGHGLCBLDJ != null) target = "hinge:" + joint.ALGHGLCBLDJ.spring.targetPosition.ToString("F3", CultureInfo.InvariantCulture);
				break;
			}
			string action = "none";
			for (int i = 0; i < parts.Length; i++)
				if (parts[i].ActionTrace != null) { action = parts[i].ActionTrace; break; }
			return " frame=" + Time.frameCount + " fixedTime=" + Time.fixedTime.ToString("F3", CultureInfo.InvariantCulture)
				+ " action=" + action + " mainToggles=" + ReadToggles(MainToggles).ToString("X16") + " alternateToggles="
				+ ReadToggles(AlternateToggles).ToString("X16") + " mask=" + HOCGCCAIPFF.JNPMJANFGNH.ToString("X16")
				+ " toggleKeys=" + ((List<KeyCode>)MainKeys.GetValue(null)).Count + "/" + ((List<KeyCode>)AlternateKeys.GetValue(null)).Count
				+ " altHeld=" + ((List<KeyCode>)AltKeys.GetValue(null)).Count + " target=" + target;
		}

		private sealed class InputLayout
		{
			internal readonly int Mode;
			private readonly int[] first = new int[62], second = new int[62];
			private readonly int[][] conflicts = new int[62][];
			internal InputLayout(AssignData assign, BuildData build)
			{
				Mode = build == null ? -1 : (int)build.actionProcessing;
				if (assign == null) return;
				List<KeyCode>[] native = new List<KeyCode>[62];
				BuildKeyConflicts.Invoke(null, new object[] { assign, native });
				for (int i = 0; i < 62; i++)
				{
					first[i] = (int)assign.GetKeyCode(i); second[i] = (int)assign.GetKeyCode2(i);
					if (native[i] == null) continue;
					conflicts[i] = new int[native[i].Count];
					for (int j = 0; j < native[i].Count; j++) conflicts[i][j] = (int)native[i][j];
				}
			}
			internal ulong Mask(ulong toggles, int[] keys, int[] alt)
			{
				return LegacyPersistentAssignMask.Build(toggles, first, second, conflicts,
					Mode != (int)BuildData.EMBMFGCDBAM.Mixed, keys, alt);
			}
		}

		private sealed class Part
		{
			internal readonly PartsController Component;
			internal readonly ScalarLayout Scalars;
			internal readonly string Key;
			private readonly PartsController.HILPGPFCIGP[] actions;
			internal string ActionTrace
			{
				get
				{
					if (actions.Length == 0) return null;
					PartsController.HILPGPFCIGP a = actions[0];
					return a.ILHIGOPKKKC + ":phase=" + a.CPHBKJKAJED + ":state=" + a.ENEHEJFCFLC
						+ ":repeat=" + a.FOKGJIMFGGN + ":delay=" + a.DDLJAGKHECF.ToString("F3", CultureInfo.InvariantCulture);
				}
			}
			internal Part(PartsController component)
			{
				Component = component; Scalars = GetScalarLayout(component.GetType());
				BlockData block = component.JNKEKNOAPHO;
				Key = component.GetType().FullName + ":" + component.DLMKKFCHFNC.BBLGKLFBJGE + ":"
					+ (int)block.type + ":" + block.x + ":" + block.y + ":" + block.z + ":" + block.gid + ":" + block.index;
				List<PartsController.HILPGPFCIGP> list = new List<PartsController.HILPGPFCIGP>(
					(List<PartsController.HILPGPFCIGP>)ActionList.GetValue(component));
				PartsController.HILPGPFCIGP neutral = NeutralAction.GetValue(component) as PartsController.HILPGPFCIGP;
				if (neutral != null) list.Add(neutral);
				actions = list.ToArray();
			}
			internal void WriteLayout(BinaryWriter writer)
			{
				writer.Write(Key); Scalars.WriteLayout(writer);
				writer.Write(actions.Length);
				foreach (PartsController.HILPGPFCIGP action in actions)
				{
					writer.Write(action.ILHIGOPKKKC); writer.Write(action.KIJDHMLFCGD); writer.Write(action.EAEDINGGPGN);
					writer.Write(action.NDKIGJNEEHF); writer.Write(action.ELMKIKMMPLJ); writer.Write(action.PHAEDICMIBH);
					foreach (int value in action.KAEEPGIHJCA) writer.Write(value);
				}
			}
			internal LegacyControlPartState Capture()
			{
				LegacyControlPartState state = new LegacyControlPartState();
				state.Available = Component != null;
				if (!state.Available) return state;
				Scalars.Capture(Component, state);
				state.Actions = new LegacyControlActionState[actions.Length];
				for (int i = 0; i < actions.Length; i++)
				{
					PartsController.HILPGPFCIGP a = actions[i];
					state.Actions[i] = new LegacyControlActionState { Id = a.ILHIGOPKKKC, Phase = a.CPHBKJKAJED,
						Delay = a.DDLJAGKHECF, State = a.ENEHEJFCFLC, Repeats = a.FOKGJIMFGGN, Flag = a.HBHDHHEDJEG };
				}
				CaptureJoint(Component, state);
				return state;
			}
			internal void ValidateRestore(LegacyControlPartState state)
			{
				if (!state.Available) return;
				if (Component == null || !(bool)SelfInput.GetValue(Component)
					|| state.Actions.Length != actions.Length || !Scalars.Matches(state)) throw new InvalidOperationException("part-layout-" + Key);
				for (int i = 0; i < actions.Length; i++)
					if (state.Actions[i].Id != actions[i].ILHIGOPKKKC) throw new InvalidOperationException("part-action-" + Key);
				if (state.JointKind == 1 && (!(Component is JointController) || ((JointController)Component).ALGHGLCBLDJ == null))
					throw new InvalidOperationException("hinge-not-ready-" + Key);
				if (state.JointKind == 2 && (!(Component is PistonController) || (ConfigurableJoint)PistonJointField.GetValue(Component) == null))
					throw new InvalidOperationException("piston-not-ready-" + Key);
				if ((state.JointKind == 3 && !(Component is JointController)) || (state.JointKind == 4 && !(Component is PistonController)))
					throw new InvalidOperationException("broken-joint-type-" + Key);
			}
			internal void Restore(LegacyControlPartState state)
			{
				if (!state.Available || Component == null) return;
				Scalars.Restore(Component, state);
				for (int i = 0; i < actions.Length; i++)
				{
					PartsController.HILPGPFCIGP a = actions[i]; LegacyControlActionState s = state.Actions[i];
					a.CPHBKJKAJED = s.Phase; a.DDLJAGKHECF = s.Delay; a.ENEHEJFCFLC = s.State;
					a.FOKGJIMFGGN = s.Repeats; a.HBHDHHEDJEG = s.Flag;
				}
				RestoreJoint(Component, state);
				RestoreLatchedVisual(Component);
			}
		}

		private static FieldInfo Field(Type type, string name)
		{
			FieldInfo field = type.GetField(name, InstanceFields);
			if (field == null) throw new MissingFieldException(type.FullName, name);
			return field;
		}
		private static FieldInfo InputField(string name, Type expected)
		{
			FieldInfo field = typeof(HOCGCCAIPFF).GetField(name, StaticFields);
			if (field == null || field.FieldType != expected) throw new MissingFieldException("HOCGCCAIPFF", name);
			return field;
		}
		private static ulong ReadToggles(FieldInfo field)
		{
			bool[] values = (bool[])field.GetValue(null);
			if (values.Length != 60) throw new InvalidOperationException("toggle-count");
			ulong result = 0; for (int i = 0; i < 60; i++) if (values[i]) result |= 1UL << i;
			return result;
		}
		private static void WriteToggles(FieldInfo field, ulong value)
		{
			bool[] values = (bool[])field.GetValue(null);
			for (int i = 0; i < 60; i++) values[i] = (value & (1UL << i)) != 0;
		}
		private static int[] ReadKeys(FieldInfo field)
		{
			List<KeyCode> keys = (List<KeyCode>)field.GetValue(null);
			List<int> result = new List<int>();
			foreach (KeyCode key in keys) if (key != KeyCode.None && !result.Contains((int)key)) result.Add((int)key);
			result.Sort(); return result.ToArray();
		}
		private static void WriteKeys(FieldInfo field, int[] values)
		{
			List<KeyCode> keys = (List<KeyCode>)field.GetValue(null);
			keys.Clear(); foreach (int value in values) keys.Add((KeyCode)value);
		}

		// Explicit scalar allowlist: no Unity references, absolute Time.time values,
		// peer identity, projectile handles, event subscriptions or random-generator state.
		private static ScalarLayout GetScalarLayout(Type type)
		{
			ScalarLayout result;
			if (ScalarLayouts.TryGetValue(type, out result)) return result;
			List<FieldInfo> fields = new List<FieldInfo>();
			AddFields(fields, typeof(PartsController), "FJHBIDPKFJH BGBFFMOOAFC HHJKNPFCKMN ONGNOMCJBGE OHILPJBFBIB");
			if (typeof(JointController).IsAssignableFrom(type))
				AddFields(fields, typeof(JointController), "GEKMCAJOPNK HCOODEEMDEF OCGBGKBDJPL IMHCLNFACNI OPKPNLOMBDI PLIKIPIIHGL NHCGHMEIOFN BBIBCPGBCNE KAEEPGIHJCA FOFALOOAKKP DCLNENGNEPK ALLMGCLAPBA MCCHHFFNPAH LEGMPNEFFGB PLLPLBHAOBH CLDMLFLKJCA OOJOMGAEBHP");
			if (typeof(PistonController).IsAssignableFrom(type))
				AddFields(fields, typeof(PistonController), "GJICDIEIPPE EADPHECOPIL KCFPIJIEJJE JLCFAFEAOCE HEDEOMBHCCI MECNMMIDEOL DGONENHKLJM OLJKMIFPKIH EPDHAAIIKCF");
			if (typeof(WheelController).IsAssignableFrom(type)) AddFields(fields, typeof(WheelController), "PCNMHPJCHDJ");
			if (typeof(ThrusterController).IsAssignableFrom(type)) AddFields(fields, typeof(ThrusterController), "CDEFLDILHKA PGMCIONEHOJ KIDJPPBKPHP ALLLDLMEIKM");
			if (typeof(AGDeviceController).IsAssignableFrom(type)) AddFields(fields, typeof(AGDeviceController), "DHFJNLCFCON");
			if (typeof(MoverController).IsAssignableFrom(type)) AddFields(fields, typeof(MoverController), "DHFJNLCFCON");
			if (typeof(SledController).IsAssignableFrom(type)) AddFields(fields, typeof(SledController), "EOMFNIEDKDF ANBNKOMGEEE NFKJOKJJMMM");
			if (typeof(LampController).IsAssignableFrom(type)) AddFields(fields, typeof(LampController), "EOMFNIEDKDF");
			if (typeof(EmitterController).IsAssignableFrom(type)) AddFields(fields, typeof(EmitterController), "EOMFNIEDKDF");
			if (typeof(CannonController).IsAssignableFrom(type)) AddFields(fields, typeof(CannonController), "PIABCHPMJHI OGAFKPOMPJE JEENNHEACNO");
			if (typeof(LauncherController).IsAssignableFrom(type)) AddFields(fields, typeof(LauncherController), "DHFJNLCFCON DCAEFEJIFHO IPNIPHGBNJE LPLCNHLCKND");
			if (typeof(DischargerController).IsAssignableFrom(type)) AddFields(fields, typeof(DischargerController), "DNBKENONLFD OHOGEAFHOFH NHJLNEDKGIH NEDLPPMOKEI GEEHCOMGJBK");
			if (typeof(BeamerController).IsAssignableFrom(type)) AddFields(fields, typeof(BeamerController), "BGMBIONPKED HNKHHKMMELJ");
			if (typeof(SwordController).IsAssignableFrom(type)) AddFields(fields, typeof(SwordController), "MNHFHJEHDCN LOLOLOKJOCL");
			result = new ScalarLayout(fields); ScalarLayouts.Add(type, result); return result;
		}
		private static void AddFields(List<FieldInfo> fields, Type type, string names)
		{
			foreach (string name in names.Split(' ')) fields.Add(Field(type, name));
		}
		private sealed class Accessor<T>
		{
			internal readonly Func<object, T> Read;
			internal readonly Action<object, T> Write;
			internal readonly string Name;
			internal Accessor(FieldInfo field)
			{
				Name = field.DeclaringType.FullName + "." + field.Name;
				DynamicMethod getter = new DynamicMethod("control_get_" + field.Name, typeof(T), new Type[] { typeof(object) }, typeof(LegacyMachineControlRecovery), true);
				ILGenerator il = getter.GetILGenerator(); il.Emit(OpCodes.Ldarg_0); il.Emit(OpCodes.Castclass, field.DeclaringType);
				il.Emit(OpCodes.Ldfld, field); il.Emit(OpCodes.Ret);
				Read = (Func<object, T>)getter.CreateDelegate(typeof(Func<object, T>));
				DynamicMethod setter = new DynamicMethod("control_set_" + field.Name, typeof(void), new Type[] { typeof(object), typeof(T) }, typeof(LegacyMachineControlRecovery), true);
				il = setter.GetILGenerator(); il.Emit(OpCodes.Ldarg_0); il.Emit(OpCodes.Castclass, field.DeclaringType);
				il.Emit(OpCodes.Ldarg_1); il.Emit(OpCodes.Stfld, field); il.Emit(OpCodes.Ret);
				Write = (Action<object, T>)setter.CreateDelegate(typeof(Action<object, T>));
			}
		}
		private sealed class ScalarLayout
		{
			private readonly List<Accessor<float>> floats = new List<Accessor<float>>();
			private readonly List<Accessor<int>> integers = new List<Accessor<int>>();
			private readonly List<Accessor<bool>> flags = new List<Accessor<bool>>();
			internal ScalarLayout(List<FieldInfo> fields)
			{
				foreach (FieldInfo field in fields)
					if (field.FieldType == typeof(float)) floats.Add(new Accessor<float>(field));
					else if (field.FieldType == typeof(int)) integers.Add(new Accessor<int>(field));
					else if (field.FieldType == typeof(bool)) flags.Add(new Accessor<bool>(field));
					else throw new InvalidOperationException("non-scalar-control-field");
			}
			internal void WriteLayout(BinaryWriter writer)
			{
				writer.Write(floats.Count); foreach (Accessor<float> a in floats) writer.Write(a.Name);
				writer.Write(integers.Count); foreach (Accessor<int> a in integers) writer.Write(a.Name);
				writer.Write(flags.Count); foreach (Accessor<bool> a in flags) writer.Write(a.Name);
			}
			internal bool Matches(LegacyControlPartState state)
			{
				return state.Floats.Length == floats.Count && state.Integers.Length == integers.Count && (state.Flags >> flags.Count) == 0;
			}
			internal void Capture(object instance, LegacyControlPartState state)
			{
				state.Floats = new float[floats.Count]; state.Integers = new int[integers.Count];
				for (int i = 0; i < floats.Count; i++) state.Floats[i] = floats[i].Read(instance);
				for (int i = 0; i < integers.Count; i++) state.Integers[i] = integers[i].Read(instance);
				for (int i = 0; i < flags.Count; i++) if (flags[i].Read(instance)) state.Flags |= 1UL << i;
			}
			internal void Restore(object instance, LegacyControlPartState state)
			{
				for (int i = 0; i < floats.Count; i++) floats[i].Write(instance, state.Floats[i]);
				for (int i = 0; i < integers.Count; i++) integers[i].Write(instance, state.Integers[i]);
				for (int i = 0; i < flags.Count; i++) flags[i].Write(instance, (state.Flags & (1UL << i)) != 0);
			}
			internal int BoostRemaining(object instance)
			{
				foreach (Accessor<int> a in integers) if (a.Name == "ThrusterController.PGMCIONEHOJ") return a.Read(instance);
				return 0;
			}
		}

		private static void CaptureJoint(PartsController part, LegacyControlPartState state)
		{
			state.JointValues = new float[0];
			PistonController piston = part as PistonController;
			if (piston != null)
			{
				ConfigurableJoint joint = (ConfigurableJoint)PistonJointField.GetValue(piston);
				if (joint == null) { state.JointKind = 4; return; }
				state.JointKind = 2;
				state.JointFlags = (int)joint.xMotion | ((int)joint.yMotion << 2) | ((int)joint.zMotion << 4);
				List<float> v = new List<float>();
				Vector3 p = joint.targetPosition; v.Add(p.x); v.Add(p.y); v.Add(p.z);
				AddDrive(v, joint.xDrive); AddDrive(v, joint.yDrive); AddDrive(v, joint.zDrive);
				AddDrive(v, (JointDrive)PistonDriveField.GetValue(piston));
				SoftJointLimit limit = (SoftJointLimit)PistonLimitField.GetValue(piston);
				v.Add(limit.limit); v.Add(limit.bounciness); v.Add(limit.contactDistance); v.Add(joint.linearLimit.limit);
				state.JointValues = v.ToArray(); return;
			}
			JointController hinge = part as JointController;
			if (hinge == null) return;
			if (hinge.ALGHGLCBLDJ == null) { state.JointKind = 3; return; }
			HingeJoint native = hinge.ALGHGLCBLDJ;
			state.JointKind = 1;
			state.JointFlags = (native.useSpring ? 1 : 0) | (native.useLimits ? 2 : 0) | (native.useMotor ? 4 : 0);
			List<float> values = new List<float>();
			AddSpring(values, (JointSpring)JointSpringField.GetValue(hinge)); AddSpring(values, native.spring);
			AddLimits(values, (JointLimits)JointLimitsField.GetValue(hinge));
			AddLimits(values, (JointLimits)JointOtherLimitsField.GetValue(hinge)); AddLimits(values, native.limits);
			JointMotor motor = native.motor; values.Add(motor.targetVelocity); values.Add(motor.force); values.Add(motor.freeSpin ? 1f : 0f);
			// Reserved slots keep the v1 layout fixed when native control diagnostics grow.
			values.Add(0f); values.Add(0f);
			state.JointValues = values.ToArray();
		}
		private static void RestoreJoint(PartsController part, LegacyControlPartState state)
		{
			float[] v = state.JointValues;
			if (state.JointKind == 3 || state.JointKind == 4)
			{
				((JointController)part).BreakJoint();
				return;
			}
			if (state.JointKind == 2)
			{
				ConfigurableJoint joint = (ConfigurableJoint)PistonJointField.GetValue(part);
				// Damage restoration may have removed the joint after preflight. Never recreate it.
				if (joint == null) return;
				joint.targetPosition = new Vector3(v[0], v[1], v[2]);
				joint.xDrive = Drive(v, 3); joint.yDrive = Drive(v, 6); joint.zDrive = Drive(v, 9);
				PistonDriveField.SetValue(part, Drive(v, 12));
				SoftJointLimit cached = new SoftJointLimit(); cached.limit = v[15]; cached.bounciness = v[16]; cached.contactDistance = v[17];
				PistonLimitField.SetValue(part, cached); cached.limit = v[18]; joint.linearLimit = cached;
				joint.xMotion = (ConfigurableJointMotion)(state.JointFlags & 3);
				joint.yMotion = (ConfigurableJointMotion)((state.JointFlags >> 2) & 3);
				joint.zMotion = (ConfigurableJointMotion)((state.JointFlags >> 4) & 3);
			}
			else if (state.JointKind == 1)
			{
				JointController hinge = (JointController)part; HingeJoint joint = hinge.ALGHGLCBLDJ;
				if (joint == null) return;
				JointSpringField.SetValue(part, Spring(v, 0)); joint.spring = Spring(v, 3);
				JointLimitsField.SetValue(part, Limits(v, 6)); JointOtherLimitsField.SetValue(part, Limits(v, 11));
				joint.limits = Limits(v, 16);
				JointMotor motor = new JointMotor(); motor.targetVelocity = v[21]; motor.force = v[22]; motor.freeSpin = v[23] != 0f;
				joint.motor = motor;
				joint.useSpring = (state.JointFlags & 1) != 0; joint.useLimits = (state.JointFlags & 2) != 0; joint.useMotor = (state.JointFlags & 4) != 0;
			}
		}
		private static void RestoreLatchedVisual(PartsController part)
		{
			// A released Assign can leave the last hue/emission latched. Updating only
			// EOMFNIEDKDF would suppress the game's next visual refresh as "unchanged".
			if (part.DLMKKFCHFNC.IsBroken() || part.DLMKKFCHFNC.EINNGJBAMAP) return;
			LampController lamp = part as LampController;
			if (lamp != null)
			{
				GameObject bulb = LampBulb.GetValue(lamp) as GameObject;
				float command = (float)LampCommand.GetValue(lamp);
				if (bulb != null && command > -9000f)
				{
					BulbController controller = bulb.GetComponent<BulbController>();
					if (controller != null) controller.ApplyHue(command < 24.5f ? command / 24f : -1f);
				}
			}
			EmitterController emitter = part as EmitterController;
			if (emitter != null)
			{
				SmokeController smoke = EmitterSmoke.GetValue(emitter) as SmokeController;
				if (smoke != null) smoke.MGHHBHLDBHN = (float)EmitterCommand.GetValue(emitter);
			}
		}
		private static void AddSpring(List<float> v, JointSpring s) { v.Add(s.spring); v.Add(s.damper); v.Add(s.targetPosition); }
		private static JointSpring Spring(float[] v, int i) { JointSpring s = new JointSpring(); s.spring = v[i]; s.damper = v[i + 1]; s.targetPosition = v[i + 2]; return s; }
		private static void AddDrive(List<float> v, JointDrive d) { v.Add(d.positionSpring); v.Add(d.positionDamper); v.Add(d.maximumForce); }
		private static JointDrive Drive(float[] v, int i) { JointDrive d = new JointDrive(); d.positionSpring = v[i]; d.positionDamper = v[i + 1]; d.maximumForce = v[i + 2]; return d; }
		private static void AddLimits(List<float> v, JointLimits l) { v.Add(l.min); v.Add(l.max); v.Add(l.bounciness); v.Add(l.bounceMinVelocity); v.Add(l.contactDistance); }
		private static JointLimits Limits(float[] v, int i) { JointLimits l = new JointLimits(); l.min = v[i]; l.max = v[i + 1]; l.bounciness = v[i + 2]; l.bounceMinVelocity = v[i + 3]; l.contactDistance = v[i + 4]; return l; }
	}
}
