using static DiveDeep.Models.Enums;

namespace DiveDeep.Models
  
    
{
    public abstract class Product
    {
        public int Id { get; set; }
        public string Brand { get; set; } = "";
        public string? Model { get; set; } = "";
        public float PricePerDay { get; set; }
        public ProductCategory Category { get; set; }
       

        public virtual IEnumerable<string> SizeOptions => Enumerable.Empty<string>();
        public virtual IEnumerable<string> SuitTypeOptions => Enumerable.Empty<string>();
        public virtual string SizeLabel => "Størrelse"; 

    }
}
