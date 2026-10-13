using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
#if !MPATCHER_UPDATER_POLICY_ONLY
using MPatchrMain;
using UnityEngine;
#endif

namespace MPatcherFork.CustomPatches
{
#if !MPATCHER_UPDATER_POLICY_ONLY
	internal enum MPatcherUpdaterState
	{
		Idle,
		Checking,
		UpToDate,
		Available,
		Downloading,
		Launching,
		Failed,
		Updated
	}

	internal static class MPatcherUpdater
	{
		internal const string StableManifestUrl = "https://github.com/RestartLive113/MPatcher/releases/latest/download/MPatcherUpdate.ini";
		internal const string AlphaManifestUrl = "https://github.com/RestartLive113/MPatcher/releases/download/alpha/MPatcherUpdate.ini";
		private const float NetworkTimeoutSeconds = 30f;
		private const string ResultFileName = "MPatcherUpdate.result.ini";

		private static MPatchr runner;
		private static Uri manifestUri;
		private static MPatcherUpdateManifest availableManifest;
		private static bool started;
		private static bool automaticTestApply;
		private static int checkGeneration;
		private static string currentVersion = "0.0.0";
		private static MPatcherReleaseChannel currentBuildChannel = MPatcherReleaseChannel.Stable;
		private static MPatcherReleaseChannel selectedChannel = MPatcherReleaseChannel.Stable;
		private static string lastInstalledVersion = string.Empty;
		private static string statusDetail = string.Empty;
		private static float downloadProgress;
		private static MPatcherUpdaterState state = MPatcherUpdaterState.Idle;

		internal static MPatcherUpdaterState State { get { return state; } }
		internal static string CurrentVersion { get { return currentVersion; } }
		internal static string AvailableVersion { get { return availableManifest == null ? string.Empty : availableManifest.VersionText; } }
		internal static string LastInstalledVersion { get { return lastInstalledVersion; } }
		internal static string StatusDetail { get { return statusDetail; } }
		internal static float DownloadProgress { get { return downloadProgress; } }
		internal static MPatcherReleaseChannel SelectedChannel { get { return selectedChannel; } }

		internal static void TryStart(MPatchr owner)
		{
			if (started || owner == null)
				return;
			started = true;
			runner = owner;
			try
			{
				currentVersion = ReadCurrentVersion();
				Version parsedCurrent;
				if (!MPatcherUpdateManifest.TryParseVersion(currentVersion, out parsedCurrent))
					throw new InvalidOperationException("assembly release version is missing or invalid");

				currentBuildChannel = GetBuildChannel(currentVersion);
				selectedChannel = ReadSelectedChannel();
				manifestUri = ResolveManifestUri(selectedChannel);
				automaticTestApply = HasArgument("--mpatcher-auto-apply")
					&& MPatcherUpdateManifest.IsLoopbackTestUri(manifestUri);
				TryEnableTls12();
				Log("REGISTERED version=" + currentVersion + " buildChannel=" + currentBuildChannel
					+ " selectedChannel=" + selectedChannel + " manifest=" + manifestUri
					+ " testOverride=" + MPatcherUpdateManifest.IsLoopbackTestUri(manifestUri)
					+ " autoApply=" + automaticTestApply + " packageTimeout=first-byte60/stall60/total600");
				ReadPreviousResult();
				runner.StartCoroutine(CheckCoroutine(true));
			}
			catch (Exception error)
			{
				SetState(MPatcherUpdaterState.Failed, "startup: " + error.Message, 0f);
				Log("REGISTER_FAILED type=" + error.GetType().Name + " message=" + error.Message);
			}
		}

