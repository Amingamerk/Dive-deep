using DiveDeep.Models.Weather;
using System.Net.Http.Json;
using System.Net.Http;

namespace DiveDeep.Services.WheaterAPIHttp
{
    public class GeocodingHttpService : IGeocodingHttpService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public GeocodingHttpService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<Location?> GetLocationAsync(string locationInput)
        {
            if (string.IsNullOrWhiteSpace(locationInput))
                return null;

            HttpClient httpClient = _httpClientFactory.CreateClient("OpenMeteoGeocodingAPI");

            HttpResponseMessage response;
            try
            {
                var url = $"search?name={Uri.EscapeDataString(locationInput.Trim())}" +
                          "&count=1&language=da&format=json&countryCode=DK";

                response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();  

                Geocoding? geocoding = await response.Content.ReadFromJsonAsync<Geocoding>();

                if (geocoding?.Results is null || geocoding.Results.Count == 0)
                { 
                    return null; 
                }

                var result = geocoding.Results[0];
                return new Location(result.Name, result.Latitude, result.Longitude, result.Elevation);
            }
            catch (HttpRequestException ex)
            {
                throw new ApplicationException("Kunne ikke få forbindelse til Geocoding Open-Meteo API'et.", ex);
            }
        }
    }

}
