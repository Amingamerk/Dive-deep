namespace DiveDeep.Services.WheaterAPIHttp
{
    public class CurrentWheaterHttpService : ICurrentWheaterHttpService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CurrentWheaterHttpService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        }
    }
