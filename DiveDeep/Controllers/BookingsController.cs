using DiveDeep.Data;
using DiveDeep.Lib.Models;
using DiveDeep.Services.HttpServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DiveDeep.Controllers
{
    [Authorize]
    public class BookingsController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IBookingHttpService _bookingHttpService;

        public BookingsController(UserManager<ApplicationUser> userManager, IBookingHttpService bookingHttpService)
        {
            _userManager = userManager;
            _bookingHttpService = bookingHttpService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            List<BookingDto> bookings = await _bookingHttpService.GetByUserId(userId!);
            List<BookingDto> userBookings = bookings.OrderBy(b => b.StartTime).ToList();
            return View(userBookings);
        }
    }
}
