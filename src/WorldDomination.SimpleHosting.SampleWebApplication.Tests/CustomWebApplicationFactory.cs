using System.Linq;
using FizzWare.NBuilder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;
using WorldDomination.SimpleHosting.SampleWebApplication.Services;

namespace WorldDomination.SimpleHosting.SampleWebApplication.Tests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override IHostBuilder CreateHostBuilder() => SimpleHosting.Program.CreateHostBuilder<Startup>(new MainOptions());

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove the existing registration for an IWeatherService.
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IWeatherService));
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // Use a mocked IWeatherService.
                var weatherForcasts = Builder<WeatherForecast>.CreateListOfSize(5).Build();
                var weatherService = new Mock<IWeatherService>();
                weatherService.Setup(x => x.GetWeatherAsync()).ReturnsAsync(weatherForcasts);
                services.AddTransient(_ => weatherService.Object);
            });
        }
    }
}
