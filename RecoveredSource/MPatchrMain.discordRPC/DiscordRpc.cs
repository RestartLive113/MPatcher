using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace MPatchrMain.discordRPC;

public class DiscordRpc
{
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void ReadyCallback(ref DiscordUser connectedUser);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void DisconnectedCallback(int errorCode, string message);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void ErrorCallback(int errorCode, string message);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void JoinCallback(string secret);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void SpectateCallback(string secret);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void RequestCallback(ref DiscordUser request);

	public struct EventHandlers
	{
		public ReadyCallback readyCallback;

		public DisconnectedCallback disconnectedCallback;

		public ErrorCallback errorCallback;

		public JoinCallback joinCallback;

		public SpectateCallback spectateCallback;

		public RequestCallback requestCallback;
	}

	[Serializable]
	public struct RichPresenceStruct
	{
		public IntPtr state;

		public IntPtr details;

		public long startTimestamp;

		public long endTimestamp;

		public IntPtr largeImageKey;

		public IntPtr largeImageText;

		public IntPtr smallImageKey;

		public IntPtr smallImageText;

		public IntPtr partyId;

		public int partySize;

		public int partyMax;

		public IntPtr matchSecret;

		public IntPtr joinSecret;

		public IntPtr spectateSecret;

		public bool instance;
	}

	[Serializable]
	public struct DiscordUser
	{
		public string userId;

		public string username;

		public string discriminator;

		public string avatar;
	}

	public enum Reply
	{
		No,
		Yes,
		Ignore
	}

	public class RichPresence
	{
		private RichPresenceStruct hl63E3lf57o2LLkT5YJIeFU;

		private readonly List<IntPtr> FN0vvUIwPGkU7MSQfpciQ8Q = new List<IntPtr>(10);

		public string state;

		public string details;

		public long startTimestamp;

		public long endTimestamp;

		public string largeImageKey;

		public string largeImageText;

		public string smallImageKey;

		public string smallImageText;

		public string partyId;

		public int partySize;

		public int partyMax;

		public string matchSecret;

		public string joinSecret;

		public string spectateSecret;

		public bool instance;

		internal RichPresenceStruct D_00245fJv34UlT6sJiYe81og_Y()
		{
			if (FN0vvUIwPGkU7MSQfpciQ8Q.Count > 0)
			{
				eY3OuTrOVC5Fy8wP6RwUWu0();
			}
			hl63E3lf57o2LLkT5YJIeFU.state = ghNhrWUmIJjOOmXcHSQiRpM(state);
			hl63E3lf57o2LLkT5YJIeFU.details = ghNhrWUmIJjOOmXcHSQiRpM(details);
			hl63E3lf57o2LLkT5YJIeFU.startTimestamp = startTimestamp;
			hl63E3lf57o2LLkT5YJIeFU.endTimestamp = endTimestamp;
			hl63E3lf57o2LLkT5YJIeFU.largeImageKey = ghNhrWUmIJjOOmXcHSQiRpM(largeImageKey);
			hl63E3lf57o2LLkT5YJIeFU.largeImageText = ghNhrWUmIJjOOmXcHSQiRpM(largeImageText);
			hl63E3lf57o2LLkT5YJIeFU.smallImageKey = ghNhrWUmIJjOOmXcHSQiRpM(smallImageKey);
			hl63E3lf57o2LLkT5YJIeFU.smallImageText = ghNhrWUmIJjOOmXcHSQiRpM(smallImageText);
			hl63E3lf57o2LLkT5YJIeFU.partyId = ghNhrWUmIJjOOmXcHSQiRpM(partyId);
			hl63E3lf57o2LLkT5YJIeFU.partySize = partySize;
			hl63E3lf57o2LLkT5YJIeFU.partyMax = partyMax;
			hl63E3lf57o2LLkT5YJIeFU.matchSecret = ghNhrWUmIJjOOmXcHSQiRpM(matchSecret);
			hl63E3lf57o2LLkT5YJIeFU.joinSecret = ghNhrWUmIJjOOmXcHSQiRpM(joinSecret);
			hl63E3lf57o2LLkT5YJIeFU.spectateSecret = ghNhrWUmIJjOOmXcHSQiRpM(spectateSecret);
			hl63E3lf57o2LLkT5YJIeFU.instance = instance;
			return hl63E3lf57o2LLkT5YJIeFU;
		}

