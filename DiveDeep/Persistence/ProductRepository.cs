using DiveDeep.Data;
using DiveDeep.Models;
using static DiveDeep.Models.Enums;

namespace DiveDeep.Persistence
{
    public class ProductRepository : IProductRepository
    {

        private DiveDeepContext _diveDeepContext;

        public ProductRepository(DiveDeepContext diveDeepContext)
        {
            _diveDeepContext = diveDeepContext;
        }


        public List<Product> GetAll()
        {
            _diveDeepContext.Database.EnsureCreated();
            return _diveDeepContext.Products.ToList();
        }

        public Product? GetById(int id)
        {
            _diveDeepContext.Database.EnsureCreated();

            var product = _diveDeepContext.Products.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                //TODO
            }
            return product;

        }
        
        public List<Product> GetByCategory(ProductCategory category) => GetAll().Where(p => p.Category == category).ToList();

        public List<ProductCategory> GetProductCategories()
        {
            return GetAll().Select(p => p.Category).Distinct().ToList();
        }

        public List<Product> GetVariants(string brand, string model)
        {
            return GetAll()
                .Where(p =>
                    p.Brand == brand &&
                    p.Model == model)
                .ToList();
        }
    }
}
