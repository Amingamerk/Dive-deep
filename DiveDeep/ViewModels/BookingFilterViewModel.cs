using DiveDeep.Data;
using DiveDeep.Lib.Models;
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

        public List<BookingDto> Bookings { get; set; } = new();

        // bookingen kender kun brugerens id, så mailen slås op i listen vi allerede har hentet
        public string EmailFor(string userId)
        {
            ApplicationUser? user = Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return "";
            }

            return user.Email ?? "";
        }

        public List<BookingDto> Apply()
        {
            List<BookingDto> result = Bookings;

            if (UserId != null)
            {
                result = result.Where(b => b.UserId == UserId).ToList();
            }

            if (Category != null)
            {
                result = result.Where(b => b.Product.Category == Category).ToList();
            }

            if (Period == "Kommende")
            {
                result = result.Where(b => b.StartTime > DateTime.Today).ToList();
            }
            else if (Period == "Igangværende")
            {
                result = result.Where(b => b.StartTime <= DateTime.Today && b.EndTime >= DateTime.Today).ToList();
            }
            else if (Period == "Afsluttede")
            {
                result = result.Where(b => b.EndTime < DateTime.Today).ToList();
            }

            if (Type == "Enkelt")
            {
                result = result.Where(b => b.BundleBookingId == null).ToList();
            }
            else if (Type == "Pakke")
            {
                result = result.Where(b => b.BundleBookingId != null).ToList();
            }

            return result;
        }
    }
}
