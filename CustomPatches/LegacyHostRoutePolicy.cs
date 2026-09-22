using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace MPatcherFork.CustomPatches
{
	// Native room metadata is a sequence of ~KEYvalue fields (three-letter keys).
	// MPR identifies one live host session, never a restarted process or room name.
	internal static class LegacyHostRoutePolicy
	{
		internal static bool ValidSession(string value)
		{
			if (value == null || value.Length != 32) return false;
			for (int i = 0; i < value.Length; i++)
				if (!(value[i] >= '0' && value[i] <= '9' || value[i] >= 'a' && value[i] <= 'f')) return false;
			return true;
		}

		internal static string ReadField(string comment, string key)
		{
			if (string.IsNullOrEmpty(comment)) return null;
			int start = comment.IndexOf("~" + key, StringComparison.Ordinal);
			if (start < 0) return null;
			start += 4;
			int end = comment.IndexOf('~', start);
			return end < 0 ? comment.Substring(start) : comment.Substring(start, end - start);
		}

		internal static string Session(string comment)
		{
			string value = ReadField(comment, "MPR");
			return ValidSession(value) ? value : null;
		}

		internal static string Decorate(string comment, string session, string[] addresses)
		{
			if (!ValidSession(session)) throw new ArgumentException("Invalid live session id");
			return "~MPR" + session + "~MPI" + string.Join(",", Normalize(addresses)) + (comment ?? "");
		}

		internal static string[] Normalize(string[] addresses)
		{
			List<string> result = new List<string>();
			if (addresses != null)
				foreach (string value in addresses)
				{
					IPAddress parsed;
					if (string.IsNullOrEmpty(value) || !IPAddress.TryParse(value, out parsed)
						|| parsed.AddressFamily != AddressFamily.InterNetwork || IPAddress.IsLoopback(parsed)) continue;
					byte[] bytes = parsed.GetAddressBytes();
					if (bytes[0] == 0 || bytes[0] >= 224 || bytes[0] == 169 && bytes[1] == 254) continue;
					string normalized = parsed.ToString();
					if (!result.Contains(normalized)) result.Add(normalized);
					if (result.Count == 16) break;
				}
			result.Sort(StringComparer.Ordinal);
			return result.ToArray();
		}

		internal static string[] Addresses(string comment, string[] nativeAddresses)
		{
			List<string> result = new List<string>();
			string advertised = ReadField(comment, "MPI");
			if (Session(comment) != null && advertised != null && advertised.Length <= 256)
				result.AddRange(advertised.Split(','));
			if (nativeAddresses != null) result.AddRange(nativeAddresses);
			return Normalize(result.ToArray());
		}

		internal static bool Matches(string expectedSession, string expectedGuid,
			string candidateComment, string candidateGuid)
		{
			// Never weaken a known session identity to GUID/name matching.
			if (ValidSession(expectedSession)) return Session(candidateComment) == expectedSession;
			return !string.IsNullOrEmpty(expectedGuid) && expectedGuid != "0" && candidateGuid == expectedGuid;
		}
	}
}
