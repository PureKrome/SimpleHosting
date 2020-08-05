using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WorldDomination.SimpleHosting.SampleWebApplication
{
    public class Program
    {
        public static Task Main(string[] args)
        {
            var options = new MainOptions
            {
                CommandLineArguments = args,
                FirstLoggingInformationMessage = "~~ Sample Web Application ~~",
                LogAssemblyInformation = true,
                LastLoggingInformationMessage = "-- Sample Web Application has ended/terminated --",

                CustomPreHostRunAsyncAction = new Action<IHost>(host =>
                {
                    using (var scope = host.Services.CreateScope())
                    {
                        var services = scope.ServiceProvider;
                        var logger = services.GetRequiredService<ILogger<Program>>();
                        logger.LogInformation($"Inside the {nameof(MainOptions.CustomPreHostRunAsyncAction)} method. Woot!");
                    }
                })
            };

            return SimpleHosting.Program.Main<Startup>(options);
        }
    }
}
