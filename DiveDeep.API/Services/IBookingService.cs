using DiveDeep.API.Models;
using DiveDeep.Lib.Models;
namespace DiveDeep.API.Services
{
    public interface IBookingService
    {
        // Generelle "værktøjsmetoder" herunder

        Task<BookingValidationResult> ValidateDates(DateTime startTime, DateTime endTime);

        // Tjekker at hvert produkt passer til sin plads i pakken og er ledigt i perioden
        Task<BookingValidationResult> ValidateBundleProducts(int bundleId, List<int> products, DateTime startTime, DateTime endTime);

        // API metoder herunder

        Task<List<Booking>> GetAll();

        Task<List<Booking>> GetByUserId(string id);

        Task<Booking?> GetById(int id);

        Task Delete(int id);

        Task<BundleBooking?> GetBundleBookingById(int id);

        Task DeleteBundleBooking(int id);

        Task<BookingValidationResult> UpdateBooking(Booking booking);

        Task<BookingValidationResult> UpdateBundleBooking(List<Booking> bookings, DateTime startTime, DateTime endTime);

        Task<BookingValidationResult> CreateBookings(List<Booking> bookings, List<BundleBooking> bundleBookings);
    }
}
