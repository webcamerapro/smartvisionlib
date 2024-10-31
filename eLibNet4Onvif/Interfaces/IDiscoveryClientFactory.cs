using System.Collections.Generic;

namespace eLibNet4Onvif.Interfaces
{
    internal interface IDiscoveryClientFactory
    {
        IEnumerable<IDiscoveryClient> CreateDiscoveryClients();
    }
}