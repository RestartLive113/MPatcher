using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Win32;

namespace MachineCraftMPatcherInstaller
{
	internal sealed class InstallState
	{
		internal bool ValidGame;
		internal bool LoaderExists;
		internal bool ManagedInstall;
		internal bool CurrentPayload;
		internal string LoaderHash;
		internal string Description;
	}

	internal sealed class OperationResult
	{
		internal bool Success;
		internal string Message;
		internal string LogPath;
	}

	internal sealed class InstallManifest
	{
		internal string ProductVersion;
		internal string InstalledPayloadSha256;
		internal string InstalledWatchdogSha256;
		internal string InstalledUtc;
		internal bool OriginalExisted;
		internal string BackupFileName;
		internal string OriginalSha256;
	}

	internal static class InstallerEngine
	{
		private const string ManifestFileName = "MPatcherFork.install.ini";
		private const string BackupDirectoryName = "MPatcherForkBackup";
		private const string SupportDirectoryName = "MPatcherFork";
		private const string WatchdogFileName = "MPatcherCrashWatchdog.exe";
		private static readonly object LogSync = new object();

		internal static bool IsGameRoot(string root)
		{
			if (string.IsNullOrWhiteSpace(root))
				return false;
			try
			{
				string normalized = Path.GetFullPath(root.Trim());
				return File.Exists(Path.Combine(normalized, "McnCraft.exe"))
					&& File.Exists(Path.Combine(normalized, "McnCraft_Data", "Mono", "mono.dll"));
			}
			catch
			{
				return false;
			}
		}

		internal static string AutoDetectGameRoot()
		{
			List<string> candidates = new List<string>();
			AddCandidate(candidates, Environment.CurrentDirectory);
			AddCandidate(candidates, AppDomain.CurrentDomain.BaseDirectory);

			string[] registryKeys = new string[]
			{
				@"HKEY_CURRENT_USER\Software\Valve\Steam",
				@"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Valve\Steam",
				@"HKEY_LOCAL_MACHINE\SOFTWARE\Valve\Steam"
			};
			for (int i = 0; i < registryKeys.Length; i++)
			{
				object value = Registry.GetValue(registryKeys[i], "SteamPath", null);
				if (value == null)
					value = Registry.GetValue(registryKeys[i], "InstallPath", null);
				if (value != null)
					AddSteamRoot(candidates, value.ToString());
			}

			string programFilesX86 = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
			if (!string.IsNullOrEmpty(programFilesX86))
				AddSteamRoot(candidates, Path.Combine(programFilesX86, "Steam"));

			for (int i = 0; i < candidates.Count; i++)
			{
				if (IsGameRoot(candidates[i]))
					return Path.GetFullPath(candidates[i]);
			}
			return string.Empty;
		}

		private static void AddSteamRoot(List<string> candidates, string steamRoot)
		{
			if (string.IsNullOrWhiteSpace(steamRoot))
				return;
			steamRoot = steamRoot.Replace('/', Path.DirectorySeparatorChar);
			AddCandidate(candidates, Path.Combine(steamRoot, "steamapps", "common", "MachineCraft"));

			string libraryFile = Path.Combine(steamRoot, "steamapps", "libraryfolders.vdf");
			if (!File.Exists(libraryFile))
				return;
			try
			{
				string text = File.ReadAllText(libraryFile);
				MatchCollection matches = Regex.Matches(text, "\\\"path\\\"\\s+\\\"([^\\\"]+)\\\"", RegexOptions.IgnoreCase);
				for (int i = 0; i < matches.Count; i++)
				{
					string path = matches[i].Groups[1].Value.Replace("\\\\", "\\");
					AddCandidate(candidates, Path.Combine(path, "steamapps", "common", "MachineCraft"));
				}
			}
			catch
			{
			}
		}

		private static void AddCandidate(List<string> candidates, string path)
		{
			if (string.IsNullOrWhiteSpace(path))
				return;
			try
			{
				string full = Path.GetFullPath(path.Trim());
				for (int i = 0; i < candidates.Count; i++)
				{
					if (string.Equals(candidates[i], full, StringComparison.OrdinalIgnoreCase))
						return;
				}
				candidates.Add(full);
			}
			catch
			{
			}
		}

