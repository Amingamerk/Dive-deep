using DiveDeep.Models;
using DiveDeep.Persistence;
using DiveDeep.Services;
using Microsoft.AspNetCore.Mvc;

namespace DiveDeep.Controllers
{
    public class BookingsController : Controller
    {
        private readonly IBookingRepository _bookingRepository;


        public BookingsController(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        //public IActionResult Index()
        //{
        //    var bookings = _bookingRepository.GetAll();
        //    return View();
        //}


        [HttpPost]
        public IActionResult Add(Booking booking)
        {
            _bookingRepository.Add(booking);
            return RedirectToAction("Details", "Products", new { id = booking.ProductId });

        }
    }
}
