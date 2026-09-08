namespace DiveDeep.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int ProductId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Quantity { get; set; }
    }
}
