using DiveDeep.Models;
using System.ComponentModel.DataAnnotations;

namespace DiveDeep.ViewModels
{
    public class ProductFormViewModel
    {
        public int ProductId { get; set; }

        public ProductCategory Category { get; set; }

        [Required(ErrorMessage = "Skriv et mærke")]
        public string Brand { get; set; } = "";

        public string? Model { get; set; }

        [Range(1, 10000, ErrorMessage = "Prisen skal være mellem 1 og 10000 kr")]
        public float PricePerDay { get; set; }

        // BCD, finner og dragter
        public Size Size { get; set; }

        // kun dragter
        public SuitType SuitType { get; set; }
        public string? Gender { get; set; }
        public string? Thickness { get; set; }

        // kun flasker
        public int VolumeLiters { get; set; }

        // kun regulatorsæt
        public string? FirstStep { get; set; }
        public string? SecondStep { get; set; }
        public string? Octopus { get; set; }

        public int? ProductImageId { get; set; }

        public IFormFile? ImageFile { get; set; }
    }
}
