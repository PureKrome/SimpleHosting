namespace WorldDomination.SimpleHosting.SampleWebApplication;

public class HostedBackgroundService : BackgroundService
{
    private readonly IServiceProvider _services;
    private readonly ILogger<HostedBackgroundService> _logger;

    public HostedBackgroundService(IServiceProvider services, ILogger<HostedBackgroundService> logger)
    {
        _services = services ?? throw new ArgumentNullException(nameof(services));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Consume Scoped Service Hosted Service running.");

        while (!cancellationToken.IsCancellationRequested)
        {
            _logger.LogDebug("Doing stuff that takes a while (like checking a queue) ...");

            await Task.Delay(1000 * 5, cancellationToken);

            _logger.LogDebug("Doing stuff - finisihed.");
        }
    }
}
