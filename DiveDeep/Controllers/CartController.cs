using DiveDeep.Data;
using DiveDeep.Models;
using DiveDeep.Persistence;
using DiveDeep.Services;
using DiveDeep.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DiveDeep.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly BookingService _bookingService;
        private readonly IProductRepository _productRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartController(ICartService cartService, BookingService bookingService, IProductRepository productRepository, UserManager<ApplicationUser> userManager)
        {
            _cartService = cartService;
            _bookingService = bookingService;
            _productRepository = productRepository;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            Cart cart = _cartService.GetCart();

            CartViewModel vm = new()
            {
                Items = cart.Items,
                Total = (decimal)cart.Items.Sum(i => i.PricePerDay * i.Quantity)
            };

            return View(vm);
        }


        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, string dateRange, string? size, string? gender)
        {
            if (!DateRangeParser.TryParse(dateRange, out DateTime startTime, out DateTime endTime))
            {
                TempData["Error"] = "Vælg en gyldig periode";
                return RedirectToAction("Details", "Products", new { id = productId });
            }

            Product? product = _productRepository.GetById(productId);
            if (product == null)
            {
                return NotFound();
            }

            // Produktet lægges kun i kurven. Bookingen gemmes først, når kunden trykker "Book" i kurven
            _cartService.AddItem(product, size, gender, startTime, endTime);
            TempData["Success"] = $"{product.Brand} {product.Model} tilføjet til kurven!";

            return RedirectToAction("Details", "Products", new { id = productId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Book()
        {
            Cart cart = _cartService.GetCart();

            // Find id på den bruger, der er logget ind
            string? userId = _userManager.GetUserId(User);

            List<Booking> bookings = new();

            // konverter CartItem til booking
            foreach (CartItem item in cart.Items)
            {
                Booking booking = new();
                booking.ProductId = item.ProductId;
                booking.StartTime = item.StartTime;
                booking.EndTime = item.EndTime;
                booking.UserId = userId;
                bookings.Add(booking);
            }

            BookingValidationResult bookingValidationResult = _bookingService.CreateBookings(bookings);
            if (!bookingValidationResult.IsSuccessful)
            {
                TempData["Error"] = bookingValidationResult.ErrorMessage;
                return RedirectToAction(nameof(Index));
            }

            _cartService.ClearCart();
            TempData["Success"] = "Tak for din booking! Dit udstyr står klar i butikken på startdatoen.";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int productId, string? size, string? gender)
        {
            _cartService.RemoveItem(productId, size, gender);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult ClearCart()
        {
            _cartService.ClearCart();
            return RedirectToAction(nameof(Index));
        }

    }
}