		internal static InstallState Probe(string root)
		{
			InstallState state = new InstallState();
			state.ValidGame = IsGameRoot(root);
			if (!state.ValidGame)
			{
				state.Description = InstallerText.GameFolderNotFound;
				return state;
			}

			root = Path.GetFullPath(root);
			string loader = GetLoaderPath(root);
			string manifest = GetManifestPath(root);
			state.LoaderExists = File.Exists(loader);
			state.ManagedInstall = File.Exists(manifest);
			if (state.LoaderExists)
			{
				state.LoaderHash = ComputeSha256(loader);
				string watchdog = GetWatchdogPath(root);
				state.CurrentPayload = string.Equals(state.LoaderHash, PayloadInfo.Sha256, StringComparison.OrdinalIgnoreCase)
					&& File.Exists(watchdog)
					&& string.Equals(ComputeSha256(watchdog), PayloadInfo.WatchdogSha256, StringComparison.OrdinalIgnoreCase);
			}

			if (state.CurrentPayload)
				state.Description = InstallerText.CurrentVersionInstalled(PayloadInfo.Version);
			else if (state.ManagedInstall)
				state.Description = InstallerText.UpdateAvailable;
			else if (state.LoaderExists)
				state.Description = InstallerText.ExistingMPatcherDetected;
			else
				state.Description = InstallerText.CleanGameDetected;
			return state;
		}

