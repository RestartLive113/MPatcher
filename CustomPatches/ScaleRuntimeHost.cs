using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
    // A module update replaces these external files, not the precompiled core.
    // Version one loads once per game process; the next launch takes the update.
    public static class ScaleRuntimeHost
    {
        public const int ApiVersion = 1;
        private static bool attempted;
        private static IScaleRuntime runtime;
        private static Assembly verifiedModule;
        private static string verifiedModulePath;
        private static string verifiedPayloadHash;
        internal static IScaleRuntime Runtime { get { return runtime; } }

        // The DLL scanner may exempt only the exact bytes that this host verified
        // and successfully initialized. Byte-loaded assemblies have no Location.
        internal static bool TryGetVerifiedModule(out Assembly module, out string path, out string hash)
        {
            module = null; path = null; hash = null;
            if (runtime == null || verifiedModule == null
                || !ReferenceEquals(runtime.GetType().Assembly, verifiedModule)
                || string.IsNullOrEmpty(verifiedModulePath) || string.IsNullOrEmpty(verifiedPayloadHash)) return false;
            module = verifiedModule;
            path = verifiedModulePath;
            hash = verifiedPayloadHash;
            return true;
        }

        internal static bool TryLoad()
        {
            if (attempted) return runtime != null;
            attempted = true;
            try
            {
                string directory = Path.Combine(Application.dataPath, "MPatcher");
                directory = Path.Combine(directory, "Modules");
                string path = Path.Combine(directory, "MPatcher.ScaleRuntime.dll");
                string manifestPath = Path.Combine(directory, "MPatcher.ScaleRuntime.manifest.ini");
                Dictionary<string, string> manifest = ReadManifest(manifestPath);
                string value;
                if (!manifest.TryGetValue("HostApiVersion", out value) || value != "1")
                    throw new InvalidDataException("Scale module host API mismatch");
                if (!manifest.TryGetValue("ModuleAssemblyName", out value) || value != "MPatcher.ScaleRuntime")
                    throw new InvalidDataException("Scale module assembly name mismatch");
                byte[] bytes = File.ReadAllBytes(path);
                string hash;
                using (SHA256 sha = SHA256.Create())
                    hash = BitConverter.ToString(sha.ComputeHash(bytes)).Replace("-", "");
                if (!manifest.TryGetValue("PayloadSha256", out value)
                    || value.Length != 64 || !string.Equals(value, hash, StringComparison.OrdinalIgnoreCase))
                    throw new InvalidDataException("Scale module payload hash mismatch");
                // Load the exact verified bytes, avoiding a second file read.
                Assembly module = Assembly.Load(bytes);
                AssemblyName identity = module.GetName();
                if (identity.Name != "MPatcher.ScaleRuntime"
                    || !SameBytes(identity.GetPublicKey(), typeof(ScaleRuntimeHost).Assembly.GetName().GetPublicKey()))
                    throw new InvalidDataException("Scale module identity mismatch");
                Type entry = module.GetType("MPatcherFork.ScaleRuntime.ScaleRuntimeModule", true);
                if (!typeof(IScaleRuntime).IsAssignableFrom(entry))
                    throw new InvalidDataException("Scale module entry does not implement the host API");
                IScaleRuntime loaded = (IScaleRuntime)Activator.CreateInstance(entry);
                if (loaded.ApiVersion != ApiVersion)
                    throw new InvalidDataException("Scale module entry API mismatch");
                loaded.Initialize();
                verifiedModule = module;
                verifiedModulePath = Path.GetFullPath(path);
                verifiedPayloadHash = hash;
                runtime = loaded;
                Log("[SCALE-MODULE] LOADED api=" + ApiVersion + " payload=" + hash
                    + " path=" + path + " updates=module-only restart=required hooks=core");
                return true;
            }
            catch (Exception error)
            {
                runtime = null;
                verifiedModule = null;
                verifiedModulePath = null;
                verifiedPayloadHash = null;
                Log("[SCALE-MODULE] LOAD_FAILED nativeSculpt=available " + error);
                return false;
            }
        }

        private static Dictionary<string, string> ReadManifest(string path)
        {
            Dictionary<string, string> values = new Dictionary<string, string>(StringComparer.Ordinal);
            foreach (string line in File.ReadAllLines(path))
            {
                string text = line.Trim();
                if (text.Length == 0 || text.StartsWith("#", StringComparison.Ordinal)) continue;
                int split = text.IndexOf('=');
                if (split <= 0) throw new InvalidDataException("Invalid Scale module manifest line");
                string key = text.Substring(0, split).Trim();
                if (values.ContainsKey(key)) throw new InvalidDataException("Duplicate Scale module manifest key");
                values.Add(key, text.Substring(split + 1).Trim());
            }
            return values;
        }

        private static bool SameBytes(byte[] first, byte[] second)
        {
            if (first == null || second == null || first.Length != second.Length || first.Length == 0) return false;
            for (int i = 0; i < first.Length; i++) if (first[i] != second[i]) return false;
            return true;
        }

        // Runtime code must not bind to recovered obfuscated logger names.
        public static void Log(string message)
        {
            try { global::mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(message); }
            catch (Exception) { }
        }
    }
}
