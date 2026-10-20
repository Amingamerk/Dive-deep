namespace DiveDeep.Lib.Models
{
    public class BookingDto
    {
        public int BookingId { get; set; }

        public DateTime StartTime { get; set; }
        
        public DateTime EndTime { get; set; }
        
        public int ProductId { get; set; }

        // Produktet og RowVersion er tomme når vi opretter en ny booking, så de må ikke være krav
        public ProductDto? Product { get; set; }

        public int? BundleBookingId { get; set; }

        public string? BundleName { get; set; }

        public string? UserId { get; set; }

        public byte[]? RowVersion { get; set; }
    }
}