		internal static OperationResult Install(string root, Action<string> progress)
		{
			OperationResult result = new OperationResult();
			try
			{
				root = NormalizeAndValidateRoot(root);
				result.LogPath = GetInstallerLogPath(root);
				EnsureGameIsClosed(root);
				MigrateLegacyLogs(root);
				Report(root, progress, "INSTALL_BEGIN version=" + PayloadInfo.Version);

				string monoDirectory = Path.Combine(root, "McnCraft_Data", "Mono");
				string loaderPath = GetLoaderPath(root);
				string manifestPath = GetManifestPath(root);
				string backupDirectory = Path.Combine(root, "McnCraft_Data", BackupDirectoryName);
				string supportDirectory = Path.Combine(root, "McnCraft_Data", SupportDirectoryName);
				string watchdogPath = GetWatchdogPath(root);
				Directory.CreateDirectory(backupDirectory);
				Directory.CreateDirectory(supportDirectory);

				InstallManifest manifest = null;
				if (File.Exists(manifestPath))
					manifest = ReadManifest(manifestPath);

				string currentHash = File.Exists(loaderPath) ? ComputeSha256(loaderPath) : string.Empty;
				if (manifest == null)
				{
					manifest = new InstallManifest();
					manifest.OriginalExisted = File.Exists(loaderPath)
						&& !string.Equals(currentHash, PayloadInfo.Sha256, StringComparison.OrdinalIgnoreCase);
					if (manifest.OriginalExisted)
					{
						manifest.OriginalSha256 = currentHash;
						manifest.BackupFileName = CreateBackupFileName(backupDirectory, currentHash);
						string backupPath = Path.Combine(backupDirectory, manifest.BackupFileName);
						File.Copy(loaderPath, backupPath, false);
						Report(root, progress, "BACKUP_CREATED sha256=" + currentHash + " file=" + manifest.BackupFileName);
					}
					else
					{
						Report(root, progress, File.Exists(loaderPath) ? "CURRENT_PAYLOAD_ADOPTED" : "CLEAN_GAME_DETECTED");
					}
				}
				else
				{
					bool currentLoaderIsManaged = File.Exists(loaderPath)
						&& (string.Equals(currentHash, manifest.InstalledPayloadSha256, StringComparison.OrdinalIgnoreCase)
							|| string.Equals(currentHash, PayloadInfo.Sha256, StringComparison.OrdinalIgnoreCase));
					if (File.Exists(loaderPath) && !currentLoaderIsManaged)
					{
						manifest.OriginalExisted = true;
						manifest.OriginalSha256 = currentHash;
						manifest.BackupFileName = CreateBackupFileName(backupDirectory, currentHash);
						string replacementBackup = Path.Combine(backupDirectory, manifest.BackupFileName);
						File.Copy(loaderPath, replacementBackup, false);
						Report(root, progress, "EXTERNAL_LOADER_BACKUP_CREATED sha256=" + currentHash + " file=" + manifest.BackupFileName);
					}
					else if (manifest.OriginalExisted)
					{
						string existingBackup = Path.Combine(backupDirectory, manifest.BackupFileName ?? string.Empty);
						if (!File.Exists(existingBackup))
							throw new InvalidOperationException(InstallerText.OriginalBackupMissing(existingBackup));
					}
					Report(root, progress, "MANAGED_UPDATE previousVersion=" + (manifest.ProductVersion ?? "unknown"));
				}

				string stagedPayload = Path.Combine(monoDirectory, "__Internal.MPatcherFork.staged.dll");
				ExtractResource(PayloadInfo.ResourceName, stagedPayload);
				string stagedHash = ComputeSha256(stagedPayload);
				if (!string.Equals(stagedHash, PayloadInfo.Sha256, StringComparison.OrdinalIgnoreCase))
					throw new InvalidDataException(InstallerText.EmbeddedHashMismatch(stagedHash));
				string stagedWatchdog = Path.Combine(supportDirectory, WatchdogFileName + ".staged");
				ExtractResource(PayloadInfo.WatchdogResourceName, stagedWatchdog);
				string stagedWatchdogHash = ComputeSha256(stagedWatchdog);
				if (!string.Equals(stagedWatchdogHash, PayloadInfo.WatchdogSha256, StringComparison.OrdinalIgnoreCase))
					throw new InvalidDataException(InstallerText.EmbeddedHashMismatch(stagedWatchdogHash));

				string rollbackPath = Path.Combine(monoDirectory, "__Internal.MPatcherFork.rollback.dll");
				string watchdogRollbackPath = Path.Combine(supportDirectory, WatchdogFileName + ".rollback");
				string manifestRollbackPath = manifestPath + ".rollback";
				DeleteIfExists(rollbackPath);
				DeleteIfExists(watchdogRollbackPath);
				DeleteIfExists(manifestRollbackPath);
				bool previousLoaderExisted = File.Exists(loaderPath);
				bool previousWatchdogExisted = File.Exists(watchdogPath);
				bool previousManifestExisted = File.Exists(manifestPath);
				try
				{
					if (previousManifestExisted)
						File.Copy(manifestPath, manifestRollbackPath, false);
					if (previousLoaderExisted)
					{
						File.SetAttributes(loaderPath, FileAttributes.Normal);
						File.Move(loaderPath, rollbackPath);
					}
					if (previousWatchdogExisted)
					{
						File.SetAttributes(watchdogPath, FileAttributes.Normal);
						File.Move(watchdogPath, watchdogRollbackPath);
					}
					File.Move(stagedPayload, loaderPath);
					File.Move(stagedWatchdog, watchdogPath);

					manifest.ProductVersion = PayloadInfo.Version;
					manifest.InstalledPayloadSha256 = PayloadInfo.Sha256;
					manifest.InstalledWatchdogSha256 = PayloadInfo.WatchdogSha256;
					manifest.InstalledUtc = DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture);
					WriteManifest(manifestPath, manifest);

					string installedHash = ComputeSha256(loaderPath);
					if (!string.Equals(installedHash, PayloadInfo.Sha256, StringComparison.OrdinalIgnoreCase))
						throw new InvalidDataException(InstallerText.InstalledHashMismatch(installedHash));
					string installedWatchdogHash = ComputeSha256(watchdogPath);
					if (!string.Equals(installedWatchdogHash, PayloadInfo.WatchdogSha256, StringComparison.OrdinalIgnoreCase))
						throw new InvalidDataException(InstallerText.InstalledHashMismatch(installedWatchdogHash));
					DeleteIfExists(rollbackPath);
					DeleteIfExists(watchdogRollbackPath);
					DeleteIfExists(manifestRollbackPath);
				}
				catch
				{
					DeleteIfExists(loaderPath);
					if (File.Exists(rollbackPath))
						File.Move(rollbackPath, loaderPath);
					DeleteIfExists(watchdogPath);
					if (File.Exists(watchdogRollbackPath))
						File.Move(watchdogRollbackPath, watchdogPath);
					DeleteIfExists(manifestPath);
					if (File.Exists(manifestRollbackPath))
						File.Move(manifestRollbackPath, manifestPath);
					throw;
				}
				finally
				{
					DeleteIfExists(stagedPayload);
					DeleteIfExists(stagedWatchdog);
					DeleteIfExists(manifestRollbackPath);
				}

				Report(root, progress, "INSTALL_OK sha256=" + PayloadInfo.Sha256
					+ " watchdogSha256=" + PayloadInfo.WatchdogSha256);
				result.Success = true;
				result.Message = InstallerText.InstallSucceeded(PayloadInfo.Version);
			}
			catch (Exception error)
			{
				SafeReport(root, progress, "INSTALL_FAILED type=" + error.GetType().Name + " message=" + error.Message);
				result.Success = false;
				result.Message = error.Message;
				if (string.IsNullOrEmpty(result.LogPath))
					result.LogPath = GetFallbackLogPath();
			}
			return result;
		}

