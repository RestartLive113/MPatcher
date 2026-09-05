using System;
using McnCraft;
using UnityEngine;

public class MCNPlayer
{
	internal MachineController zBDVDz48brB0y3J_0024BubuF9w;

	private OPLNFKECCLE BGwtRStXemlOx0njO90_0024Ppg;

	private NetworkPlayer legacyNetworkPlayer;

	private bool hasLegacyNetworkPlayer;

	private int eixI2D98jUdpHLf7924_0024YBs;

	private string bxB3vseLbvcO4byY7o57Kb0;

	private string h7qSw1OJN2MqL1qckstsw9U;

	public int plrID
	{
		get
		{
			if (eixI2D98jUdpHLf7924_0024YBs == 0 && photonPlayer != null)
			{
				eixI2D98jUdpHLf7924_0024YBs = smethod_0(photonPlayer);
			}
			return eixI2D98jUdpHLf7924_0024YBs;
		}
	}

	internal OPLNFKECCLE photonPlayer
	{
		get
		{
			if (HNJDDKJLHMM.FHLGOMHPDLN == HNJDDKJLHMM.HKGAACMIPIH.Legacy
				|| zBDVDz48brB0y3J_0024BubuF9w == null)
			{
				return null;
			}
			if (BGwtRStXemlOx0njO90_0024Ppg == null)
			{
				PhotonView view = zBDVDz48brB0y3J_0024BubuF9w.GetComponent<PhotonView>();
				if (view != null)
					BGwtRStXemlOx0njO90_0024Ppg = smethod_1(view);
			}
			return BGwtRStXemlOx0njO90_0024Ppg;
		}
	}

	internal bool hasLegacyPlayer => hasLegacyNetworkPlayer;

	internal NetworkPlayer legacyPlayer => legacyNetworkPlayer;

	internal bool MatchesLegacyPlayer(NetworkPlayer player)
	{
		return hasLegacyNetworkPlayer && legacyNetworkPlayer.guid == player.guid;
	}

	public string playerName
	{
		get
		{
			return bxB3vseLbvcO4byY7o57Kb0;
		}
		internal set
		{
			bxB3vseLbvcO4byY7o57Kb0 = value;
		}
	}

	public string machineName
	{
		get
		{
			return h7qSw1OJN2MqL1qckstsw9U;
		}
		internal set
		{
			h7qSw1OJN2MqL1qckstsw9U = value;
		}
	}

	internal MCNPlayer(MachineController machineController_0)
	{
		zBDVDz48brB0y3J_0024BubuF9w = machineController_0;
		bxB3vseLbvcO4byY7o57Kb0 = machineController_0.GOMAGBONMGB;
		h7qSw1OJN2MqL1qckstsw9U = machineController_0.KEBGCENDMCI;
		if (HNJDDKJLHMM.FHLGOMHPDLN == HNJDDKJLHMM.HKGAACMIPIH.Legacy)
		{
			NetworkView view = machineController_0.GetComponent<NetworkView>();
			if (view != null)
			{
				legacyNetworkPlayer = view.owner;
				hasLegacyNetworkPlayer = true;
				eixI2D98jUdpHLf7924_0024YBs = legacyNetworkPlayer.guid.GetHashCode();
			}
		}
		else
		{
			PhotonView view = machineController_0.GetComponent<PhotonView>();
			if (view != null)
				eixI2D98jUdpHLf7924_0024YBs = smethod_0(smethod_1(view));
		}
	}

	public void sendMessage(string msg)
	{
		if (HNJDDKJLHMM.FHLGOMHPDLN == HNJDDKJLHMM.HKGAACMIPIH.Legacy)
		{
			Game game = Arena.OEDCBNHNGMJ as Game;
			NetworkView view = game != null ? game.GetComponent<NetworkView>() : null;
			if (view == null || !hasLegacyNetworkPlayer)
				throw new InvalidOperationException("Legacy player transport is unavailable");
			view.RPC(global::_003CModule_003E.smethod_26<string>(3882697595u), legacyNetworkPlayer, new object[2]
			{
				smethod_2(global::_003CModule_003E.smethod_27<string>(1066875425u), msg),
				-1
			});
			MPatcherFork.CustomPatches.LegacyServerScripts.LogTransport("SEND player=" + plrID + " chars=" + (msg == null ? 0 : msg.Length));
			return;
		}
		smethod_3(Arena.OEDCBNHNGMJ.GetComponent<PhotonView>(), global::_003CModule_003E.smethod_26<string>(3882697595u), photonPlayer, new object[2]
		{
			smethod_2(global::_003CModule_003E.smethod_27<string>(1066875425u), msg),
			-1
		});
	}

	internal static int smethod_0(OPLNFKECCLE oplnfkeccle_0)
	{
		return oplnfkeccle_0.PMCNNMLPGBB;
	}

	internal static OPLNFKECCLE smethod_1(PhotonView photonView_0)
	{
		return photonView_0.MFNPHJFOIGC;
	}

	internal static string smethod_2(string string_0, string string_1)
	{
		return string_0 + string_1;
	}

	internal static void smethod_3(PhotonView photonView_0, string string_0, OPLNFKECCLE oplnfkeccle_0, object[] object_0)
	{
		photonView_0.RPC(string_0, oplnfkeccle_0, object_0);
	}
}
