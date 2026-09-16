namespace DiveDeep.ViewModels
{
    public class BundleCardViewModel
    {
        public int BundleId { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public float DiscountPercent { get; set; }
        public float FromPricePerDay { get; set; }
    }
}
