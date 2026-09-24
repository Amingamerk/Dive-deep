namespace DiveDeep.Services
{
    public class ImageService
    {
        // Image to ByteArray
        public byte[] ReadFileBytes(IFormFile file)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                file.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}
