namespace DiveDeep.Lib.Models
{
    public class ValidateBundleProductsRequestDto
    {
        public int BundleId { get; set; }

        public List<int> ProductIds { get; set; } = new List<int>();

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}
