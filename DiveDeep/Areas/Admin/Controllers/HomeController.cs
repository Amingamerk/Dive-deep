using DiveDeep.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace DiveDeep.Areas.Admin.Controllers
{
    public class HomeController : AdminBaseController
    {
        public HomeController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