		private IntPtr ghNhrWUmIJjOOmXcHSQiRpM(string string_0)
		{
			if (smethod_0(string_0))
			{
				return IntPtr.Zero;
			}
			int num = smethod_2(smethod_1(), string_0);
			IntPtr intPtr = smethod_3(num + 1);
			for (int i = 0; i < num + 1; i++)
			{
				smethod_4(intPtr, i, (byte)0);
			}
			FN0vvUIwPGkU7MSQfpciQ8Q.Add(intPtr);
			smethod_6(smethod_5(smethod_1(), string_0), 0, intPtr, num);
			return intPtr;
		}

		private static string JF_0024xwQigVQBtyB1RKE8vgGUDYL7uc7Co6vgqASJ6ate4(string string_0)
		{
			string string_1 = smethod_7(string_0);
			byte[] array = smethod_5(smethod_8(), string_1);
			if (array.Length != 0 && array[array.Length - 1] != 0)
			{
				string_1 = smethod_9(string_1, global::_003CModule_003E.smethod_28<string>(1668539733u));
			}
			return smethod_10(smethod_1(), smethod_5(smethod_1(), string_1));
		}

		internal void eY3OuTrOVC5Fy8wP6RwUWu0()
		{
			for (int num = FN0vvUIwPGkU7MSQfpciQ8Q.Count - 1; num >= 0; num--)
			{
				smethod_11(FN0vvUIwPGkU7MSQfpciQ8Q[num]);
				FN0vvUIwPGkU7MSQfpciQ8Q.RemoveAt(num);
			}
		}

		internal static bool smethod_0(string string_0)
		{
			return string.IsNullOrEmpty(string_0);
		}

		internal static Encoding smethod_1()
		{
			return Encoding.UTF8;
		}

		internal static int smethod_2(Encoding encoding_0, string string_0)
		{
			return encoding_0.GetByteCount(string_0);
		}

		internal static IntPtr smethod_3(int int_0)
		{
			return Marshal.AllocHGlobal(int_0);
		}

		internal static void smethod_4(IntPtr intptr_0, int int_0, byte byte_0)
		{
			Marshal.WriteByte(intptr_0, int_0, byte_0);
		}

		internal static byte[] smethod_5(Encoding encoding_0, string string_0)
		{
			return encoding_0.GetBytes(string_0);
		}

		internal static void smethod_6(byte[] byte_0, int int_0, IntPtr intptr_0, int int_1)
		{
			Marshal.Copy(byte_0, int_0, intptr_0, int_1);
		}

		internal static string smethod_7(string string_0)
		{
			return string_0.Trim();
		}

		internal static Encoding smethod_8()
		{
			return Encoding.Default;
		}

		internal static string smethod_9(string string_0, string string_1)
		{
			return string_0 + string_1;
		}

		internal static string smethod_10(Encoding encoding_0, byte[] byte_0)
		{
			return encoding_0.GetString(byte_0);
		}

		internal static void smethod_11(IntPtr intptr_0)
		{
			Marshal.FreeHGlobal(intptr_0);
		}
	}

	[DllImport("discord-rpc", CallingConvention = CallingConvention.Cdecl)]
	public static extern void Discord_Initialize(string applicationId, ref EventHandlers handlers, bool autoRegister, string optionalSteamId);

	[DllImport("discord-rpc", CallingConvention = CallingConvention.Cdecl)]
	public static extern void Discord_Shutdown();

	[DllImport("discord-rpc", CallingConvention = CallingConvention.Cdecl)]
	public static extern void Discord_RunCallbacks();

	[DllImport("discord-rpc", CallingConvention = CallingConvention.Cdecl)]
	private static extern void Discord_UpdatePresence(ref RichPresenceStruct richPresenceStruct_0);

	[DllImport("discord-rpc", CallingConvention = CallingConvention.Cdecl)]
	public static extern void Discord_ClearPresence();

	[DllImport("discord-rpc", CallingConvention = CallingConvention.Cdecl)]
	public static extern void Discord_Respond(string userId, Reply reply);

	[DllImport("discord-rpc", CallingConvention = CallingConvention.Cdecl)]
	public static extern void Discord_UpdateHandlers(ref EventHandlers handlers);

	public static void UpdatePresence(RichPresence presence)
	{
		RichPresenceStruct richPresenceStruct_ = presence.D_00245fJv34UlT6sJiYe81og_Y();
		Discord_UpdatePresence(ref richPresenceStruct_);
		presence.eY3OuTrOVC5Fy8wP6RwUWu0();
	}
}
