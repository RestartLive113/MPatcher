using System;
using System.IO;
using System.Text;

namespace MPatcherFork.CustomPatches
{
	// A clean exit removes this marker. If the process disappears without running
	// the exit hooks, the next join to the same Legacy target can claim the
	// server-side retained snapshot.
	internal static class LegacyCrashResumeMarker
	{
		internal const string FileName = "legacy-active-session-v1.txt";
		private const string Magic = "MPATCHER_LEGACY_ACTIVE_V1";

		internal static bool Matches(string token, string target, out string reason)
		{
			reason = null;
			try
			{
				string path = MarkerPath();
				if (!File.Exists(path)) { reason = "marker-missing"; return false; }
				string storedToken;
				string storedTarget;
				if (!TryDecode(File.ReadAllText(path), out storedToken, out storedTarget, out reason))
					return false;
				if (!string.Equals(storedToken, token, StringComparison.OrdinalIgnoreCase))
				{
					reason = "token-mismatch";
					return false;
				}
				if (!string.Equals(storedTarget, target, StringComparison.Ordinal))
				{
					reason = "target-mismatch";
					return false;
				}
				reason = "matched";
				return true;
			}
			catch (Exception error)
			{
				reason = "marker-read-" + error.GetType().Name;
				return false;
			}
		}

		internal static bool MatchesTarget(string target, out string reason)
		{
			reason = null;
			try
			{
				string path = MarkerPath();
				if (!File.Exists(path)) { reason = "marker-missing"; return false; }
				string storedToken;
				string storedTarget;
				if (!TryDecode(File.ReadAllText(path), out storedToken, out storedTarget, out reason))
					return false;
				if (!string.Equals(storedTarget, target, StringComparison.Ordinal))
				{
					reason = "target-mismatch";
					return false;
				}
				reason = "matched";
				return true;
			}
			catch (Exception error)
			{
				reason = "marker-read-" + error.GetType().Name;
				return false;
			}
		}

		internal static bool Arm(string token, string target, out string reason)
		{
			reason = null;
			if (!LegacyTransientReconnect.ValidToken(token) || string.IsNullOrEmpty(target)
				|| target == "unavailable")
			{
				reason = "invalid-session";
				return false;
			}
			try
			{
				string path = MarkerPath();
				string directory = Path.GetDirectoryName(path);
				Directory.CreateDirectory(directory);
				string temporary = path + ".tmp";
				File.WriteAllText(temporary, Encode(token, target), Encoding.UTF8);
				if (File.Exists(path)) File.Delete(path);
				File.Move(temporary, path);
				reason = "armed";
				return true;
			}
			catch (Exception error)
			{
				reason = "marker-write-" + error.GetType().Name;
				return false;
			}
		}

		internal static bool Clear(out string reason)
		{
			reason = null;
			try
			{
				string path = MarkerPath();
				bool existed = File.Exists(path);
				if (existed) File.Delete(path);
				string temporary = path + ".tmp";
				if (File.Exists(temporary)) File.Delete(temporary);
				reason = existed ? "cleared" : "already-clear";
				return true;
			}
			catch (Exception error)
			{
				reason = "marker-delete-" + error.GetType().Name;
				return false;
			}
		}

		internal static string Encode(string token, string target)
		{
			return Magic + "\n" + token.ToLowerInvariant() + "\n"
				+ Convert.ToBase64String(Encoding.UTF8.GetBytes(target)) + "\n";
		}

		internal static bool TryDecode(string text, out string token, out string target,
			out string reason)
		{
			token = null;
			target = null;
			reason = null;
			if (string.IsNullOrEmpty(text)) { reason = "marker-empty"; return false; }
			string[] lines = text.Replace("\r", "").Split('\n');
			if (lines.Length < 3 || lines[0] != Magic)
			{
				reason = "marker-format";
				return false;
			}
			if (!LegacyTransientReconnect.ValidToken(lines[1]))
			{
				reason = "marker-token";
				return false;
			}
			try { target = Encoding.UTF8.GetString(Convert.FromBase64String(lines[2])); }
			catch { reason = "marker-target-base64"; return false; }
			if (string.IsNullOrEmpty(target) || target.Length > 2048)
			{
				reason = "marker-target";
				return false;
			}
			token = lines[1].ToLowerInvariant();
			return true;
		}

		private static string MarkerPath()
		{
			return Path.Combine(Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), "UserData"),
				"_mpatcher"), FileName);
		}
	}
}
