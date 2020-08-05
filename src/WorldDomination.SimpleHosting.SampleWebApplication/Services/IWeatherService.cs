using System.Collections.Generic;
using System.Threading.Tasks;

namespace WorldDomination.SimpleHosting.SampleWebApplication.Services
{
    public interface IWeatherService
    {
        Task<IEnumerable<WeatherForecast>> GetWeatherAsync();
    }
}
