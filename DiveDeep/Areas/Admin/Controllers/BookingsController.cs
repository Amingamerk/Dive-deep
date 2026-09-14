using DiveDeep.Models;
using DiveDeep.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace DiveDeep.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookingsController : Controller
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingsController(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public IActionResult Index()
        {
            // Hurtig oversigt: alle bookinger, nyeste først
            List<Booking> bookings = _bookingRepository.GetAll()
                .OrderByDescending(b => b.BookingId)
                .ToList();

            return View(bookings);
        }
    }
}
