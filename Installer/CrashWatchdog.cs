using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace MachineCraftMPatcherCrashWatchdog
{
	internal static class Program
	{
		private const int TailLineCount = 250;
		private static readonly UTF8Encoding Utf8 = new UTF8Encoding(false);

		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool GetExitCodeProcess(IntPtr processHandle, out uint exitCode);

		private static int Main(string[] args)
		{
			Dictionary<string, string> options = ParseArguments(args);
			int pid;
			if (!int.TryParse(GetOption(options, "--pid"), NumberStyles.Integer,
				CultureInfo.InvariantCulture, out pid) || pid <= 0)
				return 2;

			string gameRoot = GetOption(options, "--root");
			string logsDirectory = GetOption(options, "--logs");
			string session = GetOption(options, "--session");
			string statePath = GetOption(options, "--state");
			string patchLogPath = GetOption(options, "--patch-log");
			string unityLogPath = GetOption(options, "--unity-log");
			if (string.IsNullOrEmpty(logsDirectory) || string.IsNullOrEmpty(session))
				return 3;

			Directory.CreateDirectory(logsDirectory);
			string watcherLogPath = Path.Combine(logsDirectory, "CrashWatchdog_" + session
				+ "_pid" + pid + ".log");
			try
			{
				return Watch(pid, gameRoot, logsDirectory, session, statePath,
					patchLogPath, unityLogPath, watcherLogPath);
			}
			catch (Exception error)
			{
				SafeAppend(watcherLogPath, "WATCHDOG_FAILED type=" + error.GetType().Name
					+ " message=" + error.Message + Environment.NewLine + error.StackTrace);
				return 1;
			}
		}

		private static int Watch(int pid, string gameRoot, string logsDirectory, string session,
			string statePath, string patchLogPath, string unityLogPath, string watcherLogPath)
		{
			Process gameProcess;
			try
			{
				gameProcess = Process.GetProcessById(pid);
			}
			catch (ArgumentException)
			{
				SafeAppend(watcherLogPath, "PROCESS_NOT_FOUND pid=" + pid);
				return 4;
			}

			DateTime observedUtc = DateTime.UtcNow;
			DateTime processStartUtc = observedUtc;
			string executablePath = Path.Combine(gameRoot ?? string.Empty, "McnCraft.exe");
			string executableVersion = string.Empty;
			try { processStartUtc = gameProcess.StartTime.ToUniversalTime(); } catch { }
			try { executablePath = gameProcess.MainModule.FileName; } catch { }
			try { executableVersion = FileVersionInfo.GetVersionInfo(executablePath).FileVersion; } catch { }

			SafeAppend(watcherLogPath, "WATCHING gamePid=" + pid
				+ " observedUtc=" + observedUtc.ToString("o", CultureInfo.InvariantCulture)
				+ " processStartUtc=" + processStartUtc.ToString("o", CultureInfo.InvariantCulture)
				+ " executable=" + Quote(executablePath));

			IntPtr processHandle = gameProcess.Handle;
			long peakWorkingSet = 0;
			long peakVirtualMemory = 0;
			while (!gameProcess.WaitForExit(500))
			{
				try
				{
					gameProcess.Refresh();
					peakWorkingSet = Math.Max(peakWorkingSet, gameProcess.WorkingSet64);
					peakVirtualMemory = Math.Max(peakVirtualMemory, gameProcess.VirtualMemorySize64);
				}
				catch { }
			}
			DateTime exitUtc = DateTime.UtcNow;
			uint nativeExitCode;
			int exitCode = GetExitCodeProcess(processHandle, out nativeExitCode)
				? unchecked((int)nativeExitCode) : int.MinValue;
			gameProcess.Dispose();

			string state = ReadState(statePath);
			bool clean = state.StartsWith("state=clean", StringComparison.OrdinalIgnoreCase);
			string archivedUnityOutput = ArchiveUnityOutput(gameRoot, logsDirectory, session, pid, watcherLogPath);
			SafeAppend(watcherLogPath, "EXIT gamePid=" + pid
				+ " code=" + FormatExitCode(exitCode)
				+ " clean=" + clean
				+ " state=" + Quote(FirstLine(state))
				+ " exitUtc=" + exitUtc.ToString("o", CultureInfo.InvariantCulture));

			if (clean)
			{
				SafeDelete(statePath);
				return 0;
			}

			Thread.Sleep(3000);
			List<string> dumps = CollectCrashDumps(pid, processStartUtc, logsDirectory, watcherLogPath);
			string applicationEvents = ReadApplicationCrashEvents(pid, executablePath, processStartUtc);
			string reportPath = Path.Combine(logsDirectory, "Crash_" + session + "_pid" + pid + "_exit.txt");
			StringBuilder report = new StringBuilder();
			report.AppendLine("MachineCraft abnormal-exit report");
			report.AppendLine("observedUtc=" + observedUtc.ToString("o", CultureInfo.InvariantCulture));
			report.AppendLine("processStartUtc=" + processStartUtc.ToString("o", CultureInfo.InvariantCulture));
			report.AppendLine("exitUtc=" + exitUtc.ToString("o", CultureInfo.InvariantCulture));
			report.AppendLine("runtimeSeconds=" + Math.Max(0.0, (exitUtc - processStartUtc).TotalSeconds).ToString("0.000", CultureInfo.InvariantCulture));
			report.AppendLine("pid=" + pid);
			report.AppendLine("exitCode=" + FormatExitCode(exitCode));
			report.AppendLine("classification=" + ClassifyExitCode(exitCode));
			report.AppendLine("cleanShutdownMarker=false");
			report.AppendLine("executable=" + executablePath);
			report.AppendLine("fileVersion=" + executableVersion);
			report.AppendLine("os=" + Environment.OSVersion);
			report.AppendLine("clr=" + Environment.Version);
			report.AppendLine("peakWorkingSetBytes=" + peakWorkingSet);
			report.AppendLine("peakVirtualMemoryBytes=" + peakVirtualMemory);
			report.AppendLine("patchLog=" + patchLogPath);
			report.AppendLine("unityLog=" + unityLogPath);
			report.AppendLine("archivedUnityOutput=" + archivedUnityOutput);
			report.AppendLine("dumpFiles=" + (dumps.Count == 0 ? "<none>" : string.Join(";", dumps.ToArray())));
			report.AppendLine();
			report.AppendLine("[Session state]");
			report.AppendLine(string.IsNullOrEmpty(state) ? "<missing>" : state);
			report.AppendLine();
			report.AppendLine("[Windows Application crash events]");
			report.AppendLine(string.IsNullOrEmpty(applicationEvents) ? "<none found>" : applicationEvents);
			AppendTail(report, "MPatcher log", patchLogPath);
			AppendTail(report, "Unity callback log", unityLogPath);
			AppendTail(report, "Unity output_log snapshot", archivedUnityOutput);
			File.WriteAllText(reportPath, report.ToString(), Utf8);
			SafeAppend(watcherLogPath, "CRASH_REPORT path=" + Quote(reportPath)
				+ " dumps=" + dumps.Count + " events=" + (!string.IsNullOrEmpty(applicationEvents)));
			SafeDelete(statePath);
			return 0;
		}

		private static string ArchiveUnityOutput(string gameRoot, string logsDirectory,
			string session, int pid, string watcherLogPath)
		{
			try
			{
				string source = Path.Combine(gameRoot, "McnCraft_Data", "output_log.txt");
				if (!File.Exists(source))
					return string.Empty;
				string destination = Path.Combine(logsDirectory, "UnityOutput_" + session
					+ "_pid" + pid + ".log");
				File.Copy(source, destination, true);
				return destination;
			}
			catch (Exception error)
			{
				SafeAppend(watcherLogPath, "UNITY_OUTPUT_ARCHIVE_FAILED type=" + error.GetType().Name
					+ " message=" + error.Message);
				return string.Empty;
			}
		}

		private static string ReadApplicationCrashEvents(int pid, string executablePath, DateTime processStartUtc)
		{
			StringBuilder matches = new StringBuilder();
			try
			{
				string queryText = "*[System[(EventID=1000 or EventID=1001) and TimeCreated[timediff(@SystemTime) <= 180000]]]";
				EventLogQuery query = new EventLogQuery("Application", PathType.LogName, queryText);
				query.ReverseDirection = true;
				using (EventLogReader reader = new EventLogReader(query))
				{
					for (int read = 0; read < 60; read++)
					{
						using (EventRecord record = reader.ReadEvent())
						{
							if (record == null)
								break;
							string xml = record.ToXml();
							if (xml.IndexOf("McnCraft.exe", StringComparison.OrdinalIgnoreCase) < 0
								&& (!string.IsNullOrEmpty(executablePath)
									&& xml.IndexOf(executablePath, StringComparison.OrdinalIgnoreCase) < 0))
								continue;
							if (record.TimeCreated.HasValue
								&& record.TimeCreated.Value.ToUniversalTime() < processStartUtc.AddSeconds(-30))
								continue;
							matches.AppendLine("eventId=" + record.Id
								+ " time=" + (record.TimeCreated.HasValue
									? record.TimeCreated.Value.ToString("o", CultureInfo.InvariantCulture) : "<unknown>"));
							try { matches.AppendLine(record.FormatDescription()); } catch { }
							matches.AppendLine(xml);
							matches.AppendLine();
						}
					}
				}
			}
			catch (Exception error)
			{
				matches.AppendLine("<event-query-failed " + error.GetType().Name + ": " + error.Message + ">");
			}
			return matches.ToString();
		}

		private static List<string> CollectCrashDumps(int pid, DateTime processStartUtc,
			string logsDirectory, string watcherLogPath)
		{
			List<string> copied = new List<string>();
			string sourceDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CrashDumps");
			if (!Directory.Exists(sourceDirectory))
				return copied;

			string destinationDirectory = Path.Combine(logsDirectory, "CrashDumps");
			Directory.CreateDirectory(destinationDirectory);
			DateTime earliest = processStartUtc.AddSeconds(-30);
			for (int attempt = 0; attempt < 6; attempt++)
			{
				string[] candidates;
				try { candidates = Directory.GetFiles(sourceDirectory, "McnCraft.exe*" + pid + "*.dmp"); }
				catch { candidates = new string[0]; }
				for (int i = 0; i < candidates.Length; i++)
				{
					try
					{
						FileInfo item = new FileInfo(candidates[i]);
						if (item.LastWriteTimeUtc < earliest)
							continue;
						string destination = UniquePath(destinationDirectory, item.Name);
						File.Copy(item.FullName, destination, false);
						copied.Add(destination);
					}
					catch (Exception error)
					{
						SafeAppend(watcherLogPath, "DUMP_COPY_FAILED path=" + Quote(candidates[i])
							+ " type=" + error.GetType().Name + " message=" + error.Message);
					}
				}
				if (copied.Count > 0)
					break;
				Thread.Sleep(2000);
			}
			return copied;
		}

		private static string UniquePath(string directory, string fileName)
		{
			string path = Path.Combine(directory, fileName);
			if (!File.Exists(path))
				return path;
			string name = Path.GetFileNameWithoutExtension(fileName);
			string extension = Path.GetExtension(fileName);
			for (int suffix = 2; ; suffix++)
			{
				path = Path.Combine(directory, name + "." + suffix + extension);
				if (!File.Exists(path))
					return path;
			}
		}

		private static void AppendTail(StringBuilder report, string title, string path)
		{
			report.AppendLine();
			report.AppendLine("[Tail: " + title + "]");
			if (string.IsNullOrEmpty(path) || !File.Exists(path))
			{
				report.AppendLine("<missing>");
				return;
			}
			try
			{
				Queue<string> lines = new Queue<string>();
				using (StreamReader reader = new StreamReader(new FileStream(path, FileMode.Open,
					FileAccess.Read, FileShare.ReadWrite | FileShare.Delete), Encoding.UTF8, true))
				{
					string line;
					while ((line = reader.ReadLine()) != null)
					{
						if (line.Length > 8192)
							line = line.Substring(0, 8192) + "...<truncated>";
						lines.Enqueue(line);
						if (lines.Count > TailLineCount)
							lines.Dequeue();
					}
				}
				foreach (string line in lines)
					report.AppendLine(line);
			}
			catch (Exception error)
			{
				report.AppendLine("<read-failed " + error.GetType().Name + ": " + error.Message + ">");
			}
		}

		private static string ReadState(string path)
		{
			try { return string.IsNullOrEmpty(path) || !File.Exists(path) ? string.Empty : File.ReadAllText(path); }
			catch { return string.Empty; }
		}

		private static string FirstLine(string value)
		{
			if (string.IsNullOrEmpty(value))
				return string.Empty;
			int index = value.IndexOfAny(new char[] { '\r', '\n' });
			return index < 0 ? value : value.Substring(0, index);
		}

		private static string FormatExitCode(int exitCode)
		{
			return exitCode.ToString(CultureInfo.InvariantCulture) + " (0x"
				+ unchecked((uint)exitCode).ToString("X8", CultureInfo.InvariantCulture) + ")";
		}

		private static string ClassifyExitCode(int exitCode)
		{
			switch (unchecked((uint)exitCode))
			{
				case 0xC0000005: return "native access violation";
				case 0xC00000FD: return "stack overflow";
				case 0xC0000409: return "stack buffer overrun or fast-fail";
				case 0xE0434352: return "unhandled CLR exception";
				case 0x40000015: return "fatal application exit";
				case 0xC000001D: return "illegal instruction";
				case 0xC0000094: return "integer divide by zero";
				case 0xC0000096: return "privileged instruction";
				case 0x80000003: return "breakpoint exception";
				case 0: return "unclean termination with zero exit code";
				default: return "abnormal process exit";
			}
		}

		private static Dictionary<string, string> ParseArguments(string[] args)
		{
			Dictionary<string, string> result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			for (int i = 0; i + 1 < args.Length; i += 2)
				result[args[i]] = args[i + 1];
			return result;
		}

		private static string GetOption(Dictionary<string, string> options, string key)
		{
			string value;
			return options.TryGetValue(key, out value) ? value : string.Empty;
		}

		private static string Quote(string value)
		{
			if (value == null)
				return "<null>";
			return "\"" + value.Replace("\\", "\\\\").Replace("\r", "\\r")
				.Replace("\n", "\\n").Replace("\"", "\\\"") + "\"";
		}

		private static void SafeAppend(string path, string message)
		{
			try
			{
				File.AppendAllText(path, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff",
					CultureInfo.InvariantCulture) + " " + message + Environment.NewLine, Utf8);
			}
			catch { }
		}

		private static void SafeDelete(string path)
		{
			try { if (!string.IsNullOrEmpty(path) && File.Exists(path)) File.Delete(path); }
			catch { }
		}
	}
}
