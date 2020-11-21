using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WorldDomination.SimpleHosting.SampleWebApplication.Services;

namespace WorldDomination.SimpleHosting.SampleWebApplication
{
    public class Startup
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new System.ArgumentNullException(nameof(configuration));
        }

        public Startup(IConfiguration configuration, ILogger logger)
        {
            _configuration = configuration ?? throw new System.ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            _logger.LogInformation("Configuring services");

            services.AddControllers();

            // Some fake database migration service.
            services.AddHostedService<HostedService1>();
            services.AddHostedService<HostedService2>();

            // This is a -real- Weather Service.
            services.AddTransient<IWeatherService, WeatherService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            _logger.LogInformation("Configuring Middleware");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                // Root/default route.
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });

                endpoints.MapControllers();
            });
        }
    }
}
