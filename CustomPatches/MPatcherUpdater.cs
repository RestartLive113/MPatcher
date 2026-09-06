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
using MPatchrMain;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
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
		internal const string DefaultManifestUrl = "https://github.com/RestartLive113/MPatcher/releases/latest/download/MPatcherUpdate.ini";
		private const float NetworkTimeoutSeconds = 30f;
		private const string ResultFileName = "MPatcherUpdate.result.ini";

		private static MPatchr runner;
		private static Uri manifestUri;
		private static MPatcherUpdateManifest availableManifest;
		private static bool started;
		private static bool automaticTestApply;
		private static string currentVersion = "0.0.0";
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

				manifestUri = ResolveManifestUri();
				automaticTestApply = HasArgument("--mpatcher-auto-apply")
					&& MPatcherUpdateManifest.IsLoopbackTestUri(manifestUri);
				TryEnableTls12();
				Log("REGISTERED version=" + currentVersion + " manifest=" + manifestUri
					+ " testOverride=" + MPatcherUpdateManifest.IsLoopbackTestUri(manifestUri)
					+ " autoApply=" + automaticTestApply);
				ReadPreviousResult();
				runner.StartCoroutine(CheckCoroutine(true));
			}
			catch (Exception error)
			{
				SetState(MPatcherUpdaterState.Failed, "startup: " + error.Message, 0f);
				Log("REGISTER_FAILED type=" + error.GetType().Name + " message=" + error.Message);
			}
		}

		internal static void HandleButton()
		{
			if (runner == null || state == MPatcherUpdaterState.Checking
				|| state == MPatcherUpdaterState.Downloading || state == MPatcherUpdaterState.Launching)
				return;
			if (state == MPatcherUpdaterState.Available && availableManifest != null)
				runner.StartCoroutine(DownloadAndApplyCoroutine());
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
			if (startupCheck)
				yield return new WaitForSecondsRealtime(2f);

			availableManifest = null;
			SetState(MPatcherUpdaterState.Checking, string.Empty, 0f);
			Log("CHECK_BEGIN url=" + manifestUri);
			WWW request = CreateRequest(manifestUri.AbsoluteUri);
			float startedAt = Time.realtimeSinceStartup;
			while (!request.isDone && Time.realtimeSinceStartup - startedAt < NetworkTimeoutSeconds)
				yield return null;

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
			if (!MPatcherUpdateManifest.TryParse(text, manifestUri, out parsed, out parseError))
			{
				Fail("CHECK_FAILED reason=manifest error=" + parseError, parseError);
				yield break;
			}

			availableManifest = parsed;
			if (parsed.IsNewerThan(currentVersion))
			{
				SetState(MPatcherUpdaterState.Available, string.Empty, 0f);
				Log("UPDATE_AVAILABLE current=" + currentVersion + " latest=" + parsed.VersionText
					+ " bytes=" + parsed.InstallerLength + " sha256=" + parsed.InstallerSha256);
				if (automaticTestApply)
				{
					automaticTestApply = false;
					Log("TEST_AUTO_APPLY version=" + parsed.VersionText);
					yield return runner.StartCoroutine(DownloadAndApplyCoroutine());
				}
			}
			else
			{
				SetState(MPatcherUpdaterState.UpToDate, string.Empty, 0f);
				Log("UP_TO_DATE current=" + currentVersion + " latest=" + parsed.VersionText);
			}
		}

		private static IEnumerator DownloadAndApplyCoroutine()
		{
			MPatcherUpdateManifest target = availableManifest;
			if (target == null || !target.IsNewerThan(currentVersion))
				yield break;

			SetState(MPatcherUpdaterState.Downloading, string.Empty, 0f);
			Log("DOWNLOAD_BEGIN version=" + target.VersionText + " url=" + target.InstallerUri);
			WWW request = CreateRequest(target.InstallerUri.AbsoluteUri);
			float startedAt = Time.realtimeSinceStartup;
			while (!request.isDone && Time.realtimeSinceStartup - startedAt < NetworkTimeoutSeconds)
			{
				downloadProgress = Mathf.Clamp01(request.progress);
				MPatcherUpdaterUi.Refresh();
				yield return null;
			}

			if (!request.isDone)
			{
				request.Dispose();
				Fail("DOWNLOAD_FAILED reason=timeout", "timeout");
				yield break;
			}
			if (!string.IsNullOrEmpty(request.error))
			{
				string requestError = request.error;
				request.Dispose();
				Fail("DOWNLOAD_FAILED reason=network error=" + requestError, requestError);
				yield break;
			}

			byte[] bytes = request.bytes;
			request.Dispose();
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

		private static Uri ResolveManifestUri()
		{
			string value = GetArgumentValue("--mpatcher-update-manifest");
			if (value == null || value.Trim().Length == 0)
				value = DefaultManifestUrl;
			Uri uri;
			if (!Uri.TryCreate(value, UriKind.Absolute, out uri) || !MPatcherUpdateManifest.IsManifestSourceAllowed(uri))
				throw new InvalidOperationException("update manifest URL is not allowed");
			return uri;
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
}
