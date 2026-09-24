using DiveDeep.Data;
using DiveDeep.Models;
using DiveDeep.Persistence;
using DiveDeep.Services;
using DiveDeep.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace DiveDeep.Controllers
{
    [Authorize]
    public class BookingsController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly BookingService _bookingService;
        private readonly IProductRepository _productRepository;

        public BookingsController(UserManager<ApplicationUser> userManager, BookingService bookingService)
        {
            _userManager = userManager;
            _bookingService = bookingService;
        }

        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);
            List<Booking> userBookings = _bookingService.GetByUserId(userId!).OrderBy(b => b.StartTime).ToList();
            return View(userBookings);
        }
    }
}
