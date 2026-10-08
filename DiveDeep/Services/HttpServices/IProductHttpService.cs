using DiveDeep.Lib.Models;

namespace DiveDeep.Services.HttpServices
{
    public interface IProductHttpService
    {
        //Task Add(ProductDto product);
        //Task Update(ProductDto product);
        //Task<bool> HasBookings(int productId);
        //Task Delete(int id);
        //Task<List<ProductDto>> GetAll();
        Task<ProductDto?> GetById(int id);
        Task<List<ProductDto>> GetVariants(string brand, string model);
        Task<List<ProductDto>> GetVariants(string brand);
        Task<List<ProductDto>> GetByCategory(ProductCategory category);
        Task<List<ProductCategory>> GetProductCategories();
        // image stuff
        Task<ProductImageDto?> GetImage(int productId);
        //Task SaveImage(int productId, byte[] data, string contentType);

        Task<List<DateTime>> GetBookedDates(int productId, DateTime fromDate, DateTime toDate);

        // Availability helpers used by controller
        //Task<bool> IsProductAvailable(int productId, DateTime startDate, DateTime endDate, int requestedQuantity = 1);
        //Task<List<ProductDto>> GetAvailableProducts(ProductCategory category, DateTime startDate, DateTime endDate);
        //List<string> GetBlockedDates(int productId, DateTime startDate, DateTime endDate);

        //void Update
    }
}
