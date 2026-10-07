using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DiveDeep.Lib.Models
{
    public class BookingDto
    {
        public int BookingId { get; set; }

        public DateTime StartTime { get; set; }
        
        public DateTime EndTime { get; set; }
        
        public ProductDto productDto { get; set; }

        public int? BundleBookingId { get; set; }
        
        public string? BundleName { get; set; }

        public string UserId { get; set; }

        public byte[] RowVersion { get; set; } = null!;
    }
}
