using DiveDeep.Lib.Models;
using DiveDeep.Models;
using DiveDeep.Persistence;
using DiveDeep.Services;
using DiveDeep.Services.HttpServices;
using DiveDeep.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace DiveDeep.Areas.Admin.Controllers
{
    public class ProductsController : AdminBaseController
    {
        private readonly IProductHttpService _productHttpService;
        private readonly ImageService _imageService;

        public ProductsController(IProductHttpService productHttpService, ImageService imageService)
        {
            _productHttpService = productHttpService;
            _imageService = imageService;
        }

        public async Task<IActionResult> Index(ProductCategory? category)
        {
            List<ProductDto> products;
            if (category == null)
            {
                products = await _productHttpService.GetAll();
            }
            else
            {
                products = await _productHttpService.GetByCategory(category.Value);
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
        public async Task<IActionResult> Add(ProductFormViewModel vm)
        {
            if (ModelState.IsValid == false)
            {
                return View(vm);
            }

            ProductDto product = CreateProduct(vm);
            int productId = await _productHttpService.Add(product);

            if (vm.ImageFile != null)
            {
                ProductImageDto productImageDto = new();
                productImageDto.Image = _imageService.ReadFileBytes(vm.ImageFile);
                productImageDto.ContentType = vm.ImageFile.ContentType;
                await _productHttpService.SaveImage(productId, productImageDto);
            }

            TempData["Success"] = $"{product.Brand} {product.Model} er oprettet";
            return RedirectToAction(nameof(Index), new { category = vm.Category });
        }

        public async Task<IActionResult> Edit(int id)
        {
            ProductDto? product = await _productHttpService.GetById(id);
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
            if (product.Category == ProductCategory.BCD)
            {
                vm.Size = product.Size;
            }
            else if (product.Category == ProductCategory.DiveSuit)
            {
                vm.Size = product.Size;
                vm.SuitType = product.SuitType;
                vm.Gender = product.Gender;
                vm.Thickness = product.Thickness;
            }
            else if (product.Category == ProductCategory.Fins)
            {
                vm.Size = product.Size;
            }
            else if (product.Category == ProductCategory.Tank)
            {
                vm.VolumeLiters = product.VolumeLiters;
            }
            else if (product.Category == ProductCategory.RegulatorSet)
            {
                vm.FirstStep = product.FirstStep;
                vm.SecondStep = product.SecondStep;
                vm.Octopus = product.Octopus;
            }

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductFormViewModel vm)
        {
            if (ModelState.IsValid == false)
            {
                return View(vm);
            }

            ProductDto product = CreateProduct(vm);
            await _productHttpService.Update(product);

            if (vm.ImageFile != null)
            {
                ProductImageDto productImageDto = new();
                productImageDto.Image = _imageService.ReadFileBytes(vm.ImageFile);
                productImageDto.ContentType = vm.ImageFile.ContentType;
                await _productHttpService.SaveImage(vm.ProductId, productImageDto);
            }

            TempData["Success"] = $"{product.Brand} {product.Model} er gemt";
            return RedirectToAction(nameof(Index), new { category = vm.Category });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            ProductDto? product = await _productHttpService.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            // ellers ville databasen også slette kundernes bookinger
            if (await _productHttpService.HasBookings(id))
            {
                TempData["Error"] = $"{product.Brand} {product.Model} ({product.VariantLabel}) har bookinger og kan ikke slettes";
                return RedirectToAction(nameof(Index), new { category = product.Category });
            }

            await _productHttpService.Delete(id);
            TempData["Success"] = $"{product.Brand} {product.Model} ({product.VariantLabel}) er slettet";

            return RedirectToAction(nameof(Index), new { category = product.Category });
        }

        // laver en ProductDto ud fra formularen
        private ProductDto CreateProduct(ProductFormViewModel vm)
        {
            ProductDto productDto = new();
            productDto.ProductId = vm.ProductId;
            productDto.Brand = vm.Brand;
            productDto.Model = vm.Model;
            productDto.PricePerDay = vm.PricePerDay;
            productDto.Category = vm.Category;

            // Sæt size for Fins, BCD og DiveSuit
            if (vm.Category == ProductCategory.Fins)
            {
                productDto.Size = vm.Size;
            }
            if (vm.Category == ProductCategory.BCD)
            {
                productDto.Size = vm.Size;
            }
            if (vm.Category == ProductCategory.DiveSuit)
            {
                productDto.Size = vm.Size;
                productDto.SuitType = vm.SuitType;
                productDto.Gender = vm.Gender ?? "";

                // Thickness bruges kun af våddragter
                if (vm.SuitType == SuitType.Wetsuit)
                {
                    productDto.Thickness = vm.Thickness;
                }
            }

            if (vm.Category == ProductCategory.RegulatorSet)
            {
                productDto.FirstStep = vm.FirstStep ?? "";
                productDto.SecondStep = vm.SecondStep ?? "";
                productDto.Octopus = vm.Octopus ?? "";
            }

            if (vm.Category == ProductCategory.Tank)
            {
                productDto.VolumeLiters = vm.VolumeLiters;
            }

            return productDto;
        }
    }
}
