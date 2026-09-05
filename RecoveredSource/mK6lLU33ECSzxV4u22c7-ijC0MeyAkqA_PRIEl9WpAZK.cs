using System;
using System.Diagnostics;
using System.IO;
using System.Text;

internal static class mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK
{
	internal static bool zZ8XBiwHZiD6wPKk_vbYVck;

	private static StreamWriter pbI40qI1KpksUE1BIoFfJ5w;

	private static bool vC0jINU0z82T_00243JYUIpdRn4;

	private static readonly object logSync = new object();

	internal static string CurrentLogPath { get; private set; }

	internal static string LogsDirectory { get; private set; }

	internal static string SessionStamp { get; private set; }

	internal static void YELeoCirSeVGf6u7nOIXkng(object object_0, bool bool_0 = false)
	{
		lock (logSync)
		{
			if (vC0jINU0z82T_00243JYUIpdRn4 || (!zZ8XBiwHZiD6wPKk_vbYVck && bool_0))
			{
				return;
			}
			try
			{
				if (pbI40qI1KpksUE1BIoFfJ5w == null)
				{
					try
					{
						string executable = Process.GetCurrentProcess().MainModule.FileName;
						string gameRoot = Path.GetDirectoryName(executable);
						LogsDirectory = Path.Combine(gameRoot, "logs");
					}
					catch (Exception)
					{
						LogsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "logs");
					}
					Directory.CreateDirectory(LogsDirectory);
					SessionStamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
					CurrentLogPath = Path.Combine(LogsDirectory, "MPatcher_" + SessionStamp
						+ "_pid" + Process.GetCurrentProcess().Id + ".log");
					pbI40qI1KpksUE1BIoFfJ5w = new StreamWriter(
						new FileStream(CurrentLogPath, FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite),
						new UTF8Encoding(false));
					pbI40qI1KpksUE1BIoFfJ5w.AutoFlush = true;
					pbI40qI1KpksUE1BIoFfJ5w.WriteLine("=== MPatcher Fork started "
						+ DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")
						+ " pid=" + Process.GetCurrentProcess().Id + " ===");
				}
				string level = bool_0 ? "DEBUG" : "INFO";
				pbI40qI1KpksUE1BIoFfJ5w.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")
					+ " [" + level + "] " + (object_0 ?? "<null>"));
			}
			catch (Exception)
			{
				vC0jINU0z82T_00243JYUIpdRn4 = true;
			}
		}
	}

	internal static void Flush()
	{
		lock (logSync)
		{
			try
			{
				if (pbI40qI1KpksUE1BIoFfJ5w != null)
					pbI40qI1KpksUE1BIoFfJ5w.Flush();
			}
			catch (Exception)
			{
			}
		}
	}

	internal static string[] smethod_0(string string_0)
	{
		return Directory.GetFiles(string_0);
	}

	internal static string smethod_1(string string_0)
	{
		return Path.GetFileName(string_0);
	}

	internal static bool smethod_2(string string_0, string string_1)
	{
		return string_0.StartsWith(string_1);
	}

	internal static bool smethod_3(string string_0, string string_1)
	{
		return string_0.EndsWith(string_1);
	}

	internal static void smethod_4(string string_0)
	{
		File.Delete(string_0);
	}

	internal static bool smethod_5(string string_0)
	{
		return File.Exists(string_0);
	}

	internal static Process smethod_6()
	{
		return Process.GetCurrentProcess();
	}

	internal static int smethod_7(Process process_0)
	{
		return process_0.Id;
	}
}
