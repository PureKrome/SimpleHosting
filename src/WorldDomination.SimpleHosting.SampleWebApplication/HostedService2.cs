namespace WorldDomination.SimpleHosting.SampleWebApplication;

public class HostedService2 : CustomHostedService
{
    public HostedService2(ILogger<HostedService2> logger) : base("HostedService-2 (e.g. Database migrations)", logger)
    {
    }
}
