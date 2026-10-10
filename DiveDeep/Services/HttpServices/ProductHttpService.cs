using DiveDeep.Lib.Models;
using DiveDeep.Models;
using Mapster;

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
            using var httpClient = _httpClientFactory.CreateClient("DiveDeepAPI");

            var response = await httpClient.GetAsync($"products/{productId}/booked-dates/{fromDate}/{toDate}");

            if (!(response?.IsSuccessStatusCode) ?? false)
            {
                return null;
            }

            return await response.Content.ReadFromJsonAsync<List<DateTime>>();
        }

        public async Task<List<ProductDto>> GetByCategory(ProductCategory category)
        {
            using var httpClient = _httpClientFactory.CreateClient("DiveDeepAPI");

            var response = await httpClient.GetAsync($"products/categories/{category}");

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
            using var httpClient = _httpClientFactory.CreateClient("DiveDeepAPI");

            var response = await httpClient.GetAsync($"products/{productId}/image");

            if (!(response?.IsSuccessStatusCode) ?? false)
            {
                return null;
            }
            ProductImageDto productImageDto = new();
            productImageDto.Image = await response.Content.ReadAsByteArrayAsync();
            if (response.Content.Headers.ContentType != null)
            {
                productImageDto.ContentType = response.Content.Headers.ContentType.MediaType;
            }
            else
            {
                return null;
            }
            return productImageDto;
        }

        public async Task<List<ProductCategory>> GetProductCategories()
        {
            using var httpClient = _httpClientFactory.CreateClient("DiveDeepAPI");

            var response = await httpClient.GetAsync($"products/categories");

            if (!(response?.IsSuccessStatusCode) ?? false)
            {
                return null;
            }

            return await response.Content.ReadFromJsonAsync<List<ProductCategory>>();
        }

        public async Task<List<ProductDto>> GetVariantsById(int id)
        {
            using var httpClient = _httpClientFactory.CreateClient("DiveDeepAPI");

            var response = await httpClient.GetAsync($"products/{id}/variants");

            if (!(response?.IsSuccessStatusCode) ?? false)
            {
                return null;
            }

            return await response.Content.ReadFromJsonAsync<List<ProductDto>>();
        }
    }
}
