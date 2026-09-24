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
            Booking? booking = GetById(id);
            if (booking == null) return;
            _diveDeepContext.Bookings.Remove(booking);

            _diveDeepContext.SaveChanges();
        }

        public List<Booking> GetAll()
        {
            return _diveDeepContext.Bookings
                .Include(b => b.Product)
                .Include(b => b.BundleBooking)
                .Include(b => b.User)
                .ToList();
        }

        public Booking? GetById(int id)
        {
            var booking = _diveDeepContext.Bookings
                .Include(b => b.Product)
                .Include(b => b.User)
                .Include(b => b.BundleBooking)
                .FirstOrDefault(x => x.BookingId == id);
            return booking;
        }

        public List<Booking> GetByUserId(string id)
        {
            return _diveDeepContext.Bookings
                .Include(u => u.User)
                .Include(b => b.Product)
                .Include(b => b.BundleBooking)
                .Where(u => u.UserId == id.ToString())
                .ToList();
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
            Booking? bookingToUpdate = GetById(booking.BookingId);
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

        public void AddBundleBooking(BundleBooking bundleBooking)
        {
            _diveDeepContext.Add<BundleBooking>(bundleBooking);

            _diveDeepContext.SaveChanges();
        }

        public BundleBooking? GetBundleBookingById(int id)
        {
            BundleBooking? bundleBooking = _diveDeepContext.BundleBookings
                .Include(bb => bb.Bookings)
                .ThenInclude(b => b.Product)
                .Include(bb => bb.Bookings)
                .ThenInclude(b => b.User)
                .FirstOrDefault(bb => bb.BundleBookingId == id);

            return bundleBooking;
        }

        public void UpdateBundleBooking(List<Booking> bookings)
        {
            foreach (Booking booking in bookings)
            {
                Booking? bookingToUpdate = GetById(booking.BookingId);
                if (bookingToUpdate == null) continue;

                bookingToUpdate.StartTime = booking.StartTime;
                bookingToUpdate.EndTime = booking.EndTime;
                bookingToUpdate.ProductId = booking.ProductId;
                _diveDeepContext.Entry(bookingToUpdate).Property(b => b.RowVersion).OriginalValue = booking.RowVersion;
            }

            // gem hele pakken på én gang, så intet bliver gemt hvis der er en konflikt
            _diveDeepContext.SaveChanges();
        }

        public void DeleteBundleBooking(int id)
        {
            BundleBooking? bundleBooking = GetBundleBookingById(id);
            if (bundleBooking == null) return;

            // bookingerne skal slettes med, ellers bliver de til enkeltbookinger
            _diveDeepContext.Bookings.RemoveRange(bundleBooking.Bookings);
            _diveDeepContext.BundleBookings.Remove(bundleBooking);

            _diveDeepContext.SaveChanges();
        }
    }
}
