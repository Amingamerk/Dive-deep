using DiveDeep.Models;
using DiveDeep.Data;

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
    }
}
