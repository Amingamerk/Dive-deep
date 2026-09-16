namespace DiveDeep.ViewModels
{
    public class BundleConfirmationViewModel
    {
        public string BundleName { get; set; } = "";

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int Days { get; set; }

        public List<string> ProductLabels { get; set; } = new List<string>();

        public float FullPrice { get; set; }

        public float DiscountPercent { get; set; }

        public float TotalPrice { get; set; }
    }
}
