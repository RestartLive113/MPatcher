using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Microsoft.Win32.SafeHandles;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
	internal static class CrashDiagnostics
	{
		private const int RecentIssueLimit = 100;
		private const int MiniDumpFlags = 0x00000001 | 0x00000004 | 0x00000020 | 0x00000100 | 0x00001000;

		private static readonly object sync = new object();
		private static readonly List<string> recentIssues = new List<string>();
		private static StreamWriter unityWriter;
		private static Application.LogCallback unityLogCallback;
		private static UnhandledExceptionEventHandler unhandledExceptionCallback;
		private static bool registered;
		private static bool cleanExit;
		private static string gameRoot;
		private static string logsDirectory;
		private static string sessionStamp;
		private static string unityLogPath;
		private static string statePath;

		[DllImport("Dbghelp.dll", SetLastError = true)]
		private static extern bool MiniDumpWriteDump(IntPtr processHandle, uint processId,
			SafeFileHandle fileHandle, int dumpType, IntPtr exceptionParam,
			IntPtr userStreamParam, IntPtr callbackParam);

		internal static void TryRegister()
		{
			lock (sync)
			{
				if (registered)
					return;
				registered = true;
			}

			try
			{
				Process process = Process.GetCurrentProcess();
				gameRoot = ResolveGameRoot(process);
				logsDirectory = global::mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.LogsDirectory;
				if (string.IsNullOrEmpty(logsDirectory))
					logsDirectory = Path.Combine(gameRoot, "logs");
				Directory.CreateDirectory(logsDirectory);

				sessionStamp = global::mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.SessionStamp;
				if (string.IsNullOrEmpty(sessionStamp))
					sessionStamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");

				unityLogPath = Path.Combine(logsDirectory, "MachineCraft_" + sessionStamp
					+ "_pid" + process.Id + ".log");
				statePath = Path.Combine(logsDirectory, ".MPatcherSession_" + sessionStamp
					+ "_pid" + process.Id + ".state");

				unityWriter = new StreamWriter(new FileStream(unityLogPath, FileMode.CreateNew,
					FileAccess.Write, FileShare.ReadWrite), new UTF8Encoding(false));
				unityWriter.AutoFlush = true;
				unityWriter.WriteLine("=== MachineCraft Unity log started "
					+ DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + " pid=" + process.Id + " ===");
				unityWriter.WriteLine("unity=" + Application.unityVersion + " platform=" + Application.platform
					+ " dataPath=" + Application.dataPath);

				WriteState("active", null);
				unityLogCallback = HandleUnityLog;
				Application.logMessageReceivedThreaded += unityLogCallback;
				unhandledExceptionCallback = HandleUnhandledException;
				AppDomain.CurrentDomain.UnhandledException += unhandledExceptionCallback;

				StartWatchdog(process);
				LogShared("REGISTERED logs=" + Quote(logsDirectory)
					+ " unityLog=" + Quote(unityLogPath)
					+ " patchLog=" + Quote(global::mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.CurrentLogPath)
					+ " cleanExit=MPatchr.OnApplicationQuit");
			}
			catch (Exception error)
			{
				LogShared("REGISTER_FAILED type=" + error.GetType().Name + " message=" + Quote(error.Message));
			}
		}

		private static string ResolveGameRoot(Process process)
		{
			try
			{
				return Path.GetDirectoryName(process.MainModule.FileName);
			}
			catch (Exception)
			{
				return Path.GetFullPath(Path.Combine(Application.dataPath, ".."));
			}
		}

		private static void StartWatchdog(Process process)
		{
			string watchdogPath = Path.Combine(Path.Combine(Path.Combine(gameRoot, "McnCraft_Data"),
				"MPatcherFork"), "MPatcherCrashWatchdog.exe");
			if (!File.Exists(watchdogPath))
			{
				LogShared("WATCHDOG_UNAVAILABLE path=" + Quote(watchdogPath));
				return;
			}

			ProcessStartInfo startInfo = new ProcessStartInfo();
			startInfo.FileName = watchdogPath;
			startInfo.Arguments = "--pid " + process.Id
				+ " --root " + QuoteArgument(gameRoot)
				+ " --logs " + QuoteArgument(logsDirectory)
				+ " --session " + QuoteArgument(sessionStamp)
				+ " --state " + QuoteArgument(statePath)
				+ " --patch-log " + QuoteArgument(global::mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.CurrentLogPath)
				+ " --unity-log " + QuoteArgument(unityLogPath);
			startInfo.UseShellExecute = false;
			startInfo.CreateNoWindow = true;
			startInfo.WindowStyle = ProcessWindowStyle.Hidden;
			Process watcher = Process.Start(startInfo);
			LogShared("WATCHDOG_STARTED watcherPid=" + (watcher == null ? 0 : watcher.Id)
				+ " gamePid=" + process.Id);
		}

		private static string QuoteArgument(string value)
		{
			if (value == null)
				value = string.Empty;
			return "\"" + value.Replace("\"", "\\\"") + "\"";
		}

		private static void HandleUnityLog(string condition, string stackTrace, LogType type)
		{
			string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
			string entry = timestamp + " [" + type + "] [thread=" + Thread.CurrentThread.ManagedThreadId + "] "
				+ (condition ?? "<null>");
			lock (sync)
			{
				try
				{
					if (unityWriter != null)
					{
						unityWriter.WriteLine(entry);
						if (!string.IsNullOrEmpty(stackTrace))
							unityWriter.WriteLine(stackTrace);
					}
				}
				catch (Exception)
				{
				}

				if (type == LogType.Warning || type == LogType.Error
					|| type == LogType.Assert || type == LogType.Exception)
				{
					string issue = entry + (string.IsNullOrEmpty(stackTrace) ? string.Empty : Environment.NewLine + stackTrace);
					recentIssues.Add(issue);
					if (recentIssues.Count > RecentIssueLimit)
						recentIssues.RemoveAt(0);
				}
			}
		}

		private static void HandleUnhandledException(object sender, UnhandledExceptionEventArgs args)
		{
			Exception error = args == null ? null : args.ExceptionObject as Exception;
			try
			{
				WriteState("managed-crash", error == null ? Convert.ToString(args.ExceptionObject) : error.ToString());
				string reportPath = WriteManagedCrashReport(error, args != null && args.IsTerminating);
				string dumpPath = TryWriteManagedDump();
				LogShared("MANAGED_UNHANDLED report=" + Quote(reportPath) + " dump=" + Quote(dumpPath)
					+ " terminating=" + (args != null && args.IsTerminating));
				global::mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.Flush();
			}
			catch (Exception)
			{
			}
		}

		private static string WriteManagedCrashReport(Exception error, bool terminating)
		{
			string path = Path.Combine(logsDirectory, "Crash_" + sessionStamp + "_pid"
				+ Process.GetCurrentProcess().Id + "_managed.txt");
			StringBuilder report = new StringBuilder();
			report.AppendLine("MachineCraft managed crash report");
			report.AppendLine("time=" + DateTime.Now.ToString("o"));
			report.AppendLine("pid=" + Process.GetCurrentProcess().Id);
			report.AppendLine("terminating=" + terminating);
			report.AppendLine("gameRoot=" + gameRoot);
			report.AppendLine("unity=" + Application.unityVersion);
			report.AppendLine("clr=" + Environment.Version);
			report.AppendLine("os=" + Environment.OSVersion);
			report.AppendLine("patchLog=" + global::mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.CurrentLogPath);
			report.AppendLine("unityLog=" + unityLogPath);
			report.AppendLine();
			report.AppendLine("[Unhandled exception]");
			report.AppendLine(error == null ? "<non-Exception object>" : error.ToString());
			report.AppendLine();
			report.AppendLine("[Recent Unity warnings/errors]");
			lock (sync)
			{
				for (int i = 0; i < recentIssues.Count; i++)
				{
					report.AppendLine(recentIssues[i]);
					report.AppendLine();
				}
			}
			File.WriteAllText(path, report.ToString(), new UTF8Encoding(false));
			return path;
		}

		private static string TryWriteManagedDump()
		{
			try
			{
				string dumpDirectory = Path.Combine(logsDirectory, "CrashDumps");
				Directory.CreateDirectory(dumpDirectory);
				string path = Path.Combine(dumpDirectory, "McnCraft_" + sessionStamp + "_pid"
					+ Process.GetCurrentProcess().Id + "_managed.dmp");
				using (FileStream dump = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read))
				{
					Process process = Process.GetCurrentProcess();
					if (!MiniDumpWriteDump(process.Handle, (uint)process.Id, dump.SafeFileHandle,
						MiniDumpFlags, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero))
					{
						int error = Marshal.GetLastWin32Error();
						dump.Close();
						try { File.Delete(path); } catch (Exception) { }
						return "<failed-win32-" + error + ">";
					}
				}
				return path;
			}
			catch (Exception error)
			{
				return "<failed-" + error.GetType().Name + ">";
			}
		}

		private static void WriteState(string state, string details)
		{
			if (string.IsNullOrEmpty(statePath))
				return;
			StringBuilder text = new StringBuilder();
			text.AppendLine("state=" + state);
			text.AppendLine("time=" + DateTime.Now.ToString("o"));
			text.AppendLine("pid=" + Process.GetCurrentProcess().Id);
			if (!string.IsNullOrEmpty(details))
				text.AppendLine("details=" + details.Replace("\r", "\\r").Replace("\n", "\\n"));
			File.WriteAllText(statePath, text.ToString(), new UTF8Encoding(false));
		}

		internal static void MarkCleanExit()
		{
			lock (sync)
			{
				if (!registered || cleanExit)
					return;
				cleanExit = true;
			}

			try { WriteState("clean", null); } catch (Exception) { }
			try
			{
				if (unityLogCallback != null)
					Application.logMessageReceivedThreaded -= unityLogCallback;
			}
			catch (Exception) { }
			try
			{
				if (unhandledExceptionCallback != null)
					AppDomain.CurrentDomain.UnhandledException -= unhandledExceptionCallback;
			}
			catch (Exception) { }
			lock (sync)
			{
				try
				{
					if (unityWriter != null)
					{
						unityWriter.WriteLine("=== Clean shutdown " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + " ===");
						unityWriter.Flush();
						unityWriter.Close();
						unityWriter = null;
					}
				}
				catch (Exception) { }
			}
			LogShared("CLEAN_EXIT state=" + Quote(statePath));
			global::mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.Flush();
		}

		private static void LogShared(string message)
		{
			try
			{
				global::mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(
					"[CRASH-DIAGNOSTICS] " + message);
			}
			catch (Exception) { }
		}

		private static string Quote(string value)
		{
			if (value == null)
				return "<null>";
			return "\"" + value.Replace("\\", "\\\\").Replace("\r", "\\r")
				.Replace("\n", "\\n").Replace("\"", "\\\"") + "\"";
		}
	}
}
