using DiveDeep.API.Data;
using DiveDeep.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DiveDeep.API.Persistence
{
    public class BookingRepository : IBookingRepository
    {

        private DiveDeepContext _diveDeepContext;

        public BookingRepository(DiveDeepContext diveDeepContext)
        {
            _diveDeepContext = diveDeepContext;
        }

        public async Task Add(Booking booking)
        {
            _diveDeepContext.Add<Booking>(booking);

            _diveDeepContext.SaveChanges();
        }

        public async Task Delete(int id)
        {
            Booking? booking = await GetById(id);
            if (booking == null) return;
            _diveDeepContext.Bookings.Remove(booking);

            _diveDeepContext.SaveChanges();
        }

        public async Task<List<Booking>> GetAll()
        {
            return _diveDeepContext.Bookings
                .Include(b => b.Product)
                .Include(b => b.BundleBooking)
                .Include(b => b.User)
                .ToList();
        }

        public async Task<Booking?> GetById(int id)
        {
            var booking = _diveDeepContext.Bookings
                .Include(b => b.Product)
                .Include(b => b.User)
                .Include(b => b.BundleBooking)
                .FirstOrDefault(x => x.BookingId == id);
            return booking;
        }

        public async Task<List<Booking>> GetByUserId(string id)
        {
            return _diveDeepContext.Bookings
                .Include(u => u.User)
                .Include(b => b.Product)
                .Include(b => b.BundleBooking)
                .Where(u => u.UserId == id.ToString())
                .ToList();
        }

        public async Task<Booking?> FindOverlappingBooking(int productId, DateTime startTime, DateTime endTime, int? excludedBookingId)
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

        public async Task Update(Booking booking)
        {
            Booking? bookingToUpdate = await GetById(booking.BookingId);
            if (bookingToUpdate == null) return;
            if (bookingToUpdate != null)
            {
                bookingToUpdate.StartTime = booking.StartTime;
                bookingToUpdate.EndTime = booking.EndTime;
                bookingToUpdate.ProductId = booking.ProductId;
            }
            _diveDeepContext.Entry(bookingToUpdate!).Property(b => b.RowVersion).OriginalValue = booking.RowVersion;
            _diveDeepContext.SaveChanges();
        }

        public async Task AddBundleBooking(BundleBooking bundleBooking)
        {
            _diveDeepContext.Add<BundleBooking>(bundleBooking);

            _diveDeepContext.SaveChanges();
        }

        public async Task<BundleBooking?> GetBundleBookingById(int id)
        {
            BundleBooking? bundleBooking = _diveDeepContext.BundleBookings
                .Include(bb => bb.Bookings)
                .ThenInclude(b => b.Product)
                .Include(bb => bb.Bookings)
                .ThenInclude(b => b.User)
                .FirstOrDefault(bb => bb.BundleBookingId == id);

            return bundleBooking;
        }

        public async Task UpdateBundleBooking(List<Booking> bookings)
        {
            foreach (Booking booking in bookings)
            {
                Booking? bookingToUpdate = await GetById(booking.BookingId);
                if (bookingToUpdate == null) continue;

                bookingToUpdate.StartTime = booking.StartTime;
                bookingToUpdate.EndTime = booking.EndTime;
                bookingToUpdate.ProductId = booking.ProductId;
                _diveDeepContext.Entry(bookingToUpdate).Property(b => b.RowVersion).OriginalValue = booking.RowVersion;
            }

            // gem hele pakken på én gang, så intet bliver gemt hvis der er en konflikt
            _diveDeepContext.SaveChanges();
        }

        public async Task DeleteBundleBooking(int id)
        {
            BundleBooking? bundleBooking = await GetBundleBookingById(id);
            if (bundleBooking == null) return;

            // bookingerne skal slettes med, ellers bliver de til enkeltbookinger
            _diveDeepContext.Bookings.RemoveRange(bundleBooking.Bookings);
            _diveDeepContext.BundleBookings.Remove(bundleBooking);

            _diveDeepContext.SaveChanges();
        }
    }
}
