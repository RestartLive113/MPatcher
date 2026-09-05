using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using McnCraft;
using UnityEngine;

public static class MCNServer
{
	[Serializable]
	[CompilerGenerated]
	private sealed class Class5
	{
		public static readonly Class5 _003C_003E9 = new Class5();

		public static Predicate<MCNPlayer> _003C_003E9__4_0;

		internal bool _0024QHyKwOMF_X9qIRoZF6aUkMU0mZDXs9m1mUDO_0024ixwg14(MCNPlayer mcnplayer_0)
		{
			if (mcnplayer_0.photonPlayer != null)
			{
				return smethod_0((UnityEngine.Object)mcnplayer_0.zBDVDz48brB0y3J_0024BubuF9w, (UnityEngine.Object)null);
			}
			return true;
		}

		internal static bool smethod_0(UnityEngine.Object object_0, UnityEngine.Object object_1)
		{
			return object_0 == object_1;
		}
	}

	[CompilerGenerated]
	private sealed class Class6
	{
		public MachineController bB4RX4LWgpSZhBBrKYj_00248es;

		internal bool vTuMBuSgSc4N_00244jSWcsAAdqUq5Zg6weRBxyfwR_0024FIjPR(MCNPlayer mcnplayer_0)
		{
			return smethod_0((UnityEngine.Object)mcnplayer_0.zBDVDz48brB0y3J_0024BubuF9w, (UnityEngine.Object)bB4RX4LWgpSZhBBrKYj_00248es);
		}

		internal static bool smethod_0(UnityEngine.Object object_0, UnityEngine.Object object_1)
		{
			return object_0 == object_1;
		}
	}

	[CompilerGenerated]
	private sealed class Class7
	{
		public MachineController qletaLcC6jh75TPOkbQk_e8;

		internal bool SRHdti6oMwshWOebrMKEKMvko47KsLY6SPVS4cL5kiwO(MCNPlayer mcnplayer_0)
		{
			return smethod_0((UnityEngine.Object)mcnplayer_0.zBDVDz48brB0y3J_0024BubuF9w, (UnityEngine.Object)qletaLcC6jh75TPOkbQk_e8);
		}

		internal static bool smethod_0(UnityEngine.Object object_0, UnityEngine.Object object_1)
		{
			return object_0 == object_1;
		}
	}

	internal static List<MCNPlayer> dPzzlzSuv9qXe46XqJWKwDU = new List<MCNPlayer>();

	public static MCNPlayer[] players => smethod_0();

	internal static MCNPlayer iyCMH8XqR8q4d_MbL6JGTluLbwIGhTxt9OQJCHTQYnTI(MachineController machineController_0)
	{
		smethod_0();
		return dPzzlzSuv9qXe46XqJWKwDU.Find((MCNPlayer mcnplayer_0) => Class6.smethod_0((UnityEngine.Object)mcnplayer_0.zBDVDz48brB0y3J_0024BubuF9w, (UnityEngine.Object)machineController_0));
	}

	internal static MCNPlayer[] smethod_0(bool bool_0 = false)
	{
		if (!bool_0)
		{
			if (HNJDDKJLHMM.FHLGOMHPDLN == HNJDDKJLHMM.HKGAACMIPIH.Legacy)
				dPzzlzSuv9qXe46XqJWKwDU.RemoveAll((MCNPlayer player) => !player.hasLegacyPlayer || Class5.smethod_0((UnityEngine.Object)player.zBDVDz48brB0y3J_0024BubuF9w, (UnityEngine.Object)null));
			else
				dPzzlzSuv9qXe46XqJWKwDU.RemoveAll((MCNPlayer mcnplayer_0) => mcnplayer_0.photonPlayer == null || Class5.smethod_0((UnityEngine.Object)mcnplayer_0.zBDVDz48brB0y3J_0024BubuF9w, (UnityEngine.Object)null));
		}
		foreach (MachineController qletaLcC6jh75TPOkbQk_e8 in Arena.PBBCHKBJAEA)
		{
			if (!dPzzlzSuv9qXe46XqJWKwDU.Exists((MCNPlayer mcnplayer_0) => Class7.smethod_0((UnityEngine.Object)mcnplayer_0.zBDVDz48brB0y3J_0024BubuF9w, (UnityEngine.Object)qletaLcC6jh75TPOkbQk_e8)))
			{
				dPzzlzSuv9qXe46XqJWKwDU.Add(new MCNPlayer(qletaLcC6jh75TPOkbQk_e8));
			}
		}
		return dPzzlzSuv9qXe46XqJWKwDU.ToArray();
	}

	public static void broadcastMessage(string msg)
	{
		if (HNJDDKJLHMM.FHLGOMHPDLN == HNJDDKJLHMM.HKGAACMIPIH.Legacy)
		{
			Game game = Arena.OEDCBNHNGMJ as Game;
			NetworkView view = game != null ? game.GetComponent<NetworkView>() : null;
			if (view == null)
				throw new InvalidOperationException("Legacy server transport is unavailable");
			view.RPC(global::_003CModule_003E.smethod_29<string>(1582385189u), RPCMode.All, new object[2]
			{
				smethod_1(global::_003CModule_003E.smethod_26<string>(1072847304u), msg),
				-1
			});
			MPatcherFork.CustomPatches.LegacyServerScripts.LogTransport("BROADCAST chars=" + (msg == null ? 0 : msg.Length));
			return;
		}
		smethod_2(Arena.OEDCBNHNGMJ.GetComponent<PhotonView>(), global::_003CModule_003E.smethod_29<string>(1582385189u), BFDCHLBGJHF.All, new object[2]
		{
			smethod_1(global::_003CModule_003E.smethod_26<string>(1072847304u), msg),
			-1
		});
	}

