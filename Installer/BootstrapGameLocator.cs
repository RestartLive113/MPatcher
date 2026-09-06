using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Win32;

namespace MachineCraftMPatcherInstaller
{
	internal sealed class BootstrapGameState
	{
		internal bool ValidGame;
		internal bool LoaderExists;
		internal bool ManagedInstall;
		internal string InstalledVersion;
	}

	internal static class BootstrapGameLocator
	{
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
				if (IsGameRoot(candidates[i]))
					return Path.GetFullPath(candidates[i]);
			return string.Empty;
		}

		internal static BootstrapGameState Probe(string root)
		{
			BootstrapGameState state = new BootstrapGameState();
			state.ValidGame = IsGameRoot(root);
			if (!state.ValidGame)
				return state;
			root = Path.GetFullPath(root.Trim());
			state.LoaderExists = File.Exists(Path.Combine(root, "McnCraft_Data", "Mono", "__Internal.dll"));
			string manifestPath = Path.Combine(root, "McnCraft_Data", "MPatcherFork.install.ini");
			state.ManagedInstall = File.Exists(manifestPath);
			if (state.ManagedInstall)
				state.InstalledVersion = ReadValue(manifestPath, "ProductVersion");
			return state;
		}

		internal static string GetInstallerLogPath(string root)
		{
			if (IsGameRoot(root))
			{
				string directory = Path.Combine(Path.GetFullPath(root.Trim()), "logs");
				Directory.CreateDirectory(directory);
				return Path.Combine(directory, "MPatcherInstaller.log");
			}
			string fallback = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MPatcher", "logs");
			Directory.CreateDirectory(fallback);
			return Path.Combine(fallback, "MPatcherInstaller.log");
		}

		private static string ReadValue(string path, string key)
		{
			try
			{
				string prefix = key + "=";
				string[] lines = File.ReadAllLines(path);
				for (int i = 0; i < lines.Length; i++)
					if (lines[i].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
						return lines[i].Substring(prefix.Length).Trim();
			}
			catch
			{
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
					if (string.Equals(candidates[i], full, StringComparison.OrdinalIgnoreCase))
						return;
				candidates.Add(full);
			}
			catch
			{
			}
		}
	}
}
