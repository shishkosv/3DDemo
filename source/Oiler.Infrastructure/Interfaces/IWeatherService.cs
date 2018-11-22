
using Oiler.Infrastructure.Models;

namespace Oiler.Infrastructure.Interfaces
{
    public interface IWeatherService
    {
        WeatherCollection GetWeatherHistory(string citySymbol);

        GroupWeatherCollection GetWeatherBreakdown(string citySymbol);
    }
}
