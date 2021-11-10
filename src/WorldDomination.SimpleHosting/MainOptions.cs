using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WorldDomination.SimpleHosting
{
    public class MainOptions<TStartup> where TStartup : class
    {
        /// <summary>
        /// Command line arguments.
        /// </summary>
        public string[] CommandLineArguments { get; set; }

        /// <summary>
        /// Optional text which is first displayed when the application starts.
        /// </summary>
        /// <remarks>This can be useful to help determine if things have started and are working ok.</remarks>
        public string FirstLoggingInformationMessage { get; set; }

        /// <summary>
        /// Write the assembly name, version and date information to the logger?
        /// </summary>
        public bool LogAssemblyInformation { get; set; } = true;

        /// <summary>
        /// Optional text which is last displayed when the application stops.
        /// </summary>
        /// <remarks>This could be useful to help determine when things are finally stopping.</remarks>
        public string LastLoggingInformationMessage { get; set; }

        /// <summary>
        /// Adds services to the container. This can be called multiple times and the results will be additive.
        /// </summary>
        /// <remarks>This is required for Background Hosted Services, where there is no ConfigureService method to override, such as with a Web host.</remarks>
        public Action<HostBuilderContext, IServiceCollection> ConfigureCustomServices { get; set; }

        public Func<WebHostBuilderContext, ILogger, TStartup> StartupActivation { get; set; }
    }
}
