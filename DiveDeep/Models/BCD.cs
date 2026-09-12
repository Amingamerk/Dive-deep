namespace DiveDeep.Models

{

    public class BCD : Product
    {
        public Size Size { get; set; }
        public override ProductCategory Category => ProductCategory.BCD;
        public override string VariantLabel => Size.ToString();
    }
}
