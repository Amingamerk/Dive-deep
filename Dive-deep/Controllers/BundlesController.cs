using Microsoft.AspNetCore.Mvc;
using DiveDeep.Persistence;
using Dive_deep.Models;

namespace Dive_deep.Controllers
{
    public class BundlesController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }
    }
}
