using DiveDeep.Models.Weather;
using DiveDeep.Services.WeatherAPIHttp;
using Microsoft.AspNetCore.Mvc;

namespace DiveDeep.Controllers
{
    public class WeatherController : Controller
    {
        private readonly ICurrentWeatherHttpService _weatherService;

        public WeatherController(ICurrentWeatherHttpService weatherService)
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