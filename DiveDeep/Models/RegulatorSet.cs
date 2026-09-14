namespace DiveDeep.Models
{
    public class RegulatorSet : Product
    {
        public string FirstStep { get; set; } = "";
        public string SecondStep { get; set; } = "";
        public string Octopus { get; set; } = "";
        public override string VariantLabel => $"{FirstStep} / {SecondStep} / {Octopus}";
        public override string VariantHeading => "Trin 1 / Trin 2 / Octopus";
        public override ProductCategory Category => ProductCategory.RegulatorSet;
    }
}
