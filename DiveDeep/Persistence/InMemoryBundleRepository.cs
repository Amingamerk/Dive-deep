using DiveDeep.Models;

namespace DiveDeep.Persistence
{
    public class InMemoryBundleRepository : IBundleRepository
    {
        private static readonly List<Bundle> bundles = new()
        {
            new Bundle
            {
                BundleId = 1,
                Name = "Komplet dykkersæt",
                Description = "BCD, dykkerdragt, regulatorsæt, tank, finner, maske og snorkel.",
                Categories = new List<ProductCategory> { 
                    ProductCategory.BCD,
                    ProductCategory.DiveSuit,
                    ProductCategory.Tank,
                    ProductCategory.RegulatorSet,
                    ProductCategory.MaskSnorkel,
                    ProductCategory.Fins
                },
                DiscountPercent = 20
            },
            new Bundle
            {
                BundleId = 2,
                Name = "Maske, snorkel og finner",
                Description = "Maske, snorkel og finner. Det letteste sted at starte,\r\nhvis du bare vil se, hvad der er dernede.",
                Categories = new List<ProductCategory> {
                    ProductCategory.MaskSnorkel,
                    ProductCategory.Fins
                },
                DiscountPercent = 20

            }
        };

        public List<Bundle> GetAll()
        {
            return bundles;
        }

        public Bundle? GetById(int id)
        {
            Bundle? bundle = bundles.FirstOrDefault(p => p.BundleId == id);
            if (bundle == null)
            {
                //TODO
            }
            return bundle;
        }
    }
}
