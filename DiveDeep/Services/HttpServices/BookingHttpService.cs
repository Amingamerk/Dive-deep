using DiveDeep.Lib.Models;

namespace DiveDeep.Services.HttpServices
{
    public class BookingHttpService : IBookingHttpService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BookingHttpService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<BookingDto>> GetAll()
        {
            using var httpClient = _httpClientFactory.CreateClient("DiveDeepAPI");

            var response = await httpClient.GetAsync($"bookings");

            List<BookingDto> bookingDtos = new();
            if (!response.IsSuccessStatusCode)
            {
                return bookingDtos;
            }

            bookingDtos = await response.Content.ReadFromJsonAsync<List<BookingDto>>() ?? new();

            return bookingDtos;
        }

        public async Task<BookingDto?> GetById(int id)
        {
            using var httpClient = _httpClientFactory.CreateClient("DiveDeepAPI");

            var response = await httpClient.GetAsync($"bookings/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return await response.Content.ReadFromJsonAsync<BookingDto>();
        }

        public async Task<List<BookingDto>> GetByUserId(string id)
        {
            using var httpClient = _httpClientFactory.CreateClient("DiveDeepAPI");

            var response = await httpClient.GetAsync($"bookings/user/{id}");

            List<BookingDto> bookingDtos = new();
            if (!response.IsSuccessStatusCode)
            {
                return bookingDtos;
            }

            bookingDtos = await response.Content.ReadFromJsonAsync<List<BookingDto>>() ?? new();

            return bookingDtos;
        }

        public async Task<BundleBookingDto?> GetBundleBookingById(int id)
        {
            using var httpClient = _httpClientFactory.CreateClient("DiveDeepAPI");

            var response = await httpClient.GetAsync($"bookings/bundlebooking/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return await response.Content.ReadFromJsonAsync<BundleBookingDto>();
        }

        public async Task<BookingValidationResult> CreateBookings(List<BookingDto> bookings, List<BundleBookingDto> bundleBookings)
        {
            using var httpClient = _httpClientFactory.CreateClient("DiveDeepAPI");

            // hele kurven sendes i ét kald, så API'et kan tjekke den samlet
            CreateBookingsRequestDto request = new();
            request.Bookings = bookings;
            request.BundleBookings = bundleBookings;

            var response = await httpClient.PostAsJsonAsync("bookings", request);

            return await ReadValidationResult(response);
        }

        public async Task<BookingValidationResult> ValidateBundleProducts(int bundleId, List<int> productIds, DateTime startTime, DateTime endTime)
        {
            using var httpClient = _httpClientFactory.CreateClient("DiveDeepAPI");

            ValidateBundleProductsRequestDto request = new();
            request.BundleId = bundleId;
            request.ProductIds = productIds;
            request.StartTime = startTime;
            request.EndTime = endTime;

            var response = await httpClient.PostAsJsonAsync("bookings/validate-bundle", request);

            return await ReadValidationResult(response);
        }

        public async Task<BookingValidationResult> UpdateBooking(BookingDto booking)
        {
            using var httpClient = _httpClientFactory.CreateClient("DiveDeepAPI");

            var response = await httpClient.PutAsJsonAsync("bookings", booking);

            return await ReadValidationResult(response);
        }

        public async Task<BookingValidationResult> UpdateBundleBooking(List<BookingDto> bookings, DateTime startTime, DateTime endTime)
        {
            using var httpClient = _httpClientFactory.CreateClient("DiveDeepAPI");

            UpdateBundleBookingRequestDto request = new();
            request.Bookings = bookings;
            request.StartTime = startTime;
            request.EndTime = endTime;

            var response = await httpClient.PutAsJsonAsync("bookings/bundlebooking", request);

            return await ReadValidationResult(response);
        }

        public async Task Delete(int id)
        {
            using var httpClient = _httpClientFactory.CreateClient("DiveDeepAPI");

            var response = await httpClient.DeleteAsync($"bookings/{id}");
        }

        public async Task DeleteBundleBooking(int id)
        {
            using var httpClient = _httpClientFactory.CreateClient("DiveDeepAPI");

            var response = await httpClient.DeleteAsync($"bookings/bundlebooking/{id}");
        }

        // alle de kald der validerer svarer med et BookingValidationResult, så vi har lige en metoder som sørger for at de bliver læst på samme måde hver gang
        private static async Task<BookingValidationResult> ReadValidationResult(HttpResponseMessage response)
        {
            BookingValidationResult result = new();

            if (!response.IsSuccessStatusCode)
            {
                result.IsSuccessful = false;
                result.ErrorMessage = "Der skete en fejl. Prøv igen.";
                return result;
            }

            return await response.Content.ReadFromJsonAsync<BookingValidationResult>() ?? result;
        }
    }
}
