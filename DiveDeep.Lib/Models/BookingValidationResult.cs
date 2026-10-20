namespace DiveDeep.Lib.Models
{
    public class BookingValidationResult
    {
        public bool IsSuccessful { get; set; }
        public string? ErrorMessage { get; set; }
        public string? Key { get; set; }

        // Id på en nyoprettet pakkebooking, så web appen kan sende kunden videre til bekræftelsen
        public int? CreatedBundleBookingId { get; set; }
    }
}
