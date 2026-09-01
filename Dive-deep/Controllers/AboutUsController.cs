using Microsoft.AspNetCore.Mvc;

namespace Dive_deep.Controllers
{
    public class AboutUsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
