namespace WorldDomination.SimpleHosting.SampleWebApplication;

public abstract class CustomHostedService : IHostedService
{
    private readonly string _name;
    private readonly ILogger _logger;

    public CustomHostedService(string name, ILogger logger)
    {
        _name = name;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Starting executing {_name} worker.");

        // Just delay for a bit .. then finish.
        // E.g. Doing some Database preparation.
        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now.ToString());
        await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);

        _logger.LogInformation($"Finishing StartAsync {_name} worker.");
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
