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
		internal string ErrorDetails;
		internal string LogPath;
		internal int ClosedGameProcessCount;
		internal int ForcedGameProcessCount;
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

	internal sealed class SupportDirectoryReset
	{
		internal string SupportPath;
		internal string SnapshotPath;
		internal string SnapshotFilePath;
		internal bool PreviousDirectory;
		internal bool PreviousFile;
		internal bool ReplacementCreated;
		internal bool Committed;
	}

	internal sealed class BackupRelocation
	{
		internal string Source;
		internal string Destination;
	}

	internal static class InstallerEngine
	{
		private const string ManifestFileName = "MPatcherFork.install.ini";
		private const string BackupDirectoryName = "MPatcherForkBackup";
		private const string SupportDirectoryName = "MPatcherFork";
		private const string SupportSnapshotDirectoryName = "MPatcherFork.install-previous";
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
				GameCloseResult gameClose = GameProcessController.CloseSelectedGame(root,
					delegate(string message) { Report(root, progress, message); });
				result.ClosedGameProcessCount = gameClose.ClosedProcessCount;
				result.ForcedGameProcessCount = gameClose.ForcedProcessCount;
				WaitForCrashWatchdog(root);
				MigrateLegacyLogs(root);
				Report(root, progress, "INSTALL_BEGIN version=" + PayloadInfo.Version);

				string monoDirectory = Path.Combine(root, "McnCraft_Data", "Mono");
				string loaderPath = GetLoaderPath(root);
				string manifestPath = GetManifestPath(root);
				string backupDirectory = Path.Combine(root, "McnCraft_Data", BackupDirectoryName);
				string supportDirectory = Path.Combine(root, "McnCraft_Data", SupportDirectoryName);
				string watchdogPath = GetWatchdogPath(root);
				Directory.CreateDirectory(backupDirectory);

				InstallManifest manifest = null;
				if (File.Exists(manifestPath))
					manifest = ReadManifest(manifestPath);
				SortedDictionary<string, string> knownBackups = ReadKnownBackups(root, manifest);

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

				SupportDirectoryReset supportReset = BeginSupportDirectoryReset(root, supportDirectory, progress);
				string stagedPayload = Path.Combine(monoDirectory, "__Internal.MPatcherFork.staged.dll");
				string stagedWatchdog = Path.Combine(supportDirectory, WatchdogFileName + ".staged");
				try
				{
					ExtractResource(PayloadInfo.ResourceName, stagedPayload);
					string stagedHash = ComputeSha256(stagedPayload);
					if (!string.Equals(stagedHash, PayloadInfo.Sha256, StringComparison.OrdinalIgnoreCase))
						throw new InvalidDataException(InstallerText.EmbeddedHashMismatch(stagedHash));
					ExtractResource(PayloadInfo.WatchdogResourceName, stagedWatchdog);
					string stagedWatchdogHash = ComputeSha256(stagedWatchdog);
					if (!string.Equals(stagedWatchdogHash, PayloadInfo.WatchdogSha256, StringComparison.OrdinalIgnoreCase))
						throw new InvalidDataException(InstallerText.EmbeddedHashMismatch(stagedWatchdogHash));

					string rollbackPath = Path.Combine(monoDirectory, "__Internal.MPatcherFork.rollback.dll");
					string manifestRollbackPath = manifestPath + ".rollback";
					DeleteIfExists(rollbackPath);
					DeleteIfExists(manifestRollbackPath);
					bool previousLoaderExisted = File.Exists(loaderPath);
					bool previousManifestExisted = File.Exists(manifestPath);
					bool loaderMovedToRollback = false;
					bool newLoaderInstalled = false;
					List<BackupRelocation> archivedBackups = new List<BackupRelocation>();
					string workDirectory = Path.Combine(root, "Work");
					bool workDirectoryCreated = false;
					try
					{
						if (previousManifestExisted)
							File.Copy(manifestPath, manifestRollbackPath, false);
						if (previousLoaderExisted)
						{
							File.SetAttributes(loaderPath, FileAttributes.Normal);
							File.Move(loaderPath, rollbackPath);
							loaderMovedToRollback = true;
						}
						File.Move(stagedPayload, loaderPath);
						newLoaderInstalled = true;
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
						ArchiveKnownBackups(root, backupDirectory, manifest.BackupFileName,
							knownBackups, archivedBackups, progress);
						if (!Directory.Exists(workDirectory))
						{
							Directory.CreateDirectory(workDirectory);
							workDirectoryCreated = true;
							Report(root, progress, "SCRIPT_WORK_CREATED directory=Work");
						}

#if MPATCHER_ZAPRET_PACKAGE
						NetworkAssistSetup.Install(root);
						Report(root, progress, "NETWORK_ASSIST_INSTALLED defaultEnabled=false profile=Legacy-services-only");
#endif
						DeleteIfExists(rollbackPath);
						DeleteIfExists(manifestRollbackPath);
					}
					catch
					{
						RollbackBackupArchive(root, archivedBackups, progress);
						if (workDirectoryCreated && Directory.Exists(workDirectory)
							&& Directory.GetFileSystemEntries(workDirectory).Length == 0)
							Directory.Delete(workDirectory, false);
						if (newLoaderInstalled)
							DeleteIfExists(loaderPath);
						if (loaderMovedToRollback && File.Exists(rollbackPath))
							File.Move(rollbackPath, loaderPath);
						if (File.Exists(manifestRollbackPath))
						{
							DeleteIfExists(manifestPath);
							File.Move(manifestRollbackPath, manifestPath);
						}
						else if (!previousManifestExisted)
							DeleteIfExists(manifestPath);
						throw;
					}
					finally
					{
						DeleteIfExists(manifestRollbackPath);
					}
					CommitSupportDirectoryReset(root, supportReset, progress);
				}
				finally
				{
					DeleteIfExists(stagedPayload);
					DeleteIfExists(stagedWatchdog);
					if (!supportReset.Committed)
						RollbackSupportDirectoryReset(root, supportReset, progress);
				}

				Report(root, progress, "INSTALL_OK sha256=" + PayloadInfo.Sha256
					+ " watchdogSha256=" + PayloadInfo.WatchdogSha256);
				result.Success = true;
				result.Message = InstallerText.InstallSucceeded(PayloadInfo.Version, root,
					result.ClosedGameProcessCount, result.ForcedGameProcessCount);
			}
			catch (Exception error)
			{
				SafeReport(root, progress, "INSTALL_FAILED type=" + error.GetType().Name + " message=" + error.Message);
				result.Success = false;
				result.ErrorDetails = error.Message;
				result.Message = InstallerText.InstallFailed(error.Message);
				if (string.IsNullOrEmpty(result.LogPath))
					result.LogPath = GetFallbackLogPath();
			}
			return result;
		}

		private static SupportDirectoryReset BeginSupportDirectoryReset(string root,
			string supportDirectory, Action<string> progress)
		{
			string dataDirectory = Path.Combine(root, "McnCraft_Data");
			SupportDirectoryReset reset = new SupportDirectoryReset();
			reset.SupportPath = supportDirectory;
			reset.SnapshotPath = Path.Combine(dataDirectory, SupportSnapshotDirectoryName);
			reset.SnapshotFilePath = reset.SnapshotPath + ".file";
			ValidateManagedSupportPath(root, reset.SupportPath);
			ValidateManagedSupportPath(root, reset.SnapshotPath);
			ValidateManagedSupportPath(root, reset.SnapshotFilePath);
			Report(root, progress, "SUPPORT_RESET_BEGIN path=" + supportDirectory);
			try
			{
				DeleteManagedSupportPath(root, reset.SnapshotPath);
				DeleteManagedSupportPath(root, reset.SnapshotFilePath);
				if (Directory.Exists(supportDirectory))
				{
					Directory.Move(supportDirectory, reset.SnapshotPath);
					reset.PreviousDirectory = true;
				}
				else if (File.Exists(supportDirectory))
				{
					File.SetAttributes(supportDirectory, FileAttributes.Normal);
					File.Move(supportDirectory, reset.SnapshotFilePath);
					reset.PreviousFile = true;
				}
				Directory.CreateDirectory(supportDirectory);
				reset.ReplacementCreated = true;
				Report(root, progress, "SUPPORT_RESET_READY previous="
					+ (reset.PreviousDirectory ? "directory" : reset.PreviousFile ? "file" : "none"));
				return reset;
			}
			catch
			{
				RollbackSupportDirectoryReset(root, reset, progress);
				throw;
			}
		}

		private static void CommitSupportDirectoryReset(string root,
			SupportDirectoryReset reset, Action<string> progress)
		{
			reset.Committed = true;
			try
			{
				DeleteManagedSupportPath(root, reset.SnapshotPath);
				DeleteManagedSupportPath(root, reset.SnapshotFilePath);
				Report(root, progress, "SUPPORT_RESET_COMMIT");
			}
			catch (Exception error)
			{
				SafeReport(root, progress, "SUPPORT_RESET_CLEANUP_FAILED type="
					+ error.GetType().Name + " message=" + error.Message);
			}
		}

		private static void RollbackSupportDirectoryReset(string root,
			SupportDirectoryReset reset, Action<string> progress)
		{
			try
			{
				if (reset.ReplacementCreated || reset.PreviousDirectory || reset.PreviousFile)
					DeleteManagedSupportPath(root, reset.SupportPath);
				if (reset.PreviousDirectory && Directory.Exists(reset.SnapshotPath))
					Directory.Move(reset.SnapshotPath, reset.SupportPath);
				else if (reset.PreviousFile && File.Exists(reset.SnapshotFilePath))
					File.Move(reset.SnapshotFilePath, reset.SupportPath);
				SafeReport(root, progress, "SUPPORT_RESET_ROLLBACK");
			}
			catch (Exception error)
			{
				SafeReport(root, progress, "SUPPORT_RESET_ROLLBACK_FAILED type="
					+ error.GetType().Name + " message=" + error.Message);
			}
		}

		private static void DeleteManagedSupportPath(string root, string path)
		{
			ValidateManagedSupportPath(root, path);
			if (Directory.Exists(path))
			{
				FileAttributes attributes = File.GetAttributes(path);
				Directory.Delete(path, (attributes & FileAttributes.ReparsePoint) == 0);
			}
			else if (File.Exists(path))
			{
				File.SetAttributes(path, FileAttributes.Normal);
				File.Delete(path);
			}
		}

		private static void ValidateManagedSupportPath(string root, string path)
		{
			string dataDirectory = Path.GetFullPath(Path.Combine(root, "McnCraft_Data"));
			string fullPath = Path.GetFullPath(path);
			if (!string.Equals(Path.GetDirectoryName(fullPath), dataDirectory,
				StringComparison.OrdinalIgnoreCase))
				throw new InvalidOperationException("Refusing support path outside McnCraft_Data: " + fullPath);
			string name = Path.GetFileName(fullPath);
			if (!string.Equals(name, SupportDirectoryName, StringComparison.OrdinalIgnoreCase)
				&& !string.Equals(name, SupportSnapshotDirectoryName, StringComparison.OrdinalIgnoreCase)
				&& !string.Equals(name, SupportSnapshotDirectoryName + ".file", StringComparison.OrdinalIgnoreCase))
				throw new InvalidOperationException("Refusing unmanaged support path: " + fullPath);
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
				GameCloseResult gameClose = GameProcessController.CloseSelectedGame(root,
					delegate(string message) { Report(root, progress, message); });
				result.ClosedGameProcessCount = gameClose.ClosedProcessCount;
				result.ForcedGameProcessCount = gameClose.ForcedProcessCount;
				WaitForCrashWatchdog(root);
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
						result.Message = InstallerText.UninstallSucceeded(InstallerText.UnmanagedPayloadRemoved,
							root, result.ClosedGameProcessCount, result.ForcedGameProcessCount);
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

#if MPATCHER_ZAPRET_PACKAGE
				NetworkAssistSetup.Uninstall(root);
				Report(root, progress, "NETWORK_ASSIST_UNREGISTERED otherGamesPreserved=true");
#endif
				DeleteIfExists(manifestPath);
				Report(root, progress, "UNINSTALL_OK");
				result.Success = true;
				result.Message = InstallerText.UninstallSucceeded(result.Message, root,
					result.ClosedGameProcessCount, result.ForcedGameProcessCount);
			}
			catch (Exception error)
			{
				SafeReport(root, progress, "UNINSTALL_FAILED type=" + error.GetType().Name + " message=" + error.Message);
				result.Success = false;
				result.ErrorDetails = error.Message;
				result.Message = InstallerText.UninstallFailed(error.Message);
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

		private static void WaitForCrashWatchdog(string root)
		{
			string watchdogPath = GetWatchdogPath(root);
			Process[] watchdogs = Process.GetProcessesByName("MPatcherCrashWatchdog");
			for (int i = 0; i < watchdogs.Length; i++)
			{
				try
				{
					if (watchdogs[i].HasExited) continue;
					ProcessModule module = watchdogs[i].MainModule;
					if (module == null)
					{
						if (watchdogs[i].HasExited) continue;
						throw new InvalidOperationException(InstallerText.CrashWatchdogBusy);
					}
					string executable = module.FileName;
					if (!string.Equals(Path.GetFullPath(executable), Path.GetFullPath(watchdogPath),
						StringComparison.OrdinalIgnoreCase))
						continue;
					if (!watchdogs[i].WaitForExit(20000))
						throw new InvalidOperationException(InstallerText.CrashWatchdogBusy);
				}
				catch (System.ComponentModel.Win32Exception)
				{
					if (!watchdogs[i].HasExited) throw;
				}
				catch (InvalidOperationException)
				{
					if (!watchdogs[i].HasExited) throw;
				}
				finally
				{
					watchdogs[i].Dispose();
				}
			}
		}

		private static SortedDictionary<string, string> ReadKnownBackups(string root, InstallManifest manifest)
		{
			SortedDictionary<string, string> known = new SortedDictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			string log = GetInstallerLogPath(root);
			if (File.Exists(log))
			{
				Regex record = new Regex(@" (?:BACKUP_CREATED|EXTERNAL_LOADER_BACKUP_CREATED) sha256=([0-9A-Fa-f]{64}) file=([^\r\n]+)$");
				using (StreamReader reader = new StreamReader(new FileStream(log, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
				{
					string line;
					while ((line = reader.ReadLine()) != null)
					{
						Match match = record.Match(line);
						if (match.Success) RememberBackup(known, match.Groups[2].Value, match.Groups[1].Value);
					}
				}
			}
			if (manifest != null && manifest.OriginalExisted)
				RememberBackup(known, manifest.BackupFileName, manifest.OriginalSha256);
			return known;
		}

		private static void RememberBackup(SortedDictionary<string, string> known, string name, string hash)
		{
			// The archive is limited to exact installer records and verified bytes.
			// Filename shape alone never authorizes moving a DLL out of validation.
			if (string.IsNullOrEmpty(name) || name.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0
				|| name.IndexOfAny(new char[] { '/', '\\', ':' }) >= 0
				|| !name.StartsWith("__Internal.", StringComparison.OrdinalIgnoreCase)
				|| !name.EndsWith(".dll", StringComparison.OrdinalIgnoreCase)
				|| string.IsNullOrEmpty(hash) || !Regex.IsMatch(hash, @"\A[0-9A-Fa-f]{64}\z")) return;
			string previous;
			if (known.TryGetValue(name, out previous) && !string.Equals(previous, hash, StringComparison.OrdinalIgnoreCase))
				known[name] = string.Empty;
			else known[name] = hash;
		}

		private static void ArchiveKnownBackups(string root, string backupDirectory, string currentBackup,
			SortedDictionary<string, string> known, List<BackupRelocation> moved, Action<string> progress)
		{
			foreach (KeyValuePair<string, string> item in known)
			{
				if (string.Equals(item.Key, currentBackup, StringComparison.OrdinalIgnoreCase) || item.Value.Length == 0) continue;
				string source = Path.Combine(backupDirectory, item.Key);
				if (!File.Exists(source)) continue;
				string hash = ComputeSha256(source);
				if (!string.Equals(hash, item.Value, StringComparison.OrdinalIgnoreCase))
				{
					Report(root, progress, "BACKUP_HISTORY_RETAINED reason=hash-mismatch file=" + item.Key);
					continue;
				}
				string archive = Path.Combine(root, "MPatcherBackupHistory");
				Directory.CreateDirectory(archive);
				string destination = Path.Combine(archive, item.Key);
				int suffix = 2;
				while (File.Exists(destination) || Directory.Exists(destination))
					destination = Path.Combine(archive, Path.GetFileNameWithoutExtension(item.Key) + "."
						+ (suffix++).ToString(CultureInfo.InvariantCulture) + ".dll");
				File.Move(source, destination);
				moved.Add(new BackupRelocation { Source = source, Destination = destination });
				Report(root, progress, "BACKUP_HISTORY_ARCHIVED sha256=" + hash + " file=" + item.Key
					+ " destination=" + destination);
			}
		}

		private static void RollbackBackupArchive(string root, List<BackupRelocation> moved, Action<string> progress)
		{
			for (int index = moved.Count - 1; index >= 0; index--)
			{
				BackupRelocation item = moved[index];
				try
				{
					File.Move(item.Destination, item.Source);
					SafeReport(root, progress, "BACKUP_HISTORY_ROLLBACK file=" + Path.GetFileName(item.Source));
				}
				catch (Exception error)
				{
					SafeReport(root, progress, "BACKUP_HISTORY_ROLLBACK_FAILED archive=" + item.Destination + " message=" + error.Message);
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

		internal static void LogAutomaticEvent(string root, string message)
		{
			Report(root, null, message);
		}
	}
}
