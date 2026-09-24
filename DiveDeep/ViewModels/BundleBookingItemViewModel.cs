using DiveDeep.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace DiveDeep.ViewModels
{
    public class BundleBookingItemViewModel
    {
        public int BookingId { get; set; }

        public int ProductId { get; set; }

        public byte[] RowVersion { get; set; } = null!;

        [ValidateNever]
        public string CategoryTitle { get; set; } = "";

        [ValidateNever]
        public string ProductName { get; set; } = "";

        [BindNever]
        [ValidateNever]
        public List<Product> Variants { get; set; } = new();
    }
}
