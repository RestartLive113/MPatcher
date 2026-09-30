using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using HarmonyLib;

namespace MPatcherFork.CustomPatches
{
    // The manifest is produced together with the exact precompiled assembly set.
    // Missing plans use the original Harmony path, including settings changes.
    internal static class PrepatchRuntime
    {
        private sealed class Entry { internal string Assembly, Type, Method; internal MethodInfo Resolved; }
        private static readonly Dictionary<string, Entry> entries = new Dictionary<string, Entry>();
        private static MethodInfo markNoInlining;
        private static int hits, misses;
        internal static void TryRegister()
        {
            Stream stream = typeof(PrepatchRuntime).Assembly.GetManifestResourceStream("MPatcher.Prepatch.Plans.tsv");
            if (stream == null) return;
            using (StreamReader reader = new StreamReader(stream))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] fields = line.Split('\t');
                    if (fields.Length != 4) throw new InvalidDataException("Invalid prepatch manifest");
                    entries.Add(fields[0], new Entry { Assembly = Decode(fields[1]), Type = Decode(fields[2]), Method = Decode(fields[3]) });
                }
            }
            Assembly harmony = typeof(Harmony).Assembly;
            markNoInlining = AccessTools.Method(harmony.GetType("HarmonyLib.Memory", true), "MarkForNoInlining");
            new Harmony("local.mpatcher.precompiled.v1").Patch(AccessTools.Method(harmony.GetType("HarmonyLib.MethodPatcher", true), "CreatePatchedMethod"),
                new HarmonyMethod(AccessTools.Method(typeof(PrepatchRuntime), "Resolve")), null, null, null);
            Log("REGISTERED plans=" + entries.Count + " mode=precompiled-il fallback=unseen-plan");
        }
        private static bool Resolve(MethodBase __0, MethodBase __1, string __2, List<MethodInfo> __3, List<MethodInfo> __4, List<MethodInfo> __5, List<MethodInfo> __6, ref MethodInfo __result)
        {
            string[] plan = { Identity(__0), Identity(__1), Methods(__3), Methods(__4), Methods(__5), Methods(__6) };
            string key;
            using (SHA256 sha = SHA256.Create()) key = BitConverter.ToString(sha.ComputeHash(Encoding.UTF8.GetBytes(string.Join("\n---\n", plan)))).Replace("-", "");
            Entry entry;
            if (!entries.TryGetValue(key, out entry))
            {
                misses++;
                Log("MISS count=" + misses + " key=" + key + " target=" + __0.DeclaringType.FullName + "." + __0.Name);
                return true;
            }
            if (entry.Resolved == null)
            {
                Assembly owner = null;
                foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
                    if (assembly.GetName().Name == entry.Assembly) { owner = assembly; break; }
                if (owner == null) owner = Assembly.Load(entry.Assembly);
                entry.Resolved = owner.GetType(entry.Type.TrimStart('~'), true).GetMethod(entry.Method, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
                if (entry.Resolved == null) throw new MissingMethodException(entry.Type, entry.Method);
                if (entry.Type.StartsWith("~"))
                {
                    Type dmdType = typeof(Harmony).Assembly.GetType("MonoMod.Utils.DynamicMethodDefinition", true);
                    object dmd = Activator.CreateInstance(dmdType, new object[] { entry.Resolved });
                    entry.Resolved = (MethodInfo)dmdType.GetMethod("Generate", Type.EmptyTypes).Invoke(dmd, null);
                }
            }
            markNoInlining.Invoke(null, new object[] { __0 });
            __result = entry.Resolved;
            hits++;
            if (hits <= 3 || hits % 50 == 0) Log("HIT count=" + hits + " misses=" + misses + " target=" + __0.DeclaringType.FullName + "." + __0.Name);
            return false;
        }
        private static string Identity(MethodBase method)
        {
            return method == null ? "" : method.Module.Assembly.FullName + "|" + method.Module.ModuleVersionId + "|" + method.MetadataToken.ToString("X8") + "|" + method.DeclaringType.FullName + "::" + method;
        }
        private static string Methods(List<MethodInfo> methods)
        {
            List<string> values = new List<string>();
            foreach (MethodInfo method in methods) values.Add(Identity(method));
            return string.Join("\n", values.ToArray());
        }
        private static string Decode(string value) { return Encoding.UTF8.GetString(Convert.FromBase64String(value)); }
        private static void Log(string message) { MagnificationDownscale.Log("PREPATCH " + message); }
    }
}
