using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace DiveDeep.ViewModels
{
    public class BundleBookingEditViewModel
    {
        public int BundleBookingId { get; set; }

        [ValidateNever]
        public string BundleName { get; set; } = "";

        [ValidateNever]
        public string UserEmail { get; set; } = "";

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public List<BundleBookingItemViewModel> Items { get; set; } = new();
    }
}
