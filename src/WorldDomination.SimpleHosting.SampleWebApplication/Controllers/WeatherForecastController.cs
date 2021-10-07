using Microsoft.AspNetCore.Mvc;
using WorldDomination.SimpleHosting.SampleWebApplication.Services;

namespace WorldDomination.SimpleHosting.SampleWebApplication.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IWeatherService _weatherService;

    public WeatherForecastController(IWeatherService weatherService)
    {
        _weatherService = weatherService ?? throw new ArgumentNullException(nameof(weatherService));
    }

    [HttpGet]
    public async Task<IEnumerable<WeatherForecast>> Get()
    {
        return await _weatherService.GetWeatherAsync();
    }
}
