using Dive_deep.Models;
using static Dive_deep.Models.Enums;
using DiveDeep.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace Dive_deep.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            List<ProductCategory> categories = ProductRepository.GetProductCategories();
            return View(categories);
        }
    }
}
