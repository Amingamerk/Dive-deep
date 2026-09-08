using DiveDeep.Models;

namespace DiveDeep.Persistence
{
    public static class BookingRepository
    {
        private static readonly List<Booking> bookings = new();
        private static bool _initialized = false;

        public static List<Booking> GetAll()
        {
            if (!_initialized)
            {
                InitializeTestBookings();
                _initialized = true;
            }
            return bookings;
        }

        public static Booking? GetById(int id) => bookings.FirstOrDefault(b => b.BookingId == id);

        public static void Add(Booking booking)
        {
            if (booking == null) return;
            booking.BookingId = bookings.Any() ? bookings.Max(b => b.BookingId) + 1 : 1;
            bookings.Add(booking);
        }

        public static void Update(Booking booking)
        {
            var existingBooking = GetById(booking.BookingId);
            if (existingBooking != null)
            {
                existingBooking.ProductId = booking.ProductId;
                existingBooking.StartTime = booking.StartTime;
                existingBooking.EndTime = booking.EndTime;
                existingBooking.Quantity = booking.Quantity;
            }
        }

        public static void Delete(int id) => bookings.RemoveAll(b => b.BookingId == id);

        private static void InitializeTestBookings()
        {
            // Test bookinger for BCD (Id: 1)
            Add(new Booking
            {
                ProductId = 1, // Scubapro Navigator Lite BCD - Small
                StartTime = DateTime.Now.AddDays(5),
                EndTime = DateTime.Now.AddDays(8),
                Quantity = 1
            });

            Add(new Booking
            {
                ProductId = 1, // Scubapro Navigator Lite BCD - Small
                StartTime = DateTime.Now.AddDays(15),
                EndTime = DateTime.Now.AddDays(20),
                Quantity = 1
            });

            // Test bookinger for DiveSuit (Id: 13)
            Add(new Booking
            {
                ProductId = 13, // Scubapro Definition 3mm - XtraSmall
                StartTime = DateTime.Now.AddDays(3),
                EndTime = DateTime.Now.AddDays(6),
                Quantity = 1
            });

            Add(new Booking
            {
                ProductId = 13,
                StartTime = DateTime.Now.AddDays(12),
                EndTime = DateTime.Now.AddDays(18),
                Quantity = 1
            });

            // Test bookinger for Tank (Id: 93)
            Add(new Booking
            {
                ProductId = 93, // Scubapro 5 liter tank
                StartTime = DateTime.Now.AddDays(7),
                EndTime = DateTime.Now.AddDays(10),
                Quantity = 1
            });

            // Test bookinger for MaskSnorkel (Id: 100)
            Add(new Booking
            {
                ProductId = 100, // Scubapro Ghost
                StartTime = DateTime.Now.AddDays(2),
                EndTime = DateTime.Now.AddDays(4),
                Quantity = 1
            });
        }
    }
}