		private static string CreateBackupFileName(string backupDirectory, string sha256)
		{
			string prefix = "__Internal.before-mpatcher."
				+ DateTime.UtcNow.ToString("yyyyMMdd-HHmmss", CultureInfo.InvariantCulture)
				+ "." + sha256.Substring(0, 12);
			string fileName = prefix + ".dll";
			int suffix = 2;
			while (File.Exists(Path.Combine(backupDirectory, fileName)))
			{
				fileName = prefix + "." + suffix.ToString(CultureInfo.InvariantCulture) + ".dll";
				suffix++;
			}
			return fileName;
		}

		internal static OperationResult Uninstall(string root, Action<string> progress)
		{
			OperationResult result = new OperationResult();
			try
			{
				root = NormalizeAndValidateRoot(root);
				result.LogPath = GetInstallerLogPath(root);
				EnsureGameIsClosed(root);
				Report(root, progress, "UNINSTALL_BEGIN");

				string loaderPath = GetLoaderPath(root);
				string watchdogPath = GetWatchdogPath(root);
				string manifestPath = GetManifestPath(root);
				if (!File.Exists(manifestPath))
				{
					if (File.Exists(loaderPath)
						&& string.Equals(ComputeSha256(loaderPath), PayloadInfo.Sha256, StringComparison.OrdinalIgnoreCase))
					{
						File.SetAttributes(loaderPath, FileAttributes.Normal);
						File.Delete(loaderPath);
						DeleteCurrentWatchdog(root, watchdogPath, PayloadInfo.WatchdogSha256, progress);
						Report(root, progress, "UNINSTALL_OK mode=unmanaged-current-payload");
						result.Success = true;
						result.Message = InstallerText.UnmanagedPayloadRemoved;
						return result;
					}
					throw new InvalidOperationException(InstallerText.InstallManifestMissing);
				}

				InstallManifest manifest = ReadManifest(manifestPath);
				if (manifest == null)
					throw new InvalidDataException(InstallerText.ManifestReadFailed(manifestPath));
				if (File.Exists(loaderPath))
				{
					string currentHash = ComputeSha256(loaderPath);
					if (!string.Equals(currentHash, manifest.InstalledPayloadSha256, StringComparison.OrdinalIgnoreCase)
						&& !string.Equals(currentHash, PayloadInfo.Sha256, StringComparison.OrdinalIgnoreCase))
						throw new InvalidOperationException(InstallerText.LoaderChanged(currentHash));
				}

				if (manifest.OriginalExisted)
				{
					string backupPath = Path.Combine(root, "McnCraft_Data", BackupDirectoryName, manifest.BackupFileName ?? string.Empty);
					if (!File.Exists(backupPath))
						throw new FileNotFoundException(InstallerText.PreviousBackupMissing, backupPath);
					string backupHash = ComputeSha256(backupPath);
					if (!string.Equals(backupHash, manifest.OriginalSha256, StringComparison.OrdinalIgnoreCase))
						throw new InvalidDataException(InstallerText.BackupDamaged(backupHash));
					string stagedRestore = loaderPath + ".restore";
					DeleteIfExists(stagedRestore);
					File.Copy(backupPath, stagedRestore, false);
					DeleteIfExists(loaderPath);
					File.Move(stagedRestore, loaderPath);
					Report(root, progress, "RESTORED_PREVIOUS_MPATCHER sha256=" + backupHash);
					result.Message = InstallerText.PreviousMPatcherRestored;
				}
				else
				{
					DeleteIfExists(loaderPath);
					Report(root, progress, "RESTORED_CLEAN_GAME");
					result.Message = InstallerText.CleanGameRestored;
				}

				DeleteCurrentWatchdog(root, watchdogPath,
					string.IsNullOrEmpty(manifest.InstalledWatchdogSha256)
						? PayloadInfo.WatchdogSha256 : manifest.InstalledWatchdogSha256,
					progress);

				DeleteIfExists(manifestPath);
				Report(root, progress, "UNINSTALL_OK");
				result.Success = true;
			}
			catch (Exception error)
			{
				SafeReport(root, progress, "UNINSTALL_FAILED type=" + error.GetType().Name + " message=" + error.Message);
				result.Success = false;
				result.Message = error.Message;
				if (string.IsNullOrEmpty(result.LogPath))
					result.LogPath = GetFallbackLogPath();
			}
			return result;
		}

