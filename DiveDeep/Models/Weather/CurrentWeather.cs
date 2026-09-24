using System.Text.Json.Serialization;

namespace DiveDeep.Models.Weather
{
    // Svar fra api.open-meteo.com/v1/forecast
    public class ForecastResponse
    {
        [JsonPropertyName("current")]
        public ForecastCurrent? Current { get; set; }
    }

    public class ForecastCurrent
    {
        [JsonPropertyName("interval")]
        public int Interval { get; set; }

        [JsonPropertyName("rain")]
        public double Rain { get; set; }

        [JsonPropertyName("wind_speed_10m")]
        public double WindSpeed10m { get; set; }

        [JsonPropertyName("weather_code")]
        public int WeatherCode { get; set; }
    }

    // Svar fra marine-api.open-meteo.com/v1/marine
    public class MarineResponse
    {
        [JsonPropertyName("current")]
        public MarineCurrent? Current { get; set; }
    }

    public class MarineCurrent
    {
        [JsonPropertyName("wave_height")]
        public double? WaveHeight { get; set; }

        [JsonPropertyName("sea_surface_temperature")]
        public double? SeaSurfaceTemperature { get; set; }
    }

    public class CurrentWeather
    {
        public string LocationName { get; set; } = "";
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public double WindSpeedMs { get; set; }
        public double RainMm { get; set; }
        public double? WaveHeightM { get; set; }
        public double? SeaTemperatureC { get; set; }
        public bool IsThunderstorm { get; set; }
    }
}