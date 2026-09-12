namespace DiveDeep.ViewModels
{
    public class ProductViewModel
    {
        public string Brand { get; set; } = "";
        public string? Model { get; set; }
        public float PricePerDay { get; set; }
        public string VariantHeading { get; set; } = "Størrelse";
        public int SelectedProductId { get; set; }
        public List<ProductVariantViewModel> Variants { get; set; } = new();
        public List<string> BookedDates { get; set; } = new();
    }
}
