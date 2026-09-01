using Microsoft.AspNetCore.Mvc;

namespace Dive_deep.Controllers
{
    public class BundelsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
