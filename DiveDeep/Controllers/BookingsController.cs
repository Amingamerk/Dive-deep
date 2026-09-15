using DiveDeep.Models;
using DiveDeep.Persistence;
using DiveDeep.Services;
using DiveDeep.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace DiveDeep.Controllers
{
    public class BookingsController : Controller
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingsController(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        [HttpPost]
        public IActionResult Add(int productId, string dateRange)
        {
            Booking booking = new();
            booking.ProductId = productId;
            if (DateRangeParser.TryParse(dateRange, out DateTime startTime, out DateTime endTime))
            {
                booking.StartTime = startTime;
                booking.EndTime = endTime;
            }
            else
            {
                // Datoerne kunne ikke læses. Der findes ikke et Add-view, så kunden sendes
                // tilbage til produktet. TempData bruges, fordi ModelState forsvinder ved en redirect
                TempData["Error"] = "Vælg en gyldig periode";
                return RedirectToAction("Details", "Products", new { id = productId });
            }

            _bookingRepository.Add(booking);
            return RedirectToAction("Details", "Products", new { id = productId });
        }
    }
}
