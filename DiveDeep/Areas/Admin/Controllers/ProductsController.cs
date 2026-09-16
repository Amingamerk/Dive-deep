using DiveDeep.Models;
using DiveDeep.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace DiveDeep.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {
            // Hurtig oversigt: alle produkter sorteret efter kategori, mærke og model
            List<Product> products = _productRepository.GetAll()
                .OrderBy(p => p.Category)
                .ThenBy(p => p.Brand)
                .ThenBy(p => p.Model)
                .ThenBy(p => p.ProductId)
                .ToList();

            return View(products);
        }
    }
}