		private static string NormalizeAndValidateRoot(string root)
		{
			if (!IsGameRoot(root))
				throw new DirectoryNotFoundException(InstallerText.SelectGameFolder);
			return Path.GetFullPath(root.Trim());
		}

		private static void EnsureGameIsClosed(string root)
		{
			Process[] processes = Process.GetProcessesByName("McnCraft");
			for (int i = 0; i < processes.Length; i++)
			{
				try
				{
					string processRoot = Path.GetDirectoryName(processes[i].MainModule.FileName);
					if (string.Equals(Path.GetFullPath(processRoot), root, StringComparison.OrdinalIgnoreCase))
						throw new InvalidOperationException(InstallerText.CloseSelectedGame);
				}
				finally
				{
					processes[i].Dispose();
				}
			}

			string watchdogPath = GetWatchdogPath(root);
			Process[] watchdogs = Process.GetProcessesByName("MPatcherCrashWatchdog");
			for (int i = 0; i < watchdogs.Length; i++)
			{
				try
				{
					string executable = watchdogs[i].MainModule.FileName;
					if (!string.Equals(Path.GetFullPath(executable), Path.GetFullPath(watchdogPath),
						StringComparison.OrdinalIgnoreCase))
						continue;
					if (!watchdogs[i].WaitForExit(20000))
						throw new InvalidOperationException(InstallerText.CrashWatchdogBusy);
				}
				finally
				{
					watchdogs[i].Dispose();
				}
			}
		}

		private static void MigrateLegacyLogs(string root)
		{
			string dataDirectory = Path.Combine(root, "McnCraft_Data");
			string logsDirectory = Path.Combine(root, "logs");
			Directory.CreateDirectory(logsDirectory);
			List<string> files = new List<string>();
			try { files.AddRange(Directory.GetFiles(dataDirectory, "MPatcherFork*.log")); } catch { }
			string originalLog = Path.Combine(dataDirectory, "MPatcher.log");
			if (File.Exists(originalLog))
				files.Add(originalLog);
			for (int i = 0; i < files.Count; i++)
			{
				try
				{
					FileInfo item = new FileInfo(files[i]);
					string destinationName = "Legacy_" + item.LastWriteTime.ToString("yyyyMMdd_HHmmss",
						CultureInfo.InvariantCulture) + "_" + item.Name;
					string destination = Path.Combine(logsDirectory, destinationName);
					int suffix = 2;
					while (File.Exists(destination))
					{
						destination = Path.Combine(logsDirectory, Path.GetFileNameWithoutExtension(destinationName)
							+ "." + suffix.ToString(CultureInfo.InvariantCulture) + Path.GetExtension(destinationName));
						suffix++;
					}
					File.Move(item.FullName, destination);
				}
				catch
				{
				}
			}
		}

		private static void DeleteCurrentWatchdog(string root, string watchdogPath,
			string expectedHash, Action<string> progress)
		{
			if (!File.Exists(watchdogPath))
				return;
			string currentHash = ComputeSha256(watchdogPath);
			if (!string.Equals(currentHash, expectedHash, StringComparison.OrdinalIgnoreCase)
				&& !string.Equals(currentHash, PayloadInfo.WatchdogSha256, StringComparison.OrdinalIgnoreCase))
			{
				Report(root, progress, "WATCHDOG_RETAINED reason=file-changed sha256=" + currentHash);
				return;
			}
			DeleteIfExists(watchdogPath);
			Report(root, progress, "WATCHDOG_REMOVED sha256=" + currentHash);
			try
			{
				string directory = Path.GetDirectoryName(watchdogPath);
				if (Directory.Exists(directory) && Directory.GetFileSystemEntries(directory).Length == 0)
					Directory.Delete(directory, false);
			}
			catch { }
		}

		private static string GetLoaderPath(string root)
		{
			return Path.Combine(root, "McnCraft_Data", "Mono", "__Internal.dll");
		}

		private static string GetManifestPath(string root)
		{
			return Path.Combine(root, "McnCraft_Data", ManifestFileName);
		}

		private static string GetWatchdogPath(string root)
		{
			return Path.Combine(root, "McnCraft_Data", SupportDirectoryName, WatchdogFileName);
		}

		internal static string GetInstallerLogPath(string root)
		{
			if (IsGameRoot(root))
			{
				string directory = Path.Combine(Path.GetFullPath(root), "logs");
				Directory.CreateDirectory(directory);
				return Path.Combine(directory, "MPatcherInstaller.log");
			}
			return GetFallbackLogPath();
		}

