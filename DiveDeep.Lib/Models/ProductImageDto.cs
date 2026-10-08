using System;
using System.Collections.Generic;
using System.Text;

namespace DiveDeep.Lib.Models
{
    public class ProductImageDto
    {
        public int ProductImageId { get; set; }
        public byte[] Image { get; set; } = Array.Empty<byte>();
        public string ContentType { get; set; } = "";
    }
}
