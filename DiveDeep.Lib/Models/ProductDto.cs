namespace DiveDeep.Lib.Models
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string Brand { get; set; } = "";
        public string? Model { get; set; } = "";
        public float PricePerDay { get; set; }
        public ProductCategory Category { get; set; }

        public int? ProductImageId { get; set; } // skal bruge et api kald for at få fat i billedet

        public string VariantLabel { get; set; } = "";

        public string VariantHeading { get; set; } = "";

        public string? VariantGroup { get; set; }

        // bruges af Fins, DiveSuit og BCD
        public Size? Size { get; set; }

        // bruges af RegulatorSet:
        public string? FirstStep { get; set; }
        public string? SecondStep { get; set; }
        public string? Octopus { get; set; }

        // bruges af Tank:
        public int? VolumeLiters { get; set; }

        // bruges af DiveSuit:
        public SuitType? SuitType { get; set; }
        public string? Gender { get; set; }

        // bruges kun af våddragter, ikke tørdragter
        public string? Thickness { get; set; }

        // Bruges af Details.cshtml
        public List<DateTime> BookedDates { get; set; } = new List<DateTime>();
        public virtual ICollection<ProductCategory> Categories { get; set; } = new List<ProductCategory>();



    }
}
