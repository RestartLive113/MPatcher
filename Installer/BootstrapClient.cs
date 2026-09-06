using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace MachineCraftMPatcherInstaller
{
	internal enum BootstrapAction
	{
		Install,
		Uninstall
	}

	internal sealed class BootstrapNetworkException : Exception
	{
		internal BootstrapNetworkException(string message, Exception inner) : base(message, inner) { }
	}

	internal sealed class BootstrapChildException : Exception
	{
		internal readonly int ExitCode;
		internal readonly BootstrapAction Action;
		internal readonly string Details;
		internal readonly string LogPath;

		internal BootstrapChildException(BootstrapAction action, int exitCode, string details, string logPath)
			: base(string.IsNullOrWhiteSpace(details)
				? InstallerText.PackageReturnedExitCode(exitCode) : details)
		{
			ExitCode = exitCode;
			Action = action;
			Details = Message;
			LogPath = logPath ?? string.Empty;
		}
	}

	internal sealed class BootstrapOperationResult
	{
		internal string Version;
		internal string LogPath;
		internal string Message;
		internal int ClosedGameProcessCount;
		internal int ForcedGameProcessCount;
	}

	internal static class BootstrapClient
	{
		private const int NetworkTimeoutMilliseconds = 20000;
		private static readonly object LogSync = new object();

		internal static BootstrapManifest FetchManifest(Uri sourceUri, string gameRoot, Action<string> progress)
		{
			if (!BootstrapManifest.IsManifestSourceAllowed(sourceUri))
				throw new InvalidDataException("manifest source is not allowed");
			TryEnableTls12();
			Report(gameRoot, progress, "BOOTSTRAP_CHECK_BEGIN url=" + sourceUri.AbsoluteUri);
			string text;
			try
			{
				if (sourceUri.IsFile)
				{
					FileInfo item = new FileInfo(sourceUri.LocalPath);
					if (!item.Exists)
						throw new FileNotFoundException("test manifest was not found", item.FullName);
					if (item.Length > BootstrapInfo.MaximumManifestBytes)
						throw new InvalidDataException("manifest is too large");
					text = File.ReadAllText(item.FullName, Encoding.UTF8);
				}
				else
				{
					HttpWebRequest request = CreateRequest(sourceUri, "text/plain,*/*;q=0.5");
					using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
					using (Stream stream = response.GetResponseStream())
						text = ReadManifestText(stream, response.ContentLength);
				}
			}
			catch (WebException error)
			{
				Report(gameRoot, progress, "BOOTSTRAP_CHECK_NETWORK_FAILED status=" + error.Status
					+ " message=" + OneLine(error.Message));
				throw new BootstrapNetworkException(error.Message, error);
			}
			catch (IOException error)
			{
				if (sourceUri.IsFile)
					throw;
				Report(gameRoot, progress, "BOOTSTRAP_CHECK_NETWORK_FAILED type=IOException message="
					+ OneLine(error.Message));
				throw new BootstrapNetworkException(error.Message, error);
			}

			BootstrapManifest manifest;
			string parseError;
			if (!BootstrapManifest.TryParse(text, sourceUri, out manifest, out parseError))
			{
				Report(gameRoot, progress, "BOOTSTRAP_MANIFEST_REJECTED reason=" + OneLine(parseError));
				throw new InvalidDataException(parseError);
			}
			Report(gameRoot, progress, "BOOTSTRAP_CHECK_OK version=" + manifest.VersionText
				+ " bytes=" + manifest.PackageLength.ToString(CultureInfo.InvariantCulture)
				+ " sha256=" + manifest.PackageSha256);
			return manifest;
		}

		internal static BootstrapOperationResult DownloadAndExecute(BootstrapManifest manifest, string gameRoot,
			BootstrapAction action, InstallerLanguage language, Action<string> progress, string testTempRoot)
		{
			if (manifest == null)
				throw new ArgumentNullException("manifest");
			if (!BootstrapGameLocator.IsGameRoot(gameRoot))
				throw new DirectoryNotFoundException(InstallerText.SelectGameFolder);
			gameRoot = Path.GetFullPath(gameRoot.Trim());

			string baseRoot = string.IsNullOrWhiteSpace(testTempRoot)
				? Path.Combine(Path.GetTempPath(), "MPatcherInstaller")
				: Path.GetFullPath(testTempRoot);
			string workingDirectory = Path.Combine(baseRoot, "run-" + Guid.NewGuid().ToString("N"));
			Directory.CreateDirectory(workingDirectory);
			string partialPath = Path.Combine(workingDirectory, BootstrapInfo.PackageFileName + ".part");
			string packagePath = Path.Combine(workingDirectory, BootstrapInfo.PackageFileName);
			string resultPath = Path.Combine(workingDirectory, "operation-result.ini");
			try
			{
				Report(gameRoot, progress, "BOOTSTRAP_DOWNLOAD_BEGIN version=" + manifest.VersionText
					+ " url=" + manifest.PackageUri.AbsoluteUri);
				DownloadPackage(manifest, partialPath, gameRoot, progress);
				File.Move(partialPath, packagePath);
				Report(gameRoot, progress, "BOOTSTRAP_DOWNLOAD_OK version=" + manifest.VersionText
					+ " sha256=" + manifest.PackageSha256);
				GameCloseResult bootstrapClose = GameProcessController.CloseSelectedGame(gameRoot,
					delegate(string message) { Report(gameRoot, progress, message); });

				string actionName = action == BootstrapAction.Install ? "install" : "uninstall";
				ProcessStartInfo start = new ProcessStartInfo();
				start.FileName = packagePath;
				start.WorkingDirectory = gameRoot;
				start.UseShellExecute = false;
				start.CreateNoWindow = true;
				start.WindowStyle = ProcessWindowStyle.Hidden;
				start.Arguments = "--" + actionName + " " + QuoteArgument(gameRoot)
					+ " --lang " + LanguageCode(language)
					+ " --result-file " + QuoteArgument(resultPath);
				Report(gameRoot, progress, "BOOTSTRAP_PACKAGE_START action=" + actionName);
				int exitCode;
				using (Process process = Process.Start(start))
				{
					if (process == null)
						throw new InvalidOperationException("downloaded installer process was not created");
					process.WaitForExit();
					exitCode = process.ExitCode;
				}
				InstallerCommandResult packageResult = InstallerResultProtocol.TryRead(resultPath);
				if (packageResult != null)
				{
					Report(gameRoot, progress, "BOOTSTRAP_PACKAGE_RESULT success=" + packageResult.Success
						+ " closedGameProcesses=" + packageResult.ClosedGameProcessCount.ToString(CultureInfo.InvariantCulture)
						+ " forcedGameProcesses=" + packageResult.ForcedGameProcessCount.ToString(CultureInfo.InvariantCulture)
						+ " message=" + OneLine(packageResult.Message));
				}
				if (exitCode != 0 || (packageResult != null && !packageResult.Success))
				{
					Report(gameRoot, progress, "BOOTSTRAP_PACKAGE_FAILED action=" + actionName
						+ " exitCode=" + exitCode.ToString(CultureInfo.InvariantCulture));
					throw new BootstrapChildException(action, exitCode,
						packageResult == null ? string.Empty : packageResult.ErrorDetails,
						packageResult == null ? string.Empty : packageResult.LogPath);
				}
				Report(gameRoot, progress, "BOOTSTRAP_PACKAGE_OK action=" + actionName
					+ " version=" + manifest.VersionText);
				return new BootstrapOperationResult
				{
					Version = manifest.VersionText,
					LogPath = packageResult == null || string.IsNullOrWhiteSpace(packageResult.LogPath)
						? BootstrapGameLocator.GetInstallerLogPath(gameRoot) : packageResult.LogPath,
					Message = packageResult == null ? string.Empty : packageResult.Message,
					ClosedGameProcessCount = bootstrapClose.ClosedProcessCount,
					ForcedGameProcessCount = bootstrapClose.ForcedProcessCount
				};
			}
			finally
			{
				TryDeleteFile(partialPath);
				TryDeleteFile(packagePath);
				TryDeleteFile(resultPath);
				TryDeleteWorkingDirectory(baseRoot, workingDirectory);
			}
		}

		internal static void ReportFailure(string gameRoot, Exception error)
		{
			Report(gameRoot, null, "BOOTSTRAP_FAILED type=" + error.GetType().Name
				+ " message=" + OneLine(error.Message));
		}

		private static void DownloadPackage(BootstrapManifest manifest, string destination,
			string gameRoot, Action<string> progress)
		{
			long written = 0;
			int lastPercent = -1;
			try
			{
				using (Stream source = OpenPackageStream(manifest.PackageUri, manifest.PackageLength))
				using (FileStream target = new FileStream(destination, FileMode.CreateNew, FileAccess.Write, FileShare.None))
				{
					byte[] buffer = new byte[131072];
					int read;
					while ((read = source.Read(buffer, 0, buffer.Length)) > 0)
					{
						written += read;
						if (written > manifest.PackageLength)
							throw new InvalidDataException("downloaded package is larger than declared");
						target.Write(buffer, 0, read);
						int percent = (int)(written * 100L / manifest.PackageLength);
						if (percent >= lastPercent + 10 || percent == 100)
						{
							lastPercent = percent;
							Report(gameRoot, progress, "BOOTSTRAP_DOWNLOAD_PROGRESS percent="
								+ percent.ToString(CultureInfo.InvariantCulture));
						}
					}
				}
			}
			catch (WebException error)
			{
				throw new BootstrapNetworkException(error.Message, error);
			}
			if (written != manifest.PackageLength)
				throw new InvalidDataException("downloaded package length does not match manifest");
			string actualHash = ComputeSha256(destination);
			if (!string.Equals(actualHash, manifest.PackageSha256, StringComparison.OrdinalIgnoreCase))
				throw new InvalidDataException("downloaded package SHA-256 does not match manifest");
		}

		private static Stream OpenPackageStream(Uri uri, long expectedLength)
		{
			if (uri.IsFile)
				return new FileStream(uri.LocalPath, FileMode.Open, FileAccess.Read, FileShare.Read);
			HttpWebRequest request = CreateRequest(uri, "application/octet-stream,*/*;q=0.5");
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			if (response.ContentLength > 0 && response.ContentLength != expectedLength)
			{
				response.Dispose();
				throw new InvalidDataException("package Content-Length does not match manifest");
			}
			return new ResponseStream(response);
		}

		private static HttpWebRequest CreateRequest(Uri uri, string accept)
		{
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
			request.Method = "GET";
			request.AllowAutoRedirect = true;
			request.MaximumAutomaticRedirections = 8;
			request.Timeout = NetworkTimeoutMilliseconds;
			request.ReadWriteTimeout = NetworkTimeoutMilliseconds;
			request.UserAgent = "MPatcherInstaller";
			request.Accept = accept;
			return request;
		}

		private static string ReadManifestText(Stream stream, long contentLength)
		{
			if (contentLength > BootstrapInfo.MaximumManifestBytes)
				throw new InvalidDataException("manifest is too large");
			using (MemoryStream memory = new MemoryStream())
			{
				byte[] buffer = new byte[4096];
				int read;
				while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
				{
					if (memory.Length + read > BootstrapInfo.MaximumManifestBytes)
						throw new InvalidDataException("manifest is too large");
					memory.Write(buffer, 0, read);
				}
				return Encoding.UTF8.GetString(memory.ToArray()).TrimStart('\ufeff');
			}
		}

		private static string ComputeSha256(string path)
		{
			using (SHA256 hash = SHA256.Create())
			using (FileStream stream = File.OpenRead(path))
			{
				byte[] value = hash.ComputeHash(stream);
				StringBuilder text = new StringBuilder(value.Length * 2);
				for (int i = 0; i < value.Length; i++)
					text.Append(value[i].ToString("x2", CultureInfo.InvariantCulture));
				return text.ToString().ToUpperInvariant();
			}
		}

		private static void Report(string gameRoot, Action<string> progress, string message)
		{
			if (progress != null)
				progress(message);
			try
			{
				string line = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture)
					+ " [BOOTSTRAP] " + message + Environment.NewLine;
				lock (LogSync)
					File.AppendAllText(BootstrapGameLocator.GetInstallerLogPath(gameRoot), line, new UTF8Encoding(false));
			}
			catch
			{
			}
		}

		private static string LanguageCode(InstallerLanguage language)
		{
			if (language == InstallerLanguage.Russian)
				return "ru";
			if (language == InstallerLanguage.Japanese)
				return "ja";
			return "en";
		}

		private static string QuoteArgument(string value)
		{
			return "\"" + value.Replace("\"", "\\\"") + "\"";
		}

		private static string OneLine(string value)
		{
			return string.IsNullOrEmpty(value) ? string.Empty : value.Replace('\r', ' ').Replace('\n', ' ');
		}

		private static void TryDeleteFile(string path)
		{
			try
			{
				if (File.Exists(path))
				{
					File.SetAttributes(path, FileAttributes.Normal);
					File.Delete(path);
				}
			}
			catch
			{
			}
		}

		private static void TryDeleteWorkingDirectory(string baseRoot, string workingDirectory)
		{
			try
			{
				string resolvedBase = Path.GetFullPath(baseRoot).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
				string resolvedWorking = Path.GetFullPath(workingDirectory);
				if (!resolvedWorking.StartsWith(resolvedBase + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase))
					return;
				if (Directory.Exists(resolvedWorking))
					Directory.Delete(resolvedWorking, true);
			}
			catch
			{
			}
		}

		private static void TryEnableTls12()
		{
			try { ServicePointManager.SecurityProtocol |= (SecurityProtocolType)3072; } catch { }
		}

		private sealed class ResponseStream : Stream
		{
			private readonly HttpWebResponse response;
			private readonly Stream inner;

			internal ResponseStream(HttpWebResponse response)
			{
				this.response = response;
				inner = response.GetResponseStream();
			}

			public override bool CanRead { get { return inner.CanRead; } }
			public override bool CanSeek { get { return false; } }
			public override bool CanWrite { get { return false; } }
			public override long Length { get { throw new NotSupportedException(); } }
			public override long Position { get { throw new NotSupportedException(); } set { throw new NotSupportedException(); } }
			public override void Flush() { inner.Flush(); }
			public override int Read(byte[] buffer, int offset, int count)
			{
				try { return inner.Read(buffer, offset, count); }
				catch (IOException error) { throw new BootstrapNetworkException(error.Message, error); }
			}
			public override long Seek(long offset, SeekOrigin origin) { throw new NotSupportedException(); }
			public override void SetLength(long value) { throw new NotSupportedException(); }
			public override void Write(byte[] buffer, int offset, int count) { throw new NotSupportedException(); }

			protected override void Dispose(bool disposing)
			{
				if (disposing)
				{
					inner.Dispose();
					response.Dispose();
				}
				base.Dispose(disposing);
			}
		}
	}
}
