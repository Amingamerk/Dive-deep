using DiveDeep.Models.Weather;

namespace DiveDeep.Services.WheaterAPIHttp
{
    public interface ICurrentWheaterHttpService
    {
        Task<CurrentWeather?> GetCurrentWeatherAsync(string locationInput);
    }
}