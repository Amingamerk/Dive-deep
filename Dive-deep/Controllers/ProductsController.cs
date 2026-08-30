using Microsoft.AspNetCore.Mvc;

namespace Dive_deep.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
