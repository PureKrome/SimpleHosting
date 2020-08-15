using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;

namespace WorldDomination.SimpleHosting
{
    public class Program
    {
        private static readonly string Explosion = @"" + Environment.NewLine +
"" + Environment.NewLine +
"" + Environment.NewLine +
"                             ____" + Environment.NewLine +
"                     __,-~~/~    `---." + Environment.NewLine +
"                   _/_,---(      ,    )" + Environment.NewLine +
"               __ /        <    /   )  \\___" + Environment.NewLine +
"- ------===;;;'====------------------===;;;===----- -  -" + Environment.NewLine +
"                  \\/  ~\"~\"~\"~\"~\"~\\~\"~)~\"/" + Environment.NewLine +
"                  (_ (   \\  (     >    \\)" + Environment.NewLine +
"                   \\_(_<> _>'" + Environment.NewLine +
"                      ~ `-i' ::>|--\"" + Environment.NewLine +
"                          I;|.|.|" + Environment.NewLine +
"                         <|i::|i|`." + Environment.NewLine +
"                        (` ^'\"`-' \")" + Environment.NewLine +
"------------------------------------------------------------------" + Environment.NewLine +
"[Nuclear Explosion Mushroom by Bill March]" + Environment.NewLine +
"" + Environment.NewLine +
"------------------------------------------------" + Environment.NewLine +
"";

        /// <summary>
        /// The program's main start/entry point. Hold on to your butts .... here we go!
        /// </summary>
        /// <typeparam name="T">Startup class type.</typeparam>
        /// <param name="args">Optional command line arguments.</param>
        /// <returns>Task of this Main application run.</returns>
        public static async Task Main<T>(string[] args) where T : class
        {
            var options = new MainOptions
            {
                CommandLineArguments = args
            };

            await Main<T>(options);
        }

        /// <summary>
        /// The program's main start/entry point. Hold on to your butts .... here we go!
        /// </summary>
        /// <typeparam name="T">Startup class type.</typeparam>
        /// <param name="options">Options to help setup/configure your program.</param>
        /// <returns>Task of this Main application run.</returns>
        public static async Task Main<T>(MainOptions options) where T : class
        {
            try
            {
                if (options is null)
                {
                    throw new ArgumentNullException(nameof(options));
                }

                // Before we do _ANYTHING_ we need to have a logger so we can start
                // seeing what is going on ... good or bad.
                Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(GetConfigurationBuilder())
                    .Enrich.FromLogContext()
                    .CreateLogger();

                // Display any (optional) initial banner / opening text to define the start of this application now starting.
                if (!string.IsNullOrWhiteSpace(options.FirstLoggingInformationMessage))
                {
                    Log.Information(options.FirstLoggingInformationMessage);
                }

                if (options.LogAssemblyInformation)
                {
                    var assembly = typeof(T).Assembly;
                    var assemblyDate = string.IsNullOrWhiteSpace(assembly.Location)
                                           ? "-- unknown --"
                                           : File.GetLastWriteTime(assembly.Location).ToString("u");

                    var assemblyInfo = $"Name: {assembly.GetName().Name} | Version: {assembly.GetName().Version} | Date: {assemblyDate}";

                    Log.Information(assemblyInfo);
                }

                var host = CreateHostBuilder<T>(options).Build();

                // Execute any pre-run action.
                options.CustomPreHostRunAsyncAction?.Invoke(host);

                // Ok, now lets go and start!
                await host.RunAsync();
            }
            catch (Exception exception)
            {
                const string errorMessage = "Something seriously unexpected has occurred while preparing the Host. Sadness :~(";

                // We might NOT have created a logger ... because we might be _trying_ to create the logger but
                // we have some bad setup-configuration-data and boom!!! No logger successfully setup/created.
                // So, if we do have a logger created, then use it.
                if (Log.Logger is Logger)
                {
                    // TODO: Add metrics (like Application Insights?) to log telemetry failures.
                    Log.Logger.Fatal(exception, errorMessage);
                }
                else
                {
                    // Nope - failed to create a logger and we have a serious error. So lets
                    // just fall back to the Console and _hope_ someone can read/access that.
                    Console.WriteLine(Explosion);
                    Console.WriteLine(errorMessage);
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine($"Error: {exception.Message}");
                    Console.WriteLine();
                }
            }
            finally
            {
                var shutdownMessage = string.IsNullOrWhiteSpace(options.LastLoggingInformationMessage)
                    ? "Application has now shutdown."
                    : options.LastLoggingInformationMessage;

                // Again: did we successfully create a logger?
                if (Log.Logger is Logger)
                {
                    Log.Information(shutdownMessage);

                    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                    Log.CloseAndFlush();
                }
                else
                {
                    Console.WriteLine(shutdownMessage);
                }
            }
        }

        private static IConfiguration GetConfigurationBuilder()
        {
            var builder =  new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);

            // Check any 'Environment' json files, like appsettings.Development.json.
            // REF: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/generic-host?#environmentname
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
                ?? Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")
                ?? "Production";

            return builder
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
        }

        public static IHostBuilder CreateHostBuilder<T>(MainOptions options) where T : class
        {
            var hostBuilder = Host
                .CreateDefaultBuilder(options.CommandLineArguments)
                .ConfigureLogging((context, logging) =>
                {
                    // Don't want any of that crap.
                    logging.ClearProviders();
                })
                .UseSerilog();

            // Do we setup any custom services, like custom hosted services?
            if (options.ConfigureCustomServices != null)
            {
                hostBuilder.ConfigureServices(options.ConfigureCustomServices);
            }
            else
            {
                // No custom services provided, so use the default web host.
                hostBuilder
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.UseStartup<T>();
                    });
            }

            return hostBuilder;
        }
    }
}
