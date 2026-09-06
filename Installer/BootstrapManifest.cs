using System;
using System.Collections.Generic;
using System.Globalization;

namespace MachineCraftMPatcherInstaller
{
	internal sealed class BootstrapManifest
	{
		internal const int FormatVersion = 1;

		internal string VersionText;
		internal Version Version;
		internal Uri PackageUri;
		internal string PackageSha256;
		internal long PackageLength;

		internal static bool TryParse(string text, Uri sourceUri, out BootstrapManifest manifest, out string error)
		{
			manifest = null;
			error = string.Empty;
			if (!IsManifestSourceAllowed(sourceUri))
				return Fail("manifest source is not allowed", out error);
			if (string.IsNullOrWhiteSpace(text))
				return Fail("manifest is empty", out error);
			if (text.Length > BootstrapInfo.MaximumManifestBytes)
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

			string packageUrl;
			Uri packageUri;
			if (!values.TryGetValue("InstallerUrl", out packageUrl)
				|| !Uri.TryCreate(packageUrl, UriKind.Absolute, out packageUri)
				|| !IsPackageUriAllowed(packageUri, sourceUri, versionText))
				return Fail("package URL is not allowed", out error);

			string sha256;
			if (!values.TryGetValue("InstallerSha256", out sha256) || !IsSha256(sha256))
				return Fail("invalid package SHA-256", out error);

			string lengthText;
			long packageLength;
			if (!values.TryGetValue("InstallerLength", out lengthText)
				|| !long.TryParse(lengthText, NumberStyles.None, CultureInfo.InvariantCulture, out packageLength)
				|| packageLength <= 0 || packageLength > BootstrapInfo.MaximumPackageLength)
				return Fail("invalid package length", out error);

			manifest = new BootstrapManifest
			{
				VersionText = versionText,
				Version = parsedVersion,
				PackageUri = packageUri,
				PackageSha256 = sha256.ToUpperInvariant(),
				PackageLength = packageLength
			};
			return true;
		}

		internal static bool IsManifestSourceAllowed(Uri uri)
		{
			if (uri == null)
				return false;
			if (IsLoopbackTestUri(uri))
				return true;
			Uri production;
			return Uri.TryCreate(BootstrapInfo.ManifestUrl, UriKind.Absolute, out production)
				&& string.Equals(uri.Scheme, production.Scheme, StringComparison.OrdinalIgnoreCase)
				&& string.Equals(uri.Host, production.Host, StringComparison.OrdinalIgnoreCase)
				&& string.Equals(uri.AbsolutePath, production.AbsolutePath, StringComparison.OrdinalIgnoreCase)
				&& string.IsNullOrEmpty(uri.Query) && string.IsNullOrEmpty(uri.Fragment);
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

		private static bool IsPackageUriAllowed(Uri packageUri, Uri sourceUri, string versionText)
		{
			if (IsLoopbackTestUri(sourceUri))
				return IsLoopbackTestUri(packageUri);
			if (!string.Equals(packageUri.Scheme, Uri.UriSchemeHttps, StringComparison.OrdinalIgnoreCase)
				|| !string.Equals(packageUri.Host, "github.com", StringComparison.OrdinalIgnoreCase))
				return false;
			string expectedPath = BootstrapInfo.RepositoryReleasePath + "v" + versionText + "/" + BootstrapInfo.PackageFileName;
			return string.Equals(packageUri.AbsolutePath, expectedPath, StringComparison.OrdinalIgnoreCase)
				&& string.IsNullOrEmpty(packageUri.Query) && string.IsNullOrEmpty(packageUri.Fragment);
		}

		private static bool TryParseVersion(string text, out Version version)
		{
			version = null;
			if (string.IsNullOrWhiteSpace(text))
				return false;
			string[] pieces = text.Split('.');
			if (pieces.Length != 3)
				return false;
			for (int i = 0; i < pieces.Length; i++)
			{
				int value;
				if (pieces[i].Length == 0
					|| !int.TryParse(pieces[i], NumberStyles.None, CultureInfo.InvariantCulture, out value)
					|| value < 0)
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

		private static bool Fail(string message, out string error)
		{
			error = message;
			return false;
		}
	}
}
