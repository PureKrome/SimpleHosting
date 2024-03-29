using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WorldDomination.SimpleHosting.SampleHostedServiceApplication
{
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
            _logger.LogInformation("Starting executing {name} worker.", _name);

            // Just delay for a bit .. then finish.
            // E.g. Doing some Database preparation.
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now.ToString());
            await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);

            _logger.LogInformation("Finishing StartAsync {name} worker.", _name);
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
