using DiveDeep.Models;
using DiveDeep.Persistence;
using DiveDeep.Services;
using DiveDeep.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiveDeep.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IBookingRepository _bookingRepository;
        private readonly BookingService _bookingService;

        public CartController(ICartService cartService, IBookingRepository bookingRepository, BookingService bookingService)
        {
            _cartService = cartService;
            _bookingRepository = bookingRepository;
            _bookingService = bookingService;
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
        [ValidateAntiForgeryToken]
        public IActionResult Book()
        {
            Cart cart = _cartService.GetCart();

            if (cart.Items.Count == 0)
            {
                TempData["Error"] = "Din kurv er tom";
                return RedirectToAction(nameof(Index));
            }

            // Tjek ALLE varer, før vi gemmer noget, så kurven aldrig bliver halvt booket
            foreach (CartItem item in cart.Items)
            {
                Booking booking = new();
                booking.ProductId = item.ProductId;
                booking.StartTime = item.StartTime;
                booking.EndTime = item.EndTime;

                BookingValidationResult result = _bookingService.ValidateBooking(booking);
                if (!result.IsSuccessful)
                {
                    TempData["Error"] = $"{item.Brand} {item.Model}: {result.ErrorMessage}";
                    return RedirectToAction(nameof(Index));
                }
            }

            // Alt er i orden: opret én booking per vare i kurven
            foreach (CartItem item in cart.Items)
            {
                Booking booking = new();
                booking.ProductId = item.ProductId;
                booking.StartTime = item.StartTime;
                booking.EndTime = item.EndTime;

                _bookingRepository.Add(booking);
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
