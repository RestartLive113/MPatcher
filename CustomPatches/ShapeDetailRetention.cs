using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using MPatchrMain;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
    // Only Scale writes this optional lane. Native editing without it keeps the
    // game's size-based template and secondary low-mesh choices.
    internal static class ShapeDetailRetention
    {
        internal const string PropertyName = "shapeDetail";
        private const string PatchId = "local.moddev.machinecraft.shape-detail-retention.v1";
        private static bool registered;

        internal static void TryRegister()
        {
            if (registered) return;
            Harmony patcher = new Harmony(PatchId);
            try
            {
                HarmonyMethod bucket = new HarmonyMethod(typeof(ShapeDetailRetention), "BucketTranspiler");
                HarmonyMethod low = new HarmonyMethod(typeof(ShapeDetailRetention), "LowMeshTranspiler");
                bucket.priority = Priority.Last;
                low.priority = Priority.Last;
                patcher.Patch(Required(typeof(BoxGenController), "MakeBox"), null, null, bucket, null);
                patcher.Patch(Required(typeof(CapGenController), "MakeCapsule"), null, null, bucket, null);
                patcher.Patch(Required(typeof(BoxGenController), "SetLowMesh"), null, null, low, null);
                patcher.Patch(Required(typeof(CapGenController), "SetLowMesh"), null, null, low, null);
                registered = true;
                Log("REGISTERED version=1 targets=MakeBox,MakeCapsule,Box.SetLowMesh,Cap.SetLowMesh property="
                    + PropertyName + " writer=Scale-shrink-only floors=1..2/capsuleFamilyFloor0 boxCleanup=preserved");
            }
            catch (Exception error)
            {
                try { patcher.UnpatchAll(PatchId); }
                catch (Exception rollback) { Log("ROLLBACK_FAILED " + rollback); }
                registered = false;
                Log("REGISTER_FAILED " + error);
            }
        }

        private static MethodInfo Required(Type type, string name)
        {
            MethodInfo result = AccessTools.Method(type, name);
            if (result == null) throw new MissingMethodException(type.FullName, name);
            return result;
        }

        private static string Kind(BlockData block)
        {
            if (block == null) return null;
            return block.type == BlockData.AAHMDBHDCDK.BoxGen ? "B"
                : block.type == BlockData.AAHMDBHDCDK.CapGen ? "C" : null;
        }

        internal static int NativeBucket(BlockData block)
        {
            string kind = Kind(block);
            if (kind == null || block.actionParam == null || block.actionParam.Length < 3) return -1;
            if (kind == "B" && (block.actionID == null || block.actionID.Length == 0 || block.actionID[0] > 0)) return -1;
            float size = SetupPrecisionData.Read(block, 0);
            if (kind == "B") size = Math.Max(Math.Min(size, SetupPrecisionData.Read(block, 1)), SetupPrecisionData.Read(block, 2));
            if (float.IsNaN(size) || float.IsInfinity(size) || size < 0f) return -1;
            return kind == "B" ? (size > 50f ? 2 : size > 10f ? 1 : 0)
                : (size > 50.1f ? 2 : size > 10.1f ? 1 : 0);
        }

        private static bool TryReadMinimum(BlockData block, out int minimum)
        {
            minimum = 0;
            string kind = Kind(block);
            object raw;
            if (kind == null || block.props == null || !block.props.TryGetValue(PropertyName, out raw)) return false;
            string text = raw as string;
            if (text == "1|" + kind + "|1") minimum = 1;
            else if (text == "1|" + kind + "|2") minimum = 2;
            else if (kind == "C" && text == "2|C|0|0") minimum = 0;
            else if (kind == "C" && text == "2|C|1|0") minimum = 1;
            else if (kind == "C" && text == "2|C|2|0") minimum = 2;
            else return false;
            return true;
        }

        private static bool HasCapsuleFamily(BlockData block)
        {
            int minimum;
            object raw;
            return Kind(block) == "C" && block.actionParam != null && block.actionParam.Length > 2
                && block.actionParam[2] == 0 && TryReadMinimum(block, out minimum)
                && block.props.TryGetValue(PropertyName, out raw) && ((string)raw).StartsWith("2|C|", StringComparison.Ordinal);
        }

        internal static int RetainedCapsuleKind(int nativeKind, BlockData block)
        {
            return nativeKind == 99 && HasCapsuleFamily(block) ? 0 : nativeKind;
        }

        internal static int ReadMinimum(BlockData block)
        {
            int minimum;
            return TryReadMinimum(block, out minimum) ? minimum : 0;
        }

        internal static bool HasRetainedDetail(BlockData block)
        {
            int minimum;
            return NativeBucket(block) >= 0 && TryReadMinimum(block, out minimum);
        }

        internal static int EffectiveBucket(BlockData block)
        {
            int native = NativeBucket(block);
            return native < 0 ? native : Math.Max(native, ReadMinimum(block));
        }

        internal static int ClampBucket(int native, BlockData block)
        {
            return native < 0 || native > 2 || NativeBucket(block) < 0 ? native : Math.Max(native, ReadMinimum(block));
        }

        internal static bool RetainForScaleShrink(BlockData target, BlockData source, float coefficient)
        {
            if (float.IsNaN(coefficient) || float.IsInfinity(coefficient) || coefficient <= 0f || coefficient >= 1f
                || Kind(source) == null || Kind(source) != Kind(target) || NativeBucket(target) < 0) return false;
            // Compute before cloning/writing props; source and target may be the same object.
            int wanted = EffectiveBucket(source);
            bool capsuleFamily = Kind(source) == "C" && source.actionParam[2] == 0
                && target.actionParam[2] == 0 && (SetupPrecisionData.Read(source, 1) > 0f || HasCapsuleFamily(source));
            if (wanted < 0 || wanted > 2 || wanted == 0 && !capsuleFamily) return false;
            int floor = Math.Max(wanted, ReadMinimum(target));
            if (ReadMinimum(target) >= wanted && (!capsuleFamily || HasCapsuleFamily(target))) return false;
            Dictionary<string, object> props = target.props == null ? new Dictionary<string, object>()
                : new Dictionary<string, object>(target.props);
            props[PropertyName] = capsuleFamily ? "2|C|" + floor.ToString(CultureInfo.InvariantCulture) + "|0"
                : "1|" + Kind(target) + "|" + floor.ToString(CultureInfo.InvariantCulture);
            target.props = props;
            Log("RETAINED type=" + target.type + " block=" + target.GetPosString(false)
                + " minimum=" + floor + " capsuleFamily=" + capsuleFamily
                + " coefficient=" + coefficient.ToString("0.###", CultureInfo.InvariantCulture));
            return true;
        }

        private static List<CodeInstruction> Copy(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> result = new List<CodeInstruction>();
            foreach (CodeInstruction instruction in instructions) result.Add(new CodeInstruction(instruction));
            return result;
        }

        private static bool Field(CodeInstruction instruction, Type type, string name)
        {
            FieldInfo field = instruction.operand as FieldInfo;
            return instruction.opcode == OpCodes.Ldfld && field != null && field.DeclaringType == type && field.Name == name;
        }

        private static bool LocalLoad(CodeInstruction instruction)
        {
            return instruction.opcode.Name.StartsWith("ldloc", StringComparison.Ordinal)
                && !instruction.opcode.Name.StartsWith("ldloca", StringComparison.Ordinal);
        }

        private static CodeInstruction StoreFor(CodeInstruction load)
        {
            if (load.opcode == OpCodes.Ldloc_0) return new CodeInstruction(OpCodes.Stloc_0);
            if (load.opcode == OpCodes.Ldloc_1) return new CodeInstruction(OpCodes.Stloc_1);
            if (load.opcode == OpCodes.Ldloc_2) return new CodeInstruction(OpCodes.Stloc_2);
            if (load.opcode == OpCodes.Ldloc_3) return new CodeInstruction(OpCodes.Stloc_3);
            if (load.opcode == OpCodes.Ldloc_S) return new CodeInstruction(OpCodes.Stloc_S, load.operand);
            if (load.opcode == OpCodes.Ldloc) return new CodeInstruction(OpCodes.Stloc, load.operand);
            throw new InvalidOperationException("Unsupported native shape bucket local.");
        }

        private static IEnumerable<CodeInstruction> BucketTranspiler(IEnumerable<CodeInstruction> instructions, MethodBase __originalMethod)
        {
            List<CodeInstruction> codes = Copy(instructions);
            if (__originalMethod.DeclaringType == typeof(BoxGenController) && __originalMethod.Name == "MakeBox")
            {
                int match = -1;
                for (int i = 0; i + 2 < codes.Count; i++)
                    if (Field(codes[i], typeof(BoxGenController), "EADJPJNLLHP") && LocalLoad(codes[i + 1])
                        && codes[i + 2].opcode == OpCodes.Ldelem_Ref)
                    {
                        if (match >= 0) throw new InvalidOperationException("Multiple native Box detail selectors.");
                        match = i + 2;
                    }
                if (match < 0) throw new InvalidOperationException("Native Box detail selector changed.");
                // Stack already holds the prefab array and native bucket index.
                CodeInstruction boxFirst = new CodeInstruction(OpCodes.Ldarg_0);
                boxFirst.labels.AddRange(codes[match].labels); codes[match].labels.Clear();
                boxFirst.blocks.AddRange(codes[match].blocks); codes[match].blocks.Clear();
                codes.InsertRange(match, new[] { boxFirst,
                    new CodeInstruction(OpCodes.Ldfld, AccessTools.Field(typeof(BlockController), "JNKEKNOAPHO")),
                    new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(ShapeDetailRetention), "ClampBucket")) });
                return codes;
            }
            if (__originalMethod.DeclaringType != typeof(CapGenController) || __originalMethod.Name != "MakeCapsule")
                throw new InvalidOperationException("Unsupported native detail target.");
            int typeRead = -1;
            for (int i = 1; i + 2 < codes.Count; i++)
                if (Field(codes[i], typeof(BlockData), "actionParam") && codes[i + 1].opcode == OpCodes.Ldc_I4_2
                    && codes[i + 2].opcode == OpCodes.Ldelem_I4)
                { typeRead = i; break; }
            if (typeRead < 3 || codes[typeRead - 2].opcode != OpCodes.Ldarg_0
                || !Field(codes[typeRead - 1], typeof(BlockController), "JNKEKNOAPHO"))
                throw new InvalidOperationException("Native Capsule kind boundary changed.");
            int anchor = typeRead - 2;
            CodeInstruction bucketLoad = null;
            CodeInstruction kindLoad = null;
            int cacheAnchor = -1;
            int candidates = 0;
            for (int i = 0; i + 1 < anchor; i++)
                if (LocalLoad(codes[i]) && Field(codes[i + 1], typeof(CapGenController), "BBOJAFJKJHM"))
                    throw new InvalidOperationException("Unexpected Capsule bucket cache before kind boundary.");
            for (int i = anchor; i + 2 < codes.Count; i++)
                if (LocalLoad(codes[i]) && codes[i + 1].opcode == OpCodes.Ldarg_0
                    && Field(codes[i + 2], typeof(CapGenController), "BBOJAFJKJHM"))
                { bucketLoad = new CodeInstruction(codes[i].opcode, codes[i].operand); cacheAnchor = i; candidates++; }
            for (int i = anchor; i + 2 < codes.Count; i++)
                if (LocalLoad(codes[i]) && codes[i + 1].opcode == OpCodes.Ldarg_0
                    && Field(codes[i + 2], typeof(CapGenController), "CNCFPFCFEON"))
                {
                    if (kindLoad != null) throw new InvalidOperationException("Multiple native Capsule kind caches.");
                    kindLoad = new CodeInstruction(codes[i].opcode, codes[i].operand);
                }
            if (bucketLoad == null || candidates != 1 || kindLoad == null)
                throw new InvalidOperationException("Native Capsule bucket/kind cache changed.");
            // Keep the original Capsule family if positive source height rounded
            // to zero. Width/height math stays native; cache and switch see kind0.
            CodeInstruction kindFirst = new CodeInstruction(kindLoad.opcode, kindLoad.operand);
            kindFirst.labels.AddRange(codes[cacheAnchor].labels); codes[cacheAnchor].labels.Clear();
            kindFirst.blocks.AddRange(codes[cacheAnchor].blocks); codes[cacheAnchor].blocks.Clear();
            codes.InsertRange(cacheAnchor, new[] { kindFirst, new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Ldfld, AccessTools.Field(typeof(BlockController), "JNKEKNOAPHO")),
                new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(ShapeDetailRetention), "RetainedCapsuleKind")), StoreFor(kindLoad) });
            CodeInstruction first = bucketLoad;
            first.labels.AddRange(codes[anchor].labels); codes[anchor].labels.Clear();
            first.blocks.AddRange(codes[anchor].blocks); codes[anchor].blocks.Clear();
            codes.InsertRange(anchor, new[] { first, new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Ldfld, AccessTools.Field(typeof(BlockController), "JNKEKNOAPHO")),
                new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(ShapeDetailRetention), "ClampBucket")), StoreFor(bucketLoad) });
            return codes;
        }

        private static bool GenericGetBlockController(CodeInstruction instruction)
        {
            MethodInfo method = instruction.operand as MethodInfo;
            return method != null && method.Name == "GetComponent" && method.IsGenericMethod
                && method.DeclaringType == typeof(GameObject) && method.GetGenericArguments().Length == 1
                && method.GetGenericArguments()[0] == typeof(BlockController);
        }

        private static IEnumerable<CodeInstruction> LowMeshTranspiler(IEnumerable<CodeInstruction> instructions,
            ILGenerator generator, MethodBase __originalMethod)
        {
            List<CodeInstruction> codes = Copy(instructions);
            if (__originalMethod.Name != "SetLowMesh" || codes.Count == 0 || codes[codes.Count - 1].opcode != OpCodes.Ret)
                throw new InvalidOperationException("Native low-mesh return changed.");
            int target = codes.Count - 1;
            int insert = 0;
            if (__originalMethod.DeclaringType == typeof(BoxGenController))
            {
                // Keep the native null-primitive early return before our guard.
                if (codes.Count < 6 || codes[0].opcode != OpCodes.Ldarg_0
                    || !Field(codes[1], typeof(BoxGenController), "NGLBLAGMBLN") || codes[4].opcode != OpCodes.Ret)
                    throw new InvalidOperationException("Native Box low-mesh null guard changed.");
                insert = 5;
                int cleanup = -1;
                for (int i = 0; i < codes.Count; i++) if (GenericGetBlockController(codes[i])) cleanup = i;
                if (cleanup < 2 || codes[cleanup - 2].opcode != OpCodes.Ldarg_0
                    || !Field(codes[cleanup - 1], typeof(BoxGenController), "NGLBLAGMBLN")
                    || cleanup + 2 != codes.Count - 1)
                    throw new InvalidOperationException("Native Box low-mesh cleanup changed.");
                MethodInfo destroy = codes[cleanup + 1].operand as MethodInfo;
                if (destroy == null || destroy.DeclaringType != typeof(UnityEngine.Object) || destroy.Name != "DestroyImmediate")
                    throw new InvalidOperationException("Native Box low-mesh cleanup no longer destroys its child controller.");
                target = cleanup - 2;
            }
            else if (__originalMethod.DeclaringType != typeof(CapGenController))
                throw new InvalidOperationException("Unsupported native low-mesh target.");
            Label skip = generator.DefineLabel();
            codes[target].labels.Add(skip);
            CodeInstruction first = new CodeInstruction(OpCodes.Ldarg_0);
            first.labels.AddRange(codes[insert].labels); codes[insert].labels.Clear();
            first.blocks.AddRange(codes[insert].blocks); codes[insert].blocks.Clear();
            codes.InsertRange(insert, new[] { first,
                new CodeInstruction(OpCodes.Ldfld, AccessTools.Field(typeof(BlockController), "JNKEKNOAPHO")),
                new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(ShapeDetailRetention), "HasRetainedDetail")),
                new CodeInstruction(OpCodes.Brtrue, skip) });
            return codes;
        }

        private static void Log(string message)
        {
            ScaleRuntimeHost.Log("[SHAPE-DETAIL] " + message);
        }
    }
}
