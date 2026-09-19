namespace DiveDeep.Models
{
    public class Cart
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();
        public List<CartBundle> Bundles { get; set; } = new List<CartBundle>();
    }
}
