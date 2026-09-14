using DiveDeep.Models;
using DiveDeep.Persistence;
using DiveDeep.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiveDeep.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IProductRepository _productRepository;

        public CartController(ICartService cartService, IProductRepository productRepository)
        {
            _cartService = cartService;
            _productRepository = productRepository;
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
        public IActionResult AddToCart(int productId, string? size, string? gender)
        {
            Product? product = _productRepository.GetById(productId);

            if (product != null)
            {
                _cartService.AddItem(product, size, gender);
                return Json(new { success = true, message = $"{product.Brand} {product.Model} tilføjet til kurven!" });
            }
            
            return Json(new { success = false, message = "Produktet kunne ikke tilføjes." });
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
