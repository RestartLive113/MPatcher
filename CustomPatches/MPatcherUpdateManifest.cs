using System;
using System.Collections.Generic;
using System.Globalization;

namespace MPatcherFork.CustomPatches
{
	internal sealed class MPatcherUpdateManifest
	{
		internal const int FormatVersion = 1;
		internal const int MaximumManifestCharacters = 32768;
		internal const long MaximumInstallerLength = 64L * 1024L * 1024L;
		internal const string ProductionRepositoryPath = "/RestartLive113/MPatcher/releases/download/";

		internal string VersionText;
		internal Version Version;
		internal Uri InstallerUri;
		internal string InstallerSha256;
		internal long InstallerLength;

		internal bool IsNewerThan(string currentVersion)
		{
			Version current;
			return TryParseVersion(currentVersion, out current) && Version.CompareTo(current) > 0;
		}

		internal static bool TryParse(string text, Uri sourceUri, out MPatcherUpdateManifest manifest, out string error)
		{
			manifest = null;
			error = string.Empty;
			if (!IsManifestSourceAllowed(sourceUri))
				return Fail("manifest source is not allowed", out error);
			if (IsBlank(text))
				return Fail("manifest is empty", out error);
			if (text.Length > MaximumManifestCharacters)
				return Fail("manifest is too large", out error);

			Dictionary<string, string> values = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			string[] lines = text.Replace("\r\n", "\n").Replace('\r', '\n').Split('\n');
			for (int i = 0; i < lines.Length; i++)
			{
				string line = lines[i].Trim();
				if (line.Length == 0 || line.StartsWith("#", StringComparison.Ordinal))
					continue;
				int separator = line.IndexOf('=');
				if (separator <= 0)
					return Fail("invalid line " + (i + 1).ToString(CultureInfo.InvariantCulture), out error);
				string key = line.Substring(0, separator).Trim();
				string value = line.Substring(separator + 1).Trim();
				if (key.Length == 0 || value.Length == 0)
					return Fail("empty key or value on line " + (i + 1).ToString(CultureInfo.InvariantCulture), out error);
				if (values.ContainsKey(key))
					return Fail("duplicate key: " + key, out error);
				values.Add(key, value);
			}

			string format;
			int parsedFormat;
			if (!values.TryGetValue("Format", out format)
				|| !int.TryParse(format, NumberStyles.None, CultureInfo.InvariantCulture, out parsedFormat)
				|| parsedFormat != FormatVersion)
				return Fail("unsupported format", out error);

			string versionText;
			Version parsedVersion;
			if (!values.TryGetValue("Version", out versionText) || !TryParseVersion(versionText, out parsedVersion))
				return Fail("invalid version", out error);

			string installerUrl;
			Uri installerUri;
			if (!values.TryGetValue("InstallerUrl", out installerUrl)
				|| !Uri.TryCreate(installerUrl, UriKind.Absolute, out installerUri)
				|| !IsInstallerUriAllowed(installerUri, sourceUri, versionText))
				return Fail("installer URL is not allowed", out error);

			string sha256;
			if (!values.TryGetValue("InstallerSha256", out sha256) || !IsSha256(sha256))
				return Fail("invalid installer SHA-256", out error);

			string lengthText;
			long installerLength;
			if (!values.TryGetValue("InstallerLength", out lengthText)
				|| !long.TryParse(lengthText, NumberStyles.None, CultureInfo.InvariantCulture, out installerLength)
				|| installerLength <= 0 || installerLength > MaximumInstallerLength)
				return Fail("invalid installer length", out error);

			manifest = new MPatcherUpdateManifest
			{
				VersionText = versionText,
				Version = parsedVersion,
				InstallerUri = installerUri,
				InstallerSha256 = sha256.ToUpperInvariant(),
				InstallerLength = installerLength
			};
			return true;
		}

		internal static bool IsManifestSourceAllowed(Uri uri)
		{
			if (uri == null)
				return false;
			if (IsLoopbackTestUri(uri))
				return true;
			return string.Equals(uri.Scheme, Uri.UriSchemeHttps, StringComparison.OrdinalIgnoreCase)
				&& string.Equals(uri.Host, "github.com", StringComparison.OrdinalIgnoreCase)
				&& string.Equals(uri.AbsolutePath,
					"/RestartLive113/MPatcher/releases/latest/download/MPatcherUpdate.ini",
					StringComparison.OrdinalIgnoreCase);
		}

		internal static bool IsLoopbackTestUri(Uri uri)
		{
			if (uri == null)
				return false;
			if (uri.IsFile)
				return true;
			return (string.Equals(uri.Scheme, Uri.UriSchemeHttp, StringComparison.OrdinalIgnoreCase)
					|| string.Equals(uri.Scheme, Uri.UriSchemeHttps, StringComparison.OrdinalIgnoreCase))
				&& (uri.IsLoopback || string.Equals(uri.Host, "localhost", StringComparison.OrdinalIgnoreCase));
		}

		internal static bool TryParseVersion(string text, out Version version)
		{
			version = null;
			if (IsBlank(text))
				return false;
			string[] pieces = text.Split('.');
			if (pieces.Length != 3)
				return false;
			for (int i = 0; i < pieces.Length; i++)
			{
				int value;
				if (pieces[i].Length == 0 || !int.TryParse(pieces[i], NumberStyles.None, CultureInfo.InvariantCulture, out value) || value < 0)
					return false;
			}
			try
			{
				version = new Version(text);
				return true;
			}
			catch
			{
				return false;
			}
		}

		private static bool IsInstallerUriAllowed(Uri installerUri, Uri sourceUri, string versionText)
		{
			if (IsLoopbackTestUri(sourceUri))
				return IsLoopbackTestUri(installerUri);
			if (!string.Equals(installerUri.Scheme, Uri.UriSchemeHttps, StringComparison.OrdinalIgnoreCase)
				|| !string.Equals(installerUri.Host, "github.com", StringComparison.OrdinalIgnoreCase))
				return false;
			string expectedPath = ProductionRepositoryPath + "v" + versionText + "/MPatcherPackage.exe";
			return string.Equals(installerUri.AbsolutePath, expectedPath, StringComparison.OrdinalIgnoreCase)
				&& string.IsNullOrEmpty(installerUri.Query) && string.IsNullOrEmpty(installerUri.Fragment);
		}

		private static bool IsSha256(string value)
		{
			if (string.IsNullOrEmpty(value) || value.Length != 64)
				return false;
			for (int i = 0; i < value.Length; i++)
			{
				char c = value[i];
				if (!((c >= '0' && c <= '9') || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F')))
					return false;
			}
			return true;
		}

		private static bool IsBlank(string value)
		{
			return value == null || value.Trim().Length == 0;
		}

		private static bool Fail(string message, out string error)
		{
			error = message;
			return false;
		}
	}
}
