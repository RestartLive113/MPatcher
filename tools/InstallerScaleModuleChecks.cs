using System;
using System.IO;
using System.Reflection;
using MachineCraftMPatcherInstaller;

internal static class InstallerScaleModuleChecks
{
	private static int Main(string[] args)
	{
		try
		{
			string command = args[0], root = args[1];
			if (command == "probe")
			{
				Console.WriteLine(InstallerEngine.Probe(root).CurrentPayload);
				return 0;
			}
			if (command == "recover")
			{
				ScaleModulePayload.Recover(root, Console.WriteLine);
				return 0;
			}
			if (command == "interrupt-engine" || command == "rollback-engine")
			{
				bool injected = false;
				OperationResult fault = InstallerEngine.Install(root, delegate(string message)
				{
					Console.WriteLine(message);
					if (!injected && message.StartsWith("SCALE_MODULE_APPLIED", StringComparison.Ordinal))
					{
						injected = true;
						if (command == "interrupt-engine") Environment.Exit(0);
						throw new InvalidOperationException("Fixture: fail after module writes.");
					}
				});
				return fault.Success ? 0 : 1;
			}
			if (command == "interrupt" || command == "rollback")
			{
				string manifestPath = Path.Combine(root, "McnCraft_Data", "MPatcherFork.install.ini");
				InstallManifest manifest = File.Exists(manifestPath)
					? (InstallManifest)typeof(InstallerEngine).GetMethod("ReadManifest", BindingFlags.Static | BindingFlags.NonPublic).Invoke(null, new object[] { manifestPath })
					: new InstallManifest();
				ScaleModulePayload operation = ScaleModulePayload.Begin(root, manifest, false, Console.WriteLine);
				operation.Apply();
				ReplaceResource(PayloadInfo.ResourceName, Path.Combine(root, "McnCraft_Data", "Mono", "__Internal.dll"));
				ReplaceResource(PayloadInfo.WatchdogResourceName, Path.Combine(root, "McnCraft_Data", "MPatcherFork", "MPatcherCrashWatchdog.exe"));
				manifest.ProductVersion = PayloadInfo.Version;
				manifest.InstalledPayloadSha256 = PayloadInfo.Sha256;
				manifest.InstalledWatchdogSha256 = PayloadInfo.WatchdogSha256;
				operation.UpdateManifest(manifest);
				typeof(InstallerEngine).GetMethod("WriteManifest", BindingFlags.Static | BindingFlags.NonPublic).Invoke(null, new object[] { manifestPath, manifest, operation });
				if (command == "rollback") operation.Rollback();
				// For interrupt, exit with the persisted journal; the next process must restore all files.
				return 0;
			}
			OperationResult result = command == "install" ? InstallerEngine.Install(root, Console.WriteLine) : InstallerEngine.Uninstall(root, Console.WriteLine);
			Console.WriteLine(result.Message);
			return result.Success ? 0 : 1;
		}
		catch (Exception error)
		{
			Console.WriteLine(error.ToString());
			return 1;
		}
	}

	private static void ReplaceResource(string resource, string path)
	{
		Directory.CreateDirectory(Path.GetDirectoryName(path));
		string temporary = path + ".fixture-stage";
		using (Stream source = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource))
		using (FileStream destination = new FileStream(temporary, FileMode.CreateNew, FileAccess.Write)) source.CopyTo(destination);
		if (File.Exists(path)) File.Replace(temporary, path, null);
		else File.Move(temporary, path);
	}
}
