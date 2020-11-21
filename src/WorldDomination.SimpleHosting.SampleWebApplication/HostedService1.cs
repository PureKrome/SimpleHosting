using Microsoft.Extensions.Logging;

namespace WorldDomination.SimpleHosting.SampleWebApplication
{
    public class HostedService1 : CustomHostedService
    {
        public HostedService1(ILogger<HostedService1> logger) : base("HostedService-1 (e.g. Database setup / seeding)", logger)
        {
        }
    }
}
