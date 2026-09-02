using Microsoft.AspNetCore.Mvc;
using DiveDeep.Persistence;
using DiveDeep.Models;

namespace DiveDeep.Controllers
{
    public class BundlesController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }
    }
}
