using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace MachineCraftMPatcherInstaller
{
	internal sealed class InstallerCommandResult
	{
		internal bool Success;
		internal string Message;
		internal string ErrorDetails;
		internal string LogPath;
		internal int ClosedGameProcessCount;
		internal int ForcedGameProcessCount;
	}

	internal static class InstallerResultProtocol
	{
		private const int MaximumBytes = 65536;

		internal static void Write(string path, bool success, string message, string errorDetails, string logPath,
			int closedGameProcessCount, int forcedGameProcessCount)
		{
			if (string.IsNullOrWhiteSpace(path))
				return;
			string fullPath = Path.GetFullPath(path);
			string directory = Path.GetDirectoryName(fullPath);
			if (string.IsNullOrEmpty(directory) || !Directory.Exists(directory))
				throw new DirectoryNotFoundException(directory);
			string[] lines = new string[]
			{
				"Format=1",
				"Success=" + (success ? "True" : "False"),
				"MessageBase64=" + Encode(message),
				"DetailsBase64=" + Encode(errorDetails),
				"LogPathBase64=" + Encode(logPath),
				"ClosedGameProcesses=" + closedGameProcessCount.ToString(CultureInfo.InvariantCulture),
				"ForcedGameProcesses=" + forcedGameProcessCount.ToString(CultureInfo.InvariantCulture)
			};
			File.WriteAllLines(fullPath, lines, new UTF8Encoding(false));
		}

		internal static InstallerCommandResult TryRead(string path)
		{
			try
			{
				FileInfo item = new FileInfo(path);
				if (!item.Exists || item.Length <= 0 || item.Length > MaximumBytes)
					return null;
				Dictionary<string, string> values = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
				string[] lines = File.ReadAllLines(item.FullName, Encoding.UTF8);
				for (int i = 0; i < lines.Length; i++)
				{
					int separator = lines[i].IndexOf('=');
					if (separator <= 0)
						return null;
					string key = lines[i].Substring(0, separator);
					if (values.ContainsKey(key))
						return null;
					values.Add(key, lines[i].Substring(separator + 1));
				}
				string format;
				string successText;
				string messageText;
				string detailsText;
				string logText;
				if (!values.TryGetValue("Format", out format) || format != "1"
					|| !values.TryGetValue("Success", out successText)
					|| !values.TryGetValue("MessageBase64", out messageText)
					|| !values.TryGetValue("DetailsBase64", out detailsText)
					|| !values.TryGetValue("LogPathBase64", out logText))
					return null;
				bool success;
				if (!bool.TryParse(successText, out success))
					return null;
				int closed = ReadNonNegativeInt(values, "ClosedGameProcesses");
				int forced = ReadNonNegativeInt(values, "ForcedGameProcesses");
				if (closed < 0 || forced < 0 || forced > closed)
					return null;
				return new InstallerCommandResult
				{
					Success = success,
					Message = Decode(messageText),
					ErrorDetails = Decode(detailsText),
					LogPath = Decode(logText),
					ClosedGameProcessCount = closed,
					ForcedGameProcessCount = forced
				};
			}
			catch
			{
				return null;
			}
		}

		private static int ReadNonNegativeInt(Dictionary<string, string> values, string key)
		{
			string text;
			int value;
			if (!values.TryGetValue(key, out text)
				|| !int.TryParse(text, NumberStyles.None, CultureInfo.InvariantCulture, out value)
				|| value < 0)
				return -1;
			return value;
		}

		private static string Encode(string value)
		{
			return Convert.ToBase64String(Encoding.UTF8.GetBytes(value ?? string.Empty));
		}

		private static string Decode(string value)
		{
			return Encoding.UTF8.GetString(Convert.FromBase64String(value ?? string.Empty));
		}
	}
}
