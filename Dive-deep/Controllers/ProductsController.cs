using DiveDeep.Persistence;
using Microsoft.AspNetCore.Mvc;
using static Dive_deep.Models.Enums;

namespace Dive_deep.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            var categories = Enum.GetValues<ProductCategory>();
            return View(categories);
        }
        public IActionResult Categories()
        {
            var categories = Enum.GetValues<ProductCategory>();
            return View(categories);
        }

        public IActionResult Category(ProductCategory category)
        {
            var products = ProductRepository.GetByCategory(category)
                .GroupBy(p => new
                {
                    p.Brand,
                    p.Model
                })
                .Select(g => g.First())
                .ToList();

            return View(products);
        }

        public IActionResult Details(int id)
        {
            var product = ProductRepository.GetById(id);

            if (product == null)
            {
                return NotFound();
            }

            var variants = ProductRepository
                .GetVariants(product.Brand, product.Model);

            return View(variants);
        }
    }
}

