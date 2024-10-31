namespace eLibNet8Onvif.Interfaces;

internal interface IDiscoveryClientFactory
{
    public IEnumerable<IDiscoveryClient> CreateDiscoveryClients();
}