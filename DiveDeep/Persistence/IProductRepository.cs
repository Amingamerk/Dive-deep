using System;
using DiveDeep.Models;

namespace DiveDeep.Persistence
{
    public interface IProductRepository
    {
        void Add(Product product);
        void Update(Product product);
        bool HasBookings(int productId);
        void Delete(int id);
        List<Product> GetAll();
        Product? GetById(int id);
        List<Product> GetVariants(string brand, string model);
        List<Product> GetByCategory(ProductCategory category);
        List<ProductCategory> GetProductCategories();
        // image stuff
        ProductImage? GetImage(int productId);
        void SaveImage(int productId, byte[] data, string contentType);

        List<DateTime> GetBookedDates(int productId, DateTime fromDate, DateTime toDate);

        // Availability helpers used by controller
        bool IsProductAvailable(int productId, DateTime startDate, DateTime endDate, int requestedQuantity = 1);
        List<Product> GetAvailableProducts(ProductCategory category, DateTime startDate, DateTime endDate);
        //List<string> GetBlockedDates(int productId, DateTime startDate, DateTime endDate);

        //void Update
    }
}
