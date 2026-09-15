namespace DiveDeep.Models
{
    public class Cart
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();
        public decimal TatalPrice => (decimal)Items.Sum(i => i.PricePerDay * i.Quantity);
    }
}
