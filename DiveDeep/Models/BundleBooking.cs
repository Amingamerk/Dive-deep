namespace DiveDeep.Models
{
    public class BundleBooking
    {
        public int BundleBookingId { get; set; }

        public int BundleId { get; set; }

        public string BundleName { get; set; } = "";

        public float DiscountPercent { get; set; }

        public DateTime CreatedAt { get; set; }

        public List<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
