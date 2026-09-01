using Microsoft.AspNetCore.Mvc;

namespace Dive_deep.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
