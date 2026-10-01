using DiveDeep.Models.Weather;

namespace DiveDeep.Services.WeatherAPIHttp
{
    public interface IGeocodingHttpService
    {
        Task<Location?> GetLocationAsync(string locationInput);  

    }
}