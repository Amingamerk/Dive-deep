namespace DiveDeep.Models
{
    public class Tank : Product
    {
        public int VolumeLiters { get; set; }
        public override string VariantLabel => $"{VolumeLiters} liter";
        public override ProductCategory Category => ProductCategory.Tank;
        public override string VariantHeading => "Volumen";
    }
}
