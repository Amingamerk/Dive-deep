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

        public async Task<int> Add(ProductDto product)
        {
            using var httpClient = _httpClientFactory.CreateClient("DiveDeepAPI");

            var response = await httpClient.PostAsJsonAsync($"products/add", product);

            return await response.Content.ReadFromJsonAsync<int>();
        }

        public async Task Delete(int id)
        {
            using var httpClient = _httpClientFactory.CreateClient("DiveDeepAPI");

            var response = await httpClient.DeleteAsync($"products/{id}");
        }

        public async Task<List<ProductDto>> GetAll()
        {
            using var httpClient = _httpClientFactory.CreateClient("DiveDeepAPI");

            var response = await httpClient.GetAsync($"products");

            List<ProductDto> productDtos = new();
            if (!(response?.IsSuccessStatusCode) ?? false)
            {
                return productDtos;
            }

            productDtos = await response.Content.ReadFromJsonAsync<List<ProductDto>>();

            return productDtos;
        }

        public async Task<List<DateTime>> GetBookedDates(int productId, DateTime fromDate, DateTime toDate)
        {
            using var httpClient = _httpClientFactory.CreateClient("DiveDeepAPI");
            string fromDateString = fromDate.ToString("yyyy-MM-dd");
            string toDateString = toDate.ToString("yyyy-MM-dd");
            var response = await httpClient.GetAsync($"products/{productId}/booked-dates/{fromDateString}/{toDateString}");

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
                return new List<ProductDto>();
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

        public async Task SaveImage(int productId, ProductImageDto productImageDto)
        {
            using var httpClient = _httpClientFactory.CreateClient("DiveDeepAPI");

            var response = await httpClient.PostAsJsonAsync($"products/{productId}/add-image", productImageDto);
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

        public async Task<bool> HasBookings(int id)
        {
            using var httpClient = _httpClientFactory.CreateClient("DiveDeepAPI");

            var response = await httpClient.GetAsync($"products/{id}/has-bookings");

            if ((response?.IsSuccessStatusCode) ?? false)
            {
                return await response.Content.ReadFromJsonAsync<bool>();
            }
            else
            {
                return true;
            }
        }

        public async Task Update(ProductDto product)
        {
            using var httpClient = _httpClientFactory.CreateClient("DiveDeepAPI");

            var response = await httpClient.PutAsJsonAsync("products", product);
        }
    }
}
