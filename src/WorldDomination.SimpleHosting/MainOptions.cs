using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WorldDomination.SimpleHosting
{
    public class MainOptions
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
        /// Custom action to configure your services. This will NOT setup the default web host.
        /// </summary>
        /// <remarks>An example of this would be for your own HostedService for doing DB setup/migrations or a Background Task which has no Kestrel server, running.</remarks>
        public Action<HostBuilderContext, IServiceCollection> ConfigureCustomServices { get; set; }

        /// <summary>
        /// Custom logic to invoke on the Host that was built, but before the host starts.<br/>
        /// An example of this could be some database migrations.
        /// </summary>
        public Action<IHost> CustomPreHostRunAsyncAction { get; set; }
    }
}
