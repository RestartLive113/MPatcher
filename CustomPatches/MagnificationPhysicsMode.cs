using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using McnCraft;
using MPatchrMain;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
    // The existing structure header uses 10000 + percent. 11000 + percent
    // denotes the same visible size with native physics and the independent
    // MPatcher settings. It is read
    // before the remote structure's rigidbodies and joints are constructed.
    internal static class MagnificationPhysicsMode
    {
        private const int NativeHeaderBase = 10000;
        private const int UnscaledPhysicsHeaderBase = 11000;
        private sealed class Entry { internal MachineController Machine; internal bool Apply; }
        private static readonly Dictionary<int, Entry> modes = new Dictionary<int, Entry>();
        private static FieldInfo readerMachine;

        internal static void Register(Harmony harmony)
        {
            Type iterator = typeof(MachineSerializer).GetNestedType("<MakeStructureNB>c__Iterator0", BindingFlags.NonPublic);
            if (iterator == null) throw new MissingMemberException("MakeStructureNB iterator");
            readerMachine = AccessTools.Field(iterator, "CECOKMDKAND");
            if (readerMachine == null) throw new MissingFieldException(iterator.FullName, "CECOKMDKAND");
            MethodInfo initialize = AccessTools.DeclaredMethod(typeof(MachineController), "Initialize",
                new[] { typeof(string), typeof(BuildData), typeof(AssignData), typeof(Vector3) });
            MethodInfo writer = AccessTools.DeclaredMethod(typeof(MachineSerializer), "SyncStructure", new[] { typeof(bool[]) });
            MethodInfo reader = AccessTools.DeclaredMethod(iterator, "MoveNext");
            if (initialize == null || writer == null || reader == null) throw new MissingMethodException("Magnification physics transport");
            harmony.Patch(initialize, new HarmonyMethod(AccessTools.Method(typeof(MagnificationPhysicsMode), "InitializePrefix")), null, null, null);
            harmony.Patch(writer, null, null, new HarmonyMethod(AccessTools.Method(typeof(MagnificationPhysicsMode), "WriterTranspiler")), null);
            harmony.Patch(reader, null, null, new HarmonyMethod(AccessTools.Method(typeof(MagnificationPhysicsMode), "ReaderTranspiler")), null);
            MagnificationDownscale.Log("PHYSICS_MODE_REGISTERED default=on off=native-magnification+independent-patches identity100=forced-native activation=new-Initialize water=recovered-native-helper recoil=recovered-native-helper storage=machine-mcpd wire=structure-header peers=require-patch");
        }

        internal static bool IsIdentityPercent(int percent) { return percent == 100; }
        internal static bool EffectiveApply(int percent, bool requested)
        {
            // Legacy zero/negative values are restored by the game as 100%.
            return requested && percent > 0 && !IsIdentityPercent(percent);
        }

        private static void InitializePrefix(MachineController __instance, string __0, BuildData __1)
        {
            if (__instance == null || __0 != "self") return;
            mcpd data = MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4;
            int percent = __1 == null ? 100 : __1.magnification;
            Set(__instance, EffectiveApply(percent, data == null || data.magnificationPhysics), "owner", percent);
        }

        internal static float PhysicsScale(MachineController machine)
        {
            return Applies(machine) ? MagnificationPhysicsNormalization.MachineScale(machine) : 1f;
        }

        internal static bool Applies(MachineController machine)
        {
            // The existing isolated native-control probe intentionally does not
            // register the physics mode. Its generated recovered helpers must
            // also take their original branch, not the research conversions.
            if (MagnificationCraftProbe.NativeControl) return false;
            if (machine == null) return true;
            Entry entry;
            if (modes.TryGetValue(machine.GetInstanceID(), out entry)
                && ReferenceEquals(entry.Machine, machine)) return entry.Apply;
            // The owner/remote request is authoritative during construction:
            // DJCDFLDHPHK still contains the previous scale in InitializePrefix.
            return machine.DJCDFLDHPHK != 1f;
        }

        internal static bool IsVanilla(MachineController machine) { return !Applies(machine); }

        private static void Set(MachineController machine, bool apply, string source, int percent)
        {
            if (machine == null) return;
            apply = EffectiveApply(percent, apply);
            int id = machine.GetInstanceID();
            Entry previous;
            bool changed = !modes.TryGetValue(id, out previous) || !ReferenceEquals(previous.Machine, machine) || previous.Apply != apply;
            // Keep ON requests too, so a new 50% craft whose pre-initialization
            // float is still 1 does not accidentally take the native path.
            modes[id] = new Entry { Machine = machine, Apply = apply };
            if (machine.GetComponent<MagnificationPhysicsModeCleanup>() == null)
                machine.gameObject.AddComponent<MagnificationPhysicsModeCleanup>();
            if (changed) MagnificationDownscale.Log("PHYSICS_MODE machine=" + id + " apply=" + apply
                + " source=" + source + " profile=" + (apply ? "R68c" : "native-magnification+independent-patches")
                + " requestedPercent=" + percent + " identity100=" + IsIdentityPercent(percent)
                + " activation=Initialize preInitializeScale=" + MagnificationPhysicsNormalization.MachineScale(machine).ToString("R", CultureInfo.InvariantCulture));
        }

        internal static void Forget(MachineController machine)
        {
            if (ReferenceEquals(machine, null)) return;
            int id = machine.GetInstanceID();
            Entry entry;
            if (modes.TryGetValue(id, out entry) && ReferenceEquals(entry.Machine, machine)) modes.Remove(id);
        }

        private static int EncodeHeader(int percent, int baseHeader, MachineSerializer serializer)
        {
            if (baseHeader != NativeHeaderBase || percent < MagnificationDownscale.MinimumPercent
                || percent > MagnificationDownscale.MaximumPercent) return baseHeader + percent;
            MachineController machine = serializer == null ? null : serializer.GetComponent<MachineController>();
            bool requested = Applies(machine);
            int result = EncodeMarker(percent, requested);
            if (machine != null) MagnificationDownscale.Log("PHYSICS_WIRE_SEND machine=" + machine.GetInstanceID()
                + " percent=" + percent + " apply=" + EffectiveApply(percent, requested)
                + " requested=" + requested + " marker=" + result);
            return result;
        }

        private static int DecodeHeader(int marker, int baseHeader, object iterator)
        {
            if (baseHeader != NativeHeaderBase) return marker - baseHeader;
            bool encodedOff = IsOffMarker(marker);
            int percent = DecodePercent(marker);
            MachineController machine = readerMachine.GetValue(iterator) as MachineController;
            bool apply = EffectiveApply(percent, !encodedOff);
            Set(machine, apply, "remote-header", percent);
            MagnificationDownscale.Log("PHYSICS_WIRE_RECEIVE machine=" + (machine == null ? 0 : machine.GetInstanceID())
                + " percent=" + percent + " apply=" + apply + " encodedApply=" + !encodedOff + " marker=" + marker);
            return percent;
        }

        internal static int EncodeMarker(int percent, bool apply)
        {
            return (EffectiveApply(percent, apply) || percent < MagnificationDownscale.MinimumPercent || percent > MagnificationDownscale.MaximumPercent
                ? NativeHeaderBase : UnscaledPhysicsHeaderBase) + percent;
        }

        internal static bool IsOffMarker(int marker)
        {
            return marker >= UnscaledPhysicsHeaderBase + MagnificationDownscale.MinimumPercent
                && marker <= UnscaledPhysicsHeaderBase + MagnificationDownscale.MaximumPercent;
        }

        internal static int DecodePercent(int marker)
        {
            return marker - (IsOffMarker(marker) ? UnscaledPhysicsHeaderBase : NativeHeaderBase);
        }

        private static IEnumerable<CodeInstruction> WriterTranspiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> codes = MagnificationDownscale.Copy(instructions);
            int found = 0;
            for (int i = codes.Count - 4; i >= 1; i--)
            {
                if (!Integer(codes[i], NativeHeaderBase) || codes[i + 1].opcode != OpCodes.Add
                    || !Integer(codes[i + 2], 16) || codes[i + 3].opcode != OpCodes.Shl) continue;
                codes.Insert(i + 1, new CodeInstruction(OpCodes.Ldarg_0));
                codes[i + 2].opcode = OpCodes.Call;
                codes[i + 2].operand = AccessTools.Method(typeof(MagnificationPhysicsMode), "EncodeHeader");
                found++;
            }
            MagnificationDownscale.Require(found == 1, "one SyncStructure magnification header", found);
            return codes;
        }

        private static IEnumerable<CodeInstruction> ReaderTranspiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> codes = MagnificationDownscale.Copy(instructions);
            int found = 0;
            for (int i = codes.Count - 2; i >= 1; i--)
            {
                if (!MagnificationDownscale.Field(codes[i - 1], OpCodes.Ldfld, typeof(NMLMDCCDFPN), "CDCGCGDAOEO")
                    || !Integer(codes[i], NativeHeaderBase) || codes[i + 1].opcode != OpCodes.Sub) continue;
                codes.Insert(i + 1, new CodeInstruction(OpCodes.Ldarg_0));
                codes[i + 2].opcode = OpCodes.Call;
                codes[i + 2].operand = AccessTools.Method(typeof(MagnificationPhysicsMode), "DecodeHeader");
                found++;
            }
            MagnificationDownscale.Require(found == 1, "one MakeStructureNB magnification decoder", found);
            return codes;
        }

        private static bool Integer(CodeInstruction code, int value)
        {
            return (code.opcode == OpCodes.Ldc_I4 || code.opcode == OpCodes.Ldc_I4_S)
                && Convert.ToInt32(code.operand) == value;
        }
    }

    internal sealed class MagnificationPhysicsModeCleanup : MonoBehaviour
    {
        private void OnDestroy() { MagnificationPhysicsMode.Forget(GetComponent<MachineController>()); }
    }
}
