namespace DiveDeep.ViewModels
{
    public class BundleSlotViewModel
    {
        public string Title { get; set; } = "";
        public List<ProductVariantViewModel> Options { get; set; } = new List<ProductVariantViewModel>();
    }
}
