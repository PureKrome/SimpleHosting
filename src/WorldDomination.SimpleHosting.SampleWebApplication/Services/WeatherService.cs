namespace WorldDomination.SimpleHosting.SampleWebApplication.Services;

public class WeatherService : IWeatherService
{
    private static readonly string[] Summaries = new[]
    {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

    public async Task<IEnumerable<WeatherForecast>> GetWeatherAsync()
    {
        var rng = new Random();
        var result = Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = rng.Next(-20, 55),
            Summary = Summaries[rng.Next(Summaries.Length)]
        })
        .ToArray();

        return await Task.FromResult(result);
    }
}
