using System.Net.NetworkInformation;
using System.Net.Sockets;
using eLibNet8Onvif.Interfaces;

namespace eLibNet8Onvif.Models;

internal class DiscoveryClientFactory : IDiscoveryClientFactory
{
    public IEnumerable<IDiscoveryClient> CreateDiscoveryClients()
    {
        var discoveryClients = new List<IDiscoveryClient>();
        foreach (var adapter in NetworkInterface.GetAllNetworkInterfaces())
        {
            if ((adapter.NetworkInterfaceType is not NetworkInterfaceType.Ethernet && adapter.NetworkInterfaceType is not NetworkInterfaceType.Wireless80211) ||
                adapter.OperationalStatus == OperationalStatus.Down ||
                !adapter.Supports(NetworkInterfaceComponent.IPv4))
                continue;
            foreach (var ipAddress in adapter.GetIPProperties().UnicastAddresses.Select(ipAddressInfo => ipAddressInfo.Address))
                try
                {
                    if (ipAddress.AddressFamily != AddressFamily.InterNetwork)
                        continue;
                    discoveryClients.Add(new DiscoveryClient(new(ipAddress, 0)));
                } catch (SocketException)
                {
                    //ignored
                }
        }

        return discoveryClients;
    }
}