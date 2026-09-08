using DiveDeep.Models;

namespace DiveDeep.Persistence
{
    public interface IBookingRepository
    {
        void Add(Booking booking);
        void Delete(int id);
        List<Booking> GetAll();
        Booking? GetById(int id);
        Booking? FindOverlappingBooking(int productId, DateTime startTime, DateTime endTime, int? excludedBookingId);
        void Update(Booking booking);
    }
}
