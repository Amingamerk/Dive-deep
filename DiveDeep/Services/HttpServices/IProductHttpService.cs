using DiveDeep.Lib.Models;

namespace DiveDeep.Services.HttpServices
{
    public interface IProductHttpService
    {
        Task<int> Add(ProductDto product);
        Task Update(ProductDto product);
        Task<bool> HasBookings(int id);
        Task Delete(int id);
        Task<List<ProductDto>> GetAll();
        Task<ProductDto?> GetById(int id);
        Task<List<ProductDto>> GetVariantsById(int id);
        Task<List<ProductDto>> GetByCategory(ProductCategory category);
        Task<List<ProductCategory>> GetProductCategories();
        // image stuff
        Task<ProductImageDto?> GetImage(int productId);
        Task SaveImage(int productId, ProductImageDto productImageDto);

        Task<List<DateTime>> GetBookedDates(int productId, DateTime fromDate, DateTime toDate);

        // Availability helpers used by controller
        //Task<bool> IsProductAvailable(int productId, DateTime startDate, DateTime endDate, int requestedQuantity = 1);
        //Task<List<ProductDto>> GetAvailableProducts(ProductCategory category, DateTime startDate, DateTime endDate);
        //List<string> GetBlockedDates(int productId, DateTime startDate, DateTime endDate);

    }
}
