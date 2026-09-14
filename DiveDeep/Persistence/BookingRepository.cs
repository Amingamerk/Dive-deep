using DiveDeep.Data;
using DiveDeep.Models;
using Microsoft.EntityFrameworkCore;

namespace DiveDeep.Persistence
{
    public class BookingRepository : IBookingRepository
    {

        private DiveDeepContext _diveDeepContext;

        public BookingRepository(DiveDeepContext diveDeepContext)
        {
            _diveDeepContext = diveDeepContext;
        }

        public void Add(Booking booking)
        {
            _diveDeepContext.Add<Booking>(booking);

            _diveDeepContext.SaveChanges();
        }

        public void Delete(int id)
        {
            _diveDeepContext.Bookings.Remove(GetById(id));

            _diveDeepContext.SaveChanges();
        }

        public List<Booking> GetAll()
        {
            return _diveDeepContext.Bookings
                .Include(b => b.Product)
                .ToList();
        }

        public Booking? GetById(int id)
        {
            var booking = _diveDeepContext.Bookings
                .Include(b => b.Product)
                .FirstOrDefault(x => x.BookingId == id);
            return booking;
        }

        public Booking? FindOverlappingBooking(int productId, DateTime startTime, DateTime endTime, int? excludedBookingId)
        {
            var booking = _diveDeepContext.Bookings
                .Include(b => b.Product)
                .FirstOrDefault(x =>
                    x.ProductId == productId &&
                    x.StartTime < endTime &&
                    x.EndTime > startTime &&
                    (excludedBookingId == null || x.BookingId != excludedBookingId)
                    );
            return booking;
        }

        public void Update(Booking booking)
        {
            //Wait

            //var bookingToUpdate = GetById(booking.BookingId);
            //if (bookingToUpdate != null)
            //{
            //    //bookingToUpdate.Title = booking.Title;
            //    //bookingToUpdate.StartTime = booking.StartTime;
            //    //bookingToUpdate.EndTime = booking.EndTime;
            //    //bookingToUpdate.RoomId = booking.RoomId;
            //}
            //_diveDeepContext.Update<Booking>(bookingToUpdate);
            //_diveDeepContext.SaveChanges();
        }
    }
}