	public static void setSnow(int level)
	{
		level = Mathf.Clamp(level, 0, 9);
		if (smethod_3((UnityEngine.Object)(Arena.OEDCBNHNGMJ as Game)))
		{
			smethod_4(Arena.OEDCBNHNGMJ as Game, level, bool_0: false);
		}
	}

	public static void setRain(int level)
	{
		level = Mathf.Clamp(level, 0, 9);
		if (smethod_3((UnityEngine.Object)(Arena.OEDCBNHNGMJ as Game)))
		{
			smethod_4(Arena.OEDCBNHNGMJ as Game, level, bool_0: true);
		}
	}

	public static void setCloud(int level)
	{
		level = Mathf.Clamp(level, 0, 9);
		if (smethod_3((UnityEngine.Object)(Arena.OEDCBNHNGMJ as Game)))
		{
			smethod_5(Arena.OEDCBNHNGMJ as Game, -1f, -1, level);
		}
	}

	public static void setCycle(int cycle)
	{
		if (smethod_3((UnityEngine.Object)(Arena.OEDCBNHNGMJ as Game)))
		{
			smethod_5(Arena.OEDCBNHNGMJ as Game, -1f, cycle, -1);
		}
	}

	public static void setTime(int time)
	{
		if (smethod_3((UnityEngine.Object)(Arena.OEDCBNHNGMJ as Game)))
		{
			smethod_5(Arena.OEDCBNHNGMJ as Game, (float)time, -1, -1);
		}
	}

	public static void playMovie(string URL)
	{
		if (!smethod_6((UnityEngine.Object)(Arena.OEDCBNHNGMJ as Game), (UnityEngine.Object)null))
		{
			Game game = Arena.OEDCBNHNGMJ as Game;
			if (smethod_3((UnityEngine.Object)game.BIJHIJAIDBA))
			{
				smethod_7(game.BIJHIJAIDBA, URL, 0, bool_0: true);
			}
		}
	}

	public static void stopMovie()
	{
		if (!smethod_6((UnityEngine.Object)(Arena.OEDCBNHNGMJ as Game), (UnityEngine.Object)null))
		{
			Game game = Arena.OEDCBNHNGMJ as Game;
			if (smethod_3((UnityEngine.Object)game.BIJHIJAIDBA))
			{
				smethod_7(game.BIJHIJAIDBA, global::_003CModule_003E.smethod_29<string>(554603703u), 0, bool_0: true);
			}
		}
	}

	public static void pauseMovie()
	{
		if (!smethod_6((UnityEngine.Object)(Arena.OEDCBNHNGMJ as Game), (UnityEngine.Object)null))
		{
			Game game = Arena.OEDCBNHNGMJ as Game;
			if (smethod_3((UnityEngine.Object)game.BIJHIJAIDBA))
			{
				smethod_7(game.BIJHIJAIDBA, global::_003CModule_003E.smethod_28<string>(2213417069u), 0, bool_0: true);
			}
		}
	}

	public static void resumeMovie()
	{
		if (!smethod_6((UnityEngine.Object)(Arena.OEDCBNHNGMJ as Game), (UnityEngine.Object)null))
		{
			Game game = Arena.OEDCBNHNGMJ as Game;
			if (smethod_3((UnityEngine.Object)game.BIJHIJAIDBA))
			{
				smethod_7(game.BIJHIJAIDBA, global::_003CModule_003E.smethod_26<string>(3526537040u), 0, bool_0: true);
			}
		}
	}

	public static void StartCoroutine(IEnumerator routine)
	{
		smethod_8((MonoBehaviour)Arena.OEDCBNHNGMJ, routine);
	}

	internal static string smethod_1(string string_0, string string_1)
	{
		return string_0 + string_1;
	}

	internal static void smethod_2(PhotonView photonView_0, string string_0, BFDCHLBGJHF bfdchlbgjhf_0, object[] object_0)
	{
		photonView_0.RPC(string_0, bfdchlbgjhf_0, object_0);
	}

	internal static bool smethod_3(UnityEngine.Object object_0)
	{
		return object_0;
	}

	internal static void smethod_4(Game game_0, int int_0, bool bool_0)
	{
		game_0.SyncFallout(int_0, bool_0);
	}

	internal static void smethod_5(Game game_0, float float_0, int int_0, int int_1)
	{
		game_0.SyncAzureSky(float_0, int_0, int_1);
	}

	internal static bool smethod_6(UnityEngine.Object object_0, UnityEngine.Object object_1)
	{
		return object_0 == object_1;
	}

	internal static void smethod_7(StreamPlayer streamPlayer_0, string string_0, int int_0, bool bool_0)
	{
		streamPlayer_0.Play(string_0, int_0, bool_0);
	}

	internal static Coroutine smethod_8(MonoBehaviour monoBehaviour_0, IEnumerator ienumerator_0)
	{
		return monoBehaviour_0.StartCoroutine(ienumerator_0);
	}
}
