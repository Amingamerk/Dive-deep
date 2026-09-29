using DiveDeep.Models.Weather;
using DiveDeep.Services.WheaterAPIHttp;
using Microsoft.AspNetCore.Mvc;

namespace DiveDeep.Controllers
{
    public class WeatherController : Controller
    {
        private readonly ICurrentWheaterHttpService _weatherService;

        public WeatherController(ICurrentWheaterHttpService weatherService)
        {
            _weatherService = weatherService;
        }

        public async Task<IActionResult> Index(string? locationInput)
        {
            if (string.IsNullOrWhiteSpace(locationInput))
            {
                return View();
            }

            CurrentWeather? weather = await _weatherService.GetCurrentWeatherAsync(locationInput);

            if (weather is null)
            {
                ViewBag.Error = "Kunne ikke finde stedet.";
            }

            return View(weather);
        }
    }
}