using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Shouldly;
using Xunit;

namespace WorldDomination.SimpleHosting.SampleWebApplication.Tests.WeatherForecastControllerTests
{
    public class GetTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;

        public GetTests(CustomWebApplicationFactory factory)
        {
            _factory = factory;
        }

        public static TheoryData<MainOptions<Startup>> Data => new TheoryData<MainOptions<Startup>>
        {
            { 
                new MainOptions<Startup>()
            },
            {
                new MainOptions<Startup>
                {
                    StartupActivation = new System.Func<WebHostBuilderContext, ILogger, Startup>((context, logger) => new Startup(context.Configuration, logger))
                }
            }
        };

        [Theory]
        [MemberData(nameof(Data))]
        public async Task GivenAValidRequest_Get_ReturnsAnHttpStatus200OK(MainOptions<Startup> mainOptions)
        {
            _factory.MainOptions = mainOptions;

            var client = _factory.CreateClient();

            // Act.
            var result = await client.GetAsync("/WeatherForecast");

            // Assert.
            result.StatusCode.ShouldBe(System.Net.HttpStatusCode.OK);
        }
    }
}
