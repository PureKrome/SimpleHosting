namespace WorldDomination.SimpleHosting.SampleWebApplication.Services;

public interface IWeatherService
{
    Task<IEnumerable<WeatherForecast>> GetWeatherAsync();
}
