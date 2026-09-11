namespace DiveDeep.Models
{
    public class BookingValidationResult
    {
        public bool IsSuccessful { get; set; }
        public string? ErrorMessage { get; set; }
        public string? Key { get; set; }
    }
}
