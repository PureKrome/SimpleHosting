using System.Threading.Tasks;
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

        [Fact]
        public async Task GivenAValidRequest_Get_ReturnsAnHttpStatus200OK()
        {
            var client = _factory.CreateClient();

            // Act.
            var result = await client.GetAsync("/WeatherForecast");

            // Assert.
            result.StatusCode.ShouldBe(System.Net.HttpStatusCode.OK);
        }
    }
}
