using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using MPatchrMain;

namespace MPatcherFork.CustomPatches
{
	internal static class InstallerBackupScriptCompatibility
	{
		private const string PatchId = "mpatcher.installer-backup-script-compatibility.v3";
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
				Log("REGISTERED version=3 scope=native-transpiler+mpatcher-direct-helper exception=manifest-and-sha256-verified-backup only rejectedDll=file-log userData=unchanged");
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
			Log("SCAN total=" + files.Length + " checked=" + filtered.Length + " reason=" + reason);
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
