using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace DiveDeep.Models
{
    public class Booking
    {
        public int BookingId { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        [Display(Name = "Product")]
        public int ProductId { get; set; }

        [ValidateNever]
        [BindNever]
        public Product Product { get; set; } = null!;
        //public int Quantity { get; set; }
    }
}
