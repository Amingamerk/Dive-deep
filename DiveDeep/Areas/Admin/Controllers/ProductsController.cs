using DiveDeep.Models;
using DiveDeep.Persistence;
using DiveDeep.Services;
using DiveDeep.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DiveDeep.Areas.Admin.Controllers
{
    public class ProductsController : AdminBaseController
    {
        private readonly IProductRepository _productRepository;
        private readonly ImageService _imageService;

        public ProductsController(IProductRepository productRepository, ImageService imageService)
        {
            _productRepository = productRepository;
            _imageService = imageService;
        }

        public IActionResult Index(ProductCategory? category)
        {
            List<Product> products;
            if (category == null)
            {
                products = _productRepository.GetAll();
            }
            else
            {
                products = _productRepository.GetByCategory(category.Value);
            }

            // sorteret efter kategori, mærke og model
            products = products
                .OrderBy(p => p.Category)
                .ThenBy(p => p.Brand)
                .ThenBy(p => p.Model)
                .ThenBy(p => p.ProductId)
                .ToList();

            ViewBag.Category = category;

            return View(products);
        }

        public IActionResult Add(ProductCategory category)
        {
            ProductFormViewModel vm = new();
            vm.Category = category;

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(ProductFormViewModel vm)
        {
            if (ModelState.IsValid == false)
            {
                return View(vm);
            }

            Product product = CreateProduct(vm);
            _productRepository.Add(product);

            if (vm.ImageFile != null)
            {
                _productRepository.SaveImage(product.ProductId, _imageService.ReadFileBytes(vm.ImageFile), vm.ImageFile.ContentType);
            }

            TempData["Success"] = $"{product.Brand} {product.Model} er oprettet";
            return RedirectToAction(nameof(Index), new { category = vm.Category });
        }

        public IActionResult Edit(int id)
        {
            Product? product = _productRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            ProductFormViewModel vm = new();
            vm.ProductId = product.ProductId;
            vm.Category = product.Category;
            vm.Brand = product.Brand;
            vm.Model = product.Model;
            vm.PricePerDay = product.PricePerDay;
            vm.ProductImageId = product.ProductImageId;

            // felter som kun findes på den enkelte kategori
            if (product is BCD bcd)
            {
                vm.Size = bcd.Size;
            }
            else if (product is DiveSuit diveSuit)
            {
                vm.Size = diveSuit.Size;
                vm.SuitType = diveSuit.SuitType;
                vm.Gender = diveSuit.Gender;
                vm.Thickness = diveSuit.Thickness;
            }
            else if (product is Fins fins)
            {
                vm.Size = fins.Size;
            }
            else if (product is Tank tank)
            {
                vm.VolumeLiters = tank.VolumeLiters;
            }
            else if (product is RegulatorSet regulatorSet)
            {
                vm.FirstStep = regulatorSet.FirstStep;
                vm.SecondStep = regulatorSet.SecondStep;
                vm.Octopus = regulatorSet.Octopus;
            }

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductFormViewModel vm)
        {
            if (ModelState.IsValid == false)
            {
                return View(vm);
            }

            Product product = CreateProduct(vm);
            _productRepository.Update(product);

            if (vm.ImageFile != null)
            {
                _productRepository.SaveImage(vm.ProductId, _imageService.ReadFileBytes(vm.ImageFile), vm.ImageFile.ContentType);
            }

            TempData["Success"] = $"{product.Brand} {product.Model} er gemt";
            return RedirectToAction(nameof(Index), new { category = vm.Category });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            Product? product = _productRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            // ellers ville databasen også slette kundernes bookinger
            if (_productRepository.HasBookings(id))
            {
                TempData["Error"] = $"{product.Brand} {product.Model} ({product.VariantLabel}) har bookinger og kan ikke slettes";
                return RedirectToAction(nameof(Index), new { category = product.Category });
            }

            _productRepository.Delete(id);
            TempData["Success"] = $"{product.Brand} {product.Model} ({product.VariantLabel}) er slettet";

            return RedirectToAction(nameof(Index), new { category = product.Category });
        }

        // laver den rigtige type produkt ud fra kategorien
        private Product CreateProduct(ProductFormViewModel vm)
        {
            Product product;

            switch (vm.Category)
            {
                case ProductCategory.BCD:
                    BCD bcd = new();
                    bcd.Size = vm.Size;
                    product = bcd;
                    break;

                case ProductCategory.DiveSuit:
                    DiveSuit diveSuit = new();
                    diveSuit.Size = vm.Size;
                    diveSuit.SuitType = vm.SuitType;
                    diveSuit.Gender = vm.Gender ?? "";
                    diveSuit.Thickness = vm.Thickness;
                    product = diveSuit;
                    break;

                case ProductCategory.Fins:
                    Fins fins = new();
                    fins.Size = vm.Size;
                    product = fins;
                    break;

                case ProductCategory.Tank:
                    Tank tank = new();
                    tank.VolumeLiters = vm.VolumeLiters;
                    product = tank;
                    break;

                case ProductCategory.RegulatorSet:
                    RegulatorSet regulatorSet = new();
                    regulatorSet.FirstStep = vm.FirstStep ?? "";
                    regulatorSet.SecondStep = vm.SecondStep ?? "";
                    regulatorSet.Octopus = vm.Octopus ?? "";
                    product = regulatorSet;
                    break;

                default:
                    product = new MaskSnorkel();
                    break;
            }

            product.ProductId = vm.ProductId;
            product.Brand = vm.Brand;
            product.Model = vm.Model;
            product.PricePerDay = vm.PricePerDay;

            return product;
        }
    }
}
