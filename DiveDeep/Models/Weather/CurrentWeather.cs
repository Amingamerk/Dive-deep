using System.Text.Json.Serialization;

namespace DiveDeep.Models.Weather
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
    public class CurrentWeather
    {
        [JsonPropertyName("time")]
        public string Time { get; set; }

        [JsonPropertyName("interval")]
        public int Interval { get; set; }

        [JsonPropertyName("rain")]
        public double Rain { get; set; }

        [JsonPropertyName("wind_speed_10m")]
        public double WindSpeed10m { get; set; }

        [JsonPropertyName("weather_code")]
        public int WeatherCode { get; set; }
    }

    public class CurrentMeasurements
    {
        [JsonPropertyName("time")]
        public string Time { get; set; }

        [JsonPropertyName("interval")]
        public string Interval { get; set; } = "seconds";

        [JsonPropertyName("rain")]
        public string Rain { get; set; } = "mm";

        [JsonPropertyName("wind_speed_10m")]
        public string WindSpeed10m { get; set; } = "m/s";

        [JsonPropertyName("weather_code")]
        public string WeatherCode { get; set; }
    }

    public class Root
    {
        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }

        [JsonPropertyName("generationtime_ms")]
        public double GenerationtimeMs { get; set; }

        [JsonPropertyName("utc_offset_seconds")]
        public int UtcOffsetSeconds { get; set; } = 7200;

        [JsonPropertyName("timezone")]
        public string Timezone { get; set; } = "Europe/Copenhagen";

        [JsonPropertyName("timezone_abbreviation")]
        public string TimezoneAbbreviation { get; set; } = "GMT+2";

        [JsonPropertyName("elevation")]
        public double Elevation { get; set; } = 18;

        [JsonPropertyName("current_measurements")]
        public CurrentMeasurements CurrentMeasurements { get; set; }

        [JsonPropertyName("currentWeather")]
        public CurrentWeather CurrentWeather { get; set; }
    }


}

