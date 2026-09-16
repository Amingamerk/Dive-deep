namespace DiveDeep.Models
{
    public static class CategoryInfo
    {
        public static readonly Dictionary<ProductCategory, (string Title, string ImageFile, string AltText)> Categories = new()
        {
            [ProductCategory.BCD] = ("BCD'er", "BCD.png", "BCD / vestsystem"),
            [ProductCategory.DiveSuit] = ("Dykkerdragter", "wetsuit.png", "Dykkerdragt"),
            [ProductCategory.Fins] = ("Finner", "fins.png", "Svømmefinner"),
            [ProductCategory.MaskSnorkel] = ("Masker & snorkler", "mask.png", "Dykkermaske og snorkel"),
            [ProductCategory.RegulatorSet] = ("Regulatorsæt", "regulator_WIP.png", "Regulatorsæt"),
            [ProductCategory.Tank] = ("Dykkertanke", "tank.png", "Dykkertank")
        };
    }
}
