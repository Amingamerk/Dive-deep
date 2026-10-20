namespace DiveDeep.Lib.Models
{
    public class BundleBookingDto
    {
        public int BundleBookingId { get; set; }

        public int BundleId { get; set; }

        public string BundleName { get; set; } = "";

        public float DiscountPercent { get; set; }

        public DateTime CreatedAt { get; set; }

        public List<BookingDto> Bookings { get; set; } = new List<BookingDto>();
    }
}
