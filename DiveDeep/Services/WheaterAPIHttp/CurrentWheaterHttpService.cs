using DiveDeep.Models.Weather;
using System.Globalization;
using System.Net.Http.Json;
using System.Net.Http;

namespace DiveDeep.Services.WheaterAPIHttp
{
    public class CurrentWheaterHttpService : ICurrentWheaterHttpService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IGeocodingHttpService _geocodingService;

        public CurrentWheaterHttpService(IHttpClientFactory httpClientFactory, IGeocodingHttpService geocodingService)
        {
            _httpClientFactory = httpClientFactory;
            _geocodingService = geocodingService;
        }

        public async Task<CurrentWeather?> GetCurrentWeatherAsync(string locationInput)
        {
            if (string.IsNullOrWhiteSpace(locationInput))
            {
                return null;
            }

            Location? location = await _geocodingService.GetLocationAsync(locationInput);
            if (location is null)
            {
                return null;
            }

            HttpClient weatherClient = _httpClientFactory.CreateClient("OpenMeteoWheaterAPI");
            HttpClient marineClient = _httpClientFactory.CreateClient("OpenMeteoMarineWheaterAPI");

            HttpResponseMessage response;
            try
            {
                var weatherUrl = $"forecast?latitude={location.Latitude}&longitude={location.Longitude}" +
                 "&current=rain,wind_speed_10m,weather_code" +
                 "&timezone=auto&forecast_days=1&wind_speed_unit=ms";


                response = await weatherClient.GetAsync(weatherUrl);
                response.EnsureSuccessStatusCode();

                ForecastResponse? forecast = await response.Content.ReadFromJsonAsync<ForecastResponse>();

                if (forecast?.Current is null)
                {
                    return null;
                }

                MarineCurrent? marine = null;
                try
                {
                    var marineUrl = $"marine?latitude={location.Latitude}&longitude={location.Longitude}" +
                                    "&current=wave_height,sea_surface_temperature&forecast_days=1";

                    response = await marineClient.GetAsync(marineUrl);
                    response.EnsureSuccessStatusCode();

                    MarineResponse? marineResponse = await response.Content.ReadFromJsonAsync<MarineResponse>();
                    marine = marineResponse?.Current;
                }
                catch (HttpRequestException)
                {

                }

                return new CurrentWeather
                {
                    LocationName = location.Name,
                    Latitude = location.Latitude,
                    Longitude = location.Longitude,
                    WindSpeedMs = forecast.Current.WindSpeed10m,
                    RainMmPerHour = forecast.Current.Rain * 4,
                    WaveHeightM = marine?.WaveHeight,
                    SeaTemperatureC = marine?.SeaSurfaceTemperature,
                    IsThunderstorm = forecast.Current.WeatherCode >= 95
                };
            }
            catch (HttpRequestException ex)
            {
                throw new ApplicationException("Kunne ikke få forbindelse til Open-Meteo vejr-API'et.", ex);
            }
        }
    }
}