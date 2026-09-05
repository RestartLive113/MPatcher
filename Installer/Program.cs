using System;
using System.Windows.Forms;

namespace MachineCraftMPatcherInstaller
{
	internal static class Program
	{
		[STAThread]
		private static int Main(string[] args)
		{
			InstallerText.UseSystemLanguage();
			string language = GetOptionValue(args, "--lang");
			if (!string.IsNullOrEmpty(language))
				InstallerText.TrySetLanguageCode(language);

			string installPath = GetOptionValue(args, "--install");
			if (!string.IsNullOrEmpty(installPath))
				return InstallerEngine.Install(installPath, null).Success ? 0 : 1;
			string uninstallPath = GetOptionValue(args, "--uninstall");
			if (!string.IsNullOrEmpty(uninstallPath))
				return InstallerEngine.Uninstall(uninstallPath, null).Success ? 0 : 1;

			string initialPath = GetOptionValue(args, "--path");
			if (string.IsNullOrEmpty(initialPath))
				initialPath = InstallerEngine.AutoDetectGameRoot();
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm(initialPath));
			return 0;
		}

		private static string GetOptionValue(string[] args, string option)
		{
			for (int i = 0; i + 1 < args.Length; i++)
			{
				if (string.Equals(args[i], option, StringComparison.OrdinalIgnoreCase))
					return args[i + 1];
			}
			return string.Empty;
		}
	}
}
