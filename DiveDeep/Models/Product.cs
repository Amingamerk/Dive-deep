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
        public abstract ProductCategory Category { get; }

        public virtual string VariantLabel => Model ?? "";

        public virtual string VariantHeading => "Størrelse";

        public virtual string? VariantGroup => null;

        // Navigation: collection of bookings that reference this product
        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
