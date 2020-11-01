using Microsoft.Extensions.Logging;

namespace WorldDomination.SimpleHosting.SampleWebApplication
{
    public class HostedService2 : CustomHostedService
    {
        public HostedService2(ILogger<HostedService1> logger) : base("HostedService-2", logger)
        {
        }
    }
}