		internal static void ChangeChannel(MPatcherReleaseChannel channel)
		{
			if (selectedChannel == channel || runner == null)
				return;
			if (state == MPatcherUpdaterState.Downloading || state == MPatcherUpdaterState.Launching)
			{
				Log("CHANNEL_CHANGE_SKIPPED reason=update-in-progress requested=" + channel);
				MPatcherUpdaterUi.Refresh();
				return;
			}
			selectedChannel = channel;
			checkGeneration++;
			availableManifest = null;
			try
			{
				manifestUri = ResolveManifestUri(channel);
				Log("CHANNEL_CHANGED selected=" + channel + " manifest=" + manifestUri);
				SetState(MPatcherUpdaterState.Idle, string.Empty, 0f);
				runner.StartCoroutine(CheckCoroutine(false));
			}
			catch (Exception error)
			{
				Fail("CHANNEL_CHANGE_FAILED type=" + error.GetType().Name + " message=" + error.Message,
					error.Message);
			}
		}

		internal static void HandleButton()
		{
			if (runner == null || state == MPatcherUpdaterState.Checking
				|| state == MPatcherUpdaterState.Downloading || state == MPatcherUpdaterState.Launching)
				return;
			if (state == MPatcherUpdaterState.Available && availableManifest != null)
				runner.StartCoroutine(new MPatcherDownloadScope(DownloadAndApplyCoroutine()));
			else
				runner.StartCoroutine(CheckCoroutine(false));
		}

		internal static string Localize(string russian, string english, string japanese)
		{
			string language = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
			if (string.Equals(language, "ru", StringComparison.OrdinalIgnoreCase))
				return russian;
			if (string.Equals(language, "ja", StringComparison.OrdinalIgnoreCase))
				return japanese;
			return english;
		}

		private static IEnumerator CheckCoroutine(bool startupCheck)
		{
			if (manifestUri == null)
				yield break;
			int generation = ++checkGeneration;
			Uri requestManifestUri = manifestUri;
			MPatcherReleaseChannel requestChannel = selectedChannel;
			if (startupCheck)
				yield return new WaitForSecondsRealtime(2f);
			if (generation != checkGeneration)
				yield break;

			availableManifest = null;
			SetState(MPatcherUpdaterState.Checking, string.Empty, 0f);
			Log("CHECK_BEGIN channel=" + requestChannel + " url=" + requestManifestUri);
			WWW request = CreateRequest(requestManifestUri.AbsoluteUri);
			float startedAt = Time.realtimeSinceStartup;
			while (!request.isDone && Time.realtimeSinceStartup - startedAt < NetworkTimeoutSeconds)
				yield return null;
			if (generation != checkGeneration)
			{
				request.Dispose();
				Log("CHECK_CANCELLED generation=" + generation + " channel=" + requestChannel);
				yield break;
			}

			if (!request.isDone)
			{
				request.Dispose();
				Fail("CHECK_FAILED reason=timeout", "timeout");
				yield break;
			}
			if (!string.IsNullOrEmpty(request.error))
			{
				string requestError = request.error;
				request.Dispose();
				Fail("CHECK_FAILED reason=network error=" + requestError, requestError);
				yield break;
			}

			string text = request.text;
			request.Dispose();
			MPatcherUpdateManifest parsed;
			string parseError;
			if (!MPatcherUpdateManifest.TryParse(text, requestManifestUri, requestChannel,
				out parsed, out parseError))
			{
				Fail("CHECK_FAILED reason=manifest error=" + parseError, parseError);
				yield break;
			}

			availableManifest = parsed;
			if (parsed.ShouldInstall(currentVersion, currentBuildChannel))
			{
				SetState(MPatcherUpdaterState.Available, string.Empty, 0f);
				Log("UPDATE_AVAILABLE current=" + currentVersion + " currentChannel=" + currentBuildChannel
					+ " latest=" + parsed.VersionText + " targetChannel=" + parsed.Channel
					+ " bytes=" + parsed.InstallerLength + " sha256=" + parsed.InstallerSha256);
				if (automaticTestApply)
				{
					automaticTestApply = false;
					Log("TEST_AUTO_APPLY version=" + parsed.VersionText);
					yield return runner.StartCoroutine(new MPatcherDownloadScope(DownloadAndApplyCoroutine()));
				}
			}
			else
			{
				SetState(MPatcherUpdaterState.UpToDate, string.Empty, 0f);
				Log("UP_TO_DATE current=" + currentVersion + " channel=" + currentBuildChannel
					+ " latest=" + parsed.VersionText + " selectedChannel=" + requestChannel);
			}
		}

