using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Security.Cryptography;
using HarmonyLib;
using MPatchrMain;

namespace MPatcherFork.CustomPatches
{
	internal static class InstallerBackupScriptCompatibility
	{
		private const string PatchId = "mpatcher.installer-backup-script-compatibility.v4";
		private static Harmony harmony;
		internal static void TryRegister()
		{
			if (harmony != null) return;
			Harmony candidate = new Harmony(PatchId);
			try
			{
				MethodInfo native = AccessTools.Method(typeof(JKGKJLLFMLE), "LCILLKPBBEC");
				if (native == null) throw new MissingMethodException("DLL scan entry point");
				HarmonyMethod hook = new HarmonyMethod(AccessTools.Method(typeof(InstallerBackupScriptCompatibility), "FilterScan"));
				HarmonyMethod completed = new HarmonyMethod(AccessTools.Method(typeof(InstallerBackupScriptCompatibility), "ScanCompleted"));
				candidate.Patch(native, null, completed, hook, null);
				harmony = candidate;
				Log("REGISTERED version=4 scope=native-transpiler+mpatcher-direct-helper exceptions=verified-installer-backup,host-loaded-exact-Scale-module rejectedDll=file-log userData=unchanged");
			}
			catch (Exception error)
			{
				candidate.UnpatchAll(PatchId);
				Log("REGISTER_FAILED type=" + error.GetType().Name + " message=" + error.Message);
			}
		}

		private static IEnumerable<CodeInstruction> FilterScan(IEnumerable<CodeInstruction> instructions)
		{
			MethodInfo native = AccessTools.Method(typeof(Directory), "GetFiles", new Type[] { typeof(string), typeof(string), typeof(SearchOption) });
			MethodInfo replacement = AccessTools.Method(typeof(InstallerBackupScriptCompatibility), "GetActiveDlls");
			List<CodeInstruction> result = new List<CodeInstruction>();
			int count = 0;
			foreach (CodeInstruction instruction in instructions)
			{
				CodeInstruction copy = new CodeInstruction(instruction);
				if (copy.opcode == OpCodes.Call && Equals(copy.operand, native))
				{ copy.operand = replacement; count++; }
				result.Add(copy);
			}
			if (count != 1) throw new InvalidOperationException("Expected one DLL enumeration, found " + count);
			return result;
		}

		internal static string[] GetActiveDlls(string path, string pattern, SearchOption option)
		{
			string[] files = Directory.GetFiles(path, pattern, option);
			string reason;
			string[] filtered = InstallerBackupDllPolicy.Filter(path, files, out reason);
			string moduleReason;
			filtered = FilterVerifiedScaleModule(path, filtered, out moduleReason);
			Log("SCAN total=" + files.Length + " checked=" + filtered.Length + " reason=" + reason + " module=" + moduleReason);
			// Match the recovered scanner's parser and allowlist exactly. Reporting
			// its first rejection makes stale backup files and path parsing failures
			// distinguishable without clearing the game's wrong-DLL flag.
			foreach (string file in filtered)
			{
				string name = file.Substring(file.LastIndexOf('\\') + 1);
				name = name.Substring(0, name.Length - 4);
				if (Array.IndexOf(MPatchr.string_2, name) >= 0) continue;
				Log("MPATCHER_REJECT file=" + file + " parsedName=" + name);
				break;
			}
			return filtered;
		}

		internal static string[] FilterVerifiedScaleModule(string dataDirectory, string[] files, out string reason)
		{
			reason = "no-host-verified-module";
			try
			{
				Assembly module;
				string loadedPath, loadedHash;
				if (!ScaleRuntimeHost.TryGetVerifiedModule(out module, out loadedPath, out loadedHash)) return files;
				string data = Path.GetFullPath(dataDirectory).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
				if (!string.Equals(Path.GetFileName(data), "McnCraft_Data", StringComparison.OrdinalIgnoreCase)) return files;
				string ownedDirectory = Path.Combine(Path.Combine(data, "MPatcher"), "Modules");
				string ownedPath = Path.Combine(ownedDirectory, "MPatcher.ScaleRuntime.dll");
				if (!string.Equals(Path.GetFullPath(loadedPath), ownedPath, StringComparison.OrdinalIgnoreCase))
				{ reason = "host-module-path-mismatch"; return files; }
				bool enumerated = false;
				foreach (string file in files)
					if (string.Equals(Path.GetFullPath(file), ownedPath, StringComparison.OrdinalIgnoreCase)) enumerated = true;
				if (!enumerated) { reason = "host-module-not-enumerated"; return files; }
				AssemblyName identity = module.GetName();
				if (identity.Name != "MPatcher.ScaleRuntime" || !SamePublicKey(identity.GetPublicKey(),
					typeof(ScaleRuntimeHost).Assembly.GetName().GetPublicKey()))
				{ reason = "host-module-identity-mismatch"; return files; }

				string manifestPath = Path.Combine(ownedDirectory, "MPatcher.ScaleRuntime.manifest.ini");
				if (!File.Exists(manifestPath) || new FileInfo(manifestPath).Length > 65536)
				{ reason = "host-module-manifest-missing-or-oversize"; return files; }
				Dictionary<string, string> manifest = new Dictionary<string, string>(StringComparer.Ordinal);
				foreach (string line in File.ReadAllLines(manifestPath))
				{
					string text = line.Trim();
					if (text.Length == 0 || text.StartsWith("#", StringComparison.Ordinal)) continue;
					int split = text.IndexOf('=');
					if (split <= 0) { reason = "host-module-manifest-invalid"; return files; }
					string key = text.Substring(0, split).Trim();
					if (manifest.ContainsKey(key)) { reason = "host-module-manifest-duplicate-key"; return files; }
					manifest.Add(key, text.Substring(split + 1).Trim());
				}
				string value;
				if (!manifest.TryGetValue("HostApiVersion", out value) || value != "1"
					|| !manifest.TryGetValue("ModuleAssemblyName", out value) || value != "MPatcher.ScaleRuntime"
					|| !manifest.TryGetValue("PayloadSha256", out value) || value.Length != 64
					|| !string.Equals(value, loadedHash, StringComparison.OrdinalIgnoreCase))
				{ reason = "host-module-manifest-mismatch"; return files; }
				string actualHash;
				using (SHA256 sha = SHA256.Create())
				using (FileStream stream = new FileStream(ownedPath, FileMode.Open, FileAccess.Read, FileShare.Read))
					actualHash = BitConverter.ToString(sha.ComputeHash(stream)).Replace("-", "");
				if (!string.Equals(actualHash, loadedHash, StringComparison.OrdinalIgnoreCase))
				{ reason = "host-module-payload-mismatch"; return files; }

				List<string> result = new List<string>();
				foreach (string file in files)
					if (!string.Equals(Path.GetFullPath(file), ownedPath, StringComparison.OrdinalIgnoreCase)) result.Add(file);
				reason = "host-loaded-verified-scale-module";
				return result.ToArray();
			}
			catch (Exception error) { reason = "host-module-verification-failed-" + error.GetType().Name; return files; }
		}

		private static bool SamePublicKey(byte[] first, byte[] second)
		{
			if (first == null || second == null || first.Length == 0 || first.Length != second.Length) return false;
			for (int i = 0; i < first.Length; i++) if (first[i] != second[i]) return false;
			return true;
		}

		internal static void ScanCompleted()
		{
			Log("RESULT wrongDll=" + JKGKJLLFMLE.JNOHOLDLAKD);
		}

		private static void Log(string text)
		{
			mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng("[SCRIPT-DLL-COMPAT] " + text);
		}
	}
}
