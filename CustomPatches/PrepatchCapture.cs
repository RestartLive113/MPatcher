using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using HarmonyLib;
using MPatchrMain;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
    // Explicit build fixture only. Collects the real ordered Harmony plans and
    // the persisted Cecil output; it never runs in published builds by default.
    internal static class PrepatchCapture
    {
        private static string directory;
        private static readonly object sync = new object();
        private static int count, failures;
        private static readonly HashSet<string> recorded = new HashSet<string>();
        [ThreadStatic] private static string emittedAssembly, emittedType, emittedMethod;
        private static bool finished;
        internal static void Begin()
        {
            PrepatchRuntime.TryRegister();
            string requested = Environment.GetEnvironmentVariable("MPATCHER_PREPATCH_CAPTURE");
            if (string.IsNullOrEmpty(requested)) return;
            string project = Path.Combine(Environment.GetEnvironmentVariable("USERPROFILE") ?? "", "Documents\\MachineCraftMP\\analysis\\prepatch-all-20260930");
            directory = Path.GetFullPath(requested);
            string root = Path.GetFullPath(Path.Combine(Application.dataPath, "..")).TrimEnd('\\', '/');
            if (!directory.StartsWith(project + "\\", StringComparison.OrdinalIgnoreCase) || !root.StartsWith(project + "\\", StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("Prepatch capture requires isolated project fixture");
            Directory.CreateDirectory(directory);
            Type patcher = typeof(Harmony).Assembly.GetType("HarmonyLib.MethodPatcher", true);
            new Harmony("local.mpatcher.prepatch.capture").Patch(AccessTools.Method(patcher, "CreatePatchedMethod"), null,
                new HarmonyMethod(AccessTools.Method(typeof(PrepatchCapture), "Record")),
                new HarmonyMethod(AccessTools.Method(typeof(PrepatchCapture), "PersistEmitter")), null);

            Log("BEGIN dump=" + directory + " mode=inventory-only");
        }
        private static IEnumerable<CodeInstruction> PersistEmitter(IEnumerable<CodeInstruction> instructions)
        {
            int count = 0;
            foreach(CodeInstruction source in instructions)
            {
                CodeInstruction code = new CodeInstruction(source);
                MethodInfo called = code.operand as MethodInfo;
                if (called != null && called.DeclaringType.FullName == "MonoMod.Utils.DynamicMethodDefinition" && called.Name == "Generate" && called.GetParameters().Length == 0)
                { code.opcode = OpCodes.Call; code.operand = AccessTools.Method(typeof(PrepatchCapture), "EmitPersisted"); count++; }
                yield return code;
            }
            if(count != 1) throw new InvalidOperationException("Unexpected Harmony generation sites: " + count);
        }
        private static MethodInfo EmitPersisted(object dmd)
        {
            const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            object module = dmd.GetType().GetProperty("Module", flags).GetValue(dmd, null);
            object definition = dmd.GetType().GetProperty("Definition", flags).GetValue(dmd, null);
            string name = "Dynamic_MPatchPrecompiled_" + Guid.NewGuid().ToString("N");
            object assembly = module.GetType().GetProperty("Assembly", flags).GetValue(module, null);
            object identity = assembly.GetType().GetProperty("Name", flags).GetValue(assembly, null);
            identity.GetType().GetProperty("Name", flags).SetValue(identity, name, null);
            module.GetType().GetProperty("Name", flags).SetValue(module, name + ".dll", null);
            module.GetType().GetMethod("Write", flags, null, new[] { typeof(string) }, null).Invoke(module, new object[] { Path.Combine(directory, name + ".dll") });
            emittedAssembly = name;
            emittedMethod = (string)definition.GetType().GetProperty("Name", flags).GetValue(definition, null);
            object owner = definition.GetType().GetProperty("DeclaringType", flags).GetValue(definition, null);
            emittedType = (string)owner.GetType().GetProperty("FullName", flags).GetValue(owner, null);
            // Use the original skip-visibility runtime generator during capture.
            // Saved IL is later injected into its owning type with explicit bridges.
            return (MethodInfo)dmd.GetType().GetMethod("Generate", flags, null, Type.EmptyTypes, null).Invoke(dmd, null);
        }
        private static string Identity(MethodBase method)
        {
            return method == null ? "" : method.Module.Assembly.FullName + "|" + method.Module.ModuleVersionId + "|" + method.MetadataToken.ToString("X8") + "|" + method.DeclaringType.FullName + "::" + method;
        }
        private static string Methods(List<MethodInfo> methods)
        {
            List<string> values = new List<string>(); foreach (MethodInfo m in methods) values.Add(Identity(m));
            return string.Join("\n", values.ToArray());
        }
        private static string Encode(string text) { return Convert.ToBase64String(Encoding.UTF8.GetBytes(text ?? "")); }
        private static void Record(MethodBase __0, MethodBase __1, string __2, List<MethodInfo> __3, List<MethodInfo> __4, List<MethodInfo> __5, List<MethodInfo> __6, MethodInfo __result)
        {
            if (directory == null || __result == null || string.IsNullOrEmpty(emittedAssembly)) return;
            try
            {
                string[] plan = { Identity(__0), Identity(__1), Methods(__3), Methods(__4), Methods(__5), Methods(__6) };
                string key;
                using (SHA256 sha = SHA256.Create()) key = BitConverter.ToString(sha.ComputeHash(Encoding.UTF8.GetBytes(string.Join("\n---\n", plan)))).Replace("-", "");
                List<string> fields = new List<string> { key };
                recorded.Add(key);
                foreach (string part in plan) fields.Add(Encode(part));
                fields.Add(Encode(__2)); fields.Add(Encode(emittedAssembly)); fields.Add(Encode(emittedType)); fields.Add(Encode(emittedMethod));
                lock(sync) { File.AppendAllText(Path.Combine(directory, "plans.tsv"), string.Join("\t", fields.ToArray()) + "\n"); count++; }
            }
            catch (Exception error) { failures++; Log("RECORD_FAILED " + error); }
            finally { emittedAssembly = emittedType = emittedMethod = null; }
        }
        private static string Group(MethodInfo method, Dictionary<Type,string> groups)
        {
            for(Type type=method.DeclaringType; type!=null; type=type.DeclaringType) { string group; if(groups.TryGetValue(type,out group)) return group; }
            return null;
        }
        private static void ExpandConfigurations()
        {
            Dictionary<Type,string> groups=new Dictionary<Type,string>();
            foreach(MPatchrMain.patching.Patch item in global::FMpPDgPqT_0024MlkjDbhXAGLgVbz45OJagoxMsHXXVw14C6.ckaPtFGmKTrL9dtatRozMxw)
                foreach(Type type in item.patchClasses) { string previous; groups[type]=groups.TryGetValue(type,out previous) && previous!=item.internalName ? previous+"+"+item.internalName : item.internalName; }
            Assembly harmony=typeof(Harmony).Assembly;
            MethodInfo sort=AccessTools.Method(harmony.GetType("HarmonyLib.PatchFunctions",true),"GetSortedPatchMethods");
            MethodInfo create=AccessTools.Method(harmony.GetType("HarmonyLib.MethodPatcher",true),"CreatePatchedMethod");
            int variants=0, maximum=0;
            foreach(MethodBase original in Harmony.GetAllPatchedMethods().ToArray())
            {
                if(original.Module.Assembly==harmony)continue;
                Patches info=Harmony.GetPatchInfo(original);
                HarmonyLib.Patch[][] lists={info.Prefixes.ToArray(),info.Postfixes.ToArray(),info.Transpilers.ToArray(),info.Finalizers.ToArray()};
                string[] options=lists.SelectMany(x=>x).Select(x=>Group(x.patch,groups)).Where(x=>x!=null).Distinct().OrderBy(x=>x).ToArray();
                if(options.Length==0)continue;
                maximum=Math.Max(maximum,options.Length);
                if(options.Length>12)throw new InvalidOperationException("Too many independent options at "+original);
                Log("CONFIGURATION_TARGET method="+original.DeclaringType.FullName+"."+original.Name+" groups="+string.Join(",",options)+" subsets="+(1<<options.Length));
                for(int mask=0;mask<(1<<options.Length);mask++)
                {
                    List<MethodInfo>[] sorted=new List<MethodInfo>[4];
                    for(int kind=0;kind<4;kind++)
                    {
                        List<HarmonyLib.Patch> selected=new List<HarmonyLib.Patch>();
                        foreach(HarmonyLib.Patch patch in lists[kind]) {string group=Group(patch.patch,groups);int index=group==null?-1:Array.IndexOf(options,group);if(index<0 || (mask&(1<<index))!=0)selected.Add(patch);}
                        sorted[kind]=(List<MethodInfo>)sort.Invoke(null,new object[]{original,selected.ToArray()});
                    }
                    string[] plan={Identity(original),"",Methods(sorted[0]),Methods(sorted[1]),Methods(sorted[2]),Methods(sorted[3])};
                    string key;using(SHA256 sha=SHA256.Create())key=BitConverter.ToString(sha.ComputeHash(Encoding.UTF8.GetBytes(string.Join("\n---\n",plan)))).Replace("-","");
                    if(recorded.Contains(key))continue;
                    try {create.Invoke(null,new object[]{original,null,"local.mpatcher.prepatch.configuration",sorted[0],sorted[1],sorted[2],sorted[3]});variants++;}
                    catch(Exception error){failures++;Log("CONFIGURATION_FAILED target="+original+" mask="+mask+" error="+error);}
                }
            }
            Log("CONFIGURATIONS_COMPLETE added="+variants+" maxGroupsPerTarget="+maximum);
        }
        internal static void Poll()
        {
            if (directory == null || finished || !File.Exists(Path.Combine(directory, "finish.request"))) return;
            finished = true;
            try
            {
                List<string> inventory = new List<string>();
                foreach (MethodBase method in Harmony.GetAllPatchedMethods()) inventory.Add(Identity(method));
                File.WriteAllLines(Path.Combine(directory, "active-targets.txt"), inventory.ToArray());
                int classes = 0;
                foreach (MPatchrMain.patching.Patch patch in global::FMpPDgPqT_0024MlkjDbhXAGLgVbz45OJagoxMsHXXVw14C6.ckaPtFGmKTrL9dtatRozMxw)
                {
                    Log("CATALOG patch=" + patch.internalName + " enabled=" + patch.patched + " classes=" + patch.patchClasses.Length);
                    foreach (Type type in patch.patchClasses)
                    {
                        classes++;
                        if (global::OhrvFPoRtmqzpOPJ_gdyp43sPzNUY8iJz48Anha3tAYq.j9O0UPvu4fmbYepM3CYIC_0024s.Contains(type)) continue;
                        try { new Harmony("local.mpatcher.prepatch.optional." + patch.internalName).ProcessorForAnnotatedClass(type).Patch(); }
                        catch(Exception error) { failures++; Log("OPTIONAL_FAILED patch=" + patch.internalName + " type=" + type.FullName + " error=" + error); }
                    }
                }
                ExpandConfigurations();
                Log("COMPLETE activeTargets=" + inventory.Count + " catalogClasses=" + classes + " emitted=" + count + " failures=" + failures);
                File.WriteAllText(Path.Combine(directory, "complete.txt"), "activeTargets=" + inventory.Count + "\nclasses=" + classes + "\nemitted=" + count + "\nfailures=" + failures);
            }
            catch(Exception error) { Log("FAILED " + error); }
            Application.Quit();
        }
        private static void Log(string message) { MagnificationDownscale.Log("PREPATCH_CAPTURE " + message); }
    }
}