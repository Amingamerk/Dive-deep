
using System.Text.Json.Serialization;

namespace DiveDeep.Models
{
    public class CartItem
    {
        public int ProductId { get; set; }
        [JsonIgnore]
        public Product Product { get; set; }
        public string Brand { get; set; }

        public string Model { get; set; }

        public string? SelectedSize { get; set; }

        public string? SelectedGender { get; set; }

        public float PricePerDay { get; set; }

        public string ImagePath { get; set; }

        public int Quantity { get; set; }

        // Perioden gemmes i kurven, så bookingen først oprettes når kunden trykker "Book"
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }

}
