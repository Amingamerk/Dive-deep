using DiveDeep.Lib.Models;

namespace DiveDeep.Services.HttpServices
{
    public interface IBookingHttpService
    {
        Task<List<BookingDto>> GetAll();
        Task<BookingDto?> GetById(int id);
        Task<List<BookingDto>> GetByUserId(string id);
        Task<BundleBookingDto?> GetBundleBookingById(int id);
        Task<BookingValidationResult> CreateBookings(List<BookingDto> bookings, List<BundleBookingDto> bundleBookings);
        Task<BookingValidationResult> ValidateBundleProducts(int bundleId, List<int> productIds, DateTime startTime, DateTime endTime);
        Task<BookingValidationResult> UpdateBooking(BookingDto booking);
        Task<BookingValidationResult> UpdateBundleBooking(List<BookingDto> bookings, DateTime startTime, DateTime endTime);
        Task Delete(int id);
        Task DeleteBundleBooking(int id);
    }
}
