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

			string automaticUpdatePath = GetOptionValue(args, "--auto-update");
			if (!string.IsNullOrEmpty(automaticUpdatePath))
			{
				int waitPid;
				if (!int.TryParse(GetOptionValue(args, "--wait-pid"), out waitPid) || waitPid <= 0)
					return 2;
				return AutoUpdateRunner.Run(automaticUpdatePath, waitPid, HasOption(args, "--restart-game"),
					GetOptionValue(args, "--restart-update-manifest"));
			}

			string installPath = GetOptionValue(args, "--install");
			if (!string.IsNullOrEmpty(installPath))
				return CompleteCommand(installPath, InstallerEngine.Install(installPath, null),
					GetOptionValue(args, "--result-file"));
			string uninstallPath = GetOptionValue(args, "--uninstall");
			if (!string.IsNullOrEmpty(uninstallPath))
				return CompleteCommand(uninstallPath, InstallerEngine.Uninstall(uninstallPath, null),
					GetOptionValue(args, "--result-file"));

			string initialPath = GetOptionValue(args, "--path");
			if (string.IsNullOrEmpty(initialPath))
				initialPath = InstallerEngine.AutoDetectGameRoot();
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm(initialPath));
			return 0;
		}

		private static int CompleteCommand(string gameRoot, OperationResult result, string resultPath)
		{
			try
			{
				InstallerResultProtocol.Write(resultPath, result.Success, result.Message, result.ErrorDetails, result.LogPath,
					result.ClosedGameProcessCount, result.ForcedGameProcessCount);
			}
			catch (Exception error)
			{
				try
				{
					InstallerEngine.LogAutomaticEvent(gameRoot, "COMMAND_RESULT_WRITE_FAILED type="
						+ error.GetType().Name + " message="
						+ (error.Message ?? string.Empty).Replace('\r', ' ').Replace('\n', ' '));
				}
				catch
				{
				}
			}
			return result.Success ? 0 : 1;
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

		private static bool HasOption(string[] args, string option)
		{
			for (int i = 0; i < args.Length; i++)
				if (string.Equals(args[i], option, StringComparison.OrdinalIgnoreCase))
					return true;
			return false;
		}
	}
}
