using DiveDeep.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace DiveDeep.ViewModels
{
    public class BookingEditViewModel
    {
        public Booking Booking { get; set; } = new();

        [ValidateNever]
        public string ProductName { get; set; } = "";

        [ValidateNever]
        public string UserEmail { get; set; } = "";

        // andre størrelser af samme model
        [BindNever]
        [ValidateNever]
        public List<Product> Variants { get; set; } = new();
    }
}
