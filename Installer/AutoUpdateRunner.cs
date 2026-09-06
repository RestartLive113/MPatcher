using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;

namespace MachineCraftMPatcherInstaller
{
	internal static class AutoUpdateRunner
	{
		private const int WaitTimeoutMilliseconds = 300000;
		private const string ResultFileName = "MPatcherUpdate.result.ini";

		internal static int Run(string root, int waitPid, bool restartGame, string restartManifestUrl)
		{
			bool targetExited = false;
			OperationResult result = null;
			try
			{
				if (!InstallerEngine.IsGameRoot(root))
					throw new DirectoryNotFoundException(InstallerText.SelectGameFolder);
				root = Path.GetFullPath(root.Trim());
				InstallerEngine.LogAutomaticEvent(root, "AUTO_UPDATE_BEGIN version=" + PayloadInfo.Version
					+ " waitPid=" + waitPid.ToString(CultureInfo.InvariantCulture)
					+ " restart=" + restartGame);
				WaitForGame(root, waitPid);
				targetExited = true;
				Thread.Sleep(500);
				result = InstallerEngine.Install(root, null);
				WriteResult(root, result.Success, result.Message);
				InstallerEngine.LogAutomaticEvent(root, result.Success
					? "AUTO_UPDATE_OK version=" + PayloadInfo.Version
					: "AUTO_UPDATE_FAILED message=" + OneLine(result.Message));
			}
			catch (Exception error)
			{
				if (InstallerEngine.IsGameRoot(root))
				{
					root = Path.GetFullPath(root.Trim());
					try { InstallerEngine.LogAutomaticEvent(root, "AUTO_UPDATE_FAILED type=" + error.GetType().Name
						+ " message=" + OneLine(error.Message)); } catch { }
					try { WriteResult(root, false, error.Message); } catch { }
				}
				return 1;
			}

			if (restartGame && targetExited)
			{
				try
				{
					RestartGame(root, restartManifestUrl);
					InstallerEngine.LogAutomaticEvent(root, "AUTO_UPDATE_RESTARTED executable="
						+ Path.Combine(root, "McnCraft.exe"));
				}
				catch (Exception error)
				{
					InstallerEngine.LogAutomaticEvent(root, "AUTO_UPDATE_RESTART_FAILED type=" + error.GetType().Name
						+ " message=" + OneLine(error.Message));
					return 1;
				}
			}
			return result != null && result.Success ? 0 : 1;
		}

		private static void WaitForGame(string root, int processId)
		{
			if (processId <= 0 || processId == Process.GetCurrentProcess().Id)
				throw new ArgumentOutOfRangeException("processId");
			Process process;
			try
			{
				process = Process.GetProcessById(processId);
			}
			catch (ArgumentException)
			{
				InstallerEngine.LogAutomaticEvent(root, "AUTO_UPDATE_WAIT alreadyExited pid=" + processId);
				return;
			}

			using (process)
			{
				string expected = Path.GetFullPath(Path.Combine(root, "McnCraft.exe"));
				string actual = Path.GetFullPath(process.MainModule.FileName);
				if (!string.Equals(actual, expected, StringComparison.OrdinalIgnoreCase))
					throw new InvalidOperationException("wait PID is not the selected MachineCraft process");
				InstallerEngine.LogAutomaticEvent(root, "AUTO_UPDATE_WAIT pid=" + processId);
				if (!process.WaitForExit(WaitTimeoutMilliseconds))
					throw new TimeoutException("MachineCraft did not close within five minutes");
			}
			InstallerEngine.LogAutomaticEvent(root, "AUTO_UPDATE_WAIT_OK pid=" + processId);
		}

		private static void RestartGame(string root, string restartManifestUrl)
		{
			ProcessStartInfo start = new ProcessStartInfo();
			start.FileName = Path.Combine(root, "McnCraft.exe");
			start.WorkingDirectory = root;
			start.UseShellExecute = true;
			Uri manifestUri;
			if (!string.IsNullOrWhiteSpace(restartManifestUrl)
				&& Uri.TryCreate(restartManifestUrl, UriKind.Absolute, out manifestUri)
				&& IsLoopbackTestUri(manifestUri))
				start.Arguments = "--mpatcher-update-manifest " + QuoteArgument(manifestUri.AbsoluteUri);
			Process process = Process.Start(start);
			if (process == null)
				throw new InvalidOperationException("MachineCraft process was not created");
			process.Dispose();
		}

		private static bool IsLoopbackTestUri(Uri uri)
		{
			if (uri == null)
				return false;
			if (uri.IsFile)
				return true;
			return (string.Equals(uri.Scheme, Uri.UriSchemeHttp, StringComparison.OrdinalIgnoreCase)
					|| string.Equals(uri.Scheme, Uri.UriSchemeHttps, StringComparison.OrdinalIgnoreCase))
				&& (uri.IsLoopback || string.Equals(uri.Host, "localhost", StringComparison.OrdinalIgnoreCase));
		}

		private static void WriteResult(string root, bool success, string message)
		{
			string directory = Path.Combine(root, "McnCraft_Data", "MPatcherFork");
			Directory.CreateDirectory(directory);
			string path = Path.Combine(directory, ResultFileName);
			string temporary = path + ".tmp";
			if (File.Exists(temporary)) File.Delete(temporary);
			string[] lines = new string[]
			{
				"Format=1",
				"Success=" + success.ToString(CultureInfo.InvariantCulture),
				"Version=" + PayloadInfo.Version,
				"CompletedUtc=" + DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture),
				"Message=" + OneLine(message)
			};
			File.WriteAllLines(temporary, lines, new UTF8Encoding(false));
			if (File.Exists(path)) File.Delete(path);
			File.Move(temporary, path);
		}

		private static string QuoteArgument(string value)
		{
			return "\"" + value.Replace("\"", "\\\"") + "\"";
		}

		private static string OneLine(string value)
		{
			if (string.IsNullOrEmpty(value))
				return string.Empty;
			return value.Replace('\r', ' ').Replace('\n', ' ');
		}
	}
}
