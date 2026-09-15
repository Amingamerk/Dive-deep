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
        private readonly ICartService _cartService;
        private readonly IProductRepository _productRepository;

        public BookingsController(IBookingRepository bookingRepository, ICartService cartService, IProductRepository productRepository)
        {
            _bookingRepository = bookingRepository;
            _cartService = cartService;
            _productRepository = productRepository;
        }

        [HttpPost]
        public IActionResult Add(int productId, string dateRange, string? size, string? gender)
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
                TempData["Error"] = "Vælg en gyldig periode";
                return RedirectToAction("Details", "Products", new { id = productId });
            }

            _bookingRepository.Add(booking);
            
            // Tilføj produktet til kurven
            Product? product = _productRepository.GetById(productId);
            if (product != null)
            {
                _cartService.AddItem(product, size, gender);
                TempData["Success"] = $"{product.Brand} {product.Model} tilføjet til kurven!";
            }
            
            return RedirectToAction("Details", "Products", new { id = productId });
        }
    }
}
