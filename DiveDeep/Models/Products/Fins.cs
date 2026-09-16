namespace DiveDeep.Models
{
    public class Fins : Product
    {
        public Size Size { get; set; }
        public override ProductCategory Category => ProductCategory.Fins;
        public override string VariantLabel => Size.ToString();
    }
}
