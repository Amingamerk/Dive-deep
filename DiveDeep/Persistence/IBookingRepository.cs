using DiveDeep.Models;

namespace DiveDeep.Persistence
{
    public interface IBookingRepository
    {
        void Add(Booking booking);
        void Delete(int id);
        List<Booking> GetAll();
        Booking? GetById(int id);
        List<Booking> GetByUserId(string id);
        Booking? FindOverlappingBooking(int productId, DateTime startTime, DateTime endTime, int? excludedBookingId);
        void Update(Booking booking);
        void AddBundleBooking(BundleBooking bundleBooking);
        BundleBooking? GetBundleBookingById(int id);
        void UpdateBundleBooking(List<Booking> bookings);
        void DeleteBundleBooking(int id);
    }
}
