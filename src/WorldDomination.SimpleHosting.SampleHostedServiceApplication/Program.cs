using System;
using System.Threading.Tasks;
using WorldDomination.SimpleHosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WorldDomination.SimpleHosting.SampleHostedServiceApplication
{
    public class Program
    {
        public static Task Main(string[] args)
        {
            var options = new MainOptions
            {
                CommandLineArguments = args,
                FirstLoggingInformationMessage = "~~ Sample Hosted Services Application ~~",
                LogAssemblyInformation = true,
                LastLoggingInformationMessage = "-- Sample Hosted Services Application has ended/terminated --",
                ConfigureCustomServices = new Action<HostBuilderContext, IServiceCollection>((hostContext, services) =>
                {
                    services.AddHostedService<HostedService1>();
                    services.AddHostedService<HostedService2>();
                }),
                CustomPreHostRunAsyncAction = new Action<IHost>(host =>
                {
                    using (var scope = host.Services.CreateScope())
                    {
                        var services = scope.ServiceProvider;
                        var logger = services.GetRequiredService<ILogger<Program>>();
                        logger.LogInformation($"Inside the {nameof(MainOptions.CustomPreHostRunAsyncAction)} method - before hosts all start. Woot!");
                    }
                })
            };


            return SimpleHosting.Program.Main<Program>(options);
        }
    }
}
