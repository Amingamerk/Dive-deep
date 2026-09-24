using System.Text.Json.Serialization;

namespace DiveDeep.Models.Weather
{
    public class Geocoding
    {

        [JsonPropertyName("results")]
        public List<Result> Results { get; set; }
    }

    // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
    public class Result
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }

        [JsonPropertyName("elevation")]
        public double Elevation { get; set; }

        [JsonPropertyName("postcodes")]
        public List<string> Postcodes { get; set; }
    }


}
