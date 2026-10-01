using DiveDeep.Models.Weather;

namespace DiveDeep.Services.WeatherAPIHttp
{
    public interface ICurrentWeatherHttpService
    {
        Task<CurrentWeather?> GetCurrentWeatherAsync(string locationInput);
    }
}