		private static IEnumerator DownloadAndApplyCoroutine()
		{
			MPatcherUpdateManifest target = availableManifest;
			if (target == null || !target.ShouldInstall(currentVersion, currentBuildChannel))
				yield break;

			SetState(MPatcherUpdaterState.Downloading, string.Empty, 0f);
			Log("DOWNLOAD_BEGIN version=" + target.VersionText + " url=" + target.InstallerUri
				+ " expectedBytes=" + target.InstallerLength + " firstByteTimeoutSeconds=60 stallTimeoutSeconds=60 totalTimeoutSeconds=600");
			WWW request = CreateRequest(target.InstallerUri.AbsoluteUri);
			float startedAt = Time.realtimeSinceStartup;
			MPatcherDownloadRequests.Begin(request, startedAt, target.InstallerLength);
			while (!request.isDone && MPatcherDownloadRequests.Continue(request, Time.realtimeSinceStartup, startedAt))
			{
				downloadProgress = Mathf.Clamp01(request.progress);
				MPatcherUpdaterUi.Refresh();
				yield return null;
			}

			if (!request.isDone)
			{
				request.Dispose();
				Fail("DOWNLOAD_FAILED reason=timeout " + MPatcherDownloadRequests.End(request, Time.realtimeSinceStartup), "timeout");
				yield break;
			}
			if (!string.IsNullOrEmpty(request.error))
			{
				string requestError = request.error;
				request.Dispose();
				Fail("DOWNLOAD_FAILED reason=network error=" + requestError + " "
					+ MPatcherDownloadRequests.End(request, Time.realtimeSinceStartup), requestError);
				yield break;
			}

			byte[] bytes = request.bytes;
			request.Dispose();
			Log("DOWNLOAD_RECEIVED " + MPatcherDownloadRequests.End(request, Time.realtimeSinceStartup,
				bytes == null ? 0L : bytes.LongLength));
			if (bytes == null || bytes.LongLength != target.InstallerLength)
			{
				Fail("DOWNLOAD_FAILED reason=length actual=" + (bytes == null ? 0L : bytes.LongLength)
					+ " expected=" + target.InstallerLength, "length mismatch");
				yield break;
			}
			string actualHash = ComputeSha256(bytes);
			if (!string.Equals(actualHash, target.InstallerSha256, StringComparison.OrdinalIgnoreCase))
			{
				Fail("DOWNLOAD_FAILED reason=sha256 actual=" + actualHash + " expected=" + target.InstallerSha256,
					"SHA-256 mismatch");
				yield break;
			}

			string installerPath;
			try
			{
				installerPath = SaveInstaller(bytes, target.VersionText);
			}
			catch (Exception error)
			{
				Fail("DOWNLOAD_FAILED reason=save type=" + error.GetType().Name + " message=" + error.Message,
					error.Message);
				yield break;
			}

			Log("DOWNLOAD_OK path=" + Quote(installerPath) + " bytes=" + bytes.LongLength + " sha256=" + actualHash);
			SetState(MPatcherUpdaterState.Launching, string.Empty, 1f);
			try
			{
				LaunchInstaller(installerPath);
			}
			catch (Win32Exception error)
			{
				Fail("LAUNCH_FAILED type=Win32Exception code=" + error.NativeErrorCode + " message=" + error.Message,
					error.Message);
			}
			catch (Exception error)
			{
				Fail("LAUNCH_FAILED type=" + error.GetType().Name + " message=" + error.Message, error.Message);
			}
		}

		private static WWW CreateRequest(string url)
		{
			Dictionary<string, string> headers = new Dictionary<string, string>();
			headers["User-Agent"] = "MPatcher/" + currentVersion;
			headers["Accept"] = "application/octet-stream,text/plain;q=0.9,*/*;q=0.8";
			return new WWW(url, (byte[])null, headers);
		}

