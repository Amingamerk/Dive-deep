namespace DiveDeep.Models
{
    public class Bundle
    {
        public int BundleId { get; set; }

        public string Name { get; set; } = "";

        public string Description { get; set; } = "";

        public List<ProductCategory> Categories { get; set; } = new List<ProductCategory>();

        public float DiscountPercent { get; set; }

        public float ApplyDiscount(float fullPrice)
        {
            return fullPrice * (100 - DiscountPercent) / 100;
        }
    }
}
