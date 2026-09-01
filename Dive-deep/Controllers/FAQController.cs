using Microsoft.AspNetCore.Mvc;

namespace Dive_deep.Controllers
{
    public class FAQController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