		private static void LaunchInstaller(string installerPath)
		{
			string gameRoot = GetGameRoot();
			int processId = Process.GetCurrentProcess().Id;
			StringBuilder arguments = new StringBuilder();
			arguments.Append("--auto-update ").Append(QuoteArgument(gameRoot));
			arguments.Append(" --wait-pid ").Append(processId.ToString(CultureInfo.InvariantCulture));
			arguments.Append(" --restart-game --lang ").Append(GetInstallerLanguage());
			if (MPatcherUpdateManifest.IsLoopbackTestUri(manifestUri))
				arguments.Append(" --restart-update-manifest ").Append(QuoteArgument(manifestUri.AbsoluteUri));

			ProcessStartInfo start = new ProcessStartInfo();
			start.FileName = installerPath;
			start.Arguments = arguments.ToString();
			start.WorkingDirectory = gameRoot;
			start.UseShellExecute = true;
			if (RequiresElevation(gameRoot))
				start.Verb = "runas";

			Process process = Process.Start(start);
			if (process == null)
				throw new InvalidOperationException("installer process was not created");
			Log("INSTALLER_STARTED pid=" + process.Id + " waitPid=" + processId + " elevation="
				+ string.Equals(start.Verb, "runas", StringComparison.OrdinalIgnoreCase));
			process.Dispose();

			MPatchr.Mv429kCvkgErRv8Rn7I_0024WM0 = true;
			Log("GAME_QUIT_REQUESTED reason=update target=" + availableManifest.VersionText);
			Application.Quit();
		}

		private static bool RequiresElevation(string gameRoot)
		{
			string probe = Path.Combine(Path.Combine(Path.Combine(gameRoot, "McnCraft_Data"), "Mono"),
				".mpatcher-write-test-" + Process.GetCurrentProcess().Id);
			try
			{
				using (FileStream stream = new FileStream(probe, FileMode.CreateNew, FileAccess.Write, FileShare.None,
					1, FileOptions.DeleteOnClose))
				{
					stream.WriteByte(0);
				}
				return false;
			}
			catch
			{
				try { if (File.Exists(probe)) File.Delete(probe); } catch { }
				return true;
			}
		}

