using DiveDeep.Data;
using DiveDeep.Models;

namespace DiveDeep.ViewModels
{
    public class BookingFilterViewModel
    {
        public string? UserId { get; set; }

        public ProductCategory? Category { get; set; }

        public string? Period { get; set; }

        public string? Type { get; set; }

        public List<ApplicationUser> Users { get; set; } = new();

        public List<Booking> Bookings { get; set; } = new();
    }
}
