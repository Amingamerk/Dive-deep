using DiveDeep.API.Models;

namespace DiveDeep.API.Persistence
{
    public interface IBookingRepository
    {
        Task Add(Booking booking);
        Task Delete(int id);
        Task<List<Booking>> GetAll();
        Task<Booking?> GetById(int id);
        Task<List<Booking>> GetByUserId(string id);
        Task<Booking?> FindOverlappingBooking(int productId, DateTime startTime, DateTime endTime, int? excludedBookingId);
        Task Update(Booking booking);
        Task AddBundleBooking(BundleBooking bundleBooking);
        Task<BundleBooking?> GetBundleBookingById(int id);
        Task UpdateBundleBooking(List<Booking> bookings);
        Task DeleteBundleBooking(int id);
    }
}
