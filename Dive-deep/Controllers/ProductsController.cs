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
                var products = ProductRepository.GetByCategory(category);

                return View(products);
            }
        }
    }