		private static string SaveInstaller(byte[] bytes, string version)
		{
			string directory = Path.Combine(Path.Combine(
				Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MPatcher"), "Updates");
			Directory.CreateDirectory(directory);
			string destination = Path.Combine(directory, "MPatcherInstaller-" + version + ".exe");
			string temporary = destination + ".part";
			if (File.Exists(temporary)) File.Delete(temporary);
			using (FileStream stream = new FileStream(temporary, FileMode.CreateNew, FileAccess.Write, FileShare.None))
				stream.Write(bytes, 0, bytes.Length);
			if (File.Exists(destination)) File.Delete(destination);
			File.Move(temporary, destination);
			return destination;
		}

		private static string ReadCurrentVersion()
		{
			object[] attributes = typeof(MPatcherUpdater).Assembly.GetCustomAttributes(
				typeof(AssemblyInformationalVersionAttribute), false);
			if (attributes.Length != 1)
				return string.Empty;
			return ((AssemblyInformationalVersionAttribute)attributes[0]).InformationalVersion;
		}

		private static Uri ResolveManifestUri(MPatcherReleaseChannel channel)
		{
			string value = GetArgumentValue("--mpatcher-update-manifest");
			if (value == null || value.Trim().Length == 0)
				value = channel == MPatcherReleaseChannel.Alpha ? AlphaManifestUrl : StableManifestUrl;
			Uri uri;
			if (!Uri.TryCreate(value, UriKind.Absolute, out uri) || !MPatcherUpdateManifest.IsManifestSourceAllowed(uri))
				throw new InvalidOperationException("update manifest URL is not allowed");
			MPatcherReleaseChannel productionChannel;
			if (MPatcherUpdateManifest.TryGetProductionChannel(uri, out productionChannel)
				&& productionChannel != channel)
				throw new InvalidOperationException("update manifest channel does not match selected channel");
			return uri;
		}

		private static MPatcherReleaseChannel ReadSelectedChannel()
		{
			settingsIngame settings = MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68;
			if (settings != null && settings.updateChannel == settingsIngame.updateChannels.alpha)
				return MPatcherReleaseChannel.Alpha;
			if (settings != null && settings.updateChannel != settingsIngame.updateChannels.stable)
			{
				Log("CHANNEL_NORMALIZED old=" + settings.updateChannel + " new=stable");
				settings.updateChannel = settingsIngame.updateChannels.stable;
				settings.UUiRNMwxRbfk_Fs4cDErRoM();
			}
			return MPatcherReleaseChannel.Stable;
		}

		private static MPatcherReleaseChannel GetBuildChannel(string version)
		{
			return version != null && version.Split('.').Length == 4
				? MPatcherReleaseChannel.Alpha
				: MPatcherReleaseChannel.Stable;
		}

		private static string GetArgumentValue(string option)
		{
			string[] args = Environment.GetCommandLineArgs();
			for (int i = 0; i < args.Length; i++)
			{
				if (string.Equals(args[i], option, StringComparison.OrdinalIgnoreCase) && i + 1 < args.Length)
					return args[i + 1];
				string prefix = option + "=";
				if (args[i].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
					return args[i].Substring(prefix.Length);
			}
			return string.Empty;
		}

		private static bool HasArgument(string option)
		{
			string[] args = Environment.GetCommandLineArgs();
			for (int i = 0; i < args.Length; i++)
				if (string.Equals(args[i], option, StringComparison.OrdinalIgnoreCase))
					return true;
			return false;
		}

		private static string GetGameRoot()
		{
			DirectoryInfo data = new DirectoryInfo(Application.dataPath);
			if (data.Parent == null)
				throw new DirectoryNotFoundException("MachineCraft root was not found");
			return data.Parent.FullName;
		}

		private static string GetInstallerLanguage()
		{
			string language = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
			if (string.Equals(language, "ru", StringComparison.OrdinalIgnoreCase)) return "ru";
			if (string.Equals(language, "ja", StringComparison.OrdinalIgnoreCase)) return "ja";
			return "en";
		}

		private static string QuoteArgument(string value)
		{
			return "\"" + value.Replace("\"", "\\\"") + "\"";
		}

		private static string ComputeSha256(byte[] bytes)
		{
			using (SHA256 sha = SHA256.Create())
			{
				byte[] hash = sha.ComputeHash(bytes);
				StringBuilder result = new StringBuilder(hash.Length * 2);
				for (int i = 0; i < hash.Length; i++)
					result.Append(hash[i].ToString("X2", CultureInfo.InvariantCulture));
				return result.ToString();
			}
		}

		private static void TryEnableTls12()
		{
			try
			{
				ServicePointManager.SecurityProtocol |= (SecurityProtocolType)3072;
			}
			catch (Exception error)
			{
				Log("TLS12_ENABLE_FAILED type=" + error.GetType().Name + " message=" + error.Message);
			}
		}

		private static void ReadPreviousResult()
		{
			string path = Path.Combine(Path.Combine(Path.Combine(GetGameRoot(), "McnCraft_Data"), "MPatcherFork"), ResultFileName);
			if (!File.Exists(path))
				return;
			try
			{
				Dictionary<string, string> values = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
				string[] lines = File.ReadAllLines(path);
				for (int i = 0; i < lines.Length; i++)
				{
					int separator = lines[i].IndexOf('=');
					if (separator > 0)
						values[lines[i].Substring(0, separator)] = lines[i].Substring(separator + 1);
				}
				string success;
				string version;
				values.TryGetValue("Success", out success);
				values.TryGetValue("Version", out version);
				if (string.Equals(success, "True", StringComparison.OrdinalIgnoreCase))
				{
					lastInstalledVersion = version ?? currentVersion;
					SetState(MPatcherUpdaterState.Updated, lastInstalledVersion, 1f);
					Log("PREVIOUS_UPDATE_OK version=" + lastInstalledVersion);
				}
				else
				{
					SetState(MPatcherUpdaterState.Failed, "previous automatic update failed", 0f);
					Log("PREVIOUS_UPDATE_FAILED version=" + (version ?? "unknown"));
				}
				File.Delete(path);
			}
			catch (Exception error)
			{
				Log("RESULT_READ_FAILED type=" + error.GetType().Name + " message=" + error.Message);
			}
		}

		private static void Fail(string logMessage, string detail)
		{
			Log(logMessage);
			SetState(MPatcherUpdaterState.Failed, detail, 0f);
		}

		private static void SetState(MPatcherUpdaterState newState, string detail, float progress)
		{
			state = newState;
			statusDetail = detail ?? string.Empty;
			downloadProgress = progress;
			MPatcherUpdaterUi.Refresh();
		}

		private static string Quote(string value)
		{
			return "\"" + value + "\"";
		}

		internal static void Log(string message)
		{
			mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(
				"[MPatcher.Update] " + message);
		}
	}
#endif

	// Pure deadline state: a slow transfer can continue while progress advances,
	// startup, stalled traffic and the total lifetime still have bounded waits.
	internal sealed class MPatcherDownloadDeadline
	{
		internal const float FirstByteSeconds = 60f;
		internal const float StallSeconds = 60f;
		internal const float TotalSeconds = 600f;
		internal readonly float StartedAt;
		internal float LastDataAt;
		internal long ReceivedBytes;
		private float lastObservedAt;
		private bool invalidClock;

		internal MPatcherDownloadDeadline(float startedAt)
		{
			StartedAt = LastDataAt = lastObservedAt = startedAt;
			invalidClock = float.IsNaN(startedAt) || float.IsInfinity(startedAt);
		}

		internal void Observe(float now, long receivedBytes)
		{
			if (float.IsNaN(now) || float.IsInfinity(now) || now < lastObservedAt)
				invalidClock = true;
			else lastObservedAt = now;
			if (receivedBytes > ReceivedBytes)
			{
				ReceivedBytes = receivedBytes;
				LastDataAt = now;
			}
		}

		internal string TimeoutReason(float now)
		{
			if (invalidClock || float.IsNaN(now) || float.IsInfinity(now) || now < lastObservedAt)
				return "clock-rollback";
			if (now - StartedAt >= TotalSeconds) return "total";
			if (ReceivedBytes == 0L) return now - StartedAt >= FirstByteSeconds ? "first-byte" : null;
			return now - LastDataAt >= StallSeconds ? "stall" : null;
		}

		internal static float ClampProgress(float progress)
		{
			if (float.IsNaN(progress) || float.IsInfinity(progress)) return 0f;
			return Math.Max(0f, Math.Min(1f, progress));
		}

		internal static long EstimateProgress(float progress, long expectedBytes, long previousBytes)
		{
			if (expectedBytes <= 0L) return 0L;
			long estimate = (long)((double)ClampProgress(progress) * expectedBytes);
			return Math.Max(Math.Max(0L, Math.Min(expectedBytes, previousBytes)), Math.Min(expectedBytes, estimate));
		}
	}

	internal sealed class MPatcherDownloadScope : IEnumerator, IDisposable
	{
		[ThreadStatic] internal static MPatcherDownloadScope CurrentScope;
		private readonly IEnumerator inner;
		private bool disposed;
		internal Action Cleanup;
		internal MPatcherDownloadScope(IEnumerator original) { inner = original; }
		public object Current { get { return inner.Current; } }
		public void Reset() { throw new NotSupportedException(); }
		public bool MoveNext()
		{
			if (disposed) return false;
			MPatcherDownloadScope previous = CurrentScope;
			try
			{
				CurrentScope = this;
				bool next = inner.MoveNext();
				if (!next) Dispose();
				return next;
			}
			catch { Dispose(); throw; }
			finally { CurrentScope = previous; }
		}
		public void Dispose()
		{
			if (disposed) return;
			disposed = true;
			Action cleanup = Cleanup; Cleanup = null;
			try
			{
				IDisposable disposable = inner as IDisposable;
				if (disposable != null) disposable.Dispose();
			}
			finally { if (cleanup != null) cleanup(); }
		}
	}

#if !MPATCHER_UPDATER_POLICY_ONLY
	// Keep transfer state outside the existing coroutine's hoisted field layout.
	// Every terminal path removes its WWW key before hash/save/launch handling.
	internal static class MPatcherDownloadRequests
	{
		private sealed class Transfer
		{
			internal MPatcherDownloadDeadline Deadline;
			internal long ExpectedBytes;
			internal float LastLogAt;
			internal float Progress;
			internal MPatcherDownloadScope Scope;
			internal bool Completed;
		}
		private static readonly Dictionary<WWW, Transfer> transfers = new Dictionary<WWW, Transfer>();

		internal static void Begin(WWW request, float startedAt, long expectedBytes)
		{
			transfers[request] = new Transfer { Deadline = new MPatcherDownloadDeadline(startedAt),
				ExpectedBytes = expectedBytes, LastLogAt = startedAt, Scope = MPatcherDownloadScope.CurrentScope };
			if (MPatcherDownloadScope.CurrentScope != null)
				MPatcherDownloadScope.CurrentScope.Cleanup = delegate { Cancel(request); };
		}

		internal static bool Continue(WWW request, float now, float startedAt)
		{
			Transfer transfer;
			if (!transfers.TryGetValue(request, out transfer) || transfer.Deadline.StartedAt != startedAt)
				return false;
			// On this Unity build bytesDownloaded can wait for the entire request.
			// Its nonblocking progress getter is the existing updater's polling API.
			transfer.Progress = MPatcherDownloadDeadline.ClampProgress(request.progress);
			now = Time.realtimeSinceStartup;
			transfer.Deadline.Observe(now, MPatcherDownloadDeadline.EstimateProgress(transfer.Progress,
				transfer.ExpectedBytes, transfer.Deadline.ReceivedBytes));
			if (now - transfer.LastLogAt >= 10f)
			{
				transfer.LastLogAt = now;
				MPatcherUpdater.Log("DOWNLOAD_PROGRESS " + Describe(transfer, now));
			}
			return transfer.Deadline.TimeoutReason(now) == null;
		}

		internal static string End(WWW request, float now, long receivedBytes = -1L)
		{
			Transfer transfer;
			if (!transfers.TryGetValue(request, out transfer)) return "phase=missing-transfer";
			transfers.Remove(request);
			if (transfer.Scope != null) transfer.Scope.Cleanup = null;
			if (receivedBytes >= 0L)
			{
				transfer.Deadline.Observe(now, receivedBytes);
				transfer.Deadline.ReceivedBytes = receivedBytes;
				transfer.Deadline.LastDataAt = now;
				transfer.Progress = 1f;
				transfer.Completed = true;
			}
			return Describe(transfer, now);
		}

		private static void Cancel(WWW request)
		{
			if (!transfers.ContainsKey(request)) return;
			string detail = End(request, Time.realtimeSinceStartup);
			try { request.Dispose(); }
			finally { MPatcherUpdater.Log("DOWNLOAD_CANCELLED " + detail); }
		}

		private static string Describe(Transfer transfer, float now)
		{
			return "phase=" + (transfer.Completed ? "completed" : transfer.Deadline.TimeoutReason(now)
				?? (transfer.Deadline.ReceivedBytes == 0L ? "awaiting-first-byte" : "receiving"))
				+ (transfer.Completed ? " receivedBytes=" : " estimatedBytes=")
				+ transfer.Deadline.ReceivedBytes.ToString(CultureInfo.InvariantCulture)
				+ " countSource=" + (transfer.Completed ? "completed-bytes" : "WWW.progress-estimate")
				+ " expectedBytes=" + transfer.ExpectedBytes.ToString(CultureInfo.InvariantCulture)
				+ " progress=" + transfer.Progress.ToString("0.000", CultureInfo.InvariantCulture)
				+ " elapsedSeconds=" + (now - transfer.Deadline.StartedAt).ToString("0.0", CultureInfo.InvariantCulture)
				+ " idleSeconds=" + (now - transfer.Deadline.LastDataAt).ToString("0.0", CultureInfo.InvariantCulture);
		}
	}
#endif
}
