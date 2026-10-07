using DiveDeep.API.Models;
using DiveDeep.Lib.Models;

namespace DiveDeep.API.Persistence
{
    public interface IProductRepository
    {
        Task Add(Product product);
        Task Update(Product product);
        Task<bool> HasBookings(int productId);
        Task Delete(int id);
        Task<List<Product>> GetAll();
        Task<Product?> GetById(int id);
        Task<List<Product>> GetVariants(string brand, string model);
        Task<List<Product>> GetByCategory(ProductCategory category);
        Task<List<ProductCategory>> GetProductCategories();
        // image stuff
        Task<ProductImage?> GetImage(int productId);
        Task SaveImage(int productId, byte[] data, string contentType);

        Task<List<DateTime>> GetBookedDates(int productId, DateTime fromDate, DateTime toDate);

        // Availability helpers used by controller
        Task<bool> IsProductAvailable(int productId, DateTime startDate, DateTime endDate, int requestedQuantity = 1);
        Task<List<Product>> GetAvailableProducts(ProductCategory category, DateTime startDate, DateTime endDate);
        //List<string> GetBlockedDates(int productId, DateTime startDate, DateTime endDate);

        //void Update
    }
}
