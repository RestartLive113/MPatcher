using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace MachineCraftMPatcherInstaller
{
	internal sealed class GameProcessCloseException : Exception
	{
		internal readonly int ProcessId;

		internal GameProcessCloseException(int processId, string message, Exception inner)
			: base(message, inner)
		{
			ProcessId = processId;
		}
	}

	internal sealed class GameCloseResult
	{
		internal int ClosedProcessCount;
		internal int ForcedProcessCount;
	}

	internal static class GameProcessController
	{
		private const uint ProcessQueryLimitedInformation = 0x1000;
		private const int GracefulCloseTimeoutMilliseconds = 8000;
		private const int ForcedCloseTimeoutMilliseconds = 5000;

		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern IntPtr OpenProcess(uint desiredAccess, bool inheritHandle, int processId);

		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool QueryFullProcessImageName(IntPtr process, int flags,
			StringBuilder executableName, ref int size);

		[DllImport("kernel32.dll")]
		private static extern bool CloseHandle(IntPtr handle);

		internal static GameCloseResult CloseSelectedGame(string root, Action<string> report)
		{
			GameCloseResult result = new GameCloseResult();
			string expectedExecutable = Path.GetFullPath(Path.Combine(root, "McnCraft.exe"));
			Process[] processes = Process.GetProcessesByName("McnCraft");
			List<Process> selected = new List<Process>();
			try
			{
				for (int i = 0; i < processes.Length; i++)
				{
					Process process = processes[i];
					if (HasExited(process))
						continue;
					Exception pathError;
					string executable = TryGetExecutablePath(process, out pathError);
					if (string.IsNullOrEmpty(executable))
					{
						if (HasExited(process))
							continue;
						Emit(report, "GAME_PROCESS_PATH_FAILED pid=" + Invariant(process.Id)
							+ " type=" + (pathError == null ? "Unknown" : pathError.GetType().Name));
						throw new GameProcessCloseException(process.Id,
							InstallerText.GameProcessPathUnavailable(process.Id), pathError);
					}
					if (string.Equals(Path.GetFullPath(executable), expectedExecutable,
						StringComparison.OrdinalIgnoreCase))
						selected.Add(process);
				}

				for (int i = 0; i < selected.Count; i++)
				{
					Process process = selected[i];
					if (HasExited(process))
						continue;
					bool forced = CloseOne(process, expectedExecutable, report);
					result.ClosedProcessCount++;
					if (forced)
						result.ForcedProcessCount++;
				}
			}
			finally
			{
				for (int i = 0; i < processes.Length; i++)
					processes[i].Dispose();
			}
			return result;
		}

		private static bool CloseOne(Process process, string executable, Action<string> report)
		{
			int processId = process.Id;
			Emit(report, "GAME_PROCESS_FOUND pid=" + Invariant(processId) + " executable=" + executable);
			Emit(report, InstallerText.ClosingGameProcess(processId));

			bool closeRequested = false;
			try
			{
				process.Refresh();
				closeRequested = process.CloseMainWindow();
			}
			catch (InvalidOperationException)
			{
				if (HasExited(process))
					return false;
			}
			catch (Exception error)
			{
				Emit(report, "GAME_CLOSE_WINDOW_FAILED pid=" + Invariant(processId)
					+ " type=" + error.GetType().Name + " message=" + OneLine(error.Message));
			}

			if (closeRequested)
			{
				Emit(report, "GAME_CLOSE_REQUESTED pid=" + Invariant(processId) + " method=window");
				if (WaitForExit(process, GracefulCloseTimeoutMilliseconds))
				{
					Emit(report, "GAME_CLOSE_OK pid=" + Invariant(processId) + " forced=False");
					Emit(report, InstallerText.GameProcessClosed(false));
					return false;
				}
			}

			string forceReason = closeRequested ? "graceful-timeout" : "no-main-window";
			Emit(report, "GAME_CLOSE_FORCE_BEGIN pid=" + Invariant(processId) + " reason=" + forceReason);
			try
			{
				if (!HasExited(process))
					process.Kill();
			}
			catch (Exception error)
			{
				if (!HasExited(process))
				{
					Emit(report, "GAME_CLOSE_FAILED pid=" + Invariant(processId)
						+ " type=" + error.GetType().Name + " message=" + OneLine(error.Message));
					throw new GameProcessCloseException(processId,
						InstallerText.GameProcessCloseFailed(processId, error.Message), error);
				}
			}

			if (!WaitForExit(process, ForcedCloseTimeoutMilliseconds))
			{
				Emit(report, "GAME_CLOSE_FAILED pid=" + Invariant(processId) + " reason=force-timeout");
				throw new GameProcessCloseException(processId,
					InstallerText.GameProcessCloseFailed(processId, "timeout"), null);
			}
			Emit(report, "GAME_CLOSE_OK pid=" + Invariant(processId) + " forced=True");
			Emit(report, InstallerText.GameProcessClosed(true));
			return true;
		}

		private static string TryGetExecutablePath(Process process, out Exception error)
		{
			error = null;
			try
			{
				return process.MainModule.FileName;
			}
			catch (Exception firstError)
			{
				error = firstError;
			}

			IntPtr handle = IntPtr.Zero;
			try
			{
				handle = OpenProcess(ProcessQueryLimitedInformation, false, process.Id);
				if (handle == IntPtr.Zero)
				{
					error = new Win32Exception(Marshal.GetLastWin32Error());
					return string.Empty;
				}
				StringBuilder path = new StringBuilder(32768);
				int length = path.Capacity;
				if (!QueryFullProcessImageName(handle, 0, path, ref length))
				{
					error = new Win32Exception(Marshal.GetLastWin32Error());
					return string.Empty;
				}
				return path.ToString();
			}
			catch (Exception fallbackError)
			{
				error = fallbackError;
				return string.Empty;
			}
			finally
			{
				if (handle != IntPtr.Zero)
					CloseHandle(handle);
			}
		}

		private static bool WaitForExit(Process process, int timeoutMilliseconds)
		{
			try
			{
				return process.HasExited || process.WaitForExit(timeoutMilliseconds);
			}
			catch (InvalidOperationException)
			{
				return true;
			}
		}

		private static bool HasExited(Process process)
		{
			try { return process.HasExited; }
			catch (InvalidOperationException) { return true; }
		}

		private static string Invariant(int value)
		{
			return value.ToString(CultureInfo.InvariantCulture);
		}

		private static void Emit(Action<string> report, string message)
		{
			if (report != null && !string.IsNullOrEmpty(message))
				report(message);
		}

		private static string OneLine(string value)
		{
			return (value ?? string.Empty).Replace('\r', ' ').Replace('\n', ' ');
		}
	}
}
