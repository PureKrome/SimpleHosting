using Microsoft.Extensions.Logging;

namespace WorldDomination.SimpleHosting.SampleHostedServiceApplication
{
    public class HostedService1 : CustomHostedService
    {
        public HostedService1(ILogger<HostedService1> logger) : base("HostedService-1", logger)
        {
        }
    }
}
