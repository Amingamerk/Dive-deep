namespace DiveDeep.Models
{
    public class ProductImage
    {
        public int ProductImageId { get; set; }
        public byte[] Image { get; set; } = Array.Empty<byte>();
        public string ContentType { get; set; } = "";
    }
}
