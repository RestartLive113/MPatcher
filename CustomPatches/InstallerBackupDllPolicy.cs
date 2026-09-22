using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

namespace MPatcherFork.CustomPatches
{
	internal static class InstallerBackupDllPolicy
	{
		// Ignore only the exact original loader recorded by our installer and still
		// matching its SHA-256. Other files, even in the backup folder, remain checked.
		internal static string[] Filter(string dataDirectory, string[] files, out string reason)
		{
			reason = "no-verified-backup";
			try
			{
				string data = Path.GetFullPath(dataDirectory).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
				if (!string.Equals(Path.GetFileName(data), "McnCraft_Data", StringComparison.OrdinalIgnoreCase)) return files;
				string manifest = Path.Combine(data, "MPatcherFork.install.ini");
				if (!File.Exists(manifest) || new FileInfo(manifest).Length > 65536) return files;
				Dictionary<string, string> values = new Dictionary<string, string>(StringComparer.Ordinal);
				foreach (string line in File.ReadAllLines(manifest))
				{
					int split = line.IndexOf('=');
					if (split <= 0) continue;
					string key = line.Substring(0, split);
					if (values.ContainsKey(key)) { reason = "duplicate-manifest-key"; return files; }
					values.Add(key, line.Substring(split + 1));
				}
				string existed, name, expected;
				if (!values.TryGetValue("OriginalExisted", out existed) || existed != "True"
					|| !values.TryGetValue("BackupFileName", out name)
					|| !values.TryGetValue("OriginalSha256", out expected) || expected.Length != 64
					|| string.IsNullOrEmpty(name) || name.IndexOfAny(new char[] { '/', '\\', ':' }) >= 0
					|| !name.StartsWith("__Internal.", StringComparison.OrdinalIgnoreCase)
					|| !name.EndsWith(".dll", StringComparison.OrdinalIgnoreCase)) return files;
				string backup = Path.Combine(Path.Combine(data, "MPatcherForkBackup"), name);
				if (!File.Exists(backup)) return files;
				string actual;
				using (SHA256 sha = SHA256.Create())
				using (Stream stream = File.OpenRead(backup))
					actual = BitConverter.ToString(sha.ComputeHash(stream)).Replace("-", "");
				if (!string.Equals(actual, expected, StringComparison.OrdinalIgnoreCase))
				{ reason = "backup-hash-mismatch"; return files; }
				List<string> result = new List<string>();
				foreach (string file in files)
					if (!string.Equals(Path.GetFullPath(file), backup, StringComparison.OrdinalIgnoreCase)) result.Add(file);
				reason = result.Count == files.Length ? "verified-backup-not-enumerated" : "verified-installer-backup";
				return result.ToArray();
			}
			catch (Exception error) { reason = "verification-failed-" + error.GetType().Name; return files; }
		}
	}
}
