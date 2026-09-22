using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace MPatcherFork.CustomPatches
{
	internal static class LegacyHostNetworkState
	{
		internal static string[] ReadAddresses()
		{
			List<string> addresses = new List<string>();
			foreach (NetworkInterface adapter in NetworkInterface.GetAllNetworkInterfaces())
			{
				if (adapter.OperationalStatus != OperationalStatus.Up
					|| adapter.NetworkInterfaceType == NetworkInterfaceType.Loopback) continue;
				foreach (UnicastIPAddressInformation address in adapter.GetIPProperties().UnicastAddresses)
					if (address.Address.AddressFamily == AddressFamily.InterNetwork)
						addresses.Add(address.Address.ToString());
			}
			return LegacyHostRoutePolicy.Normalize(addresses.ToArray());
		}
	}
}
