namespace DiveDeep.Lib.Models
{
    public class CreateBookingsRequestDto
    {
        // Kurven sender Bookings og BundleBookings samtidigt, så vi laver en DTO til at håndtere det
        public List<BookingDto> Bookings { get; set; } = new List<BookingDto>();

        public List<BundleBookingDto> BundleBookings { get; set; } = new List<BundleBookingDto>();


    }
}
