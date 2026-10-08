using DiveDeep.Lib.Models;

namespace DiveDeep.Services.HttpServices
{
    public class ProductHttpService : IProductHttpService
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public ProductHttpService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<DateTime>> GetBookedDates(int productId, DateTime fromDate, DateTime toDate)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ProductDto>> GetByCategory(ProductCategory category)
        {
            using var httpClient = _httpClientFactory.CreateClient("DiveDeepAPI");

            var response = await httpClient.GetAsync($"Categories/{category}");

            if (!(response?.IsSuccessStatusCode) ?? false)
            {
                return null;
            }
            
            return await response.Content.ReadFromJsonAsync<List<ProductDto>>();
        }

        public async Task<ProductDto?> GetById(int id)
        {
            using var httpClient = _httpClientFactory.CreateClient("DiveDeepAPI");

            var response = await httpClient.GetAsync($"products/{id}");

            if (!(response?.IsSuccessStatusCode) ?? false)
            {
                return null;
            }

            return await response.Content.ReadFromJsonAsync<ProductDto>();
        }

        public async Task<ProductImageDto?> GetImage(int productId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ProductCategory>> GetProductCategories()
        {
            throw new NotImplementedException();
        }

        public async Task<List<ProductDto>> GetVariants(string brand, string model)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ProductDto>> GetVariants(string brand)
        {
            throw new NotImplementedException();
        }
    }
}
