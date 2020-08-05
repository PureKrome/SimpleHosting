using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WorldDomination.SimpleHosting.SampleWebApplication
{
    public class CustomHostedService : IHostedService
    {
        private const string _name = "Custom Hosted Service";
        private readonly ILogger _logger;

        public CustomHostedService(ILogger<CustomHostedService> logger)
        {
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Starting executing {_name} worker.");

            // Just delay for a bit .. then finish.
            // E.g. Doing some Database preparation.
            _logger.LogInformation(" *** Pretending to do some Database migration or whatever ****");

            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now.ToString());
            await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);

            _logger.LogInformation($"Finishing executing {_name} worker.");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            // No Op.
            return Task.CompletedTask;
        }
    }
}
