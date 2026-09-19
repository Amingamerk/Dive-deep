namespace DiveDeep.Models
{
    public class CartBundle
    {
        public int BundleId { get; set; }
        public string BundleName { get; set; } = "";
        public float DiscountPercent { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public List<CartItem> Items { get; set; } = new();
    }
}
