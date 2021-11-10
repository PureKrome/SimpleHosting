using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WorldDomination.SimpleHosting.SampleHostedServiceApplication
{
    public class Program
    {
        public static Task Main(string[] args)
        {
            var options = new MainOptions<Program>
            {
                CommandLineArguments = args,
                FirstLoggingInformationMessage = "~~ Sample Hosted Services Application ~~",
                LogAssemblyInformation = true,
                LastLoggingInformationMessage = "-- Sample Hosted Services Application has ended/terminated --",
                ConfigureCustomServices = new Action<HostBuilderContext, IServiceCollection>((hostContext, services) =>
                {
                    services.AddHostedService<HostedService1>();
                    services.AddHostedService<HostedService2>();
                })
            };

            return SimpleHosting.Program.Main(options);
        }
    }
}
