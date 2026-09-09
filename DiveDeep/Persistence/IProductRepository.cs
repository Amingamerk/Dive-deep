using System;
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

        // Availability helpers used by controller
        bool IsProductAvailable(int productId, DateTime startDate, DateTime endDate, int requestedQuantity = 1);
        List<string> GetBlockedDates(int productId, DateTime startDate, DateTime endDate);

        //void Update
    }
}
