using System;
using System.IO;
using System.Windows.Forms;

namespace MachineCraftMPatcherInstaller
{
	internal static class BootstrapProgram
	{
		[STAThread]
		private static int Main(string[] args)
		{
			InstallerText.UseSystemLanguage();
			string language = GetOptionValue(args, "--lang");
			if (!string.IsNullOrEmpty(language))
				InstallerText.TrySetLanguageCode(language);

			Uri manifestUri;
			try
			{
				manifestUri = ResolveManifestUri(args);
			}
			catch (Exception error)
			{
				MessageBox.Show(InstallerText.BootstrapDownloadError(error.Message), BootstrapInfo.ProductName,
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				return 2;
			}

			string automatedAction = GetOptionValue(args, "--bootstrap-action");
			if (!string.IsNullOrEmpty(automatedAction))
				return RunAutomated(args, manifestUri, automatedAction);

			string initialPath = GetOptionValue(args, "--path");
			if (string.IsNullOrEmpty(initialPath))
				initialPath = BootstrapGameLocator.AutoDetectGameRoot();
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new BootstrapForm(initialPath, manifestUri));
			return 0;
		}

		private static int RunAutomated(string[] args, Uri manifestUri, string actionText)
		{
			string explicitManifest = GetOptionValue(args, "--bootstrap-manifest");
			if (string.IsNullOrEmpty(explicitManifest) || !BootstrapManifest.IsLoopbackTestUri(manifestUri))
				return 2;
			BootstrapAction action;
			if (string.Equals(actionText, "install", StringComparison.OrdinalIgnoreCase))
				action = BootstrapAction.Install;
			else if (string.Equals(actionText, "uninstall", StringComparison.OrdinalIgnoreCase))
				action = BootstrapAction.Uninstall;
			else
				return 2;
			string gameRoot = GetOptionValue(args, "--path");
			try
			{
				BootstrapManifest manifest = BootstrapClient.FetchManifest(manifestUri, gameRoot, null);
				BootstrapClient.DownloadAndExecute(manifest, gameRoot, action, InstallerText.Language, null,
					GetOptionValue(args, "--bootstrap-temp-root"));
				return 0;
			}
			catch (BootstrapNetworkException error)
			{
				BootstrapClient.ReportFailure(gameRoot, error);
				return 3;
			}
			catch (Exception error)
			{
				BootstrapClient.ReportFailure(gameRoot, error);
				return 1;
			}
		}

		private static Uri ResolveManifestUri(string[] args)
		{
			string overrideValue = GetOptionValue(args, "--bootstrap-manifest");
			Uri uri;
			if (string.IsNullOrEmpty(overrideValue))
			{
				if (!Uri.TryCreate(BootstrapInfo.ManifestUrl, UriKind.Absolute, out uri))
					throw new InvalidDataException("production manifest URL is invalid");
				return uri;
			}
			if (!Uri.TryCreate(overrideValue, UriKind.Absolute, out uri) || !BootstrapManifest.IsLoopbackTestUri(uri))
				throw new InvalidDataException("test manifest override must use file or loopback HTTP");
			return uri;
		}

		private static string GetOptionValue(string[] args, string option)
		{
			for (int i = 0; i + 1 < args.Length; i++)
				if (string.Equals(args[i], option, StringComparison.OrdinalIgnoreCase))
					return args[i + 1];
			return string.Empty;
		}
	}
}
