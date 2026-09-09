using DiveDeep.Models;
using static DiveDeep.Models.Enums;

namespace DiveDeep.Persistence
{
    public interface IProductRepository
    {
        //void Add
        //void Delete(int id);
        List<Product> GetAll();
        Product? GetById(int id);
        List<Product> GetVariants(string brand, string model);
        List<Product> GetByCategory(ProductCategory category);
        List<ProductCategory> GetProductCategories();
        //void Update
    }
}
