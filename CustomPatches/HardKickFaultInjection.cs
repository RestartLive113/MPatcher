using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using HarmonyLib;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
	// Isolated fault injector for testing MPatcher HardKick. It deliberately refuses
	// to arm outside the dedicated test copy and fails closed when its marker expires.
	internal static class HardKickFaultInjection
	{
		private const string PatchId = "local.moddev.machinecraft.hardkick-fault.v1";
		private const string RequiredRootName = "MachineCraftHardKickTarget";
		private const string MarkerFileName = "hardkick-fault-client.enabled";
		private const string MarkerToken = "MCDEV_HARDKICK_FAULT_INJECTION_V1";
		private const int MaxIgnoredMessages = 8;
		private const float LifetimeSeconds = 600f;

		private static Harmony harmony;
		private static MethodInfo targetMethod;
		private static StreamWriter writer;
		private static string markerPath;
		private static int ignoredMessages;
		private static float expiresAt;
		private static bool expirationLogged;
		private static bool limitLogged;

		internal static void TryRegister()
		{
			try
			{
				string root = Path.GetFullPath(Path.Combine(Application.dataPath, ".."));
				if (!string.Equals(Path.GetFileName(root), RequiredRootName, StringComparison.Ordinal))
				{
					LogShared("NOT_ARMED root=" + Path.GetFileName(root));
					return;
				}

				markerPath = Path.Combine(Path.Combine(root, "UserData"), MarkerFileName);
				if (!MarkerIsAuthorized())
				{
					LogShared("NOT_ARMED marker missing or invalid");
					return;
				}

				OpenLog(root);
				targetMethod = AccessTools.Method(typeof(Game), "RPC_SysMsg", new Type[] { typeof(string) });
				MethodInfo prefixMethod = typeof(HardKickFaultInjection).GetMethod(
					"Prefix", BindingFlags.Static | BindingFlags.NonPublic);
				if (targetMethod == null || prefixMethod == null || targetMethod.ReturnType != typeof(void))
					throw new MissingMethodException("Game.RPC_SysMsg(string)");

				harmony = new Harmony(PatchId);
				HarmonyMethod prefix = new HarmonyMethod(prefixMethod);
				prefix.priority = Priority.First;
				harmony.Patch(targetMethod, prefix, null, null, null);
				expiresAt = Time.realtimeSinceStartup + LifetimeSeconds;
				Log("ARMED target=Game.RPC_SysMsg lifetimeSeconds=600 maxIgnored=8 pid="
					+ Process.GetCurrentProcess().Id);
			}
			catch (Exception error)
			{
				Log("REGISTER_FAILED type=" + error.GetType().Name + " message=" + error.Message);
			}
		}

		private static bool Prefix(Game __instance, string INGDDFODJPD)
		{
			try
			{
				bool isBan = INGDDFODJPD != null && INGDDFODJPD.StartsWith("BAN:", StringComparison.Ordinal);
				bool isKick = INGDDFODJPD != null && INGDDFODJPD.StartsWith("KICK:", StringComparison.Ordinal);
				if (!isBan && !isKick)
					return true;

				if (!MarkerIsAuthorized())
				{
					Log("DISARMED marker missing or invalid; allowing " + (isBan ? "BAN" : "KICK"));
					return true;
				}
				if (Time.realtimeSinceStartup > expiresAt)
				{
					if (!expirationLogged)
					{
						expirationLogged = true;
						Log("DISARMED lifetime expired; allowing host moderation");
					}
					return true;
				}
				if (ignoredMessages >= MaxIgnoredMessages)
				{
					if (!limitLogged)
					{
						limitLogged = true;
						Log("DISARMED ignore limit reached; allowing host moderation");
					}
					return true;
				}

				GameObject localMachine = __instance.JPIAFJHAPHM;
				if (localMachine == null)
				{
					Log("PASS local machine unavailable");
					return true;
				}

				string targetName = INGDDFODJPD.Substring(isBan ? 4 : 5);
				if (!string.Equals(localMachine.name, targetName, StringComparison.Ordinal))
					return true;

				ignoredMessages++;
				Log("IGNORED kind=" + (isBan ? "BAN" : "KICK") + " target=" + targetName
					+ " count=" + ignoredMessages + "/" + MaxIgnoredMessages);
				return false;
			}
			catch (Exception error)
			{
				Log("PREFIX_FAILED type=" + error.GetType().Name + "; allowing original behavior");
				return true;
			}
		}

		private static bool MarkerIsAuthorized()
		{
			try
			{
				return markerPath != null
					&& File.Exists(markerPath)
					&& string.Equals(File.ReadAllText(markerPath).Trim(), MarkerToken, StringComparison.Ordinal);
			}
			catch (Exception)
			{
				return false;
			}
		}

		private static void OpenLog(string root)
		{
			string logDirectory = Path.Combine(Path.Combine(root, "logs"), "HardKickFault");
			Directory.CreateDirectory(logDirectory);
			string logPath = Path.Combine(logDirectory, "hardkick-fault-" + Process.GetCurrentProcess().Id
				+ "-" + DateTime.UtcNow.ToString("yyyyMMdd-HHmmss-fff") + ".log");
			writer = new StreamWriter(new FileStream(logPath, FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite),
				new UTF8Encoding(false));
			writer.AutoFlush = true;
		}

		private static void Log(string message)
		{
			string text = "[HARDKICK-FAULT] " + message;
			try
			{
				if (writer != null)
					writer.WriteLine(DateTime.UtcNow.ToString("o") + " " + text);
			}
			catch (Exception)
			{
			}
			LogShared(message);
		}

		private static void LogShared(string message)
		{
			string text = "[HARDKICK-FAULT] " + message;
			try { global::mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(text); }
			catch (Exception) { }
		}
	}
}
