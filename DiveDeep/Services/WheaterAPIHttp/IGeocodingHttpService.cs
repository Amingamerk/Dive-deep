using DiveDeep.Models.Weather;

namespace DiveDeep.Services.WheaterAPIHttp
{
    public interface IGeocodingHttpService
    {
        Task<Location?> GetLocationAsync(string locationInput);  

    }
}