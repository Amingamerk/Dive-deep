using Microsoft.AspNetCore.Mvc;

namespace Dive_deep.Controllers
{
    public class PrivacyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
