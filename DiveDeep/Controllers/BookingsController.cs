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
                ModelState.AddModelError("DateRange", "Vælg en gyldig periode");
                return View();
            }

            _bookingRepository.Add(booking);
            return RedirectToAction("Details", "Products", new { id = productId });
        }
    }
}
