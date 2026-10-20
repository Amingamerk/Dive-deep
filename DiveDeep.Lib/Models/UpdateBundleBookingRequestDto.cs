namespace DiveDeep.Lib.Models
{
    public class UpdateBundleBookingRequestDto
    {
        // alle bookinger i pakken får den samme periode
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public List<BookingDto> Bookings { get; set; } = new List<BookingDto>();
    }
}
