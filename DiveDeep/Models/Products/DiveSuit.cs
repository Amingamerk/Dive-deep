namespace DiveDeep.Models
{
    public class DiveSuit : Product
    {
        public Size Size { get; set; }
        public SuitType SuitType { get; set; }
        public string Gender { get; set; } = "";

        public override string VariantLabel => Size.ToString();

        // Kun våddragter har en tykkelse. Tørdragter har ingen, derfor må string gerne være null 
        public string? Thickness { get; set; }

        public override ProductCategory Category => ProductCategory.DiveSuit;

        // Dragter findes i både herre og dameudgave, så hver størrelse hører til en gruppe i dropdown
        public override string? VariantGroup
        {
            get
            {
                if (SuitType == SuitType.Drysuit)
                {
                    return Gender;
                }
                // f.eks. "Herre – 5 mm".
                return $"{Gender} – {Thickness}";
            }
        }
    }
}
