using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiveDeep.Controllers
{
    public class CartController : Controller
    {
        // GET: BasketsController
        public ActionResult Index()
        {
            return View();
        }

        // GET: BasketsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BasketsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BasketsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BasketsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BasketsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BasketsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BasketsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
