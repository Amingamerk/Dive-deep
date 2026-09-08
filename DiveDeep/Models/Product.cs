using System.Collections.Generic;
using DiveDeep.Models;

namespace DiveDeep.Models
{
    public abstract class Product
    {
        public int ProductId { get; set; }
        public string Brand { get; set; } = "";
        public string? Model { get; set; } = "";
        public float PricePerDay { get; set; }
        public Enums.ProductCategory Category { get; set; }
        public virtual IEnumerable<string> SizeOptions { get; }
        public virtual IEnumerable<string> SuitTypeOptions { get; }
        public virtual string SizeLabel { get; }

        // Navigation: collection of bookings that reference this product
        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
