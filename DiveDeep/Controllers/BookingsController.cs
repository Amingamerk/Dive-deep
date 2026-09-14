using DiveDeep.Models;
using DiveDeep.Persistence;
using DiveDeep.Services;
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
            string[] parts = dateRange.Split(" til ");

            DateTime startTime = DateTime.ParseExact(parts[0], "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime endTime;

            if (parts.Length == 2)
            {
                endTime = DateTime.ParseExact(parts[1], "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            else
            {
                // Hvis der kun er valgt en dag regner vi med at lejen slutter dagen efter.
                // Leje er minimum 24 timer, ellers kan vi ikke markere en dag som optaget
                endTime = startTime.AddDays(1);
            }

            Booking booking = new();
            booking.ProductId = productId;
            booking.StartTime = startTime;
            booking.EndTime = endTime;

            _bookingRepository.Add(booking);
            return RedirectToAction("Details", "Products", new { id = productId });
        }
    }
}