		private static string GetFallbackLogPath()
		{
			string directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MPatcher", "logs");
			Directory.CreateDirectory(directory);
			return Path.Combine(directory, "MPatcherInstaller.log");
		}

		private static void ExtractResource(string resourceName, string destination)
		{
			DeleteIfExists(destination);
			using (Stream source = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
			{
				if (source == null)
					throw new MissingManifestResourceException(InstallerText.EmbeddedPayloadMissing(resourceName));
				using (FileStream target = new FileStream(destination, FileMode.CreateNew, FileAccess.Write, FileShare.None))
				{
					byte[] buffer = new byte[131072];
					int read;
					while ((read = source.Read(buffer, 0, buffer.Length)) > 0)
						target.Write(buffer, 0, read);
				}
			}
		}

		internal static string ComputeSha256(string path)
		{
			using (SHA256 hash = SHA256.Create())
			using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
			{
				byte[] value = hash.ComputeHash(stream);
				StringBuilder result = new StringBuilder(value.Length * 2);
				for (int i = 0; i < value.Length; i++)
					result.Append(value[i].ToString("X2", CultureInfo.InvariantCulture));
				return result.ToString();
			}
		}

		private static void WriteManifest(string path, InstallManifest manifest)
		{
			string temporary = path + ".tmp";
			DeleteIfExists(temporary);
			string[] lines = new string[]
			{
				"Format=2",
				"ProductVersion=" + (manifest.ProductVersion ?? string.Empty),
				"InstalledPayloadSha256=" + (manifest.InstalledPayloadSha256 ?? string.Empty),
				"InstalledWatchdogSha256=" + (manifest.InstalledWatchdogSha256 ?? string.Empty),
				"InstalledUtc=" + (manifest.InstalledUtc ?? string.Empty),
				"OriginalExisted=" + manifest.OriginalExisted.ToString(CultureInfo.InvariantCulture),
				"BackupFileName=" + (manifest.BackupFileName ?? string.Empty),
				"OriginalSha256=" + (manifest.OriginalSha256 ?? string.Empty)
			};
			File.WriteAllLines(temporary, lines, new UTF8Encoding(false));
			DeleteIfExists(path);
			File.Move(temporary, path);
		}

		private static InstallManifest ReadManifest(string path)
		{
			Dictionary<string, string> values = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			string[] lines = File.ReadAllLines(path);
			for (int i = 0; i < lines.Length; i++)
			{
				int separator = lines[i].IndexOf('=');
				if (separator > 0)
					values[lines[i].Substring(0, separator)] = lines[i].Substring(separator + 1);
			}
			if (!values.ContainsKey("Format") || (values["Format"] != "1" && values["Format"] != "2"))
				throw new InvalidDataException(InstallerText.UnsupportedManifest);
			InstallManifest manifest = new InstallManifest();
			manifest.ProductVersion = GetValue(values, "ProductVersion");
			manifest.InstalledPayloadSha256 = GetValue(values, "InstalledPayloadSha256");
			manifest.InstalledWatchdogSha256 = GetValue(values, "InstalledWatchdogSha256");
			manifest.InstalledUtc = GetValue(values, "InstalledUtc");
			manifest.OriginalExisted = string.Equals(GetValue(values, "OriginalExisted"), "True", StringComparison.OrdinalIgnoreCase);
			manifest.BackupFileName = GetValue(values, "BackupFileName");
			manifest.OriginalSha256 = GetValue(values, "OriginalSha256");
			return manifest;
		}

		private static string GetValue(Dictionary<string, string> values, string key)
		{
			string value;
			return values.TryGetValue(key, out value) ? value : string.Empty;
		}

		private static void DeleteIfExists(string path)
		{
			if (!File.Exists(path))
				return;
			File.SetAttributes(path, FileAttributes.Normal);
			File.Delete(path);
		}

		private static void Report(string root, Action<string> progress, string message)
		{
			string line = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture) + " " + message;
			string logPath = GetInstallerLogPath(root);
			lock (LogSync)
			{
				File.AppendAllText(logPath, line + Environment.NewLine, new UTF8Encoding(false));
			}
			if (progress != null)
				progress(message);
		}

		private static void SafeReport(string root, Action<string> progress, string message)
		{
			try
			{
				Report(root, progress, message);
			}
			catch
			{
				if (progress != null)
					progress(message);
			}
		}
	}
}
