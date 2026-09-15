namespace DiveDeep.Models
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string Brand { get; set; }

        public string Model { get; set; }

        public string? SelectedSize { get; set; }

        public string? SelectedGender { get; set; }

        public float PricePerDay { get; set; }

        public string ImagePath { get; set; }

        public int Quantity { get; set; }
    }

}
