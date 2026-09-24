using DiveDeep.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace DiveDeep.Areas.Admin.Controllers
{
    public class HomeController : AdminBaseController
    {
        private readonly IProductRepository _productRepository;

        public HomeController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
