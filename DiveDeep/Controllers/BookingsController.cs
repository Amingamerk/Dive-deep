using DiveDeep.Data;
using DiveDeep.Models;
using DiveDeep.Persistence;
using DiveDeep.Services;
using DiveDeep.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace DiveDeep.Controllers
{
    public class BookingsController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly ICartService _cartService;
        private readonly IProductRepository _productRepository;

        public BookingsController(UserManager<ApplicationUser> userManager, ICartService cartService, IProductRepository productRepository)
        {

            _userManager = userManager;
            _cartService = cartService;
            _productRepository = productRepository;
        }

        [HttpPost]
        public IActionResult Add(int productId, string dateRange, string? size, string? gender)
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
    }
}
