namespace DiveDeep.ViewModels
{
    public class BundleViewModel
    {
        public int BundleId { get; set; }
        public string Name { get; set; } = "";
        public float DiscountPercent { get; set; }
        public string? DateRange { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<BundleSlotViewModel> Slots { get; set; } = new();
        public bool AllSlotsAvailable { get; set; }

    }
}
